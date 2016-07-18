using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class OffdayProcessHeader
{
    public OffdayProcessHeader() { }
    
    private int sl;
    private DateTime claiminDdate;
    private DateTime sysIndate;
    private DateTime sysOutdate;
    private DateTime actIndate;
    private DateTime actOutdate;
    private string sysIntime;
    private string sysOuttime;
    private string sysTotalhrs;
    private string actIntime;
    private string actOuttime;
    private string remarks;
    private string acthrs;
    private string applicantid;
    private string transactionno;
    private string processid;
    private string flowid;
    private string entryuserid;
    private string type;
    private double quantity;
    private string unit;
    public double rate;
    private double unitrate;
    private double unitamount;
    private double maximumlimit;
    private DateTime paymentdate;
    private string paymentnarration;
    private double payableamount;

    public int SL
    {
        get { return sl; }
        set { sl = value; }    
    }

    public DateTime ClaiminDdate
    {
        get { return claiminDdate; }
        set { claiminDdate = value; }
    }
    public DateTime SysIndate
    {
        get { return sysIndate; }
        set { sysIndate = value; }
    }
    public DateTime SysOutdate
    {
        get { return sysOutdate; }
        set { sysOutdate = value; }
    }
    public DateTime ActIndate
    {
        get { return actIndate; }
        set { actIndate = value; }
    }
    public DateTime ActOutdate
    {
        get { return actOutdate; }
        set { actOutdate = value; }
    }
    public string SysIntime
    {
        get { return sysIntime; }
        set { sysIntime = value; }
    }
    public string SysOuttime
    {
        get { return sysOuttime; }
        set { sysOuttime = value; }
    }
    public string SysTotalhrs
    {
        get { return sysTotalhrs; }
        set { sysTotalhrs = value; }
    }
    public string ActIntime
    {
        get { return actIntime; }
        set { actIntime = value; }
    }
    public string ActOuttime
    {
        get { return actOuttime; }
        set { actOuttime = value; }
    }
    public string Remarks
    {
        get { return remarks; }
        set { remarks = value; }
    }
    public string Acthrs
    {
        get { return acthrs; }
        set { acthrs = value; }
    }
    public string ApplicantId
    {
        get { return applicantid; }
        set { applicantid = value; }
    }
    public string Transactionno
    {
        get { return transactionno; }
        set { transactionno = value; }
    }
    public string ProcessId
    {
        get { return processid; }
        set { processid = value; }
    }
    public string FlowId
    {
        get { return flowid; }
        set { flowid = value; }
    }
    public string EntryUserid
    {
        get { return entryuserid; }
        set { entryuserid = value; }
    }
    public string Type
    {
        get { return type; }
        set { type = value; }
    }
    public double Quantity
    {
        get { return quantity; }
        set { quantity = value; }
    }
    public string Unit
    {
        get { return unit; }
        set { unit = value; }
    }
    public double UnitRate
    {
        get { return unitrate; }
        set { unitrate = value; }
    }
    public double UnitAmount
    {
        get { return unitamount; }
        set { unitamount = value; } 
    }
    public double MaximumLimit
    {
        get { return maximumlimit; }
        set { maximumlimit = value; }
    }
    public DateTime PaymentDate
    {
        get { return paymentdate; }
        set { paymentdate = value; }
    }
    public string PaymentNarration
    {
        get { return paymentnarration; }
        set { paymentnarration = value; }
    }
    public double PayableAmount
    {
        get { return payableamount; }
        set { payableamount = value; }
    }
    public double Rate
    {
        get { return rate; }
        set { rate = value; }
    }

}


