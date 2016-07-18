using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LunchBillApplicationController
/// </summary>
public class LunchBillApplicationController
{
	public LunchBillApplicationController()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void ApplyForBill(string connectionString, LunchBillApplication objLunchBillApplication)
    {
        try
        {
            var storedProcedureComandTest = "exec [spProcesstblProcessDataHeaderLunchBill] '" +
                                        objLunchBillApplication.ApplicantCode + "','" +
                                        objLunchBillApplication.EntryUser + "','" +
                                        objLunchBillApplication.DateClaim + "'," +
                                        objLunchBillApplication.TransactionNoLineNo + ",'" +
                                        objLunchBillApplication.TransactionNo + "','" +
                                        objLunchBillApplication.ProcessCode + "','" +
                                        objLunchBillApplication.ProcessFlowCode + "'," +
                                        objLunchBillApplication.ProcessLevelCode + ",'" +
                                        objLunchBillApplication.ProcessTypeCode + "','" +
                                        objLunchBillApplication.PurposeofClaim + "','" +
                                        objLunchBillApplication.LocationDuringLunch + "'," +
                                        objLunchBillApplication.AmountCost + ",'" +
                                        objLunchBillApplication.AssignedBy + "'";
            StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);

        }
        catch (Exception msgException)
        {
            
            throw msgException;
        }
    }
}