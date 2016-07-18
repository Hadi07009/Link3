using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LoginUserInformation
/// </summary>
public class LoginUserInformation
{
    public LoginUserInformation()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable GetBasicInformation(string connectionString, string userId)
    {
        string sql = @"select EmpID,EmpName,Dept,Designation,b.Emp_Mas_Join_Date as Joiningdate,b.Emp_Mas_DOB, b.Emp_Mas_Confrim_Date  
	                    from Emp_Details a inner join HrMs_Emp_mas b on a.EmpID=b.Emp_Mas_Emp_Id
	                    inner join tblUserInfo C ON a.EmpID = C.UserEmpId 
	                    where C.UserEmpId ='" + userId + "'";
        return StoredProcedureExecutor.StoredProcedureExecuteReader(connectionString, sql);
    }
    public DataTable GetProfileImage(string connectionString, string userId)
    {
        DataTable dt = new DataTable();

        var storedProcedureCommandText = "exec [EmpBasicInformationGetFromHrms_Emp_Photo] '" + userId + "','" + current.CompanyCode + "'";
        dt=StoredProcedureExecutor.StoredProcedureExecuteReader(connectionString, storedProcedureCommandText);
        if (dt.Rows.Count == 0)
        {
            storedProcedureCommandText = "exec [SPGetEmp_Photo] '" + userId + "','" + current.CompanyCode + "'";
            dt = StoredProcedureExecutor.StoredProcedureExecuteReader(connectionString, storedProcedureCommandText);
        }

        return dt;

    }
}