using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SqlgenerateForPayroll
/// </summary>
public class SqlgenerateForPayroll
{
	public SqlgenerateForPayroll()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static string GetDataForPost(string companyCode, int monthNo, int yearValue)
    {
        //return "SELECT COM_CODE, SAL_MONTH, SAL_YEAR, POST_TIME FROM SYM_SAL_POST where COM_CODE = '"+companyCode+"' and SAL_MONTH = "+monthNo+" and SAL_YEAR = "+yearValue+"";

        return "select * from hrms_salary where MONTH(Salmonth)=" + monthNo + " and YEAR(Salmonth)=" + yearValue + " and SalGrade<>'50'";
               
    }

    public static string GetBonusDataForPost(string companyCode, int monthNo, int yearValue)
    {
        return "select * from hrms_salary where month(Salmonth) = " + monthNo + " and YEAR(Salmonth) = " + yearValue + " and SalGrade=50";

    }
    public static string DeleteAllSalary1()
    {
        return "DELETE FROM hrms_salary1";
    }

    public static string GetDataBySalaryCycle(DateTime stDate, DateTime endDate, DateTime salDate)
    {
        return "SELECT     HrMs_Emp_Mas.Emp_Mas_Emp_Id, HrMs_Emp_Mas.Emp_Mas_Join_Date, HrMs_Emp_Mas.Emp_Mas_Term_Flg," 
                      +"HrMs_Emp_Mas.Emp_Mas_Emp_Type, hrms_emp_grd_det.det_grade,"
                       +"(SELECT COUNT(*) AS Days "
                        +"FROM  HrMs_Atnd_Det"
                         +" WHERE (Atnd_Det_Emp_Id = HrMs_Emp_Mas.Emp_Mas_Emp_Id) and Atnd_det_date between  CONVERT(Datetime,'"+stDate+"',103) and CONVERT(Datetime,'"+endDate+"',103)  AND (Atnd_det_offlg NOT IN ('N', 'L', 'G', 'H','S'))) AS PrsDys,"
                          +" (SELECT     ISNULL(SUM(Leave_Det_Emp_Days), 0) AS lvs"
                           +" FROM  HrMs_Emp_Leave_Det"
                            +" WHERE (Leave_Det_Emp_Id = HrMs_Emp_Mas.Emp_Mas_Emp_Id) and Leave_Det_Sta_Date between CONVERT(Datetime,'"+stDate+"',103) and CONVERT(Datetime,'"+endDate+"',103)"                            
                             +" ) AS LevDys,"

                                +" (case  Hrms_Division_Master.T_Int when 0 then  (SELECT     COUNT(*) AS hls "
                            +" FROM  HrMs_Holiday"
                            +" WHERE Holiday_Date between CONVERT(Datetime,'"+stDate+"',103) and CONVERT(Datetime,'"+endDate+"',103)  AND "
                            +"(CONVERT(datetime, Holiday_Date, 103) >= CONVERT(datetime, HrMs_Emp_Mas.Emp_Mas_Join_Date, 103)) AND "
                            +" (Holiday_Date NOT IN"
                            + " (SELECT     Atnd_det_date "
                            + "FROM          HrMs_Atnd_Det AS HrMs_Atnd_Det_2"
                            + " WHERE   Atnd_det_date between CONVERT(Datetime,'"+stDate+"',103) and CONVERT(Datetime,'"+endDate+"',103) AND "
                            +" (Atnd_Det_Emp_Id = HrMs_Emp_Mas.Emp_Mas_Emp_Id) AND (Atnd_det_offlg IN ('S', 'L', 'A')))))   else  (SELECT     COUNT(*) AS Expr1"
                            + " FROM HrMs_Atnd_Det AS HrMs_Atnd_Det_3"
                            +" WHERE Atnd_det_date between CONVERT(Datetime,'"+stDate+"',103) and CONVERT(Datetime,'"+endDate+"',103) AND (Atnd_Det_Emp_Id = HrMs_Emp_Mas.Emp_Mas_Emp_Id) AND "
                                                   + " (Atnd_det_offlg = 'H') )  end) as Holydays,"
                          
                          +" (SELECT     COUNT(*) AS TotMark"
                            +" FROM          HrMs_Atnd_Det AS HrMs_Atnd_Det_1 "
                            +" WHERE      (Atnd_Det_Emp_Id = HrMs_Emp_Mas.Emp_Mas_Emp_Id) AND (Atnd_det_offlg = 'S') AND Atnd_det_date between CONVERT(Datetime,'"+stDate+"',103) and CONVERT(Datetime,'"+endDate+"',103) "
                            +" ) AS LopDys,"
                          +" (SELECT     ISNULL(SUM(Adv_Det_Inst_Val), 0) AS Expr1"
                            +" FROM          HrMs_Emp_Adv_Det"
                            +" WHERE      (Adv_Det_Emp_Id = HrMs_Emp_Mas.Emp_Mas_Emp_Id) AND (Adv_Det_Month <= '"+salDate+"') AND (Adv_Det_End_Date > '"+salDate+"')) "
                            +" AS AdvAmt, "

                            +" (SELECT    For_Det_Value"
                            +" FROM         hrms_emp_for_det where hrms_emp_for_det.For_Det_Empid=HrMs_Emp_Mas.Emp_Mas_Emp_Id and hrms_emp_for_det.For_Det_ForCode='PAYDAY') as payday,"

                            +" Hrms_Trans_Det.Trans_Det_Emp_Id, Hrms_Trans_Det.Trans_Det_DivID, Hrms_Division_Master.T_Int"
                            + " FROM         HrMs_Emp_Mas INNER JOIN"
                            +" hrms_emp_grd_det ON HrMs_Emp_Mas.Emp_Mas_Emp_Id = hrms_emp_grd_det.det_empid INNER JOIN"
                            +" Hrms_Trans_Det ON HrMs_Emp_Mas.Emp_Mas_Emp_Id = Hrms_Trans_Det.Trans_Det_Emp_Id INNER JOIN"
                            +" Hrms_Division_Master ON Hrms_Trans_Det.Trans_Det_DivID = Hrms_Division_Master.Division_Master_Code"
                            +" WHERE     (HrMs_Emp_Mas.Emp_Mas_Term_Flg = 'N')  and HrMs_Emp_Mas.Emp_Mas_join_date< '"+endDate+"' ORDER BY HrMs_Emp_Mas.Emp_Mas_Emp_Id";
    }

