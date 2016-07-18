using System;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using LibraryDTO;
using LibraryDAL;
using LibraryDAL.SCBLDataSetTableAdapters;

public partial class frm_po_send_mail : System.Web.UI.Page
{
   

    protected void Page_Load(object sender, EventArgs e)
    {
               
        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";
        getpage();
        if (!Page.IsPostBack)
        {
            string[] tempdata = (string[])Session[clsStatic.sessionTempPrintData];
            txtto.Text = tempdata[12];
        }
        else
        {

        }
          
    }
    private void getpage()
    {
        HtmlTable htbl = (HtmlTable)Session[clsStatic.sessionTempHtmlTable];
        HtmlTable gentbl = (HtmlTable)Session[clsStatic.sessionGenHtmlTable];
        HtmlTable spetbl = (HtmlTable)Session[clsStatic.sessionSpeHtmlTable];
        HtmlTable paytbl = (HtmlTable)Session[clsStatic.sessionPayHtmlTable];
        CheckBox chk;

        HtmlTableRow hrow;

        string[] tempdata = (string[])Session[clsStatic.sessionTempPrintData];
        decimal tot = 0;

        string genstr = "";
        string spestr = "";
        string paystr = "";

        int gcnt = 1;
        int scnt = 1;
        int pcnt = 1;

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
            hrow.Cells.Add(new HtmlTableCell());
            hrow.Cells.Add(new HtmlTableCell());

            hrow.Cells[0].InnerText = hr.Cells[0].InnerText;
            hrow.Cells[1].InnerText = hr.Cells[2].InnerText;

            try
            {
                if (hr.Cells[0].InnerText == "\r\n                            Sl") goto skip;

                hrow.Cells[0].InnerText = hr.Cells[0].InnerText;
                hrow.Cells[2].InnerText = hr.Cells[3].InnerText;
                hrow.Cells[3].InnerText = hr.Cells[4].InnerText;
                hrow.Cells[4].InnerText = hr.Cells[5].InnerText;
                hrow.Cells[5].InnerText = hr.Cells[6].InnerText;
                hrow.Cells[6].InnerText = hr.Cells[7].InnerText;
                hrow.Cells[7].InnerText = hr.Cells[8].InnerText;
                hrow.Cells[8].InnerText = hr.Cells[9].InnerText;
                tblhtml.Rows.Add(hrow);
                tot = tot + Convert.ToDecimal(hr.Cells[9].InnerText);

            skip: ;
            }
            catch
            {
            }
        }


        lbldate.Text = tempdata[0];
        lblto.Text = tempdata[1];
        lblsub.Text = tempdata[2];
        lblfrom.Text = tempdata[3];
        lbladd.Text = tempdata[5];
        lblporef.Text = tempdata[10];

        lbltot.Text = tot.ToString("N2") + " [" + NumerictowordClass.FNumber(tot.ToString("N2")) + "]";



        foreach (HtmlTableRow hr in gentbl.Rows)
        {
            chk = new CheckBox();
            if (hr.Cells[0].InnerText == "\r\n                        Sl") goto skip;
            try
            {
                chk = (CheckBox)hr.Cells[1].Controls[0];
                if (chk != null)
                    if (chk.Checked)
                    {
                        genstr = genstr + gcnt.ToString() + ". " + hr.Cells[2].InnerText + "<br />";
                        gcnt++;
                    }
            }
            catch
            {
            }
        skip: ;
        }

        foreach (HtmlTableRow hr in spetbl.Rows)
        {
            chk = new CheckBox();
            if (hr.Cells[0].InnerText == "\r\n                            Sl") goto skip2;
            try
            {
                chk = (CheckBox)hr.Cells[1].Controls[0];
                if (chk != null)
                    if (chk.Checked)
                    {
                        spestr = spestr + scnt.ToString() + ". " + hr.Cells[2].InnerText + "<br />";
                        scnt++;
                    }
            }
            catch
            {
            }
        skip2: ;
        }


        foreach (HtmlTableRow hr in paytbl.Rows)
        {
            chk = new CheckBox();
            if (hr.Cells[0].InnerText == "\r\n                            Sl") goto skip3;
            try
            {
                chk = (CheckBox)hr.Cells[1].Controls[0];
                if (chk != null)
                    if (chk.Checked)
                    {
                        paystr = paystr + pcnt.ToString() + ". " + hr.Cells[2].InnerText + "<br />";
                        pcnt++;
                    }
            }
            catch
            {
            }
        skip3: ;
        }

        if (genstr != "") lblgen.Text = "GENERAL TERMS";
        if (spestr != "") lblspe.Text = "SPECIAL TERMS";
        if (paystr != "") lblpay.Text = "PAYMENT TERMS (" + tempdata[11] + " ADVANCE)";

        genterms.InnerHtml = genstr;
        spterms.InnerHtml = spestr;
        payterms.InnerHtml = paystr;

        

