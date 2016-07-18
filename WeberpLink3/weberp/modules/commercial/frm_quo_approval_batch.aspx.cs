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
using AjaxControlToolkit;

public partial class frm_quo_approval_batch : System.Web.UI.Page
{

    string totamount = "";  

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication();
        clsStatic.MsgConfirmBox(btnforward, "Are you sure to Forward those items ?");
        clsStatic.MsgConfirmBox(btnreject, "Are you sure to Reject those items ?");
        clsStatic.MsgConfirmBox(btnapp, "Are you sure to Approve those items ?");
        tbltooltip.Visible = true;
        tblmaster.BgColor = "FFFFFFF";
        if (!Page.IsPostBack)
        {           
            get_pending();     
                           
        }
                   
        tbltooltip.Visible = false;       

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
    private PlaceHolder generate_comments(PlaceHolder cph, string ref_no)
    {
        
        tbl_CommentsTableAdapter com = new tbl_CommentsTableAdapter();
        SCBLDataSet.tbl_CommentsDataTable dt = new SCBLDataSet.tbl_CommentsDataTable();
       
        dt = com.GetCommentsByRef(ref_no);
        cph.Controls.Clear();
        foreach (SCBLDataSet.tbl_CommentsRow dr in dt.Rows)
        {
            ClientSide_modules_commercial_usercontrols_ctl_comments ctl2 = (ClientSide_modules_commercial_usercontrols_ctl_comments)LoadControl("./usercontrols/ctl_comments.ascx");
            Label lblname = (Label)ctl2.FindControl("lblname");
            Label lbldate = (Label)ctl2.FindControl("lbldate");
            HtmlTableCell celcomm = (HtmlTableCell)ctl2.FindControl("celcomm");
            Image imgimage = (Image)ctl2.FindControl("imgimage");

            imgimage.ImageUrl = "~/handler/hndImage.ashx?id=" + dr.app_id;

            //ctl2.ID = cph.ClientID + "ctl_" + phcomments.Controls.Count.ToString();

            lblname.Text = dr.app_name.ToString() + " (" + dr.app_designation.ToString() + ")";
            lbldate.Text = dr.app_date.ToString();
            celcomm.InnerText = dr.gen_comments.ToString();

            cph.Controls.Add(ctl2);
           
        }
        return cph;
    }

