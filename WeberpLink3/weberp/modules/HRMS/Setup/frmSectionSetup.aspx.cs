using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Setup_frmSectionSetup : System.Web.UI.Page
{
    private const string Rnode = "K";
    string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        txtSectionCode.Attributes.Add("readonly", "readonly");
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

    private void GetMaxSectionCode()
    {
        var storedProcedureCommandTest = " EXEC SectionGetMaxCodeHrms_Sect_Mas ";
        var dtSectionCode = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandTest);
        if (dtSectionCode.Rows.Count > 0)
        {
            txtSectionCode.Text = dtSectionCode.Rows[0][0].ToString();
        }
    }

    protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dbname = ddlcompany.SelectedItem.Value;
        var constr = ConfigurationSettings.AppSettings["SCFConnectionString"].Replace("SCF", dbname);
        Session[GlobalData.sessionConnectionstring] = constr;
        txtHeadOfSection_AutoCompleteExtender.ContextKey = Session[GlobalData.sessionConnectionstring].ToString();
        txtSubstituteHOS_AutoCompleteExtender.ContextKey = Session[GlobalData.sessionConnectionstring].ToString();
        txtSectionCode_AutoCompleteExtender.ContextKey = Session[GlobalData.sessionConnectionstring].ToString();
        txtSectionName_AutoCompleteExtender.ContextKey = Session[GlobalData.sessionConnectionstring].ToString();
        ClsDropDownListController.LoadDropDownList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetOfficeLocationIntoDDL(), ddlOfficeLocation, "Division_Master_Name", "Division_Master_Code");
        ClsDropDownListController.LoadDropDownList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetDepartmentIntoDDL(), ddlDepartmentCode, "Dept_Name", "Dept_Code");
        ClsDropDownListController.LoadddlStatus(ddlStatus);
        GetMaxSectionCode();
        LoadSection();
    }

    private void SaveSection()
    {
        SectionSetup objSectionSetup = new SectionSetup();
        objSectionSetup.CompanyCode = ddlcompany.SelectedValue;
        objSectionSetup.OfficeLocation = ddlOfficeLocation.SelectedValue;
        objSectionSetup.DepartmentCode = ddlDepartmentCode.SelectedValue;
        objSectionSetup.SectionCode = txtSectionCode.Text;
        objSectionSetup.SectionName = txtSectionName.Text;
        objSectionSetup.TxtTag = btnSaveSection.Text;
        objSectionSetup.HeadOfSection = txtHeadOfSection.Text == string.Empty ? null : txtHeadOfSection.Text;
        objSectionSetup.SubstituteHOS = txtSubstituteHOS.Text == string.Empty ? null : txtSubstituteHOS.Text;
        objSectionSetup.TxtStatus = ddlStatus.SelectedValue;
        if (objSectionSetup.TxtTag == "Save")
        {
            Save(Session[GlobalData.sessionConnectionstring].ToString(), objSectionSetup);
        }
        else
        {
            Update(Session[GlobalData.sessionConnectionstring].ToString(), objSectionSetup);
        }
    }

    public void Save(string connectionString, SectionSetup objSectionSetup)
    {
        
        var myConnection = new SqlConnection(connectionString);
        myConnection.Open();
        try
        {
            var myCommand = myConnection.CreateCommand();
            var dtSection = new DataTable();
            myCommand.CommandText = "exec [SectionGetFromHrms_Sect_Mas] '" + objSectionSetup.CompanyCode + "','" + objSectionSetup.OfficeLocation + "','" + objSectionSetup.DepartmentCode + "','" + objSectionSetup.SectionName + "','" + objSectionSetup.SectionCode + "'";
            myCommand.ExecuteNonQuery();
            var daSection = new SqlDataAdapter(myCommand);
            daSection.Fill(dtSection);
            if (dtSection.Rows.Count == 0)
            {
                new SqlCommand("exec [SectionInitiateIntoHrms_Sect_Mas] " +
                                 "'" + objSectionSetup.CompanyCode + "'," +
                                 "'" + objSectionSetup.OfficeLocation + "'," +
                                 "'" + objSectionSetup.DepartmentCode + "'," +
                                 "'" + objSectionSetup.SectionName + "'," +
                                 "'" + objSectionSetup.SectionCode + "'," +
                                 "'" + objSectionSetup.TxtTag + "'," +
                                 "'" + objSectionSetup.HeadOfSection + "'," +
                                 "'" + objSectionSetup.SubstituteHOS + "'," +
                                 "'" + objSectionSetup.TxtStatus + "';", myConnection)
                                .ExecuteNonQuery();
                ClearAllControl();
                LoadSection();
                MessageBox1.ShowSuccess("Data Saved Successfully ");
            }
            else if (dtSection.Rows.Count > 0)
            {
                MessageBox1.ShowInfo("This Section Code or Section Already Exit !");
            }
            else
            {
                ClearAllControl();
                LoadSection();                
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

    public void Update(string connectionString, SectionSetup objSectionSetup)
    {
        var myConnection = new SqlConnection(connectionString);
        myConnection.Open();
        try
        {
            var myCommand = myConnection.CreateCommand();
            var dtSection = new DataTable();
            myCommand.CommandText = "exec [SectionGetFromHrms_Sect_Mas] '" + objSectionSetup.CompanyCode + "','" + objSectionSetup.OfficeLocation + "','" + objSectionSetup.DepartmentCode + "','" + objSectionSetup.SectionName + "','" + objSectionSetup.SectionCode + "'";
            myCommand.ExecuteNonQuery();
            var daSection = new SqlDataAdapter(myCommand);
            daSection.Fill(dtSection);
            if (dtSection.Rows.Count == 1)
            {
                new SqlCommand("exec [SectionInitiateIntoHrms_Sect_Mas] " +
                                 "'" + objSectionSetup.CompanyCode + "'," +
                                 "'" + objSectionSetup.OfficeLocation + "'," +
                                 "'" + objSectionSetup.DepartmentCode + "'," +
                                 "'" + objSectionSetup.SectionName + "'," +
                                 "'" + objSectionSetup.SectionCode + "'," +
                                 "'" + objSectionSetup.TxtTag + "'," +
                                 "'" + objSectionSetup.HeadOfSection + "'," +
                                 "'" + objSectionSetup.SubstituteHOS + "'," +
                                 "'" + objSectionSetup.TxtStatus + "';", myConnection)
                                .ExecuteNonQuery();
                ClearAllControl();
                LoadSection();
                MessageBox1.ShowSuccess("Data Saved Successfully ");
            }
            else if (dtSection.Rows.Count == 0)
            {
                btnSaveSection.Text = "Save";                
                MessageBox1.ShowInfo("This Section did not found ! So, Please Save Now.");
            }
            else
            {
                ClearAllControl();
                LoadSection();                
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
        if (ddlDepartmentCode.SelectedValue == "-1")
        {
            ddlDepartmentCode.Focus();
            return "Please Select Department Code Correctly !";
        }
        if (txtSectionCode.Text == string.Empty)
        {
            txtSectionCode.Focus();
            return "Must Enter Section Code !";
        }
        if (txtSectionName.Text == string.Empty)
        {
            txtSectionName.Focus();
            return "Must Enter Section Name !";
        }
        if (btnSaveSection.Text == "Save" && txtSectionName.Text != string.Empty)
        {
            string sectionCode = GetSectionCode(txtSectionName.Text);
            if (sectionCode != null)
            {
                if (sectionCode != txtSectionCode.Text.Trim())
                {
                    return "Section code did not correct !";
                }
            }
        }
        return checkValidation;
    }

    private void ClearAllControl()
    {
        ddlOfficeLocation.SelectedValue = "-1";
        ddlDepartmentCode.SelectedValue = "-1";
        //txtSectionCode.Text = string.Empty;
        txtSectionName.Text = string.Empty;
        //txtSectionCode.Enabled = true;
        btnSaveSection.Text = "Save";
        txtSubstituteHOS.Text = string.Empty;
        txtHeadOfSection.Text = string.Empty;
        ddlStatus.SelectedValue = "1";
        ddlcompany.Enabled = true;
        ddlOfficeLocation.Enabled = true;
        ddlDepartmentCode.Enabled = true;
        ClsDropDownListController.EnableDisableDropDownList(ddlcompany);
        GetMaxSectionCode();
    }

    private void LoadSection()
    {
        var dtSection = new DataTable();
        var myConnection = new SqlConnection(Session[GlobalData.sessionConnectionstring].ToString());
        var myCommand = myConnection.CreateCommand();
        myConnection.Open();
        myCommand.CommandText = "exec [SectionGetAllFromHrms_Sect_Mas] ";
        myCommand.ExecuteNonQuery();
        var daSection = new SqlDataAdapter(myCommand);
        daSection.Fill(dtSection);
        grdShowSection.DataSource = null;
        grdShowSection.DataBind();
        if (dtSection.Rows.Count > 0)
        {
            grdShowSection.DataSource = dtSection;
            grdShowSection.DataBind();
        }
        myConnection.Close();
    }

    protected void btnSaveSection_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckAllValidation();
        switch (validationMsg)
        {
            case "":
                {
                    SaveSection();
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }
    }

    protected void grdShowSection_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void grdShowSection_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        string lblCompanyCode = ((Label)grdShowSection.Rows[selectedIndex].FindControl("lblCompanyCode")).Text;
        string lblOfficeLocationCode = ((Label)grdShowSection.Rows[selectedIndex].FindControl("lblOfficeLocationCode")).Text;
        string lblDepartmentCode = ((Label)grdShowSection.Rows[selectedIndex].FindControl("lblDepartmentCode")).Text;
        string lblSectionCode = ((Label)grdShowSection.Rows[selectedIndex].FindControl("lblSectionCode")).Text;
        string lblSectionName = ((Label)grdShowSection.Rows[selectedIndex].FindControl("lblSectionName")).Text;
        string lblSect_Head1Code = ((Label)grdShowSection.Rows[selectedIndex].FindControl("lblSect_Head1Code")).Text;
        string lblSect_Head2Code = ((Label)grdShowSection.Rows[selectedIndex].FindControl("lblSect_Head2Code")).Text;
        if (e.CommandName.Equals("Delete"))
        {
            try
            {
                DataProcess.DeleteQuery(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlDeleteSection(lblCompanyCode, lblOfficeLocationCode, lblDepartmentCode, lblSectionCode, lblSectionName));
                ClearAllControl();
                LoadSection();
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
            string lblStatusValue = ((Label)grdShowSection.Rows[selectedIndex].FindControl("lblStatusValue")).Text;
            ddlcompany.SelectedValue = lblCompanyCode;
            ddlOfficeLocation.SelectedValue = lblOfficeLocationCode;
            ddlDepartmentCode.SelectedValue = lblDepartmentCode;
            txtSectionCode.Text = lblSectionCode;
            txtSectionName.Text = lblSectionName;
            btnSaveSection.Text = "Update";
            txtSectionCode.Enabled = false;
            ddlcompany.Enabled = false;
            ddlOfficeLocation.Enabled = false;
            ddlDepartmentCode.Enabled = false;
            txtHeadOfSection.Text = lblSect_Head1Code;
            txtSubstituteHOS.Text = lblSect_Head2Code;
            if (lblStatusValue != "") ddlStatus.SelectedValue = lblStatusValue;
        }
    }

    protected void grdShowSection_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Button delbutton = (Button)e.Row.Cells[16].Controls[0];
            delbutton.OnClientClick = "if (!confirm('Are you sure you want to delete this record ?')) return;";
        }
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[4].Visible = false;
        e.Row.Cells[5].Visible = false;
        e.Row.Cells[9].Visible = false;
        e.Row.Cells[11].Visible = false;
        e.Row.Cells[14].Visible = false;
    }

    protected void txtHeadOfSection_TextChanged(object sender, EventArgs e)
    {
        if (txtHeadOfSection.Text != string.Empty)
        {
            txtHeadOfSection.Text = txtHeadOfSection.Text.Split(':')[0].Trim();

        }
    }

    protected void txtSubstituteHOS_TextChanged(object sender, EventArgs e)
    {
        if (txtSubstituteHOS.Text != string.Empty)
        {
            txtSubstituteHOS.Text = txtSubstituteHOS.Text.Split(':')[0].Trim();

        }

    }

    private void SectionSortWithCondition(string officeLocation)
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [SectionSortFromHrms_Sect_Mas] '" + officeLocation + "' ";
            var dtSection = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandTest);
            grdShowSection.DataSource = null;
            grdShowSection.DataBind();
            if (dtSection.Rows.Count > 0)
            {
                grdShowSection.DataSource = dtSection;
                grdShowSection.DataBind();
            }
            ddlDepartmentCode.SelectedValue = "-1";
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

    private void SectionSortWithCondition(string officeLocation,string departmentCode)
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [SectionSortByDeptFromHrms_Sect_Mas] '" + officeLocation + "','" + departmentCode + "'";
            var dtSection = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandTest);
            grdShowSection.DataSource = null;
            grdShowSection.DataBind();
            if (dtSection.Rows.Count > 0)
            {
                grdShowSection.DataSource = dtSection;
                grdShowSection.DataBind();
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

    private string GetSectionCode(string sectionName)
    {
        string sectionCode = null;
        DataTable dtSectionCode = DataProcess.GetData(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetSectionCode(sectionName));
        if (dtSectionCode.Rows.Count > 0)
        {
            sectionCode = dtSectionCode.Rows[0][0].ToString();
        }
        return sectionCode;
    }

    protected void ddlOfficeLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        SectionSortWithCondition(ddlOfficeLocation.SelectedValue);
        var officeLocation = ddlOfficeLocation.SelectedValue;
        if (officeLocation == "-1")
        {
            ClsDropDownListController.LoadDropDownList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetDepartmentIntoDDL(), ddlDepartmentCode, "Dept_Name", "Dept_Code");
        }
        else
        {
            CommonMethods.LoadDepartmentCode(Session[GlobalData.sessionConnectionstring].ToString(),officeLocation,ddlDepartmentCode);
        }
    }

    protected void ddlDepartmentCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        SectionSortWithCondition(ddlOfficeLocation.SelectedValue, ddlDepartmentCode.SelectedValue);
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAllControl();
    }
    protected void txtSectionName_TextChanged(object sender, EventArgs e)
    {
        if (btnSaveSection.Text == "Save" && txtSectionName.Text != string.Empty)
        {
            string sectionCode = GetSectionCode(txtSectionName.Text);
            if (sectionCode != null)
            {
                txtSectionCode.Text = string.Empty;
                txtSectionCode.Text = sectionCode;
            }
            else
            {
                GetMaxSectionCode();
            }

        }

    }
    protected void btnExporttoExcel_Click(object sender, EventArgs e)
    {
        if (grdShowSection.Rows.Count != 0)
        {
            const string type = "SECTION SETUP.xls";
            ExportGridToExcel.Export(type, grdShowSection);
        }
        else
        {
            MessageBox1.ShowInfo("There is no data for Export ! ");
        }
    }
}