        //daycount.InnerHtml = tempdata[9];

    }
   

    protected void Button1_Click(object sender, EventArgs e)
    {
        string[] rec;
        string[] rcc;
        int i, cnt;
        string sid;
        LibraryDAL.dsLinkofficeTableAdapters.tblUserInfoTableAdapter usr = new LibraryDAL.dsLinkofficeTableAdapters.tblUserInfoTableAdapter();

        System.Net.Mail.SmtpClient smtp = new SmtpClient(System.Configuration.ConfigurationSettings.AppSettings["smtpserver"].ToString());
        //System.Net.Mail.SmtpClient smtp = new SmtpClient("mail.link3.net");
        System.Net.Mail.MailMessage msg = new MailMessage();

        
        //smtp.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;


        try
        {
            sid = usr.GetUserByCode(current.UserId.ToString())[0].UserEmail.ToString();

            if (sid == "")
            {
                msg.From = new MailAddress("masud.mis@sevencircle-bd.com", "Shun Shing Cement Mills Ltd.");
            }
            else
            {
                msg.From = new MailAddress(sid, current.UserName.ToString());
            }

            rec = txtto.Text.Split(',', ';');

            rcc = txtcc.Text.Split(',', ';');

            cnt = rec.Length;
            for (i = 0; i < cnt; i++)
            {
                if (rec[i].Trim() != "")
                    msg.To.Add(new System.Net.Mail.MailAddress(rec[i].Trim()));
            }
            cnt = rcc.Length;
            for (i = 0; i < cnt; i++)
            {
                if (rcc[i].Trim() != "")
                    msg.CC.Add(new System.Net.Mail.MailAddress(rcc[i].Trim()));
            }



            msg.Subject = "Purchase Order";

            msg.IsBodyHtml = true;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            tbldet.RenderControl(hw);



            if (rdoformat.SelectedIndex == 0)
            {
                msg.Body = sb.ToString();
            }
            else
            {

                //template file
                string name = "c:\\purchase_order.doc";
                FileStream fs = new FileStream(name, FileMode.Create, FileAccess.Write);

                StreamWriter sw1 = new StreamWriter(fs, System.Text.Encoding.GetEncoding("gb2312"));

                sw1.WriteLine(sb.ToString());

                sw1.Close();

                object SourceFileName = "c:\\purchase_order.doc";
                object newFileName = "c:\\purchase_order.pdf";
                Doc2PDFAtServerClass.word2PdfFcih(SourceFileName, newFileName);
                msg.Body = "Please find attachment file of Purchase Order.";

                msg.Attachments.Add(new Attachment("c:\\purchase_order.pdf"));

            }

            smtp.Send(msg);
            lblsent.Text = "Email sent.";
            lblsent.Visible = true;
            btnsend.Enabled = false;


        }
        catch
        {
            lblsent.Text = "Email sending error, Try again pls.";
            lblsent.Visible = true;
        }
    }
    static public class Doc2PDFAtServerClass
    {

        static public void word2PdfFcih(object SourceFileName, object newFileName)
        {

            //Pid++;

            Microsoft.Office.Interop.Word.ApplicationClass MSdoc = null;


            //object Source = "d:\\Document" + Pid.ToString(System.Globalization.CultureInfo.CurrentCulture) + ".doc";

            object Source = SourceFileName;

            object readOnly = false;

            object Unknown = System.Reflection.Missing.Value; //Type.Missing;

            object missing = Type.Missing;

            try
            {

                //Creating the instance of Word Application

                if (MSdoc == null)

                    MSdoc = new Microsoft.Office.Interop.Word.ApplicationClass();

                MSdoc.Visible = false;

                MSdoc.Documents.Open(ref Source, ref Unknown,

                ref readOnly, ref Unknown, ref Unknown,

                ref Unknown, ref Unknown, ref Unknown,

                ref Unknown, ref Unknown, ref Unknown,

                ref Unknown, ref Unknown, ref Unknown, ref Unknown, ref Unknown);

                MSdoc.Application.Visible = false;

                MSdoc.WindowState = Microsoft.Office.Interop.Word.WdWindowState.wdWindowStateMinimize;

                object FileName = newFileName;

                object FileFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF;

                object LockComments = false;

                object AddToRecentFiles = false;

                object ReadOnlyRecommended = false;

                object EmbedTrueTypeFonts = true;

                object SaveNativePictureFormat = false;

                object SaveFormsData = false;

                object SaveAsAOCELetter = false;

                //object Encoding = MsoEncoding.msoEncodingUSASCII;

                object InsertLineBreaks = false;

                object AllowSubstitutions = false;

                object LineEnding = Microsoft.Office.Interop.Word.WdLineEndingType.wdCRLF;

                object AddBiDiMarks = false;

                /*


                to get more details about SaveAs(...) function and it's parameter ,read this microsoft's link


                http://msdn2.microsoft.com/en-us/library/aa662158(office.10).aspx


                */


                MSdoc.ActiveDocument.SaveAs(ref FileName, ref FileFormat, ref LockComments,
                ref missing, ref AddToRecentFiles, ref missing,

                ref ReadOnlyRecommended, ref EmbedTrueTypeFonts,

                ref SaveNativePictureFormat, ref SaveFormsData,

                ref SaveAsAOCELetter, ref /*Encoding*/missing, ref InsertLineBreaks,

                ref AllowSubstitutions, ref LineEnding, ref AddBiDiMarks);

            }

            catch (FileLoadException e)
            {

                Console.WriteLine(e.Message + "Error");

            }

            catch (FileNotFoundException e)
            {

                Console.WriteLine(e.Message + "Error");

            }

            catch (FormatException e)
            {

                Console.WriteLine(e.Message + "Error");

            }

            finally
            {

                if (MSdoc != null)
                {
                    MSdoc.Documents.Close(ref Unknown, ref Unknown, ref Unknown);
                    //WordDoc.Application.Quit(ref Unknown, ref Unknown, ref Unknown);
                }

                // for closing the application

                // WordDoc.Quit(ref Unknown, ref Unknown, ref Unknown);

                MSdoc.Quit(ref Unknown, ref Unknown, ref Unknown);

                MSdoc = null;

            }

        }

    }
}

