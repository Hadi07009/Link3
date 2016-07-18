using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibraryDAL;
using LibraryDAL.SCBL2DataSetTableAdapters;
using System.Data;
public partial class modules_commercial_usercontrols_ctl_fpi_download : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void load_all(string fpi_ref)
    {
        tbl_file_detailTableAdapter file = new tbl_file_detailTableAdapter();
        SCBL2DataSet.tbl_file_detailDataTable dtfl = new SCBL2DataSet.tbl_file_detailDataTable();
        dtfl = file.GetDataByFpiRef(fpi_ref);
        
        DataTable dt = new DataTable();

        dt.Columns.Add("FPI REF", typeof(string));
        dt.Columns.Add("FILE ID", typeof(string));
        dt.Columns.Add("CATEGORY", typeof(string));
        dt.Columns.Add("FILE NAME", typeof(string));

        foreach (SCBL2DataSet.tbl_file_detailRow dr in dtfl.Rows)
        {
            dt.Rows.Add(fpi_ref, dr.unique_id, dr.category, dr.org_file_name);
        }

        gdItem.DataSource = dt;
        gdItem.DataBind();

    }

    
   
    protected void gdItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string file_id = e.Row.Cells[2].Text;
            Button btnattachment = (Button)e.Row.FindControl("btndownload");
            btnattachment.Attributes.Add("onclick ", "window.open('./frm_pdf_viewer.aspx?docid=" + file_id + "','','titlebar = yes, addressbar = no, resizable= yes, scrollbars=yes')");
        }
    }
}