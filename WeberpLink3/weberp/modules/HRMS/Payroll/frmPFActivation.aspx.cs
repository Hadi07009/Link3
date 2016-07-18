using CrystalDecisions.Shared;
using LibraryPAY.DsSalaryTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Payroll_frmPFActivation : System.Web.UI.Page
{
    private const string Rnode = "aa";
    string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        if (!Page.IsPostBack)
        {
            tbldet.Visible = false;
            LoadCompanyByUserPermission("ADM", Rnode);
            ClsDropDownListController.EnableDisableDropDownList(ddlcompany);
            ddlcompany_SelectedIndexChanged(sender, e);
            txtEmpId_AutoCompleteExtender.ContextKey = ConnectionString;

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
        string constr = System.Configuration.ConfigurationSettings.AppSettings["SCFConnectionString"].ToString().Replace("SCF", dbname);
        Session[GlobalData.sessionConnectionstring] = constr;
        Session["CompanyName"] = ddlcompany.SelectedItem.Text;
        Session["CompanyAddress"] = "";
        LibraryPAY.Properties.Settings.Default["SCFConnectionString"] = constr;
        LibraryPAY.Properties.Settings.Default.Save();
    }

    protected void btnInclude_Click(object sender, EventArgs e)
    {
        lblmsgPf.Text = "";
        if (txtEmpId.Text.Trim() == "")
        {
            MessageBox1.ShowWarning("Please enter employee ID correctly");
            return;
        }

        try
        {

            string empid = txtEmpId.Text.Split(':')[0].ToString();

            var storedProcedureComandTest = "exec [spPFMemberShipActivation] '" + empid.ToString() + "','1'";
            StoredProcedureExecutor.StoredProcedureExecuteNonQuery(ConnectionString, storedProcedureComandTest);
            MessageBox1.ShowWarning("PF Activation Successful");
           
        }
        catch (Exception ex)
        {
            MessageBox1.ShowWarning("Error occured. Please try again later");
        }

    }

    protected void btnDeactivePF_Click(object sender, EventArgs e)
    {
        lblmsgPf.Text = "";
        if (txtEmpId.Text.Trim() == "")
        {
            MessageBox1.ShowWarning("Please enter employee ID correctly");
            return;
        }

        try
        {
            string empid = txtEmpId.Text.Split(':')[0].ToString();
            var storedProcedureComandTest = "exec [spPFMemberShipActivation] '" + empid.ToString() + "','0'";
            StoredProcedureExecutor.StoredProcedureExecuteNonQuery(ConnectionString, storedProcedureComandTest);
            MessageBox1.ShowWarning("PF Deactivation Successful");
        }
        catch (Exception ex)
        {
            MessageBox1.ShowWarning("Error occured. Please try again later");
        }
    }
    protected void btnPFStatus_Click(object sender, EventArgs e)
    {
        GridView1.DataSource = null;

        string sql = "";
        string empid = txtEmpId.Text.Split(':')[0].ToString();
        if (empid.ToString()=="")
            sql = @"select For_Det_Empid as Empid,convert(varchar(12),Emp_Mas_Join_Date,111) as [Joining Date],EmpName,Designation,Dept,cast(For_Det_Value as decimal(18,2)) as [PF Amount],
                (case when formulaStatus=1 then 'Active' else 'Inactive' end) as [PF Status]  
                from hrms_emp_for_det a 
                inner join Emp_Details b on a.For_Det_Empid=b.EmpID
                inner join HrMs_Emp_mas c on c.Emp_Mas_Emp_Id=a.For_Det_Empid
                where For_Det_ForCode='PFEC'  and Emp_Mas_Term_Flg='N'
                order by formulaStatus,Empid";
        else
            sql = @"select For_Det_Empid as Empid,convert(varchar(12),Emp_Mas_Join_Date,111) as [Joining Date],EmpName,Designation,Dept,cast(For_Det_Value as decimal(18,2)) as [PF Amount],
                (case when formulaStatus=1 then 'Active' else 'Inactive' end) as [PF Status]  
                from hrms_emp_for_det a 
                inner join Emp_Details b on a.For_Det_Empid=b.EmpID
                inner join HrMs_Emp_mas c on c.Emp_Mas_Emp_Id=a.For_Det_Empid
                where For_Det_ForCode='PFEC'  and Emp_Mas_Term_Flg='N' and a.For_Det_Empid='" + empid.ToString() + "' order by formulaStatus,Empid";

        DataTable dt = new DataTable();
        dt = DataProcess.GetData(ConnectionString, sql);

        GridView1.DataSource = dt;
        GridView1.DataBind();

    }
}