using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Details_frmAdvanceDetailsEntry : System.Web.UI.Page
{
    private const string Rnode = "K";
    string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        if (!Page.IsPostBack)
        {
            LoadCompanyByUserPermission("ADM", Rnode);
            PanelForEmployeeDetails.Visible = false;
            panelForAdvanceType.Visible = false;
            ClsDropDownListController.EnableDisableDropDownList(ddlcompany);
            ddlcompany_SelectedIndexChanged(sender, e);
        }
    }
    
    private void LoadCompanyByUserPermission(string userid, string nodeid)
    {
        DataTable dt = new DataTable();
        ListItem lst;
        ddlcompany.Items.Clear();
        dt = AccessPermission.GetCompanyByUserandNodeid(ConnectionString, userid, nodeid);
        if (dt.Rows.Count > 0)
        {
            ddlcompany.Items.Insert(0, new ListItem("--- Please Select ---", "-1"));
            foreach (DataRow dr in dt.Rows)
            {
                lst = new ListItem();
                lst.Text = dr["COMP_CODE"].ToString() + ":" + dr["COMP_NAME"].ToString();
                lst.Value = dr["COMP_CODE"].ToString();
                ddlcompany.Items.Add(lst);
            }
        }
        else
        {
            ddlcompany.Items.Insert(0, new ListItem("--- No Data Found ---", "-1"));
        }
    }

    protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dbname = ddlcompany.SelectedItem.Value;
        var constr = ConfigurationSettings.AppSettings["SCFConnectionString"].Replace("SCF", dbname);
        Session[GlobalData.sessionConnectionstring] = constr;
        txtEmployeeCode_AutoCompleteExtender.ContextKey = Session[GlobalData.sessionConnectionstring].ToString();
        ClsDropDownListController.LoadDropDownListWithConcatenation(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetAdvanceTypeIntoDDL(), ddlAdvanceType, "advanceName", "advanceCode");
        LoadAdvanceDetailsEntry();
    }

    private string CheckAllValidation()
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
        if (ddlAdvanceType.SelectedValue == "-1")
        {
            ddlAdvanceType.Focus();
            return "Please Select Advance Type Correctly !";
        }
        if (txtFrequency.Text == string.Empty)
        {
            txtFrequency.Focus();
            return "Please Type Frequency Correctly !";
        }
        return checkValidation;
    }

    private void ClearAllControl()
    {
        txtEmployeeCode.Text = string.Empty;
        ddlAdvanceType.SelectedValue = "-1";
        txtAdvanceAmount.Text = string.Empty;
        txtFrequency.Text = string.Empty;
        txtFrequencySize.Text = string.Empty;
        calenderAdvanceTakenDate.SelectedDate = DateTime.Now;
        calenderDeductionEndDate.SelectedDate = DateTime.Now;
        calenderDeductionStartDate.SelectedDate = DateTime.Now;
        lblReferenceNoForUpdate.Text = string.Empty;
        PanelForEmployeeDetails.Visible = false;
        panelForAdvanceType.Visible = false;
        btnSaveAdvanceDetails.Text = "Save";
    }

    private void LoadAdvanceDetailsEntry()
    {
        string msg = null;
        try
        {
            const string storedProcedureCommandTest = "exec [AdvanceDetailsGetAllFromHrMs_Emp_Adv_Det] ";
            var dtAdvanceDetails = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandTest);
            grdAdvanceDetails.DataSource = null;
            grdAdvanceDetails.DataBind();
            if (dtAdvanceDetails.Rows.Count > 0)
            {
                grdAdvanceDetails.DataSource = dtAdvanceDetails;
                grdAdvanceDetails.DataBind();
            }
        }
        catch (SqlException sqlError)
        {
            msg = " Error Occured During Load Data From Database, Data did not Loaded from Database ! ";

        }
        catch (Exception inSystemExep)
        {
            msg = " Error Occured, Data did not Loaded from Database  !";

        }
        finally
        {
            if (msg != null)
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + msg + " ');",
                    true);
            }

        }
    }

    private void ShowEmployeeDetails(string employeeId)
    {
        string msg = null;
        try
        {
            var storedProcedureCommandText = "exec [Transfer_PromotionGetEmployeeDetails] '" + employeeId + "'";
            var dtEmployeeDetails = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandText);
            PanelForEmployeeDetails.Visible = false;
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
            msg = " Error Occured During Load Data From Database, Data did not Loaded from Database ! ";

        }
        catch (Exception inSystemExep)
        {
            msg = " Error Occured, Data did not Loaded from Database  !";

        }
        finally
        {
            if (msg != null)
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + msg + " ');",
                    true);
            }

        }
    }

    private void ShowAdvanceTypeDetails(string advanceTypeCode)
    {
        string msg = null;
        try
        {
            var storedProcedureCommandText = "exec [AdvanceDetailsEntryGetAdvanceDetails] '" + advanceTypeCode + "'";
            var dtAdvanceTypeDetails = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandText);
            panelForAdvanceType.Visible = false;
            if (dtAdvanceTypeDetails.Rows.Count > 0)
            {
                lblAdvanceTypeName.Text = dtAdvanceTypeDetails.Rows[0].ItemArray[0].ToString() == string.Empty ? null : dtAdvanceTypeDetails.Rows[0].ItemArray[0].ToString();
                lblMinimumAmount.Text = dtAdvanceTypeDetails.Rows[0].ItemArray[1].ToString() == string.Empty ? null : dtAdvanceTypeDetails.Rows[0].ItemArray[1].ToString();
                lblMaximumAmount.Text = dtAdvanceTypeDetails.Rows[0].ItemArray[2].ToString() == string.Empty ? null : dtAdvanceTypeDetails.Rows[0].ItemArray[2].ToString();
                lblPaymentMethod.Text = dtAdvanceTypeDetails.Rows[0].ItemArray[3].ToString() == string.Empty ? null : dtAdvanceTypeDetails.Rows[0].ItemArray[3].ToString();
                lblFrequency.Text = dtAdvanceTypeDetails.Rows[0].ItemArray[4].ToString() == string.Empty ? null : dtAdvanceTypeDetails.Rows[0].ItemArray[4].ToString();
                panelForAdvanceType.Visible = true;
            }
        }
        catch (SqlException sqlError)
        {
            msg = " Error Occured During Load Data From Database, Data did not Loaded from Database ! ";

        }
        catch (Exception inSystemExep)
        {
            msg = " Error Occured, Data did not Loaded from Database  !";

        }
        finally
        {
            if (msg != null)
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + msg + " ');",
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
        }
    }

    protected void ddlAdvanceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAdvanceType.SelectedValue != "-1")
        {
            ShowAdvanceTypeDetails(ddlAdvanceType.SelectedValue);
        }
    }

    protected void btnSaveAdvanceDetails_Click(object sender, EventArgs e)
    {
        var validationMsg = CheckAllValidation();
        switch (validationMsg)
        {
            case "":
                {
                    var msg = SaveAdvanceDetails();
                    MessageBox1.ShowSuccess(msg);
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }
    }

    private string SaveAdvanceDetails()
    {
        AdvanceDetailsEntry objAdvanceDetailsEntry = new AdvanceDetailsEntry();
        objAdvanceDetailsEntry.CompanyCode = ddlcompany.SelectedValue;
        objAdvanceDetailsEntry.EmployeeCode = txtEmployeeCode.Text;
        objAdvanceDetailsEntry.AdvanceCode = ddlAdvanceType.SelectedValue;
        objAdvanceDetailsEntry.InstallmentNo = txtFrequency.Text == string.Empty ? 0 : Convert.ToSingle(txtFrequency.Text);
        objAdvanceDetailsEntry.AdvanceTakenDate = Convert.ToDateTime(calenderAdvanceTakenDate.SelectedDate).ToString("dd-MMM-yyyy");
        objAdvanceDetailsEntry.DeductionStartDate = Convert.ToDateTime(calenderDeductionStartDate.SelectedDate).ToString("dd-MMM-yyyy");
        objAdvanceDetailsEntry.DeductionEndDate = Convert.ToDateTime(calenderDeductionEndDate.SelectedDate).ToString("dd-MMM-yyyy");
        objAdvanceDetailsEntry.AdvanceAmount = txtAdvanceAmount.Text == string.Empty ? 0 : Convert.ToDouble(txtAdvanceAmount.Text);
        objAdvanceDetailsEntry.InstallmentSize = txtFrequencySize.Text == string.Empty ? 0 : Convert.ToSingle(txtFrequencySize.Text);
        objAdvanceDetailsEntry.EntryUserId = current.UserId.ToString();
        objAdvanceDetailsEntry.ReferenceNo = lblReferenceNoForUpdate.Text == string.Empty ? null : lblReferenceNoForUpdate.Text;
        objAdvanceDetailsEntry.TxtTag = btnSaveAdvanceDetails.Text;
        return objAdvanceDetailsEntry.TxtTag == "Save" ? Save(Session[GlobalData.sessionConnectionstring].ToString(), objAdvanceDetailsEntry) : Update(Session[GlobalData.sessionConnectionstring].ToString(), objAdvanceDetailsEntry);
    }

    public string Save(string connectionString, AdvanceDetailsEntry objAdvanceDetailsEntry)
    {
        string msg;
        var myConnection = new SqlConnection(connectionString);
        myConnection.Open();
        try
        {
            new SqlCommand("exec [AdvanceDetailsInitiateIntoHrMs_Emp_Adv_Det] " +
                           "'" + objAdvanceDetailsEntry.CompanyCode + "'," +
                           "'" + objAdvanceDetailsEntry.EmployeeCode + "'," +
                           "'" + objAdvanceDetailsEntry.AdvanceCode + "'," +
                           "" + objAdvanceDetailsEntry.AdvanceAmount + "," +
                           "'" + objAdvanceDetailsEntry.DeductionStartDate + "'," +
                           "'" + objAdvanceDetailsEntry.DeductionEndDate + "'," +
                           "" + objAdvanceDetailsEntry.InstallmentSize + "," +
                           "'" + objAdvanceDetailsEntry.EntryUserId + "'," +
                           "'" + objAdvanceDetailsEntry.AdvanceTakenDate + "'," +
                           "'" + objAdvanceDetailsEntry.ReferenceNo + "'," +
                           "" + objAdvanceDetailsEntry.InstallmentNo + "," +
                           "'" + objAdvanceDetailsEntry.TxtTag + "';", myConnection)
                .ExecuteNonQuery();
            msg = "Data Saved Successfully ";
            ClearAllControl();
            LoadAdvanceDetailsEntry();
        }
        catch (SqlException sqlError)
        {
            msg = "Error Occured During Operation into Database, Data did not Save into Database !";
        }
        catch (Exception inSystemExep)
        {
            msg = " Error Occured, Data did not Save into Database !";
        }
        myConnection.Close();
        return msg;
    }

    public string Update(string connectionString, AdvanceDetailsEntry objAdvanceDetailsEntry)
    {
        string msg;
        var myConnection = new SqlConnection(connectionString);
        myConnection.Open();
        try
        {
            var dtAdvanceDetails = DataProcess.GetData(connectionString, Sqlgenerate.SqlGetRecordFromHrMs_Emp_Adv_Det(objAdvanceDetailsEntry.ReferenceNo));
            if (dtAdvanceDetails.Rows.Count == 1)
            {
                new SqlCommand("exec [AdvanceDetailsInitiateIntoHrMs_Emp_Adv_Det] " +
                               "'" + objAdvanceDetailsEntry.CompanyCode + "'," +
                               "'" + objAdvanceDetailsEntry.EmployeeCode + "'," +
                               "'" + objAdvanceDetailsEntry.AdvanceCode + "'," +
                               "" + objAdvanceDetailsEntry.AdvanceAmount + "," +
                               "'" + objAdvanceDetailsEntry.DeductionStartDate + "'," +
                               "'" + objAdvanceDetailsEntry.DeductionEndDate + "'," +
                               "" + objAdvanceDetailsEntry.InstallmentSize + "," +
                               "'" + objAdvanceDetailsEntry.EntryUserId + "'," +
                               "'" + objAdvanceDetailsEntry.AdvanceTakenDate + "'," +
                               "'" + objAdvanceDetailsEntry.ReferenceNo + "'," +
                               "" + objAdvanceDetailsEntry.InstallmentNo + "," +
                               "'" + objAdvanceDetailsEntry.TxtTag + "';", myConnection)
                    .ExecuteNonQuery();
                msg = "Data Saved Successfully ";
                ClearAllControl();
                LoadAdvanceDetailsEntry();
            }
            else if (dtAdvanceDetails.Rows.Count == 0)
            {
                btnSaveAdvanceDetails.Text = "Save";
                msg = "Data did not found ! So, Please Save Now.";
            }
            else
            {
                ClearAllControl();
                LoadAdvanceDetailsEntry();
                msg = " Please try again !";
            }
        }
        catch (SqlException sqlError)
        {
            msg = "Error Occured During Operation into Database, Data did not Save into Database !";
        }
        catch (Exception inSystemExep)
        {
            msg = " Error Occured, Data did not Save into Database !";
        }
        myConnection.Close();
        return msg;
    }

    protected void grdAdvanceDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void grdAdvanceDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        var lblReferenceNo = ((Label)grdAdvanceDetails.Rows[selectedIndex].FindControl("lblReferenceNo")).Text;
        var lblCompanyCode = ((Label)grdAdvanceDetails.Rows[selectedIndex].FindControl("lblCompanyCode")).Text;
        var lblEmployeeId = ((Label)grdAdvanceDetails.Rows[selectedIndex].FindControl("lblEmployeeID")).Text;
        var lblAdvanceTypeCode = ((Label)grdAdvanceDetails.Rows[selectedIndex].FindControl("lblAdvanceTypeCode")).Text;
        var lblAdvanceAmount = ((Label)grdAdvanceDetails.Rows[selectedIndex].FindControl("lblAdvanceAmount")).Text;
        var lblFrequencyValue = ((Label)grdAdvanceDetails.Rows[selectedIndex].FindControl("lblFrequency")).Text;
        var lblFrequencySizeG = ((Label)grdAdvanceDetails.Rows[selectedIndex].FindControl("lblFrequencySize")).Text;
        var lblAdvanceTakenDate = ((Label)grdAdvanceDetails.Rows[selectedIndex].FindControl("lblAdvanceTakenDate")).Text;
        var lblDeductionStartDate = ((Label)grdAdvanceDetails.Rows[selectedIndex].FindControl("lblDeductionStartDate")).Text;
        var lblDeductionEndDate = ((Label)grdAdvanceDetails.Rows[selectedIndex].FindControl("lblDeductionEndDate")).Text;
        if (e.CommandName.Equals("Delete"))
        {
            string msg = null;
            try
            {
                var sqlQuery = "DELETE from HrMs_Emp_Adv_Det WHERE referenceNo='" + lblReferenceNo + "'";
                DataProcess.DeleteQuery(Session[GlobalData.sessionConnectionstring].ToString(), sqlQuery);
                LoadAdvanceDetailsEntry();
            }
            catch (SqlException sqlError)
            {
                msg = "  Error Occured During Operation into Database, Data did not Delete from Database !  ";

            }
            catch (Exception inSystemExep)
            {
                msg = " Error Occured, Data did not Delete from Database  ! ";

            }
            finally
            {
                if (msg != null)
                {
                    ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "MessageBox",
                        "alert(' " + msg + " ');",
                        true);
                }
            }
        }
        else if (e.CommandName.Equals("Select"))
        {
            ddlcompany.SelectedValue = lblCompanyCode;
            txtEmployeeCode.Text = lblEmployeeId;
            ShowEmployeeDetails(txtEmployeeCode.Text);
            ddlAdvanceType.SelectedValue = lblAdvanceTypeCode;
            ShowAdvanceTypeDetails(ddlAdvanceType.SelectedValue);
            txtAdvanceAmount.Text = lblAdvanceAmount;
            txtFrequency.Text = lblFrequencyValue;
            txtFrequencySize.Text = lblFrequencySizeG;
            calenderAdvanceTakenDate.SelectedDate = Convert.ToDateTime(lblAdvanceTakenDate);
            calenderDeductionEndDate.SelectedDate = Convert.ToDateTime(lblDeductionEndDate);
            calenderDeductionStartDate.SelectedDate = Convert.ToDateTime(lblDeductionStartDate);
            lblReferenceNoForUpdate.Text = lblReferenceNo;
            btnSaveAdvanceDetails.Text = "Update";
        }
    }

    protected void grdAdvanceDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[2].Visible = false;
        e.Row.Cells[5].Visible = false;
    }

    protected void calenderDeductionStartDate_DateChanged(object sender, EventArgs e)
    {
        var modeOfPayment = lblPaymentMethod.Text;
        if (modeOfPayment != string.Empty)
        {
            calenderDeductionEndDate.SelectedDate = calenderDeductionStartDate.SelectedDate.AddMonths(Convert.ToInt32(modeOfPayment));

        }
    }
}