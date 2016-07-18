using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
//using LibraryApproval;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;



namespace LibraryDAL
{
    public class ConfigureSheetDTL
    {
        public ConfigureSheetDTL()
        {

        }
       
        private DateTime datefrom;
        private DateTime dateto;

        public DateTime DateFrom
        {
            get{return datefrom;}
            set { datefrom = value; }
        }
        public DateTime DateTo
        {
            get { return dateto; }
            set { dateto = value; }
        }
    }

    public class clsPrmInfo
    {
        public clsPrmInfo(){}

        private DateTime fromdate;
        private DateTime todate;
        private string query;
        private string multiquery;
        private string reportname;
        private string reportTytle;

       

        public String Query
        {
            get { return query; }
            set { query = value; }

        }
        public String MultiQuery
        {
            get { return multiquery; }
            set { multiquery = value; }

        }
        public DateTime Fromdate
        {
            get { return fromdate; }
            set { fromdate = value; }
        }

        public DateTime Todate
        {
            get { return todate; }
            set { todate = value; }
        }

        public String ReportName
        {
            get { return reportname; }
            set { reportname = value; }
        }

        public string ReportTytle
        {
            get { return reportTytle; }
            set { reportTytle = value; }
        }
       

    }

    //[Serializable] 
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




    public class clsRptReport
    {
        public clsRptReport() { }
        private string rptfilename;
        private string rptselectionformulla;
        private ConnectionInfo rptconnectioninfo;
        private ParameterFields rptparametersfields;
        private int zoom;
        private string formulla;
        private object dt;

        public String Rptselectionformulla
        {
            get { return rptselectionformulla; }
            set { rptselectionformulla = value; }
        }

        public String FileName
        {
            get { return rptfilename; }
            set { rptfilename = value; }
        }


        public ParameterFields ParametersFields
        {
            get { return rptparametersfields; }
            set { rptparametersfields = value; }
        }

        public int Zoom
        {
            get { return zoom; }
            set { zoom = value; }
        }


        public Object DtTbl
        {
            get { return dt; }
            set { dt = value; }
        }

    }

   
    
}
