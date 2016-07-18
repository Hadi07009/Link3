using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ConveyanceApplication
/// </summary>
public class ConveyanceApplication : VariousProcessApplication
{
    public ConveyanceApplication()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private string _fromLocation;

    public string FromLocation
    {
        get { return _fromLocation; }
        set { _fromLocation = value; }
    }
    private string _toLocation;

    public string ToLocation
    {
        get { return _toLocation; }
        set { _toLocation = value; }
    }
    private string _modeofJourney;

    public string ModeofJourney
    {
        get { return _modeofJourney; }
        set { _modeofJourney = value; }
    }

    private string _assignedByEmployee;

    public string AssignedByEmployee
    {
        get { return _assignedByEmployee; }
        set { _assignedByEmployee = value; }
    }


    //public string ActingPersonCode { get; set; }



    public int ActionTypeCode { get; set; }
}