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
using Microsoft.Office.Interop;

public partial class modules_HRMS_Payments_frmOFOTNTPaymentforAccounts : System.Web.UI.Page
{
    string ConnectionStr = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    decimal PayableAmt = 0;
    decimal PayableAmtLocked = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        StaticData.MsgConfirmBox(btnApprove, "Are you sure want to Approve Leave ? ");
        StaticData.MsgConfirmBox(btnReject, "Are you sure want to Reject Leave ? ");
        StaticData.MsgConfirmBox(btnForward, "Are you sure want to Forward Leave ? ");
        if (!Page.IsPostBack)
        {
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
                      
            Session["ActingPersonID"] = "L3T150";

            Session["fdate"] = fDate;
            Session["lDate"] = lDate;
                                   
            LoadApprovedOFOTNTReadyforPaymentLocked(fDate, lDate,"");

            LoadApprovedOFOTNTDataReadyForPayment(fDate, lDate, "OF,OT,NT");

           
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

        sqlConn.Close();

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
        cmd.CommandText = "spProcessGetPendingOffdayByTransactionno";
        cmd.Parameters.Add(new SqlParameter("@transactionno", SqlDbType.NVarChar)).Value = transactionno.ToString();
        cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid.ToString();
        cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid.ToString();
        cmd.Parameters.Add(new SqlParameter("@actingpersonid", SqlDbType.NVarChar)).Value = actingpersonid.ToString();
        cmd.Parameters.Add(new SqlParameter("@leavecode", SqlDbType.NVarChar)).Value = leavecode.ToString();

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
        cmd.Parameters.Add(new SqlParameter("@transactionno", SqlDbType.NVarChar)).Value = transactionno.ToString();
        cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid.ToString();
        cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid.ToString();
        cmd.Parameters.Add(new SqlParameter("@actingpersonid", SqlDbType.NVarChar)).Value = actingpersonid.ToString();
        cmd.Parameters.Add(new SqlParameter("@leavecode", SqlDbType.NVarChar)).Value = leavecode.ToString();

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

        sqlConn.Close();

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
        cmd.CommandText = "spProcessGetAllPendingOffday";

        cmd.Parameters.Add(new SqlParameter("@actempid", SqlDbType.NVarChar)).Value = actingpersonid.ToString();
        cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid.ToString();
        cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid.ToString();

        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        sqlConn.Close();

        GridViewPendingLocked.DataSource = dt;
        GridViewPendingLocked.DataBind();
        GridViewPendingLocked.Columns[8].Visible = false;
        GridViewPendingLocked.Columns[5].Visible = false;


        string gridhieght = GridViewPendingLocked.Height.Value.ToString();
        
        int gdr = GridViewPendingLocked.Rows.Count;     
        CollapsiblePanelExtenderSrch.ExpandedSize =(gdr*40)+25;
    }

    private void LoadApprovedOffdayApplication(string actingpersonid, string Processid, string flowid,DateTime fDate, DateTime lDate)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetAllApprovedOffday";

        cmd.Parameters.Add(new SqlParameter("@fDate", SqlDbType.DateTime)).Value = fDate;
        cmd.Parameters.Add(new SqlParameter("@lDate ", SqlDbType.DateTime)).Value = lDate;
        cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid.ToString();
        cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid.ToString();

        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        sqlConn.Close();

        GridViewPendingLocked.DataSource = dt;
        GridViewPendingLocked.DataBind();
        GridViewPendingLocked.Columns[8].Visible = false;
        GridViewPendingLocked.Columns[5].Visible = false;
        GridViewPendingLocked.Columns[9].Visible = false;
        GridViewPendingLocked.Columns[10].Visible = false;
        GridViewPendingLocked.Columns[11].Visible = false;


        string gridhieght = GridViewPendingLocked.Height.Value.ToString();

