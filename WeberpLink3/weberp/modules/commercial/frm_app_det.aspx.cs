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

public partial class frm_app_det : System.Web.UI.Page
{
   

    protected void Page_Load(object sender, EventArgs e)
    {
       
        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";

        clsStatic.MsgConfirmBox(btnadd, "Are you sure to add/edit ?");
        clsStatic.MsgConfirmBox(btnremove, "Are you sure to remove ?");

        if (!Page.IsPostBack)
        {
            load_all_app();
        }
        else
        {

        }          
    }

    private void load_all_app()
    {
        App_Type_DetTableAdapter app = new App_Type_DetTableAdapter();
        SCBLDataSet.App_Type_DetDataTable dt = new SCBLDataSet.App_Type_DetDataTable();
                
        DataTable dtgrid = new DataTable();
        
        dt = app.GetAllApp();

        dtgrid.Rows.Clear();
        dtgrid.Columns.Clear();

        dtgrid.Columns.Add("APP ID", typeof(string));
        dtgrid.Columns.Add("APP TYPE", typeof(string));
        dtgrid.Columns.Add("APP NAME", typeof(string));
        dtgrid.Columns.Add("APP DETAILS", typeof(string));
        

        foreach (SCBLDataSet.App_Type_DetRow dr in dt.Rows)
        {
            dtgrid.Rows.Add(dr.app_id, dr.app_type, dr.app_name, dr.app_desc);
        }

        gdapp.DataSource = dtgrid;
        gdapp.DataBind();       
        
       

    }

    protected void gdapp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gdapp, "Select$" + e.Row.RowIndex);
            
        }


    }

    protected void gdapp_SelectedIndexChanged(object sender, EventArgs e)
    {
        int indx = gdapp.SelectedIndex;

        if (indx != -1)
        {

            if (gdapp.Rows[indx].Cells[0].Text.Trim() == "&nbsp;")
                txtid.Text = "";
            else
                txtid.Text = gdapp.Rows[indx].Cells[0].Text.Trim();

            if (gdapp.Rows[indx].Cells[1].Text.Trim() == "&nbsp;")
                txttype.Text = "";
            else
                txttype.Text = gdapp.Rows[indx].Cells[1].Text.Trim();

            if (gdapp.Rows[indx].Cells[2].Text.Trim() == "&nbsp;")
                txtname.Text = "";
            else
                txtname.Text = gdapp.Rows[indx].Cells[2].Text.Trim();

            if (gdapp.Rows[indx].Cells[3].Text.Trim() == "&nbsp;")
                txtdesc.Text = "";
            else
                txtdesc.Text = gdapp.Rows[indx].Cells[3].Text.Trim();
        }
    }

    private void clear_all()
    {
        txtdesc.Text = "";
        txtid.Text = "";
        txtname.Text = "";
        txttype.Text = "";
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        App_Type_DetTableAdapter app = new App_Type_DetTableAdapter();

        if (txtid.Text=="") return;
        if (txtname.Text == "") return;

        if (app.GetDataById(txtid.Text).Rows.Count > 0)
        {
            app.UpdateByApp(txttype.Text, txtname.Text, txtdesc.Text, txtid.Text);
        }
        else
        {
            app.InsertApp(txtid.Text,txttype.Text,txtname.Text,txtdesc.Text);
        }
        
        load_all_app();
        clear_all();
    }
    protected void btnremove_Click(object sender, EventArgs e)
    {
        App_Type_DetTableAdapter app = new App_Type_DetTableAdapter();

        if (txtid.Text == "") return;

        app.DeleteByApp(txtid.Text);         

        load_all_app();
        clear_all();
    }
}
