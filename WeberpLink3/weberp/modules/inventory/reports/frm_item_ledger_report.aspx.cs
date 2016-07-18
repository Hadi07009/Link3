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

public partial class frm_item_ledger_report : System.Web.UI.Page
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
            load_main_group();
            load_itms_category();
        }
        else
        {
        }
    }

    private void load_main_group()
    {
        InMa_Grp_CodeTableAdapter grp = new InMa_Grp_CodeTableAdapter();
        ErpDataSet.InMa_Grp_CodeDataTable dtgrp = new ErpDataSet.InMa_Grp_CodeDataTable();
        ListItem lst;

        dtgrp = grp.GetDataByCode("I01", "", "");

        ddlmaingroup.Items.Clear();
        ddlmaingroup.Items.Add("");

        foreach (ErpDataSet.InMa_Grp_CodeRow dr in dtgrp.Rows)
        {
            lst = new ListItem();
            lst.Value = dr.Grp_Code;
            lst.Text = dr.Grp_Code + ":" + dr.Grp_Code_Name;
            ddlmaingroup.Items.Add(lst);
        }

    }

    private void load_itms_category()
    {
        ListItem lst;
     
        lst = new ListItem();
        lst.Text = "Finish Goods";
        lst.Value = "F";
        ddlitemcategory.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "Raw Material";
        lst.Value = "R";
        ddlitemcategory.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "Process Material";
        lst.Value = "P";
        ddlitemcategory.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "Packing Material";
        lst.Value = "K";
        ddlitemcategory.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "Mechanical";
        lst.Value = "M";
        ddlitemcategory.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "Electrical";
        lst.Value = "E";
        ddlitemcategory.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "Tools";
        lst.Value = "T";
        ddlitemcategory.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "General Hardware";
        lst.Value = "H";
        ddlitemcategory.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "Fuel & Lubricant";
        lst.Value = "L";
        ddlitemcategory.Items.Add(lst);
        //
        lst = new ListItem();
        lst.Text = "Civil";
        lst.Value = "C";
        ddlitemcategory.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "Domestic Equipments";
        lst.Value = "D";
        ddlitemcategory.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "Wastage and Scrap";
        lst.Value = "W";
        ddlitemcategory.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "Fixed Asset";
        lst.Value = "A";
        ddlitemcategory.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "IT Equipments";
        lst.Value = "I";
        ddlitemcategory.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "Auto Mobile";
        lst.Value = "V";
        ddlitemcategory.Items.Add(lst);

        //Item Location 

    }
    

    protected void btnview_Click(object sender, EventArgs e)
    {
        DateTime fdate = Convert.ToDateTime(cldfrdate.SelectedDate.ToShortDateString());
        DateTime tdate = Convert.ToDateTime(cldtodate.SelectedDate.ToShortDateString());

        LibraryDTO.clsReport rpt = new LibraryDTO.clsReport();
        ParameterFields myParams = new ParameterFields();
        string title = "", title2="", durl = "";
        if (fdate > tdate) return;

        rpt.SelectionFormulla = "";

        if (chkitemcat.Checked == false)
        {
           // if (rpt.SelectionFormulla != "") rpt.SelectionFormulla = rpt.SelectionFormulla + " and ";

            rpt.SelectionFormulla += " {inv_ledger;1.Itm_Det_Type_flag}='" + ddlitemcategory.SelectedItem.Value + "'";
        }


        if (chkmaingrp.Checked == false)
        {
            if (rpt.SelectionFormulla != "") rpt.SelectionFormulla = rpt.SelectionFormulla + " and ";
            rpt.SelectionFormulla += " {inv_ledger;1.first_grp_code} = '" + ddlmaingroup.SelectedItem.Value + "'";
            
        }

        if (chksubgrp.Checked == false)
        {
            if (rpt.SelectionFormulla != "") rpt.SelectionFormulla = rpt.SelectionFormulla + " and ";
            rpt.SelectionFormulla += " {inv_ledger;1.second_grp_code} = '" + ddlsubgroup.SelectedItem.Value + "'";
           
        }

        if (chksubsub.Checked == false)
        {
            if (rpt.SelectionFormulla != "") rpt.SelectionFormulla = rpt.SelectionFormulla + " and ";
            rpt.SelectionFormulla += " {inv_ledger;1.third_grp_code} = '" + ddlsubsubgroup.SelectedItem.Value + "'";
            
            
        }


        if (chkitm.Checked == false)
        {
            if (rpt.SelectionFormulla != "") rpt.SelectionFormulla = rpt.SelectionFormulla + " and ";
            rpt.SelectionFormulla += " {inv_ledger;1.Itm_Det_Icode} in  '" + ddlitmfrom.SelectedItem.Value + "' to '" + ddlitmto.SelectedItem.Value + "'";
            
            
        }

        title = "Item Ledger Report ("+( rdolistreporttype.SelectedItem.Text +")");

        parameterpass(myParams, "title", title);
        parameterpass(myParams, "companytitle", current.CompanyName);
        parameterpass(myParams, "companyaddress", current.CompanyAddress);

        string strcode ="CMGEN";

        parameterpass(myParams, "@st_date",  fdate.ToShortDateString() );
        parameterpass(myParams, "@end_date", tdate.ToShortDateString());

        parameterpass(myParams, "@str_code", strcode);



        rpt.ParametersFields = myParams;

        if (rdolistreporttype.SelectedIndex == 0)
        {
            rpt.FileName = "files/rpt_store_ledger_det.rpt";
            rpt.SelectionFormulla = rpt.SelectionFormulla.Replace("{inv_ledger;1", "{inv_ledger_det;1");
        }
        else
        {
            rpt.FileName = "files/rpt_item_ledger_sum.rpt";
        }
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

    protected void ddlloanref_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlmaingroup_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlsubsubgroup.Items.Clear();
        ddlitmfrom.Items.Clear();
        ddlitmto.Items.Clear();
        InMa_Grp_CodeTableAdapter grp = new InMa_Grp_CodeTableAdapter();
        ErpDataSet.InMa_Grp_CodeDataTable dtgrp = new ErpDataSet.InMa_Grp_CodeDataTable();
        DataTable dt = new DataTable();
        dtgrp = grp.GetDataByCode("I02", ddlmaingroup.SelectedValue.ToString(), "");
        ListItem lst;

        lst = new ListItem();

        ddlsubgroup.Items.Clear();
        ddlsubgroup.Items.Add("");

        foreach (ErpDataSet.InMa_Grp_CodeRow dr in dtgrp.Rows)
        {
            lst = new ListItem();
            lst.Value = dr.Grp_Code;
            lst.Text = dr.Grp_Code + ":" + dr.Grp_Code_Name;
            ddlsubgroup.Items.Add(lst);
        }
    }
    protected void ddlsubgroup_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlitmfrom.Items.Clear();
        ddlitmto.Items.Clear();
        InMa_Grp_CodeTableAdapter grp = new InMa_Grp_CodeTableAdapter();
        ErpDataSet.InMa_Grp_CodeDataTable dtgrp = new ErpDataSet.InMa_Grp_CodeDataTable();
        DataTable dt = new DataTable();
        dtgrp = grp.GetDataByCode("I03",ddlmaingroup.SelectedValue.ToString(), ddlsubgroup.SelectedValue.ToString());
        ListItem lst;

        lst = new ListItem();

        ddlsubsubgroup.Items.Clear();
        ddlsubsubgroup.Items.Add("");

        foreach (ErpDataSet.InMa_Grp_CodeRow dr in dtgrp.Rows)
        {
            lst = new ListItem();
            lst.Value = dr.Grp_Code;
            lst.Text = dr.Grp_Code + ":" + dr.Grp_Code_Name;
            ddlsubsubgroup.Items.Add(lst);
        }
    }
    protected void ddlsubsubgroup_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlitmfrom.Items.Clear();
        ddlitmto.Items.Clear();

        string pref = ddlmaingroup.SelectedValue.ToString() + "." + ddlsubgroup.SelectedValue.ToString() + "." + ddlsubsubgroup.SelectedValue.ToString();

        if (pref.Length != 10) return;

        InMa_Itm_DetTableAdapter itm = new InMa_Itm_DetTableAdapter();
        ErpDataSet.InMa_Itm_DetDataTable dtitm = new ErpDataSet.InMa_Itm_DetDataTable();

        dtitm = itm.GetDataByAllGroup(pref);
        ListItem lst, lst2;

        lst = new ListItem();

        ddlitmfrom.Items.Clear();
        ddlitmfrom.Items.Add("");

        ddlitmto.Items.Clear();
        ddlitmto.Items.Add("");

        foreach (ErpDataSet.InMa_Itm_DetRow  dr in dtitm.Rows)
        {
            lst = new ListItem();
            lst.Value = dr.Itm_Det_Icode;
            lst.Text = dr.Itm_Det_desc;
            ddlitmfrom.Items.Add(lst);

            //lst2 = new ListItem();
            //lst2.Value = dr.Itm_Det_Icode;
            //lst2.Text = dr.Itm_Det_desc;

            ddlitmto.Items.Add(lst);
        }

       

    }
}
