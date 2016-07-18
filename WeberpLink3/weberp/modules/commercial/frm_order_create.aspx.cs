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
using LibraryDTO;
using LibraryDAL.SCBLDataSetTableAdapters;
using LibraryDAL.SCBL2DataSetTableAdapters;
using LibraryDAL.ErpDataSetTableAdapters;
using AjaxControlToolkit;

public partial class frm_order_create : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {        
        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";
        
        if (!Page.IsPostBack)
        {
            load_party();            
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

    private void SortDDL(ref DropDownList objDDL)
    {
        ArrayList textList = new ArrayList();
        ArrayList valueList = new ArrayList();


        foreach (ListItem li in objDDL.Items)
        {
            textList.Add(li.Text);
        }

        textList.Sort();


        foreach (object item in textList)
        {
            string value = objDDL.Items.FindByText(item.ToString()).Value;
            valueList.Add(value);
        }
        objDDL.Items.Clear();

        for (int i = 0; i < textList.Count; i++)
        {
            ListItem objItem = new ListItem(textList[i].ToString(), valueList[i].ToString());
            objDDL.Items.Add(objItem);
        }

        
    }

    private void load_party()
    {
        PuTr_IN_Det_Scbl2TableAdapter det = new PuTr_IN_Det_Scbl2TableAdapter();
        SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable dt = new SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable();
        PuMa_Par_AdrTableAdapter adr = new PuMa_Par_AdrTableAdapter();
        ListItem lst;
        string tmpstr;
        bool dupp;

        ddlparty.Items.Clear();
        ddlparty.Items.Add("");

        string plnts = "Plants: ";
        int i, len, cnt, indx;
        string[] plant_list = get_plant("LPOC");

        if (plant_list == null)
        {
            lblplant.Text = "";
            return;
        }

        len = plant_list.Length;

        for (i = 0; i < len; i++)
        {
            if (plant_list[i].ToString() != "")
                plnts = plnts + plant_list[i].ToString() + ", ";
        }

        lblplant.Text = plnts;


        //dt = det.GetDataByReqStatus("LPO","APP");
        dt = det.GetDataByReqStatus2("LPO","FPO","APP");
        //dt.DefaultView.Sort = dt.IN_Det_RefColumn.ColumnName+ " asc";

        cnt = dt.Rows.Count;
        for (indx = cnt; indx > 0; indx--)
        {

            for (i = 0; i < len; i++)
            {
                if (dt[indx - 1].IN_Det_Code.Substring(0, 2) == plant_list[i])
                    goto nextcheck1;
            }
            dt.RemovePuTr_IN_Det_Scbl2Row(dt[indx - 1]);

        nextcheck1: ;
        }


        foreach (SCBL2DataSet.PuTr_IN_Det_Scbl2Row dr in dt.Rows)
        {
            dupp = false;
            lst = new ListItem();
            try
            {
                lst.Text = adr.GetDataByAdrCode(dr.In_Det_App_Party.ToString())[0].par_adr_name.ToString() + ": " + dr.IN_Det_Code + ": " + dr.In_Det_Pur_Type;
                tmpstr = dr.IN_Det_Code + ":" + dr.In_Det_Pur_Type + ":" + dr.In_Det_App_Party;
                lst.Value = tmpstr;
                foreach (ListItem ls in ddlparty.Items)
                {
                    if (ls.Value.ToString() == tmpstr) dupp = true;
                }
                if (!dupp)
                    ddlparty.Items.Add(lst);
            }
            catch { }
        }

        SortDDL(ref this.ddlparty);

       
    }


    protected void ddlparty_SelectedIndexChanged(object sender, EventArgs e)
    {
        string pcode = ddlparty.SelectedValue.ToString();

        btncreate.Visible = false;
        lbltot.Visible = false;
        gdItem.Visible = false;

        if (pcode == "")
        {
            return;
        }

        lbltot.Visible = true;
        gdItem.Visible = true;
        btncreate.Visible = true;
        generate_data(pcode);  

       

    }

    private void generate_data(string party_det)
    {
                
        decimal qty, rate, tot, gtot;
       
        string[] tmp = party_det.Split(':');
        string cash_type = tmp[0].ToString();
        string pur_type = tmp[1].ToString();
        string app_party = tmp[2].ToString();
        int icnt=0;
       

        tbl_tac_logTableAdapter log = new tbl_tac_logTableAdapter();
        SCBLDataSet.tbl_tac_logDataTable dtlog = new SCBLDataSet.tbl_tac_logDataTable();

        PuMa_Par_AdrTableAdapter adr = new PuMa_Par_AdrTableAdapter();
        quotation_detTableAdapter qdet = new quotation_detTableAdapter();
        SCBLDataSet.quotation_detRow qr;
        SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable dtdet = new SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable();
        PuTr_IN_Det_Scbl2TableAdapter det = new PuTr_IN_Det_Scbl2TableAdapter();
        SCBL2DataSet.PuTr_IN_Det_Scbl2Row dr;

        dtdet = det.GetPartywiseforPoCreate("APP", app_party,cash_type,pur_type);
               
        if (dtdet.Rows.Count == 0) { btncreate.Visible = false; lbltot.Visible = false; return; }

        btncreate.Visible = true;
        lbltot.Visible = true;
               
        gtot = 0;

        gdItem.DataSource = dtdet;
        gdItem.DataBind();

        foreach (GridViewRow gr in gdItem.Rows)
        {
            Label lblitem = (Label)gr.FindControl("Label1");
            Label lblrefno = (Label)gr.FindControl("Label2");
            Label lblparty = (Label)gr.FindControl("Label3");
            Label lblqty = (Label)gr.FindControl("Label4");
            Label lblavqty = (Label)gr.FindControl("Label5");
            Label lblrate = (Label)gr.FindControl("Label6");
            Label lblamount = (Label)gr.FindControl("Label7");
            Label lblspe = (Label)gr.FindControl("Label8");
            Label lblbrand = (Label)gr.FindControl("Label9");
            Label lblorigin = (Label)gr.FindControl("Label10");
            Label lblpacking = (Label)gr.FindControl("Label11");
            CheckBox chksel = (CheckBox)gr.FindControl("CheckBox1");
            TextBox txtpoqty = (TextBox)gr.FindControl("TextBox1");

            chksel.Checked = true;

            dr = dtdet[icnt];

            qty = Convert.ToDecimal(dr.IN_Det_Bal_Qty);
            rate = Convert.ToDecimal(dr.IN_Det_Lin_Rat);
            tot = qty * rate;
           
            gtot = gtot + tot;

            lblrefno.Text = dr.IN_Det_Ref.ToString();
            lblparty.Text = app_party + ":" + adr.GetDataByAdrCode(app_party)[0].par_adr_name.ToString();
            lblitem.Text = dr.IN_Det_Icode.ToString() +": " + dr.IN_Det_Itm_Desc.ToString();
            lblqty.Text = dr.IN_Det_Lin_Qty.ToString("N2") + " " + dr.IN_Det_Itm_Uom.ToString();
            lblavqty.Text = dr.IN_Det_Bal_Qty.ToString("N2");
            txtpoqty.Text = dr.IN_Det_Bal_Qty.ToString("N2");            
            lblrate.Text = rate.ToString("N2");
            lblamount.Text = tot.ToString("N2");

            qr=qdet.GetDataByRefParty(dr.In_Det_Quo_Ref, dr.In_Det_App_Party)[0];
            lblspe.Text = qr.specification;
            lblbrand.Text = qr.product_brand;
            lblorigin.Text = qr.origin;
            lblpacking.Text = qr.packing;


            icnt++;
           
        }

        if (gtot == 0)
            btncreate.Visible = false;
        else
            btncreate.Visible = true;

        lbltot.Text = "Total Amount TK: " + gtot.ToString("N2");        
    }

    protected void btncreate_Click(object sender, EventArgs e)
    {
       
        int cnt = 0;
        clsSpo[] seldet;
        seldet = new clsSpo[gdItem.Rows.Count];

        foreach (GridViewRow gr in gdItem.Rows)
        {
            CheckBox chksel = (CheckBox)gr.FindControl("CheckBox1");            

            if (chksel.Checked)
            {
                Label lblrefno = (Label)gr.FindControl("Label2");
                Label lblitem = (Label)gr.FindControl("Label1");
                TextBox txtpoqty = (TextBox)gr.FindControl("TextBox1");
                Label lblavqty = (Label)gr.FindControl("Label5");

                if (txtpoqty.Text != "")
                {
                    if ((Convert.ToDecimal(txtpoqty.Text) > 0) && (Convert.ToDecimal(lblavqty.Text) >= Convert.ToDecimal(txtpoqty.Text)))
                    {
                        seldet[cnt] = new clsSpo();

                        seldet[cnt].Seq = cnt;
                        seldet[cnt].RefNo = lblrefno.Text;
                        seldet[cnt].Icode = lblitem.Text.Split(':')[0];
                        seldet[cnt].Qnty = Convert.ToDecimal(txtpoqty.Text);

                        cnt = cnt + 1;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }

                
            }
        }

        string[] tmp = ddlparty.SelectedValue.ToString().Split(':');        
        string app_party = tmp[2].ToString();

       // if (app_party.Substring(0, 4) == "APN-") return;


        if (cnt > 0)
        {            
            Session[clsStatic.sessionItemSelForPO] = seldet;
            Session[clsStatic.sessionPartySelForPO] = ddlparty.SelectedValue.ToString(); 
                      
            Response.Redirect("./frm_order_create_final.aspx");
            
        }       
    }
       
    
}
