using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EmployeeSettlement
/// </summary>
public class EmployeeSettlement : EmployeeInformation
{
	public EmployeeSettlement()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string SettlementType { get; set; }
    public string AcceptanceDate { get; set; }
    public int NoticePeriod { get; set; }
    public string RelievingDate { get; set; }
    public int CompensationDays { get; set; }
    public string CommentsOrReason { get; set; }
    public string TxtTag { get; set; }
    public string EntryUserId { get; set; }
}