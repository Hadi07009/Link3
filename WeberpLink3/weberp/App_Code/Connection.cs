using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;  

/// <summary>
/// Summary description for Connection
/// </summary>
namespace Dataaccess

{
public class LNKConnection
    {
        public string conn;
        public SqlConnection con;
        public LNKConnection()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public SqlConnection init()
        {
            conn = ConfigurationSettings.AppSettings["UbasysConnectionString"].ToString();
            con = new SqlConnection(conn);
            //con.ConnectionTimeout = 100;


            try
            {
                con.Open();
            }


            catch (SqlException ex)
            {
                ex.Message.ToString();
            }


            return con;

        }

        public void destory()
        {
            init();
            // con.ConnectionTimeout = 100;
            con.Close();
        }
    }
public class Connection
{
    public string conn;
    public SqlConnection con;
    public Connection()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public SqlConnection init()
    {
        conn = ConfigurationSettings.AppSettings["UbasysConnectionString"].ToString();
        con = new SqlConnection(conn);
        //con.ConnectionTimeout = 100;
        

        try
        {
            con.Open();
        }


        catch (SqlException ex)
        {
            ex.Message.ToString();
        }


        return con;

    }

        public void destory()
        {
            init();
           // con.ConnectionTimeout = 100;
            con.Close();
        }
    }
public class L3TConnection
{
    public string conn;
    public SqlConnection con;
    public L3TConnection()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public SqlConnection init()
    {
        conn = ConfigurationSettings.AppSettings["UbasysConnectionString"].ToString();
        con = new SqlConnection(conn);
        //con.ConnectionTimeout = 100;


        try
        {
            con.Open();
        }


        catch (SqlException ex)
        {
            ex.Message.ToString();
        }


        return con;

    }

    public void destory()
    {
        init();
        // con.ConnectionTimeout = 100;
        con.Close();
    }
}
public class SMSConnection
{
    public string conn;
    public SqlConnection con;
    public SMSConnection()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public SqlConnection init()
    {
        conn = ConfigurationSettings.AppSettings["UbasysConnectionString"].ToString();
        con = new SqlConnection(conn);
        //con.ConnectionTimeout = 100;


        try
        {
            con.Open();
        }


        catch (SqlException ex)
        {
            ex.Message.ToString();
        }


        return con;

    }

    public void destory()
    {
        init();
        // con.ConnectionTimeout = 100;
        con.Close();
    }
}
}
