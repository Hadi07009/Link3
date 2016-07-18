using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OthersPayment
/// </summary>
public class OthersPayment
{
	public OthersPayment()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private DateTime _firstDate;

    public DateTime FirstDate
    {
        get { return _firstDate; }
        set {
            _firstDate = value; }
    }


    private DateTime _lastDate;

    public DateTime LastDate
    {
        get { return _lastDate; }
        set {
            _lastDate = value; }
    }
}