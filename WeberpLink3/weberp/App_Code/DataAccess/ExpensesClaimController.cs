using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ExpensesClaimController
/// </summary>
public class ExpensesClaimController
{
	public ExpensesClaimController()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void ApplyForExpensesPayment(string connectionString, ProcessAdvancePayment objProcessAdvancePayment)
    {
        try
        {
            var storedProcedureComandTest = "exec [spProcesstblProcessDataHeaderExpensesClaim] '" +
                                        objProcessAdvancePayment.ApplicantCode + "','" +
                                        objProcessAdvancePayment.EntryUser + "','" +
                                        objProcessAdvancePayment.DateClaim + "'," +
                                        objProcessAdvancePayment.TransactionNoLineNo + ",'" +
                                        objProcessAdvancePayment.TransactionNo + "','" +
                                        objProcessAdvancePayment.ProcessCode + "','" +
                                        objProcessAdvancePayment.ProcessFlowCode + "'," +
                                        objProcessAdvancePayment.ProcessLevelCode + ",'" +
                                        objProcessAdvancePayment.ProcessTypeCode + "','" +
                                        objProcessAdvancePayment.ExpenditureArea + "'," +
                                        objProcessAdvancePayment.AmountCost + "";
            StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);

        }
        catch (Exception msgException)
        {
            
            throw msgException;
        }
    }
    public void SaveAdvanceReceivedAmount(string connectionString, ProcessAdvancePayment objProcessAdvancePayment)
    {
        try
        {
            var storedProcedureComandTest = "exec [spProcesstblProcessDataDetailsExpensesClaim] '" +
                                        objProcessAdvancePayment.TransactionNo + "'," +
                                        objProcessAdvancePayment.AdvanceReceived + "," +
                                        objProcessAdvancePayment.NetClaim + "";
            StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);

        }
        catch (Exception msgException)
        {
            
            throw msgException;
        }
 
    }
    public DataTable GetAdvanceReceivedAmount(string connectionString, ProcessAdvancePayment objProcessAdvancePayment)
    {
        try
        {
            var sqlQuery = "SELECT DISTINCT ROUND( advancedReceivedOnBDT,2) AS advancedReceivedOnBDT,ROUND( netClaimBDT,2) AS netClaimBDT FROM tblProcessDataHeader WHERE transactionNo = '" + objProcessAdvancePayment.TransactionNo + "'";
            return StoredProcedureExecutor.StoredProcedureExecuteReader(connectionString, sqlQuery);

        }
        catch (Exception msgException)
        {
            
            throw msgException;
        }
 
    }

}