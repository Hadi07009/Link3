using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Details_frmActiveEmployeeReport : System.Web.UI.Page
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

    private string CheckValidationForGetActiveEmp()
    {
        const string checkValidation = "";
        if (txtToDateForActiveEmp.Text == string.Empty)
        {
            txtToDateForActiveEmp.Focus();
            return "Please Select To Date Correctly !";
        }
        return checkValidation;
    }

    private void LoadActiveEmployeeAccordingToDate(string toDate)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlConnection sqlConn = null;
            sqlConn = new SqlConnection(ConnectionStr);
            sqlConn.Open();
            cmd.Connection = sqlConn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spProcessGetActiveEmployeeByDate";
            cmd.Parameters.Add(new SqlParameter("@dateTo", SqlDbType.NVarChar)).Value = toDate.ToString();
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            grdGetActiveEmp.DataSource = null;
            grdGetActiveEmp.DataBind();
            if (dt.Rows.Count > 0)
            {
                grdGetActiveEmp.DataSource = dt;
                grdGetActiveEmp.DataBind();
            }

        }
        catch (Exception msg)
        {

            MessageBox1.ShowError(msg.Message);
        }
    }

    protected void btnActiveEmp_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckValidationForGetActiveEmp();
        switch (validationMsg)
        {
            case "":
                {
                    string toDate = txtToDateForActiveEmp.Text.ToString();
                    LoadActiveEmployeeAccordingToDate(toDate);
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
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

    protected void btnExportActiveEmployee_Click(object sender, EventArgs e)
    {
        if (grdGetActiveEmp.Rows.Count != 0)
        {
            string type = "ActiveEmployee.xls";
            Export(type, grdGetActiveEmp);
        }
        else
        {
            string validationMsg = "There is no data for Export ! ";
            MessageBox1.ShowInfo(validationMsg);
        }
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

    private void InsertDataForActiveEmployeeReport(string toDate, string userId, string sProcedure)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = sProcedure;
        cmd.Parameters.Add(new SqlParameter("@dateTo", SqlDbType.NVarChar)).Value = toDate.ToString();
        cmd.Parameters.Add(new SqlParameter("@userId", SqlDbType.NVarChar)).Value = userId.ToString();
        cmd.ExecuteNonQuery();

    }

    protected void btnReportForActiveEmployee_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckValidationForGetActiveEmp();
        switch (validationMsg)
        {
            case "":
                {
                    DataProcess.DeleteQuery(ConnectionStr, "Delete FROM [Hrms_ActiveEmployee_ForReport] WHERE Userid='" + Session["EntryUserid"].ToString() + "'");
                    string toDate = txtToDateForActiveEmp.Text.ToString();
                    InsertDataForActiveEmployeeReport(toDate, Session["EntryUserid"].ToString(), "spProcessGetActiveEmployeeByDateForReport");
                    string selectionfor, parameter;
                    selectionfor = "{Hrms_ActiveEmployee_ForReport.Userid}='" + Session["EntryUserid"].ToString() + "'";
                    string CompanyName = "CompanyName" + ":" + Session["CompanyName"].ToString().Split(':')[1].ToString();
                    string CompanyAddress = "CompanyAddress" + ":" + Session["CompanyAddress"].ToString();
                    parameter = CompanyName + ";" + CompanyAddress;
                    string reportname = "../Reports/ActiveEmployeeList.rpt";
                    ShowReport(selectionfor, parameter, reportname);
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }
    }

    protected void grdGetActiveEmp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
        e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
        e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;
        e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Left;
    }
}