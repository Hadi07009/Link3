using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Payroll_frmEmployeeInvest : System.Web.UI.Page
{
    readonly string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"];
    EmployeeInvestAmountController _objEmpInvestAmountController;
    EmployeeInvestAmount _objEmpInvestAmount;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                LoadYear();
                txtEmployeeSearch_AutoCompleteExtender.ContextKey = _connectionString;
                GetInvestRecord();

            }

        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message); 
        }

    }

    private void LoadYear()
    {
        _objEmpInvestAmountController = new EmployeeInvestAmountController();
        ddlYear.DataSource = _objEmpInvestAmountController.GenerateASetOfYear();
        ddlYear.DataValueField = "yearId";
        ddlYear.DataTextField = "yearName";
        ddlYear.DataBind();
        ddlYear.SelectedValue = DateTime.Now.Year.ToString();
    }
    protected void txtEmployeeSearch_TextChanged(object sender, EventArgs e)
    {
        if (txtEmployeeSearch.Text != string.Empty)
        {
            txtEmployeeSearch.Text = txtEmployeeSearch.Text.Split(':')[0].Trim() == "" ? "" : txtEmployeeSearch.Text.Split(':')[0].Trim();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            ActionEmployeeInvestAmount();
            GetInvestRecord();
            ClearAllControl();
            MessageBox1.ShowSuccess("Data saved Successful");
        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }

    private void ActionEmployeeInvestAmount()
    {
        _objEmpInvestAmountController = new EmployeeInvestAmountController();
        _objEmpInvestAmount = new EmployeeInvestAmount();
        _objEmpInvestAmount.FinancialYear = ddlYear.SelectedValue;
        _objEmpInvestAmount.EmployeeCode = txtEmployeeSearch.Text;
        _objEmpInvestAmount.InvestAmount = Convert.ToDecimal( txtInvestAmount.Text);
        _objEmpInvestAmount.InvestDescription = txtDescription.Text;
        
        if (btnSave.Text == "Save")
        {
            _objEmpInvestAmountController.Save(_connectionString,_objEmpInvestAmount);
            
            
        }
        else
        {
            _objEmpInvestAmount.ReferenceNumber = Session["refeNumber"].ToString(); 
            _objEmpInvestAmountController.Update(_connectionString,_objEmpInvestAmount);

        }
    }

    private void GetInvestRecord()
    {
        _objEmpInvestAmountController = new EmployeeInvestAmountController();
        var dtInvestRecord = _objEmpInvestAmountController.GetRecord(_connectionString);
        grdInvestRecord.DataSource = null;
        grdInvestRecord.DataBind();
        if (dtInvestRecord.Rows.Count > 0)
        {
            grdInvestRecord.DataSource = dtInvestRecord;
            grdInvestRecord.DataBind();
        }
    }
    protected void grdInvestRecord_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        var lblreferenceNumber = ((Label)grdInvestRecord.Rows[selectedIndex].FindControl("lblreferenceNumber")).Text;
        if (e.CommandName.Equals("Delete"))
        {
            try
            {
                _objEmpInvestAmount = new EmployeeInvestAmount();
                _objEmpInvestAmount.ReferenceNumber = lblreferenceNumber;
                _objEmpInvestAmountController = new EmployeeInvestAmountController();
                _objEmpInvestAmountController.Delete(_connectionString, _objEmpInvestAmount);
                GetInvestRecord();

            }
            catch (Exception msgException)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "MessageBox", "alert(' " + msgException.Message + " ');", true);
            }
        }
        else if (e.CommandName.Equals("Select"))
        {
            var lblfinancialYear = ((Label)grdInvestRecord.Rows[selectedIndex].FindControl("lblfinancialYear")).Text;
            var lblemployeeCode = ((Label)grdInvestRecord.Rows[selectedIndex].FindControl("lblemployeeCode")).Text;
            var lblinvestAmount = ((Label)grdInvestRecord.Rows[selectedIndex].FindControl("lblinvestAmount")).Text;
            var lblinvestDescription = ((Label)grdInvestRecord.Rows[selectedIndex].FindControl("lblinvestDescription")).Text;
            btnSave.Text = "Update";
            Session["refeNumber"] = lblreferenceNumber;
            ddlYear.SelectedValue = lblfinancialYear;
            txtEmployeeSearch.Text = lblemployeeCode;
            txtInvestAmount.Text = lblinvestAmount;
            txtDescription.Text = lblinvestDescription;
        }

    }
    protected void grdInvestRecord_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAllControl();
    }

    private void ClearAllControl()
    {
        txtEmployeeSearch.Text = string.Empty;
        txtInvestAmount.Text = string.Empty;
        txtDescription.Text = string.Empty;
        btnSave.Text = "Save";
    }
    protected void grdInvestRecord_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;
    }
}