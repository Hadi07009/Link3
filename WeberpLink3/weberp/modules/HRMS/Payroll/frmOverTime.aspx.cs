using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Payroll_frmOverTime : System.Web.UI.Page
{
    private string _connectionStringExcel = ConfigurationManager.ConnectionStrings["Excel07ConStringOverTime"].ConnectionString;
    readonly string _connectionString = ConfigurationManager.AppSettings["UbasysConnectionString"];
    private OvertimeController _objOvertimeController;
    private Overtime _objOvertime;
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
            MessageBox1.ShowSuccess("Over Time Update Successful.");
        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }

    private void UploadTaxChallanData()
    {
        _connectionStringExcel = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\CEBD-HR-Payroll-Documents\\Conveyance.xls;Extended Properties='Excel 8.0;HDR={1}'";

        var sheetName = "Sheet1$";
        CommonMethods objCommonMethods = new CommonMethods();
        DataTable dtFromExcel = objCommonMethods.GetDataFromExcel(_connectionStringExcel, sheetName);



        foreach (DataRow dtRow in dtFromExcel.Rows)
        {
            if (Convert.ToDouble(dtRow["OT Amount"].ToString()) > 0)
            {
                _objOvertimeController = new OvertimeController();
                _objOvertime = new Overtime();
                _objOvertime.EmployeeCode = dtRow["Employee No"].ToString();
                _objOvertime.ChallanDate = dtRow[1].ToString();
                _objOvertime.ChallanForMonth = dtRow[2].ToString();
                _objOvertime.ChallanNumber = dtRow[3].ToString();
                _objOvertime.ChallamAmount = dtRow[4].ToString() == "" ? 0 : Convert.ToDecimal(dtRow[4].ToString());
                _objOvertime.Paytype = "OT";
                _objOvertimeController.Save(_connectionString, _objOvertime);
            }
            if (Convert.ToDouble(dtRow["Food"].ToString()) > 0)
            {
                _objOvertimeController = new OvertimeController();
                _objOvertime = new Overtime();
                _objOvertime.EmployeeCode = dtRow[0].ToString();
                _objOvertime.ChallanDate = dtRow[1].ToString();
                _objOvertime.ChallanForMonth = dtRow[2].ToString();
                _objOvertime.ChallanNumber = dtRow[3].ToString();
                _objOvertime.ChallamAmount = dtRow[4].ToString() == "" ? 0 : Convert.ToDecimal(dtRow[4].ToString());
                _objOvertime.Paytype = "FA";
                _objOvertimeController.Save(_connectionString, _objOvertime);
            }
            
           
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
        _objOvertimeController = new OvertimeController();
        _objOvertime = new Overtime();
        _objOvertime.ChallanDate = CalenderFromDate.Text == string.Empty ? null : Convert.ToDateTime(CalenderFromDate.Text).ToString("dd-MM-yyyy");
        _objOvertime.ChallanDateSearch = CalenderToDate.Text == string.Empty ? null : Convert.ToDateTime(CalenderToDate.Text).ToString("dd-MM-yyyy");
        _objOvertime.Paytype = "OT";

        var dtChallanRecord = _objOvertimeController.GetDate(_connectionString, _objOvertime);
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
            string Challmonth = ((Label)grdTaxChallan.Rows[selectedIndex].FindControl("lblChallanForMonth")).Text;

            _objOvertimeController = new OvertimeController();
            _objOvertime = new Overtime();
            _objOvertime.EmployeeCode = lblEmpID;
            _objOvertime.ChallanForMonth = Challmonth;
            _objOvertime.Paytype = "OT";
            _objOvertimeController.Delete(_connectionString, _objOvertime);
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
                    Upload(fileName, referenceno, filenameReference);
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