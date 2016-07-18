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

public partial class frm_fpi_rpt_custom_clearing_info_det : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";

        if (!Page.IsPostBack)
        {
            cldfrdate.SelectedDate = Convert.ToDateTime("01/01/2014");
            cldtodate.SelectedDate = DateTime.Now;
            loaditem();
            loadlcno();
          
        }
        else
        {

        }
    }
    private void loaditem()
    {

        InMa_Itm_DetTableAdapter itm = new InMa_Itm_DetTableAdapter();
        ErpDataSet.InMa_Itm_DetDataTable dtitm = new ErpDataSet.InMa_Itm_DetDataTable();

        dtitm = itm.GetDataByItmType("R");
        ListItem lst;
        ddlitem.Items.Clear();
        ddlitem.Items.Add("");

        foreach (LibraryDAL.ErpDataSet.InMa_Itm_DetRow dr in dtitm.Rows)
        {
            lst = new ListItem();
            lst.Value = dr.Itm_Det_Sec_Code;
            lst.Text = dr.Itm_Det_desc;
            ddlitem.Items.Add(lst);
        }

    }

    //private void loadparty()
    //{
    //    AccCoaGroupCodeSetupTableAdapter party = new AccCoaGroupCodeSetupTableAdapter();
    //    LibraryDAL.AccDataSet.AccCoaGroupCodeSetupDataTable dtparty = new AccDataSet.AccCoaGroupCodeSetupDataTable();
    //    ListItem lst;
    //    ddlparty.Items.Clear();
    //    ddlparty.Items.Add("");
    //    dtparty = party.GetDataByForLoanParty("T05", 1);
    //    foreach (AccDataSet.AccCoaGroupCodeSetupRow dr in dtparty.Rows)
    //    {
    //        lst = new ListItem();
    //        lst.Value = dr.Ccg_Code;
    //        lst.Text = dr.Ccg_Name;
    //        ddlparty.Items.Add(lst);
    //    }
    //}

    private void loadlcno()
    {
        get_lc_numberTableAdapter lc = new get_lc_numberTableAdapter();
        FpiDataSet.get_lc_numberDataTable dtlc = new FpiDataSet.get_lc_numberDataTable();

        ListItem lst;
        ddllcrefno.Items.Clear();
        ddllcrefno.Items.Add("");
        dtlc = lc.GetData();
        foreach (FpiDataSet.get_lc_numberRow dr in dtlc.Rows)
        {
            lst = new ListItem();
            lst.Value = dr.lc_number;
            lst.Text = dr.lc_number+":"+dr.lc_date.ToShortDateString()+":"+dr.quantity+":"+dr.unit;
            ddllcrefno.Items.Add(lst);
        }
    }
   
    protected void btnview_Click(object sender, EventArgs e)
    {
        DateTime fdate = Convert.ToDateTime(cldfrdate.SelectedDate.ToShortDateString());
        DateTime tdate = Convert.ToDateTime(cldtodate.SelectedDate.ToShortDateString());

        LibraryDTO.clsReport rpt = new LibraryDTO.clsReport();
        ParameterFields myParams = new ParameterFields();
        string title = "" , durl ="";
        if (fdate > tdate) return;

        rpt.SelectionFormulla = "";
        //{tbl_fpi_lc_info.lc_close_status}

        if (rdolistlcstatus.SelectedIndex == 0)
        {
            rpt.SelectionFormulla = "({tbl_fpi_custom_clearing.lc_date} in " + "datetime('" + fdate + "') to " + "datetime('" + tdate + "'))";
        }

        else  if (rdolistlcstatus.SelectedIndex == 1)
        {          
            rpt.SelectionFormulla = "({tbl_fpi_custom_clearing.lc_date} in " + "datetime('" + fdate + "') to " + "datetime('" + tdate + "'))  and ({tbl_fpi_lc_info.lc_close_status}<>'Y')";
        }

        else 
        {
            rpt.SelectionFormulla = "({tbl_fpi_custom_clearing.lc_date} in " + "datetime('" + fdate + "') to " + "datetime('" + tdate + "'))  and ({tbl_fpi_lc_info.lc_close_status}='Y')";
        }

        if (chkallitm.Checked == false)
        {
            rpt.SelectionFormulla += " and {tbl_fpi_custom_clearing.mpr_itm_code} = '" + ddlitem.SelectedItem.Value + "'";
        }

        if (chkalllc.Checked == false)
        {
            rpt.SelectionFormulla += " and {tbl_fpi_custom_clearing.lc_number} = '" + ddllcrefno.SelectedItem.Value + "'";
        }


        parameterpass(myParams, "companytitle", current.CompanyName);
        parameterpass(myParams, "companyaddress", current.CompanyAddress);

        title = "Custom Clearing Information " + rdolisttype.SelectedItem.Text+" "+ "(" + rdolistlcstatus.SelectedItem.Text + " " + " LC" + ")";

      

        parameterpass(myParams, "title", title);

       parameterpass(myParams, "period", "Period :" + fdate.ToShortDateString() + " To " + tdate.ToShortDateString());

        rpt.ParametersFields = myParams;

        if (rdolisttype.SelectedIndex == 0)
        {
            rpt.FileName = "files/rpt_custom_clearing.rpt";
        }
        else
        {
            rpt.FileName = "files/rpt_custom_clearing_summery.rpt";
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
