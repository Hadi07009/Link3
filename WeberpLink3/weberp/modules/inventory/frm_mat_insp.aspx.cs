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
using System.Collections.Generic;
using System.Linq;
using AjaxControlToolkit;




public partial class frm_mat_insp : System.Web.UI.Page
{
    string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

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
        DtInspTableAdapter hdr = new DtInspTableAdapter();
        SCBLQry.DtInspDataTable dt = new SCBLQry.DtInspDataTable();

        ListItem lst;
        ddlpolist.Items.Clear();
        ddlpolist.Items.Add("");
       
        string plnts = "Plants: ";
        int i, len, cnt, indx;
        string[] plant_list = get_plant("MATR");
        bool dupp;

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

        dt = hdr.GetDataFoInsp();

        cnt = dt.Rows.Count;
        for (indx = cnt; indx > 0; indx--)
        {

            for (i = 0; i < len; i++)
            {
                if (dt[indx - 1].PO_Hdr_Code.Substring(0, 2) == plant_list[i])
                    goto nextcheck1;
            }
            dt.RemoveDtInspRow(dt[indx - 1]);

        nextcheck1: ;
        }

      

        foreach (SCBLQry.DtInspRow dr in dt.Rows)
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

    private void load_all_po()
    {
        DtInspTableAdapter hdr = new DtInspTableAdapter();
        SCBLQry.DtInspDataTable dthdr = new SCBLQry.DtInspDataTable();
        PuTr_PO_Det_ScblTableAdapter det = new PuTr_PO_Det_ScblTableAdapter();
        SCBLDataSet.PuTr_PO_Det_ScblDataTable dtdet;
        DataTable dttemp = new DataTable();
        double avlitm;
        int indx = 0;

        Label lblref;
        Label lblicode;
        Label lblidet;
        Label lbluom;
        Label lblorgitm;
        Label lblrecitm;
        Label lblinsitm;
        TextBox txtinspitm;        

        dttemp.Columns.Add("Ref", typeof(string));
        dttemp.Columns.Add("Icode", typeof(string));

        int i, len, cnt;
        string[] plant_list = get_plant("MATR");
        if (plant_list == null)
            return;

        len = plant_list.Length;

        dthdr = hdr.GetDataFoInsp();

        cnt = dthdr.Rows.Count;
        for (indx = cnt; indx > 0; indx--)
        {

            for (i = 0; i < len; i++)
            {
                if (dthdr[indx - 1].PO_Hdr_Code.Substring(0, 2) == plant_list[i])
                    goto nextcheck1;
            }
            dthdr.RemoveDtInspRow(dthdr[indx - 1]);

        nextcheck1: ;
        }

        foreach (SCBLQry.DtInspRow drhdr in dthdr.Rows)
        {
            dtdet = new SCBLDataSet.PuTr_PO_Det_ScblDataTable();
            dtdet = det.GetDetByRef(drhdr.PO_Hdr_Ref.ToString());

            foreach (SCBLDataSet.PuTr_PO_Det_ScblRow dr in dtdet.Rows)
            {
                dttemp.Rows.Add(dr.PO_Det_Ref,dr.PO_Det_Icode);
            }
        }

        gdItem.DataSource = dttemp;
        gdItem.DataBind();

        foreach (SCBLQry.DtInspRow drhdr in dthdr.Rows)
        {
            dtdet = new SCBLDataSet.PuTr_PO_Det_ScblDataTable();
            dtdet = det.GetDetByRef(drhdr.PO_Hdr_Ref.ToString());

            foreach (SCBLDataSet.PuTr_PO_Det_ScblRow dr in dtdet.Rows)
            {
                lblref = new Label();
                lblicode = new Label();
                lblidet = new Label();
                lbluom = new Label();
                lblorgitm = new Label();
                lblrecitm = new Label();
                lblinsitm = new Label();
                txtinspitm = new TextBox();
               
                lblref = (Label)gdItem.Rows[indx].FindControl("Label1");
                lblicode = (Label)gdItem.Rows[indx].FindControl("Label2");
                lblidet = (Label)gdItem.Rows[indx].FindControl("Label3");
                lbluom = (Label)gdItem.Rows[indx].FindControl("Label4");
                lblorgitm = (Label)gdItem.Rows[indx].FindControl("Label5");
                lblrecitm = (Label)gdItem.Rows[indx].FindControl("Label6");
                lblinsitm = (Label)gdItem.Rows[indx].FindControl("Label7");
                txtinspitm = (TextBox)gdItem.Rows[indx].FindControl("TextBox1");
               

                lblref.Text = dr.PO_Det_Ref.ToString();
                lblicode.Text = dr.PO_Det_Icode.ToString();
                lblidet.Text = dr.PO_Det_Itm_Desc.ToString();
                lbluom.Text = dr.PO_Det_Itm_Uom.ToString();
                lblorgitm.Text = dr.PO_Det_Lin_Qty.ToString("N2");
                lblrecitm.Text = dr.PO_Det_Org_QTY.ToString("N2");
                lblinsitm.Text = dr.PO_Det_Ins_QTY.ToString("N2");
                avlitm = dr.PO_Det_Lin_Qty - dr.PO_Det_Org_QTY - dr.PO_Det_Ins_QTY;
                gdItem.Rows[indx].ToolTip = "Specification: " + dr.PO_Det_Specification + ".| Brand: " + dr.PO_Det_Brand + ".| Origin: " + dr.PO_Det_Origin + ".| Packing: " + dr.PO_Det_Packing;

                txtinspitm.Text = avlitm.ToString("N2");
               
                if (avlitm == 0)
                {
                    txtinspitm.Visible = false;                    
                }
                
                indx++;
            }           
        }
        gdItem.Columns[0].Visible = false;
        gdItem.Columns[8].Visible = false;
        gdItem.Columns[9].Visible = false;
        gdItem.Columns[10].Visible = false;
        gdItem.Columns[11].Visible = false;

    }