    public static string InsertSalary1( string employeeCode, DateTime salaryMonth, string calCode, Decimal calValue, string salFlag, string vewFlag, string salGrade, string postFlag)
    {
        return "INSERT INTO [hrms_salary1] ([Empcode], [Salmonth], [Calcode], [CalVal], [Sal_Flg], [View_Flg], [SalGrade], [post_flg]) VALUES ('"+employeeCode+"', '"+salaryMonth+"', '"+calCode+"', "+calValue+", '"+salFlag+"', '"+vewFlag+"', '"+salGrade+"', '"+postFlag+"')";
    }

    public static string GetDatabyDeptDiv()
    {
        return "SELECT  Hrms_Trans_Det.Trans_det_DivID as Division,Hrms_dept_master.Dept_name as Department,SUM(hrms_salary1.CalVal) AS TotalSalary "
                +" FROM hrms_salary1 "
                +" INNER JOIN Hrms_Trans_Det ON hrms_salary1.Empcode = Hrms_Trans_Det.Trans_Det_Emp_Id "
                +" inner join Hrms_dept_master on Hrms_Trans_Det.Trans_det_deptID=Hrms_dept_master.Dept_Code and Hrms_Trans_Det.Trans_det_DivID=Hrms_dept_master.Dept_Division_Code "
                +" where calCode in('NET') group by Hrms_Trans_Det.Trans_det_DivID,Hrms_dept_master.Dept_name order by Hrms_Trans_Det.Trans_det_DivID";
    }

    public static string GetDataFromhrms_salary1()
    {
        return "SELECT Empcode, Salmonth, Calcode, CalVal, Sal_Flg, View_Flg, SalGrade, post_flg FROM hrms_salary1";
    }

    public static string DeletePostedSalary(int monthNo, int yearValue)
    {
        return "DELETE FROM hrms_salary where month(salmonth) = " + monthNo + " and year(salmonth) = " + yearValue + " and SalGrade<>'50'";
    }

    public static string DeletePostedBonus(int monthNo, int yearValue)
    {
        return "DELETE FROM hrms_salary where month(salmonth) = " + monthNo + " and year(salmonth) = " + yearValue + " and SalGrade='50'";
    }

    public static string salaryPost()
    {
        return "insert into hrms_salary select * from hrms_salary1 where SalGrade<>'50'";
    }
    public static string pftransferdelete(int monthNo, int yearValue)
    {
        string refno = "TRAN" + yearValue.ToString() + string.Format("{0:00}", monthNo).ToString();

        return "delete from Hrms_Pf_Transfer where PF_Ref='" + refno.ToString() + "'";
    }

    public static string pftransferInsert(int monthNo, int yearValue)
    {

        string refno = "TRAN" + yearValue.ToString() + string.Format("{0:00}", monthNo).ToString();
        return "insert into Hrms_Pf_Transfer select '" + refno + "',Empcode,Salmonth,Calcode,CalVal,post_flg from hrms_salary1 where Calcode in ('PFEMP','PFEC') and calval > 0";
    }



    public static string BonusPost()
    {
        return "insert into hrms_salary select * from hrms_salary1 where SalGrade='50'";
    }

    public static string GetMaxRef(string periodValue)
    {
        return "SELECT max(convert(int,right(wrk_ref_no,6)))+1 as maxsl FROM FA_TE_WH where wrk_jrn_type = 'JV' and wrk_acc_period = '"+periodValue+"'";
    }

