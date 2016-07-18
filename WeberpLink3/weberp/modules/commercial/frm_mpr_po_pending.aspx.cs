using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.IO;
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
using LibraryDAL.SCBLQryTableAdapters;

public partial class frm_mpr_po_pending : System.Web.UI.Page
{
   

    protected void Page_Load(object sender, EventArgs e)
    {

        //current.UserId = "MON";
        //current.UserName = "MONJU";

        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";
      
        if (!Page.IsPostBack)
        {
            cldfrom.SelectedDate = Convert.ToDateTime("01/12/2009");
            cldto.SelectedDate = DateTime.Now;
            load_plant();
           
        
        }
        else
        {

        }
          
    }
    

    private void load_plant()
    {
        string ucode = current.UserId.ToString();
        User_Role_DefinitionTableAdapter uda = new User_Role_DefinitionTableAdapter();
        SCBLDataSet.User_Role_DefinitionDataTable udt = new SCBLDataSet.User_Role_DefinitionDataTable();
        tbl_trn_detTableAdapter trn = new tbl_trn_detTableAdapter();
        SCBLDataSet.tbl_trn_detDataTable dttrn = new SCBLDataSet.tbl_trn_detDataTable();

        ListItem lst;
        int i;
        string[] items;

        udt = uda.GetDataByUserCodeRole(ucode, "MPRREP");


        ddlplantlist.Items.Clear();
        ddlplantlist.Items.Add("");
        if (udt.Rows.Count == 0) return;

        items = udt[0].plant_list.Split(',');

        dttrn = trn.GetAllCodeByType("IN");

        foreach (SCBLDataSet.tbl_trn_detRow dr in dttrn.Rows)
        {
            for (i = 0; i < items.Length; i++)
            {
                if (dr.trn_code.Substring(0, 2) == items[i].ToString())
                {
                    lst = new ListItem();
                    lst.Value = dr.trn_code.ToString();
                    lst.Text = dr.trn_code.ToString();
                    ddlplantlist.Items.Add(lst);
                    goto nextsrc;
                }
            }
        nextsrc: ;
        }

    }


    protected void btnview_Click(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(2000);
       
        if (ddlplantlist.SelectedItem.ToString() == "")
        {
            GdHeader.Visible = false;
            return;
        }
        GdHeader.Visible = true;

        DateTime frdate = cldfrom.SelectedDate;
        DateTime todate = cldto.SelectedDate.AddDays(1);

        decimal mprqty, poqty, insqty, mrrqty, pendqty;

        DtMprPendingTableAdapter indet = new DtMprPendingTableAdapter();
        SCBLQry.DtMprPendingDataTable dtdet = new SCBLQry.DtMprPendingDataTable();
       
        DataTable dt = new DataTable();

        dt.Columns.Clear();
        dt.Columns.Add("VIEW", typeof(string));
        dt.Columns.Add("MPR", typeof(string));
        dt.Columns.Add("DATE", typeof(string));       
        dt.Columns.Add("ICODE", typeof(string));
        dt.Columns.Add("ITEM", typeof(string));
        dt.Columns.Add("UOM", typeof(string));
        dt.Columns.Add("MPR QTY", typeof(decimal));        
        dt.Columns.Add("PO QTY", typeof(decimal));        
        dt.Columns.Add("PENDING QTY", typeof(decimal));
        dt.Columns.Add("INS QTY", typeof(decimal));
        dt.Columns.Add("MRR QTY", typeof(decimal));
        dt.Columns.Add("STATUS", typeof(string));

        dtdet = indet.GetPendingMprPo(ddlplantlist.SelectedValue.ToString(), frdate, todate);

       

        foreach (SCBLQry.DtMprPendingRow dr in dtdet.Rows)
        {
            mprqty = Convert.ToDecimal(dr.IN_Det_Lin_Qty.ToString("N2"));
            if (dr.IsPO_Det_Lin_QtyNull()) { poqty = 0; } else { poqty = Convert.ToDecimal(dr.PO_Det_Lin_Qty.ToString("N2")); }
            if (dr.IsPO_Det_Ins_QTYNull()) { insqty = 0; } else { insqty = Convert.ToDecimal(dr.PO_Det_Ins_QTY.ToString("N2")); }
            if (dr.IsPO_Det_Org_QTYNull()) { mrrqty = 0; } else { mrrqty = Convert.ToDecimal(dr.PO_Det_Org_QTY.ToString("N2")); }
            pendqty = mprqty - poqty;

            //dt.Rows.Add("",dr.IN_Det_Ref, dr.IN_Hdr_St_DATE.ToShortDateString(), dr.IN_Det_Icode, dr.IN_Det_Itm_Desc, dr.IN_Det_Itm_Uom, mprqty, poqty, insqty, mrrqty, pendqty, dr.IN_Det_Exp_Dat, dr.IN_Hdr_Com5, dr.In_Det_Status);

            switch (rdotype.SelectedIndex)
            {
                case 0:
                    if(mprqty == pendqty)
                        dt.Rows.Add("", dr.IN_Det_Ref, dr.IN_Hdr_St_DATE.ToShortDateString(), dr.IN_Det_Icode, dr.IN_Det_Itm_Desc, dr.IN_Det_Itm_Uom, mprqty, poqty, pendqty, insqty, mrrqty, dr.In_Det_Status);
                    break;

                case 1:
                    if ((pendqty > 0) && (mprqty != pendqty))
                        dt.Rows.Add("", dr.IN_Det_Ref, dr.IN_Hdr_St_DATE.ToShortDateString(), dr.IN_Det_Icode, dr.IN_Det_Itm_Desc, dr.IN_Det_Itm_Uom, mprqty, poqty, pendqty, insqty, mrrqty, dr.In_Det_Status);
                    break;

                case 2:
                    if (pendqty <= 0)
                        dt.Rows.Add("", dr.IN_Det_Ref, dr.IN_Hdr_St_DATE.ToShortDateString(), dr.IN_Det_Icode, dr.IN_Det_Itm_Desc, dr.IN_Det_Itm_Uom, mprqty, poqty, pendqty, insqty, mrrqty, dr.In_Det_Status);
                    break;

            }

            
        }


        if (dt.Rows.Count > 0) { btnexport.Visible = true; } else { btnexport.Visible = false; }       
      
        
        ViewState[clsStatic.ViewStateDataTable] = dt;
        GdHeader.DataSource = dt;
        GdHeader.DataBind();

    }
    protected void gdItem_SelectedIndexChanged(object sender, EventArgs e)
    {


    }
    protected void GdHeader_RowCommand(object sender, GridViewCommandEventArgs e)
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

            

