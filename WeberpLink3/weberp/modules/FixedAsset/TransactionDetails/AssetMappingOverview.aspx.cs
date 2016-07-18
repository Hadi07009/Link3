using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibraryDAL;
using System.Windows.Forms;

public partial class modules_FixedAsset_TransactionDetails_AssetMappingOverview : System.Web.UI.Page
{
    string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadAssetMappingInformation();
            PanelForDetails.Visible = false;
        }
    }

    private void LoadAssetMappingInformation()
    {
        string _msg = null;
        try
        {
            var storedProcedureCommandTest = "exec [FAGetallFromInma_Itm_AccMapping] ";
            var dtAssetMapping = StoredProcedureExecutor.StoredProcedureExecuteReader(_connectionString, storedProcedureCommandTest);
            grdItemShow.DataSource = null;
            grdItemShow.DataBind();
            if (dtAssetMapping.Rows.Count > 0)
            {
                grdItemShow.DataSource = dtAssetMapping;
                grdItemShow.DataBind();
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
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearFieldData();
    }

    private void ClearFieldData()
    {
        txtItemName.Text = string.Empty;
        txtItemCode.Text = string.Empty;
        txtFaAcc.Text = string.Empty;
        txtDepAcc.Text = string.Empty;
        txtWriteoffAcc.Text = string.Empty;
        txtDispAcc.Text = string.Empty;
        txtRevAcc.Text = string.Empty;
        txtAccCode.Text = string.Empty;
        txtCogsAcc.Text = string.Empty;
        txtExpenseAcc.Text = string.Empty;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _msg = null;
        string itemCode = txtItemCode.Text.Trim() == string.Empty ? null : txtItemCode.Text.Trim() ;
        string itemAccountCode = txtAccCode.Text == string.Empty ? null : txtAccCode.Text;
        string faAcc = 	txtFaAcc.Text == string.Empty ? null : txtFaAcc.Text ;	
        string depAcc = txtDepAcc.Text == string.Empty ? null : txtDepAcc.Text;
        string writeoffCode = txtWriteoffAcc.Text == string.Empty ? null : txtWriteoffAcc.Text; 	
        string dispAcc	= txtDispAcc.Text == string.Empty ? null : txtDispAcc.Text;
        string revAcc = txtRevAcc.Text == string.Empty ? null : txtRevAcc.Text;
        string CogsAcc = txtCogsAcc.Text == string.Empty ? null : txtCogsAcc.Text;
        string ExpenseAcc = txtExpenseAcc.Text == string.Empty ? null : txtExpenseAcc.Text;
        try
        {
            var storedProcedureCommandText = "exec [FAUpdate_Inma_Itm_AccMapping] '" + itemCode + "','" + itemAccountCode + "','" + faAcc + "','" + depAcc + "','" + dispAcc + "','" + revAcc + "','" + writeoffCode + "','"+CogsAcc+"','"+ExpenseAcc+"'";
            StoredProcedureExecutor.StoredProcedureExecuteNonQuery(_connectionString, storedProcedureCommandText);
            ClearFieldData();
            LoadAssetMappingInformation();
            PanelForDetails.Visible = false;
            _msg = "Data Saved Successfully ";

        }
        catch (SqlException sqlError)
        {
            _msg = "Error Occured During Operation into Database, Data did not Save into Database !";
        }
        catch (Exception inSystemExep)
        {
            _msg = " Error Occured, Data did not Save into Database !";
        }
        finally
        {
            if (_msg != null)
            {
                ScriptManager.RegisterStartupScript(this,GetType(),"MessageBox","alert(' " + _msg + " ');",true);
            }
 
        }
    }
    
    protected void txtFaAcc_TextChanged(object sender, EventArgs e)
    {
        if (txtFaAcc.Text != string.Empty)
        {
            txtFaAcc.Text = txtFaAcc.Text.Split(':')[0].Trim() == "" ? "" : txtFaAcc.Text.Split(':')[0].Trim();
        }
    }
    protected void txtDepAcc_TextChanged(object sender, EventArgs e)
    {
        if (txtDepAcc.Text != string.Empty)
        {
            txtDepAcc.Text = txtDepAcc.Text.Split(':')[0].Trim() == "" ? "" : txtDepAcc.Text.Split(':')[0].Trim();
        }

    }
    
    protected void txtDispAcc_TextChanged(object sender, EventArgs e)
    {
        if (txtDispAcc.Text != string.Empty)
        {
            txtDispAcc.Text = txtDispAcc.Text.Split(':')[0].Trim() == "" ? "" : txtDispAcc.Text.Split(':')[0].Trim();
        }

    }
    protected void txtRevAcc_TextChanged(object sender, EventArgs e)
    {
        if (txtRevAcc.Text != string.Empty)
        {
            txtRevAcc.Text = txtRevAcc.Text.Split(':')[0].Trim() == "" ? "" : txtRevAcc.Text.Split(':')[0].Trim();
        }

    }
    
    protected void txtAccCode_TextChanged(object sender, EventArgs e)
    {
        if (txtAccCode.Text != string.Empty)
        {
            txtAccCode.Text = txtAccCode.Text.Split(':')[0].Trim() == "" ? "" : txtAccCode.Text.Split(':')[0].Trim();
        }

    }
    protected void grdItemShow_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        if (e.CommandName.Equals("Select"))
        {
            txtItemCode.Text = grdItemShow.Rows[selectedIndex].Cells[1].Text;
            txtItemName.Text = grdItemShow.Rows[selectedIndex].Cells[2].Text == "&nbsp;" ? string.Empty : grdItemShow.Rows[selectedIndex].Cells[2].Text;
            txtAccCode.Text = grdItemShow.Rows[selectedIndex].Cells[3].Text == "&nbsp;" ? string.Empty : grdItemShow.Rows[selectedIndex].Cells[3].Text;
            txtFaAcc.Text = grdItemShow.Rows[selectedIndex].Cells[4].Text == "&nbsp;" ? string.Empty : grdItemShow.Rows[selectedIndex].Cells[4].Text;
            txtDepAcc.Text = grdItemShow.Rows[selectedIndex].Cells[5].Text == "&nbsp;" ? string.Empty : grdItemShow.Rows[selectedIndex].Cells[5].Text;
            txtDispAcc.Text = grdItemShow.Rows[selectedIndex].Cells[7].Text == "&nbsp;" ? string.Empty : grdItemShow.Rows[selectedIndex].Cells[7].Text;
            txtRevAcc.Text = grdItemShow.Rows[selectedIndex].Cells[6].Text == "&nbsp;" ? string.Empty : grdItemShow.Rows[selectedIndex].Cells[6].Text;
            txtWriteoffAcc.Text = grdItemShow.Rows[selectedIndex].Cells[8].Text == "&nbsp;" ? string.Empty : grdItemShow.Rows[selectedIndex].Cells[8].Text;
            txtCogsAcc.Text = grdItemShow.Rows[selectedIndex].Cells[9].Text == "&nbsp;" ? string.Empty : grdItemShow.Rows[selectedIndex].Cells[9].Text;
            txtExpenseAcc.Text = grdItemShow.Rows[selectedIndex].Cells[10].Text == "&nbsp;" ? string.Empty : grdItemShow.Rows[selectedIndex].Cells[10].Text; 
            PanelForDetails.Visible = true;
        }
    }
    protected void txtWriteoffAcc_TextChanged(object sender, EventArgs e)
    {
        if (txtWriteoffAcc.Text != string.Empty)
        {
            txtWriteoffAcc.Text = txtWriteoffAcc.Text.Split(':')[0].Trim() == "" ? "" : txtWriteoffAcc.Text.Split(':')[0].Trim();
        }

    }
    protected void txtCogsAcc_TextChanged(object sender, EventArgs e)
    {
        if (txtCogsAcc.Text != string.Empty)
        {
            txtCogsAcc.Text = txtCogsAcc.Text.Split(':')[0].Trim() == "" ? "" : txtCogsAcc.Text.Split(':')[0].Trim();
        }

    }
    protected void txtExpenseAcc_TextChanged(object sender, EventArgs e)
    {
        if (txtExpenseAcc.Text != string.Empty)
        {
            txtExpenseAcc.Text = txtExpenseAcc.Text.Split(':')[0].Trim() == "" ? "" : txtExpenseAcc.Text.Split(':')[0].Trim();
        }

    }
}
