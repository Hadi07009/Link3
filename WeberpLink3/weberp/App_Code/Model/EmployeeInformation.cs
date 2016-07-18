using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EmployeeInformation
/// </summary>
public class EmployeeInformation
{
	public EmployeeInformation()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string EmployeeCode { get;  set; }
    public string EmployeeFirstName { get;  set; }
    public string EmployeeLastName { get;  set; }
    public string JoiningDate { get;  set; }
    public string Status { get;  set; }
    public string OfficeLocation { get;  set; }
    public string WorkLocation { get; set; }
    public string DepartmentCode { get;  set; }
    public string SectionCode { get;  set; }
    public string Designation { get;  set; }
    public string EmployeeType { get;  set; }
    public string CardId { get;  set; }
    public string CardExpiryDate { get; set; }
    public string FatherHusbandName { get;  set; }
    public string MotherName { get;  set; }
    public string SpouseName { get; set; }

    public string Religion { get;  set; }
    public string DateOfBirth { get;  set; }
    public string MaritalStatus { get;  set; }
    public string Gender { get;  set; }
    public string BloodGroup { get;  set; }
    public string HomePhone { get;  set; }
    public string Phone1 { get;  set; }
    public string Phone2 { get;  set; }
    public string TInNumber { get;  set; }
    public string PassportNumber { get;  set; }

    public string EmergencyContactName { get;  set; }
    public string EmergencyNumber { get;  set; }


    public string   EmergencyContactpersonName2 { get; set; }
    public string EmergencyContactpersonName2Number { get; set; }

    public string EmpShift { get; set; }

    public string PresentAddress { get;  set; }
    public string DistrictP { get;  set; }
    public string DivisionP { get;  set; }
    public string PostalCodeP { get;  set; }
    public string CountryP { get;  set; }
    public string PermanentAddress { get;  set; }
    public string DistrictPr { get;  set; }
    public string DivisionPr { get;  set; }
    public string PostalCodePr { get;  set; }
    public string CountryPr { get;  set; }
    public string BankCode { get;  set; }
    public string BranchCode { get;  set; }
    public string BankAccountNo { get;  set; }
    public string EmployeeGrade { get;  set; }
    public string DesignationLevel { get; set; }

    public string Email { get; set; }
    public string EmailPersonal { get; set; }


    public int RowNumberForUpdate { get; set; }
    public string CompanyCode { get; set; }
    public byte[] EmpPhoto { get; set; }
    public string ContactPersonPresent { get; set; }
    public string ContactNumberPresent { get; set; }
    public string EmailPresent { get; set; }
    public string ContactPersonPermanent { get; set; }
    public string ContactNumberPermanent { get; set; }
    public string EmailPermanent { get; set; }
    public string EmployeeNID { get; set; }
    public int ProbationPeriod { get; set; }
    public string DrivingLicense { get; set; }


    public List<AcademicQualification> AcademicQualifications { get;  set; }
    public List<ProfessionalQualification> ProfessionalQualifications { get; set; }
    public List<DependentsInformation> DependentsInformations { get; set; }
    public List<AssetAllocation> AssetAllocations{ get; set; }
    public List<LeaveAllocation> LeaveAllocations { get; set; }
    public List<TrainingRecord> TrainingRecords { get; set; }

    #region Employee Reference

    public string EmpRefDeptCode { get; set; }
    public string EmpRefCompanyCode { get; set; }
    public string EmpRefEmpId { get; set; }

    public string EmpRefName { get; set; }
    public string EmpRefContactNumber { get; set; }
    public string EmpRefEmail { get; set; }
    public string EmpRefNID { get; set; }

    public string Emp_Ref_Organization { get; set; }
    public string Emp_Ref_Designation { get; set; }

    public string Emp_Ref2_Name { get; set; }
    public string Emp_Ref2_Organization { get; set; }
    public string Emp_Ref2_Designation { get; set; }
    public string Emp_Ref2_ContactNumber { get; set; }
    public string Emp_Ref2_Email { get; set; }
    public string Emp_Ref2_NID { get; set; }
    

    #endregion Employee Reference

    public string DocumentCode { get; set; }

    public string ActivityCode { get; set; }
}