using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class modules_HRMS_Details_frmDaywiseLeaveReport : System.Web.UI.Page
{
    string rnode = "f";
    readonly string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"];
    private DaywiseLeaveReport _objDaywiseLeaveReport;
    private DaywiseLeaveReportController _objDaywiseLeaveReportController;

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        popupFromDate.Attributes.Add("readonly", "readonly");
        popupToDate.Attributes.Add("readonly", "readonly");
        try
        {
            if (!Page.IsPostBack)
            {
                LoadCompanyByUserPermission("ADM", rnode);
                ClsDropDownListController.EnableDisableDropDownList(ddlcompany);
                ClsDropDownListController.LoadCheckBoxList(_connectionString, Sqlgenerate.SqlGetOfficeLocationIntoDDL(), chkofficelocation, "Division_Master_Name", "Division_Master_Code");
                txtEmployeeCode_AutoCompleteExtender.ContextKey = _connectionString;
                GetDepartmentCode();
            }

        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }

    }

    private void GetDepartmentCode()
    {
        try
        {
            DataTable dt = new DataTable();
            CommonMethods objCommonMethods = new CommonMethods();
            dt = objCommonMethods.LoadDepartmentIdByuserCode(_connectionString, "ADM", ddlcompany.SelectedValue, rnode.ToString());
            ddlDepartmentId.Items.Clear();
            ddlDepartmentId.Items.Add("ALL");
            foreach (DataRow dr in dt.Rows)
            {
                ListItem lst = new ListItem();
                lst.Value = dr["DeptID"].ToString();
                lst.Text = dr["Dept"].ToString();
                ddlDepartmentId.Items.Add(lst);
            }

        }
        catch (Exception msgException)
        {
            
            throw msgException;
        }
    }
    private void LoadCompanyByUserPermission(string userid, string nodeid)
    {
        DataTable dt = new DataTable();
        ListItem lst;
        ddlcompany.Items.Clear();
        ddlcompany.Items.Add("");
        dt = AccessPermission.GetCompanyByUserandNodeid(_connectionString, userid, nodeid);
        foreach (DataRow dr in dt.Rows)
        {
            lst = new ListItem();
            lst.Text = dr["COMP_CODE"].ToString() + ":" + dr["COMP_NAME"].ToString();
            lst.Value = dr["COMP_CODE"].ToString();
            ddlcompany.Items.Add(lst);
        }
    }
    

    
    

    private void ShowLeaveRecordDetails()
    {
        try
        {
            string officelocation = null;
            _objDaywiseLeaveReport = new DaywiseLeaveReport();
            _objDaywiseLeaveReport.StartDate = popupFromDate.Text == string.Empty ? null : Convert.ToDateTime(popupFromDate.Text).ToString("dd-MMM-yyyy");
            _objDaywiseLeaveReport.EndDate = popupToDate.Text == string.Empty ? null : Convert.ToDateTime(popupToDate.Text).ToString("dd-MMM-yyyy");
            foreach (ListItem lst in chkofficelocation.Items)
            {
                if (lst.Selected)
                {
                    if (officelocation == null)
                    {
                        officelocation += "" + lst.Value.ToString() + "";
                    }
                    else
                    {
                        officelocation += "','" + lst.Value.ToString() + "";
                    }
                }
            }
            if (officelocation != null)
            {
                officelocation = "('" + officelocation + "')";
                
            }

            _objDaywiseLeaveReport.OfficeLocation = officelocation;

            _objDaywiseLeaveReport.Department = ddlDepartmentId.SelectedValue == "ALL" ? null : ddlDepartmentId.SelectedValue;
            _objDaywiseLeaveReport.EmployeeCategory = ddlEmpCategory.SelectedValue == "-1" ? null : ddlEmpCategory.SelectedValue;
            _objDaywiseLeaveReport.EmployeeCode = txtEmployeeCode.Text == string.Empty ? null : txtEmployeeCode.Text;
            
            _objDaywiseLeaveReportController = new DaywiseLeaveReportController();
            DataTable dtLeaveRecordDetails = _objDaywiseLeaveReportController.GetLeaveRecord(_connectionString, _objDaywiseLeaveReport);
            grdLeaveDetails.DataSource = null;
            grdLeaveDetails.DataBind();
            btnExporttoExcelDetails.Visible = false;
            if (dtLeaveRecordDetails.Rows.Count > 0)
            {
                grdLeaveDetails.DataSource = dtLeaveRecordDetails;
                grdLeaveDetails.DataBind();
                btnExporttoExcelDetails.Visible = true;
            }

        }
        catch (Exception msgException)
        {
            
            throw msgException;
        }
    }
    
    
    
    protected void btnExporttoExcelDetails_Click(object sender, EventArgs e)
    {
        const string type = "Day_wise_Leave_Report.xls";
        ExportGridToExcel.Export(type, grdLeaveDetails);
    }
    protected void txtEmployeeCode_TextChanged(object sender, EventArgs e)
    {
        if (txtEmployeeCode.Text != string.Empty)
        {
            txtEmployeeCode.Text = txtEmployeeCode.Text.Split(':')[0].Trim() == "" ? "" : txtEmployeeCode.Text.Split(':')[0].Trim();
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            ShowLeaveRecordDetails();

        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }

    }
}