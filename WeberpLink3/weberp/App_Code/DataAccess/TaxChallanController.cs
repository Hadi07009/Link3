using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TaxChallanController
/// </summary>
public class TaxChallanController
{
    public TaxChallanController()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void Save(string connectionString,TaxChallan objTaxChallan)
    {
        var storedProcedureComandTest = "exec [TaxchallanInitiate_tbl_tax_challan] '" +
                                        objTaxChallan.EmployeeCode + "','" +
                                        objTaxChallan.ChallanDate + "','" +
                                        objTaxChallan.ChallanForMonth + "','" +
                                        objTaxChallan.ChallanNumber + "'," +
                                        objTaxChallan.ChallamAmount + "";
        StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);
    }
    public DataTable GetDate(string connectionString,TaxChallan objTaxChallan)
    {
        DataTable dtTaxChallan = null;
        var storedProcedureComandTest = "exec [TaxchallanGet_tbl_tax_challan] '" +
                                        objTaxChallan.ChallanDate + "','" +
                                        objTaxChallan.ChallanDateSearch + "'";
        return dtTaxChallan = StoredProcedureExecutor.StoredProcedureExecuteReader(connectionString, storedProcedureComandTest);
    }
    public void Delete(string connectionString, TaxChallan objTaxChallan)
    {
        var storedProcedureComandTest = "exec [TaxchallanDelete_tbl_tax_challan] '" +
                                        objTaxChallan.EmployeeCode + "','" +
                                        objTaxChallan.ChallanNumber + "'";
        StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);
 
    }
}