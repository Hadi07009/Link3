using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using ADODB;
using CrystalDecisions;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using LibraryDAL;
using System.Web.Security;
using System.Configuration;
using System.Net.Mail;
using System.Net;

public partial class modules_HRMS_Details_frmAttendanceReportMail : System.Web.UI.Page
{
    string ConnectionStr = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        if (!IsPostBack)
        {
            DateTime dt = DateTime.Now.AddDays(-30);
            txtdaterange1.Text = dt.ToString("dd/MM/yyyy");
            DateTime dt2 = DateTime.Now;
            txtdaterange2.Text = dt2.ToString("dd/MM/yyyy");
            LoadDepartment();
            LoadGrid();
            LoadCombo();
            Session[StaticData.sessionUserId] = "SSP-0001";
        }
    }

    private void LoadGrid()
    {
        DataTable dt = new DataTable();
        string sql = "select distinct 1 as SL,b.Dept,b.Sect,b.Office,a.EmpType,EmailFrom,EmailTo,EmailCc,Subject,EmailBody,Refid,"
                  + "a.[Status] AS valueStatus,CASE WHEN (a.[Status] = '1') THEN 'Active' WHEN (a.[Status] = '0') THEN 'Inactive' ELSE '' END AS TxtStatus"
                  + " from [HrmsAttendanceMailConfigure] a"
                  + " inner join Emp_Details b on a.Deptid=b.DeptID and a.Sectid=b.SectID and a.Location=b.OfficeID";
        dt = DataProcess.GetData(ConnectionStr, sql);
        gdvConfig.DataSource = dt;
        gdvConfig.DataBind();
    }

    private void LoadDepartment()
    {
        DataTable dt = new DataTable();
        dt = DataProcess.GetData(ConnectionStr, "select distinct a.DeptID,a.Dept from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id and Emp_Mas_Term_Flg<>'Y' order by a.Dept");
        foreach (DataRow dr in dt.Rows)
        {
            ListItem lst = new ListItem();
            lst.Text = dr["Dept"].ToString();
            lst.Value = dr["DeptID"].ToString();
            ChkLisBoxDepartment.Items.Add(lst);
        }
    }

    private void LoadDeptCombo()
    {
        DataTable dt = new DataTable();
        ddlDept.Items.Clear();
        ddlSection.Items.Clear();
        ddlDept.Items.Add("");
        dt = DataProcess.GetData(ConnectionStr, "select distinct a.DeptID,a.Dept from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id and Emp_Mas_Term_Flg<>'Y' order by a.Dept");
        foreach (DataRow dr in dt.Rows)
        {
            ListItem lst = new ListItem();
            lst.Text = dr["Dept"].ToString();
            lst.Value = dr["DeptID"].ToString();
            ddlDept.Items.Add(lst);
        }
    }

    private void LoadSectionCombo(string deptid)
    {
        DataTable dt = new DataTable();
        ddlSection.Items.Clear();
        ddlLocation.Items.Clear();
        ddlSection.Items.Add("");
        dt = DataProcess.GetData(ConnectionStr, "select distinct a.Sectid,a.Sect from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id and Emp_Mas_Term_Flg<>'Y' and a.DeptID='" + deptid.ToString() + "' order by a.Sect");
        foreach (DataRow dr in dt.Rows)
        {
            ListItem lst = new ListItem();
            lst.Text = dr["Sect"].ToString();
            lst.Value = dr["Sectid"].ToString();
            ddlSection.Items.Add(lst);
        }
    }

    private void LoadLocationCombo(string Sectid)
    {
        DataTable dt = new DataTable();
        ddlLocation.Items.Clear();
        ddlLocation.Items.Add("");
        dt = DataProcess.GetData(ConnectionStr, "select distinct a.officeid,a.office from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id and Emp_Mas_Term_Flg<>'Y' and a.Sectid='" + Sectid.ToString() + "'");
        foreach (DataRow dr in dt.Rows)
        {
            ListItem lst = new ListItem();
            lst.Text = dr["office"].ToString();
            lst.Value = dr["officeid"].ToString();
            ddlLocation.Items.Add(lst);
        }
    }

    private void LoadEmployeeTypeCombo(string officeid)
    {
        DataTable dt = new DataTable();
        ddlEmpType.Items.Add("");
        dt = DataProcess.GetData(ConnectionStr, "select distinct a.EmpType from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id and Emp_Mas_Term_Flg<>'Y' and a.officeid='" + officeid.ToString() + "'");
        foreach (DataRow dr in dt.Rows)
        {
            ListItem lst = new ListItem();
            lst.Text = dr["empType"].ToString();
            lst.Value = dr["empType"].ToString();
            ddlSection.Items.Add(lst);
        }
    }

    private void LoadCombo()
    {
        DataTable dt = new DataTable();
        string sql = "";
        ddlDept.Items.Clear();
        ddlDept.Items.Add("");
        dt = DataProcess.GetData(ConnectionStr, "select distinct a.DeptID,a.Dept from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id and Emp_Mas_Term_Flg<>'Y' order by a.Dept");
        foreach (DataRow dr in dt.Rows)
        {
            ListItem lst = new ListItem();
            lst.Text = dr["Dept"].ToString();
            lst.Value = dr["DeptID"].ToString();
            ddlDept.Items.Add(lst);
        }
        ddlSection.Items.Clear();
        ddlSection.Items.Add("");
        dt = DataProcess.GetData(ConnectionStr, "select distinct a.Sectid,a.Sect from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id and Emp_Mas_Term_Flg<>'Y' order by a.Sect");
        foreach (DataRow dr in dt.Rows)
        {
            ListItem lst = new ListItem();
            lst.Text = dr["Sect"].ToString();
            lst.Value = dr["Sectid"].ToString();
            ddlSection.Items.Add(lst);
        }
        ddlLocation.Items.Clear();
        ddlLocation.Items.Add("");
        dt = DataProcess.GetData(ConnectionStr, "select distinct a.officeid,a.office from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id and Emp_Mas_Term_Flg<>'Y'");
        foreach (DataRow dr in dt.Rows)
        {
            ListItem lst = new ListItem();
            lst.Text = dr["office"].ToString();
            lst.Value = dr["officeid"].ToString();
            ddlLocation.Items.Add(lst);
        }
        sql = "select distinct EmpType from Emp_Details";
        dt = DataProcess.GetData(ConnectionStr, sql);
        ddlEmpType.Items.Clear();
        ddlEmpType.Items.Add("");
        foreach (DataRow dr in dt.Rows)
        {
            ListItem lst = new ListItem();
            lst.Text = dr["EmpType"].ToString();
            lst.Value = dr["EmpType"].ToString();
            ddlEmpType.Items.Add(lst);
        }
        ClsDropDownListController.LoadddlStatus(ddlStatus);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Recordset rec = new Recordset();
        Recordset rec1 = new Recordset();
        Recordset RecOBal = new Recordset();
        ADODB.Connection DC = new ADODB.Connection();
        string constr, str, sql;
        object dummy = Type.Missing;
        DateTime dtfr, dtto;
        DateTime dt = Convert.ToDateTime(txtdaterange1.Text);
        DateTime dt2 = Convert.ToDateTime(txtdaterange2.Text);
        constr = System.Configuration.ConfigurationManager.AppSettings["ConStrLNK"].ToString();
        DC.Open(constr, null, null, 0);
        CrystalReportSource1.EnableCaching = false;
        sql = "if exists(Select * from sysobjects where name = 'view_lwp' and type = 'V' ) begin drop view view_lwp end  ";
        DC.Execute(sql, out dummy, 1);
        sql = " create view view_lwp as SELECT HrMs_Emp_Leave_Det.Leave_Det_Emp_Id,count (HrMs_Emp_Leave_Det.Leave_Det_Emp_Id) as lwp "
            + " FROM HrMs_Emp_Leave_Det INNER JOIN Hrms_Trans_Det ON HrMs_Emp_Leave_Det.Leave_Det_Emp_Id = Hrms_Trans_Det.Trans_Det_Emp_Id "
            + " Inner Join HRMS_Leave_Mas ON HrMs_Emp_Leave_Det.Leave_Det_LCode = HRMS_Leave_Mas.Leave_Mas_Code AND "
            + " Hrms_Trans_Det.Trans_Det_Emptype = HRMS_Leave_Mas.T_C2 WHERE (HRMS_Leave_Mas.Leave_Mas_Mode_Of_Pay = 'NP') and "
            + " (HrMs_Emp_Leave_Det.leave_det_sta_date between convert(datetime,'" + dt + "',103) and convert(datetime,'" + dt2 + "',103)) GROUP BY HrMs_Emp_Leave_Det.Leave_Det_Emp_Id";
        DC.Execute(sql, out dummy, 1);
        sql = "if exists(Select * from sysobjects where name = 'leave_in_holiday' and type = 'V' ) begin drop view leave_in_holiday end  ";
        DC.Execute(sql, out dummy, 1);
        sql = "create view leave_in_holiday as SELECT HrMs_Emp_Leave_Det.Leave_Det_Emp_Id, "
           + " count (HrMs_Emp_Leave_Det.Leave_Det_Emp_Id)  AS DayCount FROM HrMs_Emp_Leave_Det INNER JOIN "
           + " hrms_holiday ON HrMs_Emp_Leave_Det.Leave_Det_Sta_Date = hrms_holiday.holiday_date WHERE (HrMs_Emp_Leave_Det.Leave_Det_Sta_Date BETWEEN "
           + " CONVERT(datetime, '" + dt + "', 103) AND  CONVERT(datetime,'" + dt2 + "', 103)) GROUP BY HrMs_Emp_Leave_Det.Leave_Det_Emp_Id ";
        DC.Execute(sql, out dummy, 1);
        sql = "if exists(Select * from sysobjects where name = 'count_emp_holiday' and type = 'V' ) begin drop view count_emp_holiday end  ";
        DC.Execute(sql, out dummy, 1);
        sql = "create view count_emp_holiday as SELECT HrMs_Emp_mas.Emp_Mas_Emp_Id,COUNT(hrms_holiday.holiday_date) As Holiday, "
           + " Wrk_days = CASE WHEN dbo.HrMs_Emp_mas.Emp_Mas_Join_Date BETWEEN CONVERT(datetime, '" + dt + "', 103) AND CONVERT(datetime,'" + dt2 + "', 103) THEN  "
           + " (day(CONVERT(datetime,'" + dt2 + "', 103)) - day(dbo.HrMs_Emp_mas.Emp_Mas_Join_Date)) "
           + "  + 1 ELSE day(CONVERT(datetime, '" + dt2 + "', 103)) END FROM HrMs_Emp_mas LEFT OUTER JOIN hrms_holiday ON HrMs_Emp_mas.Emp_Mas_Join_Date <= hrms_holiday.holiday_date "
           + " WHERE (hrms_holiday.holiday_date BETWEEN CONVERT(datetime, '" + dt + "', 103) AND  CONVERT(datetime,'" + dt2 + "', 103))"
           + " GROUP BY HrMs_Emp_mas.Emp_Mas_Emp_Id,dbo.HrMs_Emp_mas.Emp_Mas_Join_Date";
        DC.Execute(sql, out dummy, 1);
        show_report4();
    }

    private void ShowReport(string reportname, string selectionfor)
    {
        clsReport rpt = new clsReport();
        ParameterFields myParams = new ParameterFields();
        ConnectionInfo ConnInfo = new ConnectionInfo();
        string SCFconnStr = ConnectionStr.ToString();
        string[] ff;
        string[] ss;
        string[] prm;        
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
        rpt.SelectionFormulla = selectionfor;
        Session[GlobalData.sessionReportDet] = rpt;
        RegisterStartupScript("click", "<script>window.open('./frm_report_viewer.aspx');</script>");
    }
    
    private void show_report4()
    {
        ADODB.Connection DC = new ADODB.Connection();
        string rep = "";
        string[] ff, ss;
        ConnectionInfo ConnInfo = new ConnectionInfo();
        string constr = System.Configuration.ConfigurationManager.AppSettings["ConStrLNK"].ToString();
        DC.Open(constr, null, null, 0);
        string reportname = "../Reports/Dept_Atn_Rpt_sum.rpt";
        ShowReport(reportname,"");
    }

    protected void btnAttendanceMail_Click(object sender, EventArgs e)
    {
        DateTime dtfr = Convert.ToDateTime(txtdaterange1.Text);
        DateTime dtfr2 = Convert.ToDateTime(txtdaterange2.Text);
        DataTable dt = new DataTable();
        string Deptid = "";
        string Sectid = "";
        string EmpType = "";
        string Location = "";
        string Emailfrom = "";
        string EmailTo = "";
        string EmailCc = "";
        string Subject = "";
        string EmailBody = "";
        string date1 = "";
        string date2 = "";
        string sql = "";
        date1 = "date(" + dtfr.Year + "," + string.Format("{0:00}", dtfr.Month) + "," + string.Format("{0:00}", dtfr.Day) + ")";
        date2 = "date(" + dtfr2.Year + "," + string.Format("{0:00}", dtfr2.Month) + "," + string.Format("{0:00}", dtfr2.Day) + ")";
        string ConnectionStr = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
        List<ListItem> selected = new List<ListItem>();
        foreach (ListItem item in ChkLisBoxDepartment.Items)
        {
            if (item.Selected)
            {
                sql = "select Deptid,Sectid,EmpType,Location,EmailFrom,EmailTo,EmailCc,Subject,EmailBody from HrmsAttendanceMailConfigure where Status='1' and Deptid='" + item.Value + "'"
                         + " group by Deptid,Sectid,EmpType,Location,EmailFrom,EmailTo,EmailCc,Subject,EmailBody";
                dt = DataProcess.GetData(ConnectionStr, sql);
                foreach (DataRow dr in dt.Rows)
                {
                    Deptid = dr["Deptid"].ToString();
                    Sectid = dr["Sectid"].ToString();
                    EmpType = dr["EmpType"].ToString();
                    Location = dr["Location"].ToString();
                    Emailfrom = dr["EmailFrom"].ToString();
                    EmailTo = dr["EmailTo"].ToString();
                    EmailCc = dr["EmailCc"].ToString();
                    Subject = dr["Subject"].ToString();
                    EmailBody = dr["EmailBody"].ToString();
                    Sendmail(Emailfrom, "", EmailTo, EmailCc, Subject, EmailBody, Deptid, Sectid, EmpType, Location, date1, date2);
                }
            }
        }
    }

    private void SendAttendanceMail()
    {
        //Sendmail("n.islam@link3.net", "Nazrul", "n.islam@link3.net", "Nazrul", "", "Attendance", "Test", "L3T593", "n.islam@link3.net"); 
    }

    private void ShowAttendanceReport()
    {
        ADODB.Connection DC = new ADODB.Connection();
        DateTime dtfr, dtto, dtfr1, dtfr2;
        string[] ff, ss;
        string str, vcoa, vac;
        ParameterFields paramFields = new ParameterFields();
        ConnectionInfo ConnInfo = new ConnectionInfo();
        string constr = System.Configuration.ConfigurationManager.AppSettings["ConStrLNK"].ToString();
        DC.Open(constr, null, null, 0);
        string crdate = DateTime.Now.Day.ToString();
        DateTime crdate1 = DateTime.Now.Date;
        dtfr = Convert.ToDateTime(txtdaterange1.Text);
        dtfr2 = Convert.ToDateTime(txtdaterange2.Text);
        string strdtfrm = dtfr.Year.ToString() + "," + dtfr.Month.ToString() + "," + dtfr.Day.ToString();
        string strdtfrm2 = dtfr2.Year.ToString() + "," + dtfr2.Month.ToString() + "," + dtfr2.Day.ToString();
        ff = constr.Split('=');
        ss = ff[2].Split(';');
        ConnInfo.ServerName = ss[0];
        ss = ff[3].Split(';');
        ConnInfo.DatabaseName = ss[0];
        ss = ff[4].Split(';');
        ConnInfo.UserID = ss[0];
        ss = ff[5].Split(';');
        ConnInfo.Password = ss[0];
        string rep = "";
        clsReport rpt = new clsReport();
        rpt.FileName = "../REPORT/AttendanceMail.rpt";
        rpt.ConnectionInfo = ConnInfo;
        rpt.ParametersFields = paramFields;
        rpt.SelectionFormulla = rep;
        string qrystr = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
        Cache.Insert(qrystr, rpt);
        RegisterStartupScript("Click", "<script>window.open('./frm_report_viewer.aspx?session_id_no=" + qrystr + "');</script>");
    }

    private bool Sendmail(string sid, string sname, string rid, string ccid, string msub, string mbody, string Deptid, string Sectid, string EmpType, string Location, string date1, string date2)
    {
        bool flg = false;
        ReportDocument crystalReport = new ReportDocument();
        crystalReport.Load(Server.MapPath("../Reports/AttendanceMail.rpt"));
        crystalReport.RecordSelectionFormula = "{Hrms_Dept_Master.Dept_Code }='" + Deptid.ToString() + "' and {Hrms_Atnd_Det.Atnd_det_date}>=" + date1 + " and {Hrms_Atnd_Det.Atnd_det_date}<=" + date2 + "";
        if (Sectid != "")
        {
            crystalReport.RecordSelectionFormula = crystalReport.RecordSelectionFormula + " and {Hrms_Sect_Mas.Sect_Code}='" + Sectid.ToString() + "'";
        }
        if (EmpType != "")
        {
            crystalReport.RecordSelectionFormula = crystalReport.RecordSelectionFormula + "and {Emp_Details.EmpType}='" + EmpType.ToString() + "'";
        }
        if (Location != "")
        {
            crystalReport.RecordSelectionFormula = crystalReport.RecordSelectionFormula + "and {Hrms_Division_Master.Division_Master_Code}='" + Location.ToString() + "'";
        }
        SetDBLogonForReport(ref crystalReport);
        try
        {
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("mailx.link3.net");
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.From = new System.Net.Mail.MailAddress(sid, sname);
            msg.To.Add(new System.Net.Mail.MailAddress(rid));
            string[] cary = ccid.Split(':');
            if (cary.Length >= 1)
            {
                for (int i = 0; i < cary.Length; i++)
                {
                    if (cary[i].Trim().ToString() != "")
                    {
                        msg.CC.Add(new System.Net.Mail.MailAddress(cary[i].Trim()));
                    }
                }
            }
            msg.Attachments.Add(new Attachment(crystalReport.ExportToStream(ExportFormatType.PortableDocFormat), "AttendanceDetails.pdf"));
            ReportDocument crystalReport2 = new ReportDocument();
            crystalReport2.Load(Server.MapPath("../Reports/AttendanceMailSummery.rpt"));
            crystalReport2.RecordSelectionFormula = "{Hrms_Dept_Master.Dept_Code }='" + Deptid.ToString() + "' and {Hrms_Atnd_Det.Atnd_det_date}>=" + date1 + " and {Hrms_Atnd_Det.Atnd_det_date}<=" + date2 + "";

            if (Sectid != "")
            {
                crystalReport2.RecordSelectionFormula = crystalReport.RecordSelectionFormula + " and {Hrms_Sect_Mas.Sect_Code}='" + Sectid.ToString() + "'";
            }
            if (EmpType != "")
            {
                crystalReport2.RecordSelectionFormula = crystalReport.RecordSelectionFormula + "and {Emp_Details.EmpType}='" + EmpType.ToString() + "'";
            }
            if (Location != "")
            {
                crystalReport2.RecordSelectionFormula = crystalReport.RecordSelectionFormula + "and {Hrms_Division_Master.Division_Master_Code}='" + Location.ToString() + "'";
            }
            SetDBLogonForReport(ref crystalReport2);
            msg.Attachments.Add(new Attachment(crystalReport2.ExportToStream(ExportFormatType.PortableDocFormat), "AttendanceSummery.pdf"));
            msg.Subject = msub;
            msg.Body = mbody;
            smtp.Send(msg);
            flg = true;
        }
        catch
        {
        }
        return flg;
    }

    private bool SendmailIndividual(string sid, string sname, string rid, string ccid, string msub, string mbody, string Deptid, string Sectid, string empid, string Location, string date1, string date2)
    {
        bool flg = false;
        string ReportName = "Attendance-" + empid + ".pdf";
        ReportDocument crystalReport = new ReportDocument();
        crystalReport.Load(Server.MapPath("../Reports/AttendanceMail.rpt"));
        crystalReport.RecordSelectionFormula = "{Hrms_Atnd_Det.Atnd_det_date}>=" + date1 + " and {Hrms_Atnd_Det.Atnd_det_date}<=" + date2 + "";
        if (empid != "")
        {
            crystalReport.RecordSelectionFormula = crystalReport.RecordSelectionFormula + "and {Emp_Details.Empid}='" + empid.ToString() + "'";
        }
        SetDBLogonForReport(ref crystalReport);
        try
        {
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("mailx.link3.net");
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.From = new System.Net.Mail.MailAddress(sid, sname);
            msg.To.Add(new System.Net.Mail.MailAddress(rid));
            string[] cary = ccid.Split(':');
            if (cary.Length >= 1)
            {
                for (int i = 0; i < cary.Length; i++)
                {
                    if (cary[i].Trim().ToString() != "")
                    {
                        msg.CC.Add(new System.Net.Mail.MailAddress(cary[i].Trim()));
                    }
                }
            }
            msg.Attachments.Add(new Attachment(crystalReport.ExportToStream(ExportFormatType.PortableDocFormat), ReportName));
            msg.Subject = msub;
            msg.Body = mbody;
            smtp.Send(msg);
            flg = true;
        }
        catch
        {
        }
        return flg;
    }

    protected void btnAttendanceMail0_Click(object sender, EventArgs e)
    {
        string empid = Session[StaticData.sessionUserId].ToString();
        string emailfrom = "";
        string emailto = "";
        string emailCc = "";
        string emailsubject = "";
        string emailbody = "";
        string date1 = "";
        string date2 = "";
        string EmployeeID = txtempid.Text;
        if (txtempid.Text == "") return;
        if (txtemail.Text == "") return;

        DateTime dtfr = Convert.ToDateTime(txtdaterange1.Text);
        DateTime dtfr2 = Convert.ToDateTime(txtdaterange2.Text);
        date1 = "date(" + dtfr.Year + "," + string.Format("{0:00}", dtfr.Month) + "," + string.Format("{0:00}", dtfr.Day) + ")";
        date2 = "date(" + dtfr2.Year + "," + string.Format("{0:00}", dtfr2.Month) + "," + string.Format("{0:00}", dtfr2.Day) + ")";
        DataTable dt = new DataTable();
        string sql = "select isnull(Emp_Mas_Remarks,'') as Emailfromid,Emp_Mas_First_Name+SPACE(1)+Emp_Mas_Last_Name as EmpName from HrMs_Emp_mas where Emp_Mas_Emp_Id='" + empid + "'";
        dt = DataProcess.GetData(ConnectionStr, sql);
        if (dt.Rows.Count > 0)
        {
            emailfrom = dt.Rows[0]["Emailfromid"].ToString();
            emailto = txtemail.Text;
            emailCc = txtCc.Text;
            emailsubject = "Attendance of- " + EmployeeID;
            emailbody = "Please find the attendance report of " + dt.Rows[0]["EmpName"].ToString() + " as attachment";
            SendmailIndividual(emailfrom, "", emailto, emailCc, emailsubject, emailbody, "", "", EmployeeID, "", date1, date2);
        }
    }

    private void SetDBLogonForReport(ref ReportDocument reportDocument)
    {
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConfigurationManager.AppSettings["UbasysConnectionString"].ToString());
        ConnectionInfo connectionInfo = new ConnectionInfo();
        connectionInfo.DatabaseName = builder.InitialCatalog;
        connectionInfo.UserID = builder.UserID;
        connectionInfo.Password = builder.Password;
        connectionInfo.ServerName = builder.DataSource;
        Tables tables = reportDocument.Database.Tables;
        foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
        {
            TableLogOnInfo tableLogonInfo = table.LogOnInfo;
            tableLogonInfo.ConnectionInfo = connectionInfo;
            table.ApplyLogOnInfo(tableLogonInfo);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string msg = "";
        if (ddlDept.SelectedItem.Text == "" || txtEmailFrom.Text == "" || txtSubject.Text == "" || txtBody.Text == "")
        {
            msg = "Message:" + "\n" + "Give all information correctly";
            System.Windows.Forms.MessageBox.Show(msg);
            return;
        }
        string dept = ddlDept.SelectedIndex != -1 ? ddlDept.SelectedItem.Value : "";
        string sect = ddlSection.SelectedIndex != -1 ? ddlSection.SelectedItem.Value : "";
        string emptype = ddlEmpType.SelectedIndex != -1 ? ddlEmpType.SelectedItem.Value : "";
        string Location = ddlLocation.SelectedIndex != -1 ? ddlLocation.SelectedItem.Value : "";
        string emailfrom = txtEmailFrom.Text;
        string emailto = txtEmailTo.Text;
        string emailcc = txtEmailCc.Text;
        string emailsubject = txtSubject.Text;
        string emailbody = txtBody.Text;
        string statusValue = ddlStatus.SelectedValue;
        string sql = "";
        if (txtRefId.Text == "")
        {
            sql = "insert into HrmsAttendanceMailConfigure([Deptid],[Sectid],[EmpType],[Location],[EmailFrom],[EmailTo],[EmailCc],[Subject],[EmailBody],[Status])"
                + " values('" + dept.ToString() + "','" + sect.ToString() + "','" + emptype.ToString() + "','" + Location.ToString() + "','" + emailfrom.ToString() + "','" + emailto.ToString() + "','" + emailcc.ToString() + "','" + emailsubject.ToString() + "','" + emailbody.ToString() + "','"+statusValue+"')";
        }
        else
        {
            sql = "update HrmsAttendanceMailConfigure set Deptid='" + dept.ToString() + "',Sectid='" + sect.ToString() + "',Emptype='" + emptype.ToString() + "',Location='" + Location.ToString() + "',EmailFrom='" + emailfrom.ToString() + "',EmailTo='" + emailto.ToString() + "',EmailCc='" + emailcc.ToString() + "',Subject='" + emailsubject.ToString() + "',EmailBody='" + emailbody.ToString() + "',[Status] = '" + statusValue + "' where [Refid]=" + txtRefId.Text + "";
        }
        if (DataProcess.ExecuteQuery(ConnectionStr, sql))
        {
            msg = "Message:" + "\n" + "Data Saved Successful";
            System.Windows.Forms.MessageBox.Show(msg);
        }
        else
        {
            msg = "Message:" + "\n" + "Data not saved. Try agan later ";
            System.Windows.Forms.MessageBox.Show(msg);
        }
        LoadGrid();
        ddlDept.SelectedIndex = -1;
        ddlSection.SelectedIndex = -1;
        ddlLocation.SelectedIndex = -1;
        ddlEmpType.SelectedIndex = -1;
        txtRefId.Text = "";
        txtEmailFrom.Text = "";
        txtEmailTo.Text = "";
        txtEmailCc.Text = "";
        txtSubject.Text = "";
        txtBody.Text = "";
        ddlStatus.SelectedValue = "1";
    }

    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        string depttid = ddlDept.SelectedItem.Value.ToString();
        LoadSectionCombo(depttid);
    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Sectid = ddlSection.SelectedItem.Value.ToString();
        LoadLocationCombo(Sectid);
    }

    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void gdvConfig_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void gdvConfig_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[12].Visible = false;
    }

    protected void gdvConfig_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string msg = "";
        try
        {
            if (e.CommandName.Equals("DeleteLine"))
            {
                int indx = Convert.ToInt32(e.CommandArgument);
                string refid = gdvConfig.Rows[indx].Cells[10].Text;
                string sql = "delete from [HrmsAttendanceMailConfigure] where Refid=" + refid + "";
                DataProcess.ExecuteQuery(ConnectionStr, sql);
                LoadGrid();
                msg = "Message:" + "\n" + "Data Deleted Successfull";
                System.Windows.Forms.MessageBox.Show(msg);
            }
            else if (e.CommandName.Equals("EditLine"))
            {
                int indx = Convert.ToInt32(e.CommandArgument);
                ddlDept.SelectedValue = ddlDept.Items.FindByText(Getstring(gdvConfig.Rows[indx].Cells[1].Text)).Value;
                ddlSection.SelectedValue = ddlSection.Items.FindByText(Getstring(gdvConfig.Rows[indx].Cells[2].Text)).Value;
                ddlLocation.SelectedValue = ddlLocation.Items.FindByText(Getstring(gdvConfig.Rows[indx].Cells[3].Text)).Value;
                ddlEmpType.SelectedValue = ddlEmpType.Items.FindByText(Getstring(gdvConfig.Rows[indx].Cells[4].Text)).Value;
                txtEmailFrom.Text = gdvConfig.Rows[indx].Cells[5].Text.ToString();
                txtEmailTo.Text = gdvConfig.Rows[indx].Cells[6].Text.ToString();
                txtEmailCc.Text = gdvConfig.Rows[indx].Cells[7].Text.ToString();
                txtSubject.Text = gdvConfig.Rows[indx].Cells[8].Text.ToString();
                txtBody.Text = gdvConfig.Rows[indx].Cells[9].Text.ToString();
                txtRefId.Text = gdvConfig.Rows[indx].Cells[10].Text.ToString();
                ddlStatus.SelectedValue = Getstring(gdvConfig.Rows[indx].Cells[12].Text);
            }
        }
        catch (Exception ex)
        {
            msg = "Message:" + "\n" + "error!!!!!";
            System.Windows.Forms.MessageBox.Show(msg);
        }
    }

    private string Getstring(string str)
    {
        str = str.Replace("&amp;", "&");
        str = str.Replace("&#39;", "'");
        return str;
    }
}