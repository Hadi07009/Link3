using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProfessionalQualification
/// </summary>
public class ProfessionalQualification
{
	public ProfessionalQualification()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private string nameofInstitution;
    private string institutionAddress;
    private string serviceStartDate;
    private string serviceEndDate;
    private string designation;
    private string serviceDescription;
    private double grossSalary;

    public string NameofInstitution
    {
        get { return nameofInstitution; }
        set { nameofInstitution = value; }
    }

    public string InstitutionAddress
    {
        get { return institutionAddress; }
        set { institutionAddress = value; }
    }

    public string ServiceStartDate
    {
        get { return serviceStartDate; }
        set { serviceStartDate = value; }
    }

    public string ServiceEndDate
    {
        get { return serviceEndDate; }
        set { serviceEndDate = value; }
    }

    public string Designation
    {
        get { return designation; }
        set { designation = value; }
    }

    public string ServiceDescription
    {
        get { return serviceDescription; }
        set { serviceDescription = value; }
    }

    public double GrossSalary
    {
        get { return grossSalary; }
        set { grossSalary = value; }
    }

    public string DocumentCode { get; set; }
}