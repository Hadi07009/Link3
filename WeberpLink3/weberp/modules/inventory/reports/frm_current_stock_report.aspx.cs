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

public partial class frm_current_stock_report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";
        lblmessage.Visible = false;

        if (!Page.IsPostBack)
        {
            //cldfrdate.SelectedDate = Convert.ToDateTime("01/01/2014");
            //cldtodate.SelectedDate = DateTime.Now;
            load_main_group();
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
    

    protected void btnview_Click(object sender, EventArgs e)
    {
        //DateTime fdate = Convert.ToDateTime(cldfrdate.SelectedDate.ToShortDateString());
        //DateTime tdate = Convert.ToDateTime(cldtodate.SelectedDate.ToShortDateString());

        LibraryDTO.clsReport rpt = new LibraryDTO.clsReport();
        ParameterFields myParams = new ParameterFields();

        rpt.SelectionFormulla = "";

        if (rdolistitemqty.SelectedIndex == 0)
        {
            rpt.SelectionFormulla = "({view_current_stock.current_stock}>=0)";
        }
      
         else  if (rdolistitemqty.SelectedIndex == 1)
        {
            rpt.SelectionFormulla = "({view_current_stock.current_stock}>0)";
        }

         else 
        {
            rpt.SelectionFormulla = "({view_current_stock.current_stock}=0)";
        
        }

         if (chkmaingrp.Checked == false)
         {
             if (ddlmaingroup.Text == "")
             {
                 lblmessage.Text = "Select main group";
                 lblmessage.Visible = true;
                 return; 
             }
             rpt.SelectionFormulla += " and {view_current_stock.first_grp_code} ='" + ddlmaingroup.SelectedItem.Value + "'";
         }

         if (chksubgrp.Checked == false)
         {
             if (ddlsubgroup.Text == "")
             {
                 lblmessage.Text = "Select sub group";
                 lblmessage.Visible = true;

                 return;
             }
             rpt.SelectionFormulla += " and {view_current_stock.second_grp_code} = '" + ddlsubgroup.SelectedItem.Value + "'";
         }

         if (chksubsubgrp.Checked == false)
         {
             if (ddlsubsubgroup.Text == "")
             {
                 lblmessage.Text = "Select sub sub group";
                 lblmessage.Visible = true;

                 return;
             }
             rpt.SelectionFormulla += " and {view_current_stock.third_grp_code} = '" + ddlsubsubgroup.SelectedItem.Value + "'";
         }


         parameterpass(myParams, "companytitle", current.CompanyName);
         parameterpass(myParams, "companyaddress", current.CompanyAddress);

        rpt.ParametersFields = myParams;

        if (rdolistitemqty.SelectedIndex == 0)
        {
            rpt.FileName = "files/rpt_current_stock_report.rpt";
        }
        else
        {
            rpt.FileName = "files/rpt_current_stock_report.rpt";
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
}
