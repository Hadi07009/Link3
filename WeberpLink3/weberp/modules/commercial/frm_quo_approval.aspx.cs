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
using LibraryDAL.SCBLDataSetTableAdapters;
using LibraryDAL.ErpDataSetTableAdapters;
using AjaxControlToolkit;

public partial class frm_quo_approval : System.Web.UI.Page
{
    
       

    protected void Page_Load(object sender, EventArgs e)
    {
       

        clsStatic.CheckUserAuthentication();
        clsStatic.MsgConfirmBox(btnforward, "Are you sure to Forward this items ?");
        clsStatic.MsgConfirmBox(btnreject, "Are you sure to Reject this items ?");
        clsStatic.MsgConfirmBox(btnapp, "Are you sure to Approve this items ?");
        tbltooltip.Visible = true;
        tblmaster.BgColor = "FFFFFFF";
        if (!Page.IsPostBack)
        {           
            get_pending();            
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
                    generate_detail_data(ddllist.SelectedValue.ToString());
                    Session[clsStatic.sessionCurrentRefFocus] = null;
                    ddllist_SelectedIndexChanged(sender, e);
                }
            }
        }
        else
        {
         
        }
                
        generate_detail_data(ddllist.SelectedValue.ToString());
        
        tbltooltip.Visible = false;
        seturgentcolor();
      
    }

    private void seturgentcolor()
    {
        foreach (ListItem lst in ddllist.Items)
        {
            if (lst.Text.IndexOf("URGENT") != -1) lst.Attributes.Add("style", "color:red");
        }
    }

    private string get_my_app()
    {
        User_Role_DefinitionTableAdapter urole = new User_Role_DefinitionTableAdapter();
        SCBLDataSet.User_Role_DefinitionDataTable udt = new SCBLDataSet.User_Role_DefinitionDataTable();
        
        string my_app = "";
        udt = urole.GetRoleByUser(current.UserId.ToString(),"CS");
        if (udt.Rows.Count > 0) my_app = udt[0].role_as.ToString();



        return my_app;
    }

    private bool getbatchper(string app_code)
    {
        tbl_app_ruleTableAdapter app = new tbl_app_ruleTableAdapter();
        SCBLDataSet.tbl_app_ruleDataTable dt = new SCBLDataSet.tbl_app_ruleDataTable();
        bool aper;
        dt = app.GetDataByAppId(app_code);

        if (dt.Rows.Count == 0)
        {
            aper = false;
        }
        else
        {
            if (dt[0].app_per == 1) aper = true; else aper = false;
        }

        return aper;
    }

    private bool getaper(string ttype, string app_code)
    {
        tbl_app_ruleTableAdapter app = new tbl_app_ruleTableAdapter();
        SCBLDataSet.tbl_app_ruleDataTable dt = new SCBLDataSet.tbl_app_ruleDataTable();
        bool aper;
        dt = app.GetDataByTypeApp(ttype, app_code);

        if (dt.Rows.Count == 0)
        {
            aper = false;
        }
        else
        {
            if (dt[0].app_per == 1) aper = true; else aper = false;
        }

        return aper;
    }

    private bool getedtper(string ttype, string app_code)
    {
        tbl_app_ruleTableAdapter app = new tbl_app_ruleTableAdapter();
        SCBLDataSet.tbl_app_ruleDataTable dt = new SCBLDataSet.tbl_app_ruleDataTable();
        bool edtper;
        dt = app.GetDataByTypeApp(ttype, app_code);

        if (dt.Rows.Count == 0)
        {
            edtper = false;
        }
        else
        {
            if (dt[0].edit_per == 1) edtper = true; else edtper = false;
        }

        return edtper;
    }

    private bool getrper(string ttype, string app_code)
    {
        tbl_app_ruleTableAdapter app = new tbl_app_ruleTableAdapter();
        SCBLDataSet.tbl_app_ruleDataTable dt = new SCBLDataSet.tbl_app_ruleDataTable();
        bool rper;
        dt = app.GetDataByTypeApp(ttype, app_code);

        if (dt.Rows.Count == 0)
        {
            rper = false;
        }
        else
        {
            if (dt[0].rej_per == 1) rper = true; else rper = false;
        }

        return rper;
    }

    private void getDropdownList(string ttype, string app_code)
    {
        tbl_app_ruleTableAdapter app = new tbl_app_ruleTableAdapter();
        SCBLDataSet.tbl_app_ruleDataTable dt = new SCBLDataSet.tbl_app_ruleDataTable();
        App_Type_DetTableAdapter apptype = new App_Type_DetTableAdapter();

       
        dt = app.GetDataByTypeApp(ttype, app_code);
        string tmpstr;
        
        ListItem lst;
        ddlforto.Items.Clear();
        ddlforto.Items.Add("");

        if (dt.Rows.Count != 0)
        {

            tmpstr = dt[0].for_1.ToString();
            if (tmpstr != "")
            {
                lst = new ListItem();
                lst.Value = tmpstr;
                lst.Text = tmpstr + ": " + apptype.GetDataByAppName(tmpstr)[0].app_desc.ToString();
                ddlforto.Items.Add(lst);
            }

            tmpstr = dt[0].for_2.ToString();
            if (tmpstr != "")
            {
                lst = new ListItem();
                lst.Value = tmpstr;
                lst.Text = tmpstr + ": " + apptype.GetDataByAppName(tmpstr)[0].app_desc.ToString();
                ddlforto.Items.Add(lst);
            }

            tmpstr = dt[0].for_3.ToString();
            if (tmpstr != "")
            {
                lst = new ListItem();
                lst.Value = tmpstr;
                lst.Text = tmpstr + ": " + apptype.GetDataByAppName(tmpstr)[0].app_desc.ToString();
                ddlforto.Items.Add(lst);
            }

            tmpstr = dt[0].for_4.ToString();
            if (tmpstr != "")
            {
                lst = new ListItem();
                lst.Value = tmpstr;
                lst.Text = tmpstr + ": " + apptype.GetDataByAppName(tmpstr)[0].app_desc.ToString();
                ddlforto.Items.Add(lst);
            }

            tmpstr = dt[0].for_5.ToString();
            if (tmpstr != "")
            {
                lst = new ListItem();
                lst.Value = tmpstr;
                lst.Text = tmpstr + ": " + apptype.GetDataByAppName(tmpstr)[0].app_desc.ToString();
                ddlforto.Items.Add(lst);
            }
        }
        
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

            phcomments.Controls.Add(ctl);
        }
        
    }

    private void generate_detail_data(string icode_quoref)
    {
        tbl_tac_logTableAdapter log = new tbl_tac_logTableAdapter();
        SCBLDataSet.tbl_tac_logDataTable dtlog = new SCBLDataSet.tbl_tac_logDataTable();
        PuTr_IN_Det_ScblTableAdapter srdet = new PuTr_IN_Det_ScblTableAdapter();
        quotation_detTableAdapter quo = new quotation_detTableAdapter();        
        SCBLDataSet.quotation_detDataTable dt = new SCBLDataSet.quotation_detDataTable();
        SCBLDataSet.PuTr_IN_Det_ScblRow dr;
        PuTr_PO_Det_ScblTableAdapter podet = new PuTr_PO_Det_ScblTableAdapter();
        SCBLDataSet.PuTr_PO_Det_ScblDataTable dtrecent = new SCBLDataSet.PuTr_PO_Det_ScblDataTable();
        PuMa_Par_AdrTableAdapter adr = new PuMa_Par_AdrTableAdapter();
        ErpDataSet.PuMa_Par_AdrDataTable dtadr;

        LibraryDAL.ErpDataSetTableAdapters.InMa_Stk_CtlTableAdapter stk = new LibraryDAL.ErpDataSetTableAdapters.InMa_Stk_CtlTableAdapter();
        ErpDataSet.InMa_Stk_CtlDataTable dtstk = new ErpDataSet.InMa_Stk_CtlDataTable();


        ListItem lst;
        string my_app = get_my_app();
        decimal qty;
        Label lbltooltip;
        Literal ltrl;
        els.ToolTip toolt;       
        string ref_no, icode, qref, tac_ref, genstr, spestr, paystr,pay_type,sel_party_code;
        string[] tmp;
        int cur_index,i,gcnt,scnt,pcnt;
        int vdays = 0;
        HtmlInputRadioButton rdolist;
        Control ctl;


        if (icode_quoref == "") return;

        tmp=icode_quoref.Split(':');
        icode = tmp[0].ToString();
        qref = tmp[1].ToString();

        dr = srdet.GetQuoByItem("QUO", my_app, icode,qref)[0];
                
        btnapp.Visible = getaper(dr.In_Det_Quo_Flow, my_app);
        btnreject.Visible = getrper(dr.In_Det_Quo_Flow, my_app);
        btnaddquo.Visible = getedtper(dr.In_Det_Quo_Flow, my_app);

        //getDropdownList(dr.IN_Det_Quo_Flow, my_app);     
            
        generate_comments(dr.In_Det_Quo_Ref.ToString());

        lblmpr.Text = dr.IN_Det_Ref.ToString();  
        lblqref.Text = dr.In_Det_Quo_Ref.ToString();
        lblproduct.Text = dr.IN_Det_Icode.ToString() + " : " + dr.IN_Det_Itm_Desc.ToString();
        lblqty.Text = dr.IN_Det_Lin_Qty.ToString() + " " + dr.IN_Det_Itm_Uom.ToString();

         dtstk = stk.GetDataByItemStore(icode, dr.IN_Det_Str_Code);

         if (dtstk.Rows.Count == 0)
         {
             lblcurstk.Text = "";

         }
         else
         {
             lblcurstk.Text = dtstk[0].Stk_Ctl_Free_Stk.ToString("N2") + " " + dr.IN_Det_Itm_Uom.ToString();             
         }

        
        lblreq.Text = dr.IN_Det_Code.ToString() + ", " + dr.In_Det_Pur_Type.ToString();
        lblremarks.Text = dr.In_Det_Remarks;
        //set recent price

        dtrecent = podet.GetDataforPriceLog(dr.IN_Det_Icode.ToString());
        ddlrecentlist.Items.Clear();

        foreach (SCBLDataSet.PuTr_PO_Det_ScblRow drr in dtrecent.Rows)
        {
            lst = new ListItem();
            lst.Text = drr.PO_Det_Lin_Rat.ToString("N2") + " [" + drr.PO_Det_Ref.ToString() + "]";
            lst.Value = drr.PO_Det_Ref.ToString();
            ddlrecentlist.Items.Add(lst);
            if (ddlrecentlist.Items.Count > 20) break;
        }



        qty = (decimal)dr.IN_Det_Lin_Qty;
        sel_party_code = dr.In_Det_App_Party.ToString();
        
        //quotation
        ref_no = dr.In_Det_Quo_Ref.ToString();            

        dt = quo.GetQuotationByRef(ref_no);
        cur_index = 1;

        tbl_party.Rows[0].Cells[1].Visible = false;
        foreach (SCBLDataSet.quotation_detRow drquo in dt.Rows)
        {
            if (cur_index == 16) break;


            if (Session[clsStatic.sessionPartySelFlag].ToString() != ref_no)
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

            dtadr = new ErpDataSet.PuMa_Par_AdrDataTable();
            dtadr = adr.GetDataByAdrCode(drquo.party_code);

            tbl_party.Rows[cur_index].Cells[1].InnerText = drquo.party_code;
            if (dtadr.Rows.Count == 0)
            {
                tbl_party.Rows[cur_index].Cells[2].InnerText = drquo.party_det;
            }
            else
            {
                tbl_party.Rows[cur_index].Cells[2].InnerText = drquo.party_det + ", " + dtadr[0].Par_Adr_Line_1 + " " + dtadr[0].Par_Adr_Line_2 + " " + dtadr[0].Par_Adr_Line_3 + " " + dtadr[0].Par_Adr_Line_4 + " " + dtadr[0].Par_Adr_Line_5 + " Ph: " + dtadr[0].Par_Adr_Tel_No + " " + dtadr[0].Par_Adr_Lst_No + " Fax: " + dtadr[0].Par_Adr_Fax_No +" Email: " + dtadr[0].Par_Adr_Email_Id;
            }
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
        Session[clsStatic.sessionPartySelFlag] = ref_no;
        
        for (i = cur_index; i <= 15; i++)
        {
            rdolist = new HtmlInputRadioButton();
            ctl = new Control();
            ctl = tbl_party.Rows[cur_index].Cells[0].Controls[1];
            rdolist = (HtmlInputRadioButton)ctl;
            rdolist.Checked = false;

            tbl_party.Rows[i].Visible = false;
        }
        
        
    }


    private void get_pending()
    {
        PuTr_IN_Det_ScblTableAdapter indet = new PuTr_IN_Det_ScblTableAdapter();
        SCBLDataSet.PuTr_IN_Det_ScblDataTable dtdet = new SCBLDataSet.PuTr_IN_Det_ScblDataTable();
        ListItem lst;
      
       
        string my_app = get_my_app();
       

        //if (my_app == "") return;

        dtdet = indet.GetPendingForQuoApp("QUO",my_app);

        if (dtdet.Rows.Count == 0)
        {
            Response.Redirect("./frm_com_inbox.aspx");
        }

        
        //if (getbatchper(my_app)) Response.Redirect("./frm_quo_approval_batch.aspx");

        ddllist.Items.Clear();
        //ddllist.Items.Add("");


        foreach (SCBLDataSet.PuTr_IN_Det_ScblRow dr in dtdet.Rows)
        {
            lst = new ListItem();
            lst.Text = dr.IN_Det_Itm_Desc.ToString() + "    [" + dr.IN_Det_Lin_Qty.ToString() + " " + dr.IN_Det_Itm_Uom.ToString() + "]";
            lst.Value = dr.IN_Det_Icode.ToString() + ":" + dr.In_Det_Quo_Ref.ToString();

            if (dr.T_Fl == "U")
            {
                lst.Text = lst.Text + " [URGENT]";               
            }
            
            ddllist.Items.Add(lst);           
        }

        tblquotation.Visible = false;
        lblcount.Text = "(" + (ddllist.Items.Count).ToString() + ")";
        call_ddl_change();
              
   }

    private void call_ddl_change()
    {
        string selval = ddllist.SelectedValue.ToString();

        if (selval == "")
        {
            tblquotation.Visible = false; return;
        }
        else
            tblquotation.Visible = true;

        //initialize_page();


        string my_app = get_my_app();
        PuTr_IN_Det_ScblTableAdapter indet = new PuTr_IN_Det_ScblTableAdapter();
        SCBLDataSet.PuTr_IN_Det_ScblRow dr;
        string icode, qref;
        string[] tmp;

        tmp = selval.Split(':');
        icode = tmp[0].ToString();
        qref = tmp[1].ToString();

        dr = indet.GetQuoByItem("QUO", my_app, icode, qref)[0];

        getDropdownList(dr.In_Det_Quo_Flow, my_app);    
    }

    protected void ddllist_SelectedIndexChanged(object sender, EventArgs e)
    {
        call_ddl_change();
        
    }

    private bool check_approval_validity( string icode, string quoref)
    {
        PuTr_IN_Det_ScblTableAdapter det = new PuTr_IN_Det_ScblTableAdapter();
        bool flg = false;
        string my_app = get_my_app();

        if (det.GetQuoByItem("QUO", my_app, icode, quoref).Count > 0) flg = true;

        return flg;
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

    private bool savedata(string act_type, string icode,string qref, bool rej)
    {
        bool flg=true;
        PuTr_IN_Det_ScblTableAdapter det = new PuTr_IN_Det_ScblTableAdapter();
        tbl_CommentsTableAdapter comm = new tbl_CommentsTableAdapter();
        LibraryDAL.dsLinkofficeTableAdapters.tblUserInfoTableAdapter usr = new LibraryDAL.dsLinkofficeTableAdapters.tblUserInfoTableAdapter();
        
        string my_app = get_my_app();
        string party_code,status,status1,party_det,comments_det;
        decimal lrate;
        int sel_index;
        string uid = current.UserId.ToString();
        string uname = current.UserName.ToString();
        string desig = usr.GetUserByCode(uid)[0].UserDesignation.ToString();


        sel_index = GetSelectedIndex();
        if (rej)
        {
            if (sel_index == -1)
            {
                sel_index = 1;
            }
        }
        else
        {
            if (sel_index == -1)
            {
                flg = false;
                goto skip_all;
            }
        }
        
        party_code = tbl_party.Rows[sel_index].Cells[1].InnerText;
        lrate = Convert.ToDecimal(tbl_party.Rows[sel_index].Cells[3].InnerText);
        party_det = tbl_party.Rows[sel_index].Cells[2].InnerText;
        
        comments_det = "(Selected Party: " + party_det + " ) "+txtcomments.Text;


        if (act_type=="APP")
        {
            status="APP";
            status1="";       

           
        }
        else
        {
            //party_code="";
            //lrate = 0;

            
            if(act_type=="REJ")
            {
                status = "TEN";
                status1="";
            }
            else
            {
                status="QUO";
                status1=ddlforto.SelectedValue.ToString();
            }
           
        }
        SqlTransaction myTrn = HelperTA.OpenTransaction(det.Connection);

        try
        {            
            det.AttachTransaction(myTrn);
            comm.AttachTransaction(myTrn);

            det.UpdateFromQuoApp(lrate, status, status1, party_code, icode, qref);
            comm.InsertComments(qref, 1, DateTime.Now, uid, uname, desig, 1, my_app, status, comments_det, "", status1);

            myTrn.Commit();       
            //myTrn.Rollback();
        }
        catch(Exception ex)
        {
            myTrn.Rollback();
            flg = false;
        }

        finally
        {
            HelperTA.CloseTransaction(det.Connection, myTrn);
        }

        skip_all:

        return flg;
    }
    protected void btnapp_Click(object sender, EventArgs e)
    {        
        string icode, qref;
        string[] tmp;
        tmp = lblproduct.Text.Split(':');
        icode = tmp[0].Trim();
        qref = lblqref.Text;

        if (check_approval_validity(icode, qref) == false) { return; }

        if (savedata("APP", icode, qref, false) == true)
        {
            get_pending();
            //ddllist.Text = "";
            txtcomments.Text = "";
            ddlforto.Text = "";
            //generate_detail_data(""); 
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        else { lblComm.Text = "ERROR "; lblComm.Visible = true; }

        seturgentcolor();
    }

    

    protected void btnforward_Click(object sender, EventArgs e)
    {
        bool flg = false;
        if (ddlforto.Text == "") { lblComm.Text = "Pls select forward to "; lblComm.Visible = true; return; }
        string forto = ddlforto.SelectedValue.ToString();
        string icode, qref;
        string[] tmp;
        tmp = lblproduct.Text.Split(':');
        icode = tmp[0].Trim();
        qref = lblqref.Text;

        if (check_approval_validity(icode, qref) == false) { return; }

        if (GetSelectedIndex() == -1) return;

        if (savedata("FOR", icode, qref, false) == true)
        {
            get_pending();
            //ddllist.Text = "";
            txtcomments.Text = "";
            ddlforto.Text = "";
            //generate_detail_data("");
            flg = true;
        }
        else { lblComm.Text = "ERROR "; lblComm.Visible = true; }

        if (flg)
        {

            string sid, sname, msub, mbody;
            clsEmailReceiver[] rec_det = new clsEmailReceiver[20];
            int remailcnt = 0;
            dsLinkoffice.tblUserInfoRow  udr;
            LibraryDAL.dsLinkofficeTableAdapters.tblUserInfoTableAdapter usrdal = new LibraryDAL.dsLinkofficeTableAdapters.tblUserInfoTableAdapter();
            User_Role_DefinitionTableAdapter roleta = new User_Role_DefinitionTableAdapter();
            SCBLDataSet.User_Role_DefinitionDataTable roledt = new SCBLDataSet.User_Role_DefinitionDataTable();

            udr = usrdal.GetUserByCode(current.UserId.ToString())[0];
            sid = udr.UserEmail.ToString();
            sname = udr.UserName.ToString();

            msub = "A Comperative Statement (CS) forwarded to you [" + lblmpr.Text + "]";
            mbody = "\n\n " + "A Comperative Statement (CS) forwarded to you [" + lblmpr.Text + "]";
            mbody += "\n " + "To view details please login in at http://203.76.114.131/RT/rtd/frmLogin.aspx ";
            mbody += "\n " + "**THIS IS AUTO GENERATED EMAIL AND DONT REQUIRE A REPLY.**";

           
            roledt = roleta.GetDataByRole(forto);

            foreach (SCBLDataSet.User_Role_DefinitionRow rdr in roledt.Rows)
            {
                rec_det[remailcnt] = new clsEmailReceiver();
                rec_det[remailcnt].Rname = rdr.user_name;
                rec_det[remailcnt].Rid = usrdal.GetUserByCode(rdr.user_id)[0].UserEmail;
                remailcnt++;
            }
            clsStatic.SendMail(sid, sname, rec_det, msub, mbody);
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        seturgentcolor();

    }
    protected void btnreject_Click(object sender, EventArgs e)
    {
        if (txtcomments.Text == "") { lblComm.Text = "Pls type your comments "; lblComm.Visible = true; return; }
        string icode, qref;
        string[] tmp;
        tmp = lblproduct.Text.Split(':');
        icode = tmp[0].Trim();
        qref = lblqref.Text;

        if (check_approval_validity(icode, qref) == false) { return; }
        
        if (savedata("REJ", icode, qref, true) == true)
        {
            get_pending();
            //ddllist.Text = "";
            txtcomments.Text = "";
            ddlforto.Text = "";
            //generate_detail_data("");
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        else { lblComm.Text = "ERROR "; lblComm.Visible = true; }

        seturgentcolor();
    }


    private void show_tooltip(string icode_qref, string pcode)
    {
        string my_app = get_my_app();

        tbl_tac_logTableAdapter log = new tbl_tac_logTableAdapter();
        SCBLDataSet.tbl_tac_logDataTable dtlog = new SCBLDataSet.tbl_tac_logDataTable();
        PuTr_IN_Det_ScblTableAdapter srdet = new PuTr_IN_Det_ScblTableAdapter();
        quotation_detTableAdapter quo = new quotation_detTableAdapter();
        SCBLDataSet.quotation_detRow drquo;
        SCBLDataSet.PuTr_IN_Det_ScblRow dr;
        string[] tmp;
        string icode, qref, pay_type, genstr, spestr, paystr, tac_ref;
        int gcnt, scnt, pcnt;
        int vdays = 0;

        tmp = icode_qref.Split(':');
        icode = tmp[0].ToString();
        qref = tmp[1].ToString();

        dr = srdet.GetQuoByItem("QUO", my_app, icode, qref)[0];
        drquo = quo.GetDataByRefParty(dr.In_Det_Quo_Ref, pcode)[0];


        genstr = "";
        spestr = "";
        paystr = "";
        gcnt = 0;
        scnt = 0;
        pcnt = 0;

        tac_ref = drquo.gen_terms;
        dtlog = log.GetDataByRef(tac_ref);

        foreach (SCBLDataSet.tbl_tac_logRow drlog in dtlog.Rows)
        {
            vdays = drlog.valid_days;
            switch (drlog.tac_type)
            {
                case "gen":
                    {
                        gcnt++;
                        genstr = genstr + gcnt.ToString() + ". " + drlog.content_det.ToString() + "<br />";
                        break;
                    }

                case "spe":
                    {
                        scnt++;
                        spestr = spestr + scnt.ToString() + ". " + drlog.content_det.ToString() + "<br />";
                        break;
                    }

                case "pay":
                    {
                        pcnt++;
                        paystr = paystr + pcnt.ToString() + ". " + drlog.content_det.ToString() + "<br />";
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

               

        tbltooltippnl.Rows[0].Cells[0].InnerText = drquo.party_det.ToUpper();
        tbltooltippnl.Rows[2].Cells[1].InnerHtml = genstr;
        tbltooltippnl.Rows[3].Cells[1].InnerHtml = spestr;
        tbltooltippnl.Rows[4].Cells[0].InnerHtml = "Pay Terms(" + pay_type + ")";
        tbltooltippnl.Rows[4].Cells[1].InnerHtml = paystr;
        tbltooltippnl.Rows[5].Cells[1].InnerHtml = vdays.ToString();

        btnedit.PostBackUrl = "./frm_tac_edit.aspx?tac_log_id=" + tac_ref + "&focus_ref_no=" + ddllist.SelectedValue.ToString() + "&party_name=" + drquo.party_det.ToUpper();
        btneditval.PostBackUrl = "./frm_val_edit.aspx?quot_ref=" + dr.In_Det_Quo_Ref.ToString() + "&pcode=" + pcode + "&focusref=" + ddllist.SelectedValue.ToString();

        ModalPopupExtender5.Show();
    }

    protected void lnktc1_Click(object sender, EventArgs e)
    {

        string icode = ddllist.SelectedValue.ToString();
        string pcode = tbl_party.Rows[1].Cells[1].InnerText;
        show_tooltip(icode, pcode);
        
    }
    protected void lnktc2_Click(object sender, EventArgs e)
    {
        string icode = ddllist.SelectedValue.ToString();
        string pcode = tbl_party.Rows[2].Cells[1].InnerText;
        show_tooltip(icode, pcode);
    }
    protected void lnktc3_Click(object sender, EventArgs e)
    {
        string icode = ddllist.SelectedValue.ToString();
        string pcode = tbl_party.Rows[3].Cells[1].InnerText;
        show_tooltip(icode, pcode);
    }
    protected void lnktc4_Click(object sender, EventArgs e)
    {
        string icode = ddllist.SelectedValue.ToString();
        string pcode = tbl_party.Rows[4].Cells[1].InnerText;
        show_tooltip(icode, pcode);
    }
    protected void lnktc5_Click(object sender, EventArgs e)
    {
        string icode = ddllist.SelectedValue.ToString();
        string pcode = tbl_party.Rows[5].Cells[1].InnerText;
        show_tooltip(icode, pcode);
    }
    protected void lnktc6_Click(object sender, EventArgs e)
    {
        string icode = ddllist.SelectedValue.ToString();
        string pcode = tbl_party.Rows[6].Cells[1].InnerText;
        show_tooltip(icode, pcode);
    }
    protected void lnktc7_Click(object sender, EventArgs e)
    {
        string icode = ddllist.SelectedValue.ToString();
        string pcode = tbl_party.Rows[7].Cells[1].InnerText;
        show_tooltip(icode, pcode);
    }
    protected void lnktc8_Click(object sender, EventArgs e)
    {
        string icode = ddllist.SelectedValue.ToString();
        string pcode = tbl_party.Rows[8].Cells[1].InnerText;
        show_tooltip(icode, pcode);
    }
    protected void lnktc9_Click(object sender, EventArgs e)
    {
        string icode = ddllist.SelectedValue.ToString();
        string pcode = tbl_party.Rows[9].Cells[1].InnerText;
        show_tooltip(icode, pcode);
    }
    protected void lnktc10_Click(object sender, EventArgs e)
    {
        string icode = ddllist.SelectedValue.ToString();
        string pcode = tbl_party.Rows[10].Cells[1].InnerText;
        show_tooltip(icode, pcode);
    }
    protected void lnktc11_Click(object sender, EventArgs e)
    {
        string icode = ddllist.SelectedValue.ToString();
        string pcode = tbl_party.Rows[11].Cells[1].InnerText;
        show_tooltip(icode, pcode);
    }
    protected void lnktc12_Click(object sender, EventArgs e)
    {
        string icode = ddllist.SelectedValue.ToString();
        string pcode = tbl_party.Rows[12].Cells[1].InnerText;
        show_tooltip(icode, pcode);
    }
    protected void lnktc13_Click(object sender, EventArgs e)
    {
        string icode = ddllist.SelectedValue.ToString();
        string pcode = tbl_party.Rows[13].Cells[1].InnerText;
        show_tooltip(icode, pcode);
    }
    protected void lnktc14_Click(object sender, EventArgs e)
    {
        string icode = ddllist.SelectedValue.ToString();
        string pcode = tbl_party.Rows[14].Cells[1].InnerText;
        show_tooltip(icode, pcode);
    }
    protected void lnktc15_Click(object sender, EventArgs e)
    {
        string icode = ddllist.SelectedValue.ToString();
        string pcode = tbl_party.Rows[15].Cells[1].InnerText;
        show_tooltip(icode, pcode);
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
