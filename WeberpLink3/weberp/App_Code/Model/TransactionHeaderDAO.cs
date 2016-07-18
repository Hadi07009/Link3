using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TransactionHeaderDAO
    {
       public TransactionHeaderDAO() { }
        //Table Name : AccTransactionHeader

        private string trnrefno;
        private double trnjrncode;
        private string trnjrntype;
        private string trnaccperiod;
        private DateTime trnentrydate;
        private DateTime trndate;
        private string trncurrcode;
        private int trncurrrate;
        private string trnentryuser;
        private string trnlastupdateuser;
        private DateTime trnlastupdatedate;
        private string trnentryflag;
        private string trnreviseflag;
        private string trnreverseflag;
        private string vouchertype;
       

        public string VoucherType
        {
            get { return vouchertype; }
            set { vouchertype = value; }
        }
        private string modulename;

        public string ModuleName
        {
            get { return modulename; }
            set { modulename = value; }
        }        

        public string TrnRefNo
        {
            get { return trnrefno; }
            set { trnrefno = value; }
        }       

        public double TrnJrnCode
        {
            get { return trnjrncode; }
            set { trnjrncode = value; }
        }        

        public string TrnJrnType
        {
            get { return trnjrntype; }
            set { trnjrntype = value; }
        }       

        public string TrnAccPeriod
        {
            get { return trnaccperiod; }
            set { trnaccperiod = value; }
        }   

        public DateTime TrnEntryDATE
        {
            get { return trnentrydate; }
            set { trnentrydate = value; }
        }

        public DateTime TrnDATE
        {
            get { return trndate; }
            set { trndate = value; }
        }       

        public string TrnCurrCode
        {
            get { return trncurrcode; }
            set { trncurrcode = value; }
        }        

        public int TrnCurrRate
        {
            get { return trncurrrate; }
            set { trncurrrate = value; }
        }       

        public string TrnEntryUser
        {
            get { return trnentryuser; }
            set { trnentryuser = value; }
        }       

        public string TrnLastUpdateUser
        {
            get { return trnlastupdateuser; }
            set { trnlastupdateuser = value; }
        }       

        public DateTime TrnLastUpdateDATE
        {
            get { return trnlastupdatedate; }
            set { trnlastupdatedate = value; }
        }       
        public string TrnEntryFlag
        {
            get { return trnentryflag; }
            set { trnentryflag = value; }
        }       
        public string TrnReviseFlag
        {
            get { return trnreviseflag; }
            set { trnreviseflag = value; }
        }        
        public string TrnReverseFlag
        {
            get { return trnreverseflag; }
            set { trnreverseflag = value; }
        }             

    }

