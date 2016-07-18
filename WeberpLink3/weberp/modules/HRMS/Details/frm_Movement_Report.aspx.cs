using ADODB;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Details_frm_Movement_Report : System.Web.UI.Page
{
    string rnode = "I";
    string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        if (!Page.IsPostBack)
        {
            LoadCompanyByUserPermission("ADM", rnode);
            ClsDropDownListController.EnableDisableDropDownList(ddlcompany);
            ddlcompany_SelectedIndexChanged(sender, e);
            txtEmpId_AutoCompleteExtender.ContextKey = ConnectionString;
            Loadtime();
        }
    }

    private void LoadCompanyByUserPermission(string userid, string nodeid)
    {
        DataTable dt = new DataTable();
        ListItem lst;
        ddlcompany.Items.Clear();
        ddlcompany.Items.Add("");
        dt = AccessPermission.GetCompanyByUserandNodeid(ConnectionString, userid, nodeid);
        foreach (DataRow dr in dt.Rows)
        {
            lst = new ListItem();
            lst.Text = dr["COMP_CODE"].ToString() + ":" + dr["COMP_NAME"].ToString();
            lst.Value = dr["COMP_CODE"].ToString();
            ddlcompany.Items.Add(lst);
        }
    }

    protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        string dbname = ddlcompany.SelectedItem.Value.ToString();
        string constr = ConnectionString;
        Session[GlobalData.sessionConnectionstring] = constr;
        Session["CompanyName"] = ddlcompany.SelectedItem.Text;
        Session["CompanyAddress"] = current.CompanyAddress.ToString();
        Session["ConnectionStr"] = constr.ToString();
        Session["db"] = dbname.ToString();
        Session["EntryUserid"] = current.UserId.Trim();
        LibraryPAY.Properties.Settings.Default["SCFConnectionString"] = constr;
        LibraryPAY.Properties.Settings.Default.Save();
        LoadofficeLocation();
        LoadDepartmentIdByuserCode("ADM", dbname.ToString(), rnode.ToString());
    }

    private void LoadofficeLocation()
    {
        var dbname = ddlcompany.SelectedItem.Value;
        Session[GlobalData.sessionConnectionstring] = ConnectionString;
        ClsDropDownListController.LoadDropDownList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetOfficeLocationIntoDDL(), ddlOfficeLocation, "Division_Master_Name", "Division_Master_Code");
    }

    private void Loadtime()
    {

        DataTable DT = new DataTable();
        string SQL = "";

        SQL = "select Convert(Datetime,'2000-01-01 '+InTime,103) as InPunch,Convert(Datetime,'2000-01-01 '+OutTime,103) as OutPunch from Hrms_PunchLimit where Status=1";
        DT = DataProcess.GetData(ConnectionString, SQL);
        DateTime dt1 = Convert.ToDateTime(DT.Rows[0]["InPunch"].ToString());
        DateTime dt2 = Convert.ToDateTime(DT.Rows[0]["OutPunch"].ToString());


        MKB.TimePicker.TimeSelector mkb = (MKB.TimePicker.TimeSelector)timeoffIntime;
        MKB.TimePicker.TimeSelector mkb2 = (MKB.TimePicker.TimeSelector)timeoffOuttime;

        //DateTime dt1 = DateTime.Parse("01-01-2015 09:30 AM");
        MKB.TimePicker.TimeSelector.AmPmSpec am_pm;
        if (dt1.ToString("tt") == "AM")
        {
            am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
        }
        else
        {
            am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
        }
        mkb.SetTime(dt1.Hour, dt1.Minute, am_pm);

        //DateTime dt2 = DateTime.Parse("01-01-2015 12:30 PM");
        MKB.TimePicker.TimeSelector.AmPmSpec am_pm2;
        if (dt2.ToString("tt") == "AM")
        {
            am_pm2 = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
        }
        else
        {
            am_pm2 = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
        }
        mkb2.SetTime(dt2.Hour, dt2.Minute, am_pm2);

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

    public void LoadDepartmentIdByuserCode(string userid, string companyid, string nodeid)
    {
        DataTable dt = new DataTable();
        string strSql = "";
        strSql = "  SELECT distinct DeptID, Dept FROM Emp_Details"
                      + " INNER JOIN Hrms_Emp_Mas on Emp_Details.EmpId = Hrms_Emp_Mas .Emp_Mas_Emp_Id and Emp_Mas_Term_Flg = 'N'"
                      + " Inner join tblUserCompanyDepartment on DepartmentID=Deptid"
                      + " where [UserID]='" + userid + "' and [NodeID]='" + nodeid + "' and [CompanyID]='" + companyid + "'"
                      + " ORDER BY Dept  ASC";
        dt = DataProcess.GetData(Session["ConnectionStr"].ToString(), strSql);
        if (dt.Rows.Count == 0)
        {
            strSql = "SELECT distinct DeptID, Dept FROM Emp_Details INNER JOIN Hrms_Emp_Mas on Emp_Details.EmpId = Hrms_Emp_Mas .Emp_Mas_Emp_Id and Emp_Mas_Term_Flg = 'N' ORDER BY Dept  ASC";
            dt = DataProcess.GetData(Session["ConnectionStr"].ToString(), strSql);
        }
        ddlDepartmentId.Items.Clear();
        ddlDepartmentId.Items.Insert(0, new ListItem("--- All Department ---", "-1"));
        foreach (DataRow dr in dt.Rows)
        {
            ListItem lst = new ListItem();
            lst.Value = dr["DeptID"].ToString();
            lst.Text = dr["Dept"].ToString();
            ddlDepartmentId.Items.Add(lst);
        }
    }

    private string CheckAllValidation()
    {
        const string checkValidation = "";
        if (ddlcompany.SelectedValue == "")
        {
            ddlcompany.Focus();
            return "Please Select Company Correctly !";
        }
        if (txtEmpId.Text == string.Empty)
        {
            txtEmpId.Focus();
            return "Must Enter Employee ID !";
        }
        return checkValidation;
    }

    protected void btnPreviewIndividual_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckAllValidation();
        switch (validationMsg)
        {
            case "":
                {

                    MovementRegisterReport();

                    string rpttitle = "rpttitle:" + "In";
                    string dateFrom = Convert.ToDateTime(txtFromDate.SelectedDate).ToString("dd-MMM-yyyy");
                    string dateTo = Convert.ToDateTime(txtToDate.SelectedDate).ToString("dd-MMM-yyyy");

                    string selectionfor, parameter;
                    selectionfor = "{HrmsMovementRpt.Userid}='" + Session["EntryUserid"].ToString() + "'";
                    string CompanyName = "CompanyName" + ":" + Session["CompanyName"].ToString().Split(':')[1].ToString();
                    string CompanyAddress = "CompanyAddress" + ":" + Session["CompanyAddress"].ToString();
                    dateFrom = "dateFrom:" + dateFrom.ToString();
                    dateTo = "dateTo:" + dateTo.ToString();

                    parameter = CompanyName + ";" + CompanyAddress + ";" + dateFrom + ";" + dateTo + ";" + rpttitle;
                    string reportname = "../Reports/MovementRegisterReport.rpt";
                    ShowReport(selectionfor, parameter, reportname);
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }

    }

    private void CallLateAttendanceSp()
    {
        DateTime fdate, lDate;
        string deptid, empid, userid;
        fdate = txtFromDate.SelectedDate;
        lDate = txtToDate.SelectedDate;
        deptid = "";
        int cond = 0;
        deptid = ddlDepartmentId.SelectedItem.Value;
        if (RadioButtonList2.SelectedIndex == 0)
        {
            cond = 1;
        }
        else if (RadioButtonList2.SelectedIndex == 1)
        {
            cond = 2;
        }
        else
        {
            cond = 0;
        }

        userid = current.UserId.Trim();


        empid = txtEmpId.Text.Trim().Split(':')[0].ToString();
        LeaveProcess lvp = new LeaveProcess();
        string al = lvp.spCxecuteAbsentListAttendanceIndividual(Session["ConnectionStr"].ToString(), fdate, lDate, deptid, empid, cond, rnode.ToString(), Session["db"].ToString(), userid.ToString());
    }

    private void LoadAttendanceRecords(string dateFrom, string dateTo, string departmentCode, string companyCode, string userId)
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [spGetAttendancePunchRecordsByDate_Department] '" + dateFrom + "','" + dateTo + "','" + departmentCode + "','" + companyCode + "','" + userId + "'";
            var dtAttendance = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandTest);
            grdAttendanceRecord.DataSource = null;
            grdAttendanceRecord.DataBind();
            if (dtAttendance.Rows.Count > 0)
            {
                grdAttendanceRecord.DataSource = dtAttendance;
                grdAttendanceRecord.DataBind();
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

    private void LoadInpunchMissingRecords(string dateFrom, string dateTo, string departmentCode, string companyCode, string userId, string OfficeCode)
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [spGetAttendanceInPunchMissingRecordsByDate_Department] '" + dateFrom + "','" + dateTo + "','" + departmentCode + "','" + companyCode + "','" + userId + "','" + OfficeCode + "'";
            var dtAttendance = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandTest);
            grdAttendanceRecord.DataSource = null;
            grdAttendanceRecord.DataBind();
            if (dtAttendance.Rows.Count > 0)
            {
                grdAttendanceRecord.DataSource = dtAttendance;
                grdAttendanceRecord.DataBind();
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

    private void LoadOutpunchMissingRecords(string dateFrom, string dateTo, string departmentCode, string companyCode, string userId, string OfficeCode)
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [spGetAttendanceOutPunchMissingRecordsByDate_Department] '" + dateFrom + "','" + dateTo + "','" + departmentCode + "','" + companyCode + "','" + userId + "','" + OfficeCode + "'";
            var dtAttendance = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandTest);
            grdAttendanceRecord.DataSource = null;
            grdAttendanceRecord.DataBind();
            if (dtAttendance.Rows.Count > 0)
            {
                grdAttendanceRecord.DataSource = dtAttendance;
                grdAttendanceRecord.DataBind();
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

    private void InpunchMissingRecordsReports(string dateFrom, string dateTo, string departmentCode, string companyCode, string userId, string OfficeCode)
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [spInpunchMissingRecordsReportsByDate_Department] '" + dateFrom + "','" + dateTo + "','" + departmentCode + "','" + companyCode + "','" + userId + "','" + OfficeCode + "'";
            var dtAttendance = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandTest);
            grdAttendanceRecord.DataSource = null;
            grdAttendanceRecord.DataBind();
            if (dtAttendance.Rows.Count > 0)
            {
                grdAttendanceRecord.DataSource = dtAttendance;
                grdAttendanceRecord.DataBind();
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

    private void OutpunchMissingRecordsReport(string dateFrom, string dateTo, string departmentCode, string companyCode, string userId, string OfficeCode)
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [spOutpunchMissingRecordsReportsByDate_Department] '" + dateFrom + "','" + dateTo + "','" + departmentCode + "','" + companyCode + "','" + userId + "','" + OfficeCode + "'";
            var dtAttendance = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandTest);
            grdAttendanceRecord.DataSource = null;
            grdAttendanceRecord.DataBind();
            if (dtAttendance.Rows.Count > 0)
            {
                grdAttendanceRecord.DataSource = dtAttendance;
                grdAttendanceRecord.DataBind();
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

    private void InsertAttendanceRecordsForReport(string dateFrom, string dateTo, string departmentCode, string companyCode, string userId)
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [spInsertAttendanceRecordsForReport] '" + dateFrom + "','" + dateTo + "','" + departmentCode + "','" + companyCode + "','" + userId + "'";
            StoredProcedureExecutor.StoredProcedureExecuteNonQuery(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandTest);
        }
        catch (SqlException sqlError)
        {
            _msg = " Error Occured During Data insert info Database ! ";
        }
        catch (Exception inSystemExep)
        {
            _msg = " Error Occured, Data did not save into Database  !";
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

    protected void btnPreviewSummary_Click(object sender, EventArgs e)
    {
        if (ddlcompany.SelectedValue == "")
        {
            string validationMsg = "Please Select Company ! ";
            ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + validationMsg + "');",
                    true);
            return;
        }


        if (RadioButtonList2.SelectedIndex == 0)
        {
            InpunchMissing();
        }
        else
        {
            OutpunchMissing();
        }

    }

    private void InpunchMissing()
    {
        try
        {
            string CompCode = ddlcompany.SelectedValue.ToString();
            string OfficeCode = ddlOfficeLocation.SelectedValue;
            string DeptCode = ddlDepartmentId.SelectedValue;
            string dateFrom = Convert.ToDateTime(txtFromDate.SelectedDate).ToString("dd-MMM-yyyy");
            string dateTo = Convert.ToDateTime(txtToDate.SelectedDate).ToString("dd-MMM-yyyy");
            LoadInpunchMissingRecords(dateFrom, dateTo, DeptCode, CompCode, Session["EntryUserid"].ToString(), OfficeCode);

        }
        catch (Exception ex)
        {

        }

    }

    private void OutpunchMissing()
    {
        try
        {
            string CompCode = ddlcompany.SelectedValue.ToString();
            string OfficeCode = ddlOfficeLocation.SelectedValue;
            string DeptCode = ddlDepartmentId.SelectedValue;
            string dateFrom = Convert.ToDateTime(txtFromDate.SelectedDate).ToString("dd-MMM-yyyy");
            string dateTo = Convert.ToDateTime(txtToDate.SelectedDate).ToString("dd-MMM-yyyy");
            LoadOutpunchMissingRecords(dateFrom, dateTo, DeptCode, CompCode, Session["EntryUserid"].ToString(), OfficeCode);

        }
        catch (Exception ex)
        {

        }

    }

    private void InpunchMissingReport()
    {
        try
        {
            string CompCode = ddlcompany.SelectedValue.ToString();
            string OfficeCode = ddlOfficeLocation.SelectedValue;
            string DeptCode = ddlDepartmentId.SelectedValue;
            string dateFrom = Convert.ToDateTime(txtFromDate.SelectedDate).ToString("dd-MMM-yyyy");
            string dateTo = Convert.ToDateTime(txtToDate.SelectedDate).ToString("dd-MMM-yyyy");
            InpunchMissingRecordsReports(dateFrom, dateTo, DeptCode, CompCode, Session["EntryUserid"].ToString(), OfficeCode);

        }
        catch (Exception ex)
        {

        }

    }

    private void OutpunchMissingReport()
    {
        try
        {
            string CompCode = ddlcompany.SelectedValue.ToString();
            string OfficeCode = ddlOfficeLocation.SelectedValue;
            string DeptCode = ddlDepartmentId.SelectedValue;
            string dateFrom = Convert.ToDateTime(txtFromDate.SelectedDate).ToString("dd-MMM-yyyy");
            string dateTo = Convert.ToDateTime(txtToDate.SelectedDate).ToString("dd-MMM-yyyy");
            OutpunchMissingRecordsReport(dateFrom, dateTo, DeptCode, CompCode, Session["EntryUserid"].ToString(), OfficeCode);

        }
        catch (Exception ex)
        {

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

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (grdAttendanceRecord.Rows.Count != 0)
        {
            string type = "EmployeeAttendanceReport.xls";
            Export(type, grdAttendanceRecord);
        }
        else
        {
            string validationMsg = "First of All Preview Summary, Then Export ! ";
            MessageBox1.ShowInfo(validationMsg);
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ddlcompany.SelectedValue == "")
        {
            string validationMsg = "Please Select Company ! ";
            ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + validationMsg + "');",
                    true);
            return;
        }

        string rpttitle = "";

        if (RadioButtonList2.SelectedIndex == 0)
        {
            InpunchMissingReport();
            rpttitle = "rpttitle:" + "In";
        }
        else
        {
            OutpunchMissingReport();
            rpttitle = "rpttitle:" + "Out";
        }


        string dateFrom = Convert.ToDateTime(txtFromDate.SelectedDate).ToString("dd-MMM-yyyy");
        string dateTo = Convert.ToDateTime(txtToDate.SelectedDate).ToString("dd-MMM-yyyy");

        string selectionfor, parameter;
        selectionfor = "{HrmsMovementRpt.Userid}='" + Session["EntryUserid"].ToString() + "'";
        string CompanyName = "CompanyName" + ":" + Session["CompanyName"].ToString().Split(':')[1].ToString();
        string CompanyAddress = "CompanyAddress" + ":" + Session["CompanyAddress"].ToString();
        dateFrom = "dateFrom:" + dateFrom.ToString();
        dateTo = "dateTo:" + dateTo.ToString();

        parameter = CompanyName + ";" + CompanyAddress + ";" + dateFrom + ";" + dateTo + ";" + rpttitle;
        string reportname = "../Reports/PunchMissingReport.rpt";
        ShowReport(selectionfor, parameter, reportname);

    }
    protected void btnExport0_Click(object sender, EventArgs e)
    {
        SqlConnection myConnection = new SqlConnection(ConnectionString);
        myConnection.Open();
        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;

        myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;

        try
        {
            string CompCode = ddlcompany.SelectedValue.ToString();
            string OfficeCode = ddlOfficeLocation.SelectedValue;
            string ShiftCode = "";
            string intime = TimeFormatGenerate(timeoffIntime.Date.Hour.ToString() + ":" + timeoffIntime.Date.Minute.ToString() + ":" + timeoffIntime.AmPm.ToString());
            string OutTime = TimeFormatGenerate(timeoffOuttime.Date.Hour.ToString() + ":" + timeoffOuttime.Date.Minute.ToString() + ":" + timeoffOuttime.AmPm.ToString());
            string userid = Session["EntryUserid"].ToString();

            string sql = "";

            sql = "update Hrms_PunchLimit set Status='0',EntryDate=Convert(Datetime,'" + System.DateTime.Now + "',103),EntryUserid='" + userid + "' where status='1'";
            DataProcess.ExecuteQuery(myCommand, sql);

            sql = "insert into Hrms_PunchLimit([CompanyCode],[OfficeLocationCode],[ShiftCode],[InTime],[OutTime],[Status],[EntryDate],[EntryUserid])values('" + CompCode + "','" + OfficeCode + "','" + ShiftCode + "','" + intime + "','" + OutTime + "','1',Convert(Datetime,'" + System.DateTime.Now + "',103),'" + userid + "')";
            DataProcess.ExecuteQuery(myCommand, sql);

            myTrans.Commit();

        }
        catch
        {

            myTrans.Rollback("SaveAllTransaction");
        }
        finally
        {
            myConnection.Close();

        }
    }

    private string TimeFormatGenerate(string atf)
    {
        string rtf = "";
        int h = Convert.ToInt32(atf.Split(':')[0].ToString());
        int m = Convert.ToInt32(atf.Split(':')[1].ToString());
        if (h > 12)
        {
            h = h - 12;
        }
        string hh = string.Format("{0:00}", h);
        string mm = string.Format("{0:00}", m);
        string ampm = atf.Split(':')[2].ToString();
        rtf = hh + ":" + mm + " " + ampm;
        return rtf;
    }
    protected void btnPreviewMovement_Click(object sender, EventArgs e)
    {
        if (ddlcompany.SelectedValue == "")
        {
            string validationMsg = "Please Select Company ! ";
            ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + validationMsg + "');",
                    true);
            return;
        }

        MovementRegister();

    }
    private void MovementRegister()
    {
        try
        {

            string userid = txtEmpId.Text.Split(':')[0].ToString();
            string[] tmp = txtEmpId.Text.Split(':');
            if (tmp.Length > 1)
            {
                userid = tmp[0].ToString();
            }
            else
            {
                userid = "";
            }

            string dateFrom = Convert.ToDateTime(txtFromDate.SelectedDate).ToString("dd-MMM-yyyy");
            string dateTo = Convert.ToDateTime(txtToDate.SelectedDate).ToString("dd-MMM-yyyy");


            LoadMovementRecords(dateFrom, dateTo, ddlDepartmentId.SelectedValue, ddlcompany.SelectedValue, userid);

        }
        catch (Exception ex)
        {

        }

    }

    private void MovementRegisterReport()
    {
        try
        {
            string entryuserid = Session["EntryUserid"].ToString();
            string userid = txtEmpId.Text.Split(':')[0].ToString();
            string[] tmp = txtEmpId.Text.Split(':');
            if (tmp.Length > 1)
            {
                userid = tmp[0].ToString();
            }
            else
            {
                userid = "";
            }

            string dateFrom = Convert.ToDateTime(txtFromDate.SelectedDate).ToString("dd-MMM-yyyy");
            string dateTo = Convert.ToDateTime(txtToDate.SelectedDate).ToString("dd-MMM-yyyy");


            LoadMovementRecordsReport(dateFrom, dateTo, ddlDepartmentId.SelectedValue, ddlcompany.SelectedValue, userid, entryuserid);

        }
        catch (Exception ex)
        {

        }

    }


    private void LoadMovementRecords(string dateFrom, string dateTo, string departmentCode, string companyCode, string userId)
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [spGetMovementRecordsByDate_Department] '" + dateFrom + "','" + dateTo + "','" + departmentCode + "','" + companyCode + "','" + userId + "'";
            var dtAttendance = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandTest);
            grdMovementRecords.DataSource = null;
            grdMovementRecords.DataBind();
            if (dtAttendance.Rows.Count > 0)
            {
                grdMovementRecords.DataSource = dtAttendance;
                grdMovementRecords.DataBind();
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

    private void LoadMovementRecordsReport(string dateFrom, string dateTo, string departmentCode, string companyCode, string userId, string entryuserid)
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [spMovementRecordsReportByDate_Department] '" + dateFrom + "','" + dateTo + "','" + departmentCode + "','" + companyCode + "','" + userId + "','" + entryuserid + "'";
            var dtAttendance = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandTest);

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



    protected void ddlOfficeLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        var officeLocation = ddlOfficeLocation.SelectedValue;
        CommonMethods.LoadDepartmentCode(Session[GlobalData.sessionConnectionstring].ToString(), officeLocation, ddlDepartmentId);
    }

    protected void btnExport1_Click(object sender, EventArgs e)
    {
        string userid = txtEmpId.Text.Split(':')[0].ToString();
        string[] tmp = txtEmpId.Text.Split(':');
        if (tmp.Length > 1)
        {
            userid = tmp[0].ToString();
        }
        else
        {
            userid = "";
        }

        if (grdAttendanceRecord.Rows.Count != 0)
        {
            string type = userid + "Movement.xls";
            Export(type, grdMovementRecords);
        }
        else
        {
            string validationMsg = "No data in preview list to export. \nPreview the data then export ! ";
            ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + validationMsg + "');",
                    true);
        }
    }
}