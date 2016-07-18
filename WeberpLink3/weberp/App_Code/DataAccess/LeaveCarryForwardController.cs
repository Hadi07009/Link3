using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LeaveCarryForwardController
/// </summary>
public class LeaveCarryForwardController
{
    public LeaveCarryForwardController()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public void Save(string connectionString, LeaveCarryForward objCarryForward)
    {
        var dtCheckedData = Check(connectionString, objCarryForward);
        if (dtCheckedData.Rows.Count > 0)
        {
            throw new Exception("Leave  type alrady exist.  ");
        }
        var storedProcedureComandTest = "exec [spInitiate_HRMS_Leave_CarryForward] '" +
                                        objCarryForward.EmployeeCode + "','" +
                                        objCarryForward.SelectedDate + "','" +
                                        objCarryForward.LeaveType + "'," +
                                        objCarryForward.NoofLeave + ",'" +
                                        objCarryForward.EntryUser + "'";
        StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);
    }
    public DataTable GetData(string connectionString, LeaveCarryForward objCarryForward)
    {
        DataTable dtLeaveRecord = null;
        var storedProcedureComandTest = "exec [spGet_HRMS_Leave_CarryForward] '" + objCarryForward.EmployeeCode + "'";
        dtLeaveRecord = StoredProcedureExecutor.StoredProcedureExecuteReader(connectionString, storedProcedureComandTest);
        return dtLeaveRecord;
    }
    public void Delete(string connectionString, LeaveCarryForward objCarryForward)
    {
        var storedProcedureComandTest = "exec [spDelete_HRMS_Leave_CarryForward] '" +
                                        objCarryForward.EmployeeCode + "','" +
                                        objCarryForward.LeaveType + "'";
        StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);
    }
    private DataTable Check(string connectionString, LeaveCarryForward objCarryForward)
    {
        DataTable dtLeaveRecord = null;
        var storedProcedureComandTest = "exec [spCheck_HRMS_Leave_CarryForward] '" +
                                        objCarryForward.EmployeeCode + "','" +
                                        objCarryForward.LeaveType + "'";
        dtLeaveRecord = StoredProcedureExecutor.StoredProcedureExecuteReader(connectionString, storedProcedureComandTest);
        return dtLeaveRecord;
    }
}