    public static string GetDataHrmsDepartment()
    {
        return "SELECT Hrms_Dept_Master.Dept_Division_Code, Hrms_Dept_Master.Dept_Code, HrMs_PayRoll_Cal.PayRoll_GL_Acc, Hrms_Dept_Master.Dept_Name, "
                         +" HrMs_PayRoll_Cal.PayRoll_GL_TADA, HrMs_PayRoll_Cal.PayRoll_GL_FDAL "
                         +" FROM Hrms_Dept_Master INNER JOIN "
                         +" HrMs_PayRoll_Cal ON Hrms_Dept_Master.Dept_Code = HrMs_PayRoll_Cal.PayRoll_Dept "
                         +" ORDER BY Hrms_Dept_Master.Dept_Code";
    }

    public static string InsertWH(string wrkOprId, string wrkRefNo, string wrkJrnType, DateTime wrkTrnDate, DateTime wrkEntryDate, string wrkAccPeriod, string wrkEntryFlag, string tC1, string tC2, string tFl, int tIn)
    {
        return "INSERT INTO [FA_TE_WH] ([Wrk_Opr_Id], [Wrk_Ref_No], [Wrk_Jrn_Type], [Wrk_Trn_DATE], [Wrk_Entry_DATE], [Wrk_Acc_Period], [Wrk_Entry_Flag], [T_C1], [T_C2], [T_Fl], [T_In]) VALUES ('" + wrkOprId + "','" + wrkRefNo + "','" + wrkJrnType + "','" + wrkTrnDate + "','" + wrkEntryDate + "','" + wrkAccPeriod + "','" + wrkEntryFlag + "','" + tC1 + "','" + tC2 + "','" + tFl + "'," + tIn + ")";
    }

    public static string GetEarnsDataByDivdept(string transDetDivID, string transDetDeptID)
    {
        return "SELECT     Hrms_Trans_Det.Trans_Det_DivID, Hrms_Trans_Det.Trans_det_DeptID, SUM(hrms_salary1.CalVal) -(select  isnull(sum(hrms_salary1.CalVal),0) from hrms_salary1 INNER JOIN Hrms_Trans_Det ON hrms_salary1.Empcode = Hrms_Trans_Det.Trans_Det_Emp_Id "
                +" INNER JOIN hrms_emp_led_det ON hrms_salary1.Calcode = hrms_emp_led_det.Led_Det_ForCode "
                + " WHERE (hrms_emp_led_det.led_Det_Split = 'Y' and hrms_emp_led_det.Led_Det_DCFlag='D' and  hrms_emp_led_det.Led_Det_ForCode in('TA','DA','FDAL')) AND (Hrms_Trans_Det.Trans_Det_DivID = '" + transDetDivID + "') AND  "
                                      + " (Hrms_Trans_Det.Trans_det_DeptID = '" + transDetDeptID + "') "
                +" ) AS CalVal, hrms_emp_led_det.Led_Det_SeqNo, "
                                      +" hrms_emp_led_det.Led_Det_DCFlag, hrms_emp_led_det.led_Det_Split, hrms_emp_led_det.Led_Det_Grpt1, hrms_emp_led_det.Led_Det_Grpt2, "
                                      +" hrms_emp_led_det.Led_Det_Grpt6, hrms_emp_led_det.Led_Det_Grpt7, hrms_emp_led_det.Led_Det_AccCode "
                +" FROM hrms_salary1 INNER JOIN "
                                      +" Hrms_Trans_Det ON hrms_salary1.Empcode = Hrms_Trans_Det.Trans_Det_Emp_Id INNER JOIN "
                                      +" hrms_emp_led_det ON hrms_salary1.Calcode = hrms_emp_led_det.Led_Det_ForCode "
                + " WHERE (hrms_emp_led_det.led_Det_Split = 'N' and hrms_emp_led_det.Led_Det_DCFlag='D' ) AND (Hrms_Trans_Det.Trans_Det_DivID = '" + transDetDivID + "') AND "
                                      + " (Hrms_Trans_Det.Trans_det_DeptID = '" + transDetDeptID + "') "
                +" GROUP BY Hrms_Trans_Det.Trans_Det_DivID, Hrms_Trans_Det.Trans_det_DeptID, hrms_emp_led_det.Led_Det_SeqNo, hrms_emp_led_det.Led_Det_DCFlag, "
                      +" hrms_emp_led_det.led_Det_Split, hrms_emp_led_det.Led_Det_Grpt1, hrms_emp_led_det.Led_Det_Grpt2, hrms_emp_led_det.Led_Det_Grpt6, "
                      +" hrms_emp_led_det.Led_Det_Grpt7, hrms_emp_led_det.Led_Det_AccCode, hrms_emp_led_det.Led_Det_AccCode";
    }

