using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using LibraryDAL;
using LibraryDAL.dsInventory;
using LibraryDAL.dsInventory.dsInventoryMasterTableAdapters;

public partial class modules_FixedAsset_Report_frmDepreciationReportViewer : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                show_report();
            }
            catch (Exception)
            {

                //throw;
            }
        }
        show_report();
    }

    private void show_report()
    {
        CrystalReportViewer1.ReportSource = null;

        string ConnectionString;
        string[] ff, ss;
        string item = "";
        string store="";

        SqlConnection conn = new SqlConnection();
        ConnectionInfo crConnInfo = new ConnectionInfo();
        ReportDocument cryRpt = new ReportDocument();
        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        ConnectionInfo crConnectionInfo = new ConnectionInfo();
        Tables CrTables;

       // string constr = "Data Source=192.168.10.110;Initial Catalog=L3T;User ID=sa;Password=";

        string constr = System.Configuration.ConfigurationManager.AppSettings["L3TConnectionString"].ToString();
        ConnectionString = constr;
        conn.ConnectionString = ConnectionString;
        conn.Open();

        ff = constr.Split('=');

        ss = ff[1].Split(';');
        crConnInfo.ServerName = ss[0];

        ss = ff[2].Split(';');
        crConnInfo.DatabaseName = ss[0];

        ss = ff[3].Split(';');
        crConnInfo.UserID = ss[0];

        ss = ff[4].Split(';');
        crConnInfo.Password = ss[0];

        crConnInfo.ServerName = crConnInfo.ServerName;
        crConnInfo.DatabaseName = crConnInfo.DatabaseName;
        crConnInfo.UserID = crConnInfo.UserID;
        crConnInfo.Password = crConnInfo.Password;


        string qry = Request.QueryString["qry"];

        var rptfile = qry.Split(',')[0];
        var selection = qry.Split(',')[1];
        var parameter = qry.Split(',')[2];
        
        cryRpt.Load(Server.MapPath(rptfile));
        

        CrTables = cryRpt.Database.Tables;
        foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
        {
            crtableLogoninfo = CrTable.LogOnInfo;
            crtableLogoninfo.ConnectionInfo = crConnInfo;
            CrTable.ApplyLogOnInfo(crtableLogoninfo);
        }

        //foreach (FormulaFieldDefinition thisFormulaField in cryRpt.DataDefinition.FormulaFields)
        //{
        //    if (thisFormulaField.FormulaName == "{@fFromDate}" || thisFormulaField.FormulaName == "{@DFrm}" || thisFormulaField.FormulaName == "{@fDate}" || thisFormulaField.FormulaName == "{@datefrom}")
        //    {
        //        thisFormulaField.Text = "'" + Session["RptDateFrom"].ToString() + "'";
        //    }
        //    if (thisFormulaField.FormulaName == "{@fToDate}" || thisFormulaField.FormulaName == "{@DTo}" || thisFormulaField.FormulaName == "{@dateto}")
        //    {
        //        thisFormulaField.Text = "'" + Session["RptDateTo"].ToString() + "'";
        //    }
        //}

        foreach (FormulaFieldDefinition thisFormulaField in cryRpt.DataDefinition.FormulaFields)
        {
            if (thisFormulaField.FormulaName == "{@fFromDate}" || thisFormulaField.FormulaName == "{@DFrm}" || thisFormulaField.FormulaName == "{@fDate}" || thisFormulaField.FormulaName == "{@datefrom}")
            {
                thisFormulaField.Text = "'" + parameter.ToString() + "'";
            }
            if (thisFormulaField.FormulaName == "{@fToDate}" || thisFormulaField.FormulaName == "{@DTo}" || thisFormulaField.FormulaName == "{@dateto}")
            {
                thisFormulaField.Text = "'" + Session["RptDateTo"].ToString() + "'";
            }
        }         

        if (selection != "")
        {
            selection = "{ViewDepreCiationData.ItemCurrentLine}='" + selection + "'";
            CrystalReportViewer1.SelectionFormula = selection;
        }
        CrystalReportViewer1.ReportSource = cryRpt;
        CrystalReportViewer1.RefreshReport();
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/modules/FixedAsset/Transactiondetails/frmDepreciationReport.aspx");
    }
}
