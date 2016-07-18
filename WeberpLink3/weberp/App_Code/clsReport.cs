using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Net.Mail;
using System.Data;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;

/// <summary>
/// 
/// Summary description for clsReport
/// </summary>
public class clsReport
{
    public clsReport()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private string rptfilename;
    private string rptselectionformulla;
    private ConnectionInfo rptconnectioninfo;
    private ParameterFields rptparametersfields;
    private string formulla;


    public String FileName
    {
        get { return rptfilename; }
        set { rptfilename = value; }
    }

    public String SelectionFormulla
    {
        get { return rptselectionformulla; }
        set { rptselectionformulla = value; }
    }

    public String Formulla
    {
        get { return formulla; }
        set { formulla = value; }
    }

    public ConnectionInfo ConnectionInfo
    {
        get { return rptconnectioninfo; }
        set { rptconnectioninfo = value; }
    }

    public ParameterFields ParametersFields
    {
        get { return rptparametersfields; }
        set { rptparametersfields = value; }
    }



}

public  class clsPayslip
{

    public  clsPayslip() { }


    public  void SendEmail()
    {        

        SendPaySlipMail();

    }

    public void AttendanceProcess()
    {

        CallAttendanceProcess();

    }

    private bool CallAttendanceProcess()
    {
        try
        {
            string _connectionString = ConfigurationManager.AppSettings["UbasysConnectionString"];

            ImportDataHeadoffice();
            ImportDataHeadofficePD();

            ImportDataBranchoffice();
            ImportDataBranchofficePD();

            ImportDataBranchofficeExcel();
            

            var spcmd = " EXEC spAttendanceUpdateBySystemCompanyWiseHO 'CEL'";
            StoredProcedureExecutor.StoredProcedureExecuteNonQuery(_connectionString, spcmd);

            //Holidy update 
            DateTime fromdate = Convert.ToDateTime(DateProcess.GetServerDate(_connectionString));
            HolidayDataEntryAllCompany(fromdate);
            
        }
        catch (Exception ex)
        {
           
        }
        return false;
    }

    private void ImportDataHeadoffice()
    {
        string _connectionStringAccessHO = ConfigurationManager.AppSettings["AccessConnectionStringHO"];
        string _connectionString = ConfigurationManager.AppSettings["UbasysConnectionString"];
        DateTime fromdate = Convert.ToDateTime(DateProcess.GetServerDate(_connectionString));               

        string YY = fromdate.Year.ToString();
        string mm = string.Format("{0:00}", fromdate.Month);
        string dbfilename = YY + " " + mm;

        _connectionStringAccessHO = _connectionStringAccessHO.Replace("variabledb", dbfilename);

        var conn = new OleDbConnection(_connectionStringAccessHO);        

        try
        {
            conn.Open();

            ImportUserinfo();

            string deletequery = "delete from CHECKINOUT_TEMP";
            DataProcess.ExecuteQuery(_connectionString, deletequery);

            const string tableName = "NASTANI";
            String query = "select ID,Vreme,'',0,'1','',0,'',0 from [{0}] where year(Vreme)='" + fromdate.Year + "' and month(Vreme)='" + fromdate.Month + "' and day(Vreme)='" + fromdate.Day + "' and ID <> 0 ";

            query = String.Format(query, tableName);
            var ds = new DataSet();

            var da = new OleDbDataAdapter(query, conn);
            da.Fill(ds, tableName);
            //conn.Close();

            foreach (DataTable dataTable in ds.Tables)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    var storedProcedureComandTest = "exec [ImportDataFrom_CHECKINOUT_TEMP] '" + dataRow[0].ToString() + "'," +
                                                    "'" + dataRow[1].ToString() + "'," +
                                                    "'" + dataRow[2].ToString() + "'," +
                                                    "" + dataRow[3].ToString() + "," +
                                                    "'" + dataRow[4].ToString() + "'," +
                                                    "'" + dataRow[5].ToString() + "'," +
                                                    "'" + dataRow[6].ToString() + "'," +
                                                    "'" + dataRow[7].ToString() + "'," +
                                                    "" + dataRow[8].ToString();
                    StoredProcedureExecutor.StoredProcedureExecuteNonQuery(_connectionString, storedProcedureComandTest);

                }
            }

