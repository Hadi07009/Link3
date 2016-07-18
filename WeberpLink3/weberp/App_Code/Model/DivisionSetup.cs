using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DivisionSetup
/// </summary>
public class DivisionSetup
{
	public DivisionSetup()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string _companyCode;

    public string CompanyCode
    {
        get { return _companyCode; }
        set { _companyCode = value; }
    }
    private string _divisionCode;

    public string DivisionCode
    {
        get { return _divisionCode; }
        set { _divisionCode = value; }
    }
    private string _divisionName;

    public string DivisionName
    {
        get { return _divisionName; }
        set { _divisionName = value; }
    }
    private string _location;

    public string Location
    {
        get { return _location; }
        set { _location = value; }
    }
    private string _address1;

    public string Address1
    {
        get { return _address1; }
        set { _address1 = value; }
    }
    private string _address2;

    public string Address2
    {
        get { return _address2; }
        set { _address2 = value; }
    }
    private string _address3;

    public string Address3
    {
        get { return _address3; }
        set { _address3 = value; }
    }
    private string _txtTag;

    public string TxtTag
    {
        get { return _txtTag; }
        set { _txtTag = value; }
    }
    private string _txtStatus;

    public string TxtStatus
    {
        get { return _txtStatus; }
        set { _txtStatus = value; }
    }
}