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

public partial class frm_fpi_rpt_lc_wise_expence : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //current.UserId = "MON";
        //current.UserName = "MON";
        //current.UserDepartment = "MON";
        //current.UserDesignation = "MON";
        //current.UserEmail = "MON";

        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";

        if (!Page.IsPostBack)
        {
            cldfrdate.SelectedDate = Convert.ToDateTime("01/01/2014");
            cldtodate.SelectedDate = DateTime.Now;
            loaditem();
            loadlcno();
           // loadparty();
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


    private void loadlcno()
    {
        get_lc_numberTableAdapter lc = new get_lc_numberTableAdapter();
        FpiDataSet.get_lc_numberDataTable dtlc = new FpiDataSet.get_lc_numberDataTable();

        ListItem lst;
        ddllcno.Items.Clear();
        ddllcno.Items.Add("");
        dtlc = lc.GetData();
        foreach (FpiDataSet.get_lc_numberRow dr in dtlc.Rows)
        {
            lst = new ListItem();
            lst.Value = dr.lc_number;
            lst.Text = dr.lc_number + ":" + dr.lc_date.ToShortDateString() + ":" + dr.quantity + ":" + dr.unit;
            ddllcno.Items.Add(lst);
        }
    }
   
    protected void btnview_Click(object sender, EventArgs e)
    {

        string title = "";
        DateTime fdate = Convert.ToDateTime(cldfrdate.SelectedDate.ToShortDateString());
        DateTime tdate = Convert.ToDateTime(cldtodate.SelectedDate.ToShortDateString());

        ParameterFields myParams = new ParameterFields();

        LibraryDTO.clsReport rpt = new LibraryDTO.clsReport();
                
        if (fdate > tdate) return;

        rpt.SelectionFormulla = "{view_lc_expence.Trn_AnaGroupLabelCode3} = 'T04' and ({view_lc_expence.Trn_DATE} in " + "datetime('" + fdate + "') to " + "datetime('" + tdate + "'))"; //  and {view_lc_expence.Trn_Ac_Code} = '" + txtcoacode.Text.Split(':')[0];
                
        if (rdoreporttype.SelectedIndex == 1)
        {
            rpt.SelectionFormulla += "and {view_lc_expence.lc_close_status} <> 'C' ";
        }
        else if (rdoreporttype.SelectedIndex == 2)
        {
            rpt.SelectionFormulla += "and {view_lc_expence.lc_close_status} = 'C' ";
        }

        rpt.SelectionFormulla += " and {view_lc_expence.Trn_Ac_Code} = '" + txtcoacode.Text.Split(':')[0] + "'";

        if (chkalllc.Checked ==false)
        {
            rpt.SelectionFormulla += " and {view_lc_expence.Trn_AnaGroupDefinationCode1} ='" + ddllcno.SelectedValue + "'";
        }

        if (chkallitm.Checked== false)
        {
            rpt.SelectionFormulla += " and {view_lc_expence.Trn_AnaGroupDefinationCode2} ='" + ddlitem.SelectedItem.Text + "'";
        }

        parameterpass(myParams, "companytitle", current.CompanyName);
        parameterpass(myParams, "companyaddress", current.CompanyAddress);

        title = "LC Wise Expence  (" + rdolisttype.SelectedItem.Text + ")" + "Report";

        parameterpass(myParams, "title", title);
        rpt.ParametersFields = myParams;
        
        if (rdolisttype.SelectedIndex == 0)
        {
            rpt.FileName = "files/rptLcExpenceDetail.rpt";
        }
        else
        {
            rpt.FileName = "files/rptLcExpenceSummary.rpt";
        }
        rpt.PageZoomFactor = 100;
        current.SessionReport = rpt;

        RegisterStartupScript("click", "<script>window.open('frm_rpt_viewer.aspx');</script>");
        //RegisterStartupScript("click", "<script>window.open('frm_rpt_viewer.aspx');</script>");

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


class Sample
{
public Sample(int x) { }

    public Sample() : this(1) { }









}




