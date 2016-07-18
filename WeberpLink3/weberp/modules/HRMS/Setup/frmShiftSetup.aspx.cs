using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Setup_frmShiftSetup : System.Web.UI.Page
{
    string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        if (!Page.IsPostBack)
        {
            LoadAllShift();
        }
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

    private void LoadAllShift()
    {
        string msg = null;
        try
        {
            string storedProcedureCommandTest = "exec [spShiftGetFrom_hrms_shift_mas] ";
            var dtAllShift = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureCommandTest);
            grdShift.DataSource = null;
            grdShift.DataBind();
            if (dtAllShift.Rows.Count > 0)
            {
                grdShift.DataSource = dtAllShift;
                grdShift.DataBind();
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

    private void SaveShift()
    {
        string dateForFromTime = DateTime.Now.Date.ToString("dd-MMM-yyyy");
        string dateForToTime = DateTime.Now.Date.ToString("dd-MMM-yyyy");
        if (timeoffOuttime.AmPm.ToString() == "AM")
        {
            dateForToTime = DateTime.Now.Date.AddDays(1).ToString("dd-MMM-yyyy");
        }

        ShiftSetup objShiftSetup = new ShiftSetup();
        objShiftSetup.ShiftCode = txtShiftCode.Text;
        objShiftSetup.ShiftTitle = txtShiftTitle.Text;
        objShiftSetup.FromTime = TimeFormatGenerate(timeoffIntime.Date.Hour.ToString() + ":" + timeoffIntime.Date.Minute.ToString() + ":" + timeoffIntime.AmPm.ToString());
        objShiftSetup.ToTime = TimeFormatGenerate(timeoffOuttime.Date.Hour.ToString() + ":" + timeoffOuttime.Date.Minute.ToString() + ":" + timeoffOuttime.AmPm.ToString());
        DateTime dateInTime = Convert.ToDateTime(dateForFromTime + " " + objShiftSetup.FromTime);
        DateTime dateOutTime = Convert.ToDateTime(dateForToTime + " " + objShiftSetup.ToTime);
        double totalMinutes = DateProcess.GetTotalMinutes(dateInTime, dateOutTime);
        lblHour.Text = DateProcess.TimeDuration(Convert.ToInt32(totalMinutes));
        objShiftSetup.TotalHour = lblHour.Text;
        objShiftSetup.GraceTime = Convert.ToInt32(txtGraceTime.Text);


        
        var myConnection = new SqlConnection(_connectionString);
        myConnection.Open();
        try
        {
            new SqlCommand("exec [spShiftInitiateInto_hrms_shift_mas] " +
                            "'" + objShiftSetup.ShiftCode + "'," +
                            "'" + objShiftSetup.ShiftTitle + "'," +
                            "'" + objShiftSetup.FromTime + "'," +
                            "'" + objShiftSetup.ToTime + "'," +   
                             "'" + objShiftSetup.TotalHour + "',"+
                            "'" + objShiftSetup.GraceTime + "';", myConnection)
                            .ExecuteNonQuery();
            ClearAllControl();
            LoadAllShift();
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
    private string CheckAllValidation()
    {
        const string checkValidation = "";
        if (txtShiftCode.Text == string.Empty)
        {
            txtShiftCode.Focus();
            return "Please type shift code correctly !";
        }


        if (txtShiftTitle.Text == string.Empty)
        {
            txtShiftTitle.Focus();
            return "Please type shift title correctly !";
        }

        if (txtGraceTime.Text == string.Empty)
        {
            txtGraceTime.Focus();
            return "Please type Grace Time !";
        }


        if (btnSave.Text == "Save")
        {
            DataTable dtShift = DataProcess.GetData(_connectionString, Sqlgenerate.SqlGetShift(txtShiftCode.Text, txtShiftTitle.Text));
            if (dtShift.Rows.Count > 0)
            {
                return "This shift code or title already exist !";
            }
        }
        return checkValidation;
    }
    private void ClearAllControl()
    {
        txtShiftCode.Text = string.Empty;
        txtShiftTitle.Text = string.Empty;
        lblHour.Text = string.Empty;
        InTimeFormatSet("09:00 AM".Trim());
        OutTimeFormatSet("06:00 PM".Trim());
        btnSave.Text = "Save";
        txtGraceTime.Text = string.Empty;
        txtShiftCode.Enabled = true;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        var validationMsg = CheckAllValidation();
        switch (validationMsg)
        {
            case "":
                {
                    SaveShift();
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }
    }
    protected void grdShift_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        var lblShiftCode = ((Label)grdShift.Rows[selectedIndex].FindControl("lblShiftCode")).Text;
        if (e.CommandName.Equals("Delete"))
        {
            string msg = null;
            try
            {
                DataProcess.DeleteQuery(_connectionString, Sqlgenerate.SqlDeleteShift(lblShiftCode));
                LoadAllShift();
                
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
            var lblFromTime = ((Label)grdShift.Rows[selectedIndex].FindControl("lblFromTime")).Text;
            var lblToTime = ((Label)grdShift.Rows[selectedIndex].FindControl("lblToTime")).Text;
            var lblTotalHour = ((Label)grdShift.Rows[selectedIndex].FindControl("lblTotalHour")).Text;
            var lblShiftTitle = ((Label)grdShift.Rows[selectedIndex].FindControl("lblShiftTitle")).Text;

            var lblgraceTime = ((Label)grdShift.Rows[selectedIndex].FindControl("lblGraceTime")).Text;


            txtShiftCode.Text = lblShiftCode;
            txtShiftTitle.Text = lblShiftTitle;
            InTimeFormatSet(lblFromTime.Trim());
            OutTimeFormatSet(lblToTime.Trim());
            lblHour.Text = lblTotalHour;
            btnSave.Text = "Update";
            txtShiftCode.Enabled = false;
            txtGraceTime.Text = lblgraceTime;
        }
    }
    protected void grdShift_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAllControl();
    }
}