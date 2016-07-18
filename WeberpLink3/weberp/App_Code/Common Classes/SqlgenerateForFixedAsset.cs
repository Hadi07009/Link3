using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SqlgenerateForFixedAsset
/// </summary>
public class SqlgenerateForFixedAsset
{
	public SqlgenerateForFixedAsset()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Group Setup

    public static string SqlGetGroupData()
    {
        return "SELECT Grp_Def_Del_Chk, Grp_Def_Id, Grp_Def_Name, Grp_Def_Short, Grp_Def_Upd, T_C1, T_C2, T_Fl, T_In FROM InMa_Grp_Def WHERE (LEFT (Grp_Def_Id, 1) = 'I') ORDER BY Grp_Def_Id";
    }

    public static string SqlCheckDuplocateData(string grpDefId)
    {
        return "SELECT Grp_Def_Del_Chk, Grp_Def_Id, Grp_Def_Name, Grp_Def_Short, Grp_Def_Upd, T_C1, T_C2, T_Fl, T_In FROM InMa_Grp_Def WHERE (Grp_Def_Id = '"+grpDefId+"')";
    }

    public static string SqlUpdateGroupDef(string grpDefName, string grpDefShort, string grpDefDelChk, DateTime grpDefUpd, string TC1, string TC2, string TFl, int TIn, string grpDefId) 
    {
        return "UPDATE [InMa_Grp_Def] SET [Grp_Def_Name]='" + grpDefName + "',[Grp_Def_Short]='" + grpDefShort + "', [Grp_Def_Del_Chk]='" + grpDefDelChk + "', [Grp_Def_Upd]= CONVERT(DATETIME,'" + grpDefUpd + "',103), [T_C1]='" + TC1 + "', [T_C2]='" + TC2 + "', [T_Fl]='" + TFl + "', [T_In]=" + TIn + " WHERE [Grp_Def_Id]='" + grpDefId + "'";
    }

    public static string SqlInsertGroupDef(string grpDefId, string grpDefName, string grpDefShort, string grpDefDelChk, DateTime grpDefUpd, string TC1, string TC2, string TFl, int TIn)
    {
        return "INSERT INTO [InMa_Grp_Def] ([Grp_Def_Id], [Grp_Def_Name], [Grp_Def_Short], [Grp_Def_Del_Chk], [Grp_Def_Upd], [T_C1], [T_C2], [T_Fl], [T_In]) VALUES ('" + grpDefId + "', '" + grpDefName + "', '" + grpDefShort + "', '" + grpDefDelChk + "',CONVERT(DATETIME,'" + grpDefUpd + "',103), '" + TC1 + "', '" + TC2 + "', '" + TFl + "', " + TIn + ")";
    }
    #endregion Group Setup

    #region Group Code Setup

    public static string SqlGetItemGroupCode()
    {
        return "SELECT Grp_Code, Grp_Code_Id, Grp_Code_Name, Grp_Code_Ref_No, Grp_Code_Sht, Grp_Code_Upd, T_C1, T_C2, T_Fl, T_In FROM InMa_Grp_Code WHERE (LEFT (Grp_Code_Id, 1) = 'I')";
    }

    public static string CheckDuplicateData(string grpCodeId, string grpCode)
    {
        return "SELECT Grp_Code, Grp_Code_Id, Grp_Code_Name, Grp_Code_Ref_No, Grp_Code_Sht, Grp_Code_Upd, T_C1, T_C2, T_Fl, T_In FROM InMa_Grp_Code WHERE (Grp_Code_Id = '" + grpCodeId + "') AND (Grp_Code = '" + grpCode + "')";
    }

    public static string UpdateItemGroupCode(string grpCodeName, string grpCodeSht, DateTime grpCodeUpd, string TC1, string TC2, string TFl, int TIn, string grpCodeId, string grpCode)
    {
        return "UPDATE [InMa_Grp_Code] SET  [Grp_Code_Name]='" + grpCodeName + "', [Grp_Code_Sht]='" + grpCodeSht + "', [Grp_Code_Upd]=CONVERT(DATETIME,'" + grpCodeUpd + "',103), [T_C1]='" + TC1 + "', [T_C2]='" + TC2 + "', [T_Fl]='" + TFl + "', [T_In]=" + TIn + " WHERE [Grp_Code_Id]='" + grpCodeId + "' AND [Grp_Code]='" + grpCode + "'";
    }

    public static string GetMaxRefNo()
    {
        return "SELECT MAX(Grp_Code_Ref_No) AS MaxRefNo FROM InMa_Grp_Code";
    }

    public static string InsertItemGroupCode(string grpCodeId, string grpCode, string grpCodeName, string grpCodeSht, DateTime grpCodeUpd, string TC1, string TC2, string TFl, int TIn)
    {
        return "INSERT INTO [InMa_Grp_Code] ([Grp_Code_Id], [Grp_Code], [Grp_Code_Name], [Grp_Code_Sht], [Grp_Code_Upd], [T_C1], [T_C2], [T_Fl], [T_In]) VALUES ('" + grpCodeId + "','" + grpCode + "', '" + grpCodeName + "', '" + grpCodeSht + "',CONVERT(DATETIME,'" + grpCodeUpd + "',103), '" + TC1 + "', '" + TC2 + "', '" + TFl + "', " + TIn + ")";
    }    
    public static string GetItemGroupCodeByGroupID(string grpCodeId)
    {
        return "SELECT * FROM InMa_Grp_Code WHERE (Grp_Code_Id = '" + grpCodeId + "') order by Grp_Code_Name ";
    }
    #endregion Group Code Setup

    #region Item Unit Setup

    public static string GetData()
    {
        return "SELECT Uom_Code, Uom_Name, Uom_Upd_DATE, T_C1, T_C2, T_Fl, T_In FROM InMa_Uom order by Uom_Name";
    }

    public static string CheckDuplicateData(string uomCode)
    {
        return "SELECT T_C1, T_C2, T_Fl, T_In, Uom_Code, Uom_Name, Uom_Upd_DATE FROM InMa_Uom WHERE (Uom_Code = '" + uomCode + "')";
    }

    public static string UpdateUOM(string uomName, DateTime uomUpdDATE, string TC1, string TC2, string TFl, int TIn, string uomCode)
    {
        return "UPDATE  [InMa_Uom]  SET [Uom_Name]='" + uomName + "', [Uom_Upd_DATE]= CONVERT(DATETIME, '" + uomUpdDATE + "',103), [T_C1]='" + TC1 + "', [T_C2]='" + TC2 + "', [T_Fl]='" + TFl + "', [T_In]=" + TIn + " WHERE [Uom_Code]='" + uomCode + "'";
    }

    public static string InsertUOM(string uomCode, string uomName, DateTime uomUpdDATE, string TC1, string TC2, string TFl, int TIn)
    {
        return "INSERT INTO [InMa_Uom] ([Uom_Code], [Uom_Name], [Uom_Upd_DATE], [T_C1], [T_C2], [T_Fl], [T_In]) VALUES ('" + uomCode + "', '" + uomName + "', CONVERT(DATETIME, '" + uomUpdDATE + "',103), '" + TC1 + "', '" + TC2 + "', '" + TFl + "', " + TIn + ")";
    }

    public static string DeleteUOM(string uomCode)
    {
        return "DELETE FROM InMa_Uom WHERE Uom_Code = '"+uomCode+"'";
    }

    #endregion Item Unit Setup
    #region Item Information Setup
    public static string GetItemType()
    {
        return "SELECT ItemTypeGroupID, ItemTypeID, ItemTypeName FROM InMa_Known_Value order by ItemTypeName";
    }
    public static string GetItemByCode(string itmDetIcode)          
    {
        return "SELECT * FROM InMa_Itm_Det WHERE (Itm_Det_Icode = '" + itmDetIcode + "')";
    }
    public static string UpdateItem(string itmDetSecCode, string itmDetDesc, string itmDetaAddes1, string itmDetaDddes2, string ItmDetAddDes3, string itmDetPUSAUnit,
        string itmDetStkUnit, int itmDetUnitWgt, string itmDetMPOFlag, string itmDetMRPFlag, string itmDetBOMFlag, int itmDetExpLevel, string itmDetConCode, DateTime itmDetLstUpd,
        string itmDetCom, string itmDetAccCode, string itmDetABC, string itmDetXYZ, string itmDetFSN, string itmDetSizeFlag, string itmDetColorFlag, string itmDetModelFlag, string itmDetTextureFlag,
        string itmDetOthers1Flag, string itmDetOthers2Flag, string itmDetOthers3Flag, string itmDetTypeFlag, string tC1, string tC2, string tFl, int tIn, string iStatus, int itemTypeId, string itmDetIcode)
    {
        return "UPDATE  [InMa_Itm_Det] Set [Itm_Det_Sec_Code]='" + itmDetSecCode + "', [Itm_Det_desc]='" + itmDetDesc + "', [Itm_Det_add_des1]='" + itmDetaAddes1 + "',"
            + " [Itm_Det_add_des2]='" + itmDetaDddes2 + "', [Itm_Det_add_des3]='" + ItmDetAddDes3 + "', [Itm_Det_PUSA_unit]='" + itmDetPUSAUnit + "', [Itm_Det_stk_unit]='" + itmDetStkUnit + "', "
            + " [Itm_Det_unit_wgt]=" + itmDetUnitWgt + ", [Itm_Det_MPO_flag]='" + itmDetMPOFlag + "', [Itm_Det_MRP_flag]='" + itmDetMRPFlag + "', [Itm_Det_BOM_flag]='" + itmDetBOMFlag + "', "
            + " [Itm_Det_exp_level]=" + itmDetExpLevel + ", [Itm_Det_con_code]='" + itmDetConCode + "', [Itm_Det_Lst_Upd]= CONVERT(DATETIME,'" + itmDetLstUpd + "',103), [Itm_Det_com]='" + itmDetCom + "', "
            + " [Itm_Det_Acc_code]='" + itmDetAccCode + "', [Itm_Det_ABC]='" + itmDetABC + "', [Itm_Det_XYZ]='" + itmDetXYZ + "', [Itm_Det_FSN]='" + itmDetFSN + "', [Itm_Det_Size_flag]='" + itmDetSizeFlag + "', "
            + " [Itm_Det_Color_flag]='" + itmDetColorFlag + "', [Itm_Det_Model_flag]='" + itmDetModelFlag + "', [Itm_Det_Texture_flag]='" + itmDetTextureFlag + "', "
            + " [Itm_Det_Others1_flag]='" + itmDetOthers1Flag + "', [Itm_Det_Others2_flag]='" + itmDetOthers2Flag + "', [Itm_Det_Others3_flag]='" + itmDetOthers3Flag + "', "
            + " [Itm_Det_Type_flag]='" + itmDetTypeFlag + "', [T_C1]='" + tC1 + "', [T_C2]='" + tC2 + "', [T_Fl]='" + tFl + "', [T_In]=" + tIn + ", [istatus]='" + iStatus + "', [ItemTypeId]=" + itemTypeId + " "
            + " WHERE Itm_Det_Icode='" + itmDetIcode + "'";
    }

    public static string InsertItem(string itmDetIcode, string itmDetSecCode, string itmDetDesc, string itmDetAddDes1, string itmDetAddDes2, string itmDetAddDes3,
        string itmDetPUSAUnit, string itmDetStkUnit, int itmDetUnitWgt, string itmDetMPOFlag, string itmDetMRPFlag, string itmDetBOMFlag, int itmDetExpLevel, string itmDetConCode, string itmDetTrnPst, DateTime itmDetLstUpd, DateTime itmDetLstTrn, string itmDetCom,
        string itmDetAccCode, string itmDetABC, string itmDetXYZ, string itmDetFSN, string itmDetSizeFlag, string itmDetColorFlag, string itmDetModelFlag, string itmDetTextureFlag, string itmDetOthers1Flag, string itmDetOthers2Flag, string itmDetOthers3Flag,
        string itmDetTypeFlag, string tC1, string tC2, string tFl, int tIn, string iStatus, int itemTypeId)
    {
        string sql = "";
        return sql="INSERT INTO [InMa_Itm_Det] ([Itm_Det_Icode], [Itm_Det_Sec_Code], [Itm_Det_desc], [Itm_Det_add_des1], [Itm_Det_add_des2], "
            +" [Itm_Det_add_des3], [Itm_Det_PUSA_unit], [Itm_Det_stk_unit], [Itm_Det_unit_wgt], [Itm_Det_MPO_flag], [Itm_Det_MRP_flag], [Itm_Det_BOM_flag], "
            +" [Itm_Det_exp_level], [Itm_Det_con_code], [Itm_Det_trn_pst], [Itm_Det_Lst_Upd], [Itm_Det_Lst_trn], [Itm_Det_com], [Itm_Det_Acc_code], [Itm_Det_ABC], "
            +" [Itm_Det_XYZ], [Itm_Det_FSN], [Itm_Det_Size_flag], [Itm_Det_Color_flag], [Itm_Det_Model_flag], [Itm_Det_Texture_flag], [Itm_Det_Others1_flag], "
            +" [Itm_Det_Others2_flag], [Itm_Det_Others3_flag], [Itm_Det_Type_flag], [T_C1], [T_C2], [T_Fl], [T_In], [istatus], [ItemTypeId]) VALUES "
            + " ('" + itmDetIcode + "', '" + itmDetSecCode + "', '" + itmDetDesc + "', '" + itmDetAddDes1 + "', '" + itmDetAddDes2 + "', '" + itmDetAddDes3 + "', "
            + " '" + itmDetPUSAUnit + "', '" + itmDetStkUnit + "', " + itmDetUnitWgt + ", '" + itmDetMPOFlag + "', '" + itmDetMRPFlag + "', '" + itmDetBOMFlag + "', " + itmDetExpLevel + ", '" + itmDetConCode + "', "
            + " '" + itmDetTrnPst + "', '" + itmDetLstUpd + "', '" + itmDetLstTrn + "', '" + itmDetCom + "', '" + itmDetAccCode + "', '" + itmDetABC + "', '" + itmDetXYZ + "', '" + itmDetFSN + "', "
            + " '" + itmDetSizeFlag + "', '" + itmDetColorFlag + "', '" + itmDetModelFlag + "', '" + itmDetTextureFlag + "', '" + itmDetOthers1Flag + "', '" + itmDetOthers2Flag + "', "
            + " '" + itmDetOthers3Flag + "', '" + itmDetTypeFlag + "', '" + tC1 + "', '" + tC2 + "', '" + tFl + "', " + tIn + ", '" + iStatus + "', " + itemTypeId + ")";
    }

    public static string DeleteItemGroup(string itmGrpIcode)
    {
        return "DELETE  FROM   InMa_Itm_Grp WHERE Itm_Grp_icode= '" + itmGrpIcode + "'";
    }

    public static string InsertItemGroup(string itmGrpIcode, string itmGrpId, string itmGrpCode)
    {
        return "INSERT INTO [InMa_Itm_Grp] ([Itm_Grp_icode], [Itm_Grp_Id], [Itm_Grp_Code]) VALUES ('" + itmGrpIcode + "', '" + itmGrpId + "', '" + itmGrpCode + "')";
    }

    public static string GetGroupCodeByItem(string itmGrpIcode)
    {
        return "SELECT Itm_Grp_Code, Itm_Grp_Id, Itm_Grp_icode FROM InMa_Itm_Grp WHERE (Itm_Grp_icode = '" + itmGrpIcode + "')";
    }

    public static string GetStkCtlByItemCode(string stkCtlICode)
    {
        return "SELECT Stk_Ctl_Ave_Val, Stk_Ctl_Cur_Stk, Stk_Ctl_FIFO_Val, Stk_Ctl_Free_Stk, Stk_Ctl_ICode, Stk_Ctl_Ind_Stk, Stk_Ctl_LIFO_Val, Stk_Ctl_Lat_Val, "
        +" Stk_Ctl_Lst_Iss_Dat, Stk_Ctl_Lst_Rec_Dat, Stk_Ctl_Max_Stk, Stk_Ctl_Min_Stk, Stk_Ctl_On_Ord_Stk, Stk_Ctl_Quot_Stk, Stk_Ctl_Reord_Stk, Stk_Ctl_SCode, "
        + " Stk_Ctl_Std_Val, Stk_Ctl_Str_Grp, T_C1, T_C2, T_Fl, T_In FROM InMa_Stk_Ctl WHERE (Stk_Ctl_ICode = '" + stkCtlICode + "')";
    }

    public static string GetItemAccMapping(string itemCode)
    {
        return "SELECT [FaAcc],[DepAcc],[AcmDepAcc],[DispAcc],[RevAcc],[ModelNumber],[LifeCycle],[VatAcc],[TaxAcc] FROM [Inma_Itm_AccMapping] WHERE [ItemCode] = '" + itemCode + "'";
    }
   
    #endregion Item Information Setup

    #region Purchase Order

    public static string GetMaxPoNo(DateTime datePeriod)
    {
        return "SELECT MAX(RIGHT(PO_Hdr_Ref, 6)) AS MaxPoRefNo FROM PuTr_PO_Hdr WHERE (PO_Hdr_Type = 'PO') AND (PO_Hdr_Code = 'PO') AND "
            + " (CONVERT(DATETIME, PO_Hdr_DATE, 103) >= CONVERT(datetime, '" + datePeriod + "', 103))";
    }

    public static string InsertPoHdr(string pOHdrType, string pOHdrCode, string poRefNo, string pOHdrRef, string pOHdrPcode, string pOHdrAcode,
        DateTime pOHdrDate, string pOHdrCom1, string pOHdrCom2, string pOHdrCom3, string pOHdrCom4, string pOHdrCom5, string pOHdrCom6, string pOHdrCom7,
        string pOHdrCom8, string pOHdrCom9, string pOHdrCom10, decimal pOHdrValue, string pOHdrHPCFlag, string pOHdrEntPrd, string pOHdrOprCode,
        DateTime poHdrOrdDate, string pOHdrPrdCld, string pOHdrExpTyp, string pOHdrCmtAcc, DateTime poHdrDueDate, string poHdrDuePrd, string tC1, string tC2,
        string tFl, int tIn, string poHdrCurrCode, int poHdrExchRate)
    {
        return "INSERT INTO [PuTr_PO_Hdr] ([PO_Hdr_Type], [PO_Hdr_Code], [PO_Hdr_Ref], [PO_Hdr_Pcode], [PO_Hdr_Dcode], [PO_Hdr_Acode], [PO_Hdr_DATE], [PO_Hdr_Com1], "
            +" [PO_Hdr_Com2], [PO_Hdr_Com3], [PO_Hdr_Com4], [PO_Hdr_Com5], [PO_Hdr_Com6], [PO_Hdr_Com7], [PO_Hdr_Com8], [PO_Hdr_Com9], [PO_Hdr_Com10], [PO_Hdr_Value], "
            +" [PO_Hdr_HPC_Flag], [PO_Hdr_Ent_Prd], [PO_Hdr_Opr_Code], [Po_Hdr_Ord_Date], [PO_Hdr_Prd_Cld], [PO_Hdr_Exp_Typ], [PO_Hdr_Cmt_Acc], [po_hdr_due_date],  "
            + " [Po_Hdr_Due_Prd], [T_C1], [T_C2], [T_Fl], [T_In], [Po_Hdr_Curr_Code], [Po_Hdr_Exch_rate]) VALUES ('" + pOHdrType + "', '" + pOHdrCode + "', '" + poRefNo + "', '" + pOHdrRef + "', '" + pOHdrPcode + "', "
            + " '" + pOHdrAcode + "', '" + pOHdrDate + "', '" + pOHdrCom1 + "', '" + pOHdrCom2 + "', '" + pOHdrCom3 + "', '" + pOHdrCom4 + "', '" + pOHdrCom5 + "', "
            + " '" + pOHdrCom6 + "', '" + pOHdrCom7 + "', '" + pOHdrCom8 + "', '" + pOHdrCom9 + "', '" + pOHdrCom10 + "', " + pOHdrValue + ", '" + pOHdrHPCFlag + "', "
            + " '" + pOHdrEntPrd + "', '" + pOHdrOprCode + "', '" + poHdrOrdDate + "', '" + pOHdrPrdCld + "', '" + pOHdrExpTyp + "', '" + pOHdrCmtAcc + "', "
            + " '" + poHdrDueDate + "', '" + poHdrDuePrd + "', '" + tC1 + "', '" + tC2 + "', '" + tFl + "', " + tIn + ", '" + poHdrCurrCode + "', " + poHdrExchRate + ")";
    }

