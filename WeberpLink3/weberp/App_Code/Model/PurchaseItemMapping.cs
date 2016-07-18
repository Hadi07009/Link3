using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PurchaseItemMapping
/// </summary>
public class PurchaseItemMapping
{
	public PurchaseItemMapping()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int RefNo { get; set; }
    public string SupplierCode { get; set; }
    public string ItemCode { get; set; }
    public string ItemStatus { get; set; }
    public string EntryUserId { get; set; }
    public string EntryDate { get; set; }
}