using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FormulaSetup
/// </summary>
public class FormulaSetup
{
	public FormulaSetup()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string _fCode;

    public string FCode
    {
        get { return _fCode; }
        set { _fCode = value; }
    }
    private string _fName;

    public string FName
    {
        get { return _fName; }
        set { _fName = value; }
    }
    private string _fBase;

    public string FBase
    {
        get { return _fBase; }
        set { _fBase = value; }
    }
    private string _fOperator;

    public string FOperator
    {
        get { return _fOperator; }
        set { _fOperator = value; }
    }
    private string _fMultiplier;

    public string FMultiplier
    {
        get { return _fMultiplier; }
        set { _fMultiplier = value; }
    }
    private string _fAccumulation;

    public string FAccumulation
    {
        get { return _fAccumulation; }
        set { _fAccumulation = value; }
    }
    private string _fAccumulationValue;

    public string FAccumulationValue
    {
        get { return _fAccumulationValue; }
        set { _fAccumulationValue = value; }
    }
    private string _fManualEntry;

    public string FManualEntry
    {
        get { return _fManualEntry; }
        set { _fManualEntry = value; }
    }
    private string _txtTag;

    public string TxtTag
    {
        get { return _txtTag; }
        set { _txtTag = value; }
    }
}