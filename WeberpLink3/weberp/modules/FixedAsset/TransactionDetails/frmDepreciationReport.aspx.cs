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

public partial class modules_FixedAsset_TransactionDetails_frmDepreciationReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            // LoadInitGrid();
        }
    }    
    protected void btnPrintduration_Click(object sender, EventArgs e)
    {

        ////
        //string ConnectionStr = "Data Source=192.168.10.133;Initial Catalog=L3T;User ID=sa;Password=";
        string ConnectionStr = System.Configuration.ConfigurationManager.AppSettings["L3TConnectionString"].ToString();
        string sql = "";
        DateTime startdate, enddate;
        try
        {
            //int prd = 0;
            //int monthno = Convert.ToDateTime(txtFromDate.Text).Month;
            //if (monthno == 9) //July-sep 1st quarter
            //{ prd = 1; }
            //else if (monthno == 12)  //Oct-Dec 2nd qarter
            //{ prd = 2; }
            //else if (monthno == 3)//Oct-Dec 3rd qarter
            //{ prd = 3; }
            //else if (monthno == 6) //Oct-Dec 4th qarter
            //{ prd = 4; }

            DateTime dt = Convert.ToDateTime(txtFromDate.Text);
            int prd = returnperiod(dt);
            
            if (prd==0)
            {
                Label1.Text = "Invalid date of quarter of a year";
                return;
            }
            else
            {
                Label1.Text = "";
            }

            startdate = Convert.ToDateTime(Convert.ToDateTime(txtFromDate.Text).AddMonths(prd * (-3))).AddDays(1);
            enddate = Convert.ToDateTime(txtFromDate.Text);

            DataProcess.ExecuteQuery(ConnectionStr, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ViewDepreCiationData]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[ViewDepreCiationData]");

            sql = "Create view [dbo].[ViewDepreCiationData] as "
                + " select itm_det_date as [Purchase Date],a.ItemCode as [Item Code],c.Itm_Det_desc,a.TrackingInfo as [Tracking No],"
                + " c.Fxd_Acc_code as [Account Code],DepreciationDate as [Dep. Date],b.Itemqty as Qty,b.ItemRate,a.ItemInitialValue as [Unit Price],"
                + " a.ItemDepreciationSL,a.ItemCurrentLine,c.Fxd_Second_Grp as [Group Code],c.Itm_Det_Acc_code,c.Dpre_Acc_code,c.Pro_Depre_Acc_code,c.Accu_Depre_Acc_Code,Dis_Acc_code,Revo_Acc_Code,Cash_Acc_Dis,"
                + " d.DepreciationRate,d.DepreciationMethodID from FAS_Item_Depreciation a "
                + " inner join FAS_InMa_Itm_Serial b on a.TrackingInfo=b.TrackingInfo"
                + " inner join FAS_FixedAssetSetUpNew c on  c.Itm_Grp_icode=a.ItemCode and c.TrackingInfo=a.TrackingInfo"
                + " inner join FAS_DepreciationSetup d on d.ItemGroupID=c.Fxd_Second_Grp"
                + " where a.DepreciationDate=CONVERT(Datetime,'" + enddate + "',103)";

            DataProcess.ExecuteQuery(ConnectionStr, sql);


            //View for depreciation of current period

            DataProcess.ExecuteQuery(ConnectionStr, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[view_opening_addition]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[view_opening_addition]");

            sql = "create view view_opening_addition as"
                + " select a.TrackingInfo,"
                + " sum(case when DepreciationDate<CONVERT(Datetime,'" + startdate + "',103) then Addition else 0 end) as openingdepreciation,"
                + " sum(case when DepreciationDate between CONVERT(Datetime,'" + startdate + "',103) and CONVERT(Datetime,'" + enddate + "',103) then Addition else 0 end) as additionduringthisperiod"
                + " from FAS_Item_Depreciation a group by a.TrackingInfo";

            DataProcess.ExecuteQuery(ConnectionStr, sql);

            /////        

            string ReportName = "ItemWiseDepreciationasondate.rpt";
            string selectionformula = "";
            string parameter = "As on " + txtFromDate.Text;            
            string qry = ReportName + "," + selectionformula + "," + parameter;

            Response.Redirect("~/modules/FixedAsset/Report/frmDepreciationReportViewer.aspx?qry=" + qry);
        }
        catch(Exception ex)
        {
            throw ex;
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
        //string ConnectionStr = "Data Source=192.168.10.133;Initial Catalog=L3T;User ID=sa;Password=";
        string ConnectionStr = System.Configuration.ConfigurationManager.AppSettings["L3TConnectionString"].ToString();
        string sql = "";
        DateTime startdate, enddate;

        try
        {

            startdate = Convert.ToDateTime(txtDate1.Text);
            enddate = Convert.ToDateTime(txtDate2.Text);
                        
            
            int prd = returnperiod(startdate);
            int prd1 = returnperiod(enddate);

            //if (prd1==0)
            //{
            //    Label2.Text = "Invalid Date duration";
            //    return;
            //}
            //else
            //{
            //    Label2.Text = "";
            //}

            DataProcess.ExecuteQuery(ConnectionStr, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ViewFilterData]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[ViewFilterData]");
            sql = "Create view [dbo].[ViewFilterData] as "
                + " select TrackingInfo, MAX(ItemDepreciationSL)as ItemDepreciationSL from FAS_Item_Depreciation where DepreciationPeriodFrom >=CONVERT(Datetime,'" + startdate + "',103) and DepreciationPeriodTo <=CONVERT(Datetime,'" + enddate + "',103)"
                + " and TrackingInfo in (select TrackingInfo from FAS_Item_Depreciation where DepreciationPeriodFrom >=CONVERT(Datetime,'" + startdate + "',103)  and DepreciationPeriodTo <= CONVERT(Datetime,'" + enddate + "',103) and ItemDepreciationSL =1)"
                + " group by TrackingInfo";

            DataProcess.ExecuteQuery(ConnectionStr, sql);
                        

            DataProcess.ExecuteQuery(ConnectionStr, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ViewDepreCiationData]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[ViewDepreCiationData]");

            sql = "Create view [dbo].[ViewDepreCiationData] as "
                + " select itm_det_date as [Purchase Date],a.ItemCode as [Item Code],c.Itm_Det_desc,a.TrackingInfo as [Tracking No],"
                + " c.Fxd_Acc_code as [Account Code],DepreciationDate as [Dep. Date],b.Itemqty as Qty,b.ItemRate,convert(decimal,a.ItemInitialValue) as [Unit Price],"
                + " a.ItemDepreciationSL,a.ItemCurrentLine,c.Fxd_Second_Grp as [Group Code],c.Itm_Det_Acc_code,c.Dpre_Acc_code,c.Pro_Depre_Acc_code,c.Accu_Depre_Acc_Code,Dis_Acc_code,Revo_Acc_Code,Cash_Acc_Dis,"
                + " d.DepreciationRate,d.DepreciationMethodID from FAS_Item_Depreciation a "
                + " inner join FAS_InMa_Itm_Serial b on a.TrackingInfo=b.TrackingInfo"
                + " inner join FAS_FixedAssetSetUpNew c on  c.Itm_Grp_icode=a.ItemCode and c.TrackingInfo=a.TrackingInfo"
                + " inner join FAS_DepreciationSetup d on d.ItemGroupID=c.Fxd_Second_Grp"
                + " inner join ViewFilterData e on e.TrackingInfo=a.TrackingInfo and e.ItemDepreciationSL=a.ItemDepreciationSL"
                + " where DepreciationPeriodFrom >=CONVERT(Datetime,'" + startdate + "',103) and DepreciationPeriodTo <=CONVERT(Datetime,'" + enddate + "',103)";

            DataProcess.ExecuteQuery(ConnectionStr, sql);

            DataProcess.ExecuteQuery(ConnectionStr, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[view_opening_addition]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[view_opening_addition]");

            sql = "create view view_opening_addition as"
                + " select a.TrackingInfo,"
                + " sum(case when DepreciationDate<CONVERT(Datetime,'" + startdate + "',103) then Addition else 0 end) as openingdepreciation,"
                + " sum(case when DepreciationDate between CONVERT(Datetime,'" + startdate + "',103) and CONVERT(Datetime,'" + enddate + "',103) then Addition else 0 end) as additionduringthisperiod"
                + " from FAS_Item_Depreciation a group by a.TrackingInfo";

            DataProcess.ExecuteQuery(ConnectionStr, sql);

            string ReportName = "";
            if (rdoSelection.SelectedItem.Value == "WithoutAddress")
            {
                ReportName = "ItemWiseDepreciationDateDuration.rpt";
            }
            else
            {
                ReportName = "ItemWiseDepreciationDateDurationWithAddress.rpt";
            }
                        
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
            //int prd = 0;
            //int monthno = Convert.ToDateTime(txtfromdate1.Text).Month;
            
            //if (monthno == 9) //July-sep 1st quarter
            //{ prd = 1; }
            //else if (monthno == 12)  //Oct-Dec 2nd qarter
            //{ prd = 2; }
            //else if (monthno == 3)//Oct-Dec 3rd qarter
            //{ prd = 3; }
            //else if (monthno == 6) //Oct-Dec 4th qarter
            //{ prd = 4; }

            DateTime dt = Convert.ToDateTime(txtfromdate1.Text);
            int prd = returnperiod(dt);
            
            if (prd == 0)
            {
                Label3.Text = "Invalid Date.....";
                return;
            }
            else
            {
                Label3.Text = "";
            }
            
            startdate = Convert.ToDateTime(Convert.ToDateTime(txtfromdate1.Text).AddMonths(prd * (-3))).AddDays(1);
            enddate = Convert.ToDateTime(txtfromdate1.Text);

            DataProcess.ExecuteQuery(ConnectionStr, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ViewDepreCiationData]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[ViewDepreCiationData]");

            sql = "Create view [dbo].[ViewDepreCiationData] as "
                + " select itm_det_date as [Purchase Date],a.ItemCode as [Item Code],c.Itm_Det_desc,a.TrackingInfo as [Tracking No],"
                + " c.Fxd_Acc_code as [Account Code],DepreciationDate as [Dep. Date],b.Itemqty as Qty,b.ItemRate,convert(decimal,a.ItemInitialValue) as [Unit Price],"
                + " a.ItemDepreciationSL,a.ItemCurrentLine,c.Fxd_Second_Grp as [Group Code],c.Itm_Det_Acc_code,c.Dpre_Acc_code,c.Pro_Depre_Acc_code,c.Accu_Depre_Acc_Code,Dis_Acc_code,Revo_Acc_Code,Cash_Acc_Dis,"
                + " d.DepreciationRate,d.DepreciationMethodID from FAS_Item_Depreciation a "
                + " inner join FAS_InMa_Itm_Serial b on a.TrackingInfo=b.TrackingInfo"
                + " inner join FAS_FixedAssetSetUpNew c on  c.Itm_Grp_icode=a.ItemCode and c.TrackingInfo=a.TrackingInfo"
                + " inner join FAS_DepreciationSetup d on d.ItemGroupID=c.Fxd_Second_Grp"
                + " where a.DepreciationDate=CONVERT(Datetime,'" + enddate + "',103)";

            DataProcess.ExecuteQuery(ConnectionStr, sql);

            DataProcess.ExecuteQuery(ConnectionStr, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[view_opening_addition]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[view_opening_addition]");

            sql = "create view view_opening_addition as"
                + " select a.TrackingInfo,"
                + " sum(case when DepreciationDate<CONVERT(Datetime,'" + startdate + "',103) then Addition else 0 end) as openingdepreciation,"
                + " sum(case when DepreciationDate between CONVERT(Datetime,'" + startdate + "',103) and CONVERT(Datetime,'" + enddate + "',103) then Addition else 0 end) as additionduringthisperiod"
                + " from FAS_Item_Depreciation a group by a.TrackingInfo";

            DataProcess.ExecuteQuery(ConnectionStr, sql);

            string ReportName = "ItemWiseReportWithDepreciation.rpt";
            string selectionformula = "";
            string parameter = "As on " + txtfromdate1.Text;
            string qry = ReportName + "," + selectionformula + "," + parameter;

            Response.Redirect("~/modules/FixedAsset/Report/frmDepreciationReportViewer.aspx?qry=" + qry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnItmwiseSummary_Click(object sender, EventArgs e)
    {
        //string ConnectionStr = "Data Source=192.168.10.133;Initial Catalog=L3T;User ID=sa;Password=";
        string ConnectionStr = System.Configuration.ConfigurationManager.AppSettings["L3TConnectionString"].ToString();
        string sql = "";
        DateTime startdate, enddate;
        try
        {
            //int prd = 0;
            //int monthno = Convert.ToDateTime(txtasondate1.Text).Month;
            //if (monthno == 9) //July-sep 1st quarter
            //{ prd = 1; }
            //else if (monthno == 12)  //Oct-Dec 2nd qarter
            //{ prd = 2; }
            //else if (monthno == 3)//Oct-Dec 3rd qarter
            //{ prd = 3; }
            //else if (monthno == 6) //Oct-Dec 4th qarter
            //{ prd = 4; }

            DateTime dt = Convert.ToDateTime(txtasondate1.Text);
            int prd = returnperiod(dt);

            if (prd == 0)
            {
                Label4.Text = "Invalid Date........";
                return;
            }
            else
            {
                Label4.Text = "";
            }            


            startdate = Convert.ToDateTime(Convert.ToDateTime(txtasondate1.Text).AddMonths(prd * (-3))).AddDays(1);
            enddate = Convert.ToDateTime(txtasondate1.Text);

            DataProcess.ExecuteQuery(ConnectionStr, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ViewDepreCiationData]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[ViewDepreCiationData]");

            sql = "Create view [dbo].[ViewDepreCiationData] as "
                + " select itm_det_date as [Purchase Date],a.ItemCode as [Item Code],c.Itm_Det_desc,a.TrackingInfo as [Tracking No],"
                + " c.Fxd_Acc_code as [Account Code],DepreciationDate as [Dep. Date],b.Itemqty as Qty,b.ItemRate,convert(decimal,a.ItemInitialValue) as [Unit Price],"
                + " a.ItemDepreciationSL,a.ItemCurrentLine,c.Fxd_Second_Grp as [Group Code],c.Itm_Det_Acc_code,c.Dpre_Acc_code,c.Pro_Depre_Acc_code,c.Accu_Depre_Acc_Code,Dis_Acc_code,Revo_Acc_Code,Cash_Acc_Dis,"
                + " d.DepreciationRate,d.DepreciationMethodID from FAS_Item_Depreciation a "
                + " inner join FAS_InMa_Itm_Serial b on a.TrackingInfo=b.TrackingInfo"
                + " inner join FAS_FixedAssetSetUpNew c on  c.Itm_Grp_icode=a.ItemCode and c.TrackingInfo=a.TrackingInfo"
                + " inner join FAS_DepreciationSetup d on d.ItemGroupID=c.Fxd_Second_Grp"
                + " where a.DepreciationDate=CONVERT(Datetime,'" + enddate + "',103)";

            DataProcess.ExecuteQuery(ConnectionStr, sql);

            DataProcess.ExecuteQuery(ConnectionStr, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[view_opening_addition]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[view_opening_addition]");

            sql = "create view view_opening_addition as"
                + " select a.TrackingInfo,"
                + " sum(case when DepreciationDate<CONVERT(Datetime,'" + startdate + "',103) then Addition else 0 end) as openingdepreciation,"
                + " sum(case when DepreciationDate between CONVERT(Datetime,'" + startdate + "',103) and CONVERT(Datetime,'" + enddate + "',103) then Addition else 0 end) as additionduringthisperiod"
                + " from FAS_Item_Depreciation a group by a.TrackingInfo";

            DataProcess.ExecuteQuery(ConnectionStr, sql);

            DataProcess.ExecuteQuery(ConnectionStr, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[view_Previous_purchase]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[view_Previous_purchase]");

            sql = "create view view_Previous_purchase as "
                + " select distinct a.TrackingInfo,b.Itemqty as Qty,b.ItemRate,convert(decimal,a.ItemInitialValue) as [Unit Price] from FAS_Item_Depreciation a"
                + " inner join FAS_InMa_Itm_Serial b on b.TrackingInfo=a.TrackingInfo "
                + " where b.itm_det_date<CONVERT(Datetime,'" + startdate + "',103)";

            DataProcess.ExecuteQuery(ConnectionStr, sql);


            string ReportName = "ItemWiseDepreciationSummaryAsondate.rpt";
            string selectionformula = "";
            string parameter = "As on " + txtasondate1.Text;
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
    protected void btnPreviewPeriodical_Click(object sender, EventArgs e)
    {
        string ConnectionStr = System.Configuration.ConfigurationManager.AppSettings["L3TConnectionString"].ToString();
        string sql = "";
        DateTime startdate, enddate;

        try
        {

            startdate = Convert.ToDateTime(txtfdt.Text);
            enddate = Convert.ToDateTime(txttdt.Text);


            int prd = returnperiod(startdate);
            int prd1 = returnperiod(enddate);

            if (prd1 == 0)
            {
                Label2.Text = "Invalid Date duration";
                return;
            }
            else
            {
                Label2.Text = "";
            }

            DataProcess.ExecuteQuery(ConnectionStr, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ViewFilterData]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[ViewFilterData]");
            sql = "Create view [dbo].[ViewFilterData] as "
                + " select TrackingInfo, MAX(ItemDepreciationSL)as ItemDepreciationSL from FAS_Item_Depreciation where DepreciationPeriodFrom >=CONVERT(Datetime,'" + startdate + "',103) and DepreciationPeriodTo <=CONVERT(Datetime,'" + enddate + "',103)"
                + " and TrackingInfo in (select TrackingInfo from FAS_Item_Depreciation where DepreciationPeriodFrom >=CONVERT(Datetime,'" + startdate + "',103)  and DepreciationPeriodTo <= CONVERT(Datetime,'" + enddate + "',103) and ItemDepreciationSL =1)"
                + " group by TrackingInfo";

            DataProcess.ExecuteQuery(ConnectionStr, sql);


            DataProcess.ExecuteQuery(ConnectionStr, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ViewDepreCiationData]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[ViewDepreCiationData]");

            sql = "Create view [dbo].[ViewDepreCiationData] as "
                + " select itm_det_date as [Purchase Date],a.ItemCode as [Item Code],c.Itm_Det_desc,a.TrackingInfo as [Tracking No],"
                + " c.Fxd_Acc_code as [Account Code],DepreciationDate as [Dep. Date],b.Itemqty as Qty,b.ItemRate,convert(decimal,a.ItemInitialValue) as [Unit Price],"
                + " a.ItemDepreciationSL,a.ItemCurrentLine,c.Fxd_Second_Grp as [Group Code],c.Itm_Det_Acc_code,c.Dpre_Acc_code,c.Pro_Depre_Acc_code,c.Accu_Depre_Acc_Code,Dis_Acc_code,Revo_Acc_Code,Cash_Acc_Dis,"
                + " d.DepreciationRate,d.DepreciationMethodID from FAS_Item_Depreciation a "
                + " inner join FAS_InMa_Itm_Serial b on a.TrackingInfo=b.TrackingInfo"
                + " inner join FAS_FixedAssetSetUpNew c on  c.Itm_Grp_icode=a.ItemCode and c.TrackingInfo=a.TrackingInfo"
                + " inner join FAS_DepreciationSetup d on d.ItemGroupID=c.Fxd_Second_Grp"
                + " where DepreciationDate=CONVERT(Datetime,'" + enddate + "',103)";

            DataProcess.ExecuteQuery(ConnectionStr, sql);

            DataProcess.ExecuteQuery(ConnectionStr, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[view_opening_addition]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[view_opening_addition]");

            sql = "create view view_opening_addition as"
                + " select a.TrackingInfo,"
                + " sum(case when DepreciationDate<CONVERT(Datetime,'" + startdate + "',103) then Addition else 0 end) as openingdepreciation,"
                + " sum(case when DepreciationDate between CONVERT(Datetime,'" + startdate + "',103) and CONVERT(Datetime,'" + enddate + "',103) then Addition else 0 end) as additionduringthisperiod"
                + " from FAS_Item_Depreciation a group by a.TrackingInfo";

            DataProcess.ExecuteQuery(ConnectionStr, sql);

            string ReportName = "ItemWiseDepreciationDateDurationOpeningAddition.rpt";
            string selectionformula = "";
            string parameter = "Date from " + txtDate1.Text + " To " + txtDate2.Text;
            string qry = ReportName + "," + selectionformula + "," + parameter;

            Response.Redirect("~/modules/FixedAsset/Report/frmDepreciationReportViewer.aspx?qry=" + qry);
        }
        catch (Exception ex)
        {
            //throw ex;
        }
    }
    protected void btnPrintReport_Click(object sender, EventArgs e)
    {


        string ConnectionStr = System.Configuration.ConfigurationManager.AppSettings["L3TConnectionString"].ToString();
        string sql = "";
        DateTime startdate, enddate;

        try
        {

            startdate = Convert.ToDateTime(txtFrmDate.Text);
            enddate = Convert.ToDateTime(txtTdate.Text);


            int prd = returnperiod(startdate);
            int prd1 = returnperiod(enddate);

            if (prd1 == 0)
            {
                Label2.Text = "Invalid Date duration";
                return;
            }
            else
            {
                Label2.Text = "";
            }

                     
            DataProcess.ExecuteQuery(ConnectionStr, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ViewALLTracking]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[ViewALLTracking]");
            sql = "Create view [dbo].[ViewALLTracking] as "
                + " select distinct a.TrackingInfo,b.Fxd_Second_Grp from FAS_Item_Depreciation a inner join FAS_FixedAssetSetUpNew b on a.TrackingInfo=b.TrackingInfo "
                + " and a.ItemCode=b.Itm_Grp_icode";

            DataProcess.ExecuteQuery(ConnectionStr, sql);

            

            DataProcess.ExecuteQuery(ConnectionStr, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[viewOpeningAddition]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[viewOpeningAddition]");

            sql = "create view viewOpeningAddition as"
                + " select a.TrackingInfo,"
                + " sum(case when DepreciationDate<CONVERT(Datetime,'" + startdate + "',103) then Addition else 0 end) as openingdepreciation,"
                + " sum(case when DepreciationDate between CONVERT(Datetime,'" + startdate + "',103) and CONVERT(Datetime,'" + enddate + "',103) then Addition else 0 end) as additionduringthisperiod"
                + " from FAS_Item_Depreciation a group by a.TrackingInfo";

            DataProcess.ExecuteQuery(ConnectionStr, sql);
            
            
            DataProcess.ExecuteQuery(ConnectionStr, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[view_Previous_purchase]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[view_Previous_purchase]");
            sql = "create view view_Previous_purchase as "
               + " select distinct a.TrackingInfo,b.Itemqty as Qty,b.ItemRate,convert(decimal,a.ItemInitialValue) as [Unit Price] from FAS_Item_Depreciation a"
               + " inner join FAS_InMa_Itm_Serial b on b.TrackingInfo=a.TrackingInfo "
               + " where b.itm_det_date<CONVERT(Datetime,'" + startdate + "',103)";

            DataProcess.ExecuteQuery(ConnectionStr, sql);

            
            DataProcess.ExecuteQuery(ConnectionStr, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ViewFilterData]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[ViewFilterData]");
            sql = "Create view [dbo].[ViewFilterData] as "
                + " select TrackingInfo, MAX(ItemDepreciationSL)as ItemDepreciationSL from FAS_Item_Depreciation where DepreciationPeriodFrom >=CONVERT(Datetime,'" + startdate + "',103) and DepreciationPeriodTo <=CONVERT(Datetime,'" + enddate + "',103)"
                + " and TrackingInfo in (select TrackingInfo from FAS_Item_Depreciation where DepreciationPeriodFrom >=CONVERT(Datetime,'" + startdate + "',103)  and DepreciationPeriodTo <= CONVERT(Datetime,'" + enddate + "',103) and ItemDepreciationSL =1)"
                + " group by TrackingInfo";

            DataProcess.ExecuteQuery(ConnectionStr, sql);

            
            DataProcess.ExecuteQuery(ConnectionStr, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ViewDepreCiationData]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[ViewDepreCiationData]");

            sql = "Create view [dbo].[ViewDepreCiationData] as "
                + " select itm_det_date as [Purchase Date],a.ItemCode as [Item Code],c.Itm_Det_desc,a.TrackingInfo as [Tracking No],"
                + " c.Fxd_Acc_code as [Account Code],DepreciationDate as [Dep. Date],b.Itemqty as Qty,b.ItemRate,convert(decimal,a.ItemInitialValue) as [Unit Price],"
                + " a.ItemDepreciationSL,a.ItemCurrentLine,c.Fxd_Second_Grp as [Group Code],c.Itm_Det_Acc_code,c.Dpre_Acc_code,c.Pro_Depre_Acc_code,c.Accu_Depre_Acc_Code,Dis_Acc_code,Revo_Acc_Code,Cash_Acc_Dis,"
                + " d.DepreciationRate,d.DepreciationMethodID from FAS_Item_Depreciation a "
                + " inner join FAS_InMa_Itm_Serial b on a.TrackingInfo=b.TrackingInfo"
                + " inner join FAS_FixedAssetSetUpNew c on  c.Itm_Grp_icode=a.ItemCode and c.TrackingInfo=a.TrackingInfo"
                + " inner join FAS_DepreciationSetup d on d.ItemGroupID=c.Fxd_Second_Grp"
                + " where DepreciationDate=CONVERT(Datetime,'" + enddate + "',103)";

            DataProcess.ExecuteQuery(ConnectionStr, sql);
                        
            DataProcess.ExecuteQuery(ConnectionStr, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[viewDepriciationCost]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[viewDepriciationCost]");
            sql = "create view viewDepriciationCost as"
                   + " Select a.TrackingInfo,a.ItemDepreciationSL,b.[Account Code],b.Accu_Depre_Acc_Code,b.Cash_Acc_Dis,b.[Dep. Date],b.DepreciationRate,b.Dis_Acc_code," 
                   + " b.Dpre_Acc_code,b.[Group Code],b.[Item Code],b.ItemCurrentLine,b.ItemRate,b.Itm_Det_Acc_code,b.Itm_Det_desc,b.Qty,b.[Tracking No],b.[Unit Price] " 
                   + " from ViewFilterData as a inner join ViewDepreCiationData as b on a.TrackingInfo=b.[Tracking No] and a.ItemDepreciationSL=b.ItemDepreciationSL";

            DataProcess.ExecuteQuery(ConnectionStr, sql);



            string ReportName = "rptDepriciationRptSummeryGrpWise.rpt";
            string selectionformula = "";
            string parameter = "Date from " + txtFrmDate.Text + " To " + txtTdate.Text;
            string qry = ReportName + "," + selectionformula + "," + parameter;

            Response.Redirect("~/modules/FixedAsset/Report/frmDepreciationReportViewer.aspx?qry=" + qry);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
}
