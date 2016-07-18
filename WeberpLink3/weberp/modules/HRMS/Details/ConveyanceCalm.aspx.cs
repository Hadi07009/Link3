using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using AjaxControlToolkit;
using System.Data.SqlClient;


public partial class modules_HRMS_Details_ConveyanceCalm : System.Web.UI.Page
{
    readonly string _connectionString = ConfigurationManager.AppSettings["UbasysConnectionString"];
    private string _connectionStringExcel = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        if (Page.IsPostBack)
        {
            Session["SelectedSheetNames"] = ddlSheetName.SelectedValue;
        }
    }

    private void LoadSheetName(DataTable sheetDataTable)
    {
        ddlSheetName.Items.Clear();
        if (sheetDataTable != null)
        {
            ddlSheetName.Items.Insert(0, new ListItem("--- Please Select ---", "-1"));
            foreach (DataRow dr in sheetDataTable.Rows)
            {
                var lst = new ListItem { Value = dr["TABLE_NAME"].ToString(), Text = dr["TABLE_NAME"].ToString() };
                ddlSheetName.Items.Add(lst);
            }
        }
        else
        {
            ddlSheetName.Items.Insert(0, new ListItem("--- No Data Found ---", "-1"));
        }
    }
    private void Import_To_Grid(string filePath, string isHdr)
    {
        _connectionStringExcel = String.Format(_connectionStringExcel, filePath, isHdr);
        var connExcel = new OleDbConnection(_connectionStringExcel);
        connExcel.Open();
        var sheetTable = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        Session["SheetNames"] = sheetTable;
        connExcel.Close();
        LoadSheetName(sheetTable);
    }

    private void Import_To_Grid(string filePath, string isHdr, string sheetName)
    {
        _connectionStringExcel = String.Format(_connectionStringExcel, filePath, isHdr);
        var connExcel = new OleDbConnection(_connectionStringExcel);
        var cmdExcel = new OleDbCommand();
        var oda = new OleDbDataAdapter();
        var dt = new DataTable();
        cmdExcel.Connection = connExcel;
        connExcel.Open();
        cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();
        grdGetConveyance.Caption = Path.GetFileName(filePath);
        grdGetConveyance.DataSource = null;
        grdGetConveyance.DataBind();
        Session["excelData"] = null;
        if (dt.Rows.Count > 0)
        {
            grdGetConveyance.DataSource = dt;
            grdGetConveyance.DataBind();
            Session["excelData"] = dt;
        }
    }
    protected void ddlSheetName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSheetName.SelectedValue != "-1")
        {
            Import_To_Grid(Session["FilePath"].ToString(), "Yes", Session["SelectedSheetNames"].ToString());
        }
        else
        {
            grdGetConveyance.DataSource = null;
            grdGetConveyance.DataBind();
            Session["excelData"] = null;
        }
    }

    private void SaveConveyanceCalm()
    {
        var objConveyanceCalm = new ConveyanceCalm
        {
            PaymentPeriodFromDate =
                popupPaymentPeriodFrom.Text == string.Empty
                    ? null
                    : Convert.ToDateTime(popupPaymentPeriodFrom.Text).ToString("dd-MMM-yyyy"),
            PaymentPeriodToDate =
                popupPaymentPeriodTo.Text == string.Empty
                    ? null
                    : Convert.ToDateTime(popupPaymentPeriodTo.Text).ToString("dd-MMM-yyyy")
        };
        using (var conn = new SqlConnection(_connectionString))
        {
            conn.Open();
            var tran = conn.BeginTransaction();
            try
            {
                var dtEmployeeTable = (DataTable)Session["excelData"];
                if (dtEmployeeTable != null)
                    foreach (DataRow dtRow in dtEmployeeTable.Rows)
                    {
                        objConveyanceCalm.EmployeeId = dtRow.ItemArray[0].ToString();
                        objConveyanceCalm.ConveyanceType = dtRow.ItemArray[3].ToString();
                        objConveyanceCalm.Quantity = dtRow.ItemArray[4].ToString();
                        objConveyanceCalm.Unit = dtRow.ItemArray[5].ToString();
                        objConveyanceCalm.RateCalculation = dtRow.ItemArray[6].ToString();
                        objConveyanceCalm.Rate = dtRow.ItemArray[7].ToString();
                        objConveyanceCalm.Amount = dtRow.ItemArray[8].ToString();
                        float unitRate = Convert.ToSingle(objConveyanceCalm.Rate) *
                                         Convert.ToSingle(objConveyanceCalm.Quantity);

                        var storedProcedureComandTestDet = "exec [ConveyanceCalmInitiate_ProcessFlowDetPaymentdet] " +
                                                           "" + 1 + "," +
                                                           "'" + objConveyanceCalm.EmployeeId + "'," +
                                                           "" + Convert.ToSingle(objConveyanceCalm.Quantity) + "," +
                                                           "'" + objConveyanceCalm.Unit + "'," +
                                                           "" + Convert.ToSingle(objConveyanceCalm.Rate) + "," +
                                                           "" + unitRate + "," +
                                                           "" + Convert.ToSingle(objConveyanceCalm.Amount) + "," +
                                                           "'" + objConveyanceCalm.ConveyanceType + "'," +
                                                           "'" + objConveyanceCalm.EmployeeId + "'," +
                                                           "'" + objConveyanceCalm.EmployeeId + "'," +
                                                           "'" + current.UserId.Trim() + "'," +
                                                           "'" + objConveyanceCalm.PaymentPeriodFromDate + "'," +
                                                           "'" + objConveyanceCalm.PaymentPeriodToDate + "'," +
                                                           "" + 1 + "";
                        StoredProcedureExecutor.StoredProcedureExecuteNonQuery(_connectionString,
                            storedProcedureComandTestDet);
                    }
                tran.Commit();
                ClearAllControl();
                MessageBox1.ShowSuccess("Conveyance Calm Submitted Successfully ");
            }
            catch (Exception exception)
            {

                tran.Rollback();
                MessageBox1.ShowError("Error:" + exception.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }

    private void ClearAllControl()
    {
        if (ddlSheetName.Items.Count > 0)
        {
            ddlSheetName.SelectedValue = "-1";
        }
        popupPaymentPeriodFrom.Text = string.Empty;
        popupPaymentPeriodTo.Text = string.Empty;
        Session["excelData"] = null;
        grdGetConveyance.DataSource = null;
        grdGetConveyance.DataBind();
    }

    private string CheckValidation()
    {
        const string checkValidation = "";
        if (ddlSheetName.Items.Count == 0)
        {
            FileUploadExcelFile.Focus();
            return "Please Upload Excel File Correctly !";
        }
        if (ddlSheetName.SelectedValue == "-1")
        {
            ddlSheetName.Focus();
            return "Please Select Sheet Name Correctly !";
        }
        if (Session["excelData"] as DataTable == null)
        {
            return "No record found in selected excel file  !";
        }

        return checkValidation;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckValidation();
        switch (validationMsg)
        {
            case "":
                {
                    SaveConveyanceCalm();
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }
    }

    protected void imgBtnExcelUpload_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (!FileUploadExcelFile.HasFile) return;
            var fileName = Path.GetFileName(FileUploadExcelFile.PostedFile.FileName);
            var extension = Path.GetExtension(FileUploadExcelFile.PostedFile.FileName);
            Session["Extension"] = extension;
            var folderPath = ConfigurationManager.AppSettings["FolderPath"];
            var filePath = Server.MapPath(folderPath + fileName);
            FileUploadExcelFile.SaveAs(filePath);
            Session["FilePath"] = filePath;
            Import_To_Grid(filePath, "Yes");
        }
        catch (Exception exception)
        {

            MessageBox1.ShowInfo("Info : " + exception.Message);
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAllControl();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string sheetName = "";
        if (ddlSheetName.SelectedValue != "-1")
            sheetName = Session["SelectedSheetNames"].ToString();

        if (sheetName == "")
            sheetName = "Sheet1$";

        OleDbConnection oconn = new OleDbConnection();
        if (FileUploadExcelFile.HasFile)
        {
            if (FileUploadExcelFile.FileContent.Length > 0)
            {
                string Foldername;
                string Extension = System.IO.Path.GetExtension(FileUploadExcelFile.PostedFile.FileName);
                string filename = Path.GetFileName(FileUploadExcelFile.PostedFile.FileName.ToString());
                DataTable dt = new DataTable();

                //dt.Columns.Add("one", typeof(string));
                //dt.Columns.Add("two", typeof(string));
                //dt.Columns.Add("three", typeof(string));

                if (Extension.ToUpper() == ".XLS" || Extension.ToUpper() == ".XLSX")
                {

                    Foldername = Server.MapPath("~/Temp/");

                    FileUploadExcelFile.PostedFile.SaveAs(Foldername + filename.ToString());

                    String conStr = "";
                    switch (Extension)
                    {
                        case ".xls": //Excel 97-03
                            conStr = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                            "Data Source=" + Foldername + "//" + filename + ";" +
                            "Extended Properties=Excel 8.0;";
                            break;

                        case ".xlsx": //Excel 07
                            conStr = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                           "Data Source=" + Foldername + "//" + filename + ";" +
                           "Extended Properties=Excel 8.0;";
                            break;
                    }

                    oconn.ConnectionString = conStr;
                    //------
                    //OleDbCommand ocmd = new OleDbCommand("select * from [" + sheetName + "]", oconn);
                    //oconn.Open();
                    //OleDbDataReader odr = ocmd.ExecuteReader();

                    var connExcel = new OleDbConnection(conStr);
                    var cmdExcel = new OleDbCommand();
                    var oda = new OleDbDataAdapter();
                    cmdExcel.Connection = connExcel;
                    connExcel.Open();
                    cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                    oda.SelectCommand = cmdExcel;
                    oda.Fill(dt);
                    connExcel.Close();

                    grdGetConveyance.DataSource = null;
                    grdGetConveyance.DataBind();
                    Session["excelData"] = null;
                    if (dt.Rows.Count > 0)
                    {
                        grdGetConveyance.DataSource = dt;
                        grdGetConveyance.DataBind();
                        Session["excelData"] = dt;
                    }

                    //

                    //while (odr.Read())
                    //{
                    //    dt.Rows.Add(odr[0].ToString(), odr[1].ToString(), odr[2].ToString());
                    //}

                }
                else
                {

                }

                //grdGetConveyance.DataSource = dt;
                //grdGetConveyance.DataBind();
            }
        }
        else
        {


        }

    }
}