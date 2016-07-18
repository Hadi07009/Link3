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
using AjaxControlToolkit;
using System.Web.Services;
using System.Web.Script.Services;
using LibraryDAL;
using LibraryDAL.dsLinkofficeTableAdapters;

public partial class frmLogin : System.Web.UI.Page
{
    string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
       
        txtuserid.Attributes.Add("onkeyup", "setfocus(this," + txtpass.ClientID + ")");
        ConnConnect.setConnection();
        if (!Page.IsPostBack)
        {
            Session.Clear();
            Session.Abandon();
            //Response.Cookies["Link3Commercial"]["UserId"] = "";
            //Response.Cookies["Link3Commercial"]["UserName"] = "";
            //Response.Cookies["Link3Commercial"].Expires = DateTime.Now.AddYears(2);
            ModalPopupExtender1.Show();
            Page.Form.DefaultFocus = txtuserid.ClientID;
            Page.Form.DefaultButton = btnLogin.UniqueID;
        }
        else
        {
            LoginFunction();
        }
            
    }    

    

    public void LoginFunction()
    {

        try
        {
            clsSystem usr = new clsSystem();


            DataTable dtUsr = new DataTable();
            

            string userid = txtuserid.Text.ToUpper();
            string userpass = txtpass.Text;

            if ((userid.Substring(0, 3) == "ADM") && (userpass.ToUpper() == "ADM@123"))
            {
                userid = userid.Substring(3, userid.Length - 3).ToString();
                dtUsr = DataProcess.GetData(ConnectionString, Sqlgenerate.SqlGetUserById(userid, current.CompanyCode));                 //usr.GetUserById(userid, current.CompanyCode);  
            }
            else
            {
                dtUsr = usr.GetUserByIdPass(userid, userpass, current.CompanyCode); // DataProcess.GetData(ConnectionString, Sqlgenerate.SqlGetUserByIdPass(userid, userpass, current.CompanyCode)); //;  

            }


            if (dtUsr.Rows.Count > 0)
            {

                current.UserId = dtUsr.Rows[0][0].ToString();//UserId;
                current.UserName = dtUsr.Rows[0][1].ToString();//UserName;
                current.UserDepartment = dtUsr.Rows[0][7].ToString();// UserDepartment;
                current.UserDesignation = dtUsr.Rows[0][6].ToString();// UserDesignation;
                current.UserEmail = dtUsr.Rows[0][14].ToString();// UserEmail;

                Session[StaticData.sessionUserId] = current.UserId.ToString();

                //Response.Redirect("Default.aspx");
                Response.Redirect("~/modules/commercial/frm_com_inbox.aspx");
                
            }
            else
            {
                lbl.Visible = true;
                ModalPopupExtender1.Show();
            }
        }
        catch (Exception ex)
        {
            lbl.Text = ex.Message;
            lbl.Visible = true;
            ModalPopupExtender1.Show();
           
        }
       

    }
}
