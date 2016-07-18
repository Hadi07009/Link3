﻿using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Configuration;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.WebControls;
using System.Net.Mail;
using LibraryDAL;
using System.Net;
using System.Net.NetworkInformation;


/// <summary>
/// Summary description for clsStatic
/// </summary>
public static class clsStatic
{
    public static String sessionTreeIndex = "*_%^&**scblTreeIndex*_%^&**";
    public static String sessionUserDataset = "*_%^&**scblUserDataset*_%^&**";
    public static String sessionTempDatatable = "*_%^&**scblTempDatatable*_%^&**";
    public static String sessionRoutingPendingByItem = "*_%^&**scblRoutingPendingByItem*_%^&**";
    public static String sessionRoutingPendingByReq = "*_%^&**scblRoutingPendingByReq*_%^&**";
    public static String sessionPOApprovalPending = "*_%^&**scblPOApprovalPending*_%^&**";
    public static String sessionCurrentRefFocus = "*_%^&**scblcurrentreffocus*_%^&**";
    public static String sessionPnlUpdateFlag = "*_%^&**scblsessionPnlUpdateFlag*_%^&**";

    public static String sessionTempHtmlTable = "*_%^&**scblTempHtmlTable*_%^&**";
    public static String sessionGenHtmlTable = "*_%^&**scblgenHtmlTable*_%^&**";
    public static String sessionSpeHtmlTable = "*_%^&**scblspeHtmlTable*_%^&**";
    public static String sessionPayHtmlTable = "*_%^&**scblpayHtmlTable*_%^&**";
    public static String sessionTermsandCond = "*_%^&**scblTermsandCond*_%^&**";
    public static String sessionTempPrintData = "*_%^&**scblTempPrintData*_%^&**";
    public static String sessionTempPartyCode = "*_%^&**scblTempPartyCode*_%^&**";
    public static String sessionPoAppRef = "*_%^&**scblsessionPoAppref*_%^&**";
    public static String sessionAdvRefNo = "*_%^&**scblsessionadvrefno*_%^&**";
    public static String sessionPoForwardDet = "*_%^&**scblsessionPoForwardDet*_%^&**";
    public static String sessionSelvalforQuo = "*_%^&**scblsessionSelvalforQuo*_%^&**";
    public static String sessionPartySelFlag = "*_%^&**scblsessionPartySelFlag*_%^&**";
    public static String sessionItemSelForPO = "*_%^&**scblsessionItemSelForPO*_%^&**";
    public static String sessionPartySelForPO = "*_%^&**scblsessionPartySelForPO*_%^&**";
    public static String sessionReportDet = "*_%^&**scblsessionsessionReportDet*_%^&**";
    public static String sessionReportDocument = "*_%^&**scblsessionsessionReportdocument*_%^&**";
    public static String sessionFirstTimeCheck = "*_%^&**scblsessionsessionReportDet*_%^&**";
    public static String sessionMrrDetData = "*_%^&**scblsessionMrrDetData*_%^&**";
    public static String sessionMPRDetData = "*_%^&**scblsessionMPRDetData*_%^&**";
    public static String sessionQueryString = "*_%^&**scblsessionQueryString*_%^&**";
    public static String sessionQuotationRef = "*_%^&**scblsessionQuotationRef*_%^&**";
    public static String sessionPoReportPrm = "*_%^&**scblsessionPoReportPrm*_%^&**";
    //public static String sessionProdRef = "*_%^&**scblsessionProdref*_%^&**";
    //public static String sessionProdItmRef = "*_%^&**scblsessionProdItmref*_%^&**";
    //public static String sessionProdRefPost = "*_%^&**scblsessionProdrefpost*_%^&**";
    //public static String sessionProdItmRefPost = "*_%^&**scblsessionProdItmrefpost*_%^&**";

    public static String ViewStateDataTable = "*_%^&**scblViewStateDataTable*_%^&**";
    public static String ViewStateSortDirection = "*_%^&**scblViewStateSortDirection*_%^&**";
    public static String ViewStateSortExpression = "*_%^&**scblViewStateSortExpression*_%^&**";
    public static String ViewStateItItemsFlg = "*_%^&**scblViewStateItItemsFlg*_%^&**";
    public static String ViewStateCommandArgument = "*_%^&**scblViewStateCommandArgument*_%^&**";
    public static String sessionSelvalforItmCode = "*_%^&**scblsessionSelvalforItmCode*_%^&**";




