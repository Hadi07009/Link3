using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_admin_frmNodePermissionSummary : System.Web.UI.Page
{
    readonly string _connectionString = ConfigurationManager.AppSettings["UbasysConnectionString"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadNode();
            txtEmployeeSearch_AutoCompleteExtender.ContextKey = _connectionString;
            btnExportToExcel.Visible = false;

        }
    }

    private void LoadNode()
    {
        string sql = "SELECT DISTINCT NodeName FROM tblNodePerm WHERE CompanyCode='CEL' order by NodeName";
        ClsDropDownListController.LoadDropDownList(_connectionString, sql, ddlNode, "NodeName", "NodeName");
    }
    protected void ddlNode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string nodeName = ddlNode.SelectedValue;
            string sql = @"select b.UserId,b.UserEmpId,b.UserDepartment,b.UserName,b.UserDesignation from tblNodePerm a inner join tblUserInfo b on a.UserId=b.UserId
            where a.CompanyCode='CEL' and a.NodeName='" + nodeName + "' order by UserEmpId";
            LoadNodePremissionRecord(sql);
        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }

    private void LoadNodePremissionRecord(string sql)
    {
        var dtResult = DataProcess.GetData(_connectionString, sql);
        grdNodePermissionSummary.DataSource = null;
        grdNodePermissionSummary.DataBind();
        btnExportToExcel.Visible = false;
        if (dtResult.Rows.Count > 0)
        {
            grdNodePermissionSummary.DataSource = dtResult;
            grdNodePermissionSummary.DataBind();
            btnExportToExcel.Visible = true;
        }
    }
    protected void txtEmployeeSearch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtEmployeeSearch.Text != string.Empty)
            {
                txtEmployeeSearch.Text = txtEmployeeSearch.Text.Split(':')[0].Trim() == "" ? "" : txtEmployeeSearch.Text.Split(':')[0].Trim();
                string sql = @"  select a.NodeName from tblNodePerm a inner join tblUserInfo b on a.UserId=b.UserId
                where a.CompanyCode='CEL' and b.UserEmpId='" + txtEmployeeSearch.Text + "' order by NodeName";
                LoadNodePremissionRecord(sql);
            }

        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        if (grdNodePermissionSummary.Rows.Count != 0)
        {
            const string type = "NodePermissionSummary.xls";
            ExportGridToExcel.Export(type, grdNodePermissionSummary);
        }
        else
        {
            MessageBox1.ShowInfo("There is no data for Export ! ");
        }
    }
}