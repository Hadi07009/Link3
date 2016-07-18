using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using LibraryDAL;
using ADODB;
using CrystalDecisions;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Drawing;
using System.Drawing.Drawing2D;


public partial class modules_HRMS_Approval_frmApproval : System.Web.UI.Page
{
    string ConnectionStr = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        StaticData.MsgConfirmBox(btnApprove, "Are you sure want to Approve ? ");
        StaticData.MsgConfirmBox(btnReject, "Are you sure want to Reject ? ");
        StaticData.MsgConfirmBox(btnForward, "Are you sure want to Forward ? ");

        if (!Page.IsPostBack)
        {
            DataTable dttask = new DataTable();
            DateTime serverdate = DateProcess.GetServerDate(ConnectionStr);
            DateTime fDate = DateProcess.FirstDateOfMonth(serverdate);
            DateTime lDate = DateProcess.LastDateOfMonth(serverdate);
            dttask = DataProcess.GetData(ConnectionStr, "select * from ProcessTaskDateAllemoloyee where TaskType='' and TrnMonth=" + serverdate.Month + " and TrnYear=" + serverdate.Year + "");

            if (dttask.Rows.Count > 0)
            {
                if (dttask.Rows[0]["Isstadate"].ToString() == "Y")
                {
                    fDate = Convert.ToDateTime(dttask.Rows[0]["StartDate"].ToString());
                }
                if (dttask.Rows[0]["IsendDate"].ToString() == "Y")
                {
                    lDate = Convert.ToDateTime(dttask.Rows[0]["EndDate"].ToString());
                }
                if (dttask.Rows[0]["IsShowPreviousTask"].ToString() == "N")
                {
                    btnShow.Visible = false;
                }
                if (dttask.Rows[0]["IsShowPreviousAtnd"].ToString() == "N")
                {
                    btnShowALL.Visible = false;
                }
                if (dttask.Rows[0]["IsApplyPrevious"].ToString() == "N")
                {
                    btnApply.Visible = false;
                }
            }

            Session["ActingPersonID"] = "L3T593";
            Session["EntryUserid"] = "L3T593";
            //Session["ActingPersonID"] = Session[StaticData.sessionUserId].ToString();
            //Session["EntryUserid"] = Session[StaticData.sessionUserId].ToString();

            Session["fdate"] = fDate;
            Session["lDate"] = lDate;
            LoadAllPendingApplication(Session["ActingPersonID"].ToString());//, "P004", "4"); 
            lblPeriod.Text = FinanialPeriod(fDate);
            lblPeriodN.Text = FinanialPeriod(fDate);
            lblPeriodL.Text = FinanialPeriod(fDate);
            lblPeriodOD.Text = FinanialPeriod(fDate);
            lblPeriodAttendance.Text = FinanialPeriod(fDate);
            lblPeriodEarnedLeave.Text = FinanialPeriod(fDate);
            lblcurrentPeriod.Text = Convert.ToString(System.DateTime.Now.Date.Day) + "-" + Convert.ToString(System.DateTime.Now.Date.Month) + "-" + Convert.ToString(System.DateTime.Now.Date.Year);
            lblDateN.Text = Convert.ToString(System.DateTime.Now.Date.Day) + "-" + Convert.ToString(System.DateTime.Now.Date.Month) + "-" + Convert.ToString(System.DateTime.Now.Date.Year);
            lblDateL.Text = Convert.ToString(System.DateTime.Now.Date.Day) + "-" + Convert.ToString(System.DateTime.Now.Date.Month) + "-" + Convert.ToString(System.DateTime.Now.Date.Year);
            lblDateOD.Text = Convert.ToString(System.DateTime.Now.Date.Day) + "-" + Convert.ToString(System.DateTime.Now.Date.Month) + "-" + Convert.ToString(System.DateTime.Now.Date.Year);
            lblDateAttendance.Text = Convert.ToString(System.DateTime.Now.Date.Day) + "-" + Convert.ToString(System.DateTime.Now.Date.Month) + "-" + Convert.ToString(System.DateTime.Now.Date.Year);
            lblDateEarnedLeave.Text = Convert.ToString(System.DateTime.Now.Date.Day) + "-" + Convert.ToString(System.DateTime.Now.Date.Month) + "-" + Convert.ToString(System.DateTime.Now.Date.Year);
            Panel50.Visible = false;
            Panel1.Visible = false;
            Panel2.Visible = false;
            Panel4.Visible = false;
            Panel6.Visible = false;
            Panel8.Visible = false;
                       
        } 
    }

    private string FinanialPeriod(DateTime fdate)
    {
        string fdate1, fdate2,period;
        if (fdate >= Convert.ToDateTime("01/07/" + Convert.ToString(fdate.Year)))
        {
            fdate1 = Convert.ToString(fdate.Year);
            fdate2 = Convert.ToString(fdate.Year + 1);
        }
        else
        {
            fdate1 = Convert.ToString(fdate.Year-1);
            fdate2 = Convert.ToString(fdate.Year); 
        }
        period = fdate1 + "-" + fdate2;
        return period;
    }

    private void LoadEmployeeInformation(string empid)
    {
        DataTable dt = new DataTable();
        dt = DataProcess.GetData(ConnectionStr, "select EmpID,EmpName,Dept,Designation,b.Emp_Mas_Join_Date as Joiningdate from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id where EmpID='" + empid.ToString() + "'");
        if (dt.Rows.Count > 0)
        {
            lblId.Text = dt.Rows[0]["EmpID"].ToString();
            lblName.Text = dt.Rows[0]["EmpName"].ToString();
            lbldept.Text = dt.Rows[0]["Dept"].ToString();
            lblDesignation.Text = dt.Rows[0]["Designation"].ToString();
            lblJoiningDate.Text = dt.Rows[0]["Joiningdate"].ToString().Substring(0,10);
        }
        //  DataTable dtt = DataProcess.GetData(ConnectionStr, "select image from [WFA2].[dbo].[EmployeeImage] where userid='L3T388'");
        //  if (dtt.Rows.Count > 0)
        // {
        //string ffff = "L3T388";
        //Image1.ImageUrl = "~/ClientSide/modules/mis/naz/FORMS/HRMS/forms/hndImage.ashx?id=" + ffff;
        //  }
        // Image1.ImageUrl = "~/ClientSide/modules/HRIS/MIS//hndImage.ashx?id=" + strarr[0].Trim();
    }
    private void LoadEmployeeInformationForLeave(string empid)
    {
        DataTable dt = new DataTable();
        dt = DataProcess.GetData(ConnectionStr, "select EmpID,EmpName,Dept,Designation,b.Emp_Mas_Join_Date as Joiningdate from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id where EmpID='" + empid.ToString() + "'");
        if (dt.Rows.Count > 0)
        {
            lblIdL.Text = dt.Rows[0]["EmpID"].ToString();
            lblNameL.Text = dt.Rows[0]["EmpName"].ToString();
            lblDepartmentL.Text = dt.Rows[0]["Dept"].ToString();
            lblDesignationL.Text = dt.Rows[0]["Designation"].ToString();
            lblJoiningDateL.Text = dt.Rows[0]["Joiningdate"].ToString().Substring(0, 10);
        }
    }
    private void LoadEmployeeInformationForOffday(string empid)
    {
        DataTable dt = new DataTable();
        dt = DataProcess.GetData(ConnectionStr, "select EmpID,EmpName,Dept,Designation,b.Emp_Mas_Join_Date as Joiningdate from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id where EmpID='" + empid.ToString() + "'");
        if (dt.Rows.Count > 0)
        {
            lblIdOD.Text = dt.Rows[0]["EmpID"].ToString();
            lblNameOD.Text = dt.Rows[0]["EmpName"].ToString();
            lblDepartmentOD.Text = dt.Rows[0]["Dept"].ToString();
            lblDesignationOD.Text = dt.Rows[0]["Designation"].ToString();
            lblJoiningDateOD.Text = dt.Rows[0]["Joiningdate"].ToString().Substring(0, 10);
        }
    }
    private void LoadEmployeeInformationForAttendance(string empid)
    {
        DataTable dt = new DataTable();
        dt = DataProcess.GetData(ConnectionStr, "select EmpID,EmpName,Dept,Designation,b.Emp_Mas_Join_Date as Joiningdate from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id where EmpID='" + empid.ToString() + "'");
        if (dt.Rows.Count > 0)
        {
            lblIdAttendance.Text = dt.Rows[0]["EmpID"].ToString();
            lblNameAttendance.Text = dt.Rows[0]["EmpName"].ToString();
            lblDepartmentAttendance.Text = dt.Rows[0]["Dept"].ToString();
            lblDesignationAttendance.Text = dt.Rows[0]["Designation"].ToString();
            lblJoiningDateAttendance.Text = dt.Rows[0]["Joiningdate"].ToString().Substring(0, 10);
        }
    }
    private void LoadEmployeeInformationForEarnedLeave(string empid)
    {
        DataTable dt = new DataTable();
        dt = DataProcess.GetData(ConnectionStr, "select EmpID,EmpName,Dept,Designation,b.Emp_Mas_Join_Date as Joiningdate from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id where EmpID='" + empid.ToString() + "'");
        if (dt.Rows.Count > 0)
        {
            lblIdEarnedLeave.Text = dt.Rows[0]["EmpID"].ToString();
            lblNameEarnedLeave.Text = dt.Rows[0]["EmpName"].ToString();
            lblDepartmentEarnedLeave.Text = dt.Rows[0]["Dept"].ToString();
            lblDesignationEarnedLeave.Text = dt.Rows[0]["Designation"].ToString();
            lblJoiningDateEarnedLeave.Text = dt.Rows[0]["Joiningdate"].ToString().Substring(0, 10);
        }
    }

    private void LoadEmployeeInformationForNight(string empid)
    {
        DataTable dt = new DataTable();
        dt = DataProcess.GetData(ConnectionStr, "select EmpID,EmpName,Dept,Designation,b.Emp_Mas_Join_Date as Joiningdate from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id where EmpID='" + empid.ToString() + "'");
        if (dt.Rows.Count > 0)
        {
            lblIdN.Text = dt.Rows[0]["EmpID"].ToString();
            lblNameN.Text = dt.Rows[0]["EmpName"].ToString();
            lblDepartmentN.Text = dt.Rows[0]["Dept"].ToString();
            lblDesignationN.Text = dt.Rows[0]["Designation"].ToString();
            lblJoiningDateN.Text = dt.Rows[0]["Joiningdate"].ToString().Substring(0, 10);
        }
    }
    private void LoadOvertimeDetailsemployeeID(string empid, string Processid, string flowid, DateTime fDate, DateTime lDate)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetOvertimeStatus";
        cmd.Parameters.Add(new SqlParameter("@fDate", SqlDbType.DateTime)).Value = fDate;
        cmd.Parameters.Add(new SqlParameter("@lDate ", SqlDbType.DateTime)).Value = lDate;
        cmd.Parameters.Add(new SqlParameter("@empid", SqlDbType.NVarChar)).Value = empid.ToString();
        cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid.ToString();
        cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid.ToString();
        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        grdOvertime.DataSource = dt;
        grdOvertime.DataBind();
    } 
    private void LoadLeavBALeByemployeeID(String empid, DateTime fDate)
    {
        DataTable dt = new DataTable(); 
        LeaveProcess lvproc = new LeaveProcess();  
        lvproc.GetEmployeeLeaveBalance(ConnectionStr, fDate, empid);
        dt = DataProcess.GetData(ConnectionStr, "select LeaveType as [Code],b.Leave_Mas_Name,AllocatedLeave,AllocatedLeave-LeaveBal as [Enjoyed],LeaveBal" 
                                                + " from [HRMS_EMPLEAVEBAL] a" 
                                                + " inner join HRMS_Leave_Mas b on a.LeaveType=b.Leave_Mas_Code"
                                                + " inner join Emp_Details c on c.EmpID=a.Empid and c.EmpType=b.T_C2"
                                                + " where a.Empid='" + empid.ToString() + "' ");
        gdvLeaveInfo.DataSource = dt;
        gdvLeaveInfo.DataBind();
    }
    private void LoadPendingOvertimeByemployeeID(string transactionno, string Processid, string flowid,string actingpersonid,string leavecode)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetPendingOvertimeByTransactionno";
        cmd.Parameters.Add(new SqlParameter("@transactionno", SqlDbType.NVarChar)).Value = transactionno.ToString();
        cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid.ToString();
        cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid.ToString();
        cmd.Parameters.Add(new SqlParameter("@actingpersonid", SqlDbType.NVarChar)).Value = actingpersonid.ToString();
        cmd.Parameters.Add(new SqlParameter("@leavecode", SqlDbType.NVarChar)).Value = leavecode.ToString();
        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        grdOvertime.DataSource = dt;
        grdOvertime.DataBind();
    }
    private void LoadPendingLeaveByemployeeID(string transactionno, string Processid, string flowid, string actingpersonid, string leavecode)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetPendingLeaveStatusByTransactionno";
        cmd.Parameters.Add(new SqlParameter("@transactionno", SqlDbType.NVarChar)).Value = transactionno.ToString();
        cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid.ToString();
        cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid.ToString();
        cmd.Parameters.Add(new SqlParameter("@actingpersonid", SqlDbType.NVarChar)).Value = actingpersonid.ToString();
        cmd.Parameters.Add(new SqlParameter("@leavecode", SqlDbType.NVarChar)).Value = leavecode.ToString();
        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        GridViewLeave.DataSource = dt;
        GridViewLeave.DataBind();
    }
    private void LoadPendingOffdayByemployeeID(string transactionno, string Processid, string flowid, string actingpersonid, string leavecode)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetPendingOffdayByTransactionno";
        cmd.Parameters.Add(new SqlParameter("@transactionno", SqlDbType.NVarChar)).Value = transactionno.ToString();
        cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid.ToString();
        cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid.ToString();
        cmd.Parameters.Add(new SqlParameter("@actingpersonid", SqlDbType.NVarChar)).Value = actingpersonid.ToString();
        cmd.Parameters.Add(new SqlParameter("@leavecode", SqlDbType.NVarChar)).Value = leavecode.ToString();
        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        GridViewOffday.DataSource = dt;
        GridViewOffday.DataBind();
    }
    private void LoadPendingAttendanceByemployeeID(string transactionno, string Processid, string flowid, string actingpersonid, string leavecode)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetPendingAttendanceByTransactionno";
        cmd.Parameters.Add(new SqlParameter("@transactionno", SqlDbType.NVarChar)).Value = transactionno.ToString();
        cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid.ToString();
        cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid.ToString();
        cmd.Parameters.Add(new SqlParameter("@actingpersonid", SqlDbType.NVarChar)).Value = actingpersonid.ToString();
        cmd.Parameters.Add(new SqlParameter("@leavecode", SqlDbType.NVarChar)).Value = leavecode.ToString();
        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        grdAttendance.DataSource = dt;
        grdAttendance.DataBind();
    }
    private void LoadPendingEarnedLeaveByemployeeID(string transactionno, string Processid, string flowid, string actingpersonid, string leavecode)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetPendingEarnedLeaveByTransactionno";
        cmd.Parameters.Add(new SqlParameter("@transactionno", SqlDbType.NVarChar)).Value = transactionno.ToString();
        cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid.ToString();
        cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid.ToString();
        cmd.Parameters.Add(new SqlParameter("@actingpersonid", SqlDbType.NVarChar)).Value = actingpersonid.ToString();
        cmd.Parameters.Add(new SqlParameter("@leavecode", SqlDbType.NVarChar)).Value = leavecode.ToString();
        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        GridViewEarnedLeave.DataSource = dt;
        GridViewEarnedLeave.DataBind();
    }

    private void LoadPendingOvertimeRemarksByemployeeID(string transactionno, string Processid, string flowid, string actingpersonid, string leavecode)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetLeaveRemarksByTransactionno";
        cmd.Parameters.Add(new SqlParameter("@transactionno", SqlDbType.NVarChar)).Value = transactionno.ToString();
        cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid.ToString();
        cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid.ToString();
        cmd.Parameters.Add(new SqlParameter("@actingpersonid", SqlDbType.NVarChar)).Value = actingpersonid.ToString();
        cmd.Parameters.Add(new SqlParameter("@leavecode", SqlDbType.NVarChar)).Value = leavecode.ToString();
        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        string remarks = "Please see all remarks below:";
        string remarks1 = "";
        string renarks2 = "";
        string remarksnew = "";
        string remarksline = "----------------------------------";
        int ln = 0;
        foreach (DataRow dr in dt.Rows)
        {
            ln = ln + 1;
            remarksnew = ln.ToString()+". "+dr["EmpName"].ToString() + " >> " + dr["Remarks"].ToString();
            remarks += "\n" + remarksline.ToString();
            remarks += "\n" + remarksnew.ToString();
        } 
        txtRemarksAll.Text = remarks;
        int numLines = remarks.ToString().Split('\n').Length;
        string[] lines = txtRemarksAll.Text.Split('\n');
        numLines = lines.Length - 1;
        txtRemarksAll.Height = (numLines + ln) * 15 + 25;
        txtRemarksAll.Enabled = false;
    }
    private void LoadPendingLeaveRemarksByemployeeID(string transactionno, string Processid, string flowid, string actingpersonid, string leavecode)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetLeaveRemarksByTransactionno";
        cmd.Parameters.Add(new SqlParameter("@transactionno", SqlDbType.NVarChar)).Value = transactionno.ToString();
        cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid.ToString();
        cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid.ToString();
        cmd.Parameters.Add(new SqlParameter("@actingpersonid", SqlDbType.NVarChar)).Value = actingpersonid.ToString();
        cmd.Parameters.Add(new SqlParameter("@leavecode", SqlDbType.NVarChar)).Value = leavecode.ToString();
        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        string remarks = "Please see all remarks below:";
        string remarks1 = "";
        string renarks2 = "";
        string remarksnew = "";
        string remarksline = "----------------------------------";
        int ln = 0;
        foreach (DataRow dr in dt.Rows)
        {
            ln = ln + 1;
            remarksnew = ln.ToString() + ". " + dr["EmpName"].ToString() + " >> " + dr["Remarks"].ToString();
            remarks += "\n" + remarksline.ToString();
            remarks += "\n" + remarksnew.ToString();
        }
        txtRemarksAllLeave.Text = remarks;
        int numLines = remarks.ToString().Split('\n').Length;
        string[] lines = txtRemarksAllLeave.Text.Split('\n');
        numLines = lines.Length - 1;
        txtRemarksAllLeave.Height = (numLines + ln) * 15 + 25;
        txtRemarksAllLeave.Enabled = false;
    }
    private void LoadPendingOffdayRemarksByemployeeID(string transactionno, string Processid, string flowid, string actingpersonid, string leavecode)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetLeaveRemarksByTransactionno";
        cmd.Parameters.Add(new SqlParameter("@transactionno", SqlDbType.NVarChar)).Value = transactionno.ToString();
        cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid.ToString();
        cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid.ToString();
        cmd.Parameters.Add(new SqlParameter("@actingpersonid", SqlDbType.NVarChar)).Value = actingpersonid.ToString();
        cmd.Parameters.Add(new SqlParameter("@leavecode", SqlDbType.NVarChar)).Value = leavecode.ToString();
        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        string remarks = "Please see all remarks below:";
        string remarks1 = "";
        string renarks2 = "";
        string remarksnew = "";
        string remarksline = "----------------------------------";
        int ln = 0;
        foreach (DataRow dr in dt.Rows)
        {
            ln = ln + 1;
            remarksnew = ln.ToString() + ". " + dr["EmpName"].ToString() + " >> " + dr["Remarks"].ToString();
            remarks += "\n" + remarksline.ToString();
            remarks += "\n" + remarksnew.ToString();
        }
        txtRemarksAllOffday.Text = remarks;
        int numLines = remarks.ToString().Split('\n').Length;
        string[] lines = txtRemarksAllOffday.Text.Split('\n');
        numLines = lines.Length - 1;
        txtRemarksAllOffday.Height = (numLines + ln) * 15 + 25;
        txtRemarksAllOffday.Enabled = false;
    }
    private void LoadPendingAttendanceRemarksByemployeeID(string transactionno, string Processid, string flowid, string actingpersonid, string leavecode)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetLeaveRemarksByTransactionno";
        cmd.Parameters.Add(new SqlParameter("@transactionno", SqlDbType.NVarChar)).Value = transactionno.ToString();
        cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid.ToString();
        cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid.ToString();
        cmd.Parameters.Add(new SqlParameter("@actingpersonid", SqlDbType.NVarChar)).Value = actingpersonid.ToString();
        cmd.Parameters.Add(new SqlParameter("@leavecode", SqlDbType.NVarChar)).Value = leavecode.ToString();
        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        string remarks = "Please see all remarks below:";
        string remarks1 = "";
        string renarks2 = "";
        string remarksnew = "";
        string remarksline = "----------------------------------";
        int ln = 0;
        foreach (DataRow dr in dt.Rows)
        {
            ln = ln + 1;
            remarksnew = ln.ToString() + ". " + dr["EmpName"].ToString() + " >> " + dr["Remarks"].ToString();
            remarks += "\n" + remarksline.ToString();
            remarks += "\n" + remarksnew.ToString();
        }
        txtRemarksAllAttendance.Text = remarks;
        int numLines = remarks.ToString().Split('\n').Length;
        string[] lines = txtRemarksAllAttendance.Text.Split('\n');
        numLines = lines.Length - 1;
        txtRemarksAllAttendance.Height = (numLines + ln) * 15 + 25;
        txtRemarksAllAttendance.Enabled = false;
    }
    private void LoadPendingEarnedLeaveRemarksByemployeeID(string transactionno, string Processid, string flowid, string actingpersonid, string leavecode)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetLeaveRemarksByTransactionno";
        cmd.Parameters.Add(new SqlParameter("@transactionno", SqlDbType.NVarChar)).Value = transactionno.ToString();
        cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid.ToString();
        cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid.ToString();
        cmd.Parameters.Add(new SqlParameter("@actingpersonid", SqlDbType.NVarChar)).Value = actingpersonid.ToString();
        cmd.Parameters.Add(new SqlParameter("@leavecode", SqlDbType.NVarChar)).Value = leavecode.ToString();
        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        string remarks = "Please see all remarks below:";
        string remarks1 = "";
        string renarks2 = "";
        string remarksnew = "";
        string remarksline = "----------------------------------";
        int ln = 0;
        foreach (DataRow dr in dt.Rows)
        {
            ln = ln + 1;
            remarksnew = ln.ToString() + ". " + dr["EmpName"].ToString() + " >> " + dr["Remarks"].ToString();
            remarks += "\n" + remarksline.ToString();
            remarks += "\n" + remarksnew.ToString();
        }
        txtRemarksAllEarnedLeave.Text = remarks;
        int numLines = remarks.ToString().Split('\n').Length;
        string[] lines = txtRemarksAllEarnedLeave.Text.Split('\n');
        numLines = lines.Length - 1;
        txtRemarksAllEarnedLeave.Height = (numLines + ln) * 15 + 25;
        txtRemarksAllEarnedLeave.Enabled = false;
    }
    private void LoadAllPendingApplication(string actingpersonid)//, string Processid, string flowid)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetAllPendingApplication";
        cmd.Parameters.Add(new SqlParameter("@actempid", SqlDbType.NVarChar)).Value = actingpersonid.ToString();
        //cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid.ToString();
        //cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid.ToString();
        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        GridViewLeavePending.DataSource = dt;
        GridViewLeavePending.DataBind();
        //GridViewLeavePending.Columns[8].Visible = false;
        //GridViewLeavePending.Columns[5].Visible = false;
        string gridhieght = GridViewLeavePending.Height.Value.ToString();
        int gdr = GridViewLeavePending.Rows.Count;
        if (gdr == 0)
        {
            CollapsiblePanelExtenderSrch.ExpandedSize = 5;
        }
        else
        {
            CollapsiblePanelExtenderSrch.ExpandedSize = (gdr * 40) + 25;
        }
    } 
    private DataTable GetDatafromTable(string ConnectionStr, string sql)
    {
        DataTable dt = new DataTable();
        SqlConnection sqlConn = null;
        try
        {
            sqlConn = new SqlConnection(ConnectionStr);
            sqlConn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sql, ConnectionStr);
            da.Fill(dt);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (sqlConn.State == System.Data.ConnectionState.Open)
            {
                sqlConn.Close();
            }
        }
        return dt;
    }
    public void ButtonCall(string processId, int actionTypeId)
    {
        if (processId == "P001" && actionTypeId == 2)
        {
            btnForwardLeave_Click( this, new EventArgs() );
        }
        else if (processId == "P001" && actionTypeId == 3)
        {
            btnReturnLeave_Click(this, new EventArgs());
        }
        else if (processId == "P001" && actionTypeId == 4)
        {
            btnRejectLeave_Click(this, new EventArgs());
        }
        else if (processId == "P001" && actionTypeId == 5)
        {
            btnApproveLeave_Click(this, new EventArgs());
        }
        else if (processId == "P002" && actionTypeId == 2)
        {
            btnForwardOffday_Click(this, new EventArgs());
        }
        else if (processId == "P002" && actionTypeId == 3)
        {
            btnReturnOffday_Click(this, new EventArgs());
        }
        else if (processId == "P002" && actionTypeId == 4)
        {
            btnRejectOffday_Click(this, new EventArgs());
        }
        else if (processId == "P002" && actionTypeId == 5)
        {
            btnApproveOffday_Click(this, new EventArgs());
        }
        else if (processId == "P004" && actionTypeId == 2)
        {
            btnForward_Click(this, new EventArgs());
        }
        else if (processId == "P004" && actionTypeId == 3)
        {
            btnReturn_Click(this, new EventArgs());
        }
        else if (processId == "P004" && actionTypeId == 4)
        {
            btnReject_Click(this, new EventArgs());
        }
        else if (processId == "P004" && actionTypeId == 5)
        {
            btnApprove_Click(this, new EventArgs());
        }
        else if (processId == "P006" && actionTypeId == 2)
        {
            btnForwardNight_Click(this, new EventArgs());
        }
        else if (processId == "P006" && actionTypeId == 3)
        {
            btnReturnNight_Click(this, new EventArgs());
        }
        else if (processId == "P006" && actionTypeId == 4)
        {
            btnRejectNight_Click(this, new EventArgs());
        }
        else if (processId == "P006" && actionTypeId == 5)
        {
            btnApproveNight_Click(this, new EventArgs());
        }
        else if (processId == "P007" && actionTypeId == 2)
        {
            btnForwardAttendance_Click(this, new EventArgs());
        }
        else if (processId == "P007" && actionTypeId == 3)
        {
            btnReturnAttendance_Click(this, new EventArgs());
        }
        else if (processId == "P007" && actionTypeId == 4)
        {
            btnRejectAttendance_Click(this, new EventArgs());
        }
        else if (processId == "P007" && actionTypeId == 5)
        {
            btnApproveAttendance_Click(this, new EventArgs());
        }
        else if (processId == "P008" && actionTypeId == 2)
        {
            btnForwardEarnedLeave_Click(this, new EventArgs());
        }
        else if (processId == "P008" && actionTypeId == 3)
        {
            btnReturnEarnedLeave_Click(this, new EventArgs());
        }
        else if (processId == "P008" && actionTypeId == 4)
        {
            btnRejectEarnedLeave_Click(this, new EventArgs());
        }
        else if (processId == "P008" && actionTypeId == 5)
        {
            btnApproveEarnedLeave_Click(this, new EventArgs());
        }
    }
    protected void btnViewall_Click(object sender, EventArgs e)
    {
        string sql = "select left(b.itm_det_date,12) as Purchase_date,a.ItemCode,left(a.DepreciationDate,12) as [Dep. Date],1 as Qty,convert(decimal,a.ItemInitialValue) as [Unit Price],convert(decimal,a.ThisPrdOpeningValue) as [Balance],"
                   + " convert(decimal,a.OpeningDepreciationAmt) as Opening,convert(decimal,a.Addition) as Addition,convert(decimal,a.TotalDepreciationAmt) as Accumulated,convert(decimal,WrittenDownValue) as [Written Down Value]"
                   + " from FAS_Item_Depreciation a inner join FAS_InMa_Itm_Serial b on a.TrackingInfo=b.TrackingInfo where a.ItemCurrentLine='Y'";
        DataTable dt = GetDatafromTable(ConnectionStr, sql);
        //GridView1.DataSource = dt;
        //GridView1.DataBind();
    }
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        Connection DC = new Connection();
        DateTime dtfr, dtto, dtfr1;
        string[] ff, ss;
        string str, vcoa, vac;
        ParameterFields paramFields = new ParameterFields();
        ConnectionInfo ConnInfo = new ConnectionInfo();
        string constr = System.Configuration.ConfigurationManager.AppSettings["ConStrLNK"].ToString();
        DC.Open(constr, null, null, 0);
        string crdate = DateTime.Now.Day.ToString();
        DateTime crdate1 = DateTime.Now.Date;
        ff = constr.Split('=');
        ss = ff[2].Split(';');
        ConnInfo.ServerName = ss[0];
        ss = ff[3].Split(';');
        ConnInfo.DatabaseName = ss[0];
        ss = ff[4].Split(';');
        ConnInfo.UserID = ss[0];
        ss = ff[5].Split(';');
        ConnInfo.Password = ss[0];
        string rep = "{Hrms_Emp_EL_Encashment.EmpId} in ['L3T593']";
        clsReport rpt = new clsReport();
        rpt.FileName = "../REPORT/EarnedLeaveByEmpId.rpt";
        rpt.ConnectionInfo = ConnInfo;
        rpt.ParametersFields = paramFields;
        rpt.SelectionFormulla = rep;
        string qrystr = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
        Session[qrystr] = rpt;
        RegisterStartupScript("Click", "<script>window.open('./frm_report_viewer.aspx?session_id_no=" + qrystr + "');</script>");
    } 
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //SqlConnection myConnection = new SqlConnection(ConnectionStr);
        //myConnection.Open();
        //SqlCommand myCommand = myConnection.CreateCommand();
        //SqlTransaction myTrans;
        //SqlDataAdapter sqlDataAdapterObj = null;
        //string retValue = "";
        //myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        //myCommand.Connection = myConnection;
        //myCommand.Transaction = myTrans;
        //try
        //{
        //    List<OffdayProcessHeader> offphdrlst = new List<OffdayProcessHeader>(); 
        //    for (int i = 0; i < grdHoliday.Rows.Count;i++)
        //    {
        //        string ProcessID = "P001";
        //        string FlowID = "1";
        //        int levelid = 0;
        //        string ApplicantID = "L3T593";
        //        CheckBox chkbox = grdHoliday.Rows[i].FindControl("CheckRet") as CheckBox;
        //        if(chkbox.Checked==true)
        //        {
        //            OffdayProcessHeader offphdr = new OffdayProcessHeader();
        //            MKB.TimePicker.TimeSelector mkb = (MKB.TimePicker.TimeSelector)grdHoliday.Rows[i].FindControl("timeoff1");
        //            MKB.TimePicker.TimeSelector mkb2 = (MKB.TimePicker.TimeSelector)grdHoliday.Rows[i].FindControl("timeoff2");
        //            TextBox tb = grdHoliday.Rows[i].FindControl("txtRemarks") as TextBox;
        //            offphdr.ApplicantId=ApplicantID.ToString();
        //            offphdr.SL = Convert.ToInt32(grdHoliday.Rows[i].Cells[0].Text.ToString());
        //            offphdr.ClaiminDdate = Convert.ToDateTime(grdHoliday.Rows[i].Cells[1].Text.ToString());
        //            offphdr.SysIndate= Convert.ToDateTime(grdHoliday.Rows[i].Cells[1].Text.ToString());
        //            offphdr.SysOutdate= Convert.ToDateTime(grdHoliday.Rows[i].Cells[1].Text.ToString());
        //            offphdr.ActIndate= Convert.ToDateTime(grdHoliday.Rows[i].Cells[1].Text.ToString());
        //            offphdr.ActOutdate = Convert.ToDateTime(grdHoliday.Rows[i].Cells[1].Text.ToString());
        //            offphdr.SysIntime= grdHoliday.Rows[i].Cells[4].Text.ToString();
        //            offphdr.SysOuttime = grdHoliday.Rows[i].Cells[5].Text.ToString();
        //            offphdr.SysTotalhrs = grdHoliday.Rows[i].Cells[6].Text.ToString();
        //            offphdr.ActIntime = timeformat(mkb.Date.Hour.ToString() + ":" + mkb.Date.Minute.ToString() + ":" + mkb.AmPm.ToString());
        //            offphdr.ActOuttime = timeformat(mkb2.Date.Hour.ToString() + ":" + mkb2.Date.Minute.ToString() + ":" + mkb2.AmPm.ToString());
        //            offphdr.Remarks = tb.Text.ToString();
        //            offphdr.Acthrs = "9";
        //            offphdrlst.Add(offphdr);
        //        }              
        //    }
        //    OffdayProcess offproc=new OffdayProcess();
        //    string retval=offproc.SaveOffdayData(offphdrlst,myCommand);
        //    if(retval.ToString()=="")
        //    {
        //        myTrans.Rollback("SaveAllTransaction");
        //    }
        //    else
        //    {
        //         myTrans.Commit();
        //    }
        //}
        //catch (Exception ex)
        //{
        //     myTrans.Rollback("SaveAllTransaction");
        //}
        //finally
        //{ 
        //    myConnection.Close();
        //}
    }

    private string timeformat(string atf)
    {
        string rtf = "";
        int h = Convert.ToInt32(atf.Split(':')[0].ToString());
        int m = Convert.ToInt32(atf.Split(':')[1].ToString());
        if (h > 12)
        {
            h=h-12;
        }
        string hh = string.Format("{0:00}",h);
        string mm = string.Format("{0:00}",m);
        string ampm = atf.Split(':')[2].ToString();
        rtf = hh + ":" + mm + " " + ampm;
        return rtf; 
    }
    private void SaveOffdayData(List<OffdayProcessHeader> offphdrlst)
    {
        SqlConnection myConnection = new SqlConnection(ConnectionStr);
        myConnection.Open();
        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;
        SqlDataAdapter sqlDataAdapterObj = null;
        string retValue = "";
        myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        try
        {
            DataTable dt = new DataTable();
            //string retrefno = CallSpForRetrefNumber(ConnectionString);
            foreach (OffdayProcessHeader ofproc in offphdrlst)
            {
                myCommand.CommandText = "exec [sp_ExecuteReturnDetails] '" + ofproc.SysIndate + "','" + ofproc.SysOutdate + "'";
                myCommand.CommandTimeout = 600;
                myCommand.ExecuteNonQuery();
            }
            myTrans.Commit();
        }
        catch (Exception ex)
        {
            myTrans.Rollback("SaveAllTransaction");
        }
    }

    private void callsp()
    {
        SqlConnection oConnection = new SqlConnection(ConnectionStr.ToString());
        oConnection.Open();
        SqlCommand cmd = new SqlCommand("PostHoldData", oConnection);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlParameter operatorID = cmd.Parameters.Add("@operatorID", SqlDbType.NVarChar, 10);
        operatorID.Value ="L3T593";
        SqlParameter trndate = cmd.Parameters.Add("@trndate", SqlDbType.NVarChar, 22);
        trndate.Value = "26/12/2012"; 
        // output parm
        SqlParameter outputStr = cmd.Parameters.Add("@outputStr", SqlDbType.NVarChar, 100);
        outputStr.Direction = ParameterDirection.Output;
        // return value
        SqlParameter returnnVal = cmd.Parameters.Add("returnnVal", SqlDbType.Int);
        returnnVal.Direction = ParameterDirection.ReturnValue;
        cmd.ExecuteNonQuery();
        if ((int)returnnVal.Value == 0)
        {
            string rest = Convert.ToString(outputStr.Value);
        }
        else
        {
            string rest = Convert.ToString(outputStr.Value);
        }
    }
    
    protected void GridViewLeavePending_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlActionType = (DropDownList)e.Row.FindControl("ddlActionType");
            string empcode = e.Row.Cells[2].Text;
            int plevelid = Convert.ToInt32(GridViewLeavePending.DataKeys[e.Row.RowIndex].Values[1].ToString());
            string Processid = GridViewLeavePending.DataKeys[e.Row.RowIndex].Values[2].ToString();
            string flowid = GridViewLeavePending.DataKeys[e.Row.RowIndex].Values[3].ToString();
            DataTable dt= LoadActionPermissionIntoDropDownList(Session["ActingPersonID"].ToString(), empcode, Processid, flowid, plevelid);
            ddlActionType.Items.Clear();
            ddlActionType.Items.Add("--please select--");
            if (dt.Rows.Count == 1)
            {
                ddlActionType.Items.Clear();
            }
            if (dt.Rows.Count == 0)
            {
                ddlActionType.Items.Clear();
                ddlActionType.Items.Add("--No Permission--");
            }
            foreach (DataRow dr in dt.Rows)
            {
                ListItem lst = new ListItem();
                lst.Value = dr["ActionTypeId"].ToString();
                lst.Text = dr["Action"].ToString();
                ddlActionType.Items.Add(lst);
            }
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
            for (int i = 0; i < 12; i++ )
            {
                e.Row.Cells[i].Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Cells[i].Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Cells[i].Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GridViewLeavePending, "Select$" + e.Row.RowIndex);
            }
        }
        GridViewLeavePending.Columns[5].Visible = false;
        GridViewLeavePending.Columns[8].Visible = false;
        GridViewLeavePending.Columns[10].Visible = false;
        GridViewLeavePending.Columns[11].Visible = false;
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        SqlConnection myConnection = new SqlConnection(ConnectionStr);
        myConnection.Open();
        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;
        SqlDataAdapter sqlDataAdapterObj = null;
        string retValue = "";
        int levelid = 0;
        myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        try
        {
            List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
            for (int i = 0; i < grdOvertime.Rows.Count; i++)
            {
                string ProcessID = Session["processid"].ToString();//"P004";
                string FlowID = Session["processFlowId"].ToString();//"4";
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId = 5;  // forward
                CheckBox chkbox = grdOvertime.Rows[i].FindControl("CheckRet") as CheckBox;
                if (chkbox.Checked == true)
                {
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();
                    MKB.TimePicker.TimeSelector mkb = (MKB.TimePicker.TimeSelector)grdOvertime.Rows[i].FindControl("timeoff1");
                    MKB.TimePicker.TimeSelector mkb2 = (MKB.TimePicker.TimeSelector)grdOvertime.Rows[i].FindControl("timeoff2");
                    offphdr.ApplicantId = Session["empcode"].ToString();
                    //offphdr.ProcessnextlevelId = 1;
                    //offphdr.ActiontypeId = 1;
                    offphdr.SL = Convert.ToInt32(grdOvertime.Rows[i].Cells[0].Text.ToString());
                    offphdr.ClaiminDdate = Convert.ToDateTime(grdOvertime.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysIndate = Convert.ToDateTime(grdOvertime.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysOutdate = Convert.ToDateTime(grdOvertime.Rows[i].Cells[1].Text.ToString());
                    offphdr.ActIndate = Convert.ToDateTime(grdOvertime.Rows[i].Cells[1].Text.ToString());
                    offphdr.ActOutdate = Convert.ToDateTime(((TextBox)(grdOvertime.Rows[i].Cells[8].FindControl("txtntDate1"))).Text == "" ? "" : ((TextBox)(grdOvertime.Rows[i].Cells[8].FindControl("txtntDate1"))).Text);
                    offphdr.SysIntime = grdOvertime.Rows[i].Cells[4].Text.ToString() == "&nbsp;" ? "00:00 AM" : grdOvertime.Rows[i].Cells[4].Text.ToString();
                    offphdr.SysOuttime = grdOvertime.Rows[i].Cells[5].Text.ToString() == "&nbsp;" ? "00:00 AM" : grdOvertime.Rows[i].Cells[5].Text.ToString();
                    offphdr.SysTotalhrs = grdOvertime.Rows[i].Cells[6].Text.ToString() == "&nbsp;" ? "00:00" : grdOvertime.Rows[i].Cells[6].Text.ToString();
                    offphdr.ActIntime = timeformat(mkb.Date.Hour.ToString() + ":" + mkb.Date.Minute.ToString() + ":" + mkb.AmPm.ToString());
                    offphdr.ActOuttime = timeformat(mkb2.Date.Hour.ToString() + ":" + mkb2.Date.Minute.ToString() + ":" + mkb2.AmPm.ToString());
                    //offphdr.EntryUserid = Session["EntryUserid"].ToString();
                    offphdr.NoofLeave = Convert.ToDouble(grdOvertime.Rows[i].Cells[21].Text.ToString()); ;
                    offphdr.Leavetype = "OT";
                    offphdr.ProcessId = ProcessID.ToString();
                    offphdr.FlowId = FlowID.ToString();
                    offphdr.ActingpersonId = ActingPersonId.ToString();
                    offphdr.Remarks = txtRemarks.Text.Replace("'", "") == "" ? "NA" : txtRemarks.Text.Replace("'", "");
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.TransactionLineNo = Convert.ToInt32(grdOvertime.Rows[i].Cells[20].Text.ToString());
                    offphdr.TransactionNo = Convert.ToString(grdOvertime.Rows[i].Cells[19].Text.ToString());
                    offphdr.ProcesslevelId = Convert.ToInt32(grdOvertime.Rows[i].Cells[16].Text.ToString());
                    lvphdrlst.Add(offphdr);
                }
            }
            LeaveProcess lvproc = new LeaveProcess();
            string retval = lvproc.SaveApproveOvertimeProcessData(lvphdrlst, myCommand);
            if (retval.ToString() == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }

            LoadAllInformation();
            ReloadAllgridAfterForwardRejectApprove();
            ForExpandPendingApplications(grdOvertime, Panel50);

            if (retval.ToString() != "")
            {
                //SendMailforForword(levelid);
            }

        }
        catch (Exception ex)
        {
            //myTrans.Rollback("SaveAllTransaction");
        }
        finally
        {
            myConnection.Close();
        }
    }
    private void SendMailforApproval()
    {
        try
        {
            // mail send for Approval
            DataTable dts = DataProcess.GetData(ConnectionStr, "select a.EmpName,b.Emp_Mas_Remarks from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id where Emp_Mas_Emp_Id='" + Session["ActingPersonID"].ToString() + "'");
            DataTable dtr = DataProcess.GetData(ConnectionStr, "select a.EmpName,b.Emp_Mas_Remarks from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id where Emp_Mas_Emp_Id='" + Session["empcode"].ToString() + "'");
            string sid = dts.Rows[0]["Emp_Mas_Remarks"].ToString();
            string sname = dts.Rows[0]["EmpName"].ToString();
            string rid = dtr.Rows[0]["Emp_Mas_Remarks"].ToString();
            string rname = dtr.Rows[0]["EmpName"].ToString();
            string msub = "Approve Overtime application";
            string msgbody = "Your Overtime has been approved";
            string atn = "Dear Mr/Ms " + rname + ",";
            //string mbody = arrange_data(Session["ApplicantID"].ToString(), rname, msgbody, atn);
            string mbody = "";
            string ccid = "";
            Sendmail(sid, sname, rid, rname, ccid, msub, mbody);
            //
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void SendMailforForword(int levelid)
    {
        try
        {
            // mail send for Approval
            DataTable dts = DataProcess.GetData(ConnectionStr, "select a.EmpName,b.Emp_Mas_Remarks from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id where Emp_Mas_Emp_Id='" + Session["ActingPersonID"].ToString() + "'");
            DataTable dtr = DataProcess.GetData(ConnectionStr, "select c.Emp_Mas_Remarks,b.EmpName from ProcessAccessPermission a inner join Emp_Details b on a.AccessId=b.EmpID"
                                                              + " inner join hrms_emp_mas c on c.Emp_Mas_Emp_Id=a.AccessId"
                                                              + " where a.ProcessId='P001' and a.ProcessFlowId=1 and a.ProcessLevelid=" + levelid + " and a.ApplicantID='" + Session["empcode"].ToString() + "'");
            string sid = dts.Rows[0]["Emp_Mas_Remarks"].ToString();
            string sname = dts.Rows[0]["EmpName"].ToString();
            string rid = dtr.Rows[0]["Emp_Mas_Remarks"].ToString();
            string rname = dtr.Rows[0]["EmpName"].ToString();
            string msub = "Leave application forworded";
            string attn = "Dear sir,";
            string msgbody = "I sent a leave application for your approval";
            //string mbody = arrange_data(Session["ApplicantID"].ToString(), sname, msgbody, attn);
            string ccid = "";
            // Sendmail(sid, sname, rid, rname, ccid, msub, mbody);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
    }

    private string CheckLeaveValidation(double rCL,double rSL, double rEL)
    {
        string msg = "You have remaining ";
        //string msg1 = "Yes";
        //double CL=0;
        //double SL = 0;
        //double EL = 0;
        //double nooflv = 0;
        //string lvtype="";
        //string lvtypeselect = "";
        //for (int i = 0; i < GridViewLeave.Rows.Count; i++)
        //{ 
        //    CheckBox chkbox = GridViewLeave.Rows[i].FindControl("CheckRet") as CheckBox;
        //    if (chkbox.Checked == true)
        //    {                            
        //        DropDownList dpllvno = GridViewLeave.Rows[i].FindControl("dpllday") as DropDownList;
        //        DropDownList dpllvtype = GridViewLeave.Rows[i].FindControl("lvtype") as DropDownList;
        //        lvtype=dpllvtype.SelectedItem.Value.ToString();
        //        nooflv = Convert.ToDouble(dpllvno.SelectedItem.Value);
        //        if (lvtype.ToString() == "")
        //        {
        //            lvtypeselect = "NA"; 
        //        }
        //        if (nooflv.ToString() == "0")
        //        {
        //            lvtypeselect = "NA";
        //        }
        //        if (lvtype == "CL")
        //        {
        //            CL = CL + nooflv;
        //        }
        //        else if (lvtype == "SL")
        //        {
        //            SL = SL + nooflv;
        //        }
        //        else if (lvtype == "EL")
        //        {
        //            EL = EL + nooflv;
        //        } 
        //    }
        //}
        //if (lvtypeselect == "NA")
        //{
        //    msg = "Please select leave type and leave days then apply";
        //    msg1 = "No";
        //}
        //else
        //{
        //    if (CL > rCL)
        //    {
        //        msg = msg + "CL:" + rCL.ToString() + " but you are going to apply " + CL.ToString() + ";";
        //        msg1 = "No";
        //    }
        //    if (SL > rSL)
        //    {
        //        msg = msg + "SL:" + rCL.ToString() + " but you are going to apply " + SL.ToString() + ";";
        //        msg1 = "No";
        //    }
        //    if (EL > rEL)
        //    {
        //        msg = msg + "EL:" + rCL.ToString() + " but you are going to apply " + EL.ToString() + ";";
        //        msg1 = "No";
        //    }
        //}
        //msg = msg +"@" + msg1;
        return msg;
    }

    private void LoadAllInformation()
    {
        //DateTime fDate = Convert.ToDateTime("01/01/2013");
        //DateTime lDate = Convert.ToDateTime("31/01/2013");
        DateTime fDate = Convert.ToDateTime(Session["fdate"].ToString());
        DateTime lDate = Convert.ToDateTime(Session["lDate"].ToString());
        LoadAllPendingApplication(Session["ActingPersonID"].ToString());//, "P004", "4");
        // LoadAttendanceDetailsemployeeID(Session["ApplicantID"].ToString(), "P001", "F001", fDate, lDate);
        // LoadLeavBALeByemployeeID(Session["ApplicantID"].ToString(), fDate);
        // LoadEmployeeInformation(Session["ApplicantID"].ToString());
        lblPeriod.Text = FinanialPeriod(fDate);
        lblcurrentPeriod.Text = System.DateTime.Now.ToString();
    }

    protected void btnForward_Click(object sender, EventArgs e)
    {
        SqlConnection myConnection = new SqlConnection(ConnectionStr);
        myConnection.Open();
        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;
        SqlDataAdapter sqlDataAdapterObj = null;
        string retValue = "";
        int levelid = 0;
        myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        try
        {
            List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
            for (int i = 0; i < grdOvertime.Rows.Count; i++)
            {
                string ProcessID = Session["processid"].ToString();//"P004";
                string FlowID = Session["processFlowId"].ToString();//"4";
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId = 2;  // forward
                CheckBox chkbox = grdOvertime.Rows[i].FindControl("CheckRet") as CheckBox;
                if (chkbox.Checked == true)
                {
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();
                    offphdr.ProcessId = ProcessID.ToString();
                    offphdr.FlowId = FlowID.ToString();
                    offphdr.ActingpersonId = ActingPersonId.ToString();
                    offphdr.Remarks = txtRemarks.Text.Replace("'", "") == "" ? "NA" : txtRemarks.Text.Replace("'", "");
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.TransactionLineNo = Convert.ToInt32(grdOvertime.Rows[i].Cells[20].Text.ToString());
                    offphdr.TransactionNo = Convert.ToString(grdOvertime.Rows[i].Cells[19].Text.ToString());
                    offphdr.ProcesslevelId = Convert.ToInt32(grdOvertime.Rows[i].Cells[16].Text.ToString());
                    lvphdrlst.Add(offphdr);
                }
            }
            LeaveProcess lvproc = new LeaveProcess();
            string retval = lvproc.SaveForwardOvertimeProcessData(lvphdrlst, myCommand);
            if (retval.ToString() == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }
            LoadAllInformation();
            ReloadAllgridAfterForwardRejectApprove();
            ForExpandPendingApplications(grdOvertime, Panel50);
            if (retval.ToString() != "")
            {
                //SendMailforForword(levelid);
            }
        }
        catch (Exception ex)
        {
            //myTrans.Rollback("SaveAllTransaction");
        }
        finally
        {
            myConnection.Close();
        }
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        SqlConnection myConnection = new SqlConnection(ConnectionStr);
        myConnection.Open();
        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;
        SqlDataAdapter sqlDataAdapterObj = null;
        string retValue = "";
        int levelid = 0;
        myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        try
        {
            List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
            for (int i = 0; i < grdOvertime.Rows.Count; i++)
            {
                string ProcessID = Session["processid"].ToString();//"P004";
                string FlowID = Session["processFlowId"].ToString();//"4";
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId = 4;  // forward
                CheckBox chkbox = grdOvertime.Rows[i].FindControl("CheckRet") as CheckBox;
                if (chkbox.Checked == true)
                {
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();
                    offphdr.ProcessId = ProcessID.ToString();
                    offphdr.FlowId = FlowID.ToString();
                    offphdr.ActingpersonId = ActingPersonId.ToString();
                    offphdr.Remarks = txtRemarks.Text.Replace("'", "") == "" ? "NA" : txtRemarks.Text.Replace("'", "");
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.TransactionLineNo = Convert.ToInt32(grdOvertime.Rows[i].Cells[20].Text.ToString());
                    offphdr.TransactionNo = Convert.ToString(grdOvertime.Rows[i].Cells[19].Text.ToString());
                    offphdr.ProcesslevelId = Convert.ToInt32(grdOvertime.Rows[i].Cells[16].Text.ToString());
                    lvphdrlst.Add(offphdr);
                }
            }
            LeaveProcess lvproc = new LeaveProcess();
            string retval = lvproc.SaveRejectOvertimeProcessData(lvphdrlst, myCommand);
            if (retval.ToString() == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }
            LoadAllInformation();
            ReloadAllgridAfterForwardRejectApprove();
            ForExpandPendingApplications(grdOvertime, Panel50);
            if (retval.ToString() != "")
            {
                SendMailforForword(levelid);
            }
        }
        catch (Exception ex)
        {
            //myTrans.Rollback("SaveAllTransaction");
        }
        finally
        {
            myConnection.Close();
        }
    }
    protected void btnPostLeave_Click(object sender, EventArgs e)
    {
        //SqlConnection myConnection = new SqlConnection(ConnectionStr);
        //myConnection.Open();
        //SqlCommand myCommand = myConnection.CreateCommand();
        //SqlTransaction myTrans;
        //SqlDataAdapter sqlDataAdapterObj = null;
        //string retValue = "";
        //myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        //myCommand.Connection = myConnection;
        //myCommand.Transaction = myTrans;
        //try
        //{
        //    List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
        //    for (int i = 0; i < GridViewLeave.Rows.Count; i++)
        //    {
        //        string ProcessID = "P001";
        //        string FlowID = "1";
        //        int levelid = 0;
        //        string ApplicantID = "L3T593";
        //        CheckBox chkbox = GridViewLeave.Rows[i].FindControl("CheckRet") as CheckBox;
        //        if (chkbox.Checked == true)
        //        {
        //            LeaveProcessHeader offphdr = new LeaveProcessHeader();
        //            TextBox tb = GridViewLeave.Rows[i].FindControl("txtlvRemarks") as TextBox;
        //            DropDownList dpllvno = GridViewLeave.Rows[i].FindControl("dpllday") as DropDownList;
        //            DropDownList dpllvtype = GridViewLeave.Rows[i].FindControl("lvtype") as DropDownList;
        //            offphdr.ApplicantId = ApplicantID.ToString();
        //            offphdr.SL = Convert.ToInt32(GridViewLeave.Rows[i].Cells[0].Text.ToString());
        //            offphdr.ClaiminDdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text.ToString());
        //            offphdr.SysIndate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text.ToString());
        //            offphdr.SysOutdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text.ToString());
        //            offphdr.ActIndate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text.ToString());
        //            offphdr.ActOutdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text.ToString());
        //            offphdr.SysIntime = GridViewLeave.Rows[i].Cells[4].Text.ToString();
        //            offphdr.SysOuttime = GridViewLeave.Rows[i].Cells[5].Text.ToString();
        //            offphdr.SysTotalhrs = GridViewLeave.Rows[i].Cells[6].Text.ToString();
        //            offphdr.NoofLeave = Convert.ToDouble(dpllvno.SelectedItem.Value);
        //            offphdr.Leavetype = dpllvtype.SelectedItem.Value.ToString();
        //            offphdr.Leavedescription = dpllvtype.SelectedItem.Text.ToString();
        //            offphdr.Remarks = tb.Text.ToString();
        //            offphdr.Acthrs = "00:00";
        //            lvphdrlst.Add(offphdr);
        //        }
        //    }
        //    LeaveProcess lvproc = new LeaveProcess();
        //    string retval = lvproc.SaveLeaveData(lvphdrlst, myCommand);
        //    if (retval.ToString() == "")
        //    {
        //        myTrans.Rollback("SaveAllTransaction");
        //    }
        //    else
        //    {
        //        myTrans.Commit();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    myTrans.Rollback("SaveAllTransaction");
        //}
        //finally
        //{
        //    myConnection.Close();
        //}
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        lblcurrentPeriod.Text = DateTime.Now.ToString();
    }
    protected void btnprevious_Click(object sender, EventArgs e)
    {
        DateTime fDate = Convert.ToDateTime("01/12/2012");
        DateTime lDate = Convert.ToDateTime("31/12/2013");
        //LoadAttendanceDetailsemployeeID(Session["ApplicantID"].ToString(), "P001", "1", fDate, lDate);
        LoadLeavBALeByemployeeID(Session["ApplicantID"].ToString(), fDate);
        LoadEmployeeInformation(Session["ApplicantID"].ToString());
        //PanelLeaveHdr.Visible=false; 
        lblPeriod.Text = FinanialPeriod(fDate);
        lblcurrentPeriod.Text = System.DateTime.Now.ToString();
    }
    protected void btnCurrentPeriod_Click(object sender, EventArgs e)
    {
        ReloadAllgridAfterForwardRejectApprove();
        btnPostLeave.Visible = false;
        btnApply.Visible = false;
        btnForward.Visible = false;
        btnReturn.Visible = false;
        btnReject.Visible = false;
        btnApprove.Visible = false;
        LoadActionPermissionForApprovalPerson(Session["ActingPersonID"].ToString(), Session["empcode"].ToString(), "P001", "1", Convert.ToInt32(Session["plevelid"].ToString()));
    }
    protected void gdvLeaveInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false; 
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        DateTime fDate = Convert.ToDateTime(txtFromDate.Text);
        DateTime lDate = Convert.ToDateTime(txtToDate.Text);
        //LoadLeaveByemployeeID(Session["empcode"].ToString(), "P001", "1", fDate, lDate);
    }
    protected void btnShowALL_Click(object sender, EventArgs e)
    {
        //DateTime fDate = Convert.ToDateTime(txtFromDate.Text);
        //DateTime lDate = Convert.ToDateTime(txtToDate.Text);
        //Session["ApplicantID"] = "L3T716";
        //LoadOvertimeDetailsemployeeID(Session["ApplicantID"].ToString(), "P004", "1", fDate, lDate);
    }

    protected void GridViewLeavePending_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        string str0 = gv.DataKeyNames[0];
        Session["LeaveCode"] = gv.DataKeys[gv.SelectedIndex].Values[0].ToString(); 
        Session["plevelid"] = gv.DataKeys[gv.SelectedIndex].Values[1].ToString();
        Session["processid"] = gv.DataKeys[gv.SelectedIndex].Values[2].ToString();
        Session["processFlowId"] = gv.DataKeys[gv.SelectedIndex].Values[3].ToString();
        int indx = GridViewLeavePending.SelectedIndex;
        if (indx == -1)
        {
            return;
        }
        Session["referance"] = GridViewLeavePending.Rows[indx].Cells[1].Text.ToString();
        Session["empcode"] = GridViewLeavePending.Rows[indx].Cells[2].Text.ToString();
        ReloadAllgridAfterForwardRejectApprove();
        CollapsiblePanelExtenderSrch.Collapsed = true;
        CollapsiblePanelExtenderSrch.ClientState = "true";
    }

    private void ReloadAllgridAfterForwardRejectApprove()
    {
        DateTime fDate = Convert.ToDateTime(Session["fdate"].ToString());


        if (Session["processid"].ToString() == "P004")
        {
            ReloadAllgridAfterForwardRejectApproveOvertime();
            btnPostLeave.Visible = false;
            btnApply.Visible = false;
            btnForward.Visible = false;
            btnReturn.Visible = false;
            btnReject.Visible = false;
            btnApprove.Visible = false;
            LoadActionPermissionForApprovalPerson(Session["ActingPersonID"].ToString(), Session["empcode"].ToString(), Session["processid"].ToString(), Session["processFlowId"].ToString(), Convert.ToInt32(Session["plevelid"].ToString()));

        }
        else if (Session["processid"].ToString() == "P006")
        {
            ReloadAllgridAfterForwardRejectApproveNight();
            btnForwardNight.Visible = false;
            btnReturnNight.Visible = false;
            btnRejectNight.Visible = false;
            btnApproveNight.Visible = false;
            LoadActionPermissionForApprovalPersonForNight(Session["ActingPersonID"].ToString(), Session["empcode"].ToString(), Session["processid"].ToString(), Session["processFlowId"].ToString(), Convert.ToInt32(Session["plevelid"].ToString()));
        }
        else if (Session["processid"].ToString() == "P001")
        {
            ReloadAllgridAfterForwardRejectApproveLeave();
            btnForwardLeave.Visible = false;
            btnReturnLeave.Visible = false;
            btnRejectLeave.Visible = false;
            btnApproveLeave.Visible = false;
            LoadActionPermissionForApprovalPersonForLeave(Session["ActingPersonID"].ToString(), Session["empcode"].ToString(), Session["processid"].ToString(), Session["processFlowId"].ToString(), Convert.ToInt32(Session["plevelid"].ToString()));
        }
        else if (Session["processid"].ToString() == "P002")
        {
            ReloadAllgridAfterForwardRejectApproveOffday();
            btnForwardOffday.Visible = false;
            btnReturnOffday.Visible = false;
            btnRejectOffday.Visible = false;
            btnApproveOffday.Visible = false;
            LoadActionPermissionForApprovalPersonForOffday(Session["ActingPersonID"].ToString(), Session["empcode"].ToString(), Session["processid"].ToString(), Session["processFlowId"].ToString(), Convert.ToInt32(Session["plevelid"].ToString()));
        }
        else if (Session["processid"].ToString() == "P007")
        {
            ReloadAllgridAfterForwardRejectApproveAttendance();
            btnForwardAttendance.Visible = false;
            btnReturnAttendance.Visible = false;
            btnRejectAttendance.Visible = false;
            btnApproveAttendance.Visible = false;
            LoadActionPermissionForApprovalPersonForAttendance(Session["ActingPersonID"].ToString(), Session["empcode"].ToString(), Session["processid"].ToString(), Session["processFlowId"].ToString(), Convert.ToInt32(Session["plevelid"].ToString()));
        }
        else if (Session["processid"].ToString() == "P008")
        {
            ReloadAllgridAfterForwardRejectApproveEarnedLeave();
            btnForwardEarnedLeave.Visible = false;
            btnReturnEarnedLeave.Visible = false;
            btnRejectEarnedLeave.Visible = false;
            btnApproveEarnedLeave.Visible = false;
            LoadActionPermissionForApprovalPersonForEarnedLeave(Session["ActingPersonID"].ToString(), Session["empcode"].ToString(), Session["processid"].ToString(), Session["processFlowId"].ToString(), Convert.ToInt32(Session["plevelid"].ToString()));
        }
        if (grdOvertime.Rows.Count < 1)
        {
            Panel50.Visible = false;
        }
        else
        {
            Panel50.Visible = true;
        }
        if (grdNight.Rows.Count < 1)
        {
            Panel1.Visible = false;
        }
        else
        {
            Panel1.Visible = true;
        }
        if (GridViewLeave.Rows.Count < 1)
        {
            Panel2.Visible = false;
        }
        else
        {
            Panel2.Visible = true;
        }
        if (GridViewOffday.Rows.Count < 1)
        {
            Panel4.Visible = false;
        }
        else
        {
            Panel4.Visible = true;
        }
        if (grdAttendance.Rows.Count < 1)
        {
            Panel6.Visible = false;
        }
        else
        {
            Panel6.Visible = true;
        }
        if (GridViewEarnedLeave.Rows.Count < 1)
        {
            Panel8.Visible = false;
        }
        else
        {
            Panel8.Visible = true;
        }
    }
    private void ReloadAllgridAfterForwardRejectApproveNight()
    {
        LoadPendingNightByemployeeID(Session["referance"].ToString(), Session["processid"].ToString(), Session["processFlowId"].ToString(), Session["ActingPersonID"].ToString(), Session["LeaveCode"].ToString());
        LoadPendingNightRemarksByemployeeID(Session["referance"].ToString(), Session["processid"].ToString(), Session["processFlowId"].ToString(), Session["ActingPersonID"].ToString(), Session["LeaveCode"].ToString());
        LoadEmployeeInformationForNight(Session["empcode"].ToString());
        grdOvertime.DataSource = null;
        grdOvertime.DataBind();
        GridViewLeave.DataSource = null;
        GridViewLeave.DataBind();
        GridViewOffday.DataSource = null;
        GridViewOffday.DataBind();
        grdAttendance.DataSource = null;
        grdAttendance.DataBind();
        GridViewEarnedLeave.DataSource = null;
        GridViewEarnedLeave.DataBind();
    }
    private void ReloadAllgridAfterForwardRejectApproveOvertime()
    {
        DateTime fDate = Convert.ToDateTime(Session["fdate"].ToString());
        LoadPendingOvertimeByemployeeID(Session["referance"].ToString(), Session["processid"].ToString(), Session["processFlowId"].ToString(), Session["ActingPersonID"].ToString(), Session["LeaveCode"].ToString());
        LoadPendingOvertimeRemarksByemployeeID(Session["referance"].ToString(), Session["processid"].ToString(), Session["processFlowId"].ToString(), Session["ActingPersonID"].ToString(), Session["LeaveCode"].ToString());
        LoadEmployeeInformation(Session["empcode"].ToString());
        grdNight.DataSource = null;
        grdNight.DataBind();
        GridViewLeave.DataSource = null;
        GridViewLeave.DataBind();
        GridViewOffday.DataSource = null;
        GridViewOffday.DataBind();
        grdAttendance.DataSource = null;
        grdAttendance.DataBind();
        GridViewEarnedLeave.DataSource = null;
        GridViewEarnedLeave.DataBind();

        DataTable dt = DataProcess.GetData(ConnectionStr, "select distinct b.EmpName from [ProcessFlowdet] a inner join Emp_Details b on a.ResponsiblePerson=b.EmpID  where TransactionNo='" + Session["referance"].ToString() + "'");
        if (dt.Rows.Count != 0)
        {
            lblResponsibleperson.Text = dt.Rows[0]["EmpName"].ToString();
        }
    }
    private void ReloadAllgridAfterForwardRejectApproveLeave()
    {
        DateTime fDate = Convert.ToDateTime(Session["fdate"].ToString());
        LoadPendingLeaveByemployeeID(Session["referance"].ToString(), Session["processid"].ToString(), Session["processFlowId"].ToString(), Session["ActingPersonID"].ToString(), Session["LeaveCode"].ToString());
        LoadPendingLeaveRemarksByemployeeID(Session["referance"].ToString(), Session["processid"].ToString(), Session["processFlowId"].ToString(), Session["ActingPersonID"].ToString(), Session["LeaveCode"].ToString());
        LoadEmployeeInformationForLeave(Session["empcode"].ToString());
        grdOvertime.DataSource = null;
        grdOvertime.DataBind();
        grdNight.DataSource = null;
        grdNight.DataBind();
        GridViewOffday.DataSource = null;
        GridViewOffday.DataBind();
        grdAttendance.DataSource = null;
        grdAttendance.DataBind();
        GridViewEarnedLeave.DataSource = null;
        GridViewEarnedLeave.DataBind();
        //LoadLeavBALeByemployeeID(Session["empcode"].ToString(), fDate);
        DataTable dt = DataProcess.GetData(ConnectionStr, "select distinct b.EmpName from [ProcessFlowdet] a inner join Emp_Details b on a.ResponsiblePerson=b.EmpID  where TransactionNo='" + Session["referance"].ToString() + "'");
        if (dt.Rows.Count != 0)
        {
            lblResponsiblepersonDuringLeave.Text = dt.Rows[0]["EmpName"].ToString();
        }
    }
    private void ReloadAllgridAfterForwardRejectApproveOffday()
    {
        DateTime fDate = Convert.ToDateTime(Session["fdate"].ToString());
        LoadPendingOffdayByemployeeID(Session["referance"].ToString(), Session["processid"].ToString(), Session["processFlowId"].ToString(), Session["ActingPersonID"].ToString(), Session["LeaveCode"].ToString());
        LoadPendingOffdayRemarksByemployeeID(Session["referance"].ToString(), Session["processid"].ToString(), Session["processFlowId"].ToString(), Session["ActingPersonID"].ToString(), Session["LeaveCode"].ToString());
        LoadEmployeeInformationForOffday(Session["empcode"].ToString());
        grdOvertime.DataSource = null;
        grdOvertime.DataBind();
        grdNight.DataSource = null;
        grdNight.DataBind();
        GridViewLeave.DataSource = null;
        GridViewLeave.DataBind();
        grdAttendance.DataSource = null;
        grdAttendance.DataBind();
        GridViewEarnedLeave.DataSource = null;
        GridViewEarnedLeave.DataBind();
        DataTable dt = DataProcess.GetData(ConnectionStr, "select distinct b.EmpName from [ProcessFlowdet] a inner join Emp_Details b on a.ResponsiblePerson=b.EmpID  where TransactionNo='" + Session["referance"].ToString() + "'");
        if (dt.Rows.Count != 0)
        {
            lblResponsiblepersonDuringOffday.Text = dt.Rows[0]["EmpName"].ToString();
        }
    }
    private void ReloadAllgridAfterForwardRejectApproveAttendance()
    {
        DateTime fDate = Convert.ToDateTime(Session["fdate"].ToString());
        LoadPendingAttendanceByemployeeID(Session["referance"].ToString(), Session["processid"].ToString(), Session["processFlowId"].ToString(), Session["ActingPersonID"].ToString(), Session["LeaveCode"].ToString());
        LoadPendingAttendanceRemarksByemployeeID(Session["referance"].ToString(), Session["processid"].ToString(), Session["processFlowId"].ToString(), Session["ActingPersonID"].ToString(), Session["LeaveCode"].ToString());
        LoadEmployeeInformationForAttendance(Session["empcode"].ToString());
        grdOvertime.DataSource = null;
        grdOvertime.DataBind();
        grdNight.DataSource = null;
        grdNight.DataBind();
        GridViewLeave.DataSource = null;
        GridViewLeave.DataBind();
        GridViewOffday.DataSource = null;
        GridViewOffday.DataBind();
        GridViewEarnedLeave.DataSource = null;
        GridViewEarnedLeave.DataBind();
        //DataTable dt = DataProcess.GetData(ConnectionStr, "select distinct b.EmpName from [ProcessFlowdet] a inner join Emp_Details b on a.ResponsiblePerson=b.EmpID  where TransactionNo='" + Session["referance"].ToString() + "'");
        //if (dt.Rows.Count != 0)
        //{
        //    lblResponsiblepersonDuringOffday.Text = dt.Rows[0]["EmpName"].ToString();
        //}
    }
    private void ReloadAllgridAfterForwardRejectApproveEarnedLeave()
    {
        DateTime fDate = Convert.ToDateTime(Session["fdate"].ToString());
        LoadPendingEarnedLeaveByemployeeID(Session["referance"].ToString(), Session["processid"].ToString(), Session["processFlowId"].ToString(), Session["ActingPersonID"].ToString(), Session["LeaveCode"].ToString());
        LoadPendingEarnedLeaveRemarksByemployeeID(Session["referance"].ToString(), Session["processid"].ToString(), Session["processFlowId"].ToString(), Session["ActingPersonID"].ToString(), Session["LeaveCode"].ToString());
        LoadEmployeeInformationForEarnedLeave(Session["empcode"].ToString());
        grdOvertime.DataSource = null;
        grdOvertime.DataBind();
        grdNight.DataSource = null;
        grdNight.DataBind();
        GridViewLeave.DataSource = null;
        GridViewLeave.DataBind();
        GridViewOffday.DataSource = null;
        GridViewOffday.DataBind();
        grdAttendance.DataSource = null;
        grdAttendance.DataBind();
    }
    private void LoadPendingNightRemarksByemployeeID(string transactionno, string Processid, string flowid, string actingpersonid, string leavecode)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetNightRemarksByTransactionno";
        cmd.Parameters.Add(new SqlParameter("@transactionno", SqlDbType.NVarChar)).Value = transactionno.ToString();
        cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid.ToString();
        cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid.ToString();
        cmd.Parameters.Add(new SqlParameter("@actingpersonid", SqlDbType.NVarChar)).Value = actingpersonid.ToString();
        cmd.Parameters.Add(new SqlParameter("@leavecode", SqlDbType.NVarChar)).Value = leavecode.ToString();
        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        string remarks = "Please see all remarks below:";
        string remarks1 = "";
        string renarks2 = "";
        string remarksnew = "";
        string remarksline = "----------------------------------";
        int ln = 0;
        foreach (DataRow dr in dt.Rows)
        {
            ln = ln + 1;
            remarksnew = ln.ToString() + ". " + dr["EmpName"].ToString() + " >> " + dr["Remarks"].ToString();
            remarks += "\n" + remarksline.ToString();
            remarks += "\n" + remarksnew.ToString();
        }
        txtRemarksAllNight.Text = remarks;
        int numLines = remarks.ToString().Split('\n').Length;
        string[] lines = txtRemarksAllNight.Text.Split('\n');
        numLines = lines.Length - 1;
        txtRemarksAllNight.Height = (numLines + ln) * 15 + 25;
        txtRemarksAllNight.Enabled = false;
    }
    private void LoadPendingNightByemployeeID(string transactionno, string Processid, string flowid, string actingpersonid, string leavecode)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetPendingNightByTransactionno";
        cmd.Parameters.Add(new SqlParameter("@transactionno", SqlDbType.NVarChar)).Value = transactionno.ToString();
        cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid.ToString();
        cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid.ToString();
        cmd.Parameters.Add(new SqlParameter("@actingpersonid", SqlDbType.NVarChar)).Value = actingpersonid.ToString();
        cmd.Parameters.Add(new SqlParameter("@leavecode", SqlDbType.NVarChar)).Value = leavecode.ToString();
        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        grdNight.DataSource = dt;
        grdNight.DataBind();
    }
    private DataTable LoadActionPermissionIntoDropDownList(string actingpersonid, string empcode, string Processid, string flowid, int plevelid)
    {
        DataTable dt = new DataTable();
        LeaveProcess lvp = new LeaveProcess();
        return dt = lvp.GetApprovalPermissionIntoDDDL(ConnectionStr, actingpersonid, empcode, Processid, flowid, plevelid);
    }

    private void LoadActionPermissionForApprovalPerson(string actingpersonid, string empcode, string Processid, string flowid,int plevelid)
    {
        DataTable dt = new DataTable();
        LeaveProcess lvp = new LeaveProcess();
        dt = lvp.GetApprovalPermission(ConnectionStr, actingpersonid, empcode, Processid, flowid, plevelid);
        foreach (DataRow dr in dt.Rows)
        {
            //if (dr["Part"].ToString() == "1")
            //{
            //    btnApply.Visible=true;
            //}
            if (dr["Part"].ToString() == "2")
            {
                btnForward.Visible = true;
            }
            else if (dr["Part"].ToString() == "3")
            {
                btnReturn.Visible = true;
            }
            else if (dr["Part"].ToString() == "4")
            {
                btnReject.Visible = true;
                
            }
            else if (dr["Part"].ToString() == "5")
            {
                btnApprove.Visible = true;
            }
        }
    }
    private void LoadActionPermissionForApprovalPersonForNight(string actingpersonid, string empcode, string Processid, string flowid, int plevelid)
    {
        DataTable dt = new DataTable();
        LeaveProcess lvp = new LeaveProcess();
        dt = lvp.GetApprovalPermission(ConnectionStr, actingpersonid, empcode, Processid, flowid, plevelid);
        foreach (DataRow dr in dt.Rows)
        {
            //if (dr["Part"].ToString() == "1")
            //{
            //    btnApplyNight.Visible = true;
            //}
            if (dr["Part"].ToString() == "2")
            {
                btnForwardNight.Visible = true;
            }
            else if (dr["Part"].ToString() == "3")
            {
                btnReturnNight.Visible = true;
            }
            else if (dr["Part"].ToString() == "4")
            {
                btnRejectNight.Visible = true;
            }
            else if (dr["Part"].ToString() == "5")
            {
                btnApproveNight.Visible = true;
            }
        }
    }
    private void LoadActionPermissionForApprovalPersonForLeave(string actingpersonid, string empcode, string Processid, string flowid, int plevelid)
    {
        DataTable dt = new DataTable();
        LeaveProcess lvp = new LeaveProcess();
        dt = lvp.GetApprovalPermission(ConnectionStr, actingpersonid, empcode, Processid, flowid, plevelid);
        foreach (DataRow dr in dt.Rows)
        {
            //if (dr["Part"].ToString() == "1")
            //{
            //    btnApply.Visible = true;
            //}
            if (dr["Part"].ToString() == "2")
            {
                btnForwardLeave.Visible = true;
            }
            else if (dr["Part"].ToString() == "3")
            {
                btnReturnLeave.Visible = true;
            }
            else if (dr["Part"].ToString() == "4")
            {
                btnRejectLeave.Visible = true;
            }
            else if (dr["Part"].ToString() == "5")
            {
                btnApproveLeave.Visible = true;
            }
        }
    }
    private void LoadActionPermissionForApprovalPersonForOffday(string actingpersonid, string empcode, string Processid, string flowid, int plevelid)
    {
        DataTable dt = new DataTable();
        LeaveProcess lvp = new LeaveProcess();
        dt = lvp.GetApprovalPermission(ConnectionStr, actingpersonid, empcode, Processid, flowid, plevelid);
        foreach (DataRow dr in dt.Rows)
        {
            //if (dr["Part"].ToString() == "1")
            //{
            //    btnApply.Visible = true;
            //}
            if (dr["Part"].ToString() == "2")
            {
                btnForwardOffday.Visible = true;
            }
            else if (dr["Part"].ToString() == "3")
            {
                btnReturnOffday.Visible = true;
            }
            else if (dr["Part"].ToString() == "4")
            {
                btnRejectOffday.Visible = true;
            }
            else if (dr["Part"].ToString() == "5")
            {
                btnApproveOffday.Visible = true;
            }
        }
    }
    private void LoadActionPermissionForApprovalPersonForAttendance(string actingpersonid, string empcode, string Processid, string flowid, int plevelid)
    {
        DataTable dt = new DataTable();
        LeaveProcess lvp = new LeaveProcess();
        dt = lvp.GetApprovalPermission(ConnectionStr, actingpersonid, empcode, Processid, flowid, plevelid);
        foreach (DataRow dr in dt.Rows)
        {
            //if (dr["Part"].ToString() == "1")
            //{
            //    btnApply.Visible = true;
            //}
            if (dr["Part"].ToString() == "2")
            {
                btnForwardAttendance.Visible = true;
            }
            else if (dr["Part"].ToString() == "3")
            {
                btnReturnAttendance.Visible = true;
            }
            else if (dr["Part"].ToString() == "4")
            {
                btnRejectAttendance.Visible = true;
            }
            else if (dr["Part"].ToString() == "5")
            {
                btnApproveAttendance.Visible = true;
            }
        }
    }
    private void LoadActionPermissionForApprovalPersonForEarnedLeave(string actingpersonid, string empcode, string Processid, string flowid, int plevelid)
    {
        DataTable dt = new DataTable();
        LeaveProcess lvp = new LeaveProcess();
        dt = lvp.GetApprovalPermission(ConnectionStr, actingpersonid, empcode, Processid, flowid, plevelid);
        foreach (DataRow dr in dt.Rows)
        {
            //if (dr["Part"].ToString() == "1")
            //{
            //    btnApply.Visible = true;
            //}
            if (dr["Part"].ToString() == "2")
            {
                btnForwardEarnedLeave.Visible = true;
            }
            else if (dr["Part"].ToString() == "3")
            {
                btnReturnEarnedLeave.Visible = true;
            }
            else if (dr["Part"].ToString() == "4")
            {
                btnRejectEarnedLeave.Visible = true;
            }
            else if (dr["Part"].ToString() == "5")
            {
                btnApproveEarnedLeave.Visible = true;
            }
        }
    }
    private void LoadActionPermissionForApprovalPersonNight(string actingpersonid, string empcode, string Processid, string flowid, int plevelid)
    {
        DataTable dt = new DataTable();
        LeaveProcess lvp = new LeaveProcess();
        dt = lvp.GetApprovalPermission(ConnectionStr, actingpersonid, empcode, Processid, flowid, plevelid);
        foreach (DataRow dr in dt.Rows)
        {
            //if (dr["Part"].ToString() == "1")
            //{
            //    btnApply.Visible = true;
            //}
            if (dr["Part"].ToString() == "2")
            {
                btnForwardNight.Visible = true;
            }
            else if (dr["Part"].ToString() == "3")
            {
                btnReturnNight.Visible = true;
            }
            else if (dr["Part"].ToString() == "4")
            {
                btnRejectNight.Visible = true;
            }
            else if (dr["Part"].ToString() == "5")
            {
                btnApproveNight.Visible = true;
            }
        }
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        SqlConnection myConnection = new SqlConnection(ConnectionStr);
        myConnection.Open();
        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;
        SqlDataAdapter sqlDataAdapterObj = null;
        string retValue = "";
        int levelid = 0;
        myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        try
        {
            List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
            for (int i = 0; i < grdOvertime.Rows.Count; i++)
            {
                string ProcessID = Session["processid"].ToString();//"P004";
                string FlowID = Session["processFlowId"].ToString();//"4";
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId = 3;  // forward
                CheckBox chkbox = grdOvertime.Rows[i].FindControl("CheckRet") as CheckBox;
                if (chkbox.Checked == true)
                {
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();
                    offphdr.ProcessId = ProcessID.ToString();
                    offphdr.FlowId = FlowID.ToString();
                    offphdr.ActingpersonId = ActingPersonId.ToString();
                    offphdr.Remarks = txtRemarks.Text.Replace("'", "") == "" ? "NA" : txtRemarks.Text.Replace("'", "");
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.TransactionLineNo = Convert.ToInt32(grdOvertime.Rows[i].Cells[20].Text.ToString());
                    offphdr.TransactionNo = Convert.ToString(grdOvertime.Rows[i].Cells[19].Text.ToString());
                    offphdr.ProcesslevelId = Convert.ToInt32(grdOvertime.Rows[i].Cells[16].Text.ToString());
                    lvphdrlst.Add(offphdr);
                }
            }
            LeaveProcess lvproc = new LeaveProcess();
            string retval = lvproc.SaveReturnOvertimeProcessData(lvphdrlst, myCommand);
            if (retval.ToString() == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }
            LoadAllInformation();
            ReloadAllgridAfterForwardRejectApprove();
            ForExpandPendingApplications(grdOvertime, Panel50);
            if (retval.ToString() != "")
            {
                SendMailforForword(levelid);
            }
        }
        catch (Exception ex)
        {
            //myTrans.Rollback("SaveAllTransaction");
        }
        finally
        {
            myConnection.Close();
        }
    }
    protected void GridViewLeavePending_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewLeavePending.PageIndex = e.NewPageIndex;
        GridViewLeavePending.DataBind();
        LoadAllPendingApplication(Session["ActingPersonID"].ToString());//, "P004", "4");
    }
    private bool Sendmail(string sid,string sname,string rid,string rname,string ccid,string msub,string mbody)
    { 
        bool flg = false;
        try
        {
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("mail.link3.net");
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
    private string arrange_data(string eid, string sname, string body,string atn)
    {
        string str = "";
        str = "\n" + atn;
        str += "\n\n" + body.ToString();
        str += "\n\nEmployee ID : " + eid;
        str += "\nName          : " + sname.ToString();
        str += "\n\n\n\nTo view detail or update this request just login the link bellow:\n\n";
        //str += "http://office.link3.net/mis/Clientside/frm_login.aspx?updatereferrenceno=" + ref_name;
        str += "http://office.link3.net";
        str += "\n\n\n\n";
        str += "This is auto generated mail from LinkOffice.";
        return str;
    }
    protected void grdOvertime_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
            CheckBox chkbox = e.Row.FindControl("CheckRet") as CheckBox;
            Label lb = e.Row.FindControl("lblstatus") as Label;
            DateTime dta = Convert.ToDateTime(e.Row.Cells[1].Text);
            lb.Text = e.Row.Cells[15].Text;
            MKB.TimePicker.TimeSelector mkb = (MKB.TimePicker.TimeSelector)e.Row.FindControl("timeoff1");
            MKB.TimePicker.TimeSelector mkb2 = (MKB.TimePicker.TimeSelector)e.Row.FindControl("timeoff2");
            string IntimeValue = e.Row.Cells[22].Text.ToString();
            if (IntimeValue != "&nbsp;")
            {
                DateTime dt = DateTime.Parse(IntimeValue);
                MKB.TimePicker.TimeSelector.AmPmSpec am_pm;
                if (dt.ToString("tt") == "AM")
                {
                    am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                }
                else
                {
                    am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
                }
                mkb.SetTime(dt.Hour, dt.Minute, am_pm);
            }
            string OuttimeValue = e.Row.Cells[23].Text.ToString();
            if (OuttimeValue != "&nbsp;")
            {
                DateTime dt = DateTime.Parse(OuttimeValue);
                MKB.TimePicker.TimeSelector.AmPmSpec am_pm;
                if (dt.ToString("tt") == "AM")
                {
                    am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                }
                else
                {
                    am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
                }
                mkb2.SetTime(dt.Hour, dt.Minute, am_pm);
            }
            if (Convert.ToInt32(e.Row.Cells[16].Text) > 0)
            {
                //chkbox.Enabled = false;
                mkb.Enabled = false;
                mkb2.Enabled = false;
                e.Row.BackColor = System.Drawing.Color.LightGreen;
            }
            if (dta < Convert.ToDateTime(Session["fdate"].ToString()))
            {
               // chkbox.Enabled = false;
                mkb.Enabled = false;
                mkb2.Enabled = false;
            }
            //if (e.Row.Cells[13].Text == "Y")
            //{
            //    chkbox.Enabled = false;
            //    mkb.Enabled = false;
            //    mkb2.Enabled = false;
            //}
        }
        e.Row.Cells[3].Visible = false;
        e.Row.Cells[13].Visible = false;
        e.Row.Cells[14].Visible = false;
        e.Row.Cells[15].Visible = false;
        e.Row.Cells[16].Visible = false;
        e.Row.Cells[17].Visible = false;
        e.Row.Cells[18].Visible = false;
        e.Row.Cells[19].Visible = false;
        e.Row.Cells[20].Visible = false;
        e.Row.Cells[21].Visible = false;
        e.Row.Cells[22].Visible = false;
        e.Row.Cells[23].Visible = false;
    }
    protected void btnForwardNight_Click(object sender, EventArgs e)
    {
        SqlConnection myConnection = new SqlConnection(ConnectionStr);
        myConnection.Open();
        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;
        SqlDataAdapter sqlDataAdapterObj = null;
        string retValue = "";
        int levelid = 0;
        myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        try
        {
            List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
            for (int i = 0; i < grdNight.Rows.Count; i++)
            {
                string ProcessID = Session["processid"].ToString(); // "P006";
                string FlowID = Session["processFlowId"].ToString();//"6";
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId = 2;  // forward
                CheckBox chkbox = grdNight.Rows[i].FindControl("CheckRet") as CheckBox;
                if (chkbox.Checked == true)
                {
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();
                    offphdr.ProcessId = ProcessID.ToString();
                    offphdr.FlowId = FlowID.ToString();
                    offphdr.ActingpersonId = ActingPersonId.ToString();
                    offphdr.Remarks = txtRemarksNight.Text.Replace("'", "") == "" ? "NA" : txtRemarksNight.Text.Replace("'", "");
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.TransactionLineNo = Convert.ToInt32(grdNight.Rows[i].Cells[20].Text.ToString());
                    offphdr.TransactionNo = Convert.ToString(grdNight.Rows[i].Cells[19].Text.ToString());
                    offphdr.ProcesslevelId = Convert.ToInt32(grdNight.Rows[i].Cells[16].Text.ToString());
                    lvphdrlst.Add(offphdr);
                }
            }
            LeaveProcess lvproc = new LeaveProcess();
            string retval = lvproc.SaveForwardNightProcessData(lvphdrlst, myCommand);
            if (retval.ToString() == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }
            LoadAllInformation();
            ReloadAllgridAfterForwardRejectApprove();
            ForExpandPendingApplications(grdNight, Panel1);
            if (retval.ToString() != "")
            {
                SendMailforForword(levelid);
            }
        }
        catch (Exception ex)
        {
            //myTrans.Rollback("SaveAllTransaction");
        }
        finally
        {
            myConnection.Close();
        }
    }
    protected void btnRejectNight_Click(object sender, EventArgs e)
    {
        SqlConnection myConnection = new SqlConnection(ConnectionStr);
        myConnection.Open();
        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;
        SqlDataAdapter sqlDataAdapterObj = null;
        string retValue = "";
        int levelid = 0;
        myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        try
        {
            List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
            for (int i = 0; i < grdNight.Rows.Count; i++)
            {
                string ProcessID = Session["processid"].ToString();//"P006";
                string FlowID = Session["processFlowId"].ToString();//"6";
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId = 4;  // forward
                CheckBox chkbox = grdNight.Rows[i].FindControl("CheckRet") as CheckBox;
                if (chkbox.Checked == true)
                {
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();
                    offphdr.ProcessId = ProcessID.ToString();
                    offphdr.FlowId = FlowID.ToString();
                    offphdr.ActingpersonId = ActingPersonId.ToString();
                    offphdr.Remarks = txtRemarksNight.Text.Replace("'", "") == "" ? "NA" : txtRemarksNight.Text.Replace("'", "");
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.TransactionLineNo = Convert.ToInt32(grdNight.Rows[i].Cells[20].Text.ToString());
                    offphdr.TransactionNo = Convert.ToString(grdNight.Rows[i].Cells[19].Text.ToString());
                    offphdr.ProcesslevelId = Convert.ToInt32(grdNight.Rows[i].Cells[16].Text.ToString());
                    lvphdrlst.Add(offphdr);
                }
            }
            LeaveProcess lvproc = new LeaveProcess();
            string retval = lvproc.SaveRejectNightProcessData(lvphdrlst, myCommand);
            if (retval.ToString() == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }
            LoadAllInformation();
            ReloadAllgridAfterForwardRejectApprove();
            ForExpandPendingApplications(grdNight, Panel1);
            if (retval.ToString() != "")
            {
                SendMailforForword(levelid);
            }
        }
        catch (Exception ex)
        {
            //myTrans.Rollback("SaveAllTransaction");
        }
        finally
        {
            myConnection.Close();
        }
    }
    protected void btnApproveNight_Click(object sender, EventArgs e)
    {
        SqlConnection myConnection = new SqlConnection(ConnectionStr);
        myConnection.Open();
        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;
        SqlDataAdapter sqlDataAdapterObj = null;
        string retValue = "";
        int levelid = 0;
        myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        try
        {
            List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
            for (int i = 0; i < grdNight.Rows.Count; i++)
            {
                string ProcessID = Session["processid"].ToString();//"P006";
                string FlowID = Session["processFlowId"].ToString();//"6";
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId = 5;  // forward
                CheckBox chkbox = grdNight.Rows[i].FindControl("CheckRet") as CheckBox;
                if (chkbox.Checked == true)
                {
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();
                    MKB.TimePicker.TimeSelector mkb = (MKB.TimePicker.TimeSelector)grdNight.Rows[i].FindControl("timeoff1");
                    MKB.TimePicker.TimeSelector mkb2 = (MKB.TimePicker.TimeSelector)grdNight.Rows[i].FindControl("timeoff2");
                    offphdr.ApplicantId = Session["empcode"].ToString();
                    //offphdr.ProcessnextlevelId = 1;
                    //offphdr.ActiontypeId = 1;
                    offphdr.SL = Convert.ToInt32(grdNight.Rows[i].Cells[0].Text.ToString());
                    offphdr.ClaiminDdate = Convert.ToDateTime(grdNight.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysIndate = Convert.ToDateTime(grdNight.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysOutdate = Convert.ToDateTime(grdNight.Rows[i].Cells[1].Text.ToString());
                    offphdr.ActIndate = Convert.ToDateTime(grdNight.Rows[i].Cells[1].Text.ToString());
                    offphdr.ActOutdate = Convert.ToDateTime(((TextBox)(grdNight.Rows[i].Cells[8].FindControl("txtntDate1"))).Text == "" ? "" : ((TextBox)(grdNight.Rows[i].Cells[8].FindControl("txtntDate1"))).Text);
                    offphdr.SysIntime = grdNight.Rows[i].Cells[4].Text.ToString() == "&nbsp;" ? "00:00 AM" : grdNight.Rows[i].Cells[4].Text.ToString();
                    offphdr.SysOuttime = grdNight.Rows[i].Cells[5].Text.ToString() == "&nbsp;" ? "00:00 AM" : grdNight.Rows[i].Cells[5].Text.ToString();
                    offphdr.SysTotalhrs = grdNight.Rows[i].Cells[6].Text.ToString() == "&nbsp;" ? "00:00" : grdNight.Rows[i].Cells[6].Text.ToString();
                    offphdr.ActIntime = timeformat(mkb.Date.Hour.ToString() + ":" + mkb.Date.Minute.ToString() + ":" + mkb.AmPm.ToString());
                    offphdr.ActOuttime = timeformat(mkb2.Date.Hour.ToString() + ":" + mkb2.Date.Minute.ToString() + ":" + mkb2.AmPm.ToString());
                    //offphdr.EntryUserid = Session["EntryUserid"].ToString();
                    offphdr.NoofLeave = Convert.ToDouble(grdNight.Rows[i].Cells[21].Text.ToString()); ;
                    offphdr.Leavetype = "NT";
                    offphdr.ProcessId = ProcessID.ToString();
                    offphdr.FlowId = FlowID.ToString();
                    offphdr.ActingpersonId = ActingPersonId.ToString();
                    offphdr.Remarks = txtRemarksNight.Text.Replace("'", "") == "" ? "NA" : txtRemarksNight.Text.Replace("'", "");
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.TransactionLineNo = Convert.ToInt32(grdNight.Rows[i].Cells[20].Text.ToString());
                    offphdr.TransactionNo = Convert.ToString(grdNight.Rows[i].Cells[19].Text.ToString());
                    offphdr.ProcesslevelId = Convert.ToInt32(grdNight.Rows[i].Cells[16].Text.ToString());
                    lvphdrlst.Add(offphdr);
                }
            }
            LeaveProcess lvproc = new LeaveProcess();
            string retval = lvproc.SaveApproveNightProcessData(lvphdrlst, myCommand);
            if (retval.ToString() == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }
            LoadAllInformation();
            ReloadAllgridAfterForwardRejectApprove();
            ForExpandPendingApplications(grdNight, Panel1);
            if (retval.ToString() != "")
            {
                SendMailforForword(levelid);
            }

        }
        catch (Exception ex)
        {
            //myTrans.Rollback("SaveAllTransaction");
        }
        finally
        {
            myConnection.Close();
        }
    }
    protected void btnReturnNight_Click(object sender, EventArgs e)
    {
        SqlConnection myConnection = new SqlConnection(ConnectionStr);
        myConnection.Open();
        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;
        SqlDataAdapter sqlDataAdapterObj = null;
        string retValue = "";
        int levelid = 0;
        myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        try
        {
            List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
            for (int i = 0; i < grdNight.Rows.Count; i++)
            {
                string ProcessID = Session["processid"].ToString();//"P006";
                string FlowID = Session["processFlowId"].ToString();//"6";
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId = 3;  // forward
                CheckBox chkbox = grdNight.Rows[i].FindControl("CheckRet") as CheckBox;
                if (chkbox.Checked == true)
                {
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();
                    offphdr.ProcessId = ProcessID.ToString();
                    offphdr.FlowId = FlowID.ToString();
                    offphdr.ActingpersonId = ActingPersonId.ToString();
                    offphdr.Remarks = txtRemarksNight.Text.Replace("'", "") == "" ? "NA" : txtRemarksNight.Text.Replace("'", "");
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.TransactionLineNo = Convert.ToInt32(grdNight.Rows[i].Cells[20].Text.ToString());
                    offphdr.TransactionNo = Convert.ToString(grdNight.Rows[i].Cells[19].Text.ToString());
                    offphdr.ProcesslevelId = Convert.ToInt32(grdNight.Rows[i].Cells[16].Text.ToString());
                    lvphdrlst.Add(offphdr);
                }
            }
            LeaveProcess lvproc = new LeaveProcess();
            string retval = lvproc.SaveReturnNightProcessData(lvphdrlst, myCommand);
            if (retval.ToString() == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }
            LoadAllInformation();
            ReloadAllgridAfterForwardRejectApprove();
            ForExpandPendingApplications(grdNight, Panel1);
            if (retval.ToString() != "")
            {
                SendMailforForword(levelid);
            }
        }
        catch (Exception ex)
        {
            //myTrans.Rollback("SaveAllTransaction");
        }
        finally
        {
            myConnection.Close();
        }
    }

    protected void grdNight_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
            CheckBox chkbox = e.Row.FindControl("CheckRet") as CheckBox;
            Label lb = e.Row.FindControl("lblstatus") as Label;
            DateTime dta = Convert.ToDateTime(e.Row.Cells[1].Text);
            lb.Text = e.Row.Cells[15].Text;
            MKB.TimePicker.TimeSelector mkb = (MKB.TimePicker.TimeSelector)e.Row.FindControl("timeoff1");
            MKB.TimePicker.TimeSelector mkb2 = (MKB.TimePicker.TimeSelector)e.Row.FindControl("timeoff2");
            string IntimeValue = e.Row.Cells[22].Text.ToString();
            if (IntimeValue != "&nbsp;")
            {
                DateTime dt = DateTime.Parse(IntimeValue);
                MKB.TimePicker.TimeSelector.AmPmSpec am_pm;
                if (dt.ToString("tt") == "AM")
                {
                    am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                }
                else
                {
                    am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
                }
                mkb.SetTime(dt.Hour, dt.Minute, am_pm);
            }
            string OuttimeValue = e.Row.Cells[23].Text.ToString();
            if (OuttimeValue != "&nbsp;")
            {
                DateTime dt = DateTime.Parse(OuttimeValue);
                MKB.TimePicker.TimeSelector.AmPmSpec am_pm;
                if (dt.ToString("tt") == "AM")
                {
                    am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                }
                else
                {
                    am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
                }
                mkb2.SetTime(dt.Hour, dt.Minute, am_pm);
            }
            if (Convert.ToInt32(e.Row.Cells[16].Text) > 0)
            {
                //chkbox.Enabled = false;
                mkb.Enabled = false;
                mkb2.Enabled = false;
                e.Row.BackColor = System.Drawing.Color.LightGreen;
            }
            if (dta < Convert.ToDateTime(Session["fdate"].ToString()))
            {
               // chkbox.Enabled = false;
                mkb.Enabled = false;
                mkb2.Enabled = false;
            }
            //if (e.Row.Cells[13].Text == "Y")
            //{
            //    chkbox.Enabled = false;
            //    mkb.Enabled = false;
            //    mkb2.Enabled = false;
            //}
        }
        e.Row.Cells[3].Visible = false;
        e.Row.Cells[13].Visible = false;
        e.Row.Cells[14].Visible = false;
        e.Row.Cells[15].Visible = false;
        e.Row.Cells[16].Visible = false;
        e.Row.Cells[17].Visible = false;
        e.Row.Cells[18].Visible = false;
        e.Row.Cells[19].Visible = false;
        e.Row.Cells[20].Visible = false;
        e.Row.Cells[21].Visible = false;
        e.Row.Cells[22].Visible = false;
        e.Row.Cells[23].Visible = false;
    }
    protected void GridViewLeave_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
            CheckBox chkbox = e.Row.FindControl("Checklv") as CheckBox;
            TextBox tb = e.Row.FindControl("txtlvRemarks") as TextBox;
            Label lb = e.Row.FindControl("txtlvstatus") as Label;
            tb.Width = 200;
            DropDownList dpl = e.Row.FindControl("dpllday") as DropDownList;
            ListItem lst1 = new ListItem();
            lst1.Text = "--Select--";
            lst1.Value = "0";
            dpl.Items.Add(lst1);
            ListItem lst2 = new ListItem();
            lst2.Text = "Full Day";
            lst2.Value = "1";
            dpl.Items.Add(lst2);
            ListItem lst3 = new ListItem();
            lst3.Text = "Half Day";
            lst3.Value = "0.5";
            dpl.Items.Add(lst3);
            dpl.Width = 75;
            dpl.SelectedValue = e.Row.Cells[14].Text;
            DropDownList dpllt = e.Row.FindControl("lvtype") as DropDownList;
            ListItem lstlt = new ListItem();
            lstlt.Text = "--Select--";
            lstlt.Value = "";
            dpllt.Items.Add(lstlt);
            ListItem lstlt1 = new ListItem();
            lstlt1.Text = "Casual Leave";
            lstlt1.Value = "CL";
            dpllt.Items.Add(lstlt1);
            ListItem lstlt2 = new ListItem();
            lstlt2.Text = "Sick Leave";
            lstlt2.Value = "SL";
            dpllt.Items.Add(lstlt2);
            ListItem lstlt3 = new ListItem();
            lstlt3.Text = "Earned Leave";
            lstlt3.Value = "EL";
            dpllt.Items.Add(lstlt3);
            dpllt.Width = 100;
            dpllt.SelectedValue = e.Row.Cells[13].Text;
            DateTime dta = Convert.ToDateTime(e.Row.Cells[1].Text);
            lb.Text = e.Row.Cells[18].Text;
            if (Convert.ToInt32(e.Row.Cells[19].Text) > 0)
            {
                dpl.Enabled = false;
                dpllt.Enabled = false;
                //chkbox.Enabled = false;
                tb.Enabled = false;
                e.Row.BackColor = System.Drawing.Color.LightGreen;
                e.Row.Cells[3].Text = "Leave";
            }
            if (e.Row.Cells[3].Text == "H")
            {
                e.Row.Cells[3].Text = "Holiday";
                e.Row.Cells[4].Text = "-";
                e.Row.Cells[5].Text = "-";
                e.Row.Cells[6].Text = "-";
                e.Row.BackColor = System.Drawing.Color.LightGray;
                // chkbox.Visible = false;
                tb.Visible = false;
                lb.Visible = false;
                dpl.Visible = false;
                dpllt.Visible = false;
            }
            if (e.Row.Cells[3].Text == "L" || e.Row.Cells[3].Text == "N")
            {
                e.Row.Cells[3].Text = "Leave";
                e.Row.BackColor = System.Drawing.Color.LightGreen;
               // chkbox.Enabled = false;
                tb.Enabled = false;
                lb.Text = e.Row.Cells[17].Text;
                dpl.Enabled = false;
                dpllt.Enabled = false;
            }
            if (e.Row.Cells[3].Text == "A")
            {
                e.Row.Cells[3].Text = "Present";
                chkbox.Enabled = true;
                tb.Enabled = true;
            }
            if (e.Row.Cells[3].Text == "P")
            {
                e.Row.Cells[3].Text = "Absent";
                e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                chkbox.Enabled = true;
                tb.Enabled = true;
            }
            if (dta < Convert.ToDateTime(Session["fdate"].ToString()))
            {
                //chkbox.Enabled = false;
                tb.Enabled = false;
                lb.Enabled = false;
                dpl.Enabled = false;
                dpllt.Enabled = false;
            }
            if (e.Row.Cells[16].Text == "Y")
            {
                chkbox.Enabled = false;
                tb.Enabled = false;
                lb.Enabled = false;
                dpl.Enabled = false;
                dpllt.Enabled = false;
            }

        }
        e.Row.Cells[7].Visible = false;
        e.Row.Cells[13].Visible = false;
        e.Row.Cells[14].Visible = false;
        e.Row.Cells[15].Visible = false;
        e.Row.Cells[16].Visible = false;
        e.Row.Cells[17].Visible = false;
        e.Row.Cells[18].Visible = false;
        e.Row.Cells[19].Visible = false;
        e.Row.Cells[20].Visible = false;
        e.Row.Cells[21].Visible = false;
    }
    protected void btnForwardLeave_Click(object sender, EventArgs e)
    {
        SqlConnection myConnection = new SqlConnection(ConnectionStr);
        myConnection.Open();
        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;
        SqlDataAdapter sqlDataAdapterObj = null;
        string retValue = "";
        int levelid = 0;
        myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        try
        {
            List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
            for (int i = 0; i < GridViewLeave.Rows.Count; i++)
            {
                string ProcessID = Session["processid"].ToString(); //"P001";
                string FlowID = Session["processFlowId"].ToString();//"1";
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId = 2;  // forward
                CheckBox chkbox = GridViewLeave.Rows[i].FindControl("Checklv") as CheckBox;
                if (chkbox.Checked == true)
                {
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();
                    TextBox tb = GridViewLeave.Rows[i].FindControl("txtlvRemarks") as TextBox;
                    DropDownList dpllvno = GridViewLeave.Rows[i].FindControl("dpllday") as DropDownList;
                    DropDownList dpllvtype = GridViewLeave.Rows[i].FindControl("lvtype") as DropDownList;
                    offphdr.TransactionNo = Convert.ToString(GridViewLeave.Rows[i].Cells[20].Text.ToString());
                    offphdr.TransactionLineNo = Convert.ToInt32(GridViewLeave.Rows[i].Cells[21].Text.ToString());
                    offphdr.ApplicantId = Session["empcode"].ToString();
                    offphdr.ProcessId = ProcessID.ToString();
                    offphdr.FlowId = FlowID.ToString();
                    offphdr.ProcesslevelId = Convert.ToInt32(GridViewLeave.Rows[i].Cells[19].Text.ToString());
                    levelid = offphdr.ProcesslevelId + 1;  // To find next level id
                    offphdr.ProcessnextlevelId = 1;
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.SL = Convert.ToInt32(GridViewLeave.Rows[i].Cells[0].Text.ToString());
                    offphdr.ClaiminDdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysIndate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysOutdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.ActIndate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.ActOutdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysIntime = GridViewLeave.Rows[i].Cells[4].Text.ToString() == "&nbsp;" ? "00:00 AM" : GridViewLeave.Rows[i].Cells[4].Text.ToString();
                    offphdr.SysOuttime = GridViewLeave.Rows[i].Cells[5].Text.ToString() == "&nbsp;" ? "00:00 AM" : GridViewLeave.Rows[i].Cells[5].Text.ToString();
                    offphdr.SysTotalhrs = GridViewLeave.Rows[i].Cells[6].Text.ToString() == "&nbsp;" ? "00:00" : GridViewLeave.Rows[i].Cells[6].Text.ToString();
                    offphdr.NoofLeave = Convert.ToDouble(dpllvno.SelectedItem.Value);
                    offphdr.Leavetype = dpllvtype.SelectedItem.Value.ToString();
                    offphdr.Leavedescription = dpllvtype.SelectedItem.Text.ToString();
                    offphdr.Acthrs = "00:00";
                    offphdr.ActingpersonId = ActingPersonId.ToString();
                    offphdr.Remarks = txtRemarksLeave.Text.Replace("'", "") == "" ? "NA" : txtRemarksLeave.Text.Replace("'", "");
                    lvphdrlst.Add(offphdr);
                }
            }
            LeaveProcess lvproc = new LeaveProcess();
            string retval = lvproc.SaveForwardLeaveProcessData(lvphdrlst, myCommand);
            if (retval.ToString() == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }
            LoadAllInformation();
            ReloadAllgridAfterForwardRejectApprove();
            ForExpandPendingApplications(GridViewLeave, Panel2);
            if (retval.ToString() != "")
            {
                //SendMailforForword(levelid);
            }
        }
        catch (Exception ex)
        {
            myTrans.Rollback("SaveAllTransaction");
        }
        finally
        {
            myConnection.Close();
        }
    }
    protected void btnRejectLeave_Click(object sender, EventArgs e)
    {
        SqlConnection myConnection = new SqlConnection(ConnectionStr);
        myConnection.Open();
        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;
        SqlDataAdapter sqlDataAdapterObj = null;
        string retValue = "";
        myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        try
        {
            List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
            for (int i = 0; i < GridViewLeave.Rows.Count; i++)
            {
                string ProcessID = Session["processid"].ToString();//"P001";
                string FlowID = Session["processFlowId"].ToString();//"1";
                int levelid = 0;
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId = 4;          // 97 for reject
                CheckBox chkbox = GridViewLeave.Rows[i].FindControl("Checklv") as CheckBox;
                if (chkbox.Checked == true)
                {
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();
                    TextBox tb = GridViewLeave.Rows[i].FindControl("txtlvRemarks") as TextBox;
                    DropDownList dpllvno = GridViewLeave.Rows[i].FindControl("dpllday") as DropDownList;
                    DropDownList dpllvtype = GridViewLeave.Rows[i].FindControl("lvtype") as DropDownList;
                    offphdr.TransactionNo = Convert.ToString(GridViewLeave.Rows[i].Cells[20].Text.ToString());
                    offphdr.TransactionLineNo = Convert.ToInt32(GridViewLeave.Rows[i].Cells[21].Text.ToString());
                    offphdr.ApplicantId = Session["empcode"].ToString();
                    offphdr.ProcessId = ProcessID.ToString();
                    offphdr.FlowId = FlowID.ToString();
                    offphdr.ProcesslevelId = Convert.ToInt32(GridViewLeave.Rows[i].Cells[19].Text.ToString());
                    offphdr.ProcessnextlevelId = 1;
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.SL = Convert.ToInt32(GridViewLeave.Rows[i].Cells[0].Text.ToString());
                    offphdr.ClaiminDdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysIndate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysOutdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.ActIndate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.ActOutdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysIntime = GridViewLeave.Rows[i].Cells[4].Text.ToString() == "&nbsp;" ? "00:00 AM" : GridViewLeave.Rows[i].Cells[4].Text.ToString();
                    offphdr.SysOuttime = GridViewLeave.Rows[i].Cells[5].Text.ToString() == "&nbsp;" ? "00:00 AM" : GridViewLeave.Rows[i].Cells[5].Text.ToString();
                    offphdr.SysTotalhrs = GridViewLeave.Rows[i].Cells[6].Text.ToString() == "&nbsp;" ? "00:00" : GridViewLeave.Rows[i].Cells[6].Text.ToString();
                    offphdr.NoofLeave = Convert.ToDouble(dpllvno.SelectedItem.Value);
                    offphdr.Leavetype = dpllvtype.SelectedItem.Value.ToString();
                    offphdr.Leavedescription = dpllvtype.SelectedItem.Text.ToString();
                    offphdr.Remarks = txtRemarksLeave.Text.Replace("'", "") == "" ? "NA" : txtRemarksLeave.Text.Replace("'", "");
                    offphdr.Acthrs = "00:00";
                    offphdr.ActingpersonId = ActingPersonId.ToString();
                    lvphdrlst.Add(offphdr);
                }
            }
            LeaveProcess lvproc = new LeaveProcess();
            string retval = lvproc.SaveRejectLeaveProcessData(lvphdrlst, myCommand);
            if (retval.ToString() == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }
            LoadAllInformation();
            ReloadAllgridAfterForwardRejectApproveLeave();
            ForExpandPendingApplications(GridViewLeave, Panel2);
        }
        catch (Exception ex)
        {
            myTrans.Rollback("SaveAllTransaction");
        }
        finally
        {
            myConnection.Close();
        }
    }
    protected void btnApproveLeave_Click(object sender, EventArgs e)
    {
        SqlConnection myConnection = new SqlConnection(ConnectionStr);
        myConnection.Open();
        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;
        SqlDataAdapter sqlDataAdapterObj = null;
        string retValue = "";
        myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        try
        {
            List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
            for (int i = 0; i < GridViewLeave.Rows.Count; i++)
            {
                string ProcessID = Session["processid"].ToString();//"P001";
                string FlowID = Session["processFlowId"].ToString();//"1";
                int levelid = 0;
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId = 5;  // Approve
                CheckBox chkbox = GridViewLeave.Rows[i].FindControl("Checklv") as CheckBox;
                if (chkbox.Checked == true)
                {
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();
                    TextBox tb = GridViewLeave.Rows[i].FindControl("txtlvRemarks") as TextBox;
                    DropDownList dpllvno = GridViewLeave.Rows[i].FindControl("dpllday") as DropDownList;
                    DropDownList dpllvtype = GridViewLeave.Rows[i].FindControl("lvtype") as DropDownList;
                    offphdr.TransactionNo = Convert.ToString(GridViewLeave.Rows[i].Cells[20].Text.ToString());
                    offphdr.TransactionLineNo = Convert.ToInt32(GridViewLeave.Rows[i].Cells[21].Text.ToString());
                    offphdr.ApplicantId = Session["empcode"].ToString();
                    offphdr.ProcessId = ProcessID.ToString();
                    offphdr.FlowId = FlowID.ToString();
                    offphdr.ProcesslevelId = Convert.ToInt32(GridViewLeave.Rows[i].Cells[19].Text.ToString());
                    offphdr.ProcessnextlevelId = 1;
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.SL = Convert.ToInt32(GridViewLeave.Rows[i].Cells[0].Text.ToString());
                    offphdr.ClaiminDdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysIndate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysOutdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.ActIndate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.ActOutdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysIntime = GridViewLeave.Rows[i].Cells[4].Text.ToString() == "&nbsp;" ? "00:00 AM" : GridViewLeave.Rows[i].Cells[4].Text.ToString();
                    offphdr.SysOuttime = GridViewLeave.Rows[i].Cells[5].Text.ToString() == "&nbsp;" ? "00:00 AM" : GridViewLeave.Rows[i].Cells[5].Text.ToString();
                    offphdr.SysTotalhrs = GridViewLeave.Rows[i].Cells[6].Text.ToString() == "&nbsp;" ? "00:00" : GridViewLeave.Rows[i].Cells[6].Text.ToString();
                    offphdr.NoofLeave = Convert.ToDouble(dpllvno.SelectedItem.Value);
                    offphdr.Leavetype = dpllvtype.SelectedItem.Value.ToString();
                    offphdr.Leavedescription = dpllvtype.SelectedItem.Text.ToString();
                    offphdr.Remarks = txtRemarksLeave.Text.Replace("'", "") == "" ? "NA" : txtRemarksLeave.Text.Replace("'", "");
                    offphdr.Acthrs = "00:00";
                    offphdr.ActingpersonId = ActingPersonId.ToString();
                    lvphdrlst.Add(offphdr);
                }
            }
            LeaveProcess lvproc = new LeaveProcess();
            string retval = lvproc.SaveApproveLeaveProcessData(lvphdrlst, myCommand);
            if (retval.ToString() == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }
            LoadAllInformation();
            ReloadAllgridAfterForwardRejectApproveLeave();
            ForExpandPendingApplications(GridViewLeave, Panel2);
            if (retval.ToString() != "")
            {
                SendMailforApproval();   /// Send mail
            }
        }
        catch (Exception ex)
        {
            myTrans.Rollback("SaveAllTransaction");
        }
        finally
        {
            myConnection.Close();
        }
    }
    protected void btnReturnLeave_Click(object sender, EventArgs e)
    {
        SqlConnection myConnection = new SqlConnection(ConnectionStr);
        myConnection.Open();
        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;
        SqlDataAdapter sqlDataAdapterObj = null;
        string retValue = "";
        myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        try
        {
            List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
            for (int i = 0; i < GridViewLeave.Rows.Count; i++)
            {
                string ProcessID = Session["processid"].ToString();//"P001";
                string FlowID = Session["processFlowId"].ToString();//"1";
                int levelid = 0;
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId = 3;  // Return
                CheckBox chkbox = GridViewLeave.Rows[i].FindControl("Checklv") as CheckBox;
                if (chkbox.Checked == true)
                {
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();
                    TextBox tb = GridViewLeave.Rows[i].FindControl("txtlvRemarks") as TextBox;
                    DropDownList dpllvno = GridViewLeave.Rows[i].FindControl("dpllday") as DropDownList;
                    DropDownList dpllvtype = GridViewLeave.Rows[i].FindControl("lvtype") as DropDownList;
                    offphdr.TransactionNo = Convert.ToString(GridViewLeave.Rows[i].Cells[20].Text.ToString());
                    offphdr.TransactionLineNo = Convert.ToInt32(GridViewLeave.Rows[i].Cells[21].Text.ToString());
                    offphdr.ApplicantId = Session["empcode"].ToString();
                    offphdr.ProcessId = ProcessID.ToString();
                    offphdr.FlowId = FlowID.ToString();
                    offphdr.ProcesslevelId = Convert.ToInt32(GridViewLeave.Rows[i].Cells[19].Text.ToString());
                    offphdr.ProcessnextlevelId = 1;
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.SL = Convert.ToInt32(GridViewLeave.Rows[i].Cells[0].Text.ToString());
                    offphdr.ClaiminDdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysIndate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysOutdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.ActIndate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.ActOutdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysIntime = GridViewLeave.Rows[i].Cells[4].Text.ToString() == "&nbsp;" ? "00:00 AM" : GridViewLeave.Rows[i].Cells[4].Text.ToString();
                    offphdr.SysOuttime = GridViewLeave.Rows[i].Cells[5].Text.ToString() == "&nbsp;" ? "00:00 AM" : GridViewLeave.Rows[i].Cells[5].Text.ToString();
                    offphdr.SysTotalhrs = GridViewLeave.Rows[i].Cells[6].Text.ToString() == "&nbsp;" ? "00:00" : GridViewLeave.Rows[i].Cells[6].Text.ToString();
                    offphdr.NoofLeave = Convert.ToDouble(dpllvno.SelectedItem.Value);
                    offphdr.Leavetype = dpllvtype.SelectedItem.Value.ToString();
                    offphdr.Leavedescription = dpllvtype.SelectedItem.Text.ToString();
                    offphdr.Remarks = tb.Text.ToString();
                    offphdr.Acthrs = "00:00";
                    offphdr.ActingpersonId = ActingPersonId.ToString();
                    lvphdrlst.Add(offphdr);
                }
            }
            LeaveProcess lvproc = new LeaveProcess();
            string retval = lvproc.SaveReturnLeaveProcessData(lvphdrlst, myCommand);
            if (retval.ToString() == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }
            LoadAllInformation();
            ReloadAllgridAfterForwardRejectApproveLeave();
            ForExpandPendingApplications(GridViewLeave, Panel2);
        }
        catch (Exception ex)
        {
            myTrans.Rollback("SaveAllTransaction");
        }
        finally
        {
            myConnection.Close();
        }
    }
    protected void GridViewOffday_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
            CheckBox chkbox = e.Row.FindControl("Checklv") as CheckBox;
            TextBox tb = e.Row.FindControl("txtlvRemarks") as TextBox;
            Label lb = e.Row.FindControl("txtlvstatus") as Label;
            tb.Width = 200;
            DateTime dta = Convert.ToDateTime(e.Row.Cells[1].Text);
            MKB.TimePicker.TimeSelector mkb = (MKB.TimePicker.TimeSelector)e.Row.FindControl("timeoff1");
            MKB.TimePicker.TimeSelector mkb2 = (MKB.TimePicker.TimeSelector)e.Row.FindControl("timeoff2");
            DateTime dt1 = DateTime.Parse(e.Row.Cells[22].Text.ToString());
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
            DateTime dt2 = DateTime.Parse(e.Row.Cells[23].Text.ToString());
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
            lb.Text = e.Row.Cells[18].Text;
            if (Convert.ToInt32(e.Row.Cells[19].Text) > 0)
            {
                tb.Enabled = false;
                e.Row.BackColor = System.Drawing.Color.LightGreen;
                e.Row.Cells[3].Text = "Off day";
            }
            if (e.Row.Cells[3].Text == "H")
            {
                e.Row.Cells[3].Text = "Holiday";
                e.Row.Cells[4].Text = "-";
                e.Row.Cells[5].Text = "-";
                e.Row.Cells[6].Text = "-";
                e.Row.BackColor = System.Drawing.Color.LightGray;
                // chkbox.Visible = false;
                tb.Visible = false;
                lb.Visible = false;
            }
            if (e.Row.Cells[3].Text == "L" || e.Row.Cells[3].Text == "N")
            {
                e.Row.Cells[3].Text = "Leave";
                e.Row.BackColor = System.Drawing.Color.LightGreen;
                //chkbox.Enabled = false;
                tb.Enabled = false;
                lb.Text = e.Row.Cells[17].Text;
            }
            if (e.Row.Cells[3].Text == "A")
            {
                e.Row.Cells[3].Text = "Present";
                chkbox.Enabled = true;
                tb.Enabled = true;
            }
            if (e.Row.Cells[3].Text == "P")
            {
                e.Row.Cells[3].Text = "Absent";
                e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                chkbox.Enabled = true;
                tb.Enabled = true;
            }

            if (dta < Convert.ToDateTime(Session["fdate"].ToString()))
            {
                //chkbox.Enabled = false;
                tb.Enabled = false;
                lb.Enabled = false;
            }
            if (e.Row.Cells[16].Text == "Y")
            {
                chkbox.Enabled = false;
                tb.Enabled = false;
                lb.Enabled = false;
            }
        }
        e.Row.Cells[7].Visible = false;
        //e.Row.Cells[8].Visible = false;
        //e.Row.Cells[9].Visible = false;
        e.Row.Cells[13].Visible = false;
        e.Row.Cells[14].Visible = false;
        e.Row.Cells[15].Visible = false;
        e.Row.Cells[16].Visible = false;
        e.Row.Cells[17].Visible = false;
        e.Row.Cells[18].Visible = false;
        e.Row.Cells[19].Visible = false;
        e.Row.Cells[20].Visible = false;
        e.Row.Cells[21].Visible = false;
        e.Row.Cells[22].Visible = false;
        e.Row.Cells[23].Visible = false;
    }
    protected void btnForwardOffday_Click(object sender, EventArgs e)
    {
        SqlConnection myConnection = new SqlConnection(ConnectionStr);
        myConnection.Open();
        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;
        SqlDataAdapter sqlDataAdapterObj = null;
        string retValue = "";
        int levelid = 0;
        myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        try
        {
            List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
            for (int i = 0; i < GridViewOffday.Rows.Count; i++)
            {
                string ProcessID = Session["processid"].ToString();//"P002";
                string FlowID = Session["processFlowId"].ToString();//"2";
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId = 2;  // forward
                CheckBox chkbox = GridViewOffday.Rows[i].FindControl("Checklv") as CheckBox;
                if (chkbox.Checked == true)
                {
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();
                    TextBox tb = GridViewOffday.Rows[i].FindControl("txtlvRemarks") as TextBox;
                    DropDownList dpllvno = GridViewOffday.Rows[i].FindControl("dpllday") as DropDownList;
                    DropDownList dpllvtype = GridViewOffday.Rows[i].FindControl("lvtype") as DropDownList;
                    offphdr.TransactionNo = Convert.ToString(GridViewOffday.Rows[i].Cells[20].Text.ToString());
                    offphdr.TransactionLineNo = Convert.ToInt32(GridViewOffday.Rows[i].Cells[21].Text.ToString());
                    offphdr.ApplicantId = Session["empcode"].ToString();
                    offphdr.ProcessId = ProcessID.ToString();
                    offphdr.FlowId = FlowID.ToString();
                    offphdr.ProcesslevelId = Convert.ToInt32(GridViewOffday.Rows[i].Cells[19].Text.ToString());
                    levelid = offphdr.ProcesslevelId + 1;  // To find next level id
                    offphdr.ProcessnextlevelId = 1;
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.SL = Convert.ToInt32(GridViewOffday.Rows[i].Cells[0].Text.ToString());
                    offphdr.ClaiminDdate = Convert.ToDateTime(GridViewOffday.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysIndate = Convert.ToDateTime(GridViewOffday.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysOutdate = Convert.ToDateTime(GridViewOffday.Rows[i].Cells[1].Text.ToString());
                    offphdr.ActIndate = Convert.ToDateTime(GridViewOffday.Rows[i].Cells[1].Text.ToString());
                    offphdr.ActOutdate = Convert.ToDateTime(GridViewOffday.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysIntime = GridViewOffday.Rows[i].Cells[4].Text.ToString() == "&nbsp;" ? "00:00 AM" : GridViewOffday.Rows[i].Cells[4].Text.ToString();
                    offphdr.SysOuttime = GridViewOffday.Rows[i].Cells[5].Text.ToString() == "&nbsp;" ? "00:00 AM" : GridViewOffday.Rows[i].Cells[5].Text.ToString();
                    offphdr.SysTotalhrs = GridViewOffday.Rows[i].Cells[6].Text.ToString() == "&nbsp;" ? "00:00" : GridViewOffday.Rows[i].Cells[6].Text.ToString();
                    offphdr.Acthrs = "00:00";
                    offphdr.ActingpersonId = ActingPersonId.ToString();
                    offphdr.Remarks = txtRemarksOffday.Text.Replace("'", "") == "" ? "NA" : txtRemarksOffday.Text.Replace("'", "");
                    lvphdrlst.Add(offphdr);
                }
            }
            LeaveProcess lvproc = new LeaveProcess();
            string retval = lvproc.SaveForwardOffdayProcessData(lvphdrlst, myCommand);
            if (retval.ToString() == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }
            LoadAllInformation();
            ReloadAllgridAfterForwardRejectApproveOffday();
            ForExpandPendingApplications(GridViewOffday, Panel4);
            if (retval.ToString() != "")
            {
                //SendMailforForword(levelid);
            }
            // Response.Redirect(Request.Url.AbsoluteUri);
        }
        catch (Exception ex)
        {
            myTrans.Rollback("SaveAllTransaction");
        }
        finally
        {
            myConnection.Close();
        }
    }
    protected void btnReturnOffday_Click(object sender, EventArgs e)
    {
        SqlConnection myConnection = new SqlConnection(ConnectionStr);
        myConnection.Open();
        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;
        SqlDataAdapter sqlDataAdapterObj = null;
        string retValue = "";
        myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        try
        {
            List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
            for (int i = 0; i < GridViewOffday.Rows.Count; i++)
            {
                string ProcessID = Session["processid"].ToString();//"P002";
                string FlowID = Session["processFlowId"].ToString();//"2";
                int levelid = 0;
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId = 3;  // Return
                CheckBox chkbox = GridViewOffday.Rows[i].FindControl("Checklv") as CheckBox;
                if (chkbox.Checked == true)
                {
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();
                    TextBox tb = GridViewOffday.Rows[i].FindControl("txtlvRemarks") as TextBox;
                    DropDownList dpllvno = GridViewOffday.Rows[i].FindControl("dpllday") as DropDownList;
                    DropDownList dpllvtype = GridViewOffday.Rows[i].FindControl("lvtype") as DropDownList;
                    offphdr.TransactionNo = Convert.ToString(GridViewOffday.Rows[i].Cells[20].Text.ToString());
                    offphdr.TransactionLineNo = Convert.ToInt32(GridViewOffday.Rows[i].Cells[21].Text.ToString());
                    offphdr.ApplicantId = Session["empcode"].ToString();
                    offphdr.ProcessId = ProcessID.ToString();
                    offphdr.FlowId = FlowID.ToString();
                    offphdr.ProcesslevelId = Convert.ToInt32(GridViewOffday.Rows[i].Cells[19].Text.ToString());
                    offphdr.ProcessnextlevelId = 1;
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.SL = Convert.ToInt32(GridViewOffday.Rows[i].Cells[0].Text.ToString());
                    offphdr.ClaiminDdate = Convert.ToDateTime(GridViewOffday.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysIndate = Convert.ToDateTime(GridViewOffday.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysOutdate = Convert.ToDateTime(GridViewOffday.Rows[i].Cells[1].Text.ToString());
                    offphdr.ActIndate = Convert.ToDateTime(GridViewOffday.Rows[i].Cells[1].Text.ToString());
                    offphdr.ActOutdate = Convert.ToDateTime(GridViewOffday.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysIntime = GridViewOffday.Rows[i].Cells[4].Text.ToString() == "&nbsp;" ? "00:00 AM" : GridViewOffday.Rows[i].Cells[4].Text.ToString();
                    offphdr.SysOuttime = GridViewOffday.Rows[i].Cells[5].Text.ToString() == "&nbsp;" ? "00:00 AM" : GridViewOffday.Rows[i].Cells[5].Text.ToString();
                    offphdr.SysTotalhrs = GridViewOffday.Rows[i].Cells[6].Text.ToString() == "&nbsp;" ? "00:00" : GridViewOffday.Rows[i].Cells[6].Text.ToString();
                    offphdr.NoofLeave = Convert.ToDouble(dpllvno.SelectedItem.Value);
                    offphdr.Leavetype = dpllvtype.SelectedItem.Value.ToString();
                    offphdr.Leavedescription = dpllvtype.SelectedItem.Text.ToString();
                    offphdr.Remarks = tb.Text.ToString();
                    offphdr.Acthrs = "00:00";
                    offphdr.ActingpersonId = ActingPersonId.ToString();
                    lvphdrlst.Add(offphdr);
                }
            }
            LeaveProcess lvproc = new LeaveProcess();
            string retval = lvproc.SaveReturnOffdayProcessData(lvphdrlst, myCommand);
            if (retval.ToString() == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }
            LoadAllInformation();
            ReloadAllgridAfterForwardRejectApproveOffday();
            ForExpandPendingApplications(GridViewOffday, Panel4);
        }
        catch (Exception ex)
        {
            myTrans.Rollback("SaveAllTransaction");
        }
        finally
        {
            myConnection.Close();
        }
    }
    protected void btnRejectOffday_Click(object sender, EventArgs e)
    {
        SqlConnection myConnection = new SqlConnection(ConnectionStr);
        myConnection.Open();
        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;
        SqlDataAdapter sqlDataAdapterObj = null;
        string retValue = "";
        myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        try
        {
            List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
            for (int i = 0; i < GridViewOffday.Rows.Count; i++)
            {
                string ProcessID = Session["processid"].ToString();//"P002";
                string FlowID = Session["processFlowId"].ToString();//"2";
                int levelid = 0;
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId = 4;          // 97 for reject
                CheckBox chkbox = GridViewOffday.Rows[i].FindControl("Checklv") as CheckBox;
                if (chkbox.Checked == true)
                {
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();
                    TextBox tb = GridViewOffday.Rows[i].FindControl("txtlvRemarks") as TextBox;
                    DropDownList dpllvno = GridViewOffday.Rows[i].FindControl("dpllday") as DropDownList;
                    DropDownList dpllvtype = GridViewOffday.Rows[i].FindControl("lvtype") as DropDownList;
                    offphdr.TransactionNo = Convert.ToString(GridViewOffday.Rows[i].Cells[20].Text.ToString());
                    offphdr.TransactionLineNo = Convert.ToInt32(GridViewOffday.Rows[i].Cells[21].Text.ToString());
                    offphdr.ApplicantId = Session["empcode"].ToString();
                    offphdr.ProcessId = ProcessID.ToString();
                    offphdr.FlowId = FlowID.ToString();
                    offphdr.ProcesslevelId = Convert.ToInt32(GridViewOffday.Rows[i].Cells[19].Text.ToString());
                    offphdr.ProcessnextlevelId = 1;
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.SL = Convert.ToInt32(GridViewOffday.Rows[i].Cells[0].Text.ToString());
                    offphdr.ClaiminDdate = Convert.ToDateTime(GridViewOffday.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysIndate = Convert.ToDateTime(GridViewOffday.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysOutdate = Convert.ToDateTime(GridViewOffday.Rows[i].Cells[1].Text.ToString());
                    offphdr.ActIndate = Convert.ToDateTime(GridViewOffday.Rows[i].Cells[1].Text.ToString());
                    offphdr.ActOutdate = Convert.ToDateTime(GridViewOffday.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysIntime = GridViewOffday.Rows[i].Cells[4].Text.ToString() == "&nbsp;" ? "00:00 AM" : GridViewOffday.Rows[i].Cells[4].Text.ToString();
                    offphdr.SysOuttime = GridViewOffday.Rows[i].Cells[5].Text.ToString() == "&nbsp;" ? "00:00 AM" : GridViewOffday.Rows[i].Cells[5].Text.ToString();
                    offphdr.SysTotalhrs = GridViewOffday.Rows[i].Cells[6].Text.ToString() == "&nbsp;" ? "00:00" : GridViewOffday.Rows[i].Cells[6].Text.ToString();
                    offphdr.NoofLeave = Convert.ToDouble(dpllvno.SelectedItem.Value);
                    offphdr.Leavetype = dpllvtype.SelectedItem.Value.ToString();
                    offphdr.Leavedescription = dpllvtype.SelectedItem.Text.ToString();
                    offphdr.Remarks = txtRemarksOffday.Text.Replace("'", "") == "" ? "NA" : txtRemarksOffday.Text.Replace("'", "");
                    offphdr.Acthrs = "00:00";
                    offphdr.ActingpersonId = ActingPersonId.ToString();
                    lvphdrlst.Add(offphdr);
                }
            }
            LeaveProcess lvproc = new LeaveProcess();
            string retval = lvproc.SaveRejectOffdayProcessData(lvphdrlst, myCommand);
            if (retval.ToString() == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }
            LoadAllInformation();
            ReloadAllgridAfterForwardRejectApproveOffday();
            ForExpandPendingApplications(GridViewOffday, Panel4);
        }
        catch (Exception ex)
        {
            myTrans.Rollback("SaveAllTransaction");
        }
        finally
        {
            myConnection.Close();
        }
    }
    protected void btnApproveOffday_Click(object sender, EventArgs e)
    {
        SqlConnection myConnection = new SqlConnection(ConnectionStr);
        myConnection.Open();
        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;
        SqlDataAdapter sqlDataAdapterObj = null;
        string retValue = "";
        myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        try
        {
            List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
            for (int i = 0; i < GridViewOffday.Rows.Count; i++)
            {
                string ProcessID = Session["processid"].ToString();//"P002";
                string FlowID = Session["processFlowId"].ToString(); //"2";
                int levelid = 0;
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId = 5;  // Approve
                CheckBox chkbox = GridViewOffday.Rows[i].FindControl("Checklv") as CheckBox;
                if (chkbox.Checked == true)
                {
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();
                    TextBox tb = GridViewOffday.Rows[i].FindControl("txtlvRemarks") as TextBox;
                    DropDownList dpllvno = GridViewOffday.Rows[i].FindControl("dpllday") as DropDownList;
                    DropDownList dpllvtype = GridViewOffday.Rows[i].FindControl("lvtype") as DropDownList;
                    offphdr.TransactionNo = Convert.ToString(GridViewOffday.Rows[i].Cells[20].Text.ToString());
                    offphdr.TransactionLineNo = Convert.ToInt32(GridViewOffday.Rows[i].Cells[21].Text.ToString());
                    offphdr.ApplicantId = Session["empcode"].ToString();
                    offphdr.ProcessId = ProcessID.ToString();
                    offphdr.FlowId = FlowID.ToString();
                    offphdr.ProcesslevelId = Convert.ToInt32(GridViewOffday.Rows[i].Cells[19].Text.ToString());
                    offphdr.ProcessnextlevelId = 1;
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.SL = Convert.ToInt32(GridViewOffday.Rows[i].Cells[0].Text.ToString());
                    offphdr.ClaiminDdate = Convert.ToDateTime(GridViewOffday.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysIndate = Convert.ToDateTime(GridViewOffday.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysOutdate = Convert.ToDateTime(GridViewOffday.Rows[i].Cells[1].Text.ToString());
                    offphdr.ActIndate = Convert.ToDateTime(GridViewOffday.Rows[i].Cells[1].Text.ToString());
                    offphdr.ActOutdate = Convert.ToDateTime(GridViewOffday.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysIntime = GridViewOffday.Rows[i].Cells[4].Text.ToString() == "&nbsp;" ? "00:00 AM" : GridViewOffday.Rows[i].Cells[4].Text.ToString();
                    offphdr.SysOuttime = GridViewOffday.Rows[i].Cells[5].Text.ToString() == "&nbsp;" ? "00:00 AM" : GridViewOffday.Rows[i].Cells[5].Text.ToString();
                    offphdr.SysTotalhrs = GridViewOffday.Rows[i].Cells[6].Text.ToString() == "&nbsp;" ? "00:00" : GridViewOffday.Rows[i].Cells[6].Text.ToString();
                    offphdr.ActIntime = GridViewOffday.Rows[i].Cells[7].Text.ToString();
                    offphdr.ActIntime = GridViewOffday.Rows[i].Cells[8].Text.ToString();
                    offphdr.NoofLeave = Convert.ToDouble(0);
                    offphdr.Leavetype = "OF";
                    offphdr.Remarks = txtRemarksOffday.Text.Replace("'", "") == "" ? "NA" : txtRemarksOffday.Text.Replace("'", "");
                    offphdr.Acthrs = "00:00";
                    offphdr.ActingpersonId = ActingPersonId.ToString();
                    lvphdrlst.Add(offphdr);
                }
            }
            LeaveProcess lvproc = new LeaveProcess();
            string retval = lvproc.SaveApproveOffdayProcessData(lvphdrlst, myCommand);
            if (retval.ToString() == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }
            LoadAllInformation();
            ReloadAllgridAfterForwardRejectApproveOffday();
            ForExpandPendingApplications(GridViewOffday, Panel4);
            if (retval.ToString() != "")
            {
                // SendMailforApproval();   /// Send mail
            }
        }
        catch (Exception ex)
        {
            myTrans.Rollback("SaveAllTransaction");
        }
        finally
        {
            myConnection.Close();
        }
    }  
    protected void GridViewLeavePending_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("Submit"))
            {
                GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                int rowIndex = gvr.RowIndex;
                Session["LeaveCode"] = GridViewLeavePending.DataKeys[rowIndex].Values[0].ToString();
                Session["plevelid"] = GridViewLeavePending.DataKeys[rowIndex].Values[1].ToString();
                Session["processid"] = GridViewLeavePending.DataKeys[rowIndex].Values[2].ToString();
                Session["processFlowId"] = GridViewLeavePending.DataKeys[rowIndex].Values[3].ToString();
                Session["referance"] = GridViewLeavePending.Rows[rowIndex].Cells[1].Text.ToString();
                Session["empcode"] = GridViewLeavePending.Rows[rowIndex].Cells[2].Text.ToString();
                DropDownList ddl = (DropDownList)GridViewLeavePending.Rows[rowIndex].Cells[12].FindControl("ddlActionType");
                if (ddl.SelectedItem.Text == "--please select--" || ddl.SelectedItem.Text == "--No Permission--")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please Select Permission Correctly');", true);
                }
                else
                {
                    int actionId = Convert.ToInt32(ddl.SelectedValue);
                    ReloadAllgridAfterForwardRejectApprove();
                    if(grdOvertime.Rows.Count != 0)
                    {
                        SelectCheckbox(grdOvertime);
                    }
                    else if (grdNight.Rows.Count != 0)
                    {
                        SelectCheckbox(grdNight);
                    }
                    else if (GridViewLeave.Rows.Count != 0)
                    {
                        SelectCheckbox(GridViewLeave);
                    }
                    else if (GridViewOffday.Rows.Count != 0)
                    {
                        SelectCheckbox(GridViewOffday);
                    }
                    else if (grdAttendance.Rows.Count != 0)
                    {
                        SelectCheckbox(grdAttendance);
                    }
                    else if (GridViewEarnedLeave.Rows.Count != 0)
                    {
                        SelectCheckbox(GridViewEarnedLeave);
                    }
                    ButtonCall(Session["processid"].ToString(), actionId);
                }
                Panel50.Visible = false;
                Panel1.Visible = false;
                Panel2.Visible = false;
                Panel4.Visible = false;
                Panel6.Visible = false;
                Panel8.Visible = false;
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void grdAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
            CheckBox chkbox = e.Row.FindControl("CheckRet") as CheckBox;
            Label lb = e.Row.FindControl("lblstatus") as Label;
            DateTime dta = Convert.ToDateTime(e.Row.Cells[1].Text);
            lb.Text = e.Row.Cells[15].Text;
            MKB.TimePicker.TimeSelector mkb = (MKB.TimePicker.TimeSelector)e.Row.FindControl("timeoff1");
            MKB.TimePicker.TimeSelector mkb2 = (MKB.TimePicker.TimeSelector)e.Row.FindControl("timeoff2");
            string IntimeValue = e.Row.Cells[22].Text.ToString();
            if (IntimeValue != "&nbsp;")
            {
                DateTime dt = DateTime.Parse(IntimeValue);
                MKB.TimePicker.TimeSelector.AmPmSpec am_pm;
                if (dt.ToString("tt") == "AM")
                {
                    am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                }
                else
                {
                    am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
                }
                mkb.SetTime(dt.Hour, dt.Minute, am_pm);
            }
            string OuttimeValue = e.Row.Cells[23].Text.ToString();
            if (OuttimeValue != "&nbsp;")
            {
                DateTime dt = DateTime.Parse(OuttimeValue);
                MKB.TimePicker.TimeSelector.AmPmSpec am_pm;
                if (dt.ToString("tt") == "AM")
                {
                    am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                }
                else
                {
                    am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
                }
                mkb2.SetTime(dt.Hour, dt.Minute, am_pm);
            }
            if (Convert.ToInt32(e.Row.Cells[16].Text) > 0)
            {
                //chkbox.Enabled = false;
                mkb.Enabled = false;
                mkb2.Enabled = false;
                e.Row.BackColor = System.Drawing.Color.LightGreen;
            }
            if (dta < Convert.ToDateTime(Session["fdate"].ToString()))
            {
                //chkbox.Enabled = false;
                mkb.Enabled = false;
                mkb2.Enabled = false;
            }
            //if (e.Row.Cells[13].Text == "Y")
            //{
            //    chkbox.Enabled = false;
            //    mkb.Enabled = false;
            //    mkb2.Enabled = false;
            //}
        }
        e.Row.Cells[3].Visible = false;
        e.Row.Cells[13].Visible = false;
        e.Row.Cells[14].Visible = false;
        e.Row.Cells[15].Visible = false;
        e.Row.Cells[16].Visible = false;
        e.Row.Cells[17].Visible = false;
        e.Row.Cells[18].Visible = false;
        e.Row.Cells[19].Visible = false;
        e.Row.Cells[20].Visible = false;
        e.Row.Cells[21].Visible = false;
        e.Row.Cells[22].Visible = false;
        e.Row.Cells[23].Visible = false;
    }

    protected void btnForwardAttendance_Click(object sender, EventArgs e)
    {
        SqlConnection myConnection = new SqlConnection(ConnectionStr);
        myConnection.Open();
        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;
        SqlDataAdapter sqlDataAdapterObj = null;
        string retValue = "";
        int levelid = 0;
        myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        try
        {
            List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
            for (int i = 0; i < grdAttendance.Rows.Count; i++)
            {
                string ProcessID = Session["processid"].ToString(); //PID;
                string FlowID = Session["processFlowId"].ToString(); //FID;
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId = 2;  // forward
                CheckBox chkbox = grdAttendance.Rows[i].FindControl("CheckRet") as CheckBox;
                if (chkbox.Checked == true)
                {
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();
                    offphdr.ProcessId = ProcessID.ToString();
                    offphdr.FlowId = FlowID.ToString();
                    offphdr.ActingpersonId = ActingPersonId.ToString();
                    offphdr.Remarks = txtRemarks.Text.Replace("'", "") == "" ? "NA" : txtRemarks.Text.Replace("'", "");
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.TransactionLineNo = Convert.ToInt32(grdAttendance.Rows[i].Cells[20].Text.ToString());
                    offphdr.TransactionNo = Convert.ToString(grdAttendance.Rows[i].Cells[19].Text.ToString());
                    offphdr.ProcesslevelId = Convert.ToInt32(grdAttendance.Rows[i].Cells[16].Text.ToString());
                    lvphdrlst.Add(offphdr);
                }
            }
            LeaveProcess lvproc = new LeaveProcess();
            string retval = lvproc.SaveForwardAttendanceProcessData(lvphdrlst, myCommand);
            if (retval.ToString() == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }

            LoadAllInformation();
            ReloadAllgridAfterForwardRejectApprove();
            ForExpandPendingApplications(grdAttendance, Panel6);
            if (retval.ToString() != "")
            {
                //SendMailforForword(levelid);
            }

            //Response.Redirect(Request.Url.AbsoluteUri);
        }
        catch (Exception ex)
        {
            //myTrans.Rollback("SaveAllTransaction");
        }
        finally
        {
            myConnection.Close();
        }

    }
    protected void btnReturnAttendance_Click(object sender, EventArgs e)
    {
        SqlConnection myConnection = new SqlConnection(ConnectionStr);
        myConnection.Open();
        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;
        SqlDataAdapter sqlDataAdapterObj = null;
        string retValue = "";
        int levelid = 0;
        myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        try
        {
            List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
            for (int i = 0; i < grdAttendance.Rows.Count; i++)
            {
                string ProcessID = Session["processid"].ToString();
                string FlowID = Session["processFlowId"].ToString(); 
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId = 3;  // forward
                CheckBox chkbox = grdAttendance.Rows[i].FindControl("CheckRet") as CheckBox;
                if (chkbox.Checked == true)
                {
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();
                    offphdr.ProcessId = ProcessID.ToString();
                    offphdr.FlowId = FlowID.ToString();
                    offphdr.ActingpersonId = ActingPersonId.ToString();
                    offphdr.Remarks = txtRemarks.Text.Replace("'", "") == "" ? "NA" : txtRemarks.Text.Replace("'", "");
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.TransactionLineNo = Convert.ToInt32(grdAttendance.Rows[i].Cells[20].Text.ToString());
                    offphdr.TransactionNo = Convert.ToString(grdAttendance.Rows[i].Cells[19].Text.ToString());
                    offphdr.ProcesslevelId = Convert.ToInt32(grdAttendance.Rows[i].Cells[16].Text.ToString());
                    lvphdrlst.Add(offphdr);
                }
            }
            LeaveProcess lvproc = new LeaveProcess();
            string retval = lvproc.SaveReturnOvertimeProcessData(lvphdrlst, myCommand);
            if (retval.ToString() == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }
            LoadAllInformation();
            ReloadAllgridAfterForwardRejectApprove();
            ForExpandPendingApplications(grdAttendance, Panel6);
            if (retval.ToString() != "")
            {
                //SendMailforForword(levelid);
            }
        }
        catch (Exception ex)
        {
            //myTrans.Rollback("SaveAllTransaction");
        }
        finally
        {
            myConnection.Close();
        }
    }
    protected void btnRejectAttendance_Click(object sender, EventArgs e)
    {
        SqlConnection myConnection = new SqlConnection(ConnectionStr);
        myConnection.Open();
        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;
        SqlDataAdapter sqlDataAdapterObj = null;
        string retValue = "";
        int levelid = 0;
        myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        try
        {
            List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
            for (int i = 0; i < grdAttendance.Rows.Count; i++)
            {
                string ProcessID = Session["processid"].ToString();
                string FlowID = Session["processFlowId"].ToString();
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId = 4;  // forward
                CheckBox chkbox = grdAttendance.Rows[i].FindControl("CheckRet") as CheckBox;
                if (chkbox.Checked == true)
                {
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();
                    offphdr.ProcessId = ProcessID.ToString();
                    offphdr.FlowId = FlowID.ToString();
                    offphdr.ActingpersonId = ActingPersonId.ToString();
                    offphdr.Remarks = txtRemarks.Text.Replace("'", "") == "" ? "NA" : txtRemarks.Text.Replace("'", "");
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.TransactionLineNo = Convert.ToInt32(grdAttendance.Rows[i].Cells[20].Text.ToString());
                    offphdr.TransactionNo = Convert.ToString(grdAttendance.Rows[i].Cells[19].Text.ToString());
                    offphdr.ProcesslevelId = Convert.ToInt32(grdAttendance.Rows[i].Cells[16].Text.ToString());
                    lvphdrlst.Add(offphdr);
                }
            }
            LeaveProcess lvproc = new LeaveProcess();
            string retval = lvproc.SaveRejectAttendanceProcessData(lvphdrlst, myCommand);
            if (retval.ToString() == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }
            LoadAllInformation();
            ReloadAllgridAfterForwardRejectApprove();
            ForExpandPendingApplications(grdAttendance, Panel6);
            if (retval.ToString() != "")
            {
                //SendMailforForword(levelid);
            }
            // Response.Redirect(Request.Url.AbsoluteUri);
        }
        catch (Exception ex)
        {
            //myTrans.Rollback("SaveAllTransaction");
        }
        finally
        {
            myConnection.Close();
        }
    }
    protected void btnApproveAttendance_Click(object sender, EventArgs e)
    {
        SqlConnection myConnection = new SqlConnection(ConnectionStr);
        myConnection.Open();
        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;
        SqlDataAdapter sqlDataAdapterObj = null;
        string retValue = "";
        int levelid = 0;
        myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        try
        {
            List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
            for (int i = 0; i < grdAttendance.Rows.Count; i++)
            {
                string ProcessID = Session["processid"].ToString();
                string FlowID = Session["processFlowId"].ToString();
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId = 5;  // forward
                CheckBox chkbox = grdAttendance.Rows[i].FindControl("CheckRet") as CheckBox;
                if (chkbox.Checked == true)
                {
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();
                    MKB.TimePicker.TimeSelector mkb = (MKB.TimePicker.TimeSelector)grdAttendance.Rows[i].FindControl("timeoff1");
                    MKB.TimePicker.TimeSelector mkb2 = (MKB.TimePicker.TimeSelector)grdAttendance.Rows[i].FindControl("timeoff2");
                    offphdr.ApplicantId = Session["empcode"].ToString();
                    //offphdr.ProcessnextlevelId = 1;
                    //offphdr.ActiontypeId = 1;
                    offphdr.SL = Convert.ToInt32(grdAttendance.Rows[i].Cells[0].Text.ToString());
                    offphdr.ClaiminDdate = Convert.ToDateTime(grdAttendance.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysIndate = Convert.ToDateTime(grdAttendance.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysOutdate = Convert.ToDateTime(grdAttendance.Rows[i].Cells[1].Text.ToString());
                    offphdr.ActIndate = Convert.ToDateTime(grdAttendance.Rows[i].Cells[1].Text.ToString());
                    offphdr.ActOutdate = Convert.ToDateTime(((TextBox)(grdAttendance.Rows[i].Cells[8].FindControl("txtntDate1"))).Text == "" ? "" : ((TextBox)(grdAttendance.Rows[i].Cells[8].FindControl("txtntDate1"))).Text);
                    offphdr.SysIntime = grdAttendance.Rows[i].Cells[4].Text.ToString() == "&nbsp;" ? "00:00 AM" : grdAttendance.Rows[i].Cells[4].Text.ToString();
                    offphdr.SysOuttime = grdAttendance.Rows[i].Cells[5].Text.ToString() == "&nbsp;" ? "00:00 AM" : grdAttendance.Rows[i].Cells[5].Text.ToString();
                    offphdr.SysTotalhrs = grdAttendance.Rows[i].Cells[6].Text.ToString() == "&nbsp;" ? "00:00" : grdAttendance.Rows[i].Cells[6].Text.ToString();
                    offphdr.ActIntime = timeformat(mkb.Date.Hour.ToString() + ":" + mkb.Date.Minute.ToString() + ":" + mkb.AmPm.ToString());
                    offphdr.ActOuttime = timeformat(mkb2.Date.Hour.ToString() + ":" + mkb2.Date.Minute.ToString() + ":" + mkb2.AmPm.ToString());
                    //offphdr.EntryUserid = Session["EntryUserid"].ToString();
                    offphdr.NoofLeave = Convert.ToDouble(grdAttendance.Rows[i].Cells[21].Text.ToString()); ;
                    offphdr.Leavetype = "AT";
                    offphdr.ProcessId = ProcessID.ToString();
                    offphdr.FlowId = FlowID.ToString();
                    offphdr.ActingpersonId = ActingPersonId.ToString();
                    offphdr.Remarks = txtRemarks.Text.Replace("'", "") == "" ? "NA" : txtRemarks.Text.Replace("'", "");
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.TransactionLineNo = Convert.ToInt32(grdAttendance.Rows[i].Cells[20].Text.ToString());
                    offphdr.TransactionNo = Convert.ToString(grdAttendance.Rows[i].Cells[19].Text.ToString());
                    offphdr.ProcesslevelId = Convert.ToInt32(grdAttendance.Rows[i].Cells[16].Text.ToString());
                    lvphdrlst.Add(offphdr);
                }
            }
            LeaveProcess lvproc = new LeaveProcess();
            string retval = lvproc.SaveApproveAttendanceProcessData(lvphdrlst, myCommand);
            if (retval.ToString() == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }
            LoadAllInformation();
            ReloadAllgridAfterForwardRejectApprove();
            ForExpandPendingApplications(grdAttendance, Panel6);
            if (retval.ToString() != "")
            {
                //SendMailforForword(levelid);
            }
            // Response.Redirect(Request.Url.AbsoluteUri);
        }
        catch (Exception ex)
        {
            //myTrans.Rollback("SaveAllTransaction");
        }
        finally
        {
            myConnection.Close();
        }
    }
    private void SelectCheckbox(GridView gdv)
    {
        for (int i = 0; i < gdv.Rows.Count; i++)
        {
            CheckBox chkbox = gdv.Rows[i].FindControl("Checklv") as CheckBox;
            chkbox.Checked = true;
        }

    }
    protected void btnSearchIntogrd_Click(object sender, EventArgs e)
    {
        //SearchIntoPendinggrd("GridViewLeavePending", "txtForSearchIntogrd");
    }
    //[System.Web.Services.WebMethod]
    public void SearchIntoPendinggrd()
    {
        string searchValue = txtForSearchIntogrd.Text.Trim();
        try
        {
            foreach (GridViewRow row in GridViewLeavePending.Rows)
            {
                if (searchValue == "")
                {
                    row.Visible = true;
                }
                else
                {
                    if (row.Cells[2].Text.ToString().Equals(searchValue) || row.Cells[6].Text.ToString().Equals(searchValue))//(row.Cells[2].Text.ToString().Equals(searchValue))
                    {
                        row.Visible = true;
                    }
                    else
                        row.Visible = false;
                }
            }

        }
        catch (Exception)
        {

            throw;
        }
    }
    private void ForExpandPendingApplications( GridView nAmeofgrd, Panel nAmeofpanel)
    {
        if (nAmeofgrd.Rows.Count < 1)
        {
            CollapsiblePanelExtenderSrch.Collapsed = false;
            CollapsiblePanelExtenderSrch.ClientState = "false";
            nAmeofpanel.Visible = false;
        }
        else
        {
            nAmeofpanel.Visible = true;
        }
    }
    //protected void txtForSearchIntogrd_TextChanged(object sender, EventArgs e)
    //{
    //    SearchIntoPendinggrd();

    //}

    //private void MyTextBox_OnKeyPress(string ctrlName, string args)
    //{
    //    SearchIntoPendinggrd();
    //    //your code goes here
    //}

    protected void GridViewEarnedLeave_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
            CheckBox chkbox = e.Row.FindControl("Checklv") as CheckBox;
            TextBox tb = e.Row.FindControl("txtlvRemarks") as TextBox;
            Label lb = e.Row.FindControl("txtlvstatus") as Label;
            tb.Width = 200;

            // if (e.Row.Cells[16].Text == "Y")
            // {
            //     chkbox.Enabled = false;
            //     tb.Enabled = false;
            //     lb.Enabled = false;               
            // }   

        }

        e.Row.Cells[1].Visible = false;
        e.Row.Cells[14].Visible = false;
        e.Row.Cells[15].Visible = false;
        e.Row.Cells[19].Visible = false;
        e.Row.Cells[20].Visible = false;
        e.Row.Cells[21].Visible = false;
        e.Row.Cells[22].Visible = false;
        e.Row.Cells[23].Visible = false;
        e.Row.Cells[24].Visible = false;
        e.Row.Cells[25].Visible = false;
        e.Row.Cells[26].Visible = false;
        e.Row.Cells[27].Visible = false;
        e.Row.Cells[28].Visible = false;
    }
    protected void btnForwardEarnedLeave_Click(object sender, EventArgs e)
    {
        SqlConnection myConnection = new SqlConnection(ConnectionStr);
        myConnection.Open();
        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;
        SqlDataAdapter sqlDataAdapterObj = null;
        string retValue = "";
        int levelid = 0;
        myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        try
        {

            List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
            for (int i = 0; i < GridViewEarnedLeave.Rows.Count; i++)
            {
                string ProcessID = Session["processid"].ToString(); //PID;
                string FlowID = Session["processFlowId"].ToString(); //FID;
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId = 2;  // forward
                CheckBox chkbox = GridViewEarnedLeave.Rows[i].FindControl("Checklv") as CheckBox;
                if (chkbox.Checked == true)
                {
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();
                    TextBox tb = GridViewEarnedLeave.Rows[i].FindControl("txtlvRemarks") as TextBox;
                    DropDownList dpllvno = GridViewEarnedLeave.Rows[i].FindControl("dpllday") as DropDownList;
                    DropDownList dpllvtype = GridViewEarnedLeave.Rows[i].FindControl("lvtype") as DropDownList;

                    offphdr.TransactionNo = Convert.ToString(GridViewEarnedLeave.Rows[i].Cells[27].Text.ToString());
                    offphdr.TransactionLineNo = Convert.ToInt32(GridViewEarnedLeave.Rows[i].Cells[28].Text.ToString());
                    offphdr.ApplicantId = Session["empcode"].ToString();
                    offphdr.ProcessId = ProcessID.ToString();
                    offphdr.FlowId = FlowID.ToString();
                    offphdr.ProcesslevelId = Convert.ToInt32(GridViewEarnedLeave.Rows[i].Cells[25].Text.ToString());
                    levelid = offphdr.ProcesslevelId + 1;  // To find next level id
                    offphdr.ProcessnextlevelId = 1;
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.SL = Convert.ToInt32(GridViewEarnedLeave.Rows[i].Cells[0].Text.ToString());
                    offphdr.ClaiminDdate = Convert.ToDateTime(GridViewEarnedLeave.Rows[i].Cells[3].Text.ToString());
                    offphdr.SysIndate = Convert.ToDateTime(GridViewEarnedLeave.Rows[i].Cells[2].Text.ToString());
                    offphdr.SysOutdate = Convert.ToDateTime(GridViewEarnedLeave.Rows[i].Cells[3].Text.ToString());
                    offphdr.ActIndate = Convert.ToDateTime(GridViewEarnedLeave.Rows[i].Cells[2].Text.ToString());
                    offphdr.ActOutdate = Convert.ToDateTime(GridViewEarnedLeave.Rows[i].Cells[3].Text.ToString());
                    offphdr.SysIntime = "";
                    offphdr.SysOuttime = "";
                    offphdr.SysTotalhrs = "";
                    offphdr.Acthrs = "00:00";
                    offphdr.ActingpersonId = ActingPersonId.ToString();
                    offphdr.Remarks = txtRemarksEarnedLeave.Text.Replace("'", "") == "" ? "NA" : txtRemarksEarnedLeave.Text.Replace("'", "");
                    lvphdrlst.Add(offphdr);
                }
            }
            LeaveProcess lvproc = new LeaveProcess();
            string retval = lvproc.SaveForwardEarnedLeaveProcessData(lvphdrlst, myCommand);
            if (retval.ToString() == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }

            LoadAllInformation();
            ReloadAllgridAfterForwardRejectApprove();
            ForExpandPendingApplications(GridViewEarnedLeave, Panel8);
            if (retval.ToString() != "")
            {
                //SendMailforForword(levelid);
            }

            // Response.Redirect(Request.Url.AbsoluteUri);

        }
        catch (Exception ex)
        {
            myTrans.Rollback("SaveAllTransaction");
        }
        finally
        {
            myConnection.Close();
        }
    }
    protected void btnReturnEarnedLeave_Click(object sender, EventArgs e)
    {
        SqlConnection myConnection = new SqlConnection(ConnectionStr);
        myConnection.Open();
        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;
        SqlDataAdapter sqlDataAdapterObj = null;
        string retValue = "";
        myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        try
        {
            List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
            for (int i = 0; i < GridViewEarnedLeave.Rows.Count; i++)
            {
                string ProcessID = Session["processid"].ToString(); //PID;
                string FlowID = Session["processFlowId"].ToString(); //FID;
                int levelid = 0;
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId = 3;  // Return
                CheckBox chkbox = GridViewEarnedLeave.Rows[i].FindControl("Checklv") as CheckBox;
                if (chkbox.Checked == true)
                {
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();
                    TextBox tb = GridViewEarnedLeave.Rows[i].FindControl("txtlvRemarks") as TextBox;
                    DropDownList dpllvno = GridViewEarnedLeave.Rows[i].FindControl("dpllday") as DropDownList;
                    DropDownList dpllvtype = GridViewEarnedLeave.Rows[i].FindControl("lvtype") as DropDownList;
                    offphdr.TransactionNo = Convert.ToString(GridViewEarnedLeave.Rows[i].Cells[20].Text.ToString());
                    offphdr.TransactionLineNo = Convert.ToInt32(GridViewEarnedLeave.Rows[i].Cells[21].Text.ToString());
                    offphdr.ApplicantId = Session["empcode"].ToString();
                    offphdr.ProcessId = ProcessID.ToString();
                    offphdr.FlowId = FlowID.ToString();
                    offphdr.ProcesslevelId = Convert.ToInt32(GridViewEarnedLeave.Rows[i].Cells[19].Text.ToString());
                    offphdr.ProcessnextlevelId = 1;
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.SL = Convert.ToInt32(GridViewEarnedLeave.Rows[i].Cells[0].Text.ToString());
                    offphdr.ClaiminDdate = Convert.ToDateTime(GridViewEarnedLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysIndate = Convert.ToDateTime(GridViewEarnedLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysOutdate = Convert.ToDateTime(GridViewEarnedLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.ActIndate = Convert.ToDateTime(GridViewEarnedLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.ActOutdate = Convert.ToDateTime(GridViewEarnedLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysIntime = GridViewEarnedLeave.Rows[i].Cells[4].Text.ToString() == "&nbsp;" ? "00:00 AM" : GridViewEarnedLeave.Rows[i].Cells[4].Text.ToString();
                    offphdr.SysOuttime = GridViewEarnedLeave.Rows[i].Cells[5].Text.ToString() == "&nbsp;" ? "00:00 AM" : GridViewEarnedLeave.Rows[i].Cells[5].Text.ToString();
                    offphdr.SysTotalhrs = GridViewEarnedLeave.Rows[i].Cells[6].Text.ToString() == "&nbsp;" ? "00:00" : GridViewEarnedLeave.Rows[i].Cells[6].Text.ToString();
                    offphdr.NoofLeave = Convert.ToDouble(dpllvno.SelectedItem.Value);
                    offphdr.Leavetype = dpllvtype.SelectedItem.Value.ToString();
                    offphdr.Leavedescription = dpllvtype.SelectedItem.Text.ToString();
                    offphdr.Remarks = tb.Text.ToString();
                    offphdr.Acthrs = "00:00";
                    offphdr.ActingpersonId = ActingPersonId.ToString();
                    lvphdrlst.Add(offphdr);
                }
            }
            LeaveProcess lvproc = new LeaveProcess();
            string retval = lvproc.SaveReturnOffdayProcessData(lvphdrlst, myCommand);
            if (retval.ToString() == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }
            LoadAllInformation();
            ReloadAllgridAfterForwardRejectApprove();
            ForExpandPendingApplications(GridViewEarnedLeave, Panel8);
        }
        catch (Exception ex)
        {
            myTrans.Rollback("SaveAllTransaction");
        }
        finally
        {
            myConnection.Close();
        }
    }
    protected void btnRejectEarnedLeave_Click(object sender, EventArgs e)
    {
        SqlConnection myConnection = new SqlConnection(ConnectionStr);
        myConnection.Open();
        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;
        SqlDataAdapter sqlDataAdapterObj = null;
        string retValue = "";
        myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        try
        {
            List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
            for (int i = 0; i < GridViewEarnedLeave.Rows.Count; i++)
            {
                string ProcessID = Session["processid"].ToString(); //PID;
                string FlowID = Session["processFlowId"].ToString(); //FID;
                int levelid = 0;
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId = 4;          // 97 for reject
                CheckBox chkbox = GridViewEarnedLeave.Rows[i].FindControl("Checklv") as CheckBox;
                if (chkbox.Checked == true)
                {
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();
                    TextBox tb = GridViewEarnedLeave.Rows[i].FindControl("txtlvRemarks") as TextBox;
                    DropDownList dpllvno = GridViewEarnedLeave.Rows[i].FindControl("dpllday") as DropDownList;
                    DropDownList dpllvtype = GridViewEarnedLeave.Rows[i].FindControl("lvtype") as DropDownList;
                    offphdr.TransactionNo = Convert.ToString(GridViewEarnedLeave.Rows[i].Cells[20].Text.ToString());
                    offphdr.TransactionLineNo = Convert.ToInt32(GridViewEarnedLeave.Rows[i].Cells[21].Text.ToString());
                    offphdr.ApplicantId = Session["empcode"].ToString();
                    offphdr.ProcessId = ProcessID.ToString();
                    offphdr.FlowId = FlowID.ToString();
                    offphdr.ProcesslevelId = Convert.ToInt32(GridViewEarnedLeave.Rows[i].Cells[19].Text.ToString());
                    offphdr.ProcessnextlevelId = 1;
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.SL = Convert.ToInt32(GridViewEarnedLeave.Rows[i].Cells[0].Text.ToString());
                    offphdr.ClaiminDdate = Convert.ToDateTime(GridViewEarnedLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysIndate = Convert.ToDateTime(GridViewEarnedLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysOutdate = Convert.ToDateTime(GridViewEarnedLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.ActIndate = Convert.ToDateTime(GridViewEarnedLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.ActOutdate = Convert.ToDateTime(GridViewEarnedLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysIntime = GridViewEarnedLeave.Rows[i].Cells[4].Text.ToString() == "&nbsp;" ? "00:00 AM" : GridViewEarnedLeave.Rows[i].Cells[4].Text.ToString();
                    offphdr.SysOuttime = GridViewEarnedLeave.Rows[i].Cells[5].Text.ToString() == "&nbsp;" ? "00:00 AM" : GridViewEarnedLeave.Rows[i].Cells[5].Text.ToString();
                    offphdr.SysTotalhrs = GridViewEarnedLeave.Rows[i].Cells[6].Text.ToString() == "&nbsp;" ? "00:00" : GridViewEarnedLeave.Rows[i].Cells[6].Text.ToString();
                    offphdr.NoofLeave = Convert.ToDouble(dpllvno.SelectedItem.Value);
                    offphdr.Leavetype = dpllvtype.SelectedItem.Value.ToString();
                    offphdr.Leavedescription = dpllvtype.SelectedItem.Text.ToString();
                    offphdr.Remarks = txtRemarksEarnedLeave.Text.Replace("'", "") == "" ? "NA" : txtRemarksEarnedLeave.Text.Replace("'", "");
                    offphdr.Acthrs = "00:00";
                    offphdr.ActingpersonId = ActingPersonId.ToString();
                    lvphdrlst.Add(offphdr);
                }
            }
            LeaveProcess lvproc = new LeaveProcess();
            string retval = lvproc.SaveRejectOffdayProcessData(lvphdrlst, myCommand);
            if (retval.ToString() == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }
            LoadAllInformation();
            ReloadAllgridAfterForwardRejectApprove();
            ForExpandPendingApplications(GridViewEarnedLeave, Panel8);
        }
        catch (Exception ex)
        {
            myTrans.Rollback("SaveAllTransaction");
        }
        finally
        {
            myConnection.Close();
        }
    }
    protected void btnApproveEarnedLeave_Click(object sender, EventArgs e)
    {
        SqlConnection myConnection = new SqlConnection(ConnectionStr);
        myConnection.Open();
        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;
        SqlDataAdapter sqlDataAdapterObj = null;
        string retValue = "";
        myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        try
        {
            List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
            for (int i = 0; i < GridViewEarnedLeave.Rows.Count; i++)
            {
                string ProcessID = Session["processid"].ToString(); //PID;
                string FlowID = Session["processFlowId"].ToString(); //FID;
                int levelid = 0;
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId = 5;  // Approve
                CheckBox chkbox = GridViewEarnedLeave.Rows[i].FindControl("Checklv") as CheckBox;
                if (chkbox.Checked == true)
                {
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();
                    TextBox tb = GridViewEarnedLeave.Rows[i].FindControl("txtlvRemarks") as TextBox;
                    DropDownList dpllvno = GridViewEarnedLeave.Rows[i].FindControl("dpllday") as DropDownList;
                    DropDownList dpllvtype = GridViewEarnedLeave.Rows[i].FindControl("lvtype") as DropDownList;
                    offphdr.TransactionNo = Convert.ToString(GridViewEarnedLeave.Rows[i].Cells[27].Text.ToString());
                    offphdr.TransactionLineNo = Convert.ToInt32(GridViewEarnedLeave.Rows[i].Cells[28].Text.ToString());
                    offphdr.ApplicantId = Session["empcode"].ToString();
                    offphdr.ProcessId = ProcessID.ToString();
                    offphdr.FlowId = FlowID.ToString();
                    offphdr.ProcesslevelId = Convert.ToInt32(GridViewEarnedLeave.Rows[i].Cells[25].Text.ToString());
                    offphdr.ProcessnextlevelId = 1;
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.SL = Convert.ToInt32(GridViewEarnedLeave.Rows[i].Cells[0].Text.ToString());
                    offphdr.ClaiminDdate = Convert.ToDateTime(GridViewEarnedLeave.Rows[i].Cells[3].Text.ToString());
                    offphdr.SysIndate = Convert.ToDateTime(GridViewEarnedLeave.Rows[i].Cells[2].Text.ToString());
                    offphdr.SysOutdate = Convert.ToDateTime(GridViewEarnedLeave.Rows[i].Cells[3].Text.ToString());
                    offphdr.ActIndate = Convert.ToDateTime(GridViewEarnedLeave.Rows[i].Cells[2].Text.ToString());
                    offphdr.ActOutdate = Convert.ToDateTime(GridViewEarnedLeave.Rows[i].Cells[3].Text.ToString());
                    offphdr.SysIntime = "";
                    offphdr.SysOuttime = "";
                    offphdr.SysTotalhrs = "";
                    offphdr.ActIntime = "";
                    offphdr.NoofLeave = Convert.ToDouble(0);
                    offphdr.Leavetype = "EL";
                    offphdr.Remarks = txtRemarksEarnedLeave.Text.Replace("'", "") == "" ? "NA" : txtRemarksEarnedLeave.Text.Replace("'", "");
                    offphdr.Acthrs = "00:00";
                    offphdr.ActingpersonId = ActingPersonId.ToString();
                    lvphdrlst.Add(offphdr);
                }
            }
            LeaveProcess lvproc = new LeaveProcess();
            string retval = lvproc.SaveApproveEarnedLeaveProcessData(lvphdrlst, myCommand);
            if (retval.ToString() == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }
            LoadAllInformation();
            ReloadAllgridAfterForwardRejectApprove();
            ForExpandPendingApplications(GridViewEarnedLeave, Panel8);
            if (retval.ToString() != "")
            {
                // SendMailforApproval();   /// Send mail
            }

        }
        catch (Exception ex)
        {
            myTrans.Rollback("SaveAllTransaction");
        }
        finally
        {
            myConnection.Close();
        }
    }
}
