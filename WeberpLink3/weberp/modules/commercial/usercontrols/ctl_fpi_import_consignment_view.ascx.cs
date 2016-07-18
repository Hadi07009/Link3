using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibraryDAL.FpiDataSetTableAdapters;
using LibraryDAL;

public partial class ClientSide_modules_commercial_usercontrols_ctl_fpi_import_consignment_view : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {


    }

    

    public void load_data(string ref_no, int con_no)
    {
        tbl_fpi_consignment_infoTableAdapter con = new tbl_fpi_consignment_infoTableAdapter();
      FpiDataSet.tbl_fpi_consignment_infoDataTable dtcon = new FpiDataSet.tbl_fpi_consignment_infoDataTable();
     

      dtcon = con.GetDataByRefCon(ref_no, con_no);
      if (dtcon.Rows.Count == 0) return;
        else
        {


            lbl_consign_number.Text = dtcon[0].consign_no.ToString();

            lbl_BL_number.Text = dtcon[0].bill_of_lading;
            lbl_bill_of_leading_quantity.Text = dtcon[0].bill_of_lading_quantity.ToString("N2");


            lbl_vessel_name.Text = dtcon[0].Vessel_name;
            lbl_import_rout.Text = dtcon[0].Import_route;
            lbl_import_rotation_no.Text = dtcon[0].import_rotation_number;
            lbl_shipping_agent_charge.Text = dtcon[0].shiping_agent_charge.ToString("N2");
            dt_arrival.Text = dtcon[0].arrival_date.ToShortDateString();
            lbl_shipping_agent_name.Text = dtcon[0].shipping_agent_name;
            lbl_stevedoring.Text = dtcon[0].stevedoring_service_provider;
            lbl_date.Text = dtcon[0].date_bill_of_lading.ToShortDateString();
            lbl_unit.Text = dtcon[0].unit;
            lbl_country.Text = dtcon[0].country;
        }

    }
}