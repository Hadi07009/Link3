using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrystalDecisions.Shared;
/// <summary>
/// Summary description for clsDTO
/// </summary>
public class clsDTO
{
    public clsDTO()
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


    public ConnectionInfo Rptconnectioninfo
    {
        get { return rptconnectioninfo; }
        set { rptconnectioninfo = value; }
    }


    public Object DtTbl
    {
        get { return dt; }
        set { dt = value; }
    }

}



namespace LibraryDTO
{

    public class clsEmailReceiver
    {
        public clsEmailReceiver()
        {
        }

        private string rid;
        private string rname;

        public String Rid
        {
            get { return rid; }
            set { rid = value; }
        }

        public String Rname
        {
            get { return rname; }
            set { rname = value; }
        }

    }

    public class clsReportParameter
    {
        public clsReportParameter()
        {
        }

        private string potype;
        private string plant;
        private DateTime fromdate;
        private DateTime todate;

        public String PoType
        {
            get { return potype; }
            set { potype = value; }
        }

        public String Plant
        {
            get { return plant; }
            set { plant = value; }
        }

        public DateTime FromDate
        {
            get { return fromdate; }
            set { fromdate = value; }
        }

        public DateTime ToDate
        {
            get { return todate; }
            set { todate = value; }
        }

    }

    public class clsMrrData
    {
        public clsMrrData()
        {
        }
        private int seqno;
        private string ref_no;
        private string icode;
        private string idet;
        private string uom;
        private string brand;
        private string origin;
        private string packing;
        private decimal srqty;
        private decimal poqty;
        private decimal recqty;
        private decimal insqty;
        private decimal availqty;
        private decimal entryqty;
        private decimal okqty;
        private decimal rejqty;
        private decimal rate;
        private decimal amnt;
        private string pcode;
        private string adrcode;
        private string adrdet;
        private string pdet;
        private string plant;
        private string pur_by;
        private string pur_from;
        private string pur_type;
        private string modeofdel;
        private string store;
        private string reason;
        private string locofuse;
        private string fromdept;
        private string todept;
        private DateTime purdate;
        private DateTime issuedate;
        private DateTime srdate;
        private string remarks;
        private int lineno;
        private string dcode;
        private string production;
        private string dbtcode;
        private string dbtanal;
        private string caracc;
        private string caraccdet;
        private string caradr;
        private string caradrdet;
        private decimal carcost;
        private decimal stdratio;

        public int Seqno
        {
            get { return seqno; }
            set { seqno = value; }
        }

        public String Ref_no
        {
            get { return ref_no; }
            set { ref_no = value; }
        }
        public String Icode
        {
            get { return icode; }
            set { icode = value; }
        }
        public String Idet
        {
            get { return idet; }
            set { idet = value; }
        }
        public String Uom
        {
            get { return uom; }
            set { uom = value; }
        }

        public String Brand
        {
            get { return brand; }
            set { brand = value; }
        }
        public String Origin
        {
            get { return origin; }
            set { origin = value; }
        }
        public String Packing
        {
            get { return packing; }
            set { packing = value; }
        }
        public decimal Srqty
        {
            get { return srqty; }
            set { srqty = value; }
        }

        public decimal Poqty
        {
            get { return poqty; }
            set { poqty = value; }
        }

        public decimal Recqty
        {
            get { return recqty; }
            set { recqty = value; }
        }

        public decimal Insqty
        {
            get { return insqty; }
            set { insqty = value; }
        }

        public decimal Availqty
        {
            get { return availqty; }
            set { availqty = value; }
        }

        public decimal Entryqty
        {
            get { return entryqty; }
            set { entryqty = value; }
        }

        public decimal OkQty
        {
            get { return okqty; }
            set { okqty = value; }
        }

        public decimal RejQty
        {
            get { return rejqty; }
            set { rejqty = value; }
        }
        public decimal Rate
        {
            get { return rate; }
            set { rate = value; }
        }

        public decimal Amnt
        {
            get { return amnt; }
            set { amnt = value; }
        }

        public String Pcode
        {
            get { return pcode; }
            set { pcode = value; }
        }
        public String Pdet
        {
            get { return pdet; }
            set { pdet = value; }
        }

        public String Adrcode
        {
            get { return adrcode; }
            set { adrcode = value; }
        }

        public String AdrDet
        {
            get { return adrdet; }
            set { adrdet = value; }
        }


        public String Plant
        {
            get { return plant; }
            set { plant = value; }
        }

        public String Pur_by
        {
            get { return pur_by; }
            set { pur_by = value; }
        }

