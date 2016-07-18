using CrystalDecisions.Shared;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Activities.Statements;


public partial class modules_HRMS_Setup_frmEmployeeInformation : System.Web.UI.Page
{
    private const string Rnode = "K";
    private readonly EmployeeInformation _objEmployeeInformation = new EmployeeInformation();
    private string _msg;
    string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    EmployeeInformationController objForRefDepartment;
    private DocumentUploadController _objDocumentUploadController;
    private DocumentUpload _objDocumentUpload;
    string filepath = System.Configuration.ConfigurationSettings.AppSettings["upl"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        txtEmployeeCode.Attributes.Add("onkeyup", "setfocus(this," + txtEmployeeCode.ClientID + ")");
        popupJoiningDate.Attributes.Add("readonly", "readonly");
        popupDateOfBirth.Attributes.Add("readonly", "readonly");
        if (!Page.IsPostBack)
        {
            LoadCompanyByUserPermission("ADM", Rnode);
            grdAcademicRecords.DataSource = ViewState["AcademicQualification"];
            grdAcademicRecords.DataBind();
            grdDependentsInformation.DataSource = ViewState["DependentsInformation"];
            grdDependentsInformation.DataBind();
            grdAssetAllocation.DataSource = ViewState["AssetAllocation"];
            grdAssetAllocation.DataBind();
            grdProfessionalQualification.DataSource = ViewState["ProfessionalQualification"];
            grdProfessionalQualification.DataBind();
            grdTrainingRecord.DataSource = ViewState["TrainingRecord"];
            grdTrainingRecord.DataBind();
            ClsDropDownListController.EnableDisableDropDownList(ddlcompany);
            ddlcompany_SelectedIndexChanged(sender, e);
            PanelForEmployeeDetails.Visible = false;
            PanelReportingPersonDetails.Visible = false;
        }

        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }

