using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Dataaccess;
//using System.Transactions;
using LibraryDAL;
using LibraryDAL.dsInventory;
using LibraryDAL.dsInventory.dsInventoryMasterTableAdapters;
using LibraryDAL.dsInventory.dsInventoryTransactionTableAdapters;
//using LibraryDAL.dsInventory.dsInventoryJVTableAdapters;
using LibraryDAL.dsFixedAsset.dsFasTableAdapters;
//using FA_COM_JD_REFNOTableAdapter = LibraryDAL.dsInventory.dsInventoryJVTableAdapters.FA_COM_JD_REFNOTableAdapter;

public partial class modules_FixedAsset_TransactionDetails_GetDataFromInv : System.Web.UI.Page
{
    string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {

        StaticData.checkUserAuthentication();

        if (!IsPostBack)
        {
           loadgriddata();
           lblerrormessage.Text = "No Data Found";
        }

    }

    private void loadgriddata()
    {
        btnconvertdata.Visible = false;
        CheckBox1.Visible = false;

        DataTable dgrt = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetConvertedDataForFixedAsset());
               
        //dsFixedAssetTableAdapters.GetOunItemTableAdapter grt = new dsFixedAssetTableAdapters.GetOunItemTableAdapter();
        //dsFixedAsset.GetOunItemDataTable dgrt = new dsFixedAsset.GetOunItemDataTable();
        //dgrt = grt.GetData(); 
        //dsFixedAssetTableAdapters.FAS_FixedAssetSetUpTableAdapter fas = new dsFixedAssetTableAdapters.FAS_FixedAssetSetUpTableAdapter();
        //dsFixedAsset.FAS_FixedAssetSetUpDataTable dfas = new dsFixedAsset.FAS_FixedAssetSetUpDataTable();

        string facode = "";
        
        GridView2.DataSource = dgrt;
        GridView2.DataBind();
        
        int curindx = 0;

