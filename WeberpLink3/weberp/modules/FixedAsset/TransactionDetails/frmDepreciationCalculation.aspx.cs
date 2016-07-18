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
using LibraryDAL.dsInventory.dsInventoryMasterTableAdapters;
using LibraryDAL.dsInventory.dsInventoryTransactionTableAdapters;

public partial class modules_FixedAsset_TransactionDetails_frmDepreciationCalculation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        StaticData.MsgConfirmBox(btnCalcCulate, "Are you sure want to calculate depreciation ? ");
        
        StaticData.MsgConfirmBox(btnJvpost, "Are you sure want to post the JV ? ");
                       
        if (!Page.IsPostBack)
        {
            LoadDepreciationCalculationdate();
            //Loadjvdate();

            lblCal.Text = "";
            btnExportActiveEmployee.Visible = false; 
           
        }

        

    }

    private void LoadDepreciationCalculationdate()
    {       
       
        string ConnectionStr = System.Configuration.ConfigurationManager.AppSettings["L3TConnectionString"].ToString();

        bool usersectdt=false;

        DateTime maxdt,StartDate,EndDate;
              

        string sql = "select MAX(DepreciationDate) as depdt from FAS_Item_Depreciation";

        DataTable dt = GetDatafromTable(ConnectionStr, sql);

        

        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["depdt"].ToString() == "")
            {
                maxdt = Convert.ToDateTime("01/07/2012");
                usersectdt = true;
            }
            else
            {
                maxdt = Convert.ToDateTime(dt.Rows[0]["depdt"].ToString());
            }
            
        }
        else
        {
            maxdt = Convert.ToDateTime("01/07/2012");
            usersectdt=true;
        }      
        
        StartDate = Convert.ToDateTime(Convert.ToDateTime(maxdt).AddDays(1));        
        EndDate = Convert.ToDateTime(Convert.ToDateTime(StartDate).AddMonths(3)).AddDays(-1);


        if (usersectdt == false)
        {
            txtFromDate.Text = Convert.ToString(StartDate.ToShortDateString());
            txtToDate.Text = Convert.ToString(EndDate.ToShortDateString());
            txtFromDate.Enabled = false;
            txtToDate.Enabled = false;
        }
        else
        {
            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy"); 
        }

       
 
    }
    private void Loadjvdate()
    {       
        string ConnectionStr = System.Configuration.ConfigurationManager.AppSettings["L3TConnectionString"].ToString();

        bool usersectdt = false;

        DateTime jvdate;


        string sql = "select min(DepreciationDate) as jvdate from FAS_Item_Depreciation where isjvpost='N'";

        DataTable dt = GetDatafromTable(ConnectionStr, sql);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["jvdate"].ToString() == "")
            {
                jvdate = Convert.ToDateTime("30/09/2012");
                usersectdt = true;
            }
            else
            {
                jvdate = Convert.ToDateTime(dt.Rows[0]["jvdate"].ToString());
            }
            
        }
        else
        {
            jvdate = Convert.ToDateTime("30/09/2012");
            usersectdt = true;
        }    
        
        if (usersectdt == false)
        {
            txtJvdate.Text = Convert.ToString(jvdate.ToShortDateString());
            txtJvdate.Enabled = false;            
        }
        else
        {
            txtJvdate.Text = DateTime.Now.ToString("dd/MM/yyyy");           
        }       
 
    }

    private void LoadInitGrid()
    {
        var dt = new DataTable();
        dt.Rows.Clear();
        //dt.Columns.Add("LineNo",typeof(string));
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
    private void ClearFieldData(string Pst_Flg)
    {
        if (Pst_Flg == "P" || Pst_Flg == "H")
        {
            lblEditFlag.Text = "N";

            txtPoSearch.Text = "";
            txtPoSearch.Enabled = true;
            btnSearch.Enabled = true;
            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtRemarks.Text = "";
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        var taPoHdr = new PuTr_PO_HdrTableAdapter();
        var taPoDet = new PuTr_PO_DetTableAdapter();
        var taSupp = new PuMa_Par_AccTableAdapter();

        try
        {
            var PORef = "";
            if (txtPoSearch.Text.ToUpper() != "")
            {
                try
                {
                    String[] temp = txtPoSearch.Text.Split(':');
                    if (temp.Length < 2) return;
                    PORef = temp[0];
                }
                catch (Exception)
                {

                    //throw;
                }
            }
            var dtPoHdr = taPoHdr.GetPOByRefNo(PORef);
            if (dtPoHdr.Rows.Count > 0)
            {
                var dtSupp = taSupp.GetSupplierByCode(dtPoHdr[0].PO_Hdr_Pcode);
                var supplier = dtPoHdr[0].PO_Hdr_Pcode + ":" + dtSupp[0].Par_Acc_Name;
                txtFromDate.Text = dtPoHdr[0].PO_Hdr_DATE.ToString("dd/MM/yyyy");
                txtToDate.Text = dtPoHdr[0].po_hdr_due_date.ToString("dd/MM/yyyy");
                txtRemarks.Text = dtPoHdr[0].PO_Hdr_Com1;

                var dt = new DataTable();
                LoadInitGrid();
                dt = (DataTable)ViewState["datatable"];

                var dtPoDet = taPoDet.GetPODetByRefNo(PORef);
                int i = 0;
                foreach (DataRow row in dtPoDet.Rows)
                {
                    dt.Rows.Add(dtPoDet[i].PO_Det_Icode, dtPoDet[i].PO_Det_Itm_Desc,
                                dtPoDet[i].PO_Det_Itm_Uom,
                                dtPoDet[i].PO_Det_Str_Code, dtPoDet[i].PO_Det_Lin_Qty.ToString("##.##"),
                                dtPoDet[i].PO_Det_Lin_Rat.ToString("##.##"),
                                dtPoDet[i].PO_Det_Lin_Amt.ToString("##.##"));
                    ViewState["datatable"] = dt;
                    SetGridData();
                    i++;
                }

                txtPoSearch.Enabled = false;
                btnSearch.Enabled = false;
                btnViewall.Visible = true;


                // Expand);
                this.CollapsiblePanelExtenderHdr.Collapsed = false;
                this.CollapsiblePanelExtenderHdr.ClientState = "false";
                // Collapse
            }
        }
        catch (Exception)
        {
            //throw;
        }
    }
    protected void btnCalcCulate_Click(object sender, EventArgs e)
    {
        // Expand
       // this.CollapsiblePanelExtenderHdr.Collapsed = true;
       // this.CollapsiblePanelExtenderHdr.ClientState = "true";
        // Collapse

        DateTime dtfrom = Convert.ToDateTime(txtFromDate.Text);
        DateTime dtto = Convert.ToDateTime(txtToDate.Text);
        string entryuser = Session[StaticData.sessionUserId].ToString();
        //string entryuser = "ADM";
                        
        int prd = returnperiod(dtfrom);
        int prd1 = returnperiod(dtto);

        if (prd1 == 0)
        {
            txtRemarks.Text = "Invalid Date duration";
            return;
        }
        else
        {
            txtRemarks.Text = "";
        }



        SqlConnection sqlConn = null;

        int noOfRowsAffected = 0;     

        string ConnectionStr = System.Configuration.ConfigurationManager.AppSettings["L3TConnectionString"].ToString();

        try
        {           
            string sql = "exec procFixedAssetItemDepreciation '" + dtfrom.ToShortDateString() + "','" + dtto.ToShortDateString() + "','" + entryuser + "'";
            sqlConn = new SqlConnection(ConnectionStr);
            sqlConn.Open();
            SqlCommand sqlCom = new SqlCommand(sql, sqlConn);
            sqlCom.CommandTimeout = 600;
            noOfRowsAffected = sqlCom.ExecuteNonQuery();
            
            LoadDepreciationCalculationdate();

            lblCal.Text = "Depreciation Calculation Successfull from '" + txtFromDate.Text + "' to '" + txtToDate.Text + "'";
                      

        }
        catch (SqlException sqlExceptionObject)
        {            
            if (sqlExceptionObject.Number == 2627)
            {
                System.Windows.Forms.MessageBox.Show(sqlExceptionObject.Message);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show(sqlExceptionObject.Message);
            }
        }
        catch (Exception exceptionObject)
        {
            throw exceptionObject;
        }
        finally
        {
            if (sqlConn.State == System.Data.ConnectionState.Open)
            {
                btnCalcCulate.Enabled = true;
                sqlConn.Close();
            }
        }

    }
    private int returnperiod(DateTime dt)
    {
        int prd = 0;
        int monthno = Convert.ToDateTime(dt).Month;
        if (monthno == 9) //July-sep 1st quarter
        { prd = 1; }
        else if (monthno == 12)  //Oct-Dec 2nd qarter
        { prd = 2; }
        else if (monthno == 3)//Oct-Dec 3rd qarter
        { prd = 3; }
        else if (monthno == 6) //Oct-Dec 4th qarter
        { prd = 4; }

        return prd;
    }

    private DataTable GetDatafromTable(string ConnectionStr, string sql)
    {
        DataTable dt = new DataTable();
        SqlConnection sqlConn = null;

        try
        {
            sqlConn = new SqlConnection(ConnectionStr);
            sqlConn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sql, ConnectionStr);
            da.Fill(dt);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (sqlConn.State == System.Data.ConnectionState.Open)
            {
                sqlConn.Close();
            }
        }

        return dt;


    }
    protected void btnViewall_Click(object sender, EventArgs e)
    {
        string ConnectionStr = System.Configuration.ConfigurationManager.AppSettings["L3TConnectionString"].ToString();        

        string sql = "select left(b.itm_det_date,12) as Purchase_date,a.ItemCode,left(a.DepreciationDate,12) as [Dep. Date],1 as Qty,convert(decimal,a.ItemInitialValue) as [Unit Price],convert(decimal,a.ThisPrdOpeningValue) as [Balance],"
                   + " convert(decimal,a.OpeningDepreciationAmt) as Opening,convert(decimal,a.Addition) as Addition,convert(decimal,a.TotalDepreciationAmt) as Accumulated,convert(decimal,WrittenDownValue) as [Written Down Value]"
                   + " from FAS_Item_Depreciation a inner join FAS_InMa_Itm_Serial b on a.TrackingInfo=b.TrackingInfo where a.ItemCurrentLine='Y'";

        DataTable dt = GetDatafromTable(ConnectionStr, sql);

        GridView1.DataSource = dt;
        GridView1.DataBind();

    }
    protected void btnJvpost_Click(object sender, EventArgs e)
    {
        if (txtJvdate.Text == "") return;
               
        string ConnectionStr = System.Configuration.ConfigurationManager.AppSettings["L3TConnectionString"].ToString();

        int prd = 0;
        int monthno = Convert.ToDateTime(txtJvdate.Text).Month;
        if (monthno == 9) //July-sep 1st quarter
        { prd = 1; }
        else if (monthno == 12)  //Oct-Dec 2nd qarter
        { prd = 2; }
        else if (monthno == 3)//Oct-Dec 3rd qarter
        { prd = 3; }
        else if (monthno == 6) //Oct-Dec 4th qarter
        { prd = 4; }

        bool Postdata = false;

        TransactionHeaderDAO thDao = new TransactionHeaderDAO();
        thDao.TrnAccPeriod = Convert.ToDateTime(txtJvdate.Text).Year + "/" + String.Format("{0:00}", Convert.ToDateTime(txtJvdate.Text).Month);
        thDao.TrnCurrCode = "BDT";
        thDao.TrnCurrRate = 1;
        thDao.TrnDATE = Convert.ToDateTime(txtJvdate.Text);
        thDao.TrnEntryDATE = DateProcess.GetServerDate(ConnectionStr);
        thDao.TrnEntryFlag = "T";
        thDao.TrnEntryUser = "ADM";
        thDao.TrnJrnType = "JV";
        thDao.TrnRefNo = "";
        thDao.VoucherType = "J";
        thDao.ModuleName = "Accounts";

        //Details 
        List<TransactionDetailsDAO> tdDaolst = new List<TransactionDetailsDAO>();

        int lineno = 0;

        string yearof = Convert.ToString(Convert.ToDateTime(txtJvdate.Text).Year - 1) + "-" + Convert.ToString(Convert.ToDateTime(txtJvdate.Text).Year);


        for (int j = 0; j <= 2; j++)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                lineno = lineno + 1;

                if (j == 0) // Depreciation account "D"
                {
                    TransactionDetailsDAO tdDao = new TransactionDetailsDAO();

                    tdDao.TrnAcCode = GridView1.Rows[i].Cells[3].Text.ToString();
                    tdDao.TrnAcDesc = DataProcess.GetSingleValueFromtable(ConnectionStr, "budg", "Gl_Coa_Name", "where Gl_Coa_Code='" + tdDao.TrnAcCode + "'");
                    tdDao.TrnAcType = DataProcess.GetSingleValueFromtable(ConnectionStr, "budg", "Gl_Coa_Type", "where Gl_Coa_Code='" + tdDao.TrnAcCode + "'");
                    tdDao.TrnAmount = Convert.ToDouble(GridView1.Rows[i].Cells[6].Text.ToString());  //this period depreciation amount
                    tdDao.TrnLineNo = lineno.ToString();
                    tdDao.TrnMatch = "";
                    tdDao.TrnNarration = "Amount charges as depreciation on" + " " + GridView1.Rows[i].Cells[2].Text.ToString() + " For the year of " + yearof;
                    tdDao.TrnPaymentDATE = DateProcess.GetServerDate(ConnectionStr);
                    tdDao.TrnRefNo = "";
                    tdDao.TrnTrntype = "D";
                    tdDao.TrnDueDATE = tdDao.TrnPaymentDATE.AddDays(30);
                    tdDao.TrnChequeNo = "";

                    tdDaolst.Add(tdDao);


                }
                else if (j == 1 && prd < 4)  // before ending financial year provision depreciation account "C"
                {
                    TransactionDetailsDAO tdDao = new TransactionDetailsDAO();

                    tdDao.TrnAcCode = GridView1.Rows[i].Cells[4].Text.ToString();
                    tdDao.TrnAcDesc = DataProcess.GetSingleValueFromtable(ConnectionStr, "budg", "Gl_Coa_Name", "where Gl_Coa_Code='" + tdDao.TrnAcCode + "'");
                    tdDao.TrnAcType = DataProcess.GetSingleValueFromtable(ConnectionStr, "budg", "Gl_Coa_Type", "where Gl_Coa_Code='" + tdDao.TrnAcCode + "'");
                    tdDao.TrnAmount = Convert.ToDouble(GridView1.Rows[i].Cells[6].Text.ToString()); // this period Depreciation Amount
                    tdDao.TrnLineNo = lineno.ToString();
                    tdDao.TrnMatch = "";
                    tdDao.TrnNarration = "Provision made against depreciation on " + GridView1.Rows[i].Cells[2].Text.ToString() + "For the year of " + yearof;
                    tdDao.TrnPaymentDATE = DateProcess.GetServerDate(ConnectionStr);
                    tdDao.TrnRefNo = "";
                    tdDao.TrnTrntype = "C";
                    tdDao.TrnDueDATE = tdDao.TrnPaymentDATE.AddDays(30);
                    tdDao.TrnChequeNo = "";

                    tdDaolst.Add(tdDao);


                }
                else if (j == 1 && prd == 4) //ending of financial year provision depreciation account "D" 
                {
                    TransactionDetailsDAO tdDao = new TransactionDetailsDAO();

                    tdDao.TrnAcCode = GridView1.Rows[i].Cells[4].Text.ToString();
                    tdDao.TrnAcDesc = DataProcess.GetSingleValueFromtable(ConnectionStr, "budg", "Gl_Coa_Name", "where Gl_Coa_Code='" + tdDao.TrnAcCode + "'");
                    tdDao.TrnAcType = DataProcess.GetSingleValueFromtable(ConnectionStr, "budg", "Gl_Coa_Type", "where Gl_Coa_Code='" + tdDao.TrnAcCode + "'");
                    tdDao.TrnAmount = Convert.ToDouble(GridView1.Rows[i].Cells[7].Text.ToString());  // Previous period total Provision Amount
                    tdDao.TrnLineNo = lineno.ToString();
                    tdDao.TrnMatch = "";
                    tdDao.TrnNarration = "Provision made against depreciation on " + GridView1.Rows[i].Cells[2].Text.ToString() + "For the year of " + yearof;
                    tdDao.TrnPaymentDATE = DateProcess.GetServerDate(ConnectionStr);
                    tdDao.TrnRefNo = "";
                    tdDao.TrnTrntype = "D";
                    tdDao.TrnDueDATE = tdDao.TrnPaymentDATE.AddDays(30);
                    tdDao.TrnChequeNo = "";

                    tdDaolst.Add(tdDao);


                }
                else if (j == 2 && prd == 4) //ending of financial year Accomulated depreciation account "C"
                {
                    TransactionDetailsDAO tdDao = new TransactionDetailsDAO();

                    tdDao.TrnAcCode = GridView1.Rows[i].Cells[5].Text.ToString();
                    tdDao.TrnAcDesc = DataProcess.GetSingleValueFromtable(ConnectionStr, "budg", "Gl_Coa_Name", "where Gl_Coa_Code='" + tdDao.TrnAcCode + "'");
                    tdDao.TrnAcType = DataProcess.GetSingleValueFromtable(ConnectionStr, "budg", "Gl_Coa_Type", "where Gl_Coa_Code='" + tdDao.TrnAcCode + "'");
                    tdDao.TrnAmount = Convert.ToDouble(GridView1.Rows[i].Cells[6].Text.ToString()) + Convert.ToDouble(GridView1.Rows[i].Cells[7].Text.ToString()); // Previous period provision + this period depreciation amt
                    tdDao.TrnLineNo = lineno.ToString();
                    tdDao.TrnMatch = "";
                    tdDao.TrnNarration = "Provision made against depreciation on " + GridView1.Rows[i].Cells[2].Text.ToString() + "For the year of " + yearof;
                    tdDao.TrnPaymentDATE = DateProcess.GetServerDate(ConnectionStr);
                    tdDao.TrnRefNo = "";
                    tdDao.TrnTrntype = "C";
                    tdDao.TrnDueDATE = tdDao.TrnPaymentDATE.AddDays(30);
                    tdDao.TrnChequeNo = "";

                    tdDaolst.Add(tdDao);

                }

            }
        }

        TransactionEntryBLL tBll = new TransactionEntryBLL();
        if (Postdata)
        {
            string[] str = tBll.PostData(ConnectionStr, thDao, tdDaolst);
            if (str[0] != "")
            {
                //if (StringProcess.Left(txtReferenceNo.Text.ToString(), 1) == "S") DeleteSaveData(txtReferenceNo.Text);
                //MessageBox.Show(StringProcess.postedMessage, StringProcess.messageHead);
                //TransactionPostRefNofrm rf = new TransactionPostRefNofrm(str);
                //rf.ShowDialog();
                //ClearForm();
                //GetRefNo();
                //dgvJournalVoucher.DataSource = null;
            }

        }
        else
        {
            bool updateFlg = false;

            string refNo = tBll.saveData(ConnectionStr, thDao, tdDaolst, updateFlg);

            if (refNo != "")
            {               
                GridView1.DataSource = null;
                DataProcess.ExecuteQuery(ConnectionStr, "update FAS_Item_Depreciation set ISJVPOST='Y' where Convert(Datetime,DepreciationDate,103)=Convert(Datetime,'" + Convert.ToDateTime(txtJvdate.Text) + "',103)");
                Loadjvdate();
            }
        }

    }
    protected void btnShowJV_Click(object sender, EventArgs e)
    {        
        string ConnectionStr = System.Configuration.ConfigurationManager.AppSettings["L3TConnectionString"].ToString();
        string sql = "";
        DateTime provstdate, provenddate;

        int prd = 0;
        int monthno = Convert.ToDateTime(txtJvdate.Text).Month;
        if (monthno == 9) //July-sep 1st quarter
        { prd = 1; }
        else if (monthno == 12)  //Oct-Dec 2nd qarter
        { prd = 2; }
        else if (monthno == 3)//Oct-Dec 3rd qarter
        { prd = 3; }
        else if (monthno == 6) //Oct-Dec 4th qarter
        { prd = 4; }

        DateTime trnjvdate = Convert.ToDateTime(txtJvdate.Text);

        provstdate = Convert.ToDateTime(Convert.ToDateTime(txtJvdate.Text).AddMonths(prd * (-3))).AddDays(1);
        provenddate = Convert.ToDateTime(Convert.ToDateTime(provstdate).AddMonths(prd * (3) - 3)).AddDays(-1);

        if (provenddate < provstdate)
        {
            provenddate = provstdate;
        }

        //string sql = "select a.DepreciationDate as [Transaction Date],b.Fxd_Second_Grp as [Group Code],c.Ccg_Name as [Group Name],b.Dpre_Acc_code as [Depreciation A/C],b.Pro_Depre_Acc_code as [Provision Dep A/C],b.Accu_Depre_Acc_Code as [Accumulated Dep A/C],sum(a.Addition) as [Dep Amount] from FAS_Item_Depreciation a" 
        //            + " inner join FAS_FixedAssetSetUp b on a.ItemCode=b.Itm_Grp_icode" 
        //            + " inner join View_FixedAssetSecondGroup c on c.Coa_Grp_Code=b.Fxd_Second_Grp"
        //            + " where ItemCurrentLine='Y' and a.ISJVPOST='N'"
        //            + " group by a.DepreciationDate,b.Fxd_Second_Grp,c.Ccg_Name,b.Dpre_Acc_code,b.Pro_Depre_Acc_code,b.Accu_Depre_Acc_Code";


        //View for provision depreciation of previous period
        DataProcess.ExecuteQuery(ConnectionStr, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[view_Depreciation_Provision]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[view_Depreciation_Provision]");

        sql = "CREATE view [dbo].[view_Depreciation_Provision] as "
                    + " select b.Fxd_Second_Grp as [GroupCode],c.Ccg_Name as [GroupName],"
                    + " b.Dpre_Acc_code as [DepreciationAC],b.Pro_Depre_Acc_code as [ProvisionDepAC],b.Accu_Depre_Acc_Code as [AccumulatedDepAC],"
                    + " sum(a.Addition) as [DepAmount] from FAS_Item_Depreciation a "
                    + " inner join FAS_FixedAssetSetUpNew b on a.ItemCode=b.Itm_Grp_icode and a.TrackingInfo=b.TrackingInfo "
                    + " inner join View_FixedAssetSecondGroup c on c.Coa_Grp_Code=b.Fxd_Second_Grp "
                    + " where ItemCurrentLine='N' and Convert(Datetime,DepreciationPeriodTo,103) between CONVERT(datetime,'" + provstdate + "',103) and  CONVERT(datetime,'" + provenddate + "',103)"
                    + " group by b.Fxd_Second_Grp,c.Ccg_Name,b.Dpre_Acc_code,b.Pro_Depre_Acc_code,b.Accu_Depre_Acc_Code";

        DataProcess.ExecuteQuery(ConnectionStr, sql);


        //View for depreciation of current period

        DataProcess.ExecuteQuery(ConnectionStr, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[View_Current_Period_depreciation]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[View_Current_Period_depreciation]");

        sql = "CREATE view [dbo].[View_Current_Period_depreciation] as "
            + " select a.DepreciationDate as [TransactionDate],b.Fxd_Second_Grp as [GroupCode],c.Ccg_Name as [GroupName],"
            + " b.Dpre_Acc_code as [DepreciationAC],b.Pro_Depre_Acc_code as [ProvisionDepAC],b.Accu_Depre_Acc_Code as [AccumulatedDepAC],"
            + " sum(a.Addition) as [DepAmount] from FAS_Item_Depreciation a "
            + " inner join FAS_FixedAssetSetUpNew b on a.ItemCode=b.Itm_Grp_icode and a.TrackingInfo=b.TrackingInfo"
            + " inner join View_FixedAssetSecondGroup c on c.Coa_Grp_Code=b.Fxd_Second_Grp"
            + " where DepreciationDate=CONVERT(Datetime,'" + trnjvdate + "',103) and a.ISJVPOST='N' group by a.DepreciationDate,b.Fxd_Second_Grp,c.Ccg_Name,b.Dpre_Acc_code,b.Pro_Depre_Acc_code,b.Accu_Depre_Acc_Code";

        DataProcess.ExecuteQuery(ConnectionStr, sql);

        sql = "select a.[TransactionDate] as [Trn. Date],a.[GroupCode] as [Group Code],a.[GroupName] as [Group Name],"
                    + " a.[DepreciationAC] as [Depreciation A/C],a.[ProvisionDepAC] as [ProvisionDep A/C]"
                    + " ,a.[AccumulatedDepAC] as [AccumulatedDep A/C],a.[DepAmount] as [Dep amt],isnull(b.[DepAmount],0) as [Provision amt]"
                    + " from [View_Current_Period_depreciation] a left outer join [view_Depreciation_Provision] b on a.[GroupCode]=b.[GroupCode]";

        DataTable dt = GetDatafromTable(ConnectionStr, sql);

        GridView1.DataSource = dt;
        GridView1.DataBind();

    }
    protected void BtnDepSumary_Click(object sender, EventArgs e)
    {        
        string ConnectionStr = System.Configuration.ConfigurationManager.AppSettings["L3TConnectionString"].ToString();
        string sql = "";
        if (rdoOption.SelectedItem.Value == "History")
        {
            sql = "select left(b.itm_det_date,12) as [Purchase Date],a.ItemCode as [Item Code],a.TrackingInfo as [Tracking No],c.Fxd_Acc_code as [Account Code],left(a.DepreciationDate,12) as [Dep. Date],1 as Qty,convert(decimal(18,2),a.ItemInitialValue) as [Unit Price],convert(decimal(18,2),a.ThisPrdOpeningValue) as [Balance],"
                       + " convert(decimal(18,2),a.OpeningDepreciationAmt) as Opening,convert(decimal(18,2),a.Addition) as Addition,convert(decimal(18,2),a.TotalDepreciationAmt) as Accumulated,convert(decimal(18,2),WrittenDownValue) as [Written Down Value]"
                       + " from FAS_Item_Depreciation a inner join FAS_InMa_Itm_Serial b on a.TrackingInfo=b.TrackingInfo inner join FAS_FixedAssetSetUpNew c on  c.Itm_Grp_icode=a.ItemCode and c.TrackingInfo=a.TrackingInfo order by a.TrackingInfo,ItemDepreciationSL ";

        }
        else
        {
            sql = "select left(b.itm_det_date,12) as [Purchase Date],a.ItemCode as [Item Code],a.TrackingInfo as [Tracking No],c.Fxd_Acc_code as [Account Code],left(a.DepreciationDate,12) as [Dep. Date],1 as Qty,convert(decimal(18,2),a.ItemInitialValue) as [Unit Price],convert(decimal(18,2),a.ThisPrdOpeningValue) as [Balance],"
                      + " convert(decimal(18,2),a.OpeningDepreciationAmt) as Opening,convert(decimal(18,2),a.Addition) as Addition,convert(decimal(18,2),a.TotalDepreciationAmt) as Accumulated,convert(decimal(18,2),WrittenDownValue) as [Written Down Value]"
                      + " from FAS_Item_Depreciation a inner join FAS_InMa_Itm_Serial b on a.TrackingInfo=b.TrackingInfo inner join FAS_FixedAssetSetUpNew c on  c.Itm_Grp_icode=a.ItemCode and c.TrackingInfo=a.TrackingInfo where a.ItemCurrentLine='Y' order by a.TrackingInfo";

        }

        DataTable dt = GetDatafromTable(ConnectionStr, sql);
        GridView1.DataSource = dt;
        GridView1.DataBind();

        if (dt.Rows.Count > 0)
        {
            btnExportActiveEmployee.Visible = true;
        }
        else
        {
            btnExportActiveEmployee.Visible = false; 
        }

    }

    protected void btnExportActiveEmployee_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count != 0)
        {
            string type = "DepreciationData.xls";
            Export(type, GridView1);
        }
        else
        {
            string validationMsg = "There is no data for Export ! ";
            ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + validationMsg + "');",
                    true);
        }
    }

    public void Export(string fileName, GridView gv)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader(
            "content-disposition", string.Format("attachment; filename={0}", fileName));
        HttpContext.Current.Response.ContentType = "application/ms-excel";

        using (System.IO.StringWriter sw = new System.IO.StringWriter())
        {
            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            {
                System.Web.UI.WebControls.Table table = new System.Web.UI.WebControls.Table();
                table.GridLines = gv.GridLines;
                if (gv.HeaderRow != null)
                {
                    PrepareControlForExport(gv.HeaderRow);
                    table.Rows.Add(gv.HeaderRow);
                }
                foreach (GridViewRow row in gv.Rows)
                {
                    PrepareControlForExport(row);
                    table.Rows.Add(row);
                }
                if (gv.FooterRow != null)
                {
                    PrepareControlForExport(gv.FooterRow);
                    table.Rows.Add(gv.FooterRow);
                }
                table.RenderControl(htw);
                HttpContext.Current.Response.Write(sw.ToString());
                HttpContext.Current.Response.End();
            }
        }
    }

    private static void PrepareControlForExport(Control control)
    {
        for (int i = 0; i < control.Controls.Count; i++)
        {
            Control current = control.Controls[i];
            if (current is LinkButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
            }
            else if (current is ImageButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
            }
            else if (current is HyperLink)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
            }
            else if (current is DropDownList)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
            }
            else if (current is CheckBox)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
            }
            else if (current is Button)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as Button).Text));
            }

            if (current.HasControls())
            {
                PrepareControlForExport(current);
            }
        }
    }
    protected void BtnViewDepreciation_Click(object sender, EventArgs e)
    {
        DateTime startdate = Convert.ToDateTime(System.DateTime.Now);
        DateTime enddate = Convert.ToDateTime(System.DateTime.Now);

        string ConnectionStr = System.Configuration.ConfigurationManager.AppSettings["L3TConnectionString"].ToString();
        string sql = "";
        if (rdoOpt.SelectedItem.Value == "History")
        {
            startdate = Convert.ToDateTime(txtDepFromDate.Text);
            enddate = Convert.ToDateTime(txtDepToDate.Text);

            sql = "select left(b.itm_det_date,12) as [Purchase Date],a.ItemCode as [Item Code],a.TrackingInfo as [Tracking No],c.Fxd_Acc_code as [Account Code],left(a.DepreciationDate,12) as [Dep. Date],1 as Qty,convert(decimal(18,2),a.ItemInitialValue) as [Unit Price],convert(decimal(18,2),a.ThisPrdOpeningValue) as [Balance],"
                       + " convert(decimal(18,2),a.OpeningDepreciationAmt) as Opening,convert(decimal(18,2),a.Addition) as Addition,convert(decimal(18,2),a.TotalDepreciationAmt) as Accumulated,convert(decimal(18,2),WrittenDownValue) as [Written Down Value]"
                       + " from FAS_Item_Depreciation a inner join FAS_InMa_Itm_Serial b on a.TrackingInfo=b.TrackingInfo inner join FAS_FixedAssetSetUpNew c on  c.Itm_Grp_icode=a.ItemCode and c.TrackingInfo=a.TrackingInfo  where DepreciationPeriodFrom >=CONVERT(Datetime,'" + startdate + "',103)  and DepreciationPeriodTo <= CONVERT(Datetime,'" + enddate + "',103) order by a.TrackingInfo,ItemDepreciationSL ";

        }
        else
        {
            sql = "select left(b.itm_det_date,12) as [Purchase Date],a.ItemCode as [Item Code],a.TrackingInfo as [Tracking No],c.Fxd_Acc_code as [Account Code],left(a.DepreciationDate,12) as [Dep. Date],1 as Qty,convert(decimal(18,2),a.ItemInitialValue) as [Unit Price],convert(decimal(18,2),a.ThisPrdOpeningValue) as [Balance],"
                      + " convert(decimal(18,2),a.OpeningDepreciationAmt) as Opening,convert(decimal(18,2),a.Addition) as Addition,convert(decimal(18,2),a.TotalDepreciationAmt) as Accumulated,convert(decimal(18,2),WrittenDownValue) as [Written Down Value]"
                      + " from FAS_Item_Depreciation a inner join FAS_InMa_Itm_Serial b on a.TrackingInfo=b.TrackingInfo inner join FAS_FixedAssetSetUpNew c on  c.Itm_Grp_icode=a.ItemCode and c.TrackingInfo=a.TrackingInfo where a.ItemCurrentLine='Y' order by a.TrackingInfo";

        }

        DataTable dt = GetDatafromTable(ConnectionStr, sql);
        GridView1.DataSource = dt;
        GridView1.DataBind();

        if (dt.Rows.Count > 0)
        {
            btnExportActiveEmployee.Visible = true;
        }
        else
        {
            btnExportActiveEmployee.Visible = false;
        }
    }
}
