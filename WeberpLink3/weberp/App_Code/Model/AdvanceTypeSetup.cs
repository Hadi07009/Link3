using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AdvanceTypeSetup
/// </summary>
public class AdvanceTypeSetup
{
	public AdvanceTypeSetup()
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
    private string advanceCode;

    public string AdvanceCode
    {
        get { return advanceCode; }
        set { advanceCode = value; }
    }
    private string advanceName;

    public string AdvanceName
    {
        get { return advanceName; }
        set { advanceName = value; }
    }
    private double minimumAmount;

    public double MinimumAmount
    {
        get { return minimumAmount; }
        set { minimumAmount = value; }
    }
    private double maximumAmount;

    public double MaximumAmount
    {
        get { return maximumAmount; }
        set { maximumAmount = value; }
    }
    private string modeOfPayment;

    public string ModeOfPayment
    {
        get { return modeOfPayment; }
        set { modeOfPayment = value; }
    }
    private int frequency;

    public int Frequency
    {
        get { return frequency; }
        set { frequency = value; }
    }
    private string txtTag;

    public string TxtTag
    {
        get { return txtTag; }
        set { txtTag = value; }
    }
}