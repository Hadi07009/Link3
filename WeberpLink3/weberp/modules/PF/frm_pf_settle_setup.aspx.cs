using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using CrystalDecisions;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using LibraryDAL;
using LibraryPF;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Globalization;
using LibraryPF.dsMasterDataTableAdapters;

public partial class frm_pf_settle_setup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);
        clsStatic.MsgConfirmBox(btadd, "Are you sure to add ? ");
        if (Page.IsPostBack == false)
        {                     
            Tbldet.Visible = true;
            Load_grid();

        }
       
    }


 


    Hrms_pf_sat_setupTableAdapter pfsat = new Hrms_pf_sat_setupTableAdapter();
    LibraryPF.dsMasterData.Hrms_pf_sat_setupDataTable dtpfsat = new LibraryPF.dsMasterData.Hrms_pf_sat_setupDataTable();

    
    protected void btnupdate_Click(object sender, EventArgs e)
    {

        pfsat = new Hrms_pf_sat_setupTableAdapter();
        dtpfsat = new LibraryPF.dsMasterData.Hrms_pf_sat_setupDataTable();

        if (check_entry() != "")
        {
            lblmessage.Text = check_entry();
            lblmessage.ForeColor = System.Drawing.Color.Red;
            return; 
        }
   
       try 
       
       {

           pfsat.InsertPFSatDet(Convert.ToInt32(txtfrommonth.Text), Convert.ToInt32(txttomonth.Text), Convert.ToDecimal(txtowncontribtion.Text), Convert.ToDecimal(txtownprofit.Text), Convert.ToDecimal(txtemployeercontribtion.Text), Convert.ToDecimal(txtemployeerprofit.Text));
       }

        catch(Exception ex)
       {

         lblmessage.Text=  ex.Message;
         lblmessage.ForeColor = System.Drawing.Color.Red;
         return;          
        }

        lblmessage.Text = "Data Saved Successfully";
        lblmessage.ForeColor = System.Drawing.Color.Green;
        Load_grid();
        Tbldet.Visible = true;
        Clear_field();

    }

    private string  check_entry()
    {

        if (txtfrommonth.Text == "") return "Enter from month";
        if (txttomonth.Text == "") return "Enter to month";

        if (txtowncontribtion.Text == "") return "Enter own contribution";
        if (txtownprofit.Text == "") return "Enter own profit";
        if (txtemployeercontribtion.Text == "") return "Enter employeer contribution";
        if (txtemployeerprofit.Text == "") return "Enter employeer profit";

        return "";
    
    }


    private void Load_grid()
    {
        DataTable dt = new DataTable();

        pfsat = new Hrms_pf_sat_setupTableAdapter();
        dtpfsat = new LibraryPF.dsMasterData.Hrms_pf_sat_setupDataTable();
       
        dtpfsat = pfsat.GetDataAll();

        
        dt.Columns.Add("From month", typeof(string));
        dt.Columns.Add("To month", typeof(string));
        dt.Columns.Add("Own contribution", typeof(string));
        dt.Columns.Add("Own profit", typeof(string));
        dt.Columns.Add("Employeer contribution", typeof(string));
        dt.Columns.Add("Employeer profit", typeof(string));


        foreach (LibraryPF.dsMasterData.Hrms_pf_sat_setupRow dr in dtpfsat.Rows)
        {
            dt.Rows.Add( dr.from_month, dr.to_month, dr.own_cont_ratio,dr.own_profit_ratio,dr.employer_cont_ratio,dr.employer_profit_ratio);
        }

        gdvpfsat.DataSource = dt;
        gdvpfsat.DataBind();
      

        
    }
   
    private void Clear_field()
    {
        txtfrommonth.Text = "";
        txtowncontribtion.Text = "";
        txtemployeercontribtion.Text = "";
        txttomonth.Text = "";
        txtownprofit.Text = "";
        txtemployeerprofit.Text = "";
       // ddlcompany.Text = "";

        Loaddata();


    }


    protected void gdvpfsat_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmessage.Text = "";

        pfsat = new Hrms_pf_sat_setupTableAdapter();
        dtpfsat = new LibraryPF.dsMasterData.Hrms_pf_sat_setupDataTable();
        int indx = gdvpfsat.SelectedIndex;
        if (indx < 0) return;
        int  frommonth;
      

        
        frommonth = Convert.ToInt32(gdvpfsat.Rows[indx].Cells[1].Text);
        pfsat.DeleteDataByCompany(frommonth);
        Load_grid();
       
    }
   
   
    protected void btnclear_Click(object sender, EventArgs e)
    {
        Clear_field();
        lblmessage.Text = "";
    }
    protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
    {         
        Loaddata();
          Clear_field();
    }



    private void Loaddata()
    {

        lblmessage.Text = "";
        pfsat = new Hrms_pf_sat_setupTableAdapter();
        dtpfsat = new LibraryPF.dsMasterData.Hrms_pf_sat_setupDataTable();


        dtpfsat = pfsat.GetData();

        if (dtpfsat.Rows.Count > 0)
        {
            Load_grid();


            Tbldet.Visible = true;
        }
        else
        {
            Tbldet.Visible = false;
        }

    
    }
}
