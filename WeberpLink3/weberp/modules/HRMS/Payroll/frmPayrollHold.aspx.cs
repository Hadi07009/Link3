using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Payroll_frmPayrollHold : System.Web.UI.Page
{
    string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        if (!Page.IsPostBack)
        {
            PanelForEmployeeDetails.Visible = false;
            PanelEmployeeDetailsTab2.Visible = false;
            txtEmployeeCode_AutoCompleteExtender.ContextKey = ConnectionString;
            txtEmployeeCodeTab2_AutoCompleteExtender.ContextKey = ConnectionString;
            Session["userId"] = "ADM";
            LoadPayrollHoldRecord();
        }
    }

    private void ShowEmployeeDetails(string employeeId)
    {
        string msg = null;
        try
        {
            var storedProcedureCommandText = "exec [Transfer_PromotionGetEmployeeDetails] '" + employeeId + "'";
            var dtEmployeeDetails = StoredProcedureExecutor.StoredProcedureExecuteReader(ConnectionString, storedProcedureCommandText);
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

    private void ShowEmployeeDetailsTab2(string employeeId)
    {
        string msg = null;
        try
        {
            var storedProcedureCommandText = "exec [Transfer_PromotionGetEmployeeDetails] '" + employeeId + "'";
            var dtEmployeeDetails = StoredProcedureExecutor.StoredProcedureExecuteReader(ConnectionString, storedProcedureCommandText);
            PanelEmployeeDetailsTab2.Visible = false;
            if (dtEmployeeDetails.Rows.Count > 0)
            {
                lblNameTab2.Text = dtEmployeeDetails.Rows[0].ItemArray[0].ToString() + " " + dtEmployeeDetails.Rows[0].ItemArray[1].ToString();
                lblJoiningDateTab2.Text = dtEmployeeDetails.Rows[0].ItemArray[2].ToString() == string.Empty ? null : Convert.ToDateTime(dtEmployeeDetails.Rows[0].ItemArray[2].ToString()).ToString("dd-MMM-yyyy");
                lblOfficeLocationTab2.Text = dtEmployeeDetails.Rows[0].ItemArray[3].ToString();
                lblDepartmentTab2.Text = dtEmployeeDetails.Rows[0].ItemArray[4].ToString();
                lblSectionTab2.Text = dtEmployeeDetails.Rows[0].ItemArray[5].ToString();
                lblDesignationTab2.Text = dtEmployeeDetails.Rows[0].ItemArray[6].ToString() == string.Empty ? null : dtEmployeeDetails.Rows[0].ItemArray[6].ToString();
                PanelEmployeeDetailsTab2.Visible = true;
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

    private string CheckAllValidation()
    {
        const string checkValidation = "";
        if (txtEmployeeCode.Text == string.Empty)
        {
            txtEmployeeCode.Focus();
            return "Must Enter Employee Code !";
        }
        return checkValidation;
    }

    private string SavePayrollHold()
    {
        PayrollHold objPayrollHold = new PayrollHold();
        objPayrollHold.EmployeeCode = txtEmployeeCode.Text;
        objPayrollHold.ReasonOfPayrollHold = (txtReasonOfPayrollHold.Text == string.Empty ? null : txtReasonOfPayrollHold.Text);
        objPayrollHold.UserCode = Session["userId"].ToString();
        objPayrollHold.AutoNumberForUpdate = Convert.ToInt32(lblForUpdate.Text == string.Empty ? null : lblForUpdate.Text);
        objPayrollHold.TxtTag = btnHoldPayroll.Text;
        if (objPayrollHold.TxtTag != "UPDATE")
        {
            return Save(ConnectionString, objPayrollHold);
        }
        else
        {
            return Update(ConnectionString, objPayrollHold);
        }
    }

    public string Save(string connectionString, PayrollHold objPayrollHold)
    {
        string _msg;
        var myConnection = new SqlConnection(connectionString);
        myConnection.Open();
        try
        {
            var myCommand = myConnection.CreateCommand();
            var dtPayrollHold = new DataTable();
            dtPayrollHold = DataProcess.GetData(connectionString, Sqlgenerate.SqlGetPayrollHoldRecord(objPayrollHold.EmployeeCode));
            if (dtPayrollHold.Rows.Count == 0)
            {
                new SqlCommand("exec [PayrollHoldInto_Hrms_Salary_Hold] " +
                                 "'" + objPayrollHold.EmployeeCode+ "'," +
                                 "'" + objPayrollHold.ReasonOfPayrollHold + "'," +
                                 "'" + objPayrollHold.UserCode + "'," +
                                 "" + objPayrollHold.AutoNumberForUpdate + "," +                                
                                 "'" + objPayrollHold.TxtTag + "';", myConnection)
                                .ExecuteNonQuery();
                _msg = "Data Saved Successfully ";
                LoadPayrollHoldRecord(objPayrollHold.EmployeeCode);
                LoadPayrollHoldRecord();
                ClearAllControl();
            }
            else if (dtPayrollHold.Rows.Count > 0)
            {
                _msg = " Payroll of This Employee Already Hold !";
            }
            else
            {
                LoadPayrollHoldRecord(objPayrollHold.EmployeeCode);
                LoadPayrollHoldRecord();
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

    public string Update(string connectionString, PayrollHold objPayrollHold)
    {
        string _msg;
        var myConnection = new SqlConnection(connectionString);
        myConnection.Open();
        try
        {
            var myCommand = myConnection.CreateCommand();
            var dtPayrollHold = new DataTable();
            dtPayrollHold = DataProcess.GetData(connectionString, Sqlgenerate.SqlGetPayrollHoldRecord(objPayrollHold.EmployeeCode,objPayrollHold.AutoNumberForUpdate));
            if (dtPayrollHold.Rows.Count == 1)
            {
                new SqlCommand("exec [PayrollHoldInto_Hrms_Salary_Hold] " +
                                 "'" + objPayrollHold.EmployeeCode + "'," +
                                 "'" + objPayrollHold.ReasonOfPayrollHold + "'," +
                                 "'" + objPayrollHold.UserCode + "'," +
                                 "" + objPayrollHold.AutoNumberForUpdate + "," +
                                 "'" + objPayrollHold.TxtTag + "';", myConnection)
                                .ExecuteNonQuery();
                _msg = "Data Saved Successfully ";
                LoadPayrollHoldRecord(objPayrollHold.EmployeeCode);
                LoadPayrollHoldRecord();
                ClearAllControl();
            }
            else if (dtPayrollHold.Rows.Count == 0)
            {
                btnHoldPayroll.Text = "Hold Payroll";
                _msg = "This Data did not found ! So, Please Save Now.";
            }
            else if (dtPayrollHold.Rows.Count == 2)
            {
                _msg = " Payroll of This Employee Already Hold !";
            }
            else
            {
                LoadPayrollHoldRecord(objPayrollHold.EmployeeCode);
                LoadPayrollHoldRecord();
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

    private void ClearAllControl()
    {
        btnHoldPayroll.Text = "Hold Payroll";
        lblForUpdate.Text = string.Empty;
    }

    private void LoadPayrollHoldRecord( string employeeCode)
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [PayrollHoldGetFrom_Hrms_Salary_Hold] '"+employeeCode+"'";
            var dtPayrollHoldRecord = StoredProcedureExecutor.StoredProcedureExecuteReader(ConnectionString, storedProcedureCommandTest);
            grdPayrollHold.DataSource = null;
            grdPayrollHold.DataBind();
            if (dtPayrollHoldRecord.Rows.Count > 0)
            {
                grdPayrollHold.DataSource = dtPayrollHoldRecord;
                grdPayrollHold.DataBind();
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

    private void LoadPayrollHoldRecord()
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [PayrollHoldGetAllFrom_Hrms_Salary_Hold]";
            var dtPayrollHoldRecord = StoredProcedureExecutor.StoredProcedureExecuteReader(ConnectionString, storedProcedureCommandTest);
            grdPayrollHoldAll.DataSource = null;
            grdPayrollHoldAll.DataBind();
            if (dtPayrollHoldRecord.Rows.Count > 0)
            {
                grdPayrollHoldAll.DataSource = dtPayrollHoldRecord;
                grdPayrollHoldAll.DataBind();
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
            LoadPayrollHoldRecord(txtEmployeeCode.Text);
        }
    }

    protected void txtEmployeeCodeTab2_TextChanged(object sender, EventArgs e)
    {
        if (txtEmployeeCodeTab2.Text != string.Empty)
        {
            txtEmployeeCodeTab2.Text = txtEmployeeCodeTab2.Text.Split(':')[0].Trim() == "" ? "" : txtEmployeeCodeTab2.Text.Split(':')[0].Trim();
            ShowEmployeeDetailsTab2(txtEmployeeCodeTab2.Text);
        }
    }

    protected void btnHoldPayroll_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckAllValidation();
        switch (validationMsg)
        {
            case "":
                {
                    string msg = SavePayrollHold();
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

    protected void grdPayrollHold_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        string lblAutono = ((Label)grdPayrollHold.Rows[selectedIndex].FindControl("lblAutono")).Text;
        if (e.CommandName.Equals("Delete"))
        {
            string _msg = null;
            try
            {
                DataProcess.UpdateQuery(ConnectionString, Sqlgenerate.SqlUpdateHoldStatus(Convert.ToInt32( lblAutono)));
                LoadPayrollHoldRecord(txtEmployeeCode.Text);
                LoadPayrollHoldRecord();
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
            string lblEmpcode = ((Label)grdPayrollHold.Rows[selectedIndex].FindControl("lblEmpcode")).Text;
            string lblRemarks = ((Label)grdPayrollHold.Rows[selectedIndex].FindControl("lblRemarks")).Text;
            txtEmployeeCode.Text = lblEmpcode;
            txtReasonOfPayrollHold.Text = lblRemarks;
            lblForUpdate.Text = lblAutono;
            btnHoldPayroll.Text = "UPDATE";
        }
    }

    protected void grdPayrollHold_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void grdPayrollHold_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[4].Visible = false;
    }
}