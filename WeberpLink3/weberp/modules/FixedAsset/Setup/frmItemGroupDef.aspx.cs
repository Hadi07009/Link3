using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_FixedAsset_Setup_Default : System.Web.UI.Page
{
    string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadgridData();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        var dtItmGrpDef = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.SqlCheckDuplocateData(dlstGrp.SelectedValue));
        if (dtItmGrpDef.Rows.Count > 0)
        {
            DataProcess.UpdateQuery(_connectionString, SqlgenerateForFixedAsset.SqlUpdateGroupDef(txtGrpName.Text.Trim(), txtGrpSrtName.Text.Trim(), "", DateTime.Now, "", "", "", 0, dlstGrp.SelectedValue));
        }
        else
        {
            DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.SqlInsertGroupDef(dlstGrp.SelectedValue, txtGrpName.Text.Trim(), txtGrpSrtName.Text.Trim(), "", DateTime.Now, "", "", "", 0));
        }
        txtGrpName.Text = "";
        txtGrpSrtName.Text = "";
        LoadgridData();
    }

    private void LoadgridData()
    {
        var dtGridData = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.SqlGetGroupData());
        gvItemGrpDef.DataSource = null;
        gvItemGrpDef.DataBind();
        if (dtGridData.Rows.Count > 0)
        {
            gvItemGrpDef.DataSource = dtGridData;
            gvItemGrpDef.DataBind();
        }
    }

    protected void gvItemGrpDef_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(gvItemGrpDef, "Select$" + e.Row.RowIndex);
        }
    }

    protected void gvItemGrpDef_SelectedIndexChanged(object sender, EventArgs e)
    {
        int indx = gvItemGrpDef.SelectedIndex;
        if (indx != -1)
        {
            txtGrpName.Text = gvItemGrpDef.Rows[indx].Cells[1].Text.Trim() == "&nbsp;"
                ? ""
                : gvItemGrpDef.Rows[indx].Cells[1].Text.Trim();
            txtGrpSrtName.Text = gvItemGrpDef.Rows[indx].Cells[2].Text.Trim() == "&nbsp;"
                ? ""
                : gvItemGrpDef.Rows[indx].Cells[2].Text.Trim();
            var grpCode = gvItemGrpDef.Rows[indx].Cells[0].Text.Trim() == "&nbsp;"
                ? ""
                : gvItemGrpDef.Rows[indx].Cells[0].Text.Trim();
            dlstGrp.SelectedIndex =
                dlstGrp.Items.IndexOf(dlstGrp.Items.FindByValue(grpCode));
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtGrpName.Text = "";
        txtGrpSrtName.Text = "";
        dlstGrp.SelectedIndex = 0;
    }
}