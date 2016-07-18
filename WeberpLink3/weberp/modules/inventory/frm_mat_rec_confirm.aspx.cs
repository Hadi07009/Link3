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
using LibraryDAL.SCBLQryTableAdapters;
using LibraryDAL.SCBLDataSetTableAdapters;
using LibraryDAL.SCBL2DataSetTableAdapters;
using LibraryDAL.ErpDataSetTableAdapters;

public partial class frm_mat_rec_confirm : System.Web.UI.Page
{  

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";       

        if (!Page.IsPostBack)
        {
            if (Request.QueryString.Count > 0)
            {
                Session[clsStatic.sessionQueryString] = Request.QueryString["ret_rec_ref"].ToString();
                RegisterStartupScript("click", "<script>window.open('./frm_mat_trn_print.aspx');</script>");
                cldmrrdate.SelectedDate = DateTime.Now;
            }
           
            get_all_po();
            tblspo.Visible = false;
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

    private void get_all_po()
    {
        DtMatRecRetMRRTableAdapter hdr = new DtMatRecRetMRRTableAdapter();
        SCBLQry.DtMatRecRetMRRDataTable dt = new SCBLQry.DtMatRecRetMRRDataTable();
        bool dupp;
        ListItem lst;
        ddlpolist.Items.Clear();
        ddlpolist.Items.Add("");
       
        string plnts = "Plants: ";
        int i, len, cnt, indx;
        string[] plant_list = get_plant("MATR");

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

        dt = hdr.GetDataForMRR();

        cnt = dt.Rows.Count;
        for (indx = cnt; indx > 0; indx--)
        {

            for (i = 0; i < len; i++)
            {
                if (dt[indx - 1].PO_Hdr_Code.Substring(0, 2) == plant_list[i])
                    goto nextcheck1;
            }
            dt.RemoveDtMatRecRetMRRRow(dt[indx - 1]);

        nextcheck1: ;
        }

        foreach (SCBLQry.DtMatRecRetMRRRow dr in dt.Rows)
        {
            dupp = false;
            lst = new ListItem();
            lst.Text = dr.PO_Hdr_Ref.ToString() + ":" + dr.PO_Hdr_Com1.ToString();
            lst.Value = dr.PO_Hdr_Ref.ToString() + ":" + dr.PO_Hdr_Pcode.ToString() + ":" + dr.PO_Hdr_Code.ToString();
            foreach (ListItem ls in ddlpolist.Items)
            {
                if (ls.Value.ToString() == lst.Value) dupp = true;
            }
            if (!dupp)
                ddlpolist.Items.Add(lst);
        }

 
    }

  

    private void load_single_po(string selval)
    {
        PuTr_PO_Hdr_ScblTableAdapter hdr = new PuTr_PO_Hdr_ScblTableAdapter();
        SCBLDataSet.PuTr_PO_Hdr_ScblDataTable dthdr = new SCBLDataSet.PuTr_PO_Hdr_ScblDataTable();

        PuTr_PO_Det_Scbl2TableAdapter det = new PuTr_PO_Det_Scbl2TableAdapter();
        SCBL2DataSet.PuTr_PO_Det_Scbl2DataTable dtdet = new SCBL2DataSet.PuTr_PO_Det_Scbl2DataTable();

        bool prcflg = false;
        double avlitm;
        int indx = 0;
        CheckBox chksel;
        Label lblref;
        Label lblicode;
        Label lblidet;
        Label lbluom;
        Label lblorgitm;
        Label lblrecitm;
        Label lblinsitm;
        Label lineno;
        TextBox txtretqty;
        TextBox txtbrand;
        TextBox txtorigin;
        TextBox txtpacking;


        dthdr = hdr.GetHdrDataByRef(selval);
        if (dthdr[0].PO_Hdr_Ref.Substring(0, 1) == "S")
        {
            tblspo.Visible = true;
            lblby.Text = dthdr[0].PO_Hdr_Com3.ToString();
            lblfrom.Text = dthdr[0].PO_Hdr_Com4.ToString();
        }
        else
        {
            tblspo.Visible = false;
        }

        
        dtdet = det.GetDataByRefForMrr(selval,"");         
        gdItem.DataSource = dtdet;
        gdItem.DataBind();
        
       
        foreach (SCBL2DataSet.PuTr_PO_Det_Scbl2Row dr in dtdet.Rows)
        {
            chksel = new CheckBox();
            lblref = new Label();
            lblicode = new Label();
            lblidet = new Label();
            lbluom = new Label();
            lblorgitm = new Label();
            lblrecitm = new Label();
            lblinsitm = new Label();
            lineno = new Label();
            txtretqty = new TextBox();
            txtbrand = new TextBox();
            txtorigin = new TextBox();
            txtpacking = new TextBox();

            chksel = (CheckBox)gdItem.Rows[indx].FindControl("CheckBox1");
            lblref = (Label)gdItem.Rows[indx].FindControl("Label1");
            lblicode = (Label)gdItem.Rows[indx].FindControl("Label2");
            lblidet = (Label)gdItem.Rows[indx].FindControl("Label3");
            lbluom = (Label)gdItem.Rows[indx].FindControl("Label4");
            lblorgitm = (Label)gdItem.Rows[indx].FindControl("Label5");
            lblrecitm = (Label)gdItem.Rows[indx].FindControl("Label6");
            lblinsitm = (Label)gdItem.Rows[indx].FindControl("Label7");
            lineno = (Label)gdItem.Rows[indx].FindControl("Label8");
            txtretqty = (TextBox)gdItem.Rows[indx].FindControl("TextBox1");
            txtbrand = (TextBox)gdItem.Rows[indx].FindControl("TextBox2");
            txtorigin = (TextBox)gdItem.Rows[indx].FindControl("TextBox3");
            txtpacking = (TextBox)gdItem.Rows[indx].FindControl("TextBox4");
                       
            lblref.Text = dr.PO_Det_Ref.ToString();
            lblicode.Text = dr.PO_Det_Icode.ToString();
            lblidet.Text = dr.PO_Det_Itm_Desc.ToString();
            lbluom.Text = dr.PO_Det_Itm_Uom.ToString();
            lblorgitm.Text = dr.PO_Det_Lin_Qty.ToString("N2");
            lblrecitm.Text = dr.PO_Det_Org_QTY.ToString("N2");
            lblinsitm.Text = dr.PO_Det_Ins_QTY.ToString("N2");
            lineno.Text = dr.PO_Det_Lno.ToString();

            avlitm = dr.PO_Det_Ins_QTY;

            txtbrand.Text = dr.PO_Det_Brand;
            txtorigin.Text = dr.PO_Det_Origin;
            txtpacking.Text = dr.PO_Det_Packing;

            //gdItem.Rows[indx].ToolTip = "Specification: " + dr.PO_Det_Specification + ".| Brand: " + dr.PO_Det_Brand + ".| Origin: " + dr.PO_Det_Origin + ".| Packing: " + dr.PO_Det_Packing;

            txtretqty.Text = avlitm.ToString("N2");
            txtretqty.Style.Add("visibility", "hidden");
            txtbrand.Style.Add("visibility", "hidden");
            txtorigin.Style.Add("visibility", "hidden");
            txtpacking.Style.Add("visibility", "hidden");
          
            if (avlitm == 0)
            {
                txtretqty.Visible = false;
                chksel.Visible = false;
            }
            else
                prcflg = true;

            indx++;
        }
        if (prcflg)
        {
            btnProceed.Visible = true;
            lblmode.Visible = true;
            txtmodeofdel.Visible = true;
        }

        
    }

    protected void gdItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            TextBox txtqty = ((TextBox)e.Row.FindControl("TextBox1"));
            TextBox txtbrand = ((TextBox)e.Row.FindControl("TextBox2"));
            TextBox txtorigin = ((TextBox)e.Row.FindControl("TextBox3"));
            TextBox txtpacking = ((TextBox)e.Row.FindControl("TextBox4"));

            ((CheckBox)e.Row.FindControl("CheckBox1")).Attributes.Add("onClick", "ShowHideField(this," + txtqty.ClientID + "," + txtbrand.ClientID + "," + txtorigin.ClientID + "," + txtpacking.ClientID + ")");

        }
    }
      

    private void generate_data()
    {
        string selval = ddlpolist.SelectedValue.ToString();
        string ref_no = ddlpolist.SelectedValue.ToString().Split(':')[0];
        
        btnProceed.Visible = false;
        lblmode.Visible = false;
        txtmodeofdel.Visible = false;
        
        switch (selval)
        {
            case "":
                gdItem.Visible = false;
                tblspo.Visible = false;
                break;
                            
            default:
                gdItem.Visible = true;
                load_single_po(ref_no);

                break;

        }
    }


    protected void ddlpolist_SelectedIndexChanged(object sender, EventArgs e)
    {
        generate_data();        
    }

    protected void btnProceed_Click(object sender, EventArgs e)
    {
        PuTr_PO_Det_Scbl2TableAdapter det = new PuTr_PO_Det_Scbl2TableAdapter();

        if ((gdItem.Rows.Count < 1) || (gdItem.Visible == false)) return;

        if (cldmrrdate.SelectedDate > DateTime.Now) return;

        InTr_Trn_HdrTableAdapter intrhdr = new InTr_Trn_HdrTableAdapter();
        ErpDataSet.InTr_Trn_HdrDataTable dthdr = new ErpDataSet.InTr_Trn_HdrDataTable();


        clsMrrData[] mrrdata = new clsMrrData[100];
        bool entry_check = true;
        int cnt = 0;
        CheckBox chk;
        Label lblref, lblpoqty, lblrecqty, lblinsqty, lblicode, lblidet, lbluom, lbllno;;
        TextBox txtqty,txtbrand, txtorigin, txtpacking;
        string pcode, pdet,plant;
        decimal availqty, recqty;

        pcode = ddlpolist.SelectedValue.ToString().Split(':')[1];
        pdet = ddlpolist.SelectedItem.Text.Split(':')[1];
        plant = ddlpolist.SelectedValue.ToString().Split(':')[2];

        dthdr = intrhdr.GetDataByPlantfordate(plant.Substring(0, 2).ToString() + "MRR");
        if (dthdr.Count > 0)
        {
            if (dthdr[0].Trn_Hdr_DATE > cldmrrdate.SelectedDate) return;
        }

        
        foreach (GridViewRow gr in gdItem.Rows)
        {
            chk = new CheckBox();
            lblref = new Label();
            lblicode = new Label();
            lblidet = new Label();
            lbluom = new Label();
            lblpoqty = new Label();
            lblrecqty = new Label();
            lblinsqty = new Label();
            lbllno = new Label();
            txtqty = new TextBox();
            txtbrand = new TextBox();
            txtorigin = new TextBox();
            txtpacking = new TextBox();

            chk = (CheckBox)gr.FindControl("CheckBox1");
            lblref = (Label)gr.FindControl("Label1");
            lblicode = (Label)gr.FindControl("Label2");
            lblidet = (Label)gr.FindControl("Label3");
            lbluom = (Label)gr.FindControl("Label4");
            txtqty = (TextBox)gr.FindControl("TextBox1");
            lblpoqty = (Label)gr.FindControl("Label5");
            lblrecqty = (Label)gr.FindControl("Label6");
            lblinsqty = (Label)gr.FindControl("Label7");
            lbllno = (Label)gr.FindControl("Label8");
            txtbrand = (TextBox)gr.FindControl("TextBox2");
            txtorigin = (TextBox)gr.FindControl("TextBox3");
            txtpacking = (TextBox)gr.FindControl("TextBox4");


            if (chk.Checked)
            {
                try
                {
                    //availqty = Convert.ToDecimal(lblinsqty.Text);
                    availqty = Convert.ToDecimal(det.GetDataByRefLine(lblref.Text,(short) Convert.ToInt32(lbllno.Text))[0].PO_Det_Ins_QTY);
                    recqty = Convert.ToDecimal(txtqty.Text);

                    if ((recqty == 0) || (recqty > availqty))
                    {
                        entry_check = false;
                    }
                    else
                    {
                        mrrdata[cnt] = new clsMrrData();
                        mrrdata[cnt].Seqno = cnt + 1;
                        mrrdata[cnt].Ref_no = lblref.Text;
                        mrrdata[cnt].Icode = lblicode.Text;
                        mrrdata[cnt].Idet = lblidet.Text;
                        mrrdata[cnt].Uom = lbluom.Text;
                        mrrdata[cnt].Poqty = Convert.ToDecimal(lblpoqty.Text);
                        mrrdata[cnt].Recqty = Convert.ToDecimal(lblrecqty.Text);
                        mrrdata[cnt].Insqty = Convert.ToDecimal(lblinsqty.Text);
                        mrrdata[cnt].Availqty = availqty;
                        mrrdata[cnt].Entryqty = Convert.ToDecimal(txtqty.Text);
                        mrrdata[cnt].Pcode = pcode;
                        mrrdata[cnt].Pdet = pdet;
                        mrrdata[cnt].Pur_by = lblby.Text;
                        mrrdata[cnt].Pur_from = lblfrom.Text;
                        mrrdata[cnt].Modeofdel = txtmodeofdel.Text;                        
                        mrrdata[cnt].Plant = plant;
                        mrrdata[cnt].Brand = txtbrand.Text;
                        mrrdata[cnt].Origin = txtorigin.Text;
                        mrrdata[cnt].Packing = txtpacking.Text;
                        mrrdata[cnt].Purdate = cldmrrdate.SelectedDate;
                        mrrdata[cnt].LineNo = Convert.ToInt32(lbllno.Text);
                        cnt++;
                    }
                }
                catch
                {
                    entry_check = false;
                }
            }
        }

        if ((!entry_check) || (cnt == 0)) return;

        Session[clsStatic.sessionMrrDetData] = mrrdata;
        Response.Redirect("./frm_mat_rec_confirm_final.aspx");
    }
}
