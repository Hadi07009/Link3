using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Text;
using System.Data.SqlClient;
/// <summary>
/// Summary description for DBConnCls
/// </summary>
public class DBConnCls
{
    private string connectionString = null;


    string server_name = "(Local)", DB = "SSP", password = "", SLog = "sa";
    static string SName, Slog, SDB, SPassword;

    public DBConnCls()
    {
        //  Get_Serverinfo();
        //  connectionString = @"Data Source=" + server_name + ";Initial Catalog =" + DB + ";User ID =" + SLog + "; Password =" + password + ";Persist Security Info=True;";
    }

    public SqlConnection Connection
    {
        get
        {
            return new SqlConnection(connectionString);
        }
    }

    public static SqlConnection ConnectionStr
    {
        get
        {
            return new SqlConnection(@"Data Source=" + GetServerName() + ";Initial Catalog =" + GetDatabaseName() + ";User ID =" + GetServerLoginID() + "; Password =" + GetServerServerPassword() + ";Persist Security Info=True;");
        }
    }

    public static string ConnectionStrSystemDB
    {
        get
        {
            return ConfigurationSettings.AppSettings["UbasysConnectionString"].ToString();
        }
    }

    public static string ConnectionString
    {
        get
        {
            return (ConfigurationSettings.AppSettings["UbasysConnectionString"].ToString());
        }
    }
    public static string ConnectionStringL3T
    {
        get
        {
            return (ConfigurationSettings.AppSettings["L3TConnectionString"].ToString());
        }
    }
    public static string ConnectionStringWFA2
    {
        get
        {
            return (ConfigurationSettings.AppSettings["WFA2ConnectionString"].ToString());
        }
    }

    private static string GetDatabaseName()
    {
        string input = ConfigurationSettings.AppSettings["UbasysConnectionString"].ToString();
        string[] a = { };
        a = input.ToString().Split(';');
        if (a.Length > 3)
        {
            SDB = a[0].ToString();
            SName = a[1].ToString();
            Slog = a[2].ToString();
            SPassword = a[3].ToString();
        }
        else
        {
            SDB = a[0].ToString();
            SName = a[1].ToString();
            Slog = a[2].ToString();
        }
        return SDB;
    }
    private static string GetServerName()
    {
        string input = ConfigurationSettings.AppSettings["UbasysConnectionString"].ToString();
        string[] a = { };
        a = input.ToString().Split(';');
        if (a.Length > 3)
        {
            SDB = a[0].ToString();
            SName = a[1].ToString();
            Slog = a[2].ToString();
            SPassword = a[3].ToString();
        }
        else
        {
            SDB = a[0].ToString();
            SName = a[1].ToString();
            Slog = a[2].ToString();
        }
        return SName;
    }
    private static string GetServerLoginID()
    {
        string input = ConfigurationSettings.AppSettings["UbasysConnectionString"].ToString();
        string[] a = { };
        a = input.ToString().Split(';');
        if (a.Length > 3)
        {
            SDB = a[0].ToString();
            SName = a[1].ToString();
            Slog = a[2].ToString();
            SPassword = a[3].ToString();
        }
        else
        {
            SDB = a[0].ToString();
            SName = a[1].ToString();
            Slog = a[2].ToString();
        }
        return Slog;
    }
    private static string GetServerServerPassword()
    {
        string input = ConfigurationSettings.AppSettings["UbasysConnectionString"].ToString();
        string[] a = { };

        a = input.ToString().Split(';');


        if (a.Length > 3)
        {
            SDB = a[0].ToString();
            SName = a[1].ToString();
            Slog = a[2].ToString();
            SPassword = a[3].ToString();
        }
        else
        {
            SDB = a[0].ToString();
            SName = a[1].ToString();
            Slog = a[2].ToString();
        }
        return SPassword;
    }

    private void Get_Serverinfo()
    {
        string input = ConfigurationSettings.AppSettings["UbasysConnectionString"].ToString();
        string[] a = { };
        a = input.ToString().Split(';');
        if (a.Length > 2)
        {
            server_name = a[0].ToString();
            SLog = a[1].ToString();
            password = a[2].ToString();
        }
        else
        {
            server_name = a[0].ToString();
            SLog = a[1].ToString();
        }
    }
}