            GdHeader.DataSource = dttemp;
            GdHeader.DataBind();



        }



    }

    protected void GdHeader_SelectedIndexChanged(object sender, EventArgs e)
    {
        int indx = GdHeader.SelectedIndex;
        string ref_no, itm_code;
       
        if (indx != -1)
        {
            ref_no = GdHeader.Rows[indx].Cells[0].Text.Trim();
            itm_code = GdHeader.Rows[indx].Cells[2].Text.Trim();
            
        }
        AddSortImage(GdHeader.HeaderRow);
    }

    protected void GdHeader_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState[clsStatic.ViewStateSortExpression] = e.SortExpression;
        AddSortImage(GdHeader.HeaderRow);
    }

    

    private void AddSortImage( GridViewRow headerRow)
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
      
        headerRow.Cells[columnIndex].Controls.Add(sortImage);

    }


    

    protected void GdHeader_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        HtmlInputButton btn;
        string ref_no;
        string itm_code;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
            //e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
            //e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GdHeader, "Select$" + e.Row.RowIndex);

            decimal mprqty = Convert.ToDecimal(e.Row.Cells[6].Text);
            decimal poqty = Convert.ToDecimal(e.Row.Cells[7].Text);
            if (poqty == 0) { e.Row.Attributes.Add("style", "background-color:#FFCCFF"); }
            else if (poqty < mprqty) { e.Row.Attributes.Add("style", "background-color:#FFFF99"); }
            else { e.Row.Attributes.Add("style", "background-color:white"); }

            btn = new HtmlInputButton();

            ref_no = e.Row.Cells[1].Text;
            itm_code = e.Row.Cells[3].Text;            

            btn.Attributes.Add("onclick ", "window.open('./frm_mpr_lifecycle.aspx?ref_no=" + ref_no + "&itm_code=" + itm_code + "')");
            btn.ID = "btnnewwindow" + e.Row.ClientID;
            btn.Value = "View";
            btn.Attributes.Add("class", "btn2");
            e.Row.Cells[0].Controls.Add(btn);
        }       

       

    }


    protected void btnexport_Click(object sender, EventArgs e)
    {
        clsStatic.Export("PendingList.xls", GdHeader);
    }

    
}


