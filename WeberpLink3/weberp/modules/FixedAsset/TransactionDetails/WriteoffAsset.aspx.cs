using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibraryDAL;


public partial class modules_FixedAsset_TransactionDetails_WriteoffAsset : System.Web.UI.Page
{
    string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadTrnType(2);
            //LoadItemRevalueationInformation();
            Session[StaticData.sessionUserId] = ""; 
        }
        txtItemSearch_AutoCompleteExtender.ContextKey = _connectionString.ToString();
    }

    private void LoadTrnType(int TypeID)
    {
        ClsDropDownListController.LoadDropDownList(_connectionString, SqlgenerateForFixedAsset.GetTrnType(TypeID), ddlTrnType, "TypeName", "TypeId");
        ddlTrnType.Items.RemoveAt(0);
        //ddlTrnType.Items.Insert(0, new ListItem("", ""));
    }

    protected void txtItemSearch_TextChanged(object sender, EventArgs e)
    {
        if (txtItemSearch.Text != string.Empty)
        {
            txtItemSearch.Text = txtItemSearch.Text.Split(':')[0].Trim() == "" ? "" : txtItemSearch.Text.Split(':')[0].Trim();
            string itemCode = txtItemSearch.Text;
            ClsDropDownListController.LoadDropDownList(_connectionString, SqlgenerateForFixedAsset.GetItemtrackingNo(itemCode), ddlTrackingNo, "trackingInfo", "trackingInfo");
        }
    }
    protected void ddlTrackingNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        string itemCode = txtItemSearch.Text;
        string trackingNo = ddlTrackingNo.SelectedValue;
        DataTable dtItemInformaton = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetItemInformationByCodeTrack(itemCode, trackingNo));
        if (dtItemInformaton.Rows.Count > 0)
        {
            lblItemName.Text = dtItemInformaton.Rows[0].ItemArray[0].ToString();
            lblItemInitialValue.Text = dtItemInformaton.Rows[0].ItemArray[1].ToString();
            lblUODValue.Text = dtItemInformaton.Rows[0].ItemArray[2].ToString();
            lblLineNo.Text = dtItemInformaton.Rows[0].ItemArray[3].ToString();

            LoadItemRevalueationInformation(trackingNo);
        }
        else
        {
            lblItemName.Text = string.Empty;
            lblItemInitialValue.Text = string.Empty;
            lblUODValue.Text = string.Empty;
            lblLineNo.Text = string.Empty;
        }
    }
    private string CheckAllValidation()
    {
        const string checkValidation = "";
        if (txtItemSearch.Text == string.Empty)
        {
            txtItemSearch.Focus();
            return "Must Enter Item Code !";
        }
        if (ddlTrackingNo.SelectedValue == "-1")
        {
            ddlTrackingNo.Focus();
            return "Please Select Tracking No Correctly !";
        }
        return checkValidation;
    }
    private void ClearAllControl()
    {
        txtItemSearch.Text = string.Empty;
        ddlTrackingNo.Items.Clear();
        lblItemName.Text = string.Empty;
        lblItemInitialValue.Text = string.Empty;
        lblUODValue.Text = string.Empty;
        ddlTrnType.SelectedValue = "";
        txtAmount.Text = string.Empty;
        popupTrnDate.SelectedDate = DateTime.Now;
        lblLineNo.Text = string.Empty;
        btnSave.Text = "Save";
        lblRefNumber.Text = string.Empty;
    }
    private void LoadItemRevalueationInformation(string trackingNo)
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [FAGetAllFrom_FA_Item_Revalueation] '" + trackingNo + "'";
            var dtItemRevalueation = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureCommandTest);
            grdItemRevalueation.DataSource = null;
            grdItemRevalueation.DataBind();
            if (dtItemRevalueation.Rows.Count > 0)
            {
                grdItemRevalueation.DataSource = dtItemRevalueation;
                grdItemRevalueation.DataBind();
            }
        }
        catch (SqlException sqlError)
        {
            _msg = " Error Occured During Load Data From Database, Data did not Loaded from Database ! ";

        }
        catch (Exception inSystemExep)
        {
            _msg = " Error Occured, Data did not Loaded from Database  !";

        }
        finally
        {
            if (_msg != null)
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + _msg + " ');",
                    true);
            }

        }
    }
    private string SaveItemRevalueationInformation()
    {
        string msg = "";
        ItemRevalueation objItemRevalueation = new ItemRevalueation();
        objItemRevalueation.TrnDate = Convert.ToDateTime(popupTrnDate.SelectedDate).ToString("dd-MMM-yyyy"); 
        objItemRevalueation.ItemCode = txtItemSearch.Text;
        objItemRevalueation.TrackingNo = ddlTrackingNo.SelectedValue;
        objItemRevalueation.LineNumber = Convert.ToInt32( lblLineNo.Text);
        objItemRevalueation.TrnType = ddlTrnType.SelectedValue;
        objItemRevalueation.TrnAmount = Convert.ToDecimal( txtAmount.Text);
        objItemRevalueation.EntryUser = Session[StaticData.sessionUserId].ToString();
        objItemRevalueation.TxtTag = btnSave.Text;
        if (objItemRevalueation.TxtTag == "Save")
        {
            msg= Save(_connectionString, objItemRevalueation);
        }
        else
        {
            msg= Update(_connectionString, objItemRevalueation);
        }

        LoadItemRevalueationInformation(objItemRevalueation.TrackingNo);

        return msg;
        
    }
    public string Save(string connectionString, ItemRevalueation objItemRevalueation)
    {
        string _msg;
        try
        {
            var dtItemRevalueation = DataProcess.GetData(connectionString, SqlgenerateForFixedAsset.CheckItemRevalueationInformation(objItemRevalueation.ItemCode, objItemRevalueation.TrackingNo, objItemRevalueation.LineNumber));
            if (dtItemRevalueation.Rows.Count == 0)
            {
                var storedProcedureComandTest = "exec [FAInitiateInto_FA_Item_Revalueation] " + 0 + "," +
                                 "'" + objItemRevalueation.TrnDate + "'," +
                                 "'" + objItemRevalueation.ItemCode + "'," +
                                 "'" + objItemRevalueation.TrackingNo + "'," +
                                 "" + objItemRevalueation.LineNumber + "," +
                                 "'" + objItemRevalueation.TrnType + "'," +
                                 "" + objItemRevalueation.TrnAmount + "," +
                                 "'" + objItemRevalueation.EntryUser + "'," +
                                 "'" + DateProcess.GetServerDate(_connectionString) + "'";
                StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);
                _msg = "Data Saved Successfully ";
                ClearAllControl();                
            }
            else
            {
                _msg = "Data Already Exist...";
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
    public string Update(string connectionString, ItemRevalueation objItemRevalueation)
    {
        string _msg;
        try
        {
            var storedProcedureComandTest = "exec [FAInitiateInto_FA_Item_Revalueation] " + Convert.ToInt32( lblRefNumber.Text) + "," +
                                 "'" + objItemRevalueation.TrnDate + "'," +
                                 "'" + objItemRevalueation.ItemCode + "'," +
                                 "'" + objItemRevalueation.TrackingNo + "'," +
                                 "" + objItemRevalueation.LineNumber + "," +
                                 "'" + objItemRevalueation.TrnType + "'," +
                                 "" + objItemRevalueation.TrnAmount + "," +
                                 "'" + objItemRevalueation.EntryUser + "'," +
                                 "'" + DateProcess.GetServerDate(_connectionString) + "'";
            StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);
            _msg = "Data Saved Successfully ";
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
    protected void btnSave_Click(object sender, EventArgs e)
    {

        string validationMsg = CheckAllValidation();
        
        switch (validationMsg)
        {
            case "":
                {
                    string msg = SaveItemRevalueationInformation();                                     
                    
                    ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "MessageBox",
                        "alert(' " + msg + "');",true);
                   
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
    protected void grdItemRevalueation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        string lblRefNo = ((Label)grdItemRevalueation.Rows[selectedIndex].FindControl("lblRefNo")).Text;
        if (e.CommandName.Equals("Select"))
        {
            string lblItemCode = ((Label)grdItemRevalueation.Rows[selectedIndex].FindControl("lblItemCode")).Text;
            string lblTrackingNumber = ((Label)grdItemRevalueation.Rows[selectedIndex].FindControl("lblTrackingNumber")).Text;
            string lblTrnType = ((Label)grdItemRevalueation.Rows[selectedIndex].FindControl("lblTrnType")).Text;
            string lblAmount = ((Label)grdItemRevalueation.Rows[selectedIndex].FindControl("lblAmount")).Text;
            string lblTrnDate = ((Label)grdItemRevalueation.Rows[selectedIndex].FindControl("lblTrnDate")).Text;
            string lblLineNumber = ((Label)grdItemRevalueation.Rows[selectedIndex].FindControl("lblLineNo")).Text;

            txtItemSearch.Text = lblItemCode;
            ClsDropDownListController.LoadDropDownList(_connectionString, SqlgenerateForFixedAsset.GetItemtrackingNo(lblItemCode), ddlTrackingNo, "trackingInfo", "trackingInfo");
            ddlTrackingNo.SelectedValue = lblTrackingNumber;
            DataTable dtItemInformaton = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetItemInformationByCodeTrack(lblItemCode, lblTrackingNumber));
            if (dtItemInformaton.Rows.Count > 0)
            {
                lblItemName.Text = dtItemInformaton.Rows[0].ItemArray[0].ToString();
                lblItemInitialValue.Text = dtItemInformaton.Rows[0].ItemArray[1].ToString();
                lblUODValue.Text = dtItemInformaton.Rows[0].ItemArray[2].ToString();
            }
            ddlTrnType.SelectedValue = lblTrnType;
            txtAmount.Text = lblAmount;
            popupTrnDate.SelectedDate = Convert.ToDateTime( lblTrnDate);
            lblLineNo.Text = lblLineNumber;
            lblRefNumber.Text = lblRefNo;
            btnSave.Text = "Update";
        }
    }
    protected void grdItemRevalueation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[4].Visible = false;
        e.Row.Cells[7].Visible = false;
        e.Row.Cells[8].Visible = false;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAllControl();
    }
}
