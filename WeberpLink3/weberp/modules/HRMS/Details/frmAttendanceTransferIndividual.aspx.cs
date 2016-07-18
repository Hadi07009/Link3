using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Details_frmAttendanceTransferIndividual : System.Web.UI.Page
{
    private const string Rnode = "d";
    readonly string _connectionString = ConfigurationManager.AppSettings["UbasysConnectionString"];
    readonly string _connectionStringAccessAushulia = ConfigurationManager.AppSettings["AccessConnectionStringAushulia"];
    readonly string _connectionStringAccessUser = ConfigurationManager.AppSettings["AccessConnectionStringUser"];
    string _connectionStringAccessHO = ConfigurationManager.AppSettings["AccessConnectionStringHO"];
    string _connectionStringExcel = ConfigurationManager.ConnectionStrings["Excel07ConStringFixed"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {

        clsStatic.CheckUserAuthentication(true);

        GlobalData.ConfirmBox(btnTransferAttendance, "Are you sure to import data ?");
        GlobalData.ConfirmBox(btnUpdateData, "Are you sure to post ?");
        GlobalData.ConfirmBox(btnPostAttendanceNew, "Are you sure to post attendance ?");
        GlobalData.ConfirmBox(btnPostAttendanceFromXLS, "Are you sure to post manual attendance ?");
        GlobalData.ConfirmBox(btnAttendanceImportFromXl, "Are you sure to import xl data ?");
        //GlobalData.ConfirmBox(btnPostHoliday, "Are you sure to post holiday ?");

        if (!Page.IsPostBack)
        {
            LoadCompanyByUserPermission("ADM", Rnode);
            lblImport.Text = "";
            ddlSheetName.Items.Clear();
            ClsDropDownListController.EnableDisableDropDownList(ddlcompany);
            ddlcompany_SelectedIndexChanged(sender, e);

            txtFromDate0.Text = DateProcess.GetServerDate(_connectionString).ToString().Substring(0,10);



        }
        if (IsPostBack)
        {
            Session["SelectedSheetNames"] = ddlSheetName.SelectedValue;
            ddlSheetName.Items.Clear();
            if (Session["SheetNames"] != null)
            {
                DataTable dtSheet = (DataTable)Session["SheetNames"];
                ddlSheetName.Items.Insert(0, new ListItem("--- Please Select ---", "-1"));
                foreach (DataRow dr in dtSheet.Rows)
                {
                    ListItem lst = new ListItem();
                    lst.Value = dr["TABLE_NAME"].ToString();
                    lst.Text = dr["TABLE_NAME"].ToString();
                    ddlSheetName.Items.Add(lst);
                }
            }
            else
            {
                ddlSheetName.Items.Insert(0, new ListItem("--- No Data Found ---", "-1"));
            }
            Session["SheetNames"] = null;
        }
    }

    private void LoadCompanyByUserPermission(string userid, string nodeid)
    {
        DataTable dt = new DataTable();
        ListItem lst;
        ddlcompany.Items.Clear();
        ddlcompany.Items.Add("");
        dt = AccessPermission.GetCompanyByUserandNodeid(_connectionString, userid, nodeid);
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
        Session["ConnectionStr"] = constr.ToString();
        LoadofficeLocation();
        LoadDepartmentId();
        txtEmployeeSearch_AutoCompleteExtender.ContextKey = Session[GlobalData.sessionConnectionstring].ToString();
    }

    private void LoadofficeLocation()
    {
        DataTable dt = new DataTable();
        dt = DataProcess.GetData(Session[GlobalData.sessionConnectionstring].ToString(), "select distinct trans_det_divID from hrms_trans_det");
        //ddlofficeLocation.Items.Clear();
        foreach (DataRow dr in dt.Rows)
        {
            //ddlofficeLocation.Items.Add(dr["trans_det_divID"].ToString());          
        }
    }

    private void MessageBoxShow(Page page, string message)
    {
        Literal ltr = new Literal();
        ltr.Text = @"<script type='text/javascript'> alert('" + message + "') </script>";
        page.Controls.Add(ltr);
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

    public void LoadShiftAllocationRecord()
    {
        LeaveProcess lvproc = new LeaveProcess();
        string DateOfAllocation = txtFromDate.SelectedDate.ToString();
        grdShiftAllocationPreview.DataSource = lvproc.GetAllocatedShifrRecord(Session["ConnectionStr"].ToString(), txtFromDate.SelectedDate.ToString(), ddlDepartmentId.SelectedValue);
        grdShiftAllocationPreview.DataBind();
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
                    //row.Cells[0].RowSpan = previousRow.Cells[0].RowSpan < 2 ? 2 :
                    //                           previousRow.Cells[0].RowSpan + 1;
                    //previousRow.Cells[0].Visible = false;
                    for (int i = 0; i < row.Cells.Count; i++)
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
        grdLoadShiftType.DataSource = dt;
        grdLoadShiftType.DataBind();
    }

    public void LoadShiftType1()
    {
        DataTable dt = new DataTable();
        string strSql = "select Shift_Mas_Code as [Shift Code],Shift_Mas_Desc as Shift,Shift_Mas_From as [From],Shift_Mas_To as [To] from hrms_shift_mas";
        dt = DataProcess.GetData(Session["ConnectionStr"].ToString(), strSql);
        grdLoadShiftType0.DataSource = dt;
        grdLoadShiftType0.DataBind();
    }

    protected void btnShowEmployee_Click(object sender, EventArgs e)
    {
        if (ddlDepartmentId.SelectedIndex == -1) return;
        DataTable dt = new DataTable();
        lblShowDept.Text = ddlDepartmentId.SelectedItem.Text;
        LabelShowDate.Text = txtFromDate.SelectedDate.ToString();
        string strSql = "select EmpID,EmpName,Designation from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id where DeptID='" + ddlDepartmentId.SelectedValue + "' and Emp_Mas_Term_Flg='N'";
        dt = DataProcess.GetData(Session["ConnectionStr"].ToString(), strSql);
        GridViewShowEmployeePerDept.DataSource = dt;
        GridViewShowEmployeePerDept.DataBind();
        LoadShiftType();
        this.ModalPopupExtender1.Show();
    }

    protected void btnApply_Click(object sender, EventArgs e)
    {
        if (GridViewShowEmployeePerDept.Rows.Count != 0)
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
                objLeaveProcess.DeletePreviousAllcatedShift(myCommand, ddlDepartmentId.SelectedValue.ToString(), txtFromDate.SelectedDate.ToString());
                List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
                for (int i = 0; i < GridViewShowEmployeePerDept.Rows.Count; i++)
                {
                    CheckBox chkbox = GridViewShowEmployeePerDept.Rows[i].FindControl("CheckRet") as CheckBox;
                    CheckBoxList chkboxForShiftStatus = GridViewShowEmployeePerDept.Rows[i].FindControl("cblForShiftSelect") as CheckBoxList;
                    if (chkbox.Checked == true)
                    {
                        List<Guid> things = new List<Guid>();
                        foreach (ListItem item in chkboxForShiftStatus.Items)
                        {
                            if (item.Selected)
                            {
                                LeaveProcessHeader offphdr = new LeaveProcessHeader();
                                offphdr.DateForSA = txtFromDate.SelectedDate.ToString();
                                offphdr.ShiftID = item.Value;
                                offphdr.InTime = "";
                                offphdr.OutTime = "";
                                offphdr.EmpID = GridViewShowEmployeePerDept.Rows[i].Cells[2].Text.ToString();
                                lvphdrlst.Add(offphdr);
                            }
                        }
                    }
                }
                int shiftid = Convert.ToInt32(ddlShift.SelectedValue);
                string DateTimeForAllocation = (txtFromDate.SelectedDate).ToString();
                LeaveProcess lvproc = new LeaveProcess();
                string retval = lvproc.SaveShiftAllocationData(lvphdrlst, myCommand);
                if (retval.ToString() == "")
                {
                    myTrans.Rollback("SaveAllTransaction");
                }
                else
                {
                    myTrans.Commit();
                }
                LoadShiftAllocationRecord();
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
            chk.Enabled = false;

            CheckBoxList chkboxForShiftStatus = (CheckBoxList)e.Row.FindControl("cblForShiftSelect");
            LeaveProcess lvproc = new LeaveProcess();
            DataTable ApplicantId = lvproc.GetAllocatedEmpId(Session["ConnectionStr"].ToString(), EmpId, DateAllocation);
            if (ApplicantId.Rows.Count != 0)
            {
                foreach (DataRow dr in ApplicantId.Rows)
                {
                    int sID = Convert.ToInt32(dr[0].ToString());

                    chkboxForShiftStatus.Items[sID - 1].Selected = true;
                    chkboxForShiftStatus.Items[sID - 1].Enabled = true;
                }
            }
        }
    }

    protected void ddlDepartmentId_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadShiftAllocationRecord();
    }

    protected void btnShowIndividual_Click(object sender, EventArgs e)
    {
        if (ddlDepartmentId.SelectedIndex == -1) return;
        DataTable dt = new DataTable();
        lblShowEmp.Text = GetEmployeeInformation(txtEmpId.Text);
        lblShowEmpId.Text = txtEmpId.Text;
        DateTime fdate = Convert.ToDateTime(txtFromDate.SelectedDate);
        DateTime ldate = Convert.ToDateTime(txtToDate.SelectedDate);
        lblShowadt.Text = txtFromDate.SelectedDate.ToString("dd/MM/yyyy") + " TO " + txtToDate.SelectedDate.ToString("dd/MM/yyyy");
        LeaveProcess lvproc = new LeaveProcess();
        dt = lvproc.GetAllocatedShiftbyempid(Session["ConnectionStr"].ToString(), txtEmpId.Text, fdate, ldate);
        GridViewShowEmployeeIndividual.DataSource = dt;
        GridViewShowEmployeeIndividual.DataBind();
        LoadShiftType1();
        this.ModalPopupExtender2.Show();
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
        if (GridViewShowEmployeeIndividual.Rows.Count != 0)
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
                objLeaveProcess.DeletePreviousAllcatedShiftByemployeeID(myCommand, lblShowEmpId.Text.Trim().ToString(), txtFromDate.SelectedDate.ToString(), txtToDate.SelectedDate.ToString());
                List<LeaveProcessHeader> lvphdrlst = new List<LeaveProcessHeader>();
                for (int i = 0; i < GridViewShowEmployeeIndividual.Rows.Count; i++)
                {
                    CheckBox chkbox = GridViewShowEmployeeIndividual.Rows[i].FindControl("CheckRetIndv") as CheckBox;
                    CheckBoxList chqlist = GridViewShowEmployeeIndividual.Rows[i].FindControl("cblForShiftSelectShiftIndv") as CheckBoxList;
                    if (chkbox.Checked == true)
                    {
                        foreach (ListItem item in chqlist.Items)
                        {
                            if (item.Selected)
                            {
                                LeaveProcessHeader offphdr = new LeaveProcessHeader();
                                offphdr.DateForSA = GridViewShowEmployeeIndividual.Rows[i].Cells[2].Text;
                                offphdr.ShiftID = item.Value;
                                offphdr.InTime = "";
                                offphdr.OutTime = "";
                                offphdr.EmpID = lblShowEmpId.Text.Trim();
                                lvphdrlst.Add(offphdr);
                            }
                        }
                    }
                }
                LeaveProcess lvproc = new LeaveProcess();
                string retval = lvproc.SaveShiftAllocationDataByEmpid(lvphdrlst, myCommand);
                if (retval.ToString() == "")
                {
                    myTrans.Rollback("SaveAllTransaction");
                }
                else
                {
                    myTrans.Commit();
                }
                LoadShiftAllocationRecord();
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

    protected void btnTransferAttendance_Click(object sender, EventArgs e)
    {
        try
        {
            lblImport.Text = "";
            string sql = "";
            string ConnectionStr = "Data Source=(local);Initial Catalog=SSG;User ID=sa;Password=sql@7circlemis;";
            sql = "Delete from HrmsRawData";
            DataProcess.ExecuteQuery(ConnectionStr, sql);
            DataTable dt = new DataTable();
            dt = ReadDataFromTextFile();
            foreach (DataRow dr in dt.Rows)
            {
                sql = "Insert into HrmsRawData(Rdata)values('" + dr[0].ToString() + "')";
                DataProcess.ExecuteQuery(ConnectionStr, sql);
            }
            int rt = RawdataProcessing();
            //Read Data from XlS file 
            //dt = ReadDataFromExcelfile();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    string sql = "Insert into Hrms_Movement_Data(MachineID,MovDate,MovTime,CardId,MovTimeFormat,MovInTimeFormat,MovOutTimeFormat,Rdata,Flag,Remarks,Status)values('000',Convert(Datetime,'" + dr["MovementDate"].ToString() + "',103),'091005','" + dr["EmpID"].ToString() + "','09:00 AM','09:00 AM','09:05 AM','000000000000000000000000000','A','XLS','Y')";
            //    DataProcess.ExecuteQuery(ConnectionStr, sql);
            //}
            string msg = "Data Import Successful";
            if (rt == 0)
            {
                MessageBoxShow(this, msg);
            }
        }
        catch (Exception ex)
        {
            lblImport.Text = "Data Import Failed";
        }
    }

    private DataTable ReadDataFromTextFile()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Textdata", typeof(string));
        int counter = 0;
        string line;
        System.IO.StreamReader file = new System.IO.StreamReader("C:\\Import\\Textdata.txt");
        while ((line = file.ReadLine()) != null)
        {
            string Linetext = line.ToString();
            dt.Rows.Add(Linetext.ToString());
            counter++;
        }
        file.Close();
        return dt;
    }

    private DataTable ReadDataFromExcelfile()
    {
        System.Data.DataSet ds = new System.Data.DataSet();
        string strFileName = "C:\\Import\\XLdata.xls";
        System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0; Data Source = " + strFileName + "; Extended Properties = \"Excel 8.0;HDR=Yes;IMEX=1\";");
        conn.Open();
        try
        {
            string sheetName = "M_Attendance" + "$";
            string strQuery = "SELECT * FROM [" + sheetName + "]";
            System.Data.OleDb.OleDbDataAdapter adapter = new System.Data.OleDb.OleDbDataAdapter(strQuery, conn);
            adapter.Fill(ds);
        }
        catch (Exception ex)
        {
        }
        finally
        {
            conn.Close();
        }
        return ds.Tables[0];
    }

    protected void btnUpdateData_Click(object sender, EventArgs e)
    {
        string ConnectionStr = "Data Source=(local);Initial Catalog=SSG;User ID=sa;Password=sql@7circlemis;";
        SqlConnection oConnection = new SqlConnection(ConnectionStr);
        oConnection.Open();
        try
        {
            SqlCommand cmd = new SqlCommand("[spCallAttendance]", oConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter returnnVal = cmd.Parameters.Add("returnnVal", SqlDbType.Int);
            returnnVal.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            string msg = "";
            if ((int)returnnVal.Value == 0)
            {
                msg = "Data posted Successful";
            }
            else
            {
                msg = "Error...please try again";
            }
            MessageBoxShow(this, msg);
        }
        catch (Exception ex)
        {
            oConnection.Close();
        }
        finally
        {
            oConnection.Close();
        }
    }

    private int RawdataProcessing()
    {
        int rt = -1;
        string ConnectionStr = "Data Source=(local);Initial Catalog=SSG;User ID=sa;Password=sql@7circlemis;";
        SqlConnection oConnection = new SqlConnection(ConnectionStr);
        oConnection.Open();
        try
        {
            SqlCommand cmd = new SqlCommand("[spRawDataTransfer]", oConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            //SqlParameter EmployeeID = cmd.Parameters.Add("@rEmpid", SqlDbType.NVarChar, 10);
            //EmployeeID.Value = txtempid.Text.Trim().ToString();
            // return value
            SqlParameter returnnVal = cmd.Parameters.Add("returnnVal", SqlDbType.Int);
            returnnVal.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            rt = (int)returnnVal.Value;
        }
        catch (Exception ex)
        {
            oConnection.Close();
        }
        finally
        {
            oConnection.Close();
        }
        return rt;
    }

    private DataTable ReadDataFromTextFilebak()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Textdata", typeof(string));
        int counter = 0;
        string line;
        System.IO.StreamReader file = new System.IO.StreamReader("c:\\Import\\Textdata.txt");
        while ((line = file.ReadLine()) != null)
        {
            string Linetext = line.ToString();
            dt.Rows.Add(Linetext.ToString());
            counter++;
        }
        file.Close();
        return dt;
    }

    protected void btnPostAttendanceNew_Click(object sender, EventArgs e)
    {

        string ConnectionStr = "Data Source=(local);Initial Catalog=SSG;User ID=sa;Password=sql@7circlemis;";
        SqlConnection oConnection = new SqlConnection(ConnectionStr);
        oConnection.Open();
        string db = "";
        db = ddlcompany.SelectedItem.Value;
        if (AttendanceUpdateforHOshiftCompanywise(db.ToString()) != 0) return;
        string tblName = "Hrms_Movement_Data";
        string tblName1 = db + ".dbo.HrMs_Emp_mas";
        string tblName2 = db + ".dbo.HRMS_Shift_Allocation";
        string tblName3 = db + ".dbo.hrms_shift_mas";
        string tblName4 = db + ".dbo.hrms_emp_las_pos_view";
        string tblName5 = db + ".dbo.Hrms_atnd_det";
        string tblName6 = db + ".dbo.emp_details";
        string tblName7 = "ViewCshiftIntime";
        string tblName8 = "ViewCshiftOuttime";
        string tblName9 = "ViewBshiftIntime";
        string tblName10 = "ViewPreviousDayShift";


        string sql = "select convert(datetime,a.MovDate,103) as MovDate,a.CardId,b.Emp_Mas_Emp_Id,isnull(ShiftID,0) as ShiftID,"
        + " [dbo].[ReturnShiftByPunchTime](min(a.MovTime),MAX(a.MovTime)) as ShiftCode,min(a.MovTime) as Intime,"
        + " MAX(a.MovTime) as Outtime,max(f.MaxPos) as Emppos,isnull(gg.CshiftIn,'213000') as CshiftIn ,isnull(hh.cshiftout,'060100') as cshiftout,isnull(ii.BshiftIn,'140100') as BshiftIn,isnull(jj.ShiftCode,'') as Preshift,isnull(kk.atnd_det_offlg,'') as atnd_det_offlg"
        + " from " + tblName + " a "
        + " inner join " + tblName1 + " b on a.CardID=b.Emp_Mas_CardID "
        + " inner join " + tblName6 + " em on em.empid=b.emp_mas_emp_id "
        + " left outer join " + tblName2 + " d on d.EmpID=b.Emp_Mas_Emp_Id and d.DateAllocation=a.MovDate"
        + " left outer join " + tblName4 + " f on f.Trans_Det_Emp_Id=b.Emp_Mas_Emp_Id "
        + " left outer join " + tblName7 + " gg on gg.cardid=a.CardID and gg.movdate=a.MovDate "
        + " left outer join " + tblName8 + " hh on hh.cardid=a.CardID and hh.CshiftMovoutdate=a.MovDate "
        + " left outer join " + tblName9 + " ii on ii.cardid=a.CardID and ii.movdate=a.MovDate "
        + " left outer join " + tblName10 + " jj on jj.cardid=a.CardID and jj.CompareDate=a.MovDate "
        + " left outer join " + tblName5 + " kk on kk.atnd_det_emp_id=b.emp_mas_emp_id and kk.atnd_det_date=a.movdate "
        + " where OfficeID='FO'"
        + " group by a.MovDate,b.Emp_Mas_Emp_Id,ShiftID,a.CardId,gg.CshiftIn,hh.cshiftout,ii.BshiftIn,jj.ShiftCode,kk.atnd_det_offlg";


        DataTable dt = new DataTable();
        dt = DataProcess.GetData(ConnectionStr, sql);


        string empid = "";
        DateTime movementdate;
        string shiftcode = "";
        string intime = "";
        string outtime = "";
        string mh = "";
        string cardid = "";
        string cshiftinopt = "";
        string cshiftoutopt = "";
        string bshiftinopt = "";
        string previousshift = "";
        string flag = "";
        int empos = 1;

        foreach (DataRow dr in dt.Rows)
        {
            empid = dr["Emp_Mas_Emp_Id"].ToString();
            movementdate = Convert.ToDateTime(dr["MovDate"].ToString());
            shiftcode = dr["ShiftCode"].ToString();
            intime = TimeFormat(dr["Intime"].ToString());
            outtime = TimeFormat(dr["Outtime"].ToString());
            mh = TimeDuration(intime, outtime);
            cardid = dr["CardId"].ToString();
            previousshift = dr["Preshift"].ToString();
            flag = dr["atnd_det_offlg"].ToString();
            empos = Convert.ToInt32(dr["Emppos"].ToString());


            cshiftinopt = TimeFormat(dr["CshiftIn"].ToString());
            cshiftoutopt = TimeFormat(dr["cshiftout"].ToString());
            bshiftinopt = TimeFormat(dr["BshiftIn"].ToString());

            //shiftcode = ShiftCodeByPunchTime(dr["Intime"].ToString(), dr["Outtime"].ToString());

            if (shiftcode == "A" && previousshift == "C")
                goto nextline;

            if (shiftcode == "C")
            {
                intime = cshiftinopt;
                outtime = cshiftoutopt;
                mh = TimeDuration(intime, outtime);
            }
            else if (shiftcode == "B")
            {
                intime = bshiftinopt;
                mh = TimeDuration(intime, outtime);
            }

            if (flag == "")
            {
                sql = "Insert into " + tblName5 + "([Atnd_Det_Emp_Id],[Atnd_det_date],[Atnd_Det_sftID],[Atnd_det_intime],"
                    + " [Atnd_det_outtime],[Atnd_det_hrs],[Atnd_det_rmks],[Atnd_det_offlg],[Atnd_Emp_Pos])"
                    + " values('" + empid + "',Convert(Datetime,'" + movementdate + "',103),'" + shiftcode + "','" + intime + "','" + outtime + "','" + mh + "','SYS','A'," + empos + ")";
            }
            else
            {
                sql = "update " + tblName5 + " set Atnd_Det_sftID='" + shiftcode + "',Atnd_det_intime='" + intime + "',Atnd_det_outtime='" + outtime + "',Atnd_det_hrs='" + mh + "',Atnd_det_rmks='SYS',Atnd_det_offlg='A',atnd_emp_pos=" + empos + ""
                      + " where convert(datetime,Atnd_det_date,103)=convert(datetime,'" + movementdate + "',103) and Atnd_Det_Emp_Id='" + empid + "' and Atnd_det_offlg not in('L','S')";
            }

            DataProcess.ExecuteQuery(ConnectionStr, sql);


        nextline:
            previousshift = "";

        }

        MessageBoxShow(this, "Data posted successful");


    }

    private string TimeFormat(string ptime)
    {
        int left = Convert.ToInt32(ptime.Substring(0, 2).ToString());
        string rightpart = ptime.Substring(2, 2).ToString();
        string leftpart = "";
        string ampm = "";
        string tf = "";

        if (left >= 12)
        {
            left = left - 12;
            if (left == 0) left = 12;
            leftpart = String.Format("{0:00}", left);
            ampm = "PM";
        }
        else
        {
            if (left == 0) left = 12;
            leftpart = String.Format("{0:00}", left);
            ampm = "AM";
        }

        tf = leftpart + ":" + rightpart + " " + ampm;

        return tf;

    }


    private string TimeDuration(string intime, string outtime)
    {
        DateTime intimedate = Convert.ToDateTime("01/01/2001 " + intime);
        DateTime outtimedate = Convert.ToDateTime("01/01/2001 " + outtime);

        int duration = 0;

        if (intimedate > outtimedate)
            outtimedate = Convert.ToDateTime("02/01/2001 " + outtime);

        duration = (int)(outtimedate - intimedate).TotalMinutes;

        int hr = duration / 60;
        int min = duration % 60 > 0 ? duration % 60 : 1;



        string dur = string.Format("{0:00}", hr).ToString() + ":" + string.Format("{0:00}", min).ToString();

        return dur;
    }

    private string TimeDuration(int duration)
    {

        int hr = duration / 60;
        int min = duration % 60 > 0 ? duration % 60 : 1;

        string dur = string.Format("{0:00}", hr).ToString() + ":" + string.Format("{0:00}", min).ToString();

        return dur;
    }

    private string ShiftCodeByPunchTime(string ptimein, string ptimeout)
    {
        string shiftcode = "";
        int ptime1 = Convert.ToInt32(ptimein.Substring(0, 4));
        int ptime2 = Convert.ToInt32(ptimeout.Substring(0, 4));

        if (ptime1 == ptime2)
        {
            if (ptime1 >= 530 && ptime1 < 630)
            {
                shiftcode = "A";
            }
            else if (ptime1 >= 630 && ptime1 < 730)
            {
                shiftcode = "S";
            }
            else if (ptime1 >= 730 && ptime1 < 1330)
            {
                shiftcode = "G";
            }
            else if (ptime1 >= 1330 && ptime1 < 2130)
            {
                shiftcode = "B";
            }
            else if (ptime1 >= 2130 && ptime1 <= 2400)
            {
                shiftcode = "C";
            }

        }
        else
        {
            if (ptime1 < 645 && ptime2 >= 2100)
            {
                shiftcode = "C";
            }
            else if ((ptime1 >= 530 && ptime1 < 700) && ptime2 < 2359)
            {
                shiftcode = "A";
            }
            else if ((ptime1 >= 700 && ptime1 < 830) && ptime2 < 2359)
            {
                shiftcode = "S";
            }
            else if ((ptime1 >= 830 && ptime1 < 1300) && ptime2 < 2359)
            {
                shiftcode = "G";
            }
            else if ((ptime1 >= 1300 && ptime1 < 2130) && ptime2 < 2359)
            {
                shiftcode = "B";
            }
            else
            {
                shiftcode = "G";
            }

        }

        return shiftcode;

    }

    private int HolidayDataEntryAllCompany(DateTime fromdate)
    {
        int rt = 1;
        try
        {        
            var storedProcedureComandTest = "exec [spHolidayUpdateBySystem] '" + fromdate.ToString() + "'";
            StoredProcedureExecutor.StoredProcedureExecuteNonQuery(_connectionString, storedProcedureComandTest);
            rt = 0;
        }
        catch (Exception ex)
        {
            return rt;
        }
        
        return rt;

    }

    private int AttendanceUpdateforHOshiftCompanywise(string db)
    {
        int rt = -1;

        string ConnectionStr = "Data Source=(Local);Initial Catalog=SSG;User ID=sa;Password=;";
        SqlConnection oConnection = new SqlConnection(ConnectionStr);
        oConnection.Open();

        try
        {
            SqlCommand cmd = new SqlCommand("[spAttendanceUpdateBySystemCompanyWiseHO]", oConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter dbo = cmd.Parameters.Add("@db", SqlDbType.NVarChar, 10);
            dbo.Value = db.ToString();

            // return value
            SqlParameter returnnVal = cmd.Parameters.Add("returnnVal", SqlDbType.Int);
            returnnVal.Direction = ParameterDirection.ReturnValue;

            cmd.ExecuteNonQuery();

            rt = (int)returnnVal.Value;

        }
        catch (Exception ex)
        {
            oConnection.Close();
        }
        finally
        {
            oConnection.Close();
        }

        return rt;

    }

    protected void btnPostHoliday_Click(object sender, EventArgs e)
    {
        if (CheckDate(txtFromDate0.Text) == false)
        {
            MessageBox1.ShowWarning("Date is not correct. Please enter valid date");
            return;
        }

        DateTime fromdate = Convert.ToDateTime(txtFromDate0.Text);

        if (HolidayDataEntryAllCompany(fromdate) == 0)
        {
            MessageBox1.ShowSuccess("Holiday updated Successfully ");
        }
    }
    protected void btnAttendanceImportFromXl_Click(object sender, EventArgs e)
    {
        string ConnectionStr = "Data Source=203.76.124.181;Initial Catalog=SSG;User ID=sa;Password=sql@7circlemis;";
        SqlConnection oConnection = new SqlConnection(ConnectionStr);
        oConnection.Open();

        try
        {
            string db = "";

            db = ddlcompany.SelectedItem.Value;

            DataTable dt = new DataTable();

            dt = ReadDataFromExcelfile();

            string sql = "";
            string empid = "";
            string empname = "";
            DateTime movdate;
            int shiftid = 0;
            string attendancetype = "";
            string AttendanceCode = "";
            string Remarks = "";

            sql = "delete from HrmsAttendanceImportFromXL";

            DataProcess.ExecuteQuery(ConnectionStr, sql);


            foreach (DataRow dr in dt.Rows)
            {
                empid = dr["Employee ID"].ToString();

                if (empid != "")
                {
                    empname = dr["Employee Name"].ToString();
                    movdate = Convert.ToDateTime(dr["Date"].ToString());
                    shiftid = 0;
                    attendancetype = dr["Type of Attendance"].ToString();
                    AttendanceCode = dr["Type of Attendance"].ToString();
                    Remarks = "XLS:" + dr["Remarks"].ToString();

                    sql = "insert into HrmsAttendanceImportFromXL([EmployeeId],[EmployeeName],[MovDate],[ShiftId],[AttendanceType],[AttendanceCode],[Remarks])"
                        + " values('" + empid.ToString() + "','" + empname.ToString() + "',convert(Datetime,'" + movdate + "',103)," + shiftid + ",'" + attendancetype.ToString() + "','" + AttendanceCode.ToString() + "','" + Remarks.ToString() + "')";
                    DataProcess.ExecuteQuery(ConnectionStr, sql);
                }
            }

            sql = "Delete from hrms_movement_data where left([Remarks],3)='XLS' and flag<>'A'";
            DataProcess.ExecuteQuery(ConnectionStr, sql);

            sql = "insert into hrms_movement_data select '000' as MachineID,MovDate,'093000',EmployeeId,'09:30 AM','09:30 AM','09:30 AM','',AttendanceCode,Remarks,'Y' from [HrmsAttendanceImportFromXL]";
            DataProcess.ExecuteQuery(ConnectionStr, sql);


            MessageBoxShow(this, "Attendance Import Successful");

        }
        catch (Exception ex)
        {
            oConnection.Close();
        }
        finally
        {
            oConnection.Close();
        }


    }



    private int AttendanceUpdateforCompanywiseFromXls(string db)
    {
        int rt = -1;

        string ConnectionStr = "Data Source=203.76.124.181;Initial Catalog=SSG;User ID=sa;Password=sql@7circlemis;";
        SqlConnection oConnection = new SqlConnection(ConnectionStr);
        oConnection.Open();

        try
        {
            SqlCommand cmd = new SqlCommand("[spAttendanceUpdateBySystemCompanyWiseFromXLS]", oConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter dbo = cmd.Parameters.Add("@db", SqlDbType.NVarChar, 10);
            dbo.Value = db.ToString();

            // return value
            SqlParameter returnnVal = cmd.Parameters.Add("returnnVal", SqlDbType.Int);
            returnnVal.Direction = ParameterDirection.ReturnValue;

            cmd.ExecuteNonQuery();

            rt = (int)returnnVal.Value;

        }
        catch (Exception ex)
        {
            oConnection.Close();
        }
        finally
        {
            oConnection.Close();
        }

        return rt;

    }
    private int HolidayUpdateforCompanywiseFromXls(string db)
    {
        int rt = -1;

        string ConnectionStr = "Data Source=(Local);Initial Catalog=SSG;User ID=sa;Password=;";
        SqlConnection oConnection = new SqlConnection(ConnectionStr);
        oConnection.Open();

        try
        {
            SqlCommand cmd = new SqlCommand("[spHolidayUpdateBySystemCompanyWiseFromXLS]", oConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter dbo = cmd.Parameters.Add("@db", SqlDbType.NVarChar, 10);
            dbo.Value = db.ToString();

            // return value
            SqlParameter returnnVal = cmd.Parameters.Add("returnnVal", SqlDbType.Int);
            returnnVal.Direction = ParameterDirection.ReturnValue;

            cmd.ExecuteNonQuery();

            rt = (int)returnnVal.Value;

        }
        catch (Exception ex)
        {
            oConnection.Close();
        }
        finally
        {
            oConnection.Close();
        }

        return rt;

    }
    private int LeaveUpdateforCompanywiseFromXls(string db)
    {
        int rt = -1;

        string ConnectionStr = "Data Source=(Local);Initial Catalog=SSG;User ID=sa;Password=;";
        SqlConnection oConnection = new SqlConnection(ConnectionStr);
        oConnection.Open();

        try
        {
            SqlCommand cmd = new SqlCommand("[spLeaveUpdateBySystemCompanyWiseFromXLS]", oConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter dbo = cmd.Parameters.Add("@db", SqlDbType.NVarChar, 10);
            dbo.Value = db.ToString();

            // return value
            SqlParameter returnnVal = cmd.Parameters.Add("returnnVal", SqlDbType.Int);
            returnnVal.Direction = ParameterDirection.ReturnValue;

            cmd.ExecuteNonQuery();

            rt = (int)returnnVal.Value;

        }
        catch (Exception ex)
        {
            oConnection.Close();
        }
        finally
        {
            oConnection.Close();
        }

        return rt;

    }
    protected void btnPostAttendanceFromXLS_Click(object sender, EventArgs e)
    {
        string db = "";

        string remarks = "";

        db = ddlcompany.SelectedItem.Value;

        if (db == "") return;

        if (AttendanceUpdateforCompanywiseFromXls(db) == 0)
        {
            remarks = "Attendance";
        }
        if (HolidayUpdateforCompanywiseFromXls(db) == 0)
        {
            remarks = "Holiday" + "," + remarks;
        }
        if (LeaveUpdateforCompanywiseFromXls(db) == 0)
        {
            remarks = "Leave" + "," + remarks;
        }

        if (remarks == "")
        {
            MessageBoxShow(this, "No Data Found");
        }
        else
        {
            remarks = remarks + " update successful";

            MessageBoxShow(this, remarks);
        }


    }
    protected void btnImportLocal_Click(object sender, EventArgs e)
    {

        //dt = GetDataTableExcel("C:\\XLdata.xls", "M_Attendance");


        string path = Server.MapPath("~/");

        string fileName = path + "XLData.xls";



        // string fileName = FileUploadTestFile.ResolveClientUrl(FileUploadTestFile.PostedFile.FileName);
        int count = 0;
        //DataClassesDataContext conLinq = new DataClassesDataContext("Data Source=server name;Initial Catalog=Database Name;Integrated Security=true");
        try
        {
            DataTable dtExcel = new DataTable();
            string SourceConstr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + fileName + "';Extended Properties= 'Excel 8.0;HDR=Yes;IMEX=1'";
            OleDbConnection con = new OleDbConnection(SourceConstr);
            string query = "Select * from [M_Attendance$]";
            OleDbDataAdapter data = new OleDbDataAdapter(query, con);
            data.Fill(dtExcel);
            //for (int i = 0; i < dtExcel.Rows.Count; i++)
            //{
            //   try
            //   {
            //      count += conLinq.ExecuteCommand("insert into table name values(" + dtExcel.Rows[i][0] + "," + dtExcel.Rows[i][1] + ",'" + dtExcel.Rows[i][2] + "',"+dtExcel.Rows[i][3]+")");
            //   }
            //   catch (Exception ex)
            //   {
            //      continue;
            //   }
            //}


            GridView1.DataSource = dtExcel;
            GridView1.DataBind();





        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            // conLinq.Dispose();
        }

    }

    private DataTable GetDataTableExcel(string strFileName, string sheetName)
    {

        System.Data.DataSet ds = new System.Data.DataSet();

        System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0; Data Source = " + strFileName + "; Extended Properties = \"Excel 8.0;HDR=Yes;IMEX=1\";");

        conn.Open();

        try
        {

            //  Table = sheetName;
            sheetName = sheetName + "$";
            string strQuery = "SELECT * FROM [" + sheetName + "]";

            System.Data.OleDb.OleDbDataAdapter adapter = new System.Data.OleDb.OleDbDataAdapter(strQuery, conn);

            adapter.Fill(ds);

        }
        catch (Exception ex)
        {
        }
        finally
        {
            conn.Close();
        }

        return ds.Tables[0];

    }

    private string AttachFileSave()
    {
        HttpFileCollection hfc = Request.Files;


        string rowfilename = "";


        for (int i = 0; i < hfc.Count; i++)
        {
            HttpPostedFile hpf = hfc[i];

            if (hpf.ContentLength > 0)
            {
                rowfilename = System.IO.Path.GetFileName(hpf.FileName).Replace("'", "''");
                //string filename = SalesOrderNo + "-" + System.IO.Path.GetFileName(hpf.FileName).Replace("'", "''");
                //hpf.SaveAs(Server.MapPath("~/SalesAttachfile/") + "\\" + filename);
                //string attach = @"insert into [ProcessFileUpload]([ReferenceNo],[FileName],[SavedFileName],[SerialNo],[UploadDate],[UploadBy],[Status])values('" + SalesOrderNo + "','" + rowfilename + "','" + filename + "'," + (i + 1) + ",'" + System.DateTime.Now + "','" + Session["userid"].ToString() + "','Y')";

                //DataProcess.InsertQuery(ConnectionString, attach);

            }

        }

        return rowfilename;

    }

    private void Import_To_Grid(string FilePath, string Extension, string isHDR)
    {
        string conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
        conStr = String.Format(conStr, FilePath, isHDR);
        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        cmdExcel.Connection = connExcel;
        connExcel.Open();
        Session["SheetNames"] = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
    }

    private void Import_To_Grid(string FilePath, string Extension, string isHDR, string sheetName)
    {
        string conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
        conStr = String.Format(conStr, FilePath, isHDR);
        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        cmdExcel.Connection = connExcel;
        //connExcel.Open();
        //DataTable dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //connExcel.Close();
        connExcel.Open();
        cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();
        grdGetDataFromExcel.Caption = Path.GetFileName(FilePath);
        grdGetDataFromExcel.DataSource = dt;
        grdGetDataFromExcel.DataBind();
    }
    protected void AsyncFileUploadForExcel_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        if (AsyncFileUploadForExcel.HasFile)
        {
            string FileName = Path.GetFileName(AsyncFileUploadForExcel.PostedFile.FileName);
            string Extension = Path.GetExtension(AsyncFileUploadForExcel.PostedFile.FileName);
            Session["Extension"] = Extension;
            string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
            string FilePath = Server.MapPath(FolderPath + FileName);
            AsyncFileUploadForExcel.SaveAs(FilePath);
            Session["FilePath"] = FilePath;
            Import_To_Grid(FilePath, Extension, "Yes");
        }
        else
        {
            ScriptManager.RegisterStartupScript(
            this,
            GetType(),
            "MessageBox",
            "alert(' Please Import Excel File !');",
            true);
            grdGetDataFromExcel.DataSource = null;
            grdGetDataFromExcel.DataBind();
        }
    }
    protected void ddlSheetName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Import_To_Grid(Session["FilePath"].ToString(), Session["Extension"].ToString(), "Yes", Session["SelectedSheetNames"].ToString());
        Session["FilePath"] = null;
        Session["Extension"] = null;
        Session["SelectedSheetNames"] = null;
    }
    protected void btnImportData_Click(object sender, EventArgs e)
    {

        if (CheckDate(txtFromDate0.Text) == false)
        {
            MessageBox1.ShowWarning("Date is not correct. Please enter valid date");
            return;
        }

        DataTable dtimp = new DataTable(); 
        string sql = "select * from [HrmsDataImportConfigure] where Status=1";
        dtimp = DataProcess.GetData(_connectionString, sql);

        foreach (DataRow drimp in dtimp.Rows)
        {


            //if (drimp["1001"].ToString() == "1001")
            //{
                DateTime fromdate = Convert.ToDateTime(txtFromDate0.Text);

                const string tableName = "CHECKINOUT";                
                String query = "select a.*,b.SSN from [{0}] a inner join USERINFO b on b.USERID=a.USERID  where year(CHECKTIME)='" + fromdate.Year + "' and month(CHECKTIME)='" + fromdate.Month + "' and day(CHECKTIME)='" + fromdate.Day + "'";

                query = String.Format(query, tableName);

                var ds = new DataSet();
                var conn = new OleDbConnection(_connectionStringAccessAushulia);
                try
                {
                    conn.Open();
                    var da = new OleDbDataAdapter(query, conn);
                    da.Fill(ds, tableName);
                    conn.Close();

                    foreach (DataTable dataTable in ds.Tables)
                    {
                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            var storedProcedureComandTest = "exec [ImportDataFrom_CHECKINOUT] " + dataRow[9].ToString() + "," +
                                                            "'" + dataRow[1].ToString() + "'," +
                                                            "'" + dataRow[2].ToString() + "'," +
                                                            "" + dataRow[3].ToString() + "," +
                                                            "'" + dataRow[4].ToString() + "'," +
                                                            "'" + dataRow[5].ToString() + "'," +
                                                            "'" + dataRow[6].ToString() + "'," +
                                                            "'" + dataRow[7].ToString() + "'," +
                                                            "" + dataRow[8].ToString();
                            StoredProcedureExecutor.StoredProcedureExecuteNonQuery(_connectionString, storedProcedureComandTest);

                        }

                        var spcmd = " EXEC spAttendanceUpdateBySystemCompanyWiseHO 'CEL'";
                        StoredProcedureExecutor.StoredProcedureExecuteNonQuery(_connectionString, spcmd);

                    }

                    MessageBox1.ShowSuccess("Data Saved Successfully ");
                }
                catch (OleDbException exp)
                {
                    MessageBox1.ShowError("Database Error:" + exp.Message);
                }
                catch (Exception exceptionMsg)
                {
                    MessageBox1.ShowError("Error:" + exceptionMsg.Message);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
          //  }




        }
    }

    private bool CheckDate(string dtt)
    {
        try
        {
            DateTime dtr = Convert.ToDateTime(dtt);

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
       
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {

        if (CheckDate(txtFromDate0.Text) == false)
        {
            MessageBox1.ShowWarning("Date is not correct. Please enter valid date");
            return;
        }

        string selectedEmployeeCode = txtEmployeeSearch.Text == string.Empty ? null : txtEmployeeSearch.Text;

        ImportDataHeadoffice(selectedEmployeeCode);

        ImportDataBranchoffice(selectedEmployeeCode);        
       
        ImportDataBranchofficeExcel(selectedEmployeeCode);

        
        var spcmd = " EXEC spAttendanceUpdateBySystemCompanyWiseHO 'CEL'";
        StoredProcedureExecutor.StoredProcedureExecuteNonQuery(_connectionString, spcmd);
        
        //Holidy update 
        DateTime fromdate = Convert.ToDateTime(txtFromDate0.Text);
        HolidayDataEntryAllCompany(fromdate);
        //
                
        MessageBox1.ShowSuccess("Attendance Imported Successfully ");              
       
    }


    private void ImportDataBranchoffice(string empcode)
    {

        var conn = new OleDbConnection(_connectionStringAccessAushulia);
        
        
        try
        {
            conn.Open();

            DateTime fromdate = Convert.ToDateTime(txtFromDate0.Text);
            const string tableName = "CHECKINOUT";
            String query = "select a.*,b.SSN from [{0}] a inner join USERINFO b on b.USERID=a.USERID  where year(CHECKTIME)='" + fromdate.Year + "' and month(CHECKTIME)='" + fromdate.Month + "' and day(CHECKTIME)='" + fromdate.Day + "' and b.SSN='" + empcode + "'";

            query = String.Format(query, tableName);
            var ds = new DataSet();

            var da = new OleDbDataAdapter(query, conn);
            da.Fill(ds, tableName);
            conn.Close();

            foreach (DataTable dataTable in ds.Tables)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    var storedProcedureComandTest = "exec [ImportDataFrom_CHECKINOUT] " + dataRow[9].ToString() + "," +
                                                    "'" + dataRow[1].ToString() + "'," +
                                                    "'" + dataRow[2].ToString() + "'," +
                                                    "" + dataRow[3].ToString() + "," +
                                                    "'" + dataRow[4].ToString() + "'," +
                                                    "'" + dataRow[5].ToString() + "'," +
                                                    "'" + dataRow[6].ToString() + "'," +
                                                    "'" + dataRow[7].ToString() + "'," +
                                                    "" + dataRow[8].ToString();
                    StoredProcedureExecutor.StoredProcedureExecuteNonQuery(_connectionString, storedProcedureComandTest);

                }
            }            
           
        }
        catch (OleDbException exp)
        {
            MessageBox1.ShowError("Database Error:" + exp.Message);
        }
        catch (Exception exceptionMsg)
        {
            MessageBox1.ShowError("Error:" + exceptionMsg.Message);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }

    private void ImportDataHeadoffice(string empcode)
    {
        DateTime fromdate = Convert.ToDateTime(txtFromDate0.Text);
        
        if (ImportUserinfo() != "")
        {
            MessageBox1.ShowError(ImportUserinfo().ToString());
            return; 
        }
        
        string YY = fromdate.Year.ToString();
        string mm = string.Format("{0:00}", fromdate.Month);
        string dbfilename = YY + " " + mm;
        _connectionStringAccessHO = _connectionStringAccessHO.Replace("variabledb", dbfilename);
        
        var conn = new OleDbConnection(_connectionStringAccessHO);
        

        try
        {
            conn.Open();

            string deletequery = "delete from CHECKINOUT_TEMP";
            DataProcess.ExecuteQuery(_connectionString, deletequery);
            
            const string tableName = "NASTANI";
            String query = "select ID,Vreme,'',0,'1','',0,'',0 from [{0}] where year(Vreme)='" + fromdate.Year + "' and month(Vreme)='" + fromdate.Month + "' and day(Vreme)='" + fromdate.Day + "'";

            query = String.Format(query, tableName);
            var ds = new DataSet();

            var da = new OleDbDataAdapter(query, conn);
            da.Fill(ds, tableName);
            //conn.Close();

            foreach (DataTable dataTable in ds.Tables)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    var storedProcedureComandTest = "exec [ImportDataFrom_CHECKINOUT_TEMP] " + dataRow[0].ToString() + "," +
                                                    "'" + dataRow[1].ToString() + "'," +
                                                    "'" + dataRow[2].ToString() + "'," +
                                                    "" + dataRow[3].ToString() + "," +
                                                    "'" + dataRow[4].ToString() + "'," +
                                                    "'" + dataRow[5].ToString() + "'," +
                                                    "'" + dataRow[6].ToString() + "'," +
                                                    "'" + dataRow[7].ToString() + "'," +
                                                    "" + dataRow[8].ToString();
                    StoredProcedureExecutor.StoredProcedureExecuteNonQuery(_connectionString, storedProcedureComandTest);

                }
            }

            string sqlchk = @"Insert into CHECKINOUT
                                select b.EMPCODE,[CHECKTIME],[CHECKTYPE],[VERIFYCODE],[SENSORID],[Memoinfo],[WorkCode],[sn],[UserExtFmt] 
                                from CHECKINOUT_TEMP a inner join NsistemUsers b on a.userid=b.USERCARDID and b.EMPCODE='" + empcode + "'";


            DataProcess.ExecuteQuery(_connectionString, sqlchk);


        }
        catch (OleDbException exp)
        {
            MessageBox1.ShowError("Database Error:" + exp.Message);
        }
        catch (Exception exceptionMsg)
        {
            MessageBox1.ShowError("Error:" + exceptionMsg.Message);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

    }

    private void ImportDataBranchofficeExcel(string employeeCode)
    {
        try
        {
            var sheetName = "Sheet1$";

            //_connectionStringExcel = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\CEBD-Payroll-Reports\\CheckInOut.xls;Extended Properties='Excel 8.0;HDR={1}'";

            CommonMethods objCommonMethods = new CommonMethods();
            DataTable dtFromExcel = objCommonMethods.GetDataFromExcel(_connectionStringExcel, sheetName,employeeCode);
            CheckinDataController objCheckinDataController = new CheckinDataController();
            foreach (DataRow dtRow in dtFromExcel.Rows)
            {
                CheckinData objCheckInData = new CheckinData();
                objCheckInData.EmployeeCode = dtRow[1].ToString();
                if (dtRow[2].ToString() != "")
                {
                    objCheckInData.CheckinDate = Convert.ToDateTime((Convert.ToDateTime(dtRow[0]).ToString("dd-MM-yyyy") + " " + dtRow[2].ToString()));
                    objCheckinDataController.Save(_connectionString, objCheckInData);
                }
                if (dtRow[3].ToString() != "")
                {
                    objCheckInData.CheckinDate = Convert.ToDateTime((Convert.ToDateTime(dtRow[0]).ToString("dd-MM-yyyy") + " " + dtRow[3].ToString()));
                    objCheckinDataController.Save(_connectionString, objCheckInData);
                }
            }
            objCheckinDataController.AttendanceUpdate(_connectionString);
        }
        catch (Exception msgException)
        {
            MessageBox1.ShowError(msgException.Message);
        }
    }

    private string ImportUserinfo()
    {
        string msg = "";
        var connM = new OleDbConnection(_connectionStringAccessUser);
        connM.Open();

        try
        {            
            DateTime fromdate = Convert.ToDateTime(txtFromDate0.Text);
            const string tableNameM = "users";
            String queryM = "select RedBr,Name,ID,Detail1 from [{0}] ";

            queryM = String.Format(queryM, tableNameM);
            var dsM = new DataSet();
            var daM = new OleDbDataAdapter(queryM, connM);
            daM.Fill(dsM, tableNameM);

            foreach (DataTable dataTable in dsM.Tables)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    var storedProcedureComandTest = "exec [ImportDataFrom_NsistemUsers] " + dataRow[0].ToString() + "," +
                                                    "'" + dataRow[1].ToString() + "'," +
                                                    "'" + dataRow[2].ToString() + "'," +
                                                    "" + dataRow[3].ToString();
                    StoredProcedureExecutor.StoredProcedureExecuteNonQuery(_connectionString, storedProcedureComandTest);

                }
            }
            
        }
        catch (OleDbException exp)
        {
            msg="Database Error:" + exp.Message;
        }
        catch (Exception exceptionMsg)
        {
            msg="Error:" + exceptionMsg.Message;
        }
        finally
        {
            if (connM.State == ConnectionState.Open)
            {
                connM.Close();
            }
        }

        return msg;
 
    }
    protected void txtEmployeeSearch_TextChanged(object sender, EventArgs e)
    {
        if (txtEmployeeSearch.Text != string.Empty)
        {
            txtEmployeeSearch.Text = txtEmployeeSearch.Text.Split(':')[0].Trim() == "" ? "" : txtEmployeeSearch.Text.Split(':')[0].Trim();
        }
    }
}