using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LeaveTypeSetup
/// </summary>
public class LeaveTypeSetup
{
	public LeaveTypeSetup()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string companyCode;

    public string CompanyCode
    {
        get { return companyCode; }
        set { companyCode = value; }
    }
    private string leaveCode;

    public string LeaveCode
    {
        get { return leaveCode; }
        set { leaveCode = value; }
    }
    private string leaveName;

    public string LeaveName
    {
        get { return leaveName; }
        set { leaveName = value; }
    }
    private string modeOfPayment;

    public string ModeOfPayment
    {
        get { return modeOfPayment; }
        set { modeOfPayment = value; }
    }
    private int maximumPerAllow;

    public int MaximumPerAllow
    {
        get { return maximumPerAllow; }
        set { maximumPerAllow = value; }
    }
    private string employeeType;

    public string EmployeeType
    {
        get { return employeeType; }
        set { employeeType = value; }
    }
    private string carryForwordNextYear;

    public string CarryForwordNextYear
    {
        get { return carryForwordNextYear; }
        set { carryForwordNextYear = value; }
    }
    private int maximumLeaveCarryForwordToNextYear;

    public int MaximumLeaveCarryForwordToNextYear
    {
        get { return maximumLeaveCarryForwordToNextYear; }
        set { maximumLeaveCarryForwordToNextYear = value; }
    }
    private string txtTag;

    public string TxtTag
    {
        get { return txtTag; }
        set { txtTag = value; }
    }
    private string _employeeGender;

    public string EmployeeGender
    {
        get { return _employeeGender; }
        set { _employeeGender = value; }
    }
}