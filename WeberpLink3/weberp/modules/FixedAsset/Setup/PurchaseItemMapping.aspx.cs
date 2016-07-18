using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_FixedAsset_Setup_PurchaseItemMapping : System.Web.UI.Page
{
    string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            Session[StaticData.sessionUserId] = "";
            Session["valueForUpdate"] = string.Empty;
        }
        txtSupplier_AutoCompleteExtender.ContextKey = _connectionString.ToString();
        txtItem_AutoCompleteExtender.ContextKey = _connectionString.ToString();

    }
    private void GetPurchaseItemInformation(string supplierCode)
    {
        var storedProcedureComandTest = "exec [ItemMappingGetFrom_Inma_Purchase_Item_Mapping] '" + supplierCode + "'";
        DataTable dtPurchaseItem = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureComandTest);
        grdGetMappingInformation.DataSource = null;
        grdGetMappingInformation.DataBind();
        if (dtPurchaseItem.Rows.Count > 0)
        {
            grdGetMappingInformation.DataSource = dtPurchaseItem;
            grdGetMappingInformation.DataBind();
        }
    }
    protected void txtSupplier_TextChanged(object sender, EventArgs e)
    {
        if (txtSupplier.Text != string.Empty)
        {
            txtSupplier.Text = txtSupplier.Text.Split(':')[0].Trim() == "" ? "" : txtSupplier.Text.Split(':')[0].Trim();
            GetPurchaseItemInformation(txtSupplier.Text);
        }
    }
    protected void txtItem_TextChanged(object sender, EventArgs e)
    {
        if (txtItem.Text != string.Empty)
        {
            txtItem.Text = txtItem.Text.Split(':')[0].Trim() == "" ? "" : txtItem.Text.Split(':')[0].Trim();
        }
    }
    private string CheckAllValidation()
    {
        const string checkValidation = "";
        
        if (txtSupplier.Text == string.Empty)
        {
            txtSupplier.Focus();
            return "Please Enter Supplier Correctly !";
        }
        if (txtItem.Text == string.Empty)
        {
            txtItem.Focus();
            return "Please Enter Item Correctly !";
        }
        return checkValidation;
    }
    private void ClearControl()
    {
        txtItem.Text = string.Empty;
        rblStatus.SelectedIndex = 0;
        Session["valueForUpdate"] = string.Empty;
    }
    private void ClearAllControl()
    {
        txtSupplier.Text = string.Empty;
        txtItem.Text = string.Empty;
        rblStatus.SelectedIndex = 0;
        Session["valueForUpdate"] = string.Empty;
        grdGetMappingInformation.DataSource = null;
        grdGetMappingInformation.DataBind();
    }
    public string Save(string connectionString, PurchaseItemMapping objPurchaseItemMapping)
    {
        string _msg;
        try
        {
            var dtPurchaseItem = DataProcess.GetData(connectionString, SqlgenerateForFixedAsset.SqlGetPurchaseItemMappingRecord(objPurchaseItemMapping.SupplierCode,objPurchaseItemMapping.ItemCode));
            if (dtPurchaseItem.Rows.Count == 0)
            {
                var storedProcedureComandTest = "exec [ItemMappingInto_Inma_Purchase_Item_Mapping] " + objPurchaseItemMapping.RefNo + ",'" +objPurchaseItemMapping.SupplierCode + "','" + objPurchaseItemMapping.ItemCode + "','" + objPurchaseItemMapping.ItemStatus + "','" + objPurchaseItemMapping.EntryUserId + "','" + objPurchaseItemMapping.EntryDate + "'";
                StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);
                _msg = "Data Saved Successfully ";
                GetPurchaseItemInformation(objPurchaseItemMapping.SupplierCode);
                ClearControl();
            }
            else 
            {
                _msg = "These data already exist !";
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
    public string Update(string connectionString, PurchaseItemMapping objPurchaseItemMapping)
    {
        string _msg;
        try
        {
            var storedProcedureComandTest = "exec [ItemMappingInto_Inma_Purchase_Item_Mapping] " + objPurchaseItemMapping.RefNo + ",'" + objPurchaseItemMapping.SupplierCode + "','" + objPurchaseItemMapping.ItemCode + "','" + objPurchaseItemMapping.ItemStatus + "','" + objPurchaseItemMapping.EntryUserId + "','" + objPurchaseItemMapping.EntryDate + "'";
            StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);
            _msg = "Data Saved Successfully ";
            GetPurchaseItemInformation(objPurchaseItemMapping.SupplierCode);
            ClearControl();
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
    private string SavePurchaseItem()
    {
        PurchaseItemMapping objPurchaseItemMapping = new PurchaseItemMapping();
        objPurchaseItemMapping.SupplierCode = txtSupplier.Text;
        objPurchaseItemMapping.ItemCode = txtItem.Text;
        objPurchaseItemMapping.ItemStatus = rblStatus.SelectedValue;
        objPurchaseItemMapping.EntryUserId = Session[StaticData.sessionUserId].ToString();
        objPurchaseItemMapping.EntryDate = DateProcess.GetServerDate(_connectionString).ToString();
        objPurchaseItemMapping.RefNo = Session["valueForUpdate"].ToString() == string.Empty ? 0 : Convert.ToInt32( Session["valueForUpdate"].ToString());
        if (objPurchaseItemMapping.RefNo == 0)
        {
            return Save(_connectionString, objPurchaseItemMapping);
        }
        else
        {
            return Update(_connectionString, objPurchaseItemMapping);
        }

        
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckAllValidation();
        switch (validationMsg)
        {
            case "":
                {

                    string msg = SavePurchaseItem();
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
    protected void grdGetMappingInformation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Select"))
        {
            int selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
            string lblRefNo = ((Label)grdGetMappingInformation.Rows[selectedIndex].FindControl("lblRefNo")).Text;
            string lblItemCode = ((Label)grdGetMappingInformation.Rows[selectedIndex].FindControl("lblItemCode")).Text;
            string lblStatusValue = ((Label)grdGetMappingInformation.Rows[selectedIndex].FindControl("lblStatusValue")).Text;

            Session["valueForUpdate"] = lblRefNo;
            txtItem.Text = lblItemCode;
            rblStatus.SelectedValue = lblStatusValue;
        }
    }
    protected void grdGetMappingInformation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[4].Visible = false;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAllControl();
    }
}