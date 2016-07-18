using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Xml;
using System.Web.UI.WebControls;
using LibraryDAL;
using LibraryDAL.dsLinkofficeTableAdapters;
using System.Data;
using System.Data.SqlClient;



public partial class frmUserInformation : System.Web.UI.Page
{
    string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        //clsStatic.CheckUserAuthentication(true);
        if(!IsPostBack)
        {
           
        }
        
    }
      
    protected void txtUserlist_TextChanged(object sender, EventArgs e)
    {
        textChange();
    }

    private void textChange()
    {
        
        string[] strarr = txtUserlist.Text.Split(':');
        clsSystem usr = new clsSystem();
        dsLinkoffice.tblUserInfoDataTable dtuser = new dsLinkoffice.tblUserInfoDataTable();
       
        if (strarr.Length >= 2)
        {
            dtuser = usr.GetUserById(strarr[0],current.CompanyCode);
            if (dtuser.Count > 0)
            {
                txtuserid.Text = dtuser[0].UserId;
                txtName.Text = dtuser[0].UserName;
                txtshortname.Text = dtuser[0].UserDiaplayName;
                txtEmpcode.Text = dtuser[0].UserEmpId;
                txtDepartment.Text = dtuser[0].UserDepartment;
                txtDesignation.Text = dtuser[0].UserDesignation;
                txtEmail.Text = dtuser[0].UserEmail;
                txtMobile.Text = dtuser[0].UserMob;
                rdoStatus.SelectedIndex = dtuser[0].UserStatus;
                imgEmployee.ImageUrl = "~/handler/hndImage.ashx?id=" + txtuserid.Text;

                ShowImage(txtEmpcode.Text, current.CompanyCode);

            }
        }
    }

    private void ShowImage(string employeeCode,string Comcode)
    {
        var myConnection = new SqlConnection(ConnectionString);
        var myCommand = myConnection.CreateCommand();
        myConnection.Open();

        var dtPhoto = new DataTable();
        myCommand.CommandText = "exec [EmpBasicInformationGetFromHrms_Emp_Photo] '" + employeeCode + "','" + Comcode + "'";
        myCommand.ExecuteNonQuery();
        var daPhoto = new SqlDataAdapter(myCommand);
        daPhoto.Fill(dtPhoto);
        if (dtPhoto.Rows.Count > 0)
        {
            var img = (byte[])dtPhoto.Rows[0].ItemArray[0];
            string base64string = Convert.ToBase64String(img, 0, img.Length);
            lblImage.Text = "<img src='data:image/png;base64," + base64string + "' alt='<br />  Photo <br />  Not <br />  Available ' width='150px' hight='150px' vspace='5px' hspace='5px' />";
            ViewState["profileImage"] = img;
        }
    }



    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtuserid.Text.Length < 3) return;
        if (txtName.Text.Length < 3) return;
        clsSystem usr = new clsSystem();
        string userid=txtuserid.Text.Trim();
        string empid = txtEmpcode.Text.Trim();

        
        if (usr.GetUserById(empid, current.CompanyCode).Count == 0)
        {
            //add
            DataTable dtu = DataProcess.GetData(ConnectionString, Sqlgenerate.SqlGetUserinfoByuserid(userid, current.CompanyCode));

            if (dtu.Rows.Count > 0)
            {
                string msg = "his User id has allready assigned to employee id:" + dtu.Rows[0]["UserEmpId"].ToString();
                MessageBox1.ShowWarning(msg);
                return; 
            }            
            if(usr.InsertUser(txtuserid.Text, txtEmpcode.Text, txtName.Text, txtDesignation.Text, txtDepartment.Text,
                           txtEmail.Text, txtMobile.Text, null, txtshortname.Text, rdoStatus.SelectedIndex,current.CompanyCode)>0)
            {
                updpnl.Update();
                MessageBox1.ShowSuccess("User Inormation Saveed Successfull ");
            }
        }
        else
        {
            //edit

            DataTable dtu = DataProcess.GetData(ConnectionString, Sqlgenerate.SqlGetUserinfoByuseridEmpid(userid, current.CompanyCode,empid));

            if (dtu.Rows.Count > 0)
            {
                string msg = "This User id has allready assigned to employee id:"+dtu.Rows[0]["UserEmpId"].ToString();
                MessageBox1.ShowWarning(msg);
                return; 
            }               

            if (DataProcess.ExecuteQuery(ConnectionString, Sqlgenerate.UpdateUserInformationByempidCompanyCode(txtuserid.Text, txtEmpcode.Text, txtName.Text, txtDesignation.Text, txtDepartment.Text,
                           txtEmail.Text, txtMobile.Text, txtshortname.Text, rdoStatus.SelectedIndex, current.CompanyCode)))
            {
                updpnl.Update();
                MessageBox1.ShowSuccess("User Inormation Updateed Successfull "); 
            }


           

            //if(usr.UpdateUserInformation(txtuserid.Text, txtEmpcode.Text, txtName.Text, txtDesignation.Text, txtDepartment.Text,
            //               txtEmail.Text, txtMobile.Text, txtshortname.Text, rdoStatus.SelectedIndex, current.CompanyCode) > 0)
            //{
            //    updpnl.Update();
            //    MessageBox1.ShowSuccess("User Inormation Updateed Successfull ");
            //}

        }

        //if (usr.UpdateUserPasswd(txtuserid.Text, "",current.CompanyCode) > 0)
        //{
        //    updpnl.Update();
        //    clsStatic.MsgConfirmBox("Password Removed");
        //}

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        clear_all();
    }
    private void clear_all()
    {
        txtuserid.Text = "";
        txtName.Text = "";
        txtshortname.Text = "";
        txtEmpcode.Text = "";
        txtDepartment.Text = "";
        txtDesignation.Text = "";
        txtEmail.Text = "";
        txtMobile.Text = "";
        txtNewpass.Text = "";
        txtConpass.Text = "";
    }
    protected void btnPass_Click(object sender, EventArgs e)
    {
        clsSystem usr = new clsSystem();
        //if (usr.UpdateUserPasswd(txtuserid.Text, "", current.CompanyCode) > 0)
        //{
        //    updpnl.Update();
        //    clsStatic.MsgConfirmBox("Password Removed");
        //}
               

        if (txtNewpass.Text == txtConpass.Text)
        {
            //if (usr.GetUserByIdPass(txtuserid.Text, txtNewpass.Text, current.CompanyCode).Count == 0)
            //{
            //    MessageBox1.ShowSuccess("User id or password is not correct");
            //    return;
            //}

            if (usr.UpdateUserPasswd(txtuserid.Text, txtNewpass.Text, current.CompanyCode) > 0)
            {
                MessageBox1.ShowSuccess("Password reset successful");
                clear_all();
            }
            else
            {
                MessageBox1.ShowSuccess("Password is not correct"); 
            }

        }          

    }
    protected void btngetimage_Click(object sender, EventArgs e)
    {
        clsSystem usr = new clsSystem();
        int len = upd_image.PostedFile.ContentLength;
        byte[] pic = new byte[len];
        upd_image.PostedFile.InputStream.Read(pic, 0, len);
        if (usr.UpdateUserImage(txtuserid.Text, pic, current.CompanyCode) > 0)
        {
            updpnl.Update();
            clsStatic.MsgConfirmBox("Image Updated");
        }
    }
    
    protected void btnImportUser_Click1(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();

            string sql = @"select EmpID as Userid,EmpName as UserName,EmpName as DisplayName,'' as CompanyCode,'' as Passkey,EmpID as userempid,
                        Designation as Userdesignation,Dept,isnull(Emp_Mas_Remarks,'') as Useremail,left(Emp_Mas_HandSet,20) as Emp_Mas_HandSet,'' as  Img,GETDATE(),0 as UserStatus,isnull(UserEmpId,0) as uemp  
                        from Emp_Details a 
                        inner join hrms_emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id 
                        left outer join tblUserInfo c on c.UserEmpId=a.empid
                        where Emp_Mas_Term_Flg='N'";

            dt = DataProcess.GetData(ConnectionString, sql);

            string userid = "";
            string UserName = "";
            string DisplayName = "";
            string CompanyCode = "";
            string Passkey = "";
            string userempid = "";
            string Userdesignation = "";
            string Dept = "";
            string Useremail = "";
            string Emp_Mas_HandSet = "";
            string Img = "";
            DateTime EntryDate =System.DateTime.Now;
            string UserStatus = "";
            string uemp = "";
            int iii = 0;
            int jjj = 0;
            foreach (DataRow dr in dt.Rows)
            {
                userid = dr["Userid"].ToString();
                UserName = dr["UserName"].ToString();
                DisplayName = dr["DisplayName"].ToString();
                CompanyCode = current.CompanyCode;
                Passkey = dr["Passkey"].ToString();
                userempid = dr["userempid"].ToString();
                Userdesignation = dr["Userdesignation"].ToString();
                Dept = dr["Dept"].ToString();
                Useremail = dr["Useremail"].ToString();
                Emp_Mas_HandSet = dr["Emp_Mas_HandSet"].ToString();
                Img = dr["Img"].ToString();
                EntryDate = Convert.ToDateTime(System.DateTime.Now);
                UserStatus = dr["UserStatus"].ToString();
                uemp = dr["uemp"].ToString();
                if (uemp == "0")
                {
                    sql = @"insert into tblUserInfo([UserId],[UserName],UserDiaplayName,CompanyCode,UserPassword,UserEmpId,UserDesignation,
                        UserDepartment,UserEmail,UserMob,UserImage,UserCreateDate,[UserStatus],[UserHtml])
                        values('" + userid + "','" + UserName + "','" + DisplayName + "','" + CompanyCode + "','" + Passkey + "','" + userempid + "','" + Userdesignation + "','" + Dept + "', '" + Useremail + "', '" + Emp_Mas_HandSet + "', '" + Img + "', Convert(Datetime,'" + EntryDate + "',103), '" + UserStatus + "','')";
                    iii++;

                }
                else
                {
                    sql = @"update tblUserInfo set UserName='" + UserName + "',UserDiaplayName='" + DisplayName + "',UserDesignation='" + Userdesignation + "',UserDepartment='" + Dept + "',UserEmail='" + Useremail + "',UserMob='" + Emp_Mas_HandSet + "',[UserHtml]='' where UserId='" + userid + "'";
                    jjj++;
                }
                
                DataProcess.ExecuteQuery(ConnectionString, sql);               

            }

            string msg = "Data Saved Successfully";
            MessageBox1.ShowSuccess(msg);

        }
        catch (Exception ex)
        {
            string msg = "Data import error. Please try again";
            MessageBox1.ShowSuccess(msg);

        }


    }
}
