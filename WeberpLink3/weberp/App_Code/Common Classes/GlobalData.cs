using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.IO;

/// <summary>
/// Summary description for GlobalData
/// </summary>
public class GlobalData
{
    public static string SessionUserId = "pfuserid";
    public static string SessionUserName = "pfusername";
    public static string SessionRefNo = "pfrefno";
    public static string SessionRefForView = "pfrefnoforview";
    public static string SessionCurPageIndex = "pfcurpageindex";
    public static string SessionDtFrom = "pfdtfrom";
    public static string SessionDtTo = "pfdtto";
    public static string SessionALL = "pfall";
    public static string SessionUserActBy  = "pfuseractby";
    public static string SessionAppointData = "pfappointdata";
    public static string sessionReportDet = "*_%^&**scblsessionsessionReportDet*_%^&**";
    public static string sessionReportDocument = "*_%^&**scblsessionsessionReportdocument*_%^&**";
    public static string sessionConnectionstring = "*_%^&**sessionConnectionstring*_%^&**";

	public GlobalData()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static void ConfirmBox(Button btn , string strMessage)
    {
        btn.Attributes.Add("onclick", "return confirm('" + strMessage + "');");
    }

    public static string PassEnc(string originalPassword)
    {
        string enc;

        Byte[] originalBytes;
        Byte[] encodedBytes;

        MD5 md5;
        md5 = new MD5CryptoServiceProvider();

        originalBytes = System.Text.ASCIIEncoding.Default.GetBytes(originalPassword);
        encodedBytes = md5.ComputeHash(originalBytes);

        if(originalPassword == "")
            enc = "";
        else
            enc = BitConverter.ToString(encodedBytes);
      

        return enc;

    }

    
    public static bool SendMail(string senderemail, string sendername, string rec_add, string rec_name, string m_sub, string msgbody)
    {
        bool fl = true;
        
        try {
            
            string ms;


            ms = "mail.link3.net";

            SmtpClient smtp = new SmtpClient(ms);
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress(senderemail, sendername);

            msg.To.Add(new MailAddress(rec_add, rec_name));

            msg.Subject = m_sub;

            msg.Body = msgbody;

            smtp.Send(msg);

             }
        catch
            {
                fl = false;
            }
            
        return fl;
   }

    public static int SetMsg()
    {
        return 0;
    }

    public static bool CheckUserAuthentication()
    {
        if (HttpContext.Current.Session.Count == 0) HttpContext.Current.Response.Redirect("./frm_login.aspx");
        return true;
    }

    public static bool CheckADMAuthentication()
    {
        if(HttpContext.Current.Session.Count == 0) HttpContext.Current.Response.Redirect("./frm_login.aspx");        
        return true;
    }


    //Attachment
    //string strFileName = null;
        
    //    SmtpClient smtp = new SmtpClient("mail.link3.net");
    //    MailMessage msg = new MailMessage();
    //    msg.From = new MailAddress("monju@link3.net");

    //    msg.To.Add(new MailAddress("monju@link3.net"));

    //    msg.Subject = "Attachment Test";

    //    if (FileUpload1.PostedFile != null)
    //    {

    //        HttpPostedFile ulFile = FileUpload1.PostedFile;
            
    //        int nFileLen = ulFile.ContentLength;
            
    //        if (nFileLen > 0)
    //        {
                                
    //            strFileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
    //            strFileName = Server.MapPath("attachments") + "\\" +strFileName;
               

    //            FileUpload1.PostedFile.SaveAs(strFileName);

    //            Attachment attach = new Attachment(strFileName);

    //            msg.Attachments.Add(attach);
             
               
    //        }
    //    }

    //    msg.Body = "Message body";

    //    smtp.Send(msg);
    //    try
    //    {
    //        if (strFileName != null)
    //            File.Delete(strFileName);
    //    }
    //    catch { }


}
