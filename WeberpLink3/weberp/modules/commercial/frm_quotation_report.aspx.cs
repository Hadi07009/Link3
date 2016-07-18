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
using LibraryDAL;
using LibraryDAL.SCBLDataSetTableAdapters;

public partial class frm_quotation_report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication();
       
        
        tblmaster.BgColor = "FFFFFFF";
        if (!Page.IsPostBack)
        {
            
        }

    }

    
    
    
    protected void btnview_Click(object sender, EventArgs e)
    {
        gdItem.Visible = false;
        string itm_code, party_code;
        quotation_detTableAdapter det = new quotation_detTableAdapter();
        SCBLDataSet.quotation_detDataTable dt = new SCBLDataSet.quotation_detDataTable();

        DataTable tbl = new DataTable();

        string[] tmp = txtitem.Text.Split(':');

        if (tmp.Length < 2)
        {
            itm_code = "";
        }
        else
        {
            itm_code = tmp[0];
        }

        tmp = txtpartydet.Text.Split(':');

        if (tmp.Length < 2)
        {
            party_code = "";
        }
        else
        {
            party_code = tmp[0];
        }

        if ((itm_code == "") && (party_code == "")) return;

        if ((itm_code != "") && (party_code != ""))
        {
            dt = det.GetDataByPartyItem(party_code, itm_code);
        }
        else
        {
            if (itm_code != "")
            {
                dt = det.GetDataByItem(itm_code);
            }
            else
            {
                dt = det.GetDataByParty(party_code);
            }
        }


        tbl.Columns.Add("ICODE", typeof(string));
        tbl.Columns.Add("IDET", typeof(string));
        tbl.Columns.Add("PCODE", typeof(string));
        tbl.Columns.Add("PDET", typeof(string));
        tbl.Columns.Add("QTY", typeof(double));
        tbl.Columns.Add("RATE", typeof(decimal));
        tbl.Columns.Add("AMNT", typeof(decimal));
        tbl.Columns.Add("BRAND", typeof(string));
        tbl.Columns.Add("ORIGIN", typeof(string));
        tbl.Columns.Add("PACKING", typeof(string));
        tbl.Columns.Add("DATE", typeof(DateTime));



        foreach (SCBLDataSet.quotation_detRow dr in dt.Rows)
        {
            tbl.Rows.Add(dr.product_code, dr.product_det, dr.party_code, dr.party_det, dr.qty, clsStatic.NumericSameConvertion(dr.rate), clsStatic.NumericSameConvertion((decimal)dr.qty * dr.rate), dr.product_brand, dr.origin, dr.packing, dr.quotation_date);
        }



        gdItem.Visible = true;

        gdItem.DataSource = tbl;
        gdItem.DataBind();

        ViewState[clsStatic.ViewStateDataTable] = tbl;
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

        headerRow.Cells[columnIndex].Controls.Add(sortImage);

    }
}

