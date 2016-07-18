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

public partial class frm_tender_inquiry_print : System.Web.UI.Page
{
   

    protected void Page_Load(object sender, EventArgs e)
    {
               
        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";
        getpage();
        if (!Page.IsPostBack)
        {
            
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
                    tblhtml.Rows.Add(hrow);
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
    
}

