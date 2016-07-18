using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EmployeeInvestAmount
/// </summary>
public class EmployeeInvestAmount
{
	public EmployeeInvestAmount()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string _referenceNumber;

    public string ReferenceNumber
    {
        get { return _referenceNumber; }
        set { _referenceNumber = value; }
    }
    private string _financialYear;

    public string FinancialYear
    {
        get { return _financialYear; }
        set { _financialYear = value; }
    }
    private string _employeeCode;

    public string EmployeeCode
    {
        get { return _employeeCode; }
        set { _employeeCode = value; }
    }
    private decimal _investAmount;

    public decimal InvestAmount
    {
        get { return _investAmount; }
        set { _investAmount = value; }
    }
    private string _investDescription;

    public string InvestDescription
    {
        get { return _investDescription; }
        set { _investDescription = value; }
    }
}