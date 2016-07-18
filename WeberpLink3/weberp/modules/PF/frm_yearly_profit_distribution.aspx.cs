using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using CrystalDecisions;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Web.UI.HtmlControls;
using System.Globalization;
using LibraryPF.dsTransactionTableAdapters;
using System.Data.SqlClient;
using LibraryPF;

public partial class frm_yearly_profit_distribution : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        clsStatic.MsgConfirmBox(btnpostdistribution, "Are you sure to post ? ");
        clsStatic.MsgConfirmBox(btnsavedistribution, "Are you sure to save ? ");
       
        if (Page.IsPostBack == false)
        {        
           // dtlastprofitdistribution.Enabled = false;
            load_last_distribution_date();

            txtdistributionrate.Text = "1";

            //btnpostdistribution.Visible = buttonvisibility();           

            dtdistributionupto.SelectedDate = dtlastprofitdistribution.SelectedDate.AddYears(1);
            
            if (current.PermissionPrm.IndexOf("Update") != -1) btnpostdistribution.Visible = true;
        }
       
    }


   
 
    private bool buttonvisibility()
    {

    tbl_emp_profit_dist_saveTableAdapter profit = new tbl_emp_profit_dist_saveTableAdapter();

     bool   flg = false;

     if (profit.GetData().Rows.Count > 0)
     {
         flg = true;
     }

        return flg ;
    }

    private void load_last_distribution_date()
    {

        hrms_pf_distributionTableAdapter dis = new hrms_pf_distributionTableAdapter();
        dtlastprofitdistribution.SelectedDate=Convert.ToDateTime(dis.GetLastDate());

    }

    protected void btnsavedistribution_Click(object sender, EventArgs e)
    {
        lblmessage.Text = "";
        lbltotmonthinvestment.Text = "";
        lbltotdistributionprofit.Text = "";


        if (check_entry() != "")
        {          
            MessageBox1.ShowWarning(check_entry());
            return;
        }

        sp_emp_distribution_listTableAdapter emp = new sp_emp_distribution_listTableAdapter();
        dsTransaction.sp_emp_distribution_listDataTable dtemp = new dsTransaction.sp_emp_distribution_listDataTable();

        tbl_emp_profit_dist_saveTableAdapter profit = new tbl_emp_profit_dist_saveTableAdapter();

        view_Hrms_Pf_Transfer_grpTableAdapter pftrans = new view_Hrms_Pf_Transfer_grpTableAdapter();
        dsTransaction.view_Hrms_Pf_Transfer_grpDataTable dtpftrans;


        DateTime dtst = DateTime.Now;

        DateTime last_dis = dtlastprofitdistribution.SelectedDate;
        DateTime cur_dis = dtdistributionupto.SelectedDate;

        decimal distribution_rate, profit_rate, tot_profit;

        profit_rate = Convert.ToDecimal(txtprofit.Text);

        distribution_rate = Convert.ToDecimal(txtdistributionrate.Text);
        

        tot_profit = distribution_rate * profit_rate;

        txtdistributionamt.Text = tot_profit.ToString("N2");

        decimal pro_ratio=0;

        profit.DeleteAllData();
        dtemp = emp.GetData(last_dis);
        int nom = 0;

        bool flg = false;
        foreach (dsTransaction.sp_emp_distribution_listRow dr in dtemp.Rows)
        {
            dtpftrans = new dsTransaction.view_Hrms_Pf_Transfer_grpDataTable();
            dtpftrans = pftrans.GetDataByEmpDate(dr.emp_code, last_dis, cur_dis);

            if ((dtpftrans.Rows.Count == 0) && ((dr.own_opening + dr.emp_opening) == 0)) { goto next_empl; }

            if ((dr.own_opening + dr.emp_opening) > 0)
            {
                profit.InsertProfitSave(dr.emp_code, "Opening", last_dis, dr.own_opening, dr.emp_opening, (dr.own_opening + dr.emp_opening), 12, (12 * dr.own_opening), (12 * dr.emp_opening), (12 * (dr.own_opening + dr.emp_opening)), 0, 0, 0, 0);
            }

            foreach (dsTransaction.view_Hrms_Pf_Transfer_grpRow drtrn in dtpftrans.Rows)
            {
                if (cur_dis <= drtrn.PF_date)
                {
                    nom = 0;

                }
                else if (cur_dis.Year == drtrn.PF_date.Year)
                {
                    nom = cur_dis.Month - drtrn.PF_date.Month;
                }
                else
                {
                    nom = cur_dis.Month - drtrn.PF_date.Month + 12;
                }

                if (nom > 0)
                {
                    profit.InsertProfitSave(dr.emp_code, "Monthly", drtrn.PF_date, drtrn.PFEC, drtrn.PFEMP, drtrn.PFFD, nom, (nom * drtrn.PFEC), (nom * drtrn.PFEMP), (nom * drtrn.PFFD), 0, 0, 0, 0);
                }
            }

            flg = true;

        next_empl: ;
        }

        decimal tot_invest = Convert.ToDecimal(profit.GetTotInvest());

        if (tot_invest != 0)
        {
            pro_ratio = (tot_profit / tot_invest);
        }
        profit.UpdateProfitRatio(pro_ratio);
        lbltotmonthinvestment.Text = "Total month investment : " + tot_invest.ToString("N2");
        lbltotdistributionprofit.Text = "Total distributed profit : " + Convert.ToDecimal(profit.GetTotProfit()).ToString("N2");

        if (flg == true)
        {
            lblmessage.Text = "Data Saved Successfully";
            lblmessage.ForeColor = System.Drawing.Color.Blue;
            btnpostdistribution.Visible = buttonvisibility(); 
        }

        else
        {
            lblmessage.Text = "No Data for Saving";
            lblmessage.ForeColor = System.Drawing.Color.Red;
        
        }
       
       
    }

    private string  check_entry()
    {
        if (txtprofit.Text == "") return "Enter profit";
        if (txtdistributionrate.Text == "") return "Enter distribution rate";      
        if (Convert.ToDecimal (txtdistributionrate.Text)>1) return " Distribution rate must be less then 1";
        return "";
        
    }
    protected void btnpostdistribution_Click(object sender, EventArgs e)
    {

        if (txtprofit.Text == "" || txtdistributionrate.Text == "" || txtdistributionamt.Text == "")
        {
            MessageBox1.ShowWarning("Please save distribution then post.");
            return;
        }

        if (lbltotmonthinvestment.Text == "")
        {
            MessageBox1.ShowWarning("Please save distribution then post.");
            return;
        }


        hrms_pf_Dis_HDRTableAdapter dishdr = new hrms_pf_Dis_HDRTableAdapter();
        hrms_pf_distributionTableAdapter disdet = new hrms_pf_distributionTableAdapter();

        tbl_emp_profit_dist_saveTableAdapter profit = new tbl_emp_profit_dist_saveTableAdapter();
        dsTransaction.tbl_emp_profit_dist_saveDataTable dtprofit = new dsTransaction.tbl_emp_profit_dist_saveDataTable();

        DateTime cur_dis = dtdistributionupto.SelectedDate;
        decimal tot_profit = Convert.ToDecimal(txtdistributionamt.Text.Trim());


        


        int max_sl = Convert.ToInt32(dishdr.GetMaxSl());

        string ref_no = "DIS" + cur_dis.Year.ToString() + string.Format("{0:00}", cur_dis.Month) + "-" + string.Format("{0:00000}", max_sl);

        dtprofit = profit.GetDataByGroupBy();


        SqlTransaction myTrans = HelperTA.OpenTransaction(dishdr.Connection);
        try
        {
            dishdr.AttachTransaction(myTrans);
            disdet.AttachTransaction(myTrans);
            profit.AttachTransaction(myTrans);

            double pro_ratio = Convert.ToDouble(dtprofit[0].profit_ratio);

            foreach (dsTransaction.tbl_emp_profit_dist_saveRow dr in dtprofit.Rows)
            {
                disdet.InsertDet(ref_no, "Yearly Final", dr.emp_code, cur_dis, dr.own_profit, dr.emp_profit);
            }

            dishdr.InsertHdr(ref_no, cur_dis, tot_profit, 100, pro_ratio, tot_profit);
            profit.DeleteAllData();
            myTrans.Commit();
        }

        catch(Exception ex)
        {
            myTrans.Rollback();
            lblmessage.Text = ex.Message;
            return;    
        }

        finally
        {

            HelperTA.CloseTransaction(dishdr.Connection, myTrans);
        }

        Response.Redirect(Request.Url.AbsoluteUri);
    
    }
    protected void btnviewreport_Click(object sender, EventArgs e)
    {
        ParameterFields myParams = new ParameterFields();
        LibraryDTO.clsReport rpt = new LibraryDTO.clsReport();       
        rpt.FileName = "reports/rpt_profit_distribution_save.rpt";
        parameterpass(myParams, "CompanyName", current.CompanyName);
        parameterpass(myParams, "CompanyAddress", current.CompanyAddress);
        rpt.ParametersFields = myParams;
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


    private void getdistributedamount()
    {

        decimal distributed_rate, profit;
        distributed_rate = txtdistributionrate.Text == "" ? 0 : Convert.ToDecimal(txtdistributionrate.Text);
        profit = txtprofit.Text == "" ? 0 : Convert.ToDecimal(txtprofit.Text);
        txtdistributionamt.Text = (distributed_rate * profit).ToString("N2");
    
    
    }
    protected void txtprofit_TextChanged(object sender, EventArgs e)
    {
        getdistributedamount();
    }
    protected void txtdistributionrate_TextChanged(object sender, EventArgs e)
    {
        getdistributedamount();
    }
}
