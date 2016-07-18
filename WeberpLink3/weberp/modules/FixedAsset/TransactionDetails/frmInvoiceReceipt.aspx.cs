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

public partial class frmInvoiceReceipt : System.Web.UI.Page
{
    string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    decimal PayableAmt = 0;
    decimal Expense = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication();

        if (!Page.IsPostBack)
        {            
            AutoCompleteExtender1.ContextKey = _connectionString;
            txtTaxAccount_AutoCompleteExtender.ContextKey = _connectionString;
            txtAccHead_AutoCompleteExtender.ContextKey = _connectionString;               
        
            LoadInitGrid();
            //Session[StaticData.sessionUserId] = "";

            LoadSupplier();

            pDetailsHeader.Visible = false;
            pDetBody.Visible = false; 
                
        }
    }

    private void LoadSupplier()
    {
        var dtInsuTrnSet = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetSupplierByPendingPO());
        cboSupplier.DataSource = dtInsuTrnSet;
        cboSupplier.DataTextField = "Par_Acc_Name";
        cboSupplier.DataValueField = "Par_Acc_Code";
        cboSupplier.DataBind();
        cboSupplier.Items.Insert(0, "------Select-------"); 
    }

    private void LoadCombo(string pType)
    {
        try
        {
            var dt = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetLCExpense(pType));
            ddlExpense.DataSource = dt;
            ddlExpense.DataTextField = "ExpenseName";
            ddlExpense.DataValueField = "ExpenseID";
            ddlExpense.DataBind();
            ddlExpense.Items.Insert(0, "------Select-------");            
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
        dt.Columns.Add("ExpenseID", typeof(string));   
        dt.Columns.Add("ExpenseHead", typeof(string));
        dt.Columns.Add("ExpAmount", typeof(string));
        dt.Columns.Add("AccHead", typeof(string));   
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
        pDetBody.Visible = false;
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
                 txtLcNumber.Text = dt.Rows[0]["LCNumber"].ToString();
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
                 {
                     btnSave.Visible = true;
                     ControllValidation("Y");
                 }
                 else
                 {
                     btnSave.Visible = false;
                     ControllValidation("N");
                 }


                 dt = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetVATandTaxAmount(mrrref));
                 if (dt.Rows.Count > 0)
                 {
                     string dtn = Convert.ToDateTime(System.DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                     txtVatAccount.Text = dt.Rows[0]["vatacc"].ToString()!=""?dt.Rows[0]["vatacc"].ToString():txtVatAccount.Text;
                     txtTaxAccount.Text = dt.Rows[0]["taxacc"].ToString()!=""?dt.Rows[0]["taxacc"].ToString():txtTaxAccount.Text;
                     txtVatAmount.Text = dt.Rows[0]["vatamt"].ToString();
                     txtTaxAmount.Text = dt.Rows[0]["taxamt"].ToString();
                     txtInvoiceNumber.Text = dt.Rows[0]["InvoiceNumber"].ToString();
                     txtReceiptDate.Text = Convert.ToDateTime(dt.Rows[0]["InvReceivedDate"] == "" ? dtn : dt.Rows[0]["InvReceivedDate"].ToString()).ToString("dd/MM/yyyy");

                     LoadUploadFileByRef(mrrref);

                 }       
             }
             else
             {
                 btnSave.Visible = true;
             }
                       

             GetExpense(poRef, mrrref);

             LoadCombo(GetPoType(poRef));

             string userid = Session[StaticData.sessionUserId].ToString();

             GetAccessPermission(userid, "frmInvoiceReceipt");
         }
    }

    private string GetPoType(string porefno)
    {
        DataTable dtppType = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetPOTypeByReference(porefno));
        string pType = "";
        if (dtppType.Rows.Count > 0)
        {
            pType = dtppType.Rows[0]["POType"].ToString();
        }

        return pType; 
    }

    private void GetExpense(string Pono, string Mrrno)
    { 
        try
        {
            String sql = @"select b.ExpenseID,b.ExpenseName as ExpenseHead,a.ExpAmount,a.AccHead from AccLcExpenseDet a inner join AccLCExpenseHead b on a.ExpenseID=b.ExpenseID
                            where a.PONumber='" + Pono + "' and a.MRRNumber='" + Mrrno + "'";
            DataTable dt = new DataTable();
            dt = DataProcess.GetData(_connectionString, sql);
            ViewState["datatable"] = dt;
            gdvExpense.DataSource = dt;
            gdvExpense.DataBind();            
        }
        catch(Exception ex)
        {
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
        int ii = 0;
        double lccost=0;
        double distratio = 0;
        string sql = "";
        string entryuser="";

        int lineno=1;
        DateTime dtr = Convert.ToDateTime(txtReceiptDate.Text);
        string trnPeriod = Convert.ToDateTime(txtReceiptDate.Text.Trim()).ToString("MM") + "/" + Convert.ToDateTime(txtReceiptDate.Text.Trim()).ToString("yyyy");
             
        SqlConnection conn = new SqlConnection(_connectionString);
        conn.Open();
        SqlTransaction myTran = conn.BeginTransaction();
        try
        {                                                                
            //Lc expense
            foreach (GridViewRow gr in gdvExpense.Rows)
            {
                string pono = lblPO.Text;
                string lcno = txtLcNumber.Text;
                string Mrrno = lblMRR.Text;
                string expid = gr.Cells[0].Text.ToString();
                string expname = gr.Cells[1].Text.ToString();
                double amt = Convert.ToDouble(gr.Cells[2].Text.ToString());
                string AccCode = gr.Cells[3].Text.ToString();
                entryuser = Session[StaticData.sessionUserId].ToString();
                DateTime entrydate = System.DateTime.Now;
                if (ii == 0)
                    DataProcess.ExecuteQuery(_connectionString, SqlgenerateForFixedAsset.DeleteLcExpense(pono, Mrrno));

                DataProcess.ExecuteQuery(_connectionString, SqlgenerateForFixedAsset.InsertLcExpense(pono, lcno, Mrrno, expid, amt, AccCode, entryuser, entrydate));
                ii++;        
            }
                        
            string vatacc = "";
            string taxacc = "";
            string invno = txtInvoiceNumber.Text;
           
            sql = "";
            if (txtVatAccount.Text.Equals("") == false)
            {
                vatacc = txtVatAccount.Text.Split(':')[0].ToString();
            }
            if (txtVatAccount.Text.Equals("") == false)
            {
                taxacc = txtTaxAccount.Text.Split(':')[0].ToString();
            }

            sql = @"update AccPaybleProgress set InvoiceReceived='Y',Jrnupdpermission='N',InvReceivedDate=convert(datetime,'" + txtReceiptDate.Text + "',103),InvReceivedBy='" + entryuser.ToString() + "',UpdateUserid='" + entryuser.ToString() + "',UpdateDate=convert(datetime,'" + txtReceiptDate.Text + "',103),VATAcc='" + vatacc.ToString() + "',TaxAcc='" + taxacc + "',InvoiceNumber='" + invno.ToString() + "',VATamt='" + txtVatAmount.Text + "',TAXamt='" + txtTaxAmount.Text + "', LCNumber='" + txtLcNumber.Text + "' where ReferenceNumber='" + mrrref.ToString() + "'";
            DataProcess.ExecuteQuery(_connectionString, sql);           

            AttachFileSave(mrrref, poref);

            myTran.Commit();
            lblerrormsg.Text = "Data Saved Successfull";
            ClearFieldData("P");                          

        }
        catch(Exception ex)
        {
            myTran.Rollback();
            lblerrormsg.Text = "Invoice receipt has been error. Please try again later";   
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
         AddComments();
     }

     private void AddComments()
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
             
             AddComments();
             
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
             DisableAllpermission();

             DataTable dt = new DataTable();
             string sql = "";
             sql = "select * from tblFormControlAccessPermission where userid='" + userid + "' and FormName='" + formname + "'";
             dt = DataProcess.GetData(_connectionString, sql);
                       
             foreach (DataRow dr in dt.Rows)
             {
                 if (dr["controllname"].ToString() == "btnSave" && dr["AccessPermission"].ToString() == "Y")
                 {
                     btnSave.Enabled = true;
                     btnSave.Visible = true; 
                 }
                 if (dr["controllname"].ToString() == "btnSaveFinal" && dr["AccessPermission"].ToString() == "Y")
                 {
                     btnSaveFinal.Enabled = true;
                     btnSaveFinal.Visible = true;
                 }
                 else if (dr["controllname"].ToString() == "btnComments" && dr["AccessPermission"].ToString() == "Y")
                 {
                     btnComments.Enabled = true;
                     btnComments.Visible = true;
                 }
                 else if (dr["controllname"].ToString() == "btnVatTaxUpdate" && dr["AccessPermission"].ToString() == "Y")
                 {
                     btnVatTaxUpdate.Enabled = true;
                     btnVatTaxUpdate.Visible = true;
                 }
                 else if (dr["controllname"].ToString() == "btnPOview" && dr["AccessPermission"].ToString() == "Y")
                 {
                     btnPOview.Enabled = true;
                     btnPOview.Visible = true;
                 }

             }
         }
         catch (Exception ex)
         { 
         }
     }


     private void DisableAllpermission()
     {
         //Initially disable all permission
         btnSave.Enabled = false;
         btnSave.Visible = false;
         btnSaveFinal.Enabled = false;
         btnSaveFinal.Visible = false;
         btnComments.Enabled = false;
         btnComments.Visible = false;
         btnVatTaxUpdate.Enabled = false;
         btnVatTaxUpdate.Visible = false;
         btnPOview.Enabled = false;
         btnPOview.Visible = false; 
     }

     protected void btnAdd_Click(object sender, EventArgs e)
     {
         DataTable dt = new DataTable();  
         dt = (DataTable)ViewState["datatable"];
         dt.Rows.Add(ddlExpense.SelectedItem.Value, ddlExpense.SelectedItem.Text, txtExpAmt.Text, txtAccHead.Text);
         ViewState["datatable"] = dt;
         gdvExpense.DataSource = dt;
         gdvExpense.DataBind();

         ddlExpense.SelectedIndex = -1;
         txtExpAmt.Text = "";
         txtAccHead.Text = "";

     }
     protected void gdvExpense_SelectedIndexChanged(object sender, EventArgs e)
     {
         var indx = gdvExpense.SelectedIndex;
         if (indx != -1)
         {
             var dt = new DataTable();
             dt = (DataTable)ViewState["datatable"];
             dt.Rows.RemoveAt(indx);
             ViewState["datatable"] = dt;
             gdvExpense.DataSource = dt;
             gdvExpense.DataBind();
             gdvExpense.SelectedIndex = -1;
         }      
     }


     protected void gdvExpense_RowDataBound(object sender, GridViewRowEventArgs e)
     {
         if (e.Row.RowType == DataControlRowType.Header)
         {

         }
         else if (e.Row.RowType == DataControlRowType.DataRow)
         {
             Expense = Expense + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ExpAmount"));

             e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
             e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
             e.Row.Cells[2].Text = Convert.ToDecimal(e.Row.Cells[2].Text).ToString("#,##0.00");           
         }
         else if (e.Row.RowType == DataControlRowType.Footer)
         {
             e.Row.Cells[1].Text = "TOTAL:";
             e.Row.Cells[2].Text = Expense.ToString("#,##0.00");
             e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
             e.Row.Font.Bold = true;
         }    
        
     }
     protected void btnSaveFinal_Click(object sender, EventArgs e)
     {
        DataTable dt = new DataTable();
        string mrrref = lblMRR.Text;
        string poref =lblPO.Text;
        string dcno = "";
        string grnno = "";
        double totamt = 0;
        int ii = 0;
        double lccost=0;
        double distratio = 0;
        string APCode = ""; 

        int lineno=0;
        DateTime dtr = Convert.ToDateTime(txtReceiptDate.Text);
        string trnPeriod = Convert.ToDateTime(txtReceiptDate.Text.Trim()).ToString("MM") + "/" + Convert.ToDateTime(txtReceiptDate.Text.Trim()).ToString("yyyy");
             
        SqlConnection conn = new SqlConnection(_connectionString);
        conn.Open();
        SqlTransaction myTran = conn.BeginTransaction();
        try
        {                  

            #region NewAccHdr
            TransactionHeaderDAO DaoHdr = new TransactionHeaderDAO();
            DaoHdr.TrnRefNo ="";
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

            dt = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetMrrDataByMrrNumberForFinalJournal(mrrref));

            #region Inventory

            foreach (DataRow dr in dt.Rows)
            {
                lineno++;

                TransactionDetailsDAO tdDao1 = new TransactionDetailsDAO();
                tdDao1.TrnAcCode = dr["ItemAcc"].ToString();
                tdDao1.TrnAcDesc = DataProcess.GetSingleValueFromtable(_connectionString, "budg", "Gl_Coa_Name", "where Gl_Coa_Code='" + tdDao1.TrnAcCode + "'");
                tdDao1.TrnAcType = DataProcess.GetSingleValueFromtable(_connectionString, "budg", "Gl_Coa_Type", "where Gl_Coa_Code='" + tdDao1.TrnAcCode + "'");
                tdDao1.TrnAmount = Convert.ToDouble(dr["Amount"].ToString());
                tdDao1.TrnLineNo = lineno.ToString();
                tdDao1.TrnMatch = "";
                tdDao1.TrnNarration = dr["Item Name"].ToString() +"; Qty:"+ dr["MRR Qty"].ToString()+"; Rate:" + dr["Rate"].ToString();
                tdDao1.TrnPaymentDATE = DateProcess.GetServerDate(_connectionString);
                tdDao1.TrnRefNo = "";
                tdDao1.TrnTrntype = "D";
                tdDao1.TrnDueDATE = tdDao1.TrnPaymentDATE.AddDays(30);
                tdDao1.TrnChequeNo = "";
                tdDao1.TrnDcNo = dr["PORef"].ToString();
                tdDao1.TrnGRNNo = dr["MRR Number"].ToString();

                APCode = dr["APCode"].ToString();


                dcno = tdDao1.TrnDcNo;
                grnno = tdDao1.TrnGRNNo;

                totamt = totamt + tdDao1.TrnAmount;

                tdDaolst.Add(tdDao1);

                
            }

            #endregion


            #region AP
            TransactionDetailsDAO tdDao2 = new TransactionDetailsDAO();           

            double vat = Convert.ToDouble(txtVatAmount.Text);
            double tax = Convert.ToDouble(txtTaxAmount.Text);
            double ap = totamt - (vat + tax);

            lineno++;

            tdDao2.TrnAcCode = APCode.ToString();
            tdDao2.TrnAcDesc = DataProcess.GetSingleValueFromtable(_connectionString, "budg", "Gl_Coa_Name", "where Gl_Coa_Code='" + tdDao2.TrnAcCode + "'");
            tdDao2.TrnAcType = DataProcess.GetSingleValueFromtable(_connectionString, "budg", "Gl_Coa_Type", "where Gl_Coa_Code='" + tdDao2.TrnAcCode + "'");
            tdDao2.TrnAmount = Convert.ToDouble(ap);
            tdDao2.TrnLineNo = lineno.ToString();
            tdDao2.TrnMatch = "";
            tdDao2.TrnNarration ="AP";
            tdDao2.TrnPaymentDATE = DateProcess.GetServerDate(_connectionString);
            tdDao2.TrnRefNo ="";
            tdDao2.TrnTrntype = "C";
            tdDao2.TrnDueDATE = tdDao2.TrnPaymentDATE.AddDays(30);
            tdDao2.TrnChequeNo = "";
            tdDao2.TrnDcNo = dcno.ToString();
            tdDao2.TrnGRNNo = grnno.ToString();

            tdDaolst.Add(tdDao2);

            #endregion


            #region VAT
            if (Convert.ToDouble(txtVatAmount.Text) > 0)
            {
                //VAT                   
                lineno++;
                TransactionDetailsDAO tdDao3 = new TransactionDetailsDAO();
                tdDao3.TrnAcCode = txtVatAccount.Text.Split(':')[0].ToString();
                tdDao3.TrnAcDesc = DataProcess.GetSingleValueFromtable(_connectionString, "budg", "Gl_Coa_Name", "where Gl_Coa_Code='" + tdDao3.TrnAcCode + "'");
                tdDao3.TrnAcType = DataProcess.GetSingleValueFromtable(_connectionString, "budg", "Gl_Coa_Type", "where Gl_Coa_Code='" + tdDao3.TrnAcCode + "'");
                tdDao3.TrnAmount = Convert.ToDouble(vat);
                tdDao3.TrnLineNo = lineno.ToString();
                tdDao3.TrnMatch = "";
                tdDao3.TrnNarration = "VAT";
                tdDao3.TrnPaymentDATE = DateProcess.GetServerDate(_connectionString);
                tdDao3.TrnRefNo = DaoHdr.TrnRefNo;
                tdDao3.TrnTrntype = "C";
                tdDao3.TrnDueDATE = tdDao3.TrnPaymentDATE.AddDays(30);
                tdDao3.TrnChequeNo = "";
                tdDao3.TrnDcNo = dcno.ToString();
                tdDao3.TrnGRNNo = grnno.ToString();

                tdDaolst.Add(tdDao3);
            }

            #endregion


            #region TAX

            if (Convert.ToDouble(txtTaxAmount.Text) > 0)
            {
                //Tax
                lineno++;
                TransactionDetailsDAO tdDao4 = new TransactionDetailsDAO();
                tdDao4.TrnAcCode = txtTaxAccount.Text.Split(':')[0].ToString();
                tdDao4.TrnAcDesc = DataProcess.GetSingleValueFromtable(_connectionString, "budg", "Gl_Coa_Name", "where Gl_Coa_Code='" + tdDao4.TrnAcCode + "'");
                tdDao4.TrnAcType = DataProcess.GetSingleValueFromtable(_connectionString, "budg", "Gl_Coa_Type", "where Gl_Coa_Code='" + tdDao4.TrnAcCode + "'");
                tdDao4.TrnAmount = Convert.ToDouble(tax);
                tdDao4.TrnLineNo = lineno.ToString();
                tdDao4.TrnMatch = "";
                tdDao4.TrnNarration = "TAX";
                tdDao4.TrnPaymentDATE = DateProcess.GetServerDate(_connectionString);
                tdDao4.TrnRefNo = DaoHdr.TrnRefNo;
                tdDao4.TrnTrntype = "C";
                tdDao4.TrnDueDATE = tdDao2.TrnPaymentDATE.AddDays(30);
                tdDao4.TrnChequeNo = "";
                tdDao4.TrnDcNo = dcno.ToString();
                tdDao4.TrnGRNNo = grnno.ToString();
                tdDaolst.Add(tdDao4);
            }

            #endregion

            #region LC Expence to supplier liability

            foreach (GridViewRow gr in gdvExpense.Rows)
            {               
                string pono = lblPO.Text;
                string lcno = txtLcNumber.Text;
                string Mrrno = lblMRR.Text;
                string expid = gr.Cells[0].Text.ToString();
                string expname = gr.Cells[1].Text.ToString();
                double amt = Convert.ToDouble(gr.Cells[2].Text.ToString());
                string AccCode = gr.Cells[3].Text.ToString();
                string entryuser = Session[StaticData.sessionUserId].ToString();
                DateTime entrydate = System.DateTime.Now;
                if (ii == 0)
                    DataProcess.ExecuteQuery(_connectionString, SqlgenerateForFixedAsset.DeleteLcExpense(pono, Mrrno));

                DataProcess.ExecuteQuery(_connectionString, SqlgenerateForFixedAsset.InsertLcExpense(pono, lcno, Mrrno, expid, amt, AccCode, entryuser, entrydate));
                ii++;


                lineno++;
                TransactionDetailsDAO tdDao5 = new TransactionDetailsDAO();
                tdDao5.TrnAcCode = AccCode.ToString(); //DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetLcExpenseHead(expid)).Rows[0]["AccCode"].ToString();
                tdDao5.TrnAcDesc = DataProcess.GetSingleValueFromtable(_connectionString, "budg", "Gl_Coa_Name", "where Gl_Coa_Code='" + tdDao5.TrnAcCode + "'");
                tdDao5.TrnAcType = DataProcess.GetSingleValueFromtable(_connectionString, "budg", "Gl_Coa_Type", "where Gl_Coa_Code='" + tdDao5.TrnAcCode + "'");
                tdDao5.TrnAmount = Convert.ToDouble(amt);
                tdDao5.TrnLineNo = lineno.ToString();
                tdDao5.TrnMatch = "";
                tdDao5.TrnNarration = expname.ToString();
                tdDao5.TrnPaymentDATE = DateProcess.GetServerDate(_connectionString);
                tdDao5.TrnRefNo = DaoHdr.TrnRefNo.ToString();
                tdDao5.TrnTrntype = "C";
                tdDao5.TrnDueDATE = tdDao5.TrnPaymentDATE.AddDays(30);
                tdDao5.TrnChequeNo = "";
                tdDao5.TrnDcNo = dcno.ToString();
                tdDao5.TrnGRNNo = grnno.ToString();

                lccost = lccost + amt;
                
                tdDaolst.Add(tdDao5);

            }

            #endregion

            if (lccost > 0)
            {
                #region LC Expence add to item rate
                double expamt = 0;
                distratio = Math.Round((lccost / totamt), 2);
                dt = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.GetMrrDataByMrrNumberForFinalJournal(mrrref));

                foreach (DataRow dr in dt.Rows)
                {
                    lineno++;

                    TransactionDetailsDAO tdDao6 = new TransactionDetailsDAO();
                    tdDao6.TrnAcCode = dr["ItemAcc"].ToString();
                    tdDao6.TrnAcDesc = DataProcess.GetSingleValueFromtable(_connectionString, "budg", "Gl_Coa_Name", "where Gl_Coa_Code='" + tdDao6.TrnAcCode + "'");
                    tdDao6.TrnAcType = DataProcess.GetSingleValueFromtable(_connectionString, "budg", "Gl_Coa_Type", "where Gl_Coa_Code='" + tdDao6.TrnAcCode + "'");
                    tdDao6.TrnAmount = Math.Round(Convert.ToDouble(dr["Amount"].ToString()) * distratio, 2);
                    tdDao6.TrnLineNo = lineno.ToString();
                    tdDao6.TrnMatch = "";
                    tdDao6.TrnNarration = tdDao6.TrnAcDesc;
                    tdDao6.TrnPaymentDATE = DateProcess.GetServerDate(_connectionString);
                    tdDao6.TrnRefNo = "";
                    tdDao6.TrnTrntype = "D";
                    tdDao6.TrnDueDATE = tdDao6.TrnPaymentDATE.AddDays(30);
                    tdDao6.TrnChequeNo = "";
                    tdDao6.TrnDcNo = dr["PORef"].ToString();
                    tdDao6.TrnGRNNo = dr["MRR Number"].ToString();

                    expamt = expamt + tdDao6.TrnAmount;

                    if (expamt > lccost)
                        tdDao6.TrnAmount = tdDao6.TrnAmount - (expamt - lccost);


                    tdDaolst.Add(tdDao6);

                    
                }

                #endregion

                #region foreign gain loss
                if (expamt < lccost)
                {
                    lineno++;

                    TransactionDetailsDAO tdDao7 = new TransactionDetailsDAO();
                    tdDao7.TrnAcCode = "GaninLossAcc";
                    tdDao7.TrnAcDesc = DataProcess.GetSingleValueFromtable(_connectionString, "budg", "Gl_Coa_Name", "where Gl_Coa_Code='" + tdDao7.TrnAcCode + "'");
                    tdDao7.TrnAcType = DataProcess.GetSingleValueFromtable(_connectionString, "budg", "Gl_Coa_Type", "where Gl_Coa_Code='" + tdDao7.TrnAcCode + "'");
                    tdDao7.TrnAmount = Math.Round(Convert.ToDouble(expamt - lccost));
                    tdDao7.TrnLineNo = lineno.ToString();
                    tdDao7.TrnMatch = "";
                    tdDao7.TrnNarration = "";
                    tdDao7.TrnPaymentDATE = DateProcess.GetServerDate(_connectionString);
                    tdDao7.TrnRefNo = "";
                    tdDao7.TrnTrntype = "D";
                    tdDao7.TrnDueDATE = tdDao7.TrnPaymentDATE.AddDays(30);
                    tdDao7.TrnChequeNo = "";
                    tdDao7.TrnDcNo = dcno.ToString();
                    tdDao7.TrnGRNNo = grnno.ToString();

                    expamt = expamt + tdDao7.TrnAmount;

                    if (expamt > lccost)
                        tdDao7.TrnAmount = tdDao7.TrnAmount - (expamt - lccost);


                    tdDaolst.Add(tdDao7);
                                      
                }

                #endregion
            }


            TransactionEntryBLL tBll = new TransactionEntryBLL();

            string vatacc = txtVatAccount.Text.Split(':')[0].ToString();
            string taxacc = txtTaxAccount.Text.Split(':')[0].ToString();
            string invno = txtInvoiceNumber.Text;
            string str = tBll.JournalforReceiptInvoice(_connectionString, DaoHdr, tdDaolst, vatacc, taxacc,invno, false);

            //AttachFileSave(mrrref, poref);                                    

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
        catch (Exception ex)
        {
            myTran.Rollback();
        }
     }
     protected void txtAccHead_TextChanged(object sender, EventArgs e)
     {
         if (txtAccHead.Text != string.Empty)
         {
             txtAccHead.Text = txtAccHead.Text.Split(':')[0].Trim() == "" ? "" : txtAccHead.Text.Split(':')[0].Trim();
         }
     }

     private void ControllValidation(string etype)
     {
         if (etype == "Y")
         {
             txtVatAccount.Enabled = true;
             txtVatAmount.Enabled = true;
             txtTaxAccount.Enabled = true;
             txtTaxAmount.Enabled = true;
             txtInvoiceNumber.Enabled = true;
             txtLcNumber.Enabled = true;
             txtReceiptDate.Enabled = true;
             btnAdd.Visible = true;
             btnAdd.Enabled = true;
             gdvExpense.Enabled = true;
             ddlExpense.Enabled = true;
             txtExpAmt.Enabled = true;
             txtAccHead.Enabled = false;

         }
         else
         {
             txtVatAccount.Enabled = false;
             txtVatAmount.Enabled = false;
             txtTaxAccount.Enabled = false;
             txtTaxAmount.Enabled = false;
             txtInvoiceNumber.Enabled = false;
             txtLcNumber.Enabled = false;
             txtReceiptDate.Enabled = false;
             btnAdd.Visible = false;
             btnAdd.Enabled = false;
             gdvExpense.Enabled = false;
             ddlExpense.Enabled = false;
             txtExpAmt.Enabled = false;
             txtAccHead.Enabled = false;
         }
     }
}
