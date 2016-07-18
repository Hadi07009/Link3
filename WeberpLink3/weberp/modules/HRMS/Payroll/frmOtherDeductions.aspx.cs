using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Payroll_frmOtherDeductions : System.Web.UI.Page
{
    private string _connectionStringExcel = ConfigurationManager.ConnectionStrings["Excel07ConStringOverTime"].ConnectionString;
    readonly string _connectionString = ConfigurationManager.AppSettings["UbasysConnectionString"];
    private OtherAllowancesController _objOvertimeController;
    private OtherAllowances _objOvertime;
    private DocumentUploadController _objDocumentUploadController;
    private DocumentUpload _objDocumentUpload;
    private double _totalValue = 0;

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
            MessageBox1.ShowSuccess("Other Deduction Update Successful.");
        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }

    private void UploadTaxChallanData()
    {
        DateTime saldate = Convert.ToDateTime(CalenderSalmonth.Text);

        string mm = DateProcess.MonthName(saldate.Month - 1);
        string yyyy = saldate.Year.ToString();
        string oFilename = "OtherDeductions-" + mm + yyyy;

        _connectionStringExcel = _connectionStringExcel.Replace("OverTime", oFilename);

             
        var sheetName = "Sheet1$";
        CommonMethods objCommonMethods = new CommonMethods();
        DataTable dtFromExcel = objCommonMethods.GetDataFromExcel(_connectionStringExcel, sheetName);


        string sql = "delete from HrmsOthersPaymentwithsalary where month(oSalmonth)=month(Convert(Datetime,'" + saldate + "',103)) and year(oSalmonth)=year(Convert(Datetime,'" + saldate + "',103)) and [Type] in('OD')";
        DataProcess.ExecuteQuery(_connectionString, sql);

        
        foreach (DataRow dtRow in dtFromExcel.Rows)
        {
            if (Convert.ToDouble(dtRow["Deduction Amount"].ToString() == "" ? "0" : dtRow["Deduction Amount"].ToString()) > 0)
            {
                _objOvertimeController = new OtherAllowancesController();
                _objOvertime = new OtherAllowances();
                _objOvertime.EmployeeCode = dtRow["Employee No"].ToString();
                _objOvertime.ChallanDate = saldate;
                _objOvertime.ChallanForMonth = saldate;
                _objOvertime.ChallanNumber ="0";
                _objOvertime.ChallamAmount = dtRow["Deduction Amount"].ToString() == "" ? 0 : Convert.ToDecimal(dtRow["Deduction Amount"].ToString());
                _objOvertime.Paytype = "OD";
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
        _objOvertimeController = new OtherAllowancesController();
        _objOvertime = new OtherAllowances();
        _objOvertime.ChallanDate = Convert.ToDateTime(CalenderFromDate.Text);
        _objOvertime.ChallanDateSearch = CalenderToDate.Text == string.Empty ? null : Convert.ToDateTime(CalenderToDate.Text).ToString("dd-MM-yyyy");
        _objOvertime.Paytype = "OD";

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
            string Challmonth = Convert.ToDateTime(((Label)grdTaxChallan.Rows[selectedIndex].FindControl("lblChallanForMonth")).Text).ToString("dd-MM-yyyy");;

            _objOvertimeController = new OtherAllowancesController();
            _objOvertime = new OtherAllowances();
            _objOvertime.EmployeeCode = lblEmpID;
            _objOvertime.ChallanForMonth = Convert.ToDateTime(Challmonth);
            _objOvertime.Paytype = "OD";
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
    protected void grdTaxChallan_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var rowTotal = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "challamAmount"));
                _totalValue = _totalValue + rowTotal;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].Text = @"Total :";
                e.Row.Cells[5].Text = _totalValue.ToString("F");
            }
        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }
}