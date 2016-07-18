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
using LibraryDTO;
using System.Net.Mail;
using LibraryDAL.SCBLDataSetTableAdapters;
using LibraryDAL.ProdReportDataSetTableAdapters;
using LibraryDAL.ProdDataSetTableAdapters;

public partial class ClientSide_modules_commercial_reports_frm_report_viewer : System.Web.UI.Page
{

    ReportDocument rpt1 = new ReportDocument();
    protected void Page_Init(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication();

        //if (!this.IsPostBack)
        //{

            string ref_no = Request.QueryString["ref_no"].ToString();
            string prodid = Request.QueryString["prod_id"].ToString();
            string tp = Request.QueryString["tp"].ToString();

            show_report(ref_no, prodid, tp);
        //}
        //else
        //{
                     

        //}
    }




    private void show_report(string uid, string prodid, string tp)
    {
        DataTable dt = new DataTable();
        dtIssueTableAdapter TAIssue = new dtIssueTableAdapter();
        tbl_prod_cs_reportTableAdapter cs = new tbl_prod_cs_reportTableAdapter();
        tbl_prod_cost_sheetTableAdapter cost = new tbl_prod_cost_sheetTableAdapter();
        LibraryDAL.ProdDataSet.tbl_prod_cost_sheetDataTable dtcost = new LibraryDAL.ProdDataSet.tbl_prod_cost_sheetDataTable();

        if (tp == "cs")
        {

            dt = cs.GetDataForReport(uid);
            if (dt.Rows.Count == 0) return;
            rpt1.Load(Server.MapPath("files/reptProductionCs.rpt"));

            ParameterFields flds = new ParameterFields();
            ParameterField param1 = new ParameterField();
            ParameterDiscreteValue dis1 = new ParameterDiscreteValue();
           
            string pid = dt.Rows[0]["prod_id"].ToString();
            DateTime stdate = Convert.ToDateTime(dt.Rows[0]["stdate"].ToString());
            DateTime enddate = Convert.ToDateTime(dt.Rows[0]["enddate"].ToString());
            string batch_ref_list = "";


            dtcost = cost.GetDataByProdIdDate(pid, stdate, enddate);

            foreach (LibraryDAL.ProdDataSet.tbl_prod_cost_sheetRow dr in dtcost.Rows)
            {
                if (batch_ref_list == "")
                {
                    batch_ref_list = dr.ref_no;
                }
                else
                {
                    batch_ref_list += ", " + dr.ref_no;
                }

            }

            param1.ParameterFieldName = "prm_batch_list";
            dis1.Value = batch_ref_list;
            param1.CurrentValues.Add(dis1);
            flds.Add(param1);

            CrystalReportViewer1.ParameterFieldInfo = flds;
            rpt1.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rpt1;
            CrystalReportViewer1.DataBind();


        }
        else
        {
            dt = cs.GetDataForReport(uid);
            if (dt.Rows.Count == 0) return;
            rpt1.Load(Server.MapPath("files/reptProductionCs_batch.rpt"));

            ParameterFields flds = new ParameterFields();
            ParameterField param1 = new ParameterField();
            ParameterDiscreteValue dis1 = new ParameterDiscreteValue();
           

            string pid = dt.Rows[0]["prod_id"].ToString();
            string prod_ref = dt.Rows[0]["prod_ref"].ToString();
            
           
            CrystalReportViewer1.ParameterFieldInfo = flds;
            rpt1.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rpt1;
            CrystalReportViewer1.DataBind();
        }



   }

     

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void CrystalReportViewer1_Unload(object sender, EventArgs e)
    {
        rpt1.Close();
        rpt1.Dispose();
        GC.Collect();

        
    }

   
}
