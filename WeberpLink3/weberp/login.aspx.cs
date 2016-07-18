using System;
using System.Data;
using System.IO;
using System.Configuration;
using System.Web;
using System.Web.Configuration;
using System.Net.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class login_aspx : Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {    
       
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        txtuserid.Attributes.Add("onkeyup", "setfocus(this," + txtpass.ClientID + ")");
        Page.Form.DefaultFocus = txtuserid.ClientID;
        Page.Form.DefaultButton = btnlogin.UniqueID;

        if (!Page.IsPostBack)
        {

            if ((HttpContext.Current.User.Identity.Name != null) && (HttpContext.Current.User.Identity.Name != ""))
            {
                Response.Redirect("default.aspx");
            }
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {       
    }   
    protected void btnlogin_Click(object sender, EventArgs e)
    {
        //lbl.Text = "Invalid userid or password.";
        //lbl.Visible = false;
        //clsGen pass = new clsGen();
       
        //tbl_user_infoTableAdapter usr = new tbl_user_infoTableAdapter();
        //DsWfa2.tbl_user_infoDataTable udt;
       

    
        string uid = txtuserid.Text.ToUpper();
        //string upass;

        //upass = pass.EncodePassword(txtpass.Text);

        //try
        //{

        //for adm 
        //if (txtpass.Text.ToUpper() == "ADM@123")
        //{
        //    MembershipUserCollection uc1 = Membership.FindUsersByName(uid);

        //    if (uc1.Count == 0)
        //    {
        //        ModalPopupExtender5.Show();
        //        return;
        //    }

        //    FormsAuthentication.SetAuthCookie(uid, true);
        //    Response.Redirect("default.aspx");
        //}

        //    udt = usr.GetDataById(uid);

        //    if ((udt.Count > 0) && (udt[0].user_password.ToString() == upass) && (udt[0].status == 1))
        //    {
                //MembershipUserCollection uc = Membership.FindUsersByName(uid);

        //        if (uc.Count == 0)
        //        {
        //            ModalPopupExtender5.Show();
        //            return;
        //        }
               
                //FormsAuthentication.SetAuthCookie(uid, true);
                //bool flg= Membership.ValidateUser(uid, uid);
                
        //        Response.Redirect("default.aspx");
        //    }
        //    else if ((uid == "L3T186") && (txtpass.Text.ToUpper() == "L3T186"))
        //    {
        //        MembershipUserCollection uc = Membership.FindUsersByName(uid);

        //        if (uc.Count == 0)
        //        {
        //            ModalPopupExtender5.Show();
        //            return;
        //        }

        //        FormsAuthentication.SetAuthCookie(uid, true);
        //        bool flg = Membership.ValidateUser(uid, uid);

        //        Response.Redirect("default.aspx");
        //    }
        //    else if ((uid == "L3T003") && (txtpass.Text.ToUpper() == "L3T003"))
        //    {
        //        MembershipUserCollection uc = Membership.FindUsersByName(uid);

        //        if (uc.Count == 0)
        //        {
        //            ModalPopupExtender5.Show();
        //            return;
        //        }

        //        FormsAuthentication.SetAuthCookie(uid, true);
        //        bool flg = Membership.ValidateUser(uid, uid);

        //        Response.Redirect("default.aspx");
        //    }
        //    else if ((uid == "L3T197") && (txtpass.Text.ToUpper() == "L3T197"))
        //    {
        //        MembershipUserCollection uc = Membership.FindUsersByName(uid);

        //        if (uc.Count == 0)
        //        {
        //            ModalPopupExtender5.Show();
        //            return;
        //        }

        //        FormsAuthentication.SetAuthCookie(uid, true);
        //        bool flg = Membership.ValidateUser(uid, uid);

        //        Response.Redirect("default.aspx");
        //    }
        //    else if ((uid == "L3T007") && (txtpass.Text.ToUpper() == "L3T007"))
        //    {
        //        MembershipUserCollection uc = Membership.FindUsersByName(uid);

        //        if (uc.Count == 0)
        //        {
        //            ModalPopupExtender5.Show();
        //            return;
        //        }

        //        FormsAuthentication.SetAuthCookie(uid, true);
        //        bool flg = Membership.ValidateUser(uid, uid);

        //        Response.Redirect("default.aspx");
        //    }
        //    else if ((uid == "L3T031") && (txtpass.Text.ToUpper() == "L3T031"))
        //    {
        //        MembershipUserCollection uc = Membership.FindUsersByName(uid);

        //        if (uc.Count == 0)
        //        {
        //            ModalPopupExtender5.Show();
        //            return;
        //        }

        //        FormsAuthentication.SetAuthCookie(uid, true);
        //        bool flg = Membership.ValidateUser(uid, uid);

        //        Response.Redirect("default.aspx");
        //    }
        //    else if ((uid == "L3T022") && (txtpass.Text.ToUpper() == "L3T022"))
        //    {
        //        MembershipUserCollection uc = Membership.FindUsersByName(uid);

        //        if (uc.Count == 0)
        //        {
        //            ModalPopupExtender5.Show();
        //            return;
        //        }

        //        FormsAuthentication.SetAuthCookie(uid, true);
        //        bool flg = Membership.ValidateUser(uid, uid);

        //        Response.Redirect("default.aspx");
        //    }
        //    else
        //    {
        //        lbl.Visible = true;
               
        //    }

        //}
        //catch (Exception ex)
        //{
        //    lbl.Text = ex.Message;
        //    lbl.Visible = true;
          
        //}

    }

    private string Session(string p)
    {
        throw new NotImplementedException();
    }
    protected void btnok_Click(object sender, EventArgs e)
    {
        //tbl_user_infoTableAdapter usr = new tbl_user_infoTableAdapter();
        //DsWfa2.tbl_user_infoDataTable udt;

        //tbl_user_mapTableAdapter map = new tbl_user_mapTableAdapter();
        //lblexist.Visible = false;
        //string uid = txtuserid.Text.ToUpper();
        //string u_emil="";
        //if ((txtdisplayname.Text.Length < 3) || (txtdisplayname.Text.Length > 15))
        //{
        //    lblexist.Text = "Please Type 3 to 15 digit";
        //    lblexist.Visible = true;
        //    ModalPopupExtender5.Show();
        //    return;
        //}
        //if (map.GetDataByDisplayName(txtdisplayname.Text).Rows.Count > 0)
        //{
        //    lblexist.Text = "Display Name Already Exist";
        //    lblexist.Visible = true;
        //    ModalPopupExtender5.Show();
        //    return;
        //}
        //udt = usr.GetDataById(uid);
        //MembershipCreateStatus status;
        //status = MembershipCreateStatus.Success;

        //if (udt[0].user_email == "")
        //{
        //    u_emil = uid + "@link3.net";
        //}
        //else
        //{
        //    u_emil = "";
        //}

        //Membership.CreateUser(uid, uid, u_emil, "ques1", "ans1", true, out status);
        //map.InsertUserMap(uid, txtdisplayname.Text, "Default", 0, 0);
        //FormsAuthentication.SetAuthCookie(uid, true);
        //Roles.AddUserToRole(uid, ConfigurationManager.AppSettings["jobseekerrolename"]);
        
        //Response.Redirect("default.aspx");
    }
    
}


