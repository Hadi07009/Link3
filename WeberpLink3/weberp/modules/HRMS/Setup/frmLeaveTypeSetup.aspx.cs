using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Setup_frmLeaveTypeSetup : System.Web.UI.Page
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
        LoadLeaveType();
    }
    
    private string SaveLeaveType()
    {
        LeaveTypeSetup objLeaveTypeSetup = new LeaveTypeSetup();
        objLeaveTypeSetup.CompanyCode = ddlcompany.SelectedValue;
        objLeaveTypeSetup.LeaveCode = txtLeaveCode.Text;
        objLeaveTypeSetup.LeaveName = txtLeaveName.Text;
        objLeaveTypeSetup.ModeOfPayment = rblModeOfPayment.SelectedValue;
        objLeaveTypeSetup.MaximumPerAllow = Convert.ToInt32(txtMaximumPerAllow.Text == string.Empty ? null : txtMaximumPerAllow.Text);
        objLeaveTypeSetup.EmployeeType = ddlEmployeeType.SelectedValue;
        objLeaveTypeSetup.CarryForwordNextYear = rblCarryForword.SelectedValue;
        objLeaveTypeSetup.MaximumLeaveCarryForwordToNextYear = Convert.ToInt32(txtMaximumLeaveCarryForword.Text == string.Empty ? null : txtMaximumLeaveCarryForword.Text);
        objLeaveTypeSetup.TxtTag = btnSaveLeaveType.Text;
        objLeaveTypeSetup.EmployeeGender = rblForGender.SelectedValue;
        if (objLeaveTypeSetup.TxtTag == "Save")
        {
            return Save(Session[GlobalData.sessionConnectionstring].ToString(), objLeaveTypeSetup);
        }
        else 
        {
            return Update(Session[GlobalData.sessionConnectionstring].ToString(), objLeaveTypeSetup);
            
        }
    }
    public string Save(string connectionString, LeaveTypeSetup objLeaveTypeSetup)
    {
        string _msg;
        try
        {
            var dtLeaveType = DataProcess.GetData(connectionString, Sqlgenerate.SqlGetLeaveTypeRecord(objLeaveTypeSetup.CompanyCode, objLeaveTypeSetup.LeaveCode, objLeaveTypeSetup.EmployeeType));
            if (dtLeaveType.Rows.Count == 0)
            {
                var storedProcedureComandTest = "exec [LeaveTypeInitiateInto_HRMS_Leave_Mas] '" + objLeaveTypeSetup.CompanyCode + "','" + objLeaveTypeSetup.LeaveCode + "','" + objLeaveTypeSetup.LeaveName + "'," + objLeaveTypeSetup.MaximumPerAllow + ",'" + objLeaveTypeSetup.ModeOfPayment + "','" + objLeaveTypeSetup.CarryForwordNextYear + "'," + objLeaveTypeSetup.MaximumLeaveCarryForwordToNextYear + ",'" + objLeaveTypeSetup.EmployeeType + "','"+objLeaveTypeSetup.EmployeeGender +"','"+ objLeaveTypeSetup.TxtTag + "'";
                StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);
                _msg = "Data Saved Successfully ";
                ClearAllControl();
                LoadLeaveType();
            }
            else if (dtLeaveType.Rows.Count > 0)
            {
                _msg = "This Leave Type Code Of This Selected Company Already Exist !";
            }
            else
            {
                ClearAllControl();
                LoadLeaveType();
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

    public string Update(string connectionString, LeaveTypeSetup objLeaveTypeSetup)
    {
        string _msg;
        try
        {
            var dtLeaveType = DataProcess.GetData(connectionString, Sqlgenerate.SqlGetLeaveTypeRecord(objLeaveTypeSetup.CompanyCode, objLeaveTypeSetup.LeaveCode, objLeaveTypeSetup.EmployeeType));
            if (dtLeaveType.Rows.Count == 1)
            {
                var storedProcedureComandTest = "exec [LeaveTypeInitiateInto_HRMS_Leave_Mas] '" + objLeaveTypeSetup.CompanyCode + "','" + objLeaveTypeSetup.LeaveCode + "','" + objLeaveTypeSetup.LeaveName + "'," + objLeaveTypeSetup.MaximumPerAllow + ",'" + objLeaveTypeSetup.ModeOfPayment + "','" + objLeaveTypeSetup.CarryForwordNextYear + "'," + objLeaveTypeSetup.MaximumLeaveCarryForwordToNextYear + ",'" + objLeaveTypeSetup.EmployeeType + "','"+objLeaveTypeSetup.EmployeeGender+"','" + objLeaveTypeSetup.TxtTag + "'";
                StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);
                _msg = "Data Saved Successfully ";
                ClearAllControl();
                LoadLeaveType();
            }
            else if (dtLeaveType.Rows.Count == 0)
            {
                btnSaveLeaveType.Text = "Save";
                _msg = "This Leave Type Code Of This Selected Company did not found ! So, Please Save Now.";
            }
            else
            {
                ClearAllControl();
                LoadLeaveType();
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
        if(txtLeaveCode.Text == string.Empty)
        {
            txtLeaveCode.Focus();
            return "Please Enter Leave Code Correctly !";
        }
        if(txtLeaveName.Text == string.Empty)
        {
            txtLeaveName.Focus();
            return "Please Enter Leave Name Correctly !";
        }
        if(ddlEmployeeType.SelectedValue == "-1")
        {
            ddlEmployeeType.Focus();
            return "Please Select Employee Type Correctly !";
        }
        return checkValidation;
    }

    private void ClearAllControl()
    {
        txtLeaveCode.Text = string.Empty;
        txtLeaveName.Text = string.Empty;
        txtMaximumLeaveCarryForword.Text = string.Empty;
        txtMaximumPerAllow.Text = string.Empty;
        rblCarryForword.SelectedValue = "Y";
        rblModeOfPayment.SelectedValue = "FP";
        ddlEmployeeType.SelectedValue = "-1";
        btnSaveLeaveType.Text = "Save";
        txtLeaveCode.Enabled = true;
        ddlcompany.Enabled = true;
        rblForGender.SelectedValue = "B";
        ClsDropDownListController.EnableDisableDropDownList(ddlcompany);
    }

    private void LoadLeaveType()
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [LeaveTypeGetAllFrom_HRMS_Leave_Mas] ";
            var dtLeaveType = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandTest);
            grdShowLeaveType.DataSource = null;
            grdShowLeaveType.DataBind();
            if (dtLeaveType.Rows.Count > 0)
            {
                grdShowLeaveType.DataSource = dtLeaveType;
                grdShowLeaveType.DataBind();
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
            if(_msg != null)
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' "+ _msg+" ');",
                    true);
            }
 
        }
    }

    protected void btnSaveLeaveType_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckAllValidation();
        switch (validationMsg)
        {
            case "":
                {

                    string msg = SaveLeaveType();
                    MessageBox1.ShowSuccess(msg);

                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }

    }
    protected void grdShowLeaveType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        string lblCompanyCode = ((Label)grdShowLeaveType.Rows[selectedIndex].FindControl("lblCompanyCode")).Text;
        string lblLeaveCode = ((Label)grdShowLeaveType.Rows[selectedIndex].FindControl("lblLeaveCode")).Text;
        string lblemptype = ((Label)grdShowLeaveType.Rows[selectedIndex].FindControl("lblEmployeeType")).Text;
        if (e.CommandName.Equals("Delete"))
        {
            string _msg = null;
            try
            {
                DataProcess.DeleteQuery(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlDeleteLeaveTypeRecord(lblCompanyCode, lblLeaveCode, lblemptype));
                LoadLeaveType();
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
            string lblLeaveName = ((Label)grdShowLeaveType.Rows[selectedIndex].FindControl("lblLeaveName")).Text;
            string lblModeOfPaymentCode = ((Label)grdShowLeaveType.Rows[selectedIndex].FindControl("lblModeOfPaymentCode")).Text;
            string lblmaxParAllow = ((Label)grdShowLeaveType.Rows[selectedIndex].FindControl("lblmaxParAllow")).Text;
            string lblEmployeeType = ((Label)grdShowLeaveType.Rows[selectedIndex].FindControl("lblEmployeeType")).Text;
            string lblCarryForwordCode = ((Label)grdShowLeaveType.Rows[selectedIndex].FindControl("lblCarryForwordCode")).Text;
            string lblMaxCarryForword = ((Label)grdShowLeaveType.Rows[selectedIndex].FindControl("lblMaxCarryForword")).Text;
            string lblGenderValue = ((Label)grdShowLeaveType.Rows[selectedIndex].FindControl("lblGenderValue")).Text;

            ddlcompany.SelectedValue = lblCompanyCode;
            txtLeaveCode.Text = lblLeaveCode;
            txtLeaveName.Text = lblLeaveName;
            rblModeOfPayment.SelectedValue = lblModeOfPaymentCode;
            txtMaximumPerAllow.Text = lblmaxParAllow;
            ddlEmployeeType.SelectedValue = lblEmployeeType;
            rblCarryForword.SelectedValue = lblCarryForwordCode;
            txtMaximumLeaveCarryForword.Text = lblMaxCarryForword;
            rblForGender.SelectedValue = lblGenderValue;
            btnSaveLeaveType.Text = "Update";
            txtLeaveCode.Enabled = false;
            ddlcompany.Enabled = false;
        }
    }
    protected void grdShowLeaveType_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void grdShowLeaveType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[6].Visible = false;
        e.Row.Cells[10].Visible = false;
        e.Row.Cells[13].Visible = false;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAllControl();
    }
}