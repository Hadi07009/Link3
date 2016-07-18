using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Payroll_frmOthersPayment : System.Web.UI.Page
{
    readonly string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"];
    protected void Page_Load(object sender, EventArgs e)
    {
        popupFromDate.Attributes.Add("readonly", "readonly");
        popupToDate.Attributes.Add("readonly", "readonly");
        if (!Page.IsPostBack)
        {
            ClsDropDownListController.LoadCheckBoxList(_connectionString, Sqlgenerate.SqlGetOfficeLocationIntoDDL(), chkofficelocation, "Division_Master_Name", "Division_Master_Code");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            ShowOthersPayment();
        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
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

    private void ShowReport(string SCFconnStr, string selectionfor, string parameter, string reportname)
    {
        clsReport rpt = new clsReport();
        ParameterFields myParams = new ParameterFields();
        ConnectionInfo ConnInfo = new ConnectionInfo();
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
    

    private void ShowOthersPayment()
    {
        GenerateOfficeLocationView();
        OthersPayment objOthersPayment = new OthersPayment();
        objOthersPayment.FirstDate =  Convert.ToDateTime(popupFromDate.Text);
        objOthersPayment.LastDate =  Convert.ToDateTime(popupToDate.Text);

        string sql = @"CREATE VIEW viewOthersPaymentwithsalary AS
                    select EmpID,
                    sum(case when Type='OT' then oSalNumber else 0 end)  as EH,
                    sum(case when Type='OT' then oSalamAmount else 0 end)  as OT,
                    sum(case when Type='FA' then oSalamAmount else 0 end)  as FD,
                    sum(case when Type='CA' then oSalamAmount else 0 end)  as CA,
                    sum(case when Type='HA' then oSalamAmount else 0 end)  as HA,
                    sum(case when Type='TA' then oSalamAmount else 0 end)  as TA,
                    sum(case when Type='OA' then oSalamAmount else 0 end)  as OA
                    from HrmsOthersPaymentwithsalary 
                    where CONVERT(DATETIME,oSalmonth,103) between CONVERT(DATETIME, '" + objOthersPayment.FirstDate + "',103) and CONVERT(DATETIME,'" + objOthersPayment.LastDate + "',103) group by EmpID";
        DataProcess.ExecuteQuery(_connectionString, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[viewOthersPaymentwithsalary]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[viewOthersPaymentwithsalary]");
        DataProcess.ExecuteQuery(_connectionString, sql);
        string selectionfor, parameter;
        selectionfor = "";
        string CompanyName = "CompanyName" + ":" + current.CompanyName.ToString();
        string CompanyAddress = "CompanyAddress" + ":" + current.CompanyAddress.ToString();
        parameter = CompanyName + ";" + CompanyAddress ;
        string reportname = "../Reports/OthersPaymentwithsalaryReport.rpt";
        ShowReport(_connectionString, selectionfor, parameter, reportname);
    }

    private void GenerateOfficeLocationView()
    {
        string officelocation = "";

        foreach (ListItem lst in chkofficelocation.Items)
        {
            if (lst.Selected)
            {
                if (officelocation == "")
                {
                    officelocation += "'" + lst.Value.ToString() + "'" ;
                }
                else
                {
                    officelocation += ",'" + lst.Value.ToString() + "'";
                }
            }
        }

        if (officelocation == "")
        {
            foreach (ListItem lst in chkofficelocation.Items)
            {
                if (officelocation == "")
                {
                    officelocation += "'" + lst.Value.ToString() + "'";
                }
                else
                {
                    officelocation += ",'" + lst.Value.ToString() + "'";
                }
            }
        }

        string sql = @"CREATE VIEW viewOthersPaymentwithsalaryOffice AS                    
                        SELECT DISTINCT Division_Master_Code,Division_Master_Name FROM HRMS_DIVISION_MASTER WHERE T_C1 = '1' AND 
                        Division_Master_Code IN ("+officelocation+") ";
        DataProcess.ExecuteQuery(_connectionString, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[viewOthersPaymentwithsalaryOffice]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[viewOthersPaymentwithsalaryOffice]");
        DataProcess.ExecuteQuery(_connectionString, sql);
    }
}