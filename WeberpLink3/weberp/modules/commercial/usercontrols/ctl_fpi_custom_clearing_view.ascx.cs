using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibraryDAL.FpiDataSetTableAdapters;
using LibraryDAL;

public partial class ClientSide_modules_commercial_usercontrols_ctl_fpi_custom_clearing_view : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    public void load_all( string ref_no,int consign_no)
    {

        tbl_fpi_custom_clearingTableAdapter tacustom = new tbl_fpi_custom_clearingTableAdapter();
        FpiDataSet.tbl_fpi_custom_clearingDataTable dtcustom = new FpiDataSet.tbl_fpi_custom_clearingDataTable();

        dtcustom = tacustom.GetDataByRefCon(ref_no, consign_no);
        if (dtcustom.Rows.Count == 0)
        {

            return;
        }

        else
        {
            lbl_consign_no.Text = dtcustom[0].consign_number.ToString();
            lbl_consign_qty.Text = dtcustom[0].consign_quantity.ToString("N2");
            lbl_LC_no.Text = dtcustom[0].lc_number;
            lbl_LC_value.Text = dtcustom[0].lc_value.ToString();
            lbl_invoice_value.Text = dtcustom[0].invoice_value.ToString();
            lbl_LC_date.Text = dtcustom[0].lc_date.ToShortDateString();
            lbl_expire_date.Text = dtcustom[0].lc_expire_date.ToShortDateString();
            lbl_shipment_date.Text = dtcustom[0].shipment_date.ToShortDateString();
            lbl_LC_amendment_value.Text = dtcustom[0].lc_amendment_value.ToString("N2");
            lbl_currency_invoice.Text = dtcustom[0].currency.ToString();
            lbl_exchange_rate.Text = dtcustom[0].exchange_rate.ToString("N2");
            lbl_total_value.Text = dtcustom[0].total_value.ToString("N2");
            lbl_payment_terms.Text = dtcustom[0].payment_terms.ToString();
            lbl_maturity_date.Text = dtcustom[0].maturity_date.ToShortDateString();

            lbl_duty.Text = dtcustom[0].duty.ToString("N2");
            lbl_regulatory_duty.Text = dtcustom[0].regular_duty.ToString("N2");
            lbl_SuplimentoryDuty.Text = dtcustom[0].suppliment_duty.ToString("N2");
            lbl_vat.Text = dtcustom[0].vat.ToString("N2");
            lbl_ait.Text = dtcustom[0].ait.ToString("N2");
            lbl_atv.Text = dtcustom[0].atv.ToString("N2");
            lbl_PSI.Text = dtcustom[0].psi.ToString("N2");
            lbl_DF_Vat.Text = dtcustom[0].df_vat.ToString("N2");
            lbl_port_dues.Text = dtcustom[0].port_dues.ToString("N2");
            lbl_stevedoring.Text = dtcustom[0].stevedoring_service_provider.ToString();
            lbl_stevedoring_change.Text = dtcustom[0].stevedoring_charge.ToString("N2");
            lbl_boc.Text = dtcustom[0].boc.ToString("N2");
            lbl_shipping_agent_name.Text = dtcustom[0].shipping_agnt_name.ToString();
            lbl_shipping_agent_charge.Text = dtcustom[0].shipping_agnt_charge.ToString("N2");
            lbl_freight_forward_name.Text = dtcustom[0].freight_forword_name.ToString();
            lbl_NOC_charge.Text = dtcustom[0].noc_charge.ToString("N2");
            lbl_survey_charge.Text = dtcustom[0].surveyor_charge.ToString("N2");
            lbl_CF_rate.Text = dtcustom[0].cf_agnt_fee.ToString("N2");
            lbl_total_fee.Text = dtcustom[0].total_cf_fee.ToString("N2");
            lbl_carrying_rate.Text = dtcustom[0].carrying_rate.ToString("N2");
            lblTotalCarryingCost.Text = dtcustom[0].total_carrying_cost.ToString("N2");
            lblOtherCost.Text = dtcustom[0].other_cost.ToString("N2");
            lbl_reason_for_othe_cost.Text = dtcustom[0].reason_for_other_cost.ToString();
            lbl_total_cost.Text = dtcustom[0].total_cost.ToString("N2");
            lbl_net_cost.Text = dtcustom[0].net_cost.ToString("N2");
            lbl_rate.Text = dtcustom[0].rate_per_quantity.ToString("N2");
            lbl_vat_2.Text = dtcustom[0].vat.ToString("N2");
            lbl_ait_2.Text = dtcustom[0].ait.ToString("N2");
            lbl_atv_2.Text = dtcustom[0].atv.ToString("N2");

            lbl_duty_pro.Text = dtcustom[0].duty_pro.ToString("N2");
            lbl_regulatory_duty_pro.Text = dtcustom[0].regular_duty_pro.ToString("N2");
            lbl_suplimentery_duty_pro.Text = dtcustom[0].suppliment_duty.ToString("N2");
            lbl_vat_pro.Text = dtcustom[0].vat_pro.ToString("N2");
            lbl_ait_pro.Text = dtcustom[0].ait_pro.ToString("N2");
            lbl_atv_pro.Text = dtcustom[0].atv_pro.ToString("N2");
            lbl_PSI_pro.Text = dtcustom[0].psi_pro.ToString("N2");
            lbl_DF_vat_pro.Text = dtcustom[0].df_vat_pro.ToString("N2");
            lbl_port_dues_pro.Text = dtcustom[0].port_dues_pro.ToString("N2");
            lbl_stevedoring_change_pro.Text = dtcustom[0].stevedoring_charge_pro.ToString("N2");
            lbl_boc_pro.Text = dtcustom[0].boc_charge_pro.ToString("N2");
            lbl_shipping_agent_charge_pro.Text = dtcustom[0].shipping_agnt_charge_pro.ToString("N2");
            lbl_NOC_charge_pro.Text = dtcustom[0].noc_charge_pro.ToString("N2");
            lbl_survey_charge_pro.Text = dtcustom[0].surveyor_charge_pro.ToString("N2");
            lbl_CF_rate_pro.Text = dtcustom[0].cf_agnt_fee_pro.ToString();
            lbl_total_fee_Pro.Text = dtcustom[0].total_cf_fee_pro.ToString();
            lblCarryingRatepro.Text = dtcustom[0].carrying_rate_pro.ToString();
            lblTotalCarryingCostPro.Text = dtcustom[0].total_carrying_cost_pro.ToString();
            lbl_other_cost_pro.Text = dtcustom[0].other_cost_pro.ToString();
            lbl_total_cost_pro.Text = dtcustom[0].total_cost_pro.ToString();
            lbl_net_cost_pro.Text = dtcustom[0].net_cost_pro.ToString();
            lbl_rate_pro.Text = dtcustom[0].rate_per_quantity_pro.ToString();
            lbl_vat_2_pro.Text = dtcustom[0].vat_pro.ToString("N2");
            lbl_ait_2_pro.Text = dtcustom[0].ait_pro.ToString("N2");
            lbl_atv_2_pro.Text = dtcustom[0].atv_pro.ToString("N2");
            lbl_comments.Text = dtcustom[0].comments;

        }
    
    
    }
}