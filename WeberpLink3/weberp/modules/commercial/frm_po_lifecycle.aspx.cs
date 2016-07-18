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

public partial class frm_po_lifecycle : System.Web.UI.Page
{
   

    protected void Page_Load(object sender, EventArgs e)
    {
               
        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor ="FFFFFFF";

        if (!Page.IsPostBack)
        {
            if (Request.QueryString.Count < 0) { tblmaster.Visible = false; return; }
            string ref_no = Request.QueryString["ref_no"];            
            if (ref_no == null) { tblmaster.Visible = false; return; }

            detailview(ref_no);

        }
        
    }

    private void detailview(string ref_no)
    {


        
        gdItem.Visible = true;      

        PuTr_PO_Det_ScblTableAdapter podet = new PuTr_PO_Det_ScblTableAdapter();
        SCBLDataSet.PuTr_PO_Det_ScblDataTable dtpodet = new SCBLDataSet.PuTr_PO_Det_ScblDataTable();

        InTr_Trn_HdrTableAdapter intrhdr = new InTr_Trn_HdrTableAdapter();
        ErpDataSet.InTr_Trn_HdrDataTable dtintrhdr = new ErpDataSet.InTr_Trn_HdrDataTable();

        tbl_mat_rec_retTableAdapter mat = new tbl_mat_rec_retTableAdapter();
        SCBL2DataSet.tbl_mat_rec_retDataTable dtmat = new SCBL2DataSet.tbl_mat_rec_retDataTable();

        PuTr_PO_Hdr_ScblTableAdapter pohdr = new PuTr_PO_Hdr_ScblTableAdapter();
        SCBLDataSet.PuTr_PO_Hdr_ScblDataTable dthdr = new SCBLDataSet.PuTr_PO_Hdr_ScblDataTable();
        App_Type_DetTableAdapter appdet = new App_Type_DetTableAdapter();

        string poref, item_code, trntype, status_det;
        int po_lno;
        GridView gv;

        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();


        // header data

        dthdr = pohdr.GetHdrDataByRef(ref_no);
        if (dthdr.Rows.Count == 0) return;


        switch (dthdr[0].PO_Hdr_Status)
        {
            case "APP":
                status_det = "PO CREATED";
                break;

            case "RUN":
                status_det = "PO PENDING FOR " + appdet.GetDataByAppName(dthdr[0].PO_Hdr_Pending)[0].app_desc.ToString();

                break;


            case "ADV":
                status_det = "SPO CREATED, REALISATION PENDING";
                break;


            case "ADRUN":
                status_det = "PO REALISATION PENDING FOR " + appdet.GetDataByAppName(dthdr[0].PO_Hdr_Pending)[0].app_desc.ToString();
                //
                break;

            case "CLOSING":
                status_det = "PO CLOSING PENDING FOR " + appdet.GetDataByAppName(dthdr[0].PO_Hdr_Pending)[0].app_desc.ToString();
                //
                break;

            case "CLOSED":
                status_det = "PO CLOSED";
                break;


            case "CANCELING":
                status_det = "PO CANCEL PENDING FOR " + appdet.GetDataByAppName(dthdr[0].PO_Hdr_Pending)[0].app_desc.ToString();
                //
                break;


            case "CANCELED":
                status_det = "PO CANCELED";
                break;


            case "REVISING":
                status_det = "PO REVISE PENDING FOR " + appdet.GetDataByAppName(dthdr[0].PO_Hdr_Pending)[0].app_desc.ToString();
                break;


            case "REJ":
                status_det = "REJECTED ";
                break;


            default: status_det = "";
                //
                break;
        }


        lblref.Text = dthdr[0].PO_Hdr_Ref;
        lblpodate.Text = dthdr[0].PO_Hdr_DATE.ToShortDateString();
        lblparty.Text = dthdr[0].PO_Hdr_Com1;
        lblemp.Text = dthdr[0].PO_Hdr_Com3;
        lblamount.Text = dthdr[0].PO_Hdr_Value.ToString("N2");
        lblstatus.Text = status_det;


        // detail data
        dt.Columns.Clear();

        dt.Columns.Add("PO REF", typeof(string));
        dt.Columns.Add("ICODE", typeof(string));
        dt.Columns.Add("ITEM", typeof(string));
        dt.Columns.Add("SPECIFICATION", typeof(string));
        dt.Columns.Add("BRAND", typeof(string));
        dt.Columns.Add("ORIGIN", typeof(string));
        dt.Columns.Add("PACKING", typeof(string));
        dt.Columns.Add("MPR REF", typeof(string));
        dt.Columns.Add("UOM", typeof(string));
        dt.Columns.Add("RATE", typeof(string));
        dt.Columns.Add("PO QTY", typeof(string));
        dt.Columns.Add("REC QTY", typeof(string));
        dt.Columns.Add("INS QTY", typeof(string));

        dtpodet = podet.GetDetByRef(ref_no);


        foreach (SCBLDataSet.PuTr_PO_Det_ScblRow dr in dtpodet.Rows)
        {

            dt.Rows.Add(dr.PO_Det_Ref + "," + dr.PO_Det_Lno.ToString(), dr.PO_Det_Icode, dr.PO_Det_Itm_Desc, dr.PO_Det_Specification, dr.PO_Det_Brand, dr.PO_Det_Origin, dr.PO_Det_Packing, dr.PO_Det_Pr_Ref, dr.PO_Det_Itm_Uom, dr.PO_Det_Lin_Rat.ToString("N2"), dr.PO_Det_Lin_Qty.ToString("N2"), dr.PO_Det_Org_QTY.ToString("N2"), dr.PO_Det_Ins_QTY.ToString("N2"));

        }

        gdItem.DataSource = dt;
        gdItem.DataBind();

        foreach (GridViewRow gr in gdItem.Rows)
        {
            poref = gr.Cells[1].Text.Split(',')[0];
            po_lno = Convert.ToInt32(gr.Cells[1].Text.Split(',')[1]);
            item_code = gr.Cells[2].Text;
            gv = new GridView();
            gv = (GridView)gr.Cells[0].FindControl("GridView1");

            dt2 = new DataTable();
            dt2.Columns.Clear();
            dt2.Columns.Add("TYPE", typeof(string));
            dt2.Columns.Add("REF NO", typeof(string));
            dt2.Columns.Add("QTY", typeof(string));
            dt2.Columns.Add("DATE TIME", typeof(DateTime));

            dtmat = new SCBL2DataSet.tbl_mat_rec_retDataTable();
            dtmat = mat.GetDataByPorefSl(poref, po_lno);

            foreach (SCBL2DataSet.tbl_mat_rec_retRow dtr in dtmat.Rows)
            {
                switch (dtr.trn_type)
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
                        trntype = dtr.trn_type;
                        break;

                }

                dt2.Rows.Add(trntype, dtr.trn_ref_no, dtr.itm_rec_ret_qty.ToString("N2"), dtr.trn_datetime);
            }

            gv.DataSource = dt2;
            gv.DataBind();

        }

    }
  
    
    
}

