using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Xml;
using System.Web.UI.WebControls;
using LibraryDAL;
using LibraryDAL.dsLinkofficeTableAdapters;
using System.Data;
using LibraryDAL.dsMasTableAdapters;
using LibraryPF.dsMasterDataTableAdapters;
using LibraryPF;
public partial class frmWorkLocationSetup : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);
        if(!IsPostBack)
        {
            if (!Page.IsPostBack)
            {
                LoadOfficeLocation();
                Load_grid();
                txtWorkLocationId.Text = MaxLocID().ToString();
                
            }
        }
        
    }

    private string check_entry()
    {

        if (ddlOfficeLocation.Text == "") return "Please select Office Location";
        if (txtWorkLocationId.Text == "") return "Please enter Work Location ID";
        if (txtWorklocationName.Text == "") return "Please enter Work Location Name";

        return "";
    
    }

    private void Clear_field()
    {
        ddlOfficeLocation.Text = "";       
        txtWorklocationName.Text = "";
        txtAddress.Text = "";
        lblmessage.Text = "";
        txtWorkLocationId.Text = MaxLocID().ToString();

    }


    public void LoadOfficeLocation()
    {
        Hrms_Division_MasterTableAdapter office = new Hrms_Division_MasterTableAdapter();
        dsMasterData.Hrms_Division_MasterDataTable dtoffice = new dsMasterData.Hrms_Division_MasterDataTable();
        dtoffice = office.GetDataAll();
       

        ddlOfficeLocation.Items.Clear();
        ddlOfficeLocation.Items.Add("");
        foreach (dsMasterData.Hrms_Division_MasterRow dr in dtoffice.Rows)
        {
            ListItem lst = new ListItem();
            lst.Value = dr.Division_Master_Code.ToString();
            lst.Text = dr.Division_Master_Name.ToString();
            ddlOfficeLocation.Items.Add(lst);
        }
    }


    private int MaxLocID()
    {
        int maxid;
        HRMS_WORK_LOCATION_MASTERTableAdapter work = new HRMS_WORK_LOCATION_MASTERTableAdapter();
        return maxid = Convert.ToInt32(work.GetMaxLocationID()) + 1;


    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        HRMS_WORK_LOCATION_MASTERTableAdapter work = new HRMS_WORK_LOCATION_MASTERTableAdapter();
        dsMasterData.HRMS_WORK_LOCATION_MASTERDataTable dtwork = new dsMasterData.HRMS_WORK_LOCATION_MASTERDataTable();
        

        if (check_entry() != "")
        {
            lblmessage.Text = check_entry();
            lblmessage.ForeColor = System.Drawing.Color.Red;
            return;

        }

        dtwork = work.GetDataByOfficeIdWorkLocId(ddlOfficeLocation.SelectedItem.Value, txtWorkLocationId.Text);


        try
        {
            if (dtwork.Rows.Count > 0)
            {

                work.UpdateByOfficeIdWorkLocID(txtWorklocationName.Text, txtAddress.Text, ddlOfficeLocation.SelectedItem.Value, txtWorkLocationId.Text);

            }
            else
            {
                work.InsertWorkLocationSetup(ddlOfficeLocation.SelectedItem.Value, txtWorkLocationId.Text,txtWorklocationName.Text, txtAddress.Text);

            }
        }

        catch(Exception ex)
        {

            lblmessage.Text = ex.Message;
            lblmessage.ForeColor = System.Drawing.Color.Red;
            return;

        }

 
        Load_grid();
        Clear_field();

        lblmessage.Text = "Data Saved";
        lblmessage.ForeColor = System.Drawing.Color.Green;
     

    }


    private void Load_grid()
    {
        DataTable dt = new DataTable();

        GetWorkLocationDataTableAdapter work = new GetWorkLocationDataTableAdapter();
        dsMasterData.GetWorkLocationDataDataTable dtwork = new dsMasterData.GetWorkLocationDataDataTable();

        dtwork = work.GetDataAll();

        dt.Columns.Add("Office ID", typeof(string));
        dt.Columns.Add("Office Name", typeof(string));
        dt.Columns.Add("Work Location ID", typeof(string));
        dt.Columns.Add("Work Location Name", typeof(string));
        dt.Columns.Add("Address", typeof(string));


        foreach (dsMasterData.GetWorkLocationDataRow dr in dtwork.Rows)
        {            

           

            dt.Rows.Add(dr.Division_Master_Code, dr.Division_Master_Name, dr.WorkLocationId, dr.WorkLocationName,dr.IsWorkLocAddressNull()?"--":dr.WorkLocAddress);
        }

        gvworkLocation.DataSource = dt;
        gvworkLocation.DataBind();

    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        Clear_field();
    }
    protected void gvworkLocation_SelectedIndexChanged(object sender, EventArgs e)
    {

        HRMS_WORK_LOCATION_MASTERTableAdapter work = new HRMS_WORK_LOCATION_MASTERTableAdapter();
        dsMasterData.HRMS_WORK_LOCATION_MASTERDataTable dtwork = new dsMasterData.HRMS_WORK_LOCATION_MASTERDataTable();

        lblmessage.Text = "";

        int indx = gvworkLocation.SelectedIndex;
        if (indx < 0) return;
        string officeid,worklocid;

        officeid = gvworkLocation.Rows[indx].Cells[1].Text;

        worklocid = gvworkLocation.Rows[indx].Cells[3].Text;
        dtwork = work.GetDataByOfficeIdWorkLocId(officeid, worklocid);

        ddlOfficeLocation.Text = dtwork[0].OfficeLocationId;

        txtWorkLocationId.Text = dtwork[0].WorkLocationId;
        txtWorklocationName.Text = dtwork[0].WorkLocationName;
        txtAddress.Text =dtwork[0].IsWorkLocAddressNull()?"":dtwork[0].WorkLocAddress.ToString();

    }
    protected void gvworkLocation_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[3].Visible = false;
    }
}
