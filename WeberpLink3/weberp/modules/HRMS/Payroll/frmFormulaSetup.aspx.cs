using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Payroll_frmFormulaSetup : System.Web.UI.Page
{
    private const string Rnode = "ac";
    string ConnectionStr = ConfigurationSettings.AppSettings["SCFConnectionString"].Replace("SCF", "SSP");
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        if (!Page.IsPostBack)
        {
            LoadddlOperator();
            LoadAllFormula();
        }
        txtBase_AutoCompleteExtender.ContextKey = ConnectionStr;
        txtMultiplier_AutoCompleteExtender.ContextKey = ConnectionStr;
        txtAccumulationValue_AutoCompleteExtender.ContextKey = ConnectionStr;
    }

    private string SaveFormulaDetails()
    {
        FormulaSetup objFormulaSetup = new FormulaSetup();
        objFormulaSetup.FCode = txtCode.Text == string.Empty ? null : txtCode.Text;
        objFormulaSetup.FName = txtName.Text == string.Empty ? null : txtName.Text;
        objFormulaSetup.FBase = txtBase.Text == string.Empty ? null : txtBase.Text;
        objFormulaSetup.FOperator = ddlOperator.SelectedValue == "-1" ? "" : ddlOperator.SelectedValue;
        objFormulaSetup.FMultiplier = txtMultiplier.Text == string.Empty ? null : txtMultiplier.Text;
        objFormulaSetup.FAccumulation = (chbAccumulation.Checked == true ? "+" : "-");
        objFormulaSetup.FAccumulationValue = txtAccumulationValue.Text == string.Empty ? null : txtAccumulationValue.Text;
        objFormulaSetup.FManualEntry = (chbManualEntry.Checked == true ? "1" : "0");
        objFormulaSetup.TxtTag = btnSave.Text;
        if (objFormulaSetup.TxtTag == "Save")
        {
            return Save(ConnectionStr, objFormulaSetup);
        }
        else
        {
            return Update(ConnectionStr, objFormulaSetup);
        }
    }

    public string Save(string connectionString, FormulaSetup objFormulaSetup)
    {
        string _msg;
        var myConnection = new SqlConnection(connectionString);
        myConnection.Open();
        try
        {
            var myCommand = myConnection.CreateCommand();
            var dtFormula = new DataTable();
            dtFormula = DataProcess.GetData(connectionString, Sqlgenerate.SqlSearchFormulaRecord(objFormulaSetup.FCode));
            if (dtFormula.Rows.Count == 0)
            {
                new SqlCommand("exec [FormulaInitiateInto_HrMs_For_Mas]" +
                                 "'" + objFormulaSetup.FCode + "'," +
                                 "'" + objFormulaSetup.FName + "'," +
                                 "'" + objFormulaSetup.FBase + "'," +
                                 "'" + objFormulaSetup.FOperator + "'," +
                                 "'" + objFormulaSetup.FMultiplier + "'," +
                                 "'" + objFormulaSetup.FAccumulation + "'," +
                                 "'" + objFormulaSetup.FAccumulationValue + "'," +
                                 "" + objFormulaSetup.FManualEntry + "," +
                                 "'" + objFormulaSetup.TxtTag + "';", myConnection)
                                .ExecuteNonQuery();
                _msg = "Data Saved Successfully ";
                ClearAllControl();
                LoadAllFormula();
            }
            else if (dtFormula.Rows.Count > 0)
            {
                _msg = "This Code Already Exit !";
            }
            else
            {
                ClearAllControl();
                LoadAllFormula();
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

    public string Update(string connectionString, FormulaSetup objFormulaSetup)
    {
        string _msg;
        var myConnection = new SqlConnection(connectionString);
        myConnection.Open();
        try
        {
            var myCommand = myConnection.CreateCommand();
            var dtFormula = new DataTable();
            dtFormula = DataProcess.GetData(connectionString, Sqlgenerate.SqlSearchFormulaRecord(objFormulaSetup.FCode));
            if (dtFormula.Rows.Count == 1)
            {
                new SqlCommand("exec [FormulaInitiateInto_HrMs_For_Mas]" +
                                 "'" + objFormulaSetup.FCode + "'," +
                                 "'" + objFormulaSetup.FName + "'," +
                                 "'" + objFormulaSetup.FBase + "'," +
                                 "'" + objFormulaSetup.FOperator + "'," +
                                 "'" + objFormulaSetup.FMultiplier + "'," +
                                 "'" + objFormulaSetup.FAccumulation + "'," +
                                 "'" + objFormulaSetup.FAccumulationValue + "'," +
                                 "" + objFormulaSetup.FManualEntry + "," +
                                 "'" + objFormulaSetup.TxtTag + "';", myConnection)
                                .ExecuteNonQuery();
                _msg = "Data Saved Successfully ";
                ClearAllControl();
                LoadAllFormula();
            }
            else if (dtFormula.Rows.Count == 0)
            {
                btnSave.Text = "Save";
                _msg = "This Code did not found ! So, Please Save Now.";
            }
            else if (dtFormula.Rows.Count == 2)
            {
                _msg = "This Code Already Exit !";
            }
            else
            {
                ClearAllControl();
                LoadAllFormula();
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

    public List<GetOperator> Users()
    {
        return new List<GetOperator> {
            new GetOperator{OperatorValue="-1",OperatorName="--- Please Select ---"}
            ,new GetOperator{OperatorValue="+",OperatorName="+"}
            ,new GetOperator{OperatorValue="-",OperatorName="-"}
            ,new GetOperator{OperatorValue="*",OperatorName="*" }
            ,new GetOperator{OperatorValue="/",OperatorName="/" }
            ,new GetOperator{OperatorValue="%",OperatorName="%" }
            };
    }

    private void LoadddlOperator()
    {
        ddlOperator.DataSource = Users().ToList();
        ddlOperator.DataTextField = "OperatorName";
        ddlOperator.DataValueField = "OperatorValue";
        ddlOperator.DataBind();
    }

    private void ClearAllControl()
    {
        txtCode.Text = string.Empty;
        txtName.Text = string.Empty;
        txtBase.Text = string.Empty;
        ddlOperator.SelectedValue = "-1";
        txtMultiplier.Text = string.Empty;
        chbAccumulation.Checked = false;
        txtAccumulationValue.Text = string.Empty;
        chbManualEntry.Checked = false;
        btnSave.Text = "Save";
    }
    private string checkAllValidation()
    {
        const string checkValidation = "";
        if (txtCode.Text == string.Empty)
        {
            txtCode.Focus();
            return "Please Type Code Correctly !";
        }
        if (txtName.Text == string.Empty)
        {
            txtName.Focus();
            return "Please Type Name Correctly !";
        }
        return checkValidation;
    }

    protected void grdGetSelectedFormula_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void grdGetSelectedFormula_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        string fCode = ((Label)grdGetSelectedFormula.Rows[selectedIndex].FindControl("lblfCode")).Text;
        if (e.CommandName.Equals("Delete"))
        {
            try
            {
                DataProcess.DeleteQuery(ConnectionStr, Sqlgenerate.SqlDeleteFormulaRecord(fCode));
                LoadAllFormula();
            }
            catch (Exception inSystemExep)
            {
                ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "MessageBox",
                        "alert(' Error Occured, Data did not Delete  ! ');",
                        true);
            }
        }
        else if (e.CommandName.Equals("Select"))
        {
            btnSave.Text = "Update";
            string fName = ((Label)grdGetSelectedFormula.Rows[selectedIndex].FindControl("lblfName")).Text;
            string fBase = ((Label)grdGetSelectedFormula.Rows[selectedIndex].FindControl("lblfBase")).Text;
            string fOperator = ((Label)grdGetSelectedFormula.Rows[selectedIndex].FindControl("lblfOperator")).Text;
            string fMultiplier = ((Label)grdGetSelectedFormula.Rows[selectedIndex].FindControl("lblfMultiplier")).Text;
            string fAccumulation = ((Label)grdGetSelectedFormula.Rows[selectedIndex].FindControl("lblfAccumulation")).Text;
            string fAccumulationValue = ((Label)grdGetSelectedFormula.Rows[selectedIndex].FindControl("lblfAccumulationValue")).Text;
            string fManualEntry = ((Label)grdGetSelectedFormula.Rows[selectedIndex].FindControl("lblfManualEntry")).Text;


            txtCode.Text = fCode;
            txtName.Text = fName;
            txtBase.Text = fBase;
            ddlOperator.SelectedValue = fOperator == "" ? "-1" : fOperator;
            txtMultiplier.Text = fMultiplier;
            if (fAccumulation == "+") chbAccumulation.Checked = true;
            else chbAccumulation.Checked = false;
            txtAccumulationValue.Text = fAccumulationValue;
            if (fManualEntry == "1") chbManualEntry.Checked = true;
            else chbManualEntry.Checked = false;
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string validationMsg = checkAllValidation();
        switch (validationMsg)
        {
            case "":
                {

                    string msg = SaveFormulaDetails();
                    MessageBox1.ShowSuccess(msg);
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }

    }

    private void LoadAllFormula()
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [FormulaGetAllFrom_HrMs_For_Mas] ";
            var dtFormula = StoredProcedureExecutor.StoredProcedureExecuteReader(ConnectionStr, storedProcedureCommandTest);
            grdGetSelectedFormula.DataSource = null;
            grdGetSelectedFormula.DataBind();
            if (dtFormula.Rows.Count > 0)
            {
                grdGetSelectedFormula.DataSource = dtFormula;
                grdGetSelectedFormula.DataBind();
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
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAllControl();
    }
    protected void txtBase_TextChanged(object sender, EventArgs e)
    {
        if (txtBase.Text.Trim() != "")
        {
            if (txtBase.Text.Trim().Split(':').Length > 1)
            {
                txtBase.Text=txtBase.Text.Trim().Split(':')[0].ToString(); 
            }
        }
    }
}
public class GetOperator
{
    public string OperatorValue { get; set; }
    public string OperatorName { get; set; }
}