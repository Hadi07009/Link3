using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_FixedAsset_Setup_frmUOM : System.Web.UI.Page
{
    string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                LoadGridData();
            }
            catch (Exception)
            {

                //throw;
            }
        }
    }
    private void LoadGridData()
    {
        try
        {
            var dtGridData = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetData());
            gvUom.DataSource = null;
            gvUom.DataBind();
            if(dtGridData.Rows.Count > 0)
            {
                gvUom.DataSource = dtGridData;
                gvUom.DataBind();
            }
        }
        catch (Exception)
        {

            //throw;
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtUomCode.Text = "";
        txtUomName.Text = "";
        txtUomDecPlaces.Text = "";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            var dtUom = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.CheckDuplicateData(txtUomCode.Text.Trim()));
                
            if (dtUom.Rows.Count > 0)
            {
                DataProcess.UpdateQuery(_connectionString, SqlgenerateForFixedAsset.UpdateUOM(txtUomName.Text.Trim(), DateTime.Now, "", "", "",
                                Convert.ToInt32(txtUomDecPlaces.Text.Trim()), txtUomCode.Text.Trim()));
                
            }
            else
            {
                DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertUOM(txtUomCode.Text.Trim(), txtUomName.Text.Trim(), DateTime.Now, "", "", "",
                                Convert.ToInt32(txtUomDecPlaces.Text)));
                
            }
            txtUomCode.Text = "";
            txtUomName.Text = "";
            txtUomDecPlaces.Text = "";
            LoadGridData();
        }
        catch (Exception)
        {

            //throw;
        }
    }
    protected void gvUom_SelectedIndexChanged(object sender, EventArgs e)
    {
        int indx = gvUom.SelectedIndex;

        if (indx != -1)
        {
            try
            {
                txtUomCode.Text = gvUom.Rows[indx].Cells[0].Text.Trim() == "&nbsp;"
                                      ? ""
                                      : gvUom.Rows[indx].Cells[0].Text.Trim();
                txtUomName.Text = gvUom.Rows[indx].Cells[1].Text.Trim() == "&nbsp;"
                                         ? ""
                                         : gvUom.Rows[indx].Cells[1].Text.Trim();
                txtUomDecPlaces.Text = gvUom.Rows[indx].Cells[2].Text.Trim() == "&nbsp;"
                                         ? ""
                                         : gvUom.Rows[indx].Cells[2].Text.Trim();
            }
            catch (Exception)
            {

                //throw;
            }
        }
    }
    protected void gvUom_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(gvUom, "Select$" + e.Row.RowIndex);
        }
    }
}