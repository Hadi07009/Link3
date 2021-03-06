﻿using System;
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

public partial class modules_HRMS_Approval_frmEarnLeaveApplicationApproval : System.Web.UI.Page
{

    string ConnectionStr = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    string PID ="P008";
    string FID = "8";

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
                      
            dttask = DataProcess.GetData(ConnectionStr, "select * from ProcessTaskDateAllemoloyee where TaskType='EL' and [Status]='Y'");

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
            Session["EntryUserid"] = "";

            //Session["ActingPersonID"] = Session[StaticData.sessionUserId].ToString();
            //Session["EntryUserid"] = Session[StaticData.sessionUserId].ToString();

            Session["fdate"] = fDate;
            Session["lDate"] = lDate;
           
            LoadpendingELApplication(Session["ActingPersonID"].ToString(), PID, FID);  
           
            lblPeriod.Text = FinanialPeriod(fDate);
            
            lblcurrentPeriod.Text = Convert.ToString(System.DateTime.Now.Date.Day) + "-" + Convert.ToString(System.DateTime.Now.Date.Month) + "-" + Convert.ToString(System.DateTime.Now.Date.Year);

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

    private void LoadLeaveByemployeeID(string empid, string Processid, string flowid, DateTime fDate, DateTime lDate)
    {          

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();     
        SqlConnection sqlConn = null;        
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();        
        cmd.Connection = sqlConn;
        
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetLeaveStatusByEmpid";

        cmd.Parameters.Add(new SqlParameter("@fDate", SqlDbType.DateTime)).Value = fDate;
        cmd.Parameters.Add(new SqlParameter("@lDate ", SqlDbType.DateTime)).Value = lDate;
        cmd.Parameters.Add(new SqlParameter("@empid", SqlDbType.NVarChar)).Value = empid.ToString();
        cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid.ToString();
        cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid.ToString();

        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);       
        da.Fill(dt);

