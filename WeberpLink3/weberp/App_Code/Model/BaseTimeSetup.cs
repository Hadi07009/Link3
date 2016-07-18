using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BaseTimeSetup
/// </summary>
public class BaseTimeSetup
{
	public BaseTimeSetup()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string _shiftCode;

    public string ShiftCode
    {
        get { return _shiftCode; }
        set {
            if (value == "-1")
            {
                throw new Exception("Please select shift.");
                
            }
            _shiftCode = value; }
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

    public int SlNumber { get; set; }
}