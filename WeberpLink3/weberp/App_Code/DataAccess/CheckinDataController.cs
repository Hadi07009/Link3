using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CheckinDataController
/// </summary>
public class CheckinDataController
{
    public CheckinDataController()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public void Save(string connectionString, CheckinData objCheckinData)
    {
        var storedProcedureComandTest = "exec [spCHECKDataInitiate_CHECKINOUT] '" +
                                        objCheckinData.EmployeeCode + "','" +
                                        objCheckinData.CheckinDate + "'";
        StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);
    }
    public void AttendanceUpdate(string connectionString)
    {
        var storedProcedureComandTest = "exec [spAttendanceUpdateBySystemCompanyWiseHO] '" + current.CompanyCode + "'";

        StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);
    }
}