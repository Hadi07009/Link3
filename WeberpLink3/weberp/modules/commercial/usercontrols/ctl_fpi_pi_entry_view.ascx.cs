using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibraryDAL.FpiDataSetTableAdapters;

public partial class ClientSide_modules_commercial_usercontrols_ctl_fpi_pi_entry_view : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

       

    }


    public void load_details(string ref_no)
    {

        tbl_fpi_approval_dataTableAdapter app = new tbl_fpi_approval_dataTableAdapter();
        LibraryDAL.FpiDataSet.tbl_fpi_approval_dataDataTable dt_app = new LibraryDAL.FpiDataSet.tbl_fpi_approval_dataDataTable();


        dt_app = app.GetDataByRef(ref_no);
        if (dt_app.Rows.Count < 0)
        {
            return;
        }
        else
        {
            lbl_ref_no.Text = dt_app[0].ref_no;
            lbl_user_name.Text = dt_app[0].user_name;
            lbl_date.Text = dt_app[0].current_datetime.ToShortDateString();
            lbl_expense_nature.Text = dt_app[0].nature_of_expense;
            lbl_location.Text = dt_app[0].location;
            lb_lPI_no.Text = dt_app[0].pi_no;
            lbl_date_PI.Text = dt_app[0].pi_date.ToShortDateString();
            lbl_supp.Text = dt_app[0].supplier;
            lbl_item_det.Text = dt_app[0].Item_details;
            lbl_origin.Text = dt_app[0].origin;
            lbl_quantity.Text = dt_app[0].quantity.ToString();
            lbl_unit.Text = dt_app[0].unit;
            lbl_currency.Text = dt_app[0].currency_unit_rate;
            lbl_unit_rate.Text = dt_app[0].unit_rate.ToString();
            lbl_total_value.Text = dt_app[0].total_value.ToString();
            lbl_currency_total_value.Text = dt_app[0].currency_total_value;
            lbl_payment.Text = dt_app[0].payment_terms;
            lbl_shipment.Text = dt_app[0].last_date_of_shipment.ToShortDateString();
            lbl_LC_validity_date.Text = dt_app[0].lc_validity_date.ToShortDateString();
            lbl_port_of_loading.Text = dt_app[0].port_of_loading;
            lbl_portof_discharge.Text = dt_app[0].port_of_discharge;
            lbl_arrival.Text = dt_app[0].expected_arrival_time.ToShortDateString();
            lbl_CF_agent.Text = dt_app[0].name_of_cf_agent;
            lbl_transport_contact.Text = dt_app[0].transport_contractor;
            lbl_mode_of_transport.Text = dt_app[0].mode_of_transport;
            lbl_stock_in_hand.Text = dt_app[0].stock_in_hand;
            lbl_LC_details.Text = dt_app[0].lc_details_pipeline;
            lbl_total_stock.Text = dt_app[0].Istotal_stock_with_pipelineNull() ? "" : dt_app[0].total_stock_with_pipeline;

            lbl_production_period.Text = dt_app[0].period_coverage_period.ToShortDateString();
            lbl_unit_rate_previuos.Text = dt_app[0].previous_unit_rate;
            lbl_date_previous_date.Text = dt_app[0].date_unit_rate_previous.ToShortDateString();

        }

    
    
    }

    private void clear_all()
    { 
    
    
    }
}