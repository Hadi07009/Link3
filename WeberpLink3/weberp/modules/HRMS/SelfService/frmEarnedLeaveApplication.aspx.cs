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

public partial class Modules_HRMS_SelfService_frmEarnedLeaveApplication : System.Web.UI.Page
{
    string ConnectionStr = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    string PID ="P008";
    string FID = "8";

    protected void Page_Load(object sender, EventArgs e)
    {
        StaticData.MsgConfirmBox(btnApply, "Are you sure want to Apply Earned Leave? ");
        if (!Page.IsPostBack)
        {           
            DateTime fDate, lDate;           
            fDate = Convert.ToDateTime(ReturnDecissinon().Split('@')[0].ToString());
            lDate = Convert.ToDateTime(ReturnDecissinon().Split('@')[1].ToString());

            Session["ApplicantID"] = "SSP-0006";
            Session["EntryUserid"] = "SSP-0006";  //login user id  

            //Session["ApplicantID"] = Session[StaticData.sessionUserId].ToString();
            //Session["EntryUserid"] = Session[StaticData.sessionUserId].ToString();

            txtempid.Text = "SSP-0006";
            Session["fdate"] = fDate;
            Session["lDate"] = lDate;

            LoadEarnedLeaveDetailsemployeeID(Session["ApplicantID"].ToString(), PID, FID, fDate, lDate);
            LoadLeavBALeByemployeeID(Session["ApplicantID"].ToString(), fDate);
            LoadEmployeeInformation(Session["ApplicantID"].ToString());
            //LoadResponsiblePersonID(Session["ApplicantID"].ToString());
                                
            //PanelLeaveHdr.Visible=false; 

            LoadAllInformation();
            
            lblPeriod.Text = FinanialPeriod(fDate);
            lblcurrentPeriod.Text = Convert.ToString(System.DateTime.Now.Date.Day) + "-" + Convert.ToString(System.DateTime.Now.Date.Month) + "-" + Convert.ToString(System.DateTime.Now.Date.Year);

            lblRemarks.Text = "Remarks";
            lblRemarks.Visible = false;
            txtRemarks.Visible = false;
            btnApplyAndApprove.Visible = false;
            btnApply.Visible = false;
        }  
    }

    private string ReturnDecissinon()
    {
        string ret = "";
        DataTable dttask = new DataTable();
        DateTime serverdate = DateProcess.GetServerDate(ConnectionStr);
        DateTime fDate = DateProcess.FirstDateOfMonth(serverdate);
        DateTime lDate = DateProcess.LastDateOfMonth(serverdate);

        dttask = DataProcess.GetData(ConnectionStr, "select * from ProcessTaskDateAllemoloyee where TaskType='EL' and TrnMonth=" + serverdate.Month + " and TrnYear=" + serverdate.Year + "");

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
                lst.Text = dr["EmpName"].ToString();

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
        cmd.Parameters.Add(new SqlParameter("@empid", SqlDbType.NVarChar)).Value = empid.ToString();
        cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid.ToString();
        cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid.ToString();

        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);       
        da.Fill(dt);

