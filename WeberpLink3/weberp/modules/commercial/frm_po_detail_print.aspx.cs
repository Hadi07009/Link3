using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using LibraryDTO;
using LibraryDAL;
using LibraryDAL.ErpDataSetTableAdapters;
using LibraryDAL.SCBLDataSetTableAdapters;
using LibraryDAL.SCBL2DataSetTableAdapters;
using LibraryDAL.SCBL3DataSetTableAdapters;

public partial class frm_po_detail_print : System.Web.UI.Page
{
   

    protected void Page_Load(object sender, EventArgs e)
    {
               
        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor ="FFFFFFF";
        getpage();
        
    }
    private void getpage()
    {
        PuTr_PO_Hdr3TableAdapter hdr = new PuTr_PO_Hdr3TableAdapter();
        PuTr_PO_Det3TableAdapter det = new PuTr_PO_Det3TableAdapter();
        SCBL3DataSet.PuTr_PO_Det3DataTable dtdet = new SCBL3DataSet.PuTr_PO_Det3DataTable();
        SCBL3DataSet.PuTr_PO_Hdr3DataTable dthdr = new SCBL3DataSet.PuTr_PO_Hdr3DataTable();
        PuMa_Par_AdrTableAdapter adr = new PuMa_Par_AdrTableAdapter();
        ErpDataSet.PuMa_Par_AdrRow row;

        tbl_tac_logTableAdapter log = new tbl_tac_logTableAdapter();
        SCBLDataSet.tbl_tac_logDataTable dtlog = new SCBLDataSet.tbl_tac_logDataTable();

        tbl_spo_advance_hdrTableAdapter spohdr = new tbl_spo_advance_hdrTableAdapter();
        tbl_spo_advance_detTableAdapter spodet = new tbl_spo_advance_detTableAdapter();
        SCBL2DataSet.tbl_spo_advance_detDataTable dtspodet = new SCBL2DataSet.tbl_spo_advance_detDataTable();
        SCBL2DataSet.tbl_spo_advance_hdrDataTable dtspohdr = new SCBL2DataSet.tbl_spo_advance_hdrDataTable();

       
        string pay_type = "";
        string genstr = "";
        string spestr = "";
        string paystr = "";

        clsReportParameter prm = new clsReportParameter();
        prm = (clsReportParameter)Session[clsStatic.sessionPoReportPrm];

        //prm.FromDate = DateTime.Now.AddMonths(-1);
        //prm.ToDate = DateTime.Now;
        //prm.PoType = "LPO";
        //prm.Plant = "CP";


        lblhdate.Text = "FROM " + prm.FromDate.ToShortDateString() + " TO " + prm.ToDate.ToShortDateString();
        lblplant.Text = prm.Plant;

        switch(prm.PoType)
        {
            case"LPO":
                lblpotype.Text = "LOCAL PURCHASE ORDER";
                dthdr = hdr.GetDataForPrint("CT", "L", "APP", prm.FromDate.AddDays(-1), prm.ToDate);
                break;

            case "SPOU":
                lblpotype.Text = "SPOT PURCHASE ORDER (BEFORE REALISED)";
                dthdr = hdr.GetDataForPrint2(prm.Plant, "S", "ADV","ADRUN","APP", prm.FromDate.AddDays(-1), prm.ToDate);
                break;

            case"SPOR":
                lblpotype.Text = "SPOT PURCHASE ORDER (AFTER REALISED)";
                dthdr = hdr.GetDataForPrint(prm.Plant, "S", "APP", prm.FromDate.AddDays(-1), prm.ToDate);
                break;
        }


        phreport.Controls.Clear();

        foreach (SCBL3DataSet.PuTr_PO_Hdr3Row dr in dthdr.Rows)
        {
            ClientSide_modules_commercial_usercontrols_ctl_po_detail_view ctl = (ClientSide_modules_commercial_usercontrols_ctl_po_detail_view)LoadControl("./usercontrols/ctl_po_detail_view.ascx");

            HtmlTableRow hrow;

            Label lbldate = (Label)ctl.FindControl("lbldate");
            Label lblto = (Label)ctl.FindControl("lblto");
            Label lblsub = (Label)ctl.FindControl("lblsub");
            Label lblfrom = (Label)ctl.FindControl("lblfrom");
            Label lbladd = (Label)ctl.FindControl("lbladd");
            Label lblporef = (Label)ctl.FindControl("lblporef");
            Label lbltot = (Label)ctl.FindControl("lbltot");
            Label lblgen = (Label)ctl.FindControl("lblgen");
            Label lblspe = (Label)ctl.FindControl("lblspe");
            Label lblpay = (Label)ctl.FindControl("lblpay");
            HtmlTable tblhtml = (HtmlTable)ctl.FindControl("tblhtml");

            HtmlTableCell genterms = (HtmlTableCell)ctl.FindControl("genterms");
            HtmlTableCell spterms = (HtmlTableCell)ctl.FindControl("spterms");
            HtmlTableCell payterms = (HtmlTableCell)ctl.FindControl("payterms");

            dtdet = new SCBL3DataSet.PuTr_PO_Det3DataTable();
            dtdet = det.GetDataByRef(dr.PO_Hdr_Ref);

            lbldate.Text = dr.PO_Hdr_DATE.ToShortDateString();

            switch (prm.PoType)
            {
                case "LPO":
                    lblsub.Text = "LOCAL PURCHASE ORDER";
                    lblto.Text = dr.PO_Hdr_Com1;
                    try
                    {
                        row = adr.GetDataByAdrCode(dr.PO_Hdr_Dcode)[0];
                        lbladd.Text = row.Par_Adr_Line_1 + " " + row.Par_Adr_Line_2 + " " + row.Par_Adr_Line_3 + " " + row.Par_Adr_Line_4 + " " + row.Par_Adr_Line_5;

                    }
                    catch { }
                    break;

                case "SPOU":
                    lblsub.Text = "LOCAL PURCHASE ORDER (BEFORE REALISED)";
                    lblto.Text = dr.PO_Hdr_Com3;
                    lbladd.Text = "";
                    break;

                case "SPOR":
                    lblsub.Text = "LOCAL PURCHASE ORDER (AFTER REALISED)";
                    lblto.Text = dr.PO_Hdr_Com3;
                    lbladd.Text = "";
                    break;
            }

           
           
            lblfrom.Text = "Name of employee" + "\n" + "Director Board";
            lblporef.Text = dr.PO_Hdr_Ref;

            if (prm.PoType == "SPOU")
            {
                dtspohdr = spohdr.GetDataByRef(dr.PO_Hdr_Ref);
                dtspodet = spodet.GetDataByRef(dr.PO_Hdr_Ref);
                lbltot.Text = dtspohdr[0].adv_amount.ToString("N2") + " [" + NumerictowordClass.FNumber(dtspohdr[0].adv_amount.ToString("N2")) + "]";

                foreach (SCBL2DataSet.tbl_spo_advance_detRow drr in dtspodet.Rows)
                {

                    hrow = new HtmlTableRow();

                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());

                    hrow.Cells[0].InnerText = drr.seq_no.ToString();
                    hrow.Cells[1].InnerText = drr.item_det;
                                  
                    hrow.Cells[2].InnerText = ".";
                    

                    if (drr.brand == "")
                        hrow.Cells[3].InnerText = ".";
                    else
                        hrow.Cells[3].InnerText = drr.brand;

                    if (drr.origin == ".")
                        hrow.Cells[4].InnerText = "";
                    else
                        hrow.Cells[4].InnerText = drr.origin;

                    if (drr.packing == "")
                        hrow.Cells[5].InnerText = "";
                    else
                        hrow.Cells[5].InnerText = drr.packing;

                    hrow.Cells[6].InnerText = drr.item_qty.ToString("N2") + " " + drr.item_uom.ToString();     
                    hrow.Cells[7].InnerText = drr.item_rate.ToString("N2");
                    hrow.Cells[8].InnerText = drr.tot_amount.ToString("N2");
                    tblhtml.Rows.Add(hrow);
                }
            
            }
            else
            {
                lbltot.Text = dr.PO_Hdr_Value.ToString("N2") + " [" + NumerictowordClass.FNumber(dr.PO_Hdr_Value.ToString("N2")) + "]";

                foreach (SCBL3DataSet.PuTr_PO_Det3Row drr in dtdet.Rows)
                {

                    hrow = new HtmlTableRow();

                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());

                    hrow.Cells[0].InnerText = drr.PO_Det_Lno.ToString();
                    hrow.Cells[1].InnerText = drr.PO_Det_Itm_Desc;
                   
                    if (drr.PO_Det_Specification == "")
                        hrow.Cells[2].InnerText = ".";
                    else
                        hrow.Cells[2].InnerText = drr.PO_Det_Specification;

                    if (drr.PO_Det_Brand == "")
                        hrow.Cells[3].InnerText = ".";
                    else
                        hrow.Cells[3].InnerText = drr.PO_Det_Brand;

                    if (drr.PO_Det_Origin == ".")
                        hrow.Cells[4].InnerText = "";
                    else
                        hrow.Cells[4].InnerText = drr.PO_Det_Origin;

                    if (drr.PO_Det_Packing == "")
                        hrow.Cells[5].InnerText = "";
                    else
                        hrow.Cells[5].InnerText = drr.PO_Det_Packing;

                    hrow.Cells[6].InnerText = drr.PO_Det_Lin_Qty.ToString("N2") + " " + drr.PO_Det_Itm_Uom.ToString();
                    hrow.Cells[7].InnerText = drr.PO_Det_Lin_Rat.ToString("N2");
                    hrow.Cells[8].InnerText = drr.PO_Det_Lin_Amt.ToString("N2");
                    tblhtml.Rows.Add(hrow);
                }
            }


            dtlog = new SCBLDataSet.tbl_tac_logDataTable();
            dtlog = log.GetDataByRef(dr.PO_Hdr_Ref);

            foreach (SCBLDataSet.tbl_tac_logRow drlog in dtlog.Rows)
            {           
              
                switch (drlog.tac_type)
                {

                    case "gen":
                        {
                           genstr += drlog.content_det;
                            break;
                        }

                    case "spe":
                        {
                            spestr += drlog.content_det;
                            break;
                        }

                    case "pay":
                        {
                            pay_type = drlog.pay_type.ToUpper();
                            paystr += drlog.content_det;
                            break;
                        }

                }
            }
            
            if (genstr != "") lblgen.Text = "GENERAL TERMS"; else lblgen.Visible = false;
            if (spestr != "") lblspe.Text = "SPECIAL TERMS"; else lblspe.Visible = false;
            if (paystr != "") lblpay.Text = "PAYMENT TERMS (" + pay_type + " ADVANCE)";
            
            genterms.InnerHtml = genstr;
            spterms.InnerHtml = spestr;
            payterms.InnerHtml = paystr;

            phreport.Controls.Add(ctl);

        }
        



    }
  
    
    
}