    public static string EditPoHdr(string pOHdrPcode, string pOHdrDcode, string pOHdrAcode, DateTime pOHdrDate, string pOHdrCom1, string pOHdrCom2, string pOHdrCom3,
        string pOHdrCom4, string pOHdrCom5, string pOHdrCom6, string pOHdrCom7, string pOHdrCom8, string pOHdrCom9, string pOHdrCom10, decimal pOHdrValue,
        string pOHdrHPCFlag, string pOHdrEntPrd, string pOHdrOprCode, DateTime poHdrOrdDate, string pOHdrPrdCld, string pOHdrExpTyp, string pOHdrCmtAcc,
        DateTime poHdrDue_Date, string poHdrDuePrd, string tC1, string tC2, string tFl, int tIn, string poHdrCurrCode, int poHdrExchRate, string pOHdrRef)
    {
        return "UPDATE [PuTr_PO_Hdr]  SET  [PO_Hdr_Pcode]='" + pOHdrPcode + "', [PO_Hdr_Dcode]='" + pOHdrDcode + "', [PO_Hdr_Acode]='" + pOHdrAcode + "', "
            + " [PO_Hdr_DATE]='" + pOHdrDate + "', [PO_Hdr_Com1]='" + pOHdrCom1 + "', [PO_Hdr_Com2]='" + pOHdrCom2 + "', [PO_Hdr_Com3]='" + pOHdrCom3 + "', "
            + " [PO_Hdr_Com4]='" + pOHdrCom4 + "', [PO_Hdr_Com5]='" + pOHdrCom5 + "', [PO_Hdr_Com6]='" + pOHdrCom6 + "', [PO_Hdr_Com7]='" + pOHdrCom7 + "', "
            + " [PO_Hdr_Com8]='" + pOHdrCom8 + "', [PO_Hdr_Com9]='" + pOHdrCom9 + "', [PO_Hdr_Com10]='" + pOHdrCom10 + "', [PO_Hdr_Value]=" + pOHdrValue + ", "
            + " [PO_Hdr_HPC_Flag]='" + pOHdrHPCFlag + "', [PO_Hdr_Ent_Prd]='" + pOHdrEntPrd + "', [PO_Hdr_Opr_Code]='" + pOHdrOprCode + "', "
            + " [Po_Hdr_Ord_Date]='" + poHdrOrdDate + "',[PO_Hdr_Prd_Cld]='" + pOHdrPrdCld + "', [PO_Hdr_Exp_Typ]='" + pOHdrExpTyp + "', [PO_Hdr_Cmt_Acc]='" + pOHdrCmtAcc + "', "
            + " [po_hdr_due_date]='" + poHdrDue_Date + "', [Po_Hdr_Due_Prd]='" + poHdrDuePrd + "', [T_C1]='" + tC1 + "', [T_C2]='" + tC2 + "', [T_Fl]='" + tFl + "', [T_In]=" + tIn + ", "
            + " [Po_Hdr_Curr_Code]='" + poHdrCurrCode + "', [Po_Hdr_Exch_rate]=" + poHdrExchRate + " WHERE [PO_Hdr_Ref]='" + pOHdrRef + "'";
    }

    public static string DeletePoDet(string pODetRef)
    {
        return "DELETE FROM [PuTr_PO_Det]  WHERE [PO_Det_Ref]='" + pODetRef + "'";
    }

    public static string InsertPoDet(string pODetType, string pODetCode, string pODetRef, short pODetLno, string pODetSfx, int pODetExpLnop, string pODetIcode,
        string pODetItmDesc, string pODetItmUom, string pODetStrCode, string pODetBinCode, string pODetPrRef, int pODetPrLno, string pODetBatNo,
        DateTime pODetExpDat, DateTime pODetDueDat, double pODetLinQty, int pODetOrgQTY, double pODetBalQty, int pODetUntWgt, string pODetOCFlag, string pODetBalUpd,
        decimal pODetLinRat, decimal pODetLinAmt, decimal pODetLinNet, string tC1, string tC2, string tFl, int tIn)
    {
        return "INSERT INTO [PuTr_PO_Det] ([PO_Det_Type], [PO_Det_Code], [PO_Det_Ref], [PO_Det_Lno], [PO_Det_Sfx], [PO_Det_Exp_Lno], [PO_Det_Icode], [PO_Det_Itm_Desc],  "
            +" [PO_Det_Itm_Uom], [PO_Det_Str_Code], [PO_Det_Bin_Code], [PO_Det_Pr_Ref], [PO_Det_Pr_Lno], [PO_Det_Bat_No], [PO_Det_Exp_Dat], [PO_Det_Due_Dat], [PO_Det_Lin_Qty], "
            +" [PO_Det_Org_QTY], [PO_Det_Bal_Qty], [PO_Det_Unt_Wgt], [PO_Det_OC_Flag], [PO_Det_Bal_Upd], [PO_Det_Lin_Rat], [PO_Det_Lin_Amt], [PO_Det_Lin_Net], [T_C1], [T_C2],  "
            + " [T_Fl], [T_In]) VALUES ('" + pODetType + "', '" + pODetCode + "', '" + pODetRef + "', " + pODetLno + ", '" + pODetSfx + "', " + pODetExpLnop + ", "
            + " '" + pODetIcode + "', '" + pODetItmDesc + "', '" + pODetItmUom + "', '" + pODetStrCode + "', '" + pODetBinCode + "', '" + pODetPrRef + "', "
            + " " + pODetPrLno + ", '" + pODetBatNo + "', '" + pODetExpDat + "', '" + pODetDueDat + "', " + pODetLinQty + ", " + pODetOrgQTY + ", " + pODetBalQty + ", "
            + " " + pODetUntWgt + ", '" + pODetOCFlag + "', '" + pODetBalUpd + "', " + pODetLinRat + ", " + pODetLinAmt + ", " + pODetLinNet + ", '" + tC1 + "', "
            + " '" + tC2 + "', '" + tFl + "', " + tIn + ")";
    }

    public static string GetTranByTypeCode(string trnSetTrType, string trnSetTrCode, string opPstOprCode)
    {
        return "SELECT InSu_Trn_Set.Trn_Set_Tr_Type, InSu_Trn_Set.Trn_Set_Tr_Code, InSu_Trn_Set.Trn_Set_Tr_Name, InSu_Trn_Set.Trn_Set_Batch, " 
                      +" InSu_Trn_Set.Trn_Set_BOM, InSu_Trn_Set.Trn_Set_Due_Date, InSu_Trn_Set.Trn_Set_Stk_Upd, InSu_Trn_Set.Trn_Set_Ord_Flag, "
                      +" InSu_Trn_Set.Trn_Set_IQ_Flag, InSu_Trn_Set.Trn_Set_Tr_Pfix, InSu_Trn_Set.Trn_Set_Tr_Ref, InSu_Trn_Set.Trn_Set_Tr_Next_No, "
                      +" InSu_Trn_Set.Trn_Set_Ord_Ref, InSu_Trn_Set.Trn_Set_Ord_Pfix, InSu_Trn_Set.Trn_Set_Ord_Next_No, InSu_Trn_Set.Trn_Set_Iq_Ref, "
                      +" InSu_Trn_Set.Trn_Set_IQ_Pfix, InSu_Trn_Set.Trn_Set_Iq_Next_No, InSu_Trn_Set.Trn_Set_Part_Ship, InSu_Trn_Set.Trn_Set_Unit_Con, "
                      +" InSu_Trn_Set.Trn_Set_Con_Ord, InSu_Trn_Set.Trn_Set_Itm_Valid, InSu_Trn_Set.Trn_Set_Sup_Link, InSu_Trn_Set.Trn_Set_Val_Req, "
                      +" InSu_Trn_Set.Trn_Set_Doc_Anal, InSu_Trn_Set.Trn_Set_Lin_Val, InSu_Trn_Set.Trn_Set_Disp_1, InSu_Trn_Set.Trn_Set_Disp_2, "
                      +" InSu_Trn_Set.Trn_Set_Disp_3, InSu_Trn_Set.Trn_Set_Led_Inter, InSu_Trn_Set.Trn_Set_Unit_Wgt, InSu_Trn_Set.Trn_Set_Def_Str, "
                      +" InSu_Trn_Set.Trn_Set_Str_Cde, InSu_Trn_Set.Trn_Set_Fix_Qty, InSu_Trn_Set.Trn_Set_Lin_Qty, InSu_Trn_Set.Trn_Set_Last_Tr_Date, "
                      +" InSu_Trn_Set.Trn_Set_Last_Upd, InSu_Trn_Set.Trn_Prst_Flag, InSu_Trn_Set.Trn_Set_Add_Val, InSu_Trn_Set.Trn_Set_Value_Code, "
                      +" InSu_Trn_Set.Trn_Set_Cst_Metd, InSu_Trn_Set.Trn_Set_Auto_Itm, InSu_Trn_Set.Trn_Set_CrLt_Chk, InSu_Trn_Set.Trn_Set_CrLt_Pass, "
                      +" InSu_Trn_Set.Trn_Set_CrLt_ODue, InSu_Trn_Set.Trn_Set_Cmt_Flag, InSu_Trn_Set.Trn_Set_Rt_Req, InSu_Trn_Set.Trn_Set_Rt_No, "
                      +" InSu_Trn_Set.Trn_Set_Cmt_Acc_Code, InSu_Trn_Set.T_C1, InSu_Trn_Set.T_C2, InSu_Trn_Set.T_Fl, InSu_Trn_Set.T_In, InSu_Trn_Set.Trn_Set_Event, "
                      +" InSu_Op_Pst.Op_Pst_Opr_Code, InSu_Op_Pst.Op_Pst_Tr_type, InSu_Op_Pst.Op_Pst_Tr_Code, InSu_Op_Pst.Op_Pst_Hld_Per, "
                      +" InSu_Op_Pst.Op_Pst_Rel_Per, InSu_Op_Pst.Op_Pst_Post_Per, InSu_Op_Pst.Op_Pst_Book_Per, InSu_Op_Pst.Op_Pst_Led_Int, "
                      +" InSu_Op_Pst.OP_Pst_Post_pw, InSu_Op_Pst.OP_Pst_Book_Pw, InSu_Op_Pst.OP_Pst_Led_OnOff, InSu_Op_Pst.OP_Pst_Led_Off_Chk, "
                      +" InSu_Op_Pst.T_C1 AS InSu_Op_Pst_T_C1, InSu_Op_Pst.T_C2 AS InSu_Op_Pst_T_C2, InSu_Op_Pst.T_Fl AS InSu_Op_Pst_T_Fl, InSu_Op_Pst.T_In AS InSu_Op_Pst_T_In "
                      +" FROM InSu_Trn_Set INNER JOIN InSu_Op_Pst ON InSu_Trn_Set.Trn_Set_Tr_Type = InSu_Op_Pst.Op_Pst_Tr_type AND InSu_Trn_Set.Trn_Set_Tr_Code = "
                      + " InSu_Op_Pst.Op_Pst_Tr_Code WHERE (InSu_Trn_Set.Trn_Set_Tr_Type = '" + trnSetTrType + "') AND (InSu_Trn_Set.Trn_Set_Tr_Code = '" + trnSetTrCode + "') AND "
                      + " (InSu_Op_Pst.Op_Pst_Opr_Code = '" + opPstOprCode + "')";
    }

    public static string GetSupplierByCode(string parAccCode)
    {
        return "SELECT Par_Acc_Ana_Req, Par_Acc_Bal_Amt, Par_Acc_Bal_Flag, Par_Acc_Bal_Te_Req, Par_Acc_Bo, Par_Acc_Code, Par_Acc_Comm, Par_Acc_Cur_Code, "
        +" Par_Acc_Name, Par_Acc_Nar_Amt_Req, Par_Acc_Nar_Amt_Type, Par_Acc_Perm, Par_Acc_Sec_Code, Par_Acc_Sta, Par_Acc_Tot_Cr, Par_Acc_Tot_Db, Par_Acc_Trn_DATE, "
        + " Par_Acc_Trn_Flag, Par_Acc_Type, Par_Acc_Unpost_Cr, Par_Acc_Unpost_Db, Par_Acc_Upd_DATE, T_C1, T_C2, T_Fl, T_In FROM PuMa_Par_Acc WHERE (Par_Acc_Code = '" + parAccCode + "')";
    }

    public static string GetStoreByCode(string strLocId)
    {
        return "SELECT Str_Loc_Grp, Str_Loc_Id, Str_Loc_Last_Upd, Str_Loc_Lst_Trn_DATE, Str_Loc_Name, Str_Loc_Neg_Stk, Str_Loc_Trn_Pst, Str_Loc_Type, T_C1, T_C2, T_Fl, T_In "
        + " FROM InMa_Str_Loc WHERE (Str_Loc_Id = '" + strLocId + "')";
    }

    public static string GetPOByRefNo(string pOHdrRef)
    {
        return "SELECT PO_Hdr_Acode, PO_Hdr_Cmt_Acc, PO_Hdr_Code, PO_Hdr_Com1, PO_Hdr_Com10, PO_Hdr_Com2, PO_Hdr_Com3, PO_Hdr_Com4, PO_Hdr_Com5, PO_Hdr_Com6, PO_Hdr_Com7, "
        +" PO_Hdr_Com8, PO_Hdr_Com9, PO_Hdr_DATE, PO_Hdr_Dcode, PO_Hdr_Ent_Prd, PO_Hdr_Exp_Typ, PO_Hdr_HPC_Flag, PO_Hdr_Opr_Code, PO_Hdr_Pcode, PO_Hdr_Prd_Cld, PO_Hdr_Ref, "
        +" PO_Hdr_Type, PO_Hdr_Value, Po_Hdr_Curr_Code, Po_Hdr_Due_Prd, Po_Hdr_Exch_rate, Po_Hdr_Ord_Date, T_C1, T_C2, T_Fl, T_In, po_hdr_due_date FROM PuTr_PO_Hdr  "
        + " WHERE (PO_Hdr_Ref = '" + pOHdrRef + "')";
    }

    public static string GetPODetByRefNo(string pODetRef)
    {
        return "SELECT PO_Det_Bal_Qty, PO_Det_Bal_Upd, PO_Det_Bat_No, PO_Det_Bin_Code, PO_Det_Code, PO_Det_Due_Dat, PO_Det_Exp_Dat, PO_Det_Exp_Lno, PO_Det_Icode, "
        +" PO_Det_Itm_Desc, PO_Det_Itm_Uom, PO_Det_Lin_Amt, PO_Det_Lin_Net, PO_Det_Lin_Qty, PO_Det_Lin_Rat, PO_Det_Lno, PO_Det_OC_Flag, PO_Det_Org_QTY, PO_Det_Pr_Lno, "
        + " PO_Det_Pr_Ref, PO_Det_Ref, PO_Det_Sfx, PO_Det_Str_Code, PO_Det_Type, PO_Det_Unt_Wgt, T_C1, T_C2, T_Fl, T_In FROM PuTr_PO_Det WHERE (PO_Det_Ref = '" + pODetRef + "') "
        +" ORDER BY PO_Det_Lno";
    }
    #endregion Purchase Order

    #region Material Receive

    public static string GetMaxMrrRefNo(DateTime chkPeriod)
    {
        return "SELECT isnull(MAX(RIGHT(Trn_Hdr_Ref, 5)),0) AS MaxMrrRefNo FROM InTr_Trn_Hdr WHERE (Trn_Hdr_Type = 'RC') AND (Trn_Hdr_Code = 'PO') AND "
            + " (CONVERT(DATETIME, Trn_Hdr_DATE, 103) >= CONVERT(datetime, '" + chkPeriod + "', 103))";
    }

    public static string GetMaxBarcodeNo()
    {
        string sql = "";
        return sql = "SELECT isnull(MAX(RIGHT(itm_det_barcode, 8)),0)+1  from [InMa_Itm_BarCode]";
    }


    public static string DeleteMRRDet(string trnDetType, string trnDetCode, string trnDetRef)
    {
        return "DELETE  FROM  InTr_Trn_Det WHERE Trn_Det_Type= '" + trnDetType + "' AND Trn_Det_Code= '" + trnDetCode + "' AND Trn_Det_Ref= '" + trnDetRef + "'";
    }

    public static string DeleteMrrSerial(string itmDdetRef)
    {
        return "DELETE  FROM   InMa_Itm_Serial  WHERE itm_det_ref= '" + itmDdetRef + "'";
    }

    public static string GetMaxRateId(DateTime chkPeriod)
    {
        return "SELECT isnull(MAX(RIGHT(itm_rate_id, 6)),0) AS MaxRateId FROM  InMa_Itm_Rate WHERE (CONVERT(DATETIME, itm_rate_trndate, 103) >= CONVERT(datetime, '" + chkPeriod + "', 103))";
    }

    public static string GetMaxRateIdGrp()
    {
        return "SELECT isnull(MAX(RIGHT(itm_rate_id_grp, 6)),0) AS MaxRateId FROM InMa_Itm_Rate";
    }

    public static string GetRateByItmStoreDate(string itmRateIcode, string itmRateScode, DateTime itmRateTrndate)
    {
        return "SELECT itm_rate_icode, itm_rate_id, itm_rate_id_grp, itm_rate_lineno, itm_rate_qty, itm_rate_rate, itm_rate_scode, itm_rate_trn_ref, itm_rate_trndate "
        + " FROM InMa_Itm_Rate WHERE (itm_rate_icode = '" + itmRateIcode + "') AND (itm_rate_scode = '" + itmRateScode + "') AND (itm_rate_trndate = '" + itmRateTrndate + "') order by itm_rate_lineno";
    }

    public static string InsertItemRate(string itmRateScode, string itmRateIcode, string itmRateTrnRef, DateTime itmRateTrndate, decimal itmRateQty, decimal itmRateRate,
        int itmRateLineno, string itmRateId, string itmRateIdGrp)
    {
        string sql = "";
        return sql=@"INSERT INTO [InMa_Itm_Rate] ([itm_rate_scode], [itm_rate_icode], [itm_rate_trn_ref], [itm_rate_trndate], [itm_rate_qty], [itm_rate_rate], [itm_rate_lineno], "
            + " [itm_rate_id], [itm_rate_id_grp]) VALUES ('" + itmRateScode + "', '" + itmRateIcode + "', '" + itmRateTrnRef + "', Convert(datetime,'" + itmRateTrndate + "',103), "
            + " " + itmRateQty + ", " + itmRateRate + ", " + itmRateLineno + ", '" + itmRateId + "', '" + itmRateIdGrp + "')";
    }

    public static string GetMrrRefFormat()
    {
        return "SELECT SA_Mn, SA_No, SA_Post, SA_Prefix, SA_Sep, SA_Type, SA_Yr FROM SaTr_SA_Number WHERE (SA_Type = 'RC')";
    }

    public static string GetPoDetByItem(string pODetRef, string pODetIcode)
    {
        string sql = "";
        return sql=@"SELECT PO_Det_Bal_Qty, PO_Det_Bal_Upd, PO_Det_Bat_No, PO_Det_Bin_Code, PO_Det_Code, PO_Det_Due_Dat, PO_Det_Exp_Dat, PO_Det_Exp_Lno, PO_Det_Icode, "
        +" PO_Det_Itm_Desc, PO_Det_Itm_Uom, PO_Det_Lin_Amt, PO_Det_Lin_Net, PO_Det_Lin_Qty, PO_Det_Lin_Rat, PO_Det_Lno, PO_Det_OC_Flag, PO_Det_Org_QTY, PO_Det_Pr_Lno, "
        + " PO_Det_Pr_Ref, PO_Det_Ref, PO_Det_Sfx, PO_Det_Str_Code, PO_Det_Type, PO_Det_Unt_Wgt, T_C1, T_C2, T_Fl,PO_Det_Ins_QTY,PO_Det_Qc_QTY,T_In FROM PuTr_PO_Det WHERE (PO_Det_Ref = '" + pODetRef + "') "
        + " AND (PO_Det_Icode = '" + pODetIcode + "')";
    }

    public static string GetStkCtlByItemStore(string stkCtlICode, string stkCtlSCode)
    {
        string sql = "";
        return sql="SELECT Stk_Ctl_Ave_Val, Stk_Ctl_Cur_Stk, Stk_Ctl_FIFO_Val, Stk_Ctl_Free_Stk, Stk_Ctl_ICode, Stk_Ctl_Ind_Stk, Stk_Ctl_LIFO_Val, Stk_Ctl_Lat_Val, "
        +" Stk_Ctl_Lst_Iss_Dat, Stk_Ctl_Lst_Rec_Dat, Stk_Ctl_Max_Stk, Stk_Ctl_Min_Stk, Stk_Ctl_On_Ord_Stk, Stk_Ctl_Quot_Stk, Stk_Ctl_Reord_Stk, Stk_Ctl_SCode,  "
        + " Stk_Ctl_Std_Val, Stk_Ctl_Str_Grp, T_C1, T_C2, T_Fl, T_In FROM InMa_Stk_Ctl WHERE (Stk_Ctl_ICode = '" + stkCtlICode + "') AND (Stk_Ctl_SCode = '" + stkCtlSCode + "')";
    }

