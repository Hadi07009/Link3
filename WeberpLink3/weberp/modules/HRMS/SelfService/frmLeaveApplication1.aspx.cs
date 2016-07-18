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
using System.Net;
using System.Net.NetworkInformation;


public partial class ClientSide_modules_mis_naz_FORMS_HRMS_Forms_frmLeaveApplication1 : System.Web.UI.Page
{
    string ConnectionStr = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    string PID ="P001";
    string FID = "1";
    DateTime ServerDate = DateProcess.GetServerDate(System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString());
    private DocumentUploadController _objDocumentUploadController;
    private DocumentUpload _objDocumentUpload;
    string filepath = System.Configuration.ConfigurationSettings.AppSettings["upl"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {           
            DateTime fDate, lDate;           
            fDate = Convert.ToDateTime(ReturnDecissinon().Split('@')[0]);
            lDate = Convert.ToDateTime(ReturnDecissinon().Split('@')[1]);

            //Session["ApplicantID"] = "SSP-0006";
            //Session["EntryUserid"] = "SSP-0006";

            Session["ApplicantID"] = Session[StaticData.sessionUserId].ToString();
            Session["EntryUserid"] = Session[StaticData.sessionUserId].ToString();
            Session["fdate"] = fDate;
            Session["lDate"] = lDate;
            
            LoadAttendanceDetailsemployeeID(Session["ApplicantID"].ToString(),PID,FID, fDate, lDate);
            LoadLeavBALeByemployeeID(Session["ApplicantID"].ToString(), fDate);
            LoadEmployeeInformation(Session["ApplicantID"].ToString());
            LoadResponsiblePersonID(Session["ApplicantID"].ToString());
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
        dttask = DataProcess.GetData(ConnectionStr, "select * from ProcessTaskDateAllemoloyee where TaskType='LV'");
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
        ret = fDate + "@" + lDate;
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
        dt = DataProcess.GetData(ConnectionStr, "select EmpID,EmpName,Dept,Designation,b.Emp_Mas_Join_Date as Joiningdate from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id where EmpID='" + empid + "'");
        if (dt.Rows.Count > 0)
        {
            lblId.Text = dt.Rows[0]["EmpID"].ToString();
            lblName.Text = dt.Rows[0]["EmpName"].ToString();
            lbldept.Text = dt.Rows[0]["Dept"].ToString();
            lblDesignation.Text = dt.Rows[0]["Designation"].ToString();
            lblJoiningDate.Text = dt.Rows[0]["Joiningdate"].ToString().Substring(0,10);
        }
        string ffff = "";
        Image1.ImageUrl = "~/ClientSide/modules/mis/naz/FORMS/HRMS/forms/hndImage.ashx?id=" + ffff;
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
        cmd.Parameters.Add(new SqlParameter("@empid", SqlDbType.NVarChar)).Value = empid;   
        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        sqlConn.Close();
        dplResponsible.Items.Add("");
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                ListItem lst = new ListItem();
                lst.Value = dr["EmpID"].ToString();
                lst.Text = dr["EmpID"]+":"+dr["EmpName"];
                dplResponsible.Items.Add(lst);
            }
        }
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

