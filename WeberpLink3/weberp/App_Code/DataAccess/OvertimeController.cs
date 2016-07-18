using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TaxChallanController
/// </summary>
public class OvertimeController
{
    public OvertimeController()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void Save(string connectionString, Overtime objOvertime)
    {
        var storedProcedureComandTest = "exec [spOthersPaymentInitiate] '" +
                                        objOvertime.EmployeeCode + "','" +
                                        objOvertime.ChallanDate + "','" +
                                        objOvertime.ChallanForMonth + "','" +
                                        objOvertime.ChallanNumber + "'," +
                                        objOvertime.ChallamAmount + ",'" +
                                        objOvertime.Paytype + "'";

        StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);
    }
    public DataTable GetDate(string connectionString,Overtime objOvertime)
    {
        DataTable dtTaxChallan = null;
        var storedProcedureComandTest = "exec [spGetOthersPayment] '" +
                                        objOvertime.ChallanDate + "','" +
                                        objOvertime.ChallanDateSearch + "','" +
                                        objOvertime.Paytype +"'";
        return dtTaxChallan = StoredProcedureExecutor.StoredProcedureExecuteReader(connectionString, storedProcedureComandTest);
    }
    public void Delete(string connectionString, Overtime objoverTime)
    {
        var storedProcedureComandTest = "exec [spOthersPaymentDelete] '" +
                                        objoverTime.EmployeeCode + "','" +
                                        objoverTime.ChallanForMonth + "','" +
                                        objoverTime.Paytype+"'";
        StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);
 
    }
}