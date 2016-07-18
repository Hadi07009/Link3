using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TaxChallan
/// </summary>
public class OtherAllowances
{
    public OtherAllowances()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string EmployeeCode { get; set; }   
    public DateTime ChallanDate { get; set; }
    public DateTime ChallanForMonth { get; set; }
    public string ChallanNumber { get; set; }
    public decimal ChallamAmount { get; set; }  
    public string ChallanDateSearch { get; set; }
    public string Paytype { get; set; }
}