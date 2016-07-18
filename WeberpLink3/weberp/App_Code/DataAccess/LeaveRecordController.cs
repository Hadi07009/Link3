using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LeaveRecordController
/// </summary>
public class LeaveRecordController
{
    public LeaveRecordController()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable GetLeaveRecord(string connectionString, LeaveRecord objLeaveRecord)
    {
        string storedProcedureComandTest;
        if (objLeaveRecord.EmployeeCode == null)
        {
            DataTable dtEmployeeCode = CommonMethods.GetActiveEmployeeCode(connectionString);
            foreach (DataRow dataRow in dtEmployeeCode.Rows)
            {
                storedProcedureComandTest = "exec [spProcessGetLeavebalanceForReport] '" + dataRow.ItemArray[0] + "','" + objLeaveRecord.StartDate + "','"+objLeaveRecord.EndDate+"'";
                StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);
            }

        }
        else
        {
            storedProcedureComandTest = "exec [spProcessGetLeavebalanceForReport] '" + objLeaveRecord.EmployeeCode + "','" + objLeaveRecord.StartDate + "','"+objLeaveRecord.EndDate+"'";
            StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);

        }

        var storedProcedureComandRead = "exec [spProcessGetLeaveRecord] '" + objLeaveRecord.EmployeeCode + "'";
        return StoredProcedureExecutor.StoredProcedureExecuteReader(connectionString, storedProcedureComandRead);
    }

    public DataTable GetLeaveRecordDetails(string connectionString, LeaveRecord objLeaveRecord)
    {
        var storedProcedureComandRead = "exec [spProcessGetLeaveRecordDetails] '" + objLeaveRecord.EmployeeCode + "','"+objLeaveRecord.StartDate+"','"+objLeaveRecord.EndDate+"'";
        return StoredProcedureExecutor.StoredProcedureExecuteReader(connectionString, storedProcedureComandRead);
    }
}