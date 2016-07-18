using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibraryDAL;
using LibraryDAL.SCBLDataSetTableAdapters;
using LibraryDAL.SCBL2DataSetTableAdapters;

public partial class ClientSide_modules_commercial_usercontrols_ctl_fpi_mpr_item_view : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
      
    }

    public void load_all( string mpr_ref, string itm_code)
    {
        SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable itm = new SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable();
        PuTr_IN_Det_Scbl2TableAdapter det = new PuTr_IN_Det_Scbl2TableAdapter();
        SCBLDataSet.PuTr_IN_Hdr_ScblRow drhdr;
        PuTr_IN_Hdr_ScblTableAdapter hdr = new PuTr_IN_Hdr_ScblTableAdapter();
        itm = det.GetDataByRefItem(mpr_ref, itm_code);
        if (itm.Rows.Count == 0) { clear_all(); return; }
        drhdr = hdr.GetDataByRef(mpr_ref)[0];
        lbl_MPR_ref.Text = mpr_ref;
        lbl_MPR_datetime.Text = drhdr.IN_Hdr_St_DATE.ToString();
        lbl_item_details.Text = itm[0].IN_Det_Icode + ":" + itm[0].IN_Det_Itm_Desc;
        lbl_quantity.Text = itm[0].IN_Det_Lin_Qty.ToString("N2") + " " + itm[0].IN_Det_Itm_Uom;
        lbl_specification.Text = itm[0].In_Det_Specification;
        lbl_brand.Text = itm[0].In_Det_Brand;
        lbl_origin.Text = itm[0].In_Det_Origin;
        lbl_packing.Text = itm[0].In_Det_Packing;
        lbl_ETR.Text = itm[0].IN_Det_Exp_Dat.ToShortDateString();
        lbl_remarks.Text = itm[0].In_Det_Remarks.ToString();
    }
    

    private void clear_all()
    { 
    }
    
}