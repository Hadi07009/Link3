using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibraryDAL.FpiDataSetTableAdapters;

public partial class ClientSide_modules_commercial_usercontrols_ctl_fpi_lc_entry_view : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void load_data( string ref_no)
    {
        tbl_fpi_lc_infoTableAdapter lcinfo = new tbl_fpi_lc_infoTableAdapter();
        LibraryDAL.FpiDataSet.tbl_fpi_lc_infoDataTable dtlcinfo = new LibraryDAL.FpiDataSet.tbl_fpi_lc_infoDataTable();
        dtlcinfo = lcinfo.GetDataByRef(ref_no);
        if (dtlcinfo.Rows.Count == 0) return;
        else
        {
            lbl_LC_bank_name.Text = dtlcinfo[0].bank_name;
          lbl_branch_name.Text = dtlcinfo[0].bank_branch_name;
          lbl_LC_opening_margin.Text = dtlcinfo[0].lc_opening_margin.ToString("N2");
          lbl_LC_cash_margin.Text = dtlcinfo[0].lc_cash_margin.ToString("N2");
          lbl_LC_fdr_margin.Text = dtlcinfo[0].lc_fdr_margin.ToString("N2");
          lbl_currency_opening_margin.Text = dtlcinfo[0].currency_lc_opening_margin;
          lbl_currency_cash_margin.Text = dtlcinfo[0].currency_lc_cash_margin;
          lbl_currency_fdr_margin.Text = dtlcinfo[0].currency_fdr_margin;
          lbl_insurance_company.Text = dtlcinfo[0].insurance_company_name;
          lbl_insurance_branch.Text = dtlcinfo[0].insurance_branch_name;
          lbl_insurance_total_amt.Text = dtlcinfo[0].insurance_total_amt.ToString("N2");
          lbl_premimum_amt.Text = dtlcinfo[0].premium_amt.ToString("N2");
          lbl_vat_amt.Text = dtlcinfo[0].vat_amt.ToString("N2");
          lbl_stamp_amt.Text = dtlcinfo[0].stamp_duty_amt.ToString("N2");
          lbl_currency_total_amt.Text = dtlcinfo[0].currency_insurance_total_amt;
          lbl_currency_premium_amt.Text = dtlcinfo[0].currency_premium_amt;
          lbl_currency_vat_amt.Text = dtlcinfo[0].currency_vat_amt;
          lbl_currency_stamp_amt.Text = dtlcinfo[0].currency_stamp_duty_amt;
          if (dtlcinfo[0].lc_close_status == "Y")
          {
              lbl_close_status.Text = "YES (" + dtlcinfo[0].lc_close_jv+")";
          }
          else
          {
              lbl_close_status.Text = "NO";
          }

        }

    }

}