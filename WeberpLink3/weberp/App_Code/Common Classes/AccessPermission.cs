using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

    public class AccessPermission
    {
        public AccessPermission() { }

        public static DataTable GetData(string ConnectionString, string QueryStr)
        {
            SqlConnection sqlConn = null;
            DataTable dataTableObj = null;
            try
            {
                string selectQuery = QueryStr;
                sqlConn = new SqlConnection(ConnectionString);
                sqlConn.Open();
                SqlDataAdapter sqlDataAdapterObj = new SqlDataAdapter(selectQuery, sqlConn);
                dataTableObj = new DataTable();
                sqlDataAdapterObj.Fill(dataTableObj);
                
            }
            catch (SqlException sqlExceptionObject)
            {
               
            }
            catch (Exception exceptionObject)
            {
               
            }
            finally
            {
                if (sqlConn.State == System.Data.ConnectionState.Open)
                {
                    sqlConn.Close();
                }
            }
            return dataTableObj;
        }

        public static DataTable GetData(SqlCommand myCommand,string QueryStr)
        {
            DataTable dataTableObj = new DataTable();
            try
            {
                myCommand.CommandText= QueryStr;
                SqlDataAdapter sqlDataAdapterObj = new SqlDataAdapter();
                sqlDataAdapterObj.SelectCommand = myCommand;
                sqlDataAdapterObj.Fill(dataTableObj);
            }
            catch (SqlException sqlExceptionObject)
            {
               
            }
            catch (Exception exceptionObject)
            {
               
            }
            return dataTableObj;
        }

        public static DataTable GetCompanyByUserandNodeid(string ConnectionString, string Userid,string Nodeid)
        {
            SqlConnection sqlConn = null;
            DataTable dataTableObj = null;
            try
            {
                string selectQuery = Sqlgenerate.SqlGetCompanyByUserandNodeid(Userid, Nodeid);

                sqlConn = new SqlConnection(ConnectionString);
                sqlConn.Open();
                SqlDataAdapter sqlDataAdapterObj = new SqlDataAdapter(selectQuery, sqlConn);
                dataTableObj = new DataTable();
                sqlDataAdapterObj.Fill(dataTableObj);

            }           
            catch (Exception exceptionObject)
            {
            }
            finally
            {
                if (sqlConn.State == System.Data.ConnectionState.Open)
                {
                    sqlConn.Close();
                }
            }
            return dataTableObj;
        }

    }

