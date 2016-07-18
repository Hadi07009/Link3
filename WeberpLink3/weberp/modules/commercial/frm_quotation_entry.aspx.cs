using System;
using System.IO;
using System.Data;
using System.Text;
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
using LibraryDAL.SCBL2DataSetTableAdapters;

public partial class frm_quotation_entry : System.Web.UI.Page
{
    quotation_detTableAdapter quo = new quotation_detTableAdapter();
    SCBLDataSet.quotation_detDataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {                         
        
        tblmaster.BgColor = "FFFFFFF";
        tbltooltip.Visible = false;         

        if (!Page.IsPostBack)
        {
            tbltooltip2.Visible = false;  
            load_quot();
            get_pending();
           
        }
        else
        {
        }
        
    }

    private void load_quot()
    {
        quotation_logTableAdapter log = new quotation_logTableAdapter();
        SCBLDataSet.quotation_logDataTable dt = new SCBLDataSet.quotation_logDataTable();
        ListItem lst;
        dt = log.GetRecentData(Convert.ToDateTime(DateTime.Now.ToShortDateString()).AddDays(-5));

        ddlquotlog.Items.Clear();
        ddlquotlog.Items.Add("");

        foreach (SCBLDataSet.quotation_logRow dr in dt.Rows)
        {
            lst = new ListItem();
            lst.Value = dr.log_id;
            lst.Text = dr.party_code + " : " + dr.party_name;
            ddlquotlog.Items.Add(lst);
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

    private void get_pending()
    {



        SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable dtdet = new SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable();
        PuTr_IN_Det_Scbl2TableAdapter srdet = new PuTr_IN_Det_Scbl2TableAdapter();

        string plnts = "Plants: ";
        int i, len,cnt,indx;


        // For Quotetion entry permission as QEN but database as TEN

        string[] plant_list = get_plant("QEN");
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

        // For Quotetion entry permission as QEN but database as TEN
        //dtdet = srdet.GetDataByReqStatus("LPO", "TEN");
        dtdet = srdet.GetDataByReqStatus2("LPO", "FPO","TEN");


        cnt = dtdet.Rows.Count;

        for (indx = cnt; indx > 0; indx--)
        {

            for (i = 0; i < len; i++)
            {
                if (dtdet[indx - 1].IN_Det_Code.Substring(0, 2) == plant_list[i])
                    goto nextcheck1;

            }
            dtdet.RemovePuTr_IN_Det_Scbl2Row(dtdet[indx - 1]);


        nextcheck1: ;
        }




        if (dtdet.Rows.Count < 1)
        {
            btnQuotation.Visible = false;
            btnQuotation0.Visible = false;
            return;
        }
        
        generate_detail_data(dtdet);
        
    }

    private void generate_detail_data(SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable dtdet)
    {   
        DataTable dt = new DataTable();
       
        int qcnt;

        dt.Columns.Add("MPR", typeof(string));
        dt.Columns.Add("ICODE", typeof(string));
        dt.Columns.Add("IDET", typeof(string));
        dt.Columns.Add("QTY", typeof(double));
        dt.Columns.Add("UOM", typeof(string));
        dt.Columns.Add("SPECIFICATION", typeof(string));
        dt.Columns.Add("BRAND", typeof(string));
        dt.Columns.Add("ORIGIN", typeof(string));
        dt.Columns.Add("PACKING", typeof(string));
        dt.Columns.Add("ETR", typeof(string));
        dt.Columns.Add("REMARKS", typeof(string));
        dt.Columns.Add("NOE", typeof(int));


        foreach (SCBL2DataSet.PuTr_IN_Det_Scbl2Row dr in dtdet.Rows)
        {
            qcnt = quo.GetActiveQuote("", dr.IN_Det_Icode.ToString(), dr.IN_Det_Ref.ToString()).Rows.Count;

            dt.Rows.Add(dr.IN_Det_Ref, dr.IN_Det_Icode, dr.IN_Det_Itm_Desc, dr.IN_Det_Lin_Qty, dr.IN_Det_Itm_Uom, dr.In_Det_Specification, dr.In_Det_Brand, dr.In_Det_Origin, dr.In_Det_Packing, clsStatic.DateTimeToStringForSorting(dr.IN_Det_Exp_Dat), dr.In_Det_Remarks, qcnt);
           
        }
       
        gdItem.DataSource = dt;
        gdItem.DataBind();


        

        
        ViewState[clsStatic.ViewStateDataTable] = dt;

    }

    protected void gdItem_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState[clsStatic.ViewStateSortExpression] = e.SortExpression;
        AddSortImage(gdItem.HeaderRow);
    }

    protected void gdItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName.Equals("Sort"))
        {

            DataTable dttemp = new DataTable();
            dttemp = (DataTable)ViewState[clsStatic.ViewStateDataTable];


            if (ViewState[clsStatic.ViewStateSortDirection] != null)
                if ((SortDirection)ViewState[clsStatic.ViewStateSortDirection] == SortDirection.Descending)
                {

                    dttemp.DefaultView.Sort = e.CommandArgument + "  ASC";
                    ViewState[clsStatic.ViewStateSortDirection] = SortDirection.Ascending;
                }
                else
                {
                    dttemp.DefaultView.Sort = e.CommandArgument + "  DESC";
                    ViewState[clsStatic.ViewStateSortDirection] = SortDirection.Descending;
                }
            else
            {
                dttemp.DefaultView.Sort = e.CommandArgument + "  ASC";
                ViewState[clsStatic.ViewStateSortDirection] = SortDirection.Ascending;
            }



            gdItem.DataSource = dttemp;
            gdItem.DataBind();



        }



    }

    private void AddSortImage(GridViewRow headerRow)
    {

        if (ViewState[clsStatic.ViewStateSortExpression] == null) return;

        int columnIndex = 1;
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState[clsStatic.ViewStateDataTable];
        if (dt == null) return;
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            if (dt.Columns[i].Caption == ViewState[clsStatic.ViewStateSortExpression].ToString())
            {
                columnIndex = i;
            }
        }


        Image sortImage = new Image();

        if (ViewState[clsStatic.ViewStateSortDirection] == null) return;

        if ((SortDirection)ViewState[clsStatic.ViewStateSortDirection] == SortDirection.Ascending)
            sortImage.ImageUrl = "~/images/group_arrow_top.gif";
        else
            sortImage.ImageUrl = "~/images/group_arrow_bottom.gif";

        headerRow.Cells[columnIndex+2].Controls.Add(sortImage);

    }

    protected void gdItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            dt = new SCBLDataSet.quotation_detDataTable();
            dt = quo.GetActiveQuote("", e.Row.Cells[4].Text, e.Row.Cells[3].Text);
            int qcnt = dt.Rows.Count;
            int i = 0;
            int j;
            Button btn = new Button();
            btn = (Button)e.Row.Cells[2].Controls[1];
            ClientSide_modules_commercial_usercontrols_ctl_quotation_view ctl = (ClientSide_modules_commercial_usercontrols_ctl_quotation_view)e.Row.Cells[2].FindControl("ctl1");

            if (qcnt != 0)
            {

                Label lbltooltip;
                els.ToolTip toolt;
                Literal ltrl;
                HtmlTable htbl;
                string confirm;

                lbltooltip = new Label();
                lbltooltip.ForeColor = System.Drawing.Color.Black;
                lbltooltip.Text = qcnt.ToString();
                e.Row.Cells[0].Controls.Add(lbltooltip);
                ltrl = new Literal();
                toolt = new els.ToolTip();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                htbl = new HtmlTable();
                htbl = tbltooltip;
                htbl.Visible = true;

                foreach (SCBLDataSet.quotation_detRow dr in dt.Rows)
                {
                    i++;
                    htbl.Rows[i].Cells[0].InnerText = i.ToString();
                    htbl.Rows[i].Cells[1].InnerText = dr.party_det;
                    htbl.Rows[i].Cells[2].InnerText = dr.rate.ToString("N2");
                    htbl.Rows[i].Cells[3].InnerText = dr.specification;
                    htbl.Rows[i].Cells[4].InnerText = dr.product_brand;
                    htbl.Rows[i].Cells[5].InnerText = dr.origin;
                    htbl.Rows[i].Cells[6].InnerText = dr.packing;
                    htbl.Rows[i].Visible = true;
                }

                for (j = i + 1; j < 14; j++)
                {
                    htbl.Rows[j].Visible = false;
                }

                htbl.RenderControl(hw);
                toolt.Add(lbltooltip, ltrl, sb.ToString());
                toolt.Build();
                e.Row.Cells[0].Controls.Add(ltrl);

                btn.CommandArgument = e.Row.Cells[3].Text + ":" + e.Row.Cells[4].Text + ":" + e.Row.Cells[5].Text;
                //confirm = "IETM: [" + e.Row.Cells[4].Text + "]  REF: [" + e.Row.Cells[2].Text + "] ?";
                //StaticData.MsgConfirmBox(btn, confirm);
                tbltooltip.Visible = false;



                // view qotation


                HtmlTable tbl_party = (HtmlTable)ctl.FindControl("tbl_party");

                tbltooltip2.Visible = true;
                tbl_party = set_quot(e.Row.Cells[4].Text, e.Row.Cells[3].Text, tbl_party);
                tbltooltip2.Visible = false;
                //tbltooltip2.Visible = false;

            }
            else
            {
                ctl.Visible = false;
                btn.Visible = false;
            }

            // e.Row.Cells[2].Wrap = false;
            //e.Row.Cells[11].Wrap = false;
            //e.Row.Cells[3].Wrap = false;


        }
               

    }

    private HtmlTable set_quot(string product_code, string req_id, HtmlTable tbl_party)
    {
        tbl_tac_logTableAdapter log = new tbl_tac_logTableAdapter();
        SCBLDataSet.tbl_tac_logDataTable dtlog = new SCBLDataSet.tbl_tac_logDataTable();
        quotation_detTableAdapter quo = new quotation_detTableAdapter();
        SCBLDataSet.quotation_detDataTable dt = new SCBLDataSet.quotation_detDataTable();



        Label lbltooltip;
        Literal ltrl;
        els.ToolTip toolt;
        string tac_ref, genstr, spestr, paystr, pay_type;

        int cur_index, i, gcnt, scnt, pcnt;
        int vdays = 0;
        HtmlInputRadioButton rdolist;
        Control ctl;



        //quotation

        dt = quo.GetActiveQuote("", product_code, req_id);

        cur_index = 1;
        tbl_party.Rows[0].Cells[1].Visible = false;

        foreach (SCBLDataSet.quotation_detRow drquo in dt.Rows)
        {
            if (cur_index == 16) break;

            rdolist = new HtmlInputRadioButton();
            ctl = new Control();
            ctl = tbl_party.Rows[cur_index].Cells[0].Controls[1];
            rdolist = (HtmlInputRadioButton)ctl;
            rdolist.Visible = false;
            //totamount = (drquo.rate * qty).ToString("N2");


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


            tbltooltip2.Rows[0].Cells[0].InnerText = drquo.party_det.ToUpper();
            tbltooltip2.Rows[2].Cells[1].InnerHtml = genstr;
            tbltooltip2.Rows[3].Cells[1].InnerHtml = spestr;
            tbltooltip2.Rows[4].Cells[0].InnerHtml = "Pay Terms(" + pay_type + ")";
            tbltooltip2.Rows[4].Cells[1].InnerHtml = paystr;
            tbltooltip2.Rows[5].Cells[1].InnerHtml = vdays.ToString();

            lbltooltip = new Label();
            lbltooltip.ForeColor = System.Drawing.Color.Black;
            lbltooltip.Text = cur_index.ToString() + ".";
            tbl_party.Rows[cur_index].Cells[0].Controls.Add(lbltooltip);
            ltrl = new Literal();
            toolt = new els.ToolTip();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            tbltooltip2.RenderControl(hw);
            toolt.Add(lbltooltip, ltrl, sb.ToString());
            // toolt.Add(tbl_party.Rows[cur_index].Cells[2], ltrl, sb.ToString());
            toolt.Build();
            tbl_party.Rows[cur_index].Cells[0].Controls.Add(ltrl);

            tbl_party.Rows[cur_index].Cells[3].InnerText = drquo.rate.ToString("N2");

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
    private string get_flow_tepmate(string req_type, string cash_type, decimal totval)
    {
        App_Flow_DefinitionTableAdapter app = new App_Flow_DefinitionTableAdapter();
        SCBLDataSet.App_Flow_DefinitionDataTable dt = new SCBLDataSet.App_Flow_DefinitionDataTable();

        string tem_id = "";
        dt = app.GetTemplate("CS", req_type, cash_type, totval);

        if (dt.Rows.Count > 0)
            tem_id = dt[0].flow_id.ToString();


        return tem_id;
    }
    private string get_pend_for(string selval)
    {
        string pend_for = "";
        tbl_app_ruleTableAdapter tbl = new tbl_app_ruleTableAdapter();
        try
        {
            pend_for = tbl.GetDataByTypeSeq(selval, 1)[0].app_id.ToString();
        }
        catch
        {
            pend_for = "";
        }
        return pend_for;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
       
       
        Button btn = (Button)sender;
        string[] tmp = btn.CommandArgument.Split(':');
        if (tmp.Length < 3) return;

        string mpr_no = tmp[0].ToString();
        string icode = tmp[1].ToString();
        string idet = tmp[2].ToString();
        lblmpr.Text = mpr_no;
        lblitem.Text = icode + ":" + idet;

        txtcomments.Text = "";
        chkurgent.Checked = false;

        ModalPopupExtender1.Show();
    }
    
    
    protected void btnQuotation_Click(object sender, EventArgs e)
    {
        int tot = 0;

        CheckBox chk;
        string lblcode, lblref;
        string item_list = "";
        string Itemcode = "";


        ddlquotlog.Text = "";


        foreach (GridViewRow gr in gdItem.Rows)
        {
            chk = new CheckBox();
            chk = (CheckBox)gr.FindControl("CheckBox1");

            lblref = gr.Cells[3].Text;
            lblcode = gr.Cells[4].Text;



            if (chk.Checked)
            {
                tot += 1;
                item_list = item_list + lblref + ":" + lblcode + "+";

                if (tot == 1)
                    Itemcode = "('" + lblcode + "'";
                else
                    Itemcode = Itemcode + ",'" + lblcode + "'";
            }

        }

        if (tot == 0) { Response.Redirect(Request.Url.AbsoluteUri); return; }

        Session[clsStatic.sessionSelvalforQuo] = item_list;
        Session[clsStatic.sessionSelvalforItmCode] = Itemcode+")";

        Response.Redirect("./frm_quotation_add.aspx");   
              
              
               
    }
       

    protected void ddlquotlog_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selval = ddlquotlog.SelectedValue.ToString();
        CheckBox chk;

        if (selval != "")
        {

            quotation_logTableAdapter qlog = new quotation_logTableAdapter();
            SCBLDataSet.quotation_logRow logdr;


            logdr = qlog.GetDataById(selval)[0];
            string[] tmparr, tmp;
            string ref_no, icode;
            int cnt, i;


            tmparr = logdr.item_code_det.Split('+');

            cnt = tmparr.Length;

            for (i = 0; i < cnt; i++)
            {
                if (tmparr[i] != "")
                {
                    tmp = tmparr[i].Split(':');
                    ref_no = tmp[0];
                    icode = tmp[1];

                    foreach (GridViewRow gr in gdItem.Rows)
                    {
                        chk = new CheckBox();
                        chk = (CheckBox)gr.FindControl("CheckBox1");

                        if (gr.Cells[2].Text == icode)
                        {
                            Session[clsStatic.sessionSelvalforQuo] = selval;
                            Response.Redirect("./frm_quotation_add_ddl.aspx");
                        }
                    }
                }

            }


        }
    }
    protected void gdItem_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ButtonOk_Click(object sender, EventArgs e)
    {
        string[]  tmp= lblitem.Text.Split(':');
        string mpr_no = lblmpr.Text;
        string icode = tmp[0].ToString();
        string strcomments = txtcomments.Text;
        string t_fl = "";
        if (chkurgent.Checked) { t_fl = "U"; strcomments = "[URGENT] " + strcomments; }
                    

        bool flg = false;
        LibraryDAL.dsLinkofficeTableAdapters.tblUserInfoTableAdapter usr = new LibraryDAL.dsLinkofficeTableAdapters.tblUserInfoTableAdapter();
        SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable dt_srdet = new SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable();
        PuTr_IN_Det_ScblTableAdapter In_Det = new PuTr_IN_Det_ScblTableAdapter();
        PuTr_IN_Det_Scbl2TableAdapter In_Det2 = new PuTr_IN_Det_Scbl2TableAdapter();
        quotation_detTableAdapter quo = new quotation_detTableAdapter();
        SCBLDataSet.quotation_detDataTable qtbl = new SCBLDataSet.quotation_detDataTable();
        tbl_CommentsTableAdapter comm = new tbl_CommentsTableAdapter();

        string userid, user_name, new_ref, app_flow, pending_for;
        double max_ref;


        userid = current.UserId.ToString();
        user_name = current.UserName.ToString();
        string desig = usr.GetUserByCode(userid)[0].UserDesignation.ToString();


        dt_srdet = In_Det2.GetDataByRefItem(mpr_no, icode);

        qtbl = quo.GetActiveQuot_rate("", icode, mpr_no);

        if (qtbl.Rows.Count == 0 || dt_srdet[0].In_Det_Status != "TEN")
            return;

        //set app flow and pending for

        decimal csval = Convert.ToDecimal(qtbl[0].qty) * Convert.ToDecimal(qtbl[0].rate);

        app_flow = get_flow_tepmate(mpr_no.Substring(0, 2) + "MPR", "LPO", csval);
        pending_for = get_pend_for(app_flow);

        if (pending_for == "") return;

        max_ref = Convert.ToDouble(quo.GetMaxRef()) + 1;
        new_ref = "QUO-" + string.Format("{0:000000}", max_ref);

        SqlTransaction myTrn = HelperTA.OpenTransaction(In_Det.Connection);

        try
        {
            In_Det.AttachTransaction(myTrn);
            quo.AttachTransaction(myTrn);

            In_Det.UpdateFromQuoProceed("QUO", pending_for, new_ref, app_flow, t_fl, "TEN", icode, mpr_no);
            quo.UpdatefromQuoFor(new_ref, "", icode, mpr_no);
            comm.InsertComments(new_ref, 1, DateTime.Now, userid, user_name, desig, 1, "QUO", "RUN", strcomments, "", pending_for);
            myTrn.Commit();
            flg = true;

        }
        catch
        {
            myTrn.Rollback();
        }
        finally
        {
            HelperTA.CloseTransaction(In_Det.Connection, myTrn);
        }

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

            msub = "A Comperative Statement created and pending for you [" + mpr_no + "]";
            mbody = "\n\n " + "A Comperative Statement created and pending for you [" + mpr_no + "]";
            mbody += "\n " + "To view details please login in at http://203.76.114.131/cml/cmlcom";
            mbody += "\n " + "**THIS IS AUTO GENERATED EMAIL AND DONT REQUIRE A REPLY.**";

            roledt = roleta.GetDataByRole(pending_for);

            foreach (SCBLDataSet.User_Role_DefinitionRow rdr in roledt.Rows)
            {
                rec_det[remailcnt] = new clsEmailReceiver();
                rec_det[remailcnt].Rname = rdr.user_name;
                rec_det[remailcnt].Rid = usrdal.GetUserByCode(rdr.user_id)[0].UserEmail;
                remailcnt++;
            }
            clsStatic.SendMail(sid, sname, rec_det, msub, mbody);

        }


        Response.Redirect(Request.Url.AbsoluteUri);


    }
}
