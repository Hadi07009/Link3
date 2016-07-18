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
using LibraryDAL.SCBLDataSetTableAdapters;
using LibraryDAL.SCBL2DataSetTableAdapters;
using FreeTextBoxControls;
using System.IO;

public partial class frm_quotation_add : System.Web.UI.Page
{  

    protected void Page_Load(object sender, EventArgs e)
    {              
        tblmaster.BgColor = "FFFFFFF";

        if (!Page.IsPostBack)
        {            
            get_master_tac(ddlpayterms.SelectedValue.ToString());

            string ItemCode = Session[clsStatic.sessionSelvalforItmCode].ToString();
            
            LoadSupplier(ItemCode);

        }
        set_data_from_cmd(Session[clsStatic.sessionSelvalforQuo].ToString());
        string refno = Session[clsStatic.sessionSelvalforQuo].ToString().Split(':')[0].ToString();
        LoadUploadFileByRef(refno);

       
          
    }

    private void LoadSupplier(string ItemCode)
    {
        string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
        DataTable dt = new DataTable();
        string sql = @"select Par_Acc_Code,Par_Acc_Name from Inma_Purchase_Item_Mapping a" 
                      + " inner join PuMa_Par_Acc b on a.ParAccCode=b.Par_Acc_Code where ItemCode in " + ItemCode.ToString()  + " order by Par_Acc_Name";

        ddlSupplier.Items.Clear();
        ddlSupplier.Items.Add("---Select Supplier---");
        dt = DataProcess.GetData(_connectionString, sql);
        foreach (DataRow dr in dt.Rows)
        {
            ListItem lst = new ListItem();
            lst.Value = dr["Par_Acc_Code"].ToString();
            lst.Text = dr["Par_Acc_Code"].ToString() + ":" + dr["Par_Acc_Name"].ToString();
            ddlSupplier.Items.Add(lst);
        }

    }

    private void get_master_tac(string pay_type)
    {
        load_tandc_gen();
        load_tandc_spe();
        load_tandc_pay(pay_type);
    }

    private void load_tandc_gen()
    {
        tbl_tac_logTableAdapter log = new tbl_tac_logTableAdapter();
        SCBLDataSet.tbl_tac_logDataTable logdt = new SCBLDataSet.tbl_tac_logDataTable();
      
        tbl_tac_detTableAdapter tac = new tbl_tac_detTableAdapter();
        SCBLDataSet.tbl_tac_detDataTable dt = new SCBLDataSet.tbl_tac_detDataTable();
        dt = tac.GetDataByType("gen");
        FreeTextBox txt;
        CheckBox chk;

        gdgen.DataSource = dt;
        gdgen.DataBind();

        foreach (SCBLDataSet.tbl_tac_detRow dr in dt.Rows)
        {
            txt = new FreeTextBox();
            chk = new CheckBox();
            txt = (FreeTextBox)gdgen.Rows[Convert.ToInt16(dr.tac_seq_no) - 1].FindControl("TextBox1");
            chk = (CheckBox)gdgen.Rows[Convert.ToInt16(dr.tac_seq_no) - 1].FindControl("CheckBox1");
            txt.Text = dr.tac_det;
            chk.Text = Convert.ToInt16(dr.tac_seq_no).ToString();            
        }
    }

