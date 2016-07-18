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
using LibraryDAL.SCBL2DataSetTableAdapters;
using FreeTextBoxControls;
public partial class frm_quotation_add_cs : System.Web.UI.Page
{
   

    protected void Page_Load(object sender, EventArgs e)
    {
        
        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";

        if (!Page.IsPostBack)
        {
            if (Session[clsStatic.sessionQuotationRef] == null) return;

            set_data_from_cmd(Session[clsStatic.sessionQuotationRef].ToString());
            get_master_tac(ddlpayterms.SelectedValue.ToString());
        }

      
          
    }

    private void get_master_tac(string pay_type)
    {
        load_tandc_gen();
        load_tandc_spe();
        load_tandc_pay(pay_type);
    }

    private void load_tandc_gen()
    {
        tbl_tac_logTableAdapter log = new tbl_tac_logTableAdapter();
        SCBLDataSet.tbl_tac_logDataTable logdt = new SCBLDataSet.tbl_tac_logDataTable();
      
        tbl_tac_detTableAdapter tac = new tbl_tac_detTableAdapter();
        SCBLDataSet.tbl_tac_detDataTable dt = new SCBLDataSet.tbl_tac_detDataTable();
        dt = tac.GetDataByType("gen");
        FreeTextBox txt;
        CheckBox chk;

        gdgen.DataSource = dt;
        gdgen.DataBind();

        foreach (SCBLDataSet.tbl_tac_detRow dr in dt.Rows)
        {
            txt = new FreeTextBox();
            chk = new CheckBox();
            txt = (FreeTextBox)gdgen.Rows[Convert.ToInt16(dr.tac_seq_no) - 1].FindControl("TextBox1");
            chk = (CheckBox)gdgen.Rows[Convert.ToInt16(dr.tac_seq_no) - 1].FindControl("CheckBox1");
            txt.Text = dr.tac_det;
            chk.Text = Convert.ToInt16(dr.tac_seq_no).ToString();            
        }
    }

    private void load_tandc_spe()
    {
        tbl_tac_logTableAdapter log = new tbl_tac_logTableAdapter();
        SCBLDataSet.tbl_tac_logDataTable logdt = new SCBLDataSet.tbl_tac_logDataTable();

        tbl_tac_detTableAdapter tac = new tbl_tac_detTableAdapter();
        SCBLDataSet.tbl_tac_detDataTable dt = new SCBLDataSet.tbl_tac_detDataTable();
        dt = tac.GetDataByType("spe");
        FreeTextBox txt;
        CheckBox chk;

        gdspe.DataSource = dt;
        gdspe.DataBind();

        foreach (SCBLDataSet.tbl_tac_detRow dr in dt.Rows)
        {
            txt = new FreeTextBox();
            chk = new CheckBox();
            txt = (FreeTextBox)gdspe.Rows[Convert.ToInt16(dr.tac_seq_no) - 1].FindControl("TextBox2");
            chk = (CheckBox)gdspe.Rows[Convert.ToInt16(dr.tac_seq_no) - 1].FindControl("CheckBox2");
            txt.Text = dr.tac_det;
            chk.Text = Convert.ToInt16(dr.tac_seq_no).ToString();                        
        }


    }
    private void load_tandc_pay(string pay_type)
    {
        tbl_tac_logTableAdapter log = new tbl_tac_logTableAdapter();
        SCBLDataSet.tbl_tac_logDataTable logdt = new SCBLDataSet.tbl_tac_logDataTable();

        tbl_tac_detTableAdapter tac = new tbl_tac_detTableAdapter();
        SCBLDataSet.tbl_tac_detDataTable dt = new SCBLDataSet.tbl_tac_detDataTable();
        dt = tac.GetDataByType2("pay", "com", pay_type);
        FreeTextBox txt;
        CheckBox chk;
        int seq = 0;

        gdpay.DataSource = dt;
        gdpay.DataBind();

        foreach (SCBLDataSet.tbl_tac_detRow dr in dt.Rows)
        {
            txt = new FreeTextBox();
            chk = new CheckBox();
            txt = (FreeTextBox)gdpay.Rows[seq].FindControl("TextBox3");
            chk = (CheckBox)gdpay.Rows[seq].FindControl("CheckBox3");
            txt.Text = dr.tac_det;
            chk.Text = Convert.ToInt16(dr.tac_seq_no).ToString();
            seq++;
        }


    }