    public static string GetMrrDataByMrrItem(string stkValTrnRef, string stkValItmCode, string stkValStrCode)
    {
        return "SELECT F_C1, F_C2, F_C3, F_C4, Stk_Val_Ave_Rate, Stk_Val_Itm_Code, Stk_Val_Itm_Desc, Stk_Val_Itm_Qty, Stk_Val_Lat_Rate, Stk_Val_Std_Rate, Stk_Val_Str_Code, "
        +" Stk_Val_Trn_Code, Stk_Val_Trn_Date, Stk_Val_Trn_Ref, Stk_Val_Trn_Type, sl_no FROM InMa_Stk_Val WHERE (Stk_Val_Trn_Type = 'RC') AND (Stk_Val_Trn_Code = 'PO') AND "
        + " (Stk_Val_Trn_Ref = '" + stkValTrnRef + "') AND (Stk_Val_Itm_Code = '" + stkValItmCode + "') AND (Stk_Val_Str_Code = '" + stkValStrCode + "')";
    }

    public static string InsertStkVal(string stkValTrnType, string stkValTrnCode, string stkValTrnRef, DateTime stkValTrnDate, string stkValItmCode,
        string stkValItmDesc, string stkValStrCode, decimal stkValLatRate, decimal stkValAveRate, decimal stkValStdRate, double stkValItmQty, string fC1,
        string fC2, string fC3, string fC4)
    {
        return "INSERT INTO [InMa_Stk_Val] ([Stk_Val_Trn_Type], [Stk_Val_Trn_Code], [Stk_Val_Trn_Ref], [Stk_Val_Trn_Date], [Stk_Val_Itm_Code], [Stk_Val_Itm_Desc], "
            +" [Stk_Val_Str_Code], [Stk_Val_Lat_Rate], [Stk_Val_Ave_Rate], [Stk_Val_Std_Rate], [Stk_Val_Itm_Qty], [F_C1], [F_C2], [F_C3], [F_C4]) VALUES "
            + " ('" + stkValTrnType + "', '" + stkValTrnCode + "', '" + stkValTrnRef + "', '" + stkValTrnDate + "', '" + stkValItmCode + "', '" + stkValItmDesc + "', "
            + " '" + stkValStrCode + "', " + stkValLatRate + ", " + stkValAveRate + ", " + stkValStdRate + ", " + stkValItmQty + ", '" + fC1 + "', "
            + " '" + fC2 + "', '" + fC3 + "', '" + fC4 + "')";
    }

    public static string GetItmStkByICode(string itmStkIcode)
    {
        return "SELECT Itm_Stk_AVE_Rat, Itm_Stk_BSP_Rat, Itm_Stk_Cst_Meth, Itm_Stk_Cur, Itm_Stk_Icode, Itm_Stk_LAT_Rat, Itm_Stk_OSP_Rat, Itm_Stk_STD_Rat, T_C1, T_C2, T_Fl, "
        + " T_In FROM InMa_Itm_Stk WHERE (Itm_Stk_Icode = '" + itmStkIcode + "')";
    }

    public static string EditItmStk(double itmStkCur, string itmStkCstMeth, int itmStkBSPRat, int itmStkOSPRat, decimal itmStkLATRat, decimal itmStkAVERat,
        string tC1, string tC2, string tFl, int tIn, string itmStkIcode)
    {
        return "UPDATE [InMa_Itm_Stk]  SET [Itm_Stk_Cur]= " + itmStkCur + ", [Itm_Stk_Cst_Meth]= '" + itmStkCstMeth + "', [Itm_Stk_BSP_Rat]= " + itmStkBSPRat + ", "
            + " [Itm_Stk_OSP_Rat]= " + itmStkOSPRat + ", [Itm_Stk_LAT_Rat]= " + itmStkLATRat + ", [Itm_Stk_AVE_Rat]= " + itmStkAVERat + ", [T_C1]= '" + tC1 + "', "
            + " [T_C2]= '" + tC2 + "', [T_Fl]= '" + tFl + "', [T_In]= " + tIn + " WHERE [Itm_Stk_Icode]= '" + itmStkIcode + "'";
    }

    public static string InsertItmStk(string itmStkIcode, double itmStkCur, string itmStkCstMeth, int itmStkBSPRat, int itmStkOSPRat, decimal itmStkSTDRat,
        decimal itmStkLATRat, decimal itmStkAVERat, string tC1, string tC2, string tFl, int tIn)
    {
        return "INSERT INTO [InMa_Itm_Stk] ([Itm_Stk_Icode], [Itm_Stk_Cur], [Itm_Stk_Cst_Meth], [Itm_Stk_BSP_Rat], [Itm_Stk_OSP_Rat], [Itm_Stk_STD_Rat], [Itm_Stk_LAT_Rat], "
            + " [Itm_Stk_AVE_Rat], [T_C1], [T_C2], [T_Fl], [T_In]) VALUES ('" + itmStkIcode + "', " + itmStkCur + ", '" + itmStkCstMeth + "', " + itmStkBSPRat + ", "
            + " " + itmStkOSPRat + ", " + itmStkSTDRat + ", " + itmStkLATRat + ", " + itmStkAVERat + ", '" + tC1 + "', '" + tC2 + "', '" + tFl + "', " + tIn + ")";
    }

    public static string GetDataByItemCode(string itemCode, string storeCode)
    {
        return "SELECT SLNo, ItemCode, ItemName, StoreCode, StoreName, UOM, MSL, MslTouchedDate FROM tbl_MinimumStockLevel where ItemCode= '" + itemCode + "' and StoreCode= '" + storeCode + "'";
    }

    public static string InsertMRRDet(string trnDetType, string trnDetCode, string trnDetRef, short trnDetLno, string trnDetSfx, int trnDetExpLno, string trnDetIcode,
        string trnDetItmDesc, string trnDetItmUom, string trnDetStrCode, string trnDetBinCode, string trnDetOrdRef, short trnDetOrdLno, string trnDetBatNo,
        DateTime trnDetExpDat, DateTime trnDetBookDat, double trnDetLinQty, int trnDetUntWgt, decimal trnDetLinRat, decimal trnDetLinAmt,
        decimal trnDetLinNet, string tC1, string tC2, string tFl, int tIn, int trnDetBalQty)
    {
        string sql = "";
        return sql="INSERT INTO [InTr_Trn_Det] ([Trn_Det_Type], [Trn_Det_Code], [Trn_Det_Ref], [Trn_Det_Lno], [Trn_Det_Sfx], [Trn_Det_Exp_Lno], [Trn_Det_Icode], [Trn_Det_Itm_Desc], "
            +" [Trn_Det_Itm_Uom], [Trn_Det_Str_Code], [Trn_Det_Bin_Code], [Trn_Det_Ord_Ref], [Trn_Det_Ord_Lno], [Trn_Det_Bat_No], [Trn_Det_Exp_Dat], [Trn_Det_Book_Dat], "
            +" [Trn_Det_Lin_Qty], [Trn_Det_Unt_Wgt], [Trn_Det_Lin_Rat], [Trn_Det_Lin_Amt], [Trn_Det_Lin_Net], [T_C1], [T_C2], [T_Fl], [T_In], [Trn_Det_Bal_Qty]) VALUES "
            + " ('" + trnDetType + "', '" + trnDetCode + "', '" + trnDetRef + "', " + trnDetLno + ", '" + trnDetSfx + "', " + trnDetExpLno + ", '" + trnDetIcode + "', "
            + " '" + trnDetItmDesc + "', '" + trnDetItmUom + "', '" + trnDetStrCode + "', '" + trnDetBinCode + "', '" + trnDetOrdRef + "', " + trnDetOrdLno + ", "
            + " '" + trnDetBatNo + "', Convert(Datetime,'" + trnDetExpDat + "',103), Convert(datetime,'" + trnDetBookDat + "',103), " + trnDetLinQty + ", " + trnDetUntWgt + ", " + trnDetLinRat + ", "
            + " " + trnDetLinAmt + ", " + trnDetLinNet + ", '" + tC1 + "', '" + tC2 + "', '" + tFl + "', " + tIn + ", " + trnDetBalQty + ")";
    }

    public static string InsertItemSerial(string itmDetIcode, string itmDetSerialNo, string itmDetRef, string itmDetStrCode, string itmDetTrnType, string itmDetTrnCode,
        DateTime itmDetDate, string itmStatus, string itmRateId)
    {
        string sql = "";
        return sql="INSERT INTO [InMa_Itm_Serial] ([itm_det_icode], [itm_det_serial_no], [itm_det_ref], [itm_det_str_code], [itm_det_trn_type], [itm_det_trn_code], [itm_det_date], "
            + " [itm_status], [itm_rate_id]) VALUES ('" + itmDetIcode + "', '" + itmDetSerialNo + "', '" + itmDetRef + "', '" + itmDetStrCode + "', '" + itmDetTrnType + "', "
            + " '" + itmDetTrnCode + "', Convert(Datetime,'" + itmDetDate + "',103), '" + itmStatus + "', '" + itmRateId + "')";
    }

    public static string InsertItemSerialtemp(string Poref,string itmDetIcode, string itmDetSerialNo, string itmDetRef, string itmDetStrCode, string itmDetTrnType, string itmDetTrnCode,
       DateTime itmDetDate, string itmStatus, string itmRateId)
    {
        string sql = "";
        return sql = "INSERT INTO [InMa_Itm_Serial_temp] ([itm_po_ref],[itm_det_icode], [itm_det_serial_no], [itm_det_ref], [itm_det_str_code], [itm_det_trn_type], [itm_det_trn_code], [itm_det_date], "
            + " [itm_status], [itm_rate_id]) VALUES ('" + Poref + "','" + itmDetIcode + "', '" + itmDetSerialNo + "', '" + itmDetRef + "', '" + itmDetStrCode + "', '" + itmDetTrnType + "', "
            + " '" + itmDetTrnCode + "', Convert(Datetime,'" + itmDetDate + "',103), '" + itmStatus + "', '" + itmRateId + "')";
    }

    public static string InsertItemBarcode(string Poref, string itmDetIcode, string itmDetSerialNo, string itmDetRef, string itmDetStrCode, string itmDetTrnType, string itmDetTrnCode,
       DateTime itmDetDate, string itmStatus, string itmRateId,int quantity,string barcode)
    {
        string sql = "";
        return sql = "INSERT INTO [InMa_Itm_BarCode] ([itm_po_ref],[itm_det_icode], [itm_det_serial_no], [itm_det_ref], [itm_det_str_code], [itm_det_trn_type], [itm_det_trn_code], [itm_det_date], "
            + " [itm_status], [itm_rate_id],[itm_det_quantity],[itm_det_barcode]) VALUES ('" + Poref + "','" + itmDetIcode + "', '" + itmDetSerialNo + "', '" + itmDetRef + "', '" + itmDetStrCode + "', '" + itmDetTrnType + "', "
            + " '" + itmDetTrnCode + "', Convert(Datetime,'" + itmDetDate + "',103), '" + itmStatus + "', '" + itmRateId + "','" + quantity + "','" + barcode  + "')";
    }


    public static string UpdatePoQty(double pODetOrgQTY, double pODetBalQty, double pODetQcQty, string pODetBalUpd, string pODetRef, string pODetIcode)
    {
        return "UPDATE [PuTr_PO_Det]  SET  [PO_Det_Org_QTY]= " + pODetOrgQTY + ", [PO_Det_Bal_Qty]= " + pODetBalQty + ", [PO_Det_Bal_Upd]= '" + pODetBalUpd + "',[PO_Det_Qc_QTY]= " + pODetQcQty + "  "
        + " WHERE [PO_Det_Ref]= '" + pODetRef + "' AND [PO_Det_Icode]= '" + pODetIcode + "'";
    }

    public static string EditStkCtl(string stkCtlStrGrp, double stkCtlCurStk, double stkCtlFreeStk, decimal stkCtlStdVal, decimal stkCtlAveVal, decimal stkCtlLatVal,
        DateTime stkCtlLstRecDat, string tC1, string tC2, string tFl, int tIn, string stkCtlICode, string stkCtlSCode)
    {
        return "UPDATE [InMa_Stk_Ctl]  SET [Stk_Ctl_Str_Grp]= '" + stkCtlStrGrp + "', [Stk_Ctl_Cur_Stk]= " + stkCtlCurStk + ", [Stk_Ctl_Free_Stk]= " + stkCtlFreeStk + ", "
            + " [Stk_Ctl_Std_Val]= " + stkCtlStdVal + ", [Stk_Ctl_Ave_Val]= " + stkCtlAveVal + ", [Stk_Ctl_Lat_Val]= " + stkCtlLatVal + ", [Stk_Ctl_Lst_Rec_Dat]= '" + stkCtlLstRecDat + "' , "
            + " [T_C1]= '" + tC1 + "', [T_C2]= '" + tC2 + "', [T_Fl]= '" + tFl + "', [T_In]= " + tIn + " WHERE [Stk_Ctl_ICode]= '" + stkCtlICode + "' AND [Stk_Ctl_SCode]= '" + stkCtlSCode + "'";
    }

    public static string EditMRRHdr(string trnHdrPcode, string trnHdrDcode, string trnHdrAcode, DateTime trnHdrDate, string trnHdrCom1, string trnHdrCom2,
        string trnHdrCom3, string trnHdrCom4, string trnHdrCom5, string trnHdrCom6, string trnHdrCom7, string trnHdrCom8, string trnHdrCom9, string trnHdrCom10,
        decimal trnHdrValue, string trnHdrHRPBFlag, string trnHdrEntPrd, string trnHdrOprCode, string trnHdrPrdCld, string trnHdrExpTyp, string trnHdrLedInt,
        string trnHdrDCNo, string trnHdrEIFlg, string trnHdrCno, string tC1, string tC2, string tFl, int tIn, int trnHdrexcDuty, DateTime trnHdrDcDate,
        string trnHdrCIDate, string trnHdrPassNo, string trnHdrType, string trnHdrCode, string trnHdrRef)
    {
        return "UPDATE [InTr_Trn_Hdr]  SET [Trn_Hdr_Pcode]= '" + trnHdrPcode + "', [Trn_Hdr_Dcode]= '" + trnHdrDcode + "', [Trn_Hdr_Acode]= '" + trnHdrAcode + "', "
            + " [Trn_Hdr_DATE]= '" + trnHdrDate + "', [Trn_Hdr_Com1]= '" + trnHdrCom1 + "', [Trn_Hdr_Com2]= '" + trnHdrCom2 + "', [Trn_Hdr_Com3]= '" + trnHdrCom3 + "', "
            + " [Trn_Hdr_Com4]= '" + trnHdrCom4 + "', [Trn_Hdr_Com5]= '" + trnHdrCom5 + "', [Trn_Hdr_Com6]= '" + trnHdrCom6 + "', [Trn_Hdr_Com7]= '" + trnHdrCom7 + "', "
            + " [Trn_Hdr_Com8]= '" + trnHdrCom8 + "', [Trn_Hdr_Com9]= '" + trnHdrCom9 + "', [Trn_Hdr_Com10]= '" + trnHdrCom10 + "', [Trn_Hdr_Value]= " + trnHdrValue + ", "
            + " [Trn_Hdr_HRPB_Flag]= '" + trnHdrHRPBFlag + "', [Trn_Hdr_Ent_Prd]= '" + trnHdrEntPrd + "', [Trn_Hdr_Opr_Code]= '" + trnHdrOprCode + "', "
            + " [Trn_Hdr_Prd_Cld]= '" + trnHdrPrdCld + "', [Trn_Hdr_Exp_Typ]= '" + trnHdrExpTyp + "', [Trn_Hdr_Led_Int]= '" + trnHdrLedInt + "', [Trn_Hdr_DC_No]= '" + trnHdrDCNo + "', "
            + " [Trn_Hdr_EI_Flg]= '" + trnHdrEIFlg + "', [Trn_Hdr_Cno]= '" + trnHdrCno + "', [T_C1]= '" + tC1 + "', [T_C2]= '" + tC2 + "', [T_Fl]= '" + tFl + "', "
            + " [T_In]= " + tIn + ", [Trn_Hdr_exc_duty]= " + trnHdrexcDuty + ", [Trn_Hdr_Dc_Date]= '" + trnHdrDcDate + "', [Trn_Hdr_CI_Date]= '" + trnHdrCIDate + "', "
            + " [Trn_Hdr_Pass_No]= '" + trnHdrPassNo + "' WHERE [Trn_Hdr_Type]= '" + trnHdrType + "' AND [Trn_Hdr_Code]= '" + trnHdrCode + "' AND [Trn_Hdr_Ref]= '" + trnHdrRef + "'";
    }

    public static string InsertMRRHdr(string trnHdrType, string trnHdrCode, string trnHdrRef, string trnHdrPcode, string trnHdrDcode, string trnHdrAcode,
        DateTime trnHdrDate, string trnHdrCom1, string trnHdrCom2, string trnHdrCom3, string trnHdrCom4, string trnHdrCom5, string trnHdrCom6, string trnHdrCom7,
        string trnHdrCom8, string trnHdrCom9, string trnHdrCom10, decimal trnHdrValue, string trnHdrHRPBFlag, string trnHdrEntPrd, string trnHdrOprCode,
        string trnHdrPrdCld, string trnHdrExpTyp, string trnHdrLedInt, string trnHdrDCNo, string trnHdrEIFlg, string trnHdrCno, string tC1, string tC2,
        string tFl, int tIn, int trnHdrExcDuty, DateTime trnHdrDcDate, string trnHdrCIDate, string trnHdrPassNo)
    {
        string sql = "";
        return sql="INSERT INTO [InTr_Trn_Hdr] ([Trn_Hdr_Type], [Trn_Hdr_Code], [Trn_Hdr_Ref], [Trn_Hdr_Pcode], [Trn_Hdr_Dcode], [Trn_Hdr_Acode], [Trn_Hdr_DATE], [Trn_Hdr_Com1], "
            +" [Trn_Hdr_Com2], [Trn_Hdr_Com3], [Trn_Hdr_Com4], [Trn_Hdr_Com5], [Trn_Hdr_Com6], [Trn_Hdr_Com7], [Trn_Hdr_Com8], [Trn_Hdr_Com9], [Trn_Hdr_Com10], [Trn_Hdr_Value], "
            +" [Trn_Hdr_HRPB_Flag], [Trn_Hdr_Ent_Prd], [Trn_Hdr_Opr_Code], [Trn_Hdr_Prd_Cld], [Trn_Hdr_Exp_Typ], [Trn_Hdr_Led_Int], [Trn_Hdr_DC_No], [Trn_Hdr_EI_Flg], "
            + " [Trn_Hdr_Cno], [T_C1], [T_C2], [T_Fl], [T_In], [Trn_Hdr_exc_duty], [Trn_Hdr_Dc_Date], [Trn_Hdr_CI_Date], [Trn_Hdr_Pass_No]) VALUES ( '" + trnHdrType + "', "
            + " '" + trnHdrCode + "', '" + trnHdrRef + "', '" + trnHdrPcode + "', '" + trnHdrDcode + "', '" + trnHdrAcode + "', Convert(datetime,'" + trnHdrDate + "',103), '" + trnHdrCom1 + "', "
            + " '" + trnHdrCom2 + "', '" + trnHdrCom3 + "', '" + trnHdrCom4 + "', '" + trnHdrCom5 + "', '" + trnHdrCom6 + "', '" + trnHdrCom7 + "', '" + trnHdrCom8 + "', "
            + " '" + trnHdrCom9 + "', '" + trnHdrCom10 + "', " + trnHdrValue + ", '" + trnHdrHRPBFlag + "', '" + trnHdrEntPrd + "', '" + trnHdrOprCode + "', "
            + " '" + trnHdrPrdCld + "', '" + trnHdrExpTyp + "', '" + trnHdrLedInt + "', '" + trnHdrDCNo + "', '" + trnHdrEIFlg + "', '" + trnHdrCno + "',"
            + " '" + tC1 + "', '" + tC2 + "', '" + tFl + "', " + tIn + ", " + trnHdrExcDuty + ", Convert(Datetime,'" + trnHdrDcDate + "',103), '" + trnHdrCIDate + "', '" + trnHdrPassNo + "')";
    }

    public static string UpdateQuery(string mslTouchedDate, string itemCode, string storeCode)
    {
        return "UPDATE [tbl_MinimumStockLevel] SET [MslTouchedDate] = '" + mslTouchedDate + "' WHERE [ItemCode] = '" + itemCode + "' and StoreCode= '" + storeCode + "'";
    }

