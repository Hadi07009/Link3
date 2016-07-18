using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TransactionDetailsDAO
    {
       public TransactionDetailsDAO() { }
        // Table Name : AccTransactionDetails
        private string trnrefno;
        private string trnaccode;
        private float trnjrncode;
        private string trnlineno;
        private string trnnarration;
        private string trntrntype;
        private double trnamount;
        private string trnmatch;
        private string trnchequeno;
        private string trnallocateflag;
        private string trnrevalflag;
        private DateTime trnpaymentdate;
        private string trnacdesc;
        private string trnactype;
        private string trnbusflag;
        private DateTime trnduedate;
        private string trnsecno;
        private string trnadrcode;
        private string trndcno;
        private string trngrnno;
        private string trnbankrecoflag;
        private int trnsubno;
        private int trntotint;
        private string trnassetyn;

        public string TrnRefNo
        {
            get { return trnrefno; }
            set { trnrefno = value; }
        }
        

        public string TrnAcCode
        {
            get { return trnaccode; }
            set { trnaccode = value; }
        }
       

        public float TrnJrnCode
        {
            get { return trnjrncode; }
            set { trnjrncode = value; }
        }
        

        public string TrnLineNo
        {
            get { return trnlineno; }
            set { trnlineno = value; }
        }
        

        public string TrnNarration
        {
            get { return trnnarration; }
            set { trnnarration = value; }
        }
       

        public string TrnTrntype
        {
            get { return trntrntype; }
            set { trntrntype = value; }
        }
       

        public Double TrnAmount
        {
            get { return trnamount; }
            set { trnamount = value; }
        }
       

        public string TrnMatch
        {
            get { return trnmatch; }
            set { trnmatch = value; }
        }
      

        public string TrnChequeNo
        {
            get { return trnchequeno; }
            set { trnchequeno = value; }
        }
       

        public string TrnAllocateFlag
        {
            get { return trnallocateflag; }
            set { trnallocateflag = value; }
        }
        

        public string TrnRevalFlag
        {
            get { return trnrevalflag; }
            set { trnrevalflag = value; }
        }
       

        public DateTime TrnPaymentDATE
        {
            get { return trnpaymentdate; }
            set { trnpaymentdate = value; }
        }
      

        public string TrnAcDesc
        {
            get { return trnacdesc; }
            set { trnacdesc = value; }
        }
        

        public string TrnAcType
        {
            get { return trnactype; }
            set { trnactype = value; }
        }
        

        public string TrnBusFlag
        {
            get { return trnbusflag; }
            set { trnbusflag = value; }
        }
       

        public DateTime TrnDueDATE
        {
            get { return trnduedate; }
            set { trnduedate = value; }
        }
        

        public string TrnSecNo
        {
            get { return trnsecno; }
            set { trnsecno = value; }
        }
        

        public string TrnAdrCode
        {
            get { return trnadrcode; }
            set { trnadrcode = value; }
        }
        

        public string TrnDcNo
        {
            get { return trndcno; }
            set { trndcno = value; }
        }
      

        public string TrnGRNNo
        {
            get { return trngrnno; }
            set { trngrnno = value; }
        }
        

        public string TrnBankRecoFlag
        {
            get { return trnbankrecoflag; }
            set { trnbankrecoflag = value; }
        }
        

        public int TrnSubNo
        {
            get { return trnsubno; }
            set { trnsubno = value; }
        }
     

        public int TrnTotInt
        {
            get { return trntotint; }
            set { trntotint = value; }
        }
       

        public string TrnAssetYN
        {
            get { return trnassetyn; }
            set { trnassetyn = value; }
        }
        private string instrumentNumber;

        public string InstrumentType
        {
            get { return instrumentNumber; }
            set { instrumentNumber = value; }
        }
        private DateTime trnChequeDate;

        public DateTime TrnChequeDate
        {
            get { return trnChequeDate; }
            set { trnChequeDate = value; }
        }

       // Receive Cheque Information

        private string trnbankname;
        private string trnbranchname;
        private string trnaccountno;
        private DateTime trnchequedate;

      
        public string RtrnBankName
        {
            get { return trnbankname; }
            set { trnbankname = value; }
        }


        public string RtrnBranchName
        {
            get { return trnbranchname; }
            set { trnbranchname = value; }
        }


        public string RtrnAccountNo
        {
            get { return trnaccountno; }
            set { trnaccountno = value; }
        }


        public DateTime RtrnChequeDATE
        {
            get { return trnchequedate; }
            set { trnchequedate = value; }
        }

        #region Analysis 

       
        private string trnanaGroupdefinationcode1;
        private string trnanagroupdefinationcode2;
        private string trnanagroupdefinationcode3;
        private string trnanagroupdefinationcode4;
        private string trnanagroupdefinationcode5;
        private string trnanagrouplabelcode1;
        private string trnanagrouplabelcode2;
        private string trnanagrouplabelcode3;
        private string trnanagrouplabelcode4;
        private string trnanagrouplabelcode5;

        public string TrnAnaGroupDefinationCode1
        {
            get { return trnanaGroupdefinationcode1; }
            set { trnanaGroupdefinationcode1 = value; }
        }


        public string TrnAnaGroupDefinationCode2
        {
            get { return trnanagroupdefinationcode2; }
            set { trnanagroupdefinationcode2 = value; }
        }


        public string TrnAnaGroupDefinationCode3
        {
            get { return trnanagroupdefinationcode3; }
            set { trnanagroupdefinationcode3 = value; }
        }


        public string TrnAnaGroupDefinationCode4
        {
            get { return trnanagroupdefinationcode4; }
            set { trnanagroupdefinationcode4 = value; }
        }


        public string TrnAnaGroupDefinationCode5
        {
            get { return trnanagroupdefinationcode5; }
            set { trnanagroupdefinationcode5 = value; }
        }


        public string TrnAnaGroupLabelCode1
        {
            get { return trnanagrouplabelcode1; }
            set { trnanagrouplabelcode1 = value; }
        }


        public string TrnAnaGroupLabelCode2
        {
            get { return trnanagrouplabelcode2; }
            set { trnanagrouplabelcode2 = value; }
        }


        public string TrnAnaGroupLabelCode3
        {
            get { return trnanagrouplabelcode3; }
            set { trnanagrouplabelcode3 = value; }
        }


        public string TrnAnaGroupLabelCode4
        {
            get { return trnanagrouplabelcode4; }
            set { trnanagrouplabelcode4 = value; }
        }


        public string TrnAnaGroupLabelCode5
        {
            get { return trnanagrouplabelcode5; }
            set { trnanagrouplabelcode5 = value; }
        }

        #endregion

    }