        if (dgrt.Rows.Count > 0)
        {
            btnconvertdata.Visible = true;
            CheckBox1.Visible = true;
            lblerrormessage.Text = "";

            foreach (DataRow drt in dgrt.Rows)
            {
                string icode=drt["Trn_Det_Icode"].ToString();

                DataTable dfas = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetDataByItemGrpIcode(icode));


                if (dfas.Rows.Count > 0)
                {                    
                    facode = dfas.Rows[0]["Fxd_Acc_code"].ToString();
                }
                else
                {
                    facode = "";
                }

                Label refno = (Label)GridView2.Rows[curindx].FindControl("lblrefno");
                Label lblitemCode = (Label)GridView2.Rows[curindx].FindControl("lblitemCode");
                Label lblitemName = (Label)GridView2.Rows[curindx].FindControl("lblitemName");
                Label lblPartyCode = (Label)GridView2.Rows[curindx].FindControl("lblPartyCode");
                Label lblPartyName = (Label)GridView2.Rows[curindx].FindControl("lblPartyName");
                TextBox txtacCode = (TextBox)GridView2.Rows[curindx].FindControl("txtacCode");
                Label lblqty = (Label)GridView2.Rows[curindx].FindControl("lblqty");
                Label lblamountw = (Label)GridView2.Rows[curindx].FindControl("lblamountw");
                Label lblDate = (Label)GridView2.Rows[curindx].FindControl("lblDate");


                refno.Text = drt["RefNo"].ToString();
                lblitemCode.Text = drt["Trn_Det_Icode"].ToString();
                lblitemName.Text =drt["Trn_Det_Itm_Desc"].ToString();
                lblPartyCode.Text =drt["Trn_Hdr_Pcode"].ToString();
                lblPartyName.Text =drt["par_adr_name"].ToString();
                txtacCode.Text = facode;
                lblqty.Text =drt["Trn_Det_Lin_Qty"].ToString();
                lblamountw.Text =drt["Trn_Det_Lin_Amt"].ToString();
                lblDate.Text = drt["Trn_Hdr_DATE"].ToString();                
 
                curindx++;
            }
        }


    }

    private void loadgriddataFiber()
    {

        dsFixedAssetTableAdapters.GetFiberDataTableAdapter grt = new dsFixedAssetTableAdapters.GetFiberDataTableAdapter();
        dsFixedAsset.GetFiberDataDataTable dgrt = new dsFixedAsset.GetFiberDataDataTable();

        dgrt = grt.GetDataFiberITem();

        dsFixedAssetTableAdapters.FAS_FixedAssetSetUpTableAdapter fas = new dsFixedAssetTableAdapters.FAS_FixedAssetSetUpTableAdapter();
        dsFixedAsset.FAS_FixedAssetSetUpDataTable dfas = new dsFixedAsset.FAS_FixedAssetSetUpDataTable();

        string facode = "";

        GridView2.DataSource = dgrt;

        GridView2.DataBind();

        int curindx = 0;

        if (dgrt.Rows.Count > 0)
        {
            foreach (dsFixedAsset.GetFiberDataRow  drt in dgrt.Rows)
            {

                dfas = fas.GetDataByItmGrpicode(drt.Trn_Det_Icode);
                if (dfas.Rows.Count > 0)
                {
                    facode = dfas[0].Fxd_Acc_code;
                }
                else
                {
                    facode = "";
                }

                Label refno = (Label)GridView2.Rows[curindx].FindControl("lblrefno");
                Label lblitemCode = (Label)GridView2.Rows[curindx].FindControl("lblitemCode");
                Label lblitemName = (Label)GridView2.Rows[curindx].FindControl("lblitemName");
                Label lblPartyCode = (Label)GridView2.Rows[curindx].FindControl("lblPartyCode");
                Label lblPartyName = (Label)GridView2.Rows[curindx].FindControl("lblPartyName");
                TextBox txtacCode = (TextBox)GridView2.Rows[curindx].FindControl("txtacCode");
                Label lblqty = (Label)GridView2.Rows[curindx].FindControl("lblqty");
                Label lblamountw = (Label)GridView2.Rows[curindx].FindControl("lblamountw");
                Label lblDate = (Label)GridView2.Rows[curindx].FindControl("lblDate");


                refno.Text = drt.RefNo;
                lblitemCode.Text = drt.Trn_Det_Icode;
                lblitemName.Text = drt.Trn_Det_Itm_Desc;
                lblPartyCode.Text = drt.Trn_Hdr_Pcode;
                lblPartyName.Text = drt.par_adr_name;
                txtacCode.Text = facode;
                lblqty.Text = drt.Trn_Det_Lin_Qty.ToString();
                lblamountw.Text = drt.Trn_Det_Lin_Amt.ToString();
                lblDate.Text = drt.Trn_Hdr_DATE.ToString("dd/MM/yyyy");
                // lbldate

                curindx++;
            }
        }

    }
    protected void btnconvertdata_Click(object sender, EventArgs e)
    {
        #region veriable 

        lblerrormessage.Visible = false;
        DateTime dt=DateTime.Now ;
        string trachininfo = "";
        int p = 0;
        for (int ii = 0; ii < GridView2.Rows.Count; ii++)
        {
            CheckBox cb = (CheckBox)GridView2.Rows[ii].FindControl("chkupdw");
            if (cb.Checked)
            {
                p += 1;
            }
        }

        if (p == 0)
        {
            lblerrormessage.Visible = true;
            lblerrormessage.Text = "Selection ERROR";
            return;
        }

      
        dsFixedAssetTableAdapters.fxdFAReferenceNumbersTableAdapter fxup = new dsFixedAssetTableAdapters.fxdFAReferenceNumbersTableAdapter();

        FAS_InTr_Trn_HdrTableAdapter taMrrHdr = new FAS_InTr_Trn_HdrTableAdapter();
        LibraryDAL.dsFixedAsset.dsFas.FAS_InTr_Trn_HdrDataTable dmr = new LibraryDAL.dsFixedAsset.dsFas.FAS_InTr_Trn_HdrDataTable(); 

        FAS_InTr_Trn_DetTableAdapter taMrrDet = new FAS_InTr_Trn_DetTableAdapter();
        LibraryDAL.dsFixedAsset.dsFas.FAS_InTr_Trn_DetDataTable dtamrrdet = new LibraryDAL.dsFixedAsset.dsFas.FAS_InTr_Trn_DetDataTable();
      
        FAS_InMa_Itm_SerialTableAdapter taItemSerial = new FAS_InMa_Itm_SerialTableAdapter();


       dsFixedAssetTableAdapters.FAS_FixedAssetSetUpNewTableAdapter fanew = new dsFixedAssetTableAdapters.FAS_FixedAssetSetUpNewTableAdapter();


        dsFixedAssetTableAdapters.Inv_InTr_Trn_DetTableAdapter indet = new dsFixedAssetTableAdapters.Inv_InTr_Trn_DetTableAdapter();
        dsFixedAsset.Inv_InTr_Trn_DetDataTable dindet = new dsFixedAsset.Inv_InTr_Trn_DetDataTable();  

        dsFixedAssetTableAdapters.Inv_InTr_Trn_HdrTableAdapter inhd=new dsFixedAssetTableAdapters.Inv_InTr_Trn_HdrTableAdapter();
        dsFixedAsset.Inv_InTr_Trn_HdrDataTable dinhd=new dsFixedAsset.Inv_InTr_Trn_HdrDataTable();

        dsFixedAssetTableAdapters.Inv_InMa_Itm_SerialTableAdapter inser = new dsFixedAssetTableAdapters.Inv_InMa_Itm_SerialTableAdapter();
        dsFixedAsset.Inv_InMa_Itm_SerialDataTable dinser = new dsFixedAsset.Inv_InMa_Itm_SerialDataTable();

        LibraryDAL.dsInventory.dsInventoryTransactionTableAdapters.Item_Movement_dtlTableAdapter imv = new LibraryDAL.dsInventory.dsInventoryTransactionTableAdapters.Item_Movement_dtlTableAdapter();
        LibraryDAL.dsInventory.dsInventoryTransaction.Item_Movement_dtlDataTable dimv = new LibraryDAL.dsInventory.dsInventoryTransaction.Item_Movement_dtlDataTable();


        for (int im = 0; im < GridView2.Rows.Count; im++)
        {

            CheckBox cb = (CheckBox)GridView2.Rows[im].FindControl("chkupdw");
            Label refno = (Label)GridView2.Rows[im].FindControl("lblrefno");
            Label lblitemCode = (Label)GridView2.Rows[im].FindControl("lblitemCode");
            Label lblqty = (Label)GridView2.Rows[im].FindControl("lblqty");
            if (cb.Checked)
            {
                dtamrrdet = taMrrDet.GetDataByDuplicate(refno.Text.Replace("&amp;", "&").ToString(), lblitemCode.Text,
                    Convert.ToDouble(lblqty.Text));
            }

            
            if (dtamrrdet.Rows.Count > 0)
            {
                lblerrormessage.Visible = true;
                lblerrormessage.Text = "You have already convert this item";
                return;
            }

            string sid = Session[StaticData.sessionUserId].ToString();
            //if (sid != "L3T388")
            //{
            //    if (dtamrrdet.Rows.Count > 0)
            //    {
            //        lblerrormessage.Visible = true;
            //        lblerrormessage.Text = "You have already convert this item";
            //        return;
            //    }
            //}

        }

        #endregion


         string ConnectionStr = System.Configuration.ConfigurationManager.AppSettings["L3TConnectionString"].ToString();


         for (int i = 0; i < GridView2.Rows.Count; i++)
         {
             CheckBox cb = (CheckBox)GridView2.Rows[i].FindControl("chkupdw");
             CheckBox cb2 = (CheckBox)GridView2.Rows[i].FindControl("chkupdwtt");
             Label refno = (Label)GridView2.Rows[i].FindControl("lblrefno");
             Label lblitemCode = (Label)GridView2.Rows[i].FindControl("lblitemCode");
             Label lblqty = (Label)GridView2.Rows[i].FindControl("lblqty");
             TextBox txtacCode = (TextBox)GridView2.Rows[i].FindControl("txtacCode");

             SqlTransaction myTran = clsDbHelper.OpenTransaction(taMrrHdr.Connection );

             List<TransactionDetailsDAO> tdDaolst = new List<TransactionDetailsDAO>();

             try
             {

                 taMrrHdr.AttachTransaction(myTran);
                 taMrrDet.AttachTransaction(myTran);
                 taItemSerial.AttachTransaction(myTran);


                 TransactionHeaderDAO thDao = new TransactionHeaderDAO();


                 #region Header and details insert

                 if (cb.Checked && cb2.Checked)
                 {


                     dindet = indet.GetDataByTrn_Det_Ref(refno.Text.Replace("&amp;", "&").ToString(), lblitemCode.Text, Convert.ToDouble(lblqty.Text));
                     dinhd = inhd.GetDataByTrn_Hdr_Ref(refno.Text.Replace("&amp;", "&").ToString());
                     dinser = inser.GetDataByitm_det_ref(refno.Text.Replace("&amp;", "&").ToString(), lblitemCode.Text);

                     if (dindet.Rows.Count > 0)
                     {

                         taMrrDet.InsertMRRDet(dindet[0].Trn_Det_Type, dindet[0].Trn_Det_Code, dindet[0].Trn_Det_Ref, Convert.ToInt16(dindet[0].Trn_Det_Lno), dindet[0].Trn_Det_Sfx, Convert.ToInt16(dindet[0].Trn_Det_Exp_Lno),
                             dindet[0].Trn_Det_Icode, dindet[0].Trn_Det_Itm_Desc, dindet[0].Trn_Det_Itm_Uom, dindet[0].Trn_Det_Str_Code, dindet[0].Trn_Det_Bin_Code,
                             dindet[0].Trn_Det_Ord_Ref, Convert.ToInt16(dindet[0].Trn_Det_Ord_Lno), dindet[0].Trn_Det_Bat_No, Convert.ToDateTime(dindet[0].Trn_Det_Exp_Dat),
                             Convert.ToDateTime(dindet[0].Trn_Det_Book_Dat), Convert.ToDouble(dindet[0].Trn_Det_Lin_Qty), Convert.ToDouble(dindet[0].Trn_Det_Unt_Wgt),
                             Convert.ToDecimal(dindet[0].Trn_Det_Lin_Rat), Convert.ToDecimal(dindet[0].Trn_Det_Lin_Amt), Convert.ToDecimal(dindet[0].Trn_Det_Lin_Net), dindet[0].T_C1,
                             dindet[0].T_C2, dindet[0].T_Fl, Convert.ToInt32(dindet[0].T_In), Convert.ToDouble(dindet[0].Trn_Det_Bal_Qty), dindet[0].Trn_Det_Ref);
                     }


                     dmr = taMrrHdr.GetDataByTrn_Hdr_Ref(refno.Text.Replace("&amp;", "&").ToString());
                     if (dinhd.Rows.Count > 0)
                     {

                         if (dmr.Rows.Count == 0)
                         {

                             taMrrHdr.InsertMRRHdr(dinhd[0].Trn_Hdr_Type, dinhd[0].Trn_Hdr_Code, dinhd[0].Trn_Hdr_Ref, dinhd[0].Trn_Hdr_Pcode, dinhd[0].Trn_Hdr_Dcode,
                                              dinhd[0].Trn_Hdr_Acode, Convert.ToDateTime(dinhd[0].Trn_Hdr_DATE), dinhd[0].Trn_Hdr_Com1, "", "", "", "", "", "", "", "", "", Convert.ToDecimal(dinhd[0].Trn_Hdr_Value),
                                              dinhd[0].Trn_Hdr_HRPB_Flag, dinhd[0].Trn_Hdr_Ent_Prd, dinhd[0].Trn_Hdr_Opr_Code, dinhd[0].Trn_Hdr_Prd_Cld, dinhd[0].Trn_Hdr_Exp_Typ, dinhd[0].Trn_Hdr_Led_Int, dinhd[0].Trn_Hdr_DC_No,
                                              dinhd[0].Trn_Hdr_EI_Flg, dinhd[0].Trn_Hdr_Cno, dinhd[0].T_C1, dinhd[0].T_C2, dinhd[0].T_Fl, Convert.ToInt32(dinhd[0].T_In), Convert.ToDecimal(dinhd[0].Trn_Hdr_exc_duty),
                                              Convert.ToDateTime(dinhd[0].IsTrn_Hdr_Dc_DateNull() ? DateTime.Now : dinhd[0].Trn_Hdr_Dc_Date), Convert.ToDateTime(dinhd[0].IsTrn_Hdr_CI_DateNull() ? DateTime.Now : dinhd[0].Trn_Hdr_CI_Date), dinhd[0].Trn_Hdr_Pass_No);
                         }

                     }

                 #endregion

                     #region JV Region

                     #region New Account JV

                     int lineno = 1;
                     DateTime fgtN = Convert.ToDateTime(dinhd[0].Trn_Hdr_DATE);

                     Label issuenoN = (Label)GridView2.Rows[i].FindControl("lblrefno");
                     Label itemCodeN = (Label)GridView2.Rows[i].FindControl("lblitemCode");
                     Label itemNameN = (Label)GridView2.Rows[i].FindControl("lblitemName");
                     Label supplierCodeN = (Label)GridView2.Rows[i].FindControl("lblPartyCode");
                     Label supplierNameN = (Label)GridView2.Rows[i].FindControl("lblPartyName");
                     TextBox accCodeN = (TextBox)GridView2.Rows[i].FindControl("txtacCode");
                     Label quantityN = (Label)GridView2.Rows[i].FindControl("lblqty");
                     Label amountN = (Label)GridView2.Rows[i].FindControl("lblamountw");
                     Label lblDateN = (Label)GridView2.Rows[i].FindControl("lblDate");


                     // Header 

                     //TransactionHeaderDAO thDao = new TransactionHeaderDAO();
                     thDao.TrnAccPeriod = Convert.ToDateTime(fgtN).Year + "/" + String.Format("{0:00}", Convert.ToDateTime(fgtN).Month);
                     thDao.TrnCurrCode = "BDT";
                     thDao.TrnCurrRate = 1;
                     thDao.TrnDATE = Convert.ToDateTime(fgtN);
                     thDao.TrnEntryDATE = DateProcess.GetServerDate(ConnectionStr);
                     thDao.TrnEntryFlag = "L";
                     thDao.TrnEntryUser = "ADM";
                     thDao.TrnJrnType = "FJV";
                     thDao.TrnRefNo = "";
                     thDao.VoucherType = "J";
                     thDao.ModuleName = "Accounts";


                     // Details Debit...........

                     //List<TransactionDetailsDAO> tdDaolst = new List<TransactionDetailsDAO>();


                     TransactionDetailsDAO tdDao = new TransactionDetailsDAO();

                     tdDao.TrnAcCode = accCodeN.Text;
                     tdDao.TrnAcDesc = DataProcess.GetSingleValueFromtable(ConnectionStr, "budg", "Gl_Coa_Name", "where Gl_Coa_Code='" + tdDao.TrnAcCode + "'");
                     tdDao.TrnAcType = DataProcess.GetSingleValueFromtable(ConnectionStr, "budg", "Gl_Coa_Type", "where Gl_Coa_Code='" + tdDao.TrnAcCode + "'");
                     tdDao.TrnAmount = Convert.ToDouble(amountN.Text.ToString());
                     tdDao.TrnLineNo = lineno.ToString();
                     tdDao.TrnMatch = "";
                     tdDao.TrnNarration = itemNameN.Text + " " + "Issue qty : " + quantityN.Text + "Issue NO" + issuenoN.Text;
                     tdDao.TrnPaymentDATE = DateProcess.GetServerDate(ConnectionStr);
                     tdDao.TrnRefNo = "";
                     tdDao.TrnTrntype = "D";
                     tdDao.TrnDueDATE = tdDao.TrnPaymentDATE.AddDays(30);
                     tdDao.TrnChequeNo = "";
                     tdDao.TrnAdrCode = supplierCodeN.Text;
                     tdDao.TrnDcNo = itemCodeN.Text;
                     tdDao.TrnGRNNo = dinhd[0].Trn_Hdr_Ref;

                     tdDaolst.Add(tdDao);

                     // Details Credit............

                     TransactionDetailsDAO tdDao2 = new TransactionDetailsDAO();
                     lineno = lineno + 1;
                     tdDao2.TrnAcCode = "01.10.400.100.0001";
                     tdDao2.TrnAcDesc = DataProcess.GetSingleValueFromtable(ConnectionStr, "budg", "Gl_Coa_Name", "where Gl_Coa_Code='" + tdDao2.TrnAcCode + "'");
                     tdDao2.TrnAcType = DataProcess.GetSingleValueFromtable(ConnectionStr, "budg", "Gl_Coa_Type", "where Gl_Coa_Code='" + tdDao2.TrnAcCode + "'");
                     tdDao2.TrnAmount = Convert.ToDouble(amountN.Text.ToString());
                     tdDao2.TrnLineNo = lineno.ToString();
                     tdDao2.TrnMatch = "";
                     tdDao2.TrnNarration = "01.10.400.100.0001" + ", " + "Issue to Fixed Asset against " + "Issue NO " + issuenoN.Text;
                     tdDao2.TrnPaymentDATE = DateProcess.GetServerDate(ConnectionStr);
                     tdDao2.TrnRefNo = "";
                     tdDao2.TrnTrntype = "C";
                     tdDao2.TrnDueDATE = tdDao2.TrnPaymentDATE.AddDays(30);
                     tdDao2.TrnChequeNo = "";
                     tdDao2.TrnAdrCode = supplierCodeN.Text;
                     tdDao2.TrnDcNo = itemCodeN.Text;
                     tdDao2.TrnGRNNo = dinhd[0].Trn_Hdr_Ref;

                     tdDaolst.Add(tdDao2);



                     #endregion



                     #endregion


                     #region serial entry

                     int qty = Convert.ToInt32(dindet[0].Trn_Det_Lin_Qty);
                     string itemcode = dindet[0].Trn_Det_Icode;
                     int cur = 0;
                     int jj = 0;

                     #region I T E M      MO V E M E NT

                     dimv = imv.GetDataByitm_det_ref(refno.Text);

                     if (dimv.Rows.Count > 0)
                     {
                         imv.DeleteQuery(dimv[0].itm_det_ref);
                     }

                     #endregion

                     string SerialNo = "";

                     if (dinser.Rows.Count > 0)
                     {

                         #region Item movement..........

                         foreach (dsFixedAsset.Inv_InMa_Itm_SerialRow dr in dinser.Rows)
                         {
                             SerialNo += dr.itm_det_serial_no + ",";
                         }

                         #endregion

                         for (int pi = 1; pi <= Convert.ToInt32(qty); pi++)
                         {
                             if (i == 0)
                             {
                                 if (jj == 0)
                                 {
                                     trachininfo = TrachinginfoGenerator(itemcode, lblDateN.Text);
                                     jj++;
                                 }
                                 else
                                 {

                                     string bgh = trachininfo;
                                     string rightStr = trachininfo.Substring(16, 7);
                                     cur = int.Parse(rightStr);
                                     cur++;

                                     string aa = cur.ToString();
                                     string cc = aa.PadLeft(7, '0');

                                     string leftStr = trachininfo.Substring(0, 16);
                                     trachininfo = leftStr + cc;

                                 }
                             }
                             else
                             {
                                 string bgh = trachininfo;
                                 string rightStr = trachininfo.Substring(16, 7);
                                 cur = int.Parse(rightStr);
                                 cur++;

                                 string aa = cur.ToString();
                                 string cc = aa.PadLeft(7, '0');

                                 string leftStr = trachininfo.Substring(0, 16);
                                 trachininfo = leftStr + cc;
                             }

                             taItemSerial.InsertItemSerial(dinser[0].itm_det_icode, dinser[0].itm_det_serial_no, dinser[0].itm_det_ref,
                                           dinser[0].itm_det_str_code, dinser[0].itm_det_trn_type, dinser[0].itm_det_trn_code,
                                           Convert.ToDateTime(dinser[0].itm_det_date), dinser[0].itm_status, trachininfo, Convert.ToInt32(1),
                                           Convert.ToDecimal(dindet[0].Trn_Det_Lin_Rat));
                             
                             //string sql = SqlgenerateForFixedAsset.InsertItemSerial(dinser[0].itm_det_icode, dinser[0].itm_det_serial_no, dinser[0].itm_det_ref,
                             //              dinser[0].itm_det_str_code, dinser[0].itm_det_trn_type, dinser[0].itm_det_trn_code,
                             //              Convert.ToDateTime(dinser[0].itm_det_date), dinser[0].itm_status, trachininfo, Convert.ToInt32(1),
                             //              Convert.ToDouble(dindet[0].Trn_Det_Lin_Rat));

                             //DataProcess.ExecuteQuery(ConnectionStr, sql);




                             fanew.InsertQuery(itemCodeN.Text, itemNameN.Text, "02.10.100.100.0012", "01.10.100.017.0004", "40.10.140.100.1000", "25.01.002.001",
                                 "25.01.001.001", "", "", "", "Technical Equipment", "Fixed Assets", trachininfo, 2, Convert.ToDecimal(18));
                         }


                         #region ITEM MOVEMENT....................

                         double maxmovref = Convert.ToDouble(imv.GetMaxMovRefNo());
                         string maxmovrefno = "MOV-" + System.DateTime.Now.ToString("yyMMdd") + "-" + string.Format("{0:0000000}", maxmovref);

                         string[] serialnumber = getserialnumber(SerialNo.Trim());
                         int qt = 0;
                         for (int sn = 0; sn < serialnumber.Length; sn++)
                         {
                             if (SerialNo == "")
                             {
                                 qt = Convert.ToInt32(qty);
                             }
                             else
                             {
                                 qt = 1;
                             }

                             imv.InsertQuery(Convert.ToInt32(maxmovref), maxmovrefno, dinser[0].itm_det_icode, serialnumber[sn], dinser[0].itm_det_ref,
                                 dinser[0].itm_det_str_code, dinser[0].itm_det_trn_type,
                                 dinser[0].itm_det_trn_code, Convert.ToDateTime(dinser[0].itm_det_date), "Good", trachininfo, Convert.ToInt32(qt), 0,
                                 supplierCodeN.Text, DateTime.Now, DateTime.Now, Session[StaticData.sessionUserId].ToString());
                         }


                         #endregion

                     }

                     else
                     {
                         if (i == 0)
                         {
                             trachininfo = TrachinginfoGenerator(itemcode, lblDateN.Text);
                         }
                         else
                         {
                             string bgh = trachininfo;
                             string rightStr = trachininfo.Substring(16, 7);
                             cur = int.Parse(rightStr);
                             cur++;

                             string aa = cur.ToString();
                             string cc = aa.PadLeft(7, '0');

                             string leftStr = trachininfo.Substring(0, 16);
                             trachininfo = leftStr + cc;
                         }

                         taItemSerial.InsertItemSerial(dindet[0].Trn_Det_Icode, "", dindet[0].Trn_Det_Ref,
                                          "BGEN", "IT", "STR", Convert.ToDateTime(Convert.ToDateTime(dinhd[0].Trn_Hdr_DATE)), "Good", trachininfo, Convert.ToInt32(qty),
                                          Convert.ToDecimal(dindet[0].Trn_Det_Lin_Rat));

                         //.............................

                         //     dmap = famp.GetDataByFixedAssetACCode(accCode.Text);

                         //fanew.InsertQuery(itemCode.Text, itemName.Text, dfcod[0].Itm_Det_Acc_code, accCode.Text, dmap[0].DepreciationAC, dmap[0].ProvisionDepreAC,
                         //              dmap[0].AccumulatedDepreAC, dmap[0].DisposalAC, dmap[0].RevaluationAC, dmap[0].CashAConDisposal, dmap[0].ItemSecondGroup,
                         //              "Fixed Assets", trachininfo, Convert.ToInt32(dspt[0].DepreciationMethodID), Convert.ToDecimal(dspt[0].DepreciationRate)); 


                         fanew.InsertQuery(itemCodeN.Text, itemNameN.Text, "02.10.100.100.0020", "01.10.100.017.0011", "40.10.140.100.1000", "25.01.002.001",
                             "25.01.001.001", "", "", "", "Technical Equipment", "Fixed Assets", trachininfo, 2, Convert.ToDecimal(18));

                         #region Item Movement

                         double maxmovref = Convert.ToDouble(imv.GetMaxMovRefNo());
                         string maxmovrefno = "MOV-" + System.DateTime.Now.ToString("yyMM") + "-" + string.Format("{0:0000000}", maxmovref);

                         imv.InsertQuery(Convert.ToInt32(maxmovref), maxmovrefno, dindet[0].Trn_Det_Icode, "", dindet[0].Trn_Det_Ref,
                             "BGEN", "IT", "STR", Convert.ToDateTime(Convert.ToDateTime(dinhd[0].Trn_Hdr_DATE)), "Good", trachininfo, Convert.ToInt32(qty), 0,
                             supplierCodeN.Text, DateTime.Now, DateTime.Now, Session[StaticData.sessionUserId].ToString());

                         #endregion
                     }

                     #endregion

                     #region  fxdFAReferenceNumbers
                     L3TConnection common = new L3TConnection();
                     SqlConnection con;
                     con = common.init();

                     string updt = @"update fxdFAReferenceNumbers set isFAUpdated='1' where RefNo='" + dindet[0].Trn_Det_Ref + "' and itemQty='" + lblqty.Text + "' and ItemCode='" + itemCodeN.Text + "'";
                     SqlCommand cmd = new SqlCommand(updt, con);
                     cmd.ExecuteNonQuery();
                     con.Close();

                 }

                     #endregion


                 bool updateFlg = false;

                 TransactionEntryBLL tBll = new TransactionEntryBLL();
                 string refNo = tBll.saveData(ConnectionStr, thDao, tdDaolst, updateFlg);

                 if (refNo != "")
                 {
                     //  myJvTran.Commit();
                     myTran.Commit();
                 }
                 else
                 {
                     //   myJvTran.Rollback();
                     myTran.Rollback();
                     return;
                 }
             }

             catch (Exception ex)
             {
                 //   myJvTran.Rollback();
                 myTran.Rollback();

                 //MessageBox.Show("ERROR :" + ex.Message);
                 return;
             }


         }

         if (RadioButtonList1.SelectedIndex == 0)
         {
             loadgriddata();
         }

         if (RadioButtonList1.SelectedIndex == 1)
         {
             loadgriddataFiber();
         }


    }


    private string[] getserialnumber(string serial)
    {

        string[] tmp = serial.Split(',');
        int ln = tmp.Length;
        string[] sl = new string[ln];
        for (int i = 0; i < ln; i++)
        {
            sl[i] = tmp[i].ToString();
        }

        return sl;


    }


    private string TrachinginfoGenerator(string techi,string fsdate)
    {
        DateTime dt = Convert.ToDateTime(fsdate);  
        string geno = "";
        dsFixedAssetTableAdapters.FAS_FixedAssetSetUpTableAdapter fas = new dsFixedAssetTableAdapters.FAS_FixedAssetSetUpTableAdapter();
        dsFixedAsset.FAS_FixedAssetSetUpDataTable dfas = new dsFixedAsset.FAS_FixedAssetSetUpDataTable();



        dfas = fas.GetDataByItmGrpicode(techi);



        var dtRefNo = fas.ABC(Convert.ToDecimal(dt.ToString("yyyy")));
        var nextRefNo = (dtRefNo == null || Convert.ToInt32(dtRefNo) == 0) ? 1 : Convert.ToInt32(dtRefNo);
        var gt = nextRefNo;



        string fgt = gt.ToString().PadLeft(7, '0');

        if (dfas.Rows.Count > 0)
        {
            string gp = dfas[0].Fxd_Second_Grp;
            string fr = gp.Substring(0, 3);


            string[] serial = techi.Split('.');

            string fi = serial[1].Trim();
            string rightStr = fi.Substring(1, 2);
            string fi2 = serial[2].Trim();

            string rightStr2 = fi2.Substring(1, 2);

            geno = fr.ToUpper() + "-" + rightStr + "-" + rightStr + "-" + dt.ToString("MM") + dt.ToString("yy") + "-" + "A" + fgt;

        }


        return geno;
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string ss = e.Row.Cells[10].Text.Replace("12:00:00 AM", "");
        e.Row.Cells[10].Text = ss.ToString();   
  
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {

            for (int ix = 0; ix < GridView2.Rows.Count; ix++)
            {
                CheckBox cb = (CheckBox)GridView2.Rows[ix].FindControl("chkupdw");
                cb.Checked = true;
            }
        }

        else
        {
            for (int ix = 0; ix < GridView2.Rows.Count; ix++)
            {
                CheckBox cb = (CheckBox)GridView2.Rows[ix].FindControl("chkupdw");
                cb.Checked = false;
            }
        }
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnconvertdata.Visible = true;
        CheckBox1.Visible = true;
        CheckBox1.Checked = false; 
        if (RadioButtonList1.SelectedIndex == 0)
        {
            loadgriddata();
        }

        if (RadioButtonList1.SelectedIndex == 1)
        {
            loadgriddataFiber();
        }

    }
}