    private HtmlTable set_quot(SCBLDataSet.PuTr_IN_Det_ScblRow dr, HtmlTable tbl_party)
    {
        tbl_tac_logTableAdapter log = new tbl_tac_logTableAdapter();
        SCBLDataSet.tbl_tac_logDataTable dtlog = new SCBLDataSet.tbl_tac_logDataTable();        
        quotation_detTableAdapter quo = new quotation_detTableAdapter();
        SCBLDataSet.quotation_detDataTable dt = new SCBLDataSet.quotation_detDataTable();
       

        decimal qty;
        Label lbltooltip;
        Literal ltrl;
        els.ToolTip toolt;
        string ref_no, tac_ref, genstr, spestr, paystr, pay_type, sel_party_code;
        
        int cur_index, i, gcnt, scnt, pcnt;
        int vdays = 0;
        HtmlInputRadioButton rdolist;
        Control ctl;
              

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

            
            if (drquo.party_code.ToString() == sel_party_code)
            {
                rdolist = new HtmlInputRadioButton();
                ctl = new Control();
                ctl = tbl_party.Rows[cur_index].Cells[0].Controls[1];
                rdolist = (HtmlInputRadioButton)ctl;
                rdolist.Checked = true;
                totamount = (drquo.rate * qty).ToString("N2");
            }
            else
            {
                rdolist = new HtmlInputRadioButton();
                ctl = new Control();
                ctl = tbl_party.Rows[cur_index].Cells[0].Controls[1];
                rdolist = (HtmlInputRadioButton)ctl;
                rdolist.Checked = false;
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
                            paystr = paystr + pcnt.ToString() + ". " + drlog.content_det.ToString() + "</br>";
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
            lbltooltip.Text = cur_index.ToString() + ".";
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
       
        for (i = cur_index; i <= 15; i++)
        {
            rdolist = new HtmlInputRadioButton();
            ctl = new Control();
            ctl = tbl_party.Rows[cur_index].Cells[0].Controls[1];
            rdolist = (HtmlInputRadioButton)ctl;
            rdolist.Checked = false;

            tbl_party.Rows[i].Visible = false;
        }
       

        return tbl_party;
    }

   

    private void get_pending()
    {
        PuTr_IN_Det_ScblTableAdapter indet = new PuTr_IN_Det_ScblTableAdapter();
        SCBLDataSet.PuTr_IN_Det_ScblDataTable dtdet = new SCBLDataSet.PuTr_IN_Det_ScblDataTable();
        PuTr_PO_Det_ScblTableAdapter podet = new PuTr_PO_Det_ScblTableAdapter();
        SCBLDataSet.PuTr_PO_Det_ScblDataTable dtrecent = new SCBLDataSet.PuTr_PO_Det_ScblDataTable();

        ListItem lst;

        int cnt = 0;     
       
        string my_app = get_my_app();      

        //if (my_app == "") return;

        dtdet = indet.GetPendingForQuoApp("QUO",my_app);
        if (dtdet.Rows.Count == 0)
        {
            Response.Redirect("./frm_com_inbox.aspx");
        }
        //dtdet.DefaultView.Sort = dtdet.IN_Det_Lin_AmtColumn.ColumnName + " DESC";

        gdItem.DataSource = dtdet;
        gdItem.DataBind();

       

        foreach (SCBLDataSet.PuTr_IN_Det_ScblRow dr in dtdet.Rows)
        {
            Label lblmprref = (Label)gdItem.Rows[cnt].Cells[1].FindControl("Label1");
            Label lblquoref = (Label)gdItem.Rows[cnt].Cells[2].FindControl("Label2");
            Label lblicode = (Label)gdItem.Rows[cnt].Cells[3].FindControl("Label3");
            Label lblamnt = (Label)gdItem.Rows[cnt].Cells[4].FindControl("Label4");

            ClientSide_modules_commercial_usercontrols_ctl_cs_batch_approval ctl = (ClientSide_modules_commercial_usercontrols_ctl_cs_batch_approval)gdItem.Rows[cnt].Cells[5].FindControl("ctl1");

            Label lblheader = (Label)ctl.FindControl("lblheader");
            Label lblmpr = (Label)ctl.FindControl("lblmpr");
            Label lblqref = (Label)ctl.FindControl("lblqref");
            Label lblproduct = (Label)ctl.FindControl("lblproduct");
            Label lblqty = (Label)ctl.FindControl("lblqty");
            Label lblreq = (Label)ctl.FindControl("lblreq");
            DropDownList ddlrecentlist = (DropDownList)ctl.FindControl("ddlrecentlist");
            PlaceHolder phcomments = (PlaceHolder)ctl.FindControl("phcomments");
            HtmlTable tbl_party = (HtmlTable)ctl.FindControl("tbl_party");

            lblmprref.Text = dr.IN_Det_Ref.ToString();
            lblquoref.Text = dr.In_Det_Quo_Ref.ToString();
            lblicode.Text = dr.IN_Det_Icode.ToString();
            lblheader.Text = dr.IN_Det_Itm_Desc.ToString();
            lblheader.Width = 830;

            phcomments = generate_comments(phcomments, dr.In_Det_Quo_Ref.ToString());

            lblmpr.Text = dr.IN_Det_Ref.ToString();
            lblqref.Text = dr.In_Det_Quo_Ref.ToString();
            lblproduct.Text = dr.IN_Det_Icode.ToString() + " : " + dr.IN_Det_Itm_Desc.ToString();
            lblqty.Text = dr.IN_Det_Lin_Qty.ToString() + " " + dr.IN_Det_Itm_Uom.ToString();
            lblreq.Text = dr.IN_Det_Code.ToString() + ", " + dr.In_Det_Pur_Type.ToString();

            dtrecent = podet.GetDataforPriceLog(dr.IN_Det_Icode.ToString());

            ddlrecentlist.Items.Clear();
            foreach (SCBLDataSet.PuTr_PO_Det_ScblRow drr in dtrecent.Rows)
            {
                lst = new ListItem();
                lst.Text = drr.PO_Det_Lin_Rat.ToString("N2") + " [" + drr.PO_Det_Ref.ToString() + "]";
                lst.Value = drr.PO_Det_Lin_Rat.ToString("N2") + " [" + drr.PO_Det_Ref.ToString() + "]";
                ddlrecentlist.Items.Add(lst);
                if (ddlrecentlist.Items.Count > 20) break;
            }

            tbl_party = set_quot(dr, tbl_party);
            lblamnt.Text = totamount;

            cnt++;            
                  
        }


        //DataTable dttemp = (DataTable)gdItem.DataSource;
        //dttemp.DefaultView.Sort = e.CommandArgument + "  ASC";
        
        btnapp.Visible = getaper(dtdet[0].In_Det_Quo_Flow, my_app);
        btnreject.Visible = getrper(dtdet[0].In_Det_Quo_Flow, my_app);
        getDropdownList(dtdet[0].In_Det_Quo_Flow, my_app);        
        lblcount.Text = "(" + (dtdet.Rows.Count).ToString() + ")";
                     
   }    

    


    private bool check_approval_validity(string my_app, string icode, string quoref)
    {
        PuTr_IN_Det_ScblTableAdapter det = new PuTr_IN_Det_ScblTableAdapter();
        bool flg = false;
        if (det.GetQuoByItem("QUO", my_app, icode, quoref).Count > 0) flg = true;

        return flg;
    }



  

    protected void btnforward_Click(object sender, EventArgs e)
    {

        if (ddlforto.Text == "") { lblComm.Text = "Pls select forward to "; lblComm.Visible = true; return; }
        string forto = ddlforto.SelectedValue.ToString();

        
        LibraryDAL.dsLinkofficeTableAdapters.tblUserInfoTableAdapter usr = new LibraryDAL.dsLinkofficeTableAdapters.tblUserInfoTableAdapter();
        string my_app = get_my_app();

        string uid = current.UserId.ToString();
        string uname = current.UserName.ToString();
        string desig = usr.GetUserByCode(uid)[0].UserDesignation.ToString();


        int cnt = 0;

        foreach (GridViewRow gr in gdItem.Rows)
        {
            CheckBox chk = (CheckBox)gr.Cells[0].FindControl("CheckBox1");
            if (chk.Checked)
            {
                Label lblmprref = (Label)gr.Cells[1].FindControl("Label1");
                Label lblquoref = (Label)gr.Cells[2].FindControl("Label2");
                Label lblitmcode = (Label)gr.Cells[3].FindControl("Label3");
                if (check_approval_validity(my_app, lblitmcode.Text, lblquoref.Text) == true)
                {
                    cnt++;
                    PuTr_IN_Det_ScblTableAdapter det = new PuTr_IN_Det_ScblTableAdapter();
                    tbl_CommentsTableAdapter comm = new tbl_CommentsTableAdapter();
                    SqlTransaction myTrn = HelperTA.OpenTransaction(det.Connection);

                    try
                    {
                        det.AttachTransaction(myTrn);
                        comm.AttachTransaction(myTrn);

                        det.UpdateForQuoBatchApprove("QUO", forto, lblitmcode.Text, lblquoref.Text);
                        comm.InsertComments(lblquoref.Text, 1, DateTime.Now, uid, uname, desig, 1, my_app, "FOR", txtcomments.Text, "", "");

                        myTrn.Commit();
                    }
                    catch
                    {
                        myTrn.Rollback();
                    }

                    finally
                    {
                        HelperTA.CloseTransaction(det.Connection, myTrn);
                    }
                }
            }
        }

        Response.Redirect(Request.Url.AbsoluteUri);

    }
    protected void btnreject_Click(object sender, EventArgs e)
    {
        
        LibraryDAL.dsLinkofficeTableAdapters.tblUserInfoTableAdapter usr = new LibraryDAL.dsLinkofficeTableAdapters.tblUserInfoTableAdapter();
        string my_app = get_my_app();

        string uid = current.UserId.ToString();
        string uname = current.UserName.ToString();
        string desig = usr.GetUserByCode(uid)[0].UserDesignation.ToString();


        int cnt = 0;
      
        foreach (GridViewRow gr in gdItem.Rows)
        {
            CheckBox chk = (CheckBox)gr.Cells[0].FindControl("CheckBox1");
            if (chk.Checked)
            {
                Label lblmprref = (Label)gr.Cells[1].FindControl("Label1");
                Label lblquoref = (Label)gr.Cells[2].FindControl("Label2");
                Label lblitmcode = (Label)gr.Cells[3].FindControl("Label3");
                if (check_approval_validity(my_app,lblitmcode.Text, lblquoref.Text) == true)
                {
                    cnt++;
                    PuTr_IN_Det_ScblTableAdapter det = new PuTr_IN_Det_ScblTableAdapter();
                    tbl_CommentsTableAdapter comm = new tbl_CommentsTableAdapter();
                    SqlTransaction myTrn = HelperTA.OpenTransaction(det.Connection);

                    try
                    {
                        det.AttachTransaction(myTrn);
                        comm.AttachTransaction(myTrn);

                        det.UpdateForQuoBatchApprove("TEN", "", lblitmcode.Text, lblquoref.Text);
                        comm.InsertComments(lblquoref.Text, 1, DateTime.Now, uid, uname, desig, 1, my_app, "REJ", txtcomments.Text, "", "");

                        myTrn.Commit();
                    }
                    catch
                    {
                        myTrn.Rollback();
                    }

                    finally
                    {
                        HelperTA.CloseTransaction(det.Connection, myTrn);
                    }
                }
            }
        }
               
        Response.Redirect(Request.Url.AbsoluteUri);
        
    }

    protected void btnapp_Click(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(4000);

       
        LibraryDAL.dsLinkofficeTableAdapters.tblUserInfoTableAdapter usr = new LibraryDAL.dsLinkofficeTableAdapters.tblUserInfoTableAdapter();
        string my_app = get_my_app();

        string uid = current.UserId.ToString();
        string uname = current.UserName.ToString();
        string desig = usr.GetUserByCode(uid)[0].UserDesignation.ToString();


        int cnt = 0;
        string ref_list = "";
        foreach (GridViewRow gr in gdItem.Rows)
        {
            CheckBox chk = (CheckBox)gr.Cells[0].FindControl("CheckBox1");
            if (chk.Checked)
            {                
                Label lblmprref = (Label)gr.Cells[1].FindControl("Label1");
                Label lblquoref = (Label)gr.Cells[2].FindControl("Label2");
                Label lblitmcode = (Label)gr.Cells[3].FindControl("Label3");
                if (check_approval_validity(my_app,lblitmcode.Text, lblquoref.Text) == true)
                {
                    cnt++;
                    ref_list = ref_list + lblmprref.Text + "\n ";
                    PuTr_IN_Det_ScblTableAdapter det = new PuTr_IN_Det_ScblTableAdapter();
                    tbl_CommentsTableAdapter comm = new tbl_CommentsTableAdapter();
                    SqlTransaction myTrn = HelperTA.OpenTransaction(det.Connection);

                    try
                    {
                        det.AttachTransaction(myTrn);
                        comm.AttachTransaction(myTrn);

                        det.UpdateForQuoBatchApprove("APP", "", lblitmcode.Text, lblquoref.Text);
                        comm.InsertComments(lblquoref.Text, 1, DateTime.Now, uid, uname, desig, 1, my_app, "APP", txtcomments.Text, "", "");

                        myTrn.Commit();
                    }
                    catch
                    {
                        myTrn.Rollback();                       
                    }

                    finally
                    {
                        HelperTA.CloseTransaction(det.Connection, myTrn);
                    }
                    

                }


            }            
        }

        if (cnt != 0)
        {
            send_mail(ref_list);
        }

        Response.Redirect(Request.Url.AbsoluteUri);
    }

    private void send_mail(string reflist)
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

        msub = "Comperative Statement (CS) Approved";
        mbody = "\n\n " + "Comperative Statement (CS) Approved";
        mbody += "\n\n" + reflist;
        mbody += "\n " + "To view details please login in at http://203.76.114.131/cml/cmlcom ";
        mbody += "\n " + "**THIS IS AUTO GENERATED EMAIL AND DONT REQUIRE A REPLY.**";


        roledt = roleta.GetDataByRoleType("CS");

        foreach (SCBLDataSet.User_Role_DefinitionRow rdr in roledt.Rows)
        {
            rec_det[remailcnt] = new clsEmailReceiver();
            rec_det[remailcnt].Rname = rdr.user_name;
            rec_det[remailcnt].Rid = usrdal.GetUserByCode(rdr.user_id)[0].UserEmail;
            remailcnt++;
        }
        clsStatic.SendMail(sid, sname, rec_det, msub, mbody);

    }

   
   

   
    protected void gdItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            ((CheckBox)e.Row.FindControl("CheckBox1")).Attributes.Add("onclick", "javascript:SelectAll('" + ((CheckBox)e.Row.FindControl("CheckBox1")).ClientID + "')");
        }      

    }
    }
