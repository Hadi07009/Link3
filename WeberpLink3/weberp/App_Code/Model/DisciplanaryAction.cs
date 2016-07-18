
using System;
using System.Activities.Statements;

/// <summary>
/// Summary description for DisciplanaryAction
/// </summary>
public class DisciplanaryAction
{
    public DisciplanaryAction()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private string _caseCode;
    private string _employeeCode;
    private string _caseTopic;
    private string _caseDate;
    private string _inquary;
    private string _inquaryRecomondation;
    private string _caseAction;
    private string _remarks;

    public string CaseCode
    {
        get { return _caseCode; }
        set { _caseCode = value; }
    }



    public string EmployeeCode
    {
        get { return _employeeCode; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("Must Enter" + " Employee Code ");
            }
            _employeeCode = value;
        }
    }

    public string CaseTopic
    {
        get { return _caseTopic; }
        set { _caseTopic = value; }
    }

    public string CaseDate
    {
        get { return _caseDate; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("Must Enter" + " Date ");
            }
            _caseDate = value;
        }
    }

    public string Inquary
    {
        get { return _inquary; }
        set { _inquary = value; }
    }

    public string InquaryRecomondation
    {
        get { return _inquaryRecomondation; }
        set { _inquaryRecomondation = value; }
    }

    public string CaseAction
    {
        get { return _caseAction; }
        set { _caseAction = value; }
    }

    public string Remarks
    {
        get { return _remarks; }
        set { _remarks = value; }
    }

    public string EntryUser { get; set; }
    public string Status { get; set; }
}