    private void load_tandc_spe()
    {
        tbl_tac_logTableAdapter log = new tbl_tac_logTableAdapter();
        SCBLDataSet.tbl_tac_logDataTable logdt = new SCBLDataSet.tbl_tac_logDataTable();

        tbl_tac_detTableAdapter tac = new tbl_tac_detTableAdapter();
        SCBLDataSet.tbl_tac_detDataTable dt = new SCBLDataSet.tbl_tac_detDataTable();
        dt = tac.GetDataByType("spe");
        FreeTextBox txt;
        CheckBox chk;

        gdspe.DataSource = dt;
        gdspe.DataBind();

        foreach (SCBLDataSet.tbl_tac_detRow dr in dt.Rows)
        {
            txt = new FreeTextBox();
            chk = new CheckBox();
            txt = (FreeTextBox)gdspe.Rows[Convert.ToInt16(dr.tac_seq_no) - 1].FindControl("TextBox2");
            chk = (CheckBox)gdspe.Rows[Convert.ToInt16(dr.tac_seq_no) - 1].FindControl("CheckBox2");
            txt.Text = dr.tac_det;
            chk.Text = Convert.ToInt16(dr.tac_seq_no).ToString();                        
        }


    }
    private void load_tandc_pay(string pay_type)
    {
        tbl_tac_logTableAdapter log = new tbl_tac_logTableAdapter();
        SCBLDataSet.tbl_tac_logDataTable logdt = new SCBLDataSet.tbl_tac_logDataTable();

        tbl_tac_detTableAdapter tac = new tbl_tac_detTableAdapter();
        SCBLDataSet.tbl_tac_detDataTable dt = new SCBLDataSet.tbl_tac_detDataTable();
        //dt = tac.GetDataByType2("pay", "com", pay_type);
        dt = tac.GetDataByTypeCat("pay",pay_type);


        FreeTextBox txt;
        CheckBox chk;
        int seq = 0;

        gdpay.DataSource = dt;
        gdpay.DataBind();

        foreach (SCBLDataSet.tbl_tac_detRow dr in dt.Rows)
        {
            txt = new FreeTextBox();
            chk = new CheckBox();
            txt = (FreeTextBox)gdpay.Rows[seq].FindControl("TextBox3");
            chk = (CheckBox)gdpay.Rows[seq].FindControl("CheckBox3");
            txt.Text = dr.tac_det;
            chk.Text = Convert.ToInt16(dr.tac_seq_no).ToString();
            seq++;
        }


    }

    //private string get_my_app()
    //{
    //    User_Role_DefinitionTableAdapter urole = new User_Role_DefinitionTableAdapter();
    //    SCBLDataSet.User_Role_DefinitionDataTable udt = new SCBLDataSet.User_Role_DefinitionDataTable();
    //    string my_app = "";
    //    udt = urole.GetRoleByUser(current.UserId.ToString());

    //    if (udt.Rows.Count > 0) my_app = udt[0].role_as.ToString();

    //    return my_app;
    //}

   

