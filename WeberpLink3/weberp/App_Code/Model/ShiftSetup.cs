using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ShiftSetup
/// </summary>
public class ShiftSetup
{
	public ShiftSetup()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string shiftCode;

    public string ShiftCode
    {
        get { return shiftCode; }
        set { shiftCode = value; }
    }
    private string shiftTitle;

    public string ShiftTitle
    {
        get { return shiftTitle; }
        set { shiftTitle = value; }
    }
    private string fromTime;

    public string FromTime
    {
        get { return fromTime; }
        set { fromTime = value; }
    }
    private string toTime;

    public string ToTime
    {
        get { return toTime; }
        set { toTime = value; }
    }
    private string totalHour;

    public string TotalHour
    {
        get { return totalHour; }
        set { totalHour = value; }
    }

    private int graceTime;

    public int GraceTime
    {
        get { return graceTime; }
        set { graceTime = value; }
    }

}