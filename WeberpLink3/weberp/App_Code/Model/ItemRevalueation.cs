using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ItemRevalueation
/// </summary>
public class ItemRevalueation
{
	public ItemRevalueation()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string _trnDate;

    public string TrnDate
    {
        get { return _trnDate; }
        set { _trnDate = value; }
    }
    private string _itemCode;

    public string ItemCode
    {
        get { return _itemCode; }
        set { _itemCode = value; }
    }
    private string _trackingNo;

    public string TrackingNo
    {
        get { return _trackingNo; }
        set { _trackingNo = value; }
    }
    private int _lineNumber;

    public int LineNumber
    {
        get { return _lineNumber; }
        set { _lineNumber = value; }
    }
    private string _trnType;

    public string TrnType
    {
        get { return _trnType; }
        set { _trnType = value; }
    }
    private Decimal _trnAmount;

    public Decimal TrnAmount
    {
        get { return _trnAmount; }
        set { _trnAmount = value; }
    }
    private string _entryUser;

    public string EntryUser
    {
        get { return _entryUser; }
        set { _entryUser = value; }
    }
    private string _txtTag;

    public string TxtTag
    {
        get { return _txtTag; }
        set { _txtTag = value; }
    }

    private string _description;

    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }
}