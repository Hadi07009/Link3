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

public partial class modules_HRMS_Approval_frmLocalTourExpenseApproval : Page 
{

    string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    string _pid = "P006";
    string _fid = "6";
    private ConveyanceApplication _objConveyanceApplication;
    private ConveyanceApplicationController _objConveyanceApplicationController;
    private LocalTourExpenseClaim _objLocalTourExpenseClaim;
    private LocalTourExpenseClaimController _objLocalTourExpenseClaimController;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            clsStatic.CheckUserAuthentication(true);
            if (!Page.IsPostBack)
            {
                Session["ApplicantID"] = Session[StaticData.sessionUserId].ToString();
                Session["ActingPersonID"] = Session[StaticData.sessionUserId].ToString();
                Session["EntryUserid"] = Session[StaticData.sessionUserId].ToString();
                LoadPendingExpensesBillApplication();
                PanelForDetails.Visible = false;
            }

        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }

    private void LoadPendingExpensesBillApplication()
    {
        try
        {
            _objConveyanceApplication = new ConveyanceApplication();
            _objConveyanceApplication.ActingPersonCode = Session["ActingPersonID"].ToString();
            _objConveyanceApplication.ProcessCode = _pid;
            _objConveyanceApplication.ProcessFlowCode = _fid;
            _objConveyanceApplicationController = new ConveyanceApplicationController();
            var dtConveyanceRecord = _objConveyanceApplicationController.ShowPendingApplication(_connectionString, _objConveyanceApplication);
            grdPendingExpensesBill.DataSource = null;
            grdPendingExpensesBill.DataBind();
            if (dtConveyanceRecord.Rows.Count > 0)
            {
                grdPendingExpensesBill.DataSource = dtConveyanceRecord;
                grdPendingExpensesBill.DataBind();
            }

        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }

    private void LoadEmployeeInformation(string empid)
    {
        DataTable dt = new DataTable();
        dt = DataProcess.GetData(_connectionString, "select EmpID,EmpName,Dept,Designation,b.Emp_Mas_Join_Date as Joiningdate from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id where EmpID='" + empid + "'");
        if (dt.Rows.Count > 0)
        {
            lblId.Text = dt.Rows[0]["EmpID"].ToString();
            lblName.Text = dt.Rows[0]["EmpName"].ToString();
            lbldept.Text = dt.Rows[0]["Dept"].ToString();
            lblDesignation.Text = dt.Rows[0]["Designation"].ToString();
            lblJoiningDate.Text = dt.Rows[0]["Joiningdate"].ToString().Substring(0, 10);
        }
    }
    protected void grdPendingExpensesBill_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);

            e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(grdPendingExpensesBill, "Select$" + e.Row.RowIndex);
        }
        e.Row.Cells[5].Visible = false;

    }
    private void LoadActionPermissionForApprovalPerson(string actingpersonid, string empcode, string Processid, string flowid, int plevelid)
    {
        DataTable dt = new DataTable();

        LeaveProcess lvp = new LeaveProcess();

        dt = lvp.GetApprovalPermission(_connectionString, actingpersonid, empcode, Processid, flowid, plevelid);

        foreach (DataRow dr in dt.Rows)
        {
            if (dr["Part"].ToString() == "2")
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
    protected void grdPendingExpensesBill_SelectedIndexChanged(object sender, EventArgs e)
    {
        int indx = grdPendingExpensesBill.SelectedIndex;
        if (indx == -1)
        {
            return;
        }
        Session["referance"] = grdPendingExpensesBill.Rows[indx].Cells[1].Text;
        Session["empcode"] = grdPendingExpensesBill.Rows[indx].Cells[2].Text;
        Session["plevelid"] = grdPendingExpensesBill.Rows[indx].Cells[5].Text;

        btnForward.Visible = false;
        btnReturn.Visible = false;
        btnReject.Visible = false;
        btnApprove.Visible = false;
        PanelForDetails.Visible = true;
        LoadEmployeeInformation(Session["empcode"].ToString());
        LoadExpensesBillDetails(Session["referance"].ToString());
        LoadAdvanceReceivedData(Session["referance"].ToString());
        LoadActionPermissionForApprovalPerson(Session["ActingPersonID"].ToString(), Session["empcode"].ToString(), _pid, _fid, Convert.ToInt32(Session["plevelid"].ToString()));
    }

    private void ClearControl()
    {
        lblDateofDeparture.Text = string.Empty;
        lblDepartureTime.Text = string.Empty;
        lblDateofArrival.Text = string.Empty;
        lblArrivalTime.Text = string.Empty;
        lblFrom.Text = string.Empty;
        lblTo.Text = string.Empty;
        lblAdvanceReceived.Text = string.Empty;
        lblAdvanceReceivedDate.Text = string.Empty;
        lblActualClaimAmountBDT.Text = string.Empty;
        lblNetClaim.Text = string.Empty;
    }
    private void LoadAdvanceReceivedData(string transactionNo)
    {
        try
        {
            _objLocalTourExpenseClaim = new LocalTourExpenseClaim();
            _objLocalTourExpenseClaim.TransactionNo = transactionNo;
            _objLocalTourExpenseClaimController = new LocalTourExpenseClaimController();
            var dtAdvanceReceived = _objLocalTourExpenseClaimController.GetDetailsInformation(_connectionString,_objLocalTourExpenseClaim);
            ClearControl();
            foreach (DataRow row in dtAdvanceReceived.Rows)
            {
                lblDateofDeparture.Text = row["departureDate"].ToString();
                lblDepartureTime.Text = row["departureTime"].ToString();
                lblDateofArrival.Text = row["arrivalDate"].ToString();
                lblArrivalTime.Text = row["arrivalTime"].ToString();
                lblFrom.Text = row["fromLocation"].ToString();
                lblTo.Text = row["toLocation"].ToString();
                lblAdvanceReceived.Text = row["advancedReceivedOnBDT"].ToString();
                lblAdvanceReceivedDate.Text = row["advancedReceivedDate"].ToString();
                lblActualClaimAmountBDT.Text = row["actualClaimAmountBDT"].ToString();
                lblNetClaim.Text = row["netClaimBDT"].ToString();
            }

        }
        catch (Exception msgException)
        {
            
            throw msgException;
        }
    }

    private void LoadExpensesBillDetails(string transactionNo)
    {
        _objConveyanceApplication = new ConveyanceApplication();
        _objConveyanceApplication.TransactionNo = transactionNo;
        _objConveyanceApplication.ProcessCode = _pid;
        _objConveyanceApplication.ProcessFlowCode = _fid;
        _objConveyanceApplication.ActingPersonCode = Session["ActingPersonID"].ToString();
        _objConveyanceApplicationController = new ConveyanceApplicationController();
        var dtConveyanceDetails = _objConveyanceApplicationController.ShowPendingApplicationDetails(_connectionString, _objConveyanceApplication);
        grdLunchBillDetails.DataSource = null;
        grdLunchBillDetails.DataBind();
        if (dtConveyanceDetails.Rows.Count > 0)
        {
            grdLunchBillDetails.DataSource = dtConveyanceDetails;
            grdLunchBillDetails.DataBind();

        }

    }
    protected void grdPendingExpensesBill_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            ActionForApplicationApproval(5);
            LoadPendingExpensesBillApplication();
            PanelForDetails.Visible = false;

        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }

    private void ActionForApplicationApproval(int actionType)
    {
        try
        {
            foreach (GridViewRow rowNo in grdLunchBillDetails.Rows)
            {
                _objConveyanceApplication = new ConveyanceApplication();
                _objConveyanceApplication.ActionTypeCode = actionType;
                Label lblTransactionNo = rowNo.FindControl("lblTransactionNo") as Label;
                Label lblTransactionNoLineNo = rowNo.FindControl("lblTransactionNoLineNo") as Label;
                Label lblProcessLevelid = rowNo.FindControl("lblProcessLevelid") as Label;
                _objConveyanceApplication.TransactionNo = lblTransactionNo.Text;
                _objConveyanceApplication.TransactionNoLineNo = Convert.ToInt32(lblTransactionNoLineNo.Text);
                _objConveyanceApplication.ProcessCode = _pid;
                _objConveyanceApplication.ProcessFlowCode = _fid;
                _objConveyanceApplication.ProcessLevelCode = Convert.ToInt32( lblProcessLevelid.Text);
                _objConveyanceApplication.ActingPersonCode = Session["ActingPersonID"].ToString();
                _objConveyanceApplicationController = new ConveyanceApplicationController();
                _objConveyanceApplicationController.ActionOnApplication(_connectionString, _objConveyanceApplication);
            }

        }
        catch (Exception msgException)
        {
            
            throw msgException;
        }


    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            ActionForApplicationApproval(4);
            LoadPendingExpensesBillApplication();
            PanelForDetails.Visible = false;

        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }

    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        try
        {
            ActionForApplicationApproval(3);
            LoadPendingExpensesBillApplication();
            PanelForDetails.Visible = false;

        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }
    protected void btnForward_Click(object sender, EventArgs e)
    {
        try
        {
            ActionForApplicationApproval(2);
            LoadPendingExpensesBillApplication();
            PanelForDetails.Visible = false;

        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }
    private decimal totalAmount = 0;
    protected void grdLunchBillDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var lblAmount = ((Label)e.Row.FindControl("lblamountCost")).Text == string.Empty ? 0 : Convert.ToDecimal(((Label)e.Row.FindControl("lblamountCost")).Text);
            totalAmount += lblAmount;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            ((Label)e.Row.FindControl("lblTotalAmount")).Text = totalAmount.ToString();
        }
        e.Row.Cells[4].Visible = false;
        e.Row.Cells[5].Visible = false;
        e.Row.Cells[6].Visible = false;
        e.Row.Cells[7].Visible = false;
        e.Row.Cells[8].Visible = false;
        e.Row.Cells[9].Visible = false;
        e.Row.Cells[10].Visible = false;
        
    }
}
