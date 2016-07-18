using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AnnualStatementController
/// </summary>
public class AnnualStatementController
{
    public AnnualStatementController()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private void Save(string connectionString, AnnualStatement objAnnualStatement)
    {
        try
        {
            string fromDate = "CONVERT(DATETIME,'" + objAnnualStatement.FromDate + "',103)";
            string toDate = "CONVERT(DATETIME,'" + objAnnualStatement.ToDate + "',103)";
            string sql = null;
            sql = @"INSERT INTO tblAnnualStatementReport
                SELECT A.EmpID
	                ,B.Emp_Mas_TIN
	                ,0 AS salary
	                ,sum(case when Calcode = 'STDHR' then CalVal else 0 end) AS [House Rent Allowance]
	                ,sum(case when Calcode = 'STDCON' then CalVal else 0 end) AS [Conveyance Allowance] 
	                ,0 AS [entertainmentAmount]  
	                ,sum(case when Calcode = 'STDMA' then CalVal else 0 end) AS [Medical Allowance]
	                ,sum(case when Calcode = 'STDOTH' then CalVal else 0 end) AS [Other Allowance]
	                ,0 AS [rentFreeAccommodation]
	                ,0 AS [freeConveyance]
	                ,0 AS [freeOrConcessionalPassages]
	                ,0 AS [salaryPaid]
	                ,sum(case when Calcode = 'PFEC' then CalVal else 0 end) AS [Provident Fund Employers Contribution]
	                ,0 AS [anyBenefit]
	                ,0 AS [liableToTaxAmount]
                    ,0 AS [taxPayableonTheAmount]
                    ,0 AS [investmentAmount]
                    ,0 AS [taxCreditAmount]
                    ,0 AS [taxPayableAmount]
                    ,0 AS [taxDeductedAndPaid]
                    ,0 AS [remarks]
                    ,0 AS [totalIncomeSection]
                    ,0 AS [totalIncomeSalaryCertificate]
                    ,0 AS [taxDeductedSalaryCertificate]
                    ,0 AS [diffTotalSalaryIncome]
                    ,0 AS [diffTotalTaxDeposit]
                    ,0 AS [diffTotalTaxDeposit(WSSS)]
                    ,'" + objAnnualStatement.UserID + "' AS [userID]" + @"
	                FROM Emp_Details A 
	                INNER JOIN HrMs_Emp_mas B ON A.EmpID = B.Emp_Mas_Emp_Id
	                INNER JOIN hrms_salary C ON A.EmpID = C.Empcode
	                WHERE C.Salmonth BETWEEN " + fromDate + " AND " + toDate + "";
            if (objAnnualStatement.EmployeeCode != null)
            {
                sql = sql + " AND A.EmpID = '" + objAnnualStatement.EmployeeCode + "'";
            }

            sql = sql + " GROUP BY A.EmpID,B.Emp_Mas_TIN	ORDER BY A.EmpID";
            DataProcess.InsertQuery(connectionString, sql);

        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }

    private void Delete(string connectionString, AnnualStatement objAnnualStatement)
    {
        try
        {
            string sql = null;
            sql = @"DELETE FROM tblAnnualStatementReport WHERE userID = '" + objAnnualStatement.UserID + "'";
            DataProcess.DeleteQuery(connectionString, sql);

        }
        catch (Exception msgException)
        {

            throw msgException;
        }

    }
    public DataTable ShowRecord(string connectionString, AnnualStatement objAnnualStatement)
    {
        SqlConnection myConnection = new SqlConnection(connectionString);
        myConnection.Open();
        SqlTransaction transaction = myConnection.BeginTransaction();
        try
        {
            Delete(connectionString, objAnnualStatement);
            Save(connectionString,objAnnualStatement);
            string sql = null;
            DataTable dtAnnualStatementRecord = new DataTable();
            sql = @"SELECT B.EmpName
	          ,[employeeCode]
	          ,B.Designation
	          ,B.Dept
              ,[TIN]
              ,[salaryAmount]
              ,[houseRent]
              ,[conveyanceAmount]
              ,[entertainmentAmount]
              ,[medicalAmount]
              ,[othersAmount]
              ,[rentFreeAccommodation]
              ,[freeConveyance]
              ,[freeOrConcessionalPassages]
              ,[salaryPaid]
              ,[providentFundEmployersContribution]
              ,[anyBenefit]
              ,[liableToTaxAmount]
              ,[taxPayableonTheAmount]
              ,[investmentAmount]
              ,[taxCreditAmount]
              ,[taxPayableAmount]
              ,[taxDeductedAndPaid]
              ,[remarks]
              ,[totalIncomeSection]
              ,[totalIncomeSalaryCertificate]
              ,[taxDeductedSalaryCertificate]
              ,[diffTotalSalaryIncome]
              ,[diffTotalTaxDeposit]
              ,[diffTotalTaxDeposit(WSSS)]      
          FROM [tblAnnualStatementReport] A
          INNER JOIN Emp_Details B ON A.employeeCode = B.EmpID
          WHERE [userID] = '" + objAnnualStatement.UserID + "' ORDER BY B.EmpID";
            dtAnnualStatementRecord = DataProcess.GetData(connectionString, sql);
            transaction.Commit();
            myConnection.Close();
            return dtAnnualStatementRecord;
        }
        catch (Exception msgException)
        {
            transaction.Rollback();
            if (myConnection.State == System.Data.ConnectionState.Open)
            {
                myConnection.Close();
                
            }

            throw msgException;
        }

    }
}