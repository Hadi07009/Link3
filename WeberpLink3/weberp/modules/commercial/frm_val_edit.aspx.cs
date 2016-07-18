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

public partial class frm_val_edit : System.Web.UI.Page
{
   

    protected void Page_Load(object sender, EventArgs e)
    {
        
        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";

        if (!Page.IsPostBack)
        {
            string quot_ref = Request.QueryString["quot_ref"].ToString();
            string pcode = Request.QueryString["pcode"].ToString();
            Session[clsStatic.sessionCurrentRefFocus] = Request.QueryString["focusref"].ToString();

            load_data(quot_ref, pcode);
            
        }
        else
        {

        }          
    }

    private void load_data(string quot_ref, string pcode)
    {
        quotation_detTableAdapter quo = new quotation_detTableAdapter();
        SCBLDataSet.quotation_detRow dr;

        dr = quo.GetDataByRefParty(quot_ref, pcode)[0];
        txtid.Text = quot_ref;
        txtparty.Text = dr.party_code.ToString() + ": " + dr.party_det.ToString();
        txtitem.Text = dr.product_code.ToString() + ": " + dr.product_det.ToString();
        txtqty.Text=dr.qty.ToString()+ " " + dr.uom.ToString();


        txtspecification.Text = dr.specification.ToString();
        txtbrand.Text = dr.product_brand.ToString();
        txtorigin.Text = dr.origin.ToString();
        txtpacking.Text = dr.packing.ToString();
        txtrate.Text = dr.rate.ToString();
        lbltk.Text = "TK. / " + dr.uom.ToString();


    }
     
  
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        quotation_detTableAdapter quo = new quotation_detTableAdapter();

        string[] tmp = txtparty.Text.Split(':');
        string pcode = tmp[0].ToString();

        quo.UpdateFromCSApp(Convert.ToDecimal(txtrate.Text), txtspecification.Text, txtbrand.Text, txtorigin.Text, txtpacking.Text, txtid.Text, pcode);

        Response.Redirect("./frm_quo_approval.aspx");
    }
}

