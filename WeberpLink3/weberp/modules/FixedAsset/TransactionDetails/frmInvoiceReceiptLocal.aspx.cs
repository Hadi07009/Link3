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
using System.IO;

public partial class frmInvoiceReceiptLocal : System.Web.UI.Page
{
    string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    decimal PayableAmt = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //LoadCombo();

            AutoCompleteExtender1.ContextKey = _connectionString;
            txtTaxAccount_AutoCompleteExtender.ContextKey = _connectionString;   
        
            LoadInitGrid();
            //Session[StaticData.sessionUserId] = "";

            LoadSupplier();

            pDetailsHeader.Visible = false;
            pDetBody.Visible = false; 
                
        }
    }

    private void LoadSupplier()
    {
        var dtInsuTrnSet = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetSupplierByPendingInvoice("LPO"));
        cboSupplier.DataSource = dtInsuTrnSet;
        cboSupplier.DataTextField = "Par_Acc_Name";
        cboSupplier.DataValueField = "Par_Acc_Code";
        cboSupplier.DataBind();
        cboSupplier.Items.Insert(0, "------Select-------"); 
    }

    private void LoadCombo()
    {
        try
        {
            var dtInsuTrnSet = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetTrnByType("IS", "ADM"));
            cboSupplier.DataSource = dtInsuTrnSet;
            cboSupplier.DataTextField = "Trn_Set_Tr_Name";
            cboSupplier.DataValueField = "Trn_Set_IQ_Pfix";
            cboSupplier.DataBind();
            cboSupplier.Items.Insert(0, "------Select-------");            
        }
        catch (Exception e)
        {
 
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

   
   
    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;

        
        //this.CollapsiblePanelExtenderHdr.Collapsed = true;
        //this.CollapsiblePanelExtenderHdr.ClientState = "true";
        //this.CollapsiblePanelExtenderDet.Collapsed = false;
        //this.CollapsiblePanelExtenderDet.ClientState = "false";
    }
        
    

    private void ClearFieldData(string Pst_Flg)
    {            
        cboSupplier.SelectedIndex = 0;
        lblPO.Text = "";
        lblMRR.Text = "";
        txtVatAccount.Text = "";
        txtVatAmount.Text = "";
        txtTaxAccount.Text = "";
        txtTaxAmount.Text = "";
        txtInvoiceNumber.Text = "";
        txtReceiptDate.Text = "";
        
        gridMrr.DataSource = null;        
        gridMrr.DataBind();

        gvMrrData.DataSource = null;
        gvMrrData.DataBind();

        pDetBody.Visible = false;

       
      
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
       
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearFieldData("P");        
    }   
    private bool SaveData(string HPFlag)
    {
        return true;
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        var SrRefNo = "";
        try
        {
            
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
    
   
    protected void cboSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        string suppliercode = cboSupplier.SelectedItem.Value;
        var dt = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetLpobySupplier(suppliercode));
        gvMrrData.DataSource = dt;
        gvMrrData.DataBind();
    }
    protected void gvMrrData_SelectedIndexChanged(object sender, EventArgs e)
    {
         var indx = gvMrrData.SelectedIndex;
         if (indx != -1)
         {
             GridViewRow row = gvMrrData.Rows[indx];

             var poRef =row.Cells[1].Text.Trim();
             var mrrref = row.Cells[3].Text.Trim();
             lblPO.Text = poRef.ToString();
             lblMRR.Text = mrrref.ToString();

             var dt = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetMrrDataByMrrNumber(mrrref));
             gridMrr.DataSource = dt;
             gridMrr.DataBind();
             
             GetRemarks(mrrref);

             string invoice = "";
             string jrnupd = "";
             if (dt.Rows.Count > 0)
             {
                 invoice = dt.Rows[0]["Invoice"].ToString();
                 jrnupd = dt.Rows[0]["Jrnupdpermission"].ToString();
             }
             else
             {
                 invoice = dt.Rows[0]["Invoice"].ToString();
 
             }

             dt = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetVATandTaxCode());
             foreach (DataRow dr in dt.Rows)
             {
                 if (dr["Code"].ToString() == "VAT")
                     txtVatAccount.Text = dr["AccName"].ToString();
                 else
                     txtTaxAccount.Text = dr["AccName"].ToString();
             }
                          
             pDetBody.Visible = true;
             
             if (invoice.ToString()== "Y")
             {
                 if (jrnupd == "Y")
                     btnSave.Visible = true;
                 else
                     btnSave.Visible = false;

                 
                 dt = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetVATandTaxCodeAndAmount(mrrref));
                 if (dt.Rows.Count > 0)
                 {
                     txtVatAccount.Text = dt.Rows[0]["vatacc"].ToString();
                     txtTaxAccount.Text = dt.Rows[0]["taxacc"].ToString();
                     txtVatAmount.Text = dt.Rows[0]["vatamt"].ToString();
                     txtTaxAmount.Text = dt.Rows[0]["taxamt"].ToString();
                     txtInvoiceNumber.Text = dt.Rows[0]["InvoiceNumber"].ToString();
                     txtReceiptDate.Text = Convert.ToDateTime(dt.Rows[0]["InvReceivedDate"].ToString()).ToString("dd/MM/yyyy");

                     LoadUploadFileByRef(mrrref);

                 }       
             }
             else
             {
                 btnSave.Visible = true;
             }

             string userid = Session[StaticData.sessionUserId].ToString();

             GetAccessPermission(userid, "frmInvoiceReceiptAudit");
         }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        string mrrref = lblMRR.Text;
        string poref =lblPO.Text;
        string dcno = "";
        string grnno = "";
        double totamt = 0;

        int lineno=1;
        DateTime dtr = Convert.ToDateTime(txtReceiptDate.Text);
        string trnPeriod = Convert.ToDateTime(txtReceiptDate.Text.Trim()).ToString("MM") + "/" + Convert.ToDateTime(txtReceiptDate.Text.Trim()).ToString("yyyy");
             
        SqlConnection conn = new SqlConnection(_connectionString);
        conn.Open();
        SqlTransaction myTran = conn.BeginTransaction();
        try
        {
            dt = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetJournaldetails(mrrref));

            #region NewAccHdr
            TransactionHeaderDAO DaoHdr = new TransactionHeaderDAO();
            DaoHdr.TrnRefNo = dt.Rows[0]["Trn_Ref_No"].ToString();
            DaoHdr.TrnAccPeriod = trnPeriod;
            DaoHdr.TrnCurrCode = "BDT";
            DaoHdr.TrnCurrRate = 1;
            DaoHdr.TrnDATE = Convert.ToDateTime(txtReceiptDate.Text.Trim());
            DaoHdr.TrnEntryDATE = DateProcess.GetServerDate(_connectionString);
            DaoHdr.TrnEntryFlag = "A";
            DaoHdr.TrnEntryUser = "SUB";
            DaoHdr.TrnJrnType = "RJV";           
            DaoHdr.VoucherType = "J";
            DaoHdr.ModuleName = "Accounts";
            #endregion

            List<TransactionDetailsDAO> tdDaolst = new List<TransactionDetailsDAO>();
                       

            foreach (DataRow dr in dt.Rows)
            {                              
                if (dr["trn_trn_type"].ToString() == "D")
                {                    
                    TransactionDetailsDAO tdDao = new TransactionDetailsDAO();
                    tdDao.TrnAcCode = dr["Trn_Ac_Code"].ToString();
                    tdDao.TrnAcDesc = DataProcess.GetSingleValueFromtable(_connectionString, "budg", "Gl_Coa_Name", "where Gl_Coa_Code='" + tdDao.TrnAcCode + "'");
                    tdDao.TrnAcType = DataProcess.GetSingleValueFromtable(_connectionString, "budg", "Gl_Coa_Type", "where Gl_Coa_Code='" + tdDao.TrnAcCode + "'");
                    tdDao.TrnAmount = Convert.ToDouble(dr["Trn_Amount"].ToString());
                    tdDao.TrnLineNo = lineno.ToString();
                    tdDao.TrnMatch = "";
                    tdDao.TrnNarration = dr["Trn_Narration"].ToString();
                    tdDao.TrnPaymentDATE = DateProcess.GetServerDate(_connectionString);
                    tdDao.TrnRefNo = dr["Trn_Ref_No"].ToString();
                    tdDao.TrnTrntype = "D";
                    tdDao.TrnDueDATE = tdDao.TrnPaymentDATE.AddDays(30);
                    tdDao.TrnChequeNo = "";
                    tdDao.TrnDcNo = dr["Trn_Dc_No"].ToString();
                    tdDao.TrnGRNNo = dr["Trn_GRN_No"].ToString();

                    dcno = tdDao.TrnDcNo;
                    grnno = tdDao.TrnGRNNo;

                    totamt = totamt + tdDao.TrnAmount;

                    tdDaolst.Add(tdDao);

                    lineno++;
                }                                         
            }
            

            dt = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetJournaldetailsBySupplier(mrrref,"S"));

            double vat = Convert.ToDouble(txtVatAmount.Text);
            double tax = Convert.ToDouble(txtTaxAmount.Text);
          

            foreach (DataRow dr in dt.Rows)
            {
                TransactionDetailsDAO tdDao = new TransactionDetailsDAO();
                               
                double ap = totamt - (vat + tax);
                             
                tdDao.TrnAcCode = dr["Trn_Ac_Code"].ToString();
                tdDao.TrnAcDesc = DataProcess.GetSingleValueFromtable(_connectionString, "budg", "Gl_Coa_Name", "where Gl_Coa_Code='" + tdDao.TrnAcCode + "'");
                tdDao.TrnAcType = DataProcess.GetSingleValueFromtable(_connectionString, "budg", "Gl_Coa_Type", "where Gl_Coa_Code='" + tdDao.TrnAcCode + "'");
                tdDao.TrnAmount = Convert.ToDouble(ap);
                tdDao.TrnLineNo = lineno.ToString();
                tdDao.TrnMatch = "";
                tdDao.TrnNarration = dr["Trn_Narration"].ToString();
                tdDao.TrnPaymentDATE = DateProcess.GetServerDate(_connectionString);
                tdDao.TrnRefNo = dr["Trn_Ref_No"].ToString();
                tdDao.TrnTrntype = "C";
                tdDao.TrnDueDATE = tdDao.TrnPaymentDATE.AddDays(30);
                tdDao.TrnChequeNo = "";
                tdDao.TrnDcNo = dr["Trn_Dc_No"].ToString();
                tdDao.TrnGRNNo = dr["Trn_GRN_No"].ToString();

                tdDaolst.Add(tdDao); 
            }
                       
            if (Convert.ToDouble(txtVatAmount.Text) > 0)
            {
                //VAT                   
                lineno++;
                TransactionDetailsDAO tdDao1 = new TransactionDetailsDAO();
                tdDao1.TrnAcCode = txtVatAccount.Text.Split(':')[0].ToString();
                tdDao1.TrnAcDesc = DataProcess.GetSingleValueFromtable(_connectionString, "budg", "Gl_Coa_Name", "where Gl_Coa_Code='" + tdDao1.TrnAcCode + "'");
                tdDao1.TrnAcType = DataProcess.GetSingleValueFromtable(_connectionString, "budg", "Gl_Coa_Type", "where Gl_Coa_Code='" + tdDao1.TrnAcCode + "'");
                tdDao1.TrnAmount = Convert.ToDouble(vat);
                tdDao1.TrnLineNo = lineno.ToString();
                tdDao1.TrnMatch = "";
                tdDao1.TrnNarration ="VAT";
                tdDao1.TrnPaymentDATE = DateProcess.GetServerDate(_connectionString);
                tdDao1.TrnRefNo = DaoHdr.TrnRefNo;
                tdDao1.TrnTrntype = "C";
                tdDao1.TrnDueDATE = tdDao1.TrnPaymentDATE.AddDays(30);
                tdDao1.TrnChequeNo = "";
                tdDao1.TrnDcNo = dcno.ToString();
                tdDao1.TrnGRNNo = grnno.ToString();

                tdDaolst.Add(tdDao1);
            }

            if (Convert.ToDouble(txtTaxAmount.Text) > 0)
            {
                //Tax
                lineno++;
                TransactionDetailsDAO tdDao2 = new TransactionDetailsDAO();
                tdDao2.TrnAcCode = txtTaxAccount.Text.Split(':')[0].ToString();
                tdDao2.TrnAcDesc = DataProcess.GetSingleValueFromtable(_connectionString, "budg", "Gl_Coa_Name", "where Gl_Coa_Code='" + tdDao2.TrnAcCode + "'");
                tdDao2.TrnAcType = DataProcess.GetSingleValueFromtable(_connectionString, "budg", "Gl_Coa_Type", "where Gl_Coa_Code='" + tdDao2.TrnAcCode + "'");
                tdDao2.TrnAmount = Convert.ToDouble(tax);
                tdDao2.TrnLineNo = lineno.ToString();
                tdDao2.TrnMatch = "";
                tdDao2.TrnNarration ="TAX";
                tdDao2.TrnPaymentDATE = DateProcess.GetServerDate(_connectionString);
                tdDao2.TrnRefNo = DaoHdr.TrnRefNo;
                tdDao2.TrnTrntype = "C";
                tdDao2.TrnDueDATE = tdDao2.TrnPaymentDATE.AddDays(30);
                tdDao2.TrnChequeNo = "";
                tdDao2.TrnDcNo = dcno.ToString();
                tdDao2.TrnGRNNo = grnno.ToString();
                tdDaolst.Add(tdDao2);
            }
                                   

            TransactionEntryBLL tBll = new TransactionEntryBLL();

            string vatacc = txtVatAccount.Text.Split(':')[0].ToString();
            string taxacc = txtTaxAccount.Text.Split(':')[0].ToString();
            string invno = txtInvoiceNumber.Text;
            string str = tBll.JournalforReceiptInvoice(_connectionString, DaoHdr, tdDaolst, vatacc, taxacc,invno, true);

            AttachFileSave(mrrref, poref);
                                    

            if (str != "")
            {
                myTran.Commit();
                lblerrormsg.Text ="Invoice receipt successfull";                             
                ClearFieldData("P");               
            }
            else
            {
                myTran.Rollback();
                lblerrormsg.Text = "Invoice receipt has been error. Please try again later";   
            }

        }
        catch(Exception ex)
        {
            myTran.Rollback();
        }

    }

    private void AttachFileSave(string mrrno, string lpono)
    {
        HttpFileCollection hfc = Request.Files;

        string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
        string referenceno = mrrno.ToString();
        string userid = Session[StaticData.sessionUserId].ToString();

        for (int i = 0; i < hfc.Count; i++)
        {
            HttpPostedFile hpf = hfc[i];

            if (hpf.ContentLength > 0)
            {
                string rowfilename = System.IO.Path.GetFileName(hpf.FileName).Replace("'", "''");
                string filename = referenceno + "-" + System.IO.Path.GetFileName(hpf.FileName).Replace("'", "''");
                hpf.SaveAs(Server.MapPath("~/AttachMentfile/") + "\\" + filename);

                string attach = @"insert into [ProcessFileUpload]([ReferenceNo],[FileName],[SavedFileName],[SerialNo],[UploadDate],[UploadBy],[Status],[pReferenceNo])values('" + referenceno + "','" + rowfilename + "','" + filename + "'," + (i + 1) + ",Convert(Datetime,'" + System.DateTime.Now.Date.ToString() + "',103),'" + userid.ToString() + "','Y','" + lpono + "')";

                DataProcess.InsertQuery(_connectionString, attach);

            }

        }
    }
     protected void Button1_Click(object sender, EventArgs e)
    {
        RegisterStartupScript("click", "<script>window.open('../../commercial/frm_po_detailview.aspx?po_ref_no=" + lblPO .Text + "');</script>");        
    }

     private void LoadUploadFileByRef(string refno)
     {
         string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
         DataTable dt = new DataTable();
         dt = DataProcess.GetData(_connectionString, "select [ReferenceNo],[FileName],[SavedFileName] from [ProcessFileUpload] where ReferenceNo='" + refno.ToString() + "' order by [ReferenceNo],[SerialNo]");
         gdvFileLoad.DataSource = dt;
         gdvFileLoad.DataBind();
     }
    
     private void GetFile(String strPath, String strSuggestedName)
     {

         String strServerPath;
         System.IO.FileInfo objSourceFileInfo;
         strServerPath = this.Server.MapPath(strSuggestedName);
         objSourceFileInfo = new System.IO.FileInfo(strPath);

         if (objSourceFileInfo.Exists)
         {
             Response.Clear();
             Response.ContentType = "application/octet-stream";
             Response.AddHeader("Content-Disposition", "attachment; filename=" + strSuggestedName);
             Response.AddHeader("Content-Length", objSourceFileInfo.Length.ToString());
             Response.WriteFile(objSourceFileInfo.FullName);
             Response.Flush();
             Response.End();
         }
         else
         {
             Response.Write("This file does not exist.");
         }
     }
     protected void gdvFileLoad_RowDataBound(object sender, GridViewRowEventArgs e)
     {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
             e.Row.Cells[0].Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
             e.Row.Cells[0].Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";

             e.Row.Cells[1].Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
             e.Row.Cells[1].Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
             e.Row.Cells[2].Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
             e.Row.Cells[2].Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";

             e.Row.Cells[0].Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gdvFileLoad, "Select$" + e.Row.RowIndex);
             e.Row.Cells[1].Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gdvFileLoad, "Select$" + e.Row.RowIndex);
             e.Row.Cells[2].Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gdvFileLoad, "Select$" + e.Row.RowIndex);

         }
         e.Row.Cells[0].Visible = false;
         e.Row.Cells[2].Visible = false;
         e.Row.Cells[3].Visible = false;
     }

     protected void gdvFileLoad_SelectedIndexChanged(object sender, EventArgs e)
     {
         int indx = gdvFileLoad.SelectedIndex;
         string gg = gdvFileLoad.Rows[indx].Cells[2].Text.Trim();
         String F1Path, F1Name;
         string abc = Server.MapPath("~/AttachMentfile/") + gg.ToString().Replace("&amp;", "&");
         F1Path = abc.ToString();
         F1Name = Path.GetFileName(F1Path);
         GetFile(F1Path, F1Name);
     }

     protected void gridMrr_RowDataBound(object sender, GridViewRowEventArgs e)
     {

         if (e.Row.RowType == DataControlRowType.Header)
         {
             
         }
         else if (e.Row.RowType == DataControlRowType.DataRow)
         {
             PayableAmt = PayableAmt + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));

             e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
             e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
             e.Row.Cells[8].Text = Convert.ToDecimal(e.Row.Cells[8].Text).ToString("#,##0.00");
             e.Row.Cells[9].Text = Convert.ToDecimal(e.Row.Cells[9].Text).ToString("#,##0.00");

         }
         else if (e.Row.RowType == DataControlRowType.Footer)
         {
             e.Row.Cells[8].Text = "TOTAL:";            
             e.Row.Cells[9].Text = PayableAmt.ToString("#,##0.00");
             e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
             e.Row.Font.Bold = true;
         }

         e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
         e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
         e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
         e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
         e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
         e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;

         e.Row.Cells[10].Visible = false;
         e.Row.Cells[11].Visible = false; 

     }
     protected void btnComments_Click(object sender, EventArgs e)
     {
         try
         {
             string sql = "";
             string userid = Session[StaticData.sessionUserId].ToString();
             sql = "insert into [tblComments](ReferenceNo,CommentsBy,Comments,CommentsDate)values('" + lblMRR.Text + "','" + userid.ToString() + "','" + txtRemarks.Text + "',Convert(Datetime,'" + System.DateTime.Now + "',103))";
             DataProcess.ExecuteQuery(_connectionString, sql);

             txtRemarks.Text = "";

             GetRemarks(lblMRR.Text);

         }
         catch (Exception ex)
         { 
         }
     }

     private void GetRemarks(string rRemarks)
     {
         try
         {
             DataTable dt = new DataTable();
             string sql = "";
             sql = "select *,UserName from [tblComments] a inner join tbluserinfo b on a.CommentsBy=b.userid where ReferenceNo='" + rRemarks.ToString() + "' order by rLineNo desc";
             dt = DataProcess.GetData(_connectionString, sql);
             string remarks = "Please see all remarks below:";
             string remarks1 = "";
             string renarks2 = "";
             string remarksnew = "";
             string remarksline = "----------------------------------";
             int ln = 0;
             foreach (DataRow dr in dt.Rows)
             {
                 ln = ln + 1;
                 remarksnew = ln.ToString() + ". " + dr["UserName"].ToString() + " >> " + dr["Comments"].ToString();
                 remarks += "\n" + remarksline.ToString();
                 remarks += "\n" + remarksnew.ToString();
                 remarks += "\n" + String.Format("{0:f}", Convert.ToDateTime(dr["CommentsDate"].ToString())); ; //Convert.ToDateTime(dr["CommentsDate"].ToString("dd/MM/yyyy"));
             }
             txtRemarksAll.Text = remarks;
             int numLines = remarks.ToString().Split('\n').Length;
             string[] lines = txtRemarksAll.Text.Split('\n');
             numLines = lines.Length - 1;
             txtRemarksAll.Height = (numLines + ln) * 15 + numLines*10;
             txtRemarksAll.Enabled = false;
         }
         catch (Exception ex)
         { 
         }
     }
     protected void btnVatTaxUpdate_Click(object sender, EventArgs e)
     {
         try
         {
             string sql = "";
             string userid = Session[StaticData.sessionUserId].ToString();
             sql = "insert into [tblComments](ReferenceNo,CommentsBy,Comments,CommentsDate)values('" + lblMRR.Text + "','" + userid.ToString() + "','" + txtRemarks.Text + "','" + System.DateTime.Now + "')";
             DataProcess.ExecuteQuery(_connectionString, sql);

             sql = "update AccPaybleProgress set Jrnupdpermission='Y' where ReferenceNumber='" + lblMRR.Text + "'";
             DataProcess.ExecuteQuery(_connectionString, sql);

             txtRemarks.Text = "";

             GetRemarks(lblMRR.Text);

         }
         catch (Exception ex)
         {
         }
     }

     private void GetAccessPermission(string userid,string formname)
     {
         try
         {
             DataTable dt = new DataTable();
             string sql = "";
             sql = "select * from tblFormControlAccessPermission where userid='" + userid + "' and FormName='" + formname + "'";
             dt = DataProcess.GetData(_connectionString, sql);
             foreach (DataRow dr in dt.Rows)
             {
                 if (dr["controllname"].ToString() == "btnSave" && dr["AccessPermission"].ToString() == "N")
                 {
                     btnSave.Enabled = false;
                     btnSave.Visible = false; 
                 }
                 else if (dr["controllname"].ToString() == "btnComments" && dr["AccessPermission"].ToString() == "N")
                 {
                     btnComments.Enabled = false;
                     btnComments.Visible = false;
                 }
                 else if (dr["controllname"].ToString() == "btnVatTaxUpdate" && dr["AccessPermission"].ToString() == "N")
                 {
                     btnVatTaxUpdate.Enabled = false;
                     btnVatTaxUpdate.Visible = false;
                 }
                 else if (dr["controllname"].ToString() == "btnPOview" && dr["AccessPermission"].ToString() == "N")
                 {
                     btnPOview.Enabled = false;
                     btnPOview.Visible = false;
                 }

             }
         }
         catch (Exception ex)
         { 
         }
     }

}
