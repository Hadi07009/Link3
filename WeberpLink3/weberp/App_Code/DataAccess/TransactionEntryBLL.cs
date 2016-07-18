using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using LibraryDAL;
using Link3FrameWork; 

public class TransactionEntryBLL
    {
        public TransactionEntryBLL() { }
        
        public string GetReferenceNo(SqlCommand myCommand, string JrnType, string VoucherType, string ModuleName, string accPeriod, DateTime TransactionDate)
        {
            string ReferenceNo = "", PreFix = "", NextNum = "", Yr = "", month = "", day = "", refNo = "", refType = "", startNum = "";
            SqlDataAdapter sqlDataAdapterObj = null, sqlData, sqlData1, sqlData2;
            DataTable dataTableObj, dt, dt1, dt2;
            int ResetMonth = 0;
            try
            {
                myCommand.CommandText = "select * from AccJournalSetup where jrntype='" + JrnType + "' and VoucherType='" + VoucherType + "' and JrnModule='" + ModuleName + "'";
                sqlDataAdapterObj = new SqlDataAdapter();
                sqlDataAdapterObj.SelectCommand = myCommand;
                dataTableObj = new DataTable();
                sqlDataAdapterObj.Fill(dataTableObj);
                if (dataTableObj.Rows.Count > 0)
                {
                    PreFix = dataTableObj.Rows[0]["JrnPrefix"].ToString();
                    NextNum = dataTableObj.Rows[0]["JrnNextRefNo"].ToString();
                    startNum = dataTableObj.Rows[0]["JrnStartRefNo"].ToString();
                    refType = dataTableObj.Rows[0]["JrnRefNoType"].ToString();
                    ResetMonth = Convert.ToInt32(dataTableObj.Rows[0]["JrnRefNoResetPeriod"].ToString());

                    if (dataTableObj.Rows[0]["JrnAddDay"].ToString() == "Y")
                        day = StringProcess.addZeroInString(TransactionDate.Day.ToString(), 2, true);
                    if (dataTableObj.Rows[0]["JrnAddMonth"].ToString() == "Y")
                        month = StringProcess.addZeroInString(TransactionDate.Month.ToString(), 2, true);
                    if (dataTableObj.Rows[0]["JrnAddYear"].ToString() == "Y")
                        Yr = StringProcess.Right(TransactionDate.Year.ToString(), 2);

                    if (refType == "P")
                    {
                        myCommand.CommandText = "select MAX([Trn_DATE]) as maxref  from AccTransactionHeader where Trn_Jrn_Type='" + JrnType + "' and MONTH(Trn_DATE)=" + month + " and YEAR(Trn_DATE)=" + TransactionDate.Year;
                        sqlData = new SqlDataAdapter();
                        sqlDataAdapterObj.SelectCommand = myCommand;
                        dt = new DataTable();
                        sqlDataAdapterObj.Fill(dt);
                        if (Convert.ToString(dt.Rows[0]["maxref"]) == "")
                        {
                            ReferenceNo = PreFix + Yr + month + day + "-" + startNum;
                        }
                        else
                        {
                            DateTime dd = Convert.ToDateTime(dt.Rows[0]["maxref"]);
                            myCommand.CommandText = "select MAX([Trn_Ref_No]) as maxref  from AccTransactionHeader where Trn_Jrn_Type='" + JrnType + "' and Trn_DATE<=Convert(datetime,'" + TransactionDate + "',103) and MONTH(Trn_DATE)=" + month + " and YEAR(Trn_DATE)=" + TransactionDate.Year;
                            sqlData1 = new SqlDataAdapter();
                            sqlDataAdapterObj.SelectCommand = myCommand;
                            dt1 = new DataTable();
                            sqlDataAdapterObj.Fill(dt1);

                            if (Convert.ToString(dt1.Rows[0]["maxref"]) == "")
                            {
                                System.Windows.Forms.MessageBox.Show("Entry is not Allowed before " + dd.Date + " can 't generate Reference No. ", StringProcess.messageHead);
                                ReferenceNo = "";
                            }
                            else
                            {
                                myCommand.CommandText = "select MAX([Trn_Ref_No]) as maxref  from AccTransactionHeader where Trn_Jrn_Type='" + JrnType + "' and Trn_DATE > Convert(datetime,'" + TransactionDate + "',103) and MONTH(Trn_DATE)=" + month + " and YEAR(Trn_DATE)=" + TransactionDate.Year;
                                sqlData2 = new SqlDataAdapter();
                                sqlDataAdapterObj.SelectCommand = myCommand;
                                dt2 = new DataTable();
                                sqlDataAdapterObj.Fill(dt2);


                                if (Convert.ToString(dt2.Rows[0]["maxref"]) == "")
                                {
                                    refNo = Convert.ToString(dt1.Rows[0]["maxref"]);
                                    if (refNo.IndexOf('\\', 0) != -1)
                                    {
                                        string[] rfn = refNo.Split('\\');
                                        ReferenceNo = PreFix + Yr + month + day + "-" + NextNumber(refNo, true);
                                    }
                                    else
                                    {
                                        ReferenceNo = PreFix + Yr + month + day + "-" + NextNumber(refNo, false);
                                    }
                                }
                                else
                                {
                                    refNo = Convert.ToString(dt1.Rows[0]["maxref"]);
                                    ReferenceNo = PreFix + Yr + month + day + "-" + NextNumber(refNo, true);
                                }

                            }

                        }
                    }
                    else
                    {

                        if (TransactionDate.Date.Month == ResetMonth) //reset referance number from this month 
                        {
                            myCommand.CommandText = "select MAX([Trn_Ref_No]) as maxref  from AccTransactionHeader where Trn_Jrn_Type='" + JrnType + "' and MONTH(Trn_DATE)=" + month + " and YEAR(Trn_DATE)=" + TransactionDate.Year;
                            sqlData1 = new SqlDataAdapter();
                            sqlDataAdapterObj.SelectCommand = myCommand;
                            dt1 = new DataTable();
                            sqlDataAdapterObj.Fill(dt1);

                            if (Convert.ToString(dt1.Rows[0]["maxref"]) != "")
                            {
                                ReferenceNo = PreFix + Yr + month + day + "-" + NextNumber(dt1.Rows[0]["maxref"].ToString(), false);
                            }
                            else
                            {
                                ReferenceNo = PreFix + Yr + month + day + "-" + startNum;
                            }
                        }
                        else if (TransactionDate.Date.Month != ResetMonth)
                        {
                            myCommand.CommandText = "select MAX([Trn_Ref_No]) as maxref  from AccTransactionHeader where Trn_Jrn_Type='" + JrnType + "'";
                            sqlData1 = new SqlDataAdapter();
                            sqlDataAdapterObj.SelectCommand = myCommand;
                            dt1 = new DataTable();
                            sqlDataAdapterObj.Fill(dt1);

                            if (Convert.ToString(dt1.Rows[0]["maxref"]) != "")
                            {
                                ReferenceNo = PreFix + Yr + month + day + "-" + NextNumber(dt1.Rows[0]["maxref"].ToString(), false);
                            }
                            else
                            {
                                ReferenceNo = PreFix + Yr + month + day + "-" + startNum;
                            }
                        }
                        else
                        {
                            ReferenceNo = PreFix + Yr + month + day + "-" + startNum;
                        }


                    }


                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, StringProcess.messageHead);
            }
            return ReferenceNo;
        }
       
        private string NextNumber(string refNo, bool ex)  
        {
            string[] rf;// refNo.Split('-');
            string newRef = "";
            if (!ex)
            {
                rf = refNo.Split('-');
                if (rf.Length > 1)
                {
                    newRef = (Convert.ToInt32(rf[rf.Length - 1]) + 1).ToString();
                    newRef = StringProcess.addZeroInString(newRef, rf[rf.Length - 1].Length, true);
                }
            }
            else
            {
                rf = refNo.Split('-');
                string[] r = rf[rf.Length - 1].Split('\\');

                if (r.Length > 1)
                {
                    string sbStr = r[r.Length - 1];
                    if (sbStr.ToUpper() == "ZZ")
                    {
                        newRef = r[0] + "\\AA";
                    }
                    else if (StringProcess.Right(sbStr, 1).ToUpper() == "Z")
                    {
                        newRef = r[0] + "\\" + Convert.ToChar((Convert.ToChar(StringProcess.Left(sbStr, 1).ToUpper()) + 1)) + 'A';
                    }
                    else
                    {
                        newRef = r[0] + "\\" + StringProcess.Left(sbStr, 1).ToUpper() + Convert.ToChar((Convert.ToChar(StringProcess.Right(sbStr, 1).ToUpper()) + 1));
                    }

                }
                else
                {
                    newRef = r[0] + "\\AA";
                }
            }
            return newRef;
        }

        public string[] PostData(string ConnectionStr, TransactionHeaderDAO thDao, List<TransactionDetailsDAO> tdDaolst)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionStr);
            myConnection.Open();

            SqlCommand myCommand = myConnection.CreateCommand();
            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string[] retValue = new string[2] { "", "" }; string OldRef = "", userid = "";
            myTrans = myConnection.BeginTransaction("PostAccData");

            myCommand.Connection = myConnection;
            myCommand.Transaction = myTrans;

            try
            {
                myCommand.CommandText = "select (ISNULL(MAX(trn_jrn_code),0)+1) as maxJrnCode from AccTransactionHeader";
                sqlDataAdapterObj = new SqlDataAdapter();
                sqlDataAdapterObj.SelectCommand = myCommand;
                DataTable dtmaxJrnCode = new DataTable();
                sqlDataAdapterObj.Fill(dtmaxJrnCode);
                if (dtmaxJrnCode.Rows.Count > 0)
                    thDao.TrnJrnCode = Convert.ToDouble(dtmaxJrnCode.Rows[0]["maxJrnCode"].ToString());
                else
                {
                    myTrans.Rollback("PostAccData"); return retValue;
                }

                OldRef = thDao.TrnRefNo; userid = thDao.TrnEntryUser;
                thDao.TrnRefNo = GetReferenceNo(myCommand, thDao.TrnJrnType, thDao.VoucherType, thDao.ModuleName, thDao.TrnAccPeriod, thDao.TrnDATE);
                if (thDao.TrnRefNo == "")
                {
                    myTrans.Rollback("PostAccData"); return retValue;
                }

                myCommand.CommandText = "select * from AccTransactionHeader where Trn_Ref_No='" + thDao.TrnRefNo + "'";
                sqlDataAdapterObj = new SqlDataAdapter();
                sqlDataAdapterObj.SelectCommand = myCommand;
                DataTable dtmaxRefCheck = new DataTable();
                sqlDataAdapterObj.Fill(dtmaxRefCheck);

                if (dtmaxRefCheck.Rows.Count > 0)
                {
                    myTrans.Rollback("PostAccData");
                    System.Windows.Forms.MessageBox.Show("Refference No already Exits\rPlease Try Again Later", StringProcess.messageHead);
                    return retValue;
                }

                myCommand.CommandText = @"INSERT INTO [AccTransactionHeader]([Trn_Ref_No],[Trn_Jrn_Code],[Trn_Jrn_Type],[Trn_Voucher_Type],[Trn_Acc_Period],
                                        [Trn_Entry_DATE],[Trn_DATE],[Trn_Curr_Code],[Trn_Curr_Rate],[Trn_Entry_User],[Trn_LastUpdate_User],
                                        [Trn_LastUpdate_DATE],[Trn_Entry_Flag],[Trn_Revise_Flag],[Trn_Reverse_Flag]) VALUES('" +
                                        thDao.TrnRefNo + "'," + thDao.TrnJrnCode + ",'" + thDao.TrnJrnType + "','" + thDao.VoucherType + "','" + thDao.TrnAccPeriod + "',convert(datetime,'" +
                                        thDao.TrnEntryDATE + "',103),convert(datetime,'" + thDao.TrnDATE + "',103),'" + thDao.TrnCurrCode + "','" + thDao.TrnCurrRate + "','" + thDao.TrnEntryUser + "','" + thDao.TrnEntryUser + "',convert(datetime,'" +
                                        thDao.TrnEntryDATE + "',103),'" + thDao.TrnEntryFlag + "','" + thDao.TrnReviseFlag + "','" + thDao.TrnReverseFlag + "')";
                myCommand.ExecuteNonQuery();

                foreach (TransactionDetailsDAO tdDao in tdDaolst)
                {
                    myCommand.CommandText = "select * from AccTransactionSplit_" + thDao.TrnEntryUser + " where Trn_Ac_Code='" + tdDao.TrnAcCode + "' and Trn_Line_No=" + tdDao.TrnLineNo;
                    sqlDataAdapterObj = new SqlDataAdapter();
                    sqlDataAdapterObj.SelectCommand = myCommand;
                    DataTable dtAnalysis = new DataTable();
                    sqlDataAdapterObj.Fill(dtAnalysis);

                    string ChequeDate = null;
                    if (tdDao.TrnChequeNo != "") ChequeDate = tdDao.TrnChequeDate.Date.ToString();

                    if (!DataProcess.ExecuteQuery(myCommand, "UPDATE [AccCOA] SET [Gl_Coa_Trn_Flg] ='Y'  WHERE Gl_Coa_Code='" + tdDao.TrnAcCode + "'"))
                    {
                        myTrans.Rollback("PostAccData"); return retValue;
                    }

                    if (dtAnalysis.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtAnalysis.Rows)
                        {
                            myCommand.CommandText = @"INSERT INTO [AccTransactionDetails]([Trn_Ref_No],[Trn_Ac_Code],[Trn_Jrn_Code],[Trn_Line_No],[Trn_Narration],
                                                    [Trn_Trn_type],[Trn_Amount],[Trn_Match],[Trn_Cheque_No],[Trn_Allocate_Flag],[Trn_Reval_Flag],
                                                    [Trn_Payment_DATE],[Trn_Ac_Desc],[Trn_Ac_Type],[Trn_Bus_Flag],[Trn_Due_DATE],[Trn_Sec_No],
                                                    [Trn_Adr_Code],[Trn_Dc_No],[Trn_GRN_No],[Trn_Bank_Reco_Flag],[Trn_Sub_No],[Trn_Tot_Int],
                                                    [Trn_Asset_Y_N],Trn_Instrument_Type,Trn_Cheque_Date,[T_Fl],[T_In],T_C1,T_C2,Trn_Code) VALUES('" +
                                                    thDao.TrnRefNo + "','" + tdDao.TrnAcCode + "','" + thDao.TrnJrnCode + "','" + tdDao.TrnLineNo + "','" + DataValidator.InvalidCharecterHandler(tdDao.TrnNarration) + "','" +
                                                    tdDao.TrnTrntype + "'," + dr["Trn_Amount"].ToString() + ",'" + tdDao.TrnMatch + "','" + tdDao.TrnChequeNo + "','" + tdDao.TrnAllocateFlag + "','" + tdDao.TrnRevalFlag + "',convert(datetime,'" +
                                                    tdDao.TrnPaymentDATE + "',103),'" + tdDao.TrnAcDesc + "','" + tdDao.TrnAcType + "','" + tdDao.TrnBusFlag + "',convert(datetime,'" + tdDao.TrnDueDATE + "',103),'" + tdDao.TrnSecNo + "','" +
                                                    tdDao.TrnAdrCode + "','" + tdDao.TrnDcNo + "','" + tdDao.TrnGRNNo + "','" + tdDao.TrnBankRecoFlag + "','" + dr["Trn_Sub_No"].ToString() + "','" + tdDao.TrnTotInt + "','" +
                                                    tdDao.TrnAssetYN + "','" + tdDao.InstrumentType + "',Convert(datetime,'" + ChequeDate + "',103),'" + thDao.VoucherType + "','','','','')";
                            myCommand.ExecuteNonQuery();

                            myCommand.CommandText = @"INSERT INTO [AccTransactionAnalysis]([Trn_Ref_No],[Trn_Ac_Code],[Trn_Line_No],[Trn_Sub_No],
                                                    [Trn_AnaGroupDefinationCode1],[Trn_AnaGroupDefinationCode2],[Trn_AnaGroupDefinationCode3],
                                                    [Trn_AnaGroupDefinationCode4],[Trn_AnaGroupDefinationCode5],[Trn_AnaGroupLabelCode1],
                                                    [Trn_AnaGroupLabelCode2],[Trn_AnaGroupLabelCode3],[Trn_AnaGroupLabelCode4],
                                                    [Trn_AnaGroupLabelCode5]) VALUES('" +
                                                    thDao.TrnRefNo + "','" + tdDao.TrnAcCode + "'," + tdDao.TrnLineNo + "," + dr["Trn_Sub_No"].ToString() + ",'" +
                                                    dr["Trn_AnaGroupDefinationCode1"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode2"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode3"].ToString() + "','" +
                                                    dr["Trn_AnaGroupDefinationCode4"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode5"].ToString() + "','" + dr["Trn_AnaGroupLabelCode1"].ToString() + "','" +
                                                    dr["Trn_AnaGroupLabelCode2"].ToString() + "','" + dr["Trn_AnaGroupLabelCode3"].ToString() + "','" + dr["Trn_AnaGroupLabelCode4"].ToString() + "','" +
                                                    dr["Trn_AnaGroupLabelCode5"].ToString() + "')";
                            myCommand.ExecuteNonQuery();

                            if (!DataProcess.ExecuteQuery(myCommand, "update AccCoaGroupSetup set Cost_Trn_Present='Y',Cost_Del_Check_Flag='N' where Cost_Id in ('" + dr["Trn_AnaGroupLabelCode1"].ToString() + "','" + dr["Trn_AnaGroupLabelCode2"].ToString() + "','" + dr["Trn_AnaGroupLabelCode3"].ToString() + "','" + dr["Trn_AnaGroupLabelCode4"].ToString() + "','" + dr["Trn_AnaGroupLabelCode5"].ToString() + "')"))
                            {
                                myTrans.Rollback("PostAccData"); return retValue;
                            }

                            if (!DataProcess.ExecuteQuery(myCommand, "update AccCoaGroupCodeSetup set Ccg_Trn_Present='Y' where Ccg_Code in ('" + dr["Trn_AnaGroupDefinationCode1"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode2"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode3"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode4"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode5"].ToString() + "')"))
                            {
                                myTrans.Rollback("PostAccData"); return retValue;
                            }

                        }
                    }
                    else
                    {
                        myCommand.CommandText = @"INSERT INTO [AccTransactionDetails]([Trn_Ref_No],[Trn_Ac_Code],[Trn_Jrn_Code],[Trn_Line_No],[Trn_Narration],
                                                    [Trn_Trn_type],[Trn_Amount],[Trn_Match],[Trn_Cheque_No],[Trn_Allocate_Flag],[Trn_Reval_Flag],
                                                    [Trn_Payment_DATE],[Trn_Ac_Desc],[Trn_Ac_Type],[Trn_Bus_Flag],[Trn_Due_DATE],[Trn_Sec_No],
                                                    [Trn_Adr_Code],[Trn_Dc_No],[Trn_GRN_No],[Trn_Bank_Reco_Flag],[Trn_Sub_No],[Trn_Tot_Int],
                                                    [Trn_Asset_Y_N],Trn_Instrument_Type,Trn_Cheque_Date,[T_Fl],[T_In],T_C1,T_C2,Trn_Code) VALUES('" +
                                                    thDao.TrnRefNo + "','" + tdDao.TrnAcCode + "','" + thDao.TrnJrnCode + "','" + tdDao.TrnLineNo + "','" + DataValidator.InvalidCharecterHandler(tdDao.TrnNarration) + "','" +
                                                    tdDao.TrnTrntype + "'," + tdDao.TrnAmount + ",'" + tdDao.TrnMatch + "','" + tdDao.TrnChequeNo + "','" + tdDao.TrnAllocateFlag + "','" + tdDao.TrnRevalFlag + "',convert(datetime,'" +
                                                    tdDao.TrnPaymentDATE + "',103),'" + tdDao.TrnAcDesc + "','" + tdDao.TrnAcType + "','" + tdDao.TrnBusFlag + "',convert(datetime,'" + tdDao.TrnDueDATE + "',103),'" + tdDao.TrnSecNo + "','" +
                                                    tdDao.TrnAdrCode + "','" + tdDao.TrnDcNo + "','" + tdDao.TrnGRNNo + "','" + tdDao.TrnBankRecoFlag + "','" + tdDao.TrnSubNo + "','" + tdDao.TrnTotInt + "','" +
                                                    tdDao.TrnAssetYN + "','" + tdDao.InstrumentType + "',Convert(datetime,'" + ChequeDate + "',103),'" + thDao.VoucherType + "','','','','')";
                        myCommand.ExecuteNonQuery();

                    }

                    //if (tdDao.TrnChequeNo != "" && thDao.VoucherType == StringEnum.GetStringValue(VoucherType.ChequeVoucher) && tdDao.InstrumentType == Enum.GetName(typeof(InstrumentType), InstrumentType.Cheque))
                    //{
                    //    if (!DataProcess.ExecuteQuery(myCommand, "UPDATE [AccBankCheuqeDetails] SET  [BNK_CHQ_BOOK_DATE] = Convert(datetime,'" + ChequeDate + "',103),[BNK_CHQ_Post_flag] = 'Y' WHERE BNK_CHQ_NO='" + tdDao.TrnChequeNo + "'"))
                    //    {
                    //        myTrans.Rollback("PostAccData"); return retValue;
                    //    }
                    //}

//                    if (tdDao.TrnChequeNo != "" && thDao.VoucherType == StringEnum.GetStringValue(VoucherType.MoneyReceipt) && tdDao.TrnTrntype == "C")
//                    {
//                        myCommand.CommandText = @" delete from AccTransactionReceivableBankInfo where Trn_Ref_No='" + thDao.TrnRefNo + "' and and [Trn_Cheque_No]=" + tdDao.TrnChequeNo;

//                        myCommand.ExecuteNonQuery();

//                        myCommand.CommandText = @"INSERT INTO [AccTransactionReceivableBankInfo] ([Trn_Ref_No],[Trn_Jrn_Code],[Trn_Line_No],
//                                                [Trn_Bank_Name],[Trn_Branch_Name],[Trn_Account_No],
//                                                [Trn_Cheque_No],[Trn_Cheque_DATE]) VALUES ('" +
//                                                thDao.TrnRefNo + "'," + thDao.TrnJrnCode + "," + tdDao.TrnLineNo + ",'" +
//                                                tdDao.RtrnBankName + "','" + tdDao.RtrnBranchName + "','" + tdDao.RtrnAccountNo + "','" +
//                                                tdDao.TrnChequeNo + "',convert(datetime,'" + ChequeDate + "',103))";
//                        myCommand.ExecuteNonQuery();
//                    }

                }

                if (!DeleteSaveData(myCommand, OldRef, userid))
                {
                    myTrans.Rollback("PostAccData"); return retValue;
                }

                myTrans.Commit();
                retValue[0] = thDao.TrnRefNo;
                retValue[1] = thDao.TrnJrnCode.ToString();
                return retValue;
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback("PostAccData");
                    System.Windows.Forms.MessageBox.Show(e.Message, StringProcess.messageHead);
                    return retValue;
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return retValue;
                    }
                    System.Windows.Forms.MessageBox.Show(ex.Message, StringProcess.messageHead);
                }

                return retValue;
            }
            finally
            {
                myConnection.Close();
            }

            return retValue;
        }

        public string[] PostHoldData(string ConnectionStr, TransactionHeaderDAO thDao, List<TransactionDetailsDAO> tdDaolst)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionStr);
            myConnection.Open();

            SqlCommand myCommand = myConnection.CreateCommand();
            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string[] retValue = new string[2] { "", "" }; string OldRef = "",userid="";
            myTrans = myConnection.BeginTransaction("PostAccHoldData");

            myCommand.Connection = myConnection;
            myCommand.Transaction = myTrans;

            try
            {
                myCommand.CommandText = "select (ISNULL(MAX(trn_jrn_code),0)+1) as maxJrnCode from AccTransactionHeader";
                sqlDataAdapterObj = new SqlDataAdapter();
                sqlDataAdapterObj.SelectCommand = myCommand;
                DataTable dtmaxJrnCode = new DataTable();
                sqlDataAdapterObj.Fill(dtmaxJrnCode);
                if (dtmaxJrnCode.Rows.Count > 0)
                    thDao.TrnJrnCode = Convert.ToDouble(dtmaxJrnCode.Rows[0]["maxJrnCode"].ToString());
                else
                {
                    myTrans.Rollback("PostAccHoldData"); return retValue;
                }
                OldRef = thDao.TrnRefNo; userid = thDao.TrnEntryUser;
                thDao.TrnRefNo = GetReferenceNo(myCommand, thDao.TrnJrnType, thDao.VoucherType, thDao.ModuleName, thDao.TrnAccPeriod, thDao.TrnDATE);
                if (thDao.TrnRefNo == "")
                {
                    myTrans.Rollback("PostAccHoldData"); return retValue;
                }

                myCommand.CommandText = "select * from AccTransactionHeader where Trn_Ref_No='" + thDao.TrnRefNo + "'";
                sqlDataAdapterObj = new SqlDataAdapter();
                sqlDataAdapterObj.SelectCommand = myCommand;
                DataTable dtmaxRefCheck = new DataTable();
                sqlDataAdapterObj.Fill(dtmaxRefCheck);

                if (dtmaxRefCheck.Rows.Count > 0)
                {
                    myTrans.Rollback("PostAccHoldData");
                    System.Windows.Forms.MessageBox.Show("Refference No. already Exits\rPlease Try Again Later", StringProcess.messageHead);
                    return retValue;
                }

                myCommand.CommandText = @"INSERT INTO [AccTransactionHeader]([Trn_Ref_No],[Trn_Jrn_Code],[Trn_Jrn_Type],[Trn_Voucher_Type],[Trn_Acc_Period],
                                        [Trn_Entry_DATE],[Trn_DATE],[Trn_Curr_Code],[Trn_Curr_Rate],[Trn_Entry_User],[Trn_LastUpdate_User],
                                        [Trn_LastUpdate_DATE],[Trn_Entry_Flag],[Trn_Revise_Flag],[Trn_Reverse_Flag]) VALUES('" +
                                        thDao.TrnRefNo + "'," + thDao.TrnJrnCode + ",'" + thDao.TrnJrnType + "','" + thDao.VoucherType + "','" + thDao.TrnAccPeriod + "',convert(datetime,'" +
                                        thDao.TrnEntryDATE + "',103),convert(datetime,'" + thDao.TrnDATE + "',103),'" + thDao.TrnCurrCode + "','" + thDao.TrnCurrRate + "','" + thDao.TrnEntryUser + "','" + thDao.TrnEntryUser + "',convert(datetime,'" +
                                        thDao.TrnEntryDATE + "',103),'" + thDao.TrnEntryFlag + "','" + thDao.TrnReviseFlag + "','" + thDao.TrnReverseFlag + "')";
                myCommand.ExecuteNonQuery();

                foreach (TransactionDetailsDAO tdDao in tdDaolst)
                {
                    myCommand.CommandText = "select * from AccTransactionAnalysisHold where trn_ref_no='"+tdDao.TrnRefNo+"' and Trn_Ac_Code='" + tdDao.TrnAcCode + "' and Trn_Line_No=" + tdDao.TrnLineNo + " and trn_sub_no=" + tdDao.TrnSubNo;
                    sqlDataAdapterObj = new SqlDataAdapter();
                    sqlDataAdapterObj.SelectCommand = myCommand;
                    DataTable dtAnalysis = new DataTable();
                    sqlDataAdapterObj.Fill(dtAnalysis);

                    string ChequeDate = null;
                    if (tdDao.TrnChequeNo != "") ChequeDate = tdDao.TrnChequeDate.Date.ToString();

                    if (!DataProcess.ExecuteQuery(myCommand, "UPDATE [AccCOA] SET [Gl_Coa_Trn_Flg] ='Y'  WHERE Gl_Coa_Code='" + tdDao.TrnAcCode + "'"))
                    {
                        myTrans.Rollback("PostAccHoldData"); return retValue;
                    }

                    if (dtAnalysis.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtAnalysis.Rows)
                        {
                            myCommand.CommandText = @"INSERT INTO [AccTransactionDetails]([Trn_Ref_No],[Trn_Ac_Code],[Trn_Jrn_Code],[Trn_Line_No],[Trn_Narration],
                                                    [Trn_Trn_type],[Trn_Amount],[Trn_Match],[Trn_Cheque_No],[Trn_Allocate_Flag],[Trn_Reval_Flag],
                                                    [Trn_Payment_DATE],[Trn_Ac_Desc],[Trn_Ac_Type],[Trn_Bus_Flag],[Trn_Due_DATE],[Trn_Sec_No],
                                                    [Trn_Adr_Code],[Trn_Dc_No],[Trn_GRN_No],[Trn_Bank_Reco_Flag],[Trn_Sub_No],[Trn_Tot_Int],
                                                    [Trn_Asset_Y_N],Trn_Instrument_Type,Trn_Cheque_Date,[T_Fl],[T_In],T_C1,T_C2,Trn_Code) VALUES('" +
                                                    thDao.TrnRefNo + "','" + tdDao.TrnAcCode + "','" + thDao.TrnJrnCode + "','" + tdDao.TrnLineNo + "','" + DataValidator.InvalidCharecterHandler(tdDao.TrnNarration) + "','" +
                                                    tdDao.TrnTrntype + "'," + tdDao.TrnAmount + ",'" + tdDao.TrnMatch + "','" + tdDao.TrnChequeNo + "','" + tdDao.TrnAllocateFlag + "','" + tdDao.TrnRevalFlag + "',convert(datetime,'" +
                                                    tdDao.TrnPaymentDATE + "',103),'" + tdDao.TrnAcDesc + "','" + tdDao.TrnAcType + "','" + tdDao.TrnBusFlag + "',convert(datetime,'" + tdDao.TrnDueDATE + "',103),'" + tdDao.TrnSecNo + "','" +
                                                    tdDao.TrnAdrCode + "','" + tdDao.TrnDcNo + "','" + tdDao.TrnGRNNo + "','" + tdDao.TrnBankRecoFlag + "','" + dr["Trn_Sub_No"].ToString() + "','" + tdDao.TrnTotInt + "','" +
                                                    tdDao.TrnAssetYN + "','" + tdDao.InstrumentType + "',Convert(datetime,'" + ChequeDate + "',103),'" + thDao.VoucherType + "','','','','')";
                            myCommand.ExecuteNonQuery();

                            myCommand.CommandText = @"INSERT INTO [AccTransactionAnalysis]([Trn_Ref_No],[Trn_Ac_Code],[Trn_Line_No],[Trn_Sub_No],
                                                    [Trn_AnaGroupDefinationCode1],[Trn_AnaGroupDefinationCode2],[Trn_AnaGroupDefinationCode3],
                                                    [Trn_AnaGroupDefinationCode4],[Trn_AnaGroupDefinationCode5],[Trn_AnaGroupLabelCode1],
                                                    [Trn_AnaGroupLabelCode2],[Trn_AnaGroupLabelCode3],[Trn_AnaGroupLabelCode4],
                                                    [Trn_AnaGroupLabelCode5]) VALUES('" +
                                                    thDao.TrnRefNo + "','" + tdDao.TrnAcCode + "'," + tdDao.TrnLineNo + "," + dr["Trn_Sub_No"].ToString() + ",'" +
                                                    dr["Trn_AnaGroupDefinationCode1"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode2"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode3"].ToString() + "','" +
                                                    dr["Trn_AnaGroupDefinationCode4"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode5"].ToString() + "','" + dr["Trn_AnaGroupLabelCode1"].ToString() + "','" +
                                                    dr["Trn_AnaGroupLabelCode2"].ToString() + "','" + dr["Trn_AnaGroupLabelCode3"].ToString() + "','" + dr["Trn_AnaGroupLabelCode4"].ToString() + "','" +
                                                    dr["Trn_AnaGroupLabelCode5"].ToString() + "')";
                            myCommand.ExecuteNonQuery();

                            if (!DataProcess.ExecuteQuery(myCommand, "update AccCoaGroupSetup set Cost_Trn_Present='Y',Cost_Del_Check_Flag='N' where Cost_Id in ('" + dr["Trn_AnaGroupLabelCode1"].ToString() + "','" + dr["Trn_AnaGroupLabelCode2"].ToString() + "','" + dr["Trn_AnaGroupLabelCode3"].ToString() + "','" + dr["Trn_AnaGroupLabelCode4"].ToString() + "','" + dr["Trn_AnaGroupLabelCode5"].ToString() + "')"))
                            {
                                myTrans.Rollback("PostAccHoldData"); return retValue;
                            }

                            if (!DataProcess.ExecuteQuery(myCommand, "update AccCoaGroupCodeSetup set Ccg_Trn_Present='Y' where Ccg_Code in ('" + dr["Trn_AnaGroupDefinationCode1"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode2"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode3"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode4"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode5"].ToString() + "')"))
                            {
                                myTrans.Rollback("PostAccHoldData"); return retValue;
                            }
                        }
                    }
                    else
                    {
                        myCommand.CommandText = @"INSERT INTO [AccTransactionDetails]([Trn_Ref_No],[Trn_Ac_Code],[Trn_Jrn_Code],[Trn_Line_No],[Trn_Narration],
                                                    [Trn_Trn_type],[Trn_Amount],[Trn_Match],[Trn_Cheque_No],[Trn_Allocate_Flag],[Trn_Reval_Flag],
                                                    [Trn_Payment_DATE],[Trn_Ac_Desc],[Trn_Ac_Type],[Trn_Bus_Flag],[Trn_Due_DATE],[Trn_Sec_No],
                                                    [Trn_Adr_Code],[Trn_Dc_No],[Trn_GRN_No],[Trn_Bank_Reco_Flag],[Trn_Sub_No],[Trn_Tot_Int],
                                                    [Trn_Asset_Y_N],Trn_Instrument_Type,Trn_Cheque_Date,[T_Fl],[T_In],T_C1,T_C2,Trn_Code) VALUES('" +
                                                    thDao.TrnRefNo + "','" + tdDao.TrnAcCode + "','" + thDao.TrnJrnCode + "','" + tdDao.TrnLineNo + "','" + DataValidator.InvalidCharecterHandler(tdDao.TrnNarration) + "','" +
                                                    tdDao.TrnTrntype + "'," + tdDao.TrnAmount + ",'" + tdDao.TrnMatch + "','" + tdDao.TrnChequeNo + "','" + tdDao.TrnAllocateFlag + "','" + tdDao.TrnRevalFlag + "',convert(datetime,'" +
                                                    tdDao.TrnPaymentDATE + "',103),'" + tdDao.TrnAcDesc + "','" + tdDao.TrnAcType + "','" + tdDao.TrnBusFlag + "',convert(datetime,'" + tdDao.TrnDueDATE + "',103),'" + tdDao.TrnSecNo + "','" +
                                                    tdDao.TrnAdrCode + "','" + tdDao.TrnDcNo + "','" + tdDao.TrnGRNNo + "','" + tdDao.TrnBankRecoFlag + "','" + tdDao.TrnSubNo + "','" + tdDao.TrnTotInt + "','" +
                                                    tdDao.TrnAssetYN + "','" + tdDao.InstrumentType + "',Convert(datetime,'" + ChequeDate + "',103),'" + thDao.VoucherType + "','','','','')";
                        myCommand.ExecuteNonQuery();

                    }

                    //if (tdDao.TrnChequeNo != "" && thDao.VoucherType == StringEnum.GetStringValue(VoucherType.ChequeVoucher) && tdDao.InstrumentType == Enum.GetName(typeof(InstrumentType), InstrumentType.Cheque))
                    //{
                    //    if (!DataProcess.ExecuteQuery(myCommand, "UPDATE [AccBankCheuqeDetails] SET  [BNK_CHQ_BOOK_DATE] = Convert(datetime,'" + ChequeDate + "',103),[BNK_CHQ_Post_flag] = 'Y' WHERE BNK_CHQ_NO='" + tdDao.TrnChequeNo + "'"))
                    //    {
                    //        myTrans.Rollback("PostAccHoldData"); return retValue;
                    //    }
                    //}

//                    if (tdDao.TrnChequeNo != "" && thDao.VoucherType == StringEnum.GetStringValue(VoucherType.MoneyReceipt) && tdDao.TrnTrntype == "C")
//                    {
//                        myCommand.CommandText = @" delete from AccTransactionReceivableBankInfo where Trn_Ref_No='" + thDao.TrnRefNo + "' and [Trn_Cheque_No]=" + tdDao.TrnChequeNo;

//                        myCommand.ExecuteNonQuery();
                        
//                        myCommand.CommandText = @"INSERT INTO [AccTransactionReceivableBankInfo] ([Trn_Ref_No],[Trn_Jrn_Code],[Trn_Line_No],
//                                                [Trn_Bank_Name],[Trn_Branch_Name],[Trn_Account_No],
//                                                [Trn_Cheque_No],[Trn_Cheque_DATE]) VALUES ('" +
//                                                thDao.TrnRefNo + "'," + thDao.TrnJrnCode + "," + tdDao.TrnLineNo + ",'" +
//                                                tdDao.RtrnBankName + "','" + tdDao.RtrnBranchName + "','" + tdDao.RtrnAccountNo + "','" +
//                                                tdDao.TrnChequeNo + "',convert(datetime,'" + ChequeDate + "',103))";
//                        myCommand.ExecuteNonQuery();
//                    }

                }
                if (!DeleteSaveData(myCommand, OldRef, userid))
                {
                    myTrans.Rollback("PostAccHoldData"); return retValue;
                }
                myTrans.Commit();
                retValue[0] = thDao.TrnRefNo;
                retValue[1] = thDao.TrnJrnCode.ToString();
                return retValue;
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback("PostAccHoldData");
                    System.Windows.Forms.MessageBox.Show(e.Message, StringProcess.messageHead);
                    return retValue;
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return retValue;
                    }
                    System.Windows.Forms.MessageBox.Show(ex.Message, StringProcess.messageHead);
                }

                return retValue;
            }
            finally
            {
                myConnection.Close();
            }

            return retValue;
        }

        public string saveData(string ConnectionStr, TransactionHeaderDAO thDao, List<TransactionDetailsDAO> tdDaolst,bool updateFlg)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionStr);
            myConnection.Open();

            SqlCommand myCommand = myConnection.CreateCommand();
            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";
            myTrans = myConnection.BeginTransaction("SaveAccData");

            myCommand.Connection = myConnection;
            myCommand.Transaction = myTrans;

            try
            {
                if (!updateFlg)
                {
                    myCommand.CommandText = "select (ISNULL(MAX(trn_jrn_code),0)+1) as maxJrnCode from AccTransactionHeaderHold";
                    sqlDataAdapterObj = new SqlDataAdapter();
                    sqlDataAdapterObj.SelectCommand = myCommand;
                    DataTable dtmaxJrnCode = new DataTable();
                    sqlDataAdapterObj.Fill(dtmaxJrnCode);
                    if (dtmaxJrnCode.Rows.Count > 0)
                        thDao.TrnJrnCode = Convert.ToDouble(dtmaxJrnCode.Rows[0]["maxJrnCode"].ToString());
                    else
                    {
                        myTrans.Rollback("SaveAccData"); return retValue;
                    }

                    thDao.TrnRefNo = GetReferenceNoForSave(myCommand, thDao.TrnJrnType, thDao.VoucherType, thDao.ModuleName, thDao.TrnAccPeriod, thDao.TrnDATE);
                    if (thDao.TrnRefNo == "")
                    {
                        myTrans.Rollback("SaveAccData"); return retValue;
                    }

                    myCommand.CommandText = "select * from AccTransactionHeaderHold where Trn_Ref_No='" + thDao.TrnRefNo + "'";
                    sqlDataAdapterObj = new SqlDataAdapter();
                    sqlDataAdapterObj.SelectCommand = myCommand;
                    DataTable dtmaxRefCheck = new DataTable();
                    sqlDataAdapterObj.Fill(dtmaxRefCheck);

                    if (dtmaxRefCheck.Rows.Count > 0)
                    {
                        myTrans.Rollback("SaveAccData");
                        System.Windows.Forms.MessageBox.Show("Refference No already Exits\rPlease Try Again Later", StringProcess.messageHead);
                        return retValue;
                    }
                }
                else
                {
                    DataTable dt = DataProcess.GetData(myCommand, "select * from AccTransactionHeaderHold where Trn_Ref_No='"+thDao.TrnRefNo+"'");
                    if (dt.Rows.Count > 0)
                        thDao.TrnJrnCode = Convert.ToDouble(dt.Rows[0]["trn_jrn_code"].ToString());
                }
                if (!updateFlg)
                {
                    myCommand.CommandText = @"INSERT INTO [AccTransactionHeaderHold]([Trn_Ref_No],[Trn_Jrn_Code],[Trn_Jrn_Type],[Trn_Voucher_Type],[Trn_Acc_Period],
                                        [Trn_Entry_DATE],[Trn_DATE],[Trn_Curr_Code],[Trn_Curr_Rate],[Trn_Entry_User],[Trn_LastUpdate_User],
                                        [Trn_LastUpdate_DATE],[Trn_Entry_Flag],[Trn_Revise_Flag],[Trn_Reverse_Flag]) VALUES('" +
                                            thDao.TrnRefNo + "'," + thDao.TrnJrnCode + ",'" + thDao.TrnJrnType + "','" + thDao.VoucherType + "','" + thDao.TrnAccPeriod + "',convert(datetime,'" +
                                            thDao.TrnEntryDATE + "',103),convert(datetime,'" + thDao.TrnDATE + "',103),'" + thDao.TrnCurrCode + "','" + thDao.TrnCurrRate + "','" + thDao.TrnEntryUser + "','" + thDao.TrnEntryUser + "',convert(datetime,'" +
                                            thDao.TrnEntryDATE + "',103),'" + thDao.TrnEntryFlag + "','" + thDao.TrnReviseFlag + "','" + thDao.TrnReverseFlag + "')";
                    myCommand.ExecuteNonQuery();
                }
                else
                {
                    myCommand.CommandText = @"UPDATE [AccTransactionHeaderHold]
                                                    SET [Trn_Acc_Period] = '" + thDao.TrnAccPeriod + "'" +
                                                        ",[Trn_DATE] = convert(datetime,'" + thDao.TrnDATE + "',103)" +
                                                        ",[Trn_LastUpdate_User] = '" + thDao.TrnEntryUser + "'" +
                                                        ",[Trn_LastUpdate_DATE] = convert(datetime,'" + thDao.TrnEntryDATE + "',103)" +
                                                        " WHERE [Trn_Ref_No]='" + thDao.TrnRefNo + "'";

                    myCommand.ExecuteNonQuery();
                }
                myCommand.CommandText = "delete from AccTransactionDetailsHold where Trn_Ref_No='" + thDao.TrnRefNo + "'";
                myCommand.ExecuteNonQuery();
                myCommand.CommandText = "delete from AccTransactionAnalysisHold where Trn_Ref_No='" + thDao.TrnRefNo + "'";
                myCommand.ExecuteNonQuery();

                foreach (TransactionDetailsDAO tdDao in tdDaolst)
                {
                    myCommand.CommandText = "select * from AccTransactionSplit_" + thDao.TrnEntryUser + " where Trn_Ac_Code='" + tdDao.TrnAcCode + "' and Trn_Line_No=" + tdDao.TrnLineNo;
                    sqlDataAdapterObj = new SqlDataAdapter();
                    sqlDataAdapterObj.SelectCommand = myCommand;
                    DataTable dtAnalysis = new DataTable();
                    sqlDataAdapterObj.Fill(dtAnalysis);

                    string ChequeDate = null;
                    if (tdDao.TrnChequeNo != "") ChequeDate = tdDao.TrnChequeDate.Date.ToString();

                    if (!DataProcess.ExecuteQuery(myCommand, "UPDATE [AccCOA] SET [Gl_Coa_Trn_Flg] ='Y'  WHERE Gl_Coa_Code='" + tdDao.TrnAcCode + "'"))
                    {
                        myTrans.Rollback("SaveAccData"); return retValue;
                    }

                    if (dtAnalysis.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtAnalysis.Rows)
                        {
                            myCommand.CommandText = "select * from AccTransactionDetailsHold where Trn_Ref_No='" + thDao.TrnRefNo + "' and Trn_Ac_Code='" + tdDao.TrnAcCode + "' and Trn_Line_No=" + tdDao.TrnLineNo + " and Trn_Sub_No=" + dr["Trn_Sub_No"].ToString();
                            sqlDataAdapterObj = new SqlDataAdapter();
                            sqlDataAdapterObj.SelectCommand = myCommand;
                            DataTable dtUpdate = new DataTable();
                            sqlDataAdapterObj.Fill(dtUpdate);

                            if (dtUpdate.Rows.Count==0)
                            {
                                myCommand.CommandText = @"INSERT INTO [AccTransactionDetailsHold]([Trn_Ref_No],[Trn_Ac_Code],[Trn_Jrn_Code],[Trn_Line_No],[Trn_Narration],
                                                    [Trn_Trn_type],[Trn_Amount],[Trn_Match],[Trn_Cheque_No],[Trn_Allocate_Flag],[Trn_Reval_Flag],
                                                    [Trn_Payment_DATE],[Trn_Ac_Desc],[Trn_Ac_Type],[Trn_Bus_Flag],[Trn_Due_DATE],[Trn_Sec_No],
                                                    [Trn_Adr_Code],[Trn_Dc_No],[Trn_GRN_No],[Trn_Bank_Reco_Flag],[Trn_Sub_No],[Trn_Tot_Int],
                                                    [Trn_Asset_Y_N],Trn_Instrument_Type,Trn_Cheque_Date,[T_Fl],[T_In],T_C1,T_C2,Trn_Code) VALUES('" +
                                                        thDao.TrnRefNo + "','" + tdDao.TrnAcCode + "','" + thDao.TrnJrnCode + "','" + tdDao.TrnLineNo + "','" + DataValidator.InvalidCharecterHandler(tdDao.TrnNarration) + "','" +
                                                        tdDao.TrnTrntype + "'," + dr["Trn_Amount"].ToString() + ",'" + tdDao.TrnMatch + "','" + tdDao.TrnChequeNo + "','" + tdDao.TrnAllocateFlag + "','" + tdDao.TrnRevalFlag + "',convert(datetime,'" +
                                                        tdDao.TrnPaymentDATE + "',103),'" + DataValidator.InvalidCharecterHandler(tdDao.TrnAcDesc) + "','" + tdDao.TrnAcType + "','" + tdDao.TrnBusFlag + "',convert(datetime,'" + tdDao.TrnDueDATE + "',103),'" + tdDao.TrnSecNo + "','" +
                                                        tdDao.TrnAdrCode + "','" + tdDao.TrnDcNo + "','" + tdDao.TrnGRNNo + "','" + tdDao.TrnBankRecoFlag + "','" + dr["Trn_Sub_No"].ToString() + "','" + tdDao.TrnTotInt + "','" +
                                                        tdDao.TrnAssetYN + "','" + tdDao.InstrumentType + "',Convert(datetime,'" + ChequeDate + "',103),'" + thDao.VoucherType + "','','','','')";
                                myCommand.ExecuteNonQuery();

                                myCommand.CommandText = @"INSERT INTO [AccTransactionAnalysisHold]([Trn_Ref_No],[Trn_Ac_Code],[Trn_Line_No],[Trn_Sub_No],
                                                    [Trn_AnaGroupDefinationCode1],[Trn_AnaGroupDefinationCode2],[Trn_AnaGroupDefinationCode3],
                                                    [Trn_AnaGroupDefinationCode4],[Trn_AnaGroupDefinationCode5],[Trn_AnaGroupLabelCode1],
                                                    [Trn_AnaGroupLabelCode2],[Trn_AnaGroupLabelCode3],[Trn_AnaGroupLabelCode4],
                                                    [Trn_AnaGroupLabelCode5]) VALUES('" +
                                                        thDao.TrnRefNo + "','" + tdDao.TrnAcCode + "'," + tdDao.TrnLineNo + "," + dr["Trn_Sub_No"].ToString() + ",'" +
                                                        dr["Trn_AnaGroupDefinationCode1"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode2"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode3"].ToString() + "','" +
                                                        dr["Trn_AnaGroupDefinationCode4"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode5"].ToString() + "','" + dr["Trn_AnaGroupLabelCode1"].ToString() + "','" +
                                                        dr["Trn_AnaGroupLabelCode2"].ToString() + "','" + dr["Trn_AnaGroupLabelCode3"].ToString() + "','" + dr["Trn_AnaGroupLabelCode4"].ToString() + "','" +
                                                        dr["Trn_AnaGroupLabelCode5"].ToString() + "')";
                                myCommand.ExecuteNonQuery();
                            }
                            else
                            {
                                myCommand.CommandText = @"UPDATE [AccTransactionDetailsHold]
                                                       SET [Trn_Narration] = '"+DataValidator.InvalidCharecterHandler(tdDao.TrnNarration)+"'"+
                                                          ",[Trn_Trn_type] = '"+tdDao.TrnTrntype+"'"+
                                                          ",[Trn_Amount] = " + dr["Trn_Amount"].ToString() +
                                                          ",[Trn_Match] = '"+tdDao.TrnMatch+"'"+
                                                          ",[Trn_Cheque_No] = '"+tdDao.TrnChequeNo+"'"+
                                                          ",[Trn_Allocate_Flag] = '"+tdDao.TrnAllocateFlag+"'"+
                                                          ",[Trn_Reval_Flag] = '"+tdDao.TrnRevalFlag+"'"+
                                                          ",[Trn_Payment_DATE] = Convert(datetime,'"+tdDao.TrnPaymentDATE+"',103)"+
                                                          ",[Trn_Due_DATE] = Convert(datetime,'" + tdDao.TrnDueDATE + "',103)" +
                                                          ",[Trn_Sec_No] = '"+tdDao.TrnSecNo+"'"+
                                                          ",[Trn_Adr_Code] = '"+tdDao.TrnAdrCode+"'"+
                                                          ",[Trn_Dc_No] = '"+tdDao.TrnDcNo+"'"+
                                                          ",[Trn_GRN_No] = '"+tdDao.TrnGRNNo+"'"+
                                                          ",[Trn_Bank_Reco_Flag] = '"+tdDao.TrnBankRecoFlag+"'"+
                                                          ",[Trn_Sub_No] = "+dr["Trn_Sub_No"].ToString()+
                                                          ",[Trn_Tot_Int] = "+ tdDao.TrnTotInt +
                                                          ",[Trn_Asset_Y_N] = '"+tdDao.TrnAssetYN+"'"+
                                                          ",Trn_Instrument_Type='"+tdDao.InstrumentType+"',Trn_Cheque_Date=convert(datetime,'"+ChequeDate+"',103)"+
                                                          " WHERE Trn_Ref_No='" + thDao.TrnRefNo + "' and Trn_Ac_Code='" + tdDao.TrnAcCode + "' and Trn_Line_No=" + tdDao.TrnLineNo + " and Trn_Sub_No=" + dr["Trn_Sub_No"].ToString(); 
                                myCommand.ExecuteNonQuery();

                                myCommand.CommandText = @"UPDATE [AccTransactionAnalysisHold]
                                                            SET [Trn_AnaGroupDefinationCode1] = '"+ dr["Trn_AnaGroupDefinationCode1"].ToString()+"'"+
                                                          ",[Trn_AnaGroupDefinationCode2] = '" + dr["Trn_AnaGroupDefinationCode2"].ToString() + "'" +
                                                          ",[Trn_AnaGroupDefinationCode3] = '" + dr["Trn_AnaGroupDefinationCode3"].ToString() + "'" +
                                                          ",[Trn_AnaGroupDefinationCode4] = '" + dr["Trn_AnaGroupDefinationCode4"].ToString() + "'" +
                                                          ",[Trn_AnaGroupDefinationCode5] = '" + dr["Trn_AnaGroupDefinationCode5"].ToString() + "'" +
                                                          ",[Trn_AnaGroupLabelCode1] = '" + dr["Trn_AnaGroupLabelCode1"].ToString() + "'" +
                                                          ",[Trn_AnaGroupLabelCode2] = '" + dr["Trn_AnaGroupLabelCode2"].ToString() + "'" +
                                                          ",[Trn_AnaGroupLabelCode3] = '" + dr["Trn_AnaGroupLabelCode3"].ToString() + "'" +
                                                          ",[Trn_AnaGroupLabelCode4] = '" + dr["Trn_AnaGroupLabelCode4"].ToString() + "'" +
                                                          ",[Trn_AnaGroupLabelCode5] = '" + dr["Trn_AnaGroupLabelCode5"].ToString() + "'" +
                                                          " WHERE Trn_Ref_No='"+thDao.TrnRefNo+"' and Trn_Ac_Code='" + tdDao.TrnAcCode + "' and Trn_Line_No=" + tdDao.TrnLineNo + " and Trn_Sub_No=" + dr["Trn_Sub_No"].ToString();
                                myCommand.ExecuteNonQuery();
                            }
                        }
                    }
                    else
                    {
                        myCommand.CommandText = "select * from AccTransactionDetailsHold where Trn_Ref_No='" + thDao.TrnRefNo + "' and Trn_Ac_Code='" + tdDao.TrnAcCode + "' and Trn_Line_No=" + tdDao.TrnLineNo ;
                        sqlDataAdapterObj = new SqlDataAdapter();
                        sqlDataAdapterObj.SelectCommand = myCommand;
                        DataTable dtUpdate = new DataTable();
                        sqlDataAdapterObj.Fill(dtUpdate);

                        if (dtUpdate.Rows.Count == 0)
                        {
                            myCommand.CommandText = @"INSERT INTO [AccTransactionDetailsHold]([Trn_Ref_No],[Trn_Ac_Code],[Trn_Jrn_Code],[Trn_Line_No],[Trn_Narration],
                                                    [Trn_Trn_type],[Trn_Amount],[Trn_Match],[Trn_Cheque_No],[Trn_Allocate_Flag],[Trn_Reval_Flag],
                                                    [Trn_Payment_DATE],[Trn_Ac_Desc],[Trn_Ac_Type],[Trn_Bus_Flag],[Trn_Due_DATE],[Trn_Sec_No],
                                                    [Trn_Adr_Code],[Trn_Dc_No],[Trn_GRN_No],[Trn_Bank_Reco_Flag],[Trn_Sub_No],[Trn_Tot_Int],
                                                    [Trn_Asset_Y_N],Trn_Instrument_Type,Trn_Cheque_Date,[T_Fl],[T_In],T_C1,T_C2,Trn_Code) VALUES('" +
                                                        thDao.TrnRefNo + "','" + tdDao.TrnAcCode + "','" + thDao.TrnJrnCode + "','" + tdDao.TrnLineNo + "','" + DataValidator.InvalidCharecterHandler(tdDao.TrnNarration) + "','" +
                                                        tdDao.TrnTrntype + "'," + tdDao.TrnAmount + ",'" + tdDao.TrnMatch + "','" + tdDao.TrnChequeNo + "','" + tdDao.TrnAllocateFlag + "','" + tdDao.TrnRevalFlag + "',convert(datetime,'" +
                                                        tdDao.TrnPaymentDATE + "',103),'" + DataValidator.InvalidCharecterHandler(tdDao.TrnAcDesc) + "','" + tdDao.TrnAcType + "','" + tdDao.TrnBusFlag + "',convert(datetime,'" + tdDao.TrnDueDATE + "',103),'" + tdDao.TrnSecNo + "','" +
                                                        tdDao.TrnAdrCode + "','" + tdDao.TrnDcNo + "','" + tdDao.TrnGRNNo + "','" + tdDao.TrnBankRecoFlag + "','" + tdDao.TrnSubNo + "','" + tdDao.TrnTotInt + "','" +
                                                        tdDao.TrnAssetYN + "','" + tdDao.InstrumentType + "',Convert(datetime,'" + ChequeDate + "',103),'" + thDao.VoucherType + "','','','','')";
                            myCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            myCommand.CommandText = @"UPDATE [AccTransactionDetailsHold]
                                                       SET [Trn_Narration] = '" + DataValidator.InvalidCharecterHandler(tdDao.TrnNarration) + "'" +
                                                         ",[Trn_Trn_type] = '" + tdDao.TrnTrntype + "'" +
                                                         ",[Trn_Amount] = " + tdDao.TrnAmount +
                                                         ",[Trn_Match] = '" + tdDao.TrnMatch + "'" +
                                                         ",[Trn_Cheque_No] = '" + tdDao.TrnChequeNo + "'" +
                                                         ",[Trn_Allocate_Flag] = '" + tdDao.TrnAllocateFlag + "'" +
                                                         ",[Trn_Reval_Flag] = '" + tdDao.TrnRevalFlag + "'" +
                                                         ",[Trn_Payment_DATE] = Convert(datetime,'" + tdDao.TrnPaymentDATE + "',103)" +
                                                         ",[Trn_Due_DATE] = Convert(datetime,'" + tdDao.TrnDueDATE + "',103)" +
                                                         ",[Trn_Sec_No] = '" + tdDao.TrnSecNo + "'" +
                                                         ",[Trn_Adr_Code] = '" + tdDao.TrnAdrCode + "'" +
                                                         ",[Trn_Dc_No] = '" + tdDao.TrnDcNo + "'" +
                                                         ",[Trn_GRN_No] = '" + tdDao.TrnGRNNo + "'" +
                                                         ",[Trn_Bank_Reco_Flag] = '" + tdDao.TrnBankRecoFlag + "'" +
                                                         ",[Trn_Sub_No] =0 " + 
                                                         ",[Trn_Tot_Int] = " + tdDao.TrnTotInt +
                                                         ",[Trn_Asset_Y_N] = '" + tdDao.TrnAssetYN + "'" +
                                                          ",Trn_Instrument_Type='" + tdDao.InstrumentType + "',Trn_Cheque_Date=convert(datetime,'" + ChequeDate + "',103)" +
                                                         " WHERE Trn_Ref_No='" + thDao.TrnRefNo + "' and Trn_Ac_Code='" + tdDao.TrnAcCode + "' and Trn_Line_No=" + tdDao.TrnLineNo ;
                            myCommand.ExecuteNonQuery();
                        }
                    }

                    if (tdDao.TrnChequeNo != "" && thDao.VoucherType == StringEnum.GetStringValue(VoucherType.ChequeVoucher) && tdDao.InstrumentType == Enum.GetName(typeof(InstrumentType), InstrumentType.Cheque))
                    {
                        if (!DataProcess.ExecuteQuery(myCommand, "UPDATE [AccBankCheuqeDetails] SET  [BNK_CHQ_BOOK_DATE] = Convert(datetime,'" + ChequeDate + "',103),[BNK_CHQ_Post_flag] = '' WHERE BNK_CHQ_NO='" + tdDao.TrnChequeNo + "'"))
                        {
                            myTrans.Rollback("SaveAccData"); return retValue;
                        }
                    }

                    if (Convert.ToString(tdDao.TrnChequeNo) != "" && thDao.VoucherType == StringEnum.GetStringValue(VoucherType.MoneyReceipt) && tdDao.TrnTrntype == "C")
                    {
                        myCommand.CommandText = @" delete from AccTransactionReceivableBankInfoHold where Trn_Ref_No='" + thDao.TrnRefNo + "' and [Trn_Cheque_No]=" + tdDao.TrnChequeNo;

                        myCommand.ExecuteNonQuery();

                        myCommand.CommandText = @"INSERT INTO [AccTransactionReceivableBankInfoHold] ([Trn_Ref_No],[Trn_Jrn_Code],[Trn_Line_No],
                                                [Trn_Bank_Name],[Trn_Branch_Name],[Trn_Account_No],
                                                [Trn_Cheque_No],[Trn_Cheque_DATE]) VALUES ('" +
                                                thDao.TrnRefNo + "'," + thDao.TrnJrnCode + "," + tdDao.TrnLineNo + ",'" +
                                                tdDao.RtrnBankName + "','" + tdDao.RtrnBranchName + "','" + tdDao.RtrnAccountNo + "','" +
                                                tdDao.TrnChequeNo + "',convert(datetime,'" + ChequeDate + "',103))";
                        myCommand.ExecuteNonQuery();
                    }

                }

                string updaterefno = thDao.TrnRefNo.ToString().Substring(thDao.TrnRefNo.ToString().Length - 6);
                // Update referance number setup table

                if (!DataProcess.ExecuteQuery(myCommand, "update AccJournalSetup set JrnNextRefNo='" + updaterefno + "' where jrntype='" + thDao.TrnJrnType + "' and VoucherType='" + thDao.VoucherType + "' and JrnModule='" + thDao.ModuleName + "'"))
                {
                    myTrans.Rollback("SaveAccData"); return retValue;
                }          

                myTrans.Commit();
                
                return thDao.TrnRefNo;
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback("SaveAccData");
                    System.Windows.Forms.MessageBox.Show(e.Message, StringProcess.messageHead);
                    return retValue;
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return retValue;
                    }
                    System.Windows.Forms.MessageBox.Show(ex.Message, StringProcess.messageHead);
                }

                return retValue;
            }
            finally
            {
                myConnection.Close();
            }

          
        }

        public string BookAccountDataOfMRR(string ConnectionStr, TransactionHeaderDAO thDao, List<TransactionDetailsDAO> tdDaolst, bool updateFlg)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionStr);
            myConnection.Open();

            SqlCommand myCommand = myConnection.CreateCommand();
            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";
            string grnno="";
            myTrans = myConnection.BeginTransaction("SaveAccData");

            myCommand.Connection = myConnection;
            myCommand.Transaction = myTrans;

            try
            {
                if (!updateFlg)
                {
                    myCommand.CommandText = "select (ISNULL(MAX(trn_jrn_code),0)+1) as maxJrnCode from AccTransactionHeaderHold";
                    sqlDataAdapterObj = new SqlDataAdapter();
                    sqlDataAdapterObj.SelectCommand = myCommand;
                    DataTable dtmaxJrnCode = new DataTable();
                    sqlDataAdapterObj.Fill(dtmaxJrnCode);
                    if (dtmaxJrnCode.Rows.Count > 0)
                        thDao.TrnJrnCode = Convert.ToDouble(dtmaxJrnCode.Rows[0]["maxJrnCode"].ToString());
                    else
                    {
                        myTrans.Rollback("SaveAccData"); return retValue;
                    }

                    thDao.TrnRefNo = GetReferenceNoForSave(myCommand, thDao.TrnJrnType, thDao.VoucherType, thDao.ModuleName, thDao.TrnAccPeriod, thDao.TrnDATE);
                    if (thDao.TrnRefNo == "")
                    {
                        myTrans.Rollback("SaveAccData"); return retValue;
                    }

                    myCommand.CommandText = "select * from AccTransactionHeaderHold where Trn_Ref_No='" + thDao.TrnRefNo + "'";
                    sqlDataAdapterObj = new SqlDataAdapter();
                    sqlDataAdapterObj.SelectCommand = myCommand;
                    DataTable dtmaxRefCheck = new DataTable();
                    sqlDataAdapterObj.Fill(dtmaxRefCheck);

                    if (dtmaxRefCheck.Rows.Count > 0)
                    {
                        myTrans.Rollback("SaveAccData");
                        System.Windows.Forms.MessageBox.Show("Refference No already Exits\rPlease Try Again Later", StringProcess.messageHead);
                        return retValue;
                    }
                }
                else
                {
                    DataTable dt = DataProcess.GetData(myCommand, "select * from AccTransactionHeaderHold where Trn_Ref_No='" + thDao.TrnRefNo + "'");
                    if (dt.Rows.Count > 0)
                        thDao.TrnJrnCode = Convert.ToDouble(dt.Rows[0]["trn_jrn_code"].ToString());
                }
                if (!updateFlg)
                {
                    myCommand.CommandText = @"INSERT INTO [AccTransactionHeaderHold]([Trn_Ref_No],[Trn_Jrn_Code],[Trn_Jrn_Type],[Trn_Voucher_Type],[Trn_Acc_Period],
                                        [Trn_Entry_DATE],[Trn_DATE],[Trn_Curr_Code],[Trn_Curr_Rate],[Trn_Entry_User],[Trn_LastUpdate_User],
                                        [Trn_LastUpdate_DATE],[Trn_Entry_Flag],[Trn_Revise_Flag],[Trn_Reverse_Flag]) VALUES('" +
                                            thDao.TrnRefNo + "'," + thDao.TrnJrnCode + ",'" + thDao.TrnJrnType + "','" + thDao.VoucherType + "','" + thDao.TrnAccPeriod + "',convert(datetime,'" +
                                            thDao.TrnEntryDATE + "',103),convert(datetime,'" + thDao.TrnDATE + "',103),'" + thDao.TrnCurrCode + "','" + thDao.TrnCurrRate + "','" + thDao.TrnEntryUser + "','" + thDao.TrnEntryUser + "',convert(datetime,'" +
                                            thDao.TrnEntryDATE + "',103),'" + thDao.TrnEntryFlag + "','" + thDao.TrnReviseFlag + "','" + thDao.TrnReverseFlag + "')";
                    myCommand.ExecuteNonQuery();
                }
                else
                {
                    myCommand.CommandText = @"UPDATE [AccTransactionHeaderHold]
                                                    SET [Trn_Acc_Period] = '" + thDao.TrnAccPeriod + "'" +
                                                        ",[Trn_DATE] = convert(datetime,'" + thDao.TrnDATE + "',103)" +
                                                        ",[Trn_LastUpdate_User] = '" + thDao.TrnEntryUser + "'" +
                                                        ",[Trn_LastUpdate_DATE] = convert(datetime,'" + thDao.TrnEntryDATE + "',103)" +
                                                        " WHERE [Trn_Ref_No]='" + thDao.TrnRefNo + "'";

                    myCommand.ExecuteNonQuery();
                }
                myCommand.CommandText = "delete from AccTransactionDetailsHold where Trn_Ref_No='" + thDao.TrnRefNo + "'";
                myCommand.ExecuteNonQuery();
                myCommand.CommandText = "delete from AccTransactionAnalysisHold where Trn_Ref_No='" + thDao.TrnRefNo + "'";
                myCommand.ExecuteNonQuery();

                foreach (TransactionDetailsDAO tdDao in tdDaolst)
                {
                    myCommand.CommandText = "select * from AccTransactionSplit_" + thDao.TrnEntryUser + " where Trn_Ac_Code='" + tdDao.TrnAcCode + "' and Trn_Line_No=" + tdDao.TrnLineNo;
                    sqlDataAdapterObj = new SqlDataAdapter();
                    sqlDataAdapterObj.SelectCommand = myCommand;
                    DataTable dtAnalysis = new DataTable();
                    sqlDataAdapterObj.Fill(dtAnalysis);

                    string ChequeDate = null;
                    if (tdDao.TrnChequeNo != "") ChequeDate = tdDao.TrnChequeDate.Date.ToString();
                    grnno=tdDao.TrnGRNNo.ToString();

                    if (!DataProcess.ExecuteQuery(myCommand, "UPDATE [AccCOA] SET [Gl_Coa_Trn_Flg] ='Y'  WHERE Gl_Coa_Code='" + tdDao.TrnAcCode + "'"))
                    {
                        myTrans.Rollback("SaveAccData"); return retValue;
                    }

                    if (dtAnalysis.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtAnalysis.Rows)
                        {
                            myCommand.CommandText = "select * from AccTransactionDetailsHold where Trn_Ref_No='" + thDao.TrnRefNo + "' and Trn_Ac_Code='" + tdDao.TrnAcCode + "' and Trn_Line_No=" + tdDao.TrnLineNo + " and Trn_Sub_No=" + dr["Trn_Sub_No"].ToString();
                            sqlDataAdapterObj = new SqlDataAdapter();
                            sqlDataAdapterObj.SelectCommand = myCommand;
                            DataTable dtUpdate = new DataTable();
                            sqlDataAdapterObj.Fill(dtUpdate);

                            if (dtUpdate.Rows.Count == 0)
                            {
                                myCommand.CommandText = @"INSERT INTO [AccTransactionDetailsHold]([Trn_Ref_No],[Trn_Ac_Code],[Trn_Jrn_Code],[Trn_Line_No],[Trn_Narration],
                                                    [Trn_Trn_type],[Trn_Amount],[Trn_Match],[Trn_Cheque_No],[Trn_Allocate_Flag],[Trn_Reval_Flag],
                                                    [Trn_Payment_DATE],[Trn_Ac_Desc],[Trn_Ac_Type],[Trn_Bus_Flag],[Trn_Due_DATE],[Trn_Sec_No],
                                                    [Trn_Adr_Code],[Trn_Dc_No],[Trn_GRN_No],[Trn_Bank_Reco_Flag],[Trn_Sub_No],[Trn_Tot_Int],
                                                    [Trn_Asset_Y_N],Trn_Instrument_Type,Trn_Cheque_Date,[T_Fl],[T_In],T_C1,T_C2,Trn_Code) VALUES('" +
                                                        thDao.TrnRefNo + "','" + tdDao.TrnAcCode + "','" + thDao.TrnJrnCode + "','" + tdDao.TrnLineNo + "','" + DataValidator.InvalidCharecterHandler(tdDao.TrnNarration) + "','" +
                                                        tdDao.TrnTrntype + "'," + dr["Trn_Amount"].ToString() + ",'" + tdDao.TrnMatch + "','" + tdDao.TrnChequeNo + "','" + tdDao.TrnAllocateFlag + "','" + tdDao.TrnRevalFlag + "',convert(datetime,'" +
                                                        tdDao.TrnPaymentDATE + "',103),'" + DataValidator.InvalidCharecterHandler(tdDao.TrnAcDesc) + "','" + tdDao.TrnAcType + "','" + tdDao.TrnBusFlag + "',convert(datetime,'" + tdDao.TrnDueDATE + "',103),'" + tdDao.TrnSecNo + "','" +
                                                        tdDao.TrnAdrCode + "','" + tdDao.TrnDcNo + "','" + tdDao.TrnGRNNo + "','" + tdDao.TrnBankRecoFlag + "','" + dr["Trn_Sub_No"].ToString() + "','" + tdDao.TrnTotInt + "','" +
                                                        tdDao.TrnAssetYN + "','" + tdDao.InstrumentType + "',Convert(datetime,'" + ChequeDate + "',103),'" + thDao.VoucherType + "','','','','')";
                                myCommand.ExecuteNonQuery();

                                myCommand.CommandText = @"INSERT INTO [AccTransactionAnalysisHold]([Trn_Ref_No],[Trn_Ac_Code],[Trn_Line_No],[Trn_Sub_No],
                                                    [Trn_AnaGroupDefinationCode1],[Trn_AnaGroupDefinationCode2],[Trn_AnaGroupDefinationCode3],
                                                    [Trn_AnaGroupDefinationCode4],[Trn_AnaGroupDefinationCode5],[Trn_AnaGroupLabelCode1],
                                                    [Trn_AnaGroupLabelCode2],[Trn_AnaGroupLabelCode3],[Trn_AnaGroupLabelCode4],
                                                    [Trn_AnaGroupLabelCode5]) VALUES('" +
                                                        thDao.TrnRefNo + "','" + tdDao.TrnAcCode + "'," + tdDao.TrnLineNo + "," + dr["Trn_Sub_No"].ToString() + ",'" +
                                                        dr["Trn_AnaGroupDefinationCode1"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode2"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode3"].ToString() + "','" +
                                                        dr["Trn_AnaGroupDefinationCode4"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode5"].ToString() + "','" + dr["Trn_AnaGroupLabelCode1"].ToString() + "','" +
                                                        dr["Trn_AnaGroupLabelCode2"].ToString() + "','" + dr["Trn_AnaGroupLabelCode3"].ToString() + "','" + dr["Trn_AnaGroupLabelCode4"].ToString() + "','" +
                                                        dr["Trn_AnaGroupLabelCode5"].ToString() + "')";
                                myCommand.ExecuteNonQuery();
                            }
                            else
                            {
                                myCommand.CommandText = @"UPDATE [AccTransactionDetailsHold]
                                                       SET [Trn_Narration] = '" + DataValidator.InvalidCharecterHandler(tdDao.TrnNarration) + "'" +
                                                          ",[Trn_Trn_type] = '" + tdDao.TrnTrntype + "'" +
                                                          ",[Trn_Amount] = " + dr["Trn_Amount"].ToString() +
                                                          ",[Trn_Match] = '" + tdDao.TrnMatch + "'" +
                                                          ",[Trn_Cheque_No] = '" + tdDao.TrnChequeNo + "'" +
                                                          ",[Trn_Allocate_Flag] = '" + tdDao.TrnAllocateFlag + "'" +
                                                          ",[Trn_Reval_Flag] = '" + tdDao.TrnRevalFlag + "'" +
                                                          ",[Trn_Payment_DATE] = Convert(datetime,'" + tdDao.TrnPaymentDATE + "',103)" +
                                                          ",[Trn_Due_DATE] = Convert(datetime,'" + tdDao.TrnDueDATE + "',103)" +
                                                          ",[Trn_Sec_No] = '" + tdDao.TrnSecNo + "'" +
                                                          ",[Trn_Adr_Code] = '" + tdDao.TrnAdrCode + "'" +
                                                          ",[Trn_Dc_No] = '" + tdDao.TrnDcNo + "'" +
                                                          ",[Trn_GRN_No] = '" + tdDao.TrnGRNNo + "'" +
                                                          ",[Trn_Bank_Reco_Flag] = '" + tdDao.TrnBankRecoFlag + "'" +
                                                          ",[Trn_Sub_No] = " + dr["Trn_Sub_No"].ToString() +
                                                          ",[Trn_Tot_Int] = " + tdDao.TrnTotInt +
                                                          ",[Trn_Asset_Y_N] = '" + tdDao.TrnAssetYN + "'" +
                                                          ",Trn_Instrument_Type='" + tdDao.InstrumentType + "',Trn_Cheque_Date=convert(datetime,'" + ChequeDate + "',103)" +
                                                          " WHERE Trn_Ref_No='" + thDao.TrnRefNo + "' and Trn_Ac_Code='" + tdDao.TrnAcCode + "' and Trn_Line_No=" + tdDao.TrnLineNo + " and Trn_Sub_No=" + dr["Trn_Sub_No"].ToString();
                                myCommand.ExecuteNonQuery();

                                myCommand.CommandText = @"UPDATE [AccTransactionAnalysisHold]
                                                            SET [Trn_AnaGroupDefinationCode1] = '" + dr["Trn_AnaGroupDefinationCode1"].ToString() + "'" +
                                                          ",[Trn_AnaGroupDefinationCode2] = '" + dr["Trn_AnaGroupDefinationCode2"].ToString() + "'" +
                                                          ",[Trn_AnaGroupDefinationCode3] = '" + dr["Trn_AnaGroupDefinationCode3"].ToString() + "'" +
                                                          ",[Trn_AnaGroupDefinationCode4] = '" + dr["Trn_AnaGroupDefinationCode4"].ToString() + "'" +
                                                          ",[Trn_AnaGroupDefinationCode5] = '" + dr["Trn_AnaGroupDefinationCode5"].ToString() + "'" +
                                                          ",[Trn_AnaGroupLabelCode1] = '" + dr["Trn_AnaGroupLabelCode1"].ToString() + "'" +
                                                          ",[Trn_AnaGroupLabelCode2] = '" + dr["Trn_AnaGroupLabelCode2"].ToString() + "'" +
                                                          ",[Trn_AnaGroupLabelCode3] = '" + dr["Trn_AnaGroupLabelCode3"].ToString() + "'" +
                                                          ",[Trn_AnaGroupLabelCode4] = '" + dr["Trn_AnaGroupLabelCode4"].ToString() + "'" +
                                                          ",[Trn_AnaGroupLabelCode5] = '" + dr["Trn_AnaGroupLabelCode5"].ToString() + "'" +
                                                          " WHERE Trn_Ref_No='" + thDao.TrnRefNo + "' and Trn_Ac_Code='" + tdDao.TrnAcCode + "' and Trn_Line_No=" + tdDao.TrnLineNo + " and Trn_Sub_No=" + dr["Trn_Sub_No"].ToString();
                                myCommand.ExecuteNonQuery();
                            }
                        }
                    }
                    else
                    {
                        myCommand.CommandText = "select * from AccTransactionDetailsHold where Trn_Ref_No='" + thDao.TrnRefNo + "' and Trn_Ac_Code='" + tdDao.TrnAcCode + "' and Trn_Line_No=" + tdDao.TrnLineNo;
                        sqlDataAdapterObj = new SqlDataAdapter();
                        sqlDataAdapterObj.SelectCommand = myCommand;
                        DataTable dtUpdate = new DataTable();
                        sqlDataAdapterObj.Fill(dtUpdate);

                        if (dtUpdate.Rows.Count == 0)
                        {
                            myCommand.CommandText = @"INSERT INTO [AccTransactionDetailsHold]([Trn_Ref_No],[Trn_Ac_Code],[Trn_Jrn_Code],[Trn_Line_No],[Trn_Narration],
                                                    [Trn_Trn_type],[Trn_Amount],[Trn_Match],[Trn_Cheque_No],[Trn_Allocate_Flag],[Trn_Reval_Flag],
                                                    [Trn_Payment_DATE],[Trn_Ac_Desc],[Trn_Ac_Type],[Trn_Bus_Flag],[Trn_Due_DATE],[Trn_Sec_No],
                                                    [Trn_Adr_Code],[Trn_Dc_No],[Trn_GRN_No],[Trn_Bank_Reco_Flag],[Trn_Sub_No],[Trn_Tot_Int],
                                                    [Trn_Asset_Y_N],Trn_Instrument_Type,Trn_Cheque_Date,[T_Fl],[T_In],T_C1,T_C2,Trn_Code) VALUES('" +
                                                        thDao.TrnRefNo + "','" + tdDao.TrnAcCode + "','" + thDao.TrnJrnCode + "','" + tdDao.TrnLineNo + "','" + DataValidator.InvalidCharecterHandler(tdDao.TrnNarration) + "','" +
                                                        tdDao.TrnTrntype + "'," + tdDao.TrnAmount + ",'" + tdDao.TrnMatch + "','" + tdDao.TrnChequeNo + "','" + tdDao.TrnAllocateFlag + "','" + tdDao.TrnRevalFlag + "',convert(datetime,'" +
                                                        tdDao.TrnPaymentDATE + "',103),'" + DataValidator.InvalidCharecterHandler(tdDao.TrnAcDesc) + "','" + tdDao.TrnAcType + "','" + tdDao.TrnBusFlag + "',convert(datetime,'" + tdDao.TrnDueDATE + "',103),'" + tdDao.TrnSecNo + "','" +
                                                        tdDao.TrnAdrCode + "','" + tdDao.TrnDcNo + "','" + tdDao.TrnGRNNo + "','" + tdDao.TrnBankRecoFlag + "','" + tdDao.TrnSubNo + "','" + tdDao.TrnTotInt + "','" +
                                                        tdDao.TrnAssetYN + "','" + tdDao.InstrumentType + "',Convert(datetime,'" + ChequeDate + "',103),'" + thDao.VoucherType + "','','','','')";
                            myCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            myCommand.CommandText = @"UPDATE [AccTransactionDetailsHold]
                                                       SET [Trn_Narration] = '" + DataValidator.InvalidCharecterHandler(tdDao.TrnNarration) + "'" +
                                                         ",[Trn_Trn_type] = '" + tdDao.TrnTrntype + "'" +
                                                         ",[Trn_Amount] = " + tdDao.TrnAmount +
                                                         ",[Trn_Match] = '" + tdDao.TrnMatch + "'" +
                                                         ",[Trn_Cheque_No] = '" + tdDao.TrnChequeNo + "'" +
                                                         ",[Trn_Allocate_Flag] = '" + tdDao.TrnAllocateFlag + "'" +
                                                         ",[Trn_Reval_Flag] = '" + tdDao.TrnRevalFlag + "'" +
                                                         ",[Trn_Payment_DATE] = Convert(datetime,'" + tdDao.TrnPaymentDATE + "',103)" +
                                                         ",[Trn_Due_DATE] = Convert(datetime,'" + tdDao.TrnDueDATE + "',103)" +
                                                         ",[Trn_Sec_No] = '" + tdDao.TrnSecNo + "'" +
                                                         ",[Trn_Adr_Code] = '" + tdDao.TrnAdrCode + "'" +
                                                         ",[Trn_Dc_No] = '" + tdDao.TrnDcNo + "'" +
                                                         ",[Trn_GRN_No] = '" + tdDao.TrnGRNNo + "'" +
                                                         ",[Trn_Bank_Reco_Flag] = '" + tdDao.TrnBankRecoFlag + "'" +
                                                         ",[Trn_Sub_No] =0 " +
                                                         ",[Trn_Tot_Int] = " + tdDao.TrnTotInt +
                                                         ",[Trn_Asset_Y_N] = '" + tdDao.TrnAssetYN + "'" +
                                                          ",Trn_Instrument_Type='" + tdDao.InstrumentType + "',Trn_Cheque_Date=convert(datetime,'" + ChequeDate + "',103)" +
                                                         " WHERE Trn_Ref_No='" + thDao.TrnRefNo + "' and Trn_Ac_Code='" + tdDao.TrnAcCode + "' and Trn_Line_No=" + tdDao.TrnLineNo;
                            myCommand.ExecuteNonQuery();
                        }
                    }

                    if (tdDao.TrnChequeNo != "" && thDao.VoucherType == StringEnum.GetStringValue(VoucherType.ChequeVoucher) && tdDao.InstrumentType == Enum.GetName(typeof(InstrumentType), InstrumentType.Cheque))
                    {
                        if (!DataProcess.ExecuteQuery(myCommand, "UPDATE [AccBankCheuqeDetails] SET  [BNK_CHQ_BOOK_DATE] = Convert(datetime,'" + ChequeDate + "',103),[BNK_CHQ_Post_flag] = '' WHERE BNK_CHQ_NO='" + tdDao.TrnChequeNo + "'"))
                        {
                            myTrans.Rollback("SaveAccData"); return retValue;
                        }
                    }

                    if (Convert.ToString(tdDao.TrnChequeNo) != "" && thDao.VoucherType == StringEnum.GetStringValue(VoucherType.MoneyReceipt) && tdDao.TrnTrntype == "C")
                    {
                        myCommand.CommandText = @" delete from AccTransactionReceivableBankInfoHold where Trn_Ref_No='" + thDao.TrnRefNo + "' and [Trn_Cheque_No]=" + tdDao.TrnChequeNo;

                        myCommand.ExecuteNonQuery();

                        myCommand.CommandText = @"INSERT INTO [AccTransactionReceivableBankInfoHold] ([Trn_Ref_No],[Trn_Jrn_Code],[Trn_Line_No],
                                                [Trn_Bank_Name],[Trn_Branch_Name],[Trn_Account_No],
                                                [Trn_Cheque_No],[Trn_Cheque_DATE]) VALUES ('" +
                                                thDao.TrnRefNo + "'," + thDao.TrnJrnCode + "," + tdDao.TrnLineNo + ",'" +
                                                tdDao.RtrnBankName + "','" + tdDao.RtrnBranchName + "','" + tdDao.RtrnAccountNo + "','" +
                                                tdDao.TrnChequeNo + "',convert(datetime,'" + ChequeDate + "',103))";
                        myCommand.ExecuteNonQuery();
                    }

                }

                string updaterefno = thDao.TrnRefNo.ToString().Substring(thDao.TrnRefNo.ToString().Length - 6);
                // Update referance number setup table

                if (!DataProcess.ExecuteQuery(myCommand, "update AccJournalSetup set JrnNextRefNo='" + updaterefno + "' where jrntype='" + thDao.TrnJrnType + "' and VoucherType='" + thDao.VoucherType + "' and JrnModule='" + thDao.ModuleName + "'"))
                {
                    myTrans.Rollback("SaveAccData"); return retValue;
                }
                                
                //Payableprogress jurnal
                DateTime adjdtp = DateProcess.LastDateOfMonth(thDao.TrnDATE);

                myCommand.CommandText = @"INSERT INTO [AccPaybleProgress] ([ReferenceNumber],[Status],[InvoiceReceived],[AdjustmentPeriod],
                                                [EntryUserID],[EntryDate],[UpdateUserid],
                                                [UpdateDate],Jrnupdpermission) VALUES ('" +
                                                grnno.ToString() + "','Y','N',convert(datetime,'" + adjdtp + "',103),'" + thDao.TrnEntryUser + "',convert(datetime,'" + thDao.TrnEntryDATE + "',103),'" + thDao.TrnEntryUser + "',convert(datetime,'" + thDao.TrnEntryDATE + "',103),'Y')";
                myCommand.ExecuteNonQuery();

                

                myTrans.Commit();

                return thDao.TrnRefNo;
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback("SaveAccData");
                    System.Windows.Forms.MessageBox.Show(e.Message, StringProcess.messageHead);
                    return retValue;
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return retValue;
                    }
                    System.Windows.Forms.MessageBox.Show(ex.Message, StringProcess.messageHead);
                }

                return retValue;
            }
            finally
            {
                myConnection.Close();
            }


        }

        public string JournalforReceiptInvoice(string ConnectionStr, TransactionHeaderDAO thDao, List<TransactionDetailsDAO> tdDaolst,string vatacc,string taxacc,string invno, bool updateFlg)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionStr);
            myConnection.Open();

            SqlCommand myCommand = myConnection.CreateCommand();
            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";
            string grnno = "";
            myTrans = myConnection.BeginTransaction("SaveAccData");

            myCommand.Connection = myConnection;
            myCommand.Transaction = myTrans;

            try
            {
                if (!updateFlg)
                {
                    myCommand.CommandText = "select (ISNULL(MAX(trn_jrn_code),0)+1) as maxJrnCode from AccTransactionHeader";
                    sqlDataAdapterObj = new SqlDataAdapter();
                    sqlDataAdapterObj.SelectCommand = myCommand;
                    DataTable dtmaxJrnCode = new DataTable();
                    sqlDataAdapterObj.Fill(dtmaxJrnCode);
                    if (dtmaxJrnCode.Rows.Count > 0)
                        thDao.TrnJrnCode = Convert.ToDouble(dtmaxJrnCode.Rows[0]["maxJrnCode"].ToString());
                    else
                    {
                        myTrans.Rollback("SaveAccData"); return retValue;
                    }

                    thDao.TrnRefNo = GetReferenceNo(myCommand, thDao.TrnJrnType, thDao.VoucherType, thDao.ModuleName, thDao.TrnAccPeriod, thDao.TrnDATE);
                    if (thDao.TrnRefNo == "")
                    {
                        myTrans.Rollback("SaveAccData"); return retValue;
                    }

                    myCommand.CommandText = "select * from AccTransactionHeader where Trn_Ref_No='" + thDao.TrnRefNo + "'";
                    sqlDataAdapterObj = new SqlDataAdapter();
                    sqlDataAdapterObj.SelectCommand = myCommand;
                    DataTable dtmaxRefCheck = new DataTable();
                    sqlDataAdapterObj.Fill(dtmaxRefCheck);

                    if (dtmaxRefCheck.Rows.Count > 0)
                    {
                        myTrans.Rollback("SaveAccData");
                        System.Windows.Forms.MessageBox.Show("Refference No already Exits\rPlease Try Again Later", StringProcess.messageHead);
                        return retValue;
                    }
                }
                else
                {
                    DataTable dt = DataProcess.GetData(myCommand, "select * from AccTransactionHeader where Trn_Ref_No='" + thDao.TrnRefNo + "'");
                    if (dt.Rows.Count > 0)
                        thDao.TrnJrnCode = Convert.ToDouble(dt.Rows[0]["trn_jrn_code"].ToString());
                }
                if (!updateFlg)
                {
                    myCommand.CommandText = @"INSERT INTO [AccTransactionHeader]([Trn_Ref_No],[Trn_Jrn_Code],[Trn_Jrn_Type],[Trn_Voucher_Type],[Trn_Acc_Period],
                                        [Trn_Entry_DATE],[Trn_DATE],[Trn_Curr_Code],[Trn_Curr_Rate],[Trn_Entry_User],[Trn_LastUpdate_User],
                                        [Trn_LastUpdate_DATE],[Trn_Entry_Flag],[Trn_Revise_Flag],[Trn_Reverse_Flag]) VALUES('" +
                                            thDao.TrnRefNo + "'," + thDao.TrnJrnCode + ",'" + thDao.TrnJrnType + "','" + thDao.VoucherType + "','" + thDao.TrnAccPeriod + "',convert(datetime,'" +
                                            thDao.TrnEntryDATE + "',103),convert(datetime,'" + thDao.TrnDATE + "',103),'" + thDao.TrnCurrCode + "','" + thDao.TrnCurrRate + "','" + thDao.TrnEntryUser + "','" + thDao.TrnEntryUser + "',convert(datetime,'" +
                                            thDao.TrnEntryDATE + "',103),'" + thDao.TrnEntryFlag + "','" + thDao.TrnReviseFlag + "','" + thDao.TrnReverseFlag + "')";
                    myCommand.ExecuteNonQuery();
                }
                else
                {
                    myCommand.CommandText = @"UPDATE [AccTransactionHeader]
                                                    SET [Trn_Acc_Period] = '" + thDao.TrnAccPeriod + "'" +
                                                        ",[Trn_DATE] = convert(datetime,'" + thDao.TrnDATE + "',103)" +
                                                        ",[Trn_LastUpdate_User] = '" + thDao.TrnEntryUser + "'" +
                                                        ",[Trn_LastUpdate_DATE] = convert(datetime,'" + thDao.TrnEntryDATE + "',103)" +
                                                        " WHERE [Trn_Ref_No]='" + thDao.TrnRefNo + "'";

                    myCommand.ExecuteNonQuery();
                }
                myCommand.CommandText = "delete from AccTransactionDetails where Trn_Ref_No='" + thDao.TrnRefNo + "'";
                myCommand.ExecuteNonQuery();
                myCommand.CommandText = "delete from AccTransactionAnalysis where Trn_Ref_No='" + thDao.TrnRefNo + "'";
                myCommand.ExecuteNonQuery();

                foreach (TransactionDetailsDAO tdDao in tdDaolst)
                {
                    myCommand.CommandText = "select * from AccTransactionSplit_" + thDao.TrnEntryUser + " where Trn_Ac_Code='" + tdDao.TrnAcCode + "' and Trn_Line_No=" + tdDao.TrnLineNo;
                    sqlDataAdapterObj = new SqlDataAdapter();
                    sqlDataAdapterObj.SelectCommand = myCommand;
                    DataTable dtAnalysis = new DataTable();
                    sqlDataAdapterObj.Fill(dtAnalysis);

                    string ChequeDate = null;
                    if (tdDao.TrnChequeNo != "") ChequeDate = tdDao.TrnChequeDate.Date.ToString();
                    grnno = tdDao.TrnGRNNo.ToString();

                    if (!DataProcess.ExecuteQuery(myCommand, "UPDATE [AccCOA] SET [Gl_Coa_Trn_Flg] ='Y'  WHERE Gl_Coa_Code='" + tdDao.TrnAcCode + "'"))
                    {
                        myTrans.Rollback("SaveAccData"); return retValue;
                    }

                    if (dtAnalysis.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtAnalysis.Rows)
                        {
                            myCommand.CommandText = "select * from AccTransactionDetails where Trn_Ref_No='" + thDao.TrnRefNo + "' and Trn_Ac_Code='" + tdDao.TrnAcCode + "' and Trn_Line_No=" + tdDao.TrnLineNo + " and Trn_Sub_No=" + dr["Trn_Sub_No"].ToString();
                            sqlDataAdapterObj = new SqlDataAdapter();
                            sqlDataAdapterObj.SelectCommand = myCommand;
                            DataTable dtUpdate = new DataTable();
                            sqlDataAdapterObj.Fill(dtUpdate);

                            if (dtUpdate.Rows.Count == 0)
                            {
                                myCommand.CommandText = @"INSERT INTO [AccTransactionDetails]([Trn_Ref_No],[Trn_Ac_Code],[Trn_Jrn_Code],[Trn_Line_No],[Trn_Narration],
                                                    [Trn_Trn_type],[Trn_Amount],[Trn_Match],[Trn_Cheque_No],[Trn_Allocate_Flag],[Trn_Reval_Flag],
                                                    [Trn_Payment_DATE],[Trn_Ac_Desc],[Trn_Ac_Type],[Trn_Bus_Flag],[Trn_Due_DATE],[Trn_Sec_No],
                                                    [Trn_Adr_Code],[Trn_Dc_No],[Trn_GRN_No],[Trn_Bank_Reco_Flag],[Trn_Sub_No],[Trn_Tot_Int],
                                                    [Trn_Asset_Y_N],Trn_Instrument_Type,Trn_Cheque_Date,[T_Fl],[T_In],T_C1,T_C2,Trn_Code) VALUES('" +
                                                        thDao.TrnRefNo + "','" + tdDao.TrnAcCode + "','" + thDao.TrnJrnCode + "','" + tdDao.TrnLineNo + "','" + DataValidator.InvalidCharecterHandler(tdDao.TrnNarration) + "','" +
                                                        tdDao.TrnTrntype + "'," + dr["Trn_Amount"].ToString() + ",'" + tdDao.TrnMatch + "','" + tdDao.TrnChequeNo + "','" + tdDao.TrnAllocateFlag + "','" + tdDao.TrnRevalFlag + "',convert(datetime,'" +
                                                        tdDao.TrnPaymentDATE + "',103),'" + DataValidator.InvalidCharecterHandler(tdDao.TrnAcDesc) + "','" + tdDao.TrnAcType + "','" + tdDao.TrnBusFlag + "',convert(datetime,'" + tdDao.TrnDueDATE + "',103),'" + tdDao.TrnSecNo + "','" +
                                                        tdDao.TrnAdrCode + "','" + tdDao.TrnDcNo + "','" + tdDao.TrnGRNNo + "','" + tdDao.TrnBankRecoFlag + "','" + dr["Trn_Sub_No"].ToString() + "','" + tdDao.TrnTotInt + "','" +
                                                        tdDao.TrnAssetYN + "','" + tdDao.InstrumentType + "',Convert(datetime,'" + ChequeDate + "',103),'" + thDao.VoucherType + "','','','','')";
                                myCommand.ExecuteNonQuery();

                                myCommand.CommandText = @"INSERT INTO [AccTransactionAnalysis]([Trn_Ref_No],[Trn_Ac_Code],[Trn_Line_No],[Trn_Sub_No],
                                                    [Trn_AnaGroupDefinationCode1],[Trn_AnaGroupDefinationCode2],[Trn_AnaGroupDefinationCode3],
                                                    [Trn_AnaGroupDefinationCode4],[Trn_AnaGroupDefinationCode5],[Trn_AnaGroupLabelCode1],
                                                    [Trn_AnaGroupLabelCode2],[Trn_AnaGroupLabelCode3],[Trn_AnaGroupLabelCode4],
                                                    [Trn_AnaGroupLabelCode5]) VALUES('" +
                                                        thDao.TrnRefNo + "','" + tdDao.TrnAcCode + "'," + tdDao.TrnLineNo + "," + dr["Trn_Sub_No"].ToString() + ",'" +
                                                        dr["Trn_AnaGroupDefinationCode1"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode2"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode3"].ToString() + "','" +
                                                        dr["Trn_AnaGroupDefinationCode4"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode5"].ToString() + "','" + dr["Trn_AnaGroupLabelCode1"].ToString() + "','" +
                                                        dr["Trn_AnaGroupLabelCode2"].ToString() + "','" + dr["Trn_AnaGroupLabelCode3"].ToString() + "','" + dr["Trn_AnaGroupLabelCode4"].ToString() + "','" +
                                                        dr["Trn_AnaGroupLabelCode5"].ToString() + "')";
                                myCommand.ExecuteNonQuery();
                            }
                            else
                            {
                                myCommand.CommandText = @"UPDATE [AccTransactionDetails]
                                                       SET [Trn_Narration] = '" + DataValidator.InvalidCharecterHandler(tdDao.TrnNarration) + "'" +
                                                          ",[Trn_Trn_type] = '" + tdDao.TrnTrntype + "'" +
                                                          ",[Trn_Amount] = " + dr["Trn_Amount"].ToString() +
                                                          ",[Trn_Match] = '" + tdDao.TrnMatch + "'" +
                                                          ",[Trn_Cheque_No] = '" + tdDao.TrnChequeNo + "'" +
                                                          ",[Trn_Allocate_Flag] = '" + tdDao.TrnAllocateFlag + "'" +
                                                          ",[Trn_Reval_Flag] = '" + tdDao.TrnRevalFlag + "'" +
                                                          ",[Trn_Payment_DATE] = Convert(datetime,'" + tdDao.TrnPaymentDATE + "',103)" +
                                                          ",[Trn_Due_DATE] = Convert(datetime,'" + tdDao.TrnDueDATE + "',103)" +
                                                          ",[Trn_Sec_No] = '" + tdDao.TrnSecNo + "'" +
                                                          ",[Trn_Adr_Code] = '" + tdDao.TrnAdrCode + "'" +
                                                          ",[Trn_Dc_No] = '" + tdDao.TrnDcNo + "'" +
                                                          ",[Trn_GRN_No] = '" + tdDao.TrnGRNNo + "'" +
                                                          ",[Trn_Bank_Reco_Flag] = '" + tdDao.TrnBankRecoFlag + "'" +
                                                          ",[Trn_Sub_No] = " + dr["Trn_Sub_No"].ToString() +
                                                          ",[Trn_Tot_Int] = " + tdDao.TrnTotInt +
                                                          ",[Trn_Asset_Y_N] = '" + tdDao.TrnAssetYN + "'" +
                                                          ",Trn_Instrument_Type='" + tdDao.InstrumentType + "',Trn_Cheque_Date=convert(datetime,'" + ChequeDate + "',103)" +
                                                          " WHERE Trn_Ref_No='" + thDao.TrnRefNo + "' and Trn_Ac_Code='" + tdDao.TrnAcCode + "' and Trn_Line_No=" + tdDao.TrnLineNo + " and Trn_Sub_No=" + dr["Trn_Sub_No"].ToString();
                                myCommand.ExecuteNonQuery();

                                myCommand.CommandText = @"UPDATE [AccTransactionAnalysis]
                                                            SET [Trn_AnaGroupDefinationCode1] = '" + dr["Trn_AnaGroupDefinationCode1"].ToString() + "'" +
                                                          ",[Trn_AnaGroupDefinationCode2] = '" + dr["Trn_AnaGroupDefinationCode2"].ToString() + "'" +
                                                          ",[Trn_AnaGroupDefinationCode3] = '" + dr["Trn_AnaGroupDefinationCode3"].ToString() + "'" +
                                                          ",[Trn_AnaGroupDefinationCode4] = '" + dr["Trn_AnaGroupDefinationCode4"].ToString() + "'" +
                                                          ",[Trn_AnaGroupDefinationCode5] = '" + dr["Trn_AnaGroupDefinationCode5"].ToString() + "'" +
                                                          ",[Trn_AnaGroupLabelCode1] = '" + dr["Trn_AnaGroupLabelCode1"].ToString() + "'" +
                                                          ",[Trn_AnaGroupLabelCode2] = '" + dr["Trn_AnaGroupLabelCode2"].ToString() + "'" +
                                                          ",[Trn_AnaGroupLabelCode3] = '" + dr["Trn_AnaGroupLabelCode3"].ToString() + "'" +
                                                          ",[Trn_AnaGroupLabelCode4] = '" + dr["Trn_AnaGroupLabelCode4"].ToString() + "'" +
                                                          ",[Trn_AnaGroupLabelCode5] = '" + dr["Trn_AnaGroupLabelCode5"].ToString() + "'" +
                                                          " WHERE Trn_Ref_No='" + thDao.TrnRefNo + "' and Trn_Ac_Code='" + tdDao.TrnAcCode + "' and Trn_Line_No=" + tdDao.TrnLineNo + " and Trn_Sub_No=" + dr["Trn_Sub_No"].ToString();
                                myCommand.ExecuteNonQuery();
                            }
                        }
                    }
                    else
                    {
                        myCommand.CommandText = "select * from AccTransactionDetails where Trn_Ref_No='" + thDao.TrnRefNo + "' and Trn_Ac_Code='" + tdDao.TrnAcCode + "' and Trn_Line_No=" + tdDao.TrnLineNo;
                        sqlDataAdapterObj = new SqlDataAdapter();
                        sqlDataAdapterObj.SelectCommand = myCommand;
                        DataTable dtUpdate = new DataTable();
                        sqlDataAdapterObj.Fill(dtUpdate);

                        if (dtUpdate.Rows.Count == 0)
                        {
                            myCommand.CommandText = @"INSERT INTO [AccTransactionDetails]([Trn_Ref_No],[Trn_Ac_Code],[Trn_Jrn_Code],[Trn_Line_No],[Trn_Narration],
                                                    [Trn_Trn_type],[Trn_Amount],[Trn_Match],[Trn_Cheque_No],[Trn_Allocate_Flag],[Trn_Reval_Flag],
                                                    [Trn_Payment_DATE],[Trn_Ac_Desc],[Trn_Ac_Type],[Trn_Bus_Flag],[Trn_Due_DATE],[Trn_Sec_No],
                                                    [Trn_Adr_Code],[Trn_Dc_No],[Trn_GRN_No],[Trn_Bank_Reco_Flag],[Trn_Sub_No],[Trn_Tot_Int],
                                                    [Trn_Asset_Y_N],Trn_Instrument_Type,Trn_Cheque_Date,[T_Fl],[T_In],T_C1,T_C2,Trn_Code) VALUES('" +
                                                        thDao.TrnRefNo + "','" + tdDao.TrnAcCode + "','" + thDao.TrnJrnCode + "','" + tdDao.TrnLineNo + "','" + DataValidator.InvalidCharecterHandler(tdDao.TrnNarration) + "','" +
                                                        tdDao.TrnTrntype + "'," + tdDao.TrnAmount + ",'" + tdDao.TrnMatch + "','" + tdDao.TrnChequeNo + "','" + tdDao.TrnAllocateFlag + "','" + tdDao.TrnRevalFlag + "',convert(datetime,'" +
                                                        tdDao.TrnPaymentDATE + "',103),'" + DataValidator.InvalidCharecterHandler(tdDao.TrnAcDesc) + "','" + tdDao.TrnAcType + "','" + tdDao.TrnBusFlag + "',convert(datetime,'" + tdDao.TrnDueDATE + "',103),'" + tdDao.TrnSecNo + "','" +
                                                        tdDao.TrnAdrCode + "','" + tdDao.TrnDcNo + "','" + tdDao.TrnGRNNo + "','" + tdDao.TrnBankRecoFlag + "','" + tdDao.TrnSubNo + "','" + tdDao.TrnTotInt + "','" +
                                                        tdDao.TrnAssetYN + "','" + tdDao.InstrumentType + "',Convert(datetime,'" + ChequeDate + "',103),'" + thDao.VoucherType + "','','','','')";
                            myCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            myCommand.CommandText = @"UPDATE [AccTransactionDetails]
                                                       SET [Trn_Narration] = '" + DataValidator.InvalidCharecterHandler(tdDao.TrnNarration) + "'" +
                                                         ",[Trn_Trn_type] = '" + tdDao.TrnTrntype + "'" +
                                                         ",[Trn_Amount] = " + tdDao.TrnAmount +
                                                         ",[Trn_Match] = '" + tdDao.TrnMatch + "'" +
                                                         ",[Trn_Cheque_No] = '" + tdDao.TrnChequeNo + "'" +
                                                         ",[Trn_Allocate_Flag] = '" + tdDao.TrnAllocateFlag + "'" +
                                                         ",[Trn_Reval_Flag] = '" + tdDao.TrnRevalFlag + "'" +
                                                         ",[Trn_Payment_DATE] = Convert(datetime,'" + tdDao.TrnPaymentDATE + "',103)" +
                                                         ",[Trn_Due_DATE] = Convert(datetime,'" + tdDao.TrnDueDATE + "',103)" +
                                                         ",[Trn_Sec_No] = '" + tdDao.TrnSecNo + "'" +
                                                         ",[Trn_Adr_Code] = '" + tdDao.TrnAdrCode + "'" +
                                                         ",[Trn_Dc_No] = '" + tdDao.TrnDcNo + "'" +
                                                         ",[Trn_GRN_No] = '" + tdDao.TrnGRNNo + "'" +
                                                         ",[Trn_Bank_Reco_Flag] = '" + tdDao.TrnBankRecoFlag + "'" +
                                                         ",[Trn_Sub_No] =0 " +
                                                         ",[Trn_Tot_Int] = " + tdDao.TrnTotInt +
                                                         ",[Trn_Asset_Y_N] = '" + tdDao.TrnAssetYN + "'" +
                                                          ",Trn_Instrument_Type='" + tdDao.InstrumentType + "',Trn_Cheque_Date=convert(datetime,'" + ChequeDate + "',103)" +
                                                         " WHERE Trn_Ref_No='" + thDao.TrnRefNo + "' and Trn_Ac_Code='" + tdDao.TrnAcCode + "' and Trn_Line_No=" + tdDao.TrnLineNo;
                            myCommand.ExecuteNonQuery();
                        }
                    }

                    if (tdDao.TrnChequeNo != "" && thDao.VoucherType == StringEnum.GetStringValue(VoucherType.ChequeVoucher) && tdDao.InstrumentType == Enum.GetName(typeof(InstrumentType), InstrumentType.Cheque))
                    {
                        if (!DataProcess.ExecuteQuery(myCommand, "UPDATE [AccBankCheuqeDetails] SET  [BNK_CHQ_BOOK_DATE] = Convert(datetime,'" + ChequeDate + "',103),[BNK_CHQ_Post_flag] = '' WHERE BNK_CHQ_NO='" + tdDao.TrnChequeNo + "'"))
                        {
                            myTrans.Rollback("SaveAccData"); return retValue;
                        }
                    }

                    if (Convert.ToString(tdDao.TrnChequeNo) != "" && thDao.VoucherType == StringEnum.GetStringValue(VoucherType.MoneyReceipt) && tdDao.TrnTrntype == "C")
                    {
                        myCommand.CommandText = @" delete from AccTransactionReceivableBankInfo where Trn_Ref_No='" + thDao.TrnRefNo + "' and [Trn_Cheque_No]=" + tdDao.TrnChequeNo;

                        myCommand.ExecuteNonQuery();

                        myCommand.CommandText = @"INSERT INTO [AccTransactionReceivableBankInfo] ([Trn_Ref_No],[Trn_Jrn_Code],[Trn_Line_No],
                                                [Trn_Bank_Name],[Trn_Branch_Name],[Trn_Account_No],
                                                [Trn_Cheque_No],[Trn_Cheque_DATE]) VALUES ('" +
                                                thDao.TrnRefNo + "'," + thDao.TrnJrnCode + "," + tdDao.TrnLineNo + ",'" +
                                                tdDao.RtrnBankName + "','" + tdDao.RtrnBranchName + "','" + tdDao.RtrnAccountNo + "','" +
                                                tdDao.TrnChequeNo + "',convert(datetime,'" + ChequeDate + "',103))";
                        myCommand.ExecuteNonQuery();
                    }

                }

                string updaterefno = thDao.TrnRefNo.ToString().Substring(thDao.TrnRefNo.ToString().Length - 6);
                // Update referance number setup table

                if (!DataProcess.ExecuteQuery(myCommand, "update AccJournalSetup set JrnNextRefNo='" + updaterefno + "' where jrntype='" + thDao.TrnJrnType + "' and VoucherType='" + thDao.VoucherType + "' and JrnModule='" + thDao.ModuleName + "'"))
                {
                    myTrans.Rollback("SaveAccData"); return retValue;
                }

                //Payableprogress jurnal
                DateTime adjdtp = DateProcess.LastDateOfMonth(thDao.TrnDATE);

                myCommand.CommandText = @"update AccPaybleProgress set Status='N',InvoiceReceived='Y',Jrnupdpermission='N',InvReceivedDate=convert(datetime,'" + thDao.TrnDATE + "',103),InvReceivedBy='" + thDao.TrnEntryUser + "',UpdateUserid='" + thDao.TrnEntryUser + "',UpdateDate=convert(datetime,'" + thDao.TrnEntryDATE + "',103),VATAcc='" + vatacc.ToString() + "',TaxAcc='" + taxacc + "',InvoiceNumber='" + invno.ToString() + "' where ReferenceNumber='" + grnno.ToString() + "'";
                myCommand.ExecuteNonQuery(); 

//                if (!updateFlg)
//                {
//                    myCommand.CommandText = @"INSERT INTO [AccPaybleProgress] ([ReferenceNumber],[Status],[InvoiceReceived],[AdjustmentPeriod],
//                                                [EntryUserID],[EntryDate],[UpdateUserid],
//                                                [UpdateDate],[Jrnupdpermission]) VALUES ('" +
//                                                grnno.ToString() + "','Y','N',convert(datetime,'" + adjdtp + "',103),'" + thDao.TrnEntryUser + "',convert(datetime,'" + thDao.TrnEntryDATE + "',103),'" + thDao.TrnEntryUser + "',convert(datetime,'" + thDao.TrnEntryDATE + "',103),'N')";
//                    myCommand.ExecuteNonQuery();

//                }
//                else
//                {
//                    myCommand.CommandText = @"update AccPaybleProgress set InvoiceReceived='Y',Jrnupdpermission='N',InvReceivedDate=convert(datetime,'" + thDao.TrnDATE + "',103),InvReceivedBy='" + thDao.TrnEntryUser + "',UpdateUserid='" + thDao.TrnEntryUser + "',UpdateDate=convert(datetime,'" + thDao.TrnEntryDATE + "',103),VATAcc='" + vatacc.ToString() + "',TaxAcc='" + taxacc + "',InvoiceNumber='" + invno.ToString() + "' where ReferenceNumber='" + grnno.ToString() + "'";
//                    myCommand.ExecuteNonQuery(); 
//                }

                myTrans.Commit();

                return thDao.TrnRefNo;
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback("SaveAccData");
                    System.Windows.Forms.MessageBox.Show(e.Message, StringProcess.messageHead);
                    return retValue;
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return retValue;
                    }
                    System.Windows.Forms.MessageBox.Show(ex.Message, StringProcess.messageHead);
                }

                return retValue;
            }
            finally
            {
                myConnection.Close();
            }


        }        

        private string GetReferenceNoForSave(SqlCommand myCommand, string JrnType, string VoucherType, string ModuleName, string accPeriod, DateTime TransactionDate)
        {

            string ReferenceNo = "", PreFix = "", NextNum = "", month = "", day = "", refType = "", startNum = "";
            SqlDataAdapter sqlDataAdapterObj = null, sqlData1, sqlData, sqlData2, sqlData3;
            DataTable dataTableObj, dt, dt1, dt2, dt3;
            int ResetMonth = 0;

            try
            {
                myCommand.CommandText = "select * from AccJournalSetup where jrntype='" + JrnType + "' and VoucherType='" + VoucherType + "' and JrnModule='" + ModuleName + "'";
                sqlDataAdapterObj = new SqlDataAdapter();
                sqlDataAdapterObj.SelectCommand = myCommand;
                dataTableObj = new DataTable();
                sqlDataAdapterObj.Fill(dataTableObj);
                if (dataTableObj.Rows.Count > 0)
                {
                    PreFix = dataTableObj.Rows[0]["JrnPrefix"].ToString();
                    NextNum = dataTableObj.Rows[0]["JrnNextRefNo"].ToString();
                    startNum = dataTableObj.Rows[0]["JrnStartRefNo"].ToString();
                    month = StringProcess.Left(DateProcess.MonthName(TransactionDate.Month - 1), 3).ToUpper();
                    day = StringProcess.addZeroInString(TransactionDate.Day.ToString(), 2, true);
                    refType = dataTableObj.Rows[0]["JrnRefNoType"].ToString();
                    ResetMonth = Convert.ToInt32(dataTableObj.Rows[0]["JrnRefNoResetPeriod"].ToString());

                    if (refType == "P")
                    {
                        // First check hold mode
                        myCommand.CommandText = "select MAX([Trn_DATE]) as maxref  from AccTransactionHeaderHold where Trn_Jrn_Type='" + JrnType + "' and MONTH(Trn_DATE)=" + TransactionDate.Month + " and YEAR(Trn_DATE)=" + TransactionDate.Year;
                        sqlData = new SqlDataAdapter();
                        sqlDataAdapterObj.SelectCommand = myCommand;
                        dt = new DataTable();
                        sqlDataAdapterObj.Fill(dt);

                        if (Convert.ToString(dt.Rows[0]["maxref"]) == "")
                        {

                            // then check post mode
                            myCommand.CommandText = "select MAX([Trn_DATE]) as maxref  from AccTransactionHeader where Trn_Jrn_Type='" + JrnType + "' and MONTH(Trn_DATE)=" + TransactionDate.Month + " and YEAR(Trn_DATE)=" + TransactionDate.Year;
                            sqlData1 = new SqlDataAdapter();
                            sqlDataAdapterObj.SelectCommand = myCommand;
                            dt1 = new DataTable();
                            sqlDataAdapterObj.Fill(dt1);

                            if (Convert.ToString(dt1.Rows[0]["maxref"]) == "")
                            {
                                ReferenceNo = "S" + PreFix + StringProcess.Right(TransactionDate.Year.ToString(), 2) + month + "000001";
                            }
                            else
                            {
                                ReferenceNo = "S" + PreFix + StringProcess.Right(TransactionDate.Year.ToString(), 2) + month + StringProcess.addZeroInString(Convert.ToString(Convert.ToDouble(NextNum) + 1), 6, true);
                            }


                        }
                        else
                        {
                            ReferenceNo = "S" + PreFix + StringProcess.Right(TransactionDate.Year.ToString(), 2) + month + StringProcess.addZeroInString(Convert.ToString(Convert.ToDouble(NextNum) + 1), 6, true);
                        }

                    }
                    else   // Continious 
                    {
                        if (TransactionDate.Date.Month == ResetMonth) //reset referance number from this month 
                        {
                            myCommand.CommandText = "select MAX([Trn_Ref_No]) as maxref  from AccTransactionHeader where Trn_Jrn_Type='" + JrnType + "' and MONTH(Trn_DATE)=" + TransactionDate.Month + " and YEAR(Trn_DATE)=" + TransactionDate.Year;
                            sqlData2 = new SqlDataAdapter();
                            sqlDataAdapterObj.SelectCommand = myCommand;
                            dt2 = new DataTable();
                            sqlDataAdapterObj.Fill(dt2);
                            if (Convert.ToString(dt2.Rows[0]["maxref"]) != "")
                            {
                                ReferenceNo = "S" + PreFix + StringProcess.Right(TransactionDate.Year.ToString(), 2) + month + StringProcess.addZeroInString(Convert.ToString(Convert.ToDouble(NextNum) + 1), 6, true);
                            }
                            else
                            {
                                myCommand.CommandText = "select MAX([Trn_Ref_No]) as maxref  from AccTransactionHeaderhold where Trn_Jrn_Type='" + JrnType + "' and MONTH(Trn_DATE)=" + TransactionDate.Month + " and YEAR(Trn_DATE)=" + TransactionDate.Year;
                                sqlData3 = new SqlDataAdapter();
                                sqlDataAdapterObj.SelectCommand = myCommand;
                                dt3 = new DataTable();
                                sqlDataAdapterObj.Fill(dt3);
                                if (Convert.ToString(dt2.Rows[0]["maxref"]) != "")
                                {
                                    ReferenceNo = "S" + PreFix + StringProcess.Right(TransactionDate.Year.ToString(), 2) + month + StringProcess.addZeroInString(Convert.ToString(Convert.ToDouble(NextNum) + 1), 6, true);
                                }
                                else
                                {
                                    ReferenceNo = "S" + PreFix + StringProcess.Right(TransactionDate.Year.ToString(), 2) + month + "000001";
                                }
                            }
                        }
                        else if (TransactionDate.Date.Month != ResetMonth)
                        {
                            ReferenceNo = "S" + PreFix + StringProcess.Right(TransactionDate.Year.ToString(), 2) + month + StringProcess.addZeroInString(Convert.ToString(Convert.ToDouble(NextNum) + 1), 6, true);
                        }

                    }


                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, StringProcess.messageHead);
            }
            return ReferenceNo;
        }
    
        private bool DeleteSaveData(SqlCommand myCommand, string RefNo,string userid)
        {
            try
            {
                myCommand.CommandText = "Delete  FROM [AccTransactionHeaderHold] where trn_ref_no='" + RefNo + "'";
                myCommand.ExecuteNonQuery();

                myCommand.CommandText = "Delete  FROM [AccTransactionDetailsHold] where trn_ref_no='" + RefNo + "'";
                myCommand.ExecuteNonQuery();

                myCommand.CommandText = "Delete FROM [AccTransactionAnalysisHold] where trn_ref_no='" + RefNo + "'";
                myCommand.ExecuteNonQuery();

                myCommand.CommandText = "Delete from AccTransactionSplit_" + userid + " where Trn_Ref_No='" + RefNo + "'";
                myCommand.ExecuteNonQuery();

                myCommand.CommandText = "Delete from AccTransactionReceivableBankInfoHold where Trn_Ref_No='" + RefNo + "'";
                myCommand.ExecuteNonQuery();
                return true;
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message,StringProcess.messageHead);
                return false;
            }
        }

        public bool ReviseTransaction(string ConnectionStr, List<TransactionDetailsDAO> tdDaolst,string[] Str)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionStr);
            myConnection.Open();

            SqlCommand myCommand = myConnection.CreateCommand();
            SqlTransaction myTrans;
           
            myTrans = myConnection.BeginTransaction("ReviseAccData");

            myCommand.Connection = myConnection;
            myCommand.Transaction = myTrans;

            try
            {
                if (!DataProcess.ExecuteQuery(myCommand, "insert into AccTransactionDetailsAmd select *,CONVERT(datetime,'" + DateProcess.GetServerDate(ConnectionStr) + "',103),'" + Str[3] + "' from AccTransactionDetails where Trn_Ref_No='" + Str[0] + "' and Trn_Ac_Code='" + Str[1] + "' and Trn_Line_No=" + Str[2]))
                {
                    myTrans.Rollback("ReviseAccData"); return false;
                }

                if (!DataProcess.ExecuteQuery(myCommand, "insert into AccTransactionAnalysisAmd select *," + Str[4] + " from AccTransactionAnalysis where Trn_Ref_No='" + Str[0] + "' and Trn_Ac_Code='" + Str[1] + "' and Trn_Line_No=" + Str[2]))
                {
                    myTrans.Rollback("ReviseAccData"); return false;
                }

                if (!DataProcess.ExecuteQuery(myCommand, "Delete from AccTransactionDetails where Trn_Ref_No='" + Str[0] + "' and Trn_Ac_Code='" + Str[1] + "' and Trn_Line_No=" + Str[2]))
                {
                    myTrans.Rollback("ReviseAccData"); return false;
                }
                if (!DataProcess.ExecuteQuery(myCommand, "Delete from AccTransactionAnalysis where Trn_Ref_No='" + Str[0] + "' and Trn_Ac_Code='" + Str[1] + "' and Trn_Line_No=" + Str[2]))
                {
                    myTrans.Rollback("ReviseAccData"); return false;
                }
                if (!DataProcess.ExecuteQuery(myCommand, "update AccTransactionHeader set Trn_Revise_Flag='Y' where Trn_Ref_No='" + Str[0] + "'"))
                {
                    myTrans.Rollback("ReviseAccData"); return false;
                }
                foreach (TransactionDetailsDAO tdDao in tdDaolst)
                {
                    string ChequeDate = null;
                    if (tdDao.TrnChequeNo != "") ChequeDate = tdDao.TrnChequeDate.Date.ToString();
                    myCommand.CommandText = @"INSERT INTO [AccTransactionDetails]([Trn_Ref_No],[Trn_Ac_Code],[Trn_Jrn_Code],[Trn_Line_No],[Trn_Narration],
                                                    [Trn_Trn_type],[Trn_Amount],[Trn_Match],[Trn_Cheque_No],[Trn_Allocate_Flag],[Trn_Reval_Flag],
                                                    [Trn_Payment_DATE],[Trn_Ac_Desc],[Trn_Ac_Type],[Trn_Bus_Flag],[Trn_Due_DATE],[Trn_Sec_No],
                                                    [Trn_Adr_Code],[Trn_Dc_No],[Trn_GRN_No],[Trn_Bank_Reco_Flag],[Trn_Sub_No],[Trn_Tot_Int],
                                                    [Trn_Asset_Y_N],Trn_Instrument_Type,Trn_Cheque_Date,[T_Fl],[T_In],T_C1,T_C2,Trn_Code) VALUES('" +
                                            tdDao.TrnRefNo + "','" + tdDao.TrnAcCode + "','" + tdDao.TrnJrnCode + "','" + tdDao.TrnLineNo + "','" + DataValidator.InvalidCharecterHandler(tdDao.TrnNarration) + "','" +
                                            tdDao.TrnTrntype + "'," + tdDao.TrnAmount + ",'" + tdDao.TrnMatch + "','" + tdDao.TrnChequeNo + "','" + tdDao.TrnAllocateFlag + "','" + tdDao.TrnRevalFlag + "',convert(datetime,'" +
                                            tdDao.TrnPaymentDATE + "',103),'" + tdDao.TrnAcDesc + "','" + tdDao.TrnAcType + "','" + tdDao.TrnBusFlag + "',convert(datetime,'" + tdDao.TrnDueDATE + "',103),'" + tdDao.TrnSecNo + "','" +
                                            tdDao.TrnAdrCode + "','" + tdDao.TrnDcNo + "','" + tdDao.TrnGRNNo + "','" + tdDao.TrnBankRecoFlag + "','" + tdDao.TrnSubNo + "','" + tdDao.TrnTotInt + "','" +
                                            tdDao.TrnAssetYN + "','" + tdDao.InstrumentType + "',Convert(datetime,'" + ChequeDate + "',103),'','','','','')";
                    myCommand.ExecuteNonQuery();

                    myCommand.CommandText = @"INSERT INTO [AccTransactionAnalysis]([Trn_Ref_No],[Trn_Ac_Code],[Trn_Line_No],[Trn_Sub_No],
                                                    [Trn_AnaGroupDefinationCode1],[Trn_AnaGroupDefinationCode2],[Trn_AnaGroupDefinationCode3],
                                                    [Trn_AnaGroupDefinationCode4],[Trn_AnaGroupDefinationCode5],[Trn_AnaGroupLabelCode1],
                                                    [Trn_AnaGroupLabelCode2],[Trn_AnaGroupLabelCode3],[Trn_AnaGroupLabelCode4],
                                                    [Trn_AnaGroupLabelCode5]) VALUES('" +
                                            tdDao.TrnRefNo + "','" + tdDao.TrnAcCode + "'," + tdDao.TrnLineNo + "," + tdDao.TrnSubNo + ",'" +
                                            tdDao.TrnAnaGroupDefinationCode1 + "','" + tdDao.TrnAnaGroupDefinationCode2 + "','" + tdDao.TrnAnaGroupDefinationCode3 + "','" +
                                            tdDao.TrnAnaGroupDefinationCode4 + "','" + tdDao.TrnAnaGroupDefinationCode5 + "','" + tdDao.TrnAnaGroupLabelCode1 + "','" +
                                            tdDao.TrnAnaGroupLabelCode2 + "','" + tdDao.TrnAnaGroupLabelCode3 + "','" + tdDao.TrnAnaGroupLabelCode4 + "','" +
                                            tdDao.TrnAnaGroupLabelCode5 + "')";
                    myCommand.ExecuteNonQuery();


                }

                myTrans.Commit();
                return true;
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback("ReviseAccData");
                    System.Windows.Forms.MessageBox.Show(e.Message, StringProcess.messageHead);
                    return false;
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return false;
                    }
                    System.Windows.Forms.MessageBox.Show(ex.Message, StringProcess.messageHead);
                }

                return false;
            }
            finally
            {
                myConnection.Close();
            }
        }

        public string[] ReverseTransaction(string ConnectionStr, string RefNo, string TrnAccPeriod, DateTime TrnDATE, string TrnEntryUser)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionStr);
            myConnection.Open();

            SqlCommand myCommand = myConnection.CreateCommand();
            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string[] retValue = new string[2] { "", "" }; string OldRef = "", userid = "";
            string TrnJrnCode,TrnRefNo;
            myTrans = myConnection.BeginTransaction("ReverseAccData");

            myCommand.Connection = myConnection;
            myCommand.Transaction = myTrans;

            try
            {
                DataTable TrnHeader = DataProcess.GetData(myCommand, "Select * from AccTransactionHeader where Trn_Ref_No='" + RefNo + "'");
                if (TrnHeader.Rows.Count == 0)
                {
                    System.Windows.Forms.MessageBox.Show("No Records Found to Reverse in Transaction Header", StringProcess.messageHead); return retValue;
                }
                myCommand.CommandText = "select (ISNULL(MAX(trn_jrn_code),0)+1) as maxJrnCode from AccTransactionHeader";
                sqlDataAdapterObj = new SqlDataAdapter();
                sqlDataAdapterObj.SelectCommand = myCommand;
                DataTable dtmaxJrnCode = new DataTable();
                sqlDataAdapterObj.Fill(dtmaxJrnCode);
                if (dtmaxJrnCode.Rows.Count > 0)
                   TrnJrnCode = Convert.ToDouble(dtmaxJrnCode.Rows[0]["maxJrnCode"].ToString()).ToString();
                else
                {
                    myTrans.Rollback("ReverseAccData"); return retValue;
                }

                TrnRefNo = GetReferenceNo(myCommand, TrnHeader.Rows[0]["Trn_Jrn_Type"].ToString(), TrnHeader.Rows[0]["Trn_Voucher_Type"].ToString(),StringEnum.GetStringValue( ModuleName.Accounts), TrnAccPeriod, TrnDATE);
                if (TrnRefNo == "")
                {
                    myTrans.Rollback("ReverseAccData"); return retValue;
                }

                myCommand.CommandText = "select * from AccTransactionHeader where Trn_Ref_No='" + TrnRefNo + "'";
                sqlDataAdapterObj = new SqlDataAdapter();
                sqlDataAdapterObj.SelectCommand = myCommand;
                DataTable dtmaxRefCheck = new DataTable();
                sqlDataAdapterObj.Fill(dtmaxRefCheck);

                if (dtmaxRefCheck.Rows.Count > 0)
                {
                    myTrans.Rollback("ReverseAccData");
                    System.Windows.Forms.MessageBox.Show("Refference No already Exits\rPlease Try Again Later", StringProcess.messageHead);
                    return retValue;
                }

                if (!DataProcess.ExecuteQuery(myCommand, "update AccTransactionHeader set Trn_Reverse_Flag='Y',ReverseRef='"+TrnRefNo+"' where Trn_Ref_No='" + RefNo + "'"))
                {
                    myTrans.Rollback("ReviseAccData"); return retValue;
                }
                
                myCommand.CommandText = @"INSERT INTO [AccTransactionHeader]([Trn_Ref_No],[Trn_Jrn_Code],[Trn_Jrn_Type],[Trn_Voucher_Type],[Trn_Acc_Period],
                                        [Trn_Entry_DATE],[Trn_DATE],[Trn_Curr_Code],[Trn_Curr_Rate],[Trn_Entry_User],[Trn_LastUpdate_User],
                                        [Trn_LastUpdate_DATE],[Trn_Entry_Flag],[Trn_Revise_Flag],[Trn_Reverse_Flag]) VALUES('" +
                                        TrnRefNo + "'," + TrnJrnCode + ",'" + TrnHeader.Rows[0]["Trn_Jrn_Type"].ToString() + "','" + TrnHeader.Rows[0]["Trn_Voucher_Type"].ToString() + "','" + TrnAccPeriod + "',convert(datetime,'" +
                                        DateProcess.GetServerDate(ConnectionStr) + "',103),convert(datetime,'" + TrnDATE + "',103),'" + TrnHeader.Rows[0]["Trn_Curr_Code"].ToString() + "','" + TrnHeader.Rows[0]["Trn_Curr_Rate"].ToString() + "','" + TrnEntryUser + "','" + TrnEntryUser + "',convert(datetime,'" +
                                        DateProcess.GetServerDate(ConnectionStr) + "',103),'" + TrnHeader.Rows[0]["Trn_Entry_Flag"].ToString() + "','" + TrnHeader.Rows[0]["Trn_Revise_Flag"].ToString() + "','')";
                myCommand.ExecuteNonQuery();
                DataTable trnDetails = DataProcess.GetData(myCommand, "select * from AccTransactionDetails where Trn_Ref_No='" + RefNo + "'");
                foreach (DataRow dr in trnDetails.Rows)
                {
                    string TrnTrnType="D";
                    if(dr["Trn_Trn_type"].ToString()=="D")
                        TrnTrnType="C";
                    myCommand.CommandText = @"INSERT INTO [AccTransactionDetails]([Trn_Ref_No],[Trn_Ac_Code],[Trn_Jrn_Code],[Trn_Line_No],[Trn_Narration],
                                                    [Trn_Trn_type],[Trn_Amount],[Trn_Match],[Trn_Cheque_No],[Trn_Allocate_Flag],[Trn_Reval_Flag],
                                                    [Trn_Payment_DATE],[Trn_Ac_Desc],[Trn_Ac_Type],[Trn_Bus_Flag],[Trn_Due_DATE],[Trn_Sec_No],
                                                    [Trn_Adr_Code],[Trn_Dc_No],[Trn_GRN_No],[Trn_Bank_Reco_Flag],[Trn_Sub_No],[Trn_Tot_Int],
                                                    [Trn_Asset_Y_N],Trn_Instrument_Type,Trn_Cheque_Date,[T_Fl],[T_In],T_C2,T_C1,Trn_Code) VALUES('" +
                                                      TrnRefNo + "','" + dr["Trn_Ac_Code"].ToString() + "','" + TrnJrnCode + "','" + dr["Trn_Line_No"].ToString() + "','" + dr["Trn_Narration"].ToString() + "','" +
                                                      TrnTrnType + "'," + dr["Trn_Amount"].ToString() + ",'" + dr["Trn_Match"].ToString() + "','" + dr["Trn_Cheque_No"].ToString() + "','" + dr["Trn_Allocate_Flag"].ToString() + "','" + dr["Trn_Reval_Flag"].ToString() + "',convert(datetime,'" +
                                                      dr["Trn_Payment_DATE"].ToString() + "',103),'" + dr["Trn_Ac_Desc"].ToString() + "','" + dr["Trn_Ac_Type"].ToString() + "','" + dr["Trn_Bus_Flag"].ToString() + "',convert(datetime,'" + dr["Trn_Due_DATE"].ToString() + "',103),'" + dr["Trn_Sec_No"].ToString() + "','" +
                                                      dr["Trn_Adr_Code"].ToString() + "','" + dr["Trn_Dc_No"].ToString() + "','" + dr["Trn_GRN_No"].ToString() + "','" + dr["Trn_Bank_Reco_Flag"].ToString() + "','" + dr["Trn_Sub_No"].ToString() + "','" + dr["Trn_Tot_Int"].ToString() + "','" +
                                                      dr["Trn_Asset_Y_N"].ToString() + "','" + dr["Trn_Instrument_Type"].ToString() + "',Convert(datetime,'" + dr["Trn_Cheque_Date"].ToString() + "',103),'','','" + RefNo + "','" + dr["T_C1"].ToString() + "','" + dr["Trn_Code"].ToString() + "')";
                            myCommand.ExecuteNonQuery();
                            if (Convert.ToString(dr["Trn_Cheque_No"]) != "" && TrnHeader.Rows[0]["Trn_Voucher_Type"].ToString() == StringEnum.GetStringValue(VoucherType.ChequeVoucher) && Convert.ToString(dr["Trn_Instrument_Type"]) == Enum.GetName(typeof(InstrumentType), InstrumentType.Cheque))
                            {
                                if (!DataProcess.ExecuteQuery(myCommand, "UPDATE [AccBankCheuqeDetails] SET [BNK_CHQ_Post_flag] = 'N' WHERE BNK_CHQ_NO='" + Convert.ToString(dr["Trn_Cheque_No"]) + "'"))
                                {
                                    myTrans.Rollback("ReverseAccData"); return retValue;
                                }
                            }

   
                }

                DataTable ana=DataProcess.GetData(myCommand,"select * from AccTransactionAnalysis where Trn_Ref_No='"+RefNo+"'");
                    
                foreach(DataRow dr in ana.Rows)
                {
                           
                            myCommand.CommandText = @"INSERT INTO [AccTransactionAnalysis]([Trn_Ref_No],[Trn_Ac_Code],[Trn_Line_No],[Trn_Sub_No],
                                                    [Trn_AnaGroupDefinationCode1],[Trn_AnaGroupDefinationCode2],[Trn_AnaGroupDefinationCode3],
                                                    [Trn_AnaGroupDefinationCode4],[Trn_AnaGroupDefinationCode5],[Trn_AnaGroupLabelCode1],
                                                    [Trn_AnaGroupLabelCode2],[Trn_AnaGroupLabelCode3],[Trn_AnaGroupLabelCode4],
                                                    [Trn_AnaGroupLabelCode5]) VALUES('" +
                                                    TrnRefNo + "','" + dr["Trn_Ac_Code"].ToString() + "'," + dr["Trn_Line_No"].ToString() + "," + dr["Trn_Sub_No"].ToString() + ",'" +
                                                    dr["Trn_AnaGroupDefinationCode1"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode2"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode3"].ToString() + "','" +
                                                    dr["Trn_AnaGroupDefinationCode4"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode5"].ToString() + "','" + dr["Trn_AnaGroupLabelCode1"].ToString() + "','" +
                                                    dr["Trn_AnaGroupLabelCode2"].ToString() + "','" + dr["Trn_AnaGroupLabelCode3"].ToString() + "','" + dr["Trn_AnaGroupLabelCode4"].ToString() + "','" +
                                                    dr["Trn_AnaGroupLabelCode5"].ToString() + "')";
                            myCommand.ExecuteNonQuery();
                }
                           

                  if (!DataProcess.ExecuteQuery(myCommand,"update AccTransactionDetails set trn_bus_flag='R' where trn_ref_no='" + RefNo+"'") )
                  {
                       myTrans.Rollback("ReverseAccData"); return retValue;
                  }
                  if (!DataProcess.ExecuteQuery(myCommand, "update AccTransactionDetails set trn_dc_no='' where trn_ref_no='" + RefNo + "' and trn_dc_no='Chq. Realised'"))
                  {
                      myTrans.Rollback("ReverseAccData"); return retValue;
                  }
                  if (!DataProcess.ExecuteQuery(myCommand, "update AccTransactionDetails set trn_reval_flag='' where trn_ref_no='" + RefNo + "' and trn_reval_flag='R'"))
                  {
                      myTrans.Rollback("ReverseAccData"); return retValue;
                  }

//                  if (tdDao.TrnChequeNo != "" && thDao.VoucherType == StringEnum.GetStringValue(VoucherType.MoneyReceipt) && tdDao.TrnTrntype == "C")
//                  {
//                      myCommand.CommandText = @"INSERT INTO [AccTransactionReceivableBankInfo] ([Trn_Ref_No],[Trn_Jrn_Code],[Trn_Line_No],
//                                                [Trn_Bank_Name],[Trn_Branch_Name],[Trn_Account_No],
//                                                [Trn_Cheque_No],[Trn_Cheque_DATE]) VALUES ('" +
//                                              thDao.TrnRefNo + "'," + thDao.TrnJrnCode + "," + tdDao.TrnLineNo + ",'" +
//                                              tdDao.RtrnBankName + "','" + tdDao.RtrnBranchName + "','" + tdDao.RtrnAccountNo + "','" +
//                                              tdDao.TrnChequeNo + "',convert(datetime,'" + ChequeDate + "',103))";
//                      myCommand.ExecuteNonQuery();
//                  }

//                }

                myTrans.Commit();
                retValue[0] = TrnRefNo;
                retValue[1] = TrnJrnCode.ToString();
                return retValue;
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback("ReverseAccData");
                    System.Windows.Forms.MessageBox.Show(e.Message, StringProcess.messageHead);
                    return retValue;
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return retValue;
                    }
                    System.Windows.Forms.MessageBox.Show(ex.Message, StringProcess.messageHead);
                }

                return retValue;
            }
            finally
            {
                myConnection.Close();
            }

            return retValue;
        }

        public string[] ReverseTransactionForChequeBounch(string ConnectionStr, string RefNo, string TrnAccPeriod, DateTime TrnDATE, string TrnEntryUser,string ChkBounch)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionStr);
            myConnection.Open();

            SqlCommand myCommand = myConnection.CreateCommand();
            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string[] retValue = new string[2] { "", "" }; string OldRef = "", userid = "";
            string TrnJrnCode, TrnRefNo;
            myTrans = myConnection.BeginTransaction("ReverseAccData");

            myCommand.Connection = myConnection;
            myCommand.Transaction = myTrans;

            try
            {
                DataTable TrnHeader = DataProcess.GetData(myCommand, "Select * from AccTransactionHeader where Trn_Ref_No='" + RefNo + "'");
                if (TrnHeader.Rows.Count == 0)
                {
                    System.Windows.Forms.MessageBox.Show("No Records Found to Reverse in Transaction Header", StringProcess.messageHead); return retValue;
                }
                myCommand.CommandText = "select (ISNULL(MAX(trn_jrn_code),0)+1) as maxJrnCode from AccTransactionHeader";
                sqlDataAdapterObj = new SqlDataAdapter();
                sqlDataAdapterObj.SelectCommand = myCommand;
                DataTable dtmaxJrnCode = new DataTable();
                sqlDataAdapterObj.Fill(dtmaxJrnCode);
                if (dtmaxJrnCode.Rows.Count > 0)
                    TrnJrnCode = Convert.ToDouble(dtmaxJrnCode.Rows[0]["maxJrnCode"].ToString()).ToString();
                else
                {
                    myTrans.Rollback("ReverseAccData"); return retValue;
                }

                TrnRefNo = GetReferenceNo(myCommand, TrnHeader.Rows[0]["Trn_Jrn_Type"].ToString(), TrnHeader.Rows[0]["Trn_Voucher_Type"].ToString(), StringEnum.GetStringValue(ModuleName.Accounts), TrnAccPeriod, TrnDATE);
                if (TrnRefNo == "")
                {
                    myTrans.Rollback("ReverseAccData"); return retValue;
                }

                myCommand.CommandText = "select * from AccTransactionHeader where Trn_Ref_No='" + TrnRefNo + "'";
                sqlDataAdapterObj = new SqlDataAdapter();
                sqlDataAdapterObj.SelectCommand = myCommand;
                DataTable dtmaxRefCheck = new DataTable();
                sqlDataAdapterObj.Fill(dtmaxRefCheck);

                if (dtmaxRefCheck.Rows.Count > 0)
                {
                    myTrans.Rollback("ReverseAccData");
                    System.Windows.Forms.MessageBox.Show("Refference No already Exits\rPlease Try Again Later", StringProcess.messageHead);
                    return retValue;
                }

                if (!DataProcess.ExecuteQuery(myCommand, "update AccTransactionHeader set Trn_Reverse_Flag='Y',ReverseRef='" + TrnRefNo + "' where Trn_Ref_No='" + RefNo + "'"))
                {
                    myTrans.Rollback("ReviseAccData"); return retValue;
                }

                myCommand.CommandText = @"INSERT INTO [AccTransactionHeader]([Trn_Ref_No],[Trn_Jrn_Code],[Trn_Jrn_Type],[Trn_Voucher_Type],[Trn_Acc_Period],
                                        [Trn_Entry_DATE],[Trn_DATE],[Trn_Curr_Code],[Trn_Curr_Rate],[Trn_Entry_User],[Trn_LastUpdate_User],
                                        [Trn_LastUpdate_DATE],[Trn_Entry_Flag],[Trn_Revise_Flag],[Trn_Reverse_Flag]) VALUES('" +
                                        TrnRefNo + "'," + TrnJrnCode + ",'" + TrnHeader.Rows[0]["Trn_Jrn_Type"].ToString() + "','" + TrnHeader.Rows[0]["Trn_Voucher_Type"].ToString() + "','" + TrnAccPeriod + "',convert(datetime,'" +
                                        DateProcess.GetServerDate(ConnectionStr) + "',103),convert(datetime,'" + TrnDATE + "',103),'" + TrnHeader.Rows[0]["Trn_Curr_Code"].ToString() + "','" + TrnHeader.Rows[0]["Trn_Curr_Rate"].ToString() + "','" + TrnEntryUser + "','" + TrnEntryUser + "',convert(datetime,'" +
                                        DateProcess.GetServerDate(ConnectionStr) + "',103),'" + TrnHeader.Rows[0]["Trn_Entry_Flag"].ToString() + "','" + TrnHeader.Rows[0]["Trn_Revise_Flag"].ToString() + "','')";
                myCommand.ExecuteNonQuery();
                DataTable trnDetails = DataProcess.GetData(myCommand, "select * from AccTransactionDetails where Trn_Ref_No='" + RefNo + "'");
                foreach (DataRow dr in trnDetails.Rows)
                {
                    string TrnTrnType = "D";
                    if (dr["Trn_Trn_type"].ToString() == "D")
                        TrnTrnType = "C";
                    myCommand.CommandText = @"INSERT INTO [AccTransactionDetails]([Trn_Ref_No],[Trn_Ac_Code],[Trn_Jrn_Code],[Trn_Line_No],[Trn_Narration],
                                                    [Trn_Trn_type],[Trn_Amount],[Trn_Match],[Trn_Cheque_No],[Trn_Allocate_Flag],[Trn_Reval_Flag],
                                                    [Trn_Payment_DATE],[Trn_Ac_Desc],[Trn_Ac_Type],[Trn_Bus_Flag],[Trn_Due_DATE],[Trn_Sec_No],
                                                    [Trn_Adr_Code],[Trn_Dc_No],[Trn_GRN_No],[Trn_Bank_Reco_Flag],[Trn_Sub_No],[Trn_Tot_Int],
                                                    [Trn_Asset_Y_N],Trn_Instrument_Type,Trn_Cheque_Date,[T_Fl],[T_In],T_C2,T_C1,Trn_Code) VALUES('" +
                                                      TrnRefNo + "','" + dr["Trn_Ac_Code"].ToString() + "','" + TrnJrnCode + "','" + dr["Trn_Line_No"].ToString() + "','Bounced Against :'+'"+RefNo+" ,'+'" + dr["Trn_Narration"].ToString() + "','" +
                                                      TrnTrnType + "'," + dr["Trn_Amount"].ToString() + ",'" + dr["Trn_Match"].ToString() + "','" + dr["Trn_Cheque_No"].ToString() + "','" + dr["Trn_Allocate_Flag"].ToString() + "','" + ChkBounch + "',convert(datetime,'" +
                                                      dr["Trn_Payment_DATE"].ToString() + "',103),'" + dr["Trn_Ac_Desc"].ToString() + "','" + dr["Trn_Ac_Type"].ToString() + "','" + dr["Trn_Bus_Flag"].ToString() + "',convert(datetime,'" + dr["Trn_Due_DATE"].ToString() + "',103),'" + dr["Trn_Sec_No"].ToString() + "','" +
                                                      dr["Trn_Adr_Code"].ToString() + "','" + dr["Trn_Dc_No"].ToString() + "','" + dr["Trn_GRN_No"].ToString() + "','" + dr["Trn_Bank_Reco_Flag"].ToString() + "','" + dr["Trn_Sub_No"].ToString() + "','" + dr["Trn_Tot_Int"].ToString() + "','" +
                                                      dr["Trn_Asset_Y_N"].ToString() + "','" + dr["Trn_Instrument_Type"].ToString() + "',Convert(datetime,'" + dr["Trn_Cheque_Date"].ToString() + "',103),'','','" + RefNo + "','" + dr["T_C1"].ToString() + "','" + dr["Trn_Code"].ToString() + "')";
                    myCommand.ExecuteNonQuery();
                    if (Convert.ToString(dr["Trn_Cheque_No"]) != "" && TrnHeader.Rows[0]["Trn_Voucher_Type"].ToString() == StringEnum.GetStringValue(VoucherType.ChequeVoucher) && Convert.ToString(dr["Trn_Instrument_Type"]) == Enum.GetName(typeof(InstrumentType), InstrumentType.Cheque))
                    {
                        if (!DataProcess.ExecuteQuery(myCommand, "UPDATE [AccBankCheuqeDetails] SET [BNK_CHQ_Post_flag] = 'N' WHERE BNK_CHQ_NO='" + Convert.ToString(dr["Trn_Cheque_No"]) + "'"))
                        {
                            myTrans.Rollback("ReverseAccData"); return retValue;
                        }
                    }


                }

                DataTable ana = DataProcess.GetData(myCommand, "select * from AccTransactionAnalysis where Trn_Ref_No='" + RefNo + "'");

                foreach (DataRow dr in ana.Rows)
                {

                    myCommand.CommandText = @"INSERT INTO [AccTransactionAnalysis]([Trn_Ref_No],[Trn_Ac_Code],[Trn_Line_No],[Trn_Sub_No],
                                                    [Trn_AnaGroupDefinationCode1],[Trn_AnaGroupDefinationCode2],[Trn_AnaGroupDefinationCode3],
                                                    [Trn_AnaGroupDefinationCode4],[Trn_AnaGroupDefinationCode5],[Trn_AnaGroupLabelCode1],
                                                    [Trn_AnaGroupLabelCode2],[Trn_AnaGroupLabelCode3],[Trn_AnaGroupLabelCode4],
                                                    [Trn_AnaGroupLabelCode5]) VALUES('" +
                                            TrnRefNo + "','" + dr["Trn_Ac_Code"].ToString() + "'," + dr["Trn_Line_No"].ToString() + "," + dr["Trn_Sub_No"].ToString() + ",'" +
                                            dr["Trn_AnaGroupDefinationCode1"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode2"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode3"].ToString() + "','" +
                                            dr["Trn_AnaGroupDefinationCode4"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode5"].ToString() + "','" + dr["Trn_AnaGroupLabelCode1"].ToString() + "','" +
                                            dr["Trn_AnaGroupLabelCode2"].ToString() + "','" + dr["Trn_AnaGroupLabelCode3"].ToString() + "','" + dr["Trn_AnaGroupLabelCode4"].ToString() + "','" +
                                            dr["Trn_AnaGroupLabelCode5"].ToString() + "')";
                    myCommand.ExecuteNonQuery();
                }


                if (!DataProcess.ExecuteQuery(myCommand, "update AccTransactionDetails set Trn_Reval_Flag='B',trn_bus_flag='R' where trn_ref_no='" + RefNo+"'"))
                {
                    myTrans.Rollback("ReverseAccData"); return retValue;
                }
                if (!DataProcess.ExecuteQuery(myCommand, "update AccTransactionDetails set trn_dc_no='' where trn_ref_no='" + RefNo + "' and trn_dc_no='Chq. Realised'"))
                {
                    myTrans.Rollback("ReverseAccData"); return retValue;
                }
                if (!DataProcess.ExecuteQuery(myCommand, "update AccTransactionDetails set trn_reval_flag='' where trn_ref_no='" + RefNo + "' and trn_reval_flag='R'"))
                {
                    myTrans.Rollback("ReverseAccData"); return retValue;
                }

                ana = DataProcess.GetData(myCommand, "select * from AccTransactionReceivableBankInfo where Trn_Ref_No='" + RefNo + "'");

                foreach (DataRow dr in ana.Rows)
                {
                     myCommand.CommandText = @"INSERT INTO [AccTransactionReceivableBankInfo] ([Trn_Ref_No],[Trn_Jrn_Code],[Trn_Line_No],
                                                                [Trn_Bank_Name],[Trn_Branch_Name],[Trn_Account_No],
                                                                [Trn_Cheque_No],[Trn_Cheque_DATE]) VALUES ('" +
                                                TrnRefNo + "'," + dr["Trn_Jrn_Code"].ToString() + "," + dr["Trn_Line_No"].ToString() + ",'" +
                                            dr["Trn_Bank_Name"].ToString() + "','" + dr["Trn_Branch_Name"].ToString() + "','" + dr["Trn_Account_No"].ToString() + "','" +
                                            dr["Trn_Cheque_No"].ToString() + "',convert(datetime,'" + dr["Trn_Cheque_DATE"].ToString() + "',103))";
                     myCommand.ExecuteNonQuery();
                }

                ana = DataProcess.GetData(myCommand, "select * from AccTransactionReceivableBankInfo where Trn_Ref_No='" + RefNo + "' and trn_line_no='1'");
                foreach (DataRow dr in ana.Rows)
                {
                    myCommand.CommandText = @"INSERT INTO [Acc_CHQ_BOUNCE]([CHQ_BOUNCE_ACC_CODE],[CHQ_BOUNCE_CHQ_NO],[CHQ_BOUNCE_BANK_CODE],[CHQ_BOUNCE_DATE]) 
                                        VALUES('" + dr["Trn_Account_No"].ToString() + "','" + dr["Trn_Cheque_No"].ToString() + "','" + dr["Trn_Branch_Name"].ToString() + "',convert(datetime,'" + DateProcess.GetServerDate(ConnectionStr) + "',103))";
                    myCommand.ExecuteNonQuery();
                }
                
                myTrans.Commit();
                retValue[0] = TrnRefNo;
                retValue[1] = TrnJrnCode.ToString();
                return retValue;
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback("ReverseAccData");
                    System.Windows.Forms.MessageBox.Show(e.Message, StringProcess.messageHead);
                    return retValue;
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return retValue;
                    }
                    System.Windows.Forms.MessageBox.Show(ex.Message, StringProcess.messageHead);
                }

                return retValue;
            }
            finally
            {
                myConnection.Close();
            }

            return retValue;
        }

        public string[] ChequeRealisation(string ConnectionStr, string RefNo, string TrnAccPeriod, DateTime TrnDATE, string TrnEntryUser, string accCode, string ChequeClearingAccount)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionStr);
            myConnection.Open();

            SqlCommand myCommand = myConnection.CreateCommand();
            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string[] retValue = new string[2] { "", "" }; string OldRef = "", userid = "";
            string TrnJrnCode, TrnRefNo;
            myTrans = myConnection.BeginTransaction("ChequeRealisation");

            myCommand.Connection = myConnection;
            myCommand.Transaction = myTrans;
            try
            {
                if (accCode == ChequeClearingAccount)
                {
                    if (!DataProcess.ExecuteQuery(myCommand, "update AccTransactionDetails set Trn_Reval_Flag='R',trn_Narration='Realisation Against '+'" + RefNo + " '+trn_Narration,trn_match='" + RefNo + "' where trn_ref_no='" + RefNo + "' and trn_trn_type='C' and trn_ac_code='" + accCode + "'"))
                    {
                        myTrans.Rollback("ChequeRealisation"); return retValue;
                    }
                    myTrans.Commit();
                    return retValue;
                }
                else
                {
                    DataTable TrnHeader = DataProcess.GetData(myCommand, "Select * from AccTransactionHeader where Trn_Ref_No='" + RefNo + "'");
                    if (TrnHeader.Rows.Count == 0)
                    {
                        System.Windows.Forms.MessageBox.Show("No Records Found to Reverse in Transaction Header", StringProcess.messageHead); return retValue;
                    }
                    myCommand.CommandText = "select (ISNULL(MAX(trn_jrn_code),0)+1) as maxJrnCode from AccTransactionHeader";
                    sqlDataAdapterObj = new SqlDataAdapter();
                    sqlDataAdapterObj.SelectCommand = myCommand;
                    DataTable dtmaxJrnCode = new DataTable();
                    sqlDataAdapterObj.Fill(dtmaxJrnCode);
                    if (dtmaxJrnCode.Rows.Count > 0)
                        TrnJrnCode = Convert.ToDouble(dtmaxJrnCode.Rows[0]["maxJrnCode"].ToString()).ToString();
                    else
                    {
                        myTrans.Rollback("ChequeRealisation"); return retValue;
                    }

                    TrnRefNo = GetReferenceNo(myCommand, "JV", "J", StringEnum.GetStringValue(ModuleName.Accounts), TrnAccPeriod, TrnDATE);
                    if (TrnRefNo == "")
                    {
                        myTrans.Rollback("ChequeRealisation"); return retValue;
                    }

                    myCommand.CommandText = "select * from AccTransactionHeader where Trn_Ref_No='" + TrnRefNo + "'";
                    sqlDataAdapterObj = new SqlDataAdapter();
                    sqlDataAdapterObj.SelectCommand = myCommand;
                    DataTable dtmaxRefCheck = new DataTable();
                    sqlDataAdapterObj.Fill(dtmaxRefCheck);

                    if (dtmaxRefCheck.Rows.Count > 0)
                    {
                        myTrans.Rollback("ChequeRealisation");
                        System.Windows.Forms.MessageBox.Show("Refference No already Exits\rPlease Try Again Later", StringProcess.messageHead);
                        return retValue;
                    }

                    myCommand.CommandText = @"INSERT INTO [AccTransactionHeader]([Trn_Ref_No],[Trn_Jrn_Code],[Trn_Jrn_Type],[Trn_Voucher_Type],[Trn_Acc_Period],
                                        [Trn_Entry_DATE],[Trn_DATE],[Trn_Curr_Code],[Trn_Curr_Rate],[Trn_Entry_User],[Trn_LastUpdate_User],
                                        [Trn_LastUpdate_DATE],[Trn_Entry_Flag],[Trn_Revise_Flag],[Trn_Reverse_Flag]) VALUES('" +
                                            TrnRefNo + "'," + TrnJrnCode + ",'JV','J','" + TrnAccPeriod + "',convert(datetime,'" +
                                            DateProcess.GetServerDate(ConnectionStr) + "',103),convert(datetime,'" + TrnDATE + "',103),'" + TrnHeader.Rows[0]["Trn_Curr_Code"].ToString() + "','" + TrnHeader.Rows[0]["Trn_Curr_Rate"].ToString() + "','" + TrnEntryUser + "','" + TrnEntryUser + "',convert(datetime,'" +
                                            DateProcess.GetServerDate(ConnectionStr) + "',103),'" + TrnHeader.Rows[0]["Trn_Entry_Flag"].ToString() + "','" + TrnHeader.Rows[0]["Trn_Revise_Flag"].ToString() + "','')";
                    myCommand.ExecuteNonQuery();
                    DataTable trnDetails = DataProcess.GetData(myCommand, "select * from AccTransactionDetails where Trn_Ref_No='" + RefNo + "' and trn_ac_code='"+ChequeClearingAccount+"' order by Trn_Line_No,Trn_Sub_No");
                    int lin = 1; string Trn_Dc_No = "Chq. Realised";
                    foreach (DataRow dr in trnDetails.Rows)
                    {
                        string TrnTrnType = "D";
                         myCommand.CommandText = @"INSERT INTO [AccTransactionDetails]([Trn_Ref_No],[Trn_Ac_Code],[Trn_Jrn_Code],[Trn_Line_No],[Trn_Narration],
                                                    [Trn_Trn_type],[Trn_Amount],[Trn_Match],[Trn_Cheque_No],[Trn_Allocate_Flag],[Trn_Reval_Flag],
                                                    [Trn_Payment_DATE],[Trn_Ac_Desc],[Trn_Ac_Type],[Trn_Bus_Flag],[Trn_Due_DATE],[Trn_Sec_No],
                                                    [Trn_Adr_Code],[Trn_Dc_No],[Trn_GRN_No],[Trn_Bank_Reco_Flag],[Trn_Sub_No],[Trn_Tot_Int],
                                                    [Trn_Asset_Y_N],Trn_Instrument_Type,Trn_Cheque_Date,[T_Fl],[T_In],T_C2,T_C1,Trn_Code) VALUES('" +
                                                          TrnRefNo + "','" + dr["Trn_Ac_Code"].ToString() + "','" + TrnJrnCode + "','" + lin.ToString() + "','Realisation Against :'+'" + RefNo + " ,'+'" + dr["Trn_Narration"].ToString() + "','" +
                                                          TrnTrnType + "'," + dr["Trn_Amount"].ToString() + ",'" + dr["Trn_Match"].ToString() + "','" + dr["Trn_Cheque_No"].ToString() + "','" + dr["Trn_Allocate_Flag"].ToString() + "','',convert(datetime,'" +
                                                          dr["Trn_Payment_DATE"].ToString() + "',103),'" + dr["Trn_Ac_Desc"].ToString() + "','" + dr["Trn_Ac_Type"].ToString() + "','" + dr["Trn_Bus_Flag"].ToString() + "',convert(datetime,'" + dr["Trn_Due_DATE"].ToString() + "',103),'" + dr["Trn_Sec_No"].ToString() + "','" +
                                                          dr["Trn_Adr_Code"].ToString() + "','" + Trn_Dc_No + "','" + dr["Trn_GRN_No"].ToString() + "','" + dr["Trn_Bank_Reco_Flag"].ToString() + "','" + dr["Trn_Sub_No"].ToString() + "','" + dr["Trn_Tot_Int"].ToString() + "','" +
                                                          dr["Trn_Asset_Y_N"].ToString() + "','" + dr["Trn_Instrument_Type"].ToString() + "',Convert(datetime,'" + dr["Trn_Cheque_Date"].ToString() + "',103),'','','" + RefNo + "','" + dr["T_C1"].ToString() + "','" + dr["Trn_Code"].ToString() + "')";
                        myCommand.ExecuteNonQuery();
                        lin = lin + 1;
                    }

                    string trnActype = "C";

                    foreach (DataRow dr in trnDetails.Rows)
                    {
                        string TrnTrnType = "C";
                        myCommand.CommandText = @"INSERT INTO [AccTransactionDetails]([Trn_Ref_No],[Trn_Ac_Code],[Trn_Jrn_Code],[Trn_Line_No],[Trn_Narration],
                                                    [Trn_Trn_type],[Trn_Amount],[Trn_Match],[Trn_Cheque_No],[Trn_Allocate_Flag],[Trn_Reval_Flag],
                                                    [Trn_Payment_DATE],[Trn_Ac_Desc],[Trn_Ac_Type],[Trn_Bus_Flag],[Trn_Due_DATE],[Trn_Sec_No],
                                                    [Trn_Adr_Code],[Trn_Dc_No],[Trn_GRN_No],[Trn_Bank_Reco_Flag],[Trn_Sub_No],[Trn_Tot_Int],
                                                    [Trn_Asset_Y_N],Trn_Instrument_Type,Trn_Cheque_Date,[T_Fl],[T_In],T_C2,T_C1,Trn_Code) VALUES('" +
                                                         TrnRefNo + "','" + accCode + "','" + TrnJrnCode + "','" + lin.ToString() + "','Realisation Against :'+'" + RefNo + " ,'+'" + dr["Trn_Narration"].ToString() + "','" +
                                                         TrnTrnType + "'," + dr["Trn_Amount"].ToString() + ",'" + dr["Trn_Match"].ToString() + "','" + dr["Trn_Cheque_No"].ToString() + "','" + dr["Trn_Allocate_Flag"].ToString() + "','',convert(datetime,'" +
                                                         dr["Trn_Payment_DATE"].ToString() + "',103),'" + dr["Trn_Ac_Desc"].ToString() + "','" + trnActype.ToString() + "','" + dr["Trn_Bus_Flag"].ToString() + "',convert(datetime,'" + dr["Trn_Due_DATE"].ToString() + "',103),'" + dr["Trn_Sec_No"].ToString() + "','" +
                                                         dr["Trn_Adr_Code"].ToString() + "','" + Trn_Dc_No + "','" + dr["Trn_GRN_No"].ToString() + "','" + dr["Trn_Bank_Reco_Flag"].ToString() + "','" + dr["Trn_Sub_No"].ToString() + "','" + dr["Trn_Tot_Int"].ToString() + "','" +
                                                         dr["Trn_Asset_Y_N"].ToString() + "','" + dr["Trn_Instrument_Type"].ToString() + "',Convert(datetime,'" + dr["Trn_Cheque_Date"].ToString() + "',103),'','','" + RefNo + "','" + dr["T_C1"].ToString() + "','" + dr["Trn_Code"].ToString() + "')";
                        myCommand.ExecuteNonQuery();
                        lin = lin + 1;
                    }

                    DataTable ana = DataProcess.GetData(myCommand, "select * from AccTransactionAnalysis where Trn_Ref_No='" + RefNo + "' and trn_ac_code='" + ChequeClearingAccount + "' order by [Trn_Line_No],[Trn_Sub_No]");
                    lin = 1;
                    foreach (DataRow dr in ana.Rows)
                    {

                        myCommand.CommandText = @"INSERT INTO [AccTransactionAnalysis]([Trn_Ref_No],[Trn_Ac_Code],[Trn_Line_No],[Trn_Sub_No],
                                                    [Trn_AnaGroupDefinationCode1],[Trn_AnaGroupDefinationCode2],[Trn_AnaGroupDefinationCode3],
                                                    [Trn_AnaGroupDefinationCode4],[Trn_AnaGroupDefinationCode5],[Trn_AnaGroupLabelCode1],
                                                    [Trn_AnaGroupLabelCode2],[Trn_AnaGroupLabelCode3],[Trn_AnaGroupLabelCode4],
                                                    [Trn_AnaGroupLabelCode5]) VALUES('" +
                                                TrnRefNo + "','" + dr["Trn_Ac_Code"].ToString() + "'," + lin.ToString() + "," + dr["Trn_Sub_No"].ToString() + ",'" +
                                                dr["Trn_AnaGroupDefinationCode1"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode2"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode3"].ToString() + "','" +
                                                dr["Trn_AnaGroupDefinationCode4"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode5"].ToString() + "','" + dr["Trn_AnaGroupLabelCode1"].ToString() + "','" +
                                                dr["Trn_AnaGroupLabelCode2"].ToString() + "','" + dr["Trn_AnaGroupLabelCode3"].ToString() + "','" + dr["Trn_AnaGroupLabelCode4"].ToString() + "','" +
                                                dr["Trn_AnaGroupLabelCode5"].ToString() + "')";
                        myCommand.ExecuteNonQuery();
                        lin = lin + 1;
                    }
                    foreach (DataRow dr in ana.Rows)
                    {

                        myCommand.CommandText = @"INSERT INTO [AccTransactionAnalysis]([Trn_Ref_No],[Trn_Ac_Code],[Trn_Line_No],[Trn_Sub_No],
                                                    [Trn_AnaGroupDefinationCode1],[Trn_AnaGroupDefinationCode2],[Trn_AnaGroupDefinationCode3],
                                                    [Trn_AnaGroupDefinationCode4],[Trn_AnaGroupDefinationCode5],[Trn_AnaGroupLabelCode1],
                                                    [Trn_AnaGroupLabelCode2],[Trn_AnaGroupLabelCode3],[Trn_AnaGroupLabelCode4],
                                                    [Trn_AnaGroupLabelCode5]) VALUES('" +
                                                TrnRefNo + "','" + accCode + "'," + lin.ToString() + "," + dr["Trn_Sub_No"].ToString() + ",'" +
                                                dr["Trn_AnaGroupDefinationCode1"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode2"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode3"].ToString() + "','" +
                                                dr["Trn_AnaGroupDefinationCode4"].ToString() + "','" + dr["Trn_AnaGroupDefinationCode5"].ToString() + "','" + dr["Trn_AnaGroupLabelCode1"].ToString() + "','" +
                                                dr["Trn_AnaGroupLabelCode2"].ToString() + "','" + dr["Trn_AnaGroupLabelCode3"].ToString() + "','" + dr["Trn_AnaGroupLabelCode4"].ToString() + "','" +
                                                dr["Trn_AnaGroupLabelCode5"].ToString() + "')";
                        myCommand.ExecuteNonQuery();
                        lin = lin + 1;
                    }

                    if (!DataProcess.ExecuteQuery(myCommand, "update AccTransactionDetails set Trn_Reval_Flag='R' where trn_ref_no='" + RefNo+"' and trn_ac_code='"+ChequeClearingAccount+"'"))
                    {
                        myTrans.Rollback("ChequeRealisation"); return retValue;
                    }


                    ana = DataProcess.GetData(myCommand, "select * from AccTransactionReceivableBankInfo where Trn_Ref_No='" + RefNo + "' and Trn_Account_No='" + ChequeClearingAccount + "'");
                    lin = 1;
                    foreach (DataRow dr in ana.Rows)
                    {
                        myCommand.CommandText = @"INSERT INTO [AccTransactionReceivableBankInfo] ([Trn_Ref_No],[Trn_Jrn_Code],[Trn_Line_No],
                                                                [Trn_Bank_Name],[Trn_Branch_Name],[Trn_Account_No],
                                                                [Trn_Cheque_No],[Trn_Cheque_DATE]) VALUES ('" +
                                                   TrnRefNo + "'," +TrnJrnCode + "," + lin.ToString() + ",'" +
                                               dr["Trn_Bank_Name"].ToString() + "','" + dr["Trn_Branch_Name"].ToString() + "','" + dr["Trn_Account_No"].ToString() + "','" +
                                               dr["Trn_Cheque_No"].ToString() + "',convert(datetime,'" + dr["Trn_Cheque_DATE"].ToString() + "',103))";
                        myCommand.ExecuteNonQuery();
                        lin = lin + 1;
                    }
                    foreach (DataRow dr in ana.Rows)
                    {
                        myCommand.CommandText = @"INSERT INTO [AccTransactionReceivableBankInfo] ([Trn_Ref_No],[Trn_Jrn_Code],[Trn_Line_No],
                                                                [Trn_Bank_Name],[Trn_Branch_Name],[Trn_Account_No],
                                                                [Trn_Cheque_No],[Trn_Cheque_DATE]) VALUES ('" +
                                                   TrnRefNo + "'," + TrnJrnCode + "," + lin.ToString() + ",'" +
                                               dr["Trn_Bank_Name"].ToString() + "','" + dr["Trn_Branch_Name"].ToString() + "','" + accCode + "','" +
                                               dr["Trn_Cheque_No"].ToString() + "',convert(datetime,'" + dr["Trn_Cheque_DATE"].ToString() + "',103))";
                        myCommand.ExecuteNonQuery();
                        lin = lin + 1;
                    }

                    myTrans.Commit();
                    retValue[0] = TrnRefNo;
                    retValue[1] = TrnJrnCode.ToString();
                    return retValue;
                }
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback("ChequeRealisation");
                    System.Windows.Forms.MessageBox.Show(e.Message, StringProcess.messageHead);
                    return retValue;
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return retValue;
                    }
                    System.Windows.Forms.MessageBox.Show(ex.Message, StringProcess.messageHead);
                }

                return retValue;
            }
            finally
            {
                myConnection.Close();
            }

            return retValue;
        }        

        public string JournalPermission(SqlCommand myCommand,string companycode, string UserId)
        {
            SqlDataAdapter sqlDataAdapterObj = null;
            string jr = "";
            string jr1 = "";
            DataTable dt;
            myCommand.CommandText = "select distinct [JrnType] from [UserJournalPermission] where [CompanyCode]='" + companycode + "' and [UserID]='" + UserId + "'";
            sqlDataAdapterObj = new SqlDataAdapter();
            sqlDataAdapterObj.SelectCommand = myCommand;
            dt = new DataTable();
            sqlDataAdapterObj.Fill(dt);

            int i = 0;
            
            jr = "(";

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    jr1 = "";
                    jr1 =Convert.ToString(dt.Rows[i]["JrnType"].ToString());

                    if (dt.Rows.Count-1 >i)
                    {
                        jr1 = "'" + jr1 + "',";
                    }
                    else
                    {
                        jr1 = "'" + jr1 + "'"; 
                    }
                    
                    jr = jr + jr1;

                    i = i + 1;
                }              

            }
            else
            { 
            }

            jr = jr + ")";

            return jr;
        }
    }

