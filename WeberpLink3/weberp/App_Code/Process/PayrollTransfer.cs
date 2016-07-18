using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using LibraryDAL;


public class PayrollTransfer
    {
    public PayrollTransfer() { }

        public string SaveLeaveData(List<LeaveProcessHeader> lvphdrlst,SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessLeavedataSaveByAdmin] '" + ofproc.ApplicantId + "','" + ofproc.ActIndate + "','" + ofproc.SysIntime + "','" + ofproc.SysOuttime + "','" + ofproc.SysTotalhrs + "','" + ofproc.Leavetype + "'," + ofproc.NoofLeave + ",'" + ofproc.Leavedescription + "','" + ofproc.Remarks + "'";
                    myCommand.CommandTimeout = 600;
                    myCommand.ExecuteNonQuery();
                }

                retValue = "Data Saved Successful";

                return retValue;

            }
            catch (Exception)
            {
                return retValue;
            }
        }                  
        public string GetEmployeeLeaveBalance(string ConnectionStr,DateTime fDate,string empid)
        {
                      
            SqlCommand cmd = new SqlCommand();
            SqlConnection sqlConn = null;
            sqlConn = new SqlConnection(ConnectionStr);
            sqlConn.Open();
            cmd.Connection = sqlConn;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spProcessGetLeavebalance";

            cmd.Parameters.Add(new SqlParameter("@empid", SqlDbType.NVarChar)).Value = empid.ToString();
            cmd.Parameters.Add(new SqlParameter("@periodfrom", SqlDbType.DateTime)).Value = fDate;
            cmd.Parameters.Add(new SqlParameter("@LeaveType", SqlDbType.NVarChar)).Value = "ALL";

            cmd.ExecuteNonQuery();

            sqlConn.Close();

            return "OK";
        
        }
        public string GetTransactionNo(string ConnectionStr)
        {
            string rest;

            SqlConnection oConnection = new SqlConnection(ConnectionStr.ToString());
            oConnection.Open();
            SqlCommand cmd = new SqlCommand("spProcessGetTransactionNo", oConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            //SqlParameter operatorID = cmd.Parameters.Add("@operatorID", SqlDbType.NVarChar, 10);
            //operatorID.Value = "L3T593";

            //SqlParameter trndate = cmd.Parameters.Add("@trndate", SqlDbType.NVarChar, 22);
            //trndate.Value = "26/12/2012";

            // output parm
            SqlParameter outputStr = cmd.Parameters.Add("@outputStr", SqlDbType.NVarChar, 100);
            outputStr.Direction = ParameterDirection.Output;

            // return value
            SqlParameter returnnVal = cmd.Parameters.Add("returnnVal", SqlDbType.Int);
            returnnVal.Direction = ParameterDirection.ReturnValue;

            cmd.ExecuteNonQuery();

            if ((int)returnnVal.Value == 0)
            {
                rest = Convert.ToString(outputStr.Value);
            }
            else
            {
                rest = Convert.ToString(outputStr.Value);
            }

            return rest;

        }            
        public string SavePaymentDone(string ConnectionStr, string paymentrefno,string type,string pmtnar,string euserid)
        {
            string rest;

            SqlConnection oConnection = new SqlConnection(ConnectionStr.ToString());
            oConnection.Open();
            SqlCommand cmd = new SqlCommand("spProcessPaymentDone", oConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter refno = cmd.Parameters.Add("@paymentrefno", SqlDbType.NVarChar, 22);
            refno.Value = paymentrefno.ToString();

            SqlParameter ptype = cmd.Parameters.Add("@ptype", SqlDbType.NVarChar, 22);
            ptype.Value = type.ToString();

            SqlParameter pnar = cmd.Parameters.Add("@paynarration", SqlDbType.NVarChar, 22);
            pnar.Value = pmtnar.ToString();

            SqlParameter userid = cmd.Parameters.Add("@entryuserid", SqlDbType.NVarChar, 22);
            userid.Value = euserid.ToString();

            // output parm
            SqlParameter outputStr = cmd.Parameters.Add("@outputStr", SqlDbType.NVarChar, 100);
            outputStr.Direction = ParameterDirection.Output;

            // return value
            SqlParameter returnnVal = cmd.Parameters.Add("returnnVal", SqlDbType.Int);
            returnnVal.Direction = ParameterDirection.ReturnValue;

            cmd.ExecuteNonQuery();

            if ((int)returnnVal.Value == 0)
            {
                rest = Convert.ToString(outputStr.Value);
            }
            else
            {
                rest = Convert.ToString(outputStr.Value);
            }

            return rest;

        }       
        public DataTable GetApprovalPermissionIntoDDDL(string ConnectionStr, string actingpersonid, string empcode, string Processid, string flowid, int processlevelid)
        {

            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlConnection sqlConn = null;
            sqlConn = new SqlConnection(ConnectionStr);
            sqlConn.Open();
            cmd.Connection = sqlConn;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spProcessGetApprovalPermissionByActingpersonIDIntoDropDownList";

            cmd.Parameters.Add(new SqlParameter("@actingpersonid", SqlDbType.NVarChar)).Value = actingpersonid.ToString();
            cmd.Parameters.Add(new SqlParameter("@empcode", SqlDbType.NVarChar)).Value = empcode.ToString();
            cmd.Parameters.Add(new SqlParameter("@processid", SqlDbType.NVarChar)).Value = Processid.ToString();
            cmd.Parameters.Add(new SqlParameter("@flowid", SqlDbType.NVarChar)).Value = flowid.ToString();
            cmd.Parameters.Add(new SqlParameter("@ProcessLevelID", SqlDbType.Int)).Value = processlevelid;


            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            sqlConn.Close();

            return dt;
        }
       
        public string TransferJournaltoNewAccounts(SqlCommand myCommand,PayrollTransferHeader payhdr)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {
                myCommand.CommandText = "exec [spTransferPayrollJournalToNewAccounts] '" + payhdr.rPayMonth + "','" + payhdr.rPayYear + "'";
                myCommand.CommandTimeout = 600;
                myCommand.ExecuteNonQuery();
               
                retValue = "Data Saved Successful";

                return retValue;

            }
            catch (Exception)
            {
                return retValue;
            }
        }              

    }

