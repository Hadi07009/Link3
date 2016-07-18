using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ForeignTourExpenseClaimController
/// </summary>
public class ForeignTourExpenseClaimController
{
	public ForeignTourExpenseClaimController()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void ApplyForExpense(string connectionString, ForeignTourExpenseClaim objForeignTourExpenseClaim)
    {
        try
        {
            var storedProcedureComandTest = "exec [spProcesstblProcessDataHeaderForeignTourExpenses] '" +
                                        objForeignTourExpenseClaim.ApplicantCode + "','" +
                                        objForeignTourExpenseClaim.EntryUser + "','" +
                                        objForeignTourExpenseClaim.DateClaim + "'," +
                                        objForeignTourExpenseClaim.TransactionNoLineNo + ",'" +
                                        objForeignTourExpenseClaim.TransactionNo + "','" +
                                        objForeignTourExpenseClaim.ProcessCode + "','" +
                                        objForeignTourExpenseClaim.ProcessFlowCode + "'," +
                                        objForeignTourExpenseClaim.ProcessLevelCode + ",'" +
                                        objForeignTourExpenseClaim.ProcessTypeCode + "','" +
                                        objForeignTourExpenseClaim.ExpenditureArea + "'," +
                                        objForeignTourExpenseClaim.AmountCost + ",'" +                                        
                                        objForeignTourExpenseClaim.PurposeofClaim + "'";
            StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);

        }
        catch (Exception msgException)
        {
            
            throw msgException;
        }
    }

    public void SaveDetailsInformaton(string connectionString, ForeignTourExpenseClaim objForeignTourExpenseClaim)
    {
        try
        {
            var storedProcedureComandTest = "exec [spProcesstblProcessDataDetailsForeignTourExpenses] '" +
                                        objForeignTourExpenseClaim.TransactionNo + "'," +
                                        objForeignTourExpenseClaim.AdvanceReceived + "," +
                                        objForeignTourExpenseClaim.NetClaim + ",'" +
                                        objForeignTourExpenseClaim.DateClaim + "','" +
                                        objForeignTourExpenseClaim.DepartureTime + "','" +
                                        objForeignTourExpenseClaim.ArrivalDate + "','" +
                                        objForeignTourExpenseClaim.ArrivalTime + "','" +
                                        objForeignTourExpenseClaim.AdvanceReceivedDate + "'," +
                                        objForeignTourExpenseClaim.ActualClaimAmount + "," +
                                        objForeignTourExpenseClaim.DailyAllowanceNoofDays + "," +
                                        objForeignTourExpenseClaim.AccommodationNoofDays + ",'" +
                                        objForeignTourExpenseClaim.PlaceofTour + "','" +
                                        objForeignTourExpenseClaim.Country + "','" +
                                        objForeignTourExpenseClaim.VendorTour + "','" +
                                        objForeignTourExpenseClaim.DepartureFlight + "','" +
                                        objForeignTourExpenseClaim.ArrivalFlight + "'," +                                        
                                        objForeignTourExpenseClaim.DurationDaysTour + "";
            StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);

        }
        catch (Exception msgException)
        {
            
            throw msgException;
        }
    }
}