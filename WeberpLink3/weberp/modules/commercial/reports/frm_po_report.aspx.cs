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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using LibraryDAL;
using LibraryDTO;
using LibraryDAL.SCBLDataSetTableAdapters;

public partial class frm_po_report : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {

        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";
        
        if (!Page.IsPostBack)
        {
            load_mpr();

        }
        else
        {

        }

    }


    private void load_mpr()
    {
        chkCode.Items.Clear();
        chkCode.Items.Add("RTMPR");
    }


    private void show_report(string potype, string status, string code)
    {
        clsReport rpt = new clsReport();      
        ConnectionInfo ConnInfo = new ConnectionInfo();
        string SCBLconnStr = System.Configuration.ConfigurationManager.AppSettings["SCBLConnectionString"].ToString();
        string[] ff;
        string[] ss;

        string[] frd = new string[4];
        string[] tod = new string[4];
        string DateFr, DateTo, selectionFormulla;
        ParameterFields myParams = new ParameterFields();

        frd[0] = cldfrom.SelectedDate.Day.ToString();
        frd[1] = cldfrom.SelectedDate.Month.ToString();
        frd[2] = cldfrom.SelectedDate.Year.ToString();

        tod[0] = cldto.SelectedDate.Day.ToString();
        tod[1] = cldto.SelectedDate.Month.ToString();
        tod[2] = cldto.SelectedDate.Year.ToString();

        DateFr = "date(" + frd[2] + "," + frd[1] + "," + frd[0] + ")";
        DateTo = "date(" + tod[2] + "," + tod[1] + "," + tod[0] + ")";



        ff = SCBLconnStr.Split('=');

        ss = ff[1].Split(';');
        ConnInfo.ServerName = ss[0];

        ss = ff[2].Split(';');
        ConnInfo.DatabaseName = ss[0];

        ss = ff[3].Split(';');
        ConnInfo.UserID = ss[0];

        ss = ff[4].Split(';');
        ConnInfo.Password = ss[0];

      
        parameterpass(myParams,"POType", potype);
        if (ChkAllCode.Checked)
            parameterpass(myParams, "Code", "ALL");
        else
            parameterpass(myParams, "Code", code);

        if (ChkAllStatus.Checked)
            parameterpass(myParams, "Status", "ALL");
        else
            parameterpass(myParams, "Status", status);

        parameterpass(myParams, "DateFrom", cldfrom.VisibleDate.ToShortDateString());
        parameterpass(myParams, "DateTo", cldto.VisibleDate.ToShortDateString());

        parameterpass(myParams, "companytitle", current.CompanyName);
        parameterpass(myParams, "companyaddress", current.CompanyAddress);


        selectionFormulla = "Left ({PuTr_PO_Hdr.PO_Hdr_Ref},1 ) in " + potype + " and {putr_po_hdr.po_hdr_code} in " + code + " and {putr_po_hdr.po_hdr_status} in " + status + " and ({putr_po_hdr.po_hdr_date} in " + DateFr + " to " + DateTo + ")";


        rpt.FileName = "./files/rptPO_details.rpt"; 
        rpt.ConnectionInfo = ConnInfo;
        rpt.ParametersFields = myParams;
        rpt.SelectionFormulla = selectionFormulla;
        //current.SessionReport  = rpt;
        
        RegisterStartupScript("click", "<script>window.open('./frm_report_viewer.aspx');</script>");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string potype = "[";
        string status = "[";
        string code = "[";


        foreach (ListItem lst in ChkStatus.Items)
        {
            if (lst.Selected)
                status = status + "'" + lst.Value.ToString() + "',";
        }
        status = status.Remove(status.Length - 1, 1) + "]";

        foreach (ListItem lst in chkCode.Items)
        {
            if (lst.Selected)
                code = code + "'" + lst.Value.ToString() + "',";
        }
        code = code.Remove(code.Length - 1, 1) + "]";


        foreach (ListItem lst in ChkPOType.Items)
        {
            if (lst.Selected)
                potype = potype + "'" + lst.Value.ToString().Substring(0,1) + "',";
        }
        potype = potype.Remove(potype.Length - 1, 1) + "]";


        if (ChkStatus.SelectedIndex != -1 & ChkPOType.SelectedIndex != -1 & chkCode.SelectedIndex != -1)
            show_report(potype, status, code);

    }


    protected void ChkAllStatus_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkAllStatus.Checked)
        {
            foreach (ListItem lst in ChkStatus.Items)
            {
                lst.Selected = true;
            }
        }

        else
        {
            foreach (ListItem lst in ChkStatus.Items)
            {
                lst.Selected = false;
            }

        }
    }


    protected void ChkAllCode_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkAllCode.Checked)
        {
            foreach (ListItem lst in chkCode.Items)
            {
                lst.Selected = true;
            }
        }

        else
        {
            foreach (ListItem lst in chkCode.Items)
            {
                lst.Selected = false;
            }

        }
    }

    private void parameterpass(ParameterFields myParams,string pname, string value)
    {
        ParameterField param = new ParameterField();
        ParameterDiscreteValue dis1 = new ParameterDiscreteValue();

        param.ParameterFieldName = pname;
        dis1.Value = value;
        param.CurrentValues.Add(dis1);
        myParams.Add(param);

    }

}