    private void set_data_from_cmd(string itm_list)
    {
        PuTr_IN_Det_Scbl2TableAdapter quot = new PuTr_IN_Det_Scbl2TableAdapter();
        SCBL2DataSet.PuTr_IN_Det_Scbl2Row drdet;
        quotation_detTableAdapter qdet = new quotation_detTableAdapter();
        SCBLDataSet.quotation_detDataTable dtqdet;

        string[] tmparr,tmp;        
        int cnt, i,dcnt;
        int tot = 0;
        string my_app = "TEN";   
        string ref_no,icode;

        tmparr = itm_list.Split('+');    

        cnt = tmparr.Length;

        celquo.Controls.Clear();

        for (i = 0; i < cnt; i++)
        {
            if (tmparr[i] != "")
            {                
                tot += 1;
                tmp = tmparr[i].Split(':');
                ref_no = tmp[0];
                icode = tmp[1];

                ClientSide_modules_commercial_usercontrols_ctl_quotation_entry ctl = (ClientSide_modules_commercial_usercontrols_ctl_quotation_entry)LoadControl("./usercontrols/ctl_quotation_entry.ascx");

                Label lblref = (Label)ctl.FindControl("lblref");
                Label lblreqtype = (Label)ctl.FindControl("lblreqtype");
                Label lblitmcode = (Label)ctl.FindControl("lblitmcode");
                Label lblsl = (Label)ctl.FindControl("lblsl");
                Label lblproduct = (Label)ctl.FindControl("lblproduct");
                Label lblqty = (Label)ctl.FindControl("lblqty");
                Label lbltk = (Label)ctl.FindControl("lbltk");
                TextBox txtspecification = (TextBox)ctl.FindControl("txtspecification");
                TextBox txtbrand = (TextBox)ctl.FindControl("txtbrand");
                TextBox txtorigin = (TextBox)ctl.FindControl("txtorigin");
                TextBox txtpacking = (TextBox)ctl.FindControl("txtpacking");
                Label Label1 = (Label)ctl.FindControl("Label1");
                DropDownList DropDownList1 = (DropDownList)ctl.FindControl("DropDownList1");

                ctl.ID = "ctl_entry" + celquo.Controls.Count.ToString();

                drdet = quot.GetDataByRefItem(ref_no, icode)[0];

                lblref.Text = ref_no;
                lblreqtype.Text = drdet.IN_Det_Code.ToString() + ", " + drdet.In_Det_Pur_Type.ToString();
                lblsl.Text = tot.ToString() + ".";
                lblitmcode.Text = drdet.IN_Det_Icode.ToString();
                lblproduct.Text = drdet.IN_Det_Itm_Desc;
                lblqty.Text = drdet.IN_Det_Lin_Qty + " " + drdet.IN_Det_Itm_Uom;
                lbltk.Text = "Tk. /" + drdet.IN_Det_Itm_Uom;

                txtspecification.Text = drdet.In_Det_Specification +" " + drdet.In_Det_Remarks;
                txtbrand.Text = drdet.In_Det_Brand;
                txtorigin.Text = drdet.In_Det_Origin;
                txtpacking.Text = drdet.In_Det_Packing;

                Label1.Visible = true;
                DropDownList1.Visible = true;

                DropDownList1.Items.Clear();
                dtqdet = new SCBLDataSet.quotation_detDataTable();
                dtqdet = qdet.GetDataByItem(drdet.IN_Det_Icode.ToString());

                dtqdet.DefaultView.Sort = dtqdet.entry_dateColumn.ColumnName + " DESC";

                dcnt = 0;
                foreach (SCBLDataSet.quotation_detRow qdr in dtqdet.Rows)
                {
                    dcnt++;
                    DropDownList1.Items.Add(qdr.rate.ToString("N2") + " [" + qdr.party_det.ToString() + "]");
                    if (dcnt > 50) break;
                }

                celquo.Controls.Add(ctl);
                
            }

        }

    }

    private void set_data_from_ddl(string logid, bool ftime)
    {
        PuTr_IN_Det_ScblTableAdapter quot = new PuTr_IN_Det_ScblTableAdapter();
        SCBLDataSet.PuTr_IN_Det_ScblRow drdet;

        quotation_logTableAdapter qlog = new quotation_logTableAdapter();
        SCBLDataSet.quotation_logRow logdr;
        logdr = qlog.GetDataById(logid)[0];
        string[] tmparr;
        string pcode;
        int cnt,i;
        int tot = 0;
        string my_app = "TEN";


        pcode = logdr.party_code;

        tmparr = logdr.item_code_det.Split(':');
        
        if (ftime)
            ddlSupplier.SelectedItem.Value = pcode; //txtparty.Text = pcode;


        cnt = tmparr.Length;

        celquo.Controls.Clear();

        for (i = 0; i < cnt; i++)
        {

            if (tmparr[i] != "")
            {
                if (quot.GetDataByItemStatus(tmparr[i], my_app).Rows.Count > 0)
                {
                    tot += 1;
                    ClientSide_modules_commercial_usercontrols_ctl_quotation_entry ctl = (ClientSide_modules_commercial_usercontrols_ctl_quotation_entry)LoadControl("./usercontrols/ctl_quotation_entry.ascx");

                    Label lblreqtype = (Label)ctl.FindControl("lblreqtype");
                    Label lblitmcode = (Label)ctl.FindControl("lblitmcode");
                    Label lblsl = (Label)ctl.FindControl("lblsl");
                    Label lblproduct = (Label)ctl.FindControl("lblproduct");
                    Label lblqty = (Label)ctl.FindControl("lblqty");
                    Label lbltk = (Label)ctl.FindControl("lbltk");

                    ctl.ID = "ctl_entry" + celquo.Controls.Count.ToString();

                    drdet = quot.GetDataByItemStatus(tmparr[i], my_app)[0];

                    lblreqtype.Text = drdet.IN_Det_Code.ToString() + ", " + drdet.In_Det_Pur_Type.ToString();
                    lblsl.Text = tot.ToString() + ".";
                    lblitmcode.Text = tmparr[i];
                    lblproduct.Text = drdet.IN_Det_Itm_Desc;
                    lblqty.Text = drdet.IN_Det_Lin_Qty + " " + drdet.IN_Det_Itm_Uom;
                    lbltk.Text = "Tk. /" + drdet.IN_Det_Itm_Uom;

                    celquo.Controls.Add(ctl);
                }
            }

        }

    }

