using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Setup_frmDesignationSetup : System.Web.UI.Page
{
    private const string Rnode = "K";
    string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        txtDesignationCode.Attributes.Add("readonly", "readonly");

        if (!Page.IsPostBack)
        {
            LoadCompanyByUserPermission("ADM", Rnode);
            ClsDropDownListController.EnableDisableDropDownList(ddlcompany);
            ddlcompany_SelectedIndexChanged(sender, e);
            GetMaxDesignationCode();
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

    private void GetMaxDesignationCode()
    {
        var storedProcedureCommandTest = " EXEC DesignationGetMaxCodeHrms_Job_Master ";
        var dtDesignationCode = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandTest);
        if (dtDesignationCode.Rows.Count > 0)
        {
            txtDesignationCode.Text = dtDesignationCode.Rows[0][0].ToString();
        }
    }
    
    protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dbname = ddlcompany.SelectedItem.Value;
        var constr = ConfigurationSettings.AppSettings["SCFConnectionString"].Replace("SCF", dbname);
        Session[GlobalData.sessionConnectionstring] = constr;
        ClsDropDownListController.LoadDropDownList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetEmployeeTypeIntoDDL(), ddlEmployeeType, "EmpType", "EmpTCode");
        ClsDropDownListController.LoadDropDownList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetJobTypeIntoDDL(), ddlJobType, "JobTypeTitle", "JobTypeCode");
        ClsDropDownListController.LoadDropDownList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetMngLevelIntoDDL(), ddlMngLevel, "mngLevelTitle", "mngLevelCode");
        ClsDropDownListController.LoadddlStatus(ddlStatus);
        LoadDesignation();
    }

    private string SaveDesignation()
    {
        DesignationSetup objDesignationSetup = new DesignationSetup();
        objDesignationSetup.DesignationCode = txtDesignationCode.Text;
        objDesignationSetup.Designation = txtDesignation.Text;
        objDesignationSetup.JobType = txtDesignationCode.Text;
        objDesignationSetup.MngLevel = ddlMngLevel.SelectedValue;
        objDesignationSetup.EmployeeType = ddlEmployeeType.SelectedValue;
        objDesignationSetup.TxtTag = btnSaveDesignation.Text;
        objDesignationSetup.TxtStatus = ddlStatus.SelectedValue;
        return Save(Session[GlobalData.sessionConnectionstring].ToString(), objDesignationSetup);
    }

    public string Save(string connectionString, DesignationSetup objDesignationSetup)
    {
        string _msg;
        var myConnection = new SqlConnection(connectionString);
        myConnection.Open();
        try
        {
            var myCommand = myConnection.CreateCommand();
            var dtDesignation = new DataTable();
            myCommand.CommandText = "exec [DesignationGetFromHrms_Job_Master] '" + objDesignationSetup.DesignationCode + "','" + objDesignationSetup.Designation + "'";
            myCommand.ExecuteNonQuery();
            var daDesignation = new SqlDataAdapter(myCommand);
            daDesignation.Fill(dtDesignation);
            if ((dtDesignation.Rows.Count == 0 && objDesignationSetup.TxtTag == "Save") || (dtDesignation.Rows.Count == 1 && objDesignationSetup.TxtTag == "Update"))
            {
                new SqlCommand("exec [DesignationInitiateIntoHrms_Job_Master] " +
                                 "'" + objDesignationSetup.DesignationCode + "'," +
                                 "'" + objDesignationSetup.Designation + "'," +
                                 "'" + objDesignationSetup.JobType + "'," +
                                 "'" + objDesignationSetup.MngLevel + "'," +
                                 "'" + objDesignationSetup.EmployeeType + "'," +
                                 "'" + objDesignationSetup.TxtTag + "'," +
                                 "'" + objDesignationSetup.TxtStatus + "';", myConnection)
                                .ExecuteNonQuery();
                _msg = "Data Saved Successfully ";
                ClearAllControl();
                LoadDesignation();
            }
            else if (dtDesignation.Rows.Count == 0 && objDesignationSetup.TxtTag == "Update")
            {
                btnSaveDesignation.Text = "Save";
                _msg = "This Designation Already Deleted ! So, Please Save Now.";
            }
            else if (dtDesignation.Rows.Count > 0 && objDesignationSetup.TxtTag == "Save")
            {
                _msg = "This Designation Code or Designation Already Exit !";
            }
            else
            {
                ClearAllControl();
                LoadDesignation();
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
        if (ddlEmployeeType.SelectedValue == "-1")
        {
            ddlEmployeeType.Focus();
            return "Please Select Employee Type Correctly !";
        }
        if (ddlMngLevel.SelectedValue == "-1")
        {
            ddlMngLevel.Focus();
            return "Please Select Mng. Level Correctly !";
        }
        //if (ddlJobType.SelectedValue == "-1")
        //{
        //    ddlJobType.Focus();
        //    return "Please Select Job Type Correctly !";
        //}
        if (txtDesignationCode.Text == string.Empty)
        {
            txtDesignationCode.Focus();
            return "Must Enter Designation Code !";
        }
        if (txtDesignation.Text == string.Empty)
        {
            txtDesignation.Focus();
            return "Must Enter Designation !";
        }
        return checkValidation;
    }

    private void ClearAllControl()
    {
        ddlEmployeeType.SelectedValue = "-1";
        ddlMngLevel.SelectedValue = "-1";
        ddlJobType.SelectedValue = "-1";
        //txtDesignationCode.Text = string.Empty;
        txtDesignation.Text = string.Empty;
        btnSaveDesignation.Text = "Save";
        //txtDesignationCode.Enabled = true;
        ddlStatus.SelectedValue = "1";
        ClsDropDownListController.EnableDisableDropDownList(ddlcompany);
        GetMaxDesignationCode();
    }

    private void LoadDesignation()
    {
        var dtDepartment = new DataTable();
        var myConnection = new SqlConnection(Session[GlobalData.sessionConnectionstring].ToString());
        var myCommand = myConnection.CreateCommand();
        myConnection.Open();
        myCommand.CommandText = "exec [DesignationGetAllFromHrms_Job_Master]";
        myCommand.ExecuteNonQuery();
        var daDepartment = new SqlDataAdapter(myCommand);
        daDepartment.Fill(dtDepartment);
        grdShowDesignation.DataSource = null;
        grdShowDesignation.DataBind();
        if (dtDepartment.Rows.Count > 0)
        {
            grdShowDesignation.DataSource = dtDepartment;
            grdShowDesignation.DataBind();
        }
        myConnection.Close();
    }

    protected void btnSaveDesignation_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckAllValidation();
        switch (validationMsg)
        {
            case "":
                {

                    string msg = SaveDesignation();
                    MessageBox1.ShowSuccess(msg);

                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }
    }

    protected void grdShowDesignation_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void grdShowDesignation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        string lblDesignationCode = ((Label)grdShowDesignation.Rows[selectedIndex].FindControl("lblDesignationCode")).Text;
        string lblDesignation = ((Label)grdShowDesignation.Rows[selectedIndex].FindControl("lblDesignation")).Text;
        string lblJobTypeCode = ((Label)grdShowDesignation.Rows[selectedIndex].FindControl("lblJobTypeCode")).Text;
        string lblMngLevel = ((Label)grdShowDesignation.Rows[selectedIndex].FindControl("lblMngLevel")).Text;
        string lblEmpTypeCode = ((Label)grdShowDesignation.Rows[selectedIndex].FindControl("lblEmpTypeCode")).Text;
        if (e.CommandName.Equals("Delete"))
        {
            try
            {
                DataProcess.DeleteQuery(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlDeleteDesignation(lblDesignationCode, lblDesignation));
                ClearAllControl();
                LoadDesignation();
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
            string lblStatusValue = ((Label)grdShowDesignation.Rows[selectedIndex].FindControl("lblStatusValue")).Text;
            ddlJobType.SelectedValue = lblJobTypeCode;          
            ddlMngLevel.SelectedValue = ddlMngLevel.Items.FindByText(lblMngLevel).Value;
            ddlEmployeeType.SelectedValue = lblEmpTypeCode;
            txtDesignationCode.Text = lblDesignationCode;
            txtDesignation.Text = lblDesignation;
            btnSaveDesignation.Text = "Update";
            txtDesignationCode.Enabled = false;
            if (lblStatusValue != "") ddlStatus.SelectedValue = lblStatusValue;
        }
    }

    protected void grdShowDesignation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Button delbutton = (Button)e.Row.Cells[11].Controls[0];
            delbutton.OnClientClick = "if (!confirm('Are you sure you want to delete this record ?')) return;";
        }

        e.Row.Cells[1].Visible = false;
        e.Row.Cells[6].Visible = false;
        e.Row.Cells[7].Visible = false;
        e.Row.Cells[9].Visible = false;
    }

    private void DesignatoinSortWithCondition(string employeeType, string jobType)
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [DesignationSortFromHrms_Job_Master] '" + jobType + "','" + employeeType + "' ";
            var dtDesignation = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandTest);
            grdShowDesignation.DataSource = null;
            grdShowDesignation.DataBind();
            if (dtDesignation.Rows.Count > 0)
            {
                grdShowDesignation.DataSource = dtDesignation;
                grdShowDesignation.DataBind();
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

    protected void ddlEmployeeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DesignatoinSortWithCondition(ddlEmployeeType.SelectedValue, ddlJobType.SelectedValue);
    }
    protected void ddlJobType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DesignatoinSortWithCondition(ddlEmployeeType.SelectedValue, ddlJobType.SelectedValue);
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAllControl();
    }
    protected void btnExporttoExcel_Click(object sender, EventArgs e)
    {
        if (grdShowDesignation.Rows.Count != 0)
        {
            const string type = "DESIGNATION SETUP.xls";
            ExportGridToExcel.Export(type, grdShowDesignation);
        }
        else
        {
            MessageBox1.ShowInfo("There is no data for Export ! ");
        }
    }
}