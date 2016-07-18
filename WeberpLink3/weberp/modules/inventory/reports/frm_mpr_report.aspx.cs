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

public partial class frm_mpr_report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";
        lblmessage.Visible = false;

        if (!Page.IsPostBack)
        {
            DateTime now = DateTime.Now;
            DateTime date = new DateTime(now.Year, now.Month, 1);
            cldfrdate.SelectedDate = date;
            cldtodate.SelectedDate = DateTime.Now;
            Autoteformprref.ContextKey = cldfrdate.SelectedDate.ToShortDateString() + ":" + cldtodate.SelectedDate.AddDays(1).ToShortDateString();

            // loaddepartment();
             loadstore();
        }
        else
        {
        }
    }
   
    //private void loaddepartment()
    //{
    //    Hrms_Dept_MasterTableAdapter dept = new Hrms_Dept_MasterTableAdapter();
    //    SCBLIN.Hrms_Dept_MasterDataTable dtdept = new SCBLIN.Hrms_Dept_MasterDataTable();

    //    ListItem lst;
    //    ddldepartment .Items.Clear();
    //    ddldepartment.Items.Add("");
    //    dtdept = dept.GetAllDept();
    //    foreach (SCBLIN.Hrms_Dept_MasterRow dr in dtdept.Rows)
    //    {
    //        lst = new ListItem();
    //        lst.Value = dr.Dept_Code;
    //        lst.Text = dr.Dept_Name;
    //        ddldepartment.Items.Add(lst);
    //    }
    //}

    private void loadstore()
    {
        InMa_Str_LocTableAdapter store = new InMa_Str_LocTableAdapter();
        ErpDataSet.InMa_Str_LocDataTable dtstore = new ErpDataSet.InMa_Str_LocDataTable();

        ListItem lst;
        ddlstore.Items.Clear();
       // ddlstore.Items.Add("");
        dtstore = store.GetAllStore();
        foreach (ErpDataSet.InMa_Str_LocRow  dr in dtstore.Rows)
        {
            lst = new ListItem();
            lst.Value = dr.Str_Loc_Id ;
            lst.Text = dr.Str_Loc_Name ;
            ddlstore.Items.Add(lst);
        }
    }

    protected void btnview_Click(object sender, EventArgs e)
    {
        //DateTime fdate = Convert.ToDateTime(cldfrdate.SelectedDate.ToShortDateString());
        //DateTime tdate = Convert.ToDateTime(cldtodate.SelectedDate.ToShortDateString());

       

        //LibraryDTO.clsReport rpt = new LibraryDTO.clsReport();
        //ParameterFields myParams = new ParameterFields();
        //string title = "", title2="", durl = "";
        //if (fdate > tdate) return;

        //rpt.SelectionFormulla = "";
        //rpt.SelectionFormulla = "({View_Material_purchase_requisition.IN_Hdr_Type}='IN' and {View_Material_purchase_requisition.IN_Det_Str_Code}='" + ddlstore.SelectedItem.Value + "' and {View_Material_purchase_requisition.IN_Hdr_St_DATE} in " + "datetime('" + fdate + "') to " + "datetime('" + tdate + "'))";

        //if (chkdeptall.Checked == false)

        //{
        //    PuTr_IN_HdrTableAdapter mpr = new PuTr_IN_HdrTableAdapter();
        //    SCBLDataSet.PuTr_IN_HdrDataTable dtmpr = new SCBLDataSet.PuTr_IN_HdrDataTable();
        //    dtmpr = mpr.GetDataByRef(txtmprref.Text.Trim());

        //    if (txtmprref.Text.Trim() == "")
        //    {
        //        lblmessage.Text = "Type Ref No.";
        //        lblmessage.Visible = true;
        //        return;
        //    }

        //    if (dtmpr.Rows.Count == 0)
        //    {

        //        lblmessage.Text = "Invalid Ref No.";
        //        lblmessage.Visible = true;
        //        return;
        //    }

        //    rpt.SelectionFormulla += " and {View_Material_purchase_requisition.IN_Hdr_Ref} = '" + txtmprref.Text.Trim() + "'";
        //}

        //parameterpass(myParams, "companytitle", current.CompanyName);
        //parameterpass(myParams, "companyaddress", current.CompanyAddress);


        //title = "MATERIAL PURCHASE REQUISITION (MPR)" ;
        //parameterpass(myParams, "title", title);

        //rpt.ParametersFields = myParams;

        //rpt.FileName = "files/rpt_mpr_con.rpt";

        //rpt.PageZoomFactor = 100;
        //current.SessionReport = rpt;
        //RegisterStartupScript("click", "<script>window.open('frm_rpt_viewer.aspx');</script>");

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

    protected void cldtodate_DateChanged(object sender, EventArgs e)
    {

        Autoteformprref.ContextKey = cldfrdate.SelectedDate.ToShortDateString() + ":" + cldtodate.SelectedDate.AddDays(1).ToShortDateString();

        
    }
}
