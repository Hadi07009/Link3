using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for QueryReportController
/// </summary>
public class QueryReportController
{
	public QueryReportController()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string SqlGetQuery()
    {
        return "SELECT DISTINCT DataSource,SourceName FROM TBL_SQL_SOURCE ORDER BY SourceName";
    }
    public string SqlGetReportName()
    {
        return "SELECT DISTINCT [SQL],SQLNAME FROM [TBL_SQL_STATEMENT] ORDER BY SQLNAME";
    }
    public DataTable GetColumn(string connectionString,QueryReport objQueryReport)
    {
        DataTable dtColumn = null;
        string sqlString = "SELECT DataFields FROM TBL_SQL_SOURCE WHERE SourceName = '" + objQueryReport.QueryText + "'";
        return dtColumn = DataProcess.GetData(connectionString,sqlString);
    }

    public void Save(string connectionString, QueryReport objQueryReport)
    {
        if (CheckReportName(connectionString,objQueryReport.ReportName.ToString()) == true)
        {
            throw new Exception("Report Name '" + objQueryReport.ReportName + "' Already Exist ");
        }

        string existingReport = CheckSQL(connectionString,objQueryReport.SelectedColumn.ToString());
        if (existingReport != null)
        {
            throw new Exception("SQL already exist as ' " + existingReport + " ' report name");
            
        }
        
        string sqlString = @"INSERT INTO TBL_SQL_STATEMENT ([SQL],SQLNAME,MODULENAME,[STATUS],ENTRYUSERID,ENTRYDATE) 
		VALUES('" + objQueryReport.SelectedColumn + "','" + objQueryReport.ReportName + "','HRMS',1,'"+objQueryReport.EntryUser+"',GETDATE()) ";
        DataProcess.InsertQuery(connectionString,sqlString);
    }

    public DataTable GetQueryData(string connectionString, QueryReport objQueryReport)
    {
        DataTable dtQueryData = null;
        string sqlString = objQueryReport.ReportName;
        return dtQueryData = DataProcess.GetData(connectionString,sqlString);
    }
    private bool CheckReportName(string connectinString,string reportName)
    {
        bool reportNameResult = false;
        string sqlString = "SELECT DISTINCT [SQLNAME] FROM [TBL_SQL_STATEMENT] WHERE SQLNAME = '"+reportName+"'";
        DataTable dtReportName = DataProcess.GetData(connectinString,sqlString);
        if (dtReportName.Rows.Count > 0)
        {
            reportNameResult = true;
        }

        return reportNameResult;
    }
    private string CheckSQL(string connectionString, string sqlQuery)
    {
        string reportName = null;
        string fullSQL = "SELECT SQLNAME FROM TBL_SQL_STATEMENT WHERE [SQL] = '"+sqlQuery+"'";
        var dtReportName = DataProcess.GetData(connectionString, fullSQL);
        foreach (DataRow row in dtReportName.Rows)
        {
            reportName = row["SQLNAME"].ToString();
        }
        return reportName;
    }
}