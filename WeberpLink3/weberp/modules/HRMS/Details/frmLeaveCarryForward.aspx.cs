using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Details_frmLeaveCarryForward : System.Web.UI.Page
{
    readonly string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"];
    LeaveCarryForwardController _objLeaveCarryForwardController;
    LeaveCarryForward _objLeaveCarryForward;
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);
        popupDate.Attributes.Add("readonly", "readonly");
        if (!Page.IsPostBack)
        {
            txtEmployeeSearch_AutoCompleteExtender.ContextKey = _connectionString;
        }

    }
    protected void txtEmployeeSearch_TextChanged(object sender, EventArgs e)
    {
        if (txtEmployeeSearch.Text != string.Empty)
        {
            txtEmployeeSearch.Text = txtEmployeeSearch.Text.Split(':')[0].Trim() == "" ? "" : txtEmployeeSearch.Text.Split(':')[0].Trim();
            var storedProcedureCommandText = "exec [ManualLeaveGetLeaveType] '" + txtEmployeeSearch.Text + "'";
            ClsDropDownListController.LoadDropDownListFromStoredProcedure(_connectionString, storedProcedureCommandText, ddlLeaveType, "Leave_Mas_Name", "Leave_Mas_Code");
            GetCarryLeaveRecord(txtEmployeeSearch.Text);
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            SaveCarryForwardData();
            clearfield();
            MessageBox1.ShowSuccess("Data saved Successful");
        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }

    }

    private void SaveCarryForwardData()
    {
        _objLeaveCarryForward = new LeaveCarryForward();
        _objLeaveCarryForward.EmployeeCode = txtEmployeeSearch.Text == string.Empty ? null : txtEmployeeSearch.Text;
        _objLeaveCarryForward.SelectedDate = popupDate.Text == string.Empty ? null : Convert.ToDateTime(popupDate.Text).ToString("dd-MMM-yyyy");
        _objLeaveCarryForward.LeaveType = ddlLeaveType.SelectedValue == "-1" ? null : ddlLeaveType.SelectedValue;
        _objLeaveCarryForward.NoofLeave = txtNoOfDays.Text == string.Empty ? 0 : Convert.ToSingle(txtNoOfDays.Text);
        _objLeaveCarryForward.EntryUser = current.UserId;
        _objLeaveCarryForwardController = new LeaveCarryForwardController();
        _objLeaveCarryForwardController.Save(_connectionString, _objLeaveCarryForward);

        GetCarryLeaveRecord(_objLeaveCarryForward.EmployeeCode);

       


    }

    private void clearfield()
    {
        txtEmployeeSearch.Text = "";
        popupDate.Text = "";
        txtNoOfDays.Text = "";
    }

    private void GetCarryLeaveRecord(string employeeCode)
    {
        _objLeaveCarryForward = new LeaveCarryForward();
        _objLeaveCarryForward.EmployeeCode = employeeCode;
        _objLeaveCarryForwardController = new LeaveCarryForwardController();
        var dtLeaveReport = _objLeaveCarryForwardController.GetData(_connectionString, _objLeaveCarryForward);
        grdLeaveReport.DataSource = null;
        grdLeaveReport.DataBind();
        if (dtLeaveReport.Rows.Count > 0)
        {
            grdLeaveReport.DataSource = dtLeaveReport;
            grdLeaveReport.DataBind();
        }

    }
    protected void grdLeaveReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());

        if (e.CommandName.Equals("Delete"))
        {
            var lblEmpid = ((Label)grdLeaveReport.Rows[selectedIndex].FindControl("lblEmpid")).Text;
            var lblLeaveType = ((Label)grdLeaveReport.Rows[selectedIndex].FindControl("lblLeaveType")).Text;
            string msg = null;
            try
            {
                _objLeaveCarryForwardController = new LeaveCarryForwardController();
                _objLeaveCarryForward = new LeaveCarryForward();
                _objLeaveCarryForward.EmployeeCode = lblEmpid;
                _objLeaveCarryForward.LeaveType = lblLeaveType;
                _objLeaveCarryForwardController.Delete(_connectionString, _objLeaveCarryForward);
                GetCarryLeaveRecord(_objLeaveCarryForward.EmployeeCode);
            }
            catch (SqlException sqlError)
            {
                msg = "  Error Occured During Operation into Database, Data did not Delete from Database !  ";

            }
            catch (Exception inSystemExep)
            {
                msg = " Error Occured, Data did not Delete from Database  ! ";

            }
            finally
            {
                if (msg != null)
                {
                    ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "MessageBox",
                        "alert(' " + msg + " ');",
                        true);
                }
            }
        }
    }
    protected void grdLeaveReport_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void grdLeaveReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[4].Visible = false;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearControl();
    }
    private void ClearControl()
    {
        txtEmployeeSearch.Text = string.Empty;
        popupDate.Text = string.Empty;
        ddlLeaveType.Items.Clear();
        txtNoOfDays.Text = string.Empty;
        grdLeaveReport.DataSource = null;
        grdLeaveReport.DataBind();
    }
}