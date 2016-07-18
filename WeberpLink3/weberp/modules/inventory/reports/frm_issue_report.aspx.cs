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


public partial class frm_issue_report : System.Web.UI.Page
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

            iframe.Visible = false;
            dtstdate.SelectedDate = DateTime.Now.AddDays(-7);
            dtfinndate.SelectedDate = DateTime.Now;
        
        }
    
          
    }
    

   


    protected void btnview_Click(object sender, EventArgs e)
    {
        LibraryDTO.clsReport rpt = new LibraryDTO.clsReport();
        ParameterFields myParams = new ParameterFields();
        ParameterField param = new ParameterField();
        ParameterDiscreteValue dis = new ParameterDiscreteValue();

        DateTime dtst = Convert.ToDateTime(dtstdate.SelectedDate.ToShortDateString());
        DateTime dtend = Convert.ToDateTime(dtfinndate.SelectedDate.ToShortDateString());
        //string[] tmp;
        //tmp = txtvihiclelist.Text.Split(':')[0];
        string frc = txtvihiclelist.Text.Split(':')[0], toc = txtvihiclelistto.Text.Split(':')[0];

        string selfor = "({view_vehicle_issue.Trn_Hdr_DATE} IN DateTime('" + dtst + "') TO DateTime('" + dtend + "'))";

        if (chkall.Checked==false)
        {
            selfor = selfor + " and ({view_vehicle_issue.T_C2} in '" + frc + "' to '" + toc + "' )";
        }

        param.ParameterFieldName = "prm_period";
        dis.Value = "Period From " + dtstdate.SelectedDate.ToShortDateString() + " To " + dtfinndate.SelectedDate.ToShortDateString();
        param.CurrentValues.Add(dis);
        myParams.Add(param);
        rpt.DatabaseName = "RTL";
        rpt.ParametersFields = myParams;
        rpt.SelectionFormulla = selfor;
        rpt.FileName = "files/rpt_vehicle_issue.rpt";
        rpt.PageZoomFactor = 75;
        current.SessionReport = rpt;
        iframe.Visible = true;

    }

    


   
    
}