    private void ddlchange()
    {
        ddlpayterms.SelectedValue.ToString();
        load_tandc_pay(ddlpayterms.SelectedValue.ToString());        
    }


    protected void ddlpayterms_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        ddlchange();        
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        PuTr_IN_Det_Scbl2TableAdapter srdet = new PuTr_IN_Det_Scbl2TableAdapter();

        tbl_tac_logTableAdapter taclog = new tbl_tac_logTableAdapter();
        quotation_detTableAdapter quo = new quotation_detTableAdapter();
        //quotation_logTableAdapter log = new quotation_logTableAdapter();

        int i, cnt, max_seq, len, indx;
        string tmp_str, pcode, pdet, uom, req_type, pur_type, tac_ref_no;
        string[] tmp;
        double qnty, max_ref;
        CheckBox chk;
        FreeTextBox txt; 

        ClientSide_modules_commercial_usercontrols_ctl_quotation_entry ctl;
        TextBox txtspecification, txtbrand, txtorigin, txtpacking, txtrate;
        Label lblref, lblitmcode, lblproduct, lblqty, lblreqtype;

        //check valid days entry
        if (txtvaliddays.Text == "") return;

        if (Convert.ToInt32(txtvaliddays.Text) < 1) return;

        //ckeck party

        //tmp_str = txtparty.Text;
        tmp_str = ddlSupplier.SelectedItem.Text;

        tmp = tmp_str.Split(':');

        if (tmp.Length < 2) return;

        // check pay terms entry

        indx = 0;
        foreach (GridViewRow gr in gdgen.Rows)
        {
            chk = new CheckBox();           
            chk = (CheckBox)gr.FindControl("CheckBox1");

            if (chk.Checked) indx++;
            
        }
        if (indx < 1) { lblmsg.Visible = true; return; }


        indx = 0;
        foreach (GridViewRow gr in gdpay.Rows)
        {
            chk = new CheckBox();           
            chk = (CheckBox)gr.FindControl("CheckBox3");

            if (chk.Checked) indx++;            
        }

        if (indx < 1) { lblmsg.Visible = true; return; }
        

        //Insert TAC
        max_ref = Convert.ToDouble(taclog.GetMaxRef()) + 1;
        tac_ref_no = "QEN-" + string.Format("{0:000000}", max_ref);

        indx = 0;
        foreach (GridViewRow gr in gdgen.Rows)
        {
            chk = new CheckBox();
            txt = new FreeTextBox();

            txt = (FreeTextBox)gr.FindControl("TextBox1");
            chk = (CheckBox)gr.FindControl("CheckBox1");
           
            if (chk.Checked)
            {
                indx++;
                taclog.Inserttac(tac_ref_no, "QEN", "gen", "", Convert.ToInt32(chk.Text), indx, txt.Text, Convert.ToInt32(txtvaliddays.Text), "");
            }
        }

