using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Details_ActiveEmployeeInformation : System.Web.UI.Page
{
    string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    int totalEmployee = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        if (!IsPostBack)
        {
            GetTotalActiveEmployee();
            GetTotalActiveEmployeeDepartmentWise();
            GetTotalActiveEmployeeDesignationWise();
            GetTotalActiveEmployeeOfficeLocationWise();
            PanelForHeader.Visible = false;
            btnSeeMore.Visible = false;
            PanelForDetails.Visible = false;
            ClsDropDownListController.LoadDropDownList(_connectionString, Sqlgenerate.SqlGetSearchArea(), ddlSearchArea, "searchText", "searchID");
            PanelForAgeSearch.Visible = false;
            PanelForSearchDate.Visible = false;
            btnExportDetailsInformation.Visible = false;
            PanelForSection.Visible = false;
            btnExportSearchData.Visible = false;
        }
        GetTotalActiveEmployeeChart();
        GetTotalActiveEmployeeDepartmentChart();
        GetTotalActiveEmployeeDesignationChart();
        GetTotalActiveEmployeeOfficeLocationChart();
    }

    private void GetTotalActiveEmployee()
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [spActiveEmployeeGetTotal] ";
            var dtTotalActiveEmployee = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureCommandTest);
            grdTotalActiveEmployee.DataSource = null;
            grdTotalActiveEmployee.DataBind();
            if (dtTotalActiveEmployee.Rows.Count > 0)
            {
                grdTotalActiveEmployee.DataSource = dtTotalActiveEmployee;
                grdTotalActiveEmployee.DataBind();
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

    private void GetTotalActiveEmployeeDepartmentWise()
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [spActiveEmployeeGetTotalDeptWise] ";
            var dtTotalActiveEmployeeDepartment = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureCommandTest);
            grdDepartment.DataSource = null;
            grdDepartment.DataBind();
            if (dtTotalActiveEmployeeDepartment.Rows.Count > 0)
            {
                grdDepartment.DataSource = dtTotalActiveEmployeeDepartment;
                grdDepartment.DataBind();
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

    private void GetTotalActiveEmployeeChart()
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [spActiveEmployeeGetTotalintoChart] ";
            var dtTotalActiveEmployee = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureCommandTest);
            ChartTotalEmployee.DataSource = null;
            ChartTotalEmployee.DataBind();
            if (dtTotalActiveEmployee.Rows.Count > 0)
            {
                ChartTotalEmployee.Visible = true;
                ChartTotalEmployee.DataSource = dtTotalActiveEmployee;
                ChartTotalEmployee.DataBind();

                ChartTotalEmployee.ChartAreas["ChartArea1"].AxisX.Title = "Gender ";
                ChartTotalEmployee.ChartAreas["ChartArea1"].AxisY.Title = "No of employee";
                ChartTotalEmployee.Series["Team"].XValueMember = "employeeGender";
                ChartTotalEmployee.Series["Team"].YValueMembers = "employeeNumber";
                ChartTotalEmployee.Series["Team"].IsValueShownAsLabel = true;
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

    private void GetTotalActiveEmployeeDepartmentChart()
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [spActiveEmployeeGetTotalDeptWise] ";
            var dtTotalActiveEmployeeDepartment = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureCommandTest);
            ChartDepartmentWise.DataSource = null;
            ChartDepartmentWise.DataBind();
            if (dtTotalActiveEmployeeDepartment.Rows.Count > 0)
            {
                ChartDepartmentWise.Visible = true;
                ChartDepartmentWise.DataSource = dtTotalActiveEmployeeDepartment;
                ChartDepartmentWise.DataBind();

                ChartDepartmentWise.ChartAreas["ChartArea1"].AxisX.Title = "Department ";
                ChartDepartmentWise.ChartAreas["ChartArea1"].AxisY.Title = "No of employee";
                ChartDepartmentWise.Series["Team"].XValueMember = "Dept";
                ChartDepartmentWise.Series["Team"].YValueMembers = "noOfEmployee";
                ChartDepartmentWise.Series["Team"].IsValueShownAsLabel = true;
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

    private void GetTotalActiveEmployeeDesignationWise()
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [spActiveEmployeeGetTotalDesignationWise] ";
            var dtTotalActiveEmployeeDesignation = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureCommandTest);
            grdDesignation.DataSource = null;
            grdDesignation.DataBind();
            if (dtTotalActiveEmployeeDesignation.Rows.Count > 0)
            {
                grdDesignation.DataSource = dtTotalActiveEmployeeDesignation;
                grdDesignation.DataBind();
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

    private void GetTotalActiveEmployeeDesignationChart()
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [spActiveEmployeeGetTotalDesignationWise] ";
            var dtTotalActiveEmployeeDesignation = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureCommandTest);
            ChartDesignation.DataSource = null;
            ChartDesignation.DataBind();
            if (dtTotalActiveEmployeeDesignation.Rows.Count > 0)
            {
                ChartDesignation.Visible = true;
                ChartDesignation.DataSource = dtTotalActiveEmployeeDesignation;
                ChartDesignation.DataBind();

                ChartDesignation.ChartAreas["ChartArea1"].AxisX.Title = "Designation ";
                ChartDesignation.ChartAreas["ChartArea1"].AxisY.Title = "No of employee";
                ChartDesignation.Series["Team"].XValueMember = "Designation";
                ChartDesignation.Series["Team"].YValueMembers = "noOfEmployee";
                ChartDesignation.Series["Team"].IsValueShownAsLabel = true;
                ChartDesignation.ChartAreas["ChartArea1"].AxisX.LabelStyle.Interval = 1;
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

    private void GetTotalActiveEmployeeOfficeLocationWise()
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [spActiveEmployeeGetTotalOfficeLocationWise] ";
            var dtTotalActiveEmployeeOffiLocation = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureCommandTest);
            grdOfficelocation.DataSource = null;
            grdOfficelocation.DataBind();
            if (dtTotalActiveEmployeeOffiLocation.Rows.Count > 0)
            {
                grdOfficelocation.DataSource = dtTotalActiveEmployeeOffiLocation;
                grdOfficelocation.DataBind();
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

    private void GetTotalActiveEmployeeOfficeLocationChart()
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [spActiveEmployeeGetTotalOfficeLocationWise] ";
            var dtTotalActiveEmployeeOffiLocation = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureCommandTest);
            ChartOfficeLocation.DataSource = null;
            ChartOfficeLocation.DataBind();
            if (dtTotalActiveEmployeeOffiLocation.Rows.Count > 0)
            {
                ChartOfficeLocation.Visible = true;
                ChartOfficeLocation.DataSource = dtTotalActiveEmployeeOffiLocation;
                ChartOfficeLocation.DataBind();

                ChartOfficeLocation.ChartAreas["ChartArea1"].AxisX.Title = "Office Location ";
                ChartOfficeLocation.ChartAreas["ChartArea1"].AxisY.Title = "No of employee";
                ChartOfficeLocation.Series["Team"].XValueMember = "Office";
                ChartOfficeLocation.Series["Team"].YValueMembers = "noOfEmployee";
                ChartOfficeLocation.Series["Team"].IsValueShownAsLabel = true;
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

    private void GetTotalActiveEmployeeParticuarDepartment(string departmentCode)
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [spActiveEmployeeGetParticularDept] '" + departmentCode + "'";
            var dtTotalActiveEmployeeDepartment = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureCommandTest);
            grdParticularDepartment.DataSource = null;
            grdParticularDepartment.DataBind();
            btnSeeMore.Visible = false;
            PanelForDetails.Visible = false;
            PanelForSection.Visible = false;
            if (dtTotalActiveEmployeeDepartment.Rows.Count > 0)
            {
                grdParticularDepartment.DataSource = dtTotalActiveEmployeeDepartment;
                grdParticularDepartment.DataBind();
                btnSeeMore.Visible = true;
                PanelForDetails.Visible = true;
                PanelForSection.Visible = true;
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

    private void GetTotalActiveEmployeeParticuarDepartment(string departmentCode, string sectionCode)
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [spActiveEmployeeGetParticularDeptBySection] '" + departmentCode + "','" + sectionCode + "'";
            var dtTotalActiveEmployeeDepartment = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureCommandTest);
            grdParticularDepartment.DataSource = null;
            grdParticularDepartment.DataBind();
            btnSeeMore.Visible = false;
            if (dtTotalActiveEmployeeDepartment.Rows.Count > 0)
            {
                grdParticularDepartment.DataSource = dtTotalActiveEmployeeDepartment;
                grdParticularDepartment.DataBind();
                btnSeeMore.Visible = true;
                PanelForDetails.Visible = true;
                PanelForSection.Visible = true;
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

    private void GetTotalActiveEmployeeParticuarDepartmentEmployee(string departmentCode, string sectionCode)
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [spActiveEmployeeParticularDeptBySectionEmpCount] '" + departmentCode + "','" + sectionCode + "'";
            var dtTotalActiveEmployeeDepartment = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureCommandTest);
            sectionEmployeeNo = 0;
            if (dtTotalActiveEmployeeDepartment.Rows.Count > 0)
            {
                sectionEmployeeNo = Convert.ToInt32(dtTotalActiveEmployeeDepartment.Rows[0][0].ToString());
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

    private void GetTotalActiveEmployeeParticuarDesignation(string designationCode)
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [spActiveEmployeeGetParticularDesignation] '" + designationCode + "'";
            var dtTotalActiveEmployeeDesignation = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureCommandTest);
            grdParticularDesignation.DataSource = null;
            grdParticularDesignation.DataBind();
            btnSeeMore.Visible = false;
            PanelForDetails.Visible = false;
            if (dtTotalActiveEmployeeDesignation.Rows.Count > 0)
            {
                grdParticularDesignation.DataSource = dtTotalActiveEmployeeDesignation;
                grdParticularDesignation.DataBind();
                btnSeeMore.Visible = true;
                PanelForDetails.Visible = true;
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

    private void GetTotalActiveEmployeeParticuarOffice(string officeCode)
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [spActiveEmployeeGetParticularOffice] '" + officeCode + "'";
            var dtTotalActiveEmployeeDesignation = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureCommandTest);
            grdParticularDesignation.DataSource = null;
            grdParticularDesignation.DataBind();
            btnSeeMore.Visible = false;
            if (dtTotalActiveEmployeeDesignation.Rows.Count > 0)
            {
                grdParticularDesignation.DataSource = dtTotalActiveEmployeeDesignation;
                grdParticularDesignation.DataBind();
                btnSeeMore.Visible = true;
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

    private void GetTotalActiveEmployeeDetails()
    {
        string selectionArea = lblSelectionArea.Text;
        string selectionValue = lblSelectionValue.Text;
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [spActiveEmployeeGetDetails] '" + selectionValue + "','" + selectionArea + "'";
            var dtTotalActiveEmployeeDetails = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureCommandTest);
            grdDetails.DataSource = null;
            grdDetails.DataBind();
            btnExportDetailsInformation.Visible = false;
            if (dtTotalActiveEmployeeDetails.Rows.Count > 0)
            {
                grdDetails.DataSource = dtTotalActiveEmployeeDetails;
                grdDetails.DataBind();
                btnExportDetailsInformation.Visible = true;
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

    private void GetTotalActiveEmployeeDetailsDesignationWise()
    {
        string departmentCode = lblSelectionValue.Text.ToString();
        string designstionCode = Session["designationCode"].ToString();
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [spActiveEmployeeGetDetailsDesignationWise] '" + departmentCode + "','" + designstionCode + "'";
            var dtTotalActiveEmployeeDetails = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureCommandTest);
            grdDetails.DataSource = null;
            grdDetails.DataBind();
            btnExportDetailsInformation.Visible = false;
            if (dtTotalActiveEmployeeDetails.Rows.Count > 0)
            {
                grdDetails.DataSource = dtTotalActiveEmployeeDetails;
                grdDetails.DataBind();
                btnExportDetailsInformation.Visible = true;
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
    protected void grdDepartment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int totalNumberOfEmployee = Convert.ToInt32(((Label)e.Row.FindControl("lblNoofEmployee")).Text);
            int lblTotalActiveEmployee = Convert.ToInt32(((Label)grdTotalActiveEmployee.Rows[0].FindControl("lblTotalActiveEmployee")).Text);
            double tempVaue = (100 * totalNumberOfEmployee);
            double getEmpPercentage = Convert.ToDouble((tempVaue / lblTotalActiveEmployee).ToString("N2"));
            ((Label)e.Row.FindControl("lblPercentDepartment")).Text = getEmpPercentage.ToString() + " %";
            totalEmployee += totalNumberOfEmployee;

        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "Total :";
            e.Row.Cells[3].Text = totalEmployee.ToString();
            e.Row.Font.Bold = true;
        }
        e.Row.Cells[1].Visible = false;
    }

    int totalEmployeeDesignation = 0;
    protected void grdDesignation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int totalNumberOfEmployee = Convert.ToInt32(((Label)e.Row.FindControl("lblNoofEmployee")).Text);
            int lblTotalActiveEmployee = Convert.ToInt32(((Label)grdTotalActiveEmployee.Rows[0].FindControl("lblTotalActiveEmployee")).Text);
            double tempVaue = (100 * totalNumberOfEmployee);
            double getEmpPercentage = Convert.ToDouble((tempVaue / lblTotalActiveEmployee).ToString("N2"));
            ((Label)e.Row.FindControl("lblPercentDepartment")).Text = getEmpPercentage.ToString() + " %";
            totalEmployeeDesignation += totalNumberOfEmployee;

        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "Total :";
            e.Row.Cells[3].Text = totalEmployeeDesignation.ToString();
            e.Row.Font.Bold = true;
        }
        e.Row.Cells[1].Visible = false;
    }
    int totalEmployeeOfficeLocation = 0;
    protected void grdOfficelocation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int totalNumberOfEmployee = Convert.ToInt32(((Label)e.Row.FindControl("lblNoofEmployee")).Text);
            int lblTotalActiveEmployee = Convert.ToInt32(((Label)grdTotalActiveEmployee.Rows[0].FindControl("lblTotalActiveEmployee")).Text);
            double tempVaue = (100 * totalNumberOfEmployee);
            double getEmpPercentage = Convert.ToDouble((tempVaue / lblTotalActiveEmployee).ToString("N2"));
            ((Label)e.Row.FindControl("lblPercentDepartment")).Text = getEmpPercentage.ToString() + " %";
            totalEmployeeOfficeLocation += totalNumberOfEmployee;

        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "Total :";
            e.Row.Cells[3].Text = totalEmployeeOfficeLocation.ToString();
            e.Row.Font.Bold = true;
        }
        e.Row.Cells[1].Visible = false;
    }
    private void LoadSectionCode(string departmentCode)
    {
        ClsDropDownListController.LoadDropDownList(_connectionString, Sqlgenerate.SqlGetSectionIntoDDL(departmentCode), ddlSection, "Sect_Name", "Sect_Code");
        ddlSection.Items.RemoveAt(0);
        ddlSection.Items.Insert(0, new ListItem("--- All Section ---", "-1"));
    }
    protected void grdDepartment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Select"))
        {
            lblSelectedCondition.Text = string.Empty;
            lblSelectedEmployeeNumber.Text = string.Empty;
            grdParticularDesignation.DataSource = null;
            grdParticularDesignation.DataBind();
            PanelForParticularOffice.Visible = false;
            lblSelectionValue.Text = string.Empty;
            lblSelectionArea.Text = string.Empty;
            grdDetails.DataSource = null;
            grdDetails.DataBind();
            btnExportDetailsInformation.Visible = false;
            Session["designationCode"] = string.Empty;

            int selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
            string lblDepartmentCode = ((Label)grdDepartment.Rows[selectedIndex].FindControl("lblDepartmentCode")).Text;
            string lblDepartment = ((Label)grdDepartment.Rows[selectedIndex].FindControl("lblDepartment")).Text;
            string lblNoofEmployee = ((Label)grdDepartment.Rows[selectedIndex].FindControl("lblNoofEmployee")).Text;
            lblSelectedCondition.Text = lblDepartment;
            lblSelectedEmployeeNumber.Text = lblNoofEmployee;
            GetTotalActiveEmployeeParticuarDepartment(lblDepartmentCode);
            lblSelectionValue.Text = lblDepartmentCode;
            lblSelectionArea.Text = "DEP";
            PanelForHeader.Visible = true;
            PanelForParticularDept.Visible = true;
            LoadSectionCode(lblDepartmentCode);
        }
    }
    protected void grdParticularDepartment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int lblTotalActiveEmployee = Convert.ToInt32(lblSelectedEmployeeNumber.Text);
        if (sectionEmployeeNo != 0)
        {
            lblTotalActiveEmployee = sectionEmployeeNo;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int totalNumberOfEmployee = Convert.ToInt32(((Label)e.Row.FindControl("lblNoofEmployee")).Text);
            double tempVaue = (100 * totalNumberOfEmployee);
            double getEmpPercentage = Convert.ToDouble((tempVaue / lblTotalActiveEmployee).ToString("N2"));
            ((Label)e.Row.FindControl("lblPercentDepartment")).Text = getEmpPercentage.ToString() + " %";
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "Total :";
            e.Row.Cells[3].Text = lblTotalActiveEmployee.ToString();
            e.Row.Font.Bold = true;
        }
        e.Row.Cells[1].Visible = false;
    }
    protected void grdDesignation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Select"))
        {
            lblSelectedCondition.Text = string.Empty;
            lblSelectedEmployeeNumber.Text = string.Empty;
            grdParticularDepartment.DataSource = null;
            grdParticularDepartment.DataBind();
            PanelForParticularDept.Visible = false;
            lblSelectionValue.Text = string.Empty;
            lblSelectionArea.Text = string.Empty;
            grdDetails.DataSource = null;
            grdDetails.DataBind();
            btnExportDetailsInformation.Visible = false;
            Session["designationCode"] = string.Empty;
            PanelForSection.Visible = false;

            int selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
            string lblDesignationCode = ((Label)grdDesignation.Rows[selectedIndex].FindControl("lblDesignationCode")).Text;
            string lblDesignation = ((Label)grdDesignation.Rows[selectedIndex].FindControl("lblDesignation")).Text;
            string lblNoofEmployee = ((Label)grdDesignation.Rows[selectedIndex].FindControl("lblNoofEmployee")).Text;
            lblSelectedCondition.Text = lblDesignation;
            lblSelectedEmployeeNumber.Text = lblNoofEmployee;
            PanelForHeader.Visible = true;
            lblSelectionValue.Text = lblDesignationCode;
            lblSelectionArea.Text = "DES"; ;
            GetTotalActiveEmployeeParticuarDesignation(lblDesignationCode);
            PanelForParticularOffice.Visible = true;
        }
    }
    protected void grdParticularDesignation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int lblTotalActiveEmployee = Convert.ToInt32(lblSelectedEmployeeNumber.Text);
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int totalNumberOfEmployee = Convert.ToInt32(((Label)e.Row.FindControl("lblNoofEmployee")).Text);
            double tempVaue = (100 * totalNumberOfEmployee);
            double getEmpPercentage = Convert.ToDouble((tempVaue / lblTotalActiveEmployee).ToString("N2"));
            ((Label)e.Row.FindControl("lblPercentDepartment")).Text = getEmpPercentage.ToString() + " %";
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "Total :";
            e.Row.Cells[3].Text = lblTotalActiveEmployee.ToString();
            e.Row.Font.Bold = true;
        }
        e.Row.Cells[1].Visible = false;
    }
    protected void grdOfficelocation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Select"))
        {
            lblSelectedCondition.Text = string.Empty;
            lblSelectedEmployeeNumber.Text = string.Empty;
            grdParticularDepartment.DataSource = null;
            grdParticularDepartment.DataBind();
            PanelForParticularDept.Visible = false;
            lblSelectionValue.Text = string.Empty;
            lblSelectionArea.Text = string.Empty;
            grdDetails.DataSource = null;
            grdDetails.DataBind();
            btnExportDetailsInformation.Visible = false;
            Session["designationCode"] = string.Empty;
            PanelForSection.Visible = false;

            int selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
            string lblOfficeLocationCode = ((Label)grdOfficelocation.Rows[selectedIndex].FindControl("lblOfficeLocationCode")).Text;
            string lblOfficeLocation = ((Label)grdOfficelocation.Rows[selectedIndex].FindControl("lblOfficeLocation")).Text;
            string lblNoofEmployee = ((Label)grdOfficelocation.Rows[selectedIndex].FindControl("lblNoofEmployee")).Text;
            lblSelectedCondition.Text = lblOfficeLocation;
            lblSelectedEmployeeNumber.Text = lblNoofEmployee;
            PanelForHeader.Visible = true;
            lblSelectionValue.Text = lblOfficeLocationCode;
            lblSelectionArea.Text = "OFFLOC";
            GetTotalActiveEmployeeParticuarOffice(lblOfficeLocationCode);
            PanelForParticularOffice.Visible = true;
        }
    }
    protected void btnSeeMore_Click(object sender, EventArgs e)
    {
        Session["designationCode"] = string.Empty;
        GetTotalActiveEmployeeDetails();
    }
    protected void grdParticularDepartment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Select"))
        {
            int selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
            string lblDesignationCode = ((Label)grdParticularDepartment.Rows[selectedIndex].FindControl("lblDesignationCode")).Text;
            Session["designationCode"] = lblDesignationCode;
            GetTotalActiveEmployeeDetailsDesignationWise();
        }
    }
    protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDetails.PageIndex = e.NewPageIndex;
        grdDetails.DataBind();
        if (Session["designationCode"].ToString() == string.Empty)
        {
            GetTotalActiveEmployeeDetails();
        }
        else
        {
            GetTotalActiveEmployeeDetailsDesignationWise();
        }
    }
    protected void grdDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }


    public void Export(string fileName, GridView gv)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader(
            "content-disposition", string.Format("attachment; filename={0}", fileName));
        HttpContext.Current.Response.ContentType = "application/ms-excel";

        using (System.IO.StringWriter sw = new System.IO.StringWriter())
        {
            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            {
                //  Create a table to contain the grid
                System.Web.UI.WebControls.Table table = new System.Web.UI.WebControls.Table();

                //  include the gridline settings
                table.GridLines = gv.GridLines;

                //  add the header row to the table
                if (gv.HeaderRow != null)
                {
                    PrepareControlForExport(gv.HeaderRow);
                    table.Rows.Add(gv.HeaderRow);
                }

                //  add each of the data rows to the table
                foreach (GridViewRow row in gv.Rows)
                {
                    PrepareControlForExport(row);
                    table.Rows.Add(row);
                }

                //  add the footer row to the table
                if (gv.FooterRow != null)
                {
                    PrepareControlForExport(gv.FooterRow);
                    table.Rows.Add(gv.FooterRow);
                }

                //  render the table into the htmlwriter
                table.RenderControl(htw);

                //  render the htmlwriter into the response
                HttpContext.Current.Response.Write(sw.ToString());
                HttpContext.Current.Response.End();
            }
        }
    }

    private static void PrepareControlForExport(Control control)
    {
        for (int i = 0; i < control.Controls.Count; i++)
        {
            Control current = control.Controls[i];
            if (current is LinkButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
            }
            else if (current is ImageButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
            }
            else if (current is HyperLink)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
            }
            else if (current is DropDownList)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
            }
            else if (current is CheckBox)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
            }
            else if (current is Button)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as Button).Text));
            }

            if (current.HasControls())
            {
                PrepareControlForExport(current);
            }
        }
    }
    protected void btnExportDepartment_Click(object sender, EventArgs e)
    {
        if (grdDepartment.Rows.Count != 0)
        {
            string type = "Department wise employee.xls";
            Export(type, grdDepartment);
        }
        else
        {
            string validationMsg = "There is no data for Export ! ";
            ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + validationMsg + "');",
                    true);
        }
    }
    protected void btnExportOfficeLocation_Click(object sender, EventArgs e)
    {
        if (grdOfficelocation.Rows.Count != 0)
        {
            string type = "Office location wise employee.xls";
            Export(type, grdOfficelocation);
        }
        else
        {
            string validationMsg = "There is no data for Export ! ";
            ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + validationMsg + "');",
                    true);
        }
    }
    protected void btnExportDesignation_Click(object sender, EventArgs e)
    {
        if (grdDesignation.Rows.Count != 0)
        {
            string type = "Designation wise employee.xls";
            Export(type, grdDesignation);
        }
        else
        {
            string validationMsg = "There is no data for Export ! ";
            ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + validationMsg + "');",
                    true);
        }

    }

    private string CheckAllValidation()
    {
        const string checkValidation = "";
        if (ddlSearchArea.SelectedValue == "-1")
        {
            ddlSearchArea.Focus();
            return "Please Select Correctly !";
        }
        if (ddlConditionOperator.SelectedValue == "-1")
        {
            ddlConditionOperator.Focus();
            return "Please Select Operator Correctly !";
        }
        return checkValidation;
    }

    private void GetTotalActiveEmployeeByCondition(int searchArea, string conditionalOperator, string searchValue)
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [spActiveEmployeeSearch] " + searchArea + ",'" + conditionalOperator + "','" + searchValue + "'";
            var dtTotalActiveEmployeeDetails = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureCommandTest);
            grdSearchData.DataSource = null;
            grdSearchData.DataBind();
            btnExportSearchData.Visible = false;

            PanelForDetails.Visible = false;
            PanelForSection.Visible = false;
            lblSelectedCondition.Text = string.Empty;
            lblSelectedEmployeeNumber.Text = string.Empty;
            grdParticularDepartment.DataSource = null;
            grdParticularDepartment.DataBind();
            grdParticularDesignation.DataSource = null;
            grdParticularDesignation.DataBind();
            btnSeeMore.Visible = false;
            grdDetails.DataSource = null;
            grdDetails.DataBind();
            btnExportDetailsInformation.Visible = false;

            if (dtTotalActiveEmployeeDetails.Rows.Count > 0)
            {
                grdSearchData.DataSource = dtTotalActiveEmployeeDetails;
                grdSearchData.DataBind();
                btnExportSearchData.Visible = true;
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        string validationMsg = CheckAllValidation();
        switch (validationMsg)
        {
            case "":
                {
                    int searchArea = Convert.ToInt32(ddlSearchArea.SelectedValue);
                    string conditionalOperator = ddlConditionOperator.SelectedValue;
                    string searchValue = null;
                    if (searchArea == 1)
                    {
                        searchValue = txtForSearchAge.Text;
                    }
                    else
                    {
                        searchValue = Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy");
                    }
                    GetTotalActiveEmployeeByCondition(searchArea, conditionalOperator, searchValue);
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }
    }
    protected void ddlSearchArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchArea.SelectedValue == "-1")
        {
            PanelForAgeSearch.Visible = false;
            PanelForSearchDate.Visible = false;

        }
        else if (ddlSearchArea.SelectedValue == "1")
        {
            PanelForAgeSearch.Visible = true;
            PanelForSearchDate.Visible = false;
        }
        else
        {
            PanelForAgeSearch.Visible = false;
            PanelForSearchDate.Visible = true;
        }
    }

    private void GetTotalActiveEmployeeDetailsInformation()
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [spActiveEmployeeDetailsInformation] ";
            var dtTotalActiveEmployeeDetails = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureCommandTest);
            grdDetails.DataSource = null;
            grdDetails.DataBind();
            btnExportDetailsInformation.Visible = false;
            if (dtTotalActiveEmployeeDetails.Rows.Count > 0)
            {
                grdDetails.DataSource = dtTotalActiveEmployeeDetails;
                grdDetails.DataBind();
                btnExportDetailsInformation.Visible = true;
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

    private void GetTotalActiveEmployeeMaleInformation()
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [spActiveEmployeeMaleInformation] ";
            var dtTotalActiveEmployeeDetails = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureCommandTest);
            grdDetails.DataSource = null;
            grdDetails.DataBind();
            btnExportDetailsInformation.Visible = false;
            if (dtTotalActiveEmployeeDetails.Rows.Count > 0)
            {
                grdDetails.DataSource = dtTotalActiveEmployeeDetails;
                grdDetails.DataBind();
                btnExportDetailsInformation.Visible = true;
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

    private void GetTotalActiveEmployeeFemaleInformation()
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [spActiveEmployeeFemaleInformation] ";
            var dtTotalActiveEmployeeDetails = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureCommandTest);
            grdDetails.DataSource = null;
            grdDetails.DataBind();
            btnExportDetailsInformation.Visible = false;
            if (dtTotalActiveEmployeeDetails.Rows.Count > 0)
            {
                grdDetails.DataSource = dtTotalActiveEmployeeDetails;
                grdDetails.DataBind();
                btnExportDetailsInformation.Visible = true;
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

    private void ControlVisiable()
    {
        PanelForDetails.Visible = true;
        PanelForSection.Visible = false;
        lblSelectedCondition.Text = string.Empty;
        lblSelectedEmployeeNumber.Text = string.Empty;
        grdParticularDepartment.DataSource = null;
        grdParticularDepartment.DataBind();
        grdParticularDesignation.DataSource = null;
        grdParticularDesignation.DataBind();
        btnSeeMore.Visible = false;
    }
    protected void LinkButtonTotalEmp_Click(object sender, EventArgs e)
    {
        GetTotalActiveEmployeeDetailsInformation();
        ControlVisiable();
    }
    protected void btnExportDetailsInformation_Click(object sender, EventArgs e)
    {
        if (grdDetails.Rows.Count != 0)
        {
            string type = "Employee Details.xls";
            Export(type, grdDetails);
        }
        else
        {
            string validationMsg = "There is no data for Export ! ";
            ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + validationMsg + "');",
                    true);
        }
    }
    protected void LinkButtonForMale_Click(object sender, EventArgs e)
    {
        GetTotalActiveEmployeeMaleInformation();
        ControlVisiable();
    }
    protected void LinkButtonForFemale_Click(object sender, EventArgs e)
    {
        GetTotalActiveEmployeeFemaleInformation();
        ControlVisiable();
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        string departmentCode = lblSelectionValue.Text;
        string sectionCode = ddlSection.SelectedValue;
        GetTotalActiveEmployeeParticuarDepartmentEmployee(departmentCode, sectionCode);
        if (sectionCode == "-1")
        {
            GetTotalActiveEmployeeParticuarDepartment(departmentCode);
        }
        else
        {
            GetTotalActiveEmployeeParticuarDepartment(departmentCode, sectionCode);
        }
    }

    public int sectionEmployeeNo { get; set; }
    protected void btnExportSearchData_Click(object sender, EventArgs e)
    {
        if (grdSearchData.Rows.Count != 0)
        {
            string type = "Employee list.xls";
            Export(type, grdSearchData);
        }
        else
        {
            string validationMsg = "There is no data for Export ! ";
            ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + validationMsg + "');",
                    true);
        }
    }
}