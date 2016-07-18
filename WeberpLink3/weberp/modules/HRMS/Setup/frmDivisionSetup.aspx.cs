using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Setup_frmDivisionSetup : System.Web.UI.Page
{
    private const string Rnode = "K";
    string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    #region LoadEvent

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        if (!Page.IsPostBack)
        {
            LoadCompanyByUserPermission("ADM", Rnode);
            ClsDropDownListController.EnableDisableDropDownList(ddlcompany);
            ddlcompany_SelectedIndexChanged( sender,  e);
        }
    }

    #endregion LoadEvent

    #region Methods

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

    private void SaveDivision()
    {
        DivisionSetup objDivisionSetup = new DivisionSetup();
        objDivisionSetup.CompanyCode = ddlcompany.SelectedValue;
        objDivisionSetup.DivisionCode = txtDivisionCode.Text == string.Empty ? null : txtDivisionCode.Text;
        objDivisionSetup.DivisionName = txtDivisionName.Text == string.Empty ? null : txtDivisionName.Text;
        objDivisionSetup.Location = txtLocation.Text == string.Empty ? null : txtLocation.Text;
        objDivisionSetup.Address1 = txtAddress1.Text == string.Empty ? null : txtAddress1.Text;
        objDivisionSetup.Address2 = txtAddress2.Text == string.Empty ? null : txtAddress2.Text;
        objDivisionSetup.Address3 = txtAddress3.Text == string.Empty ? null : txtAddress3.Text;
        objDivisionSetup.TxtStatus = ddlStatus.SelectedValue;
        objDivisionSetup.TxtTag = btnSaveDivision.Text;
        Save(Session[GlobalData.sessionConnectionstring].ToString(), objDivisionSetup);
    }

    public void Save(string connectionString, DivisionSetup objDivisionSetup)
    {
        string _msg;
        var myConnection = new SqlConnection(connectionString);
        myConnection.Open();
        try
        {
            var myCommand = myConnection.CreateCommand();
            var dtDivision = new DataTable();
            myCommand.CommandText = "exec [DivisionGetFromHrms_Division_Master] '" + objDivisionSetup.DivisionCode + "','" + objDivisionSetup.DivisionName + "','" + objDivisionSetup.CompanyCode + "'";
            myCommand.ExecuteNonQuery();
            var daDivision = new SqlDataAdapter(myCommand);
            daDivision.Fill(dtDivision);
            if ((dtDivision.Rows.Count == 0 && objDivisionSetup.TxtTag == "Save") || (dtDivision.Rows.Count == 1 && objDivisionSetup.TxtTag == "Update"))
            {
                new SqlCommand("exec [DivisionInitiateInto_Hrms_Division_Master] " +
                                 "'" + objDivisionSetup.DivisionCode + "'," +
                                 "'" + objDivisionSetup.DivisionName + "'," +
                                 "'" + objDivisionSetup.CompanyCode + "'," +
                                 "'" + objDivisionSetup.Location + "'," +
                                 "'" + objDivisionSetup.Address1 + "'," +
                                 "'" + objDivisionSetup.Address2 + "'," +
                                 "'" + objDivisionSetup.Address3 + "'," +
                                 "'" + objDivisionSetup.TxtTag + "'," +
                                 "'" + objDivisionSetup.TxtStatus + "';", myConnection)
                                .ExecuteNonQuery();
                _msg = "Data Saved Successfully ";
                ClearAllControl();
                LoadDivisionCompanyWise();
                MessageBox1.ShowSuccess(_msg);
            }
            else if (dtDivision.Rows.Count == 0 && objDivisionSetup.TxtTag == "Update")
            {
                btnSaveDivision.Text = "Save";
                _msg = "This Office Location of The Selected Company Already Deleted ! So, Please Save Now.";
                MessageBox1.ShowInfo(_msg);
            }
            else if (dtDivision.Rows.Count > 0 && objDivisionSetup.TxtTag == "Save")
            {
                _msg = "This Office Location Code or Name of The Selected Company Already Exist !";
                MessageBox1.ShowInfo(_msg);
            }
            else
            {
                ClearAllControl();
                LoadDivisionCompanyWise();
                _msg = " Please try again !";
                MessageBox1.ShowInfo(_msg);
            }

        }
        catch (SqlException sqlError)
        {
            _msg = "Error Occured During Operation into Database, Data did not Save into Database !";
            MessageBox1.ShowError(_msg);
        }
        catch (Exception inSystemExep)
        {
            _msg = " Error Occured, Data did not Save into Database !";
            MessageBox1.ShowError(_msg);
        }
        myConnection.Close();
    }

    private string CheckAllValidation()
    {
        const string checkValidation = "";
        if (ddlcompany.SelectedValue == "-1")
        {
            ddlcompany.Focus();
            return "Please Select Company Correctly !";
        }
        if (txtDivisionCode.Text == string.Empty)
        {
            txtDivisionCode.Focus();
            return " Please Enter Office Location Code Correctly ! ";
        }
        if (txtDivisionName.Text == string.Empty)
        {
            txtDivisionName.Focus();
            return " Please Enter Office Location Name Correctly ! ";
        }
        return checkValidation;
    }

    private void ClearAllControl()
    {
        txtDivisionCode.Text = string.Empty;
        txtDivisionName.Text = string.Empty;
        txtLocation.Text = string.Empty;
        txtAddress1.Text = string.Empty;
        txtAddress2.Text = string.Empty;
        txtAddress3.Text = string.Empty;
        btnSaveDivision.Text = "Save";
        txtDivisionCode.Enabled = true;
        ddlcompany.Enabled = true;
        ddlStatus.SelectedValue = "1";
        ClsDropDownListController.EnableDisableDropDownList(ddlcompany);
    }

    private void LoadDivisionCompanyWise()
    {
        var dtDivision = new DataTable();
        var myConnection = new SqlConnection(Session[GlobalData.sessionConnectionstring].ToString());
        var myCommand = myConnection.CreateCommand();
        myConnection.Open();
        myCommand.CommandText = "exec [DivisionGetAllFromHrms_Division_Master] '" + ddlcompany.SelectedValue + "'";
        myCommand.ExecuteNonQuery();
        var daDivision = new SqlDataAdapter(myCommand);
        daDivision.Fill(dtDivision);
        grdShowDivision.DataSource = null;
        grdShowDivision.DataBind();
        if (dtDivision.Rows.Count > 0)
        {
            grdShowDivision.DataSource = dtDivision;
            grdShowDivision.DataBind();
        }
        myConnection.Close();
    }

    #endregion Methods

    #region Events

    protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dbname = ddlcompany.SelectedItem.Value;
        if (dbname != "-1")
        {
            var constr = ConfigurationSettings.AppSettings["SCFConnectionString"].Replace("SCF", dbname);
            Session[GlobalData.sessionConnectionstring] = constr;
            ClsDropDownListController.LoadddlStatus(ddlStatus);
            LoadDivisionCompanyWise();
        }
        else
        {
            grdShowDivision.DataSource = null;
            grdShowDivision.DataBind();
        }
    }

    protected void btnSaveDivision_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckAllValidation();
        switch (validationMsg)
        {
            case "":
                {
                    SaveDivision();
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }
    }

    protected void grdShowDivision_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void grdShowDivision_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        string lblDivision_Master_Code = ((Label)grdShowDivision.Rows[selectedIndex].FindControl("lblDivision_Master_Code")).Text;
        if (e.CommandName.Equals("Delete"))
        {
            try
            {
                DataProcess.DeleteQuery(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlDeleteDivision(ddlcompany.SelectedValue, lblDivision_Master_Code));
                LoadDivisionCompanyWise();
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
            string lblDivision_Master_Name = ((Label)grdShowDivision.Rows[selectedIndex].FindControl("lblDivision_Master_Name")).Text;
            string lblDivision_Master_Loc = ((Label)grdShowDivision.Rows[selectedIndex].FindControl("lblDivision_Master_Loc")).Text;
            string lblDivision_Master_Address1 = ((Label)grdShowDivision.Rows[selectedIndex].FindControl("lblDivision_Master_Address1")).Text;
            string lblDivision_Master_Address2 = ((Label)grdShowDivision.Rows[selectedIndex].FindControl("lblDivision_Master_Address2")).Text;
            string lblDivision_Master_Address3 = ((Label)grdShowDivision.Rows[selectedIndex].FindControl("lblDivision_Master_Address3")).Text;
            string lblStatusValue = ((Label)grdShowDivision.Rows[selectedIndex].FindControl("lblStatusValue")).Text;
            txtDivisionCode.Text = lblDivision_Master_Code;
            txtDivisionName.Text = lblDivision_Master_Name;
            txtLocation.Text = lblDivision_Master_Loc;
            txtAddress1.Text = lblDivision_Master_Address1;
            txtAddress2.Text = lblDivision_Master_Address2;
            txtAddress3.Text = lblDivision_Master_Address3;
            if (lblStatusValue != "") ddlStatus.SelectedValue = lblStatusValue;
            btnSaveDivision.Text = "Update";
            ddlcompany.Enabled = false;
            txtDivisionCode.Enabled = false;
        }
    }

    #endregion Events

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAllControl();
    }
    protected void grdShowDivision_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[8].Visible = false;
    }
    protected void btnExporttoExcel_Click(object sender, EventArgs e)
    {
        if (grdShowDivision.Rows.Count != 0)
        {
            const string type = " OFFICE LOCATION SETUP.xls";
            ExportGridToExcel.Export(type, grdShowDivision);
        }
        else
        {
            MessageBox1.ShowInfo("There is no data for Export ! ");
        }
    }
}