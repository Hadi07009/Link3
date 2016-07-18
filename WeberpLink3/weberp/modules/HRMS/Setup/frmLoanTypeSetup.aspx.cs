using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Setup_frmLoanTypeSetup : System.Web.UI.Page
{
    string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        if (!Page.IsPostBack)
        {
            LoadAllLoanType();
        }
    }

    private void LoadAllLoanType()
    {
        string msg = null;
        try
        {
            string storedProcedureCommandTest = "exec [spLoanTypeGetFrom_HRMS_LoanType_Setup] ";
            var dtAllLoanType = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureCommandTest);
            grdLoanType.DataSource = null;
            grdLoanType.DataBind();
            if (dtAllLoanType.Rows.Count > 0)
            {
                grdLoanType.DataSource = dtAllLoanType;
                grdLoanType.DataBind();
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
        txtLoanCode.Text = string.Empty;
        txtLoanTitle.Text = string.Empty;
        btnSave.Text = "Save";
        txtLoanCode.Enabled = true;
    }
    private void SaveLoanType()
    {
        LoanTypeSetup objLoanTypeSetup = new LoanTypeSetup();
        objLoanTypeSetup.LoanCode = txtLoanCode.Text;
        objLoanTypeSetup.LoanTitle = txtLoanTitle.Text;

        var myConnection = new SqlConnection(_connectionString);
        myConnection.Open();
        try
        {
            new SqlCommand("exec [spLoanTypeInitiateInto_HRMS_LoanType_Setup] " +
                            "'" + objLoanTypeSetup.LoanCode + "'," +                           
                            "'" + objLoanTypeSetup.LoanTitle + "';", myConnection)
                            .ExecuteNonQuery();
            ClearAllControl();
            LoadAllLoanType();
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
        if (txtLoanCode.Text == string.Empty)
        {
            txtLoanCode.Focus();
            return "Please type loan code correctly !";
        }
        if (txtLoanTitle.Text == string.Empty)
        {
            txtLoanTitle.Focus();
            return "Please type loan title correctly !";
        }
        if (btnSave.Text == "Save")
        {
            DataTable dtLoanType = DataProcess.GetData(_connectionString, Sqlgenerate.SqlGetLoanType(txtLoanCode.Text, txtLoanTitle.Text));
            if (dtLoanType.Rows.Count > 0)
            {
                return "This loan code or title already exist !";
            }
        }
        if (btnSave.Text != "Save")
        {
            DataTable dtLoanType = DataProcess.GetData(_connectionString, Sqlgenerate.SqlGetLoanType(txtLoanTitle.Text));
            if (dtLoanType.Rows.Count > 0)
            {
                txtLoanTitle.Focus();
                return "This loan title already exist !";
            }
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
                    SaveLoanType();
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }
    }
    protected void grdLoanType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        var lblLoanCode = ((Label)grdLoanType.Rows[selectedIndex].FindControl("lblLoanCode")).Text;
        if (e.CommandName.Equals("Delete"))
        {
            string msg = null;
            try
            {
                DataProcess.DeleteQuery(_connectionString, Sqlgenerate.SqlDeleteLoanType(lblLoanCode));
                LoadAllLoanType();
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
            var lblLoanTitle = ((Label)grdLoanType.Rows[selectedIndex].FindControl("lblLoanTitle")).Text;
            txtLoanCode.Text = lblLoanCode;
            txtLoanTitle.Text = lblLoanTitle;
            btnSave.Text = "Update";
            txtLoanCode.Enabled = false;
        }
    }
    protected void grdLoanType_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAllControl();
    }
}