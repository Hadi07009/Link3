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

public partial class frm_po_status_hdr : System.Web.UI.Page
{
   

    protected void Page_Load(object sender, EventArgs e)
    {
        
               
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

    private void detailview(string ref_no)
    {
        

        lbldetail.Visible = true;
        gdItem.Visible = true;
              

        DateTime frdate = cldfrom.SelectedDate;
        DateTime todate = cldto.SelectedDate.AddDays(1);
        
        PuTr_PO_Det_ScblTableAdapter podet = new PuTr_PO_Det_ScblTableAdapter();
        SCBLDataSet.PuTr_PO_Det_ScblDataTable dtpodet = new SCBLDataSet.PuTr_PO_Det_ScblDataTable();

        InTr_Trn_HdrTableAdapter intrhdr = new InTr_Trn_HdrTableAdapter();
        ErpDataSet.InTr_Trn_HdrDataTable dtintrhdr = new ErpDataSet.InTr_Trn_HdrDataTable();

        tbl_mat_rec_retTableAdapter mat = new tbl_mat_rec_retTableAdapter();
        SCBL2DataSet.tbl_mat_rec_retDataTable dtmat = new SCBL2DataSet.tbl_mat_rec_retDataTable();

        string poref, item_code, trntype;
        int po_lno;
        GridView gv;

        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();

        dt.Columns.Clear();
        
        dt.Columns.Add("PO REF", typeof(string));        
        dt.Columns.Add("ICODE", typeof(string));
        dt.Columns.Add("ITEM", typeof(string));
        dt.Columns.Add("SPECIFICATION", typeof(string));
        dt.Columns.Add("BRAND", typeof(string));
        dt.Columns.Add("ORIGIN", typeof(string));
        dt.Columns.Add("PACKING", typeof(string));
        dt.Columns.Add("MPR REF", typeof(string));
        dt.Columns.Add("UOM", typeof(string));
        dt.Columns.Add("RATE", typeof(string));
        dt.Columns.Add("PO QTY", typeof(string));        
        dt.Columns.Add("REC QTY", typeof(string));        
        dt.Columns.Add("INS QTY", typeof(string));

        dtpodet = podet.GetDetByRef(ref_no);


        foreach (SCBLDataSet.PuTr_PO_Det_ScblRow dr in dtpodet.Rows)
        {

            dt.Rows.Add(dr.PO_Det_Ref + "," + dr.PO_Det_Lno.ToString(), dr.PO_Det_Icode, dr.PO_Det_Itm_Desc, dr.PO_Det_Specification, dr.PO_Det_Brand, dr.PO_Det_Origin, dr.PO_Det_Packing, dr.PO_Det_Pr_Ref, dr.PO_Det_Itm_Uom, dr.PO_Det_Lin_Rat.ToString("N2"), dr.PO_Det_Lin_Qty.ToString("N2"), dr.PO_Det_Org_QTY.ToString("N2"), dr.PO_Det_Ins_QTY.ToString("N2"));
            
        }
        
        gdItem.DataSource = dt;
        gdItem.DataBind();

        foreach (GridViewRow gr in gdItem.Rows)
        {
            poref = gr.Cells[1].Text.Split(',')[0];
            po_lno = Convert.ToInt32(gr.Cells[1].Text.Split(',')[1]);

            item_code = gr.Cells[2].Text;
            gv = new GridView();
            gv = (GridView)gr.Cells[0].FindControl("GridView1");

            dt2 = new DataTable();
            dt2.Columns.Clear();
            dt2.Columns.Add("TYPE",typeof(string));
            dt2.Columns.Add("REF NO", typeof(string));
            dt2.Columns.Add("QTY", typeof(string));
            dt2.Columns.Add("DATE TIME", typeof(DateTime));

            dtmat = new SCBL2DataSet.tbl_mat_rec_retDataTable();
            dtmat = mat.GetDataByPorefSl(poref, po_lno);

            foreach (SCBL2DataSet.tbl_mat_rec_retRow dtr in dtmat.Rows)
            {
                switch (dtr.trn_type)
                {
                    case "OK":
                        trntype = "INSPECTED";
                        break;
                    case "INSPECTION":
                        trntype = "DELIVERED";
                        break;
                    case "CONFIRM":
                        trntype = "MRR";
                        break;
                    default:
                        trntype = dtr.trn_type;
                        break;

                }

                dt2.Rows.Add(trntype,dtr.trn_ref_no, dtr.itm_rec_ret_qty.ToString("N2"), dtr.trn_datetime);
            }

            gv.DataSource = dt2;
            gv.DataBind();

        }
        
    }


    protected void btnview_Click(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(2000);
        gdItem.Visible = false;
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
        dt.Columns.Add("PO REF", typeof(string));
        dt.Columns.Add("DATE", typeof(string));
        dt.Columns.Add("PARTY", typeof(string));
        dt.Columns.Add("EMPLOYEE", typeof(string));
        dt.Columns.Add("AMOUNT", typeof(decimal));       
        dt.Columns.Add("STATUS", typeof(string));

        dthdr = pohdr.GetDataByDate(ddlplantlist.SelectedItem.ToString(), frdate, todate);

        foreach(SCBLDataSet.PuTr_PO_Hdr_ScblRow dr in dthdr.Rows)
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

            dt.Rows.Add("",dr.PO_Hdr_Ref, dr.PO_Hdr_DATE.ToShortDateString(), dr.PO_Hdr_Com1, dr.PO_Hdr_Com3, Convert.ToDecimal(dr.PO_Hdr_Value.ToString("N2")), status_det);


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
        string ref_no;
        lbldetail.Visible = false;
        gdItem.Visible = false;

        if (indx != -1)
        {
            ref_no = GdHeader.Rows[indx].Cells[1].Text.Trim();            
            detailview(ref_no);
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
            btn = new HtmlInputButton();

            ref_no = e.Row.Cells[1].Text;
            itm_code = e.Row.Cells[3].Text;          

            btn.Attributes.Add("onclick ", "window.open('./frm_po_lifecycle.aspx?ref_no=" + ref_no + "')");
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

