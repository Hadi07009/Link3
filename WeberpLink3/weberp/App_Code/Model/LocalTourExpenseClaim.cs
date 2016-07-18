using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LocalTourExpenseClaim
/// </summary>
public class LocalTourExpenseClaim : TourExpenseClaim
{
	public LocalTourExpenseClaim()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    
    private string _fromPlace;

    public string FromPlace
    {
        get { return _fromPlace; }
        set { _fromPlace = value; }
    }
    private string _toPlace;

    public string ToPlace
    {
        get { return _toPlace; }
        set { _toPlace = value; }
    }

}