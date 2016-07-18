using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Setup_frmAdvanceTypeSetup : System.Web.UI.Page
{
    private const string Rnode = "K";
    string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        if (!Page.IsPostBack)
        {
            LoadCompanyByUserPermission("ADM", Rnode);
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
        LoadAdvanceType();
    }

    private string SaveAdvanceType()
    {
        AdvanceTypeSetup objAdvanceTypeSetup = new AdvanceTypeSetup();
        objAdvanceTypeSetup.CompanyCode = ddlcompany.SelectedValue;
        objAdvanceTypeSetup.AdvanceCode = txtAdvanceCode.Text;
        objAdvanceTypeSetup.AdvanceName = txtAdvanceName.Text;
        objAdvanceTypeSetup.MinimumAmount = Convert.ToDouble(txtMinimumAmount.Text == string.Empty ? null : txtMinimumAmount.Text);
        objAdvanceTypeSetup.MaximumAmount = Convert.ToDouble(txtMaximumAmount.Text == string.Empty ? null : txtMaximumAmount.Text);
        objAdvanceTypeSetup.ModeOfPayment = ddlModeOfPayment.SelectedValue;
        objAdvanceTypeSetup.Frequency = Convert.ToInt32(txtFrequency.Text == string.Empty ? null : txtFrequency.Text);
        objAdvanceTypeSetup.TxtTag = btnSaveAdvanceType.Text;
        if (objAdvanceTypeSetup.TxtTag == "Save")
        {
            return Save(Session[GlobalData.sessionConnectionstring].ToString(), objAdvanceTypeSetup);
        }
        else
        {
            return Update(Session[GlobalData.sessionConnectionstring].ToString(), objAdvanceTypeSetup);
        }
    }

    public string Save(string connectionString, AdvanceTypeSetup objAdvanceTypeSetup)
    {
        string _msg;
        try
        {
            var dtAdvanceType = DataProcess.GetData(connectionString, Sqlgenerate.SqlGetAdvanceTypeRecord(objAdvanceTypeSetup.CompanyCode, objAdvanceTypeSetup.AdvanceCode));
            if (dtAdvanceType.Rows.Count == 0)
            {
                var storedProcedureComandTest = "exec [AdvanceTypeInitiateInto_HRMS_AdvanceTypeSetup] '" + objAdvanceTypeSetup.CompanyCode + "','" + objAdvanceTypeSetup.AdvanceCode + "','" + objAdvanceTypeSetup.AdvanceName + "'," + objAdvanceTypeSetup.MinimumAmount + "," + objAdvanceTypeSetup.MaximumAmount + ",'" + objAdvanceTypeSetup.ModeOfPayment + "'," + objAdvanceTypeSetup.Frequency + ",'" + objAdvanceTypeSetup.TxtTag + "'";
                StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);
                _msg = "Data Saved Successfully ";
                ClearAllControl();
                LoadAdvanceType();
            }
            else if (dtAdvanceType.Rows.Count > 0)
            {
                _msg = "This Advance Type Code Of This Selected Company Already Exist !";
            }
            else
            {
                ClearAllControl();
                LoadAdvanceType();
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
        return _msg;
    }

    public string Update(string connectionString, AdvanceTypeSetup objAdvanceTypeSetup)
    {
        string _msg;
        try
        {
            var dtAdvanceType = DataProcess.GetData(connectionString, Sqlgenerate.SqlGetAdvanceTypeRecord(objAdvanceTypeSetup.CompanyCode,objAdvanceTypeSetup.AdvanceCode));
            if (dtAdvanceType.Rows.Count == 1)
            {
                var storedProcedureComandTest = "exec [AdvanceTypeInitiateInto_HRMS_AdvanceTypeSetup] '" + objAdvanceTypeSetup.CompanyCode + "','" + objAdvanceTypeSetup.AdvanceCode + "','" + objAdvanceTypeSetup.AdvanceName + "'," + objAdvanceTypeSetup.MinimumAmount + "," + objAdvanceTypeSetup.MaximumAmount + ",'" + objAdvanceTypeSetup.ModeOfPayment + "'," + objAdvanceTypeSetup.Frequency + ",'" + objAdvanceTypeSetup.TxtTag + "'";
                StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);
                _msg = "Data Saved Successfully ";
                ClearAllControl();
                LoadAdvanceType();
            }
            else if (dtAdvanceType.Rows.Count == 0)
            {
                btnSaveAdvanceType.Text = "Save";
                _msg = "This Advance Type Code Of This Selected Company did not found ! So, Please Save Now.";
            }
            else
            {
                ClearAllControl();
                LoadAdvanceType();
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
        if (txtAdvanceCode.Text == string.Empty)
        {
            txtAdvanceCode.Focus();
            return "Please Enter Advance Code Correctly !";
        }
        if (txtAdvanceName.Text == string.Empty)
        {
            txtAdvanceName.Focus();
            return "Please Enter Advance Name Correctly !";
        }
        if (ddlModeOfPayment.SelectedValue == "-1")
        {
            ddlModeOfPayment.Focus();
            return "Please Select Mode of Payment Correctly !";
        }
        return checkValidation;
    }

    private void ClearAllControl()
    {
        txtAdvanceCode.Text = string.Empty;
        txtAdvanceName.Text = string.Empty;
        txtMaximumAmount.Text = string.Empty;
        txtMinimumAmount.Text = string.Empty;
        ddlModeOfPayment.SelectedValue = "-1";
        txtFrequency.Text = string.Empty;
        btnSaveAdvanceType.Text = "Save";
        txtAdvanceCode.Enabled = true;
        ddlcompany.Enabled = true;
        ClsDropDownListController.EnableDisableDropDownList(ddlcompany);
    }

    private void LoadAdvanceType()
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [AdvanceTypeGetAllFrom_HRMS_AdvanceTypeSetup] ";
            var dtAdvanceType = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandTest);
            grdShowAdvanceType.DataSource = null;
            grdShowAdvanceType.DataBind();
            if (dtAdvanceType.Rows.Count > 0)
            {
                grdShowAdvanceType.DataSource = dtAdvanceType;
                grdShowAdvanceType.DataBind();
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

    protected void btnSaveAdvanceType_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckAllValidation();
        switch (validationMsg)
        {
            case "":
                {

                    string msg = SaveAdvanceType();
                    MessageBox1.ShowSuccess(msg);

                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }

    }

    protected void grdShowAdvanceType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        string lblCompanyCode = ((Label)grdShowAdvanceType.Rows[selectedIndex].FindControl("lblCompanyCode")).Text;
        string lblAdvanceCode = ((Label)grdShowAdvanceType.Rows[selectedIndex].FindControl("lblAdvanceCode")).Text;
        if (e.CommandName.Equals("Delete"))
        {
            string _msg = null;
            try
            {
                DataProcess.DeleteQuery(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlDeleteAdvanceTypeRecord(lblCompanyCode, lblAdvanceCode));
                LoadAdvanceType();
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
            string lblAdvanceName = ((Label)grdShowAdvanceType.Rows[selectedIndex].FindControl("lblAdvanceName")).Text;
            string lblMinimumAmount = ((Label)grdShowAdvanceType.Rows[selectedIndex].FindControl("lblMinimumAmount")).Text;
            string lblMaximumAmount = ((Label)grdShowAdvanceType.Rows[selectedIndex].FindControl("lblMaximumAmount")).Text;
            string lblModeOfPaymentCode = ((Label)grdShowAdvanceType.Rows[selectedIndex].FindControl("lblModeOfPaymentCode")).Text;
            string lblFrequency = ((Label)grdShowAdvanceType.Rows[selectedIndex].FindControl("lblFrequency")).Text;
            ddlcompany.SelectedValue = lblCompanyCode;
            txtAdvanceCode.Text = lblAdvanceCode;
            txtAdvanceName.Text = lblAdvanceName;
            txtMinimumAmount.Text = lblMinimumAmount;
            txtMaximumAmount.Text = lblMaximumAmount;
            ddlModeOfPayment.SelectedValue = lblModeOfPaymentCode;
            txtFrequency.Text = lblFrequency;
            btnSaveAdvanceType.Text = "Update";
            txtAdvanceCode.Enabled = false;
            ddlcompany.Enabled = false;
        }

    }
    protected void grdShowAdvanceType_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void grdShowAdvanceType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[7].Visible = false;
    }
    
}