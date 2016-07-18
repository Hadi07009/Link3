using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class SamaParAccDAO
{
    public SamaParAccDAO(){}


    private string paracccode;
    private string paraccname;
    private string paraccseccode;
    private string paracccomm;
    private string paracctype;
    private string paraccbo;
    private string paraccsta;
    private DateTime paracctrndate;
    private string paraccctrnflag;
    private DateTime paracccupddate;
    private int paracccperm;
    private decimal paracctotcr;
    private decimal paracctotdb;
    private decimal paraccunpostcr;
    private decimal paraccuppostdb;
    private decimal paraccbalamt;
    private string paraccbalflg;
    private string paracccurcode;
    private string paraccanareq;
    private string paraccbaltereq;
    private string paraccnaramtreq;
    private string paraccnaramttype;
    private string tc1;
    private string tc2;
    private string tfl;
    private string tin;

    
    public string ParAccName
    {
        get { return paraccname; }
        set { paraccname = value; }
    }
    public string ParAccSecCode
    {
        get { return paraccseccode; }
        set { paraccseccode = value; }
    }
    public string ParAccType
    {
        get { return paracctype; }
        set { paracctype = value; }
    }

    public string ParAccComm
    {
        get { return paracccomm; }
        set { paracccomm = value; }
    }

    public string ParAccBo
    {
        get { return paraccbo; }
        set { paraccbo = value; }
    }
    public string ParAccSta
    {
        get { return paraccsta; }
        set { paraccsta = value; }
    }
    public DateTime ParAccTrnDate
    {
        get { return paracctrndate; }
        set { paracctrndate = value; }
    }
    public string ParAccTrnFlag
    {
        get { return paraccctrnflag; }
        set { paraccctrnflag = value; }
    }
    public DateTime ParAcccUpdDate
    {
        get { return paracccupddate; }
        set { paracccupddate = value; }
    }
    public int ParAccPerm
    {
        get { return paracccperm; }
        set { paracccperm = value; }
    }
    public decimal ParAcctotCr
    {
        get { return paracctotcr; }
        set { paracctotcr = value; }
    }
    public decimal ParAcctotDb
    {
        get { return paracctotdb; }
        set { paracctotdb = value; }
    }
    public decimal ParAccUnPostCr
    {
        get { return paraccunpostcr; }
        set { paraccunpostcr = value; }
    }
    public decimal ParAccUpPostDb
    {
        get { return paraccuppostdb; }
        set { paraccuppostdb = value; }
    }
    public decimal ParAccBalAmt
    {
        get { return paraccbalamt; }
        set { paraccbalamt = value; }
    }
    public string ParAccBalFlg
    {
        get { return paraccbalflg; }
        set { paraccbalflg = value; }
    }
    public string ParAccCurCode
    {
        get { return paracccurcode; }
        set { paracccurcode = value; }
    }
    public string ParAccAnaReq
    {
        get { return paraccanareq; }
        set { paraccanareq = value; }
    }
    public string ParAccBalTeReq
    {
        get { return paraccbaltereq; }
        set { paraccbaltereq = value; }
    }
    public string ParAccNarAmtReq
    {
        get { return paraccnaramtreq; }
        set { paraccnaramtreq = value; }
    }
    public DateTime ParAccUpdDate
    {
        get { return paracccupddate; }
        set { paracccupddate = value; }
    }
    public string ParAccNarAmtType
    {
        get { return paraccnaramttype; }
        set { paraccnaramttype = value; }
    }
    public string TC1
    {
        get { return tc1; }
        set { tc1 = value; }
    }
    public string TC2
    {
        get { return tc2; }
        set { tc2 = value; }
    }

    public string TFl
    {
        get { return tfl; }
        set { tfl = value; }
    }
    public string TIn
    {
        get { return tin; }
        set { tin = value; }
    }
    public string ParAccCode
    {
        get { return paracccode; }
        set { paracccode = value; }
    }
}

public class CoaAccGroupDAO
{
    public CoaAccGroupDAO() { }
    private string coacode;

    public string CoaCode
    {
        get { return coacode; }
        set { coacode = value; }
    }
    private string coagroupid;

    public string CoaGroupID
    {
        get { return coagroupid; }
        set { coagroupid = value; }
    }
    private string coagroupcode;

