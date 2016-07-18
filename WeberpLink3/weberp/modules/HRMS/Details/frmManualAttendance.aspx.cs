using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Details_frmManualAttendance : System.Web.UI.Page
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
            Session["tagForLoad"] = "NotSearch";
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
        return checkValidation;
    }

    private string CheckValidationForSearch()
    {
        const string checkValidation = "";
        if (ddlcompany.SelectedValue == "-1")
        {
            ddlcompany.Focus();
            return "Please Select Company Correctly !";
        }
        return checkValidation;
    }

    private void ClearAllControl()
    {
        txtRemarks.Text = string.Empty;
        calenderFromDate.SelectedDate = DateTime.Now;
        calenderToDate.SelectedDate = DateTime.Now;
        ddlDepartment.SelectedValue = "-1";
        txtEmployeeCodeForSearch.Text = string.Empty;
        btnSave.Text = "Save";
        Session["tagForLoad"] = "NotSearch";
        ControlGetEnabledTrue();
    }

    private void ControlGetEnabledFalse()
    {
        ddlcompany.Enabled = false;
        calenderTargetDate.Enabled = false;
        txtEmployeeCode.Enabled = false;
    }

    private void ControlGetEnabledTrue()
    {
        ddlcompany.Enabled = true;
        calenderTargetDate.Enabled = true;
        txtEmployeeCode.Enabled = true;
        ClsDropDownListController.EnableDisableDropDownList(ddlcompany);
    }

    private void SearchAttendanceRecord(string dateFrom, string dateTo, string employeeCode, string departmentCode)
    {
        string msg = null;
        try
        {
            string storedProcedureCommandTest = "exec [ManualAttendance_SearchFrom_hrms_atnd_det] '" + employeeCode + "','" + dateFrom + "','" + dateTo + "','" + departmentCode + "'";
            var dtEmployeeAttendance = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandTest);
            grdGetAttendanceRecord.DataSource = null;
            grdGetAttendanceRecord.DataBind();
            if (dtEmployeeAttendance.Rows.Count > 0)
            {
                grdGetAttendanceRecord.DataSource = dtEmployeeAttendance;
                grdGetAttendanceRecord.DataBind();
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

    private void SearchAttendanceRecordByempid(string dateFrom, string dateTo, string employeeCode)
    {
        DataTable dtEmployeeAttendance = new DataTable(); 

        string msg = null;

        try
        {
            string storedProcedureCommandTest = "exec [ManualAttendance_SearchFrom_hrms_atnd_det_byempid] '" + employeeCode + "','" + dateFrom + "','" + dateTo + "'";
            dtEmployeeAttendance = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandTest);
            grdGetAttendanceRecord.DataSource = null;
            grdGetAttendanceRecord.DataBind();
            if (dtEmployeeAttendance.Rows.Count > 0)
            {
                grdGetAttendanceRecord.DataSource = dtEmployeeAttendance;
                grdGetAttendanceRecord.DataBind();
                TimeSetintimepicker(dtEmployeeAttendance);
            }
            else
            {
                TimeSetintimepicker(dtEmployeeAttendance); 
            }


        }       
        catch (Exception inSystemExep)
        {
            msg = " Error Occured, Data did not Loaded from Database  !";
        }
        
    }


    private void TimeSetintimepicker(DataTable dtEmployeeAttendance)
    {
        string intime = "";
        string outtime = "";
        if (dtEmployeeAttendance.Rows.Count > 0)
        {
            intime=dtEmployeeAttendance.Rows[0]["Atnd_det_intime"].ToString();
            outtime = dtEmployeeAttendance.Rows[0]["Atnd_det_outtime"].ToString();
        }
        else
        {
            intime = "09:00 AM";
            outtime = "06:00 PM";
        }

        MKB.TimePicker.TimeSelector mkb = (MKB.TimePicker.TimeSelector)timeoffIntime;
        MKB.TimePicker.TimeSelector mkb2 = (MKB.TimePicker.TimeSelector)timeoffOuttime;
        DateTime dt1 = DateTime.Parse(intime);
        MKB.TimePicker.TimeSelector.AmPmSpec am_pm;
        if (dt1.ToString("tt") == "AM")
        {
            am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
        }
        else
        {
            am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
        }
        mkb.SetTime(dt1.Hour, dt1.Minute, am_pm);

        DateTime dt2 = DateTime.Parse(outtime);
        MKB.TimePicker.TimeSelector.AmPmSpec am_pm2;
        if (dt2.ToString("tt") == "AM")
        {
            am_pm2 = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
        }
        else
        {
            am_pm2 = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
        }

        mkb2.SetTime(dt2.Hour, dt2.Minute, am_pm2);
 
    }

   

    private void LoadParticularEmployeeAttendance(string dateForAttendance, string employeeCode)
    {
        string msg = null;
        try
        {
            string storedProcedureCommandTest = "exec [ManualAttendance_GetFrom_hrms_atnd_det] '" + employeeCode + "','" + dateForAttendance + "'";
            var dtEmployeeAttendance = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandTest);
            grdGetAttendanceRecord.DataSource = null;
            grdGetAttendanceRecord.DataBind();
            if (dtEmployeeAttendance.Rows.Count > 0)
            {
                grdGetAttendanceRecord.DataSource = dtEmployeeAttendance;
                grdGetAttendanceRecord.DataBind();
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
                lblEmployeeName.Text = dtEmployeeDetails.Rows[0].ItemArray[0] + " " + dtEmployeeDetails.Rows[0].ItemArray[1];
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

    private string SaveManualAttendance()
    {
        ManualAttendance objManualAttendance = new ManualAttendance();
        objManualAttendance.DateForAttendance = Convert.ToDateTime(calenderTargetDate.SelectedDate).ToString("dd-MMM-yyyy");
        objManualAttendance.EmployeeCode = txtEmployeeCode.Text;
        objManualAttendance.InTime = TimeFormatGenerate(timeoffIntime.Date.Hour.ToString() + ":" + timeoffIntime.Date.Minute.ToString() + ":" + timeoffIntime.AmPm.ToString());
        objManualAttendance.OutTime = TimeFormatGenerate(timeoffOuttime.Date.Hour.ToString() + ":" + timeoffOuttime.Date.Minute.ToString() + ":" + timeoffOuttime.AmPm.ToString());
        DateTime dateInTime = Convert.ToDateTime(objManualAttendance.DateForAttendance + " " + objManualAttendance.InTime);
        DateTime dateOutTime = Convert.ToDateTime(objManualAttendance.DateForAttendance + " " + objManualAttendance.OutTime);
        double totalMinutes = DateProcess.GetTotalMinutes(dateInTime, dateOutTime);
        lblHour.Text = DateProcess.TimeDuration(Convert.ToInt32(totalMinutes));
        objManualAttendance.Hours = lblHour.Text;
        objManualAttendance.Remarks = txtRemarks.Text;
        objManualAttendance.TxtTag = btnSave.Text;

        return Save(Session[GlobalData.sessionConnectionstring].ToString(), objManualAttendance);

        //return objManualAttendance.TxtTag == "Save" ? Save(Session[GlobalData.sessionConnectionstring].ToString(), objManualAttendance) : Update(Session[GlobalData.sessionConnectionstring].ToString(), objManualAttendance);
    }

    public string Save(string connectionString, ManualAttendance objManualAttendance)
    {
        string msg;
        var myConnection = new SqlConnection(connectionString);
        myConnection.Open();
        try
        {
            new SqlCommand("exec [ManualAttendance_InitiateInto_hrms_atnd_det] " +
                            "'" + objManualAttendance.EmployeeCode + "'," +
                            "'" + objManualAttendance.DateForAttendance + "'," +
                            "'" + objManualAttendance.InTime + "'," +
                            "'" + objManualAttendance.OutTime + "'," +
                            "'" + objManualAttendance.Hours + "'," +
                            "'" + objManualAttendance.Remarks + "'," +
                            "'" + objManualAttendance.TxtTag + "';", myConnection)
                 .ExecuteNonQuery();

            msg = "Data Saved Successfully ";
            ClearAllControl();
            LoadParticularEmployeeAttendance(objManualAttendance.DateForAttendance, objManualAttendance.EmployeeCode);

        }
        catch (Exception ex)
        {
            msg = " Error Occured, Data did not Save into Database !";
        }
        finally
        {
            myConnection.Close(); 
        }        

        return msg;
    }

    public string Update(string connectionString, ManualAttendance objManualAttendance)
    {
        string msg;
        var myConnection = new SqlConnection(connectionString);
        myConnection.Open();
        try
        {
            var dtManualAttendance = DataProcess.GetData(connectionString, Sqlgenerate.SqlGetEmployeeAttendanceRecord(objManualAttendance.EmployeeCode, objManualAttendance.DateForAttendance));
            if (dtManualAttendance.Rows.Count == 1)
            {
                new SqlCommand("exec [ManualAttendance_InitiateInto_hrms_atnd_det] " +
                            "'" + objManualAttendance.EmployeeCode + "'," +
                            "'" + objManualAttendance.DateForAttendance + "'," +
                            "'" + objManualAttendance.InTime + "'," +
                            "'" + objManualAttendance.OutTime + "'," +
                            "'" + objManualAttendance.Hours + "'," +
                            "'" + objManualAttendance.Remarks + "'," +
                            "'" + objManualAttendance.TxtTag + "';", myConnection)
                 .ExecuteNonQuery();
                msg = "Data Saved Successfully ";
                ClearAllControl();
                LoadParticularEmployeeAttendance(objManualAttendance.DateForAttendance, objManualAttendance.EmployeeCode);
            }
            else if (dtManualAttendance.Rows.Count == 0)
            {
                btnSave.Text = "Save";
                msg = "Data did not found of this Employee ! So, Please Save Now.";
            }
            else if (dtManualAttendance.Rows.Count == 2)
            {
                msg = "Data of this Employee Already Exit !";
            }
            else
            {
                ClearAllControl();
                LoadParticularEmployeeAttendance(objManualAttendance.DateForAttendance, objManualAttendance.EmployeeCode);
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

    private string TimeFormatGenerate(string atf)
    {
        string rtf = "";
        int h = Convert.ToInt32(atf.Split(':')[0].ToString());
        int m = Convert.ToInt32(atf.Split(':')[1].ToString());
        if (h > 12)
        {
            h = h - 12;
        }
        string hh = string.Format("{0:00}", h);
        string mm = string.Format("{0:00}", m);
        string ampm = atf.Split(':')[2].ToString();
        rtf = hh + ":" + mm + " " + ampm;
        return rtf;
    }

    private void InTimeFormatSet(string atf)
    {
        if (atf != null)
        {
            DateTime dt = DateTime.Parse(atf);
            MKB.TimePicker.TimeSelector.AmPmSpec am_pm;
            if (dt.ToString("tt") == "AM")
            {
                am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
            }
            else
            {
                am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
            }
            timeoffIntime.SetTime(dt.Hour, dt.Minute, am_pm);
        }
    }

    private void OutTimeFormatSet(string atf)
    {
        if (atf != null)
        {
            DateTime dt = DateTime.Parse(atf);
            MKB.TimePicker.TimeSelector.AmPmSpec am_pm;
            if (dt.ToString("tt") == "AM")
            {
                am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
            }
            else
            {
                am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
            }
            timeoffOuttime.SetTime(dt.Hour, dt.Minute, am_pm);
        }
    }

    private void ClearAll()
    {
        try
        {
            calenderTargetDate.SelectedDate = DateTime.Now;
            txtEmployeeCode.Text = string.Empty;
            PanelForEmployeeDetails.Visible = false;
            txtRemarks.Text = string.Empty;
            calenderFromDate.SelectedDate = DateTime.Now;
            calenderToDate.SelectedDate = DateTime.Now;
            ddlDepartment.SelectedValue = "-1";
            txtEmployeeCodeForSearch.Text = string.Empty;
            grdGetAttendanceRecord.DataSource = null;
            grdGetAttendanceRecord.DataBind();
            Session["tagForLoad"] = "NotSearch";
            btnSave.Text = "Save";
            ControlGetEnabledTrue();
            
        }
        catch (Exception msg)
        {

            ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "MessageBox",
                        "alert(' " + msg + " ');",
                        true);
        }
    }

    #endregion Methods

    #region Events

    protected void txtEmployeeCode_TextChanged(object sender, EventArgs e)
    {
        if (txtEmployeeCode.Text != string.Empty)
        {
            txtEmployeeCode.Text = txtEmployeeCode.Text.Split(':')[0].Trim() == "" ? "" : txtEmployeeCode.Text.Split(':')[0].Trim();
            ShowEmployeeDetails(txtEmployeeCode.Text);

            GetAttendanceRecord();

            timeoffIntime.Focus(); 

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        var validationMsg = CheckAllValidation();
        switch (validationMsg)
        {
            case "":
                {
                    var msg = SaveManualAttendance();
                    GetAttendanceRecord();
                    txtEmployeeCode.Focus(); 
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

    private void GetAttendanceRecord()
    {
        var dateFrom = Convert.ToDateTime(calenderTargetDate.SelectedDate).ToString("dd-MMM-yyyy");
        var dateTo = Convert.ToDateTime(calenderTargetDate.SelectedDate).ToString("dd-MMM-yyyy");
        var employeeID = txtEmployeeCode.Text;
        SearchAttendanceRecordByempid(dateFrom, dateTo, employeeID);
    }

    protected void grdGetAttendanceRecord_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void grdGetAttendanceRecord_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        var lblAtnd_Det_Emp_Id = ((Label)grdGetAttendanceRecord.Rows[selectedIndex].FindControl("lblAtnd_Det_Emp_Id")).Text;
        var lblAtnd_det_dateT = ((Label)grdGetAttendanceRecord.Rows[selectedIndex].FindControl("lblAtnd_det_date")).Text;
        var lblAtnd_det_date = Convert.ToDateTime(lblAtnd_det_dateT).ToString("dd-MMM-yyyy");
        if (e.CommandName.Equals("Delete"))
        {
            string msg = null;
            try
            {
                var storedProcedureComandTest = "exec [ManualAttendance_DeleteFrom_hrms_atnd_det] '" + lblAtnd_det_date + "','" + lblAtnd_Det_Emp_Id + "'";
                StoredProcedureExecutor.StoredProcedureExecuteNonQuery(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureComandTest);
                if (Session["tagForLoad"] == "NotSearch")
                {
                    var dateForAttendance = Convert.ToDateTime(calenderTargetDate.SelectedDate).ToString("dd-MMM-yyyy");
                    var employeeCode = txtEmployeeCode.Text == string.Empty ? null : txtEmployeeCode.Text;
                    LoadParticularEmployeeAttendance(dateForAttendance, employeeCode);
                }
                else if (Session["tagForLoad"] == "Search")
                {
                    var dateFrom = Convert.ToDateTime(calenderFromDate.SelectedDate).ToString("dd-MMM-yyyy");
                    var dateTo = Convert.ToDateTime(calenderToDate.SelectedDate).ToString("dd-MMM-yyyy");
                    var employeeID = txtEmployeeCodeForSearch.Text;
                    var departmentCode = ddlDepartment.SelectedValue;
                    SearchAttendanceRecord(dateFrom, dateTo, employeeID, departmentCode);
                }
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
            var lblAtnd_det_intime = ((Label)grdGetAttendanceRecord.Rows[selectedIndex].FindControl("lblAtnd_det_intime")).Text;
            var lblAtnd_det_outtime = ((Label)grdGetAttendanceRecord.Rows[selectedIndex].FindControl("lblAtnd_det_outtime")).Text;
            var lblAtnd_det_hrs = ((Label)grdGetAttendanceRecord.Rows[selectedIndex].FindControl("lblAtnd_det_hrs")).Text;
            var lblAtnd_det_rmks = ((Label)grdGetAttendanceRecord.Rows[selectedIndex].FindControl("lblAtnd_det_rmks")).Text;
            txtEmployeeCode.Text = lblAtnd_Det_Emp_Id;
            ShowEmployeeDetails(txtEmployeeCode.Text);
            calenderTargetDate.SelectedDate = Convert.ToDateTime(lblAtnd_det_date);
            InTimeFormatSet(lblAtnd_det_intime.Trim());
            OutTimeFormatSet(lblAtnd_det_outtime.Trim());
            lblHour.Text = lblAtnd_det_hrs;
            txtRemarks.Text = lblAtnd_det_rmks;
            btnSave.Text = "Update";
            ControlGetEnabledFalse();
        }
    }

    protected void txtEmployeeCodeForSearch_TextChanged(object sender, EventArgs e)
    {
        if (txtEmployeeCodeForSearch.Text != string.Empty)
        {
            txtEmployeeCodeForSearch.Text = txtEmployeeCodeForSearch.Text.Split(':')[0].Trim() == "" ? "" : txtEmployeeCodeForSearch.Text.Split(':')[0].Trim();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        var validationMsg = CheckValidationForSearch();
        switch (validationMsg)
        {
            case "":
                {
                    var dateFrom = Convert.ToDateTime(calenderFromDate.SelectedDate).ToString("dd-MMM-yyyy");
                    var dateTo = Convert.ToDateTime(calenderToDate.SelectedDate).ToString("dd-MMM-yyyy");
                    var employeeID = txtEmployeeCodeForSearch.Text;
                    var departmentCode = ddlDepartment.SelectedValue;
                    SearchAttendanceRecord(dateFrom, dateTo, employeeID, departmentCode);
                    Session["tagForLoad"] = "Search";
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

    protected void btnClearAll_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    
    protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dbname = ddlcompany.SelectedItem.Value;
        if (dbname != "-1")
        {
            var constr = ConfigurationSettings.AppSettings["SCFConnectionString"].Replace("SCF", dbname);
            Session[GlobalData.sessionConnectionstring] = constr;
            txtEmployeeCode_AutoCompleteExtender.ContextKey = Session[GlobalData.sessionConnectionstring].ToString();
            txtEmployeeCodeForSearch_AutoCompleteExtender.ContextKey = Session[GlobalData.sessionConnectionstring].ToString();
            ClsDropDownListController.LoadDropDownList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetDepartmentIntoDDL(), ddlDepartment, "Dept_Name", "Dept_Code");
        }
    }
    #endregion Events
    
}