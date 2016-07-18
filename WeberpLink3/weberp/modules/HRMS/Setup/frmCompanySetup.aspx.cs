using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Setup_frmCompanySetup : System.Web.UI.Page
{
    private const string Rnode = "K";
    string ConnectionStr = ConfigurationSettings.AppSettings["SCFConnectionString"].Replace("SCF", current.CompanyCode);

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        if (!Page.IsPostBack)
        {
            LoadAllCompany();
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
       
    }
    private string SaveCompany()
    {
        CompanySetup objCompanySetup = new CompanySetup();
        objCompanySetup.CompanyName = txtCompanyName.Text;
        objCompanySetup.CompanyID = txtCompanyID.Text;
        objCompanySetup.Address1 = txtAddress1.Text == string.Empty ? null : txtAddress1.Text;
        objCompanySetup.Address2 = txtAddress2.Text == string.Empty ? null : txtAddress2.Text;
        objCompanySetup.Address3 = txtAddress3.Text == string.Empty ? null : txtAddress3.Text;
        objCompanySetup.ContactPersonAddress = txtContactPersonAddress.Text == string.Empty ? null : txtContactPersonAddress.Text;
        objCompanySetup.ContactPersonEmail = txtContactPersonEmail.Text == string.Empty ? null : txtContactPersonEmail.Text;
        objCompanySetup.PhoneNumber = txtPhoneNumber.Text == string.Empty ? null : txtPhoneNumber.Text;
        objCompanySetup.FAX = txtFax.Text == string.Empty ? null : txtFax.Text;
        objCompanySetup.Email = txtEmail.Text == string.Empty ? null : txtEmail.Text;
        objCompanySetup.URL = txtURL.Text == string.Empty ? null : txtURL.Text;
        objCompanySetup.TIN = txtTIN.Text == string.Empty ? null : txtTIN.Text;
        objCompanySetup.RegNo = txtRegNo.Text == string.Empty ? null : txtRegNo.Text;
        objCompanySetup.VATNo = txtVATNo.Text == string.Empty ? null : txtVATNo.Text;
        objCompanySetup.Insurance1 = txtInsurance1.Text == string.Empty ? null : txtInsurance1.Text;
        objCompanySetup.TxtTag = btnCompanySetup.Text;
        if (objCompanySetup.TxtTag == "Save")
        {
            return Save(ConnectionStr.ToString(), objCompanySetup);
        }
        else
        {
            return Update(ConnectionStr.ToString(), objCompanySetup);
        }
    }
    public string Save(string connectionString, CompanySetup objCompanySetup)
    {
        string _msg;
        var myConnection = new SqlConnection(connectionString);
        myConnection.Open();
        try
        {
            var myCommand = myConnection.CreateCommand();
            var dtCompany = new DataTable();
            dtCompany = DataProcess.GetData(connectionString, Sqlgenerate.SqlSearchCompanyRecord(objCompanySetup.CompanyName, objCompanySetup.CompanyID));
            if (dtCompany.Rows.Count == 0)
            {
                new SqlCommand("exec [CompanyInitiateIntoHrms_Company_Master] " +
                                 "'" + objCompanySetup.CompanyName + "'," +
                                 "'" + objCompanySetup.CompanyID + "'," +
                                 "'" + objCompanySetup.Address1 + "'," +
                                 "'" + objCompanySetup.Address2 + "'," +
                                 "'" + objCompanySetup.Address3 + "'," +
                                 "'" + objCompanySetup.Address4 + "'," +
                                 "'" + objCompanySetup.ContactPersonAddress + "'," +
                                 "'" + objCompanySetup.ContactPersonEmail + "'," +
                                 "'" + objCompanySetup.PhoneNumber + "'," +
                                 "'" + objCompanySetup.FAX + "'," +
                                 "'" + objCompanySetup.Email + "'," +
                                 "'" + objCompanySetup.URL + "'," +
                                 "'" + objCompanySetup.TIN + "'," +
                                 "'" + objCompanySetup.RegNo + "'," +
                                 "'" + objCompanySetup.VATNo + "'," +
                                 "'" + objCompanySetup.Insurance1 + "'," +
                                 "'" + objCompanySetup.TxtTag + "';", myConnection)
                                .ExecuteNonQuery();
                _msg = "Data Saved Successfully ";
                if (ViewState["companyLogo"] != null)
                {
                    _msg = SaveCompanyLogo(objCompanySetup.CompanyID);
                }
                ClearAllControl();
                LoadAllCompany();
            }
            else if (dtCompany.Rows.Count > 0)
            {
                _msg = "This Company ID or Name Already Exist !";
            }
            else
            {
                ClearAllControl();
                LoadAllCompany();
                _msg = " Please try again !";
            }

        }
        catch (SqlException sqlError)
        {
            _msg = "Error Occured During Operation into Database, Data did not Save into Database !";
        }
        catch (Exception inSystemExep)
        {
            _msg = " Error Occured, Data did not Save into Database !";
        }
        myConnection.Close();
        return _msg;
    }
    public string Update(string connectionString, CompanySetup objCompanySetup)
    {
        string _msg;
        var myConnection = new SqlConnection(connectionString);
        myConnection.Open();
        try
        {
            var myCommand = myConnection.CreateCommand();
            var dtCompany = new DataTable();
            dtCompany = DataProcess.GetData(connectionString, Sqlgenerate.SqlSearchCompanyRecord(objCompanySetup.CompanyName, objCompanySetup.CompanyID));
            if (dtCompany.Rows.Count == 1)
            {
                new SqlCommand("exec [CompanyInitiateIntoHrms_Company_Master] " +
                                 "'" + objCompanySetup.CompanyName + "'," +
                                 "'" + objCompanySetup.CompanyID + "'," +
                                 "'" + objCompanySetup.Address1 + "'," +
                                 "'" + objCompanySetup.Address2 + "'," +
                                 "'" + objCompanySetup.Address3 + "'," +
                                 "'" + objCompanySetup.Address4 + "'," +
                                 "'" + objCompanySetup.ContactPersonAddress + "'," +
                                 "'" + objCompanySetup.ContactPersonEmail + "'," +
                                 "'" + objCompanySetup.PhoneNumber + "'," +
                                 "'" + objCompanySetup.FAX + "'," +
                                 "'" + objCompanySetup.Email + "'," +
                                 "'" + objCompanySetup.URL + "'," +
                                 "'" + objCompanySetup.TIN + "'," +
                                 "'" + objCompanySetup.RegNo + "'," +
                                 "'" + objCompanySetup.VATNo + "'," +
                                 "'" + objCompanySetup.Insurance1 + "'," +
                                 "'" + objCompanySetup.TxtTag + "';", myConnection)
                                .ExecuteNonQuery();
                _msg = "Data Saved Successfully ";
                if (ViewState["companyLogo"] != null)
                {
                    _msg = SaveCompanyLogo(objCompanySetup.CompanyID);
                }
                ClearAllControl();
                LoadAllCompany();
            }
            else if (dtCompany.Rows.Count == 0)
            {
                btnCompanySetup.Text = "Save";
                _msg = "This Company did not found ! So, Please Save Now.";
            }
            else if (dtCompany.Rows.Count == 2)
            {
                _msg = "This Company Name Already Exist !";
            }
            else
            {
                ClearAllControl();
                LoadAllCompany();
                _msg = " Please try again !";
            }
        }
        catch (SqlException sqlError)
        {
            _msg = "Error Occured During Operation into Database, Data did not Save into Database !";
        }
        catch (Exception inSystemExep)
        {
            _msg = " Error Occured, Data did not Save into Database !";
        }
        myConnection.Close();
        return _msg;
    }

    private string CheckAllValidation()
    {
        const string checkValidation = "";
        if (txtCompanyID.Text == string.Empty)
        {
            txtCompanyID.Focus();
            return "Must Enter Company ID !";
        }
        if (txtCompanyID.Text.Length != 3)
        {
            txtCompanyID.Focus();
            return "Must Enter 3 Characters for Company ID !";
        }
        if (txtCompanyName.Text == string.Empty)
        {
            txtCompanyName.Focus();
            return "Must Enter Company Name !";
        }
        return checkValidation;
    }

    private void ClearAllControl()
    {
        txtCompanyID.Text = string.Empty;
        txtCompanyName.Text = string.Empty;
        txtAddress1.Text = string.Empty;
        txtAddress2.Text = string.Empty;
        txtAddress3.Text = string.Empty;
        txtContactPersonAddress.Text = string.Empty;
        txtContactPersonEmail.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtFax.Text = string.Empty;
        txtInsurance1.Text = string.Empty;
        txtPhoneNumber.Text = string.Empty;
        txtRegNo.Text = string.Empty;
        txtTIN.Text = string.Empty;
        txtURL.Text = string.Empty;
        txtVATNo.Text = string.Empty;
        txtCompanyID.Enabled = true;
        ViewState["companyLogo"] = null;
        lblImage.Text = "<br /> Logo <br />  Not <br />  Available ";
        btnCompanySetup.Text = "Save";
    }

    private void LoadAllCompany()
    {
        var dtJobType = new DataTable();
        var myConnection = new SqlConnection(ConnectionStr.ToString());
        var myCommand = myConnection.CreateCommand();
        myConnection.Open();
        myCommand.CommandText = "exec [CompanyGetAllFromhrms_company_master] ";
        myCommand.ExecuteNonQuery();
        var daJobType = new SqlDataAdapter(myCommand);
        daJobType.Fill(dtJobType);
        grdShowAllCompany.DataSource = null;
        grdShowAllCompany.DataBind();
        if (dtJobType.Rows.Count > 0)
        {
            grdShowAllCompany.DataSource = dtJobType;
            grdShowAllCompany.DataBind();
        }
        myConnection.Close();
    }
    
    protected void grdShowAllCompany_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        string lblCompanyID = ((Label)grdShowAllCompany.Rows[selectedIndex].FindControl("lblCompanyID")).Text;
        if (e.CommandName.Equals("Delete"))
        {
            try
            {
                DataProcess.DeleteQuery(ConnectionStr.ToString(), Sqlgenerate.SqlDeleteCompanyRecord(lblCompanyID));
                LoadAllCompany();
            }
            catch (SqlException sqlError)
            {
                ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "MessageBox",
                        "alert(' Error Occured During Operation into Database, Data did not Delete from Database ! ');",
                        true);
            }
            catch (Exception inSystemExep)
            {
                ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "MessageBox",
                        "alert(' Error Occured, Data did not Delete from Database  ! ');",
                        true);
            }
        }
        else if (e.CommandName.Equals("Select"))
        {
            string lblCompanyName = ((Label)grdShowAllCompany.Rows[selectedIndex].FindControl("lblCompanyName")).Text;
            string lblAddress1 = ((Label)grdShowAllCompany.Rows[selectedIndex].FindControl("lblAddress1")).Text;
            string lblAddress2 = ((Label)grdShowAllCompany.Rows[selectedIndex].FindControl("lblAddress2")).Text;
            string lblAddress3 = ((Label)grdShowAllCompany.Rows[selectedIndex].FindControl("lblAddress3")).Text;
            string lblContactPersonAddress = ((Label)grdShowAllCompany.Rows[selectedIndex].FindControl("lblContactPersonAddress")).Text;
            string lblContactPersonEmail = ((Label)grdShowAllCompany.Rows[selectedIndex].FindControl("lblContactPersonEmail")).Text;
            string lblPhoneNumber = ((Label)grdShowAllCompany.Rows[selectedIndex].FindControl("lblPhoneNumber")).Text;
            string lblFax = ((Label)grdShowAllCompany.Rows[selectedIndex].FindControl("lblFax")).Text;
            string lblEmail = ((Label)grdShowAllCompany.Rows[selectedIndex].FindControl("lblEmail")).Text;
            string lblURL = ((Label)grdShowAllCompany.Rows[selectedIndex].FindControl("lblURL")).Text;
            string lblTIN = ((Label)grdShowAllCompany.Rows[selectedIndex].FindControl("lblTIN")).Text;
            string lblRegNo = ((Label)grdShowAllCompany.Rows[selectedIndex].FindControl("lblRegNo")).Text;
            string lblVATNo = ((Label)grdShowAllCompany.Rows[selectedIndex].FindControl("lblVATNo")).Text;
            string lblInsurance = ((Label)grdShowAllCompany.Rows[selectedIndex].FindControl("lblInsurance")).Text;
            ViewState["companyLogo"] = null;
            lblImage.Text = "<br /> Logo <br />  Not <br />  Available ";
            try
            {

                var dtPhoto = DataProcess.GetData(ConnectionStr.ToString(), Sqlgenerate.SqlGetCompanyLogo(lblCompanyID));
                if (dtPhoto.Rows.Count > 0 && !Convert.IsDBNull(dtPhoto.Rows[0].ItemArray[0]))
                {
                    var img = (byte[])dtPhoto.Rows[0].ItemArray[0];
                    string base64string = Convert.ToBase64String(img, 0, img.Length);
                    lblImage.Text = "<img src='data:image/png;base64," + base64string + "' alt='<br /> Logo <br />  Not <br />  Available ' width='150px' hight='150px' vspace='5px' hspace='5px' />";
                    ViewState["companyLogo"] = img;
                }

            }
            catch (Exception inSystemExep)
            {

                ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "MessageBox",
                        "alert(' Error Occured During Load Company Logo ! ');",
                        true);
            }
            txtCompanyID.Text = lblCompanyID;
            txtCompanyName.Text = lblCompanyName;
            txtAddress1.Text = lblAddress1;
            txtAddress2.Text = lblAddress2;
            txtAddress3.Text = lblAddress3;
            txtContactPersonAddress.Text = lblContactPersonAddress;
            txtContactPersonEmail.Text = lblContactPersonEmail;
            txtPhoneNumber.Text = lblPhoneNumber;
            txtFax.Text = lblFax;
            txtEmail.Text = lblEmail;
            txtURL.Text = lblURL;
            txtTIN.Text = lblTIN;
            txtRegNo.Text = lblRegNo;
            txtVATNo.Text = lblVATNo;
            txtInsurance1.Text = lblInsurance;
            btnCompanySetup.Text = "Update";
            txtCompanyID.Enabled = false;
        }
    }
    protected void grdShowAllCompany_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void btnImageUpload_Click(object sender, EventArgs e)
    {
        if (LogoUpload.HasFile)
        {
            if (LogoUpload.PostedFile.ContentType == "image/jpg" ||
                LogoUpload.PostedFile.ContentType == "image/jpeg" ||
                LogoUpload.PostedFile.ContentType == "image/gif" ||
                LogoUpload.PostedFile.ContentType == "image/bmp" ||
                LogoUpload.PostedFile.ContentType == "image/png")
            {
                int filelenght = LogoUpload.PostedFile.ContentLength;
                if (filelenght <= 524288)  
                {
                    byte[] imagebytes = new byte[filelenght];
                    LogoUpload.PostedFile.InputStream.Read(imagebytes, 0, filelenght);
                    byte[] img = imagebytes;
                    string base64string = Convert.ToBase64String(img, 0, img.Length);
                    System.Drawing.Image im = System.Drawing.Image.FromStream(LogoUpload.PostedFile.InputStream);
                    double imageHight = im.PhysicalDimension.Height;
                    double imageWidth = im.PhysicalDimension.Width;
                    if (imageHight <= 150 && imageWidth <= 150)
                    {
                        lblImage.Text = "<img src='data:image/png;base64," + base64string +
                                        "' alt='<br /> <br /> Logo <br />  Not <br />  Available ' width='" + imageWidth + "' hight='" + imageHight + "' vspace='5px' hspace='5px' />";
                        ViewState["companyLogo"] = imagebytes;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(
                            this,
                            GetType(),
                            "MessageBox",
                            "alert(' Logo size should not be greater than 150X150 !');",
                            true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "MessageBox",
                        "alert(' Logo exceeds maximum size !');",
                        true);
                }
            }
        }
    }
    #region Logo
    private string SaveCompanyLogo(string companyID)
    {
        string _msg;
        try
        {
            SqlConnection con = null;
            con = new SqlConnection(ConnectionStr.ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE Hrms_Company_Master SET CompanyLogo =	ISNULL(@CompanyLogo,CompanyLogo) WHERE	CompanyId=@CompanyId";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@CompanyLogo", ViewState["companyLogo"]);
            cmd.Parameters.AddWithValue("@CompanyId", companyID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            _msg = "Data Saved Successfully ";
        }
        catch (Exception errotExceptionMsg)
        {

            _msg = "Only Logo did not Save into Database !";
        }
        return _msg;
    }

    #endregion Phote
    protected void grdShowAllCompany_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[16].Visible = false;
        e.Row.Cells[17].Visible = false;
    }


    protected void btnCompanySetup_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckAllValidation();
        switch (validationMsg)
        {
            case "":
                {
                    string msg = SaveCompany();
                    MessageBox1.ShowSuccess(msg);
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }
    }
}