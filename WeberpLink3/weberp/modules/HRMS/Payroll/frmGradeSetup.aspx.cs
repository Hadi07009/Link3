using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Payroll_frmGradeSetup : System.Web.UI.Page
{
    private const string Rnode = "K";
    string ConnectionStr = ConfigurationSettings.AppSettings["SCFConnectionString"].Replace("SCF", "SSP");
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        if (!Page.IsPostBack)
        {
            LoadGradeDescription();
            ClsDropDownListController.LoadDropDownList(ConnectionStr, Sqlgenerate.SqlGetGradeDescription(), ddlGrade, "Grade_Def_Desc", "Grade_Def_Code");
            LoadFormulaDescription();
            txtSelectionValue.Visible = false;
        }
    }

    private string SaveGradeDescription()
    {
        GradeSetup objGradeSetup = new GradeSetup();
        objGradeSetup.GradeCode = lblGradeCodeForUpdate.Text == string.Empty ? null : lblGradeCodeForUpdate.Text;
        objGradeSetup.GradeDescription = txtGradeDefination.Text;
        objGradeSetup.TxtTag = btnSaveGradeDefination.Text;
        if (objGradeSetup.TxtTag == "Save")
        {
            return Save(ConnectionStr, objGradeSetup);
        }
        else
        {
            return Update(ConnectionStr, objGradeSetup);
        }
    }

    public string Save(string connectionString, GradeSetup objGradeSetup)
    {
        string _msg;
        try
        {
            var dtGradeDescription = DataProcess.GetData(connectionString, Sqlgenerate.SqlSearchGradeValuebyCode(objGradeSetup.GradeDescription));
            if (dtGradeDescription.Rows.Count == 0)
            {
                var storedProcedureComandTest = "exec [GradeInitiateInto_hrms_grade_def] '" + objGradeSetup.GradeCode + "','" + objGradeSetup.GradeDescription + "','" + objGradeSetup.TxtTag + "'";
                StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);
                _msg = "Data Saved Successfully ";
                ClearAllControlForFirstTab();
                LoadGradeDescription();
                ClsDropDownListController.LoadDropDownList(ConnectionStr, Sqlgenerate.SqlGetGradeDescription(), ddlGrade, "Grade_Def_Desc", "Grade_Def_Code");
            }
            else if (dtGradeDescription.Rows.Count > 0)
            {
                _msg = "This Grade Description Already Exsit !";
            }
            else
            {
                ClearAllControlForFirstTab();
                LoadGradeDescription();
                _msg = " Please try again !";
            }
        }
        catch (SqlException sqlError)
        {
            _msg = "Error Occured During Operation into Database, Data did not Save into Database !";
        }
        catch (Exception inSystemExep)
        {
            _msg = " Error Occured, Data did not Save into Database !";
        }
        return _msg;
    }

    public string Update(string connectionString, GradeSetup objGradeSetup)
    {
        string _msg;
        try
        {
            var dtGradeDescription = DataProcess.GetData(connectionString, Sqlgenerate.SqlSearchGradeValuebyCode(objGradeSetup.GradeCode));
            if (dtGradeDescription.Rows.Count == 1)
            {
                var storedProcedureComandTest = "exec [GradeInitiateInto_hrms_grade_def] '" + objGradeSetup.GradeCode + "','" + objGradeSetup.GradeDescription + "','" + objGradeSetup.TxtTag + "'";
                StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);
                _msg = "Data Saved Successfully ";
                ClearAllControlForFirstTab();
                LoadGradeDescription();
                ClsDropDownListController.LoadDropDownList(ConnectionStr, Sqlgenerate.SqlGetGradeDescription(), ddlGrade, "Grade_Def_Desc", "Grade_Def_Code");
            }
            else if (dtGradeDescription.Rows.Count == 0)
            {
                btnSaveGradeDefination.Text = "Save";
                _msg = "This Grade Description did not found ! So, Please Save Now.";
            }
            else
            {
                ClearAllControlForFirstTab();
                LoadGradeDescription();
                _msg = " Please try again !";
            }
        }
        catch (SqlException sqlError)
        {
            _msg = "Error Occured During Operation into Database, Data did not Save into Database !";
        }
        catch (Exception inSystemExep)
        {
            _msg = " Error Occured, Data did not Save into Database !";
        }
        return _msg;
    }

    private string CheckAllValidation()
    {
        const string checkValidation = "";
        if (txtGradeDefination.Text == string.Empty)
        {
            txtGradeDefination.Focus();
            return "Please Enter Grade Description Correctly !";
        }
        return checkValidation;
    }

    private string CheckAllValidationForSecondTab()
    {
        const string checkValidation = "";
        if (ddlGrade.SelectedValue == "-1")
        {
            ddlGrade.Focus();
            return "Please Select Grade Correctly !";
        }
        if (ddlFormula.SelectedValue == "-1")
        {
            ddlFormula.Focus();
            return "Please Select Formula Correctly !";
        }
        //string sqlQueryString = "SELECT * FROM hrms_grade_det WHERE Det_Code = '" + ddlGrade.SelectedValue + "' AND Det_For = '" + ddlFormula.SelectedValue + "'";
        //var dtForCheck = DataProcess.GetData(ConnectionStr, sqlQueryString);
        //if (dtForCheck.Rows.Count != 0)
        //{
        //    ddlGrade.Focus();
        //    ddlFormula.Focus();
        //    return "This Grade and Formula Already Exist  !";
        //}
        if (rblSelectionCriteria.SelectedValue == "C")
        {
            if (ddlConditionFormula1.SelectedValue == "-1")
            {
                ddlConditionFormula1.Focus();
                return "Please Select Formula Correctly !";
            }
            if (ddlConditionOperator1.SelectedValue == "-1")
            {
                ddlConditionOperator1.Focus();
                return "Please Select Conditional Operator Correctly !";
            }
            if (ddlConditionFormula2.SelectedValue == "-1")
            {
                ddlConditionFormula2.Focus();
                return "Please Select Formula Correctly !";
            }
            if (ddlConditionOperator2.SelectedValue == "-1")
            {
                ddlConditionOperator2.Focus();
                return "Please Select Conditional Operator Correctly !";
            }
            if (ddlOutcomeFormula.SelectedValue == "-1")
            {
                ddlOutcomeFormula.Focus();
                return "Please Select Outcome Formula Correctly !";
            }
            if (ddlOutcomeOperator.SelectedValue == "-1")
            {
                ddlOutcomeOperator.Focus();
                return "Please Select Outcome Operator Correctly !";
            }
            if (ddlOutcomeFormula2.SelectedValue == "-1")
            {
                ddlOutcomeFormula2.Focus();
                return "Please Select Outcome Formula Correctly !";
            }
        }
        if (rblSelectionCriteria.SelectedValue == "V")
        {
            if (txtSelectionValue.Text == string.Empty)
            {
                txtSelectionValue.Focus();
                return "Please Enter Value Correctly !";
            }
        }
        if (btnAdd.Text == @"Add")
        {
            foreach (GridViewRow dataRow in grdGetSelectedValue.Rows)
            {
                string lblFormulaValue = ((Label)grdGetSelectedValue.Rows[dataRow.RowIndex].FindControl("lblFormulaValue")).Text;
                if (ddlFormula.SelectedValue == lblFormulaValue)
                {
                    return "This Formula Already Exsit !";
                }
            }
        }
        return checkValidation;
    }

    private void ClearAllControlForFirstTab()
    {
        txtGradeDefination.Text = string.Empty;
        lblGradeCodeForUpdate.Text = string.Empty;
        btnSaveGradeDefination.Text = "Save";
    }

    private void ClearAllControlForSecondTab()
    {
        txtSelectionValue.Text = string.Empty;
        ddlConditionFormula1.SelectedValue = "-1";
        ddlConditionOperator1.SelectedValue = "-1";
        txtConditionalValue1.Text = string.Empty;
        rblAndOr.SelectedValue = "A";
        ddlConditionFormula2.SelectedValue = "-1";
        ddlConditionOperator2.SelectedValue = "-1";
        txtConditionalValue2.Text = string.Empty;
        ddlOutcomeFormula.SelectedValue = "-1";
        ddlOutcomeOperator.SelectedValue = "-1";
        ddlOutcomeFormula2.SelectedValue = "-1";
        CheckBoxPaySetup.Checked = false;
        CheckBoxValueDefault.Checked = false;
        rblSelectionCriteria.Enabled = true;
        ddlGrade.Enabled = true;
        ddlFormula.Enabled = true;
    }

    private void LoadGradeDescription()
    {
        string _msg = null;
        try
        {
            var dtGradeDescription = DataProcess.GetData(ConnectionStr, Sqlgenerate.SqlGetGradeDescription());
            grdLoadGradeDefination.DataSource = null;
            grdLoadGradeDefination.DataBind();
            if (dtGradeDescription.Rows.Count > 0)
            {
                grdLoadGradeDefination.DataSource = dtGradeDescription;
                grdLoadGradeDefination.DataBind();
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
    
    private void LoadFormulaDescription()
    {
        ClsDropDownListController.LoadDropDownList(ConnectionStr, Sqlgenerate.SqlGetFormulaDescription(), ddlFormula, "For_Mas_Cal_Name", "For_Mas_Cal_Code");
        ClsDropDownListController.LoadDropDownList(ConnectionStr, Sqlgenerate.SqlGetFormulaDescription(), ddlConditionFormula1, "For_Mas_Cal_Name", "For_Mas_Cal_Code");
        ClsDropDownListController.LoadDropDownList(ConnectionStr, Sqlgenerate.SqlGetFormulaDescription(), ddlConditionFormula2, "For_Mas_Cal_Name", "For_Mas_Cal_Code");
        ClsDropDownListController.LoadDropDownList(ConnectionStr, Sqlgenerate.SqlGetFormulaDescription(), ddlOutcomeFormula, "For_Mas_Cal_Name", "For_Mas_Cal_Code");
        ClsDropDownListController.LoadDropDownList(ConnectionStr, Sqlgenerate.SqlGetFormulaDescription(), ddlOutcomeFormula2, "For_Mas_Cal_Name", "For_Mas_Cal_Code");
    }

    private void BindGrid()
    {
        try
        {
            DataTable dt = new DataTable();
            if (btnAdd.Text == "Add")
            {
                if ((DataTable)ViewState["vdt"] != null)
                {
                    dt = (DataTable)ViewState["vdt"];
                }
                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add("Line", typeof(int));
                    dt.Columns.Add("FormulaValue", typeof(string));
                    dt.Columns.Add("FormulaText", typeof(string));
                    dt.Columns.Add("SelectionCriteriaValue", typeof(string));
                    dt.Columns.Add("SelectionCriteriaText", typeof(string));
                    dt.Columns.Add("ConditionFormula1Value", typeof(string));
                    dt.Columns.Add("ConditionFormula1Text", typeof(string));
                    dt.Columns.Add("ConditionOperator1Value", typeof(string));
                    dt.Columns.Add("ConditionValue1", typeof(string));
                    dt.Columns.Add("ConditionAndOrValue", typeof(string));
                    dt.Columns.Add("ConditionAndOrText", typeof(string));
                    dt.Columns.Add("OutcomeFormula1Value", typeof(string));
                    dt.Columns.Add("OutcomeFormula1Text", typeof(string));
                    dt.Columns.Add("OutcomeOperatorValue", typeof(string));
                    dt.Columns.Add("OutcomeFormula2Value", typeof(string));
                    dt.Columns.Add("OutcomeFormula2Text", typeof(string));
                    dt.Columns.Add("PaysetupDefault", typeof(string));
                    dt.Columns.Add("ValueDefault", typeof(string));
                }
                var lineNo = Convert.ToInt32(dt.AsEnumerable().Max(row => row["Line"])) + 1;
                string formulaValue = ddlFormula.SelectedValue;
                string formulaText = ddlFormula.SelectedItem.Text;
                string selectionCriteriaValue = rblSelectionCriteria.SelectedValue;
                string selectionCriteriaText = rblSelectionCriteria.SelectedItem.Text;
                string paysetupValue = (CheckBoxPaySetup.Checked == true ? "T" : "F");
                string valueDefault = (CheckBoxValueDefault.Checked == true ? "1" : null);

                if (selectionCriteriaValue == "W")
                {
                    dt.Rows.Add
                        (
                        lineNo,
                        formulaValue,
                        formulaText,
                        selectionCriteriaValue,
                        selectionCriteriaText,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        paysetupValue,
                        valueDefault
                        );
                }
                if (selectionCriteriaValue == "V")
                {
                    string selectionValue = txtSelectionValue.Text == string.Empty ? null : txtSelectionValue.Text;
                    dt.Rows.Add
                        (
                        lineNo,
                        formulaValue,
                        formulaText,
                        selectionCriteriaValue,
                        selectionCriteriaText,
                        null,
                        null,
                        null,
                        selectionValue,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        paysetupValue,
                        valueDefault
                        );
                }
                if (selectionCriteriaValue == "C")
                {
                    string selectionValue = txtSelectionValue.Text == string.Empty ? null : txtSelectionValue.Text;
                    string conditionFormula1Value = ddlConditionFormula1.SelectedValue;
                    string conditionFormula1Text = ddlConditionFormula1.SelectedItem.Text;
                    string conditionOperator1Value = ddlConditionOperator1.SelectedValue;
                    string conditionValue1 = txtConditionalValue1.Text == string.Empty ? null : txtConditionalValue1.Text;
                    string conditionAndOrValue = rblAndOr.SelectedValue;
                    string conditionAndOrText = rblAndOr.SelectedItem.Text;
                    string outcomeFormula1Value = ddlOutcomeFormula.SelectedValue;
                    string outcomeFormula1Text = ddlOutcomeFormula.SelectedItem.Text;
                    string outcomeOperatorValue = ddlOutcomeOperator.SelectedValue;
                    string outcomeFormula2Value = ddlOutcomeFormula2.SelectedValue;
                    string outcomeFormula2Text = ddlOutcomeFormula2.SelectedItem.Text;

                    dt.Rows.Add
                        (
                        lineNo,
                        formulaValue,
                        formulaText,
                        selectionCriteriaValue,
                        selectionCriteriaText,
                        conditionFormula1Value,
                        conditionFormula1Text,
                        conditionOperator1Value,
                        conditionValue1,
                        conditionAndOrValue,
                        conditionAndOrText,
                        outcomeFormula1Value,
                        outcomeFormula1Text,
                        outcomeOperatorValue,
                        outcomeFormula2Value,
                        outcomeFormula2Text,
                        paysetupValue,
                        valueDefault
                        );
                    string conditionFormula2Value = ddlConditionFormula2.SelectedValue;
                    string conditionFormula2Text = ddlConditionFormula2.SelectedItem.Text;
                    string conditionOperator2Value = ddlConditionOperator2.SelectedValue;
                    string conditionValue2 = txtConditionalValue2.Text == string.Empty ? null : txtConditionalValue2.Text;
                    dt.Rows.Add
                        (
                        lineNo,
                        formulaValue,
                        formulaText,
                        selectionCriteriaValue,
                        selectionCriteriaText,
                        conditionFormula2Value,
                        conditionFormula2Text,
                        conditionOperator2Value,
                        conditionValue2,
                        conditionAndOrValue,
                        conditionAndOrText,
                        outcomeFormula1Value,
                        outcomeFormula1Text,
                        outcomeOperatorValue,
                        outcomeFormula2Value,
                        outcomeFormula2Text,
                        paysetupValue,
                        valueDefault
                        );
                }
                ViewState["vdt"] = dt;
                grdGetSelectedValue.DataSource = dt;
                grdGetSelectedValue.DataBind();
                ClearAllControlForSecondTab();
            }
            else if (btnAdd.Text != "Add")
            {
                if ((DataTable)ViewState["vdt"] != null)
                {
                    dt = (DataTable)ViewState["vdt"];
                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow dr = dt.Rows[i];
                        if (dr["Line"].ToString() == Session["index"].ToString())
                        {
                            dt.Rows[i].Delete();
                        }
                    }
                    dt.AcceptChanges();
                }
                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add("Line", typeof(int));
                    dt.Columns.Add("FormulaValue", typeof(string));
                    dt.Columns.Add("FormulaText", typeof(string));
                    dt.Columns.Add("SelectionCriteriaValue", typeof(string));
                    dt.Columns.Add("SelectionCriteriaText", typeof(string));
                    dt.Columns.Add("ConditionFormula1Value", typeof(string));
                    dt.Columns.Add("ConditionFormula1Text", typeof(string));
                    dt.Columns.Add("ConditionOperator1Value", typeof(string));
                    dt.Columns.Add("ConditionValue1", typeof(string));
                    dt.Columns.Add("ConditionAndOrValue", typeof(string));
                    dt.Columns.Add("ConditionAndOrText", typeof(string));
                    dt.Columns.Add("OutcomeFormula1Value", typeof(string));
                    dt.Columns.Add("OutcomeFormula1Text", typeof(string));
                    dt.Columns.Add("OutcomeOperatorValue", typeof(string));
                    dt.Columns.Add("OutcomeFormula2Value", typeof(string));
                    dt.Columns.Add("OutcomeFormula2Text", typeof(string));
                    dt.Columns.Add("PaysetupDefault", typeof(string));
                    dt.Columns.Add("ValueDefault", typeof(string));
                }
                var lineNo = Convert.ToInt32(Session["index"].ToString());
                string formulaValue = ddlFormula.SelectedValue;
                string formulaText = ddlFormula.SelectedItem.Text;
                string selectionCriteriaValue = rblSelectionCriteria.SelectedValue;
                string selectionCriteriaText = rblSelectionCriteria.SelectedItem.Text;
                string paysetupValue = (CheckBoxPaySetup.Checked == true ? "T" : "F");
                string valueDefault = (CheckBoxValueDefault.Checked == true ? "1" : null);

                if (selectionCriteriaValue == "W")
                {
                    dt.Rows.Add
                        (
                        lineNo,
                        formulaValue,
                        formulaText,
                        selectionCriteriaValue,
                        selectionCriteriaText,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        paysetupValue,
                        valueDefault
                        );
                }
                if (selectionCriteriaValue == "V")
                {
                    string selectionValue = txtSelectionValue.Text == string.Empty ? null : txtSelectionValue.Text;
                    dt.Rows.Add
                        (
                        lineNo,
                        formulaValue,
                        formulaText,
                        selectionCriteriaValue,
                        selectionCriteriaText,
                        null,
                        null,
                        null,
                        selectionValue,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        paysetupValue,
                        valueDefault
                        );
                }
                if (selectionCriteriaValue == "C")
                {
                    string selectionValue = txtSelectionValue.Text == string.Empty ? null : txtSelectionValue.Text;
                    string conditionFormula1Value = ddlConditionFormula1.SelectedValue;
                    string conditionFormula1Text = ddlConditionFormula1.SelectedItem.Text;
                    string conditionOperator1Value = ddlConditionOperator1.SelectedValue;
                    string conditionValue1 = txtConditionalValue1.Text == string.Empty ? null : txtConditionalValue1.Text;
                    string conditionAndOrValue = rblAndOr.SelectedValue;
                    string conditionAndOrText = rblAndOr.SelectedItem.Text;
                    string outcomeFormula1Value = ddlOutcomeFormula.SelectedValue;
                    string outcomeFormula1Text = ddlOutcomeFormula.SelectedItem.Text;
                    string outcomeOperatorValue = ddlOutcomeOperator.SelectedValue;
                    string outcomeFormula2Value = ddlOutcomeFormula2.SelectedValue;
                    string outcomeFormula2Text = ddlOutcomeFormula2.SelectedItem.Text;

                    dt.Rows.Add
                        (
                        lineNo,
                        formulaValue,
                        formulaText,
                        selectionCriteriaValue,
                        selectionCriteriaText,
                        conditionFormula1Value,
                        conditionFormula1Text,
                        conditionOperator1Value,
                        conditionValue1,
                        conditionAndOrValue,
                        conditionAndOrText,
                        outcomeFormula1Value,
                        outcomeFormula1Text,
                        outcomeOperatorValue,
                        outcomeFormula2Value,
                        outcomeFormula2Text,
                        paysetupValue,
                        valueDefault
                        );
                    string conditionFormula2Value = ddlConditionFormula2.SelectedValue;
                    string conditionFormula2Text = ddlConditionFormula2.SelectedItem.Text;
                    string conditionOperator2Value = ddlConditionOperator2.SelectedValue;
                    string conditionValue2 = txtConditionalValue2.Text == string.Empty ? null : txtConditionalValue2.Text;
                    dt.Rows.Add
                        (
                        lineNo,
                        formulaValue,
                        formulaText,
                        selectionCriteriaValue,
                        selectionCriteriaText,
                        conditionFormula2Value,
                        conditionFormula2Text,
                        conditionOperator2Value,
                        conditionValue2,
                        conditionAndOrValue,
                        conditionAndOrText,
                        outcomeFormula1Value,
                        outcomeFormula1Text,
                        outcomeOperatorValue,
                        outcomeFormula2Value,
                        outcomeFormula2Text,
                        paysetupValue,
                        valueDefault
                        );
                }
                #region ForUpdate
                //dt = (DataTable)ViewState["vdt"];
                //int i = 1;
                //int updateFirstline = 1;
                //foreach (System.Data.DataColumn col in dt.Columns) col.ReadOnly = false;
                //foreach (DataRow dr in dt.Rows)
                //{
                //    if (Convert.ToInt32(Session["index"].ToString()) == Convert.ToInt32(dr["Line"].ToString()))  
                //    {
                //        string formulaValue = ddlFormula.SelectedValue;
                //        string formulaText = ddlFormula.SelectedItem.Text;
                //        string selectionCriteriaValue = rblSelectionCriteria.SelectedValue;
                //        string selectionCriteriaText = rblSelectionCriteria.SelectedItem.Text;
                //        string paysetupValue = (CheckBoxPaySetup.Checked == true ? "T" : "F");
                //        string valueDefault = (CheckBoxValueDefault.Checked == true ? "1" : null);
                //        if (selectionCriteriaValue == "W")
                //        {
                //            dr["FormulaValue"] = formulaValue;
                //            dr["FormulaText"] = formulaText;
                //            dr["SelectionCriteriaValue"] = selectionCriteriaValue;
                //            dr["SelectionCriteriaText"] =  selectionCriteriaText;
                //            dr["ConditionFormula1Value"] =  null;
                //            dr["ConditionFormula1Text"] =   null;
                //            dr["ConditionOperator1Value"] =  null;
                //            dr["ConditionValue1"] =          null;
                //            dr["ConditionAndOrValue"] =      null;
                //            dr["ConditionAndOrText"] =       null;
                //            dr["OutcomeFormula1Value"] =     null;
                //            dr["OutcomeFormula1Text"] =      null;
                //            dr["OutcomeOperatorValue"] =     null;
                //            dr["OutcomeFormula2Value"] =     null;
                //            dr["OutcomeFormula2Text"] =      null;
                //            dr["PaysetupDefault"] =          paysetupValue;
                //            dr["ValueDefault"] = valueDefault;
                //        }
                //        if (selectionCriteriaValue == "V")
                //        {
                //            string selectionValue = txtSelectionValue.Text == string.Empty ? null : txtSelectionValue.Text;
                //            dr["FormulaValue"] = formulaValue;
                //            dr["FormulaText"] = formulaText;
                //            dr["SelectionCriteriaValue"] = selectionCriteriaValue;
                //            dr["SelectionCriteriaText"] = selectionCriteriaText;
                //            dr["ConditionFormula1Value"] = null;
                //            dr["ConditionFormula1Text"] = null;
                //            dr["ConditionOperator1Value"] = null;
                //            dr["ConditionValue1"] = selectionValue;
                //            dr["ConditionAndOrValue"] = null;
                //            dr["ConditionAndOrText"] = null;
                //            dr["OutcomeFormula1Value"] = null;
                //            dr["OutcomeFormula1Text"] = null;
                //            dr["OutcomeOperatorValue"] = null;
                //            dr["OutcomeFormula2Value"] = null;
                //            dr["OutcomeFormula2Text"] = null;
                //            dr["PaysetupDefault"] = paysetupValue;
                //            dr["ValueDefault"] = valueDefault;
                //        }
                //        if (selectionCriteriaValue == "C")
                //        {
                //            string selectionValue = txtSelectionValue.Text == string.Empty ? null : txtSelectionValue.Text;
                //            string conditionFormula1Value = ddlConditionFormula1.SelectedValue;
                //            string conditionFormula1Text = ddlConditionFormula1.SelectedItem.Text;
                //            string conditionOperator1Value = ddlConditionOperator1.SelectedValue;
                //            string conditionValue1 = txtConditionalValue1.Text == string.Empty ? null : txtConditionalValue1.Text;
                //            string conditionAndOrValue = rblAndOr.SelectedValue;
                //            string conditionAndOrText = rblAndOr.SelectedItem.Text;
                //            string outcomeFormula1Value = ddlOutcomeFormula.SelectedValue;
                //            string outcomeFormula1Text = ddlOutcomeFormula.SelectedItem.Text;
                //            string outcomeOperatorValue = ddlOutcomeOperator.SelectedValue;
                //            string outcomeFormula2Value = ddlOutcomeFormula2.SelectedValue;
                //            string outcomeFormula2Text = ddlOutcomeFormula2.SelectedItem.Text;

                //            if (updateFirstline == 1)
                //            {
                //                dr["FormulaValue"] = formulaValue;
                //                dr["FormulaText"] = formulaText;
                //                dr["SelectionCriteriaValue"] = selectionCriteriaValue;
                //                dr["SelectionCriteriaText"] = selectionCriteriaText;
                //                dr["ConditionFormula1Value"] = conditionFormula1Value;
                //                dr["ConditionFormula1Text"] = conditionFormula1Text;
                //                dr["ConditionOperator1Value"] = conditionOperator1Value;
                //                dr["ConditionValue1"] = conditionValue1;
                //                dr["ConditionAndOrValue"] = conditionAndOrValue;
                //                dr["ConditionAndOrText"] = conditionAndOrText;
                //                dr["OutcomeFormula1Value"] = outcomeFormula1Value;
                //                dr["OutcomeFormula1Text"] = outcomeFormula1Text;
                //                dr["OutcomeOperatorValue"] = outcomeOperatorValue;
                //                dr["OutcomeFormula2Value"] = outcomeFormula2Value;
                //                dr["OutcomeFormula2Text"] = outcomeFormula2Text;
                //                dr["PaysetupDefault"] = paysetupValue;
                //                dr["ValueDefault"] = valueDefault;
                //                i = i - 1;
                //            }
                //            if (updateFirstline == 2)
                //            {
                //                string conditionFormula2Value = ddlConditionFormula2.SelectedValue;
                //                string conditionFormula2Text = ddlConditionFormula2.SelectedItem.Text;
                //                string conditionOperator2Value = ddlConditionOperator2.SelectedValue;
                //                string conditionValue2 = txtConditionalValue2.Text == string.Empty ? null : txtConditionalValue2.Text;
                //                dr["FormulaValue"] = formulaValue;
                //                dr["FormulaText"] = formulaText;
                //                dr["SelectionCriteriaValue"] = selectionCriteriaValue;
                //                dr["SelectionCriteriaText"] = selectionCriteriaText;
                //                dr["ConditionFormula1Value"] = conditionFormula2Value;
                //                dr["ConditionFormula1Text"] = conditionFormula2Text;
                //                dr["ConditionOperator1Value"] = conditionOperator2Value;
                //                dr["ConditionValue1"] = conditionValue2;
                //                dr["ConditionAndOrValue"] = conditionAndOrValue;
                //                dr["ConditionAndOrText"] = conditionAndOrText;
                //                dr["OutcomeFormula1Value"] = outcomeFormula1Value;
                //                dr["OutcomeFormula1Text"] = outcomeFormula1Text;
                //                dr["OutcomeOperatorValue"] = outcomeOperatorValue;
                //                dr["OutcomeFormula2Value"] = outcomeFormula2Value;
                //                dr["OutcomeFormula2Text"] = outcomeFormula2Text;
                //                dr["PaysetupDefault"] = paysetupValue;
                //                dr["ValueDefault"] = valueDefault;
                //            }
                //            updateFirstline = 2;
                //        }
                //    }
                //}
                #endregion ForUpdate
                dt.AcceptChanges();
                dt.DefaultView.Sort = "Line";
                dt = dt.DefaultView.ToTable();
                ViewState["vdt"] = dt;
                grdGetSelectedValue.DataSource = null;
                grdGetSelectedValue.DataBind();
                grdGetSelectedValue.DataSource = dt;
                grdGetSelectedValue.DataBind();
                btnAdd.Text = "Add";
                ClearAllControlForSecondTab();
            }
        }
        catch (Exception excMsg)
        {
            MessageBox1.ShowError("Error:" + excMsg.Message);
        }
    }

    private void DisableCommandField(GridView grdView)
    {
        foreach (GridViewRow rwNumber in grdView.Rows)
        {
            rwNumber.Cells[19].Enabled = false;
        }
    }

    protected void btnSaveGradeDefination_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckAllValidation();
        switch (validationMsg)
        {
            case "":
                {

                    string msg = SaveGradeDescription();
                    ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "MessageBox",
                        "alert(' " + msg + "');",
                        true);
                }
                break;
            default:
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + validationMsg + "');",
                    true);
                break;
        }
    }

    protected void grdLoadGradeDefination_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        string lblGradeCode = ((Label)grdLoadGradeDefination.Rows[selectedIndex].FindControl("lblGradeCode")).Text;
        string lblGradeDescription = ((Label)grdLoadGradeDefination.Rows[selectedIndex].FindControl("lblGradeDescription")).Text;
        if (e.CommandName.Equals("Delete"))
        {
            try
            {
                DataProcess.DeleteQuery(ConnectionStr, Sqlgenerate.SqlDeleteGradeDescription(lblGradeCode));
                LoadGradeDescription();
            }
            catch (SqlException sqlError)
            {
                ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "MessageBox",
                        "alert(' Error Occured During Operation into Database, Data did not Delete from Database ! ');",
                        true);
            }
            catch (Exception inSystemExep)
            {
                ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "MessageBox",
                        "alert(' Error Occured, Data did not Delete from Database  ! ');",
                        true);
            }
        }
        else if (e.CommandName.Equals("Select"))
        {
            txtGradeDefination.Text = lblGradeDescription;
            lblGradeCodeForUpdate.Text = lblGradeCode;
            btnSaveGradeDefination.Text = "Update";
        }
    }

    protected void grdLoadGradeDefination_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void grdLoadGradeDefination_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void rblSelectionCriteria_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.rblSelectionCriteria.SelectedValue == "C")
        {
            PanelForCondition.Visible = true;
            txtSelectionValue.Visible = false;
        }
        else if (this.rblSelectionCriteria.SelectedValue == "V")
        {
            PanelForCondition.Visible = false;
            txtSelectionValue.Visible = true;
        }
        else
        {
            PanelForCondition.Visible = false;
            txtSelectionValue.Visible = false;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckAllValidationForSecondTab();
        switch (validationMsg)
        {
            case "":
                {
                    BindGrid();
                }
                break;
            default:
                MessageBox1.ShowWarning(validationMsg);
                break;
        }
    }

    public static void MergeRows(GridView gridView)
    {
        for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gridView.Rows[rowIndex];
            GridViewRow previousRow = gridView.Rows[rowIndex + 1];
            var lblLine = (Label)gridView.Rows[rowIndex].FindControl("lblLine");
            var lblLinePrevious = (Label)gridView.Rows[rowIndex + 1].FindControl("lblLine");
            if (lblLine.Text == lblLinePrevious.Text)
            {
                row.Cells[0].RowSpan = previousRow.Cells[0].RowSpan < 2 ? 2 : previousRow.Cells[0].RowSpan + 1;
                row.Cells[18].RowSpan = previousRow.Cells[18].RowSpan < 2 ? 2 : previousRow.Cells[18].RowSpan + 1;
                row.Cells[19].RowSpan = previousRow.Cells[19].RowSpan < 2 ? 2 : previousRow.Cells[19].RowSpan + 1;
                previousRow.Cells[0].Visible = false;
                previousRow.Cells[18].Visible = false;
                previousRow.Cells[19].Visible = false;
            }
        }
    }

    private void LoadGrade(string gradeCode)
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [GradeGetFrom_hrms_grade_det] '" + gradeCode + "'";
            var dtGrade = StoredProcedureExecutor.StoredProcedureExecuteReader(ConnectionStr, storedProcedureCommandTest);

            ViewState["vdt"] = null;
            ViewState["vdt"] = dtGrade;
            grdGetSelectedValue.DataSource = null;
            grdGetSelectedValue.DataBind();
            if (dtGrade.Rows.Count != 0)
            {
                grdGetSelectedValue.DataSource = dtGrade;
                grdGetSelectedValue.DataBind();
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

    protected void grdGetSelectedValue_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        string lblLine = ((Label)grdGetSelectedValue.Rows[selectedIndex].FindControl("lblLine")).Text;
        if (e.CommandName.Equals("Delete"))
        {
            try
            {
                if ((DataTable)ViewState["vdt"] != null)
                {
                    var dt = (DataTable)ViewState["vdt"];
                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow dr = dt.Rows[i];
                        if (dr["Line"].ToString() == lblLine)
                        {
                            dt.Rows[i].Delete();
                        }
                    }
                    dt.AcceptChanges();
                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow dr = dt.Rows[i];
                        int lineNumberInDt = Convert.ToInt32(dr["Line"].ToString());
                        int newLineNumber = Convert.ToInt32(lblLine);
                        if (lineNumberInDt > newLineNumber)
                        {
                            dr["Line"] = (lineNumberInDt - 1).ToString();
                        }
                    }
                    dt.AcceptChanges();
                    ViewState["vdt"] = dt;
                    grdGetSelectedValue.DataSource = null;
                    grdGetSelectedValue.DataBind();
                    if (dt.Rows.Count != 0)
                    {
                        grdGetSelectedValue.DataSource = dt;
                        grdGetSelectedValue.DataBind();
                    }
                }
            }
            catch (Exception inSystemExep)
            {
                ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "MessageBox",
                        "alert(' Error Occured, Data did not Delete  ! ');",
                        true);
            }
        }
        else if (e.CommandName.Equals("Select"))
        {
            Session["index"] = lblLine;
            btnAdd.Text = "Update";
            //rblSelectionCriteria.Enabled = false;
            string lblFormulaValue = ((Label)grdGetSelectedValue.Rows[selectedIndex].FindControl("lblFormulaValue")).Text;
            string lblSelectionCriteriaValue = ((Label)grdGetSelectedValue.Rows[selectedIndex].FindControl("lblSelectionCriteriaValue")).Text;
            string lblPaysetupDefault = ((Label)grdGetSelectedValue.Rows[selectedIndex].FindControl("lblPaysetupDefault")).Text;
            string lblValueDefault = ((Label)grdGetSelectedValue.Rows[selectedIndex].FindControl("lblValueDefault")).Text;
            string lblConditionValue1 = ((Label)grdGetSelectedValue.Rows[selectedIndex].FindControl("lblConditionValue1")).Text;

            ddlFormula.SelectedValue = lblFormulaValue;
            rblSelectionCriteria.SelectedValue = lblSelectionCriteriaValue;
            if (lblPaysetupDefault == "T") CheckBoxPaySetup.Checked = true;
            else CheckBoxPaySetup.Checked = false;
            if (lblValueDefault == "1") CheckBoxValueDefault.Checked = true;
            else CheckBoxValueDefault.Checked = false;
            if (lblSelectionCriteriaValue == "V")
            {
                PanelForCondition.Visible = false;
                txtSelectionValue.Visible = true;
                txtSelectionValue.Text = lblConditionValue1;
            }
            if (lblSelectionCriteriaValue == "C")
            {
                string lblConditionFormula1Value = ((Label)grdGetSelectedValue.Rows[selectedIndex].FindControl("lblConditionFormula1Value")).Text;
                string lblConditionOperator1Value = ((Label)grdGetSelectedValue.Rows[selectedIndex].FindControl("lblConditionOperator1Value")).Text;
                string lblConditionAndOrValue = ((Label)grdGetSelectedValue.Rows[selectedIndex].FindControl("lblConditionAndOrValue")).Text;
                string lblOutcomeFormula1Value = ((Label)grdGetSelectedValue.Rows[selectedIndex].FindControl("lblOutcomeFormula1Value")).Text;
                string lblOutcomeOperatorValue = ((Label)grdGetSelectedValue.Rows[selectedIndex].FindControl("lblOutcomeOperatorValue")).Text;
                string lblOutcomeFormula2Value = ((Label)grdGetSelectedValue.Rows[selectedIndex].FindControl("lblOutcomeFormula2Value")).Text;
                string lblConditionFormula2Value = ((Label)grdGetSelectedValue.Rows[selectedIndex + 1].FindControl("lblConditionFormula1Value")).Text;
                string lblConditionOperator2Value = ((Label)grdGetSelectedValue.Rows[selectedIndex + 1].FindControl("lblConditionOperator1Value")).Text;
                string lblConditionValue2 = ((Label)grdGetSelectedValue.Rows[selectedIndex + 1].FindControl("lblConditionValue1")).Text;

                ddlConditionFormula1.SelectedValue = lblConditionFormula1Value;
                ddlConditionOperator1.SelectedValue = lblConditionOperator1Value;
                rblAndOr.SelectedValue = lblConditionAndOrValue;
                txtConditionalValue1.Text = lblConditionValue1;
                ddlOutcomeFormula.SelectedValue = lblOutcomeFormula1Value;
                ddlOutcomeOperator.SelectedValue = lblOutcomeOperatorValue;
                ddlOutcomeFormula2.SelectedValue = lblOutcomeFormula2Value;
                ddlConditionFormula2.SelectedValue = lblConditionFormula2Value;
                ddlConditionOperator2.SelectedValue = lblConditionOperator2Value;
                txtConditionalValue2.Text = lblConditionValue2;
                PanelForCondition.Visible = true;
                txtSelectionValue.Visible = false;
            }
            if (lblSelectionCriteriaValue == "W")
            {
                PanelForCondition.Visible = false;
                txtSelectionValue.Visible = false;
            }
            DisableCommandField(grdGetSelectedValue);
            ddlGrade.Enabled = false;
            ddlFormula.Enabled = false;
        }
    }

    protected void grdGetSelectedValue_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void grdGetSelectedValue_PreRender(object sender, EventArgs e)
    {
        MergeRows(grdGetSelectedValue);
    }

    protected void grdGetSelectedValue_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[3].Visible = false;
        e.Row.Cells[5].Visible = false;
        e.Row.Cells[9].Visible = false;
        e.Row.Cells[11].Visible = false;
        e.Row.Cells[14].Visible = false;
    }

    private void ClearControlAfterSave()
    {
        ddlFormula.SelectedValue = "-1";
        ddlGrade.SelectedValue = "-1";
    }

    private string SaveGradeDetails()
    {
        string _msg;
        try
        {
            string gradeValue = ddlGrade.SelectedValue;
            //string formulaValue = ddlFormula.SelectedValue;
            
            foreach (GridViewRow itemRow in grdGetSelectedValue.Rows)
            {
                if (itemRow.RowType == DataControlRowType.DataRow)
                {
                    string lblFormulaValue = ((Label)itemRow.FindControl("lblFormulaValue")).Text;
                    int lblLine = Convert.ToInt32(((Label)itemRow.FindControl("lblLine")).Text);
                    string lblSelectionCriteriaValue = ((Label)itemRow.FindControl("lblSelectionCriteriaValue")).Text;
                    string lblPaysetupDefault = ((Label)itemRow.FindControl("lblPaysetupDefault")).Text;

                    string lblValueDefault = ((Label)itemRow.FindControl("lblValueDefault")).Text;
                    string lblConditionFormula1Value = ((Label)itemRow.FindControl("lblConditionFormula1Value")).Text;
                    string lblConditionOperator1Value = ((Label)itemRow.FindControl("lblConditionOperator1Value")).Text;
                    string lblConditionValue1 = ((Label)itemRow.FindControl("lblConditionValue1")).Text;

                    string lblConditionAndOrValue = ((Label)itemRow.FindControl("lblConditionAndOrValue")).Text;

                    string lblOutcomeFormula1Value = ((Label)itemRow.FindControl("lblOutcomeFormula1Value")).Text;
                    string lblOutcomeOperatorValue = ((Label)itemRow.FindControl("lblOutcomeOperatorValue")).Text;
                    string lblOutcomeFormula2Value = ((Label)itemRow.FindControl("lblOutcomeFormula2Value")).Text;
                    DataProcess.DeleteQuery(ConnectionStr, Sqlgenerate.SqlDeleteGradeDescription(gradeValue, lblFormulaValue));
                    var storedProcedureComandTest = "exec [GradeInitiateInto_hrms_grade_det]" +
                                                    "'" + gradeValue + "','" +
                                                    lblFormulaValue + "'," +
                                                    lblLine + ",'" +
                                                    lblConditionFormula1Value + "','" +
                                                    lblConditionOperator1Value + "','" +
                                                    lblConditionValue1 + "','" +
                                                    lblValueDefault + "','" +
                                                    lblOutcomeFormula1Value + "','" +
                                                    lblOutcomeOperatorValue + "','" +
                                                    lblOutcomeFormula2Value + "','" +
                                                    lblSelectionCriteriaValue + "','" +
                                                    lblPaysetupDefault + "'," +
                                                    lblLine + ",'" +
                                                    lblConditionAndOrValue + "'";
                    StoredProcedureExecutor.StoredProcedureExecuteNonQuery(ConnectionStr, storedProcedureComandTest);
                }
            }
            _msg = "Data Saved Successfully ";
            ViewState["vdt"] = null;
            grdGetSelectedValue.DataSource = null;
            grdGetSelectedValue.DataBind();
            ClearControlAfterSave();
        }
        catch (SqlException sqlError)
        {
            _msg = "Error Occured During Operation into Database, Data did not Save into Database !";
        }
        catch (Exception inSystemExep)
        {
            _msg = " Error Occured, Data did not Save into Database !";
        }
        return _msg;
    }

    private string CheckAllValidationForGradeDetails()
    {
        const string checkValidation = "";
        if (ddlGrade.SelectedValue == "-1")
        {
            ddlGrade.Focus();
            return "Please Select Grade Correctly !";
        }
        if (grdGetSelectedValue.Rows.Count == 0)
        {
            return "Please Add Grade Details !";
        }
        return checkValidation;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckAllValidationForGradeDetails();
        switch (validationMsg)
        {
            case "":
                {

                    string msg = SaveGradeDetails();
                    ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "MessageBox",
                        "alert(' " + msg + "');",
                        true);
                }
                break;
            default:
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "MessageBox",
                    "alert(' " + validationMsg + "');",
                    true);
                break;
        }
    }

    protected void ddlGrade_SelectedIndexChanged(object sender, EventArgs e)
    {

        LoadGrade(ddlGrade.SelectedValue);
    }

    protected void ddlFormula_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    
}