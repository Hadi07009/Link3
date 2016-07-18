using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Setup_frmLateReasonSetup : System.Web.UI.Page
{
    string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        popupFromDate.Attributes.Add("readonly", "readonly");
        popupToDate.Attributes.Add("readonly", "readonly");
        if (!Page.IsPostBack)
        {
            ClsDropDownListController.LoadDropDownList(_connectionString, Sqlgenerate.SqlGetShiftTypeIntoDDL(), ddlShift, "Shift", "Shift Code");
            LoadAllLateReason();
        }
    }

    private void LoadAllLateReason()
    {
        string msg = null;
        try
        {
            string storedProcedureCommandTest = "exec [spLateReasonGet_HRMS_LateReason_Record] ";
            var dtAllLateReason = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureCommandTest);
            grdLateReason.DataSource = null;
            grdLateReason.DataBind();
            if (dtAllLateReason.Rows.Count > 0)
            {
                grdLateReason.DataSource = dtAllLateReason;
                grdLateReason.DataBind();
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

    private void ClearAllControl()
    {
        ddlShift.SelectedValue = "-1";
        popupFromDate.Text = string.Empty;
        popupToDate.Text = string.Empty;
        txtReason.Text = string.Empty;
        btnSave.Text = "Save";
        ddlShift.Enabled = true;
        popupFromDate.Enabled = true;
        popupToDate.Enabled = true;
    }

    private void SaveLateReason(string targateDate)
    {
        LateReasonSetup objLateReasonSetup = new LateReasonSetup();
        objLateReasonSetup.AttendanceDate = targateDate;
        objLateReasonSetup.ShiftCode = ddlShift.SelectedValue;
        objLateReasonSetup.LateReason = txtReason.Text;
        objLateReasonSetup.EntryUserID = current.UserId.ToString();
        var myConnection = new SqlConnection(_connectionString);
        myConnection.Open();
        try
        {
            new SqlCommand("exec [spLateReasonInitiate_HRMS_LateReason_Record] " +
                            "'" + objLateReasonSetup.AttendanceDate + "'," +
                            "'" + objLateReasonSetup.ShiftCode + "'," +
                            "'" + objLateReasonSetup.LateReason + "'," +
                            "'" + objLateReasonSetup.EntryUserID + "';", myConnection)
                            .ExecuteNonQuery();
            MessageBox1.ShowSuccess("Data Saved Successfully ");
        }
        catch (SqlException sqlError)
        {
            MessageBox1.ShowInfo("Error Occured During Operation into Database, Data did not Save into Database !");
        }
        catch (Exception inSystemExep)
        {
            MessageBox1.ShowInfo(" Error Occured, Data did not Save into Database !");
        }
        finally
        {
            myConnection.Close();
        }
    }
    private string CheckAllValidation()
    {
        const string checkValidation = "";
        if (ddlShift.SelectedValue == "-1")
        {
            ddlShift.Focus();
            return "Please select shift correctly !";
        }
        if (popupFromDate.Text == string.Empty)
        {
            popupFromDate.Focus();
            return "Please select date from correctly !";
        }
        if (popupToDate.Text == string.Empty)
        {
            popupToDate.Focus();
            return "Please select date to correctly !";
        }
        if (txtReason.Text == string.Empty)
        {
            txtReason.Focus();
            return "Please type reason correctly !";
        }

        return checkValidation;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        var validationMsg = CheckAllValidation();
        switch (validationMsg)
        {
            case "":
                {
                    DateTime fromDate =  Convert.ToDateTime(popupFromDate.Text);
                    DateTime toDate = Convert.ToDateTime(popupToDate.Text);
                    if (fromDate > toDate)
                    {
                        MessageBox1.ShowWarning(" 'Date To' must be grater than or equal to 'Date From'");
                        return;
                    }

                    while (fromDate <= toDate)
                    {
                        DataTable dtAttendanceCount = DataProcess.GetData(_connectionString, Sqlgenerate.SqlCheckhrms_atnd_det(fromDate, ddlShift.SelectedValue));
                        if (dtAttendanceCount.Rows.Count > 0)
                        {
                            int numberOfRow =  Convert.ToInt32( dtAttendanceCount.Rows[0].ItemArray[0].ToString());
                            if (numberOfRow == 0)
                            {
                                MessageBox1.ShowWarning(" Please entry attendance for  '" + fromDate.ToShortDateString() + "'  date and  '"+ddlShift.SelectedItem.Text+"'  shift, then late reason  ",100,500);
                                return;
                            }
                        }

                        SaveLateReason(fromDate.ToString("dd-MMM-yyyy"));
                        fromDate = fromDate.AddDays(1);
                    }
                    ClearAllControl();
                    LoadAllLateReason();
                    
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }
    }
    protected void grdLateReason_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        var lblattendanceDate = ((Label)grdLateReason.Rows[selectedIndex].FindControl("lblattendanceDate")).Text;
        var lblshiftCode = ((Label)grdLateReason.Rows[selectedIndex].FindControl("lblshiftCode")).Text;
        if (e.CommandName.Equals("Delete"))
        {
            string msg = null;
            try
            {
                DataProcess.DeleteQuery(_connectionString, Sqlgenerate.SqlDeleteLateReason(lblattendanceDate, lblshiftCode));
                LoadAllLateReason();
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
            var lbllateReason = ((Label)grdLateReason.Rows[selectedIndex].FindControl("lbllateReason")).Text;
            popupFromDate.Text = Convert.ToDateTime(lblattendanceDate.ToString()).ToShortDateString();
            popupToDate.Text = Convert.ToDateTime(lblattendanceDate.ToString()).ToShortDateString();
            popupFromDate.Enabled = false;
            popupToDate.Enabled = false;
            txtReason.Text = lbllateReason;
            ddlShift.SelectedValue = lblshiftCode;
            ddlShift.Enabled = false;
            btnSave.Text = "Update";
            
        }
    }
    protected void grdLateReason_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAllControl();
    }
}