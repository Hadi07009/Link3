using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EmployeePromotion
/// </summary>
public class EmployeePromotion : EmployeeTransfer
{
	public EmployeePromotion()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string toDesignation;

    public string ToDesignation
    {
        get { return toDesignation; }
        set { toDesignation = value; }
    }
    private string remarks;

    public string Remarks
    {
        get { return remarks; }
        set { remarks = value; }
    }
    private string fromDesignation;

    public string FromDesignation
    {
        get { return fromDesignation; }
        set { fromDesignation = value; }
    }
}