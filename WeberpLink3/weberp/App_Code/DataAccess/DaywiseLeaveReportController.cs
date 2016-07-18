using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DaywiseLeaveReportController
/// </summary>
public class DaywiseLeaveReportController
{
    public DaywiseLeaveReportController()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable GetLeaveRecord(string connectionString, DaywiseLeaveReport objDaywiseLeaveReport)
    {
        try
        {
            DataTable dtLeaveReport = new DataTable();
            string sql = @"select a.Leave_Det_Emp_Id,c.EmpName,c.Designation,c.Dept,b.Leave_Mas_Name,a.Leave_Det_Sta_Date ,(CONVERT(VARCHAR, a.Leave_Det_Emp_Days,5) + ' Day') AS Leave_Det_Emp_Days
	                from HrMs_Emp_Leave_Det a
	                inner join HRMS_Leave_Mas b on a.Leave_Det_LCode=b.Leave_Mas_Code
	                inner join Emp_Details c on c.EmpID=a.Leave_Det_Emp_Id and c.EmpType=b.T_C2
	                left outer join HrMs_Emp_mas d on a.Leave_Det_Emp_Id = d.Emp_Mas_Emp_Id
	                where CONVERT(DATETIME,a.Leave_Det_Sta_Date,103)" +
                        " BETWEEN CONVERT(DATETIME,'" + objDaywiseLeaveReport.StartDate + "',103)  AND CONVERT(DATETIME,'" + objDaywiseLeaveReport.EndDate + "',103) ";
            if (objDaywiseLeaveReport.OfficeLocation != null)
            {
                sql = sql + " AND c.OfficeID IN" + objDaywiseLeaveReport.OfficeLocation + "";
            }

            if (objDaywiseLeaveReport.Department != null)
            {
                sql = sql + " AND c.DeptID = '" + objDaywiseLeaveReport.Department + "'";

            }

            if (objDaywiseLeaveReport.EmployeeCategory != null)
            {
                sql = sql + " AND c.emptype = '" + objDaywiseLeaveReport.EmployeeCategory + "'";

            }

            if (objDaywiseLeaveReport.EmployeeCode != null)
            {
                sql = sql + " AND c.EmpID = '" + objDaywiseLeaveReport.EmployeeCode + "'";

            }

            sql = sql + " order by Leave_Det_Emp_Id,Leave_Det_LCode";
            dtLeaveReport = StoredProcedureExecutor.StoredProcedureExecuteReader(connectionString, sql);
            return dtLeaveReport;

        }
        catch (Exception msgException)
        {

            throw msgException;
        }

    }
}