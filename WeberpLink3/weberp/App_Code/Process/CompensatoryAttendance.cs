using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CompensatoryAttendance
/// </summary>
public class CompensatoryAttendance
{
	public CompensatoryAttendance()
	{
		//
		// TODO: Add constructor logic here
		//
    }
    public static DataTable GetEmployeeForCompensatory(string ConnectionStr, DateTime fDate, DateTime toDate, string deptId)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();
        cmd.Connection = sqlConn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetEmployeeForCompensatory";
        cmd.Parameters.Add(new SqlParameter("@dateFrom", SqlDbType.DateTime)).Value = fDate;
        cmd.Parameters.Add(new SqlParameter("@dateTo", SqlDbType.DateTime)).Value = toDate;
        cmd.Parameters.Add(new SqlParameter("@deptId", SqlDbType.NVarChar)).Value = deptId.ToString();
        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        sqlConn.Close();
        return dt;
    }
}