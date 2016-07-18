using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Payroll_frmEarnedLeaveCalculation : System.Web.UI.Page
{
    private const string Rnode = "aa";
    string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);
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
        LibraryPAY.Properties.Settings.Default["SCFConnectionString"] = constr;
        LibraryPAY.Properties.Settings.Default.Save();
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

    protected void btnEL_Click(object sender, EventArgs e)
    {
        Elcalculationbyemployeeid();
    }

    private void Elcalculationbyemployeeid()
    {
        SqlConnection oConnection = new SqlConnection(Session[GlobalData.sessionConnectionstring].ToString());
        oConnection.Open();
        SqlCommand cmd = new SqlCommand("EarnLeaveCalculationByEmpid", oConnection);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlParameter EmployeeID = cmd.Parameters.Add("@remployeeid", SqlDbType.NVarChar, 10);
        EmployeeID.Value = txtempid.Text.Trim().ToString();
        SqlParameter trndate = cmd.Parameters.Add("@rdateto", SqlDbType.NVarChar, 22);
        trndate.Value = dt2.SelectedDate.ToShortDateString();
        SqlParameter operatorID = cmd.Parameters.Add("@entryuser", SqlDbType.NVarChar, 10);
        operatorID.Value = "ADM";//Session[GlobalData.SessionUserId].ToString();

        // output parm
        //SqlParameter outputStr = cmd.Parameters.Add("@outputStr", SqlDbType.NVarChar, 100);
        //outputStr.Direction = ParameterDirection.Output;

        // return value
        SqlParameter returnnVal = cmd.Parameters.Add("returnnVal", SqlDbType.Int);
        returnnVal.Direction = ParameterDirection.ReturnValue;
        cmd.ExecuteNonQuery();
        string msg = "";
        if ((int)returnnVal.Value == 0)
        {
            //string rest = Convert.ToString(outputStr.Value);
            msg = "EL Calculated Successful";
        }
        else
        {
            msg = "Error...please try again";
        }
        MessageBoxShow(this, msg);
    }

    protected void btnElpreview_Click(object sender, EventArgs e)
    {
        string selectionfor, parameter, flag;
        if (txtempid.Text.Trim().ToString() == "")
        {
            MessageBoxShow(this, "Select Employee then try...");
        }

        DataTable dt = DataProcess.GetData(Session[GlobalData.sessionConnectionstring].ToString(), "select HPflag from [Hrms_Emp_EL_Encashment] where empid='" + txtempid.Text.Trim().ToString() + "' and paymentNo=(select max(paymentNo)from [Hrms_Emp_EL_Encashment] where empid='" + txtempid.Text.Trim().ToString() + "')");
        if (dt.Rows.Count > 0)
        {
            flag = dt.Rows[0]["HPflag"].ToString();
        }
        else
        {
            MessageBoxShow(this, "No Data Found...");
            return;
        }

        selectionfor = "{EmpELEncashmentbyid.EmpID}='" + txtempid.Text.Trim().ToString() + "' ";

        string CompanyName = "CompanyName" + ":" + Session["CompanyName"].ToString().Split(':')[1].ToString();
        string CompanyAddress = "CompanyAddress" + ":" + Session["CompanyAddress"].ToString();
        flag = "flag" + ":" + flag.ToString();

        parameter = CompanyName + ";" + CompanyAddress + ";" + flag;

        string reportname = "../Reports/ELencashmentByemp.rpt";

        ShowReport(selectionfor, parameter, reportname);
    }

    protected void btnElpost_Click(object sender, EventArgs e)
    {
        if (txtempid.Text.Trim().ToString() == "")
        {
            return;
        }
        SqlConnection oConnection = new SqlConnection(Session[GlobalData.sessionConnectionstring].ToString());
        oConnection.Open();
        SqlCommand cmd = new SqlCommand("[EarnLeavePaymentPostByempid]", oConnection);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlParameter EmployeeID = cmd.Parameters.Add("@rEmpid", SqlDbType.NVarChar, 10);
        EmployeeID.Value = txtempid.Text.Trim().ToString();

        // return value
        SqlParameter returnnVal = cmd.Parameters.Add("returnnVal", SqlDbType.Int);
        returnnVal.Direction = ParameterDirection.ReturnValue;
        cmd.ExecuteNonQuery();
        string msg = "";
        if ((int)returnnVal.Value == 0)
        {
            msg = "Data posted Successful";
            txtempid.Text = "";
        }
        else
        {
            msg = "Error...please try again";
        }
        MessageBoxShow(this, msg);
    }

    private void MessageBoxShow(Page page, string message)
    {
        Literal ltr = new Literal();
        ltr.Text = @"<script type='text/javascript'> alert('" + message + "') </script>";
        page.Controls.Add(ltr);
    }
}