        GridViewLeave.DataSource = dt;
        GridViewLeave.DataBind();
    }

    private void LoadPendingOffdayByemployeeID(string transactionno, string Processid, string flowid,string actingpersonid,string leavecode)
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
        cmd.Parameters.Add(new SqlParameter("@empid", SqlDbType.NVarChar)).Value = empid.ToString();
        cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid.ToString();
        cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid.ToString();

        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        GridViewLeave.DataSource = dt;
        GridViewLeave.DataBind();
    }

    
    private void LoadpendingOffdayApplication(string actingpersonid, string Processid, string flowid)
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetAllPendingEarnedLeave";

        cmd.Parameters.Add(new SqlParameter("@actempid", SqlDbType.NVarChar)).Value = actingpersonid.ToString();
        cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid.ToString();
        cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid.ToString();

        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        GridViewLeavePending.DataSource = dt;
        GridViewLeavePending.DataBind();
        //GridViewLeavePending.Columns[8].Visible = false;
        //GridViewLeavePending.Columns[5].Visible = false;


        string gridhieght = GridViewLeavePending.Height.Value.ToString();
        
        int gdr = GridViewLeavePending.Rows.Count;     
        CollapsiblePanelExtenderSrch.ExpandedSize =(gdr*40)+25;     

    }
    private void LoadpendingELApplication(string actingpersonid, string Processid, string flowid)
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetAllPendingEarnedLeave";

        cmd.Parameters.Add(new SqlParameter("@actempid", SqlDbType.NVarChar)).Value = actingpersonid.ToString();
        cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid.ToString();
        cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid.ToString();

        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        GridViewLeavePending.DataSource = dt;
        GridViewLeavePending.DataBind();
        //GridViewLeavePending.Columns[8].Visible = false;
        //GridViewLeavePending.Columns[5].Visible = false;


        string gridhieght = GridViewLeavePending.Height.Value.ToString();
        
        int gdr = GridViewLeavePending.Rows.Count;     
        CollapsiblePanelExtenderSrch.ExpandedSize =(gdr*40)+25;     

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
    protected void GridViewLeave_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void GridViewLeavePending_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlActionType = (DropDownList)e.Row.FindControl("ddlLoadPermission");
            string empcode = e.Row.Cells[2].Text;
            int plevelid = Convert.ToInt32(e.Row.Cells[9].Text);
            DataTable dt = LoadActionPermissionIntoDropDownList(Session["ActingPersonID"].ToString(), empcode, PID, FID, plevelid);
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
            //for (int i = 0; i < 10; i++)
            //{
            //    e.Row.Cells[i].Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
            //    e.Row.Cells[i].Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
            //    e.Row.Cells[i].Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GridViewLeavePending, "Select$" + e.Row.RowIndex);
            //}
                           
        }

        e.Row.Cells[5].Visible = false;
        //e.Row.Cells[8].Visible = false; 
        e.Row.Cells[9].Visible = false; 

    }
    private DataTable LoadActionPermissionIntoDropDownList(string actingpersonid, string empcode, string Processid, string flowid, int plevelid)
    {
        DataTable dt = new DataTable();
        LeaveProcess lvp = new LeaveProcess();
        return dt = lvp.GetApprovalPermissionIntoDDDL(ConnectionStr, actingpersonid, empcode, Processid, flowid, plevelid);
    }

    protected void btnApprove_Click(object sender, EventArgs e)
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
                int ActionTypeId =5;  // Approve

                CheckBox chkbox = GridViewLeave.Rows[i].FindControl("Checklv") as CheckBox;

                if (chkbox.Checked == true)
                {

                    LeaveProcessHeader offphdr = new LeaveProcessHeader();

                    TextBox tb = GridViewLeave.Rows[i].FindControl("txtlvRemarks") as TextBox;
                    DropDownList dpllvno = GridViewLeave.Rows[i].FindControl("dpllday") as DropDownList;
                    DropDownList dpllvtype = GridViewLeave.Rows[i].FindControl("lvtype") as DropDownList;

                    offphdr.TransactionNo = Convert.ToString(GridViewLeave.Rows[i].Cells[27].Text.ToString());
                    offphdr.TransactionLineNo = Convert.ToInt32(GridViewLeave.Rows[i].Cells[28].Text.ToString());
                    offphdr.ApplicantId = Session["empcode"].ToString();
                    offphdr.ProcessId = ProcessID.ToString();
                    offphdr.FlowId = FlowID.ToString();
                    offphdr.ProcesslevelId = Convert.ToInt32(GridViewLeave.Rows[i].Cells[25].Text.ToString());
                    offphdr.ProcessnextlevelId = 1;
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.SL = Convert.ToInt32(GridViewLeave.Rows[i].Cells[0].Text.ToString());
                    offphdr.ClaiminDdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[3].Text.ToString());
                    offphdr.SysIndate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[2].Text.ToString());
                    offphdr.SysOutdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[3].Text.ToString());
                    offphdr.ActIndate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[2].Text.ToString());
                    offphdr.ActOutdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[3].Text.ToString());
                    offphdr.SysIntime = "";
                    offphdr.SysOuttime = "";
                    offphdr.SysTotalhrs = "";
                    offphdr.ActIntime = "";


                    offphdr.NoofLeave = Convert.ToDouble(GridViewLeave.Rows[i].Cells[20].Text.ToString());
                    offphdr.Leavetype = "EL";                  
                    offphdr.Remarks = txtRemarks.Text.Replace("'","") == "" ? "NA" : txtRemarks.Text.Replace("'","");
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
            string msub = "Approve Leave application";
            string msgbody = "Your leave has been approved";
            string atn = "Dear Mr/Ms " + rname + ",";
            //string mbody = arrange_data(Session["ApplicantID"].ToString(), rname, msgbody, atn);
            string mbody = msgbody.ToString();
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
            //Sendmail(sid, sname, rid, rname, ccid, msub, mbody);
            
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
            if (gdvLeaveInfo.Rows[j].Cells[0].Text.ToString() == "CL")
            {
                rCL = Convert.ToDouble(gdvLeaveInfo.Rows[j].Cells[4].Text.ToString());
            }
            else if (gdvLeaveInfo.Rows[j].Cells[0].Text.ToString() == "SL")
            {
                rSL = Convert.ToDouble(gdvLeaveInfo.Rows[j].Cells[4].Text.ToString());
            }
            if (gdvLeaveInfo.Rows[j].Cells[0].Text.ToString() == "EL")
            {
                rEL = Convert.ToDouble(gdvLeaveInfo.Rows[j].Cells[4].Text.ToString());
            }

        }


        if (CheckLeaveValidation(rCL, rSL, rEL).Split('@')[1].ToString() == "No")
        {
            msg = "Message:" + "\n" + CheckLeaveValidation(rCL, rSL, rEL).Split('@')[0].ToString();
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
                string ProcessID = "P001";
                string FlowID = "1";
                int levelid = 0;
                string ApplicantID = Session["ApplicantID"].ToString();
                string Transactionno = "T130800001";

                CheckBox chkbox = GridViewLeave.Rows[i].FindControl("Checklv") as CheckBox;

                if (chkbox.Checked == true)
                {

                    LeaveProcessHeader offphdr = new LeaveProcessHeader();

                    TextBox tb = GridViewLeave.Rows[i].FindControl("txtlvRemarks") as TextBox;
                    DropDownList dpllvno = GridViewLeave.Rows[i].FindControl("dpllday") as DropDownList;
                    DropDownList dpllvtype = GridViewLeave.Rows[i].FindControl("lvtype") as DropDownList;

                    offphdr.TransactionNo = Transactionno.ToString();
                    offphdr.ApplicantId = ApplicantID.ToString();
                    offphdr.ProcessId = "P001";
                    offphdr.FlowId = "F001";
                    offphdr.ProcesslevelId = 1;
                    offphdr.ProcessnextlevelId = 1;
                    offphdr.ActiontypeId = 1;
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
                                        
                    lvphdrlst.Add(offphdr);
                }
            }

            LeaveProcess lvproc = new LeaveProcess();

            string retval = lvproc.SaveInitiateLeaveProcessData(lvphdrlst, myCommand);

           
            if (retval.ToString() == "")
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

            if (chkbox.Checked == true)
            {                            
                DropDownList dpllvno = GridViewLeave.Rows[i].FindControl("dpllday") as DropDownList;
                DropDownList dpllvtype = GridViewLeave.Rows[i].FindControl("lvtype") as DropDownList;

                lvtype=dpllvtype.SelectedItem.Value.ToString();
                nooflv = Convert.ToDouble(dpllvno.SelectedItem.Value);

                if (lvtype.ToString() == "")
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
                msg = msg + "CL:" + rCL.ToString() + " but you are going to apply " + CL.ToString() + ";";
                msg1 = "No";
            }
            if (SL > rSL)
            {
                msg = msg + "SL:" + rCL.ToString() + " but you are going to apply " + SL.ToString() + ";";
                msg1 = "No";
            }
            if (EL > rEL)
            {
                msg = msg + "EL:" + rCL.ToString() + " but you are going to apply " + EL.ToString() + ";";
                msg1 = "No";
            }
        }

        msg = msg +"@" + msg1;

        return msg;

    }

    private void LoadAllInformation()
    {
        DateTime fDate = Convert.ToDateTime(Session["fdate"].ToString());
        DateTime lDate = Convert.ToDateTime(Session["lDate"].ToString());

        LoadpendingOffdayApplication(Session["ActingPersonID"].ToString(),PID,FID);

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

            for (int i = 0; i < GridViewLeave.Rows.Count; i++)
            {
                string ProcessID =PID;
                string FlowID =FID;
                
                              
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId =2;  // forward


                CheckBox chkbox = GridViewLeave.Rows[i].FindControl("Checklv") as CheckBox;

                if (chkbox.Checked == true)
                {

                    LeaveProcessHeader offphdr = new LeaveProcessHeader();

                    TextBox tb = GridViewLeave.Rows[i].FindControl("txtlvRemarks") as TextBox;
                    DropDownList dpllvno = GridViewLeave.Rows[i].FindControl("dpllday") as DropDownList;
                    DropDownList dpllvtype = GridViewLeave.Rows[i].FindControl("lvtype") as DropDownList;

                    offphdr.TransactionNo = Convert.ToString(GridViewLeave.Rows[i].Cells[27].Text.ToString());
                    offphdr.TransactionLineNo = Convert.ToInt32(GridViewLeave.Rows[i].Cells[28].Text.ToString());
                    offphdr.ApplicantId = Session["empcode"].ToString();
                    offphdr.ProcessId = ProcessID.ToString();
                    offphdr.FlowId = FlowID.ToString();
                    offphdr.ProcesslevelId = Convert.ToInt32(GridViewLeave.Rows[i].Cells[25].Text.ToString());
                    levelid = offphdr.ProcesslevelId + 1;  // To find next level id
                    offphdr.ProcessnextlevelId = 1;    
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.SL = Convert.ToInt32(GridViewLeave.Rows[i].Cells[0].Text.ToString());
                    offphdr.ClaiminDdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[3].Text.ToString());
                    offphdr.SysIndate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[2].Text.ToString());
                    offphdr.SysOutdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[3].Text.ToString());
                    offphdr.ActIndate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[2].Text.ToString());
                    offphdr.ActOutdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[3].Text.ToString());
                    offphdr.SysIntime = "";
                    offphdr.SysOuttime = "";
                    offphdr.SysTotalhrs = "";
                                       
                    offphdr.Acthrs = "00:00";
                    offphdr.ActingpersonId = ActingPersonId.ToString();

                    offphdr.Remarks = txtRemarks.Text.Replace("'", "") == "" ? "NA" : txtRemarks.Text.Replace("'", "");

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
                string FlowID = "2";
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
                    offphdr.Remarks = txtRemarks.Text.Replace("'", "") == "" ? "NA" : txtRemarks.Text.Replace("'", "");
                    offphdr.Acthrs = "00:00";
                    offphdr.ActingpersonId = ActingPersonId.ToString();
                    
                    lvphdrlst.Add(offphdr);
                }
            }

            LeaveProcess lvproc = new LeaveProcess();

            string retval = lvproc.SaveRejectELProcessData(lvphdrlst, myCommand);
                     
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
                string FlowID = "1";
                int levelid = 0;
                string ApplicantID = "L3T593";

                CheckBox chkbox = GridViewLeave.Rows[i].FindControl("Checklv") as CheckBox;

                if (chkbox.Checked == true)
                {

                    LeaveProcessHeader offphdr = new LeaveProcessHeader();

                    TextBox tb = GridViewLeave.Rows[i].FindControl("txtlvRemarks") as TextBox;
                    DropDownList dpllvno = GridViewLeave.Rows[i].FindControl("dpllday") as DropDownList;
                    DropDownList dpllvtype = GridViewLeave.Rows[i].FindControl("lvtype") as DropDownList;

                    offphdr.ApplicantId = ApplicantID.ToString();
                    offphdr.SL = Convert.ToInt32(GridViewLeave.Rows[i].Cells[0].Text.ToString());
                    offphdr.ClaiminDdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysIndate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysOutdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.ActIndate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.ActOutdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysIntime = GridViewLeave.Rows[i].Cells[4].Text.ToString();
                    offphdr.SysOuttime = GridViewLeave.Rows[i].Cells[5].Text.ToString();
                    offphdr.SysTotalhrs = GridViewLeave.Rows[i].Cells[6].Text.ToString();

                    offphdr.NoofLeave = Convert.ToDouble(dpllvno.SelectedItem.Value);
                    offphdr.Leavetype = dpllvtype.SelectedItem.Value.ToString();
                    offphdr.Leavedescription = dpllvtype.SelectedItem.Text.ToString();

                    offphdr.Remarks = tb.Text.ToString();

                    offphdr.Acthrs = "00:00";

                    lvphdrlst.Add(offphdr);
                }
            }

            LeaveProcess lvproc = new LeaveProcess();

            string retval = lvproc.SaveLeaveData(lvphdrlst, myCommand);

            if (retval.ToString() == "")
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

       
        LoadAttendanceDetailsemployeeID(Session["ApplicantID"].ToString(), "P001", "1", fDate, lDate);
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
        LoadLeaveByemployeeID(Session["empcode"].ToString(), "P001", "1", fDate, lDate);
    }
    protected void btnShowALL_Click(object sender, EventArgs e)
    {
        DateTime fDate = Convert.ToDateTime(txtFromDate.Text);
        DateTime lDate = Convert.ToDateTime(txtToDate.Text);
        LoadAttendanceDetailsemployeeID(Session["empcode"].ToString(), "P001", "1", fDate, lDate);
    }

    protected void GridViewLeavePending_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        int indx = GridViewLeavePending.SelectedIndex;

        if (indx == -1)
        {
            return;
        }

        Session["referance"] = GridViewLeavePending.Rows[indx].Cells[1].Text.ToString();
        Session["empcode"] = GridViewLeavePending.Rows[indx].Cells[2].Text.ToString();
        Session["LeaveCode"] = GridViewLeavePending.Rows[indx].Cells[5].Text.ToString();
        Session["plevelid"] = GridViewLeavePending.Rows[indx].Cells[8].Text.ToString();


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
       
        LoadPendingOffdayByemployeeID(Session["referance"].ToString(),PID,FID, Session["ActingPersonID"].ToString(), Session["LeaveCode"].ToString());
       
        //LoadLeavBALeByemployeeID(Session["empcode"].ToString(), fDate);

        LoadEmployeeInformation(Session["empcode"].ToString());

        DataTable dt = DataProcess.GetData(ConnectionStr, "select distinct b.EmpName from [ProcessFlowdet] a inner join Emp_Details b on a.ResponsiblePerson=b.EmpID  where TransactionNo='" + Session["referance"].ToString() + "'");

        //lblResponsibleperson.Text = dt.Rows[0]["EmpName"].ToString();

        LoadPendingLeaveRemarksByemployeeID(Session["referance"].ToString(),PID,FID, Session["ActingPersonID"].ToString(), Session["LeaveCode"].ToString());


        if (GridViewLeave.Rows.Count < 1)
        {
            Panel50.Visible = false;
        }
        else
        {
            Panel50.Visible = true;
        }
    }
    private void ReloadAllgridAfterForwardRejectApproveBySupervisor()
    {
        DateTime fDate = Convert.ToDateTime(Session["fdate"].ToString());
        //LoadPendingOffdayByemployeeID(Session["referance"].ToString(),PID, "2", Session["ActingPersonID"].ToString(), Session["LeaveCode"].ToString());
        //LoadLeavBALeByemployeeID(Session["empcode"].ToString(), fDate);
        LoadEmployeeInformation(Session["empcode"].ToString());

        DataTable dt = DataProcess.GetData(ConnectionStr, "select distinct b.EmpName from [ProcessFlowdet] a inner join Emp_Details b on a.ResponsiblePerson=b.EmpID  where TransactionNo='" + Session["referance"].ToString() + "'");

        lblResponsibleperson.Text = dt.Rows[0]["EmpName"].ToString();

        LoadPendingLeaveRemarksByemployeeID(Session["referance"].ToString(),PID,FID, Session["ActingPersonID"].ToString(), Session["LeaveCode"].ToString());


        if (GridViewLeave.Rows.Count < 1)
        {
            Panel50.Visible = false;
        }
        else
        {
            Panel50.Visible = true;
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
                string FlowID = "2";
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
        LoadpendingOffdayApplication(Session["ActingPersonID"].ToString(),PID,FID);       

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
    protected void GridViewLeavePending_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("Submit"))
            {
                GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                int rowIndex = gvr.RowIndex;
                Session["referance"] = GridViewLeavePending.Rows[rowIndex].Cells[1].Text.ToString();
                Session["empcode"] = GridViewLeavePending.Rows[rowIndex].Cells[2].Text.ToString();
                Session["LeaveCode"] = GridViewLeavePending.Rows[rowIndex].Cells[5].Text.ToString();
                Session["plevelid"] = GridViewLeavePending.Rows[rowIndex].Cells[8].Text.ToString();
                DropDownList ddl = (DropDownList)GridViewLeavePending.Rows[rowIndex].Cells[10].FindControl("ddlLoadPermission");
                if (ddl.SelectedItem.Text == "--please select--" || ddl.SelectedItem.Text == "--No Permission--")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please Select Permission Correctly');", true);
                }
                else
                {
                    int actionId = Convert.ToInt32(ddl.SelectedValue);
                    ReloadAllgridAfterForwardRejectApprove();
                    SelectCheckbox(GridViewLeave);
                    ButtonCall(actionId);
                }
                Panel50.Visible = false;
            }
            else if (e.CommandName.Equals("Preview"))
            {
                GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                int rowIndex = gvr.RowIndex;
                Session["referance"] = GridViewLeavePending.Rows[rowIndex].Cells[1].Text.ToString();
                Session["empcode"] = GridViewLeavePending.Rows[rowIndex].Cells[2].Text.ToString();
                Session["LeaveCode"] = GridViewLeavePending.Rows[rowIndex].Cells[5].Text.ToString();
                Session["plevelid"] = GridViewLeavePending.Rows[rowIndex].Cells[8].Text.ToString();                             
                PreviewELbyEmpid(Session["empcode"].ToString());
                //Panel50.Visible = false;
            }

        }
        catch (Exception)
        {

            throw;
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

    public void ButtonCall(int actionTypeId)
    {
        if (actionTypeId == 2)
        {
            btnForward_Click(this, new EventArgs());
        }
        else if (actionTypeId == 3)
        {
            btnReturn_Click(this, new EventArgs());
        }
        else if (actionTypeId == 4)
        {
            btnReject_Click(this, new EventArgs());
        }
        else if (actionTypeId == 5)
        {
            btnApprove_Click(this, new EventArgs());
        }
    }

    private void PreviewELbyEmpid(string empid)
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

        string rep = "{Hrms_Emp_EL_Encashment.EmpId} in ['" + empid + "']";

        clsReport rpt = new clsReport();

        rpt.FileName = "../REPORT/EarnedLeaveByEmpIdExtended.rpt";
        rpt.ConnectionInfo = ConnInfo;
        rpt.ParametersFields = paramFields;
        rpt.SelectionFormulla = rep;

        string qrystr = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
        //Session[qrystr] = rpt;
        Cache.Insert(qrystr, rpt);

        RegisterStartupScript("Click", "<script>window.open('./frm_report_viewer.aspx?session_id_no=" + qrystr + "');</script>");

    }
}
