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
using System.Threading;
using LibraryDAL;
using LibraryDTO;
using LibraryDAL.SCBLDataSetTableAdapters;
using LibraryDAL.ErpDataSetTableAdapters;
public partial class frm_purchase_requisition : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication();
        clsStatic.MsgConfirmBox(btnSubmit, "Are you sure to submit?");
        tblmaster.BgColor = "FFFFFFF";
        if (!Page.IsPostBack)
        {
            Session[clsStatic.sessionTempDatatable] = null;
            getcombodata();
            txtDate.Text = DateTime.Now.Date.ToShortDateString();
            cldetr.SelectedDate = DateTime.Now.AddDays(10);
            set_grid();
            
        }
        else
        {           

        }
    }


    private void getcombodata()
    {
        LibraryDAL.SCBLINTableAdapters.Hrms_Dept_MasterTableAdapter dpt = new LibraryDAL.SCBLINTableAdapters.Hrms_Dept_MasterTableAdapter();
        SCBLIN.Hrms_Dept_MasterDataTable dtdept = new SCBLIN.Hrms_Dept_MasterDataTable();

        InMa_Str_LocTableAdapter str = new InMa_Str_LocTableAdapter();
        ErpDataSet.InMa_Str_LocDataTable dtstr = new ErpDataSet.InMa_Str_LocDataTable();

        tbl_trn_detTableAdapter trn = new tbl_trn_detTableAdapter();
        SCBLDataSet.tbl_trn_detDataTable dttrn = new SCBLDataSet.tbl_trn_detDataTable();

        User_Role_DefinitionTableAdapter uda = new User_Role_DefinitionTableAdapter();
        SCBLDataSet.User_Role_DefinitionDataTable udt=new SCBLDataSet.User_Role_DefinitionDataTable();
        ListItem lst;
        string ucode = current.UserId.ToString();
        int i;
        string[] items;

        //set noe

        ddlstore.Items.Clear();
        ddlstore.Items.Add("");
        dtstr = str.GetAllStore();

        foreach (ErpDataSet.InMa_Str_LocRow dr in dtstr.Rows)
        {
            lst = new ListItem();
            lst.Value = dr.Str_Loc_Id;
            lst.Text = dr.Str_Loc_Id + ":" + dr.Str_Loc_Name;
            ddlstore.Items.Add(lst);
        }


        ddlnoe.Items.Clear();
        ddlnoe.Items.Add("");
        ddlnoe.Items.Add("Capital Expense");
        ddlnoe.Items.Add("Consumable Spares");
        ddlnoe.Items.Add("Raw Meterials");
        ddlnoe.Items.Add("Packing Meterials");

        //set dept

        ddldept.Items.Clear();
        dtdept = dpt.GetAllDept();

        foreach (SCBLIN.Hrms_Dept_MasterRow dr in dtdept.Rows)
        {
            lst = new ListItem();
            lst.Value = dr.Dept_Code;
            lst.Text = dr.Dept_Code + ":" + dr.Dept_Name;
            ddldept.Items.Add(lst);
        }

            

        
         


    }

   
    
    private void set_grid()
    {
        btnSubmit.Visible = false;
        gdItem.Visible = false;

        if(Session[clsStatic.sessionTempDatatable] != null)
        {
            DataTable dt = new DataTable();
            DataTable dtgrid = new DataTable();
            int cnt = 0;
           
            dt = (DataTable)Session[clsStatic.sessionTempDatatable];

            if (dt.Rows.Count > 0)
            {
                btnSubmit.Visible = true;
                gdItem.Visible = true;

                dtgrid.Rows.Clear();
                dtgrid.Columns.Clear();

                dtgrid.Columns.Add("Sl", typeof(int));
                dtgrid.Columns.Add("Item Code", typeof(string));
                dtgrid.Columns.Add("Item Desc", typeof(string));
                dtgrid.Columns.Add("UOM", typeof(string));
                dtgrid.Columns.Add("Free Qty", typeof(string));
                dtgrid.Columns.Add("Req Qty", typeof(string));
                dtgrid.Columns.Add("Store Code", typeof(string));
                dtgrid.Columns.Add("Store Desc", typeof(string));
                dtgrid.Columns.Add("Specification", typeof(string));
                dtgrid.Columns.Add("Brand", typeof(string));
                dtgrid.Columns.Add("Origin", typeof(string));
                dtgrid.Columns.Add("Packing", typeof(string));
                dtgrid.Columns.Add("ETR", typeof(string));
                dtgrid.Columns.Add("Nature of Exp", typeof(string));
                dtgrid.Columns.Add("Location", typeof(string));
                dtgrid.Columns.Add("Remarks", typeof(string));
                               

                foreach (DataRow dr in dt.Rows)
                {                    
                    cnt += 1;
                    dtgrid.Rows.Add(cnt, dr["icode"].ToString(), dr["idesc"].ToString(), dr["uom"].ToString(), dr["free"].ToString(), dr["qty"].ToString(), dr["scode"].ToString(), dr["sdesc"].ToString(), dr["spe"].ToString(), dr["brand"].ToString(), dr["origin"].ToString(), dr["packing"].ToString(), dr["etr"].ToString(), dr["noe"].ToString(), dr["loc"].ToString(), dr["remarks"].ToString());
                }

                gdItem.DataSource = dtgrid;
                gdItem.DataBind();

            }

        }
    }

    private bool chk_duplicate_data(string icode )
    {
        InMa_Itm_DetTableAdapter itm = new InMa_Itm_DetTableAdapter();

        bool flg = true;        
        
        DataTable dt = new DataTable();
        dt = (DataTable)Session[clsStatic.sessionTempDatatable];
        if (dt != null)
        {
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["icode"].ToString() == icode) { flg = false; }
                }
                
            }            
        }
        

        return flg;
    }

    //private decimal chk_budget(string icode, decimal qty, decimal rate)
    //{
    //    tbl_item_budgetTableAdapter bud = new tbl_item_budgetTableAdapter();
    //    decimal totamnt = qty * rate;
    //    string cur_session;      
    //    int curyear = DateTime.Now.Year;
    //    if (DateTime.Now.Month > 6) cur_session = curyear.ToString() + "/" + (curyear + 1).ToString();
    //    else cur_session = (curyear - 1).ToString() + "/" + curyear.ToString();
    //    decimal flg;

    //    SCBLDataSet.tbl_item_budgetDataTable dtbud = new SCBLDataSet.tbl_item_budgetDataTable();
        
    //    dtbud = bud.GetDataByItemSession(icode, cur_session);       
    //    if (dtbud.Rows.Count == 0)
    //    {
    //        flg = -1;
    //        goto last;
    //    }

    //    flg = totamnt;

    //    if (dtbud[0].available_limit < totamnt)
    //        flg = -1;
    // last:

    //    return flg;
    //}

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        txtitem.Focus();

        lblmsg.Visible = false;
        DataTable dt=new DataTable();
        string[] tdata;
        string icode, idesc, sdesc,uom;        
        decimal qty;

        tdata = txtitem.Text.Split(':');

        if ((tdata.Length < 3)||(txtqty.Text==""))
        {
            lblmsg.Text = "Item/Quantity Error";
            lblmsg.Visible = true;
            return;
        }
        
        qty = Convert.ToDecimal(txtqty.Text);

        if( qty <= 0)
        {
            lblmsg.Text = "Quantity Error";
            lblmsg.Visible = true;
            return;
        }

        icode = tdata[0].Trim();       
                
        if (chk_duplicate_data(icode) == false)
        {
            lblmsg.Text = "Item Already Added /IT Items mixed";
            lblmsg.Visible = true;
            return;
        }

        if (ddlstore.Text == "")
        {
            lblmsg.Text = "Store Error";
            lblmsg.Visible = true;
            return;
        }

        //get stk, rate

        LibraryDAL.ErpDataSetTableAdapters.InMa_Stk_CtlTableAdapter stk = new LibraryDAL.ErpDataSetTableAdapters.InMa_Stk_CtlTableAdapter();
        ErpDataSet.InMa_Stk_CtlDataTable dtstk=new ErpDataSet.InMa_Stk_CtlDataTable();


        string store_code = ddlstore.SelectedValue.ToString();


        decimal rate, freestk, totamnt;
        dtstk = stk.GetDataByItemStore(icode, store_code);

        if (dtstk.Rows.Count == 0)
        {
            //lblmsg.Text = "Item Not Found for this Store";
            //lblmsg.Visible = true;
            //return;
            rate = 0;
            freestk = 0;
            totamnt = 0;
            
        }
        else
        {
            if(dtstk[0].Stk_Ctl_Cur_Stk!=0)
                rate = dtstk[0].Stk_Ctl_Ave_Val /(decimal) dtstk[0].Stk_Ctl_Cur_Stk;
            else
                rate=0;

            freestk = (decimal)dtstk[0].Stk_Ctl_Free_Stk;
            totamnt = (decimal)10000000;// chk_budget(icode, qty, rate);
        }



        if (totamnt == -1)
        {
            //lblmsg.Text = "Budget Error for this Item";
            //lblmsg.Visible = true;
            //return;
            //temporary stopped
            totamnt = 0;
        }

        LibraryDAL.ErpDataSetTableAdapters.InMa_Itm_DetTableAdapter itm = new LibraryDAL.ErpDataSetTableAdapters.InMa_Itm_DetTableAdapter();
        ErpDataSet.InMa_Itm_DetRow dritm;
        LibraryDAL.ErpDataSetTableAdapters.InMa_Str_LocTableAdapter store = new LibraryDAL.ErpDataSetTableAdapters.InMa_Str_LocTableAdapter();
        ErpDataSet.InMa_Str_LocRow drstore;

        dritm = itm.GetItemByCode(icode)[0];
        idesc = dritm.Itm_Det_desc.ToString();
        uom = dritm.Itm_Det_PUSA_unit.ToString();
        drstore = store.GetDataByCode(store_code)[0];


        sdesc = drstore.Str_Loc_Name;
        

        if (Session[clsStatic.sessionTempDatatable] != null)
        {
            dt = (DataTable)Session[clsStatic.sessionTempDatatable];
        }
        else
        {
            dt.Rows.Clear();
            dt.Columns.Clear();

            //dt.Columns.Add("Sl", typeof(int));
            dt.Columns.Add("icode", typeof(string));
            dt.Columns.Add("idesc", typeof(string));
            dt.Columns.Add("uom", typeof(string));
            dt.Columns.Add("free", typeof(string));
            dt.Columns.Add("qty", typeof(string));
            dt.Columns.Add("scode", typeof(string));
            dt.Columns.Add("sdesc", typeof(string));
            dt.Columns.Add("amnt", typeof(decimal));
            dt.Columns.Add("irate", typeof(decimal));
            dt.Columns.Add("spe", typeof(string));
            dt.Columns.Add("brand", typeof(string));
            dt.Columns.Add("origin", typeof(string));
            dt.Columns.Add("packing", typeof(string));
            dt.Columns.Add("etr", typeof(string));
            dt.Columns.Add("noe", typeof(string));
            dt.Columns.Add("loc", typeof(string));
            dt.Columns.Add("remarks", typeof(string));

           
        }

        dt.Rows.Add(icode, idesc, uom, freestk.ToString("N2"), txtqty.Text, store_code, sdesc, totamnt, rate, txtspecification.Text, txtbrand.Text, txtorigin.Text, txtpacking.Text, cldetr.SelectedDate.ToShortDateString(),ddlnoe.Text,txtlocation.Text, txtremarks.Text);

       
        txtitem.Text = "";
        txtqty.Text = "";
        txtspecification.Text = "";
        txtbrand.Text = "";
        txtorigin.Text = "";
        txtpacking.Text = "";
        ddlnoe.Text = "";
        txtlocation.Text = "";
        txtremarks.Text = "";
        ddlstore.SelectedIndex = -1;

        Session[clsStatic.sessionTempDatatable] = new DataTable();
        Session[clsStatic.sessionTempDatatable] = dt;
        set_grid();

        //updsub.Update();
        
    }
    protected void gdItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        int indx = gdItem.SelectedIndex;

         if (indx != -1)
         {
             DataTable dt = new DataTable();

             dt = (DataTable)Session[clsStatic.sessionTempDatatable];
             dt.Rows.RemoveAt(indx);
             Session[clsStatic.sessionTempDatatable] = dt;
             set_grid();
         }
    }

    private bool check_data()
    {
        bool flg = true;
        DataTable dt = new DataTable();
        dt = (DataTable)Session[clsStatic.sessionTempDatatable];
            
        if (dt.Rows.Count < 1) flg = false;

        return flg;

    }


    private string get_flow_tepmate(string req_type, string cash_type, decimal totval)
    {
        App_Flow_DefinitionTableAdapter app = new App_Flow_DefinitionTableAdapter();
        SCBLDataSet.App_Flow_DefinitionDataTable dt = new SCBLDataSet.App_Flow_DefinitionDataTable();

        string tem_id = "";
        dt = app.GetTemplate("MPR", req_type, cash_type, totval);

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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        bool isok = true;
        string sid, sname, msub, mbody;
        string deptcode = ddldept.SelectedValue.ToString();
        string deptdet = ddldept.SelectedItem.Text.Split(':')[1];
        clsEmailReceiver[] rec_det = new clsEmailReceiver[20];
        int remailcnt=0;
        string pend_for = "";
        dsLinkoffice.tblUserInfoRow  udr;
        LibraryDAL.dsLinkofficeTableAdapters.tblUserInfoTableAdapter usrdal = new LibraryDAL.dsLinkofficeTableAdapters.tblUserInfoTableAdapter();
        User_Role_DefinitionTableAdapter roleta = new User_Role_DefinitionTableAdapter();
        SCBLDataSet.User_Role_DefinitionDataTable roledt = new SCBLDataSet.User_Role_DefinitionDataTable();
        tbl_CommentsTableAdapter com = new tbl_CommentsTableAdapter();
        
        clsStatic.CheckUserAuthentication();
        //Thread.Sleep(4000);
        if (check_data() == false) return;

        udr = usrdal.GetUserByCode(current.UserId.ToString())[0];

        DataTable dt = new DataTable();
        dt = (DataTable)Session[clsStatic.sessionTempDatatable];
        
        string cur_session;        
        int curyear = DateTime.Now.Year;

        if (DateTime.Now.Month > 6) cur_session = curyear.ToString() + "/" + (curyear + 1).ToString();
        else cur_session = (curyear - 1).ToString() + "/" + curyear.ToString();

        int rcnt;

        string selval = "CTMPR";
        string app_flow;
        decimal totanmt,lrate,lamnt;
        decimal gtotanmt=0;

        PuTr_IN_Hdr_ScblTableAdapter hdr = new PuTr_IN_Hdr_ScblTableAdapter();
        PuTr_IN_Det_ScblTableAdapter det = new PuTr_IN_Det_ScblTableAdapter();
        

        InSu_Trn_SetTableAdapter scfset = new InSu_Trn_SetTableAdapter();

        string last_num = scfset.GetDataByType_Code("IN", selval)[0].Trn_Set_Iq_Next_No.ToString();
        string ref_no = "CTPR" + string.Format("{0:00}", Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2, 2))) + string.Format("{0:00}", DateTime.Now.Month) + "-" + last_num;

        string next_num = string.Format("{0:00000}", Convert.ToInt32(last_num) + 1);

        
        SqlTransaction myTrn = HelperTA.OpenTransaction(hdr.Connection);
        


        try
        {
            hdr.AttachTransaction(myTrn);
            det.AttachTransaction(myTrn);
            com.AttachTransaction(myTrn);
          

            scfset.AttachTransaction(myTrn);
                         
            rcnt=0;
            foreach (DataRow dr in dt.Rows)
            {
                rcnt= rcnt + 1;

                lrate=Convert.ToDecimal(dr["irate"].ToString());
                lamnt = lrate * Convert.ToDecimal(dr["qty"]);

                det.InsertPutr_In_Det("IN", selval, ref_no, (short)rcnt, "", 0, dr["icode"].ToString().Trim(), dr["idesc"].ToString().Trim(), dr["uom"].ToString().Trim(), dr["scode"].ToString().Trim(), "", "", Convert.ToDateTime(dr["etr"].ToString()), Convert.ToDouble(dr["qty"]), 0, Convert.ToDouble(dr["qty"]), 0, 0, "O", "N", lrate, lamnt, lamnt, "", "", "", "", "", "", 0, dr["remarks"].ToString(), "", 0, "RUN", "", "", "", "", "", "", dr["spe"].ToString(), dr["brand"].ToString(), dr["origin"].ToString(), dr["packing"].ToString(), dr["noe"].ToString(), dr["loc"].ToString());

                //update budget
                totanmt = Convert.ToDecimal(dr["amnt"]);               
                gtotanmt += lamnt;
            }

            
            app_flow = get_flow_tepmate(selval, "", gtotanmt);
            

            pend_for = get_pend_for(app_flow);

            if ((app_flow == "") || (pend_for == "")) { isok = false; }

            hdr.InsertMprHdr("IN", selval, ref_no, "RUN", pend_for, app_flow, deptcode, "", "", DateTime.Now, DateTime.Now, "", "", "", txtcomm.Text, deptdet, "", "", "", "", "", gtotanmt, "H", "", current.UserId.ToString(), "", "", "", "", "", 0);

            com.InsertComments(ref_no, 1, DateTime.Now, current.UserId.ToString(), current.UserName.ToString(), udr.UserDesignation, 1, "MPRINI", "INI", "(INITIATOR)", "", "");

            scfset.UpdateNextNum(next_num, "IN", selval);

            if (isok)
            {
                myTrn.Commit();
                
            }
            else
            {
                myTrn.Rollback();
                
            }
            Session[clsStatic.sessionTempDatatable] = null;
            
        }
        catch
        {
            myTrn.Rollback();
            
            isok = false;
        }
        finally
        {
            HelperTA.CloseTransaction(hdr.Connection, myTrn);
            
        }
        lblporef.Text = ref_no;

        if (isok)
        {
            //send mail
            
            sid = udr.UserEmail.ToString();
            sname = udr.UserName.ToString();
            
            msub = "A purchase requisition submitted [" + ref_no + "]";
            mbody = "\n\n " + "A purchase requisition submitted [" + ref_no + "]";
            mbody += "\n " + "To view details please login in at http://203.76.114.131/cml/cmlcom ";
            mbody += "\n " + "**THIS IS AUTO GENERATED EMAIL AND DONT REQUIRE A REPLY.**";

            roledt = roleta.GetDataByRole(pend_for);

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

            clsStatic.SendMail(sid, sname, rec_det, msub, mbody);
            ModalPopupExtender5.Show();    
        }

        


    }
    protected void btnok_Click(object sender, EventArgs e)
    {
        Response.Redirect("./frm_com_inbox.aspx");    
    }
    

    protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session[clsStatic.sessionTempDatatable] = null;
        set_grid();
     
    }
    
}
