using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ManualLeaveEntry
/// </summary>
public class ManualLeaveEntry
{
	public ManualLeaveEntry()
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
    private string _leaveTypeCode;

    public string LeaveTypeCode
    {
        get { return _leaveTypeCode; }
        set { _leaveTypeCode = value; }
    }
    private float _availableLeaveNo;

    public float AvailableLeaveNo
    {
        get { return _availableLeaveNo; }
        set { _availableLeaveNo = value; }
    }
    private string _leaveStartDate;

    public string LeaveStartDate
    {
        get { return _leaveStartDate; }
        set { _leaveStartDate = value; }
    }
    private float _noOfDays;

    public float NoOfDays
    {
        get { return _noOfDays; }
        set { _noOfDays = value; }
    }
    private string _remarks;

    public string Remarks
    {
        get { return _remarks; }
        set { _remarks = value; }
    }
    private string txtTag;

    public string TxtTag
    {
        get { return txtTag; }
        set { txtTag = value; }
    }



    private string _address_During_Leave;

    public string Address_During_Leave
    {
        get { return _address_During_Leave; }
        set { _address_During_Leave = value; }
    }

    private string _contact_Number_during_Leave;

    public string Contact_Number_during_Leave
    {
        get { return _contact_Number_during_Leave; }
        set { _contact_Number_during_Leave = value; }
    }


    private string _responsible_Person_During_Leave;

    public string Responsible_Person_During_Leave
    {
        get { return _responsible_Person_During_Leave; }
        set { _responsible_Person_During_Leave = value; }
    }

    public string EntryUserID { get; set; }
    public DateTime LeaveDate { get; set; }




}