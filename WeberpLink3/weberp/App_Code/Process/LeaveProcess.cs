using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using LibraryDAL;


public class LeaveProcess
    {
    public LeaveProcess() { }

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
        public string SaveInitiateLeaveProcessData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessInitiateLeaveProcess] '" + ofproc.ApplicantId + "','" + ofproc.ProcessId + "','" + ofproc.FlowId + "'," + ofproc.ProcesslevelId + ",'" + ofproc.ApplicantId + "','" + ofproc.Remarks + "','" + ofproc.EntryUserid + "'," + ofproc.ActiontypeId + ",'" + ofproc.ClaiminDdate + "','" + ofproc.SysIntime + "','" + ofproc.SysOuttime + "','" + ofproc.SysTotalhrs + "'," + ofproc.NoofLeave + ",'" + ofproc.Leavetype + "'," + ofproc.SL + ",'" + ofproc.TransactionNo + "','" + ofproc.ResponsiblepersonId + "','" + ofproc.MovementNo + "'";
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

        public string SaveInitiateOffdayProcessData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessInitiateOffdayProcess] '" + ofproc.ApplicantId + "','" + ofproc.ProcessId + "','" + ofproc.FlowId + "'," + ofproc.ProcesslevelId + ",'" + ofproc.ApplicantId + "','" + ofproc.Remarks + "','" + ofproc.EntryUserid + "'," + ofproc.ActiontypeId + ",'" + ofproc.ClaiminDdate + "','" + ofproc.SysIntime + "','" + ofproc.SysOuttime + "','" + ofproc.SysTotalhrs + "','" + ofproc.ActIntime + "','" + ofproc.ActOuttime + "'," + ofproc.SL + ",'" + ofproc.TransactionNo + "','" + ofproc.ResponsiblepersonId + "','" + ofproc.MovementNo + "'";
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

        public string SaveInitiateOvertimeProcessData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessInitiateOvertimeProcess] '" + ofproc.ApplicantId + "','" + ofproc.ProcessId + "','" + ofproc.FlowId + "'," + ofproc.ProcesslevelId + ",'" + ofproc.ApplicantId + "','" + ofproc.Remarks + "','" + ofproc.EntryUserid + "'," + ofproc.ActiontypeId + ",'" + ofproc.ClaiminDdate + "','" + ofproc.SysIntime + "','" + ofproc.SysOuttime + "','" + ofproc.SysTotalhrs + "','" + ofproc.ActIntime + "','" + ofproc.ActOuttime + "'," + ofproc.SL + ",'" + ofproc.TransactionNo + "','" + ofproc.ResponsiblepersonId + "','" + ofproc.MovementNo + "','" + ofproc.ActOutdate + "'";
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
        public string SaveInitiateAttendanceProcessData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessInitiateAttendanceProcess] '" + ofproc.ApplicantId + "','" + ofproc.ProcessId + "','" + ofproc.FlowId + "'," + ofproc.ProcesslevelId + ",'" + ofproc.ApplicantId + "','" + ofproc.Remarks + "','" + ofproc.EntryUserid + "'," + ofproc.ActiontypeId + ",'" + ofproc.ClaiminDdate + "','" + ofproc.SysIntime + "','" + ofproc.SysOuttime + "','" + ofproc.SysTotalhrs + "','" + ofproc.ActIntime + "','" + ofproc.ActOuttime + "'," + ofproc.SL + ",'" + ofproc.TransactionNo + "','" + ofproc.ResponsiblepersonId + "','" + ofproc.MovementNo + "','" + ofproc.ActOutdate + "'";
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

        public string SaveApproveLeaveProcessData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessApproveLeaveProcess] '" + ofproc.ApplicantId + "','" + ofproc.ProcessId + "','" + ofproc.FlowId + "','" + ofproc.ActingpersonId + "','" + ofproc.Remarks + "'," + ofproc.ActiontypeId + ",'" + ofproc.ClaiminDdate + "','" + ofproc.SysIntime + "','" + ofproc.SysOuttime + "','" + ofproc.SysTotalhrs + "'," + ofproc.NoofLeave + ",'" + ofproc.Leavetype + "'," + ofproc.TransactionLineNo + ",'" + ofproc.TransactionNo + "'," + ofproc.ProcesslevelId + "";
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

        public string SaveApproveEarnedLeaveProcessData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessApproveEarnedLeaveProcess] '" + ofproc.ApplicantId + "','" + ofproc.ProcessId + "','" + ofproc.FlowId + "','" + ofproc.ActingpersonId + "','" + ofproc.Remarks + "'," + ofproc.ActiontypeId + ",'" + ofproc.ClaiminDdate + "','" + ofproc.SysIntime + "','" + ofproc.SysOuttime + "','" + ofproc.SysTotalhrs + "','" + ofproc.ActIntime + "','" + ofproc.ActOuttime + "','" + ofproc.Acthrs + "'," + ofproc.NoofLeave + ",'" + ofproc.Leavetype + "'," + ofproc.TransactionLineNo + ",'" + ofproc.TransactionNo + "'," + ofproc.ProcesslevelId + "";
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


        public string SaveRejectELProcessData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessRejecteELProcess] '" + ofproc.ProcessId + "','" + ofproc.FlowId + "','" + ofproc.ActingpersonId + "','" + ofproc.Remarks + "'," + ofproc.ActiontypeId + "," + ofproc.TransactionLineNo + ",'" + ofproc.TransactionNo + "'," + ofproc.ProcesslevelId + "";
                    myCommand.CommandTimeout = 600;
                    myCommand.ExecuteNonQuery();
                }

                retValue = "Data Reject Successful";

                return retValue;

            }
            catch (Exception)
            {
                return retValue;
            }
        }
        public string CancelELProcessData(List<OffdayProcessHeader> ofphdrlst, SqlCommand myCommand)
        {
            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {
                foreach (OffdayProcessHeader ofproc in ofphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessCancelPaymentProcessApprovedData] '" + ofproc.ApplicantId + "','" + ofproc.ProcessId + "','" + ofproc.FlowId + "','" + ofproc.Type + "','" + ofproc.Transactionno + "','" + ofproc.EntryUserid + "'";
                    myCommand.CommandTimeout = 600;
                    myCommand.ExecuteNonQuery();
                }

                retValue = "Data Cancel Successful";
                return retValue;
            }
            catch (Exception)
            {
                return retValue;
            }
        }

        public string RejectELPaymentAfterPaymentProcess(string paymentrefence, string empid, string Narration, string EntryUserid, SqlCommand myCommand)
        {
            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                myCommand.CommandText = "exec [spProcessRejectPaymentAfterProcessing] '" + paymentrefence + "','" + empid + "','" + Narration + "','" + EntryUserid + "'";
                myCommand.CommandTimeout = 600;
                myCommand.ExecuteNonQuery();

                retValue = "Payment Reject Successful";
                return retValue;
            }
            catch (Exception)
            {
                return retValue;
            }
        }



        public string SaveApproveOffdayProcessData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessApproveOffdayProcess] '" + ofproc.ApplicantId + "','" + ofproc.ProcessId + "','" + ofproc.FlowId + "','" + ofproc.ActingpersonId + "','" + ofproc.Remarks + "'," + ofproc.ActiontypeId + ",'" + ofproc.ClaiminDdate + "','" + ofproc.SysIntime + "','" + ofproc.SysOuttime + "','" + ofproc.SysTotalhrs + "','" + ofproc.ActIntime + "','" + ofproc.ActOuttime + "','" + ofproc.Acthrs + "'," + ofproc.NoofLeave + ",'" + ofproc.Leavetype + "'," + ofproc.TransactionLineNo + ",'" + ofproc.TransactionNo + "'," + ofproc.ProcesslevelId + "";
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
        public string SaveLockedApprovedOffdayProcessData(List<OffdayProcessHeader> ofphdrlst, SqlCommand myCommand)
        {
            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {
                foreach (OffdayProcessHeader ofproc in ofphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessLockedApproveDataforPayment] '" + ofproc.ApplicantId + "','" + ofproc.ProcessId + "','" + ofproc.FlowId + "','" + ofproc.ActIndate + "','" + ofproc.ActOutdate + "','" + ofproc.Type + "','" + ofproc.EntryUserid + "'";
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
        public string SavePaymentProcessOffdayProcessData(List<OffdayProcessHeader> ofphdrlst, SqlCommand myCommand,string paymentref)
        {
            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {
                foreach (OffdayProcessHeader ofproc in ofphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessPaymentProcessApprovedData] '" + ofproc.ApplicantId + "','" + ofproc.ProcessId + "','" + ofproc.FlowId + "','" + ofproc.ActIndate + "','" + ofproc.ActOutdate + "','" + ofproc.Type + "','" + ofproc.Quantity + "','" + ofproc.UnitRate + "'," + ofproc.Rate + ",'" + ofproc.MaximumLimit + "','" + ofproc.PayableAmount + "','" + ofproc.PaymentDate + "','" + ofproc.PaymentNarration + "','" + ofproc.EntryUserid + "','" + paymentref.ToString() + "'";
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
        public string SaveForwardLeaveProcessData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessForwardLeaveProcess] '" + ofproc.ProcessId + "','" + ofproc.FlowId + "','" + ofproc.ActingpersonId + "','" + ofproc.Remarks + "'," + ofproc.ActiontypeId + "," + ofproc.TransactionLineNo + ",'" + ofproc.TransactionNo + "'," + ofproc.ProcesslevelId + "";
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
        public string SaveReturnLeaveProcessData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessReturnLeaveProcess] '" + ofproc.ProcessId + "','" + ofproc.FlowId + "','" + ofproc.ActingpersonId + "','" + ofproc.Remarks + "'," + ofproc.ActiontypeId + "," + ofproc.TransactionLineNo + ",'" + ofproc.TransactionNo + "'," + ofproc.ProcesslevelId + "";
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
        public string SaveReturnOffdayProcessData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessReturnOffdayProcess] '" + ofproc.ProcessId + "','" + ofproc.FlowId + "','" + ofproc.ActingpersonId + "','" + ofproc.Remarks + "'," + ofproc.ActiontypeId + "," + ofproc.TransactionLineNo + ",'" + ofproc.TransactionNo + "'," + ofproc.ProcesslevelId + "";
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

        public string SaveForwardOffdayProcessData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessForwardOffdayProcess] '" + ofproc.ProcessId + "','" + ofproc.FlowId + "','" + ofproc.ActingpersonId + "','" + ofproc.Remarks + "'," + ofproc.ActiontypeId + "," + ofproc.TransactionLineNo + ",'" + ofproc.TransactionNo + "'," + ofproc.ProcesslevelId + "";
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
        public string SaveForwardEarnedLeaveProcessData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessForwardEarnedLeaveProcess] '" + ofproc.ProcessId + "','" + ofproc.FlowId + "','" + ofproc.ActingpersonId + "','" + ofproc.Remarks + "'," + ofproc.ActiontypeId + "," + ofproc.TransactionLineNo + ",'" + ofproc.TransactionNo + "'," + ofproc.ProcesslevelId + "";
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
        public string SaveRejectLeaveProcessData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessRejecteLeaveProcess] '" + ofproc.ProcessId + "','" + ofproc.FlowId + "','" + ofproc.ActingpersonId + "','" + ofproc.Remarks + "'," + ofproc.ActiontypeId + "," + ofproc.TransactionLineNo + ",'" + ofproc.TransactionNo + "'," + ofproc.ProcesslevelId + "";
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

        public string SavePaymentDoneForEL(string ConnectionStr, string paymentrefno, string type, string pmtnar, string euserid, string empid)
        {
            string rest;

            SqlConnection oConnection = new SqlConnection(ConnectionStr.ToString());
            oConnection.Open();
            SqlCommand cmd = new SqlCommand("spProcessPaymentDoneForEL", oConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter refno = cmd.Parameters.Add("@paymentrefno", SqlDbType.NVarChar, 22);
            refno.Value = paymentrefno.ToString();

            SqlParameter ptype = cmd.Parameters.Add("@ptype", SqlDbType.NVarChar, 22);
            ptype.Value = type.ToString();

            SqlParameter pnar = cmd.Parameters.Add("@paynarration", SqlDbType.NVarChar, 500);
            pnar.Value = pmtnar.ToString();

            SqlParameter userid = cmd.Parameters.Add("@entryuserid", SqlDbType.NVarChar, 22);
            userid.Value = euserid.ToString();

            SqlParameter ApplicantId = cmd.Parameters.Add("@empid", SqlDbType.NVarChar, 22);
            ApplicantId.Value = empid.ToString();

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


        public string SaveRejectOffdayProcessData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessRejecteOffdayProcess] '" + ofproc.ProcessId + "','" + ofproc.FlowId + "','" + ofproc.ActingpersonId + "','" + ofproc.Remarks + "'," + ofproc.ActiontypeId + "," + ofproc.TransactionLineNo + ",'" + ofproc.TransactionNo + "'," + ofproc.ProcesslevelId + "";
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
        public string GetMovementNo(string ConnectionStr)
        {
            string rest;

            SqlConnection oConnection = new SqlConnection(ConnectionStr.ToString());
            oConnection.Open();
            SqlCommand cmd = new SqlCommand("spProcessGetMovementNO", oConnection);
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
        public string GetPaymentReferenceNo(string ConnectionStr,string paymentdate)
        {
            string rest;

            SqlConnection oConnection = new SqlConnection(ConnectionStr.ToString());
            oConnection.Open();
            SqlCommand cmd = new SqlCommand("spProcessGetPaymentRefNo", oConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter trndate = cmd.Parameters.Add("@paydate", SqlDbType.NVarChar, 22);
            trndate.Value =paymentdate.ToString();

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

        public DataTable GetApprovalPermission(string ConnectionStr,string actingpersonid, string empcode, string Processid, string flowid,int processlevelid)
        {

            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlConnection sqlConn = null;
            sqlConn = new SqlConnection(ConnectionStr);
            sqlConn.Open();
            cmd.Connection = sqlConn;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spProcessGetApprovalPermissionByActingpersonID";

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

        public string SaveApproveOvertimeProcessData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessApproveOvertimeProcess] '" + ofproc.ApplicantId + "','" + ofproc.ProcessId + "','" + ofproc.FlowId + "','" + ofproc.ActingpersonId + "','" + ofproc.Remarks + "'," + ofproc.ActiontypeId + ",'" + ofproc.ClaiminDdate + "','" + ofproc.SysIntime + "','" + ofproc.SysOuttime + "','" + ofproc.SysTotalhrs + "'," + ofproc.NoofLeave + ",'" + ofproc.Leavetype + "'," + ofproc.TransactionLineNo + ",'" + ofproc.TransactionNo + "'," + ofproc.ProcesslevelId + "";
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


        public string SaveApproveAttendanceProcessData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessApproveAttendanceProcess] '" + ofproc.ApplicantId + "','" + ofproc.ProcessId + "','" + ofproc.FlowId + "','" + ofproc.ActingpersonId + "','" + ofproc.Remarks + "'," + ofproc.ActiontypeId + ",'" + ofproc.ClaiminDdate + "','" + ofproc.SysIntime + "','" + ofproc.SysOuttime + "','" + ofproc.SysTotalhrs + "'," + ofproc.NoofLeave + ",'" + ofproc.Leavetype + "'," + ofproc.TransactionLineNo + ",'" + ofproc.TransactionNo + "'," + ofproc.ProcesslevelId + "";
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
        public string SaveApproveBusinessLeaveProcessData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessApproveBusinessLeaveProcess] '" + ofproc.ApplicantId + "','" + ofproc.ProcessId + "','" + ofproc.FlowId + "','" + ofproc.ActingpersonId + "','" + ofproc.Remarks + "'," + ofproc.ActiontypeId + ",'" + ofproc.ClaiminDdate + "','" + ofproc.SysIntime + "','" + ofproc.SysOuttime + "','" + ofproc.SysTotalhrs + "'," + ofproc.NoofLeave + ",'" + ofproc.Leavetype + "'," + ofproc.TransactionLineNo + ",'" + ofproc.TransactionNo + "'," + ofproc.ProcesslevelId + "";
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

        public string SaveForwardOvertimeProcessData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessForwardOvertimeProcess] '" + ofproc.ProcessId + "','" + ofproc.FlowId + "','" + ofproc.ActingpersonId + "','" + ofproc.Remarks + "'," + ofproc.ActiontypeId + "," + ofproc.TransactionLineNo + ",'" + ofproc.TransactionNo + "'," + ofproc.ProcesslevelId + "";
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

        public string SaveForwardAttendanceProcessData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessForwardAttendanceProcess] '" + ofproc.ProcessId + "','" + ofproc.FlowId + "','" + ofproc.ActingpersonId + "','" + ofproc.Remarks + "'," + ofproc.ActiontypeId + "," + ofproc.TransactionLineNo + ",'" + ofproc.TransactionNo + "'," + ofproc.ProcesslevelId + "";
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

        public string SaveReturnOvertimeProcessData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessReturnOvertimeProcess] '" + ofproc.ProcessId + "','" + ofproc.FlowId + "','" + ofproc.ActingpersonId + "','" + ofproc.Remarks + "'," + ofproc.ActiontypeId + "," + ofproc.TransactionLineNo + ",'" + ofproc.TransactionNo + "'," + ofproc.ProcesslevelId + "";
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
        public string SaveRejectOvertimeProcessData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessRejecteOvertimeProcess] '" + ofproc.ProcessId + "','" + ofproc.FlowId + "','" + ofproc.ActingpersonId + "','" + ofproc.Remarks + "'," + ofproc.ActiontypeId + "," + ofproc.TransactionLineNo + ",'" + ofproc.TransactionNo + "'," + ofproc.ProcesslevelId + "";
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

        public string SaveRejectAttendanceProcessData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessRejecteAttendanceProcess] '" + ofproc.ProcessId + "','" + ofproc.FlowId + "','" + ofproc.ActingpersonId + "','" + ofproc.Remarks + "'," + ofproc.ActiontypeId + "," + ofproc.TransactionLineNo + ",'" + ofproc.TransactionNo + "'," + ofproc.ProcesslevelId + "";
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

        public string SaveInitiateNightProcessData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessInitiateNightProcess] '" + ofproc.ApplicantId + "','" + ofproc.ProcessId + "','" + ofproc.FlowId + "'," + ofproc.ProcesslevelId + ",'" + ofproc.ApplicantId + "','" + ofproc.Remarks + "','" + ofproc.EntryUserid + "'," + ofproc.ActiontypeId + ",'" + ofproc.ClaiminDdate + "','" + ofproc.SysIntime + "','" + ofproc.SysOuttime + "','" + ofproc.SysTotalhrs + "','" + ofproc.ActIntime + "','" + ofproc.ActOuttime + "'," + ofproc.SL + ",'" + ofproc.TransactionNo + "','" + ofproc.ResponsiblepersonId + "','" + ofproc.MovementNo + "','" + ofproc.ActOutdate + "'";
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
       
        public string SaveForwardNightProcessData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessForwardNightProcess] '" + ofproc.ProcessId + "','" + ofproc.FlowId + "','" + ofproc.ActingpersonId + "','" + ofproc.Remarks + "'," + ofproc.ActiontypeId + "," + ofproc.TransactionLineNo + ",'" + ofproc.TransactionNo + "'," + ofproc.ProcesslevelId + "";
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

        public string SaveReturnNightProcessData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessReturnNightProcess] '" + ofproc.ProcessId + "','" + ofproc.FlowId + "','" + ofproc.ActingpersonId + "','" + ofproc.Remarks + "'," + ofproc.ActiontypeId + "," + ofproc.TransactionLineNo + ",'" + ofproc.TransactionNo + "'," + ofproc.ProcesslevelId + "";
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


        public string SaveRejectNightProcessData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessRejecteNightProcess] '" + ofproc.ProcessId + "','" + ofproc.FlowId + "','" + ofproc.ActingpersonId + "','" + ofproc.Remarks + "'," + ofproc.ActiontypeId + "," + ofproc.TransactionLineNo + ",'" + ofproc.TransactionNo + "'," + ofproc.ProcesslevelId + "";
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

        public string SaveApproveNightProcessData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessApproveNightProcess]'" + ofproc.ApplicantId + "','" + ofproc.ProcessId + "','" + ofproc.FlowId + "','" + ofproc.ActingpersonId + "','" + ofproc.Remarks + "'," + ofproc.ActiontypeId + ",'" + ofproc.ClaiminDdate + "','" + ofproc.SysIntime + "','" + ofproc.SysOuttime + "','" + ofproc.SysTotalhrs + "'," + ofproc.NoofLeave + ",'" + ofproc.Leavetype + "'," + ofproc.TransactionLineNo + ",'" + ofproc.TransactionNo + "'," + ofproc.ProcesslevelId + "";
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

        public void EmpgrddetSave(string ConnectionStr, string empcode, string grade)
        {                       
            SqlCommand cmd = new SqlCommand();
            SqlConnection sqlConn = null;
            sqlConn = new SqlConnection(ConnectionStr);
            sqlConn.Open();
            cmd.Connection = sqlConn;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "EmpBasicInformationInitiateIntohrms_emp_grd_det";

            cmd.Parameters.Add(new SqlParameter("@det_empid", SqlDbType.NVarChar)).Value = empcode.ToString();
            cmd.Parameters.Add(new SqlParameter("@det_grade", SqlDbType.NVarChar)).Value = grade.ToString();
           
            cmd.ExecuteNonQuery();
           
            sqlConn.Close();
 
        }

        #region ShiftAllocation
        public void DeletePreviousAllcatedShift(SqlCommand myCommand, string DepId, string DateForAllocation)
        {
            myCommand.CommandText = "exec [spShiftAllocationTransformAll] '" + DepId + "','" + DateForAllocation + "'";
            myCommand.CommandTimeout = 0;
            myCommand.ExecuteNonQuery();
        }

        public void DeletePreviousAllcatedShiftByemployeeID(SqlCommand myCommand, string Empid, string Datefrom,string Dateto)
        {
            myCommand.CommandText = "exec [spShiftAllocationTransformByempid] '" + Empid + "','" + Datefrom + "','" + Dateto + "'";
            myCommand.CommandTimeout = 0;
            myCommand.ExecuteNonQuery();
        }

        public string SaveShiftAllocationData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand, string DepId, int shiftid, string DateForAllocation)
        {
            string retValue = "";
            try
            {              
                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {

                    myCommand.CommandText = "exec [spShiftAllocationInsert] '" + ofproc.DateForSA + "','" + ofproc.EmpID + "'," + ofproc.ShiftID + ",'" + ofproc.InTime + "','" + ofproc.OutTime + "'";
                    myCommand.CommandTimeout = 0;
                    //CommandTimeout = 0;
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

        public string SaveHolidayAllocationData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand, string DepId, int shiftid, string DateForAllocation)
        {
            string retValue = "";
            try
            {
                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {

                    myCommand.CommandText = "exec [spHolidayAllocationInsert] '" + ofproc.DateForSA + "','" + ofproc.EmpID + "'," + ofproc.ShiftID + ",'" + ofproc.InTime + "','" + ofproc.OutTime + "'";
                    myCommand.CommandTimeout = 0;
                    //CommandTimeout = 0;
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

        public string SaveApproveAttendance(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand,string db)
        {
            string retValue = "";
            try
            {
                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {

                    myCommand.CommandText = "exec [spSaveApprovedAttendance] '" + ofproc.EmpID + "','" + ofproc.ClaiminDdate + "','" + db + "'";
                    myCommand.CommandTimeout = 0;
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

        public string SaveManualApproveAttendance(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand, string db)
        {
            string retValue = "";
            try
            {
                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {

                    myCommand.CommandText = "exec [spSaveManualApprovedAttendance] '" + ofproc.EmpID + "','" + ofproc.ClaiminDdate + "','" + ofproc.ActIntime + "','" + ofproc.ActOuttime + "','" + ofproc.LeaveRemarks + "','" + db + "','" + ofproc.EntryUserid + "'";
                    myCommand.CommandTimeout = 0;
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
        public string DeleteAttendance(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand, string db)
        {
            string retValue = "";
            try
            {
                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {

                    myCommand.CommandText = "exec [spDeleteManualApprovedAttendance] '" + ofproc.EmpID + "','" + ofproc.ClaiminDdate + "','" + ofproc.ActIntime + "','" + ofproc.ActOuttime + "','" + db + "','" + ofproc.Remarks + "','" + ofproc.EntryUserid + "'";
                    myCommand.CommandTimeout = 0;
                    myCommand.ExecuteNonQuery();
                }
                retValue = "Data delete Successful";
                return retValue;
            }
            catch (Exception)
            {
                return retValue;
            }
        }

        public DataTable GetAllocatedShifrRecord(string ConnectionStr, string DateOfAllocation, string DeptId)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlConnection sqlConn = null;
            sqlConn = new SqlConnection(ConnectionStr);
            sqlConn.Open();
            cmd.Connection = sqlConn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spShiftAllocationShow";
            cmd.Parameters.Add(new SqlParameter("@tDate", SqlDbType.NVarChar)).Value = DateOfAllocation.ToString();
            cmd.Parameters.Add(new SqlParameter("@DepID", SqlDbType.NVarChar)).Value = DeptId.ToString();
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            sqlConn.Close();
            return dt;
        }

        public DataTable GetAllocatedHolidayRecord(string ConnectionStr, string DateOfAllocation, string DeptId)
        {

            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlConnection sqlConn = null;
            sqlConn = new SqlConnection(ConnectionStr);
            sqlConn.Open();
            cmd.Connection = sqlConn;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spShiftAllocationShow";

            cmd.Parameters.Add(new SqlParameter("@tDate", SqlDbType.NVarChar)).Value = DateOfAllocation.ToString();
            cmd.Parameters.Add(new SqlParameter("@DepID", SqlDbType.NVarChar)).Value = DeptId.ToString();
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            sqlConn.Close();

            return dt;
        }

        public string SaveInitiateOffdayProcessDataSpecial(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessInitiateOffdayProcessSpecial] '" + ofproc.ApplicantId + "','" + ofproc.ProcessId + "','" + ofproc.FlowId + "'," + ofproc.ProcesslevelId + ",'" + ofproc.ApplicantId + "','" + ofproc.Remarks + "','" + ofproc.EntryUserid + "'," + ofproc.ActiontypeId + ",'" + ofproc.ClaiminDdate + "','" + ofproc.SysIntime + "','" + ofproc.SysOuttime + "','" + ofproc.SysTotalhrs + "','" + ofproc.ActIntime + "','" + ofproc.ActOuttime + "'," + ofproc.SL + ",'" + ofproc.TransactionNo + "','" + ofproc.ResponsiblepersonId + "','" + ofproc.MovementNo + "'," + ofproc.noOfDays + "";
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
        public string SaveInitiateEarnedLeaveProcessData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessInitiateEarnedLeaveProcess] '" + ofproc.ApplicantId + "','" + ofproc.ProcessId + "','" + ofproc.FlowId + "'," + ofproc.ProcesslevelId + ",'" + ofproc.ApplicantId + "','" + ofproc.Remarks + "','" + ofproc.EntryUserid + "'," + ofproc.ActiontypeId + ",'" + ofproc.ClaiminDdate + "','" + ofproc.SysIntime + "','" + ofproc.SysOuttime + "','" + ofproc.SysTotalhrs + "'," + ofproc.NoofLeave + ",'" + ofproc.Leavetype + "'," + ofproc.SL + ",'" + ofproc.TransactionNo + "','" + ofproc.ResponsiblepersonId + "','" + ofproc.MovementNo + "'";
                    myCommand.CommandTimeout = 600;
                    myCommand.ExecuteNonQuery();
                }

                retValue = "Earned Leave Application has been Successful";

                return retValue;

            }
            catch (Exception)
            {
                return retValue;
            }
        }

        public string SaveApproveEarnedLeaveProcessDataDirectly(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessInitiateAndApproveEarnedLeaveProcessDirectly] '" + ofproc.ApplicantId + "','" + ofproc.ProcessId + "','" + ofproc.FlowId + "','" + ofproc.ActingpersonId + "','" + ofproc.Remarks + "'," + ofproc.ActiontypeId + ",'" + ofproc.ClaiminDdate + "'," + ofproc.NoofLeave + ",'" + ofproc.Leavetype + "'," + ofproc.TransactionLineNo + ",'" + ofproc.TransactionNo + "'," + ofproc.ProcesslevelId + "";
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
        public DataTable GetAllocatedEmpId(string ConnectionStr, string EmpId, int ShiftId, string DateOfAllocation)
        {

            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlConnection sqlConn = null;
            sqlConn = new SqlConnection(ConnectionStr);
            sqlConn.Open();
            cmd.Connection = sqlConn;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spShiftAllocationForCheck";

            cmd.Parameters.Add(new SqlParameter("@EmpID", SqlDbType.NVarChar)).Value = EmpId.ToString();
            //cmd.Parameters.Add(new SqlParameter("@shiftid", SqlDbType.Int)).Value = ShiftId.ToString();
            cmd.Parameters.Add(new SqlParameter("@tDate", SqlDbType.NVarChar)).Value = DateOfAllocation.ToString();
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            sqlConn.Close();

            return dt;
        }

        public DataTable GetAllocatedEmpId(string ConnectionStr, string EmpId, string DateOfAllocation)
        {

            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlConnection sqlConn = null;
            sqlConn = new SqlConnection(ConnectionStr);
            sqlConn.Open();
            cmd.Connection = sqlConn;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spShiftAllocationForCheck";

            cmd.Parameters.Add(new SqlParameter("@EmpID", SqlDbType.NVarChar)).Value = EmpId.ToString();
            //cmd.Parameters.Add(new SqlParameter("@shiftid", SqlDbType.Int)).Value = ShiftId.ToString();
            cmd.Parameters.Add(new SqlParameter("@tDate", SqlDbType.NVarChar)).Value = DateOfAllocation.ToString();
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            sqlConn.Close();

            return dt;
        }

        public DataTable GetHolidayAllocatedEmpId(string ConnectionStr, string EmpId, int ShiftId, string DateOfAllocation)
        {

            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlConnection sqlConn = null;
            sqlConn = new SqlConnection(ConnectionStr);
            sqlConn.Open();
            cmd.Connection = sqlConn;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spHolidayAllocationForCheck";

            cmd.Parameters.Add(new SqlParameter("@EmpID", SqlDbType.NVarChar)).Value = EmpId.ToString();
            //cmd.Parameters.Add(new SqlParameter("@shiftid", SqlDbType.Int)).Value = ShiftId.ToString();
            cmd.Parameters.Add(new SqlParameter("@tDate", SqlDbType.NVarChar)).Value = DateOfAllocation.ToString();
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            sqlConn.Close();

            return dt;
        }

        public DataTable GetAllocatedShiftbyempid(string ConnectionStr, string EmpId, DateTime fdate, DateTime ldate)
        {

            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlConnection sqlConn = null;
            sqlConn = new SqlConnection(ConnectionStr);
            sqlConn.Open();
            cmd.Connection = sqlConn;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spGetAllocatedShiftbyempid";

            cmd.Parameters.Add(new SqlParameter("@EmpID", SqlDbType.NVarChar)).Value = EmpId.ToString();
            cmd.Parameters.Add(new SqlParameter("@fdate", SqlDbType.DateTime)).Value = fdate;
            cmd.Parameters.Add(new SqlParameter("@ldate", SqlDbType.DateTime)).Value = ldate;
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            sqlConn.Close();

            return dt;
        }

        public string spCxecuteAbsentListReport(string ConnectionStr, DateTime fDate, DateTime lDate, string deptid, string empid,int cond,string nodeid,string db)
        {

            SqlCommand cmd = new SqlCommand();
            SqlConnection sqlConn = null;
            sqlConn = new SqlConnection(ConnectionStr);
            sqlConn.Open();
            cmd.Connection = sqlConn;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spAbsentListReport";
            cmd.Parameters.Add(new SqlParameter("@date1", SqlDbType.DateTime)).Value = fDate;
            cmd.Parameters.Add(new SqlParameter("@date2", SqlDbType.DateTime)).Value = lDate;
            cmd.Parameters.Add(new SqlParameter("@deptid", SqlDbType.NVarChar)).Value = deptid;
            cmd.Parameters.Add(new SqlParameter("@cond", SqlDbType.Int)).Value = cond;
            cmd.Parameters.Add(new SqlParameter("@nodeid", SqlDbType.NVarChar)).Value = nodeid.ToString();
            cmd.Parameters.Add(new SqlParameter("@db", SqlDbType.NVarChar)).Value = db.ToString();
            cmd.Parameters.Add(new SqlParameter("@userid", SqlDbType.NVarChar)).Value = empid.ToString();
            

            cmd.ExecuteNonQuery();

            sqlConn.Close();

            return "OK";

        }

        public string spCxecuteAbsentListAttendance(string ConnectionStr, DateTime fDate, DateTime lDate, string deptid, string empid, int cond, string nodeid,string db)
        {

            SqlCommand cmd = new SqlCommand();
            SqlConnection sqlConn = null;
            sqlConn = new SqlConnection(ConnectionStr);
            sqlConn.Open();
            cmd.Connection = sqlConn;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spAbsentListAttendance";
            cmd.Parameters.Add(new SqlParameter("@date1", SqlDbType.DateTime)).Value = fDate;
            cmd.Parameters.Add(new SqlParameter("@date2", SqlDbType.DateTime)).Value = lDate;
            cmd.Parameters.Add(new SqlParameter("@deptid", SqlDbType.NVarChar)).Value = deptid;
            cmd.Parameters.Add(new SqlParameter("@cond", SqlDbType.Int)).Value = cond;
            cmd.Parameters.Add(new SqlParameter("@nodeid", SqlDbType.NVarChar)).Value = nodeid.ToString();
            cmd.Parameters.Add(new SqlParameter("@db", SqlDbType.NVarChar)).Value = db.ToString();
            cmd.Parameters.Add(new SqlParameter("@userid", SqlDbType.NVarChar)).Value = empid.ToString();


            cmd.ExecuteNonQuery();

            sqlConn.Close();

            return "OK";

        }
       
        public string spAbsentListEmplloyee(string ConnectionStr, DateTime fDate, DateTime lDate, string deptid, string empid, int cond, string nodeid, string db)
        {

            SqlCommand cmd = new SqlCommand();
            SqlConnection sqlConn = null;
            sqlConn = new SqlConnection(ConnectionStr);
            sqlConn.Open();
            cmd.Connection = sqlConn;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spAbsentListEmployee";
            cmd.Parameters.Add(new SqlParameter("@date1", SqlDbType.DateTime)).Value = fDate;
            cmd.Parameters.Add(new SqlParameter("@date2", SqlDbType.DateTime)).Value = lDate;
            cmd.Parameters.Add(new SqlParameter("@deptid", SqlDbType.NVarChar)).Value = deptid;
            cmd.Parameters.Add(new SqlParameter("@cond", SqlDbType.Int)).Value = cond;
            cmd.Parameters.Add(new SqlParameter("@nodeid", SqlDbType.NVarChar)).Value = nodeid.ToString();
            cmd.Parameters.Add(new SqlParameter("@db", SqlDbType.NVarChar)).Value = db.ToString();
            cmd.Parameters.Add(new SqlParameter("@userid", SqlDbType.NVarChar)).Value = empid.ToString();


            cmd.ExecuteNonQuery();

            sqlConn.Close();

            return "OK";

        }
        public string spAbsentListEmplloyeeManual(string ConnectionStr, DateTime fDate, DateTime lDate, string deptid, string empid, int cond, string nodeid, string db)
        {

            SqlCommand cmd = new SqlCommand();
            SqlConnection sqlConn = null;
            sqlConn = new SqlConnection(ConnectionStr);
            sqlConn.Open();
            cmd.Connection = sqlConn;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spAbsentListEmployeeManual";
            cmd.Parameters.Add(new SqlParameter("@date1", SqlDbType.DateTime)).Value = fDate;
            cmd.Parameters.Add(new SqlParameter("@date2", SqlDbType.DateTime)).Value = lDate;
            cmd.Parameters.Add(new SqlParameter("@deptid", SqlDbType.NVarChar)).Value = deptid;
            cmd.Parameters.Add(new SqlParameter("@cond", SqlDbType.Int)).Value = cond;
            cmd.Parameters.Add(new SqlParameter("@nodeid", SqlDbType.NVarChar)).Value = nodeid.ToString();
            cmd.Parameters.Add(new SqlParameter("@db", SqlDbType.NVarChar)).Value = db.ToString();
            cmd.Parameters.Add(new SqlParameter("@userid", SqlDbType.NVarChar)).Value = empid.ToString();
            
            cmd.ExecuteNonQuery();

            sqlConn.Close();

            return "OK";

        }
        public string spPresentListEmplloyee(string ConnectionStr, DateTime fDate, DateTime lDate, string deptid, string empid, int cond, string nodeid, string db)
        {

            SqlCommand cmd = new SqlCommand();
            SqlConnection sqlConn = null;
            sqlConn = new SqlConnection(ConnectionStr);
            sqlConn.Open();
            cmd.Connection = sqlConn;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spPresentListEmployee";
            cmd.Parameters.Add(new SqlParameter("@date1", SqlDbType.DateTime)).Value = fDate;
            cmd.Parameters.Add(new SqlParameter("@date2", SqlDbType.DateTime)).Value = lDate;
            cmd.Parameters.Add(new SqlParameter("@deptid", SqlDbType.NVarChar)).Value = deptid;
            cmd.Parameters.Add(new SqlParameter("@cond", SqlDbType.Int)).Value = cond;
            cmd.Parameters.Add(new SqlParameter("@nodeid", SqlDbType.NVarChar)).Value = nodeid.ToString();
            cmd.Parameters.Add(new SqlParameter("@db", SqlDbType.NVarChar)).Value = db.ToString();
            cmd.Parameters.Add(new SqlParameter("@userid", SqlDbType.NVarChar)).Value = empid.ToString();


            cmd.ExecuteNonQuery();

            sqlConn.Close();

            return "OK";

        }
       
        public string spPresentListEmplloyeeManual(string ConnectionStr, DateTime fDate, DateTime lDate, string deptid, string empid, int cond, string nodeid, string db)
        {

            SqlCommand cmd = new SqlCommand();
            SqlConnection sqlConn = null;
            sqlConn = new SqlConnection(ConnectionStr);
            sqlConn.Open();
            cmd.Connection = sqlConn;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spPresentListEmployeeManual";
            cmd.Parameters.Add(new SqlParameter("@date1", SqlDbType.DateTime)).Value = fDate;
            cmd.Parameters.Add(new SqlParameter("@date2", SqlDbType.DateTime)).Value = lDate;
            cmd.Parameters.Add(new SqlParameter("@deptid", SqlDbType.NVarChar)).Value = deptid;
            cmd.Parameters.Add(new SqlParameter("@cond", SqlDbType.Int)).Value = cond;
            cmd.Parameters.Add(new SqlParameter("@nodeid", SqlDbType.NVarChar)).Value = nodeid.ToString();
            cmd.Parameters.Add(new SqlParameter("@db", SqlDbType.NVarChar)).Value = db.ToString();
            cmd.Parameters.Add(new SqlParameter("@userid", SqlDbType.NVarChar)).Value = empid.ToString();


            cmd.ExecuteNonQuery();

            sqlConn.Close();

            return "OK";

        }
       
        public string spCxecuteAbsentListAttendanceIndividual(string ConnectionStr, DateTime fDate, DateTime lDate, string deptid, string empid, int cond, string nodeid, string db,string userid)
        {

            SqlCommand cmd = new SqlCommand();
            SqlConnection sqlConn = null;
            sqlConn = new SqlConnection(ConnectionStr);
            sqlConn.Open();
            cmd.Connection = sqlConn;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spAbsentListAttendanceIndividual";
            cmd.Parameters.Add(new SqlParameter("@date1", SqlDbType.DateTime)).Value = fDate;
            cmd.Parameters.Add(new SqlParameter("@date2", SqlDbType.DateTime)).Value = lDate;
            //cmd.Parameters.Add(new SqlParameter("@deptid", SqlDbType.NVarChar)).Value = deptid;
            cmd.Parameters.Add(new SqlParameter("@empid", SqlDbType.NVarChar)).Value = empid;
            cmd.Parameters.Add(new SqlParameter("@cond", SqlDbType.Int)).Value = cond;
            cmd.Parameters.Add(new SqlParameter("@nodeid", SqlDbType.NVarChar)).Value = nodeid.ToString();
            cmd.Parameters.Add(new SqlParameter("@db", SqlDbType.NVarChar)).Value = db.ToString();
            cmd.Parameters.Add(new SqlParameter("@userid", SqlDbType.NVarChar)).Value = userid.ToString();


            cmd.ExecuteNonQuery();

            sqlConn.Close();

            return "OK";

        }

        public string spCxecuteAbsentListAttendanceALL(string ConnectionStr, DateTime fDate, DateTime lDate, string deptid, string empid, int cond, string nodeid, string db, string userid)
        {

            SqlCommand cmd = new SqlCommand();
            SqlConnection sqlConn = null;
            sqlConn = new SqlConnection(ConnectionStr);
            sqlConn.Open();
            cmd.Connection = sqlConn;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spAbsentListAttendanceALL";
            cmd.Parameters.Add(new SqlParameter("@date1", SqlDbType.DateTime)).Value = fDate;
            cmd.Parameters.Add(new SqlParameter("@date2", SqlDbType.DateTime)).Value = lDate;
            //cmd.Parameters.Add(new SqlParameter("@deptid", SqlDbType.NVarChar)).Value = deptid;
            cmd.Parameters.Add(new SqlParameter("@empid", SqlDbType.NVarChar)).Value = empid;
            cmd.Parameters.Add(new SqlParameter("@cond", SqlDbType.Int)).Value = cond;
            cmd.Parameters.Add(new SqlParameter("@nodeid", SqlDbType.NVarChar)).Value = nodeid.ToString();
            cmd.Parameters.Add(new SqlParameter("@db", SqlDbType.NVarChar)).Value = db.ToString();
            cmd.Parameters.Add(new SqlParameter("@userid", SqlDbType.NVarChar)).Value = userid.ToString();


            cmd.ExecuteNonQuery();

            sqlConn.Close();

            return "OK";

        }

        public string SaveShiftAllocationDataByEmpid(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {
            string retValue = "";
            try
            {
                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {

                    myCommand.CommandText = "exec [spShiftAllocationInsertByEmpId] '" + ofproc.DateForSA + "','" + ofproc.EmpID + "','" + ofproc.ShiftID + "','" + ofproc.InTime + "','" + ofproc.OutTime + "'";
                    myCommand.CommandTimeout = 0;
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


        public string SaveShiftAllocationData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {
            string retValue = "";
            try
            {
                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {

                    myCommand.CommandText = "exec [spShiftAllocationInsert] '" + ofproc.DateForSA + "','" + ofproc.EmpID + "','" + ofproc.ShiftID + "','" + ofproc.InTime + "','" + ofproc.OutTime + "'";
                    myCommand.CommandTimeout = 0;
                    //CommandTimeout = 0;
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

        


        #endregion


        #region Application Condition Setup

        public string ApplicationConditionSetup(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {
            string retValue = "";
            try
            {
                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spProcessTaskDateAllemoloyeeInsert] '" + ofproc.TaskType + "','" + ofproc.StartDate + "','" + ofproc.EndDate + "','" + ofproc.Isstadate + "','" + ofproc.IsendDate + "','" + ofproc.IsShowPreviousTask + "','" + ofproc.IsShowPreviousAtnd + "','" + ofproc.IsApplyPrevious + "'," + ofproc.TrnMonth + "," + ofproc.TrnYear + ",'" + ofproc.Status + "'";
                    myCommand.CommandTimeout = 0;
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
        #endregion


        public string ApplyAndApproveAttendanceProcessData(List<LeaveProcessHeader> lvphdrlst, SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {

                foreach (LeaveProcessHeader ofproc in lvphdrlst)
                {
                    myCommand.CommandText = "exec [spApplyAndApproveAttendanceProcess] '" + ofproc.ApplicantId + "','" + ofproc.ProcessId + "','" + ofproc.FlowId + "'," + ofproc.ProcesslevelId + ",'" + ofproc.ApplicantId + "','" + ofproc.Remarks + "','" + ofproc.EntryUserid + "'," + ofproc.ActiontypeId + ",'" + ofproc.ClaiminDdate + "','" + ofproc.SysIntime + "','" + ofproc.SysOuttime + "','" + ofproc.SysTotalhrs + "','" + ofproc.ActIntime + "','" + ofproc.ActOuttime + "'," + ofproc.SL + ",'" + ofproc.TransactionNo + "','" + ofproc.ResponsiblepersonId + "','" + ofproc.MovementNo + "','" + ofproc.ActOutdate + "'";
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

    }

