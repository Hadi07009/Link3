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
                    slNo = Math.Max(slNo, accountLevel) + 1;
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
}