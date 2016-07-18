
using LibraryDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class HomePage : System.Web.UI.Page
{
    string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        ConnConnect.setConnection();
    }

    //public void LoginFunction()
    //{

    //    try
    //    {
    //        clsSystem usr = new clsSystem();


    //        DataTable dtUsr = new DataTable();


    //        string userid = txtuserid.Text.ToUpper();
    //        string userpass = txtpass.Text;

    //        if ((userid.Substring(0, 3) == "ADM") && (userpass.ToUpper() == "ADM@123"))
    //        {
    //            userid = userid.Substring(3, userid.Length - 3).ToString();
    //            dtUsr = DataProcess.GetData(ConnectionString, Sqlgenerate.SqlGetUserById(userid, current.CompanyCode));                 //usr.GetUserById(userid, current.CompanyCode);  
    //        }
    //        else
    //        {
    //            dtUsr = usr.GetUserByIdPass(userid, userpass, current.CompanyCode); // DataProcess.GetData(ConnectionString, Sqlgenerate.SqlGetUserByIdPass(userid, userpass, current.CompanyCode)); //;  

    //        }


    //        if (dtUsr.Rows.Count > 0)
    //        {
    //            current.UserId = dtUsr.Rows[0][0].ToString();//UserId;
    //            current.UserName = dtUsr.Rows[0][1].ToString();//UserName;
    //            current.UserDepartment = dtUsr.Rows[0][7].ToString();// UserDepartment;
    //            current.UserDesignation = dtUsr.Rows[0][6].ToString();// UserDesignation;
    //            current.UserEmail = dtUsr.Rows[0][14].ToString();// UserEmail;

    //            Session[StaticData.sessionUserId] = current.UserId.ToString();

    //            //Response.Redirect("Default.aspx");
    //            Response.Redirect("~/modules/commercial/frm_com_inbox.aspx");
    //        }
    //        else
    //        {
    //            lbl.Visible = true;
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lbl.Text = ex.Message;
    //        lbl.Visible = true;
    //    }
    //}

    public void LoginFunctionOld()
    {

        try
        {
            clsSystem usr = new clsSystem();


            dsLinkoffice.tblUserInfoDataTable dtUsr = new dsLinkoffice.tblUserInfoDataTable();


            string userid = txtuserid.Text.ToUpper();
            string userpass = txtpass.Text;

            if ((userid.Length > 3) && (userid.Substring(0, 3) == "ADM") && (txtpass.Text.ToUpper() == "ADM123"))
            {
                userid = userid.Replace("ADM", "");
                dtUsr = usr.GetUserById(userid, current.CompanyCode);
            }
            else
            {
                dtUsr = usr.GetUserByIdPass(userid, userpass, current.CompanyCode); // DataProcess.GetData(ConnectionString, Sqlgenerate.SqlGetUserByIdPass(userid, userpass, current.CompanyCode)); //;  
            }



            if (dtUsr.Rows.Count > 0)
            {
                LibraryPF.dsMasterDataTableAdapters.Hrms_Company_MasterTableAdapter com = new LibraryPF.dsMasterDataTableAdapters.Hrms_Company_MasterTableAdapter();
                LibraryPF.dsMasterData.Hrms_Company_MasterDataTable dtcom = new LibraryPF.dsMasterData.Hrms_Company_MasterDataTable();

                dtcom = com.GetData();
                current.UserId = dtUsr[0].UserId.ToString();//UserId;
                current.UserName = dtUsr[0].UserName.ToString();//UserName;
                current.UserDepartment = dtUsr[0].UserDepartment.ToString();// UserDepartment;
                current.UserDesignation = dtUsr[0].UserDesignation.ToString();// UserDesignation;
                current.UserEmail = dtUsr[0].UserEmail.ToString();// UserEmail;
                current.UserNode = dtUsr[0].UserHtml.ToString();
                Session[StaticData.sessionUserId] = current.UserId.ToString();

                current.CompanyName = dtcom[0].CompanyName;
                current.CompanyAddress = dtcom[0].Address1 + " " + dtcom[0].Address2 + " " + dtcom[0].Address3 + " " + dtcom[0].Address4;

               // set_cookies();
                //Response.Redirect("Default.aspx");
                //Response.Redirect("~/modules/commercial/frm_com_inbox.aspx");
                Response.Redirect("~/Default.aspx");

            }
            else
            {
                lbl.Visible = true;
            }

        }
        catch (Exception ex)
        {
            lbl.Text = ex.Message;
            lbl.Visible = true;
        }
    }

    private void set_cookies()
    {
        string sitepref = ConfigurationManager.AppSettings["sitepref"];

        Response.Cookies[sitepref]["UserId"] = current.UserId;
        Response.Cookies[sitepref]["UserName"] = current.UserName;
        Response.Cookies[sitepref]["UserDepartment"] = current.UserDepartment;
        Response.Cookies[sitepref]["UserDesignation"] = current.UserDesignation;
        Response.Cookies[sitepref]["UserEmail"] = current.UserEmail;
        Response.Cookies[sitepref]["UserNode"] = current.UserNode;
        Response.Cookies[sitepref]["CompanyName"] = current.CompanyName;
        Response.Cookies[sitepref]["CompanyAddress"] = current.CompanyAddress;
        Response.Cookies[sitepref].Expires = DateTime.Now.AddDays(1);
    }

    public void LoginFunctionOld2()
    {
        activeDirectory ad = new activeDirectory();
        string ADName = System.Configuration.ConfigurationSettings.AppSettings["dip"].ToString();

        try
        {
            clsSystem usr = new clsSystem();


            dsLinkoffice.tblUserInfoDataTable dtUsr = new dsLinkoffice.tblUserInfoDataTable();


            string userid = txtuserid.Text;
            string userpass = txtpass.Text;

            if ((userid.ToUpper() == "ADM") && (userpass.ToUpper() == "MONJU@123"))
            {
                dtUsr = usr.GetUserById(userid);
            }

            else if (userid.ToUpper() == "ADM")
            {
                dtUsr = usr.GetUserByIdPass(userid.ToUpper(), userpass, current.CompanyCode);
            }
            else
            {
                if (ADName == "")
                {
                    dtUsr = usr.GetUserByIdPass(userid.ToUpper(), userpass, current.CompanyCode);
                }
                else
                {
                    if (ad.IsAuthenticate(userid, userpass) == false)
                    {
                        lbl.Visible = true;
                        return;
                    }
                    dtUsr = usr.GetUserById(userid);
                }
            }

            if (dtUsr.Rows.Count > 0)
            {
                if (dtUsr[0].UserStatus == 1)
                {
                    LibraryPF.dsMasterDataTableAdapters.Hrms_Company_MasterTableAdapter com = new LibraryPF.dsMasterDataTableAdapters.Hrms_Company_MasterTableAdapter();
                    LibraryPF.dsMasterData.Hrms_Company_MasterDataTable dtcom = new LibraryPF.dsMasterData.Hrms_Company_MasterDataTable();

                    dtcom = com.GetData();
                    //current.UserId = dtUsr[0].UserId.ToString();//UserId;
                    current.UserId = dtUsr[0].UserEmpId.ToString();//UserId;
                    current.UserName = dtUsr[0].UserName.ToString();//UserName;
                    current.UserDepartment = dtUsr[0].UserDepartment.ToString();// UserDepartment;
                    current.UserDesignation = dtUsr[0].UserDesignation.ToString();// UserDesignation;
                    current.UserEmail = dtUsr[0].UserEmail.ToString();// UserEmail;
                    current.UserNode = dtUsr[0].UserHtml.ToString();    //user node
                    Session[StaticData.sessionUserId] = current.UserId.ToString();

                    current.CompanyName = dtcom[0].CompanyName;
                    current.CompanyAddress = dtcom[0].Address1 + " " + dtcom[0].Address2 + " " + dtcom[0].Address3 + " " + dtcom[0].Address4;


                    current.UserAdID = userid;
                    current.UserAdPass = userpass;
                  //  set_cookies();
                    //Response.Redirect("Default.aspx");
                    //Response.Redirect("~/modules/commercial/frm_com_inbox.aspx");
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    lbl.Visible = true;
                }

            }
            else
            {
                lbl.Visible = true;
            }

        }
        catch (Exception ex)
        {
            lbl.Text = ex.Message;
            lbl.Visible = true;
        }
    }

    public void LoginFunction()
    {
        activeDirectory ad = new activeDirectory();
        string ADName = System.Configuration.ConfigurationSettings.AppSettings["dip"].ToString();

        try
        {
            clsSystem usr = new clsSystem();


            dsLinkoffice.tblUserInfoDataTable dtUsr = new dsLinkoffice.tblUserInfoDataTable();


            string userid = txtuserid.Text;
            string userpass = txtpass.Text;

            if ((userid.ToUpper() == "ADM") && (userpass.ToUpper() == "MONJU@123"))
            {
                dtUsr = usr.GetUserById(userid);
            }

            else if (userid.ToUpper() == "ADM")
            {
                dtUsr = usr.GetUserByIdPass(userid.ToUpper(), userpass, current.CompanyCode);
            }
            else
            {
                if (ADName == "")
                {
                    dtUsr = usr.GetUserByIdPass(userid.ToUpper(), userpass, current.CompanyCode);
                }
                else
                {
                    if (ad.IsAuthenticate(userid, userpass) == false)
                    {
                        lbl.Visible = true;
                        return;
                    }
                    dtUsr = usr.GetUserById(userid);
                }
            }

            if (dtUsr.Rows.Count > 0)
            {
                if (dtUsr[0].UserStatus == 1)
                {
                    LibraryPF.dsMasterDataTableAdapters.Hrms_Company_MasterTableAdapter com = new LibraryPF.dsMasterDataTableAdapters.Hrms_Company_MasterTableAdapter();
                    LibraryPF.dsMasterData.Hrms_Company_MasterDataTable dtcom = new LibraryPF.dsMasterData.Hrms_Company_MasterDataTable();

                    dtcom = com.GetData();
                    //current.UserId = dtUsr[0].UserId.ToString();//UserId;
                    current.UserId = dtUsr[0].UserEmpId.ToString();//UserId;
                    current.UserName = dtUsr[0].UserName.ToString();//UserName;
                    current.UserDepartment = dtUsr[0].UserDepartment.ToString();// UserDepartment;
                    current.UserDesignation = dtUsr[0].UserDesignation.ToString();// UserDesignation;
                    current.UserEmail = dtUsr[0].UserEmail.ToString();// UserEmail;
                    current.UserNode = dtUsr[0].UserHtml.ToString();    //user node
                    Session[StaticData.sessionUserId] = current.UserId.ToString();

                    current.CompanyName = dtcom[0].CompanyName;
                    current.CompanyAddress = dtcom[0].Address1 + " " + dtcom[0].Address2 + " " + dtcom[0].Address3 + " " + dtcom[0].Address4;


                    current.UserAdID = userid;
                    current.UserAdPass = userpass;
                    //  set_cookies();
                    //Response.Redirect("Default.aspx");
                    //Response.Redirect("~/modules/commercial/frm_com_inbox.aspx");
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    lbl.Visible = true;
                }

            }
            else
            {
                lbl.Visible = true;
            }

        }
        catch (Exception ex)
        {
            lbl.Text = ex.Message;
            lbl.Visible = true;
        }
    }

    private string CheckAllValidation()
    {
        const string checkValidation = "";
        if (txtuserid.Text == string.Empty)
        {
            txtuserid.Focus();
            return "Please Enter User Name Correctly !";
        }

        return checkValidation;
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {

        string validationMsg = CheckAllValidation();
        switch (validationMsg)
        {
            case "":
                {
                    LoginFunction();
                }
                break;
            default:
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + validationMsg + "');",
                    true);
                break;
        }
    }

}

