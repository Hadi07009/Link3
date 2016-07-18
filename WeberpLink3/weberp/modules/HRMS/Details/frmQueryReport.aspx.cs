using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Details_frmQueryReport : System.Web.UI.Page
{
    readonly string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"];
    private QueryReportController _objQueryReportController;
    private QueryReport _objQueryReport;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                PanelForSelection.Visible = false;
                _objQueryReportController = new QueryReportController();
                ClsDropDownListController.LoadDropDownList(_connectionString, _objQueryReportController.SqlGetQuery(), ddlQueryType, "SourceName", "DataSource");
                ClsDropDownListController.LoadDropDownList(_connectionString, _objQueryReportController.SqlGetReportName(), ddlReport, "SQLNAME", "SQL");
                lstBoxField.BorderWidth = 1;
                lstBoxField.BorderColor = System.Drawing.Color.Gray;
                lstBoxSelectedField.BorderWidth = 1;
                lstBoxSelectedField.BorderColor = System.Drawing.Color.Gray;
                Session["selectedColumn"] = "";
                Session["selectedColumnRemove"] = "";
                btnExporttoExcel.Visible = false;
            }
            catch (Exception msgException)
            {

                MessageBox1.ShowError(msgException.Message);
            }

        }

    }
    protected void ddlQueryType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            _objQueryReportController = new QueryReportController();
            _objQueryReport = new QueryReport();
            _objQueryReport.QueryText = ddlQueryType.SelectedValue == "-1" ? null : ddlQueryType.SelectedItem.Text;
            var dtColumn = _objQueryReportController.GetColumn(_connectionString, _objQueryReport);
            lstBoxField.Items.Clear();
            lstBoxSelectedField.Items.Clear();
            PanelForSelection.Visible = false;
            foreach (DataRow row in dtColumn.Rows)
            {
                string dataFields = row.ItemArray[0].ToString();
                string[] dataField = dataFields.Split(',');
                for (int i = 0; i < dataField.Length; i++)
                {
                    dataField[i] = dataField[i].Trim();
                    lstBoxField.Items.Add(new ListItem(dataField[i].ToString(), dataField[i].ToString()));
                    lstBoxField.AppendDataBoundItems = true;
                }
                PanelForSelection.Visible = true;
            }
            lstBoxField.Rows = lstBoxField.Items.Count == 0 ? 1 : lstBoxField.Items.Count;
        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }


    protected void lstBoxField_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            for (var i = 0; i < lstBoxField.Items.Count; i++)
            {
                if (!lstBoxField.Items[i].Selected) continue;
                else
                {
                    Session["selectedColumn"] = lstBoxField.Items[i].ToString();
                }
            }
        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }
    protected void lstBoxSelectedField_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            for (var i = 0; i < lstBoxSelectedField.Items.Count; i++)
            {
                if (!lstBoxSelectedField.Items[i].Selected) continue;
                else
                {
                    Session["selectedColumnRemove"] = lstBoxSelectedField.Items[i].ToString();
                }
            }
        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }

    }
    protected void btnForwardAllColumn_Click(object sender, EventArgs e)
    {
        try
        {
            lstBoxSelectedField.Items.Clear();
            for (var i = 0; i < lstBoxField.Items.Count; i++)
            {
                lstBoxSelectedField.Items.Add(new ListItem(lstBoxField.Items[i].ToString(), lstBoxField.Items[i].ToString()));
                lstBoxSelectedField.AppendDataBoundItems = true;
            }

        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }
    protected void btnBackAllColumn_Click(object sender, EventArgs e)
    {
        try
        {
            lstBoxSelectedField.Items.Clear();

        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            SaveQueryReport();
            ClsDropDownListController.LoadDropDownList(_connectionString, _objQueryReportController.SqlGetReportName(), ddlReport, "SQLNAME", "SQL");
            MessageBox1.ShowSuccess("Data Saved Successfully");
        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }

    private void SaveQueryReport()
    {
        _objQueryReportController = new QueryReportController();
        _objQueryReport = new QueryReport();
        _objQueryReport.ReportName = txtReportName.Text == string.Empty ? null : txtReportName.Text;
        _objQueryReport.QueryText = ddlQueryType.SelectedValue == "-1" ? null : ddlQueryType.SelectedValue;
        _objQueryReport.EntryUser = current.UserId;
        string selectedColumns = null;
        foreach (ListItem columnValue in lstBoxSelectedField.Items)
        {
            if (columnValue.Value == null) continue;
            else
            {
                if (selectedColumns == null)
                {
                    selectedColumns = "SELECT " + columnValue.Value;

                }
                else
                {
                    selectedColumns = selectedColumns + "," + columnValue.Value;
                }
            }
        }
        if (selectedColumns != null)
        {
            selectedColumns = selectedColumns + " " + _objQueryReport.QueryText.Replace("'", "''");

        }
        _objQueryReport.SelectedColumn = selectedColumns;
        _objQueryReportController.Save(_connectionString, _objQueryReport);
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ShowQueryData();
        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }

    private void ShowQueryData()
    {
        _objQueryReportController = new QueryReportController();
        _objQueryReport = new QueryReport();
        _objQueryReport.ReportName = ddlReport.SelectedValue == "-1" ? null : ddlReport.SelectedValue;
        var dtQueryData = _objQueryReportController.GetQueryData(_connectionString, _objQueryReport);
        grdGetQueryData.DataSource = null;
        grdGetQueryData.DataBind();
        btnExporttoExcel.Visible = false;
        if (dtQueryData.Rows.Count > 0)
        {
            grdGetQueryData.DataSource = dtQueryData;
            grdGetQueryData.DataBind();
            btnExporttoExcel.Visible = true;
        }
    }
    protected void btnForwardColumn_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["selectedColumn"].ToString() != "")
            {
                lstBoxSelectedField.Items.Remove(Session["selectedColumn"].ToString());
                lstBoxSelectedField.Items.Add(new ListItem(Session["selectedColumn"].ToString(), Session["selectedColumn"].ToString()));
                lstBoxSelectedField.AppendDataBoundItems = true;
            }
        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }
    protected void btnBackColumn_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["selectedColumnRemove"].ToString() != "")
            {
                lstBoxSelectedField.Items.Remove(Session["selectedColumnRemove"].ToString());
            }
        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }
    protected void btnExporttoExcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (grdGetQueryData.Rows.Count != 0)
            {
                string type = ddlReport.SelectedItem.Text + ".xls";
                ExportGridToExcel.Export(type, grdGetQueryData);
            }
        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }
}