        public String Pur_from
        {
            get { return pur_from; }
            set { pur_from = value; }
        }
        public String Pur_type
        {
            get { return pur_type; }
            set { pur_type = value; }
        }
        public String Modeofdel
        {
            get { return modeofdel; }
            set { modeofdel = value; }
        }

        public String Store
        {
            get { return store; }
            set { store = value; }
        }

        public String Reason
        {
            get { return reason; }
            set { reason = value; }
        }

        public String Locofuse
        {
            get { return locofuse; }
            set { locofuse = value; }
        }
        public String Fromdept
        {
            get { return fromdept; }
            set { fromdept = value; }
        }

        public String Todept
        {
            get { return todept; }
            set { todept = value; }
        }

        public DateTime Purdate
        {
            get { return purdate; }
            set { purdate = value; }
        }
        public DateTime Issuedate
        {
            get { return issuedate; }
            set { issuedate = value; }
        }
        public DateTime Srdate
        {
            get { return srdate; }
            set { srdate = value; }
        }
        public String Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

        public int LineNo
        {
            get { return lineno; }
            set { lineno = value; }
        }

        public String Dcode
        {
            get { return dcode; }
            set { dcode = value; }
        }

        public String Production
        {
            get { return production; }
            set { production = value; }
        }

        public String DbtCode
        {
            get { return dbtcode; }
            set { dbtcode = value; }
        }

        public String DbtAnal
        {
            get { return dbtanal; }
            set { dbtanal = value; }
        }


        public String Caracc
        {
            get { return caracc; }
            set { caracc = value; }
        }

        public String Caraccdet
        {
            get { return caraccdet; }
            set { caraccdet = value; }
        }

        public String Caradr
        {
            get { return caradr; }
            set { caradr = value; }
        }

        public String Caradrdet
        {
            get { return caradrdet; }
            set { caradrdet = value; }
        }

        public decimal Carcost
        {
            get { return carcost; }
            set { carcost = value; }
        }

        public decimal StdRatio
        {
            get { return stdratio; }
            set { stdratio = value; }
        }
        

    }

    public class clsItemDetail
    {
        public clsItemDetail()
        {
        }
        private int seqno;
        private string itemcode;
        private string brand;
        private string origin;
        private string packing;

        public int Seqno
        {
            get { return seqno; }
            set { seqno = value; }
        }

        public String Itemcode
        {
            get { return itemcode; }
            set { itemcode = value; }
        }
        public String Brand
        {
            get { return brand; }
            set { brand = value; }
        }
        public String Origin
        {
            get { return origin; }
            set { origin = value; }
        }
        public String Packing
        {
            get { return packing; }
            set { packing = value; }
        }

    }

    public class clsJVdata
    {
        public clsJVdata()
        {
        }
        private int seqno;
        private string ref_no;
        private string wrk_ac_code;
        private string wrk_ac_type;
        private string wrk_narration;
        private string wrk_trn_type;
        private string wrk_match;
        private string grp1;
        private string grp2;
        private string grp6;
        private string grp7;
        private string adrcode;


        public int Seqno
        {
            get { return seqno; }
            set { seqno = value; }
        }

        public String Ref_no
        {
            get { return ref_no; }
            set { ref_no = value; }
        }
        public String Wrk_ac_code
        {
            get { return wrk_ac_code; }
            set { wrk_ac_code = value; }
        }
        public String Wrk_ac_type
        {
            get { return wrk_ac_type; }
            set { wrk_ac_type = value; }
        }
        public String Wrk_narration
        {
            get { return wrk_narration; }
            set { wrk_narration = value; }
        }
        public String Wrk_trn_type
        {
            get { return wrk_trn_type; }
            set { wrk_trn_type = value; }
        }

        public String Wrk_match
        {
            get { return wrk_match; }
            set { wrk_match = value; }
        }

        public String Grp1
        {
            get { return grp1; }
            set { grp1 = value; }
        }
        public String Grp2
        {
            get { return grp2; }
            set { grp2 = value; }
        }

        public String Grp6
        {
            get { return grp6; }
            set { grp6 = value; }
        }

        public String Grp7
        {
            get { return grp7; }
            set { grp7 = value; }
        }

        public String Adrcode
        {
            get { return adrcode; }
            set { adrcode = value; }
        }


    }

    public class clsMprData
    {
        public clsMprData()
        {
        }
        private int seqno;
        private string ref_no;
        private string icode;
        private string idet;
        private string uom;
        private string strcode;
        private decimal stkqty;
        private decimal mprqty;
        private decimal poqty;
        private decimal orgqty;
        private decimal balqty;
        private decimal availqty;
        private decimal entryqty;
        private decimal itemrate;


