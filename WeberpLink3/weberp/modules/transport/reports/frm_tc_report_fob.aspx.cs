using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibraryDAL.dsScfTableAdapters;
using LibraryDAL.SCBLINTableAdapters;
using LibraryDAL;
using LibraryDAL.dsMovementTableAdapters;
using LibraryDAL.dsTransportTableAdapters;
using CrystalDecisions.Web.Design;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Net.Mail;


public partial class frm_tc_report_fob : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        clsStatic.CheckUserAuthentication();
        if (!Page.IsPostBack)
        {
            //------------------------------

            //clsSystem usr = new clsSystem();
            //dsLinkoffice.tblUserInfoDataTable dtusr = new dsLinkoffice.tblUserInfoDataTable();
            //string userid = "MON";
            //dtusr = usr.GetUserById(userid, current.CompanyCode);
            //current.UserId = dtusr[0].UserId;
            //current.UserName = dtusr[0].UserName;
            //current.UserDepartment = dtusr[0].UserDepartment;
            //current.UserDesignation = dtusr[0].UserDesignation;
            //current.UserEmail = dtusr[0].UserEmail;

            //------------------------------            
            dtstdate.SelectedDate = DateTime.Now;
            dtfinndate.SelectedDate = DateTime.Now;

            tblparty.Visible = true;
            tblwing.Visible = false;
            load_wing();
                                  
        }
    }
       

    private void load_wing()
    {
        SaMa_Trn_IndexTableAdapter ind = new SaMa_Trn_IndexTableAdapter();
        dsScf.SaMa_Trn_IndexDataTable dtind = new dsScf.SaMa_Trn_IndexDataTable();
        ListItem lst;
        chkwing.Items.Clear();

        dtind = ind.GetDataByType("SS");

        foreach (dsScf.SaMa_Trn_IndexRow dr in dtind.Rows)
        {
            lst = new ListItem();
            lst.Text = dr.Trn_Code;
            lst.Value = dr.Trn_Index.ToString();
            chkwing.Items.Add(lst);

        }

    }

    
    protected void btnShowPeport_Click(object sender, EventArgs e)
    {
        SaMa_Rpt_PerTableAdapter rptper = new SaMa_Rpt_PerTableAdapter();
        SCBLIN.SaMa_Rpt_PerDataTable dtrptper = new SCBLIN.SaMa_Rpt_PerDataTable();

        DateTime fdate = Convert.ToDateTime(dtstdate.SelectedDate.ToShortDateString());
        DateTime tdate = Convert.ToDateTime(dtfinndate.SelectedDate.ToShortDateString());


        LibraryDTO.clsReport rpt = new LibraryDTO.clsReport();
        ParameterFields myParams = new ParameterFields();
        string title = "";
        string section = "";


        rpt.SelectionFormulla = "{view_fob_do_report.Trn_Hdr_Type} ='II' ";


        switch (rdosel.SelectedIndex)
        {
            case 0:
                rpt.SelectionFormulla += "and ({view_fob_do_report.Trn_Hdr_HRPB_flag} ='P')  and  {view_fob_do_report.Trn_Hdr_DATE} in " + "datetime('" + fdate + "') to " + "datetime('" + tdate + "')";
                break;

            case 1:
                rpt.SelectionFormulla += "and ({view_fob_do_report.Trn_Hdr_HRPB_flag} ='H') ";
                break;
                                           
            case 2:
                rpt.SelectionFormulla += " and {view_fob_do_report.Trn_Hdr_Ref} ='" + txttcno.Text.Split(':')[0] + "'";
                break;

            default:
                break;
        }

        if (rdoparty.Enabled)
        {
            if (rdoparty.SelectedIndex == 0)
            {
                if (chkpartyall.Checked == false)
                {
                    rpt.SelectionFormulla += " and {view_fob_do_report.Par_Acc_Code} = '" + txtparty.Text.Split(':')[0] + "'";
                    if ((ddlsubparty.Text != "ALL") && (ddlsubparty.Text != ""))
                    {
                        rpt.SelectionFormulla += " and {view_fob_do_report.Par_Adr_Code} = '" + ddlsubparty.SelectedValue.Split(':')[0] + "'";
                    }
                }
            }
            else
            {

                if (chkwingall.Checked == false)
                {
                    foreach (ListItem lst in chkwing.Items)
                    {
                        if (lst.Selected)
                        {
                            if (section == "") section = "'" + lst.Text.ToString() + "'";
                            else section = section + ",'" + lst.Text.ToString() + "'";
                        }
                    }

                    if (section == "") section = "['']"; else section = "[" + section + "]";
                    rpt.SelectionFormulla += " and {view_fob_do_report.sc_hdr_code} in " + section + "";

                }
            }
        }

        //permission for specific user

        dtrptper = rptper.GetDataByUser(current.UserId);
        if (dtrptper.Rows.Count > 0)
        {
            string plist = "";
            foreach (SCBLIN.SaMa_Rpt_PerRow dr in dtrptper.Rows)
            {
                if (plist == "")
                {
                    plist = "'" + dr.Rpt_Per_Acc_Code + "'";
                }
                else
                {
                    plist = plist + ",'" + dr.Rpt_Per_Acc_Code + "'";
                }
            }

            rpt.SelectionFormulla = rpt.SelectionFormulla + " and ({view_fob_do_report.Par_Acc_Code} in [" + plist + "])";

        }


        if (fdate > tdate) return;        
        title = rdosel.SelectedItem.Text + ":" + "(" + rdogrp.SelectedItem.Text + ")";

        if (rdotype.SelectedIndex == 0)
        {
            rpt.FileName = "file/fob_TransportContactReport.rpt";
        }
        else
        {
            rpt.FileName = "file/fob_TransportContactSummary.rpt";
            title = rdogrp.SelectedItem.Text.ToString().Replace("wise","");
        }

        parameterpass(myParams, "companytitle", current.CompanyName);
        parameterpass(myParams, "companyaddress", current.CompanyAddress);

        parameterpass(myParams, "headline", title);

        
        parameterpass(myParams, "period", "Period :" + fdate.ToShortDateString() + " To " + tdate.ToShortDateString());
        
        parameterpass(myParams, "grpupby", rdogrp.SelectedIndex.ToString());


        rpt.ParametersFields = myParams;
        rpt.PageZoomFactor = 100;

        current.SessionReport = rpt;

        RegisterStartupScript("click", "<script>window.open('./frm_rpt_viewer.aspx');</script>");

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
    protected void txtparty_TextChanged(object sender, EventArgs e)
    {
        SaMa_Par_AdrTableAdapter adr = new SaMa_Par_AdrTableAdapter();
        dsTransport.SaMa_Par_AdrDataTable dtadr = new dsTransport.SaMa_Par_AdrDataTable();

        ddlsubparty.Items.Clear();

        dtadr = adr.GetDataByAccCode(txtparty.Text.Split(':')[0]);
        if (dtadr.Rows.Count == 0) return;

        ddlsubparty.Items.Add("ALL");

        foreach (dsTransport.SaMa_Par_AdrRow dr in dtadr.Rows)
        {
            ddlsubparty.Items.Add(dr.Par_Adr_Code + ":" + dr.par_adr_name);
        }

        

    }
    protected void rdoparty_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoparty.SelectedIndex == 0)
        {
            tblparty.Visible = true;
            tblwing.Visible = false;
        }
        else
        {
            tblparty.Visible = false;
            tblwing.Visible = true;
        }
    }
    protected void rdosel_SelectedIndexChanged(object sender, EventArgs e)
    {
        dtstdate.Enabled = true;
        dtfinndate.Enabled = true;       
        lblstatus.Visible = false;
        txttcno.Visible = false;
        rdoparty.Enabled = true;
        rdoparty_SelectedIndexChanged(null, null);


       


        if ((rdosel.SelectedIndex == 1)||(rdosel.SelectedIndex == 2))
        {
            dtstdate.Enabled = false;
            dtfinndate.Enabled = false;           
          
        }

        if (rdosel.SelectedIndex == 2)
        {
            lblstatus.Visible = true;
            txttcno.Visible = true;
            rdoparty.Enabled = false;
            tblparty.Visible = false;
            tblwing.Visible = false;

        }


    }
   
}
