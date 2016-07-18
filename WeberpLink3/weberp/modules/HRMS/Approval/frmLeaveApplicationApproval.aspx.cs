using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
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

public partial class modules_HRMS_Approval_frmLeaveApplicationApproval : Page
{

    string ConnectionStr = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    string PID = "P001";
    string FID = "1";
    protected void Page_Load(object sender, EventArgs e)
    {
        StaticData.MsgConfirmBox(btnApprove, "Are you sure want to Approve? ");
        StaticData.MsgConfirmBox(btnReject, "Are you sure want to Reject? ");
        StaticData.MsgConfirmBox(btnForward, "Are you sure want to Forward? ");
        if (!Page.IsPostBack)
        {
            DataTable dttask = new DataTable();
            DateTime serverdate = DateProcess.GetServerDate(ConnectionStr);
            DateTime fDate = DateProcess.FirstDateOfMonth(serverdate);
            DateTime lDate = DateProcess.LastDateOfMonth(serverdate);
            dttask = DataProcess.GetData(ConnectionStr, "select * from ProcessTaskDateAllemoloyee where TaskType='LV' and [Status]='Y'");
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

            //Session["ActingPersonID"] = "";
            //Session["EntryUserid"] = "";

            Session["ApplicantID"] = Session[StaticData.sessionUserId].ToString();
            Session["ActingPersonID"] = Session[StaticData.sessionUserId].ToString();
            Session["EntryUserid"] = Session[StaticData.sessionUserId].ToString();

            Session["fdate"] = fDate;
            Session["lDate"] = lDate;
            LoadpendingLeaveApplication(Session["ActingPersonID"].ToString(),PID,FID);  
            lblPeriod.Text = FinanialPeriod(fDate);
            lblcurrentPeriod.Text = Convert.ToString(DateTime.Now.Date.Day) + "-" + Convert.ToString(DateTime.Now.Date.Month) + "-" + Convert.ToString(DateTime.Now.Date.Year);
            Panel50.Visible = false;
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
        dt = DataProcess.GetData(ConnectionStr, "select EmpID,EmpName,Dept,Designation,b.Emp_Mas_Join_Date as Joiningdate from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id where EmpID='" + empid + "'");
        if (dt.Rows.Count > 0)
        {
            lblId.Text = dt.Rows[0]["EmpID"].ToString();
            lblName.Text = dt.Rows[0]["EmpName"].ToString();
            lbldept.Text = dt.Rows[0]["Dept"].ToString();
            lblDesignation.Text = dt.Rows[0]["Designation"].ToString();
            lblJoiningDate.Text = dt.Rows[0]["Joiningdate"].ToString().Substring(0,10);
        }
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
                                                + " where a.Empid='" + empid + "' ");
        gdvLeaveInfo.DataSource = dt;
        gdvLeaveInfo.DataBind();
    }