    public string CoaGroupCode
    {
        get { return coagroupcode; }
        set { coagroupcode = value; }
    }
    private string coagroupcodeName;

    public string CoaGroupCodeName
    {
        get { return coagroupcodeName; }
        set { coagroupcodeName = value; }
    }
}

public class AccCoaAnalysis
{
    public AccCoaAnalysis() { }
    private string glCoaCode;

    public string GlCoaCode
    {
        get { return glCoaCode; }
        set { glCoaCode = value; }
    }
    private string cOSTID;

    public string COSTID
    {
        get { return cOSTID; }
        set { cOSTID = value; }
    }
    private string cOSTNAME;

    public string COSTNAME
    {
        get { return cOSTNAME; }
        set { cOSTNAME = value; }
    }

    private int linNo;

    public int LinNo
    {
        get { return linNo; }
        set { linNo = value; }
    }

}

public class COADAO
{
    public COADAO() { }
    private int coaid;
    private string glcoacode;
    private string glcoaname;
    private string glcoaseccode;
    private string glcoatype;
    private string glcoabo;
    private string glcoasta;
    private DateTime glcoatrndate;
    private string glcoacheqduete;
    private string glcoacomm;
    private string glcoacurcode;
    private string glcoaupddate;
    private string glcoatrnflg;
    private string glcoacntrlcode;
    private string glcoaassetyn;
    private string glcoaassetonoff;
    private string glcoatrnanareq;
    private string glcoabaltereq;
    private string glcoanaramtreq;
    private string glcoanaramttype;
    private string glcoacrcode;
    private string glcoacompcode;
    private string bankcashtype;
    private int accTypeID;
    private string acctypename;

    public string AccTypeName
    {
        get { return acctypename; }
        set { acctypename = value; }
    }

    public int AccTypeID
    {
        get { return accTypeID; }
        set { accTypeID = value; }
    }


    public string BankCashType
    {
        get { return bankcashtype; }
        set { bankcashtype = value; }
    }

    public int CoaID
    {
        get { return coaid; }
        set { coaid = value; }
    }
    public string GlCoaCode
    {
        get { return glcoacode; }
        set { glcoacode = value; }
    }
    public string GlCoaName
    {
        get { return glcoaname; }
        set { glcoaname = value; }
    }
    public string GlCoaType
    {
        get { return glcoatype; }
        set { glcoatype = value; }
    }
    public string GlCoaBo
    {
        get { return glcoabo; }
        set { glcoabo = value; }
    }
    public string GlCoaComm
    {
        get { return glcoacomm; }
        set { glcoacomm = value; }
    }
    public string GlCoaSta
    {
        get { return glcoasta; }
        set { glcoasta = value; }
    }
    public DateTime GlCoaTrnDATE
    {
        get { return glcoatrndate; }
        set { glcoatrndate = value; }
    }
    public string GlCoaTrnFlg
    {
        get { return glcoatrnflg; }
        set { glcoatrnflg = value; }
    }
    public string GlCoaCntrlCode
    {
        get { return glcoacntrlcode; }
        set { glcoacntrlcode = value; }
    }
    public string GlCoaAssetYN
    {
        get { return glcoaassetyn; }
        set { glcoaassetyn = value; }
    }
    public string GlCoaAssetONOFF
    {
        get { return glcoaassetonoff; }
        set { glcoaassetonoff = value; }
    }

    public string GlCoaTrnAnaReq
    {
        get { return glcoatrnanareq; }
        set { glcoatrnanareq = value; }
    }

    public string GlCoaBalTEReq
    {
        get { return glcoabaltereq; }
        set { glcoabaltereq = value; }
    }


    public string GlCoaNarAmtReq
    {
        get { return glcoanaramtreq; }
        set { glcoanaramtreq = value; }
    }
    public string GlCoaNarAmtType
    {
        get { return glcoanaramttype; }
        set { glcoanaramttype = value; }
    }

    public string GLCOACRCODE
    {
        get { return glcoacrcode; }
        set { glcoacrcode = value; }
    }
    public string GlCoaCompCode
    {
        get { return glcoacompcode; }
        set { glcoacompcode = value; }
    }
}

