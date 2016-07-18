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
public partial class frmMailTypeSetup : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);
        if(!IsPostBack)
        {
            if (!Page.IsPostBack)
            {          
               
                Load_grid();
                txtMailTypeId.Text = MaxID().ToString();
            }
        }
        
    }

    private int MaxID()
    {
        int maxid;
        TblMailTypeTableAdapter mail = new TblMailTypeTableAdapter();
        return maxid = Convert.ToInt32(mail.MaxMailTypeID()) + 1;


    }

    private string check_entry()
    {


        if (txtMailTypeId.Text == "") return "Please enter Mail Type Id";
        if (txtMailTypeName.Text == "") return "Please enter Mail Type Name";
       

        return "";
    
    }

    private void Clear_field()
    {
        txtMailTypeId.Text = "";
        txtMailTypeName.Text = "";
        lblmessage.Text = "";
        txtMailTypeId.Text = MaxID().ToString();

    }


  

    protected void btnsave_Click(object sender, EventArgs e)
    {
        TblMailTypeTableAdapter mail = new TblMailTypeTableAdapter();
        LibraryPF.dsMasterData.TblMailTypeDataTable dtmail = new LibraryPF.dsMasterData.TblMailTypeDataTable();
        

        if (check_entry() != "")
        {
            lblmessage.Text = check_entry();
            lblmessage.ForeColor = System.Drawing.Color.Red;
            return;

        }

        dtmail = mail.GetDataByMailTypeId(Convert.ToInt32(txtMailTypeId.Text));


        try
        {
            if (dtmail.Rows.Count > 0)
            {

                mail.UpdateMailType(txtMailTypeName.Text,Convert.ToInt32(txtMailTypeId.Text));

            }
            else
            {
                mail.InsertMailType(Convert.ToInt32(txtMailTypeId.Text),txtMailTypeName.Text);

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
       
        TblMailTypeTableAdapter mail = new TblMailTypeTableAdapter();
        LibraryPF.dsMasterData.TblMailTypeDataTable dtmail = new LibraryPF.dsMasterData.TblMailTypeDataTable();

        dtmail = mail.GetDataAll();

        dt.Columns.Add("Mail Type ID", typeof(string));
        dt.Columns.Add("Mail Type Name", typeof(string));

        foreach (dsMasterData.TblMailTypeRow dr in dtmail.Rows)
        {

            dt.Rows.Add(dr.MailTypeId, dr.MailTypeName);
        }

        gvmail.DataSource = dt;
        gvmail.DataBind();


    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        Clear_field();
    }
    protected void gvmail_SelectedIndexChanged(object sender, EventArgs e)
    {

        TblMailTypeTableAdapter mail = new TblMailTypeTableAdapter();
        LibraryPF.dsMasterData.TblMailTypeDataTable dtmail = new LibraryPF.dsMasterData.TblMailTypeDataTable();

        lblmessage.Text = "";

        int indx = gvmail.SelectedIndex;
        if (indx < 0) return;
        int mailtype;

        mailtype = Convert.ToInt32(gvmail.Rows[indx].Cells[1].Text);
        dtmail = mail.GetDataByMailTypeId(mailtype);

        txtMailTypeId.Text = dtmail[0].MailTypeId.ToString();
        txtMailTypeName.Text = dtmail[0].MailTypeName;
      
    }
}