    public static string InsertWD(
                                  string wrkRefNo, float wrkLineNo, string wrkAcCode, string wrkNarration, string wrkTrnType, decimal wrkAmount,
                                  string wrkMatch, float wrkChequeNo, string wrkCurrCode, decimal wrkCurrRate, decimal wrkCurrAmount, string wrkGrpt0,
                                  string wrkGrpt1, string wrkGrpt2, string wrkGrpt3, string wrkGrpt4, string wrkGrpt5, string wrkGrpt6, string wrkGrpt7,
                                  string wrkGrpt8, string wrkGrpt9, string wrkAllocateFlag, string wrkAcDesc, string wrkAcType, string wrkTrnDue_Date,
                                  string wrkAdrCode, string wrkDcNo, string wrkGRNNo, string wrkDepositNo, string tC1, string tC2, string tFl, int tIn 
                                 )
    {
        return "INSERT INTO [FA_TE_WD] ([Wrk_Ref_No], [Wrk_Line_No], [Wrk_Ac_Code], [Wrk_Narration], "
            +" [Wrk_Trn_Type], [Wrk_Amount], [Wrk_Match], [Wrk_Cheque_No], [Wrk_Curr_Code], [Wrk_Curr_Rate], "
            +" [Wrk_Curr_Amount], [Wrk_Grpt0], [Wrk_Grpt1], [Wrk_Grpt2], [Wrk_Grpt3], [Wrk_Grpt4], [Wrk_Grpt5], "
            +" [Wrk_Grpt6], [Wrk_Grpt7], [Wrk_Grpt8], [Wrk_Grpt9], [Wrk_Allocate_Flag], [Wrk_ac_Desc], [Wrk_Ac_Type], "
            +" [Wrk_Trn_Due_DATE], [Wrk_Adr_Code], [Wrk_Dc_No], [Wrk_GRN_No], [Wrk_DepositNo], [T_C1], [T_C2], [T_Fl], [T_In]) "
            + " VALUES ('" + wrkRefNo + "'," + wrkLineNo + ",'" + wrkAcCode + "','" + wrkNarration + "','" + wrkTrnType + "'," + wrkAmount + ",'" + wrkMatch + "', "
            + " " + wrkChequeNo + ",'" + wrkCurrCode + "'," + wrkCurrRate + "," + wrkCurrAmount + ",'" + wrkGrpt0 + "','" + wrkGrpt1 + "','" + wrkGrpt2 + "', "
            + " '" + wrkGrpt3 + "','" + wrkGrpt4 + "','" + wrkGrpt5 + "','" + wrkGrpt6 + "','" + wrkGrpt7 + "','" + wrkGrpt8 + "','" + wrkGrpt9 + "','" + wrkAllocateFlag + "', "
            + " '" + wrkAcDesc + "','" + wrkAcType + "','" + wrkTrnDue_Date + "','" + wrkAdrCode + "','" + wrkDcNo + "','" + wrkGRNNo + "','" + wrkDepositNo + "','" + tC1 + "','" + tC2 + "','" + tFl + "'," + tIn + ")";
    }

    public static string GetDataTADAByDivDept(string transDetDivID, string transdetDeptID)
    {
        return "SELECT Hrms_Trans_Det.Trans_Det_DivID, Hrms_Trans_Det.Trans_det_DeptID, isnull(SUM(hrms_salary1.CalVal),0)  AS CalVal, hrms_emp_led_det.Led_Det_SeqNo, "
                      +" hrms_emp_led_det.Led_Det_DCFlag, hrms_emp_led_det.led_Det_Split, hrms_emp_led_det.Led_Det_Grpt1, hrms_emp_led_det.Led_Det_Grpt2, "
                      +" hrms_emp_led_det.Led_Det_Grpt6, hrms_emp_led_det.Led_Det_Grpt7, hrms_emp_led_det.Led_Det_AccCode "
                      +" FROM hrms_salary1 INNER JOIN "
                      +" Hrms_Trans_Det ON hrms_salary1.Empcode = Hrms_Trans_Det.Trans_Det_Emp_Id INNER JOIN "
                      +" hrms_emp_led_det ON hrms_salary1.Calcode = hrms_emp_led_det.Led_Det_ForCode "
                      + " WHERE (hrms_emp_led_det.led_Det_Split = 'Y' and hrms_emp_led_det.Led_Det_DCFlag='D'  and hrms_emp_led_det.Led_Det_ForCode in('TA','DA')) AND (Hrms_Trans_Det.Trans_Det_DivID = '" + transDetDivID + "') AND "
                      + " (Hrms_Trans_Det.Trans_det_DeptID = '" + transdetDeptID + "') "
                      +" GROUP BY Hrms_Trans_Det.Trans_Det_DivID, Hrms_Trans_Det.Trans_det_DeptID, hrms_emp_led_det.Led_Det_SeqNo, hrms_emp_led_det.Led_Det_DCFlag, "
                      +" hrms_emp_led_det.led_Det_Split, hrms_emp_led_det.Led_Det_Grpt1, hrms_emp_led_det.Led_Det_Grpt2, hrms_emp_led_det.Led_Det_Grpt6, "
                      +" hrms_emp_led_det.Led_Det_Grpt7, hrms_emp_led_det.Led_Det_AccCode, hrms_emp_led_det.Led_Det_AccCode";
    }

