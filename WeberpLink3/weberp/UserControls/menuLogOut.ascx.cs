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
using LibraryDAL;
using LibraryDAL.dsLinkofficeTableAdapters;
public partial class UserControls_menuLogOut : System.Web.UI.UserControl
{
    private string _catalogiconimageurl;
    private string _description;
    private string _subtitle;
    private string _title;
    private string _titleiconimageurl;
    private string _titleurl;

    string IWebPart.CatalogIconImageUrl
    {
        get { return _catalogiconimageurl; }
        set { _catalogiconimageurl = value; }
    }
    string IWebPart.Description
    {
        get { return _description; }
        set { _description = value; }
    }
    string IWebPart.Subtitle
    {
        get { return _subtitle; }
    }
    string IWebPart.Title
    {
        get { return _title; }
        set { _title = value; }
    }
    string IWebPart.TitleIconImageUrl
    {
        get { return _titleiconimageurl; }
        set { _titleiconimageurl = value; }
    }

    string IWebPart.TitleUrl
    {
        get { return _titleurl; }
        set { _titleurl = value; }
    }

    private void Page_Load(object sender, System.EventArgs e)
    {

        PNL.Visible = true;
        Panel1.Visible = true;
        Panel2.Visible = true;
        Panel3.Visible = true;
        Panel4.Visible = true;
        
        
        _title = "  Settings";
        _description = "";
        _titleiconimageurl = "~/App_Themes/LinkofficeTheme/images/menuicon.jpg";
        if (!Page.IsPostBack)
        {
            //if (Session.Count == 0)
            //    Ul1.Visible = false;
        }
        

    }
    protected void lnkLogout_Click(object sender, EventArgs e)
    {
     //   Session.Abandon();
        Response.Redirect("~/frmLogin.aspx");
               
       
    }

    protected void lnkChangepass_Click(object sender, EventArgs e)
    {
        clsSystem usr = new clsSystem();
       

        if (txtNewpass.Text == txtConpass.Text)
        {
            if (usr.GetUserByIdPass(current.UserId,txtpass.Text, current.CompanyCode).Count == 0) return;

            if (usr.UpdateUserPasswd(current.UserId, txtNewpass.Text, current.CompanyCode) == 1)
            {
                goto succeed;
            }
            
        }
       
        ModalPopupExtender4.Show();
        return;
    succeed: 
        ModalPopupExtender3.Show();
        
    }




    protected void Button1_Click(object sender, EventArgs e)
    {
        
    }

    protected void lnkMyprofile_Click(object sender, EventArgs e)
    {
        lblid.Text = current.UserId;
        txtname.Text = current.UserName;
        txtdesig.Text = current.UserDesignation;
        txtdept.Text = current.UserDepartment;
        txtemail.Text = current.UserEmail;
        ModalPopupExtender5.Show();
        
    }

    protected void btnprofileupdate_Click(object sender, EventArgs e)
    {
        clsSystem usr = new clsSystem();
        if(usr.UpdateUserProfile(lblid.Text, txtname.Text, txtdesig.Text, txtdept.Text, txtemail.Text, current.CompanyCode)>0)
        {
            current.UserName = txtname.Text;
            current.UserDepartment = txtdept.Text;
            current.UserDesignation = txtdesig.Text;
            current.UserEmail = txtemail.Text;

        }
        ModalPopupExtender3.Show();
    }
}
