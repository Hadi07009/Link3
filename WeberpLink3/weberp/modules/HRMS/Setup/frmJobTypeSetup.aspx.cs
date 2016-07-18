using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class modules_HRMS_Setup_frmJobTypeSetup : System.Web.UI.Page
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
        LoadJobType();
    }
    private string SaveJobType()
    {
        JobTypeSetup objJobTypeSetup = new JobTypeSetup();
        objJobTypeSetup.JobTypeCode = txtJobTypeCode.Text;
        objJobTypeSetup.JobTypeTitle = txtJobTypeTitle.Text;
        objJobTypeSetup.TxtTag = btnSaveJobType.Text;
        if (objJobTypeSetup.TxtTag == "Save")
        {
            return Save(Session[GlobalData.sessionConnectionstring].ToString(), objJobTypeSetup);
        }
        else
        {
            return Update(Session[GlobalData.sessionConnectionstring].ToString(), objJobTypeSetup);
        }
    }

    public string Save(string connectionString, JobTypeSetup objJobTypeSetup)
    {
        string _msg;
        var myConnection = new SqlConnection(connectionString);
        myConnection.Open();
        try
        {
            var myCommand = myConnection.CreateCommand();
            var dtJobType = new DataTable();
            dtJobType = DataProcess.GetData(connectionString, Sqlgenerate.SqlGetJobTypeRecord(objJobTypeSetup.JobTypeTitle, objJobTypeSetup.JobTypeCode));
            if (dtJobType.Rows.Count == 0)
            {
                new SqlCommand("exec [JobTypeInitiateIntoHrms_Job_Type] " +
                                 "'" + objJobTypeSetup.JobTypeCode + "'," +
                                 "'" + objJobTypeSetup.JobTypeTitle + "'," +
                                 "'" + objJobTypeSetup.TxtTag + "';", myConnection)
                                .ExecuteNonQuery();
                _msg = "Data Saved Successfully ";
                ClearAllControl();
                LoadJobType();
            }
            else if (dtJobType.Rows.Count > 0)
            {
                _msg = "This Job Type Code or Title Already Exist !";
            }
            else
            {
                ClearAllControl();
                LoadJobType();
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

    public string Update(string connectionString, JobTypeSetup objJobTypeSetup)
    {
        string _msg;
        var myConnection = new SqlConnection(connectionString);
        myConnection.Open();
        try
        {
            var myCommand = myConnection.CreateCommand();
            var dtJobType = new DataTable();
            dtJobType = DataProcess.GetData(connectionString, Sqlgenerate.SqlGetJobTypeRecord(objJobTypeSetup.JobTypeTitle, objJobTypeSetup.JobTypeCode));
            if (dtJobType.Rows.Count == 1)
            {
                new SqlCommand("exec [JobTypeInitiateIntoHrms_Job_Type] " +
                                 "'" + objJobTypeSetup.JobTypeCode + "'," +
                                 "'" + objJobTypeSetup.JobTypeTitle + "'," +
                                 "'" + objJobTypeSetup.TxtTag + "';", myConnection)
                                .ExecuteNonQuery();
                _msg = "Data Saved Successfully ";
                ClearAllControl();
                LoadJobType();
            }
            else if (dtJobType.Rows.Count == 0)
            {
                btnSaveJobType.Text = "Save";
                _msg = "This Job Type did not found ! So, Please Save Now.";
            }
            else if (dtJobType.Rows.Count == 2)
            {
                _msg = "This Job Type Title Already Exit !";
            }
            else
            {
                ClearAllControl();
                LoadJobType();
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
        if (txtJobTypeCode.Text == string.Empty)
        {
            txtJobTypeCode.Focus();
            return "Must Enter Job Type Code !";
        }
        if (txtJobTypeTitle.Text == string.Empty)
        {
            txtJobTypeTitle.Focus();
            return "Must Enter Job Type Title !";
        }
        return checkValidation;
    }

    private void ClearAllControl()
    {
        txtJobTypeCode.Text = string.Empty;
        txtJobTypeTitle.Text = string.Empty;
        txtJobTypeCode.Enabled = true;
        btnSaveJobType.Text = "Save";
    }

    private void LoadJobType()
    {
        var dtJobType = new DataTable();
        var myConnection = new SqlConnection(Session[GlobalData.sessionConnectionstring].ToString());
        var myCommand = myConnection.CreateCommand();
        myConnection.Open();
        myCommand.CommandText = "exec [JobTypeGetAllFromHrms_Job_Type] ";
        myCommand.ExecuteNonQuery();
        var daJobType = new SqlDataAdapter(myCommand);
        daJobType.Fill(dtJobType);
        grdShowJobType.DataSource = null;
        grdShowJobType.DataBind();
        if (dtJobType.Rows.Count > 0)
        {
            grdShowJobType.DataSource = dtJobType;
            grdShowJobType.DataBind();
        }
        myConnection.Close();
    }

    protected void btnSaveJobType_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckAllValidation();
        switch (validationMsg)
        {
            case "":
                {
                    string msg = SaveJobType();
                    MessageBox1.ShowSuccess(msg);
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }
    }
    protected void grdShowJobType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        string lblJobTypeCode = ((Label)grdShowJobType.Rows[selectedIndex].FindControl("lblJobTypeCode")).Text;
        string lblJobTypeTitle = ((Label)grdShowJobType.Rows[selectedIndex].FindControl("lblJobTypeTitle")).Text;
        if (e.CommandName.Equals("Delete"))
        {
            try
            {
                DataProcess.DeleteQuery(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlDeleteJobTypeRecord(lblJobTypeCode));
                LoadJobType();
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
            txtJobTypeCode.Text = lblJobTypeCode;
            txtJobTypeTitle.Text = lblJobTypeTitle;
            btnSaveJobType.Text = "Update";
            txtJobTypeCode.Enabled = false;
        }
    }

    protected void grdShowJobType_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAllControl();
    }
}