    public static void MsgConfirmBox(Button btn, string strMessage)
    {
        strMessage = strMessage.Replace("'", "\\'");
        btn.Attributes.Add("onclick", "return confirm('" + strMessage + "');");

    }
    public static void MsgConfirmBox(LinkButton btn, string strMessage)
    {
        strMessage = strMessage.Replace("'", "\\'");
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

    public static void CheckUserAuthentication(bool isPermissionChk)
    {
        CheckUserAuthentication();
        if (!isPermissionChk) return;


        string[] tmp;
        string curpath = HttpContext.Current.Request.Url.AbsoluteUri.ToUpper();

        tmp = curpath.Split(new string[] { "MODULES" }, StringSplitOptions.None);

        if (current.UserNode.ToString().ToUpper().Contains(tmp[1])) return;

        //foreach (dsLinkoffice.tblNodePermRow dr in current.UserNode.Rows)
        //{
        //    tmp = dr.NodeUrl.Split('~');
        //    if(tmp.Length>1)
        //        if(curpath.Contains(tmp[1]))
        //        {
        //            current.PermissionPrm = dr.NodePerm;
        //            current.FormParameter = dr.NodeParam;
        //            return;
        //        }
        //}

        HttpContext.Current.Response.Redirect("~/Default.aspx");


    }

    public static void CheckUserAuthentication()
    {
        current.PermissionPrm = "";
        current.FormParameter = "";

        try
        {
            if (HttpContext.Current.Session.Count == 0)
            {
               // recover_session();
            }
            else if (current.UserId == null)
            {
                //recover_session();
            }
            else if (current.UserId == "")
            {
                //recover_session();
            }
        }
        catch
        {
            GotoHome();
            
        }

        if (current.UserId == null) GotoHome();


        if (current.UserId == "")   GotoHome();


    }


    private static void GotoHome()
    {
        string logouturl = System.Configuration.ConfigurationSettings.AppSettings["logouturl"].ToString();
        if (logouturl == "")
        {
            string curl = HttpContext.Current.Request.Url.AbsoluteUri;
            string pref;
            //string loginKey = clsEncryptDecrypt.GetLoginKey(current.UserId);

            if (curl.ToUpper().Contains("203"))
            {
                pref = "http://office.link3.net/";
            }
            else
            {
                pref = "http://office.link3.net/";
            }

            HttpContext.Current.Response.Redirect(pref + "Login/Home/Login");
        }
        else
        {
            HttpContext.Current.Response.Redirect(logouturl);
        }
    }

    private static void recover_session()
    {
        string sitepref = ConfigurationManager.AppSettings["sitepref"];

        current.UserId = HttpContext.Current.Response.Cookies[sitepref]["UserId"];
        current.UserName = HttpContext.Current.Response.Cookies[sitepref]["UserName"];
        current.UserDepartment = HttpContext.Current.Response.Cookies[sitepref]["UserDepartment"];
        current.UserDesignation = HttpContext.Current.Response.Cookies[sitepref]["UserDesignation"];
        current.UserEmail = HttpContext.Current.Response.Cookies[sitepref]["UserEmail"];
        current.UserNode = HttpContext.Current.Response.Cookies[sitepref]["UserNode"];
        current.CompanyName = HttpContext.Current.Response.Cookies[sitepref]["CompanyName"];
        current.CompanyAddress = HttpContext.Current.Response.Cookies[sitepref]["CompanyAddress"];

        HttpContext.Current.Response.Cookies[sitepref].Expires = DateTime.Now.AddDays(1);

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


    public static bool SendMail(string senderemail, string sendername, LibraryDTO.clsEmailReceiver[] rec_det, string m_sub, string msgbody)
    {
        bool fl = true;

        try
        {

            string ms;

            ms = System.Configuration.ConfigurationSettings.AppSettings["smtpserver"].ToString();

            SmtpClient smtp = new SmtpClient(ms);
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress(senderemail, sendername);

            if (rec_det == null)
            {
                msg.To.Add(new MailAddress("masud.mis@sevencircle-bd.com", "MIS SCBL"));
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

    public static bool SendMail(string senderemail, string sendername, string m_sub, string msgbody)
    {
        bool fl = true;

        try
        {

            string ms;

            ms = System.Configuration.ConfigurationSettings.AppSettings["smtpserver"].ToString();

            SmtpClient smtp = new SmtpClient(ms);
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress(senderemail, sendername);

            msg.To.Add(new MailAddress("masud.mis@sevencircle-bd.com", "MIS SCBL"));

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
                System.Web.UI.WebControls.Table table = new System.Web.UI.WebControls.Table();

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
        string htt = "";
        GridView gv;
        System.Web.UI.HtmlControls.HtmlTable tbl;
        System.Web.UI.HtmlControls.HtmlTableRow row;
        System.Web.UI.HtmlControls.HtmlTableCell cel;

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
            else if (current is GridView)
            {
                control.Controls.Remove(current);
                gv = (GridView)current;
                tbl = new System.Web.UI.HtmlControls.HtmlTable();

                row = new System.Web.UI.HtmlControls.HtmlTableRow();
                for (int j = 0; j < gv.HeaderRow.Cells.Count; j++)
                {
                    cel = new System.Web.UI.HtmlControls.HtmlTableCell();
                    cel.InnerHtml = "<b>" + gv.HeaderRow.Cells[j].Text + "</b>";
                    row.Cells.Add(cel);
                }

                tbl.Rows.Add(row);

                foreach (GridViewRow gr in gv.Rows)
                {
                    row = new System.Web.UI.HtmlControls.HtmlTableRow();
                    for (int j = 0; j < gr.Cells.Count; j++)
                    {
                        cel = new System.Web.UI.HtmlControls.HtmlTableCell();
                        cel.InnerText = gr.Cells[j].Text;
                        row.Cells.Add(cel);
                    }

                    tbl.Rows.Add(row);
                }

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                tbl.RenderControl(hw);
                htt = sb.ToString().Replace("&amp;#39;", "'").Replace("&quot;", "''").Replace("&amp;", "&");
                control.Controls.AddAt(i, new LiteralControl(htt));
            }

            if (current.HasControls())
            {
                PrepareControlForExport(current);
            }
        }
    }

    public static bool VoucherPrint(string constr, string RefNo, string RefNo1, string TableName, string jrnType, string frmdate, string todate)
    {

        return DataProcess.ExecuteQuery(constr, "exec VoucherPrint1 '" + RefNo + "','" + RefNo1 + "','" + TableName + "','" + jrnType + "','" + frmdate + "','" + todate + "'");
    }


    public static bool Sendmail(string sid, string sname, string rid, string rname, string ccid, string msub, string mbody, string ConnectionStr)
    {
        bool flg = false;
        try
        {
            string smtpadr = "";
            string passkey = "";
            DataTable dt = getSmtp(ConnectionStr);
            if (dt.Rows.Count > 0)
            {
                smtpadr = dt.Rows[0]["Smtp"].ToString();
                sid = dt.Rows[0]["MailFrom"].ToString();
                sname = dt.Rows[0]["Name"].ToString();
                passkey = dt.Rows[0]["Password"].ToString();
            }

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(smtpadr);
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.From = new System.Net.Mail.MailAddress(sid, sname);

            NetworkCredential Credentials = new NetworkCredential(sid, passkey);
            smtp.Credentials = Credentials;


            msg.To.Add(new System.Net.Mail.MailAddress(rid));
            string[] cary = ccid.Split(':');
            if (cary.Length >= 1)
            {
                for (int i = 0; i < cary.Length; i++)
                {
                    if (cary[i].Trim() != "")
                    {
                        msg.CC.Add(new System.Net.Mail.MailAddress(cary[i].Trim()));
                    }
                }
            }
            msg.Subject = msub;
            msg.Body = mbody;
            smtp.Send(msg);
            flg = true;
        }
        catch
        {
        }
        return flg;
    }

    private static DataTable getSmtp(string ConnectionStr)
    {
        DataTable dt = new DataTable();
        string sql = "select MailFrom,Name,[Password],Smtp from TblSmtpSetup where MailTypeId=1 and status=1";
        dt = DataProcess.GetData(ConnectionStr, sql);
        return dt;
    }


}

public static class current
{

    public static String CompanyCode
    {
        get { return ConfigurationManager.AppSettings["companycode"].ToString(); }

    }
    public static String CompanyName
    {
        get
        {
            if (HttpContext.Current.Session["@#$$%@)CompanyName(@^&^&%"] == null) { return ""; }

            return HttpContext.Current.Session["@#$$%@)CompanyName(@^&^&%"].ToString();
        }
        set { HttpContext.Current.Session["@#$$%@)CompanyName(@^&^&%"] = value; }

    }

    public static String CompanyAddress
    {
        get
        {
            if (HttpContext.Current.Session["@#$$%@)CompanyAddress(@^&^&%"] == null) { return ""; }

            return HttpContext.Current.Session["@#$$%@)CompanyAddress(@^&^&%"].ToString();
        }
        set { HttpContext.Current.Session["@#$$%@)CompanyAddress(@^&^&%"] = value; }

    }


    public static String UserId
    {


        get
        {
            if (HttpContext.Current.Session["@#$$%@)UserId(@^&^&%"] == null) { return ""; }

            return HttpContext.Current.Session["@#$$%@)UserId(@^&^&%"].ToString();
        }
        set { HttpContext.Current.Session["@#$$%@)UserId(@^&^&%"] = value; }


    }

    public static String UserName
    {
        get { return HttpContext.Current.Session["@#$$%@)UserName(@^&^&%"].ToString(); }
        set { HttpContext.Current.Session["@#$$%@)UserName(@^&^&%"] = value; }
    }

    public static String UserDesignation
    {
        get { return HttpContext.Current.Session["@#$$%@)UserDesignation(@^&^&%"].ToString(); }
        set { HttpContext.Current.Session["@#$$%@)UserDesignation(@^&^&%"] = value; }
    }

    public static String UserDepartment
    {
        get { return HttpContext.Current.Session["@#$$%@)UserDepartment(@^&^&%"].ToString(); }
        set { HttpContext.Current.Session["@#$$%@)UserDepartment(@^&^&%"] = value; }
    }

    public static String UserEmail
    {
        get { return HttpContext.Current.Session["@#$$%@)Email(@^&^&%"].ToString(); }
        set { HttpContext.Current.Session["@#$$%@)Email(@^&^&%"] = value; }
    }
    public static String UserNode
    {
        get
        {
            if (HttpContext.Current.Session["@#$$%@)UserNode(@^&^&%"] == null) { return ""; }
            return HttpContext.Current.Session["@#$$%@)UserNode(@^&^&%"].ToString();
        }

        set { HttpContext.Current.Session["@#$$%@)UserNode(@^&^&%"] = value; }
    }

    public static string UserAdID
    {
        get
        {
            if (HttpContext.Current.Session["@#$$%@)UserAdID(@^&^&%"] == null) { return ""; }
            return HttpContext.Current.Session["@#$$%@)UserAdID(@^&^&%"].ToString();
        }

        set { HttpContext.Current.Session["@#$$%@)UserAdID(@^&^&%"] = value; }
    }

    public static string UserAdPass
    {
        get
        {
            if (HttpContext.Current.Session["@#$$%@)UserAdPass(@^&^&%"] == null) { return ""; }
            return HttpContext.Current.Session["@#$$%@)UserAdPass(@^&^&%"].ToString();
        }

        set { HttpContext.Current.Session["@#$$%@)UserAdPass(@^&^&%"] = value; }
    }

    public static String PermissionPrm
    {
        get { return HttpContext.Current.Session["@#$$%@)PermissionPrm(@^&^&%"].ToString(); }
        set { HttpContext.Current.Session["@#$$%@)PermissionPrm(@^&^&%"] = value; }
    }
    public static String FormParameter
    {
        get { return HttpContext.Current.Session["@#$$%@)FormParameter(@^&^&%"].ToString(); }
        set { HttpContext.Current.Session["@#$$%@)FormParameter(@^&^&%"] = value; }
    }

    public static LibraryDTO.clsReport SessionReport
    {
        get { return (LibraryDTO.clsReport)HttpContext.Current.Session["@#$$%@)SessionReport(@^&^&%"]; }
        set { HttpContext.Current.Session["@#$$%@)SessionReport(@^&^&%"] = value; }
    }

    public static ReportDocument ReportDocument
    {
        get { return (ReportDocument)HttpContext.Current.Session["@#$$%@)ReportDocument(@^&^&%"]; }
        set { HttpContext.Current.Session["@#$$%@)FormParameter(@^&^&%"] = value; }
    }
}
