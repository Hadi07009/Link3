using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LeaveStatementController
/// </summary>
public class LeaveStatementController
{
    public LeaveStatementController()
    {
        //
        // TODO: Add constructor logic here
        //
    }
   

    public void LeaveStatementPrepare(string connectionString, LeaveStatement objLeaveStatement)
    {
        string storedProcedureComandTest = "exec [spProcessLeaveStatementPrepare] '" + objLeaveStatement.FromDate + "','" + objLeaveStatement.OfficeLocation + "','" + objLeaveStatement.EntryUser + "'";
        StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);
    }
}