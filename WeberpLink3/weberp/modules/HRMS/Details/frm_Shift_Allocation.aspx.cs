using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Details_frm_Shift_Allocation : System.Web.UI.Page
{
    private const string Rnode = "c";
    string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        if (!Page.IsPostBack)
        {
            LoadCompanyByUserPermission("ADM", Rnode);
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
        Session["ConnectionStr"] = constr;
        txtEmpId_AutoCompleteExtender.ContextKey = Session[GlobalData.sessionConnectionstring].ToString();
        LoadDepartmentId();
    }

    public void LoadDepartmentId()
    {
        DataTable dt = new DataTable();
        dt = DataProcess.GetData(Session["ConnectionStr"].ToString(), Sqlgenerate.SqlGetEmployeeDepartment());
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
        grdLoadShiftType.DataSource = DataProcess.GetData(Session["ConnectionStr"].ToString(), Sqlgenerate.SqlGetShiftType());
        grdLoadShiftType.DataBind();
    }

    public void LoadShiftType1()
    {
        grdLoadShiftType0.DataSource = DataProcess.GetData(Session["ConnectionStr"].ToString(), Sqlgenerate.SqlGetShiftType());
        grdLoadShiftType0.DataBind();
    }

    protected void btnShowEmployee_Click(object sender, EventArgs e)
    {
        if (ddlDepartmentId.SelectedIndex == -1) return;
        lblShowDept.Text = ddlDepartmentId.SelectedItem.Text;
        LabelShowDate.Text = txtFromDate.SelectedDate.ToString("dd-MMM-yyyy");
        //ClsDropDownListController.LoadCheckBoxList(Session["ConnectionStr"].ToString(), Sqlgenerate.SqlGetShiftTypeIntoDDL(), chkshift, "Shift", "Shift Code");
        GridViewShowEmployeePerDept.DataSource = DataProcess.GetData(Session["ConnectionStr"].ToString(), Sqlgenerate.SqlGetEmployeeByDepartment(ddlDepartmentId.SelectedValue));
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

                LeaveProcess lvproc = new LeaveProcess();
                string retval = lvproc.SaveShiftAllocationData(lvphdrlst, myCommand);
                if (retval == "")
                {
                    myTrans.Rollback("SaveAllTransaction");
                }
                else
                {
                    myTrans.Commit();
                }
                //LoadShiftAllocationRecord();
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
        //LoadShiftAllocationRecord();
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
            string empId = e.Row.Cells[2].Text;
            //int shiftId = Convert.ToInt32(ddlShift.SelectedValue);
            string dateAllocation = txtFromDate.SelectedDate.ToString("dd/MM/yyyy");
            CheckBox chk = (CheckBox)e.Row.FindControl("CheckRet");
            chk.Checked = true;
            chk.Enabled = false;
            CheckBoxList chkboxForShiftStatus = (CheckBoxList)e.Row.FindControl("cblForShiftSelect");
            ClsDropDownListController.LoadCheckBoxListWithOutConcate(Session["ConnectionStr"].ToString(), Sqlgenerate.SqlGetShiftTypeIntoDDL(), chkboxForShiftStatus, "Shift", "Shift Code");
            LeaveProcess lvproc = new LeaveProcess();
            DataTable applicantId = lvproc.GetAllocatedEmpId(Session["ConnectionStr"].ToString(), empId, dateAllocation);
            if (applicantId.Rows.Count != 0)
            {
                foreach (DataRow dr in applicantId.Rows)
                {
                    String SHID = dr[0].ToString();

                    for (int i = 0; i < chkboxForShiftStatus.Items.Count; i++)
                    {
                        if (chkboxForShiftStatus.Items[i].Value == SHID)
                        {
                            chkboxForShiftStatus.Items[i].Selected = true;
                            chkboxForShiftStatus.Items[i].Enabled = true;
                        }

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
        if (ddlDepartmentId.SelectedIndex == -1) return;
        lblShowEmp.Text = GetEmployeeInformation(txtEmpId.Text);
        lblShowEmpId.Text = txtEmpId.Text;
        DateTime fdate = Convert.ToDateTime(txtFromDate.SelectedDate);
        DateTime ldate = Convert.ToDateTime(txtToDate.SelectedDate);
        lblShowadt.Text = txtFromDate.SelectedDate.ToString("dd/MM/yyyy") + " TO " + txtToDate.SelectedDate.ToString("dd/MM/yyyy");
        LeaveProcess lvproc = new LeaveProcess();
        GridViewShowEmployeeIndividual.DataSource = lvproc.GetAllocatedShiftbyempid(Session["ConnectionStr"].ToString(), txtEmpId.Text, fdate, ldate);
        GridViewShowEmployeeIndividual.DataBind();
        LoadShiftType1();
        this.ModalPopupExtender2.Show();
    }

    private string GetEmployeeInformation(string empid)
    {
        string empname = "Not Found";
        DataTable dt = new DataTable();
        dt = DataProcess.GetData(Session["ConnectionStr"].ToString(), Sqlgenerate.SqlGetEmployeeName(empid));
        if (dt.Rows.Count > 0)
        {
            empname = dt.Rows[0]["EmpName"].ToString();
        }
        return empname;
    }

    private string GetEmployeeShift(string empid, string dateForShift)
    {
        string shiftCode = null;
        var storedProcedureComandRead = "exec [spShiftGetShiftCode] '" + empid + "','" + dateForShift + "'";
        DataTable dtShiftCode = StoredProcedureExecutor.StoredProcedureExecuteReader(ConnectionString, storedProcedureComandRead);
        if (dtShiftCode.Rows.Count > 0)
        {
            shiftCode = dtShiftCode.Rows[0]["Emp_Shift"].ToString();
        }
        return shiftCode;
    }

    protected void GridViewShowEmployeePerDept0_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
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
                //LoadShiftAllocationRecord();
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
            CheckBox chk = (CheckBox)e.Row.FindControl("CheckRetIndv");
            chk.Checked = true;
            chk.Enabled = false;

            CheckBoxList chkboxForShiftStatus = (CheckBoxList)e.Row.FindControl("cblForShiftSelectShiftIndv");
            ClsDropDownListController.LoadCheckBoxListWithOutConcate(Session["ConnectionStr"].ToString(), Sqlgenerate.SqlGetShiftTypeIntoDDL(), chkboxForShiftStatus, "Shift", "Shift Code");

            string shid = e.Row.Cells[4].Text;
            string shiftDate = e.Row.Cells[2].Text == "&nbsp;" ? null : e.Row.Cells[2].Text;

            if (shid == "&nbsp;")
            {
                string empId = txtEmpId.Text == string.Empty ? null : txtEmpId.Text;
                shid = GetEmployeeShift(empId, shiftDate);
            }

            for (int i = 0; i < chkboxForShiftStatus.Items.Count; i++)
            {
                if (chkboxForShiftStatus.Items[i].Value == shid)
                {
                    chkboxForShiftStatus.Items[i].Selected = true;
                    chkboxForShiftStatus.Items[i].Enabled = true;
                }

            }

        }
        e.Row.Cells[4].Visible = false;
        e.Row.Cells[5].Visible = false;
        e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
        e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
        e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
    }
    protected void txtEmpId_TextChanged(object sender, EventArgs e)
    {
        if (txtEmpId.Text != string.Empty)
        {
            txtEmpId.Text = txtEmpId.Text.Split(':')[0].Trim() == "" ? "" : txtEmpId.Text.Split(':')[0].Trim();
        }
    }
}