    private void LoadAttendanceDetailsemployeeID(string empid, string Processid, string flowid, DateTime fDate, DateTime lDate)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetAttendanceStatusByEmpid";
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
        string[] ff, ss;        
        ParameterFields paramFields = new ParameterFields();
        ConnectionInfo ConnInfo = new ConnectionInfo();
        string constr = System.Configuration.ConfigurationManager.AppSettings["ConStrLNK"];
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
        myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        try
        {
            DataTable dt = new DataTable();
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
        SqlParameter outputStr = cmd.Parameters.Add("@outputStr", SqlDbType.NVarChar, 100);
        outputStr.Direction = ParameterDirection.Output;
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
                    + " inner join HrMs_Emp_mas b on b.Emp_Mas_Emp_Type=a.T_C2 and b.Emp_Mas_Emp_Id='" + Session["ApplicantID"] + "'"
                    + " inner join Hrms_Emp_Leave_Info c on c.Hrms_Emp_Id=b.Emp_Mas_Emp_Id and c.Leave_Type=a.Leave_Mas_Code" 
                    + " where a.T_FL=1 and Leave_Mas_Code not in"
                    + " ("
                    + " select case when Emp_Mas_Gender='M' then 'ML' else ''end from hrms_leave_mas a"
                    + " inner join HrMs_Emp_mas b on b.Emp_Mas_Emp_Type=a.T_C2 and b.Emp_Mas_Emp_Id='" + Session["ApplicantID"] + "') order by Leave_mas_Name";
        
        dtl = DataProcess.GetData(ConnectionStr, sql);

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var lbSl = e.Row.FindControl("lblSL") as Label;
            if (lbSl != null) lbSl.Text = Convert.ToString(e.Row.RowIndex + 1);
            CheckBox chkbox = e.Row.FindControl("Checklv") as CheckBox;
            TextBox tb = e.Row.FindControl("txtlvRemarks") as TextBox;
            Label lb = e.Row.FindControl("txtlvstatus") as Label;
            tb.Width = 200;
            DropDownList dpl = e.Row.FindControl("dpllday") as DropDownList;

            Label lblatsts = e.Row.FindControl("lblStatus") as Label;

            
            ListItem lst1 = new ListItem();            
            lst1.Text = "--Select--";
            lst1.Value = "0";
            dpl.Items.Add(lst1);
            ListItem lst2 = new ListItem();
            lst2.Text = "Full Day";
            lst2.Value = "1";
            dpl.Items.Add(lst2);
            ListItem lst3 = new ListItem();
            if (lblatsts.Text != "H")
            {
                lst3.Text = "Half Day";
                lst3.Value = "0.5";
                dpl.Items.Add(lst3);
            }
            dpl.Width = 100;

            var lblNoOfDays = e.Row.FindControl("lblNoOfDay") as Label;
            if (lblNoOfDays != null) dpl.SelectedValue = lblNoOfDays.Text;
            var dpllt = e.Row.FindControl("lvtype") as DropDownList;
            var lstlt = new ListItem {Text = "--Select--", Value = ""};
            dpllt.Items.Add(lstlt);
            foreach (DataRow dr in dtl.Rows)
            {
                ListItem lstlv = new ListItem();
                lstlv.Value = dr["Leave_mas_code"].ToString();
                lstlv.Text = dr["Leave_mas_Name"].ToString();
                dpllt.Items.Add(lstlv);
            }
            dpllt.Width = 150;
            var lblType = e.Row.FindControl("lblType") as Label;
            if (lblType != null) dpllt.SelectedValue = lblType.Text;
            var lbDate = e.Row.FindControl("lblDate") as Label;
            DateTime dta = Convert.ToDateTime(lbDate.Text);
            Label lblAction = e.Row.FindControl("lblAction") as Label;
            lb.Text = lblAction.Text;
            Label lblProcessLevelid = e.Row.FindControl("lblProcessLevelid") as Label;
            Label lblActionTypeID = e.Row.FindControl("lblActionTypeID") as Label;
            Label lblStatus = e.Row.FindControl("lblStatus") as Label;
            if (Convert.ToInt32(lblProcessLevelid.Text) > 0 && Convert.ToInt32(lblActionTypeID.Text) != 3)
            {
                dpl.Enabled = false;
                dpllt.Enabled = false;
                chkbox.Enabled = false;
                tb.Enabled = false;
                e.Row.BackColor = System.Drawing.Color.LightGreen;
                lblStatus.Text = "Leave";
            }
            if (Convert.ToInt32(lblActionTypeID.Text) == 3)
            {
                dpl.Enabled = true;
                dpllt.Enabled = true;
                chkbox.Enabled = true;
                tb.Enabled = true;
                e.Row.BackColor = System.Drawing.Color.Yellow;
            }
            //if (dta < Convert.ToDateTime(Session["fdate"].ToString()))
            //{
            //    chkbox.Enabled = false;
            //    tb.Enabled = false;
            //    lb.Enabled = false;
            //    dpl.Enabled = false;
            //    dpllt.Enabled = false;
            //}
            Label lblIntime = e.Row.FindControl("lblIntime") as Label;
            Label lblOuttime = e.Row.FindControl("lblOuttime") as Label;
            Label lblHours = e.Row.FindControl("lblHours") as Label;
            if (lblStatus.Text == "H")
            {
                lblStatus.Text = "Holiday";
                //lblIntime.Text = "-";
                //lblOuttime.Text = "-";
                //lblHours.Text = "-";
                e.Row.BackColor = System.Drawing.Color.LightGray;
                //chkbox.Visible = false;
                //tb.Visible = false;
                //lb.Visible = false;
                //dpl.Visible = false;
                //dpllt.Visible = false; 
            }
            if (lblStatus.Text == "O")
            {
                lblStatus.Text = "Holiday";
                //lblIntime.Text = "-";
                //lblOuttime.Text = "-";
                //lblHours.Text = "-";
                e.Row.BackColor = System.Drawing.Color.LightGray;
                //chkbox.Visible = false;
                //tb.Visible = false;
                //lb.Visible = false;
                //dpl.Visible = false;
                //dpllt.Visible = false; 
            }
            var lblIsProcessLocked = e.Row.FindControl("lblIsProcessLocked") as Label;
            if (lblStatus.Text == "L" || lblStatus.Text == "N")
            {
                lblStatus.Text = "Leave";
                e.Row.BackColor = System.Drawing.Color.LightGreen;
                chkbox.Enabled = false;
                tb.Enabled = false;
                if (lblIsProcessLocked != null) lb.Text = lblIsProcessLocked.Text;
                dpl.Enabled = false;
                dpllt.Enabled = false;
            }
            if (lblStatus.Text == "A")
            {
                lblStatus.Text = "Present";
                chkbox.Enabled = true;
                tb.Enabled = true; 
            }
            if (lblStatus.Text == "P")
            {
                lblStatus.Text = "Absent";
                lblStatus.ForeColor = System.Drawing.Color.Red;
                var cellIndexOfStatus = GetColumnIndexByName(e.Row, "Status");
                e.Row.Cells[cellIndexOfStatus].ForeColor = System.Drawing.Color.Red;
                chkbox.Enabled = true;
                tb.Enabled = true;
            }
            if (lblStatus.Text == "Z")
            {
                lblStatus.Text = "";
                chkbox.Enabled = true;
                tb.Enabled = true;
            }
            //if (dta < Convert.ToDateTime(Session["fdate"].ToString()))
            //{
            //    chkbox.Enabled = false;
            //    tb.Enabled = false;
            //    lb.Enabled = false;
            //    dpl.Enabled = false;
            //    dpllt.Enabled = false;
            //}

            Label lblIsLineLocked = e.Row.FindControl("lblIsLineLocked") as Label;
            if (lblIsLineLocked.Text == "Y" && Convert.ToInt32(lblActionTypeID.Text) != 3)
            {
                chkbox.Enabled = false;
                tb.Enabled = false;
                lb.Enabled = false;
                dpl.Enabled = false;
                dpllt.Enabled = false;
            }                       
        }
        var comments = GetColumnIndexByName(e.Row, "Comments");
        e.Row.Cells[comments].Visible = false;
        var type = GetColumnIndexByName(e.Row, "Type");
        e.Row.Cells[type].Visible = false;
        var noofd = GetColumnIndexByName(e.Row, "noofd");
        e.Row.Cells[noofd].Visible = false;
        var remarks = GetColumnIndexByName(e.Row, "Remarks");
        e.Row.Cells[remarks].Visible = false;
        var lockedL = GetColumnIndexByName(e.Row, "LockedL");
        e.Row.Cells[lockedL].Visible = false;
        var lockedP = GetColumnIndexByName(e.Row, "LockedP");
        e.Row.Cells[lockedP].Visible = false;
        var action = GetColumnIndexByName(e.Row, "Action");
        e.Row.Cells[action].Visible = false;
        var pLid = GetColumnIndexByName(e.Row, "PLid");
        e.Row.Cells[pLid].Visible = false;
        var actionTypeId = GetColumnIndexByName(e.Row, "ActionTypeID");
        e.Row.Cells[actionTypeId].Visible = false;
        var astatus = GetColumnIndexByName(e.Row, "AStatus");
        e.Row.Cells[astatus].Visible = false;




    }
    int GetColumnIndexByName(GridViewRow row, string columnName)
    {
        int columnIndex = 0;
        foreach (DataControlFieldCell cell in row.Cells)
        {
            if (cell.ContainingField is TemplateField)
                if (((TemplateField)cell.ContainingField).HeaderText.Equals(columnName))
                    break;
            columnIndex++;
        }
        return columnIndex;
    }
   
    protected void btnApply_Click(object sender, EventArgs e)
    {
        SqlConnection myConnection = new SqlConnection(ConnectionStr);
        myConnection.Open();
        SqlCommand myCommand = myConnection.CreateCommand();
       
        SqlTransaction myTrans;


        bool uploadDocument = false;
        string msg = "";
        double rCL =0;
        double rSL =0;
        double rAL =0;
        string leavedate = "";
        string leavetype = "";
        double leavedays = 0.0;
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
            if (gdvLeaveInfo.Rows[j].Cells[0].Text == "AL")
            {
                rAL = Convert.ToDouble(gdvLeaveInfo.Rows[j].Cells[4].Text);
            }
        }

        //Sandwich leave check
        string sql = "";
        string AppID = Session["ApplicantID"].ToString();
        sql = "delete from [Hrms_Emp_Leave_Det_Apply] where Empid='" + AppID + "'";
        DataProcess.ExecuteQuery(ConnectionStr, sql);

        for (int sw = 0; sw < GridViewLeave.Rows.Count; sw++)
        {           
            
            DateTime lvdate;
            CheckBox chkb= GridViewLeave.Rows[sw].FindControl("Checklv") as CheckBox;
            if (chkb.Checked == true)
            {
                Label lblDate = GridViewLeave.Rows[sw].FindControl("lblDate") as Label;
                DropDownList dpllvtp = GridViewLeave.Rows[sw].FindControl("lvtype") as DropDownList;
                lvdate = Convert.ToDateTime(lblDate.Text);
                string lvtype=dpllvtp.SelectedItem.Value;
                sql = "insert into [Hrms_Emp_Leave_Det_Apply]([Empid],[LeaveDate],[LeaveType])values('" + AppID + "',convert(Datetime,'" + lvdate + "',103),'" + lvtype + "')";
                DataProcess.ExecuteQuery(ConnectionStr, sql);
            } 
        }


               
        Process processobj = new Process();
        string outputmsg = processobj.CheckSandwichLeave(ConnectionStr, AppID);
        if (outputmsg.Trim() != "")
        {
            outputmsg = "Please select sandwich leave on " + outputmsg;
            MessageBox1.ShowWarning(outputmsg);
            return;
        }

        //end Sandwich leave check


        if (dplResponsible.SelectedIndex==0)
        {
            msg = "Please Select Responsible Person Name";
            //System.Windows.Forms.MessageBox.Show(msg);
            MessageBox1.ShowWarning(msg);
            
            return;
        }
        if (CheckLeaveValidation(rCL, rSL, rAL).Split('@')[1] == "No")
        {
            msg =CheckLeaveValidation(rCL, rSL, rAL).Split('@')[0];
            //System.Windows.Forms.MessageBox.Show(msg);
            MessageBox1.ShowWarning(msg);
            return;
        }

        myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        try
        {
            List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
            LeaveProcess lvp=new LeaveProcess();
            string Transactionno = lvp.GetTransactionNo(ConnectionStr);
            int movementno = Convert.ToInt32(lvp.GetMovementNo(ConnectionStr));
            for (int i = 0; i < GridViewLeave.Rows.Count; i++)
            {
                string ProcessID =PID;
                string FlowID =FID;                
                string ApplicantID = Session["ApplicantID"].ToString();
                CheckBox chkbox = GridViewLeave.Rows[i].FindControl("Checklv") as CheckBox;
                if (chkbox.Checked == true)
                {
                    uploadDocument = true;
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();
                    TextBox tb = GridViewLeave.Rows[i].FindControl("txtlvRemarks") as TextBox;
                    DropDownList dpllvno = GridViewLeave.Rows[i].FindControl("dpllday") as DropDownList;
                    DropDownList dpllvtype = GridViewLeave.Rows[i].FindControl("lvtype") as DropDownList;
                    Label lblSL = GridViewLeave.Rows[i].FindControl("lblSL") as Label;
                    Label lblDate = GridViewLeave.Rows[i].FindControl("lblDate") as Label;
                    Label lblIntime = GridViewLeave.Rows[i].FindControl("lblIntime") as Label;
                    Label lblOuttime = GridViewLeave.Rows[i].FindControl("lblOuttime") as Label;
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
                    offphdr.SysOuttime = lblOuttime.Text == "&nbsp;" ? "00:00 AM" : lblOuttime.Text;
                    offphdr.SysTotalhrs = lblHours.Text == "&nbsp;" ? "00:00" : lblHours.Text;
                    offphdr.NoofLeave = Convert.ToDouble(dpllvno.SelectedItem.Value);
                    offphdr.Leavetype = dpllvtype.SelectedItem.Value;
                    offphdr.Leavedescription = dpllvtype.SelectedItem.Text;
                    offphdr.Remarks =txtRemarks.Text.Replace("'","");
                    offphdr.Acthrs = "00:00";
                    offphdr.ResponsiblepersonId = dplResponsible.SelectedItem.Value;
                    offphdr.MovementNo = movementno;
                    offphdr.EntryUserid = Session["EntryUserid"].ToString();
                    if (leavedate == "")
                    {
                        leavedate = offphdr.ClaiminDdate.ToShortDateString();
                        leavetype = dpllvtype.SelectedItem.Text;
                        leavedays = offphdr.NoofLeave;
                    }
                    else
                    {
                        leavedate = leavedate + "," + offphdr.ClaiminDdate.ToShortDateString();
                        leavetype = leavetype + "," + dpllvtype.SelectedItem.Text;
                        leavedays = leavedays + offphdr.NoofLeave;
                    }                   
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
            if (retval != "")
            {
                string str = SendMailtoApprover(leavedate, leavetype, leavedays);
                //string user = str.Split(':')[0];
                //string mobileno = str.Split(':')[1];
                //string smsstr = str.Split(':')[2];
                //SendSMStoMySql(user, smsstr, mobileno);
                MessageBox1.ShowSuccess("Leave application successful");               

            }

            if (uploadDocument == true)
            {
                AttachFileSave(Transactionno);
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


    private void CheckSandwichLeave()
    {
        List<LeaveProcessHeader> lvphdrsw = new List<LeaveProcessHeader>();
        List<LeaveProcessHeader> lvphdrALL = new List<LeaveProcessHeader>();

        for (int i = 0; i < GridViewLeave.Rows.Count; i++)
        {
            Label lblSL = GridViewLeave.Rows[i].FindControl("lblSL") as Label;
            Label lblDate = GridViewLeave.Rows[i].FindControl("lblDate") as Label;
            CheckBox chkbox = GridViewLeave.Rows[i].FindControl("Checklv") as CheckBox;
            
            string ProcessID = PID;
            string FlowID = FID;
            string ApplicantID = Session["ApplicantID"].ToString();
            int chk = 0;
            string flg = "";

            if (chkbox.Checked == true)
            {
                LeaveProcessHeader lvh = new LeaveProcessHeader();                
                lvh.ActIndate = Convert.ToDateTime(lblDate.Text);                
                lvphdrsw.Add(lvh);
                flg = "1";
            }

            DataTable dtall = new DataTable();
            foreach (DataRow dr in dtall.Rows)
            {
                LeaveProcessHeader lvall = new LeaveProcessHeader();
                lvall.ActIndate = Convert.ToDateTime(dr[""].ToString());
                lvall.chkstatus = Convert.ToInt32(flg);
                lvall.flag = "A/L/H";
                lvphdrALL.Add(lvall);
            }
        }

        foreach (LeaveProcessHeader lvhd in lvphdrsw)
        {

 
        }


 
    }

    
    
    private string SendMailtoApprover(string leavedate, string leavetype, double leavedays)
    {
        string user = "";
        try
        {
            string sid = "";
            string msgbody = "";
            string sname = "";
            string rid = "";
            string rname = "";
            string msub = "";
            string mbody = "";
            string ccid = "";


            DataTable dts = DataProcess.GetData(ConnectionStr, "select a.EmpName,b.Emp_Mas_Remarks from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id where Emp_Mas_Emp_Id='" + Session["ApplicantID"] + "'");
            DataTable dtr = DataProcess.GetData(ConnectionStr, "select c.Emp_Mas_Remarks,b.EmpName,isnull(Emp_Mas_HandSet,'') as Emp_Mas_HandSet from ProcessAccessPermission a inner join Emp_Details b on a.AccessId=b.EmpID  inner join hrms_emp_mas c on c.Emp_Mas_Emp_Id=a.AccessId"
                                                             + " where a.ProcessId='P001' and a.ProcessFlowId=1 and a.ProcessLevelid=1 and a.ApplicantID='" + Session["ApplicantID"] + "'");
            DataTable dtcc = DataProcess.GetData(ConnectionStr, "select a.EmpID,b.Emp_Mas_Remarks from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id where Emp_Mas_Emp_Id='" + dplResponsible.SelectedItem.Value + "'");
            string lvd="";
            if (leavedays > 1)
                lvd = leavedays + " days";
            else
                lvd = leavedays + " day";
            msgbody = "I have applied " + lvd + "(" + leavedate + ") " + "(" + leavetype + ")" + " leave application in MIS is pending for your approval.";
            if (dts.Rows.Count > 0)
            {
                sid = dts.Rows[0]["Emp_Mas_Remarks"].ToString();
                sname = dts.Rows[0]["EmpName"].ToString();
            }
            if (dtr.Rows.Count > 0)
            {
                rid = dtr.Rows[0]["Emp_Mas_Remarks"].ToString();
                rname = dtr.Rows[0]["EmpName"].ToString();
            }
            msub = "Leave Application";
            mbody = arrange_data(Session["ApplicantID"].ToString(), sname, msgbody);
            if (dtcc.Rows.Count > 0)
            {
                ccid = dtcc.Rows[0]["Emp_Mas_Remarks"].ToString();
            }
            if (sid != "" && rid != "")
            {
                clsStatic.Sendmail(sid, sname, rid, rname, ccid, msub, mbody, ConnectionStr);
                msgbody = msgbody + "-" + sname;
                user = dts.Rows[0]["EmpName"] + ":" + dtr.Rows[0]["Emp_Mas_HandSet"] + ":" + msgbody;
            }

            return user;
        }
        catch(Exception ex)
        {
            return "";
        }
        
    }

    
    
    private string CheckLeaveValidation(double rCL,double rSL, double rAL)
    {
        string msg = "You have remaining ";
        string msg1 = "Yes";
        double CL=0;
        double SL = 0;
        double AL = 0;
        double nooflv = 0;
        string lvtype="";
        string lvtypeselect = "";
        int jj = 0;
        for (int i = 0; i < GridViewLeave.Rows.Count; i++)
        {                    
            CheckBox chkbox = GridViewLeave.Rows[i].FindControl("Checklv") as CheckBox;
            if (chkbox.Checked == true)
            {
                jj++;

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
                else if (lvtype == "AL")
                {
                    AL = AL + nooflv;
                }             
            }
        }

        if (jj == 0)
        {
            msg = "Please select leave date then apply";
            msg1 = "No";
        }
        else if (lvtypeselect == "NA")
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
                msg = msg + "SL:" + rSL + " but you are going to apply " + SL + ";";
                msg1 = "No";
            }
            if (AL > rAL)
            {
                msg = msg + "AL:" + rAL + " but you are going to apply " + AL + ";";
                msg1 = "No";
            }
        }
        msg = msg +"@" + msg1;
        return msg;
    }

    private void LoadAllInformation()
    {
        DateTime fDate, lDate;
        fDate=Convert.ToDateTime(ReturnDecissinon().Split('@')[0]);
        lDate = Convert.ToDateTime(ReturnDecissinon().Split('@')[1]);
        LoadAttendanceDetailsemployeeID(Session["ApplicantID"].ToString(),PID,FID, fDate, lDate);
        LoadLeavBALeByemployeeID(Session["ApplicantID"].ToString(), fDate);
        LoadEmployeeInformation(Session["ApplicantID"].ToString());
        lblPeriod.Text = FinanialPeriod(fDate);
        lblcurrentPeriod.Text = System.DateTime.Now.ToString();
    }

    protected void btnForward_Click(object sender, EventArgs e)
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
                string ProcessID =PID;
                string FlowID =FID;                
                string ApplicantID =Session["ApplicantID"].ToString();
                string Transactionno = "T130800001";
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId = 2;
                CheckBox chkbox = GridViewLeave.Rows[i].FindControl("Checklv") as CheckBox;
                if (chkbox.Checked == true)
                {
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();
                    TextBox tb = GridViewLeave.Rows[i].FindControl("txtlvRemarks") as TextBox;
                    DropDownList dpllvno = GridViewLeave.Rows[i].FindControl("dpllday") as DropDownList;
                    DropDownList dpllvtype = GridViewLeave.Rows[i].FindControl("lvtype") as DropDownList;
                    offphdr.TransactionNo = Transactionno;
                    offphdr.ApplicantId = ApplicantID;
                    offphdr.ProcessId =PID;
                    offphdr.FlowId =FID;
                    offphdr.ProcesslevelId = 1;
                    offphdr.ProcessnextlevelId = 1;
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.SL = Convert.ToInt32(GridViewLeave.Rows[i].Cells[0].Text);
                    offphdr.ClaiminDdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text);
                    offphdr.SysIndate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text);
                    offphdr.SysOutdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text);
                    offphdr.ActIndate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text);
                    offphdr.ActOutdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text);
                    offphdr.SysIntime = GridViewLeave.Rows[i].Cells[4].Text == "&nbsp;" ? "00:00 AM" : GridViewLeave.Rows[i].Cells[4].Text;
                    offphdr.SysOuttime = GridViewLeave.Rows[i].Cells[5].Text == "&nbsp;" ? "00:00 AM" : GridViewLeave.Rows[i].Cells[5].Text;
                    offphdr.SysTotalhrs = GridViewLeave.Rows[i].Cells[6].Text == "&nbsp;" ? "00:00" : GridViewLeave.Rows[i].Cells[6].Text;
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
                string ApplicantID = Session["ApplicantID"].ToString();
                string Transactionno = "T130800001";
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId = 3;
                CheckBox chkbox = GridViewLeave.Rows[i].FindControl("Checklv") as CheckBox;
                if (chkbox.Checked == true)
                {
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();
                    TextBox tb = GridViewLeave.Rows[i].FindControl("txtlvRemarks") as TextBox;
                    DropDownList dpllvno = GridViewLeave.Rows[i].FindControl("dpllday") as DropDownList;
                    DropDownList dpllvtype = GridViewLeave.Rows[i].FindControl("lvtype") as DropDownList;
                    offphdr.TransactionNo = Transactionno;
                    offphdr.ApplicantId = ApplicantID;
                    offphdr.ProcessId =PID;
                    offphdr.FlowId =FID;
                    offphdr.ProcesslevelId = 1;
                    offphdr.ProcessnextlevelId = 1;
                    offphdr.ActiontypeId = ActionTypeId;
                    offphdr.SL = Convert.ToInt32(GridViewLeave.Rows[i].Cells[0].Text);
                    offphdr.ClaiminDdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text);
                    offphdr.SysIndate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text);
                    offphdr.SysOutdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text);
                    offphdr.ActIndate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text);
                    offphdr.ActOutdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text);
                    offphdr.SysIntime = GridViewLeave.Rows[i].Cells[4].Text == "&nbsp;" ? "00:00 AM" : GridViewLeave.Rows[i].Cells[4].Text;
                    offphdr.SysOuttime = GridViewLeave.Rows[i].Cells[5].Text == "&nbsp;" ? "00:00 AM" : GridViewLeave.Rows[i].Cells[5].Text;
                    offphdr.SysTotalhrs = GridViewLeave.Rows[i].Cells[6].Text == "&nbsp;" ? "00:00" : GridViewLeave.Rows[i].Cells[6].Text;
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
            string retval = lvproc.SaveRejectLeaveProcessData(lvphdrlst, myCommand);
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
    protected void btnPostLeave_Click(object sender, EventArgs e)
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
                string ProcessID =PID;
                string FlowID =FID;                
                string ApplicantID = "L3T593";
                CheckBox chkbox = GridViewLeave.Rows[i].FindControl("Checklv") as CheckBox;
                if (chkbox.Checked == true)
                {
                    LeaveProcessHeader offphdr = new LeaveProcessHeader();
                    TextBox tb = GridViewLeave.Rows[i].FindControl("txtlvRemarks") as TextBox;
                    DropDownList dpllvno = GridViewLeave.Rows[i].FindControl("dpllday") as DropDownList;
                    DropDownList dpllvtype = GridViewLeave.Rows[i].FindControl("lvtype") as DropDownList;
                    offphdr.ApplicantId = ApplicantID;
                    offphdr.SL = Convert.ToInt32(GridViewLeave.Rows[i].Cells[0].Text);
                    offphdr.ClaiminDdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text);
                    offphdr.SysIndate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text);
                    offphdr.SysOutdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text);
                    offphdr.ActIndate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text);
                    offphdr.ActOutdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[1].Text);
                    offphdr.SysIntime = GridViewLeave.Rows[i].Cells[4].Text;
                    offphdr.SysOuttime = GridViewLeave.Rows[i].Cells[5].Text;
                    offphdr.SysTotalhrs = GridViewLeave.Rows[i].Cells[6].Text;
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
        lblPeriod.Text = FinanialPeriod(fDate);
        lblcurrentPeriod.Text = System.DateTime.Now.ToString();
    }
    protected void btnCurrentPeriod_Click(object sender, EventArgs e)
    {
        DateTime fDate = Convert.ToDateTime("01/01/2013");
        DateTime lDate = Convert.ToDateTime("31/01/2013");
        LoadAttendanceDetailsemployeeID(Session["ApplicantID"].ToString(),PID,FID, fDate, lDate);
        LoadLeavBALeByemployeeID(Session["ApplicantID"].ToString(), fDate);
        LoadEmployeeInformation(Session["ApplicantID"].ToString());
        lblPeriod.Text = FinanialPeriod(fDate);
        lblcurrentPeriod.Text = System.DateTime.Now.ToString();
    }
    protected void gdvLeaveInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false; 
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (txtFromDate.Text == "" || txtToDate.Text == "")
        {            
            return;
        }
        DateTime fDate = Convert.ToDateTime(txtFromDate.Text);
        DateTime lDate = Convert.ToDateTime(txtToDate.Text);          
        LoadLeaveByemployeeID(Session["ApplicantID"].ToString(),PID,FID, fDate, lDate);
        btnApply.Visible = false;
        txtRemarks.Visible = false;
        dplResponsible.Visible = false;
        Label5.Visible = false;
        Label6.Visible = false;
    }
    protected void btnShowALL_Click(object sender, EventArgs e)
    {
        DateTime fDate = Convert.ToDateTime(txtFromDate.Text);
        DateTime lDate = Convert.ToDateTime(txtToDate.Text);       
        LoadAttendanceDetailsemployeeID(Session["ApplicantID"].ToString(),PID,FID, fDate, lDate);
        btnApply.Visible = true;
        txtRemarks.Visible = true;
        dplResponsible.Visible = true;
        Label5.Visible = true;
        Label6.Visible = true; 
    }
    private bool Sendmail(string sid, string sname, string rid, string rname, string ccid, string msub, string mbody)
    {
        bool flg = false;
        try
        {
            string smtpadr = "";
            string passkey = "";
            DataTable dt = getSmtp();
            if (dt.Rows.Count>0)
            {
                smtpadr = dt.Rows[0]["Smtp"].ToString();
                sid = dt.Rows[0]["MailFrom"].ToString();
                sname = dt.Rows[0]["Name"].ToString();
                passkey = dt.Rows[0]["Password"].ToString();                
            }

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(smtpadr);
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.From = new System.Net.Mail.MailAddress(sid, sname);
                        
            NetworkCredential Credentials = new NetworkCredential(sid, passkey);
            smtp.Credentials = Credentials;


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

    private DataTable getSmtp()
    {
        DataTable dt = new DataTable();
        string sql = "select MailFrom,Name,[Password],Smtp from TblSmtpSetup where MailTypeId=1 and status=1";
        dt = DataProcess.GetData(ConnectionStr, sql);
        return dt;
    }

    private string arrange_data(string eid, string sname,string body)
    {
        string str = "";
        str = "\nDear Sir,";
        str += "\n\n"+body;
        str += "\n\nEmployee ID : " + eid;
        str += "\nName          : " + sname;
        str += "\n\n\nTo view detail or update this request click the link bellow:\n";        
        str += "https://cebd-payroll/payroll";
        str += "\n\n\n";
        str += "This is auto generated mail from HRIS and Payroll";
        return str;
    }

    private void AttachFileSave(string referenceNo)
    {
        try
        {
            HttpFileCollection hfc = Request.Files;
            _objDocumentUploadController = new DocumentUploadController();
            for (int i = 0; i < hfc.Count; i++)
            {
                HttpPostedFile hpf = hfc[i];
                if (hpf.ContentLength > 0)
                {
                    string referenceno = _objDocumentUploadController.GetReferenceNo(ConnectionStr);
                    string fileName = System.IO.Path.GetFileName(hpf.FileName).Replace("'", "''");
                    string filenameReference = referenceno + "-" + fileName;
                    hpf.SaveAs(filepath + "\\" + filenameReference);
                    Upload(fileName, referenceno, filenameReference);
                    SaveLeaveDocument(referenceNo, filenameReference);
                }
            }
        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }

    private void Upload(string fileName, string referenceNo, string filenemeReference)
    {
        try
        {
            _objDocumentUploadController = new DocumentUploadController();
            _objDocumentUpload = new DocumentUpload();
            _objDocumentUpload.DocumentTypeCode = "4";
            _objDocumentUpload.Description = null;
            _objDocumentUpload.DocumentContent = null;
            _objDocumentUpload.EntryUser = current.UserId;
            _objDocumentUpload.DocumentContent = fileName;
            _objDocumentUpload.documentCode = referenceNo;
            _objDocumentUpload.documentName = filenemeReference;
            _objDocumentUploadController.DocumentUpload(ConnectionStr, _objDocumentUpload);
        }
        catch (Exception msgException)
        {
            MessageBox1.ShowError(msgException.Message);
        }
    }
    public void SaveLeaveDocument(string referenceNumber, string documentCode)
    {
        var storedProcedureComandTest = "exec [LeaveDocumentInitiate_HRMS_Emp_LeaveDocument] '" +
                                        referenceNumber + "','" +
                                        documentCode + "'";
        StoredProcedureExecutor.StoredProcedureExecuteNonQuery(ConnectionStr, storedProcedureComandTest);
    }
}
