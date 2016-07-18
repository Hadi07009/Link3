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
using System.Windows.Forms;

public partial class frmStoreRequisition : System.Web.UI.Page
{
    string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadCombo();
         
            txtRequiredDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            AutoCompleteExtenderReqBy.ContextKey = txtRequiredDate.Text.Trim();
            autoComplete1.ContextKey = _connectionString;
            txtStore_AutoCompleteExtender.ContextKey = _connectionString;
            txtSrSearch_AutoCompleteExtenderSr.ContextKey = _connectionString;
            LoadInitGrid();
            Session[StaticData.sessionUserId] = "";            
        }
    }

    private void LoadCombo()
    {
        try
        {
            var dtInsuTrnSet = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetTrnByType("IS", "ADM"));
            cboReqType.DataSource = dtInsuTrnSet;
            cboReqType.DataTextField = "Trn_Set_Tr_Name";
            cboReqType.DataValueField = "Trn_Set_IQ_Pfix";
            cboReqType.DataBind();
            cboReqType.Items.Insert(0, "------Select-------");

            var dtIssueType = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetDataIssueType());
            cboIssueType.DataSource = dtIssueType;
            cboIssueType.DataTextField = "Iss_Type_Name";
            cboIssueType.DataValueField = "Iss_Type_Id";
            cboIssueType.DataBind();
            cboIssueType.Items.Insert(0, "------Select-------");


            var dtPriority = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetDataPriority());
            cboPriority.DataSource = dtPriority;
            cboPriority.DataTextField = "Priority_Name";
            cboPriority.DataValueField = "Priority_Id";
            cboPriority.DataBind();
            cboPriority.Items.Insert(0, "------Select-------");
            cboPriority.SelectedIndex = 2;
        }
        catch (Exception e)
        {
 
        }
 
    }
        

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable dmn = new DataTable();
        lblerrormsg.Visible = false;
        
        try
        {
            var itemCode = "";
            var itemName = "";
            var storeCode = "";
            if (txtItem.Text != "")
            {
                string[] temp = txtItem.Text.Split(':');
                if (temp.Length >= 1)
                {
                    itemCode = temp[0];
                    itemName = temp[1];
                }
            }
            if (txtStore.Text != "")
            {
                string[] temp = txtStore.Text.Split(':');
                if (temp.Length >= 1) storeCode = temp[0];
            }
          
            DataTable dt = (DataTable)ViewState["datatable"];

            decimal latRate = 0;
            double amount = 0;
            var dtItmStk = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetItmStkByICode(itemCode));
            if (dtItmStk.Rows.Count > 0)
            {
                latRate = dtItmStk.Rows[0]["Itm_Stk_LAT_Rat"].ToString() == null ? 0 : Convert.ToDecimal( dtItmStk.Rows[0]["Itm_Stk_LAT_Rat"].ToString());
            }
            amount = Convert.ToDouble(txtQuantity.Text.Trim()) * Convert.ToDouble(latRate);
            dt.Rows.Add(itemCode, itemName, txtUom.Text.Trim(), storeCode, txtQuantity.Text.Trim(), latRate, amount,
                        txtCurrentStock.Text.Trim());
            ViewState["datatable"] = dt;
            SetGridData();
            dmn = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetDataByItemCode(itemCode, storeCode));
            if (dmn.Rows.Count > 0)
            {
                int mst = Convert.ToInt32( dmn.Rows[0]["MSL"].ToString());
                string fm = dmn.Rows[0]["MslTouchedDate"].ToString() == null ? "NA" : dmn.Rows[0]["MslTouchedDate"].ToString();
                int tost = Convert.ToInt32(txtCurrentStock.Text) + Convert.ToInt32(txtQuantity.Text);
                if (fm == "NA")
                {
                    if (mst >= tost)
                    {
                        DataProcess.UpdateQuery(_connectionString, SqlgenerateForFixedAsset.UpdateQuery(DateTime.Now.Date.ToString(), itemCode, storeCode));
                    }
                }
            }

            ClearFieldData("");

            txtItem.Focus();
        }
        catch (Exception ex)
        {
            MessageBox.Show("ERROR :" + ex.Message);
            return;
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
        dt.Columns.Add("Current Stock", typeof(string));
        ViewState["datatable"] = dt;
    }

    private void SetGridData()
    {
        var dt = new DataTable();
        dt = (DataTable)ViewState["datatable"];
        gvSR.DataSource = dt;
        gvSR.DataBind();

        if (gvSR.Columns.Count > 8)
        {
            gvSR.Columns[6].Visible = false;
            gvSR.Columns[7].Visible = false;
            gvSR.Columns[8].Visible = false;
        }
        else
        {
            if (gvSR.HeaderRow != null)
            {
                if (gvSR.HeaderRow.Cells.Count >= 8)
                {
                    gvSR.HeaderRow.Cells[6].Visible = false;
                    gvSR.HeaderRow.Cells[7].Visible = false;
                    gvSR.HeaderRow.Cells[8].Visible = false;
                    foreach (GridViewRow gvr in gvSR.Rows)
                    {
                        gvr.Cells[6].Visible = false;
                        gvr.Cells[7].Visible = false;
                        gvr.Cells[8].Visible = false;
                    }
                }
            }
        }


        if (gvSR.Rows.Count > 0)
        {
            //btnHold.Visible = true;
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
        var indx = gvSR.SelectedIndex;
        if (indx != -1)
        {
            var dt = new DataTable();
            dt = (DataTable)ViewState["datatable"];
            dt.Rows.RemoveAt(indx);
            ViewState["datatable"] = dt;
            SetGridData();
            gvSR.SelectedIndex = -1;
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
        lblEditFlag.Text = "N";

        if (Pst_Flg == "P")
        {
            txtSrSearch.Text = "";
            txtSrSearch.Enabled = true;
            txtReqFor.Text = "";
            txtLocation.Text = "";
            txtReqBy.Text = "";
            txtRequiredDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtRemarks.Text = "";
            btnPrint.Visible = false;
            cboReqType.SelectedIndex = 0;
            cboIssueType.SelectedIndex = 0;
            cboPriority.SelectedIndex = 2;
            pHdrBody.Enabled = true;
            pDetBody.Enabled = true;
            LoadInitGrid();
            SetGridData();
        }

        txtStore.Text = "";
        txtCurrentStock.Text = "";
        txtItem.Text = "";
        txtUom.Text = "";
        txtQuantity.Text = "";
        txtSrSearch.Text = "";

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            var SrRef = "";
            if (txtSrSearch.Text.ToUpper() != "")
            {
                string[] temp = txtSrSearch.Text.Split(':');
                if (temp.Length >= 1) SrRef = temp[0];
            }
            var dtSrHdr = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetSrHdrByRefNo(SrRef));
            if (dtSrHdr.Rows.Count > 0)
            {
                lblEditFlag.Text = "Y";
                btnClear.Visible = true;
                btnClear.Enabled = true;
                txtSrSearch.Enabled = false;
                btnPrint.Visible = true;
                cboReqType.SelectedValue = dtSrHdr.Rows[0]["Sr_Hdr_Code"].ToString();
                AutoCompleteExtenderLoc.ContextKey = cboReqType.SelectedValue + ":" + (dtSrHdr.Rows[0]["Sr_Hdr_Pcode"].ToString() == null ? "" : dtSrHdr.Rows[0]["Sr_Hdr_Pcode"].ToString()) + ":" + (dtSrHdr.Rows[0]["Sr_Hdr_St_DATE"].ToString() == null ? DateTime.Now.ToString() : dtSrHdr.Rows[0]["Sr_Hdr_St_DATE"].ToString());

                #region GetReFor
                var client = "";
                if (dtSrHdr.Rows[0]["Sr_Hdr_Code"].ToString().Trim() != "SRO")
                {
                    var dtClient = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetClientByCode(dtSrHdr.Rows[0]["Sr_Hdr_Pcode"].ToString() == null ? "" : dtSrHdr.Rows[0]["Sr_Hdr_Pcode"].ToString()));
                    if (dtClient.Rows.Count > 0)
                    {
                        client = dtClient.Rows[0]["Par_Acc_Code"].ToString() + ":" + dtClient.Rows[0]["Par_Acc_Name"].ToString();
                    }
                    txtReqFor.Text = client;
                }
                else
                {
                    var dtDptLoc = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetDeptByCode(dtSrHdr.Rows[0]["Sr_Hdr_Pcode"].ToString() == null ? "" : dtSrHdr.Rows[0]["Sr_Hdr_Pcode"].ToString()));
                    if (dtDptLoc.Rows.Count > 0)
                    {
                        client = dtDptLoc.Rows[0]["Ccg_Code"].ToString().Trim() + ":" + dtDptLoc.Rows[0]["Ccg_Name"].ToString().Trim();
                    }
                    txtReqFor.Text = client;
                }
                #endregion

                #region LocationID
                var locationId = "";
                if (dtSrHdr.Rows[0]["Sr_Hdr_Code"].ToString() == "SR")
                {
                    var dtClient = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetClientAddrByCode(dtSrHdr.Rows[0]["Sr_Hdr_Acode"].ToString() == null ? "" : dtSrHdr.Rows[0]["Sr_Hdr_Acode"].ToString()));
                    locationId = dtSrHdr.Rows[0]["Sr_Hdr_Acode"].ToString() + ":" + dtClient.Rows[0]["par_adr_name"].ToString();
                }

                if (dtSrHdr.Rows[0]["Sr_Hdr_Code"].ToString() == "SRB")
                {
                    var dtBackbone = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.CheckDuplicateData(dtSrHdr.Rows[0]["T_C1"].ToString() == null ? "" : dtSrHdr.Rows[0]["T_C2"].ToString(),
                                                                     dtSrHdr.Rows[0]["Sr_Hdr_Acode"].ToString() == null
                                                                         ? ""
                                                                         : dtSrHdr.Rows[0]["Sr_Hdr_Acode"].ToString()));
                    locationId = dtBackbone.Rows[0]["Grp_Code_Id"].ToString() + ":" + dtSrHdr.Rows[0]["Sr_Hdr_Acode"].ToString() + ":" +
                                 dtBackbone.Rows[0]["Grp_Code_Name"].ToString();
                }

                if (dtSrHdr.Rows[0]["Sr_Hdr_Code"].ToString() == "SRO")
                {
                    Recordset rs = new Recordset();
                    Connection DC = new Connection();
                    string constr, str;
                    constr = System.Configuration.ConfigurationManager.AppSettings["Wfa2ConStr"].ToString();
                    DC.Open(constr, null, null, 0);
                    str = "WITH OwnUseLoc (LocID,LocName) as (select distinct [userid],[user_name] from tbl_user_info " +
                          "Where [userid]='" + (dtSrHdr.Rows[0]["Sr_Hdr_Acode"].ToString() == null ? "" : dtSrHdr.Rows[0]["Sr_Hdr_Acode"].ToString()) + "' union all select distinct department_code,department from WFA2.dbo.tbl_user_info " +
                          "where department_code='" + (dtSrHdr.Rows[0]["Sr_Hdr_Acode"].ToString() == null ? "" : dtSrHdr.Rows[0]["Sr_Hdr_Acode"].ToString()) + "') " +
                          "Select LocID,LocName from OwnUseLoc";
                    rs.Open(str, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    if (!rs.EOF)
                    {
                        locationId = rs.Fields[0].Value + ":" + rs.Fields[1].Value;
                    }
                    rs.Close();
                    DC.Close();
                }

                if (dtSrHdr.Rows[0]["Sr_Hdr_Code"].ToString() == "SRS")
                {
                    Recordset rsSale = new Recordset();
                    Connection DCSale = new Connection();
                    string constr, str;
                    constr = System.Configuration.ConfigurationManager.AppSettings["L3TConnStr"].ToString();
                    DCSale.Open(constr, null, null, 0);
                    str = "SELECT Gl_Coa_Code,Gl_Coa_Name FROM FA_GL_COA WHERE Gl_Coa_Code='" + (dtSrHdr.Rows[0]["Sr_Hdr_Acode"].ToString() == null ? "" : dtSrHdr.Rows[0]["Sr_Hdr_Acode"].ToString()) + "'";

                    rsSale.Open(str, DCSale, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    if (!rsSale.EOF)
                    {
                        locationId = rsSale.Fields[0].Value + ":" + rsSale.Fields[1].Value;
                    }
                    rsSale.Close();
                    DCSale.Close();
                }

                txtLocation.Text = locationId;
                #endregion

                cboIssueType.SelectedValue = dtSrHdr.Rows[0]["T_C1"].ToString();

                var reqBy = "";
                var dtEmp = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetDataById(dtSrHdr.Rows[0]["Sr_Hdr_Req_By"].ToString() == null ? "" : dtSrHdr.Rows[0]["Sr_Hdr_Req_By"].ToString()));
                if (dtEmp.Rows.Count > 0)
                {
                    reqBy = dtEmp.Rows[0]["userid"].ToString() + ":" + dtEmp.Rows[0]["user_name"].ToString();
                }
                txtReqBy.Text = reqBy;

                txtTkiNo.Text = dtSrHdr.Rows[0]["Sr_Hdr_Com2"].ToString();

                txtRequiredDate.Text = dtSrHdr.Rows[0]["Sr_Hdr_St_DATE"].ToString() == null
                                           ? DateTime.Now.ToString("dd/MM/yyyy")
                                           : dtSrHdr.Rows[0]["Sr_Hdr_St_DATE"].ToString();
                cboPriority.SelectedValue = dtSrHdr.Rows[0]["Sr_Hdr_Priority"].ToString();
                txtRemarks.Text = dtSrHdr.Rows[0]["Sr_Hdr_Com1"].ToString();

                var dt = new DataTable();
                LoadInitGrid();
                dt = (DataTable)ViewState["datatable"];
                var dtSrDet = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetSrDetByRefNo(SrRef));
                int i = 0;
                foreach (DataRow row in dtSrDet.Rows)
                {
                    dt.Rows.Add(dtSrDet.Rows[i]["Sr_Det_Icode"].ToString(), dtSrDet.Rows[i]["Sr_Det_Itm_Desc"].ToString(), dtSrDet.Rows[i]["Sr_Det_Itm_Uom"].ToString(),
                                dtSrDet.Rows[i]["Sr_Det_Str_Code"].ToString(), dtSrDet.Rows[i]["Sr_Det_Lin_Qty"].ToString(), dtSrDet.Rows[i]["Sr_Det_Lin_Rat"].ToString(),
                                dtSrDet.Rows[i]["Sr_Det_Lin_Amt"].ToString(), dtSrDet.Rows[i]["Sr_Det_Cur_Stk"].ToString());
                    ViewState["datatable"] = dt;
                    SetGridData();
                    i++;
                }

                if (dtSrHdr.Rows[0]["Sr_Hdr_HPC_Flag"].ToString() == "P")
                {
                    btnHold.Visible = false;
                    btnPost.Visible = false;
                    pHdrBody.Enabled = false;
                    pDetBody.Enabled = false;
                    gvSR.Columns[0].Visible = false;
                    gvSR.Enabled = false;
                }
                else
                {
                    //btnHold.Visible = true;
                    btnPost.Visible = true;
                    pHdrBody.Enabled = true;
                    pDetBody.Enabled = true;
                    gvSR.Columns[0].Visible = true;
                    gvSR.Enabled = true;
                }
                this.CollapsiblePanelExtenderHdr.Collapsed = false;
                this.CollapsiblePanelExtenderHdr.ClientState = "false";
                this.CollapsiblePanelExtenderDet.Collapsed = true;
                this.CollapsiblePanelExtenderDet.ClientState = "true";
            }
        }
        catch (Exception ex)
        {
            //throw;
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearFieldData("P");        
    }

    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
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
            
        }
    }

    protected void CustomValidator4_ServerValidate(object source, ServerValidateEventArgs args)
    {
        Recordset rs = new Recordset();
        Connection DC = new Connection();
        string constr, str;
        var empCode = "";
        try
        {
            if (txtReqBy.Text.ToUpper() != "")
            {
                string[] temp = txtReqBy.Text.Split(':');
                if (temp.Length >= 1) empCode = temp[0];
            }

            constr = _connectionString;
            DC.Open(constr, null, null, 0);
            var empId = empCode;
            str = "select [userid],[user_name] from tbl_user_info Where [userid] ='" + empId + "' and ([status]=1  or resign_date>=Convert(datetime,'" + txtRequiredDate.Text.Trim() + "',103)) ";
            rs.Open(str, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);
            var reqBy = "";
            args.IsValid = rs.RecordCount > 0;
            rs.Close();
            DC.Close();
        }
        catch (Exception)
        {
            args.IsValid = false;
            
        }
    }

    protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
    {
        Recordset rs = new Recordset();
        Connection DC = new Connection();
        string constr, str;
        var clientCode = "";
        try
        {
            if (txtReqFor.Text.ToUpper() != "")
            {
                string[] temp = txtReqFor.Text.Split(':');
                if (temp.Length >= 1) clientCode = temp[0];
            }

            constr = _connectionString;
            DC.Open(constr, null, null, 0);
            if (cboReqType.SelectedValue == "SRO")
            {
                str = "select Distinct [Ccg_Code],[Ccg_Name] from  FA_COM_CCG Where [Ccg_Cost_Id]='T02' and Ccg_Code='" + clientCode + "'";
            }
            else
            {
                str = "select Distinct Par_Acc_Code,Par_Acc_Name from  SaMa_Par_Acc Where Par_Acc_Code='" + clientCode + "'";
            }
            rs.Open(str, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);
            args.IsValid = !rs.EOF;

            rs.Close();
            DC.Close();
        }
        catch (Exception)
        {
            args.IsValid = false;
            
        }
    }

    protected void txtStore_TextChanged(object sender, EventArgs e)
    {
        try
        {
            var itemCode = "";
            if (txtItem.Text.ToUpper() != "ALL")
            {
                string[] temp = txtItem.Text.Split(':');
                if (temp.Length >= 1) itemCode = temp[0];
            }

            var storeCode = "";
            if (txtStore.Text.ToUpper() != "ALL")
            {
                string[] temp = txtStore.Text.Split(':');
                if (temp.Length >= 1) storeCode = temp[0];
            }
            if (itemCode != "" && storeCode != "")
            {
                var dtStkCtl = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetStkCtlByItemStore(itemCode, storeCode));
                txtCurrentStock.Text = dtStkCtl.Rows.Count > 0
                                           ? (dtStkCtl.Rows[0]["Stk_Ctl_Cur_Stk"].ToString() == null
                                                  ? "0"
                                                  : dtStkCtl.Rows[0]["Stk_Ctl_Cur_Stk"].ToString())
                                           : "0";


                txtQuantity.Focus();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("ERROR :" + ex.Message);

            
        }        
    }

    private bool SaveData(string HPFlag)
    {
        if (!Page.IsValid) return false;

        string prefix = "", yr = "", mn = "";
        bool new_period = false;
        string SrRefNo = "", reqFor = "", locGrp = "", locId = "", empId = "";
        DateTime chkPeriod = DateTime.Now;
        string trnPeriod = Convert.ToDateTime(txtRequiredDate.Text.Trim()).ToString("MM") + "/" + Convert.ToDateTime(txtRequiredDate.Text.Trim()).ToString("yyyy");
        SqlConnection conn = new SqlConnection(_connectionString);
        conn.Open();
        SqlTransaction myTran = conn.BeginTransaction();
        try
        {
            #region GetReqFor
            if (txtReqFor.Text.ToUpper() != "")
            {
                string[] temp = txtReqFor.Text.Split(':');
                if (temp.Length >= 1) reqFor = temp[0];
            }

            #endregion

            #region GetLocId
            if (txtLocation.Text.ToUpper() != "")
            {
                string[] temp = txtLocation.Text.Split(':');
                if (temp.Length >= 1)
                {
                    if (cboReqType.SelectedValue == "SRB")
                    {
                        locGrp = temp[0];
                        locId = temp[1];
                    }
                    else
                    {
                        locGrp = temp[1];
                        locId = temp[0];
                    }
                }
            }
            #endregion

            #region GetEmployeeId
            if (txtReqBy.Text.ToUpper() != "")
            {
                string[] temp = txtReqBy.Text.Split(':');
                if (temp.Length >= 1) empId = temp[0];
            }
            #endregion

            if (lblEditFlag.Text == "N")
            {
                #region GetNewSrRefNo
                var dtGetTranPerm = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetTranByTypeCode("IS", cboReqType.SelectedValue, "ADM"));
                if (dtGetTranPerm.Rows.Count > 0)
                {
                    mn = Convert.ToDateTime(txtRequiredDate.Text.Trim()).ToString("MM");
                    yr = Convert.ToDateTime(txtRequiredDate.Text.Trim()).ToString("yy");

                    var Spr = dtGetTranPerm.Rows[0]["Trn_Set_IQ_Pfix"].ToString().Trim();

                    prefix = Spr + yr + mn;

                    chkPeriod = Convert.ToInt32(mn) < 7 ? Convert.ToDateTime("01/07/" + (Convert.ToInt32(yr) - 1)) : Convert.ToDateTime("01/07/" + yr);

                }

                var dtRefNo = DataProcess.GetSingleValueFromtable(_connectionString, SqlgenerateForFixedAsset.GetMaxSrRefNo(Convert.ToDateTime(chkPeriod)));
                var nextRefNo = (dtRefNo == null || dtRefNo == "") ? 1 : Convert.ToInt32(dtRefNo) + 1;
                SrRefNo = prefix + nextRefNo.ToString("000000");
                #endregion

                #region InsertSrHdr
                DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertSrHdr("IS", cboReqType.SelectedValue, SrRefNo, reqFor,
                                    reqFor, locId, empId,
                                    Convert.ToDateTime(DateTime.Now),
                                    Convert.ToDateTime(txtRequiredDate.Text.Trim()), txtRemarks.Text.Trim(), txtTkiNo.Text.Trim(), "", "",
                                    "", "", "", "", "", "", 0, cboPriority.SelectedValue, "N", "", HPFlag, trnPeriod, "SUB", "",
                                    "", cboIssueType.SelectedValue, locGrp, "", 0));
                #endregion
            }
            else
            {
                #region GetSrchSrRefNo
                if (txtSrSearch.Text.ToUpper() != "")
                {
                    string[] temp = txtSrSearch.Text.Split(':');
                    if (temp.Length >= 1) SrRefNo = temp[0];
                }
                #endregion

                #region EditSrHdr
                DataProcess.UpdateQuery(_connectionString, SqlgenerateForFixedAsset.EditSrHdr(reqFor, reqFor, locId,
                                  empId, Convert.ToDateTime(DateTime.Now),
                                  Convert.ToDateTime(txtRequiredDate.Text.Trim()), txtRemarks.Text.Trim(), txtTkiNo.Text.Trim(), "", "",
                                  "", "", "", "", "", "", 0, cboPriority.SelectedValue, "N", "", HPFlag, trnPeriod, "SUB", "",
                                  "", cboIssueType.SelectedValue, locGrp, "", 0, SrRefNo));
                #endregion

                #region DeleteSrDet
                DataProcess.DeleteQuery(_connectionString, SqlgenerateForFixedAsset.DeleteSrDetByRefNo(SrRefNo));
                #endregion
            }

            var dt = new DataTable();
            dt = (DataTable)ViewState["datatable"];
            short i = 1;
            foreach (DataRow row in dt.Rows)
            {
                var itemCode = row["Item Code"].ToString();
                var itemName = row["Item Name"].ToString();
                var uom = row["UOM"].ToString();
                var store = row["Store"].ToString();
                var quantity = Convert.ToDouble(row["Quantity"].ToString());
                var rate = Convert.ToDecimal(row["Rate"].ToString());
                var amount = Convert.ToDecimal(row["Amount"].ToString());
                var curStk = Convert.ToDouble(row["Current Stock"].ToString());

                DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertSrDet("IS", cboReqType.SelectedValue, SrRefNo, i, "", 0, itemCode, itemName, uom,
                                    store, "", "", Convert.ToDateTime(txtRequiredDate.Text.Trim()), quantity, 0,
                                    quantity, 0, "C",
                                    "N", rate, amount, amount, "", "N", "", cboPriority.SelectedValue, "", "N",
                                    "", curStk, "", "",
                                    "", 0));
                i++;
            }
            myTran.Commit();
            lblMsgHdr.Text = HPFlag == "H" ? "Successfully Holded" : "Successfull Posted";
            tblPopUp.Rows[2].Cells[0].InnerText = "Store Requisition No";
            txtSrRef.Visible = true;
            txtSrRef.Text = SrRefNo;
            ModalPopupExtender3.Show();
            this.CollapsiblePanelExtenderHdr.Collapsed = false;
            this.CollapsiblePanelExtenderHdr.ClientState = "false";
            this.CollapsiblePanelExtenderDet.Collapsed = true;
            this.CollapsiblePanelExtenderDet.ClientState = "true";
            ClearFieldData("P");
            return true;
        }
        catch (Exception ex)
        {
            myTran.Rollback();
            tblPopUp.Rows[2].Cells[0].InnerText = "";
            lblMsgHdr.Text = "Data Processing Error.";
            ModalPopupExtender3.Show();
            return false;
        }
        finally
        {
            conn.Close();
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        var SrRefNo = "";
        try
        {
            if (txtSrSearch.Text.ToUpper() != "")
            {
                string[] temp = txtSrSearch.Text.Split(':');
                if (temp.Length >= 1) SrRefNo = temp[0];
            }
            Response.Redirect("~/modules/FixedAsset/Report/frmSrReport.aspx?SrNo=" + SrRefNo);
        }
        catch (Exception)
        {

            //throw;
        } 
    }
    protected void btnHold_Click(object sender, EventArgs e)
    {
        SaveData("H");
    }
    protected void txtReqFor_TextChanged(object sender, EventArgs e)
    {
        try
        {
            var trnType = "";
            var accCode = "";
            trnType = cboReqType.SelectedValue;
            if (txtReqFor.Text.Trim() != "")
            {
                string[] temp = txtReqFor.Text.Trim().Split(':');
                if (temp.Length >= 1) accCode = temp[0];
            }
            AutoCompleteExtenderLoc.ContextKey = trnType + ":" + accCode + ":" + txtRequiredDate.Text.Trim();
            if (trnType == "SR")
            {
                var dtClientAdr = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetClientAdrByAccCode(accCode));
                if (dtClientAdr.Rows.Count == 1)
                {
                    txtLocation.Text = dtClientAdr.Rows[0]["Par_Adr_Code"].ToString() + ":" + dtClientAdr.Rows[0]["par_adr_name"].ToString();
                    cboIssueType.Focus();
                }
                else
                {
                    txtLocation.Focus();
                }
            }
            else
            {
                txtLocation.Focus();
            }
        }
        catch (Exception)
        {

            //throw;
        }        
    }
    protected void cboReqType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var trnType = "";
            var accCode = "";
            trnType = cboReqType.SelectedValue;
            if (txtReqFor.Text.Trim() != "")
            {
                string[] temp = txtReqFor.Text.Trim().Split(':');
                if (temp.Length >= 1) accCode = temp[0];
            }
            AutoCompleteExtenderClient.ContextKey = cboReqType.SelectedValue;
            AutoCompleteExtenderLoc.ContextKey = trnType + ":" + accCode + ":" + txtRequiredDate.Text.Trim();
            var dtIssueType = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetDataIssueType());
            cboIssueType.DataSource = dtIssueType;
            cboIssueType.DataTextField = "Iss_Type_Name";
            cboIssueType.DataValueField = "Iss_Type_Id";
            cboIssueType.DataBind();
            cboIssueType.Items.Insert(0, "------Select-------");
            txtReqFor.Focus();
        }
        catch (Exception)
        {

            //throw;
        }        
    }
    protected void CustomValidator5_ServerValidate(object source, ServerValidateEventArgs args)
    {
        Recordset rs = new Recordset();
        Connection DC = new Connection();
        string constr, str;

        var srType = "";
        var accCode = "";
        var locationID = "";

        try
        {
            srType = cboReqType.SelectedValue;

            if (txtReqFor.Text.ToUpper() != "")
            {
                string[] temp = txtReqFor.Text.Split(':');
                if (temp.Length >= 1) accCode = temp[0];
            }

            if (txtLocation.Text.ToUpper() != "")
            {
                string[] temp = txtLocation.Text.Split(':');
                if (temp.Length >= 1)
                {
                    locationID = srType == "SRB" ? temp[1] : temp[0];
                }
            }

            constr = _connectionString;
            DC.Open(constr, null, null, 0);

            switch (srType)
            {
                case "SR":
                    str = "Select Par_Adr_Code,par_adr_name from SaMa_Par_Adr where Par_Adr_Acc_Code='" + accCode + "' " +
                          "and Par_Adr_Code='" + locationID + "'";
                    rs.Open(str, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    args.IsValid = !rs.EOF;

                    rs.Close();
                    DC.Close();
                    break;

                case "SRB":
                    string analGrp = "";
                    str =
                        "Select trn_doc_anal_flag From InSu_Trn_Doc_Anal Where  trn_doc_anal_tr_type = 'IS' And  trn_doc_anal_tr_code = 'SRB'";
                    rs.Open(str, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    while (!rs.EOF)
                    {
                        if (analGrp != "")
                        {
                            analGrp = analGrp + ",'" + rs.Fields[0].Value + "'";
                        }
                        else
                        {
                            analGrp = "'" + rs.Fields[0].Value + "'";
                        }
                        rs.MoveNext();
                    }
                    rs.Close();

                    str = "select Grp_Code_Id,Grp_Code,Grp_Code_Name from InMa_Grp_Code where Grp_Code='" + locationID + "'";
                    rs.Open(str, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    args.IsValid = !rs.EOF;

                    rs.Close();
                    DC.Close();
                    break;


                case "SRO":
                    DC.Close();
                    var constrWfa2 = _connectionString;
                    DC.Open(constrWfa2, null, null, 0);
                    str = "select distinct [userid],[user_name] from tbl_user_info Where department_code='" + accCode +
                          "' and ([status]=1  or resign_date>=Convert(datetime,'" + txtRequiredDate.Text.Trim() + "',103)) and [userid]='" + locationID + "' union all " +
                          "select distinct department_code,department from tbl_user_info where department_code='" + locationID + "'";
                    rs.Open(str, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    args.IsValid = !rs.EOF;

                    rs.Close();
                    DC.Close();
                    break;
            }
        }
        catch (Exception)
        {
            args.IsValid = false;
            
        }
    }
    protected void txtLocation_TextChanged(object sender, EventArgs e)
    {
        try
        {
            cboIssueType.Focus();
        }
        catch (Exception)
        {

        }
    }
    protected void cboIssueType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtReqBy.Focus();
        }
        catch (Exception)
        {

        }
    }
    protected void txtRequiredDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            var trnType = "";
            var accCode = "";
            trnType = cboReqType.SelectedValue;
            if (txtReqFor.Text.Trim() != "")
            {
                string[] temp = txtReqFor.Text.Trim().Split(':');
                if (temp.Length >= 1) accCode = temp[0];
            }
            AutoCompleteExtenderLoc.ContextKey = trnType + ":" + accCode + ":" + txtRequiredDate.Text.Trim();
            AutoCompleteExtenderReqBy.ContextKey = txtRequiredDate.Text.Trim();
            txtRemarks.Focus();
        }
        catch (Exception ex)
        {

        }
    }
    protected void txtReqBy_TextChanged(object sender, EventArgs e)
    {
        txtTkiNo.Focus();
    }
    protected void txtTkiNo_TextChanged(object sender, EventArgs e)
    {
        
    }
}
