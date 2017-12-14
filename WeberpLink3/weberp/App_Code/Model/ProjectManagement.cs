using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProjectManagement
/// </summary>
public class ProjectManagement
{
	public ProjectManagement()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string _responsibleID;

    public string ResponsibleID
    {
        get { return _responsibleID; }
        set {
            if (value == null)
            {
                throw new Exception(" Responsible ID is required ");
            }
            _responsibleID = value; }
    }
    private DataTable _projectList;

    public DataTable ProjectList
    {
        get { return _projectList; }
        set { _projectList = value; }
    }
    private int _parentActivityID;

    public int ParentActivityID
    {
        get { return _parentActivityID; }
        set { _parentActivityID = value; }
    }
    private DataTable _subProjectList;

    public DataTable SubProjectList
    {
        get { return _subProjectList; }
        set { _subProjectList = value; }
    }
    private int _activityID;

    public int ActivityID
    {
        get { return _activityID; }
        set { _activityID = value; }
    }
    private string _activityName;

    public string ActivityName
    {
        get { return _activityName; }
        set {
            if (value == null)
            {
                throw new Exception("Activity is required ");
                
            } _activityName = value;
        }
    }
    private string _activityDescription;

    public string ActivityDescription
    {
        get { return _activityDescription; }
        set { _activityDescription = value; }
    }
    private string _accountableID;

    public string AccountableID
    {
        get { return _accountableID; }
        set { _accountableID = value; }
    }
    private string _consultedID;

    public string ConsultedID
    {
        get { return _consultedID; }
        set { _consultedID = value; }
    }
    private string _informedID;

    public string InformedID
    {
        get { return _informedID; }
        set { _informedID = value; }
    }
    private DateTime? _dueDate;

    public DateTime? DueDate
    {
        get { return _dueDate; }
        set {
            if (value == null)
            {
                throw new Exception("Due Date is required ");
            } _dueDate = value;
        }
    }

    
    private string _entryUserId;

    public string EntryUserId
    {
        get { return _entryUserId; }
        set { _entryUserId = value; }
    }
    private int? _priorityID;

    public int? PriorityID
    {
        get { return _priorityID; }
        set {
            if (value == null)
            {
                throw new Exception("Priority is required ");
            } _priorityID = value;
        }
    }
    private string _description;

    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }
    private string _logisticesRequired;

    public string LogisticesRequired
    {
        get { return _logisticesRequired; }
        set { _logisticesRequired = value; }
    }
    private decimal? _relatedCosts;

    public decimal? RelatedCosts
    {
        get { return _relatedCosts; }
        set { _relatedCosts = value; }
    }

    private string _anyIssue;

    public string AnyIssue
    {
        get { return _anyIssue; }
        set { _anyIssue = value; }
    }
    private string _remarks;

    public string Remarks
    {
        get { return _remarks; }
        set { _remarks = value; }
    }
    private string _dataStatus;

    public string DataStatus
    {
        get { return _dataStatus; }
        set { _dataStatus = value; }
    }
    private int _seqNo;

    public int SeqNo
    {
        get { return _seqNo; }
        set {
            if (value == 0)
            {
                throw new Exception("SeqNo is required ");
            } _seqNo = value;
        }
    }
    private int _tierNo;

    public int TierNo
    {
        get { return _tierNo; }
        set {
            if (value == 0)
            {
                throw new Exception("TierNo is required");
            } _tierNo = value;
        }
    }
    private int _serialNo;

    public int SerialNo
    {
        get { return _serialNo; }
        set {
            if (value == 0)
            {
                throw new Exception("SerialNo is required");
            } _serialNo = value;
        }
    }
    private int _rootNo;

    public int RootNo
    {
        get { return _rootNo; }
        set {
            if (value == 0)
            {
                throw new Exception("RootNo is required");
            } _rootNo = value;
        }
    }
    private DataTable _activityDetails;

    public DataTable ActivityDetails
    {
        get { return _activityDetails; }
        set { _activityDetails = value; }
    }
    private string _treePath;

    public string TreePath
    {
        get { return _treePath; }
        set { _treePath = value; }
    }
    private int _projectID;

    public int ProjectID
    {
        get { return _projectID; }
        set { _projectID = value; }
    }
    private int? _activityStatusID;

    public int? ActivityStatusID
    {
        get { return _activityStatusID; }
        set {
            if (value == null)
            {
             throw new Exception("Status is required");   
            } _activityStatusID = value;
        }
    }
    private DataTable _activityLog;

    public DataTable ActivityLog
    {
        get { return _activityLog; }
        set { _activityLog = value; }
    }
    
  
}