    private DataTable LoadLeaveByemployeeID(string empid, string Processid, string flowid, DateTime fDate, DateTime lDate)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();     
        SqlConnection sqlConn = null;        
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();        
        cmd.Connection = sqlConn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetPreviousLeaveStatusByEmpid";
        cmd.Parameters.Add(new SqlParameter("@fDate", SqlDbType.DateTime)).Value = fDate;
        cmd.Parameters.Add(new SqlParameter("@lDate ", SqlDbType.DateTime)).Value = lDate;
        cmd.Parameters.Add(new SqlParameter("@empid", SqlDbType.NVarChar)).Value = empid;
        cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid;
        cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid;
        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);       
        da.Fill(dt);
        sqlConn.Close();
        return dt;
    }

    private void LoadPendingLeaveByemployeeID(string transactionno, string Processid, string flowid,string actingpersonid,string leavecode)
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;

        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetPendingLeaveStatusByTransactionno";
        cmd.Parameters.Add(new SqlParameter("@transactionno", SqlDbType.NVarChar)).Value = transactionno;
        cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid;
        cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid;
        cmd.Parameters.Add(new SqlParameter("@actingpersonid", SqlDbType.NVarChar)).Value = actingpersonid;
        cmd.Parameters.Add(new SqlParameter("@leavecode", SqlDbType.NVarChar)).Value = leavecode;

        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        sqlConn.Close();

        GridViewLeave.DataSource = dt;
        GridViewLeave.DataBind();
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
        cmd.Parameters.Add(new SqlParameter("@transactionno", SqlDbType.NVarChar)).Value = transactionno;
        cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid;
        cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid;
        cmd.Parameters.Add(new SqlParameter("@actingpersonid", SqlDbType.NVarChar)).Value = actingpersonid;
        cmd.Parameters.Add(new SqlParameter("@leavecode", SqlDbType.NVarChar)).Value = leavecode;

        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        sqlConn.Close();

        string remarks = "Please see all remarks below:";
        string remarks1 = "";
        string renarks2 = "";
        string remarksnew = "";
        string remarksline = "----------------------------------";
        int ln = 0;
        foreach (DataRow dr in dt.Rows)
        {
            ln = ln + 1;
            remarksnew = ln+". "+dr["EmpName"] + " >> " + dr["Remarks"];
            remarks += "\n" + remarksline;
            remarks += "\n" + remarksnew;
            
        }  
                    
        txtRemarksAll.Text = remarks;
        int numLines = remarks.Split('\n').Length;

        string[] lines = txtRemarksAll.Text.Split('\n');
        numLines = lines.Length - 1;
        txtRemarksAll.Height = (numLines + ln) * 15 + 25;
        txtRemarksAll.Enabled = false; 

    }

    private void LoadAttendanceDetailsemployeeID(string empid, string Processid, string flowid, DateTime fDate, DateTime lDate)
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetAllDayStatus";

        cmd.Parameters.Add(new SqlParameter("@fDate", SqlDbType.DateTime)).Value = fDate;
        cmd.Parameters.Add(new SqlParameter("@lDate ", SqlDbType.DateTime)).Value = lDate;
        cmd.Parameters.Add(new SqlParameter("@empid", SqlDbType.NVarChar)).Value = empid;
        cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid;
        cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid;

        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        sqlConn.Close();

        GridViewLeave.DataSource = dt;
        GridViewLeave.DataBind();
    }

    private void LoadpendingLeaveApplication(string actingpersonid, string Processid, string flowid)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetAllPendingLeave";
        cmd.Parameters.Add(new SqlParameter("@actempid", SqlDbType.NVarChar)).Value = actingpersonid;
        cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid;
        cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid;
        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        sqlConn.Close();
        GridViewLeavePending.DataSource = dt;
        GridViewLeavePending.DataBind();
        //GridViewLeavePending.Columns[8].Visible = false;
        //GridViewLeavePending.Columns[5].Visible = false;
        //string gridhieght = GridViewLeavePending.Height.Value.ToString();
        //int gdr = GridViewLeavePending.Rows.Count;          
        //CollapsiblePanelExtenderSrch.ExpandedSize =(gdr*40)+25;
        //CollapsiblePanelExtenderSrch.ExpandedSize =Convert.ToInt32(pnlSrchDet.Height.Value);
    }


    private string GetPendingApplicationCount(string actingpersonid, string Processid, string flowid)
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        string TaskCount = "-1";
        try
        {            
            DataTable dt = new DataTable();           
            cmd.Connection = sqlConn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spProcessGetPendingApplicationCount";
            cmd.Parameters.Add(new SqlParameter("@actempid", SqlDbType.NVarChar)).Value = actingpersonid;
            cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid;
            cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid;
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                TaskCount = dt.Rows[0]["nooftask"].ToString();
            }

            //return TaskCount;
                        
        }
        catch (Exception ex)
        {
            TaskCount = "-1";            
        }
        finally
        {
            sqlConn.Close();
        }

        return TaskCount;

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
            if (sqlConn.State == ConnectionState.Open)
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

        string constr = ConfigurationManager.AppSettings["ConStrLNK"];

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

        string qrystr = (DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second).ToString();
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
        int h = Convert.ToInt32(atf.Split(':')[0]);
        int m = Convert.ToInt32(atf.Split(':')[1]);

        if (h > 12)
        { 
            h=h-12;
        }              
        
        string hh = string.Format("{0:00}",h);
        string mm = string.Format("{0:00}",m);
        string ampm = atf.Split(':')[2];
                
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

        SqlConnection oConnection = new SqlConnection(ConnectionStr);


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
    protected void GridViewLeave_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataTable dtl = new DataTable();

        string sql = "select distinct Leave_mas_code,Leave_mas_Name from hrms_leave_mas a"
                    + " inner join HrMs_Emp_mas b on b.Emp_Mas_Emp_Type=a.T_C2 and b.Emp_Mas_Emp_Id='" + Session["ApplicantID"].ToString() + "'"
                    + " where a.T_FL=1 and Leave_Mas_Code not in"
                    + " ("
                    + " select case when Emp_Mas_Gender='M' then 'ML' else ''end from hrms_leave_mas a"
                    + " inner join HrMs_Emp_mas b on b.Emp_Mas_Emp_Type=a.T_C2 and b.Emp_Mas_Emp_Id='" + Session["ApplicantID"].ToString() + "') order by Leave_mas_Name";

        dtl = DataProcess.GetData(ConnectionStr, sql);
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbSl = e.Row.FindControl("lblSL") as Label;
            //var sl = GetColumnIndexByName(e.Row, "Sl");
            lbSl.Text = Convert.ToString(e.Row.RowIndex + 1);
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

            dpl.Width = 120;
            
            Label lblNoOfDays = e.Row.FindControl("lblNoOfDays") as Label;
            dpl.SelectedValue = lblNoOfDays.Text;


            DropDownList dpllt = e.Row.FindControl("lvtype") as DropDownList;

            ListItem lstlt = new ListItem();
            lstlt.Text = "--Select--";
            lstlt.Value = "";
            dpllt.Items.Add(lstlt);
            foreach (DataRow dr in dtl.Rows)
            {
                ListItem lstlv = new ListItem();
                lstlv.Value = dr["Leave_mas_code"].ToString();
                lstlv.Text = dr["Leave_mas_Name"].ToString();
                dpllt.Items.Add(lstlv);
            }

            //ListItem lstlt1 = new ListItem();
            //lstlt1.Text = "Casual Leave";
            //lstlt1.Value = "CL";
            //dpllt.Items.Add(lstlt1);

            //ListItem lstlt2 = new ListItem();
            //lstlt2.Text = "Sick Leave";
            //lstlt2.Value = "SL";
            //dpllt.Items.Add(lstlt2);

            //ListItem lstlt3 = new ListItem();
            //lstlt3.Text = "Earned Leave";
            //lstlt3.Value = "EL";
            //dpllt.Items.Add(lstlt3);

            dpllt.Width = 130;
           
            Label lblType = e.Row.FindControl("lblType") as Label;
            dpllt.SelectedValue = lblType.Text;
            
            Label lbDate = e.Row.FindControl("lblDate") as Label;
            DateTime dta = Convert.ToDateTime(Convert.ToDateTime(lbDate.Text).ToShortDateString());
            Label lblAction = e.Row.FindControl("lblAction") as Label;

            lb.Text = lblAction.Text;
            Label lblProcessLevelid = e.Row.FindControl("lblProcessLevelid") as Label;

            var value = lblProcessLevelid.Text;
            
            Label lbstatus = e.Row.FindControl("lblStatus") as Label;
            if (!string.IsNullOrEmpty(value) && Convert.ToInt32(value) > 0)
            {
                dpl.Enabled = false;
                dpllt.Enabled = false;
                //chkbox.Enabled = false;
                chkbox.Checked = true;
                tb.Enabled = false;
                e.Row.BackColor = System.Drawing.Color.LightGreen;

                lbstatus.Text = "Leave";

            }
            if (lbstatus.Text == "H")
            {
                lbstatus.Text = "Holiday";
                
                Label lbsIntime = e.Row.FindControl("lblIntime") as Label;
                Label lblOutTime = e.Row.FindControl("lblOutTime") as Label;
                Label lblHours = e.Row.FindControl("lblHours") as Label;
                
                lbsIntime.Text = "-";
                lblOutTime.Text = "-";
                lblHours.Text = "-";
                e.Row.BackColor = System.Drawing.Color.LightGray;
                // chkbox.Visible = false;
                tb.Visible = false;
                lb.Visible = false;
                dpl.Visible = false;
                dpllt.Visible = false;

            }
            if (lbstatus.Text == "L" || lbstatus.Text == "N")
            {
                lbstatus.Text = "Leave";
                e.Row.BackColor = System.Drawing.Color.LightGreen;
                //chkbox.Enabled = true;
                tb.Enabled = false;
                Label lblIsProcessLocked = e.Row.FindControl("lblIsProcessLocked") as Label;
                lb.Text = lblIsProcessLocked.Text;
                dpl.Enabled = false;
                dpllt.Enabled = false;
            }
            if (lbstatus.Text == "A")
            {
                lbstatus.Text = "Present";
                chkbox.Enabled = true;
                tb.Enabled = true;
            }
            if (lbstatus.Text == "P")
            {
                lbstatus.Text = "Absent";
                //e.Row.Cells[atnd_det_offlg].ForeColor = System.Drawing.Color.Red;
                chkbox.Enabled = true;
                tb.Enabled = true;
            }

            if (dta < Convert.ToDateTime(Session["fdate"].ToString()))
            {
                chkbox.Enabled = false;
                tb.Enabled = false;
                lb.Enabled = false;
                dpl.Enabled = false;
                dpllt.Enabled = false;
            }
           
            Label lblIsLineLocked = e.Row.FindControl("lblIsLineLocked") as Label;
            if (lblIsLineLocked.Text == "Y")
            {
                chkbox.Enabled = false;
                tb.Enabled = false;
                lb.Enabled = false;
                dpl.Enabled = false;
                dpllt.Enabled = false;
            }

        }
        #region Junk
        
        //        rowNumberDataRow["Atnd_det_offlg"].Equals("Leave"); //"Leave";//e.Row.Cells[3].Text = "Leave";

        //    }
        //    if (rowNumberDataRow["Atnd_det_offlg"].ToString() == "H")
        //    {
        //        rowNumberDataRow["Atnd_det_offlg"] = "Holiday";
        //        rowNumberDataRow["Atnd_det_intime"] = "-";
        //        rowNumberDataRow["Atnd_det_outtime"] = "-";
        //        rowNumberDataRow["Atnd_det_hrs"] = "-";
        //        e.Row.BackColor = Color.LightGray;
        //       // chkbox.Visible = false;
        //        tb.Visible = false;
        //        lb.Visible = false;
        //        dpl.Visible = false;
        //        dpllt.Visible = false; 

        //    }
        //    if (rowNumberDataRow["Atnd_det_offlg"].ToString() == "L" || rowNumberDataRow["Atnd_det_offlg"].ToString() == "N")
        //    {
        //        rowNumberDataRow["Atnd_det_offlg"] = "Leave";
        //        e.Row.BackColor = Color.LightGreen;
        //        chkbox.Enabled = false;
        //        tb.Enabled = false;
        //        lb.Text = rowNumberDataRow["IsProcessLocked"].ToString();
        //        dpl.Enabled = false;
        //        dpllt.Enabled = false;
        //    }
        //    if (rowNumberDataRow["Atnd_det_offlg"].ToString() == "A")
        //    {
        //        rowNumberDataRow["Atnd_det_offlg"] = "Present";
        //        chkbox.Enabled = true;
        //        tb.Enabled = true; 
        //    }
        //    if (rowNumberDataRow["Atnd_det_offlg"].ToString() == "P")
        //    {
        //        rowNumberDataRow["Atnd_det_offlg"] = "Absent";
        //        //e.Row.Cells[3].ForeColor = Color.Red;
        //        chkbox.Enabled = true;
        //        tb.Enabled = true;
        //    }

        //    if (dta < Convert.ToDateTime(Session["fdate"].ToString()))
        //    {
        //        chkbox.Enabled = false;
        //        tb.Enabled = false;
        //        lb.Enabled = false;
        //        dpl.Enabled = false;
        //        dpllt.Enabled = false;
        //    }
        //    if (rowNumberDataRow["IsLineLocked"].ToString() == "Y")
        //    {
        //        chkbox.Enabled = false;
        //        tb.Enabled = false;
        //        lb.Enabled = false;
        //        dpl.Enabled = false;
        //        dpllt.Enabled = false;
        //    }
        //}
        #endregion


        var leavetype = GetColumnIndexByName(e.Row, "Type");
        e.Row.Cells[leavetype].Visible = false;
        var noofdayshide = GetColumnIndexByName(e.Row, "Comments");
        e.Row.Cells[noofdayshide].Visible = false;
        var remarks = GetColumnIndexByName(e.Row, "noofd");
        e.Row.Cells[remarks].Visible = false;
        var iIsLineLocked = GetColumnIndexByName(e.Row, "Remarks");
        e.Row.Cells[iIsLineLocked].Visible = false;
        var isProcessLockedl = GetColumnIndexByName(e.Row, "LockedL");
        e.Row.Cells[isProcessLockedl].Visible = false;
        var actionhide = GetColumnIndexByName(e.Row, "Action");
        e.Row.Cells[actionhide].Visible = false;
        var processLevelidhide = GetColumnIndexByName(e.Row, "PLid");
        e.Row.Cells[processLevelidhide].Visible = false;
        var transactionNohide = GetColumnIndexByName(e.Row, "tno");
        e.Row.Cells[transactionNohide].Visible = false;
        var transactionNoLineNo = GetColumnIndexByName(e.Row, "tlno");
        e.Row.Cells[transactionNoLineNo].Visible = false;
        var LockedP = GetColumnIndexByName(e.Row, "LockedP");
        e.Row.Cells[LockedP].Visible = false;

        var astatus = GetColumnIndexByName(e.Row, "AStatus");
        e.Row.Cells[astatus].Visible = false;

        //e.Row.Cells[GridViewLeave.Columns[0].HeaderText].Text;



    }
    int GetColumnIndexByName(GridViewRow row, string columnName)
    {
        int columnIndex = 0;
        foreach (DataControlFieldCell cell in row.Cells)
        {
            if (cell.ContainingField is TemplateField)
                if (((TemplateField)cell.ContainingField).HeaderText.Equals(columnName))
                    break;
            columnIndex++; // keep adding 1 while we don't have the correct name
        }
        return columnIndex;
    }

    private void HideColumn(string columName)
    {
        var fieldType = (DataControlField)GridViewLeave.Columns.Cast<DataControlField>().SingleOrDefault(fld => (fld.HeaderText == columName));
        if (fieldType != null)
            fieldType.Visible = false;
    }

    protected void GridViewLeavePending_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);

            e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(GridViewLeavePending, "Select$" + e.Row.RowIndex);

               
        }

        e.Row.Cells[5].Visible = false;
        e.Row.Cells[8].Visible = false;   


    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        SqlConnection myConnection = new SqlConnection(ConnectionStr);
        myConnection.Open();
        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;
        myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        try
        {
            List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();

            for (int i = 0; i < GridViewLeave.Rows.Count; i++)
            {
                string processId =PID;
                string flowId =FID;
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId =5;  // Approve
                CheckBox chkbox = GridViewLeave.Rows[i].FindControl("Checklv") as CheckBox;
                if (chkbox.Checked)
                {
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();
                    TextBox tb = GridViewLeave.Rows[i].FindControl("txtlvRemarks") as TextBox;
                    DropDownList dpllvno = GridViewLeave.Rows[i].FindControl("dpllday") as DropDownList;
                    DropDownList dpllvtype = GridViewLeave.Rows[i].FindControl("lvtype") as DropDownList;
                    Label lblTransactionNo = GridViewLeave.Rows[i].FindControl("lblTransactionNo") as Label;
                    Label lblTransactionNoLineNo = GridViewLeave.Rows[i].FindControl("lblTransactionNoLineNo") as Label;
                    Label lblProcessLevelid = GridViewLeave.Rows[i].FindControl("lblProcessLevelid") as Label;
                    Label lblSL = GridViewLeave.Rows[i].FindControl("lblSL") as Label;
                    Label lblDate = GridViewLeave.Rows[i].FindControl("lblDate") as Label;
                    Label lblIntime = GridViewLeave.Rows[i].FindControl("lblIntime") as Label;
                    Label lblOutTime = GridViewLeave.Rows[i].FindControl("lblOutTime") as Label;
                    Label lblHours = GridViewLeave.Rows[i].FindControl("lblHours") as Label;
                    offphdr.TransactionNo = Convert.ToString(lblTransactionNo.Text);
                    offphdr.TransactionLineNo = Convert.ToInt32(lblTransactionNoLineNo.Text);
                    offphdr.ApplicantId = Session["empcode"].ToString();
                    offphdr.ProcessId = processId;
                    offphdr.FlowId = flowId;
                    offphdr.ProcesslevelId = Convert.ToInt32(lblProcessLevelid.Text);
                    offphdr.ProcessnextlevelId = 1;
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.SL = Convert.ToInt32(lblSL.Text);
                    offphdr.ClaiminDdate = Convert.ToDateTime(lblDate.Text);
                    offphdr.SysIndate = Convert.ToDateTime(lblDate.Text);
                    offphdr.SysOutdate = Convert.ToDateTime(lblDate.Text);
                    offphdr.ActIndate = Convert.ToDateTime(lblDate.Text);
                    offphdr.ActOutdate = Convert.ToDateTime(lblDate.Text);
                    offphdr.SysIntime = lblIntime.Text == "&nbsp;" ? "00:00 AM" : lblIntime.Text;
                    offphdr.SysOuttime = lblOutTime.Text == "&nbsp;" ? "00:00 AM" : lblOutTime.Text;
                    offphdr.SysTotalhrs = lblHours.Text == "&nbsp;" ? "00:00" : lblHours.Text;
                    offphdr.NoofLeave = Convert.ToDouble(dpllvno.SelectedItem.Value);
                    offphdr.Leavetype = dpllvtype.SelectedItem.Value;
                    offphdr.Leavedescription = dpllvtype.SelectedItem.Text;
                    offphdr.Remarks = txtRemarks.Text.Replace("'","") == "" ? "NA" : txtRemarks.Text.Replace("'","");
                    offphdr.Acthrs = "00:00";
                    offphdr.ActingpersonId = ActingPersonId;
                   
                    lvphdrlst.Add(offphdr);
                }
            }

            LeaveProcess lvproc = new LeaveProcess();

            string retval = lvproc.SaveApproveLeaveProcessData(lvphdrlst, myCommand);
                                 
            if (retval == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }

            LoadAllInformation();

            ReloadAllgridAfterForwardRejectApprove();

            if (retval != "")
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
    private void SendMailforApproval()
    {
        try
        {
            // mail send for Approval
            DataTable dts = DataProcess.GetData(ConnectionStr, "select a.EmpName,b.Emp_Mas_Remarks from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id where Emp_Mas_Emp_Id='" + Session["ActingPersonID"] + "'");
            DataTable dtr = DataProcess.GetData(ConnectionStr, "select a.EmpName,b.Emp_Mas_Remarks,b.Emp_Mas_Gender from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id where Emp_Mas_Emp_Id='" + Session["empcode"] + "'");


            string atn = "";
            string Gender = dtr.Rows[0]["Emp_Mas_Gender"].ToString();
            string sid = dts.Rows[0]["Emp_Mas_Remarks"].ToString();
            string sname = dts.Rows[0]["EmpName"].ToString();
            string rid = dtr.Rows[0]["Emp_Mas_Remarks"].ToString();
            string rname = dtr.Rows[0]["EmpName"].ToString();
            string msub = "Approve Leave application";
            string msgbody = "Your leave has been approved";
           
            if (Gender == "M")
                atn = "Dear Mr " + rname + ",";
            else if (Gender == "F")
                atn = "Dear Ms " + rname + ",";
            else
                atn = "Dear Applicant,";

            string mbody = arrange_data(Session["empcode"].ToString(), rname, msgbody, atn);            
            string ccid = "";
            //Sendmail(sid, sname, rid, rname, ccid, msub, mbody);
            clsStatic.Sendmail(sid, sname, rid, rname, ccid, msub, mbody, ConnectionStr);  
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
            DataTable dts = DataProcess.GetData(ConnectionStr, "select a.EmpName,b.Emp_Mas_Remarks from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id where Emp_Mas_Emp_Id='" + Session["ActingPersonID"] + "'");
            DataTable dtr = DataProcess.GetData(ConnectionStr, "select c.Emp_Mas_Remarks,b.EmpName,c.Emp_Mas_Gender from ProcessAccessPermission a inner join Emp_Details b on a.AccessId=b.EmpID"
                                                              + " inner join hrms_emp_mas c on c.Emp_Mas_Emp_Id=a.AccessId"
                                                              + " where a.ProcessId='P001' and a.ProcessFlowId=1 and a.ProcessLevelid=" + levelid + " and a.ApplicantID='" + Session["empcode"] + "'");

            string atn = "";
            string Gender = dtr.Rows[0]["Emp_Mas_Gender"].ToString();
            string sid = dts.Rows[0]["Emp_Mas_Remarks"].ToString();
            string sname = dts.Rows[0]["EmpName"].ToString();
            string rid = dtr.Rows[0]["Emp_Mas_Remarks"].ToString();
            string rname = dtr.Rows[0]["EmpName"].ToString();
            string msub = "Leave application";
            atn = "Dear Sir,";         
            string msgbody = "I have sent a leave application for your approval";
            string mbody = arrange_data(Session["ApplicantID"].ToString(), sname, msgbody, atn);
            //string mbody = "I have sent a leave application for your approval";
            string ccid = "";
            //Sendmail(sid, sname, rid, rname, ccid, msub, mbody);
            clsStatic.Sendmail(sid, sname, rid, rname, ccid, msub, mbody, ConnectionStr);  
            
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    private void SendMailforReject()
    {
        try
        {
            // mail send for Reject
           
            DataTable dts = DataProcess.GetData(ConnectionStr, "select a.EmpName,b.Emp_Mas_Remarks from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id where Emp_Mas_Emp_Id='" + Session["ActingPersonID"] + "'");
            DataTable dtr = DataProcess.GetData(ConnectionStr, "select a.EmpName,b.Emp_Mas_Remarks,b.Emp_Mas_Gender from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id where Emp_Mas_Emp_Id='" + Session["empcode"] + "'");


            string atn = "";
            string Gender = dtr.Rows[0]["Emp_Mas_Gender"].ToString();
            string sid = dts.Rows[0]["Emp_Mas_Remarks"].ToString();
            string sname = dts.Rows[0]["EmpName"].ToString();
            string rid = dtr.Rows[0]["Emp_Mas_Remarks"].ToString();
            string rname = dtr.Rows[0]["EmpName"].ToString();
            string msub = "Reject Leave application";
            string msgbody = "Your leave has been been rejected";
            
            if (Gender == "M")
                atn = "Dear Mr " + rname + ",";
            else if (Gender == "F")
                atn = "Dear Ms " + rname + ",";
            else
                atn = "Dear Applicant,";


            string mbody = arrange_data(Session["empcode"].ToString(), rname, msgbody, atn);            
            string ccid = "";
            //Sendmail(sid, sname, rid, rname, ccid, msub, mbody);
            clsStatic.Sendmail(sid, sname, rid, rname, ccid, msub, mbody, ConnectionStr);  
            //

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    private void SendMailforReturn()
    {
        try
        {
            // mail send for Return

            DataTable dts = DataProcess.GetData(ConnectionStr, "select a.EmpName,b.Emp_Mas_Remarks from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id where Emp_Mas_Emp_Id='" + Session["ActingPersonID"] + "'");
            DataTable dtr = DataProcess.GetData(ConnectionStr, "select a.EmpName,b.Emp_Mas_Remarks,b.Emp_Mas_Gender from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id where Emp_Mas_Emp_Id='" + Session["empcode"] + "'");

            string atn="";
            string Gender = dtr.Rows[0]["Emp_Mas_Gender"].ToString();
            string sid = dts.Rows[0]["Emp_Mas_Remarks"].ToString();
            string sname = dts.Rows[0]["EmpName"].ToString();
            string rid = dtr.Rows[0]["Emp_Mas_Remarks"].ToString();
            string rname = dtr.Rows[0]["EmpName"].ToString();
            string msub = "Return leave application";
            string msgbody = "Your leave has been been returned. Correct your leave application and send again";
            if (Gender == "M")
                atn = "Dear Mr " + rname + ",";
            else if (Gender == "F")
                atn = "Dear Ms " + rname + ",";
            else
                atn = "Dear Applicant,";

            string mbody = arrange_data(Session["empcode"].ToString(), rname, msgbody, atn);

            //string mbody = "Your leave has been been returned. Correct your leave application and send again";

            string ccid = "";
            //Sendmail(sid, sname, rid, rname, ccid, msub, mbody);
            clsStatic.Sendmail(sid, sname, rid, rname, ccid, msub, mbody, ConnectionStr);  
            //

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }


    protected void btnApply_Click(object sender, EventArgs e)
    {
        SqlConnection myConnection = new SqlConnection(ConnectionStr);
        myConnection.Open();

        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;
        SqlDataAdapter sqlDataAdapterObj = null;
        string retValue = "";
        string msg = "";
        

        double rCL =0;
        double rSL =0;
        double rEL =0;


        for (int j = 0; j < gdvLeaveInfo.Rows.Count; j++)
        {
            if (gdvLeaveInfo.Rows[j].Cells[0].Text == "CL")
            {
                rCL = Convert.ToDouble(gdvLeaveInfo.Rows[j].Cells[4].Text);
            }
            else if (gdvLeaveInfo.Rows[j].Cells[0].Text == "SL")
            {
                rSL = Convert.ToDouble(gdvLeaveInfo.Rows[j].Cells[4].Text);
            }
            if (gdvLeaveInfo.Rows[j].Cells[0].Text == "EL")
            {
                rEL = Convert.ToDouble(gdvLeaveInfo.Rows[j].Cells[4].Text);
            }

        }


        if (CheckLeaveValidation(rCL, rSL, rEL).Split('@')[1] == "No")
        {
            msg = "Message:" + "\n" + CheckLeaveValidation(rCL, rSL, rEL).Split('@')[0];
            System.Windows.Forms.MessageBox.Show(msg);
            return;
        }
       

        myTrans = myConnection.BeginTransaction("SaveAllTransaction");

        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;


        try
        {

            List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();

            for (int i = 0; i < GridViewLeave.Rows.Count; i++)
            {
                string ProcessID =PID;
                string FlowID =FID;
                int levelid = 0;
                string ApplicantID = Session["ApplicantID"].ToString();
                string Transactionno = "T130800001";

                CheckBox chkbox = GridViewLeave.Rows[i].FindControl("Checklv") as CheckBox;

                if (chkbox.Checked)
                {

                    LeaveProcessHeader offphdr = new LeaveProcessHeader();

                    TextBox tb = GridViewLeave.Rows[i].FindControl("txtlvRemarks") as TextBox;
                    DropDownList dpllvno = GridViewLeave.Rows[i].FindControl("dpllday") as DropDownList;
                    DropDownList dpllvtype = GridViewLeave.Rows[i].FindControl("lvtype") as DropDownList;
                    Label lblSL = GridViewLeave.Rows[i].FindControl("lblSL") as Label;
                    Label lblDate = GridViewLeave.Rows[i].FindControl("lblDate") as Label;
                    Label lblIntime = GridViewLeave.Rows[i].FindControl("lblIntime") as Label;
                    Label lblOutTime = GridViewLeave.Rows[i].FindControl("lblOutTime") as Label;
                    Label lblHours = GridViewLeave.Rows[i].FindControl("lblHours") as Label;

                    offphdr.TransactionNo = Transactionno;
                    offphdr.ApplicantId = ApplicantID;
                    offphdr.ProcessId =PID;
                    offphdr.FlowId =FID;
                    offphdr.ProcesslevelId = 1;
                    offphdr.ProcessnextlevelId = 1;
                    offphdr.ActiontypeId = 1;
                    offphdr.SL = Convert.ToInt32(lblSL.Text);
                    offphdr.ClaiminDdate = Convert.ToDateTime(lblDate.Text);
                    offphdr.SysIndate = Convert.ToDateTime(lblDate.Text);
                    offphdr.SysOutdate = Convert.ToDateTime(lblDate.Text);
                    offphdr.ActIndate = Convert.ToDateTime(lblDate.Text);
                    offphdr.ActOutdate = Convert.ToDateTime(lblDate.Text);
                    offphdr.SysIntime = lblIntime.Text == "&nbsp;" ? "00:00 AM" : lblIntime.Text;
                    offphdr.SysOuttime = lblOutTime.Text == "&nbsp;" ? "00:00 AM" : lblOutTime.Text;
                    offphdr.SysTotalhrs = lblHours.Text == "&nbsp;" ? "00:00" : lblHours.Text;
                    offphdr.NoofLeave = Convert.ToDouble(dpllvno.SelectedItem.Value);
                    offphdr.Leavetype = dpllvtype.SelectedItem.Value;
                    offphdr.Leavedescription = dpllvtype.SelectedItem.Text;
                    offphdr.Remarks = tb.Text;
                    offphdr.Acthrs = "00:00";
                                        
                    lvphdrlst.Add(offphdr);
                }
            }

            LeaveProcess lvproc = new LeaveProcess();

            string retval = lvproc.SaveInitiateLeaveProcessData(lvphdrlst, myCommand);

           
            if (retval == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }

            LoadAllInformation();

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

    private string CheckLeaveValidation(double rCL,double rSL, double rEL)
    {
        string msg = "You have remaining ";
        string msg1 = "Yes";

        double CL=0;
        double SL = 0;
        double EL = 0;
        double nooflv = 0;
        string lvtype="";
        string lvtypeselect = "";

        for (int i = 0; i < GridViewLeave.Rows.Count; i++)
        {                    
            
            CheckBox chkbox = GridViewLeave.Rows[i].FindControl("Checklv") as CheckBox;

            if (chkbox.Checked)
            {                            
                DropDownList dpllvno = GridViewLeave.Rows[i].FindControl("dpllday") as DropDownList;
                DropDownList dpllvtype = GridViewLeave.Rows[i].FindControl("lvtype") as DropDownList;

                lvtype=dpllvtype.SelectedItem.Value;
                nooflv = Convert.ToDouble(dpllvno.SelectedItem.Value);

                if (lvtype == "")
                {
                    lvtypeselect = "NA"; 
                }

                if (nooflv.ToString() == "0")
                {
                    lvtypeselect = "NA";
                }

                if (lvtype == "CL")
                {
                    CL = CL + nooflv;
                }
                else if (lvtype == "SL")
                {
                    SL = SL + nooflv;
                }
                else if (lvtype == "EL")
                {
                    EL = EL + nooflv;
                }             
                
            }
        }

        if (lvtypeselect == "NA")
        {
            msg = "Please select leave type and leave days then apply";
            msg1 = "No";
        }
        else
        {

            if (CL > rCL)
            {
                msg = msg + "CL:" + rCL + " but you are going to apply " + CL + ";";
                msg1 = "No";
            }
            if (SL > rSL)
            {
                msg = msg + "SL:" + rCL + " but you are going to apply " + SL + ";";
                msg1 = "No";
            }
            if (EL > rEL)
            {
                msg = msg + "EL:" + rCL + " but you are going to apply " + EL + ";";
                msg1 = "No";
            }
        }

        msg = msg +"@" + msg1;

        return msg;

    }

    private void LoadAllInformation()
    {
        DateTime fDate = Convert.ToDateTime("01/01/2013");
        DateTime lDate = Convert.ToDateTime("31/01/2013");
                      
        LoadpendingLeaveApplication(Session["ActingPersonID"].ToString(),PID,FID);

       // LoadAttendanceDetailsemployeeID(Session["ApplicantID"].ToString(), "P001", "F001", fDate, lDate);
       // LoadLeavBALeByemployeeID(Session["ApplicantID"].ToString(), fDate);
       // LoadEmployeeInformation(Session["ApplicantID"].ToString());
        lblPeriod.Text = FinanialPeriod(fDate);
        lblcurrentPeriod.Text = DateTime.Now.ToString();
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
            for (int i = 0; i < GridViewLeave.Rows.Count; i++)
            {
                string ProcessID =PID;
                string FlowID =FID;
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId =2;  // forward


                CheckBox chkbox = GridViewLeave.Rows[i].FindControl("Checklv") as CheckBox;

                if (chkbox.Checked)
                {
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();
                    TextBox tb = GridViewLeave.Rows[i].FindControl("txtlvRemarks") as TextBox;
                    DropDownList dpllvno = GridViewLeave.Rows[i].FindControl("dpllday") as DropDownList;
                    DropDownList dpllvtype = GridViewLeave.Rows[i].FindControl("lvtype") as DropDownList;
                    Label lblTransactionNo = GridViewLeave.Rows[i].FindControl("lblTransactionNo") as Label;
                    Label lblTransactionNoLineNo = GridViewLeave.Rows[i].FindControl("lblTransactionNoLineNo") as Label;
                    Label lblProcessLevelid = GridViewLeave.Rows[i].FindControl("lblProcessLevelid") as Label;
                    Label lblSL = GridViewLeave.Rows[i].FindControl("lblSL") as Label;
                    Label lblDate = GridViewLeave.Rows[i].FindControl("lblDate") as Label;
                    Label lblIntime = GridViewLeave.Rows[i].FindControl("lblIntime") as Label;
                    Label lblOutTime = GridViewLeave.Rows[i].FindControl("lblOutTime") as Label;
                    Label lblHours = GridViewLeave.Rows[i].FindControl("lblHours") as Label;

                    offphdr.TransactionNo = Convert.ToString(lblTransactionNo.Text);
                    offphdr.TransactionLineNo = Convert.ToInt32(lblTransactionNoLineNo.Text);
                    offphdr.ApplicantId = Session["empcode"].ToString();
                    offphdr.ProcessId = ProcessID;
                    offphdr.FlowId = FlowID;
                    offphdr.ProcesslevelId = Convert.ToInt32(lblProcessLevelid.Text);
                    levelid = offphdr.ProcesslevelId + 1;  // To find next level id
                    offphdr.ProcessnextlevelId = 1;    
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.SL = Convert.ToInt32(lblSL.Text);
                    offphdr.ClaiminDdate = Convert.ToDateTime(lblDate.Text);
                    offphdr.SysIndate = Convert.ToDateTime(lblDate.Text);
                    offphdr.SysOutdate = Convert.ToDateTime(lblDate.Text);
                    offphdr.ActIndate = Convert.ToDateTime(lblDate.Text);
                    offphdr.ActOutdate = Convert.ToDateTime(lblDate.Text);
                    offphdr.SysIntime = lblIntime.Text == "&nbsp;" ? "00:00 AM" : lblIntime.Text;
                    offphdr.SysOuttime = lblOutTime.Text == "&nbsp;" ? "00:00 AM" : lblOutTime.Text;
                    offphdr.SysTotalhrs = lblHours.Text == "&nbsp;" ? "00:00" : lblHours.Text;
                    offphdr.NoofLeave = Convert.ToDouble(dpllvno.SelectedItem.Value);
                    offphdr.Leavetype = dpllvtype.SelectedItem.Value;
                    offphdr.Leavedescription = dpllvtype.SelectedItem.Text;
                    
                    offphdr.Acthrs = "00:00";
                    offphdr.ActingpersonId = ActingPersonId;

                    offphdr.Remarks = txtRemarks.Text.Replace("'", "") == "" ? "NA" : txtRemarks.Text.Replace("'", "");

                    lvphdrlst.Add(offphdr);
                }
            }

            LeaveProcess lvproc = new LeaveProcess();

            string retval = lvproc.SaveForwardLeaveProcessData(lvphdrlst, myCommand);
                                  
            if (retval == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }

            LoadAllInformation();

            ReloadAllgridAfterForwardRejectApprove();

            if (retval != "")
            {
                SendMailforForword(levelid);
            }

        }
        catch (Exception ex)
        {
            
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
        myTrans = myConnection.BeginTransaction("SaveAllTransaction");

        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        
        try
        {

            List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();

            for (int i = 0; i < GridViewLeave.Rows.Count; i++)
            {
                string ProcessID =PID;
                string FlowID =FID;
                int levelid = 0;
                          
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId = 4;          // 97 for reject

                CheckBox chkbox = GridViewLeave.Rows[i].FindControl("Checklv") as CheckBox;

                if (chkbox.Checked)
                {

                    LeaveProcessHeader offphdr = new LeaveProcessHeader();

                    TextBox tb = GridViewLeave.Rows[i].FindControl("txtlvRemarks") as TextBox;
                    DropDownList dpllvno = GridViewLeave.Rows[i].FindControl("dpllday") as DropDownList;
                    DropDownList dpllvtype = GridViewLeave.Rows[i].FindControl("lvtype") as DropDownList;
                    Label lblTransactionNo = GridViewLeave.Rows[i].FindControl("lblTransactionNo") as Label;
                    Label lblTransactionNoLineNo = GridViewLeave.Rows[i].FindControl("lblTransactionNoLineNo") as Label;
                    Label lblProcessLevelid = GridViewLeave.Rows[i].FindControl("lblProcessLevelid") as Label;
                    Label lblSL = GridViewLeave.Rows[i].FindControl("lblSL") as Label;
                    Label lblDate = GridViewLeave.Rows[i].FindControl("lblDate") as Label;
                    Label lblIntime = GridViewLeave.Rows[i].FindControl("lblIntime") as Label;
                    Label lblOutTime = GridViewLeave.Rows[i].FindControl("lblOutTime") as Label;
                    Label lblHours = GridViewLeave.Rows[i].FindControl("lblHours") as Label;

                    offphdr.TransactionNo = Convert.ToString(lblTransactionNo.Text);
                    offphdr.TransactionLineNo = Convert.ToInt32(lblTransactionNoLineNo.Text);
                    offphdr.ApplicantId = Session["empcode"].ToString();
                    offphdr.ProcessId = ProcessID;
                    offphdr.FlowId = FlowID;
                    offphdr.ProcesslevelId = Convert.ToInt32(lblProcessLevelid.Text);
                    offphdr.ProcessnextlevelId = 1;
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.SL = Convert.ToInt32(lblSL.Text);
                    offphdr.ClaiminDdate = Convert.ToDateTime(lblDate.Text);
                    offphdr.SysIndate = Convert.ToDateTime(lblDate.Text);
                    offphdr.SysOutdate = Convert.ToDateTime(lblDate.Text);
                    offphdr.ActIndate = Convert.ToDateTime(lblDate.Text);
                    offphdr.ActOutdate = Convert.ToDateTime(lblDate.Text);
                    offphdr.SysIntime = lblIntime.Text == "&nbsp;" ? "00:00 AM" : lblIntime.Text;
                    offphdr.SysOuttime = lblOutTime.Text == "&nbsp;" ? "00:00 AM" : lblOutTime.Text;
                    offphdr.SysTotalhrs = lblHours.Text == "&nbsp;" ? "00:00" : lblHours.Text;
                    offphdr.NoofLeave = Convert.ToDouble(dpllvno.SelectedItem.Value);
                    offphdr.Leavetype = dpllvtype.SelectedItem.Value;
                    offphdr.Leavedescription = dpllvtype.SelectedItem.Text;
                    offphdr.Remarks = txtRemarks.Text.Replace("'", "") == "" ? "NA" : txtRemarks.Text.Replace("'", "");
                    offphdr.Acthrs = "00:00";
                    offphdr.ActingpersonId = ActingPersonId;
                    
                    lvphdrlst.Add(offphdr);
                }
            }

            LeaveProcess lvproc = new LeaveProcess();

            string retval = lvproc.SaveRejectLeaveProcessData(lvphdrlst, myCommand);
                       

            if (retval == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }

            if (retval != "")
            {
                SendMailforReject();
            }

            LoadAllInformation();

            ReloadAllgridAfterForwardRejectApprove();

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
    protected void btnPostLeave_Click(object sender, EventArgs e)
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
                string ProcessID =PID;
                string FlowID =FID;
                int levelid = 0;
                string ApplicantID = "L3T593";

                CheckBox chkbox = GridViewLeave.Rows[i].FindControl("Checklv") as CheckBox;

                if (chkbox.Checked)
                {

                    LeaveProcessHeader offphdr = new LeaveProcessHeader();

                    TextBox tb = GridViewLeave.Rows[i].FindControl("txtlvRemarks") as TextBox;
                    DropDownList dpllvno = GridViewLeave.Rows[i].FindControl("dpllday") as DropDownList;
                    DropDownList dpllvtype = GridViewLeave.Rows[i].FindControl("lvtype") as DropDownList;
                    Label lblSL = GridViewLeave.Rows[i].FindControl("lblSL") as Label;
                    Label lblDate = GridViewLeave.Rows[i].FindControl("lblDate") as Label;
                    Label lblIntime = GridViewLeave.Rows[i].FindControl("lblIntime") as Label;
                    Label lblOutTime = GridViewLeave.Rows[i].FindControl("lblOutTime") as Label;
                    Label lblHours = GridViewLeave.Rows[i].FindControl("lblHours") as Label;

                    offphdr.ApplicantId = ApplicantID;
                    offphdr.SL = Convert.ToInt32(lblSL.Text);
                    offphdr.ClaiminDdate = Convert.ToDateTime(lblDate.Text);
                    offphdr.SysIndate = Convert.ToDateTime(lblDate.Text);
                    offphdr.SysOutdate = Convert.ToDateTime(lblDate.Text);
                    offphdr.ActIndate = Convert.ToDateTime(lblDate.Text);
                    offphdr.ActOutdate = Convert.ToDateTime(lblDate.Text);
                    offphdr.SysIntime = lblIntime.Text;
                    offphdr.SysOuttime = lblOutTime.Text;
                    offphdr.SysTotalhrs = lblHours.Text;

                    offphdr.NoofLeave = Convert.ToDouble(dpllvno.SelectedItem.Value);
                    offphdr.Leavetype = dpllvtype.SelectedItem.Value;
                    offphdr.Leavedescription = dpllvtype.SelectedItem.Text;

                    offphdr.Remarks = tb.Text;

                    offphdr.Acthrs = "00:00";

                    lvphdrlst.Add(offphdr);
                }
            }

            LeaveProcess lvproc = new LeaveProcess();

            string retval = lvproc.SaveLeaveData(lvphdrlst, myCommand);

            if (retval == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
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
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        lblcurrentPeriod.Text = DateTime.Now.ToString();
    }
    protected void btnprevious_Click(object sender, EventArgs e)
    {
        DateTime fDate = Convert.ToDateTime("01/12/2012");
        DateTime lDate = Convert.ToDateTime("31/12/2013");

       
        LoadAttendanceDetailsemployeeID(Session["ApplicantID"].ToString(),PID,FID, fDate, lDate);
        LoadLeavBALeByemployeeID(Session["ApplicantID"].ToString(), fDate);
        LoadEmployeeInformation(Session["ApplicantID"].ToString());

        //PanelLeaveHdr.Visible=false; 

        lblPeriod.Text = FinanialPeriod(fDate);
        lblcurrentPeriod.Text = DateTime.Now.ToString();

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

        LoadActionPermissionForApprovalPerson(Session["ActingPersonID"].ToString(), Session["empcode"].ToString(),PID,FID, Convert.ToInt32(Session["plevelid"].ToString()));

    }
    protected void gdvLeaveInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false; 
    }
    private string  CheckValidation()
    {
        const string checkValidation = "";
        if (txtFromDate.Text == string.Empty)
        {
            txtFromDate.Focus();
            return "Select From Date Correctly !";
        }
        if (txtToDate.Text == string.Empty)
        {
            txtToDate.Focus();
            return "Select To Date Correctly  !";
        }
        return checkValidation;
 
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckValidation();
            switch (validationMsg)
            {
                case "":
                {
                    DateTime fDate = Convert.ToDateTime(txtFromDate.Text);
                    DateTime lDate = Convert.ToDateTime(txtToDate.Text);
                    DataTable dtForPreviousLeave = LoadLeaveByemployeeID(Session["empcode"].ToString(), PID, FID, fDate, lDate);
                    if (dtForPreviousLeave.Rows.Count > 0)
                    {
                        GridViewLeaveForPreviousRecord.DataSource = dtForPreviousLeave;
                        GridViewLeaveForPreviousRecord.DataBind();
                        PanelForShowPreviousLeave_ModalPopupExtender.Show();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(
                       this,
                       GetType(),
                       "MessageBox",
                       "alert(' No Data Found !');",
                       true);   
                    }
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
    protected void btnShowALL_Click(object sender, EventArgs e)
    {
        DateTime fDate = Convert.ToDateTime(txtFromDate.Text);
        DateTime lDate = Convert.ToDateTime(txtToDate.Text);
        LoadAttendanceDetailsemployeeID(Session["empcode"].ToString(),PID,FID, fDate, lDate);
    }

    protected void GridViewLeavePending_SelectedIndexChanged(object sender, EventArgs e)
    {
        int indx = GridViewLeavePending.SelectedIndex;
        if (indx == -1)
        {
            return;
        }
        Session["referance"] = GridViewLeavePending.Rows[indx].Cells[1].Text;
        Session["empcode"] = GridViewLeavePending.Rows[indx].Cells[2].Text;
        Session["LeaveCode"] = GridViewLeavePending.Rows[indx].Cells[5].Text;
        Session["plevelid"] = GridViewLeavePending.Rows[indx].Cells[8].Text;
        ReloadAllgridAfterForwardRejectApprove();
        btnPostLeave.Visible = false;
        btnApply.Visible = false;       
        btnForward.Visible = false;      
        btnReturn.Visible = false;        
        btnReject.Visible = false;        
        btnApprove.Visible = false;
        LoadActionPermissionForApprovalPerson(Session["ActingPersonID"].ToString(), Session["empcode"].ToString(),PID,FID, Convert.ToInt32(Session["plevelid"].ToString()));
       
    }

    private void ReloadAllgridAfterForwardRejectApprove()
    {
        DateTime fDate = Convert.ToDateTime(Session["fdate"].ToString());
        LoadPendingLeaveByemployeeID(Session["referance"].ToString(),PID,FID, Session["ActingPersonID"].ToString(), Session["LeaveCode"].ToString());        
        LoadLeavBALeByemployeeID(Session["empcode"].ToString(), fDate);
        LoadEmployeeInformation(Session["empcode"].ToString());
        DataTable dt = DataProcess.GetData(ConnectionStr, "select distinct b.EmpName from [ProcessFlowdet] a inner join Emp_Details b on a.ResponsiblePerson=b.EmpID  where TransactionNo='" + Session["referance"] + "'");
        if (dt.Rows.Count > 0)
        {
            lblResponsibleperson.Text = dt.Rows[0]["EmpName"].ToString();
        }
        LoadPendingLeaveRemarksByemployeeID(Session["referance"].ToString(),PID,FID, Session["ActingPersonID"].ToString(), Session["LeaveCode"].ToString());
        if (GridViewLeave.Rows.Count < 1)
        {
            Panel50.Visible = false;
        }
        else
        {
            Panel50.Visible = true;
            ucLeaveDocument1.LoadUploadFileByRef(Session["referance"].ToString());
        }
    }

    private void LoadActionPermissionForApprovalPerson(string actingpersonid, string empcode, string Processid, string flowid,int plevelid)
    {
        DataTable dt = new DataTable();
 
        LeaveProcess lvp = new LeaveProcess();

        dt = lvp.GetApprovalPermission(ConnectionStr, actingpersonid, empcode, Processid, flowid, plevelid);

        foreach (DataRow dr in dt.Rows)
        {
            if (dr["Part"].ToString() == "1")
            {
                btnApply.Visible=true;
            }
            else if (dr["Part"].ToString() == "2")
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
    protected void btnReturn_Click(object sender, EventArgs e)
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
                string ProcessID =PID;
                string FlowID =FID;
                int levelid = 0;
              
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId = 3;  // Return


                CheckBox chkbox = GridViewLeave.Rows[i].FindControl("Checklv") as CheckBox;

                if (chkbox.Checked)
                {

                    LeaveProcessHeader offphdr = new LeaveProcessHeader();

                    TextBox tb = GridViewLeave.Rows[i].FindControl("txtlvRemarks") as TextBox;
                    DropDownList dpllvno = GridViewLeave.Rows[i].FindControl("dpllday") as DropDownList;
                    DropDownList dpllvtype = GridViewLeave.Rows[i].FindControl("lvtype") as DropDownList;
                    Label lblTransactionNo = GridViewLeave.Rows[i].FindControl("lblTransactionNo") as Label;
                    Label lblTransactionNoLineNo = GridViewLeave.Rows[i].FindControl("lblTransactionNoLineNo") as Label;
                    Label lblProcessLevelid = GridViewLeave.Rows[i].FindControl("lblProcessLevelid") as Label;
                    Label lblSL = GridViewLeave.Rows[i].FindControl("lblSL") as Label;
                    Label lblDate = GridViewLeave.Rows[i].FindControl("lblDate") as Label;
                    Label lblIntime = GridViewLeave.Rows[i].FindControl("lblIntime") as Label;
                    Label lblOutTime = GridViewLeave.Rows[i].FindControl("lblOutTime") as Label;
                    Label lblHours = GridViewLeave.Rows[i].FindControl("lblHours") as Label;

                    offphdr.TransactionNo = Convert.ToString(lblTransactionNo.Text);
                    offphdr.TransactionLineNo = Convert.ToInt32(lblTransactionNoLineNo.Text);
                    offphdr.ApplicantId = Session["empcode"].ToString();
                    offphdr.ProcessId = ProcessID;
                    offphdr.FlowId = FlowID;
                    offphdr.ProcesslevelId = Convert.ToInt32(lblProcessLevelid.Text);
                    offphdr.ProcessnextlevelId = 1;
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.SL = Convert.ToInt32(lblSL.Text);
                    offphdr.ClaiminDdate = Convert.ToDateTime(lblDate.Text);
                    offphdr.SysIndate = Convert.ToDateTime(lblDate.Text);
                    offphdr.SysOutdate = Convert.ToDateTime(lblDate.Text);
                    offphdr.ActIndate = Convert.ToDateTime(lblDate.Text);
                    offphdr.ActOutdate = Convert.ToDateTime(lblDate.Text);
                    offphdr.SysIntime = lblIntime.Text == "&nbsp;" ? "00:00 AM" : lblIntime.Text;
                    offphdr.SysOuttime = lblOutTime.Text == "&nbsp;" ? "00:00 AM" : lblOutTime.Text;
                    offphdr.SysTotalhrs = lblHours.Text == "&nbsp;" ? "00:00" : lblHours.Text;
                    offphdr.NoofLeave = Convert.ToDouble(dpllvno.SelectedItem.Value);
                    offphdr.Leavetype = dpllvtype.SelectedItem.Value;
                    offphdr.Leavedescription = dpllvtype.SelectedItem.Text;
                    offphdr.Remarks = tb.Text;
                    offphdr.Acthrs = "00:00";
                    offphdr.ActingpersonId = ActingPersonId;


                    lvphdrlst.Add(offphdr);
                }
            }

            LeaveProcess lvproc = new LeaveProcess();

            string retval = lvproc.SaveReturnLeaveProcessData(lvphdrlst, myCommand);

            if (retval == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }

            if (retval != "")
            {
                SendMailforReturn();
            }

            LoadAllInformation();

            ReloadAllgridAfterForwardRejectApprove();


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
    protected void GridViewLeavePending_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewLeavePending.PageIndex = e.NewPageIndex;
        LoadpendingLeaveApplication(Session["ActingPersonID"].ToString(),PID,FID);       

    }

    private bool Sendmail(string sid,string sname,string rid,string rname,string ccid,string msub,string mbody)
    {                      
        bool flg = false;
     
        try
        
        {
            SmtpClient smtp = new SmtpClient("mail.link3.net");
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress(sid, sname);           
            msg.To.Add(new MailAddress(rid));

            string[] cary = ccid.Split(':');

            if (cary.Length >= 1)
            {
                for (int i = 0; i < cary.Length; i++)
                {
                    if (cary[i].Trim() != "")
                    {
                        msg.CC.Add(new MailAddress(cary[i].Trim()));
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
        str += "\n\n" + body;
        str += "\n\nEmployee ID : " + eid;
        str += "\nName          : " + sname;
        str += "\n\n\nTo view detail or update this request click the link bellow:\n";
        str += "http://office.link3.net/login";
        str += "\n\n\n";
        str += "This is auto generated mail from HRIS and Payroll";
        return str;        
    }

    protected void GridViewLeaveForPreviousRecord_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
            //e.Row.BackColor = Color.LightGreen;
        }
        e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
        e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
        e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
    }
    protected void GridViewLeavePending_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void GridViewLeave_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
