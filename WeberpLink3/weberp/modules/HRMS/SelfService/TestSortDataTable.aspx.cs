using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

public partial class modules_HRMS_SelfService_TestSortDataTable : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            grdEmployeeRecord.DataSource = ViewState["employeeRecord"];
            grdEmployeeRecord.DataBind();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            GetDataIntoGrid();
            ClearControl();

        }
        catch (Exception msgException)
        {

            MessageBox.Show(msgException.Message);
        }
    }

    private void GetDataIntoGrid()
    {
        try
        {
            var employeeCode = txtEmployeeCode.Text == string.Empty ? null : txtEmployeeCode.Text;
            var employeeName = txtEmployeeName.Text == string.Empty ? null : txtEmployeeName.Text;

            if (btnSave.Text == "Update")
            {
                UpdateEmployeeInformation();
            }

            int slNo = 1;

            var dt = new DataTable();
            DataRow dr;
            dt.Columns.Add(new DataColumn("slNo", typeof(int)));
            dt.Columns.Add(new DataColumn("txtEmployeeCode", typeof(String)));
            dt.Columns.Add(new DataColumn("txtEmployeeName", typeof(String)));

            if (ViewState["employeeRecord"] != null)
            {
                var dtTable = (DataTable)ViewState["employeeRecord"];

                foreach (DataRow drSl in dtTable.Rows)
                {
                    int accountLevel = drSl.Field<int>("slNo");
                    slNo = Math.Max(slNo, accountLevel);
                }
                slNo = slNo + 1;

                if (btnSave.Text == "Update")
                {
                    slNo = Convert.ToInt32(Session["slNumber"].ToString());
                }

                var count = dtTable.Rows.Count;
                for (var i = 0; i < count + 1; i++)
                {
                    dt = (DataTable)ViewState["employeeRecord"];
                    if (dt.Rows.Count <= 0) continue;
                    dr = dt.NewRow();
                    dr[0] = dt.Rows[0][0].ToString();
                }
                dr = dt.NewRow();
                dr[0] = slNo;
                dr[1] = employeeCode;
                dr[2] = employeeName;

                dt.Rows.Add(dr);

            }
            else
            {
                dr = dt.NewRow();
                dr[0] = slNo;
                dr[1] = employeeCode;
                dr[2] = employeeName;

                dt.Rows.Add(dr);
            }

            dt.DefaultView.Sort = "slNo DESC";
            grdEmployeeRecord.DataSource = dt;
            grdEmployeeRecord.DataBind();
            ViewState["employeeRecord"] = dt;

        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }
    protected void grdEmployeeRecord_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            var selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());


            if (e.CommandName.Equals("Select"))
            {
                Session["indexEmployeeInformation"] = selectedIndex;
                txtEmployeeCode.Text = grdEmployeeRecord.Rows[selectedIndex].Cells[3].Text == "&nbsp;" ? string.Empty : grdEmployeeRecord.Rows[selectedIndex].Cells[3].Text;
                txtEmployeeName.Text = grdEmployeeRecord.Rows[selectedIndex].Cells[4].Text == "&nbsp;" ? string.Empty : grdEmployeeRecord.Rows[selectedIndex].Cells[4].Text;
                Session["slNumber"] = grdEmployeeRecord.Rows[selectedIndex].Cells[2].Text == "&nbsp;" ? string.Empty : grdEmployeeRecord.Rows[selectedIndex].Cells[2].Text;
                btnSave.Text = "Update";
            }

            if (!e.CommandName.Equals("Delete")) return;
            var dt = (DataTable)ViewState["employeeRecord"];

            DataTable shortedDT = new DataTable();
            DataRow[] dataRow = dt.Select("", "slNo DESC");
            if (dataRow.Length > 0)
                shortedDT = dataRow.CopyToDataTable();


            shortedDT.Rows[selectedIndex].Delete();
            shortedDT.AcceptChanges();
            shortedDT.DefaultView.Sort = "slNo DESC";
            ViewState["employeeRecord"] = null;
            grdEmployeeRecord.DataSource = null;
            grdEmployeeRecord.DataBind();
            if (shortedDT.Rows.Count > 0)
            {
                grdEmployeeRecord.DataSource = shortedDT;
                grdEmployeeRecord.DataBind();
                ViewState["employeeRecord"] = shortedDT;
            }
        }
        catch (Exception msgException)
        {

            MessageBox.Show(msgException.Message);
        }
    }
    protected void grdEmployeeRecord_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    private void ClearControl()
    {
        txtEmployeeCode.Text = string.Empty;
        txtEmployeeName.Text = string.Empty;
        btnSave.Text = "Save";
    }
    private void UpdateEmployeeInformation()
    {
        try
        {
            if ((DataTable)ViewState["employeeRecord"] != null)
            {
                var indexForDelete = Convert.ToInt32(Session["indexEmployeeInformation"].ToString());
                var dt = (DataTable)ViewState["employeeRecord"];

                DataTable shortedDT = new DataTable();
                DataRow[] dataRow = dt.Select("", "slNo DESC");
                if (dataRow.Length > 0)
                    shortedDT = dataRow.CopyToDataTable();
                                
                shortedDT.Rows[indexForDelete].Delete();
                shortedDT.AcceptChanges();
                shortedDT.DefaultView.Sort = "slNo DESC";
                ViewState["employeeRecord"] = shortedDT;
            }

        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearControl();

    }
}