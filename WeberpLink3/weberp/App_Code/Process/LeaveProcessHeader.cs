using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class LeaveProcessHeader
{
    public LeaveProcessHeader() { }


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
    private double noofleave;
    private string leavetype;
    private string leavedescription;
    private string processid;
    private string flowid;
    private int processlevelid;
    private int processnextlevelid;
    private int actiontypeid;
    private string transactionno;
    private int transactionlineno;
    private string actingpersonid;
    private string responsiblepersonid;
    private int movementno;
    private string entryuserid;
    public float noOfDays { get; set; }
    public string LeaveRemarks { get; set; }
    public int chkstatus { get; set; }
    public string flag { get; set; }

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
    public double NoofLeave
    {
        get { return noofleave; }
        set { noofleave = value; }
    }  
    public string Leavetype
    {
        get { return leavetype; }
        set { leavetype = value; }
    }
    public string Leavedescription
    {
        get { return leavedescription; }
        set { leavedescription = value; }
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
    public int ProcesslevelId
    {
        get { return processlevelid; }
        set { processlevelid = value; }
    }
    public int ProcessnextlevelId
    {
        get { return processnextlevelid; }
        set { processnextlevelid = value; }
    }
    public int ActiontypeId
    {
        get { return actiontypeid; }
        set { actiontypeid = value; }
    }
    public string TransactionNo
    {
        get { return transactionno; }
        set { transactionno = value; }
    }
    public string ActingpersonId
    {
        get { return actingpersonid; }
        set { actingpersonid = value; }
    }
    public int TransactionLineNo
    {
        get { return transactionlineno; }
        set { transactionlineno = value; }
    }
    public string ResponsiblepersonId
    {
        get { return responsiblepersonid; }
        set { responsiblepersonid = value; }
    }
    public int MovementNo
    {
        get { return movementno; }
        set { movementno = value; }
    }
    public string EntryUserid
    {
        get { return entryuserid; }
        set { entryuserid = value; }
    }

    #region ShiftAllocation

    private DateTime dateForSA;
    public string DateForSA { get; set; }
    public string EmpID { get; set; }
    public string ShiftID { get; set; }
    public string InTime { get; set; }
    public string OutTime { get; set; }

    #endregion

    #region Aplication Condition Setup

    public string TaskType { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public string Isstadate { get; set; }
    public string IsendDate { get; set; }
    public string IsShowPreviousTask { get; set; }
    public string IsShowPreviousAtnd { get; set; }
    public string IsApplyPrevious { get; set; }
    public int TrnMonth { get; set; }
    public int TrnYear { get; set; }
    public string Status { get; set; }

    #endregion
    
    
}


