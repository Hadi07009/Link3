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

public partial class frm_quotation_add_ddl : System.Web.UI.Page
{
   

    protected void Page_Load(object sender, EventArgs e)
    {
        
        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";

        if (!Page.IsPostBack)
        {            
            get_master_tac(Session[clsStatic.sessionSelvalforQuo].ToString(), ddlpayterms.SelectedValue.ToString());            
            set_data_from_ddl(Session[clsStatic.sessionSelvalforQuo].ToString(), true);
            tbltac.Visible = false;
            btnupdate.Visible = false;
    
        }
        else
        {
            set_data_from_ddl(Session[clsStatic.sessionSelvalforQuo].ToString(), false);   
        }
          
    }

    private void get_master_tac(string sel_val,string pay_type)
    {
        load_tandc_gen(sel_val);
        load_tandc_spe(sel_val);
        load_tandc_pay(sel_val, pay_type);
    }

    private void load_tandc_gen(string sel_val)
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
                         
            logdt = log.GetDataByIdTypeSeq(sel_val, "gen", dr.tac_seq_no);
            if (logdt.Rows.Count > 0)
            {
                chk.Checked = true;
                txt.Text = logdt[0].content_det;
                gdgen.Rows[Convert.ToInt16(dr.tac_seq_no) - 1].BackColor = System.Drawing.ColorTranslator.FromHtml("#88AAFF");
            }           
        }
    }

    private void load_tandc_spe(string sel_val)
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
            
            logdt = log.GetDataByIdTypeSeq(sel_val, "spe", dr.tac_seq_no);
            if (logdt.Rows.Count > 0)
            {
                chk.Checked = true;
                txt.Text = logdt[0].content_det;
                gdspe.Rows[Convert.ToInt16(dr.tac_seq_no) - 1].BackColor = System.Drawing.ColorTranslator.FromHtml("#88AAFF");
            }         

        }
    }
    private void load_tandc_pay(string sel_val, string pay_type)
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
            
            logdt = log.GetDataByIdTypeSeq(sel_val, "pay", dr.tac_seq_no);
            if (logdt.Rows.Count > 0)
            {
                chk.Checked = true;
                txt.Text = logdt[0].content_det;
                gdpay.Rows[Convert.ToInt16(seq) - 1].BackColor = System.Drawing.ColorTranslator.FromHtml("#88AAFF");
            }           

        }
    }

    private string[] get_plant(string apptype)
    {
        User_Role_DefinitionTableAdapter urole = new User_Role_DefinitionTableAdapter();
        SCBLDataSet.User_Role_DefinitionDataTable udt = new SCBLDataSet.User_Role_DefinitionDataTable();
        string[] plant_list;
        udt = urole.GetDataByUserCodeRole(current.UserId.ToString(), apptype);

        if (udt.Rows.Count > 0)
            plant_list = udt[0].plant_list.Split(',');
        else
            return null;

        return plant_list;
    }
    

    //private void set_data_from_cmd(string itm_list)
    //{
    //    PuTr_IN_Det_ScblTableAdapter quot = new PuTr_IN_Det_ScblTableAdapter();
    //    SCBLDataSet.PuTr_IN_Det_ScblRow drdet;
        
    //    string[] tmparr;        
    //    int cnt, i;
    //    int tot = 0;
    //    string my_app ="TEN";      
    //    tmparr = itm_list.Split(':');     

    //    cnt = tmparr.Length;

    //    celquo.Controls.Clear();

    //    for (i = 0; i < cnt; i++)
    //    {

    //        if (tmparr[i] != "")
    //        {
    //            if (quot.GetDataByItemStatus(tmparr[i], my_app).Rows.Count > 0)
    //            {
    //                tot += 1;
    //                ClientSide_modules_commercial_usercontrols_ctl_quotation_entry ctl = (ClientSide_modules_commercial_usercontrols_ctl_quotation_entry)LoadControl("./usercontrols/ctl_quotation_entry.ascx");

    //                Label lblreqtype = (Label)ctl.FindControl("lblreqtype");
    //                Label lblitmcode = (Label)ctl.FindControl("lblitmcode");
    //                Label lblsl = (Label)ctl.FindControl("lblsl");
    //                Label lblproduct = (Label)ctl.FindControl("lblproduct");
    //                Label lblqty = (Label)ctl.FindControl("lblqty");
    //                Label lbltk = (Label)ctl.FindControl("lbltk");

    //                HtmlTableCell celbrand = (HtmlTableCell)ctl.FindControl("celbrand");
    //                HtmlTableCell celorigin = (HtmlTableCell)ctl.FindControl("celorigin");
    //                HtmlTableCell celpacking = (HtmlTableCell)ctl.FindControl("celpacking");

    //                ctl.ID = "ctl_entry" + celquo.Controls.Count.ToString();

    //                drdet = quot.GetDataByItemStatus(tmparr[i], my_app)[0];

    //                lblreqtype.Text = drdet.IN_Det_Code.ToString() + ", " + drdet.In_Det_Pur_Type.ToString();
    //                lblsl.Text = tot.ToString() + ".";
    //                lblitmcode.Text = tmparr[i];
    //                lblproduct.Text = drdet.IN_Det_Itm_Desc;
    //                lblqty.Text = drdet.IN_Det_Lin_Qty + " " + drdet.IN_Det_Itm_Uom;
    //                lbltk.Text = "Tk. /" + drdet.IN_Det_Itm_Uom;

    //                celbrand.InnerText = drdet.In_Det_Brand.ToString();
    //                celorigin.InnerText = drdet.In_Det_Origin.ToString();
    //                celpacking.InnerText = drdet.In_Det_Packing.ToString();


    //                celquo.Controls.Add(ctl);
    //            }
    //        }

    //    }

    //}

    private void addforqentry(SCBL2DataSet.PuTr_IN_Det_Scbl2Row drdet, int tot)
    {
        ClientSide_modules_commercial_usercontrols_ctl_quotation_entry ctl = (ClientSide_modules_commercial_usercontrols_ctl_quotation_entry)LoadControl("./usercontrols/ctl_quotation_entry.ascx");

        Label lblref = (Label)ctl.FindControl("lblref");
        Label lblreqtype = (Label)ctl.FindControl("lblreqtype");
        Label lblitmcode = (Label)ctl.FindControl("lblitmcode");
        Label lblsl = (Label)ctl.FindControl("lblsl");
        Label lblproduct = (Label)ctl.FindControl("lblproduct");
        Label lblqty = (Label)ctl.FindControl("lblqty");
        Label lbltk = (Label)ctl.FindControl("lbltk");

        TextBox celspecification = (TextBox)ctl.FindControl("txtspecification");
        TextBox celbrand = (TextBox)ctl.FindControl("txtbrand");
        TextBox celorigin = (TextBox)ctl.FindControl("txtorigin");
        TextBox celpacking = (TextBox)ctl.FindControl("txtpacking");

        ctl.ID = "ctl_entry" + celquo.Controls.Count.ToString();

        lblref.Text = drdet.IN_Det_Ref;
        lblreqtype.Text = drdet.IN_Det_Code.ToString() + ", " + drdet.In_Det_Pur_Type.ToString();
        lblsl.Text = tot.ToString() + ".";
        lblitmcode.Text = drdet.IN_Det_Icode.ToString();
        lblproduct.Text = drdet.IN_Det_Itm_Desc;
        lblqty.Text = drdet.IN_Det_Lin_Qty + " " + drdet.IN_Det_Itm_Uom;
        lbltk.Text = "Tk. /" + drdet.IN_Det_Itm_Uom;

        celspecification.Text = drdet.In_Det_Specification + " " + drdet.In_Det_Remarks;
        celbrand.Text = drdet.In_Det_Brand.ToString();
        celorigin.Text = drdet.In_Det_Origin.ToString();
        celpacking.Text = drdet.In_Det_Packing.ToString();

        celquo.Controls.Add(ctl);
    }

    private void set_data_from_ddl(string logid, bool ftime)
    {
        PuTr_IN_Det_Scbl2TableAdapter srdet = new PuTr_IN_Det_Scbl2TableAdapter();
        SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable dtdet = new SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable();
       
        quotation_logTableAdapter qlog = new quotation_logTableAdapter();
        SCBLDataSet.quotation_logRow logdr;
        
        logdr = qlog.GetDataById(logid)[0];
        string[] tmparr,tmp;
        string pcode,pdet,ref_no,icode;
        int cnt,i, pcnt,pi;
        int tot = 0;
        string[] plant_list = get_plant("QEN");
        cnt = plant_list.Length;

        pcode = logdr.party_code.ToString();
        pdet = logdr.party_name.ToString();
        tmparr = logdr.item_code_det.Split('+');


        dtdet = srdet.GetDataByReqStatus("LPO", "TEN");
        pcnt = dtdet.Rows.Count;
        for (pi = pcnt; pi > 0; pi--)
        {

            for (i = 0; i < cnt; i++)
            {
                if (dtdet[pi - 1].IN_Det_Code.Substring(0, 2) == plant_list[i])
                    goto nextcheck1;

            }
            dtdet.RemovePuTr_IN_Det_Scbl2Row(dtdet[pi - 1]);


        nextcheck1: ;
        }



        if (ftime)
            txtparty.Text = pcode + ": " + pdet;

        cnt = tmparr.Length;

        celquo.Controls.Clear();

        foreach (SCBL2DataSet.PuTr_IN_Det_Scbl2Row dr in dtdet.Rows)
        {
            for (i = 0; i < cnt; i++)
            {
                if (tmparr[i] != "")
                {
                    tmp = tmparr[i].Split(':');
                    ref_no = tmp[0];
                    icode = tmp[1];
                    if (icode == dr.IN_Det_Icode)
                    {
                        tot++;
                        addforqentry(dr,tot);
                        goto nextchk;
                    }

                }
            }
        nextchk: ;

        }
                

        if (tot == 0) btnshow.Visible = false;

    }

    private void ddlchange()
    {        
        load_tandc_pay(Session[clsStatic.sessionSelvalforQuo].ToString(), ddlpayterms.SelectedValue.ToString());        
    }


    protected void ddlpayterms_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlchange();        
    }
    
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        PuTr_IN_Det_Scbl2TableAdapter srdet = new PuTr_IN_Det_Scbl2TableAdapter();

        tbl_tac_logTableAdapter taclog = new tbl_tac_logTableAdapter();
        quotation_detTableAdapter quo = new quotation_detTableAdapter();
        //quotation_logTableAdapter log = new quotation_logTableAdapter();

        int i, cnt, max_seq, len, indx;
        string tmp_str, pcode, pdet, uom, req_type, pur_type, tac_ref_no;
        string[] tmp;
        double qnty, max_ref;
        CheckBox chk;
        FreeTextBox txt;

        ClientSide_modules_commercial_usercontrols_ctl_quotation_entry ctl;
        TextBox txtspecification, txtbrand, txtorigin, txtpacking, txtrate;
        Label lblref,lblitmcode, lblproduct, lblqty, lblreqtype;

        //check valid days entry
        if (txtvaliddays.Text == "") return;

        if (Convert.ToInt32(txtvaliddays.Text) < 1) return;

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
                taclog.Inserttac(tac_ref_no, "QEN", "gen", "", Convert.ToInt32(chk.Text), indx, txt.Text, Convert.ToInt32(txtvaliddays.Text),"");
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



        cnt = celquo.Controls.Count;

        for (i = 0; i < cnt; i++)
        {

            ctl = new ClientSide_modules_commercial_usercontrols_ctl_quotation_entry();
            ctl = (ClientSide_modules_commercial_usercontrols_ctl_quotation_entry)celquo.Controls[i];

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

                if (srdet.GetDataByRefItem(lblref.Text, lblitmcode.Text)[0].In_Det_Status == "TEN")
                {
                    max_seq = Convert.ToInt32(quo.GetMaxSeq_No(lblitmcode.Text, "", lblref.Text)) + 1;

                    if (quo.ChkQuotationExistency("", lblitmcode.Text, pcode).Count == 0)
                    {
                        quo.InsertQuotation("", max_seq, lblref.Text, req_type, pur_type, lblitmcode.Text, lblproduct.Text, uom, qnty, pcode, pdet, Convert.ToDecimal(txtrate.Text), 0, txtspecification.Text, txtbrand.Text, txtorigin.Text, txtpacking.Text, DateTime.Now, DateTime.Now, tac_ref_no, tac_ref_no, tac_ref_no, "", "", "");
                    }
                }
            }
        }

        Response.Redirect("./frm_quotation_entry.aspx");
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        tbl_tac_logTableAdapter log = new tbl_tac_logTableAdapter();

        string pay_type;

        if (log.GetDataByPayType(Session[clsStatic.sessionSelvalforQuo].ToString(), "pay", "full").Rows.Count > 0)
            pay_type = "full";
        else
            if (log.GetDataByPayType(Session[clsStatic.sessionSelvalforQuo].ToString(), "pay", "part").Rows.Count > 0)
                pay_type = "part";
            else
                pay_type = "no";

        ddlpayterms.SelectedValue = pay_type;

        ddlchange();

        tbltac.Visible = true;
        btnupdate.Visible = true;

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
