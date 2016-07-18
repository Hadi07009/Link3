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
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using LibraryDTO;



/// <summary>
/// Summary description for StaticData
/// </summary>
public class StaticData
{
    public static String sessionUserId = "*_%^&**link3userId*_%^&**";
    public static String sessionUserName = "*_%^&**link3username*_%^&**";
    public static String sessionTreeIndex = "*_%^&**scblTreeIndex*_%^&**";
    public static String sessionUserDataset = "*_%^&**link3UserDataset*_%^&**";
    public static String sessionRefNo = "*_%^&**link3Refno*_%^&**";
    public static String sessionRunningRefNo = "*_%^&**link3runningRefno*_%^&**";
    public static String sessionRunningCommentRef = "*_%^&**link3runningCommentsRefno*_%^&**";
    public static String sessionDocPerNo = "*_%^&**link3sessionDocPerNo*_%^&**";
    public static String ViewStateDataTable = "*_%^&**link3ViewStateDataTable*_%^&**";
    public static String ViewStateSortDirection = "*_%^&**link3ViewStateSortDirection*_%^&**";
    public static String ViewStateSortExpression = "*_%^&**link3ViewStateSortExpression*_%^&**";
    public static String ViewStateItItemsFlg = "*_%^&**link3ViewStateItItemsFlg*_%^&**";
    public static String sessionparameterinfo = "*_%^&**Link3Viewparameterinfo*_%^&**";
    public static String sessionClientId = "*_%^&**link3sessionClientId*_%^&**";
    public static String sessionClientName = "*_%^&**link3sessionClientName*_%^&**";
    public static String sessionTask = "*_%^&**link3sessionTask*_%^&**";
    public static String sessionTempDatatable = "*_%^&**link3sessionTempDatatable*_%^&**";
    public static String SessionClientCurStatus = "*_%^&**link3SessionClientCurStatus*_%^&**";

    public static String sessionPageView = "*_%^&**link3sessionPageView*_%^&**";
    public static String sessionCategoryLoad = "*_%^&**link3sessionCategoryLoad*_%^&**";
    public static String sessionDdType = "*_%^&**link3sessionDdType*_%^&**";
    public static String sessionServiceType = "*_%^&**link3sessionServiceType*_%^&**";
    public static String sessionCompany = "*_%^&**link3sessionCompany*_%^&**";
    public static String sessionAdrCode = "*_%^&**link3sessionAdrCode*_%^&**";
    public static String sessionClientCode = "*_%^&**link3sessionClientCode*_%^&**";
    public static String sessionIns = "*_%^&**link3sessionIns*_%^&**";
    public static String sessionServiceDelivery = "*_%^&**link3ServiceDelivery*_%^&**";
    public static String sessionServiceReport = "*_%^&**link3sessionServiceReport*_%^&**";
    public static String sessionSurveyRef = "*_%^&**link3sessionSurveyRef*_%^&**";
    public static String sessionSurveyRegenrated = "*_%^&**link3sessionSurveyRegenrated*_%^&**";
    public static String sessionInsMaxRefNo = "*_%^&**link3sessionInsMaxRefNo*_%^&**";
    public static String sessionBranchAdrNewCode = "*_%^&**link3sessionBranchAdrNewCode*_%^&**";
    public static String sessionClientNewCode = "*_%^&**link3sessionClientNewCode*_%^&**";
    public static String sessionClientNewSl = "*_%^&**link3sessionClientNewSl*_%^&**";
    public static String sessionClientBrCode = "*_%^&**link3sessionClientBrCode*_%^&**";
    public static String sessionDepartmentName = "*_%^&**link3sessionDepartmentName*_%^&**";
    public static String sessionDesignationName = "*_%^&**link3sessionDesignationName*_%^&**";
    public static String sessionRequisitinRefNo = "*_%^&**link3sessionRequisitinRefNo*_%^&**";
    public static String sessionCrRefNo = "*_%^&**link3sessionCrRefNo*_%^&**";
    public static String sessionRequisitinRefNoOthers = "*_%^&**link3sessionRequisitinRefNoOthers*_%^&**";


    public static String ViewStateCommandArgument = "*_%^&**scblViewStateCommandArgument*_%^&**";
    public static String sessionItemSelForPO = "*_%^&**scblsessionItemSelForPO*_%^&**";
    public static String sessionPartySelForPO = "*_%^&**scblsessionPartySelForPO*_%^&**";
    public static String sessionPartySelFlag = "*_%^&**scblsessionPartySelFlag*_%^&**";

