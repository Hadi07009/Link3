using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for JobTypeSetup
/// </summary>
public class JobTypeSetup
{
	public JobTypeSetup()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string _jobTypeCode;

    public string JobTypeCode
    {
        get { return _jobTypeCode; }
        set { _jobTypeCode = value; }
    }
    private string _jobTypeTitle;

    public string JobTypeTitle
    {
        get { return _jobTypeTitle; }
        set { _jobTypeTitle = value; }
    }
    private string _txtTag;

    public string TxtTag
    {
        get { return _txtTag; }
        set { _txtTag = value; }
    }
}