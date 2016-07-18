using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using LibraryDAL;
using LibraryDAL.SCBLDataSetTableAdapters;

public partial class frm_po_status : System.Web.UI.Page
{  

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";       

        if (!Page.IsPostBack)
        {
            get_all_po();
        }
        else
        {

        }
    }


    private void get_all_po()
    {       
                
        
        PuTr_PO_Hdr_ScblTableAdapter hdr = new PuTr_PO_Hdr_ScblTableAdapter();
        SCBLDataSet.PuTr_PO_Hdr_ScblDataTable dt = new SCBLDataSet.PuTr_PO_Hdr_ScblDataTable();

        dt = hdr.GetDistinctStatus();

        ListItem itm;
        ddlpolist.Items.Clear();
        ddlpolist.Items.Add("");
        ddlpolist.Items.Add("ALL");

        foreach (SCBLDataSet.PuTr_PO_Hdr_ScblRow dr in dt.Rows)
        {
            itm = new ListItem();
            itm.Text = dr.PO_Hdr_Status.ToString();
            itm.Value = dr.PO_Hdr_Status.ToString();
            ddlpolist.Items.Add(itm);

        }
             
               
 
    }

    private void load_all_po(string status)
    {
        PuTr_PO_Hdr_ScblTableAdapter hdr = new PuTr_PO_Hdr_ScblTableAdapter();
        SCBLDataSet.PuTr_PO_Hdr_ScblDataTable dthdr = new SCBLDataSet.PuTr_PO_Hdr_ScblDataTable();
        PuTr_PO_Det_ScblTableAdapter det = new PuTr_PO_Det_ScblTableAdapter();
        SCBLDataSet.PuTr_PO_Det_ScblDataTable dtdet;
        DataTable dttemp = new DataTable();
                

        dttemp.Columns.Add("Ref", typeof(string));
        dttemp.Columns.Add("Req", typeof(string));
        dttemp.Columns.Add("Status", typeof(string));
        dttemp.Columns.Add("Icode", typeof(string));
        dttemp.Columns.Add("Idet", typeof(string));
        dttemp.Columns.Add("PO Qty", typeof(string));
        dttemp.Columns.Add("Rec Qty", typeof(string));
        dttemp.Columns.Add("Ins Qty", typeof(string));
        dttemp.Columns.Add("Bal Qty", typeof(string));
        dttemp.Columns.Add("Rate", typeof(string));
        dttemp.Columns.Add("Amount", typeof(string));


        if(status=="ALL")
            dthdr = hdr.GetData();
        else
            dthdr = hdr.GetDataByStatus(status);

        foreach (SCBLDataSet.PuTr_PO_Hdr_ScblRow drhdr in dthdr.Rows)
        {
            dtdet = new SCBLDataSet.PuTr_PO_Det_ScblDataTable();
            dtdet = det.GetDetByRef(drhdr.PO_Hdr_Ref.ToString());
            string sts = drhdr.PO_Hdr_Status.ToString();
            foreach (SCBLDataSet.PuTr_PO_Det_ScblRow dr in dtdet.Rows)
            {
                dttemp.Rows.Add(dr.PO_Det_Ref, dr.PO_Det_Code, sts, dr.PO_Det_Icode,dr.PO_Det_Itm_Desc,dr.PO_Det_Lin_Qty.ToString("N2"),dr.PO_Det_Org_QTY.ToString("N2"),dr.PO_Det_Ins_QTY.ToString("N2"),dr.PO_Det_Bal_Qty.ToString("N2"),dr.PO_Det_Lin_Rat.ToString("N2"),dr.PO_Det_Lin_Amt.ToString("N2"));
            }
        }

        gdItem.DataSource = dttemp;
        gdItem.DataBind();
              
        
    }
     
     

    protected void ddlpolist_SelectedIndexChanged(object sender, EventArgs e)
    {
       string selval = ddlpolist.SelectedValue.ToString();

       switch (selval)
       {
           case "":
               gdItem.Visible = false;
               break;

           default:
               gdItem.Visible = true;
               load_all_po(selval);
               break;
       }
    }
}
