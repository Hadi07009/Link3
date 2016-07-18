using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AttendanceIndividualView
/// </summary>
public class AttendanceIndividualView
{
	public AttendanceIndividualView()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string _targetDate;

    public string TargetDate
    {
        get { return _targetDate; }
        set { _targetDate = value; }
    }
    private string _dayName;

    public string DayName
    {
        get { return _dayName; }
        set { _dayName = value; }
    }
    private string _description;

    public string Description
    {
        get { return _description; }
        set { _description = value; }
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
    private string _lateBy;

    public string LateBy
    {
        get { return _lateBy; }
        set { _lateBy = value; }
    }
    private string _earlyBy;

    public string EarlyBy
    {
        get { return _earlyBy; }
        set { _earlyBy = value; }
    }
    private string _Remarks;

    public string Remarks
    {
        get { return _Remarks; }
        set { _Remarks = value; }
    }
    private string _userId;

    public string UserId
    {
        get { return _userId; }
        set { _userId = value; }
    }
    private string _workingHour;

    public string WorkingHour
    {
        get { return _workingHour; }
        set { _workingHour = value; }
    }
    private string _extraHour;

    public string ExtraHour
    {
        get { return _extraHour; }
        set { _extraHour = value; }
    }
    private string _lessHour;

    public string LessHour
    {
        get { return _lessHour; }
        set { _lessHour = value; }
    }
}