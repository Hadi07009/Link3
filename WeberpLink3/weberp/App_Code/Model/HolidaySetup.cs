using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for HolidaySetup
/// </summary>
public class HolidaySetup
{
	public HolidaySetup()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string companyCode;

    public string CompanyCode
    {
        get { return companyCode; }
        set { companyCode = value; }
    }
    private string officeLocationCode;

    public string OfficeLocationCode
    {
        get { return officeLocationCode; }
        set { officeLocationCode = value; }
    }
    private string shiftID;

    public string ShiftID
    {
        get { return shiftID; }
        set { shiftID = value; }
    }
    private string holidayDate;

    public string HolidayDate
    {
        get { return holidayDate; }
        set { holidayDate = value; }
    }

    
    private string holidayDescription;

    public string HolidayDescription
    {
        get { return holidayDescription; }
        set { holidayDescription = value; }
    }
    private string loginUserID;

    public string LoginUserID
    {
        get { return loginUserID; }
        set { loginUserID = value; }
    }


    private string txtTag;

    public string TxtTag
    {
        get { return txtTag; }
        set { txtTag = value; }
    }
    private string employeeCode;

    public string EmployeeCode
    {
        get { return employeeCode; }
        set { employeeCode = value; }
    }
    private string configurationType;

    public string ConfigurationType
    {
        get { return configurationType; }
        set { configurationType = value; }
    }
    private string referenceNo;

    public string ReferenceNo
    {
        get { return referenceNo; }
        set { referenceNo = value; }
    }

    

}