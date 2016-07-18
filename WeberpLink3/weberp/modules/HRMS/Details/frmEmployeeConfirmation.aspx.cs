using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Details_frmEmployeeConfirmation : System.Web.UI.Page
{
    readonly string _connectionStr = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"];
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        Session["ApplicantID"] = current.UserId.ToString();
        Session["EntryUserid"] = current.UserId.ToString();
        Session["CompanyName"] = current.CompanyCode.ToString() + ":" + current.CompanyName.ToString();
        Session["CompanyAddress"] = current.CompanyAddress.ToString();
        Session[GlobalData.sessionConnectionstring] = _connectionStr;
        txtFromDateConfirm.Attributes.Add("readonly", "readonly");
        txtToDateConfirm.Attributes.Add("readonly", "readonly");
        txtEmployeeSearch_AutoCompleteExtender.ContextKey = _connectionStr;

    }

    private string CheckValidationForUpdateConfirmationSalary()
    {
        const string checkValidation = "";
        if (txtEmpId.Text == string.Empty)
        {
            txtEmpId.Focus();
            return "Must Enter Employee ID !";
        }
        if (txtJoiningSal.Text == string.Empty)
        {
            txtJoiningSal.Focus();
            return "Must Enter Joining Salary !";
        }
        if (txtConfirmSalary.Text == string.Empty)
        {
            txtConfirmSalary.Focus();
            return "Must Enter Confirm Salary !";
        }
        return checkValidation;
    }

    private string CheckValidationForGetEmpList()
    {
        const string checkValidation = "";
        if (txtFromDate.Text == string.Empty)
        {
            txtFromDate.Focus();
            return "Must Select Date !";
        }
        return checkValidation;
    }

    private void ClearControls()
    {
        txtEmpId.Text = "";
        txtEmpName.Text = "";
        txtJoiningSal.Text = "";
        txtConfirmSalary.Text = "";
    }

    private string UpdateConfirmationSalary()
    {
        string _msg;
        try
        {
            DataProcess.UpdateQuery(_connectionStr, "  Update Hrms_Trans_Det set T_C1='" + txtJoiningSal.Text + "',T_C2='" + txtConfirmSalary.Text + "' where trans_det_emp_id='" + txtEmpId.Text + "' and trans_Emp_pos=1");
            _msg = "Data Saved Successfully ";
            ClearControls();
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

    private void LoadEmployeeForConfirme()
    {
        try
        {
            string checkDate = txtFromDate.Text.ToString() == "" ? "" : txtFromDate.Text.ToString();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlConnection sqlConn = null;
            sqlConn = new SqlConnection(_connectionStr);
            sqlConn.Open();
            cmd.Connection = sqlConn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spProcessLoadEmployeeForConfirme";
            cmd.Parameters.Add(new SqlParameter("@date", SqlDbType.NVarChar)).Value = checkDate.ToString();
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            grdForConfirme.DataSource = null;
            grdForConfirme.DataBind();
            if (dt.Rows.Count > 0)
            {
                grdForConfirme.DataSource = dt;
                grdForConfirme.DataBind();
            }

        }
        catch (Exception msg)
        {

            ScriptManager.RegisterStartupScript(
                      this,
                      GetType(),
                      "MessageBox",
                      "alert(' " + msg + " ');",
                      true);
        }
    }

    protected void btnApplySalary_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckValidationForUpdateConfirmationSalary();
        switch (validationMsg)
        {
            case "":
                {
                    string msg = UpdateConfirmationSalary();
                    MessageBox1.ShowSuccess(msg);
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }
    }

    protected void btnList_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckValidationForGetEmpList();
        switch (validationMsg)
        {
            case "":
                {
                    LoadEmployeeForConfirme();
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }
    }
    protected void grdForConfirme_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdForConfirme.PageIndex = e.NewPageIndex;
        grdForConfirme.DataBind();
        LoadEmployeeForConfirme();
    }
    protected void grdForConfirme_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indx = Convert.ToInt32(e.CommandArgument.ToString());
        if (e.CommandName.Equals("Select"))
        {
            txtGetEmpId.Text = grdForConfirme.Rows[indx].Cells[0].Text.ToString() == "&nbsp;" ? "" : grdForConfirme.Rows[indx].Cells[0].Text.ToString();
            txtGetEmpName.Text = grdForConfirme.Rows[indx].Cells[1].Text.ToString() == "&nbsp;" ? "" : grdForConfirme.Rows[indx].Cells[1].Text.ToString();
        }
    }
    protected void grdForConfirme_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
        }
    }

    private string CheckValidationForPostOrConfirm()
    {
        const string checkValidation = "";
        if (txtGetEmpId.Text == string.Empty)
        {
            txtGetEmpId.Focus();
            return "Must Enter Employee ID !";
        }
        if (txtGetConfirmationDate.Text == string.Empty)
        {
            txtGetConfirmationDate.Focus();
            return "Must Enter Confirm/Extend Date !";
        }
        return checkValidation;
    }

    private void ClearControlsAfterConfirm()
    {
        txtGetEmpId.Text = "";
        txtGetEmpName.Text = "";
        txtGetConfirmationDate.Text = "";
        txtRemarks.Text = "";
    }

    private string InitiateConfirmation(string type, string status)
    {
        string _msg;
        SqlConnection myConnection = new SqlConnection(_connectionStr);
        myConnection.Open();
        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;
        myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        try
        {
            List<ConfirmationStatus> confirmationList = new List<ConfirmationStatus>();
            ConfirmationStatus objConfirmationStatus = new ConfirmationStatus();
            objConfirmationStatus.empId = txtGetEmpId.Text;
            objConfirmationStatus.confirmDate = txtGetConfirmationDate.Text.Trim().ToString();
            objConfirmationStatus.extensionDate = txtGetConfirmationDate.Text.Trim().ToString();
            objConfirmationStatus.joiningSalary = 0;
            objConfirmationStatus.confirmSalary = 0;
            objConfirmationStatus.type = type;
            objConfirmationStatus.entryUser = Session["EntryUserid"].ToString();
            objConfirmationStatus.status = status;
            objConfirmationStatus.remarks = txtRemarks.Text == "" ? "" : txtRemarks.Text.ToString();
            confirmationList.Add(objConfirmationStatus);
            ConfirmationStatus objeConfirmationStatus = new ConfirmationStatus();
            string retval = objeConfirmationStatus.InitiateConfirmationStatus(confirmationList, myCommand);
            if (retval.ToString() == "")
            {
                myTrans.Rollback("SaveAllTransaction");
                _msg = " Data did not save, Rollback !";
            }
            else
            {
                myTrans.Commit();
                _msg = "Data Saved Successfully ";
            }
            LoadEmployeeForConfirme();
            ClearControlsAfterConfirm();
        }
        catch (SqlException sqlError)
        {
            _msg = "Error Occured During Operation into Database, Data did not Save into Database !";
        }
        catch (Exception inSystemExep)
        {
            _msg = " Error Occured, Data did not Save into Database !";
        }
        finally
        {
            myConnection.Close();
        }
        return _msg;
    }

    protected void btnPostForConfirm_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckValidationForPostOrConfirm();
        switch (validationMsg)
        {
            case "":
                {
                    string msg = InitiateConfirmation("C", "Y");
                    MessageBox1.ShowSuccess(msg);
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }
    }
    protected void btnForExtension_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckValidationForPostOrConfirm();
        switch (validationMsg)
        {
            case "":
                {
                    string msg = InitiateConfirmation("E", "N");
                    MessageBox1.ShowSuccess(msg);
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }
    }

    private string CheckValidationForJCRData()
    {
        const string checkValidation = "";
        if (txtEmployeeSearch.Text != string.Empty)
        {
            return checkValidation;

        }
        else
        {
            if (txtFromDate0.Text == string.Empty)
            {
                txtFromDate0.Focus();
                return "Please Select From Date Correctly !";
            }
            if (txtToDate.Text == string.Empty)
            {
                txtToDate.Focus();
                return "Please Select To Date Correctly !";
            }

        }

        return checkValidation;
    }

    private void LoadEmployeeAccordingToConfirmationDate(string fromDate, string toDate)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlConnection sqlConn = null;
            sqlConn = new SqlConnection(_connectionStr);
            sqlConn.Open();
            cmd.Connection = sqlConn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spProcessGetEmployeeByConfirmationDate";
            cmd.Parameters.Add(new SqlParameter("@dateFrom", SqlDbType.NVarChar)).Value = fromDate.ToString();
            cmd.Parameters.Add(new SqlParameter("@dateTo", SqlDbType.NVarChar)).Value = toDate.ToString();
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            grdGetJoiningNdResignation.DataSource = null;
            grdGetJoiningNdResignation.DataBind();
            if (dt.Rows.Count > 0)
            {
                grdGetJoiningNdResignation.DataSource = dt;
                grdGetJoiningNdResignation.DataBind();
            }

        }
        catch (Exception msg)
        {

            ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + msg + " ');",
                    true);
        }
    }

    private void LoadConfirmationStatusOfEmployee(string employeeCode)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlConnection sqlConn = null;
            sqlConn = new SqlConnection(_connectionStr);
            sqlConn.Open();
            cmd.Connection = sqlConn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spProcessGetConfirmationStatusOfEmployee";
            cmd.Parameters.Add(new SqlParameter("@EmployeeCode", SqlDbType.VarChar)).Value = employeeCode;
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            grdGetJoiningNdResignation.DataSource = null;
            grdGetJoiningNdResignation.DataBind();
            if (dt.Rows.Count > 0)
            {
                grdGetJoiningNdResignation.DataSource = dt;
                grdGetJoiningNdResignation.DataBind();
            }

        }
        catch (Exception msg)
        {

            ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + msg + " ');",
                    true);
        }
    }

    private void LoadEmployeeAccordingToDateRangeForConfirm(string fromDate, string toDate)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlConnection sqlConn = null;
            sqlConn = new SqlConnection(_connectionStr);
            sqlConn.Open();
            cmd.Connection = sqlConn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spProcessLoadEmployeeForConfirmByDateRange";
            cmd.Parameters.Add(new SqlParameter("@date2", SqlDbType.NVarChar)).Value = fromDate;
            cmd.Parameters.Add(new SqlParameter("@date", SqlDbType.NVarChar)).Value = toDate;
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            grdConfirmByDaterange.DataSource = null;
            grdConfirmByDaterange.DataBind();
            if (dt.Rows.Count > 0)
            {
                grdConfirmByDaterange.DataSource = dt;
                grdConfirmByDaterange.DataBind();
            }

        }
        catch (Exception msg)
        {

            ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + msg + " ');",
                    true);
        }
    }

    protected void btnConfirmationInfo_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckValidationForJCRData();
        switch (validationMsg)
        {
            case "":
                {
                    if (txtEmployeeSearch.Text == string.Empty)
                    {
                        string fromDate = txtFromDate0.Text.Trim();
                        string toDate = txtToDate.Text.Trim();
                        LoadEmployeeAccordingToConfirmationDate(fromDate, toDate);

                    }
                    else
                    {
                        LoadConfirmationStatusOfEmployee(txtEmployeeSearch.Text);
                    }
                    lblStatus.Text = "Confirmation Information";
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }
    }
    protected void grdGetJoiningNdResignation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Left;
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
                System.Web.UI.WebControls.Table table = new System.Web.UI.WebControls.Table();
                table.GridLines = gv.GridLines;
                if (gv.HeaderRow != null)
                {
                    PrepareControlForExport(gv.HeaderRow);
                    table.Rows.Add(gv.HeaderRow);
                }
                foreach (GridViewRow row in gv.Rows)
                {
                    PrepareControlForExport(row);
                    table.Rows.Add(row);
                }
                if (gv.FooterRow != null)
                {
                    PrepareControlForExport(gv.FooterRow);
                    table.Rows.Add(gv.FooterRow);
                }
                table.RenderControl(htw);
                HttpContext.Current.Response.Write(sw.ToString());
                HttpContext.Current.Response.End();
            }
        }
    }

    protected void btnExportJoinResign_Click(object sender, EventArgs e)
    {
        if (grdGetJoiningNdResignation.Rows.Count != 0)
        {
            string type = "ConfirmationRecord.xls";
            Export(type, grdGetJoiningNdResignation);
        }
        else
        {
            string validationMsg = "There is no data for Export ! ";
            MessageBox1.ShowWarning(validationMsg);
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (grdForConfirme.Rows.Count != 0)
        {
            string type = "EmployeeListForConfirmation.xls";
            Export(type, grdForConfirme);
        }
        else
        {
            string validationMsg = "There is no data for Export ! ";
            MessageBox1.ShowWarning(validationMsg);
        }
    }

    private void InsertEmployeeForConfirmatinReport()
    {
        string checkDate = txtFromDate.Text.ToString() == "" ? "" : txtFromDate.Text.ToString();
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(_connectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessInsertHrms_ForConfirmation_Information";
        cmd.Parameters.Add(new SqlParameter("@date", SqlDbType.NVarChar)).Value = checkDate.ToString();
        cmd.Parameters.Add(new SqlParameter("@userId", SqlDbType.NVarChar)).Value = Session["EntryUserid"].ToString();
        cmd.ExecuteNonQuery();

    }

    private void parameterpass(ParameterFields myParams, string pname, string value)
    {
        ParameterField param = new ParameterField();
        ParameterDiscreteValue dis1 = new ParameterDiscreteValue();
        param.ParameterFieldName = pname;
        dis1.Value = value;
        param.CurrentValues.Add(dis1);
        myParams.Add(param);
    }

    private void ShowReport(string selectionfor, string parameter, string reportname)
    {
        clsReport rpt = new clsReport();
        ParameterFields myParams = new ParameterFields();
        ConnectionInfo ConnInfo = new ConnectionInfo();
        string SCFconnStr = Session[GlobalData.sessionConnectionstring].ToString();
        string[] ff;
        string[] ss;
        string[] prm;
        prm = parameter.Split(';');
        if (prm.Length > 0)
        {
            for (int i = 0; i < prm.Length; i++)
            {
                parameterpass(myParams, prm[i].Split(':')[0].ToString(), prm[i].Split(':')[1].ToString());
            }
        }
        ff = SCFconnStr.Split('=');
        ss = ff[1].Split(';');
        ConnInfo.ServerName = ss[0];
        ss = ff[2].Split(';');
        ConnInfo.DatabaseName = ss[0];
        ss = ff[3].Split(';');
        ConnInfo.UserID = ss[0];
        ss = ff[4].Split(';');
        ConnInfo.Password = ss[0];
        rpt.FileName = reportname;
        rpt.ConnectionInfo = ConnInfo;
        rpt.ParametersFields = myParams;
        rpt.SelectionFormulla = selectionfor;
        Session[GlobalData.sessionReportDet] = rpt;
        RegisterStartupScript("click", "<script>window.open('./frm_report_viewer.aspx');</script>");
    }

    protected void btnReportForConfirmation_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckValidationForGetEmpList();
        switch (validationMsg)
        {
            case "":
                {
                    DataProcess.DeleteQuery(_connectionStr, "Delete FROM [Hrms_ForConfirmation_Information] WHERE Userid='" + Session["EntryUserid"].ToString() + "'");
                    InsertEmployeeForConfirmatinReport();
                    string selectionfor, parameter;
                    selectionfor = "{Hrms_ForConfirmation_Information.Userid}='" + Session["EntryUserid"].ToString() + "'";
                    string CompanyName = "CompanyName" + ":" + Session["CompanyName"].ToString().Split(':')[1].ToString();
                    string CompanyAddress = "CompanyAddress" + ":" + Session["CompanyAddress"].ToString();
                    parameter = CompanyName + ";" + CompanyAddress;
                    string reportname = "../Reports/ConfirmationReminderReport.rpt";
                    ShowReport(selectionfor, parameter, reportname);
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }
    }

    private void InsertDataForJoiningConfirmReport(string fromDate, string toDate, string userId, string sProcedure)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(_connectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = sProcedure;
        cmd.Parameters.Add(new SqlParameter("@dateFrom", SqlDbType.NVarChar)).Value = fromDate.ToString();
        cmd.Parameters.Add(new SqlParameter("@dateTo", SqlDbType.NVarChar)).Value = toDate.ToString();
        cmd.Parameters.Add(new SqlParameter("@userId", SqlDbType.NVarChar)).Value = userId.ToString();
        cmd.ExecuteNonQuery();

    }

    protected void btnConfirmationReport_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckValidationForJCRData();
        switch (validationMsg)
        {
            case "":
                {
                    DataProcess.DeleteQuery(_connectionStr, "Delete FROM [Hrms_JoiningResign_Information] WHERE Userid='" + Session["EntryUserid"].ToString() + "'");
                    string fromDate = txtFromDate0.Text.ToString();
                    string toDate = txtToDate.Text.ToString();
                    InsertDataForJoiningConfirmReport(fromDate, toDate, Session["EntryUserid"].ToString(), "spProcessConfirmationReportHrms_JoiningResign_Information");
                    string selectionfor, parameter;
                    selectionfor = "{Hrms_JoiningResign_Information.Userid}='" + Session["EntryUserid"].ToString() + "'";
                    string CompanyName = "CompanyName" + ":" + Session["CompanyName"].ToString().Split(':')[1].ToString();
                    string CompanyAddress = "CompanyAddress" + ":" + Session["CompanyAddress"].ToString();
                    parameter = CompanyName + ";" + CompanyAddress;
                    string reportname = "../Reports/JoiningEmployeeReport.rpt";
                    ShowReport(selectionfor, parameter, reportname);
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }
    }
    protected void txtEmpId_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtEmpId.Text != "")
            {
                txtEmpName.Text = txtEmpId.Text.ToString().Split(':')[1].ToString().Trim() == "" ? "" : txtEmpId.Text.ToString().Split(':')[1].ToString().Trim();
                txtEmpId.Text = txtEmpId.Text.ToString().Split(':')[0].ToString().Trim() == "" ? "" : txtEmpId.Text.ToString().Split(':')[0].ToString().Trim();
                DataTable dt = Process.Run("select T_C1, T_C2 from Hrms_Trans_Det WHERE Trans_Det_Emp_Id = '" + txtEmpId.Text.Trim() + "'");
                foreach (DataRow row in dt.Rows)
                {
                    txtJoiningSal.Text = row[0].ToString();
                    txtConfirmSalary.Text = row[1].ToString();
                }
            }

        }
        catch (Exception msg)
        {

            ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + msg + " ');",
                    true);
        }
    }
    protected void btnListBydaterange_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckValidationForConfirmationDateRange();
        switch (validationMsg)
        {
            case "":
                {
                    string fromDate = txtFromDateConfirm.Text.Trim();
                    string toDate = txtToDateConfirm.Text.Trim();
                    LoadEmployeeAccordingToDateRangeForConfirm(fromDate, toDate);
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }
    }

    private string CheckValidationForConfirmationDateRange()
    {
        const string checkValidation = "";
        if (txtFromDateConfirm.Text == string.Empty)
        {
            txtFromDateConfirm.Focus();
            return "Please Select From Date Correctly !";
        }
        if (txtToDateConfirm.Text == string.Empty)
        {
            txtToDateConfirm.Focus();
            return "Please Select To Date Correctly !";
        }
        return checkValidation;
    }
    protected void btnExportEmployeeForConfirm_Click(object sender, EventArgs e)
    {
        if (grdConfirmByDaterange.Rows.Count != 0)
        {
            const string type = "EmployeeForConfirm.xls";
            Export(type, grdConfirmByDaterange);
        }
        else
        {
            const string validationMsg = "There is no data for Export ! ";
            MessageBox1.ShowWarning(validationMsg);
        }
    }
    protected void txtEmployeeSearch_TextChanged(object sender, EventArgs e)
    {
        if (txtEmployeeSearch.Text != string.Empty)
        {
            txtEmployeeSearch.Text = txtEmployeeSearch.Text.Split(':')[0].Trim() == "" ? "" : txtEmployeeSearch.Text.Split(':')[0].Trim();
        }
    }
    private void DeleteConfirmationStatus(string employeeCode)
    {
        string storedProcedureComandTest = "exec [spDeleteEmployeeConfirmationStatus] '" + employeeCode + "'";
        StoredProcedureExecutor.StoredProcedureExecuteNonQuery(_connectionStr, storedProcedureComandTest);
    }

    protected void grdGetJoiningNdResignation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        if (e.CommandName.Equals("Delete"))
        {
            try
            {
                string employeeCode = grdGetJoiningNdResignation.Rows[selectedIndex].Cells[1].Text;
                DeleteConfirmationStatus(employeeCode);
                btnConfirmationInfo_Click(sender, e);

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
    }
    protected void grdGetJoiningNdResignation_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}