using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Payroll_frmTaxChallan : System.Web.UI.Page
{
    private string _connectionStringExcel = ConfigurationManager.ConnectionStrings["Excel07ConStringTaxChallan"].ConnectionString;
    readonly string _connectionString = ConfigurationManager.AppSettings["UbasysConnectionString"];
    private TaxChallanController _objTaxChallanController;
    private TaxChallan _objTaxChallan;
    private DocumentUploadController _objDocumentUploadController;
    private DocumentUpload _objDocumentUpload;

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);
        CalenderFromDate.Attributes.Add("readonly", "readonly");
        CalenderToDate.Attributes.Add("readonly", "readonly");

    }
    protected void btnUploadTaxChallan_Click(object sender, EventArgs e)
    {
        try
        {
            UploadTaxChallanData();
        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }

    private void UploadTaxChallanData()
    {
        var sheetName = "Sheet1$";
        CommonMethods objCommonMethods = new CommonMethods();
        DataTable dtFromExcel = objCommonMethods.GetDataFromExcel(_connectionStringExcel, sheetName);
        foreach (DataRow dtRow in dtFromExcel.Rows)
        {
            _objTaxChallanController = new TaxChallanController();
            _objTaxChallan = new TaxChallan();
            _objTaxChallan.EmployeeCode = dtRow[0].ToString();
            _objTaxChallan.ChallanDate = dtRow[1].ToString();
            _objTaxChallan.ChallanForMonth = dtRow[2].ToString();
            _objTaxChallan.ChallanNumber = dtRow[3].ToString();
            _objTaxChallan.ChallamAmount = dtRow[4].ToString() == "" ? 0 : Convert.ToDecimal( dtRow[4].ToString());
            _objTaxChallanController.Save(_connectionString, _objTaxChallan);
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            GetTaxChallanData();
        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }

    }

    private void GetTaxChallanData()
    {
        _objTaxChallanController = new TaxChallanController();
        _objTaxChallan = new TaxChallan();
        _objTaxChallan.ChallanDate = CalenderFromDate.Text == string.Empty ? null : Convert.ToDateTime(CalenderFromDate.Text).ToString("dd-MM-yyyy");
        _objTaxChallan.ChallanDateSearch = CalenderToDate.Text == string.Empty ? null : Convert.ToDateTime(CalenderToDate.Text).ToString("dd-MM-yyyy"); 
        var dtChallanRecord = _objTaxChallanController.GetDate(_connectionString, _objTaxChallan);
        grdTaxChallan.DataSource = null;
        grdTaxChallan.DataBind();
        if (dtChallanRecord.Rows.Count > 0)
        {
            grdTaxChallan.DataSource = dtChallanRecord;
            grdTaxChallan.DataBind();
        }
    }
    protected void grdTaxChallan_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        if (e.CommandName.Equals("Delete"))
        {
            string lblEmpID = ((Label)grdTaxChallan.Rows[selectedIndex].FindControl("lblEmpID")).Text;
            string lblChallanNumber = ((Label)grdTaxChallan.Rows[selectedIndex].FindControl("lblChallanNumber")).Text;
            _objTaxChallanController = new TaxChallanController();
            _objTaxChallan = new TaxChallan();
            _objTaxChallan.EmployeeCode = lblEmpID;
            _objTaxChallan.ChallanNumber = lblChallanNumber;
            _objTaxChallanController.Delete(_connectionString, _objTaxChallan);
            GetTaxChallanData();
        }
    }
    protected void grdTaxChallan_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btnUploadAttachment_Click(object sender, EventArgs e)
    {
        AttachFileSave();

    }
    private void AttachFileSave()
    {
        try
        {
            string filepath = System.Configuration.ConfigurationSettings.AppSettings["upl"].ToString();


            HttpFileCollection hfc = Request.Files;

            _objDocumentUploadController = new DocumentUploadController();

            string referenceno = _objDocumentUploadController.GetReferenceNo(_connectionString);
            for (int i = 0; i < hfc.Count; i++)
            {
                HttpPostedFile hpf = hfc[i];

                if (hpf.ContentLength > 0)
                {
                    string fileName = System.IO.Path.GetFileName(hpf.FileName).Replace("'", "''");
                    //string filenameReference = referenceno + "-" + fileName;
                    string filenameReference =fileName;
                    //hpf.SaveAs(Server.MapPath("~/AttachMentfile/") + "\\" + filenameReference);
                    hpf.SaveAs(filepath + "\\" + filenameReference);
                    //Upload(fileName, referenceno, filenameReference);
                }

            }

            MessageBox1.ShowSuccess("Attachment Upload Successful");

        }
        catch (Exception ex)
        {
            MessageBox1.ShowWarning(ex.Message);
        }
    }

    private void Upload(string fileName, string referenceNo, string filenemeReference)
    {
        try
        {
            _objDocumentUploadController = new DocumentUploadController();
            _objDocumentUpload = new DocumentUpload();
            _objDocumentUpload.DocumentTypeCode ="-1";
            _objDocumentUpload.Description = "Tax Challan";
            _objDocumentUpload.DocumentContent = null;
            _objDocumentUpload.EntryUser = current.UserId;
            _objDocumentUpload.DocumentContent = fileName;
            _objDocumentUpload.documentCode = referenceNo;
            _objDocumentUpload.documentName = filenemeReference;
            _objDocumentUploadController.DocumentUpload(_connectionString, _objDocumentUpload);
        }
        catch (Exception msgException)
        {
            MessageBox1.ShowError(msgException.Message);
        }
    }
}