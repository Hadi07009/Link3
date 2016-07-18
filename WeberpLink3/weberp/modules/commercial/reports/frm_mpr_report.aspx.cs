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

public partial class frm_mpr_report : System.Web.UI.Page
{   
    protected void Page_Load(object sender, EventArgs e)
    {
        //current.UserId = "MON";
        //current.UserName = "MONJU";

        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";

        if (!Page.IsPostBack)
        {
            cldfrom.SelectedDate = Convert.ToDateTime("01/12/2009");
            cldto.SelectedDate = DateTime.Now;
            load_mpr();

        }
        else
        {
           
        }
       
    }
    private void load_mpr()
    {
        chkPlant.Items.Clear();
        chkPlant.Items.Add("RTMPR");
    }
   

   
 
    //private void show_report(string selfor, string status)
    //{

    //    clsReport rpt = new clsReport();
    //    ParameterFields myParams = new ParameterFields();        
    //    ConnectionInfo ConnInfo = new ConnectionInfo();
    //    string SCBLconnStr = System.Configuration.ConfigurationManager.AppSettings["SCBLConnectionString"].ToString();
    //    string[] ff;
    //    string[] ss;

    //    string []frd=new string[4];
    //    string[] tod = new string[4];
    //    string DateFr, DateTo, selectionfor;
                

    //    frd[0] = cldfrom.SelectedDate.Day.ToString();
    //    frd[1] = cldfrom.SelectedDate.Month.ToString();
    //    frd[2] = cldfrom.SelectedDate.Year.ToString();

    //    tod[0] = cldto.SelectedDate.Day.ToString();
    //    tod[1] = cldto.SelectedDate.Month.ToString();
    //    tod[2] = cldto.SelectedDate.Year.ToString();

    //    DateFr = "date(" + frd[2] + "," + frd[1] + "," + frd[0] + ")";
    //    DateTo = "date(" + tod[2] + "," + tod[1] + "," + tod[0] + ")";

    //    ff = SCBLconnStr.Split('=');

    //    ss = ff[1].Split(';');
    //    ConnInfo.ServerName = ss[0];

    //    ss = ff[2].Split(';');
    //    ConnInfo.DatabaseName = ss[0];

    //    ss = ff[3].Split(';');
    //    ConnInfo.UserID = ss[0];

    //    ss = ff[4].Split(';');
    //    ConnInfo.Password = ss[0];
                       

        
    //    parameterpass(myParams, "Type", radType.Text);
    //    parameterpass(myParams, "Status", status);
    //    if (chkAllPlant.Checked)
    //        parameterpass(myParams, "Plant", "ALL");
    //    else
    //        parameterpass(myParams, "Plant", selfor);

    //    parameterpass(myParams, "DateFrom", cldfrom.VisibleDate.ToShortDateString());
    //    parameterpass(myParams, "DateTo", cldto.VisibleDate.ToShortDateString());

    //    parameterpass(myParams, "companytitle", current.CompanyName);
    //    parameterpass(myParams, "companyaddress", current.CompanyAddress);


    //    if(radType.SelectedIndex ==0 || radType.SelectedIndex ==2)
    //        selectionfor = "{putr_in_hdr.in_hdr_code} in " + selfor + " and {putr_in_hdr.in_hdr_status} in " + status + " and ({putr_in_hdr.in_hdr_st_date} in " + DateFr + " to " + DateTo + " or {putr_in_hdr.in_hdr_end_date} in " + DateFr + " to " + DateTo + ") ";
    //    else
    //        selectionfor = "{putr_in_hdr.in_hdr_code} in " + selfor + " and {putr_in_hdr.in_hdr_opr_code}='TGR' and {putr_in_hdr.in_hdr_status} in " + status + " and ({putr_in_hdr.in_hdr_st_date} in " + DateFr + " to " + DateTo + " or {putr_in_hdr.in_hdr_end_date} in " + DateFr + " to " + DateTo + ") ";
       


    //    rpt.ConnectionInfo = ConnInfo;
    //    rpt.FileName = "./files/rptMPR_details.rpt";
    //    rpt.ParametersFields = myParams;
    //    rpt.SelectionFormulla = selectionfor;
    //    current.SessionReport  = rpt;
        
    //    //Response.Write("<script>window.open('./frm_report_viewer.aspx');</script>");
    //    RegisterStartupScript("click", "<script>window.open('./frm_report_viewer.aspx');</script>");
    //}
    

    protected void btnShow_Click(object sender, EventArgs e)
    {
        string selfor = "[";
        string status = "[";



        foreach (ListItem lst in chkStatus.Items)
        {
            if (lst.Selected)
                status = status + "'" + lst.Value.ToString() + "',";
        }

        status = status.Remove(status.Length - 1, 1) + "]";

        foreach (ListItem lst in chkPlant.Items)
        {
            if (lst.Selected)
                selfor = selfor + "'" + lst.Value.ToString() + "',";
        }

        selfor = selfor.Remove(selfor.Length - 1, 1) + "]";

        //if (chkPlant.SelectedIndex != -1 & chkStatus.SelectedIndex != -1)
        //    //show_report(selfor, status);
    }

    protected void chkAllPlant_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAllPlant.Checked)
        {
            foreach (ListItem lst in chkPlant.Items)
            {
                lst.Selected = true;
            }
        }

        else
        {
            foreach (ListItem lst in chkPlant.Items)
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