    private void LoadBranchName(string bankCode)
    {
        ClsDropDownListController.LoadDropDownList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetBranchName(bankCode), ddlBranchName, "Bnk_info_Branch_name", "Bnk_info_Branch_Code");
    }

    private void LoadDepartmentCode(string officeLocationCode)
    {
        ClsDropDownListController.LoadDropDownList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetDepartmentCodeByOfficeLocation(officeLocationCode), ddlDepartmentCode, "Dept_Name", "Dept_Code");
    }

    private void LoadSectionCode(string officeLocationCode, string departmentCode)
    {
        ClsDropDownListController.LoadDropDownList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetSectionIntoDDL(departmentCode, officeLocationCode), ddlSectionCode, "Sect_Name", "Sect_Code");
    }

    private void LoadAllDivision(DropDownList ddDownList)
    {
        ClsDropDownListController.LoadDropDownList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetDivisionIntoDDL(), ddDownList, "DivisionName", "DivisionName");
    }

    private void LoadDistrict(DropDownList ddDropDownList, string divisionName)
    {
        ClsDropDownListController.LoadDropDownList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetDistrictIntoDDL(divisionName), ddDropDownList, "DistrictName", "DistrictName");
    }

    private void LoadReportingPersonIdForAssetAllocation()
    {
        string officeLocation = ddlOfficeLocation.SelectedValue;
        string deptCode = ddlDepartmentCode.SelectedValue;
        string empCode = txtEmployeeCode.Text == "" ? "" : txtEmployeeCode.Text;
        //ClsDropDownListController.LoadDropDownList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetReportingPersonIdIntoDDL(officeLocation, deptCode, empCode), ddlAssetReportingPerson, "Trans_Det_Emp_Id", "Trans_Det_Emp_Id");
    }

    private void BindAcademicQualificationGrid(string nameOfDegree, string institutionName, string board, string resultGrade, double passingYear, double courseDuration, string majorInGroup, string documentCodeAttaced)
    {
        var dt = new DataTable();
        DataRow dr;
        dt.Columns.Add(new DataColumn("txtNameOfDegree", typeof(String)));
        dt.Columns.Add(new DataColumn("txtInstitutionName", typeof(String)));
        dt.Columns.Add(new DataColumn("txtBoardUniversity", typeof(String)));
        dt.Columns.Add(new DataColumn("txtResultsGradeDivision", typeof(String)));
        dt.Columns.Add(new DataColumn("txtPassingYear", typeof(double)));
        dt.Columns.Add(new DataColumn("txtCourseDuration", typeof(double)));
        dt.Columns.Add(new DataColumn("txtMajor", typeof(string)));
        dt.Columns.Add(new DataColumn("FileUploadAcademic", typeof(string)));
        if (ViewState["AcademicQualification"] != null)
        {
            var dtTable = (DataTable)ViewState["AcademicQualification"];
            var count = dtTable.Rows.Count;
            for (var i = 0; i < count + 1; i++)
            {
                dt = (DataTable)ViewState["AcademicQualification"];
                if (dt.Rows.Count <= 0) continue;
                dr = dt.NewRow();
                dr[0] = dt.Rows[0][0].ToString();
            }
            dr = dt.NewRow();
            dr[0] = nameOfDegree;
            dr[1] = institutionName;
            dr[2] = board;
            dr[3] = resultGrade;
            dr[4] = passingYear;
            dr[5] = courseDuration;
            dr[6] = majorInGroup;
            dr[7] = documentCodeAttaced;
            dt.Rows.Add(dr);
        }
        else
        {
            dr = dt.NewRow();
            dr[0] = nameOfDegree;
            dr[1] = institutionName;
            dr[2] = board;
            dr[3] = resultGrade;
            dr[4] = passingYear;
            dr[5] = courseDuration;
            dr[6] = majorInGroup;
            dr[7] = documentCodeAttaced;
            dt.Rows.Add(dr);
        }
        if (ViewState["AcademicQualification"] != null)
        {
            grdAcademicRecords.DataSource = ViewState["AcademicQualification"];
            grdAcademicRecords.DataBind();
        }
        else
        {
            grdAcademicRecords.DataSource = dt;
            grdAcademicRecords.DataBind();

        }
        ViewState["AcademicQualification"] = dt;
    }

    private void BindDependentsInformationGrid(string name, string gender, string genderCode, string dateOfBirth, string relationShip)
    {
        var dt = new DataTable();
        DataRow dr;
        dt.Columns.Add(new DataColumn("txtNameDependent", typeof(String)));
        dt.Columns.Add(new DataColumn("ddlGenderDependent", typeof(String)));
        dt.Columns.Add(new DataColumn("popupDateOfBirthDependent", typeof(String)));
        dt.Columns.Add(new DataColumn("txtRelationshipWithEmployee", typeof(String)));
        dt.Columns.Add(new DataColumn("ddlGenderCodeDependent", typeof(String)));

        if (ViewState["DependentsInformation"] != null)
        {
            var dtTable = (DataTable)ViewState["DependentsInformation"];
            var count = dtTable.Rows.Count;
            for (var i = 0; i < count + 1; i++)
            {
                dt = (DataTable)ViewState["DependentsInformation"];
                if (dt.Rows.Count <= 0) continue;
                dr = dt.NewRow();
                dr[0] = dt.Rows[0][0].ToString();
            }
            dr = dt.NewRow();
            dr[0] = name;
            dr[1] = gender;
            dr[2] = dateOfBirth;
            dr[3] = relationShip;
            dr[4] = genderCode;

            dt.Rows.Add(dr);

        }
        else
        {
            dr = dt.NewRow();
            dr[0] = name;
            dr[1] = gender;
            dr[2] = dateOfBirth;
            dr[3] = relationShip;
            dr[4] = genderCode;

            dt.Rows.Add(dr);
        }
        if (ViewState["DependentsInformation"] != null)
        {
            grdDependentsInformation.DataSource = ViewState["DependentsInformation"];
            grdDependentsInformation.DataBind();
        }
        else
        {
            grdDependentsInformation.DataSource = dt;
            grdDependentsInformation.DataBind();

        }
        ViewState["DependentsInformation"] = dt;
    }

    private void BindAssetInformationGrid(string assetName, string assetIdNo, string activeDate, string inactiveDate, string status, string statusCode, string modelNo, string description, string reportingEmpId, string documentCodeAttached)
    {
        var dt = new DataTable();
        DataRow dr;
        dt.Columns.Add(new DataColumn("txtAssetName", typeof(String)));
        dt.Columns.Add(new DataColumn("txtAssetIDNo", typeof(String)));
        dt.Columns.Add(new DataColumn("popupActiveDate", typeof(String)));
        dt.Columns.Add(new DataColumn("popupInactiveDate", typeof(String)));
        dt.Columns.Add(new DataColumn("ddlStatusOfEmpForAssetAllocation", typeof(String)));
        dt.Columns.Add(new DataColumn("ddlStatusCodeOfEmpForAssetAllocation", typeof(String)));
        dt.Columns.Add(new DataColumn("txtModelNo", typeof(String)));
        dt.Columns.Add(new DataColumn("txtAssetDescription", typeof(String)));
        dt.Columns.Add(new DataColumn("ddlAssetReportingPerson", typeof(String)));
        dt.Columns.Add(new DataColumn("FileUploadAsset", typeof(String)));

        if (ViewState["AssetAllocation"] != null)
        {
            var dtTable = (DataTable)ViewState["AssetAllocation"];
            var count = dtTable.Rows.Count;
            for (var i = 0; i < count + 1; i++)
            {
                dt = (DataTable)ViewState["AssetAllocation"];
                if (dt.Rows.Count <= 0) continue;
                dr = dt.NewRow();
                dr[0] = dt.Rows[0][0].ToString();
            }
            dr = dt.NewRow();
            dr[0] = assetName;
            dr[1] = assetIdNo;
            dr[2] = activeDate;
            dr[3] = inactiveDate;
            dr[4] = status;
            dr[5] = statusCode;
            dr[6] = modelNo;
            dr[7] = description;
            dr[8] = reportingEmpId;
            dr[9] = documentCodeAttached;
            dt.Rows.Add(dr);
        }
        else
        {
            dr = dt.NewRow();
            dr[0] = assetName;
            dr[1] = assetIdNo;
            dr[2] = activeDate;
            dr[3] = inactiveDate;
            dr[4] = status;
            dr[5] = statusCode;
            dr[6] = modelNo;
            dr[7] = description;
            dr[8] = reportingEmpId;
            dr[9] = documentCodeAttached;
            dt.Rows.Add(dr);
        }
        if (ViewState["AssetAllocation"] != null)
        {
            grdAssetAllocation.DataSource = ViewState["AssetAllocation"];
            grdAssetAllocation.DataBind();
        }
        else
        {
            grdAssetAllocation.DataSource = dt;
            grdAssetAllocation.DataBind();

        }
        ViewState["AssetAllocation"] = dt;
    }

    private void BindProfessionalRecordGrid(string nameOfInstitution, string institutionAddress, string serviceStartedDate, string serviceEndDate, string designationPrevious, string serviceDescription, double grossSalary, string documentCodeAttached)
    {
        var dt = new DataTable();
        DataRow dr;
        dt.Columns.Add(new DataColumn("txtNameofInstitution", typeof(String)));
        dt.Columns.Add(new DataColumn("txtInstitutionAddress", typeof(String)));
        dt.Columns.Add(new DataColumn("popupServiceStartDate", typeof(String)));
        dt.Columns.Add(new DataColumn("popupServiceEndDate", typeof(String)));
        dt.Columns.Add(new DataColumn("txtDesignationPrevious", typeof(String)));
        dt.Columns.Add(new DataColumn("txtServiceDescription", typeof(String)));
        dt.Columns.Add(new DataColumn("txtLastGrossSalary", typeof(String)));
        dt.Columns.Add(new DataColumn("FileUploadExperiance", typeof(String)));

        if (ViewState["ProfessionalQualification"] != null)
        {
            var dtTable = (DataTable)ViewState["ProfessionalQualification"];
            var count = dtTable.Rows.Count;
            for (var i = 0; i < count + 1; i++)
            {
                dt = (DataTable)ViewState["ProfessionalQualification"];
                if (dt.Rows.Count <= 0) continue;
                dr = dt.NewRow();
                dr[0] = dt.Rows[0][0].ToString();
            }
            dr = dt.NewRow();
            dr[0] = nameOfInstitution;
            dr[1] = institutionAddress;
            dr[2] = serviceStartedDate;
            dr[3] = serviceEndDate;
            dr[4] = designationPrevious;
            dr[5] = serviceDescription;
            dr[6] = grossSalary;
            dr[7] = documentCodeAttached;
            dt.Rows.Add(dr);
        }
        else
        {
            dr = dt.NewRow();
            dr[0] = nameOfInstitution;
            dr[1] = institutionAddress;
            dr[2] = serviceStartedDate;
            dr[3] = serviceEndDate;
            dr[4] = designationPrevious;
            dr[5] = serviceDescription;
            dr[6] = grossSalary;
            dr[7] = documentCodeAttached;
            dt.Rows.Add(dr);
        }
        if (ViewState["ProfessionalQualification"] != null)
        {
            grdProfessionalQualification.DataSource = ViewState["ProfessionalQualification"];
            grdProfessionalQualification.DataBind();
        }
        else
        {
            grdProfessionalQualification.DataSource = dt;
            grdProfessionalQualification.DataBind();
        }
        ViewState["ProfessionalQualification"] = dt;
    }

    private void BindTrainingRecordGrid(string trainingCode, string trainingTitle, string nameOfInstitution, string institutionAddress, string startedDate, string endDate, int trainingDuration, double trainingFee, string trainingAchievement,
                                        string trainingTitleSpecification, string certificateCode, string certificateTitle, string fundCode, string fundTitle, string documentCodeAttached)
    {
        var dt = new DataTable();
        DataRow dr;
        dt.Columns.Add(new DataColumn("ddlTrainingTitle", typeof(String)));
        dt.Columns.Add(new DataColumn("txtNameofInstitution", typeof(String)));
        dt.Columns.Add(new DataColumn("txtInstitutionAddress", typeof(String)));
        dt.Columns.Add(new DataColumn("calendarTrainingStartDate", typeof(String)));
        dt.Columns.Add(new DataColumn("calendarTrainingEndDate", typeof(String)));
        dt.Columns.Add(new DataColumn("txtDuration", typeof(String)));
        dt.Columns.Add(new DataColumn("txtTrainingFee", typeof(String)));
        dt.Columns.Add(new DataColumn("ddlTrainingCode", typeof(String)));
        dt.Columns.Add(new DataColumn("txtTrainingBenefits", typeof(String)));

        dt.Columns.Add(new DataColumn("txtTrainingTitleSpecification", typeof(String)));
        dt.Columns.Add(new DataColumn("ddlcertificateCode", typeof(String)));
        dt.Columns.Add(new DataColumn("ddlcertificateTitle", typeof(String)));
        dt.Columns.Add(new DataColumn("ddlfundCode", typeof(String)));
        dt.Columns.Add(new DataColumn("ddlfundTitle", typeof(String)));
        dt.Columns.Add(new DataColumn("FileUploadTraining", typeof(String)));

        if (ViewState["TrainingRecord"] != null)
        {
            var dtTable = (DataTable)ViewState["TrainingRecord"];
            var count = dtTable.Rows.Count;
            for (var i = 0; i < count + 1; i++)
            {
                dt = (DataTable)ViewState["TrainingRecord"];
                if (dt.Rows.Count <= 0) continue;
                dr = dt.NewRow();
                dr[0] = dt.Rows[0][0].ToString();
            }
            dr = dt.NewRow();
            dr[0] = trainingTitle;
            dr[1] = nameOfInstitution;
            dr[2] = institutionAddress;
            dr[3] = startedDate;
            dr[4] = endDate;
            dr[5] = trainingDuration;
            dr[6] = trainingFee;
            dr[7] = trainingCode;
            dr[8] = trainingAchievement;
            dr[9] = trainingTitleSpecification;
            dr[10] = certificateCode;
            dr[11] = certificateTitle;
            dr[12] = fundCode;
            dr[13] = fundTitle;
            dr[14] = documentCodeAttached;
            dt.Rows.Add(dr);
        }
        else
        {
            dr = dt.NewRow();
            dr[0] = trainingTitle;
            dr[1] = nameOfInstitution;
            dr[2] = institutionAddress;
            dr[3] = startedDate;
            dr[4] = endDate;
            dr[5] = trainingDuration;
            dr[6] = trainingFee;
            dr[7] = trainingCode;
            dr[8] = trainingAchievement;
            dr[9] = trainingTitleSpecification;
            dr[10] = certificateCode;
            dr[11] = certificateTitle;
            dr[12] = fundCode;
            dr[13] = fundTitle;
            dr[14] = documentCodeAttached;
            dt.Rows.Add(dr);
        }
        if (ViewState["TrainingRecord"] != null)
        {
            grdTrainingRecord.DataSource = ViewState["TrainingRecord"];
            grdTrainingRecord.DataBind();
        }
        else
        {
            grdTrainingRecord.DataSource = dt;
            grdTrainingRecord.DataBind();
        }
        ViewState["TrainingRecord"] = dt;
    }

    private void LoadCompanyByUserPermission(string userid, string nodeid)
    {
        DataTable dt = new DataTable();
        ListItem lst;
        ddlcompany.Items.Clear();
        ddlcompany.Items.Add("");
        dt = AccessPermission.GetCompanyByUserandNodeid(ConnectionString, userid, nodeid);
        foreach (DataRow dr in dt.Rows)
        {
            lst = new ListItem();
            lst.Text = dr["COMP_CODE"].ToString() + ":" + dr["COMP_NAME"].ToString();
            lst.Value = dr["COMP_CODE"].ToString();
            ddlcompany.Items.Add(lst);
        }
    }

    protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dbname = ddlcompany.SelectedItem.Value;
        var constr = ConfigurationSettings.AppSettings["SCFConnectionString"].Replace("SCF", dbname);
        Session[GlobalData.sessionConnectionstring] = constr;
        txtEmployeeCode_AutoCompleteExtender.ContextKey = Session[GlobalData.sessionConnectionstring].ToString();
        txtEmployeeSearch_AutoCompleteExtender.ContextKey = Session[GlobalData.sessionConnectionstring].ToString();
        txtEmployeeIDForRef_AutoCompleteExtender.ContextKey = Session[GlobalData.sessionConnectionstring].ToString();
        txtAssetReportingPerson_AutoCompleteExtender.ContextKey = Session[GlobalData.sessionConnectionstring].ToString();
        ClsDropDownListController.LoadDropDownList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetWorkLocationIntoDDL(), ddlWorkLocation, "WorkLocationName", "WorkLocationId");
        LoadAllDivision(ddlDivisionPre);
        LoadAllDivision(ddlDivisionPer);
        ClsDropDownListController.LoadDropDownList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetDesignationIntoDDL(), ddlDesignation, "JobTitle", "JobCode");
        ClsDropDownListController.LoadDropDownList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetLeaveTypeIntoDDLByEmpid(""), ddlLeaveType, "Leave_Mas_Name", "Leave_Mas_Code");
        ClsDropDownListController.LoadDropDownList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetBankNameIntoDDL(), ddlBankName, "Bnk_info_Bnk_Name", "Bnk_info_Bnk_code");
        ClsDropDownListController.LoadDropDownList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetOfficeLocationIntoDDL(), ddlOfficeLocation, "Division_Master_Name", "Division_Master_Code");
        ClsDropDownListController.LoadDropDownList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetCompanyIntoDDL(), ddlCompanyNameForRef, "CompanyName", "CompanyId");
        ClsDropDownListController.LoadDropDownList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlTrainingIntoDDL(), ddlTraining, "trainingTitle", "trainingCode");
        ClsDropDownListController.LoadDropDownList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetShiftTypeIntoDDL(), ddlShift, "Shift", "Shift Code");
    }

    protected void btnAddAcademicRecord_Click(object sender, EventArgs e)
    {
        ModalPopupExtenderForAcademicRecord.Show();
        txtNameOfDegree.Focus();
        ClearControlsOfAcademicRecords();
        btnAddSingleAcademicRecord.Text = "Add";
    }

    protected void btnAddDependentInformation_Click(object sender, EventArgs e)
    {
        ModalPopupExtenderForDependentsInformation.Show();
        txtNameDependent.Focus();
        btnAddDependentsInformatonSingle.Text = "Add";
        ClearControlOfDependentInformation();
    }

    protected void btnAddAssetRecord_Click(object sender, EventArgs e)
    {
        //LoadReportingPersonIdForAssetAllocation();
        ddlStatusOfEmpForAssetAllocation.SelectedValue = "Y";
        ddlStatusOfEmpForAssetAllocation.Enabled = false;
        ModalPopupExtenderAssetAllocation.Show();
        popupInactiveDate.DisableTextBoxEntry = true;
        popupInactiveDate.Enabled = false;
        txtAssetName.Focus();
        CleatControlsOfAssetAllocation();
        btnAddSingleAsstInformation.Text = "Add";
    }

    private string CheckValidationForAcademicRecord(string nameOfDegree, string institutionName, string board, string resultGrade, double passingYear, double courseDuration)
    {
        const string checkValidation = "";
        if (nameOfDegree == "")
        {
            txtNameOfDegree.Focus();
            return "Must Enter Name Of Degree";
        }
        if (institutionName == "")
        {
            txtInstitutionName.Focus();
            return "Must Enter Institution Name";
        }
        if (board == "")
        {
            txtBoardUniversity.Focus();
            return "Must Enter Board/University Name";
        }
        if (resultGrade == "")
        {
            txtResultsGradeDivision.Focus();
            return "Must Enter Results Grade/Division";
        }
        if (btnAddSingleAcademicRecord.Text == "Update")
        {
            UpdateAcademicRecord();
        }

        if (nameOfDegree == "" || ViewState["AcademicQualification"] == null) return checkValidation;
        var dt = (DataTable)ViewState["AcademicQualification"];
        var any = (from DataRow dr in dt.Rows select dr["txtNameOfDegree"].ToString()).Any(columnValue => nameOfDegree == columnValue);
        if (
            !any) return checkValidation;
        txtNameOfDegree.Focus();
        return nameOfDegree + " Degree Already Exists";
    }

    private string CheckValidationForProfessionalRecord(string nameOfInstitution)
    {
        const string checkValidation = "";
        if (nameOfInstitution != "") return checkValidation;
        txtNameofInstitution.Focus();
        return "Must Enter Name Of Institution";
    }

    private string CheckValidationForTrainingRecord(string trainingCode)
    {
        const string checkValidation = "";
        //if (ddlTraining.SelectedValue == "-1")
        //{
        //    return "Please Select Training Topic Type";
        //}

        //foreach (GridViewRow rowNumber in grdTrainingRecord.Rows)
        //{
        //    if (rowNumber.Cells[12].Text.ToString().Equals(trainingCode))
        //    {
        //        return ddlTraining.SelectedItem.Text + "  Already Exists";
        //    }
        //}
        return checkValidation;
    }

    private string CheckValidationForAssetAllocation(string assetName)
    {
        const string checkValidation = "";
        if (assetName != "") return checkValidation;
        txtAssetName.Focus();
        return "Must Enter Name Of Asset";
    }

    private string CheckValidationForDependentsInformation(string name)
    {
        const string checkValidation = "";
        if (name != "") return checkValidation;
        txtNameDependent.Focus();
        return "Must Enter Name of The Persion";
    }

    private void CleatControlsOfAssetAllocation()
    {
        txtAssetName.Text = "";
        txtAssetIDNo.Text = "";
        txtModelNo.Text = string.Empty;
        txtAssetDescription.Text = string.Empty;
        txtAssetReportingPerson.Text = string.Empty;
        PanelReportingPersonDetails.Visible = false;
    }

    private void ClearControlsOfAcademicRecords()
    {
        txtNameOfDegree.Text = "";
        txtInstitutionName.Text = "";
        txtBoardUniversity.Text = "";
        txtResultsGradeDivision.Text = "";
        txtPassingYear.Text = "";
        txtCourseDuration.Text = "";
        txtMajor.Text = string.Empty;
        txtNameOfDegree.Enabled = true;
    }

    private void ClearControlOfProfessionalRecords()
    {
        txtNameofInstitution.Text = "";
        txtInstitutionAddress.Text = "";
        txtDesignationPrevious.Text = "";
        txtServiceDescription.Text = "";
        txtLastGrossSalary.Text = "";
    }

    private void ClearControlOfTrainingRecord()
    {
        //ddlTraining.SelectedValue = "-1";
        txtTrainingInstituteName.Text = string.Empty;
        txtTrainingInstituteAddress.Text = string.Empty;
        txtTrainingFee.Text = string.Empty;
        txtDuration.Text = string.Empty;
        txtTrainingBenefits.Text = string.Empty;
        txtCertificateAchieved.Text = string.Empty;
        txtFundBy.Text = string.Empty;
        txtTrainingTitle.Text = string.Empty;
    }

    private void ClearControlOfDependentInformation()
    {
        txtNameDependent.Text = "";
        txtRelationshipWithEmployee.Text = "";
    }
    private void UpdateAcademicRecord()
    {
        if ((DataTable)ViewState["AcademicQualification"] != null)
        {
            var indexForDelete = Convert.ToInt32(Session["indexAcademicRecords"].ToString());
            var dt = (DataTable)ViewState["AcademicQualification"];
            dt.Rows[indexForDelete].Delete();
            dt.AcceptChanges();
            ViewState["AcademicQualification"] = dt;
            btnAddSingleAcademicRecord.Text = "Add";
        }

    }

    protected void btnAddSingleAcademicRecord_Click(object sender, EventArgs e)
    {
        var nameOfDegree = txtNameOfDegree.Text == "" ? "" : txtNameOfDegree.Text;
        var institutionName = txtInstitutionName.Text == "" ? "" : txtInstitutionName.Text;
        var board = txtBoardUniversity.Text == "" ? "" : txtBoardUniversity.Text;
        var resultGrade = txtResultsGradeDivision.Text == "" ? "" : txtResultsGradeDivision.Text;
        var passingYear = Convert.ToDouble(txtPassingYear.Text == "" ? 0 : Convert.ToDouble(txtPassingYear.Text));
        var courseDuration = Convert.ToDouble(txtCourseDuration.Text == "" ? 0 : Convert.ToDouble(txtCourseDuration.Text));
        var majorGroup = txtMajor.Text == string.Empty ? "" : txtMajor.Text;
        var documentCode = AttachFileSaveAccordingToCatagory(FileUploadAcademic);
        if (btnAddSingleAcademicRecord.Text != "Add" && documentCode == null)
        {
            documentCode = Session["academicDocument"].ToString() == "" ? null : Session["academicDocument"].ToString();
        }



        var validadionMsg = CheckValidationForAcademicRecord(nameOfDegree, institutionName, board, resultGrade, passingYear, courseDuration);
        if (validadionMsg == "")
        {
            BindAcademicQualificationGrid(nameOfDegree, institutionName, board, resultGrade, passingYear, courseDuration, majorGroup, documentCode);
            lblForErrorMSGAccademic.Text = validadionMsg;
            ClearControlsOfAcademicRecords();
        }
        else
        {
            lblForErrorMSGAccademic.Text = validadionMsg;
            ModalPopupExtenderForAcademicRecord.Show();
        }
    }

    protected void grdAcademicRecords_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    private void GetFile(String strPath, String strSuggestedName)
    {

        String strServerPath;
        System.IO.FileInfo objSourceFileInfo;
        strServerPath = this.Server.MapPath(strSuggestedName);
        objSourceFileInfo = new System.IO.FileInfo(strPath);

        if (objSourceFileInfo.Exists)
        {
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + strSuggestedName);
            Response.AddHeader("Content-Length", objSourceFileInfo.Length.ToString());
            Response.WriteFile(objSourceFileInfo.FullName);
            Response.Flush();
            Response.End();
        }
        else
        {
            Response.Write("This file does not exist.");
        }
    }
    protected void grdAcademicRecords_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var con = Convert.ToInt32(e.CommandArgument.ToString());
        Session["rowCommandName"] = "";
        if (e.CommandName.Equals("DownloadFile"))
        {
            Session["rowCommandName"] = "fileDownload";
            string gg = grdAcademicRecords.Rows[con].Cells[8].Text.Trim();
            String F1Path, F1Name;
            string abc = filepath + "\\" + gg.ToString().Replace("&amp;", "&");
            F1Path = abc.ToString();
            F1Name = Path.GetFileName(F1Path);
            GetFile(F1Path, F1Name);
        }
        else if (e.CommandName.Equals("Select"))
        {
            Session["indexAcademicRecords"] = con;
            txtNameOfDegree.Text = grdAcademicRecords.Rows[con].Cells[1].Text == "&nbsp;" ? "" : grdAcademicRecords.Rows[con].Cells[1].Text;
            txtInstitutionName.Text = grdAcademicRecords.Rows[con].Cells[2].Text == "&nbsp;" ? "" : grdAcademicRecords.Rows[con].Cells[2].Text;
            txtBoardUniversity.Text = grdAcademicRecords.Rows[con].Cells[3].Text == "&nbsp;" ? "" : grdAcademicRecords.Rows[con].Cells[3].Text;
            txtMajor.Text = grdAcademicRecords.Rows[con].Cells[4].Text == "&nbsp;" ? "" : grdAcademicRecords.Rows[con].Cells[4].Text;
            txtResultsGradeDivision.Text = grdAcademicRecords.Rows[con].Cells[5].Text == "&nbsp;" ? "" : grdAcademicRecords.Rows[con].Cells[5].Text;
            txtPassingYear.Text = grdAcademicRecords.Rows[con].Cells[6].Text == "&nbsp;" ? "" : grdAcademicRecords.Rows[con].Cells[6].Text;
            txtCourseDuration.Text = grdAcademicRecords.Rows[con].Cells[7].Text == "&nbsp;" ? "" : grdAcademicRecords.Rows[con].Cells[7].Text;
            ModalPopupExtenderForAcademicRecord.Show();
            btnAddSingleAcademicRecord.Text = "Update";
            txtNameOfDegree.Enabled = false;
            Session["academicDocument"] = grdAcademicRecords.Rows[con].Cells[8].Text == "&nbsp;" ? "" : grdAcademicRecords.Rows[con].Cells[8].Text;

        }
        else
        {
            if (!e.CommandName.Equals("Delete")) return;
            var dt = (DataTable)ViewState["AcademicQualification"];
            dt.Rows[con].Delete();
            dt.AcceptChanges();
            ViewState["AcademicQualification"] = dt;
            if (ViewState["AcademicQualification"] == null) return;
            grdAcademicRecords.DataSource = ViewState["AcademicQualification"];
            grdAcademicRecords.DataBind();
        }



    }

    private void UpdateDependentInformation()
    {
        if ((DataTable)ViewState["DependentsInformation"] != null)
        {
            var indexForDelete = Convert.ToInt32(Session["indexDependents"].ToString());
            var dt = (DataTable)ViewState["DependentsInformation"];
            dt.Rows[indexForDelete].Delete();
            dt.AcceptChanges();
            ViewState["DependentsInformation"] = dt;
            btnAddDependentsInformatonSingle.Text = "Add";
        }
    }

    protected void btnAddDependentsInformatonSingle_Click(object sender, EventArgs e)
    {
        var name = txtNameDependent.Text;
        var genderCode = ddlGenderDependent.SelectedValue;
        var gender = ddlGenderDependent.SelectedItem.ToString();
        var dateOfBirth = Convert.ToDateTime(popupDateOfBirthDependent.SelectedDate).ToString("dd-MMM-yyyy");
        var relationShip = txtRelationshipWithEmployee.Text;
        var validationMsg = CheckValidationForDependentsInformation(name);
        if (validationMsg == "")
        {
            if (btnAddDependentsInformatonSingle.Text == "Update")
            {
                UpdateDependentInformation();
            }

            BindDependentsInformationGrid(name, gender, genderCode, dateOfBirth, relationShip);
            ClearControlOfDependentInformation();
            lblForErrorMSGOfDependentsInformation.Text = "";
        }
        else
        {
            lblForErrorMSGOfDependentsInformation.Text = validationMsg;
            ModalPopupExtenderForDependentsInformation.Show();
        }
    }

    protected void grdDependentsInformation_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void grdDependentsInformation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var con = Convert.ToInt32(e.CommandArgument.ToString());
        if (e.CommandName.Equals("Select"))
        {
            Session["indexDependents"] = con;
            txtNameDependent.Text = grdDependentsInformation.Rows[con].Cells[1].Text == "&nbsp;" ? "" : grdDependentsInformation.Rows[con].Cells[1].Text;
            ddlGenderDependent.SelectedValue = grdDependentsInformation.Rows[con].Cells[5].Text;
            popupDateOfBirthDependent.SelectedDate = Convert.ToDateTime(grdDependentsInformation.Rows[con].Cells[3].Text);
            txtRelationshipWithEmployee.Text = grdDependentsInformation.Rows[con].Cells[4].Text == "&nbsp;" ? "" : grdDependentsInformation.Rows[con].Cells[4].Text;
            ModalPopupExtenderForDependentsInformation.Show();
            btnAddDependentsInformatonSingle.Text = "Update";
        }

        if (!e.CommandName.Equals("Delete")) return;
        var dt = (DataTable)ViewState["DependentsInformation"];
        dt.Rows[con].Delete();
        dt.AcceptChanges();
        ViewState["DependentsInformation"] = dt;
        if (ViewState["DependentsInformation"] == null) return;
        grdDependentsInformation.DataSource = ViewState["DependentsInformation"];
        grdDependentsInformation.DataBind();
    }
    private void UpdateAsstInformation()
    {
        if ((DataTable)ViewState["AssetAllocation"] != null)
        {
            var indexForDelete = Convert.ToInt32(Session["indexAssetAllocation"].ToString());
            var dt = (DataTable)ViewState["AssetAllocation"];
            dt.Rows[indexForDelete].Delete();
            dt.AcceptChanges();
            ViewState["AssetAllocation"] = dt;
            btnAddSingleAsstInformation.Text = "Add";
        }

    }

    protected void btnAddSingleAsstInformation_Click(object sender, EventArgs e)
    {
        var assetName = txtAssetName.Text;
        var assetIdNo = txtAssetIDNo.Text;
        var activeDate = Convert.ToDateTime(popupActiveDate.SelectedDate).ToString("dd-MMM-yyyy");
        var inactiveDate = popupInactiveDate.SelectedDate.ToString() == "1/01/0001 12:00:00 AM" ? "" : Convert.ToDateTime(popupInactiveDate.SelectedDate).ToString("dd-MMM-yyyy");
        var statusCode = ddlStatusOfEmpForAssetAllocation.SelectedValue;
        var status = ddlStatusOfEmpForAssetAllocation.SelectedItem.ToString();
        var modelNo = txtModelNo.Text;
        var description = txtAssetDescription.Text;
        var reportingEmpId = txtAssetReportingPerson.Text == string.Empty ? "" : txtAssetReportingPerson.Text;
        var documentCode = AttachFileSaveAccordingToCatagory(FileUploadAsset);
        if (btnAddSingleAsstInformation.Text != "Add" && documentCode == null)
        {
            documentCode = Session["assetDocument"].ToString() == "" ? null : Session["assetDocument"].ToString();
        }


        var validationMsg = CheckValidationForAssetAllocation(assetName);
        if (validationMsg == "")
        {
            if (btnAddSingleAsstInformation.Text == "Update")
            {
                UpdateAsstInformation();
            }

            BindAssetInformationGrid(assetName, assetIdNo, activeDate, inactiveDate, status, statusCode, modelNo, description, reportingEmpId, documentCode);
            CleatControlsOfAssetAllocation();
            lblForErrorMSGOfAssetAllocation.Text = "";
        }
        else
        {
            lblForErrorMSGOfAssetAllocation.Text = validationMsg;
            ModalPopupExtenderAssetAllocation.Show();
        }
    }

    protected void grdAssetAllocation_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void grdAssetAllocation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var con = Convert.ToInt32(e.CommandArgument.ToString());
        Session["rowCommandName"] = "";
        if (e.CommandName == "Download")
        {
            Session["rowCommandName"] = "fileDownload";
            string gg = grdAssetAllocation.Rows[con].Cells[10].Text.Trim();
            String F1Path, F1Name;
            string abc = filepath + "\\" + gg.ToString().Replace("&amp;", "&");
            F1Path = abc.ToString();
            F1Name = Path.GetFileName(F1Path);
            GetFile(F1Path, F1Name);
        }


        if (e.CommandName.Equals("Select"))
        {
            Session["indexAssetAllocation"] = con;

            txtAssetName.Text = grdAssetAllocation.Rows[con].Cells[1].Text == "&nbsp;" ? "" : grdAssetAllocation.Rows[con].Cells[1].Text;
            txtAssetIDNo.Text = grdAssetAllocation.Rows[con].Cells[2].Text == "&nbsp;" ? "" : grdAssetAllocation.Rows[con].Cells[2].Text;
            var tempActiveDate = grdAssetAllocation.Rows[con].Cells[6].Text == "&nbsp;" ? "" : grdAssetAllocation.Rows[con].Cells[6].Text;
            if (tempActiveDate != "")
            {
                popupActiveDate.SelectedDate = Convert.ToDateTime(tempActiveDate);
            }


            var tempInactiveDate = grdAssetAllocation.Rows[con].Cells[7].Text == "&nbsp;" ? "" : grdAssetAllocation.Rows[con].Cells[7].Text;
            if (tempInactiveDate != "")
            {
                popupInactiveDate.SelectedDate = Convert.ToDateTime(tempInactiveDate);
            }


            ddlStatusOfEmpForAssetAllocation.SelectedValue = grdAssetAllocation.Rows[con].Cells[9].Text;
            txtModelNo.Text = grdAssetAllocation.Rows[con].Cells[3].Text == "&nbsp;" ? "" : grdAssetAllocation.Rows[con].Cells[3].Text;
            txtAssetDescription.Text = grdAssetAllocation.Rows[con].Cells[4].Text == "&nbsp;" ? "" : grdAssetAllocation.Rows[con].Cells[4].Text;
            txtAssetReportingPerson.Text = grdAssetAllocation.Rows[con].Cells[5].Text == "&nbsp;" ? "" : grdAssetAllocation.Rows[con].Cells[5].Text;
            ModalPopupExtenderAssetAllocation.Show();
            btnAddSingleAsstInformation.Text = "Update";
            Session["assetDocument"] = grdAssetAllocation.Rows[con].Cells[10].Text == "&nbsp;" ? "" : grdAssetAllocation.Rows[con].Cells[10].Text;
            
        }


        if (!e.CommandName.Equals("Delete")) return;
        var dt = (DataTable)ViewState["AssetAllocation"];
        dt.Rows[con].Delete();
        dt.AcceptChanges();
        ViewState["AssetAllocation"] = dt;
        if (ViewState["AssetAllocation"] == null) return;
        grdAssetAllocation.DataSource = ViewState["AssetAllocation"];
        grdAssetAllocation.DataBind();
    }

    private string CheckValidationForLeaveEntry()
    {
        const string checkValidation = "";
        if (ddlcompany.SelectedValue == "")
        {
            ddlcompany.Focus();
            return "Please Select Company !";
        }
        if (ddlLeaveType.SelectedItem.Value == "-1")
        {
            ddlLeaveType.Focus();
            return "Please Select Leave Type Correctly";
        }
        if (lstForLeaveAllocation.Items.FindByText(ddlLeaveType.SelectedItem.ToString()) != null)
        {
            return ddlLeaveType.SelectedItem + "  alrady exists ! ";
        }
        return checkValidation;
    }

    protected void btnAddLeaveIntoList_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckValidationForLeaveEntry();
        switch (validationMsg)
        {
            case "":
                {
                    lstForLeaveAllocation.Items.Add(new ListItem(ddlLeaveType.SelectedItem.ToString(), ddlLeaveType.SelectedValue));
                    lstForLeaveAllocation.AppendDataBoundItems = true;
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

    protected void lstForLeaveAllocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (var i = 0; i < lstForLeaveAllocation.Items.Count; i++)
        {
            if (!lstForLeaveAllocation.Items[i].Selected) continue;
            lstForLeaveAllocation.Items.Remove(lstForLeaveAllocation.Items[i]);
        }
    }

    protected void btnAddProfessionalRecord_Click(object sender, EventArgs e)
    {
        ModalPopupExtenderForProfessionalRecord.Show();
        txtNameofInstitution.Focus();
        ClearControlOfProfessionalRecords();
        btnAddProfessionalRecordSingle.Text = "Add";
    }
    private void UpdateProfessionalRecord()
    {
        if ((DataTable)ViewState["ProfessionalQualification"] != null)
        {
            var indexForDelete = Convert.ToInt32(Session["indexProfessionalRecords"].ToString());
            var dt = (DataTable)ViewState["ProfessionalQualification"];
            dt.Rows[indexForDelete].Delete();
            dt.AcceptChanges();
            ViewState["ProfessionalQualification"] = dt;
            btnAddProfessionalRecordSingle.Text = "Add";
        }

    }

    protected void btnAddProfessionalRecordSingle_Click(object sender, EventArgs e)
    {
        var nameOfInstitution = txtNameofInstitution.Text;
        var institutionAddress = txtInstitutionAddress.Text;
        var serviceStartedDate = Convert.ToDateTime(popupServiceStartDate.SelectedDate).ToString("dd-MMM-yyyy");
        var serviceEndDate = Convert.ToDateTime(popupServiceEndDate.SelectedDate).ToString("dd-MMM-yyyy");
        var designationPrevious = txtDesignationPrevious.Text;
        var serviceDescription = txtServiceDescription.Text;
        var grossSalary = Convert.ToDouble(txtLastGrossSalary.Text == "" ? 0 : Convert.ToDouble(txtLastGrossSalary.Text));
        var documentCode = AttachFileSaveAccordingToCatagory(FileUploadExperiance);
        if (btnAddProfessionalRecordSingle.Text != "Add" && documentCode == null)
        {
            documentCode = Session["professionalDocument"].ToString() == "" ? null : Session["professionalDocument"].ToString();
        }


        var validationMsg = CheckValidationForProfessionalRecord(nameOfInstitution);
        if (validationMsg == "")
        {
            if (btnAddProfessionalRecordSingle.Text == "Update")
            {
                UpdateProfessionalRecord();
            }


            BindProfessionalRecordGrid(nameOfInstitution, institutionAddress, serviceStartedDate, serviceEndDate, designationPrevious, serviceDescription, grossSalary, documentCode);
            ClearControlOfProfessionalRecords();
            lblForErrorMsgProfessionalRecord.Text = "";
        }
        else
        {
            lblForErrorMsgProfessionalRecord.Text = validationMsg;
            ModalPopupExtenderForProfessionalRecord.Show();
        }
    }

    protected void grdProfessionalQualification_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void grdProfessionalQualification_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var con = Convert.ToInt32(e.CommandArgument.ToString());
        Session["rowCommandName"] = "";
        if (e.CommandName == "Download")
        {
            Session["rowCommandName"] = "fileDownload";
            string gg = grdProfessionalQualification.Rows[con].Cells[8].Text.Trim();
            String F1Path, F1Name;
            string abc = filepath + "\\" + gg.ToString().Replace("&amp;", "&");
            F1Path = abc.ToString();
            F1Name = Path.GetFileName(F1Path);
            GetFile(F1Path, F1Name);
        }


        if (e.CommandName.Equals("Select"))
        {
            Session["indexProfessionalRecords"] = con;
            txtNameofInstitution.Text = grdProfessionalQualification.Rows[con].Cells[1].Text == "&nbsp;" ? "" : grdProfessionalQualification.Rows[con].Cells[1].Text;
            txtInstitutionAddress.Text = grdProfessionalQualification.Rows[con].Cells[2].Text == "&nbsp;" ? "" : grdProfessionalQualification.Rows[con].Cells[2].Text;
            var tempSerciceStartDate = grdProfessionalQualification.Rows[con].Cells[3].Text == "&nbsp;" ? "" : grdProfessionalQualification.Rows[con].Cells[3].Text;
            if (tempSerciceStartDate != "")
            {
                popupServiceStartDate.SelectedDate = Convert.ToDateTime(tempSerciceStartDate);
            }
            var tempServiceEndDate = grdProfessionalQualification.Rows[con].Cells[4].Text == "&nbsp;" ? "" : grdProfessionalQualification.Rows[con].Cells[4].Text;
            if (tempServiceEndDate != "")
            {
                popupServiceEndDate.SelectedDate = Convert.ToDateTime(tempServiceEndDate);
            }
            txtDesignationPrevious.Text = grdProfessionalQualification.Rows[con].Cells[5].Text == "&nbsp;" ? "" : grdProfessionalQualification.Rows[con].Cells[5].Text;
            txtServiceDescription.Text = grdProfessionalQualification.Rows[con].Cells[6].Text == "&nbsp;" ? "" : grdProfessionalQualification.Rows[con].Cells[6].Text;
            txtLastGrossSalary.Text = grdProfessionalQualification.Rows[con].Cells[7].Text == "&nbsp;" ? "" : grdProfessionalQualification.Rows[con].Cells[7].Text;
            ModalPopupExtenderForProfessionalRecord.Show();
            btnAddProfessionalRecordSingle.Text = "Update";
            Session["professionalDocument"] = grdProfessionalQualification.Rows[con].Cells[8].Text == "&nbsp;" ? "" : grdProfessionalQualification.Rows[con].Cells[8].Text;
        }


        if (!e.CommandName.Equals("Delete")) return;
        var dt = (DataTable)ViewState["ProfessionalQualification"];
        dt.Rows[con].Delete();
        dt.AcceptChanges();
        ViewState["ProfessionalQualification"] = dt;
        if (ViewState["ProfessionalQualification"] == null) return;
        grdProfessionalQualification.DataSource = ViewState["ProfessionalQualification"];
        grdProfessionalQualification.DataBind();
    }

    protected void grdAcademicRecords_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string documentCode = e.Row.Cells[8].Text;
            if (documentCode == "&nbsp;")
            {
                e.Row.FindControl("ImageButtonDownloadAcademic").Visible = false;
            }

            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[8].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Visible = false;
        }
    }

    protected void grdProfessionalQualification_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string documentCode = e.Row.Cells[8].Text;
            if (documentCode == "&nbsp;")
            {
                e.Row.FindControl("ImageButtonDownloadAcademic").Visible = false;
            }
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[8].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Visible = false;
        }
    }

    protected void grdDependentsInformation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[5].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[5].Visible = false;
        }
    }

    protected void grdAssetAllocation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string documentCode = e.Row.Cells[10].Text;
            if (documentCode == "&nbsp;")
            {
                e.Row.FindControl("ImageButtonDownloadAcademic").Visible = false;
            }
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
        }
    }

    protected void ddlOfficeLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        var officeLocation = ddlOfficeLocation.SelectedValue;
        if (officeLocation == "-1")
        {
            ddlDepartmentCode.Items.Clear();
            ddlSectionCode.Items.Clear();
            ClsDropDownListController.LoadDropDownList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetWorkLocationIntoDDL(), ddlWorkLocation, "WorkLocationName", "WorkLocationId");
        }
        else
        {
            LoadDepartmentCode(officeLocation);
            ClsDropDownListController.LoadDropDownList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetWorkLocationIntoDDL(officeLocation), ddlWorkLocation, "WorkLocationName", "WorkLocationId");
        }
    }

    protected void ddlDepartmentCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        var officeLocation = ddlOfficeLocation.SelectedValue;
        var departmentCode = ddlDepartmentCode.SelectedValue;
        if (officeLocation == "-1" || departmentCode == "-1")
        {
            ddlSectionCode.Items.Clear();
        }
        else
        {
            LoadSectionCode(officeLocation, departmentCode);

        }
    }

    protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
    {
        var bankCode = ddlBankName.SelectedValue;
        if (bankCode == "-1")
        {
            ddlBranchName.Items.Clear();
        }
        else
        {
            LoadBranchName(bankCode);
        }
    }

    private string SaveAllInformation()
    {
        _objEmployeeInformation.EmployeeCode = txtEmployeeCode.Text;
        _objEmployeeInformation.EmployeeFirstName = txtEmployeeFirstName.Text;
        _objEmployeeInformation.EmployeeLastName = txtEmployeeLastName.Text;
        _objEmployeeInformation.JoiningDate = popupJoiningDate.Text == string.Empty ? null : Convert.ToDateTime(popupJoiningDate.Text).ToString("dd-MMM-yyyy");
        _objEmployeeInformation.Status = ddlStatus.SelectedValue;
        _objEmployeeInformation.OfficeLocation = ddlOfficeLocation.SelectedValue;
        _objEmployeeInformation.WorkLocation = ddlWorkLocation.SelectedValue == "-1" ? "" : ddlWorkLocation.SelectedValue;
        _objEmployeeInformation.DepartmentCode = ddlDepartmentCode.SelectedValue;
        _objEmployeeInformation.SectionCode = ddlSectionCode.SelectedValue;
        _objEmployeeInformation.Designation = ddlDesignation.SelectedValue;
        _objEmployeeInformation.EmployeeType = ddlEmployeeType.SelectedValue;
        _objEmployeeInformation.CardId = txtIdCardID.Text;
        _objEmployeeInformation.CardExpiryDate = txtIdcardExpireDate.Text == string.Empty ? null : Convert.ToDateTime(txtIdcardExpireDate.Text).ToString("dd-MMM-yyyy");
        _objEmployeeInformation.FatherHusbandName = txtFatherName.Text;
        _objEmployeeInformation.MotherName = txtMotherName.Text;
        _objEmployeeInformation.SpouseName = txtSpouseName.Text;
        _objEmployeeInformation.Religion = ddlReligion.SelectedValue;
        _objEmployeeInformation.DateOfBirth = Convert.ToDateTime(popupDateOfBirth.Text).ToString("dd-MMM-yyyy");
        _objEmployeeInformation.MaritalStatus = ddlMaritalStatus.SelectedValue;
        _objEmployeeInformation.Gender = ddlGender.SelectedValue;
        _objEmployeeInformation.BloodGroup = ddlBloodGroup.SelectedValue;
        _objEmployeeInformation.HomePhone = txtHomePhone.Text;
        _objEmployeeInformation.Phone1 = txtpersonalcontactno1.Text;
        _objEmployeeInformation.Phone2 = txtpersonalcontactno2.Text;
        _objEmployeeInformation.TInNumber = txtTINNumber.Text;
        _objEmployeeInformation.PassportNumber = txtPassportNumber.Text;
        _objEmployeeInformation.EmergencyContactName = txtEmergencyContactPerson1.Text;
        _objEmployeeInformation.EmergencyNumber = txtEmergencyContactpersonnumber1.Text;
        _objEmployeeInformation.EmergencyContactpersonName2 = txtEmergencyContactPerson2.Text;
        _objEmployeeInformation.EmergencyContactpersonName2Number = txtEmergencyContactpersonnumber2.Text;
        _objEmployeeInformation.EmpShift = ddlShift.SelectedValue == "-1" ? "" : ddlShift.SelectedValue;

        //_objEmployeeInformation.EmployeeGrade = txtEmployeeGrade.Text;
        _objEmployeeInformation.PresentAddress = txtPresentAddress.Text;
        _objEmployeeInformation.DistrictP = ddlDistrictPre.SelectedValue;
        _objEmployeeInformation.DivisionP = ddlDivisionPre.SelectedValue;
        _objEmployeeInformation.PostalCodeP = txtPostalCodePre.Text;
        _objEmployeeInformation.CountryP = txtCountryPre.Text;
        _objEmployeeInformation.PermanentAddress = txtPermanentAddress.Text;
        _objEmployeeInformation.DistrictPr = ddlDistrictPer.SelectedValue;
        _objEmployeeInformation.DivisionPr = ddlDivisionPer.SelectedValue;
        _objEmployeeInformation.PostalCodePr = txtPostalCodePer.Text;
        _objEmployeeInformation.CountryPr = txtCountryPer.Text;
        _objEmployeeInformation.BankCode = ddlBankName.SelectedValue == "-1" ? "" : ddlBankName.SelectedValue;
        _objEmployeeInformation.BranchCode = ddlBranchName.SelectedValue == "-1" ? "" : ddlBranchName.SelectedValue;
        _objEmployeeInformation.BankAccountNo = txtBankAccountNo.Text == string.Empty ? "" : txtBankAccountNo.Text;
        _objEmployeeInformation.DesignationLevel = txtDesignationLevel.Text;
        _objEmployeeInformation.Email = txtEmailCompany.Text;
        _objEmployeeInformation.EmailPersonal = txtEmailPersonal.Text;
        _objEmployeeInformation.CompanyCode = ddlcompany.SelectedValue;
        _objEmployeeInformation.EmpPhoto = (byte[])ViewState["profileImage"];
        _objEmployeeInformation.EmpRefCompanyCode = ddlCompanyNameForRef.SelectedValue == "-1" ? "" : ddlCompanyNameForRef.SelectedValue;
        objForRefDepartment = new EmployeeInformationController();
        _objEmployeeInformation.EmpRefDeptCode = objForRefDepartment.GetDepartmentCodeOfEmployee(ConnectionString, txtEmployeeIDForRef.Text == string.Empty ? null : txtEmployeeIDForRef.Text);
        _objEmployeeInformation.EmpRefEmpId = txtEmployeeIDForRef.Text;
        _objEmployeeInformation.EmpRefName = txtReferenceName1.Text;
        _objEmployeeInformation.EmpRefContactNumber = txtContactNumberForRef1.Text;
        _objEmployeeInformation.EmpRefEmail = txtEmailForRef1.Text;
        _objEmployeeInformation.EmpRefNID = txtNIDForRef1.Text;
        _objEmployeeInformation.ContactPersonPresent = txtContactPersonPresent.Text;
        _objEmployeeInformation.ContactNumberPresent = txtContactNumberPresent.Text;
        _objEmployeeInformation.EmailPresent = txtEmailPresent.Text;
        _objEmployeeInformation.ContactPersonPermanent = txtContactPersonPermanent.Text;
        _objEmployeeInformation.ContactNumberPermanent = txtContactNumberPermanent.Text;
        _objEmployeeInformation.EmailPermanent = txtEmailPermanent.Text;
        _objEmployeeInformation.EmployeeNID = txtNID.Text;
        _objEmployeeInformation.RowNumberForUpdate = txtForUpdateTrans_Det.Text == string.Empty ? 0 : Convert.ToInt32(txtForUpdateTrans_Det.Text);
        _objEmployeeInformation.ProbationPeriod = txtProbationPeriod.Text == string.Empty ? 0 : Convert.ToInt32(txtProbationPeriod.Text);


        _objEmployeeInformation.Emp_Ref_Organization = txtOrganizationRf1.Text;
        _objEmployeeInformation.Emp_Ref_Designation = txtDesignationRef1.Text;

        _objEmployeeInformation.Emp_Ref2_Name = txtReferenceName2.Text;
        _objEmployeeInformation.Emp_Ref2_Organization = txtOrganizationRef2.Text;

        _objEmployeeInformation.Emp_Ref2_Designation = txtDesignationRef2.Text;
        _objEmployeeInformation.Emp_Ref2_ContactNumber = txtContactNumberForRef2.Text;
        _objEmployeeInformation.Emp_Ref2_Email = txtEmailForRef2.Text;
        _objEmployeeInformation.Emp_Ref2_NID = txtNIDForRef2.Text;
        _objEmployeeInformation.DrivingLicense = txtDrivingLicense.Text;

        SaveAcdemicRecord();
        SaveProfessionalRecord();
        SaveDependentsRecord();
        SaveAssetRecord();
        SaveLeaveAllocation();
        SaveTrainingRecord();

        return _msg = Save(Session[GlobalData.sessionConnectionstring].ToString(), _objEmployeeInformation);
    }

    public string Save(string connectionString, EmployeeInformation objEmployeeInformation)
    {
        string _msg;
        SqlConnection myConnection = new SqlConnection(connectionString);
        myConnection.Open();
        SqlTransaction transaction = myConnection.BeginTransaction();
        try
        {
            new SqlCommand("exec [EmpBasicInformationRemove] " +
                             "'" + objEmployeeInformation.CompanyCode + "'," +
                            "'" + objEmployeeInformation.EmployeeCode + "';", myConnection, transaction)
                            .ExecuteNonQuery();
            new SqlCommand("exec [EmpBasicInformationInitiateIntoHrMs_Emp_mas] " +
                           "'" + objEmployeeInformation.EmployeeCode + "'," +
                           "'" + objEmployeeInformation.EmployeeFirstName + "'," +
                           "'" + objEmployeeInformation.EmployeeLastName + "'," +
                           "'" + objEmployeeInformation.FatherHusbandName + "'," +
                           "'" + objEmployeeInformation.MotherName + "'," +
                           "'" + objEmployeeInformation.SpouseName + "'," +
                           "'" + objEmployeeInformation.Religion + "'," +
                           "'" + objEmployeeInformation.DateOfBirth + "'," +
                           "'" + objEmployeeInformation.MaritalStatus + "'," +
                           "'" + objEmployeeInformation.Gender + "'," +
                           "'" + objEmployeeInformation.BloodGroup + "'," +
                           "'" + objEmployeeInformation.HomePhone + "'," +
                           "'" + objEmployeeInformation.Phone1 + "'," +
                           "'" + objEmployeeInformation.Phone2 + "'," +
                           "'" + objEmployeeInformation.Status + "'," +
                           "'" + objEmployeeInformation.JoiningDate + "'," +
                           "'" + objEmployeeInformation.TInNumber + "'," +
                           "'" + objEmployeeInformation.PassportNumber + "'," +
                           "'" + objEmployeeInformation.EmergencyContactName + "'," +
                           "'" + objEmployeeInformation.EmergencyNumber + "'," +
                           "'" + objEmployeeInformation.EmployeeType + "'," +
                           "'" + objEmployeeInformation.DepartmentCode + "'," +
                           "'" + objEmployeeInformation.Email + "'," +
                           "'" + objEmployeeInformation.CardExpiryDate + "'," +
                           "" + objEmployeeInformation.ProbationPeriod + "," +
                           "'" + objEmployeeInformation.CardId + "'," +
                           "'" + objEmployeeInformation.EmergencyContactpersonName2 + "'," +
                           "'" + objEmployeeInformation.EmergencyContactpersonName2Number.ToString() + "'," +
                           "'" + objEmployeeInformation.EmailPersonal + "'," +
                           "'" + objEmployeeInformation.EmpShift + "'," +
                           "'" + objEmployeeInformation.DrivingLicense + "';", myConnection, transaction).ExecuteNonQuery();


            new SqlCommand("exec [EmpBasicInformationInitiateIntoHRMS_Emp_Address] " +
                           "'" + objEmployeeInformation.EmployeeCode + "'," +
                           "'" + objEmployeeInformation.PresentAddress + "'," +
                           "'" + objEmployeeInformation.DivisionP + "'," +
                           "'" + objEmployeeInformation.DistrictP + "'," +
                           "'" + objEmployeeInformation.PostalCodeP + "'," +
                           "'" + objEmployeeInformation.CountryP + "'," +
                           "'" + objEmployeeInformation.ContactPersonPresent + "'," +
                           "'" + objEmployeeInformation.ContactNumberPresent + "'," +
                           "'" + objEmployeeInformation.EmailPresent + "'," +
                           "'" + objEmployeeInformation.PermanentAddress + "'," +
                           "'" + objEmployeeInformation.DivisionPr + "'," +
                           "'" + objEmployeeInformation.DistrictPr + "'," +
                           "'" + objEmployeeInformation.PostalCodePr + "'," +
                           "'" + objEmployeeInformation.CountryPr + "'," +
                           "'" + objEmployeeInformation.ContactPersonPermanent + "'," +
                           "'" + objEmployeeInformation.ContactNumberPermanent + "'," +
                           "'" + objEmployeeInformation.EmailPermanent + "'," +
                           "'" + objEmployeeInformation.EmployeeNID + "';", myConnection, transaction).ExecuteNonQuery();
            new SqlCommand("exec [EmpBasicInformationInitiateIntoHrms_Trans_Det] " +
                           "'" + objEmployeeInformation.EmployeeCode + "'," +
                           "'" + objEmployeeInformation.OfficeLocation + "'," +
                           "'" + objEmployeeInformation.DepartmentCode + "'," +
                           "'" + objEmployeeInformation.SectionCode + "'," +
                           "'" + objEmployeeInformation.Designation + "'," +
                           "'" + objEmployeeInformation.DesignationLevel + "'," +
                           "" + objEmployeeInformation.RowNumberForUpdate + "," +
                           "'" + objEmployeeInformation.JoiningDate + "'," +
                           "'" + objEmployeeInformation.EmployeeType + "';", myConnection, transaction)
                .ExecuteNonQuery();
                     

            new SqlCommand("exec [EmpBasicInformationInitiateIntoHRMS_WORK_LOCATION] " +
                           "'" + objEmployeeInformation.EmployeeCode + "'," +
                           "'" + objEmployeeInformation.WorkLocation + "';", myConnection, transaction)
                .ExecuteNonQuery();
            //new SqlCommand("exec [EmpBasicInformationInitiateIntoHrms_Emp_Photo] " +
            //               "'" + objEmployeeInformation.CompanyCode + "'," +
            //               "'" + objEmployeeInformation.EmpPhoto + "'," +
            //               "'" + objEmployeeInformation.EmployeeCode + "';", myConnection, transaction)
            //    .ExecuteNonQuery();
            foreach (AcademicQualification academicRecord in objEmployeeInformation.AcademicQualifications)
            {
                new SqlCommand("exec [EmpBasicInformationInitiateIntoHrms_Emp_Academic_Info] " +
                               "'" + objEmployeeInformation.EmployeeCode + "'," +
                               "'" + academicRecord.NameOfDegree + "'," +
                               "'" + academicRecord.InstitutionName + "'," +
                               "'" + academicRecord.Board + "'," +
                               "'" + academicRecord.MajorInGroup + "'," +
                               "'" + academicRecord.ResultGrade + "'," +
                               "'" + academicRecord.DocumentCode + "'," +
                               academicRecord.PassingYear + "," +
                               academicRecord.CourseDuration + ";", myConnection, transaction).ExecuteNonQuery();
            }
            foreach (
                ProfessionalQualification professionalRecord in objEmployeeInformation.ProfessionalQualifications)
            {
                new SqlCommand("exec [EmpBasicInformationInitiateIntoHrms_Exp_Mas]" +
                               "'" + objEmployeeInformation.EmployeeCode + "'," +
                               "'" + professionalRecord.NameofInstitution + "'," +
                               "'" + professionalRecord.InstitutionAddress + "'," +
                               "'" + professionalRecord.Designation + "'," +
                               "'" + professionalRecord.ServiceStartDate + "'," +
                               "'" + professionalRecord.ServiceEndDate + "'," +
                               +professionalRecord.GrossSalary + "," +
                               "'" + professionalRecord.ServiceDescription + "'," +
                               "'" + professionalRecord.DocumentCode + "';", myConnection, transaction)
                    .ExecuteNonQuery();
            }
            foreach (
                TrainingRecord mappingTrainingRecord in objEmployeeInformation.TrainingRecords)
            {
                new SqlCommand("exec [EmpBasicInformationInitiateIntoHRMS_Training_Record]" +
                               "'" + objEmployeeInformation.EmployeeCode + "'," +
                               "'" + mappingTrainingRecord.NameOfInstitution + "'," +
                               "'" + mappingTrainingRecord.InstitutionAddress + "'," +
                               "'" + mappingTrainingRecord.StartedDate + "'," +
                               "'" + mappingTrainingRecord.EndDate + "'," +
                               "" + mappingTrainingRecord.TrainingDuration + "," +
                               "" + mappingTrainingRecord.TrainingFee + "," +
                               "'" + mappingTrainingRecord.TrainingCode + "'," +
                               "'" + mappingTrainingRecord.TrainingAchievement + "'," +
                               "'" + mappingTrainingRecord.TrainingTitle + "'," +
                               "'" + mappingTrainingRecord.CertificateCode + "'," +
                               "'" + mappingTrainingRecord.DocumentCode + "'," +
                               "'" + mappingTrainingRecord.FundCode + "';", myConnection, transaction)
                    .ExecuteNonQuery();
            }
            new SqlCommand("exec [EmpBasicInformationInitiateIntoHrms_Emp_Bnk_Info]" +
                           "'" + objEmployeeInformation.EmployeeCode + "'," +
                           "'" + objEmployeeInformation.BankCode + "'," +
                           "'" + objEmployeeInformation.BranchCode + "'," +
                           "'" + objEmployeeInformation.BankAccountNo + "';", myConnection, transaction)
                .ExecuteNonQuery();


            foreach (DependentsInformation dependentsRecord in objEmployeeInformation.DependentsInformations)
            {
                new SqlCommand("exec [EmpBasicInformationInitiateIntoHrms_Dpdnt_Mas]" +
                               "'" + objEmployeeInformation.EmployeeCode + "'," +
                               "'" + dependentsRecord.Name + "'," +
                               "'" + dependentsRecord.Gender + "'," +
                               "'" + dependentsRecord.DateOfBirth + "'," +
                               "'" + dependentsRecord.RelationshipWithEmployee + "';", myConnection, transaction)
                    .ExecuteNonQuery();
            }
            foreach (AssetAllocation assetRecord in objEmployeeInformation.AssetAllocations)
            {
                new SqlCommand("exec [EmpBasicInformationInitiateIntoHrms_Emp_Asset_Info]" +
                               "'" + objEmployeeInformation.EmployeeCode + "'," +
                               "'" + assetRecord.AssetName + "'," +
                               "'" + assetRecord.AssetIdNo + "'," +
                               "'" + assetRecord.ActiveDate + "'," +
                               "'" + assetRecord.InactiveDate + "'," +
                               "'" + assetRecord.AssetModelNo + "'," +
                               "'" + assetRecord.Description + "'," +
                               "'" + assetRecord.ReportingPersonId + "'," +
                               "'" + assetRecord.Status + "'," +
                               "'" + assetRecord.DocumentCode + "';", myConnection, transaction).ExecuteNonQuery();
            }
            foreach (LeaveAllocation leaveRecord in objEmployeeInformation.LeaveAllocations)
            {
                new SqlCommand("exec [EmpBasicInformationInitiateIntoHrms_Emp_Leave_Info]" +
                               "'" + objEmployeeInformation.EmployeeCode + "'," +
                               "'" + leaveRecord.LeaveType + "';", myConnection, transaction).ExecuteNonQuery();
            }
            new SqlCommand("exec [EmpBasicInformationInitiateIntoHRMS_Emp_Reference] " +
                           "'" + objEmployeeInformation.EmployeeCode + "'," +
                           "'" + objEmployeeInformation.EmpRefDeptCode + "'," +
                           "'" + objEmployeeInformation.EmpRefCompanyCode + "'," +
                           "'" + objEmployeeInformation.EmpRefEmpId + "'," +
                           "'" + objEmployeeInformation.EmpRefName + "'," +
                           "'" + objEmployeeInformation.EmpRefContactNumber + "'," +
                           "'" + objEmployeeInformation.EmpRefEmail + "'," +
                           "'" + objEmployeeInformation.EmpRefNID + "'," +
                           "'" + objEmployeeInformation.Emp_Ref_Organization + "'," +

                           "'" + objEmployeeInformation.Emp_Ref_Designation + "'," +
                           "'" + objEmployeeInformation.Emp_Ref2_Name + "'," +
                           "'" + objEmployeeInformation.Emp_Ref2_Organization + "'," +
                           "'" + objEmployeeInformation.Emp_Ref2_Designation + "'," +
                           "'" + objEmployeeInformation.Emp_Ref2_ContactNumber + "'," +
                           "'" + objEmployeeInformation.Emp_Ref2_Email + "'," +
                          "'" + objEmployeeInformation.Emp_Ref2_NID + "' ;", myConnection, transaction)
                .ExecuteNonQuery();
            AttachFileSave(_objEmployeeInformation);
            transaction.Commit();
            _msg = "Data Saved Successfully ";
            if (ViewState["profileImage"] != null)
            {
                _msg = SaveProfileImage(txtEmployeeCode.Text);
            }
            ClearAllControl();
        }
        catch (SqlException sqlError)
        {

            transaction.Rollback();
            _msg = "Error Occured During Operation into Database, Data did not Save into Database !";
        }
        catch (Exception inSystemExep)
        {
            transaction.Rollback();
            _msg = "System Error, Data did not Save into Database !";
        }
        myConnection.Close();
        return _msg;
    }
    private void AttachFileSave(EmployeeInformation objEmployeeInformation)
    {
        HttpFileCollection hfc = Request.Files;
        _objDocumentUploadController = new DocumentUploadController();
        string referenceno = _objDocumentUploadController.GetReferenceNo(ConnectionString);
        for (int i = 0; i < hfc.Count; i++)
        {
            HttpPostedFile hpf = hfc[i];
            if (hpf.ContentLength > 0)
            {
                string fileName = System.IO.Path.GetFileName(hpf.FileName).Replace("'", "''");
                string filenameReference = referenceno + "-" + fileName;
                hpf.SaveAs(filepath + "\\" + filenameReference);
                Upload(fileName, referenceno, filenameReference);
                objForRefDepartment = new EmployeeInformationController();
                objEmployeeInformation.EmployeeCode = txtEmployeeCode.Text;
                objEmployeeInformation.DocumentCode = filenameReference;
                objEmployeeInformation.ActivityCode = "1";
                objForRefDepartment.SaveOthersDocument(ConnectionString, objEmployeeInformation);
            }
        }
    }

    private string AttachFileSaveAccordingToCatagory(FileUpload idFileUpload)
    {
        //Need only Extension Here
        _objDocumentUploadController = new DocumentUploadController();
        string referenceno = _objDocumentUploadController.GetReferenceNo(ConnectionString);
        string filenameReference = null;
        if (idFileUpload.HasFile)
        {
            string fileName = System.IO.Path.GetFileName(idFileUpload.FileName).Replace("'", "''");
            filenameReference = referenceno + "-" + fileName;
            idFileUpload.SaveAs(filepath + "\\" + filenameReference);
            Upload(fileName, referenceno, filenameReference);
        }
        return filenameReference;
    }


    private void Upload(string fileName, string referenceNo, string filenemeReference)
    {
        try
        {
            _objDocumentUploadController = new DocumentUploadController();
            _objDocumentUpload = new DocumentUpload();
            _objDocumentUpload.DocumentTypeCode = "3";
            _objDocumentUpload.Description = null;
            _objDocumentUpload.DocumentContent = null;
            _objDocumentUpload.EntryUser = current.UserId;
            _objDocumentUpload.DocumentContent = fileName;
            _objDocumentUpload.documentCode = referenceNo;
            _objDocumentUpload.documentName = filenemeReference;
            _objDocumentUploadController.DocumentUpload(ConnectionString, _objDocumentUpload);
        }
        catch (Exception msgException)
        {
            MessageBox1.ShowError(msgException.Message);
        }
    }
    private void SaveAcdemicRecord()
    {
        var academicQualificationsList = (from GridViewRow rowValue in grdAcademicRecords.Rows
                                          select new AcademicQualification
                                          {
                                              NameOfDegree = rowValue.Cells[1].Text,
                                              InstitutionName = rowValue.Cells[2].Text,
                                              Board = rowValue.Cells[3].Text,
                                              MajorInGroup = rowValue.Cells[4].Text == "&nbsp;" ? "" : rowValue.Cells[4].Text,
                                              ResultGrade = rowValue.Cells[5].Text,
                                              PassingYear = Convert.ToDouble(rowValue.Cells[6].Text),
                                              CourseDuration = Convert.ToDouble(rowValue.Cells[7].Text),
                                              DocumentCode = rowValue.Cells[8].Text == "&nbsp;" ? "" : rowValue.Cells[8].Text
                                          }).ToList();
        _objEmployeeInformation.AcademicQualifications = academicQualificationsList;
    }

    private void SaveProfessionalRecord()
    {
        var professionalQualificationList = (from GridViewRow rowValue in grdProfessionalQualification.Rows
                                             select new ProfessionalQualification
                                             {
                                                 NameofInstitution = rowValue.Cells[1].Text,
                                                 InstitutionAddress = rowValue.Cells[2].Text == "&nbsp;" ? "" : rowValue.Cells[2].Text,
                                                 ServiceStartDate = Convert.ToDateTime(rowValue.Cells[3].Text).ToString(),
                                                 ServiceEndDate = Convert.ToDateTime(rowValue.Cells[4].Text).ToString(),
                                                 Designation = rowValue.Cells[5].Text == "&nbsp;" ? "" : rowValue.Cells[5].Text,
                                                 ServiceDescription = rowValue.Cells[6].Text == "&nbsp;" ? "" : rowValue.Cells[6].Text,
                                                 GrossSalary = Convert.ToDouble(rowValue.Cells[7].Text),
                                                 DocumentCode = rowValue.Cells[8].Text == "&nbsp;" ? "" : rowValue.Cells[8].Text
                                             }).ToList();
        _objEmployeeInformation.ProfessionalQualifications = professionalQualificationList;
    }

    private void SaveTrainingRecord()
    {
        var trainingRecordList = (from GridViewRow rowValue in grdTrainingRecord.Rows
                                  select new TrainingRecord
                                  {
                                      TrainingTitle = rowValue.Cells[2].Text == "&nbsp;" ? "" : rowValue.Cells[2].Text,
                                      TrainingAchievement = rowValue.Cells[3].Text == "&nbsp;" ? "" : rowValue.Cells[3].Text,
                                      NameOfInstitution = rowValue.Cells[4].Text == "&nbsp;" ? "" : rowValue.Cells[4].Text,
                                      InstitutionAddress = rowValue.Cells[5].Text == "&nbsp;" ? "" : rowValue.Cells[5].Text,
                                      StartedDate = Convert.ToDateTime(rowValue.Cells[6].Text).ToString(),
                                      EndDate = Convert.ToDateTime(rowValue.Cells[7].Text).ToString(),
                                      TrainingDuration = Convert.ToInt32(rowValue.Cells[8].Text),
                                      TrainingFee = Convert.ToDouble(rowValue.Cells[9].Text),
                                      TrainingCode = rowValue.Cells[12].Text == "&nbsp;" ? "" : rowValue.Cells[12].Text,
                                      CertificateCode = rowValue.Cells[10].Text == "&nbsp;" ? "" : rowValue.Cells[10].Text,
                                      FundCode = rowValue.Cells[11].Text == "&nbsp;" ? "" : rowValue.Cells[11].Text,
                                      DocumentCode = rowValue.Cells[15].Text == "&nbsp;" ? "" : rowValue.Cells[15].Text
                                  }).ToList();
        _objEmployeeInformation.TrainingRecords = trainingRecordList;
    }

    private void SaveDependentsRecord()
    {
        var dependentsInformationList = (from GridViewRow rowValue in grdDependentsInformation.Rows
                                         select new DependentsInformation
                                         {
                                             Name = rowValue.Cells[1].Text,
                                             DateOfBirth = Convert.ToDateTime(rowValue.Cells[3].Text).ToString(),
                                             RelationshipWithEmployee = rowValue.Cells[4].Text == "&nbsp;" ? "" : rowValue.Cells[4].Text,
                                             Gender = rowValue.Cells[5].Text
                                         }).ToList();
        _objEmployeeInformation.DependentsInformations = dependentsInformationList;
    }

    private void SaveAssetRecord()
    {
        var assetAllocationList = (from GridViewRow rowValue in grdAssetAllocation.Rows
                                   select new AssetAllocation
                                   {
                                       AssetName = rowValue.Cells[1].Text,
                                       AssetIdNo = rowValue.Cells[2].Text == "&nbsp;" ? "" : rowValue.Cells[2].Text,
                                       AssetModelNo = rowValue.Cells[3].Text == "&nbsp;" ? "" : rowValue.Cells[3].Text,
                                       Description = rowValue.Cells[4].Text == "&nbsp;" ? "" : rowValue.Cells[4].Text,
                                       ReportingPersonId = rowValue.Cells[5].Text == "&nbsp;" ? "" : rowValue.Cells[5].Text,
                                       ActiveDate = (rowValue.Cells[6].Text),
                                       InactiveDate = (rowValue.Cells[7].Text == "&nbsp;" ? null : rowValue.Cells[7].Text),
                                       Status = rowValue.Cells[9].Text,
                                       DocumentCode = (rowValue.Cells[10].Text == "&nbsp;" ? null : rowValue.Cells[10].Text)
                                   }).ToList();
        _objEmployeeInformation.AssetAllocations = assetAllocationList;
    }

    private void SaveLeaveAllocation()
    {
        var leaveAllocationList = (from ListItem leaveTypeValue in lstForLeaveAllocation.Items select new LeaveAllocation { LeaveType = leaveTypeValue.Value }).ToList();
        _objEmployeeInformation.LeaveAllocations = leaveAllocationList;
    }

    private string CheckAllValidation()
    {
        const string checkValidation = "";
        if (ddlcompany.SelectedValue == "")
        {
            ddlcompany.Focus();
            return "Please Select Company !";
        }
        if (txtEmployeeCode.Text == string.Empty)
        {
            txtEmployeeCode.Focus();
            return "Must Enter Employee Code !";
        }
        if (txtEmployeeFirstName.Text == string.Empty)
        {
            txtEmployeeFirstName.Focus();
            return "Must Enter Employee Name !";
        }
        if (popupJoiningDate.Text == string.Empty)
        {
            popupJoiningDate.Focus();
            return "Must Select Joining Date !";
        }
        if (ddlStatus.SelectedValue == "-1")
        {
            ddlStatus.Focus();
            return "Please Select Employee Status Correctly !";
        }
        if (ddlOfficeLocation.SelectedValue == "-1")
        {
            ddlOfficeLocation.Focus();
            return "Please Select Office Location Correctly !";
        }
        if (ddlWorkLocation.SelectedValue == "-1")
        {
            ddlWorkLocation.Focus();
            return "Please Select Work Location Correctly !";
        }
        if (ddlDepartmentCode.SelectedValue == "-1")
        {
            ddlDepartmentCode.Focus();
            return "Please Select Depeartment Correctly !";
        }
        if (ddlSectionCode.SelectedValue == "-1")
        {
            ddlSectionCode.Focus();
            return "Please Select Section Code Correctly !";
        }
        if (ddlDesignation.SelectedValue == "-1")
        {
            ddlDesignation.Focus();
            return "Please Select Designation Correctly !";
        }
        if (ddlEmployeeType.SelectedValue == "-1")
        {
            ddlEmployeeType.Focus();
            return "Please Select Employee Type Correctly !";
        }
        if (ddlReligion.SelectedValue == "-1")
        {
            ddlReligion.Focus();
            return "Please Select Religion Correctly !";
        }
        if (popupDateOfBirth.Text == string.Empty)
        {
            popupDateOfBirth.Focus();
            return "Must Select Date Of Birth !";
        }
        if (ddlMaritalStatus.SelectedValue == "-1")
        {
            ddlMaritalStatus.Focus();
            return "Please Select Marital Status Correctly !";
        }
        if (ddlGender.SelectedValue == "-1")
        {
            ddlGender.Focus();
            return "Please Select Gender Correctly !";
        }
        if (ddlBloodGroup.SelectedValue == "-1")
        {
            ddlBloodGroup.Focus();
            return "Please Select Blood Group Correctly !";
        }

        if (ddlBankName.SelectedValue != "-1")
        {
            if (ddlBranchName.SelectedValue == "-1")
            {
                ddlBranchName.Focus();
                return "Please Select Branch Name Correctly !";
            }
        }
        if (ddlShift.SelectedValue == "-1")
        {
            ddlShift.Focus();
            return "Please Select Employee Shift";
        }

        return checkValidation;
    }

    private string CheckAllValidationForShow()
    {
        const string checkValidation = "";
        if (ddlcompany.SelectedValue == "")
        {
            ddlcompany.Focus();
            return "Please Select Company !";
        }
        if (txtEmployeeSearch.Text == string.Empty)
        {
            txtEmployeeSearch.Focus();
            return "Must Enter Employee Code !";
        }
        return checkValidation;
    }

    private void AddOrUpdateEmployeeInformation()
    {
        string validationMsg = CheckAllValidation();
        switch (validationMsg)
        {
            case "":
                {
                    string msg = SaveAllInformation();
                    MessageBox1.ShowSuccess(msg);
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }
    }

    protected void btnSaveAllInformation_Click(object sender, EventArgs e)
    {
        AddOrUpdateEmployeeInformation();
    }

    public string LoadAllDataOfAParticularEmployee(string connectionString, string employeeCode)
    {
        var dt = new DataTable();
        var myConnection = new SqlConnection(connectionString);
        var myCommand = myConnection.CreateCommand();
        myConnection.Open();
        try
        {
            //myCommand.CommandText = "exec [EmpBasicInformationGetFromhrms_emp_grd_det] '" + employeeCode + "'";
            //myCommand.ExecuteNonQuery();
            //var daGrade = new SqlDataAdapter(myCommand);
            //daGrade.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{
            //    txtEmployeeGrade.Text = dt.Rows[0].ItemArray[0].ToString();
            //}
            var dtLeave = new DataTable();
            myCommand.CommandText = "exec [EmpBasicInformationGetFromHrms_Emp_Leave_Info] '" + employeeCode + "'";
            myCommand.ExecuteNonQuery();
            var daLeave = new SqlDataAdapter(myCommand);
            daLeave.Fill(dtLeave);
            lstForLeaveAllocation.Items.Clear();
            foreach (DataRow row in dtLeave.Rows)
            {
                lstForLeaveAllocation.Items.Add(new ListItem(row.ItemArray[1].ToString(), row.ItemArray[0].ToString()));
                lstForLeaveAllocation.AppendDataBoundItems = true;
            }
            var dtAsset = new DataTable();
            myCommand.CommandText = "exec [EmpBasicInformationGetFromHrms_Emp_Asset_Info] '" + employeeCode + "'";
            myCommand.ExecuteNonQuery();
            var daAsset = new SqlDataAdapter(myCommand);
            daAsset.Fill(dtAsset);
            ViewState["AssetAllocation"] = null;
            grdAssetAllocation.DataSource = null;
            grdAssetAllocation.DataBind();
            foreach (DataRow row in dtAsset.Rows)
            {
                BindAssetInformationGrid(
                    row.ItemArray[0].ToString(),
                    row.ItemArray[1].ToString(),
                    row.ItemArray[2].ToString(),
                    row.ItemArray[3].ToString(),
                    row.ItemArray[5].ToString(),
                    row.ItemArray[4].ToString(),
                    row.ItemArray[6].ToString(),
                    row.ItemArray[7].ToString(),
                    row.ItemArray[8].ToString(),
                    row.ItemArray[9].ToString()
                    );
            }
            var dtDependent = new DataTable();
            myCommand.CommandText = "exec [EmpBasicInformationGetFromHrms_Dpdnt_Mas] '" + employeeCode + "'";
            myCommand.ExecuteNonQuery();
            var daDependent = new SqlDataAdapter(myCommand);
            daDependent.Fill(dtDependent);
            ViewState["DependentsInformation"] = null;
            grdDependentsInformation.DataSource = null;
            grdDependentsInformation.DataBind();
            foreach (DataRow row in dtDependent.Rows)
            {
                BindDependentsInformationGrid(
                    row.ItemArray[0].ToString(),
                    row.ItemArray[4].ToString(),
                    row.ItemArray[1].ToString(),
                    row.ItemArray[2].ToString(),
                    row.ItemArray[3].ToString()
                    );
            }
            var dtBankInfo = new DataTable();
            myCommand.CommandText = "exec [EmpBasicInformationGetFromHrms_Emp_Bnk_Info] '" + employeeCode + "'";
            myCommand.ExecuteNonQuery();
            var daBandInfo = new SqlDataAdapter(myCommand);
            daBandInfo.Fill(dtBankInfo);
            if (dtBankInfo.Rows.Count > 0)
            {
                ddlBankName.SelectedValue = dtBankInfo.Rows[0].ItemArray[0].ToString() == "" ? "-1" : dtBankInfo.Rows[0].ItemArray[0].ToString();
                if (dtBankInfo.Rows[0].ItemArray[0].ToString() == "" && dtBankInfo.Rows[0].ItemArray[1].ToString() == "")
                {
                    ddlBranchName.Items.Clear();
                }
                else
                {
                    LoadBranchName(dtBankInfo.Rows[0].ItemArray[0].ToString());
                    if (ddlBranchName.Items.Count > 0)
                    {
                        ddlBranchName.SelectedValue = dtBankInfo.Rows[0].ItemArray[1].ToString() == "" ? "-1" : dtBankInfo.Rows[0].ItemArray[1].ToString();
                    }
                }
                txtBankAccountNo.Text = dtBankInfo.Rows[0].ItemArray[2].ToString();
            }
            var dtProfessional = new DataTable();
            myCommand.CommandText = "exec [EmpBasicInformationGetFromHrms_Exp_Mas] '" + employeeCode + "'";
            myCommand.ExecuteNonQuery();
            var daProfessional = new SqlDataAdapter(myCommand);
            daProfessional.Fill(dtProfessional);
            ViewState["ProfessionalQualification"] = null;
            grdProfessionalQualification.DataSource = null;
            grdProfessionalQualification.DataBind();
            foreach (DataRow row in dtProfessional.Rows)
            {
                BindProfessionalRecordGrid(
                    row.ItemArray[0].ToString(),
                    row.ItemArray[1].ToString(),
                    row.ItemArray[3].ToString(),
                    row.ItemArray[4].ToString(),
                    row.ItemArray[2].ToString(),
                    row.ItemArray[6].ToString(),
                    Convert.ToDouble(row.ItemArray[5].ToString()),
                    row.ItemArray[7].ToString()
                    );
            }
            var dtTraining = new DataTable();
            myCommand.CommandText = "exec [EmpBasicInformationGetFromHRMS_Training_Record] '" + employeeCode + "'";
            myCommand.ExecuteNonQuery();
            var daTraining = new SqlDataAdapter(myCommand);
            daTraining.Fill(dtTraining);
            ViewState["TrainingRecord"] = null;
            grdTrainingRecord.DataSource = null;
            grdTrainingRecord.DataBind();
            foreach (DataRow row in dtTraining.Rows)
            {
                BindTrainingRecordGrid(
                    row.ItemArray[0].ToString(),
                    row.ItemArray[1].ToString(),
                    row.ItemArray[2].ToString(),
                    row.ItemArray[3].ToString(),
                    row.ItemArray[4].ToString(),
                    row.ItemArray[5].ToString(),
                    Convert.ToInt32(row.ItemArray[6].ToString()),
                    Convert.ToDouble(row.ItemArray[7].ToString()),
                    row.ItemArray[8].ToString(),
                    row.ItemArray[9].ToString(),
                    row.ItemArray[10].ToString(),
                    row.ItemArray[11].ToString(),
                    row.ItemArray[12].ToString(),
                    row.ItemArray[13].ToString(),
                    row.ItemArray[14].ToString()
                    );
            }
            var dtAcademic = new DataTable();
            myCommand.CommandText = "exec [EmpBasicInformationGetFromHrms_Emp_Academic_Info] '" + employeeCode + "'";
            myCommand.ExecuteNonQuery();
            var daAcademic = new SqlDataAdapter(myCommand);
            daAcademic.Fill(dtAcademic);
            ViewState["AcademicQualification"] = null;
            grdAcademicRecords.DataSource = null;
            grdAcademicRecords.DataBind();
            foreach (DataRow row in dtAcademic.Rows)
            {
                BindAcademicQualificationGrid(
                    row.ItemArray[0].ToString(),
                    row.ItemArray[1].ToString(),
                    row.ItemArray[2].ToString(),
                    row.ItemArray[3].ToString(),
                    Convert.ToDouble(row.ItemArray[4].ToString()),
                    Convert.ToDouble(row.ItemArray[5].ToString()),
                    row.ItemArray[6].ToString(),
                    row.ItemArray[7].ToString()
                    );
            }
            var dtBasicInfo = new DataTable();
            myCommand.CommandText = "exec [EmpBasicInformationGetFromHrMs_Emp_mas] '" + employeeCode + "'";
            myCommand.ExecuteNonQuery();
            var daBasicInfo = new SqlDataAdapter(myCommand);
            daBasicInfo.Fill(dtBasicInfo);
            if (dtBasicInfo.Rows.Count == 0)
            {
                return _msg = "No data found in database for the specified employee. Please check employee id number !";
            }
            else
            {
                txtEmployeeFirstName.Text = dtBasicInfo.Rows[0].ItemArray[0].ToString();
                txtEmployeeLastName.Text = dtBasicInfo.Rows[0].ItemArray[1].ToString();
                txtFatherName.Text = dtBasicInfo.Rows[0].ItemArray[2].ToString();
                txtMotherName.Text = dtBasicInfo.Rows[0].ItemArray[3].ToString();
                txtPresentAddress.Text = dtBasicInfo.Rows[0].ItemArray[4].ToString();
                var DivisionPre = dtBasicInfo.Rows[0].ItemArray[5].ToString() == string.Empty ? "-1" : dtBasicInfo.Rows[0].ItemArray[5].ToString();
                ddlDivisionPre.SelectedValue = DivisionPre;
                if (DivisionPre != "-1")
                {
                    LoadDistrict(ddlDistrictPre, ddlDivisionPre.SelectedValue);
                    ddlDistrictPre.SelectedValue = dtBasicInfo.Rows[0].ItemArray[6].ToString() == string.Empty ? "-1" : dtBasicInfo.Rows[0].ItemArray[6].ToString();
                }
                else if (DivisionPre == "-1")
                {
                    ddlDistrictPre.Items.Clear();
                }
                txtPostalCodePre.Text = dtBasicInfo.Rows[0].ItemArray[7].ToString();
                txtCountryPre.Text = dtBasicInfo.Rows[0].ItemArray[8].ToString();
                txtPermanentAddress.Text = dtBasicInfo.Rows[0].ItemArray[9].ToString();
                var DivisionPer = dtBasicInfo.Rows[0].ItemArray[10].ToString() == string.Empty ? "-1" : dtBasicInfo.Rows[0].ItemArray[10].ToString();
                ddlDivisionPer.SelectedValue = DivisionPer;
                if (DivisionPer != "-1")
                {
                    LoadDistrict(ddlDistrictPer, ddlDivisionPer.SelectedValue);
                    ddlDistrictPer.SelectedValue = dtBasicInfo.Rows[0].ItemArray[11].ToString() == string.Empty ? "-1" : dtBasicInfo.Rows[0].ItemArray[11].ToString();
                }
                else if (DivisionPer == "-1")
                {
                    ddlDistrictPer.Items.Clear();
                }
                txtPostalCodePer.Text = dtBasicInfo.Rows[0].ItemArray[12].ToString();
                txtCountryPer.Text = dtBasicInfo.Rows[0].ItemArray[13].ToString();
                ddlReligion.SelectedValue = dtBasicInfo.Rows[0].ItemArray[14].ToString();
                popupDateOfBirth.Text = Convert.ToDateTime(dtBasicInfo.Rows[0].ItemArray[15].ToString()).ToShortDateString();
                ddlMaritalStatus.SelectedValue = dtBasicInfo.Rows[0].ItemArray[16].ToString() == string.Empty ? "-1" : dtBasicInfo.Rows[0].ItemArray[16].ToString();
                ddlGender.SelectedValue = dtBasicInfo.Rows[0].ItemArray[17].ToString();
                ddlBloodGroup.SelectedValue = dtBasicInfo.Rows[0].ItemArray[18].ToString();
                txtHomePhone.Text = dtBasicInfo.Rows[0].ItemArray[19].ToString();
                txtpersonalcontactno1.Text = dtBasicInfo.Rows[0].ItemArray[20].ToString();
                txtpersonalcontactno2.Text = dtBasicInfo.Rows[0].ItemArray[21].ToString();
                ddlStatus.SelectedValue = dtBasicInfo.Rows[0].ItemArray[22].ToString();
                popupJoiningDate.Text = dtBasicInfo.Rows[0].ItemArray[23].ToString() == null ? string.Empty : Convert.ToDateTime(dtBasicInfo.Rows[0].ItemArray[23].ToString()).ToShortDateString();
                txtTINNumber.Text = dtBasicInfo.Rows[0].ItemArray[24].ToString();
                txtPassportNumber.Text = dtBasicInfo.Rows[0].ItemArray[25].ToString();
                txtEmergencyContactPerson1.Text = dtBasicInfo.Rows[0].ItemArray[26].ToString();
                txtEmergencyContactpersonnumber1.Text = dtBasicInfo.Rows[0].ItemArray[27].ToString();
                ddlEmployeeType.SelectedValue = dtBasicInfo.Rows[0].ItemArray[28].ToString();
                //ddlDepartmentCode.SelectedValue = dtBasicInfo.Rows[0].ItemArray[29].ToString();
                txtIdCardID.Text = dtBasicInfo.Rows[0].ItemArray[30].ToString();
                txtEmailCompany.Text = dtBasicInfo.Rows[0].ItemArray[31].ToString();
                txtIdcardExpireDate.Text = dtBasicInfo.Rows[0].ItemArray[32].ToString() == "01 Jan 1900" ? string.Empty : Convert.ToDateTime(dtBasicInfo.Rows[0].ItemArray[32].ToString()).ToShortDateString();
                txtContactPersonPresent.Text = dtBasicInfo.Rows[0].ItemArray[33].ToString();
                txtContactNumberPresent.Text = dtBasicInfo.Rows[0].ItemArray[34].ToString();
                txtEmailPresent.Text = dtBasicInfo.Rows[0].ItemArray[35].ToString();
                txtContactPersonPermanent.Text = dtBasicInfo.Rows[0].ItemArray[36].ToString();
                txtContactNumberPermanent.Text = dtBasicInfo.Rows[0].ItemArray[37].ToString();
                txtEmailPermanent.Text = dtBasicInfo.Rows[0].ItemArray[38].ToString();
                txtNID.Text = dtBasicInfo.Rows[0].ItemArray[39].ToString();
                txtProbationPeriod.Text = dtBasicInfo.Rows[0].ItemArray[40].ToString();
                lblStatus.Text = ddlStatus.SelectedItem.Text;
                lblStatus.Visible = true;
                lblStatusShow.Visible = true;
                txtEmployeeCode.Text = employeeCode;
                txtSpouseName.Text = dtBasicInfo.Rows[0].ItemArray[45].ToString();
                txtEmergencyContactPerson2.Text = dtBasicInfo.Rows[0].ItemArray[46].ToString();
                txtEmergencyContactpersonnumber2.Text = dtBasicInfo.Rows[0].ItemArray[47].ToString();
                txtEmailPersonal.Text = dtBasicInfo.Rows[0].ItemArray[48].ToString();
                var shiftCode = dtBasicInfo.Rows[0].ItemArray[49].ToString() == string.Empty ? "-1" : dtBasicInfo.Rows[0].ItemArray[49].ToString();
                ddlShift.SelectedValue = shiftCode;
                txtDrivingLicense.Text = dtBasicInfo.Rows[0].ItemArray[50].ToString();

            }
            var dtBasicInfoFromTrans = new DataTable();
            myCommand.CommandText = "exec [EmpBasicInformationGetFromHrms_Trans_Det] '" + employeeCode + "'";
            myCommand.ExecuteNonQuery();
            var daBasicInfoFromTrans = new SqlDataAdapter(myCommand);
            daBasicInfoFromTrans.Fill(dtBasicInfoFromTrans);
            if (dtBasicInfoFromTrans.Rows.Count > 0)
            {
                ddlOfficeLocation.SelectedValue = dtBasicInfoFromTrans.Rows[0].ItemArray[0].ToString();
                LoadDepartmentCode(dtBasicInfoFromTrans.Rows[0].ItemArray[0].ToString());
                ddlDepartmentCode.SelectedValue = dtBasicInfoFromTrans.Rows[0].ItemArray[1].ToString();
                LoadSectionCode(dtBasicInfoFromTrans.Rows[0].ItemArray[0].ToString(), dtBasicInfoFromTrans.Rows[0].ItemArray[1].ToString());
                ddlSectionCode.SelectedValue = dtBasicInfoFromTrans.Rows[0].ItemArray[2].ToString();
                ddlDesignation.SelectedValue = dtBasicInfoFromTrans.Rows[0].ItemArray[3].ToString();
                ddlEmployeeType.SelectedValue = dtBasicInfoFromTrans.Rows[0].ItemArray[4].ToString();
                txtDesignationLevel.Text = dtBasicInfoFromTrans.Rows[0].ItemArray[5].ToString();
                txtForUpdateTrans_Det.Text = dtBasicInfoFromTrans.Rows[0].ItemArray[6].ToString();
            }

            var dtEmpReference = new DataTable();
            myCommand.CommandText = "exec [EmpBasicInformationGetFromHRMS_Emp_Reference] '" + employeeCode + "'";
            myCommand.ExecuteNonQuery();
            var daEmpReference = new SqlDataAdapter(myCommand);
            daEmpReference.Fill(dtEmpReference);
            if (dtEmpReference.Rows.Count > 0)
            {
                var CompanyNameForRef = dtEmpReference.Rows[0].ItemArray[0].ToString() == "" ? "-1" : dtEmpReference.Rows[0].ItemArray[0].ToString();
                ddlCompanyNameForRef.SelectedValue = CompanyNameForRef;
                txtEmployeeIDForRef.Text = dtEmpReference.Rows[0].ItemArray[2].ToString();
                ShowEmployeeDetails(txtEmployeeIDForRef.Text == string.Empty ? "" : txtEmployeeIDForRef.Text);
                txtReferenceName1.Text = dtEmpReference.Rows[0].ItemArray[3].ToString();
                txtContactNumberForRef1.Text = dtEmpReference.Rows[0].ItemArray[4].ToString();
                txtEmailForRef1.Text = dtEmpReference.Rows[0].ItemArray[5].ToString();
                txtNIDForRef1.Text = dtEmpReference.Rows[0].ItemArray[6].ToString();
                txtOrganizationRf1.Text = dtEmpReference.Rows[0].ItemArray[10].ToString();
                txtDesignationRef1.Text = dtEmpReference.Rows[0].ItemArray[11].ToString();
                txtReferenceName2.Text = dtEmpReference.Rows[0].ItemArray[12].ToString();
                txtOrganizationRef2.Text = dtEmpReference.Rows[0].ItemArray[13].ToString();
                txtDesignationRef2.Text = dtEmpReference.Rows[0].ItemArray[14].ToString();
                txtContactNumberForRef2.Text = dtEmpReference.Rows[0].ItemArray[15].ToString();
                txtEmailForRef2.Text = dtEmpReference.Rows[0].ItemArray[16].ToString();
                txtNIDForRef2.Text = dtEmpReference.Rows[0].ItemArray[17].ToString();

            }
            var dtPhoto = new DataTable();
            myCommand.CommandText = "exec [EmpBasicInformationGetFromHrms_Emp_Photo] '" + employeeCode + "','" + ddlcompany.SelectedValue + "'";
            myCommand.ExecuteNonQuery();
            var daPhoto = new SqlDataAdapter(myCommand);
            daPhoto.Fill(dtPhoto);
            ViewState["profileImage"] = null;
            lblImage.Text = "<br />  Photo <br />  Not <br />  Available ";
            if (dtPhoto.Rows.Count > 0)
            {
                var img = (byte[])dtPhoto.Rows[0].ItemArray[0];
                string base64string = Convert.ToBase64String(img, 0, img.Length);
                lblImage.Text = "<img src='data:image/png;base64," + base64string + "' alt='<br />  Photo <br />  Not <br />  Available ' width='150px' hight='150px' vspace='5px' hspace='5px' />";
                ViewState["profileImage"] = img;
            }

            var dtWorkLocation = new DataTable();
            myCommand.CommandText = "exec [EmpBasicInformationGetFromHRMS_WORK_LOCATION] '" + employeeCode + "'";
            myCommand.ExecuteNonQuery();
            var daWorkLocation = new SqlDataAdapter(myCommand);
            daWorkLocation.Fill(dtWorkLocation);
            if (dtWorkLocation.Rows.Count > 0)
            {
                ClsDropDownListController.LoadDropDownList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetWorkLocationIntoDDL(ddlOfficeLocation.SelectedValue), ddlWorkLocation, "WorkLocationName", "WorkLocationId");
                ddlWorkLocation.SelectedValue = dtWorkLocation.Rows[0].ItemArray[0].ToString() == "" ? "-1" : dtWorkLocation.Rows[0].ItemArray[0].ToString();
            }

            ClsDropDownListController.LoadDropDownList(Session[GlobalData.sessionConnectionstring].ToString(), Sqlgenerate.SqlGetLeaveTypeIntoDDLByEmpid(employeeCode), ddlLeaveType, "Leave_Mas_Name", "Leave_Mas_Code");
            ucOthersDocument1.LoadUploadFileByRef(employeeCode);
            _msg = "";
        }
        catch (SqlException sqlError)
        {
            _msg = "Error Occured During Operation into Database !";
        }
        catch (Exception inSystemExep)
        {
            _msg = "Error Occured !";
        }
        myConnection.Close();
        return _msg;
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckAllValidationForShow();
        switch (validationMsg)
        {
            case "":
                {
                    string employeeCode = txtEmployeeSearch.Text;
                    string getMsg = LoadAllDataOfAParticularEmployee(Session[GlobalData.sessionConnectionstring].ToString(), employeeCode);
                    if (getMsg != "")
                    {
                        MessageBox1.ShowInfo(getMsg);
                    }
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }
    }

    private void ClearAllControl()
    {
        txtForUpdateTrans_Det.Text = string.Empty;
        txtEmployeeCode.Text = string.Empty;
        txtEmployeeFirstName.Text = string.Empty;
        txtEmployeeLastName.Text = string.Empty;
        popupJoiningDate.Text = string.Empty;
        txtIdcardExpireDate.Text = string.Empty;
        ddlStatus.SelectedValue = "-1";
        ddlOfficeLocation.SelectedValue = "-1";
        ddlEmployeeType.SelectedValue = "-1";
        ddlReligion.SelectedValue = "-1";
        ddlMaritalStatus.SelectedValue = "-1";
        ddlGender.SelectedValue = "-1";
        ddlBloodGroup.SelectedValue = "-1";
        ddlDepartmentCode.Items.Clear();
        ddlSectionCode.Items.Clear();
        ddlDesignation.SelectedValue = "-1";
        ddlBankName.SelectedValue = "-1";
        ddlBranchName.Items.Clear();
        ddlLeaveType.SelectedValue = "-1";
        lstForLeaveAllocation.Items.Clear();
        txtIdCardID.Text = string.Empty;
        txtFatherName.Text = string.Empty;
        txtMotherName.Text = string.Empty;
        popupDateOfBirth.Text = string.Empty;
        txtHomePhone.Text = string.Empty;
        txtpersonalcontactno1.Text = string.Empty;
        txtpersonalcontactno2.Text = string.Empty;
        txtTINNumber.Text = string.Empty;
        txtPassportNumber.Text = string.Empty;
        txtEmergencyContactPerson1.Text = string.Empty;
        txtEmergencyContactpersonnumber1.Text = string.Empty;
        txtPresentAddress.Text = string.Empty;
        ddlDistrictPre.Items.Clear();
        ddlDivisionPre.SelectedValue = "-1";
        txtPostalCodePre.Text = string.Empty;
        txtCountryPre.Text = string.Empty;
        txtPermanentAddress.Text = string.Empty;
        ddlDivisionPer.SelectedValue = "-1";
        ddlDistrictPer.Items.Clear();
        txtPostalCodePer.Text = string.Empty;
        txtCountryPer.Text = string.Empty;
        txtBankAccountNo.Text = string.Empty;
        txtEmailPersonal.Text = string.Empty;
        txtDesignationLevel.Text = string.Empty;
        ViewState["AcademicQualification"] = null;
        ViewState["DependentsInformation"] = null;
        ViewState["AssetAllocation"] = null;
        ViewState["ProfessionalQualification"] = null;
        ViewState["TrainingRecord"] = null;
        ViewState["profileImage"] = null;
        lblImage.Text = "<br />  Photo <br />  Not <br />  Available ";
        grdAcademicRecords.DataSource = null;
        grdAcademicRecords.DataBind();
        grdAssetAllocation.DataSource = null;
        grdAssetAllocation.DataBind();
        grdDependentsInformation.DataSource = null;
        grdDependentsInformation.DataBind();
        grdProfessionalQualification.DataSource = null;
        grdProfessionalQualification.DataBind();
        grdTrainingRecord.DataSource = null;
        grdTrainingRecord.DataBind();
        ddlCompanyNameForRef.SelectedValue = "-1";
        txtEmployeeIDForRef.Text = string.Empty;
        txtReferenceName1.Text = string.Empty;
        txtContactNumberForRef1.Text = string.Empty;
        txtEmailForRef1.Text = string.Empty;
        txtNIDForRef1.Text = string.Empty;
        txtContactPersonPresent.Text = string.Empty;
        txtContactPersonPermanent.Text = string.Empty;
        txtContactNumberPresent.Text = string.Empty;
        txtContactNumberPermanent.Text = string.Empty;
        txtEmailPermanent.Text = string.Empty;
        txtEmailPresent.Text = string.Empty;
        txtProbationPeriod.Text = string.Empty;
        lblStatus.Text = string.Empty;
        lblStatus.Visible = false;
        lblStatusShow.Visible = false;
        ddlWorkLocation.SelectedValue = "-1";
        txtEmployeeSearch.Text = string.Empty;
        txtEmergencyContactPerson2.Text = string.Empty;
        txtEmergencyContactpersonnumber2.Text = string.Empty;
        txtEmailCompany.Text = string.Empty;
        txtEmailPersonal.Text = string.Empty;
        txtSpouseName.Text = string.Empty;
        txtNID.Text = string.Empty;
        txtDesignationRef1.Text = string.Empty;
        txtOrganizationRf1.Text = string.Empty;
        txtReferenceName2.Text = string.Empty;
        txtOrganizationRef2.Text = string.Empty;
        txtDesignationRef2.Text = string.Empty;
        txtContactNumberForRef2.Text = string.Empty;
        txtEmailForRef2.Text = string.Empty;
        txtNIDForRef2.Text = string.Empty;
        ddlShift.SelectedValue = "-1";
        PanelForEmployeeDetails.Visible = false;
        ucOthersDocument1.ClearGridFromUserControll();

    }

    private string DeleteAllRecordOfEmployee(string connectionString, string employeeCode)
    {
        var myConnection = new SqlConnection(connectionString);
        var myCommand = myConnection.CreateCommand();
        myConnection.Open();
        try
        {
            myCommand.CommandText = "exec [EmpBasicInformationRemove] '" + employeeCode + "'";
            myCommand.ExecuteNonQuery();
            _msg = " All Information Successfully Deleted ";
        }
        catch (SqlException sqlError)
        {
            _msg = "Error Occured During Operation into Database !";
        }
        catch (Exception inSystemExep)
        {
            _msg = "System Error !";
        }
        myConnection.Close();
        return _msg;
    }

    //protected void btnDelete_Click(object sender, EventArgs e)
    //{
    //    string employeeId = txtEmployeeCode.Text;
    //    string msg = DeleteAllRecordOfEmployee(Session[GlobalData.sessionConnectionstring].ToString(), employeeId);
    //    ClearAllControl();
    //    ScriptManager.RegisterStartupScript(
    //        this,
    //        GetType(),
    //        "MessageBox",
    //        "alert(' " + msg + "');",
    //        true);
    //}

    protected void txtEmployeeCode_TextChanged(object sender, EventArgs e)
    {
        if (txtEmployeeCode.Text != string.Empty)
        {
            txtEmployeeCode.Text = txtEmployeeCode.Text.Split(':')[0].Trim() == "" ? "" : txtEmployeeCode.Text.Split(':')[0].Trim();
        }
    }

    protected void ddlDivisionPre_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivisionPre.SelectedValue != "-1")
        {
            LoadDistrict(ddlDistrictPre, ddlDivisionPre.SelectedValue);
        }
        else
        {
            ddlDistrictPre.Items.Clear();
        }
    }

    protected void ddlDivisionPer_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivisionPer.SelectedValue != "-1")
        {
            LoadDistrict(ddlDistrictPer, ddlDivisionPer.SelectedValue);
        }
        else
        {
            ddlDistrictPer.Items.Clear();
        }
    }



    #region Photo
    private string SaveProfileImage(string employeeId)
    {
        string _msg;
        try
        {
            SqlConnection con = null;
            con = new SqlConnection(Session[GlobalData.sessionConnectionstring].ToString());
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
                cmd.Parameters.AddWithValue("@img", ViewState["profileImage"]);
                cmd.Parameters.AddWithValue("@CompanyCode", ddlcompany.SelectedValue);
                cmd.Parameters.AddWithValue("@EmpId", txtEmployeeCode.Text);
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

    private void ViewProfileImage(string employeeId)
    {
        SqlConnection con = null;
        con = new SqlConnection(Session[GlobalData.sessionConnectionstring].ToString());
        SqlCommand cmd = new SqlCommand("Select TOP 1 EmpPhoto from Hrms_Emp_Photo where EmpId = '" + employeeId + "'", con);
        con.Open();
        SqlDataReader dr = null;
        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                var img = (byte[])dr["EmpPhoto"];
                string base64string = Convert.ToBase64String(img, 0, img.Length);
                lblImage.Text = "<img src='data:image/png;base64," + base64string + "' alt='<br />  Photo <br />  Not <br />  Available '  vspace='5px' hspace='5px' />"; //width='135px' hight='180px'
                ViewState["profileImage"] = img;
            }
        }
        con.Close();
    }

    #endregion Phote



    protected void ddlCompanyNameForRef_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnNewEmployee_Click(object sender, EventArgs e)
    {
        AddOrUpdateEmployeeInformation();
    }
    protected void imgBtnProfileUpload_Click(object sender, ImageClickEventArgs e)
    {
        if (ProfileImageUpload.HasFile)
        {
            if (ProfileImageUpload.PostedFile.ContentType == "image/jpg" ||
                ProfileImageUpload.PostedFile.ContentType == "image/jpeg" ||
                ProfileImageUpload.PostedFile.ContentType == "image/gif" ||
                ProfileImageUpload.PostedFile.ContentType == "image/pjpeg" ||
                ProfileImageUpload.PostedFile.ContentType == "image/bmp" ||
                ProfileImageUpload.PostedFile.ContentType == "image/png")
            {
                int filelenght = ProfileImageUpload.PostedFile.ContentLength;
                if (filelenght <= 524288)  // 524288:500 kb // 1048576:100 kb
                {
                    //string FileName = Path.GetFileName(ProfileImageUpload.PostedFile.FileName);
                    //ProfileImageUpload.SaveAs(MapPath("~/Images/" + ProfileImageUpload.FileName));
                    //imgDemo.ImageUrl = "~/Images/" + ProfileImageUpload.FileName;

                    byte[] imagebytes = new byte[filelenght];
                    ProfileImageUpload.PostedFile.InputStream.Read(imagebytes, 0, filelenght);
                    byte[] img = imagebytes;
                    string base64string = Convert.ToBase64String(img, 0, img.Length);
                    System.Drawing.Image im = System.Drawing.Image.FromStream(ProfileImageUpload.PostedFile.InputStream);
                    double imageHight = im.PhysicalDimension.Height;
                    double imageWidth = im.PhysicalDimension.Width;
                    if (imageHight <= 150 && imageWidth <= 150)
                    {
                        lblImage.Text = "<img src='data:image/png;base64," + base64string +
                            "' alt='<br />  Photo <br />  Not <br />  Available ' width='" + imageWidth + "' hight='" + imageHight + "' vspace='5px' hspace='5px' />";
                        ViewState["profileImage"] = imagebytes;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "MessageBox",
                        "alert(' Image size should not be greater than 150X150 !');",
                        true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "MessageBox",
                        "alert(' Image size not more than 500 kb!');",
                        true);
                }

            }
        }

    }
    protected void txtEmployeeSearch_TextChanged(object sender, EventArgs e)
    {
        if (txtEmployeeSearch.Text != string.Empty)
        {
            txtEmployeeSearch.Text = txtEmployeeSearch.Text.Split(':')[0].Trim() == "" ? "" : txtEmployeeSearch.Text.Split(':')[0].Trim();
        }
    }
    protected void btnTraining_Click(object sender, EventArgs e)
    {
        ModalPopupExtenderTrainingRecord.Show();
        ClearControlOfTrainingRecord();
        btnAddTrainingRecord.Text = "Add";
    }
    private void UpdateTrainingRecord()
    {
        if ((DataTable)ViewState["TrainingRecord"] != null)
        {
            var indexForDelete = Convert.ToInt32(Session["indexTrainingRecord"].ToString());
            var dt = (DataTable)ViewState["TrainingRecord"];
            dt.Rows[indexForDelete].Delete();
            dt.AcceptChanges();
            ViewState["TrainingRecord"] = dt;
            btnAddTrainingRecord.Text = "Add";
        }

    }
    protected void btnAddTrainingRecord_Click(object sender, EventArgs e)
    {
        var documentCode = AttachFileSaveAccordingToCatagory(FileUploadTraining);
        if (btnAddTrainingRecord.Text != "Add" && documentCode == null)
        {
            documentCode = Session["trainingDocument"].ToString() == "" ? null : Session["trainingDocument"].ToString();
        }



        if (btnAddTrainingRecord.Text == "Update")
        {
            UpdateTrainingRecord();

        }


        var trainingCode = "";
        var trainingTitle = "";
        var nameOfTrainingInstitution = txtTrainingInstituteName.Text;
        var trainingInstitutionAddress = txtTrainingInstituteAddress.Text;
        var trainingStartedDate = Convert.ToDateTime(calendarTrainingStartDate.SelectedDate).ToString("dd-MMM-yyyy");
        var trainingEndDate = Convert.ToDateTime(calendarTrainingEndDate.SelectedDate).ToString("dd-MMM-yyyy");
        var trainingDuration = Convert.ToInt32(txtDuration.Text == "" ? 0 : Convert.ToInt32(txtDuration.Text));
        var trainingFee = Convert.ToDouble(txtTrainingFee.Text == "" ? 0 : Convert.ToDouble(txtTrainingFee.Text));
        var trainingAchievement = txtTrainingBenefits.Text;
        var trainingTitleSpecification = txtTrainingTitle.Text;
        string certificateCode = null;
        var certificateTitle = txtCertificateAchieved.Text == string.Empty ? null : txtCertificateAchieved.Text;
        string fundCode = null;
        var fundTitle = txtFundBy.Text == string.Empty ? null : txtFundBy.Text;
        
        var validationMsg = CheckValidationForTrainingRecord(trainingCode);
        if (validationMsg == "")
        {
            BindTrainingRecordGrid(trainingCode, trainingTitle, nameOfTrainingInstitution, trainingInstitutionAddress, trainingStartedDate, trainingEndDate, trainingDuration, trainingFee, trainingAchievement,
                 trainingTitleSpecification, certificateCode, certificateTitle, fundCode, fundTitle, documentCode);
            ClearControlOfTrainingRecord();
            lblMessageTrainingRecord.Text = string.Empty;
        }
        else
        {
            lblMessageTrainingRecord.Text = validationMsg;
            ModalPopupExtenderTrainingRecord.Show();
        }
    }
    protected void grdTrainingRecord_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void grdTrainingRecord_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var con = Convert.ToInt32(e.CommandArgument.ToString());
        Session["rowCommandName"] = "";
        if (e.CommandName == "Download")
        {
            Session["rowCommandName"] = "fileDownload";
            string gg = grdTrainingRecord.Rows[con].Cells[15].Text.Trim();
            String F1Path, F1Name;
            string abc = filepath + "\\" + gg.ToString().Replace("&amp;", "&");
            F1Path = abc.ToString();
            F1Name = Path.GetFileName(F1Path);
            GetFile(F1Path, F1Name);
        }

        if (e.CommandName.Equals("Select"))
        {
            Session["indexTrainingRecord"] = con;

            txtTrainingInstituteName.Text = grdTrainingRecord.Rows[con].Cells[4].Text == "&nbsp;" ? "" : grdTrainingRecord.Rows[con].Cells[4].Text;
            txtTrainingInstituteAddress.Text = grdTrainingRecord.Rows[con].Cells[5].Text == "&nbsp;" ? "" : grdTrainingRecord.Rows[con].Cells[5].Text;
            var tempStartDate = grdTrainingRecord.Rows[con].Cells[6].Text == "&nbsp;" ? "" : grdTrainingRecord.Rows[con].Cells[6].Text;
            if (tempStartDate != "")
            {
                calendarTrainingStartDate.SelectedDate = Convert.ToDateTime(tempStartDate);
            }


            var tempEndDate = grdTrainingRecord.Rows[con].Cells[7].Text == "&nbsp;" ? "" : grdTrainingRecord.Rows[con].Cells[7].Text;
            if (tempEndDate != "")
            {
                calendarTrainingEndDate.SelectedDate = Convert.ToDateTime(tempEndDate);
            }


            txtDuration.Text = grdTrainingRecord.Rows[con].Cells[8].Text == "&nbsp;" ? "" : grdTrainingRecord.Rows[con].Cells[8].Text;
            txtTrainingFee.Text = grdTrainingRecord.Rows[con].Cells[9].Text == "&nbsp;" ? "" : grdTrainingRecord.Rows[con].Cells[9].Text;
            txtTrainingBenefits.Text = grdTrainingRecord.Rows[con].Cells[3].Text == "&nbsp;" ? "" : grdTrainingRecord.Rows[con].Cells[3].Text;
            txtTrainingTitle.Text = grdTrainingRecord.Rows[con].Cells[2].Text == "&nbsp;" ? "" : grdTrainingRecord.Rows[con].Cells[2].Text;
            txtCertificateAchieved.Text = grdTrainingRecord.Rows[con].Cells[10].Text == "&nbsp;" ? "" : grdTrainingRecord.Rows[con].Cells[10].Text;
            txtFundBy.Text = grdTrainingRecord.Rows[con].Cells[11].Text == "&nbsp;" ? "" : grdTrainingRecord.Rows[con].Cells[11].Text;
            ModalPopupExtenderTrainingRecord.Show();
            btnAddTrainingRecord.Text = "Update";
            Session["trainingDocument"] = grdTrainingRecord.Rows[con].Cells[15].Text == "&nbsp;" ? "" : grdTrainingRecord.Rows[con].Cells[15].Text;
            
        }


        if (!e.CommandName.Equals("Delete")) return;
        var dt = (DataTable)ViewState["TrainingRecord"];
        dt.Rows[con].Delete();
        dt.AcceptChanges();
        ViewState["TrainingRecord"] = dt;
        if (ViewState["TrainingRecord"] == null) return;
        grdTrainingRecord.DataSource = ViewState["TrainingRecord"];
        grdTrainingRecord.DataBind();
    }
    protected void grdTrainingRecord_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string documentCode = e.Row.Cells[15].Text;
            if (documentCode == "&nbsp;")
            {
                e.Row.FindControl("ImageButtonDownloadAcademic").Visible = false;
            }

            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[15].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[15].Visible = false;
        }
    }
    private void parameterpass(ParameterFields myParams, string pname, string value)
    {
        ParameterField param = new ParameterField();
        ParameterDiscreteValue dis1 = new ParameterDiscreteValue();
        param.ParameterFieldName = pname;
        dis1.Value = value;
        param.CurrentValues.Add(dis1);
        myParams.Add(param);
    }
    private void ShowReport(string selectionfor, string parameter, string reportname)
    {
        clsReport rpt = new clsReport();
        ParameterFields myParams = new ParameterFields();
        ConnectionInfo ConnInfo = new ConnectionInfo();
        string SCFconnStr = Session[GlobalData.sessionConnectionstring].ToString();
        string[] ff;
        string[] ss;
        string[] prm;
        prm = parameter.Split(';');
        if (prm.Length > 0)
        {
            for (int i = 0; i < prm.Length; i++)
            {
                parameterpass(myParams, prm[i].Split(':')[0].ToString(), prm[i].Split(':')[1].ToString());
            }
        }
        ff = SCFconnStr.Split('=');
        ss = ff[1].Split(';');
        ConnInfo.ServerName = ss[0];
        ss = ff[2].Split(';');
        ConnInfo.DatabaseName = ss[0];
        ss = ff[3].Split(';');
        ConnInfo.UserID = ss[0];
        ss = ff[4].Split(';');
        ConnInfo.Password = ss[0];
        rpt.FileName = reportname;
        rpt.ConnectionInfo = ConnInfo;
        rpt.ParametersFields = myParams;
        rpt.SelectionFormulla = selectionfor;
        Session[GlobalData.sessionReportDet] = rpt;
        RegisterStartupScript("click", "<script>window.open('./frm_report_viewer.aspx');</script>");
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckAllValidationForShow();
        switch (validationMsg)
        {
            case "":
                {
                    string companyCodeValue = ddlcompany.SelectedValue;
                    string employeeCode = txtEmployeeSearch.Text;
                    string sql = @" create view ViewEmpBasicInformationGetFromHrMs_Emp_mas  as SELECT 
				Emp_Mas_First_Name,Emp_Mas_Last_Name,Emp_Mas_Father_Name,
				Emp_Mas_Mother,b.Emp_Present_Address,b.Emp_Present_Division,b.Emp_Present_District,	b.Emp_Present_PostalCode,b.Emp_Present_Country,
				b.Emp_Permanent_Address,b.Emp_Permanent_Division,b.Emp_Permanent_District,b.Emp_Permanent_PostalCode,b.Emp_Permanent_Country,
				Emp_Mas_Religion,CONVERT (VARCHAR,Emp_Mas_DOB,106) as Emp_Mas_DOB,Emp_Mas_MaritalStatus,Emp_Mas_Gender,Emp_Mas_Bloodgrp,
				Emp_Mas_Homephone,Emp_Mas_Workphone,Emp_Mas_HandSet,Emp_Mas_Status,CONVERT (VARCHAR,Emp_Mas_Join_Date,106) as Emp_Mas_Join_Date ,
				Emp_Mas_TIN,Emp_Mas_Pp_No,Emp_Mas_Emer_Cont_Name,Emp_Mas_Emer_Cont_No,Emp_Mas_Emp_Type,	Emp_Mas_Dept_Code,
				Emp_Mas_CardID,Emp_Mas_Remarks,CASE	WHEN Emp_Mas_Card_ExpiryDate IS NULL THEN CONVERT (VARCHAR,GETDATE(),106) 
				WHEN Emp_Mas_Card_ExpiryDate IS NOT NULL THEN CONVERT (VARCHAR,Emp_Mas_Card_ExpiryDate,106)	END AS Emp_Mas_Card_ExpiryDateValue,
				b.Emp_Present_ContactPerson,b.Emp_Present_ContactNumber,
				b.Emp_Present_Email,
				b.Emp_Permanent_ContactPerson,
				b.Emp_Permanent_ContactNumber,
				b.Emp_Permanent_Email,
				b.Emp_NID,
				a.T_In,
				a.Emp_Mas_Emp_Id,
				c.statusText,
				d.genderText,
				UPPER(Emp_Mas_First_Name) + ' '+UPPER( Emp_Mas_Last_Name) as FullName
	            FROM HrMs_Emp_mas a
	            LEFT JOIN HRMS_Emp_Address b
	            ON a.Emp_Mas_Emp_Id = b.Emp_Mas_Emp_Id
	            LEFT JOIN HRMS_Emp_Status c ON a.Emp_Mas_Status = c.statusCode
	            LEFT JOIN HRMS_Emp_Gender d ON a.Emp_Mas_Gender = d.genderCode
	            WHERE a.Emp_Mas_Emp_Id = '" + employeeCode + "'";
                    DataProcess.ExecuteQuery(ConnectionString, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ViewEmpBasicInformationGetFromHrMs_Emp_mas]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[ViewEmpBasicInformationGetFromHrMs_Emp_mas]");
                    DataProcess.ExecuteQuery(ConnectionString, sql);

                    string sqlBankInfo = @" create view ViewEmpBasicInformationGetFromHrms_Emp_Bnk_Info  as SELECT 
	            A.Bnk_Code,
	            A.Brc_Code,
	            A.Acc_No,
	            A.Emp_ID,
	            B.Bnk_info_Bnk_Name,
	            B.Bnk_info_Branch_name	
                FROM Hrms_Emp_Bnk_Info A
                LEFT JOIN FA_BNK_INFO B ON A.Bnk_Code = B.Bnk_info_Bnk_code AND A.Brc_Code = B.Bnk_info_Branch_Code
                WHERE A.Emp_ID = '" + employeeCode + "'";
                    DataProcess.ExecuteQuery(ConnectionString, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ViewEmpBasicInformationGetFromHrms_Emp_Bnk_Info]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[ViewEmpBasicInformationGetFromHrms_Emp_Bnk_Info]");
                    DataProcess.ExecuteQuery(ConnectionString, sqlBankInfo);

                    string sqlTrans = "create view ViewEmpBasicInformationGetFromHrms_Trans_Det as " +
                    " SELECT A.Trans_Det_DivID,A.Trans_det_DeptID,A.Trans_det_SecID," +
                    " A.Trans_Det_JobID,A.Trans_Det_Emptype,A.T_C1," +
                    " Pos as rowNumber," +
                    " A.Trans_Det_Emp_Id,B.Division_Master_Name,C.Dept_Name,D.Sect_Name,E.JobTitle" +
                    " FROM Hrms_Trans_Det A" +
                    " inner HRMS_DIVISION_MASTER B ON A.Trans_Det_DivID = B.Division_Master_Code" +
                    " inner JOIN Hrms_Dept_Master C ON A.Trans_det_DeptID = C.Dept_Code AND A.Trans_Det_DivID = C.Dept_Division_Code" +
                    " inner JOIN Hrms_Sect_Mas D ON A.Trans_Det_DivID = D.Sect_Div_Code AND A.Trans_det_DeptID = D.Sect_Dept_Code AND A.Trans_det_SecID = D.Sect_Code" +
                    " inner JOIN Hrms_Job_Master E ON A.Trans_Det_JobID = E.JobCode" +
                    " inner join emp_details em on em.empid=a.Trans_Det_Emp_Id " +
                    "WHERE Trans_Det_Emp_Id = '" + employeeCode + "'";
                    DataProcess.ExecuteQuery(ConnectionString, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ViewEmpBasicInformationGetFromHrms_Trans_Det]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[ViewEmpBasicInformationGetFromHrms_Trans_Det]");
                    DataProcess.ExecuteQuery(ConnectionString, sqlTrans);

                    string sqlWorkLocation = @" create view ViewEmpBasicInformationGetFromHRMS_WORK_LOCATION  as select A.WorkLocationId,B.WorkLocationName from HRMS_WORK_LOCATION A 
	            LEFT JOIN HRMS_WORK_LOCATION_MASTER B ON A.WorkLocationId = B.WorkLocationId
                WHERE A.EmpId = '" + employeeCode + "' AND  A.ToDate IS NULL";
                    DataProcess.ExecuteQuery(ConnectionString, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ViewEmpBasicInformationGetFromHRMS_WORK_LOCATION]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[ViewEmpBasicInformationGetFromHRMS_WORK_LOCATION]");
                    DataProcess.ExecuteQuery(ConnectionString, sqlWorkLocation);

                    string sqlPhoto =
                        @" create view ViewEmpBasicInformationGetFromHrms_Emp_Photo  as Select TOP 1 EmpPhoto from Hrms_Emp_Photo where EmpId = '" + employeeCode + "' AND CompanyCode = '" + companyCodeValue + "'";
                    DataProcess.ExecuteQuery(ConnectionString, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ViewEmpBasicInformationGetFromHrms_Emp_Photo]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[ViewEmpBasicInformationGetFromHrms_Emp_Photo]");
                    DataProcess.ExecuteQuery(ConnectionString, sqlPhoto);
                    string sqlReference = @" create view ViewEmpBasicInformationGetFromHRMS_Emp_Reference  as SELECT 
                  A.[Emp_Ref_CompanyCode]
                  ,A.[Emp_Ref_Dept_Code]
                  ,A.[Emp_Ref_Emp_Id]
                  ,A.[Emp_Ref_Name]
                  ,A.[Emp_Ref_ContactNumber]
                  ,A.[Emp_Ref_Email]
                  ,A.[Emp_Ref_NID]
                  ,A.[Emp_Id]
                  ,B.CompanyName
                  ,em.Dept
                  ,A.Emp_Ref_Organization
                  ,A.Emp_Ref_Designation
                  ,A.Emp_Ref2_Name
                  ,A.Emp_Ref2_Organization
                  ,A.Emp_Ref2_Designation
                  ,A.Emp_Ref2_ContactNumber
                  ,A.Emp_Ref2_Email
                  ,A.Emp_Ref2_NID
              FROM [HRMS_Emp_Reference] A
              LEFT outer JOIN Hrms_Company_Master B ON A.Emp_Ref_CompanyCode = B.CompanyId 
              inner join emp_details em on em.empid=a.Emp_Id 
              WHERE [Emp_Id] = '" + employeeCode + "'";
                    DataProcess.ExecuteQuery(ConnectionString, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ViewEmpBasicInformationGetFromHRMS_Emp_Reference]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[ViewEmpBasicInformationGetFromHRMS_Emp_Reference]");
                    DataProcess.ExecuteQuery(ConnectionString, sqlReference);

                    string sqlLeaveInfo =
                        @" create view ViewEmpBasicInformationGetFromHrms_Emp_Leave_Info as SELECT DISTINCT a.Leave_Type,b.Leave_Mas_Name FROM Hrms_Emp_Leave_Info a INNER JOIN HRMS_Leave_Mas b ON a.Leave_Type = b.Leave_Mas_Code WHERE a.Hrms_Emp_Id = '" + employeeCode + "'";
                    DataProcess.ExecuteQuery(ConnectionString, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ViewEmpBasicInformationGetFromHrms_Emp_Leave_Info]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[ViewEmpBasicInformationGetFromHrms_Emp_Leave_Info]");
                    DataProcess.ExecuteQuery(ConnectionString, sqlLeaveInfo);

                    string sqlDependent = @" create view ViewEmpBasicInformationGetFromHrms_Dpdnt_Mas as SELECT 
                    Dpdnt_Name,
                    Dpdnt_Gender,
                    CONVERT (varchar,Dpdnt_DOB,106) AS Dpdnt_DOB,
                    Dpdnt_Relation,
                    CASE 
		                    WHEN Dpdnt_Gender = 'M' 
                            THEN 'Male' 
                            WHEN Dpdnt_Gender = 'F' 
                            THEN 'Female' 
                            END AS GenderText
                     FROM Hrms_Dpdnt_Mas
                     WHERE Dpdnt_Empid = '" + employeeCode + "'";
                    DataProcess.ExecuteQuery(ConnectionString, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ViewEmpBasicInformationGetFromHrms_Dpdnt_Mas]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[ViewEmpBasicInformationGetFromHrms_Dpdnt_Mas]");
                    DataProcess.ExecuteQuery(ConnectionString, sqlDependent);

                    string sqlAcademicInfo = @" create view ViewEmpBasicInformationGetFromHrms_Emp_Academic_Info as SELECT 
		            Name_Of_Degree,
		            Institution_Name,
		            Board_University,
		            Results_Grade,
		            Passing_Year,
		            Course_Duration
                    FROM Hrms_Emp_Academic_Info
                    WHERE Hrms_Emp_Id = '" + employeeCode + "'";
                    DataProcess.ExecuteQuery(ConnectionString, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ViewEmpBasicInformationGetFromHrms_Emp_Academic_Info]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[ViewEmpBasicInformationGetFromHrms_Emp_Academic_Info]");
                    DataProcess.ExecuteQuery(ConnectionString, sqlAcademicInfo);

                    string sqlProfessionalInfo = @" create view ViewEmpBasicInformationGetFromHrms_Exp_Mas as SELECT 
                    Exp_Comp,
                    Exp_COmpAddress,
                    Exp_Desig,
                    CONVERT (varchar,Exp_DateFrom,106) as Exp_DateFrom ,
                    CONVERT (varchar,Exp_DateTo,106) as Exp_DateTo,
                    Exp_Salary,
                    Exp_Remarks
                    FROM Hrms_Exp_Mas
                    WHERE Exp_EmpId = '" + employeeCode + "'";
                    DataProcess.ExecuteQuery(ConnectionString, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ViewEmpBasicInformationGetFromHrms_Exp_Mas]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[ViewEmpBasicInformationGetFromHrms_Exp_Mas]");
                    DataProcess.ExecuteQuery(ConnectionString, sqlProfessionalInfo);

                    string sqlAssetInfo = @" create view ViewEmpBasicInformationGetFromHrms_Emp_Asset_Info as SELECT 
		            Asset_Name,
		            Asset_Id_No,
		            CONVERT (varchar,Active_Date,106) as Active_Date ,
		            CONVERT (varchar,Inactive_Date,106) as Inactive_Date,
		            Asset_Status,
		            CASE 
		            WHEN Asset_Status = 'Y' 
                    THEN 'Active' 
                    WHEN Asset_Status = 'N' 
                    THEN 'Inactive' 
                    END AS Asset,
                    Asset_Model_No,
		            AssetDescription,
		            Reporting_Emp_Id
                    FROM Hrms_Emp_Asset_Info
                    WHERE Hrms_Emp_Id = '" + employeeCode + "'";
                    DataProcess.ExecuteQuery(ConnectionString, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ViewEmpBasicInformationGetFromHrms_Emp_Asset_Info]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[ViewEmpBasicInformationGetFromHrms_Emp_Asset_Info]");
                    DataProcess.ExecuteQuery(ConnectionString, sqlAssetInfo);

                    string sqlTrainingInfo = @" create view ViewEmpBasicInformationGetFromHRMS_Training_Record as SELECT A.[trainingCode]
	                  ,B.trainingTitle
                      ,A.[instituteName]
                      ,A.[instituteAddress]
                      ,CONVERT (varchar,A.[startDate],106) AS startDate
                      ,CONVERT (varchar,A.[endDate],106) AS endDate
                      ,A.[trainingDuration]
                      ,A.[trainingFee]
                      ,A.trainingAchievement
                      ,A.[trainingTitle] AS trainingTitleSpecification 
                      ,A.[certificateCode]
                      ,C.certificateTitle
                      ,A.[fundCode]
                      ,D.fundTitle
                      FROM [HRMS_Training_Record] A
                      INNER JOIN HRMS_Training_Setup B ON A.trainingCode = B.trainingCode
                      LEFT JOIN HRMS_Certificate_Type C ON A.certificateCode = C.certificateCode
                      LEFT JOIN HRMS_Fund_Type D ON A.fundCode = D.fundCode
                      WHERE [employeeCode] = '" + employeeCode + "'";

                    DataProcess.ExecuteQuery(ConnectionString, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ViewEmpBasicInformationGetFromHRMS_Training_Record]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[ViewEmpBasicInformationGetFromHRMS_Training_Record]");
                    DataProcess.ExecuteQuery(ConnectionString, sqlTrainingInfo);

                    string selectionfor, parameter;
                    selectionfor = "";
                    string Emp_Mas_Emp_Id = "@Emp_Mas_Emp_Id" + ":" + employeeCode;
                    string CompanyName = "CompanyName" + ":" + "Clarke Energy Bangladesh Ltd";
                    string CompanyAddress = "CompanyAddress" + ":" + "Dhaka";
                    string Emp_ID = "@Emp_ID" + ":" + employeeCode;
                    string Trans_Det_Emp_Id = "@Trans_Det_Emp_Id" + ":" + employeeCode;
                    string EmpId = "@EmpId" + ":" + employeeCode;
                    string companyCode = "@CompanyCode" + ":" + companyCodeValue;
                    parameter = Emp_Mas_Emp_Id + ";" + CompanyName + ";" + CompanyAddress + ";" + Emp_ID + ";" + Trans_Det_Emp_Id + ";" + EmpId + ";" + companyCode;
                    string reportname = "../Reports/EmployeebasicInformation.rpt";
                    ShowReport(selectionfor, parameter, reportname);

                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }
    }
    protected void txtEmployeeIDForRef_TextChanged(object sender, EventArgs e)
    {
        if (txtEmployeeIDForRef.Text != string.Empty)
        {
            txtEmployeeIDForRef.Text = txtEmployeeIDForRef.Text.Split(':')[0].Trim() == "" ? "" : txtEmployeeIDForRef.Text.Split(':')[0].Trim();
            ShowEmployeeDetails(txtEmployeeIDForRef.Text);
        }

    }
    private void ShowEmployeeDetails(string employeeId)
    {
        string msg = null;
        try
        {
            objForRefDepartment = new EmployeeInformationController();
            var dtEmployeeDetails = objForRefDepartment.GetEmployeeDetails(ConnectionString, employeeId);
            PanelForEmployeeDetails.Visible = false;
            if (dtEmployeeDetails.Rows.Count > 0)
            {
                lblEmployeeName.Text = dtEmployeeDetails.Rows[0].ItemArray[0] + " " + dtEmployeeDetails.Rows[0].ItemArray[1];
                lblEmployeeDepartment.Text = dtEmployeeDetails.Rows[0].ItemArray[4].ToString();
                lblDesignation.Text = dtEmployeeDetails.Rows[0].ItemArray[6].ToString() == string.Empty ? null : dtEmployeeDetails.Rows[0].ItemArray[6].ToString();
                PanelForEmployeeDetails.Visible = true;
            }
        }
        catch (SqlException sqlError)
        {
            msg = " Error Occured During Load Data From Database, Data did not Loaded from Database ! ";

        }
        catch (Exception inSystemExep)
        {
            msg = " Error Occured, Data did not Loaded from Database  !";

        }
        finally
        {
            if (msg != null)
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + msg + " ');",
                    true);
            }

        }

    }

    private void ShowReportingEmployeeDetails(string employeeId)
    {
        string msg = null;
        try
        {
            objForRefDepartment = new EmployeeInformationController();
            var dtEmployeeDetails = objForRefDepartment.GetEmployeeDetails(ConnectionString, employeeId);
            PanelReportingPersonDetails.Visible = false;
            if (dtEmployeeDetails.Rows.Count > 0)
            {
                lblReportingPersonName.Text = dtEmployeeDetails.Rows[0].ItemArray[0] + " " + dtEmployeeDetails.Rows[0].ItemArray[1];
                lblReportingPersonDepartment.Text = dtEmployeeDetails.Rows[0].ItemArray[4].ToString();
                lblReportingPersonDesignation.Text = dtEmployeeDetails.Rows[0].ItemArray[6].ToString() == string.Empty ? null : dtEmployeeDetails.Rows[0].ItemArray[6].ToString();
                PanelReportingPersonDetails.Visible = true;
            }
        }
        catch (SqlException sqlError)
        {
            msg = " Error Occured During Load Data From Database, Data did not Loaded from Database ! ";

        }
        catch (Exception inSystemExep)
        {
            msg = " Error Occured, Data did not Loaded from Database  !";

        }
        finally
        {
            if (msg != null)
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + msg + " ');",
                    true);
            }

        }

    }

    protected void txtAssetReportingPerson_TextChanged(object sender, EventArgs e)
    {
        if (txtAssetReportingPerson.Text != string.Empty)
        {
            txtAssetReportingPerson.Text = txtAssetReportingPerson.Text.Split(':')[0].Trim() == string.Empty ? "" : txtAssetReportingPerson.Text.Split(':')[0].Trim();
            ShowReportingEmployeeDetails(txtAssetReportingPerson.Text);
            ModalPopupExtenderAssetAllocation.Show();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
        ViewState["profileImage"] = null;
    }
    protected void grdAcademicRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["rowCommandName"].ToString() == "fileDownload")
        {
            int indx = grdAcademicRecords.SelectedIndex;
            string gg = grdAcademicRecords.Rows[indx].Cells[8].Text.Trim();
            String F1Path, F1Name;
            string abc = filepath + "\\" + gg.ToString().Replace("&amp;", "&");
            F1Path = abc.ToString();
            F1Name = Path.GetFileName(F1Path);
            GetFile(F1Path, F1Name);

        }

    }
    protected void grdTrainingRecord_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["rowCommandName"].ToString() == "fileDownload")
        {
            int indx = grdTrainingRecord.SelectedIndex;
            string gg = grdTrainingRecord.Rows[indx].Cells[15].Text.Trim();
            String F1Path, F1Name;
            string abc = filepath + "\\" + gg.ToString().Replace("&amp;", "&");
            F1Path = abc.ToString();
            F1Name = Path.GetFileName(F1Path);
            GetFile(F1Path, F1Name);

        }

    }
    protected void grdProfessionalQualification_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["rowCommandName"].ToString() == "fileDownload")
        {
            int indx = grdProfessionalQualification.SelectedIndex;
            string gg = grdProfessionalQualification.Rows[indx].Cells[8].Text.Trim();
            String F1Path, F1Name;
            string abc = filepath + "\\" + gg.ToString().Replace("&amp;", "&");
            F1Path = abc.ToString();
            F1Name = Path.GetFileName(F1Path);
            GetFile(F1Path, F1Name);
        }
    }
    protected void grdAssetAllocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["rowCommandName"].ToString() == "fileDownload")
        {
            int indx = grdAssetAllocation.SelectedIndex;
            string gg = grdAssetAllocation.Rows[indx].Cells[10].Text.Trim();
            String F1Path, F1Name;
            string abc = filepath + "\\" + gg.ToString().Replace("&amp;", "&");
            F1Path = abc.ToString();
            F1Name = Path.GetFileName(F1Path);
            GetFile(F1Path, F1Name);

        }

    }
    protected void btnUploadDoc_Click(object sender, EventArgs e)
    {
        EmployeeInformation objEmployeeInfo = new EmployeeInformation();

        AttachFileSave(objEmployeeInfo);

        MessageBox1.ShowSuccess("Document upload successful.");

    }
}