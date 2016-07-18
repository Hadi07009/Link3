using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using LibraryDTO;
using LibraryDAL;
using LibraryDAL.SCBLDataSetTableAdapters;
using LibraryDAL.SCBL2DataSetTableAdapters;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using LibraryDAL.SCBLINTableAdapters;
using LibraryDAL.ErpDataSetTableAdapters;
using LibraryDAL.AccDataSetTableAdapters;

public partial class frm_mrr_print_report : System.Web.UI.Page
{

    ReportDocument rpt1 = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {

        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";

        if (!Page.IsPostBack)
        {

            cldfrom.SelectedDate = DateTime.Now;
            cldto.SelectedDate = DateTime.Now;
            get_all_store();
        }
        else
        {
            showreport();
        }
       
          
    }


    private void get_all_store()
    {
        InMa_Str_LocTableAdapter store = new InMa_Str_LocTableAdapter();
        ErpDataSet.InMa_Str_LocDataTable dtstore = new ErpDataSet.InMa_Str_LocDataTable();
        ListItem lst;
        dtstore = store.GetAllStore();

        ddlstore.Items.Clear();
        ddlstore.Items.Add("");

        foreach (ErpDataSet.InMa_Str_LocRow dr in dtstore.Rows)
        {
            lst = new ListItem();
            lst.Text = dr.Str_Loc_Id + ":" + dr.Str_Loc_Name;
            lst.Value = dr.Str_Loc_Id;
            ddlstore.Items.Add(lst);

        }


    }
   


    protected void btnview_Click(object sender, EventArgs e)
    {
        if (ddlstore.Text == "") return;
        InTr_Trn_DetTableAdapter det = new InTr_Trn_DetTableAdapter();
        InTr_Trn_HdrTableAdapter hdr = new InTr_Trn_HdrTableAdapter();
        InTr_Trn_ExtTableAdapter ext = new InTr_Trn_ExtTableAdapter();
        ErpDataSet.InTr_Trn_DetDataTable dtdet = new ErpDataSet.InTr_Trn_DetDataTable();
        ErpDataSet.InTr_Trn_HdrDataTable dthdr = new ErpDataSet.InTr_Trn_HdrDataTable();
        ErpDataSet.InTr_Trn_ExtDataTable dtext = new ErpDataSet.InTr_Trn_ExtDataTable();
        AccTransactionHeaderTableAdapter acchdr=new AccTransactionHeaderTableAdapter();
        AccTransactionDetailsTableAdapter accdet=new AccTransactionDetailsTableAdapter();
        AccDataSet.AccTransactionDetailsDataTable dtaccdet;
        PuMa_Par_AdrTableAdapter adr=new PuMa_Par_AdrTableAdapter();
        ErpDataSet.PuMa_Par_AdrDataTable dtadr;
        string sname, sadd, jvno="", remarks="";
        Nullable<DateTime> jvdate=null;
      
        tbl_mrr_reportTableAdapter rep = new tbl_mrr_reportTableAdapter();
        
        dthdr = hdr.GetDataByTypeDate("RC", cldfrom.SelectedDate, cldto.SelectedDate);

        rep.DeleteAllData();

        foreach (ErpDataSet.InTr_Trn_HdrRow dr in dthdr.Rows)
        {
            dtdet = new ErpDataSet.InTr_Trn_DetDataTable();
            dtext = new ErpDataSet.InTr_Trn_ExtDataTable();
            dtadr=new ErpDataSet.PuMa_Par_AdrDataTable();
            dtdet = det.GetDataByRef(dr.Trn_Hdr_Ref);
            dtext = ext.GetDataByRef(dr.Trn_Hdr_Ref);

            dtadr=adr.GetDataByAdrCode(dr.Trn_Hdr_Dcode);
            if (dtadr.Count == 0) { sname = ""; sadd = ""; } else { sname = dtadr[0].par_adr_name; sadd = dtadr[0].Par_Adr_Line_1 + " " + dtadr[0].Par_Adr_Line_2 + " " + dtadr[0].Par_Adr_Line_3; }
            
            dtaccdet=new AccDataSet.AccTransactionDetailsDataTable();
            dtaccdet=accdet.GetDataByGrnNo(dr.Trn_Hdr_Ref);
            if(dtaccdet.Count==0){ jvno=""; jvdate=null;} else{ jvno=dtaccdet[0].Trn_Ref_No; jvdate= acchdr.GetDataByRef(jvno)[0].Trn_DATE;}

           
            foreach (ErpDataSet.InTr_Trn_DetRow drow in dtdet.Rows) 
            {
                if (dr.IsTrn_Hdr_RemarksNull()) { remarks = ""; } else { remarks = dr.Trn_Hdr_Remarks; }

                if (dtext.Rows.Count == 0)
                {
                    rep.InsertMrr(dr.Trn_Hdr_Ref, dr.Trn_Hdr_DATE, "", null, drow.Trn_Det_Ord_Ref, sname, sadd, dr.Trn_Hdr_DATE, "", "", null, ddlstore.SelectedValue.ToString(), (int)drow.Trn_Det_Lno, drow.Trn_Det_Icode, drow.Trn_Det_Itm_Desc, drow.Trn_Det_Itm_Uom, drow.T_C1, drow.T_C2, drow.Trn_Det_Bat_No, (decimal)drow.Trn_Det_Lin_Qty, drow.Trn_Det_Lin_Rat, drow.Trn_Det_Lin_Amt, jvno, jvdate, remarks);
                }
                else
                {
                    rep.InsertMrr(dr.Trn_Hdr_Ref, dr.Trn_Hdr_DATE, dtext[0].Trn_Ext_SupInvNo, dtext[0].Trn_Ext_SupInvDate, drow.Trn_Det_Ord_Ref, sname, sadd, dr.Trn_Hdr_DATE, dtext[0].Trn_Ext_Mode_Deli, "", null, ddlstore.SelectedValue.ToString(), (int)drow.Trn_Det_Lno, drow.Trn_Det_Icode, drow.Trn_Det_Itm_Desc, drow.Trn_Det_Itm_Uom, drow.T_C1, drow.T_C2, drow.Trn_Det_Bat_No, (decimal)drow.Trn_Det_Lin_Qty, drow.Trn_Det_Lin_Rat, drow.Trn_Det_Lin_Amt, jvno, jvdate, remarks);
                }
            } 
        }

       


       
        showreport();

    }

    private void showreport()
    {

        DataTable dt = new DataTable();
        tbl_mrr_reportTableAdapter sto = new tbl_mrr_reportTableAdapter();
        string DateF = cldfrom.SelectedDate.ToShortDateString();
        string Datet = cldto.SelectedDate.ToShortDateString();
        dt = sto.GetData();

        CrystalReportViewer1.Zoom(75);
        rpt1.Load(Server.MapPath("files/rpt_mrr.rpt"));
        rpt1.SetDataSource(dt);       
        CrystalReportViewer1.ReportSource = rpt1;
        
        CrystalReportViewer1.DataBind();



        
    }

    
    protected void CrystalReportViewer1_Unload(object sender, EventArgs e)
    {
        rpt1.Close();
        rpt1.Dispose();
        GC.Collect();
    }
}


