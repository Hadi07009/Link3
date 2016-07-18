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
using LibraryDTO;
using LibraryDAL;
using LibraryDAL.SCBLDataSetTableAdapters;
using LibraryDAL.ErpDataSetTableAdapters;
using LibraryDAL.SCBL2DataSetTableAdapters;
using LibraryDAL.SCBLINTableAdapters;
using LibraryDAL.dsLinkofficeTableAdapters;

public partial class frm_spo_create_ini : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";

        if (!Page.IsPostBack)
        {
            //Generate_Items();
            load_plants();
            tblsel.Visible = false;
        }
        else
        {

        }
          
    }
    private string[] get_plant(string apptype)
    {
        User_Role_DefinitionTableAdapter urole = new User_Role_DefinitionTableAdapter();
        SCBLDataSet.User_Role_DefinitionDataTable udt = new SCBLDataSet.User_Role_DefinitionDataTable();
        string[] plant_list;
        udt = urole.GetDataByUserCodeRole(current.UserId.ToString(), apptype);

        if (udt.Rows.Count > 0)
            plant_list = udt[0].plant_list.Split(',');
        else
            return null;

        return plant_list;
    }

    private void load_plants()
    {
       // tbl_plantwise_storeTableAdapter pl=new tbl_plantwise_storeTableAdapter();
        //SCBLDataSet.tbl_plantwise_storeRow dr;

        int i, len;
        ListItem lst;
        ddlplants.Items.Clear();
        ddlplants.Items.Add("");

        string[] plant_list = get_plant("SPOC");

        if (plant_list == null)
            return;
                       
        lst = new ListItem();
        lst.Value = "CM";
        lst.Text = "CM:SSCML";
        ddlplants.Items.Add(lst);
        
    }

   

    private void Generate_Items(string plants)
    {
        SCBLDataSet.PuTr_IN_Det_ScblDataTable itm = new SCBLDataSet.PuTr_IN_Det_ScblDataTable();
        PuTr_IN_Det_ScblTableAdapter det = new PuTr_IN_Det_ScblTableAdapter();
        PuTr_PO_Det_ScblTableAdapter podet = new PuTr_PO_Det_ScblTableAdapter();
        SCBLDataSet.PuTr_PO_Det_ScblDataTable dt = new SCBLDataSet.PuTr_PO_Det_ScblDataTable();
        int cnt, indx;
        int slno = 0;
        TextBox txtpoqty, txtrate, txtparty, txtspe, txtbrand,txtorigin,txtpacking;
        Label lblrefno;
        Label lblicode;
        Label lblidet;
        Label lblqty;
        Label lblbal;
        DropDownList ddllog;
        ListItem lst;

        itm = det.GetDataByReqStatusForIni("SPO", "TEN","");


        cnt = itm.Rows.Count;
        for (indx = cnt; indx > 0; indx--)
        {
            if (itm[indx - 1].IN_Det_Code.Substring(0, 2) != plants)
                itm.RemovePuTr_IN_Det_ScblRow(itm[indx - 1]);       
        }


        if (itm.Rows.Count < 1)
        {
            btnproceed.Visible = false;
            gdItem.Visible = false;
            lblitem.Visible = true;
            tblsel.Visible = false;            
            return;
        }
        else
        {            
            btnproceed.Visible = true;
            gdItem.Visible = true;
            lblitem.Visible = false;
            tblsel.Visible = true;
            
            gdItem.DataSource = itm;
            gdItem.DataBind();
           
            foreach (SCBLDataSet.PuTr_IN_Det_ScblRow dr in itm.Rows)
            {
                txtpoqty = new TextBox();
                txtrate = new TextBox();
                txtparty = new TextBox();
                txtspe = new TextBox();
                txtbrand = new TextBox();
                txtorigin = new TextBox();
                txtpacking = new TextBox();

                lblrefno = new Label();               
                lblicode = new Label();
                lblidet = new Label();
                lblqty = new Label();
                ddllog = new DropDownList();

                txtpoqty = (TextBox)gdItem.Rows[slno].FindControl("TextBox1");
                txtrate = (TextBox)gdItem.Rows[slno].FindControl("TextBox2");
                txtparty = (TextBox)gdItem.Rows[slno].FindControl("TextBox3");
                txtspe = (TextBox)gdItem.Rows[slno].FindControl("TextBox4");
                txtbrand = (TextBox)gdItem.Rows[slno].FindControl("TextBox5");
                txtorigin = (TextBox)gdItem.Rows[slno].FindControl("TextBox6");
                txtpacking = (TextBox)gdItem.Rows[slno].FindControl("TextBox7");

                lblrefno = (Label)gdItem.Rows[slno].FindControl("Label1");                
                lblicode = (Label)gdItem.Rows[slno].FindControl("Label2");
                lblidet = (Label)gdItem.Rows[slno].FindControl("Label3");
                lblqty = (Label)gdItem.Rows[slno].FindControl("Label4");
                lblbal = (Label)gdItem.Rows[slno].FindControl("Label5");
                ddllog = (DropDownList)gdItem.Rows[slno].FindControl("DropDownList1");

                lblrefno.Text = dr.IN_Det_Ref.ToString();                
                lblicode.Text = dr.IN_Det_Icode.ToString();
                lblidet.Text = dr.IN_Det_Itm_Desc.ToString();
                lblqty.Text = dr.IN_Det_Lin_Qty.ToString("N2") + " " + dr.IN_Det_Itm_Uom.ToString();
                lblbal.Text = dr.IN_Det_Bal_Qty.ToString("N2");
                txtpoqty.Text = dr.IN_Det_Bal_Qty.ToString("N2");

                txtspe.Text = dr.In_Det_Specification.ToString();
                txtbrand.Text = dr.In_Det_Brand.ToString();
                txtorigin.Text = dr.In_Det_Origin.ToString();
                txtpacking.Text = dr.In_Det_Packing.ToString();

                dt=podet.GetDataforPriceLog(dr.IN_Det_Icode.ToString());
                ddllog.Items.Clear();
                
                foreach (SCBLDataSet.PuTr_PO_Det_ScblRow drr in dt.Rows)
                {
                    lst = new ListItem();
                    lst.Text = drr.PO_Det_Lin_Rat.ToString("N2");
                    lst.Value = drr.PO_Det_Lin_Rat.ToString("N2") + " [" + drr.PO_Det_Ref.ToString() + "]";
                    ddllog.Items.Add(lst);
                    if (ddllog.Items.Count > 20) break; 
                }

                txtpoqty.Style.Add("visibility","hidden");
                txtrate.Style.Add("visibility", "hidden");
                ddllog.Style.Add("visibility", "hidden");
                txtparty.Style.Add("visibility", "hidden");
                txtspe.Style.Add("visibility", "hidden");
                txtbrand.Style.Add("visibility", "hidden");
                txtorigin.Style.Add("visibility", "hidden");
                txtpacking.Style.Add("visibility", "hidden");


                slno++;
            }
                        
        }

    }

    private string getemp()
    {
        tblUserInfoTableAdapter user = new tblUserInfoTableAdapter();
        dsLinkoffice.tblUserInfoDataTable dtuser = new dsLinkoffice.tblUserInfoDataTable();
        string emp = "";

        string[] tmp = txtemployee.Text.Split(':');

        if (tmp.Length < 2) return "";

        dtuser = user.GetUserByCode(tmp[0].ToString());
        if (dtuser.Rows.Count == 0) return "";
        else
            emp = dtuser[0].UserId.ToString() + ":" + dtuser[0].UserName.ToString() + ":" + dtuser[0].UserDesignation.ToString();


        return emp;
    }
    
    
    protected void btnproceed_Click(object sender, EventArgs e)
    {
        
                
        CheckBox chk;
        Label lblref;        
        Label lblicode;
        Label lblidet;
        Label lblqty;
        Label lblbal;
        TextBox txtpoqty;
        TextBox txtrate;
        TextBox txtparty;
        TextBox txtspe;
        TextBox txtbrand;
        TextBox txtorigin;
        TextBox txtpacking;
        
        clsSpo[] seldet;
        seldet = new clsSpo[gdItem.Rows.Count];
        int seqno = 0;


        string empdet = getemp();
        if (empdet == "") return;
        

        try
        {
            foreach (GridViewRow gr in gdItem.Rows)
            {

                chk = new CheckBox();
                txtpoqty = new TextBox();
                txtrate = new TextBox();
                txtspe = new TextBox();
                txtbrand = new TextBox();
                txtorigin = new TextBox();
                txtpacking = new TextBox();

                lblref = new Label();
                lblicode = new Label();
                lblidet = new Label();
                lblqty = new Label();
                lblbal = new Label();

                txtpoqty = (TextBox)gr.FindControl("TextBox1"); 
                txtrate = (TextBox)gr.FindControl("TextBox2");
                txtparty = (TextBox)gr.FindControl("TextBox3");
                txtspe = (TextBox)gr.FindControl("TextBox4");
                txtbrand = (TextBox)gr.FindControl("TextBox5");
                txtorigin = (TextBox)gr.FindControl("TextBox6");
                txtpacking = (TextBox)gr.FindControl("TextBox7");

                chk = (CheckBox)gr.FindControl("CheckBox1");
                lblref = (Label)gr.FindControl("Label1");
                lblicode = (Label)gr.FindControl("Label2");
                lblidet = (Label)gr.FindControl("Label3");
                lblqty = (Label)gr.FindControl("Label4");
                lblbal = (Label)gr.FindControl("Label5");

                if ((chk.Checked) && (txtpoqty.Text != "") && (txtrate.Text != ""))
                    if ((Convert.ToDecimal(txtpoqty.Text) != 0) && (Convert.ToDecimal(txtrate.Text) != 0) && (Convert.ToDecimal(txtpoqty.Text) <= Convert.ToDecimal(lblbal.Text)))
                    {
                        seldet[seqno] = new clsSpo();

                        seldet[seqno].Seq = seqno;
                        seldet[seqno].RefNo = lblref.Text;
                        seldet[seqno].ReqType = lblref.Text.Substring(0,5);
                        seldet[seqno].Icode = lblicode.Text;
                        seldet[seqno].Idet = lblidet.Text;
                        seldet[seqno].Uom = lblqty.Text.Split(' ')[1];
                        seldet[seqno].Qnty = Convert.ToDecimal(txtpoqty.Text);
                        seldet[seqno].Rate = Convert.ToDecimal(txtrate.Text);
                        seldet[seqno].Totval = seldet[seqno].Qnty * seldet[seqno].Rate;
                        seldet[seqno].Specification = txtspe.Text;
                        seldet[seqno].Brand =txtbrand.Text;
                        seldet[seqno].Origin = txtorigin.Text;
                        seldet[seqno].Packing = txtpacking.Text;
                        seldet[seqno].Partydet = txtparty.Text;
                        seldet[seqno].Empdet = empdet;

                        seqno++;
                    }
            }           

        }
        catch(Exception ex)
        {
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        if (seqno > 0)
        {
            Session[clsStatic.sessionItemSelForPO] = seldet;
            Response.Redirect("./frm_spo_create_ini_final.aspx");
        }  
        
    }

   

    protected void gdItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //((CheckBox)e.Row.FindControl("CheckBox1")).Attributes.Add("onClick", "ColorRow(this)");
            
            TextBox txtqty = ((TextBox)e.Row.FindControl("TextBox1"));
            TextBox txtrate = ((TextBox)e.Row.FindControl("TextBox2"));
            TextBox txtparty = ((TextBox)e.Row.FindControl("TextBox3"));
            TextBox txtspe = ((TextBox)e.Row.FindControl("TextBox4"));
            TextBox txtbrand = ((TextBox)e.Row.FindControl("TextBox5"));
            TextBox txtorigin = ((TextBox)e.Row.FindControl("TextBox6"));
            TextBox txtpacking = ((TextBox)e.Row.FindControl("TextBox7"));
            DropDownList ddlrate = ((DropDownList)e.Row.FindControl("DropDownList1"));

            ((CheckBox)e.Row.FindControl("CheckBox1")).Attributes.Add("onClick", "ShowHideField(this," + txtqty.ClientID + "," + txtrate.ClientID + "," + ddlrate.ClientID + "," + txtparty.ClientID + "," + txtspe.ClientID + "," + txtbrand.ClientID + "," + txtorigin.ClientID + "," + txtpacking.ClientID + ")");

        }
    }


    protected void ddlplants_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selplant = ddlplants.SelectedValue.ToString();

        if (selplant == "")
        {
            btnproceed.Visible = false;
            tblsel.Visible = false;
        }
        else
        {
            Generate_Items(selplant);
        }

    }
    protected void gdItem_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    
}

