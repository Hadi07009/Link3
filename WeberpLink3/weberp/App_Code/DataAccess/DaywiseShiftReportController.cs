using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DaywiseLeaveReportController
/// </summary>
public class DaywiseShiftReportController
{
    public DaywiseShiftReportController()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable GetShiftRecord(string connectionString, DaywiseLeaveReport objDaywiseLeaveReport)
    {
        try
        {
            DataTable dtLeaveReport = new DataTable();
            string sql = @"SELECT A.Atnd_Det_Emp_Id,B.EmpName,B.Designation,B.Dept,A.Atnd_det_date,C.Shift_Mas_Desc,
                        CASE A.Atnd_det_offlg 
                        WHEN 'A' THEN 'Present'
                        WHEN 'H' THEN 'Holiday'
                        WHEN 'O' THEN 'Present'
                        WHEN 'L' THEN E.Leave_Mas_Name
                        WHEN 'N' THEN E.Leave_Mas_Name+' ( 0.5 day )'
                        ELSE ''
                        END AS atendanceStatus
                        FROM hrms_atnd_det A
                        INNER JOIN Emp_Details B ON A.Atnd_Det_Emp_Id = B.EmpID
                        INNER JOIN hrms_shift_mas C ON A.Atnd_Det_sftID = C.Shift_Mas_Code
                        LEFT JOIN HrMs_Emp_Leave_Det D ON A.Atnd_det_date = D.Leave_Det_Sta_Date AND A.Atnd_Det_Emp_Id = D.Leave_Det_Emp_Id 
                        LEFT JOIN HRMS_Leave_Mas E ON D.Leave_Det_LCode = E.Leave_Mas_Code
                        where CONVERT(DATETIME,A.Atnd_det_date,103)" +
                        " BETWEEN CONVERT(DATETIME,'" + objDaywiseLeaveReport.StartDate + "',103)  AND CONVERT(DATETIME,'" + objDaywiseLeaveReport.EndDate + "',103) ";
            if (objDaywiseLeaveReport.OfficeLocation != null)
            {
                sql = sql + " AND B.OfficeID IN" + objDaywiseLeaveReport.OfficeLocation + "";
            }

            if (objDaywiseLeaveReport.Department != null)
            {
                sql = sql + " AND B.DeptID = '" + objDaywiseLeaveReport.Department + "'";

            }

            if (objDaywiseLeaveReport.EmployeeCategory != null)
            {
                sql = sql + " AND B.emptype = '" + objDaywiseLeaveReport.EmployeeCategory + "'";

            }

            if (objDaywiseLeaveReport.EmployeeCode != null)
            {
                sql = sql + " AND B.EmpID = '" + objDaywiseLeaveReport.EmployeeCode + "'";

            }

            sql = sql + " ORDER BY A.Atnd_det_date,A.Atnd_Det_Emp_Id";
            dtLeaveReport = StoredProcedureExecutor.StoredProcedureExecuteReader(connectionString, sql);
            return dtLeaveReport;

        }
        catch (Exception msgException)
        {

            throw msgException;
        }

    }
}