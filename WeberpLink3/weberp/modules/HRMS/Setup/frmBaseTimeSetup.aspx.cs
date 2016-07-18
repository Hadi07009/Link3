using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Setup_frmBaseTimeSetup : System.Web.UI.Page
{
    string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    private BaseTimeSetup _objBaseTimeSetup;
    private BaseTimeSetupController _objBaseTimeSetupController;
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);
        popupFromDate.Attributes.Add("readonly", "readonly");
        popupToDate.Attributes.Add("readonly", "readonly");
        try
        {
            if (!Page.IsPostBack)
            {
                ClsDropDownListController.LoadDropDownList(_connectionString, Sqlgenerate.SqlGetShiftTypeIntoDDL(), ddlShift, "Shift", "Shift Code");
                GetData();

            }

        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            SaveBaseTime();
            ClearControl();
            GetData();

        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }

    private void SaveBaseTime()
    {
        try
        {
            _objBaseTimeSetup = new BaseTimeSetup();
            _objBaseTimeSetup.ShiftCode = ddlShift.SelectedValue;
            _objBaseTimeSetup.FromDate = Convert.ToDateTime(popupFromDate.Text);
            _objBaseTimeSetup.ToDate = Convert.ToDateTime(popupToDate.Text);
            CommonMethods objCommonMethods = new CommonMethods();
            _objBaseTimeSetup.InTime = objCommonMethods.TimeFormatGenerate(inTime.Date.Hour.ToString() + ":" + inTime.Date.Minute.ToString() + ":" + inTime.AmPm.ToString());
            _objBaseTimeSetup.OutTime = objCommonMethods.TimeFormatGenerate(outTime.Date.Hour.ToString() + ":" + outTime.Date.Minute.ToString() + ":" + outTime.AmPm.ToString());
            _objBaseTimeSetupController = new BaseTimeSetupController();
            _objBaseTimeSetupController.Save(_connectionString, _objBaseTimeSetup);

        }
        catch (Exception msgException)
        {
            
            throw msgException;
        }

    }
    private void ClearControl()
    {
        ddlShift.SelectedValue = "-1";
        popupFromDate.Text = string.Empty;
        popupToDate.Text = string.Empty;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearControl();
    }
    private void GetData()
    {
        _objBaseTimeSetupController = new BaseTimeSetupController();
        var dtBaseTimeRecord = _objBaseTimeSetupController.GetAll(_connectionString);
        grdBaseTime.DataSource = null;
        grdBaseTime.DataBind();
        if (dtBaseTimeRecord.Rows.Count > 0)
        {
            grdBaseTime.DataSource = dtBaseTimeRecord;
            grdBaseTime.DataBind();
            
        }
    }
    protected void grdBaseTime_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[6].Visible = false;
        e.Row.Cells[7].Visible = false;
    }
    protected void grdBaseTime_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        var lblSlno = ((Label)grdBaseTime.Rows[selectedIndex].FindControl("lblSlno")).Text;        
        if (e.CommandName.Equals("Delete"))
        {
            _objBaseTimeSetup = new BaseTimeSetup();
            _objBaseTimeSetup.SlNumber = Convert.ToInt32( lblSlno);
            _objBaseTimeSetupController = new BaseTimeSetupController();
            _objBaseTimeSetupController.Delete(_connectionString,_objBaseTimeSetup);
            GetData();
 
        }
    }
    protected void grdBaseTime_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}