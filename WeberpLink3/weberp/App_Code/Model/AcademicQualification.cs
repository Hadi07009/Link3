using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AcademicQualification
/// </summary>
public class AcademicQualification
{
	public AcademicQualification()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private string nameOfDegree;
    private string institutionName;
    private string board;
    private string resultGrade;
    private double passingYear;
    private double courseDuration;
    private string majorInGroup;

    public string MajorInGroup
    {
        get { return majorInGroup; }
        set { majorInGroup = value; }
    }

    public string NameOfDegree
    {
        get { return nameOfDegree; }
        set { nameOfDegree = value; }
    }

    public string InstitutionName
    {
        get { return institutionName; }
        set { institutionName = value; }
    }

    public string Board
    {
        get { return board; }
        set { board = value; }
    }

    public string ResultGrade
    {
        get { return resultGrade; }
        set { resultGrade = value; }
    }

    public double PassingYear
    {
        get { return passingYear; }
        set { passingYear = value; }
    }

    public double CourseDuration
    {
        get { return courseDuration; }
        set { courseDuration = value; }
    }

    public string DocumentCode { get; set; }
}