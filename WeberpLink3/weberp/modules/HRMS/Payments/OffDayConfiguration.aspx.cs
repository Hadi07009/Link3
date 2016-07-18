using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

public partial class modules_HRMS_Payments_OffDayConfiguration : System.Web.UI.Page
{
    string constr = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            LoadDepartment();
            LoadDesignation("");
            
            ListItem lst=new ListItem();
            lst.Text = "Off Day";
            lst.Value = "OF";
            RadioButtonList1.Items.Add(lst);
            
            ListItem lst1 = new ListItem();
            lst1.Text = "Over Time";
            lst1.Value = "OT";
            RadioButtonList1.Items.Add(lst1);

            ListItem lst2 = new ListItem();
            lst2.Text = "Night";
            lst2.Value = "NT";
            RadioButtonList1.Items.Add(lst2);
        }
    }

    private void LoadDepartment()
    {
        DataTable dt = DataProcess.GetData(constr, Sqlgenerate.SqlGetDepartment());
        gdvDept.DataSource = null;
        gdvDept.DataBind();
        if(dt.Rows.Count > 0)
        {
            gdvDept.DataSource = dt;
            gdvDept.DataBind();
        }
    }


    private void LoadDesignation(string Deptcode)
    {
        DataTable dt = new DataTable();
        dt = DataProcess.GetData(constr, Sqlgenerate.GetDatabyDepartmentCode(Deptcode));
        gdvDesignation.DataSource = null;
        gdvDesignation.DataBind();
        Panel4.Visible = false;
        if(dt.Rows.Count > 0)
        {
            gdvDesignation.DataSource = dt;
            gdvDesignation.DataBind();
            Panel4.Visible = true;
        }
    }
    
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedIndex == -1) return;
      
        SaveConfiguration();
    }

    private void SaveConfiguration()
    {
        if (RadioButtonList1.SelectedItem.Selected==false)
        {
            Label2.Text = "please Select Configure Type";
            return;
        }
        
        string configureType = RadioButtonList1.SelectedItem.Value;
        for (int i = 0; i < gdvDept.Rows.Count; i++)
        {
            DataTable dt = new DataTable();
            string deptcode = "";
            CheckBox Chk = (CheckBox)gdvDept.Rows[i].Cells[1].FindControl("CheckBox1");
            string desigvalue = "";
            if (Chk != null)
            {
                if (Chk.Checked == true)
                {
                    deptcode = gdvDept.Rows[i].Cells[1].Text.Replace("&amp;","&");
                    DataProcess.DeleteQuery(constr, Sqlgenerate.DeleteDataByConfigureType(configureType, deptcode));
                    for (int j = 0; j < gdvDesignation.Rows.Count; j++)
                    {
                        CheckBox Chkdesignation = (CheckBox)gdvDesignation.Rows[j].Cells[1].FindControl("CheckBox1");
                        if (Chkdesignation != null)
                        {
                            if (Chkdesignation.Checked == true)
                            {
                                if ((((TextBox)(gdvDesignation.Rows[j].Cells[3].FindControl("txtRate"))).Text) == "")
                                {
                                    Label2.Text = "please enter rate";
                                    Label2.Visible = true;
                                    return;
                                }
                                
                                decimal rate = Convert.ToDecimal(((TextBox)(gdvDesignation.Rows[j].Cells[3].FindControl("txtRate"))).Text);
                                desigvalue = gdvDesignation.Rows[j].Cells[1].Text.Replace("&amp;","&");
                                DataProcess.InsertQuery(constr, Sqlgenerate.InsertAdditionalConfigure(configureType, deptcode, desigvalue, Convert.ToDecimal(rate), DateTime.Now, DateTime.Now, "Mos", DateTime.Now, "Mos", DateTime.Now));
                                Chkdesignation.Checked = false;
                                Chk.Checked = false;
                                (((TextBox)(gdvDesignation.Rows[j].Cells[3].FindControl("txtRate"))).Text) = "";
                            }
                        }
                    }
                }
            }
        }
        Label2.Text = "Configuration Saved Successfully";
        Label2.Visible = true;
    }

    protected void gdvDept_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells.Count > 0)
        {
            e.Row.Cells[1].Visible = false;
        }
    }

    protected void gdvDesignation_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
    }
   
    protected void btnShowDesig_Click(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedIndex == -1) return;

        ShowData();
    }

    private void ShowData()
    {
        string ctype = "";
        ctype = RadioButtonList1.SelectedItem.Value;
        for (int j = 0; j < gdvDesignation.Rows.Count - 1; j++)
        {
            CheckBox Chkdesignation = (CheckBox)gdvDesignation.Rows[j].Cells[1].FindControl("CheckBox1");
            Chkdesignation.Checked = false;
            (((TextBox)(gdvDesignation.Rows[j].Cells[3].FindControl("txtRate"))).Text) = "";
        }
        DataTable dtAddition = new DataTable();
        for (int i = 0; gdvDept.Rows.Count > i; i++)
        {
            CheckBox Chk = (CheckBox)gdvDept.Rows[i].Cells[1].FindControl("CheckBox1");
            if (Chk.Checked == true)
            {
                string value = gdvDept.Rows[i].Cells[1].Text.Replace("&amp;","&");
                dtAddition = DataProcess.GetData(constr, Sqlgenerate.GetDataByConfigureType(ctype, value)); 
                if (dtAddition.Rows.Count == 0)
                {
                    for (int j = 0; j < gdvDesignation.Rows.Count; j++)
                    {
                        CheckBox Chkdesig = (CheckBox)gdvDesignation.Rows[j].Cells[2].FindControl("CheckBox1");
                        Chkdesig.Checked = false;
                    }
                }
                else
                {
                    for (int m = 0; m < dtAddition.Rows.Count; m++)
                    {
                        for (int j = 0; j < gdvDesignation.Rows.Count; j++)
                        {
                            if (dtAddition.Rows[m].ItemArray[2].ToString() == gdvDesignation.Rows[j].Cells[1].Text)
                            {
                                CheckBox Chkdesig = (CheckBox)gdvDesignation.Rows[j].Cells[2].FindControl("CheckBox1");
                                Chkdesig.Checked = true;
                                double rate = Convert.ToDouble(dtAddition.Rows[m].ItemArray[3].ToString());
                                (((TextBox)(gdvDesignation.Rows[j].Cells[3].FindControl("txtRate"))).Text) = (rate).ToString();
                            }
                        }
                    }
                    return;
                }
            }
            else
            {
                for (int j = 0; j < gdvDesignation.Rows.Count; j++)
                {
                    CheckBox Chkdesig = (CheckBox)gdvDesignation.Rows[j].Cells[2].FindControl("CheckBox1");
                    Chkdesig.Checked = false;
                }
            }
        }
    }


    protected void btnShowDesigbyDept_Click(object sender, EventArgs e)
    {

        if (RadioButtonList1.SelectedIndex == -1) return;
        
        string value = ""; 
        for (int i = 0; gdvDept.Rows.Count > i; i++)
        {
            CheckBox Chk = (CheckBox)gdvDept.Rows[i].Cells[1].FindControl("CheckBox1");
            if (Chk.Checked == true)
            {
                value = gdvDept.Rows[i].Cells[1].Text.Replace("&amp;", "&");
            }
        }
        LoadDesignation(value);
    }

    private string CheckValidation()
    {
        const string validationMSG = "";
        if (RadioButtonList1.SelectedIndex == -1)
        {
            foreach (GridViewRow rowNumber in gdvDept.Rows)
            {
                CheckBox Chk = (CheckBox)gdvDept.Rows[rowNumber.RowIndex].Cells[1].FindControl("CheckBox1");
                Chk.Checked = false;
            }
            RadioButtonList1.Focus();
            return "Please Select Off Day or Over Time or Night !";
        }
        return validationMSG;
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        string validationMsg = CheckValidation();

        switch (validationMsg)
        {
            case "":
                {
                    try
                    {
                        var rowIndex = ((GridViewRow)((Control)sender).NamingContainer).RowIndex;
                        foreach (GridViewRow rowNumber in gdvDept.Rows)
                        {
                            CheckBox Chk = (CheckBox)gdvDept.Rows[rowNumber.RowIndex].Cells[1].FindControl("CheckBox1");
                            Chk.Checked = false;
                        }
                        CheckBox selectedChk = (CheckBox)gdvDept.Rows[rowIndex].Cells[1].FindControl("CheckBox1");
                        selectedChk.Checked = true;

                        LabelDepartment.Text = RadioButtonList1.SelectedItem.Text+" configuration of "+ gdvDept.Rows[rowIndex].Cells[0].Text;

                        btnShowDesigbyDept_Click(sender, e);
                        btnShowDesig_Click(sender, e);

                    }
                    catch (Exception inSystemExep)
                    {

                        ScriptManager.RegisterStartupScript(this,GetType(), "MessageBox", "alert(' Error Occured, Data did not Find into Database  ! ');",true);
                    }
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

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow rowNumber in gdvDept.Rows)
        {
            CheckBox Chk = (CheckBox)gdvDept.Rows[rowNumber.RowIndex].Cells[1].FindControl("CheckBox1");
            Chk.Checked = false;
        }
        LoadDesignation("");
        LabelDepartment.Text = RadioButtonList1.SelectedItem.Text + " configuration";
    }
}