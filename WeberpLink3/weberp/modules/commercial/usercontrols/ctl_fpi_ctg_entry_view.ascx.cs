using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibraryDAL.FpiDataSetTableAdapters;
using LibraryDAL;

public partial class ClientSide_modules_commercial_usercontrols_ctl_fpi_ctg_entry_view : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    public void load_all(string ref_no,int con_no)
    {
        tbl_fpi_consignment_infoTableAdapter con = new tbl_fpi_consignment_infoTableAdapter();
        FpiDataSet.tbl_fpi_consignment_infoDataTable dtcon = new FpiDataSet.tbl_fpi_consignment_infoDataTable();

        tbl_fpi_cargo_landing_hdrTableAdapter cargo_hdr = new tbl_fpi_cargo_landing_hdrTableAdapter();
        FpiDataSet.tbl_fpi_cargo_landing_hdrDataTable dtcargo_hdr = new FpiDataSet.tbl_fpi_cargo_landing_hdrDataTable();

        tbl_fpi_cargo_landing_detTableAdapter det = new tbl_fpi_cargo_landing_detTableAdapter();
        FpiDataSet.tbl_fpi_cargo_landing_detDataTable dtdet = new FpiDataSet.tbl_fpi_cargo_landing_detDataTable();


        dtcargo_hdr = cargo_hdr.GetDataByRefCon(ref_no, con_no);
        dtdet = det.GetDataByRefCon(ref_no, con_no);

        if (dtcargo_hdr.Rows.Count == 0)
        {
            return;

        }

        else
        {

            lbl_ref_no.Text = dtcargo_hdr[0].ref_no;
            lbl_consign_number.Text = dtcargo_hdr[0].consign_number.ToString();
            lbl_balance_quantity.Text = dtcargo_hdr[0].balance_qty.ToString();
            lbl_mother_vessel_name.Text = dtcargo_hdr[0].mother_vessel_name;
            lbl_mother_vessel_origin.Text = dtcargo_hdr[0].mother_vessel_origin;
            lbl_import_rotation_no.Text = dtcargo_hdr[0].Import_rotation_no;
            lbl_arrival_date.Text = dtcargo_hdr[0].arrival_date.ToShortDateString();
            lbl_date_of_landing.Text = dtcargo_hdr[0].date_of_landing.ToShortDateString();
            lbl_unit_of_quantity.Text = dtcargo_hdr[0].unit_of_quantity;
            lbl_completion_date.Text  = dtcargo_hdr[0].loading_completion_date.ToShortDateString();
            lbl_mode_of_transport.Text = dtcargo_hdr[0].mode_of_transport;
            lbl_carrier_service.Text = dtcargo_hdr[0].carrier_service;

          

            decimal mrr_qty = 0, given_qty = 0, refund_qty = 0;

            foreach (FpiDataSet.tbl_fpi_cargo_landing_detRow dr in dtdet.Rows)
            {
                switch (dr.rm_uses_type)
                {
                    case "Self Use":
                        mrr_qty += dr.survay_quantity;
                        break;

                    case "Loan Given":
                        given_qty += dr.survay_quantity;
                        break;

                    case "Loan Refund":
                        refund_qty += dr.survay_quantity;
                        break;

                    default:
                        break;

                }
            }

            lbl_mrr_qty.Text = mrr_qty.ToString("N2");
            lbl_loan_given.Text = given_qty.ToString("N2");
            lbl_loan_refund.Text = refund_qty.ToString("N2");

            lbl_landed_quantity.Text = (mrr_qty + given_qty + refund_qty).ToString();  //dtcargo_hdr[0].landed_quantity.ToString();

            dtcon = con.GetDataByRefCon(dtcargo_hdr[0].ref_no, dtcargo_hdr[0].consign_number);



            if ((mrr_qty + given_qty + refund_qty - dtcon[0].bill_of_lading_quantity) > 0)
            {
                lbl_landed_quantity_short.Text = (mrr_qty + given_qty + refund_qty - dtcon[0].bill_of_lading_quantity).ToString() + "(Excess)";
            }
            else if ((mrr_qty + given_qty + refund_qty - dtcon[0].bill_of_lading_quantity) < 0)
            {
                lbl_landed_quantity_short.Text = (dtcon[0].bill_of_lading_quantity - mrr_qty -  given_qty - refund_qty).ToString() + "(Short)";
            }
            else
            {
                lbl_landed_quantity_short.Text = "Nill";
            }

            
        }

    
    
    }
}