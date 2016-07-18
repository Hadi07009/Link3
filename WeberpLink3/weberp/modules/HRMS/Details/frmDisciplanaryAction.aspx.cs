using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;


namespace modules.HRMS.Details
{
    public partial class modules_HRMS_Details_frmDisciplanaryAction : Page
    {
        readonly string _connectionString = ConfigurationManager.AppSettings["UbasysConnectionString"];
       
      
        private DisciplanaryAction _objDisciplanaryAction;
        private DisciplanaryActionController _objDisciplanaryActionController;

        protected void Page_Load(object sender, EventArgs e)
        {
            clsStatic.CheckUserAuthentication(true);

            popupJoiningDate.Attributes.Add("readonly", "readonly");
            if (!Page.IsPostBack)
            {
                txtEmployeeCode_AutoCompleteExtender.ContextKey = _connectionString;
                PanelForEmployeeDetails.Visible = false;
                btnExporttoExcel.Visible = false;
                btnPreview.Visible = false;
                
            }

        }
        private void ShowEmployeeDetails(string employeeId)
        {
            string msg = null;
            try
            {
                var storedProcedureCommandText = "exec [Transfer_PromotionGetEmployeeDetails] '" + employeeId + "'";
                var dtEmployeeDetails = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureCommandText);
                PanelForEmployeeDetails.Visible = false;
                if (dtEmployeeDetails.Rows.Count > 0)
                {
                    lblEmployeeName.Text = dtEmployeeDetails.Rows[0].ItemArray[0] + @" " + dtEmployeeDetails.Rows[0].ItemArray[1];
                    lblJoiningDate.Text = dtEmployeeDetails.Rows[0].ItemArray[2].ToString() == string.Empty ? null : Convert.ToDateTime(dtEmployeeDetails.Rows[0].ItemArray[2].ToString()).ToString("dd-MMM-yyyy");
                    lblOfficeLocation.Text = dtEmployeeDetails.Rows[0].ItemArray[3].ToString();
                    lblEmployeeDepartment.Text = dtEmployeeDetails.Rows[0].ItemArray[4].ToString();
                    lblSection.Text = dtEmployeeDetails.Rows[0].ItemArray[5].ToString();
                    lblDesignation.Text = dtEmployeeDetails.Rows[0].ItemArray[6].ToString() == string.Empty ? null : dtEmployeeDetails.Rows[0].ItemArray[6].ToString();
                    PanelForEmployeeDetails.Visible = true;
                }
            }
            catch (SqlException sqlError)
            {
                msg = " Error Occured During Load Data From Database, Data did not Loaded from Database ! ";

            }
            catch (Exception inSystemExep)
            {
                msg = " Error Occured, Data did not Loaded from Database  !";

            }
            finally
            {
                if (msg != null)
                {
                    ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "MessageBox",
                        "alert(' " + msg + " ');",
                        true);
                }
            }
        }

        private void ClearAllControl()
        {
            txtTopic.Text = string.Empty;
            popupJoiningDate.Text = string.Empty;
            txtInquary.Text = string.Empty;
            txtInquaryRecomondation.Text = string.Empty;
            txtAction.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            lblUpdate.Text = string.Empty;
            btnSave.Text = @"Save";
        }

        private void ShowDisciplanaryAction(string employeeCode)
        {
            var storedProcedureComandTest = "exec [DisciplanaryActionGet_HRMSDisciplanaryAction] '" + employeeCode + "'";
            var dtDisciplanaryAction = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureComandTest);
            grdDisciplanary.DataSource = null;
            grdDisciplanary.DataBind();
            btnExporttoExcel.Visible = false;
            btnPreview.Visible = false;
            if (dtDisciplanaryAction.Rows.Count > 0)
            {
                grdDisciplanary.DataSource = dtDisciplanaryAction;
                grdDisciplanary.DataBind();
                btnExporttoExcel.Visible = true;
                btnPreview.Visible = true;
            }
        }

        protected void txtEmployeeCode_TextChanged(object sender, EventArgs e)
        {
            if (txtEmployeeCode.Text != string.Empty)
            {
                txtEmployeeCode.Text = txtEmployeeCode.Text.Split(':')[0].Trim() == "" ? "" : txtEmployeeCode.Text.Split(':')[0].Trim();
                var employeeCode = txtEmployeeCode.Text;
                ShowEmployeeDetails(employeeCode);
                try
                {

                    ShowDisciplanaryAction(employeeCode);
                    ShowPrfileImage(employeeCode);

                }
                catch (Exception msgException)
                {

                    ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "MessageBox",
                        "alert(' " + msgException.Message + " ');",
                        true);
                }

            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveDisciplanaryAction();
        }

        private void SaveDisciplanaryAction()
        {
            try
            {
                _objDisciplanaryAction = new DisciplanaryAction
                {
                    CaseCode = lblUpdate.Text == string.Empty ? null : lblUpdate.Text,
                    EmployeeCode = txtEmployeeCode.Text == string.Empty ? null : txtEmployeeCode.Text,
                    CaseTopic = txtTopic.Text == string.Empty ? null : txtTopic.Text,
                    CaseDate =
                        popupJoiningDate.Text == string.Empty
                            ? null
                            : Convert.ToDateTime(popupJoiningDate.Text).ToString("dd-MMM-yyyy"),
                    Inquary = txtInquary.Text == string.Empty ? null : txtInquary.Text,
                    InquaryRecomondation = txtInquaryRecomondation.Text == string.Empty
                        ? null
                        : txtInquaryRecomondation.Text,
                    CaseAction = txtAction.Text == string.Empty ? null : txtAction.Text,
                    Remarks = txtRemarks.Text == string.Empty ? null : txtRemarks.Text,
                    EntryUser = current.UserId
                };
                _objDisciplanaryActionController = new DisciplanaryActionController();
                _objDisciplanaryActionController.Save(_connectionString, _objDisciplanaryAction);
                ClearAllControl();
                ShowDisciplanaryAction(_objDisciplanaryAction.EmployeeCode);
                MessageBox1.ShowSuccess("Data Saved Successfully ");

            }
            catch (Exception msgException)
            {

                MessageBox1.ShowError("Error:" + msgException.Message);
            }
        }

        protected void grdDisciplanary_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[8].Visible = false;
        }
        protected void grdDisciplanary_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
            var lblCaseCode = ((Label)grdDisciplanary.Rows[selectedIndex].FindControl("lblCaseCode")).Text;
            var lblEmployeeCode = ((Label)grdDisciplanary.Rows[selectedIndex].FindControl("lblEmployeeCode")).Text;
            var lblCaseTopic = ((Label)grdDisciplanary.Rows[selectedIndex].FindControl("lblCaseTopic")).Text;
            var lblCaseDatet = ((Label)grdDisciplanary.Rows[selectedIndex].FindControl("lblCaseDate")).Text;
            var lblCaseDate = Convert.ToDateTime(lblCaseDatet).ToString("dd-MMM-yyyy");
            var lblInquary = ((Label)grdDisciplanary.Rows[selectedIndex].FindControl("lblInquary")).Text;
            var lblInquaryRecomondation = ((Label)grdDisciplanary.Rows[selectedIndex].FindControl("lblInquaryRecomondation")).Text;
            var lblCaseAction = ((Label)grdDisciplanary.Rows[selectedIndex].FindControl("lblCaseAction")).Text;
            var lblRemarks = ((Label)grdDisciplanary.Rows[selectedIndex].FindControl("lblRemarks")).Text;
            _objDisciplanaryAction = new DisciplanaryAction
            {
                EmployeeCode = lblEmployeeCode,
                CaseCode = lblCaseCode,
                CaseDate = lblCaseDate,
                CaseAction = lblCaseAction,
                CaseTopic = lblCaseTopic,
                EntryUser = current.UserId,
                Inquary = lblInquary,
                InquaryRecomondation = lblInquaryRecomondation,
                Remarks = lblRemarks,
                Status = "D"
            };
            if (e.CommandName.Equals("Delete"))
            {
                string msg = null;
                try
                {
                    _objDisciplanaryActionController = new DisciplanaryActionController();
                    _objDisciplanaryActionController.Delete(_connectionString, _objDisciplanaryAction);
                    _objDisciplanaryActionController.SaveLogData(_connectionString, _objDisciplanaryAction);
                    ShowDisciplanaryAction(lblEmployeeCode);
                }
                catch (SqlException sqlError)
                {
                    msg = "  Error Occured During Operation into Database, Data did not Delete from Database !  ";

                }
                catch (Exception inSystemExep)
                {
                    msg = " Error Occured, Data did not Delete from Database  ! ";

                }
                finally
                {
                    if (msg != null)
                    {
                        ScriptManager.RegisterStartupScript(
                            this,
                            GetType(),
                            "MessageBox",
                            "alert(' " + msg + " ');",
                            true);
                    }
                }
            }
            else if (e.CommandName.Equals("Select"))
            {
                try
                {
                    txtEmployeeCode.Text = lblEmployeeCode;
                    ShowEmployeeDetails(lblEmployeeCode);
                    txtTopic.Text = lblCaseTopic;
                    popupJoiningDate.Text = lblCaseDate;
                    txtInquary.Text = lblInquary;
                    txtInquaryRecomondation.Text = lblInquaryRecomondation;
                    txtAction.Text = lblCaseAction;
                    txtRemarks.Text = lblRemarks;
                    btnSave.Text = @"Update";
                    lblUpdate.Text = lblCaseCode;

                }
                catch (Exception msgException)
                {

                    ScriptManager.RegisterStartupScript(
                            this,
                            GetType(),
                            "MessageBox",
                            "alert(' " + msgException.Message + " ');",
                            true);
                }
            }

        }
        protected void grdDisciplanary_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ShowEmployeeDetails(null);
            try
            {
                txtEmployeeCode.Text = string.Empty;
                ClearAllControl();
                grdDisciplanary.DataSource = null;
                grdDisciplanary.DataBind();
                btnExporttoExcel.Visible = false;
                lblImage.Text = @"<br />  Photo <br />  Not <br />  Available ";
                btnPreview.Visible = false;

            }
            catch (Exception msgException)
            {

                MessageBox1.ShowError("Error:" + msgException.Message);
            }
        }
        protected void btnExporttoExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdDisciplanary.Rows.Count != 0)
                {

                    string type = "DisciplanaryActionof" + txtEmployeeCode.Text.Trim() + ".xls";
                    ExportGridToExcel.Export(type, grdDisciplanary);
                }
                else
                {
                    MessageBox1.ShowInfo("There is no data for Export ! ");
                }

            }
            catch (Exception msgException)
            {

                MessageBox1.ShowError("Error:" + msgException.Message);
            }
        }

        private void ShowPrfileImage(string employeeCode)
        {

            var storedProcedureCommandText = "exec [EmpBasicInformationGetFromHrms_Emp_Photo] '" + employeeCode + "','CEL'";
            var dtPhoto = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureCommandText);
            lblImage.Text = @"<br />  Photo <br />  Not <br />  Available ";
            if (dtPhoto.Rows.Count > 0)
            {
                var img = (byte[])dtPhoto.Rows[0].ItemArray[0];
                var base64String = Convert.ToBase64String(img, 0, img.Length);
                lblImage.Text = @"<img src='data:image/png;base64," + base64String + @"' alt='<br />  Photo <br />  Not <br />  Available ' width='100px' hight='100px' vspace='5px' hspace='5px' />";

            }
        }
        private void ShowReport(string connectionString, string selectionfor, string parameter, string reportname)
        {
            var rpt = new clsReport();
            var myParams = new ParameterFields();
            var connInfo = new ConnectionInfo();
            string[] prm = parameter.Split(';');
            if (prm.Length > 0)
            {
                foreach (string t in prm)
                {
                    parameterpass(myParams, t.Split(':')[0], t.Split(':')[1]);
                }
            }
            string[] ff = connectionString.Split('=');
            string[] ss = ff[1].Split(';');
            connInfo.ServerName = ss[0];
            ss = ff[2].Split(';');
            connInfo.DatabaseName = ss[0];
            ss = ff[3].Split(';');
            connInfo.UserID = ss[0];
            ss = ff[4].Split(';');
            connInfo.Password = ss[0];
            rpt.FileName = reportname;
            rpt.ConnectionInfo = connInfo;
            rpt.ParametersFields = myParams;
            rpt.SelectionFormulla = selectionfor;
            Session[GlobalData.sessionReportDet] = rpt;
            RegisterStartupScript("click", "<script>window.open('./frm_report_viewer.aspx');</script>");
        }
        private void parameterpass(ParameterFields myParams, string pname, string value)
        {
            var param = new ParameterField();
            var dis1 = new ParameterDiscreteValue();
            param.ParameterFieldName = pname;
            dis1.Value = value;
            param.CurrentValues.Add(dis1);
            myParams.Add(param);
        }
        protected void btnPreviewAll_Click(object sender, EventArgs e)
        {
            try
            {
                const string selectionfor = "";
                string parameter = "CompanyName:"+current.CompanyName  + ";" +"CompanyAddress:"+ current.CompanyAddress;
                const string reportname = "../Reports/DisciplanaryActionReport.rpt";
                ShowReport(_connectionString, selectionfor, parameter, reportname);

            }
            catch (Exception msgException)
            {

                MessageBox1.ShowError("Error :" + msgException.Message);
            }
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                _objDisciplanaryAction = new DisciplanaryAction { EmployeeCode = txtEmployeeCode.Text };
                string selectionfor = "{HRMSDisciplanaryAction.employeeCode}='" + _objDisciplanaryAction.EmployeeCode + "'";
                string parameter = "CompanyName:" + current.CompanyName + ";" + "CompanyAddress:" + current.CompanyAddress;
                const string reportname = "../Reports/DisciplanaryActionReport.rpt";
                ShowReport(_connectionString, selectionfor, parameter, reportname);

            }
            catch (Exception msgException)
            {

                MessageBox1.ShowError("Error :" + msgException.Message);
            }

        }
    }
}