using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Payroll_frmPaySetup : System.Web.UI.Page
{
    private const string Rnode = "K";
    private double _totalValue = 0;
    string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        if (!Page.IsPostBack)
        {
            LoadCompanyByUserPermission("ADM", Rnode);
            Label4.Visible = false;
            txtValueForCode.Visible = false;
            ClsDropDownListController.EnableDisableDropDownList(ddlcompany);
            ddlcompany_SelectedIndexChanged(sender, e);
        }
    }

    private void LoadCompanyByUserPermission(string userid, string nodeid)
    {
        DataTable dt = new DataTable();
        ListItem lst;
        ddlcompany.Items.Clear();
        ddlcompany.Items.Add("");
        dt = AccessPermission.GetCompanyByUserandNodeid(ConnectionString, userid, nodeid);
        foreach (DataRow dr in dt.Rows)
        {
            lst = new ListItem();
            lst.Text = dr["COMP_CODE"].ToString() + ":" + dr["COMP_NAME"].ToString();
            lst.Value = dr["COMP_CODE"].ToString();
            ddlcompany.Items.Add(lst);
        }
    }

    protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        string dbname = ddlcompany.SelectedItem.Value;
        string constr = ConfigurationSettings.AppSettings["SCFConnectionString"].Replace("SCF", dbname);
        Session[GlobalData.sessionConnectionstring] = constr;
        Session["ConnectionStr"] = constr;
        txtEmpId_AutoCompleteExtender.ContextKey = Session[GlobalData.sessionConnectionstring].ToString();
        ClsDropDownListController.LoadddlStatus(ddlStatus);
    }

    protected void txtEmpId_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtEmpId.Text == "") return;
            lblEmpName.Text = txtEmpId.Text.Split(':')[1].Trim() == "" ? "" : txtEmpId.Text.Split(':')[1].Trim();
            txtEmpId.Text = txtEmpId.Text.Split(':')[0].Trim() == "" ? "" : txtEmpId.Text.Split(':')[0].Trim();
            ClsDropDownListController.LoadDropDownList(Session["ConnectionStr"].ToString(), Sqlgenerate.SqlGetGradeDescription(), ddlGrade, "Grade_Def_Desc", "Grade_Def_Code");
            DataTable dt = DataProcess.GetData(Session["ConnectionStr"].ToString(), Sqlgenerate.SqlGetEmployeeGrade(txtEmpId.Text));
            string sqlForFormula;
            ddlGrade.Enabled = true;
            if (dt.Rows.Count > 0)
            {
                ddlGrade.SelectedValue = dt.Rows[0][0].ToString();
                ddlGrade.Enabled = false;
                sqlForFormula = Sqlgenerate.SqlGetFormula(txtEmpId.Text);
            }
            else
            {
                sqlForFormula = Sqlgenerate.SqlGetFormula();
                ddlGrade.Enabled = true;
            }

            LoadFormulaDDL(sqlForFormula);
            PaySetUpFromhrms_grade_det(Session["ConnectionStr"].ToString(), txtEmpId.Text);
            LoadEmpForDet(txtEmpId.Text, ddlGrade.SelectedValue);
        }
        catch (IndexOutOfRangeException ex)
        {
            ScriptManager.RegisterStartupScript(
                   this,
                   GetType(),
                   "MessageBox",
                   "alert('Please Select Employee ID From Given List !');",
                   true);
        }
    }

    private void LoadFormulaDDL(string sqlString)
    {
        DataTable dtForFormula = DataProcess.GetData(Session["ConnectionStr"].ToString(), sqlString);
        ddlForFormula.Items.Clear();
        ddlForFormula.Items.Add("--please select--");
        foreach (DataRow dr in dtForFormula.Rows)
        {
            ListItem lst = new ListItem();
            lst.Value = dr["For_Mas_Cal_Code"].ToString();
            int ln = 6 - dr["For_Mas_Cal_Code"].ToString().ToUpper().Length;
            string sp = "";
            if (ln > 0)
            {
                for (int i = 0; i < ln; i++)
                {
                    sp += "&nbsp;&nbsp;";
                }
            }
            lst.Text = dr["For_Mas_Cal_Code"] + HttpUtility.HtmlDecode("" + sp + " : &nbsp;&nbsp;&nbsp; ") + dr["For_Mas_Cal_Name"].ToString().ToUpper();
            ddlForFormula.Items.Add(lst);
        }
    }

    protected void ddlForFormula_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlForFormula.SelectedItem.Text == "--please select--") return;
            lblFormulaName.Text = ddlForFormula.SelectedItem.Text.Split(':')[1].Trim() == "" ? "" : ddlForFormula.SelectedItem.Text.Split(':')[1].Trim();
            ddlForFormula.Text = ddlForFormula.SelectedItem.Text.Split(':')[0].Trim() == "" ? "" : ddlForFormula.SelectedItem.Text.Split(':')[0].Trim();
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    public void LoadEmpForDet(string empId, string grd)
    {
        DataTable dt = DataProcess.GetData(Session["ConnectionStr"].ToString(), Sqlgenerate.SqlGetEmployeeForDet(empId, grd));
        grdGetEmpForDet.DataSource = null;
        grdGetEmpForDet.DataBind();
        if (dt.Rows.Count > 0)
        {
            grdGetEmpForDet.DataSource = dt;
            grdGetEmpForDet.DataBind();
        }
    }

    private void InsertIntoLogTable(string detEmpId, string detCode, double detValue, string operationType)
    {
        DataProcess.InsertQuery(Session["ConnectionStr"].ToString(), Sqlgenerate.SqlInsertPaySetupLogTable(detEmpId, detCode, detValue, operationType, current.UserId.Trim().ToString()));
    }

    private string CheckValidation()
    {
        const string checkValidation = "";

        if (txtEmpId.Text == string.Empty)
        {
            txtEmpId.Focus();
            return "Must Enter Employee Code !";
        }
        return checkValidation;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckValidation();
        switch (validationMsg)
        {
            case "":
                {
                    SavePaysetupValue();
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }
    }

    private void SavePaysetupValue()
    {
        string detEmpId = txtEmpId.Text.Trim();
        string formulaCode = ddlForFormula.SelectedValue;
        string gradeCode = ddlGrade.SelectedValue;
        string formulaStatusValue = ddlStatus.SelectedValue;
        if (gradeCode != "-1" && formulaCode != "--please select--")
        {
            if (ddlGrade.Enabled)
            {
                AssignGrade();
            }
            string payValue = txtValueForCode.Text == string.Empty ? "0" : Convert.ToDouble(txtValueForCode.Text).ToString();

            if (CheckPaySetupData(detEmpId, formulaCode))
            {
                UpdateValue(detEmpId, formulaCode, payValue, formulaStatusValue);
            }
            else
            {
                InsertPayValue(detEmpId, formulaCode, Convert.ToDouble(payValue), formulaStatusValue);
            }

            CheckBaseAndUpdateDb(detEmpId, gradeCode, formulaCode, payValue.ToString(), formulaStatusValue);

            //if(formulaCode!="INCAMT")
            //    UpdateTaxAmount(detEmpId, formulaStatusValue);
           
            LoadEmpForDet(detEmpId, gradeCode);

        }
        else
        {
            MessageBox1.ShowWarning("Please assign grade or select Formula Code for   [" + detEmpId +
                                    " ]   Employee, Then pay setup  !");
        }

    }


    private void UpdateTaxAmount(string detEmpId, string formulaStatusValue)
    {        
        string taxamt = GetMonthlyTaxAmount(detEmpId).ToString();
        if (CheckPaySetupData(detEmpId, "INCAMT"))
        {
            UpdateValue(detEmpId, "INCAMT", taxamt, formulaStatusValue);
        }
        else
        {
            InsertPayValue(detEmpId, "INCAMT", Convert.ToDouble(taxamt), formulaStatusValue);
        }
        
 
    }

    private void CheckBaseAndUpdateDb(string detEmpId, string gradeCode, string formulaCode, string valuForPayTemp, string statusValue)
    {
        DataTable dtFormula = DataProcess.GetData(Session["ConnectionStr"].ToString(), Sqlgenerate.SqlGetFormulaDetailsByFormulaCode(formulaCode, gradeCode));

        foreach (DataRow rowNo in dtFormula.Rows)
        {
            string valuForPay = valuForPayTemp;
            string formulaCodeFormuladt = rowNo.ItemArray[0].ToString();
            string formulaBasedt = rowNo.ItemArray[1].ToString();
            string formulaMasMul = rowNo.ItemArray[2].ToString();
            valuForPay = (Convert.ToDouble(valuForPay) * Convert.ToDouble(formulaMasMul)).ToString();
            string st ="0";

            if (formulaCodeFormuladt == "PFEC" || formulaCodeFormuladt == "PFEMP")
                st = "0";
            else
                st = statusValue;


            if (CheckPaySetupData(detEmpId, formulaCodeFormuladt))
            {
                UpdateValue(detEmpId, formulaCodeFormuladt, valuForPay, statusValue);
            }
            else
            {
                InsertPayValue(detEmpId, formulaCodeFormuladt, Convert.ToDouble(valuForPay), st);
            }
        }
    }

    private double GetMonthlyTaxAmount(string detEmpId)
    {
        double taxamt = 0;
        DataTable dttax = DataProcess.GetData(Session["ConnectionStr"].ToString(), Sqlgenerate.Gettaxbyempid(detEmpId));
        if (dttax.Rows.Count > 0)
            taxamt = Convert.ToDouble(dttax.Rows[0]["Taxamt"].ToString());
        
        return taxamt;
       
    }


    private bool CheckPaySetupData(string detEmpId, string formulaCodeFormuladt)
    {
        string empIdForCheck = DataProcess.GetSingleValueFromtable(Session["ConnectionStr"].ToString(), "hrms_emp_for_det",
               "For_Det_Empid", "where For_Det_Empid='" + detEmpId + "' AND For_Det_ForCode='" + formulaCodeFormuladt + "'");

        if (empIdForCheck == "")
            return false;
        else
            return true;

    }

    private void InsertPayValue(string detEmpId, string detCode, double detValue, string givenStatus)
    {
        string detValueFlg = "N";
        int getDetLno = 0;
        if (chkboxForValue.Checked)
        {
            detValueFlg = "Y";
        }
        string getDetLnoString = @"SELECT det_lno  FROM hrms_grade_det b INNER JOIN hrms_emp_grd_det c ON b.Det_Code = c.det_grade WHERE c.det_empid = '" +
            detEmpId + "' and b.Det_Code='" + ddlGrade.SelectedValue + "' and b.Det_For='" + detCode + "'";
        DataTable dt = DataProcess.GetData(Session["ConnectionStr"].ToString(), getDetLnoString);
        if (dt.Rows.Count > 0)
        {
            getDetLno = Convert.ToInt32(dt.Rows[0][0].ToString());
        }
        using (SqlConnection conn = new SqlConnection(Session["ConnectionStr"].ToString()))
        {
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            try
            {
                string sqlString =
                    "INSERT INTO hrms_emp_for_det (For_Det_Empid,For_Det_ForCode,For_Det_Value,For_Det_ValFlg,for_det_sno,for_det_grd_flg,Show_flg,formulaStatus) VALUES('" +
                    detEmpId + "','" + detCode + "'," + detValue + ",'" + detValueFlg + "'," + getDetLno + ",'N','T','" + givenStatus + "')";
                DataProcess.InsertQuery(Session["ConnectionStr"].ToString(), sqlString);
                InsertIntoLogTable(detEmpId, detCode, detValue, "S");
                tran.Commit();
            }
            catch (SqlException)
            {
                tran.Rollback();
            }
            conn.Close();
        }
    }

    protected void grdGetEmpForDet_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void grdGetEmpForDet_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Delete"))
        {
            int con = Convert.ToInt32(e.CommandArgument.ToString());
            string detEmpId = ((Label)grdGetEmpForDet.Rows[con].FindControl("lblEmpCode")).Text;
            string detCode = ((Label)grdGetEmpForDet.Rows[con].FindControl("lblFormulaCode")).Text;
            double detValue = Convert.ToDouble(((Label)grdGetEmpForDet.Rows[con].FindControl("lblValue")).Text);
            using (SqlConnection conn = new SqlConnection(Session["ConnectionStr"].ToString()))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    string sqlQuery = "DELETE FROM hrms_emp_for_det where For_Det_Empid='" + detEmpId + "' AND For_Det_ForCode = '" + detCode + "'";
                    DataProcess.DeleteQuery(Session["ConnectionStr"].ToString(), sqlQuery);
                    InsertIntoLogTable(detEmpId, detCode, detValue, "D");
                    LoadEmpForDet(detEmpId, ddlGrade.SelectedValue);

                    if (detCode != "INCAMT")
                        UpdateTaxAmount(detEmpId, "1");

                    tran.Commit();
                }
                catch (SqlException sqlError)
                {
                    tran.Rollback();
                }
                conn.Close();
            }
        }
    }

    private void GetTotalOfValue(string payValue)
    {
        foreach (GridViewRow row in grdGetEmpForDet.Rows)
        {
            var pay = ((Label)row.FindControl("lblPay")).Text;
            if (pay != payValue) continue;
            double rowTotal = Convert.ToDouble(((Label)row.FindControl("lblValue")).Text);
            _totalValue = _totalValue + rowTotal;
        }
        grdGetEmpForDet.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
        grdGetEmpForDet.FooterRow.Cells[3].Text = "Total :";
        grdGetEmpForDet.FooterRow.Cells[4].Text = _totalValue.ToString("F");
    }

    protected void grdGetEmpForDet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int intex = 0;


        intex = GetColumnIndexByName(e.Row, "Det_val2");
              
        
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
        }

        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.Cells[intex].Text == "1")
        {
            var rowTotal = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "For_Det_Value"));
            _totalValue = _totalValue + rowTotal;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[3].Text = @"Total :";
            e.Row.Cells[4].Text = _totalValue.ToString("F");
        }



        e.Row.Cells[1].Visible = false;
        e.Row.Cells[6].Visible = false;
        e.Row.Cells[7].Visible = false;
        e.Row.Cells[intex].Visible = false;
        
    }

    int GetColumnIndexByName(GridViewRow row, string SearchColumnName)
    {
        int columnIndex = 0;
        foreach (DataControlFieldCell cell in row.Cells)
        {
            if (cell.ContainingField is BoundField)
            {
                if (((BoundField)cell.ContainingField).DataField.Equals(SearchColumnName))
                {
                    break;
                }
            }
            columnIndex++;
        }
        return columnIndex;
    }

    

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtEmpId.Text = string.Empty;
        lblEmpName.Text = "";
        ddlGrade.Items.Clear();
        ddlForFormula.Items.Clear();
        lblFormulaName.Text = "";
        chkboxForValue.Checked = false;
        Label4.Visible = false;
        txtValueForCode.Visible = false;
        txtValueForCode.Text = string.Empty;
        grdGetEmpForDet.DataSource = null;
        grdGetEmpForDet.DataBind();
        ddlGrade.Enabled = true;
    }

    protected void chkboxForValue_CheckedChanged(object sender, EventArgs e)
    {
        if (chkboxForValue.Checked == true)
        {
            Label4.Visible = true;
            txtValueForCode.Visible = true;
        }
        else
        {
            Label4.Visible = false;
            txtValueForCode.Visible = false;
            txtValueForCode.Text = "";
        }
    }

    protected void grdGetEmpForDet_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (txtEmpId.Text.Trim() != string.Empty)
        {
            grdGetEmpForDet.EditIndex = e.NewEditIndex;
            string detEmpId = txtEmpId.Text.Trim();
            LoadEmpForDet(detEmpId, ddlGrade.SelectedValue);
            ((TextBox)grdGetEmpForDet.Rows[grdGetEmpForDet.EditIndex].FindControl("txtValueForUpdate")).Focus();
        }
    }

    protected void grdGetEmpForDet_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdGetEmpForDet.EditIndex = -1;
        string detEmpId = txtEmpId.Text.Trim();
        LoadEmpForDet(detEmpId, ddlGrade.SelectedValue);
    }

    private void UpdateValue(string empId, string formulaCode, string valueForUpdate)
    {
        string sqlQuery = "UPDATE hrms_emp_for_det SET For_Det_Value = '" + valueForUpdate + "' , For_Det_ValFlg = 'Y' WHERE For_Det_Empid = '" + empId + "' AND For_Det_ForCode = '" + formulaCode + "'";
        DataProcess.UpdateQuery(Session["ConnectionStr"].ToString(), sqlQuery);
        InsertIntoLogTable(empId, formulaCode, Convert.ToDouble(valueForUpdate), "U");
    }

    private void UpdateValue(string empId, string formulaCode, string valueForUpdate, string formulaStatusValue)
    {
        string sqlQuery = "UPDATE hrms_emp_for_det SET For_Det_Value = '" + valueForUpdate + "' , For_Det_ValFlg = 'Y',formulaStatus = '" + formulaStatusValue + "' WHERE For_Det_Empid = '" + empId + "' AND For_Det_ForCode = '" + formulaCode + "'";
        DataProcess.UpdateQuery(Session["ConnectionStr"].ToString(), sqlQuery);
        InsertIntoLogTable(empId, formulaCode, Convert.ToDouble(valueForUpdate), "U");
    }

    protected void grdGetEmpForDet_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string empId = ((Label)grdGetEmpForDet.Rows[grdGetEmpForDet.EditIndex].FindControl("lblEmpCode")).Text;
        string formulaCode = ((Label)grdGetEmpForDet.Rows[grdGetEmpForDet.EditIndex].FindControl("lblFormulaCode")).Text;
        string statusValue = ((DropDownList)grdGetEmpForDet.Rows[grdGetEmpForDet.EditIndex].FindControl("ddlStatusForUpdate")).SelectedValue;
        string valueForUpdate =
            ((TextBox)grdGetEmpForDet.Rows[grdGetEmpForDet.EditIndex].FindControl("txtValueForUpdate")).Text ==
            string.Empty
                ? 0.ToString()
                : ((TextBox)grdGetEmpForDet.Rows[grdGetEmpForDet.EditIndex].FindControl("txtValueForUpdate")).Text;

        if (CheckPaySetupData(empId, formulaCode))
        {
            UpdateValue(empId, formulaCode, valueForUpdate, statusValue);
        }
        else
        {
            InsertPayValue(empId, formulaCode, Convert.ToDouble(valueForUpdate), statusValue);
        }

        CheckBaseAndUpdateDb(empId, ddlGrade.SelectedValue, formulaCode, valueForUpdate, statusValue);


        if (formulaCode != "INCAMT")
            UpdateTaxAmount(empId, statusValue);



        grdGetEmpForDet.EditIndex = -1;
        LoadEmpForDet(empId, ddlGrade.SelectedValue);
    }

    private void PaySetUpFromhrms_grade_det(string connectionString, string empId)
    {
        var cmd = new SqlCommand();
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(connectionString);
        sqlConn.Open();
        cmd.Connection = sqlConn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "paySetupInitiateFromhrms_grade_det";
        cmd.Parameters.Add(new SqlParameter("@For_Det_Empid", SqlDbType.NVarChar)).Value = empId;
        cmd.ExecuteNonQuery();
        sqlConn.Close();
    }

    private void AssignGrade()
    {
        if (ddlGrade.SelectedValue == "-1" || txtEmpId.Text == "")
            return;
        string detEmpId = txtEmpId.Text.Trim();
        LeaveProcess lvprocessobj = new LeaveProcess();
        lvprocessobj.EmpgrddetSave(Session[GlobalData.sessionConnectionstring].ToString(), detEmpId, ddlGrade.SelectedValue);
    }

    protected void ddlGrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadFormulaDDL(Sqlgenerate.SqlGetFormulaByGrade(ddlGrade.SelectedValue));
    }
}