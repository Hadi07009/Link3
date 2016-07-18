using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ConveyanceApplicationController
/// </summary>
public class ConveyanceApplicationController
{
	public ConveyanceApplicationController()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void ApplyConveyance(string connectionString, ConveyanceApplication objConveyanceApplication)
    {
        try
        {
            SaveConveyanceRecord(connectionString,objConveyanceApplication);
            var storedProcedureComandTest = "exec [spProcessInitiateConveyanceProcess] '" +
                                        objConveyanceApplication.ApplicantCode + "','" +
                                        objConveyanceApplication.EntryUser + "','" +
                                        objConveyanceApplication.DateClaim + "'," +
                                        objConveyanceApplication.TransactionNoLineNo + ",'" +
                                        objConveyanceApplication.TransactionNo + "','" +
                                        objConveyanceApplication.ProcessCode + "','" +
                                        objConveyanceApplication.ProcessFlowCode + "'," +
                                        objConveyanceApplication.ProcessLevelCode + ",'" +
                                        objConveyanceApplication.ProcessTypeCode + "'";
            StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);

        }
        catch (Exception msgException)
        {
            
            throw msgException;
        }
 
    }
    private void SaveConveyanceRecord(string connectionString, ConveyanceApplication objConveyanceApplication)
    {
        try
        {
            var storedProcedureComandTest = "exec [spProcesstblProcessDataHeaderConveyance] '" +
                                        objConveyanceApplication.TransactionNo + "'," +
                                        objConveyanceApplication.TransactionNoLineNo + ",'" +
                                        objConveyanceApplication.ProcessCode + "','" +
                                        objConveyanceApplication.DateClaim + "','" +
                                        objConveyanceApplication.PurposeofClaim + "','" +
                                        objConveyanceApplication.FromLocation + "','" +
                                        objConveyanceApplication.ToLocation + "','" +
                                        objConveyanceApplication.ModeofJourney + "'," +
                                        objConveyanceApplication.AmountCost + ",'" +
                                        objConveyanceApplication.AssignedByEmployee + "','" +
                                        objConveyanceApplication.EntryUser + "'";
            StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);

        }
        catch (Exception msgException)
        {
            
            throw msgException;
        }
 
    }

    public DataTable ShowPendingApplication(string connectionString, ConveyanceApplication objConveyanceApplication)
    {
        try
        {
            var storedProcedureComandTest = "exec [spProcessGetAllPendingConveyance] '" +
                                    objConveyanceApplication.ActingPersonCode + "','" +
                                    objConveyanceApplication.ProcessCode + "','" +
                                    objConveyanceApplication.ProcessFlowCode + "'";
            return StoredProcedureExecutor.StoredProcedureExecuteReader(connectionString, storedProcedureComandTest);

        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }

    public DataTable ShowPendingApplicationDetails(string connectionString, ConveyanceApplication objConveyanceApplication)
    {
        try
        {
            var storedProcedureComandTest = "exec [spProcessGetPendingConveyanceDetails] '" +
                                    objConveyanceApplication.TransactionNo + "','" +
                                    objConveyanceApplication.ProcessCode + "','" +
                                    objConveyanceApplication.ProcessFlowCode + "','" +
                                    objConveyanceApplication.ActingPersonCode + "'";
            return StoredProcedureExecutor.StoredProcedureExecuteReader(connectionString, storedProcedureComandTest);

        }
        catch (Exception msgException)
        {
            
            throw msgException;
        }
        
    }
    public void ActionOnApplication(string connectionString, ConveyanceApplication objConveyanceApplication)
    {
        try
        {
            var storedProcedureComandTest = "exec [spProcessConveyanceApplicationApproval] " +
                                        objConveyanceApplication.ActionTypeCode + "," +
                                        objConveyanceApplication.TransactionNoLineNo + ",'" +
                                        objConveyanceApplication.TransactionNo + "','" +
                                        objConveyanceApplication.ProcessCode + "','" +
                                        objConveyanceApplication.ProcessFlowCode + "'," +
                                        objConveyanceApplication.ProcessLevelCode + ",'"+
                                        objConveyanceApplication.ActingPersonCode +"'";
            StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);

        }
        catch (Exception msgException)
        {
            
            throw msgException;
        }
    }
}