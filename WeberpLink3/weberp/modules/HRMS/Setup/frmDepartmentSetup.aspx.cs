using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class modules_HRMS_Setup_frmDepartmentSetup : Page
{
    private const string Rnode = "K";

    readonly string _connectionString = ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        txtDepartmentCode.Attributes.Add("readonly", "readonly");
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
        dt = AccessPermission.GetCompanyByUserandNodeid(_connectionString, userid, nodeid);
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
        txtHeadOfDepartment_AutoCompleteExtender.ContextKey = Session[GlobalData.sessionConnectionstring].ToString();
        txtSubstituteHOD_AutoCompleteExtender.ContextKey = Session[GlobalData.sessionConnectionstring].ToString();
        txtDepartmentCode_AutoCompleteExtender.ContextKey = Session[GlobalData.sessionConnectionstring].ToString();
        txtDepartmentName_AutoCompleteExtender.ContextKey = Session[GlobalData.sessionConnectionstring].ToString();
        ClsDropDownListController.LoadDropDownList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetOfficeLocationIntoDDL(), ddlOfficeLocation, "Division_Master_Name", "Division_Master_Code");
        ClsDropDownListController.LoadddlStatus(ddlStatus);
        GetMaxDepartmentCode();
        LoadDepartment();
    }

    private void GetMaxDepartmentCode()
    {
        const string storedProcedureCommandTest = " EXEC DepartmentGetMaxCodeHrms_Dept_Master ";
        var dtDepartmentCode = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandTest);
        if (dtDepartmentCode.Rows.Count > 0)
        {
            txtDepartmentCode.Text = dtDepartmentCode.Rows[0][0].ToString();
        }
    }
    private void SaveDepartment()
    {
        DepartmentSetup objDepartmentSetup = new DepartmentSetup();
        objDepartmentSetup.CompanyCode = ddlcompany.SelectedValue;
        objDepartmentSetup.OfficeLocation = ddlOfficeLocation.SelectedValue;
        objDepartmentSetup.DepartmentCode = txtDepartmentCode.Text;
        objDepartmentSetup.DepartmentName = txtDepartmentName.Text;
        objDepartmentSetup.DepartmentLocation = txtDepartmentLocation.Text;
        objDepartmentSetup.TxtTag = btnSaveDepartment.Text;
        objDepartmentSetup.HeadOfDepartment = txtHeadOfDepartment.Text == string.Empty ? null : txtHeadOfDepartment.Text;
        objDepartmentSetup.SubstituteHOD = txtSubstituteHOD.Text == string.Empty ? null : txtSubstituteHOD.Text;
        objDepartmentSetup.TxtStatus = ddlStatus.SelectedValue;
        Save(Session[GlobalData.sessionConnectionstring].ToString(), objDepartmentSetup);
    }

    public void Save(string connectionString, DepartmentSetup objDepartmentSetup)
    {
        var myConnection = new SqlConnection(connectionString);
        myConnection.Open();
        try
        {
            var myCommand = myConnection.CreateCommand();
            var dtDepartment = new DataTable();
            myCommand.CommandText = "exec [DepartmentGetFromHrms_Dept_Master] '" + objDepartmentSetup.CompanyCode + "','" + objDepartmentSetup.OfficeLocation + "','" + objDepartmentSetup.DepartmentCode + "','" + objDepartmentSetup.DepartmentName + "'";
            myCommand.ExecuteNonQuery();
            var daDepartment = new SqlDataAdapter(myCommand);
            daDepartment.Fill(dtDepartment);
            if ((dtDepartment.Rows.Count == 0 && objDepartmentSetup.TxtTag == "Save") || (dtDepartment.Rows.Count == 1 && objDepartmentSetup.TxtTag == "Update"))
            {
                new SqlCommand("exec [DepartmentInitiateIntoHrms_Dept_Master] " +
                                 "'" + objDepartmentSetup.CompanyCode + "'," +
                                 "'" + objDepartmentSetup.OfficeLocation + "'," +
                                 "'" + objDepartmentSetup.DepartmentCode + "'," +
                                 "'" + objDepartmentSetup.DepartmentName + "'," +
                                 "'" + objDepartmentSetup.DepartmentLocation + "'," +
                                 "'" + objDepartmentSetup.TxtTag + "'," +
                                 "'" + objDepartmentSetup.HeadOfDepartment + "'," +
                                 "'" + objDepartmentSetup.SubstituteHOD + "'," +
                                 "'" + objDepartmentSetup.TxtStatus + "';", myConnection)
                                .ExecuteNonQuery();
                ClearAllControl();
                LoadDepartment();
                MessageBox1.ShowSuccess("Data Saved Successfully ");
            }
            else if (dtDepartment.Rows.Count == 0 && objDepartmentSetup.TxtTag == "Update")
            {
                btnSaveDepartment.Text = "Save";
                MessageBox1.ShowInfo("This Department Already Deleted ! So, Please Save Now.");
            }
            else if (dtDepartment.Rows.Count > 0 && objDepartmentSetup.TxtTag == "Save")
            {
                MessageBox1.ShowInfo("This Department Already Exit !");
            }
            else
            {
                ClearAllControl();
                LoadDepartment();
                MessageBox1.ShowInfo(" Please try again !");
            }
        }
        catch (SqlException sqlError)
        {
            MessageBox1.ShowError("Error Occured During Operation into Database, Data did not Save into Database !");
        }
        catch (Exception inSystemExep)
        {
            MessageBox1.ShowError(" Error Occured, Data did not Save into Database !");
        }
        finally
        {
            myConnection.Close();
        }
    }

    private string CheckAllValidation()
    {
        const string checkValidation = "";
        if (ddlcompany.SelectedValue == "-1")
        {
            ddlcompany.Focus();
            return "Please Select Company Correctly !";
        }
        if (ddlOfficeLocation.SelectedValue == "-1")
        {
            ddlOfficeLocation.Focus();
            return "Please Select Office Location Correctly !";
        }
        if (txtDepartmentCode.Text == string.Empty)
        {
            txtDepartmentCode.Focus();
            return "Must Enter Department Code !";
        }
        if (txtDepartmentName.Text == string.Empty)
        {
            txtDepartmentName.Focus();
            return "Must Enter Department Name !";
        }
        if (btnSaveDepartment.Text == "Save" && txtDepartmentName.Text != string.Empty)
        {
            string depatementCode = GetDepartmentCode(txtDepartmentName.Text);
            if (depatementCode != null)
            {
                if (depatementCode != txtDepartmentCode.Text.Trim())
                {
                    return "Department code did not correct !";
                }
                
            }

            string depatementName =  GetDepartmentName(txtDepartmentCode.Text);
            if (depatementName != null)
            {
                if (depatementName != txtDepartmentName.Text.Trim())
                {
                    return "Department code did not correct !";
                }
                
            }
            
        }
        return checkValidation;
    }

    private void ClearAllControl()
    {
        ddlOfficeLocation.SelectedValue = "-1";
        //txtDepartmentCode.Text = string.Empty;
        txtDepartmentName.Text = string.Empty;
        txtDepartmentLocation.Text = string.Empty;
        btnSaveDepartment.Text = "Save";
        ddlcompany.Enabled = true;
        ddlOfficeLocation.Enabled = true;
        //txtDepartmentCode.Enabled = true;
        txtHeadOfDepartment.Text = string.Empty;
        txtSubstituteHOD.Text = string.Empty;
        ddlStatus.SelectedValue = "1";
        ClsDropDownListController.EnableDisableDropDownList(ddlcompany);
        GetMaxDepartmentCode();
    }

    private void LoadDepartment()
    {
        var dtDepartment = new DataTable();
        var myConnection = new SqlConnection(Session[GlobalData.sessionConnectionstring].ToString());
        var myCommand = myConnection.CreateCommand();
        myConnection.Open();
        myCommand.CommandText = "exec [DepartmentGetAllFromHrms_Dept_Master] ";
        myCommand.ExecuteNonQuery();
        var daDepartment = new SqlDataAdapter(myCommand);
        daDepartment.Fill(dtDepartment);
        grdShowDepartment.DataSource = null;
        grdShowDepartment.DataBind();
        if (dtDepartment.Rows.Count > 0)
        {
            grdShowDepartment.DataSource = dtDepartment;
            grdShowDepartment.DataBind();
        }
        myConnection.Close();
    }

    private string GetDepartmentCode(string departmentName)
    {
        string departmentCode = null;
        DataTable dtDepartmentCode = DataProcess.GetData(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetDepartmentCode(departmentName));
        if (dtDepartmentCode.Rows.Count > 0)
        {
            departmentCode = dtDepartmentCode.Rows[0][0].ToString();
        }
        return departmentCode;
    }

    private string GetDepartmentName(string departmentCode)
    {
        string departmentName = null;
        DataTable dtDepartmentName = DataProcess.GetData(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetDepartmentName(departmentCode));
        if (dtDepartmentName.Rows.Count > 0)
        {
            departmentName = dtDepartmentName.Rows[0][0].ToString();
        }
        return departmentName;
    }

    protected void btnSaveDepartment_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckAllValidation();
        switch (validationMsg)
        {
            case "":
                {
                    SaveDepartment(); 
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }
    }

    protected void grdShowDepartment_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void grdShowDepartment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        string lblCompanyCode = ((Label)grdShowDepartment.Rows[selectedIndex].FindControl("lblCompanyCode")).Text;
        string lblOfficeLocationCode = ((Label)grdShowDepartment.Rows[selectedIndex].FindControl("lblOfficeLocationCode")).Text;
        string lblDepartmentCode = ((Label)grdShowDepartment.Rows[selectedIndex].FindControl("lblDepartmentCode")).Text;
        string lblDepartmetName = ((Label)grdShowDepartment.Rows[selectedIndex].FindControl("lblDepartmetName")).Text;
        string lblDepartmetLocation = ((Label)grdShowDepartment.Rows[selectedIndex].FindControl("lblDepartmentLocation")).Text;
        string lblHODCode = ((Label)grdShowDepartment.Rows[selectedIndex].FindControl("lblHODCode")).Text;
        string lblSubstituteHODCode = ((Label)grdShowDepartment.Rows[selectedIndex].FindControl("lblSubstituteHODCode")).Text;
        if (e.CommandName.Equals("Delete"))
        {
            try
            {
                DataProcess.DeleteQuery(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlDeleteDepartment(lblCompanyCode, lblOfficeLocationCode, lblDepartmentCode, lblDepartmetName));
                ClearAllControl();
                LoadDepartment();
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
            string lblStatusValue = ((Label)grdShowDepartment.Rows[selectedIndex].FindControl("lblStatusValue")).Text;
            ddlcompany.SelectedValue = lblCompanyCode;
            ddlOfficeLocation.SelectedValue = lblOfficeLocationCode;
            txtDepartmentCode.Text = lblDepartmentCode;
            txtDepartmentName.Text = lblDepartmetName;
            txtDepartmentLocation.Text = lblDepartmetLocation;
            btnSaveDepartment.Text = "Update";
            ddlcompany.Enabled = false;
            ddlOfficeLocation.Enabled = false;
            txtDepartmentCode.Enabled = false;
            txtHeadOfDepartment.Text = lblHODCode;
            txtSubstituteHOD.Text = lblSubstituteHODCode;
            if (lblStatusValue != "") ddlStatus.SelectedValue = lblStatusValue;
        }
    }

    protected void grdShowDepartment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Button delbutton = (Button)e.Row.Cells[15].Controls[0];
            delbutton.OnClientClick = "if (!confirm('Are you sure you want to delete this record ?')) return;";
        }
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[4].Visible = false;
        e.Row.Cells[8].Visible = false;
        e.Row.Cells[10].Visible = false;
        e.Row.Cells[13].Visible = false;
    }

    protected void txtHeadOfDepartment_TextChanged(object sender, EventArgs e)
    {
        if (txtHeadOfDepartment.Text != string.Empty)
        {
            txtHeadOfDepartment.Text = txtHeadOfDepartment.Text.Split(':')[0].Trim();
            
        }
    }

    protected void txtSubstituteHOD_TextChanged(object sender, EventArgs e)
    {
        if (txtSubstituteHOD.Text != string.Empty)
        {
            txtSubstituteHOD.Text = txtSubstituteHOD.Text.Split(':')[0].Trim();

        }
    }

    private void DepartmentSortWithCondition(string officeLocation)
    {
        string msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [DepartmentSortFromHrms_Dept_Master] '" + officeLocation + "' ";
            var dtDepartment = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandTest);
            grdShowDepartment.DataSource = null;
            grdShowDepartment.DataBind();
            if (dtDepartment.Rows.Count > 0)
            {
                grdShowDepartment.DataSource = dtDepartment;
                grdShowDepartment.DataBind();
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

    protected void ddlOfficeLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        DepartmentSortWithCondition(ddlOfficeLocation.SelectedValue);
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAllControl();
    }
    protected void txtDepartmentName_TextChanged(object sender, EventArgs e)
    {
        if (btnSaveDepartment.Text == "Save" && txtDepartmentName.Text != string.Empty)
        {
            string depatementCode = GetDepartmentCode(txtDepartmentName.Text);
            if (depatementCode != null)
	        {
                txtDepartmentCode.Text = string.Empty;
                txtDepartmentCode.Text = depatementCode;
            }
            else
            {
                GetMaxDepartmentCode();
            }
            
        }
    }
    
    protected void btnExporttoExcel_Click(object sender, EventArgs e)
    {
        if (grdShowDepartment.Rows.Count != 0)
        {
            const string type = "DEPARTMENT SETUP.xls";
            ExportGridToExcel.Export(type, grdShowDepartment);
        }
        else
        {
            MessageBox1.ShowInfo("There is no data for Export ! ");
        }
    }
}