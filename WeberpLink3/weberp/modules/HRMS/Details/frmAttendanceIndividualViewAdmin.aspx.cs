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
using LibraryDAL.dsInventory.dsInventoryMasterTableAdapters;
using LibraryDAL.dsInventory.dsInventoryTransactionTableAdapters;
using ADODB;
using CrystalDecisions;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;

public partial class Modules_HRMS_SelfService_frmAttendanceIndividualViewAdmin : System.Web.UI.Page
{
    readonly string _connectionStr = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"];
    private const string PID = "P007";
    private const string FID = "7";
    double _total = 0;
    double _totalLess = 0;
    double _totalWorkingMinutes = 0;

    protected void Page_Load(object sender, EventArgs e)
    {       
        clsStatic.CheckUserAuthentication(true);

        if (!Page.IsPostBack)
        {
            DateTime fDate, lDate;
            fDate = Convert.ToDateTime(ReturnDecissinon().Split('@')[0]);
            lDate = Convert.ToDateTime(ReturnDecissinon().Split('@')[1]);

            //Session["ApplicantID"] = "300084";
            //Session["EntryUserid"] = "300084";  //login user id  

            Session["ApplicantID"] = Session[StaticData.sessionUserId].ToString();
            Session["EntryUserid"] = Session[StaticData.sessionUserId].ToString();

            Session["fdate"] = fDate;
            Session["lDate"] = lDate;

            LoadOvertimeDetailsemployeeID(Session["ApplicantID"].ToString(), PID, FID, fDate, lDate);
            LoadEmployeeInformation(Session["ApplicantID"].ToString());
            LoadResponsiblePersonID(Session["ApplicantID"].ToString());
            LoadLeavBALeByemployeeID(Session["ApplicantID"].ToString(), fDate);
            LoadEmpForAttendanceView(Session["EntryUserid"].ToString());
            lblPeriod.Text = FinanialPeriod(fDate);
            lblcurrentPeriod.Text = Convert.ToString(System.DateTime.Now.Date.Day) + "-" + Convert.ToString(System.DateTime.Now.Date.Month) + "-" + Convert.ToString(System.DateTime.Now.Date.Year);
            txtEmployeeCode_AutoCompleteExtender.ContextKey = _connectionStr;
        }

    }

    private void LoadEmpForAttendanceView(string employeeCode)
    {
        string storedProcedureCommandTest = "exec [spProcessLoadEmpForAttendanceView] '" + employeeCode + "'";
        ClsDropDownListController.LoadDropDownListUsingStoredProcedureWithConcatenation(_connectionStr, storedProcedureCommandTest, ddlEmployeeId, "EmpName", "EmpID");
        ddlEmployeeId.Items.RemoveAt(0);
        ddlEmployeeId.Items.Insert(0, new ListItem(lblName.Text, employeeCode));

        if (ddlEmployeeId.Items.Count > 1)
        {
            Label6.Visible = true;
        }

    }

    private string ReturnDecissinon()
    {
        string ret = "";
        DataTable dttask = new DataTable();
        DateTime serverdate = DateProcess.GetServerDate(_connectionStr);
        DateTime fDate = DateProcess.FirstDateOfMonth(serverdate);
        DateTime lDate = DateProcess.LastDateOfMonth(serverdate);

        dttask = DataProcess.GetData(_connectionStr, "select * from ProcessTaskDateAllemoloyee where TaskType='ATI' and [Status]='N'");
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
                //btnShow.Visible = false;
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
        ret = fDate + "@" + lDate;
        return ret;
    }

    private void LoadLeavBALeByemployeeID(String empid, DateTime fDate)
    {
        DataTable dt = new DataTable();
        LeaveProcess lvproc = new LeaveProcess();
        lvproc.GetEmployeeLeaveBalance(_connectionStr, fDate, empid);
        dt = DataProcess.GetData(_connectionStr, "select LeaveType as [Code],b.Leave_Mas_Name,AllocatedLeave,Enjoied as [Enjoyed],LeaveBal"
                                                + " from [HRMS_EMPLEAVEBAL] a"
                                                + " inner join HRMS_Leave_Mas b on a.LeaveType=b.Leave_Mas_Code"
                                                + " inner join Emp_Details c on c.EmpID=a.Empid and c.EmpType=b.T_C2"
                                                + " where a.Empid='" + empid + "' ");
        gdvLeaveInfo.DataSource = dt;
        gdvLeaveInfo.DataBind();
    }

