using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Link3FrameWork;
using LibraryDAL;
using System.Data;
using System.Data.SqlClient;
using LibraryDAL.AccDataSetTableAdapters;
using LibraryDAL.AccDataSet2TableAdapters;
/// <summary>
/// Summary description for clsAccounts
/// </summary>
public class clsAccounts
{
    public clsAccounts()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static string GetReferenceNoForSave(string JrnType, string VoucherType, string ModuleName, string accPeriod, DateTime TransactionDate)
    {

        string ReferenceNo = "", PreFix = "", NextNum = "", month = "", day = "", refType = "", startNum = "";
        AccJournalSetupTableAdapter jour = new AccJournalSetupTableAdapter();
        AccTransactionHeaderHoldTableAdapter wh = new AccTransactionHeaderHoldTableAdapter();

        AccDataSet2.AccJournalSetupDataTable dataTableObj = new AccDataSet2.AccJournalSetupDataTable();
        try
        {
            dataTableObj = jour.GetDataForRef(JrnType, VoucherType, ModuleName);

            if (dataTableObj.Rows.Count > 0)
            {
                PreFix = dataTableObj.Rows[0]["JrnPrefix"].ToString();
                NextNum = dataTableObj.Rows[0]["JrnNextRefNo"].ToString();
                startNum = dataTableObj.Rows[0]["JrnStartRefNo"].ToString();
                month = StringProcess.Left(DateProcess.MonthName(TransactionDate.Month - 1), 3).ToUpper();
                day = StringProcess.addZeroInString(TransactionDate.Day.ToString(), 2, true);
                refType = dataTableObj.Rows[0]["JrnRefNoType"].ToString();




                double maxref = Convert.ToDouble(wh.GetMaxRefS()) + 1;

                ReferenceNo = "S" + PreFix + StringProcess.Right(TransactionDate.Year.ToString(), 2) + month + StringProcess.addZeroInString(Convert.ToString(maxref), 6, true);
            }
        }
        catch (Exception e)
        {

        }
        return ReferenceNo;
    }

    public static string GetReferenceNoForSave(string JrnType, string VoucherType, string ModuleName, string accPeriod, DateTime TransactionDate, double maxref)
    {

        string ReferenceNo = "", PreFix = "", NextNum = "", month = "", day = "", refType = "", startNum = "";
        AccJournalSetupTableAdapter jour = new AccJournalSetupTableAdapter();
        AccTransactionHeaderHoldTableAdapter wh = new AccTransactionHeaderHoldTableAdapter();

        AccDataSet2.AccJournalSetupDataTable dataTableObj = new AccDataSet2.AccJournalSetupDataTable();
        try
        {
            dataTableObj = jour.GetDataForRef(JrnType, VoucherType, ModuleName);

            if (dataTableObj.Rows.Count > 0)
            {
                PreFix = dataTableObj.Rows[0]["JrnPrefix"].ToString();
                NextNum = dataTableObj.Rows[0]["JrnNextRefNo"].ToString();
                startNum = dataTableObj.Rows[0]["JrnStartRefNo"].ToString();
                month = StringProcess.Left(DateProcess.MonthName(TransactionDate.Month - 1), 3).ToUpper();
                day = StringProcess.addZeroInString(TransactionDate.Day.ToString(), 2, true);
                refType = dataTableObj.Rows[0]["JrnRefNoType"].ToString();


                maxref = maxref + 1;

                ReferenceNo = "S" + PreFix + StringProcess.Right(TransactionDate.Year.ToString(), 2) + month + StringProcess.addZeroInString(Convert.ToString(maxref), 6, true);
            }
        }
        catch (Exception e)
        {

        }
        return ReferenceNo;
    }


    public static string GetReferenceNumber(string JrnType, DateTime TransactionDate)
    {
        string ReferenceNo = "";

        GetNewPostedRefTableAdapter getref = new GetNewPostedRefTableAdapter();

        getref.GetData(TransactionDate, JrnType, ref ReferenceNo);

        return ReferenceNo;


    }
    public static string GetReferenceNo(SqlCommand myCommand, string JrnType, string VoucherType, string ModuleName, string accPeriod, DateTime TransactionDate)
    {
        SqlConnection SCFConnection = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["RTDConnectionString"].ToString());
        SCFConnection.Open();

        myCommand = new SqlCommand("", SCFConnection);

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
                                if (refNo.IndexOf('/', 0) != -1)
                                {
                                    string[] rfn = refNo.Split('/');
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
        }
        return ReferenceNo;
    }

    private static string NextNumber(string refNo, bool ex)  // Modify by nazrul
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
            string[] r = rf[rf.Length - 1].Split('/');

            if (r.Length > 1)
            {
                string sbStr = r[r.Length - 1];
                if (sbStr.ToUpper() == "ZZ")
                {
                    newRef = r[0] + "/AA";
                }
                else if (StringProcess.Right(sbStr, 1).ToUpper() == "Z")
                {
                    newRef = r[0] + "/" + Convert.ToChar((Convert.ToChar(StringProcess.Left(sbStr, 1).ToUpper()) + 1)) + 'A';
                }
                else
                {
                    newRef = r[0] + "/" + StringProcess.Left(sbStr, 1).ToUpper() + Convert.ToChar((Convert.ToChar(StringProcess.Right(sbStr, 1).ToUpper()) + 1));
                }

            }
            else
            {
                newRef = r[0] + "/AA";
            }
        }
        return newRef;
    }
}