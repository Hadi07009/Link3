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

public partial class frm_mpr_routing : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        
        
        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";
        lblmsg.Visible = false;

        if (!Page.IsPostBack)
        {
            get_pending(); 
        }
        else
        {

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

        PuTr_IN_Det_ScblTableAdapter srdet = new PuTr_IN_Det_ScblTableAdapter();
        SCBLDataSet.PuTr_IN_Det_ScblDataTable dtbyreq = new SCBLDataSet.PuTr_IN_Det_ScblDataTable();
        
        string plnts = "Plants: ";
        int i, len, indx, cnt;
        string[] plant_list = get_plant("ROU");
        if (plant_list == null)
        {
            lblplants.Text = "";
            return;
        }

        len = plant_list.Length;

        for (i = 0; i < len; i++)
        {
            if (plant_list[i].ToString() != "")
                plnts = plnts + plant_list[i].ToString() + ", ";
        }

        lblplants.Text = plnts;


        dtbyreq = srdet.GetPendingByRole("ROU");
       

        cnt = dtbyreq.Rows.Count;

        for (indx = cnt; indx > 0; indx--)
        {

            for (i = 0; i < len; i++)
            {
                if (dtbyreq[indx - 1].IN_Det_Code.Substring(0, 2) == plant_list[i])
                    goto nextcheck1;

            }
           dtbyreq.RemovePuTr_IN_Det_ScblRow(dtbyreq[indx - 1]);


        nextcheck1: ;
        }


        if (dtbyreq == null) return;

        if (dtbyreq.Rows.Count < 1)
        {
            lblcount.Text = "Total Pending Items: 0";         
           
            return;
        }


        generate_detail_data(dtbyreq);

    }


    private void generate_detail_data(SCBLDataSet.PuTr_IN_Det_ScblDataTable dtdet)
    {
        
        DataTable dt = new DataTable();
        

        dt.Columns.Add("MPR", typeof(string));
        dt.Columns.Add("ICODE", typeof(string));
        dt.Columns.Add("IDET", typeof(string));
        dt.Columns.Add("QTY", typeof(double));
        dt.Columns.Add("UOM", typeof(string));
        dt.Columns.Add("RATE", typeof(decimal));
        dt.Columns.Add("AMNT", typeof(decimal));
        dt.Columns.Add("SPECIFICATION", typeof(string));
        dt.Columns.Add("BRAND", typeof(string));
        dt.Columns.Add("ORIGIN", typeof(string));
        dt.Columns.Add("PACKING", typeof(string));
        dt.Columns.Add("ETR", typeof(string));
        dt.Columns.Add("REMARKS", typeof(string));
        
              
        foreach (SCBLDataSet.PuTr_IN_Det_ScblRow dr in dtdet.Rows)
        {
            dt.Rows.Add(dr.IN_Det_Ref, dr.IN_Det_Icode, dr.IN_Det_Itm_Desc, dr.IN_Det_Lin_Qty, dr.IN_Det_Itm_Uom, Convert.ToDecimal(dr.IN_Det_Lin_Rat.ToString("N2")), Convert.ToDecimal(dr.IN_Det_Lin_Amt.ToString("N2")), dr.In_Det_Specification, dr.In_Det_Brand, dr.In_Det_Origin, dr.In_Det_Packing, clsStatic.DateTimeToStringForSorting(dr.IN_Det_Exp_Dat), dr.In_Det_Remarks.ToString());
            
        }
        lblcount.Text = "Total Pending Items: " + dt.Rows.Count.ToString();

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

        headerRow.Cells[columnIndex + 2].Controls.Add(sortImage);

    }

    protected void gdItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string confirm;
            Button btn = new Button();
            btn = (Button)e.Row.Cells[1].Controls[1];


            btn.CommandArgument = e.Row.Cells[2].Text + ":" + e.Row.Cells[3].Text + ":" + e.Row.RowIndex.ToString();
            confirm = "IETM: [" + e.Row.Cells[4].Text + "]  REF: [" + e.Row.Cells[2].Text + "]  ?";

            clsStatic.MsgConfirmBox(btn, confirm);

            e.Row.Cells[2].Wrap = false;
            e.Row.Cells[13].Wrap = false;
            e.Row.Cells[3].Wrap = false;

            //e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
            //e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
            //e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gdItem, "Select$" + e.Row.RowIndex);

        }

    }

    private bool chk_for_routing(string ref_no, string item_code)
    {
        bool flg = true;

        PuTr_IN_Det_ScblTableAdapter srdet = new PuTr_IN_Det_ScblTableAdapter();
        try
        {
            if (srdet.CheckPendingValidity(ref_no, item_code, "ROU").Rows.Count == 0) flg = false;
        }
        catch
        {
            flg = false;
        }

        return flg;
    }

    private string chk_for_return(string ref_no, string item_code)
    {
        tbl_CommentsTableAdapter comm = new tbl_CommentsTableAdapter();
        PuTr_IN_Det_ScblTableAdapter srdet = new PuTr_IN_Det_ScblTableAdapter();
        PuTr_IN_Hdr_ScblTableAdapter hdr = new PuTr_IN_Hdr_ScblTableAdapter();
        App_Type_DetTableAdapter app = new App_Type_DetTableAdapter();

        string pendfor = "";
        try
        {
            pendfor = hdr.GetDataByRef(ref_no)[0].IN_Hdr_Pending.ToString();

            if (srdet.CheckPendingValidity(ref_no, item_code, "ROU").Rows.Count == 0) return "";
            if (app.GetDataByAppName(pendfor).Rows.Count == 0)
            {
                if (comm.GetDataByRefCurStatus(pendfor, "APP").Rows.Count == 0) pendfor = "";
                else pendfor = comm.GetDataByRefCurStatus(pendfor, "APP")[0].role_as;
            }
        }
        catch
        {
            pendfor = "";
        }

        return pendfor;
    }


    protected void btnok_Click(object sender, EventArgs e)
    {

        string[] tmp = ViewState[clsStatic.ViewStateCommandArgument].ToString().Split(':');
        if (tmp.Length < 2) return;

        string mpr_no = tmp[0].ToString();
        string icode = tmp[1].ToString();
        int rindx = Convert.ToInt32(tmp[2].ToString());

        DropDownList ddl = new DropDownList();

        ddl = (DropDownList)gdItem.Rows[rindx].Cells[0].Controls[1];

        string ptype = ddl.SelectedValue.ToString();


        if (gdItem.Rows[rindx].Cells[2].Text != mpr_no) return;
        if (gdItem.Rows[rindx].Cells[3].Text != icode) return;

        PuTr_IN_Det_Scbl2TableAdapter In_Det = new PuTr_IN_Det_Scbl2TableAdapter();
        tbl_CommentsTableAdapter comm = new tbl_CommentsTableAdapter();
        LibraryDAL.dsLinkofficeTableAdapters.tblUserInfoTableAdapter usr = new LibraryDAL.dsLinkofficeTableAdapters.tblUserInfoTableAdapter();


        string userid, user_name, pendfor;
       

        userid = current.UserId.ToString();
        user_name = current.UserName.ToString();

       

        string desig = usr.GetUserByCode(userid)[0].UserDesignation.ToString();
        pendfor = chk_for_return(mpr_no, icode);

        if (pendfor == "") { lblmsg.Text = "This item will not be teturn cause of too older."; lblmsg.Visible = true; return; }
              
       
        SqlTransaction myTrn = HelperTA.OpenTransaction(In_Det.Connection);

        try
        {
            In_Det.AttachTransaction(myTrn);
            comm.AttachTransaction(myTrn);

            In_Det.UpdateFromReturn("RET", pendfor, mpr_no, icode);
            comm.InsertComments(mpr_no, 1, DateTime.Now, userid, user_name, desig, 1, "ROU", "RET", txtcomments.Text, icode, "");
           
            myTrn.Commit();
        }
        catch
        {

            myTrn.Rollback();
        }
        finally
        {
            HelperTA.CloseTransaction(In_Det.Connection, myTrn);
        }
        
        Response.Redirect(Request.Url.AbsoluteUri);

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        string[] tmp = btn.CommandArgument.Split(':');
        if (tmp.Length < 2) return;

        string mpr_no = tmp[0].ToString();
        string icode = tmp[1].ToString();        
        int rindx = Convert.ToInt32(tmp[2].ToString());
       
        DropDownList ddl = new DropDownList();

        ddl = (DropDownList)gdItem.Rows[rindx].Cells[0].Controls[1];

        string ptype = ddl.SelectedValue.ToString();
        
      
        if (gdItem.Rows[rindx].Cells[2].Text != mpr_no) return;
        if (gdItem.Rows[rindx].Cells[3].Text != icode) return;

        PuTr_IN_Det_ScblTableAdapter In_Det = new PuTr_IN_Det_ScblTableAdapter();
        

        string  userid, user_name;
       
              
        

        userid = current.UserId.ToString();
        user_name = current.UserName.ToString();


        if (chk_for_routing(mpr_no, icode) == false) return;


        if (ptype == "RETURN")
        {            

            ViewState[clsStatic.ViewStateCommandArgument] = btn.CommandArgument.ToString();
            ModalPopupExtender5.Show();
            return;
        }


       
        SqlTransaction myTrn = HelperTA.OpenTransaction(In_Det.Connection);

        try
        {
            In_Det.AttachTransaction(myTrn);
            In_Det.UpdateFromRouting("TEN", ptype, mpr_no, icode);
            
           
            myTrn.Commit();
        }
        catch
        {

            myTrn.Rollback();
        }
        finally
        {
            HelperTA.CloseTransaction(In_Det.Connection, myTrn);
        }
        
        Response.Redirect(Request.Url.AbsoluteUri);

    }

    protected void btnexport_Click(object sender, EventArgs e)
    {
        clsStatic.Export("MPRrouting.xls", gdItem);
    }
}