        public int Seqno
        {
            get { return seqno; }
            set { seqno = value; }
        }

        public String Ref_no
        {
            get { return ref_no; }
            set { ref_no = value; }
        }
        public String Icode
        {
            get { return icode; }
            set { icode = value; }
        }
        public String Idet
        {
            get { return idet; }
            set { idet = value; }
        }
        public String Uom
        {
            get { return uom; }
            set { uom = value; }
        }
        public String StrCode
        {
            get { return strcode; }
            set { strcode = value; }
        }

        public decimal Stkqty
        {
            get { return stkqty; }
            set { stkqty = value; }
        }

        public decimal Mprqty
        {
            get { return mprqty; }
            set { mprqty = value; }
        }
        public decimal Poqty
        {
            get { return poqty; }
            set { poqty = value; }
        }

        public decimal Orgqty
        {
            get { return orgqty; }
            set { orgqty = value; }
        }

        public decimal Balqty
        {
            get { return balqty; }
            set { balqty = value; }
        }

        public decimal Availqty
        {
            get { return availqty; }
            set { availqty = value; }
        }

        public decimal Entryqty
        {
            get { return entryqty; }
            set { entryqty = value; }
        }

        public decimal Itemrate
        {
            get { return itemrate; }
            set { itemrate = value; }
        }


    }

    public class clsTandC
    {
        public clsTandC()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private int sl_no;
        private string type;
        private int tem_seq;
        private int type_seq;
        private int seq;
        private string data;


        public int Sl_no
        {
            get { return sl_no; }
            set { sl_no = value; }
        }

        public String Type
        {
            get { return type; }
            set { type = value; }
        }

        public int Tem_seq
        {
            get { return tem_seq; }
            set { tem_seq = value; }
        }

        public int Type_seq
        {
            get { return type_seq; }
            set { type_seq = value; }
        }


        public int Seq
        {
            get { return seq; }
            set { seq = value; }
        }

        public String Data
        {
            get { return data; }
            set { data = value; }
        }
    }

    public class clsSpo
    {
        public clsSpo()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private int seq;
        private int lno;
        private string reqtype;
        private string refno;
        private string icode;
        private string idet;
        private string pcode;
        private string pdet;
        private string uom;
        private decimal qnty;
        private decimal rate;
        private decimal totval;
        private string specification;
        private string brand;
        private string origin;
        private string packing;
        private string empdet;
        private string partydet;

        public int Seq
        {
            get { return seq; }
            set { seq = value; }
        }
        public int Lno
        {
            get { return lno; }
            set { lno = value; }
        }

        public String ReqType
        {
            get { return reqtype; }
            set { reqtype = value; }
        }

        public String RefNo
        {
            get { return refno; }
            set { refno = value; }
        }
        public String Icode
        {
            get { return icode; }
            set { icode = value; }
        }
        public String Idet
        {
            get { return idet; }
            set { idet = value; }
        }

        public String Uom
        {
            get { return uom; }
            set { uom = value; }
        }
        public String Pcode
        {
            get { return pcode; }
            set { pcode = value; }
        }
        public String Pdet
        {
            get { return pdet; }
            set { pdet = value; }
        }
        public Decimal Qnty
        {
            get { return qnty; }
            set { qnty = value; }
        }

        public Decimal Rate
        {
            get { return rate; }
            set { rate = value; }
        }

        public Decimal Totval
        {
            get { return totval; }
            set { totval = value; }
        }
        public String Specification
        {
            get { return specification; }
            set { specification = value; }
        }
        public String Brand
        {
            get { return brand; }
            set { brand = value; }
        }
        public String Origin
        {
            get { return origin; }
            set { origin = value; }
        }
        public String Packing
        {
            get { return packing; }
            set { packing = value; }
        }
        public String Partydet
        {
            get { return partydet; }
            set { partydet = value; }
        }
        public String Empdet
        {
            get { return empdet; }
            set { empdet = value; }
        }

    }


    public class clsReport
    {
        public clsReport() { }
    
        private string databasename;
        private int pagezoomfactor;
        private object dt;

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

        public String DatabaseName
        {
            get { return databasename; }
            set { databasename = value; }
        }

        public int PageZoomFactor
        {
            get { return pagezoomfactor; }
            set { pagezoomfactor = value; }
        }

        public ConnectionInfo Rptconnectioninfo
        {
            get { return rptconnectioninfo; }
            set { rptconnectioninfo = value; }
        }


        public Object DtTbl
        {
            get { return dt; }
            set { dt = value; }
        }

    }

}

