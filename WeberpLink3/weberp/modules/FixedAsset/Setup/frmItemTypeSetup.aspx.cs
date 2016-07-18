using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_FixedAsset_Setup_frmItemTypeSetup : System.Web.UI.Page
{
    string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoaddropDown();
            Session["ItemGroupCode"] = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.SqlGetItemGroupCode());
            var dtGrpCode = (DataTable)Session["ItemGroupCode"];
            LoadGridData(dtGrpCode);
            GetmaximumCode();
            ddl1.Visible = false;
            ddl2.Visible = false;
            Label5.Visible = false;
            Label6.Visible = false;
        }
    }

    private void GetmaximumCode()
    {
        DataTable dt = new DataTable();
        string sql = "select stuff('00000',6-len(isnull(Convert(int,MAX(Grp_Code)),0)+1),20,isnull(Convert(int,MAX(Grp_Code)),0)+1) as maxno from InMa_Grp_Code";
        dt = DataProcess.GetData(_connectionString, sql);
        txtGroupCode.Text = dt.Rows[0]["maxno"].ToString();
    }

    private void LoaddropDown()
    {
        dlistItemGroup.Items.Add("");
        var dtItemGroup = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.SqlGetGroupData());
        foreach (DataRow dr in dtItemGroup.Rows)
        {
            ListItem lst = new ListItem();
            lst.Text = dr["Grp_Def_Name"].ToString();
            lst.Value = dr["Grp_Def_Id"].ToString();
            dlistItemGroup.Items.Add(lst);        
        }
 
    }

    private DataTable GetItemGroup()
    {
        var dtItemGroup = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.SqlGetGroupData());
        return dtItemGroup;
    }
    protected void gvItemGroupCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        int indx = gvItemGroupCode.SelectedIndex;

        if (indx != -1)
        {
            try
            {
                txtGroupCode.Text = gvItemGroupCode.Rows[indx].Cells[1].Text.Trim() == "&nbsp;"
                                      ? ""
                                      : gvItemGroupCode.Rows[indx].Cells[1].Text.Trim();
                txtGroupCodeName.Text = gvItemGroupCode.Rows[indx].Cells[2].Text.Trim() == "&nbsp;"
                                         ? ""
                                         : gvItemGroupCode.Rows[indx].Cells[2].Text.Trim();
                txtGroupCodeShortName.Text = gvItemGroupCode.Rows[indx].Cells[3].Text.Trim() == "&nbsp;"
                                         ? ""
                                         : gvItemGroupCode.Rows[indx].Cells[3].Text.Trim();
                var grpCode = gvItemGroupCode.Rows[indx].Cells[0].Text.Trim() == "&nbsp;"
                                         ? ""
                                         : gvItemGroupCode.Rows[indx].Cells[0].Text.Trim();
                dlistItemGroup.SelectedIndex =
                    dlistItemGroup.Items.IndexOf(dlistItemGroup.Items.FindByValue(grpCode));
            }
            catch (Exception)
            {

                //throw;
            }
        }
    }
    protected void dlistItemGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            
            Session["ItemGroupCode"] = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetItemGroupCodeByGroupID(dlistItemGroup.SelectedValue));
            var dtGrpCode = (DataTable)Session["ItemGroupCode"];
            LoadGridData(dtGrpCode);

            if (dlistItemGroup.SelectedItem.Value == "I01")
            {
                ddl1.Visible = false;
                ddl2.Visible = false;
                Label5.Visible = false;
                Label6.Visible = false;
            }
            else if (dlistItemGroup.SelectedItem.Value == "I02")
            {
                string rcode = "I01";
                string pcode = "0";
                string hints = "---Select Asset Type";
                Dropdownbyroot(rcode, pcode, hints,ddl1);
                ddl1.Visible = true; 
                ddl2.Visible=false;
                Label5.Visible = true;
                Label6.Visible = false;
            }
            else if (dlistItemGroup.SelectedItem.Value == "I03")
            {
                string rcode = "I01";
                string pcode = "0";
                string hints = "---Select Asset Type";
                Dropdownbyroot(rcode, pcode, hints, ddl1);
                ddl1.Visible = true;
                ddl2.Visible = true;
                Label5.Visible = true;
                Label6.Visible = true;
            }        


        }
        catch (Exception)
        {

            //throw;
        }
    }

    private void Dropdownbyroot(string rcode,string pcode,string hints,DropDownList ddl)
    {
        DataTable dt = new DataTable();
        string sql = "select Grp_Code as codeval,Grp_Code_Name as codetext from InMa_Grp_Code where Grp_Code_Id='" + rcode + "' and T_C2='" + pcode + "' order by T_C1";
        dt = DataProcess.GetData(_connectionString, sql);

        ddl.Items.Clear();

        ddl.Items.Add(hints);
        
        foreach (DataRow dr in dt.Rows)
        {
            ListItem lst = new ListItem();
            lst.Value = dr["codeval"].ToString();
            lst.Text = dr["codetext"].ToString();
            ddl.Items.Add(lst);
        }
 
    }


    protected void gvItemGroupCode_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvItemGroupCode.PageIndex = e.NewPageIndex;
        var dtGrpCode = (DataTable)Session["ItemGroupCode"];
        LoadGridData(dtGrpCode);
    }

    private void LoadGridData(DataTable dtGroupCode)
    {
        gvItemGroupCode.DataSource = dtGroupCode;
        gvItemGroupCode.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {            
            var dtGrpCode = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.CheckDuplicateData(dlistItemGroup.SelectedValue, txtGroupCode.Text.Trim()));
                
            if (dtGrpCode.Rows.Count > 0)
            {
                DataProcess.UpdateQuery(_connectionString, SqlgenerateForFixedAsset.UpdateItemGroupCode(txtGroupCodeName.Text.Trim(), txtGroupCodeShortName.Text.Trim(),
                                              DateTime.Now, "", "", "", 0, dlistItemGroup.SelectedValue,
                                              txtGroupCode.Text.Trim()));
                
            }
            else
            {
                //var dtRefNo = DataProcess.GetSingleValueFromtable(_connectionString, SqlgenerateForFixedAsset.GetMaxRefNo());                    
                //int nextRefNo = (dtRefNo == null || Convert.ToInt32(dtRefNo) == 0) ? 1001 : Convert.ToInt32(dtRefNo) + 1;
                


                string rootid = "";
                string codeid = "";
                string parrentid = "";
                string codename = "";
                string sequenceid = "0";
                string tfl="0";
                int tin=0;
                string shtname="";
                DateTime upd=Convert.ToDateTime(System.DateTime.Now.ToShortDateString());

                rootid = dlistItemGroup.SelectedItem.Value;
                codeid = txtGroupCode.Text;
                codename = txtGroupCodeName.Text;

                if (rootid == "I01")
                {
                    parrentid = "0"; 
                }
                else if (rootid == "I02")
                {
                    parrentid =ddl1.SelectedItem.Value;
                }
                else if (rootid == "I03")
                {
                    parrentid = ddl2.SelectedItem.Value;
                }

                string sql=SqlgenerateForFixedAsset.InsertItemGroupCode(rootid,codeid,codename,shtname,upd,sequenceid,parrentid,tfl,tin);

                DataProcess.InsertQuery(_connectionString, sql);                          
               
            }

            ClearDataAfterSaveUpdate();
            
            //LoadGridData(dlistItemGroup.SelectedItem.Value);
        }
        catch (Exception)
        {

            //throw;
        }
    }

    private void ClearDataAfterSaveUpdate()
    {
        txtGroupCodeShortName.Text = "";
        txtGroupCodeName.Text = "";
        txtGroupCode.Text = "";
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            txtGroupCode.Text = "";
            txtGroupCodeName.Text = "";
            txtGroupCodeShortName.Text = "";
            dlistItemGroup.SelectedIndex = 0;
            ddl1.SelectedIndex = 0;
            ddl2.SelectedIndex = 0;
            
            Session["ItemGroupCode"] = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.SqlGetItemGroupCode());
            var dtGrpCode = (DataTable)Session["ItemGroupCode"];
            LoadGridData(dtGrpCode);
        }
        catch (Exception)
        {

            //throw;
        }
    }
    protected void gvItemGroupCode_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(gvItemGroupCode, "Select$" + e.Row.RowIndex);
        }
    }
    protected void ddl1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dlistItemGroup.SelectedItem.Value == "I01")
        {
            ddl1.Visible = false;
            ddl2.Visible = false;
            Label5.Visible = false;
            Label6.Visible = false;
        }
        else if (dlistItemGroup.SelectedItem.Value == "I02")
        {
            ddl1.Visible = true;
            ddl2.Visible = false;
            Label5.Visible = true;
            Label6.Visible = false;
        }
        else if (dlistItemGroup.SelectedItem.Value == "I03")
        {
            ddl1.Visible = true;
            ddl2.Visible = true;
            Label5.Visible = true;
            Label6.Visible = true;
        }
                
        string rcode = "I02";
        string pcode = ddl1.SelectedItem.Value;
        string hints = "---Select Asset Category";
        Dropdownbyroot(rcode, pcode, hints, ddl2);      
        
    }
}