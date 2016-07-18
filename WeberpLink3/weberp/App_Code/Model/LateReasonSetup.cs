using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LateReasonSetup
/// </summary>
public class LateReasonSetup
{
	public LateReasonSetup()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string attendanceDate;

    public string AttendanceDate
    {
        get { return attendanceDate; }
        set { attendanceDate = value; }
    }
    private string shiftCode;

    public string ShiftCode
    {
        get { return shiftCode; }
        set { shiftCode = value; }
    }
    private string lateReason;

    public string LateReason
    {
        get { return lateReason; }
        set { lateReason = value; }
    }
    private string entryUserID;

    public string EntryUserID
    {
        get { return entryUserID; }
        set { entryUserID = value; }
    }
}