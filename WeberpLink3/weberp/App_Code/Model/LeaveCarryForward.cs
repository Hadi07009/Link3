using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LeaveCarryForward
/// </summary>
public class LeaveCarryForward
{
    public LeaveCarryForward()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private string _employeeCode;

    public string EmployeeCode
    {
        get { return _employeeCode; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("Must Enter" + " Employee ID ");
            }
            _employeeCode = value;
        }
    }
    private string _leaveType;

    public string LeaveType
    {
        get { return _leaveType; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("Please select leave  type ");
            } 
            _leaveType = value; }
    }
    private string _selectedDate;

    public string SelectedDate
    {
        get { return _selectedDate; }
        set { _selectedDate = value; }
    }


    private float _noofLeave;

    public float NoofLeave
    {
        get { return _noofLeave; }
        set { _noofLeave = value; }
    }
    private string _entryUser;

    public string EntryUser
    {
        get { return _entryUser; }
        set { _entryUser = value; }
    }
    private string _entryDate;

    public string EntryDate
    {
        get { return _entryDate; }
        set { _entryDate = value; }
    }
}