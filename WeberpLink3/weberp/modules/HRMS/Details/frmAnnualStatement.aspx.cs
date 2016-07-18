using ADODB;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using LibraryPF.dsMasterDataTableAdapters;

public partial class modules_HRMS_Details_frmAnnualStatement : System.Web.UI.Page
{
    string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    private const string Rnode = "K";
    private AnnualStatement _objAnnualStatement;
    private AnnualStatementController _objAnnualStatementController;
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        if (!IsPostBack)
        {
            LoadCompanyByUserPermission("ADM", Rnode);
            ClsDropDownListController.EnableDisableDropDownList(ddlcompany);
            txtemployeeSearch_AutoComplxtender.ContextKey = _connectionString;
            btnExporttoExcel.Visible = false;

        }
    }
    private void LoadCompanyByUserPermission(string userid, string nodeid)
    {
        DataTable dt = new DataTable();
        ListItem lst;
        ddlcompany.Items.Clear();
        dt = AccessPermission.GetCompanyByUserandNodeid(_connectionString, userid, nodeid);
        if (dt.Rows.Count > 0)
        {
            ddlcompany.Items.Insert(0, new ListItem("--- Please Select ---", "-1"));
            foreach (DataRow dr in dt.Rows)
            {
                lst = new ListItem();
                lst.Text = dr["COMP_CODE"].ToString() + ":" + dr["COMP_NAME"].ToString();
                lst.Value = dr["COMP_CODE"].ToString();
                ddlcompany.Items.Add(lst);
            }
        }
        else
        {
            ddlcompany.Items.Insert(0, new ListItem("--- No Data Found ---", "-1"));
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            ShowAnnualStatement();
        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }

    private void ShowAnnualStatement()
    {
        _objAnnualStatement = new AnnualStatement();
        _objAnnualStatement.FromDate = Convert.ToDateTime(txtFromDate.Text);
        _objAnnualStatement.ToDate = Convert.ToDateTime(txtToDate.Text);
        _objAnnualStatement.EmployeeCode = txtemployeeSearch.Text == string.Empty ? null : txtemployeeSearch.Text;
        _objAnnualStatement.UserID = current.UserId;
        _objAnnualStatementController = new AnnualStatementController();
        var dtAnnualStatement = _objAnnualStatementController.ShowRecord(_connectionString, _objAnnualStatement);
        grdAnnualStatement.DataSource = null;
        grdAnnualStatement.DataBind();
        btnExporttoExcel.Visible = false;
        if (dtAnnualStatement.Rows.Count > 0)
        {
            grdAnnualStatement.DataSource = dtAnnualStatement;
            grdAnnualStatement.DataBind();
            btnExporttoExcel.Visible = true;
            
        }
    }

    
    protected void txtemployeeSearch_TextChanged(object sender, EventArgs e)
    {
        if (txtemployeeSearch.Text != string.Empty)
        {
            txtemployeeSearch.Text =  txtemployeeSearch.Text.Split(':')[0].Trim();
        }
    }
    protected void btnExporttoExcel_Click(object sender, EventArgs e)
    {
        if (grdAnnualStatement.Rows.Count != 0)
        {
            const string type = "Annual Statement.xls";
            ExportGridToExcel.Export(type, grdAnnualStatement);
        }
        else
        {
            MessageBox1.ShowInfo("There is no data for Export ! ");
        }
    }
    
}