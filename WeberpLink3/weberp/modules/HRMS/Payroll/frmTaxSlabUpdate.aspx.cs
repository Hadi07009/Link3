using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class modules_HRMS_Payroll_frmTaxSlabUpdate : System.Web.UI.Page
{
    readonly string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"];
    TaxSlabUpdate _objTaxSlabUpdate;
    TaxSlabUpdateController _objTaxSlabUpdateController;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                LoadYear();
            }

        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }

    private void LoadYear()
    {
        ddlYear.DataSource = GenerateASetOfYear();
        ddlYear.DataValueField = "yearId";
        ddlYear.DataTextField = "yearName";
        ddlYear.DataBind();
        ddlYear.SelectedValue = DateTime.Now.Year.ToString();
    }

    private DataTable GenerateASetOfYear()
    {
        DataTable dtYear = new DataTable();
        dtYear.Columns.Add("yearId", typeof(string));
        dtYear.Columns.Add("yearName", typeof(string));

        for (int i = DateTime.Now.Year - 1; i < DateTime.Now.Year + 10; i++)
        {
            DataRow drNew = dtYear.NewRow();
            drNew["yearId"] = i.ToString() + "-" + (i+1).ToString();
            drNew["yearName"] = i.ToString() + "-" + (i + 1).ToString();
            dtYear.Rows.Add(drNew);
        }
        return dtYear;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            ActionForTaxSlab();
            GetTaxSlabRecord();
            ClearAllControl();
            MessageBox1.ShowSuccess("Data saved Successful");

        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }

    private void ActionForTaxSlab()
    {
        _objTaxSlabUpdate = new TaxSlabUpdate();
        _objTaxSlabUpdate.FinancialYear = ddlYear.SelectedValue;
        _objTaxSlabUpdate.FromDate = Convert.ToDateTime(txtFromDate.Text);
        _objTaxSlabUpdate.Slab = Convert.ToInt32( txtSlab.Text);
        _objTaxSlabUpdate.SlabAmount = Convert.ToDecimal( txtSlabAmount.Text);
        _objTaxSlabUpdate.SlabType = ddlGender.SelectedValue == "-1" ? null : ddlGender.SelectedValue;
        _objTaxSlabUpdate.TaxRate = Convert.ToSingle( txtTaxRate.Text);
        _objTaxSlabUpdate.ToDate = Convert.ToDateTime(txtToDate.Text);
        _objTaxSlabUpdateController = new TaxSlabUpdateController();
        if (btnSave.Text == "Save")
        {
            _objTaxSlabUpdateController.Save(_connectionString, _objTaxSlabUpdate);
        }
        else
        {
            _objTaxSlabUpdate.ReferenceNumber = Session["refNumber"].ToString();
            _objTaxSlabUpdateController.Update(_connectionString,_objTaxSlabUpdate);
        }

    }
    private void GetTaxSlabRecord()
    {
        _objTaxSlabUpdateController = new TaxSlabUpdateController();
        _objTaxSlabUpdate = new TaxSlabUpdate();
        _objTaxSlabUpdate.SlabType = ddlGender.SelectedValue;
        var dtTaxSlab = _objTaxSlabUpdateController.GetTaxSlab(_connectionString,_objTaxSlabUpdate);
        grdSlabRecord.DataSource = null;
        grdSlabRecord.DataBind();
        if (dtTaxSlab.Rows.Count > 0)
        {
            grdSlabRecord.DataSource = dtTaxSlab;
            grdSlabRecord.DataBind();
            
        }

    }
    protected void ddlGender_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GetTaxSlabRecord();

        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }
    protected void grdSlabRecord_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        var lblreferenceNumber = ((Label)grdSlabRecord.Rows[selectedIndex].FindControl("lblreferenceNumber")).Text;
        if (e.CommandName.Equals("Delete"))
        {
            try
            {
                _objTaxSlabUpdateController = new TaxSlabUpdateController();
                _objTaxSlabUpdate = new TaxSlabUpdate();
                _objTaxSlabUpdate.ReferenceNumber = lblreferenceNumber;
                _objTaxSlabUpdateController.Delete(_connectionString, _objTaxSlabUpdate);
                GetTaxSlabRecord();

            }
            catch (Exception msgException)
            {

                ScriptManager.RegisterStartupScript( this,GetType(),"MessageBox","alert(' " + msgException.Message + " ');",true);
            }
        }
        else if (e.CommandName.Equals("Select"))
        {
            try
            {
                var lblfinancialYear = ((Label)grdSlabRecord.Rows[selectedIndex].FindControl("lblfinancialYear")).Text;
                var lblslabType = ((Label)grdSlabRecord.Rows[selectedIndex].FindControl("lblslabType")).Text;
                var lblfromDate = ((Label)grdSlabRecord.Rows[selectedIndex].FindControl("lblfromDate")).Text;
                var lbltoDate = ((Label)grdSlabRecord.Rows[selectedIndex].FindControl("lbltoDate")).Text;
                var lblslab = ((Label)grdSlabRecord.Rows[selectedIndex].FindControl("lblslab")).Text;
                var lblslabAmount = ((Label)grdSlabRecord.Rows[selectedIndex].FindControl("lblslabAmount")).Text;
                var lbltaxRate = ((Label)grdSlabRecord.Rows[selectedIndex].FindControl("lbltaxRate")).Text;
                ddlYear.SelectedValue = lblfinancialYear;
                ddlGender.SelectedValue = lblslabType;
                txtFromDate.Text = lblfromDate;
                txtToDate.Text = lbltoDate;
                txtSlab.Text = lblslab;
                txtSlabAmount.Text = lblslabAmount;
                txtTaxRate.Text = lbltaxRate;
                btnSave.Text = "Update";
                Session["refNumber"] = lblreferenceNumber;

            }
            catch (Exception msgException)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "MessageBox", "alert(' " + msgException.Message + " ');", true);
            }
        }
    }
    protected void grdSlabRecord_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    private void ClearAllControl()
    {
        btnSave.Text = "Save";
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        txtSlab.Text = string.Empty;
        txtSlabAmount.Text = string.Empty;
        txtTaxRate.Text = string.Empty;
 
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAllControl();

    }
    protected void grdSlabRecord_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;
        e.Row.Cells[2].Visible = false;
    }
}