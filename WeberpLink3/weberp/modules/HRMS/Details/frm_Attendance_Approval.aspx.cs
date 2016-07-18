using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Details_frm_Attendance_Approval : System.Web.UI.Page
{
    string rnode = "f";
    string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        GlobalData.ConfirmBox(btnSubmitAttendance, "Are you sure to approve attendance ?");
        if (!Page.IsPostBack)
        {
            LoadCompanyByUserPermission("ADM", rnode);
            ClsDropDownListController.EnableDisableDropDownList(ddlcompany);
            ddlcompany_SelectedIndexChanged(sender, e);
 
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
        string constr = System.Configuration.ConfigurationSettings.AppSettings["SCFConnectionString"].ToString().Replace("SCF", dbname);
        Session[GlobalData.sessionConnectionstring] = constr;
        Session["CompanyName"] = ddlcompany.SelectedItem.Text;
        Session["CompanyAddress"] = current.CompanyAddress;
        Session["ConnectionStr"] = constr.ToString();
        Session["db"] = dbname.ToString();
        LibraryPAY.Properties.Settings.Default["SCFConnectionString"] = constr;
        LibraryPAY.Properties.Settings.Default.Save();
        ClsDropDownListController.LoadCheckBoxList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetOfficeLocationIntoDDL(), chkofficelocation, "Division_Master_Name", "Division_Master_Code");
        txtEmployeeSearch_AutoCompleteExtender.ContextKey = Session[GlobalData.sessionConnectionstring].ToString();
        LoadDepartmentIdByuserCode("ADM", dbname.ToString(), rnode.ToString());//(Session[GlobalData.SessionUserId].ToString(), dbname.ToString(), rnode.ToString());
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

    protected void btnRefresh_Click(object sender, EventArgs e)
    {

    }

    







    public void LoadDepartmentId()
    {
        DataTable dt = new DataTable();
        string strSql = "SELECT distinct DeptID, Dept FROM Emp_Details INNER JOIN Hrms_Emp_Mas on Emp_Details.EmpId = Hrms_Emp_Mas .Emp_Mas_Emp_Id and Emp_Mas_Term_Flg = 'N' ORDER BY Dept  ASC";
        dt = DataProcess.GetData(Session["ConnectionStr"].ToString(), strSql);
        ddlDepartmentId.Items.Clear();
        ddlDepartmentId.Items.Add("--please select--");
        foreach (DataRow dr in dt.Rows)
        {
            ListItem lst = new ListItem();
            lst.Value = dr["DeptID"].ToString();
            lst.Text = dr["Dept"].ToString();
            ddlDepartmentId.Items.Add(lst);
        }
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
        ddlDepartmentId.Items.Add("ALL");
        foreach (DataRow dr in dt.Rows)
        {
            ListItem lst = new ListItem();
            lst.Value = dr["DeptID"].ToString();
            lst.Text = dr["Dept"].ToString();
            ddlDepartmentId.Items.Add(lst);
        }
    }

    public void LoadShiftAllocationRecord()
    {
        //LeaveProcess lvproc = new LeaveProcess();
        //string DateOfAllocation = txtFromDate.SelectedDate.ToString();
        //grdShiftAllocationPreview.DataSource = lvproc.GetAllocatedHolidayRecord(Session["ConnectionStr"].ToString(), txtFromDate.SelectedDate.ToString(), ddlDepartmentId.SelectedValue);
        //grdShiftAllocationPreview.DataBind();
    }

    public static void MergeRows(GridView gridView)
    {
        try
        {
            for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex + 1];
                if (row.Cells[0].Text == previousRow.Cells[0].Text)
                {
                    for (int i = 0; i < row.Cells.Count - 1; i++)
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

    public void LoadShiftType()
    {
        DataTable dt = new DataTable();
        string strSql = "select Shift_Mas_Code as [Shift Code],Shift_Mas_Desc as Shift,Shift_Mas_From as [From],Shift_Mas_To as [To] from hrms_shift_mas";
        dt = DataProcess.GetData(Session["ConnectionStr"].ToString(), strSql);        
    }

    public void LoadShiftType1()
    {
        DataTable dt = new DataTable();
        string strSql = "select Shift_Mas_Code as [Shift Code],Shift_Mas_Desc as Shift,Shift_Mas_From as [From],Shift_Mas_To as [To] from hrms_shift_mas";
        dt = DataProcess.GetData(Session["ConnectionStr"].ToString(), strSql);
      
    }

    protected void btnShowEmployee_Click(object sender, EventArgs e)
    {
        if (ddlDepartmentId.SelectedIndex == -1) return;
        DataTable dt = new DataTable();
       
        string strSql = "select EmpID,EmpName,Designation from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id where DeptID='" + ddlDepartmentId.SelectedValue + "' and Emp_Mas_Term_Flg='N'";

        dt = DataProcess.GetData(Session["ConnectionStr"].ToString(), strSql);
      
    }   

    protected void grdShiftAllocationPreview_PreRender(object sender, EventArgs e)
    {
        //MergeRows(grdShiftAllocationPreview);
    }

    protected void grdShiftAllocationPreview_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdShiftAllocationPreview.PageIndex = e.NewPageIndex;
        grdShiftAllocationPreview.DataBind();
        LoadShiftAllocationRecord();
    }

    protected void grdShiftAllocationPreview_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[0].ForeColor = System.Drawing.Color.Red;
            e.Row.Cells[0].Font.Bold = true;

            MKB.TimePicker.TimeSelector mkb = (MKB.TimePicker.TimeSelector)e.Row.FindControl("timeoff1");
            MKB.TimePicker.TimeSelector mkb2 = (MKB.TimePicker.TimeSelector)e.Row.FindControl("timeoff2");

            DateTime dt1 = DateTime.Parse(e.Row.Cells[7].Text.ToString());
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

            DateTime dt2 = DateTime.Parse(e.Row.Cells[8].Text.ToString());
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
            

        }

        e.Row.Cells[7].Visible = false;
        e.Row.Cells[8].Visible = false;
  
    }

    
    protected void ddlDepartmentId_SelectedIndexChanged(object sender, EventArgs e)
    {
        //LoadShiftAllocationRecord();
    }   

    private string GetEmployeeInformation(string empid)
    {
        string empname = "Not Found";
        DataTable dt = new DataTable();
        dt = DataProcess.GetData(Session["ConnectionStr"].ToString(), "select EmpName from Emp_Details where EmpID='" + empid.ToString() + "'");
        if (dt.Rows.Count > 0)
        {
            empname = dt.Rows[0]["EmpName"].ToString();
        }
        return empname;
    }

    
   
    
   

    protected void chkbForSelectALL_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void chkbALL_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void btnPreview_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        string empid = Session[StaticData.sessionUserId].ToString();
        string officelocation = "";

        DateTime dtserver = DateProcess.GetServerDate(Session[GlobalData.sessionConnectionstring].ToString());
        DateTime dtto = txtToDate.SelectedDate;
        DateTime dtfrom = txtFromDate.SelectedDate;
        if (dtto > dtserver || dtfrom > dtserver)
        {
            MessageBox1.ShowWarning("Date selection error. You can not select date greater than server date.");
            return;
        }


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
        if (ddlDepartmentId.SelectedValue == "ALL")
        {
            sql = "create view viewemplistforattendanceapproval as select EmpID,Dept,Sect,Designation from Emp_Details where OfficeID in " + officelocation + "";
        }
        else
        {
            sql = "create view viewemplistforattendanceapproval as select EmpID,Dept,Sect,Designation from Emp_Details where OfficeID in " + officelocation + " and DeptID = '" + ddlDepartmentId.SelectedValue + "'";

        }

        string empcode = "";
        if (txtEmployeeSearch.Text.Trim().Equals("") == false)
        {
            string[] emp = txtEmployeeSearch.Text.Split(':');
            if (emp.Length > 0)
            {
                empcode = emp[0].ToString();
                sql = "create view viewemplistforattendanceapproval as select EmpID,Dept,Sect,Designation from Emp_Details where EmpID='" + empcode + "'";
            }
            else
                return;
        }

        DataProcess.ExecuteQuery(Session[GlobalData.sessionConnectionstring].ToString(), "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[viewemplistforattendanceapproval]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[viewemplistforattendanceapproval]");
        DataProcess.ExecuteQuery(Session[GlobalData.sessionConnectionstring].ToString(), sql);
        
        CallAbsentListSp();

        string selectionfor, parameter;
        selectionfor = "{hrms_AbsentList_byDept.Userid}='" + empid + "'";//"{hrms_AbsentList_byDept.Userid}='" + Session[GlobalData.SessionUserId].ToString() + "'";
        string CompanyName = "CompanyName" + ":" + Session["CompanyName"].ToString().Split(':')[1].ToString();
        string CompanyAddress = "CompanyAddress" + ":" + Session["CompanyAddress"].ToString();
        parameter = CompanyName + ";" + CompanyAddress;
        string reportname = "../Reports/AbsentReport.rpt";
        ShowReport(selectionfor, parameter, reportname);
    }

    private void CallAbsentListSp()
    {
        DateTime fdate, lDate;
        string deptid, empid;
        fdate = txtFromDate.SelectedDate;
        lDate = txtToDate.SelectedDate;
        deptid = "";
        int cond = 0;
        deptid = ddlDepartmentId.SelectedItem.Value;
        empid = Session[StaticData.sessionUserId].ToString();
        LeaveProcess lvp = new LeaveProcess();
        string al = lvp.spAbsentListEmplloyee(Session["ConnectionStr"].ToString(), fdate, lDate, deptid, empid, cond, rnode.ToString(), Session["db"].ToString());
    }

    private void CallAbsentListSpManual()
    {
        DateTime fdate, lDate;
        string deptid, empid;
        fdate = txtFromDate.SelectedDate;
        lDate = txtToDate.SelectedDate;
        deptid = "";
        int cond = 0;
        deptid = ddlDepartmentId.SelectedItem.Value;
        empid = Session[StaticData.sessionUserId].ToString();
        LeaveProcess lvp = new LeaveProcess();
        string al = lvp.spAbsentListEmplloyeeManual(Session["ConnectionStr"].ToString(), fdate, lDate, deptid, empid, cond, rnode.ToString(), Session["db"].ToString());
    }


    private void CallPresentListSp()
    {
        DateTime fdate, lDate;
        string deptid, empid;
        fdate = txtFromDate.SelectedDate;
        lDate = txtToDate.SelectedDate;
        deptid = "";
        int cond = 0;
        deptid = ddlDepartmentId.SelectedItem.Value;
        empid = Session[StaticData.sessionUserId].ToString();
        LeaveProcess lvp = new LeaveProcess();
        string al = lvp.spPresentListEmplloyee(Session["ConnectionStr"].ToString(), fdate, lDate, deptid, empid, cond, rnode.ToString(), Session["db"].ToString());
    }

    private void CallPresentListSpManual()
    {
        DateTime fdate, lDate;
        string deptid, empid;
        fdate = txtFromDate.SelectedDate;
        lDate = txtToDate.SelectedDate;
        deptid = "";
        int cond = 0;
        deptid = ddlDepartmentId.SelectedItem.Value;
        empid = Session[StaticData.sessionUserId].ToString();
        LeaveProcess lvp = new LeaveProcess();
        string al = lvp.spPresentListEmplloyeeManual(Session["ConnectionStr"].ToString(), fdate, lDate, deptid, empid, cond, rnode.ToString(), Session["db"].ToString());
    }

    protected void btnShowAttendance_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        string empid = Session[StaticData.sessionUserId].ToString();
        string officelocation = "";


        DateTime dtserver = DateProcess.GetServerDate(Session[GlobalData.sessionConnectionstring].ToString());
        DateTime dtto = txtToDate.SelectedDate;
        DateTime dtfrom = txtFromDate.SelectedDate;
        if (dtto > dtserver || dtfrom > dtserver)
        {
            MessageBox1.ShowWarning("Date selection error. You can not select date greater than server date.");
            return;
        }

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

        string empcategory = ddlEmpCategory.SelectedItem.Value;
        string sql = "";
        if (ddlDepartmentId.SelectedValue == "ALL")
        {
            if (empcategory == "-1") 
                sql = "create view viewemplistforattendanceapproval as select EmpID,Dept,Sect,Designation from Emp_Details where OfficeID in " + officelocation + "";
            else
                sql = "create view viewemplistforattendanceapproval as select EmpID,Dept,Sect,Designation from Emp_Details where OfficeID in " + officelocation + " and emptype='" + empcategory + "'";
        }
        else
        {
            if (empcategory == "-1") 
                sql = "create view viewemplistforattendanceapproval as select EmpID,Dept,Sect,Designation from Emp_Details where OfficeID in " + officelocation + " and DeptID = '" + ddlDepartmentId.SelectedValue + "'";
            else
                sql = "create view viewemplistforattendanceapproval as select EmpID,Dept,Sect,Designation from Emp_Details where OfficeID in " + officelocation + " and DeptID = '" + ddlDepartmentId.SelectedValue + "' and emptype='" + empcategory + "'";

        }



        string empcode="";
        if (txtEmployeeSearch.Text.Trim().Equals("") == false)
        {
            string[] emp = txtEmployeeSearch.Text.Split(':');
            if (emp.Length > 0)
            {
                empcode = emp[0].ToString();
                sql = "create view viewemplistforattendanceapproval as select EmpID,Dept,Sect,Designation from Emp_Details where EmpID='" + empcode + "'";
            }
            else
                return;
        }


        DataProcess.ExecuteQuery(Session[GlobalData.sessionConnectionstring].ToString(), "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[viewemplistforattendanceapproval]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[viewemplistforattendanceapproval]");
        DataProcess.ExecuteQuery(Session[GlobalData.sessionConnectionstring].ToString(), sql);

        CallAbsentListSpManual();

        sql = "select EmployeeID,EmployeeName,adate,Intime,Outtime,Remarks from hrms_AbsentList_byDeptManual where userid='" + empid.ToString() + "' order by EmployeeID,adate";

        dt = DataProcess.GetData(Session["ConnectionStr"].ToString(), sql);

        grdShiftAllocationPreview.DataSource = dt;
        grdShiftAllocationPreview.DataBind();
        if (grdShiftAllocationPreview.Rows.Count > 0)
        {
            btnSubmitAttendance.Visible = true;
            btnDeleteAttendance.Visible = true; 
        }
        else
        {
            btnSubmitAttendance.Visible = false;
            btnDeleteAttendance.Visible = false; 
        }      

    }

    protected void btnSubmitAttendance_Click(object sender, EventArgs e)
    {
        if (grdShiftAllocationPreview.Rows.Count != 0)
        {
            SqlConnection myConnection = new SqlConnection(Session["ConnectionStr"].ToString());
            myConnection.Open();
            SqlCommand myCommand = myConnection.CreateCommand();
            SqlTransaction myTrans;
            myTrans = myConnection.BeginTransaction("SaveAllTransaction");
            myCommand.Connection = myConnection;
            myCommand.Transaction = myTrans;
            try
            {
                string empid = Session[StaticData.sessionUserId].ToString();
                LeaveProcess objLeaveProcess = new LeaveProcess();
                List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
                for (int i = 0; i < grdShiftAllocationPreview.Rows.Count; i++)
                {
                    CheckBox chkbox = grdShiftAllocationPreview.Rows[i].FindControl("CheckIndv") as CheckBox;
                    TextBox txtremarks = grdShiftAllocationPreview.Rows[i].FindControl("txtRemarks") as TextBox;

                    if (chkbox.Checked == true)
                    {
                        LeaveProcessHeader offphdr = new LeaveProcessHeader();
                        MKB.TimePicker.TimeSelector mkb = (MKB.TimePicker.TimeSelector)grdShiftAllocationPreview.Rows[i].FindControl("timeoff1");
                        MKB.TimePicker.TimeSelector mkb2 = (MKB.TimePicker.TimeSelector)grdShiftAllocationPreview.Rows[i].FindControl("timeoff2");
                                                                  
                        offphdr.EmpID = grdShiftAllocationPreview.Rows[i].Cells[0].Text;
                        offphdr.ActIntime = timeformat(mkb.Date.Hour.ToString() + ":" + mkb.Date.Minute.ToString() + ":" + mkb.AmPm.ToString());
                        offphdr.ActOuttime = timeformat(mkb2.Date.Hour.ToString() + ":" + mkb2.Date.Minute.ToString() + ":" + mkb2.AmPm.ToString());
                        offphdr.ClaiminDdate = Convert.ToDateTime(grdShiftAllocationPreview.Rows[i].Cells[2].Text);
                        offphdr.LeaveRemarks = txtremarks.Text.Trim().Replace("'", "");
                        offphdr.EntryUserid = empid.ToString();

                        lvphdrlst.Add(offphdr);
                    }
                }
                LeaveProcess lvproc = new LeaveProcess();
                string retval = lvproc.SaveManualApproveAttendance(lvphdrlst, myCommand, Session["db"].ToString());
                if (retval.ToString() == "")
                {
                    myTrans.Rollback("SaveAllTransaction");
                }
                else
                {
                    myTrans.Commit();
                    MessageBox1.ShowSuccess("Attendance Approve Successful");
                }

                ClearGridData();
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

    private string timeformat(string atf)
    {
        string rtf = "";
        int h = Convert.ToInt32(atf.Split(':')[0].ToString());
        int m = Convert.ToInt32(atf.Split(':')[1].ToString());

        if (h > 12)
        {
            h = h - 12;
        }

        string hh = string.Format("{0:00}", h);
        string mm = string.Format("{0:00}", m);
        string ampm = atf.Split(':')[2].ToString();

        rtf = hh + ":" + mm + " " + ampm;
        return rtf;
    }

    protected void btnPreviewIndividual_Click(object sender, EventArgs e)
    {
       
        string selectionfor, parameter;
        selectionfor = "{Hrms_Attendance_Individual.Userid}='" + Session[GlobalData.SessionUserId].ToString() + "'";
        string CompanyName = "CompanyName" + ":" + Session["CompanyName"].ToString().Split(':')[1].ToString();
        string CompanyAddress = "CompanyAddress" + ":" + Session["CompanyAddress"].ToString();
        parameter = CompanyName + ";" + CompanyAddress;
        string reportname = "./Report/AbsentReportIndividual.rpt";
        ShowReport(selectionfor, parameter, reportname);
    }

   
    protected void btnShowPresentAttendance_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        string empid = Session[StaticData.sessionUserId].ToString();
        string officelocation = "";

        DateTime dtserver = DateProcess.GetServerDate(Session[GlobalData.sessionConnectionstring].ToString());
        DateTime dtto = txtToDate.SelectedDate;
        DateTime dtfrom = txtFromDate.SelectedDate;
        if (dtto > dtserver || dtfrom > dtserver)
        {
            MessageBox1.ShowWarning("Date selection error. You can not select date greater than server date.");
            return;
        }


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
        if (ddlDepartmentId.SelectedValue == "ALL")
        {
            sql = "create view viewemplistforattendanceapproval as select EmpID,Dept,Sect,Designation from Emp_Details where OfficeID in " + officelocation + "";
        }
        else
        {
            sql = "create view viewemplistforattendanceapproval as select EmpID,Dept,Sect,Designation from Emp_Details where OfficeID in " + officelocation + " and DeptID = '" + ddlDepartmentId.SelectedValue + "'";

        }

        string empcode = "";
        if (txtEmployeeSearch.Text.Trim().Equals("") == false)
        {
            string[] emp = txtEmployeeSearch.Text.Split(':');
            if (emp.Length > 0)
            {
                empcode = emp[0].ToString();
                sql = "create view viewemplistforattendanceapproval as select EmpID,Dept,Sect,Designation from Emp_Details where EmpID='" + empcode + "'";
            }
            else
                return;
        }


        DataProcess.ExecuteQuery(Session[GlobalData.sessionConnectionstring].ToString(), "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[viewemplistforattendanceapproval]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[viewemplistforattendanceapproval]");
        DataProcess.ExecuteQuery(Session[GlobalData.sessionConnectionstring].ToString(), sql);

        CallPresentListSpManual();

        sql = "select EmployeeID,EmployeeName,adate,Intime,Outtime,Remarks from hrms_AbsentList_byDeptManual where userid='" + empid.ToString() + "' order by EmployeeID,adate";

        dt = DataProcess.GetData(Session["ConnectionStr"].ToString(), sql);

        grdShiftAllocationPreview.DataSource = dt;
        grdShiftAllocationPreview.DataBind();
        if (grdShiftAllocationPreview.Rows.Count > 0)
        {
            btnSubmitAttendance.Visible = true;
            btnDeleteAttendance.Visible = true;
        }
        else
        {
            btnSubmitAttendance.Visible = false;
            btnDeleteAttendance.Visible = false;
        }   
    }
    protected void btnDeleteAttendance_Click(object sender, EventArgs e)
    {
        if (grdShiftAllocationPreview.Rows.Count != 0)
        {
            SqlConnection myConnection = new SqlConnection(Session["ConnectionStr"].ToString());
            myConnection.Open();
            SqlCommand myCommand = myConnection.CreateCommand();
            SqlTransaction myTrans;
            myTrans = myConnection.BeginTransaction("SaveAllTransaction");
            myCommand.Connection = myConnection;
            myCommand.Transaction = myTrans;
            try
            {
                string empid = Session[StaticData.sessionUserId].ToString();
                LeaveProcess objLeaveProcess = new LeaveProcess();
                List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
                for (int i = 0; i < grdShiftAllocationPreview.Rows.Count; i++)
                {
                    CheckBox chkbox = grdShiftAllocationPreview.Rows[i].FindControl("CheckIndv") as CheckBox;
                    if (chkbox.Checked == true)
                    {
                        LeaveProcessHeader offphdr = new LeaveProcessHeader();
                        MKB.TimePicker.TimeSelector mkb = (MKB.TimePicker.TimeSelector)grdShiftAllocationPreview.Rows[i].FindControl("timeoff1");
                        MKB.TimePicker.TimeSelector mkb2 = (MKB.TimePicker.TimeSelector)grdShiftAllocationPreview.Rows[i].FindControl("timeoff2");
                        TextBox txtbox = grdShiftAllocationPreview.Rows[i].FindControl("txtRemarks") as TextBox;

                        offphdr.EmpID = grdShiftAllocationPreview.Rows[i].Cells[0].Text;
                        offphdr.ActIntime = timeformat(mkb.Date.Hour.ToString() + ":" + mkb.Date.Minute.ToString() + ":" + mkb.AmPm.ToString());
                        offphdr.ActOuttime = timeformat(mkb2.Date.Hour.ToString() + ":" + mkb2.Date.Minute.ToString() + ":" + mkb2.AmPm.ToString());
                        offphdr.ClaiminDdate = Convert.ToDateTime(grdShiftAllocationPreview.Rows[i].Cells[2].Text);
                        offphdr.Remarks = txtbox.Text.Replace("'", "");
                        offphdr.EntryUserid = empid.ToString();
                        lvphdrlst.Add(offphdr);
                    }
                }
                LeaveProcess lvproc = new LeaveProcess();
                string retval = lvproc.DeleteAttendance(lvphdrlst, myCommand, Session["db"].ToString());
                if (retval.ToString() == "")
                {
                    myTrans.Rollback("SaveAllTransaction");
                }
                else
                {
                    myTrans.Commit();
                    MessageBox1.ShowSuccess("Attendance delete Successful");
                }

                ClearGridData();
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

    private void ClearGridData()
    {
        DataTable dt = new DataTable();
        grdShiftAllocationPreview.DataSource = null;
        grdShiftAllocationPreview.DataBind();
        btnSubmitAttendance.Visible = false;
        btnDeleteAttendance.Visible = false; 
    }

    protected void btnPreviewPresent_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        string empid = Session[StaticData.sessionUserId].ToString();
        string officelocation = "";

        DateTime dtserver = DateProcess.GetServerDate(Session[GlobalData.sessionConnectionstring].ToString());
        DateTime dtto = txtToDate.SelectedDate;
        DateTime dtfrom = txtFromDate.SelectedDate;
        if (dtto > dtserver || dtfrom > dtserver)
        {
            MessageBox1.ShowWarning("Date selection error. You can not select date greater than server date.");
            return;
        }


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
        if (ddlDepartmentId.SelectedValue == "ALL")
        {
            sql = "create view viewemplistforattendanceapproval as select EmpID,Dept,Sect,Designation from Emp_Details where OfficeID in " + officelocation + "";
        }
        else
        {
            sql = "create view viewemplistforattendanceapproval as select EmpID,Dept,Sect,Designation from Emp_Details where OfficeID in " + officelocation + " and DeptID = '" + ddlDepartmentId.SelectedValue + "'";

        }

        string empcode = "";
        if (txtEmployeeSearch.Text.Trim().Equals("") == false)
        {
            string[] emp = txtEmployeeSearch.Text.Split(':');
            if (emp.Length > 0)
            {
                empcode = emp[0].ToString();
                sql = "create view viewemplistforattendanceapproval as select EmpID,Dept,Sect,Designation from Emp_Details where EmpID='" + empcode + "'";
            }
            else
                return;
        }

        DataProcess.ExecuteQuery(Session[GlobalData.sessionConnectionstring].ToString(), "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[viewemplistforattendanceapproval]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[viewemplistforattendanceapproval]");
        DataProcess.ExecuteQuery(Session[GlobalData.sessionConnectionstring].ToString(), sql);

        CallPresentListSp();

        string selectionfor, parameter;
        selectionfor = "{hrms_AbsentList_byDept.Userid}='" + empid + "'";//"{hrms_AbsentList_byDept.Userid}='" + Session[GlobalData.SessionUserId].ToString() + "'";
        string CompanyName = "CompanyName" + ":" + Session["CompanyName"].ToString().Split(':')[1].ToString();
        string CompanyAddress = "CompanyAddress" + ":" + Session["CompanyAddress"].ToString();
        parameter = CompanyName + ";" + CompanyAddress;
        string reportname = "../Reports/PresentReport.rpt";
        ShowReport(selectionfor, parameter, reportname);
    }
}