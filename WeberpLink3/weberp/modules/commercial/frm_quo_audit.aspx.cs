using System;
using System.IO;
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
using LibraryDTO;
using LibraryDAL.SCBLQryTableAdapters;
using LibraryDAL.SCBLDataSetTableAdapters;
using LibraryDAL.SCBL2DataSetTableAdapters;
using AjaxControlToolkit;

public partial class frm_quo_audit : System.Web.UI.Page
{
    
       

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication();
        
        tbltooltip.Visible = true;
        tblmaster.BgColor = "FFFFFFF";
        if (!Page.IsPostBack)
        {
            Session[clsStatic.sessionPartySelFlag] = "";

            if (Session[clsStatic.sessionCurrentRefFocus] == null)
            {

            }
            else
            {
                if (Session[clsStatic.sessionCurrentRefFocus].ToString() == "")
                {

                }
                else
                {
                  
                    Session[clsStatic.sessionCurrentRefFocus] = null;

                }
            }

            if ((Request.QueryString["qref"] != null)&&(Request.QueryString["mpr_ref"] != null)&&(Request.QueryString["icode"] != null))
            {
                generate_detail_data(Request.QueryString["qref"].ToString(), Request.QueryString["mpr_ref"].ToString(), Request.QueryString["icode"].ToString());
            }

        }
        else
        {
         
        }
                
        //generate_detail_data(ddllist.SelectedValue.ToString());
        
