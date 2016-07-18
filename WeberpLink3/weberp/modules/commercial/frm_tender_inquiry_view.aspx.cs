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

public partial class frm_tender_inquiry_view : System.Web.UI.Page
{
   

    protected void Page_Load(object sender, EventArgs e)
    {
               
        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";
        getpage();
        if (!Page.IsPostBack)
        {
            HtmlInputButton btnnewwindow = new HtmlInputButton();
            btnnewwindow.Attributes.Add("onclick ", "window.open('./frm_tender_inquiry_print.aspx')");
            btnnewwindow.ID = "btnnewwindow";
            btnnewwindow.Value = "Print Version";
            btnnewwindow.Attributes.Add("class", "btn2");
            lnkView.Controls.Add(btnnewwindow);

            HtmlInputButton btnnewwindow2 = new HtmlInputButton();
            btnnewwindow2.Attributes.Add("onclick ", "window.open('./frm_tender_inquiry_mail.aspx')");
            btnnewwindow2.ID = "btnnewwindow2";
            btnnewwindow2.Value = "Email Version";
            btnnewwindow2.Attributes.Add("class", "btn2");
            lnkMail.Controls.Add(btnnewwindow2);
        }
        else
        {

        }
          
    }
    private void getpage()
    {
        HtmlTable htbl = (HtmlTable)Session[clsStatic.sessionTempHtmlTable];
        HtmlTableRow hrow;
        TextBox tbx;
        CheckBox chk;
        string[] tempdata;
        clsTandC[] tac = new clsTandC[60];         
        int len,i;

        decimal tot = 0;   

        string genstr = "";
        string spestr = "";
        string paystr = "";
        int slno = 1;

        foreach (HtmlTableRow hr in htbl.Rows)
        {

            hrow = new HtmlTableRow();

            hrow.Cells.Add(new HtmlTableCell());
            hrow.Cells.Add(new HtmlTableCell());
            hrow.Cells.Add(new HtmlTableCell());
            hrow.Cells.Add(new HtmlTableCell());
            hrow.Cells.Add(new HtmlTableCell());
            hrow.Cells.Add(new HtmlTableCell());
            hrow.Cells.Add(new HtmlTableCell());

            hrow.Cells[0].InnerText = hr.Cells[1].InnerText;
            hrow.Cells[1].InnerText = hr.Cells[4].InnerText;



            try
            {

                if (hr.Cells[1].InnerText == "\r\n                            Sl") goto skip;

                chk = new CheckBox();
                chk = (CheckBox)hr.Cells[0].Controls[0];

                if (chk.Checked)
                {
                    hrow.Cells[0].InnerText = slno.ToString();
                    slno++;
                }

                tbx = new TextBox();
                tbx = (TextBox)hr.Cells[5].Controls[0];
                if (tbx.Text == "")
                    hrow.Cells[2].InnerText = ".";
                else
                    hrow.Cells[2].InnerText = tbx.Text;

                tbx = new TextBox();
                tbx = (TextBox)hr.Cells[6].Controls[0];
                if (tbx.Text == "")
                    hrow.Cells[3].InnerText = ".";
                else
                    hrow.Cells[3].InnerText = tbx.Text;

                tbx = new TextBox();
                tbx = (TextBox)hr.Cells[7].Controls[0];
                if (tbx.Text == "")
                    hrow.Cells[4].InnerText = ".";
                else
                    hrow.Cells[4].InnerText = tbx.Text;

                tbx = new TextBox();
                tbx = (TextBox)hr.Cells[8].Controls[0];
                if (tbx.Text == "")
                    hrow.Cells[5].InnerText = ".";
                else
                    hrow.Cells[5].InnerText = tbx.Text;

                tbx = new TextBox();
                tbx = (TextBox)hr.Cells[9].Controls[0];
                if (tbx.Text == "")
                    hrow.Cells[6].InnerText = ".";
                else
                    hrow.Cells[6].InnerText = tbx.Text;



                if (chk.Checked)
                {
                    tblhtml.Rows.Add(hrow);
                    tot = tot + Convert.ToDecimal(hr.Cells[9].InnerText);
                }

            skip: ;
            }
            catch
            {
            }

        }

        tempdata = (string[])Session[clsStatic.sessionTempPrintData];

        lbldate.Text = tempdata[0];
        lblto.Text = tempdata[1];
        lblsub.Text = tempdata[2];
        lblfrom.Text = tempdata[3];
        lblpaytype.Text = tempdata[5];
        lbladd.Text = tempdata[7];
        lblphone.Text = tempdata[8];
        lblfax.Text = tempdata[9];
        lblemail.Text = tempdata[10];

        tac =(clsTandC[]) Session[clsStatic.sessionTermsandCond];

        len = tac.Length;
        for (i = 0; i < len; i++)
        {
            if (tac[i] == null) goto complete;
            switch(tac[i].Type)
            {
                   
                case "gen":
                    if (genstr != "") genstr += "<br />  <br />";
                    genstr = genstr + (tac[i].Seq+1).ToString() + ". " + tac[i].Data;
                    break;

                case "spe":
                    if (spestr != "") spestr += "  <br />  <br />";
                    spestr = spestr + (tac[i].Seq + 1).ToString() + ". " + tac[i].Data;
                    break;

                case "pay":
                    if (paystr != "") paystr += "<br />  <br />";
                    paystr = paystr + (tac[i].Seq + 1).ToString() + ". " + tac[i].Data;
                    break;
                default: break;

            }
            
        }

    complete:
        genterms.InnerHtml = genstr;
        spterms.InnerHtml = spestr;
        payterms.InnerHtml = paystr;
        

    }
    private void saved_quo_log()
    {
        tbl_tac_logTableAdapter taclog = new tbl_tac_logTableAdapter();
        quotation_logTableAdapter log = new quotation_logTableAdapter();
        CheckBox chk;
        string[] tempdata = (string[])Session[clsStatic.sessionTempPrintData];
        int i, cnt, len;
        string[] tmp;
        string acode, aname, itemdet,ref_no;
        double max_ref;
        HtmlTable htbl = (HtmlTable)Session[clsStatic.sessionTempHtmlTable];
        
        clsTandC[] tac = new clsTandC[60];
        tac = (clsTandC[])Session[clsStatic.sessionTermsandCond];


        max_ref = Convert.ToDouble(log.GetMaxRef()) + 1;

        ref_no = "LOG-" + string.Format("{0:000000}", max_ref);

        tmp = tempdata[6].ToString().Split(':');

        acode = tmp[0].Trim();
        aname = tmp[1].Trim();

        cnt = htbl.Rows.Count;
        itemdet = "";

        for (i = 1; i < cnt; i++)
        {
            chk = new CheckBox();
            chk = (CheckBox)htbl.Rows[i].Cells[0].Controls[0];
            if (chk.Checked)
                itemdet = itemdet + htbl.Rows[i].Cells[2].InnerText + ":" + htbl.Rows[i].Cells[3].InnerText + "+";
        }

        if (itemdet != "")
        {
            log.InsertQuotationlog(ref_no, acode, aname, DateTime.Now, itemdet);
        }

        //insert t&c

        len = tac.Length;
        for (i = 0; i < len; i++)
        {
            if (tac[i] == null) goto complete;
            switch (tac[i].Type)
            {

                case "gen":
                    taclog.Inserttac(ref_no, "INQ", "gen", "", tac[i].Tem_seq, tac[i].Seq + 1, tac[i].Data, 0, "");                    
                    break;

                case "spe":

                    taclog.Inserttac(ref_no, "INQ", "spe", "", tac[i].Tem_seq, tac[i].Seq + 1, tac[i].Data, 0, "");
                    break;

                case "pay":

                    taclog.Inserttac(ref_no, "INQ", "pay", tempdata[4], tac[i].Tem_seq, tac[i].Seq + 1, tac[i].Data, 0, "");
                    break;
                default: break;
            }
        }

    complete:
        btnsave.Enabled = false;

    }
    protected void lnkView_Click(object sender, EventArgs e)
    {

        RegisterStartupScript("click", "<script>window.open('./frm_tender_inquiry_print.aspx');</script>");

    }
    protected void lnkMail_Click(object sender, EventArgs e)
    {
        RegisterStartupScript("click", "<script>window.open('./frm_tender_inquiry_mail.aspx');</script>");
    }
    
    

    protected void btnsave_Click(object sender, EventArgs e)
    {
        saved_quo_log();

    }
    protected void btnagain_Click(object sender, EventArgs e)
    {
        Response.Redirect("./frm_tender_inquiry.aspx");
    }
    
}

