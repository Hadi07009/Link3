using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GradeSetup
/// </summary>
public class GradeSetup
{
	public GradeSetup()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string _gradeCode;

    public string GradeCode
    {
        get { return _gradeCode; }
        set { _gradeCode = value; }
    }
    private string _gradeDescription;

    public string GradeDescription
    {
        get { return _gradeDescription; }
        set { _gradeDescription = value; }
    }
    private string _txtTag;

    public string TxtTag
    {
        get { return _txtTag; }
        set { _txtTag = value; }
    }
}