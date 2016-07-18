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

public partial class frm_po_detail_view : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {

        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor ="FFFFFFF";
        
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
       
        rdocode.Items.Clear();
        rdocode.Items.Add("CM");
        rdocode.SelectedIndex = 0;

    }
       

    protected void btnShow_Click(object sender, EventArgs e)
    {
        clsReportParameter prm = new clsReportParameter();

        prm.PoType = rdopoype.SelectedValue.ToString();
        prm.Plant = rdocode.SelectedValue.ToString();
        prm.ToDate = cldto.SelectedDate;
        prm.FromDate = cldfrom.SelectedDate;

        Session[clsStatic.sessionPoReportPrm] = prm;

        RegisterStartupScript("click", "<script>window.open('./frm_po_detail_print.aspx');</script>");

    }








   
}