    public static string GetDataFDALByDivdept(string transDetDivID, string transdetDeptID)
    {
        return "SELECT Hrms_Trans_Det.Trans_Det_DivID, Hrms_Trans_Det.Trans_det_DeptID, isnull(SUM(hrms_salary1.CalVal),0)  AS CalVal, hrms_emp_led_det.Led_Det_SeqNo, "
                      +" hrms_emp_led_det.Led_Det_DCFlag, hrms_emp_led_det.led_Det_Split, hrms_emp_led_det.Led_Det_Grpt1, hrms_emp_led_det.Led_Det_Grpt2, "
                      +" hrms_emp_led_det.Led_Det_Grpt6, hrms_emp_led_det.Led_Det_Grpt7, hrms_emp_led_det.Led_Det_AccCode "
                      +" FROM hrms_salary1 INNER JOIN "
                      +" Hrms_Trans_Det ON hrms_salary1.Empcode = Hrms_Trans_Det.Trans_Det_Emp_Id INNER JOIN "
                      +" hrms_emp_led_det ON hrms_salary1.Calcode = hrms_emp_led_det.Led_Det_ForCode "
                      + " WHERE (hrms_emp_led_det.led_Det_Split = 'Y' and hrms_emp_led_det.Led_Det_DCFlag='D'  and hrms_emp_led_det.Led_Det_ForCode in('FDAL')) AND (Hrms_Trans_Det.Trans_Det_DivID = '" + transDetDivID + "') AND  "
                      + " (Hrms_Trans_Det.Trans_det_DeptID = '" + transdetDeptID + "') "
                      +" GROUP BY Hrms_Trans_Det.Trans_Det_DivID, Hrms_Trans_Det.Trans_det_DeptID, hrms_emp_led_det.Led_Det_SeqNo, hrms_emp_led_det.Led_Det_DCFlag, "
                      +" hrms_emp_led_det.led_Det_Split, hrms_emp_led_det.Led_Det_Grpt1, hrms_emp_led_det.Led_Det_Grpt2, hrms_emp_led_det.Led_Det_Grpt6, "
                      +" hrms_emp_led_det.Led_Det_Grpt7, hrms_emp_led_det.Led_Det_AccCode, hrms_emp_led_det.Led_Det_AccCode";
    }

    public static string GetOthersDataByDivDept(string transDetDivID, string transdetDeptID)
    {
        return "SELECT Hrms_Trans_Det.Trans_Det_DivID, Hrms_Trans_Det.Trans_det_DeptID, SUM(hrms_salary1.CalVal) AS CalVal, hrms_emp_led_det.Led_Det_SeqNo, "
                      +" hrms_emp_led_det.Led_Det_DCFlag, hrms_emp_led_det.led_Det_Split, hrms_emp_led_det.Led_Det_Grpt1, hrms_emp_led_det.Led_Det_Grpt2, "
                      +" hrms_emp_led_det.Led_Det_Grpt6, hrms_emp_led_det.Led_Det_Grpt7, hrms_emp_led_det.Led_Det_AccCode "
                      +" FROM hrms_salary1 INNER JOIN "
                      +" Hrms_Trans_Det ON hrms_salary1.Empcode = Hrms_Trans_Det.Trans_Det_Emp_Id INNER JOIN "
                      +" hrms_emp_led_det ON hrms_salary1.Calcode = hrms_emp_led_det.Led_Det_ForCode "
                      + " WHERE (hrms_emp_led_det.led_Det_Split = 'N' and hrms_emp_led_det.Led_Det_DCFlag='C' ) AND (Hrms_Trans_Det.Trans_Det_DivID = '" + transDetDivID + "') AND "
                      + " (Hrms_Trans_Det.Trans_det_DeptID = '" + transdetDeptID + "') "
                      +" GROUP BY Hrms_Trans_Det.Trans_Det_DivID, Hrms_Trans_Det.Trans_det_DeptID, hrms_emp_led_det.Led_Det_SeqNo, hrms_emp_led_det.Led_Det_DCFlag, "
                      +" hrms_emp_led_det.led_Det_Split, hrms_emp_led_det.Led_Det_Grpt1, hrms_emp_led_det.Led_Det_Grpt2, hrms_emp_led_det.Led_Det_Grpt6, "
                      +" hrms_emp_led_det.Led_Det_Grpt7, hrms_emp_led_det.Led_Det_AccCode, hrms_emp_led_det.Led_Det_AccCode";
    }