    //private string get_my_app()
    //{
    //    User_Role_DefinitionTableAdapter urole = new User_Role_DefinitionTableAdapter();
    //    SCBLDataSet.User_Role_DefinitionDataTable udt = new SCBLDataSet.User_Role_DefinitionDataTable();
    //    string my_app = "";
    //    udt = urole.GetRoleByUser(current.UserId.ToString());

    //    if (udt.Rows.Count > 0) my_app = udt[0].role_as.ToString();

    //    return my_app;
    //}

   

    private void set_data_from_cmd(string quotation_ref)
    {
        PuTr_IN_Det_Scbl2TableAdapter indet = new PuTr_IN_Det_Scbl2TableAdapter();
        SCBL2DataSet.PuTr_IN_Det_Scbl2Row drdet;
        quotation_detTableAdapter qdet = new quotation_detTableAdapter();
        SCBLDataSet.quotation_detDataTable quodt = new SCBLDataSet.quotation_detDataTable();
        string mpr_ref_no, itmcode;     

        int i=1;

        DataTable dt = new DataTable();

        dt.Columns.Clear();
        dt.Columns.Add("SL", typeof(string));
        dt.Columns.Add("PCODE", typeof(string));
        dt.Columns.Add("PDET", typeof(string));
        dt.Columns.Add("QTY", typeof(string));
        dt.Columns.Add("RATE", typeof(string));
        dt.Columns.Add("ANMT", typeof(string));
        dt.Columns.Add("SPCFICAT", typeof(string));
        dt.Columns.Add("BRAND", typeof(string));
        dt.Columns.Add("ORIGIN", typeof(string));
        dt.Columns.Add("PACKING", typeof(string));


        quodt = qdet.GetQuotationByRef(quotation_ref);

        if (quodt.Rows.Count == 0) return;

        mpr_ref_no = quodt[0].requisition_id.ToString();
        itmcode = quodt[0].product_code.ToString();

        lblcurlist.Text = "CURRENT QUOT FOR ITEM- " + quodt[0].product_code.ToString() + ": " + quodt[0].product_det.ToString();

        foreach (SCBLDataSet.quotation_detRow dr in quodt.Rows)
        {
            dt.Rows.Add(i.ToString(), dr.party_code, dr.party_det, dr.qty.ToString("N2") + " " + dr.uom, dr.rate.ToString("N2"), ((decimal)dr.qty * dr.rate).ToString("N2"), dr.specification, dr.product_brand, dr.origin, dr.packing);
                i++;
        }

        gdItem.DataSource = dt;
        gdItem.DataBind();

        
        Label lblref = (Label)ctlquo.FindControl("lblref");
        Label lblreqtype = (Label)ctlquo.FindControl("lblreqtype");
        Label lblitmcode = (Label)ctlquo.FindControl("lblitmcode");
        Label lblsl = (Label)ctlquo.FindControl("lblsl");
        Label lblproduct = (Label)ctlquo.FindControl("lblproduct");
        Label lblqty = (Label)ctlquo.FindControl("lblqty");
        Label lbltk = (Label)ctlquo.FindControl("lbltk");

        TextBox txtspecification = (TextBox)ctlquo.FindControl("txtspecification");
        TextBox txtbrand = (TextBox)ctlquo.FindControl("txtbrand");
        TextBox txtorigin = (TextBox)ctlquo.FindControl("txtorigin");
        TextBox txtpacking = (TextBox)ctlquo.FindControl("txtpacking");


        drdet = indet.GetDataByRefItem(mpr_ref_no, itmcode)[0];

        lblref.Text = mpr_ref_no;
        lblreqtype.Text = drdet.IN_Det_Code.ToString() + ", " + drdet.In_Det_Pur_Type.ToString();
        lblsl.Text = "1.";
        lblitmcode.Text = drdet.IN_Det_Icode.ToString();
        lblproduct.Text = drdet.IN_Det_Itm_Desc;
        lblqty.Text = drdet.IN_Det_Lin_Qty + " " + drdet.IN_Det_Itm_Uom;
        lbltk.Text = "Tk. /" + drdet.IN_Det_Itm_Uom;

        txtspecification.Text = drdet.In_Det_Specification + " " + drdet.In_Det_Remarks;
        txtbrand.Text = drdet.In_Det_Brand;
        txtorigin.Text = drdet.In_Det_Origin;
        txtpacking.Text = drdet.In_Det_Packing;
       
        
           
    }

  

