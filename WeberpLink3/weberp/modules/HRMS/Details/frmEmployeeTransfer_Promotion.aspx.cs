using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Details_frmEmployeeTransfer_Promotion : System.Web.UI.Page
{
    private const string Rnode = "U";
    string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        if (!Page.IsPostBack)
        {
            LoadCompanyByUserPermission("ADM", Rnode);
            PanelForEmployeeDetails.Visible = false;
            PanelForPromotion.Visible = false;
            ClsDropDownListController.EnableDisableDropDownList(ddlcompany);
            ddlcompany_SelectedIndexChanged(sender, e);
        }
    }

    private void LoadCompanyByUserPermission(string userid, string nodeid)
    {
        DataTable dt = new DataTable();
        ListItem lst;
        ddlcompany.Items.Clear();
        ddlcompany.Items.Add("");
        dt = AccessPermission.GetCompanyByUserandNodeid(ConnectionString, userid, nodeid);
        foreach (DataRow dr in dt.Rows)
        {
            lst = new ListItem();
            lst.Text = dr["COMP_CODE"].ToString() + ":" + dr["COMP_NAME"].ToString();
            lst.Value = dr["COMP_CODE"].ToString();
            ddlcompany.Items.Add(lst);
        }
    }

    protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dbname = ddlcompany.SelectedItem.Value;
        var constr = ConfigurationSettings.AppSettings["SCFConnectionString"].Replace("SCF", dbname);
        Session[GlobalData.sessionConnectionstring] = constr;
        txtEmployeeCode_AutoCompleteExtender.ContextKey = Session[GlobalData.sessionConnectionstring].ToString();
        ClsDropDownListController.LoadDropDownList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetOfficeLocationIntoDDL(), ddlOfficeLocation, "Division_Master_Name", "Division_Master_Code");
        ClsDropDownListController.LoadDropDownList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetDesignationIntoDDL(), ddlDesignation, "JobTitle", "JobCode");
    }

    private string SubmitTransferRecord()
    {

        EmployeeTransfer objEmployeeTransfer = new EmployeeTransfer();
        objEmployeeTransfer.CompanyCode = ddlcompany.SelectedValue;
        objEmployeeTransfer.EmployeeCode = txtEmployeeCode.Text;
        objEmployeeTransfer.ToOfficeLocation = ddlOfficeLocation.SelectedValue;
        objEmployeeTransfer.ToDepartment = ddlDepartmentCode.SelectedValue;
        objEmployeeTransfer.ToSection = ddlSectionCode.SelectedValue;
        objEmployeeTransfer.TransferredDate = Convert.ToDateTime(popupTransferredDate.Text).ToString("dd-MMM-yyyy");
        objEmployeeTransfer.EntryUserID = current.UserId.ToString();
        objEmployeeTransfer.ActionType = rblForActionType.SelectedValue;
        objEmployeeTransfer.TxtTag = btnSaveActionType.Text;
        if (objEmployeeTransfer.TxtTag == "Save")
        {
            objEmployeeTransfer.RowNo = 0;
            return SaveTransferRecord(Session[GlobalData.sessionConnectionstring].ToString(), objEmployeeTransfer);
        }
        else
        {
            objEmployeeTransfer.RowNo = Convert.ToInt32(lblRowNoForUpdate.Text == string.Empty ? null : lblRowNoForUpdate.Text);
            objEmployeeTransfer.FromOfficeLocation = lblOfficeLocationCode.Text;
            objEmployeeTransfer.FromDepartment = lblDepartmentCode.Text;
            objEmployeeTransfer.FromSection = lblSectionCode.Text;
            return UpdateTransferRecord(Session[GlobalData.sessionConnectionstring].ToString(), objEmployeeTransfer);
        }
    }

    private string SubmitPromotionRecord()
    {
        EmployeePromotion objEmployeePromotion = new EmployeePromotion();
        objEmployeePromotion.CompanyCode = ddlcompany.SelectedValue;
        objEmployeePromotion.EmployeeCode = txtEmployeeCode.Text;
        objEmployeePromotion.ToDesignation = ddlDesignation.SelectedValue;
        objEmployeePromotion.TransferredDate = Convert.ToDateTime(popupPromotionDate.Text).ToString("dd-MMM-yyyy");
        objEmployeePromotion.EntryUserID = current.UserId.ToString();
        objEmployeePromotion.ActionType = rblForActionType.SelectedValue;
        objEmployeePromotion.Remarks = txtRemarks.Text == string.Empty ? null : txtRemarks.Text.ToString();
        objEmployeePromotion.TxtTag = btnSaveActionType.Text;
        if (objEmployeePromotion.TxtTag == "Save")
        {
            objEmployeePromotion.RowNo = 0;
            return SavePromotionRecord(Session[GlobalData.sessionConnectionstring].ToString(), objEmployeePromotion);
        }
        else
        {
            objEmployeePromotion.RowNo = Convert.ToInt32(lblRowNoForUpdate.Text == string.Empty ? null : lblRowNoForUpdate.Text);
            objEmployeePromotion.FromDesignation = lblDesignationCode.Text;
            return UpdatePromotionRecord(Session[GlobalData.sessionConnectionstring].ToString(), objEmployeePromotion);
        }
    }

    public string SaveTransferRecord(string connectionString, EmployeeTransfer objEmployeeTransfer)
    {
        string _msg;
        try
        {
            var storedProcedureComandTest = "exec [TransferInitiateInto_HRMS_Trans_Loc] '" + objEmployeeTransfer.CompanyCode + "','" + objEmployeeTransfer.EmployeeCode + "','" + objEmployeeTransfer.ToDepartment + "','" + objEmployeeTransfer.ToSection + "','" + objEmployeeTransfer.ToOfficeLocation + "','" + objEmployeeTransfer.TransferredDate + "','" + objEmployeeTransfer.ActionType + "','" + objEmployeeTransfer.EntryUserID + "'," + objEmployeeTransfer.RowNo + "";
            StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);
            LoadAllTransfer_PromotionRecord(objEmployeeTransfer.EmployeeCode, objEmployeeTransfer.ActionType);
            ShowEmployeeDetails(objEmployeeTransfer.EmployeeCode);
            ClearAllControlForTransfer();
            _msg = "Data Saved Successfully ";
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

    public string UpdateTransferRecord(string connectionString, EmployeeTransfer objEmployeeTransfer)
    {
        string _msg;
        try
        {
            var dtCheckForUpdate = DataProcess.GetData(connectionString, Sqlgenerate.SqlGetRecordFromHrms_Trans_Det(objEmployeeTransfer.EmployeeCode, objEmployeeTransfer.RowNo));
            if (dtCheckForUpdate.Rows.Count == 1)
            {
                var storedProcedureComandTest = "exec [TransferUpdateIntoTables] '" + objEmployeeTransfer.CompanyCode + "','" + objEmployeeTransfer.EmployeeCode + "','" + objEmployeeTransfer.FromDepartment + "','" + objEmployeeTransfer.FromSection + "','" + objEmployeeTransfer.FromOfficeLocation + "','" + objEmployeeTransfer.ToDepartment + "','" + objEmployeeTransfer.ToSection + "','" + objEmployeeTransfer.ToOfficeLocation + "','" + objEmployeeTransfer.TransferredDate + "','" + objEmployeeTransfer.EntryUserID + "'," + objEmployeeTransfer.RowNo + "";
                StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);
                LoadAllTransfer_PromotionRecord(objEmployeeTransfer.EmployeeCode, objEmployeeTransfer.ActionType);
                ShowEmployeeDetails(objEmployeeTransfer.EmployeeCode);
                ClearAllControlForTransfer();
                _msg = "Data Update Successfully ";
            }
            else if (dtCheckForUpdate.Rows.Count == 0)
            {
                btnSaveActionType.Text = "Save";
                _msg = "This Data did not found ! So, Please Save Now.";
            }
            else
            {
                LoadAllTransfer_PromotionRecord(objEmployeeTransfer.EmployeeCode, objEmployeeTransfer.ActionType);
                ShowEmployeeDetails(objEmployeeTransfer.EmployeeCode);
                ClearAllControlForTransfer();
                _msg = " Please try again !";
            }
        }
        catch (SqlException sqlError)
        {
            _msg = "Error Occured During Operation into Database, Data did not Update into Database !";
        }
        catch (Exception inSystemExep)
        {
            _msg = " Error Occured, Data did not Update into Database !";
        }

        return _msg;
    }

    public string SavePromotionRecord(string connectionString, EmployeePromotion objEmployeePromotion)
    {
        string _msg;
        try
        {
            var storedProcedureComandTest = "exec [PromotionInitiateInto_HRMS_Trans_Loc] '" + objEmployeePromotion.CompanyCode + "','" + objEmployeePromotion.EmployeeCode + "','" + objEmployeePromotion.TransferredDate + "','" + objEmployeePromotion.ActionType + "','" + objEmployeePromotion.EntryUserID + "'," + objEmployeePromotion.RowNo + ",'" + objEmployeePromotion.ToDesignation + "','" + objEmployeePromotion.Remarks + "'";
            StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);
            LoadAllTransfer_PromotionRecord(objEmployeePromotion.EmployeeCode, objEmployeePromotion.ActionType);
            ShowEmployeeDetails(objEmployeePromotion.EmployeeCode);
            ClearAllControlForPromotion();
            _msg = "Data Saved Successfully ";
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

    public string UpdatePromotionRecord(string connectionString, EmployeePromotion objEmployeePromotion)
    {
        string _msg;
        try
        {
            var dtCheckForUpdate = DataProcess.GetData(connectionString, Sqlgenerate.SqlGetRecordFromHrms_Trans_Det(objEmployeePromotion.EmployeeCode, objEmployeePromotion.RowNo));
            if (dtCheckForUpdate.Rows.Count == 1)
            {
                var storedProcedureComandTest = "exec [PromotionUpdateIntoTables] '" + objEmployeePromotion.CompanyCode + "','" + objEmployeePromotion.EmployeeCode + "','" + objEmployeePromotion.FromDesignation + "','" + objEmployeePromotion.TransferredDate + "','" + objEmployeePromotion.EntryUserID + "'," + objEmployeePromotion.RowNo + ",'" + objEmployeePromotion.ToDesignation + "','" + objEmployeePromotion.Remarks + "'";
                StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);
                LoadAllTransfer_PromotionRecord(objEmployeePromotion.EmployeeCode, objEmployeePromotion.ActionType);
                ShowEmployeeDetails(objEmployeePromotion.EmployeeCode);
                ClearAllControlForPromotion();
                _msg = "Data Update Successfully ";

            }
            else if (dtCheckForUpdate.Rows.Count == 0)
            {
                btnSaveActionType.Text = "Save";
                _msg = "This Data did not found ! So, Please Save Now.";
            }
            else
            {
                LoadAllTransfer_PromotionRecord(objEmployeePromotion.EmployeeCode, objEmployeePromotion.ActionType);
                ShowEmployeeDetails(objEmployeePromotion.EmployeeCode);
                ClearAllControlForPromotion();
                _msg = " Please try again !";
            }
        }
        catch (SqlException sqlError)
        {
            _msg = "Error Occured During Operation into Database, Data did not Update into Database !";
        }
        catch (Exception inSystemExep)
        {
            _msg = " Error Occured, Data did not Update into Database !";
        }
        return _msg;
    }

    private string CheckAllValidationForTransfer()
    {
        const string checkValidation = "";
        if (ddlcompany.SelectedValue == "-1")
        {
            ddlcompany.Focus();
            return "Please Select Company Correctly !";
        }
        if (txtEmployeeCode.Text == string.Empty)
        {
            txtEmployeeCode.Focus();
            return "Please Select Employee Code From Given List Correctly !";
        }
        if (ddlOfficeLocation.SelectedValue == "-1")
        {
            ddlOfficeLocation.Focus();
            return "Please Select Office Location Correctly !";
        }
        if (ddlDepartmentCode.SelectedValue == "-1")
        {
            ddlDepartmentCode.Focus();
            return "Please Select Department Code Correctly !";
        }
        if (ddlSectionCode.SelectedValue == "-1")
        {
            ddlSectionCode.Focus();
            return "Please Select Section Code Correctly !";
        }
        return checkValidation;
    }

    private string CheckAllValidationForPromotion()
    {
        const string checkValidation = "";
        if (ddlcompany.SelectedValue == "-1")
        {
            ddlcompany.Focus();
            return "Please Select Company Correctly !";
        }
        if (txtEmployeeCode.Text == string.Empty)
        {
            txtEmployeeCode.Focus();
            return "Please Select Employee Code From Given List Correctly !";
        }
        if (ddlDesignation.SelectedValue == "-1")
        {
            ddlDesignation.Focus();
            return "Please Select Designation Correctly !";
        }
        return checkValidation;
    }

    private void ClearAllControlForTransfer()
    {
        ddlOfficeLocation.SelectedValue = "-1";
        ddlDepartmentCode.Items.Clear();
        ddlSectionCode.Items.Clear();
        popupTransferredDate.Text =DateTime.Now.ToString("dd/MM/yyyy");
        lblRowNoForUpdate.Text = string.Empty;
        btnSaveActionType.Text = "Save";
    }

    private void ClearAllControlForPromotion()
    {
        lblRowNoForUpdate.Text = string.Empty;
        ddlDesignation.SelectedValue = "-1";
        popupPromotionDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtRemarks.Text = string.Empty;
        btnSaveActionType.Text = "Save";
    }

    private void LoadDepartmentCode(string officeLocationCode)
    {
        ClsDropDownListController.LoadDropDownList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetDepartmentCodeByOfficeLocation(officeLocationCode), ddlDepartmentCode, "Dept_Name", "Dept_Code");
    }

    private void LoadSectionCode(string officeLocationCode, string departmentCode)
    {
        ClsDropDownListController.LoadDropDownList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetSectionIntoDDL(departmentCode, officeLocationCode), ddlSectionCode, "Sect_Name", "Sect_Code");
    }

    private void LoadAdvanceType()
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [AdvanceTypeGetAllFrom_HRMS_AdvanceTypeSetup] ";
            var dtAdvanceType = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandTest);
            grdEmployeeTransfer.DataSource = null;
            grdEmployeeTransfer.DataBind();
            if (dtAdvanceType.Rows.Count > 0)
            {
                grdEmployeeTransfer.DataSource = dtAdvanceType;
                grdEmployeeTransfer.DataBind();
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

    public void ActionForTransfer()
    {
        string validationMsg = CheckAllValidationForTransfer();
        switch (validationMsg)
        {
            case "":
                {

                    string msg = SubmitTransferRecord();
                    MessageBox1.ShowSuccess(msg);

                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }
    }

    public void ActionForPromotion()
    {
        string validationMsg = CheckAllValidationForPromotion();
        switch (validationMsg)
        {
            case "":
                {

                    string msg = SubmitPromotionRecord();
                    MessageBox1.ShowSuccess(msg);

                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }
    }

    protected void btnSaveActionType_Click(object sender, EventArgs e)
    {
        if (rblForActionType.SelectedValue == "T")
        {
            ActionForTransfer();
        }
        else if (rblForActionType.SelectedValue == "P")
        {
            ActionForPromotion();
        }
    }

    protected void grdEmployeeTransfer_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void grdEmployeeTransfer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        string lblCompanyCode = ((Label)grdEmployeeTransfer.Rows[selectedIndex].FindControl("lblCompanyCode")).Text;
        string lblEmployeeID = ((Label)grdEmployeeTransfer.Rows[selectedIndex].FindControl("lblEmployeeID")).Text;
        string lblToDepartment = ((Label)grdEmployeeTransfer.Rows[selectedIndex].FindControl("lblToDepartment")).Text;
        string lblToSection = ((Label)grdEmployeeTransfer.Rows[selectedIndex].FindControl("lblToSection")).Text;
        string lblToOfficeLocation = ((Label)grdEmployeeTransfer.Rows[selectedIndex].FindControl("lblToOfficeLocation")).Text;
        string lblFromDepartment = ((Label)grdEmployeeTransfer.Rows[selectedIndex].FindControl("lblFromDepartment")).Text;
        string lblFromSection = ((Label)grdEmployeeTransfer.Rows[selectedIndex].FindControl("lblFromSection")).Text;
        string lblFromOfficeLocation = ((Label)grdEmployeeTransfer.Rows[selectedIndex].FindControl("lblFromOfficeLocation")).Text;
        string lblTransDetDate = ((Label)grdEmployeeTransfer.Rows[selectedIndex].FindControl("lblTransDetDate")).Text;
        string entryUserID = current.UserId.ToString();
        int lblAutoSerial = Convert.ToInt32(((Label)grdEmployeeTransfer.Rows[selectedIndex].FindControl("lblAutoSerial")).Text);

        if (e.CommandName.Equals("Delete"))
        {
            string _msg = null;
            try
            {
                var storedProcedureCommandTest = "exec [TransferDeleteFrom_Hrms_Trans_Det] '" + lblCompanyCode + "','" + lblEmployeeID + "','" + lblFromDepartment + "','" + lblFromSection + "','" + lblFromOfficeLocation + "','" + lblTransDetDate + "','" + entryUserID + "'," + lblAutoSerial + "";
                StoredProcedureExecutor.StoredProcedureExecuteNonQuery(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandTest);
                LoadAllTransfer_PromotionRecord(txtEmployeeCode.Text, rblForActionType.SelectedValue);
            }
            catch (SqlException sqlError)
            {
                _msg = "  Error Occured During Operation into Database, Data did not Delete from Database !  ";

            }
            catch (Exception inSystemExep)
            {
                _msg = " Error Occured, Data did not Delete from Database  ! ";

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
        else if (e.CommandName.Equals("Select"))
        {
            ddlcompany.SelectedValue = lblCompanyCode;
            txtEmployeeCode.Text = lblEmployeeID;
            lblOfficeLocationCode.Text = lblFromOfficeLocation;
            lblDepartmentCode.Text = lblFromDepartment;
            lblSectionCode.Text = lblFromSection;
            ddlOfficeLocation.SelectedValue = lblToOfficeLocation;
            LoadDepartmentCode(lblToOfficeLocation);
            ddlDepartmentCode.SelectedValue = lblToDepartment;
            LoadSectionCode(lblToOfficeLocation, lblToDepartment);
            ddlSectionCode.SelectedValue = lblToSection;
            popupTransferredDate.Text = lblTransDetDate;
            lblRowNoForUpdate.Text = lblAutoSerial.ToString();
            btnSaveActionType.Text = "Update";
        }
    }

    protected void grdEmployeeTransfer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[2].Visible = false;
        e.Row.Cells[5].Visible = false;
        e.Row.Cells[7].Visible = false;
        e.Row.Cells[9].Visible = false;
        e.Row.Cells[11].Visible = false;
        e.Row.Cells[13].Visible = false;
        e.Row.Cells[15].Visible = false;
        e.Row.Cells[18].Visible = false;
    }

    private void ShowEmployeeDetails(string employeeId)
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandText = "exec [Transfer_PromotionGetEmployeeDetails] '" + employeeId + "'";
            var dtEmployeeDetails = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandText);
            if (dtEmployeeDetails.Rows.Count > 0)
            {
                lblEmployeeName.Text = dtEmployeeDetails.Rows[0].ItemArray[0].ToString() + " " + dtEmployeeDetails.Rows[0].ItemArray[1].ToString();
                lblJoiningDate.Text = dtEmployeeDetails.Rows[0].ItemArray[2].ToString() == string.Empty ? null : Convert.ToDateTime(dtEmployeeDetails.Rows[0].ItemArray[2].ToString()).ToString("dd-MMM-yyyy");
                lblOfficeLocation.Text = dtEmployeeDetails.Rows[0].ItemArray[3].ToString();
                lblEmployeeDepartment.Text = dtEmployeeDetails.Rows[0].ItemArray[4].ToString();
                lblSection.Text = dtEmployeeDetails.Rows[0].ItemArray[5].ToString();
                lblDesignation.Text = dtEmployeeDetails.Rows[0].ItemArray[6].ToString() == string.Empty ? null : dtEmployeeDetails.Rows[0].ItemArray[6].ToString();
                PanelForEmployeeDetails.Visible = true;
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

    protected void txtEmployeeCode_TextChanged(object sender, EventArgs e)
    {
        if (txtEmployeeCode.Text != string.Empty)
        {
            txtEmployeeCode.Text = txtEmployeeCode.Text.Split(':')[0].Trim() == "" ? "" : txtEmployeeCode.Text.Split(':')[0].Trim();
            ShowEmployeeDetails(txtEmployeeCode.Text);
            LoadAllTransfer_PromotionRecord(txtEmployeeCode.Text, rblForActionType.SelectedValue);
        }
    }

    protected void ddlOfficeLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        var officeLocation = ddlOfficeLocation.SelectedValue;
        if (officeLocation == "-1")
        {
            ddlDepartmentCode.Items.Clear();
            ddlSectionCode.Items.Clear();
        }
        else
        {
            LoadDepartmentCode(officeLocation);
        }
    }

    protected void ddlDepartmentCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        var officeLocation = ddlOfficeLocation.SelectedValue;
        var departmentCode = ddlDepartmentCode.SelectedValue;
        if (officeLocation == "-1" || departmentCode == "-1")
        {
            ddlSectionCode.Items.Clear();
        }
        else
        {
            LoadSectionCode(officeLocation, departmentCode);

        }
    }

    protected void rblForActionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblForActionType.SelectedValue == "T")
        {
            PanelForTransfer.Visible = true;
            PanelForPromotion.Visible = false;
        }
        else
        {
            PanelForPromotion.Visible = true;
            PanelForTransfer.Visible = false;
            popupPromotionDate.Enabled = true;
        }
        if (txtEmployeeCode.Text != string.Empty)
        {
            LoadAllTransfer_PromotionRecord(txtEmployeeCode.Text, rblForActionType.SelectedValue);
        }
    }

    private void LoadAllTransfer_PromotionRecord(string employeeId, string actionType)
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [Transfer_PromotionGetAllFrom_HRMS_Trans_Loc] '" + employeeId + "','" + actionType + "'";
            var dtTransferRecord = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandTest);
            grdEmployeeTransfer.DataSource = null;
            grdEmployeeTransfer.DataBind();
            grdEmployeePromotion.DataSource = null;
            grdEmployeePromotion.DataBind();
            if (dtTransferRecord.Rows.Count > 0)
            {
                if (actionType == "T")
                {
                    grdEmployeeTransfer.DataSource = dtTransferRecord;
                    grdEmployeeTransfer.DataBind();
                }
                else
                {
                    grdEmployeePromotion.DataSource = dtTransferRecord;
                    grdEmployeePromotion.DataBind();
                }
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

    protected void grdEmployeePromotion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        string lblCompanyID = ((Label)grdEmployeePromotion.Rows[selectedIndex].FindControl("lblCompanyID")).Text;
        string lblEmployeeID = ((Label)grdEmployeePromotion.Rows[selectedIndex].FindControl("lblEmployeeID")).Text;
        string lblFromDesignation = ((Label)grdEmployeePromotion.Rows[selectedIndex].FindControl("lblFromDesignation")).Text;
        string lblToDesignation = ((Label)grdEmployeePromotion.Rows[selectedIndex].FindControl("lblToDesignation")).Text;
        string lblRemarks = ((Label)grdEmployeePromotion.Rows[selectedIndex].FindControl("lblRemarks")).Text;
        string lblTransDetDate = ((Label)grdEmployeePromotion.Rows[selectedIndex].FindControl("lblTransDetDate")).Text;
        string entryUserID = current.UserId.ToString();
        int lblAutoSerial = Convert.ToInt32(((Label)grdEmployeePromotion.Rows[selectedIndex].FindControl("lblAutoSerial")).Text);

        if (e.CommandName.Equals("Delete"))
        {
            string _msg = null;
            try
            {
                var storedProcedureCommandTest = "exec [PromotionDeleteFrom_Hrms_Trans_Det] '" + lblCompanyID + "','" + lblEmployeeID + "','" + lblFromDesignation + "','" + lblToDesignation + "','" + lblRemarks + "','" + lblTransDetDate + "','" + entryUserID + "'," + lblAutoSerial + "";
                StoredProcedureExecutor.StoredProcedureExecuteNonQuery(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandTest);
                LoadAllTransfer_PromotionRecord(txtEmployeeCode.Text, rblForActionType.SelectedValue);
            }
            catch (SqlException sqlError)
            {
                _msg = "  Error Occured During Operation into Database, Data did not Delete from Database !  ";

            }
            catch (Exception inSystemExep)
            {
                _msg = " Error Occured, Data did not Delete from Database  ! ";

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
        else if (e.CommandName.Equals("Select"))
        {
            ddlcompany.SelectedValue = lblCompanyID;
            txtEmployeeCode.Text = lblEmployeeID;
            ddlDesignation.SelectedValue = lblToDesignation;
            popupPromotionDate.Text = lblTransDetDate;
            txtRemarks.Text = lblRemarks;
            lblDesignationCode.Text = lblFromDesignation;
            lblRowNoForUpdate.Text = lblAutoSerial.ToString();
            btnSaveActionType.Text = "Update";
        }
    }

    protected void grdEmployeePromotion_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void grdEmployeePromotion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[2].Visible = false;
        e.Row.Cells[6].Visible = false;
        e.Row.Cells[8].Visible = false;
        e.Row.Cells[11].Visible = false;
    }
}