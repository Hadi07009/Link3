using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

namespace LibraryPAY
{
    public class PayConnectionManager
    {
        private SqlCommand cmd;
        private SqlDataAdapter da;

        public PayConnectionManager()
        {

        }

        public static void setConnection()
        {
            LibraryPAY.Properties.Settings.Default["UBASYSConnectionString"] = ConfigurationSettings.AppSettings["UBASYSConnectionString"].ToString();
            LibraryPAY.Properties.Settings.Default["SCFConnectionString"] = ConfigurationSettings.AppSettings["SCFConnectionString"].ToString();

            LibraryPAY.Properties.Settings.Default.Save();
                        
        }

        private static SqlConnection GetConnection()
        {
            //string connStr = "Data Source = (local);Initial Catalog=AffordableAsiaDB;Integrated Security=True";
            string connStr = ConfigurationSettings.AppSettings["SPFConnectionString"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            return conn;
        }

        public static void SetCommandConnection(ref SqlCommand cmd)
        {
            if (cmd == null)
            {
                cmd = new SqlCommand();
                cmd.Connection = GetConnection();
            }
        }

        public static void SetCommandConnection(ref SqlCommand cmd, ref SqlDataAdapter da)
        {
            if (cmd == null)
                cmd = new SqlCommand();

            if (da == null)
                da = new SqlDataAdapter();

            cmd.Connection = GetConnection();
            da.SelectCommand = cmd;
        }

        public static void ReleaseCommandConnection(ref SqlCommand cmd)
        {
            if (cmd != null)
            {
                cmd.Connection.Dispose();
                cmd.Dispose();
            }
        }

        public static void ReleaseCommandConnection(ref SqlCommand cmd, ref SqlDataAdapter da)
        {
            if (cmd != null)
            {
                cmd.Connection.Dispose();
                cmd.Dispose();
            }

            if (da != null)
                da.Dispose();
        }
    }
}
