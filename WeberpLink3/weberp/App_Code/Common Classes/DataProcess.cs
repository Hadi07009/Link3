using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

    public class DataProcess
    {
        public DataProcess() { }

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

        public static string GetSingleValueFromtable(string ConnectionString, string TableName,string ColName, string WhereClause)
        {
            SqlConnection sqlConn = null;
            DataTable dataTableObj = null;
            string retValue="";
            try
            {
                string selectQuery = "select top 1 "+ ColName +" from "+ TableName +" " + WhereClause;
                sqlConn = new SqlConnection(ConnectionString);
                sqlConn.Open();
                SqlDataAdapter sqlDataAdapterObj = new SqlDataAdapter(selectQuery, sqlConn);
                dataTableObj = new DataTable();
                sqlDataAdapterObj.Fill(dataTableObj);

                if (dataTableObj.Rows.Count > 0)
                    retValue = dataTableObj.Rows[0][ColName].ToString();

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
            return retValue;
        }

        public static string GetSingleValueFromtable(string ConnectionString, string selectQuery)
        {
            SqlConnection sqlConn = null;
            DataTable dataTableObj = null;
            string retValue = "";
            try
            {
                sqlConn = new SqlConnection(ConnectionString);
                sqlConn.Open();
                SqlDataAdapter sqlDataAdapterObj = new SqlDataAdapter(selectQuery, sqlConn);
                dataTableObj = new DataTable();
                sqlDataAdapterObj.Fill(dataTableObj);

                if (dataTableObj.Rows.Count > 0)
                    retValue = dataTableObj.Rows[0][0].ToString();

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
            return retValue;
        }

        public static bool InsertQuery(string ConnectionString, string QueryStr)
        {
            SqlConnection sqlConn = null;
            int noOfRowsAffected = 0;
            try
            {   
                sqlConn = new SqlConnection(ConnectionString);
                sqlConn.Open();
                SqlCommand sqlCom = new SqlCommand(QueryStr, sqlConn);
                noOfRowsAffected = sqlCom.ExecuteNonQuery();
            }            
            catch (Exception exceptionObj)
            {
                return false;                
            }
            finally
            {
                if (sqlConn.State == System.Data.ConnectionState.Open)
                {
                    sqlConn.Close();
                }
            }

            if (noOfRowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
           
        }

        public static bool UpdateQuery(string ConnectionString, string QueryStr)
        {
            SqlConnection sqlConn = null;
            int noOfRowsAffected = 0;

            try
            {
                sqlConn = new SqlConnection(ConnectionString);
                sqlConn.Open();
                SqlCommand sqlCom = new SqlCommand(QueryStr, sqlConn);
                noOfRowsAffected = sqlCom.ExecuteNonQuery();
            }
            catch (SqlException sqlExceptionObject)
            {
               
            }
            catch (Exception exceptionObj)
            {
               
            }
            finally
            {
                if (sqlConn.State == System.Data.ConnectionState.Open)
                {
                    sqlConn.Close();
                }
            }

            if (noOfRowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool DeleteQuery(string ConnectionString, string QueryStr)
        {
            SqlConnection sqlConn = null;
            int noOfRowsAffected = 0;

            try
            {
                sqlConn = new SqlConnection(ConnectionString);
                sqlConn.Open();
                SqlCommand sqlCom = new SqlCommand(QueryStr, sqlConn);
                noOfRowsAffected = sqlCom.ExecuteNonQuery();
            }
            catch (SqlException sqlExceptionObject)
            {
                
            }
            catch (Exception exceptionObj)
            {
                
            }
            finally
            {
                if (sqlConn.State == System.Data.ConnectionState.Open)
                {
                    sqlConn.Close();
                }
            }

            if (noOfRowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ExecuteQuery(string ConnectionString, string QueryStr)
        {
            SqlConnection sqlConn = null;
            int noOfRowsAffected = 0;
            try
            {
                sqlConn = new SqlConnection(ConnectionString);
                sqlConn.Open();
                SqlCommand sqlCom = new SqlCommand(QueryStr, sqlConn);
                noOfRowsAffected = sqlCom.ExecuteNonQuery();
                return true;
            }
            catch (SqlException sqlExceptionObj)
            {
                if (sqlExceptionObj.Number == 2627)     //Violation of primary key Msg no =2627
                {
                    return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exceptionObj)
            {
                return false;
            }
            finally
            {
                if (sqlConn.State == System.Data.ConnectionState.Open)
                {
                    sqlConn.Close();
                }
            }
        }

        public static int ExecuteQueryNoofRoweffect(string ConnectionString, string QueryStr)
        {
            SqlConnection sqlConn = null;
            int noOfRowsAffected = 0;
            try
            {
                sqlConn = new SqlConnection(ConnectionString);
                sqlConn.Open();
                SqlCommand sqlCom = new SqlCommand(QueryStr, sqlConn);
                noOfRowsAffected = sqlCom.ExecuteNonQuery();
                return noOfRowsAffected;
            }
            catch (SqlException sqlExceptionObj)
            {
                if (sqlExceptionObj.Number == 2627)     //Violation of primary key Msg no =2627
                {
                    return 0;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception exceptionObj)
            {
                return 0;
            }
            finally
            {
                if (sqlConn.State == System.Data.ConnectionState.Open)
                {
                    sqlConn.Close();
                }
            }
        }

        public static bool ExecuteQuery(SqlCommand myCommand,string QueryStr)
        {
            try
            {
                myCommand.CommandText = QueryStr;
                myCommand.ExecuteNonQuery();
                return true;
            }
            catch (SqlException sqlExceptionObj)
            {
                if (sqlExceptionObj.Number == 2627)     //Violation of primary key Msg no =2627
                {
                    return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exceptionObj)
            {
                return false;
            }
        }
    }