    private void load_single_po(string selval)
    {
        PuTr_PO_Hdr_ScblTableAdapter hdr = new PuTr_PO_Hdr_ScblTableAdapter();
        SCBLDataSet.PuTr_PO_Hdr_ScblDataTable dthdr = new SCBLDataSet.PuTr_PO_Hdr_ScblDataTable();

        PuTr_PO_Det_ScblTableAdapter det = new PuTr_PO_Det_ScblTableAdapter();
        SCBLDataSet.PuTr_PO_Det_ScblDataTable dtdet = new SCBLDataSet.PuTr_PO_Det_ScblDataTable();

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
        TextBox txtinspitm;
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

        dtdet = det.GetDetByRef(selval); 
        
        gdItem.DataSource = dtdet;
        gdItem.DataBind();

        
        foreach (SCBLDataSet.PuTr_PO_Det_ScblRow dr in dtdet.Rows)
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
            txtinspitm = new TextBox();
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
            txtinspitm = (TextBox)gdItem.Rows[indx].FindControl("TextBox1");
            txtbrand = (TextBox)gdItem.Rows[indx].FindControl("TextBox2");
            txtorigin = (TextBox)gdItem.Rows[indx].FindControl("TextBox3");
            txtpacking = (TextBox)gdItem.Rows[indx].FindControl("TextBox4");


            lblref.Text = dr.PO_Det_Ref.ToString();
            lblicode.Text = dr.PO_Det_Icode.ToString();
            lblidet.Text = dr.PO_Det_Itm_Desc.ToString();
            lbluom.Text = dr.PO_Det_Itm_Uom.ToString();
            lblorgitm.Text = dr.PO_Det_Lin_Qty.ToString("N2");
            lblrecitm.Text = dr.PO_Det_Org_QTY.ToString("N2");

            double insqty = dr.PO_Det_Org_QTY + dr.PO_Det_Ins_QTY + dr.PO_Det_Qc_QTY;

            lblinsitm.Text = insqty.ToString("N2");//dr.PO_Det_Ins_QTY.ToString("N2");
            lineno.Text = dr.PO_Det_Lno.ToString();

            avlitm = dr.PO_Det_Lin_Qty - dr.PO_Det_Org_QTY - dr.PO_Det_Ins_QTY - dr.PO_Det_Qc_QTY;//dr.PO_Det_Lin_Qty - dr.PO_Det_Org_QTY - dr.PO_Det_Ins_QTY;

            txtbrand.Text = dr.PO_Det_Brand;
            txtorigin.Text = dr.PO_Det_Origin;
            txtpacking.Text = "";// dr.PO_Det_Packing;

            //gdItem.Rows[indx].ToolTip = "Specification: " + dr.PO_Det_Specification + ".| Brand: " + dr.PO_Det_Brand + ".| Origin: " + dr.PO_Det_Origin + ".| Packing: " + dr.PO_Det_Packing;

            txtinspitm.Text = avlitm.ToString("N2");
            txtinspitm.Style.Add("visibility", "hidden");
            txtbrand.Style.Add("visibility", "hidden");
            txtorigin.Style.Add("visibility", "hidden");
            txtpacking.Style.Add("visibility", "hidden");


            var txtSerialNo = (TextBox)gdItem.Rows[indx].FindControl("TextBox4");
            var dtChkserial = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetItemByCode(lblicode.Text));
            if (dtChkserial.Rows.Count > 0)
            {
                if (dtChkserial.Rows[0]["Itm_Det_Others1_flag"].ToString() == "Y")
                {
                    txtSerialNo.Enabled = true;
                }
                else                
                {
                    txtSerialNo.Enabled = false;
                }
            }
            else
            {
                txtSerialNo.Enabled = false;
            }