    public static string GetDataByDivDept(string transDetDivID, string transdetDeptID)
    {
        return "SELECT Hrms_Dept_Master.Dept_Division_Code, Hrms_Dept_Master.Dept_Code, Hrms_Trans_Det.Trans_Det_Emp_Id, hrms_salary1.Calcode, "
                      +" hrms_salary1.CalVal, HrMs_PayRoll_Cal.PayRoll_GL_Acc, hrms_emp_led_det.Led_Det_SeqNo, hrms_emp_led_det.Led_Det_AccCode, "
                      +" hrms_emp_led_det.Led_Det_ForCode, hrms_emp_led_det.Led_Det_DCFlag, hrms_emp_led_det.led_Det_Split, hrms_emp_led_det.Led_Det_Grpt1, "
                      +" hrms_emp_led_det.Led_Det_Grpt2, hrms_emp_led_det.Led_Det_Grpt6, hrms_emp_led_det.Led_Det_Grpt7, Hrms_Dept_Master.Dept_Name "
                      +" FROM Hrms_Dept_Master INNER JOIN "
                      +" Hrms_Trans_Det ON Hrms_Dept_Master.Dept_Division_Code = Hrms_Trans_Det.Trans_Det_DivID AND "
                      +" Hrms_Dept_Master.Dept_Code = Hrms_Trans_Det.Trans_det_DeptID INNER JOIN "
                      +" hrms_salary1 ON Hrms_Trans_Det.Trans_Det_Emp_Id = hrms_salary1.Empcode INNER JOIN "
                      +" HrMs_PayRoll_Cal ON Hrms_Dept_Master.Dept_Code = HrMs_PayRoll_Cal.PayRoll_Dept INNER JOIN "
                      +" hrms_emp_led_det ON hrms_salary1.Calcode = hrms_emp_led_det.Led_Det_ForCode "
                      + " WHERE (hrms_salary1.CalVal > 0) and hrms_emp_led_det.led_Det_Split='Y' and hrms_emp_led_det.Led_Det_DCFlag='C' and Hrms_Dept_Master.Dept_Division_Code = '" + transDetDivID + "' and Hrms_Dept_Master.Dept_Code = '" + transdetDeptID + "' "
                      +" ORDER BY Hrms_Dept_Master.Dept_Code, Hrms_Trans_Det.Trans_Det_Emp_Id";
 
    }

    public static string GetTotAmount(string wrkTrnType, string wrkRefNo)
    {
        return "SELECT sum(wrk_amount) FROM FA_TE_WD where wrk_trn_type = '" + wrkTrnType + "' and wrk_ref_no = '" + wrkRefNo + "'";
    }

    public static string InsertJv(string companyCode, int salMonth, int salYear, string postDate)
    {
        return "INSERT INTO [SYM_SAL_POST] ([COM_CODE], [SAL_MONTH], [SAL_YEAR], [POST_TIME]) VALUES ('" + companyCode + "'," + salMonth + "," + salYear + ",'" + postDate + "')";
    }

