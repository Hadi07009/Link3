using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for QueryReport
/// </summary>
public class QueryReport
{
	public QueryReport()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string _queryText;

    public string QueryText
    {
        get { return _queryText; }
        set { _queryText = value; }
    }

    private string _selectedColumn;

    public string SelectedColumn
    {
        get { return _selectedColumn; }
        set {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("Please select column ");
            }
            _selectedColumn = value; }
    }
    
    private string _reportName;

    public string ReportName
    {
        get { return _reportName; }
        set {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("Report name did not correct");
            }
            _reportName = value; }
    }

    public string EntryUser { get; set; }
}