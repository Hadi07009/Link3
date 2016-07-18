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
using LibraryDAL.SCBLQryTableAdapters;

public partial class frm_mpr_lifecycle : System.Web.UI.Page
{
   

    protected void Page_Load(object sender, EventArgs e)
    {
               
        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";

        //if (!Page.IsPostBack)
        //{
            if (Request.QueryString.Count < 0) { tblmaster.Visible = false; return; }
            string ref_no = Request.QueryString["ref_no"];
            string itm_code = Request.QueryString["itm_code"];
            if ((ref_no == null) || (itm_code == null)) { tblmaster.Visible = false; return; }

            generate_data(ref_no, itm_code);

        //}
        
    }

    private void generate_data(string ref_no, string itm_code)
    {
        PuTr_IN_Det_Scbl2TableAdapter indet = new PuTr_IN_Det_Scbl2TableAdapter();
        SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable dtindet = new SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable();

        PuTr_IN_Hdr_ScblTableAdapter inhdr = new PuTr_IN_Hdr_ScblTableAdapter();
        SCBLDataSet.PuTr_IN_Hdr_ScblDataTable dtinhdr = new SCBLDataSet.PuTr_IN_Hdr_ScblDataTable();
                
        DtMprToPOTableAdapter podet = new DtMprToPOTableAdapter();
        SCBLQry.DtMprToPODataTable dtpodet = new SCBLQry.DtMprToPODataTable();
        
        tbl_mat_rec_retTableAdapter mat = new tbl_mat_rec_retTableAdapter();
        SCBL2DataSet.tbl_mat_rec_retDataTable dtmat = new SCBL2DataSet.tbl_mat_rec_retDataTable();

        GridView gv;
        string trntype;

        dtinhdr = inhdr.GetDataByRef(ref_no);
        if (dtinhdr.Rows.Count == 0) { tblmaster.Visible = false; return; }

        dtindet = indet.GetDataByRefItem(ref_no, itm_code);
        if (dtindet.Rows.Count == 0) { tblmaster.Visible = false; return; }

        lnkref.Text = ref_no;

        lnkref.Attributes.Add("onclick ", "window.open('./frm_mpr_view.aspx?ref_no=" + ref_no + "')");


        lblitm.Text = itm_code + ":" + dtindet[0].IN_Det_Itm_Desc;
        lblmprdate.Text = dtinhdr[0].IN_Hdr_St_DATE.ToString();
        if (dtinhdr[0].IN_Hdr_Status == "REJ") { lblstatus.Text = "REJ"; } else { lblstatus.Text = dtindet[0].In_Det_Status; }
        lblqty.Text = dtindet[0].IN_Det_Lin_Qty.ToString("N2") + " " + dtindet[0].IN_Det_Itm_Uom;
        lblspecification.Text = dtindet[0].In_Det_Specification;
        lblbrand.Text = dtindet[0].In_Det_Brand;
        lblorigin.Text = dtindet[0].In_Det_Origin;
        lblpacking.Text = dtindet[0].In_Det_Packing;
        lblnoe.Text = dtindet[0].In_Det_Noe ;
        lblloc.Text = dtindet[0].In_Det_Loc;
        lblremarks.Text = dtindet[0].In_Det_Remarks;
        lbletr.Text = dtindet[0].IN_Det_Exp_Dat.ToShortDateString();

        //po detail

        dtpodet = podet.GetDataFromMprPo(ref_no, itm_code);

        DataTable dt = new DataTable();
        DataTable dtmrr = new DataTable();

        dt.Columns.Add("A", typeof(string));
        dt.Columns.Add("B", typeof(string));

        DataRow dtr;

        foreach (SCBLQry.DtMprToPORow dr in dtpodet.Rows)
        {
            dtr = dt.NewRow();

            dtr[0] = "PO REF";
            dtr[1] = dr.PO_Hdr_Ref + "," + dr.PO_Det_Lno.ToString();
            dt.Rows.Add(dtr);

            //dt.Rows.Add("PO REF", dr.PO_Hdr_Ref + "," + dr.PO_Det_Lno.ToString());
            dt.Rows.Add("PO DATE", dr.PO_Hdr_DATE.ToShortDateString());
            dt.Rows.Add("SUPPLIER", dr.PO_Hdr_Com1+" "+dr.PO_Hdr_Com3);           
            dt.Rows.Add("SPECIFIC", dr.PO_Det_Specification);
            dt.Rows.Add("BRAND", dr.PO_Det_Brand);
            dt.Rows.Add("ORIGIN", dr.PO_Det_Origin);
            dt.Rows.Add("PACKING", dr.PO_Det_Packing);
            dt.Rows.Add("QUANTITY", dr.PO_Det_Lin_Qty.ToString("N2") + " " + dr.PO_Det_Itm_Uom);
            dt.Rows.Add("RATE", dr.PO_Det_Lin_Rat.ToString("N2"));
            dt.Rows.Add("AMOUNT", dr.PO_Det_Lin_Amt.ToString("N2"));
            dt.Rows.Add("MRR DET", dr.PO_Hdr_Ref + "," + dr.PO_Det_Lno.ToString());// column name should not be change

        }


        gdItem.DataSource = dt;
        gdItem.DataBind();
        LinkButton lnk;

        foreach (GridViewRow gr in gdItem.Rows)
        {
            if (gr.Cells[0].Text == "PO REF")
            {
                lnk = new LinkButton();
                lnk.Text = gr.Cells[1].Text;
                lnk.Attributes.Add("onclick ", "window.open('./frm_po_detailview.aspx?po_ref_no=" + gr.Cells[1].Text.Split(',')[0] + "')");
                gr.Cells[1].Controls.Add(lnk);
            }


            if (gr.Cells[0].Text == "MRR DET")
            {
                gv = new GridView();
                dtmrr = new DataTable();
                dtmat = new SCBL2DataSet.tbl_mat_rec_retDataTable();
                dtmat = mat.GetDataByPorefSl(gr.Cells[1].Text.Split(',')[0], Convert.ToInt32(gr.Cells[1].Text.Split(',')[1]));

                dtmrr.Columns.Clear();
                dtmrr.Columns.Add("TRN TYPE", typeof(string));
                dtmrr.Columns.Add("TRN/MRR REF", typeof(string));
                dtmrr.Columns.Add("TRN TIME", typeof(DateTime));
                dtmrr.Columns.Add("QTY", typeof(string));
                dtmrr.Columns.Add("BR|ORG|PAC", typeof(string));

                foreach (SCBL2DataSet.tbl_mat_rec_retRow drr in dtmat.Rows)
                {
                    switch(drr.trn_type)
                    {
                        case "OK":
                            trntype = "INSPECTED";
                            break;
                        case "INSPECTION":
                            trntype = "DELIVERED";
                            break;
                        case "CONFIRM":
                            trntype = "MRR";
                            break;
                        default:
                            trntype = drr.trn_type;
                            break;

                    }

                    dtmrr.Rows.Add(trntype, drr.trn_ref_no, drr.trn_datetime, drr.itm_rec_ret_qty.ToString("N2") + " " + drr.uom, drr.brand + "| " + drr.origin + "| " + drr.packing);
                }

                gv.ID = gr.Cells[1].Text + itm_code;
                gv.DataSource = dtmrr;
                gv.DataBind();
                gr.Cells[1].Controls.Add(gv);

            }
        }
               

    }
  
    
    
}