            if (avlitm == 0)
            {
                txtinspitm.Visible = false;
                chksel.Visible = false;
            }
            else
                prcflg = true;

            indx++;
        }
        if (prcflg) btnProceed.Visible = true;

        gdItem.Columns[0].Visible = true;
        gdItem.Columns[8].Visible = true;
        gdItem.Columns[9].Visible = true;
        gdItem.Columns[10].Visible = true;
        gdItem.Columns[11].Visible = true;


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

        switch (selval)
        {
            case "":
                gdItem.Visible = false;
                tblspo.Visible = false;
                break;

            case "ALL":
                gdItem.Visible = true;
                tblspo.Visible = false;
                load_all_po();
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
        if ((gdItem.Rows.Count < 1) || (gdItem.Visible == false)) return;

        if (CheckSerialNumber() == false) return;      


        clsMrrData[] mrrdata = new clsMrrData[100];
        bool entry_check = true;
        int cnt = 0;
        CheckBox chk;
        Label lblref, lblpoqty, lblrecqty, lblinsqty, lblicode, lblidet, lbluom, lbllno;
        TextBox txtqty, txtbrand, txtorigin, txtpacking;
        string pcode, pdet;
        decimal availqty, recqty;

        pcode = ddlpolist.SelectedValue.ToString().Split(':')[1];
        pdet = ddlpolist.SelectedItem.Text.Split(':')[1];

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
                    availqty = Convert.ToDecimal(lblpoqty.Text) - Convert.ToDecimal(lblinsqty.Text);                   
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
                        mrrdata[cnt].Brand = txtbrand.Text;
                        mrrdata[cnt].Origin = txtorigin.Text;
                        mrrdata[cnt].Packing = txtpacking.Text;
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
        Response.Redirect("./frm_mat_insp_final.aspx");
    }

    private bool CheckSerialNumber()
    {
        bool rt = true;
        try
        {
            foreach (GridViewRow row in gdItem.Rows)
            {

                var insQty = ((TextBox)(row.Cells[7].FindControl("TextBox1"))).Text; 
                var serialTextBox = (TextBox)row.Cells[11].FindControl("TextBox4");
                var itmSerial = ((TextBox)(row.Cells[11].FindControl("TextBox4"))).Text;

                if (serialTextBox.Enabled == true)
                {
                    if (itmSerial == "")
                    {
                        lblmsg.Text = "Please enter serial number at line: "+(row.RowIndex+1).ToString();
                        return false;
                    }
                }

                if (itmSerial != "")
                {
                    string[] temp = itmSerial.Split(',');
                    List<string> vals = new List<string>();
                    bool returnValue = false;
                    string dplicateSerial = "";
                    foreach (string s in temp)
                    {
                        if (vals.Contains(s))
                        {
                            dplicateSerial = s;
                            returnValue = true;
                            break;
                        }
                        vals.Add(s);
                    }

                    if (returnValue)
                    {
                        lblmsg.Text = "Duplicate Serial No Found. Serial #" + dplicateSerial;                       
                        return false;
                    }
                }

                if (itmSerial != "")
                {
                    string[] temp = itmSerial.Split(',');

                    string ins = insQty.ToString();
                    double numberOfSerial = Convert.ToDouble(temp.Length.ToString());
                    double insQuanty = Convert.ToDouble(ins);
                    if (insQuanty != numberOfSerial)
                    {
                        lblmsg.Text = "Inspection Quantity and Number of Serial does not match.";
                        return false;                       
                    }
                }

            }
        }
        catch
        {
            return false;
        }
        return true;
    }

}
