using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Payroll_frmPayslip : System.Web.UI.Page
{
    private const string Rnode = "aa";
    string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        txtSalaryDate.Attributes.Add("readonly", "readonly");
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
        Session[GlobalData.sessionConnectionstring] = ConnectionString;
        Session["CompanyName"] = ddlcompany.SelectedItem.Text;
        Session["CompanyAddress"] = current.CompanyAddress.ToString();
        LibraryPAY.Properties.Settings.Default["SCFConnectionString"] = ConnectionString;
        LibraryPAY.Properties.Settings.Default.Save();
        LoadofficeLocation();

        txtEmpId_AutoCompleteExtender.ContextKey = Session[GlobalData.sessionConnectionstring].ToString();
    }

    private void LoadofficeLocation()
    {

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



    protected void btnSalreportPosted_Click(object sender, EventArgs e)
    {

        if (ddlcompany.Text == "")
        {
            lblmsg.Text = "Company Selection Error";
            lblmsg.Visible = true;
            return;
        }

        if (PeriodSelectionValidation() == false)
        {
            lblmsg.Text = "Period Selection Error";
            lblmsg.Visible = true;
            return;
        }
        if (txtEmpId.Text.Trim() == "")
        {
            lblmsg.Text = "Employee ID Selection Error";
            lblmsg.Visible = true;
            return;
        }

        string employeeid = "";
        string[] emp = txtEmpId.Text.Split(':');
        if (emp.Length < 2)
            return;
        else
            employeeid = emp[0].ToString();


        string rFileName = "";

        if (rblForPay.SelectedValue == "S")
        {
            rFileName = "HrmsEmpPaySlip.rpt";
        }
        else
        {
            rFileName = "HrmsEmpBonusslip.rpt";
        }



        if (rblForCondition.SelectedValue == "P")
        {
            string selectionfor, parameter;
            int mth = Convert.ToInt32(txtSalaryDate.Text.Split('/')[0].ToString());
            int yr = Convert.ToInt32(txtSalaryDate.Text.Split('/')[1].ToString());
            string salmonth = txtSalaryDate.Text;
            System.Globalization.DateTimeFormatInfo mfi = new
            System.Globalization.DateTimeFormatInfo();
            string strMonthName = mfi.GetMonthName(mth).ToString();
            salmonth = strMonthName + "/" + txtSalaryDate.Text.Split('/')[1].ToString();

            if (rblForPay.SelectedValue == "S")
            {
                //Salary
                DataProcess.ExecuteQuery(Session[GlobalData.sessionConnectionstring].ToString(), "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[salarypostedview]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[salarypostedview]");
                DataProcess.ExecuteQuery(Session[GlobalData.sessionConnectionstring].ToString(), "Create view salarypostedview as select * from Hrms_salary where month(salmonth)=" + mth + " and year(salmonth)=" + yr + " and empcode='" + employeeid.ToString() + "' and SalGrade<>'50'");
            }
            else
            {
                //Bonus
                DataProcess.ExecuteQuery(Session[GlobalData.sessionConnectionstring].ToString(), "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[salarypostedview]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[salarypostedview]");
                DataProcess.ExecuteQuery(Session[GlobalData.sessionConnectionstring].ToString(), "Create view salarypostedview as select * from Hrms_salary where month(salmonth)=" + mth + " and year(salmonth)=" + yr + " and empcode='" + employeeid.ToString() + "' and SalGrade='50'");

            }


            selectionfor = "{Hrms_Trans_Det.Trans_Det_Emp_Id}='" + employeeid.ToString() + "'";

            string CompanyName = "CompanyName" + ":" + Session["CompanyName"].ToString().Split(':')[1].ToString();
            string CompanyAddress = "CompanyAddress" + ":" + Session["CompanyAddress"].ToString();
            salmonth = "salmonth" + ":" + salmonth;
            parameter = salmonth + ";" + CompanyName + ";" + CompanyAddress;
            string reportname = "../Reports/" + rFileName;
            ShowReport(selectionfor, parameter, reportname);
        }
        else if (rblForCondition.SelectedValue == "H")
        {
            string selectionfor, parameter;
            int mth = Convert.ToInt32(txtSalaryDate.Text.Split('/')[0].ToString());
            int yr = Convert.ToInt32(txtSalaryDate.Text.Split('/')[1].ToString());
            string salmonth = txtSalaryDate.Text;
            System.Globalization.DateTimeFormatInfo mfi = new
            System.Globalization.DateTimeFormatInfo();
            string strMonthName = mfi.GetMonthName(mth).ToString();
            salmonth = strMonthName + "/" + txtSalaryDate.Text.Split('/')[1].ToString();

            selectionfor = "{Hrms_Trans_Det.Trans_Det_Emp_Id}=" + employeeid.ToString();

            string CompanyName = "CompanyName" + ":" + Session["CompanyName"].ToString().Split(':')[1].ToString();
            string CompanyAddress = "CompanyAddress" + ":" + Session["CompanyAddress"].ToString();
            salmonth = "salmonth" + ":" + salmonth;
            parameter = salmonth + ";" + CompanyName + ";" + CompanyAddress;
            string reportname = "../Reports/" + rFileName;
            ShowReport(selectionfor, parameter, reportname);
        }
    }

    private bool PeriodSelectionValidation()
    {
        int mm = 0, yyyy = 0;
        string[] tmp = txtSalaryDate.Text.Split('/');
        if (tmp.Length < 2) return false;
        try
        {
            mm = Convert.ToInt32(tmp[0]);
            yyyy = Convert.ToInt32(tmp[1]);
            return true;
        }
        catch
        {
            return false;
        }

    }
}