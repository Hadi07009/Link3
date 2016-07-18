using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TaxSlabUpdate
/// </summary>
public class TaxSlabUpdate
{
	public TaxSlabUpdate()
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
    private DateTime _fromDate;

    public DateTime FromDate
    {
        get { return _fromDate; }
        set { _fromDate = value; }
    }
    private DateTime _toDate;

    public DateTime ToDate
    {
        get { return _toDate; }
        set { _toDate = value; }
    }
    private string _slabType;

    public string SlabType
    {
        get { return _slabType; }
        set { _slabType = value; }
    }
    private int _slab;

    public int Slab
    {
        get { return _slab; }
        set { _slab = value; }
    }
    private decimal _slabAmount;

    public decimal SlabAmount
    {
        get { return _slabAmount; }
        set { _slabAmount = value; }
    }
    private float _taxRate;

    public float TaxRate
    {
        get { return _taxRate; }
        set { _taxRate = value; }
    }
}