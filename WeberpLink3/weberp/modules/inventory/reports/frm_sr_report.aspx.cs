using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using LibraryDTO;
using LibraryDAL;
using LibraryDAL.SCBLDataSetTableAdapters;
using LibraryDAL.SCBL2DataSetTableAdapters;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using LibraryDAL.SCBLINTableAdapters;


public partial class frm_sr_report : System.Web.UI.Page
{

    ReportDocument rpt1 = new ReportDocument();
    protected void Page_Init(object sender, EventArgs e)
    {

        //current.UserId = "MON";
        //current.UserName = "MONJU";

        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";
      
        if (!Page.IsPostBack)
        {           
            load_plant();
           
        
        }
        else
        {
            showreport(false);
        }
          
    }
    

    private void load_plant()
    {         
        tbl_trn_detTableAdapter trn = new tbl_trn_detTableAdapter();
        SCBLDataSet.tbl_trn_detDataTable dttrn = new SCBLDataSet.tbl_trn_detDataTable();

        ListItem lst;
        int i;
        string[] items;



        ddlplantlist.Items.Clear();
        ddlplantlist.Items.Add("");
       
        dttrn = trn.GetAllCodeByType("IN");

        foreach (SCBLDataSet.tbl_trn_detRow dr in dttrn.Rows)
        {            
            lst = new ListItem();
            lst.Value = dr.trn_code.Substring(0, 2) + "SRQ";
            lst.Text = dr.trn_code.Substring(0, 2) + "SRQ";
            ddlplantlist.Items.Add(lst);            
                
        }

    }


    protected void btnview_Click(object sender, EventArgs e)
    {
        showreport(true);

    }

    private void showreport(bool flg)
    {
        if (Session["ddl"] == null) return;
        if (Session["ddl"].ToString() == "") return;

        DataTable dt = new DataTable();
        Intr_Sr_ReportTableAdapter rep = new Intr_Sr_ReportTableAdapter();
        
        DateTime dtfr = cldfrom.SelectedDate , dtto =cldto.SelectedDate;
        ParameterFields flds = new ParameterFields();        
        ParameterField param1 = new ParameterField();
        ParameterDiscreteValue dis1 = new ParameterDiscreteValue();
        ParameterField param2 = new ParameterField();
        ParameterDiscreteValue dis2 = new ParameterDiscreteValue();
        ParameterField param3 = new ParameterField();
        ParameterDiscreteValue dis3 = new ParameterDiscreteValue();

        param1.ParameterFieldName = "prmdtfrom";
        dis1.Value = dtfr.ToShortDateString();
        param1.CurrentValues.Add(dis1);
        flds.Add(param1);

        param2.ParameterFieldName = "prmdtto";
        dis2.Value = dtto.ToShortDateString();
        param2.CurrentValues.Add(dis2);
        flds.Add(param2);
        
        param3.ParameterFieldName = "prmtype";
        dis3.Value = Session["ddl"].ToString().ToString();
        param3.CurrentValues.Add(dis3);
        flds.Add(param3);


        if (flg)
        {
            dt = rep.GetDataForReport("APP", Session["ddl"].ToString(), dtfr, dtto.AddDays(1));
            Session["dt"] = dt;
        }
        else 
        {
            dt = (DataTable) Session["dt"];
        }
        

        rpt1.Load(Server.MapPath("files/rpt_sr_report.rpt"));
        rpt1.SetDataSource(dt);
               

        CrystalReportViewer1.ReportSource = rpt1;
        CrystalReportViewer1.ParameterFieldInfo = flds;
        CrystalReportViewer1.Zoom(75);
        CrystalReportViewer1.DataBind();
      
    }


    protected void ddlplantlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["ddl"] = ddlplantlist.Text;
    }
    protected void CrystalReportViewer1_Unload(object sender, EventArgs e)
    {
        rpt1.Close();
        rpt1.Dispose();
        GC.Collect();
    }
}


