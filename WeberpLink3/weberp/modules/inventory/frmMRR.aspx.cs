using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using LibraryDAL;

public partial class frmMRR : System.Web.UI.Page
{
    string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtReceiptDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtInvoiceDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            LoadInitMrrGrid();

            GetAllPoList();

        }
    }

    private void GetAllPoList()
    {
        ListItem lst;
        ddlpolist.Items.Clear();
        ddlpolist.Items.Add("");

        var dtMrrHdr = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetAllPo());

        foreach (DataRow dr in dtMrrHdr.Rows)
        {
            lst = new ListItem();
            lst.Text = dr["PO_Hdr_Ref"].ToString() + ":" + dr["PO_Hdr_Com1"].ToString();
            lst.Value = dr["PO_Hdr_Ref"].ToString() + ":" + dr["PO_Hdr_Pcode"].ToString() + ":" + dr["PO_Hdr_Code"].ToString();
            ddlpolist.Items.Add(lst);
        }
 
    }

    private void LoadInitMrrGrid()
    {
        var dt = new DataTable();
        dt.Rows.Clear();
        dt.Columns.Add("L#", typeof(string));
        dt.Columns.Add("Item Code", typeof(string));
        dt.Columns.Add("Item Name", typeof(string));
        dt.Columns.Add("UOM", typeof(string));
        dt.Columns.Add("Store", typeof(string));
        dt.Columns.Add("Quantity", typeof(string));
        dt.Columns.Add("Rate", typeof(string));
        dt.Columns.Add("Amount", typeof(string));
        dt.Columns.Add("Serial", typeof(string));
        ViewState["datatable"] = dt;
    }

    private void LoadInitPoGrid()
    {
        var dt = new DataTable();
        dt.Columns.Add("Po_Det_Lno", typeof(Int16));
        dt.Columns.Add("Po_Det_Icode", typeof(string));
        dt.Columns.Add("Po_Det_Itm_Desc", typeof(string));
        dt.Columns.Add("Po_Det_Itm_Uom", typeof(string));
        dt.Columns.Add("Po_Det_Str_Code", typeof(string));
        dt.Columns.Add("Po_Det_Bal_Qty", typeof(float));
        dt.Columns.Add("Po_Det_Lin_Rat", typeof(double));
        dt.Columns.Add("Po_Det_Mrr_Qty", typeof(float));
        dt.Columns.Add("Po_Det_Serial_No", typeof(string));
        ViewState["datatablePo"] = dt;
    }

    private void SetMrrGridData()
    {
        var dt = new DataTable();
        dt = (DataTable)ViewState["datatable"];
        gvMRR.DataSource = dt;
        gvMRR.DataBind();

        if (gvMRR.Rows.Count > 0)
        {
            this.CollapsiblePanelExtenderHdr.Collapsed = false;
            this.CollapsiblePanelExtenderHdr.ClientState = "false";
            this.CollapsiblePanelExtenderDet.Collapsed = false;
            this.CollapsiblePanelExtenderDet.ClientState = "false";
            //btnHold.Visible = true;
            btnPost.Visible = true;
        }
        else
        {
            this.CollapsiblePanelExtenderHdr.Collapsed = true;
            this.CollapsiblePanelExtenderHdr.ClientState = "true";
            this.CollapsiblePanelExtenderDet.Collapsed = true;
            this.CollapsiblePanelExtenderDet.ClientState = "true";
            //btnHold.Visible = false;
            btnPost.Visible = false;
        }
    }

    private void SetPoGridData()
    {
        var dt = new DataTable();
        dt = (DataTable)ViewState["datatablePo"];
        gvPoDet.DataSource = dt;
        gvPoDet.DataBind();

        if (gvPoDet.Rows.Count > 0)
        {
            this.CollapsiblePanelExtenderHdr.Collapsed = false;
            this.CollapsiblePanelExtenderHdr.ClientState = "false";
            this.CollapsiblePanelExtenderSelectPO.Collapsed = false;
            this.CollapsiblePanelExtenderSelectPO.ClientState = "false";
        }
        else
        {
            if (gvMRR.Rows.Count > 0)
            {
                this.CollapsiblePanelExtenderHdr.Collapsed = false;
                this.CollapsiblePanelExtenderHdr.ClientState = "false";
            }
            else
            {
                this.CollapsiblePanelExtenderHdr.Collapsed = true;
                this.CollapsiblePanelExtenderHdr.ClientState = "true";
            }
            this.CollapsiblePanelExtenderSelectPO.Collapsed = true;
            this.CollapsiblePanelExtenderSelectPO.ClientState = "true";
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        this.CollapsiblePanelExtenderHdr.Collapsed = true;
        this.CollapsiblePanelExtenderHdr.ClientState = "true";
        this.CollapsiblePanelExtenderDet.Collapsed = false;
        this.CollapsiblePanelExtenderDet.ClientState = "false";
    }

    private void ClearFieldData(string Pst_Flg)
    {
        if (Pst_Flg != "M")
        {
            txtMrrSearch.Text = "";
        }
        lblEditFlag.Text = "N";
        txtMrrSearch.Enabled = true;
        btnSearchMrr.Enabled = true;
        btnPrint.Visible = false;
        txtSupplier.Text = "";
        txtSupplier.Enabled = true;
        txtReceiptDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtInvoiceDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtDcNo.Text = "";
        txtRemarks.Text = "";
        LoadInitMrrGrid();
        SetMrrGridData();
        LoadInitPoGrid();
        SetPoGridData();
        ddlpolist.Text = "";
        ddlpolist.Enabled = true;
        btnSelectPo.Enabled = true;
        pnlSelectPoBody.Enabled = true;
        pHdrBody.Enabled = true;
        pDetBody.Enabled = true;
        gvMRR.Columns[0].Visible = true;
        gvMRR.Enabled = true;
        this.CollapsiblePanelExtenderSrchMrr.Collapsed = true;
        this.CollapsiblePanelExtenderSrchMrr.ClientState = "true";
        this.CollapsiblePanelExtenderSelectPO.Collapsed = false;
        this.CollapsiblePanelExtenderSelectPO.ClientState = "false";
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            ClearFieldData("M");

            var MRRRef = "";
            if (txtMrrSearch.Text.ToUpper() != "")
            {
                string[] temp = txtMrrSearch.Text.Split(':');
                if (temp.Length >= 1) MRRRef = temp[0];
            }
            var dtMrrHdr = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetMrrHdrByRefNo(MRRRef));
            if (dtMrrHdr.Rows.Count > 0)
            {
                var dtSupp = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetSupplierByCode(dtMrrHdr.Rows[0]["Trn_Hdr_Pcode"].ToString()));
                var supplier = dtMrrHdr.Rows[0]["Trn_Hdr_Pcode"].ToString() + ":" + dtSupp.Rows[0]["Par_Acc_Name"].ToString();
                txtSupplier.Text = supplier;
                txtReceiptDate.Text = dtMrrHdr.Rows[0]["Trn_Hdr_DATE"].ToString();
                txtInvoiceDate.Text = dtMrrHdr.Rows[0]["Trn_Hdr_Dc_Date"].ToString();
                txtDcNo.Text = dtMrrHdr.Rows[0]["Trn_Hdr_DC_No"].ToString();
                txtRemarks.Text = dtMrrHdr.Rows[0]["Trn_Hdr_Com1"].ToString();

                var dt = new DataTable();
                LoadInitMrrGrid();
                dt = (DataTable)ViewState["datatable"];

                var dtMrrDet = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetMrrDetByRefNo(MRRRef));
                int i = 0;

                foreach (DataRow row in dtMrrDet.Rows)
                {
                    var serial = "";
                    int j = 0;
                    var dtSerial = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetSerialByMrrRef(MRRRef, dtMrrDet.Rows[i]["Trn_Det_Icode"].ToString()));
                    foreach (DataRow dr in dtSerial.Rows)
                    {
                        serial = serial + "," + (dtSerial.Rows[j]["itm_det_serial_no"].ToString() == null ? "" : dtSerial.Rows[j]["itm_det_serial_no"].ToString());
                        j++;
                    }
                    if (serial != "")
                    {
                        serial = (serial).Substring(1);
                    }
                    dt.Rows.Add(dtMrrDet.Rows[i]["Trn_Det_Lno"].ToString(), dtMrrDet.Rows[i]["Trn_Det_Icode"].ToString(), dtMrrDet.Rows[i]["Trn_Det_Itm_Desc"].ToString(),
                                dtMrrDet.Rows[i]["Trn_Det_Itm_Uom"].ToString(),
                                dtMrrDet.Rows[i]["Trn_Det_Str_Code"].ToString(), dtMrrDet.Rows[i]["Trn_Det_Lin_Qty"].ToString(),
                                dtMrrDet.Rows[i]["Trn_Det_Lin_Rat"].ToString(),
                                dtMrrDet.Rows[i]["Trn_Det_Lin_Amt"].ToString(), serial);
                    ViewState["datatable"] = dt;
                    SetMrrGridData();
                    i++;
                }

                btnClearMrr.Visible = true;
                txtMrrSearch.Enabled = false;
                btnPrint.Visible = true;

                if (dtMrrHdr.Rows[0]["Trn_Hdr_HRPB_Flag"].ToString() == "P")
                {
                    btnHold.Visible = false;
                    btnPost.Visible = false;
                    pnlSelectPoBody.Enabled = false;
                    pHdrBody.Enabled = false;
                    pDetBody.Enabled = false;
                    gvMRR.Columns[0].Visible = false;
                    gvMRR.Enabled = false;
                }
                else
                {
                    btnHold.Visible = true;
                    btnPost.Visible = true;
                    pnlSelectPoBody.Enabled = true;
                    pHdrBody.Enabled = true;
                    pDetBody.Enabled = true;
                    gvMRR.Columns[0].Visible = true;
                    gvMRR.Enabled = true;
                }
                this.CollapsiblePanelExtenderSelectPO.Collapsed = true;
                this.CollapsiblePanelExtenderSelectPO.ClientState = "true";
                this.CollapsiblePanelExtenderSrchMrr.Collapsed = false;
                this.CollapsiblePanelExtenderSrchMrr.ClientState = "false";
                lblEditFlag.Text = "Y";
            }
        }
        catch (Exception)
        {
            btnClearMrr.Visible = true;
            
        }
    }

    protected void gvMRR_SelectedIndexChanged(object sender, EventArgs e)
    {
        var indx = gvMRR.SelectedIndex;
        if (indx != -1)
        {
            try
            {
                GridViewRow row = gvMRR.Rows[indx];

                var PORef = "";
                if (ddlpolist.Text.ToUpper() != "")
                {
                    string[] temp = ddlpolist.Text.Split(':');
                    if (temp.Length >= 1) PORef = temp[0];
                }

                var dtPoDet = new DataTable();
                dtPoDet = (DataTable)ViewState["datatablePo"];

                Label lblSerial = (Label)row.FindControl("Label1");
                var serialText = (lblSerial != null) ? lblSerial.Text : "";
                var dtDbPoDet = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetPoDetByItem(PORef, row.Cells[2].Text.Trim()));
                dtPoDet.Rows.Add(Convert.ToInt16(row.Cells[1].Text), row.Cells[2].Text, row.Cells[3].Text,
                                 row.Cells[4].Text, row.Cells[5].Text,
                                 dtDbPoDet.Rows[0]["PO_Det_Bal_Qty"].ToString() == null ? 0 : Convert.ToInt32( dtDbPoDet.Rows[0]["PO_Det_Bal_Qty"].ToString()),
                                 row.Cells[7].Text, row.Cells[6].Text, serialText);

                ViewState["datatablePo"] = dtPoDet;
                SetPoGridData();

                var dt = new DataTable();
                dt = (DataTable)ViewState["datatable"];
                dt.Rows.RemoveAt(indx);
                ViewState["datatable"] = dt;
                SetMrrGridData();
                gvMRR.SelectedIndex = -1;
            }
            catch (Exception)
            {

                //throw;
            }
        }        
    }

    protected void gvPoDet_SelectedIndexChanged(object sender, EventArgs e)
    {
        var indx = gvPoDet.SelectedIndex;
        if (indx != -1)
        {
            try
            {
                GridViewRow row = gvPoDet.Rows[indx];

                var poQty = Convert.ToInt32(row.Cells[5].Text.Trim());
                var poRate = Convert.ToDecimal(row.Cells[6].Text.Trim());
                var mrrQty = Convert.ToInt32(((TextBox)(row.Cells[7].FindControl("txtMrrQty"))).Text);
                var serialTextBox = (TextBox)row.Cells[8].FindControl("txtSerial");
                var itmSerial = ((TextBox)(row.Cells[8].FindControl("txtSerial"))).Text;

                if (serialTextBox.Visible == true)
                {
                    if (itmSerial == "")
                    {
                        gvPoDet.SelectedIndex = -1;
                        lblMsgHdr.Text = "Enter Serial No";
                        tblPopUp.Rows[2].Cells[0].InnerText = "";
                        txtMrrRef.Visible = false;
                        ModalPopupExtender3.Show();
                        return;
                    }
                }


                if (mrrQty > poQty)
                {
                    gvPoDet.SelectedIndex = -1;
                    return;
                }

                if (itmSerial != "")
                {
                    string[] temp = itmSerial.Split(',');
                    List<string> vals = new List<string>();
                    bool returnValue = false;
                    string dplicateSerial = "";
                    foreach (string s in temp)
                    {
                        if (vals.Contains(s))
                        {
                            dplicateSerial = s;
                            returnValue = true;
                            break;
                        }
                        vals.Add(s);
                    }

                    if (returnValue)
                    {
                        tblPopUp.Rows[2].Cells[0].InnerText = "";
                        lblMsgHdr.Text = "Duplicate Serial No Found. Serial #" + dplicateSerial;
                        txtMrrRef.Visible = false;
                        ModalPopupExtender3.Show();
                        gvPoDet.SelectedIndex = -1;
                        return;
                    }
                }


                if (itmSerial != "")
                {
                    string[] temp = itmSerial.Split(',');
                    var numberOfSerial = temp.Length;
                    if (mrrQty != numberOfSerial)
                    {
                        tblPopUp.Rows[2].Cells[0].InnerText = "";
                        lblMsgHdr.Text = "MRR Quantity and Number of Serial does not match.";
                        txtMrrRef.Visible = false;
                        ModalPopupExtender3.Show();
                        gvPoDet.SelectedIndex = -1;
                        return;
                    }
                }

                var dt = new DataTable();
                dt = (DataTable)ViewState["datatable"];

                dt.Rows.Add(row.Cells[0].Text.Trim(), row.Cells[1].Text.Trim(), row.Cells[2].Text.Trim(), row.Cells[3].Text.Trim(),
                            row.Cells[4].Text.Trim(), mrrQty.ToString(), poRate.ToString(),
                            (Convert.ToDecimal(mrrQty) * Convert.ToDecimal(poRate)).ToString("00.0000"), itmSerial);
                ViewState["datatable"] = dt;
                SetMrrGridData();

                var dtPoDet = new DataTable();
                dtPoDet = (DataTable)ViewState["datatablePo"];
                dtPoDet.Rows.RemoveAt(indx);
                ViewState["datatablePo"] = dtPoDet;
                SetPoGridData();
                gvPoDet.SelectedIndex = -1;
            }
            catch (Exception)
            {

                //throw;
            }
        }        
    }
    protected void gvPoDet_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "btnAdd")
        {
            int index = Convert.ToInt32(e.CommandArgument);     
            GridViewRow row = gvPoDet.Rows[index];
        }
    }
    protected void btnClearPo_Click(object sender, EventArgs e)
    {
        ClearFieldData("H");
        ddlpolist.Enabled = true;
        btnSelectPo.Enabled = true;       
    }
    protected void btnClearMrr_Click(object sender, EventArgs e)
    {
        ClearFieldData("P");        
    }
    protected void btnSelectPo_Click(object sender, EventArgs e)
    {
        try
        {
            var PORef = "";
            if (ddlpolist.Text.ToUpper() != "")
            {
                string[] temp = ddlpolist.Text.Split(':');
                if (temp.Length >= 1) PORef = temp[0];
            }
            var dtPoHdr = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetPOByRefNo(PORef));
            if (dtPoHdr.Rows.Count > 0)
            {
                var dtSupp = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetSupplierByCode(dtPoHdr.Rows[0]["PO_Hdr_Pcode"].ToString()));
                var supplier = dtPoHdr.Rows[0]["PO_Hdr_Pcode"].ToString() + ":" + dtSupp.Rows[0]["Par_Acc_Name"].ToString();
                txtSupplier.Text = supplier;
                txtSupplier.Enabled = false;

                txtReceiptDate.Text = dtPoHdr.Rows[0]["PO_Hdr_DATE"].ToString();
                txtInvoiceDate.Text = dtPoHdr.Rows[0]["PO_Hdr_DATE"].ToString();

                var dtPoDet = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetPendingPoData(PORef));
                LoadInitPoGrid();

                var dt = new DataTable();
                dt = (DataTable)ViewState["datatablePo"];

                foreach (DataRow dr in dtPoDet.Rows)
                {
                    dt.Rows.Add(dr["PO_Det_Lno"].ToString(), 
                        dr["PO_Det_Icode"].ToString(),
                        dr["PO_Det_Itm_Desc"].ToString(), 
                        dr["PO_Det_Itm_Uom"].ToString(), 
                        dr["PO_Det_Str_Code"].ToString(),
                        dr["PO_Det_Bal_Qty"].ToString(),
                        dr["PO_Det_Lin_Rat"].ToString(), 
                        dr["PO_Det_Bal_Qty"].ToString(), "");
                }

                ViewState["datatablePo"] = dt;
                SetPoGridData();
                ddlpolist.Enabled = false;
                btnSelectPo.Enabled = false;
                btnClearPo.Visible = true;
                btnClearPo.Enabled = true;
            }
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

    private bool SaveData(string HPFlag)
    {
        //if (!Page.IsValid) return false;
               
        bool ReturnDberror=false;

        if (txtDcNo.Text.Trim() == "")
        {
            lblMsgHdr.Text = "Enter DC No first.";
            tblPopUp.Rows[2].Cells[0].InnerText = "";
            txtMrrRef.Text = "";
            txtMrrRef.Visible = false;
            ModalPopupExtender3.Show();
            return false;
        }

        string Prefix = "", yr = "", mn = "", Spr = "", mnFlg = "";
        string MrrRefNo = "";
        string MaxJvRefNo;
        string NewJvRefNo;
        decimal totAmount = 0;

        
        string trnPeriod = Convert.ToDateTime(txtReceiptDate.Text.Trim()).ToString("MM") + "/" + Convert.ToDateTime(txtReceiptDate.Text.Trim()).ToString("yyyy");
        DateTime chkPeriod = DateTime.Now;
        var userId = Session[StaticData.sessionUserId].ToString();
        DataTable dmns = new DataTable();

        SqlConnection conn = new SqlConnection(_connectionString);
        conn.Open();
        SqlTransaction myTran = conn.BeginTransaction();
        try
        {
            var dt = new DataTable();
            dt = (DataTable)ViewState["datatable"];
            foreach (DataRow row in dt.Rows)
            {
                totAmount = (totAmount + Convert.ToDecimal(row["Amount"].ToString()));
            }
            #region GetPoRefNo
            var PORef = "";
            if (ddlpolist.Text.ToUpper() != "")
            {
                string[] temp = ddlpolist.Text.Split(':');
                if (temp.Length >= 1) PORef = temp[0];
            }

            #endregion

            #region GetSupplier
            string supplierCode = "";
            var supplierName = "";
            if (txtSupplier.Text.ToUpper() != "")
            {
                string[] temp = txtSupplier.Text.Split(':');
                if (temp.Length >= 1)
                {
                    supplierCode = temp[0];
                    supplierName = temp[1];
                }
            }



            #endregion

            //barcode number
            string barcodeformat = "";
            var barcodeNumber = 0;
            var barcodeRef = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetMaxBarcodeNo());
            barcodeNumber = (barcodeRef.Rows[0][0].ToString() == null || Convert.ToInt32(barcodeRef.Rows[0][0].ToString()) == 0) ? 1 : Convert.ToInt32(barcodeRef.Rows[0][0].ToString());
                
            
            if (lblEditFlag.Text == "N")
            {
                #region GetNewMrrRefNo

                var dtMrrRefFormat = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetMrrRefFormat());
                if (dtMrrRefFormat.Rows.Count > 0)
                {
                    Prefix = dtMrrRefFormat.Rows[0]["SA_Prefix"].ToString();
                    Spr = dtMrrRefFormat.Rows[0]["SA_Sep"].ToString();

                    if (dtMrrRefFormat.Rows[0]["SA_Yr"].ToString() == "Y")
                    {
                        yr = Convert.ToDateTime(txtReceiptDate.Text.Trim()).ToString("yy");
                    }
                    else
                    {
                        yr = "";
                    }

                    if (dtMrrRefFormat.Rows[0]["SA_Mn"].ToString() == "Y")
                    {
                        mn = Convert.ToDateTime(txtReceiptDate.Text.Trim()).ToString("MM");
                    }
                    else
                    {
                        mn = "";                        
                    }

                    Prefix = Prefix + yr + mn + Spr;
                    chkPeriod = Convert.ToInt32(mn) < 7
                                    ? Convert.ToDateTime("01/07/" + (Convert.ToInt32(yr) - 1))
                                    : Convert.ToDateTime("01/07/" + yr);
                }

                var dtRefNo = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetMaxMrrRefNo(chkPeriod));

                var nextRefNo = (dtRefNo.Rows[0][0].ToString() == null || Convert.ToInt32(dtRefNo.Rows[0][0].ToString()) == 0) ? 1 : Convert.ToInt32(dtRefNo.Rows[0][0].ToString()) + 1;
                MrrRefNo = Prefix + nextRefNo.ToString("00000");

                #endregion

                #region InsertMrrHdr
                string tempValue2 = null;
                ReturnDberror=DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertMRRHdr("RC", "PO", MrrRefNo, supplierCode, supplierCode, supplierCode,
                                      Convert.ToDateTime(txtReceiptDate.Text.Trim()), txtRemarks.Text.Trim(), "", "", "",
                                      "", "", "", "", "", "", totAmount, HPFlag,
                                      trnPeriod, "SUB", "", "", "Y",
                                      txtDcNo.Text.Trim(), "", "", "", "", "", 0, 0,
                                      Convert.ToDateTime(txtInvoiceDate.Text.Trim()), tempValue2, ""));

                if (ReturnDberror == false)
                    goto errorCode;

                #endregion
            }
            else
            {
                #region GetSrchMrrRefNo

                if (txtMrrSearch.Text.ToUpper() != "")
                {
                    string[] temp = txtMrrSearch.Text.Split(':');
                    if (temp.Length >= 1) MrrRefNo = temp[0];
                }

                #endregion

                #region EditMrrHdr
                string tempValue =  null;
                ReturnDberror=DataProcess.UpdateQuery(_connectionString, SqlgenerateForFixedAsset.EditMRRHdr(supplierCode, supplierCode, supplierCode,
                                    Convert.ToDateTime(txtReceiptDate.Text.Trim()), txtRemarks.Text.Trim(), "", "", "", "", "", "", "", "", "",
                                    totAmount, HPFlag, trnPeriod,
                                    "SUB", "", "", "Y", txtDcNo.Text.Trim(), "", "", "", "", "", 0, 0,
                                    Convert.ToDateTime(txtInvoiceDate.Text.Trim()), tempValue, "", "RC", "PO", MrrRefNo));

                if (ReturnDberror == false)
                    goto errorCode;

                #endregion

                #region DeleteMrrDet
                ReturnDberror=DataProcess.DeleteQuery(_connectionString, SqlgenerateForFixedAsset.DeleteMRRDet("RC", "PO", MrrRefNo));
                if (ReturnDberror == false)
                    goto errorCode;
                #endregion

                #region DeleteSerial
                ReturnDberror=DataProcess.DeleteQuery(_connectionString, SqlgenerateForFixedAsset.DeleteMrrSerial(MrrRefNo));
                if (ReturnDberror == false)
                    goto errorCode;
                
                #endregion
            }

            #region NewAccHdr
            TransactionHeaderDAO DaoHdr = new TransactionHeaderDAO();
            DaoHdr.TrnAccPeriod = trnPeriod;
            DaoHdr.TrnCurrCode = "BDT";
            DaoHdr.TrnCurrRate = 1;
            DaoHdr.TrnDATE = Convert.ToDateTime(txtReceiptDate.Text.Trim());
            DaoHdr.TrnEntryDATE = DateProcess.GetServerDate(_connectionString);
            DaoHdr.TrnEntryFlag = "A";
            DaoHdr.TrnEntryUser = "SUB";
            DaoHdr.TrnJrnType = "RJV";
            DaoHdr.TrnRefNo = "";
            DaoHdr.VoucherType = "J";
            DaoHdr.ModuleName = "Accounts";
            #endregion

            string CreditNarration = "";

            List<TransactionDetailsDAO> tdDaolst = new List<TransactionDetailsDAO>();

            Int16 i = 1;
            foreach (DataRow row in dt.Rows)
            {
                decimal avgVal = 0;
                decimal stdVal = 0;
                decimal latVal = 0;
                decimal latRat = 0;
                decimal avgRat = 0;
                decimal stdRat = 0;
                int curStk = 0;
                double newCurStk = 0;

                Int16 lineNo = Convert.ToInt16(row["L#"].ToString());
                string itemCode = row["Item Code"].ToString();
                string itemName = row["Item Name"].ToString();
                string uom = row["UOM"].ToString();
                string store = row["Store"].ToString();
                decimal quantity = Convert.ToDecimal(row["Quantity"].ToString());
                decimal rate = Convert.ToDecimal(row["Rate"].ToString());
                decimal amount = quantity * rate;
                

                if (i == 1)
                {
                    CreditNarration = quantity.ToString() + " " + uom + " " + itemName;
                }
                else
                {
                    CreditNarration = CreditNarration + "," + quantity.ToString() + " " + uom + " " + itemName;
                }

                if (CreditNarration.Length >= 170)
                {
                    CreditNarration = CreditNarration.Substring(0, 169);
                }


                #region GetRateID
                chkPeriod = Convert.ToInt32(mn) < 7
                                        ? Convert.ToDateTime("01/07/" + (Convert.ToInt32(yr) - 1))
                                        : Convert.ToDateTime("01/07/" + yr);
                var dtRateId = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetMaxRateId(chkPeriod));

                var nextRateId = (dtRateId.Rows[0][0].ToString() == null || Convert.ToInt32(dtRateId.Rows[0][0].ToString()) == 0) ? 1 : Convert.ToInt32(dtRateId.Rows[0][0].ToString()) + 1;
                var NewRateId = "RT" + yr + mn + "-" + nextRateId.ToString("000000");

                var dtRateIdGrp = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetMaxRateIdGrp());

                var nextRateIdGrp = (dtRateIdGrp.Rows[0][0].ToString() == null || Convert.ToInt32(dtRateIdGrp.Rows[0][0].ToString()) == 0) ? 1 : Convert.ToInt32(dtRateIdGrp.Rows[0][0].ToString()) + 1;
                var NewRateIdGrp = "GRP-" + nextRateIdGrp.ToString("000000");
                #endregion

                #region Insert_MrrDet

                ReturnDberror=DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertMRRDet("RC", "PO", MrrRefNo, i, "", 0, itemCode, itemName, uom, store, "", PORef,
                                      lineNo, row["Serial"].ToString(),
                                      Convert.ToDateTime(txtReceiptDate.Text.Trim()),
                                      Convert.ToDateTime(txtReceiptDate.Text.Trim()), Convert.ToDouble(quantity), 0, rate, amount,
                                      amount, quantity.ToString(), "", NewRateId, 1, 0));


               
                
                var serialnumber = row["Serial"].ToString().Trim().Split(',');
                if (serialnumber.Length==0)
                {
                    //non serial Item
                    int qty = (int)quantity;
                    barcodeformat = yr + mn + barcodeNumber.ToString("00000000");

                    ReturnDberror = DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertItemBarcode(PORef, itemCode, "", MrrRefNo, store, "RC", "PO",
                                                        Convert.ToDateTime(txtReceiptDate.Text.Trim()), "Good", NewRateId, qty, barcodeformat));

                    barcodeNumber++;
                }




                if (ReturnDberror == false)
                    goto errorCode;

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
                            ReturnDberror=DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertItemSerial(itemCode, serialNo, MrrRefNo, store, "RC", "PO",
                                                          Convert.ToDateTime(txtReceiptDate.Text.Trim()), "Good",
                                                          NewRateId));
                            ReturnDberror = DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertItemSerialtemp(PORef, itemCode, serialNo, MrrRefNo, store, "RC", "PO",
                                                         Convert.ToDateTime(txtReceiptDate.Text.Trim()), "Good",
                                                         NewRateId));

                            barcodeformat = yr + mn + barcodeNumber.ToString("00000000");
                            int qty =1;
                            ReturnDberror = DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertItemBarcode(PORef, itemCode, serialNo, MrrRefNo, store, "RC", "PO",
                                                               Convert.ToDateTime(txtReceiptDate.Text.Trim()), "Good", NewRateId, qty, barcodeformat));

                            barcodeNumber++;

                            if (ReturnDberror == false)
                                goto errorCode;
                        }
                    }
                    #endregion

                    #region UpdateRate
                    int rateLineNo = 0;
                    var dtItemSerial = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetRateByItmStoreDate(itemCode, store,
                                                                        Convert.ToDateTime(txtReceiptDate.Text.Trim())));
                        
                    if (dtItemSerial.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in dtItemSerial.Rows)
                        {
                            rateLineNo = Convert.ToInt32( dtItemSerial.Rows[0]["itm_rate_lineno"].ToString());
                        }
                        rateLineNo = rateLineNo + 1;
                    }
                    else
                    {
                        rateLineNo = 1;
                    }

                    ReturnDberror=DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertItemRate(store, itemCode, MrrRefNo, Convert.ToDateTime(txtReceiptDate.Text.Trim()),
                                              Convert.ToDecimal(quantity), Convert.ToDecimal(rate), rateLineNo, NewRateId,
                                              NewRateIdGrp));


                    if (ReturnDberror == false)
                        goto errorCode;

                    #endregion

                    #region UpdatePODet

                    var dtPoDet = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetPoDetByItem(PORef, itemCode));
                        
                    if (dtPoDet.Rows.Count > 0)
                    {
                        double orgQty = dtPoDet.Rows[0]["PO_Det_Org_QTY"].ToString() == null ? 0 : Convert.ToDouble( dtPoDet.Rows[0]["PO_Det_Org_QTY"].ToString()) + Convert.ToDouble(quantity);
                        double QcQty = dtPoDet.Rows[0]["PO_Det_Qc_QTY"].ToString() == null ? 0 : Convert.ToDouble(dtPoDet.Rows[0]["PO_Det_Qc_QTY"].ToString()) - Convert.ToDouble(quantity);
                        double insQty = dtPoDet.Rows[0]["PO_Det_Ins_QTY"].ToString() == null ? 0 : Convert.ToDouble(dtPoDet.Rows[0]["PO_Det_Ins_QTY"].ToString());
                        double balQty = dtPoDet.Rows[0]["PO_Det_Lin_Qty"].ToString() == null ? 0 : Convert.ToDouble(dtPoDet.Rows[0]["PO_Det_Lin_Qty"].ToString()) - orgQty - QcQty - insQty;
                        ReturnDberror=DataProcess.UpdateQuery(_connectionString, SqlgenerateForFixedAsset.UpdatePoQty(orgQty, balQty,QcQty,"Y", PORef, itemCode));
                        if (ReturnDberror == false)
                            goto errorCode;
                    }

                    #endregion

                    #region UpdateItmStk
                    var dtItmStk = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetItmStkByICode(itemCode));
                        
                    if (dtItmStk.Rows.Count > 0)
                    {
                        var dtGetStkCtl = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetStkCtlByItemStore(itemCode, store));
                            
                        dmns = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetDataByItemCode(itemCode, store));
                            
                        if (dtGetStkCtl.Rows.Count > 0)
                        {
                            newCurStk = (dtGetStkCtl.Rows[0]["Stk_Ctl_Cur_Stk"].ToString() == null ? 0 : Convert.ToDouble( dtGetStkCtl.Rows[0]["Stk_Ctl_Cur_Stk"].ToString())) +
                                        Convert.ToDouble(quantity);
                            avgRat = ((dtGetStkCtl.Rows[0]["Stk_Ctl_Ave_Val"].ToString() == null ? 0 : Convert.ToDecimal( dtGetStkCtl.Rows[0]["Stk_Ctl_Ave_Val"].ToString())) + amount) / (decimal)newCurStk;
                            avgVal = (dtGetStkCtl.Rows[0]["Stk_Ctl_Ave_Val"].ToString() == null ? 0 : Convert.ToDecimal( dtGetStkCtl.Rows[0]["Stk_Ctl_Ave_Val"].ToString())) +
                                     amount;
                        }
                        stdVal = (decimal)((dtItmStk.Rows[0]["Itm_Stk_Cur"].ToString() == null ? 0 : Convert.ToDouble( dtItmStk.Rows[0]["Itm_Stk_Cur"].ToString())) + Convert.ToDouble(quantity)) *
                                 (dtItmStk.Rows[0]["Itm_Stk_STD_Rat"].ToString() == null ? 0 : Convert.ToDecimal( dtItmStk.Rows[0]["Itm_Stk_STD_Rat"].ToString()));

                        latVal = ((decimal)(dtItmStk.Rows[0]["Itm_Stk_Cur"].ToString() == null ? 0 : Convert.ToDecimal( dtItmStk.Rows[0]["Itm_Stk_Cur"].ToString())) + quantity) *
                                 rate;
                        DataProcess.UpdateQuery(_connectionString, SqlgenerateForFixedAsset.EditItmStk(newCurStk, "A", 0, 0, rate, avgRat, "", "", "", 0, itemCode));
                            

                        if (dmns.Rows.Count > 0)
                        {
                            int npo = Convert.ToInt32( dmns.Rows[0]["MSL"].ToString());

                            if (npo < Convert.ToInt32(newCurStk))
                            {
                                string tempValue2 = null;
                                DataProcess.UpdateQuery(_connectionString, SqlgenerateForFixedAsset.UpdateQuery(tempValue2, itemCode, store));
                                
                            }
                        }
                    }
                    else
                    {
                        stdVal = amount;
                        latVal = amount;
                        avgVal = amount;
                        DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertItmStk(itemCode, Convert.ToDouble(quantity), "A", 0, 0, rate, rate, rate, "", "", "", 0));
                    }
                    #endregion

                    #region UpdateStkCtl
                    var dtStkCtl = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetStkCtlByItemStore(itemCode, store));
                       
                    if (dtStkCtl.Rows.Count > 0)
                    {
                        newCurStk = dtStkCtl.Rows[0]["Stk_Ctl_Cur_Stk"].ToString() == null ? 0 : Convert.ToDouble( dtStkCtl.Rows[0]["Stk_Ctl_Cur_Stk"].ToString()) + Convert.ToDouble(quantity);
                        double newFreeStk = dtStkCtl.Rows[0]["Stk_Ctl_Free_Stk"].ToString() == null
                                             ? 0
                                             : Convert.ToDouble( dtStkCtl.Rows[0]["Stk_Ctl_Free_Stk"].ToString()) + Convert.ToDouble(quantity);

                        DataProcess.UpdateQuery(_connectionString, SqlgenerateForFixedAsset.EditStkCtl("", newCurStk, newFreeStk, stdVal, avgVal, latVal,
                                            Convert.ToDateTime(txtReceiptDate.Text.Trim()), "", "", "", 0, itemCode,
                                            store));
                    }
                    else
                    {
                        string tempValue3 = null;
                        DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertStkCtl(store, itemCode, "", Convert.ToDouble(quantity), Convert.ToDouble(quantity), 0, 0, 0, 0, 0, 0, Convert.ToDecimal(amount), Convert.ToDecimal(amount),
                                              Convert.ToDecimal(amount), 0, 0, Convert.ToDateTime(txtReceiptDate.Text.Trim()), tempValue3, "", "",
                                              "", 0));
                        
                    }
                    #endregion

                    #region UpdateStkVal

                    var dtCtlStk = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetStkCtlByItemStore(itemCode, store));
                        
                    if (dtCtlStk.Rows.Count > 0)
                    {
                        curStk = Convert.ToInt32(dtCtlStk.Rows[0]["Stk_Ctl_Cur_Stk"].ToString() == null ? 0 : Convert.ToInt32( dtCtlStk.Rows[0]["Stk_Ctl_Cur_Stk"].ToString())); 
                        latRat = (dtCtlStk.Rows[0]["Stk_Ctl_Lat_Val"].ToString() == null ? 0 : Convert.ToDecimal( dtCtlStk.Rows[0]["Stk_Ctl_Lat_Val"].ToString())) / curStk;
                        avgRat = dtCtlStk.Rows[0]["Stk_Ctl_Ave_Val"].ToString() == null ? 0 : Convert.ToDecimal( dtCtlStk.Rows[0]["Stk_Ctl_Ave_Val"].ToString()) / curStk;
                        stdRat = dtCtlStk.Rows[0]["Stk_Ctl_Std_Val"].ToString() == null ? 0 : Convert.ToDecimal( dtCtlStk.Rows[0]["Stk_Ctl_Std_Val"].ToString()) / curStk;
                    }

                    var dtStkVal = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetMrrDataByMrrItem(MrrRefNo, itemCode, store));
                        
                    if (dtStkVal.Rows.Count > 0)
                    {
                        DataProcess.UpdateQuery(_connectionString, SqlgenerateForFixedAsset.UpdateStkVal(Convert.ToDateTime(txtReceiptDate.Text.Trim()), itemName,
                                              latRat, avgRat, stdRat,
                                              dtStkVal.Rows[0]["Stk_Val_Itm_Qty"].ToString() == null
                                                  ? 0
                                                  : Convert.ToDouble( dtStkVal.Rows[0]["Stk_Val_Itm_Qty"].ToString()) + Convert.ToDouble(quantity),
                                              curStk.ToString(), "", "", "", "RC", "PO", MrrRefNo, itemCode, store));
                    }
                    else
                    {
                        DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertStkVal("RC", "PO", MrrRefNo, Convert.ToDateTime(txtReceiptDate.Text.Trim()),
                                              itemCode,
                                              itemName, store, rate, avgRat, stdRat, Convert.ToDouble(quantity), curStk.ToString(), "",
                                              "",
                                              ""));
                    }
                    #endregion


                    #region NewAccDetDebit
                    TransactionDetailsDAO tdDao = new TransactionDetailsDAO();
                    var dtItemDet = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetItemByCode(itemCode));
                    tdDao.TrnAcCode = dtItemDet.Rows[0]["Itm_Det_Acc_code"].ToString();
                    tdDao.TrnAcDesc = DataProcess.GetSingleValueFromtable(_connectionString, "budg", "Gl_Coa_Name", "where Gl_Coa_Code='" + tdDao.TrnAcCode + "'");
                    tdDao.TrnAcType = DataProcess.GetSingleValueFromtable(_connectionString, "budg", "Gl_Coa_Type", "where Gl_Coa_Code='" + tdDao.TrnAcCode + "'");
                    tdDao.TrnAmount = Convert.ToDouble(amount);
                    tdDao.TrnLineNo = i.ToString();
                    tdDao.TrnMatch = "";
                    tdDao.TrnNarration = "purchase qty : " + quantity + " " + uom + " " + itemName + " from " + supplierName + " against " + PORef;
                    tdDao.TrnPaymentDATE = DateProcess.GetServerDate(_connectionString);
                    tdDao.TrnRefNo = "";
                    tdDao.TrnTrntype = "D";
                    tdDao.TrnDueDATE = tdDao.TrnPaymentDATE.AddDays(30);
                    tdDao.TrnChequeNo = "";
                    tdDao.TrnGRNNo = MrrRefNo;
                    tdDao.TrnDcNo = PORef;

                    tdDaolst.Add(tdDao);
                    #endregion
                }
                i++;
            }

            #region NewAccDetCredit
            if (HPFlag == "P")
            {
                TransactionDetailsDAO DaoDetCr = new TransactionDetailsDAO();

                DaoDetCr.TrnAcCode = supplierCode;
                DaoDetCr.TrnAcDesc = DataProcess.GetSingleValueFromtable(_connectionString, "budg", "Gl_Coa_Name", "where Gl_Coa_Code='" + DaoDetCr.TrnAcCode + "'");
                DaoDetCr.TrnAcType = DataProcess.GetSingleValueFromtable(_connectionString, "budg", "Gl_Coa_Type", "where Gl_Coa_Code='" + DaoDetCr.TrnAcCode + "'");
                DaoDetCr.TrnAmount = Convert.ToDouble(totAmount);
                DaoDetCr.TrnLineNo = i.ToString();
                DaoDetCr.TrnMatch = "";
                DaoDetCr.TrnNarration = "Provision made against store item " + CreditNarration + " against P/O " + PORef;
                DaoDetCr.TrnPaymentDATE = DateProcess.GetServerDate(_connectionString);
                DaoDetCr.TrnRefNo = "";
                DaoDetCr.TrnTrntype = "C";
                DaoDetCr.TrnDueDATE = DaoDetCr.TrnPaymentDATE.AddDays(30);
                DaoDetCr.TrnChequeNo = "";
                DaoDetCr.TrnGRNNo = MrrRefNo;

                tdDaolst.Add(DaoDetCr);
            }
            #endregion

            #region UpdateJvRefNo
            
            #endregion

            DateTime adjp=DateProcess.LastDateOfMonth(DaoHdr.TrnDATE);
            ReturnDberror = DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertPayableProgressInfo(MrrRefNo,adjp,DaoHdr.TrnEntryUser,DaoHdr.TrnEntryDATE));
            if (ReturnDberror == false)
                goto errorCode;

            myTran.Commit();

            lblMsgHdr.Text = HPFlag == "H" ? "Successfully Holded" : "Successfull Posted";
            tblPopUp.Rows[2].Cells[0].InnerText = "MRR Ref No";
            txtMrrRef.Text = MrrRefNo;
            txtMrrRef.Visible = true;
            ModalPopupExtender3.Show();
            ClearFieldData("P");
            GetAllPoList();
      

            //TransactionEntryBLL tBll = new TransactionEntryBLL();

            //string str = tBll.BookAccountDataOfMRR(_connectionString, DaoHdr, tdDaolst, false);

            //if (str != "")
            //{
            //    myTran.Commit();
            //    lblMsgHdr.Text = HPFlag == "H" ? "Successfully Holded" : "Successfull Posted";
            //    tblPopUp.Rows[2].Cells[0].InnerText = "MRR Ref No";
            //    txtMrrRef.Text = MrrRefNo;
            //    txtMrrRef.Visible = true;
            //    ModalPopupExtender3.Show();
            //    ClearFieldData("P");
            //    GetAllPoList();
            //    ReturnDberror = true;
            //    return true;
               
            //}
            //else
            //{
            //    myTran.Rollback();
            //    tblPopUp.Rows[2].Cells[0].InnerText = "";
            //    lblMsgHdr.Text = "Data Processing Error.";
            //    txtMrrRef.Visible = false;
            //    ModalPopupExtender3.Show();
            //    return false;        
                
            //}


        errorCode:
            if (ReturnDberror == false)
            {
                myTran.Rollback();
                tblPopUp.Rows[2].Cells[0].InnerText = "";
                lblMsgHdr.Text = "Data Processing Error.";
                txtMrrRef.Visible = false;
                ModalPopupExtender3.Show();
                return false;
            }       

        }      

        catch (Exception ex)
        {            
            myTran.Rollback();
            tblPopUp.Rows[2].Cells[0].InnerText = "";
            lblMsgHdr.Text = "Data Processing Error.";
            txtMrrRef.Visible = false;
            ModalPopupExtender3.Show();
            return false;
        }
        finally
        {
            conn.Close();           
        }

        return ReturnDberror;  

    }    

    protected void btnHold_Click(object sender, EventArgs e)
    {
        SaveData("H");
    }
    protected void gvPoDet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                var txtMrrqty = (TextBox)e.Row.FindControl("txtMrrQty");

                var poqty = Convert.ToDouble(e.Row.Cells[5].Text.ToString());
                if (poqty <= 0)
                {
                    e.Row.Cells[9].Enabled = false;
                    e.Row.Cells[9].Visible = false;
                    e.Row.BackColor = System.Drawing.Color.Red;

                    txtMrrqty.Enabled = false;  
                }

                var txtSerialNo = (TextBox)e.Row.FindControl("txtSerial");
                var dtChkserial = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetItemByCode(e.Row.Cells[1].Text.Trim()));
                if (dtChkserial.Rows.Count > 0)
                {
                    if (dtChkserial.Rows[0]["Itm_Det_Others1_flag"].ToString() == "Y")
                    {
                        txtSerialNo.Visible = true;
                    }
                    else
                    {
                        var dtItmSerial = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetSerialByItem(e.Row.Cells[1].Text.Trim()));
                        txtSerialNo.Visible = dtItmSerial.Rows.Count > 0;
                    }
                }
                else
                {
                    txtSerialNo.Visible = false;
                }
            }
            catch (Exception)
            {

                //throw;
            }
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        //var MrrRefNo = "";
        //try
        //{
        //    if (txtMrrSearch.Text.ToUpper() != "")
        //    {
        //        string[] temp = txtMrrSearch.Text.Split(':');
        //        if (temp.Length >= 1) MrrRefNo = temp[0];
        //    }
        //    Response.Redirect("~/ClientSide/Inventory/Report/frmMrrReport.aspx?MrrNo=" + MrrRefNo);
        //}
        //catch (Exception)
        //{

        //    //throw;
        //}
    }
    protected void btnShowPO_Click(object sender, EventArgs e)
    {
        try
        {
            var PORef = "";
            if (ddlpolist.Text.ToUpper() != "")
            {
                string[] temp = ddlpolist.Text.Split(':');
                if (temp.Length >= 1) PORef = temp[0];
            }
            var dtPoHdr = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetPOByRefNo(PORef));
            if (dtPoHdr.Rows.Count > 0)
            {
                var dtSupp = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetSupplierByCode(dtPoHdr.Rows[0]["PO_Hdr_Pcode"].ToString()));
                var supplier = dtPoHdr.Rows[0]["PO_Hdr_Pcode"].ToString() + ":" + dtSupp.Rows[0]["Par_Acc_Name"].ToString();
                txtSupplier.Text = supplier;
                txtSupplier.Enabled = false;

                txtReceiptDate.Text = dtPoHdr.Rows[0]["PO_Hdr_DATE"].ToString();
                txtInvoiceDate.Text = dtPoHdr.Rows[0]["PO_Hdr_DATE"].ToString();

                var dtPoDet = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetPendingPoData(PORef));
                
                LoadInitPoGrid();

                var dt = new DataTable();
                dt = (DataTable)ViewState["datatablePo"];

                foreach (DataRow dr in dtPoDet.Rows)
                {
                    dt.Rows.Add(dr["PO_Det_Lno"].ToString(),
                        dr["PO_Det_Icode"].ToString(),
                        dr["PO_Det_Itm_Desc"].ToString(),
                        dr["PO_Det_Itm_Uom"].ToString(),
                        dr["PO_Det_Str_Code"].ToString(),
                        dr["PO_Det_Bal_Qty"].ToString(),
                        dr["PO_Det_Lin_Rat"].ToString(),
                        dr["PO_Det_Bal_Qty"].ToString(),
                        dr["Po_Det_Serial_No"].ToString() 
                        );
                }

                ViewState["datatablePo"] = dt;
                SetPoGridData();
                ddlpolist.Enabled = false;
                btnSelectPo.Enabled = false;
                btnClearPo.Visible = true;
                btnClearPo.Enabled = true;
            }
        }
        catch (Exception)
        {

            //throw;
        }
    }
}