    public static string UpdateStkVal(DateTime stkValTrnDate, string stkValItmDesc, decimal stkValLatRate, decimal stkValAveRate, decimal stkValStdRate,
        double stkValItmQty, string fC1, string fC2, string fC3, string fC4, string stkValTrnType, string stkValTrnCode, string stkValTrnRef, string stkValItmCode,
        string stkValStrCode)
    {
        return "UPDATE [InMa_Stk_Val]  SET [Stk_Val_Trn_Date]= '" + stkValTrnDate + "', [Stk_Val_Itm_Desc]= '" + stkValItmDesc + "', [Stk_Val_Lat_Rate]= " + stkValLatRate + ", "
            + " [Stk_Val_Ave_Rate]= " + stkValAveRate + ", [Stk_Val_Std_Rate]= " + stkValStdRate + ", [Stk_Val_Itm_Qty]= " + stkValItmQty + ", [F_C1]= '" + fC1 + "', "
            + " [F_C2]= '" + fC2 + "', [F_C3]= '" + fC3 + "', [F_C4]= '" + fC4 + "' WHERE [Stk_Val_Trn_Type]= '" + stkValTrnType + "' AND [Stk_Val_Trn_Code]= '" + stkValTrnCode + "' "
            + " AND [Stk_Val_Trn_Ref]= '" + stkValTrnRef + "' AND [Stk_Val_Itm_Code]= '" + stkValItmCode + "' AND [Stk_Val_Str_Code]= '" + stkValStrCode + "'";
    }

    public static string InsertStkCtl(string stkCtlSCode, string stkCtlICode, string stkCtlStrGrp, double stkCtlCurStk, double stkCtlFreeStk, int stkCtlOnOrdStk,
        int stkCtlIndStk, int stkCtlQuotStk, int stkCtlMinStk, int stkCtlReordStk, int stkCtlMaxStk, decimal stkCtlStdVal, decimal stkCtlAveVal, decimal stkCtlLatVal,
        int stkCtlFIFOVal, int stkCtlLIFOVal, DateTime stkCtlLstRecDat, string stkCtlLstIssDat, string tC1, string tC2, string tFl, int tIn)
    {
        return "INSERT INTO [InMa_Stk_Ctl] ([Stk_Ctl_SCode], [Stk_Ctl_ICode], [Stk_Ctl_Str_Grp], [Stk_Ctl_Cur_Stk], [Stk_Ctl_Free_Stk], [Stk_Ctl_On_Ord_Stk], [Stk_Ctl_Ind_Stk], "
            +" [Stk_Ctl_Quot_Stk], [Stk_Ctl_Min_Stk], [Stk_Ctl_Reord_Stk], [Stk_Ctl_Max_Stk], [Stk_Ctl_Std_Val], [Stk_Ctl_Ave_Val], [Stk_Ctl_Lat_Val], [Stk_Ctl_FIFO_Val], "
            + " [Stk_Ctl_LIFO_Val], [Stk_Ctl_Lst_Rec_Dat], [Stk_Ctl_Lst_Iss_Dat], [T_C1], [T_C2], [T_Fl], [T_In]) VALUES ( '" + stkCtlSCode + "', '" + stkCtlICode + "', "
            + " '" + stkCtlStrGrp + "', " + stkCtlCurStk + ", " + stkCtlFreeStk + ", " + stkCtlOnOrdStk + ", " + stkCtlIndStk + ", " + stkCtlQuotStk + ", "
            + " " + stkCtlMinStk + ", " + stkCtlReordStk + ", " + stkCtlMaxStk + ", " + stkCtlStdVal + ", " + stkCtlAveVal + ", " + stkCtlLatVal + ", "
            + " " + stkCtlFIFOVal + ", " + stkCtlLIFOVal + ", '" + stkCtlLstRecDat + "', '" + stkCtlLstIssDat + "', '" + tC1 + "', '" + tC2 + "', '" + tFl + "', " + tIn + ")";
    }

    public static string GetMrrHdrByRefNo(string trnHdrRef)
    {
        return "SELECT T_C1, T_C2, T_Fl, T_In, Trn_Hdr_Acode, Trn_Hdr_CI_Date, Trn_Hdr_Cno, Trn_Hdr_Code, Trn_Hdr_Com1, Trn_Hdr_Com10, Trn_Hdr_Com2, Trn_Hdr_Com3, Trn_Hdr_Com4, "
            +" Trn_Hdr_Com5, Trn_Hdr_Com6, Trn_Hdr_Com7, Trn_Hdr_Com8, Trn_Hdr_Com9, Trn_Hdr_DATE, Trn_Hdr_DC_No, Trn_Hdr_Dc_Date, Trn_Hdr_Dcode, Trn_Hdr_EI_Flg, Trn_Hdr_Ent_Prd, "
            +" Trn_Hdr_Exp_Typ, Trn_Hdr_HRPB_Flag, Trn_Hdr_Led_Int, Trn_Hdr_Opr_Code, Trn_Hdr_Pass_No, Trn_Hdr_Pcode, Trn_Hdr_Prd_Cld, Trn_Hdr_Ref, Trn_Hdr_Type, Trn_Hdr_Value, "
            + " Trn_Hdr_exc_duty FROM InTr_Trn_Hdr WHERE (Trn_Hdr_Type = 'RC') AND (Trn_Hdr_Code = 'PO') AND (Trn_Hdr_Ref = '" + trnHdrRef + "')";
    }

    public static string GetMrrDetByRefNo(string trnDetRef)
    {
        return "SELECT T_C1, T_C2, T_Fl, T_In, Trn_Det_Bal_Qty, Trn_Det_Bat_No, Trn_Det_Bin_Code, Trn_Det_Book_Dat, Trn_Det_Code, Trn_Det_Exp_Dat, Trn_Det_Exp_Lno, Trn_Det_Icode, "
            +" Trn_Det_Itm_Desc, Trn_Det_Itm_Uom, Trn_Det_Lin_Amt, Trn_Det_Lin_Net, Trn_Det_Lin_Qty, Trn_Det_Lin_Rat, Trn_Det_Lno, Trn_Det_Ord_Lno, Trn_Det_Ord_Ref, Trn_Det_Ref, "
            +" Trn_Det_Sfx, Trn_Det_Str_Code, Trn_Det_Type, Trn_Det_Unt_Wgt FROM InTr_Trn_Det WHERE (Trn_Det_Type = 'RC') AND (Trn_Det_Code = 'PO') AND "
            + " (Trn_Det_Ref = '" + trnDetRef + "') ORDER BY Trn_Det_Lno";
    }

    public static string GetSerialByMrrRef(string itmDetRef, string itmDetIcode)
    {
        return "SELECT itm_det_date, itm_det_icode, itm_det_ref, itm_det_serial_no, itm_det_str_code, itm_det_trn_code, itm_det_trn_type, itm_rate_id, itm_status, "
            + " sl_no FROM InMa_Itm_Serial WHERE (itm_det_ref = '" + itmDetRef + "') AND (itm_det_icode = '" + itmDetIcode + "')";
    }

    public static string GetSerialByItem(string itmDetIcode)
    {
        return "SELECT itm_det_date, itm_det_icode, itm_det_ref, itm_det_serial_no, itm_det_str_code, itm_det_trn_code, itm_det_trn_type, itm_rate_id, itm_status, sl_no FROM InMa_Itm_Serial WHERE (itm_det_icode = '" + itmDetIcode + "')";
    }

    public static string GetPendingPoData(string pODetRef)
    {
        string sql = "";
        return sql = @"SELECT PO_Det_Qc_QTY as PO_Det_Bal_Qty, PO_Det_Bal_Upd, PO_Det_Bat_No, PO_Det_Bin_Code, PO_Det_Code, PO_Det_Due_Dat, PO_Det_Exp_Dat, PO_Det_Exp_Lno, PO_Det_Icode, "
       + " PO_Det_Itm_Desc, PO_Det_Itm_Uom, PO_Det_Lin_Amt, PO_Det_Lin_Net, PO_Det_Lin_Qty, PO_Det_Lin_Rat, PO_Det_Lno, PO_Det_OC_Flag, PO_Det_Org_QTY, PO_Det_Pr_Lno, "
       + " PO_Det_Pr_Ref, PO_Det_Ref, PO_Det_Sfx, PO_Det_Str_Code, PO_Det_Type, PO_Det_Unt_Wgt, T_C1, T_C2, T_Fl, T_In,dbo.[GetSerialNumberbyPoItemcode](a.PO_Det_Ref,a.PO_Det_Icode)  as Po_Det_Serial_No FROM PuTr_PO_Det a WHERE (PO_Det_Ref = '" + pODetRef + "') ";
        
    }

    public static string GetAllPo()
    {
        return @"select po_hdr_ref,po_hdr_pcode,po_hdr_code,po_hdr_com1 from PuTr_PO_Hdr 
                INNER JOIN PuTr_PO_Det ON PuTr_PO_Hdr.PO_Hdr_Ref=PuTr_PO_Det.PO_Det_Ref 
                where PuTr_PO_Hdr.PO_Hdr_Status='APP' and  PuTr_PO_Det.PO_Det_Qc_QTY>0 
                group by  po_hdr_ref,po_hdr_pcode,po_hdr_code,po_hdr_com1
                having sum(PuTr_PO_Det.PO_Det_Qc_QTY)>0 
                order by putr_po_hdr.PO_Hdr_Com1";
    }


    #endregion Material Receive

    #region Item Requisition

    public static string GetTrnByType(string trnSetTrType,string opPstOprCode)
    {
        string sql = "";

        //return "SELECT a.*,b.* "
        //              +" FROM InSu_Trn_Set a INNER JOIN InSu_Op_Pst b ON a.Trn_Set_Tr_Type = b.Op_Pst_Tr_type AND "
        //              +" a.Trn_Set_Tr_Code = b.Op_Pst_Tr_Code WHERE (a.Trn_Set_Tr_Type = '"+trnSetTrType+"') AND "
        //              +" (b.Op_Pst_Opr_Code = '"+opPstOprCode+"')";

        return sql = "SELECT a.*,b.* "
                      + " FROM InSu_Trn_Set a INNER JOIN InSu_Op_Pst b ON a.Trn_Set_Tr_Type = b.Op_Pst_Tr_type AND "
                      + " a.Trn_Set_Tr_Code = b.Op_Pst_Tr_Code WHERE (a.Trn_Set_Tr_Type = '" + trnSetTrType + "')";                  	

 	
    }

    public static string GetSupplierByPendingInvoice(string POType)
    {
        string sql="";
        return sql = @"select distinct Par_Acc_Code,Par_Acc_Name,InvoiceReceived from intr_trn_hdr a 
                    inner join AccPaybleProgress b on a.Trn_Hdr_Ref=b.ReferenceNumber
                    inner join PuMa_Par_Acc d on d.Par_Acc_Code=a.Trn_Hdr_Pcode 
                    inner join PRTOMRR e on e.MRR=b.ReferenceNumber
                    where Status='Y' and e.PurType='" + POType + "' order by Par_Acc_Name";

    }
    public static string GetSupplierByPendingPO()
    {
        string sql = "";
        return sql = @"select distinct Par_Acc_Code,Par_Acc_Name from intr_trn_hdr a 
                    inner join AccPaybleProgress b on a.Trn_Hdr_Ref=b.ReferenceNumber
                    inner join PuMa_Par_Acc d on d.Par_Acc_Code=a.Trn_Hdr_Pcode 
                    inner join PRTOMRR e on e.MRR=b.ReferenceNumber
                    where Status='Y' order by Par_Acc_Name";

    }
    public static string GetPOTypeByReference(string pono)
    {
        string sql = "";
        return sql = @"select distinct In_Det_Pur_Type as POType from PuTr_PO_Det a inner join PuTr_IN_Det b on a.PO_Det_Pr_Ref=b.IN_Det_Ref
                       where PO_Det_Ref='" + pono + "'";

    }
    public static string GetLpobySupplier(string SupplierCode)
    {
        string sql = "";
        return sql = @"select distinct Trn_Det_Ord_Ref as POREF,Par_Acc_Name as Supplier,Trn_Hdr_Ref as MRR,sum(Trn_Det_Lin_Qty*Trn_Det_Lin_Amt) as MRRValue,Trn_Hdr_DATE as MRRDate,
                        InvoiceNumber as [Invoice Number],InvReceivedDate as [Invoice Date],InvReceivedBy as [Received By],'Pending' as [Audit Status]
                        from intr_trn_hdr a 
                        inner join AccPaybleProgress b on a.Trn_Hdr_Ref=b.ReferenceNumber
                        inner join PuMa_Par_Acc d on d.Par_Acc_Code=a.Trn_Hdr_Pcode
                        inner join intr_trn_det e on e.Trn_Det_Ref=a.Trn_Hdr_Ref
                        where Status='Y'  and Trn_Hdr_Pcode='" + SupplierCode + "' group by Trn_Det_Ord_Ref,Trn_Hdr_Ref,Trn_Hdr_DATE,Par_Acc_Name,InvoiceNumber,InvReceivedDate,InvReceivedBy order by Trn_Det_Ord_Ref";

    }
    public static string GetMrrDataByMrrNumber(string Mrrnumber)
    {
        string sql = "";
        return sql = @"select Trn_Hdr_Ref as [MRR Number],Trn_Hdr_DATE as [MRR Date],e.Trn_Det_Icode as [Item Code],f.Itm_Det_desc  as [Item Name],f.Itm_Det_stk_unit as UOM,
                        (select sum(po_det_lin_qty) from PuTr_PO_Det where PO_Det_Ref=e.Trn_Det_Ord_Ref and PO_Det_Icode=e.Trn_Det_Icode) as [PO Qty],
                        (select sum(Trn_Det_Lin_Qty) from InTr_Trn_Det where Trn_Det_Ord_Ref=e.Trn_Det_Ord_Ref and Trn_Det_Icode=e.Trn_Det_Icode and Trn_Det_Type='RC')as Received,
                        Trn_Det_Lin_Qty as [MRR Qty],Trn_Det_Lin_Rat as Rate,sum(Trn_Det_Lin_Qty*Trn_Det_Lin_Rat) as Amount,InvoiceReceived as Invoice,Jrnupdpermission,isnull(LCNumber,'') as LCNumber                        
                        from intr_trn_hdr a 
                        inner join AccPaybleProgress b on a.Trn_Hdr_Ref=b.ReferenceNumber
                        inner join PuMa_Par_Acc d on d.Par_Acc_Code=a.Trn_Hdr_Pcode
                        inner join intr_trn_det e on e.Trn_Det_Ref=a.Trn_Hdr_Ref
                        inner join InMa_Itm_Det f on f.Itm_Det_Icode=e.Trn_Det_Icode
                        where Status='Y'  and Trn_Hdr_Ref='" + Mrrnumber + "' group by Trn_Hdr_Ref,Trn_Hdr_DATE,Trn_Det_Icode,Itm_Det_desc,Itm_Det_stk_unit,Trn_Det_Lin_Qty,Trn_Det_Lin_Rat,InvoiceReceived,Jrnupdpermission,Trn_Det_Ord_Ref,LCNumber order by e.Trn_Det_Icode";

    }

    public static string GetMrrDataByMrrNumberForFinalJournal(string Mrrnumber)
    {
        string sql = "";
        return sql = @"select Trn_Det_Ord_Ref as PORef,Trn_Hdr_Ref as [MRR Number],Trn_Hdr_DATE as [MRR Date],e.Trn_Det_Icode as [Item Code],f.Itm_Det_desc  as [Item Name],f.Itm_Det_stk_unit as UOM,
                        (select sum(po_det_lin_qty) from PuTr_PO_Det where PO_Det_Ref=e.Trn_Det_Ord_Ref and PO_Det_Icode=e.Trn_Det_Icode) as [PO Qty],
                        (select sum(Trn_Det_Lin_Qty) from InTr_Trn_Det where Trn_Det_Ord_Ref=e.Trn_Det_Ord_Ref and Trn_Det_Icode=e.Trn_Det_Icode and Trn_Det_Type='RC')as Received,
                        Trn_Det_Lin_Qty as [MRR Qty],Trn_Det_Lin_Rat as Rate,sum(Trn_Det_Lin_Qty*Trn_Det_Lin_Rat) as Amount,InvoiceReceived as Invoice,Jrnupdpermission,ItemAcc,Trn_Hdr_Pcode as APCode                        
                        from intr_trn_hdr a 
                        inner join AccPaybleProgress b on a.Trn_Hdr_Ref=b.ReferenceNumber
                        inner join PuMa_Par_Acc d on d.Par_Acc_Code=a.Trn_Hdr_Pcode
                        inner join intr_trn_det e on e.Trn_Det_Ref=a.Trn_Hdr_Ref
                        inner join InMa_Itm_Det f on f.Itm_Det_Icode=e.Trn_Det_Icode
                        inner join Inma_Itm_AccMapping g on g.ItemCode=e.Trn_Det_Icode 
                        where Status='Y'  and Trn_Hdr_Ref='" + Mrrnumber + "' group by Trn_Hdr_Ref,Trn_Hdr_DATE,Trn_Det_Icode,Itm_Det_desc,Itm_Det_stk_unit,Trn_Det_Lin_Qty,Trn_Det_Lin_Rat,InvoiceReceived,Jrnupdpermission,Trn_Det_Ord_Ref,ItemAcc,Trn_Hdr_Pcode order by e.Trn_Det_Icode";

    }



    public static string GetJournalByMrrNumber(string Mrrnumber)
    {
        string sql = "";
        return sql = @"select Trn_Hdr_Ref as [MRR Number],Trn_Hdr_DATE as [MRR Date],e.Trn_Det_Icode as [Item Code],f.Itm_Det_desc  as [Item Name],f.Itm_Det_stk_unit as UOM,
                        Trn_Det_Lin_Qty as Qty,Trn_Det_Lin_Rat as Rate,sum(Trn_Det_Lin_Qty*Trn_Det_Lin_Rat) as Amount
                        from intr_trn_hdr a 
                        inner join AccPaybleProgress b on a.Trn_Hdr_Ref=b.ReferenceNumber
                        inner join PuMa_Par_Acc d on d.Par_Acc_Code=a.Trn_Hdr_Pcode
                        inner join intr_trn_det e on e.Trn_Det_Ref=a.Trn_Hdr_Ref
                        inner join InMa_Itm_Det f on f.Itm_Det_Icode=e.Trn_Det_Icode
                        where InvoiceReceived='N' and Status='Y'  and Trn_Hdr_Ref='" + Mrrnumber + "' group by Trn_Hdr_Ref,Trn_Hdr_DATE,Trn_Det_Icode,Itm_Det_desc,Itm_Det_stk_unit,Trn_Det_Lin_Qty,Trn_Det_Lin_Rat order by e.Trn_Det_Icode";

    }
    public static string GetJournaldetails(string Mrrnumber)
    {
        string sql = "";
        return sql = @"select * from AccTransactionDetailsHold where Trn_GRN_No='" + Mrrnumber + "'";
    }
    public static string GetJournaldetailsBySupplier(string Mrrnumber,string type)
    {
        string sql = "";
        return sql = @"select * from AccTransactionDetailsHold where Trn_GRN_No='" + Mrrnumber + "' and trn_ac_type='" + type + "'";
    }

    public static string GetVATandTaxCode()
    {
        string sql = "";
        return sql = @"select Led_Int_Det_Acc_Code+':'+Led_Int_Det_Acc_Name as AccName,Led_Int_Det_Code as Code from InSu_Led_Int_Det where Led_Int_Det_Code in('VAT','TAX')";
    }
    public static string GetVATandTaxCodeAndAmount(string mrrref)
    {
        string sql = "";
        return sql = @"select vatacc,(select sum(Trn_Amount) from AccTransactionDetailsHold where Trn_Ref_No=a.Trn_Ref_No and Trn_Ac_Code=b.vatacc)  as vatamt,
                        taxacc,(select sum(Trn_Amount) from AccTransactionDetailsHold where Trn_Ref_No=a.Trn_Ref_No and Trn_Ac_Code=b.taxacc) as taxamt,
                        InvoiceNumber,InvReceivedDate
                        from AccTransactionDetailsHold a 
                        inner join AccPaybleProgress b on a.Trn_GRN_No=b.ReferenceNumber
                        where Trn_GRN_No='" + mrrref + "' group by vatacc,Trn_Ref_No,taxacc,InvoiceNumber,InvReceivedDate";
    }
    public static string GetVATandTaxAmount(string mrrref)
    {
        string sql = "";
        return sql = @"select isnull(VATAcc,'') as VATAcc,isnull(TaxAcc,'') as TaxAcc,isnull(VATamt,0) as vatamt,isnull(TAXamt,0) as taxamt,
                        isnull(InvoiceNumber,'') as InvoiceNumber,isnull(InvReceivedDate,getdate()) as InvReceivedDate from AccPaybleProgress where ReferenceNumber='" + mrrref + "'";
    }

    public static string GetDataIssueType()
    {
        return "SELECT Iss_Type_Id, Iss_Type_Name, Iss_Type_Acc_Code, Iss_Type_IsActive, Iss_Type_Flag FROM InTr_Sr_Issue_Type";
    }
    public static string GetDataPriority()
    {
        return "SELECT Priority_Id, Priority_Name, Priority_IsActive, Priority_Flag FROM InTr_Sr_Priority";
    }

