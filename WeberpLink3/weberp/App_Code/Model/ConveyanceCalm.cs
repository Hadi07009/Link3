using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ConveyanceCalm
/// </summary>
public class ConveyanceCalm
{
	public ConveyanceCalm()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private string _employeeId;
    private string _conveyanceType;
    private string _quantity;
    private string _unit;
    private string _rateCalculation;
    private string _rate;
    private string _amount;
    private string _paymentPeriodFromDate;
    private string _paymentPeriodToDate;

    public string EmployeeId
    {
        get { return _employeeId; }
        set { _employeeId = value; }
    }

    public string ConveyanceType
    {
        get { return _conveyanceType; }
        set { _conveyanceType = value; }
    }

    public string Quantity
    {
        get { return _quantity; }
        set { _quantity = value; }
    }

    public string Unit
    {
        get { return _unit; }
        set { _unit = value; }
    }

    public string RateCalculation
    {
        get { return _rateCalculation; }
        set { _rateCalculation = value; }
    }

    public string Rate
    {
        get { return _rate; }
        set { _rate = value; }
    }

    public string Amount
    {
        get { return _amount; }
        set { _amount = value; }
    }

    public string PaymentPeriodFromDate
    {
        get { return _paymentPeriodFromDate; }
        set { _paymentPeriodFromDate = value; }
    }

    public string PaymentPeriodToDate
    {
        get { return _paymentPeriodToDate; }
        set { _paymentPeriodToDate = value; }
    }
}   