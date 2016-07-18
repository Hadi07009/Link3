using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibraryDAL.dsLinkofficeTableAdapters;
namespace LibraryDAL
{
    public class clsSystem
    {
        public clsSystem()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public dsLinkoffice.tblUserInfoDataTable GetUserByIdPass(string userid, string password, string companyCode)
        {

            tblUserInfoTableAdapter usr = new tblUserInfoTableAdapter();
            PasswordEncryptDAL pass = new PasswordEncryptDAL();
            string enpass = pass.EncodePassword(password);
            return usr.GetUserByIdPasswd(userid, enpass, (short)1, companyCode);

        }
        public dsLinkoffice.tblUserInfoDataTable GetUserById(string empid, string companyCode)
        {
            tblUserInfoTableAdapter usr = new tblUserInfoTableAdapter();
            return usr.GetDataByUserId(empid, companyCode);

        }

        public dsLinkoffice.tblUserInfoDataTable GetUserById(string userid)
        {
            tblUserInfoTableAdapter usr = new tblUserInfoTableAdapter();
            return usr.GetUserByCode(userid);

        }

        public string[] GetUserList(string prefixText, int count, string companyCode)
        {
            tblUserInfoTableAdapter usr = new tblUserInfoTableAdapter();
            dsLinkoffice.tblUserInfoDataTable dtuser = new dsLinkoffice.tblUserInfoDataTable();
            int maxsize = 100;

            if (prefixText == "*")
            {
                dtuser = usr.GetAllUser(companyCode);
            }
            else
            {
                prefixText = "%" + prefixText + "%";
                dtuser = usr.GetUserSearch(prefixText, companyCode);
            }


            string[] str;

            if (dtuser.Rows.Count > maxsize)
                str = new string[maxsize];
            else
                str = new string[dtuser.Rows.Count];

            int indx = 0;


            foreach (dsLinkoffice.tblUserInfoRow dr in dtuser.Rows)
            {
                str[indx] = dr.UserId.ToString() + ":" + dr.UserName.ToString();
                indx++;

                if (indx == maxsize) break;
            }

            return str;
        }

        public int UpdateUserProfile(string userid, string username, string userdesig, string userdept, string useremail, string companyCode)
        {
            tblUserInfoTableAdapter usr = new tblUserInfoTableAdapter();
            return usr.UpdateUserProfile(username, userdesig, userdept, useremail, userid, companyCode);
        }
        public int UpdateUserInformation(string userid, string userempid, string username, string userdesig, string userdept, string useremail, string usermobile, string usershortname, int userstatus, string companyCode)
        {
            tblUserInfoTableAdapter usr = new tblUserInfoTableAdapter();
            return usr.UpdateUser(username, usershortname, companyCode, userempid, userdesig, userdept, useremail, usermobile, userstatus, userid);                    

        }       

        public int UpdateUserPasswd(string userid, string userpass, string companyCode)
        {
            tblUserInfoTableAdapter usr = new tblUserInfoTableAdapter();
            PasswordEncryptDAL pass = new PasswordEncryptDAL();
            string enpass = pass.EncodePassword(userpass);
            return usr.UpdateUserPassword(enpass, userid, companyCode);
        }

        public int UpdateUserImage(string userid, byte[] userimage, string companyCode)
        {
            tblUserInfoTableAdapter usr = new tblUserInfoTableAdapter();
            return usr.UpdateUserImage(userimage, userid, companyCode);
        }
        public int InsertUser(string userid, string userempid, string username, string userdesig, string userdept, string useremail, string usermobile, byte[] userimage, string usershortname, int userstatus, string companyCode)
        {
            tblUserInfoTableAdapter usr = new tblUserInfoTableAdapter();
            return usr.InsertUser(userid, username, usershortname, companyCode, "", userempid, userdesig, userdept,
                                  useremail, usermobile, userimage, DateTime.Now, DateTime.Now, userstatus,"");
        }


        public dsLinkoffice.tblNodePermDataTable GetNodeByUserId(string userid, string companyCode)
        {
            tblNodePermTableAdapter usr = new tblNodePermTableAdapter();
            return usr.GetNodePermByUserId(userid, companyCode);
        }

        public bool UpdateNodePermission(string userid, dsLinkoffice.tblNodePermDataTable dtprm, string companyCode)
        {
            tblNodePermTableAdapter per = new tblNodePermTableAdapter();
            bool flag = true;

            try
            {
               
                per.DeleteByUserId(userid, companyCode);

                foreach (dsLinkoffice.tblNodePermRow dr in dtprm.Rows)
                    per.InsertNode(companyCode, dr.UserId, dr.NodeId, dr.NodeName, dr.NodeFormName, dr.NodeUrl, dr.NodePerm, dr.NodeParam, dr.NodePermType);
   

            }
            catch (Exception ex)
            {
                flag = false;
            }

            return flag;
        }

                       

    }
}
