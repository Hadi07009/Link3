using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ManualAttendance
/// </summary>
public class ManualAttendance
{
	public ManualAttendance()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string _dateForAttendance;

    public string DateForAttendance
    {
        get { return _dateForAttendance; }
        set { _dateForAttendance = value; }
    }
    private string _employeeCode;

    public string EmployeeCode
    {
        get { return _employeeCode; }
        set { _employeeCode = value; }
    }
    private string _inTime;

    public string InTime
    {
        get { return _inTime; }
        set { _inTime = value; }
    }
    private string _outTime;

    public string OutTime
    {
        get { return _outTime; }
        set { _outTime = value; }
    }
    private string _hours;

    public string Hours
    {
        get { return _hours; }
        set { _hours = value; }
    }
    private string _remarks;

    public string Remarks
    {
        get { return _remarks; }
        set { _remarks = value; }
    }
    private string _txtTag;

    public string TxtTag
    {
        get { return _txtTag; }
        set { _txtTag = value; }
    }
}