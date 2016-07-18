using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LunchBillApplication
/// </summary>
public class LunchBillApplication : VariousProcessApplication
{
	public LunchBillApplication()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string _locationDuringLunch;

    public string LocationDuringLunch
    {
        get { return _locationDuringLunch; }
        set { _locationDuringLunch = value; }
    }
    private string _assignedBy;

    public string AssignedBy
    {
        get { return _assignedBy; }
        set { _assignedBy = value; }
    }
}