    protected string ReturnDecissinon1()
    {
        string ret = "";
        DataTable dttask = new DataTable();
        DateTime serverdate = DateProcess.GetServerDate(_connectionStr);
        DateTime fDate = DateProcess.FirstDateOfMonth(serverdate);
        DateTime lDate = DateProcess.LastDateOfMonth(serverdate);
        dttask = DataProcess.GetData(_connectionStr, "select * from ProcessTaskDateAllemoloyee where TaskType='OT' and [Status]='Y'");
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
                //btnShow.Visible = false;
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
        ret = fDate + "@" + lDate;
        return ret;
    }

    private string FinanialPeriod(DateTime fdate)
    {
        string fdate1, fdate2, period;
        if (fdate >= Convert.ToDateTime("01/07/" + Convert.ToString(fdate.Year)))
        {
            fdate1 = Convert.ToString(fdate.Year);
            fdate2 = Convert.ToString(fdate.Year + 1);
        }
        else
        {
            fdate1 = Convert.ToString(fdate.Year - 1);
            fdate2 = Convert.ToString(fdate.Year);
        }
        period = fdate1 + "-" + fdate2;
        return period;
    }
    private void LoadEmployeeInformation(string empid)
    {
        DataTable dt = new DataTable();
        dt = DataProcess.GetData(_connectionStr, "select EmpID,EmpName,Dept,Designation,b.Emp_Mas_Join_Date as Joiningdate from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id where EmpID='" + empid + "'");
        if (dt.Rows.Count > 0)
        {
            lblId.Text = dt.Rows[0]["EmpID"].ToString();
            lblName.Text = dt.Rows[0]["EmpName"].ToString();
            lbldept.Text = dt.Rows[0]["Dept"].ToString();
            lblDesignation.Text = dt.Rows[0]["Designation"].ToString();
            lblJoiningDate.Text = dt.Rows[0]["Joiningdate"].ToString().Substring(0, 10);
        }
        string ffff = "L3T388";
        Image1.ImageUrl = "~/ClientSide/modules/mis/naz/FORMS/HRMS/forms/hndImage.ashx?id=" + ffff;
    }
    private void LoadResponsiblePersonID(string empid)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(_connectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetResponsiblePersonByEmpid";
        cmd.Parameters.Add(new SqlParameter("@empid", SqlDbType.NVarChar)).Value = empid;
        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        dplResponsible.Items.Add("");
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                ListItem lst = new ListItem();
                lst.Value = dr["EmpID"].ToString();
                lst.Text = dr["EmpName"].ToString();
                dplResponsible.Items.Add(lst);
            }
        }
    }

    private void LoadOvertimeDetailsemployeeID(string empid, string Processid, string flowid, DateTime fDate, DateTime lDate)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(_connectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetAttendanceStatusIndividualViewNew";

        cmd.Parameters.Add(new SqlParameter("@fDate", SqlDbType.DateTime)).Value = fDate;
        cmd.Parameters.Add(new SqlParameter("@lDate ", SqlDbType.DateTime)).Value = lDate;
        cmd.Parameters.Add(new SqlParameter("@empid", SqlDbType.NVarChar)).Value = empid;
        cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid;
        cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid;

        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        grdHoliday.DataSource = dt;
        grdHoliday.DataBind();
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
    protected void btnViewall_Click(object sender, EventArgs e)
    {
        string sql = "select left(b.itm_det_date,12) as Purchase_date,a.ItemCode,left(a.DepreciationDate,12) as [Dep. Date],1 as Qty,convert(decimal,a.ItemInitialValue) as [Unit Price],convert(decimal,a.ThisPrdOpeningValue) as [Balance],"
                   + " convert(decimal,a.OpeningDepreciationAmt) as Opening,convert(decimal,a.Addition) as Addition,convert(decimal,a.TotalDepreciationAmt) as Accumulated,convert(decimal,WrittenDownValue) as [Written Down Value]"
                   + " from FAS_Item_Depreciation a inner join FAS_InMa_Itm_Serial b on a.TrackingInfo=b.TrackingInfo where a.ItemCurrentLine='Y'";

        DataTable dt = GetDatafromTable(_connectionStr, sql);
        //GridView1.DataSource = dt;
        //GridView1.DataBind();
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
        int h = Convert.ToInt32(atf.Split(':')[0]);
        int m = Convert.ToInt32(atf.Split(':')[1]);

        if (h > 12)
        {
            h = h - 12;
        }

        string hh = string.Format("{0:00}", h);
        string mm = string.Format("{0:00}", m);
        string ampm = atf.Split(':')[2];

        rtf = hh + ":" + mm + " " + ampm;
        return rtf;
    }

    protected void btnApply_Click(object sender, EventArgs e)
    {
        if (Process.CheckProcessConfigurationStatus(_connectionStr, PID, Session["ApplicantID"].ToString()) == false)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "MessageBox", "alert(' Attendance process is not configured yet.Please contact human resource department');", true);
            return;
        }
        SqlConnection myConnection = new SqlConnection(_connectionStr);
        myConnection.Open();

        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;


        myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        try
        {
            List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
            LeaveProcess lvp = new LeaveProcess();
            string Transactionno = lvp.GetTransactionNo(_connectionStr);
            int movementno = Convert.ToInt32(lvp.GetMovementNo(_connectionStr));
            for (int i = 0; i < grdHoliday.Rows.Count; i++)
            {
                string ProcessID = PID;
                string FlowID = FID;
                string ApplicantID = Session["ApplicantID"].ToString();
                CheckBox chkbox = grdHoliday.Rows[i].FindControl("CheckRet") as CheckBox;
                if (chkbox.Checked == true)
                {
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();

                    MKB.TimePicker.TimeSelector mkb = (MKB.TimePicker.TimeSelector)grdHoliday.Rows[i].FindControl("timeoff1");
                    MKB.TimePicker.TimeSelector mkb2 = (MKB.TimePicker.TimeSelector)grdHoliday.Rows[i].FindControl("timeoff2");

                    offphdr.TransactionNo = Transactionno;
                    offphdr.ApplicantId = ApplicantID;
                    offphdr.ProcessId = ProcessID;
                    offphdr.FlowId = FlowID;
                    offphdr.ProcesslevelId = 1;// dplResponsible.SelectedItem.Value==""?1:0;
                    offphdr.ProcessnextlevelId = 1;
                    offphdr.ActiontypeId = 1;
                    offphdr.SL = Convert.ToInt32(grdHoliday.Rows[i].Cells[0].Text);
                    offphdr.ClaiminDdate = Convert.ToDateTime(grdHoliday.Rows[i].Cells[1].Text);
                    offphdr.SysIndate = Convert.ToDateTime(grdHoliday.Rows[i].Cells[1].Text);
                    offphdr.SysOutdate = Convert.ToDateTime(grdHoliday.Rows[i].Cells[1].Text);
                    offphdr.ActIndate = Convert.ToDateTime(grdHoliday.Rows[i].Cells[1].Text);
                    offphdr.ActOutdate = Convert.ToDateTime(((TextBox)(grdHoliday.Rows[i].Cells[8].FindControl("txtntDate1"))).Text == "" ? "" : ((TextBox)(grdHoliday.Rows[i].Cells[8].FindControl("txtntDate1"))).Text);

                    offphdr.SysIntime = grdHoliday.Rows[i].Cells[4].Text == "&nbsp;" ? "00:00 AM" : grdHoliday.Rows[i].Cells[4].Text;
                    offphdr.SysOuttime = grdHoliday.Rows[i].Cells[5].Text == "&nbsp;" ? "00:00 AM" : grdHoliday.Rows[i].Cells[5].Text;
                    offphdr.SysTotalhrs = grdHoliday.Rows[i].Cells[6].Text == "&nbsp;" ? "00:00" : grdHoliday.Rows[i].Cells[6].Text;

                    offphdr.ActIntime = timeformat(mkb.Date.Hour + ":" + mkb.Date.Minute + ":" + mkb.AmPm);
                    offphdr.ActOuttime = timeformat(mkb2.Date.Hour + ":" + mkb2.Date.Minute + ":" + mkb2.AmPm);

                    offphdr.Remarks = txtRemarks.Text;
                    offphdr.Acthrs = "00:00";
                    offphdr.Remarks = txtRemarks.Text.Replace("'", "");
                    offphdr.ResponsiblepersonId = ApplicantID;//dplResponsible.SelectedItem.Value;
                    offphdr.MovementNo = movementno;
                    offphdr.EntryUserid = Session["EntryUserid"].ToString();
                    lvphdrlst.Add(offphdr);

                    DateTime InDateTime = Convert.ToDateTime(offphdr.SysIndate.ToShortDateString() + " " + offphdr.ActIntime);
                    DateTime OutDateTime = Convert.ToDateTime(offphdr.ActOutdate.ToShortDateString() + " " + offphdr.ActOuttime);
                    if (InDateTime > OutDateTime) return;
                }
            }
            LeaveProcess lvproc = new LeaveProcess();
            string retval = lvproc.SaveInitiateAttendanceProcessData(lvphdrlst, myCommand);
            if (retval == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }
            LoadAllInformation();
            if (retval != "")
            {
                //SendMailtoApprover();
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


    private void LoadAllInformation()
    {
        DateTime fDate, lDate;
        fDate = Convert.ToDateTime(ReturnDecissinon().Split('@')[0]);
        lDate = Convert.ToDateTime(ReturnDecissinon().Split('@')[1]);
        LoadOvertimeDetailsemployeeID(Session["ApplicantID"].ToString(), PID, FID, fDate, lDate);
        LoadEmployeeInformation(Session["ApplicantID"].ToString());
        lblPeriod.Text = FinanialPeriod(fDate);
        lblcurrentPeriod.Text = System.DateTime.Now.ToString();
    }

    protected void btnprevious_Click(object sender, EventArgs e)
    {
        DateTime fDate = Convert.ToDateTime("01/12/2012");
        DateTime lDate = Convert.ToDateTime("31/12/2013");


        LoadOvertimeDetailsemployeeID(Session["ApplicantID"].ToString(), PID, FID, fDate, lDate);
        LoadEmployeeInformation(Session["ApplicantID"].ToString());
        //PanelLeaveHdr.Visible=false; 
        lblPeriod.Text = FinanialPeriod(fDate);
        lblcurrentPeriod.Text = System.DateTime.Now.ToString();
    }
    protected void btnCurrentPeriod_Click(object sender, EventArgs e)
    {
        DateTime fDate = Convert.ToDateTime("01/01/2013");
        DateTime lDate = Convert.ToDateTime("31/01/2013");


        LoadOvertimeDetailsemployeeID(Session["ApplicantID"].ToString(), PID, FID, fDate, lDate);
        LoadEmployeeInformation(Session["ApplicantID"].ToString());
        //PanelLeaveHdr.Visible=false; 
        lblPeriod.Text = FinanialPeriod(fDate);
        lblcurrentPeriod.Text = System.DateTime.Now.ToString();
    }
    protected void gdvLeaveInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        DateTime fDate = Convert.ToDateTime(txtFromDate.Text);
        DateTime lDate = Convert.ToDateTime(txtToDate.Text);
        Session["ApplicantID"] = "L3T593";
        //LoadLeaveByemployeeID(Session["ApplicantID"].ToString(), "P001", "1", fDate, lDate);
    }
    protected void btnShowALL_Click(object sender, EventArgs e)
    {
        if (txtFromDate.Text == string.Empty || txtToDate.Text == string.Empty) return;
        DateTime fDate = Convert.ToDateTime(txtFromDate.Text);
        DateTime lDate = Convert.ToDateTime(txtToDate.Text);
        //Session["ApplicantID"] = ddlEmployeeId.SelectedValue;
        Session["ApplicantID"] = txtEmployeeCode.Text;
        LoadOvertimeDetailsemployeeID(Session["ApplicantID"].ToString(), PID, FID, fDate, lDate);
        LoadEmployeeInformation(Session["ApplicantID"].ToString());
        LoadResponsiblePersonID(Session["ApplicantID"].ToString());
        LoadLeavBALeByemployeeID(Session["ApplicantID"].ToString(), fDate);

    }
    private bool Sendmail(string sid, string sname, string rid, string rname, string ccid, string msub, string mbody)
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
                    if (cary[i].Trim() != "")
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

    protected void grdHoliday_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
            CheckBox chkbox = e.Row.FindControl("CheckRet") as CheckBox;
            Label lb = e.Row.FindControl("lblstatus") as Label;
            DateTime dta = Convert.ToDateTime(e.Row.Cells[1].Text);
            lb.Text = e.Row.Cells[16].Text;
            MKB.TimePicker.TimeSelector mkb = (MKB.TimePicker.TimeSelector)e.Row.FindControl("timeoff1");
            MKB.TimePicker.TimeSelector mkb2 = (MKB.TimePicker.TimeSelector)e.Row.FindControl("timeoff2");

            DateTime dt1 = DateTime.Parse(e.Row.Cells[18].Text);
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

            DateTime dt2 = DateTime.Parse(e.Row.Cells[19].Text);
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

            if (Convert.ToInt32(e.Row.Cells[17].Text) > 0)
            {
                chkbox.Enabled = false;
                mkb.Enabled = false;
                mkb2.Enabled = false;
                e.Row.BackColor = System.Drawing.Color.LightGreen;

            }
            //if (dta < Convert.ToDateTime(Session["fdate"].ToString()))
            //{
            //    chkbox.Enabled = false;
            //    mkb.Enabled = false;
            //    mkb2.Enabled = false;
            //}
            if (e.Row.Cells[3].Text == "H")
            {
                if (e.Row.Cells[26].Text == "&nbsp;")
                    e.Row.Cells[3].Text = "HOLIDAY" + "(Shifting)";
                else
                    e.Row.Cells[3].Text = "HOLIDAY" + "(" + e.Row.Cells[26].Text + ")";

                e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[22].Text = "";
                e.Row.Cells[23].Text = "";
            }
            if (e.Row.Cells[3].Text == "O")
            {
                e.Row.Cells[3].Text = "HOLIDAY";
                e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[22].Text = "";
                e.Row.Cells[23].Text = "";
            }

            if (e.Row.Cells[3].Text == "L" || e.Row.Cells[3].Text == "N")
            {
                e.Row.Cells[3].Text = "LEAVE";
                chkbox.Enabled = false;
                mkb.Enabled = false;
                mkb2.Enabled = false;
                e.Row.Cells[3].ForeColor = System.Drawing.Color.Navy;
                e.Row.Cells[3].Font.Bold = true;
                e.Row.Cells[22].Text = "";
                e.Row.Cells[23].Text = "";
            }
            if (e.Row.Cells[3].Text == "A")
            {
                e.Row.Cells[3].Text = "PRESENT";
            }
           
            if (e.Row.Cells[3].Text == "P" && Convert.ToDateTime(e.Row.Cells[1].Text) <= Convert.ToDateTime(System.DateTime.Now.ToShortDateString()))
            {
                e.Row.Cells[3].Text = "ABSENT";
                e.Row.BackColor = System.Drawing.Color.Red;
            }
            if (e.Row.Cells[3].Text == "P" && Convert.ToDateTime(e.Row.Cells[1].Text) > Convert.ToDateTime(System.DateTime.Now.ToShortDateString()))
            {
                e.Row.Cells[3].Text = "";
                e.Row.Cells[22].Text = "";
                e.Row.Cells[23].Text = "";

            }
            if (e.Row.Cells[3].Text == "Z")
            {
                e.Row.Cells[3].Text = "";
                e.Row.Cells[22].Text = "";
                e.Row.Cells[23].Text = "";
            }
            if (e.Row.Cells[22].Text != "0")
            {
                e.Row.Cells[22].ForeColor = System.Drawing.Color.Red;
            }
            if (e.Row.Cells[23].Text != "0")
            {
                e.Row.Cells[23].ForeColor = System.Drawing.Color.Red;
            }


            int totalMin = Convert.ToInt32(e.Row.Cells[20].Text);//just changed the index of cells based on your requirements 
            _total += totalMin;
            int totalLessMin = Convert.ToInt32(e.Row.Cells[25].Text);
            _totalLess += totalLessMin;
            int totalWorkingMin = Convert.ToInt32(e.Row.Cells[27].Text);
            _totalWorkingMinutes += totalWorkingMin;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {            
            e.Row.Cells[12].Text = DateProcess.TimeDuration(Convert.ToInt32(_total.ToString()));
            e.Row.Cells[21].Text = DateProcess.TimeDuration(Convert.ToInt32(_totalLess.ToString()));
            e.Row.Cells[6].Text = DateProcess.TimeDuration(Convert.ToInt32(_totalWorkingMinutes.ToString()));
            e.Row.Font.Bold = true;
        }

        e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Center;
        e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[21].HorizontalAlign = HorizontalAlign.Center;
        e.Row.Cells[22].HorizontalAlign = HorizontalAlign.Center;
        e.Row.Cells[23].HorizontalAlign = HorizontalAlign.Center;

        //e.Row.Cells[6].Visible = false;
        e.Row.Cells[7].Visible = false;
        e.Row.Cells[8].Visible = false;
        e.Row.Cells[9].Visible = false;
        e.Row.Cells[10].Visible = false;
        e.Row.Cells[11].Visible = false;
        //e.Row.Cells[12].Visible = false;
        e.Row.Cells[13].Visible = false;
        e.Row.Cells[14].Visible = false;
        e.Row.Cells[15].Visible = false;
        e.Row.Cells[16].Visible = false;
        e.Row.Cells[17].Visible = false;
        e.Row.Cells[18].Visible = false;
        e.Row.Cells[19].Visible = false;
        e.Row.Cells[20].Visible = false;
        //e.Row.Cells[21].Visible = false;

        e.Row.Cells[25].Visible = false;
        e.Row.Cells[26].Visible = false;
        e.Row.Cells[27].Visible = false;
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
                //  Create a table to contain the grid
                System.Web.UI.WebControls.Table table = new System.Web.UI.WebControls.Table();

                //  include the gridline settings
                table.GridLines = gv.GridLines;

                //  add the header row to the table
                if (gv.HeaderRow != null)
                {
                    PrepareControlForExport(gv.HeaderRow);
                    table.Rows.Add(gv.HeaderRow);
                }

                //  add each of the data rows to the table
                foreach (GridViewRow row in gv.Rows)
                {
                    PrepareControlForExport(row);
                    table.Rows.Add(row);
                }

                //  add the footer row to the table
                if (gv.FooterRow != null)
                {
                    PrepareControlForExport(gv.FooterRow);
                    table.Rows.Add(gv.FooterRow);
                }

                //  render the table into the htmlwriter
                table.RenderControl(htw);

                //  render the htmlwriter into the response
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
        if (grdHoliday.Rows.Count != 0)
        {
            string type = "Attendance of " + ddlEmployeeId.Text + ".xls";
            Export(type, grdHoliday);
        }
        else
        {
            string validationMsg = "There is no data for Export ! ";
            ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + validationMsg + "');",
                    true);
        }
    }

    private void ShowReport(string connectionString, string selectionfor, string parameter, string reportname)
    {
        try
        {
            var rpt = new clsReport();
            var myParams = new ParameterFields();
            var connInfo = new ConnectionInfo();
            string[] prm = parameter.Split(';');
            if (prm.Length > 0)
            {
                foreach (string t in prm)
                {
                    parameterpass(myParams, t.Split(':')[0], t.Split(':')[1]);
                }
            }
            string[] ff = connectionString.Split('=');
            string[] ss = ff[1].Split(';');
            connInfo.ServerName = ss[0];
            ss = ff[2].Split(';');
            connInfo.DatabaseName = ss[0];
            ss = ff[3].Split(';');
            connInfo.UserID = ss[0];
            ss = ff[4].Split(';');
            connInfo.Password = ss[0];
            rpt.FileName = reportname;
            rpt.ConnectionInfo = connInfo;
            rpt.ParametersFields = myParams;
            rpt.SelectionFormulla = selectionfor;
            Session[GlobalData.sessionReportDet] = rpt;
            RegisterStartupScript("click", "<script>window.open('./frm_report_viewer.aspx');</script>");

        }
        catch (Exception msgException)
        {
            
            throw msgException;
        }
    }
    private void parameterpass(ParameterFields myParams, string pname, string value)
    {
        try
        {
            var param = new ParameterField();
            var dis1 = new ParameterDiscreteValue();
            param.ParameterFieldName = pname;
            dis1.Value = value;
            param.CurrentValues.Add(dis1);
            myParams.Add(param);

        }
        catch (Exception msgException)
        {
            
            throw msgException;
        }
    }

    private void PreviewData()
    {
        try
        {
            AttendanceIndividualViewController objAttendanceIndividualViewController = new AttendanceIndividualViewController();
            AttendanceIndividualView objAttendanceIndividualView = new AttendanceIndividualView();
            objAttendanceIndividualView.UserId = current.UserId;
            objAttendanceIndividualViewController.Delete(_connectionStr, objAttendanceIndividualView);
            foreach (GridViewRow grdRow in grdHoliday.Rows)
            {
                objAttendanceIndividualView = new AttendanceIndividualView();

                objAttendanceIndividualView.TargetDate = grdRow.Cells[1].Text.ToString() == "&nbsp;" ? null : grdRow.Cells[1].Text.ToString();
                objAttendanceIndividualView.DayName = grdRow.Cells[2].Text.ToString() == "&nbsp;" ? null : grdRow.Cells[2].Text.ToString();
                objAttendanceIndividualView.Description = grdRow.Cells[3].Text.ToString() == "&nbsp;" ? null : grdRow.Cells[3].Text.ToString();
                objAttendanceIndividualView.InTime = grdRow.Cells[4].Text.ToString() == "&nbsp;" ? null : grdRow.Cells[4].Text.ToString();
                objAttendanceIndividualView.OutTime = grdRow.Cells[5].Text.ToString() == "&nbsp;" ? null : grdRow.Cells[5].Text.ToString();
                objAttendanceIndividualView.LateBy = grdRow.Cells[22].Text.ToString() == "&nbsp;" ? null : grdRow.Cells[22].Text.ToString();
                objAttendanceIndividualView.EarlyBy = grdRow.Cells[23].Text.ToString() == "&nbsp;" ? null : grdRow.Cells[23].Text.ToString();
                objAttendanceIndividualView.Remarks = grdRow.Cells[24].Text.ToString() == "&nbsp;" ? null : grdRow.Cells[24].Text.ToString();
                objAttendanceIndividualView.UserId = current.UserId;
                objAttendanceIndividualView.WorkingHour = grdRow.Cells[6].Text.ToString() == "&nbsp;" ? null : grdRow.Cells[6].Text.ToString();
                objAttendanceIndividualView.ExtraHour = grdRow.Cells[12].Text.ToString() == "&nbsp;" ? null : grdRow.Cells[12].Text.ToString();
                objAttendanceIndividualView.LessHour = grdRow.Cells[21].Text.ToString() == "&nbsp;" ? null : grdRow.Cells[21].Text.ToString();
                objAttendanceIndividualViewController.Save(_connectionStr, objAttendanceIndividualView);
            }

            string selectionfor = "{tblAttendanceReport.userId}='" + current.UserId + "'";
            string CompanyName = "CompanyName" + ":" + current.CompanyName;
            string CompanyAddress = "CompanyAddress" + ":" + current.CompanyAddress;
            string ID = "ID" + ":" + (lblId.Text == string.Empty ? null : lblId.Text);
            string Name = "Name" + ":" + (lblName.Text == string.Empty ? null : lblName.Text);
            string Department = "Department" + ":" + (lbldept.Text == string.Empty ? null : lbldept.Text);
            string Designation = "Designation" + ":" + (lblDesignation.Text == string.Empty ? null : lblDesignation.Text);
            string JoiningDate = "Joining Date" + ":" + (lblJoiningDate.Text == string.Empty ? null : lblJoiningDate.Text);
            string totalworkingHour = "totalWorkingHour" + ":" + (grdHoliday.FooterRow.Cells[6].Text.ToString() == "&nbsp;" ? null : grdHoliday.FooterRow.Cells[6].Text.ToString().Replace(":", "@"));
            string totalextraHour = "totalExtraHour" + ":" + (grdHoliday.FooterRow.Cells[12].Text.ToString() == "&nbsp;" ? null : grdHoliday.FooterRow.Cells[12].Text.ToString().Replace(":", "@"));
            string totallessHour = "totalLessHour" + ":" + (grdHoliday.FooterRow.Cells[21].Text.ToString() == "&nbsp;" ? null : grdHoliday.FooterRow.Cells[21].Text.ToString().Replace(":", "@"));
            string parameter = CompanyName + ";" + CompanyAddress + ";" + ID + ";" + Name + ";" + Department + ";" + Designation + ";" + JoiningDate + ";" + totalworkingHour + ";" + totalextraHour + ";" + totallessHour;
            const string reportname = "../Reports/ReportAttendanceIndividualView.rpt";
            ShowReport(_connectionStr, selectionfor, parameter, reportname);

        }
        catch (Exception msgException)
        {
            
            throw msgException;
        }
    }
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        try
        {
            PreviewData();
        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }
    protected void txtEmployeeCode_TextChanged(object sender, EventArgs e)
    {
        if (txtEmployeeCode.Text != string.Empty)
        {
            txtEmployeeCode.Text = txtEmployeeCode.Text.Split(':')[0].Trim() == "" ? "" : txtEmployeeCode.Text.Split(':')[0].Trim();
            var employeeCode = txtEmployeeCode.Text;           
        }
    }
}