    private void ddlchange()
    {
        ddlpayterms.SelectedValue.ToString();
        load_tandc_pay(ddlpayterms.SelectedValue.ToString());        
    }


    protected void ddlpayterms_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        ddlchange();        
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        lblmsg.Visible = false;

        PuTr_IN_Det_Scbl2TableAdapter srdet = new PuTr_IN_Det_Scbl2TableAdapter();

        tbl_tac_logTableAdapter taclog = new tbl_tac_logTableAdapter();
        quotation_detTableAdapter quo = new quotation_detTableAdapter();
        //quotation_logTableAdapter log = new quotation_logTableAdapter();

        string quoref = Session[clsStatic.sessionQuotationRef].ToString();
        int  max_seq, indx;
        string tmp_str, pcode, pdet, uom, req_type, pur_type, tac_ref_no;
        string[] tmp;
        double qnty, max_ref;
        CheckBox chk;
        FreeTextBox txt; 

        ClientSide_modules_commercial_usercontrols_ctl_quotation_entry ctl;
        TextBox txtspecification, txtbrand, txtorigin, txtpacking, txtrate;
        Label lblref, lblitmcode, lblproduct, lblqty, lblreqtype;

        //check valid days entry
        if (txtvaliddays.Text == "") return;

        if (Convert.ToInt32(txtvaliddays.Text) < 1) return;

        //ckeck party

        tmp_str = txtparty.Text;
        tmp = tmp_str.Split(':');

        if (tmp.Length < 2) return;

        // check pay terms entry

        indx = 0;
        foreach (GridViewRow gr in gdgen.Rows)
        {
            chk = new CheckBox();
            chk = (CheckBox)gr.FindControl("CheckBox1");

            if (chk.Checked) indx++;

        }
        if (indx < 1) { lblmsg.Visible = true; return; }


        indx = 0;
        foreach (GridViewRow gr in gdpay.Rows)
        {
            chk = new CheckBox();
            chk = (CheckBox)gr.FindControl("CheckBox3");

            if (chk.Checked) indx++;
        }

        if (indx < 2) { lblmsg.Visible = true; return; }
        

        //Insert TAC
        max_ref = Convert.ToDouble(taclog.GetMaxRef()) + 1;
        tac_ref_no = "QEN-" + string.Format("{0:000000}", max_ref);

        indx = 0;
        foreach (GridViewRow gr in gdgen.Rows)
        {
            chk = new CheckBox();
            txt = new FreeTextBox();

            txt = (FreeTextBox)gr.FindControl("TextBox1");
            chk = (CheckBox)gr.FindControl("CheckBox1");
           
            if (chk.Checked)
            {
                indx++;
                taclog.Inserttac(tac_ref_no, "QEN", "gen", "", Convert.ToInt32(chk.Text), indx, txt.Text, Convert.ToInt32(txtvaliddays.Text), "");
            }
        }

        indx = 0;
        foreach (GridViewRow gr in gdspe.Rows)
        {
            chk = new CheckBox();
            txt = new FreeTextBox();

            txt = (FreeTextBox)gr.FindControl("TextBox2");
            chk = (CheckBox)gr.FindControl("CheckBox2");

            if (chk.Checked)
            {
                indx++;
                taclog.Inserttac(tac_ref_no, "QEN", "spe", "", Convert.ToInt32(chk.Text), indx, txt.Text, Convert.ToInt32(txtvaliddays.Text), "");
            }
        }

        indx = 0;
        foreach (GridViewRow gr in gdpay.Rows)
        {
            chk = new CheckBox();
            txt = new FreeTextBox();

            txt = (FreeTextBox)gr.FindControl("TextBox3");
            chk = (CheckBox)gr.FindControl("CheckBox3");

            if (chk.Checked)
            {
                indx++;
                taclog.Inserttac(tac_ref_no, "QEN", "pay", ddlpayterms.SelectedValue.ToString(), Convert.ToInt32(chk.Text), indx, txt.Text, Convert.ToInt32(txtvaliddays.Text), "");
            }
        }



       

