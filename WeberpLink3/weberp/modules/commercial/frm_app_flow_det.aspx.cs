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

public partial class frm_app_flow_det : System.Web.UI.Page
{
   

    protected void Page_Load(object sender, EventArgs e)
    {
       
        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";

        clsStatic.MsgConfirmBox(btnadd, "Are you sure to add/edit ?");
        clsStatic.MsgConfirmBox(btnremove, "Are you sure to remove ?");

        if (!Page.IsPostBack)
        {
            load_all_flow();

        }
        else
        {

        }
          
    }


    private void load_all_flow()
    {
        App_Flow_HdrTableAdapter flow = new App_Flow_HdrTableAdapter();
        SCBLDataSet.App_Flow_HdrDataTable dt = new SCBLDataSet.App_Flow_HdrDataTable();
                
        DataTable dtgrid = new DataTable();

        dt = flow.GetAllFlow();

        dtgrid.Rows.Clear();
        dtgrid.Columns.Clear();

        dtgrid.Columns.Add("FLOW ID", typeof(string));       
        dtgrid.Columns.Add("FLOW DETAILS", typeof(string));
        

        foreach (SCBLDataSet.App_Flow_HdrRow dr in dt.Rows)
        {

            dtgrid.Rows.Add(dr.flow_id, dr.flow_det);
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
                txtdesc.Text = "";
            else
                txtdesc.Text = gdapp.Rows[indx].Cells[1].Text.Trim();
        }
    }

    private void clear_all()
    {
        txtdesc.Text = "";
        txtid.Text = "";
        
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        App_Flow_HdrTableAdapter flow = new App_Flow_HdrTableAdapter();

        if (txtid.Text=="") return;
        if (txtdesc.Text == "") return;

        if (flow.GetDataById(txtid.Text).Rows.Count > 0)
        {
            flow.UpdateFlow(txtdesc.Text, txtid.Text);
        }
        else
        {
            flow.Insertflow(txtid.Text, txtdesc.Text);
        }
        


        load_all_flow();
        clear_all();
    }
    protected void btnremove_Click(object sender, EventArgs e)
    {
        App_Flow_HdrTableAdapter flow = new App_Flow_HdrTableAdapter();

        if (txtid.Text == "") return;

        flow.Deleteflow(txtid.Text);        


        load_all_flow();
        clear_all();
    }
}
