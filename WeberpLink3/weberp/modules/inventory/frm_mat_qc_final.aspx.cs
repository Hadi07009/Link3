using System;
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
using LibraryDAL.SCBLDataSetTableAdapters;
using LibraryDAL.SCBL2DataSetTableAdapters;

public partial class frm_mat_qc_final : System.Web.UI.Page
{
    string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";
        clsStatic.MsgConfirmBox(btnconfirm, "Are you sure to confirm receive above items.");
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
        DataTable dt = new DataTable();
               
        
        
        dt.Rows.Clear();
        dt.Columns.Clear();

        dt.Columns.Add("SL", typeof(int));
        dt.Columns.Add("REF", typeof(string));  
        dt.Columns.Add("ITEM", typeof(string));
        dt.Columns.Add("UOM", typeof(string));
        dt.Columns.Add("INS QTY", typeof(decimal));
        dt.Columns.Add("OK QTY", typeof(decimal));
        dt.Columns.Add("REJ QTY", typeof(decimal));
        dt.Columns.Add("REMARKS", typeof(string));
        dt.Columns.Add("BRAND", typeof(string));
        dt.Columns.Add("ORIGIN", typeof(string));
        dt.Columns.Add("PACKING", typeof(string));

        for (i = 0; i < cnt; i++)
        {
            if(mrrdata[i]==null) break;
            lblpartydet.Text = mrrdata[i].Pcode + ":" + mrrdata[i].Pdet;
            dt.Rows.Add(mrrdata[i].Seqno, mrrdata[i].Ref_no, mrrdata[i].Idet, mrrdata[i].Uom, mrrdata[i].Insqty, mrrdata[i].OkQty, mrrdata[i].RejQty, mrrdata[i].Remarks, mrrdata[i].Brand, mrrdata[i].Origin, mrrdata[i].Packing);
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

    private string get_mat_rec_ret_ref(string trn_type)
    {
        string ref_no = "";
        tbl_mat_rec_retTableAdapter matrec = new tbl_mat_rec_retTableAdapter();
        double max_ref = Convert.ToDouble(matrec.GetMaxRef()) + 1;
        ref_no = trn_type + "-" + string.Format("{0:000000}", max_ref);

        return ref_no;
    }

    protected void btnconfirm_Click(object sender, EventArgs e)
    {
        clsMrrData[] mrrdata = (clsMrrData[])Session[clsStatic.sessionMrrDetData];
        if (mrrdata==null) return;
        if (mrrdata.Length == 0) return;
        
       
        //PuTr_PO_Hdr_ScblTableAdapter hdr = new PuTr_PO_Hdr_ScblTableAdapter();
        PuTr_PO_Det_Scbl2TableAdapter det = new PuTr_PO_Det_Scbl2TableAdapter();
        SCBL2DataSet.PuTr_PO_Det_Scbl2DataTable dtdet = new SCBL2DataSet.PuTr_PO_Det_Scbl2DataTable();
        SCBL2DataSet.PuTr_PO_Det_Scbl2Row[] dr = new SCBL2DataSet.PuTr_PO_Det_Scbl2Row[mrrdata.Length];
        //SCBLDataSet.PuTr_PO_Hdr_ScblRow hdrdr;
        tbl_mat_rec_retTableAdapter matrec = new tbl_mat_rec_retTableAdapter();
       
        string oprcode = current.UserId.ToString();
        if (oprcode.Length > 3) oprcode = oprcode.Substring(0, 3);
        string ref_no = mrrdata[0].Ref_no;
        //hdrdr = hdr.GetHdrDataByRef(ref_no)[0];        
        string sql = SqlgenerateForFixedAsset.GetHdrDataByRef(ref_no.ToString());
        
        DataTable hdrdr = DataProcess.GetData(_connectionString, sql);

        

        string ret_rec_ref = get_mat_rec_ret_ref("RETURN");
        string ret_rec_ref_ok = get_mat_rec_ret_ref("OK");
        if (hdrdr.Rows[0]["PO_Hdr_Status"].ToString() != "APP") return;                
        bool updateflg = true;
        decimal linqty, orgqty, insqty, okqty, rejqty, balanceqty, insqty_new; 
        int i;
        int tot = 0;
        int cnt = mrrdata.Length;

        for (i = 0; i < cnt; i++)
        {
            if (mrrdata[i] == null) break;
            dtdet = det.GetDetByRefItem(ref_no, mrrdata[i].Icode);
            if (dtdet.Rows.Count == 1)
                dr[i] = dtdet[0];
            else
                dr[i] = det.GetDataByRefLine(ref_no, (short)mrrdata[i].LineNo)[0];
        }
       
        //SqlTransaction myTrn = HelperTA.OpenTransaction(det.Connection);


        SqlConnection conn = new SqlConnection(_connectionString);
        conn.Open();
        SqlTransaction myTran = conn.BeginTransaction();
              
        try
        {
            //det.AttachTransaction(myTrn);
            //matrec.AttachTransaction(myTrn);
            
            for (i = 0; i < cnt; i++)
            {
                if (mrrdata[i] == null) break;
                tot++;
                okqty = mrrdata[i].OkQty;
                rejqty = mrrdata[i].RejQty;
               
                linqty = Convert.ToDecimal(dr[i].PO_Det_Lin_Qty);
                orgqty = Convert.ToDecimal(dr[i].PO_Det_Org_QTY);
                insqty = Convert.ToDecimal(dr[i].PO_Det_Ins_QTY);
                
                if (insqty != (okqty + rejqty)) { updateflg = false; goto transaction_complete; }

                balanceqty = (linqty - orgqty - insqty) + rejqty;

                insqty_new = insqty - rejqty;

                //set data rec ret transaction
                                
                if (okqty > 0)
                {
                    //matrec.InsertMatRecRet(ret_rec_ref_ok, tot, "OK", DateTime.Now, oprcode, ref_no, (int)dr[i].PO_Det_Lno, hdrdr.PO_Hdr_Pcode, hdrdr.PO_Hdr_Com1, dr[i].PO_Det_Icode, dr[i].PO_Det_Itm_Desc, dr[i].PO_Det_Itm_Uom, mrrdata[i].Brand, mrrdata[i].Origin, mrrdata[i].Packing, mrrdata[i].OkQty, (decimal)dr[i].PO_Det_Lin_Qty, orgqty, insqty_new, balanceqty, dr[i].PO_Det_Lin_Rat, mrrdata[i].Remarks, txtcomm.Text);
                    updateflg=DataProcess.ExecuteQuery(_connectionString, SqlgenerateForFixedAsset.InsertMatRecRet(ret_rec_ref_ok, tot, "OK", DateTime.Now, oprcode, ref_no, (int)dr[i].PO_Det_Lno, hdrdr.Rows[0]["PO_Hdr_Pcode"].ToString(), hdrdr.Rows[0]["PO_Hdr_Com1"].ToString(), dr[i].PO_Det_Icode, dr[i].PO_Det_Itm_Desc, dr[i].PO_Det_Itm_Uom, mrrdata[i].Brand, mrrdata[i].Origin, mrrdata[i].Packing, (double)mrrdata[i].OkQty, (double)dr[i].PO_Det_Lin_Qty, (double)orgqty, (double)insqty_new, (double)balanceqty, (double)dr[i].PO_Det_Lin_Rat, mrrdata[i].Remarks, txtcomm.Text));
                    if(!updateflg)
                        goto transaction_complete;

                }

                if (rejqty > 0)
                {
                    //matrec.InsertMatRecRet(ret_rec_ref, tot, "RETURN", DateTime.Now, oprcode, ref_no, (int)dr[i].PO_Det_Lno, hdrdr.PO_Hdr_Pcode, hdrdr.PO_Hdr_Com1, dr[i].PO_Det_Icode, dr[i].PO_Det_Itm_Desc, dr[i].PO_Det_Itm_Uom, mrrdata[i].Brand, mrrdata[i].Origin, mrrdata[i].Packing, mrrdata[i].RejQty, (decimal)dr[i].PO_Det_Lin_Qty, orgqty, insqty_new, balanceqty, dr[i].PO_Det_Lin_Rat, mrrdata[i].Remarks, txtcomm.Text);
                    updateflg=DataProcess.ExecuteQuery(_connectionString, SqlgenerateForFixedAsset.InsertMatRecRet(ret_rec_ref, tot, "RETURN", DateTime.Now, oprcode, ref_no, (int)dr[i].PO_Det_Lno, hdrdr.Rows[0]["PO_Hdr_Pcode"].ToString(), hdrdr.Rows[0]["PO_Hdr_Com1"].ToString(), dr[i].PO_Det_Icode, dr[i].PO_Det_Itm_Desc, dr[i].PO_Det_Itm_Uom, mrrdata[i].Brand, mrrdata[i].Origin, mrrdata[i].Packing, (double)mrrdata[i].RejQty, (double)dr[i].PO_Det_Lin_Qty, (double)orgqty, (double)insqty_new, (double)balanceqty, (double)dr[i].PO_Det_Lin_Rat, mrrdata[i].Remarks, txtcomm.Text));
                    if (!updateflg)
                        goto transaction_complete;
                }
                //update po data
                //det.UpdateQtyForInsp((double)orgqty, (double)insqty_new, (double)balanceqty, "", ref_no, (short)mrrdata[i].LineNo);

                updateflg=DataProcess.ExecuteQuery(_connectionString, SqlgenerateForFixedAsset.UpdateQueryForQc((double)insqty, (double)okqty, ref_no.ToString(), dr[i].PO_Det_Icode.ToString(), (int)dr[i].PO_Det_Lno));
                if (!updateflg)
                    goto transaction_complete;

                #region InsertSerial

                var serial = mrrdata[i].Packing.ToString().Trim().Split(',');                
                foreach (var s in serial)
                {
                    var serialNo = s.ToString();
                    if (serialNo != "")
                    {
                        DataProcess.InsertQuery(_connectionString, SqlgenerateForFixedAsset.InsertItemSerialtemp(mrrdata[0].Ref_no,dr[i].PO_Det_Icode, serialNo, ret_rec_ref, "", "IN", "QC",
                                                      Convert.ToDateTime(DateTime.Now), "Good", ""));

                    }
                }
                #endregion
                                               
            }                              


        transaction_complete:

            if (updateflg)
            {
                myTran.Commit();
            }
            else
            {
                myTran.Rollback();
            }

        }
        catch
        {
            myTran.Rollback();
        }
        finally
        {
            //HelperTA.CloseTransaction(det.Connection, myTrn);
            conn.Close();
        }

        if (updateflg)
        {
            Session[clsStatic.sessionMrrDetData] = null;
            lbllogref.Text = ret_rec_ref;
            ModalPopupExtender5.Show();
            btnCancel_Click(null, null);
        }

    }

    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("./frm_mat_qc.aspx");
    }
    protected void btnok_Click(object sender, EventArgs e)
    {
        Response.Redirect("./frm_mat_qc.aspx");
    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
        Response.Redirect("./frm_mat_qc.aspx?ret_rec_ref=" + lbllogref.Text);
    }
    protected void gdItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[10].Text = "Serial Number";
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        { 
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        { 
        }

        e.Row.Cells[10].Width = 250;
        
    }
}