        indx = 0;
        foreach (GridViewRow gr in gdspe.Rows)
        {
            chk = new CheckBox();
            txt = new FreeTextBox();

            txt = (FreeTextBox)gr.FindControl("TextBox2");
            chk = (CheckBox)gr.FindControl("CheckBox2");

            if (chk.Checked)
            {
                indx++;
                taclog.Inserttac(tac_ref_no, "QEN", "spe", "", Convert.ToInt32(chk.Text), indx, txt.Text, Convert.ToInt32(txtvaliddays.Text), "");
            }
        }

        indx = 0;
        foreach (GridViewRow gr in gdpay.Rows)
        {
            chk = new CheckBox();
            txt = new FreeTextBox();

            txt = (FreeTextBox)gr.FindControl("TextBox3");
            chk = (CheckBox)gr.FindControl("CheckBox3");

            if (chk.Checked)
            {
                indx++;
                taclog.Inserttac(tac_ref_no, "QEN", "pay", ddlpayterms.SelectedValue.ToString(), Convert.ToInt32(chk.Text), indx, txt.Text, Convert.ToInt32(txtvaliddays.Text), "");
            }
        }

      

        cnt = celquo.Controls.Count;
        
        for (i = 0; i < cnt; i++)
        {

            ctl = new ClientSide_modules_commercial_usercontrols_ctl_quotation_entry();
            ctl = (ClientSide_modules_commercial_usercontrols_ctl_quotation_entry)celquo.Controls[i];

            lblref = new Label();
            lblref = (Label)ctl.FindControl("lblref");

            lblreqtype = new Label();
            lblreqtype = (Label)ctl.FindControl("lblreqtype");

            lblitmcode = new Label();
            lblitmcode = (Label)ctl.FindControl("lblitmcode");

            lblqty = new Label();
            lblqty = (Label)ctl.FindControl("lblqty");

            lblproduct = new Label();
            lblproduct = (Label)ctl.FindControl("lblproduct");

            txtspecification = new TextBox();
            txtspecification = (TextBox)ctl.FindControl("txtspecification");

            txtbrand = new TextBox();
            txtbrand = (TextBox)ctl.FindControl("txtbrand");

            txtorigin = new TextBox();
            txtorigin = (TextBox)ctl.FindControl("txtorigin");

            txtpacking = new TextBox();
            txtpacking = (TextBox)ctl.FindControl("txtpacking");

            txtrate = new TextBox();
            txtrate = (TextBox)ctl.FindControl("txtrate");
                       
            tmp = lblreqtype.Text.Split(',');
            req_type = tmp[0].Trim();
            pur_type = tmp[1].Trim();

            tmp_str = ddlSupplier.SelectedItem.Text;
            tmp = tmp_str.Split(':');

            pcode = tmp[0];
            pdet = tmp[1].Trim();

            tmp_str = lblqty.Text;
            tmp = tmp_str.Split(' ');

            qnty = Convert.ToDouble(tmp[0]);
            uom = tmp[1];

            if (txtrate.Text != "")
            {
                if (srdet.GetDataByRefItem(lblref.Text, lblitmcode.Text)[0].In_Det_Status == "TEN")
                {
                    max_seq = Convert.ToInt32(quo.GetMaxSeq_No(lblitmcode.Text, "", lblref.Text)) + 1;

                    if (quo.ChkQuotationExistency("", lblitmcode.Text, pcode).Count == 0)
                    {
                        quo.InsertQuotation("", max_seq, lblref.Text, req_type, pur_type, lblitmcode.Text, lblproduct.Text, uom, qnty, pcode, pdet, Convert.ToDecimal(txtrate.Text), 0, txtspecification.Text, txtbrand.Text, txtorigin.Text, txtpacking.Text, DateTime.Now, DateTime.Now, tac_ref_no, tac_ref_no, tac_ref_no, "", "", "");
                    }
                }
            }

        }

        
        string refno = Session[clsStatic.sessionSelvalforQuo].ToString().Split(':')[0].ToString();
        string qn = tac_ref_no.ToString();
        AttachFileSave(qn, refno);
        

