using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_SelfService_frmConveyanceApplication : System.Web.UI.Page
{
    string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    private ConveyanceApplication _objConveyanceApplication;
    private ConveyanceApplicationController _objConveyanceApplicationController;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                LoadEmployeeInformation(Session[StaticData.sessionUserId].ToString());
                txtEmployeeSearch_AutoCompleteExtender.ContextKey = _connectionString;
                grdConveyanceRecord.DataSource = ViewState["conveyanceRecord"];
                grdConveyanceRecord.DataBind();
            }

        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }

    }


    private void LoadEmployeeInformation(string employeeCode)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = DataProcess.GetData(_connectionString, "select EmpID,EmpName,Dept,Designation,b.Emp_Mas_Join_Date as Joiningdate from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id where EmpID='" + employeeCode + "'");
            if (dt.Rows.Count > 0)
            {
                lblId.Text = dt.Rows[0]["EmpID"].ToString();
                lblName.Text = dt.Rows[0]["EmpName"].ToString();
                lbldept.Text = dt.Rows[0]["Dept"].ToString();
                lblDesignation.Text = dt.Rows[0]["Designation"].ToString();
                lblJoiningDate.Text = dt.Rows[0]["Joiningdate"].ToString().Substring(0, 10);
            }

        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }
    protected void txtEmployeeSearch_TextChanged(object sender, EventArgs e)
    {
        if (txtEmployeeSearch.Text != string.Empty)
        {
            txtEmployeeSearch.Text = txtEmployeeSearch.Text.Split(':')[0].Trim();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            BindConveyanceData();
            ClearControl();

        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }

    private void BindConveyanceData()
    {
        _objConveyanceApplication = new ConveyanceApplication();
        _objConveyanceApplication.DateClaim = Convert.ToDateTime(txtDate.Text);
        _objConveyanceApplication.PurposeofClaim = txtPurposeofJourney.Text;
        _objConveyanceApplication.FromLocation = txtFrom.Text;
        _objConveyanceApplication.ToLocation = txtTo.Text;
        _objConveyanceApplication.ModeofJourney = txtModeofJourney.Text;
        _objConveyanceApplication.AmountCost = txtAmount.Text == string.Empty ? 0 : Convert.ToDecimal(txtAmount.Text);
        _objConveyanceApplication.AssignedByEmployee = txtEmployeeSearch.Text;
        if (btnAdd.Text == "Update")
        {
            UpdateConveyanceRecord();
        }
        BindConveyanceGrid(_objConveyanceApplication);
    }
    private void BindConveyanceGrid(ConveyanceApplication objConveyanceApplication)
    {
        var dt = new DataTable();
        DataRow dr;
        dt.Columns.Add(new DataColumn("txtDate", typeof(DateTime)));
        dt.Columns.Add(new DataColumn("txtPurposeofJourney", typeof(String)));
        dt.Columns.Add(new DataColumn("txtFrom", typeof(String)));
        dt.Columns.Add(new DataColumn("txtTo", typeof(String)));
        dt.Columns.Add(new DataColumn("txtModeofJourney", typeof(String)));
        dt.Columns.Add(new DataColumn("txtAmount", typeof(decimal)));
        dt.Columns.Add(new DataColumn("txtEmployeeSearch", typeof(String)));

        if (ViewState["conveyanceRecord"] != null)
        {
            var dtTable = (DataTable)ViewState["conveyanceRecord"];
            var count = dtTable.Rows.Count;
            for (var i = 0; i < count + 1; i++)
            {
                dt = (DataTable)ViewState["conveyanceRecord"];
                if (dt.Rows.Count <= 0) continue;
                dr = dt.NewRow();
                dr[0] = dt.Rows[0][0].ToString();
            }
            dr = dt.NewRow();
            dr[0] = objConveyanceApplication.DateClaim;
            dr[1] = objConveyanceApplication.PurposeofClaim;
            dr[2] = objConveyanceApplication.FromLocation;
            dr[3] = objConveyanceApplication.ToLocation;
            dr[4] = objConveyanceApplication.ModeofJourney;
            dr[5] = objConveyanceApplication.AmountCost;
            dr[6] = objConveyanceApplication.AssignedByEmployee;

            dt.Rows.Add(dr);

        }
        else
        {
            dr = dt.NewRow();
            dr[0] = objConveyanceApplication.DateClaim;
            dr[1] = objConveyanceApplication.PurposeofClaim;
            dr[2] = objConveyanceApplication.FromLocation;
            dr[3] = objConveyanceApplication.ToLocation;
            dr[4] = objConveyanceApplication.ModeofJourney;
            dr[5] = objConveyanceApplication.AmountCost;
            dr[6] = objConveyanceApplication.AssignedByEmployee;

            dt.Rows.Add(dr);
        }
        if (ViewState["conveyanceRecord"] != null)
        {
            grdConveyanceRecord.DataSource = ViewState["conveyanceRecord"];
            grdConveyanceRecord.DataBind();
        }
        else
        {
            grdConveyanceRecord.DataSource = dt;
            grdConveyanceRecord.DataBind();

        }
        ViewState["conveyanceRecord"] = dt;
    }

    private void ClearControl()
    {
        txtDate.Text = string.Empty;
        txtPurposeofJourney.Text = string.Empty;
        txtFrom.Text = string.Empty;
        txtTo.Text = string.Empty;
        txtModeofJourney.Text = string.Empty;
        txtAmount.Text = string.Empty;
        txtEmployeeSearch.Text = string.Empty;
        btnAdd.Text = "Add";
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearControl();
        grdConveyanceRecord.DataSource = null;
        grdConveyanceRecord.DataBind();
        ViewState["conveyanceRecord"] = null;
    }
    protected void grdConveyanceRecord_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var con = Convert.ToInt32(e.CommandArgument.ToString());

        if (e.CommandName.Equals("Select"))
        {
            Session["indexConveyanceRecord"] = con;
            txtDate.Text = grdConveyanceRecord.Rows[con].Cells[1].Text == "&nbsp;" ? "" : grdConveyanceRecord.Rows[con].Cells[1].Text;
            txtPurposeofJourney.Text = grdConveyanceRecord.Rows[con].Cells[2].Text == "&nbsp;" ? "" : grdConveyanceRecord.Rows[con].Cells[2].Text;
            txtFrom.Text = grdConveyanceRecord.Rows[con].Cells[3].Text == "&nbsp;" ? "" : grdConveyanceRecord.Rows[con].Cells[3].Text;
            txtTo.Text = grdConveyanceRecord.Rows[con].Cells[4].Text == "&nbsp;" ? "" : grdConveyanceRecord.Rows[con].Cells[4].Text;
            txtModeofJourney.Text = grdConveyanceRecord.Rows[con].Cells[5].Text == "&nbsp;" ? "" : grdConveyanceRecord.Rows[con].Cells[5].Text;
            txtAmount.Text = grdConveyanceRecord.Rows[con].Cells[6].Text == "&nbsp;" ? "" : grdConveyanceRecord.Rows[con].Cells[6].Text;
            txtEmployeeSearch.Text = grdConveyanceRecord.Rows[con].Cells[7].Text == "&nbsp;" ? "" : grdConveyanceRecord.Rows[con].Cells[7].Text;
            btnAdd.Text = "Update";
        }

        if (!e.CommandName.Equals("Delete")) return;
        var dt = (DataTable)ViewState["conveyanceRecord"];
        dt.Rows[con].Delete();
        dt.AcceptChanges();
        ViewState["conveyanceRecord"] = dt;
        if (ViewState["conveyanceRecord"] == null) return;
        grdConveyanceRecord.DataSource = ViewState["conveyanceRecord"];
        grdConveyanceRecord.DataBind();

    }
    protected void grdConveyanceRecord_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    private decimal totalAmount = 0;
    protected void grdConveyanceRecord_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var amountValue = Convert.ToDecimal( e.Row.Cells[6].Text);
            totalAmount += amountValue;
 
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[5].Text = "Total Amount = ";
            e.Row.Cells[6].Text = totalAmount.ToString();
            e.Row.Font.Bold = true;
 
        }

    }
    private void UpdateConveyanceRecord()
    {
        if ((DataTable)ViewState["conveyanceRecord"] != null)
        {
            var indexForDelete = Convert.ToInt32(Session["indexConveyanceRecord"].ToString());
            var dt = (DataTable)ViewState["conveyanceRecord"];
            dt.Rows[indexForDelete].Delete();
            dt.AcceptChanges();
            ViewState["conveyanceRecord"] = dt;
        }
    }
    protected void btnApplyConveyance_Click(object sender, EventArgs e)
    {
        try
        {
            ApplyForConveyanceCost();
            ClearControl();
            grdConveyanceRecord.DataSource = null;
            grdConveyanceRecord.DataBind();
            ViewState["conveyanceRecord"] = null;

        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }

    private void ApplyForConveyanceCost()
    {
        try
        {
            LeaveProcess lvp = new LeaveProcess();
            string transactionNo = lvp.GetTransactionNo(_connectionString);
            foreach (GridViewRow dtRow in grdConveyanceRecord.Rows)
            {
                _objConveyanceApplication = new ConveyanceApplication();
                Label lblSl = dtRow.FindControl("lblSl") as Label;
                _objConveyanceApplication.TransactionNoLineNo = Convert.ToInt32(lblSl.Text);
                _objConveyanceApplication.DateClaim = Convert.ToDateTime(dtRow.Cells[1].Text.ToString());
                _objConveyanceApplication.PurposeofClaim = dtRow.Cells[2].Text.ToString() == "&nbsp;" ? "" : dtRow.Cells[2].Text.ToString();
                _objConveyanceApplication.FromLocation = dtRow.Cells[3].Text.ToString() == "&nbsp;" ? "" : dtRow.Cells[3].Text.ToString();
                _objConveyanceApplication.ToLocation = dtRow.Cells[4].Text.ToString() == "&nbsp;" ? "" : dtRow.Cells[4].Text.ToString();
                _objConveyanceApplication.ModeofJourney = dtRow.Cells[5].Text.ToString() == "&nbsp;" ? "" : dtRow.Cells[5].Text.ToString();
                _objConveyanceApplication.AmountCost = dtRow.Cells[6].Text.ToString() == "&nbsp;" ? 0 : Convert.ToDecimal(dtRow.Cells[6].Text.ToString());
                _objConveyanceApplication.AssignedByEmployee = dtRow.Cells[7].Text.ToString() == "&nbsp;" ? "" : dtRow.Cells[7].Text.ToString();
                _objConveyanceApplication.EntryUser = current.UserId;
                _objConveyanceApplication.ApplicantCode = lblId.Text;
                _objConveyanceApplication.TransactionNo = transactionNo;
                _objConveyanceApplication.ProcessCode = "P003";
                _objConveyanceApplication.ProcessFlowCode = "3";
                _objConveyanceApplication.ProcessLevelCode = 6;
                _objConveyanceApplication.ProcessTypeCode = "CM";
                _objConveyanceApplicationController = new ConveyanceApplicationController();
                _objConveyanceApplicationController.ApplyConveyance(_connectionString, _objConveyanceApplication);

            }

        }
        catch (Exception msgException)
        {
            
            throw msgException;
        }
    }
}