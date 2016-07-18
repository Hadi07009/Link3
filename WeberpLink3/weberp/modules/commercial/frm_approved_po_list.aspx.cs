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
using AjaxControlToolkit;

public partial class frm_approved_po_list : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";
        if (!Page.IsPostBack)
        {
            load_all_status();
        }
        else
        {
        }
        get_app_data();
    }


    private void load_all_status()
    {
        PuTr_PO_Hdr_ScblTableAdapter hdr = new PuTr_PO_Hdr_ScblTableAdapter();
        SCBLDataSet.PuTr_PO_Hdr_ScblDataTable dt = new SCBLDataSet.PuTr_PO_Hdr_ScblDataTable();

        dt = hdr.GetDistinctStatus();

        ListItem itm;
        ddlstatus.Items.Clear();
        ddlstatus.Items.Add("");
        foreach(SCBLDataSet.PuTr_PO_Hdr_ScblRow dr in dt.Rows)
        {
            itm = new ListItem();
            itm.Text = dr.PO_Hdr_Status.ToString();
            itm.Value = dr.PO_Hdr_Status.ToString();
            ddlstatus.Items.Add(itm);

        }

    }

    private void generate_detail_data(SCBLDataSet.PuTr_PO_Hdr_ScblDataTable dthdr)
    {        
        tbl_tac_logTableAdapter log = new tbl_tac_logTableAdapter();
        SCBLDataSet.tbl_tac_logDataTable dtlog = new SCBLDataSet.tbl_tac_logDataTable();

        quotation_detTableAdapter quo = new quotation_detTableAdapter();
        SCBLDataSet.quotation_detRow drq;
        PuTr_PO_Hdr_ScblTableAdapter hdr = new PuTr_PO_Hdr_ScblTableAdapter();
        PuTr_PO_Det_ScblTableAdapter det = new PuTr_PO_Det_ScblTableAdapter();
        SCBLDataSet.PuTr_PO_Det_ScblDataTable dtdet = new SCBLDataSet.PuTr_PO_Det_ScblDataTable();
        int cnt = 0;
        int gcnt, scnt, pcnt;
        string tac_ref, genstr, spestr, paystr;
        bool l_first=true;
        celctl.Controls.Clear();
        foreach (SCBLDataSet.PuTr_PO_Hdr_ScblRow hdr_dr in dthdr.Rows)
        {
            ClientSide_modules_commercial_usercontrols_ctl_po_approval ctl = (ClientSide_modules_commercial_usercontrols_ctl_po_approval)LoadControl("./usercontrols/ctl_po_approval.ascx");
            
            //Control ctl = LoadControl("./usercontrols/ctl_po_approval.ascx");

            Label lblhead = (Label)ctl.FindControl("lblhead");            
            Label lblref = (Label)ctl.FindControl("lblref");
            Label lblparty = (Label)ctl.FindControl("lblparty");            
            Label lblamount = (Label)ctl.FindControl("lblamount");
            Label lblinward = (Label)ctl.FindControl("lblinward");
            HtmlTableCell celgent = (HtmlTableCell)ctl.FindControl("celgen");
            HtmlTableCell celspet = (HtmlTableCell)ctl.FindControl("celspe");
            HtmlTableCell celpayt = (HtmlTableCell)ctl.FindControl("celpay");

            PlaceHolder celdetail = (PlaceHolder)ctl.FindControl("celdetail");

            //Button btnreject = (Button)ctl.FindControl("btnreject");
            //Button btnforward = (Button)ctl.FindControl("btnforward");
                      

            ctl.ID = "ctl_" + celctl.Controls.Count.ToString();

            if (l_first == true)
            {
                l_first = false;
                CollapsiblePanelExtender cpeDesc = (CollapsiblePanelExtender)ctl.FindControl("cpeDesc");
                cpeDesc.Collapsed = false;
            }
           

            lblhead.Text = "   " + hdr_dr.PO_Hdr_Com1.ToString() + " [TK. " + hdr_dr.PO_Hdr_Value.ToString("N2") + "]";
            lblref.Text = hdr_dr.PO_Hdr_Ref.ToString();
            lblparty.Text = hdr_dr.PO_Hdr_Pcode.ToString() + ": " + hdr_dr.PO_Hdr_Com1.ToString();
            lblamount.Text = hdr_dr.PO_Hdr_Value.ToString("N2");
            lblinward.Text =NumerictowordClass.FNumber(hdr_dr.PO_Hdr_Value.ToString("N2"));
            
            // add detail data;

            celdetail.Controls.Clear();
            dtdet = det.GetDetByRef(hdr_dr.PO_Hdr_Ref);
            cnt = 0;
            foreach (SCBLDataSet.PuTr_PO_Det_ScblRow det_dr in dtdet.Rows)
            {
                cnt++;

                ClientSide_modules_commercial_usercontrols_ctl_po_item_det ctldet = (ClientSide_modules_commercial_usercontrols_ctl_po_item_det)LoadControl("./usercontrols/ctl_po_item_det.ascx");
                
                Label lblsl = (Label)ctldet.FindControl("lblsl");
                Label lblproduct = (Label)ctldet.FindControl("lblproduct");
                Label lblrate = (Label)ctldet.FindControl("lblrate");
                Label lblqty = (Label)ctldet.FindControl("lblqty");
                Label lbliamount = (Label)ctldet.FindControl("lbliamount");
                Label lbliinward = (Label)ctldet.FindControl("lbliinward");
                CheckBox chksel = (CheckBox)ctldet.FindControl("chksel");
                Label lblpoqty = (Label)ctldet.FindControl("lblpoqty");
                TextBox txtpo = (TextBox)ctldet.FindControl("txtpo");

                HtmlTableCell celspe = (HtmlTableCell)ctldet.FindControl("celspe");
                HtmlTableCell celbrand = (HtmlTableCell)ctldet.FindControl("celbrand");
                HtmlTableCell celorigin = (HtmlTableCell)ctldet.FindControl("celorigin");
                HtmlTableCell celpacking = (HtmlTableCell)ctldet.FindControl("celpacking");
                
                
                ctldet.ID = "celdetail_" + celdetail.Controls.Count.ToString();

                lblsl.Text = cnt.ToString() + ".";
                lblproduct.Text = det_dr.PO_Det_Icode.ToString() + ": " + det_dr.PO_Det_Itm_Desc.ToString();
                lblrate.Text = det_dr.PO_Det_Lin_Rat.ToString("N2");
                lblqty.Text = det_dr.PO_Det_Lin_Qty.ToString("N2");
                lbliamount.Text=det_dr.PO_Det_Lin_Amt.ToString("N2");
                lbliinward.Text = NumerictowordClass.FNumber(det_dr.PO_Det_Lin_Amt.ToString("N2"));
                txtpo.Text = det_dr.PO_Det_Lin_Qty.ToString("N2");


                if (lblref.Text.Substring(0, 1) == "L")
                {
                    try
                    {
                        drq = quo.GetDataByRefParty(det_dr.PO_Det_Quo_Ref, hdr_dr.PO_Hdr_Pcode.ToString())[0];
                        celspe.InnerText = drq.specification;
                        celbrand.InnerText = drq.product_brand;
                        celorigin.InnerText = drq.origin;
                        celpacking.InnerText = drq.packing;
                    }
                    catch
                    { 
                    }
                    
                }
                txtpo.Visible = false;
                lblpoqty.Visible = false;
                chksel.Visible = false;
                celdetail.Controls.Add(ctldet);
            }

            genstr = "";
            spestr = "";
            paystr = "";
            gcnt = 0;
            scnt = 0;
            pcnt = 0;
            tac_ref = hdr_dr.PO_Hdr_Ref.ToString();

            dtlog = log.GetDataByRef(tac_ref);

            foreach (SCBLDataSet.tbl_tac_logRow drlog in dtlog.Rows)
            {
                switch (drlog.tac_type)
                {
                    case "gen":
                        {
                            gcnt++;
                            genstr = genstr + gcnt.ToString() + ". " + drlog.content_det.ToString() + "<br />";
                            break;
                        }

                    case "spe":
                        {
                            scnt++;
                            spestr = spestr + scnt.ToString() + ". " + drlog.content_det.ToString() + "<br />";
                            break;
                        }

                    case "pay":
                        {
                            pcnt++;
                            paystr = paystr + pcnt.ToString() + ". " + drlog.content_det.ToString() + "<br />";
                            break;
                        }

                }
            }
            celgent.InnerHtml = genstr;
            celspet.InnerHtml = spestr;
            celpayt.InnerHtml = paystr;

           // btnforward.Visible = false;
            //btnreject.Visible = false;

            celctl.Controls.Add(ctl);
        }
               
    }


    private void get_app_data()
    {
        PuTr_PO_Hdr_ScblTableAdapter hdr = new PuTr_PO_Hdr_ScblTableAdapter();
        SCBLDataSet.PuTr_PO_Hdr_ScblDataTable dthdr = new SCBLDataSet.PuTr_PO_Hdr_ScblDataTable();
        string status = ddlstatus.SelectedValue.ToString();

        dthdr = hdr.GetDataByStatus(status);
        if (dthdr.Rows.Count > 0)
        {
            celctl.Visible = true;
            lblcount.Text = "Total Count : " + dthdr.Rows.Count.ToString();
            generate_detail_data(dthdr);
        }
        else
        {
            lblcount.Text = "No PO found." + dthdr.Rows.Count.ToString();
            celctl.Visible = false;
        }


   }

    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
