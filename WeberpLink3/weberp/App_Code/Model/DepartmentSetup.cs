using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DepartmentSetup
/// </summary>
public class DepartmentSetup
{
    public DepartmentSetup()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private string _companyCode;

    public string CompanyCode
    {
        get { return _companyCode; }
        set { _companyCode = value; }
    }
    private string _officeLocation;

    public string OfficeLocation
    {
        get { return _officeLocation; }
        set { _officeLocation = value; }
    }
    private string _departmentCode;

    public string DepartmentCode
    {
        get { return _departmentCode; }
        set { _departmentCode = value; }
    }
    private string _departmentName;

    public string DepartmentName
    {
        get { return _departmentName; }
        set { _departmentName = value; }
    }
    private string _departmentLocation;

    public string DepartmentLocation
    {
        get { return _departmentLocation; }
        set { _departmentLocation = value; }
    }
    private string _txtTag;

    public string TxtTag
    {
        get { return _txtTag; }
        set { _txtTag = value; }
    }
    private string _headOfDepartment;

    public string HeadOfDepartment
    {
        get { return _headOfDepartment; }
        set { _headOfDepartment = value; }
    }
    private string _substituteHOD;

    public string SubstituteHOD
    {
        get { return _substituteHOD; }
        set { _substituteHOD = value; }
    }
    private string _txtStatus;

    public string TxtStatus
    {
        get { return _txtStatus; }
        set { _txtStatus = value; }
    }
}