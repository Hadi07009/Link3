using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TrainingRecord
{
    private string trainingCode;

    public string TrainingCode
    {
        get { return trainingCode; }
        set { trainingCode = value; }
    }
    private string nameOfInstitution;

    public string NameOfInstitution
    {
        get { return nameOfInstitution; }
        set { nameOfInstitution = value; }
    }
    private string institutionAddress;

    public string InstitutionAddress
    {
        get { return institutionAddress; }
        set { institutionAddress = value; }
    }
    private string startedDate;

    public string StartedDate
    {
        get { return startedDate; }
        set { startedDate = value; }
    }
    private string endDate;

    public string EndDate
    {
        get { return endDate; }
        set { endDate = value; }
    }
    private int trainingDuration;

    public int TrainingDuration
    {
        get { return trainingDuration; }
        set { trainingDuration = value; }
    }
    private double trainingFee;

    public double TrainingFee
    {
        get { return trainingFee; }
        set { trainingFee = value; }
    }
    private string trainingAchievement;

    public string TrainingAchievement
    {
        get { return trainingAchievement; }
        set { trainingAchievement = value; }
    }
    private string trainingTitle;

    public string TrainingTitle
    {
        get { return trainingTitle; }
        set { trainingTitle = value; }
    }
    private string certificateCode;

    public string CertificateCode
    {
        get { return certificateCode; }
        set { certificateCode = value; }
    }
    private string fundCode;

    public string FundCode
    {
        get { return fundCode; }
        set { fundCode = value; }
    }

    public string DocumentCode { get; set; }
}