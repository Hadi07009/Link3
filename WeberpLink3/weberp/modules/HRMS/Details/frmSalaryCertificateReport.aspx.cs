using ADODB;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using LibraryPF.dsMasterDataTableAdapters;

public partial class modules_HRMS_Details_frmSalaryCertificateReport : System.Web.UI.Page
{
    string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    private const string Rnode = "K";
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        if (!IsPostBack)
        {
            LoadCompanyByUserPermission("ADM", Rnode);
            ClsDropDownListController.EnableDisableDropDownList(ddlcompany);
            Session["EntryUserid"] = current.UserId.Trim();
            ddlcompany_SelectedIndexChanged(sender, e);
            txtemployeeSearch_AutoComplxtender.ContextKey = Session[GlobalData.sessionConnectionstring].ToString();

        }
    }
    private void LoadCompanyByUserPermission(string userid, string nodeid)
    {
        DataTable dt = new DataTable();
        ListItem lst;
        ddlcompany.Items.Clear();
        dt = AccessPermission.GetCompanyByUserandNodeid(ConnectionString, userid, nodeid);
        if (dt.Rows.Count > 0)
        {
            ddlcompany.Items.Insert(0, new ListItem("--- Please Select ---", "-1"));
            foreach (DataRow dr in dt.Rows)
            {
                lst = new ListItem();
                lst.Text = dr["COMP_CODE"].ToString() + ":" + dr["COMP_NAME"].ToString();
                lst.Value = dr["COMP_CODE"].ToString();
                ddlcompany.Items.Add(lst);
            }
        }
        else
        {
            ddlcompany.Items.Insert(0, new ListItem("--- No Data Found ---", "-1"));
        }
    }


    private void parameterpass(ParameterFields myParams, string pname, string value)
    {
        ParameterField param = new ParameterField();
        ParameterDiscreteValue dis1 = new ParameterDiscreteValue();
        param.ParameterFieldName = pname;
        dis1.Value = value;
        param.CurrentValues.Add(dis1);
        myParams.Add(param);
    }

    private void ShowReport(string selectionfor, string parameter, string reportname)
    {
        clsReport rpt = new clsReport();
        ParameterFields myParams = new ParameterFields();
        ConnectionInfo ConnInfo = new ConnectionInfo();
        string SCFconnStr = Session[GlobalData.sessionConnectionstring].ToString();
        string[] ff;
        string[] ss;
        string[] prm;
        prm = parameter.Split(';');
        if (prm.Length > 0)
        {
            for (int i = 0; i < prm.Length; i++)
            {
                parameterpass(myParams, prm[i].Split(':')[0].ToString(), prm[i].Split(':')[1].ToString());
            }
        }
        ff = SCFconnStr.Split('=');
        ss = ff[1].Split(';');
        ConnInfo.ServerName = ss[0];
        ss = ff[2].Split(';');
        ConnInfo.DatabaseName = ss[0];
        ss = ff[3].Split(';');
        ConnInfo.UserID = ss[0];
        ss = ff[4].Split(';');
        ConnInfo.Password = ss[0];
        rpt.FileName = reportname;
        rpt.ConnectionInfo = ConnInfo;
        rpt.ParametersFields = myParams;
        rpt.SelectionFormulla = selectionfor;
        Session[GlobalData.sessionReportDet] = rpt;
        RegisterStartupScript("click", "<script>window.open('./frm_report_viewer.aspx');</script>");
    }


    private string fiscal_year(DateTime dt)
    {

        string fiscal = "";

        sp_Fiscal_YearTableAdapter fis = new sp_Fiscal_YearTableAdapter();
        LibraryPF.dsMasterData.sp_Fiscal_YearDataTable dtfis = new LibraryPF.dsMasterData.sp_Fiscal_YearDataTable();
       
        DateTime currentdate = Convert.ToDateTime(dt);
        dtfis = fis.GetData(currentdate);

        fiscal = dtfis[0].fiscalyear.ToString();
        return fiscal;


    }

    protected void btnTaxReportShow_Click(object sender, EventArgs e)
    {
        tbl_Salary_Certificate_Ref_ReportTableAdapter cer = new tbl_Salary_Certificate_Ref_ReportTableAdapter();
        LibraryPF.dsMasterData.tbl_Salary_Certificate_Ref_ReportDataTable dtcer = new LibraryPF.dsMasterData.tbl_Salary_Certificate_Ref_ReportDataTable();


        hrms_emp_grd_detTableAdapter emp = new hrms_emp_grd_detTableAdapter();
        LibraryPF.dsMasterData.hrms_emp_grd_detDataTable dtemp = new LibraryPF.dsMasterData.hrms_emp_grd_detDataTable();
        dtemp = emp.GetData();

        string selectionfor, parameter, reportname;       
        selectionfor = "";

        string dateFrom = Convert.ToDateTime(txtFromDate.SelectedDate).ToString("dd-MMM-yyyy");
        string dateTo = Convert.ToDateTime(txtToDate.SelectedDate).ToString("dd-MMM-yyyy");

        string CompanyName = "CompanyName" + ":" + Session["CompanyName"].ToString().Split(':')[1].ToString();
        string CompanyAddress = "CompanyAddress" + ":" + Session["CompanyAddress"].ToString();

        string Startdate = "@start_date" + ":" + dateFrom;
        string Enddate = "@end_date" + ":" + dateTo;
        string regardsby = "regardsby" + ":" +txtregards.Text;

        string fiscalyear = fiscal_year(Convert.ToDateTime(dateTo));
        int refno = Convert.ToInt32(cer.MaxRef(fiscalyear));
     
        string ref_no = "";
        string refbyemp = "";


        if (txtemployeeSearch.Text != "")
        {

            dtcer = cer.GetRefByIDFiscalYear(txtemployeeSearch.Text.Split(':')[0], fiscalyear);

            if (dtcer.Rows.Count != 0)
            {
                refbyemp = dtcer[0].RefNo;
            }

            if(refbyemp=="")
            {
                refno = refno + 1;
                ref_no = "CEBL/" + txtemployeeSearch.Text.Split(':')[0] + "/SS" + "/" + fiscalyear + "/" + string.Format("{0:0000}", refno);
                cer.InsertFiscalData(txtemployeeSearch.Text.Split(':')[0], fiscalyear, ref_no);
            }

            else
            {
                //dtcer = cer.GetRefByIDFiscalYear(txtemployeeSearch.Text.Split(':')[0], fiscalyear);
                //ref_no = dtcer[0].RefNo;
            }
        }


        else

        {

            for (int i = 0; i < dtemp.Rows.Count; i++)
            {

                refno = Convert.ToInt32(cer.MaxRef( fiscalyear));

                dtcer = cer.GetRefByIDFiscalYear(dtemp[i].det_empid, fiscalyear);

                if (dtcer.Rows.Count != 0)
                {
                    refbyemp = dtcer[0].RefNo;
                }
            
                if (refbyemp == "")
                {
                    refno = refno + 1;
                    ref_no = "CEBL/" + dtemp[i].det_empid + "/SS" + "/" + fiscalyear + "/" + string.Format("{0:0000}", refno);
                    cer.InsertFiscalData(dtemp[i].det_empid, fiscalyear, ref_no);
                }

                else
                {
                   // dtcer = cer.GetRefByIDFiscalYear(dtemp[i].det_empid, fiscalyear);
                   // ref_no = dtcer[i].RefNo;
                }
            }

        }

        parameter = CompanyName + ";" + CompanyAddress + ";" + Startdate + ";" + Enddate + ";" + regardsby;
        reportname = "../Reports/SalaryCertificateReport.rpt";

        if (txtemployeeSearch.Text != "")
        {
            selectionfor = " {sp_SalaryCertificate;1.EmpID}=('" + txtemployeeSearch.Text.Split(':')[0] + "')";
        }

       
        ShowReport(selectionfor, parameter, reportname);
    }
        
    protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dbname = ddlcompany.SelectedItem.Value;
        if (dbname != "-1")
        {
            var constr = ConfigurationSettings.AppSettings["SCFConnectionString"].Replace("SCF", dbname);
            Session[GlobalData.sessionConnectionstring] = constr;           
            Session["CompanyName"] = ddlcompany.SelectedItem.Text;
            Session["CompanyAddress"] = current.CompanyAddress.ToString();
            Session["ConnectionStr"] = constr.ToString();
            Session["db"] = dbname.ToString();
            Session["EntryUserid"] = current.UserId.Trim();
        }
    }
    
}