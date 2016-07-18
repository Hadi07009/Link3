using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LocalTourExpenseClaimController
/// </summary>
public class LocalTourExpenseClaimController
{
	public LocalTourExpenseClaimController()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void ApplyForExpense(string connectionString, LocalTourExpenseClaim objLocalTourExpenseClaim)
    {
        try
        {
            var storedProcedureComandTest = "exec [spProcesstblProcessDataHeaderLocalTourExpenses] '" +
                                        objLocalTourExpenseClaim.ApplicantCode + "','" +
                                        objLocalTourExpenseClaim.EntryUser + "','" +
                                        objLocalTourExpenseClaim.DateClaim + "'," +
                                        objLocalTourExpenseClaim.TransactionNoLineNo + ",'" +
                                        objLocalTourExpenseClaim.TransactionNo + "','" +
                                        objLocalTourExpenseClaim.ProcessCode + "','" +
                                        objLocalTourExpenseClaim.ProcessFlowCode + "'," +
                                        objLocalTourExpenseClaim.ProcessLevelCode + ",'" +
                                        objLocalTourExpenseClaim.ProcessTypeCode + "','" +
                                        objLocalTourExpenseClaim.ExpenditureArea + "'," +
                                        objLocalTourExpenseClaim.AmountCost + ",'" +
                                        objLocalTourExpenseClaim.FromPlace + "','" +
                                        objLocalTourExpenseClaim.ToPlace + "'";
            StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);

        }
        catch (Exception msgException)
        {
            
            throw msgException;
        }
 
    }
    public void SaveDetailsInformaton(string connectionString, LocalTourExpenseClaim objLocalTourExpenseClaim)
    {
        try
        {
            var storedProcedureComandTest = "exec [spProcesstblProcessDataDetailsLocalTourExpenses] '" +
                                        objLocalTourExpenseClaim.TransactionNo + "'," +
                                        objLocalTourExpenseClaim.AdvanceReceived + "," +
                                        objLocalTourExpenseClaim.NetClaim + ",'" +
                                        objLocalTourExpenseClaim.DateClaim + "','" +
                                        objLocalTourExpenseClaim.DepartureTime + "','" +
                                        objLocalTourExpenseClaim.ArrivalDate + "','" +
                                        objLocalTourExpenseClaim.ArrivalTime + "','" +
                                        objLocalTourExpenseClaim.AdvanceReceivedDate + "'," +
                                        objLocalTourExpenseClaim.ActualClaimAmount + "," +
                                        objLocalTourExpenseClaim.DailyAllowanceNoofDays + "," +
                                        objLocalTourExpenseClaim.AccommodationNoofDays + "";
            StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);

        }
        catch (Exception msgException)
        {
            
            throw msgException;
        }
 
    }
    public DataTable GetDetailsInformation(string connectionString,LocalTourExpenseClaim objLocalTourExpenseClaim)
    {
        try
        {
            var storedProcedureComandTest = "exec [spProcessGettblProcessDataDetails] '" +
                                        objLocalTourExpenseClaim.TransactionNo + "'";
            return StoredProcedureExecutor.StoredProcedureExecuteReader(connectionString, storedProcedureComandTest);

        }
        catch (Exception msgException)
        {
            
            throw msgException;
        }
 
    }
}