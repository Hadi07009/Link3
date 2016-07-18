using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PayrollHold
/// </summary>
public class PayrollHold
{
	public PayrollHold()
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
    private string _reasonOfPayrollHold;

    public string ReasonOfPayrollHold
    {
        get { return _reasonOfPayrollHold; }
        set { _reasonOfPayrollHold = value; }
    }
    private string _userCode;

    public string UserCode
    {
        get { return _userCode; }
        set { _userCode = value; }
    }
    private string _txtTag;

    public string TxtTag
    {
        get { return _txtTag; }
        set { _txtTag = value; }
    }
    private int _autoNumberForUpdate;

    public int AutoNumberForUpdate
    {
        get { return _autoNumberForUpdate; }
        set { _autoNumberForUpdate = value; }
    }
}