using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using LibraryDAL;
using LibraryDAL.dsInventory.dsInventoryMasterTableAdapters;
using LibraryDAL.dsInventory.dsInventoryTransactionTableAdapters;

public partial class modules_FixedAsset_TransactionDetails_frmFixedAssetsAcquisitionReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtDate1.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDate2.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txtfromdate1.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate1.Text = DateTime.Now.ToString("dd/MM/yyyy");

            // LoadInitGrid();
        }
    }    
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
    }
    protected void btnViewall_Click(object sender, EventArgs e)
    {
    }
    protected void btnPrintFromTo_Click(object sender, EventArgs e)
    {             
        string ConnectionStr = System.Configuration.ConfigurationManager.AppSettings["L3TConnectionString"].ToString();
        string sql = "";
        DateTime startdate, enddate;
        try
        {

            startdate = Convert.ToDateTime(txtDate1.Text);
            enddate = Convert.ToDateTime(txtDate2.Text);
            DataProcess.ExecuteQuery(ConnectionStr, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ViewFixedAssetAcquisition]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[ViewFixedAssetAcquisition]");

            sql = "Create view [dbo].[ViewFixedAssetAcquisition] as "
              + " select a.itm_det_date,c.Itm_Det_desc,a.TrackingInfo,a.ItemQty,a.ItemRate,a.ItemQty*a.ItemRate as amt,a.itm_det_ref,d.par_adr_name,e.Fxd_Acc_code ,f.Gl_Coa_Name,e.Fxd_Second_Grp "
              + " from FAS_InMa_Itm_Serial a inner join FAS_InTr_Trn_Hdr b on a.itm_det_ref=b.Trn_Hdr_Ref"
              + " inner join InMa_Itm_Det c on a.itm_det_icode=c.Itm_Det_Icode Left outer join SaMa_Par_Adr d on b.Trn_Hdr_Acode=d.Par_Adr_Code"
              + " inner join FAS_FixedAssetSetUpNew e on e.TrackingInfo=a.TrackingInfo left outer join budg f on e.Fxd_Acc_code=f.Gl_Coa_Code"
              + " where a.itm_det_date between CONVERT(Datetime,'" + startdate + "',103) and CONVERT(Datetime,'" + enddate + "',103)";



            DataProcess.ExecuteQuery(ConnectionStr, sql);



            string ReportName = "FixedAssetAcquisitionReport.rpt";
            string selectionformula = "";
            string parameter = "Date from " + txtDate1.Text + " To " + txtDate2.Text;
            string qry = ReportName + "," + selectionformula + "," + parameter;

            Response.Redirect("~/modules/FixedAsset/Report/frmDepreciationReportViewer.aspx?qry=" + qry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnItemwisedet_Click(object sender, EventArgs e)
    {
        //string ConnectionStr = "Data Source=192.168.10.133;Initial Catalog=L3T;User ID=sa;Password=";
        string ConnectionStr = System.Configuration.ConfigurationManager.AppSettings["L3TConnectionString"].ToString();
        string sql = "";
        DateTime startdate, enddate;

        try
        {

            startdate = Convert.ToDateTime(txtfromdate1.Text);
            enddate = Convert.ToDateTime(txtToDate1.Text);
            DataProcess.ExecuteQuery(ConnectionStr, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ViewFixedAssetAcquisition]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[ViewFixedAssetAcquisition]");

            sql = "Create view [dbo].[ViewFixedAssetAcquisition] as "
              + " select a.itm_det_date,c.Itm_Det_desc,a.TrackingInfo,a.ItemQty,a.ItemRate,a.ItemQty*a.ItemRate as amt,a.itm_det_ref,d.par_adr_name,e.Fxd_Acc_code ,f.Gl_Coa_Name,e.Fxd_Second_Grp "
              + " from FAS_InMa_Itm_Serial a inner join FAS_InTr_Trn_Hdr b on a.itm_det_ref=b.Trn_Hdr_Ref"
              + " inner join InMa_Itm_Det c on a.itm_det_icode=c.Itm_Det_Icode left outer join SaMa_Par_Adr d on b.Trn_Hdr_Acode=d.Par_Adr_Code"
              + " inner join FAS_FixedAssetSetUpNew e on e.TrackingInfo=a.TrackingInfo left outer join budg f on e.Fxd_Acc_code=f.Gl_Coa_Code"
              + " where a.itm_det_date between CONVERT(Datetime,'" + startdate + "',103) and CONVERT(Datetime,'" + enddate + "',103)";
            
            DataProcess.ExecuteQuery(ConnectionStr, sql);
            
            string ReportName = "FixedAssetsAcquisitionSummeryReport.rpt";
            string selectionformula = "";
            string parameter = "Date from " + txtfromdate1.Text + " To " + txtToDate1.Text;
            string qry = ReportName + "," + selectionformula + "," + parameter;

            Response.Redirect("~/modules/FixedAsset/Report/frmDepreciationReportViewer.aspx?qry=" + qry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private int returnperiod(DateTime dt)
    {
        int prd = 0;
        int monthno = Convert.ToDateTime(dt).Month;
        if (monthno == 9) //July-sep 1st quarter
        { prd = 1; }
        else if (monthno == 12)  //Oct-Dec 2nd qarter
        { prd = 2; }
        else if (monthno == 3)//Oct-Dec 3rd qarter
        { prd = 3; }
        else if (monthno == 6) //Oct-Dec 4th qarter
        { prd = 4; }

        return prd;
    }
    
}