        Response.Redirect("./frm_quotation_entry.aspx");
    }  

    protected void gdgen_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            ((CheckBox)e.Row.FindControl("CheckBox1")).Attributes.Add("onclick", "javascript:SelectAll('" + ((CheckBox)e.Row.FindControl("CheckBox1")).ClientID + "','1')");
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((CheckBox)e.Row.FindControl("CheckBox1")).Attributes.Add("onClick", "ColorRow(this)");

        }


    }

    protected void gdspe_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Find the checkbox control in header and add an attribute
            ((CheckBox)e.Row.FindControl("CheckBox2")).Attributes.Add("onclick", "javascript:SelectAll('" + ((CheckBox)e.Row.FindControl("CheckBox2")).ClientID + "','2')");
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((CheckBox)e.Row.FindControl("CheckBox2")).Attributes.Add("onClick", "ColorRow(this)");

        }
    }

    protected void gdpay_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Find the checkbox control in header and add an attribute
            ((CheckBox)e.Row.FindControl("CheckBox3")).Attributes.Add("onclick", "javascript:SelectAll('" + ((CheckBox)e.Row.FindControl("CheckBox3")).ClientID + "','3')");
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((CheckBox)e.Row.FindControl("CheckBox3")).Attributes.Add("onClick", "ColorRow(this)");

        }
    }

    private void AttachFileSave(string QuotationNo, string refno)
    {
        HttpFileCollection hfc = Request.Files;

        string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
        string referenceno = QuotationNo.ToString();
        string userid = Session[StaticData.sessionUserId].ToString();

        for (int i = 0; i < hfc.Count; i++)
        {
            HttpPostedFile hpf = hfc[i];

            if (hpf.ContentLength > 0)
            {
                string rowfilename = System.IO.Path.GetFileName(hpf.FileName).Replace("'", "''");
                string filename = referenceno + "-" + System.IO.Path.GetFileName(hpf.FileName).Replace("'", "''");
                hpf.SaveAs(Server.MapPath("~/AttachMentfile/") + "\\" + filename);

                string attach = @"insert into [ProcessFileUpload]([ReferenceNo],[FileName],[SavedFileName],[SerialNo],[UploadDate],[UploadBy],[Status],[pReferenceNo])values('" + referenceno + "','" + rowfilename + "','" + filename + "'," + (i + 1) + ",Convert(Datetime,'" + System.DateTime.Now.Date.ToString() + "',103),'" + userid.ToString() + "','Y','" + refno + "')";

                DataProcess.InsertQuery(_connectionString, attach);

            }

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
    private void LoadUploadFileByRef(string refno)
    {
        string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
        DataTable dt = new DataTable();
        dt = DataProcess.GetData(_connectionString, "select [ReferenceNo],[FileName],[SavedFileName] from [ProcessFileUpload] where pReferenceNo='" + refno.ToString() + "' order by [ReferenceNo],[SerialNo]");
        gdvFileLoad.DataSource = dt;
        gdvFileLoad.DataBind();
    }
    protected void gdvFileLoad_SelectedIndexChanged(object sender, EventArgs e)
    {
        int indx = gdvFileLoad.SelectedIndex;
        string gg = gdvFileLoad.Rows[indx].Cells[2].Text.Trim();
        String F1Path, F1Name;
        string abc = Server.MapPath("~/AttachMentfile/") + gg.ToString().Replace("&amp;","&");
        F1Path = abc.ToString();
        F1Name = Path.GetFileName(F1Path);
        GetFile(F1Path, F1Name);
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
}
