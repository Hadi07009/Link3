using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Reflection;

/// <summary>
/// Summary description for HelperTA
/// </summary>
public class HelperTA
{
	public HelperTA()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    
    //private static void SetTransaction(SqlDataAdapter adapter, SqlTransaction trans)
    //{
    //    adapter.InsertCommand.Connection = trans.Connection;
    //    adapter.InsertCommand.Transaction = trans;  
       
    //    //adapter.UpdateCommand.Connection = trans.Connection;
    //    //adapter.UpdateCommand.Transaction = trans;
    //    //adapter.DeleteCommand.Connection = trans.Connection;
    //    //adapter.DeleteCommand.Transaction = trans;

       
    //}

    //public static void SetTransaction(object tableAdapter, SqlTransaction trans)
    //{
    //    SqlDataAdapter adapter = GetAdapter(tableAdapter);
    //    SetTransaction(adapter, trans);
     
    //}

    public static SqlDataAdapter GetAdapter(object tableAdapter)
    {
        Type tableAdapterType = tableAdapter.GetType();
        SqlDataAdapter adapter = (SqlDataAdapter)tableAdapterType.GetProperty("Adapter", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(tableAdapter, null);

        return adapter;
    }

    public static SqlTransaction OpenTransaction(SqlConnection Conn)
    {
        if (Conn.State == 0)
            Conn.Open();       
        return Conn.BeginTransaction();
    }

    public static void CloseTransaction(SqlConnection Conn, SqlTransaction Trn)
    {
        if (Conn.State != 0)
            Conn.Close();
        Trn.Dispose();
        Conn.Dispose();
    }

    public static void CloseTransaction(SqlConnection Conn)
    {
        if (Conn.State != 0)
            Conn.Close();

        Conn.Dispose();
        
    }
        
}


