using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Details_frmResignationReport : System.Web.UI.Page
{
    string ConnectionStr = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        Session["EntryUserid"] = current.UserId.Trim();
        Session["CompanyName"] = current.CompanyCode.ToString() + ":" + current.CompanyName.ToString();
        Session["CompanyAddress"] = current.CompanyAddress.ToString();
        Session[GlobalData.sessionConnectionstring] = ConnectionStr;

    }

    private string CheckValidationForJCRData()
    {
        const string checkValidation = "";
        if (txtFromDate0.Text == string.Empty)
        {
            txtFromDate0.Focus();
            return "Please Select From Date Correctly !";
        }
        if (txtToDate.Text == string.Empty)
        {
            txtToDate.Focus();
            return "Please Select To Date Correctly !";
        }
        return checkValidation;
    }

    private void LoadJoiningNdResignationEmp(string typeOfSearch)
    {
        try
        {
            string fromDate = txtFromDate0.Text.Trim();
            string toDate = txtToDate.Text.Trim();
            string sqlString = "select EmpID as EID,EmpName as [Employee Name],Dept as [Department],Designation,replace(convert(NVARCHAR,c.Emp_Mas_Join_Date, 106), ' ', '/') as [Joining Date],isnull(replace(convert(NVARCHAR,c.Emp_Mas_Confrim_Date, 106), ' ', '/'),'') as [Confirm Date],replace(convert(NVARCHAR,b.Accp_Date, 106), ' ', '/') as [Accept Date],replace(convert(NVARCHAR,b.Rel_Date, 106), ' ', '/') as [Release Date],b.Settle_Typ as [Settlement Type] from Emp_Details a inner join hrms_emp_Settlement b on a.EmpID=b.Emp_id inner join HrMs_Emp_mas c on c.Emp_Mas_Emp_Id=a.EmpID where " + typeOfSearch + " between CONVERT(datetime,'" + fromDate + "',103) and CONVERT(datetime,'" + toDate + "',103) order by EmpID";
            DataTable dataTable = Process.Run(sqlString);
            grdGetJoiningNdResignation.DataSource = null;
            grdGetJoiningNdResignation.DataBind();
            if (dataTable.Rows.Count > 0)
            {
                grdGetJoiningNdResignation.DataSource = dataTable;
                grdGetJoiningNdResignation.DataBind();
            }

        }
        catch (Exception msg)
        {

            MessageBox1.ShowError(msg.Message);
        }
    }

    private static void PrepareControlForExport(Control control)
    {
        for (int i = 0; i < control.Controls.Count; i++)
        {
            Control current = control.Controls[i];
            if (current is LinkButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
            }
            else if (current is ImageButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
            }
            else if (current is HyperLink)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
            }
            else if (current is DropDownList)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
            }
            else if (current is CheckBox)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
            }
            else if (current is Button)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as Button).Text));
            }

            if (current.HasControls())
            {
                PrepareControlForExport(current);
            }
        }
    }

    public void Export(string fileName, GridView gv)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader(
            "content-disposition", string.Format("attachment; filename={0}", fileName));
        HttpContext.Current.Response.ContentType = "application/ms-excel";

        using (System.IO.StringWriter sw = new System.IO.StringWriter())
        {
            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            {
                System.Web.UI.WebControls.Table table = new System.Web.UI.WebControls.Table();
                table.GridLines = gv.GridLines;
                if (gv.HeaderRow != null)
                {
                    PrepareControlForExport(gv.HeaderRow);
                    table.Rows.Add(gv.HeaderRow);
                }
                foreach (GridViewRow row in gv.Rows)
                {
                    PrepareControlForExport(row);
                    table.Rows.Add(row);
                }
                if (gv.FooterRow != null)
                {
                    PrepareControlForExport(gv.FooterRow);
                    table.Rows.Add(gv.FooterRow);
                }
                table.RenderControl(htw);
                HttpContext.Current.Response.Write(sw.ToString());
                HttpContext.Current.Response.End();
            }
        }
    }

    private void InsertDataForJoiningConfirmReport(string fromDate, string toDate, string userId, string sProcedure)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = sProcedure;
        cmd.Parameters.Add(new SqlParameter("@dateFrom", SqlDbType.NVarChar)).Value = fromDate.ToString();
        cmd.Parameters.Add(new SqlParameter("@dateTo", SqlDbType.NVarChar)).Value = toDate.ToString();
        cmd.Parameters.Add(new SqlParameter("@userId", SqlDbType.NVarChar)).Value = userId.ToString();
        cmd.ExecuteNonQuery();

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

    protected void btnResignation_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckValidationForJCRData();
        switch (validationMsg)
        {
            case "":
                {
                    LoadJoiningNdResignationEmp("b.Rel_Date");
                    lblStatus.Text = "Resignation Information";
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }
    }

    protected void grdGetJoiningNdResignation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Left;

    }

    protected void btnExportJoinResign_Click(object sender, EventArgs e)
    {
        if (grdGetJoiningNdResignation.Rows.Count != 0)
        {
            string type = "ResignationRecord.xls";
            Export(type, grdGetJoiningNdResignation);
        }
        else
        {
            string validationMsg = "There is no data for Export ! ";
            MessageBox1.ShowInfo(validationMsg);
        }
    }

    protected void btnResignationReport_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckValidationForJCRData();
        switch (validationMsg)
        {
            case "":
                {
                    DataProcess.DeleteQuery(ConnectionStr, "Delete FROM [Hrms_JoiningResign_Information] WHERE Userid='" + Session["EntryUserid"].ToString() + "'");
                    string fromDate = txtFromDate0.Text.ToString();
                    string toDate = txtToDate.Text.ToString();
                    InsertDataForJoiningConfirmReport(fromDate, toDate, Session["EntryUserid"].ToString(), "spProcessInsertResignHrms_JoiningResign_Information");
                    string selectionfor, parameter;
                    selectionfor = "{Hrms_JoiningResign_Information.Userid}='" + Session["EntryUserid"].ToString() + "'";
                    string CompanyName = "CompanyName" + ":" + Session["CompanyName"].ToString().Split(':')[1].ToString();
                    string CompanyAddress = "CompanyAddress" + ":" + Session["CompanyAddress"].ToString();
                    parameter = CompanyName + ";" + CompanyAddress;
                    string reportname = "../Reports/JoiningEmployeeReport.rpt";
                    ShowReport(selectionfor, parameter, reportname);
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }
    }
}