    public static String sessionCurrentRefFocus = "*_%^&**scblcurrentreffocus*_%^&**";
    public static String sessionPnlUpdateFlag = "*_%^&**scblsessionPnlUpdateFlag*_%^&**";
    public static String sessionCurPlant = "*_%^&**sessionCurPlant*_%^&**";
    public static String sessionSelvalforQuo = "*_%^&**scblsessionSelvalforQuo*_%^&**";
    public static String sessionTempPrintData = "*_%^&**scblTempPrintData*_%^&**";
    public static String sessionTempHtmlTable = "*_%^&**scblTempHtmlTable*_%^&**";
    public static String sessionTermsandCond = "*_%^&**scblTermsandCond*_%^&**";
    public static String sessionQuotationRef = "*_%^&**scblsessionQuotationRef*_%^&**";
    public static String sessionGenHtmlTable = "*_%^&**scblgenHtmlTable*_%^&**";
    public static String sessionSpeHtmlTable = "*_%^&**scblspeHtmlTable*_%^&**";
    public static String sessionPayHtmlTable = "*_%^&**scblpayHtmlTable*_%^&**";
    
   


    public StaticData()
    {


    }

    public static void checkSessionReset()
    {
        if (HttpContext.Current.Session.Count == 0)
        {
            HttpContext.Current.Session[StaticData.sessionUserId] = HttpContext.Current.Request.Cookies["Link3Complain"]["UserId"].ToString();
            HttpContext.Current.Session[StaticData.sessionUserName] = HttpContext.Current.Request.Cookies["Link3Complain"]["UserName"].ToString();
            HttpContext.Current.Session[StaticData.sessionPnlUpdateFlag] = true;
        }


    }

    //public static void checkUserAuthentication()
    //{
    //    if (HttpContext.Current.Session.Count == 0)
    //    {
    //        try
    //        {
    //            HttpContext.Current.Session[StaticData.sessionUserId] = HttpContext.Current.Request.Cookies["Link3Complain"]["UserId"].ToString();
    //            HttpContext.Current.Session[StaticData.sessionUserName] = HttpContext.Current.Request.Cookies["Link3Complain"]["UserName"].ToString();
    //            HttpContext.Current.Session[StaticData.sessionDepartmentName] = HttpContext.Current.Request.Cookies["Link3Complain"]["department"].ToString();
    //            HttpContext.Current.Session[StaticData.sessionDesignationName] = HttpContext.Current.Request.Cookies["Link3Complain"]["designation"].ToString();
    //        }
    //        catch
    //        {
    //            HttpContext.Current.Response.Redirect("~/ClientSide/frm_login.aspx");

    //        }
            
    //    }
    //    else
        
    //    {
    //        HttpContext.Current.Request.Cookies["Link3Complain"].Expires = DateTime.Now.AddMinutes(50);
    //    }
    //        //

    //}

    public static void checkUserAuthentication()
    {
        if (HttpContext.Current.Session.Count == 0)
            HttpContext.Current.Response.Redirect("~/ClientSide/frm_login.aspx");


    }



    public static void MsgConfirmBox(Button btn, string strMessage)
    {
        btn.Attributes.Add("onclick", "return confirm('" + strMessage + "');");

    }
    public static void MsgConfirmBox(LinkButton btn, string strMessage)
    {
        btn.Attributes.Add("onclick", "return confirm('" + strMessage + "');");

    }
    public static void MsgConfirmBox(string strMessage)
    {
        strMessage = strMessage.Replace("'", "\\'");
        string script = "<script type= \"text/javascript\">alert('" + strMessage + "'); </script> ";
        Page page = HttpContext.Current.CurrentHandler as Page;
        if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
        {
            //page.ClientScript.RegisterClientScriptBlock(typeof (clsStatic), "alert", script);
            ScriptManager.RegisterStartupScript(page, typeof(Page), "temp", script, false);
        }
    }

    public static string DateTimeToStringForSorting(DateTime Date)
    {
        string ret = "";
        ret = string.Format("{0:0000}", Date.Year) + "-" + string.Format("{0:00}", Date.Month) + "-" + string.Format("{0:00}", Date.Day);
        return ret;
    }



    public static decimal NumericConvertion(double val)
    {
        return Convert.ToDecimal(val.ToString("N2"));
    }

    public static double NumericConvertion(decimal val)
    {
        return Convert.ToDouble(val.ToString("N2"));
    }

    public static double NumericSameConvertion(double val)
    {
        return Convert.ToDouble(val.ToString("N2"));
    }

    public static decimal NumericSameConvertion(decimal val)
    {
        return Convert.ToDecimal(val.ToString("N2"));
    }





