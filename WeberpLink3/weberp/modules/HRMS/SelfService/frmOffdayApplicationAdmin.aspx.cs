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

public partial class ClientSide_modules_mis_naz_FORMS_HRMS_Forms_frmOffdayApplicationAdmin : System.Web.UI.Page
{

    string ConnectionStr = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    string PID = "P002";
    string FID = "2";
    double total = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        //StaticData.MsgConfirmBox(btnSubmit, "Are you sure want to Apply Off Day ? ");
        
                             
        if (!Page.IsPostBack)
        {           
            DateTime fDate, lDate;           
            fDate = Convert.ToDateTime(ReturnDecissinon().Split('@')[0].ToString());
            lDate = Convert.ToDateTime(ReturnDecissinon().Split('@')[1].ToString());

            DataTable dttask = new DataTable();
            dttask = DataProcess.GetData(ConnectionStr, "select * from ProcessTaskDateAllemoloyee where TaskType='OF' and [Status]='Y'");

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


            Session["ApplicantID"] = "";            
            Session["EntryUserid"] = "L3T593";  //login user id             
            //Session["EntryUserid"] = Session[StaticData.sessionUserId].ToString();


            Session["fdate"] = fDate;
            Session["lDate"] = lDate;

            LoadOffdayAttendanceDetailsemployeeID(Session["ApplicantID"].ToString(), PID,FID, fDate, lDate);           
            LoadEmployeeInformation(Session["ApplicantID"].ToString());
            LoadResponsiblePersonID(Session["ApplicantID"].ToString());
                                
            //PanelLeaveHdr.Visible=false; 
            
            lblPeriod.Text = FinanialPeriod(fDate);
            lblcurrentPeriod.Text = Convert.ToString(System.DateTime.Now.Date.Day) + "-" + Convert.ToString(System.DateTime.Now.Date.Month) + "-" + Convert.ToString(System.DateTime.Now.Date.Year);
                        
        }        

    }

    private string ReturnDecissinon()
    {
        string ret = "";

        DataTable dttask = new DataTable();
        DateTime serverdate = DateProcess.GetServerDate(ConnectionStr);
        DateTime fDate = DateProcess.FirstDateOfMonth(serverdate);
        DateTime lDate = DateProcess.LastDateOfMonth(serverdate);

        //dttask = DataProcess.GetData(ConnectionStr, "select * from ProcessTaskDateAllemoloyee where TaskType='LV' and TrnMonth=" + serverdate.Month + " and TrnYear=" + serverdate.Year + "");

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

        ret = fDate.ToString() + "@" + lDate.ToString();

        return ret;
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
            string ffff = "L3T388";
            Image1.ImageUrl = "~/ClientSide/modules/mis/naz/FORMS/HRMS/forms/hndImage.ashx?id=" + ffff;
      //  }
       // Image1.ImageUrl = "~/ClientSide/modules/HRIS/MIS//hndImage.ashx?id=" + strarr[0].Trim();
    }   

    private void LoadResponsiblePersonID(string empid)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetResponsiblePersonByEmpid";        
        cmd.Parameters.Add(new SqlParameter("@empid", SqlDbType.NVarChar)).Value = empid.ToString();   
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
                lst.Text = dr["EmpID"].ToString()+":"+dr["EmpName"].ToString();

                dplResponsible.Items.Add(lst);
            }
        }


 
    }
       
    private void LoadOffdayAttendanceDetailsemployeeID(string empid, string Processid, string flowid, DateTime fDate, DateTime lDate)
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetOffDayStatus";

        cmd.Parameters.Add(new SqlParameter("@fDate", SqlDbType.DateTime)).Value = fDate;
        cmd.Parameters.Add(new SqlParameter("@lDate ", SqlDbType.DateTime)).Value = lDate;
        cmd.Parameters.Add(new SqlParameter("@empid", SqlDbType.NVarChar)).Value = empid.ToString();
        cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid.ToString();
        cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid.ToString();

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
        
        myTrans = myConnection.BeginTransaction("SaveAllTransaction");

        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;


        try
        {

            List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();

            LeaveProcess lvp=new LeaveProcess();
            string Transactionno = lvp.GetTransactionNo(ConnectionStr);
            int movementno = Convert.ToInt32(lvp.GetMovementNo(ConnectionStr));

            for (int i = 0; i < grdHoliday.Rows.Count; i++)
            {
                string ProcessID =PID;
                string FlowID =FID;
                int levelid = 0;
                
                string ApplicantID = Session["ApplicantID"].ToString();
                
                CheckBox chkbox = grdHoliday.Rows[i].FindControl("CheckRet") as CheckBox;

                if (chkbox.Checked == true)
                {
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();
                    
                    MKB.TimePicker.TimeSelector mkb = (MKB.TimePicker.TimeSelector)grdHoliday.Rows[i].FindControl("timeoff1");
                    MKB.TimePicker.TimeSelector mkb2 = (MKB.TimePicker.TimeSelector)grdHoliday.Rows[i].FindControl("timeoff2");
                                                     
                    offphdr.TransactionNo = Transactionno.ToString();
                    offphdr.ApplicantId = ApplicantID.ToString();
                    offphdr.ProcessId = ProcessID.ToString();
                    offphdr.FlowId = FlowID.ToString();
                    offphdr.ProcesslevelId = 1;// dplResponsible.SelectedItem.Value==""?1:0;
                    offphdr.ProcessnextlevelId = 1;
                    offphdr.ActiontypeId = 1;
                    offphdr.SL = Convert.ToInt32(grdHoliday.Rows[i].Cells[0].Text.ToString());
                    offphdr.ClaiminDdate = Convert.ToDateTime(grdHoliday.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysIndate = Convert.ToDateTime(grdHoliday.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysOutdate = Convert.ToDateTime(grdHoliday.Rows[i].Cells[1].Text.ToString());
                    offphdr.ActIndate = Convert.ToDateTime(grdHoliday.Rows[i].Cells[1].Text.ToString());
                    offphdr.ActOutdate = Convert.ToDateTime(grdHoliday.Rows[i].Cells[1].Text.ToString());
                    offphdr.SysIntime = grdHoliday.Rows[i].Cells[4].Text.ToString() == "&nbsp;" ? "00:00 AM" : grdHoliday.Rows[i].Cells[4].Text.ToString();
                    offphdr.SysOuttime = grdHoliday.Rows[i].Cells[5].Text.ToString() == "&nbsp;" ? "00:00 AM" : grdHoliday.Rows[i].Cells[5].Text.ToString();
                    offphdr.SysTotalhrs = grdHoliday.Rows[i].Cells[6].Text.ToString() == "&nbsp;" ? "00:00" : grdHoliday.Rows[i].Cells[6].Text.ToString();

                    offphdr.ActIntime = timeformat(mkb.Date.Hour.ToString() + ":" + mkb.Date.Minute.ToString() + ":" + mkb.AmPm.ToString());
                    offphdr.ActOuttime = timeformat(mkb2.Date.Hour.ToString() + ":" + mkb2.Date.Minute.ToString() + ":" + mkb2.AmPm.ToString());

                    offphdr.Remarks = txtRemarks.Text.ToString();
                    offphdr.Acthrs = "00:00";
                    offphdr.Remarks =txtRemarks.Text.ToString().Replace("'","");                    
                    offphdr.ResponsiblepersonId = dplResponsible.SelectedItem.Value;
                    offphdr.MovementNo = movementno;
                    offphdr.EntryUserid = Session["EntryUserid"].ToString();
                                        
                    lvphdrlst.Add(offphdr);

                    DateTime InDateTime = Convert.ToDateTime(offphdr.SysIndate.ToShortDateString() + " " + offphdr.ActIntime);
                    DateTime OutDateTime = Convert.ToDateTime(offphdr.ActOutdate.ToShortDateString() + " " + offphdr.ActOuttime);
                    if (InDateTime > OutDateTime) return;

                }
            }

            LeaveProcess lvproc = new LeaveProcess();

            string retval = lvproc.SaveInitiateOffdayProcessData(lvphdrlst, myCommand);
                               
            if (retval.ToString() == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }
                        
            LoadAllInformation();

            if (retval.ToString() != "")
            {
               // SendMailtoApprover();
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

    private void SendMailtoApprover()
    {
        try
        {
            DataTable dts = DataProcess.GetData(ConnectionStr, "select a.EmpName,b.Emp_Mas_Remarks from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id where Emp_Mas_Emp_Id='" + Session["ApplicantID"].ToString() + "'");
            DataTable dtr = DataProcess.GetData(ConnectionStr, "select c.Emp_Mas_Remarks,b.EmpName from ProcessAccessPermission a inner join Emp_Details b on a.AccessId=b.EmpID  inner join hrms_emp_mas c on c.Emp_Mas_Emp_Id=a.AccessId"
                                                             + " where a.ProcessId='P001' and a.ProcessFlowId=1 and a.ProcessLevelid=1 and a.ApplicantID='" + Session["ApplicantID"].ToString() + "'");


            DataTable dtcc = DataProcess.GetData(ConnectionStr, "select a.EmpID,b.Emp_Mas_Remarks from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id where Emp_Mas_Emp_Id='" + dplResponsible.SelectedItem.Value + "'");

            string msgbody = "I have applied for a leave for your approval.";

            string sid = dts.Rows[0]["Emp_Mas_Remarks"].ToString();
            string sname = dts.Rows[0]["EmpName"].ToString();
            string rid = dtr.Rows[0]["Emp_Mas_Remarks"].ToString();
            string rname = dtr.Rows[0]["EmpName"].ToString();
            string msub = "Leave Application";
            string mbody = arrange_data(Session["ApplicantID"].ToString(), sname, msgbody);
            string ccid = dtcc.Rows[0]["Emp_Mas_Remarks"].ToString();

            Sendmail(sid, sname, rid, rname, ccid, msub, mbody);

        }
        catch(Exception ex)
        {
            throw ex; 
        }
    }


    private void LoadAllInformation()
    {
        DateTime fDate, lDate;

        fDate = Convert.ToDateTime(Session["fdate"].ToString());
        lDate = Convert.ToDateTime(Session["lDate"].ToString());
      
        LoadOffdayAttendanceDetailsemployeeID(Session["ApplicantID"].ToString(), PID,FID, fDate, lDate);
        
        LoadEmployeeInformation(Session["ApplicantID"].ToString());
        lblPeriod.Text = FinanialPeriod(fDate);
        lblcurrentPeriod.Text = System.DateTime.Now.ToString();
    }
   
    protected void btnprevious_Click(object sender, EventArgs e)
    {
        DateTime fDate = Convert.ToDateTime("01/12/2012");
        DateTime lDate = Convert.ToDateTime("31/12/2013");


        LoadOffdayAttendanceDetailsemployeeID(Session["ApplicantID"].ToString(), "P001", "1", fDate, lDate);       
        LoadEmployeeInformation(Session["ApplicantID"].ToString());

        //PanelLeaveHdr.Visible=false; 

        lblPeriod.Text = FinanialPeriod(fDate);
        lblcurrentPeriod.Text = System.DateTime.Now.ToString();

    }
    protected void btnCurrentPeriod_Click(object sender, EventArgs e)
    {
        DateTime fDate = Convert.ToDateTime("01/01/2013");
        DateTime lDate = Convert.ToDateTime("31/01/2013");

       
        LoadOffdayAttendanceDetailsemployeeID(Session["ApplicantID"].ToString(), "P001", "F001", fDate, lDate);
        
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
        Session["ApplicantID"] = txtempid.Text;
        LoadEmployeeInformation(Session["ApplicantID"].ToString());
        LoadOffdayAttendanceDetailsemployeeID(Session["ApplicantID"].ToString(), PID, FID, fDate, lDate);   
        
    }
    protected void btnShowALL_Click(object sender, EventArgs e)
    {
        DateTime fDate = Convert.ToDateTime(txtFromDate.Text);
        DateTime lDate = Convert.ToDateTime(txtToDate.Text);

        Session["ApplicantID"] = txtempid.Text;

        LoadOffdayAttendanceDetailsemployeeID(Session["ApplicantID"].ToString(),PID,FID, fDate, lDate);
        LoadEmployeeInformation(Session["ApplicantID"].ToString());
        LoadResponsiblePersonID(Session["ApplicantID"].ToString());
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

    private string arrange_data(string eid, string sname,string body)
    {
        string str = "";

        str = "\nDear Sir,";
        str += "\n\n"+body.ToString();
        str += "\n\nEmployee ID : " + eid;
        str += "\nName          : " + sname.ToString();
       
        str += "\n\n\n\nTo view detail or update this request just login the link bellow:\n\n";
        //str += "http://office.link3.net/mis/Clientside/frm_login.aspx?updatereferrenceno=" + ref_name;
        str += "http://office.link3.net";
        str += "\n\n\n\n";
        str += "This is auto generated mail from LinkOffice.";


        return str;

    }

    protected void grdHoliday_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
            CheckBox chkbox = e.Row.FindControl("CheckRet") as CheckBox;
            if (e.Row.Cells[4].Text.Trim().ToString() != "&nbsp;" || e.Row.Cells[5].Text.Trim().ToString() != "&nbsp;")
            {
                chkbox.Checked = true;
            }
            Label lb = e.Row.FindControl("lblstatus") as Label;

            DateTime dta = Convert.ToDateTime(e.Row.Cells[1].Text);

            lb.Text = e.Row.Cells[15].Text;

            MKB.TimePicker.TimeSelector mkb = (MKB.TimePicker.TimeSelector)e.Row.FindControl("timeoff1");
            MKB.TimePicker.TimeSelector mkb2 = (MKB.TimePicker.TimeSelector)e.Row.FindControl("timeoff2");
            
            DateTime dt1 = DateTime.Parse(e.Row.Cells[17].Text.ToString());
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

            DateTime dt2 = DateTime.Parse(e.Row.Cells[18].Text.ToString());
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
            

            if (Convert.ToInt32(e.Row.Cells[16].Text) > 0)
            {                
                chkbox.Enabled = false;
                chkbox.Checked = false;
                e.Row.BackColor = System.Drawing.Color.LightGreen;
                mkb.Enabled = false;
                mkb2.ReadOnly = false;
            }
            if (dta < Convert.ToDateTime(Session["fdate"].ToString()))
            {
                chkbox.Enabled = false;
                chkbox.Checked = false;
                mkb.Enabled = false;
                mkb2.Enabled = false;
            }
            if (e.Row.Cells[13].Text == "Y")
            {
                chkbox.Enabled = false;
                chkbox.Checked = false;
                mkb.Enabled = false;
                mkb2.Enabled = false;
            }
            int totalMin = Convert.ToInt32(e.Row.Cells[19].Text.ToString());//just changed the index of cells based on your requirements 

            //if (totalMin > 540)
            //    total += (totalMin - 540);
            total += totalMin;
        }

        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[10].Text = "Total Extra Hour";
            e.Row.Cells[11].Text = DateProcess.TimeDuration(Convert.ToInt32(total.ToString()));
            e.Row.Font.Bold = true;
        }

        e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;

        
        //e.Row.Cells[12].Visible = false;
        e.Row.Cells[13].Visible = false;
        e.Row.Cells[14].Visible = false;
        e.Row.Cells[15].Visible = false;
        e.Row.Cells[16].Visible = false;
        e.Row.Cells[17].Visible = false;
        e.Row.Cells[18].Visible = false;
        e.Row.Cells[19].Visible = false;
    }
    protected void btnTotal_Click(object sender, EventArgs e)
    {
        double totalMin = 0;
        int rowNo = grdHoliday.Rows.Count, i;
        double totalMinu = 0;
        for (i = 0; i < rowNo; i++)
        {
            int action = Convert.ToInt32(grdHoliday.Rows[i].Cells[16].Text);
            CheckBox chkbox = grdHoliday.Rows[i].FindControl("CheckRet") as CheckBox;
            if (action != 0)
            {
                double exsistingMinu = Convert.ToInt32(grdHoliday.Rows[i].Cells[19].Text);
                totalMin += exsistingMinu;
            }
            else if (chkbox.Checked == true && action == 0)
            {
                MKB.TimePicker.TimeSelector mkb = (MKB.TimePicker.TimeSelector)grdHoliday.Rows[i].FindControl("timeoff1");
                MKB.TimePicker.TimeSelector mkb2 = (MKB.TimePicker.TimeSelector)grdHoliday.Rows[i].FindControl("timeoff2");
                string inTime = timeformat(mkb.Date.Hour.ToString() + ":" + mkb.Date.Minute.ToString() + ":" + mkb.AmPm.ToString());
                string outTime = timeformat(mkb2.Date.Hour.ToString() + ":" + mkb2.Date.Minute.ToString() + ":" + mkb2.AmPm.ToString());
                DateTime dateInTime = Convert.ToDateTime(grdHoliday.Rows[i].Cells[1].Text.ToString() + " " + inTime);
                DateTime dateOutTime = Convert.ToDateTime(grdHoliday.Rows[i].Cells[1].Text.ToString() + " " + outTime);
                double totalMinutes = DateProcess.GetTotalMinutes(dateInTime, dateOutTime);
                double extraMinutes = 0;
                if (totalMinutes > 540)
                {
                    extraMinutes = Convert.ToInt32(totalMinutes - 540);
                    grdHoliday.Rows[i].Cells[19].Text = extraMinutes.ToString();
                    grdHoliday.Rows[i].Cells[11].Text = DateProcess.TimeDuration(Convert.ToInt32(extraMinutes));
                }
                else
                {
                    grdHoliday.Rows[i].Cells[11].Text = "00:00";

                }
                totalMinu = Convert.ToInt32(grdHoliday.Rows[i].Cells[19].Text);
                totalMin += extraMinutes;
            }
            GridViewRow rowFooter = grdHoliday.FooterRow;
            rowFooter.Cells[11].Text = DateProcess.TimeDuration(Convert.ToInt32(totalMin));
        }
    }
}