        GridViewLeave.DataSource = dt;
        GridViewLeave.DataBind();
    }

    private void LoadEarnedLeaveDetailsemployeeID(string empid, string Processid, string flowid, DateTime fDate, DateTime lDate)
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spProcessGetEarnedLeaveStatus";

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


        int i = 0;

        foreach (GridViewRow gdr in GridViewLeave.Rows)
        {
            if (gdr.Cells[27].Text == "S")
            {
                i = i + 1;
            }
 
        }


        if (i >= 1)
        {
            lblRemarks.Visible = true;
            txtRemarks.Visible = true;
            btnApply.Visible = true;
        }
        else
        {
            lblRemarks.Visible = false;
            txtRemarks.Visible = false;
            btnApplyAndApprove.Visible = false;
            btnApply.Visible = false;
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
            
            DropDownList dpllt = e.Row.FindControl("lvtype") as DropDownList;
                      
            ListItem lstlt3 = new ListItem();
            lstlt3.Text = "Earned Leave";
            lstlt3.Value = "EL";
            dpllt.Items.Add(lstlt3);                       
                        
            dpllt.Width = 150;

            dpllt.SelectedValue = e.Row.Cells[15].Text;

            DateTime dta = Convert.ToDateTime(e.Row.Cells[1].Text);

            lb.Text = e.Row.Cells[24].Text;
                                            
            if (e.Row.Cells[26].Text == "S")
            {   chkbox.Enabled = true;
                tb.Enabled = false;
                lb.Enabled = false;                
                dpllt.Enabled = false;
            }
            if (e.Row.Cells[26].Text == "P")
            {
                e.Row.BackColor = System.Drawing.Color.LightGreen;
                chkbox.Enabled = false;
                tb.Enabled = false;
                lb.Enabled = false;
                dpllt.Enabled = false;
                lb.Text = "Payment Processing...";
            }
            if (e.Row.Cells[26].Text == "C")
            {
                e.Row.BackColor = System.Drawing.Color.Red;
                chkbox.Enabled = false;
                tb.Enabled = false;
                lb.Enabled = false;
                dpllt.Enabled = false;
                lb.Text = "Application Rejected";
            }
            
            if (e.Row.Cells[27].Text == "L")
            {
                e.Row.BackColor = System.Drawing.Color.LightGreen;
                chkbox.Enabled = false;
                tb.Enabled = false;
                lb.Enabled = false;
                dpllt.Enabled = false;
                lb.Text = "Forworded To HR";
            }

            if (e.Row.Cells[26].Text == "L")
            {
                e.Row.BackColor = System.Drawing.Color.LightGreen;
                chkbox.Enabled = false;
                tb.Enabled = false;
                lb.Enabled = false;
                dpllt.Enabled = false;
                lb.Text = "Forworded To F/A";
            }

            if (e.Row.Cells[26].Text == "Y" || e.Row.Cells[27].Text == "P")
            {
                e.Row.BackColor = System.Drawing.Color.GreenYellow;
                chkbox.Enabled = false;
                tb.Enabled = false;
                lb.Enabled = false;
                dpllt.Enabled = false;
                lb.Text = "PAID";
            }

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
                string ProcessID =PID;
                string FlowID =FID;
                string ApplicantID = Session["ApplicantID"].ToString();
                string Transactionno = "T130800001";
                string ActingPersonId = Session["ActingPersonID"].ToString();
                int ActionTypeId = 4;

                CheckBox chkbox = GridViewLeave.Rows[i].FindControl("Checklv") as CheckBox;

                if (chkbox.Checked == true)
                {

                    LeaveProcessHeader offphdr = new LeaveProcessHeader();

                    TextBox tb = GridViewLeave.Rows[i].FindControl("txtlvRemarks") as TextBox;
                    DropDownList dpllvno = GridViewLeave.Rows[i].FindControl("dpllday") as DropDownList;
                    DropDownList dpllvtype = GridViewLeave.Rows[i].FindControl("lvtype") as DropDownList;

                    offphdr.TransactionNo = Transactionno.ToString();
                    offphdr.ApplicantId = ApplicantID.ToString();
                    offphdr.ProcessId =PID;
                    offphdr.FlowId =FID;
                    offphdr.ProcesslevelId = 1;
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
    protected void btnApply_Click(object sender, EventArgs e)
    {
        SqlConnection myConnection = new SqlConnection(ConnectionStr);
        myConnection.Open();

        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;
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

                    LeaveProcessHeader offphdr = new LeaveProcessHeader();

                    TextBox tb = GridViewLeave.Rows[i].FindControl("txtlvRemarks") as TextBox;
                    DropDownList dpllvno = GridViewLeave.Rows[i].FindControl("dpllday") as DropDownList;
                    DropDownList dpllvtype = GridViewLeave.Rows[i].FindControl("lvtype") as DropDownList;

                    offphdr.TransactionNo = Transactionno.ToString();
                    offphdr.ApplicantId = ApplicantID.ToString();
                    offphdr.ProcessId =PID;
                    offphdr.FlowId =FID;
                    offphdr.ProcesslevelId = 1;// dplResponsible.SelectedItem.Value==""?1:0;
                    offphdr.ProcessnextlevelId = 1;
                    offphdr.ActiontypeId = 1;
                    offphdr.SL = Convert.ToInt32(GridViewLeave.Rows[i].Cells[0].Text.ToString());
                    offphdr.ClaiminDdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[3].Text.ToString());
                    offphdr.SysIndate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[2].Text.ToString());
                    offphdr.SysOutdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[3].Text.ToString());
                    offphdr.ActIndate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[2].Text.ToString());
                    offphdr.ActOutdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[3].Text.ToString());
                    offphdr.SysIntime = "00:00 AM";// GridViewLeave.Rows[i].Cells[4].Text.ToString() == "&nbsp;" ? "00:00 AM" : GridViewLeave.Rows[i].Cells[4].Text.ToString();
                    offphdr.SysOuttime = "00:00 AM";// GridViewLeave.Rows[i].Cells[5].Text.ToString() == "&nbsp;" ? "00:00 AM" : GridViewLeave.Rows[i].Cells[5].Text.ToString();
                    offphdr.SysTotalhrs = "00:00";// GridViewLeave.Rows[i].Cells[6].Text.ToString() == "&nbsp;" ? "00:00" : GridViewLeave.Rows[i].Cells[6].Text.ToString();
                    offphdr.NoofLeave = Convert.ToDouble(GridViewLeave.Rows[i].Cells[10].Text.ToString());
                    offphdr.Leavetype = dpllvtype.SelectedItem.Value.ToString();
                    offphdr.Leavedescription = dpllvtype.SelectedItem.Text.ToString();
                    //offphdr.Remarks = tb.Text.ToString();
                    offphdr.Remarks =txtRemarks.Text.ToString().Replace("'","");
                    offphdr.Acthrs = "00:00";
                    offphdr.ResponsiblepersonId ="";
                    offphdr.MovementNo = movementno;
                    offphdr.EntryUserid = Session["EntryUserid"].ToString();
                                        
                    lvphdrlst.Add(offphdr);
                }
            }

            LeaveProcess lvproc = new LeaveProcess();

            string retval = lvproc.SaveInitiateEarnedLeaveProcessData(lvphdrlst, myCommand);
                               
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
                //SendMailtoApprover();
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

    protected void btnApplyAndApprove_Click(object sender, EventArgs e)
    {
        SqlConnection myConnection = new SqlConnection(ConnectionStr);
        myConnection.Open();

        SqlCommand myCommand = myConnection.CreateCommand();
        SqlTransaction myTrans;
        double rCL = 0;
        double rSL = 0;
        double rEL = 0;
        
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
        myTrans = myConnection.BeginTransaction("SaveAllTransaction");

        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;
        
        try
        {
            List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
            LeaveProcess lvp = new LeaveProcess();
            string Transactionno = lvp.GetTransactionNo(ConnectionStr);
            int movementno = Convert.ToInt32(lvp.GetMovementNo(ConnectionStr));
            
            for (int i = 0; i < GridViewLeave.Rows.Count; i++)
            {
                string ProcessID = PID;
                string FlowID = FID;
                string ApplicantID = Session["ApplicantID"].ToString();
                CheckBox chkbox = GridViewLeave.Rows[i].FindControl("Checklv") as CheckBox;

                if (chkbox.Checked == true)
                {

                    LeaveProcessHeader offphdr = new LeaveProcessHeader();

                    TextBox tb = GridViewLeave.Rows[i].FindControl("txtlvRemarks") as TextBox;
                    DropDownList dpllvno = GridViewLeave.Rows[i].FindControl("dpllday") as DropDownList;
                    DropDownList dpllvtype = GridViewLeave.Rows[i].FindControl("lvtype") as DropDownList;

                    offphdr.TransactionNo = Transactionno.ToString();
                    offphdr.ApplicantId = ApplicantID.ToString();
                    offphdr.ProcessId = PID;
                    offphdr.FlowId = FID;
                    offphdr.ProcesslevelId = 1;// dplResponsible.SelectedItem.Value==""?1:0;
                    offphdr.ProcessnextlevelId = 1;
                    offphdr.ActiontypeId = 5;
                    offphdr.SL = Convert.ToInt32(GridViewLeave.Rows[i].Cells[0].Text.ToString());
                    offphdr.ClaiminDdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[3].Text.ToString());
                    offphdr.SysIndate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[2].Text.ToString());
                    offphdr.SysOutdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[3].Text.ToString());
                    offphdr.ActIndate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[2].Text.ToString());
                    offphdr.ActOutdate = Convert.ToDateTime(GridViewLeave.Rows[i].Cells[3].Text.ToString());
                    offphdr.SysIntime = "00:00 AM";// GridViewLeave.Rows[i].Cells[4].Text.ToString() == "&nbsp;" ? "00:00 AM" : GridViewLeave.Rows[i].Cells[4].Text.ToString();
                    offphdr.SysOuttime = "00:00 AM";// GridViewLeave.Rows[i].Cells[5].Text.ToString() == "&nbsp;" ? "00:00 AM" : GridViewLeave.Rows[i].Cells[5].Text.ToString();
                    offphdr.SysTotalhrs = "00:00";// GridViewLeave.Rows[i].Cells[6].Text.ToString() == "&nbsp;" ? "00:00" : GridViewLeave.Rows[i].Cells[6].Text.ToString();
                    offphdr.NoofLeave = Convert.ToDouble(GridViewLeave.Rows[i].Cells[10].Text.ToString());
                    offphdr.Leavetype = dpllvtype.SelectedItem.Value.ToString();
                    offphdr.Leavedescription = dpllvtype.SelectedItem.Text.ToString();
                    //offphdr.Remarks = tb.Text.ToString();
                    offphdr.Remarks = txtRemarks.Text.ToString().Replace("'", "");
                    offphdr.Acthrs = "00:00";
                    offphdr.ResponsiblepersonId = "";
                    offphdr.MovementNo = movementno;
                    offphdr.EntryUserid = Session["EntryUserid"].ToString();

                    lvphdrlst.Add(offphdr);
                }
            }

            LeaveProcess lvproc = new LeaveProcess();

            string retval = "";

            //retval = lvproc.SaveInitiateEarnedLeaveProcessData(lvphdrlst, myCommand);
            retval = lvproc.SaveApproveEarnedLeaveProcessDataDirectly(lvphdrlst, myCommand);            

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
                //SendMailtoApprover();
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
        DateTime fDate, lDate;

        fDate=Convert.ToDateTime(ReturnDecissinon().Split('@')[0].ToString());
        lDate = Convert.ToDateTime(ReturnDecissinon().Split('@')[1].ToString());
        LoadEarnedLeaveDetailsemployeeID(Session["ApplicantID"].ToString(), PID, FID, fDate, lDate);
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

                    offphdr.TransactionNo = Transactionno.ToString();
                    offphdr.ApplicantId = ApplicantID.ToString();
                    offphdr.ProcessId =PID;
                    offphdr.FlowId =FID;
                    offphdr.ProcesslevelId = 1;
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
                int ActionTypeId = 3;          // 97 for reject

                CheckBox chkbox = GridViewLeave.Rows[i].FindControl("Checklv") as CheckBox;

                if (chkbox.Checked == true)
                {

                    LeaveProcessHeader offphdr = new LeaveProcessHeader();

                    TextBox tb = GridViewLeave.Rows[i].FindControl("txtlvRemarks") as TextBox;
                    DropDownList dpllvno = GridViewLeave.Rows[i].FindControl("dpllday") as DropDownList;
                    DropDownList dpllvtype = GridViewLeave.Rows[i].FindControl("lvtype") as DropDownList;

                    offphdr.TransactionNo = Transactionno.ToString();
                    offphdr.ApplicantId = ApplicantID.ToString();
                    offphdr.ProcessId =PID;
                    offphdr.FlowId =FID;
                    offphdr.ProcesslevelId = 1;
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

        LoadEarnedLeaveDetailsemployeeID(Session["ApplicantID"].ToString(), PID, FID, fDate, lDate);
        LoadLeavBALeByemployeeID(Session["ApplicantID"].ToString(), fDate);
        LoadEmployeeInformation(Session["ApplicantID"].ToString());

        //PanelLeaveHdr.Visible=false; 

        lblPeriod.Text = FinanialPeriod(fDate);
        lblcurrentPeriod.Text = System.DateTime.Now.ToString();
    }
    protected void btnCurrentPeriod_Click(object sender, EventArgs e)
    {
        DateTime fDate = Convert.ToDateTime("01/01/2013");
        DateTime lDate = Convert.ToDateTime("31/01/2013");
        LoadEarnedLeaveDetailsemployeeID(Session["ApplicantID"].ToString(), PID, FID, fDate, lDate);
        LoadLeavBALeByemployeeID(Session["ApplicantID"].ToString(), fDate);
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
        DateTime lDate = Convert.ToDateTime(txtFromDate.Text);
       
        DateTime dtto = Convert.ToDateTime(fDate);
        DateTime dtApply = Convert.ToDateTime(System.DateTime.Now);

        Session["ApplicantID"] = txtempid.Text;
                
        SqlConnection sqlConn = null;

        int noOfRowsAffected = 0;
                
        try
        {
            string sql = "exec [spProcessEarnLeaveCalculationByEmpid] '" + Session["ApplicantID"].ToString().Trim() + "','" + dtto.ToShortDateString() + "','" + dtApply.ToShortDateString() + "','" + Session["EntryUserid"].ToString() + "',''";
            sqlConn = new SqlConnection(ConnectionStr);
            sqlConn.Open();
            SqlCommand sqlCom = new SqlCommand(sql, sqlConn);
            sqlCom.CommandTimeout = 600;
            noOfRowsAffected = sqlCom.ExecuteNonQuery();

            LoadAllInformation();

        }
        catch (SqlException sqlExceptionObject)
        {
            if (sqlExceptionObject.Number == 2627)
            {
                System.Windows.Forms.MessageBox.Show(sqlExceptionObject.Message);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show(sqlExceptionObject.Message);
            }
        }
        catch (Exception exceptionObject)
        {
            throw exceptionObject;
        }
        finally
        {
            if (sqlConn.State == System.Data.ConnectionState.Open)
            {               
                sqlConn.Close();
            }
        }     


    }
    
    protected void btnShowALL_Click(object sender, EventArgs e)
    {
        DateTime fDate = Convert.ToDateTime(txtFromDate.Text);
        DateTime lDate = Convert.ToDateTime(txtToDate.Text);
        Session["ApplicantID"] = txtempid.Text;
        LoadEarnedLeaveDetailsemployeeID(Session["ApplicantID"].ToString(), PID, FID, fDate, lDate);
             
        LoadLeavBALeByemployeeID(Session["ApplicantID"].ToString(), fDate);
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
     
    protected void btnPreviewEL_Click(object sender, EventArgs e)
    {
        PreviewELbyEmpid(txtempid.Text);
    }
    private void PreviewELbyEmpid(string empid)
    {
        Connection DC = new Connection();
        string[] ff, ss;
        ParameterFields paramFields = new ParameterFields();
        ConnectionInfo ConnInfo = new ConnectionInfo();

        string constr = System.Configuration.ConfigurationManager.AppSettings["ConStr"].ToString();

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