    public static bool SendMail(string senderemail, string sendername, clsEmailReceiver[] rec_det, string m_sub, string msgbody)
    {
        bool fl = true;

        try
        {

            string ms;


            ms = "mail.link3.net";

            SmtpClient smtp = new SmtpClient(ms);
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress(senderemail, sendername);

            if (rec_det == null)
            {
                //msg.To.Add(new MailAddress("monju@link3.net", "M Monjurul Islam"));
            }
            else
            {
                for (int i = 1; i < rec_det.Length; i++)
                {
                    if (rec_det[i - 1] != null)
                        if (rec_det[i - 1].Rid != "")
                            msg.To.Add(new MailAddress(rec_det[i - 1].Rid, rec_det[i - 1].Rname));
                }

            }

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

    public static void Export(string fileName, GridView gv)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader(
            "content-disposition", string.Format("attachment; filename={0}", fileName));
        HttpContext.Current.Response.ContentType = "application/ms-excel";
      

        using (System.IO.StringWriter sw = new System.IO.StringWriter())
        {
            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            {
                //  Create a table to contain the grid
                Table table = new Table();

                //  include the gridline settings
                table.GridLines = gv.GridLines;

                //  add the header row to the table
                if (gv.HeaderRow != null)
                {
                    PrepareControlForExport(gv.HeaderRow);
                    table.Rows.Add(gv.HeaderRow);
                }

                //  add each of the data rows to the table
                foreach (GridViewRow row in gv.Rows)
                {
                    PrepareControlForExport(row);
                    table.Rows.Add(row);
                }

                //  add the footer row to the table
                if (gv.FooterRow != null)
                {
                    PrepareControlForExport(gv.FooterRow);
                    table.Rows.Add(gv.FooterRow);
                }

                //  render the table into the htmlwriter
                table.RenderControl(htw);

                //  render the htmlwriter into the response
                HttpContext.Current.Response.Write(sw.ToString());
                HttpContext.Current.Response.End();
            }
        }
    }
    public static void ExportPdf(string fileName, GridView gv)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader(
            "content-disposition", string.Format("attachment; filename={0}", fileName));
        //  HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.ContentType = "application/pdf";

        using (System.IO.StringWriter sw = new System.IO.StringWriter())
        {
            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            {
                //  Create a table to contain the grid
                Table table = new Table();

                //  include the gridline settings
                table.GridLines = gv.GridLines;

                //  add the header row to the table
                if (gv.HeaderRow != null)
                {
                    PrepareControlForExport(gv.HeaderRow);
                    table.Rows.Add(gv.HeaderRow);
                }

                //  add each of the data rows to the table
                foreach (GridViewRow row in gv.Rows)
                {
                    PrepareControlForExport(row);
                    table.Rows.Add(row);
                }

                //  add the footer row to the table
                if (gv.FooterRow != null)
                {
                    PrepareControlForExport(gv.FooterRow);
                    table.Rows.Add(gv.FooterRow);
                }

                //  render the table into the htmlwriter
                table.RenderControl(htw);

                //  render the htmlwriter into the response
                HttpContext.Current.Response.Write(sw.ToString());
                HttpContext.Current.Response.End();
            }
        }
    }
    private static void PrepareControlForExport(Control control)
    {
        for (int i = 0; i < control.Controls.Count; i++)
        {
            Control current = control.Controls[i];
            if (current is LinkButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
            }
            else if (current is ImageButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
            }
            else if (current is HyperLink)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
            }
            else if (current is DropDownList)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
            }
            else if (current is CheckBox)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
            }
            else if (current is Button)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as Button).Text));
            }

            if (current.HasControls())
            {
                PrepareControlForExport(current);
            }
        }
    }

}


public static class clsEncryptDecrypt
{
    static readonly string PasswordHash = "Pa@Sw0rd";
    static readonly string SaltKey = "Sa@LT&KEY";
    static readonly string VIKey = "vi@1B2c3D4e5F6g7H8";

    public static string GetLoginKey(string userid)
    {
        string key = userid.ToUpper() + ":" + DateTime.Now.Year.ToString() + ":" + DateTime.Now.Month.ToString() + ":" + DateTime.Now.Day.ToString() + ":" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
        key = Encrypt(key);
        return key;
    }
    public static string ValidateLoginKey(string key)
    {

        try
        {
            key = key.Replace(" ", "+");
            string orgkey = Decrypt(key);
            string[] tmpval = orgkey.Split(':');
            int mm;

            if (tmpval.Length < 6)
            {
                return "";
            }

            if ((Convert.ToInt32(tmpval[1]) == DateTime.Now.Year) && (Convert.ToInt32(tmpval[2]) == DateTime.Now.Month) && (Convert.ToInt32(tmpval[3]) == DateTime.Now.Day) && (Convert.ToInt32(tmpval[4]) == DateTime.Now.Hour))
            {
                mm = Convert.ToInt32(tmpval[5]);

                if ((mm - DateTime.Now.Minute > 10) || (mm - DateTime.Now.Minute < -10))
                {
                    return "";
                }
                else
                {
                    return tmpval[0];
                }

            }
            else
            {
                return "";
            }


        }
        catch (Exception ex)
        {
            return "";
        }

    }



    private static string Encrypt(string plainText)
    {
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

        byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
        var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
        var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

        byte[] cipherTextBytes;

        using (var memoryStream = new MemoryStream())
        {
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();
                cipherTextBytes = memoryStream.ToArray();
                cryptoStream.Close();
            }
            memoryStream.Close();
        }
        return Convert.ToBase64String(cipherTextBytes);
    }

    private static string Decrypt(string encryptedText)
    {
        byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
        byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
        var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

        var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
        var memoryStream = new MemoryStream(cipherTextBytes);
        var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        byte[] plainTextBytes = new byte[cipherTextBytes.Length];

        int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
        memoryStream.Close();
        cryptoStream.Close();
        return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
    }


}




