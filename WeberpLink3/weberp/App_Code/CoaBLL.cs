using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Link3FrameWork;


    public class CoaBLL
    {
        public CoaBLL() { }

        public bool InsertCoaData(string ConnectionStr, COADAO coadao, List<CoaAccGroupDAO> grp, bool updateflg)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionStr);
            myConnection.Open();

            SqlCommand myCommand = myConnection.CreateCommand();
            SqlTransaction myTrans;

            myTrans = myConnection.BeginTransaction("InsertCoaData");

            myCommand.Connection = myConnection;
            myCommand.Transaction = myTrans;

            try
            {
                DataTable dt = DataProcess.GetData(ConnectionStr, "select * from AccCoaAnalysis where Gl_Coa_Code='" + coadao.GlCoaCode + "'");
                if (dt.Rows.Count > 0)
                    coadao.GlCoaTrnAnaReq = "Y";
                else
                    coadao.GlCoaTrnAnaReq = "N";
                coadao.GlCoaTrnFlg = "N";
                if (updateflg)
                {
                    myCommand.CommandText = "UPDATE [AccCOA] SET [Gl_Coa_Name] = '" + coadao.GlCoaName + "',[Gl_Coa_Type] = '" + coadao.GlCoaType + "',[Gl_Coa_Sta] = '" + coadao.GlCoaSta + "',[Gl_Coa_Comm] ='" + coadao.GlCoaComm + "',[Gl_Coa_Cur_Code] ='" + coadao.GLCOACRCODE + "',[Gl_Coa_Upd_DATE] = convert(datetime,'" + DateTime.Now + "',103),[Gl_Coa_Cntrl_Code] = '" + coadao.GlCoaCntrlCode + "',[Gl_Coa_Asset_Y_N] = '" + coadao.GlCoaAssetYN + "',[Gl_Coa_Trn_Ana_Req] = '" + coadao.GlCoaTrnAnaReq + "',[AccTypeID] = " + coadao.AccTypeID + ",[AccTypeName] = '" + coadao.AccTypeName + "' WHERE Gl_Coa_Code='" + coadao.GlCoaCode + "'";
                    myCommand.ExecuteNonQuery();
                }
                else
                {
                    myCommand.CommandText = @"INSERT INTO [AccCOA]([Gl_Coa_Code],[Gl_Coa_Name],[Gl_Coa_Sec_Code],
                                        [Gl_Coa_Type],Gl_Coa_Sta,[Gl_Coa_Trn_DATE],[Gl_Coa_Comm],[Gl_Coa_Cur_Code],
                                        [Gl_Coa_Upd_DATE],[Gl_Coa_Trn_Flg],[Gl_Coa_Cntrl_Code],[Gl_Coa_Asset_Y_N],
                                        [Gl_Coa_Asset_ON_OFF],[Gl_Coa_Trn_Ana_Req],[AccTypeID],[AccTypeName]) values('" +
                                            coadao.GlCoaCode + "','" + coadao.GlCoaName + "','" + coadao.GlCoaCode + "','" +
                                            coadao.GlCoaType + "','" + coadao.GlCoaSta + "',convert(datetime,'" + coadao.GlCoaTrnDATE + "',103),'" + coadao.GlCoaComm + "','" + coadao.GLCOACRCODE + "',convert(datetime,'" +
                                            DateTime.Now + "',103),'" + coadao.GlCoaTrnFlg + "','" + coadao.GlCoaCntrlCode + "','" + coadao.GlCoaAssetYN + "','" +
                                            coadao.GlCoaAssetONOFF + "','" + coadao.GlCoaTrnAnaReq + "'," + coadao.AccTypeID + ",'" + coadao.AccTypeName + "')";
                    myCommand.ExecuteNonQuery();
                }
                if (coadao.BankCashType == "B" || coadao.BankCashType == "C")
                {
                    myCommand.CommandText = @"Delete [AccCOADayBook] where Acc_Code='" + coadao.GlCoaCode + "'";
                    myCommand.ExecuteNonQuery();

                    myCommand.CommandText = @"INSERT INTO [AccCOADayBook]([Acc_Code],[Dbk_Type]) values('" + coadao.GlCoaCode + "','" + coadao.BankCashType + "')";
                    myCommand.ExecuteNonQuery();
                }

                foreach (CoaAccGroupDAO cg in grp)
                {
                    myCommand.CommandText = @"Delete from [AccCoaAccountsGroup] where coa_acc_code='" + cg.CoaCode + "' and coa_grp_id='" + cg.CoaGroupID + "'";
                    myCommand.ExecuteNonQuery();

                    myCommand.CommandText = @"INSERT INTO [AccCoaAccountsGroup](coa_acc_code,coa_grp_id,coa_grp_code) values('" + cg.CoaCode + "','" + cg.CoaGroupID + "','" + cg.CoaGroupCode + "')";
                    myCommand.ExecuteNonQuery();
                }
                myTrans.Commit();
                return true;
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback("InsertCoaData");
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

            return true;
        }

        public bool DeleteCoaData(string ConnectionStr, string coaCode)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionStr);
            myConnection.Open();

            SqlCommand myCommand = myConnection.CreateCommand();
            SqlTransaction myTrans;

            myTrans = myConnection.BeginTransaction("DeleteCoaData");

            myCommand.Connection = myConnection;
            myCommand.Transaction = myTrans;

            try
            {
                DataTable dt = DataProcess.GetData(ConnectionStr, "select * from AccCoaAnalysis where Gl_Coa_Code='" + coaCode + "'");

                myCommand.CommandText = "Delete from [AccCOA] WHERE Gl_Coa_Code='" + coaCode + "'";
                myCommand.ExecuteNonQuery();

                myCommand.CommandText = @"Delete from [AccCoaAnalysis] WHERE Gl_Coa_Code='" + coaCode + "'";
                myCommand.ExecuteNonQuery();

                myCommand.CommandText = @"Delete [AccCOADayBook] where Acc_Code='" + coaCode + "'";
                myCommand.ExecuteNonQuery();

                myCommand.CommandText = @"Delete from [AccCoaAccountsGroup] where coa_acc_code='" + coaCode + "'";
                myCommand.ExecuteNonQuery();

                myTrans.Commit();
                return true;
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback("DeleteCoaData");
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


            return true;
        }

        public bool InsertCustomerData(string ConnectionStr, SamaParAccDAO sama, List<CoaAccGroupDAO> grp, bool updateflg)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionStr);
            myConnection.Open();

            SqlCommand myCommand = myConnection.CreateCommand();
            SqlTransaction myTrans;

            myTrans = myConnection.BeginTransaction("InsertCustomerData");

            myCommand.Connection = myConnection;
            myCommand.Transaction = myTrans;

            try
            {
                DataTable dt = DataProcess.GetData(ConnectionStr, "select * from AccCoaAnalysis where Gl_Coa_Code='" + sama.ParAccCode + "'");
                if (dt.Rows.Count > 0)
                    sama.ParAccAnaReq = "Y";
                else
                    sama.ParAccAnaReq = "N";

                sama.ParAccType = "C";
                sama.ParAccBo = "O";
                sama.ParAccTrnFlag = "N";
                sama.ParAccPerm = 0;
                sama.ParAcctotCr = 0;
                sama.ParAcctotDb = 0;
                sama.ParAccUnPostCr = 0;
                sama.ParAccUpPostDb = 0;
                sama.ParAccBalAmt = 0;
                sama.ParAccBalFlg = "";
                sama.ParAccCurCode = "BDT";
                sama.ParAccBalTeReq = "N";
                sama.ParAccNarAmtReq = "N";
                sama.ParAccNarAmtType = "";
                sama.TFl = "";
                sama.TIn = "0";
                sama.ParAccUpdDate = DateTime.Now.Date;
                if (updateflg)
                {
                    myCommand.CommandText = @"UPDATE [SaMa_Par_Acc]   SET [Par_Acc_Name] = '" + sama.ParAccName + "',[Par_Acc_Sta] = '" + sama.ParAccSta + "',[Par_Acc_Upd_DATE] = Convert(datetime,'" + sama.ParAccUpdDate + "',103),[Par_Acc_Comm] = '" + sama.ParAccComm + "',[Par_Acc_Perm] = '" + sama.ParAccPerm + "',[Par_Acc_Ana_Req] = '" + sama.ParAccAnaReq + "' ,[T_C2] = '" + sama.TC2 + "',[T_C1] = '" + sama.TC1 + "' WHERE Par_Acc_Code='" + sama.ParAccCode + "' ";
                    myCommand.ExecuteNonQuery();
                }
                else
                {
                    myCommand.CommandText = @"INSERT INTO [SaMa_Par_Acc]([Par_Acc_Code],[Par_Acc_Name],
                                            [Par_Acc_Sec_Code],[Par_Acc_Type],[Par_Acc_Bo],
                                            [Par_Acc_Sta],[Par_Acc_Trn_DATE],[Par_Acc_Trn_Flag],
                                            [Par_Acc_Upd_DATE],[Par_Acc_Comm],[Par_Acc_Perm],
                                            [Par_Acc_Tot_Cr],[Par_Acc_Tot_Db],[Par_Acc_Unpost_Cr],
                                            [Par_Acc_Unpost_Db],[Par_Acc_Bal_Amt],[Par_Acc_Bal_Flag],
                                            [Par_Acc_Cur_Code],[Par_Acc_Ana_Req],[Par_Acc_Bal_Te_Req],
                                            [Par_Acc_Nar_Amt_Req],[Par_Acc_Nar_Amt_Type],[T_C1],
                                            [T_C2],[T_Fl],[T_In]) VALUES(" +
                                            "'" + sama.ParAccCode + "','" + sama.ParAccName + "'," +
                                            "'" + sama.ParAccSecCode + "','" + sama.ParAccType + "','" + sama.ParAccBo + "'," +
                                            "'" + sama.ParAccSta + "',Convert(datetime,'" + sama.ParAccTrnDate + "',103),'" + sama.ParAccTrnFlag + "'," +
                                            "Convert(datetime,'" + sama.ParAccUpdDate + "',103),'" + sama.ParAccComm + "','" + sama.ParAccPerm + "'," +
                                            "" + sama.ParAcctotCr + "," + sama.ParAcctotDb + "," + sama.ParAccUnPostCr + "," +
                                            "" + sama.ParAccUpPostDb + "," + sama.ParAccBalAmt + ",'" + sama.ParAccBalFlg + "'," +
                                            "'" + sama.ParAccCurCode + "','" + sama.ParAccAnaReq + "','" + sama.ParAccBalTeReq + "'," +
                                            "'" + sama.ParAccNarAmtReq + "','" + sama.ParAccNarAmtType + "','" + sama.TC1 + "'," +
                                            "'" + sama.TC2 + "','" + sama.TFl + "','" + sama.TIn + "')";
                    myCommand.ExecuteNonQuery();
                }
                
                foreach (CoaAccGroupDAO cg in grp)
                {
                    myCommand.CommandText = @"Delete from [SaMa_Par_Acc_Grp] where Par_Grp_Acc_Code='" + cg.CoaCode + "' and Par_Grp_Id='" + cg.CoaGroupID + "'";
                    myCommand.ExecuteNonQuery();

                    myCommand.CommandText = @"INSERT INTO [SaMa_Par_Acc_Grp](Par_Grp_Acc_Code,Par_Grp_Id,Par_Grp_Code) values('" + cg.CoaCode + "','" + cg.CoaGroupID + "','" + cg.CoaGroupCode + "')";
                    myCommand.ExecuteNonQuery();
                }
                myTrans.Commit();
                return true;
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback("InsertCustomerData");
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

            return true;
        }

        public bool InsertCustomerData(string ConnectionStr, SamaParAccDAO sama, List<CoaAccGroupDAO> grp, bool updateflg, List<AccCoaAnalysis> Analysis, string chkst)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionStr);
            myConnection.Open();

            SqlCommand myCommand = myConnection.CreateCommand();
            SqlTransaction myTrans;

            myTrans = myConnection.BeginTransaction("InsertCustomerData");

            myCommand.Connection = myConnection;
            myCommand.Transaction = myTrans;

            try
            {
             

                myCommand.CommandText = "delete from AccCoaAnalysis where Gl_coa_code = '" + sama.ParAccCode + "'";

                myCommand.ExecuteNonQuery();


                if (chkst == "Y")
                {

                    foreach (AccCoaAnalysis ana in Analysis)
                    {
                        myCommand.CommandText = "INSERT INTO [AccCoaAnalysis]([Gl_Coa_Code],[COST_ID],[COST_NAME],[LinNo]) VALUES('" + ana.GlCoaCode + "','" + ana.COSTID + "','" + ana.COSTNAME + "'," + ana.LinNo + ")";

                        myCommand.ExecuteNonQuery();
                    }
                }
                DataTable dt = DataProcess.GetData(myCommand, "select * from AccCoaAnalysis where Gl_Coa_Code='" + sama.ParAccCode + "'");
             
                if (dt.Rows.Count > 0)
                    sama.ParAccAnaReq = "Y";
                else
                    sama.ParAccAnaReq = "N";

                sama.ParAccType = "C";
                sama.ParAccBo = "O";
                sama.ParAccTrnFlag = "N";
                sama.ParAccPerm = 0;
                sama.ParAcctotCr = 0;
                sama.ParAcctotDb = 0;
                sama.ParAccUnPostCr = 0;
                sama.ParAccUpPostDb = 0;
                sama.ParAccBalAmt = 0;
                sama.ParAccBalFlg = "";
                sama.ParAccCurCode = "BDT";
                sama.ParAccBalTeReq = "N";
                sama.ParAccNarAmtReq = "N";
                sama.ParAccNarAmtType = "";
                sama.TFl = "";
                sama.TIn = "0";
                sama.ParAccUpdDate = DateTime.Now.Date;
                if (updateflg)
                {
                    myCommand.CommandText = @"UPDATE [SaMa_Par_Acc]   SET [Par_Acc_Name] = '" + sama.ParAccName + "',[Par_Acc_Sta] = '" + sama.ParAccSta + "',[Par_Acc_Upd_DATE] = Convert(datetime,'" + sama.ParAccUpdDate + "',103),[Par_Acc_Comm] = '" + sama.ParAccComm + "',[Par_Acc_Perm] = '" + sama.ParAccPerm + "',[Par_Acc_Ana_Req] = '" + sama.ParAccAnaReq + "' ,[T_C2] = '" + sama.TC2 + "',[T_C1] = '" + sama.TC1 + "' WHERE Par_Acc_Code='" + sama.ParAccCode + "' ";
                    myCommand.ExecuteNonQuery();
                }
                else
                {
                    myCommand.CommandText = @"INSERT INTO [SaMa_Par_Acc]([Par_Acc_Code],[Par_Acc_Name],
                                            [Par_Acc_Sec_Code],[Par_Acc_Type],[Par_Acc_Bo],
                                            [Par_Acc_Sta],[Par_Acc_Trn_DATE],[Par_Acc_Trn_Flag],
                                            [Par_Acc_Upd_DATE],[Par_Acc_Comm],[Par_Acc_Perm],
                                            [Par_Acc_Tot_Cr],[Par_Acc_Tot_Db],[Par_Acc_Unpost_Cr],
                                            [Par_Acc_Unpost_Db],[Par_Acc_Bal_Amt],[Par_Acc_Bal_Flag],
                                            [Par_Acc_Cur_Code],[Par_Acc_Ana_Req],[Par_Acc_Bal_Te_Req],
                                            [Par_Acc_Nar_Amt_Req],[Par_Acc_Nar_Amt_Type],[T_C1],
                                            [T_C2],[T_Fl],[T_In]) VALUES(" +
                                            "'" + sama.ParAccCode + "','" + sama.ParAccName + "'," +
                                            "'" + sama.ParAccSecCode + "','" + sama.ParAccType + "','" + sama.ParAccBo + "'," +
                                            "'" + sama.ParAccSta + "',null,'" + sama.ParAccTrnFlag + "'," +
                                            "Convert(datetime,'" + sama.ParAccUpdDate + "',103),'" + sama.ParAccComm + "','" + sama.ParAccPerm + "'," +
                                            "" + sama.ParAcctotCr + "," + sama.ParAcctotDb + "," + sama.ParAccUnPostCr + "," +
                                            "" + sama.ParAccUpPostDb + "," + sama.ParAccBalAmt + ",'" + sama.ParAccBalFlg + "'," +
                                            "'" + sama.ParAccCurCode + "','" + sama.ParAccAnaReq + "','" + sama.ParAccBalTeReq + "'," +
                                            "'" + sama.ParAccNarAmtReq + "','" + sama.ParAccNarAmtType + "','" + sama.TC1 + "'," +
                                            "'" + sama.TC2 + "','" + sama.TFl + "','" + sama.TIn + "')";
                    myCommand.ExecuteNonQuery();
                }

                foreach (CoaAccGroupDAO cg in grp)
                {
                    myCommand.CommandText = @"Delete from [SaMa_Par_Acc_Grp] where Par_Grp_Acc_Code='" + cg.CoaCode + "' and Par_Grp_Id='" + cg.CoaGroupID + "'";
                    myCommand.ExecuteNonQuery();

                    myCommand.CommandText = @"INSERT INTO [SaMa_Par_Acc_Grp](Par_Grp_Acc_Code,Par_Grp_Id,Par_Grp_Code) values('" + cg.CoaCode + "','" + cg.CoaGroupID + "','" + cg.CoaGroupCode + "')";
                    myCommand.ExecuteNonQuery();
                }

                myCommand.CommandText = "delete from CoaGrp where CoaCode = '" + sama.ParAccCode + "'";
                myCommand.ExecuteNonQuery();
                
                if (grp.Count > 0)
                {
                    string str = "INSERT INTO [CoaGrp]([CoaCode],[Grp],[Grp1],[Grp2],[Grp3]) VALUES('" + grp[0].CoaCode + "',1,'" + grp[0].CoaGroupCodeName + "','" + grp[1].CoaGroupCodeName + "','" + grp[2].CoaGroupCodeName + "')";
                    myCommand.CommandText = str;
                    myCommand.ExecuteNonQuery();
                }

                myTrans.Commit();
                return true;
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback("InsertCustomerData");
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

            return true;
        }

        public bool DeleteCustomerData(string ConnectionStr, string coaCode)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionStr);
            myConnection.Open();

            SqlCommand myCommand = myConnection.CreateCommand();
            SqlTransaction myTrans;

            myTrans = myConnection.BeginTransaction("DeleteCustomerData");

            myCommand.Connection = myConnection;
            myCommand.Transaction = myTrans;

            try
            {
                myCommand.CommandText = "Delete from [SaMa_Par_Acc] WHERE Par_Acc_Code='" + coaCode + "'";
                myCommand.ExecuteNonQuery();

                myCommand.CommandText = @"Delete from [fa_trn_grp] WHERE Gl_Coa_Code='" + coaCode + "'";
                myCommand.ExecuteNonQuery();

                myCommand.CommandText = @"Delete from [SaMa_Par_Acc_Grp] where Par_Grp_Acc_Code='" + coaCode + "'";
                myCommand.ExecuteNonQuery();

                myCommand.CommandText = @"Delete from [Fa_Acc_Grp] where Coa_Code='" + coaCode + "'";
                myCommand.ExecuteNonQuery();

                myTrans.Commit();
                return true;
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback("DeleteCustomerData");
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


            return true;
        }

        public bool InsertCustomerAddressData(string ConnectionStr, SamaParAdrDAO sama, bool updateflg)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionStr);
            myConnection.Open();

            SqlCommand myCommand = myConnection.CreateCommand();
            SqlTransaction myTrans;

            myTrans = myConnection.BeginTransaction("InsertCustomerAddressData");

            myCommand.Connection = myConnection;
            myCommand.Transaction = myTrans;

            try
            {


                sama.TFl = "";
                sama.TIn = "0";
                sama.ParAdrUpdDate = System.DateTime.Now.Date;
                if (updateflg)
                {
                    myCommand.CommandText = @"UPDATE [SaMa_Par_Adr]   SET [par_adr_name] ='" + sama.ParAdrName + "',[Par_Adr_Sec_Code] = '" + sama.ParAdrSecCode + "',[Par_Adr_Line1] = '" + sama.ParAdrLine1 + "',[Par_Adr_Line2] = '" + sama.ParAdrLine2 + "',[Par_Adr_Line3] ='" + sama.ParAdrLine3 + "',[Par_Adr_Line4] = '" + sama.ParAdrLine4 + "',[Par_Adr_Line5] ='" + sama.ParAdrLine5 + "',[Par_Adr_Cst_No] = '" + sama.ParAdrCstNo + "',[Par_Adr_Lst_No] = '" + sama.ParAdrLstNo + "',[Par_Adr_Cnt_No] = '" + sama.ParAdrCntNo + "',[Par_Adr_Tel_No] = '" + sama.ParAdrTelNo + "',[Par_Adr_Fax_No] = '" + sama.ParAdrFaxNo + "',[Par_Adr_Email_Id] = '" + sama.ParAdrEmailId + "',[Par_Adr_Acc_Code] = '" + sama.ParAdrAccCode + "',[Par_Adr_Cmt] = '" + sama.ParAdrCmt + "',[Par_Adr_Upd_DATE] = convert(datetime,'" + sama.ParAdrUpdDate + "',103),[Par_Adr_Trn_Flag] = '" + sama.ParAdrTrnFlag + "',[T_C1] = '" + sama.TC1 + "',[T_C2] ='" + sama.TC2 + "',[T_Fl] = '" + sama.TFl + "',[T_In] =" + sama.TIn + " WHERE [Par_Adr_Code]='" + sama.ParAdrCode + "'";
                    myCommand.ExecuteNonQuery();
                }
                else
                {
                    myCommand.CommandText = @"INSERT INTO [SaMa_Par_Adr]([Par_Adr_Code],[par_adr_name],[Par_Adr_Sec_Code],
                                            [Par_Adr_Line1],[Par_Adr_Line2],[Par_Adr_Line3],
                                            [Par_Adr_Line4],[Par_Adr_Line5],[Par_Adr_Cst_No],
                                            [Par_Adr_Lst_No],[Par_Adr_Cnt_No],[Par_Adr_Tel_No],
                                            [Par_Adr_Fax_No],[Par_Adr_Email_Id],[Par_Adr_Acc_Code],
                                            [Par_Adr_Cmt],[Par_Adr_Upd_DATE],[Par_Adr_Trn_Flag],
                                            [Par_Adr_Lst_Trn_DATE],[Par_Adr_Ord_Bal],[Par_Adr_Inv_Val],
                                            [T_C1],[T_C2],[T_Fl],[T_In]) VALUES(" +
                                            "'" + sama.ParAdrCode + "','" + sama.ParAdrName  + "','" + sama.ParAdrSecCode + "'" +
                                            ",'" + sama.ParAdrLine1 + "','" + sama.ParAdrLine2 + "','" + sama.ParAdrLine3 + "'" +
                                            ",'" + sama.ParAdrLine4 + "','" + sama.ParAdrLine5 + "','" + sama.ParAdrCstNo + "'" +
                                            ",'" + sama.ParAdrLstNo + "','" + sama.ParAdrCntNo + "','" + sama.ParAdrTelNo + "'" +
                                            ",'" + sama.ParAdrFaxNo + "','" + sama.ParAdrEmailId + "','" + sama.ParAdrAccCode + "'" +
                                            ",'" + sama.ParAdrCmt + "',convert(datetime,'" + sama.ParAdrUpdDate + "',103),'" + sama.ParAdrTrnFlag + "'" +
                                            ",null,0,0,'" + sama.TC1 + "','" + sama.TC2 + "','" + sama.TFl + "'," + sama.TIn + ")";
                    myCommand.ExecuteNonQuery();
                }

                if (!DataProcess.ExecuteQuery(DBConnCls.ConnectionStringWFA2, "update [clientDatabaseMain] set [UpdStatus]='Y' where [brAdrCode]='" + sama.ParAdrCode + "' and [UpdStatus]='N'"))
                {
                    myTrans.Rollback("InsertCustomerAddressData");
                    return false;
                }

                myTrans.Commit();
                return true;
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback("InsertCustomerAddressData");
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

            return true;
        }

        public bool DeleteCustomerAddressData(string ConnectionStr, string coaCode)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionStr);
            myConnection.Open();

            SqlCommand myCommand = myConnection.CreateCommand();
            SqlTransaction myTrans;

            myTrans = myConnection.BeginTransaction("DeleteCustomerAddressData");

            myCommand.Connection = myConnection;
            myCommand.Transaction = myTrans;

            try
            {
                myCommand.CommandText = "Delete from  [SaMa_Par_Adr] WHERE [Par_Adr_Code]='" + coaCode + "'";
                myCommand.ExecuteNonQuery();

                myTrans.Commit();
                return true;
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback("DeleteCustomerAddressData");
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
            return true;
        }

        public bool InsertSupplierData(string ConnectionStr, SamaParAccDAO sama, List<CoaAccGroupDAO> grp, bool updateflg, List<AccCoaAnalysis> Analysis, string chkst)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionStr);
            myConnection.Open();

            SqlCommand myCommand = myConnection.CreateCommand();
            SqlTransaction myTrans;

            myTrans = myConnection.BeginTransaction("InsertCustomerData");

            myCommand.Connection = myConnection;
            myCommand.Transaction = myTrans;

            try
            {


                myCommand.CommandText = "delete from AccCoaAnalysis where Gl_coa_code = '" + sama.ParAccCode + "'";

                myCommand.ExecuteNonQuery();


                if (chkst == "Y")
                {

                    foreach (AccCoaAnalysis ana in Analysis)
                    {
                        myCommand.CommandText = "INSERT INTO [AccCoaAnalysis]([Gl_Coa_Code],[COST_ID],[COST_NAME],[LinNo]) VALUES('" + ana.GlCoaCode + "','" + ana.COSTID + "','" + ana.COSTNAME + "'," + ana.LinNo + ")";

                        myCommand.ExecuteNonQuery();
                    }
                }
                DataTable dt = DataProcess.GetData(myCommand, "select * from AccCoaAnalysis where Gl_Coa_Code='" + sama.ParAccCode + "'");

                if (dt.Rows.Count > 0)
                    sama.ParAccAnaReq = "Y";
                else
                    sama.ParAccAnaReq = "N";

                sama.ParAccType = "C";
                sama.ParAccBo = "O";
                sama.ParAccTrnFlag = "N";
                sama.ParAccPerm = 0;
                sama.ParAcctotCr = 0;
                sama.ParAcctotDb = 0;
                sama.ParAccUnPostCr = 0;
                sama.ParAccUpPostDb = 0;
                sama.ParAccBalAmt = 0;
                sama.ParAccBalFlg = "";
                sama.ParAccCurCode = "BDT";
                sama.ParAccBalTeReq = "N";
                sama.ParAccNarAmtReq = "N";
                sama.ParAccNarAmtType = "";
                sama.TFl = "";
                sama.TIn = "0";
                sama.ParAccUpdDate = DateTime.Now.Date;
                if (updateflg)
                {
                    myCommand.CommandText = @"UPDATE [PuMa_Par_Acc]   SET [Par_Acc_Name] = '" + sama.ParAccName + "',[Par_Acc_Sta] = '" + sama.ParAccSta + "',[Par_Acc_Upd_DATE] = Convert(datetime,'" + sama.ParAccUpdDate + "',103),[Par_Acc_Comm] = '" + sama.ParAccComm + "',[Par_Acc_Perm] = '" + sama.ParAccPerm + "',[Par_Acc_Ana_Req] = '" + sama.ParAccAnaReq + "' ,[T_C2] = '" + sama.TC2 + "',[T_C1] = '" + sama.TC1 + "' WHERE Par_Acc_Code='" + sama.ParAccCode + "' ";
                    myCommand.ExecuteNonQuery();
                }
                else
                {
                    myCommand.CommandText = @"INSERT INTO [PuMa_Par_Acc]([Par_Acc_Code],[Par_Acc_Name],
                                            [Par_Acc_Sec_Code],[Par_Acc_Type],[Par_Acc_Bo],
                                            [Par_Acc_Sta],[Par_Acc_Trn_DATE],[Par_Acc_Trn_Flag],
                                            [Par_Acc_Upd_DATE],[Par_Acc_Comm],[Par_Acc_Perm],
                                            [Par_Acc_Tot_Cr],[Par_Acc_Tot_Db],[Par_Acc_Unpost_Cr],
                                            [Par_Acc_Unpost_Db],[Par_Acc_Bal_Amt],[Par_Acc_Bal_Flag],
                                            [Par_Acc_Cur_Code],[Par_Acc_Ana_Req],[Par_Acc_Bal_Te_Req],
                                            [Par_Acc_Nar_Amt_Req],[Par_Acc_Nar_Amt_Type],[T_C1],
                                            [T_C2],[T_Fl],[T_In]) VALUES(" +
                                            "'" + sama.ParAccCode + "','" + sama.ParAccName + "'," +
                                            "'" + sama.ParAccSecCode + "','" + sama.ParAccType + "','" + sama.ParAccBo + "'," +
                                            "'" + sama.ParAccSta + "',null,'" + sama.ParAccTrnFlag + "'," +
                                            "Convert(datetime,'" + sama.ParAccUpdDate + "',103),'" + sama.ParAccComm + "','" + sama.ParAccPerm + "'," +
                                            "" + sama.ParAcctotCr + "," + sama.ParAcctotDb + "," + sama.ParAccUnPostCr + "," +
                                            "" + sama.ParAccUpPostDb + "," + sama.ParAccBalAmt + ",'" + sama.ParAccBalFlg + "'," +
                                            "'" + sama.ParAccCurCode + "','" + sama.ParAccAnaReq + "','" + sama.ParAccBalTeReq + "'," +
                                            "'" + sama.ParAccNarAmtReq + "','" + sama.ParAccNarAmtType + "','" + sama.TC1 + "'," +
                                            "'" + sama.TC2 + "','" + sama.TFl + "','" + sama.TIn + "')";
                    myCommand.ExecuteNonQuery();
                }

                foreach (CoaAccGroupDAO cg in grp)
                {
                    myCommand.CommandText = @"Delete from [PuMa_Par_Acc_Grp] where Par_Grp_Acc_Code='" + cg.CoaCode + "' and Par_Grp_Id='" + cg.CoaGroupID + "'";
                    myCommand.ExecuteNonQuery();

                    myCommand.CommandText = @"INSERT INTO [PuMa_Par_Acc_Grp](Par_Grp_Acc_Code,Par_Grp_Id,Par_Grp_Code) values('" + cg.CoaCode + "','" + cg.CoaGroupID + "','" + cg.CoaGroupCode + "')";
                    myCommand.ExecuteNonQuery();
                }

                myCommand.CommandText = "delete from CoaGrp where CoaCode = '" + sama.ParAccCode + "'";
                myCommand.ExecuteNonQuery();

                if (grp.Count > 0)
                {
                    string str = "INSERT INTO [CoaGrp]([CoaCode],[Grp],[Grp1],[Grp2],[Grp3]) VALUES('" + grp[0].CoaCode + "',1,'" + grp[0].CoaGroupCodeName + "','" + grp[1].CoaGroupCodeName + "','" + grp[2].CoaGroupCodeName + "')";
                    myCommand.CommandText = str;
                    myCommand.ExecuteNonQuery();
                }

                myTrans.Commit();
                return true;
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback("InsertCustomerData");
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

            return true;
        }
    }
