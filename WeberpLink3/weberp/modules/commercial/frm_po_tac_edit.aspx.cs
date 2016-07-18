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
using FreeTextBoxControls;


public partial class frm_po_tac_edit : System.Web.UI.Page
{ 
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string po_ref = Request.QueryString["po_ref"].ToString();
            load_party(po_ref);
            load_tandc_gen(po_ref);
            load_tandc_spe(po_ref);
            load_tandc_pay(po_ref);
            load_valid_days(po_ref);
            tbltac.Visible = false;
            btnupdate.Visible = false;

        }
        else
        {

        }
          
    }
    private void load_valid_days(string tac_log_id)
    {
        tbl_tac_logTableAdapter log = new tbl_tac_logTableAdapter();
        txtvaliddays.Text = log.GetDataByRef(tac_log_id)[0].valid_days.ToString();
    }
    private void load_tandc_gen(string tac_log_id)
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
           
            logdt = log.GetDataByIdTypeSeq(tac_log_id, "gen", dr.tac_seq_no);
            if (logdt.Rows.Count > 0)
            {
                chk.Checked = true;
                txt.Text = logdt[0].content_det;
                gdgen.Rows[Convert.ToInt16(dr.tac_seq_no) - 1].BackColor = System.Drawing.ColorTranslator.FromHtml("#88AAFF");
            }
            
        }
    }

    private void load_tandc_spe(string tac_log_id)
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

            logdt = log.GetDataByIdTypeSeq(tac_log_id, "spe", dr.tac_seq_no);
            if (logdt.Rows.Count > 0)
            {
                chk.Checked = true;
                txt.Text = logdt[0].content_det;
                gdspe.Rows[Convert.ToInt16(dr.tac_seq_no) - 1].BackColor = System.Drawing.ColorTranslator.FromHtml("#88AAFF");
            }

        }


    }
    private void load_tandc_pay(string tac_log_id)
    {
        tbl_tac_logTableAdapter log = new tbl_tac_logTableAdapter();
        SCBLDataSet.tbl_tac_logDataTable logdt = new SCBLDataSet.tbl_tac_logDataTable();

        tbl_tac_detTableAdapter tac = new tbl_tac_detTableAdapter();
        SCBLDataSet.tbl_tac_detDataTable dt = new SCBLDataSet.tbl_tac_detDataTable();
               

        dt = tac.GetDataByType2("pay", "com", ddlpayterms.SelectedValue.ToString());
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

            logdt = log.GetDataByIdTypeSeq(tac_log_id, "pay", dr.tac_seq_no);
            if (logdt.Rows.Count > 0)
            {
                chk.Checked = true;
                txt.Text = logdt[0].content_det;
                gdpay.Rows[Convert.ToInt16(seq) - 1].BackColor = System.Drawing.ColorTranslator.FromHtml("#88AAFF");
            }

        }


    }

    private void load_party(string tac_log_id)
    {
        tbl_tac_logTableAdapter log = new tbl_tac_logTableAdapter();
        PuTr_PO_Hdr_ScblTableAdapter hdr = new PuTr_PO_Hdr_ScblTableAdapter();
        SCBLDataSet.PuTr_PO_Hdr_ScblDataTable dt = new SCBLDataSet.PuTr_PO_Hdr_ScblDataTable();
        dt = hdr.GetHdrDataByRef(tac_log_id);
        if (dt.Rows.Count > 0)
        {
            txtid.Text = tac_log_id;
            txtparty.Text = dt[0].PO_Hdr_Pcode.ToString() + ": " + dt[0].PO_Hdr_Com1.ToString();
        }

        if (log.GetDataByRef(tac_log_id).Rows.Count == 0)
        {
            btnshow.Visible = false;
            btnback.Visible = true;
        }
        else
        {
            btnshow.Visible = true;
            btnback.Visible = false;
        }

    }

    private void ddlchange()
    {
        load_tandc_pay(txtid.Text);
    }
    
    
    protected void ddlpayterms_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlchange();
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        tbl_tac_logTableAdapter taclog = new tbl_tac_logTableAdapter();
        quotation_detTableAdapter quo = new quotation_detTableAdapter();
        CheckBox chk;
        FreeTextBox txt; 
        int indx;
        string tac_ref_no = txtid.Text;
        //check valid days entry
        if (txtvaliddays.Text == "") return;
        if (Convert.ToInt32(txtvaliddays.Text) < 1) return;

        if (tac_ref_no == "") return;

        taclog.DeleteTacById(tac_ref_no);

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

        Response.Redirect("./frm_po_revising_app.aspx");

    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        tbl_tac_logTableAdapter log = new tbl_tac_logTableAdapter();        
        string pay_type = "";
        string tac_log_id = txtid.Text;

        if (log.GetDataByPayType(tac_log_id, "pay", "full").Rows.Count > 0)
            pay_type = "full";
        else if (log.GetDataByPayType(tac_log_id, "pay", "part").Rows.Count > 0)
            pay_type = "part";
        else
            pay_type = "no";

        ddlpayterms.SelectedValue = pay_type;
        
        ddlchange();
        
        tbltac.Visible = true;
        btnupdate.Visible = true;


    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("./frm_po_revising_app.aspx");
    }

    protected void gdgen_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((CheckBox)e.Row.FindControl("CheckBox1")).Attributes.Add("onClick", "ColorRow(this)");

            //if (((CheckBox)e.Row.FindControl("CheckBox1")).Checked)
            //    e.Row.BackColor = System.Drawing.Color.Blue; // System.Drawing.ColorTranslator.FromHtml("#88AAFF"); 
            //else
            //    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#88AAFF");  //#F8E5A1
        }


    }

    protected void gdspe_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((CheckBox)e.Row.FindControl("CheckBox2")).Attributes.Add("onClick", "ColorRow(this)");

        }
    }

    protected void gdpay_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((CheckBox)e.Row.FindControl("CheckBox3")).Attributes.Add("onClick", "ColorRow(this)");

        }
    }
}

