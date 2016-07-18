using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AdvanceDetailsEntry
/// </summary>
public class AdvanceDetailsEntry : AdvanceTypeSetup
{
	public AdvanceDetailsEntry()
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
    private double _advanceAmount;

    public double AdvanceAmount
    {
        get { return _advanceAmount; }
        set { _advanceAmount = value; }
    }
    private float _installmentNo;
    private float _installmentSize;

    public float InstallmentNo
    {
        get { return _installmentNo; }
        set { _installmentNo = value; }
    }
    private string _advanceTakenDate;
    private string   _deductionStartDate;
    private string  _deductionEndDate;
    public string AdvanceTakenDate
    {
        get { return _advanceTakenDate; }
        set { _advanceTakenDate = value; }
    }

    public string DeductionStartDate
    {
        get { return _deductionStartDate; }
        set { _deductionStartDate = value; }
    }

    public string DeductionEndDate
    {
        get { return _deductionEndDate; }
        set { _deductionEndDate = value; }
    }

    public float InstallmentSize
    {
        get { return _installmentSize; }
        set { _installmentSize = value; }
    }

    public string EntryUserId
    {
        get { return _entryUserId; }
        set { _entryUserId = value; }
    }

    public string ReferenceNo
    {
        get { return _referenceNo; }
        set { _referenceNo = value; }
    }

    private string _entryUserId;
    private string _referenceNo;
}