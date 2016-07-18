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
using LibraryDAL.SCBL2DataSetTableAdapters;


public partial class frm_mat_trn_det : System.Web.UI.Page
{   
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication();      

        tblmaster.BgColor = "FFFFFFF";
        if (!Page.IsPostBack)
        {
            load_all_type();
            cldfrom.SelectedDate = DateTime.Now.AddDays(-2);
            cldto.SelectedDate = DateTime.Now;
            load_recent_data("ALL");

        }
        else
        {
        }        
    }

    private void load_all_type()
    {
        ListItem lst;

        rdoOpt.Items.Clear();


        lst = new ListItem();
        lst.Text = "ALL";
        lst.Value = "ALL";
        lst.Selected = true;
        rdoOpt.Items.Add(lst);

        //lst = new ListItem();
        //lst.Text = "DIRECT";
        //lst.Value = "DIRECT";
        //rdoOpt.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "INSPECTION";
        lst.Value = "INSPECTION";
        rdoOpt.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "OK";
        lst.Value = "OK";
        rdoOpt.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "RETURN";
        lst.Value = "RETURN";
        rdoOpt.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "CONFIRM";
        lst.Value = "CONFIRM";
        rdoOpt.Items.Add(lst);

        

    }

    private void load_recent_data(string type)
    {
        DataTable dtgrid = new DataTable();
        DataRow dtr;
        tbl_mat_rec_retTableAdapter det = new tbl_mat_rec_retTableAdapter();        
        SCBL2DataSet.tbl_mat_rec_retDataTable dt = new SCBL2DataSet.tbl_mat_rec_retDataTable();
        SCBL2DataSet.tbl_mat_rec_retRow dr;
        DateTime frdate = cldfrom.SelectedDate;
        DateTime todate = cldto.SelectedDate.AddDays(1);


        if (type == "ALL")
            dt = det.GetDataByDate(frdate, todate);
        else
            dt = det.GetDataByDateType(frdate, todate, type);

        

        dtgrid.Rows.Clear();
        dtgrid.Columns.Clear();
        dtgrid.Columns.Add("TRN REF", typeof(string));
        dtgrid.Columns.Add("PO REF", typeof(string));
        dtgrid.Columns.Add("TRN TYPE", typeof(string));
        dtgrid.Columns.Add("PARTY CODE", typeof(string));
        dtgrid.Columns.Add("PARTY DET", typeof(string));       

        foreach (SCBL2DataSet.tbl_mat_rec_retRow dr2 in dt.Rows)
        {
            dr = null;
            dr = det.GetDataByTrnRef(dr2.trn_ref_no)[0];
            
            dtr = dtgrid.NewRow();
            dtr[0] = dr.trn_ref_no;
            dtr[1] = dr.po_ref;
            dtr[2] = dr.trn_type;
            dtr[3] = dr.pcode;
            dtr[4] = dr.pdet;            
            dtgrid.Rows.Add(dr.trn_ref_no, dr.po_ref, dr.trn_type, dr.pcode, dr.pdet);
        }

        gdUser.DataSource = dtgrid;        
        gdUser.DataBind();

    }

    protected void gdUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        tbl_mat_rec_retTableAdapter det = new tbl_mat_rec_retTableAdapter();

        string comm;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            comm = det.GetDataByTrnRef(e.Row.Cells[0].Text)[0].comments;            

            e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gdUser, "Select$" + e.Row.RowIndex);
            if (comm.Trim()!="") 
                e.Row.Attributes.Add("Title", comm);
        }


    }

    protected void gdUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        int indx = gdUser.SelectedIndex;

        if (indx != -1)
        {

            if (gdUser.Rows[indx].Cells[0].Text.Trim() != "&nbsp;")
            {
                Session[clsStatic.sessionQueryString] = gdUser.Rows[indx].Cells[0].Text.Trim();
                RegisterStartupScript("click", "<script>window.open('./frm_mat_trn_print.aspx');</script>");
            }
                
        }
    }
    protected void btnshows_Click(object sender, EventArgs e)
    {       

        load_recent_data(rdoOpt.SelectedValue.ToString());
    }
}
