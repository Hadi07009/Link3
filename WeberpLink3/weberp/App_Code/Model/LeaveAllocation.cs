using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LeaveAllocation
/// </summary>
public class LeaveAllocation
{
	public LeaveAllocation()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private string leaveType;

    public string LeaveType
    {
        get { return leaveType; }
        set { leaveType = value; }
    }
}