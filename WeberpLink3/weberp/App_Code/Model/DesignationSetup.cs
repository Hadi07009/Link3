using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DesignationSetup
/// </summary>
public class DesignationSetup
{
    
	public DesignationSetup()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string _designationCode;

    public string DesignationCode
    {
        get { return _designationCode; }
        set { _designationCode = value; }
    }

    
    private string _designation;

    public string Designation
    {
        get { return _designation; }
        set { _designation = value; }
    }

    
    private string _jobType;

    public string JobType
    {
        get { return _jobType; }
        set { _jobType = value; }
    }

    
    private string _mngLevel;

    public string MngLevel
    {
        get { return _mngLevel; }
        set { _mngLevel = value; }
    }

    
    private string _employeeType;

    public string EmployeeType
    {
        get { return _employeeType; }
        set { _employeeType = value; }
    }
    private string _txtTag;

    public string TxtTag
    {
        get { return _txtTag; }
        set { _txtTag = value; }
    }
    private string _txtStatus;

    public string TxtStatus
    {
        get { return _txtStatus; }
        set { _txtStatus = value; }
    }
}