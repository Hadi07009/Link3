using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TaxChallan
/// </summary>
public class Overtime
{
    public Overtime()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string _employeeCode;

    public string EmployeeCode
    {
        get { return _employeeCode; }
        set { _employeeCode = value; }
    }
    private string _challanDate;

    public string ChallanDate
    {
        get { return _challanDate; }
        set { _challanDate = value; }
    }
    private string _challanForMonth;

    public string ChallanForMonth
    {
        get { return _challanForMonth; }
        set { _challanForMonth = value; }
    }
    private string _challanNumber;

    public string ChallanNumber
    {
        get { return _challanNumber; }
        set { _challanNumber = value; }
    }
    private decimal _challamAmount;

    public decimal ChallamAmount
    {
        get { return _challamAmount; }
        set { _challamAmount = value; }
    }

    public string ChallanDateSearch { get; set; }
    public string Paytype { get; set; }
}