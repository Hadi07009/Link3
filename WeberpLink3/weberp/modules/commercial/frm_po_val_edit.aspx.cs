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
using LibraryDAL;
using LibraryDAL.SCBLDataSetTableAdapters;

public partial class frm_po_val_edit : System.Web.UI.Page
{
   

    protected void Page_Load(object sender, EventArgs e)
    {
        
        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";

        if (!Page.IsPostBack)
        {
            string po_ref = Request.QueryString["po_ref"].ToString();
            string icode = Request.QueryString["icode"].ToString();

            load_data(po_ref, icode);
            
        }
        else
        {

        }          
    }

    private void load_data(string po_ref, string icode)
    {
        PuTr_PO_Hdr_ScblTableAdapter pohdr = new PuTr_PO_Hdr_ScblTableAdapter();
        PuTr_PO_Det_ScblTableAdapter podet = new PuTr_PO_Det_ScblTableAdapter();
        SCBLDataSet.PuTr_PO_Det_ScblDataTable dtpodet = new SCBLDataSet.PuTr_PO_Det_ScblDataTable();
        quotation_detTableAdapter quo = new quotation_detTableAdapter();
        SCBLDataSet.quotation_detRow dr;

        string qref,pcode;

        txtid.Text = po_ref;
        txtparty.Text = pohdr.GetHdrDataByRef(po_ref)[0].PO_Hdr_Com1.ToString();
        pcode = pohdr.GetHdrDataByRef(po_ref)[0].PO_Hdr_Pcode.ToString();
        txtpcode.Text = pcode;

        dtpodet = podet.GetDetByRefItem(po_ref, icode);

        txtitemcode.Text = icode;
        txtitem.Text = dtpodet[0].PO_Det_Itm_Desc.ToString();
        txtqty.Text = dtpodet[0].PO_Det_Lin_Qty.ToString("N2");
        qref = dtpodet[0].PO_Det_Quo_Ref.ToString();
        txtrate.Text = dtpodet[0].PO_Det_Lin_Rat.ToString("N2");

        txtquoref.Text = qref;

        if (qref != "")
        {
            dr = quo.GetDataByRefParty(qref, pcode)[0];
            txtspecification.Text = dr.specification.ToString();
            txtbrand.Text = dr.product_brand.ToString();
            txtorigin.Text = dr.origin.ToString();
            txtpacking.Text = dr.packing.ToString();
        }
        else
        {
            txtspecification.Text = "";
            txtbrand.Text = "";
            txtorigin.Text = "";
            txtpacking.Text = "";
        }
        

    }
     
  
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        quotation_detTableAdapter quo = new quotation_detTableAdapter();
        PuTr_PO_Det_ScblTableAdapter det = new PuTr_PO_Det_ScblTableAdapter();

        if (txtrate.Text == "") return;
        if(Convert.ToDecimal(txtrate.Text)<=0) return;

        if (txtquoref.Text == "")
        {
            det.UpdateRate(Convert.ToDecimal(txtrate.Text), txtid.Text, txtitemcode.Text);
        }
        else
        {
            det.UpdateRate(Convert.ToDecimal(txtrate.Text), txtid.Text, txtitemcode.Text);
            quo.UpdateFromCSApp(Convert.ToDecimal(txtrate.Text), txtspecification.Text, txtbrand.Text, txtorigin.Text, txtpacking.Text, txtquoref.Text, txtpcode.Text);
        }
       
        

        Response.Redirect("./frm_po_revising_app.aspx");
    }
}

