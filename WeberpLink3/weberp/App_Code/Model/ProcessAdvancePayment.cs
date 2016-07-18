using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProcessAdvancePayment
/// </summary>
public class ProcessAdvancePayment : VariousProcessApplication
{
	public ProcessAdvancePayment()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private decimal _advanceReceived;

    public decimal AdvanceReceived
    {
        get { return _advanceReceived; }
        set { _advanceReceived = value; }
    }
    private decimal _netClaim;

    public decimal NetClaim
    {
        get { return _netClaim; }
        set { _netClaim = value; }
    }
    private string _expenditureArea;

    public string ExpenditureArea
    {
        get { return _expenditureArea; }
        set { _expenditureArea = value; }
    }

}