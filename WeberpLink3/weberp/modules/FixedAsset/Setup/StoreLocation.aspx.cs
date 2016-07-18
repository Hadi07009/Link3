using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_FixedAsset_Setup_StoreLocation : System.Web.UI.Page
{
    string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadStoreLocation();
            
        }
    }

    private void LoadStoreLocation()
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [storeLocationGet_InMa_Str_Loc] ";
            var dtStoreLocation = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureCommandTest);
            grdStoreLocation.DataSource = null;
            grdStoreLocation.DataBind();
            if (dtStoreLocation.Rows.Count > 0)
            {
                grdStoreLocation.DataSource = dtStoreLocation;
                grdStoreLocation.DataBind();
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

    private string CheckAllValidation()
    {
        const string checkValidation = "";
        if (txtStoreLocationID.Text == string.Empty)
        {
            txtStoreLocationID.Focus();
            return "Please enter store location ID  correctly !";
        }
        if (txtStoreLocationName.Text == string.Empty)
        {
            txtStoreLocationName.Focus();
            return "Please enter store Location name correctly !";
        }
        var dtStoreLocationName = DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.CheckStoreLocationName(txtStoreLocationName.Text));
        if (dtStoreLocationName.Rows.Count > 0)
        {
            txtStoreLocationName.Focus();
            return "This store location name already exist";
        }
        if (btnSave.Text == "Save")
        {
            var dtStoreLocationID =  DataProcess.GetData(_connectionString, SqlgenerateForFixedAsset.CheckStoreLocationID(txtStoreLocationID.Text));
            if (dtStoreLocationID.Rows.Count > 0)
            {
                txtStoreLocationID.Focus();
                return "This store location ID already exist";
            }

        }
        return checkValidation;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string validationMsg = CheckAllValidation();
        switch (validationMsg)
        {
            case "":
                {

                    string msg = SaveStoreLocation();
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

    private string SaveStoreLocation()
    {
        string _msg;
        try
        {
            StoreLocation objStoreLocation = new StoreLocation();
            objStoreLocation.StoreLocationID = txtStoreLocationID.Text;
            objStoreLocation.StoreLocationName = txtStoreLocationName.Text;
            var storedProcedureComandTest = "exec [storeLocationInitiate_InMa_Str_Loc] '" + objStoreLocation.StoreLocationID + "','" + objStoreLocation.StoreLocationName + "'";
            StoredProcedureExecutor.StoredProcedureExecuteNonQuery(_connectionString, storedProcedureComandTest);
            _msg = "Data Saved Successfully ";
            LoadStoreLocation();
            ClearAllControl();
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

    protected void grdStoreLocation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        string lblStoreLocationId = ((Label)grdStoreLocation.Rows[selectedIndex].FindControl("lblStoreLocationId")).Text;
        if (e.CommandName.Equals("Delete"))
        {
            string _msg = null;
            try
            {
                DataProcess.DeleteQuery(_connectionString, SqlgenerateForFixedAsset.DeleteStoreLocation(lblStoreLocationId));
                LoadStoreLocation();
            }
            catch (SqlException sqlError)
            {
                _msg = "  Error Occured During Operation into Database, Data did not Delete from Database !  ";

            }
            catch (Exception inSystemExep)
            {
                _msg = " Error Occured, Data did not Delete from Database  ! ";

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
        else if (e.CommandName.Equals("Select"))
        {
            string lblStoreLocationName = ((Label)grdStoreLocation.Rows[selectedIndex].FindControl("lblStoreLocationName")).Text;
            txtStoreLocationID.Text = lblStoreLocationId;
            txtStoreLocationName.Text = lblStoreLocationName;
            btnSave.Text = "Update";
            txtStoreLocationID.Enabled = false;
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAllControl();
    }

    private void ClearAllControl()
    {
        txtStoreLocationID.Text = string.Empty;
        txtStoreLocationName.Text = string.Empty;
        txtStoreLocationID.Enabled = true;
        btnSave.Text = "Save";
    }
    protected void grdStoreLocation_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}