    public static string GetMaxSrRefNo(DateTime datePeriod)
    {
        return "SELECT MAX(RIGHT(Sr_Hdr_Ref, 6)) AS MaxMrrRefNo FROM InTr_Sr_Hdr WHERE (Sr_Hdr_Type = 'IS')  AND (CONVERT(DATETIME, Sr_Hdr_St_DATE, 103) >= CONVERT(datetime, '" + datePeriod + "', 103))";
    }

    public static string InsertSrHdr(string srHdrType, string srHdrCode, string srHdrRef, string srHdrPcode, string srHdrDcode, string srHdrAcode, string srHdrReqBy,
        DateTime srHdrStDate, DateTime srHdrEndDate, string srHdrCom1, string srHdrCom2, string srHdrCom3, string srHdrCom4, string srHdrCom5, string srHdrCom6,
        string srHdrCom7, string srHdrCom8, string srHdrCom9, string srHdrCom10, int srHdrValue, string srHdrPriority, string srHdrAppFlag, string srHdrAppUser,
        string srHdrHPCFlag, string srHdrEntPrd, string srHdrOprCode, string srHdrPrdCld, string srHdrExpTyp, string tC1, string tC2, string tFl, int tIn)
    {
        return "INSERT INTO [InTr_Sr_Hdr] ([Sr_Hdr_Type], [Sr_Hdr_Code], [Sr_Hdr_Ref], [Sr_Hdr_Pcode], [Sr_Hdr_Dcode], [Sr_Hdr_Acode], [Sr_Hdr_Req_By], [Sr_Hdr_St_DATE], "
            +" [Sr_Hdr_End_DATE], [Sr_Hdr_Com1], [Sr_Hdr_Com2], [Sr_Hdr_Com3], [Sr_Hdr_Com4], [Sr_Hdr_Com5], [Sr_Hdr_Com6], [Sr_Hdr_Com7], [Sr_Hdr_Com8], [Sr_Hdr_Com9], "
            +" [Sr_Hdr_Com10], [Sr_Hdr_Value], [Sr_Hdr_Priority], [Sr_Hdr_App_Flag], [Sr_Hdr_App_User], [Sr_Hdr_HPC_Flag], [Sr_Hdr_Ent_Prd], [Sr_Hdr_Opr_Code], [Sr_Hdr_Prd_Cld], "
            + " [Sr_Hdr_Exp_Typ], [T_C1], [T_C2], [T_Fl], [T_In]) VALUES ( '" + srHdrType + "', '" + srHdrCode + "', '" + srHdrRef + "', '" + srHdrPcode + "', '" + srHdrDcode + "', "
            + " '" + srHdrAcode + "', '" + srHdrReqBy + "',CONVERT(datetime,'" + srHdrStDate + "',103),CONVERT(datetime,'" + srHdrEndDate + "',103), '" + srHdrCom1 + "', '" + srHdrCom2 + "', '" + srHdrCom3 + "', "
            + " '" + srHdrCom4 + "', '" + srHdrCom5 + "', '" + srHdrCom6 + "', '" + srHdrCom7 + "', '" + srHdrCom8 + "', '" + srHdrCom9 + "', '" + srHdrCom10 + "', "
            + " " + srHdrValue + ", '" + srHdrPriority + "', '" + srHdrAppFlag + "', '" + srHdrAppUser + "', '" + srHdrHPCFlag + "', '" + srHdrEntPrd + "', "
            + " '" + srHdrOprCode + "', '" + srHdrPrdCld + "', '" + srHdrExpTyp + "', '" + tC1 + "', '" + tC2 + "', '" + tFl + "', " + tIn + ")";
    }

    public static string EditSrHdr(string srHdrPcode, string srHdrDcode, string srHdrAcode, string srHdrReqBy, DateTime srHdrStDate, DateTime srHdrEndDate,
        string srHdrCom1, string srHdrCom2, string srHdrCom3, string srHdrCom4, string srHdrCom5, string srHdrCom6, string srHdrCom7, string srHdrCom8,
        string srHdrCom9, string srHdrCom10, int srHdrValue, string srHdrPriority, string srHdrAppFlag, string srHdrAppUser, string srHdrHPCFlag, string srHdrEntPrd,
        string srHdrOprCode, string srHdrPrdCld, string srHdrExpTyp, string tC1, string tC2, string tFl, int tIn, string srHdrRef)
    {
        return "UPDATE [InTr_Sr_Hdr] SET [Sr_Hdr_Pcode]= '" + srHdrPcode + "', [Sr_Hdr_Dcode]= '" + srHdrDcode + "', [Sr_Hdr_Acode]= '" + srHdrAcode + "', "
            + " [Sr_Hdr_Req_By]= '" + srHdrReqBy + "', [Sr_Hdr_St_DATE]= '" + srHdrStDate + "', [Sr_Hdr_End_DATE]= '" + srHdrEndDate + "', [Sr_Hdr_Com1]= '" + srHdrCom1 + "', "
            + " [Sr_Hdr_Com2]= '" + srHdrCom2 + "', [Sr_Hdr_Com3]= '" + srHdrCom3 + "', [Sr_Hdr_Com4]= '" + srHdrCom4 + "', [Sr_Hdr_Com5]= '" + srHdrCom5 + "', "
            + " [Sr_Hdr_Com6]= '" + srHdrCom6 + "', [Sr_Hdr_Com7]= '" + srHdrCom7 + "', [Sr_Hdr_Com8]= '" + srHdrCom8 + "', [Sr_Hdr_Com9]= '" + srHdrCom9 + "', "
            + " [Sr_Hdr_Com10]= '" + srHdrCom10 + "', [Sr_Hdr_Value]= " + srHdrValue + ", [Sr_Hdr_Priority]= '" + srHdrPriority + "', [Sr_Hdr_App_Flag]= '" + srHdrAppFlag + "', "
            + " [Sr_Hdr_App_User]= '" + srHdrAppUser + "', [Sr_Hdr_HPC_Flag]= '" + srHdrHPCFlag + "', [Sr_Hdr_Ent_Prd]= '" + srHdrEntPrd + "', [Sr_Hdr_Opr_Code]= '" + srHdrOprCode + "', "
            + " [Sr_Hdr_Prd_Cld]= '" + srHdrPrdCld + "', [Sr_Hdr_Exp_Typ]= '" + srHdrExpTyp + "', [T_C1]= '" + tC1 + "', [T_C2]= '" + tC2 + "', [T_Fl]= '" + tFl + "', "
            + " [T_In]= " + tIn + " WHERE [Sr_Hdr_Ref]= '" + srHdrRef + "' ";
    }

    public static string DeleteSrDetByRefNo(string srDetRef)
    {
        return "DELETE InTr_Sr_Det where Sr_Det_Ref= '" + srDetRef + "'";
    }

    public static string InsertSrDet(string srDetType, string srDetCode, string srDetRef, short srDetLno, string srDetSfx, int srDetExpLno, string srDetIcode,
        string srDetItmDesc, string srDetItmUom, string srDetStrCode, string srDetBinCode, string srDetBatNo, DateTime srDetExpDat, double srDetLinQty,
        int srDetOrgQTY, double srDetBalQty, int srDetUntWgt, string srDetOCFlag, string srDetBalUpd, decimal srDetLinRat, decimal srDetLinAmt,
        decimal srDetLinNet, string srDetSupName, string srDetAppFlag, string srDetAppUser, string srDetPriority, string srDetRemarks, string srDetRfqFlag,
        string srDetPrePORef, double srDetCurStk, string tC1, string tC2, string tFl, int tIn)
    {
        return "INSERT INTO [InTr_Sr_Det] ([Sr_Det_Type], [Sr_Det_Code], [Sr_Det_Ref], [Sr_Det_Lno], [Sr_Det_Sfx], [Sr_Det_Exp_Lno], [Sr_Det_Icode], [Sr_Det_Itm_Desc], "
            +" [Sr_Det_Itm_Uom], [Sr_Det_Str_Code], [Sr_Det_Bin_Code], [Sr_Det_Bat_No], [Sr_Det_Exp_Dat], [Sr_Det_Lin_Qty], [Sr_Det_Org_QTY], [Sr_Det_Bal_Qty], [Sr_Det_Unt_Wgt], "
            +" [Sr_Det_OC_Flag], [Sr_Det_Bal_Upd], [Sr_Det_Lin_Rat], [Sr_Det_Lin_Amt], [Sr_Det_Lin_Net], [Sr_Det_Sup_Name], [Sr_Det_App_Flag], [Sr_Det_App_User], [Sr_Det_Priority], "
            + " [Sr_Det_Remarks], [Sr_Det_Rfq_Flag], [Sr_Det_PrePO_Ref], [Sr_Det_Cur_Stk], [T_C1], [T_C2], [T_Fl], [T_In]) VALUES ( '" + srDetType + "', '" + srDetCode + "', "
            + " '" + srDetRef + "', " + srDetLno + ", '" + srDetSfx + "', " + srDetExpLno + ", '" + srDetIcode + "', '" + srDetItmDesc + "', '" + srDetItmUom + "', "
            + " '" + srDetStrCode + "', '" + srDetBinCode + "', '" + srDetBatNo + "',CONVERT(datetime,'" + srDetExpDat + "',103), " + srDetLinQty + ", " + srDetOrgQTY + ", "
            + " " + srDetBalQty + ", " + srDetUntWgt + ", '" + srDetOCFlag + "', '" + srDetBalUpd + "', " + srDetLinRat + ", " + srDetLinAmt + ", " + srDetLinNet + ", "
            + " '" + srDetSupName + "', '" + srDetAppFlag + "', '" + srDetAppUser + "', '" + srDetPriority + "', '" + srDetRemarks + "', '" + srDetRfqFlag + "', "
            + " '" + srDetPrePORef + "', " + srDetCurStk + ", '" + tC1 + "', '" + tC2 + "', '" + tFl + "', " + tIn + ")";
    }

    public static string GetSrHdrByRefNo(string srHdrRef)
    {
        return "SELECT Sr_Hdr_Acode, Sr_Hdr_App_Flag, Sr_Hdr_App_User, Sr_Hdr_Code, Sr_Hdr_Com1, Sr_Hdr_Com10, Sr_Hdr_Com2, Sr_Hdr_Com3, Sr_Hdr_Com4, Sr_Hdr_Com5, Sr_Hdr_Com6, "
            +" Sr_Hdr_Com7, Sr_Hdr_Com8, Sr_Hdr_Com9, Sr_Hdr_Dcode, Sr_Hdr_End_DATE, Sr_Hdr_Ent_Prd, Sr_Hdr_Exp_Typ, Sr_Hdr_HPC_Flag, Sr_Hdr_Opr_Code, Sr_Hdr_Pcode,  "
            +" Sr_Hdr_Prd_Cld, Sr_Hdr_Priority, Sr_Hdr_Ref, Sr_Hdr_Req_By, Sr_Hdr_St_DATE, Sr_Hdr_Type, Sr_Hdr_Value, T_C1, T_C2, T_Fl, T_In FROM InTr_Sr_Hdr WHERE "
            + " (Sr_Hdr_Ref = '" + srHdrRef + "')";
    }

    public static string GetSrDetByRefNo(string srDetRef)
    {
        return "SELECT Sr_Det_App_Flag, Sr_Det_App_User, Sr_Det_Bal_Qty, Sr_Det_Bal_Upd, Sr_Det_Bat_No, Sr_Det_Bin_Code, Sr_Det_Code, Sr_Det_Cur_Stk, Sr_Det_Exp_Dat, "
            +" Sr_Det_Exp_Lno, Sr_Det_Icode, Sr_Det_Itm_Desc, Sr_Det_Itm_Uom, Sr_Det_Lin_Amt, Sr_Det_Lin_Net, Sr_Det_Lin_Qty, Sr_Det_Lin_Rat, Sr_Det_Lno, Sr_Det_OC_Flag, "
            +" Sr_Det_Org_QTY, Sr_Det_PrePO_Ref, Sr_Det_Priority, Sr_Det_Ref, Sr_Det_Remarks, Sr_Det_Rfq_Flag, Sr_Det_Sfx, Sr_Det_Str_Code, Sr_Det_Sup_Name, Sr_Det_Type, "
            + " Sr_Det_Unt_Wgt, T_C1, T_C2, T_Fl, T_In FROM InTr_Sr_Det WHERE (Sr_Det_Ref = '" + srDetRef + "')";
    }

    public static string GetClientByCode(string parAccCode)
    {
        return "SELECT Par_Acc_Code, Par_Acc_Name, Par_Acc_Sec_Code, Par_Acc_Type, Par_Acc_Bo, Par_Acc_Sta, Par_Acc_Trn_DATE, Par_Acc_Trn_Flag, Par_Acc_Upd_DATE, "
            +" Par_Acc_Comm, Par_Acc_Perm, Par_Acc_Tot_Cr, Par_Acc_Tot_Db, Par_Acc_Unpost_Cr, Par_Acc_Unpost_Db, Par_Acc_Bal_Amt, Par_Acc_Bal_Flag,Par_Acc_Cur_Code, "
            + " Par_Acc_Ana_Req, Par_Acc_Bal_Te_Req, Par_Acc_Nar_Amt_Req, Par_Acc_Nar_Amt_Type, T_C1, T_C2, T_Fl, T_In FROM SaMa_Par_Acc WHERE Par_Acc_Code= '" + parAccCode + "'";
    }

    public static string GetClientAddrByCode(string parAdrCode)
    {
        return "SELECT Par_Adr_Code, par_adr_name, Par_Adr_Sec_Code, Par_Adr_Line1, Par_Adr_Line2, Par_Adr_Line3, Par_Adr_Line4, Par_Adr_Line5, Par_Adr_Cst_No, "
            +" Par_Adr_Lst_No, Par_Adr_Cnt_No, Par_Adr_Tel_No, Par_Adr_Fax_No, Par_Adr_Email_Id, Par_Adr_Acc_Code, Par_Adr_Cmt, Par_Adr_Upd_DATE, Par_Adr_Trn_Flag, "
            + " Par_Adr_Lst_Trn_DATE, Par_Adr_Ord_Bal, Par_Adr_Inv_Val,T_C1, T_C2, T_Fl, T_In FROM SaMa_Par_Adr WHERE Par_Adr_Code= '" + parAdrCode + "'";
    }

    public static string GetDeptByCode(string ccgCode)
    {
        return "SELECT Ccg_Cost_Id, Ccg_Code, Ccg_Name, Ccg_Short_Name, Ccg_Last_Upd_DATE, T_C1, T_C2, T_Fl, T_In FROM FA_COM_CCG WHERE [Ccg_Cost_Id]='T01' AND Ccg_Code= '" + ccgCode + "'";
    }

    public static string GetDataById(string userID)
    {
        return "SELECT department, last_update, resign_date, status, user_designation, user_email, user_name, user_password, userid FROM tbl_user_info WHERE (userid = '" + userID + "')";
    }

    public static string GetClientAdrByAccCode(string parAdrAccCode)
    {
        return "SELECT Par_Adr_Code, par_adr_name, Par_Adr_Sec_Code, Par_Adr_Line1, Par_Adr_Line2, Par_Adr_Line3, Par_Adr_Line4, Par_Adr_Line5, Par_Adr_Cst_No, "
            +" Par_Adr_Lst_No, Par_Adr_Cnt_No, Par_Adr_Tel_No, Par_Adr_Fax_No, Par_Adr_Email_Id, Par_Adr_Acc_Code, Par_Adr_Cmt, Par_Adr_Upd_DATE, Par_Adr_Trn_Flag, "
            + " Par_Adr_Lst_Trn_DATE, Par_Adr_Ord_Bal, Par_Adr_Inv_Val, Par_Adr_App_Cr_Lmt, Par_Adr_App_Cr_Prd, T_C1, T_C2, T_Fl, T_In FROM SaMa_Par_Adr Where Par_Adr_Acc_Code= '" + parAdrAccCode + "'";
    }
    #endregion Item Requisition

    #region Item Issue

    public static string GetIssHdrByRefNo(string trnHdrRef)
    {
        return "SELECT T_C1, T_C2, T_Fl, T_In, Trn_Hdr_Acode, Trn_Hdr_CI_Date, Trn_Hdr_Cno, Trn_Hdr_Code, Trn_Hdr_Com1, Trn_Hdr_Com10, Trn_Hdr_Com2, Trn_Hdr_Com3, Trn_Hdr_Com4, "
            +" Trn_Hdr_Com5, Trn_Hdr_Com6, Trn_Hdr_Com7, Trn_Hdr_Com8, Trn_Hdr_Com9, Trn_Hdr_DATE, Trn_Hdr_DC_No, Trn_Hdr_Dc_Date, Trn_Hdr_Dcode, Trn_Hdr_EI_Flg, Trn_Hdr_Ent_Prd, "
            +" Trn_Hdr_Exp_Typ, Trn_Hdr_HRPB_Flag, Trn_Hdr_Led_Int, Trn_Hdr_Opr_Code, Trn_Hdr_Pass_No, Trn_Hdr_Pcode, Trn_Hdr_Prd_Cld, Trn_Hdr_Ref, Trn_Hdr_Type, Trn_Hdr_Value, "
            + " Trn_Hdr_exc_duty FROM InTr_Trn_Hdr WHERE (Trn_Hdr_Type = 'IS') AND (Trn_Hdr_Ref = '" + trnHdrRef + "')";
    }

    public static string GetIssDetByRefNo(string trnDetRef)
    {
        return "SELECT T_C1, T_C2, T_Fl, T_In, Trn_Det_Bal_Qty, Trn_Det_Bat_No, Trn_Det_Bin_Code, Trn_Det_Book_Dat, Trn_Det_Code, Trn_Det_Exp_Dat, Trn_Det_Exp_Lno, Trn_Det_Icode, "
            +" Trn_Det_Itm_Desc, Trn_Det_Itm_Uom, Trn_Det_Lin_Amt, Trn_Det_Lin_Net, Trn_Det_Lin_Qty, Trn_Det_Lin_Rat, Trn_Det_Lno, Trn_Det_Ord_Lno, Trn_Det_Ord_Ref, Trn_Det_Ref, "
            + " Trn_Det_Sfx, Trn_Det_Str_Code, Trn_Det_Type, Trn_Det_Unt_Wgt FROM InTr_Trn_Det WHERE (Trn_Det_Type = 'IS') AND (Trn_Det_Ref = '" + trnDetRef + "') ORDER BY Trn_Det_Lno";
    }

    public static string GetIssueTypeById(string issTypeId)
    {
        return "SELECT Iss_Type_Acc_Code, Iss_Type_Flag, Iss_Type_Id, Iss_Type_IsActive, Iss_Type_Name FROM InTr_Sr_Issue_Type WHERE (Iss_Type_Id = '" + issTypeId + "')";
    }

    public static string GetSrDetByIcode(string srDetRef, string srDetIcode)
    {
        return "SELECT Sr_Det_App_Flag, Sr_Det_App_User, Sr_Det_Bal_Qty, Sr_Det_Bal_Upd, Sr_Det_Bat_No, Sr_Det_Bin_Code, Sr_Det_Code, Sr_Det_Cur_Stk, Sr_Det_Exp_Dat, "
            +" Sr_Det_Exp_Lno, Sr_Det_Icode, Sr_Det_Itm_Desc, Sr_Det_Itm_Uom, Sr_Det_Lin_Amt, Sr_Det_Lin_Net, Sr_Det_Lin_Qty, Sr_Det_Lin_Rat, Sr_Det_Lno, Sr_Det_OC_Flag, "
            +" Sr_Det_Org_QTY, Sr_Det_PrePO_Ref, Sr_Det_Priority, Sr_Det_Ref, Sr_Det_Remarks, Sr_Det_Rfq_Flag, Sr_Det_Sfx, Sr_Det_Str_Code, Sr_Det_Sup_Name, Sr_Det_Type, "
            + " Sr_Det_Unt_Wgt, T_C1, T_C2, T_Fl, T_In FROM InTr_Sr_Det WHERE (Sr_Det_Ref = '" + srDetRef + "') AND (Sr_Det_Icode = '" + srDetIcode + "')";
    }

    public static string GetFIFOItem(string itmRateIcode, string itmRateScode)
    {
        return "SELECT itm_rate_icode, itm_rate_id, itm_rate_id_grp, itm_rate_lineno, itm_rate_qty, itm_rate_rate, itm_rate_scode, itm_rate_trn_ref, itm_rate_trndate "
            + " FROM InMa_Itm_Rate WHERE (itm_rate_icode = '" + itmRateIcode + "') AND (itm_rate_scode = '" + itmRateScode + "') AND (itm_rate_qty > 0) ORDER BY itm_rate_trndate, itm_rate_lineno";
    }

