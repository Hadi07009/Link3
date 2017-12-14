using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ProjectManagementController
/// </summary>
public class ProjectManagementController
{
    public ProjectManagementController()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable GetProjectList(string connectionString, ProjectManagement objProjectManagement)
    {
        try
        {
            string sql = null;
            DataTable dtProject = new DataTable();
            sql = @" ;WITH items AS (
                SELECT  A.ParentActivityID,CAST(A.[ActivityID] AS VARCHAR(255)) AS [ActivityID]				   
                  ,SPACE(3*A.TierNo)+A.[ActivityName] AS [ActivityName]       
                  ,A.[ResponsibleID]
                  ,A.[AccountableID]
                  ,A.[ConsultedID]
                  ,A.[InformedID]      
                  ,A.[DueDate]
                  ,B.PriorityText
                  ,A.PriorityID
                  ,A.TierNo
                  ,A.RootNo
                  ,A.SeqNo
                  ,A.SerialNo 
                  ,CAST(A.[ActivityID] AS VARCHAR(255)) AS TreePath
              FROM [tblProjectManagement] A 
              INNER JOIN tblPriority B ON A.PriorityID = B.PriorityID
              WHERE A.[DataStatus] = 'A' AND ( A.[ResponsibleID] = '" + objProjectManagement.ResponsibleID + "' OR A.AccountableID = '" + objProjectManagement.ResponsibleID + "') "
              + @" UNION ALL  
              SELECT  D.ParentActivityID,CAST(D.[ActivityID] AS VARCHAR(255)) AS [ActivityID]				   
                  ,REPLACE(SPACE(3*D.TierNo) , SPACE(1), '&nbsp;')+D.[ActivityName] AS [ActivityName]
                  ,D.[ResponsibleID]
                  ,D.[AccountableID]
                  ,D.[ConsultedID]
                  ,D.[InformedID]      
                  ,D.[DueDate]
                  ,E.PriorityText
                  ,D.PriorityID
                  ,D.TierNo
                  ,D.RootNo
                  ,D.SeqNo
                  ,D.SerialNo
                  ,CAST(TreePath + '.' + CAST(D.[ActivityID] AS VARCHAR(255)) AS VARCHAR(255)) AS TreePath                   
              FROM [tblProjectManagement] D               
              INNER JOIN items itms ON itms.ActivityID = D.ParentActivityID
              INNER JOIN tblPriority E ON D.PriorityID = E.PriorityID
                WHERE D.[DataStatus] = 'A'
              )
            SELECT * FROM items ORDER BY TreePath ";
            dtProject = DataProcess.GetData(connectionString, sql);
            if (dtProject != null)
            {
                dtProject = GetDataProperFormat(connectionString, dtProject, objProjectManagement);
            }

            return dtProject;
        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }

