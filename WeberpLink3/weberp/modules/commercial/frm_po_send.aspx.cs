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
using LibraryDAL.ErpDataSetTableAdapters;
using LibraryDAL.SCBLDataSetTableAdapters;


public partial class frm_po_send : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {        
        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor ="FFFFFFF";       
        if (!Page.IsPostBack)
        {

            //txtdate.Text = DateTime.Now.ToShortDateString();
            txtfrom.Text = "Name" + "\n" + "Designation"; //current.UserName.ToString();
            txtsub.Text = "Purchase Order";

            if (Request.QueryString.Count == 1)
                if (Request.QueryString["po_ref_no"] != null)
                {
                    PuTr_PO_Hdr_ScblTableAdapter hdr = new PuTr_PO_Hdr_ScblTableAdapter();
                    SCBLDataSet.PuTr_PO_Hdr_ScblDataTable dthdr = new SCBLDataSet.PuTr_PO_Hdr_ScblDataTable();

                    dthdr = hdr.GetHdrDataByRef(Request.QueryString["po_ref_no"].ToString());

                    if (dthdr.Rows.Count > 0)
                    {
                        txtparty.Text = dthdr[0].PO_Hdr_Code + ":" + dthdr[0].PO_Hdr_Ref + ":" + dthdr[0].PO_Hdr_Com1;
                        Generate_Items();
                    }
 
                }           



        }
        else
        {
            Generate_Items();
        }
       
    }

    //private void load_party()
    //{
    //    PuTr_PO_Hdr_ScblTableAdapter acc = new PuTr_PO_Hdr_ScblTableAdapter();
    //    DataTable dt = new DataTable();

    //    ListItem lst;
    //    dt = acc.GetDataByStatus("APP");

    //    ddlparty.Items.Clear();
    //    ddlparty.Items.Add("");

    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        if(dr["po_hdr_ref"].ToString().Substring(0,2)=="LP")
    //        {
    //            lst = new ListItem();
    //            lst.Value = dr["po_hdr_code"].ToString().Substring(0, 2) + ":" + dr["po_hdr_ref"].ToString();
    //            lst.Text = dr["po_hdr_ref"].ToString() + ": " + dr["po_hdr_com1"].ToString();
    //            ddlparty.Items.Add(lst);
    //        }
    //    }

    //}

    private void Generate_Items()
    {
        tbl_tac_logTableAdapter log = new tbl_tac_logTableAdapter();
        SCBLDataSet.tbl_tac_logDataTable dtlog = new SCBLDataSet.tbl_tac_logDataTable();

        PuTr_PO_Hdr_ScblTableAdapter hdr=new PuTr_PO_Hdr_ScblTableAdapter();
        PuTr_PO_Det_ScblTableAdapter det = new PuTr_PO_Det_ScblTableAdapter();
        SCBLDataSet.PuTr_PO_Det_ScblDataTable itm = new SCBLDataSet.PuTr_PO_Det_ScblDataTable();
        
        //quotation_detTableAdapter qdet = new quotation_detTableAdapter();
        //SCBLDataSet.quotation_detDataTable quodt;

        
        int gcnt, scnt, pcnt;
        int daycnt = 0;
        string tac_ref;
        string pay_type = "";
        CheckBox chk;
      
        string pcode;
        string[] tmp = txtparty.Text.Split(':');
        if (tmp.Length < 3) { btnproceed.Visible = false; return; } else { btnproceed.Visible = true; }
        
        string ref_no = tmp[1];
               
        itm = det.GetDetByRef(ref_no);


        if (itm.Rows.Count < 1)
        {
            return;
        }
        else
        {
            int slno=0;
            string itemdet;

            HtmlTableRow hrow;

            //tblhtml.Rows.Clear();

            foreach (SCBLDataSet.PuTr_PO_Det_ScblRow dr in itm.Rows)
            {
                slno = slno + 1;

                pcode = hdr.GetHdrDataByRef(dr.PO_Det_Ref)[0].PO_Hdr_Dcode.ToString();
                txtdate.Text = hdr.GetHdrDataByRef(dr.PO_Det_Ref)[0].PO_Hdr_DATE.ToShortDateString();

                //quodt = new SCBLDataSet.quotation_detDataTable();
                //quodt = qdet.GetDataByRefParty(dr.PO_Det_Quo_Ref.ToString(), pcode);
                
               
                itemdet = dr.PO_Det_Itm_Desc.ToString();
                
                hrow = new HtmlTableRow();
                
                hrow.Cells.Add(new HtmlTableCell());
                hrow.Cells.Add(new HtmlTableCell());
                hrow.Cells.Add(new HtmlTableCell());
                hrow.Cells.Add(new HtmlTableCell());
                hrow.Cells.Add(new HtmlTableCell());
                hrow.Cells.Add(new HtmlTableCell());
                hrow.Cells.Add(new HtmlTableCell());
                hrow.Cells.Add(new HtmlTableCell());
                hrow.Cells.Add(new HtmlTableCell());
                hrow.Cells.Add(new HtmlTableCell());
               
                hrow.Cells[0].InnerText = slno.ToString();
                hrow.Cells[1].InnerText = dr.PO_Det_Icode;
                hrow.Cells[2].InnerText = itemdet.ToString();


                if (dr.PO_Det_Specification == "")
                    hrow.Cells[3].InnerText = ".";
                else
                    hrow.Cells[3].InnerText = dr.PO_Det_Specification;

                if (dr.PO_Det_Brand == "")
                    hrow.Cells[4].InnerText = ".";
                else
                    hrow.Cells[4].InnerText = dr.PO_Det_Brand;

                if (dr.PO_Det_Origin == ".")
                    hrow.Cells[5].InnerText = "";
                else
                    hrow.Cells[5].InnerText = dr.PO_Det_Origin;

                if (dr.PO_Det_Packing == "")
                    hrow.Cells[6].InnerText = "";
                else
                    hrow.Cells[6].InnerText = dr.PO_Det_Packing;

               
                hrow.Cells[7].InnerText = dr.PO_Det_Lin_Qty.ToString() + " " + dr.PO_Det_Itm_Uom.ToString();
                hrow.Cells[8].InnerText = dr.PO_Det_Lin_Rat.ToString("N2");
                hrow.Cells[9].InnerText = ((decimal)dr.PO_Det_Lin_Qty * dr.PO_Det_Lin_Rat).ToString("N2");
                tblhtml.Rows.Add(hrow);
            }

            //

            
            gcnt = 1;
            scnt = 1;
            pcnt = 1;
            tac_ref = ref_no;

            tblgen.Rows.Clear();
            tblspe.Rows.Clear();
            tblpay.Rows.Clear();

            dtlog = log.GetDataByRef(tac_ref);

            foreach (SCBLDataSet.tbl_tac_logRow drlog in dtlog.Rows)
            {
                daycnt = drlog.valid_days;
                hrow = new HtmlTableRow();
                hrow.Cells.Add(new HtmlTableCell());
                hrow.Cells.Add(new HtmlTableCell());
                hrow.Cells.Add(new HtmlTableCell());

                chk = new CheckBox();
                chk.Checked = true;
                chk.ID =  gcnt.ToString() + scnt.ToString() + pcnt.ToString();

                hrow.Cells[1].Controls.Add(chk);
                hrow.Cells[2].InnerHtml = drlog.content_det.ToString();

                switch (drlog.tac_type)
                {
                        
                    case "gen":
                        {
                            hrow.Cells[0].InnerText = gcnt.ToString();
                            tblgen.Rows.Add(hrow);
                            gcnt++;                            
                            break;
                        }

                    case "spe":
                        {
                            hrow.Cells[0].InnerText = scnt.ToString();
                            tblspe.Rows.Add(hrow);
                            scnt++;                            
                            break;
                        }

                    case "pay":
                        {
                            pay_type = drlog.pay_type.ToUpper();
                            hrow.Cells[0].InnerText = pcnt.ToString();
                            tblpay.Rows.Add(hrow);
                            pcnt++;                            
                            break;
                        }

                }
            }

            lblpaytype.Text = pay_type;
           // celgen.InnerHtml = genstr;
           // celspe.InnerHtml = spestr;
            //celpay.InnerHtml = paystr;
 
            //

           
        }

    }

    private void readyData()
    {
        PuMa_Par_AdrTableAdapter adr = new PuMa_Par_AdrTableAdapter();
        ErpDataSet.PuMa_Par_AdrRow row;
        PuTr_PO_Hdr_ScblTableAdapter hdr = new PuTr_PO_Hdr_ScblTableAdapter();
        string[] tempdata, tmp;

        tmp = txtparty.Text.Split(':');

        if (tmp.Length < 3) return;

        string pdet = tmp[1] + ":" + tmp[2];
       
        string pcode = hdr.GetHdrDataByRef(tmp[1])[0].PO_Hdr_Dcode.ToString();

        tempdata = new string[13];
        tempdata[0] = txtdate.Text;
        tempdata[1] = tmp[2].ToString();
        tempdata[2] = txtsub.Text;
        tempdata[3] = txtfrom.Text;
        row = adr.GetDataByAdrCode(pcode)[0];

        tempdata[4] = pdet;
        tempdata[5] = row.Par_Adr_Line_1 + " " + row.Par_Adr_Line_2 + " " + row.Par_Adr_Line_3 + " " + row.Par_Adr_Line_4 + " " + row.Par_Adr_Line_5;
               
        //tempdata[9] = celvaliddays.InnerHtml;
        tempdata[10] = tmp[1];
        tempdata[11] = lblpaytype.Text;
        tempdata[12] = row.Par_Adr_Email_Id;
        Session[clsStatic.sessionTempPrintData] = tempdata;
        Session[clsStatic.sessionTempHtmlTable] = tblhtml;
        Session[clsStatic.sessionGenHtmlTable] = tblgen;
        Session[clsStatic.sessionSpeHtmlTable] = tblspe;
        Session[clsStatic.sessionPayHtmlTable] = tblpay;

    }

   
   
    protected void btnproceed_Click(object sender, EventArgs e)
    {
        readyData();
        Response.Redirect("./frm_po_send_view.aspx");
    }
    protected void txtparty_TextChanged(object sender, EventArgs e)
    {

    }
}
