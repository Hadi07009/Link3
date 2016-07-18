using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AnnualStatement
/// </summary>
public class AnnualStatement
{
	public AnnualStatement()
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
    private DateTime _fromDate;

    public DateTime FromDate
    {
        get { return _fromDate; }
        set { _fromDate = value; }
    }
    private DateTime _toDate;

    public DateTime ToDate
    {
        get { return _toDate; }
        set { _toDate = value; }
    }
    private string _userID;

    public string UserID
    {
        get { return _userID; }
        set { _userID = value; }
    }
}