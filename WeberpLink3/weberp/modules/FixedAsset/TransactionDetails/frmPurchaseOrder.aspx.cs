using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class modules_FixedAsset_TransactionDetails_frmPurchaseOrder : System.Web.UI.Page
{
    string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtSupplier.Focus();
            txtOrderDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDueDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtAmount.Text = "0";
            LoadInitGrid();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtSupplier.Text.Trim() == "")
        {
            lblMsgHdr.Text = "Enter Supplier first.";
            tblPopUp.Rows[2].Cells[0].InnerText = "";
            txtPoRef.Text = "";
            txtPoRef.Visible = false;
            ModalPopupExtender3.Show();
            return;
        }
        try
        {
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
            var storeCode = "";
            if (txtStore.Text.ToUpper() != "")
            {
                string[] temp = txtStore.Text.Split(':');
                if (temp.Length >= 1) storeCode = temp[0];
            }
            var dt = new DataTable();
            dt = (DataTable)ViewState["datatable"];
            dt.Rows.Add(itemCode, itemName, txtUom.Text.Trim(), storeCode, txtQuantity.Text.Trim(), txtRate.Text.Trim(),
                        Convert.ToDecimal(txtQuantity.Text.Trim()) * Convert.ToDecimal(txtRate.Text.Trim()));
            ViewState["datatable"] = dt;
            SetGridData();
            ClearFieldData("");
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
        dt.Columns.Add("Store", typeof(string));
        dt.Columns.Add("Quantity", typeof(string));
        dt.Columns.Add("Rate", typeof(string));
        dt.Columns.Add("Amount", typeof(string));
        ViewState["datatable"] = dt;
    }

    private void SetGridData()
    {
        var dt = new DataTable();
        dt = (DataTable)ViewState["datatable"];
        GridView1.DataSource = dt;
        GridView1.DataBind();
        if (GridView1.Rows.Count > 0)
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

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        var indx = GridView1.SelectedIndex;
        if (indx != -1)
        {
            var dt = new DataTable();
            dt = (DataTable)ViewState["datatable"];
            dt.Rows.RemoveAt(indx);
            ViewState["datatable"] = dt;
            SetGridData();
            GridView1.SelectedIndex = -1;
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;
        this.CollapsiblePanelExtenderHdr.Collapsed = true;
        this.CollapsiblePanelExtenderHdr.ClientState = "true";
        this.CollapsiblePanelExtenderDet.Collapsed = false;
        this.CollapsiblePanelExtenderDet.ClientState = "false";
        txtItem.Focus();
    }

    protected void txtQuantity_TextChanged(object sender, EventArgs e)
    {
        decimal qty = 0, rate = 0;
        qty = txtQuantity.Text == "" ? 0 : Convert.ToDecimal(txtQuantity.Text.Trim());
        rate = txtRate.Text == "" ? 0 : Convert.ToDecimal(txtRate.Text.Trim());
        txtAmount.Text = (qty * rate).ToString();
    }

    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        decimal qty = 0, rate = 0;
        qty = txtQuantity.Text == "" ? 0 : Convert.ToDecimal(txtQuantity.Text.Trim());
        rate = txtRate.Text == "" ? 0 : Convert.ToDecimal(txtRate.Text.Trim());
        txtAmount.Text = (qty * rate).ToString();
    }

    protected void txtItem_TextChanged(object sender, EventArgs e)
    {
        var itemCode = "";
        try
        {
            if (txtItem.Text.ToUpper() != "ALL")
            {
                string[] temp = txtItem.Text.Split(':');
                if (temp.Length >= 1) itemCode = temp[0];
            }
            if (itemCode != "")
            {
                var dtItem = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetItemByCode(itemCode));
                txtUom.Text = dtItem.Rows[0]["Itm_Det_stk_unit"].ToString();
                txtStore.Focus();
            }
        }
        catch (Exception)
        {

            //throw;
        }
    }

    private void ClearFieldData(string Pst_Flg)
    {
        if (Pst_Flg == "P" || Pst_Flg == "H")
        {
            lblEditFlag.Text = "N";
            txtPoSearch.Text = "";
            txtPoSearch.Enabled = true;
            btnSearch.Enabled = true;
            txtSupplier.Text = "";
            txtOrderDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDueDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtRemarks.Text = "";
        }
        txtItem.Text = "";
        txtUom.Text = "";
        txtStore.Text = "";
        txtQuantity.Text = "";
        txtRate.Text = "";
        txtAmount.Text = "0";
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            var PORef = "";
            if (txtPoSearch.Text.ToUpper() != "")
            {
                string[] temp = txtPoSearch.Text.Split(':');
                if (temp.Length >= 1) PORef = temp[0];
            }
            var dtPoHdr = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetPOByRefNo(PORef));
            if (dtPoHdr.Rows.Count > 0)
            {
                var dtSupp = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetSupplierByCode(dtPoHdr.Rows[0]["PO_Hdr_Pcode"].ToString()));
                var supplier = dtPoHdr.Rows[0]["PO_Hdr_Pcode"].ToString() + ":" + dtSupp.Rows[0]["Par_Acc_Name"].ToString();
                txtSupplier.Text = supplier;
                txtOrderDate.Text = (dtPoHdr.Rows[0]["PO_Hdr_DATE"].ToString()).ToString();
                txtDueDate.Text = dtPoHdr.Rows[0]["po_hdr_due_date"].ToString();
                txtRemarks.Text = dtPoHdr.Rows[0]["PO_Hdr_Com1"].ToString();
                var dt = new DataTable();
                LoadInitGrid();
                dt = (DataTable)ViewState["datatable"];
                var dtPoDet = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetPODetByRefNo(PORef));
                int i = 0;
                foreach (DataRow row in dtPoDet.Rows)
                {
                    dt.Rows.Add(dtPoDet.Rows[i]["PO_Det_Icode"].ToString(), dtPoDet.Rows[i]["PO_Det_Itm_Desc"].ToString(),
                                dtPoDet.Rows[i]["PO_Det_Itm_Uom"].ToString(),
                                dtPoDet.Rows[i]["PO_Det_Str_Code"].ToString(), dtPoDet.Rows[i]["PO_Det_Lin_Qty"].ToString(),
                                dtPoDet.Rows[i]["PO_Det_Lin_Rat"].ToString(),
                                dtPoDet.Rows[i]["PO_Det_Lin_Amt"].ToString());
                    ViewState["datatable"] = dt;
                    SetGridData();
                    i++;
                }
                txtPoSearch.Enabled = false;
                btnSearch.Enabled = false;
                btnClear.Visible = true;
                if (dtPoHdr.Rows[0]["PO_Hdr_HPC_Flag"].ToString() == "P")
                {
                    btnHold.Visible = false;
                    btnPost.Visible = false;
                    pHdrBody.Enabled = false;
                    pDetBody.Enabled = false;
                    GridView1.Columns[0].Visible = false;
                    GridView1.Enabled = false;
                }
                else
                {
                    btnHold.Visible = true;
                    btnPost.Visible = true;
                    pHdrBody.Enabled = true;
                    pDetBody.Enabled = true;
                    GridView1.Columns[0].Visible = true;
                    GridView1.Enabled = true;
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
        var PoRefNo = "";
        bool new_period = false;
        DateTime chkPeriod = DateTime.Now;
        var userId = Session[StaticData.sessionUserId].ToString();
        decimal totAmount = 0;
        string trnPeriod = Convert.ToDateTime(txtOrderDate.Text.Trim()).ToString("MM") + "/" + Convert.ToDateTime(txtOrderDate.Text.Trim()).ToString("yyyy");
        SqlConnection conn = new SqlConnection(_connectionString);
        SqlTransaction tran = conn.BeginTransaction();
        conn.Open();
        try
        {
            var supplierCode = "";
            if (txtSupplier.Text.ToUpper() != "")
            {
                string[] temp = txtSupplier.Text.Split(':');
                if (temp.Length >= 1) supplierCode = temp[0];
            }
            #region GetTotalAmount
            var dtTot = new DataTable();
            dtTot = (DataTable)ViewState["datatable"];
            foreach (DataRow row in dtTot.Rows)
            {
                totAmount = (totAmount + Convert.ToDecimal(row["Amount"].ToString()));
            }
            #endregion
            var dtGetTranPerm = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetTranByTypeCode("RC", "PO", "ADM"));
            if (dtGetTranPerm.Rows.Count > 0)
            {
                if (lblEditFlag.Text == "N")
                {
                    #region GetNewPoRef

                    mn = Convert.ToDateTime(txtOrderDate.Text.Trim()).ToString("MM");
                    yr = Convert.ToDateTime(txtOrderDate.Text.Trim()).ToString("yy");
                    var Spr = dtGetTranPerm.Rows[0]["Trn_Set_Ord_Pfix"].ToString().Trim();
                    prefix = Spr + yr + mn;
                    chkPeriod = Convert.ToInt32(mn) < 7 ? Convert.ToDateTime("01/07/" + (Convert.ToInt32(yr) - 1)) : Convert.ToDateTime("01/07/" + yr);
                    var dtRefNo = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetMaxPoNo(Convert.ToDateTime(chkPeriod)));
                    var nextRefNo = (dtRefNo.Rows[0][0].ToString() == null || Convert.ToInt32(dtRefNo.Rows[0][0].ToString()) == 0) ? 1 : Convert.ToInt32(dtRefNo.Rows[0][0].ToString()) + 1;
                    PoRefNo = prefix + nextRefNo.ToString("000000");
                    #endregion
                    #region InsertPoHdr
                    DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertPoHdr("PO", "PO", PoRefNo, supplierCode, supplierCode,
                                        supplierCode, Convert.ToDateTime(txtOrderDate.Text.Trim()),
                                        txtRemarks.Text.Trim(), "", "", "", "", "", "", "", "", "",
                                        totAmount, HPFlag, trnPeriod, "SUB",
                                        Convert.ToDateTime(txtOrderDate.Text.Trim()), "", "", "",
                                        Convert.ToDateTime(txtDueDate.Text.Trim()), trnPeriod, "", "", "", 0,
                                        "BDT", 0));
                    #endregion
                }
                else
                {
                    #region GetSrchPoRef
                    if (txtPoSearch.Text.ToUpper() != "")
                    {
                        string[] temp = txtPoSearch.Text.Split(':');
                        if (temp.Length >= 1) PoRefNo = temp[0];
                    }
                    #endregion
                    #region EditPoHdr
                    DataProcess.UpdateQuery(_connectionString, SqlgenerateForFixedAsset.EditPoHdr(supplierCode, supplierCode, supplierCode, Convert.ToDateTime(txtOrderDate.Text.Trim()), txtRemarks.Text.Trim(), "",
                                      "", "", "", "", "", "", "", "", totAmount, HPFlag, trnPeriod, "SUB",
                                      Convert.ToDateTime(txtOrderDate.Text.Trim()), "", "", "",
                                      Convert.ToDateTime(txtDueDate.Text.Trim()), trnPeriod, "", "", "", 0, "BDT",
                                      0, PoRefNo));
                    #endregion
                    #region DeletePoDet
                    DataProcess.DeleteQuery(_connectionString, SqlgenerateForFixedAsset.DeletePoDet(PoRefNo));
                    #endregion
                }
                #region InsertPoDet
                var dt = new DataTable();
                dt = (DataTable)ViewState["datatable"];
                Int16 i = 1;
                foreach (DataRow row in dt.Rows)
                {
                    DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertPoDet("PO", "PO", PoRefNo,
                                        i, "", 0,
                                        row["Item Code"].ToString(), row["Item Name"].ToString(), row["UOM"].ToString(),
                                        row["Store"].ToString(), "", "", 0, "",
                                        Convert.ToDateTime(txtOrderDate.Text.Trim()),
                                        Convert.ToDateTime(txtDueDate.Text.Trim()),
                                        Convert.ToDouble(row["Quantity"].ToString()), 0,
                                        Convert.ToDouble(row["Quantity"].ToString()), 0, "O", "N",
                                        Convert.ToDecimal(row["Rate"].ToString()),
                                        Convert.ToDecimal(row["Quantity"].ToString()) *
                                        Convert.ToDecimal(row["Rate"].ToString()),
                                        Convert.ToDecimal(row["Quantity"].ToString()) *
                                        Convert.ToDecimal(row["Rate"].ToString()),
                                        "", "", "", 0));
                    i++;
                }
                #endregion
            }
            tran.Commit();
            lblMsgHdr.Text = HPFlag == "H" ? "Successfully Holded" : "Successfull Posted";
            tblPopUp.Rows[2].Cells[0].InnerText = "Purchase Order No";
            txtPoRef.Visible = true;
            txtPoRef.Text = PoRefNo;
            ModalPopupExtender3.Show();
            this.CollapsiblePanelExtenderHdr.Collapsed = false;
            this.CollapsiblePanelExtenderHdr.ClientState = "false";
            this.CollapsiblePanelExtenderDet.Collapsed = true;
            this.CollapsiblePanelExtenderDet.ClientState = "true";
            ClearFieldData(HPFlag);
            LoadInitGrid();
            SetGridData();
            return true;
        }
        catch (Exception ex)
        {
            tran.Rollback();
            tblPopUp.Rows[2].Cells[0].InnerText = "";
            lblMsgHdr.Text = "Data Processing Error.";
            ModalPopupExtender3.Show();
            return false;
            //throw;
        }
        finally
        {
            conn.Close();
        }
    }

    protected void btnHold_Click(object sender, EventArgs e)
    {
        SaveData("H");
    }

    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        var supplierCode = "";
        try
        {
            if (txtSupplier.Text.ToUpper() != "")
            {
                string[] temp = txtSupplier.Text.Split(':');
                if (temp.Length >= 1) supplierCode = temp[0];
            }
            var dtSupp = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetSupplierByCode(supplierCode));
            args.IsValid = dtSupp.Rows.Count > 0;
        }
        catch (Exception)
        {
            args.IsValid = false;
            //throw;
        }
    }

    protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
    {
        var itemCode = "";
        try
        {
            if (txtItem.Text.ToUpper() != "")
            {
                string[] temp = txtItem.Text.Split(':');
                if (temp.Length >= 1) itemCode = temp[0];
            }
            var dtItem = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetItemByCode(itemCode));
            args.IsValid = dtItem.Rows.Count > 0;
        }
        catch (Exception)
        {
            args.IsValid = false;
            //throw;
        }
    }

    protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
    {
        var storeCode = "";
        try
        {
            if (txtStore.Text.ToUpper() != "")
            {
                string[] temp = txtStore.Text.Split(':');
                if (temp.Length >= 1) storeCode = temp[0];
            }
            var dtStore = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetStoreByCode(storeCode));
            args.IsValid = dtStore.Rows.Count > 0;
        }
        catch (Exception)
        {
            args.IsValid = false;
            //throw;
        }
    }

    protected void txtStore_TextChanged(object sender, EventArgs e)
    {
        if (txtStore.Text != "") txtQuantity.Focus();
    }
}