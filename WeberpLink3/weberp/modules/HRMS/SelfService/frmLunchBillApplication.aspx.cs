using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_SelfService_frmLunchBillApplication : System.Web.UI.Page
{
    string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    private LunchBillApplication _objLunchBillApplication;
    private LunchBillApplicationController _objLunchBillApplicationController;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            clsStatic.CheckUserAuthentication(true);
            if (!Page.IsPostBack)
            {
                LoadEmployeeInformation(Session[StaticData.sessionUserId].ToString());
                txtEmployeeSearch_AutoCompleteExtender.ContextKey = _connectionString;
                grdLunchBill.DataSource = ViewState["lunchBillRecord"];
                grdLunchBill.DataBind();
            }

        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }

    }

    private void LoadEmployeeInformation(string employeeCode)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = DataProcess.GetData(_connectionString, "select EmpID,EmpName,Dept,Designation,b.Emp_Mas_Join_Date as Joiningdate from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id where EmpID='" + employeeCode + "'");
            if (dt.Rows.Count > 0)
            {
                lblId.Text = dt.Rows[0]["EmpID"].ToString();
                lblName.Text = dt.Rows[0]["EmpName"].ToString();
                lbldept.Text = dt.Rows[0]["Dept"].ToString();
                lblDesignation.Text = dt.Rows[0]["Designation"].ToString();
                lblJoiningDate.Text = dt.Rows[0]["Joiningdate"].ToString().Substring(0, 10);
            }

        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }
    protected void txtEmployeeSearch_TextChanged(object sender, EventArgs e)
    {
        if (txtEmployeeSearch.Text != string.Empty)
        {
            txtEmployeeSearch.Text = txtEmployeeSearch.Text.Split(':')[0].Trim();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            BindLunchBillRecord();
            ClearControl();

        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }

    private void BindLunchBillRecord()
    {
        try
        {
            _objLunchBillApplication = new LunchBillApplication();
            _objLunchBillApplication.DateClaim = Convert.ToDateTime(txtDate.Text);
            _objLunchBillApplication.LocationDuringLunch = txtLocationDuringLunch.Text == string.Empty ? null : txtLocationDuringLunch.Text;
            _objLunchBillApplication.PurposeofClaim = txtPurpose.Text == string.Empty ? null : txtPurpose.Text;
            _objLunchBillApplication.AmountCost = txtAmount.Text == string.Empty ? 0 : Convert.ToDecimal(txtAmount.Text);
            _objLunchBillApplication.AssignedBy = txtEmployeeSearch.Text == string.Empty ? null : txtEmployeeSearch.Text;
            if (btnAdd.Text == "Update")
            {
                UpdateLunchBillRecord();
            }
            BindLunchBillGrid(_objLunchBillApplication);

        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }

    private void UpdateLunchBillRecord()
    {
        try
        {
            if ((DataTable)ViewState["lunchBillRecord"] != null)
            {
                var indexForDelete = Convert.ToInt32(Session["indexlunchBillRecord"].ToString());
                var dt = (DataTable)ViewState["lunchBillRecord"];
                dt.Rows[indexForDelete].Delete();
                dt.AcceptChanges();
                ViewState["lunchBillRecord"] = dt;
            }

        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }

    private void BindLunchBillGrid(LunchBillApplication objLunchBillApplication)
    {
        try
        {
            var dt = new DataTable();
            DataRow dr;
            dt.Columns.Add(new DataColumn("txtDate", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("txtLocationDuringLunch", typeof(String)));
            dt.Columns.Add(new DataColumn("txtPurpose", typeof(String)));
            dt.Columns.Add(new DataColumn("txtAmount", typeof(decimal)));
            dt.Columns.Add(new DataColumn("txtEmployeeSearch", typeof(String)));

            if (ViewState["lunchBillRecord"] != null)
            {
                var dtTable = (DataTable)ViewState["lunchBillRecord"];
                var count = dtTable.Rows.Count;
                for (var i = 0; i < count + 1; i++)
                {
                    dt = (DataTable)ViewState["lunchBillRecord"];
                    if (dt.Rows.Count <= 0) continue;
                    dr = dt.NewRow();
                    dr[0] = dt.Rows[0][0].ToString();
                }
                dr = dt.NewRow();
                dr[0] = objLunchBillApplication.DateClaim;
                dr[1] = objLunchBillApplication.LocationDuringLunch;
                dr[2] = objLunchBillApplication.PurposeofClaim;
                dr[3] = objLunchBillApplication.AmountCost;
                dr[4] = objLunchBillApplication.AssignedBy;

                dt.Rows.Add(dr);

            }
            else
            {
                dr = dt.NewRow();
                dr[0] = objLunchBillApplication.DateClaim;
                dr[1] = objLunchBillApplication.LocationDuringLunch;
                dr[2] = objLunchBillApplication.PurposeofClaim;
                dr[3] = objLunchBillApplication.AmountCost;
                dr[4] = objLunchBillApplication.AssignedBy;

                dt.Rows.Add(dr);
            }
            if (ViewState["lunchBillRecord"] != null)
            {
                grdLunchBill.DataSource = ViewState["lunchBillRecord"];
                grdLunchBill.DataBind();
            }
            else
            {
                grdLunchBill.DataSource = dt;
                grdLunchBill.DataBind();

            }
            ViewState["lunchBillRecord"] = dt;

        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }

    private void ClearControl()
    {
        txtDate.Text = string.Empty;
        txtLocationDuringLunch.Text = string.Empty;
        txtPurpose.Text = string.Empty;
        txtAmount.Text = string.Empty;
        txtEmployeeSearch.Text = string.Empty;
        btnAdd.Text = "Add";
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearControl();
        grdLunchBill.DataSource = null;
        grdLunchBill.DataBind();
        ViewState["lunchBillRecord"] = null;

    }
    protected void grdLunchBill_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());

        if (e.CommandName.Equals("Select"))
        {
            try
            {
                Session["indexlunchBillRecord"] = selectedIndex;
                var lbDate = ((Label)grdLunchBill.Rows[selectedIndex].FindControl("lbDate")).Text;
                var lblLocationDuringLunch = ((Label)grdLunchBill.Rows[selectedIndex].FindControl("lblLocationDuringLunch")).Text;
                var lblPurpose = ((Label)grdLunchBill.Rows[selectedIndex].FindControl("lblPurpose")).Text;
                var lblAmount = ((Label)grdLunchBill.Rows[selectedIndex].FindControl("lblAmount")).Text;
                var lblAssignedBy = ((Label)grdLunchBill.Rows[selectedIndex].FindControl("lblAssignedBy")).Text;

                txtDate.Text = lbDate;
                txtLocationDuringLunch.Text = lblLocationDuringLunch;
                txtPurpose.Text = lblPurpose;
                txtAmount.Text = lblAmount;
                txtEmployeeSearch.Text = lblAssignedBy;
                btnAdd.Text = "Update";

            }
            catch (Exception msgException)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "MessageBox", "alert(' " + msgException.Message + " ');", true);
            }
        }


        if (!e.CommandName.Equals("Delete")) return;
        var dt = (DataTable)ViewState["lunchBillRecord"];
        dt.Rows[selectedIndex].Delete();
        dt.AcceptChanges();
        ViewState["lunchBillRecord"] = dt;
        if (ViewState["lunchBillRecord"] == null) return;
        grdLunchBill.DataSource = ViewState["lunchBillRecord"];
        grdLunchBill.DataBind();
    }
    protected void grdLunchBill_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btnApplyLunchBill_Click(object sender, EventArgs e)
    {
        try
        {
            ApplyForLunchBill();
            ClearControl();
            grdLunchBill.DataSource = null;
            grdLunchBill.DataBind();
            ViewState["lunchBillRecord"] = null;

        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }

    private void ApplyForLunchBill()
    {
        try
        {
            LeaveProcess lvp = new LeaveProcess();
            string transactionNo = lvp.GetTransactionNo(_connectionString);
            foreach (GridViewRow dtRow in grdLunchBill.Rows)
            {
                _objLunchBillApplication = new LunchBillApplication();
                Label lblSl = dtRow.FindControl("lblSl") as Label;
                Label lbDate = dtRow.FindControl("lbDate") as Label;
                Label lblLocationDuringLunch = dtRow.FindControl("lblLocationDuringLunch") as Label;
                Label lblPurpose = dtRow.FindControl("lblPurpose") as Label;
                Label lblAmount = dtRow.FindControl("lblAmount") as Label;
                Label lblAssignedBy = dtRow.FindControl("lblAssignedBy") as Label;

                _objLunchBillApplication.TransactionNoLineNo = Convert.ToInt32(lblSl.Text);
                _objLunchBillApplication.DateClaim = Convert.ToDateTime(lbDate.Text);
                _objLunchBillApplication.LocationDuringLunch = lblLocationDuringLunch.Text == string.Empty ? null : lblLocationDuringLunch.Text;
                _objLunchBillApplication.PurposeofClaim = lblPurpose.Text == string.Empty ? null : lblPurpose.Text;
                _objLunchBillApplication.AmountCost = lblAmount.Text == string.Empty ? 0 : Convert.ToDecimal(lblAmount.Text);
                _objLunchBillApplication.AssignedBy = lblAssignedBy.Text == string.Empty ? null : lblAssignedBy.Text;
                _objLunchBillApplication.EntryUser = current.UserId;
                _objLunchBillApplication.ApplicantCode = lblId.Text;
                _objLunchBillApplication.TransactionNo = transactionNo;
                _objLunchBillApplication.ProcessCode = "P004";
                _objLunchBillApplication.ProcessFlowCode = "4";
                _objLunchBillApplication.ProcessLevelCode = 7;
                _objLunchBillApplication.ProcessTypeCode = "LB";
                _objLunchBillApplicationController = new LunchBillApplicationController();
                _objLunchBillApplicationController.ApplyForBill(_connectionString, _objLunchBillApplication);
            }

        }
        catch (Exception msgException)
        {
            
            throw msgException;
        }
    }
}