    public static string GetDataBySalaryCycle2(DateTime endDate, DateTime startDate, DateTime salaryDate)
    {
        return "SELECT HrMs_Emp_Mas.Emp_Mas_Emp_Id, HrMs_Emp_Mas.Emp_Mas_Join_Date, HrMs_Emp_Mas.Emp_Mas_Term_Flg, "
                      +" HrMs_Emp_Mas.Emp_Mas_Emp_Type, hrms_emp_grd_det.det_grade, "
                      +" (SELECT COUNT(*) AS Days "
                      +" FROM HrMs_Atnd_Det "
                      + " WHERE (Atnd_Det_Emp_Id = HrMs_Emp_Mas.Emp_Mas_Emp_Id) and Atnd_det_date between CONVERT(Datetime,'" + startDate + "',103) and CONVERT(Datetime,'" + endDate + "',103)  AND (Atnd_det_offlg NOT IN ('N', 'L', 'G', 'H','S'))) AS PrsDys, "
                      +" (SELECT ISNULL(SUM(Leave_Det_Emp_Days), 0) AS lvs "
                      +" FROM HrMs_Emp_Leave_Det "
                      +" WHERE (Leave_Det_Emp_Id = HrMs_Emp_Mas.Emp_Mas_Emp_Id) and Leave_Det_Sta_Date between CONVERT(Datetime,'" + startDate + "',103) and CONVERT(Datetime,'" + endDate + "',103) "                            
                      +" ) AS LevDys, "
                      +" (case  Hrms_Division_Master.T_Int when 0 then  (SELECT COUNT(*) AS hls "
                      +" FROM HrMs_Holiday "
                      +" WHERE Holiday_Date between CONVERT(Datetime,'" + startDate + "',103) and CONVERT(Datetime,'" + endDate + "',103)  AND "
                      +" (CONVERT(datetime, Holiday_Date, 103) >= CONVERT(datetime, HrMs_Emp_Mas.Emp_Mas_Join_Date, 103)) AND "
                      +" (Holiday_Date NOT IN "
                      +" (SELECT Atnd_det_date "
                      +" FROM HrMs_Atnd_Det AS HrMs_Atnd_Det_2 "
                      +" WHERE Atnd_det_date between CONVERT(Datetime,'" + startDate + "',103) and CONVERT(Datetime,'" + endDate + "',103) AND "
                      +" (Atnd_Det_Emp_Id = HrMs_Emp_Mas.Emp_Mas_Emp_Id) AND (Atnd_det_offlg IN ('S', 'L', 'A')))))   else  (SELECT     COUNT(*) AS Expr1 "
                      +" FROM HrMs_Atnd_Det AS HrMs_Atnd_Det_3 "
                      +" WHERE Atnd_det_date between CONVERT(Datetime,'" + startDate + "',103) and CONVERT(Datetime,'" + endDate + "',103) AND (Atnd_Det_Emp_Id = HrMs_Emp_Mas.Emp_Mas_Emp_Id) AND "
                      +" (Atnd_det_offlg = 'H') )  end) as Holydays, "                          
                      +" (SELECT COUNT(*) AS TotMark "
                      +" FROM HrMs_Atnd_Det AS HrMs_Atnd_Det_1 "
                      +" WHERE (Atnd_Det_Emp_Id = HrMs_Emp_Mas.Emp_Mas_Emp_Id) AND (Atnd_det_offlg = 'S') AND Atnd_det_date between CONVERT(Datetime,'" + startDate + "',103) and CONVERT(Datetime,'" + endDate + "',103) "
                      +" ) AS LopDys, "
                      +" (SELECT ISNULL(SUM(Adv_Det_Inst_Val), 0) AS Expr1 "
                      +" FROM HrMs_Emp_Adv_Det "
                      +" WHERE (Adv_Det_Emp_Id = HrMs_Emp_Mas.Emp_Mas_Emp_Id) AND (Adv_Det_Month <= '" + salaryDate+ "') AND (Adv_Det_End_Date > '" + salaryDate+ "')) "
                      +" AS AdvAmt, "
                      +" (SELECT For_Det_Value "
                      +" FROM hrms_emp_for_det where hrms_emp_for_det.For_Det_Empid=HrMs_Emp_Mas.Emp_Mas_Emp_Id and hrms_emp_for_det.For_Det_ForCode='PAYDAY') as payday, "
                      +" Hrms_Trans_Det.Trans_Det_Emp_Id, Hrms_Trans_Det.Trans_Det_DivID, Hrms_Division_Master.T_Int "
                      +" FROM HrMs_Emp_Mas INNER JOIN "
                      +" hrms_emp_grd_det ON HrMs_Emp_Mas.Emp_Mas_Emp_Id = hrms_emp_grd_det.det_empid INNER JOIN "
                      +" Hrms_Trans_Det ON HrMs_Emp_Mas.Emp_Mas_Emp_Id = Hrms_Trans_Det.Trans_Det_Emp_Id INNER JOIN "
                      +" Hrms_Division_Master ON Hrms_Trans_Det.Trans_Det_DivID = Hrms_Division_Master.Division_Master_Code "
                      +" WHERE (HrMs_Emp_Mas.Emp_Mas_Term_Flg = 'N')  and HrMs_Emp_Mas.Emp_Mas_join_date< '" + endDate + "' ORDER BY HrMs_Emp_Mas.Emp_Mas_Emp_Id";
    }

    public static string GetDataByEmp(string forDetEmpid)
    {
        return "SELECT For_Det_Empid, For_Det_ForCode, For_Det_Value, For_Det_ValFlg, for_det_sno, for_det_grd_flg, Show_flg FROM hrms_emp_for_det where  For_Det_Empid = '" + forDetEmpid + "'";
    }

    public static string GetDataByGrade(string detCode)
    {
        return "SELECT Det_Code, Det_For, Det_Count, Det_Base, Det_Flag, Show_Flg, det_lno FROM hrms_grade_det WHERE Det_Code = '" + detCode + "' ORDER BY det_lno";
    }

