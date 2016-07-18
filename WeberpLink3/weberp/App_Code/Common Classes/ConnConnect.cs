using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using CrystalDecisions.Shared;

/// <summary>
/// Summary description for ConnConnect
/// </summary>
public class ConnConnect
{
    public ConnConnect()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static void setConnection()
    {
        if (LibraryDAL.Properties.Settings.Default["RTDConnectionString"].ToString() != ConfigurationSettings.AppSettings["RTDConnectionString"].ToString())
        {
            LibraryDAL.Properties.Settings.Default["RTDConnectionString"] = ConfigurationSettings.AppSettings["RTDConnectionString"].ToString();
            LibraryDAL.Properties.Settings.Default.Save();
        }

        if (LibraryDAL.Properties.Settings.Default["SCFConnectionString"].ToString() != ConfigurationSettings.AppSettings["SCFConnectionString"].ToString())
        {
            LibraryDAL.Properties.Settings.Default["SCFConnectionString"] = ConfigurationSettings.AppSettings["SCFConnectionString"].ToString();
            LibraryDAL.Properties.Settings.Default.Save();
        }

        if (LibraryDAL.Properties.Settings.Default["SCBLConnectionString"].ToString() != ConfigurationSettings.AppSettings["SCBLConnectionString"].ToString())
        {
            LibraryDAL.Properties.Settings.Default["SCBLConnectionString"] = ConfigurationSettings.AppSettings["SCBLConnectionString"].ToString();
            LibraryDAL.Properties.Settings.Default.Save();
        }

        if (LibraryDAL.Properties.Settings.Default["WFA2ConnectionString"].ToString() != ConfigurationSettings.AppSettings["WFA2ConnectionString"].ToString())
        {
            LibraryDAL.Properties.Settings.Default["WFA2ConnectionString"] = ConfigurationSettings.AppSettings["WFA2ConnectionString"].ToString();
            LibraryDAL.Properties.Settings.Default.Save();
        }

        if (LibraryDAL.Properties.Settings.Default["LNKConnectionString"].ToString() != ConfigurationSettings.AppSettings["LNKConnectionString"].ToString())
        {
            LibraryDAL.Properties.Settings.Default["LNKConnectionString"] = ConfigurationSettings.AppSettings["LNKConnectionString"].ToString();
            LibraryDAL.Properties.Settings.Default.Save();
        }

        if (LibraryDAL.Properties.Settings.Default["L3TConnectionString"].ToString() != ConfigurationSettings.AppSettings["L3TConnectionString"].ToString())
        {
            LibraryDAL.Properties.Settings.Default["L3TConnectionString"] = ConfigurationSettings.AppSettings["L3TConnectionString"].ToString();
            LibraryDAL.Properties.Settings.Default.Save();
        }

        if (LibraryPF.Properties.Settings.Default["L3TConnectionString"].ToString() != ConfigurationSettings.AppSettings["L3TConnectionString"].ToString())
        {
            LibraryPF.Properties.Settings.Default["L3TConnectionString"] = ConfigurationSettings.AppSettings["L3TConnectionString"].ToString();
            LibraryPF.Properties.Settings.Default.Save();
        }
    }
   
}


