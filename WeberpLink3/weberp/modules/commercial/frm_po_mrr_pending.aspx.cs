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

public partial class frm_po_mrr_pending : System.Web.UI.Page
{
   

    protected void Page_Load(object sender, EventArgs e)
    {

       // current.UserId = "MON";
        //current.UserName = "MONJU";  
 

        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor ="FFFFFFF";
      
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

        udt = uda.GetDataByUserCodeRole(ucode, "POREP");


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
                    lst.Value = dr.trn_code.Substring(0, 2).ToString();
                    lst.Text = dr.trn_code.Substring(0, 2).ToString();
                    ddlplantlist.Items.Add(lst);
                    goto nextsrc;
                }
            }
        nextsrc: ;
        }

    }


    protected void btnview_Click(object sender, EventArgs e)
    {


        if (ddlplantlist.SelectedItem.ToString() == "")
        {
            GdHeader.Visible = false;
            return;
        }
        GdHeader.Visible = true;

        string status_det = "";

        DateTime frdate = cldfrom.SelectedDate;
        DateTime todate = cldto.SelectedDate.AddDays(1);

        PuTr_PO_Hdr_ScblTableAdapter pohdr = new PuTr_PO_Hdr_ScblTableAdapter();
        SCBLDataSet.PuTr_PO_Hdr_ScblDataTable dthdr = new SCBLDataSet.PuTr_PO_Hdr_ScblDataTable();

        App_Type_DetTableAdapter appdet = new App_Type_DetTableAdapter();

        DataTable dt = new DataTable();

        dt.Columns.Clear();        
        dt.Columns.Add("DETAIL", typeof(string));
        dt.Columns.Add("POREF", typeof(string));
        dt.Columns.Add("DATE", typeof(string));
        dt.Columns.Add("PARTY", typeof(string));
        dt.Columns.Add("EMPLOYEE", typeof(string));
        dt.Columns.Add("AMOUNT", typeof(decimal));
        dt.Columns.Add("STATUS", typeof(string));

        dthdr = pohdr.GetDataByDate(ddlplantlist.SelectedItem.ToString(), frdate, todate);

        foreach (SCBLDataSet.PuTr_PO_Hdr_ScblRow dr in dthdr.Rows)
        {
            switch (dr.PO_Hdr_Status)
            {
                case "APP":
                    status_det = "PO CREATED";
                    break;

                case "RUN":
                    status_det = "PO PENDING FOR " + appdet.GetDataByAppName(dr.PO_Hdr_Pending)[0].app_desc.ToString();

                    break;

                    
                case "ADV":
                    status_det = "SPO CREATED, REALISATION PENDING";
                    break;


                case "ADRUN":
                    status_det = "PO REALISATION PENDING FOR " + appdet.GetDataByAppName(dr.PO_Hdr_Pending)[0].app_desc.ToString();
                    //
                    break;

                case "CLOSING":
                    status_det = "PO CLOSING PENDING FOR " + appdet.GetDataByAppName(dr.PO_Hdr_Pending)[0].app_desc.ToString();
                    //
                    break;

                case "CLOSED":
                    status_det = "PO CLOSED";
                    break;


                case "CANCELING":
                    status_det = "PO CANCEL PENDING FOR " + appdet.GetDataByAppName(dr.PO_Hdr_Pending)[0].app_desc.ToString();
                    //
                    break;


                case "CANCELED":
                    status_det = "PO CANCELED";
                    break;


                case "REVISING":
                    status_det = "PO REVISE PENDING FOR " + appdet.GetDataByAppName(dr.PO_Hdr_Pending)[0].app_desc.ToString();
                    //
                    break;

                case "REJ":
                    status_det = "REJECTED ";
                    break;

            }

            dt.Rows.Add("", dr.PO_Hdr_Ref, dr.PO_Hdr_DATE.ToShortDateString(), dr.PO_Hdr_Com1, dr.PO_Hdr_Com3, Convert.ToDecimal(dr.PO_Hdr_Value.ToString("N2")), status_det);


        }

        if (dt.Rows.Count > 0) { btnexport.Visible = true; } else { btnexport.Visible = false; }
        ViewState[clsStatic.ViewStateDataTable] = dt;
        GdHeader.DataSource = dt;
        GdHeader.DataBind();
               

        DataTable dt2 = new DataTable();
        string poref;
        PuTr_PO_Det_ScblTableAdapter podet = new PuTr_PO_Det_ScblTableAdapter();
        SCBLDataSet.PuTr_PO_Det_ScblDataTable dtpodet = new SCBLDataSet.PuTr_PO_Det_ScblDataTable();
        GridView gv;
        bool full = true, none = true;


        foreach (GridViewRow gr in GdHeader.Rows)
        {
            poref = gr.Cells[1].Text;

            gv = new GridView();
            gv = (GridView)gr.Cells[1].FindControl("GridView1");

            dt2 = new DataTable();
            dt2.Columns.Clear();
            dt2.Columns.Add("ICODE", typeof(string));
            dt2.Columns.Add("ITEM", typeof(string));
            dt2.Columns.Add("UOM", typeof(string));
            dt2.Columns.Add("RATE", typeof(string));
            dt2.Columns.Add("PO QTY", typeof(string));
            dt2.Columns.Add("MRR QTY", typeof(string));
            dt2.Columns.Add("PENDING QTY", typeof(string));

            dtpodet = new SCBLDataSet.PuTr_PO_Det_ScblDataTable();
            dtpodet = podet.GetDetByRef(poref);

            full = true;
            none = true;

            foreach (SCBLDataSet.PuTr_PO_Det_ScblRow dtr in dtpodet.Rows)
            {
                if (dtr.PO_Det_Bal_Qty != 0) 
                {
                    none = false; 
                }
                
                if (dtr.PO_Det_Bal_Qty != dtr.PO_Det_Lin_Qty)
                {
                    full = false;
                }
                

                dt2.Rows.Add(dtr.PO_Det_Icode, dtr.PO_Det_Itm_Desc, dtr.PO_Det_Itm_Uom, dtr.PO_Det_Lin_Rat.ToString("N2"), dtr.PO_Det_Lin_Qty.ToString("N2"), dtr.PO_Det_Org_QTY.ToString("N2"), dtr.PO_Det_Bal_Qty.ToString("N2"));
            }

            switch (rdotype.SelectedIndex)
            {
                case 0:

                    if (!full) { gr.Visible = false; }

                    break;
                case 1:
                    if (full || none) { gr.Visible = false; }
                    break;
                case 2:
                    if (!none) { gr.Visible = false; }
                    break;
                default:
                    
                    break;

            }

            gv.DataSource = dt2;
            gv.DataBind();

        }
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
            btn = new HtmlInputButton();

            ref_no = e.Row.Cells[1].Text;
            itm_code = e.Row.Cells[3].Text;

            btn.Attributes.Add("onclick ", "window.open('./frm_po_detailview.aspx?po_ref_no=" + ref_no + "')");
            btn.ID = "btnnewwindow" + e.Row.ClientID;
            btn.Value = "View";
            btn.Attributes.Add("class", "btn2");
            e.Row.Cells[0].Controls.Add(btn);
        }

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
        //    e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
        //    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GdHeader, "Select$" + e.Row.RowIndex);

        //}

    }
    protected void btnexport_Click(object sender, EventArgs e)
    {
        clsStatic.Export("Po_Status.xls", GdHeader);
    }
}

