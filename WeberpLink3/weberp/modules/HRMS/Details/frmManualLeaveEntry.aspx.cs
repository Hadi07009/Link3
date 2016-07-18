using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Details_frmManualLeaveEntry : System.Web.UI.Page
{
    private const string Rnode = "K";
    string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    #region LoadEvent
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        if (!Page.IsPostBack)
        {
            LoadCompanyByUserPermission("ADM", Rnode);
            PanelForEmployeeDetails.Visible = false;
            panelForLeaveType.Visible = false;
            ClsDropDownListController.EnableDisableDropDownList(ddlcompany);
            ddlcompany_SelectedIndexChanged(sender, e);
        }
    }
    #endregion LoadEvent

    #region Methods

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
        if (ddlLeaveType.SelectedValue == "-1")
        {
            ddlLeaveType.Focus();
            return "Please Select Leave Type Correctly !";
        }
        if (txtNoOfDays.Text == string.Empty || txtNoOfDays.Text == "0")
        {
            txtNoOfDays.Focus();
            return "Please Enter No Of Days Correctly !";
        }
        if (Convert.ToSingle(txtNoOfDays.Text) > Convert.ToSingle(lblAvailableLeave.Text))
        {
            txtNoOfDays.Focus();
            return " Only " + lblAvailableLeave.Text + " Days of " + ddlLeaveType.SelectedItem.Text + " are Available !";
        }

        if (txtAddressDuringLeave.Text  == string.Empty)
        {
            txtAddressDuringLeave.Focus();
            return "Please Enter Address During Leave !";
        }


        if (txtContactNumber.Text == string.Empty)
        {
            txtContactNumber.Focus();
            return "Please Enter Contact Number During Leave !";
        }

        if (txtResponsiblePerson.Text == string.Empty)
        {
            txtResponsiblePerson.Focus();
            return "Please EnterResponsible Person During Leave !";
        }


        return checkValidation;
    }

    private void ClearAllControl()
    {
        ddlLeaveType.SelectedValue = "-1";
        panelForLeaveType.Visible = false;
        lblAvailableLeave.Text = string.Empty;
        calenderTargetDate.SelectedDate = DateTime.Now;
        txtNoOfDays.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        txtAddressDuringLeave.Text = string.Empty;
        txtContactNumber.Text = string.Empty;
        txtResponsiblePerson.Text = string.Empty;
        btnSaveLeaveRecord.Text = "Save";
    }

    private void ResetValue()
    {       
        txtNoOfDays.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        txtAddressDuringLeave.Text = string.Empty;
        txtContactNumber.Text = string.Empty;
        txtResponsiblePerson.Text = string.Empty;
        
    }

    private void LoadLeaveRecord(string employeeCode)
    {
        string msg = null;
        try
        {
            string storedProcedureCommandTest = "exec [ManualLeave_GetForm_HrMs_Emp_Leave_Det] '" + employeeCode + "'";
            var dtLeaveRecord = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandTest);
            grdShowLeaveRecord.DataSource = null;
            grdShowLeaveRecord.DataBind();
            if (dtLeaveRecord.Rows.Count > 0)
            {
                grdShowLeaveRecord.DataSource = dtLeaveRecord;
                grdShowLeaveRecord.DataBind();
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

                lblConfirmationDate.Text = dtEmployeeDetails.Rows[0].ItemArray[7].ToString() == string.Empty ? null : Convert.ToDateTime(dtEmployeeDetails.Rows[0].ItemArray[7].ToString()).ToString("dd-MMM-yyyy");


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

    private void ShowLeaveTypeDetails(string LeaveTypeCode)
    {
        string msg = null;
        try
        {
            var storedProcedureCommandText = "exec [ManualLeaveGetLeaveDetailsFrom_hrms_leave_mas] '" + LeaveTypeCode + "'";
            var dtLeaveTypeDetails = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandText);
            panelForLeaveType.Visible = false;
            if (dtLeaveTypeDetails.Rows.Count > 0)
            {
                lblModeofPayment.Text = dtLeaveTypeDetails.Rows[0].ItemArray[2].ToString() == string.Empty ? null : dtLeaveTypeDetails.Rows[0].ItemArray[2].ToString();
                lblMaximumPerAllow.Text = dtLeaveTypeDetails.Rows[0].ItemArray[0].ToString() == string.Empty ? null : dtLeaveTypeDetails.Rows[0].ItemArray[0].ToString();
                lblEmployeeType.Text = dtLeaveTypeDetails.Rows[0].ItemArray[6].ToString() == string.Empty ? null : dtLeaveTypeDetails.Rows[0].ItemArray[6].ToString();
                lblMLCFTNYear.Text = dtLeaveTypeDetails.Rows[0].ItemArray[5].ToString() == string.Empty ? null : dtLeaveTypeDetails.Rows[0].ItemArray[5].ToString();
                lblCarryForwordNextYear.Text = dtLeaveTypeDetails.Rows[0].ItemArray[4].ToString() == string.Empty ? null : dtLeaveTypeDetails.Rows[0].ItemArray[4].ToString();
                panelForLeaveType.Visible = false;
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

    private string SaveLeaveRecord()
    {
        ManualLeaveEntry objManualLeaveEntry = new ManualLeaveEntry();
        objManualLeaveEntry.EmployeeCode = txtEmployeeCode.Text == string.Empty ? null : txtEmployeeCode.Text;
        objManualLeaveEntry.LeaveTypeCode = ddlLeaveType.SelectedValue;
        objManualLeaveEntry.AvailableLeaveNo = Convert.ToSingle(lblAvailableLeave.Text);
        objManualLeaveEntry.LeaveStartDate = Convert.ToDateTime(calenderTargetDate.SelectedDate).ToString("dd-MMM-yyyy");
        objManualLeaveEntry.NoOfDays = Convert.ToSingle(txtNoOfDays.Text);
        objManualLeaveEntry.Remarks = txtRemarks.Text == string.Empty ? null : txtRemarks.Text;
        objManualLeaveEntry.TxtTag = btnSaveLeaveRecord.Text;
        objManualLeaveEntry.Address_During_Leave = txtAddressDuringLeave.Text;
        objManualLeaveEntry.Contact_Number_during_Leave = txtContactNumber.Text;
        objManualLeaveEntry.Responsible_Person_During_Leave = txtResponsiblePerson.Text;

        return objManualLeaveEntry.TxtTag == "Save" ? Save(Session[GlobalData.sessionConnectionstring].ToString(), objManualLeaveEntry) : Update(Session[GlobalData.sessionConnectionstring].ToString(), objManualLeaveEntry);
    }

    public string Save(string connectionString, ManualLeaveEntry objManualLeaveEntry)
    {
        string msg;
        var myConnection = new SqlConnection(connectionString);
        myConnection.Open();
        try
        {
            DataTable dtManualLeave = DataProcess.GetData(connectionString, Sqlgenerate.SqlGetEmployeeLeaveRecord(objManualLeaveEntry.EmployeeCode, objManualLeaveEntry.LeaveStartDate));
            if (dtManualLeave.Rows.Count == 0)
            {
                var noOfDaysLeave = objManualLeaveEntry.NoOfDays;
                var counterForLoop = 0;
                while (counterForLoop < noOfDaysLeave)
                {
                    if (objManualLeaveEntry.NoOfDays != .5)
                    {
                        objManualLeaveEntry.NoOfDays = 1;
                    }
                    new SqlCommand("exec [ManualLeave_InitiateInto_HrMs_Emp_Leave_Det] " +
                                "'" + objManualLeaveEntry.EmployeeCode + "'," +
                                "'" + objManualLeaveEntry.LeaveTypeCode + "'," +
                                "'" + objManualLeaveEntry.LeaveStartDate + "'," +
                                "" + objManualLeaveEntry.NoOfDays + "," +
                                "" + objManualLeaveEntry.AvailableLeaveNo + "," +
                                "'" + objManualLeaveEntry.Remarks + "'," +

                                "'" + objManualLeaveEntry.TxtTag + "'," +

                                "'" + objManualLeaveEntry.Address_During_Leave + "'," +
                                "'" + objManualLeaveEntry.Contact_Number_during_Leave + "'," +

                                "'" + objManualLeaveEntry.Responsible_Person_During_Leave + "';", myConnection)




                     .ExecuteNonQuery();
                    counterForLoop++;
                    objManualLeaveEntry.NoOfDays = noOfDaysLeave - counterForLoop;
                    objManualLeaveEntry.LeaveStartDate = Convert.ToDateTime(objManualLeaveEntry.LeaveStartDate).AddDays(1).ToString("dd-MMM-yyyy");
                }
                msg = "Data Saved Successfully ";
                ClearAllControl();
                LoadLeaveRecord(objManualLeaveEntry.EmployeeCode);
            }
            else if (dtManualLeave.Rows.Count > 0)
            {
                msg = "Data of this Employee Already Exit !";
            }
            else
            {
                ClearAllControl();
                LoadLeaveRecord(objManualLeaveEntry.EmployeeCode);
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
        finally
        {
            myConnection.Close();
        }
        
        return msg;
    }

    public string Update(string connectionString, ManualLeaveEntry objManualLeaveEntry)
    {
        string msg;
        var myConnection = new SqlConnection(connectionString);
        myConnection.Open();
        try
        {
            DataTable dtManualLeave = DataProcess.GetData(connectionString, Sqlgenerate.SqlGetEmployeeLeaveRecord(objManualLeaveEntry.EmployeeCode, objManualLeaveEntry.LeaveStartDate));
            if (dtManualLeave.Rows.Count == 1)
            {
                new SqlCommand("exec [ManualLeave_InitiateInto_HrMs_Emp_Leave_Det] " +
                            "'" + objManualLeaveEntry.EmployeeCode + "'," +
                            "'" + objManualLeaveEntry.LeaveTypeCode + "'," +
                            "'" + objManualLeaveEntry.LeaveStartDate + "'," +
                            "" + objManualLeaveEntry.NoOfDays + "," +
                            "" + objManualLeaveEntry.AvailableLeaveNo + "," +
                            "'" + objManualLeaveEntry.Remarks + "'," +

                              "'" + objManualLeaveEntry.TxtTag + "'," +
                               "'" + objManualLeaveEntry.Address_During_Leave  + "'," +
                                 "'" + objManualLeaveEntry.Contact_Number_during_Leave + "'," +
   

                            "'" + objManualLeaveEntry.Responsible_Person_During_Leave + "';", myConnection)

                 .ExecuteNonQuery();

                msg = "Data Saved Successfully ";
                ClearAllControl();
                LoadLeaveRecord(objManualLeaveEntry.EmployeeCode);
            }
            else if (dtManualLeave.Rows.Count == 0)
            {
                btnSaveLeaveRecord.Text = "Save";
                msg = "Data did not found of this Employee ! So, Please Save Now.";
            }
            else if (dtManualLeave.Rows.Count == 2)
            {
                msg = "Data of this Employee Already Exit !";
            }
            else
            {
                ClearAllControl();
                LoadLeaveRecord(objManualLeaveEntry.EmployeeCode);
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

    public string DeleteLeave(string connectionString, ManualLeaveEntry objManualLeaveEntry)
    {
        string msg="";
        var myConnection = new SqlConnection(connectionString);
        
        try
        {
            myConnection.Open();

            DataTable dtManualLeave = DataProcess.GetData(connectionString, Sqlgenerate.SqlGetLeaveRecord(objManualLeaveEntry.EmployeeCode, objManualLeaveEntry.LeaveDate));
            if (dtManualLeave.Rows.Count == 1)
            {
                string sqlcmd = "exec [spDeleteLeaveManual] " +
                            "'" + objManualLeaveEntry.LeaveDate + "'," +
                            "'" + objManualLeaveEntry.EmployeeCode + "'," +
                            "'" + objManualLeaveEntry.EntryUserID + "'";

                new SqlCommand("exec [spDeleteLeaveManual] " +
                            "'" + objManualLeaveEntry.LeaveDate + "'," +
                            "'" + objManualLeaveEntry.EmployeeCode + "'," +
                            "'" + objManualLeaveEntry.EntryUserID + "';", myConnection)

                 .ExecuteNonQuery();
                msg = "Leave Delete Successful ";
                ClearAllControl();
                LoadLeaveRecord(objManualLeaveEntry.EmployeeCode);
            }
            else if (dtManualLeave.Rows.Count == 0)
            {
                msg = "NO Data Found";
            }
        }
        catch (SqlException sqlError)
        {
            msg = "Error has occured to delete data. Please try again later";
        }
        finally
        {
            myConnection.Close();
        }       
       
        return msg;
    }


    private void LoadAllocatedLeave(string employeeCode, string leaveType)
    {
        string msg = null;
        try
        {
            string [] financialPeriod  = DateProcess.GetFinancialPeriod(Convert.ToDateTime(calenderTargetDate.SelectedDate).ToString("dd-MMM-yyyy"));
            string storedProcedureCommandTest = "exec [ManualLeave_GetAllocatedLeave] '" + employeeCode + "','" + leaveType + "','" + financialPeriod[0].ToString() + "','" + financialPeriod[1].ToString() + "'";
            var dtAvailableLeave = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandTest);
            lblAvailableLeave.Text = string.Empty;
            if (dtAvailableLeave.Rows.Count > 0)
            {
                lblMaximumAllocated.Text = dtAvailableLeave.Rows[0].ItemArray[0].ToString();
            }
        }
        catch (SqlException sqlError)
        {
            msg = " Error Occured During Load Available Leave From Database, Data did not Loaded from Database ! ";

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
    private void LoadAvailableLeave(string employeeCode, string leaveType)
    {
        string msg = null;
        try
        {
            string [] financialPeriod  = DateProcess.GetFinancialPeriod(Convert.ToDateTime(calenderTargetDate.SelectedDate).ToString("dd-MMM-yyyy"));
            string storedProcedureCommandTest = "exec [ManualLeave_GetAvailableLeave] '" + employeeCode + "','" + leaveType + "','" + financialPeriod[0].ToString() + "','" + financialPeriod[1].ToString() + "'";
            var dtAvailableLeave = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandTest);
            lblAvailableLeave.Text = string.Empty;
            if (dtAvailableLeave.Rows.Count > 0)
            {
                lblAvailableLeave.Text = dtAvailableLeave.Rows[0].ItemArray[0].ToString();
            }
        }
        catch (SqlException sqlError)
        {
            msg = " Error Occured During Load Available Leave From Database, Data did not Loaded from Database ! ";

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

    #endregion Methods

    #region Events

    protected void txtEmployeeCode_TextChanged(object sender, EventArgs e)
    {
        if (txtEmployeeCode.Text != string.Empty)
        {
            txtEmployeeCode.Text = txtEmployeeCode.Text.Split(':')[0].Trim() == "" ? "" : txtEmployeeCode.Text.Split(':')[0].Trim();
            var storedProcedureCommandText = "exec [ManualLeaveGetLeaveType] '" + txtEmployeeCode.Text + "'";
            ClsDropDownListController.LoadDropDownListFromStoredProcedure(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandText, ddlLeaveType, "Leave_Mas_Name", "Leave_Mas_Code");
            ShowEmployeeDetails(txtEmployeeCode.Text);
            LoadLeaveRecord(txtEmployeeCode.Text);
            lblMaximumPerAllow.Text = "0";
            lblMaximumAllocated.Text = "0";
            lblAvailableLeave.Text = "0";

        }
      
    }

    protected void ddlLeaveType_SelectedIndexChanged(object sender, EventArgs e)
    {

        ResetValue();
        
        if (ddlLeaveType.SelectedValue != "-1")
        {
            ShowLeaveTypeDetails(ddlLeaveType.SelectedValue);
            LoadAllocatedLeave(txtEmployeeCode.Text, ddlLeaveType.SelectedValue);
            LoadAvailableLeave(txtEmployeeCode.Text, ddlLeaveType.SelectedValue);
        }
        else if (ddlLeaveType.SelectedValue == "-1")
        {
            panelForLeaveType.Visible = false;
            lblAvailableLeave.Text = string.Empty;
        }
    }

    protected void btnSaveLeaveRecord_Click(object sender, EventArgs e)
    {
        var validationMsg = CheckAllValidation();
        switch (validationMsg)
        {
            case "":
                {
                    var msg = SaveLeaveRecord();
                    MessageBox1.ShowSuccess(msg);
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }

    }

    protected void grdShowLeaveRecord_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void grdShowLeaveRecord_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        var lblLeave_Det_Emp_Id = ((Label)grdShowLeaveRecord.Rows[selectedIndex].FindControl("lblLeave_Det_Emp_Id")).Text;
        var lblLeave_Det_Sta_DateT = ((Label)grdShowLeaveRecord.Rows[selectedIndex].FindControl("lblLeave_Det_Sta_Date")).Text;
        var lblLeave_Det_Sta_Date = Convert.ToDateTime(lblLeave_Det_Sta_DateT).ToString("dd/MM/yyyy");
        DateTime dtsta = Convert.ToDateTime(lblLeave_Det_Sta_Date);
        

        if (e.CommandName.Equals("Delete"))
        {
            string msg = null;
            try
            {               
                CallSPfordeleteLeave(lblLeave_Det_Emp_Id, dtsta);

                LoadLeaveRecord(lblLeave_Det_Emp_Id);
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
            var lblLeave_Det_LCode = ((Label)grdShowLeaveRecord.Rows[selectedIndex].FindControl("lblLeave_Det_LCode")).Text;
            var lblLeave_Det_Emp_Days = ((Label)grdShowLeaveRecord.Rows[selectedIndex].FindControl("lblLeave_Det_Emp_Days")).Text;
            var lblRemarks = ((Label)grdShowLeaveRecord.Rows[selectedIndex].FindControl("lblRemarks")).Text;

            var lblAddressduringleave = ((Label)grdShowLeaveRecord.Rows[selectedIndex].FindControl("lblAddress")).Text;
            var lblContactNumber = ((Label)grdShowLeaveRecord.Rows[selectedIndex].FindControl("lblContactNumber")).Text;
            var lblResponsiblePerson = ((Label)grdShowLeaveRecord.Rows[selectedIndex].FindControl("lblResponsiblePerson")).Text;


            txtEmployeeCode.Text = lblLeave_Det_Emp_Id;
            ShowEmployeeDetails(lblLeave_Det_Emp_Id);
            ddlLeaveType.SelectedValue = lblLeave_Det_LCode;
            ShowLeaveTypeDetails(lblLeave_Det_LCode);
            calenderTargetDate.SelectedDate = Convert.ToDateTime(lblLeave_Det_Sta_Date);
            txtNoOfDays.Text = lblLeave_Det_Emp_Days;
            txtAddressDuringLeave.Text = lblAddressduringleave;
            txtContactNumber.Text = lblContactNumber;
            txtResponsiblePerson.Text = lblResponsiblePerson;

            txtRemarks.Text = lblRemarks;

            LoadAvailableLeave(txtEmployeeCode.Text, lblLeave_Det_LCode);


            btnSaveLeaveRecord.Text = "Update";
        }
    }


    private void CallSPfordeleteLeave(string empid,DateTime fDate)
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(Session[GlobalData.sessionConnectionstring].ToString());
        sqlConn.Open();

        try
        {
           
            cmd.Connection = sqlConn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spDeleteLeaveManual";
            cmd.Parameters.Add(new SqlParameter("@date1", SqlDbType.NVarChar)).Value = fDate;               
            cmd.Parameters.Add(new SqlParameter("@empid", SqlDbType.NVarChar)).Value = empid;        
            cmd.Parameters.Add(new SqlParameter("@userid", SqlDbType.NVarChar)).Value =current.UserId;
            cmd.ExecuteNonQuery();
           
            sqlConn.Close();


        }
        catch (Exception ex)
        {

        }
        finally
        {
            sqlConn.Close();
        }

    }


    protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dbname = ddlcompany.SelectedItem.Value;
        if (dbname != "-1")
        {
            var constr = ConfigurationSettings.AppSettings["SCFConnectionString"].Replace("SCF", dbname);
            Session[GlobalData.sessionConnectionstring] = constr;
            txtEmployeeCode_AutoCompleteExtender.ContextKey = Session[GlobalData.sessionConnectionstring].ToString();
        }
    }

    #endregion Events

    protected void calenderTargetDate_DateChanged(object sender, EventArgs e)
    {
        if(txtEmployeeCode.Text != string.Empty && ddlLeaveType.SelectedValue != "-1")
        {
            LoadAvailableLeave(txtEmployeeCode.Text, ddlLeaveType.SelectedValue);
        }
    }
    protected void grdShowLeaveRecord_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells.Count > 0)
        {
           // e.Row.Cells[0].Visible = false;
          //  e.Row.Cells[1].Visible = false;
            //e.Row.Cells[4].Visible = false;
            //e.Row.Cells[5].Visible = false;
            //e.Row.Cells[6].Visible = false;
            //e.Row.Cells[7].Visible = false;
            //e.Row.Cells[8].Visible = false;
            //e.Row.Cells[12].Visible = false;
            //e.Row.Cells[13].Visible = false;
            //e.Row.Cells[14].Visible = false;
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {                         
        
        string msg = null;
        try
        {
            ManualLeaveEntry objManualLeaveEntry = new ManualLeaveEntry();
            objManualLeaveEntry.EmployeeCode = txtEmployeeCode.Text == string.Empty ? null : txtEmployeeCode.Text;
            objManualLeaveEntry.LeaveDate = Convert.ToDateTime(calenderTargetDate.SelectedDate);
            objManualLeaveEntry.EntryUserID = current.UserId;
            msg=DeleteLeave(ConnectionString, objManualLeaveEntry);
            MessageBox1.ShowSuccess(msg);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
            MessageBox1.ShowError(msg);
        }       
        
    }
   
}