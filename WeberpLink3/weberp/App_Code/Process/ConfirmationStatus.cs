using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ConfirmationStatus
/// </summary>
public class ConfirmationStatus
{
	public ConfirmationStatus()
	{
		//
		// TODO: Add constructor logic here
		//
	}
     #region Properties
    public string empId { get; set; }
    public string confirmDate { get; set; }
    public string extensionDate { get; set; }
    public Decimal joiningSalary { get; set; }
    public Decimal confirmSalary { get; set; }
    public string type { get; set; }
    public string entryUser { get; set; }
    public string status { get; set; }
    public string remarks { get; set; }
    public string SqlStringForBankAccount { set; get; }
    #endregion Properties

    #region Methods
    public string InitiateConfirmationStatus(List<ConfirmationStatus> conList, SqlCommand myCommand)
    {
        string retValue = "";
        try
        {
            foreach (ConfirmationStatus ofConList in conList)
            {
                myCommand.CommandText = "exec [spProcessInitiateConfirmationStatus] '" + ofConList.empId + "','" + ofConList.confirmDate + "','" + ofConList.extensionDate + "'," + ofConList.joiningSalary + "," + ofConList.confirmSalary + ",'" + ofConList.type + "','" + ofConList.entryUser + "','" + ofConList.status + "','" + ofConList.remarks +"'";
                myCommand.CommandTimeout = 0;
                myCommand.ExecuteNonQuery();
            }
            retValue = "Data Saved Successful";
            return retValue;
        }
        catch (Exception)
        {
            return retValue;
        }
    }
    #endregion Methods
}