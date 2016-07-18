using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using LibraryDAL;
using LibraryDTO;
using LibraryDAL.SCBLQryTableAdapters;
using LibraryDAL.SCBLDataSetTableAdapters;
using LibraryDAL.SCBL2DataSetTableAdapters;
using LibraryDAL.ErpDataSetTableAdapters;
using LibraryDAL.SCBLINTableAdapters;
using LibraryDAL.ProdReportDataSetTableAdapters;
using LibraryDAL.ProdDataSetTableAdapters;
using CrystalDecisions.Shared;
using LibraryDAL.FpiDataSetTableAdapters;
using LibraryDAL.AccDataSetTableAdapters;

public partial class frm_consumption_report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";
        lblmessage.Visible = false;

        if (!Page.IsPostBack)
        {
            DateTime now = DateTime.Now;
            DateTime date = new DateTime(now.Year, now.Month, 1);
            cldfrdate.SelectedDate = date;

            cldtodate.SelectedDate = DateTime.Now;
             loadCOACode();
             loaddepartment();
        }
        else
        {
        }
    }
    private void loadCOACode()
    {
        tbl_inv_dbt_codeTableAdapter code = new tbl_inv_dbt_codeTableAdapter();
        SCBLIN.tbl_inv_dbt_codeDataTable dtcode = new SCBLIN.tbl_inv_dbt_codeDataTable();

        dtcode = code.GetAllData();
        ListItem lst;
        ddlcoacode.Items.Clear();
        ddlcoacode.Items.Add("");

        foreach (SCBLIN.tbl_inv_dbt_codeRow dr in dtcode.Rows)
        {
            lst = new ListItem();
            lst.Value = dr.inv_acc_code.ToString();
            lst.Text =  dr.inv_acc_code.ToString()+":"+dr.inv_acc_name.ToString();
           ddlcoacode.Items.Add(lst);
        }

    }
    private void loaddepartment()
    {
        Hrms_Dept_MasterTableAdapter dept = new Hrms_Dept_MasterTableAdapter();
        SCBLIN.Hrms_Dept_MasterDataTable dtdept = new SCBLIN.Hrms_Dept_MasterDataTable();

        ListItem lst;
        ddldepartment .Items.Clear();
        ddldepartment.Items.Add("");
        dtdept = dept.GetAllDept();
        foreach (SCBLIN.Hrms_Dept_MasterRow dr in dtdept.Rows)
        {
            lst = new ListItem();
            lst.Value = dr.Dept_Code;
            lst.Text = dr.Dept_Name;
            ddldepartment.Items.Add(lst);
        }
    }

    protected void btnview_Click(object sender, EventArgs e)
    {
        DateTime fdate = Convert.ToDateTime(cldfrdate.SelectedDate.ToShortDateString());
        DateTime tdate = Convert.ToDateTime(cldtodate.SelectedDate.ToShortDateString());

        LibraryDTO.clsReport rpt = new LibraryDTO.clsReport();
        ParameterFields myParams = new ParameterFields();
        string title = "", title2="", durl = "";
        if (fdate > tdate) return;

        if (chkcostcenter.Checked == false)
        {
            if (txtcostcenter.Text == "")
            {
                lblmessage.Text = "Type Cost Center";
                lblmessage.Visible = true;
                return;
            }
            AccCoaGroupCodeSetupconsumptionTableAdapter acc = new AccCoaGroupCodeSetupconsumptionTableAdapter();
            SCBLIN.AccCoaGroupCodeSetupconsumptionDataTable dtacc = new SCBLIN.AccCoaGroupCodeSetupconsumptionDataTable();
            dtacc = acc.GetDataByCcode(txtcostcenter.Text.Split(':')[0]);

            if (dtacc.Rows.Count == 0)
            {
                lblmessage.Text = "Invalid Cost Center";
                lblmessage.Visible = true ;
                return;
            }
        }

        rpt.SelectionFormulla = "";
        rpt.SelectionFormulla = "({view_consumption_report.Trn_Hdr_Type}='IS' and {view_consumption_report.Trn_Hdr_HRPB_Flag}='P'  and {view_consumption_report.Trn_Hdr_DATE} in " + "datetime('" + fdate + "') to " + "datetime('" + tdate + "'))";

        if (chkcoaall.Checked == false)
        {
            rpt.SelectionFormulla += " and {view_consumption_report.T_C1} = '" + ddlcoacode.SelectedItem.Value + "'";
        }

        if (chkdeptall.Checked == false)
        {
            rpt.SelectionFormulla += " and {view_consumption_report.Sr_Hdr_Pcode} = '" + ddldepartment .SelectedItem.Value + "'";
        }

        if (chkcostcenter.Checked == false)
        {
            rpt.SelectionFormulla += " and {view_consumption_report.T_C2} = '" + txtcostcenter.Text.Split(':')[0] + "'";
        }

        title = "Item Consumption Report  " + rdolistreporttype.SelectedItem.Text+" "+ "(" + rdolistgroupby.SelectedItem.Text+")";

        parameterpass(myParams, "title", title);

      

       parameterpass(myParams, "period", "Period :" + fdate.ToShortDateString() + " To " + tdate.ToShortDateString());

       parameterpass(myParams, "groupby", rdolistgroupby.SelectedIndex.ToString());


       parameterpass(myParams, "companytitle", current.CompanyName);
       parameterpass(myParams, "companyaddress", current.CompanyAddress);

        rpt.ParametersFields = myParams;

        if (rdolistreporttype.SelectedIndex == 0)
        {
            rpt.FileName = "files/rpt_consumption_report.rpt";
        }
        else
        {
            rpt.FileName = "files/rpt_consumption_report_sum.rpt";
        }
        rpt.PageZoomFactor = 100;
        current.SessionReport = rpt;
        RegisterStartupScript("click", "<script>window.open('frm_rpt_viewer.aspx');</script>");

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

    protected void ddlloanref_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

  
}