    public static string GetMaxIssRefNo(DateTime datePeriod)
    {
        return "SELECT isnull(MAX(RIGHT(Trn_Hdr_Ref, 5)),0) AS MaxMrrRefNo FROM InTr_Trn_Hdr WHERE (Trn_Hdr_Type = 'IS')  AND (CONVERT(DATETIME, Trn_Hdr_DATE, 103) >= CONVERT(datetime, '" + datePeriod + "', 103))";
    }

    public static string GetItmRateQtyByRateMrr(string itmRateScode, string itmRateIcode, string itmRateId, int itmRateLineno, string itmRateTrnRef)
    {
        return "SELECT itm_rate_icode, itm_rate_id, itm_rate_id_grp, itm_rate_lineno, itm_rate_qty, itm_rate_rate, itm_rate_scode, itm_rate_trn_ref, itm_rate_trndate "
        +" FROM InMa_Itm_Rate WHERE (itm_rate_scode = '"+itmRateScode+"') AND (itm_rate_icode = '"+itmRateIcode+"') AND (itm_rate_id = '"+itmRateId+"') AND "
        + " (itm_rate_lineno = " + itmRateLineno + ") AND (itm_rate_trn_ref= '" + itmRateTrnRef + "')";
    }

    public static string UpdateRateQty(decimal itmRateQty, string itmRateScode, string itmRateIcode, string itmRateId, int itmRateLineno, string itmRateTrnRef)
    {
        return "UPDATE [InMa_Itm_Rate]  SET [itm_rate_qty]= "+itmRateQty+" WHERE [itm_rate_scode]= '"+itmRateScode+"' AND [itm_rate_icode]= '"+itmRateIcode+"' AND "
            + " [itm_rate_id]= '" + itmRateId + "' AND [itm_rate_lineno]= " + itmRateLineno + " AND (itm_rate_trn_ref= '" + itmRateTrnRef + "')";
    }

    public static string UpdateSrQty(double srDetOrgQTY, double srDetBalQty, string srDetBalUpd, string srDetRef, string srDetIcode)
    {
        return "UPDATE [InTr_Sr_Det]  SET  [Sr_Det_Org_QTY]= " + srDetOrgQTY + ", [Sr_Det_Bal_Qty]= " + srDetBalQty + ", [Sr_Det_Bal_Upd]= '" + srDetBalUpd + "' WHERE "
            + " [Sr_Det_Ref]= '" + srDetRef + "' AND [Sr_Det_Icode]= '" + srDetIcode + "'";
    }

    public static string GetJvRefNo(string jrnType, string jrnRefPrefix, string tFl)
    {
        string sql = "";
        return sql = "select JrnNextRefNo from AccJournalSetup WHERE (JrnType = '" + jrnType + "') AND "
            + " (JrnPrefix = '" + jrnRefPrefix + "')";
    }

    public static string GetFaAccHead()
    {
        return "SELECT Iss_Type_Acc_Code, Iss_Type_Flag, Iss_Type_Id, Iss_Type_IsActive, Iss_Type_Name FROM InTr_Sr_Issue_Type WHERE (Iss_Type_Flag = 'F')";
    }

    public static string InsertFixedAsset(string refNo, int isFAUpdate, string updateDate, string updateUser, string itemCode, int itemQty, decimal trnAmount)
    {
        string sql = "";
        return sql="INSERT INTO [fxdFAReferenceNumbers] ([RefNo], [isFAUpdated], [UpdateDate], [UpdateUser], [ItemCode], [ItemQty],[TrnAmount]) VALUES ( '" + refNo + "', "
            + " " + isFAUpdate + ", '" + updateDate + "', '" + updateUser + "', '" + itemCode + "', " + itemQty + "," + trnAmount + ")";
    }

    public static string EditMRRHdr(string trnHdrPcode, string trnHdrDcode, string trnHdrAcode, DateTime trnHdrDate, string trnHdrCom1, string trnHdrCom2,
        string trnHdrCom3, string trnHdrCom4, string trnHdrCom5, string trnHdrCom6, string trnHdrCom7, string trnHdrCom8, string trnHdrCom9, string trnHdrCom10,
        decimal trnHdrValue, string trnHdrHRPBFlag, string trnHdrEntPrd, string trnHdrOprCode, string trnHdrPrdCld, string trnHdrExpTyp, string trnHdrLedInt,
        string trnHdrDCNo, string trnHdrEIFlg, string trnHdrCno, string tC1, string tC2, string tFl, int tIn, int trnHdrExcDuty, string trnHdrDcDate,
        string trnHdrCIDate, string trnHdrPassNo, string trnHdrType, string trnHdrCode, string trnHdrRef)
    {
        return "UPDATE [InTr_Trn_Hdr]  SET [Trn_Hdr_Pcode]= '" + trnHdrPcode + "', [Trn_Hdr_Dcode]= '" + trnHdrDcode + "', [Trn_Hdr_Acode]= '" + trnHdrAcode + "', "
            + " [Trn_Hdr_DATE]= '" + trnHdrDate + "', [Trn_Hdr_Com1]= '" + trnHdrCom1 + "', [Trn_Hdr_Com2]= '" + trnHdrCom2 + "', [Trn_Hdr_Com3]= '" + trnHdrCom3 + "', "
            + " [Trn_Hdr_Com4]= '" + trnHdrCom4 + "', [Trn_Hdr_Com5]= '" + trnHdrCom5 + "', [Trn_Hdr_Com6]= '" + trnHdrCom6 + "', [Trn_Hdr_Com7]= '" + trnHdrCom7 + "', "
            + " [Trn_Hdr_Com8]= '" + trnHdrCom8 + "', [Trn_Hdr_Com9]= '" + trnHdrCom9 + "', [Trn_Hdr_Com10]= '" + trnHdrCom10 + "', [Trn_Hdr_Value]= " + trnHdrValue + ", "
            + " [Trn_Hdr_HRPB_Flag]= '" + trnHdrHRPBFlag + "', [Trn_Hdr_Ent_Prd]= '" + trnHdrEntPrd + "', [Trn_Hdr_Opr_Code]= '" + trnHdrOprCode + "',  "
            + " [Trn_Hdr_Prd_Cld]= '" + trnHdrPrdCld + "', [Trn_Hdr_Exp_Typ]= '" + trnHdrExpTyp + "', [Trn_Hdr_Led_Int]= '" + trnHdrLedInt + "', [Trn_Hdr_DC_No]= '" + trnHdrDCNo + "', "
            + " [Trn_Hdr_EI_Flg]= '" + trnHdrEIFlg + "', [Trn_Hdr_Cno]= '" + trnHdrCno + "', [T_C1]= '" + tC1 + "', [T_C2]= '" + tC2 + "', [T_Fl]= '" + tFl + "', "
            + " [T_In]= " + tIn + ", [Trn_Hdr_exc_duty]= " + trnHdrExcDuty + ", [Trn_Hdr_Dc_Date]= '" + trnHdrDcDate + "', [Trn_Hdr_CI_Date]= '" + trnHdrCIDate + "', "
            + " [Trn_Hdr_Pass_No]= '" + trnHdrPassNo + "' WHERE [Trn_Hdr_Type]= '" + trnHdrType + "' AND [Trn_Hdr_Code]= '" + trnHdrCode + "' AND [Trn_Hdr_Ref]= '" + trnHdrRef + "'";
    }

    public static string InsertMRRHdr(string trnHdrType, string trnHdrCode, string trnHdrRef, string trnHdrPcode, string trnHdrDcode, string trnHdrAcode,
        DateTime trnHdrDate, string trnHdrCom1, string trnHdrCom2, string trnHdrCom3, string trnHdrCom4, string trnHdrCom5, string trnHdrCom6, string trnHdrCom7,
        string trnHdrCom8, string trnHdrCom9, string trnHdrCom10, decimal trnHdrValue, string trnHdrHRPBFlag, string trnHdrEntPrd, string trnHdrOprCode,
        string trnHdrPrdCld, string trnHdrExpTyp, string trnHdrLedInt, string trnHdrDCNo, string trnHdrEIFlg, string trnHdrCno, string tC1, string tC2,
        string tFl, int tIn, int trnHdrExcDuty, string trnHdrDcDate, string trnHdrCIDate, string trnHdrPassNo)
    {
        string sql = "";
        return sql="INSERT INTO [InTr_Trn_Hdr] ([Trn_Hdr_Type], [Trn_Hdr_Code], [Trn_Hdr_Ref], [Trn_Hdr_Pcode], [Trn_Hdr_Dcode], [Trn_Hdr_Acode], [Trn_Hdr_DATE], [Trn_Hdr_Com1], "
            +" [Trn_Hdr_Com2], [Trn_Hdr_Com3], [Trn_Hdr_Com4], [Trn_Hdr_Com5], [Trn_Hdr_Com6], [Trn_Hdr_Com7], [Trn_Hdr_Com8], [Trn_Hdr_Com9], [Trn_Hdr_Com10], [Trn_Hdr_Value], "
            +" [Trn_Hdr_HRPB_Flag], [Trn_Hdr_Ent_Prd], [Trn_Hdr_Opr_Code], [Trn_Hdr_Prd_Cld], [Trn_Hdr_Exp_Typ], [Trn_Hdr_Led_Int], [Trn_Hdr_DC_No], [Trn_Hdr_EI_Flg], "
            + " [Trn_Hdr_Cno], [T_C1], [T_C2], [T_Fl], [T_In], [Trn_Hdr_exc_duty], [Trn_Hdr_Dc_Date], [Trn_Hdr_CI_Date], [Trn_Hdr_Pass_No]) VALUES ( '" + trnHdrType + "', "
            + " '" + trnHdrCode + "', '" + trnHdrRef + "', '" + trnHdrPcode + "', '" + trnHdrDcode + "', '" + trnHdrAcode + "', Convert(Datetime,'" + trnHdrDate + "',103), '" + trnHdrCom1 + "', "
            + " '" + trnHdrCom2 + "', '" + trnHdrCom3 + "', '" + trnHdrCom4 + "', '" + trnHdrCom5 + "', '" + trnHdrCom6 + "', '" + trnHdrCom7 + "', '" + trnHdrCom8 + "', "
            + " '" + trnHdrCom9 + "', '" + trnHdrCom10 + "', " + trnHdrValue + ", '" + trnHdrHRPBFlag + "', '" + trnHdrEntPrd + "', '" + trnHdrOprCode + "', "
            + " '" + trnHdrPrdCld + "', '" + trnHdrExpTyp + "', '" + trnHdrLedInt + "', '" + trnHdrDCNo + "', '" + trnHdrEIFlg + "', '" + trnHdrCno + "', '" + tC1 + "', "
            + " '" + tC2 + "', '" + tFl + "', " + tIn + ", " + trnHdrExcDuty + ", '" + trnHdrDcDate + "', '" + trnHdrCIDate + "', '" + trnHdrPassNo + "')";
    }
    #endregion Item Issue    

    //#region Item Return
    //public static string GetDataByTrn_Hdr_Ref(string trnHdrRef)
    //{
    //    return "SELECT InTr_Trn_Det.Trn_Det_Icode, InTr_Trn_Det.Trn_Det_Itm_Desc, InTr_Trn_Hdr.Trn_Hdr_Ref, InTr_Trn_Hdr.Trn_Hdr_DATE, InTr_Trn_Det.Trn_Det_Lin_Qty, "
    //                     + " InTr_Trn_Det.Trn_Det_Itm_Uom, InTr_Trn_Det.Trn_Det_Bat_No, SaMa_Par_Adr.par_adr_name, SaMa_Par_Adr.Par_Adr_Code, SaMa_Par_Adr.Par_Adr_Line1, "
    //                     + " SaMa_Par_Adr.Par_Adr_Line2, SaMa_Par_Adr.Par_Adr_Line3, SaMa_Par_Adr.Par_Adr_Line4, SaMa_Par_Adr.Par_Adr_Line5, InTr_Trn_Hdr.Trn_Hdr_Com2 "
    //                     + " FROM InTr_Trn_Det INNER JOIN InTr_Trn_Hdr ON InTr_Trn_Det.Trn_Det_Ref = InTr_Trn_Hdr.Trn_Hdr_Ref INNER JOIN SaMa_Par_Adr ON "
    //                     + " InTr_Trn_Hdr.Trn_Hdr_Pcode = SaMa_Par_Adr.Par_Adr_Code where Trn_Hdr_Ref= '" + trnHdrRef + "'";
    //}

    //public static string GetMaxMovRefNo()
    //{
    //    return "select  stuff('0000000',8-len(ISNULL(max (right(Movement_RefNo,7)),0)+1),20,ISNULL(max (right(Movement_RefNo,7)),0)+1) as MaxID from Item_Movement_dtl ";
    //}

    //public static string GetMaxMovementNo()
    //{
    //    return "SELECT ISNULL(max (Movement_No),0)+1   as MaxID from Item_Movement_dtl";
    //}

    //public static string InsertQuery(int movementNo, string movementRefNo, string itmDetIcode, string itmDetSerialNo, string itmDetRef, string itmDetStrCode,
    //    string itmDetTrnType, string itmDetTrnCode, DateTime itmDetDate, string itmStatus, string trackingInfo, int itemQty, int itemRate, string moHdrPcode,
    //    DateTime moHdrDate, DateTime entryDate, string entryUserID)
    //{
    //    return "INSERT INTO [Item_Movement_dtl] ([Movement_No], [Movement_RefNo], [itm_det_icode], [itm_det_serial_no], [itm_det_ref], [itm_det_str_code], [itm_det_trn_type], "
    //        +" [itm_det_trn_code], [itm_det_date], [itm_status], [TrackingInfo], [ItemQty], [ItemRate], [Mo_Hdr_Pcode], [Mo_Hdr_Date], [EntryDate], [EntryUserID]) VALUES "
    //        + " (" + movementNo + ", '" + movementRefNo + "', '" + itmDetIcode + "', '" + itmDetSerialNo + "', '" + itmDetRef + "', '" + itmDetStrCode + "', "
    //        + " '" + itmDetTrnType + "', '" + itmDetTrnCode + "', '" + itmDetDate + "', '" + itmStatus + "', '" + trackingInfo + "', " + itemQty + ", " + itemRate + ", "
    //        + " '" + moHdrPcode + "', '" + moHdrDate + "', '" + entryDate + "', '" + entryUserID + "')";
    //}

    //public static string InsertIntoTrnHdr(string trnHdrType, string trnHdrCode, string trnHdrRef, string trnHdrPcode, string trnHdrDcode, string trnHdrAcode,
    //    DateTime trnHdrDate, string trnHdrCom1, string trnHdrCom2, string trnHdrCom3, string trnHdrCom4, string trnHdrCom5, string trnHdrCom6, string trnHdrCom7,
    //    string trnHdrCom8, string trnHdrCom9, string trnHdrCom10, int trnHdrValue, string trnHdrHRPBFlag, string trnHdrEntPrd, string trnHdrOprCode, string trnHdrPrdCld,
    //    string trnHdrExpTyp, string trnHdrLedInt, string trnHdrDCNo, string trnHdrEIFlg, string trnHdrCno, string tC1, string tC2, string tFl, int tIn,
    //    int trnHdrExcDuty, DateTime trnHdrDcDate, DateTime trnHdrCIDate, string trnHdrPassNo)
    //{
    //    return "INSERT INTO [InTr_Trn_Hdr] ([Trn_Hdr_Type], [Trn_Hdr_Code], [Trn_Hdr_Ref], [Trn_Hdr_Pcode], [Trn_Hdr_Dcode], [Trn_Hdr_Acode], [Trn_Hdr_DATE], [Trn_Hdr_Com1], "
    //        +" [Trn_Hdr_Com2], [Trn_Hdr_Com3], [Trn_Hdr_Com4], [Trn_Hdr_Com5], [Trn_Hdr_Com6], [Trn_Hdr_Com7], [Trn_Hdr_Com8], [Trn_Hdr_Com9], [Trn_Hdr_Com10], [Trn_Hdr_Value], "
    //        +" [Trn_Hdr_HRPB_Flag], [Trn_Hdr_Ent_Prd], [Trn_Hdr_Opr_Code], [Trn_Hdr_Prd_Cld], [Trn_Hdr_Exp_Typ], [Trn_Hdr_Led_Int], [Trn_Hdr_DC_No], [Trn_Hdr_EI_Flg], "
    //        + " [Trn_Hdr_Cno], [T_C1], [T_C2], [T_Fl], [T_In], [Trn_Hdr_exc_duty], [Trn_Hdr_Dc_Date], [Trn_Hdr_CI_Date], [Trn_Hdr_Pass_No]) VALUES ('" + trnHdrType + "', "
    //        + " '" + trnHdrCode + "', '" + trnHdrRef + "', '" + trnHdrPcode + "', '" + trnHdrDcode + "', '" + trnHdrAcode + "', '" + trnHdrDate + "', '" + trnHdrCom1 + "', "
    //        + " '" + trnHdrCom2 + "', '" + trnHdrCom3 + "', '" + trnHdrCom4 + "', '" + trnHdrCom5 + "', '" + trnHdrCom6 + "', '" + trnHdrCom7 + "', '" + trnHdrCom8 + "', "
    //        + " '" + trnHdrCom9 + "', '" + trnHdrCom10 + "', " + trnHdrValue + ", '" + trnHdrHRPBFlag + "', '" + trnHdrEntPrd + "', '" + trnHdrOprCode + "', "
    //        + " '" + trnHdrPrdCld + "', '" + trnHdrExpTyp + "', '" + trnHdrLedInt + "', '" + trnHdrDCNo + "', '" + trnHdrEIFlg + "', '" + trnHdrCno + "', '" + tC1 + "', "
    //        + " '" + tC2 + "', '" + tFl + "', " + tIn + ", " + trnHdrExcDuty + ", '" + trnHdrDcDate + "', '" + trnHdrCIDate + "', '" + trnHdrPassNo + "')";
    //}

    //public static string InsertIntoTrnDet(string trnDetType, string trnDetCode, string trnDetRef, int trnDetLno, string trnDetSfx, int trnDetExpLno, string trnDetIcode,
    //    string trnDetItmDesc, string trnDetItmUom, string trnDetStrCode, string trnDetBinCode, string trnDetOrdRef, int trnDetOrdLno, string trnDetBatNo,
    //    DateTime trnDetExpDat, DateTime trnDetBookDat, int trnDetLinQty, int trnDetUntWgt, int trnDetLinRat, int trnDetLinAmt, int trnDetLinNet, string tC1,
    //    string tC2, string tFl, int tIn, int trnDetBalQty)
    //{
    //    return "INSERT INTO [InTr_Trn_Det] ([Trn_Det_Type], [Trn_Det_Code], [Trn_Det_Ref], [Trn_Det_Lno], [Trn_Det_Sfx], [Trn_Det_Exp_Lno], [Trn_Det_Icode], [Trn_Det_Itm_Desc], "
    //        +" [Trn_Det_Itm_Uom], [Trn_Det_Str_Code], [Trn_Det_Bin_Code], [Trn_Det_Ord_Ref], [Trn_Det_Ord_Lno], [Trn_Det_Bat_No], [Trn_Det_Exp_Dat], [Trn_Det_Book_Dat], "
    //        +" [Trn_Det_Lin_Qty], [Trn_Det_Unt_Wgt], [Trn_Det_Lin_Rat], [Trn_Det_Lin_Amt], [Trn_Det_Lin_Net], [T_C1], [T_C2], [T_Fl], [T_In], [Trn_Det_Bal_Qty]) VALUES "
    //        + " ( '" + trnDetType + "', '" + trnDetCode + "', '" + trnDetRef + "', " + trnDetLno + ", '" + trnDetSfx + "', " + trnDetExpLno + ", '" + trnDetIcode + "', "
    //        + " '" + trnDetItmDesc + "', '" + trnDetItmUom + "', '" + trnDetStrCode + "', '" + trnDetBinCode + "', '" + trnDetOrdRef + "', " + trnDetOrdLno + ", "
    //        + " '" + trnDetBatNo + "', '" + trnDetExpDat + "', '" + trnDetBookDat + "', " + trnDetLinQty + ", " + trnDetUntWgt + ", " + trnDetLinRat + ", "
    //        + " " + trnDetLinAmt + ", " + trnDetLinNet + ", '" + tC1 + "', '" + tC2 + "', '" + tFl + "', " + tIn + ", " + trnDetBalQty + ")";
    //}

