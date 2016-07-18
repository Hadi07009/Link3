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
using LibraryDAL.dsInventory.dsInventoryMasterTableAdapters;
using LibraryDAL.dsInventory.dsInventoryMasterTableAdapters;
using LibraryDAL.dsInventory.dsInventoryTransactionTableAdapters;


public partial class modules_FixedAsset_TransactionDetails_frmIssue : System.Web.UI.Page
{
    string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtIssueDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            LoadInitIssueGrid();

            if (Request.QueryString["SRNO"] != null) txtSelectSR.Text = Request.QueryString["SRNO"].ToString();
            Session[StaticData.sessionUserId] = "";
            if (Session[StaticData.sessionUserId].ToString() == "L3T253")
            {
                pnlSelectSrBody.Visible = false;
                pnlSelectSrHdr.Visible = false;
            }
        }
    }

    private void LoadInitIssueGrid()
    {
        var dt = new DataTable();
        dt.Rows.Clear();
        dt.Columns.Add("SR Line#", typeof(string));
        dt.Columns.Add("Item Code", typeof(string));
        dt.Columns.Add("Item Name", typeof(string));
        dt.Columns.Add("UOM", typeof(string));
        dt.Columns.Add("Store", typeof(string));
        dt.Columns.Add("Quantity", typeof(string));
        dt.Columns.Add("Rate", typeof(string));
        dt.Columns.Add("Amount", typeof(string));
        dt.Columns.Add("Serial", typeof(string));
        dt.Columns.Add("Rate ID", typeof(string));
        dt.Columns.Add("Rate Line No", typeof(string));
        dt.Columns.Add("MRR No", typeof(string));
        ViewState["datatableIssue"] = dt;
    }

    private void LoadInitSrGrid()
    {
        var dt = new DataTable();
        dt.Columns.Add("Sr_Det_Lno", typeof(Int16));
        dt.Columns.Add("Sr_Det_Icode", typeof(string));
        dt.Columns.Add("Sr_Det_Itm_Desc", typeof(string));
        dt.Columns.Add("Sr_Det_Itm_Uom", typeof(string));
        dt.Columns.Add("Sr_Det_Bal_Qty", typeof(float));
        dt.Columns.Add("Sr_Det_Str_Code", typeof(string));
        dt.Columns.Add("Sr_Det_Iss_Qty", typeof(float));
        dt.Columns.Add("Sr_Det_Serial_No", typeof(string));
        ViewState["datatableSR"] = dt;
    }

    private void SetIssueGridData()
    {
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["datatableIssue"];
        gvIssue.DataSource = dt;
        gvIssue.DataBind();
        if (gvIssue.Rows.Count > 0)
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
            btnHold.Visible = false;
            btnPost.Visible = false;
        }
    }

    private void SetSrGridData()
    {
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["datatableSR"];
        gvSrDet.DataSource = dt;
        gvSrDet.DataBind();

        if (gvSrDet.Rows.Count > 0)
        {
            this.CollapsiblePanelExtenderHdr.Collapsed = false;
            this.CollapsiblePanelExtenderHdr.ClientState = "false";
            this.CollapsiblePanelExtenderSelectSr.Collapsed = false;
            this.CollapsiblePanelExtenderSelectSr.ClientState = "false";
        }
        else
        {
            if (gvIssue.Rows.Count > 0)
            {
                this.CollapsiblePanelExtenderHdr.Collapsed = false;
                this.CollapsiblePanelExtenderHdr.ClientState = "false";
            }
            else
            {
                this.CollapsiblePanelExtenderHdr.Collapsed = true;
                this.CollapsiblePanelExtenderHdr.ClientState = "true";
            }
            this.CollapsiblePanelExtenderSelectSr.Collapsed = true;
            this.CollapsiblePanelExtenderSelectSr.ClientState = "true";
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
            txtIssueSearch.Text = "";
        }
        lblEditFlag.Text = "N";
        txtIssueSearch.Enabled = true;
        btnSearch.Enabled = true;
        btnClearIssue.Enabled = true;
        txtReqFor.Text = "";
        txtLocationId.Text = "";
        txtIssueType.Text = "";
        txtIssueDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtRemarks.Text = "";

        LoadInitIssueGrid();
        SetIssueGridData();

        LoadInitSrGrid();
        SetSrGridData();

        txtSelectSR.Text = "";
        txtSelectSR.Enabled = true;
        btnSelectSR.Enabled = true;

        pnlSelectSrBody.Enabled = true;
        pHdrBody.Enabled = true;
        pDetBody.Enabled = true;
        gvIssue.Columns[0].Visible = true;
        gvIssue.Enabled = true;
        this.CollapsiblePanelExtenderSrchIssue.Collapsed = true;
        this.CollapsiblePanelExtenderSrchIssue.ClientState = "true";
        this.CollapsiblePanelExtenderSelectSr.Collapsed = false;
        this.CollapsiblePanelExtenderSelectSr.ClientState = "false";
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            ClearFieldData("M");
            var IssRef = "";
            if (txtIssueSearch.Text.ToUpper() != "")
            {
                string[] temp = txtIssueSearch.Text.Split(':');
                if (temp.Length >= 1) IssRef = temp[0];
                else IssRef = txtIssueSearch.Text.Trim();
            }

            var dtIssHdr = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetIssHdrByRefNo(IssRef));
            if (dtIssHdr.Rows.Count > 0)
            {
                txtIssueSearch.Enabled = false;
                btnSearch.Enabled = false;
                btnClearIssue.Visible = true;
                btnClearIssue.Enabled = true;
                lblEditFlag.Text = "Y";

                #region GetReqFor
                var client = "";
                var reqForName = "";
                if (dtIssHdr.Rows[0]["Trn_Hdr_Code"].ToString() != "SRO")
                {
                    var dtClient = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetClientByCode(dtIssHdr.Rows[0]["Trn_Hdr_Dcode"].ToString() == null ? "" : dtIssHdr.Rows[0]["Trn_Hdr_Dcode"].ToString()));
                    if (dtClient.Rows.Count > 0)
                    {
                        client = dtClient.Rows[0]["Par_Acc_Code"].ToString() + ":" + dtClient.Rows[0]["Par_Acc_Name"].ToString();
                        reqForName = dtClient.Rows[0]["Par_Acc_Name"].ToString();
                    }
                }
                else
                {
                    var dtDptLoc = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetDeptByCode(dtIssHdr.Rows[0]["Trn_Hdr_Dcode"].ToString() == null ? "" : dtIssHdr.Rows[0]["Trn_Hdr_Dcode"].ToString()));
                    if (dtDptLoc.Rows.Count > 0)
                    {
                        client = dtDptLoc.Rows[0]["Ccg_Code"].ToString().Trim() + ":" + dtDptLoc.Rows[0]["Ccg_Name"].ToString().Trim();
                        reqForName = dtDptLoc.Rows[0]["Ccg_Name"].ToString().Trim();
                    }
                }
                txtReqFor.Text = client;
                #endregion

                #region GetLocationId
                var locationId = "";
                if (dtIssHdr.Rows[0]["Trn_Hdr_Code"].ToString() == "SR")
                {
                    var dtClient = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetClientAddrByCode(dtIssHdr.Rows[0]["Trn_Hdr_Acode"].ToString() == null ? "" : dtIssHdr.Rows[0]["Trn_Hdr_Acode"].ToString()));
                    locationId = dtIssHdr.Rows[0]["Trn_Hdr_Acode"].ToString() + ":" + dtClient.Rows[0]["par_adr_name"].ToString();
                }

                if (dtIssHdr.Rows[0]["Trn_Hdr_Code"].ToString() == "SRB")
                {
                    var dtBackbone = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.CheckDuplicateData(dtIssHdr.Rows[0]["T_C1"].ToString() == null ? "" : dtIssHdr.Rows[0]["T_C2"].ToString(),
                                                                     dtIssHdr.Rows[0]["Trn_Hdr_Acode"].ToString() == null
                                                                         ? ""
                                                                         : dtIssHdr.Rows[0]["Trn_Hdr_Acode"].ToString()));
                    locationId = dtBackbone.Rows[0]["Grp_Code_Id"].ToString() + ":" + dtIssHdr.Rows[0]["Trn_Hdr_Acode"].ToString() + ":" +
                                 dtBackbone.Rows[0]["Grp_Code_Name"].ToString();
                }
                if (dtIssHdr.Rows[0]["Trn_Hdr_Code"].ToString() == "SRO")
                {
                    Recordset rs = new Recordset();
                    Connection DC = new Connection();
                    string constr, str;
                    constr = System.Configuration.ConfigurationManager.AppSettings["Wfa2ConStr"].ToString();
                    DC.Open(constr, null, null, 0);
                    str = "WITH OwnUseLoc (LocID,LocName) as (select distinct [userid],[user_name] from tbl_user_info " +
                          "Where [userid]='" + (dtIssHdr.Rows[0]["Trn_Hdr_Acode"].ToString() == null ? "" : dtIssHdr.Rows[0]["Trn_Hdr_Acode"].ToString()) + "' " +
                          "union all select distinct department_code,department from WFA2.dbo.tbl_user_info " +
                          "where department_code='" + (dtIssHdr.Rows[0]["Trn_Hdr_Acode"].ToString() == null ? "" : dtIssHdr.Rows[0]["Trn_Hdr_Acode"].ToString()) + "') " +
                          "Select LocID,LocName from OwnUseLoc";

                    rs.Open(str, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    if (!rs.EOF)
                    {
                        locationId = rs.Fields[0].Value + ":" + rs.Fields[1].Value;
                    }
                    rs.Close();
                    DC.Close();
                }

                if (dtIssHdr.Rows[0]["Trn_Hdr_Code"].ToString() == "SRS")
                {
                    Recordset rsSale = new Recordset();
                    Connection DCSale = new Connection();
                    string constr, str;
                    constr = System.Configuration.ConfigurationManager.AppSettings["L3TConnStr"].ToString();
                    DCSale.Open(constr, null, null, 0);
                    str = "SELECT Gl_Coa_Code,Gl_Coa_Name FROM AccCOA WHERE Gl_Coa_Code='" + (dtIssHdr.Rows[0]["Trn_Hdr_Acode"].ToString() == null ? "" : dtIssHdr.Rows[0]["Trn_Hdr_Acode"].ToString()) + "'";

                    rsSale.Open(str, DCSale, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    if (!rsSale.EOF)
                    {
                        locationId = rsSale.Fields[0].Value + ":" + rsSale.Fields[1].Value;
                    }
                    rsSale.Close();
                    DCSale.Close();
                }

                txtLocationId.Text = locationId;
                #endregion

                txtTkiNo.Text = dtIssHdr.Rows[0]["Trn_Hdr_Com2"].ToString();

                #region GetIssueType
                var issType = dtIssHdr.Rows[0]["T_C1"].ToString() == null ? "" : dtIssHdr.Rows[0]["T_C1"].ToString();
                var dtIssType = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetIssueTypeById(dtIssHdr.Rows[0]["T_C1"].ToString() == null ? "" : dtIssHdr.Rows[0]["T_C1"].ToString()));
                if (dtIssType.Rows.Count > 0)
                {
                    issType = dtIssHdr.Rows[0]["T_C1"].ToString() == null ? "" : dtIssHdr.Rows[0]["T_C1"].ToString() + ":" + dtIssType.Rows[0]["Iss_Type_Name"].ToString();
                }
                txtIssueType.Text = issType;
                #endregion

                txtIssueDate.Text = dtIssHdr.Rows[0]["Trn_Hdr_DATE"].ToString();
                txtRemarks.Text = dtIssHdr.Rows[0]["Trn_Hdr_Com1"].ToString();
                var dt = new DataTable();
                LoadInitIssueGrid();
                dt = (DataTable)ViewState["datatableIssue"];
                var dtIssDet = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetIssDetByRefNo(IssRef));
                txtSelectSR.Text = dtIssDet.Rows[0]["Trn_Det_Ord_Ref"].ToString() + ":" + reqForName;
                foreach (DataRow rowNo in dtIssDet.Rows)
                {
                    dt.Rows.Add(rowNo["Trn_Det_Ord_Lno"].ToString(), rowNo["Trn_Det_Icode"].ToString(),rowNo["Trn_Det_Itm_Desc"].ToString(),
                                rowNo["Trn_Det_Itm_Uom"].ToString(),
                                rowNo["Trn_Det_Str_Code"].ToString(), rowNo["Trn_Det_Lin_Qty"].ToString(),
                                rowNo["Trn_Det_Lin_Rat"].ToString(),
                                rowNo["Trn_Det_Lin_Amt"].ToString(), rowNo["Trn_Det_Bat_No"].ToString(), rowNo["T_Fl"].ToString() == null ? "" : rowNo["T_Fl"].ToString(),
                                rowNo["T_In"].ToString(), rowNo["T_C2"].ToString());
                    ViewState["datatableIssue"] = dt;
                    SetIssueGridData();
                }

                if (dtIssHdr.Rows[0]["Trn_Hdr_HRPB_Flag"].ToString() == "P")
                {
                    btnHold.Visible = false;
                    btnPost.Visible = false;
                    pnlSelectSrBody.Enabled = false;
                    pHdrBody.Enabled = false;
                    pDetBody.Enabled = false;
                    gvIssue.Columns[0].Visible = false;
                    gvIssue.Enabled = false;
                }
                else
                {
                    //btnHold.Visible = true;
                    btnPost.Visible = true;
                    pnlSelectSrBody.Enabled = true;
                    pHdrBody.Enabled = true;
                    pDetBody.Enabled = true;
                    gvIssue.Columns[0].Visible = true;
                    gvIssue.Enabled = true;
                }
                this.CollapsiblePanelExtenderSrchIssue.Collapsed = false;
                this.CollapsiblePanelExtenderSrchIssue.ClientState = "false";
                this.CollapsiblePanelExtenderSelectSr.Collapsed = true;
                this.CollapsiblePanelExtenderSelectSr.ClientState = "true";
            }
        }
        catch (Exception ex)
        {

            //throw;
        }
    }

    protected void gvIssue_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var indx = gvIssue.SelectedIndex;
            if (indx != -1)
            {
                GridViewRow row = gvIssue.Rows[indx];
                var SrRef = "";
                if (txtSelectSR.Text.ToUpper() != "")
                {
                    string[] temp = txtSelectSR.Text.Split(':');
                    if (temp.Length >= 1) SrRef = temp[0];
                }

                var dt = new DataTable();
                dt = (DataTable)ViewState["datatableIssue"];

                decimal issueQty = 0;
                var serial = "";
                for (int i = gvIssue.Rows.Count - 1; 0 <= i; i--)
                {
                    GridViewRow gr = gvIssue.Rows[i];
                    if (gr.Cells[2].Text == row.Cells[2].Text)
                    {
                        issueQty = issueQty + Convert.ToDecimal(gr.Cells[6].Text);
                        Label lblSerial = (Label)gr.FindControl("Label1");
                        var serialText = (lblSerial != null) ? lblSerial.Text : "";
                        if (serial != "")
                        {
                            serial = serial + "," + (serialText == "&nbsp;" ? "" : serialText);
                        }
                        else
                        {
                            serial = serialText == "&nbsp;" ? "" : serialText;
                        }
                        dt.Rows.RemoveAt(gr.RowIndex);
                        ViewState["datatableIssue"] = dt;
                    }
                }
                SetIssueGridData();
                DataTable dtSrDet = new DataTable();
                dtSrDet = (DataTable)ViewState["datatableSR"];
                var dtDbSrDet = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetSrDetByIcode(SrRef, row.Cells[2].Text.Trim()));
                dtSrDet.Rows.Add(Convert.ToInt16(row.Cells[1].Text), row.Cells[2].Text, row.Cells[3].Text,
                                 row.Cells[4].Text, dtDbSrDet.Rows[0]["Sr_Det_Bal_Qty"].ToString(), row.Cells[5].Text, issueQty, serial);
                ViewState["datatableSR"] = dtSrDet;
                SetSrGridData();
                gvIssue.SelectedIndex = -1;
            }
        }
        catch (Exception)
        {

            //throw;
        }
    }

    protected void gvSrDet_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string[] temp;
            var indx = gvSrDet.SelectedIndex;
            if (indx != -1)
            {
                GridViewRow row = gvSrDet.Rows[indx];
                var itemCode = row.Cells[1].Text.Trim();
                var storeCode = row.Cells[5].Text.Trim();
                var SrQty = Convert.ToInt32(row.Cells[4].Text.Trim());
                var SrRate = Convert.ToDecimal(1.0);
                var IssueQty = Convert.ToInt32(((TextBox)(row.Cells[6].FindControl("TextBox1"))).Text);
                var serialTextBox = (TextBox)(row.Cells[6].FindControl("txtSerial"));
                var itmSerial = ((TextBox)(row.Cells[6].FindControl("txtSerial"))).Text;

                if (IssueQty > SrQty)
                {
                    gvSrDet.SelectedIndex = -1;
                    return;
                }

                #region GetCurrentStock
                
                Connection DCCurStk = new Connection();
                Recordset rsCurStk = new Recordset();
                string connstr;
                connstr = System.Configuration.ConfigurationManager.AppSettings["ConStr"].ToString();

                DCCurStk.Open(connstr, null, null, 0);

                var strCurStk = ";WITH Cur_Bal_Qty (Trn_Det_Icode,Trn_Det_Str_Code,RcvQty, IssQty, TrnBalQty) AS ( " +
                                "SELECT     Trn_Det_Icode, Trn_Det_Str_Code, " +
                                "SUM(CASE WHEN LEFT(Trn_Det_Type, 1) = 'R' THEN Trn_Det_Lin_Qty ELSE 0 END) AS RcvQty, " +
                                "SUM(CASE WHEN LEFT(Trn_Det_Type, 1) = 'I' THEN Trn_Det_Lin_Qty ELSE 0 END) AS IssQty, " +
                                "SUM(CASE WHEN LEFT(Trn_Det_Type, 1) = 'R' THEN Trn_Det_Lin_Qty ELSE 0 END) - " +
                                "SUM(CASE WHEN LEFT(Trn_Det_Type, 1) = 'I' THEN Trn_Det_Lin_Qty ELSE 0 END) AS TrnBalQty " +
                                "FROM InTr_Trn_Det where Trn_Det_Icode='" + itemCode + "' and Trn_Det_Str_Code='" + storeCode + "' " +
                                "and Trn_Det_Book_Dat<=CONVERT(datetime,'" + txtIssueDate.Text.Trim() + "',103) GROUP BY Trn_Det_Icode, Trn_Det_Str_Code ) " +
                                ", ItmStore as ( Select Trn_Det_Icode,Trn_Det_Str_Code from Cur_Bal_Qty  UNION  " +
                                "Select Item_Code,Store from InMa_Op_Bal_New Where Item_Code='" + itemCode + "' and Store='" + storeCode + "') " +
                                "SELECT ItmStore.Trn_Det_Icode,ItmStore.Trn_Det_Str_Code,Isnull(BalQty,0) as OpBalQty,isnull(RcvQty,0) as RcvQty, " +
                                "isnull(IssQty,0) as IssQty, isnull(TrnBalQty,0) as TrnBalQty, (Isnull(TrnBalQty,0)+Isnull(BalQty,0)) as CurBalQty " +
                                "FROM ItmStore LEFT OUTER JOIN Cur_Bal_Qty on ItmStore.Trn_Det_Icode=Cur_Bal_Qty.Trn_Det_Icode " +
                                "and ItmStore.Trn_Det_Str_Code=Cur_Bal_Qty.Trn_Det_Str_Code LEFT OUTER JOIN InMa_Op_Bal_New " +
                                "on ItmStore.Trn_Det_Icode=Item_Code and ItmStore.Trn_Det_Str_Code=store";
                rsCurStk.Open(strCurStk, DCCurStk, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);
                var balQty = 0;
                if (!rsCurStk.EOF)
                {
                    balQty = Convert.ToInt32(rsCurStk.Fields["CurBalQty"].Value);
                }
                rsCurStk.Close();
                DCCurStk.Close();

                if (itemCode != "14.004.001.0036")
                {
                    if (IssueQty > balQty)
                    {
                        gvSrDet.SelectedIndex = -1;
                        lblMsgHdr.Text = "There is no item quantity upto this selected issue date.";
                        tblPopUp.Rows[2].Cells[0].InnerText = "";
                        txtIssueRef.Visible = false;
                        ModalPopupExtender3.Show();
                        return;
                    }
                }
                #endregion

                #region CheckSerialValidity
                temp = itmSerial.Split(',');
                if (serialTextBox.Visible == true)
                {
                    if (itmSerial == "")
                    {
                        gvSrDet.SelectedIndex = -1;
                        lblMsgHdr.Text = "Enter Serial No";
                        tblPopUp.Rows[2].Cells[0].InnerText = "";
                        txtIssueRef.Visible = false;
                        ModalPopupExtender3.Show();
                        return;
                    }
                    else
                    {
                        var numberOfSerial = temp.Length;
                        if (IssueQty != numberOfSerial)
                        {
                            tblPopUp.Rows[2].Cells[0].InnerText = "";
                            lblMsgHdr.Text = "Issue Quantity and Number of Serial does not match.";
                            txtIssueRef.Visible = false;
                            ModalPopupExtender3.Show();
                            gvSrDet.SelectedIndex = -1;
                            return;
                        }

                    }
                }
                #endregion

                double rate = 0, rateQty = 0, amount = 0;
                string rateid = "";
                string mrrno = "";
                var rateLineNo = 0;
                Decimal itmRemQty = Convert.ToDecimal(IssueQty);
                var j = 0;
                var k = 0;
                
                #region FIFO_SERIAL_QTY
                var serialQry = "";
                if (serialTextBox.Visible)
                {
                    Connection DC = new Connection();
                    Recordset rsRateID = new Recordset();
                    Recordset rsRateSerial = new Recordset();
                    Recordset rsRateMRR = new Recordset();
                    string qryStr, constr;
                    if (itmSerial != "")
                    {
                        var allSerial = itmSerial.Split(',');
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

                    constr = System.Configuration.ConfigurationManager.AppSettings["ConStr"].ToString();

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

                                rate = Convert.ToDouble(rsRateSerial.Fields["itm_rate_rate"].Value);
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
                            dt = (DataTable)ViewState["datatableIssue"];

                            dt.Rows.Add(row.Cells[0].Text.Trim(), row.Cells[1].Text.Trim(), row.Cells[2].Text.Trim(),
                                        row.Cells[3].Text.Trim(), row.Cells[5].Text.Trim(), rateQty, rate, amount,
                                        itmSerial,
                                        rateid, rateLineNo, mrrno);
                            ViewState["datatableIssue"] = dt;
                            SetIssueGridData();

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
                    DataTable dtFifoItmQty = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetFIFOItem(itemCode, storeCode));
                    foreach (DataRow rowNo in dtFifoItmQty.Rows)
                    {
                        if (itmRemQty > 0)
                        {
                            if (Convert.ToDecimal( rowNo["itm_rate_qty"].ToString()) >= itmRemQty)
                            {
                                rateQty = Convert.ToDouble(itmRemQty);
                                rate = Convert.ToDouble(rowNo["itm_rate_rate"].ToString() == null ? 0 : Convert.ToDouble( rowNo["itm_rate_rate"].ToString()));
                                amount = rate * rateQty;
                                rateid = rowNo["itm_rate_id"].ToString() == null ? "" : rowNo["itm_rate_id"].ToString();
                                rateLineNo = rowNo["itm_rate_lineno"].ToString() == null ? 0 : Convert.ToInt32( rowNo["itm_rate_lineno"].ToString());
                                mrrno = rowNo["itm_rate_trn_ref"].ToString() == null ? "" : rowNo["itm_rate_trn_ref"].ToString();
                                itmRemQty = 0;
                            }
                            else
                            {
                                rateQty = Convert.ToDouble(rowNo["itm_rate_qty"].ToString() == null ? 0 : Convert.ToDouble( rowNo["itm_rate_qty"].ToString()));
                                rate = Convert.ToDouble(rowNo["itm_rate_rate"].ToString() == null ? 0 : Convert.ToDouble( rowNo["itm_rate_rate"].ToString()));
                                amount = rate * rateQty;
                                rateid = rowNo["itm_rate_id"].ToString() == null ? "" : rowNo["itm_rate_id"].ToString();
                                rateLineNo = rowNo["itm_rate_lineno"].ToString() == null ? 0 : Convert.ToInt32( rowNo["itm_rate_lineno"].ToString());
                                mrrno = rowNo["itm_rate_trn_ref"].ToString() == null ? "" : rowNo["itm_rate_trn_ref"].ToString();
                                itmRemQty = itmRemQty - (rowNo["itm_rate_qty"].ToString() == null ? 0 : Convert.ToDecimal( rowNo["itm_rate_qty"].ToString()));
                            }
                            DataTable dt = new DataTable();
                            dt = (DataTable)ViewState["datatableIssue"];
                            dt.Rows.Add(row.Cells[0].Text.Trim(), row.Cells[1].Text.Trim(), row.Cells[2].Text.Trim(),
                                        row.Cells[3].Text.Trim(), row.Cells[5].Text.Trim(), rateQty, rate, amount,
                                        itmSerial,
                                        rateid, rateLineNo, mrrno);
                            ViewState["datatableIssue"] = dt;
                            SetIssueGridData();
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                #endregion

                DataTable dtSrDet = new DataTable();
                dtSrDet = (DataTable)ViewState["datatableSR"];
                dtSrDet.Rows.RemoveAt(indx);
                ViewState["datatableSR"] = dtSrDet;
                SetSrGridData();
                gvSrDet.SelectedIndex = -1;
            }
        }
        catch (Exception ex)
        {

            //throw;
        }
    }

    protected void gvSrDet_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
    }

    protected void btnClearSR_Click(object sender, EventArgs e)
    {
        txtSelectSR.Enabled = false;
        btnSelectSR.Enabled = false;
        btnClearSR.Visible = true;
        btnClearSR.Enabled = true;
        ClearFieldData("");                
    }

    protected void btnSelectSr_Click(object sender, EventArgs e)
    {
        try
        {
            #region GetSrRef
            var SrRef = "";
            if (txtSelectSR.Text.ToUpper() != "")
            {
                string[] temp = txtSelectSR.Text.Split(':');
                if (temp.Length >= 1) SrRef = temp[0];
            }
            #endregion

            var dtSrHdr = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetSrHdrByRefNo(SrRef));
            if (dtSrHdr.Rows.Count > 0)
            {
                #region GetReqFor
                var ReqFor = "";
                if (dtSrHdr.Rows[0]["Sr_Hdr_Code"].ToString() != "SRO")
                {
                    var dtClient = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetClientByCode(dtSrHdr.Rows[0]["Sr_Hdr_Dcode"].ToString() == null ? "" : dtSrHdr.Rows[0]["Sr_Hdr_Dcode"].ToString()));
                    if (dtClient.Rows.Count > 0)
                    {
                        ReqFor = dtSrHdr.Rows[0]["Sr_Hdr_Dcode"].ToString() + ":" + dtClient.Rows[0]["Par_Acc_Name"].ToString();
                    }
                    txtReqFor.Text = ReqFor;
                }
                else
                {
                    var dtDptLoc = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetDeptByCode(dtSrHdr.Rows[0]["Sr_Hdr_Pcode"].ToString() == null ? "" : dtSrHdr.Rows[0]["Sr_Hdr_Pcode"].ToString()));
                    if (dtDptLoc.Rows.Count > 0)
                    {
                        ReqFor = dtDptLoc.Rows[0]["Ccg_Code"].ToString().Trim() + ":" + dtDptLoc.Rows[0]["Ccg_Name"].ToString().Trim();
                    }
                    txtReqFor.Text = ReqFor;
                }
                #endregion

                #region GetLocID
                var locationId = "";
                if (dtSrHdr.Rows[0]["Sr_Hdr_Code"].ToString() == "SR")
                {
                    var dtClientAdr = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetClientAddrByCode(dtSrHdr.Rows[0]["Sr_Hdr_Acode"].ToString() == null ? "" : dtSrHdr.Rows[0]["Sr_Hdr_Acode"].ToString()));
                    locationId = dtSrHdr.Rows[0]["Sr_Hdr_Acode"].ToString() + ":" + dtClientAdr.Rows[0]["par_adr_name"].ToString();
                }
                if (dtSrHdr.Rows[0]["Sr_Hdr_Code"].ToString() == "SRB")
                {
                    var dtBackbone = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.CheckDuplicateData(dtSrHdr.Rows[0]["T_C1"].ToString() == null ? "" : dtSrHdr.Rows[0]["T_C2"].ToString(),
                                                                     dtSrHdr.Rows[0]["Sr_Hdr_Acode"].ToString() == null
                                                                         ? ""
                                                                         : dtSrHdr.Rows[0]["Sr_Hdr_Acode"].ToString()));
                    if (dtBackbone.Rows.Count > 0)
                    {
                        locationId = dtBackbone.Rows[0]["Grp_Code_Id"].ToString() + ":" + dtSrHdr.Rows[0]["Sr_Hdr_Acode"].ToString() + ":" +
                                 dtBackbone.Rows[0]["Grp_Code_Name"].ToString();
                    }
                }
                if (dtSrHdr.Rows[0]["Sr_Hdr_Code"].ToString() == "SRO")
                {                    
                    string str;                  

                    str = "select Sr_Hdr_Acode,b.Dept from InTr_Sr_Hdr a" 
                        + " inner join Emp_Details b on a.Sr_Hdr_Acode=b.DeptID and Sr_Hdr_Pcode=b.EmpID where Sr_Hdr_Ref='"+ SrRef + "' ";

                    var dtloccode = DataProcess.GetData(_connectionString, str);
                    if (dtloccode.Rows.Count > 0)
                    {
                        locationId = dtloccode.Rows[0]["Sr_Hdr_Acode"].ToString().Trim() + ":" + dtloccode.Rows[0]["Dept"].ToString().Trim();
                    }                    
                   
                }

                if (dtSrHdr.Rows[0]["Sr_Hdr_Code"].ToString() == "SRS")
                {
                    Recordset rsSale = new Recordset();
                    Connection DCSale = new Connection();
                    string constr, str;
                    constr = System.Configuration.ConfigurationManager.AppSettings["L3TConnStr"].ToString();
                    DCSale.Open(constr, null, null, 0);
                    str = "select Par_Adr_Code,par_adr_name from SaMa_Par_Adr where Par_Adr_Code='" + dtSrHdr.Rows[0]["Sr_Hdr_Acode"].ToString() + "'";

                    rsSale.Open(str, DCSale, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    if (!rsSale.EOF)
                    {
                        locationId = rsSale.Fields[0].Value + ":" + rsSale.Fields[1].Value;
                    }
                    rsSale.Close();
                    DCSale.Close();
                }

                txtLocationId.Text = locationId;
                #endregion

                txtTkiNo.Text = dtSrHdr.Rows[0]["Sr_Hdr_Com2"].ToString();

                #region GetIssueType
                var issueType = "";
                var dtIssueType = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetIssueTypeById(dtSrHdr.Rows[0]["T_C1"].ToString()));
                if (dtIssueType.Rows.Count > 0)
                {
                    issueType = dtSrHdr.Rows[0]["T_C1"].ToString() + ":" + dtIssueType.Rows[0]["Iss_Type_Name"].ToString();
                }
                txtIssueType.Text = issueType;
                #endregion

                txtIssueDate.Text = dtSrHdr.Rows[0]["Sr_Hdr_End_DATE"].ToString();

                #region GetSrdet
                LoadInitSrGrid();
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["datatableSR"];

                var dtSrDet = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetSrDetByRefNo(SrRef));
                foreach (DataRow rowNo in dtSrDet.Rows)
                {
                    dt.Rows.Add(rowNo["Sr_Det_Lno"].ToString(), rowNo["Sr_Det_Icode"].ToString(), rowNo["Sr_Det_Itm_Desc"].ToString(), rowNo["Sr_Det_Itm_Uom"].ToString(), rowNo["Sr_Det_Bal_Qty"].ToString(),
                                rowNo["Sr_Det_Str_Code"].ToString(), rowNo["Sr_Det_Bal_Qty"].ToString(), "");
                }
                #endregion

                ViewState["datatableSR"] = dt;
                SetSrGridData();
                txtSelectSR.Enabled = false;
                btnSelectSR.Enabled = false;
                btnClearSR.Visible = true;
                btnClearSR.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            //MessageBox.Show("ERROR :" + ex.Message);
            //throw;
        }        
    }

    protected void btnPost_Click(object sender, EventArgs e)
    {
        SaveData("P");
    }
    
    protected void txtSerial_TextChanged(object sender, EventArgs e)
    {
        try
        {
            var currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
            var item = currentRow.Cells[1].Text;
            var store = currentRow.Cells[5].Text;
            var serial = (TextBox)currentRow.FindControl("txtSerial");
            var autocompleteExtender = (AutoCompleteExtender)currentRow.FindControl("AutoCompleteExtenderAddSerial");
            autocompleteExtender.ContextKey = item + ":" + store + ":" + serial.Text;
        }
        catch (Exception)
        {

            //throw;
        }        
    }

    protected void btnAddSerial_Click(object sender, EventArgs e)
    {
        string constr = System.Configuration.ConfigurationManager.AppSettings["ConStr"].ToString();

        try
        {
            var currentRow = (GridViewRow)((Button)sender).Parent.Parent;
            var serialno = (TextBox)currentRow.FindControl("txtSerialNo");
            var serial = (TextBox)currentRow.FindControl("txtSerial");
            var itemCode = currentRow.Cells[1].Text;
            var storeCode = currentRow.Cells[5].Text;
            if (serialno.Text.Trim() == "") return;

            clsDbCon dbCon = new clsDbCon();
            string qryStr;

            Recordset rs = new Recordset();
            Connection DC = new Connection();
            string str;         

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
                  "' and itm_det_str_code='" + storeCode + "' and itm_det_serial_no = '" + serialno.Text + "'";

            rs.Open(str, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);

            if (!rs.EOF)
            {
                if (serial.Text.Trim().Length > 0)
                {
                    serial.Text = serial.Text + "," + serialno.Text;
                }
                else
                {
                    serial.Text = serialno.Text;
                }
                serialno.Text = "";

                var item = currentRow.Cells[1].Text;
                var store = currentRow.Cells[5].Text;
                var autocompleteext = (AutoCompleteExtender)currentRow.FindControl("AutoCompleteExtenderAddSerial");

                autocompleteext.ContextKey = item + ":" + store + ":" + serial.Text;
            }
            else
            {
                tblPopUp.Rows[2].Cells[0].InnerText = "";
                lblMsgHdr.Text = "Serial No does not exists.";
                txtIssueRef.Visible = false;
                ModalPopupExtender3.Show();
                gvSrDet.SelectedIndex = -1;
            }

            rs.Close();
            DC.Close();
        }
        catch (Exception)
        {

            //throw;
        }
    }
    protected void gvSrDet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var txtSerial = (TextBox)e.Row.FindControl("txtSerial");
                var txtSrchSerial = (TextBox)e.Row.FindControl("txtSerialNo");
                var btnAddSerial = (Button)e.Row.FindControl("btnAddSerial");
                var dtChkserial = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetItemByCode(e.Row.Cells[1].Text.Trim()));
                if (dtChkserial.Rows.Count > 0)
                {
                    if (dtChkserial.Rows[0]["Itm_Det_Others1_flag"].ToString() == "Y")
                    {
                        txtSrchSerial.Visible = true;
                        btnAddSerial.Visible = true;
                        txtSerial.Visible = true;

                        var item = e.Row.Cells[1].Text;
                        var store = e.Row.Cells[5].Text;
                        var autocompleteext = (AutoCompleteExtender)e.Row.FindControl("AutoCompleteExtenderAddSerial");
                        autocompleteext.ContextKey = item + ":" + store + ":" + txtSerial.Text;

                    }
                    else
                    {
                        var dtItmSerial = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetSerialByItem(e.Row.Cells[1].Text.Trim()));
                        if (dtItmSerial.Rows.Count > 0)
                        {
                            txtSrchSerial.Visible = true;
                            btnAddSerial.Visible = true;
                            txtSerial.Visible = true;

                            var item = e.Row.Cells[1].Text;
                            var store = e.Row.Cells[5].Text;
                            var autocompleteext = (AutoCompleteExtender)e.Row.FindControl("AutoCompleteExtenderAddSerial");
                            autocompleteext.ContextKey = item + ":" + store + ":" + txtSerial.Text;
                        }
                        else
                        {
                            txtSrchSerial.Visible = false;
                            btnAddSerial.Visible = false;
                            txtSerial.Visible = false;
                        }
                    }
                }
                else
                {
                    txtSrchSerial.Visible = false;
                    btnAddSerial.Visible = false;
                    txtSerial.Visible = false;
                }
            }
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
    protected void btnClearIssue_Click(object sender, EventArgs e)
    {
        ClearFieldData("");
    }

    private bool SaveData(string HPFlag)
    {
        if (!Page.IsValid) return false;

        if (txtReqFor.Text == "")
        {
            tblPopUp.Rows[2].Cells[0].InnerText = "";
            lblMsgHdr.Text = "Required For can't be Blank";
            txtIssueRef.Visible = false;
            ModalPopupExtender3.Show();
            return false;
        }

        if (txtLocationId.Text == "")
        {
            tblPopUp.Rows[2].Cells[0].InnerText = "";
            lblMsgHdr.Text = "Location ID can't be Blank";
            txtIssueRef.Visible = false;
            ModalPopupExtender3.Show();
            return false;
        }

        if (txtIssueType.Text == "")
        {
            tblPopUp.Rows[2].Cells[0].InnerText = "";
            lblMsgHdr.Text = "Issue Type can't be Blank";
            txtIssueRef.Visible = false;
            ModalPopupExtender3.Show();
            return false;
        }

        string Prefix = "", yr = "", mn = "", Spr = "", mnFlg = "";
        var IssRefNo = "";
        string MaxJvRefNo;
        string NewJvRefNo;
        decimal totAmount = 0;
        string trnPeriod = Convert.ToDateTime(txtIssueDate.Text.Trim()).ToString("MM") + "/" + Convert.ToDateTime(txtIssueDate.Text.Trim()).ToString("yyyy");
        DateTime chkPeriod = DateTime.Now;
        var userId = Session[StaticData.sessionUserId].ToString();



        var taIssHdr = new InTr_Trn_HdrTableAdapter();
        var taIssDet = new InTr_Trn_DetTableAdapter();
        var taItemSerial = new InMa_Itm_SerialTableAdapter();
        var taItemRate = new InMa_Itm_RateTableAdapter();
        //var taMrrRefFormat = new SaTr_SA_NumberTableAdapter();
        var taGetTranPerm = new InSu_Trn_Set_PstTableAdapter();
        var taSrDet = new InTr_Sr_DetTableAdapter();
        var taStkCtl = new InMa_Stk_CtlTableAdapter();
        var taStkVal = new InMa_Stk_ValTableAdapter();
        var taItmStk = new InMa_Itm_StkTableAdapter();        
        var taIssType = new InTr_Sr_Issue_TypeTableAdapter();
        var taFixedAsset = new fxdFAReferenceNumbersTableAdapter();
        var taItemMasDet = new InMa_Itm_DetTableAdapter();
        var tblmovement = new Item_Movement_dtlTableAdapter();
       
        
        SqlConnection conn = new SqlConnection(_connectionString);
        conn.Open();
        SqlTransaction tran = conn.BeginTransaction();

        try
        {
            #region GetSrRefNo
            var SRRef = "";
            var TrnType = "";
            if (txtSelectSR.Text.ToUpper() != "")
            {
                string[] temp = txtSelectSR.Text.Split(':');
                if (temp.Length >= 1) SRRef = temp[0];
                else SRRef = txtSelectSR.Text.Trim();
            }
            var dtSrType = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetSrDetByRefNo(SRRef));
            if (dtSrType.Rows.Count > 0)
            {
                TrnType = dtSrType.Rows[0]["Sr_Det_Code"].ToString();
            }
            #endregion

            #region GeReqFor
            var ReqFor = "";
            if (txtReqFor.Text.ToUpper() != "")
            {
                string[] temp = txtReqFor.Text.Split(':');
                if (temp.Length >= 1) ReqFor = temp[0];
            }
            #endregion

            #region GetLocId
            var locGrp = "";
            var locId = "";
            if (txtLocationId.Text.ToUpper() != "")
            {
                string[] temp = txtLocationId.Text.Split(':');
                if (temp.Length >= 1)
                {
                    if (TrnType == "SRB")
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

            #region GeIssueType
            var issueType = "";
            if (txtIssueType.Text.ToUpper() != "")
            {
                String[] temp = txtIssueType.Text.Split(':');
                if (temp.Length >= 1) issueType = temp[0];
            }
            #endregion

            var dtGetTranPerm = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetTranByTypeCode("IS", TrnType, "ADM"));

            if (dtGetTranPerm.Rows.Count > 0)
            {
                var dt = new DataTable();
                dt = (DataTable)ViewState["datatableIssue"];

                foreach (DataRow row in dt.Rows)
                {
                    totAmount = (totAmount + Convert.ToDecimal(row["Amount"].ToString()));
                }

                if (lblEditFlag.Text == "N")
                {
                    #region GetNewIssRefNo
                    
                    mn = Convert.ToDateTime(txtIssueDate.Text.Trim()).ToString("MM");
                    yr = Convert.ToDateTime(txtIssueDate.Text.Trim()).ToString("yy");

                    Spr = dtGetTranPerm.Rows[0]["Trn_Set_Tr_Pfix"].ToString();

                    Prefix = Spr + yr + mn;

                    chkPeriod = Convert.ToInt32(mn) < 7 ? Convert.ToDateTime("01/07/" + (Convert.ToInt32(yr) - 1)) : Convert.ToDateTime("01/07/" + yr);

                    var dtRefNo = DataProcess.GetSingleValueFromtable(_connectionString, SqlgenerateForFixedAsset.GetMaxIssRefNo(Convert.ToDateTime(chkPeriod)));
                    var nextRefNo = (dtRefNo == null || Convert.ToInt32(dtRefNo) == 0) ? 1 : Convert.ToInt32(dtRefNo) + 1;
                    IssRefNo = Prefix + nextRefNo.ToString("000000");
                    #endregion

                    #region InsertIssHdr
                    string tempValue5 = null;
                    string tempValue6 = null;
                    DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertMRRHdr("IS", TrnType, IssRefNo, ReqFor, ReqFor, locId,
                                          Convert.ToDateTime(txtIssueDate.Text.Trim()), txtRemarks.Text.Trim(), txtTkiNo.Text.Trim(), "",
                                          "",
                                          "", "", "", "", "", "", totAmount, HPFlag, trnPeriod, "SUB", "", "", "Y",
                                          "", "", "", issueType, locGrp, "", 0, 0,
                                          tempValue5, tempValue6, ""));
                    #endregion
                }
                else
                {
                    #region GetSrchIssRefNo
                    if (txtIssueSearch.Text.ToUpper() != "")
                    {
                        string[] temp = txtIssueSearch.Text.Split(':');
                        if (temp.Length < 2) return false;
                        IssRefNo = temp[0];
                    }
                    #endregion

                    #region EditIssHdr
                    string tempValue3 = null;
                    string tempValue4 = null;
                    DataProcess.UpdateQuery(_connectionString, SqlgenerateForFixedAsset.EditMRRHdr(ReqFor, ReqFor, locId, Convert.ToDateTime(txtIssueDate.Text.Trim()),
                                        txtRemarks.Text.Trim(), txtTkiNo.Text.Trim(), "", "", "", "", "", "", "", "",
                                        totAmount, HPFlag, trnPeriod, "SUB", "", "", "Y", "", "", "",
                                        issueType, locGrp, "", 0, 0, tempValue3, tempValue4, "", "IS", TrnType, IssRefNo));
                    #endregion

                    #region DeleteIssDet
                    DataProcess.DeleteQuery(_connectionString, SqlgenerateForFixedAsset.DeleteMRRDet("IS", TrnType, IssRefNo));
                    
                    #endregion

                    #region DeleteSerial
                    DataProcess.DeleteQuery(_connectionString, SqlgenerateForFixedAsset.DeleteMrrSerial(IssRefNo));
                    
                    #endregion
                }

                #region GetJvRefNo
                DataTable dtJvRefNo = new DataTable();
                if (TrnType == "SRS")
                {
                    dtJvRefNo = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetJvRefNo("EJV", "EJV", "J"));
                    MaxJvRefNo = dtJvRefNo.Rows[0]["JrnNextRefNo"].ToString();
                    NewJvRefNo = ("SCJV" + Convert.ToDateTime(DateTime.Now).ToString("yy") +
                                  Convert.ToDateTime(txtIssueDate.Text.Trim()).ToString("MMM").ToUpper() + MaxJvRefNo);
                }
                else
                {
                    dtJvRefNo = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetJvRefNo("EJV", "EJV", "J"));
                    MaxJvRefNo = dtJvRefNo.Rows[0]["JrnNextRefNo"].ToString();
                    NewJvRefNo = ("SEJV" + Convert.ToDateTime(DateTime.Now).ToString("yy") +
                                  Convert.ToDateTime(txtIssueDate.Text.Trim()).ToString("MMM").ToUpper() + MaxJvRefNo);
                }

                #endregion

                #region InsertFa_Te_WH

                #endregion

                #region NewAccHdr
                TransactionHeaderDAO DaoHdr = new TransactionHeaderDAO();
                DaoHdr.TrnAccPeriod = trnPeriod;
                DaoHdr.TrnCurrCode = "BDT";
                DaoHdr.TrnCurrRate = 1;
                DaoHdr.TrnDATE = Convert.ToDateTime(txtIssueDate.Text.Trim());
                DaoHdr.TrnEntryDATE = DateProcess.GetServerDate(_connectionString);
                DaoHdr.TrnEntryFlag = "L";
                DaoHdr.TrnEntryUser = "SUB";
                DaoHdr.TrnJrnType = "EJV";
                DaoHdr.TrnRefNo = "";
                DaoHdr.VoucherType = "J";
                DaoHdr.ModuleName = "Accounts";
                #endregion

                List<TransactionDetailsDAO> tdDaolst = new List<TransactionDetailsDAO>();
                
                Int16 i = 1;
                int k = 1;
                foreach (DataRow row in dt.Rows)
                {
                    decimal avgVal = 0;
                    decimal stdVal = 0;
                    decimal latVal = 0;
                    decimal latRat = 0;
                    decimal avgRat = 0;
                    decimal stdRat = 0;
                    var curStk = 0;
                    double newCurStk = 0;

                    var lineNo = Convert.ToInt16(row["SR Line#"].ToString());
                    var itemCode = row["Item Code"].ToString();
                    var itemName = row["Item Name"].ToString();
                    var uom = row["UOM"].ToString();
                    var store = row["Store"].ToString();
                    var quantity = Convert.ToDecimal(row["Quantity"].ToString());
                    var rate = Convert.ToDecimal(row["Rate"].ToString());
                    var rateID = row["Rate ID"].ToString();
                    var rateIdLineNo = row["Rate Line No"].ToString();
                    var mrrno = row["MRR No"].ToString();
                    var amount = quantity * rate;

                    #region Insert_IssDet
                    DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertMRRDet("IS", TrnType, IssRefNo, i, "", 0, itemCode, itemName, uom, store, "", SRRef,
                                          lineNo, row["Serial"].ToString(), Convert.ToDateTime(txtIssueDate.Text.Trim()),
                                          Convert.ToDateTime(txtIssueDate.Text.Trim()), Convert.ToDouble(quantity),
                                          0, rate, amount, amount, quantity.ToString(), mrrno, rateID,
                                          Convert.ToInt32(rateIdLineNo), 0));
                    
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
                                DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertItemSerial(itemCode, serialNo, IssRefNo, store, "IS", TrnType,
                                                              Convert.ToDateTime(txtIssueDate.Text.Trim()), "Good",
                                                              rateID));
                            }
                        }
                        #endregion

                        #region UpdateRateQty
                        var dtItmRate = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetItmRateQtyByRateMrr(store, itemCode, rateID, Convert.ToInt32(rateIdLineNo), mrrno));
                        if (dtItmRate.Rows.Count > 0)
                        {
                            DataProcess.UpdateQuery(_connectionString, SqlgenerateForFixedAsset.UpdateRateQty((dtItmRate.Rows[0]["itm_rate_qty"].ToString() == null ? 0 : Convert.ToDecimal(dtItmRate.Rows[0]["itm_rate_qty"].ToString()) - quantity), store,
                                itemCode, rateID, Convert.ToInt32(rateIdLineNo), mrrno));
                        }
                        #endregion

                        #region UpdateSrDet
                        var dtSrDet = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetSrDetByIcode(SRRef, row["Item Code"].ToString()));
                        if (dtSrDet.Rows.Count > 0)
                        {
                            var orgQty = dtSrDet.Rows[0]["Sr_Det_Org_QTY"].ToString() == null ? 0 : Convert.ToDouble( dtSrDet.Rows[0]["Sr_Det_Org_QTY"].ToString()) + Convert.ToDouble(quantity);
                            var balQty = dtSrDet.Rows[0]["Sr_Det_Lin_Qty"].ToString() == null ? 0 : Convert.ToDouble( dtSrDet.Rows[0]["Sr_Det_Lin_Qty"].ToString()) - orgQty;
                            DataProcess.UpdateQuery(_connectionString, SqlgenerateForFixedAsset.UpdateSrQty(orgQty, balQty, "Y", SRRef, itemCode));
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

                        var dtStkVal = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetMrrDataByMrrItem(IssRefNo, itemCode, store));
                        if (dtStkVal.Rows.Count > 0)
                        {
                            DataProcess.UpdateQuery(_connectionString, SqlgenerateForFixedAsset.UpdateStkVal(Convert.ToDateTime(txtIssueDate.Text.Trim()), itemName,
                                                  latRat, avgRat, stdRat,
                                                  dtStkVal.Rows[0]["Stk_Val_Itm_Qty"].ToString() == null
                                                      ? 0
                                                      : Convert.ToDouble( dtStkVal.Rows[0]["Stk_Val_Itm_Qty"].ToString()) + Convert.ToDouble(quantity),
                                                  curStk.ToString(), "", "", "", "IS", TrnType, IssRefNo, itemCode,
                                                  store));
                        }
                        else
                        {
                            DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertStkVal("IS", TrnType, IssRefNo, Convert.ToDateTime(txtIssueDate.Text.Trim()),
                                                  itemCode,
                                                  itemName, store, rate, avgRat, stdRat, Convert.ToDouble(quantity),
                                                  curStk.ToString(), "",
                                                  "",
                                                  ""));
                            
                        }
                        #endregion

                        #region UpdateStkCtl

                        DataTable dtStkCtl = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetStkCtlByItemStore(itemCode, store));
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
                                                Convert.ToDateTime(txtIssueDate.Text.Trim()), "", "", "", 0, itemCode,
                                                store));
                        }
                        //else
                        //{
                        //    tblPopUp.Rows[2].Cells[0].InnerText = "";
                        //    lblMsgHdr.Text = "Data Processing Error.";
                        //    txtIssueRef.Visible = false;
                        //    ModalPopupExtender3.Show();
                        //    return false;
                        //}
                        #endregion

                        #region CheckFixedAssetItem
                        var faFlg = "N";
                        int qt = 0;
                        var dtItemMasDet = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetItemByCode(itemCode));
                        if (dtItemMasDet.Rows.Count > 0)
                        {
                            if (Convert.ToInt32( dtItemMasDet.Rows[0]["ItemTypeId"].ToString()) == 1)
                            {
                                faFlg = "Y";
                                string tempValue1 = null;
                                string tempValue2 = null;
                                DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertFixedAsset(IssRefNo, 0, tempValue1, tempValue2, itemCode, Convert.ToInt32(quantity), Convert.ToDecimal(amount)));
                            }
                            else
                            {
                                double maxmovref = Convert.ToDouble(tblmovement.GetMaxMovRefNo());

                                string maxmovrefno = "MOV-" + System.DateTime.Now.ToString("yyMMdd") + "-" + string.Format("{0:0000000}", maxmovref);

                                string[] serialnumber = getserialnumber(row["Serial"].ToString().Trim());
                                for (int sn = 0; sn < serialnumber.Length; sn++)
                                {
                                    if (row["Serial"].ToString() == "")
                                    {
                                        qt = Convert.ToInt32(quantity);
                                    }
                                    else
                                    {
                                        qt = 1;
                                    }

                                    tblmovement.InsertQuery(Convert.ToInt32(maxmovref), maxmovrefno, itemCode, serialnumber[sn], IssRefNo, store, "IS",
                                           TrnType, Convert.ToDateTime(txtIssueDate.Text), "Good", "", Convert.ToInt32(qt), 0,
                                          ReqFor, DateTime.Now, DateTime.Now, Session[StaticData.sessionUserId].ToString());
                                }
                            }
                        }
                        #endregion

                        #region NewAccDetCredit
                        TransactionDetailsDAO tdDaoCr = new TransactionDetailsDAO();

                        
                        var dtItemDet = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetItemByCode(itemCode));
                        tdDaoCr.TrnAcCode = dtItemDet.Rows[0]["Itm_Det_Acc_code"].ToString();
                        tdDaoCr.TrnAcDesc = DataProcess.GetSingleValueFromtable(_connectionString, "budg", "Gl_Coa_Name", "where Gl_Coa_Code='" + tdDaoCr.TrnAcCode + "'");
                        tdDaoCr.TrnAcType = DataProcess.GetSingleValueFromtable(_connectionString, "budg", "Gl_Coa_Type", "where Gl_Coa_Code='" + tdDaoCr.TrnAcCode + "'");
                        tdDaoCr.TrnAmount = Convert.ToDouble(amount);
                        tdDaoCr.TrnLineNo = k.ToString();
                        tdDaoCr.TrnMatch = "";
                        tdDaoCr.TrnNarration = itemName + " Issue qty : " + quantity + " against " + txtTkiNo.Text.Trim() + " Trn No " + IssRefNo;
                        tdDaoCr.TrnPaymentDATE = DateProcess.GetServerDate(_connectionString);
                        tdDaoCr.TrnRefNo = "";
                        tdDaoCr.TrnTrntype = "C";
                        tdDaoCr.TrnDueDATE = tdDaoCr.TrnPaymentDATE.AddDays(30);
                        tdDaoCr.TrnChequeNo = "";
                        tdDaoCr.TrnGRNNo = IssRefNo;

                        tdDaolst.Add(tdDaoCr);
                        #endregion
                        
                        #region NewAccDetDebit
                        TransactionDetailsDAO tdDaoDr = new TransactionDetailsDAO();

                        var debitAccCode = "Dummy Account";

                        var dtIssType = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetDataIssueType());
                        if (faFlg == "Y")
                        {
                            dtIssType = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetFaAccHead());
                            if (dtIssType.Rows.Count > 0)
                            {
                                debitAccCode = dtIssType.Rows[0]["Iss_Type_Acc_Code"].ToString();
                            }
                        }
                        else
                        {
                            dtIssType = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetIssueTypeById(issueType));
                            if (dtIssType.Rows.Count > 0)
                            {
                                debitAccCode = dtIssType.Rows[0]["Iss_Type_Acc_Code"].ToString();
                            }
                        }

                        if (TrnType == "SRS") debitAccCode = locId;

                        tdDaoDr.TrnAcCode = debitAccCode;
                        tdDaoDr.TrnAcDesc = DataProcess.GetSingleValueFromtable(_connectionString, "budg", "Gl_Coa_Name", "where Gl_Coa_Code='" + tdDaoDr.TrnAcCode + "'");
                        tdDaoDr.TrnAcType = DataProcess.GetSingleValueFromtable(_connectionString, "budg", "Gl_Coa_Type", "where Gl_Coa_Code='" + tdDaoDr.TrnAcCode + "'");
                        tdDaoDr.TrnAmount = Convert.ToDouble(amount);
                        tdDaoDr.TrnLineNo = (k + 1).ToString();
                        tdDaoDr.TrnMatch = "";
                        tdDaoDr.TrnNarration = itemName + " Issue qty : " + quantity + " against " + txtTkiNo.Text.Trim() + " Trn No " + IssRefNo;
                        tdDaoDr.TrnPaymentDATE = DateProcess.GetServerDate(_connectionString);
                        tdDaoDr.TrnRefNo = "";
                        tdDaoDr.TrnTrntype = "D";
                        tdDaoDr.TrnDueDATE = tdDaoDr.TrnPaymentDATE.AddDays(30);
                        tdDaoDr.TrnChequeNo = "";
                        tdDaoDr.TrnGRNNo = IssRefNo;

                        tdDaolst.Add(tdDaoDr);
                        #endregion

                    }
                    i++;
                    k = k + 2;
                }

                #region UpdateJvRefNo
                #endregion

                TransactionEntryBLL tBll = new TransactionEntryBLL();

                //string str = tBll.saveData(_connectionString, DaoHdr, tdDaolst, false);
                string[] str;
                str = new string[2];  
                
                str = tBll.PostData(_connectionString, DaoHdr, tdDaolst);


                if (str[0] != "")
                {
                    tran.Commit();
                    lblMsgHdr.Text = HPFlag == "H" ? "Successfully Holded" : "Successfull Posted";
                    tblPopUp.Rows[2].Cells[0].InnerText = "Issue Ref No";
                    txtIssueRef.Text = IssRefNo;
                    txtIssueRef.Visible = true;
                    ModalPopupExtender3.Show();
                    ClearFieldData("P");
                    return true;
                }
                else
                {
                    tran.Rollback();
                    tblPopUp.Rows[2].Cells[0].InnerText = "";
                    lblMsgHdr.Text = "Data Processing Error.";
                    txtIssueRef.Visible = false;
                    ModalPopupExtender3.Show();
                    return false;
                }
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
            lblMsgHdr.Text = "Data Processing Error. \n" + ex.Message;
            txtIssueRef.Visible = false;
            ModalPopupExtender3.Show();
            return false;
        }
        finally
        {
            conn.Close();
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
    
    protected void Button1_Click(object sender, EventArgs e)
    {
        //string[] str = txtIssueSearch.Text.Split(':');   
        //Session["Link3IssueReportSession"] = str[0].ToString();
        //Response.Redirect("~/ClientSide/Inventory/frmIssueReport.aspx"); 
    }
}
