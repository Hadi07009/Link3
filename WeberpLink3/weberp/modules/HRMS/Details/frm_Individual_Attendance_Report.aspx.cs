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

public partial class modules_HRMS_Details_frm_Individual_Attendance_Report : System.Web.UI.Page
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
            btnExporttoExcel.Visible = false;
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
        LoadDepartmentIdByuserCode("ADM", dbname.ToString(), rnode.ToString());
        ClsDropDownListController.LoadCheckBoxList(ConnectionString, Sqlgenerate.SqlGetOfficeLocationIntoDDL(), chkofficelocation, "Division_Master_Name", "Division_Master_Code");
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
            return "Please Enter Employee ID !";
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
                    CallLateAttendanceSp();

                    string selectionfor, parameter;
                    selectionfor = "{Hrms_Attendance_Individual.Userid}='" + "ADM" + "'";
                    string CompanyName = "CompanyName" + ":" + Session["CompanyName"].ToString().Split(':')[1].ToString();
                    string CompanyAddress = "CompanyAddress" + ":" + Session["CompanyAddress"].ToString();
                    parameter = CompanyName + ";" + CompanyAddress;
                    string reportname = "../Reports/AttendanceReportIndividual.rpt";
                    ShowReport(selectionfor, parameter, reportname);
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
            var storedProcedureCommandTest = "exec [spGetAttendanceRecordsByDate_Department] '" + dateFrom + "','" + dateTo + "','" + departmentCode + "','" + companyCode + "','" + userId + "'";
            var dtAttendance = StoredProcedureExecutor.StoredProcedureExecuteReader(Session[GlobalData.sessionConnectionstring].ToString(), storedProcedureCommandTest);
            grdAttendanceRecord.DataSource = null;
            grdAttendanceRecord.DataBind();
            if (dtAttendance.Rows.Count > 0)
            {
                grdAttendanceRecord.DataSource = dtAttendance;
                grdAttendanceRecord.DataBind();
                grdAttendanceDetails.DataSource = null;
                grdAttendanceDetails.DataBind();
                btnExporttoExcel.Visible = false;
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
        if (ddlcompany.SelectedValue != "")
        {
            string dateFrom = Convert.ToDateTime(txtFromDate.SelectedDate).ToString("dd-MMM-yyyy");
            string dateTo = Convert.ToDateTime(txtToDate.SelectedDate).ToString("dd-MMM-yyyy");
            LoadAttendanceRecords(dateFrom, dateTo, ddlDepartmentId.SelectedValue, ddlcompany.SelectedValue, Session["EntryUserid"].ToString());
        }
        else
        {
            string validationMsg = "Please Select Company ! ";
            ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + validationMsg + "');",
                    true);
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
            ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + validationMsg + "');",
                    true);
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ddlcompany.SelectedValue != "")
        {
            string dateFrom = Convert.ToDateTime(txtFromDate.SelectedDate).ToString("dd-MMM-yyyy");
            string dateTo = Convert.ToDateTime(txtToDate.SelectedDate).ToString("dd-MMM-yyyy");
            InsertAttendanceRecordsForReport(dateFrom, dateTo, ddlDepartmentId.SelectedValue, ddlcompany.SelectedValue, Session["EntryUserid"].ToString());
            string selectionfor, parameter;
            selectionfor = "{tbl_Attendance_Report.UserId}='" + Session["EntryUserid"].ToString() + "'";
            string CompanyName = "CompanyName" + ":" + Session["CompanyName"].ToString().Split(':')[1].ToString();
            string CompanyAddress = "CompanyAddress" + ":" + Session["CompanyAddress"].ToString();
            parameter = CompanyName + ";" + CompanyAddress;
            string reportname = "../Reports/EmployeeAttendanceReport.rpt";
            ShowReport(selectionfor, parameter, reportname);
        }
        else
        {
            string validationMsg = "First of All Select Company, Then get Report ! ";
            ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + validationMsg + "');",
                    true);
        }
    }
    private string CheckValidation()
    {
        const string checkValidation = "";

        return checkValidation;
    }
    protected void btnPreviewIndividual0_Click(object sender, EventArgs e)
    {
        string officelocation = "";

        string validationMsg = CheckValidation();
        switch (validationMsg)
        {
            case "":
                {
                    foreach (ListItem lst in chkofficelocation.Items)
                    {
                        if (lst.Selected)
                        {
                            if (officelocation == "")
                            {
                                officelocation += "" + lst.Value.ToString() + "";
                            }
                            else
                            {
                                officelocation += "','" + lst.Value.ToString() + "";
                            }
                        }
                    }

                    officelocation = "('" + officelocation + "')";

                    string sql = "";

                    string empcategory = ddlEmpCategory.SelectedItem.Value;

                    if (empcategory == "-1")
                    {
                        if (ddlDepartmentId.SelectedValue == "-1")
                        {
                            sql = "create view viewForAttendanceReport as select EmpID,Dept,Sect,Designation from Emp_Details where OfficeID in " + officelocation + "";
                        }
                        else
                        {
                            sql = "create view viewForAttendanceReport as select EmpID,Dept,Sect,Designation from Emp_Details where OfficeID in " + officelocation + " and DeptID = '" + ddlDepartmentId.SelectedValue + "'";

                        }
                    }
                    else
                    {
                        if (ddlDepartmentId.SelectedValue == "-1")
                        {
                            sql = "create view viewForAttendanceReport as select EmpID,Dept,Sect,Designation from Emp_Details where OfficeID in " + officelocation + " and emptype='" + empcategory + "'";
                        }
                        else
                        {
                            sql = "create view viewForAttendanceReport as select EmpID,Dept,Sect,Designation from Emp_Details where OfficeID in " + officelocation + " and DeptID = '" + ddlDepartmentId.SelectedValue + "' and emptype='" + empcategory + "'";

                        }
                    }

                    DateTime releasedate;
                    releasedate = txtToDate.SelectedDate;

                    sql = sql + " and EmpID not in(select Emp_id from hrms_emp_Settlement where Rel_Date<Convert(Datetime,'" + releasedate + "',103))";



                    DataProcess.ExecuteQuery(Session[GlobalData.sessionConnectionstring].ToString(), "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[viewForAttendanceReport]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[viewForAttendanceReport]");
                    DataProcess.ExecuteQuery(Session[GlobalData.sessionConnectionstring].ToString(), sql);

                    CallLateAttendanceSpALL();

                    string selectionfor, parameter;
                    selectionfor = "{Hrms_Attendance_Individual.Userid}='" + current.UserId + "'";
                    string CompanyName = "CompanyName" + ":" + Session["CompanyName"].ToString().Split(':')[1].ToString();
                    string CompanyAddress = "CompanyAddress" + ":" + Session["CompanyAddress"].ToString();
                    string EmpCategory = "EmpCategory" + ":" + ddlEmpCategory.SelectedItem.Text;


                    parameter = CompanyName + ";" + CompanyAddress + ";" + EmpCategory;

                    string lateOrAtten = "";
                    if (RadioButtonList2.SelectedIndex == 0)
                    {
                        lateOrAtten = "tag" + ":" + "Late Report";

                    }
                    else
                    {
                        lateOrAtten = "tag" + ":" + "Attendance Report";
                    }
                    parameter = parameter + ";" + lateOrAtten;


                    string reportname = "";
                    if (txtEmpId.Text == string.Empty)
                    {
                        string fdate = "fromDate" + ":" + txtFromDate.SelectedDate.ToString("dd-MMM-yyyy");
                        string lDate = "toDate" + ":" + txtToDate.SelectedDate.ToString("dd-MMM-yyyy");
                        parameter = parameter + ";" + fdate + ";" + lDate;
                        reportname = "../Reports/AttendanceReportOfficeLocationWise.rpt";
                        //reportname = "../Reports/AttendanceReportOfficeLocationWiseIndiv.rpt";

                    }
                    else
                    {
                        string fdate = "fromDate" + ":" + txtFromDate.SelectedDate.ToString("dd-MMM-yyyy");
                        string lDate = "toDate" + ":" + txtToDate.SelectedDate.ToString("dd-MMM-yyyy");
                        parameter = parameter + ";" + fdate + ";" + lDate;

                        reportname = "../Reports/AttendanceReportOfficeLocationWiseIndiv.rpt";
                    }

                    ShowReport(selectionfor, parameter, reportname);
                }
                break;
            default:
                ScriptManager.RegisterStartupScript(this, GetType(), "MessageBox", "alert(' " + validationMsg + " ');", true);
                break;
        }


    }

    private void ImportDataOnView(string officeLocation, string department)
    {
        string sql = "alter view viewForAttendanceReport as select EmpID,Dept,Sect,Designation from Emp_Details where OfficeID in " + officeLocation + " and DeptID = '" + department + "'";
        DataProcess.ExecuteQuery(Session[GlobalData.sessionConnectionstring].ToString(), "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[viewForAttendanceReport]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[viewForAttendanceReport]");
        DataProcess.ExecuteQuery(Session[GlobalData.sessionConnectionstring].ToString(), sql);

    }
    private void CallLateAttendanceSpALL()
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
        string al = lvp.spCxecuteAbsentListAttendanceALL(Session["ConnectionStr"].ToString(), fdate, lDate, deptid, empid, cond, rnode.ToString(), Session["db"].ToString(), userid.ToString());

    }
    protected void btnExportAttendanceDetails_Click(object sender, EventArgs e)
    {
        try
        {
            ShowAttendanceRecord();
        }
        catch (Exception msgException)
        {
            MessageBox1.ShowError(msgException.Message);

        }
    }

    private void ShowAttendanceRecord()
    {
        try
        {
            DateTime startDate = txtFromDate.SelectedDate;
            DateTime endDate = txtToDate.SelectedDate;
            DateTime targetDate = txtFromDate.SelectedDate;

            string sqlQuery = @" SELECT DISTINCT 1 AS SLNO, Atnd_Det_Emp_Id AS [Emp. ID],b.EmpName AS Name, b.Designation";
            string sqlQuery2 = @" SELECT DISTINCT 2 AS SLNO, Atnd_Det_Emp_Id AS [Emp. ID],b.EmpName AS Name, b.Designation";
            var dateColumn = 1;
            while (targetDate <= endDate)
            {
                string hd1 = "[" + string.Format("{0:00}", dateColumn) + "]";
                string hd2 = "[ " + string.Format("{0:00}", dateColumn) + "]";

                sqlQuery = sqlQuery + @",isnull((SELECT case 
                when  atnd_det_offlg='L' then 'Leave' 
                when  atnd_det_offlg='N' then 'Leave' 
                when  atnd_det_offlg='H' then 'Holiday' 
                else  Atnd_det_intime end 
                FROM hrms_atnd_det WHERE CONVERT(DATETIME,Atnd_det_date,103) = CONVERT(DATETIME,'" + targetDate + "',103) AND Atnd_Det_Emp_Id = a.Atnd_Det_Emp_Id),'A') as " + hd1 + ","
                + " isnull((SELECT case"
                + " when  atnd_det_offlg='L' then ''"
                + " when  atnd_det_offlg='N' then ''"
                + " when  atnd_det_offlg='H' then ''"
                + " else  Atnd_det_outtime end "
                + " FROM hrms_atnd_det WHERE CONVERT(DATETIME,Atnd_det_date,103) = CONVERT(DATETIME,'" + targetDate + "',103)  AND Atnd_Det_Emp_Id = a.Atnd_Det_Emp_Id),'') as " + hd2 + "";

                sqlQuery2 = sqlQuery2 + @",(SELECT case 
                when  atnd_det_offlg='L' then '' 
                when  atnd_det_offlg='N' then '' 
                when  atnd_det_offlg='H' then '' 
                else  [dbo].[LateByEmployeeCode](Atnd_det_intime,Atnd_Det_sftID,Atnd_det_offlg) end 
                FROM hrms_atnd_det WHERE CONVERT(DATETIME,Atnd_det_date,103) = CONVERT(DATETIME,'" + targetDate + "',103) AND Atnd_Det_Emp_Id = a.Atnd_Det_Emp_Id) as " + hd1 + ","
                + " (SELECT case"
                + " when  atnd_det_offlg='L' then ''"
                + " when  atnd_det_offlg='N' then ''"
                + " when  atnd_det_offlg='H' then ''"
                + " else  [dbo].[EarlyByEmployee](Atnd_det_outtime,Atnd_Det_sftID,Atnd_det_offlg) end "
                + " FROM hrms_atnd_det WHERE CONVERT(DATETIME,Atnd_det_date,103) = CONVERT(DATETIME,'" + targetDate + "',103)  AND Atnd_Det_Emp_Id = a.Atnd_Det_Emp_Id) as " + hd2 + "";


                dateColumn++;
                targetDate = targetDate.AddDays(1);
            }

            sqlQuery = sqlQuery + @","
            + " cast((select isnull(SUM(Leave_Det_Emp_Days),'0') from HrMs_Emp_Leave_Det where Leave_Det_Emp_Id= a.Atnd_Det_Emp_Id and Leave_Det_LCode='CL'"
            + " and CONVERT(DATETIME, Leave_Det_Sta_Date,103) between CONVERT(DATETIME,'" + startDate + "',103) and CONVERT(DATETIME,'" + endDate + "',103)) as varchar(10)) AS CL,"
            + " cast((select isnull(SUM(Leave_Det_Emp_Days),0) from HrMs_Emp_Leave_Det where Leave_Det_Emp_Id= a.Atnd_Det_Emp_Id and Leave_Det_LCode='SL'"
            + " and CONVERT(DATETIME, Leave_Det_Sta_Date,103) between CONVERT(DATETIME,'" + startDate + "',103) and CONVERT(DATETIME,'" + endDate + "',103))as varchar(10)) AS SL,"
            + " cast((select isnull(SUM(Leave_Det_Emp_Days),0) from HrMs_Emp_Leave_Det where Leave_Det_Emp_Id= a.Atnd_Det_Emp_Id and Leave_Det_LCode='AL'"
            + " and CONVERT(DATETIME, Leave_Det_Sta_Date,103) between CONVERT(DATETIME,'" + startDate + "',103) and CONVERT(DATETIME,'" + endDate + "',103))as varchar(10)) AS AL,"
            + " cast((SELECT [dbo].[LateCountByEmpId](CONVERT(DATETIME,'" + startDate + "',103),CONVERT(DATETIME,'" + endDate + "',103),a.Atnd_Det_Emp_Id))as varchar(10)) AS LI, "
            + " cast((SELECT [dbo].[EarlyCountByEmpId](CONVERT(DATETIME,'" + startDate + "',103),CONVERT(DATETIME,'" + endDate + "',103),a.Atnd_Det_Emp_Id))as varchar(10)) AS EO, "
            + " cast((SELECT COUNT(pf.TransactionNo) FROM [ProcessFlowdet] pf INNER JOIN ProcessFlowDetAttendance pfa ON pf.TransactionNo = pfa.TransactionNo  WHERE pf.ProcessID = 'P002' AND pf.ActionTypeId = 5 AND CONVERT(DATETIME,pfa.Trndate,103) BETWEEN CONVERT(DATETIME,'" + startDate + "',103) and CONVERT(DATETIME,'" + endDate + "',103) AND pf.ApplicantId = a.Atnd_Det_Emp_Id) as varchar(10)) AS BL";

            sqlQuery2 = sqlQuery2 + @","
            + " '' AS CL,"
            + " '' SL,"
            + " '' AS AL,"
            + " '' AS LI ,"
            + " '' AS EO ,"
            + " '' AS BL ";

            string dateCondition = @" FROM hrms_atnd_det a INNER JOIN Emp_Details b on a.Atnd_Det_Emp_Id = b.EmpID  WHERE CONVERT(DATETIME,Atnd_det_date,103) BETWEEN CONVERT(DATETIME,'" + startDate + "',103) and CONVERT(DATETIME,'" + endDate + "',103)";
            sqlQuery = sqlQuery + dateCondition;
            sqlQuery2 = sqlQuery2 + dateCondition;





            string officelocation = "";
            foreach (ListItem lst in chkofficelocation.Items)
            {
                if (lst.Selected)
                {
                    if (officelocation == "")
                    {
                        officelocation += "" + lst.Value.ToString() + "";
                    }
                    else
                    {
                        officelocation += "','" + lst.Value.ToString() + "";
                    }
                }
            }
            if (officelocation != "")
            {
                officelocation = "('" + officelocation + "')";
                sqlQuery = sqlQuery + " AND b.OfficeID IN " + officelocation + "";
                sqlQuery2 = sqlQuery2 + " AND b.OfficeID IN " + officelocation + "";

            }

            if (ddlDepartmentId.SelectedValue != "-1")
            {
                sqlQuery = sqlQuery + " AND b.DeptID = '" + ddlDepartmentId.SelectedValue + "'";
                sqlQuery2 = sqlQuery2 + " AND b.DeptID = '" + ddlDepartmentId.SelectedValue + "'";
            }

            string empcategory = ddlEmpCategory.SelectedItem.Value;
            if (empcategory != "-1")
            {
                sqlQuery = sqlQuery + " AND b.emptype = '" + empcategory + "'";
                sqlQuery2 = sqlQuery2 + " AND b.emptype = '" + empcategory + "'";
            }

            string employeeCode = txtEmpId.Text == string.Empty ? null : txtEmpId.Text;
            if (employeeCode != null)
            {
                employeeCode = employeeCode.Trim().Split(':')[0].ToString();
                sqlQuery = sqlQuery + " AND b.EmpID = '" + employeeCode + "'";
                sqlQuery2 = sqlQuery2 + " AND b.EmpID = '" + employeeCode + "'";
            }


            sqlQuery = sqlQuery + " UNION " + sqlQuery2 + " ORDER BY Atnd_Det_Emp_Id,SLNO";



            var dtAttendanceRecord = DataProcess.GetData(ConnectionString, sqlQuery);
            grdAttendanceDetails.DataSource = null;
            grdAttendanceDetails.DataBind();
            btnExporttoExcel.Visible = false;
            if (dtAttendanceRecord.Rows.Count > 0)
            {
                grdAttendanceDetails.DataSource = dtAttendanceRecord;
                grdAttendanceDetails.DataBind();
                btnExporttoExcel.Visible = true;
                grdAttendanceRecord.DataSource = null;
                grdAttendanceRecord.DataBind();
            }
        }
        catch (Exception msgExecption)
        {

            throw msgExecption;
        }
    }

    public static void MergeRows(GridView gridView)
    {
        try
        {
            for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex + 1];
                if (row.Cells[1].Text == previousRow.Cells[1].Text)
                {
                    for (int i = 1; i < 4; i++)
                    {

                        if (row.Cells[i].Text == previousRow.Cells[i].Text)
                        {
                            row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                                                       previousRow.Cells[i].RowSpan + 1;
                            previousRow.Cells[i].Visible = false;
                        }
                    }
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }


    protected void grdAttendanceDetails_PreRender(object sender, EventArgs e)
    {
        MergeRows(grdAttendanceDetails);
    }
    protected void btnExporttoExcel_Click(object sender, EventArgs e)
    {
        if (grdAttendanceDetails.Rows.Count != 0)
        {
            const string type = "Monthly Attendance.xls";
            ExportGridToExcel.Export(type, grdAttendanceDetails);
        }
        else
        {
            MessageBox1.ShowInfo("There is no data for Export ! ");
        }
    }
    protected void grdAttendanceDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            int countHeader = e.Row.Cells.Count;
            for (int i = 3; i < countHeader - 5; i++)
            {
                string headerText = e.Row.Cells[i].Text.Trim();
                string nextHeaderText = e.Row.Cells[i + 1].Text.Trim();
                if (headerText == nextHeaderText)
                {
                    e.Row.Cells[i].ColumnSpan = e.Row.Cells[i + 1].ColumnSpan < 2 ? 2 :
                    e.Row.Cells[i + 1].ColumnSpan + 1;
                    e.Row.Cells[i + 1].Visible = false;
                }

            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            var flagText = @"For Casual Leave =	CL, For Medical Leave =	SL, For Annual Leave = AL, Absent =	A, Daily Late In = LI, Daily Early Out = EO, For Business Leave = BL";
            e.Row.Cells[3].Text = flagText.ToString();
            int countFooter = e.Row.Cells.Count;

            e.Row.Cells[3].ColumnSpan = countFooter;
            for (int i = 4; i < countFooter ; i++)
            {
                e.Row.Cells[i].Visible = false;
            }
        }
        e.Row.Cells[0].Visible = false;
    }

    protected void btnPreviewMonthlyReport_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();

        string dateFrom = Convert.ToDateTime(txtFromDate.SelectedDate).ToString("dd-MMM-yyyy");
        string dateTo = Convert.ToDateTime(txtToDate.SelectedDate).ToString("dd-MMM-yyyy");

        int mon = Convert.ToDateTime(dateFrom).Month;
        int yr = Convert.ToDateTime(dateFrom).Year;
        string wdays = "";
        dt = DataProcess.GetData(ConnectionString, "select [dbo].[getnumberofday](" + mon + ","+ yr +") as nod ");
        wdays = dt.Rows[0]["nod"].ToString();
        
               
        string sql = @"Create view ViewMonthlyAttedance as "
                + " select b.Emp_Mas_Emp_Id,isnull(sum(case when Atnd_det_offlg='H' and Emp_Mas_Join_Date<=Atnd_det_date then 1 else 0 end),0) as Holiday,"
                + " isnull((CASE WHEN b.Emp_Mas_Join_Date BETWEEN CONVERT(datetime, '" + dateFrom + "', 103)" 
                + " AND CONVERT(datetime,'" + dateTo + "', 103) THEN (day(CONVERT(datetime,'" + dateTo + "', 103))-day(b.Emp_Mas_Join_Date))+ 1" 
                + " ELSE day(CONVERT(datetime, '" + dateTo + "', 103)) END),0) as Wrk_days,"
                + " sum(case when Atnd_det_offlg='L' and c.Leave_Det_LCode<>'NL' and Emp_Mas_Join_Date<=Atnd_det_date then 1 else 0 end)"
                + " +sum(case when Atnd_det_offlg='N' and Emp_Mas_Join_Date<=Atnd_det_date then 1 else 0 end) as LeaveDays,"
                + " sum(case when Atnd_det_offlg='L' and Emp_Mas_Join_Date<=Atnd_det_date and c.Leave_Det_LCode='NL' then 1 else 0 end) as Lwp,"
                + " sum(case when Atnd_det_offlg in('A','O') and Emp_Mas_Join_Date<=Atnd_det_date then 1 else 0 end) as prsdays"
                + " from hrms_atnd_det a" 
                + " inner join HrMs_Emp_mas b on a.Atnd_Det_Emp_Id=b.Emp_Mas_Emp_Id and Emp_Mas_Term_Flg<>'Y'"
                + " left outer join HrMs_Emp_Leave_Det c on c.Leave_Det_Emp_Id=a.Atnd_Det_Emp_Id and c.Leave_Det_Sta_Date=a.Atnd_det_date"
                + " where Atnd_det_date between CONVERT(Datetime,'" + dateFrom + "',103) and CONVERT(Datetime,'" + dateTo + "',103) and Emp_Mas_Join_Date<=Atnd_det_date "
                + " group by b.Emp_Mas_Emp_Id,Emp_Mas_Join_Date";

        DataProcess.ExecuteQuery(Session[GlobalData.sessionConnectionstring].ToString(), "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ViewMonthlyAttedance]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[ViewMonthlyAttedance]");
        DataProcess.ExecuteQuery(Session[GlobalData.sessionConnectionstring].ToString(), sql);


        string selectionfor, parameter, CompanyName, CompanyAddress;

        CompanyName = "CompanyName" + ":" + Session["CompanyName"].ToString().Split(':')[1].ToString();
        CompanyAddress = "CompanyAddress" + ":" + Session["CompanyAddress"].ToString();
        wdays = "wdays" + ":" + wdays;
        dateFrom = "dateFrom" + ":" + dateFrom;
        dateTo = "dateTo" + ":" + dateTo;

       
        parameter = CompanyName + ";" + CompanyAddress + ";" + wdays + ";" + dateFrom + ";" + dateTo;
        selectionfor = "";            
        string reportname = "../Reports/AttendanceSummary.rpt";
        ShowReport(selectionfor, parameter, reportname);


    }
}