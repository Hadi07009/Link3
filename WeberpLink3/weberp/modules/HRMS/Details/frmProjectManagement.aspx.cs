using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class modules_HRMS_Details_frmProjectManagement : System.Web.UI.Page
{
    readonly string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"];
    ProjectManagement _objProjectManagement;
    ProjectManagementController _objProjectManagementController;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                LoadProjectList();
                txtpResponsibleID_AutoCompleteExtender.ContextKey = _connectionString;
                txtpAccountableID_AutoCompleteExtender.ContextKey = _connectionString;
                txtpConsultedID_AutoCompleteExtender.ContextKey = _connectionString;
                txtpInformedID_AutoCompleteExtender.ContextKey = _connectionString;
                _objProjectManagementController = new ProjectManagementController();
                _objProjectManagementController.LoadPriority(_connectionString, ddlpPriority);
                ControlPanelVisible(PanelTaksList, PanelNewEntry, PanelProgress, PanelActivityLog);
            }
            catch (Exception msgException)
            {

                MessageBox1.ShowError(msgException.Message);
            }

        }

    }

    private void ControlPanelVisible(Panel targetPanel, Panel optionalPanel1, Panel optionalPanel2, Panel optionalPanel3)
    {
        try
        {
            targetPanel.Visible = true;
            optionalPanel1.Visible = false;
            optionalPanel2.Visible = false;
            optionalPanel3.Visible = false;
        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }

    private void LoadProjectList()
    {
        try
        {
            _objProjectManagement = new ProjectManagement();
            _objProjectManagement.ResponsibleID = current.UserId.ToString();
            _objProjectManagement.EntryUserId = current.UserId.ToString();
            _objProjectManagementController = new ProjectManagementController();
            _objProjectManagement.ProjectList = null;
            _objProjectManagement.ProjectList = _objProjectManagementController.GetProjectList(_connectionString, _objProjectManagement);
            grdTaskList.DataSource = null;
            grdTaskList.DataBind();
            if (_objProjectManagement.ProjectList != null)
            {
                grdTaskList.DataSource = _objProjectManagement.ProjectList;
                grdTaskList.DataBind();
            }

        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }
    protected void grdTaskList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;
        ClsGridViewLoad.ShowConfirmDelete(grdTaskList, e);
    }
    
    protected void grdTaskList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            var selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
            lblActivityID.Text = ((Label)grdTaskList.Rows[selectedIndex].FindControl("lblActivityID")).Text;

            if (e.CommandName.Equals("Plan"))
            {
                lblActivityText.Text = ((Label)grdTaskList.Rows[selectedIndex].FindControl("lblActivityName")).Text;
                this.ClearControl();
                this.ControlPanelVisible(PanelNewEntry, PanelTaksList, PanelProgress, PanelActivityLog);

            }

            if (e.CommandName.Equals("Delete"))
            {
                _objProjectManagement = new ProjectManagement();
                _objProjectManagement.ActivityID = Convert.ToInt32(lblActivityID.Text);
                _objProjectManagementController = new ProjectManagementController();
                _objProjectManagementController.Delete(_connectionString, _objProjectManagement);
                this.LoadProjectList();
            }

            if (e.CommandName.Equals("Select"))
            {
                lblActivityText.Text = string.Empty;
                _objProjectManagement = new ProjectManagement();
                _objProjectManagement.ParentActivityID = Convert.ToInt32(lblActivityID.Text);
                _objProjectManagementController = new ProjectManagementController();
                _objProjectManagement.ActivityDetails = _objProjectManagementController.GetActivityDetails(_connectionString, _objProjectManagement);
                if (_objProjectManagement.ActivityDetails != null)
                {
                    foreach (DataRow rowNo in _objProjectManagement.ActivityDetails.Rows)
                    {
                        txtPActivity.Text = rowNo["ActivityName"].ToString();
                        txtpResponsibleID.Text = rowNo["ResponsibleID"].ToString();
                        txtpAccountableID.Text = rowNo["AccountableID"].ToString();
                        txtpConsultedID.Text = rowNo["ConsultedID"].ToString();
                        txtpInformedID.Text = rowNo["InformedID"].ToString();
                        txtpDueDate.Text = Convert.ToDateTime(rowNo["DueDate"].ToString()).ToShortDateString();
                        ddlpPriority.SelectedValue = rowNo["PriorityID"].ToString() == null ? "-1" : rowNo["PriorityID"].ToString();
                        txtpDescription.Text = rowNo["Description"].ToString();
                        txtpLogisticesRequired.Text = rowNo["LogisticesRequired"].ToString();
                        txtpRelatedCosts.Text = rowNo["RelatedCosts"].ToString() == "" ? string.Empty : Convert.ToDouble(rowNo["RelatedCosts"].ToString()).ToString("F2");
                        txtpAnyIssue.Text = rowNo["AnyIssue"].ToString();
                        txtpRemarks.Text = rowNo["Remarks"].ToString();
                        btnAdd.Text = "Save";
                        this.ControlPanelVisible(PanelNewEntry, PanelTaksList, PanelProgress, PanelActivityLog);
                    }

                }
            }

            if (e.CommandName.Equals("Progress"))
            {
                lblActivityNameProgress.Text = ((Label)grdTaskList.Rows[selectedIndex].FindControl("lblActivityName")).Text;
                _objProjectManagementController = new ProjectManagementController();
                _objProjectManagementController.LoadActivityStatus(_connectionString, ddlActivityStatus);
                this.ControlPanelVisible(PanelProgress, PanelTaksList, PanelNewEntry, PanelActivityLog);
            }

            if (e.CommandName.Equals("ActivityLog"))
            {
                lblActivityNameLog.Text = ((Label)grdTaskList.Rows[selectedIndex].FindControl("lblActivityName")).Text;
                LoadActivityLog();
                this.ControlPanelVisible(PanelActivityLog, PanelTaksList, PanelNewEntry, PanelProgress);
            }

        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }

    }

    private void LoadActivityLog()
    {
        try
        {
            _objProjectManagement = new ProjectManagement();
            _objProjectManagement.ActivityID = Convert.ToInt32(lblActivityID.Text);
            _objProjectManagementController = new ProjectManagementController();
            _objProjectManagement.ActivityLog = _objProjectManagementController.GetActivityLog(_connectionString, _objProjectManagement);
            ClsGridViewLoad.GetData(_objProjectManagement.ActivityLog, grdActivityLog);
        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }

    private void AddValuesForSubmit()
    {
        try
        {
            _objProjectManagement = new ProjectManagement();
            _objProjectManagement.ParentActivityID = Convert.ToInt32(lblActivityID.Text);
            _objProjectManagement.ActivityName = txtPActivity.Text == string.Empty ? null : txtPActivity.Text;
            _objProjectManagement.ResponsibleID = txtpResponsibleID.Text == string.Empty ? null : txtpResponsibleID.Text.Split(':')[0].Trim();
            _objProjectManagement.AccountableID = txtpAccountableID.Text == string.Empty ? null : txtpAccountableID.Text.Split(':')[0].Trim();
            _objProjectManagement.ConsultedID = txtpConsultedID.Text == string.Empty ? null : txtpConsultedID.Text.Split(':')[0].Trim();
            _objProjectManagement.InformedID = txtpInformedID.Text == string.Empty ? null : txtpInformedID.Text.Split(':')[0].Trim();
            if (txtpDueDate.Text == string.Empty || txtpDueDate.Text == "")
            {
                _objProjectManagement.DueDate = null;
            }
            else
            {
                _objProjectManagement.DueDate = Convert.ToDateTime(txtpDueDate.Text);
            }

            _objProjectManagement.EntryUserId = current.UserId;
            if (ddlpPriority.SelectedValue == "-1")
            {
                _objProjectManagement.PriorityID = null;
            }
            else
            {
                _objProjectManagement.PriorityID = Convert.ToInt32(ddlpPriority.SelectedValue);
            }

            _objProjectManagement.Description = txtpDescription.Text == string.Empty ? null : txtpDescription.Text;
            _objProjectManagement.LogisticesRequired = txtpLogisticesRequired.Text == string.Empty ? null : txtpLogisticesRequired.Text;
            if (txtpRelatedCosts.Text == string.Empty)
            {
                _objProjectManagement.RelatedCosts = 0;
            }
            else
            {
                _objProjectManagement.RelatedCosts = Convert.ToDecimal(txtpRelatedCosts.Text);
            }

            _objProjectManagement.AnyIssue = txtpAnyIssue.Text == string.Empty ? null : txtpAnyIssue.Text;
            _objProjectManagement.Remarks = txtpRemarks.Text == string.Empty ? null : txtpRemarks.Text;
            _objProjectManagementController = new ProjectManagementController();
            if (btnAdd.Text == "Add")
            {
                _objProjectManagement.ActivityDetails = _objProjectManagementController.GetActivityDetails(_connectionString, _objProjectManagement);
                if (_objProjectManagement.ActivityDetails != null)
                {
                    foreach (DataRow rowNo in _objProjectManagement.ActivityDetails.Rows)
                    {

                        _objProjectManagement.TierNo = Convert.ToInt32(rowNo["TierNo"].ToString()) + 1;
                        _objProjectManagement.SerialNo = 1;
                        _objProjectManagement.RootNo = Convert.ToInt32(rowNo["RootNo"].ToString());
                        _objProjectManagement.SeqNo = _objProjectManagementController.GetSeqNo(_connectionString, _objProjectManagement);
                    }

                }
                else
                {
                    _objProjectManagement.SeqNo = 0;
                    _objProjectManagement.TierNo = 0;
                    _objProjectManagement.SerialNo = 1;
                    _objProjectManagement.RootNo = 0;
                }

                _objProjectManagementController.Save(_connectionString, _objProjectManagement);
            }
            else
            {
                _objProjectManagement.ActivityID = Convert.ToInt32(lblActivityID.Text);
                _objProjectManagementController.Update(_connectionString, _objProjectManagement);
            }

        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }


    protected void btnDone_Click(object sender, EventArgs e)
    {
        this.ControlPanelVisible(PanelTaksList, PanelNewEntry, PanelProgress, PanelActivityLog);
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            this.AddValuesForSubmit();
            this.LoadProjectList();
            ClearControl();
            MessageBox1.ShowSuccess("Data processed successfully");
        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }

    private void ClearControl()
    {
        try
        {
            txtPActivity.Text = string.Empty;
            txtpResponsibleID.Text = string.Empty;
            txtpAccountableID.Text = string.Empty;
            txtpConsultedID.Text = string.Empty;
            txtpInformedID.Text = string.Empty;
            txtpDueDate.Text = string.Empty;
            ddlpPriority.SelectedValue = "-1";
            txtpDescription.Text = string.Empty;
            txtpLogisticesRequired.Text = string.Empty;
            txtpRelatedCosts.Text = string.Empty;
            txtpAnyIssue.Text = string.Empty;
            txtpRemarks.Text = string.Empty;
            btnAdd.Text = "Add";
        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }

    protected void grdTaskList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btnDoneProgress_Click(object sender, EventArgs e)
    {
        this.ControlPanelVisible(PanelTaksList, PanelNewEntry, PanelProgress, PanelActivityLog);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            AddValuesProgress();
            ClearControlProgress();
            MessageBox1.ShowSuccess("Data processed successfully");
        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }

    private void ClearControlProgress()
    {
        try
        {
            txtRemarksProgress.Text = string.Empty;
            ddlActivityStatus.SelectedValue = "-1";
        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }

    private void AddValuesProgress()
    {
        try
        {
            _objProjectManagement = new ProjectManagement();
            _objProjectManagement.ProjectID = 1;
            _objProjectManagement.ActivityID = Convert.ToInt32(lblActivityID.Text);
            _objProjectManagement.Remarks = txtRemarksProgress.Text == string.Empty ? null : txtRemarksProgress.Text;
            if (ddlActivityStatus.SelectedValue == "-1")
            {
                _objProjectManagement.ActivityStatusID = null;
            }
            else
            {
                _objProjectManagement.ActivityStatusID = Convert.ToInt32(ddlActivityStatus.SelectedValue);
            }

            _objProjectManagement.EntryUserId = current.UserId;
            _objProjectManagementController = new ProjectManagementController();
            _objProjectManagementController.SaveProgressRecord(_connectionString, _objProjectManagement);
        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }
    protected void btnDoneLog_Click(object sender, EventArgs e)
    {
        this.ControlPanelVisible(PanelTaksList, PanelNewEntry, PanelProgress, PanelActivityLog);
    }
}