        int gdr = GridViewPendingLocked.Rows.Count;
        if (gdr <= 0)
        {
            PanelLocked.Visible = false;
            CollapsiblePanelExtenderSrch.ExpandedSize = (gdr * 40) + 100;
        }
        else
        {
            PanelLocked.Visible = true;
            CollapsiblePanelExtenderSrch.ExpandedSize = (gdr * 40) + 50;
        }
        
    }


    private void LoadApprovedOFOTNTReadyforPaymentLocked(DateTime fDate, DateTime lDate,String type)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetAllApprovedDataReadyforPaymentLocked";

        cmd.Parameters.Add(new SqlParameter("@fDate", SqlDbType.DateTime)).Value = fDate;
        cmd.Parameters.Add(new SqlParameter("@lDate ", SqlDbType.DateTime)).Value = lDate;
        cmd.Parameters.Add(new SqlParameter("@type", SqlDbType.NVarChar)).Value = type.ToString();
        //cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid.ToString();
        //cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid.ToString();

        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        sqlConn.Close();

        GridViewPendingLocked.DataSource = dt;
        GridViewPendingLocked.DataBind();

        int gdr = GridViewPendingLocked.Rows.Count;
        if (gdr <=0)
        {
            PanelLocked.Visible = false;
            CollapsiblePanelExtenderSrch.ExpandedSize = (gdr * 40) + 100;
            pnlSrchDet.Height = (gdr * 40) + 50;
        }
        else
        {
            PanelLocked.Visible = true;
            CollapsiblePanelExtenderSrch.ExpandedSize = (gdr * 40) + 50;
            pnlSrchDet.Height = (gdr * 40) + 50;
        }
        
    }    

    private void LoadApprovedOffdayDataReadyForPayment(string Processid, string flowid, DateTime fDate, DateTime lDate)
    {      
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetAllApprovedOffdayReadyforPayment";

        cmd.Parameters.Add(new SqlParameter("@fDate", SqlDbType.DateTime)).Value = fDate;
        cmd.Parameters.Add(new SqlParameter("@lDate ", SqlDbType.DateTime)).Value = lDate;
        cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid.ToString();
        cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid.ToString();

        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        sqlConn.Close();

        GdvOffdayPaymentData.DataSource = dt;
        GdvOffdayPaymentData.DataBind();      

        int gdr = GdvOffdayPaymentData.Rows.Count;
        if (gdr <= 0)
        {
            PanelPayment.Visible = false;
            CollapsiblePanelExtenderSrchforpayment.ExpandedSize = (gdr * 40) + 100;
        }
        else
        {
            PanelPayment.Visible = true;
            CollapsiblePanelExtenderSrchforpayment.ExpandedSize = (gdr * 40) + 50; 
        }
        
    }

    private void LoadApprovedOFOTNTDataReadyForPayment(DateTime fDate, DateTime lDate,string type)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetAllApprovedDataReadyforPayment";

        cmd.Parameters.Add(new SqlParameter("@fDate", SqlDbType.DateTime)).Value = fDate;
        cmd.Parameters.Add(new SqlParameter("@lDate ", SqlDbType.DateTime)).Value = lDate;
        cmd.Parameters.Add(new SqlParameter("@type", SqlDbType.NVarChar)).Value = type.ToString();
        //cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid.ToString();

        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        sqlConn.Close();

        GdvOffdayPaymentData.DataSource = dt;
        GdvOffdayPaymentData.DataBind();

        int gdr = GdvOffdayPaymentData.Rows.Count;
        if (gdr <= 0)
        {
            PanelPayment.Visible = false;
            CollapsiblePanelExtenderSrchforpayment.ExpandedSize = (gdr * 40) + 100;
        }
        else
        {
            PanelPayment.Visible = true;
            CollapsiblePanelExtenderSrchforpayment.ExpandedSize = (gdr * 40) + 50;
        }
        
    }


    private DataTable  LoadApprovedOFOTNTDataForPaymentExport()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetOFOTNTReadyforPaymentExport";

        //cmd.Parameters.Add(new SqlParameter("@fDate", SqlDbType.DateTime)).Value = fDate;
        //cmd.Parameters.Add(new SqlParameter("@lDate ", SqlDbType.DateTime)).Value = lDate;
        //cmd.Parameters.Add(new SqlParameter("@type", SqlDbType.NVarChar)).Value = type.ToString();
        //cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid.ToString();

        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        sqlConn.Close();
        return dt;
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

            dpl.SelectedValue= e.Row.Cells[14].Text;


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
                dpl.Visible = false;
                dpllt.Visible = false; 

            }
            if (e.Row.Cells[3].Text == "L" || e.Row.Cells[3].Text == "N")
            {
                e.Row.Cells[3].Text = "Leave";
                e.Row.BackColor = System.Drawing.Color.LightGreen;
                chkbox.Enabled = false;
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
                chkbox.Enabled = false;
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
        e.Row.Cells[8].Visible = false;
        e.Row.Cells[9].Visible = false;
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

    protected void GridViewLeavePending_RowDataBound(object sender, GridViewRowEventArgs e)
    {      
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);

           // e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
           // e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GridViewPendingLocked, "Select$" + e.Row.RowIndex);



            if (e.Row.Cells[5].Text == "OF")
            {
               e.Row.Cells[6].Text = "Off day";
               e.Row.Cells[8].Text = "Days";
            }
            if (e.Row.Cells[5].Text == "OT")
            {
                e.Row.Cells[6].Text = "Over Time";
                e.Row.Cells[8].Text = "Minute";
            }
            if (e.Row.Cells[5].Text == "NT")
            {
                e.Row.Cells[6].Text = "Night";
                e.Row.Cells[8].Text = "Days";
            }
                     
            PayableAmt = PayableAmt + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Payableamt"));
                      
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;


            e.Row.Cells[9].Text = Convert.ToDecimal(e.Row.Cells[9].Text).ToString("#,##0.00");
            e.Row.Cells[10].Text = Convert.ToDecimal(e.Row.Cells[10].Text).ToString("#,##0.00");            
            
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[9].Text = "TOTAL:";
            // for the Footer, display the running totals
            e.Row.Cells[10].Text = PayableAmt.ToString("#,##0.00");         
            e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;            
            e.Row.Font.Bold = true;
        }

        e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;  
        e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;     

        e.Row.Cells[5].Visible = false;
        e.Row.Cells[11].Visible = false;
        e.Row.Cells[12].Visible = false;
        e.Row.Cells[13].Visible = false;
        e.Row.Cells[14].Visible = false;
               
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
                string ProcessID = "P002";
                string FlowID = "2";
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
                    offphdr.ActIntime = GridViewLeave.Rows[i].Cells[7].Text.ToString();
                    offphdr.ActIntime = GridViewLeave.Rows[i].Cells[8].Text.ToString();

                    offphdr.NoofLeave = Convert.ToDouble(dpllvno.SelectedItem.Value);
                    offphdr.Leavetype = dpllvtype.SelectedItem.Value.ToString();
                    offphdr.Leavedescription = dpllvtype.SelectedItem.Text.ToString();
                    offphdr.Remarks = txtRemarks.Text.Replace("'","") == "" ? "NA" : txtRemarks.Text.Replace("'","");
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
            string msgbody = "Your offday application been approved";
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
        DateTime fDate = Convert.ToDateTime("01/01/2013");
        DateTime lDate = Convert.ToDateTime("31/01/2013");

        LoadpendingOffdayApplication(Session["ActingPersonID"].ToString(), "P002", "1");

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
                string ProcessID = "P002";
                string FlowID = "2";
                
                              
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId =2;  // forward


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

                    offphdr.Remarks = txtRemarks.Text.Replace("'", "") == "" ? "NA" : txtRemarks.Text.Replace("'", "");

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

            ReloadAllgridAfterForwardRejectApprove();

            if (retval.ToString() != "")
            {
                SendMailforForword(levelid);
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
                string ProcessID = "P002";
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
                string ProcessID = "P002";
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
        if (txtDatefrom.Text == "" || txtToDate.Text == "") return; 
        
        DateTime fDate = Convert.ToDateTime(txtDatefrom.Text);
        DateTime lDate = Convert.ToDateTime(txtDateTo.Text);

        string type = "";

        for (int i = 0; i < ChkPmttypeForPayment.Items.Count; i++)
        {
            if (ChkPmttypeForPayment.Items[i].Selected)
            {
                if (i == 0)
                {
                    type = ChkPmttypeForPayment.Items[i].Value;
                }
                else
                {
                    if (type == "")
                    {
                        type = ChkPmttypeForPayment.Items[i].Value;
                    }
                    else
                    {
                        type = type + "," + ChkPmttypeForPayment.Items[i].Value;
                    }

                }
            }
        }



        LoadApprovedOFOTNTDataReadyForPayment(fDate, lDate,type);

    }
    protected void btnShowALL_Click(object sender, EventArgs e)
    {
        DateTime fDate = Convert.ToDateTime(txtFromDate.Text);
        DateTime lDate = Convert.ToDateTime(txtToDate.Text);
        LoadAttendanceDetailsemployeeID(Session["empcode"].ToString(), "P001", "1", fDate, lDate);
    }

    protected void GridViewLeavePending_SelectedIndexChanged(object sender, EventArgs e)
    {
        double amt=0;
        int indx = GridViewPendingLocked.SelectedIndex;

        if (indx == -1)
        {
            return;
        }
        for (int i = 0; i < GridViewPendingLocked.Rows.Count; i++)
        {
            CheckBox chkbox = GridViewPendingLocked.Rows[i].FindControl("CheckRet") as CheckBox;
            if (chkbox.Checked == true)
            {
                amt = amt + Convert.ToDouble(GridViewPendingLocked.Rows[i].Cells[10].Text);

            }
        }

        GridViewPendingLocked.FooterRow.Cells[10].Text = amt.ToString("#,##0.00");        
       
    }

    private void ReloadAllgridAfterForwardRejectApprove()
    {
        DateTime fDate = Convert.ToDateTime(Session["fdate"].ToString());
        LoadPendingOffdayByemployeeID(Session["referance"].ToString(), "P002", "2", Session["ActingPersonID"].ToString(), Session["LeaveCode"].ToString());        
        //LoadLeavBALeByemployeeID(Session["empcode"].ToString(), fDate);
        LoadEmployeeInformation(Session["empcode"].ToString());

        DataTable dt = DataProcess.GetData(ConnectionStr, "select distinct b.EmpName from [ProcessFlowdet] a inner join Emp_Details b on a.ResponsiblePerson=b.EmpID  where TransactionNo='" + Session["referance"].ToString() + "'");

        lblResponsibleperson.Text = dt.Rows[0]["EmpName"].ToString();

        LoadPendingLeaveRemarksByemployeeID(Session["referance"].ToString(), "P002", "2", Session["ActingPersonID"].ToString(), Session["LeaveCode"].ToString());


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
                string ProcessID = "P002";
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
        //GridViewPendingLocked.PageIndex = e.NewPageIndex;
        //LoadpendingOffdayApplication(Session["ActingPersonID"].ToString(), "P002", "1");       

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
    protected void btnlockedforpayment_Click(object sender, EventArgs e)
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
            List<OffdayProcessHeader> ofphdrlst = new List<OffdayProcessHeader>();
            
            for (int i = 0; i < GridViewPendingLocked.Rows.Count; i++)
            {               
                string ActingPersonId = Session["ActingPersonID"].ToString();
                
                CheckBox chkbox = GridViewPendingLocked.Rows[i].FindControl("CheckRet") as CheckBox;

                if (chkbox.Checked == true)
                {

                   // LeaveProcessHeader offphdr = new LeaveProcessHeader();

                    OffdayProcessHeader offphdr = new OffdayProcessHeader(); 
                                        
                    offphdr.ApplicantId = Convert.ToString(GridViewPendingLocked.Rows[i].Cells[1].Text.ToString());
                    offphdr.ProcessId = Convert.ToString(GridViewPendingLocked.Rows[i].Cells[13].Text.ToString());
                    offphdr.FlowId = Convert.ToString(GridViewPendingLocked.Rows[i].Cells[14].Text.ToString());                    
                    offphdr.ActIndate = Convert.ToDateTime(GridViewPendingLocked.Rows[i].Cells[11].Text.ToString());
                    offphdr.ActOutdate = Convert.ToDateTime(GridViewPendingLocked.Rows[i].Cells[12].Text.ToString());
                    offphdr.Type = Convert.ToString(GridViewPendingLocked.Rows[i].Cells[5].Text.ToString());
                    offphdr.EntryUserid = Session["ActingPersonID"].ToString();
                    
                    ofphdrlst.Add(offphdr);
                }
            }

            LeaveProcess lvproc = new LeaveProcess();

            string retval = lvproc.SaveLockedApprovedOffdayProcessData(ofphdrlst, myCommand);

            if (retval.ToString() == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }
                       

            LoadApprovedOFOTNTReadyforPaymentLocked(Convert.ToDateTime(Session["fdate"].ToString()), Convert.ToDateTime(Session["lDate"].ToString()),"");
            LoadApprovedOFOTNTDataReadyForPayment(Convert.ToDateTime(Session["fdate"].ToString()),Convert.ToDateTime(Session["lDate"].ToString()), "OF,OT,NT");            

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
    protected void btnPaymentDone_Click(object sender, EventArgs e)
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
            List<OffdayProcessHeader> ofphdrlst = new List<OffdayProcessHeader>();

            LeaveProcess lvp = new LeaveProcess(); 
            
            string PaymentRefno = lvp.GetPaymentReferenceNo(ConnectionStr,txtPaymentDate.Text);
            string retval = "";

            for (int i = 0; i < GdvOffdayPaymentData.Rows.Count; i++)
            {               
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId = 5;  // Approve

                CheckBox chkbox = GdvOffdayPaymentData.Rows[i].FindControl("CheckRet") as CheckBox;

                if (chkbox.Checked == true)
                {                                       
                    OffdayProcessHeader offphdr = new OffdayProcessHeader();

                    offphdr.ApplicantId = Convert.ToString(GdvOffdayPaymentData.Rows[i].Cells[1].Text.ToString());
                    offphdr.ProcessId = Convert.ToString(GdvOffdayPaymentData.Rows[i].Cells[16].Text.ToString());
                    offphdr.FlowId = Convert.ToString(GdvOffdayPaymentData.Rows[i].Cells[17].Text.ToString());                                                                             
                    offphdr.ActIndate = Convert.ToDateTime(GdvOffdayPaymentData.Rows[i].Cells[14].Text.ToString());
                    offphdr.ActOutdate = Convert.ToDateTime(GdvOffdayPaymentData.Rows[i].Cells[15].Text.ToString());
                    offphdr.Type = Convert.ToString(GdvOffdayPaymentData.Rows[i].Cells[5].Text.ToString());
                    offphdr.Quantity = Convert.ToDouble(GdvOffdayPaymentData.Rows[i].Cells[7].Text.ToString());
                    offphdr.UnitRate = Convert.ToDouble(GdvOffdayPaymentData.Rows[i].Cells[10].Text.ToString());
                    offphdr.Rate = Convert.ToDouble(GdvOffdayPaymentData.Rows[i].Cells[11].Text.ToString());
                    offphdr.MaximumLimit = Convert.ToDouble(GdvOffdayPaymentData.Rows[i].Cells[12].Text.ToString());
                    offphdr.PayableAmount = Convert.ToDouble(GdvOffdayPaymentData.Rows[i].Cells[13].Text.ToString());
                    offphdr.PaymentDate = Convert.ToDateTime(txtPaymentDate.Text);
                    offphdr.PaymentNarration =Convert.ToString(txtPaymentNarration.Text).Replace("'","");
                    offphdr.EntryUserid = Session["ActingPersonID"].ToString();

                    ofphdrlst.Add(offphdr);
                }

                LeaveProcess lvproc = new LeaveProcess();
                retval = lvproc.SavePaymentProcessOffdayProcessData(ofphdrlst, myCommand, PaymentRefno);
            }

            

            if (retval.ToString() == "")
            {
                myTrans.Rollback("SaveAllTransaction");
            }
            else
            {
                myTrans.Commit();
            }

            LoadApprovedOFOTNTDataReadyForPayment(Convert.ToDateTime(Session["fdate"].ToString()), Convert.ToDateTime(Session["lDate"].ToString()),"OF,OT,NT");

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
    protected void GdvOffdayPaymentData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int Minute=0;
        int Hour = 0;
        int rMinute = 0;
        string noofpayment = "";

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
           
            if (e.Row.Cells[5].Text == "OF")
            {
                e.Row.Cells[6].Text = "Off day";
                e.Row.Cells[9].Text = "Days";
            }
            else if (e.Row.Cells[5].Text == "OT")
            {
                e.Row.Cells[6].Text = "Over time";
                e.Row.Cells[9].Text = "Hour";  
                
                Minute=Convert.ToInt32(e.Row.Cells[7].Text);
                Hour = Minute / 60;
                rMinute = Minute%60;
                noofpayment = string.Format("{0:00}", Hour) + ":" + string.Format("{0:00}", rMinute);
                e.Row.Cells[8].Text = noofpayment.ToString();             

            }
            else if (e.Row.Cells[5].Text == "NT")
            {
                e.Row.Cells[6].Text = "Night";
                e.Row.Cells[9].Text = "Night";
            }

            PayableAmtLocked = PayableAmtLocked + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Payableamt"));

            e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;

            e.Row.Cells[10].Text = Convert.ToDecimal(e.Row.Cells[10].Text).ToString("#,##0.00");
            e.Row.Cells[11].Text = Convert.ToDecimal(e.Row.Cells[11].Text).ToString("#,##0.00");
            e.Row.Cells[12].Text = Convert.ToDecimal(e.Row.Cells[12].Text).ToString("#,##0.00");
            e.Row.Cells[13].Text = Convert.ToDecimal(e.Row.Cells[13].Text).ToString("#,##0.00");


            CheckBox chkbox = e.Row.FindControl("CheckRet") as CheckBox;
            chkbox.Checked = true;

        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[12].Text = "TOTAL:";
            // for the Footer, display the running totals
            e.Row.Cells[13].Text = PayableAmtLocked.ToString("#,##0.00");          
            e.Row.Font.Bold = true;
        }

        e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
        e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
        e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
        e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;   

        e.Row.Cells[5].Visible = false;
        e.Row.Cells[7].Visible = false;
        e.Row.Cells[11].Visible = false;        
        e.Row.Cells[14].Visible = false;
        e.Row.Cells[15].Visible = false;
        e.Row.Cells[16].Visible = false;
        e.Row.Cells[17].Visible = false;
        e.Row.Cells[18].Visible = false;
    }
    protected void btnShowPaymentInfo_Click(object sender, EventArgs e)
    {
        DateTime fDate = Convert.ToDateTime(TxtPaymentDatefrom.Text);
        DateTime lDate = Convert.ToDateTime(txtPaymentto.Text);

        string type="";

        for (int i = 0; i < ChkPmttype.Items.Count; i++)
        {
            if (ChkPmttype.Items[i].Selected)
            {
                if (i == 0)
                {
                    type = ChkPmttype.Items[i].Value;
                }
                else
                {
                    if (type == "")
                    {
                        type = ChkPmttype.Items[i].Value;
                    }
                    else 
                    {
                        type = type + "," + ChkPmttype.Items[i].Value;
                    }
                    
                }
            }
        }

        string spname="";

        if (rdoPaymentInfo.SelectedIndex == 0)
            spname = "spProcessGetPaymentInformation";
        else
            spname = "spProcessGetPaymentInformationALL";
       


        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = spname.ToString();

        cmd.Parameters.Add(new SqlParameter("@fDate", SqlDbType.DateTime)).Value = fDate;
        cmd.Parameters.Add(new SqlParameter("@lDate ", SqlDbType.DateTime)).Value = lDate;
        cmd.Parameters.Add(new SqlParameter("@type", SqlDbType.NVarChar)).Value = type.ToString();     

        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        sqlConn.Close();

        GdvPaymentInfo.DataSource = dt;
        GdvPaymentInfo.DataBind();

        int gdr = GdvPaymentInfo.Rows.Count;
        CollapsiblePanelExtenderPanelPaymentInfohdr.ExpandedSize = (gdr * 40) + 200;      
                
    }
    protected void GdvPaymentInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string reference = "";
        string type = ""; 

        if (e.CommandName == "EXPORT")
        {           
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GdvPaymentInfo.Rows[index];
            reference = row.Cells[1].Text;
            type = row.Cells[4].Text;
            ExportToXLDataPrepare(reference, type);
            Export("Exporttt.xls", gdvExport);
        }
        else if (e.CommandName == "PRINT")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GdvPaymentInfo.Rows[index];
            reference = row.Cells[1].Text;
            type = row.Cells[4].Text;
            PreviewReportPrepare(reference, type);
        }
        else if (e.CommandName == "DONE")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GdvPaymentInfo.Rows[index];
            reference = row.Cells[1].Text;
            type = row.Cells[4].Text;
            PaymentDone(reference,type);
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





    private void PaymentDone(string Paymentrefno,string type)
    {
        LeaveProcess lvproc = new LeaveProcess();
        string retval = lvproc.SavePaymentDone(ConnectionStr, Paymentrefno, type, "", Session["ActingPersonID"].ToString());
 
    }

    private void PreviewReportPrepare(string reference, string type)
    {     
        int day = DateTime.DaysInMonth(2013, 2);

        Connection DC = new Connection();
        string constr, sql;
        sql = "";
        object dummy = Type.Missing;
        string rptname = "";
        
        string[] tp=new string [3];
        
        string OF = "";
        string OT = "";
        string NT = "";

        if (type.ToString().Length == 2)
        {
            tp[0] = type.Substring(0, 2).ToString();
            tp[1] = "";
            tp[2] = "";
        }
        else if(type.ToString().Length == 5)
        {
            tp[0] = type.Substring(0, 2).ToString();
            tp[1] = type.Substring(3, 2).ToString();            
            tp[2] = ""; 
        }
        else if (type.ToString().Length == 8)
        {
            tp[0] = type.Substring(0, 2).ToString();
            tp[1] = type.Substring(3, 2).ToString();
            tp[2] = type.Substring(6, 2).ToString();
        }
       

        for (int j = 0; j <tp.Length; j++)
        {
            if (tp[j].ToString() == "OF")
            {
                OF = tp[j].ToString();
            }
            else if (tp[j].ToString() == "OT")
            {
                OT = tp[j].ToString();
            }
            else if (tp[j].ToString() == "NT")
            {
                NT = tp[j].ToString();
            }
        }

        constr = System.Configuration.ConfigurationManager.AppSettings["ConStrLNK"].ToString();

        DC.Open(constr, null, null, 0);

        sql = "if exists(Select * from sysobjects where name = 'ViewProcessOFDataSummeryDepartmentWise' ) begin drop view ViewProcessOFDataSummeryDepartmentWise end  ";
        DC.Execute(sql, out dummy, 1);
        sql = "Create view [dbo].[ViewProcessOFDataSummeryDepartmentWise] as"
                + " select b.EmpID,b.EmpName,b.Dept,b.DeptID,isnull(Noofpayment,0) as Noofpayment,isnull(UnitRate,0) as UnitRate,isnull(UnitAmount,0) as UnitAmount,isnull(MaxLimit,0)as MaxLimit,isnull(PayableAmount,0)as PayableAmount  from ProcessFlowDetPaymentSummery a" 
                + " inner join Emp_Details b on a.ApplicantID=b.EmpID"
                + " where PaymentReference='" + reference.ToString() + "' and Type='" + OF.ToString() + "'";

        DC.Execute(sql, out dummy, 1);


        sql = "if exists(Select * from sysobjects where name = 'ViewProcessOTDataSummeryDepartmentWise' ) begin drop view ViewProcessOTDataSummeryDepartmentWise end  ";
        DC.Execute(sql, out dummy, 1);
        sql = "Create view [dbo].[ViewProcessOTDataSummeryDepartmentWise] as"
                + " select b.EmpID,b.EmpName,b.Dept,b.DeptID,isnull(Noofpayment,0) as Noofpayment ,isnull(UnitRate,0) as UnitRate,isnull(UnitAmount,0) as UnitAmount,isnull(MaxLimit,0) as MaxLimit,isnull(PayableAmount,0) as PayableAmount from ProcessFlowDetPaymentSummery a"
                + " inner join Emp_Details b on a.ApplicantID=b.EmpID"
                + " where PaymentReference='" + reference.ToString() + "' and Type='" + OT.ToString() + "'";

        DC.Execute(sql, out dummy, 1);


        sql = "if exists(Select * from sysobjects where name = 'ViewProcessNTDataSummeryDepartmentWise' ) begin drop view ViewProcessNTDataSummeryDepartmentWise end  ";
        DC.Execute(sql, out dummy, 1);
        sql = "Create view [dbo].[ViewProcessNTDataSummeryDepartmentWise] as"
                + " select b.EmpID,b.EmpName,b.Dept,b.DeptID,isnull(Noofpayment,0)as Noofpayment,isnull(UnitRate,0)as UnitRate,isnull(UnitAmount,0) as UnitAmount,isnull(MaxLimit,0) as MaxLimit,isnull(PayableAmount,0) as PayableAmount from ProcessFlowDetPaymentSummery a"
                + " inner join Emp_Details b on a.ApplicantID=b.EmpID"
                + " where PaymentReference='" + reference.ToString() + "' and Type='" + NT.ToString() + "'";

        DC.Execute(sql, out dummy, 1);

        sql = "if exists(Select * from sysobjects where name = 'ViewProcessOFOTNT' ) begin drop view ViewProcessOFOTNT end  ";
        DC.Execute(sql, out dummy, 1);
        sql = "create view ViewProcessOFOTNT as" 
            + " select EmpID from ViewProcessOFDataSummeryDepartmentWise"
            + " union "
            + " select EmpID from ViewProcessOTDataSummeryDepartmentWise"
            + " union "
            + " select EmpID from ViewProcessNTDataSummeryDepartmentWise";

        DC.Execute(sql, out dummy, 1);

        rptname = "ProcessoffDayOverTimeNightCal.rpt";

        //if (RadioButtonList1.SelectedIndex == 0)
        //{
        //    rptname = "offDayOverTimeNightCal.rpt";
        //}
        //else
        //{
        //    rptname = "offDayOverTimeNightCalbyDept.rpt";
        //}

        showreport(rptname, reference);                    
       
    }

    private void ExportToXLDataPrepare(string reference, string type)
    {
        int day = DateTime.DaysInMonth(2013, 2);

        Connection DC = new Connection();
        string constr, sql;
        sql = "";
        object dummy = Type.Missing;
        string rptname = "";

        string[] tp = new string[3];

        string OF = "";
        string OT = "";
        string NT = "";

        if (type.ToString().Length == 2)
        {
            tp[0] = type.Substring(0, 2).ToString();
            tp[1] = "";
            tp[2] = "";
        }
        else if (type.ToString().Length == 5)
        {
            tp[0] = type.Substring(0, 2).ToString();
            tp[1] = type.Substring(3, 2).ToString();
            tp[2] = "";
        }
        else if (type.ToString().Length == 8)
        {
            tp[0] = type.Substring(0, 2).ToString();
            tp[1] = type.Substring(3, 2).ToString();
            tp[2] = type.Substring(6, 2).ToString();
        }


        for (int j = 0; j < tp.Length; j++)
        {
            if (tp[j].ToString() == "OF")
            {
                OF = tp[j].ToString();
            }
            else if (tp[j].ToString() == "OT")
            {
                OT = tp[j].ToString();
            }
            else if (tp[j].ToString() == "NT")
            {
                NT = tp[j].ToString();
            }
        }

        constr = System.Configuration.ConfigurationManager.AppSettings["ConStrLNK"].ToString();

        DC.Open(constr, null, null, 0);

        sql = "if exists(Select * from sysobjects where name = 'ViewProcessOFDataSummeryDepartmentWise' ) begin drop view ViewProcessOFDataSummeryDepartmentWise end  ";
        DC.Execute(sql, out dummy, 1);
        sql = "Create view [dbo].[ViewProcessOFDataSummeryDepartmentWise] as"
                + " select b.EmpID,b.EmpName,b.Dept,b.DeptID,isnull(Noofpayment,0) as Noofpayment,isnull(UnitRate,0) as UnitRate,isnull(UnitAmount,0) as UnitAmount,isnull(MaxLimit,0)as MaxLimit,isnull(PayableAmount,0)as PayableAmount  from ProcessFlowDetPaymentSummery a"
                + " inner join Emp_Details b on a.ApplicantID=b.EmpID"
                + " where PaymentReference='" + reference.ToString() + "' and Type='" + OF.ToString() + "'";

        DC.Execute(sql, out dummy, 1);


        sql = "if exists(Select * from sysobjects where name = 'ViewProcessOTDataSummeryDepartmentWise' ) begin drop view ViewProcessOTDataSummeryDepartmentWise end  ";
        DC.Execute(sql, out dummy, 1);
        sql = "Create view [dbo].[ViewProcessOTDataSummeryDepartmentWise] as"
                + " select b.EmpID,b.EmpName,b.Dept,b.DeptID,isnull(Noofpayment,0) as Noofpayment ,isnull(UnitRate,0) as UnitRate,isnull(UnitAmount,0) as UnitAmount,isnull(MaxLimit,0) as MaxLimit,isnull(PayableAmount,0) as PayableAmount from ProcessFlowDetPaymentSummery a"
                + " inner join Emp_Details b on a.ApplicantID=b.EmpID"
                + " where PaymentReference='" + reference.ToString() + "' and Type='" + OT.ToString() + "'";

        DC.Execute(sql, out dummy, 1);


        sql = "if exists(Select * from sysobjects where name = 'ViewProcessNTDataSummeryDepartmentWise' ) begin drop view ViewProcessNTDataSummeryDepartmentWise end  ";
        DC.Execute(sql, out dummy, 1);
        sql = "Create view [dbo].[ViewProcessNTDataSummeryDepartmentWise] as"
                + " select b.EmpID,b.EmpName,b.Dept,b.DeptID,isnull(Noofpayment,0)as Noofpayment,isnull(UnitRate,0)as UnitRate,isnull(UnitAmount,0) as UnitAmount,isnull(MaxLimit,0) as MaxLimit,isnull(PayableAmount,0) as PayableAmount from ProcessFlowDetPaymentSummery a"
                + " inner join Emp_Details b on a.ApplicantID=b.EmpID"
                + " where PaymentReference='" + reference.ToString() + "' and Type='" + NT.ToString() + "'";

        DC.Execute(sql, out dummy, 1);

        sql = "if exists(Select * from sysobjects where name = 'ViewProcessOFOTNT' ) begin drop view ViewProcessOFOTNT end  ";
        DC.Execute(sql, out dummy, 1);
        sql = "create view ViewProcessOFOTNT as"
            + " select EmpID from ViewProcessOFDataSummeryDepartmentWise"
            + " union "
            + " select EmpID from ViewProcessOTDataSummeryDepartmentWise"
            + " union "
            + " select EmpID from ViewProcessNTDataSummeryDepartmentWise";

        DC.Execute(sql, out dummy, 1);


        sql = "select *,'OF' as Type from ViewProcessOFDataSummeryDepartmentWise"
            + " union all"
            + " select *,'OT' as Type from ViewProcessOTDataSummeryDepartmentWise"
            + " union all"
            + " select *,'NT' as Type from ViewProcessNTDataSummeryDepartmentWise";

        DataTable dt = new DataTable();        
        dt = LoadApprovedOFOTNTDataForPaymentExport();
        gdvExport.DataSource = dt;
        gdvExport.DataBind(); 
       

    }

    private void Export2Excel(DataTable dt, string savename)
    {
        try
        {           
            
            //SaveFileDialog sfd = new SaveFileDialog();

            //sfd.FileName = savename.ToString() + ".xls";

            //if (sfd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            //{
            //    return;
            //}

            //string dd = sfd.FileName.ToString();
            string dd = "TT";

            Microsoft.Office.Interop.Excel.ApplicationClass ExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();

            ExcelApp.Application.Workbooks.Add(Type.Missing);

            int col = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                for (int j = 1; j < dt.Columns.Count + 1; j++)
                {
                    if (i == 0 && j == 1)
                    {
                        foreach (DataColumn column in dt.Columns)
                        {
                            col = col + 1;
                            ExcelApp.Cells[i + 1, col] = column.ColumnName;
                        }
                    }

                    ExcelApp.Cells[i + 2, (j - 1) + 1] = dt.Rows[i][j - 1].ToString();

                }

            }

            ExcelApp.ActiveWorkbook.SaveCopyAs(dd);

            ExcelApp.ActiveWorkbook.Saved = true;

            ExcelApp.Quit();           

        }
        catch (Exception ex)
        {
        }
    }


    private void showreport(string rptname, string reference)
    {
        Connection DC = new Connection();

        DateTime dtfr, dtto, dtfr1;
        string[] ff, ss;
        string str, vcoa, vac;

        ParameterFields paramFields = new ParameterFields();

        ParameterField paramField2 = new ParameterField();
        ParameterDiscreteValue discreteVal2 = new ParameterDiscreteValue();

        ParameterField paramField3 = new ParameterField();
        ParameterDiscreteValue discreteVal3 = new ParameterDiscreteValue();

        ParameterField paramField4 = new ParameterField();
        ParameterDiscreteValue discreteVal4 = new ParameterDiscreteValue();


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


        string rep = "";

        clsReport rpt = new clsReport();



        //paramField2.ParameterFieldName = "Fromdate";
        //discreteVal2.Value ="01/01/2013";
        //paramField2.CurrentValues.Add(discreteVal2);
        //paramFields.Add(paramField2);

        //paramField3.ParameterFieldName = "todate";
        //discreteVal3.Value = "31/01/2013";
        //paramField3.CurrentValues.Add(discreteVal3);
        //paramFields.Add(paramField3);

        paramField4.ParameterFieldName = "PaymentReferance";
        discreteVal4.Value = reference.ToString();
        paramField4.CurrentValues.Add(discreteVal4);
        paramFields.Add(paramField4);
        

        rpt.FileName = "../REPORT/" + rptname + "";
        rpt.ConnectionInfo = ConnInfo;
        rpt.ParametersFields = paramFields;
        rpt.SelectionFormulla = rep;

        string qrystr = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
        //Session[qrystr] = rpt;
        Cache.Insert(qrystr, rpt);


       // RegisterStartupScript("Click", "<script>window.open('./frm_report_viewer.aspx?session_id_no=" + qrystr + "');</script>");

        String js = "window.open('./frm_report_viewer.aspx?session_id_no=" + qrystr + "', '_blank');";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Open frm_report_viewer.aspx", js, true);

    }

    protected void BtnShowApprovedData_Click(object sender, EventArgs e)
    {
        if (txtDatefrom.Text == "" || txtDateTo.Text == "") return;

        DateTime fDate = Convert.ToDateTime(txtDatefrom.Text);
        DateTime lDate = Convert.ToDateTime(txtDateTo.Text);

        string type = "";

        for (int i = 0; i < ChkPmttypeForPayment.Items.Count; i++)
        {
            if (ChkPmttypeForPayment.Items[i].Selected)
            {
                if (i == 0)
                {
                    type = ChkPmttypeForPayment.Items[i].Value;
                }
                else
                {
                    if (type == "")
                    {
                        type = ChkPmttypeForPayment.Items[i].Value;
                    }
                    else
                    {
                        type = type + "," + ChkPmttypeForPayment.Items[i].Value;
                    }

                }
            }
        }

        LoadApprovedOFOTNTDataReadyForPayment(fDate, lDate, type);
    }
    protected void GdvPaymentInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);

            Button bt = e.Row.FindControl("btnPayment") as Button; 

            string pp=Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PaymentFlag"));

            if(pp=="Y")
            {
                e.Row.Cells[9].Text = "PAID";                
                bt.Visible = false;               
                e.Row.Cells[9].ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                e.Row.Cells[9].Text = "PENDING";
                e.Row.Cells[9].ForeColor = System.Drawing.Color.Green;
                bt.Enabled = true;
            }

        }
    }
    protected void btnShowOffday_Click(object sender, EventArgs e)
    {
        if (txtoffdayFromDate.Text == "" || txtoffdayToDate.Text == "") return;

        DateTime fDate = Convert.ToDateTime(txtoffdayFromDate.Text);
        DateTime lDate = Convert.ToDateTime(txtoffdayToDate.Text);

        string type = "";

        for (int i = 0; i < ChkPmttypeForSearch.Items.Count; i++)
        {
            if (ChkPmttypeForSearch.Items[i].Selected)
            {
                if (i == 0)
                {
                    type = ChkPmttypeForSearch.Items[i].Value;
                }
                else
                {
                    if (type == "")
                    {
                        type = ChkPmttypeForSearch.Items[i].Value;
                    }
                    else
                    {
                        type = type + "," + ChkPmttypeForSearch.Items[i].Value;
                    }

                }
            }
        }

        LoadApprovedOFOTNTReadyforPaymentLocked(fDate, lDate, type);

    }
    protected void GridViewPendingLocked_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);

            // e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
            // e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GridViewPendingLocked, "Select$" + e.Row.RowIndex);



            if (e.Row.Cells[5].Text == "OF")
            {
                e.Row.Cells[6].Text = "Off day";
                e.Row.Cells[8].Text = "Days";
            }
            if (e.Row.Cells[5].Text == "OT")
            {
                e.Row.Cells[6].Text = "Over Time";
                e.Row.Cells[8].Text = "Minute";
            }
            if (e.Row.Cells[5].Text == "NT")
            {
                e.Row.Cells[6].Text = "Night";
                e.Row.Cells[8].Text = "Days";
            }

            PayableAmt = PayableAmt + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Payableamt"));

            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;


            e.Row.Cells[9].Text = Convert.ToDecimal(e.Row.Cells[9].Text).ToString("#,##0.00");
            e.Row.Cells[10].Text = Convert.ToDecimal(e.Row.Cells[10].Text).ToString("#,##0.00");

        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[9].Text = "TOTAL:";
            // for the Footer, display the running totals
            e.Row.Cells[10].Text = PayableAmt.ToString("#,##0.00");
            e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Font.Bold = true;
        }

        e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
        e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;

        e.Row.Cells[5].Visible = false;
        e.Row.Cells[11].Visible = false;
        e.Row.Cells[12].Visible = false;
        e.Row.Cells[13].Visible = false;
        e.Row.Cells[14].Visible = false;
    }
    protected void GridViewPendingLocked_SelectedIndexChanged(object sender, EventArgs e)
    {
        double amt = 0;
        int indx = GridViewPendingLocked.SelectedIndex;

        if (indx == -1)
        {
            return;
        }
        for (int i = 0; i < GridViewPendingLocked.Rows.Count; i++)
        {
            CheckBox chkbox = GridViewPendingLocked.Rows[i].FindControl("CheckRet") as CheckBox;
            if (chkbox.Checked == true)
            {
                amt = amt + Convert.ToDouble(GridViewPendingLocked.Rows[i].Cells[10].Text);

            }
        }

        GridViewPendingLocked.FooterRow.Cells[10].Text = amt.ToString("#,##0.00");        
    }
    protected void GridViewPendingLocked_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GdvPaymentInfo_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void btmExport1_Click(object sender, EventArgs e)
    {
        string type = "";

        for (int i = 0; i < ChkPmttypeForSearch.Items.Count; i++)
        {
            if (ChkPmttypeForSearch.Items[i].Selected)
            {
                if (i == 0)
                {
                    type = ChkPmttypeForSearch.Items[i].Value;
                }
                else
                {
                    if (type == "")
                    {
                        type = ChkPmttypeForSearch.Items[i].Value;
                    }
                    else
                    {
                        type = type + "_" + ChkPmttypeForSearch.Items[i].Value;
                    }

                }
            }
        }

        if (type == "")
        {
            type = "OF_OT_NT.xls";
        }
        else
        {
            type = type + ".xls"; 
        }
              

        Export(type, GridViewPendingLocked);
    }
    protected void btmExport2_Click(object sender, EventArgs e)
    {
        string type = "";
        
        for (int i = 0; i < ChkPmttypeForPayment.Items.Count; i++)
        {
            if (ChkPmttypeForPayment.Items[i].Selected)
            {
                if (i == 0)
                {
                    type = ChkPmttypeForPayment.Items[i].Value;
                }
                else
                {
                    if (type == "")
                    {
                        type = ChkPmttypeForPayment.Items[i].Value;
                    }
                    else
                    {
                        type = type + "," + ChkPmttypeForPayment.Items[i].Value;
                    }

                }
            }
        }

        if (type == "")
        {
            type = "OF_OT_NT.xls";
        }
        else
        {
            type = type + ".xls";
        }

        Export(type, GdvOffdayPaymentData);        
    }
}
