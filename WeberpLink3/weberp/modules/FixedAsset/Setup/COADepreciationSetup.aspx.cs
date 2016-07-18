using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class modules_FixedAsset_Setup_COADepreciationSetup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        StaticData.checkUserAuthentication();

        if (!IsPostBack)
        {
            Loaddata();
            loadgriddata();
        }
    }

    private void Loaddata()
    {
        dsFixedAssetTableAdapters.FAS_DepreciationMethodSetupTableAdapter dp = new dsFixedAssetTableAdapters.FAS_DepreciationMethodSetupTableAdapter();
        dsFixedAsset.FAS_DepreciationMethodSetupDataTable ddp = new dsFixedAsset.FAS_DepreciationMethodSetupDataTable();

        ddp = dp.GetData();
         
        ListItem ll;
        ddlmethod.Items.Clear();
        if (ddp.Rows.Count > 0)
        {
            foreach (dsFixedAsset.FAS_DepreciationMethodSetupRow dr in ddp.Rows)
            {
                ll = new ListItem();
                ll.Text = dr.DepreciationMethod;
                ll.Value = dr.DepreciationMethodID.ToString();
                ddlmethod.Items.Add(ll);
            }
        }


        dsFixedAssetTableAdapters.GetDataFxdTableAdapter gbb = new dsFixedAssetTableAdapters.GetDataFxdTableAdapter();
        dsFixedAsset.GetDataFxdDataTable  dgt = new dsFixedAsset.GetDataFxdDataTable();

        dgt = gbb.GetData();  

        ListItem lt;
        ddlitem.Items.Clear();   
       // ddlmethod.Items.Clear();
        if (dgt.Rows.Count > 0)
        {
            foreach (dsFixedAsset.GetDataFxdRow dr in dgt.Rows)
            {
                lt = new ListItem();
                lt.Text = dr.Ccg_Name;
                lt.Value = dr.Coa_Grp_Code;
                ddlitem.Items.Add(lt);
            }
        }


    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        dsFixedAssetTableAdapters.FAS_DepreciationSetupTableAdapter dp = new dsFixedAssetTableAdapters.FAS_DepreciationSetupTableAdapter();
        dsFixedAsset.FAS_DepreciationSetupDataTable ddp = new dsFixedAsset.FAS_DepreciationSetupDataTable();

        ddp = dp.GetDataDuplicate(ddlitem.SelectedItem.Text);

        if (ddp.Rows.Count > 0)
        {
            lblmessage.Visible = true;
            lblmessage.Text = "This Item has Already Exist";
            return;
        }


        int maxid = Convert.ToInt32(dp.GetMaxID());
        try
        {
            dp.InsertQuery(maxid, ddlitem.SelectedItem.Text,"", Convert.ToInt32(ddlmethod.SelectedItem.Value),
                Convert.ToDecimal(txtdpreciationrate.Text), Session[StaticData.sessionUserId].ToString(), DateTime.Now.Date,
                Session[StaticData.sessionUserId].ToString(), DateTime.Now.Date);
        }

        catch (Exception ex)
        {
            //MessageBox.Show("ERROR :" + ex.Message);
            lblmessage.Text = "Insert Data failed";
            return;
        }

        lblmessage.Visible = true;
        lblmessage.Text = "Insert Data Success";

        loadgriddata();
    }

    private void loadgriddata()
    {
        DataTable dt = new DataTable();

      //  dt.Columns.Add("Item ID", typeof(int));
        dt.Columns.Add("Item Group Name", typeof(string));
        dt.Columns.Add("Depreciation Rate", typeof(decimal));
        dt.Columns.Add("Depreciation Method", typeof(string));


        dsFixedAssetTableAdapters.FAS_DepreciationSetupTableAdapter vd = new dsFixedAssetTableAdapters.FAS_DepreciationSetupTableAdapter();
        dsFixedAsset.FAS_DepreciationSetupDataTable dvd = new dsFixedAsset.FAS_DepreciationSetupDataTable();
        dvd = vd.GetData();

        if (dvd.Rows.Count > 0)
        {
            foreach (dsFixedAsset.FAS_DepreciationSetupRow  dr in dvd.Rows)
            {
                dt.Rows.Add(dr["ItemGroupID"], Convert.ToDecimal(dr["DepreciationRate"]).ToString("N2"), dr["DepreciationMethodID"]);
            }
        }

        GridView1.DataSource = dt;
        GridView1.DataBind();  
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string a=e.Row.Cells[2].Text ;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (a == "1")
            {
                e.Row.Cells[2].Text = "Straight Line";
            }
            else
            {
                e.Row.Cells[2].Text = "Reducing Balance";
            }
        }
    }
}
