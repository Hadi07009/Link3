using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADODB;
using AjaxControlToolkit;
using LibraryDAL;

public partial class modules_FixedAsset_TransactionDetails_frmStockTransfer : System.Web.UI.Page
{
    string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtTransferDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            btnEditSerial.Text = "Edit";
            tblTransDet.Rows[4].Visible = false;
            tblTransDet.Rows[5].Visible = false;
            LoadInitGrid();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtFromStore.Text.Trim() == "")
        {
            lblMsgHdr.Text = "Enter From Store first.";
            tblPopUp.Rows[2].Cells[0].InnerText = "";
            txtTransferRef.Text = "";
            txtTransferRef.Visible = false;
            ModalPopupExtender3.Show();
            return;
        }

        if (txtFromStore.Text.Trim() == "")
        {
            lblMsgHdr.Text = "Enter To Store first.";
            tblPopUp.Rows[2].Cells[0].InnerText = "";
            txtTransferRef.Text = "";
            txtTransferRef.Visible = false;
            ModalPopupExtender3.Show();
            return;
        }

        if (txtSerial.Visible)
        {
            if (txtSerial.Text.Length < 1)
            {
                lblMsgHdr.Text = "Enter To Serial No first.";
                tblPopUp.Rows[2].Cells[0].InnerText = "";
                txtTransferRef.Text = "";
                txtTransferRef.Visible = false;
                ModalPopupExtender3.Show();
                return;
            }
            else
            {
                var serial = txtSerial.Text.Split(',');
                if (Convert.ToDouble(txtQuantity.Text.Trim()) != serial.Length)
                {
                    var iCount = serial.Length;
                    lblMsgHdr.Text = "Transfer Qty does not match with Serial No.";
                    tblPopUp.Rows[2].Cells[0].InnerText = "";
                    txtTransferRef.Text = "";
                    txtTransferRef.Visible = false;
                    ModalPopupExtender3.Show();
                    return;
                }
            }
        }

        var itemCode = "";
        var itemName = "";
        if (txtItem.Text.ToUpper() != "")
        {
            string[] temp = txtItem.Text.Split(':');
            if (temp.Length >= 1)
            {
                itemCode = temp[0];
                itemName = temp[1];
            }
        }

        var fromStore = "";
        if (txtFromStore.Text.ToUpper() != "")
        {
            string[] temp = txtFromStore.Text.Split(':');
            if (temp.Length >= 1) fromStore = temp[0];
        }

        decimal rate = 0, rateQty = 0, amount = 0;
        string rateid = "";
        string mrrno = "";
        var rateLineNo = 0;
        var itmRemQty = Convert.ToDecimal(txtQuantity.Text.Trim());
        var j = 0;

        try
        {
            #region FIFO_SERIAL_QTY
            var itmSerial = txtSerial.Text.Trim();
            var serialQry = "";
            if (txtSerial.Visible)
            {
                clsDbCon dbCon = new clsDbCon();
                Connection DC = new Connection();
                Recordset rsRateID = new Recordset();
                Recordset rsRateSerial = new Recordset();
                Recordset rsRateMRR = new Recordset();

                string qryStr, constr;

                if (txtSerial.Text.Trim() != "")
                {
                    var allSerial = txtSerial.Text.Trim().Split(',');
                    foreach (var s in allSerial)
                    {
                        serialQry = serialQry + "'" + s + "',";
                    }
                    serialQry = serialQry.Substring(0, serialQry.Length - 1);
                }
                else
                {
                    serialQry = "''";
                }

                constr = _connectionString;
                DC.Open(constr, null, null, 0);

                qryStr = "SELECT  DISTINCT itm_det_icode,itm_rate_id FROM InMa_Itm_Serial WHERE itm_det_icode ='" + itemCode + "' " +
                             " AND itm_det_serial_no IN (" + serialQry + ") ORDER BY InMa_Itm_Serial.itm_rate_id";

                rsRateID.Open(qryStr, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);

                while (!rsRateID.EOF)
                {
                    qryStr = "SELECT distinct  InMa_Itm_Serial.itm_det_ref FROM InMa_Itm_Serial INNER JOIN SerialWiseRate " +
                            "ON InMa_Itm_Serial.sl_no=SerialWiseRate.SerialRate INNER JOIN InMa_Itm_Rate " +
                            "ON InMa_Itm_Serial.itm_det_icode = InMa_Itm_Rate.itm_rate_icode and " +
                            "InMa_Itm_Serial.itm_det_str_code = InMa_Itm_Rate.itm_rate_scode and InMa_Itm_Serial.itm_rate_id = InMa_Itm_Rate.itm_rate_id " +
                            "and InMa_Itm_Serial.itm_det_ref=InMa_Itm_Rate.itm_rate_trn_ref WHERE InMa_Itm_Serial.itm_det_icode ='" + itemCode + "' " +
                            "AND InMa_Itm_Serial.itm_det_serial_no IN (" + serialQry + ") " +
                            "AND InMa_Itm_Serial.itm_rate_id='" + rsRateID.Fields[1].Value.ToString() + "' ORDER BY InMa_Itm_Serial.itm_det_ref";

                    rsRateMRR.Open(qryStr, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    while (!rsRateMRR.EOF)
                    {
                        qryStr = "SELECT InMa_Itm_Serial.itm_det_icode, InMa_Itm_Serial.itm_det_serial_no, InMa_Itm_Serial.itm_det_ref, InMa_Itm_Serial.itm_det_str_code, " +
                           "InMa_Itm_Serial.itm_det_trn_type, InMa_Itm_Serial.itm_det_trn_code, InMa_Itm_Serial.itm_det_date, InMa_Itm_Serial.itm_status, " +
                           "InMa_Itm_Serial.itm_rate_id, InMa_Itm_Rate.itm_rate_lineno, InMa_Itm_Serial.sl_no, InMa_Itm_Rate.itm_rate_rate " +
                           "FROM InMa_Itm_Serial INNER JOIN SerialWiseRate on InMa_Itm_Serial.sl_no=SerialWiseRate.SerialRate INNER JOIN InMa_Itm_Rate " +
                           "ON InMa_Itm_Serial.itm_det_icode = InMa_Itm_Rate.itm_rate_icode and InMa_Itm_Serial.itm_det_str_code = InMa_Itm_Rate.itm_rate_scode " +
                           "and InMa_Itm_Serial.itm_rate_id = InMa_Itm_Rate.itm_rate_id and InMa_Itm_Serial.itm_det_ref=InMa_Itm_Rate.itm_rate_trn_ref " +
                           "WHERE InMa_Itm_Serial.itm_det_icode ='" + itemCode + "' AND InMa_Itm_Serial.itm_det_serial_no IN (" + serialQry + ") " +
                           "AND InMa_Itm_Serial.itm_rate_id='" + rsRateID.Fields[1].Value.ToString() + "' " +
                           "and InMa_Itm_Serial.itm_det_ref='" + rsRateMRR.Fields[0].Value.ToString() + "'  ORDER BY InMa_Itm_Serial.itm_rate_id";

                        rsRateSerial.Open(qryStr, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);

                        itmSerial = "";
                        while (!rsRateSerial.EOF)
                        {
                            if (itmSerial != "")
                                itmSerial = itmSerial + "," + rsRateSerial.Fields["itm_det_serial_no"].Value.ToString();
                            else
                                itmSerial = rsRateSerial.Fields["itm_det_serial_no"].Value.ToString();

                            rate = Convert.ToDecimal(rsRateSerial.Fields["itm_rate_rate"].Value);
                            rateid = rsRateSerial.Fields["itm_rate_id"].Value.ToString();
                            rateLineNo = Convert.ToInt32(rsRateSerial.Fields["itm_rate_lineno"].Value);
                            mrrno = rsRateSerial.Fields["itm_det_ref"].Value.ToString(); ;

                            j++;
                            rsRateSerial.MoveNext();
                        }
                        rateQty = j;
                        amount = rateQty * rate;
                        j = 0;

                        var dt = new DataTable();
                        dt = (DataTable)ViewState["datatable"];
                        dt.Rows.Add(itemCode, itemName, txtUom.Text.Trim(), rateQty, rate, amount, itmSerial, rateid, rateLineNo, mrrno);
                        ViewState["datatable"] = dt;
                        SetGridData();

                        rsRateMRR.MoveNext();
                        rsRateSerial.Close();
                    }
                    rsRateMRR.Close();
                    rsRateID.MoveNext();
                }
                rsRateID.Close();
                DC.Close();
            }
            else
            {
                DataTable dtFifoItmQty = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetFIFOItem(itemCode, fromStore));
                foreach (DataRow rowNo in dtFifoItmQty.Rows)
                {
                    if (itmRemQty > 0)
                    {
                        if (Convert.ToDecimal( rowNo["itm_rate_qty"].ToString()) >= itmRemQty)
                        {
                            rateQty = itmRemQty;
                            rate = rowNo["itm_rate_rate"].ToString() == null ? 0 : Convert.ToDecimal( rowNo["itm_rate_rate"].ToString());
                            amount = rate * rateQty;
                            rateid = rowNo["itm_rate_id"].ToString() == null ? "" : rowNo["itm_rate_id"].ToString();
                            rateLineNo = rowNo["itm_rate_lineno"].ToString() == null ? 0 : Convert.ToInt32(rowNo["itm_rate_lineno"].ToString());
                            mrrno = rowNo["itm_rate_trn_ref"].ToString() == null ? "" : rowNo["itm_rate_trn_ref"].ToString();
                            itmRemQty = 0;
                        }
                        else
                        {
                            rateQty = (rowNo["itm_rate_qty"].ToString() == null ? 0 : Convert.ToDecimal(rowNo["itm_rate_qty"].ToString()));
                            rate = rowNo["itm_rate_rate"].ToString() == null ? 0 : Convert.ToDecimal(rowNo["itm_rate_rate"].ToString());
                            amount = rate * rateQty;
                            rateid = rowNo["itm_rate_id"].ToString() == null ? "" : rowNo["itm_rate_id"].ToString();
                            rateLineNo = rowNo["itm_rate_lineno"].ToString() == null ? 0 : Convert.ToInt32( rowNo["itm_rate_lineno"].ToString());
                            mrrno = rowNo["itm_rate_trn_ref"].ToString() == null ? "" : rowNo["itm_rate_trn_ref"].ToString();
                            itmRemQty = itmRemQty - (rowNo["itm_rate_qty"].ToString() == null ? 0 : Convert.ToDecimal( rowNo["itm_rate_qty"].ToString()));
                        }

                        var dt = new DataTable();
                        dt = (DataTable)ViewState["datatable"];
                        dt.Rows.Add(itemCode, itemName, txtUom.Text.Trim(), rateQty, rate, amount, itmSerial, rateid, rateLineNo, mrrno);
                        ViewState["datatable"] = dt;
                        SetGridData();
                    }
                    else
                    {
                        break;
                    }
                }
            }
            #endregion

            ClearFieldData("A");
        }
        catch (Exception)
        {

            //throw;
        }
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        SaveData("P");
    }

    private void LoadInitGrid()
    {
        var dt = new DataTable();
        dt.Rows.Clear();
        dt.Columns.Add("Item Code", typeof(string));
        dt.Columns.Add("Item Name", typeof(string));
        dt.Columns.Add("UOM", typeof(string));
        dt.Columns.Add("Quantity", typeof(string));
        dt.Columns.Add("Rate", typeof(string));
        dt.Columns.Add("Amount", typeof(string));
        dt.Columns.Add("Serial", typeof(string));
        dt.Columns.Add("Rate ID", typeof(string));
        dt.Columns.Add("Rate Line No", typeof(string));
        dt.Columns.Add("MRR No", typeof(string));
        ViewState["datatable"] = dt;
    }

    private void SetGridData()
    {
        var dt = new DataTable();
        dt = (DataTable)ViewState["datatable"];
        gvStkTransfer.DataSource = dt;
        gvStkTransfer.DataBind();

        if (gvStkTransfer.Rows.Count > 0)
        {
            btnHold.Visible = true;
            btnPost.Visible = true;
        }
        else
        {
            btnHold.Visible = false;
            btnPost.Visible = false;
        }
    }

    protected void gvStkTransfer_SelectedIndexChanged(object sender, EventArgs e)
    {
        var indx = gvStkTransfer.SelectedIndex;
        if (indx != -1)
        {
            var dt = new DataTable();
            dt = (DataTable)ViewState["datatable"];
            dt.Rows.RemoveAt(indx);
            ViewState["datatable"] = dt;
            SetGridData();
            gvStkTransfer.SelectedIndex = -1;
        }        
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;
        
        this.CollapsiblePanelExtenderHdr.Collapsed = true;
        this.CollapsiblePanelExtenderHdr.ClientState = "true";        
        
        this.CollapsiblePanelExtenderDet.Collapsed = false;
        this.CollapsiblePanelExtenderDet.ClientState = "false";
    }
    
    
    protected void txtItem_TextChanged(object sender, EventArgs e)
    {
        try
        {
            var itemCode = "";
            if (txtItem.Text.ToUpper() != "")
            {
                string[] temp = txtItem.Text.Split(':');
                if (temp.Length >= 1) itemCode = temp[0];
            }

            if (itemCode != "")
            {
                var dtItem = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetItemByCode(itemCode));
                txtUom.Text = dtItem.Rows[0]["Itm_Det_stk_unit"].ToString();
            }

            var fromStore = "";
            if (txtFromStore.Text.ToUpper() != "")
            {
                string[] temp = txtFromStore.Text.Split(':');
                if (temp.Length >= 1) fromStore = temp[0];
            }

            var dtItmStkCtl = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetStkCtlByItemStore(itemCode, fromStore));
            txtCurrentStock.Text = dtItmStkCtl.Rows.Count > 0 ? dtItmStkCtl.Rows[0]["Stk_Ctl_Cur_Stk"].ToString() : "0";
            DataTable dtChkserial = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetItemByCode(itemCode));
            if (dtChkserial.Rows.Count > 0)
            {
                if (dtChkserial.Rows[0]["Itm_Det_Others1_flag"].ToString() == "Y")
                {
                    txtSerialNo.Visible = true;
                    btnAddSerial.Visible = true;
                    txtSerial.Text = "";
                    txtSerial.Visible = true;
                    btnEditSerial.Visible = true;
                    tblTransDet.Rows[4].Visible = true;
                    tblTransDet.Rows[5].Visible = true;
                    AutoCompleteExtenderSerial.ContextKey = itemCode + ":" + fromStore + ":" + txtSerial.Text.Trim();
                }
                else
                {
                    DataTable dtItmSerial = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetSerialByItem(itemCode));
                    if (dtItmSerial.Rows.Count > 0)
                    {
                        txtSerialNo.Visible = true;
                        btnAddSerial.Visible = true;
                        txtSerial.Text = "";
                        txtSerial.Visible = true;
                        btnEditSerial.Visible = true;
                        tblTransDet.Rows[4].Visible = true;
                        tblTransDet.Rows[5].Visible = true;
                        AutoCompleteExtenderSerial.ContextKey = itemCode + ":" + fromStore + ":" + txtSerial.Text.Trim();
                    }
                    else
                    {
                        txtSerialNo.Visible = false;
                        btnAddSerial.Visible = false;
                        txtSerial.Text = "";
                        txtSerial.Visible = false;
                        btnEditSerial.Visible = false;
                        tblTransDet.Rows[4].Visible = false;
                        tblTransDet.Rows[5].Visible = false;
                        AutoCompleteExtenderSerial.ContextKey = itemCode + ":" + fromStore + ":" + txtSerial.Text.Trim();
                    }
                }
            }
            return;
        }
        catch (Exception)
        {

            //throw;
        }
    }

    private void ClearFieldData(string Pst_Flg)
    {
        txtTransSearch.Text = "";
        btnSearch.Enabled = true;
        if (Pst_Flg != "A")
        {
            txtTransSearch.Text = "";
            txtTransSearch.Enabled = true;
            txtFromStore.Text = "";
            txtToStore.Text = "";
            txtRemarks.Text = "";
            txtSerialNo.Visible = false;
            btnAddSerial.Visible = false;
            txtSerial.Text = "";
            txtSerial.Visible = false;
            btnEditSerial.Visible = false;
            tblTransDet.Rows[4].Visible = false;
            tblTransDet.Rows[5].Visible = false;
            AutoCompleteExtenderSerial.ContextKey = "" + ":" + "" + ":" + "";
            LoadInitGrid();
            SetGridData();
        }

        txtItem.Text = "";
        txtUom.Text = "";
        txtCurrentStock.Text = "";
        txtQuantity.Text = "";
        txtSerialNo.Text = "";
        txtSerial.Text = "";
        btnEditSerial.Text = "Edit";
        lblEditFlag.Text = "N";
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            var TransRef = "";
            if (txtTransSearch.Text.ToUpper() != "")
            {
                string[] temp = txtTransSearch.Text.Split(':');
                if (temp.Length >= 1) TransRef = temp[0];
            }
            var dtTransHdr = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetTransHdrByRefNo(TransRef));
            if (dtTransHdr.Rows.Count > 0)
            {
                var dtFrmStrore = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetStoreByCode(dtTransHdr.Rows[0]["Trn_Hdr_Pcode"].ToString()));
                var fromStore = dtTransHdr.Rows[0]["Trn_Hdr_Pcode"].ToString() + ":" + dtFrmStrore.Rows[0]["Str_Loc_Name"].ToString();
                txtFromStore.Text = fromStore;

                var dtToStrore = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetStoreByCode(dtTransHdr.Rows[0]["Trn_Hdr_Dcode"].ToString()));
                var toStore = dtTransHdr.Rows[0]["Trn_Hdr_Dcode"].ToString() + ":" + dtToStrore.Rows[0]["Str_Loc_Name"].ToString();
                txtFromStore.Text = toStore;
                txtTransferDate.Text = dtTransHdr.Rows[0]["Trn_Hdr_DATE"].ToString();
                txtRemarks.Text = dtTransHdr.Rows[0]["Trn_Hdr_Com1"].ToString();

                var dt = new DataTable();
                LoadInitGrid();
                dt = (DataTable)ViewState["datatable"];

                var dtTransDet = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetTransDetByRefNo(TransRef));
                foreach (DataRow rowNo in dtTransDet.Rows)
                {
                    dt.Rows.Add(rowNo["Trn_Det_Icode"].ToString(), rowNo["Trn_Det_Itm_Desc"].ToString(),
                                rowNo["Trn_Det_Itm_Uom"].ToString(), Convert.ToDecimal(rowNo["Trn_Det_Lin_Qty"].ToString()),
                                rowNo["Trn_Det_Lin_Rat"].ToString(), rowNo["Trn_Det_Lin_Amt"].ToString(), rowNo["Trn_Det_Bat_No"].ToString(), rowNo["T_Fl"].ToString(),
                                rowNo["T_In"].ToString(), rowNo["T_C2"].ToString());
                    ViewState["datatable"] = dt;
                    SetGridData();
                }

                txtTransSearch.Enabled = false;
                btnSearch.Enabled = false;
                btnClear.Visible = true;

                if (dtTransHdr.Rows[0]["Trn_Hdr_HRPB_Flag"].ToString() == "P")
                {
                    btnHold.Visible = false;
                    btnPost.Visible = false;
                    pHdrBody.Enabled = false;
                    pDetBody.Enabled = false;
                    gvStkTransfer.Columns[0].Visible = false;
                    gvStkTransfer.Enabled = false;
                }
                else
                {
                    btnHold.Visible = true;
                    btnPost.Visible = true;
                    pHdrBody.Enabled = true;
                    pDetBody.Enabled = true;
                    gvStkTransfer.Columns[0].Visible = true;
                    gvStkTransfer.Enabled = true;
                }

                this.CollapsiblePanelExtenderHdr.Collapsed = false;
                this.CollapsiblePanelExtenderHdr.ClientState = "false";
                this.CollapsiblePanelExtenderDet.Collapsed = true;
                this.CollapsiblePanelExtenderDet.ClientState = "true";
                lblEditFlag.Text = "Y";
            }
        }
        catch (Exception)
        {

            //throw;
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearFieldData("P");
        pHdrBody.Enabled = true;
        pDetBody.Enabled = true;
        LoadInitGrid();
        SetGridData();
    }

    private bool SaveData(string HPFlag)
    {
        if (!Page.IsValid) return false;

        string prefix = "", yr = "", mn = "";
        var TranRefNo = "";
        bool new_period = false;
        DateTime chkPeriod = DateTime.Now;
        var userId = Session[StaticData.sessionUserId].ToString();
        string trnPeriod = Convert.ToDateTime(txtTransferDate.Text.Trim()).ToString("MM") + "/" + Convert.ToDateTime(txtTransferDate.Text.Trim()).ToString("yyyy");
        SqlConnection conn = new SqlConnection(_connectionString);
        conn.Open();
        SqlTransaction tran = conn.BeginTransaction();
        try
        {
            var dt = new DataTable();
            dt = (DataTable)ViewState["datatable"];

            #region GetStoreCode
            var fromStore = "";
            if (txtFromStore.Text.ToUpper() != "")
            {
                try
                {
                    String[] temp = txtFromStore.Text.Split(':');
                    if (temp.Length < 2) return false;
                    fromStore = temp[0];
                }
                catch (Exception)
                {

                    //throw;
                }
            }

            var toStore = "";
            if (txtToStore.Text.ToUpper() != "")
            {
                try
                {
                    String[] temp = txtToStore.Text.Split(':');
                    if (temp.Length < 2) return false;
                    toStore = temp[0];
                }
                catch (Exception)
                {

                    //throw;
                }
            }
            #endregion

            var dtGetTranPerm = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetTranByTypeCode("ST", "STR", "ADM"));                
            if (dtGetTranPerm.Rows.Count > 0)
            {
                if (lblEditFlag.Text == "N")
                {
                    #region GetNewTransRefNo
                    mn = Convert.ToDateTime(txtTransferDate.Text.Trim()).ToString("MM");
                    yr = Convert.ToDateTime(txtTransferDate.Text.Trim()).ToString("yy");

                    var Spr = dtGetTranPerm.Rows[0]["Trn_Set_Tr_Pfix"].ToString();

                    prefix = Spr + yr + mn;

                    chkPeriod = Convert.ToInt32(mn) < 7 ? Convert.ToDateTime("01/07/" + (Convert.ToInt32(yr) - 1)) : Convert.ToDateTime("01/07/" + yr);

                    var dtRefNo = DataProcess.GetSingleValueFromtable(_connectionString, SqlgenerateForFixedAsset.GetMaxTransRef(Convert.ToDateTime(chkPeriod)));
                    var nextRefNo = (dtRefNo == null || Convert.ToInt32(dtRefNo) == 0) ? 1 : Convert.ToInt32(dtRefNo) + 1;
                    TranRefNo = prefix + nextRefNo.ToString("000000");
                    #endregion

                    #region InsertTransHdr
                    string tempValue = null;

                    DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertMRRHdr("IT", "STR", TranRefNo, fromStore, toStore, "",
                                            Convert.ToDateTime(txtTransferDate.Text.Trim()), txtRemarks.Text.Trim(), "",
                                            "", "", "", "", "", "", "", "", 0, HPFlag, trnPeriod, "SUB", "", "", "",
                                            "", "", "", "", "", "", 0, 0, tempValue, tempValue, ""));

                    DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertMRRHdr("RT", "STR", TranRefNo, toStore, fromStore, "",
                                            Convert.ToDateTime(txtTransferDate.Text.Trim()), txtRemarks.Text.Trim(), "",
                                            "", "", "", "", "", "", "", "", 0, HPFlag, trnPeriod, "SUB", "", "", "",
                                            "", "", "", "", "", "", 0, 0, tempValue, tempValue, ""));
                    #endregion
                }
                else
                {
                    #region GetSrchTransRefNo

                    if (txtTransSearch.Text.ToUpper() != "")
                    {
                        try
                        {
                            String[] temp = txtTransSearch.Text.Split(':');
                            if (temp.Length < 2) return false;
                            TranRefNo = temp[0];
                        }
                        catch (Exception)
                        {
                            //throw;
                        }
                    }

                    #endregion

                    #region EditTransHdr

                    DataProcess.UpdateQuery(_connectionString, SqlgenerateForFixedAsset.EditMRRHdr(fromStore, toStore, "",
                                          Convert.ToDateTime(txtTransferDate.Text.Trim()), txtRemarks.Text.Trim(), "",
                                          "", "", "", "", "", "", "", "", 0, HPFlag, yr + "/" + mn, "SUB", "", "", "",
                                          "", "", "", "", "", "", 0, 0, null, null, "", "IT", "STR", TranRefNo));
                    DataProcess.UpdateQuery(_connectionString, SqlgenerateForFixedAsset.EditMRRHdr(toStore, fromStore, "",
                                          Convert.ToDateTime(txtTransferDate.Text.Trim()), txtRemarks.Text.Trim(), "",
                                          "", "", "", "", "", "", "", "", 0, HPFlag, yr + "/" + mn, "SUB", "", "", "",
                                          "", "", "", "", "", "", 0, 0, null, null, "", "RT", "STR", TranRefNo));
                    #endregion

                    #region DeleteTransDet

                    DataProcess.DeleteQuery(_connectionString, SqlgenerateForFixedAsset.DeleteMRRDet("IT", "STR", TranRefNo));
                    DataProcess.DeleteQuery(_connectionString, SqlgenerateForFixedAsset.DeleteMRRDet("RT", "STR", TranRefNo));
                    #endregion

                    #region DeleteSerial
                    DataProcess.DeleteQuery(_connectionString, SqlgenerateForFixedAsset.DeleteMrrSerial(TranRefNo));                    
                    #endregion
                }

                Int16 i = 1;
                foreach (DataRow row in dt.Rows)
                {
                    decimal avgVal = 0;
                    decimal stdVal = 0;
                    decimal latVal = 0;
                    decimal latRat = 0;
                    decimal avgRat = 0;
                    decimal stdRat = 0;
                    double curStk = 0;
                    double newCurStk = 0;

                    var itemCode = row["Item Code"].ToString();
                    var itemName = row["Item Name"].ToString();
                    var uom = row["UOM"].ToString(); ;
                    var quantity = Convert.ToDecimal(row["Quantity"].ToString());
                    var rate = Convert.ToDecimal(row["Rate"].ToString());
                    var amount = quantity * rate;
                    var rateID = row["Rate ID"].ToString();
                    var rateIdLineNo = row["Rate Line No"].ToString();
                    var mrrno = row["MRR No"].ToString();

                    #region GetNewRateID
                    chkPeriod = Convert.ToInt32(mn) < 7
                                            ? Convert.ToDateTime("01/07/" + (Convert.ToInt32(yr) - 1))
                                            : Convert.ToDateTime("01/07/" + yr);
                    var dtRateId = DataProcess.GetSingleValueFromtable(_connectionString, SqlgenerateForFixedAsset.GetMaxRateId(chkPeriod));                        
                    var nextRateId = (dtRateId == null || Convert.ToInt32(dtRateId) == 0) ? 1 : Convert.ToInt32(dtRateId) + 1;
                    var NewRateId = "RT" + yr + mn + "-" + nextRateId.ToString("000000");

                    var dtRateIdGrp = DataProcess.GetSingleValueFromtable(_connectionString, SqlgenerateForFixedAsset.GetMaxRateIdGrp());                        
                    var nextRateIdGrp = (dtRateIdGrp == null || Convert.ToInt32(dtRateIdGrp) == 0) ? 1 : Convert.ToInt32(dtRateIdGrp) + 1;
                    var NewRateIdGrp = "GRP-" + nextRateIdGrp.ToString("000000");
                    #endregion

                    #region Insert_Trans_Det

                    DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertMRRDet("IT", "STR", TranRefNo, i, "", 0, itemCode, itemName, uom, fromStore, "", "",
                                            1, row["Serial"].ToString(), Convert.ToDateTime(txtTransferDate.Text.Trim()),
                                            Convert.ToDateTime(txtTransferDate.Text.Trim()), Convert.ToDouble(quantity),
                                            0, rate, amount, amount, quantity.ToString(), mrrno, rateID, Convert.ToInt32(rateIdLineNo), 0));

                    DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertMRRDet("RT", "STR", TranRefNo, i, "", 0, itemCode, itemName, uom, toStore, "", "",
                                            1, row["Serial"].ToString(), Convert.ToDateTime(txtTransferDate.Text.Trim()),
                                            Convert.ToDateTime(txtTransferDate.Text.Trim()), Convert.ToDouble(quantity),
                                            0, rate, amount, amount, quantity.ToString(), "", rateID, Convert.ToInt32(rateIdLineNo), 0));
                    
                    #endregion

                    if (HPFlag == "P")
                    {
                        #region InsertSerial
                        var serial = row["Serial"].ToString().Trim().Split(',');
                        foreach (var s in serial)
                        {
                            var serialNo = s.ToString();

                            if (serialNo != "")
                            {
                                DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertItemSerial(itemCode, serialNo, TranRefNo, fromStore, "IT", "STR",
                                                          Convert.ToDateTime(txtTransferDate.Text.Trim()), "Good", rateID));
                                DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertItemSerial(itemCode, serialNo, TranRefNo, toStore, "RT", "STR",
                                                              Convert.ToDateTime(txtTransferDate.Text.Trim()), "Good", rateID));
                            }
                        }
                        #endregion

                        #region EditFromStoreRateQty
                        var rateIdGrp = "";
                        var dtItmRate = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetItemRateByItem(fromStore, itemCode, rateID,
                                                                    Convert.ToInt32(rateIdLineNo)));                            
                        if (dtItmRate.Rows.Count > 0)
                        {
                            DataProcess.UpdateQuery(_connectionString, SqlgenerateForFixedAsset.EditItemRateQty(
                                (dtItmRate.Rows[0]["itm_rate_qty"].ToString() == null ? 0 : Convert.ToDecimal(dtItmRate.Rows[0]["itm_rate_qty"].ToString()) - quantity),
                                fromStore, itemCode, rateID,
                                Convert.ToInt32(rateIdLineNo)));
                        }
                        else
                        {
                            tran.Rollback();
                            tblPopUp.Rows[2].Cells[0].InnerText = "";
                            lblMsgHdr.Text = "Stock is not available in store " + fromStore;
                            ModalPopupExtender3.Show();
                            return false;
                        }
                        #endregion

                        #region EditToStoreRateQty
                        int rateLineNo = 0;
                        var dtItemSerial = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetRateByItmStoreDate(itemCode, toStore,
                                                                            Convert.ToDateTime(txtTransferDate.Text.Trim())));
                            
                        if (dtItemSerial.Rows.Count > 0)
                        {
                            foreach (DataRow dataRow in dtItemSerial.Rows)
                            {
                                rateLineNo = Convert.ToInt32( dtItemSerial.Rows[0]["itm_rate_lineno"].ToString());
                            }
                            rateLineNo = rateLineNo + 1;
                            rateIdGrp = dtItemSerial.Rows[0]["itm_rate_id_grp"].ToString();
                        }
                        else
                        {
                            rateLineNo = 1;
                        }
                        DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertItemRate(toStore, itemCode, TranRefNo, Convert.ToDateTime(txtTransferDate.Text.Trim()),
                                                  Convert.ToDecimal(quantity), Convert.ToDecimal(rate), rateLineNo, rateID,
                                                  rateIdGrp));                        
                        #endregion

                        #region UpdateStkVal

                        
                        

                        DataTable dtCtlStk = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetStkCtlByItemStore(itemCode, fromStore));                            
                        if (dtCtlStk.Rows.Count > 0)
                        {
                            curStk = dtCtlStk.Rows[0]["Stk_Ctl_Cur_Stk"].ToString() == null ? 0 : Convert.ToDouble( dtCtlStk.Rows[0]["Stk_Ctl_Cur_Stk"].ToString());
                            latRat = Convert.ToDecimal(Convert.ToDouble(dtCtlStk.Rows[0]["Stk_Ctl_Lat_Val"].ToString() == null ? 0 : Convert.ToDouble( dtCtlStk.Rows[0]["Stk_Ctl_Lat_Val"].ToString())) / curStk);
                            avgRat = Convert.ToDecimal(Convert.ToDouble(dtCtlStk.Rows[0]["Stk_Ctl_Ave_Val"].ToString() == null ? 0 : Convert.ToDouble( dtCtlStk.Rows[0]["Stk_Ctl_Ave_Val"].ToString())) / curStk);
                            stdRat = Convert.ToDecimal(Convert.ToDouble(dtCtlStk.Rows[0]["Stk_Ctl_Std_Val"].ToString() == null ? 0 : Convert.ToDouble( dtCtlStk.Rows[0]["Stk_Ctl_Std_Val"].ToString())) / curStk);
                        }

                        DataTable dtStkVal = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetMrrDataByMrrItem(TranRefNo, itemCode, fromStore));
                        if (dtStkVal.Rows.Count > 0)
                        {
                            DataProcess.UpdateQuery(_connectionString, SqlgenerateForFixedAsset.UpdateStkVal(Convert.ToDateTime(txtTransferDate.Text.Trim()), itemName, latRat,
                                                  avgRat, stdRat,
                                                  dtStkVal.Rows[0]["Stk_Val_Itm_Qty"].ToString() == null
                                                      ? 0
                                                      : Convert.ToDouble(dtStkVal.Rows[0]["Stk_Val_Itm_Qty"].ToString()) + Convert.ToDouble(quantity),
                                                  curStk.ToString(), "", "", "", "IT", "STR", TranRefNo, itemCode,
                                                  fromStore));
                        }
                        else
                        {
                            DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertStkVal("IT", "STR", TranRefNo,
                                                  Convert.ToDateTime(txtTransferDate.Text.Trim()), itemCode, itemName,
                                                  fromStore, rate, avgRat, stdRat, Convert.ToDouble(quantity),
                                                  curStk.ToString(), "", "", ""));
                        }

                        dtCtlStk = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetStkCtlByItemStore(itemCode, toStore));
                        if (dtCtlStk.Rows.Count > 0)
                        {
                            curStk = dtCtlStk.Rows[0]["Stk_Ctl_Cur_Stk"].ToString() == null ? 0 : Convert.ToDouble( dtCtlStk.Rows[0]["Stk_Ctl_Cur_Stk"].ToString());
                            if (curStk == 0)
                            {
                                latRat = Convert.ToDecimal(amount / quantity);
                                avgRat = Convert.ToDecimal(amount / quantity);
                                stdRat = Convert.ToDecimal(amount / quantity);
                            }
                            else
                            {
                                latRat = Convert.ToDecimal(Convert.ToDouble(dtCtlStk.Rows[0]["Stk_Ctl_Lat_Val"].ToString() == null ? 0 : Convert.ToDouble( dtCtlStk.Rows[0]["Stk_Ctl_Lat_Val"].ToString())) / curStk);
                                avgRat = Convert.ToDecimal(Convert.ToDouble(dtCtlStk.Rows[0]["Stk_Ctl_Ave_Val"].ToString() == null ? 0 : Convert.ToDouble( dtCtlStk.Rows[0]["Stk_Ctl_Ave_Val"].ToString())) / curStk);
                                stdRat = Convert.ToDecimal(Convert.ToDouble(dtCtlStk.Rows[0]["Stk_Ctl_Std_Val"].ToString() == null ? 0 : Convert.ToDouble( dtCtlStk.Rows[0]["Stk_Ctl_Std_Val"].ToString())) / curStk);
                            }
                        }

                        dtStkVal = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetMrrDataByMrrItem(TranRefNo, itemCode, toStore));                            
                        if (dtStkVal.Rows.Count > 0)
                        {
                            DataProcess.UpdateQuery(_connectionString, SqlgenerateForFixedAsset.UpdateStkVal(Convert.ToDateTime(txtTransferDate.Text.Trim()), itemName, latRat,
                                                  avgRat, stdRat,
                                                  dtStkVal.Rows[0]["Stk_Val_Itm_Qty"].ToString() == null
                                                      ? 0
                                                      : Convert.ToDouble(dtStkVal.Rows[0]["Stk_Val_Itm_Qty"].ToString()) + Convert.ToDouble(quantity),
                                                  curStk.ToString(), "", "", "", "RT", "STR", TranRefNo, itemCode,
                                                  toStore));
                        }
                        else
                        {
                            DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertStkVal("RT", "STR", TranRefNo,
                                                  Convert.ToDateTime(txtTransferDate.Text.Trim()), itemCode, itemName,
                                                  toStore, rate, avgRat, stdRat, Convert.ToDouble(quantity),
                                                  curStk.ToString(), "", "", ""));
                        }
                        #endregion

                        #region UpdateStkCtl


                        DataTable dtStkCtl = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetStkCtlByItemStore(itemCode, fromStore));                            
                        if (dtStkCtl.Rows.Count > 0)
                        {
                            newCurStk = dtStkCtl.Rows[0]["Stk_Ctl_Cur_Stk"].ToString() == null
                                            ? 0
                                            : Convert.ToDouble( dtStkCtl.Rows[0]["Stk_Ctl_Cur_Stk"].ToString()) - Convert.ToDouble(quantity);
                            var newFreeStk = dtStkCtl.Rows[0]["Stk_Ctl_Free_Stk"].ToString() == null
                                                 ? 0
                                                 : Convert.ToDouble( dtStkCtl.Rows[0]["Stk_Ctl_Free_Stk"].ToString()) - Convert.ToDouble(quantity);
                            stdVal = dtStkCtl.Rows[0]["Stk_Ctl_Std_Val"].ToString() == null ? 0 : Convert.ToDecimal( dtStkCtl.Rows[0]["Stk_Ctl_Std_Val"].ToString()) - amount;
                            avgVal = dtStkCtl.Rows[0]["Stk_Ctl_Ave_Val"].ToString() == null ? 0 : Convert.ToDecimal( dtStkCtl.Rows[0]["Stk_Ctl_Ave_Val"].ToString()) - amount;
                            latVal = dtStkCtl.Rows[0]["Stk_Ctl_Lat_Val"].ToString() == null ? 0 : Convert.ToDecimal( dtStkCtl.Rows[0]["Stk_Ctl_Lat_Val"].ToString()) - amount;
                            DataProcess.UpdateQuery(_connectionString, SqlgenerateForFixedAsset.EditStkCtl("", newCurStk, newFreeStk, stdVal, avgVal, latVal,
                                                Convert.ToDateTime(txtTransferDate.Text.Trim()), "", "", "", 0, itemCode,
                                                fromStore));
                        }
                        else
                        {
                            tran.Rollback();
                            return false;
                        }
                        dtStkCtl = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetStkCtlByItemStore(itemCode, toStore));
                        if (dtStkCtl.Rows.Count > 0)
                        {
                            newCurStk = dtStkCtl.Rows[0]["Stk_Ctl_Cur_Stk"].ToString() == null ? 0 : Convert.ToDouble( dtStkCtl.Rows[0]["Stk_Ctl_Cur_Stk"].ToString()) + Convert.ToDouble(quantity);
                            var newFreeStk = dtStkCtl.Rows[0]["Stk_Ctl_Free_Stk"].ToString() == null
                                                 ? 0
                                                 : Convert.ToDouble( dtStkCtl.Rows[0]["Stk_Ctl_Free_Stk"].ToString()) + Convert.ToDouble(quantity);
                            stdVal = dtStkCtl.Rows[0]["Stk_Ctl_Std_Val"].ToString() == null ? 0 : Convert.ToDecimal( dtStkCtl.Rows[0]["Stk_Ctl_Std_Val"].ToString()) + amount;
                            avgVal = dtStkCtl.Rows[0]["Stk_Ctl_Ave_Val"].ToString() == null ? 0 : Convert.ToDecimal( dtStkCtl.Rows[0]["Stk_Ctl_Ave_Val"].ToString()) + amount;
                            latVal = dtStkCtl.Rows[0]["Stk_Ctl_Lat_Val"].ToString() == null ? 0 : Convert.ToDecimal( dtStkCtl.Rows[0]["Stk_Ctl_Lat_Val"].ToString()) + amount;
                            DataProcess.UpdateQuery(_connectionString, SqlgenerateForFixedAsset.EditStkCtl("", newCurStk, newFreeStk, stdVal, avgVal, latVal,
                                                Convert.ToDateTime(txtTransferDate.Text.Trim()), "", "", "", 0, itemCode,
                                                toStore));
                        }
                        else
                        {
                            DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertStkCtl(toStore, itemCode, "", Convert.ToDouble(quantity),
                                                  Convert.ToDouble(quantity), 0, 0, 0, 0, 0, 0, amount, amount, amount,
                                                  0, 0, Convert.ToDateTime(txtTransferDate.Text.Trim()), null, "", "",
                                                  "", 0));
                        }

                        #endregion
                    }
                    i++;
                }

                tran.Commit();

                lblMsgHdr.Text = HPFlag == "H" ? "Successfully Holded" : "Successfull Posted";
                tblPopUp.Rows[2].Cells[0].InnerText = "Transfer Ref No";
                txtTransferRef.Text = TranRefNo;
                txtTransferRef.Visible = true;
                ModalPopupExtender3.Show();
                ClearFieldData("P");
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            tran.Rollback();
            tblPopUp.Rows[2].Cells[0].InnerText = "";
            lblMsgHdr.Text = "Data Processing Error.";
            ModalPopupExtender3.Show();
            return false;
        }
    }

    protected void btnHold_Click(object sender, EventArgs e)
    {
        SaveData("H");
    }
    
    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        var storeCode = "";
        try
        {
            if (txtFromStore.Text.ToUpper() != "")
            {
                string[] temp = txtFromStore.Text.Split(':');
                if (temp.Length >= 1) storeCode = temp[0];
            }
            DataTable dtStore = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetStoreByCode(storeCode));
            args.IsValid = dtStore.Rows.Count > 0;
        }
        catch (Exception)
        {
            args.IsValid = false;
            //throw;
        }
    }

    protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
    {
        var storeCode = "";
        try
        {
            if (txtToStore.Text.ToUpper() != "")
            {
                string[] temp = txtToStore.Text.Split(':');
                if (temp.Length >= 1) storeCode = temp[0];
            }
            DataTable dtStore = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetStoreByCode(storeCode));
            args.IsValid = dtStore.Rows.Count > 0;
        }
        catch (Exception)
        {
            args.IsValid = false;
            //throw;
        }        
    }

    protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
    {
        var itemCode = "";
        try
        {
            if (txtItem.Text.ToUpper() != "")
            {
                string[] temp = txtItem.Text.Split(':');
                if (temp.Length >= 1) itemCode = temp[0];
            }
            DataTable dtItem = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetItemByCode(itemCode));
            args.IsValid = dtItem.Rows.Count > 0;
        }
        catch (Exception)
        {
            args.IsValid = false;
            //throw;
        }
    }
    protected void btnAddSerial_Click(object sender, EventArgs e)
    {
        try
        {
            var itemCode = "";
            if (txtItem.Text.ToUpper() != "")
            {
                string[] temp = txtItem.Text.Split(':');
                if (temp.Length >= 1) itemCode = temp[0];
            }

            if (itemCode != "")
            {
                var dtItem = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetItemByCode(itemCode));
                txtUom.Text = dtItem.Rows[0]["Itm_Det_stk_unit"].ToString();
            }

            var fromStore = "";
            if (txtFromStore.Text.ToUpper() != "")
            {
                string[] temp = txtFromStore.Text.Split(':');
                if (temp.Length >= 1) fromStore = temp[0];
            }

            clsDbCon dbCon = new clsDbCon();
            string qryStr;

            Recordset rs = new Recordset();
            Connection DC = new Connection();
            string constr, str;

            constr = _connectionString;

            qryStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[last_trn_status]')) " +
                     " DROP VIEW [dbo].[last_trn_status]";
            dbCon.ExecuteL3TSQLStmt(qryStr);

            qryStr =
                "CREATE VIEW last_trn_status AS SELECT itm_det_icode, itm_det_serial_no, MAX(sl_no) AS lasttrn  From Inma_itm_serial where itm_det_date <=convert(datetime,'" +
                DateTime.Now.ToString("dd/MM/yyyy") + "',103) and itm_det_icode ='" + itemCode +
                "' GROUP BY itm_det_icode, itm_det_serial_no ";
            dbCon.ExecuteL3TSQLStmt(qryStr);

            DC.Open(constr, null, null, 0);

            str = "select Distinct itm_det_serial_no from last_trn_serial_status Where itm_det_icode='" + itemCode +
                  "' and itm_det_str_code='" + fromStore + "' and itm_det_serial_no = '" + txtSerialNo.Text + "'";

            rs.Open(str, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);

            if (!rs.EOF)
            {
                if (txtSerialNo.Text.Length > 0)
                {
                    if (txtSerial.Text.Length > 0)
                    {
                        txtSerial.Text = txtSerial.Text + "," + txtSerialNo.Text.Trim();
                    }
                    else
                    {
                        txtSerial.Text = txtSerialNo.Text;
                    }
                    txtSerialNo.Text = "";
                }

                AutoCompleteExtenderSerial.ContextKey = itemCode + ":" + fromStore + ":" + txtSerial.Text.Trim();
            }
            else
            {
                tblPopUp.Rows[2].Cells[0].InnerText = "";
                lblMsgHdr.Text = "Serial No does not exists.";
                txtTransferRef.Visible = false;
                ModalPopupExtender3.Show();
                gvStkTransfer.SelectedIndex = -1;
            }

            rs.Close();
            DC.Close();
        }
        catch (Exception)
        {

            //throw;
        }
    }

    protected void btnEditSerial_Click(object sender, EventArgs e)
    {
        txtSerial.Enabled = true;
        btnEditSerial.Text = "Ok";
    }

    protected void txtSerial_TextChanged(object sender, EventArgs e)
    {
        var itemCode = "";
        if (txtItem.Text.ToUpper() != "")
        {
            string[] temp = txtItem.Text.Split(':');
            if (temp.Length >= 1) itemCode = temp[0];
        }

        if (itemCode != "")
        {
            DataTable dtItem = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetItemByCode(itemCode));
            if (dtItem.Rows.Count > 0)
            {
                txtUom.Text = dtItem.Rows[0]["Itm_Det_stk_unit"].ToString();
            }
        }

        var fromStore = "";
        if (txtFromStore.Text.ToUpper() != "")
        {
            string[] temp = txtFromStore.Text.Split(':');
            if (temp.Length >= 1) fromStore = temp[0];
        }
        AutoCompleteExtenderSerial.ContextKey = itemCode + ":" + fromStore + ":" + txtSerial.Text.Trim();
    }    
}
