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
using LibraryPF.dsTransactionTableAdapters;
using System.Data.SqlClient;

public partial class frm_pf_settlement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);
        clsStatic.MsgConfirmBox(btnsettlement, "Are you sure to settlement ? ");
        if (Page.IsPostBack == false)
        {
            tblempbasicinfo.Visible = false;
            tblpfcontribution.Visible = false;
                  
        }

    }

    private int GetMonthLength(DateTime joindate, DateTime settdate)
    {
        int len = 0;
        while (joindate.AddMonths(1).AddDays(-1) <= settdate)
        {
            len = len + 1;
           joindate = joindate.AddMonths(1);          
        }
        return len;
    }

    private void job_length()
    {
        DateTime settdate = Convert.ToDateTime(dtsettdate.SelectedDate.ToShortDateString());
        DateTime joindate;
        int joblength = 0;
        view_employee_infoTableAdapter emp = new view_employee_infoTableAdapter();
        dsMasterData.view_employee_infoDataTable dtemp = new dsMasterData.view_employee_infoDataTable ();
        dtemp = emp.GetDataByEmpCode(txtemployee.Text.Split(':')[0].ToString());
        if (dtemp.Rows.Count == 0) return;                
        joindate = dtemp[0].emp_join_date;
        joblength = GetMonthLength(joindate, settdate);
        lbljoblength.Text = joblength.ToString();
    }


    private string check_entry()
    {
        if (txtemployee.Text == "") return "Select employee";
        return "";
    }
   
    protected void btshow_Click(object sender, EventArgs e)
    {

        view_employee_infoTableAdapter emp = new view_employee_infoTableAdapter();
        dsMasterData.view_employee_infoDataTable dtemp = new dsMasterData.view_employee_infoDataTable();
        dtemp = emp.GetDataByEmpCode(txtemployee.Text.Split(':')[0].ToString());

        if (dtemp.Rows.Count == 0)
        {
            lblmessage.Text = "No record found of this Employee";
            tblempbasicinfo.Visible = false;
            tblpfcontribution.Visible = false;
            lblmessage.ForeColor = System.Drawing.Color.Red;
            lblmessage.BackColor = System.Drawing.Color.White ;
            return;
        }

        tblempbasicinfo.Visible = true ;
        tblpfcontribution.Visible = true;
      

        lblempid.Text = dtemp[0].emp_code;
        lblempname.Text = dtemp[0].emp_name;
        lbldesignation.Text = dtemp[0].emp_designation_name;
        lbldepartment.Text = dtemp[0].Isemp_section_nameNull() ? "" : dtemp[0].emp_section_name;
        lbljoindate.Text = dtemp[0].emp_join_date.ToShortDateString();
        job_length();


        sp_pf_contribution_openingTableAdapter pfcon = new sp_pf_contribution_openingTableAdapter();
        dsTransaction.sp_pf_contribution_openingDataTable dtpfcon = new dsTransaction.sp_pf_contribution_openingDataTable();

        dtpfcon = pfcon.GetData(lblempid.Text, dtsettdate.SelectedDate);
        if (dtpfcon.Rows.Count == 0)
        {
            tblempbasicinfo.Visible = false;
            tblpfcontribution.Visible = false;
            return;
        }

        lblowncont.Text = dtpfcon[0].own_cont.ToString("N2");
        lblempcont.Text = dtpfcon[0].emp_cont.ToString("N2");

        lblownprofit.Text = dtpfcon[0].own_profit.ToString("N2");
        lblcomprofit.Text = dtpfcon[0].emp_profit.ToString("N2");
        

        Hrms_pf_sat_setupTableAdapter payratio = new Hrms_pf_sat_setupTableAdapter();
        dsMasterData.Hrms_pf_sat_setupDataTable dtpayratio = new dsMasterData.Hrms_pf_sat_setupDataTable();
        dtpayratio =  payratio.GetDataByMonth(lbljoblength.Text);


        if (dtpayratio.Rows.Count == 0)
        {
            tblempbasicinfo.Visible = false;
            tblpfcontribution.Visible = false;
            return;
        }



        lblownpayratio.Text = dtpayratio[0].own_cont_ratio.ToString("N2");
        lblcompayratio.Text = dtpayratio[0].employer_cont_ratio.ToString("N2");
        lblownpayratiopro.Text = dtpayratio[0].own_profit_ratio.ToString("N2");
        lblcompayratiopro.Text = dtpayratio[0].employer_profit_ratio.ToString("N2");

        getpayable();
        paid();
        loaddue();
        SetTxtchange();
        gettotal();

    }


    private void clearfields()
    {
        txtownadjustment.Text = "0";
        txtownactualpay.Text = "0";
        txtcomnadjustment.Text = "0";
        txtcomactualpay.Text = "0";
        txtownadjustmentpro.Text = "0";
        txtownactualpaypro.Text = "0";
        txtcomadjustmentpro.Text = "0";
        txtcomactualpaypro.Text = "0";      
    }

    private void getpayable()
    {

        lblownpayable.Text = (Convert.ToDecimal(lblowncont.Text) * Convert.ToDecimal(lblownpayratio.Text)).ToString("N2");
        lblcompayable.Text = (Convert.ToDecimal(lblempcont.Text) * Convert.ToDecimal(lblcompayratio.Text)).ToString("N2");
        lblownpayablepro.Text = (Convert.ToDecimal(lblownprofit.Text) * Convert.ToDecimal(lblownpayratiopro.Text)).ToString("N2");
        lblcompayablepro.Text = (Convert.ToDecimal(lblcomprofit.Text) * Convert.ToDecimal(lblcompayratiopro.Text)).ToString("N2");
    
    }

    private void gettotal()
    {

        lbltotdue.Text = (Convert.ToDecimal(lblowndue.Text) + Convert.ToDecimal(lblcomdue.Text) + Convert.ToDecimal(lblownduepro.Text) + Convert.ToDecimal(lblcomduepro.Text)).ToString("N2");
        lbltotadj.Text = (Convert.ToDecimal(txtownadjustment.Text) + Convert.ToDecimal(txtcomnadjustment.Text) + Convert.ToDecimal(txtownadjustmentpro.Text) + Convert.ToDecimal(txtcomadjustmentpro.Text)).ToString("N2");
        lbltotpay.Text = (Convert.ToDecimal(txtownactualpay.Text) + Convert.ToDecimal(txtcomactualpay.Text) + Convert.ToDecimal(txtownactualpaypro.Text) + Convert.ToDecimal(txtcomactualpaypro.Text)).ToString("N2");
        lbltotbal.Text = (Convert.ToDecimal(lblownbalance.Text) + Convert.ToDecimal(lblcombalance.Text) + Convert.ToDecimal(lblownbalancepro.Text) + Convert.ToDecimal(lblcombalancepro.Text)).ToString("N2");
    }

    private void gettotaladjustment()
    {
        lblownpayable.Text = (Convert.ToDecimal(lblowncont.Text) * Convert.ToDecimal(lblownpayratio.Text)).ToString("N2");
        lblcompayable.Text = (Convert.ToDecimal(lblempcont.Text) * Convert.ToDecimal(lblcompayratio.Text)).ToString("N2");
        lblownpayablepro.Text = (Convert.ToDecimal(lblownprofit.Text) * Convert.ToDecimal(lblownpayratiopro.Text)).ToString("N2");
        lblcompayablepro.Text = (Convert.ToDecimal(lblcomprofit.Text) * Convert.ToDecimal(lblcompayratiopro.Text)).ToString("N2");
        lbltotdue.Text = (Convert.ToDecimal(lblownpayable.Text) + Convert.ToDecimal(lblcompayable.Text) + Convert.ToDecimal(lblownpayablepro.Text) + Convert.ToDecimal(lblcompayablepro.Text)).ToString("N2");
    }


    private void paid()
    {
        Hrms_sat_detTableAdapter paid = new Hrms_sat_detTableAdapter();
        dsTransaction.Hrms_sat_detDataTable dtpaid = new dsTransaction.Hrms_sat_detDataTable();

       lblownpaid.Text= Convert.ToString (paid.GetOwnPaid(lblempid.Text, 1));
       lblownpaidpro.Text = Convert.ToString(paid.GetOwnPaid(lblempid.Text, 2));        
       lblcompaid.Text = Convert.ToString(paid.GetComPayment(lblempid.Text, 1));
       lblcompaidpro.Text = Convert.ToString(paid.GetComPayment(lblempid.Text, 2));
       
    }

    private void loaddue()
    {
        lblowndue.Text = (Convert.ToDecimal(lblownpayable.Text) - Convert.ToDecimal(lblownpaid.Text)).ToString("N2");
        lblcomdue.Text = (Convert.ToDecimal(lblcompayable.Text) - Convert.ToDecimal(lblcompaid.Text)).ToString("N2");
        lblownduepro.Text = (Convert.ToDecimal(lblownpayablepro.Text) - Convert.ToDecimal(lblownpaidpro.Text)).ToString("N2");
        lblcomduepro.Text = (Convert.ToDecimal(lblcompayablepro.Text) - Convert.ToDecimal(lblcompaidpro.Text)).ToString("N2");
    }


    protected void btnstatement_Click(object sender, EventArgs e)
    {
        LibraryDTO.clsReport rpt = new LibraryDTO.clsReport();
        ParameterFields myParams = new ParameterFields();
        parameterpass(myParams, "@st_date", Convert.ToDateTime(lbljoindate.Text).ToShortDateString());
        parameterpass(myParams, "@end_date", dtsettdate.SelectedDate.ToShortDateString());
        parameterpass(myParams, "@emp_code", lblempid.Text);
        parameterpass(myParams, "period", "Period : " + Convert.ToDateTime(lbljoindate.Text).ToShortDateString() + " To " + dtsettdate.SelectedDate.ToShortDateString());
        parameterpass(myParams, "CompanyName", current.CompanyName);
        parameterpass(myParams, "CompanyAddress", current.CompanyAddress);
        rpt.ParametersFields = myParams;
        rpt.PageZoomFactor = 100;
        rpt.FileName = "reports/rpt_pf_statement.rpt";
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
    protected void txtownadjustment_TextChanged(object sender, EventArgs e)
    {
        SetTxtchange();
    }
    protected void txtownactualpay_TextChanged(object sender, EventArgs e)
    {
        SetTxtchange();
    }
    protected void txtcomnadjustment_TextChanged(object sender, EventArgs e)
    {
        SetTxtchange();
    }
    protected void txtcomactualpay_TextChanged(object sender, EventArgs e)
    {
        SetTxtchange();
    }
    protected void txtownadjustmentpro_TextChanged(object sender, EventArgs e)
    {
        SetTxtchange();
    }
    protected void txtownactualpaypro_TextChanged(object sender, EventArgs e)
    {
        SetTxtchange();
    }
    protected void txtcomadjustmentpro_TextChanged(object sender, EventArgs e)
    {
        SetTxtchange();
    }
    protected void txtcomactualpaypro_TextChanged(object sender, EventArgs e)
    {
        SetTxtchange();
    }


    private void SetTxtchange()
    {

        decimal c_o_ad, c_o_ac, c_c_ad, c_c_ac, p_o_ad, p_o_ac, p_c_ad, p_c_ac;

        try
        {
            c_o_ad = Convert.ToDecimal(txtownadjustment.Text == "" ? "0" : txtownadjustment.Text);
            c_o_ac = Convert.ToDecimal(txtownactualpay.Text == "" ? "0" : txtownactualpay.Text);

            c_c_ad = Convert.ToDecimal(txtcomnadjustment.Text == "" ? "0" : txtcomnadjustment.Text);
            c_c_ac = Convert.ToDecimal(txtcomactualpay.Text == "" ? "0" : txtcomactualpay.Text);

            p_o_ad = Convert.ToDecimal(txtownadjustmentpro.Text == "" ? "0" : txtownadjustmentpro.Text);
            p_o_ac = Convert.ToDecimal(txtownactualpaypro.Text == "" ? "0" : txtownactualpaypro.Text);

            p_c_ad = Convert.ToDecimal(txtcomadjustmentpro.Text == "" ? "0" : txtcomadjustmentpro.Text);
            p_c_ac = Convert.ToDecimal(txtcomactualpaypro.Text == "" ? "0" : txtcomactualpaypro.Text);


            lblownbalance.Text = (Convert.ToDecimal(lblowndue.Text) - c_o_ad - c_o_ac).ToString();
            lblcombalance.Text = (Convert.ToDecimal(lblcomdue.Text) - c_c_ad - c_c_ac).ToString();
            lblownbalancepro.Text = (Convert.ToDecimal(lblownduepro.Text) - p_o_ad - p_o_ac).ToString();
            lblcombalancepro.Text = (Convert.ToDecimal(lblcomduepro.Text) - p_c_ad - p_c_ac).ToString();

            gettotal();

        }
        catch 
        {
        }
      
    }
    protected void dtsettdate_DateChanged(object sender, EventArgs e)
    {
        clearfields();
        SetTxtchange();
    }
    protected void btnsettlement_Click(object sender, EventArgs e)
    {
        bool updflg = false;

        Hrms_sat_hdrTableAdapter setthdr = new Hrms_sat_hdrTableAdapter();
        dsTransaction.Hrms_sat_hdrDataTable settdthdr = new dsTransaction.Hrms_sat_hdrDataTable();
        Hrms_sat_detTableAdapter settdet = new Hrms_sat_detTableAdapter();
        dsTransaction.Hrms_sat_detDataTable settdtdet = new dsTransaction.Hrms_sat_detDataTable();
    
        string ref_no = "SAT" +dtsettdate.SelectedDate.Year.ToString()+ string.Format("{0:00}",dtsettdate.SelectedDate.Month).ToString()+string.Format("{0:00}",dtsettdate.SelectedDate.Day).ToString()+"-"+ lblempid.Text.ToString();

        SqlTransaction myTrans = HelperTA.OpenTransaction(setthdr.Connection);
        try
        {
            setthdr.AttachTransaction(myTrans);
            settdet.AttachTransaction(myTrans);

            settdthdr = setthdr.GetDataByRef(ref_no);
            if (settdthdr.Rows.Count > 0)
            {
                setthdr.DeleteDataHdr(ref_no);
                settdet.DeleteDataDet(ref_no);
            }


            setthdr.InsertHdr(ref_no, "", lblempid.Text, dtsettdate.SelectedDate,0, 0,Convert.ToDecimal(lbltotdue.Text), Convert.ToDecimal(lbltotadj.Text), Convert.ToDecimal(lbltotpay.Text), Convert.ToDecimal(lbltotbal.Text));
            settdet.InsertDet(ref_no, lblempid.Text, 1, "Total Contribution", Convert.ToDecimal(lblowncont.Text), Convert.ToDecimal(lblempcont.Text), Convert.ToDouble(lblownpayratio.Text), Convert.ToDouble(lblcompayratio.Text), Convert.ToDecimal(txtownactualpay.Text), Convert.ToDecimal(txtcomactualpay.Text), Convert.ToDecimal(lblownbalance.Text), Convert.ToDecimal(lblcombalance.Text));
            settdet.InsertDet(ref_no, lblempid.Text, 2, "Total Profit", Convert.ToDecimal(lblownprofit.Text), Convert.ToDecimal(lblcomprofit.Text), Convert.ToDouble(lblownpayratiopro.Text), Convert.ToDouble(lblcompayratiopro.Text), Convert.ToDecimal(txtownactualpaypro.Text), Convert.ToDecimal(txtcomactualpaypro.Text), Convert.ToDecimal(lblownbalancepro.Text), Convert.ToDecimal(lblcombalancepro.Text));
          
            myTrans.Commit();
            //Response.Redirect(Request.Url.AbsoluteUri);

            updflg = true;

        }

        catch (Exception ex)
        {
            myTrans.Rollback();
        }

        finally
        {

            HelperTA.CloseTransaction(setthdr.Connection, myTrans);
        }

        if (updflg)
            Response.Redirect(Request.Url.AbsoluteUri);

      
    }
}
