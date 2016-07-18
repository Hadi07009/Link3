using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Setup_frmHolidaySetup : System.Web.UI.Page
{
    private const string Rnode = "K";
    string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    #region Load Events
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        if (!Page.IsPostBack)
        {
            LoadCompanyByUserPermission("ADM", Rnode);
            txtEmployeeCode.Enabled = false;
            LoadYear();
            GeneratePeriod(ddlYear.SelectedValue);
            ViewState["dtCurrentData"] = null;
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

    #endregion Load Events

    #region Methods
    private string SaveHoliday()
    {
        if (checkentry() != "")
        {

            return checkentry();

        }

        HolidaySetup objHolidaySetup = null;
        List<HolidaySetup> objholiday = new List<HolidaySetup>();

        for (int i = 0; i < chkofficelocation.Items.Count; i++)
        {
            if (chkofficelocation.Items[i].Selected == true)
            {

                for (int j = 0; j < chkshift.Items.Count; j++)
                {

                    if (chkshift.Items[j].Selected == true)
                    {
                        objHolidaySetup = new HolidaySetup();

                        objHolidaySetup.CompanyCode = ddlcompany.SelectedValue;
                        objHolidaySetup.OfficeLocationCode = chkofficelocation.Items[i].Value;
                        objHolidaySetup.ShiftID = chkshift.Items[j].Value;
                        objHolidaySetup.HolidayDate = Convert.ToDateTime(popupHolidayDate.SelectedDate).ToString("dd-MMM-yyyy");
                        objHolidaySetup.HolidayDescription = txtDescription.Text.Replace("'","");
                        objHolidaySetup.LoginUserID = current.UserId.ToString();
                        objHolidaySetup.EmployeeCode = txtEmployeeCode.Text;
                        objHolidaySetup.ConfigurationType = rblForConfiguration.SelectedValue;
                        objHolidaySetup.TxtTag = btnSaveHoliday.Text;
                        objHolidaySetup.ReferenceNo = lblForRefNoForUpdate.Text;

                        objholiday.Add(objHolidaySetup);
                    }
                }
            }

        }


        if (objHolidaySetup.ConfigurationType == "S")
        {
            if (objHolidaySetup.TxtTag == "Save")
            {
                return SaveHolidaySetupTable(Session[GlobalData.sessionConnectionstring].ToString(), objHolidaySetup, objholiday);
            }
            else
            {
                //return UpdateHolidaySetupTable(Session[GlobalData.sessionConnectionstring].ToString(), objHolidaySetup);
                return "";

            }
        }
        else
        {
            if (objHolidaySetup.TxtTag == "Save")
            {
                return SaveHolidaySetupEmpWiseTable(Session[GlobalData.sessionConnectionstring].ToString(), objHolidaySetup);
            }
            else
            {
                return UpdateHolidaySetupEmpWiseTable(Session[GlobalData.sessionConnectionstring].ToString(), objHolidaySetup);

            }
        }
    }


    private void loaddata()
    {
        string periodValue = ddlPeriod.SelectedValue;
        if (periodValue != "-1")
        {
            int yearValue = Convert.ToInt32(periodValue.Split(':')[1].Trim());
            int monthValue = Convert.ToInt32(periodValue.Split(':')[0].Trim());



            if (rblForConfiguration.SelectedValue == "S")
            {
                grdShowHolidayEmpWise.DataSource = null;
                grdShowHolidayEmpWise.DataBind();
                LoadHoliday(monthValue, yearValue, ddlcompany.SelectedItem.Value);
            }
            else
            {
                grdShowHoliday.DataSource = null;
                grdShowHoliday.DataBind();
                LoadHolidayEmployeeWise(monthValue, yearValue, ddlcompany.SelectedItem.Value);
            }
        }
        else
        {
            grdShowHolidayEmpWise.DataSource = null;
            grdShowHolidayEmpWise.DataBind();
            grdShowHoliday.DataSource = null;
            grdShowHoliday.DataBind();
        }

    }


    public string SaveHolidaySetupTable(string connectionString, HolidaySetup objHolidaySetup, List<HolidaySetup> objholiday)
    {
        string _msg = "";
        var myConnection = new SqlConnection(connectionString);
        myConnection.Open();
        try
        {
            foreach (HolidaySetup obholyday in objholiday)
            {

                new SqlCommand("exec [HolidayInitiateInto_Hrms_HolidaySetup] " +
                                    "'" + obholyday.CompanyCode + "'," +
                                    "'" + obholyday.OfficeLocationCode + "'," +
                                    "'" + obholyday.ShiftID + "'," +
                                    "'" + obholyday.HolidayDate + "'," +
                                    "'" + obholyday.HolidayDescription + "'," +
                                    "'" + obholyday.LoginUserID + "';", myConnection)
                                   .ExecuteNonQuery();

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

        finally
        {
            _msg = "Holiday Data Saved Successfully";
            myConnection.Close();

        }
        return _msg;
    }


    public string SaveHolidaySetupEmpWiseTable(string connectionString, HolidaySetup objHolidaySetup)
    {
        string _msg;
        var myConnection = new SqlConnection(connectionString);
        myConnection.Open();
        try
        {
            var dtHolidayData = DataProcess.GetData(connectionString, Sqlgenerate.SqlEmployeeHolidayData(objHolidaySetup.EmployeeCode, objHolidaySetup.HolidayDate));
            if (dtHolidayData.Rows.Count == 0)
            {
                new SqlCommand("exec [HolidayInitiateInto_Hrms_HolidaySetupEmpWise] " +
                                 "'" + objHolidaySetup.CompanyCode + "'," +
                                 "'" + objHolidaySetup.OfficeLocationCode + "'," +
                                 "'" + objHolidaySetup.ShiftID + "'," +
                                 "'" + objHolidaySetup.HolidayDate + "'," +
                                 "'" + objHolidaySetup.HolidayDescription + "'," +
                                 "'" + objHolidaySetup.EmployeeCode + "'," +
                                 "'" + objHolidaySetup.ConfigurationType + "'," +
                                 "'" + objHolidaySetup.LoginUserID + "';", myConnection)
                                .ExecuteNonQuery();
                _msg = "Data Saved Successfully ";
                txtEmployeeCode_TextChanged(null, null);
                ClearAllControl();
            }
            else if (dtHolidayData.Rows.Count > 0)
            {
                _msg = "These Data Already Exist!";
            }
            else
            {
                txtEmployeeCode_TextChanged(null, null);
                ClearAllControl();
                _msg = " Please try again !";
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
        myConnection.Close();
        return _msg;

    }

    public string UpdateHolidaySetupTable(string connectionString, HolidaySetup objHolidaySetup)
    {
        string _msg;
        var myConnection = new SqlConnection(connectionString);
        myConnection.Open();
        try
        {
            var dtHolidayData = DataProcess.GetData(connectionString, Sqlgenerate.SqlCheckHolidayData(objHolidaySetup.CompanyCode, objHolidaySetup.OfficeLocationCode, objHolidaySetup.ShiftID, objHolidaySetup.HolidayDate));
            var dtReference = DataProcess.GetData(connectionString, Sqlgenerate.SqlCheckHolidayData(objHolidaySetup.ReferenceNo));
            if (dtReference.Rows.Count == 1)
            {
                new SqlCommand("exec [HolidayUpdate_Hrms_HolidaySetup] " +
                                 "'" + objHolidaySetup.CompanyCode + "'," +
                                 "'" + objHolidaySetup.OfficeLocationCode + "'," +
                                 "'" + objHolidaySetup.ShiftID + "'," +
                                 "'" + objHolidaySetup.HolidayDate + "'," +
                                 "'" + objHolidaySetup.HolidayDescription + "'," +
                                 "'" + objHolidaySetup.LoginUserID + "'," +
                                 "'" + objHolidaySetup.ReferenceNo + "';", myConnection)
                                .ExecuteNonQuery();
                _msg = "Data Saved Successfully ";
                // ddlShiftID_SelectedIndexChanged(null,null);
                ClearAllControl();
            }
            else if (dtReference.Rows.Count == 0)
            {
                btnSaveHoliday.Text = "Save";
                _msg = "Data did not found For Update ! So, Please Save Now.";
            }
            else if (dtHolidayData.Rows.Count == 2)
            {
                _msg = "These Data Already Exist !";
            }
            else
            {
                ClearAllControl();
                //ddlShiftID_SelectedIndexChanged(null, null);
                _msg = " Please try again !";
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
        myConnection.Close();
        return _msg;

    }

    public string UpdateHolidaySetupEmpWiseTable(string connectionString, HolidaySetup objHolidaySetup)
    {
        string _msg;
        var myConnection = new SqlConnection(connectionString);
        myConnection.Open();
        try
        {
            var dtHolidayData = DataProcess.GetData(connectionString, Sqlgenerate.SqlEmployeeHolidayData(objHolidaySetup.EmployeeCode, objHolidaySetup.HolidayDate));
            var dtReference = DataProcess.GetData(connectionString, Sqlgenerate.SqlEmployeeHolidayData(objHolidaySetup.ReferenceNo));
            if (dtReference.Rows.Count == 1)
            {
                new SqlCommand("exec [HolidayUpdate_Hrms_HolidaySetupEmpWise] " +
                                 "'" + objHolidaySetup.CompanyCode + "'," +
                                 "'" + objHolidaySetup.OfficeLocationCode + "'," +
                                 "'" + objHolidaySetup.ShiftID + "'," +
                                 "'" + objHolidaySetup.HolidayDate + "'," +
                                 "'" + objHolidaySetup.HolidayDescription + "'," +
                                 "'" + objHolidaySetup.EmployeeCode + "'," +
                                 "'" + objHolidaySetup.ConfigurationType + "'," +
                                 "'" + objHolidaySetup.LoginUserID + "'," +
                                 "'" + objHolidaySetup.ReferenceNo + "';", myConnection)
                                .ExecuteNonQuery();
                _msg = "Data Saved Successfully ";
                txtEmployeeCode_TextChanged(null, null);
                ClearAllControl();
            }
            else if (dtReference.Rows.Count == 0)
            {
                btnSaveHoliday.Text = "Save";
                _msg = "Data did not found For Update ! So, Please Save Now.";
            }
            else if (dtHolidayData.Rows.Count == 2)
            {
                _msg = "These Data Already Exist !";
            }
            else
            {
                txtEmployeeCode_TextChanged(null, null);
                ClearAllControl();
                _msg = " Please try again !";
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
        myConnection.Close();
        return _msg;

    }

    private string CheckAllValidation()
    {
        const string checkValidation = "";
        if (ddlcompany.SelectedValue == "-1")
        {
            ddlcompany.Focus();
            return "Please Select Company Correctly !";
        }

        if (rblForConfiguration.SelectedValue == "E" && txtEmployeeCode.Text == string.Empty)
        {
            txtEmployeeCode.Focus();
            return "Please Enter Employee ID Correctly !";
        }
        if (txtDescription.Text == string.Empty)
        {
            txtDescription.Focus();
            return "Please Type Holiday  Description Correctly !";
        }
        return checkValidation;
    }

    private string CheckAllValidationOnOfficeLocation()
    {
        const string checkValidation = "";
        if (ddlPeriod.SelectedValue == "-1")
        {
            return "Please Select Period Correctly !";
        }
        if (ddlcompany.SelectedValue == " ")
        {
            return "Please Select Company Correctly !";
        }

        return checkValidation;
    }

    private void ClearAllControl()
    {
        lblForRefNoForUpdate.Text = string.Empty;
        btnSaveHoliday.Text = "Save";
        if (rblForConfiguration.SelectedValue == "E")
        {
            txtEmployeeCode.Enabled = true;
        }
        else
        {
            txtEmployeeCode.Enabled = false;
        }
        ddlcompany.Enabled = true;
        rblForConfiguration.Enabled = true;
        popupHolidayDate.Enabled = true;
        ClsDropDownListController.EnableDisableDropDownList(ddlcompany);
    }

    private void ClearAllControlForClearBtn()
    {

        txtEmployeeCode.Text = string.Empty;
        txtDescription.Text = string.Empty;
        popupHolidayDate.SelectedDate = DateTime.Now;
        lblForRefNoForUpdate.Text = string.Empty;
        btnSaveHoliday.Text = "Save";
        if (rblForConfiguration.SelectedValue == "E")
        {
            txtEmployeeCode.Enabled = true;
        }
        else
        {
            txtEmployeeCode.Enabled = false;
        }
        ddlcompany.Enabled = true;
        rblForConfiguration.Enabled = true;
        popupHolidayDate.Enabled = true;
        ClsDropDownListController.EnableDisableDropDownList(ddlcompany);
    }

    private void GetEnabledFalse()
    {
        ddlcompany.Enabled = false;
        // ddlOfficeLocation.Enabled = false;
        //rblForConfiguration.Enabled = false;
        //  ddlShiftID.Enabled = false;
        txtEmployeeCode.Enabled = false;
        popupHolidayDate.Enabled = false;
    }

    private void LoadHoliday()
    {
        var dtHoliday = new DataTable();
        var myConnection = new SqlConnection(Session[GlobalData.sessionConnectionstring].ToString());
        var myCommand = myConnection.CreateCommand();
        myConnection.Open();
        myCommand.CommandText = "exec [HolidayGetAllFromHrms_HolidaySetup] ";
        myCommand.ExecuteNonQuery();
        var daHoliday = new SqlDataAdapter(myCommand);
        daHoliday.Fill(dtHoliday);
        grdShowHoliday.DataSource = null;
        grdShowHoliday.DataBind();
        if (dtHoliday.Rows.Count > 0)
        {
            grdShowHoliday.DataSource = dtHoliday;
            grdShowHoliday.DataBind();
        }
        myConnection.Close();
    }

    private void LoadHoliday(int monthIndex, int yearNumber)
    {
        var dtHoliday = new DataTable();
        var myConnection = new SqlConnection(Session[GlobalData.sessionConnectionstring].ToString());
        var myCommand = myConnection.CreateCommand();
        myConnection.Open();
        myCommand.CommandText = "exec [HolidayGetPeriodicDataFromHrms_HolidaySetup] " + monthIndex + "," + yearNumber + " ";
        myCommand.ExecuteNonQuery();
        var daHoliday = new SqlDataAdapter(myCommand);
        daHoliday.Fill(dtHoliday);
        grdShowHoliday.DataSource = null;
        grdShowHoliday.DataBind();

        ViewState["dtCurrentData"] = null;
        if (dtHoliday.Rows.Count > 0)
        {
            grdShowHoliday.DataSource = dtHoliday;
            grdShowHoliday.DataBind();
            ViewState["dtCurrentData"] = dtHoliday;
        }
        myConnection.Close();
    }

    private void LoadHoliday(int monthIndex, int yearNumber, string companyCode)
    {
        var dtHoliday = new DataTable();
        var myConnection = new SqlConnection(Session[GlobalData.sessionConnectionstring].ToString());
        var myCommand = myConnection.CreateCommand();
        myConnection.Open();
        myCommand.CommandText = "exec [HolidayGetPeriodicDataFromHrms_HolidaySetupCompany] " + monthIndex + "," + yearNumber + ",'" + companyCode + "'";
        myCommand.ExecuteNonQuery();
        var daHoliday = new SqlDataAdapter(myCommand);
        daHoliday.Fill(dtHoliday);
        grdShowHoliday.DataSource = null;
        grdShowHoliday.DataBind();
        ViewState["dtCurrentData"] = null;
        if (dtHoliday.Rows.Count > 0)
        {
            grdShowHoliday.DataSource = dtHoliday;
            grdShowHoliday.DataBind();
            ViewState["dtCurrentData"] = dtHoliday;
        }
        myConnection.Close();
    }

    private void LoadHoliday(int monthIndex, int yearNumber, string companyCode, string officeLocation)
    {
        var dtHoliday = new DataTable();
        var myConnection = new SqlConnection(Session[GlobalData.sessionConnectionstring].ToString());
        var myCommand = myConnection.CreateCommand();
        myConnection.Open();
        myCommand.CommandText = "exec [HolidayGetPeriodicDataFromHrms_HolidaySetupComOffLoc] " + monthIndex + "," + yearNumber + ",'" + companyCode + "','" + officeLocation + "'";
        myCommand.ExecuteNonQuery();
        var daHoliday = new SqlDataAdapter(myCommand);
        daHoliday.Fill(dtHoliday);
        grdShowHoliday.DataSource = null;
        grdShowHoliday.DataBind();
        ViewState["dtCurrentData"] = null;
        if (dtHoliday.Rows.Count > 0)
        {
            grdShowHoliday.DataSource = dtHoliday;
            grdShowHoliday.DataBind();
            ViewState["dtCurrentData"] = dtHoliday;
        }
        myConnection.Close();
    }

    private void LoadHoliday(int monthIndex, int yearNumber, string companyCode, string officeLocation, string shiftID)
    {
        var dtHoliday = new DataTable();
        var myConnection = new SqlConnection(Session[GlobalData.sessionConnectionstring].ToString());
        var myCommand = myConnection.CreateCommand();
        myConnection.Open();
        myCommand.CommandText = "exec [HolidayGetPeriodicDataFromHrms_HolidaySetupComOffLocShift] " + monthIndex + "," + yearNumber + ",'" + companyCode + "','" + officeLocation + "','" + shiftID + "'";
        myCommand.ExecuteNonQuery();
        var daHoliday = new SqlDataAdapter(myCommand);
        daHoliday.Fill(dtHoliday);
        grdShowHoliday.DataSource = null;
        grdShowHoliday.DataBind();
        ViewState["dtCurrentData"] = null;
        if (dtHoliday.Rows.Count > 0)
        {
            grdShowHoliday.DataSource = dtHoliday;
            grdShowHoliday.DataBind();
            ViewState["dtCurrentData"] = dtHoliday;
        }
        myConnection.Close();
    }

    private void LoadHolidayEmployeeWise()
    {
        var dtHolidayEmpWise = new DataTable();
        var myConnection = new SqlConnection(Session[GlobalData.sessionConnectionstring].ToString());
        var myCommand = myConnection.CreateCommand();
        myConnection.Open();
        myCommand.CommandText = "exec [HolidayGetAllFromhrms_holidaysetupEmpWise] ";
        myCommand.ExecuteNonQuery();
        var daHolidayEmpWise = new SqlDataAdapter(myCommand);
        daHolidayEmpWise.Fill(dtHolidayEmpWise);
        grdShowHolidayEmpWise.DataSource = null;
        grdShowHolidayEmpWise.DataBind();
        if (dtHolidayEmpWise.Rows.Count > 0)
        {
            grdShowHolidayEmpWise.DataSource = dtHolidayEmpWise;
            grdShowHolidayEmpWise.DataBind();
        }
        myConnection.Close();
    }

    private void LoadHolidayEmployeeWise(int monthIndex, int yearNumber)
    {
        var dtHolidayEmpWise = new DataTable();
        var myConnection = new SqlConnection(Session[GlobalData.sessionConnectionstring].ToString());
        var myCommand = myConnection.CreateCommand();
        myConnection.Open();
        myCommand.CommandText = "exec [HolidayGetPeriodicDataFromhrms_holidaysetupEmpWise]" + monthIndex + "," + yearNumber + " ";
        myCommand.ExecuteNonQuery();
        var daHolidayEmpWise = new SqlDataAdapter(myCommand);
        daHolidayEmpWise.Fill(dtHolidayEmpWise);
        grdShowHolidayEmpWise.DataSource = null;
        grdShowHolidayEmpWise.DataBind();
        if (dtHolidayEmpWise.Rows.Count > 0)
        {
            grdShowHolidayEmpWise.DataSource = dtHolidayEmpWise;
            grdShowHolidayEmpWise.DataBind();
        }
        myConnection.Close();
    }

    private void LoadHolidayEmployeeWise(int monthIndex, int yearNumber, string companyCode)
    {
        var dtHolidayEmpWise = new DataTable();
        var myConnection = new SqlConnection(Session[GlobalData.sessionConnectionstring].ToString());
        var myCommand = myConnection.CreateCommand();
        myConnection.Open();
        myCommand.CommandText = "exec [HolidayGetPeriodicDataFromhrms_holidaysetupEmpWiseCompany]" + monthIndex + "," + yearNumber + ",'" + companyCode + "'";
        myCommand.ExecuteNonQuery();
        var daHolidayEmpWise = new SqlDataAdapter(myCommand);
        daHolidayEmpWise.Fill(dtHolidayEmpWise);
        grdShowHolidayEmpWise.DataSource = null;
        grdShowHolidayEmpWise.DataBind();
        ViewState["dtCurrentData"] = null;
        if (dtHolidayEmpWise.Rows.Count > 0)
        {
            grdShowHolidayEmpWise.DataSource = dtHolidayEmpWise;
            grdShowHolidayEmpWise.DataBind();
            ViewState["dtCurrentData"] = dtHolidayEmpWise;
        }
        myConnection.Close();
    }

    private void LoadHolidayEmployeeWise(int monthIndex, int yearNumber, string companyCode, string officeLocation)
    {
        var dtHolidayEmpWise = new DataTable();
        var myConnection = new SqlConnection(Session[GlobalData.sessionConnectionstring].ToString());
        var myCommand = myConnection.CreateCommand();
        myConnection.Open();
        myCommand.CommandText = "exec [HolidayGetPeriodicDataFromhrms_holidaysetupEmpWiseCompOffLoc]" + monthIndex + "," + yearNumber + ",'" + companyCode + "','" + officeLocation + "'";
        myCommand.ExecuteNonQuery();
        var daHolidayEmpWise = new SqlDataAdapter(myCommand);
        daHolidayEmpWise.Fill(dtHolidayEmpWise);
        grdShowHolidayEmpWise.DataSource = null;
        grdShowHolidayEmpWise.DataBind();
        ViewState["dtCurrentData"] = null;
        if (dtHolidayEmpWise.Rows.Count > 0)
        {
            grdShowHolidayEmpWise.DataSource = dtHolidayEmpWise;
            grdShowHolidayEmpWise.DataBind();
            ViewState["dtCurrentData"] = dtHolidayEmpWise;
        }
        myConnection.Close();
    }

    private void LoadHolidayEmployeeWise(int monthIndex, int yearNumber, string companyCode, string officeLocation, string shiftId)
    {
        var dtHolidayEmpWise = new DataTable();
        var myConnection = new SqlConnection(Session[GlobalData.sessionConnectionstring].ToString());
        var myCommand = myConnection.CreateCommand();
        myConnection.Open();
        myCommand.CommandText = "exec [HolidayGetPeriodicDataFromhrms_holidaysetupEmpWiseCompOffLocShift]" + monthIndex + "," + yearNumber + ",'" + companyCode + "','" + officeLocation + "','" + shiftId + "'";
        myCommand.ExecuteNonQuery();
        var daHolidayEmpWise = new SqlDataAdapter(myCommand);
        daHolidayEmpWise.Fill(dtHolidayEmpWise);
        grdShowHolidayEmpWise.DataSource = null;
        grdShowHolidayEmpWise.DataBind();
        ViewState["dtCurrentData"] = null;
        if (dtHolidayEmpWise.Rows.Count > 0)
        {
            grdShowHolidayEmpWise.DataSource = dtHolidayEmpWise;
            grdShowHolidayEmpWise.DataBind();
            ViewState["dtCurrentData"] = dtHolidayEmpWise;
        }
        myConnection.Close();
    }

    private void LoadHolidayEmployeeWise(int monthIndex, int yearNumber, string companyCode, string officeLocation, string shiftId, string employeeCode)
    {
        var dtHolidayEmpWise = new DataTable();
        var myConnection = new SqlConnection(Session[GlobalData.sessionConnectionstring].ToString());
        var myCommand = myConnection.CreateCommand();
        myConnection.Open();
        myCommand.CommandText = "exec [HolidayGetPeriodicDataFromhrms_holidaysetupEmpWiseCompOffLocShiftEmployee]" + monthIndex + "," + yearNumber + ",'" + companyCode + "','" + officeLocation + "','" + shiftId + "','" + employeeCode + "'";
        myCommand.ExecuteNonQuery();
        var daHolidayEmpWise = new SqlDataAdapter(myCommand);
        daHolidayEmpWise.Fill(dtHolidayEmpWise);
        grdShowHolidayEmpWise.DataSource = null;
        grdShowHolidayEmpWise.DataBind();
        ViewState["dtCurrentData"] = null;
        if (dtHolidayEmpWise.Rows.Count > 0)
        {
            grdShowHolidayEmpWise.DataSource = dtHolidayEmpWise;
            grdShowHolidayEmpWise.DataBind();
            ViewState["dtCurrentData"] = dtHolidayEmpWise;
        }
        myConnection.Close();
    }

    protected void txtEmployeeCode_TextChanged(object sender, EventArgs e)
    {
        if (txtEmployeeCode.Text != string.Empty)
        {
            txtEmployeeCode.Text = txtEmployeeCode.Text.Split(':')[0].Trim() == "" ? "" : txtEmployeeCode.Text.Split(':')[0].Trim();
            string periodValue = ddlPeriod.SelectedValue;
            var dbname = ddlcompany.SelectedItem.Value;
            // var officeLocation = ddlOfficeLocation.SelectedValue;
            //   var shiftId = ddlShiftID.SelectedValue;
            int yearValue = Convert.ToInt32(periodValue.Split(':')[1].Trim());
            int monthValue = Convert.ToInt32(periodValue.Split(':')[0].Trim());


            string officelocation = "";

            foreach (ListItem lst in chkofficelocation.Items)
            {
                if (lst.Selected)
                {
                    if (officelocation == "")
                    {
                        officelocation += "" + lst.Value.ToString() + "";
                    }
                    else
                    {
                        officelocation += "," + lst.Value.ToString() + "";
                    }
                }
            }


            string shift = "";

            foreach (ListItem lst in chkshift.Items)
            {
                if (lst.Selected)
                {
                    if (shift == "")
                    {
                        shift += "" + lst.Value.ToString() + "";
                    }
                    else
                    {
                        shift += "," + lst.Value.ToString() + "";
                    }
                }
            }




            if (rblForConfiguration.SelectedValue != "S")
            {
                grdShowHoliday.DataSource = null;
                grdShowHoliday.DataBind();
                LoadHolidayEmployeeWise(monthValue, yearValue, dbname, officelocation, shift, txtEmployeeCode.Text);
            }

        }
    }

    private void LoadYear()
    {
        ddlYear.DataSource = GenerateASetOfYear();
        ddlYear.DataValueField = "yearId";
        ddlYear.DataTextField = "yearName";
        ddlYear.DataBind();
        ddlYear.SelectedValue = DateTime.Now.Year.ToString();
    }

    private DataTable GenerateASetOfYear()
    {
        DataTable dtYear = new DataTable();
        dtYear.Columns.Add("yearId", typeof(string));
        dtYear.Columns.Add("yearName", typeof(string));

        for (int i = DateTime.Now.Year - 1; i < DateTime.Now.Year + 10; i++)
        {
            DataRow drNew = dtYear.NewRow();
            drNew["yearId"] = i.ToString();
            drNew["yearName"] = i.ToString();
            dtYear.Rows.Add(drNew);
        }
        return dtYear;
    }

    private void GeneratePeriod(string yearValue)
    {
        ddlPeriod.Items.Clear();
        var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
        ddlPeriod.Items.Insert(0, new ListItem("--- Please Select ---", "-1"));
        for (int i = 0; i <= months.Length - 1; i++)
        {
            ddlPeriod.Items.Add(new ListItem(months[i] + " - " + yearValue, i.ToString() + " : " + yearValue));
        }
    }

    #endregion Methods

    #region Events

    protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
    {



        var dbname = ddlcompany.SelectedItem.Value;
        var constr = ConfigurationSettings.AppSettings["SCFConnectionString"].Replace("SCF", dbname);
        Session[GlobalData.sessionConnectionstring] = constr;
        txtEmployeeCode_AutoCompleteExtender.ContextKey = Session[GlobalData.sessionConnectionstring].ToString();
        //txtDescription_AutoCompleteExtender.ContextKey = Session[GlobalData.sessionConnectionstring].ToString() + ":" + rblForConfiguration.SelectedValue.ToString();


        ClsDropDownListController.LoadCheckBoxList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetOfficeLocationIntoDDL(), chkofficelocation, "Division_Master_Name", "Division_Master_Code");


        ClsDropDownListController.LoadCheckBoxList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetShiftTypeIntoDDL(), chkshift, "Shift", "Shift Code");

        string periodValue = ddlPeriod.SelectedValue;
        if (periodValue != "-1")
        {
            int yearValue = Convert.ToInt32(periodValue.Split(':')[1].Trim());
            int monthValue = Convert.ToInt32(periodValue.Split(':')[0].Trim());
            if (rblForConfiguration.SelectedValue == "S")
            {
                grdShowHolidayEmpWise.DataSource = null;
                grdShowHolidayEmpWise.DataBind();
                LoadHoliday(monthValue, yearValue, dbname);
            }
            else
            {
                grdShowHoliday.DataSource = null;
                grdShowHoliday.DataBind();
                LoadHolidayEmployeeWise(monthValue, yearValue, dbname);
            }
        }
        else
        {
            grdShowHolidayEmpWise.DataSource = null;
            grdShowHolidayEmpWise.DataBind();
            grdShowHoliday.DataSource = null;
            grdShowHoliday.DataBind();
        }
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        GeneratePeriod(ddlYear.SelectedValue);
    }

    protected void btnSaveHoliday_Click(object sender, EventArgs e)
    {


        HolidaySetup objHolidaySetup = new HolidaySetup();

        objHolidaySetup.ConfigurationType = rblForConfiguration.SelectedItem.Value;

        if (objHolidaySetup.ConfigurationType != "S")
        {

            if (checkentry_for_multiple() != "")
            {

                MessageBox1.ShowInfo(checkentry_for_multiple());

                return;

            }
        }


        string validationMsg = CheckAllValidation();
        switch (validationMsg)
        {
            case "":
                {
                    string msg = SaveHoliday();
                    //loaddata();

                    DateTime fromdate = Convert.ToDateTime(popupHolidayDate.SelectedDate);
                    
                    HolidayDataEntryAllCompany(fromdate);
                   

                    uncheckcheckboxlist();

                    MessageBox1.ShowSuccess(msg);

                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }

    }

    private int HolidayDataEntryAllCompany(DateTime fromdate)
    {
        int rt = 1;
        try
        {
            var storedProcedureComandTest = "exec [spHolidayUpdateBySystem] '" + fromdate.ToString() + "'";
            StoredProcedureExecutor.StoredProcedureExecuteNonQuery(ConnectionString, storedProcedureComandTest);
            rt = 0;
        }
        catch (Exception ex)
        {
            return rt;
        }

        return rt;

    }

    protected void grdShowHoliday_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        string lblCompanyCode = ((Label)grdShowHoliday.Rows[selectedIndex].FindControl("lblCompanyCode")).Text;
        string lblOfficeLocationCode = ((Label)grdShowHoliday.Rows[selectedIndex].FindControl("lblOfficeLocationCode")).Text;
        string lblShiftId = ((Label)grdShowHoliday.Rows[selectedIndex].FindControl("lblShiftId")).Text;
        string lblHolidayDate = ((Label)grdShowHoliday.Rows[selectedIndex].FindControl("lblHolidayDate")).Text;
        string lblHolidayDescription = ((Label)grdShowHoliday.Rows[selectedIndex].FindControl("lblHolidayDescription")).Text;
        string lblRefNo = ((Label)grdShowHoliday.Rows[selectedIndex].FindControl("lblRefNo")).Text;
        if (e.CommandName.Equals("Delete"))
        {
            try
            {
                DataTable dtCurrentData = ViewState["dtCurrentData"] as DataTable;
                var storedProcedureComandTest = "exec [HolidayDeleteFrom_Hrms_HolidaySetup] '" + lblRefNo + "','" + lblHolidayDate + "','" + lblShiftId + "'";
                StoredProcedureExecutor.StoredProcedureExecuteNonQuery(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureComandTest);

                for (int i = dtCurrentData.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dtCurrentData.Rows[i];
                    if (dr["ReferenceNo"].ToString() == lblRefNo)
                    {
                        dtCurrentData.Rows[i].Delete();
                    }
                }
                dtCurrentData.AcceptChanges();
                grdShowHoliday.DataSource = dtCurrentData;
                grdShowHoliday.DataBind();

                {

                    for (int i = 0; i < chkofficelocation.Items.Count; i++)
                    {

                        chkofficelocation.Items[i].Selected = false;
                    }

                    for (int i = 0; i < chkshift.Items.Count; i++)
                    {

                        chkofficelocation.Items[i].Selected = false;
                    }

                }
            }
            catch (SqlException sqlError)
            {
                ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "MessageBox",
                        "alert(' Error Occured During Operation into Database, Data did not Delete from Database ! ');",
                        true);
            }
            catch (Exception inSystemExep)
            {
                ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "MessageBox",
                        "alert(' Error Occured, Data did not Delete from Database  ! ');",
                        true);
            }
        }
        else if (e.CommandName.Equals("Select"))
        {
            ddlcompany.SelectedValue = lblCompanyCode;

            for (int i = 0; i < chkofficelocation.Items.Count; i++)
            {

                if (chkofficelocation.Items[i].Value == lblOfficeLocationCode)
                {
                    chkofficelocation.Items[i].Selected = true;
                }

                else
                {
                    chkofficelocation.Items[i].Selected = false;

                }
            }


            for (int i = 0; i < chkshift.Items.Count; i++)
            {

                if (chkshift.Items[i].Value == lblShiftId)
                {
                    chkshift.Items[i].Selected = true;

                }

                else
                {
                    chkshift.Items[i].Selected = false;

                }

            }

            popupHolidayDate.SelectedDate = Convert.ToDateTime(lblHolidayDate);
            txtDescription.Text = lblHolidayDescription;
            lblForRefNoForUpdate.Text = lblRefNo;
            //  btnSaveHoliday.Text = "Update";
            GetEnabledFalse();
        }
    }




    private string checkentry_for_multiple()
    {

        string officelocation = "";

        foreach (ListItem lst in chkofficelocation.Items)
        {
            if (lst.Selected)
            {
                if (officelocation == "")
                {
                    officelocation += "" + lst.Value.ToString() + "";
                }
                else
                {
                    officelocation += "," + lst.Value.ToString() + "";
                }
            }
        }

        if (officelocation == "") return "Please check Office Location";

        string[] offlocacount = officelocation.Split(',');
        if (offlocacount.Length > 1)
        {
            return "Please check  only  one office loaction ";
        }



        string shift = "";

        foreach (ListItem lst in chkshift.Items)
        {
            if (lst.Selected)
            {
                if (shift == "")
                {
                    shift += "" + lst.Value.ToString() + "";
                }
                else
                {
                    shift += "," + lst.Value.ToString() + "";
                }
            }
        }

        if (shift == "") return "Please check Shift";
        string[] shiftcount = shift.Split(',');
        if (shiftcount.Length > 1) return "Please check  only  one Shift ";


        return "";

    }



    private string checkentry()
    {
        string officelocation = "";

        foreach (ListItem lst in chkofficelocation.Items)
        {
            if (lst.Selected)
            {
                if (officelocation == "")
                {
                    officelocation += "" + lst.Value.ToString() + "";
                }
                else
                {
                    officelocation += "," + lst.Value.ToString() + "";
                }
            }
        }

        if (officelocation == "") return "Please check Office Location";


        string shift = "";

        foreach (ListItem lst in chkshift.Items)
        {
            if (lst.Selected)
            {
                if (shift == "")
                {
                    shift += "" + lst.Value.ToString() + "";
                }
                else
                {
                    shift += "," + lst.Value.ToString() + "";
                }
            }
        }

        if (shift == "") return "Please check Shift";

        return "";

    }


    private void uncheckcheckboxlist()
    {

        for (int i = 0; i < chkofficelocation.Items.Count; i++)
        {

            chkofficelocation.Items[i].Selected = false;
        }

        for (int i = 0; i < chkshift.Items.Count; i++)
        {

            chkshift.Items[i].Selected = false;
        }

    }

    protected void grdShowHoliday_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void grdShowHoliday_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[2].Visible = false;
        e.Row.Cells[4].Visible = false;
        e.Row.Cells[6].Visible = false;
    }

    protected void rblForConfiguration_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcompany.SelectedValue != "")
        {
            string periodValue = ddlPeriod.SelectedValue;

            //txtDescription_AutoCompleteExtender.ContextKey = Session[GlobalData.sessionConnectionstring].ToString() + ":" + rblForConfiguration.SelectedValue.ToString();
            if (rblForConfiguration.SelectedValue == "S")
            {
                txtEmployeeCode.Enabled = false;

                uncheckcheckboxlist();

                grdShowHolidayEmpWise.DataSource = null;
                grdShowHolidayEmpWise.DataBind();


                if (periodValue != "-1")
                {

                    int yearValue = Convert.ToInt32(periodValue.Split(':')[1].Trim());
                    int monthValue = Convert.ToInt32(periodValue.Split(':')[0].Trim());
                    LoadHoliday(monthValue, yearValue);
                }


            }
            else
            {
                txtEmployeeCode.Enabled = true;

                if (periodValue != "-1")
                {

                    int yearValue = Convert.ToInt32(periodValue.Split(':')[1].Trim());
                    int monthValue = Convert.ToInt32(periodValue.Split(':')[0].Trim());

                    uncheckcheckboxlist();

                    uncheckcheckboxlist();
                    grdShowHoliday.DataSource = null;
                    grdShowHoliday.DataBind();

                    LoadHolidayEmployeeWise(monthValue, yearValue);
                }


            }

        }
    }

    protected void grdShowHolidayEmpWise_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void grdShowHolidayEmpWise_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        string lblCompanyCode = ((Label)grdShowHolidayEmpWise.Rows[selectedIndex].FindControl("lblCompanyCode")).Text;
        string lblOfficeLocationCode = ((Label)grdShowHolidayEmpWise.Rows[selectedIndex].FindControl("lblOfficeLocationCode")).Text;
        string lblShiftId = ((Label)grdShowHolidayEmpWise.Rows[selectedIndex].FindControl("lblShiftId")).Text;
        string lblHolidayDate = ((Label)grdShowHolidayEmpWise.Rows[selectedIndex].FindControl("lblHolidayDate")).Text;
        string lblHolidayDescription = ((Label)grdShowHolidayEmpWise.Rows[selectedIndex].FindControl("lblHolidayDescription")).Text;
        string lblRefNo = ((Label)grdShowHolidayEmpWise.Rows[selectedIndex].FindControl("lblRefNo")).Text;
        string lblEmployeeID = ((Label)grdShowHolidayEmpWise.Rows[selectedIndex].FindControl("lblEmployeeID")).Text;

        if (e.CommandName.Equals("Delete"))
        {
            try
            {
                DataTable dtCurrentData = ViewState["dtCurrentData"] as DataTable;
                var storedProcedureComandTest = "exec [HolidayDeleteFrom_Hrms_HolidaySetupEmpWise] '" + lblRefNo + "','" + lblHolidayDate + "','" + lblEmployeeID + "'";
                StoredProcedureExecutor.StoredProcedureExecuteNonQuery(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureComandTest);
                for (int i = dtCurrentData.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dtCurrentData.Rows[i];
                    if (dr["ReferenceNo"].ToString() == lblRefNo)
                    {
                        dtCurrentData.Rows[i].Delete();
                    }
                }
                dtCurrentData.AcceptChanges();
                grdShowHolidayEmpWise.DataSource = dtCurrentData;
                grdShowHolidayEmpWise.DataBind();
                btnSaveHoliday.Text = "Save";

            }
            catch (SqlException sqlError)
            {
                ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "MessageBox",
                        "alert(' Error Occured During Operation into Database, Data did not Delete from Database ! ');",
                        true);
            }
            catch (Exception inSystemExep)
            {
                ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "MessageBox",
                        "alert(' Error Occured, Data did not Delete from Database  ! ');",
                        true);
            }
        }
        else if (e.CommandName.Equals("Select"))
        {
            ddlcompany.SelectedValue = lblCompanyCode;
            popupHolidayDate.SelectedDate = Convert.ToDateTime(lblHolidayDate);
            txtDescription.Text = lblHolidayDescription;
            txtEmployeeCode.Text = lblEmployeeID;
            lblForRefNoForUpdate.Text = lblRefNo;
            txtEmployeeCode.Enabled = false;
            btnSaveHoliday.Text = "Update";
            GetEnabledFalse();

            for (int i = 0; i < chkofficelocation.Items.Count; i++)
            {

                if (chkofficelocation.Items[i].Value == lblOfficeLocationCode)
                {
                    chkofficelocation.Items[i].Selected = true;
                }

                else
                {
                    chkofficelocation.Items[i].Selected = false;

                }
            }


            for (int i = 0; i < chkshift.Items.Count; i++)
            {

                if (chkshift.Items[i].Value == lblShiftId)
                {
                    chkshift.Items[i].Selected = true;

                }

                else
                {
                    chkshift.Items[i].Selected = false;

                }

            }


        }

    }

    protected void grdShowHolidayEmpWise_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[2].Visible = false;
        e.Row.Cells[4].Visible = false;
        e.Row.Cells[6].Visible = false;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAllControlForClearBtn();
        uncheckcheckboxlist();
    }

    protected void ddlPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {

        uncheckcheckboxlist();

        string periodValue = ddlPeriod.SelectedValue;
        if (periodValue != "-1")
        {
            int yearValue = Convert.ToInt32(periodValue.Split(':')[1].Trim());
            int monthValue = Convert.ToInt32(periodValue.Split(':')[0].Trim());
            
            if (rblForConfiguration.SelectedValue == "S")
            {
                grdShowHolidayEmpWise.DataSource = null;
                grdShowHolidayEmpWise.DataBind();
                //LoadHoliday(monthValue, yearValue);
            }
            else
            {
                grdShowHoliday.DataSource = null;
                grdShowHoliday.DataBind();
                //LoadHolidayEmployeeWise(monthValue, yearValue);
            }
        }
        else
        {
            grdShowHolidayEmpWise.DataSource = null;
            grdShowHolidayEmpWise.DataBind();
            grdShowHoliday.DataSource = null;
            grdShowHoliday.DataBind();
        }
    }


    #endregion Events
    protected void chkofficelocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string validationMsg = CheckAllValidationOnOfficeLocation();
        //string officelocation = "";

        //switch (validationMsg)
        //{
        //    case "":
        //        {
        //            string periodValue = ddlPeriod.SelectedValue;
        //            var dbname = ddlcompany.SelectedItem.Value;
        //            int yearValue = Convert.ToInt32(periodValue.Split(':')[1].Trim());
        //            int monthValue = Convert.ToInt32(periodValue.Split(':')[0].Trim());

        //            foreach (ListItem lst in chkofficelocation.Items)
        //            {
        //                if (lst.Selected)
        //                {
        //                    if (officelocation == "")
        //                    {
        //                        officelocation += "" + lst.Value.ToString() + "";
        //                    }
        //                    else
        //                    {
        //                        officelocation += "," + lst.Value.ToString() + "";
        //                    }
        //                }
        //            }

        //            if (rblForConfiguration.SelectedValue == "S")
        //            {
        //                grdShowHolidayEmpWise.DataSource = null;
        //                grdShowHolidayEmpWise.DataBind();


        //                if (officelocation == "")
        //                {
        //                    //LoadHoliday(monthValue, yearValue, dbname);

        //                }

        //                else
        //                {

        //                    LoadHoliday(monthValue, yearValue, dbname, officelocation);
        //                }


        //            }
        //            else
        //            {


        //                grdShowHoliday.DataSource = null;
        //                grdShowHoliday.DataBind();

        //                if (officelocation == "")
        //                {
        //                    LoadHolidayEmployeeWise(monthValue, yearValue);

        //                }

        //                else
        //                {

        //                    LoadHolidayEmployeeWise(monthValue, yearValue, dbname, officelocation);
        //                }

        //            }
        //        }
        //        break;
        //    default:
        //        grdShowHolidayEmpWise.DataSource = null;
        //        grdShowHolidayEmpWise.DataBind();
        //        grdShowHoliday.DataSource = null;
        //        grdShowHoliday.DataBind();
        //        break;
        //}

    }
    protected void chkshift_SelectedIndexChanged(object sender, EventArgs e)
    {

        string officelocation = "";

        foreach (ListItem lst in chkofficelocation.Items)
        {
            if (lst.Selected)
            {
                if (officelocation == "")
                {
                    officelocation += "" + lst.Value.ToString() + "";
                }
                else
                {
                    officelocation += "," + lst.Value.ToString() + "";
                }
            }
        }


        string shift = "";

        foreach (ListItem lst in chkshift.Items)
        {
            if (lst.Selected)
            {
                if (shift == "")
                {
                    shift += "" + lst.Value.ToString() + "";
                }
                else
                {
                    shift += "," + lst.Value.ToString() + "";
                }
            }
        }



        string periodValue = ddlPeriod.SelectedValue;

        if (periodValue == "-1") return;

        var dbname = ddlcompany.SelectedItem.Value;


        int yearValue = Convert.ToInt32(periodValue.Split(':')[1].Trim());
        int monthValue = Convert.ToInt32(periodValue.Split(':')[0].Trim());
        if (rblForConfiguration.SelectedValue == "S")
        {
            grdShowHolidayEmpWise.DataSource = null;
            grdShowHolidayEmpWise.DataBind();

            if (shift == "")
            {
                LoadHoliday(monthValue, yearValue, dbname, officelocation);
            }

            else if (officelocation == "")
            {
                //LoadHoliday(monthValue, yearValue, dbname);
            }

            else
            {
                LoadHoliday(monthValue, yearValue, dbname, officelocation, shift);
            }

        }
        else
        {

            grdShowHoliday.DataSource = null;
            grdShowHoliday.DataBind();


            if (shift == "")
            {

                LoadHolidayEmployeeWise(monthValue, yearValue, dbname, officelocation);
            }

            else if (officelocation == "")
            {

                //LoadHolidayEmployeeWise(monthValue, yearValue, dbname);
            }

            else
            {

                LoadHolidayEmployeeWise(monthValue, yearValue, dbname, officelocation, shift);
            }
        }

    }

}