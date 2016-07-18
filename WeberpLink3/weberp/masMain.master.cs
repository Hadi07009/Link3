
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using LibraryDAL.dsLinkofficeTableAdapters;

public partial class masMain_master : MasterPage
{
    string ConnectionStr = ConfigurationSettings.AppSettings["SCFConnectionString"].Replace("SCF", current.CompanyCode);

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = ConfigurationManager.AppSettings["pagetitle"];
        tblUserInfoTableAdapter map = new tblUserInfoTableAdapter();
        if (current.UserId != null)
            if (current.UserId != "")
            {
                //if (current.UserId != "ADM") LinkButton2.Visible = false;

                lblLoginUser.Text = current.UserName == null ? string.Empty : current.UserName;
            }

        if (IsPostBack == false)
        {
            ddlTema.SelectedValue = Page.Theme;
            //ShowCompanyLogo();
        }

        string sitepref = ConfigurationManager.AppSettings["sitepref"];
        if (current.UserId != null)
            tdtree.InnerHtml = map.GetDataByUserId(current.UserId, current.CompanyCode)[0].UserHtml.Replace("wwwwwwwwww", sitepref);

    }

    protected void ddlTema_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session.Add("TheTheme", ddlTema.SelectedValue);
        Server.Transfer(Request.FilePath);  //just start the page over
    }
    protected void ddlTema_DataBound(object sender, EventArgs e)
    {
        ddlTema.SelectedValue = Page.Theme;
    }

    public void ShowCompanyLogo()
    {
        //lblCompanyLogo.Text = string.Empty;
        //try
        //{
        //    var dtPhoto = DataProcess.GetData(ConnectionStr.ToString(), Sqlgenerate.SqlGetCompanyLogo(""));
        //    if (dtPhoto.Rows.Count > 0 && !Convert.IsDBNull(dtPhoto.Rows[0].ItemArray[0]))
        //    {
        //        var img = (byte[])dtPhoto.Rows[0].ItemArray[0];
        //        string base64string = Convert.ToBase64String(img, 0, img.Length);                
        //        lblCompanyLogo.Text = "<img src='data:image/png;base64," + base64string + "' alt='<br /> Logo <br />  Not <br />  Available ' width='40px' hight='40px' vspace='5px' hspace='5px' />";

        //    }
        //}
        //catch (Exception inSystemExep)
        //{

        //    ScriptManager.RegisterStartupScript(
        //            this,
        //            GetType(),
        //            "MessageBox",
        //            "alert(' Error Occured During Load Company Logo ! ');",
        //            true);
        //}
    }


    private void clear_cookies()
    {
        string sitepref = ConfigurationManager.AppSettings["sitepref"];

        Response.Cookies[sitepref]["UserId"] = null;
        Response.Cookies[sitepref]["UserName"] = null;
        Response.Cookies[sitepref]["UserDepartment"] = null;
        Response.Cookies[sitepref]["UserDesignation"] = null;
        Response.Cookies[sitepref]["UserEmail"] = null;
        Response.Cookies[sitepref]["UserNode"] = null;
        Response.Cookies[sitepref]["CompanyName"] = null;
        Response.Cookies[sitepref]["CompanyAddress"] = null;
        Response.Cookies[sitepref].Expires = DateTime.Now;


    }

    private void clear_session()
    {
        current.UserId = null;
        current.UserName = null;
        current.UserDepartment = null;
        current.UserDesignation = null;
        current.UserEmail = null;
        current.UserNode = null;
        Session[StaticData.sessionUserId] = null;
        current.CompanyName = null;
        current.CompanyAddress = null;
    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        clear_session();
        string logouturl = System.Configuration.ConfigurationSettings.AppSettings["logouturl"].ToString();

        if (logouturl == "")
        {
            string curl = Request.Url.AbsoluteUri;
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

            Response.Redirect(pref + "Login/Home/Login?LogOut=True");
        }
        else
        {
            Response.Redirect(logouturl);
        }

    }

    protected void ImageButtonHome_Click(object sender, ImageClickEventArgs e)
    {
        string curl = Request.Url.AbsoluteUri;
        string pref = curl.Split(new string[] { "login/Home" }, StringSplitOptions.None)[0];
        string loginKey = clsEncryptDecrypt.GetLoginKey(current.UserId);

        if (curl.ToUpper().Contains("203"))
        {
            pref = "http://office.link3.net/";
        }
        else
        {
            pref = "http://office.link3.net/";
        }

        Response.Redirect(pref + "Login/Home/Login?LoginId=" + loginKey);

    }
}


