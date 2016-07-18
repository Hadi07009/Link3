using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Details_frm_AbsentList_Report : System.Web.UI.Page
{
    string rnode = "h";
    string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        GlobalData.ConfirmBox(btnSubmitAttendance, "Are you sure to approve attendance ?");
        if (!Page.IsPostBack)
        {
            LoadCompanyByUserPermission("ADM", "I");
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
                      + " Inner join ubasys.dbo.tblUserCompanyDepartment on DepartmentID=Deptid"
                      + " where [UserID]='" + userid + "' and [NodeID]='" + nodeid + "' and [CompanyID]='" + companyid + "'"
                      + " ORDER BY Dept  ASC";


        dt = DataProcess.GetData(Session["ConnectionStr"].ToString(), strSql);

        if (dt.Rows.Count == 0)
        {
            strSql = "SELECT distinct DeptID, Dept FROM Emp_Details INNER JOIN Hrms_Emp_Mas on Emp_Details.EmpId = Hrms_Emp_Mas .Emp_Mas_Emp_Id and Emp_Mas_Term_Flg = 'N' ORDER BY Dept  ASC";
            dt = DataProcess.GetData(Session["ConnectionStr"].ToString(), strSql);
        }

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
        //DataTable dt = new DataTable();
        //string strSql = "select Shift_Mas_Code as [Shift Code],Shift_Mas_Desc as Shift,Shift_Mas_From as [From],Shift_Mas_To as [To] from hrms_shift_mas";
        //dt = DataProcess.GetData(Session["ConnectionStr"].ToString(), strSql);
        //grdLoadShiftType.DataSource = dt;
        //grdLoadShiftType.DataBind();
    }

    public void LoadShiftType1()
    {
        //DataTable dt = new DataTable();
        //string strSql = "select Shift_Mas_Code as [Shift Code],Shift_Mas_Desc as Shift,Shift_Mas_From as [From],Shift_Mas_To as [To] from hrms_shift_mas";
        //dt = DataProcess.GetData(Session["ConnectionStr"].ToString(), strSql);
        //grdLoadShiftType0.DataSource = dt;
        //grdLoadShiftType0.DataBind();
    }

    protected void btnShowEmployee_Click(object sender, EventArgs e)
    {
        //if (ddlDepartmentId.SelectedIndex == -1) return;

        //DataTable dt = new DataTable();
        //lblShowDept.Text = ddlDepartmentId.SelectedItem.Text;
        //LabelShowDate.Text = txtFromDate.SelectedDate.ToString();
        //string strSql = "select EmpID,EmpName,Designation from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id where DeptID='" + ddlDepartmentId.SelectedValue + "' and Emp_Mas_Term_Flg='N'";

        //dt = DataProcess.GetData(Session["ConnectionStr"].ToString(), strSql);

        //GridViewShowEmployeePerDept.DataSource = dt;
        //GridViewShowEmployeePerDept.DataBind();
        //LoadShiftType();
        //this.ModalPopupExtender1.Show();
    }

    protected void btnApply_Click(object sender, EventArgs e)
    {
        //if (GridViewShowEmployeePerDept.Rows.Count != 0)
        //{
        //    SqlConnection myConnection = new SqlConnection(Session["ConnectionStr"].ToString());
        //    myConnection.Open();
        //    SqlCommand myCommand = myConnection.CreateCommand();
        //    SqlTransaction myTrans;
        //    myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        //    myCommand.Connection = myConnection;
        //    myCommand.Transaction = myTrans;
        //    try
        //    {
        //        LeaveProcess objLeaveProcess = new LeaveProcess();
        //        objLeaveProcess.DeletePreviousAllcatedShift(myCommand, ddlDepartmentId.SelectedValue.ToString(), txtFromDate.SelectedDate.ToString());

        //        List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
        //        for (int i = 0; i < GridViewShowEmployeePerDept.Rows.Count; i++)
        //        {
        //            CheckBox chkbox = GridViewShowEmployeePerDept.Rows[i].FindControl("CheckRet") as CheckBox;
        //            CheckBoxList chkboxForShiftStatus = GridViewShowEmployeePerDept.Rows[i].FindControl("cblForShiftSelect") as CheckBoxList;
        //            if (chkbox.Checked == true)
        //            {
        //                List<Guid> things = new List<Guid>();
        //                foreach (ListItem item in chkboxForShiftStatus.Items)
        //                {
        //                    if (item.Selected)
        //                    {
        //                        LeaveProcessHeader offphdr = new LeaveProcessHeader();
        //                        offphdr.DateForSA = txtFromDate.SelectedDate.ToString();
        //                        offphdr.ShiftID = Convert.ToInt32(item.Value.ToString());
        //                        offphdr.InTime = "";
        //                        offphdr.OutTime = "";
        //                        offphdr.EmpID = GridViewShowEmployeePerDept.Rows[i].Cells[2].Text.ToString();
        //                        lvphdrlst.Add(offphdr);
        //                    }
        //                }
        //            }
        //        }

        //        int shiftid = Convert.ToInt32(ddlShift.SelectedValue);

        //        string DateTimeForAllocation = (txtFromDate.SelectedDate).ToString();
        //        LeaveProcess lvproc = new LeaveProcess();
        //        string retval = lvproc.SaveHolidayAllocationData(lvphdrlst, myCommand, ddlDepartmentId.SelectedValue.ToString(), shiftid, DateTimeForAllocation);
        //        if (retval.ToString() == "")
        //        {
        //            myTrans.Rollback("SaveAllTransaction");
        //        }
        //        else
        //        {
        //            myTrans.Commit();
        //        }

        //        LoadShiftAllocationRecord();

        //    }
        //    catch (Exception ex)
        //    {
        //        myTrans.Rollback("SaveAllTransaction");
        //    }
        //    finally
        //    {
        //        myConnection.Close();
        //    }

        //}
    }
    protected void grdShiftAllocationPreview_PreRender(object sender, EventArgs e)
    {
        MergeRows(grdShiftAllocationPreview);
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
        }
    }
    protected void GridViewShowEmployeePerDept_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string EmpId = e.Row.Cells[2].Text;
            int ShiftId = Convert.ToInt32(ddlShift.SelectedValue.ToString());
            string DateAllocation = txtFromDate.SelectedDate.ToString("dd/MM/yyyy");
            DateTime DateAllocationFinal = Convert.ToDateTime(DateAllocation);
            CheckBox chk = (CheckBox)e.Row.FindControl("CheckRet");
            chk.Checked = true;
            CheckBoxList chkboxForShiftStatus = (CheckBoxList)e.Row.FindControl("cblForShiftSelect");
            LeaveProcess lvproc = new LeaveProcess();
            DataTable ApplicantId = lvproc.GetHolidayAllocatedEmpId(Session["ConnectionStr"].ToString(), EmpId, ShiftId, DateAllocation);
            if (ApplicantId.Rows.Count != 0)
            {
                foreach (DataRow dr in ApplicantId.Rows)
                {
                    int sID = Convert.ToInt32(dr[0].ToString());
                    if (sID == 4)
                    {
                        chkboxForShiftStatus.Items[0].Selected = true;
                        chkboxForShiftStatus.Items[0].Enabled = true;
                    }
                }
            }

        }
    }

    protected void ddlDepartmentId_SelectedIndexChanged(object sender, EventArgs e)
    {
        //LoadShiftAllocationRecord();
    }

    protected void btnShowIndividual_Click(object sender, EventArgs e)
    {

        //if (ddlDepartmentId.SelectedIndex == -1) return;

        //DataTable dt = new DataTable();

        //lblShowEmp.Text = GetEmployeeInformation(txtEmpId.Text);
        //lblShowEmpId.Text = txtEmpId.Text;

        //DateTime fdate = Convert.ToDateTime(txtFromDate.SelectedDate);
        //DateTime ldate = Convert.ToDateTime(txtToDate.SelectedDate);

        //lblShowadt.Text = txtFromDate.SelectedDate.ToString("dd/MM/yyyy") + " TO " + txtToDate.SelectedDate.ToString("dd/MM/yyyy");

        //LeaveProcess lvproc = new LeaveProcess();
        //dt = lvproc.GetAllocatedShiftbyempid(Session["ConnectionStr"].ToString(), txtEmpId.Text, fdate, ldate);

        //GridViewShowEmployeeIndividual.DataSource = dt;
        //GridViewShowEmployeeIndividual.DataBind();
        //LoadShiftType1();
        //this.ModalPopupExtender2.Show();
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

    protected void GridViewShowEmployeePerDept0_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string EmpId = e.Row.Cells[2].Text;
            int ShiftId = Convert.ToInt32(ddlShift.SelectedValue.ToString());
            string DateAllocation = txtFromDate.SelectedDate.ToString("dd/MM/yyyy");

            CheckBox chk = (CheckBox)e.Row.FindControl("CheckRetIndv");
            chk.Checked = true;
            chk.Enabled = false;

            CheckBoxList chkboxForShiftStatus = (CheckBoxList)e.Row.FindControl("cblForShiftSelectShiftIndv");

            int shid = Convert.ToInt32(e.Row.Cells[4].Text.ToString());
            if (shid > 0)
            {
                chkboxForShiftStatus.Items[shid - 1].Selected = true;
                chkboxForShiftStatus.Items[shid - 1].Enabled = true;
            }

        }

    }
    protected void btnApplyIndv_Click(object sender, EventArgs e)
    {
        //if (GridViewShowEmployeeIndividual.Rows.Count != 0)
        //{
        //    SqlConnection myConnection = new SqlConnection(Session["ConnectionStr"].ToString());
        //    myConnection.Open();
        //    SqlCommand myCommand = myConnection.CreateCommand();
        //    SqlTransaction myTrans;
        //    myTrans = myConnection.BeginTransaction("SaveAllTransaction");
        //    myCommand.Connection = myConnection;
        //    myCommand.Transaction = myTrans;
        //    try
        //    {
        //        LeaveProcess objLeaveProcess = new LeaveProcess();

        //        objLeaveProcess.DeletePreviousAllcatedShiftByemployeeID(myCommand, lblShowEmpId.Text.Trim().ToString(), txtFromDate.SelectedDate.ToString(), txtToDate.SelectedDate.ToString());

        //        List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();

        //        for (int i = 0; i < GridViewShowEmployeeIndividual.Rows.Count; i++)
        //        {
        //            CheckBox chkbox = GridViewShowEmployeeIndividual.Rows[i].FindControl("CheckRetIndv") as CheckBox;

        //            CheckBoxList chqlist = GridViewShowEmployeeIndividual.Rows[i].FindControl("cblForShiftSelectShiftIndv") as CheckBoxList;

        //            if (chkbox.Checked == true)
        //            {
        //                foreach (ListItem item in chqlist.Items)
        //                {
        //                    if (item.Selected)
        //                    {
        //                        LeaveProcessHeader offphdr = new LeaveProcessHeader();
        //                        offphdr.DateForSA = GridViewShowEmployeeIndividual.Rows[i].Cells[2].Text;
        //                        offphdr.ShiftID = Convert.ToInt32(item.Value.ToString());
        //                        offphdr.InTime = "";
        //                        offphdr.OutTime = "";
        //                        offphdr.EmpID = lblShowEmpId.Text.Trim();
        //                        lvphdrlst.Add(offphdr);
        //                    }
        //                }
        //            }

        //        }

        //        LeaveProcess lvproc = new LeaveProcess();
        //        string retval = lvproc.SaveShiftAllocationDataByEmpid(lvphdrlst, myCommand);
        //        if (retval.ToString() == "")
        //        {
        //            myTrans.Rollback("SaveAllTransaction");
        //        }
        //        else
        //        {
        //            myTrans.Commit();
        //        }

        //        LoadShiftAllocationRecord();
        //    }
        //    catch (Exception ex)
        //    {
        //        myTrans.Rollback("SaveAllTransaction");
        //    }
        //    finally
        //    {
        //        myConnection.Close();
        //    }

        //}
    }
    protected void GridViewShowEmployeeIndividual_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int ShiftId = Convert.ToInt32(ddlShift.SelectedValue.ToString());
            string DateAllocation = txtFromDate.SelectedDate.ToString("dd/MM/yyyy");

            CheckBox chk = (CheckBox)e.Row.FindControl("CheckRetIndv");
            chk.Checked = true;
            chk.Enabled = false;

            CheckBoxList chkboxForShiftStatus = (CheckBoxList)e.Row.FindControl("cblForShiftSelectShiftIndv");

            int shid = Convert.ToInt32(e.Row.Cells[4].Text.ToString());
            if (shid > 0)
            {
                chkboxForShiftStatus.Items[shid - 1].Selected = true;
                chkboxForShiftStatus.Items[shid - 1].Enabled = true;
            }

            if (shid == 4)
            {
                e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;
            }

        }


        e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
        e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
        e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;

    }
    protected void GridViewShowEmployeePerDept_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void chkbForSelectALL_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void chkbALL_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        CallAbsentListSp();

        string selectionfor, parameter;

        selectionfor = "{hrms_AbsentList_byDept.Userid}='" + current.UserId + "'";//"{hrms_AbsentList_byDept.Userid}='" + Session[GlobalData.SessionUserId].ToString() + "'";

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

        if (RadioButtonList1.SelectedValue == "1")
        {
            cond = 1;
        }
        else if (RadioButtonList1.SelectedValue == "2")
        {
            cond = 2;
        }
        else if (RadioButtonList1.SelectedValue == "3")
        {
            cond = 3;
        }
        else
        {
            cond = 0;
        }

        empid = current.UserId;//Session[GlobalData.SessionUserId].ToString();

        LeaveProcess lvp = new LeaveProcess();

        string al = lvp.spCxecuteAbsentListReport(Session["ConnectionStr"].ToString(), fdate, lDate, deptid, empid, cond, rnode.ToString(), Session["db"].ToString());

    }

    protected void btnShowAttendance_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();


        string empid = Session[GlobalData.SessionUserId].ToString();

        if (RadioButtonList1.SelectedValue == "1")
            CallAbsentListSp();
        else
            empid = "";

        string sql = "select EmployeeID,EmployeeName,adate from hrms_AbsentList_byDept where userid='" + empid.ToString() + "' order by EmployeeID,adate";

        dt = DataProcess.GetData(Session["ConnectionStr"].ToString(), sql);

        grdShiftAllocationPreview.DataSource = dt;
        grdShiftAllocationPreview.DataBind();

        if (grdShiftAllocationPreview.Rows.Count > 0)
        {
            btnSubmitAttendance.Visible = true;
        }
        else
        {
            btnSubmitAttendance.Visible = false;
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
                LeaveProcess objLeaveProcess = new LeaveProcess();
                List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();

                for (int i = 0; i < grdShiftAllocationPreview.Rows.Count; i++)
                {
                    CheckBox chkbox = grdShiftAllocationPreview.Rows[i].FindControl("CheckIndv") as CheckBox;

                    if (chkbox.Checked == true)
                    {
                        LeaveProcessHeader offphdr = new LeaveProcessHeader();

                        offphdr.EmpID = grdShiftAllocationPreview.Rows[i].Cells[0].Text;
                        offphdr.ClaiminDdate = Convert.ToDateTime(grdShiftAllocationPreview.Rows[i].Cells[2].Text);
                        lvphdrlst.Add(offphdr);
                    }
                }

                LeaveProcess lvproc = new LeaveProcess();
                string retval = lvproc.SaveApproveAttendance(lvphdrlst, myCommand, Session["db"].ToString());
                if (retval.ToString() == "")
                {
                    myTrans.Rollback("SaveAllTransaction");
                }
                else
                {
                    myTrans.Commit();
                }

                btnShowAttendance_Click(null, null);

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

}