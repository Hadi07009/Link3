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

public partial class frm_tac_det : System.Web.UI.Page
{
   

    protected void Page_Load(object sender, EventArgs e)
    {
        
        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";

        clsStatic.MsgConfirmBox(btnadd, "Are you sure to add/edit ?");
        clsStatic.MsgConfirmBox(btndel, "Are you sure to remove ?");

        if (!Page.IsPostBack)
        {
            load_all_tac();

        }
        else
        {

        }
          
    }

    private void load_all_tac()
    {
        tbl_tac_detTableAdapter tac = new tbl_tac_detTableAdapter();
        SCBLDataSet.tbl_tac_detDataTable dt = new SCBLDataSet.tbl_tac_detDataTable();

        DataTable dtgrid = new DataTable();

        dt = tac.GetAllTac();

        dtgrid.Rows.Clear();
        dtgrid.Columns.Clear();

        dtgrid.Columns.Add("TAC ID", typeof(string));
        dtgrid.Columns.Add("TYPE", typeof(string));
        dtgrid.Columns.Add("SEQ", typeof(string));
        dtgrid.Columns.Add("CAT", typeof(string));
        dtgrid.Columns.Add("DETAILS", typeof(string));


        foreach (SCBLDataSet.tbl_tac_detRow dr in dt.Rows)
        {

            dtgrid.Rows.Add(dr.tac_id, dr.tac_type, dr.tac_seq_no, dr.tac_sel_cat,dr.tac_det);
        }

        gdtac.DataSource = dtgrid;
        gdtac.DataBind();


        foreach (GridViewRow gr in gdtac.Rows)
        {
            Label lbl=new Label();
            HtmlTableCell htc = new HtmlTableCell();
            htc.InnerHtml = gr.Cells[4].Text.ToString();
            gr.Cells[4].Text = htc.InnerText;
        }


    }

    protected void gdtac_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gdtac, "Select$" + e.Row.RowIndex);
        }


    }

    protected void gdtac_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbl_tac_detTableAdapter tac = new tbl_tac_detTableAdapter();

        int indx = gdtac.SelectedIndex;

        if (indx != -1)
        {

            if (gdtac.Rows[indx].Cells[0].Text.Trim() == "&nbsp;")
                txtid.Text = "";
            else
                txtid.Text = gdtac.Rows[indx].Cells[0].Text.Trim();

            if (gdtac.Rows[indx].Cells[1].Text.Trim() == "&nbsp;")
                ddltype.Text = "";
            else
                try
                {
                    ddltype.SelectedValue = gdtac.Rows[indx].Cells[1].Text.Trim();
                }
                catch
                {
                    ddltype.Text = "";
                }

            if (gdtac.Rows[indx].Cells[2].Text.Trim() == "&nbsp;")
                txtseq.Text = "";
            else
                txtseq.Text = gdtac.Rows[indx].Cells[2].Text.Trim();

            if (gdtac.Rows[indx].Cells[3].Text.Trim() == "&nbsp;")
                ddlcat.Text = "";
            else
                try
                {
                    ddlcat.SelectedValue = gdtac.Rows[indx].Cells[3].Text.Trim();
                }
                catch
                {
                    ddlcat.Text = "";
                }

            if (gdtac.Rows[indx].Cells[4].Text.Trim() == "&nbsp;")
                txteditor.Content = "";
            else
            {
                //txtdet.Text = gdtac.Rows[indx].Cells[4].Text;
                txteditor.Content = tac.GetDataByTacId(txtid.Text)[0].tac_det;
            }
        }
    }

    private void clear_all()
    {
        txtseq.Text = "";
        txtid.Text = "";
        ddlcat.Text = "";
        ddltype.Text = "";
        txteditor.Content = "";

    }

    
    protected void btnadd_Click(object sender, EventArgs e)
    {
        tbl_tac_detTableAdapter tac = new tbl_tac_detTableAdapter();
        int seq;
        if (txtid.Text == "") return;
        try
        {
            seq = Convert.ToInt32(txtseq.Text);
        }
        catch { return; }

        if (tac.GetDataByTacId(txtid.Text).Rows.Count > 0)
        {
            tac.UpdateById(ddltype.SelectedValue, seq, ddlcat.SelectedValue, txteditor.Content, txteditor.Content, txtid.Text);
        }
        else
        {
            tac.InsertTac(txtid.Text, ddltype.SelectedValue, seq, ddlcat.SelectedValue, txteditor.Content, txteditor.Content);
        }
        
        load_all_tac();
        clear_all();
    }
    protected void btndel_Click(object sender, EventArgs e)
    {
        tbl_tac_detTableAdapter tac = new tbl_tac_detTableAdapter();

        if (txtid.Text == "") return;

        tac.DeleteById(txtid.Text);


        load_all_tac();
        clear_all();
    }
}
