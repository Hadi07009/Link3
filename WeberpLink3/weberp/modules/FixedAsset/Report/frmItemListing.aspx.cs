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

public partial class modules_FixedAsset_Report_frmItemListing : System.Web.UI.Page
{
    public ReportDocument cryRpt = new ReportDocument();
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
     
        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        ConnectionInfo crConnectionInfo = new ConnectionInfo();
        Tables CrTables;

        string constr = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
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

        string rptType = Request.QueryString["rptType"] == null ? "" : Request.QueryString["rptType"].ToString();

        if (rptType=="I")        
            cryRpt.Load(Server.MapPath("Inv_Item_Details.rpt"));

        if (rptType == "G")
            cryRpt.Load(Server.MapPath("Inv_Item_Grp_Details.rpt"));

        CrTables = cryRpt.Database.Tables;
        foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
        {
            crtableLogoninfo = CrTable.LogOnInfo;
            crtableLogoninfo.ConnectionInfo = crConnInfo;
            CrTable.ApplyLogOnInfo(crtableLogoninfo);
        }

        foreach (FormulaFieldDefinition thisFormulaField in cryRpt.DataDefinition.FormulaFields)
        {
            if (thisFormulaField.FormulaName == "{@fFromDate}" || thisFormulaField.FormulaName == "{@DFrm}" || thisFormulaField.FormulaName == "{@fDate}" || thisFormulaField.FormulaName == "{@datefrom}")
            {
                thisFormulaField.Text = "";
            }
            if (thisFormulaField.FormulaName == "{@fToDate}" || thisFormulaField.FormulaName == "{@DTo}" || thisFormulaField.FormulaName == "{@dateto}")
            {
                thisFormulaField.Text = "";
            }
        }                
        CrystalReportViewer1.ReportSource = cryRpt;
        CrystalReportViewer1.RefreshReport();
    }
    protected void CrystalReportViewer1_Unload(object sender, EventArgs e)
    {
        cryRpt.Close();
        cryRpt.Dispose();
    }
}
