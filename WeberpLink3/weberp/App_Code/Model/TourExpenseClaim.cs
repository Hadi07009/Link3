using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TourExpenseClaim
/// </summary>
public class TourExpenseClaim : ProcessAdvancePayment
{
	public TourExpenseClaim()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private DateTime _arrivalDate;

    public DateTime ArrivalDate
    {
        get { return _arrivalDate; }
        set { _arrivalDate = value; }
    }
    private string _arrivalTime;

    public string ArrivalTime
    {
        get { return _arrivalTime; }
        set { _arrivalTime = value; }
    }
    private string _departureTime;

    public string DepartureTime
    {
        get { return _departureTime; }
        set { _departureTime = value; }
    }
    private float _dailyAllowanceNoofDays;

    public float DailyAllowanceNoofDays
    {
        get { return _dailyAllowanceNoofDays; }
        set { _dailyAllowanceNoofDays = value; }
    }
    private float _accommodationNoofDays;

    public float AccommodationNoofDays
    {
        get { return _accommodationNoofDays; }
        set { _accommodationNoofDays = value; }
    }
    private DateTime _advanceReceivedDate;

    public DateTime AdvanceReceivedDate
    {
        get { return _advanceReceivedDate; }
        set { _advanceReceivedDate = value; }
    }
    private decimal _actualClaimAmount;

    public decimal ActualClaimAmount
    {
        get { return _actualClaimAmount; }
        set { _actualClaimAmount = value; }
    }
}