        ctl = new ClientSide_modules_commercial_usercontrols_ctl_quotation_entry();
        ctl = ctlquo;

        lblref = new Label();
        lblref = (Label)ctl.FindControl("lblref");

        lblreqtype = new Label();
        lblreqtype = (Label)ctl.FindControl("lblreqtype");

        lblitmcode = new Label();
        lblitmcode = (Label)ctl.FindControl("lblitmcode");

        lblqty = new Label();
        lblqty = (Label)ctl.FindControl("lblqty");

        lblproduct = new Label();
        lblproduct = (Label)ctl.FindControl("lblproduct");

        txtspecification = new TextBox();
        txtspecification = (TextBox)ctl.FindControl("txtspecification");

        txtbrand = new TextBox();
        txtbrand = (TextBox)ctl.FindControl("txtbrand");

        txtorigin = new TextBox();
        txtorigin = (TextBox)ctl.FindControl("txtorigin");

        txtpacking = new TextBox();
        txtpacking = (TextBox)ctl.FindControl("txtpacking");

        txtrate = new TextBox();
        txtrate = (TextBox)ctl.FindControl("txtrate");
                   
        tmp = lblreqtype.Text.Split(',');
        req_type = tmp[0].Trim();
        pur_type = tmp[1].Trim();

        tmp_str = txtparty.Text;
        tmp = tmp_str.Split(':');

        pcode = tmp[0];
        pdet = tmp[1].Trim();

        tmp_str = lblqty.Text;
        tmp = tmp_str.Split(' ');

        qnty = Convert.ToDouble(tmp[0]);
        uom = tmp[1];

        if (txtrate.Text != "")
        {
            if (srdet.GetDataByRefItem(lblref.Text, lblitmcode.Text)[0].In_Det_Status == "QUO")
            {
                max_seq = Convert.ToInt32(quo.GetMaxSeq_No(lblitmcode.Text, quoref, lblref.Text)) + 1;

                if (quo.ChkQuotationExistency(quoref, lblitmcode.Text, pcode).Count == 0)
                {
                    quo.InsertQuotation(quoref, max_seq, lblref.Text, req_type, pur_type, lblitmcode.Text, lblproduct.Text, uom, qnty, pcode, pdet, Convert.ToDecimal(txtrate.Text), 0, txtspecification.Text, txtbrand.Text, txtorigin.Text, txtpacking.Text, DateTime.Now, DateTime.Now, tac_ref_no, tac_ref_no, tac_ref_no, "", "", "");
                }
            }
        }




        Response.Redirect(Request.Url.AbsoluteUri); 
    }

    protected void gdgen_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            ((CheckBox)e.Row.FindControl("CheckBox1")).Attributes.Add("onclick", "javascript:SelectAll('" + ((CheckBox)e.Row.FindControl("CheckBox1")).ClientID + "','1')");
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((CheckBox)e.Row.FindControl("CheckBox1")).Attributes.Add("onClick", "ColorRow(this)");

        }


    }

    protected void gdspe_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Find the checkbox control in header and add an attribute
            ((CheckBox)e.Row.FindControl("CheckBox2")).Attributes.Add("onclick", "javascript:SelectAll('" + ((CheckBox)e.Row.FindControl("CheckBox2")).ClientID + "','2')");
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((CheckBox)e.Row.FindControl("CheckBox2")).Attributes.Add("onClick", "ColorRow(this)");

        }
    }

    protected void gdpay_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Find the checkbox control in header and add an attribute
            ((CheckBox)e.Row.FindControl("CheckBox3")).Attributes.Add("onclick", "javascript:SelectAll('" + ((CheckBox)e.Row.FindControl("CheckBox3")).ClientID + "','3')");
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((CheckBox)e.Row.FindControl("CheckBox3")).Attributes.Add("onClick", "ColorRow(this)");

        }
    }

    protected void btngoback_Click(object sender, EventArgs e)
    {
        Session[clsStatic.sessionQuotationRef] = null;
        Response.Redirect("./frm_quo_approval.aspx");
    }
}
