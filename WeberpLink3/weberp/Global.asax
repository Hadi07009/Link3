<%@ Application Language="C#" %>
<%@ Import Namespace="System.Threading" %>

<script runat="server">

    void Application_Start(Object sender, EventArgs e) {
        // Code that runs on application startup
        //Thread thread = new Thread(new ThreadStart(ThreadFunc));
        //thread.IsBackground = true;
        //thread.Name = "ThreadFunc";
        //thread.Start();
    }

    protected void ThreadFunc()
    {
        System.Timers.Timer t = new System.Timers.Timer();
        t.Elapsed += new System.Timers.ElapsedEventHandler(TimerWorker);
        t.Interval = 60000;
        t.Enabled = true;
        t.AutoReset = true;
        t.Start();
        
    }

    protected void TimerWorker(object sender, System.Timers.ElapsedEventArgs e)
    {
        string UbasysConnectionStr = System.Configuration.ConfigurationManager.AppSettings["UBASYSConnectionString"].ToString();

        clsPayslip ps = new clsPayslip();

        int flag = 0;
        int flag2 = 0;
        int flag3 = 0;
        
               
        
        
        try
        {
            flag =Convert.ToInt32(DataProcess.GetData(UbasysConnectionStr, "select isnull(sum(begin_flag),0) as begin_flag from tbl_process_flag").Rows[0]["begin_flag"].ToString());

           
        }
        catch
        {
            flag = 0;
        }     
        
         
                
        if (flag == 0)
        {
            if (DataProcess.ExecuteQuery(UbasysConnectionStr, "update tbl_process_flag set begin_flag=1"))
            {
                try
                {
                    //ps.SendEmail();

                    DateTime serverdate1 = DateProcess.GetServerDate(UbasysConnectionStr).Date;
                    string servertime1 = DateProcess.GetServerTime(UbasysConnectionStr);
                    string sql1 = @"select isnull(SUM(ProcessFlag),0) as ProcessFlag from [TblTriggerProcessStatus]
                           where DATEDIFF(HH,convert(Datetime,ProcessDate+' '+TriggerTime,103),CONVERT(datetime,'" + serverdate1 + "',103)+' ' + '" + servertime1 + "')>[Duration]";

                    flag2 = (int)DataProcess.GetData(UbasysConnectionStr, sql1).Rows[0][0];
                    
                    
                    if (flag2 >= 1)
                    {
                        ps.AttendanceProcess();
                      
                        DateTime serverdate = DateProcess.GetServerDate(UbasysConnectionStr).Date;
                        string servertime = DateProcess.GetServerTime(UbasysConnectionStr);

                        string tsql = "";
                        string sql = @"select isnull(SUM(ProcessFlag),0) as ProcessFlag from [TblTriggerProcessStatus] where ProcessDate=CONVERT(Datetime,'" + serverdate + "',103)";
                        flag3 = (int)DataProcess.GetData(UbasysConnectionStr, sql).Rows[0][0];
                        if (flag3 > 0)
                        {
                            sql = "update [TblTriggerProcessStatus] set TriggerTime='" + servertime + "' where ProcessDate=CONVERT(datetime,'" + serverdate + "',103)";
                            DataProcess.ExecuteQuery(UbasysConnectionStr, sql);
                        }
                        else
                        {
                            tsql = @"select isnull(SUM(TriggerDuration),1) as TriggerDuration from [TblTriggerDuration]";
                            int flag4 = (int)DataProcess.GetData(UbasysConnectionStr, tsql).Rows[0][0];

                            sql = @"delete from [TblTriggerProcessStatus] where ProcessDate=CONVERT(Datetime,'" + serverdate + "',103)";
                            DataProcess.ExecuteQuery(UbasysConnectionStr, sql); 
                            
                            sql = @"insert into TblTriggerProcessStatus([ProcessDate],[TriggerTime],[ProcessFlag],[Duration],[Status])
                                        values(CONVERT(datetime,'" + serverdate + "',103),'" + servertime + "','1'," + flag4 + ",'1')";
                            DataProcess.ExecuteQuery(UbasysConnectionStr, sql);                             
                        }
                    }
                }
                catch
                {
                    DataProcess.ExecuteQuery(UbasysConnectionStr, "update tbl_process_flag set begin_flag=0");                        
                }
            }
        }

        DataProcess.ExecuteQuery(UbasysConnectionStr, "update tbl_process_flag set begin_flag=0");                 
       
    }
    
    void Application_End(Object sender, EventArgs e) {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(Object sender, EventArgs e) { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(Object sender, EventArgs e) {
        // Code that runs when a new session is started

    }

    void Session_End(Object sender, EventArgs e) {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    void Application_AuthenticateRequest(object sender, EventArgs e)
    {
       
    }

    
</script>
