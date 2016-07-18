using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Details_FrmEmployeeSettlement : System.Web.UI.Page
{
    private const string Rnode = "ab";
    string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        if (!Page.IsPostBack)
        {
            LoadCompanyByUserPermission("ADM", Rnode);
            PanelForEmployeeDetails.Visible = false;
            ClsDropDownListController.EnableDisableDropDownList(ddlcompany);
            ddlcompany_SelectedIndexChanged(sender, e);
            Session[GlobalData.SessionUserId] = current.UserId.ToString();
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
        txtEmployeeCode_AutoCompleteExtender.ContextKey = Session[GlobalData.sessionConnectionstring].ToString();
        LoadEmployeeSettlementDetails();
    }
    private string CheckAllValidation()
    {
        const string checkValidation = "";
        if (ddlcompany.SelectedValue == "-1")
        {
            ddlcompany.Focus();
            return "Please Select Company Correctly !";
        }
        if (txtEmployeeCode.Text == string.Empty)
        {
            txtEmployeeCode.Focus();
            return "Please Select Employee Code From Given List Correctly !";
        }
        if (ddlSettlementType.SelectedValue == "-1")
        {
            ddlSettlementType.Focus();
            return "Please Select Settlement Type Correctly !";
        }
        if (txtNoticePeriod.Text == string.Empty)
        {
            txtNoticePeriod.Focus();
            return "Please Type Notice Period Correctly !";
        }
        if (txtCompensation.Text == string.Empty)
        {
            txtCompensation.Focus();
            return "Please Type Compensation Correctly !";
        }

        return checkValidation;
    }

    private void ClearAllControl()
    {
        txtEmployeeCode.Text = string.Empty;
        ddlSettlementType.SelectedValue = "-1";
        txtNoticePeriod.Text = string.Empty;
        txtCompensation.Text = string.Empty;
        calenderAcceptanceDate.SelectedDate = DateTime.Now;
        calenderRelievingDate.SelectedDate = DateTime.Now;
        calenderDetailsLogFrom.SelectedDate = DateTime.Now;
        PanelForEmployeeDetails.Visible = false;
        txtCommentsOrReason.Text = string.Empty;
        btnSaveEmployeeSettlement.Text = "Save";
    }

    private void LoadEmployeeSettlementDetails()
    {
        string msg = null;
        try
        {
            const string storedProcedureCommandTest = "exec [EmpSettlementGetAllFromhrms_emp_Settlement] ";
            var dtEmployeeSettlement = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandTest);
            grdShowEmpSettlementDetails.DataSource = null;
            grdShowEmpSettlementDetails.DataBind();
            if (dtEmployeeSettlement.Rows.Count > 0)
            {
                grdShowEmpSettlementDetails.DataSource = dtEmployeeSettlement;
                grdShowEmpSettlementDetails.DataBind();
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

    private void ShowEmployeeDetails(string employeeId)
    {
        string msg = null;
        try
        {
            var storedProcedureCommandText = "exec [Transfer_PromotionGetEmployeeDetails] '" + employeeId + "'";
            var dtEmployeeDetails = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandText);
            PanelForEmployeeDetails.Visible = false;
            if (dtEmployeeDetails.Rows.Count > 0)
            {
                lblEmployeeName.Text = dtEmployeeDetails.Rows[0].ItemArray[0] + " " + dtEmployeeDetails.Rows[0].ItemArray[1];
                lblJoiningDate.Text = dtEmployeeDetails.Rows[0].ItemArray[2].ToString() == string.Empty ? null : Convert.ToDateTime(dtEmployeeDetails.Rows[0].ItemArray[2].ToString()).ToString("dd-MMM-yyyy");
                lblOfficeLocation.Text = dtEmployeeDetails.Rows[0].ItemArray[3].ToString();
                lblEmployeeDepartment.Text = dtEmployeeDetails.Rows[0].ItemArray[4].ToString();
                lblSection.Text = dtEmployeeDetails.Rows[0].ItemArray[5].ToString();
                lblDesignation.Text = dtEmployeeDetails.Rows[0].ItemArray[6].ToString() == string.Empty ? null : dtEmployeeDetails.Rows[0].ItemArray[6].ToString();
                PanelForEmployeeDetails.Visible = true;
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

    protected void txtEmployeeCode_TextChanged(object sender, EventArgs e)
    {
        if (txtEmployeeCode.Text != string.Empty)
        {
            txtEmployeeCode.Text = txtEmployeeCode.Text.Split(':')[0].Trim() == "" ? "" : txtEmployeeCode.Text.Split(':')[0].Trim();
            ShowEmployeeDetails(txtEmployeeCode.Text);
        }
    }

    private string SaveEmployeeSettlement()
    {
        EmployeeSettlement objEmployeeSettlement = new EmployeeSettlement();
        objEmployeeSettlement.EmployeeCode = txtEmployeeCode.Text;
        objEmployeeSettlement.SettlementType = ddlSettlementType.SelectedValue;
        objEmployeeSettlement.CompensationDays = txtCompensation.Text == string.Empty ? 0 : Convert.ToInt32(txtCompensation.Text);
        objEmployeeSettlement.AcceptanceDate = Convert.ToDateTime(calenderAcceptanceDate.SelectedDate).ToString("dd-MMM-yyyy");
        objEmployeeSettlement.RelievingDate = GenerateRelievingDate().ToString("dd-MMM-yyyy");
        objEmployeeSettlement.NoticePeriod = txtNoticePeriod.Text == string.Empty ? 0 : Convert.ToInt32(txtNoticePeriod.Text);
        objEmployeeSettlement.CommentsOrReason = txtCommentsOrReason.Text == string.Empty ? null : txtCommentsOrReason.Text;
        objEmployeeSettlement.EntryUserId = Session[GlobalData.SessionUserId].ToString();
        objEmployeeSettlement.TxtTag = btnSaveEmployeeSettlement.Text;
        return objEmployeeSettlement.TxtTag == "Save" ? Save(Session[GlobalData.sessionConnectionstring].ToString(), objEmployeeSettlement) : Update(Session[GlobalData.sessionConnectionstring].ToString(), objEmployeeSettlement);
    }

    private DateTime GenerateRelievingDate()
    {
        var relievingDate = calenderAcceptanceDate.SelectedDate;
        var noticePeriod = txtNoticePeriod.Text;
        if (noticePeriod != string.Empty)
        {
            relievingDate = calenderAcceptanceDate.SelectedDate.AddDays(Convert.ToInt32(noticePeriod));
        }
        return relievingDate;
    }

    public string Save(string connectionString, EmployeeSettlement objEmployeeSettlement)
    {
        string msg;
        var myConnection = new SqlConnection(connectionString);
        myConnection.Open();
        try
        {
            DataTable dtEmployeeSettlement = DataProcess.GetData(connectionString, Sqlgenerate.SqlGetSettlementRecord(objEmployeeSettlement.EmployeeCode));
            if (dtEmployeeSettlement.Rows.Count == 0)
            {
                new SqlCommand("exec [EmpSettlementInitiateIntohrms_emp_Settlement_log] " +
                            "'" + objEmployeeSettlement.EmployeeCode + "'," +
                            "'" + objEmployeeSettlement.SettlementType + "'," +
                            "'" + objEmployeeSettlement.AcceptanceDate + "'," +
                            "" + objEmployeeSettlement.NoticePeriod + "," +
                            "'" + objEmployeeSettlement.RelievingDate + "'," +
                            "'" + objEmployeeSettlement.CommentsOrReason + "'," +
                            "" + objEmployeeSettlement.CompensationDays + "," +
                            "'" + objEmployeeSettlement.EntryUserId + "'," +
                            "'" + objEmployeeSettlement.TxtTag + "';", myConnection)
                 .ExecuteNonQuery();
                msg = "Data Saved Successfully ";
                ClearAllControl();
                LoadEmployeeSettlementDetails();
            }
            else if (dtEmployeeSettlement.Rows.Count > 0)
            {
                msg = "Data of this Employee Already Exit !";
            }
            else
            {
                ClearAllControl();
                LoadEmployeeSettlementDetails();
                msg = " Please try again !";
            }
        }
        catch (SqlException sqlError)
        {
            msg = "Error Occured During Operation into Database, Data did not Save into Database !";
        }
        catch (Exception inSystemExep)
        {
            msg = " Error Occured, Data did not Save into Database !";
        }
        myConnection.Close();
        return msg;
    }
    public string Update(string connectionString, EmployeeSettlement objEmployeeSettlement)
    {
        string msg;
        var myConnection = new SqlConnection(connectionString);
        myConnection.Open();
        try
        {
            var dtEmployeeSettlement = DataProcess.GetData(connectionString, Sqlgenerate.SqlGetSettlementRecord(objEmployeeSettlement.EmployeeCode));
            if (dtEmployeeSettlement.Rows.Count == 1)
            {
                new SqlCommand("exec [EmpSettlementInitiateIntohrms_emp_Settlement_log] " +
                           "'" + objEmployeeSettlement.EmployeeCode + "'," +
                           "'" + objEmployeeSettlement.SettlementType + "'," +
                           "'" + objEmployeeSettlement.AcceptanceDate + "'," +
                           "" + objEmployeeSettlement.NoticePeriod + "," +
                           "'" + objEmployeeSettlement.RelievingDate + "'," +
                           "'" + objEmployeeSettlement.CommentsOrReason + "'," +
                           "" + objEmployeeSettlement.CompensationDays + "," +
                           "'" + objEmployeeSettlement.EntryUserId + "'," +
                           "'" + objEmployeeSettlement.TxtTag + "';", myConnection)
                .ExecuteNonQuery();
                msg = "Data Saved Successfully ";
                ClearAllControl();
                LoadEmployeeSettlementDetails();
            }
            else if (dtEmployeeSettlement.Rows.Count == 0)
            {
                btnSaveEmployeeSettlement.Text = "Save";
                msg = "Data did not found of this Employee ! So, Please Save Now.";
            }
            else if (dtEmployeeSettlement.Rows.Count == 2)
            {
                msg = "Data of this Employee Already Exit !";
            }
            else
            {
                ClearAllControl();
                LoadEmployeeSettlementDetails();
                msg = " Please try again !";
            }
        }
        catch (SqlException sqlError)
        {
            msg = "Error Occured During Operation into Database, Data did not Save into Database !";
        }
        catch (Exception inSystemExep)
        {
            msg = " Error Occured, Data did not Save into Database !";
        }
        myConnection.Close();
        return msg;
    }

    protected void btnSaveEmployeeSettlement_Click(object sender, EventArgs e)
    {
        var validationMsg = CheckAllValidation();
        switch (validationMsg)
        {
            case "":
                {
                    var msg = SaveEmployeeSettlement();
                    MessageBox1.ShowSuccess(msg);
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }
    }

    protected void grdShowEmpSettlementDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        var lblEmployeeId = ((Label)grdShowEmpSettlementDetails.Rows[selectedIndex].FindControl("lblEmployeeID")).Text;
        var lblSettlementType = ((Label)grdShowEmpSettlementDetails.Rows[selectedIndex].FindControl("lblSettlementType")).Text;
        var lblAcceptanceDate = ((Label)grdShowEmpSettlementDetails.Rows[selectedIndex].FindControl("lblAcceptanceDate")).Text;
        var lblNoticePeriod = ((Label)grdShowEmpSettlementDetails.Rows[selectedIndex].FindControl("lblNoticePeriod")).Text;
        var lblRelievingDate = ((Label)grdShowEmpSettlementDetails.Rows[selectedIndex].FindControl("lblRelievingDate")).Text;
        var lblCompensationDays = ((Label)grdShowEmpSettlementDetails.Rows[selectedIndex].FindControl("lblCompensationDays")).Text;
        var lblCommentsReason = ((Label)grdShowEmpSettlementDetails.Rows[selectedIndex].FindControl("lblCommentsReason")).Text;
        if (e.CommandName.Equals("Delete"))
        {
            string msg = null;
            try
            {
                var entryUserId = Session[GlobalData.SessionUserId].ToString();
                var storedProcedureCommandText = "exec [EmpSettlementInitiateIntohrms_emp_Settlement_log] '" + lblEmployeeId + "','" + lblSettlementType + "','" + lblAcceptanceDate + "'," + lblNoticePeriod + ",'" + lblRelievingDate + "','" + lblCommentsReason + "'," + lblCompensationDays + ",'" + entryUserId + "','" + "Delete" + "'";
                StoredProcedureExecutor.StoredProcedureExecuteNonQuery(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandText);
                LoadEmployeeSettlementDetails();
            }
            catch (SqlException sqlError)
            {
                msg = "  Error Occured During Operation into Database, Data did not Delete from Database !  ";

            }
            catch (Exception inSystemExep)
            {
                msg = " Error Occured, Data did not Delete from Database  ! ";

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
        else if (e.CommandName.Equals("Select"))
        {
            txtEmployeeCode.Text = lblEmployeeId;
            ShowEmployeeDetails(txtEmployeeCode.Text);
            ddlSettlementType.SelectedValue = lblSettlementType;
            txtNoticePeriod.Text = lblNoticePeriod;
            txtCompensation.Text = lblCompensationDays;
            calenderAcceptanceDate.SelectedDate = Convert.ToDateTime(lblAcceptanceDate);
            calenderRelievingDate.SelectedDate = Convert.ToDateTime(lblRelievingDate);
            txtCommentsOrReason.Text = lblCommentsReason;
            btnSaveEmployeeSettlement.Text = "Update";
        }

    }
    protected void grdShowEmpSettlementDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    protected void grdShowEmpSettlementDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    private string CheckAllValidationForLogData()
    {
        const string checkValidation = "";
        if (ddlcompany.SelectedValue != "-1") return checkValidation;
        ddlcompany.Focus();
        return "Please Select Company Correctly !";
    }

    protected void btnViewDetailsLog_Click(object sender, EventArgs e)
    {
        var validationMsg = CheckAllValidationForLogData();
        switch (validationMsg)
        {
            case "":
                {
                    ShowAllLogData();
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }

    }

    private void ShowAllLogData()
    {
        string msg = null;
        try
        {
            var detailsLogFrom = Convert.ToDateTime(calenderDetailsLogFrom.SelectedDate).ToString("dd-MMM-yyyy");
            var detailsLogTo = Convert.ToDateTime(calenderDetailsLogTo.SelectedDate).ToString("dd-MMM-yyyy");
            var storedProcedureCommandText = "exec [EmpSettlementGetAllFromhrms_emp_Settlement_log] '" + detailsLogFrom + "','" + detailsLogTo + "'";
            var detailsLogData = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandText);
            grdViewDetailsLog.DataSource = null;
            grdViewDetailsLog.DataBind();
            if (detailsLogData.Rows.Count <= 0) return;
            grdViewDetailsLog.DataSource = detailsLogData;
            grdViewDetailsLog.DataBind();
        }
        catch (SqlException sqlError)
        {
            msg = "  Error Occured During Operation into Database !  ";

        }
        catch (Exception inSystemExep)
        {
            msg = " Error Occured, Data can not read from Database  ! ";

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

    protected void calenderAcceptanceDate_DateChanged(object sender, EventArgs e)
    {
        calenderRelievingDate.SelectedDate = GenerateRelievingDate();
    }

    protected void txtNoticePeriod_Disposed(object sender, EventArgs e)
    {
        calenderRelievingDate.SelectedDate = GenerateRelievingDate();
    }

    protected void txtNoticePeriod_TextChanged(object sender, EventArgs e)
    {
        calenderRelievingDate.SelectedDate = GenerateRelievingDate();
    }
}