using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_SelfService_frmExpensesClaim : System.Web.UI.Page
{
    string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    private ProcessAdvancePayment _objProcessAdvancePayment;
    private ExpensesClaimController _objExpensesClaimController;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            clsStatic.CheckUserAuthentication(true);
            if (!Page.IsPostBack)
            {
                LoadEmployeeInformation(Session[StaticData.sessionUserId].ToString());
                grdExpensesRecord.DataSource = ViewState["expensesRecord"];
                grdExpensesRecord.DataBind();

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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            BindExpensesRecord();
            ClearControl();

        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }

    private void BindExpensesRecord()
    {
        try
        {
            _objProcessAdvancePayment = new ProcessAdvancePayment();
            _objProcessAdvancePayment.DateClaim = Convert.ToDateTime(txtDate.Text);
            _objProcessAdvancePayment.ExpenditureArea = txtJobReferenceorDescription.Text == string.Empty ? null : txtJobReferenceorDescription.Text;
            _objProcessAdvancePayment.AmountCost = txtAmount.Text == string.Empty ? 0 : Convert.ToDecimal(txtAmount.Text);
            if (btnAdd.Text == "Update")
            {
                UpdateExpensesRecord();
            }
            ExpensesRecordBindGrid(_objProcessAdvancePayment);

        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }

    private void UpdateExpensesRecord()
    {
        try
        {
            if ((DataTable)ViewState["expensesRecord"] != null)
            {
                var indexForDelete = Convert.ToInt32(Session["indexExpensesRecord"].ToString());
                var dt = (DataTable)ViewState["expensesRecord"];
                dt.Rows[indexForDelete].Delete();
                dt.AcceptChanges();
                ViewState["expensesRecord"] = dt;
            }

        }
        catch (Exception msgException)
        {
            
            throw msgException;
        }
    }

    private void ExpensesRecordBindGrid(ProcessAdvancePayment objProcessAdvancePayment)
    {
        try
        {
            var dt = new DataTable();
            DataRow dr;
            dt.Columns.Add(new DataColumn("txtDate", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("txtJobReferenceorDescription", typeof(String)));
            dt.Columns.Add(new DataColumn("txtAmount", typeof(decimal)));

            if (ViewState["expensesRecord"] != null)
            {
                var dtTable = (DataTable)ViewState["expensesRecord"];
                var count = dtTable.Rows.Count;
                for (var i = 0; i < count + 1; i++)
                {
                    dt = (DataTable)ViewState["expensesRecord"];
                    if (dt.Rows.Count <= 0) continue;
                    dr = dt.NewRow();
                    dr[0] = dt.Rows[0][0].ToString();
                }
                dr = dt.NewRow();
                dr[0] = objProcessAdvancePayment.DateClaim;
                dr[1] = objProcessAdvancePayment.ExpenditureArea;
                dr[2] = objProcessAdvancePayment.AmountCost;

                dt.Rows.Add(dr);

            }
            else
            {
                dr = dt.NewRow();
                dr[0] = objProcessAdvancePayment.DateClaim;
                dr[1] = objProcessAdvancePayment.ExpenditureArea;
                dr[2] = objProcessAdvancePayment.AmountCost;

                dt.Rows.Add(dr);
            }
            if (ViewState["expensesRecord"] != null)
            {
                grdExpensesRecord.DataSource = ViewState["expensesRecord"];
                grdExpensesRecord.DataBind();
            }
            else
            {
                grdExpensesRecord.DataSource = dt;
                grdExpensesRecord.DataBind();

            }
            ViewState["expensesRecord"] = dt;

        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }
    private void ClearControl()
    {
        txtDate.Text = string.Empty;
        txtJobReferenceorDescription.Text = string.Empty;
        txtAmount.Text = string.Empty;
        btnAdd.Text = "Add";
    }
    private void ClearAllControl()
    {
        ClearControl();
        grdExpensesRecord.DataSource = null;
        grdExpensesRecord.DataBind();
        ViewState["expensesRecord"] = null;
        txtAdvanceReceived.Text = string.Empty;
        txtNetClaim.Text = string.Empty;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAllControl();
    }
    protected void grdExpensesRecord_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());

        if (e.CommandName.Equals("Select"))
        {
            try
            {
                Session["indexExpensesRecord"] = selectedIndex;
                var lblDate = ((Label)grdExpensesRecord.Rows[selectedIndex].FindControl("lblDate")).Text;
                var lblDescription = ((Label)grdExpensesRecord.Rows[selectedIndex].FindControl("lblDescription")).Text;
                var lblAmount = ((Label)grdExpensesRecord.Rows[selectedIndex].FindControl("lblAmount")).Text;
                txtDate.Text = lblDate;
                txtJobReferenceorDescription.Text = lblDescription;
                txtAmount.Text = lblAmount;
                btnAdd.Text = "Update";

            }
            catch (Exception msgException)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "MessageBox", "alert(' " + msgException.Message + " ');", true);
            }

        }

        if (!e.CommandName.Equals("Delete")) return;
        var dt = (DataTable)ViewState["expensesRecord"];
        dt.Rows[selectedIndex].Delete();
        dt.AcceptChanges();
        ViewState["expensesRecord"] = dt;
        if (ViewState["expensesRecord"] == null) return;
        grdExpensesRecord.DataSource = ViewState["expensesRecord"];
        grdExpensesRecord.DataBind();
    }
    protected void grdExpensesRecord_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        try
        {
            ApplyForExpensesBill();
            ClearAllControl();

        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }

    private void ApplyForExpensesBill()
    {
        try
        {
            LeaveProcess lvp = new LeaveProcess();
            string transactionNo = lvp.GetTransactionNo(_connectionString);
            bool checkAdvanceReceived = true;
            foreach (GridViewRow dtRow in grdExpensesRecord.Rows)
            {
                _objProcessAdvancePayment = new ProcessAdvancePayment();
                Label lblSl = dtRow.FindControl("lblSl") as Label;
                Label lblDate = dtRow.FindControl("lblDate") as Label;
                Label lblDescription = dtRow.FindControl("lblDescription") as Label;
                Label lblAmount = dtRow.FindControl("lblAmount") as Label;


                _objProcessAdvancePayment.TransactionNoLineNo = Convert.ToInt32(lblSl.Text);
                _objProcessAdvancePayment.DateClaim = Convert.ToDateTime(lblDate.Text);
                _objProcessAdvancePayment.ExpenditureArea = lblDescription.Text == string.Empty ? null : lblDescription.Text;
                _objProcessAdvancePayment.AmountCost = lblAmount.Text == string.Empty ? 0 : Convert.ToDecimal(lblAmount.Text);

                _objProcessAdvancePayment.EntryUser = current.UserId;
                _objProcessAdvancePayment.ApplicantCode = lblId.Text;
                _objProcessAdvancePayment.TransactionNo = transactionNo;
                _objProcessAdvancePayment.ProcessCode = "P005";
                _objProcessAdvancePayment.ProcessFlowCode = "5";
                _objProcessAdvancePayment.ProcessLevelCode = 8;
                _objProcessAdvancePayment.ProcessTypeCode = "EC";
                _objExpensesClaimController = new ExpensesClaimController();
                _objExpensesClaimController.ApplyForExpensesPayment(_connectionString, _objProcessAdvancePayment);

                
                if (checkAdvanceReceived == true)
                {
                    _objProcessAdvancePayment.AdvanceReceived = txtAdvanceReceived.Text == string.Empty ? 0 : Convert.ToDecimal(txtAdvanceReceived.Text);
                    _objProcessAdvancePayment.NetClaim = txtNetClaim.Text == string.Empty ? 0 : Convert.ToDecimal(txtNetClaim.Text);
                    _objExpensesClaimController.SaveAdvanceReceivedAmount(_connectionString, _objProcessAdvancePayment);
                    checkAdvanceReceived = false;
                }
                

            }

        }
        catch (Exception msgException)
        {
            
            throw msgException;
        }
    }
    private decimal totalAmount = 0;
    protected void grdExpensesRecord_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var lblAmount = ((Label)e.Row.FindControl("lblAmount")).Text == string.Empty ? 0 : Convert.ToDecimal(((Label)e.Row.FindControl("lblAmount")).Text);
            totalAmount += lblAmount;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            ((Label)e.Row.FindControl("lblTotalAmount")).Text = totalAmount.ToString();
        }
    }
}