using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Payroll_frm_payroll_NewCycle : System.Web.UI.Page
{
    private const string Rnode = "aa";
    string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);
        GlobalData.ConfirmBox(btnsave, "Are you sure to save ?");
        GlobalData.ConfirmBox(btnpost, "Are you sure to post ?");
        GlobalData.ConfirmBox(btnjv, "Are you sure to create JV ?");
        GlobalData.ConfirmBox(btnremove, "Are you sure to remove pay setup ?");
        if (!Page.IsPostBack)
        {
            tbldet.Visible = false;
            LoadCompanyByUserPermission("ADM", Rnode);
            ClsDropDownListController.EnableDisableDropDownList(ddlcompany);
            ddlcompany_SelectedIndexChanged(sender, e);
        }
    }
    
    private void LoadBankName()
    {
        DataTable dt = new DataTable();
        string qry = "select bnkname+':'+brName as bnkname,Brc_Code from [emp_bank_view] order by bnkname ";
        dt = DataProcess.GetData(Session[GlobalData.sessionConnectionstring].ToString(), qry);
        ddlBank.Items.Clear();
        foreach (DataRow dr in dt.Rows)
        {
            ListItem lst = new ListItem();
            lst.Value = dr["Brc_Code"].ToString();
            lst.Text = dr["bnkname"].ToString();
            ddlBank.Items.Add(lst);
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
        LoadBankName();
        LoadofficeLocation();
    }

    private void LoadofficeLocation()
    {
        DataTable dt = new DataTable();
        dt = DataProcess.GetData(Session[GlobalData.sessionConnectionstring].ToString(), "select distinct trans_det_divID from hrms_trans_det");
        ddlofficeLocation.Items.Clear();
        foreach (DataRow dr in dt.Rows)
        {
            ddlofficeLocation.Items.Add(dr["trans_det_divID"].ToString());
        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        //DateTime sttime = DateTime.Now;

        //hrms_salary1TableAdapter sal1 = new hrms_salary1TableAdapter();
        //hrms_salaryTableAdapter sal = new hrms_salaryTableAdapter();
        //hrms_emp_masTableAdapter empmas = new hrms_emp_masTableAdapter();
        //DsSalary.hrms_emp_masDataTable dtempmas = new DsSalary.hrms_emp_masDataTable();
        //hrms_grade_detTableAdapter grade = new hrms_grade_detTableAdapter();
        //DsSalary.hrms_grade_detDataTable dtgrade = new DsSalary.hrms_grade_detDataTable();
        //hrms_emp_for_detTableAdapter fordet = new hrms_emp_for_detTableAdapter();
        //DsSalary.hrms_emp_for_detDataTable dtfordet = new DsSalary.hrms_emp_for_detDataTable();
        //SYM_SAL_POSTTableAdapter spost = new SYM_SAL_POSTTableAdapter();

        //int mm = 0, yyyy = 0;
        //string[] tmp = TextBox1.Text.Split('/');
        //if (tmp.Length < 2) return;
        //string dbname = ddlcompany.SelectedItem.Value.ToString();
        //try
        //{
        //    mm = Convert.ToInt32(tmp[0]);
        //    yyyy = Convert.ToInt32(tmp[1]);
        //}
        //catch
        //{
        //    lblmsg.Text = "Period Selection Error";
        //    lblmsg.Visible = true;
        //    return;
        //}
        //if (ddlcompany.Text == "")
        //{
        //    lblmsg.Text = "Company Selection Error";
        //    lblmsg.Visible = true;
        //    return;
        //}
        //try
        //{
        //    if (spost.GetDataForPost(dbname, mm, yyyy).Count > 0)
        //    {
        //        lblmsg.Text = "Salary Journal Already Created";
        //        lblmsg.Visible = true;
        //        return;
        //    }
        //    //dtlastsal = Convert.ToDateTime(sal.GetLastSalMonth());
        //    //if (dtlastsal.Year > yyyy)
        //    //{
        //    //    lblmsg.Text = "Salary Already Posted";
        //    //    lblmsg.Visible = true;
        //    //    return;
        //    //}
        //    //else if (dtlastsal.Year == yyyy)
        //    //{
        //    //    if (dtlastsal.Month >= mm)
        //    //    {
        //    //        lblmsg.Text = "Salary Already Posted";
        //    //        lblmsg.Visible = true;
        //    //        return;
        //    //    }
        //    //}
        //}
        //catch
        //{
        //    return;
        //}
        ////SqlConnection con = new SqlConnection(constr);
        ////sal1.Connection = con;
        ////grade.Connection = con;
        ////empmas.Connection = con;
        ////fordet.Connection = con;
        ////sal.Connection = con;
        ////Previous
        ////DateTime saldate = Convert.ToDateTime("01/01/2000");
        ////saldate = saldate.AddYears(yyyy - 2000);
        ////saldate = saldate.AddMonths(mm);
        ////saldate = saldate.AddDays(-1);
        ////DateTime findate;
        ////int totdays = saldate.Day;
        ////
        ////present
        //DateTime saldate = Convert.ToDateTime("01/01/2000");
        //saldate = saldate.AddYears(yyyy - 2000);
        //saldate = saldate.AddMonths(mm);
        //saldate = saldate.AddDays(-1);
        //DateTime findate;
        //DateTime stdate, enddate;
        //findate = Convert.ToDateTime("01/" + mm.ToString() + "/" + yyyy.ToString()).AddMonths(1).AddSeconds(-1);
        //int m = 0;
        //int y = 0;
        //if (mm == 1)
        //{
        //    m = 12;
        //    y = yyyy - 1;
        //}
        //else
        //{
        //    m = mm - 1;
        //    y = yyyy;
        //}
        //stdate = Convert.ToDateTime("26/" + m.ToString() + "/" + y.ToString());
        //enddate = Convert.ToDateTime("25/" + mm.ToString() + "/" + yyyy.ToString());
        //int totdays = GetTotalDays(stdate, enddate);
        ////for february first time
        ////stdate = Convert.ToDateTime("01/" + mm.ToString() + "/" + yyyy.ToString());
        ////enddate = Convert.ToDateTime("25/" + mm.ToString() + "/" + yyyy.ToString());
        ////int totdays = GetTotalDays(stdate, enddate) + 3;
        ////
        //int paydays;
        //bool flg, trnflag = true;
        //decimal stdval = 0;
        //decimal STDBAS = 0;
        //decimal STDMA = 0;
        //decimal DEDRAT = 0;
        //decimal STDENT = 0;
        //decimal STDOTH = 0;
        //decimal STDCON = 0;
        //decimal STDHRA = 0;
        //decimal Deducs = 0;
        //decimal NET = 0;
        //decimal PFEC = 0;
        //decimal earns = 0;
        //decimal ADVAM1 = 0;
        //string salflg = "";
        //decimal Bonus = 0;
        //SqlTransaction myTrans = HelperTA.OpenTransaction(sal1.Connection);
        //sal1.AttachTransaction(myTrans);
        //try
        //{
        //    sal1.DeleteAllSalary1();
        //    //dtempmas = empmas.GetData(findate, mm, yyyy, saldate);

        //    dtempmas = empmas.GetDataBySalaryCycle(enddate, stdate, saldate);
        //    foreach (DsSalary.hrms_emp_masRow dr in dtempmas.Rows)
        //    {
        //        STDBAS = 0;
        //        STDMA = 0;
        //        STDENT = 0;
        //        STDOTH = 0;
        //        STDCON = 0;
        //        STDHRA = 0;
        //        Deducs = 0;
        //        NET = 0;
        //        ADVAM1 = 0;
        //        Bonus = 0;
        //        if (dr.IspaydayNull())
        //        {
        //            paydays = dr.PrsDys + (int)dr.LevDys + dr.Holydays;
        //            //paydays = dr.PrsDys + (int)dr.LevDys + dr.Holydays+3; // For february 
        //        }
        //        else
        //        {
        //            paydays = (int)dr.payday;
        //        }
        //        DEDRAT = Convert.ToDecimal((decimal)paydays / (decimal)totdays);
        //        dtfordet = new DsSalary.hrms_emp_for_detDataTable();
        //        dtfordet = fordet.GetDataByEmp(dr.Emp_Mas_Emp_Id);
        //        dtgrade = new DsSalary.hrms_grade_detDataTable();
        //        dtgrade = grade.GetDataByGrade(dr.det_grade);
        //        foreach (DsSalary.hrms_grade_detRow grdr in dtgrade.Rows)
        //        {
        //            flg = false;
        //            salflg = "";
        //            stdval = 0;
        //            foreach (DsSalary.hrms_emp_for_detRow fordr in dtfordet.Rows)
        //            {
        //                if (grdr.Det_For.Trim() == fordr.For_Det_ForCode.Trim())
        //                {
        //                    stdval = Convert.ToDecimal(fordr.For_Det_Value.ToString("N2"));
        //                    flg = true;
        //                    goto nextcode;
        //                }
        //            }
        //        nextcode:
        //            if (flg)
        //            {
        //                if (grdr.Det_For == "STDBAS")
        //                {
        //                    STDBAS = stdval;
        //                    //stdval = Convert.ToDecimal((stdval * DEDRAT).ToString("N2"));
        //                }
        //                if (grdr.Det_For == "STDCON")
        //                {
        //                    STDCON = stdval;
        //                    //stdval = Convert.ToDecimal((stdval * DEDRAT).ToString("N2"));
        //                }

        //                if (grdr.Det_For == "STDENT")
        //                {
        //                    STDENT = stdval;
        //                    //stdval = Convert.ToDecimal((stdval * DEDRAT).ToString("N2"));
        //                }
        //                if (grdr.Det_For == "STDHRA")
        //                {
        //                    STDHRA = stdval;
        //                    //stdval = Convert.ToDecimal((stdval * DEDRAT).ToString("N2"));
        //                }
        //                if (grdr.Det_For == "STDMA")
        //                {
        //                    STDMA = stdval;
        //                    //stdval = Convert.ToDecimal((stdval * DEDRAT).ToString("N2"));
        //                }
        //                if (grdr.Det_For == "STDOTH")
        //                {
        //                    STDOTH = stdval;
        //                    //stdval = Convert.ToDecimal((stdval * DEDRAT).ToString("N2"));
        //                }

        //                if (grdr.Det_For == "PFEC")
        //                {
        //                    NET = NET + stdval;
        //                    Deducs += stdval * 2;
        //                    //stdval = Convert.ToDecimal((stdval * DEDRAT).ToString("N2"));
        //                }
        //                if (grdr.Det_For == "PFEMP")
        //                {
        //                    // PFEMP = stdval;                                                      
        //                }
        //                if (grdr.Det_For == "PFEMPR")
        //                {
        //                    // PFEMPR = stdval;                                                      
        //                }
        //                if (grdr.Det_For == "PFEMPE")
        //                {
        //                    // PFEMPE = stdval;                                                      
        //                }
        //                if (grdr.Det_For == "INCAMT")
        //                {
        //                    Deducs += stdval;
        //                    //stdval = Convert.ToDecimal((stdval * DEDRAT).ToString("N2"));
        //                }
        //                if (grdr.Det_For == "WFSUB")
        //                {
        //                    Deducs += stdval;
        //                    //stdval = Convert.ToDecimal((stdval * DEDRAT).ToString("N2"));
        //                }
        //                if (grdr.Det_For == "MOB_DE")
        //                {
        //                    Deducs += stdval;
        //                }
        //                if (grdr.Det_For == "AREAR")
        //                {
        //                    NET += stdval;
        //                }
        //                if (grdr.Det_For == "AdvAmt")
        //                {
        //                    Deducs += stdval;
        //                }
        //                if (grdr.Det_For == "ADVAM1")
        //                {
        //                    ADVAM1 = stdval;
        //                    Deducs += stdval;
        //                }
        //                if (grdr.Det_For == "REVSTA")
        //                {
        //                    Deducs += stdval;
        //                }
        //                if (grdr.Det_For == "Bonus")
        //                {
        //                    NET += stdval;
        //                }
        //                if (grdr.Det_For == "TA")
        //                {
        //                    NET += stdval;
        //                }
        //                if (grdr.Det_For == "DA")
        //                {
        //                    NET += stdval;
        //                }
        //                if (grdr.Det_For == "FDAL")
        //                {
        //                    stdval = stdval * dr.PrsDys;
        //                    NET += stdval;
        //                }
        //                sal1.InsertSalary1(dr.Emp_Mas_Emp_Id, Convert.ToDateTime(saldate.ToShortDateString()), grdr.Det_For, Convert.ToDecimal(stdval.ToString("N2")), salflg, "", dr.det_grade, "P");
        //            }
        //            else
        //            {
        //                switch (grdr.Det_For)
        //                {
        //                    case "BASIC"://
        //                        {
        //                            stdval = Convert.ToDecimal((STDBAS * DEDRAT).ToString("N2"));
        //                            NET = NET + stdval;
        //                            break;
        //                        }
        //                    case "HRA"://
        //                        {
        //                            stdval = Convert.ToDecimal((STDHRA * DEDRAT).ToString("N2"));
        //                            NET = NET + stdval;
        //                            break;
        //                        }
        //                    case "MEDALL"://
        //                        {
        //                            stdval = Convert.ToDecimal((STDMA * DEDRAT).ToString("N2"));
        //                            NET = NET + stdval;
        //                            break;
        //                        }
        //                    case "CONVEY"://
        //                        {
        //                            stdval = Convert.ToDecimal((STDCON * DEDRAT).ToString("N2"));
        //                            NET = NET + stdval;
        //                            break;
        //                        }
        //                    case "ENTALL"://
        //                        {
        //                            stdval = Convert.ToDecimal((STDENT * DEDRAT).ToString("N2"));
        //                            NET = NET + stdval;
        //                            break;
        //                        }
        //                    case "OTHALL"://
        //                        {
        //                            stdval = Convert.ToDecimal((STDOTH * DEDRAT).ToString("N2"));
        //                            NET = NET + stdval;
        //                            break;
        //                        }

        //                    case "PFEC"://
        //                        {
        //                            PFEC = Convert.ToDecimal((STDBAS * Convert.ToDecimal(.10)).ToString("N2"));
        //                            stdval = PFEC;
        //                            NET = NET + stdval;
        //                            Deducs += stdval * 2;
        //                            break;
        //                        }
        //                    case "PFEMP"://
        //                    case "PFEMPR"://
        //                    case "PFEMPE"://
        //                        {
        //                            stdval = Convert.ToDecimal((STDBAS * Convert.ToDecimal(.10)).ToString("N2"));
        //                            break;
        //                        }
        //                    case "REVSTA"://
        //                        {
        //                            stdval = Convert.ToDecimal(grdr.Det_Base);
        //                            Deducs += stdval;
        //                            break;
        //                        }
        //                    case "AdvAmt"://
        //                        {
        //                            stdval = dr.AdvAmt;
        //                            Deducs += stdval;
        //                            break;
        //                        }
        //                    case "ADVAM1"://
        //                        {
        //                            stdval = 0;
        //                            break;
        //                        }
        //                    case "MOB_DE"://
        //                    case "AREAR"://
        //                    case "HctDys"://
        //                    case "INCAMT"://
        //                    case "TA"://
        //                    case "DA"://
        //                    case "FDAL"://
        //                        {
        //                            //Deducs += stdval;
        //                            stdval = 0;
        //                            break;
        //                        }
        //                    case "NETADV"://
        //                        {
        //                            stdval = dr.AdvAmt + ADVAM1;
        //                            break;
        //                        }
        //                    case "AtdDys"://
        //                        {
        //                            stdval = dr.PrsDys;
        //                            break;
        //                        }
        //                    case "DEDRAT"://
        //                        {
        //                            stdval = Convert.ToDecimal(DEDRAT.ToString("N2"));
        //                            break;
        //                        }
        //                    case "HolDys"://
        //                        {
        //                            stdval = dr.Holydays;
        //                            break;
        //                        }
        //                    case "LevDys"://
        //                        {
        //                            stdval = (decimal)dr.LevDys;
        //                            break;
        //                        }
        //                    case "PAYDAY"://
        //                        {
        //                            stdval = paydays;
        //                            break;
        //                        }
        //                    case "PrsDys"://
        //                        {
        //                            stdval = dr.PrsDys;
        //                            break;
        //                        }
        //                    case "PYDAY1"://
        //                        {
        //                            stdval = 0;
        //                            break;
        //                        }
        //                    case "SAL"://
        //                        {
        //                            stdval = 0;
        //                            break;
        //                        }
        //                    case "TODAY1"://
        //                        {
        //                            stdval = dr.PrsDys + (decimal)dr.LevDys;
        //                            break;
        //                        }
        //                    case "TODAY2"://
        //                        {
        //                            stdval = dr.PrsDys + (decimal)dr.LevDys + dr.Holydays;
        //                            break;
        //                        }
        //                    case "TotDys"://
        //                        {
        //                            stdval = totdays;
        //                            break;
        //                        }
        //                    case "WFSUB"://
        //                        {
        //                            stdval = Convert.ToDecimal(grdr.Det_Base);
        //                            Deducs += stdval;
        //                            break;
        //                        }
        //                    default:
        //                        {
        //                            stdval = -1;
        //                            break;
        //                        }
        //                }
        //                if (stdval != -1)
        //                {
        //                    sal1.InsertSalary1(dr.Emp_Mas_Emp_Id, Convert.ToDateTime(saldate.ToShortDateString()), grdr.Det_For, Convert.ToDecimal(stdval.ToString("N2")), salflg, "", grdr.Det_Code, "P");
        //                }
        //            }
        //        }
        //        NET = NET - Deducs;
        //        earns = NET + Deducs;
        //        sal1.InsertSalary1(dr.Emp_Mas_Emp_Id, Convert.ToDateTime(saldate.ToShortDateString()), "NET", NET, "N", "", dtgrade[0].Det_Code, "P");
        //        sal1.InsertSalary1(dr.Emp_Mas_Emp_Id, Convert.ToDateTime(saldate.ToShortDateString()), "Deducs", Deducs, "D", "", dtgrade[0].Det_Code, "P");
        //        //erns
        //        sal1.InsertSalary1(dr.Emp_Mas_Emp_Id, Convert.ToDateTime(saldate.ToShortDateString()), "Earns", earns, "P", "", dtgrade[0].Det_Code, "P");
        //    }
        //    myTrans.Commit();
        //    //myTrans.Rollback();
        //    LoadSalary();
        //}
        //catch (Exception ex)
        //{
        //    myTrans.Rollback();
        //    trnflag = false;
        //}
        //finally
        //{
        //    HelperTA.CloseTransaction(sal1.Connection, myTrans);
        //}
        //DateTime endtime = DateTime.Now;
        //TimeSpan ts = endtime - sttime;
        //lblmsg.Visible = true;
        //if (trnflag)
        //{
        //    lblmsg.Text = (ts.Minutes * 60 + ts.Seconds).ToString() + " Seconds";
        //}
        //else
        //{
        //    lblmsg.Text = "Transaction Error, Please Try Again";
        //}
    }

    private int GetTotalDays(DateTime dtst, DateTime dtend)
    {
        int totdays = 0;
        DateTime dt1 = Convert.ToDateTime(dtst);
        DateTime dt2 = Convert.ToDateTime(dtend);
        TimeSpan ts = dt2 - dt1;
        totdays = ts.Days + 1;
        return totdays;
    }

    protected void btnpost_Click(object sender, EventArgs e)
    {
        //DateTime sttime = DateTime.Now;
        //hrms_salaryTableAdapter sal = new hrms_salaryTableAdapter();
        //SqlTransaction myTrans = HelperTA.OpenTransaction(sal.Connection);
        //bool flg = true;
        //hrms_salary1TableAdapter sal1 = new hrms_salary1TableAdapter();
        //DsSalary.hrms_salary1Row drsal;
        //SYM_SAL_POSTTableAdapter spost = new SYM_SAL_POSTTableAdapter();
        //string dbname = ddlcompany.SelectedItem.Value.ToString();
        //if (sal1.GetData().Count == 0) return;
        //drsal = sal1.GetData()[0];
        //if (spost.GetDataForPost(dbname, drsal.Salmonth.Month, drsal.Salmonth.Year).Count > 0)
        //{
        //    lblmsg.Text = "Salary Journal Already Created";
        //    lblmsg.Visible = true;
        //    return;
        //}
        //try
        //{
        //    sal.AttachTransaction(myTrans);
        //    sal.DeletePostedSalary(drsal.Salmonth.Month, drsal.Salmonth.Year);
        //    sal.salarypost();
        //    myTrans.Commit();
        //}
        //catch (Exception ex)
        //{
        //    myTrans.Rollback();
        //    flg = false;
        //}
        //finally
        //{
        //    HelperTA.CloseTransaction(sal.Connection, myTrans);
        //}
        //DateTime endtime = DateTime.Now;
        //TimeSpan ts = endtime - sttime;
        //lblmsg.Visible = true;
        //if (flg)
        //{
        //    lblmsg.Text = (ts.Minutes * 60 + ts.Seconds).ToString() + " Seconds";
        //}
        //else
        //{
        //    lblmsg.Text = "Transaction Error, Please Try Again";
        //}
    }

    protected void btnjv_Click(object sender, EventArgs e)
    {
        //DateTime sttime = DateTime.Now;
        //Hrms_departmentTableAdapter dptmas = new Hrms_departmentTableAdapter();
        //DsSalary.Hrms_departmentDataTable dtdptmas = new DsSalary.Hrms_departmentDataTable();
        ////hrmsPayrollGLTableAdapter dptppayrollgl = new hrmsPayrollGLTableAdapter();
        ////DsSalary.hrmsPayrollGLDataTable dtdptppayrollgl = new DsSalary.hrmsPayrollGLDataTable(); 


        //hrms_dept_journalTableAdapter dptjour = new hrms_dept_journalTableAdapter();
        //DsSalary.hrms_dept_journalDataTable dtdptjour;
        //hrms_employee_journalTableAdapter emp = new hrms_employee_journalTableAdapter();
        //DsSalary.hrms_employee_journalDataTable dtemp;
        //FA_TE_WDTableAdapter wd = new FA_TE_WDTableAdapter();
        //FA_TE_WHTableAdapter wh = new FA_TE_WHTableAdapter();
        //hrms_salary1TableAdapter sal1 = new hrms_salary1TableAdapter();
        //DsSalary.hrms_salary1Row drsal;
        //if (sal1.GetData().Count == 0) return;
        //drsal = sal1.GetData()[0];
        //string period = string.Format("{0:00}", drsal.Salmonth.Month) + "/" + drsal.Salmonth.Year.ToString();
        //int maxref = Convert.ToInt32(wh.GetMaxRef(period));
        //string jvref = "";
        //string pref = "SJV" + drsal.Salmonth.Year.ToString().Substring(2, 2) + drsal.Salmonth.ToString("MMM").ToUpper();
        //string narr = "";
        //int seq = 0;
        //decimal totc, totd;
        //bool flg = true;
        //SYM_SAL_POSTTableAdapter spost = new SYM_SAL_POSTTableAdapter();
        //string dbname = ddlcompany.SelectedItem.Value.ToString();
        //if (spost.GetDataForPost(dbname, drsal.Salmonth.Month, drsal.Salmonth.Year).Count > 0)
        //{
        //    lblmsg.Text = "Salary Journal Already Created";
        //    lblmsg.Visible = true;
        //    return;
        //}
        //SqlTransaction myTrans = HelperTA.OpenTransaction(sal1.Connection);
        //try
        //{
        //    wh.AttachTransaction(myTrans);
        //    wd.AttachTransaction(myTrans);
        //    dtdptmas = dptmas.GetData();
        //    foreach (DsSalary.Hrms_departmentRow dr in dtdptmas.Rows)
        //    {
        //        seq = 0;
        //        jvref = pref + string.Format("{0:000000}", maxref);
        //        narr = "Salary of " + dr.Dept_Name + " for the month of " + period;
        //        wh.InsertWH("ADM", jvref, "JV", drsal.Salmonth, DateTime.Now, period, "L", "Y", "Salary", "J", 0);

        //        //dtdptjour = new DsSalary.hrms_dept_journalDataTable();
        //        //dtdptjour = dptjour.GetData(dr.Dept_Division_Code, dr.Dept_Code);
        //        //foreach (DsSalary.hrms_dept_journalRow drj in dtdptjour.Rows)
        //        //{
        //        //    wd.InsertWD(jvref, (double)drj.Led_Det_SeqNo, (drj.Led_Det_DCFlag == "D" ? dr.PayRoll_GL_Acc : drj.Led_Det_AccCode), narr, drj.Led_Det_DCFlag, drj.CalVal, jvref, 0, "", 0, 0, "", (drj.Led_Det_DCFlag == "D" ? dr.Dept_Code : drj.Led_Det_Grpt1), drj.Led_Det_Grpt2, "", "", "", drj.Led_Det_Grpt6, drj.Led_Det_Grpt7, "", "", "", "", "", Convert.ToDateTime(DateTime.Now.ToShortDateString()), "", "", "", "", "", "", "", seq);
        //        //    seq++;
        //        //}
        //        //Earns-ta-da-fdal

        //        dtdptjour = new DsSalary.hrms_dept_journalDataTable();
        //        dtdptjour = dptjour.GetEarnsDataByDivdept(dr.Dept_Division_Code, dr.Dept_Code);
        //        foreach (DsSalary.hrms_dept_journalRow drj in dtdptjour.Rows)
        //        {
        //            wd.InsertWD(jvref, (double)drj.Led_Det_SeqNo, (drj.Led_Det_DCFlag == "D" ? dr.PayRoll_GL_Acc : drj.Led_Det_AccCode), narr, drj.Led_Det_DCFlag, drj.CalVal, jvref, 0, "", 0, 0, "", (drj.Led_Det_DCFlag == "D" ? dr.Dept_Code : drj.Led_Det_Grpt1), drj.Led_Det_Grpt2, "", "", "", drj.Led_Det_Grpt6, drj.Led_Det_Grpt7, "", "", "", "", "", Convert.ToDateTime(DateTime.Now.ToShortDateString()), "", "", "", "", "", "", "", seq);
        //            seq++;
        //        }
        //        //end
        //        //TADA 
        //        dtdptjour = new DsSalary.hrms_dept_journalDataTable();
        //        dtdptjour = dptjour.GetDataTADAByDivDept(dr.Dept_Division_Code, dr.Dept_Code);
        //        foreach (DsSalary.hrms_dept_journalRow drj in dtdptjour.Rows)
        //        {
        //            if (drj.CalVal > 0)
        //            {
        //                wd.InsertWD(jvref, (double)drj.Led_Det_SeqNo, dr.PayRoll_GL_TADA, narr, drj.Led_Det_DCFlag, drj.CalVal, jvref, 0, "", 0, 0, "", (drj.Led_Det_DCFlag == "D" ? dr.Dept_Code : drj.Led_Det_Grpt1), drj.Led_Det_Grpt2, "", "", "", drj.Led_Det_Grpt6, drj.Led_Det_Grpt7, "", "", "", "", "", Convert.ToDateTime(DateTime.Now.ToShortDateString()), "", "", "", "", "", "", "", seq);
        //                seq++;
        //            }
        //        }
        //        //FDAL 
        //        dtdptjour = new DsSalary.hrms_dept_journalDataTable();
        //        dtdptjour = dptjour.GetDataFDALByDivdept(dr.Dept_Division_Code, dr.Dept_Code);
        //        foreach (DsSalary.hrms_dept_journalRow drj in dtdptjour.Rows)
        //        {
        //            if (drj.CalVal > 0)
        //            {
        //                wd.InsertWD(jvref, (double)drj.Led_Det_SeqNo, dr.PayRoll_GL_FDAL, narr, drj.Led_Det_DCFlag, drj.CalVal, jvref, 0, "", 0, 0, "", (drj.Led_Det_DCFlag == "D" ? dr.Dept_Code : drj.Led_Det_Grpt1), drj.Led_Det_Grpt2, "", "", "", drj.Led_Det_Grpt6, drj.Led_Det_Grpt7, "", "", "", "", "", Convert.ToDateTime(DateTime.Now.ToShortDateString()), "", "", "", "", "", "", "", seq);
        //                seq++;
        //            }
        //        }
        //        //Net, REVSTA, WFSUB 
        //        dtdptjour = new DsSalary.hrms_dept_journalDataTable();
        //        dtdptjour = dptjour.GetOthersDataByDivDept(dr.Dept_Division_Code, dr.Dept_Code);
        //        foreach (DsSalary.hrms_dept_journalRow drj in dtdptjour.Rows)
        //        {
        //            wd.InsertWD(jvref, (double)drj.Led_Det_SeqNo, (drj.Led_Det_DCFlag == "D" ? dr.PayRoll_GL_Acc : drj.Led_Det_AccCode), narr, drj.Led_Det_DCFlag, drj.CalVal, jvref, 0, "", 0, 0, "", (drj.Led_Det_DCFlag == "D" ? dr.Dept_Code : drj.Led_Det_Grpt1), drj.Led_Det_Grpt2, "", "", "", drj.Led_Det_Grpt6, drj.Led_Det_Grpt7, "", "", "", "", "", Convert.ToDateTime(DateTime.Now.ToShortDateString()), "", "", "", "", "", "", "", seq);
        //            seq++;
        //        }
        //        //deduction employee wise   
        //        seq = 0;
        //        dtemp = new DsSalary.hrms_employee_journalDataTable();
        //        dtemp = emp.GetDataByDivDept(dr.Dept_Division_Code, dr.Dept_Code);
        //        foreach (DsSalary.hrms_employee_journalRow drj in dtemp.Rows)
        //        {
        //            wd.InsertWD(jvref, (double)drj.Led_Det_SeqNo, drj.Led_Det_AccCode, narr, drj.Led_Det_DCFlag, drj.CalVal, jvref, 0, "", 0, 0, "", drj.Trans_Det_Emp_Id, "", "", "", "", "T01", "", "", "", "", "", "", Convert.ToDateTime(DateTime.Now.ToShortDateString()), "", "", "", "", "", "", "", seq);
        //            seq++;
        //        }
        //        totc = Convert.ToDecimal(wd.GetTotAmount("C", jvref));
        //        totd = Convert.ToDecimal(wd.GetTotAmount("D", jvref));
        //        if (totc != totd)
        //        {
        //            flg = false;
        //            goto eot;
        //        }
        //        maxref++;
        //    }
        //    eot:
        //    if (flg)
        //    {
        //        //myTrans.Rollback();
        //        myTrans.Commit();
        //        spost.InsertJv(dbname, drsal.Salmonth.Month, drsal.Salmonth.Year, DateTime.Now);
        //    }
        //    else
        //    {
        //        myTrans.Rollback();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    myTrans.Rollback();
        //    flg = false;
        //}
        //finally
        //{
        //    HelperTA.CloseTransaction(wh.Connection, myTrans);
        //}
        //DateTime endtime = DateTime.Now;
        //TimeSpan ts = endtime - sttime;
        //lblmsg.Visible = true;
        //if (flg)
        //{
        //    lblmsg.Text = (ts.Minutes * 60 + ts.Seconds).ToString() + " Seconds";
        //}
        //else
        //{
        //    lblmsg.Text = "Transaction Error, Please Try Again";
        //}
    }

    protected void btnremove_Click(object sender, EventArgs e)
    {
        //DateTime sttime = DateTime.Now;
        //hrms_emp_for_detTableAdapter fordet = new hrms_emp_for_detTableAdapter();
        //fordet.DeleteForCode("ADVAM1", "PAYDAY", "MOB_DE", "AREAR");
        //DateTime endtime = DateTime.Now;
        //TimeSpan ts = endtime - sttime;
        //lblmsg.Visible = true;
        //lblmsg.Text = (ts.Minutes * 60 + ts.Seconds).ToString() + " Seconds";
    }

    protected void btnInclude_Click(object sender, EventArgs e)
    {
        //lblmsgPf.Text = "";
        //if (txtEmployeeID.Text.Trim() == "")
        //{
        //    lblmsgPf.Text = "Please enter employee ID";
        //    return;
        //}
        //hrms_emp_for_detTableAdapter fordet = new hrms_emp_for_detTableAdapter();
        //fordet.DeletePFzeroRecord(txtEmployeeID.Text.Trim());
        //txtEmployeeID.Text = "";
        //lblmsgPf.Visible = true;
        //lblmsgPf.Text = txtEmployeeID.Text.Trim() + " " + "This employee has included in PF";
    }

    //private void LoadSalary()
    //{
    //    DataTable dt = new DataTable();
    //    gdvView.DataSource = null;
    //    double HO, FO;
    //    HO = 0;
    //    FO = 0;
    //    salaryTotalTableAdapter sal = new salaryTotalTableAdapter();
    //    dt = sal.GetDatabyDeptDiv();
    //    gdvView.DataSource = dt;
    //    gdvView.DataBind();
    //    foreach (DataRow dtr in dt.Rows)
    //    {
    //        if (dtr[0].ToString() == "HO")
    //        {
    //            HO = HO + Convert.ToDouble(dtr[2].ToString());
    //        }
    //        else
    //        {
    //            FO = FO + Convert.ToDouble(dtr[2].ToString());
    //        }
    //    }
    //    lblmsgSal.Text = "";
    //    lblmsgSal.Text = "HO Grand Total Salary: " + Convert.ToString(HO) + "     FO Grand Total Salary: " + Convert.ToString(FO);
    //}

    protected void btnSalreport_Click(object sender, EventArgs e)
    {
        //string selectionfor, parameter;
        //int mth = Convert.ToInt32(TextBox1.Text.Split('/')[0].ToString());
        //int yr = Convert.ToInt32(TextBox1.Text.Split('/')[1].ToString());
        //string salmonth = TextBox1.Text;
        //System.Globalization.DateTimeFormatInfo mfi = new
        //System.Globalization.DateTimeFormatInfo();
        //string strMonthName = mfi.GetMonthName(mth).ToString();
        //salmonth = strMonthName + "/" + TextBox1.Text.Split('/')[1].ToString();
        //selectionfor = "{Hrms_Trans_Det.Trans_Det_DivID}='" + ddlofficeLocation.SelectedItem.Text + "'";
        //string CompanyName = "CompanyName" + ":" + Session["CompanyName"].ToString().Split(':')[1].ToString();
        //string CompanyAddress = "CompanyAddress" + ":" + Session["CompanyAddress"].ToString();
        //salmonth = "salmonth" + ":" + salmonth;
        //parameter = salmonth + ";" + CompanyName + ";" + CompanyAddress;
        //string reportname = "./Report/HrmsEmpSal1.rpt";
        //ShowReport(selectionfor, parameter, reportname);
    }

    private void parameterpass(ParameterFields myParams, string pname, string value)
    {
        //ParameterField param = new ParameterField();
        //ParameterDiscreteValue dis1 = new ParameterDiscreteValue();
        //param.ParameterFieldName = pname;
        //dis1.Value = value;
        //param.CurrentValues.Add(dis1);
        //myParams.Add(param);
    }

    protected void btnManpowerList_Click(object sender, EventArgs e)
    {
        //string selectionfor, parameter;
        //int mth = Convert.ToInt32(TextBox1.Text.Split('/')[0].ToString());
        //int yr = Convert.ToInt32(TextBox1.Text.Split('/')[1].ToString());
        //selectionfor = "";
        //string CompanyName = "CompanyName" + ":" + Session["CompanyName"].ToString().Split(':')[1].ToString();
        //string CompanyAddress = "CompanyAddress" + ":" + Session["CompanyAddress"].ToString();
        //parameter = CompanyName + ";" + CompanyAddress;
        //string reportname = "./Report/ManpowerList.rpt";
        //ShowReport(selectionfor, parameter, reportname);
    }

    protected void btnBankAdvice_Click(object sender, EventArgs e)
    {
        //string selectionfor, parameter;
        //int mth = Convert.ToInt32(TextBox1.Text.Split('/')[0].ToString());
        //int yr = Convert.ToInt32(TextBox1.Text.Split('/')[1].ToString());
        //DataProcess.ExecuteQuery(Session[GlobalData.sessionConnectionstring].ToString(), "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[salarypostedview]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[salarypostedview]");
        //DataProcess.ExecuteQuery(Session[GlobalData.sessionConnectionstring].ToString(), "Create view salarypostedview as select * from Hrms_salary where month(salmonth)=" + mth + " and year(salmonth)=" + yr + "");
        //selectionfor = "{emp_bank_view.brc_code}='" + ddlBank.SelectedItem.Value + "' and month({hrms_salary.salMonth}) = " + mth + " and year({hrms_salary.salMonth}) = " + yr + "";
        //string chequeno = "chequeno" + ":" + txtCheqNo.Text.Trim().ToString();
        //string chequedate = "chequedate" + ":" + dtFrom.SelectedDate.ToShortDateString();
        ////string chequedate = "chequedate" + ":" + dtFrom.Text.ToString();
        //string CompanyName = "CompanyName" + ":" + Session["CompanyName"].ToString().Split(':')[1].ToString();
        //string CompanyAddress = "CompanyAddress" + ":" + Session["CompanyAddress"].ToString();
        //parameter = chequeno + ";" + chequedate + ";" + CompanyName + ";" + CompanyAddress;
        //string reportname = "./Report/hrmsamttrans.rpt";
        //ShowReport(selectionfor, parameter, reportname);
    }

    private void ShowReport(string selectionfor, string parameter, string reportname)
    {
        //clsReport rpt = new clsReport();
        //ParameterFields myParams = new ParameterFields();
        //ConnectionInfo ConnInfo = new ConnectionInfo();
        //string SCFconnStr = Session[GlobalData.sessionConnectionstring].ToString();
        //string[] ff;
        //string[] ss;
        //string[] prm;
        //prm = parameter.Split(';');
        //if (prm.Length > 0)
        //{
        //    for (int i = 0; i < prm.Length; i++)
        //    {
        //        parameterpass(myParams, prm[i].Split(':')[0].ToString(), prm[i].Split(':')[1].ToString());
        //    }
        //}
        //ff = SCFconnStr.Split('=');
        //ss = ff[1].Split(';');
        //ConnInfo.ServerName = ss[0];
        //ss = ff[2].Split(';');
        //ConnInfo.DatabaseName = ss[0];
        //ss = ff[3].Split(';');
        //ConnInfo.UserID = ss[0];
        //ss = ff[4].Split(';');
        //ConnInfo.Password = ss[0];
        //rpt.FileName = reportname;
        //rpt.ConnectionInfo = ConnInfo;
        //rpt.ParametersFields = myParams;
        //rpt.SelectionFormulla = selectionfor;
        //Session[GlobalData.sessionReportDet] = rpt;
        //RegisterStartupScript("click", "<script>window.open('./frm_report_viewer.aspx');</script>");
    }

    protected void btnEL_Click(object sender, EventArgs e)
    {
        //Elcalculationbyemployeeid();
    }

    //private void Elcalculationbyemployeeid()
    //{
    //    SqlConnection oConnection = new SqlConnection(Session[GlobalData.sessionConnectionstring].ToString());
    //    oConnection.Open();
    //    SqlCommand cmd = new SqlCommand("EarnLeaveCalculationByEmpid", oConnection);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    SqlParameter EmployeeID = cmd.Parameters.Add("@remployeeid", SqlDbType.NVarChar, 10);
    //    EmployeeID.Value = txtempid.Text.Trim().ToString();
    //    SqlParameter trndate = cmd.Parameters.Add("@rdateto", SqlDbType.NVarChar, 22);
    //    trndate.Value = dt2.SelectedDate.ToShortDateString();
    //    SqlParameter operatorID = cmd.Parameters.Add("@entryuser", SqlDbType.NVarChar, 10);
    //    operatorID.Value = Session[GlobalData.SessionUserId].ToString();

    //    // output parm
    //    //SqlParameter outputStr = cmd.Parameters.Add("@outputStr", SqlDbType.NVarChar, 100);
    //    //outputStr.Direction = ParameterDirection.Output;

    //    // return value
    //    SqlParameter returnnVal = cmd.Parameters.Add("returnnVal", SqlDbType.Int);
    //    returnnVal.Direction = ParameterDirection.ReturnValue;
    //    cmd.ExecuteNonQuery();
    //    string msg = "";
    //    if ((int)returnnVal.Value == 0)
    //    {
    //        //string rest = Convert.ToString(outputStr.Value);
    //        msg = "EL Calculated Successful";
    //    }
    //    else
    //    {
    //        msg = "Error...please try again";
    //    }
    //    MessageBoxShow(this, msg);
    //}

    protected void btnElpreview_Click(object sender, EventArgs e)
    {
        //string selectionfor, parameter, flag;
        //if (txtempid.Text.Trim().ToString() == "")
        //{
        //    MessageBoxShow(this, "Select Employee then try...");
        //}

        //DataTable dt = DataProcess.GetData(Session[GlobalData.sessionConnectionstring].ToString(), "select HPflag from [Hrms_Emp_EL_Encashment] where empid='" + txtempid.Text.Trim().ToString() + "' and paymentNo=(select max(paymentNo)from [Hrms_Emp_EL_Encashment] where empid='" + txtempid.Text.Trim().ToString() + "')");
        //if (dt.Rows.Count > 0)
        //{
        //    flag = dt.Rows[0]["HPflag"].ToString();
        //}
        //else
        //{
        //    MessageBoxShow(this, "No Data Found...");
        //    return;
        //}

        //selectionfor = "{EmpELEncashmentbyid.EmpID}='" + txtempid.Text.Trim().ToString() + "' ";

        //string CompanyName = "CompanyName" + ":" + Session["CompanyName"].ToString().Split(':')[1].ToString();
        //string CompanyAddress = "CompanyAddress" + ":" + Session["CompanyAddress"].ToString();
        //flag = "flag" + ":" + flag.ToString();

        //parameter = CompanyName + ";" + CompanyAddress + ";" + flag;

        //string reportname = "./Report/ELencashmentByemp.rpt";

        //ShowReport(selectionfor, parameter, reportname);
    }

    protected void btnElpost_Click(object sender, EventArgs e)
    {
        //if (txtempid.Text.Trim().ToString() == "")
        //{
        //    return;
        //}
        //SqlConnection oConnection = new SqlConnection(Session[GlobalData.sessionConnectionstring].ToString());
        //oConnection.Open();
        //SqlCommand cmd = new SqlCommand("[EarnLeavePaymentPostByempid]", oConnection);
        //cmd.CommandType = CommandType.StoredProcedure;
        //SqlParameter EmployeeID = cmd.Parameters.Add("@rEmpid", SqlDbType.NVarChar, 10);
        //EmployeeID.Value = txtempid.Text.Trim().ToString();

        //// return value
        //SqlParameter returnnVal = cmd.Parameters.Add("returnnVal", SqlDbType.Int);
        //returnnVal.Direction = ParameterDirection.ReturnValue;
        //cmd.ExecuteNonQuery();
        //string msg = "";
        //if ((int)returnnVal.Value == 0)
        //{
        //    msg = "Data posted Successful";
        //    txtempid.Text = "";
        //}
        //else
        //{
        //    msg = "Error...please try again";
        //}
        //MessageBoxShow(this, msg);
    }

    private void MessageBoxShow(Page page, string message)
    {
        Literal ltr = new Literal();
        ltr.Text = @"<script type='text/javascript'> alert('" + message + "') </script>";
        page.Controls.Add(ltr);
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {

    }

    protected void btndeptsal_Click(object sender, EventArgs e)
    {
        //string selectionfor, parameter;
        //int mth = Convert.ToInt32(TextBox1.Text.Split('/')[0].ToString());
        //int yr = Convert.ToInt32(TextBox1.Text.Split('/')[1].ToString());
        //string salmonth = TextBox1.Text;
        //System.Globalization.DateTimeFormatInfo mfi = new
        //System.Globalization.DateTimeFormatInfo();
        //string strMonthName = mfi.GetMonthName(mth).ToString();
        //salmonth = strMonthName + "/" + TextBox1.Text.Split('/')[1].ToString();
        //DataProcess.ExecuteQuery(Session[GlobalData.sessionConnectionstring].ToString(), "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[salarypostedview]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[salarypostedview]");
        //DataProcess.ExecuteQuery(Session[GlobalData.sessionConnectionstring].ToString(), "Create view salarypostedview as select * from Hrms_salary where month(salmonth)=" + mth + " and year(salmonth)=" + yr + "");
        //selectionfor = "";
        //selectionfor = "month({hrms_salary.salMonth}) = " + mth + " and year({hrms_salary.salMonth}) = " + yr + " and {Hrms_Trans_Det.Trans_Det_DivID}='" + ddlofficeLocation.SelectedItem.Text + "'";
        //string CompanyName = "CompanyName" + ":" + Session["CompanyName"].ToString().Split(':')[1].ToString();
        //string CompanyAddress = "CompanyAddress" + ":" + Session["CompanyAddress"].ToString();
        //salmonth = "salmonth" + ":" + salmonth;
        //parameter = salmonth + ";" + CompanyName + ";" + CompanyAddress;
        //string reportname = "./Report/hrmsdeptsal.rpt";
        //ShowReport(selectionfor, parameter, reportname);
    }

    protected void btnPaySlip_Click(object sender, EventArgs e)
    {
        //string selectionfor, parameter;
        //if (txtid.Text.Trim().ToString() == "")
        //{
        //    MessageBoxShow(this, "Select Employee ID then try...");
        //    return;
        //}
        //int mth = Convert.ToInt32(TextBox1.Text.Split('/')[0].ToString());
        //int yr = Convert.ToInt32(TextBox1.Text.Split('/')[1].ToString());
        //string salmonth = TextBox1.Text;
        //System.Globalization.DateTimeFormatInfo mfi = new
        //System.Globalization.DateTimeFormatInfo();
        //string strMonthName = mfi.GetMonthName(mth).ToString();
        //salmonth = strMonthName + "/" + TextBox1.Text.Split('/')[1].ToString();
        //DataProcess.ExecuteQuery(Session[GlobalData.sessionConnectionstring].ToString(), "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[salarypostedview]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[salarypostedview]");
        //DataProcess.ExecuteQuery(Session[GlobalData.sessionConnectionstring].ToString(), "Create view salarypostedview as select * from Hrms_salary where month(salmonth)=" + mth + " and year(salmonth)=" + yr + "");
        //selectionfor = "";
        //selectionfor = "{HrMs_salary.Empcode}= '" + txtid.Text.Trim().ToString() + "' and month({hrms_salary.salMonth}) = " + mth + " and year({hrms_salary.salMonth}) = " + yr + "";
        //string CompanyName = "CompanyName" + ":" + Session["CompanyName"].ToString().Split(':')[1].ToString();
        //string CompanyAddress = "CompanyAddress" + ":" + Session["CompanyAddress"].ToString();
        //salmonth = "salmonth" + ":" + salmonth;
        //parameter = salmonth + ";" + CompanyName + ";" + CompanyAddress;
        //string reportname = "./Report/hrmspayslip.rpt";
        //ShowReport(selectionfor, parameter, reportname);
    }

    protected void btnCashAdvice_Click(object sender, EventArgs e)
    {
        //string selectionfor, parameter;
        //int mth = Convert.ToInt32(TextBox1.Text.Split('/')[0].ToString());
        //int yr = Convert.ToInt32(TextBox1.Text.Split('/')[1].ToString());
        //string salmonth = TextBox1.Text;
        //System.Globalization.DateTimeFormatInfo mfi = new
        //System.Globalization.DateTimeFormatInfo();
        //string strMonthName = mfi.GetMonthName(mth).ToString();
        //salmonth = strMonthName + "/" + TextBox1.Text.Split('/')[1].ToString();
        //DataProcess.ExecuteQuery(Session[GlobalData.sessionConnectionstring].ToString(), "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[salarypostedview]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[salarypostedview]");
        //DataProcess.ExecuteQuery(Session[GlobalData.sessionConnectionstring].ToString(), "Create view salarypostedview as select * from Hrms_salary where month(salmonth)=" + mth + " and year(salmonth)=" + yr + "");
        //selectionfor = selectionfor = "{Hrms_Trans_Det.Trans_Det_DivID}='" + ddlofficeLocation.SelectedItem.Text + "'"; ;

        ////selectionfor = " isnull({Hrms_Emp_Bnk_Info.Acc_No}) and month({hrms_salary.salMonth}) = " + mth + " and year({hrms_salary.salMonth}) = " + yr + "";

        //string CompanyName = "CompanyName" + ":" + Session["CompanyName"].ToString().Split(':')[1].ToString();
        //string CompanyAddress = "CompanyAddress" + ":" + Session["CompanyAddress"].ToString();
        //salmonth = "salmonth" + ":" + salmonth;
        //parameter = salmonth + ";" + CompanyName + ";" + CompanyAddress;
        //string reportname = "./Report/hrmsamttrans_cash.rpt";
        //ShowReport(selectionfor, parameter, reportname);
    }

    protected void btnSalreportPosted_Click(object sender, EventArgs e)
    {
        //string selectionfor, parameter;
        //int mth = Convert.ToInt32(TextBox1.Text.Split('/')[0].ToString());
        //int yr = Convert.ToInt32(TextBox1.Text.Split('/')[1].ToString());
        //string salmonth = TextBox1.Text;
        //System.Globalization.DateTimeFormatInfo mfi = new
        //System.Globalization.DateTimeFormatInfo();
        //string strMonthName = mfi.GetMonthName(mth).ToString();
        //salmonth = strMonthName + "/" + TextBox1.Text.Split('/')[1].ToString();
        //DataProcess.ExecuteQuery(Session[GlobalData.sessionConnectionstring].ToString(), "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[salarypostedview]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[salarypostedview]");
        //DataProcess.ExecuteQuery(Session[GlobalData.sessionConnectionstring].ToString(), "Create view salarypostedview as select * from Hrms_salary where month(salmonth)=" + mth + " and year(salmonth)=" + yr + "");
        //selectionfor = "{Hrms_Trans_Det.Trans_Det_DivID}='" + ddlofficeLocation.SelectedItem.Text + "'";
        //string CompanyName = "CompanyName" + ":" + Session["CompanyName"].ToString().Split(':')[1].ToString();
        //string CompanyAddress = "CompanyAddress" + ":" + Session["CompanyAddress"].ToString();
        //salmonth = "salmonth" + ":" + salmonth;
        //parameter = salmonth + ";" + CompanyName + ";" + CompanyAddress;
        //string reportname = "./Report/HrmsEmpSal.rpt";
        //ShowReport(selectionfor, parameter, reportname);
    }  
}