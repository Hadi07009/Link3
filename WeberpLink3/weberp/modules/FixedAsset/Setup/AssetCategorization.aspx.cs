using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_FixedAsset_Setup_AssetCategorization : System.Web.UI.Page
{
    string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    TreeNode tns = null;

    protected void Page_Load(object sender, EventArgs e)
    {

        clsStatic.MsgConfirmBox(btnSave, "Are you sure to create new item?");
        clsStatic.MsgConfirmBox(btnUpdate, "Are you sure to update information?");

        if(!IsPostBack)
        {
            PopulateRootLevel();
            PanelForParent.Visible = false;            
            PanelForSub.Visible = false;
            PanelForItemCreate.Visible = false;
            btnUpdate.Visible = false;
            PanelForCategory.Visible = false;
                        
        }       
        
    }

    private void PopulateRootLevel()
    {
        TreeViewAssetCategory.Nodes.Clear();
        var myConnection = new SqlConnection(_connectionString);
        var command = new SqlCommand("SELECT Grp_Code,Grp_Code_Name," +
        "childnodecount = (SELECT Count(*) FROM InMa_Grp_Code WHERE T_c2 = D.Grp_Code) " +
        "FROM InMa_Grp_Code D WHERE T_c2 = '0'", myConnection);
        var da = new SqlDataAdapter(command);
        var dt = new DataTable();
        da.Fill(dt);

        PopulateNodes(dt, TreeViewAssetCategory.Nodes);

        TreeViewAssetCategory.CollapseAll();

        //TreeViewAssetCategory.ExpandAll();

        
    }

    private void PopulateNodes(DataTable dt, TreeNodeCollection nodes)
    {
        foreach (DataRow dr in dt.Rows)
        {
            var tn = new TreeNode(dr["Grp_Code_Name"].ToString(), dr["Grp_Code"].ToString());
            nodes.Add(tn);
            tn.PopulateOnDemand = (int.Parse(dr["childnodecount"].ToString()) > 0);
        }
    }

    private void PopulateSubLevel(int parentid, TreeNode parentNode)
    {
        var myConnection = new SqlConnection(_connectionString);
        var objCommand = new SqlCommand(@"select Grp_Code,Grp_Code_Name,(select count(*) FROM InMa_Grp_Code WHERE T_c2=e.Grp_Code) childnodecount FROM InMa_Grp_Code e where T_c2=@T_c2 order by Grp_Code_Name", myConnection);
        objCommand.Parameters.Add("@T_c2", SqlDbType.Int).Value = parentid;
        var da = new SqlDataAdapter(objCommand);
        var dt = new DataTable();
        da.Fill(dt);
        PopulateNodes(dt, parentNode.ChildNodes);              
    }
    private string CheckAllValidation(string IsItem)
    {
        string checkValidation = "";
        string IsFa="N";
        //if((cboItemType.SelectedItem.Value=="1")||(cboItemType.SelectedItem.Value=="2"))
        //    IsFa="Y";
               
        if (txtAssetName.Text == string.Empty)
        {
            txtAssetName.Focus();
            checkValidation="Please Enter Item Name";
        }
        if (IsItem.Equals("Y") == true && cboStkUnit.Text.Equals("") == true)
        {
            checkValidation = checkValidation == "" ? "Please Enter Stock Unit" : "," + "Stock Unit";
        }
        if (IsItem.Equals("Y") == true && txtAccCode.Text.Equals("") == true)
        {
            checkValidation = checkValidation == "" ? "Please Enter Account Code" : "," + "Account Code";
        }
        if (IsItem.Equals("Y")==true && cboItemType.Text.Equals("") == true)
        {
            checkValidation = checkValidation == "" ? "Please Enter Item Type" : "," + "Item Type";
        }
        //if (IsItem.Equals("Y") == true && IsFa.Equals("Y") == true && txtFaAcc.Text.Equals("") == true)
        //{
        //    checkValidation = checkValidation == "" ? "" : "," + "Fixed Asset Account Code";
        //}
        //if (IsItem.Equals("Y") == true && IsFa.Equals("Y") == true && txtDepAcc.Text.Equals("") == true)
        //{
        //    checkValidation = checkValidation == "" ? "" : "," + "Depreciation Account Code";
        //}
        //if (IsItem.Equals("Y") == true && IsFa.Equals("Y") == true && txtAcmDepAcc.Text.Equals("") == true)
        //{
        //    checkValidation = checkValidation == "" ? "" : "," + "Accumulated Depreciation Account Code";
        //}

        return checkValidation;
    }
    
    private string SaveAssetCategory()
    {
        AssetCategorization objAssetCategorization = new AssetCategorization();
        objAssetCategorization.ParentAssetCode = lblParentValue.Text == string.Empty ? "0" : lblParentValue.Text;
        objAssetCategorization.AssetName = txtAssetName.Text;
        objAssetCategorization.CheckSub = CheckBoxSub.Checked == true ? "SUB" : "";
        objAssetCategorization.CheckItemCreate = CheckBoxItemCreate.Checked == true ? "Y" : "";
        objAssetCategorization.Itm_Det_stk_unit = cboStkUnit.SelectedValue.ToString();
        objAssetCategorization.Itm_Det_Acc_code = txtAccCode.Text == string.Empty ? null : txtAccCode.Text;
        objAssetCategorization.ItemTypeId = cboItemType.SelectedValue == "" ? 0 : Convert.ToInt32(cboItemType.SelectedValue);
        objAssetCategorization.Itm_Det_Others1_flag = chkSerial.Checked == true ? "Y" : "N";
        objAssetCategorization.FaAcc = txtFaAcc.Text == string.Empty ? null : txtFaAcc.Text;
        objAssetCategorization.DepAcc = txtDepAcc.Text == string.Empty ? null : txtDepAcc.Text;
        objAssetCategorization.AcmDepAcc = txtAcmDepAcc.Text == string.Empty ? null : txtAcmDepAcc.Text;
        objAssetCategorization.DispAcc = txtDispAcc.Text == string.Empty ? null : txtDispAcc.Text;
        objAssetCategorization.RevAcc = txtRevAcc.Text == string.Empty ? null : txtRevAcc.Text;
        objAssetCategorization.ModelNumber = txtModelNumber.Text == string.Empty ? null : txtModelNumber.Text;
        objAssetCategorization.LifeCycle = txtLifeCycle.Text == string.Empty ? 0 : Convert.ToSingle(txtLifeCycle.Text);
        objAssetCategorization.CogsAcc = txtCogsAcc.Text == string.Empty ? null : txtCogsAcc.Text;
        objAssetCategorization.ExpenseAcc = txtExpenseAcc.Text == string.Empty ? null : txtExpenseAcc.Text;
        string _msg;
        try
        {
            var dtAssetName = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.SqlCheckAssetName(objAssetCategorization.AssetName));
            if (dtAssetName.Rows.Count == 0)
            {
                var storedProcedureComandTest = "exec [spItemCategorizationInitiate_InMa_Grp_Code] '" + objAssetCategorization.ParentAssetCode + "'"
                    + ",'" + objAssetCategorization.AssetName + "','" + objAssetCategorization.CheckSub + "','" + objAssetCategorization.CheckItemCreate + "'"
                    + " ,'" + objAssetCategorization.Itm_Det_stk_unit + "','" + objAssetCategorization.Itm_Det_Acc_code + "'," + objAssetCategorization.ItemTypeId + ""
                    + " ,'" + objAssetCategorization.Itm_Det_Others1_flag + "'"
                    + " ,'" + objAssetCategorization.FaAcc + "'"
                    + " ,'" + objAssetCategorization.DepAcc + "'"
                    + " ,'" + objAssetCategorization.AcmDepAcc + "'"
                    + " ,'" + objAssetCategorization.DispAcc + "'"
                    + " ,'" + objAssetCategorization.RevAcc + "'"
                    + " ,'" + objAssetCategorization.ModelNumber + "'"
                    + " ," + objAssetCategorization.LifeCycle + ""
                    + " ,'" + objAssetCategorization.CogsAcc + "'"
                    + " ,'" + objAssetCategorization.ExpenseAcc + "'"
                    ;
                StoredProcedureExecutor.StoredProcedureExecuteNonQuery(_connectionString, storedProcedureComandTest);                
                _msg = "Data Saved Successfully ";
                TreeViewAssetCategory.Nodes.Clear();
                PopulateRootLevel();
                ClearAllControl();
            }
            else 
            {
                _msg = "This asset name already Exist !";
            }
        }
        catch (SqlException sqlError)
        {
            _msg = "Error Occured During Operation into Database, Data did not Save into Database !";
        }
        catch (Exception inSystemExep)
        {
            _msg = " Error Occured, Data did not Save into Database !";
        }
        return _msg;
    }

    private string UpdateAssetCategory()
    {
        AssetCategorization objAssetCategorization = new AssetCategorization();
        objAssetCategorization.ParentAssetCode = lblParentValue.Text == string.Empty ? "0" : lblParentValue.Text;
        objAssetCategorization.AssetName = lblParentText.Text;
        if (txtAssetName.Text != string.Empty)
        {
            objAssetCategorization.AssetName = txtAssetName.Text;
        }
        objAssetCategorization.CheckItemCreate = CheckBoxItemCreate.Checked == true ? "Y" : "";
        objAssetCategorization.Itm_Det_stk_unit = cboStkUnit.SelectedValue.ToString();
        objAssetCategorization.Itm_Det_Acc_code = txtAccCode.Text == string.Empty ? "" : txtAccCode.Text;
        objAssetCategorization.ItemTypeId = cboItemType.SelectedValue == "" ? 0 : Convert.ToInt32(cboItemType.SelectedValue);
        objAssetCategorization.Itm_Det_Others1_flag = chkSerial.Checked == true ? "Y" : "N";
        objAssetCategorization.FaAcc = txtFaAcc.Text == string.Empty ? "" : txtFaAcc.Text;
        objAssetCategorization.DepAcc = txtDepAcc.Text == string.Empty ? "" : txtDepAcc.Text;
        objAssetCategorization.AcmDepAcc = txtAcmDepAcc.Text == string.Empty ? "" : txtAcmDepAcc.Text;
        objAssetCategorization.DispAcc = txtDispAcc.Text == string.Empty ? "" : txtDispAcc.Text;
        objAssetCategorization.RevAcc = txtRevAcc.Text == string.Empty ? "" : txtRevAcc.Text;
        objAssetCategorization.ModelNumber = txtModelNumber.Text == string.Empty ? "" : txtModelNumber.Text;
        objAssetCategorization.LifeCycle = txtLifeCycle.Text == string.Empty ? 0 : Convert.ToSingle(txtLifeCycle.Text);
        objAssetCategorization.CogsAcc = txtCogsAcc.Text == string.Empty ? "" : txtCogsAcc.Text;
        objAssetCategorization.ExpenseAcc = txtExpenseAcc.Text == string.Empty ? "" : txtExpenseAcc.Text;
        string _msg;
        try
        {
            var storedProcedureComandTest = "exec [spItemCategorizationUpdate] '" + objAssetCategorization.ParentAssetCode + "'"
                    + ",'" + objAssetCategorization.AssetName + "','" + objAssetCategorization.CheckItemCreate + "'"
                    + " ,'" + objAssetCategorization.Itm_Det_stk_unit + "','" + objAssetCategorization.Itm_Det_Acc_code + "'," + objAssetCategorization.ItemTypeId + ""
                    + " ,'" + objAssetCategorization.Itm_Det_Others1_flag + "'"
                    + " ,'" + objAssetCategorization.FaAcc + "'"
                    + " ,'" + objAssetCategorization.DepAcc + "'"
                    + " ,'" + objAssetCategorization.AcmDepAcc + "'"
                    + " ,'" + objAssetCategorization.DispAcc + "'"
                    + " ,'" + objAssetCategorization.RevAcc + "'"
                    + " ,'" + objAssetCategorization.ModelNumber + "'"
                    + " ," + objAssetCategorization.LifeCycle + ""
                    + " ,'" + objAssetCategorization.CogsAcc + "'"
                    + " ,'" + objAssetCategorization.ExpenseAcc + "'"
                    ;
                StoredProcedureExecutor.StoredProcedureExecuteNonQuery(_connectionString, storedProcedureComandTest);
                _msg = "Data Saved Successfully ";
             //   TreeViewAssetCategory.Nodes.Clear();
                PopulateRootLevel();                             
                
                ClearAllControl();
            
        }
        catch (SqlException sqlError)
        {
            _msg = "Error Occured During Operation into Database, Data did not Save into Database !";
        }
        catch (Exception inSystemExep)
        {
            _msg = " Error Occured, Data did not Save into Database !";
        }
        return _msg;
    }

    private string ItemCreate(string itemCode, string itemName)
    {
        string _msg;
        try
        {
            DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertItem(itemCode, "", itemName, "", "",
                                  "", "", cboStkUnit.SelectedValue, 0, "0", "0",
                                  "", 0, "", "N", DateTime.Now, DateTime.Now,
                                  "",
                                  txtAccCode.Text.Trim(), "", "", "", "", "", "", "", chkSerial.Checked ? "Y" : "N", "",
                                  "", "", "", "", "Y", 0, "A" ,
                                  Convert.ToInt32(cboItemType.SelectedValue)));
            _msg = "Data Saved Successfully ";

        }
        catch (Exception)
        {

            _msg = " Error Occured, ( ASSET CATEGORIZATION ) is ok, but item did not create !";
        }
        return _msg;
    }

    private void ClearAllControl()
    {
        txtAssetName.Text = string.Empty;
        lblParentValue.Text = string.Empty;
        lblParentText.Text = string.Empty;
        PanelForParent.Visible = false;        
        PanelForSub.Visible = false;
        cboStkUnit.Items.Clear();
        cboItemType.Items.Clear();
        txtAccCode.Text = string.Empty;
        PanelForItemCreate.Visible = false;
        CheckBoxItemCreate.Checked = false;

        txtFaAcc.Text = string.Empty;
        txtDepAcc.Text = string.Empty;
        txtAcmDepAcc.Text = string.Empty;
        txtDispAcc.Text = string.Empty;
        txtRevAcc.Text = string.Empty;
        txtModelNumber.Text = string.Empty;
        txtLifeCycle.Text = string.Empty;
        txtCogsAcc.Text = string.Empty;
        txtExpenseAcc.Text = string.Empty;
        btnUpdate.Visible = false;
        PanelForCategory.Visible = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string IsItem = CheckBoxItemCreate.Checked == true ? "Y" : "N";
        
        string validationMsg = CheckAllValidation(IsItem);

        switch (validationMsg)
        {
            case "":
                {

                    string msg = SaveAssetCategory();
                    ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "MessageBox",
                        "alert(' " + msg + "');",
                        true);

                }
                break;
            default:
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + validationMsg + "');",
                    true);
                break;
        }
    }
    protected void TreeViewAssetCategory_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        PopulateSubLevel(Int32.Parse(e.Node.Value), e.Node);
    }
    protected void TreeViewAssetCategory_SelectedNodeChanged(object sender, EventArgs e)
    {
        ClearAllControl();     
        lblParentValue.Text = TreeViewAssetCategory.SelectedNode.Value;
        lblParentText.Text = TreeViewAssetCategory.SelectedNode.Text;
        txtAssetName.Text = TreeViewAssetCategory.SelectedNode.Text;

        txtAssetName.Focus();
        PanelForParent.Visible = true;        
        PanelForSub.Visible = true;
        btnUpdate.Visible = true;
        var storedProcedureComandTest = "exec [spItemCategorizationShowInformation] '" + lblParentValue.Text + "'";
        DataTable dtItemInformation = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureComandTest);
        if (dtItemInformation.Rows.Count > 0)
        {
            CheckBoxItemCreate.Checked = true;
            PanelForItemCreate.Visible = true;
            ClsDropDownListController.LoadDropDownList(_connectionString, SqlgenerateForFixedAsset.GetData(), cboStkUnit, "Uom_Name", "Uom_Code");
            cboStkUnit.Items.RemoveAt(0);
            cboStkUnit.Items.Insert(0, new ListItem("", ""));
            ClsDropDownListController.LoadDropDownList(_connectionString, SqlgenerateForFixedAsset.GetItemType(), cboItemType, "ItemTypeName", "ItemTypeID");
            cboItemType.Items.RemoveAt(0);
            cboItemType.Items.Insert(0, new ListItem("", ""));
            cboStkUnit.SelectedValue = dtItemInformation.Rows[0][0].ToString() == null ? "" : dtItemInformation.Rows[0][0].ToString();
            txtAccCode.Text = dtItemInformation.Rows[0][1].ToString() == null ? string.Empty : dtItemInformation.Rows[0][1].ToString();
            cboItemType.SelectedValue = dtItemInformation.Rows[0][2].ToString() == "0" ? "" : dtItemInformation.Rows[0][2].ToString();
            if (dtItemInformation.Rows[0][3].ToString() == "Y")
            {
                chkSerial.Checked = true; 
            }
            else
            {
                chkSerial.Checked = false;    
            }

            txtFaAcc.Text = dtItemInformation.Rows[0][4].ToString() == null ? string.Empty : dtItemInformation.Rows[0][4].ToString();
            txtDepAcc.Text = dtItemInformation.Rows[0][5].ToString() == null ? string.Empty : dtItemInformation.Rows[0][5].ToString();
            txtAcmDepAcc.Text = dtItemInformation.Rows[0][6].ToString() == null ? string.Empty : dtItemInformation.Rows[0][6].ToString();
            txtDispAcc.Text = dtItemInformation.Rows[0][7].ToString() == null ? string.Empty : dtItemInformation.Rows[0][7].ToString();
            txtRevAcc.Text = dtItemInformation.Rows[0][8].ToString() == null ? string.Empty : dtItemInformation.Rows[0][8].ToString();
            txtModelNumber.Text = dtItemInformation.Rows[0][9].ToString() == null ? string.Empty : dtItemInformation.Rows[0][9].ToString();
            txtLifeCycle.Text = dtItemInformation.Rows[0][10].ToString() == null ? string.Empty : dtItemInformation.Rows[0][10].ToString();
            txtCogsAcc.Text = dtItemInformation.Rows[0][11].ToString() == null ? string.Empty : dtItemInformation.Rows[0][11].ToString();
            txtExpenseAcc.Text = dtItemInformation.Rows[0][12].ToString() == null ? string.Empty : dtItemInformation.Rows[0][12].ToString();
        }

        TreeNode treeSelectedNode = TreeViewAssetCategory.SelectedNode;

        Session["indx"] = TreeViewAssetCategory.SelectedNode.Parent.Value;

        if (treeSelectedNode.Parent != null)
        {
            PanelForCategory.Visible = true;
            lblCategoryName.Text = string.Empty;
            lblCategoryName.Text = treeSelectedNode.Parent.Text;
        }
        else
        {
            PanelForCategory.Visible = false;
            lblCategoryName.Text = string.Empty;
        }
        
        TreeViewAssetCategory.SelectedNodeStyle.BackColor = System.Drawing.Color.Red;
        TreeViewAssetCategory.CollapseAll();
        treeSelectedNode.Expand();
        while (treeSelectedNode.Parent != null)
        {
            treeSelectedNode = treeSelectedNode.Parent;
            treeSelectedNode.Expand();
        }
    }


    private void ExpandSelectedTreeNode(TreeNode tn)
    {
        TreeViewAssetCategory.CollapseAll();
        tn.Expand();
        while (tn.Parent != null)
        {
            tn = tn.Parent;
            tn.Expand();
        } 
    }


    protected void CheckBoxItemCreate_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBoxItemCreate.Checked == true)
        {
            ClsDropDownListController.LoadDropDownList(_connectionString, SqlgenerateForFixedAsset.GetData(), cboStkUnit, "Uom_Name", "Uom_Code");
            cboStkUnit.Items.RemoveAt(0);
            cboStkUnit.Items.Insert(0, new ListItem("", ""));

            ClsDropDownListController.LoadDropDownList(_connectionString, SqlgenerateForFixedAsset.GetItemType(), cboItemType, "ItemTypeName", "ItemTypeID");
            cboItemType.Items.RemoveAt(0);
            cboItemType.Items.Insert(0, new ListItem("", ""));

            PanelForItemCreate.Visible = true;
        }
        else
        {
            cboStkUnit.Items.Clear();
            cboItemType.Items.Clear();
            txtAccCode.Text = string.Empty;
            PanelForItemCreate.Visible = false;
        }
    }
    protected void txtAccCode_TextChanged(object sender, EventArgs e)
    {
        if (txtAccCode.Text != string.Empty)
        {
            txtAccCode.Text = txtAccCode.Text.Split(':')[0].Trim() == "" ? "" : txtAccCode.Text.Split(':')[0].Trim();
        }
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
    protected void txtCogsAcc_TextChanged(object sender, EventArgs e)
    {
        if (txtCogsAcc.Text != string.Empty)
        {
            txtCogsAcc.Text = txtCogsAcc.Text.Split(':')[0].Trim() == "" ? "" : txtCogsAcc.Text.Split(':')[0].Trim();
        }

    }
    protected void txtExpenseAcc_TextChanged(object sender, EventArgs e)
    {
        if (txtExpenseAcc.Text != string.Empty)
        {
            txtExpenseAcc.Text = txtExpenseAcc.Text.Split(':')[0].Trim() == "" ? "" : txtExpenseAcc.Text.Split(':')[0].Trim();
        }

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string IsItem = CheckBoxItemCreate.Checked == true ? "Y" : "N";
        string validationMsg = CheckAllValidation(IsItem);
        
        switch (validationMsg)
        {
            case "":
                {

                    string msg = UpdateAssetCategory();
                    ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "MessageBox",
                        "alert(' " + msg + "');",
                        true);

                }
                break;
            default:
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + validationMsg + "');",
                    true);
                break;
        }

    }


    
}