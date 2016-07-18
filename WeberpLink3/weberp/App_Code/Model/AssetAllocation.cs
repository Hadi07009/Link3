using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AssetAllocation
/// </summary>
public class AssetAllocation
{
	public AssetAllocation()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string AssetName { get;  set; }
    public string AssetIdNo { get; set; }
    public string ActiveDate { get;  set; }
    public string InactiveDate { get;  set; }
    public string Status { get;  set; }
    public string AssetModelNo { get; set; }
    public string Description { get; set; }
    public string ReportingPersonId { get; set; }

    public string DocumentCode { get; set; }
}