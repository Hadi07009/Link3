using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ForeignTourExpenseClaim
/// </summary>
public class ForeignTourExpenseClaim : TourExpenseClaim
{
	public ForeignTourExpenseClaim()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string _placeofTour;

    public string PlaceofTour
    {
        get { return _placeofTour; }
        set { _placeofTour = value; }
    }
    private string _country;

    public string Country
    {
        get { return _country; }
        set { _country = value; }
    }
    private string _vendorTour;

    public string VendorTour
    {
        get { return _vendorTour; }
        set { _vendorTour = value; }
    }
    private string _departureFlight;

    public string DepartureFlight
    {
        get { return _departureFlight; }
        set { _departureFlight = value; }
    }
    private string _arrivalFlight;

    public string ArrivalFlight
    {
        get { return _arrivalFlight; }
        set { _arrivalFlight = value; }
    }
    private float _durationDaysTour;

    public float DurationDaysTour
    {
        get { return _durationDaysTour; }
        set { _durationDaysTour = value; }
    }
}