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
using LibraryDTO;
using LibraryDAL.SCBLDataSetTableAdapters;
using LibraryDAL.ErpDataSetTableAdapters;

public partial class frm_mpr_approval : System.Web.UI.Page
{
   

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.MsgConfirmBox(btnapprove, "Are you sure to approve ? ");
        clsStatic.MsgConfirmBox(btnreject, "Are you sure to reject ? ");
        clsStatic.MsgConfirmBox(btnforward, "Are you sure to forward ? ");

        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";             

        if (!Page.IsPostBack)
        {
            load_pending_list();                       
        }
        else
        {
            generate_comments(ddllist.SelectedValue.ToString());
        }
          
    }
    private string get_my_app()
    {
        User_Role_DefinitionTableAdapter urole = new User_Role_DefinitionTableAdapter();
        SCBLDataSet.User_Role_DefinitionDataTable udt = new SCBLDataSet.User_Role_DefinitionDataTable();
        string my_app = "";
        udt = urole.GetRoleByUser(current.UserId.ToString(),"MPR");

        if (udt.Rows.Count > 0) my_app = udt[0].role_as.ToString();

        return my_app;
    }

    private void load_pending_list()
    {
        PuTr_IN_Hdr_ScblTableAdapter srhdr = new PuTr_IN_Hdr_ScblTableAdapter();
        SCBLDataSet.PuTr_IN_Hdr_ScblDataTable srdt = new SCBLDataSet.PuTr_IN_Hdr_ScblDataTable();
        ListItem lst;
        string my_app = get_my_app();
        
        srdt = srhdr.GetPending("RUN", my_app);

        if (srdt.Rows.Count == 0)
        {
            Response.Redirect("./frm_com_inbox.aspx");
        }
        if (my_app == "") return;

        ddllist.Items.Clear();
        ddllist.Items.Add("");
        foreach (SCBLDataSet.PuTr_IN_Hdr_ScblRow dr in srdt.Rows)
        {
            lst = new ListItem();
            lst.Value = dr.IN_Hdr_Ref.ToString();
            lst.Text = dr.IN_Hdr_Ref.ToString() + "  [" + dr.IN_Hdr_St_DATE.ToString()+"]";
            ddllist.Items.Add(lst);
        }

        lblcount.Text = "(" + srdt.Rows.Count.ToString() + ")";
               
        tbl_po.Visible = false;
    }
    
    private void BindMyGridview()
    {
        DataTable dt = new DataTable();

        dt = (DataTable)Session[clsStatic.sessionTempDatatable];

        gdItem.DataSource = dt;
        gdItem.DataBind();
    }
        
    
    protected void gdItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowState==DataControlRowState.Edit)
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
            //e.Row.Cells[3].ForeColor = System.Drawing.Color.Blue;
           
            e.Row.Cells[2].Enabled = false;
            e.Row.Cells[3].Enabled = false;
            e.Row.Cells[4].Enabled = false;
            e.Row.Cells[5].Enabled = false;
            e.Row.Cells[6].Enabled = false;
            e.Row.Cells[8].Enabled = false;
            e.Row.Cells[9].Enabled = false;
            //e.Row.Cells[9].Enabled = false;
            //e.Row.Cells[10].Enabled = false;
            //e.Row.Cells[11].Enabled = false;
            //e.Row.Cells[12].Enabled = false;
            //e.Row.Cells[13].Enabled = false;
            

        }

    }
    protected void gdItem_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gdItem.EditIndex = e.NewEditIndex;        
        TextBox txt = new TextBox();
        BindMyGridview();        

    }

    protected void gdItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        decimal valqty,rate;
        string specification, brand, origin, packing, remarks;
        DateTime etrdate;

        GridViewRow ROW = gdItem.Rows[e.RowIndex];
        DataTable dt = (DataTable)Session[clsStatic.sessionTempDatatable];        
        DataRow dr = dt.Rows[e.RowIndex];

        try
        {
            valqty = Convert.ToDecimal(((TextBox)(ROW.Cells[7].Controls[0])).Text);
            rate = Convert.ToDecimal(((TextBox)(ROW.Cells[8].Controls[0])).Text);
            specification = ((TextBox)(ROW.Cells[10].Controls[0])).Text;
            brand = ((TextBox)(ROW.Cells[11].Controls[0])).Text;
            origin = ((TextBox)(ROW.Cells[12].Controls[0])).Text;
            packing = ((TextBox)(ROW.Cells[13].Controls[0])).Text;
            etrdate = Convert.ToDateTime(((TextBox)(ROW.Cells[14].Controls[0])).Text);
            remarks = ((TextBox)(ROW.Cells[15].Controls[0])).Text;

            dr["Qty"] = valqty.ToString("N2");
            dr["Amnt"] = (valqty*rate).ToString("N2");

            dr["Specification"] = specification;
            dr["Brand"] = brand;
            dr["Origin"] = origin;
            dr["Packing"] = packing;
            dr["ETR"] = etrdate.ToShortDateString();
            dr["Remarks"] = remarks;
                                  


            dr.AcceptChanges();
            Session[clsStatic.sessionTempDatatable] = dt;
        }
        catch
        {
        }

        gdItem.EditIndex = -1;
        BindMyGridview();
    }

    public void gdItem_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gdItem.EditIndex = -1;
        BindMyGridview();
        //Session["SelecetdRowIndex"] = -1;

    }
    protected void gdItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt = (DataTable)Session[clsStatic.sessionTempDatatable];
        DataRow dr = dt.Rows[e.RowIndex]; 
        dt.Rows.Remove(dr);
        Session[clsStatic.sessionTempDatatable] = dt;
        gdItem.EditIndex = -1;
        BindMyGridview();
    }

    private void generate_detail_data(string ref_no)
    {
        LibraryDAL.ErpDataSetTableAdapters.InMa_Stk_CtlTableAdapter stk = new LibraryDAL.ErpDataSetTableAdapters.InMa_Stk_CtlTableAdapter();
        ErpDataSet.InMa_Stk_CtlDataTable dtstk = new ErpDataSet.InMa_Stk_CtlDataTable();

        PuTr_IN_Hdr_ScblTableAdapter hdr = new PuTr_IN_Hdr_ScblTableAdapter();
        SCBLDataSet.PuTr_IN_Hdr_ScblDataTable dthdr = new SCBLDataSet.PuTr_IN_Hdr_ScblDataTable();
        PuTr_IN_Det_ScblTableAdapter det = new PuTr_IN_Det_ScblTableAdapter();
        SCBLDataSet.PuTr_IN_Det_ScblDataTable dtdet = new SCBLDataSet.PuTr_IN_Det_ScblDataTable();
        DataTable dt = new DataTable();

        string freestk = "";

        decimal amnt;

        dthdr = hdr.GetDataByRef(ref_no);
        dtdet=det.GetDataByInRef(ref_no);
        lbldate.Text= dtdet[0].IN_Det_Exp_Dat.ToString();
        lblref.Text = ref_no;
        lblcomments.Text = dthdr[0].IN_Hdr_Com4;
        lbldept.Text = dthdr[0].IN_Hdr_Pcode + ":" + dthdr[0].IN_Hdr_Com5;

        dt.Rows.Clear();
        dt.Columns.Clear();
        dt.Columns.Add("Sl", typeof(int));
        dt.Columns.Add("Item Code", typeof(string));
        dt.Columns.Add("Item Desc", typeof(string));
        dt.Columns.Add("UOM", typeof(string));
        dt.Columns.Add("Free Stk", typeof(string));
        dt.Columns.Add("Qty", typeof(string));
        dt.Columns.Add("Rate", typeof(string));
        dt.Columns.Add("Amnt", typeof(string));
        dt.Columns.Add("Specification", typeof(string));
        dt.Columns.Add("Brand", typeof(string));
        dt.Columns.Add("Origin", typeof(string));
        dt.Columns.Add("Packing", typeof(string));
        dt.Columns.Add("ETR", typeof(string));
        dt.Columns.Add("Remarks", typeof(string));

        foreach (SCBLDataSet.PuTr_IN_Det_ScblRow dr in dtdet.Rows)
        {
            dtstk = new ErpDataSet.InMa_Stk_CtlDataTable();
            dtstk = stk.GetDataByItemStore(dr.IN_Det_Icode, ref_no.Substring(0, 2) + "GEN");
            if (dtstk.Rows.Count == 0)
            {
                freestk = "";
            }
            else
            {
                freestk = dtstk[0].Stk_Ctl_Free_Stk.ToString("N2");
            }
            amnt = (decimal)dr.IN_Det_Lin_Qty * dr.IN_Det_Lin_Rat;
            dt.Rows.Add((int)dr.IN_Det_Lno, dr.IN_Det_Icode, dr.IN_Det_Itm_Desc, dr.IN_Det_Itm_Uom, freestk, dr.IN_Det_Lin_Qty.ToString(), dr.IN_Det_Lin_Rat.ToString("N2"), amnt, dr.In_Det_Specification, dr.In_Det_Brand, dr.In_Det_Origin, dr.In_Det_Packing, dr.IN_Det_Exp_Dat.ToShortDateString(),dr.In_Det_Remarks);
        }

        Session[clsStatic.sessionTempDatatable] = dt;
        BindMyGridview();
    }

    private void generate_comments(string ref_no)
    {
        tbl_CommentsTableAdapter com = new tbl_CommentsTableAdapter();
        SCBLDataSet.tbl_CommentsDataTable dt = new SCBLDataSet.tbl_CommentsDataTable();

        dt = com.GetCommentsByRef(ref_no);
        phcomm.Controls.Clear();
        foreach (SCBLDataSet.tbl_CommentsRow dr in dt.Rows)
        {
            ClientSide_modules_commercial_usercontrols_ctl_comments ctl = (ClientSide_modules_commercial_usercontrols_ctl_comments)LoadControl("./usercontrols/ctl_comments.ascx");
            Label lblname = (Label)ctl.FindControl("lblname");
            Label lbldate = (Label)ctl.FindControl("lbldate");
            HtmlTableCell celcomm = (HtmlTableCell)ctl.FindControl("celcomm");
            Image imgimage = (Image)ctl.FindControl("imgimage");

            imgimage.ImageUrl = "~/handler/hndImage.ashx?id=" + dr.app_id;

            ctl.ID = "ctl_" + phcomm.Controls.Count.ToString();

            lblname.Text = dr.app_name.ToString() + " (" + dr.app_designation.ToString() + ")";
            lbldate.Text = dr.app_date.ToString();
            celcomm.InnerText = dr.gen_comments.ToString();

            phcomm.Controls.Add(ctl);
        }
    }

    private void set_permission(string ref_no)
    {
        tbl_app_ruleTableAdapter app = new tbl_app_ruleTableAdapter();
        SCBLDataSet.tbl_app_ruleDataTable dt = new SCBLDataSet.tbl_app_ruleDataTable();
        PuTr_IN_Hdr_ScblTableAdapter hdr = new PuTr_IN_Hdr_ScblTableAdapter();
        App_Type_DetTableAdapter apptype = new App_Type_DetTableAdapter();

        ListItem lst;

        string template = hdr.GetDataByRef(ref_no)[0].IN_Hdr_Template.ToString();         
        string app_code = get_my_app();
        string tmpstr;

        ddlforto.Items.Clear();
        ddlforto.Items.Add("");

        dt = app.GetDataByTypeApp(template, app_code);

        if (dt.Rows.Count == 0)
        {
            btnapprove.Visible = false;
            btnforward.Visible = false;
            btnreject.Visible = false;            
        }
        else
        {
            if (dt[0].rej_per == 1) btnreject.Visible = true; else btnreject.Visible = false;
            if (dt[0].app_per == 1) btnapprove.Visible = true; else btnapprove.Visible = false;
            if (dt[0].edit_per == 1) gdItem.Enabled = true; else gdItem.Enabled = false;

            tmpstr=dt[0].for_1.ToString();            
            if ( tmpstr!= "")
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

    protected void ddllist_SelectedIndexChanged(object sender, EventArgs e)
    {
        load_data();
        txtcomments.Text = "";
    }


    private void load_data()
    {
        
        string selitem = ddllist.SelectedItem.Value.ToString();
        lblComm.Visible = false;
        if (selitem == "")
        {
            tbl_po.Visible = false;
        }
        else
        {
            tbl_po.Visible = true;
            generate_detail_data(selitem);
            generate_comments(selitem);
            set_permission(selitem);
        }
    }
    
   

    private bool savedata(string status, string pend_for,string hpcflg,string ref_no)
    {
        bool flg = true;
        string uid = current.UserId.ToString();
        string uname = current.UserName.ToString();
        string desig = "";
        string role_as = "";       
        int rcnt;
        string icode, ticode, specification, brand, origin, packing, remarks;
        DateTime etrdate;
        decimal qty, rate,amnt;
        decimal totamnt = 0;
        bool found;
        string pend_for_rout;
        

        DataTable dt = new DataTable();
        dt = (DataTable)Session[clsStatic.sessionTempDatatable];

        if (dt.Rows.Count == 0) { flg = false; goto errhndlr; }
        

        PuTr_IN_Hdr_ScblTableAdapter hdr = new PuTr_IN_Hdr_ScblTableAdapter();
        PuTr_IN_Det_ScblTableAdapter det = new PuTr_IN_Det_ScblTableAdapter();
        tbl_CommentsTableAdapter comm = new tbl_CommentsTableAdapter();
        LibraryDAL.dsLinkofficeTableAdapters.tblUserInfoTableAdapter usr=new LibraryDAL.dsLinkofficeTableAdapters.tblUserInfoTableAdapter();
        SCBLDataSet.PuTr_IN_Det_ScblDataTable dtdet = new SCBLDataSet.PuTr_IN_Det_ScblDataTable();

        desig = usr.GetUserByCode(uid)[0].UserDesignation.ToString();
        role_as=get_my_app();


        dtdet = det.GetDataByInRef(ref_no);

        SqlTransaction myTrn = HelperTA.OpenTransaction(hdr.Connection);

        try
        {
            hdr.AttachTransaction(myTrn);
            det.AttachTransaction(myTrn);
            comm.AttachTransaction(myTrn);


            if (status == "APP")    pend_for_rout = "ROU";  else pend_for_rout = status;           
                      

            //skip detail for reject
           

            if (status == "REJ") goto skipForReject;

            //delete which not found                      

            foreach (SCBLDataSet.PuTr_IN_Det_ScblRow drdet in dtdet.Rows)
            {
                ticode = drdet.IN_Det_Icode.ToString();

                found=false;

                foreach (DataRow dr in dt.Rows)
                {
                    icode = dr["Item Code"].ToString();
                    if (ticode == icode) found = true;
                }

                if (found == false)
                {
                    det.DeleteDetDataByRefItem(ref_no, ticode);
                }
            }

            //update which found
            rcnt = 0;
            
            foreach (DataRow dr in dt.Rows)
            {
                icode=dr["Item Code"].ToString();
                qty=Convert.ToDecimal(dr["Qty"]);
                rate = Convert.ToDecimal(dr["Rate"]);

                specification = dr["Specification"].ToString();
                brand = dr["Brand"].ToString();
                origin = dr["Origin"].ToString();
                packing = dr["Packing"].ToString();
                etrdate = Convert.ToDateTime(dr["ETR"].ToString());
                remarks = dr["Remarks"].ToString();

                amnt = qty * rate;
                totamnt = totamnt + amnt;
                rcnt = rcnt + 1;
                det.UpdatePutrInDet((short)rcnt, pend_for_rout, (double)qty, 0, (double)qty, amnt, amnt,specification, brand, origin, packing, etrdate, remarks, ref_no, icode);               
            }
          
            skipForReject:

            comm.InsertComments(ref_no, 1, DateTime.Now, uid, uname, desig, 1, role_as, status, txtcomments.Text, "", pend_for);

            if (status == "APP")
            {

                dtdet = det.GetDataByInRef(ref_no);
                hdr.UpdateMprApprove(status, hpcflg, uid, totamnt,DateTime.Now, ref_no);                                    
            }
            else
            {
                hdr.UpdateMprForward(status, pend_for, hpcflg, uid,totamnt, ref_no);
            }

            if (flg)
            {
                myTrn.Commit();
            }
            else
            {
                myTrn.Rollback();
            }


        }
        catch
        {
            myTrn.Rollback();
            flg = false;
        }
        finally
        {
            HelperTA.CloseTransaction(hdr.Connection, myTrn);
        }
        
        errhndlr:
        return flg;

    }

    private bool Check_Approval_Validity(string ref_no)
    {
        PuTr_IN_Hdr_ScblTableAdapter hdr = new PuTr_IN_Hdr_ScblTableAdapter();
        bool flg = false;
        string myapp = get_my_app();
        if (hdr.GetDataByRef(ref_no)[0].IN_Hdr_Pending.ToString() == myapp) { flg = true; }

        return flg;
    }

    protected void btnforward_Click(object sender, EventArgs e)
    {
        bool flg = false;
        if (txtcomments.Text == "") { lblComm.Text = "Pls type your comments "; lblComm.Visible = true; return; }
        if (ddlforto.Text == "") { lblComm.Text = "Pls select forward to "; lblComm.Visible = true; return; }

        if (Check_Approval_Validity(lblref.Text) == false) { return; }

        if (savedata("RUN", ddlforto.SelectedValue.ToString(), "H", lblref.Text) == true)
        {
            load_pending_list();
            ddllist.Text = "";
            load_data();
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

            msub = "A purchase requisition forwarded to you [" + lblref.Text + "]";
            mbody = "\n\n " + "A purchase requisition forwarded to you [" + lblref.Text + "]";
            mbody += "\n " + "To view details please login in at http://office.citycable.net/";
            mbody += "\n " + "**THIS IS AUTO GENERATED EMAIL AND DONT REQUIRE A REPLY.**";

            roledt = roleta.GetDataByRole(ddlforto.SelectedValue.ToString());

            foreach (SCBLDataSet.User_Role_DefinitionRow rdr in roledt.Rows)
            {
                rec_det[remailcnt] = new clsEmailReceiver();
                rec_det[remailcnt].Rname = rdr.user_name;
                if (usrdal.GetUserByCode(rdr.user_id).Rows.Count > 0)
                    rec_det[remailcnt].Rid = usrdal.GetUserByCode(rdr.user_id)[0].UserEmail;
                else
                    rec_det[remailcnt].Rid = "";

                remailcnt++;
            }            

            clsStatic.SendMail(sid, sname, rec_det, msub,mbody);
            
        }
    }
    protected void btnapprove_Click(object sender, EventArgs e)
    {
        bool flg = false;
        if (Check_Approval_Validity(lblref.Text) == false) { return; }

        if (savedata("APP", "", "P", lblref.Text) == true)
        {
            load_pending_list();
            ddllist.Text = "";
            load_data();
            flg = true;
        }
        else { lblComm.Text = "ERROR"; lblComm.Visible = true; }

        if (flg)
        {

            string sid, sname, msub, mbody;
            clsEmailReceiver[] rec_det = new clsEmailReceiver[20];
            int remailcnt = 0;
            dsLinkoffice.tblUserInfoRow  udr;
            LibraryDAL.dsLinkofficeTableAdapters.tblUserInfoTableAdapter usrdal = new LibraryDAL.dsLinkofficeTableAdapters.tblUserInfoTableAdapter();
            LibraryDAL.dsLinkoffice.tblUserInfoDataTable dtusr = new LibraryDAL.dsLinkoffice.tblUserInfoDataTable();
            User_Role_DefinitionTableAdapter roleta = new User_Role_DefinitionTableAdapter();
            SCBLDataSet.User_Role_DefinitionDataTable roledt = new SCBLDataSet.User_Role_DefinitionDataTable();
            tbl_CommentsTableAdapter comm = new tbl_CommentsTableAdapter();
            SCBLDataSet.tbl_CommentsDataTable dtcom = new SCBLDataSet.tbl_CommentsDataTable();

            udr = usrdal.GetUserByCode(current.UserId.ToString())[0];
            sid = udr.UserEmail.ToString();
            sname = udr.UserName.ToString();

            msub = "A purchase requisition approved [" + lblref.Text + "]";
            mbody = "\n\n " + "A purchase requisition approved and pending for you [" + lblref.Text + "]";
            mbody += "\n " + "To view details please login in at http://203.76.114.131/cml/cmlcom ";
            mbody += "\n " + "**THIS IS AUTO GENERATED EMAIL AND DONT REQUIRE A REPLY.**";

            roledt = roleta.GetDataByRole("ROU");

            foreach (SCBLDataSet.User_Role_DefinitionRow rdr in roledt.Rows)
            {
                rec_det[remailcnt] = new clsEmailReceiver();
                rec_det[remailcnt].Rname = rdr.user_name;
                rec_det[remailcnt].Rid = usrdal.GetUserByCode(rdr.user_id)[0].UserEmail;
                remailcnt++;
            }

            dtcom = comm.GetDataByRefCurStatus(lblref.Text, "INI");

            if (dtcom.Rows.Count > 0)
            {
                string srt_ini = comm.GetDataByRefCurStatus(lblref.Text, "INI")[0].app_id;
                dtusr = usrdal.GetUserByCode(srt_ini);
                if (dtusr.Rows.Count > 0)
                {
                    rec_det[remailcnt] = new clsEmailReceiver();
                    rec_det[remailcnt].Rname = dtusr[0].UserName;
                    rec_det[remailcnt].Rid = dtusr[0].UserEmail;
                }
            }

            

            clsStatic.SendMail(sid, sname, rec_det, msub, mbody);

        }

    }
    
    protected void btnreject_Click(object sender, EventArgs e)
    {
        if (txtcomments.Text == "") { lblComm.Text = "Pls type your comments "; lblComm.Visible = true; return; }
        if (Check_Approval_Validity(lblref.Text) == false) { return; }

        if (savedata("REJ", "", "R", lblref.Text) == true)
        {
            load_pending_list();
            ddllist.Text = "";
            load_data();

            //send email

            string sid, sname, msub, mbody;
            clsEmailReceiver[] rec_det = new clsEmailReceiver[20];
            int remailcnt = 0;
            dsLinkoffice.tblUserInfoRow  udr;
            LibraryDAL.dsLinkofficeTableAdapters.tblUserInfoTableAdapter usrdal = new LibraryDAL.dsLinkofficeTableAdapters.tblUserInfoTableAdapter();
            LibraryDAL.dsLinkoffice.tblUserInfoDataTable dtusr = new LibraryDAL.dsLinkoffice.tblUserInfoDataTable();     
            
            tbl_CommentsTableAdapter comm = new tbl_CommentsTableAdapter();
            SCBLDataSet.tbl_CommentsDataTable dtcom = new SCBLDataSet.tbl_CommentsDataTable();

            udr = usrdal.GetUserByCode(current.UserId.ToString())[0];
            sid = udr.UserEmail.ToString();
            sname = udr.UserName.ToString();

            msub = "A purchase requisition REJECTED [" + lblref.Text + "]";
            mbody = "\n\n " + "A purchase requisition REJECTED [" + lblref.Text + "]";
            mbody += "\n " + "To view details please login in at http://203.76.114.131/cml/cmlcom ";
            mbody += "\n " + "**THIS IS AUTO GENERATED EMAIL AND DONT REQUIRE A REPLY.**";

           
            dtcom = comm.GetDataByRefCurStatus(lblref.Text, "INI");

            if (dtcom.Rows.Count > 0)
            {
                string srt_ini = comm.GetDataByRefCurStatus(lblref.Text, "INI")[0].app_id;
                dtusr = usrdal.GetUserByCode(srt_ini);
                if (dtusr.Rows.Count > 0)
                {
                    rec_det[remailcnt] = new clsEmailReceiver();
                    rec_det[remailcnt].Rname = dtusr[0].UserName;
                    rec_det[remailcnt].Rid = dtusr[0].UserEmail;
                }
            }
            
            clsStatic.SendMail(sid, sname, rec_det, msub, mbody);


        }
        else { lblComm.Text = "ERROR"; lblComm.Visible = true; }

    }

 

    protected void btnreload_Click(object sender, EventArgs e)
    {
        load_data();
    }
    protected void gdItem_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