    private DataTable GetDataProperFormat(string connectionString, DataTable dtProject, ProjectManagement objProjectManagement)
    {
        try
        {
            DataTable dtProjectTemp = new DataTable();
            DeleteGarbageData(connectionString, objProjectManagement);
            foreach (DataRow rowNo in dtProject.Rows)
            {
                ProjectManagement objProjectManagementTemp = new ProjectManagement();
                objProjectManagementTemp.ParentActivityID = Convert.ToInt32(rowNo["ParentActivityID"].ToString());
                objProjectManagementTemp.ActivityID = Convert.ToInt32(rowNo["ActivityID"].ToString());
                objProjectManagementTemp.EntryUserId = objProjectManagement.EntryUserId;
                objProjectManagementTemp.TreePath = rowNo["TreePath"].ToString();
                if (CheckActivityID(connectionString, objProjectManagementTemp) == false)
                {
                    this.SaveTemp(connectionString, objProjectManagementTemp);
                }

            }

            dtProjectTemp = GetActualProjectList(connectionString, objProjectManagement);
            return dtProjectTemp;

        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }

    private bool CheckActivityID(string connectionString, ProjectManagement objProjectManagement)
    {
        bool activeIDTemp = false;
        string sql = null;
        DataTable dtActiveIDTemp = null;
        sql = @"SELECT count( [ActivityID]) as ActivityID FROM tblProjectManagementTemp WHERE [ActivityID] = " + objProjectManagement.ActivityID + " AND EntryUserId = '" + objProjectManagement.EntryUserId + "'";
        dtActiveIDTemp = DataProcess.GetData(connectionString, sql);
        foreach (DataRow rowNo in dtActiveIDTemp.Rows)
        {
            if (Convert.ToInt32(rowNo["ActivityID"].ToString()) != 0)
            {
                activeIDTemp = true;
            }
        }

        return activeIDTemp;
    }

    private DataTable GetActualProjectList(string connectionString, ProjectManagement objProjectManagement)
    {
        try
        {
            string sql = null;
            DataTable dtProject = new DataTable();
            sql = @" SELECT  D.ParentActivityID,CAST(D.[ActivityID] AS VARCHAR(255)) AS [ActivityID]				   
                  ,REPLACE(SPACE(3*F.TierNo) , SPACE(1), '&nbsp;')+F.[ActivityName] AS [ActivityName]
                  ,F.[ResponsibleID]
                  ,F.[AccountableID]
                  ,F.[ConsultedID]
                  ,F.[InformedID]      
                  ,F.[DueDate]
                  ,E.PriorityText
                  ,F.PriorityID
                  ,F.TierNo
                  ,F.RootNo
                  ,F.SeqNo
                  ,F.SerialNo
                  ,D.TreePath                   
              FROM tblProjectManagementTemp D   
              INNER JOIN [tblProjectManagement] F ON D.ParentActivityID = F.ParentActivityID AND D.[ActivityID] = F.ActivityID          
              INNER JOIN tblPriority E ON F.PriorityID = E.PriorityID
                WHERE D.EntryUserId = '" + objProjectManagement.EntryUserId + "' ORDER BY F.RootNo,D.TreePath ";
            return dtProject = DataProcess.GetData(connectionString, sql);
        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }

    private void DeleteGarbageData(string connectionString, ProjectManagement objProjectManagement)
    {
        try
        {
            string sql = null;
            sql = @"DELETE FROM tblProjectManagementTemp WHERE EntryUserId = '" + objProjectManagement.EntryUserId + "'";
            DataProcess.DeleteQuery(connectionString, sql);
        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }

    public DataTable GetSubProjectList(string connectionString, ProjectManagement objProjectManagement)
    {
        try
        {
            string sql = null;
            DataTable dtSubProject = new DataTable();
            sql = @"SELECT A.[ActivityID]
                  ,A.[ActivityName]      
                  ,A.[ResponsibleID]
                  ,A.[AccountableID]
                  ,A.[ConsultedID]
                  ,A.[InformedID]      
                  ,A.[DueDate]    
                  ,B.PriorityText
                  ,A.PriorityID 
                  ,A.[Description]
				  ,A.[LogisticesRequired]
				  ,A.[RelatedCosts]
				  ,A.[AnyIssue]
				  ,A.[Remarks]   
              FROM [tblProjectManagement] A 
              INNER JOIN tblPriority B ON A.PriorityID = B.PriorityID
              WHERE A.[DataStatus] = 'A' AND A.[ParentActivityID] = " + objProjectManagement.ParentActivityID + " ORDER BY A.PriorityID ,A.[ActivityID]";
            dtSubProject = DataProcess.GetData(connectionString, sql);
            return dtSubProject;
        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }
    public DataTable GetActivityDetails(string connectionString, ProjectManagement objProjectManagement)
    {
        try
        {
            string sql = null;
            DataTable dtSubProject = new DataTable();
            sql = @"SELECT A.[ActivityID]
                  ,A.[ActivityName]      
                  ,A.[ResponsibleID]
                  ,A.[AccountableID]
                  ,A.[ConsultedID]
                  ,A.[InformedID]      
                  ,A.[DueDate]    
                  ,B.PriorityText
                  ,A.PriorityID 
                  ,A.[Description]
				  ,A.[LogisticesRequired]
				  ,A.[RelatedCosts]
				  ,A.[AnyIssue]
				  ,A.[Remarks]   
				  ,A.TierNo,
				   A.RootNo
              FROM [tblProjectManagement] A 
              INNER JOIN tblPriority B ON A.PriorityID = B.PriorityID
              WHERE A.[DataStatus] = 'A' AND A.[ActivityID] = " + objProjectManagement.ParentActivityID + "";
            dtSubProject = DataProcess.GetData(connectionString, sql);
            return dtSubProject;
        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }

    public void Save(string connectionString, ProjectManagement objProjectManagement)
    {
        try
        {
            objProjectManagement.DataStatus = "A";
            objProjectManagement.ActivityID = GetActivityID(connectionString);
            string sql = null;
            sql = @"INSERT INTO [tblProjectManagement]
           ([ParentActivityID]
           ,[ActivityID]
           ,[ActivityName]           
           ,[ResponsibleID]
           ,[AccountableID]
           ,[ConsultedID]
           ,[InformedID]           
           ,[DueDate]           
           ,[EntryUserId]
           ,[EntryDate]
           ,[PriorityID]
           ,[Description]
           ,[LogisticesRequired]
           ,[RelatedCosts]
           ,[AnyIssue]
           ,[Remarks] 
           ,[DataStatus]
           ,[SeqNo]
           ,[TierNo]
           ,[SerialNo]
           ,[RootNo]
           )
         VALUES
               (" + objProjectManagement.ParentActivityID + ""
           + "," + objProjectManagement.ActivityID + ""
           + ",'" + objProjectManagement.ActivityName + "'"
           + ",'" + objProjectManagement.ResponsibleID + "'"
           + ",'" + objProjectManagement.AccountableID + "'"
           + ",'" + objProjectManagement.ConsultedID + "'"
           + ",'" + objProjectManagement.InformedID + "'"
           + ",CONVERT(DATETIME,'" + objProjectManagement.DueDate + "',103)"
           + ",'" + objProjectManagement.EntryUserId + "'"
           + "," + "CAST(GETDATE() AS DateTime)" + ""
           + "," + objProjectManagement.PriorityID + ""
           + ",'" + objProjectManagement.Description + "'"
           + ",'" + objProjectManagement.LogisticesRequired + "'"
           + "," + objProjectManagement.RelatedCosts + ""
           + ",'" + objProjectManagement.AnyIssue + "'"
           + ",'" + objProjectManagement.Remarks + "'"
           + ",'" + objProjectManagement.DataStatus + "'"
           + "," + objProjectManagement.SeqNo + ""
           + "," + objProjectManagement.TierNo + ""
           + "," + "(SELECT ISNULL(MAX([SerialNo]),0)+1 FROM [tblProjectManagement] )" + ""
           + "," + objProjectManagement.RootNo + ""
           + ")";
            DataProcess.InsertQuery(connectionString, sql);
        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }
    public void SaveTemp(string connectionString, ProjectManagement objProjectManagement)
    {
        try
        {
            objProjectManagement.DataStatus = "A";
            string sql = null;
            sql = @"INSERT INTO [tblProjectManagementTemp]
           ([ParentActivityID]
           ,[ActivityID]           
           ,[EntryUserId]           
           ,TreePath 
           )
         VALUES
               (" + objProjectManagement.ParentActivityID + ""
           + "," + objProjectManagement.ActivityID + ""
           + ",'" + objProjectManagement.EntryUserId + "'"
           + ",'" + objProjectManagement.TreePath + "'"
           + ")";
            DataProcess.InsertQuery(connectionString, sql);
        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }

    private int GetActivityID(string connectionString)
    {
        try
        {
            int activityID = 0;
            var storedProcedureComandText = "SELECT ISNULL( MAX( ActivityID),1000)+1 FROM [tblProjectManagement]";
            activityID = Convert.ToInt32(DataProcess.GetSingleValueFromtable(connectionString, storedProcedureComandText));
            return activityID;
        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }

    public void LoadPriority(string connectionString, DropDownList ddlPriority)
    {
        try
        {
            string sql = @"SELECT [PriorityID],[PriorityText] FROM [tblPriority] ORDER BY [PriorityID]";
            ClsDropDownListController.LoadDropDownList(connectionString, sql, ddlPriority, "PriorityText", "PriorityID");
        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }

    public void Update(string connectionString, ProjectManagement objProjectManagement)
    {
        try
        {
            string sql = @"UPDATE [tblProjectManagement]
                       SET 
                          [ActivityName] = '" + objProjectManagement.ActivityName + "'"
                          + ",[ResponsibleID] = '" + objProjectManagement.ResponsibleID + "'"
                          + ",[AccountableID] = '" + objProjectManagement.AccountableID + "'"
                          + ",[ConsultedID] = '" + objProjectManagement.ConsultedID + "'"
                          + ",[InformedID] = '" + objProjectManagement.InformedID + "'"
                          + ",[DueDate] = CONVERT(DATETIME,'" + objProjectManagement.DueDate + "',103)"
                          + ",[UpdateUserID] = '" + objProjectManagement.EntryUserId + "'"
                          + ",[UpdateDate] = " + "CAST(GETDATE() AS DateTime)" + ""
                          + ",[PriorityID] = " + objProjectManagement.PriorityID + ""
                          + ",[Description] = '" + objProjectManagement.Description + "'"
                          + ",[LogisticesRequired] = '" + objProjectManagement.LogisticesRequired + "'"
                          + ",[RelatedCosts] = " + objProjectManagement.RelatedCosts + ""
                          + ",[AnyIssue] = '" + objProjectManagement.AnyIssue + "'"
                          + ",[Remarks] = '" + objProjectManagement.Remarks + "'"
                     + " WHERE [ActivityID] = " + objProjectManagement.ActivityID + "";
            DataProcess.UpdateQuery(connectionString, sql);
        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }
    public void UpdateActivityStatus(string connectionString, ProjectManagement objProjectManagement)
    {
        try
        {
            string sql = @"UPDATE [tblProjectManagement]
                       SET 
                          [ActivityStatus] = " + objProjectManagement.ActivityStatusID + ""
                          + ",[UpdateUserID] = '" + objProjectManagement.EntryUserId + "'"
                          + ",[UpdateDate] = " + "CAST(GETDATE() AS DateTime)" + ""
                     + " WHERE [ActivityID] = " + objProjectManagement.ActivityID + "";
            DataProcess.UpdateQuery(connectionString, sql);
        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }

    public void Delete(string connectionString, ProjectManagement objProjectManagement)
    {
        try
        {
            objProjectManagement.DataStatus = "I";
            string sql = @"UPDATE [tblProjectManagement]
                       SET 
                          [DataStatus] = '" + objProjectManagement.DataStatus + "'"
                     + " WHERE [ActivityID] = " + objProjectManagement.ActivityID + " OR [ParentActivityID] = " + objProjectManagement.ActivityID + "";
            DataProcess.UpdateQuery(connectionString, sql);
        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }

    public int GetSeqNo(string connectionString, ProjectManagement objProjectManagement)
    {
        try
        {
            int seqNo = 0;
            var storedProcedureComandText = "SELECT MAX( A.SeqNo)+1 FROM [tblProjectManagement] A WHERE A.RootNo = " + objProjectManagement.RootNo + "";
            seqNo = Convert.ToInt32(DataProcess.GetSingleValueFromtable(connectionString, storedProcedureComandText));
            return seqNo;
        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }

    public void SaveProgressRecord(string connectionString, ProjectManagement objProjectManagement)
    {
        try
        {
            string sql = null;
            sql = @"INSERT INTO [tblProjectProgressRecord]
           ([ProjectID]
           ,[ActivityID]
           ,[ActivityStatus]
           ,[Remarks]
           ,[EntryUserId]
           ,[EntryDate])
         VALUES ( "
            + "" + objProjectManagement.ProjectID + ""
            + "," + objProjectManagement.ActivityID + ""
            + "," + objProjectManagement.ActivityStatusID + ""
            + ",'" + objProjectManagement.Remarks + "'"
            + ",'" + objProjectManagement.EntryUserId + "'"
            + "," + "CAST(GETDATE() AS DateTime)" + ""
            + ")";
            DataProcess.InsertQuery(connectionString, sql);
            this.UpdateActivityStatus(connectionString, objProjectManagement);
        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }

    public void LoadActivityStatus(string connectionString, DropDownList ddlActivityStatus)
    {
        try
        {
            string sql = @"SELECT [StatusID],[StatusText] FROM [tblActivityStatus] ORDER BY [StatusText]";
            ClsDropDownListController.LoadDropDownList(connectionString, sql, ddlActivityStatus, "StatusText", "StatusID");
        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }

    public DataTable GetActivityLog(string connectionString, ProjectManagement objProjectManagement)
    {
        try
        {
            string sql = null;
            DataTable dtActivityLog = new DataTable();
            sql = @"SELECT B.StatusText		
              ,A.[Remarks]
              ,A.[EntryUserId]
              ,A.[EntryDate]      
              FROM [tblProjectProgressRecord] A
              INNER JOIN tblActivityStatus B ON A.ActivityStatus = B.StatusID
               WHERE A.[ActivityID] = " + objProjectManagement.ActivityID + " ORDER BY A.[EntryDate]";
            dtActivityLog = DataProcess.GetData(connectionString, sql);
            return dtActivityLog;
        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }
}