using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Details_frmLeaveStatement : System.Web.UI.Page
{
    readonly string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"];
    private LeaveStatement _objLeaveStatement;
    private LeaveStatementController objLeaveStatementController;
    string rnode = "I";
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        popupFromDate.Attributes.Add("readonly", "readonly");
        if (!Page.IsPostBack)
        {
            Session["EntryUserid"] = current.UserId;
            ClsDropDownListController.LoadCheckBoxList(_connectionString, Sqlgenerate.SqlGetOfficeLocationIntoDDL(), chkofficelocation, "Division_Master_Name", "Division_Master_Code");
            txtEmpId_AutoCompleteExtender.ContextKey = _connectionString;
            LoadDepartmentIdByuserCode("ADM",current.CompanyCode, rnode.ToString());
          
        }

    }

    public void LoadDepartmentIdByuserCode(string userid, string companyid, string nodeid)
    {
        DataTable dt = new DataTable();
        string strSql = "";
        strSql = "  SELECT distinct DeptID, Dept FROM Emp_Details"
                      + " INNER JOIN Hrms_Emp_Mas on Emp_Details.EmpId = Hrms_Emp_Mas .Emp_Mas_Emp_Id and Emp_Mas_Term_Flg = 'N'"
                      + " Inner join tblUserCompanyDepartment on DepartmentID=Deptid"
                      + " where [UserID]='" + userid + "' and [NodeID]='" + nodeid + "' and [CompanyID]='" + companyid + "'"
                      + " ORDER BY Dept  ASC";

        dt = DataProcess.GetData(_connectionString.ToString(), strSql);
        if (dt.Rows.Count == 0)
        {
            strSql = "SELECT distinct DeptID, Dept FROM Emp_Details INNER JOIN Hrms_Emp_Mas on Emp_Details.EmpId = Hrms_Emp_Mas .Emp_Mas_Emp_Id and Emp_Mas_Term_Flg = 'N' ORDER BY Dept  ASC";
            dt = DataProcess.GetData(_connectionString.ToString(), strSql);
        }
        ddlDepartmentId.Items.Clear();
        ddlDepartmentId.Items.Insert(0, new ListItem("--- All Department ---", "-1"));
        foreach (DataRow dr in dt.Rows)
        {
            ListItem lst = new ListItem();
            lst.Value = dr["DeptID"].ToString();
            lst.Text = dr["Dept"].ToString();
            ddlDepartmentId.Items.Add(lst);
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            string officelocation = "";

            objLeaveStatementController = new LeaveStatementController();
            _objLeaveStatement = new LeaveStatement();
            _objLeaveStatement.EntryUser = current.UserId;
            _objLeaveStatement.FromDate = Convert.ToDateTime(popupFromDate.Text);
                       
            foreach (ListItem lst in chkofficelocation.Items)
            {
                if (lst.Selected)
                {
                    if (officelocation == "")
                    {
                        officelocation += "" + lst.Value.ToString() + "";
                    }
                    else
                    {
                        officelocation += "," + lst.Value.ToString() + "";
                    }
                }
            }

            _objLeaveStatement.OfficeLocation = officelocation;

            objLeaveStatementController.LeaveStatementPrepare(_connectionString, _objLeaveStatement);


            string sql = "";
            officelocation = "";
            foreach (ListItem lst in chkofficelocation.Items)
            {
                if (lst.Selected)
                {
                    if (officelocation == "")
                    {
                        officelocation += "'" + lst.Value.ToString() + "'";
                    }
                    else
                    {
                        officelocation += ",'" + lst.Value.ToString() + "'";
                    }
                }
            }

            string empcategory = ddlEmpCategory.SelectedItem.Value;

            officelocation = "(" + officelocation + ")";

            if (empcategory == "-1")
            {
                if (ddlDepartmentId.SelectedValue == "-1")
                {
                    sql = "create view viewForLeaveStatement as select EmpID,Dept,Sect,Designation from Emp_Details where OfficeID in " + officelocation + "";
                }
                else
                {
                    sql = "create view viewForLeaveStatement as select EmpID,Dept,Sect,Designation from Emp_Details where OfficeID in " + officelocation + " and DeptID = '" + ddlDepartmentId.SelectedValue + "'";

                }
            }
            else
            {
                if (ddlDepartmentId.SelectedValue == "-1")
                {
                    sql = "create view viewForLeaveStatement as select EmpID,Dept,Sect,Designation from Emp_Details where OfficeID in " + officelocation + " and emptype='" + empcategory + "'";
                }
                else
                {
                    sql = "create view viewForLeaveStatement as select EmpID,Dept,Sect,Designation from Emp_Details where OfficeID in " + officelocation + " and DeptID = '" + ddlDepartmentId.SelectedValue + "' and emptype='" + empcategory + "'";

                }
            }


            string empid = txtEmpId.Text.Trim().Split(':')[0].ToString();

            if (empid.ToString() != "")
            {
                sql = sql + " and EmpID='" + empid + "'";
            }


            DataProcess.ExecuteQuery(_connectionString.ToString(), "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[viewForLeaveStatement]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[viewForLeaveStatement]");
            DataProcess.ExecuteQuery(_connectionString.ToString(), sql);



            ShowFinalReport();

            
        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }   

    private void ShowFinalReport()
    {
        string selectionfor, parameter;
        selectionfor = "{tblSickLeaveBalForReport.entryUser}='" + current.UserId + "'";
        string CompanyName = "CompanyName" + ":" + current.CompanyName.ToString();
        string CompanyAddress = "CompanyAddress" + ":" + current.CompanyAddress.ToString();
        string FromDate = "FromDate" + ":" + (popupFromDate.Text == string.Empty ? "" : Convert.ToDateTime(popupFromDate.Text).ToString("dd-MMM-yyyy"));
        parameter = CompanyName + ";" + CompanyAddress + ";" + FromDate;
        string reportname = "../Reports/LeaveStatement.rpt";
        ShowReport(_connectionString, selectionfor, parameter, reportname);
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

    private void ShowReport(string SCFconnStr, string selectionfor, string parameter, string reportname)
    {
        clsReport rpt = new clsReport();
        ParameterFields myParams = new ParameterFields();
        ConnectionInfo ConnInfo = new ConnectionInfo();
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
    private void InsertTempTableLeavBALeByemployeeID(String empid, DateTime fDate)
    {
        DataTable dt = new DataTable();
        LeaveProcess lvproc = new LeaveProcess();
        lvproc.GetEmployeeLeaveBalance(_connectionString, fDate, empid);
    }
}