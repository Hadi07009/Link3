using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SectionSetup
/// </summary>
public class SectionSetup
{
    public SectionSetup()
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
    private string _sectionCode;

    public string SectionCode
    {
        get { return _sectionCode; }
        set { _sectionCode = value; }
    }
    private string _sectionName;

    public string SectionName
    {
        get { return _sectionName; }
        set { _sectionName = value; }
    }
    private string _txtTag;

    public string TxtTag
    {
        get { return _txtTag; }
        set { _txtTag = value; }
    }
    private string _headOfSection;

    public string HeadOfSection
    {
        get { return _headOfSection; }
        set { _headOfSection = value; }
    }
    private string _substituteHOS;

    public string SubstituteHOS
    {
        get { return _substituteHOS; }
        set { _substituteHOS = value; }
    }
    private string _txtStatus;

    public string TxtStatus
    {
        get { return _txtStatus; }
        set { _txtStatus = value; }
    }
}