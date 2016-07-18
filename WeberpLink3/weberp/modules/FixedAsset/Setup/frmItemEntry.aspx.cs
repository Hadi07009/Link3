using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibraryDAL;
using System.Windows.Forms;

public partial class modules_FixedAsset_Setup_frmItemEntry : System.Web.UI.Page
{
    string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    private string itemCode = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDropDown();
        }
        txtItemSearch_AutoCompleteExtender.ContextKey = _connectionString.ToString();
    }

    private void LoadDropDown()
    {
        try
        {

            ClsDropDownListController.LoadDropDownList(_connectionString, SqlgenerateForFixedAsset.GetItemType(), cboItemType, "ItemTypeName", "ItemTypeID");
            cboItemType.Items.RemoveAt(0);
            cboItemType.Items.Insert(0, new ListItem("", ""));

            ClsDropDownListController.LoadDropDownList(_connectionString, SqlgenerateForFixedAsset.GetData(), cboStkUnit, "Uom_Name", "Uom_Code");
            cboStkUnit.Items.RemoveAt(0);
            cboStkUnit.Items.Insert(0, new ListItem("", ""));

            ClsDropDownListController.LoadDropDownList(_connectionString, SqlgenerateForFixedAsset.GetData(), cboPurUnit, "Uom_Name", "Uom_Code");
            cboPurUnit.Items.RemoveAt(0);
            cboPurUnit.Items.Insert(0, new ListItem("", ""));

            ClsDropDownListController.LoadDropDownList(_connectionString, SqlgenerateForFixedAsset.GetItemGroupCodeByGroupID("I01"), cboFirstGrp, "Grp_Code_Name", "Grp_Code");
            cboFirstGrp.Items.RemoveAt(0);
            cboFirstGrp.Items.Insert(0, new ListItem("", ""));

            ClsDropDownListController.LoadDropDownList(_connectionString, SqlgenerateForFixedAsset.GetItemGroupCodeByGroupID("I02"), cboSecondGrp, "Grp_Code_Name", "Grp_Code");
            cboSecondGrp.Items.RemoveAt(0);
            cboSecondGrp.Items.Insert(0, new ListItem("", ""));

            ClsDropDownListController.LoadDropDownList(_connectionString, SqlgenerateForFixedAsset.GetItemGroupCodeByGroupID("I03"), cboThirdGrp, "Grp_Code_Name", "Grp_Code");
            cboThirdGrp.Items.RemoveAt(0);
            cboThirdGrp.Items.Insert(0, new ListItem("", ""));

            ClsDropDownListController.LoadDropDownList(_connectionString, SqlgenerateForFixedAsset.GetItemGroupCodeByGroupID("I04"), cboFourthGrp, "Grp_Code_Name", "Grp_Code");
            cboFourthGrp.Items.RemoveAt(0);
            cboFourthGrp.Items.Insert(0, new ListItem("", ""));

            //cboBomFlag.SelectedIndex = -1;
        }
        catch (Exception)
        {

            //throw;
        }
 
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearFieldData();
    }

    private void ClearFieldData()
    {
        try
        {
            txtItemName.Text = "";
            txtItemCode.Text = "";
            txtItemSecCode.Text = "";                     
            txtMaxLevel.Text = "";
            txtReordLevel.Text = "";
            txtMinLevel.Text = "";
            txtRemarks.Text = "";

            cboStkUnit.SelectedIndex = 0;
            cboPurUnit.SelectedIndex = 0;
            cboBomFlag.SelectedIndex = 0;
            cboItemType.SelectedIndex = 0;
            cboAbcCategory.SelectedIndex = 0;
            cboFirstGrp.SelectedIndex = 0;
            cboSecondGrp.SelectedIndex = 0;
            cboThirdGrp.SelectedIndex = 0;
            cboFourthGrp.SelectedIndex = 0;
            chkSerial.Checked = false;
            chkStatus.Checked = false;

            txtFaAcc.Text = string.Empty;
            txtDepAcc.Text = string.Empty;
            txtAcmDepAcc.Text = string.Empty;
            txtDispAcc.Text = string.Empty;
            txtRevAcc.Text = string.Empty;
            txtModelNumber.Text = string.Empty;
            txtLifeCycle.Text = string.Empty;
            txtVatAcc.Text = string.Empty;
            txtTaxAcc.Text = string.Empty;
            txtAccCode.Text = string.Empty;
        }
        catch (Exception)
        {

            //throw;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string abc = txtAccCode.Text.ToString();  
        SqlConnection conn = new SqlConnection(_connectionString);
        conn.Open();
        SqlTransaction tran = conn.BeginTransaction();
        try
        {
            string itemAcc = txtItemCode.Text.Trim() == string.Empty ? null : txtItemCode.Text.Trim() ;
            string faAcc = 	txtFaAcc.Text == string.Empty ? null : txtFaAcc.Text ;	
            string depAcc = txtDepAcc.Text == string.Empty ? null : txtDepAcc.Text;		
            string acmDepAcc = txtAcmDepAcc.Text == string.Empty ? null : txtAcmDepAcc.Text; 	
            string dispAcc	= txtDispAcc.Text == string.Empty ? null : txtDispAcc.Text;
            string revAcc = txtRevAcc.Text == string.Empty ? null : txtRevAcc.Text;		
            string modelNumber = txtModelNumber.Text == string.Empty ? null : txtModelNumber.Text;
            float lifeCycle = txtLifeCycle.Text == string.Empty ? 0 : Convert.ToSingle( txtLifeCycle.Text);	
            string vatAcc = txtVatAcc.Text == string.Empty ? null : txtVatAcc.Text;
            string taxAcc = txtTaxAcc.Text == string.Empty ? null : txtTaxAcc.Text; 	
	 

            var dtItem = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetItemByCode(txtItemCode.Text.Trim()));
            if (dtItem.Rows.Count > 0)
            {
                DataProcess.UpdateQuery(_connectionString, SqlgenerateForFixedAsset.UpdateItem(txtItemSecCode.Text.Trim(), txtItemName.Text.Trim(), "", "", "",
                                  cboPurUnit.SelectedValue, cboStkUnit.SelectedValue, 0, "", "",
                                  cboBomFlag.SelectedValue, 0, "", DateTime.Now,
                                  txtRemarks.Text.Trim(), abc, cboAbcCategory.SelectedValue, "", "",
                                  "", "", "", "", chkSerial.Checked ? "Y" : "N", "", "", "", "", "", "", 0, chkStatus.Checked ? "A" : "I",
                                  Convert.ToInt32(cboItemType.SelectedValue),
                                  txtItemCode.Text.Trim()));
                var storedProcedureCommandText = "exec [FAInitiateInto_Inma_Itm_AccMapping] '" + itemAcc + "','" + faAcc + "','" + depAcc + "','" + acmDepAcc + "','" + dispAcc + "','" + revAcc + "','" + modelNumber + "'," + lifeCycle + ",'" + vatAcc + "','" + taxAcc + "','" + abc + "','" + cboSecondGrp.SelectedValue + "','I02','" + cboSecondGrp.SelectedItem.Text + "','" + cboFirstGrp.SelectedItem.Text + "','" + abc + "'";
                StoredProcedureExecutor.StoredProcedureExecuteNonQuery(_connectionString, storedProcedureCommandText);
            }
            else
            {
                DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertItem(txtItemCode.Text.Trim(), txtItemSecCode.Text.Trim(), txtItemName.Text.Trim(), "", "",
                                  "", cboPurUnit.SelectedValue, cboStkUnit.SelectedValue, 0, "0", "0",
                                  cboBomFlag.SelectedValue, 0, "", "N", DateTime.Now, DateTime.Now,
                                  txtRemarks.Text.Trim(),
                                  abc, cboAbcCategory.SelectedValue, "", "", "", "", "", "", chkSerial.Checked ? "Y" : "N", "",
                                  "", "", "", "", "Y", 0, chkStatus.Checked ? "A" : "I",
                                  Convert.ToInt32(cboItemType.SelectedValue)));
                var storedProcedureCommandText = "exec [FAInitiateInto_Inma_Itm_AccMapping] '" + itemAcc + "','" + faAcc + "','" + depAcc + "','" + acmDepAcc + "','" + dispAcc + "','" + revAcc + "','" + modelNumber + "'," + lifeCycle + ",'" + vatAcc + "','" + taxAcc + "','" + abc + "','" + cboSecondGrp.SelectedValue + "','I02','" + cboSecondGrp.SelectedItem.Text + "','" + cboFirstGrp.SelectedItem.Text + "','" + abc + "'";
                StoredProcedureExecutor.StoredProcedureExecuteNonQuery(_connectionString, storedProcedureCommandText);
            }
            DataProcess.DeleteQuery(_connectionString, SqlgenerateForFixedAsset.DeleteItemGroup(txtItemCode.Text.Trim()));
            for (int i = 1; i <= 4; i++)
            {
                if (i == 1)
                {
                    DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertItemGroup(txtItemCode.Text.Trim(), "I0" + i, cboFirstGrp.SelectedValue));
                }
                if (i == 2)
                {
                    DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertItemGroup(txtItemCode.Text.Trim(), "I0" + i, cboSecondGrp.SelectedValue));
                }
                if (i == 3)
                {
                    DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertItemGroup(txtItemCode.Text.Trim(), "I0" + i, cboThirdGrp.SelectedValue));
                }
                if (i == 4)
                {
                    DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertItemGroup(txtItemCode.Text.Trim(), "I0" + i, cboFourthGrp.SelectedValue));
                }
            }
            tran.Commit();
            ClearFieldData();
        }
        catch (Exception)
        {
            tran.Rollback();
            
        }
        finally
        {
            conn.Close();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtItemSearch.Text.ToUpper() != "ALL")
        {
            try
            {
                String[] temp = txtItemSearch.Text.Split(':');
                if (temp.Length < 2) return;
                itemCode = temp[0];
            }
            catch (Exception)
            {

                //throw;
            }
        }
        if (itemCode != "")
        {
            try
            {
                var dtItem = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetItemByCode(itemCode));
                               
                txtItemName.Text = dtItem.Rows[0]["Itm_Det_desc"].ToString();
                txtItemCode.Text = dtItem.Rows[0]["Itm_Det_Icode"].ToString();
                txtItemSecCode.Text = dtItem.Rows[0]["Itm_Det_Sec_Code"].ToString();
                string ico = dtItem.Rows[0]["Itm_Det_Acc_code"].ToString();

                //string[] abc = ddlAccCode.SelectedItem.Text.Split(':');

                string Acco = SelectAccName(ico);

                string Ocp = ico + ":" + Acco;

                txtAccCode.Text = ico;
                //foreach (ListItem li in ddlAccCode.Items)
                //{

                //    if (li.Text == Ocp.ToString()) ddlAccCode.SelectedValue = li.Value;
                //}

                txtRemarks.Text = dtItem.Rows[0]["Itm_Det_com"].ToString();

                cboStkUnit.SelectedValue = dtItem.Rows[0]["Itm_Det_stk_unit"].ToString();
                cboPurUnit.SelectedValue = dtItem.Rows[0]["Itm_Det_PUSA_unit"].ToString();
                cboBomFlag.SelectedValue = dtItem.Rows[0]["Itm_Det_BOM_flag"].ToString();

                cboItemType.SelectedValue = dtItem.Rows[0]["ItemTypeId"].ToString();
                cboAbcCategory.SelectedValue = dtItem.Rows[0]["Itm_Det_ABC"].ToString();
                if (dtItem.Rows[0]["istatus"].ToString() != null)
                {
                    chkStatus.Checked = dtItem.Rows[0]["istatus"].ToString() == "A";
                }

                if (dtItem.Rows[0]["Itm_Det_Others1_flag"].ToString() != null)
                {
                    chkSerial.Checked = dtItem.Rows[0]["Itm_Det_Others1_flag"].ToString() == "Y";
                }
                
                DataTable dtItemGrp = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetGroupCodeByItem(txtItemCode.Text.Trim()));
                if (dtItemGrp.Rows.Count > 0)
                {
                    cboFirstGrp.SelectedValue = dtItemGrp.Rows[0]["Itm_Grp_Code"].ToString();
                    cboSecondGrp.SelectedValue = dtItemGrp.Rows[1]["Itm_Grp_Code"].ToString();
                    cboThirdGrp.SelectedValue = dtItemGrp.Rows[2]["Itm_Grp_Code"].ToString();
                    cboFourthGrp.SelectedValue = dtItemGrp.Rows[3]["Itm_Grp_Code"].ToString();
                }
                DataTable dtStkCtl = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetStkCtlByItemCode(txtItemCode.Text.Trim()));
                if (dtStkCtl.Rows.Count > 0)
                {
                    txtMaxLevel.Text = dtStkCtl.Rows[0]["Stk_Ctl_Max_Stk"].ToString();
                    txtMinLevel.Text = dtStkCtl.Rows[0]["Stk_Ctl_Reord_Stk"].ToString();
                    txtReordLevel.Text = dtStkCtl.Rows[0]["Stk_Ctl_Min_Stk"].ToString();
                }
                DataTable dtItemAccMapping = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetItemAccMapping(txtItemCode.Text.Trim()));
                if (dtItemAccMapping.Rows.Count > 0)
                {
                    txtFaAcc.Text = dtItemAccMapping.Rows[0]["FaAcc"].ToString();
                    txtDepAcc.Text = dtItemAccMapping.Rows[0]["DepAcc"].ToString();
                    txtAcmDepAcc.Text = dtItemAccMapping.Rows[0]["AcmDepAcc"].ToString();
                    txtDispAcc.Text = dtItemAccMapping.Rows[0]["DispAcc"].ToString();
                    txtRevAcc.Text = dtItemAccMapping.Rows[0]["RevAcc"].ToString();
                    txtModelNumber.Text = dtItemAccMapping.Rows[0]["ModelNumber"].ToString();
                    txtLifeCycle.Text = (dtItemAccMapping.Rows[0]["LifeCycle"].ToString() == "0" ? string.Empty : dtItemAccMapping.Rows[0]["LifeCycle"].ToString());
                    txtVatAcc.Text = dtItemAccMapping.Rows[0]["VatAcc"].ToString();
                    txtTaxAcc.Text = dtItemAccMapping.Rows[0]["TaxAcc"].ToString();
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }
    }
    protected void btnItmRpt_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/modules/FixedAsset/Report/frmItemListing.aspx?rptType=I");
    }
    protected void btnItmRptGrp_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/modules/FixedAsset/Report/frmItemListing.aspx?rptType=G");
    }

    //private void LoadAccCode()
    //{
    //    SqlConnection con = new SqlConnection(_connectionString);
    //    con.Open();
    //    string query = "";
    //    try
    //    {
    //        query = @"SELECT Gl_Coa_Code,Gl_Coa_Name FROM AccCOA order by Gl_Coa_Code";                    
                                          
    //        SqlCommand cmd = new SqlCommand(query, con);
    //        SqlDataReader dr = cmd.ExecuteReader();
    //        ListItem lst;
            
    //        ddlAccCode.Items.Add("");
            
    //        while (dr.Read())
    //        {
    //            lst = new ListItem();
    //            lst.Text = dr["Gl_Coa_Code"].ToString() + ":" + dr["Gl_Coa_Name"].ToString();
    //            lst.Value = dr["Gl_Coa_Code"].ToString();
    //            ddlAccCode.Items.Add(lst);
    //        }

    //        dr.Close();          
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show("ERROR :" + ex.Message);
    //    }
    //    finally
    //    {
    //        con.Close();           
    //    }
    //}

    private string SelectAccName(string Acco)
    {
        string IcoN = "";
        SqlConnection con = new SqlConnection(_connectionString); 
        string Query = @"select * from AccCOA where Gl_Coa_Code='" + Acco + "'";
        con.Open();
        SqlCommand cmd = new SqlCommand(Query, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            IcoN = dr["Gl_Coa_Name"].ToString();
        }
        dr.Close();
        con.Close();
        con.Dispose();
        return IcoN;
    }
    protected void txtFaAcc_TextChanged(object sender, EventArgs e)
    {
        if (txtFaAcc.Text != string.Empty)
        {
            txtFaAcc.Text = txtFaAcc.Text.Split(':')[0].Trim() == "" ? "" : txtFaAcc.Text.Split(':')[0].Trim();
        }
    }
    protected void txtDepAcc_TextChanged(object sender, EventArgs e)
    {
        if (txtDepAcc.Text != string.Empty)
        {
            txtDepAcc.Text = txtDepAcc.Text.Split(':')[0].Trim() == "" ? "" : txtDepAcc.Text.Split(':')[0].Trim();
        }

    }
    protected void txtAcmDepAcc_TextChanged(object sender, EventArgs e)
    {
        if (txtAcmDepAcc.Text != string.Empty)
        {
            txtAcmDepAcc.Text = txtAcmDepAcc.Text.Split(':')[0].Trim() == "" ? "" : txtAcmDepAcc.Text.Split(':')[0].Trim();
        }

    }
    protected void txtDispAcc_TextChanged(object sender, EventArgs e)
    {
        if (txtDispAcc.Text != string.Empty)
        {
            txtDispAcc.Text = txtDispAcc.Text.Split(':')[0].Trim() == "" ? "" : txtDispAcc.Text.Split(':')[0].Trim();
        }

    }
    protected void txtRevAcc_TextChanged(object sender, EventArgs e)
    {
        if (txtRevAcc.Text != string.Empty)
        {
            txtRevAcc.Text = txtRevAcc.Text.Split(':')[0].Trim() == "" ? "" : txtRevAcc.Text.Split(':')[0].Trim();
        }

    }
    protected void txtModelNumber_TextChanged(object sender, EventArgs e)
    {
        if (txtModelNumber.Text != string.Empty)
        {
            txtModelNumber.Text = txtModelNumber.Text.Split(':')[0].Trim() == "" ? "" : txtModelNumber.Text.Split(':')[0].Trim();
        }
    }
    protected void txtLifeCycle_TextChanged(object sender, EventArgs e)
    {
        if (txtLifeCycle.Text != string.Empty)
        {
            txtLifeCycle.Text = txtLifeCycle.Text.Split(':')[0].Trim() == "" ? "" : txtLifeCycle.Text.Split(':')[0].Trim();
        }

    }
    protected void txtVatAcc_TextChanged(object sender, EventArgs e)
    {
        if (txtVatAcc.Text != string.Empty)
        {
            txtVatAcc.Text = txtVatAcc.Text.Split(':')[0].Trim() == "" ? "" : txtVatAcc.Text.Split(':')[0].Trim();
        }

    }
    protected void txtTaxAcc_TextChanged(object sender, EventArgs e)
    {
        if (txtTaxAcc.Text != string.Empty)
        {
            txtTaxAcc.Text = txtTaxAcc.Text.Split(':')[0].Trim() == "" ? "" : txtTaxAcc.Text.Split(':')[0].Trim();
        }
    }
    protected void txtAccCode_TextChanged(object sender, EventArgs e)
    {
        if (txtAccCode.Text != string.Empty)
        {
            txtAccCode.Text = txtAccCode.Text.Split(':')[0].Trim() == "" ? "" : txtAccCode.Text.Split(':')[0].Trim();
        }

    }
}
