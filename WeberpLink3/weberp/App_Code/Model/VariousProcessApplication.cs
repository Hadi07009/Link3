using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VariousProcessApplication
/// </summary>
public class VariousProcessApplication
{
	public VariousProcessApplication()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private DateTime _dateClaim;

    public DateTime DateClaim
    {
        get { return _dateClaim; }
        set { _dateClaim = value; }
    }
    
    private string _purposeofClaim;

    public string PurposeofClaim
    {
        get { return _purposeofClaim; }
        set { _purposeofClaim = value; }
    }
    private decimal _amountCost;

    public decimal AmountCost
    {
        get { return _amountCost; }
        set { _amountCost = value; }
    }
    private string _transactionNo;

    public string TransactionNo
    {
        get { return _transactionNo; }
        set { _transactionNo = value; }
    }
    private int _transactionNoLineNo;

    public int TransactionNoLineNo
    {
        get { return _transactionNoLineNo; }
        set { _transactionNoLineNo = value; }
    }

    private string _applicantCode;

    public string ApplicantCode
    {
        get { return _applicantCode; }
        set { _applicantCode = value; }
    }
    private string _entryUser;

    public string EntryUser
    {
        get { return _entryUser; }
        set { _entryUser = value; }
    }
    public string ProcessCode { get; set; }

    public string ProcessFlowCode { get; set; }
    public int ProcessLevelCode { get; set; }
    public string ProcessTypeCode { get; set; }
    public string ActingPersonCode { get; set; }

    
}