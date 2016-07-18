using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class modules_HRMS_Details_frmLeaveReport : System.Web.UI.Page
{
    readonly string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"];
    private LeaveRecord _objLeaveRecord;
    private LeaveRecordController _objLeaveRecordController;

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        popupFromDate.Attributes.Add("readonly", "readonly");
        popupToDate.Attributes.Add("readonly", "readonly");
        if (!Page.IsPostBack)
        {
            txtEmployeeCode_AutoCompleteExtender.ContextKey = _connectionString;
        }

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            _objLeaveRecord = new LeaveRecord
            {
                StartDate = popupFromDate.Text == string.Empty ? null : Convert.ToDateTime(popupFromDate.Text).ToString("dd-MMM-yyyy"),
                EndDate = popupToDate.Text == string.Empty ? null : Convert.ToDateTime(popupToDate.Text).ToString("dd-MMM-yyyy"),
                EmployeeCode = txtEmployeeCode.Text == string.Empty ? null : txtEmployeeCode.Text
            };
            LoadLeaveRecord(_connectionString, _objLeaveRecord);
        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError("Error :" + msgException.Message);
        }

    }

    private void LoadLeaveRecord(string connectionString, LeaveRecord objLeaveRecord)
    {
        _objLeaveRecordController = new LeaveRecordController();
        DataTable leaveRecord = _objLeaveRecordController.GetLeaveRecord(connectionString, objLeaveRecord);
        grdLeaveSummary.DataSource = null;
        grdLeaveSummary.DataBind();
        grdLeaveDetails.DataSource = null;
        grdLeaveDetails.DataBind();
        btnExporttoExcel.Visible = false;
        btnExporttoExcelDetails.Visible = false;
        if (leaveRecord.Rows.Count > 0)
        {
            grdLeaveSummary.DataSource = leaveRecord;
            grdLeaveSummary.DataBind();
            btnExporttoExcel.Visible = true;
        }
    }
    protected void grdLeaveSummary_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("Select"))
            {
                int selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
                string lblEmpId = ((Label)grdLeaveSummary.Rows[selectedIndex].FindControl("lblEmpID")).Text;
                _objLeaveRecord = new LeaveRecord();
                _objLeaveRecord.EmployeeCode = lblEmpId;
                _objLeaveRecord.StartDate = popupFromDate.Text == string.Empty ? null : Convert.ToDateTime(popupFromDate.Text).ToString("dd-MMM-yyyy");
                _objLeaveRecord.EndDate = popupToDate.Text == string.Empty ? null : Convert.ToDateTime(popupToDate.Text).ToString("dd-MMM-yyyy");
                ShowLeaveRecordDetails(_connectionString, _objLeaveRecord);
            }
        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError("Error :" + msgException.Message);
        }
    }

    private void ShowLeaveRecordDetails(string connectionString, LeaveRecord objLeaveRecord)
    {
        _objLeaveRecordController = new LeaveRecordController();
        DataTable dtLeaveRecordDetails = _objLeaveRecordController.GetLeaveRecordDetails(connectionString, objLeaveRecord);
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
    protected void grdLeaveSummary_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[6].Visible = false;
    }
    protected void grdLeaveDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[6].Visible = false;
    }
    protected void btnExporttoExcel_Click(object sender, EventArgs e)
    {
        const string type = "LeaveReport.xls";
        ExportGridToExcel.Export(type, grdLeaveSummary);
    }
    protected void btnExporttoExcelDetails_Click(object sender, EventArgs e)
    {
        const string type = "LeaveReportDetails.xls";
        ExportGridToExcel.Export(type, grdLeaveDetails);
    }
    protected void txtEmployeeCode_TextChanged(object sender, EventArgs e)
    {
        if (txtEmployeeCode.Text != string.Empty)
        {
            txtEmployeeCode.Text = txtEmployeeCode.Text.Split(':')[0].Trim() == "" ? "" : txtEmployeeCode.Text.Split(':')[0].Trim();
        }
    }
}