    public static string GetDataForPeriodic(DateTime joiningDate, int monthNo, int yearValue, DateTime salaryDate)
    {
        return "SELECT HrMs_Emp_Mas.Emp_Mas_Emp_Id, HrMs_Emp_Mas.Emp_Mas_Join_Date, HrMs_Emp_Mas.Emp_Mas_Term_Flg, "
                      +" HrMs_Emp_Mas.Emp_Mas_Emp_Type, hrms_emp_grd_det.det_grade, "
                      +" (SELECT COUNT(*) AS Days "
                      +" FROM HrMs_Atnd_Det "
                      + " WHERE (Atnd_Det_Emp_Id = HrMs_Emp_Mas.Emp_Mas_Emp_Id) AND (MONTH(CONVERT(datetime, Atnd_det_date, 103)) = " + monthNo + ") AND "
                      + " (YEAR(CONVERT(datetime, Atnd_det_date, 103)) = " + yearValue + ") AND (Atnd_det_offlg NOT IN ('N', 'L', 'G', 'H','S'))) AS PrsDys, "
                      +" (SELECT ISNULL(SUM(Leave_Det_Emp_Days), 0) AS lvs "
                      +" FROM HrMs_Emp_Leave_Det "
                      + "WHERE (Leave_Det_Emp_Id = HrMs_Emp_Mas.Emp_Mas_Emp_Id) AND (MONTH(CONVERT(datetime, Leave_Det_Sta_Date, 103)) = " + monthNo + ") AND "
                      + " (YEAR(CONVERT(datetime, Leave_Det_Sta_Date, 103)) = " + yearValue + ")) AS LevDys, "
                      +" (case  Hrms_Division_Master.T_Int when 0 then  (SELECT     COUNT(*) AS hls "
                      +" FROM  HrMs_Holiday "
                      + " WHERE (MONTH(CONVERT(datetime, Holiday_Date, 103)) = " + monthNo + ") AND (YEAR(CONVERT(datetime, Holiday_Date, 103)) = " + yearValue + ") AND "
                      +" (CONVERT(datetime, Holiday_Date, 103) >= CONVERT(datetime, HrMs_Emp_Mas.Emp_Mas_Join_Date, 103)) AND "
                      +" (Holiday_Date NOT IN "
                      +" (SELECT Atnd_det_date "
                      +" FROM HrMs_Atnd_Det AS HrMs_Atnd_Det_2 "
                      + " WHERE (MONTH(Atnd_det_date) = " + monthNo + ") AND (YEAR(Atnd_det_date) = " + yearValue + ") AND "
                      +" (Atnd_Det_Emp_Id = HrMs_Emp_Mas.Emp_Mas_Emp_Id) AND (Atnd_det_offlg IN ('S', 'L', 'A')))))   else  (SELECT     COUNT(*) AS Expr1 "
                      +" FROM HrMs_Atnd_Det AS HrMs_Atnd_Det_3 "
                      + " WHERE (MONTH(Atnd_det_date) = " + monthNo + ") AND (YEAR(Atnd_det_date) = " + yearValue + ") AND (Atnd_Det_Emp_Id = HrMs_Emp_Mas.Emp_Mas_Emp_Id) AND "
                      +" (Atnd_det_offlg = 'H') )  end) as Holydays, "                          
                      +" (SELECT COUNT(*) AS TotMark "
                      +" FROM HrMs_Atnd_Det AS HrMs_Atnd_Det_1 "
                      +" WHERE (Atnd_Det_Emp_Id = HrMs_Emp_Mas.Emp_Mas_Emp_Id) AND (Atnd_det_offlg = 'S') AND (MONTH(CONVERT(datetime, Atnd_det_date, "
                      + " 103)) = " + monthNo + ") AND (YEAR(CONVERT(datetime, Atnd_det_date, 103)) = " + yearValue + ")) AS LopDys, "
                      +" (SELECT ISNULL(SUM(Adv_Det_Inst_Val), 0) AS Expr1 "
                      +" FROM HrMs_Emp_Adv_Det "
                      + " WHERE (Adv_Det_Emp_Id = HrMs_Emp_Mas.Emp_Mas_Emp_Id) AND (Adv_Det_Month <= '" + salaryDate + "') AND (Adv_Det_End_Date > '" + salaryDate + "')) "
                      +" AS AdvAmt, "
                      +" (SELECT For_Det_Value "
                      +" FROM hrms_emp_for_det where hrms_emp_for_det.For_Det_Empid=HrMs_Emp_Mas.Emp_Mas_Emp_Id and hrms_emp_for_det.For_Det_ForCode='PAYDAY') as payday, "
                      +" Hrms_Trans_Det.Trans_Det_Emp_Id, Hrms_Trans_Det.Trans_Det_DivID, Hrms_Division_Master.T_Int "
                      +" FROM HrMs_Emp_Mas INNER JOIN "
                      +" hrms_emp_grd_det ON HrMs_Emp_Mas.Emp_Mas_Emp_Id = hrms_emp_grd_det.det_empid INNER JOIN "
                      +" Hrms_Trans_Det ON HrMs_Emp_Mas.Emp_Mas_Emp_Id = Hrms_Trans_Det.Trans_Det_Emp_Id INNER JOIN "
                      +" Hrms_Division_Master ON Hrms_Trans_Det.Trans_Det_DivID = Hrms_Division_Master.Division_Master_Code "
                      + " WHERE (HrMs_Emp_Mas.Emp_Mas_Term_Flg = 'N')  and HrMs_Emp_Mas.Emp_Mas_join_date< '" + joiningDate + "' ORDER BY HrMs_Emp_Mas.Emp_Mas_Emp_Id";
 
    }
}