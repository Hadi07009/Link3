using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EmployeeInvestAmountController
/// </summary>
public class EmployeeInvestAmountController
{
    public EmployeeInvestAmountController()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable GenerateASetOfYear()
    {
        DataTable dtYear = new DataTable();
        dtYear.Columns.Add("yearId", typeof(string));
        dtYear.Columns.Add("yearName", typeof(string));

        for (int i = DateTime.Now.Year - 1; i < DateTime.Now.Year + 10; i++)
        {
            DataRow drNew = dtYear.NewRow();
            drNew["yearId"] = i.ToString() + "-" + (i + 1).ToString();
            drNew["yearName"] = i.ToString() + "-" + (i + 1).ToString();
            dtYear.Rows.Add(drNew);
        }
        return dtYear;
    }
    public void Save(string connectionString, EmployeeInvestAmount objEmpInvestAmount)
    {
        try
        {
            string sql = null;
            sql = @"declare @sl int,@refnumber varchar(10)
	            set @sl=(select isnull(max(RIGHT(referenceNumber,5)),0)+1 as sl from HRMS_Emp_Invest) 
	            set @refnumber=STUFF('00000',6-LEN(@sl),20,@sl)
		INSERT INTO HRMS_Emp_Invest 
		(
		referenceNumber,
		financialYear,
		employeeCode,
		investAmount,
		investDescription
		) 
		VALUES 
		(
		@refnumber,
		'" + objEmpInvestAmount.FinancialYear + "','" + objEmpInvestAmount.EmployeeCode + "'," + objEmpInvestAmount.InvestAmount + ",'" + objEmpInvestAmount.InvestDescription + "')";
            DataProcess.InsertQuery(connectionString, sql);

        }
        catch (Exception msgException)
        {

            throw msgException;
        }

    }

    public void Update(string _connectionString, EmployeeInvestAmount _objEmpInvestAmount)
    {
        try
        {
            string sql = null;
            sql = @"UPDATE HRMS_Emp_Invest
		    SET
		    financialYear	=	ISNULL('" + _objEmpInvestAmount.FinancialYear + "',financialYear),employeeCode	=	ISNULL('" + _objEmpInvestAmount.EmployeeCode + "',employeeCode)," +
            " investAmount	=	ISNULL(" + _objEmpInvestAmount.InvestAmount + ",investAmount),investDescription	=	ISNULL('" + _objEmpInvestAmount.InvestDescription + "',investDescription) " +
            " WHERE	referenceNumber= '" + _objEmpInvestAmount.ReferenceNumber + "'";
            DataProcess.UpdateQuery(_connectionString, sql);

        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }
    public DataTable GetRecord(string connectionString)
    {
        try
        {
            string sql = null;
            DataTable dtInvestRecord = new DataTable();
            sql = @"SELECT  [referenceNumber]
          ,[financialYear]
          ,[employeeCode]
          ,[investAmount]
          ,[investDescription]
            FROM [HRMS_Emp_Invest]";
            dtInvestRecord = DataProcess.GetData(connectionString, sql);
            return dtInvestRecord;

        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }

    public void Delete(string connectionString, EmployeeInvestAmount objEmpInvestAmount)
    {
        try
        {
            string sql = null;
            sql = @"DELETE FROM HRMS_Emp_Invest WHERE referenceNumber = '" + objEmpInvestAmount.ReferenceNumber + "'";
            DataProcess.DeleteQuery(connectionString, sql);

        }
        catch (Exception msgException)
        {

            throw msgException;
        }

    }
}