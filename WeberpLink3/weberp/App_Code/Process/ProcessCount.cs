using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public class ProcessCount
{
    public ProcessCount()
    {
    }  
    public DataTable GetPendingApplicationCount(string actingpersonid, string Processid, string flowid, string ConnectionStr)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(ConnectionStr);
        sqlConn.Open();       
        try
        {          
            cmd.Connection = sqlConn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spProcessGetPendingApplicationCount";
            cmd.Parameters.Add(new SqlParameter("@actempid", SqlDbType.NVarChar)).Value = actingpersonid;
            cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid;
            cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid;
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);            
        }
        catch (Exception ex)
        {            
        }
        finally
        {
            sqlConn.Close();
        }

        return dt;

    }     
}
