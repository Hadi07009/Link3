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

public partial class frm_mat_qc : System.Web.UI.Page
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
            }
            tblspo.Visible = false;
            get_all_po();
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

        DtMatRecRetQCTableAdapter hdr = new DtMatRecRetQCTableAdapter();
        SCBLQry.DtMatRecRetQCDataTable dt = new SCBLQry.DtMatRecRetQCDataTable();
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
                
        dt = hdr.GetDataForQC();

        cnt = dt.Rows.Count;
        for (indx = cnt; indx > 0; indx--)
        {

            for (i = 0; i < len; i++)
            {
                if (dt[indx - 1].PO_Hdr_Code.Substring(0, 2) == plant_list[i])
                    goto nextcheck1;
            }
            dt.RemoveDtMatRecRetQCRow(dt[indx - 1]);

        nextcheck1: ;
        }

        foreach (SCBLQry.DtMatRecRetQCRow dr in dt.Rows)
        {
            dupp = false;
            lst = new ListItem();
            lst.Text = dr.PO_Hdr_Ref.ToString() + ":" + dr.PO_Hdr_Com1.ToString();
            lst.Value = dr.PO_Hdr_Ref.ToString() + ":" + dr.PO_Hdr_Pcode.ToString();
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
        tbl_mat_rec_retTableAdapter mat = new tbl_mat_rec_retTableAdapter();
        SCBL2DataSet.tbl_mat_rec_retDataTable dtmat = new SCBL2DataSet.tbl_mat_rec_retDataTable();
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
        Label lblinsitm;       
        Label lblbrand;
        Label lblorigin;
        Label lblpacking;
        Label lineno;
        TextBox txtokqty;
        TextBox txtrejqty;
        TextBox txtComments;


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

        dtdet = det.GetDataByRef(selval,"Q"); 
        
        gdItem.DataSource = dtdet;
        gdItem.DataBind();
        
       
        foreach (SCBL2DataSet.PuTr_PO_Det_Scbl2Row dr in dtdet.Rows)
        {
            chksel = new CheckBox();
            lblref = new Label();
            lblicode = new Label();
            lblidet = new Label();
            lbluom = new Label();
            lblbrand = new Label();
            lblorigin = new Label();
            lblpacking = new Label();            
            lblinsitm = new Label();
            lineno = new Label();
            txtokqty = new TextBox();
            txtrejqty = new TextBox();
            txtComments = new TextBox();
            

            chksel = (CheckBox)gdItem.Rows[indx].FindControl("CheckBox1");
            lblref = (Label)gdItem.Rows[indx].FindControl("Label1");
            lblicode = (Label)gdItem.Rows[indx].FindControl("Label2");
            lblidet = (Label)gdItem.Rows[indx].FindControl("Label3");
            lbluom = (Label)gdItem.Rows[indx].FindControl("Label4");
            lblbrand = (Label)gdItem.Rows[indx].FindControl("Label5");
            lblorigin = (Label)gdItem.Rows[indx].FindControl("Label6");
            lblpacking = (Label)gdItem.Rows[indx].FindControl("Label7");
            lblinsitm = (Label)gdItem.Rows[indx].FindControl("Label8");
            lineno = (Label)gdItem.Rows[indx].FindControl("Label9");
            txtokqty = (TextBox)gdItem.Rows[indx].FindControl("TextBox1");
            txtrejqty = (TextBox)gdItem.Rows[indx].FindControl("TextBox2");
            txtComments = (TextBox)gdItem.Rows[indx].FindControl("TextBox3");       
            
            chksel.Checked = true;
            lblref.Text = dr.PO_Det_Ref.ToString();
            lblicode.Text = dr.PO_Det_Icode.ToString();
            lblidet.Text = dr.PO_Det_Itm_Desc.ToString();
            lbluom.Text = dr.PO_Det_Itm_Uom.ToString();
            
            dtmat = mat.GetDataByPoItmType(dr.PO_Det_Ref.ToString(), dr.PO_Det_Icode.ToString(), "INSPECTION");
            if (dtmat.Rows.Count == 0)
            {
                lblbrand.Text = dr.PO_Det_Brand;
                lblorigin.Text = dr.PO_Det_Origin;
                lblpacking.Text = dr.PO_Det_Packing;
            }
            else
            {
                lblbrand.Text = dtmat[0].brand;
                lblorigin.Text = dtmat[0].origin;
                lblpacking.Text = dtmat[0].packing;
            }

            lblinsitm.Text = dr.PO_Det_Ins_QTY.ToString("N2");
            lineno.Text = dr.PO_Det_Lno.ToString();
            txtokqty.Text = dr.PO_Det_Ins_QTY.ToString("N2");
            txtrejqty.Text = "0";
            txtComments.Text = lblpacking.Text;

            
            avlitm = dr.PO_Det_Ins_QTY;
                                   
                      
            if (avlitm == 0)
            {
                chksel.Checked = false;
                chksel.Visible = false;
            }
            else
                prcflg = true;

            indx++;
        }

        if (prcflg) btnProceed.Visible = true;
                

    }

    protected void gdItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

           
        }
    }


    private void generate_data()
    {
        string selval = ddlpolist.SelectedValue.ToString();
        string ref_no = ddlpolist.SelectedValue.ToString().Split(':')[0];
        btnProceed.Visible = false;

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

        clsMrrData[] mrrdata = new clsMrrData[100];
        bool entry_check = true;
        int cnt = 0;
        CheckBox chksel;
        Label lblref;
        Label lblicode;
        Label lblidet;
        Label lbluom;
        Label lblinsitm;
        Label lblbrand;
        Label lblorigin;
        Label lblpacking;
        Label lbllno;
        TextBox txtokqty;
        TextBox txtrejqty;
        TextBox txtremarks;

        string pcode, pdet;
        decimal availqty, rejqty, okqty;

        pcode = ddlpolist.SelectedValue.ToString().Split(':')[1];
        pdet = ddlpolist.SelectedItem.Text.Split(':')[1];

        foreach (GridViewRow gr in gdItem.Rows)
        {
            chksel = new CheckBox();
            lblref = new Label();
            lblicode = new Label();
            lblidet = new Label();
            lbluom = new Label();
            lblbrand = new Label();
            lblorigin = new Label();
            lblpacking = new Label();
            lblinsitm = new Label();
            lbllno = new Label();
            txtokqty = new TextBox();
            txtrejqty = new TextBox();
            txtremarks = new TextBox();

            chksel = (CheckBox)gr.FindControl("CheckBox1");
            lblref = (Label)gr.FindControl("Label1");
            lblicode = (Label)gr.FindControl("Label2");
            lblidet = (Label)gr.FindControl("Label3");
            lbluom = (Label)gr.FindControl("Label4");
            lblbrand = (Label)gr.FindControl("Label5");
            lblorigin = (Label)gr.FindControl("Label6");
            lblpacking = (Label)gr.FindControl("Label7");
            lblinsitm = (Label)gr.FindControl("Label8");
            lbllno = (Label)gr.FindControl("Label9");
            txtokqty = (TextBox)gr.FindControl("TextBox1");
            txtrejqty = (TextBox)gr.FindControl("TextBox2");
            txtremarks = (TextBox)gr.FindControl("TextBox3");
            
            if (chksel.Checked)
            {
                try
                {
                    //availqty = Convert.ToDecimal(lblinsitm.Text);
                    availqty = Convert.ToDecimal(det.GetDataByRefLine(lblref.Text,(short) Convert.ToInt32(lbllno.Text))[0].PO_Det_Ins_QTY);
                    okqty = Convert.ToDecimal(txtokqty.Text);
                    rejqty = Convert.ToDecimal(txtrejqty.Text);

                    if ((availqty == 0) ||(okqty < 0) || (rejqty < 0) || ((okqty + rejqty) != availqty))
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
                        //mrrdata[cnt].Poqty = Convert.ToDecimal(lblpoqty.Text);
                        //mrrdata[cnt].Recqty = Convert.ToDecimal(lblrecqty.Text);
                        mrrdata[cnt].Insqty = Convert.ToDecimal(lblinsitm.Text);
                        mrrdata[cnt].Availqty = availqty;
                        //mrrdata[cnt].Entryqty = Convert.ToDecimal(txtqty.Text);
                        mrrdata[cnt].OkQty = okqty;
                        mrrdata[cnt].RejQty = rejqty;
                        mrrdata[cnt].Pcode = pcode;
                        mrrdata[cnt].Pdet = pdet;
                        mrrdata[cnt].Pur_by = lblby.Text;
                        mrrdata[cnt].Pur_from = lblfrom.Text;
                        mrrdata[cnt].Brand = lblbrand.Text;
                        mrrdata[cnt].Origin = lblorigin.Text;
                        mrrdata[cnt].Packing = lblpacking.Text;
                        mrrdata[cnt].Remarks = txtremarks.Text;
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
        Response.Redirect("./frm_mat_qc_final.aspx");
    }
}
