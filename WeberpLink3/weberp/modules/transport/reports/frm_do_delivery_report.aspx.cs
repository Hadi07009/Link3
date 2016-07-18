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
using LibraryDAL.dsScfTableAdapters;
using LibraryDAL.dsTransportTableAdapters;

public partial class frm_do_delivery_report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";
        if (!Page.IsPostBack)
        {
            dtFrom.SelectedDate = DateTime.Now.AddDays(-15);
            dtTo.SelectedDate = DateTime.Now;

            load_sec();
           
          
        }
        else
        {

        }
    }


    private void load_maiparty(string  zonid)
    {

        SaMa_Par_AccTableAdapter acc = new SaMa_Par_AccTableAdapter();

        LibraryDAL.dsTransport.SaMa_Par_AccDataTable dtacc = new LibraryDAL.dsTransport.SaMa_Par_AccDataTable ();
        ListItem lst;

        ddlmainparty.Items.Clear();
        ddlmainparty.Items.Add("");

        if (zonid == "")
        {
            ddlmainparty.Items.Clear();
            return;
        
        }

        dtacc = acc.GetDataBySection(Convert.ToInt16(zonid));

        foreach (LibraryDAL.dsTransport.SaMa_Par_AccRow dr in dtacc.Rows)
        {
            lst = new ListItem();
            lst.Value = dr.Par_Acc_Code;
            lst.Text = dr.Par_Acc_Code + ":" + dr.Par_Acc_Name;
            ddlmainparty.Items.Add(lst);

        }

    }

    private void load_sec()
    {
        SaMa_Trn_IndexTableAdapter ind = new SaMa_Trn_IndexTableAdapter();
        LibraryDAL.dsScf.SaMa_Trn_IndexDataTable dtind = new LibraryDAL.dsScf.SaMa_Trn_IndexDataTable();
        ListItem lst;

        dtind = ind.GetDataByType("SS");

        lst = new ListItem();
        ddlsection.Items.Clear();
        ddlsection.Items.Add("");

        foreach (LibraryDAL.dsScf.SaMa_Trn_IndexRow dr in dtind.Rows)
        {
            lst = new ListItem();
            lst.Text = dr.Trn_Code;
            lst.Value = dr.Trn_Index.ToString();
            ddlsection.Items.Add(lst);
        }

    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        DateTime dtst = Convert.ToDateTime(dtFrom.SelectedDate.ToShortDateString());
        DateTime dtend = Convert.ToDateTime(dtTo.SelectedDate.ToShortDateString());

        LibraryDTO.clsReport rpt = new LibraryDTO.clsReport();
        ParameterFields myParams = new ParameterFields();
        string title = "", headline = "";
        if (dtst > dtend) return;

        if (chktime.Checked)
        {

            if (TimeSelectorfrom.AmPm == MKB.TimePicker.TimeSelector.AmPmSpec.AM)
            {
                if (TimeSelectorfrom.Hour == 12)
                    dtst = dtst.AddMinutes(TimeSelectorfrom.Minute);
                else
                    dtst = dtst.AddHours(TimeSelectorfrom.Hour).AddMinutes(TimeSelectorfrom.Minute);

            }
            else
            {
                if (TimeSelectorfrom.Hour == 12)
                    dtst = dtst.AddHours(12).AddMinutes(TimeSelectorfrom.Minute);
                else
                    dtst = dtst.AddHours(TimeSelectorfrom.Hour + 12).AddMinutes(TimeSelectorfrom.Minute);

            }
            if (TimeSelectorto.AmPm == MKB.TimePicker.TimeSelector.AmPmSpec.AM)
            {
                if (TimeSelectorto.Hour == 12)
                    dtend = dtend.AddMinutes(TimeSelectorto.Minute);
                else
                    dtend = dtend.AddHours(TimeSelectorto.Hour).AddMinutes(TimeSelectorto.Minute);
            }
            else
            {
                if (TimeSelectorto.Hour == 12)
                    dtend = dtend.AddHours(12).AddMinutes(TimeSelectorto.Minute);
                else
                    dtend = dtend.AddHours(TimeSelectorto.Hour + 12).AddMinutes(TimeSelectorto.Minute);
            }

        }
        else
        {
            dtend = dtend.AddDays(1);
        }

        rpt.SelectionFormulla = "";

        if (rdolistmovementstatus.SelectedIndex == 0)
        {


            rpt.SelectionFormulla = "({view_do_delivery_report.start_datetime} in " + "datetime('" + dtst + "') to " + "datetime('" + dtend + "')) and ({view_do_delivery_report.tc_status}='Y')and ({view_do_delivery_report.tc_type}='F') and  {view_do_delivery_report.trip_final_status} =7 and {view_do_delivery_report.uom}='" + rdolistcementtype.SelectedItem.Value + "'";
        }

        else
        {

            rpt.SelectionFormulla = "({view_do_delivery_report.tc_status}='Y')and ({view_do_delivery_report.tc_type}='F') and{view_do_delivery_report.trip_final_status} < 7";


        }

        if (rdolistdotype.SelectedIndex != 0)
        {

            rpt.SelectionFormulla += " and {view_do_delivery_report.Trn_Hdr_Code} = '" + rdolistdotype.SelectedItem.Value + "'";
        
        }

        if (chksection.Checked == false)
        {
            rpt.SelectionFormulla += " and {view_do_delivery_report.Par_Acc_Perm} = "+ ddlsection.SelectedItem.Value+"";

        }


        if (chkparty.Checked == false)
        {
            rpt.SelectionFormulla += " and {view_do_delivery_report.Par_Acc_Code} = '" + ddlmainparty.SelectedItem.Value + "'";

        }

        parameterpass(myParams, "companytitle", current.CompanyName);
        parameterpass(myParams, "companyaddress", current.CompanyAddress);

        title = "Party wise DO Delivery " + rdolistreporttype.SelectedItem.Text + " Report " +"("+ rdolistmovementstatus.SelectedItem.Text+")";
        parameterpass(myParams, "title", title);
        parameterpass(myParams, "headline", rdolistcementtype.SelectedItem.Text);


        parameterpass(myParams, "period", "Delivery Period :" + dtst.ToShortDateString() + " To " + dtend.ToShortDateString());

        parameterpass(myParams, "prm_grp", rdolistreporttype.SelectedItem.Value);

       rpt.ParametersFields = myParams;


        rpt.FileName = "file/rpt_do_delivery.rpt";

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
    protected void ddlsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        string zonid =ddlsection.SelectedItem.Value;
        load_maiparty(zonid);
      
    }
}