    //public static string InsertIntoInmaStkCtl(string Stk_Ctl_SCode, string Stk_Ctl_ICode, string Stk_Ctl_Str_Grp, double Stk_Ctl_Cur_Stk, double Stk_Ctl_Free_Stk, int Stk_Ctl_On_Ord_Stk,
    //    int Stk_Ctl_Ind_Stk, int Stk_Ctl_Quot_Stk, int Stk_Ctl_Min_Stk, int Stk_Ctl_Reord_Stk, int Stk_Ctl_Max_Stk, int Stk_Ctl_Std_Val, int Stk_Ctl_Ave_Val, int Stk_Ctl_Lat_Val,
    //    int Stk_Ctl_FIFO_Val, int Stk_Ctl_LIFO_Val, DateTime Stk_Ctl_Lst_Rec_Dat, DateTime Stk_Ctl_Lst_Iss_Dat, string p15, string p16, string p17, int p18)
    //{
    //    return "INSERT INTO [InMa_Stk_Ctl] ([Stk_Ctl_SCode], [Stk_Ctl_ICode], [Stk_Ctl_Str_Grp], [Stk_Ctl_Cur_Stk], [Stk_Ctl_Free_Stk], [Stk_Ctl_On_Ord_Stk], [Stk_Ctl_Ind_Stk], "
    //        +" [Stk_Ctl_Quot_Stk], [Stk_Ctl_Min_Stk], [Stk_Ctl_Reord_Stk], [Stk_Ctl_Max_Stk], [Stk_Ctl_Std_Val], [Stk_Ctl_Ave_Val], [Stk_Ctl_Lat_Val], [Stk_Ctl_FIFO_Val], "
    //        + " [Stk_Ctl_LIFO_Val], [Stk_Ctl_Lst_Rec_Dat], [Stk_Ctl_Lst_Iss_Dat], [T_C1], [T_C2], [T_Fl], [T_In]) VALUES ( '" + Stk_Ctl_SCode + "', '" + Stk_Ctl_ICode + "', "
    //        + " '" + Stk_Ctl_Str_Grp + "', " + Stk_Ctl_Cur_Stk + ", " + Stk_Ctl_Free_Stk + ", " + Stk_Ctl_On_Ord_Stk + ", " + Stk_Ctl_Ind_Stk + ", " + Stk_Ctl_Quot_Stk + ", " + Stk_Ctl_Min_Stk + ", "
    //        + " " + Stk_Ctl_Reord_Stk + ", " + Stk_Ctl_Max_Stk + ", " + Stk_Ctl_Std_Val + ", " + Stk_Ctl_Ave_Val + ", " + Stk_Ctl_Lat_Val + ", " + Stk_Ctl_FIFO_Val + ", " + Stk_Ctl_LIFO_Val + ", "
    //        + " '" + Stk_Ctl_Lst_Rec_Dat + "', '" + Stk_Ctl_Lst_Iss_Dat + "', @T_C1, @T_C2, @T_Fl, @T_In)";
    //}

    //public static string InsertIntoInmaItmStk(string itmStkIcode, double itmStkCur, string itmStkCstMeth, int itmStkBSPRat, int itmStkOSPRat, int itmStkSTDRat,
    //    int itmStkLATRat, int itmStkAVERat, string tC1, string tC2, string tFl, int tIn)
    //{
    //    return "INSERT INTO [InMa_Itm_Stk] ([Itm_Stk_Icode], [Itm_Stk_Cur], [Itm_Stk_Cst_Meth], [Itm_Stk_BSP_Rat], [Itm_Stk_OSP_Rat], [Itm_Stk_STD_Rat], [Itm_Stk_LAT_Rat], "
    //        + " [Itm_Stk_AVE_Rat], [T_C1], [T_C2], [T_Fl], [T_In]) VALUES ( '" + itmStkIcode + "', " + itmStkCur + ", '" + itmStkCstMeth + "', " + itmStkBSPRat + ","
    //        + " " + itmStkOSPRat + ", " + itmStkSTDRat + ", " + itmStkLATRat + ", " + itmStkAVERat + ", '" + tC1 + "', '" + tC2 + "', '" + tFl + "', " + tIn + ")";
    //}

    //public static string InsertIntoInmaStkVal(string stkValTrnType, string stkValTrnCode, string stkValTrnRef, DateTime stkValTrnDate, string stkValItmCode,
    //    string stkValItmDesc, string stkValStrCode, int stkValLatRate, int stkValAveRate, int stkValStdRate, double stkValItmQty, string fC1, string fC2,
    //    string fC3, string fC4)
    //{
    //    return "INSERT INTO [InMa_Stk_Val] ([Stk_Val_Trn_Type], [Stk_Val_Trn_Code], [Stk_Val_Trn_Ref], [Stk_Val_Trn_Date], [Stk_Val_Itm_Code], [Stk_Val_Itm_Desc], "
    //        + " [Stk_Val_Str_Code], [Stk_Val_Lat_Rate], [Stk_Val_Ave_Rate], [Stk_Val_Std_Rate], [Stk_Val_Itm_Qty], [F_C1], [F_C2], [F_C3], [F_C4]) VALUES ( '" + stkValTrnType + "', "
    //        + " '" + stkValTrnCode + "', '" + stkValTrnRef + "', '" + stkValTrnDate + "', '" + stkValItmCode + "', '" + stkValItmDesc + "', '" + stkValStrCode + "', "
    //        + " " + stkValLatRate + ", " + stkValAveRate + ", " + stkValStdRate + ", " + stkValItmQty + ", '" + fC1 + "', '" + fC2 + "', '" + fC3 + "', '" + fC4 + "')";
    //}

    //public static string InsertInmaItmSerial(string itmDetIcode, string itmDetSerialNo, string itmDetRef, string itmDetStrCode, string itmDetTrnType,
    //    string itmDetTrnCode, DateTime itmDetDate, string itmStatus, string itmRateId)
    //{
    //    return "INSERT INTO [InMa_Itm_Serial] ([itm_det_icode], [itm_det_serial_no], [itm_det_ref], [itm_det_str_code], [itm_det_trn_type], [itm_det_trn_code], [itm_det_date], "
    //        + " [itm_status], [itm_rate_id]) VALUES ('" + itmDetIcode + "', '" + itmDetSerialNo + "', '" + itmDetRef + "', '" + itmDetStrCode + "', '" + itmDetTrnType + "', "
    //        + " '" + itmDetTrnCode + "', '" + itmDetDate + "', '" + itmStatus + "', '" + itmRateId + "')";
    //}
    //#endregion Item Return

    #region Item Return

    public static string GetDataByTrnDetRef(string trnDetRef)
    {
        return " select a.Trn_Det_Ref,b.Trn_Hdr_DATE,b.Trn_Hdr_Pcode,a.Trn_Det_Lno,a.Trn_Det_Icode, a.Trn_Det_Itm_Desc,a.Trn_Det_Itm_Uom,a.Trn_Det_Lin_Qty ,a.Trn_Det_Str_Code, "
            + " a.Trn_Det_Bat_No,isnull((select sum(Trn_Det_Lin_Qty) from InTr_Trn_Det where Trn_Det_Icode=a.Trn_Det_Icode and Trn_Det_Lno=a.Trn_Det_Lno and Trn_Det_Type='RT' "
            + " and Trn_Det_Code='SRT' and Trn_Det_Ref in(select distinct Trn_Hdr_Ref from InTr_Trn_Hdr where Trn_Hdr_DC_No=a.Trn_Det_Ref)),0) as retqty from InTr_Trn_Det a "
            + " inner join InTr_Trn_Hdr b on a.Trn_Det_Ref=b.Trn_Hdr_Ref where b.Trn_Hdr_Type='IS' and Trn_Det_Ref= '" + trnDetRef + "'";
    }

    public static string GetMaxMovRefNo()
    {
        return "select  stuff('0000000',8-len(ISNULL(max (right(Movement_RefNo,7)),0)+1),20,ISNULL(max (right(Movement_RefNo,7)),0)+1) as MaxID from Item_Movement_dtl ";
    }

    public static string GetMaxMovementNo()
    {
        return "SELECT ISNULL(max (Movement_No),0)+1   as MaxID from Item_Movement_dtl";
    }

    public static string InsertQuery(int movementNo, string movementRefNo, string itmDetIcode, string itmDetSerialNo, string itmDetRef, string itmDetStrCode,
        string itmDetTrnType, string itmDetTrnCode, DateTime itmDetDate, string itmStatus, string trackingInfo, int itemQty, int itemRate, string moHdrPcode,
        DateTime moHdrDate, DateTime entryDate, string entryUserID)
    {
        return "INSERT INTO [Item_Movement_dtl] ([Movement_No], [Movement_RefNo], [itm_det_icode], [itm_det_serial_no], [itm_det_ref], [itm_det_str_code], [itm_det_trn_type], "
            + " [itm_det_trn_code], [itm_det_date], [itm_status], [TrackingInfo], [ItemQty], [ItemRate], [Mo_Hdr_Pcode], [Mo_Hdr_Date], [EntryDate], [EntryUserID]) VALUES "
            + " (" + movementNo + ", '" + movementRefNo + "', '" + itmDetIcode + "', '" + itmDetSerialNo + "', '" + itmDetRef + "', '" + itmDetStrCode + "', "
            + " '" + itmDetTrnType + "', '" + itmDetTrnCode + "', '" + itmDetDate + "', '" + itmStatus + "', '" + trackingInfo + "', " + itemQty + ", " + itemRate + ", "
            + " '" + moHdrPcode + "', '" + moHdrDate + "', '" + entryDate + "', '" + entryUserID + "')";
    }

    public static string InsertIntoTrnHdr(string trnHdrType, string trnHdrCode, string trnHdrRef, string trnHdrPcode, string trnHdrDcode, string trnHdrAcode,
        DateTime trnHdrDate, string trnHdrCom1, string trnHdrCom2, string trnHdrCom3, string trnHdrCom4, string trnHdrCom5, string trnHdrCom6, string trnHdrCom7,
        string trnHdrCom8, string trnHdrCom9, string trnHdrCom10, int trnHdrValue, string trnHdrHRPBFlag, string trnHdrEntPrd, string trnHdrOprCode, string trnHdrPrdCld,
        string trnHdrExpTyp, string trnHdrLedInt, string trnHdrDCNo, string trnHdrEIFlg, string trnHdrCno, string tC1, string tC2, string tFl, int tIn,
        int trnHdrExcDuty, DateTime trnHdrDcDate, DateTime trnHdrCIDate, string trnHdrPassNo)
    {
        return "INSERT INTO [InTr_Trn_Hdr] ([Trn_Hdr_Type], [Trn_Hdr_Code], [Trn_Hdr_Ref], [Trn_Hdr_Pcode], [Trn_Hdr_Dcode], [Trn_Hdr_Acode], [Trn_Hdr_DATE], [Trn_Hdr_Com1], "
            + " [Trn_Hdr_Com2], [Trn_Hdr_Com3], [Trn_Hdr_Com4], [Trn_Hdr_Com5], [Trn_Hdr_Com6], [Trn_Hdr_Com7], [Trn_Hdr_Com8], [Trn_Hdr_Com9], [Trn_Hdr_Com10], [Trn_Hdr_Value], "
            + " [Trn_Hdr_HRPB_Flag], [Trn_Hdr_Ent_Prd], [Trn_Hdr_Opr_Code], [Trn_Hdr_Prd_Cld], [Trn_Hdr_Exp_Typ], [Trn_Hdr_Led_Int], [Trn_Hdr_DC_No], [Trn_Hdr_EI_Flg], "
            + " [Trn_Hdr_Cno], [T_C1], [T_C2], [T_Fl], [T_In], [Trn_Hdr_exc_duty], [Trn_Hdr_Dc_Date], [Trn_Hdr_CI_Date], [Trn_Hdr_Pass_No]) VALUES ('" + trnHdrType + "', "
            + " '" + trnHdrCode + "', '" + trnHdrRef + "', '" + trnHdrPcode + "', '" + trnHdrDcode + "', '" + trnHdrAcode + "', '" + trnHdrDate + "', '" + trnHdrCom1 + "', "
            + " '" + trnHdrCom2 + "', '" + trnHdrCom3 + "', '" + trnHdrCom4 + "', '" + trnHdrCom5 + "', '" + trnHdrCom6 + "', '" + trnHdrCom7 + "', '" + trnHdrCom8 + "', "
            + " '" + trnHdrCom9 + "', '" + trnHdrCom10 + "', " + trnHdrValue + ", '" + trnHdrHRPBFlag + "', '" + trnHdrEntPrd + "', '" + trnHdrOprCode + "', "
            + " '" + trnHdrPrdCld + "', '" + trnHdrExpTyp + "', '" + trnHdrLedInt + "', '" + trnHdrDCNo + "', '" + trnHdrEIFlg + "', '" + trnHdrCno + "', '" + tC1 + "', "
            + " '" + tC2 + "', '" + tFl + "', " + tIn + ", " + trnHdrExcDuty + ", '" + trnHdrDcDate + "', '" + trnHdrCIDate + "', '" + trnHdrPassNo + "')";
    }

    public static string InsertIntoTrnDet(string trnDetType, string trnDetCode, string trnDetRef, int trnDetLno, string trnDetSfx, int trnDetExpLno, string trnDetIcode,
        string trnDetItmDesc, string trnDetItmUom, string trnDetStrCode, string trnDetBinCode, string trnDetOrdRef, int trnDetOrdLno, string trnDetBatNo,
        DateTime trnDetExpDat, DateTime trnDetBookDat, int trnDetLinQty, int trnDetUntWgt, int trnDetLinRat, int trnDetLinAmt, int trnDetLinNet, string tC1,
        string tC2, string tFl, int tIn, int trnDetBalQty)
    {
        return "INSERT INTO [InTr_Trn_Det] ([Trn_Det_Type], [Trn_Det_Code], [Trn_Det_Ref], [Trn_Det_Lno], [Trn_Det_Sfx], [Trn_Det_Exp_Lno], [Trn_Det_Icode], [Trn_Det_Itm_Desc], "
            + " [Trn_Det_Itm_Uom], [Trn_Det_Str_Code], [Trn_Det_Bin_Code], [Trn_Det_Ord_Ref], [Trn_Det_Ord_Lno], [Trn_Det_Bat_No], [Trn_Det_Exp_Dat], [Trn_Det_Book_Dat], "
            + " [Trn_Det_Lin_Qty], [Trn_Det_Unt_Wgt], [Trn_Det_Lin_Rat], [Trn_Det_Lin_Amt], [Trn_Det_Lin_Net], [T_C1], [T_C2], [T_Fl], [T_In], [Trn_Det_Bal_Qty]) VALUES "
            + " ( '" + trnDetType + "', '" + trnDetCode + "', '" + trnDetRef + "', " + trnDetLno + ", '" + trnDetSfx + "', " + trnDetExpLno + ", '" + trnDetIcode + "', "
            + " '" + trnDetItmDesc + "', '" + trnDetItmUom + "', '" + trnDetStrCode + "', '" + trnDetBinCode + "', '" + trnDetOrdRef + "', " + trnDetOrdLno + ", "
            + " '" + trnDetBatNo + "', '" + trnDetExpDat + "', '" + trnDetBookDat + "', " + trnDetLinQty + ", " + trnDetUntWgt + ", " + trnDetLinRat + ", "
            + " " + trnDetLinAmt + ", " + trnDetLinNet + ", '" + tC1 + "', '" + tC2 + "', '" + tFl + "', " + tIn + ", " + trnDetBalQty + ")";
    }

    public static string InsertIntoInmaStkCtl(string Stk_Ctl_SCode, string Stk_Ctl_ICode, string Stk_Ctl_Str_Grp, double Stk_Ctl_Cur_Stk, double Stk_Ctl_Free_Stk, int Stk_Ctl_On_Ord_Stk,
        int Stk_Ctl_Ind_Stk, int Stk_Ctl_Quot_Stk, int Stk_Ctl_Min_Stk, int Stk_Ctl_Reord_Stk, int Stk_Ctl_Max_Stk, int Stk_Ctl_Std_Val, int Stk_Ctl_Ave_Val, int Stk_Ctl_Lat_Val,
        int Stk_Ctl_FIFO_Val, int Stk_Ctl_LIFO_Val, DateTime Stk_Ctl_Lst_Rec_Dat, DateTime Stk_Ctl_Lst_Iss_Dat, string p15, string p16, string p17, int p18)
    {
        return "INSERT INTO [InMa_Stk_Ctl] ([Stk_Ctl_SCode], [Stk_Ctl_ICode], [Stk_Ctl_Str_Grp], [Stk_Ctl_Cur_Stk], [Stk_Ctl_Free_Stk], [Stk_Ctl_On_Ord_Stk], [Stk_Ctl_Ind_Stk], "
            + " [Stk_Ctl_Quot_Stk], [Stk_Ctl_Min_Stk], [Stk_Ctl_Reord_Stk], [Stk_Ctl_Max_Stk], [Stk_Ctl_Std_Val], [Stk_Ctl_Ave_Val], [Stk_Ctl_Lat_Val], [Stk_Ctl_FIFO_Val], "
            + " [Stk_Ctl_LIFO_Val], [Stk_Ctl_Lst_Rec_Dat], [Stk_Ctl_Lst_Iss_Dat], [T_C1], [T_C2], [T_Fl], [T_In]) VALUES ( '" + Stk_Ctl_SCode + "', '" + Stk_Ctl_ICode + "', "
            + " '" + Stk_Ctl_Str_Grp + "', " + Stk_Ctl_Cur_Stk + ", " + Stk_Ctl_Free_Stk + ", " + Stk_Ctl_On_Ord_Stk + ", " + Stk_Ctl_Ind_Stk + ", " + Stk_Ctl_Quot_Stk + ", " + Stk_Ctl_Min_Stk + ", "
            + " " + Stk_Ctl_Reord_Stk + ", " + Stk_Ctl_Max_Stk + ", " + Stk_Ctl_Std_Val + ", " + Stk_Ctl_Ave_Val + ", " + Stk_Ctl_Lat_Val + ", " + Stk_Ctl_FIFO_Val + ", " + Stk_Ctl_LIFO_Val + ", "
            + " '" + Stk_Ctl_Lst_Rec_Dat + "', '" + Stk_Ctl_Lst_Iss_Dat + "', @T_C1, @T_C2, @T_Fl, @T_In)";
    }

    public static string InsertIntoInmaItmStk(string itmStkIcode, double itmStkCur, string itmStkCstMeth, int itmStkBSPRat, int itmStkOSPRat, int itmStkSTDRat,
        int itmStkLATRat, int itmStkAVERat, string tC1, string tC2, string tFl, int tIn)
    {
        return "INSERT INTO [InMa_Itm_Stk] ([Itm_Stk_Icode], [Itm_Stk_Cur], [Itm_Stk_Cst_Meth], [Itm_Stk_BSP_Rat], [Itm_Stk_OSP_Rat], [Itm_Stk_STD_Rat], [Itm_Stk_LAT_Rat], "
            + " [Itm_Stk_AVE_Rat], [T_C1], [T_C2], [T_Fl], [T_In]) VALUES ( '" + itmStkIcode + "', " + itmStkCur + ", '" + itmStkCstMeth + "', " + itmStkBSPRat + ","
            + " " + itmStkOSPRat + ", " + itmStkSTDRat + ", " + itmStkLATRat + ", " + itmStkAVERat + ", '" + tC1 + "', '" + tC2 + "', '" + tFl + "', " + tIn + ")";
    }

    public static string InsertIntoInmaStkVal(string stkValTrnType, string stkValTrnCode, string stkValTrnRef, DateTime stkValTrnDate, string stkValItmCode,
        string stkValItmDesc, string stkValStrCode, int stkValLatRate, int stkValAveRate, int stkValStdRate, double stkValItmQty, string fC1, string fC2,
        string fC3, string fC4)
    {
        return "INSERT INTO [InMa_Stk_Val] ([Stk_Val_Trn_Type], [Stk_Val_Trn_Code], [Stk_Val_Trn_Ref], [Stk_Val_Trn_Date], [Stk_Val_Itm_Code], [Stk_Val_Itm_Desc], "
            + " [Stk_Val_Str_Code], [Stk_Val_Lat_Rate], [Stk_Val_Ave_Rate], [Stk_Val_Std_Rate], [Stk_Val_Itm_Qty], [F_C1], [F_C2], [F_C3], [F_C4]) VALUES ( '" + stkValTrnType + "', "
            + " '" + stkValTrnCode + "', '" + stkValTrnRef + "', '" + stkValTrnDate + "', '" + stkValItmCode + "', '" + stkValItmDesc + "', '" + stkValStrCode + "', "
            + " " + stkValLatRate + ", " + stkValAveRate + ", " + stkValStdRate + ", " + stkValItmQty + ", '" + fC1 + "', '" + fC2 + "', '" + fC3 + "', '" + fC4 + "')";
    }

    public static string InsertInmaItmSerial(string itmDetIcode, string itmDetSerialNo, string itmDetRef, string itmDetStrCode, string itmDetTrnType,
        string itmDetTrnCode, DateTime itmDetDate, string itmStatus, string itmRateId)
    {
        return "INSERT INTO [InMa_Itm_Serial] ([itm_det_icode], [itm_det_serial_no], [itm_det_ref], [itm_det_str_code], [itm_det_trn_type], [itm_det_trn_code], [itm_det_date], "
            + " [itm_status], [itm_rate_id]) VALUES ('" + itmDetIcode + "', '" + itmDetSerialNo + "', '" + itmDetRef + "', '" + itmDetStrCode + "', '" + itmDetTrnType + "', "
            + " '" + itmDetTrnCode + "', '" + itmDetDate + "', '" + itmStatus + "', '" + itmRateId + "')";
    }

