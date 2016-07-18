using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CheckinData
/// </summary>
public class CheckinData
{
	public CheckinData()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string _employeeCode;

    public string EmployeeCode
    {
        get { return _employeeCode; }
        set { _employeeCode = value; }
    }
    private DateTime _checkinDate;

    public DateTime CheckinDate
    {
        get { return _checkinDate; }
        set { _checkinDate = value; }
    }

}