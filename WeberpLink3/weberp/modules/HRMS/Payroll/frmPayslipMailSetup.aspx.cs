using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Payroll_frmPayslipMailSetup : System.Web.UI.Page
{
    private const string Rnode = "aa";
    string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        txtSalaryDate.Attributes.Add("readonly", "readonly");
        if (!Page.IsPostBack)
        {
            tbldet.Visible = false;
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
        tbldet.Visible = false;
        if (ddlcompany.Text == "") return;
        tbldet.Visible = true;
        string dbname = ddlcompany.SelectedItem.Value.ToString();
        //string constr = System.Configuration.ConfigurationSettings.AppSettings["SCFConnectionString"].ToString().Replace("SCF", dbname);
        Session[GlobalData.sessionConnectionstring] = ConnectionString;
        Session["CompanyName"] = ddlcompany.SelectedItem.Text;
        Session["CompanyAddress"] = "";
        LibraryPAY.Properties.Settings.Default["SCFConnectionString"] = ConnectionString;
        LibraryPAY.Properties.Settings.Default.Save();
        LoadofficeLocation();

        txtEmpId_AutoCompleteExtender.ContextKey = Session[GlobalData.sessionConnectionstring].ToString();
    }

    private void LoadofficeLocation()
    {        
           
    }

    private int GetTotalDays(DateTime dtst, DateTime dtend)
    {
        int totdays = 0;
        DateTime dt1 = Convert.ToDateTime(dtst);
        DateTime dt2 = Convert.ToDateTime(dtend);
        TimeSpan ts = dt2 - dt1;
        totdays = ts.Days + 1;
        return totdays;
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
    
   
    
    protected void btnSalreportPosted_Click(object sender, EventArgs e)
    {

        if (ddlcompany.Text == "")
        {
            lblmsg.Text = "Company Selection Error";
            lblmsg.Visible = true;
            return;
        }

        if (PeriodSelectionValidation() == false)
        {
            lblmsg.Text = "Period Selection Error";
            lblmsg.Visible = true;
            return;
        }
      
        string employeeid="";
        string[] emp=txtEmpId.Text.Split(':');

        if (emp.Length < 2)
            employeeid="";
        else
            employeeid = emp[0].ToString();


        int mm = Convert.ToDateTime(txtSalaryDate.Text).Month;
        int yy = Convert.ToDateTime(txtSalaryDate.Text).Year;
        string companycode = ddlcompany.SelectedItem.Value.ToString();
        string ConnectionStr = "";
        string sql = "";


        if (rblForPay.SelectedValue == "S")
        {
            if (employeeid == "")
            {
                sql = "insert into tbl_email_notification select 'Payslip','" + companycode + "',"
                           + " (select CompanyName from Hrms_Company_Master where CompanyId='" + companycode + "') as CompanyName,"
                           + " (select Address1+SPACE(1)+Address2+SPACE(1)+Address3+SPACE(1)+Address4 from Hrms_Company_Master where CompanyId='" + companycode + "') as CompanyAddress,"
                           + " b.EmpID,b.EmpName,b.deptid," + mm + " as salmonth," + yy + " as salyear,1 as Status,null,isnull(a.Emp_Mas_Remarks,PaySlip_Email) as Emp_Mas_Remarks,a.Emp_Mas_HandSet,'" + ConnectionStr + "',isnull(e.Bnk_info_Bnk_Name,'') as Bnk_Code,isnull(e.Bnk_info_Branch_name,'') as Brc_Code ,isnull(Acc_No,'') as Acc_No,0,'',0"
                           + " from HrMs_Emp_Mas a"
                           + " inner join Emp_Details b on a.Emp_Mas_Emp_Id=b.EmpID"
                           + " left outer join Hrms_Emp_Bnk_Info c on c.Emp_ID=b.EmpID"
                           + " left outer join HrMs_PaySlip_Email d on PaySlip_Dept_Code=b.deptid"
                           + " left outer join FA_BNK_INFO e on e.Bnk_info_Branch_Code=c.Brc_Code and c.Bnk_Code=e.Bnk_info_Bnk_code"
                           + " where Emp_Mas_Term_Flg<>'Y' and Emp_Mas_Emp_Id in(select Empcode from hrms_salary where MONTH(Salmonth)='" + mm + "' and YEAR(Salmonth)='" + yy + "' and SalGrade<>'50') order by b.EmpID";

            }
            else
            {
                sql = "insert into tbl_email_notification select 'Payslip','" + companycode + "',"
                          + " (select CompanyName from Hrms_Company_Master where CompanyId='" + companycode + "') as CompanyName,"
                          + " (select Address1+SPACE(1)+Address2+SPACE(1)+Address3+SPACE(1)+Address4 from Hrms_Company_Master where CompanyId='" + companycode + "') as CompanyAddress,"
                          + " b.EmpID,b.EmpName,b.deptid," + mm + " as salmonth," + yy + " as salyear,1 as Status,null,isnull(a.Emp_Mas_Remarks,PaySlip_Email) as Emp_Mas_Remarks,a.Emp_Mas_HandSet,'" + ConnectionStr + "',isnull(e.Bnk_info_Bnk_Name,'') as Bnk_Code,isnull(e.Bnk_info_Branch_name,'') as Brc_Code ,isnull(Acc_No,'') as Acc_No,0,'',0"
                          + " from HrMs_Emp_Mas a"
                          + " inner join Emp_Details b on a.Emp_Mas_Emp_Id=b.EmpID"
                          + " left outer join Hrms_Emp_Bnk_Info c on c.Emp_ID=b.EmpID"
                          + " left outer join HrMs_PaySlip_Email d on PaySlip_Dept_Code=b.deptid"
                          + " left outer join FA_BNK_INFO e on e.Bnk_info_Branch_Code=c.Brc_Code and c.Bnk_Code=e.Bnk_info_Bnk_code"
                          + " where Emp_Mas_Term_Flg<>'Y' and Emp_Mas_Emp_Id in(select Empcode from hrms_salary where MONTH(Salmonth)='" + mm + "' and YEAR(Salmonth)='" + yy + "' and Empcode='" + employeeid.ToString() + "' and SalGrade<>'50')  order by b.EmpID";

            }

        }
        else
        {
            if (employeeid == "")
            {
                sql = "insert into tbl_email_notification select 'Payslip','" + companycode + "',"
                           + " (select CompanyName from Hrms_Company_Master where CompanyId='" + companycode + "') as CompanyName,"
                           + " (select Address1+SPACE(1)+Address2+SPACE(1)+Address3+SPACE(1)+Address4 from Hrms_Company_Master where CompanyId='" + companycode + "') as CompanyAddress,"
                           + " b.EmpID,b.EmpName,b.deptid," + mm + " as salmonth," + yy + " as salyear,1 as Status,null,isnull(a.Emp_Mas_Remarks,PaySlip_Email) as Emp_Mas_Remarks,a.Emp_Mas_HandSet,'" + ConnectionStr + "',isnull(e.Bnk_info_Bnk_Name,'') as Bnk_Code,isnull(e.Bnk_info_Branch_name,'') as Brc_Code ,isnull(Acc_No,'') as Acc_No,0,'',50"
                           + " from HrMs_Emp_Mas a"
                           + " inner join Emp_Details b on a.Emp_Mas_Emp_Id=b.EmpID"
                           + " left outer join Hrms_Emp_Bnk_Info c on c.Emp_ID=b.EmpID"
                           + " left outer join HrMs_PaySlip_Email d on PaySlip_Dept_Code=b.deptid"
                           + " left outer join FA_BNK_INFO e on e.Bnk_info_Branch_Code=c.Brc_Code and c.Bnk_Code=e.Bnk_info_Bnk_code"
                           + " where Emp_Mas_Term_Flg<>'Y' and Emp_Mas_Emp_Id in(select Empcode from hrms_salary where MONTH(Salmonth)='" + mm + "' and YEAR(Salmonth)='" + yy + "' and SalGrade='50') order by b.EmpID";

            }
            else
            {
                sql = "insert into tbl_email_notification select 'Payslip','" + companycode + "',"
                          + " (select CompanyName from Hrms_Company_Master where CompanyId='" + companycode + "') as CompanyName,"
                          + " (select Address1+SPACE(1)+Address2+SPACE(1)+Address3+SPACE(1)+Address4 from Hrms_Company_Master where CompanyId='" + companycode + "') as CompanyAddress,"
                          + " b.EmpID,b.EmpName,b.deptid," + mm + " as salmonth," + yy + " as salyear,1 as Status,null,isnull(a.Emp_Mas_Remarks,PaySlip_Email) as Emp_Mas_Remarks,a.Emp_Mas_HandSet,'" + ConnectionStr + "',isnull(e.Bnk_info_Bnk_Name,'') as Bnk_Code,isnull(e.Bnk_info_Branch_name,'') as Brc_Code ,isnull(Acc_No,'') as Acc_No,0,'',50"
                          + " from HrMs_Emp_Mas a"
                          + " inner join Emp_Details b on a.Emp_Mas_Emp_Id=b.EmpID"
                          + " left outer join Hrms_Emp_Bnk_Info c on c.Emp_ID=b.EmpID"
                          + " left outer join HrMs_PaySlip_Email d on PaySlip_Dept_Code=b.deptid"
                          + " left outer join FA_BNK_INFO e on e.Bnk_info_Branch_Code=c.Brc_Code and c.Bnk_Code=e.Bnk_info_Bnk_code"
                          + " where Emp_Mas_Term_Flg<>'Y' and Emp_Mas_Emp_Id in(select Empcode from hrms_salary where MONTH(Salmonth)='" + mm + "' and YEAR(Salmonth)='" + yy + "' and Empcode='" + employeeid.ToString() + "' and SalGrade='50')  order by b.EmpID";

            }
 
        }
                
        DataProcess.ExecuteQuery(ConnectionString, sql);

        lblmsg.Text = "Data has been set to send email. Server will send email within fiew minutes";
        
    }

    private bool PeriodSelectionValidation()
    {
        int mm = 0, yyyy = 0;
        string[] tmp = txtSalaryDate.Text.Split('/');
        if (tmp.Length < 2) return false;
        try
        {
            mm = Convert.ToInt32(tmp[0]);
            yyyy = Convert.ToInt32(tmp[1]);
            return true;
        }
        catch
        {
            return false;
        }

    }
}