using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibraryDAL;
using Link3FrameWork;
using System.Data;
using System.Data.SqlClient;


public partial class modules_FixedAsset_Setup_CustomerAccount : System.Web.UI.Page
{
    string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            GetMaxCodeNumber();
        }
        txtSearch_AutoCompleteExtender.ContextKey = _connectionString.ToString();
        autoComplete1.ContextKey = _connectionString.ToString();
        AutoCompleteExtender1.ContextKey = _connectionString.ToString();
        AutoCompleteExtender3.ContextKey = _connectionString.ToString();
        AutoCompleteExtender4.ContextKey = _connectionString.ToString();
        AutoCompleteExtender2.ContextKey = _connectionString.ToString();
    }

    private void GetMaxCodeNumber()
    {
        var storedProcedureCommandTest = " EXEC CustomerAccountGetMaxFrom_SaMa_Par_Acc ";
        var dtCustomerCode = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureCommandTest);
        if (dtCustomerCode.Rows.Count > 0)
        {
            txtcode.Text = dtCustomerCode.Rows[0][0].ToString();
        }
    }

    protected void chkBalanceDuringRequired_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void chkaccountgroup_CheckedChanged(object sender, EventArgs e)
    {
        if (chkaccountgroup.Checked == true)
        {
            pnlAccountGroup.Visible = true;
        }
        else
        {
            pnlAccountGroup.Visible = false;
        }
    }
    protected void chkanalysis_CheckedChanged(object sender, EventArgs e)
    {
        if (chkanalysis.Checked == true)
        {
            pnlAnalysis.Visible = true;
        }
        else
        {
            pnlAnalysis.Visible = false;
        }
    }

    protected void txtcode_TextChanged1(object sender, EventArgs e)
    {
        string code = "";
        if (txtcode.Text.Length > 12)
        {
            string[] strseperator = { "::" };
            string[] mgrtarain = { "" };
            mgrtarain = txtcode.Text.Split(strseperator, StringSplitOptions.None);
            code = mgrtarain[0].Trim();
            getDataByCoaCode(code);
        }
        else
        {
            code = txtcode.Text;
        }
    }

    private void getDataByCoaCode(string coacode)
    {
        if (coacode == "") return;
        DataTable dt = DataProcess.GetData(_connectionString, "Select * from sama_par_acc where par_acc_code='" + coacode + "'");
        if (dt.Rows.Count > 0)
        {
            btnAdd.Text = "Modify";
            btnDelete.Enabled = true;
            txtcode.Text = dt.Rows[0]["par_acc_code"].ToString();
            txtsecondarycode.Text = dt.Rows[0]["Par_Acc_Sec_Code"].ToString();
            txtname.Text = dt.Rows[0]["Par_Acc_Name"].ToString();
            txtreletedto.Text = dt.Rows[0]["T_C2"].ToString();
            txtlasttranaction.Text = dt.Rows[0]["Par_Acc_Trn_DATE"].ToString();
            txtcomment.Text = dt.Rows[0]["Par_Acc_Comm"].ToString();
            txtcurrencycode.Text = dt.Rows[0]["Par_Acc_Cur_Code"].ToString();
            txtcheckClearingAccount.Text = dt.Rows[0]["T_C1"].ToString();

            if (dt.Rows[0]["Par_Acc_Sta"].ToString() == "Y")
            {
                chkStatus.Checked = true; chkStatus.Text = "Suspended";
            }
            else
            {
                chkStatus.Checked = false; chkStatus.Text = "Not Suspended";
            }

            #region Get Group Name
            txtlevel1.Text = "";
            txtlevel2.Text = "";
            txtlevel3.Text = "";
            txtfirstgroup.Text = "";
            txtsecondgroup.Text = "";
            txtthirdgroup.Text = "";
            DataTable dt1 = DataProcess.GetData(_connectionString, "SELECT Par_Grp_Acc_Code,Par_Grp_Id,Par_Grp_Code FROM [SaMa_Par_Acc_Grp] where Par_Grp_Acc_Code='" + coacode + "'");

            foreach (DataRow dr in dt1.Rows)
            {

                if (dr["Par_Grp_Id"].ToString() == "A1")
                {
                    COACls.getGrpName(dr["Par_Grp_Id"].ToString(), dr["Par_Grp_Code"].ToString(), txtfirstgroup);
                }
                else if (dr["Par_Grp_Id"].ToString() == "A2")
                {
                    COACls.getGrpName(dr["Par_Grp_Id"].ToString(), dr["Par_Grp_Code"].ToString(), txtsecondgroup);
                }
                else if (dr["Par_Grp_Id"].ToString() == "A3")
                {
                    COACls.getGrpName(dr["Par_Grp_Id"].ToString(), dr["Par_Grp_Code"].ToString(), txtthirdgroup);
                }
            }
            #endregion
            AnalysisDataByCoaCode(coacode);
            GetCustomerAddressInformation(coacode);
        }
        else
        {
            btnAdd.Text = "Add";
            btnDelete.Enabled = false;
            txtsecondarycode.Text = txtcode.Text;
            txtname.Focus();
            txtlasttranaction.Text = DateTime.Now.Date.ToShortDateString();
        }
        btnExit.Text = "Cancel";
        txtcode.Enabled = false;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(txtcode.Text) == "")
        {
            lblNotification.Text = "Please Enter Code";
            lblNotification.ForeColor = System.Drawing.Color.Red;
            txtcode.Focus();
            return;

        }
        if (Convert.ToString(txtname.Text) == "")
        {
            lblNotification.Text = "Please Enter Name";
            lblNotification.ForeColor = System.Drawing.Color.Red;
            txtname.Focus();
            return;

        }
        string chkst = "";
        if (chkanalysis.Checked == true)
        {
            chkst = "Y";
        }
        else
        {
            chkst = "N";
        }
        List<CoaAccGroupDAO> grpName = COACls.CoaGroupName(txtcode.Text.ToString(), txtfirstgroup, txtsecondgroup, txtthirdgroup, true);
        List<AccCoaAnalysis> Analysis = COACls.CoaAnalysis(txtcode.Text.ToString(), txtlevel1, txtlevel2, txtlevel3);

        SamaParAccDAO sama = new SamaParAccDAO();
        sama.ParAccCode = txtcode.Text;
        sama.ParAccName = txtname.Text;
        sama.ParAccSecCode = txtsecondarycode.Text;
        if (chkStatus.Checked == true)
        {
            sama.ParAccSta = "S";
        }
        else
        {
            sama.ParAccSta = "";
        }
        sama.ParAccComm = txtcomment.Text;

        if (chkBalanceDuringRequired.Checked == true)
        {
            sama.ParAccBalTeReq = "Y";
        }
        else
        {
            sama.ParAccBalTeReq = "N";
        }
        sama.TC2 = txtreletedto.Text;
        if (Convert.ToString(txtpermission.Text) == "") txtpermission.Text = "0";
        sama.ParAccPerm = Convert.ToInt32(txtpermission.Text);
        sama.ParAccCurCode = txtcurrencycode.Text;
        sama.TC1 = txtcheckClearingAccount.Text;


        bool updateglg = false; ;

        sama.ParAccName = Convert.ToString(txtname.Text);
        sama.ParAccComm = Convert.ToString(txtcomment.Text);

        SamaParAdrDAO objSamaParAdrDAO = new SamaParAdrDAO();
        objSamaParAdrDAO.ParAdrLine1 = txtAddressLine1.Text == string.Empty ? null : txtAddressLine1.Text;
        objSamaParAdrDAO.ParAdrLine2 = txtAddressLine2.Text == string.Empty ? null : txtAddressLine2.Text;
        objSamaParAdrDAO.ParAdrLine3 = txtAddressLine3.Text == string.Empty ? null : txtAddressLine3.Text;
        objSamaParAdrDAO.ParAdrLine4 = txtCity.Text == string.Empty ? null : txtCity.Text;
        objSamaParAdrDAO.ParAdrLine5 = txtZone.Text == string.Empty ? null : txtZone.Text;
        objSamaParAdrDAO.ParAdrFaxNo = txtFax.Text == string.Empty ? null : txtFax.Text;
        objSamaParAdrDAO.ParAdrEmailId = txtEmail.Text == string.Empty ? null : txtEmail.Text;
        objSamaParAdrDAO.ParAdrCntNo = txtContactPerson.Text == string.Empty ? null : txtContactPerson.Text;
        objSamaParAdrDAO.ParAdrTelNo = txtTelephone.Text == string.Empty ? null : txtTelephone.Text;
        objSamaParAdrDAO.ParAdrLstNo = txtMobileNumber.Text == string.Empty ? null : txtMobileNumber.Text;
        objSamaParAdrDAO.ParAdrCmt = txtVATRegNo.Text == string.Empty ? null : txtVATRegNo.Text;
        objSamaParAdrDAO.ParAdrCstNo = txtTIN.Text == string.Empty ? null : txtTIN.Text;
        
        
        if (btnAdd.Text == "Modify") updateglg = true;

        CoaBLL coabll = new CoaBLL();
        if (coabll.InsertCustomerData(DBConnCls.ConnectionStringL3T, sama, grpName, updateglg, Analysis, chkst))
        {
            DataProcess.ExecuteQuery(DBConnCls.ConnectionStringL3T, "exec AssignCoagrpByAccExtended '" + sama.ParAccCode.ToString() + "'");
            lblNotification.Text = StringProcess.savedMessage; txtclear();
            lblNotification.ForeColor = System.Drawing.Color.Green;

            var storedProcedureComandTest = "exec [CustomerAccountInitiateInto_SaMa_Par_Adr] '" + sama.ParAccCode + "','" + sama.ParAccName + "','" + objSamaParAdrDAO.ParAdrLine1 + "'"
                + " ,'" + objSamaParAdrDAO.ParAdrLine2 + "','" + objSamaParAdrDAO.ParAdrLine3 + "','" + objSamaParAdrDAO.ParAdrLine4 + "','" + objSamaParAdrDAO.ParAdrLine5 + "'"
                + " ,'" + objSamaParAdrDAO.ParAdrCstNo + "','" + objSamaParAdrDAO.ParAdrLstNo + "','" + objSamaParAdrDAO.ParAdrCntNo + "','" + objSamaParAdrDAO.ParAdrTelNo + "'"
                + ",'" + objSamaParAdrDAO.ParAdrFaxNo + "','" + objSamaParAdrDAO.ParAdrEmailId + "','" + objSamaParAdrDAO.ParAdrCmt + "','" + DateProcess.GetServerDate(_connectionString) + "'";
            StoredProcedureExecutor.StoredProcedureExecuteNonQuery(_connectionString, storedProcedureComandTest);
            GetMaxCodeNumber();
        }
        else
        {
            lblNotification.Text = StringProcess.unsavedMessage;
            lblNotification.ForeColor = System.Drawing.Color.Red;
        }
    }

    private void txtclear()
    {
        txtcode.Text = "";
        txtname.Text = "";
        txtsecondarycode.Text = "";
        txtcomment.Text = "";
        txtcurrencycode.Text = "";
        txtreletedto.Text = "";
        txtpermission.Text = "";
        txtlasttranaction.Text = "";
        txtaccountbalance.Text = "";
        txtcheckClearingAccount.Text = "";
        txtlevel1.Text = "";
        txtlevel2.Text = "";
        txtlevel3.Text = "";
        btnAdd.Text = "Add";
        chkStatus.Checked = false;
        chkBalanceDuringRequired.Checked = false;
        ClearCustomerAddressInformation();
    }

    private void ClearCustomerAddressInformation()
    {
        txtAddressLine1.Text = string.Empty;
        txtAddressLine2.Text = string.Empty;
        txtAddressLine3.Text = string.Empty;
        txtCity.Text = string.Empty;
        txtZone.Text = string.Empty;
        txtFax.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtContactPerson.Text = string.Empty;
        txtTelephone.Text = string.Empty;
        txtMobileNumber.Text = string.Empty;
        txtVATRegNo.Text = string.Empty;
        txtTIN.Text = string.Empty;
    }

    protected void txtcheckClearingAccount_TextChanged(object sender, EventArgs e)
    {
        if (txtcheckClearingAccount.Text != "")
        {
            string[] strseperator = { "::" };
            string[] mgrtarain = { "" };
            mgrtarain = txtcheckClearingAccount.Text.Split(strseperator, StringSplitOptions.None);
            txtcheckClearingAccount.Text = mgrtarain[0].Trim();
        }
    }

    private void AnalysisDataByCoaCode(string coacode)
    {
        DataTable dt = DataProcess.GetData(_connectionString, "select * from AccCoaAnalysis where Gl_coa_code = '" + coacode + "' order by LinNo");
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["LinNo"].ToString() == "1")
            {
                txtlevel1.Text = dr["Cost_ID"].ToString() + "::" + dr["Cost_Name"].ToString();
            }
            else if (dr["LinNo"].ToString() == "2")
            {
                txtlevel2.Text = dr["Cost_ID"].ToString() + "::" + dr["Cost_Name"].ToString();
            }
            else if (dr["LinNo"].ToString() == "3")
            {
                txtlevel3.Text = dr["Cost_ID"].ToString() + "::" + dr["Cost_Name"].ToString();
            }
        }
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        txtclear();
        GetMaxCodeNumber();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //if (Convert.ToString(txtcode.Text) == "") return;
        //DataTable dt = DataProcess.GetData(DBConnCls.ConnectionString, "select * from SaMa_Par_Acc where Par_Acc_Code='" + Convert.ToString(txtcode.Text) + "' and Par_Acc_Trn_Flag ='Y'");

        //if (dt.Rows.Count > 0)
        //{
        //    lblNotification.Text= "This Customer Account Can't be Deleted\rAlready have transaction";
        //    lblNotification.ForeColor = System.Drawing.Color.Red;
        //    return;
        //}
        //else
        //{
        //    CoaBLL cbl = new CoaBLL();
        //    if (cbl.DeleteCustomerData(DBConnCls.ConnectionString, Convert.ToString(txtcode.Text)))
        //    {
        //        lblNotification.Text=StringProcess.deletedMessage;
        //        lblNotification.ForeColor = System.Drawing.Color.Green;
        //        txtclear();
        //    }
        //}
    }
    

    private void GetCustomerAddressInformation(string parAdrCode)
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [CustomerAccountGetFrom_SaMa_Par_Adr] '"+parAdrCode+"'";
            var dtAddressInformation = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureCommandTest);
            ClearCustomerAddressInformation();
            if (dtAddressInformation.Rows.Count > 0)
            {
                txtAddressLine1.Text = dtAddressInformation.Rows[0]["Par_Adr_Line1"].ToString();
                txtAddressLine2.Text = dtAddressInformation.Rows[0]["Par_Adr_Line2"].ToString();
                txtAddressLine3.Text = dtAddressInformation.Rows[0]["Par_Adr_Line3"].ToString();
                txtCity.Text = dtAddressInformation.Rows[0]["Par_Adr_Line4"].ToString();
                txtZone.Text = dtAddressInformation.Rows[0]["Par_Adr_Line5"].ToString();
                txtFax.Text = dtAddressInformation.Rows[0]["Par_Adr_Fax_No"].ToString();
                txtEmail.Text = dtAddressInformation.Rows[0]["Par_Adr_Email_Id"].ToString();
                txtContactPerson.Text = dtAddressInformation.Rows[0]["Par_Adr_Cnt_No"].ToString();
                txtTelephone.Text = dtAddressInformation.Rows[0]["Par_Adr_Tel_No"].ToString();
                txtMobileNumber.Text = dtAddressInformation.Rows[0]["Par_Adr_Lst_No"].ToString();
                txtVATRegNo.Text = dtAddressInformation.Rows[0]["Par_Adr_Cmt"].ToString();
                txtTIN.Text = dtAddressInformation.Rows[0]["Par_Adr_Cst_No"].ToString();
            }
        }
        catch (SqlException sqlError)
        {
            _msg = " Error Occured During Load Data From Database, Data did not Loaded from Database ! ";

        }
        catch (Exception inSystemExep)
        {
            _msg = " Error Occured, Data did not Loaded from Database  !";

        }
        finally
        {
            if (_msg != null)
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + _msg + " ');",
                    true);
            }

        }
    }
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        if (txtSearch .Text != string.Empty)
        {
            txtclear();
            txtcode.Text = txtSearch.Text.Split(':')[0].Trim() == "" ? "" : txtSearch.Text.Split(':')[0].Trim();
            getDataByCoaCode(txtcode.Text.ToString());
        }

    }
}
