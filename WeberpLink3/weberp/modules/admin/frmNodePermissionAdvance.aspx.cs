using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_admin_frmNodePermissionAdvance : System.Web.UI.Page
{
    private const string rnode = "M";
    string connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadAllCompany();
            LoadNode();
            CheckBoxForSelectAll.Visible = false;
        }
    }
    private void LoadAllCompany()
    {
        string sqlQuery = "SELECT [CompanyCode],[CompanyName]  FROM [CompanyInformation] order by [CompanyName]";
        ClsDropDownListController.LoadDropDownListWithConcatenation(connectionString, sqlQuery, ddlcompany, "CompanyName", "CompanyCode");
    }

    private void LoadNode()
    {
        string sqlQuery = "SELECT DISTINCT [NodeId],[NodeDescription] FROM [NodeInformation] ORDER BY [NodeDescription]";
        ClsDropDownListController.LoadDropDownList(connectionString, sqlQuery, ddlNode, "NodeDescription", "NodeId");

        //DataTable dt = new DataTable();
        //dt = DataProcess.GetData(connectionString, sqlQuery);

        //ddlNode.Items.Clear();
        //ddlNode.Items.Add("");
        //foreach (DataRow dr in dt.Rows)
        //{
        //    ListItem lst = new ListItem();
        //    lst.Text = dr["NodeDescription"].ToString();
        //    lst.Value = dr["NodeId"].ToString();
        //    ddlNode.Items.Add(lst);
        //}
    }

    protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        string dbname = ddlcompany.SelectedValue;
        if (dbname == "-1")
        {
            CheckBoxListDepartmentId.Items.Clear();
            CheckBoxForSelectAll.Visible = false;
            grdAssignedNode.DataSource = null;
            grdAssignedNode.DataBind();
        }
        else
        {
            string constr = System.Configuration.ConfigurationSettings.AppSettings["SCFConnectionString"].Replace("SCF", dbname);
            Session[GlobalData.sessionConnectionstring] = constr;
            txtEmpId_AutoCompleteExtender.ContextKey = Session[GlobalData.sessionConnectionstring].ToString();
            Session["CompanyName"] = ddlcompany.SelectedItem.Text;
            Session["CompanyAddress"] = "";
            Session["ConnectionStr"] = constr;
            Session["db"] = dbname;
            LibraryPAY.Properties.Settings.Default["SCFConnectionString"] = constr;
            LibraryPAY.Properties.Settings.Default.Save();
            LoadPermissionRecord(txtEmpId.Text, dbname);
            LoadDepartmentId();
            if (CheckBoxListDepartmentId.Items.Count > 1)
            {
                CheckBoxForSelectAll.Visible = true;
            }
        }
    }

    public void LoadDepartmentId()
    {
        string strSql = "SELECT distinct DeptID, Dept FROM Emp_Details INNER JOIN Hrms_Emp_Mas on Emp_Details.EmpId = Hrms_Emp_Mas .Emp_Mas_Emp_Id and Emp_Mas_Term_Flg = 'N' ORDER BY Dept  ASC";
        ClsDropDownListController.LoadCheckBoxList(Session["ConnectionStr"].ToString(), strSql, CheckBoxListDepartmentId, "Dept", "DeptID");
    }

    private void LoadPermissionRecord(string userId, string companyCode)
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [userPermissionGetFromtblUserCompanyDepartment] '"+userId+"','"+companyCode+"'";
            var dtPermission = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandTest);
            grdAssignedNode.DataSource = null;
            grdAssignedNode.DataBind();
            if (dtPermission.Rows.Count > 0)
            {
                grdAssignedNode.DataSource = dtPermission;
                grdAssignedNode.DataBind();
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

    protected void txtEmpId_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtEmpId.Text != "")
            {
                txtEmpId.Text = txtEmpId.Text.Split(':')[0].Trim() == "" ? "" : txtEmpId.Text.Split(':')[0].Trim();
                LoadPermissionRecord(txtEmpId.Text, ddlcompany.SelectedValue);
                //CheckDepartmentAlreadyAssigned();
            }
        }
        catch (IndexOutOfRangeException ex)
        {
            ScriptManager.RegisterStartupScript(
                   this,
                   GetType(),
                   "MessageBox",
                   "alert('Please Select Employee ID From Given List !');",
                   true);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckAllValidation();
        switch (validationMsg)
        {
            case "":
                {
                    string msg = InitiateUserPermission();
                    LoadPermissionRecord(txtEmpId.Text, ddlcompany.SelectedValue);
                    ClearAllControl();
                    ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "MessageBox",
                        "alert(' " + msg + "');",
                        true);

                }
                break;
            default:
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + validationMsg + "');",
                    true);
                break;
        }
    }

    private void ClearAllControl()
    {
        //ddlcompany.SelectedValue = "-1";
        ddlNode.SelectedValue = "-1";
        CheckBoxListDepartmentId.ClearSelection();
        //txtEmpId.Text = string.Empty;
        //CheckBoxListDepartmentId.Items.Clear();
        //CheckBoxForSelectAll.Visible = false;
        //CheckBoxForSelectAll.Checked = false;
    }

    private string CheckAllValidation()
    {
        const string checkValidation = "";
        if (ddlcompany.SelectedValue == "-1")
        {
            ddlcompany.Focus();
            return "Please Select Company Correctly !";
        }
        if (txtEmpId.Text == string.Empty)
        {
            txtEmpId.Focus();
            return "Please Select Employee ID From Given List !";
        }
        if (ddlNode.SelectedValue == "-1")
        {
            ddlNode.Focus();
            return "Please Select Node Correctly !";
        }
        return checkValidation;
    }

    private string InitiateUserPermission()
    {
        string companyId = ddlcompany.SelectedValue;
        string empId = txtEmpId.Text;
        string nodeId = ddlNode.SelectedValue;
        int selectedCount = CheckBoxListDepartmentId.Items.Cast<ListItem>().Count(li => li.Selected);
        string _msg;
        SqlConnection myConnection = new SqlConnection(connectionString);
        myConnection.Open();
        SqlTransaction transaction = myConnection.BeginTransaction();
        try
        {
            new SqlCommand("exec [userPermissionInitiateIntotbl_adm_list_tblUserCompany] " +
                               "'" + empId + "'," +
                               "'" + nodeId + "'," +
                               "'" + companyId + "'," +
                               "" + selectedCount + ";", myConnection, transaction)
                    .ExecuteNonQuery();
            foreach (ListItem item in CheckBoxListDepartmentId.Items)
            {
                if (item.Selected)
                {
                    new SqlCommand("exec [userPermissionInitiateIntotblUserCompanyDepartment] " +
                               "'" + empId + "'," +
                               "'" + nodeId + "'," +
                               "'" + companyId + "'," +
                               "'" + item.Value + "';", myConnection, transaction)
                    .ExecuteNonQuery();
                }
            }
            transaction.Commit();
            _msg = "Data Saved Successfully ";
        }
        catch (SqlException sqlError)
        {
            transaction.Rollback();
            _msg = "Error Occured During Operation into Database, Data did not Save into Database !";
        }
        catch (Exception inSystemExep)
        {
            transaction.Rollback();
            _msg = "System Error, Data did not Save into Database !";
        }
        myConnection.Close();
        return _msg;
    }

    protected void CheckBoxForSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBoxForSelectAll.Checked == true)
        {
            foreach (ListItem li in CheckBoxListDepartmentId.Items)
            {
                li.Selected = true;
            }
        }
        else
        {
            foreach (ListItem li in CheckBoxListDepartmentId.Items)
            {
                li.Selected = false;
            }

        }
    }

    private void CheckDepartmentAlreadyAssigned(string companyId, string empId, string nodeId)
    {
        try
        {
            DataTable getEmpId = DataProcess.GetData(connectionString, "SELECT adm_id FROM [tbl_adm_list]  WHERE adm_id ='" + empId + "'  AND adm_det = '" + nodeId + "' AND Comp_Code = '" + companyId + "'");
            if (getEmpId.Rows.Count != 0)
            {
                CheckBoxListDepartmentId.ClearSelection();
                string sqlQuert = "SELECT [DepartmentID] FROM tblUserCompanyDepartment WHERE UserId = '" + empId + "' AND  NodeID = '" + nodeId + "'  AND CompanyID = '" + companyId + "'";
                DataTable dtDepartment = DataProcess.GetData(connectionString, sqlQuert);
                if (dtDepartment.Rows.Count == 0)
                {
                    foreach (ListItem li in CheckBoxListDepartmentId.Items)
                    {
                        li.Selected = true;
                    }
                }
                else
                {
                    foreach (DataRow dr in dtDepartment.Rows)
                    {
                        CheckBoxListDepartmentId.Items.FindByValue(dr["DepartmentID"].ToString()).Selected = true;
                    }
                }
            }
            else
            {
                foreach (ListItem li in CheckBoxListDepartmentId.Items)
                {
                    li.Selected = false;
                }
            }

        }
        catch (Exception msg)
        {

            throw msg;
        }
    }

    protected void ddlNode_SelectedIndexChanged(object sender, EventArgs e)
    {
        CheckDepartmentAlreadyAssigned(ddlcompany.SelectedValue,txtEmpId.Text,ddlNode.SelectedValue);
    }

    protected void grdAssignedNode_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
    }
    protected void grdAssignedNode_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void grdAssignedNode_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        string lblNodeId = ((Label)grdAssignedNode.Rows[selectedIndex].FindControl("lblNodeId")).Text;

        if (e.CommandName.Equals("Delete"))
        {
            string _msg = null;
            try
            {
                DataProcess.DeleteQuery(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlDeleteNodePermission(ddlcompany.SelectedValue, txtEmpId.Text, lblNodeId));
                LoadPermissionRecord(txtEmpId.Text, ddlcompany.SelectedValue);
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
            ddlNode.SelectedValue = lblNodeId;
            CheckDepartmentAlreadyAssigned(ddlcompany.SelectedValue, txtEmpId.Text, ddlNode.SelectedValue);
        }
    }
}