            string sqlchk = @"Insert into CHECKINOUT
                                select b.EMPCODE,[CHECKTIME],[CHECKTYPE],[VERIFYCODE],[SENSORID],[Memoinfo],[WorkCode],[sn],[UserExtFmt] 
                                from CHECKINOUT_TEMP a inner join NsistemUsers b on a.userid=b.USERCARDID";


            DataProcess.ExecuteQuery(_connectionString, sqlchk);


        }
        catch (OleDbException exp)
        {
           
        }
        catch (Exception exceptionMsg)
        {
           
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

    }
    private void ImportDataHeadofficePD()
    {
        string _connectionStringAccessHO = ConfigurationManager.AppSettings["AccessConnectionStringHO"];
        string _connectionString = ConfigurationManager.AppSettings["UbasysConnectionString"];
        DateTime fromdate = Convert.ToDateTime(DateProcess.GetServerDate(_connectionString)).AddDays(-1);

        string YY = fromdate.Year.ToString();
        string mm = string.Format("{0:00}", fromdate.Month);
        string dbfilename = YY + " " + mm;

        _connectionStringAccessHO = _connectionStringAccessHO.Replace("variabledb", dbfilename);

        var conn = new OleDbConnection(_connectionStringAccessHO);

        try
        {
            conn.Open();

            ImportUserinfo();

            string deletequery = "delete from CHECKINOUT_TEMP";
            DataProcess.ExecuteQuery(_connectionString, deletequery);

            const string tableName = "NASTANI";
            String query = "select ID,Vreme,'',0,'1','',0,'',0 from [{0}] where year(Vreme)='" + fromdate.Year + "' and month(Vreme)='" + fromdate.Month + "' and day(Vreme)='" + fromdate.Day + "' and ID <> 0 ";

            query = String.Format(query, tableName);
            var ds = new DataSet();

            var da = new OleDbDataAdapter(query, conn);
            da.Fill(ds, tableName);
            //conn.Close();

            foreach (DataTable dataTable in ds.Tables)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    var storedProcedureComandTest = "exec [ImportDataFrom_CHECKINOUT_TEMP] '" + dataRow[0].ToString() + "'," +
                                                    "'" + dataRow[1].ToString() + "'," +
                                                    "'" + dataRow[2].ToString() + "'," +
                                                    "" + dataRow[3].ToString() + "," +
                                                    "'" + dataRow[4].ToString() + "'," +
                                                    "'" + dataRow[5].ToString() + "'," +
                                                    "'" + dataRow[6].ToString() + "'," +
                                                    "'" + dataRow[7].ToString() + "'," +
                                                    "" + dataRow[8].ToString();
                    StoredProcedureExecutor.StoredProcedureExecuteNonQuery(_connectionString, storedProcedureComandTest);

                }
            }

            string sqlchk = @"Insert into CHECKINOUT
                                select b.EMPCODE,[CHECKTIME],[CHECKTYPE],[VERIFYCODE],[SENSORID],[Memoinfo],[WorkCode],[sn],[UserExtFmt] 
                                from CHECKINOUT_TEMP a inner join NsistemUsers b on a.userid=b.USERCARDID";


            DataProcess.ExecuteQuery(_connectionString, sqlchk);


        }
        catch (OleDbException exp)
        {

        }
        catch (Exception exceptionMsg)
        {

        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

    }
    private string ImportUserinfo()
    {       
        string msg = "";
        string _connectionString = ConfigurationManager.AppSettings["UbasysConnectionString"];
        string _connectionStringAccessUser = ConfigurationManager.AppSettings["AccessConnectionStringUser"];
        var connM = new OleDbConnection(_connectionStringAccessUser);
        try
        {           
           
            connM.Open();



            DateTime fromdate = Convert.ToDateTime(DateProcess.GetServerDate(_connectionString));
            const string tableNameM = "users";
            String queryM = "select RedBr,Name,ID,Detail1 from [{0}] ";

            queryM = String.Format(queryM, tableNameM);
            var dsM = new DataSet();
            var daM = new OleDbDataAdapter(queryM, connM);
            daM.Fill(dsM, tableNameM);

            foreach (DataTable dataTable in dsM.Tables)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    var storedProcedureComandTest = "exec [ImportDataFrom_NsistemUsers] '" + dataRow[0].ToString() + "'," +
                                                    "'" + dataRow[1].ToString() + "'," +
                                                    "'" + dataRow[2].ToString() + "'," +
                                                    "'" + dataRow[3].ToString() + "'";
                    StoredProcedureExecutor.StoredProcedureExecuteNonQuery(_connectionString, storedProcedureComandTest);

                }
            }           

        }
        catch (OleDbException exp)
        {
           
        }
        catch (Exception exceptionMsg)
        {
           
        }
        finally
        {
            if (connM.State == ConnectionState.Open)
            {
                connM.Close();
            }
        }

        return msg;

    }
    private void ImportDataBranchoffice()
    {
        string _connectionString = ConfigurationManager.AppSettings["UbasysConnectionString"];
        string _connectionStringAccessAushulia = ConfigurationManager.AppSettings["AccessConnectionStringAushulia"];
        var conn = new OleDbConnection(_connectionStringAccessAushulia);
        

        try
        {
            conn.Open();

            DateTime fromdate = Convert.ToDateTime(DateProcess.GetServerDate(_connectionString));
            const string tableName = "CHECKINOUT";
            String query = "select a.*,b.SSN from [{0}] a inner join USERINFO b on b.USERID=a.USERID  where year(CHECKTIME)='" + fromdate.Year + "' and month(CHECKTIME)='" + fromdate.Month + "' and day(CHECKTIME)='" + fromdate.Day + "'";

            query = String.Format(query, tableName);
            var ds = new DataSet();

            var da = new OleDbDataAdapter(query, conn);
            da.Fill(ds, tableName);
            conn.Close();

            foreach (DataTable dataTable in ds.Tables)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    var storedProcedureComandTest = "exec [ImportDataFrom_CHECKINOUT] '" + dataRow[9].ToString() + "'," +
                                                    "'" + dataRow[1].ToString() + "'," +
                                                    "'" + dataRow[2].ToString() + "'," +
                                                    "" + dataRow[3].ToString() + "," +
                                                    "'" + dataRow[4].ToString() + "'," +
                                                    "'" + dataRow[5].ToString() + "'," +
                                                    "'" + dataRow[6].ToString() + "'," +
                                                    "'" + dataRow[7].ToString() + "'," +
                                                    "" + dataRow[8].ToString();
                    StoredProcedureExecutor.StoredProcedureExecuteNonQuery(_connectionString, storedProcedureComandTest);

                }
            }

        }
        catch (OleDbException exp)
        {
            
        }
        catch (Exception exceptionMsg)
        {
           
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
    private void ImportDataBranchofficePD()
    {
        string _connectionString = ConfigurationManager.AppSettings["UbasysConnectionString"];
        string _connectionStringAccessAushulia = ConfigurationManager.AppSettings["AccessConnectionStringAushulia"];
        var conn = new OleDbConnection(_connectionStringAccessAushulia);


        try
        {
            conn.Open();

            DateTime fromdate = Convert.ToDateTime(DateProcess.GetServerDate(_connectionString)).AddDays(-1);
            const string tableName = "CHECKINOUT";
            String query = "select a.*,b.SSN from [{0}] a inner join USERINFO b on b.USERID=a.USERID  where year(CHECKTIME)='" + fromdate.Year + "' and month(CHECKTIME)='" + fromdate.Month + "' and day(CHECKTIME)='" + fromdate.Day + "'";

            query = String.Format(query, tableName);
            var ds = new DataSet();

            var da = new OleDbDataAdapter(query, conn);
            da.Fill(ds, tableName);
            conn.Close();

            foreach (DataTable dataTable in ds.Tables)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    var storedProcedureComandTest = "exec [ImportDataFrom_CHECKINOUT] '" + dataRow[9].ToString() + "'," +
                                                    "'" + dataRow[1].ToString() + "'," +
                                                    "'" + dataRow[2].ToString() + "'," +
                                                    "" + dataRow[3].ToString() + "," +
                                                    "'" + dataRow[4].ToString() + "'," +
                                                    "'" + dataRow[5].ToString() + "'," +
                                                    "'" + dataRow[6].ToString() + "'," +
                                                    "'" + dataRow[7].ToString() + "'," +
                                                    "" + dataRow[8].ToString();
                    StoredProcedureExecutor.StoredProcedureExecuteNonQuery(_connectionString, storedProcedureComandTest);

                }
            }

        }
        catch (OleDbException exp)
        {

        }
        catch (Exception exceptionMsg)
        {

        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
    private void ImportDataBranchofficeExcel()
    {
        try
        {
            string _connectionStringExcel = ConfigurationManager.ConnectionStrings["Excel07ConStringFixed"].ConnectionString;
            string _connectionString = ConfigurationManager.AppSettings["UbasysConnectionString"];

            var sheetName = "Sheet1$";
            CommonMethods objCommonMethods = new CommonMethods();
            DataTable dtFromExcel = objCommonMethods.GetDataFromExcel(_connectionStringExcel, sheetName);
            CheckinDataController objCheckinDataController = new CheckinDataController();
            foreach (DataRow dtRow in dtFromExcel.Rows)
            {
                CheckinData objCheckInData = new CheckinData();
                objCheckInData.EmployeeCode = dtRow[1].ToString();
                if (dtRow[2].ToString() != "")
                {
                    objCheckInData.CheckinDate = Convert.ToDateTime((Convert.ToDateTime(dtRow[0]).ToString("dd-MM-yyyy") + " " + dtRow[2].ToString()));
                    objCheckinDataController.Save(_connectionString, objCheckInData);
                }
                if (dtRow[3].ToString() != "")
                {
                    objCheckInData.CheckinDate = Convert.ToDateTime((Convert.ToDateTime(dtRow[0]).ToString("dd-MM-yyyy") + " " + dtRow[3].ToString()));
                    objCheckinDataController.Save(_connectionString, objCheckInData);
                }
            }
            objCheckinDataController.AttendanceUpdate(_connectionString);
        }
        catch (Exception msgException)
        {

        }
        finally
        { 
        }
    }

 
    private int HolidayDataEntryAllCompany(DateTime fromdate)
    {
        int rt = 1;
        try
        {
            string _connectionString = ConfigurationManager.AppSettings["UbasysConnectionString"];

            var storedProcedureComandTest = "exec [spHolidayUpdateBySystem] '" + fromdate.ToString() + "'";
            StoredProcedureExecutor.StoredProcedureExecuteNonQuery(_connectionString, storedProcedureComandTest);
            rt = 0;
        }
        catch (Exception ex)
        {
            return rt;
        }

        return rt;

    }


    private bool SendPaySlipMail()
    {
        try
        {           

            string sid = ""; //"n.islam@link3.net"; //dts.Rows[0]["Emp_Mas_Remarks"].ToString();
            string sname =""; //dts.Rows[0]["EmpName"].ToString();
            string rid = ""; //dtr.Rows[0]["Emp_Mas_Remarks"].ToString();
            string rname = "";// dtr.Rows[0]["EmpName"].ToString();
            string msub = "Pay Slip";
            string msgbody = "Pay Slip";
            string atn = "Dear Mr/Ms " + rname + ",";
            string mbody = "Test basis please ignore it";
            string ccid = "";

            Sendmail(sid, sname, rid, rname, ccid, msub, mbody, "", "");

            //
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return false;
    }

    private bool Sendmail(string sid, string sname, string rid, string rname, string ccid, string msub, string mbody, string empcode, string emial)
    {
        bool flg = false;

        string UbasysConnectionStr = System.Configuration.ConfigurationManager.AppSettings["UBASYSConnectionString"].ToString();
        string ConnectionStr = "";

        string email = "";
        string companycode = "";
        string companyname = "";
        string companaddress = "";
        int salmonth = 0;
        int salyear = 0;
        string accountNumber = "";
        string filename="";
        string empname = "";
        string Bankname = "";
        DateTime Lvfrom;
        DateTime Lvto; ;
        int flag = 0;
        string smtpadr = "";
        string password = "";

        ReportDocument crystalReport = new ReportDocument();
        
        DataTable dt = new DataTable();
        string sql = "select MailFrom,Name,Password,Smtp,Subject,Body from TblSmtpSetup where status=1";
        dt = DataProcess.GetData(UbasysConnectionStr, sql);
        if (dt.Rows.Count > 0)
        {
            sid = dt.Rows[0]["MailFrom"].ToString();
            sname = dt.Rows[0]["Name"].ToString();
            smtpadr = dt.Rows[0]["Smtp"].ToString();
            password = dt.Rows[0]["Password"].ToString();
        }
               

        DataTable dtsend = new DataTable();

        sql = "select employeeid,emailaddress,companyname,companydisplayname,companyaddress,connectionString,salmonth,salyear,AccountNumber,empname,BankName as Bankname,Flag from tbl_email_notification where Status=1 and emailaddress is not null and emailaddress<>'' and Tryno<5  order by CompanyName,deptid,employeeid";

        dt = DataProcess.GetData(UbasysConnectionStr, sql);

        foreach (DataRow dr in dt.Rows)
        {
            try
            {
                crystalReport = new ReportDocument();

                empcode = dr["employeeid"].ToString();
                email = dr["emailaddress"].ToString();
                companycode = dr["companyname"].ToString();
                companyname = dr["companydisplayname"].ToString();
                companaddress = dr["companyaddress"].ToString();
                ConnectionStr = dr["connectionString"].ToString();
                salmonth = Convert.ToInt32(dr["salmonth"].ToString());
                salyear = Convert.ToInt32(dr["salyear"].ToString());
                accountNumber = dr["AccountNumber"].ToString();
                Bankname = dr["Bankname"].ToString();
                empname = dr["empname"].ToString();
                filename = empcode + ".pdf";
                flag = Convert.ToInt32(dr["Flag"].ToString());                
                msub = "Pay slip" + " of " + empcode + " for " + SalPeriod(salmonth, salyear);
                mbody = MailBody(empname, SalPeriod(salmonth, salyear));
                                
                rid = email;

                if (flag == 50)
                {
                    DataProcess.ExecuteQuery(UbasysConnectionStr, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[View_Hrmssalary]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[View_Hrmssalary]");
                    sql = "create view View_Hrmssalary as select a.*,b.*,c.emp_mas_join_date,'" + companycode + "' as CompanyCode,'" + companyname + "' as CompanyName,'" + companaddress + "' as CompanyAddress,'" + accountNumber + "' as AccountNumber,'" + Bankname + "' as Bankname," + flag + " as flag"
                            + " from hrms_salary a"
                            + " inner join Emp_Details b on a.Empcode=b.EmpID"
                            + " inner join hrms_emp_mas c on c.emp_mas_emp_id=a.Empcode"
                            + " where empcode='" + empcode + "' and month(a.salmonth)=" + salmonth + " and year(a.salmonth)=" + salyear + " and salgrade='50'";


                }
                else
                {
                    DataProcess.ExecuteQuery(UbasysConnectionStr, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[View_Hrmssalary]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[View_Hrmssalary]");
                    sql = "create view View_Hrmssalary as select a.*,b.*,c.emp_mas_join_date,'" + companycode + "' as CompanyCode,'" + companyname + "' as CompanyName,'" + companaddress + "' as CompanyAddress,'" + accountNumber + "' as AccountNumber,'" + Bankname + "' as Bankname," + flag + " as flag"
                            + " from hrms_salary a"
                            + " inner join Emp_Details b on a.Empcode=b.EmpID"
                            + " inner join hrms_emp_mas c on c.emp_mas_emp_id=a.Empcode"
                            + " where empcode='" + empcode + "' and month(a.salmonth)=" + salmonth + " and year(a.salmonth)=" + salyear + " and salgrade<>'50'";

                    
                }
              
                DataProcess.ExecuteQuery(UbasysConnectionStr, sql);
                
                DataTable dsCustomers = new DataTable();

                dsCustomers = DataProcess.GetData(UbasysConnectionStr, "select * from View_Hrmssalary where empcode='" + empcode + "' and Flag='" + flag + "'");

                if (flag == 50)
                    crystalReport.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/modules/HRMS/Reports/emailHrmsEmpBonusslip.rpt"));
                else
                    crystalReport.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/modules/HRMS/Reports/emailHrmsEmpPayslip.rpt"));
               
                crystalReport.SetDataSource(dsCustomers);

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(smtpadr);
                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

                msg.From = new System.Net.Mail.MailAddress(sid, sname);
                msg.To.Add(new System.Net.Mail.MailAddress(rid));

                string[] cary = ccid.Split(':');

                if (cary.Length >= 1)
                {
                    for (int i = 0; i < cary.Length; i++)
                    {
                        if (cary[i].Trim().ToString() != "")
                        {
                            msg.CC.Add(new System.Net.Mail.MailAddress(cary[i].Trim()));
                        }
                    }
                }

                msg.Subject = msub;
                msg.Body = mbody;
                msg.Attachments.Add(new Attachment(crystalReport.ExportToStream(ExportFormatType.PortableDocFormat), filename));

                dtsend = DataProcess.GetData(UbasysConnectionStr, "select * from [tbl_email_notification] where employeeid='" + empcode + "' and status=1 and salmonth=" + salmonth + " and salyear=" + salyear + " and flag='"+ flag +"'");

                if (dtsend.Rows.Count > 0)
                {
                    if (dsCustomers.Rows.Count > 0)
                    {
                        smtp.Send(msg);
                        DataProcess.ExecuteQuery(UbasysConnectionStr, "update [tbl_email_notification] set status=0,Senddatetime=getdate() where employeeid='" + empcode + "' and status=1 and salmonth=" + salmonth + " and salyear=" + salyear + " and Flag='" + flag +"'");
                    }
                }

                flg = true;
               
            }
            catch (Exception ec)
            {
                //DataProcess.ExecuteQuery(UbasysConnectionStr, "insert into UBASYS.dbo.[tbl_email_log] (er_date, er_msg) values (getdate(),'" + ec.Message + "')");
                DataProcess.ExecuteQuery(UbasysConnectionStr, "update [tbl_email_notification] set Tryno=Tryno+1,Errormessage='" + ec.Message + "' where employeeid='" + empcode + "' and status=1 and salmonth=" + salmonth + " and salyear=" + salyear + " and Flag='" + flag + "'");
                flg = false ;
            }
            finally
            {
                crystalReport.Close();
                crystalReport.Dispose();
                GC.Collect();
            }
        

        } 
 
        
        
        return flg;
                
    }

    private string SalPeriod(int salmonth,int salyear)
    {
        string SalPeriod="";

        if (salmonth == 1)
            SalPeriod = "January" + " " + salyear.ToString();
        if (salmonth == 2)
            SalPeriod = "February" + " " + salyear.ToString();
        if (salmonth == 3)
            SalPeriod = "March" + " " + salyear.ToString();
        if (salmonth == 4)
            SalPeriod = "April" + " " + salyear.ToString();
        if (salmonth == 5)
            SalPeriod = "May" + " " + salyear.ToString();
        if (salmonth == 6)
            SalPeriod = "June" + " " + salyear.ToString();
        if (salmonth == 7)
            SalPeriod = "July" + " " + salyear.ToString();
        if (salmonth == 8)
            SalPeriod = "August" + " " + salyear.ToString();
        if (salmonth == 9)
            SalPeriod = "September" + " " + salyear.ToString();
        if (salmonth == 10)
            SalPeriod = "October" + " " + salyear.ToString();
        if (salmonth == 11)
            SalPeriod = "November" + " " + salyear.ToString();
        if (salmonth == 12)
            SalPeriod = "December" + " " + salyear.ToString();
                
        return SalPeriod;
    }

    private string[] leavedate(int salmonth,int salyear)
    {
        string[] st = new string[2];
        DateTime dt1=System.DateTime.Now;
        DateTime dt2 = System.DateTime.Now; 
        int dd;
        int mm;
        int yyyy;

        if (salmonth == 1)
        {
            dd = 26;
            mm = 12;
            yyyy = salyear - 1;
            dt1 = Convert.ToDateTime(dd.ToString() + "/" + string.Format("{0:00}", mm) + "/" + yyyy.ToString());

            dd = 25;
            mm = salmonth;
            yyyy = salyear;
            dt2 = Convert.ToDateTime(dd.ToString() + "/" + string.Format("{0:00}", mm) + "/" + yyyy.ToString());
        }
        else
        {
            dd = 26;
            mm = salmonth-1;
            yyyy = salyear;
            dt1 = Convert.ToDateTime(dd.ToString() + "/" + string.Format("{0:00}", mm) + "/" + yyyy.ToString());

            dd = 25;
            mm = salmonth;
            yyyy = salyear;
            dt2 = Convert.ToDateTime(dd.ToString() + "/" + string.Format("{0:00}", mm) + "/" + yyyy.ToString());
 
        }

        st[0] = dt1.ToShortDateString();
        st[1] = dt2.ToShortDateString();

        return st;

 
    }

    private string MailBody(string empname,string Period)
    {            
        string str = "";

        str = "\n" + "Dear Mr/Ms " + empname+",";
        str += "\n\n" + "Please find the pay slip for the month of " + Period+" as attachment";
        str += "\n\n\n\n\n" + "Note: This is a auto generated report and does not require any signature. Please do not reply this email.";

        return str;
    }

}