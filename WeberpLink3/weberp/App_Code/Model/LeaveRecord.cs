using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LeaveRecord
/// </summary>
public class LeaveRecord
{
	public LeaveRecord()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private string _startDate;
    private string _endDate;
    private string _employeeCode;

    public string StartDate
    {
        get { return _startDate; }
        set
        {
            if (value == null)
            {
                throw new Exception("Must Enter" + " From Date ");
            }
            _startDate = value;
        }
    }

    public string EndDate
    {
        get { return _endDate; }
        set
        {
            if (value == null)
            {
                throw new Exception("Must Enter" + " To Date ");
            }
            _endDate = value;
        }
    }

    public string EmployeeCode
    {
        get { return _employeeCode; }
        set { _employeeCode = value; }
    }
}