        tbltooltip.Visible = false;       

      
    }





    private void generate_detail_data(string qref, string mpr_ref, string icode)
    {
        tbl_tac_logTableAdapter log = new tbl_tac_logTableAdapter();
        SCBLDataSet.tbl_tac_logDataTable dtlog = new SCBLDataSet.tbl_tac_logDataTable();
        PuTr_IN_Det_Scbl2TableAdapter srdet = new PuTr_IN_Det_Scbl2TableAdapter();
        quotation_detTableAdapter quo = new quotation_detTableAdapter();        
        SCBLDataSet.quotation_detDataTable dt = new SCBLDataSet.quotation_detDataTable();
        SCBL2DataSet.PuTr_IN_Det_Scbl2Row dr;
        PuTr_PO_Det_ScblTableAdapter podet = new PuTr_PO_Det_ScblTableAdapter();
        SCBLDataSet.PuTr_PO_Det_ScblDataTable dtrecent = new SCBLDataSet.PuTr_PO_Det_ScblDataTable();

        ListItem lst;
//        string my_app = get_my_app();
        decimal qty;
        Label lbltooltip;
        Literal ltrl;
        els.ToolTip toolt;
        string  tac_ref, genstr, spestr, paystr, pay_type, sel_party_code;
        string[] tmp;
        int cur_index, i, gcnt, scnt, pcnt;
        int vdays = 0;
        HtmlInputRadioButton rdolist;
        Control ctl;


        if ((qref == "") || (mpr_ref == "") || (icode == "")) return;
              
        dr = srdet.GetDataByRefItem(mpr_ref, icode)[0];
                
       

        lblmpr.Text = dr.IN_Det_Ref.ToString();
        lblqref.Text = qref;
        lblproduct.Text = dr.IN_Det_Icode.ToString() + " : " + dr.IN_Det_Itm_Desc.ToString();
        lblqty.Text = dr.IN_Det_Lin_Qty.ToString() + " " + dr.IN_Det_Itm_Uom.ToString();
        lblreq.Text = dr.IN_Det_Code.ToString() + ", " + dr.In_Det_Pur_Type.ToString();

        //set recent price

        dtrecent = podet.GetDataforPriceLog(dr.IN_Det_Icode.ToString());
        ddlrecentlist.Items.Clear();
        ddlrecentlist.Visible = false;
       


        qty = (decimal)dr.IN_Det_Lin_Qty;
        sel_party_code = dr.In_Det_App_Party.ToString();
        
        //quotation


        dt = quo.GetQuotationByRef(qref);
        cur_index = 1;

        tbl_party.Rows[0].Cells[1].Visible = false;
        foreach (SCBLDataSet.quotation_detRow drquo in dt.Rows)
        {
            if (cur_index == 16) break;


            if (Session[clsStatic.sessionPartySelFlag].ToString() != qref)
            {
                if (drquo.party_code.ToString() == sel_party_code)
                {
                    rdolist = new HtmlInputRadioButton();
                    ctl = new Control();
                    ctl = tbl_party.Rows[cur_index].Cells[0].Controls[1];
                    rdolist = (HtmlInputRadioButton)ctl;
                    rdolist.Checked = true;
                }
                else
                {
                    rdolist = new HtmlInputRadioButton();
                    ctl = new Control();
                    ctl = tbl_party.Rows[cur_index].Cells[0].Controls[1];
                    rdolist = (HtmlInputRadioButton)ctl;
                    rdolist.Checked = false;
                }
            }


            tbl_party.Rows[cur_index].Cells[1].InnerText = drquo.party_code;
            tbl_party.Rows[cur_index].Cells[2].InnerText = drquo.party_det;
            
            //tooltip

            genstr = "";
            spestr = "";
            paystr = "";
            gcnt = 0;
            scnt = 0;
            pcnt = 0;

            tac_ref=drquo.gen_terms;
            dtlog = log.GetDataByRef(tac_ref);

            foreach (SCBLDataSet.tbl_tac_logRow drlog in dtlog.Rows)
            {
                vdays = drlog.valid_days;
                switch (drlog.tac_type)
                {
                    case "gen":
                        {
                            gcnt++;
                            genstr = genstr + gcnt.ToString() + ". " + drlog.content_det.ToString() + "</br>";
                            break;
                        }

                    case "spe":
                        {
                            scnt++;
                            spestr = spestr + scnt.ToString() + ". " + drlog.content_det.ToString() + "</br>";
                            break;
                        }

                    case "pay":
                        {
                            pcnt++;
                            paystr =paystr + pcnt.ToString() + ". " + drlog.content_det.ToString() + "</br>";
                            break;
                        }
                }
            }


            pay_type = "";

            if (log.GetDataByPayType(tac_ref, "pay", "full").Rows.Count > 0)
                pay_type = "full";
            else if (log.GetDataByPayType(tac_ref, "pay", "part").Rows.Count > 0)
                pay_type = "part";
            else
                pay_type = "no";
            

            tbltooltip.Rows[0].Cells[0].InnerText = drquo.party_det.ToUpper();
            tbltooltip.Rows[2].Cells[1].InnerHtml = genstr;
            tbltooltip.Rows[3].Cells[1].InnerHtml = spestr;
            tbltooltip.Rows[4].Cells[0].InnerHtml = "Pay Terms(" + pay_type + ")";
            tbltooltip.Rows[4].Cells[1].InnerHtml = paystr;
            tbltooltip.Rows[5].Cells[1].InnerHtml = vdays.ToString();
          
            lbltooltip = new Label();
            lbltooltip.ForeColor = System.Drawing.Color.Black;
            lbltooltip.Text = cur_index.ToString()+".";
            tbl_party.Rows[cur_index].Cells[0].Controls.Add(lbltooltip);                
            ltrl = new Literal();                
            toolt = new els.ToolTip();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            tbltooltip.RenderControl(hw);
            toolt.Add(lbltooltip, ltrl, sb.ToString());
           // toolt.Add(tbl_party.Rows[cur_index].Cells[2], ltrl, sb.ToString());
            toolt.Build();               
            tbl_party.Rows[cur_index].Cells[0].Controls.Add(ltrl);
                            
            tbl_party.Rows[cur_index].Cells[3].InnerText = drquo.rate.ToString("N2");
            tbl_party.Rows[cur_index].Cells[4].InnerText = (drquo.rate * qty).ToString("N2");
            tbl_party.Rows[cur_index].Cells[5].InnerText = drquo.specification;
            tbl_party.Rows[cur_index].Cells[6].InnerText = drquo.product_brand;
            tbl_party.Rows[cur_index].Cells[7].InnerText = drquo.origin;
            tbl_party.Rows[cur_index].Cells[8].InnerText = drquo.packing;

            tbl_party.Rows[cur_index].Cells[1].Visible = false;
            tbl_party.Rows[cur_index].Visible = true;

            cur_index += 1;
        }
        Session[clsStatic.sessionPartySelFlag] = qref;
        
        for (i = cur_index; i <= 15; i++)
        {
            rdolist = new HtmlInputRadioButton();
            ctl = new Control();
            ctl = tbl_party.Rows[cur_index].Cells[0].Controls[1];
            rdolist = (HtmlInputRadioButton)ctl;
            rdolist.Checked = false;

            tbl_party.Rows[i].Visible = false;
        }

        generate_comments(qref);
    }

     

   

   

    private int GetSelectedIndex()
    {
        int indx = -1;
        int i;

        HtmlInputRadioButton rdolist;
        Control ctl;

        for (i = 1; i < 11; i++)
        {
            rdolist = new HtmlInputRadioButton();
            ctl = new Control();

            ctl = tbl_party.Rows[i].Cells[0].Controls[1];

            rdolist = (HtmlInputRadioButton)ctl;

            if (rdolist.Checked) { indx = i; }
        }

        return indx;

    }


    private void generate_comments(string ref_no)
    {
        tbl_CommentsTableAdapter com = new tbl_CommentsTableAdapter();
        SCBLDataSet.tbl_CommentsDataTable dt = new SCBLDataSet.tbl_CommentsDataTable();

        dt = com.GetCommentsByRef(ref_no);
        phcomments.Controls.Clear();
        foreach (SCBLDataSet.tbl_CommentsRow dr in dt.Rows)
        {
            ClientSide_modules_commercial_usercontrols_ctl_comments ctl = (ClientSide_modules_commercial_usercontrols_ctl_comments)LoadControl("./usercontrols/ctl_comments.ascx");
            Label lblname = (Label)ctl.FindControl("lblname");
            Label lbldate = (Label)ctl.FindControl("lbldate");
            HtmlTableCell celcomm = (HtmlTableCell)ctl.FindControl("celcomm");
            Image imgimage = (Image)ctl.FindControl("imgimage");

            imgimage.ImageUrl = "~/handler/hndImage.ashx?id=" + dr.app_id;

            ctl.ID = "ctl_" + phcomments.Controls.Count.ToString();

            lblname.Text = dr.app_name.ToString() + " (" + dr.app_designation.ToString() + ")";
            lbldate.Text = dr.app_date.ToString();
            celcomm.InnerText = dr.gen_comments.ToString();

            switch (dr.cur_status)
            {
                case "RUN":
                    lblstatus.Text = "INITIATED";
                    break;
                case "QUO":
                    lblstatus.Text = "RUNNING";
                    break;

                case "APP":
                    lblstatus.Text = "APPROVED";
                    break;

                case "TEN":
                    lblstatus.Text = "REJECTED";                    
                    break;
                case "REJ":
                    lblstatus.Text = "REJECTED";
                    break;
            }

            phcomments.Controls.Add(ctl);
        }

    }

   
    
    protected void btneditval_Click(object sender, EventArgs e)
    {

    }
    protected void btnedit_Click(object sender, EventArgs e)
    {

    }

   
    
}
