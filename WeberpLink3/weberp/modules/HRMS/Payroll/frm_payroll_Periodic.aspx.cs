using LibraryPAY;
using LibraryPAY.DsSalaryTableAdapters;
using LibraryPAY.DsUbasysTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_frm_payroll_Periodic : System.Web.UI.Page
{
    private const string Rnode = "aa";
    string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        GlobalData.ConfirmBox(btnsave, "Are you sure to save ?");
        GlobalData.ConfirmBox(btnpost, "Are you sure to post ?");
        GlobalData.ConfirmBox(btnjv, "Are you sure to create JV ?");
        
        if (!Page.IsPostBack)
        {
            tbldet.Visible = false;
            LoadCompanyByUserPermission("ADM", Rnode);
            ClsDropDownListController.EnableDisableDropDownList(ddlcompany);
            ddlcompany_SelectedIndexChanged(sender, e);
        }
    }
    
    private void LoadCompanyByUserPermission(string userid, string nodeid)
    {
        DataTable dt = new DataTable();
        ListItem lst;
        ddlcompany.Items.Clear();
        ddlcompany.Items.Add("");
        dt = AccessPermission.GetCompanyByUserandNodeid(ConnectionString, userid, nodeid);
        foreach (DataRow dr in dt.Rows)
        {
            lst = new ListItem();
            lst.Text = dr["COMP_CODE"].ToString() + ":" + dr["COMP_NAME"].ToString();
            lst.Value = dr["COMP_CODE"].ToString();
            ddlcompany.Items.Add(lst);
        }
    }

    protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbldet.Visible = false;
        if (ddlcompany.Text == "") return;
        tbldet.Visible = true;
        string dbname = ddlcompany.SelectedItem.Value.ToString();
        string constr = System.Configuration.ConfigurationSettings.AppSettings["SCFConnectionString"].ToString().Replace("SCF", dbname);
        Session[GlobalData.sessionConnectionstring] = constr;
        Session["CompanyName"] = ddlcompany.SelectedItem.Text;
        Session["CompanyAddress"] = "";
        try
        {
            DataTable dtForLastSal = DataProcess.GetData(ConnectionString, Sqlgenerate.SqlGetLastSalaryMonth());
            DateTime dtlastsal = Convert.ToDateTime(dtForLastSal.Rows[0][0].ToString());
            lblsalmonth.Text = string.Format("{0:00}", dtlastsal.Month) + " / " + dtlastsal.Year.ToString();
            TextBox1.Text = string.Format("{0:00}", dtlastsal.AddMonths(1).Month) + " / " + dtlastsal.Year.ToString();
        }
        catch
        {
            lblsalmonth.Text = "Not Calculated";
            TextBox1.Text = string.Format("{0:00}", DateTime.Now.Month) + " / " + DateTime.Now.Year.ToString();
        }
    }
    
    protected void btnsave_Click(object sender, EventArgs e)
    {
        DateTime sttime = DateTime.Now;
        DataTable dtempmas = new DataTable();
        DataTable dtgrade = new DataTable();
        DataTable dtfordet = new DataTable();
        int mm = 0, yyyy = 0;
        string[] tmp = TextBox1.Text.Split('/');
        if (tmp.Length < 2) return;
        string dbname = ddlcompany.SelectedItem.Value.ToString();
        try
        {
            mm = Convert.ToInt32(tmp[0]);
            yyyy = Convert.ToInt32(tmp[1]);
        }
        catch
        {
            lblmsg.Text = "Period Selection Error";
            lblmsg.Visible = true;
            return;
        }
        if (ddlcompany.Text == "")
        {
            lblmsg.Text = "Company Selection Error";
            lblmsg.Visible = true;
            return;
        }
        try
        {
            if (DataProcess.GetData(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.GetDataForPost(dbname, mm, yyyy).ToString()).Rows.Count > 0)
            {
                lblmsg.Text = "Salary Journal Already Created";
                lblmsg.Visible = true;
                return;
            }
        }
        catch
        {
            return;
        }
        DateTime saldate = Convert.ToDateTime("01/01/2000");
        saldate = saldate.AddYears(yyyy - 2000);
        saldate = saldate.AddMonths(mm);
        saldate = saldate.AddDays(-1);
        DateTime findate;
        int totdays = saldate.Day;
        int paydays;
        bool flg, trnflag = true;
        decimal stdval = 0;
        decimal STDBAS = 0;
        decimal STDMA = 0;
        decimal DEDRAT = 0;
        decimal STDENT = 0;
        decimal STDOTH = 0;
        decimal STDCON = 0;
        decimal STDHRA = 0;
        decimal Deducs = 0;
        decimal NET = 0;
        decimal PFEC = 0;
        decimal earns = 0;
        decimal ADVAM1 = 0;
        string salflg = "";
        decimal Bonus = 0;
        SqlConnection connectionString = new SqlConnection(Session[GlobalData.sessionConnectionstring].ToString());
        connectionString.Open();
        SqlTransaction transactionString = connectionString.BeginTransaction();
        try
        {
            DataProcess.DeleteQuery(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.DeleteAllSalary1());
            findate = Convert.ToDateTime("01/" + mm.ToString() + "/" + yyyy.ToString()).AddMonths(1).AddSeconds(-1); 
            dtempmas = DataProcess.GetData(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.GetDataForPeriodic(findate, mm, yyyy, saldate));
            foreach (DataRow dr in dtempmas.Rows)
            {
                STDBAS = 0;
                STDMA = 0;
                STDENT = 0;
                STDOTH = 0;
                STDCON = 0;
                STDHRA = 0;
                Deducs = 0;
                NET = 0;
                ADVAM1 = 0;
                Bonus = 0;
                if (dr["payday"].ToString() == null)
                {
                    paydays = Convert.ToInt32(dr["PrsDys"].ToString()) + Convert.ToInt32(dr["LevDys"].ToString()) + Convert.ToInt32(dr["Holydays"].ToString());
                }
                else
                {
                    paydays = Convert.ToInt32(dr["payday"].ToString());
                }
                DEDRAT = Convert.ToDecimal((decimal)paydays / (decimal)totdays);
                dtfordet = DataProcess.GetData(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.GetDataByEmp(dr["Emp_Mas_Emp_Id"].ToString()));
                dtgrade = DataProcess.GetData(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.GetDataByGrade(dr["det_grade"].ToString()));
                foreach (DataRow grdr in dtgrade.Rows)
                {
                    flg = false;
                    salflg = "";
                    stdval = 0;
                    foreach (DataRow fordr in dtfordet.Rows)
                    {
                        if (grdr["Det_For"].ToString().Trim() == fordr["For_Det_ForCode"].ToString().Trim())
                        {
                            stdval = Convert.ToDecimal(fordr["For_Det_Value"].ToString());
                            flg = true;
                            goto nextcode;
                        }
                    }
                nextcode:
                    if (flg)
                    {
                        if (grdr["Det_For"].ToString() == "STDBAS")
                        {
                            STDBAS = stdval;
                        }
                        if (grdr["Det_For"].ToString() == "STDCON")
                        {
                            STDCON = stdval;
                        }
                        if (grdr["Det_For"].ToString() == "STDENT")
                        {
                            STDENT = stdval;
                        }
                        if (grdr["Det_For"].ToString() == "STDHRA")
                        {
                            STDHRA = stdval;
                        }
                        if (grdr["Det_For"].ToString() == "STDMA")
                        {
                            STDMA = stdval;
                        }
                        if (grdr["Det_For"].ToString() == "STDOTH")
                        {
                            STDOTH = stdval;
                        }
                        if (grdr["Det_For"].ToString() == "PFEC")
                        {
                            NET = NET + stdval;
                            Deducs += stdval * 2;
                        }
                        if (grdr["Det_For"].ToString() == "PFEMP")
                        {
                            // PFEMP = stdval;                                                      
                        }
                        if (grdr["Det_For"].ToString() == "PFEMPR")
                        {
                            // PFEMPR = stdval;                                                      
                        }
                        if (grdr["Det_For"].ToString() == "PFEMPE")
                        {
                            // PFEMPE = stdval;                                                      
                        }
                        if (grdr["Det_For"].ToString() == "INCAMT")
                        {
                            Deducs += stdval;
                        }
                        if (grdr["Det_For"].ToString() == "WFSUB")
                        {
                            Deducs += stdval;
                        }

                        if (grdr["Det_For"].ToString() == "MOB_DE")
                        {
                            Deducs += stdval;
                        }
                        if (grdr["Det_For"].ToString() == "AREAR")
                        {
                            NET += stdval;
                        }
                        if (grdr["Det_For"].ToString() == "AdvAmt")
                        {
                            Deducs += stdval;
                        }
                        if (grdr["Det_For"].ToString() == "ADVAM1")
                        {
                            ADVAM1 = stdval;
                            Deducs += stdval;
                        }
                        if (grdr["Det_For"].ToString() == "REVSTA")
                        {
                            Deducs += stdval;
                        }
                        if (grdr["Det_For"].ToString() == "Bonus")
                        {
                            NET += stdval;
                        }
                        if (grdr["Det_For"].ToString() == "TA")
                        {
                            NET += stdval;
                        }
                        if (grdr["Det_For"].ToString() == "DA")
                        {
                            NET += stdval;
                        }
                        if (grdr["Det_For"].ToString() == "FDAL")
                        {
                            stdval = stdval * Convert.ToDecimal(dr["PrsDys"].ToString());
                            NET += stdval;
                        }
                        DataProcess.InsertQuery(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.InsertSalary1(dr["Emp_Mas_Emp_Id"].ToString(), Convert.ToDateTime(saldate.ToShortDateString()), grdr["Det_For"].ToString(), Convert.ToDecimal(stdval.ToString("N2")), salflg, "", dr["det_grade"].ToString(), "P"));
                    }
                    else
                    {
                        switch (grdr["Det_For"].ToString())
                        {
                            case "BASIC"://
                                {
                                    stdval = Convert.ToDecimal((STDBAS * DEDRAT).ToString("N2"));
                                    NET = NET + stdval;
                                    break;
                                }
                            case "HRA"://
                                {
                                    stdval = Convert.ToDecimal((STDHRA * DEDRAT).ToString("N2"));
                                    NET = NET + stdval;
                                    break;
                                }
                            case "MEDALL"://
                                {
                                    stdval = Convert.ToDecimal((STDMA * DEDRAT).ToString("N2"));
                                    NET = NET + stdval;
                                    break;
                                }
                            case "CONVEY"://
                                {
                                    stdval = Convert.ToDecimal((STDCON * DEDRAT).ToString("N2"));
                                    NET = NET + stdval;
                                    break;
                                }
                            case "ENTALL"://
                                {
                                    stdval = Convert.ToDecimal((STDENT * DEDRAT).ToString("N2"));
                                    NET = NET + stdval;
                                    break;
                                }
                            case "OTHALL"://
                                {
                                    stdval = Convert.ToDecimal((STDOTH * DEDRAT).ToString("N2"));
                                    NET = NET + stdval;
                                    break;
                                }
                            case "PFEC"://
                                {
                                    PFEC = Convert.ToDecimal((STDBAS * Convert.ToDecimal(.10)).ToString("N2"));
                                    stdval = PFEC;
                                    NET = NET + stdval;
                                    Deducs += stdval * 2;
                                    break;
                                }
                            case "PFEMP"://
                            case "PFEMPR"://
                            case "PFEMPE"://
                                {
                                    stdval = Convert.ToDecimal((STDBAS * Convert.ToDecimal(.10)).ToString("N2"));
                                    break;
                                }
                            case "REVSTA"://
                                {
                                    stdval = Convert.ToDecimal(grdr["Det_Base"].ToString());
                                    Deducs += stdval;
                                    break;
                                }
                            case "AdvAmt"://
                                {
                                    stdval = Convert.ToDecimal(dr["AdvAmt"].ToString());
                                    Deducs += stdval;
                                    break;
                                }
                            case "ADVAM1"://
                                {
                                    stdval = 0;
                                    break;
                                }
                            case "MOB_DE"://
                            case "AREAR"://
                            case "HctDys"://
                            case "INCAMT"://
                            case "TA"://
                            case "DA"://
                            case "FDAL"://
                                {
                                    //Deducs += stdval;
                                    stdval = 0;
                                    break;
                                }
                            case "NETADV"://
                                {
                                    stdval = Convert.ToDecimal(dr["AdvAmt"].ToString()) + ADVAM1;
                                    break;
                                }
                            case "AtdDys"://
                                {
                                    stdval = Convert.ToDecimal(dr["PrsDys"].ToString());
                                    break;
                                }
                            case "DEDRAT"://
                                {
                                    stdval = Convert.ToDecimal(DEDRAT.ToString("N2"));
                                    break;
                                }
                            case "HolDys"://
                                {
                                    stdval = Convert.ToDecimal(dr["Holydays"].ToString());
                                    break;
                                }
                            case "LevDys"://
                                {
                                    stdval = Convert.ToDecimal(dr["LevDys"].ToString());
                                    break;
                                }
                            case "PAYDAY"://
                                {
                                    stdval = paydays;
                                    break;
                                }
                            case "PrsDys"://
                                {
                                    stdval = Convert.ToDecimal(dr["PrsDys"].ToString());
                                    break;
                                }
                            case "PYDAY1"://
                                {
                                    stdval = 0;
                                    break;
                                }
                            case "SAL"://
                                {
                                    stdval = 0;
                                    break;
                                }
                            case "TODAY1"://
                                {
                                    stdval = Convert.ToDecimal(dr["PrsDys"].ToString()) + Convert.ToDecimal(dr["LevDys"].ToString());
                                    break;
                                }
                            case "TODAY2"://
                                {
                                    stdval = Convert.ToDecimal(dr["PrsDys"].ToString()) + Convert.ToDecimal(dr["LevDys"].ToString()) + Convert.ToDecimal(dr["Holydays"].ToString());
                                    break;
                                }
                            case "TotDys"://
                                {
                                    stdval = totdays;
                                    break;
                                }
                            case "WFSUB"://
                                {
                                    stdval = Convert.ToDecimal(grdr["Det_Base"].ToString());
                                    Deducs += stdval;
                                    break;
                                }
                            default:
                                {
                                    stdval = -1;
                                    break;
                                }
                        }
                        if (stdval != -1)
                        {
                            DataProcess.InsertQuery(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.InsertSalary1(dr["Emp_Mas_Emp_Id"].ToString(), Convert.ToDateTime(saldate.ToShortDateString()), grdr["Det_For"].ToString(), Convert.ToDecimal(stdval.ToString("N2")), salflg, "", grdr["Det_Code"].ToString(), "P"));
                        }
                    }
                }
                NET = NET - Deducs;
                earns = NET + Deducs;
                DataProcess.InsertQuery(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.InsertSalary1(dr["Emp_Mas_Emp_Id"].ToString(), Convert.ToDateTime(saldate.ToShortDateString()), "NET", NET, "N", "", dtgrade.Rows[0][0].ToString(), "P"));
                DataProcess.InsertQuery(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.InsertSalary1(dr["Emp_Mas_Emp_Id"].ToString(), Convert.ToDateTime(saldate.ToShortDateString()), "Deducs", Deducs, "D", "", dtgrade.Rows[0][0].ToString(), "P"));
                DataProcess.InsertQuery(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.InsertSalary1(dr["Emp_Mas_Emp_Id"].ToString(), Convert.ToDateTime(saldate.ToShortDateString()), "Earns", earns, "P", "", dtgrade.Rows[0][0].ToString(), "P"));
            }
            transactionString.Commit();
            LoadSalary();
        }
        catch (Exception ex)
        {
            transactionString.Rollback();
            trnflag = false;
        }
        finally
        {
            connectionString.Close();
        }
        DateTime endtime = DateTime.Now;
        TimeSpan ts = endtime - sttime;
        lblmsg.Visible = true;
        if (trnflag)
        {
            lblmsg.Text = (ts.Minutes * 60 + ts.Seconds).ToString() + " Seconds";
        }
        else
        {
            lblmsg.Text = "Transaction Error, Please Try Again";
        }
    }

    protected void btnpost_Click(object sender, EventArgs e)
    {
        DateTime sttime = DateTime.Now;
        bool flg = true;
        string dbname = ddlcompany.SelectedItem.Value.ToString();
        DataTable dtForSal1 = DataProcess.GetData(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.GetDataFromhrms_salary1());
        if (dtForSal1.Rows.Count == 0) return;
        if (DataProcess.GetData(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.GetDataForPost(dbname, Convert.ToDateTime(dtForSal1.Rows[0][1].ToString()).Month, Convert.ToDateTime(dtForSal1.Rows[0][1].ToString()).Year)).Rows.Count > 0)
        {
            lblmsg.Text = "Salary Journal Already Created";
            lblmsg.Visible = true;
            return;
        }
        SqlConnection connectionString = new SqlConnection(Session[GlobalData.sessionConnectionstring].ToString());
        connectionString.Open();
        SqlTransaction transactionString = connectionString.BeginTransaction();
        try
        {
            DataProcess.DeleteQuery(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.DeletePostedSalary(Convert.ToDateTime(dtForSal1.Rows[0][1].ToString()).Month, Convert.ToDateTime(dtForSal1.Rows[0][1].ToString()).Year));
            DataProcess.InsertQuery(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.salaryPost());
            transactionString.Commit();
        }
        catch (Exception ex)
        {
            transactionString.Rollback();
            flg = false;
        }
        finally
        {
            connectionString.Close();
        }
        DateTime endtime = DateTime.Now;
        TimeSpan ts = endtime - sttime;
        lblmsg.Visible = true;
        if (flg)
        {
            lblmsg.Text = (ts.Minutes * 60 + ts.Seconds).ToString() + " Seconds";
        }
        else
        {
            lblmsg.Text = "Transaction Error, Please Try Again";
        }
    }

    protected void btnjv_Click(object sender, EventArgs e)
    {
        DateTime sttime = DateTime.Now;
        DataTable dtDpartment = null;
        DataTable dtdptjour = null;
        DataTable dtemp = null;
        DataTable dtSal1 = DataProcess.GetData(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.GetDataFromhrms_salary1());
        if (dtSal1.Rows.Count == 0) return;
        string period = string.Format("{0:00}", Convert.ToDateTime(dtSal1.Rows[0][1].ToString()).Month) + "/" + Convert.ToDateTime(dtSal1.Rows[0][1].ToString()).Year.ToString();
        int maxref = Convert.ToInt32(
            DataProcess.GetSingleValueFromtable(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.GetMaxRef(period)) == string.Empty ? null :
            DataProcess.GetSingleValueFromtable(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.GetMaxRef(period))
            );
        string jvref = "";
        string pref = "SJV" + Convert.ToDateTime(dtSal1.Rows[0][1].ToString()).Year.ToString().Substring(2, 2) + Convert.ToDateTime(dtSal1.Rows[0][1].ToString()).ToString("MMM").ToUpper();
        string narr = "";
        int seq = 0;
        decimal totc, totd;
        bool flg = true;
        string dbname = ddlcompany.SelectedItem.Value.ToString();
        if (DataProcess.GetData(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.GetDataForPost(dbname, Convert.ToDateTime(dtSal1.Rows[0][1].ToString()).Month, Convert.ToDateTime(dtSal1.Rows[0][1].ToString()).Year)).Rows.Count > 0)
        {
            lblmsg.Text = "Salary Journal Already Created";
            lblmsg.Visible = true;
            return;
        }
        SqlConnection connectionString = new SqlConnection(Session[GlobalData.sessionConnectionstring].ToString());
        connectionString.Open();
        SqlTransaction transactionString = connectionString.BeginTransaction();
        try
        {
            dtDpartment = DataProcess.GetData(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.GetDataHrmsDepartment());
            foreach (DataRow dr in dtDpartment.Rows)
            {
                seq = 0;
                jvref = pref + string.Format("{0:000000}", maxref);
                narr = "Salary of " + dr["Dept_Name"].ToString() + " for the month of " + period;
                DataProcess.InsertQuery(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.InsertWH("ADM", jvref, "JV", Convert.ToDateTime(dtSal1.Rows[0][1].ToString()), DateTime.Now, period, "L", "Y", "Salary", "J", 0));
                dtdptjour = new DataTable();
                dtdptjour = DataProcess.GetData(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.GetEarnsDataByDivdept(dr["Dept_Division_Code"].ToString(), dr["Dept_Code"].ToString()));
                foreach (DataRow drj in dtdptjour.Rows)
                {
                    DataProcess.InsertQuery(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.InsertWD(
                        jvref,
                        Convert.ToSingle(drj["Led_Det_SeqNo"].ToString()),
                        (drj["Led_Det_DCFlag"].ToString() == "D" ? dr["PayRoll_GL_Acc"].ToString() : drj["Led_Det_AccCode"].ToString()),
                        narr,
                        drj["Led_Det_DCFlag"].ToString(),
                        Convert.ToDecimal(drj["CalVal"].ToString()),
                        jvref,
                        0, "", 0, 0, "", (drj["Led_Det_DCFlag"].ToString() == "D" ? dr["Dept_Code"].ToString() : drj["Led_Det_Grpt1"].ToString()),
                        drj["Led_Det_Grpt2"].ToString(), "", "", "", drj["Led_Det_Grpt6"].ToString(), drj["Led_Det_Grpt7"].ToString(), "", "", "", "", "",
                        (DateTime.Now).ToString("dd-MMM-yyyy"), "", "", "", "", "", "", "", seq));
                    seq++;
                }
                dtdptjour = new DataTable();
                dtdptjour = DataProcess.GetData(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.GetDataTADAByDivDept(dr["Dept_Division_Code"].ToString(), dr["Dept_Code"].ToString()));
                foreach (DataRow drj in dtdptjour.Rows)
                {
                    if (Convert.ToDecimal(drj["CalVal"].ToString()) > 0)
                    {
                        DataProcess.InsertQuery(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.InsertWD(
                        jvref,
                        Convert.ToSingle(drj["Led_Det_SeqNo"].ToString()),
                        dr["PayRoll_GL_TADA"].ToString(),
                        narr,
                        drj["Led_Det_DCFlag"].ToString(),
                        Convert.ToDecimal(drj["CalVal"].ToString()),
                        jvref, 0, "", 0, 0, "",
                        (drj["Led_Det_DCFlag"].ToString() == "D" ? dr["Dept_Code"].ToString() : drj["Led_Det_Grpt1"].ToString()),
                        drj["Led_Det_Grpt2"].ToString(), "", "", "", drj["Led_Det_Grpt6"].ToString(), drj["Led_Det_Grpt7"].ToString(), "", "", "", "", "",
                        (DateTime.Now).ToString("dd-MMM-yyyy"), "", "", "", "", "", "", "", seq));
                        seq++;
                    }
                }
                dtdptjour = new DataTable();
                dtdptjour = DataProcess.GetData(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.GetDataFDALByDivdept(dr["Dept_Division_Code"].ToString(), dr["Dept_Code"].ToString()));
                foreach (DataRow drj in dtdptjour.Rows)
                {
                    if (Convert.ToDecimal(drj["CalVal"].ToString()) > 0)
                    {
                        DataProcess.InsertQuery(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.InsertWD(
                            jvref,
                            Convert.ToSingle(drj["Led_Det_SeqNo"].ToString()),
                            dr["PayRoll_GL_FDAL"].ToString(),
                            narr, drj["Led_Det_DCFlag"].ToString(),
                            Convert.ToDecimal(drj["CalVal"].ToString()),
                            jvref, 0, "", 0, 0, "",
                            (drj["Led_Det_DCFlag"].ToString() == "D" ? dr["Dept_Code"].ToString() : drj["Led_Det_Grpt1"].ToString()),
                            drj["Led_Det_Grpt2"].ToString(), "", "", "",
                            drj["Led_Det_Grpt6"].ToString(),
                            drj["Led_Det_Grpt7"].ToString(), "", "", "", "", "",
                            (DateTime.Now).ToString("dd-MMM-yyyy"), "", "", "", "", "", "", "", seq));
                        seq++;
                    }
                }
                dtdptjour = new DataTable();
                dtdptjour = DataProcess.GetData(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.GetOthersDataByDivDept(dr["Dept_Division_Code"].ToString(), dr["Dept_Code"].ToString()));
                foreach (DataRow drj in dtdptjour.Rows)
                {
                    DataProcess.InsertQuery(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.InsertWD(
                        jvref,
                        Convert.ToSingle(drj["Led_Det_SeqNo"].ToString()),
                        (drj["Led_Det_DCFlag"].ToString() == "D" ? dr["PayRoll_GL_Acc"].ToString() : drj["Led_Det_AccCode"].ToString()),
                        narr,
                        drj["Led_Det_DCFlag"].ToString(),
                        Convert.ToDecimal(drj["CalVal"].ToString()),
                        jvref, 0, "", 0, 0, "",
                        (drj["Led_Det_DCFlag"].ToString() == "D" ? dr["Dept_Code"].ToString() : drj["Led_Det_Grpt1"].ToString()),
                        drj["Led_Det_Grpt2"].ToString(), "", "", "",
                        drj["Led_Det_Grpt6"].ToString(), drj["Led_Det_Grpt7"].ToString(), "", "", "", "", "",
                        (DateTime.Now).ToString("dd-MMM-yyyy"), "", "", "", "", "", "", "", seq));
                    seq++;
                }
                seq = 0;
                dtemp = new DataTable();
                dtemp = DataProcess.GetData(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.GetDataByDivDept(dr["Dept_Division_Code"].ToString(), dr["Dept_Code"].ToString()));
                foreach (DataRow drj in dtemp.Rows)
                {
                    DataProcess.InsertQuery(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.InsertWD(
                        jvref,
                        Convert.ToSingle(drj["Led_Det_SeqNo"].ToString()),
                        drj["Led_Det_AccCode"].ToString(),
                        narr,
                        drj["Led_Det_DCFlag"].ToString(),
                        Convert.ToDecimal(drj["CalVal"].ToString()),
                        jvref, 0, "", 0, 0, "",
                        drj["Trans_Det_Emp_Id"].ToString(), "", "", "", "", "T01", "", "", "", "", "", "",
                        (DateTime.Now).ToString("dd-MMM-yyyy"), "", "", "", "", "", "", "", seq));
                    seq++;
                }
                totc = Convert.ToDecimal(DataProcess.GetSingleValueFromtable(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.GetTotAmount("C", jvref)));
                totd = Convert.ToDecimal(DataProcess.GetSingleValueFromtable(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.GetTotAmount("D", jvref)));
                if (totc != totd)
                {
                    flg = false;
                    goto eot;
                }
                maxref++;
            }
        eot:
            if (flg)
            {
                transactionString.Commit();
                DataProcess.InsertQuery(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.InsertJv(dbname, Convert.ToDateTime(dtSal1.Rows[0][1].ToString()).Month, Convert.ToDateTime(dtSal1.Rows[0][1].ToString()).Year, (DateTime.Now).ToString("dd-MMM-yyyy")));
            }
            else
            {
                transactionString.Rollback();
            }
        }
        catch (Exception ex)
        {
            transactionString.Rollback();
            flg = false;
        }
        finally
        {
            connectionString.Close();
        }
        DateTime endtime = DateTime.Now;
        TimeSpan ts = endtime - sttime;
        lblmsg.Visible = true;
        if (flg)
        {
            lblmsg.Text = (ts.Minutes * 60 + ts.Seconds).ToString() + " Seconds";
        }
        else
        {
            lblmsg.Text = "Transaction Error, Please Try Again";
        }
    }

    private void LoadSalary()
    {
        DataTable dt = new DataTable();
        gdvView.DataSource = null;
        double HO, FO;
        HO = 0;
        FO = 0;
        dt = DataProcess.GetData(Session[GlobalData.sessionConnectionstring].ToString(), SqlgenerateForPayroll.GetDatabyDeptDiv());
        gdvView.DataSource = dt;
        gdvView.DataBind();
        foreach (DataRow dtr in dt.Rows)
        {
            if (dtr[0].ToString() == "HO")
            {
                HO = HO + Convert.ToDouble(dtr[2].ToString());
            }
            else
            {
                FO = FO + Convert.ToDouble(dtr[2].ToString());
            }
        }
        lblmsgSal.Text = "";
        lblmsgSal.Text = "HO Grand Total Salary: " + Convert.ToString(HO) + "     FO Grand Total Salary: " + Convert.ToString(FO);
    }
}