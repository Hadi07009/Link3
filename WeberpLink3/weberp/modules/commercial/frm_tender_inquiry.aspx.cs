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
using LibraryDAL.SCBLDataSetTableAdapters;
using LibraryDAL.SCBL2DataSetTableAdapters;
using LibraryDAL.ErpDataSetTableAdapters;
using FreeTextBoxControls;

public partial class frm_tender_inquiry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication();
        Generate_Items();        
        tblmaster.BgColor = "FFFFFFF";

        if (!Page.IsPostBack)
        {            
            txtdate.Text = DateTime.Now.ToShortDateString();
            txtfrom.Text = current.UserName.ToString();
            txtsub.Text = "Request for submission of Price Quotation";         
            load_tandc_gen();
            load_tandc_spe();
            load_tandc_pay("full");
            ddlpayterms.SelectedValue = "full";
        }
        else
        {

        }          
    }

    private void load_tandc_gen()
    {
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
        tbl_tac_detTableAdapter tac = new tbl_tac_detTableAdapter();
        SCBLDataSet.tbl_tac_detDataTable dt = new SCBLDataSet.tbl_tac_detDataTable();
        //dt = tac.GetDataByType2("pay", "com", pay_type);
        dt = tac.GetDataByTypeCat("pay",pay_type);     
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

    private void Generate_Items()
    {
        SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable itm = new SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable();
        PuTr_IN_Det_Scbl2TableAdapter det = new PuTr_IN_Det_Scbl2TableAdapter();
        string plnts = "Plants: ";
        int i, len, cnt, indx;
        string[] plant_list = get_plant("TEN");
        
        if (plant_list == null)
        {
            lblplant.Text = "";
            return;
        }

        len = plant_list.Length;

        for (i = 0; i < len; i++)
        {
            if (plant_list[i].ToString() != "")
                plnts = plnts + plant_list[i].ToString() + ", ";
        }

        lblplant.Text = plnts;

        itm = det.GetDataByReqStatus("LPO", "TEN");

        cnt = itm.Rows.Count;
        for (indx = cnt; indx > 0; indx--)
        {

            for (i = 0; i < len; i++)
            {
                if (itm[indx - 1].IN_Det_Code.Substring(0, 2) == plant_list[i])
                    goto nextcheck1;
            }
            itm.RemovePuTr_IN_Det_Scbl2Row(itm[indx - 1]);

        nextcheck1: ;
        }



        if (itm.Rows.Count < 1)
        {
            lblnodata.Visible = true;
            return;
        }
        else
        {

            int slno=0;
            string itemdet;
            CheckBox chk;
            TextBox qty, brand, origin, specification, remarks;
           
            HtmlTableRow hrow;

            lblnodata.Visible = false;


            foreach (SCBL2DataSet.PuTr_IN_Det_Scbl2Row dr in itm.Rows)
            {
                slno=slno+1;

                chk = new CheckBox();
                qty = new TextBox();
                brand = new TextBox();
                origin = new TextBox();
                specification = new TextBox();
                remarks = new TextBox();
                
                
                qty.Text = dr.IN_Det_Lin_Qty.ToString() + " " + dr.IN_Det_Itm_Uom.ToString();
                qty.Width = 80;
                brand.Text = dr.In_Det_Brand.ToString();
                brand.Width = 80;
                origin.Text = dr.In_Det_Origin.ToString();
                origin.Width = 80;
                specification.Text = dr.In_Det_Specification.ToString();
                specification.Width = 80;
                remarks.Text = dr.In_Det_Remarks.ToString();
                remarks.Width = 80;


                itemdet = dr.IN_Det_Itm_Desc.ToString();

                
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
                hrow.Cells.Add(new HtmlTableCell());

                hrow.Cells[0].Controls.Add(chk);
                hrow.Cells[1].InnerText = slno.ToString();
                hrow.Cells[2].InnerText = dr.IN_Det_Ref.ToString();
                hrow.Cells[3].InnerText = dr.IN_Det_Icode;
                hrow.Cells[4].InnerText = itemdet.ToString();
                hrow.Cells[5].Controls.Add(qty);
                hrow.Cells[6].Controls.Add(brand);
                hrow.Cells[7].Controls.Add(origin);
                hrow.Cells[8].Controls.Add(specification);
                hrow.Cells[9].Controls.Add(remarks);

                tblhtml.Rows.Add(hrow);

            }
                        
        }

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
    
    private void readyData(string adr_code)
    {
        PuMa_Par_AdrTableAdapter adr = new PuMa_Par_AdrTableAdapter();
        ErpDataSet.PuMa_Par_AdrRow row;
       
        string[] tempdata;
        CheckBox chk;
        FreeTextBox txt;      
        int indx,seqno,gsl;
        clsTandC[] tac = new clsTandC[60];


        row = adr.GetDataByAdrCode(adr_code)[0];
        tempdata = new string[13];       

        tempdata[0] = txtdate.Text;
        tempdata[1] = row.par_adr_name;
        tempdata[2] = txtsub.Text;
        tempdata[3] = txtfrom.Text;


        //general terms
        gsl = 0;
        indx = 0;
        seqno = 0;
        foreach (GridViewRow gr in gdgen.Rows)
        {
            chk = new CheckBox();
            txt = new FreeTextBox();

            txt = (FreeTextBox)gr.FindControl("TextBox1");
            chk = (CheckBox)gr.FindControl("CheckBox1");
            seqno++;
            
            if (chk.Checked)
            {
                tac[gsl]=new clsTandC();
                tac[gsl].Sl_no = gsl;
                tac[gsl].Type = "gen";
                tac[gsl].Tem_seq = seqno;
                tac[gsl].Type_seq = Convert.ToInt32(chk.Text);
                tac[gsl].Seq = indx;
                tac[gsl].Data = txt.Text;
                indx++;
                gsl++;
            }            
        }              

        //special terms
        indx = 0;
        seqno = 0;
        foreach (GridViewRow gr in gdspe.Rows)
        {
            chk = new CheckBox();
            txt = new FreeTextBox();

            txt = (FreeTextBox)gr.FindControl("TextBox2");
            chk = (CheckBox)gr.FindControl("CheckBox2");
            seqno++;

            if (chk.Checked)
            {
                tac[gsl] = new clsTandC();
                tac[gsl].Type = "spe";
                tac[gsl].Tem_seq = seqno;                
                tac[gsl].Type_seq = Convert.ToInt32(chk.Text);
                tac[gsl].Seq = indx;
                tac[gsl].Data = txt.Text;
                indx++;
                gsl++;
            }
        }
        

        //pay terms
        indx = 0;
        seqno = 0;
        foreach (GridViewRow gr in gdpay.Rows)
        {
            chk = new CheckBox();
            txt = new FreeTextBox();

            txt = (FreeTextBox)gr.FindControl("TextBox3");
            chk = (CheckBox)gr.FindControl("CheckBox3");
            seqno++;

            if (chk.Checked)
            {
                tac[gsl] = new clsTandC();
                tac[gsl].Type = "pay";
                //tac[gsl].Tem_seq = seqno;
                tac[gsl].Tem_seq = Convert.ToInt32(chk.Text);
                tac[gsl].Type_seq = Convert.ToInt32(chk.Text);
                tac[gsl].Seq = indx;
                tac[gsl].Data = txt.Text;
                indx++;
                gsl++;
            }
        }

        tempdata[4] = ddlpayterms.SelectedItem.Value.ToString();
        tempdata[5] = ddlpayterms.SelectedItem.Text;
        tempdata[6] = txtpartydet.Text;
        tempdata[7] = row.Par_Adr_Line_1 + " " + row.Par_Adr_Line_2 + " " + row.Par_Adr_Line_3 + " " + row.Par_Adr_Line_4 + " " + row.Par_Adr_Line_5;
        tempdata[8] = row.Par_Adr_Tel_No;
        tempdata[9] = row.Par_Adr_Fax_No;
        tempdata[10] = row.Par_Adr_Email_Id;
             
        Session[clsStatic.sessionTempPrintData] = tempdata;
        Session[clsStatic.sessionTempHtmlTable] = tblhtml;
        Session[clsStatic.sessionTermsandCond] = tac;
        
    }
    protected void btnproceed_Click(object sender, EventArgs e)
    {
        string[] tmp;
        tmp = txtpartydet.Text.Split(':');
        if (tmp.Length < 2) return;
        string acc_code = tmp[0];

        readyData(acc_code);
        Response.Redirect("./frm_tender_inquiry_view.aspx");

        
    }
    protected void ddlpayterms_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        load_tandc_pay(ddlpayterms.SelectedItem.Value.ToString());
    }



    
}

