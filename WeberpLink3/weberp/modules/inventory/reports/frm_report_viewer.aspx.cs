﻿using System;
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
using LibraryDTO;

public partial class ClientSide_modules_commercial_reports_frm_report_viewer : System.Web.UI.Page
{
    ReportDocument O_Report = new ReportDocument();

    protected void Page_Init(object sender, EventArgs e)
    {        
        clsStatic.CheckUserAuthentication();

        if (!this.IsPostBack)
        {
            show_report();
        }
        else
        {
            show_report2();
            

        }
    }
    

    private void show_report2()
    {
        string qrysrt = Request.QueryString["session_id_no"].ToString();
        clsReport rpt = (clsReport)Session[qrysrt];
      
        ConnectionInfo ConnInfo = rpt.ConnectionInfo;
        string[] frm, tmp;
        O_Report = (ReportDocument)Session[qrysrt + qrysrt];

        foreach (TableLogOnInfo cnInfo in CrystalReportViewer1.LogOnInfo)
            cnInfo.ConnectionInfo = ConnInfo;


        O_Report.Load(Server.MapPath(rpt.FileName));

        Tables RepTbls = O_Report.Database.Tables;

        foreach (CrystalDecisions.CrystalReports.Engine.Table RepTbl in RepTbls)
        {
            TableLogOnInfo RepTblLogonInfo = RepTbl.LogOnInfo;
            RepTblLogonInfo.ConnectionInfo = ConnInfo;
            RepTbl.ApplyLogOnInfo(RepTblLogonInfo);
        }


        if (rpt.Formulla != "")
        {
            frm = rpt.Formulla.Split(',');
            for (int i = 0; i < frm.Length; i++)
            {
                tmp = frm[i].Split(':');
                O_Report.DataDefinition.FormulaFields[tmp[0]].Text = tmp[1];
            }
        }


        
        CrystalReportViewer1.ParameterFieldInfo = rpt.ParametersFields;
        CrystalReportViewer1.ReportSource = O_Report;
        CrystalReportViewer1.ToolbarStyle.Width = new Unit("100%");


        CrystalReportViewer1.SelectionFormula = rpt.SelectionFormulla;
        CrystalReportViewer1.DataBind();


    }

    private void show_report()
    {
        string[] frm, tmp;
        string qrysrt = Request.QueryString["session_id_no"].ToString();
        clsReport rpt = (clsReport)Session[qrysrt];
       
        ConnectionInfo ConnInfo = rpt.ConnectionInfo;

        foreach (TableLogOnInfo cnInfo in CrystalReportViewer1.LogOnInfo)
            cnInfo.ConnectionInfo = ConnInfo;


        O_Report.Load(Server.MapPath(rpt.FileName));

        Tables RepTbls = O_Report.Database.Tables;

        foreach (CrystalDecisions.CrystalReports.Engine.Table RepTbl in RepTbls)
        {
            TableLogOnInfo RepTblLogonInfo = RepTbl.LogOnInfo;
            RepTblLogonInfo.ConnectionInfo = ConnInfo;
            RepTbl.ApplyLogOnInfo(RepTblLogonInfo);
        }

        if (rpt.Formulla != "")
        {
            frm = rpt.Formulla.Split(',');
            for (int i = 0; i < frm.Length; i++)
            {
                tmp = frm[i].Split(':');
                O_Report.DataDefinition.FormulaFields[tmp[0]].Text = tmp[1];
            }
        }


        CrystalReportViewer1.ParameterFieldInfo = rpt.ParametersFields;
        CrystalReportViewer1.ReportSource = O_Report;
        CrystalReportViewer1.ToolbarStyle.Width = new Unit("100%");


        CrystalReportViewer1.SelectionFormula = rpt.SelectionFormulla;
        CrystalReportViewer1.DataBind();

        Session[qrysrt + qrysrt] = O_Report;
        
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

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void CrystalReportViewer1_Unload(object sender, EventArgs e)
    {
        O_Report.Close();
        O_Report.Dispose();
        GC.Collect();
    }
}