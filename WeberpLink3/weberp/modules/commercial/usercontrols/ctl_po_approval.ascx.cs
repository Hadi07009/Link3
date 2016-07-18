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


public partial class ClientSide_modules_commercial_usercontrols_ctl_po_approval : System.Web.UI.UserControl
{

   
    protected void Page_Load(object sender, EventArgs e)
    {
       // clsStatic.CheckUserAuthentication();        
       
        //set_approval_policy(false);
    }



    //private string get_my_app()
    //{
    //    User_Role_DefinitionTableAdapter urole = new User_Role_DefinitionTableAdapter();
    //    commercialDataSet.User_Role_DefinitionDataTable udt = new commercialDataSet.User_Role_DefinitionDataTable();
    //    string my_app = "";
    //    udt = urole.GetRoleByUser(current.UserId.ToString());

    //    if (udt.Rows.Count > 0) my_app = udt[0].role_as.ToString();

    //    return my_app;
    //}

   

    //protected void btnforward_Click(object sender, EventArgs e)
    //{
    //    System.Threading.Thread.Sleep(3000);
    //    Session[clsStatic.sessionPoForwardDet] = null;
    //    set_approval_policy(true);
        
        
    //}


    //private void set_approval_policy(bool ismanual)
    //{

    //    if ((ismanual == false) && (Session[clsStatic.sessionPoForwardDet] == null)) return;
    //    bool flg = true;
    //    bool unchange = true;

    //    int cnt, i, sl;
    //    decimal otot, qty, rate, ttot;
    //    decimal ntot = 0;

    //    HtmlTableRow tr;

    //    otot = Convert.ToDecimal(lblamount.Text);
    //    lblcref.Text = lblref.Text;
    //    lblcparty.Text = lblparty.Text;
    //    lblact.Text = lblamount.Text;

    //    if ((ismanual == false) && (Session[clsStatic.sessionPoForwardDet] != null))
    //    {
    //        tbl_item = (HtmlTable)Session[clsStatic.sessionPoForwardDet];
    //        return;
    //    }
    //    cnt = celdetail.Controls.Count;

    //    Session[clsStatic.sessionPoAppRef] = lblcref.Text;

    //    sl = 0;
    //    for (i = 0; i < cnt; i++)
    //    {
    //        Control ctlItm = celdetail.Controls[i];

    //        CheckBox chksel = (CheckBox)ctlItm.FindControl("chksel");
    //        Label lblproduct = (Label)ctlItm.FindControl("lblproduct");
    //        TextBox txtpo = (TextBox)ctlItm.FindControl("txtpo");
    //        Label lblrate = (Label)ctlItm.FindControl("lblrate");
    //        Label lblqty = (Label)ctlItm.FindControl("lblqty");

    //        if (txtpo.Text == "") txtpo.Text = "0";

    //        if ((chksel.Checked) && (Convert.ToDecimal(txtpo.Text) > 0))
    //        {
    //            sl++;
    //            qty = Convert.ToDecimal(txtpo.Text);
    //            rate = Convert.ToDecimal(lblrate.Text);

    //            ttot = qty * rate;
    //            ntot += ttot;

    //            if (qty > Convert.ToDecimal(lblqty.Text))
    //                flg = false;

    //            if (qty != Convert.ToDecimal(lblqty.Text))
    //                unchange = false;


    //            tr = new HtmlTableRow();
    //            tr.Cells.Clear();

    //            tr.Cells.Add(new HtmlTableCell());
    //            tr.Cells.Add(new HtmlTableCell());
    //            tr.Cells.Add(new HtmlTableCell());
    //            tr.Cells.Add(new HtmlTableCell());
    //            tr.Cells.Add(new HtmlTableCell());

    //            tr.Cells[0].InnerText = Convert.ToString(sl);
    //            tr.Cells[1].InnerText = lblproduct.Text;
    //            tr.Cells[2].InnerText = txtpo.Text;
    //            tr.Cells[3].InnerText = lblrate.Text;
    //            tr.Cells[4].InnerText = ttot.ToString("N2"); ;
    //            tbl_item.Rows.Add(tr);

    //        }
    //        else
    //        {
    //            unchange = false;
    //        }

    //    }

    //    if (ntot != otot) unchange = false;

    //    if (cnt == 0) flg = false;

    //    lblcamount.Text = ntot.ToString("N2");
    //    lblcinward.Text = NumerictowordClass.FNumber(ntot.ToString("N2"));

    //    if (unchange)
    //    {
    //        //forward to next app
    //        Session[clsStatic.sessionPoForwardDet] = null;
    //        update_po_status(lblref.Text, otot, true);
                      

    //        //Response.Redirect(Request.Url.AbsoluteUri);
    //    }
    //    else
    //    {
    //        if (flg)
    //        {
    //            Session[clsStatic.sessionPoForwardDet] = tbl_item;
    //            ModalPopupExtender5.Show();
    //        }
    //    }
    //}



    //private string get_flow_next_app(string ref_no, string myapp)
    //{
    //    PuTr_PO_Hdr_ScblTableAdapter po = new PuTr_PO_Hdr_ScblTableAdapter();
    //    //App_flow_detTableAdapter app = new App_flow_detTableAdapter();
    //    string next_app = "";
    //    string template = po.GetHdrDataByRef(ref_no)[0].PO_Hdr_Template.ToString();
    //    int seq_no = app.GetflowByApp(template, myapp)[0].flow_seq;
    //    int max_sl = Convert.ToInt32(app.GetMaxFlowSl(template));

    //    if (seq_no == max_sl)
    //    {
    //        next_app = "APP";
    //    }
    //    else
    //    {
    //        next_app = app.GetFlowBySeq(template, seq_no + 1)[0].app_name.ToString();
    //    }

    //    return next_app;
    //}

    //private void update_po_status(string ref_no,decimal hdrval, bool isforward)
    //{
    //    PuTr_PO_Hdr_ScblTableAdapter hdr = new PuTr_PO_Hdr_ScblTableAdapter();
    //    string myapp = get_my_app();
    //    string next_app = get_flow_next_app(ref_no, myapp);

    //    if (hdr.CheckPendingValidity(ref_no, "POI", myapp).Rows.Count != 1) return;

    //    if (isforward)
    //    {
    //        if (next_app == "APP")
    //            hdr.UpdateForward("APP", "", myapp, hdrval,"P", ref_no);
    //        else
    //            hdr.UpdateForward("POI", next_app, myapp, hdrval,"H", ref_no);
    //    }
    //    else
    //    {
    //        hdr.UpdateForward("REJ", "", myapp, hdrval,"R", ref_no);
    //    }
    //    //bewlow this for set ist item open in inbox
    //    Session[clsStatic.sessionPoAppRef] = null;
    //    if (reset_pending != null)
    //    {
    //        reset_pending(null, null);
    //    }

   // }

    //protected void btnback_Click(object sender, EventArgs e)
    //{
    //    Session[clsStatic.sessionPoForwardDet] = null;
    //    //Response.Redirect(Request.Url.AbsoluteUri);
    //}
    //protected void btnconfirm_Click(object sender, EventArgs e)
    //{
    //    //System.Threading.Thread.Sleep(3000);

    //}
}
