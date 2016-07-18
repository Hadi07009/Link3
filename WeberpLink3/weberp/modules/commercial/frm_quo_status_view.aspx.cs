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

public partial class frm_quo_status_view : System.Web.UI.Page
{
    
       

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication();
        
        tbltooltip.Visible = true;
        tblmaster.BgColor = "FFFFFFF";
        if (!Page.IsPostBack)
        {
                      

            get_plant();
            ddllist_SelectedIndexChanged(sender, e);
            

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
                    ddllist.SelectedValue = Session[clsStatic.sessionCurrentRefFocus].ToString();
                   
                    Session[clsStatic.sessionCurrentRefFocus] = null;
                    
                }
            }


        }
        else
        {
         
        }
                
        //generate_detail_data(ddllist.SelectedValue.ToString());
        
        tbltooltip.Visible = false;       

      
    }

    protected void ddlplants_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblcount.Visible = true;

        ListItem lst;

        ddllist.Items.Clear();
        ddllist.Items.Add("");
        if (ddlplants.Text == "") { ddllist_SelectedIndexChanged(sender, e); lblcount.Visible = false; return; }
        quotation_det_for_statusTableAdapter quo = new quotation_det_for_statusTableAdapter();
        SCBLQry.quotation_det_for_statusDataTable dt = new SCBLQry.quotation_det_for_statusDataTable();
        dt = quo.GetCsStatus(ddlplants.Text);
        foreach (SCBLQry.quotation_det_for_statusRow dr in dt.Rows)
        {
            lst = new ListItem();
            lst.Text = dr.requisition_id + ":" + dr.ref_no + ":" + dr.product_det;
            lst.Value = dr.ref_no + ":" + dr.requisition_id + ":" + dr.product_code;
            ddllist.Items.Add(lst);
        }

        lblcount.Text = "(" + dt.Rows.Count.ToString() +")";

        ddllist_SelectedIndexChanged(sender, e);

    }

    

   


    private void generate_detail_data(string icode_quoref)
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
        string  icode, qref, tac_ref, genstr, spestr, paystr, pay_type, sel_party_code, mpr_ref;
        string[] tmp;
        int cur_index, i, gcnt, scnt, pcnt;
        int vdays = 0;
        HtmlInputRadioButton rdolist;
        Control ctl;


        if (icode_quoref == "") return;

        tmp=icode_quoref.Split(':');
        icode = tmp[2].ToString();
        qref = tmp[0].ToString();
        mpr_ref = tmp[1].ToString();

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


    private void get_plant()
    {
        ddlplants.Items.Clear();
        ddlplants.Items.Add("");

        User_Role_DefinitionTableAdapter urole = new User_Role_DefinitionTableAdapter();
        SCBLDataSet.User_Role_DefinitionDataTable udt = new SCBLDataSet.User_Role_DefinitionDataTable();
        string[] plant_list;
        udt = urole.GetDataByUserCodeRole(current.UserId.ToString(), "QEN");

        if (udt.Rows.Count == 0) { Response.Redirect("./frm_com_inbox.aspx");  }

        if (udt.Rows.Count > 0)
            plant_list = udt[0].plant_list.Split(',');
        else
            return;

        foreach (string str in plant_list)
        {
            if (str != "") ddlplants.Items.Add(str);
        }

    }

  

    

    protected void ddllist_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selval = ddllist.SelectedValue.ToString();

        if (selval == "")
        {
            tblquotation.Visible = false; return;
        }
        else
            tblquotation.Visible = true;

        generate_detail_data(ddllist.SelectedValue.ToString());

        //initialize_page();
               

        //string my_app = get_my_app();
        //PuTr_IN_Det_ScblTableAdapter indet = new PuTr_IN_Det_ScblTableAdapter();
        //SCBLDataSet.PuTr_IN_Det_ScblRow dr;
        //string icode, qref;
        //string[] tmp;

        //tmp = selval.Split(':');
        //icode = tmp[0].ToString();
        //qref = tmp[1].ToString();

        //dr = indet.GetQuoByItem("QUO", my_app, icode, qref)[0];

               
        
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

    protected void btnaddquo_Click(object sender, EventArgs e)
    {
        string qref;
        string[] tmp;
        tmp = lblproduct.Text.Split(':');       
        qref = lblqref.Text;
        Session[clsStatic.sessionQuotationRef] = qref;
        Session[clsStatic.sessionCurrentRefFocus] = ddllist.SelectedValue.ToString();
        Response.Redirect("./frm_quotation_add_cs.aspx");
    }
    
}