    public static string GetMaxID()
    {
        return "SELECT MAX(CONVERT(int, RIGHT(itm_rate_id, 6))) + 1 AS maxid FROM InMa_Itm_Rate";
    }

    public static string InsertQuery(string itmRateScode, string itmRateIcode, string itmRateTrnRef, DateTime itmRateTrndate, decimal itmRateQty, int itmRateRate,
        int itmRateLineno, string itmRateId, string itmRateIdGrp)
    {
        return "INSERT INTO [InMa_Itm_Rate] ([itm_rate_scode], [itm_rate_icode], [itm_rate_trn_ref], [itm_rate_trndate], [itm_rate_qty], [itm_rate_rate], [itm_rate_lineno], "
            + " [itm_rate_id], [itm_rate_id_grp]) VALUES ('" + itmRateScode + "', '" + itmRateIcode + "', '" + itmRateTrnRef + "', '" + itmRateTrndate + "', " + itmRateQty + ", "
            + " " + itmRateRate + ", " + itmRateLineno + ", '" + itmRateId + "', '" + itmRateIdGrp + "')";
    }

    public static string GetDataByitm_rate_icode(string itmRateScode, string itmRateIcode)
    {
        string sql = "";
        return sql = "SELECT itm_rate_scode, itm_rate_icode, itm_rate_trn_ref, itm_rate_trndate, itm_rate_qty, itm_rate_rate, itm_rate_lineno, itm_rate_id, itm_rate_id_grp, "
        + " AutoID FROM InMa_Itm_Rate WHERE (itm_rate_scode = '" + itmRateScode + "') AND (itm_rate_icode = '" + itmRateIcode + "')";
    }

    #endregion Item Return

    public static string GetDataByTrn_Hdr_Ref(string trnHdrRef)
    {
        return "SELECT InTr_Trn_Det.Trn_Det_Icode, InTr_Trn_Det.Trn_Det_Itm_Desc, InTr_Trn_Hdr.Trn_Hdr_Ref, InTr_Trn_Hdr.Trn_Hdr_DATE, InTr_Trn_Det.Trn_Det_Lin_Qty, "
                         + " InTr_Trn_Det.Trn_Det_Itm_Uom, InTr_Trn_Det.Trn_Det_Bat_No, SaMa_Par_Adr.par_adr_name, SaMa_Par_Adr.Par_Adr_Code, SaMa_Par_Adr.Par_Adr_Line1, "
                         + " SaMa_Par_Adr.Par_Adr_Line2, SaMa_Par_Adr.Par_Adr_Line3, SaMa_Par_Adr.Par_Adr_Line4, SaMa_Par_Adr.Par_Adr_Line5, InTr_Trn_Hdr.Trn_Hdr_Com2 "
                         + " FROM InTr_Trn_Det INNER JOIN InTr_Trn_Hdr ON InTr_Trn_Det.Trn_Det_Ref = InTr_Trn_Hdr.Trn_Hdr_Ref INNER JOIN SaMa_Par_Adr ON "
                         + " InTr_Trn_Hdr.Trn_Hdr_Pcode = SaMa_Par_Adr.Par_Adr_Code where Trn_Hdr_Ref= '" + trnHdrRef + "'";
    }


    #region Stock Transfer

    public static string GetMaxTransRef(DateTime datePeriod)
    {
        return "SELECT MAX(RIGHT(Trn_Hdr_Ref, 6)) AS MaxTransRefNo FROM InTr_Trn_Hdr WHERE (Trn_Hdr_Type  in ('IT','RT')) AND (Trn_Hdr_Code ='STR' ) AND "
            + " (CONVERT(DATETIME, Trn_Hdr_DATE, 103) >= CONVERT(datetime, '" + datePeriod + "', 103))";
    }

    public static string GetItemRateByItem(string itmRateScode, string itmRateIcode, string itmRateId, int itmRateLineno)
    {
        return "SELECT itm_rate_icode, itm_rate_id, itm_rate_id_grp, itm_rate_lineno, itm_rate_qty, itm_rate_rate, itm_rate_scode, itm_rate_trn_ref, itm_rate_trndate FROM "
            + " InMa_Itm_Rate WHERE (itm_rate_scode = '" + itmRateScode + "') AND (itm_rate_icode = '" + itmRateIcode + "') AND (itm_rate_id = '" + itmRateId + "') AND (itm_rate_lineno = " + itmRateLineno + ")";
    }

    public static string EditItemRateQty(decimal itmRateOty, string itmRateScode, string itmRateIcode, string itmRateId, int itmRateLineno)
    {
        return "UPDATE [InMa_Itm_Rate]  SET [itm_rate_qty]= " + itmRateOty + " WHERE [itm_rate_scode]= '" + itmRateScode + "' AND [itm_rate_icode]= '" + itmRateIcode + "' AND "
            + " [itm_rate_id]= '" + itmRateId + "' AND [itm_rate_lineno]= " + itmRateLineno + "";
    }

    public static string GetTransHdrByRefNo(string trnHdrRef)
    {
        return "SELECT T_C1, T_C2, T_Fl, T_In, Trn_Hdr_Acode, Trn_Hdr_CI_Date, Trn_Hdr_Cno, Trn_Hdr_Code, Trn_Hdr_Com1, Trn_Hdr_Com10, Trn_Hdr_Com2, Trn_Hdr_Com3, Trn_Hdr_Com4, "
            +" Trn_Hdr_Com5, Trn_Hdr_Com6, Trn_Hdr_Com7, Trn_Hdr_Com8, Trn_Hdr_Com9, Trn_Hdr_DATE, Trn_Hdr_DC_No, Trn_Hdr_Dc_Date, Trn_Hdr_Dcode, Trn_Hdr_EI_Flg, Trn_Hdr_Ent_Prd, "
            +" Trn_Hdr_Exp_Typ, Trn_Hdr_HRPB_Flag, Trn_Hdr_Led_Int, Trn_Hdr_Opr_Code, Trn_Hdr_Pass_No, Trn_Hdr_Pcode, Trn_Hdr_Prd_Cld, Trn_Hdr_Ref, Trn_Hdr_Type, Trn_Hdr_Value, "
            + " Trn_Hdr_exc_duty FROM InTr_Trn_Hdr WHERE (Trn_Hdr_Type IN ('IT', 'RT')) AND (Trn_Hdr_Code = 'STR') AND (Trn_Hdr_Ref = '" + trnHdrRef + "')";
    }

    public static string GetTransDetByRefNo(string trnDetRef)
    {
        return "SELECT T_C1, T_C2, T_Fl, T_In, Trn_Det_Bal_Qty, Trn_Det_Bat_No, Trn_Det_Bin_Code, Trn_Det_Book_Dat, Trn_Det_Code, Trn_Det_Exp_Dat, Trn_Det_Exp_Lno, Trn_Det_Icode, "
            +" Trn_Det_Itm_Desc, Trn_Det_Itm_Uom, Trn_Det_Lin_Amt, Trn_Det_Lin_Net, Trn_Det_Lin_Qty, Trn_Det_Lin_Rat, Trn_Det_Lno, Trn_Det_Ord_Lno, Trn_Det_Ord_Ref, Trn_Det_Ref, "
            +" Trn_Det_Sfx, Trn_Det_Str_Code, Trn_Det_Type, Trn_Det_Unt_Wgt FROM InTr_Trn_Det WHERE (Trn_Det_Type IN ('IT')) AND (Trn_Det_Code = 'STR') AND "
            + " (Trn_Det_Ref = '" + trnDetRef + "') ORDER BY Trn_Det_Lno";
    }
    #endregion Stock Transfer

    public static string UpdateQueryForQc(double pODetInsQTY, double pODetOkQty, string pODetRef, string pODetIcode, int pODetLno)
    {
        string sql="";
        return sql=@"update PuTr_PO_Det set PO_Det_Ins_QTY=PO_Det_Ins_QTY-(" + pODetInsQTY + "),PO_Det_Qc_QTY=PO_Det_Qc_QTY+(" + pODetOkQty + ")"
                    + " where PO_Det_Ref='" + pODetRef + "' and PO_Det_Icode='" + pODetIcode + "' and PO_Det_Lno=" + pODetLno + "";
    }

    public static string InsertMatRecRet( string trn_ref_no, int trn_seq_no, string trn_type, DateTime trn_datetime, string user_code, 
        string po_ref, int po_line_no, string pcode, string pdet, string icode, string idet, string uom, string brand, 
        string origin, string packing, double itm_rec_ret_qty, double po_qty, double org_qty, double ins_qty, double bal_qty, double po_line_rate, string status_det, string comments)
    {
        string sql="";
        return sql=@"INSERT INTO [tbl_mat_rec_ret] ([trn_ref_no], [trn_seq_no], [trn_type], [trn_datetime], [user_code], [po_ref], [po_line_no], [pcode], [pdet], [icode], [idet], [uom], [brand], [origin], [packing], [itm_rec_ret_qty], [po_qty], [org_qty], [ins_qty], [bal_qty], [po_line_rate], [status_det], [comments]) "
        + " VALUES ('" + trn_ref_no + "', " + trn_seq_no + ", '" + trn_type + "', Convert(DateTime,'" + trn_datetime + "',103), '" + user_code + "'," 
        + " '" + po_ref + "', " + po_line_no + ", '" + pcode + "', '" + pdet + "', '" + icode + "', '" + idet + "', '" + uom + "', '" + brand + "', '" + origin + "',"
        + " '" + packing + "', " + itm_rec_ret_qty + ", " + po_qty + ", " + org_qty + ", " + ins_qty + ", " + bal_qty + ", " + po_line_rate + ", '" + status_det + "', '" + comments + "')";
    }


    public static string GetHdrDataByRef(string referencenumber)
    {
        string sql="";
        return sql = @"SELECT PO_Hdr_Acode, PO_Hdr_Cmt_Acc, PO_Hdr_Code, PO_Hdr_Com1, PO_Hdr_Com10, PO_Hdr_Com2, PO_Hdr_Com3, PO_Hdr_Com4," 
                    + " PO_Hdr_Com5, PO_Hdr_Com6, PO_Hdr_Com7, PO_Hdr_Com8, PO_Hdr_Com9, PO_Hdr_DATE, PO_Hdr_Dcode, PO_Hdr_Ent_Prd, PO_Hdr_Exp_Typ," 
                    + " PO_Hdr_HPC_Flag, PO_Hdr_Opr_Code, PO_Hdr_Pcode, PO_Hdr_Pending, PO_Hdr_Prd_Cld, PO_Hdr_Ref, PO_Hdr_Status, PO_Hdr_Template, PO_Hdr_Type," 
                    + " PO_Hdr_Value, Po_Hdr_Curr_Code, Po_Hdr_Due_Prd, Po_Hdr_Exch_rate, Po_Hdr_Ord_Date, T_C1, T_C2, T_Fl, T_In, po_hdr_due_date FROM PuTr_PO_Hdr"
                    + " WHERE (PO_Hdr_Ref = '" + referencenumber + "')";
    }

    public static string InsertItemSerial(string itm_det_icode, string itm_det_serial_no, string itm_det_ref, string itm_det_str_code, string itm_det_trn_type, string itm_det_trn_code, DateTime itm_det_date, string itm_status, 
                    string TrackingInfo, double ItemQty, double ItemRate)
    {
        string sql = "";
        return sql =@"INSERT INTO [FAS_InMa_Itm_Serial] ([itm_det_icode], [itm_det_serial_no], [itm_det_ref], [itm_det_str_code], [itm_det_trn_type], [itm_det_trn_code]," 
                    + " [itm_det_date], [itm_status], [TrackingInfo], [ItemQty], [ItemRate])" 
                    + " VALUES ('" + itm_det_icode + "', '" + itm_det_serial_no + "', '" + itm_det_ref + "', '" + itm_det_str_code + "', '" + itm_det_trn_type + "', '" + itm_det_trn_code + "', Convert(datetime,'" + itm_det_date + "',103), '" + itm_status + "'," 
                    + " '" + TrackingInfo + "', '" + ItemQty + "', '" + ItemRate + "')";
    }

    #region Revalueation Of Item

    public static string GetItemtrackingNo(string itemCoed)
    {
        string sql = "";
        return sql = "select trackingInfo from fas_item_depreciation where ItemCurrentLine = 'Y' and ItemCode = '" + itemCoed + "'";
    }

    public static string GetItemInformationByCodeTrack(string itemCode, string trackingNo)
    {
        string sql = "";
        return sql = "SELECT distinct b.Itm_Det_Desc,a.ItemInitialValue,a.WrittenDownValue,a.ItemDepreciationSL FROM fas_item_depreciation  a INNER JOIN InMa_Itm_Det b ON a.ItemCode = b.Itm_Det_ICode WHERE a.ItemCurrentLine = 'Y'  and a.ItemCode = '" + itemCode + "' AND a.trackingInfo = '" + trackingNo + "' ";
    }

    public static string CheckItemRevalueationInformation(string itemCode, string trackingNumber, int lineNumber)
    {
        string sql = "";
        return sql = "SELECT * FROM FA_Item_Revalueation WHERE ItemCode = '" + itemCode + "' AND TrackingNo = '" + trackingNumber + "' AND LineNumber = " + lineNumber + " ";
    }

    public static string GetTrnType(int TypeId)
    {
        string sql = "";
        return sql = "SELECT TypeId,TypeName FROM Fas_AssetEvaluationType WHERE TypeId =" + TypeId + "";
    }

    #endregion Revalueation Of Item

    #region Customer Account

    public static string GetDataByCliPen()
    {
        string sql = "";
        return sql = "SELECT brCliCode, brSlNo, brAdrCode, brAdrNewCode, brName, brGroupId, brGroup, brMktGrpId, brMktGroup, brClientTypeId, brClientType, brAddressTypeID, "
                      + " brAddressTypeName, brBusinessTypeId, brBusinessType, brCategoryId, brCategory, brNatureId, brNature, brStatus, brInsStatus, brCrStatus, "
                      + " brBlStatus, brSearch, SdStatus, brBlStdate, brLastBlDate, brAddress1, brAddress2, brAreaGroupId, brAreaGroup, brAreaId, brArea, brPostalArea, "
                      + " brwebsite, brstatussla, brsladate, brdateinception, brcompanyname, branchmanager, brSupportOfficeId, brSupportOffice, contact_det, "
                      + " Contact_Designation, phone_no, fax_no, email_id, mrtg_link, note_for_services, bw_as_client, add_for_p2p, note_for_bts, cur_status, "
                      + " final_update_from, final_update_by, final_update_date, i_bill_date, i_seller, i_acc_manager, i_ins_ref_no, i_ins_date, i_ins_engg, i_bill_mgr, "
                      + " i_terms_cond, sll, UpdStatus, ClientUpdStatus FROM clientDatabaseMain where UpdStatus='N' and brSlNo=1 order by brName";
    }
    #endregion Customer Account

    #region Asset Categorization

    public static string SqlCheckAssetName(string assetName)
    {
        string sql = "";
        return sql = "SELECT Grp_Code_Name FROM InMa_Grp_Code WHERE Grp_Code_Name = '" + assetName + "'";
    }
    #endregion Asset Categorization

    #region Purchase Item Mapping

    public static string SqlGetPurchaseItemMappingRecord(string supplierCode, string itemCode)
    {
        return "select ParAccCode from Inma_Purchase_Item_Mapping where ParAccCode = '" + supplierCode + "' and ItemCode = '" + itemCode + "'";
    }
    #endregion Purchase Item Mapping

    #region Store Location
    public static string DeleteStoreLocation(string storeLocationId)
    {
        string sql = "";
        return sql = "DELETE FROM  InMa_Str_Loc WHERE Str_Loc_Id = '" + storeLocationId + "'";
    }

    public static string CheckStoreLocationID(string storeLocationID)
    {
        string sql = "";
        return sql = "SELECT Str_Loc_Id FROM InMa_Str_Loc WHERE Str_Loc_Id = '" + storeLocationID + "'";
    }

    public static string CheckStoreLocationName(string storeLocationName)
    {
        string sql = "";
        return sql = "SELECT Str_Loc_Id FROM InMa_Str_Loc WHERE Str_Loc_Name = '" + storeLocationName + "'";
    }
    #endregion Store Location

    public static string GetConvertedDataForFixedAsset()
    {
        string sql = "";
        sql = @"SELECT ViewAssetN.Trn_Det_Code, dbo.ViewAssetN.Trn_Hdr_Pcode, ViewAssetN.isFAUpdated,ViewAssetN.Trn_Det_Icode, 
                         ViewAssetN.Trn_Det_Lin_Qty,ViewAssetN.Trn_Det_Lin_Rat,ViewAssetN.Trn_Det_Lin_Amt,ViewAssetN.Trn_Det_Itm_Desc, 
                         ViewAssetN.Trn_Hdr_DATE,ViewAssetN.RefNo, isnull(ViewAssetN.par_adr_name,'') as par_adr_name, InMa_Itm_Det.ItemTypeId
                         FROM ViewAssetN 
                         INNER JOIN InMa_Itm_Det ON ViewAssetN.Trn_Det_Icode = InMa_Itm_Det.Itm_Det_Icode
                         WHERE (InMa_Itm_Det.ItemTypeId = 1) and isFAUpdated='0' and Trn_Det_Type='IS' 
                         group by ViewAssetN.Trn_Det_Code,ViewAssetN.Trn_Hdr_Pcode,ViewAssetN.isFAUpdated,ViewAssetN.Trn_Det_Icode, 
                         ViewAssetN.Trn_Det_Lin_Qty,ViewAssetN.Trn_Det_Lin_Rat,ViewAssetN.Trn_Det_Lin_Amt,ViewAssetN.Trn_Det_Itm_Desc, 
                         ViewAssetN.Trn_Hdr_DATE,ViewAssetN.RefNo,ViewAssetN.par_adr_name,InMa_Itm_Det.ItemTypeId  order by refno";

        return sql;

    }

    public static string GetDataByItemGrpIcode(string Icode)
    {
        string sql = "";
        sql = @"SELECT Accu_Depre_Acc_Code, Cash_Acc_Dis, Dis_Acc_code, Dpre_Acc_code, Fxd_Acc_code, Fxd_First_Grp, Fxd_Second_Grp, 
                Grp_Code_Name, Grp_Code_Ref_No, Itm_Det_Acc_code, Itm_Det_desc, Itm_Grp_icode, Itm_grp_Id, Itm_grp_code, Pro_Depre_Acc_code, 
                Revo_Acc_Code FROM FAS_FixedAssetSetUp WHERE (Itm_Grp_icode ='" + Icode + "')";

        return sql;
    }

    public static string GetLCExpense(string pType)
    {
        string sql = "";
        return sql = "select * from AccLcExpenseHead where POType='" + pType + "' order by sequence";
    }

    public static string InsertLcExpense(string pono, string lcno, string Mrrno, string expid, double amt,string AccCode, string entryuser, DateTime entrydate)
    {
        string sql = "";
        return sql = @"insert into AccLcExpenseDet([PONumber],[LCNumber],[MRRNumber],[ExpenseID],[ExpAmount],[AccHead],[EntryUserid],[Entrydate])
                values('" + pono + "','" + lcno + "','" + Mrrno + "','" + expid + "','" + amt + "','" + AccCode + "','" + entryuser + "',Convert(Datetime,'" + entrydate + "',103))";

    }

    public static string InsertPayableProgressInfo(string grnno, DateTime adjdtp, string entryuser, DateTime entrydate)
    {
        string sql = "";        
        return sql = @"INSERT INTO [AccPaybleProgress] ([ReferenceNumber],[Status],[InvoiceReceived],[AdjustmentPeriod],
                                                [EntryUserID],[EntryDate],[UpdateUserid],
                                                [UpdateDate],Jrnupdpermission) VALUES ('" +
                                                grnno.ToString() + "','Y','N',convert(datetime,'" + adjdtp + "',103),'" + entryuser.ToString() + "',convert(datetime,'" + entrydate + "',103),'" + entryuser.ToString() + "',convert(datetime,'" + entrydate + "',103),'Y')";
             
    }

    public static string DeleteLcExpense(string pono,string mrrno)
    {
        string sql = "";
        return sql = "delete from [AccLcExpenseDet] where [PONumber]='" + pono + "' and [MRRNumber]='" + mrrno + "'";
    }

    public static string GetLcExpenseHead(string Expid)
    {
        string sql = "";
        return sql = @"select distinct AccCode from AccLCExpenseHead where ExpenseID='" + Expid + "'";

    }



}