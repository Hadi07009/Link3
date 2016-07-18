using System;
using System.Globalization;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using LibraryDAL;
using LibraryDTO;
using LibraryDAL.ErpDataSetTableAdapters;
using LibraryDAL.SCBLDataSetTableAdapters;
using LibraryDAL.SCBL2DataSetTableAdapters;
using LibraryDAL.AccDataSetTableAdapters;
using LibraryDAL.AccDataSet2TableAdapters;
using LibraryDAL.dsLinkofficeTableAdapters;

public partial class frm_mat_rec_confirm_final : System.Web.UI.Page
{  

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";
        clsStatic.MsgConfirmBox(btnconfirm, "Are you sure to confirm receive above items ?");
        if (!Page.IsPostBack)
        {
            load_all_data();
        }
        else
        {

        }
    }

    private void load_all_data()
    {
        clsMrrData[] mrrdata = (clsMrrData[])Session[clsStatic.sessionMrrDetData];
        if (mrrdata==null) return;
        if (mrrdata.Length == 0) return;

        int i;
        int cnt = mrrdata.Length;

        lblpartydet.Text = mrrdata[0].Pcode + ":" + mrrdata[0].Pdet;
        lblmodeofdel.Text = mrrdata[0].Modeofdel;
        lbldate.Text = mrrdata[0].Purdate.ToShortDateString();

        DataTable dt = new DataTable();
        dt.Rows.Clear();
        dt.Columns.Clear();

        dt.Columns.Add("SL", typeof(int));
        dt.Columns.Add("REF", typeof(string));
        dt.Columns.Add("PARTY", typeof(string));
        dt.Columns.Add("ICODE", typeof(string));
        dt.Columns.Add("IDET", typeof(string));
        dt.Columns.Add("UOM", typeof(string));
        dt.Columns.Add("REC QTY", typeof(decimal));
        dt.Columns.Add("BRAND", typeof(string));
        dt.Columns.Add("ORIGIN", typeof(string));
        dt.Columns.Add("PACKING", typeof(string));

        for (i = 0; i < cnt; i++)
        {
            if(mrrdata[i]==null) break;
            dt.Rows.Add(mrrdata[i].Seqno, mrrdata[i].Ref_no, mrrdata[i].Pdet, mrrdata[i].Icode, mrrdata[i].Idet, mrrdata[i].Uom, mrrdata[i].Entryqty, mrrdata[i].Brand, mrrdata[i].Origin, mrrdata[i].Packing);
        }

        if (mrrdata[0].Ref_no.ToString().Substring(0, 1) == "S")
        {
            tblspo.Visible = true;
            lblby.Text = mrrdata[0].Pur_by;
            lblfrom.Text = mrrdata[0].Pur_from;
        }
        else
        {
            tblspo.Visible = false;
        }

     
        gdItem.DataSource = dt;
        gdItem.DataBind();

    }




    private string get_mat_mrr_ref(string mrrtype, DateTime curdate)
    {
        string ref_no = "";
        InSu_Trn_SetTableAdapter set = new InSu_Trn_SetTableAdapter();
        ErpDataSet.InSu_Trn_SetDataTable dtset = new ErpDataSet.InSu_Trn_SetDataTable();

        dtset = set.GetDataByType_Code("RC", mrrtype);

        string set_ref = dtset[0].Trn_Set_Iq_Next_No;

        double max_ref = Convert.ToDouble(set_ref);
        ref_no = dtset[0].Trn_Set_Tr_Pfix + string.Format("{0:00}", Convert.ToInt32(curdate.Year.ToString().Substring(2, 2))) + string.Format("{0:00}", curdate.Month) + "-" + string.Format("{0:00000}", max_ref);
        return ref_no;
    }

    public static string GetMonthName()
    {
        DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);
        return info.GetAbbreviatedMonthName(DateTime.Now.Month).ToUpper();

    }


    private string get_code_type(string glcode)
    {
        AccCOATableAdapter gl = new AccCOATableAdapter();
        AccDataSet.AccCOADataTable dt = new AccDataSet.AccCOADataTable();
        dt = gl.GetDataByCode(glcode);
        if (dt.Rows.Count == 0)
        {
            return "";
        }
        else
        {
            return dt[0].Gl_Coa_Type.ToString();
        }
    }

    private string get_code_type_budg(string glcode)
    {
        budgTableAdapter bug = new budgTableAdapter();

        return bug.GetDataByCoaCode(glcode)[0].Gl_Coa_Type.ToString();

    }


    //private string getGrp1(string icode)
    //{
    //    InMa_Itm_GrpTableAdapter grp = new InMa_Itm_GrpTableAdapter();
    //    return grp.GetDataByIcode(icode, "I01")[0].Itm_Grp_Code;
    //}

    //private string getGrp6(string grp1)
    //{
    //    FA_COM_CCGTableAdapter ccg = new FA_COM_CCGTableAdapter();
    //    ErpDataSet.FA_COM_CCGDataTable dt = new ErpDataSet.FA_COM_CCGDataTable();
    //    dt = ccg.GetDataByCcgCode(grp1);
    //    if (dt.Rows.Count == 0) return "-";
    //    else
    //        return dt[0].Ccg_Cost_Id;

    //}

    private clsJVdata getjvdata(string poref, string mrr_ref, string pur_type, string plant, string trn_type, string dcode, string com4, string accode, string itmcode, string lcno)
    {
        tbl_jv_code_detTableAdapter jcode = new tbl_jv_code_detTableAdapter();
        SCBL2DataSet.tbl_jv_code_detDataTable dt = new SCBL2DataSet.tbl_jv_code_detDataTable();

        InMa_Itm_DetTableAdapter itmdet = new InMa_Itm_DetTableAdapter();
        ErpDataSet.InMa_Itm_DetDataTable dtitm = new ErpDataSet.InMa_Itm_DetDataTable();

        clsJVdata jv = new clsJVdata();

        plant = plant.Substring(0, 2).ToString() + "MPR";

        dt = jcode.GetDataByPurMrr(pur_type, plant);
        if (dt.Rows.Count == 0) return null;

        if (itmcode != "")
        {
            dtitm = itmdet.GetItemByCode(itmcode);
            if (dtitm.Rows.Count == 0) return null;

            //update for new requirement from azad
            if (dtitm[0].Itm_Det_Acc_code != "") { itmcode = dtitm[0].Itm_Det_Acc_code; }

        }

       



        jv.Wrk_trn_type = trn_type;
        jv.Wrk_narration = pur_type + "//" + poref + "//" + mrr_ref;
        jv.Adrcode = dcode;
        jv.Wrk_match = "";

        switch (pur_type)
        {
            case "LPO":
                if (trn_type == "D")
                {                    
                    if (dtitm[0].Itm_Det_Type_flag == "R")
                    {
                        jv.Wrk_ac_code = dtitm[0].T_C2.ToString();
                        jv.Wrk_ac_type = get_code_type(jv.Wrk_ac_code);
                        jv.Grp1 = itmcode;
                        jv.Grp2 = "-";
                        jv.Grp6 = "T13";
                        jv.Grp7 = "-";
                    }
                    else
                    {
                        jv.Wrk_ac_code = dtitm[0].T_C2.ToString();
                        jv.Wrk_ac_type = get_code_type(jv.Wrk_ac_code);
                        jv.Grp1 = "-";
                        jv.Grp2 = "-";
                        jv.Grp6 = "-";
                        jv.Grp7 = "-";
                    }
                    
                }
                else
                {
                    jv.Wrk_ac_code = accode;
                    jv.Wrk_ac_type = "S";
                    jv.Grp1 = "-";
                    jv.Grp2 = "-";
                    jv.Grp6 = "-";
                    jv.Grp7 = "-";
                }

                break;
            case "SPO":
                if (trn_type == "D")
                {
                    
                    if (dtitm[0].Itm_Det_Type_flag == "R")
                    {
                        jv.Wrk_ac_code = dtitm[0].T_C2.ToString();
                        jv.Wrk_ac_type = get_code_type(jv.Wrk_ac_code);
                        jv.Grp1 = itmcode;
                        jv.Grp2 = "-";
                        jv.Grp6 = "T13";
                        jv.Grp7 = "-";
                    }
                    else
                    {
                        jv.Wrk_ac_code = dtitm[0].T_C2.ToString();
                        jv.Wrk_ac_type = get_code_type(jv.Wrk_ac_code);
                        jv.Grp1 = "-";
                        jv.Grp2 = "-";
                        jv.Grp6 = "-";
                        jv.Grp7 = "-";
                    }
                    
                }
                else
                {
                    jv.Wrk_ac_code = dt[0].crd_acc_code;
                    jv.Wrk_ac_type = get_code_type_budg(jv.Wrk_ac_code);
                    jv.Grp1 = dcode;
                    jv.Grp2 = "-";
                    jv.Grp6 = "T01";
                    jv.Grp7 = "-";
                }

                break;
            case "FPO":

                if (trn_type == "D")
                {
                    
                    if (dtitm[0].Itm_Det_Type_flag == "R")
                    {
                        jv.Wrk_ac_code = dtitm[0].T_C2.ToString();
                        jv.Wrk_ac_type = get_code_type(jv.Wrk_ac_code);
                        jv.Grp1 = itmcode;
                        jv.Grp2 = "-";
                        jv.Grp6 = "T13";
                        jv.Grp7 = "-";
                    }
                    else
                    {
                        jv.Wrk_ac_code = dtitm[0].T_C2.ToString();
                        jv.Wrk_ac_type = get_code_type(jv.Wrk_ac_code);
                        jv.Grp1 = "-";
                        jv.Grp2 = "-";
                        jv.Grp6 = "-";
                        jv.Grp7 = "-";
                    }
                    
                }
                else
                {
                    jv.Wrk_ac_code = dt[0].crd_acc_code;
                    jv.Wrk_ac_type = get_code_type_budg(jv.Wrk_ac_code);
                    jv.Grp1 = lcno;
                    jv.Grp2 = itmcode;
                    jv.Grp6 = "T12";
                    jv.Grp7 = "T13";
                }

                break;

            default:
                return null;
                

        }


        return jv;
    }

  

    protected void btnconfirm_Click(object sender, EventArgs e)
    {

        clsMrrData[] mrrdata = (clsMrrData[])Session[clsStatic.sessionMrrDetData];
        if (mrrdata == null) return;
        if (mrrdata.Length == 0) return;
        
        tbl_item_budgetTableAdapter budg = new tbl_item_budgetTableAdapter();
        tblUserInfoTableAdapter userinfo = new tblUserInfoTableAdapter();
        InTr_Trn_HdrTableAdapter scfhdr = new InTr_Trn_HdrTableAdapter();
        InTr_Trn_DetTableAdapter scfdet = new InTr_Trn_DetTableAdapter();
        InTr_Trn_CalTableAdapter scfcal = new InTr_Trn_CalTableAdapter();
        InTr_Trn_ExtTableAdapter scfext = new InTr_Trn_ExtTableAdapter();
        InSu_Trn_SetTableAdapter scfset = new InSu_Trn_SetTableAdapter();
        InMa_Stk_ValTableAdapter scfstkval = new InMa_Stk_ValTableAdapter();
        InMa_Stk_CtlTableAdapter scfstkctl = new InMa_Stk_CtlTableAdapter();
        InMa_Itm_StkTableAdapter scfitmstk = new InMa_Itm_StkTableAdapter();
        AccTransactionHeaderTableAdapter scfwh = new AccTransactionHeaderTableAdapter();
        AccTransactionDetailsTableAdapter scfwd = new AccTransactionDetailsTableAdapter();
        AccTransactionAnalysisTableAdapter scfanal = new AccTransactionAnalysisTableAdapter();
        AccCoaGroupCodeSetupTableAdapter ccg = new AccCoaGroupCodeSetupTableAdapter();

        ErpDataSet.InMa_Itm_StkRow dritm;
        ErpDataSet.InMa_Stk_CtlRow[] scfstkdr = new ErpDataSet.InMa_Stk_CtlRow[mrrdata.Length];
        InSu_Trn_CalTableAdapter scftrncal = new InSu_Trn_CalTableAdapter();
        ErpDataSet.InSu_Trn_CalDataTable dtcal = new ErpDataSet.InSu_Trn_CalDataTable();

        PuTr_IN_Hdr_ScblTableAdapter inhdr=new PuTr_IN_Hdr_ScblTableAdapter();
        PuTr_PO_Hdr_ScblTableAdapter hdr = new PuTr_PO_Hdr_ScblTableAdapter();       
        PuTr_PO_Det_Scbl2TableAdapter det = new PuTr_PO_Det_Scbl2TableAdapter();
        SCBL2DataSet.PuTr_PO_Det_Scbl2DataTable dtdet = new SCBL2DataSet.PuTr_PO_Det_Scbl2DataTable();
        tbl_mat_rec_retTableAdapter insp = new tbl_mat_rec_retTableAdapter();
        SCBL2DataSet.tbl_mat_rec_retDataTable dtinsp = new SCBL2DataSet.tbl_mat_rec_retDataTable();
        SCBL2DataSet.PuTr_PO_Det_Scbl2Row[] dr = new SCBL2DataSet.PuTr_PO_Det_Scbl2Row[mrrdata.Length];
        SCBLDataSet.PuTr_PO_Hdr_ScblRow hdrdr;
        tbl_mat_rec_retTableAdapter matrec = new tbl_mat_rec_retTableAdapter();

      
        string ref_no = mrrdata[0].Ref_no;
        hdrdr = hdr.GetHdrDataByRef(ref_no)[0];
        
        string ord_type = hdrdr.PO_Hdr_Ref.Substring(0, 3).ToString();
        
        string mrr_type = hdrdr.PO_Hdr_Code.Substring(0, 2) + "MRR";
        string mrr_ref = get_mat_mrr_ref(mrr_type, mrrdata[0].Purdate);

        //double max_wref = Convert.ToDouble(scfwh.GetMaxRefS("AJV")) + 1;
        //string fate_ref = "SJV" + mrrdata[0].Purdate.Year.ToString().Substring(2, 2) + GetMonthName() + string.Format("{0:000000}", max_wref);
        string fate_ref = clsAccounts.GetReferenceNumber("AJV", mrrdata[0].Purdate);
        
        
        dtcal = scftrncal.GetDataByTypeCode("RC", mrr_type);

        string period = mrrdata[0].Purdate.Year.ToString() + "/" + string.Format("{0:00}", mrrdata[0].Purdate.Month);

        string cur_session;
        int curyear = mrrdata[0].Purdate.Year;
        if (mrrdata[0].Purdate.Month > 6) cur_session = curyear.ToString() + "/" + (curyear + 1).ToString();
        else cur_session = (curyear - 1).ToString() + "/" + curyear.ToString();

        
        if (hdrdr.PO_Hdr_Status != "APP") return;
        bool updateflg = false;


        string oprcode = current.UserId.ToString();
        if (oprcode.Length > 3) oprcode = oprcode.Substring(0, 3);

        decimal linqty, orgqty, insqty, availqty, entryqty, balanceqty, amnt, totamnt, calval, avrate, stdrate, avval, totd, totc, sdtval, latval, gqty, gamnt, grate;
        int i;
        int cnt = mrrdata.Length;
        int tot = 0;
        totamnt = 0;

        string last_num = scfset.GetDataByType_Code("RC", mrr_type)[0].Trn_Set_Iq_Next_No.ToString();
        string next_num = string.Format("{0:00000}", Convert.ToInt32(last_num) + 1);

        string crdanal, calcode, analgrp, f_itm_code = "";
        double totqty, totfreeqty,scfremqty;

        //for insp
        string mpr_no, po_no, mod_of_del, cert_no, lc_no;
        DateTime mpr_date, po_date, cert_date;
        double trn_jrn_code = Convert.ToDouble(scfwh.GetMaxJrnCode()) + 1;
        mod_of_del = lblmodeofdel.Text;
        po_no = ref_no;
        po_date = hdrdr.PO_Hdr_DATE;
        dtinsp = insp.GetDataByTypePoIcode("OK", ref_no, mrrdata[0].Icode);
        cert_no = dtinsp[0].trn_ref_no;
        cert_date = dtinsp[0].trn_datetime;
        mpr_no = det.GetDataByReff(ref_no)[0].PO_Det_Pr_Ref;
        mpr_date = inhdr.GetDataByRef(mpr_no)[0].IN_Hdr_St_DATE;
        lc_no = "";  
      
        clsJVdata[] jvdata = new clsJVdata[cnt + 1];
        

        for (i = 0; i < cnt; i++)
        {
            if (mrrdata[i] == null) break;

            dtdet = det.GetDetByRefItem(ref_no, mrrdata[i].Icode);
            if (dtdet.Rows.Count == 1)
                dr[i] = dtdet[0];
            else
                dr[i] = det.GetDataByRefLine(ref_no, (short)mrrdata[i].LineNo)[0];

            if (scfstkctl.GetDataByItemStore(mrrdata[i].Icode, dr[i].PO_Det_Str_Code).Rows.Count == 0)
                scfstkdr[i] = null;
            else
                scfstkdr[i] = scfstkctl.GetDataByItemStore(mrrdata[i].Icode, dr[i].PO_Det_Str_Code)[0];

            //set jvdata       
           
            jvdata[i] = new clsJVdata();
            f_itm_code = mrrdata[i].Icode;
            jvdata[i] = getjvdata(ref_no, mrr_ref, ord_type, mrr_type, "D", hdrdr.PO_Hdr_Dcode, mrrdata[i].Pur_from, hdrdr.PO_Hdr_Acode, mrrdata[i].Icode, lc_no);
           
            if (jvdata[i] == null) return;
           
            if ((jvdata[i].Wrk_ac_code == "") || (jvdata[i].Wrk_ac_type == ""))
            {
                
                //clsStatic.SendMail("monju@link3.net", "SCBL COMMERCIAL MODULE", null, "[COM ITEM T_C2 CODE NOT ASSIGNED]", "ITEM CODE: " + mrrdata[i].Icode);
                return;
            }
           
        }

       
        //for C
        jvdata[i] = new clsJVdata();
        jvdata[i] = getjvdata(ref_no, mrr_ref, ord_type, mrr_type, "C", hdrdr.PO_Hdr_Dcode, mrrdata[0].Pur_from, hdrdr.PO_Hdr_Acode, f_itm_code, lc_no);

        if (jvdata[i] == null) return;
        if (jvdata[i].Wrk_ac_code == "")
        {
            return;
        }

        
        SqlTransaction myTrn = HelperTA.OpenTransaction(det.Connection);
        SqlTransaction myAccTrn = HelperTA.OpenTransaction(scfhdr.Connection);
        SqlTransaction tt = HelperTA.OpenTransaction(scfwh.Connection);

        try
        {

            det.AttachTransaction(myTrn);
            matrec.AttachTransaction(myTrn);
            budg.AttachTransaction(myTrn);

            scfhdr.AttachTransaction(myAccTrn);
            scfdet.AttachTransaction(myAccTrn);
            scfcal.AttachTransaction(myAccTrn);
            scfext.AttachTransaction(myAccTrn);
            scfset.AttachTransaction(myAccTrn);
            scfstkval.AttachTransaction(myAccTrn);
            scfstkctl.AttachTransaction(myAccTrn);
            scfitmstk.AttachTransaction(myAccTrn);
            scfwh.AttachTransaction(tt);
            scfwd.AttachTransaction(tt);
            scfanal.AttachTransaction(tt);
            
            for (i = 0; i < cnt; i++)
            {
                if (mrrdata[i] == null) break;
                tot++;
                entryqty = mrrdata[i].Entryqty;

                //dr = det.GetDetByRefItem(ref_no, mrrdata[i].Icode)[0];

                linqty = Convert.ToDecimal(dr[i].PO_Det_Lin_Qty);
                orgqty = Convert.ToDecimal(dr[i].PO_Det_Org_QTY);
                insqty = Convert.ToDecimal(dr[i].PO_Det_Ins_QTY);

               
                availqty = insqty;
                if (availqty < entryqty) { updateflg = false; goto transaction_complete; }

                balanceqty = linqty - orgqty - insqty;

                //update scf data
                scfremqty = (double)(linqty - orgqty - entryqty);
               
                        //need to update bal_qty during payment
               

                //set data rec ret transaction
                matrec.InsertMatRecRet(mrr_ref, tot, "CONFIRM", DateTime.Now, oprcode, ref_no, (int)dr[i].PO_Det_Lno, hdrdr.PO_Hdr_Pcode, hdrdr.PO_Hdr_Com1, dr[i].PO_Det_Icode, dr[i].PO_Det_Itm_Desc, dr[i].PO_Det_Itm_Uom, mrrdata[i].Brand, mrrdata[i].Origin, mrrdata[i].Packing, entryqty, (decimal)dr[i].PO_Det_Lin_Qty, orgqty + entryqty, insqty - entryqty, balanceqty, dr[i].PO_Det_Lin_Rat, "", txtcomm.Text);

                //update po data
                det.UpdateQtyForInsp((double)(orgqty + entryqty), (double)(insqty - entryqty), (double)balanceqty,"", ref_no,(short) mrrdata[i].LineNo);
                
                //update scfadata
                amnt = entryqty * dr[i].PO_Det_Lin_Rat;

                totamnt = totamnt + amnt;

                scfdet.InsertTrnDet("RC", mrr_type, mrr_ref, (short)tot, "", 0, dr[i].PO_Det_Icode, dr[i].PO_Det_Itm_Desc, dr[i].PO_Det_Itm_Uom, dr[i].PO_Det_Str_Code, dr[i].PO_Det_Bin_Code, dr[i].PO_Det_Ref, 0, mrrdata[i].Packing, Convert.ToDateTime(mrrdata[0].Purdate.ToShortDateString()), Convert.ToDateTime(mrrdata[0].Purdate.ToShortDateString()), (double)entryqty, 0, dr[i].PO_Det_Lin_Rat, amnt, amnt, dr[i].PO_Det_Lin_Rat, amnt, mrrdata[i].Brand, mrrdata[i].Origin, "", 0, (double)balanceqty);

                scfcal.InsertTrnCal("RC", mrr_type, mrr_ref, (short)tot, "QTY", entryqty, null, null, null, null);
                foreach (ErpDataSet.InSu_Trn_CalRow drcal in dtcal.Rows)
                {
                    calcode = drcal.Trn_Cal_Code;
                    if (calcode == "AMT") calval = amnt; else calval = dr[i].PO_Det_Lin_Rat;
                    scfcal.InsertTrnCal("RC", mrr_type, mrr_ref, (short)tot, calcode, calval, null, null, null, null);
                }

                gqty = Convert.ToDecimal(scfstkctl.GetTotQty(dr[i].PO_Det_Icode));

                if (gqty == 0)
                {
                    gamnt = 0;
                }
                else
                {
                    gamnt = Convert.ToDecimal(scfstkctl.GetTotValue(dr[i].PO_Det_Icode));
                }
                grate = (gamnt + amnt) / (gqty + entryqty);
                stdrate = 1;

                if (scfstkdr[i] == null)
                {
                    totqty = 0;
                    totfreeqty = 0;
                    avval = 0;
                    avrate = amnt / entryqty;
                    scfstkctl.InsertStkCtl(dr[i].PO_Det_Str_Code, dr[i].PO_Det_Icode, null, (double)entryqty, (double)entryqty, 0, 0, 0, 0, 0, 0, 0, amnt, 0, 0, 0, mrrdata[0].Purdate, mrrdata[0].Purdate, "", "", "", 0);
                    
                }
                else
                {
                    totqty = scfstkdr[i].Stk_Ctl_Cur_Stk;
                    totfreeqty = scfstkdr[i].Stk_Ctl_Free_Stk;
                    avval = scfstkdr[i].Stk_Ctl_Ave_Val;
                    avrate = (scfstkdr[i].Stk_Ctl_Ave_Val + amnt) / (decimal)(scfstkdr[i].Stk_Ctl_Cur_Stk + (double)entryqty);

                    dritm = scfitmstk.GetDataByItm(dr[i].PO_Det_Icode)[0];

                    sdtval = ((decimal)dritm.Itm_Stk_Cur + entryqty) * dritm.Itm_Stk_STD_Rat;
                    latval = ((decimal)dritm.Itm_Stk_Cur + entryqty) * dr[i].PO_Det_Lin_Rat;
                    scfstkctl.UpdateForMrrRec((double)entryqty, (double)entryqty, amnt, Convert.ToDateTime(mrrdata[0].Purdate.ToShortDateString()), sdtval, latval, dr[i].PO_Det_Str_Code, dr[i].PO_Det_Icode);                    
                }

                if (scfitmstk.UpdateItmStkMrrRec((double)entryqty, dr[i].PO_Det_Lin_Rat, grate, dr[i].PO_Det_Icode) == 0)
                {
                    scfitmstk.InsertItmStk(dr[i].PO_Det_Icode, (double)entryqty, "A", 0, 0, 1, avrate, avrate, "", "", "", 0);
                }
                             
                scfstkval.InsertStkVal("RC", mrr_type, mrr_ref, Convert.ToDateTime(mrrdata[0].Purdate.ToShortDateString()), dr[i].PO_Det_Icode, dr[i].PO_Det_Itm_Desc, dr[i].PO_Det_Str_Code, dr[i].PO_Det_Lin_Rat, avrate, stdrate, (double)entryqty, "", null, null, null);
                
                //updateflg budget               

                budg.UpdateForMatReceive(amnt, entryqty, mrrdata[0].Purdate, dr[i].PO_Det_Icode, cur_session);
                
                //jv(D) for item

                jvdata[i].Wrk_narration = jvdata[i].Wrk_narration + "//" + dr[i].PO_Det_Itm_Desc + "  received " + mrrdata[i].Entryqty.ToString("N2") + " " + dr[i].PO_Det_Itm_Uom + "@ " + dr[i].PO_Det_Lin_Rat.ToString("N2");

                scfwd.InsertDetail(fate_ref, jvdata[i].Wrk_ac_code, trn_jrn_code, tot, jvdata[i].Wrk_narration, "D", amnt, jvdata[i].Wrk_match, "", Convert.ToDateTime(DateTime.Now.ToShortDateString()), "", "", "", Convert.ToDateTime(DateTime.Now.ToShortDateString()), "", jvdata[i].Wrk_ac_type, "", Convert.ToDateTime(DateTime.Now.ToShortDateString()), 0, jvdata[i].Adrcode, ref_no, mrr_ref, "", 0, 0, "", "", "", "", 0,"");
            }

            //jv(C) for item



            if (tot == 1) { jvdata[i].Wrk_narration = jvdata[i - 1].Wrk_narration; }

            if (ord_type == "SPO")
            {
                crdanal = userinfo.GetUserByCode(hdrdr.PO_Hdr_Dcode)[0].UserEmpId;
            }
            else
            {
                crdanal = jvdata[i].Adrcode;
            }

            if (ccg.GetDataByCcgCode(hdrdr.PO_Hdr_Ref).Rows.Count == 0)
            {
                ccg.InsertAnalysisSetup("T17", hdrdr.PO_Hdr_Ref, hdrdr.PO_Hdr_Ref, "", DateTime.Now, "", "", "", 0);
            }

            analgrp = ccg.GetDataByCcgCode(crdanal)[0].Ccg_Cost_Id;
            scfanal.InsertAna(fate_ref, jvdata[i].Wrk_ac_code, tot + 1, 1, crdanal, hdrdr.PO_Hdr_Ref, "", "", "", analgrp, "T17", "", "", "");

            scfwd.InsertDetail(fate_ref, jvdata[i].Wrk_ac_code, trn_jrn_code, tot + 1, jvdata[i].Wrk_narration, "C", totamnt, jvdata[i].Wrk_match, "", Convert.ToDateTime(DateTime.Now.ToShortDateString()), "", "", "", Convert.ToDateTime(DateTime.Now.ToShortDateString()), "", jvdata[i].Wrk_ac_type, "", Convert.ToDateTime(DateTime.Now.ToShortDateString()), 0, crdanal, ref_no, mrr_ref, "", 1, 0, "", "", "", "", 0, "");

            scfwh.InsertHeader(fate_ref, trn_jrn_code, "AJV", "J", period, DateTime.Now, Convert.ToDateTime(mrrdata[0].Purdate.ToShortDateString()), "BDT", 1, oprcode, oprcode, Convert.ToDateTime(DateTime.Now), "L", "", "", "");

            scfhdr.InsertTrnHdr("RC", mrr_type, mrr_ref, hdrdr.PO_Hdr_Pcode, hdrdr.PO_Hdr_Dcode, hdrdr.PO_Hdr_Acode, mrrdata[0].Purdate, "", "", "", "", "", "", "", "", "", "", totamnt, "P", period, oprcode, "", "", "", ref_no, "", "", "", "", "", 0, 0, Convert.ToDateTime(mrrdata[0].Purdate.ToShortDateString()), null, "",txtcomm.Text );
            scfext.InsertTrnExt("RC", mrr_type, mrr_ref, "", "", "", "", "", "", "", "", null, po_no, po_date, "", "", "", 0, null, mpr_no, mpr_date, mod_of_del, cert_no, cert_date);

            scfset.UpdateNextNumMrr(last_num, next_num, "RC", mrr_type);

            totc = Convert.ToDecimal(scfwd.GetTotAmountByRef("C", fate_ref));
            totd = Convert.ToDecimal(scfwd.GetTotAmountByRef("D", fate_ref));
           
           
            if ((totc == 0) || (totc != totd)) { updateflg = false; goto transaction_complete; }

            updateflg = true;
        transaction_complete:

            if (updateflg)
            {
               myTrn.Commit();
                myAccTrn.Commit();
                tt.Commit();

            //    tt.Rollback();
            //    myTrn.Rollback();
            //    myAccTrn.Rollback();
            }
            else
            {
                tt.Rollback();
                myTrn.Rollback();
                myAccTrn.Rollback();
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            tt.Rollback();
            myTrn.Rollback();
            myAccTrn.Rollback();
        }
        finally
        {
            HelperTA.CloseTransaction(scfwh.Connection, tt);
            HelperTA.CloseTransaction(det.Connection, myTrn);
            HelperTA.CloseTransaction(scfhdr.Connection, myAccTrn);
        }

        if (updateflg)
        {
            Session[clsStatic.sessionMrrDetData] = null;
            lbllogref.Text = mrr_ref;
            ModalPopupExtender5.Show();
        }

    }

    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("./frm_mat_rec_confirm.aspx");
    }
    protected void btnok_Click(object sender, EventArgs e)
    {
        Response.Redirect("./frm_mat_rec_confirm.aspx");
    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
        Response.Redirect("./frm_mat_rec_confirm.aspx?ret_rec_ref=" + lbllogref.Text);
    }
    
}
