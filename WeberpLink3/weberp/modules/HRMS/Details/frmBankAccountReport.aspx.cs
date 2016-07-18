using ADODB;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Details_frmBankAccountReport : System.Web.UI.Page
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

    private void LoadBankAccount()
    {
        try
        {
            string sqlString = "";
            if (BankAccountRadioButtonList.SelectedValue == "1")
            {
                sqlString = "select Emp_ID,emp_mas_first_name+space(1)+emp_mas_last_name as [Name],[Bnk_info_Bnk_Name] as [Bank Name],[Bnk_info_Branch_name] as [Branch],Acc_No from [Hrms_Emp_Bnk_Info] a  inner join [FA_BNK_INFO] b on a.[Brc_Code]=b.[Bnk_info_Branch_Code] and a.bnk_code=b.[Bnk_info_Bnk_code] inner join hrms_emp_mas c on c.emp_mas_emp_id=a.Emp_Id and emp_mas_term_flg<>'Y' order by a.Emp_ID";

            }
            else
            {
                sqlString = "select Emp_ID,emp_mas_first_name+space(1)+emp_mas_last_name as [Name],[Bnk_info_Bnk_Name] as [Bank Name],[Bnk_info_Branch_name] as [Branch],Acc_No from [Hrms_Emp_Bnk_Info] a  inner join [FA_BNK_INFO] b on a.[Brc_Code]=b.[Bnk_info_Branch_Code] and a.bnk_code=b.[Bnk_info_Bnk_code] inner join hrms_emp_mas c on c.emp_mas_emp_id=a.Emp_Id order by a.Emp_ID";

            }
            DataTable dataTable = DataProcess.GetData(ConnectionStr, sqlString);
            grdGetBankAccount.DataSource = null;
            grdGetBankAccount.DataBind();
            if (dataTable.Rows.Count > 0)
            {
                grdGetBankAccount.DataSource = dataTable;
                grdGetBankAccount.DataBind();
            }
        }
        catch (Exception msg)
        {

            ScriptManager.RegisterStartupScript(
                     this,
                     GetType(),
                     "MessageBox",
                     "alert(' " + msg + " ');",
                     true);
        }
    }

    protected void btnGetBankAccount_Click(object sender, EventArgs e)
    {
        LoadBankAccount();
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

    protected void btnExportBankAccount_Click(object sender, EventArgs e)
    {
        if (grdGetBankAccount.Rows.Count != 0)
        {
            string type = "BankAccount.xls";
            Export(type, grdGetBankAccount);
        }
        else
        {
            string validationMsg = "There is no data for Export ! ";
            ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + validationMsg + "');",
                    true);
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

    private void InsertBankAccountForReport()
    {
        string sqlString = "";
        if (BankAccountRadioButtonList.SelectedValue == "1")
        {
            sqlString = "insert into Hrms_ForBankAccount_Information  select Emp_ID,emp_mas_first_name+space(1)+emp_mas_last_name as [Name],[Bnk_info_Bnk_Name] as [Bank Name],[Bnk_info_Branch_name] as [Branch],Acc_No,'" + Session["EntryUserid"].ToString() + "' from [Hrms_Emp_Bnk_Info] a  inner join [FA_BNK_INFO] b on a.[Brc_Code]=b.[Bnk_info_Branch_Code] and a.bnk_code=b.[Bnk_info_Bnk_code] inner join hrms_emp_mas c on c.emp_mas_emp_id=a.Emp_Id and emp_mas_term_flg<>'Y' order by a.Emp_ID";

        }
        else
        {
            sqlString = "insert into Hrms_ForBankAccount_Information  select Emp_ID,emp_mas_first_name+space(1)+emp_mas_last_name as [Name],[Bnk_info_Bnk_Name] as [Bank Name],[Bnk_info_Branch_name] as [Branch],Acc_No,'" + Session["EntryUserid"].ToString() + "' from [Hrms_Emp_Bnk_Info] a  inner join [FA_BNK_INFO] b on a.[Brc_Code]=b.[Bnk_info_Branch_Code] and a.bnk_code=b.[Bnk_info_Bnk_code] inner join hrms_emp_mas c on c.emp_mas_emp_id=a.Emp_Id order by a.Emp_ID";

        }
        DataProcess.InsertQuery(ConnectionStr, sqlString);

    }

    protected void btnBankAccountReport_Click(object sender, EventArgs e)
    {
        DataProcess.DeleteQuery(ConnectionStr, "Delete FROM [Hrms_ForBankAccount_Information] WHERE Userid='" + Session["EntryUserid"].ToString() + "'");
        InsertBankAccountForReport();
        string selectionfor, parameter;
        selectionfor = "{Hrms_ForBankAccount_Information.Userid}='" + Session["EntryUserid"].ToString() + "'";
        string CompanyName = "CompanyName" + ":" + Session["CompanyName"].ToString().Split(':')[1].ToString();
        string CompanyAddress = "CompanyAddress" + ":" + Session["CompanyAddress"].ToString();
        parameter = CompanyName + ";" + CompanyAddress;
        string reportname = "../Reports/BankAccountInformation.rpt";
        ShowReport(selectionfor, parameter, reportname);
    }


    protected void grdGetBankAccount_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdGetBankAccount.EditIndex = -1;
        LoadBankAccount();
    }
    protected void grdGetBankAccount_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Left;
    }
    protected void grdGetBankAccount_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdGetBankAccount.EditIndex = e.NewEditIndex;
        LoadBankAccount();
    }

    private void UpdateAccountNo()
    {
        try
        {
            string empId = (grdGetBankAccount.Rows[grdGetBankAccount.EditIndex].FindControl("Label5") as Label).Text;
            string accountNo = (grdGetBankAccount.Rows[grdGetBankAccount.EditIndex].FindControl("txtAccountNo") as TextBox).Text;
            DataProcess.UpdateQuery(ConnectionStr, "UPDATE [Hrms_Emp_Bnk_Info]SET Acc_No ='" + accountNo + "' WHERE Emp_ID = '" + empId + "'");

        }
        catch (Exception msg)
        {

            ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + msg + " ');",
                    true);
        }
    }

    protected void grdGetBankAccount_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        UpdateAccountNo();
        grdGetBankAccount.EditIndex = -1;
        LoadBankAccount();
    }
}