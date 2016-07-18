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
using LibraryDAL.ErpDataSetTableAdapters;


public partial class frm_item_stock : System.Web.UI.Page
{
    ReportDocument rpt1 = new ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
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
            showreport();
        }
          
    }
    

    private void load_plant()
    {
        InMa_Str_LocTableAdapter store = new InMa_Str_LocTableAdapter();
        ErpDataSet.InMa_Str_LocDataTable dtstore = new ErpDataSet.InMa_Str_LocDataTable();
        ListItem lst;
        dtstore = store.GetAllStore();

        ddlplantlist.Items.Clear();
        ddlplantlist.Items.Add("");

        foreach (ErpDataSet.InMa_Str_LocRow dr in dtstore.Rows)
        {
            lst = new ListItem();
            lst.Text = dr.Str_Loc_Id + ":" + dr.Str_Loc_Name;
            lst.Value = dr.Str_Loc_Id;
            ddlplantlist.Items.Add(lst);

        }       

    }


    protected void btnview_Click(object sender, EventArgs e)
    {
        showreport();

    }

    private void showreport()
    {
        if (ddlplantlist.SelectedItem.ToString() == "") return;

        DataTable dt = new DataTable();
        utbl_currentstoockTableAdapter sto = new utbl_currentstoockTableAdapter();

        dt = sto.GetDataByStore(ddlplantlist.SelectedItem.ToString());

        rpt1.Load(Server.MapPath("files/rpt_itm_stk.rpt"));
        rpt1.SetDataSource(dt);

        CrystalReportViewer1.ReportSource = rpt1;
        CrystalReportViewer1.DataBind();
      
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


    protected void CrystalReportViewer1_Unload(object sender, EventArgs e)
    {
        rpt1.Close();
        rpt1.Dispose();
        GC.Collect();
    }
}


