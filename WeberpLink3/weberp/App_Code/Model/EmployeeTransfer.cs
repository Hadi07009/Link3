using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EmployeeTransfer
/// </summary>
public class EmployeeTransfer : EmployeeInformation
{
	public EmployeeTransfer()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string toDepartment;

    public string ToDepartment
    {
        get { return toDepartment; }
        set { toDepartment = value; }
    }
    private string toSection;

    public string ToSection
    {
        get { return toSection; }
        set { toSection = value; }
    }
    private string toOfficeLocation;

    public string ToOfficeLocation
    {
        get { return toOfficeLocation; }
        set { toOfficeLocation = value; }
    }
    private string actionType;

    public string ActionType
    {
        get { return actionType; }
        set { actionType = value; }
    }
    private string entryUserID;

    public string EntryUserID
    {
        get { return entryUserID; }
        set { entryUserID = value; }
    }
    private string transferredDate;

    public string TransferredDate
    {
        get { return transferredDate; }
        set { transferredDate = value; }
    }
    private int rowNo;

    public int RowNo
    {
        get { return rowNo; }
        set { rowNo = value; }
    }
    private string txtTag;

    public string TxtTag
    {
        get { return txtTag; }
        set { txtTag = value; }
    }
    private string fromDepartment;

    public string FromDepartment
    {
        get { return fromDepartment; }
        set { fromDepartment = value; }
    }
    private string fromOfficeLocation;

    public string FromOfficeLocation
    {
        get { return fromOfficeLocation; }
        set { fromOfficeLocation = value; }
    }
    private string fromSection;

    public string FromSection
    {
        get { return fromSection; }
        set { fromSection = value; }
    }

}