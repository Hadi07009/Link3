using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Data.SqlClient;
using System.Data;

public partial class UserControls_ucEmployeeInformation : System.Web.UI.UserControl
{
    readonly string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"];
    LoginUserInformation _objLoginUser;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadEmployeeInformation(current.UserId);

            if(GetUserPicture(current.UserAdID, current.UserAdPass)==false)
                ShowPrfileImage(current.UserId);          
        }
    }

    private void UploadEmpImage()
    {
       
    }

    private string SaveProfileImage(byte[] image)
    {
        string _msg;

        try
        {
            string employeeId = current.UserId;
            string companyid = current.CompanyCode;
            string sql = "";
            sql = "delete from Hrms_Emp_Photo where EmpID='" + employeeId + "'";
            DataProcess.ExecuteQuery(_connectionString, sql);

            //sql = "select EmpPhoto from Hrms_Emp_Photo where EmpID='300021'";
            //DataTable dt = new DataTable();
            //dt = DataProcess.GetData(_connectionString, sql);
            //image = dt.Rows[0]["EmpPhoto"] as byte[];

            SqlConnection con = null;
            con = new SqlConnection(_connectionString);
            SqlCommand cmdForCheck = new SqlCommand("Select TOP 1 *  from HrMs_Emp_mas where Emp_Mas_Emp_Id = '" + employeeId + "'", con);
            con.Open();
            SqlDataReader dr = null;
            dr = cmdForCheck.ExecuteReader();
            if (dr.HasRows)
            {
                con.Close();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Insert into Hrms_Emp_Photo(EmpPhoto,CompanyCode,EmpId) values(@img,@CompanyCode,@EmpId)";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@img", image);
                cmd.Parameters.AddWithValue("@CompanyCode", companyid);
                cmd.Parameters.AddWithValue("@EmpId", employeeId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            con.Close();
            _msg = "Data Saved Successfully ";
        }
        catch (Exception errotExceptionMsg)
        {
            _msg = "Only Photo did not Save into Database !";
        }
        return _msg;
    }


    private void LoadEmployeeInformation(string userId)
    {
        try
        {
            _objLoginUser = new LoginUserInformation();
            var dtBasicInformatin = _objLoginUser.GetBasicInformation(_connectionString, userId);

            if (dtBasicInformatin.Rows.Count > 0)
            {
                lblId.Text = dtBasicInformatin.Rows[0]["EmpID"].ToString();
                lblName.Text = dtBasicInformatin.Rows[0]["EmpName"].ToString();
                lbldept.Text = dtBasicInformatin.Rows[0]["Dept"].ToString();
                lblDesignation.Text = dtBasicInformatin.Rows[0]["Designation"].ToString();
                lblJoiningDate.Text = dtBasicInformatin.Rows[0]["Joiningdate"].ToString().Substring(0, 10);
                lblDateOfBirth.Text = dtBasicInformatin.Rows[0]["Emp_Mas_DOB"].ToString() == "" ? "" : dtBasicInformatin.Rows[0]["Emp_Mas_DOB"].ToString().Substring(0, 10);
                lblConfirmDate.Text = dtBasicInformatin.Rows[0]["Emp_Mas_Confrim_Date"].ToString() == "" ? "" : dtBasicInformatin.Rows[0]["Emp_Mas_Confrim_Date"].ToString().Substring(0, 10);

            }
            else
            {
                string sql = "select * from tblUserInfo where UserId='" + current.UserAdID + "'";
                DataTable dt = new DataTable();
                dt = DataProcess.GetData(_connectionString, sql);
                if (dt.Rows.Count > 0)
                {
                    lblId.Text = dt.Rows[0]["UserEmpId"].ToString();
                    lblName.Text = dt.Rows[0]["UserName"].ToString();
                    lbldept.Text = dt.Rows[0]["UserDepartment"].ToString();
                    lblDesignation.Text = dt.Rows[0]["UserDesignation"].ToString();
 
                } 
            }

        }
        catch (Exception msgException)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "MessageBox", "alert(' " + msgException.Message + " ');", true);
        }
    }


    private bool GetUserPicture(string userName, string password)
    {

        try
        {
            string dname = System.Configuration.ConfigurationSettings.AppSettings["dname"].ToString();
            string dip = System.Configuration.ConfigurationSettings.AppSettings["dip"].ToString();
            
            DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://" + dip, userName + "@" + dname, password);
            
            //var directoryEntry = new DirectoryEntry("LDAP://YourDomain");

            var directorySearcher = new DirectorySearcher(directoryEntry);
            directorySearcher.Filter = string.Format("(&(SAMAccountName={0}))", userName);
            var user = directorySearcher.FindOne();

            var bytes = user.Properties["thumbnailPhoto"][0] as byte[];

            SaveProfileImage(bytes);

            var base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            //lblImage.Text = @"<img src='data:image/png;base64," + base64String + @"' alt='<br />  Photo <br />  Not <br />  Available ' width='70px' hight='70px' vspace='5px' hspace='5px' />";

            byte[] imageBytes = Convert.FromBase64String(base64String);
            Image1.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(imageBytes); 


            return true;

        }

        catch (Exception ex)
        {
            Response.Write(ex.InnerException);
            return false;
        }
    }
      

    private void ShowPrfileImage(string userId)
    {

        try
        {
            _objLoginUser = new LoginUserInformation();
            var dtPhoto = _objLoginUser.GetProfileImage(_connectionString, userId);
            //lblImage.Text = @"<br />  Photo <br />  Not <br />  Available ";
            if (dtPhoto.Rows.Count > 0)
            {
                var img = (byte[])dtPhoto.Rows[0].ItemArray[0];
                var base64String = Convert.ToBase64String(img, 0, img.Length);
                //lblImage.Text = @"<img src='data:image/png;base64," + base64String + @"' alt='<br />  Photo <br />  Not <br />  Available ' width='70px' hight='70px' vspace='5px' hspace='5px' />";
                                
                byte[] imageBytes = Convert.FromBase64String(base64String);
                Image1.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(imageBytes); 




            }

        }
        catch (Exception msgException)
        {
            //ScriptManager.RegisterStartupScript(this, GetType(), "MessageBox", "alert(' " + msgException.Message + " ');", true);
        }
    }
}