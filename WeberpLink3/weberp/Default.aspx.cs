using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibraryDAL;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        AutoLoginFunction();

        clsStatic.CheckUserAuthentication();
    }


    public void AutoLoginFunction()
    {

        if (Request.QueryString["LoginId"] == null)
        {
            return;
        }
        //Response.Redirect("./modules/workflow/frm_com_inbox.aspx");

        string key = Request.QueryString["LoginId"].ToString();

        string uid = clsEncryptDecrypt.ValidateLoginKey(key);

        if (uid == "") return;

      


        try
        {
            ConnConnect.setConnection();
            clsSystem usr = new clsSystem();
            dsLinkoffice.tblUserInfoDataTable dtUsr = new dsLinkoffice.tblUserInfoDataTable();

            dtUsr = usr.GetUserById(uid);

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


                    current.UserAdID = uid;
                    current.UserAdPass = "";
                   
                    Response.Redirect("~/Default.aspx");
                }

                return;
            }
            else
            {
                return;
            }

        }
        catch (Exception e)
        {
            return;
        }


    }

}
