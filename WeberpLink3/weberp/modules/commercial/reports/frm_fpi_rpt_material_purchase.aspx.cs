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

public partial class frm_fpi_rpt_material_purchase : System.Web.UI.Page
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
            loadparty();
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

    private void loadparty()
    {

        PuMa_Par_AdrTableAdapter supp = new PuMa_Par_AdrTableAdapter();
        ErpDataSet.PuMa_Par_AdrDataTable dtsupp = new ErpDataSet.PuMa_Par_AdrDataTable();
        dtsupp = supp.GetData();

        ListItem lst;
        ddlparty.Items.Clear();
        ddlparty.Items.Add("");

        foreach (ErpDataSet.PuMa_Par_AdrRow dr in dtsupp.Rows)
        {
            lst = new ListItem();
            lst.Value = dr.par_adr_code;
            lst.Text = dr.par_adr_name;
            ddlparty.Items.Add(lst);
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

        rpt.SelectionFormulla = "({tbl_fpi_approval_data.pi_date} in " + "datetime('" + fdate + "') to " + "datetime('" + tdate + "'))and {tbl_fpi_approval_data.status}='" + rdolistpitype.SelectedItem.Value  + "'";


        if (chkallparty.Checked == false)
        {
            rpt.SelectionFormulla += " and {tbl_fpi_approval_data.supp_id} = '" + ddlparty.SelectedItem.Value + "'";
        }

        
        
        if (chkallitm.Checked == false)
        {
            rpt.SelectionFormulla += " and {tbl_fpi_approval_data.mpr_itm_code} = '" + ddlitem.SelectedItem.Value + "'";
        }


        parameterpass(myParams, "companytitle", current.CompanyName);
        parameterpass(myParams, "companyaddress", current.CompanyAddress);

        title = "Raw Material Purchase (" + rdolistgroup.SelectedItem.Text + ")" +" "+ rdolisttype.SelectedItem.Text;

        parameterpass(myParams, "title", title);

       parameterpass(myParams, "period", "Period :" + fdate.ToShortDateString() + " To " + tdate.ToShortDateString());

       parameterpass(myParams, "groupby", rdolistgroup.SelectedIndex.ToString());

        rpt.ParametersFields = myParams;

        if (rdolisttype.SelectedIndex == 0)
        {
            rpt.FileName = "files/rpt_rm_purchase_detail.rpt";
        }
        else
        {
            rpt.FileName = "files/rpt_rm_purchase_summary.rpt";
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
