using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Dataaccess;

public partial class modules_inventory_StoreReturn : System.Web.UI.Page
{
    public SqlConnection con;
    public SqlCommand cmd;
    public SqlDataReader dr;

    string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            L3TConnection common = new L3TConnection();
            con = common.init();

            string sbb = @"select  stuff('000000',7-len(ISNULL(max (right(Trn_Hdr_Ref,6)),0)+1),20,ISNULL(max (right(Trn_Hdr_Ref,6)),0)+1) as MaxID from dbo.InTr_Trn_Hdr where trn_hdr_type='RT' and trn_hdr_code='SRT'";
            cmd = new SqlCommand(sbb, con);
            dr = cmd.ExecuteReader();
            dr.Read();
            string kl = (dr.GetSqlString(0).IsNull) ? "" : (string)dr.GetSqlString(0);
            dr.Close();
            con.Close();
            con.Dispose();

            DateTime dt = DateTime.Now;
            mo.Text = "SRT" + dt.ToString("yyMM") + "-" + kl;
            max.Text = dt.ToString("MM/yyyy");
            lblCurrentPeriod.Text = "Current Period: " + dt.ToString("MMMM-yy");
            txtreturndate.Text = dt.ToString("dd/MM/yyyy");
            Session[StaticData.sessionUserId] = ""; //Need Change
            loadstore();
            LoadInitIssueGrid();
            
        }
    }

    private void loadstore()
    {
        L3TConnection common = new L3TConnection();
        con = common.init();
        string mkg = @" SELECT *  FROM View_Str_Loc_Per where T_C1='R' and Emp_Id='" + Session[StaticData.sessionUserId].ToString() + "'";
        cmd = new SqlCommand(mkg, con);
        dr = cmd.ExecuteReader();
        ddReturnstore.Items.Add("--Select--");
        ListItem lst;
        while (dr.Read())
        {
            lst = new ListItem();
            lst.Value = dr["Str_Loc_Id"].ToString();
            lst.Text = dr["Str_Loc_Name"].ToString();
            ddReturnstore.Items.Add(lst);
        }
        dr.Close();
        con.Close();
        con.Dispose();
    }

    private void LoadInitIssueGrid()
    {
        var dt = new DataTable();
        dt.Rows.Clear();
        dt.Columns.Add("Client Code", typeof(string));
        dt.Columns.Add("Client Name", typeof(string));
        dt.Columns.Add("Item Code", typeof(string));
        dt.Columns.Add("Item Name", typeof(string));
        dt.Columns.Add("UOM", typeof(string));
        dt.Columns.Add("Issued", typeof(string));
        dt.Columns.Add("Return Quantity", typeof(string));
        dt.Columns.Add("Serial", typeof(string));
        ViewState["datatableParty"] = dt;
    }

    private void SetGridData()
    {
        var dt = new DataTable();
        dt = (DataTable)ViewState["datatableParty"];
        gvManual.DataSource = dt;
        gvManual.DataBind();
        if (gvManual.Rows.Count == 0)
        {
            btnReurnM.Visible = false;
            txtclcode.ReadOnly = false;
        }
        else
        {
            btnReurnM.Visible = true;
        }
    }

    private void SetGridPData()
    {
        var dt = new DataTable();
        dt = (DataTable)ViewState["datatableParty"];
        gvparty.DataSource = dt;
        gvparty.DataBind();
        if (gvparty.Rows.Count == 0)
        {
            btnReurn.Visible = false;
            txtclientcode.ReadOnly = false;
            txtclientcode.Text = "";
            txtclientName.Text = "";
        }
        else
        {
            btnReurn.Visible = true;
            txtclientcode.ReadOnly = true;
        }
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmessage.Visible = false;
        pnlparty.Visible = false;
        btnReurn.Visible = false;
        btnReurnM.Visible = false;
        btnissue.Visible = false;
        Panel21.Visible = false;
        pnlManual.Visible = false;
        btnShowAllData.Visible = false;
        gvissue.DataSource = null;
        gvissue.DataBind();
        if (RadioButtonList1.SelectedIndex == 0)
        {
            Panel21.Visible = true;
            btnShowAllData.Visible = true;
            txtissueno.Text = "";
        }
        if (RadioButtonList1.SelectedIndex == 1)
        {
            pnlparty.Visible = true;
            LoadInitIssueGrid();
            SetGridPData();
        }
        if (RadioButtonList1.SelectedIndex == 2)
        {
            pnlManual.Visible = true;
            LoadInitIssueGrid();
            SetGridData();
        }
    }

    protected void btnShowAllData_Click(object sender, EventArgs e)
    {
        lblmessage.Visible = false;
        btnissue.Visible = false;
        if (txtissueno.Text == "")
        {
            lblmessage.Visible = true;
            lblmessage.Text = "Issue No  Not written";
            gvissue.DataSource = null;
            gvissue.DataBind();
            return;
        }
        showgrid(txtissueno.Text);
    }

    private void showgrid(string issue)
    {
        try
        {
            DataTable dtStoreReturn = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetDataByTrnDetRef(txtissueno.Text));
            gvissue.DataSource = dtStoreReturn;
            gvissue.DataBind();
            int curindx = 0;
            foreach (DataRow rowNo in dtStoreReturn.Rows)
            {
                Label lblDet_ref = (Label)gvissue.Rows[curindx].FindControl("lblDet_ref");
                Label lblHdr_DATE = (Label)gvissue.Rows[curindx].FindControl("lblHdr_DATE");
                Label lblHdr_Pcode = (Label)gvissue.Rows[curindx].FindControl("lblHdr_Pcode");
                Label lblDet_Lno = (Label)gvissue.Rows[curindx].FindControl("lblDet_Lno");
                Label lblDet_Icode = (Label)gvissue.Rows[curindx].FindControl("lblDet_Icode");
                Label lblDet_Itm_Desc = (Label)gvissue.Rows[curindx].FindControl("lblDet_Itm_Desc");
                Label lblDet_Itm_Uom = (Label)gvissue.Rows[curindx].FindControl("lblDet_Itm_Uom");
                Label lblIssueQuantity = (Label)gvissue.Rows[curindx].FindControl("lblIssueQuantity");
                Label lblretQty = (Label)gvissue.Rows[curindx].FindControl("lblretQty");
                TextBox txtReturnQuantity = (TextBox)gvissue.Rows[curindx].FindControl("txtReturnQuantity");
                TextBox lblserial = (TextBox)gvissue.Rows[curindx].FindControl("lblserialnumber");
                Label lblStore = (Label)gvissue.Rows[curindx].FindControl("lblStore");
                AjaxControlToolkit.AutoCompleteExtender acx = (AjaxControlToolkit.AutoCompleteExtender)gvissue.Rows[curindx].FindControl("AutoComplete3");


                lblDet_ref.Text = rowNo["Trn_Det_Ref"].ToString();
                lblHdr_DATE.Text = rowNo["Trn_Hdr_DATE"].ToString().Replace("12:00:00 AM", "");
                lblHdr_Pcode.Text = rowNo["Trn_Hdr_Pcode"].ToString();
                lblDet_Lno.Text = rowNo["Trn_Det_Lno"].ToString();
                lblDet_Icode.Text = rowNo["Trn_Det_Icode"].ToString();
                lblDet_Itm_Desc.Text = rowNo["Trn_Det_Itm_Desc"].ToString();
                lblDet_Itm_Uom.Text = rowNo["Trn_Det_Itm_Uom"].ToString();
                lblIssueQuantity.Text = rowNo["Trn_Det_Lin_Qty"].ToString();
                lblretQty.Text = Convert.ToString(rowNo["retqty"].ToString());
                lblStore.Text = rowNo["Trn_Det_Str_Code"].ToString();
                lblserial.Text = rowNo["Trn_Det_Bat_No"].ToString();
                acx.ContextKey = rowNo["Trn_Det_Icode"].ToString() + ";" + rowNo["Trn_Det_Ref"].ToString();
                curindx++;
                if (lblIssueQuantity.Text == lblretQty.Text)
                {
                    txtReturnQuantity.Enabled = false;
                }
                btnissue.Visible = true;
            }
        }
        catch (Exception ex)
        {
            return;
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }
    protected void txtitemcode_TextChanged(object sender, EventArgs e)
    {
        string[] tmp = txtitemcode.Text.Split(':');
        txtitemcode.Text = tmp[0].Trim();
        lblitemdescription.Text = tmp[1].Trim();
        UoM.Text = tmp[2].Trim();
        Quantity.Text = tmp[3];
        AutoComplete4.ContextKey = txtitemcode.Text;
    }

    protected void btnReurn_Click(object sender, EventArgs e)
    {
        lblmessage.Visible = false;
        if (ddReturnstore.SelectedItem.Text == "--Select--")
        {
            lblmessage.Visible = true;
            lblmessage.Text = "Store not selected";
            return;
        }
        if (gvparty.Rows.Count == 0)
        {
            lblmessage.Visible = true;
            lblmessage.Text = "Add Item";
            return;
        }
        SaveParty(ddReturnstore.SelectedItem.Value);
        LoadInitIssueGrid();
        SetGridPData();
        loadref();
    }

    private void cleargeneral()
    {
        txtclientcode.ReadOnly = true;
        txtitemcode.Text = "";
        lblitemdescription.Text = "";
        UoM.Text = "";
        Quantity.Text = "";
        txtquantity.Text = "";
        txtserno.Text = "";
    }

    private void SaveParty(string store)
    {
        lblmessage.Visible = false;
        string sd = "";
        string sd1 = "";
        int kl11 = 0;
        int get = 0;
        decimal getquantity = 0;
        decimal quantity = 0;
        int maxmovno = 0;
        int maxrateid = 0;
        string NewRateId = "";

        SqlConnection conn = new SqlConnection(_connectionString);
        conn.Open();
        SqlTransaction tran = conn.BeginTransaction();
        
        string clicode = "";
        try
        {
            #region Get MAX RefNo
            
            L3TConnection common = new L3TConnection();
            con = common.init();
            string sbb = @"select  stuff('000000',7-len(ISNULL(max (right(Trn_Hdr_Ref,6)),0)+1),20,ISNULL(max (right(Trn_Hdr_Ref,6)),0)+1) as MaxID from InTr_Trn_Hdr where trn_hdr_type='RT' and trn_hdr_code='SRT'";
            cmd = new SqlCommand(sbb, con);
            dr = cmd.ExecuteReader();
            dr.Read();
            string kl1 = (dr.GetSqlString(0).IsNull) ? "" : (string)dr.GetSqlString(0);
            dr.Close();
            con.Close();
            con.Dispose();

            DateTime dt = DateTime.Now;
            string dt1 = dt.ToString("yyMMdd");
            string maxref = "SRT" + dt.ToString("yyMM") + "-" + kl1;
            
            double maxmovref = Convert.ToDouble(DataProcess.GetSingleValueFromtable(_connectionString, SqlgenerateForFixedAsset.GetMaxMovRefNo()));  
            string maxmovrefno = "MOV-" + dt1 + "-" + string.Format("{0:0000000}", maxmovref);

            #endregion

            var dtp = new DataTable();
            dtp = (DataTable)ViewState["datatableParty"];
            foreach (DataRow row in dtp.Rows)
            {
                string[] tmp1 = row["Serial"].ToString().Split(',');
                clicode = row["Client Code"].ToString();
                string itcode = row["Item Code"].ToString();
                string itname = row["Item Name"].ToString();
                string uom = row["UOM"].ToString();
                string issued = row["Issued"].ToString().Replace("&nbsp;", "");
                string rq = row["Return Quantity"].ToString().Replace("&nbsp;", "");
                string slno = row["Serial"].ToString().Replace("&nbsp;", "");
                
                #region Insert InTr_Trn_Det
                
                DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertIntoTrnDet("RT", "SRT", maxref, 1, "", 0, itcode, itname, uom,
                    store, "", "", 1, slno, DateTime.Now.Date, DateTime.Now.Date,
                    Convert.ToInt32(rq), 0, 0, 0, 0, "", "", "", 0, 0));
                
                #endregion

                #region Update InMa_Stk_Ctl or Insert

                L3TConnection common1 = new L3TConnection();
                con = common1.init();
                string sbb1 = @"select isnull(SUM(Stk_Ctl_Cur_Stk),0) as tt from InMa_Stk_Ctl where Stk_Ctl_SCode='" + store +
                                                                "' and Stk_Ctl_ICode='" + itcode + "'";
                cmd = new SqlCommand(sbb1, con);
                dr = cmd.ExecuteReader();
                dr.Read();
                kl11 = Convert.ToInt32(dr["tt"]);
                dr.Close();
                con.Close();
                con.Dispose();
                if (kl11 != 0)
                {
                    quantity = Convert.ToDecimal(rq) + Convert.ToDecimal(kl11);
                    L3TConnection common11 = new L3TConnection();
                    con = common11.init();
                    string up = @"update InMa_Stk_Ctl set Stk_Ctl_Cur_Stk='" + quantity +
                                           "' , Stk_Ctl_Free_Stk='" + quantity +
                                           "'where Stk_Ctl_SCode='" + store + "' and Stk_Ctl_ICode='" + itcode + "'";
                    cmd = new SqlCommand(up, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    con.Dispose();
                }
                else
                {
                    DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertIntoInmaStkCtl(store, itcode, "", Convert.ToDouble(rq), Convert.ToDouble(rq),
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, DateTime.Now, DateTime.Now, "", "", "", 0));
                }

                #endregion

                #region InMa_Itm_Stk Insert or Update
                
                L3TConnection common12 = new L3TConnection();
                con = common1.init();
                string sbb12 = @"select isnull(SUM(Itm_Stk_Cur),0) as tt from InMa_Itm_Stk where  Itm_Stk_Icode='" + itcode + "'";
                cmd = new SqlCommand(sbb12, con);
                dr = cmd.ExecuteReader();
                dr.Read();
                get = Convert.ToInt32(dr["tt"]);
                dr.Close();
                con.Close();
                con.Dispose();
                if (get != 0)
                {
                    getquantity = Convert.ToDecimal(rq) + Convert.ToDecimal(get);
                    L3TConnection common11 = new L3TConnection();
                    con = common11.init();
                    string up = @"update  InMa_Itm_Stk set Itm_Stk_Cur='" + getquantity +
                                            "'  where  Itm_Stk_Icode='" + itcode + "'";
                    cmd = new SqlCommand(up, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    con.Dispose();
                }
                else
                {
                    DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertIntoInmaItmStk(itcode, Convert.ToDouble(rq), "", 0, 0, 0, 0, 0, "", "", "", 0));
                }

                #endregion

                #region Insert InMa_Stk_Val
                
                DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertIntoInmaStkVal("RT", "SRT", maxref, Convert.ToDateTime(txtreturndate.Text), itcode,
                    itname, store, 0, 0, 0, Convert.ToDouble(rq), "", "", "", ""));
                
                #endregion

                #region Insert Item_Movement_dtl
                
                maxmovno = Convert.ToInt32(DataProcess.GetSingleValueFromtable(_connectionString, SqlgenerateForFixedAsset.GetMaxMovementNo()));
                DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertQuery(maxmovno, maxmovrefno, itcode, slno, maxref, store, "RT", "SRT",
                    Convert.ToDateTime(txtreturndate.Text), "Good", "", Convert.ToInt32(rq), 0,
                    clicode, DateTime.Now, DateTime.Now, Session[StaticData.sessionUserId].ToString()));
                
                #endregion

                #region Insert InMa_Itm_Serial

                if (tmp1[0].ToString() != "")
                    {
                        for (int j = 0; j < tmp1.Length; j++)
                        {
                            DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertInmaItmSerial(itcode, tmp1[j].Trim(), maxref, store, "RT", "SRT",
                                Convert.ToDateTime(txtreturndate.Text), "", ""));
                        }
                    }
                
                #endregion

                #region Insert InMa_Itm_Rate
                
                DataTable drat = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetDataByitm_rate_icode(store, itcode));
                var maxrateidTemp = DataProcess.GetSingleValueFromtable(_connectionString, SqlgenerateForFixedAsset.GetMaxID());
                if(maxrateidTemp != "")
                {
                    maxrateid = Convert.ToInt32(maxrateidTemp);
                }
                NewRateId = "RT" + DateTime.Now.ToString("yyMM") + "-" + maxrateid.ToString("000000");
                if (drat.Rows.Count == 0)
                {
                    DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertQuery(store, itcode, maxref, DateTime.Now.Date, Convert.ToDecimal(rq), 0, 1, NewRateId, ""));
                }

                #endregion
            }
            
            #region Insert  InTr_Trn_Hdr

            DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertIntoTrnHdr("RT", "SRT", maxref, clicode, clicode, clicode, Convert.ToDateTime(txtreturndate.Text),
                    "", txtrefno.Text, "", "", "", "", "", "", "", "", 0, "P", max.Text, "ADM",
                    "", "", "", "", "", "", "", "", "", 0, 0, DateTime.Now, DateTime.Now, ""));

            #endregion

            lblmessage.Visible = true;
            lblmessage.Text = "Return successfully";
            txtIssueRef.Text = maxref;
            txtIssueRef.Visible = true;
            ModalPopupExtender3.Show();
            tran.Commit();
        }
        catch (Exception ex)
        {
            tran.Rollback();
            lblMsgHdr.Text = "Data Processing Error. \n" + ex.Message;
            txtIssueRef.Visible = false;
            ModalPopupExtender3.Show();
            return;
        }
    }

    protected void btnissue_Click(object sender, EventArgs e)
    {
        lblmessage.Visible = false;
        if (ddReturnstore.SelectedItem.Text == "--Select--")
        {
            lblmessage.Visible = true;
            lblmessage.Text = "Store not selected";
            return;
        }
        if (CheckRequired() == false)
        {
            lblmessage.Visible = true;
            lblmessage.Text = "ERROR";
            return;
        }
        #region Get MaxRefNo
        L3TConnection common = new L3TConnection();
        con = common.init();
        string sbb = @"select  stuff('000000',7-len(ISNULL(max (right(Trn_Hdr_Ref,6)),0)+1),20,ISNULL(max (right(Trn_Hdr_Ref,6)),0)+1) as MaxID from dbo.InTr_Trn_Hdr where trn_hdr_type='RT' and trn_hdr_code='SRT'";
        cmd = new SqlCommand(sbb, con);
        dr = cmd.ExecuteReader();
        dr.Read();
        string kl1 = (dr.GetSqlString(0).IsNull) ? "" : (string)dr.GetSqlString(0);
        dr.Close();
        con.Close();
        con.Dispose();

        DateTime dt = System.DateTime.Now;
        string dt1 = dt.ToString("yyMMdd");
        id.Text = "SRT" + dt.ToString("yyMM") + "-" + kl1;
        string refno = id.Text;
        double maxmovref = Convert.ToDouble(DataProcess.GetSingleValueFromtable(_connectionString, SqlgenerateForFixedAsset.GetMaxMovRefNo()));
        string maxmovrefno = "MOV-" + dt1 + "-" + string.Format("{0:0000000}", maxmovref);

        #endregion
        
        returnissuewise(ddReturnstore.SelectedItem.Value, refno, maxmovrefno);
        loadref();
    }

    private void loadref()
    {
        try
        {
            L3TConnection common = new L3TConnection();
            con = common.init();
            string sbb = @"select  stuff('000000',7-len(ISNULL(max (right(Trn_Hdr_Ref,6)),0)+1),20,ISNULL(max (right(Trn_Hdr_Ref,6)),0)+1) as MaxID from dbo.InTr_Trn_Hdr where trn_hdr_type='RT' and trn_hdr_code='SRT'";
            cmd = new SqlCommand(sbb, con);
            dr = cmd.ExecuteReader();
            dr.Read();
            string kl1 = (dr.GetSqlString(0).IsNull) ? "" : (string)dr.GetSqlString(0);
            dr.Close();
            con.Close();
            con.Dispose();

            DateTime dt = System.DateTime.Now;
            id.Text = "SRT" + dt.ToString("yyMM") + "-" + kl1;
            string refno = id.Text;

            mo.Text = "SRT" + dt.ToString("yyMM") + "-" + kl1;
        }
        catch (Exception ex)
        {
            return;
        }
    }

    private void returnissuewise(string store, string refno, string maxmovrefno)
    {
        double kl11 = 0;
        double get1 = 0;
        decimal quantity = 0;
        decimal getquantity1 = 0;
        int maxmovno = 0;
        int maxrateid = 0;
        string NewRateId = "";

        SqlConnection conn = new SqlConnection(_connectionString);
        conn.Open();
        SqlTransaction tran = conn.BeginTransaction();
        try
        {
            string lblHdr_Pcod = "";
            string lblDet_re = "";
            foreach (GridViewRow gr in gvissue.Rows)
            {
                Label lblDet_ref = (Label)gr.FindControl("lblDet_ref");
                Label lblHdr_DATE = (Label)gr.FindControl("lblHdr_DATE");
                Label lblHdr_Pcode = (Label)gr.FindControl("lblHdr_Pcode");
                Label lblDet_Lno = (Label)gr.FindControl("lblDet_Lno");
                Label lblDet_Icode = (Label)gr.FindControl("lblDet_Icode");
                Label lblDet_Itm_Desc = (Label)gr.FindControl("lblDet_Itm_Desc");
                Label lblDet_Itm_Uom = (Label)gr.FindControl("lblDet_Itm_Uom");
                Label lblIssueQuantity = (Label)gr.FindControl("lblIssueQuantity");
                Label lblretQty = (Label)gr.FindControl("lblretQty");
                TextBox txtReturnQuantity = (TextBox)gr.FindControl("txtReturnQuantity");
                TextBox lblserial = (TextBox)gr.FindControl("lblserialnumber");
                Label lblStore = (Label)gr.FindControl("lblStore");
                if (txtReturnQuantity.Text.Trim() != "")
                {
                    DataTable drat = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetDataByitm_rate_icode(store, lblDet_Icode.Text));
                    var maxrateidTemp = DataProcess.GetSingleValueFromtable(_connectionString, SqlgenerateForFixedAsset.GetMaxID());
                    if (maxrateidTemp != "")
                    {
                        maxrateid = Convert.ToInt32(maxrateidTemp);
                    }
                    NewRateId = "RT" + DateTime.Now.ToString("yyMM") + "-" + maxrateid.ToString("000000");
                    string[] tmp = lblserial.Text.Split(',');

                    lblHdr_Pcod = lblHdr_Pcode.Text;
                    lblDet_re = lblDet_ref.Text;

                    #region Insert InTr_Trn_Det

                    DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertIntoTrnDet("RT", "SRT", refno, Convert.ToInt16(lblDet_Lno.Text), "", 0,
                         lblDet_Icode.Text, lblDet_Itm_Desc.Text, lblDet_Itm_Uom.Text, store, "",
                         "", Convert.ToInt16(lblDet_Lno.Text), lblserial.Text, DateTime.Now.Date, DateTime.Now.Date,
                         Convert.ToInt32(txtReturnQuantity.Text), 0, 0, 0, 0, "", "", "", 0, 0));
                     
                     #endregion

                     #region Insert InMa_Stk_Ctl
                    
                    L3TConnection common1 = new L3TConnection();
                    con = common1.init();
                    string sbb1 = @"select isnull(SUM(Stk_Ctl_Cur_Stk),0) as tt from InMa_Stk_Ctl where Stk_Ctl_SCode='" + store +
                                                                    "' and Stk_Ctl_ICode='" + lblDet_Icode.Text + "'";
                    cmd = new SqlCommand(sbb1, con);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    kl11 = Convert.ToDouble(dr["tt"]);
                    dr.Close();
                    con.Close();
                    con.Dispose();
                    if (kl11 != 0)
                    {
                        quantity = Convert.ToDecimal(txtReturnQuantity.Text) + Convert.ToDecimal(kl11);
                        L3TConnection common11 = new L3TConnection();
                        con = common11.init();
                        string up = @"update InMa_Stk_Ctl set Stk_Ctl_Cur_Stk='" + quantity +
                            "' , Stk_Ctl_Free_Stk='" + quantity + "'where Stk_Ctl_SCode='" + store +
                                                                    "' and Stk_Ctl_ICode='" + lblDet_Icode.Text + "'";
                        cmd = new SqlCommand(up, con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        con.Dispose();
                    }
                    else
                    {
                        L3TConnection cmmon10 = new L3TConnection();
                        con = cmmon10.init();
                            string insert = @"insert into InMa_Stk_Ctl(Stk_Ctl_SCode,Stk_Ctl_ICode,Stk_Ctl_Str_Grp,Stk_Ctl_Cur_Stk,
                                                Stk_Ctl_Free_Stk,Stk_Ctl_On_Ord_Stk,Stk_Ctl_Ind_Stk,Stk_Ctl_Quot_Stk,Stk_Ctl_Min_Stk,
                                                Stk_Ctl_Reord_Stk,Stk_Ctl_Max_Stk,Stk_Ctl_Std_Val,Stk_Ctl_Ave_Val,Stk_Ctl_Lat_Val,
                                                Stk_Ctl_FIFO_Val,Stk_Ctl_LIFO_Val,Stk_Ctl_Lst_Rec_Dat,T_C1,
                                                T_C2,T_Fl,T_In)values('" + store + "','" + lblDet_Icode.Text +
                                  "','','" + Convert.ToDouble(txtReturnQuantity.Text) + "','" + Convert.ToDouble(txtReturnQuantity.Text) +
                                  "',0,0,0, 0, 0, 0, 0, 0, 0, 0, 0,CONVERT(datetime,'" + DateTime.Now +
                                  "', 103),'','','',0)";
                            cmd = new SqlCommand(insert, con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            con.Dispose();
                    }
                     
                     #endregion
                    
                    #region Insert InMa_Itm_Stk
                    
                    L3TConnection common12 = new L3TConnection();
                     con = common12.init();
                     string sbb12 = @"select isnull(SUM(Itm_Stk_Cur),0) as total from InMa_Itm_Stk where  Itm_Stk_Icode='" + lblDet_Icode.Text + "'";
                     cmd = new SqlCommand(sbb12, con);
                     dr = cmd.ExecuteReader();
                     dr.Read();
                     get1 = Convert.ToDouble(dr["total"]);
                     dr.Close();
                     con.Close();
                     con.Dispose();
                     if (get1 != 0)
                     {
                         getquantity1 = Convert.ToDecimal(txtReturnQuantity.Text) + Convert.ToDecimal(get1);
                         L3TConnection cmmon11 = new L3TConnection();
                         con = cmmon11.init();
                         string up1 = @"update  InMa_Itm_Stk set Itm_Stk_Cur='" + getquantity1 +
                                                 "'  where  Itm_Stk_Icode='" + lblDet_Icode.Text + "'";
                         cmd = new SqlCommand(up1, con);
                         cmd.ExecuteNonQuery();
                         con.Close();
                         con.Dispose();
                     }
                     else
                     {
                         DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertIntoInmaItmStk(lblDet_Icode.Text, Convert.ToDouble(txtReturnQuantity.Text), "", 0, 0, 0, 0, 0, "", "", "", 0));
                         
                     }
                     #endregion

                     #region Insert Item_Movement_dtl
                     
                     maxmovno = Convert.ToInt32(DataProcess.GetSingleValueFromtable(_connectionString, SqlgenerateForFixedAsset.GetMaxMovementNo()));
                     DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertQuery(maxmovno, maxmovrefno, lblDet_Icode.Text, lblserial.Text, refno, store, "RT",
                            "SRT", Convert.ToDateTime(txtreturndate.Text), "Good", "", Convert.ToInt32(lblIssueQuantity.Text), 0,
                           lblHdr_Pcode.Text, DateTime.Now, DateTime.Now, Session[StaticData.sessionUserId].ToString()));
                     
                     #endregion

                     #region Insert InMa_Stk_Val

                     DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertIntoInmaStkVal("RT", "SRT", refno, Convert.ToDateTime(txtreturndate.Text), lblDet_Icode.Text,
                         lblDet_Itm_Desc.Text, store, 0, 0, 0, Convert.ToDouble(txtReturnQuantity.Text), "", "", "", ""));
                     
                     #endregion

                     #region Insert InMa_Itm_Serial
                     if (Convert.ToDouble(txtReturnQuantity.Text) == tmp.Length && tmp[0].ToString() != "")
                     {

                         for (int i = 0; i < tmp.Length; i++)
                         {
                             DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertInmaItmSerial(lblDet_Icode.Text, tmp[i].Trim(), refno, store, "RT", "SRT",
                                 Convert.ToDateTime(txtreturndate.Text), "", ""));
                         }

                     }
                     #endregion

                     #region Insert InMa_Itm_Rate

                     if (drat.Rows.Count == 0)
                     {
                         DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertQuery(store, lblDet_Icode.Text, refno, DateTime.Now.Date, 1, 0, 1, NewRateId, ""));
                     }
                     #endregion
                 }
             }

             #region Insert InTr_Trn_Hdr

             DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertIntoTrnHdr("RT", "SRT", refno, lblHdr_Pcod, lblHdr_Pcod, lblHdr_Pcod,
             Convert.ToDateTime(txtreturndate.Text), "", txtrefno.Text, "", "", "", "", "", "", "", "", 0, "P", max.Text, "ADM",
             "", "", "", lblDet_re, "", "", "", "", "", 0, 0, DateTime.Now, DateTime.Now, ""));
             
             #endregion

             lblmessage.Visible = true;
             lblmessage.Text = "Return successfully";
             gvissue.DataSource = null;
             gvissue.DataBind();
             txtIssueRef.Text = refno;
             txtIssueRef.Visible = true;
             ModalPopupExtender3.Show();
             tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();                
                lblMsgHdr.Text = "Data Processing Error. \n" + ex.Message;
                txtIssueRef.Visible = false;
                ModalPopupExtender3.Show();
                return;
            }
    }

    private bool CheckRequired()
    {
        bool rt = true;

        string sd = "";
        string sd1 = "";

        foreach (GridViewRow gr in gvissue.Rows)
        {
            Label lblDet_ref = (Label)gr.FindControl("lblDet_ref");
            Label lblHdr_DATE = (Label)gr.FindControl("lblHdr_DATE");
            Label lblHdr_Pcode = (Label)gr.FindControl("lblHdr_Pcode");
            Label lblDet_Lno = (Label)gr.FindControl("lblDet_Lno");
            Label lblDet_Icode = (Label)gr.FindControl("lblDet_Icode");
            Label lblDet_Itm_Desc = (Label)gr.FindControl("lblDet_Itm_Desc");
            Label lblDet_Itm_Uom = (Label)gr.FindControl("lblDet_Itm_Uom");
            Label lblIssueQuantity = (Label)gr.FindControl("lblIssueQuantity");
            Label lblretQty = (Label)gr.FindControl("lblretQty");
            TextBox txtReturnQuantity = (TextBox)gr.FindControl("txtReturnQuantity");
            TextBox lblserial = (TextBox)gr.FindControl("lblserialnumber");
            Label lblStore = (Label)gr.FindControl("lblStore");
            if (txtReturnQuantity.Text.Trim() != "")
            {
                Double qnt = Convert.ToDouble(lblIssueQuantity.Text) - Convert.ToDouble(lblretQty.Text);
                if (Convert.ToDouble(txtReturnQuantity.Text) > qnt)
                {
                    lblmessage.Visible = true;
                    lblmessage.Text = "Quantity is not getter than Issue Quantity";
                    rt = false;
                    goto retuenmain;
                }
                string[] tmp = lblserial.Text.Split(',');

                Dataaccess.L3TConnection Common0 = new Dataaccess.L3TConnection();
                con = Common0.init();

                string sl = "select count(itm_det_serial_no) as tot from InMa_Itm_Serial where  itm_det_icode='" + lblDet_Icode.Text + "'";

                cmd = new SqlCommand(sl, con);
                dr = cmd.ExecuteReader();

                dr.Read();
                int no = Convert.ToInt32(dr["tot"]);
                dr.Close();
                con.Close();
                con.Dispose();
                if (no > 0)
                {
                    if (lblserial.Text == "")
                    {
                        lblmessage.Visible = true;
                        lblmessage.Text = "Serial number should not blank";
                        rt = false;
                        goto retuenmain;
                    }
                    if (Convert.ToDouble(txtReturnQuantity.Text) != tmp.Length)
                    {
                        lblmessage.Visible = true;
                        lblmessage.Text = "Serial number is not equal Quantity";
                        rt = false;
                        goto retuenmain;
                    }
                    if (Convert.ToDouble(txtReturnQuantity.Text) == tmp.Length && tmp[0].ToString() != "")
                    {
                        for (int k = 0; k < tmp.Length; k++)
                        {
                            sd = tmp[k].Trim();
                            for (int j = 0; j < tmp.Length; j++)
                            {
                                sd1 = tmp[j].Trim();
                                if (j != k)
                                {
                                    if (sd == sd1)
                                    {
                                        goto nxt;
                                    }
                                }
                            }
                        }
                        Dataaccess.L3TConnection Common = new Dataaccess.L3TConnection();
                        con = Common.init();
                        for (int i = 0; i < tmp.Length; i++)
                        {
                            string str = "SELECT count(*) as tt FROM InMa_Itm_Serial where itm_det_icode = '" + lblDet_Icode.Text +
                                "' and itm_det_serial_no='" + tmp[i].Trim() + "'  ";

                            cmd = new SqlCommand(str, con);
                            dr = cmd.ExecuteReader();

                            dr.Read();
                            int ds = Convert.ToInt32(dr["tt"]);
                            dr.Close();
                            if (ds == 0)
                            {
                                goto valid;
                            }
                        }
                        con.Close();
                        con.Dispose();
                    }
                    goto retuenmain;
                nxt:
                    lblmessage.Visible = true;
                    lblmessage.Text = " same";
                    rt = false;
                    goto retuenmain;
                valid:
                    lblmessage.Visible = true;
                    lblmessage.Text = " Not valid";
                    rt = false;
                    goto retuenmain;
                }
            }
        }
    retuenmain:
        return rt;
    }

    protected void txtclientcode_TextChanged(object sender, EventArgs e)
    {
        string[] tmp = txtclientcode.Text.Split(':');
        AutoComplete2.ContextKey = tmp[0].Trim();
        txtclientcode.Text = tmp[0];
        txtclientName.Text = tmp[1];
    }

    protected void txtclcode_TextChanged(object sender, EventArgs e)
    {
        string[] tmp = txtclcode.Text.Split(':');
        AutoComplete2.ContextKey = tmp[0].Trim();
        txtclcode.Text = tmp[0];
        txtclname.Text = tmp[1];
    }

    protected void txtitcode_TextChanged(object sender, EventArgs e)
    {
        string[] tmp = txtitcode.Text.Split(':');
        txtitcode.Text = tmp[0].Trim();
        lblitemdes.Text = tmp[1].Trim();
        lbluom.Text = tmp[2].Trim();
    }

    protected void btnReurnM_Click(object sender, EventArgs e)
    {
        lblmessage.Visible = false;
        if (ddReturnstore.SelectedItem.Text == "--Select--")
        {
            lblmessage.Visible = true;
            lblmessage.Text = "Store not selected";
            return;
        }
        if (gvManual.Rows.Count == 0)
        {
            lblmessage.Visible = true;
            lblmessage.Text = "Add Item";
            return;
        }
        #region Get MaxRefNo

        L3TConnection common = new L3TConnection();
        con = common.init();
        string sbb = @"select  stuff('000000',7-len(ISNULL(max (right(Trn_Hdr_Ref,6)),0)+1),20,ISNULL(max (right(Trn_Hdr_Ref,6)),0)+1) as MaxID from dbo.InTr_Trn_Hdr where trn_hdr_type='RT' and trn_hdr_code='SRT'";
        cmd = new SqlCommand(sbb, con);
        dr = cmd.ExecuteReader();
        dr.Read();
        string kl1 = (dr.GetSqlString(0).IsNull) ? "" : (string)dr.GetSqlString(0);
        dr.Close();
        con.Close();
        con.Dispose();
        DateTime dt = System.DateTime.Now;
        string dt1 = dt.ToString("yyMMdd");
        id.Text = "SRT" + dt.ToString("yyMM") + "-" + kl1;
        string refno = id.Text;
        double maxmovref = Convert.ToDouble(DataProcess.GetSingleValueFromtable(_connectionString, SqlgenerateForFixedAsset.GetMaxMovRefNo()));
        string maxmovrefno = "MOV-" + dt1 + "-" + string.Format("{0:0000000}", maxmovref);

        #endregion
        
        SaveManual(ddReturnstore.SelectedItem.Value, refno, maxmovrefno);
        LoadInitIssueGrid();
        SetGridData();
        loadref();
    }
    private void SaveManual(string store, string maxref, string maxmovrefno)
    {
        int kl11 = 0;
        decimal quantity = 0;
        int get = 0;
        decimal getquantity = 0;
        int maxmovno = 0;
        int maxrateid = 0;
        string clicode = "";
        string NewRateId = "";
        SqlConnection conn = new SqlConnection(_connectionString);
        conn.Open();
        SqlTransaction tran = conn.BeginTransaction();
        
        try
        {
            var dtp = new DataTable();
            dtp = (DataTable)ViewState["datatableParty"];
            foreach (DataRow row in dtp.Rows)
            {
                string[] tmp1 = row["Serial"].ToString().Split(',');
                clicode = row["Client Code"].ToString();
                string itcode = row["Item Code"].ToString();
                string itname = row["Item Name"].ToString();
                string uom = row["UOM"].ToString();
                string issued = row["Issued"].ToString().Replace("&nbsp;", "");
                string rq = row["Return Quantity"].ToString().Replace("&nbsp;", "");
                string slno = row["Serial"].ToString().Replace("&nbsp;", "");
                var maxrateidTemp = DataProcess.GetSingleValueFromtable(_connectionString, SqlgenerateForFixedAsset.GetMaxID());
                if (maxrateidTemp != "")
                {
                    maxrateid = Convert.ToInt32(maxrateidTemp);
                }
                NewRateId = "RT" + DateTime.Now.ToString("yyMM") + "-" + maxrateid.ToString("000000");

                #region Insert InMa_Itm_Serial
                
                if (tmp1[0].ToString() != "")
                {
                    for (int j = 0; j < tmp1.Length; j++)
                    {
                        DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertInmaItmSerial(itcode, tmp1[j].Trim(), maxref, store, "RT", "SRT",
                            Convert.ToDateTime(txtreturndate.Text), "", NewRateId));
                    }
                }

                #endregion

                #region Insert InTr_Trn_Det
                
                DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertIntoTrnDet("RT", "SRT", maxref, 1, "", 0, itcode, itname, uom,
                    store, "", "", 1, slno, DateTime.Now.Date, DateTime.Now.Date,
                    Convert.ToInt32(rq), 0, 0, 0, 0, "", "", "", 0, 0));

                #endregion

                #region Update InMa_Stk_Ctl or Insert
                
                L3TConnection common1 = new L3TConnection();
                con = common1.init();
                string sbb1 = @"select isnull(SUM(Stk_Ctl_Cur_Stk),0) as tt from InMa_Stk_Ctl where Stk_Ctl_SCode='" + store +
                    "' and Stk_Ctl_ICode='" + itcode + "'";
                cmd = new SqlCommand(sbb1, con);
                dr = cmd.ExecuteReader();
                dr.Read();
                kl11 = Convert.ToInt32(dr["tt"]);
                dr.Close();
                con.Close();
                con.Dispose();
                if (kl11 != 0)
                {
                    quantity = Convert.ToDecimal(rq) + Convert.ToDecimal(kl11);
                    L3TConnection common11 = new L3TConnection();
                    con = common11.init();
                    string up = @"update InMa_Stk_Ctl set Stk_Ctl_Cur_Stk='" + quantity +
                        "' , Stk_Ctl_Free_Stk='" + quantity +
                        "'where Stk_Ctl_SCode='" + store + "' and Stk_Ctl_ICode='" + itcode + "'";
                    cmd = new SqlCommand(up, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    con.Dispose();
                }
                else
                {
                    DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertIntoInmaStkCtl(store, itcode, "", Convert.ToDouble(rq), Convert.ToDouble(rq),
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, DateTime.Now, DateTime.Now, "", "", "", 0));
                }
                
                #endregion
                
                #region InMa_Itm_Stk Insert or Update
                
                L3TConnection common12 = new L3TConnection();
                con = common1.init();
                string sbb12 = @"select isnull(SUM(Itm_Stk_Cur),0) as tt from InMa_Itm_Stk where  Itm_Stk_Icode='" + itcode + "'";
                cmd = new SqlCommand(sbb12, con);
                dr = cmd.ExecuteReader();
                dr.Read();
                get = Convert.ToInt32(dr["tt"]);
                dr.Close();
                con.Close();
                con.Dispose();
                if (get != 0)
                {
                    getquantity = Convert.ToDecimal(rq) + Convert.ToDecimal(get);
                    L3TConnection common11 = new L3TConnection();
                    con = common11.init();
                    string up = @"update  InMa_Itm_Stk set Itm_Stk_Cur='" + getquantity +
                                            "'  where  Itm_Stk_Icode='" + itcode + "'";
                    cmd = new SqlCommand(up, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    con.Dispose();
                }
                else
                {
                    DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertIntoInmaItmStk(itcode, Convert.ToDouble(rq), "", 0, 0, 0, 0, 0, "", "", "", 0));
                }
                
                #endregion

                #region Insert InMa_Stk_Val
                
                DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertIntoInmaStkVal("RT", "SRT", maxref, Convert.ToDateTime(txtreturndate.Text), itcode,
                    itname, store, 0, 0, 0, Convert.ToDouble(rq), "", "", "", ""));
                
                #endregion

                #region Insert Item_Movement_dtl

                maxmovno = Convert.ToInt32(DataProcess.GetSingleValueFromtable(_connectionString, SqlgenerateForFixedAsset.GetMaxMovementNo()));
                DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertQuery(maxmovno, maxmovrefno, itcode, slno, maxref, store, "RT", "SRT",
                        Convert.ToDateTime(txtreturndate.Text), "Good", "", Convert.ToInt32(rq), 0,
                        clicode, DateTime.Now, DateTime.Now, Session[StaticData.sessionUserId].ToString()));

                #endregion

                #region Insert InMa_Itm_Rate

                DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertQuery(store, itcode, maxref, DateTime.Now.Date, Convert.ToDecimal(rq), 0, 1, NewRateId, ""));
                
                #endregion

            }

            #region Insert  InTr_Trn_Hdr

            DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertIntoTrnHdr("RT", "SRT", maxref, clicode, clicode, clicode, Convert.ToDateTime(txtreturndate.Text),
                    "", txtrefno.Text, "", "", "", "", "", "", "", "", 0, "P", max.Text, "ADM",
                    "", "", "", "", "", "", "", "", "", 0, 0, DateTime.Now, DateTime.Now, ""));

            #endregion
            
            lblmessage.Visible = true;
            lblmessage.Text = "Return successfully";
            txtIssueRef.Text = maxref;
            txtIssueRef.Visible = true;
            ModalPopupExtender3.Show();
            tran.Commit();
        }
        catch (Exception ex)
        {
            tran.Rollback();            
            lblMsgHdr.Text = "Data Processing Error. \n" + ex.Message;
            txtIssueRef.Visible = false;
            ModalPopupExtender3.Show();
            return;
        }
    }

    private void InsertManual(string store, string itemcode)
    {
        lblmessage.Visible = false;
        string sd = "";
        string sd1 = "";
        int kl11 = 0;
        int get = 0;
        decimal getquantity = 0;
        decimal quantity = 0;
        int maxmovno = 0;
        SqlConnection conn = new SqlConnection(_connectionString);
        conn.Open();
        SqlTransaction tran = conn.BeginTransaction();
        try
        {
            #region Serial number is not equal Quantity

            string[] tmp1 = txtslno.Text.Split(',');
            Dataaccess.L3TConnection Common0 = new Dataaccess.L3TConnection();
            con = Common0.init();
            string sl = "select count(itm_det_serial_no) as tot from InMa_Itm_Serial where  itm_det_icode='" + itemcode + "'";
            cmd = new SqlCommand(sl, con);
            dr = cmd.ExecuteReader();
            dr.Read();
            int no = Convert.ToInt32(dr["tot"]);
            dr.Close();
            if (no > 0)
            {
                if (Convert.ToDouble(txtqt.Text) != tmp1.Length)
                {
                    lblmessage.Visible = true;
                    lblmessage.Text = "Serial number is not equal Quantity";
                    return;
                }
            }
            con.Close();
            con.Dispose();

            #endregion

            #region Get MAX RefNo

            L3TConnection common = new L3TConnection();
            con = common.init();
            string sbb = @"select  stuff('000000',7-len(ISNULL(max (right(Trn_Hdr_Ref,6)),0)+1),20,ISNULL(max (right(Trn_Hdr_Ref,6)),0)+1) as MaxID from InTr_Trn_Hdr where trn_hdr_type='RT' and trn_hdr_code='SRT'";
            cmd = new SqlCommand(sbb, con);
            dr = cmd.ExecuteReader();
            dr.Read();
            string kl1 = (dr.GetSqlString(0).IsNull) ? "" : (string)dr.GetSqlString(0);
            dr.Close();
            con.Close();
            con.Dispose();
            DateTime dt = DateTime.Now;
            string dt1 = dt.ToString("yyMMdd");
            string maxref = "SRT" + dt.ToString("yyMM") + "-" + kl1;
            double maxmovref = Convert.ToDouble(DataProcess.GetSingleValueFromtable(_connectionString, SqlgenerateForFixedAsset.GetMaxMovRefNo()));
            string maxmovrefno = "MOV-" + dt1 + "-" + string.Format("{0:0000000}", maxmovref);

            #endregion

            #region Insert  InTr_Trn_Hdr
            
            DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertIntoTrnHdr("RT", "SRT", maxref, txtclcode.Text, txtclcode.Text, txtclcode.Text,
                Convert.ToDateTime(txtreturndate.Text), "", "", "", "", "", "", "", "", "", "", 0, "P", max.Text, "L3T388",
                "", "", "", "", "", "", "", "", "", 0, 0, DateTime.Now, DateTime.Now, ""));

            #endregion

            #region Insert InTr_Trn_Det
            
            DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertIntoTrnDet("RT", "SRT", maxref, 1, "", 0, itemcode,
                    lblitemdes.Text, "PC", store, "",
                    "", 1, txtslno.Text, DateTime.Now.Date, DateTime.Now.Date,
                    Convert.ToInt32(txtqt.Text), 0, 0, 0, 0, "", "", "", 0, 0));
            
            #endregion

            #region Update InMa_Stk_Ctl or Insert

            L3TConnection common1 = new L3TConnection();
            con = common1.init();
            string sbb1 = @"select isnull(SUM(Stk_Ctl_Cur_Stk),0) as tt from InMa_Stk_Ctl where Stk_Ctl_SCode='" + store +
                "' and Stk_Ctl_ICode='" + itemcode + "'";
            cmd = new SqlCommand(sbb1, con);
            dr = cmd.ExecuteReader();
            dr.Read();
            kl11 = Convert.ToInt32(dr["tt"]);
            dr.Close();
            con.Close();
            con.Dispose();
            if (kl11 != 0)
            {
                quantity = Convert.ToDecimal(txtqt.Text) + Convert.ToDecimal(kl11);
                L3TConnection common11 = new L3TConnection();
                con = common11.init();
                string up = @"update InMa_Stk_Ctl set Stk_Ctl_Cur_Stk='" + quantity +
                                       "' , Stk_Ctl_Free_Stk='" + quantity +
                                       "'where Stk_Ctl_SCode='" + store + "' and Stk_Ctl_ICode='" + itemcode + "'";
                cmd = new SqlCommand(up, con);
                cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();
            }
            else
            {
                DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertIntoInmaStkCtl(store, itemcode, "", Convert.ToDouble(txtqt.Text), Convert.ToDouble(txtqt.Text),
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, DateTime.Now, DateTime.Now, "", "", "", 0));
            }

            #endregion

            #region InMa_Itm_Stk Insert or Update

                L3TConnection common12 = new L3TConnection();
                con = common1.init();
                string sbb12 = @"select isnull(SUM(Itm_Stk_Cur),0) as tt from InMa_Itm_Stk where  Itm_Stk_Icode='" + itemcode + "'";
                cmd = new SqlCommand(sbb12, con);
                dr = cmd.ExecuteReader();
                dr.Read();
                get = Convert.ToInt32(dr["tt"]);
                dr.Close();
                con.Close();
                con.Dispose();
                if (get != 0)
                {
                    getquantity = Convert.ToDecimal(txtqt.Text) + Convert.ToDecimal(get);
                    L3TConnection common11 = new L3TConnection();
                    con = common11.init();
                    string up = @"update  InMa_Itm_Stk set Itm_Stk_Cur='" + getquantity +
                                            "'  where  Itm_Stk_Icode='" + itemcode + "'";
                    cmd = new SqlCommand(up, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    con.Dispose();
                }
                else
                {
                    DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertIntoInmaItmStk(itemcode, Convert.ToDouble(txtqt.Text), "", 0, 0, 0, 0, 0, "", "", "", 0));
                }
            #endregion

            #region Insert InMa_Stk_Val
            
            DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertIntoInmaStkVal("RT", "SRT", maxref, Convert.ToDateTime(txtreturndate.Text), itemcode,
                    lblitemdes.Text, store, 0, 0, 0, Convert.ToDouble(txtqt.Text), "", "", "", ""));
            #endregion

            #region Insert Item_Movement_dtl

            maxmovno = Convert.ToInt32(DataProcess.GetSingleValueFromtable(_connectionString, SqlgenerateForFixedAsset.GetMaxMovementNo()));
            DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertQuery(maxmovno, maxmovrefno, itemcode, txtslno.Text, "", store, "RT", "SRT",
                Convert.ToDateTime(txtreturndate.Text), "Good", "", Convert.ToInt32(txtqt.Text), 0,
                txtclcode.Text, DateTime.Now, DateTime.Now, Session[StaticData.sessionUserId].ToString()));
            #endregion

            #region Insert InMa_Itm_Serial
                if (Convert.ToDouble(txtqt.Text) == tmp1.Length && tmp1[0].ToString() != "")
                {
                    for (int k = 0; k < tmp1.Length; k++)
                    {
                        sd = tmp1[k].Trim();
                        for (int j = 0; j < tmp1.Length; j++)
                        {
                            sd1 = tmp1[j].Trim();
                            if (j != k)
                            {
                                if (sd == sd1)
                                {
                                    Label1.Visible = true;
                                    goto nxt;
                                }
                            }
                        }
                    }
                    for (int i = 0; i < tmp1.Length; i++)
                    {
                        Dataaccess.L3TConnection Common = new Dataaccess.L3TConnection();
                        con = Common.init();

                        string str = "SELECT count(*) as tt FROM InMa_Itm_Serial where itm_det_icode = '" + itemcode +
                                              "' and itm_det_serial_no='" + tmp1[i].Trim() + "'  ";

                        cmd = new SqlCommand(str, con);
                        dr = cmd.ExecuteReader();

                        dr.Read();
                        int ds = Convert.ToInt32(dr["tt"]);
                        dr.Close();
                        if (ds == 0)
                        {
                            goto valid;
                        }
                        con.Close();
                        con.Dispose();
                    }
                    for (int i = 0; i < tmp1.Length; i++)
                    {
                        DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertInmaItmSerial(itemcode, tmp1[i].Trim(), maxref, store, "RT", "SRT",
                            Convert.ToDateTime(txtreturndate.Text), "", ""));
                    }
                    goto complete;


                nxt:
                    lblmessage.Visible = true;
                    lblmessage.Text = "Serial number is same";
                    return;
                valid:
                    lblmessage.Visible = true;
                    lblmessage.Text = "Serial number is not valid";
                    return;
                }

                #endregion
            complete:
            lblmessage.Visible = true;
            lblmessage.Text = "Return successfully";
            tran.Commit();
        }
        catch (Exception ex)
        {
            tran.Rollback();
            return;
        }
    }

    private void clearmanual()
    {
        txtclcode.ReadOnly = true;
        txtitcode.Text = "";
        lblitemdes.Text = "";
        lbluom.Text = "";
        txtqt.Text = "";
        txtslno.Text = "";
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        lblmessage.Visible = false;
        string sd = "";
        string sd1 = "";

        if (txtclientcode.Text == "")
        {
            lblmessage.Visible = true;
            lblmessage.Text = "Client code should not Blank";
            return;
        }
        if (txtitemcode.Text == "")
        {
            lblmessage.Visible = true;
            lblmessage.Text = "Item code should not Blank";
            return;
        }
        if (txtquantity.Text == "")
        {
            lblmessage.Visible = true;
            lblmessage.Text = "Quantity not Input";
            return;
        }
        if (Convert.ToInt32(Quantity.Text) < Convert.ToInt32(txtquantity.Text))
        {
            lblmessage.Visible = true;
            lblmessage.Text = "Return Quantity is not getter than Issued Quantity";
            return;
        }
        L3TConnection Common0 = new L3TConnection();
        con = Common0.init();

        string sl = "select count(itm_det_serial_no) as tot from InMa_Itm_Serial where  itm_det_icode='" + txtitemcode.Text + "'";

        cmd = new SqlCommand(sl, con);
        dr = cmd.ExecuteReader();

        dr.Read();
        int no = Convert.ToInt32(dr["tot"]);
        dr.Close();
        con.Close();
        con.Dispose();
        if (no > 0 && txtserno.Text == "")
        {
            lblmessage.Visible = true;
            lblmessage.Text = "Invalid serial number";
            return;
        }
        #region Serial number is not equal Quantity
        string[] tmp1 = txtserno.Text.Split(',');

        Dataaccess.L3TConnection Common = new Dataaccess.L3TConnection();
        con = Common.init();

        string sln = "select count(itm_det_serial_no) as tot from InMa_Itm_Serial where  itm_det_icode='" + txtitemcode.Text + "'";

        cmd = new SqlCommand(sln, con);
        dr = cmd.ExecuteReader();

        dr.Read();
        int nb = Convert.ToInt32(dr["tot"]);
        dr.Close();
        if (nb > 0)
        {
            if (Convert.ToDouble(txtquantity.Text) != tmp1.Length)
            {
                lblmessage.Visible = true;
                lblmessage.Text = "Serial number is not equal Quantity";
                return;
            }
        }
        con.Close();
        con.Dispose();

        #endregion

        #region Serial number is valid or same
        if (Convert.ToDouble(txtquantity.Text) == tmp1.Length && tmp1[0].ToString() != "")
        {
            for (int k = 0; k < tmp1.Length; k++)
            {
                sd = tmp1[k].Trim();
                for (int j = 0; j < tmp1.Length; j++)
                {
                    sd1 = tmp1[j].Trim();
                    if (j != k)
                    {
                        if (sd == sd1)
                        {
                            Label1.Visible = true;

                            lblmessage.Visible = true;
                            lblmessage.Text = "Serial number is same";
                            return;
                        }
                    }
                }
            }
            for (int i = 0; i < tmp1.Length; i++)
            {
                Dataaccess.L3TConnection Common2 = new Dataaccess.L3TConnection();
                con = Common2.init();

                string str = "SELECT count(*) as tt FROM InMa_Itm_Serial where itm_det_icode = '" +
                    txtitemcode.Text + "' and itm_det_serial_no='" + tmp1[i].Trim() + "'";

                cmd = new SqlCommand(str, con);
                dr = cmd.ExecuteReader();

                dr.Read();
                int ds = Convert.ToInt32(dr["tt"]);
                dr.Close();
                if (ds == 0)
                {

                    lblmessage.Visible = true;
                    lblmessage.Text = "Serial number is not valid";
                    return;
                }
                con.Close();
                con.Dispose();
            }
            return;
        }

        #endregion


        var dtSrDet = new DataTable();
        dtSrDet = (DataTable)ViewState["datatableParty"];

        dtSrDet.Rows.Add(txtclientcode.Text, txtclientName.Text, txtitemcode.Text, lblitemdescription.Text,
                         UoM.Text, Quantity.Text, txtquantity.Text, txtserno.Text);

        ViewState["datatableParty"] = dtSrDet;
        gvparty.DataSource = dtSrDet;
        gvparty.DataBind();
        cleargeneral();
        btnReurn.Visible = true;
    }

    protected void gvparty_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        lblmessage.Visible = false;
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["datatableParty"];

        DataRow dr = dt.Rows[e.RowIndex];
        dt.Rows.Remove(dr);
        ViewState["datatableParty"] = dt;
        gvparty.DataSource = dt;
        gvparty.DataBind();
    }

    protected void btnAddM_Click(object sender, EventArgs e)
    {
        lblmessage.Visible = false;
        string sd = "";
        string sd1 = "";
        if (txtclcode.Text == "")
        {
            lblmessage.Visible = true;
            lblmessage.Text = "Client code should not Blank";
            return;
        }
        if (txtitcode.Text == "")
        {
            lblmessage.Visible = true;
            lblmessage.Text = "Item code should not Blank";
            return;
        }
        if (txtqt.Text == "")
        {
            lblmessage.Visible = true;
            lblmessage.Text = "Quantity not Input";
            return;
        }

        var dtSrDet = new DataTable();
        dtSrDet = (DataTable)ViewState["datatableParty"];

        dtSrDet.Rows.Add(txtclcode.Text, txtclname.Text, txtitcode.Text, lblitemdes.Text,
                         lbluom.Text, lbliss.Text, txtqt.Text, txtslno.Text);

        ViewState["datatableParty"] = dtSrDet;
        gvManual.DataSource = dtSrDet;
        gvManual.DataBind();
        clearmanual();
        btnReurnM.Visible = true;
    }

    protected void gvManual_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        lblmessage.Visible = false;
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["datatableParty"];

        DataRow dr = dt.Rows[e.RowIndex];
        dt.Rows.Remove(dr);
        ViewState["datatableParty"] = dt;
        gvManual.DataSource = dt;
        gvManual.DataBind();
    }

    protected void gvManual_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmessage.Visible = false;
        var indx = gvManual.SelectedIndex;
        if (indx != -1)
        {
            var dt = new DataTable();
            dt = (DataTable)ViewState["datatableParty"];
            dt.Rows.RemoveAt(indx);
            ViewState["datatableParty"] = dt;
            SetGridData();
            gvManual.SelectedIndex = -1;
            if (gvManual.Rows.Count == 0)
            {
                btnReurnM.Visible = false;
                txtclcode.ReadOnly = false;
            }
            else
            {
                btnReurnM.Visible = true;
                txtclcode.ReadOnly = true;
            }
        }
    }

    protected void gvparty_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmessage.Visible = false;
        var indx = gvparty.SelectedIndex;
        if (indx != -1)
        {
            var dt = new DataTable();
            dt = (DataTable)ViewState["datatableParty"];
            dt.Rows.RemoveAt(indx);
            ViewState["datatableParty"] = dt;
            SetGridPData();

            gvparty.SelectedIndex = -1;
            if (gvparty.Rows.Count == 0)
            {
                btnReurn.Visible = false;
            }
            else
            {
                btnReurn.Visible = true;
            }
        }
    }
    
    protected void btnshow_Click(object sender, EventArgs e)
    {
        lblerrormessage.Visible = false;
        if (txtclisearch.Text == "")
        {
            lblerrormessage.Visible = true;
            lblerrormessage.Text = "Party code should not blank";
            return;
        }
        string[] pcode = txtclisearch.Text.Split(':');
        L3TConnection common = new L3TConnection();
        con = common.init();

        string quert = @"Alter View View_ReturnMemo as SELECT d.Par_Adr_Code, d.par_adr_name, a.Trn_Det_Icode, a.Trn_Det_Itm_Desc, c.Itm_Det_stk_unit,
                            a.Trn_Det_Lin_Qty, a.Trn_Det_Bat_No, b.Trn_Hdr_Com2, a.Trn_Det_Book_Dat, e.Str_Loc_Name,b.Trn_Hdr_Ref
                            FROM            InTr_Trn_Det AS a INNER JOIN
                         InTr_Trn_Hdr AS b ON a.Trn_Det_Ref = b.Trn_Hdr_Ref INNER JOIN
                         InMa_Itm_Det AS c ON c.Itm_Det_Icode = a.Trn_Det_Icode INNER JOIN
                         SaMa_Par_Adr AS d ON d.Par_Adr_Code = b.Trn_Hdr_Pcode INNER JOIN
                         InMa_Str_Loc AS e ON e.Str_Loc_Id = a.Trn_Det_Str_Code
                                WHERE        (a.Trn_Det_Code = 'SRT') AND (b.Trn_Hdr_Ref = '" + pcode[0].Trim() + "')";

        cmd = new SqlCommand(quert, con);
        cmd.ExecuteNonQuery();
        con.Close();
        con.Dispose();
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('frmReportMemoCry.aspx','_newtab');", true);       
    }
}
