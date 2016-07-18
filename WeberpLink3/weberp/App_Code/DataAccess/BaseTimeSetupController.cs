using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BaseTimeSetupController
/// </summary>
public class BaseTimeSetupController
{
    public BaseTimeSetupController()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public void Save(string connectionString, BaseTimeSetup objBaseTimeSetup)
    {
        try
        {
            var storedProcedureComandTest = "exec [spInitiateHrmsBaseTime] '" +
                                        objBaseTimeSetup.ShiftCode + "','" +
                                        objBaseTimeSetup.FromDate + "','" +
                                        objBaseTimeSetup.ToDate + "','" +
                                        objBaseTimeSetup.InTime + "','" +
                                        objBaseTimeSetup.OutTime + "'";
            StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);

        }
        catch (Exception msgException)
        {
            
            throw msgException;
        }

    }
    public DataTable GetAll(string connectionString)
    {
        try
        {
            DataTable dtBaseTime = new DataTable();
            var storedProcedureComandTest = "exec [spGetBaseTimeFromHrmsBaseTime] ";
            return dtBaseTime = StoredProcedureExecutor.StoredProcedureExecuteReader(connectionString, storedProcedureComandTest);

        }
        catch (Exception msgException)
        {
            
            throw msgException;
        }
    }

    public void Delete(string connectionString, BaseTimeSetup objBaseTimeSetup)
    {
        try
        {
            string sql = "DELETE FROM [HrmsBaseTime] WHERE [Slno] = " + objBaseTimeSetup.SlNumber + "";
            StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, sql);

        }
        catch (Exception msgException)
        {
            
            throw msgException;
        }

    }
}