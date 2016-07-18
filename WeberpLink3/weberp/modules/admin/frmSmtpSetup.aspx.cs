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
using System.Net;
using System.Net.NetworkInformation;

public partial class frmSmtpSetup : System.Web.UI.Page
{
    string ConnectionStr = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);
        if(!IsPostBack)
        {
            if (!Page.IsPostBack)
            {          
                LoadMailTypeID();
                Load_grid();
            }
        }
        
    }

    private string check_entry()
    {

        if (ddlmailtypeid.Text == "") return "Please select Mail Type";
        if (txtmailfrom.Text == "") return "Please enter Mail From";
        if (txtmailaddress.Text == "") return "Please enter Mail Address";
        if (txtpassword.Text == "") return "Please enter Password";
        if (txtsmtp.Text == "") return "Please enter SMTP";
        if (txtsubject.Text == "") return "Please enter Mail Subject";
        if (txtbody.Text == "") return "Please enter Message";
        if (ddlstatus.Text == "") return "Please select Status";


        return "";
    
    }

    private void Clear_field()
    {
        ddlmailtypeid.Text = "";
        txtmailfrom.Text = "";
        txtmailaddress.Text = "";
        txtpassword.Text = "";
        txtsmtp.Text = "";
        txtbody.Text = "";
        txtsubject .Text ="";
        ddlstatus.SelectedIndex = 0;
        lblmessage.Text = "";

    }


    public void LoadMailTypeID()
    {
        TblMailTypeTableAdapter mail = new TblMailTypeTableAdapter();
        LibraryPF.dsMasterData.TblMailTypeDataTable dtmail = new LibraryPF.dsMasterData.TblMailTypeDataTable();
        dtmail = mail.GetDataAll();

        ddlmailtypeid.Items.Clear();
        ddlmailtypeid.Items.Add("");
        foreach (LibraryPF.dsMasterData.TblMailTypeRow dr in dtmail.Rows)
        {
            ListItem lst = new ListItem();
            lst.Value = dr.MailTypeId.ToString();
            lst.Text = dr.MailTypeName.ToString();
            ddlmailtypeid.Items.Add(lst);
        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        TblSmtpSetupTableAdapter smtp = new TblSmtpSetupTableAdapter();
        dsMasterData.TblSmtpSetupDataTable dtsmtp = new dsMasterData.TblSmtpSetupDataTable();
        

        if (check_entry() != "")
        {
            lblmessage.Text = check_entry();
            lblmessage.ForeColor = System.Drawing.Color.Red;
            return;

        }

        dtsmtp = smtp.GetDataByMailType(Convert.ToInt32(ddlmailtypeid.SelectedItem.Value));


        try
        {
            if (dtsmtp.Rows.Count > 0)
            {

                smtp.UpdateSmtpSetup(txtmailaddress.Text, txtmailfrom.Text, txtpassword.Text, txtsmtp.Text, txtsubject.Text.Trim(), txtbody.Text.Trim(), Convert.ToInt32(ddlstatus.SelectedItem.Value), Convert.ToInt32(ddlmailtypeid.SelectedItem.Value));

            }
            else
            {
                smtp.InsertSmtpSetup(Convert.ToInt32(ddlmailtypeid.SelectedItem.Value), txtmailaddress.Text, txtmailfrom.Text, txtpassword.Text, txtsmtp.Text, txtsubject.Text.Trim(), txtbody.Text.Trim(), Convert.ToInt32(ddlstatus.SelectedItem.Value));

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
        string status;

        GetSmtpdataTableAdapter smtp = new GetSmtpdataTableAdapter();
        dsMasterData.GetSmtpdataDataTable dtsmtp = new dsMasterData.GetSmtpdataDataTable();
        dtsmtp = smtp.GetDataAll();


        dt.Columns.Add("Mail Type ID", typeof(string));
        dt.Columns.Add("Mail Type Name", typeof(string));
        dt.Columns.Add("Mail Adrress", typeof(string));
        dt.Columns.Add("Mail From", typeof(string));    
        dt.Columns.Add("SMTP", typeof(string));
        dt.Columns.Add("Status", typeof(string));

        
        foreach (dsMasterData.GetSmtpdataRow dr in dtsmtp.Rows)
        {
            if (dr.status == 1)
            {
                status = "Active";
            }
            else
            {
                status = "In Active";
            }

            dt.Rows.Add(dr.MailTypeId, dr.MailTypeName, dr.MailFrom, dr.Name, dr.Smtp, status);
        }

        gvsmtp.DataSource = dt;
        gvsmtp.DataBind();


    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        Clear_field();
    }
    protected void gvsmtp_SelectedIndexChanged(object sender, EventArgs e)
    {

        TblSmtpSetupTableAdapter smtp = new TblSmtpSetupTableAdapter();
        dsMasterData.TblSmtpSetupDataTable dtsmtp = new dsMasterData.TblSmtpSetupDataTable();
        lblmessage.Text = "";


        int indx = gvsmtp.SelectedIndex;
        if (indx < 0) return;
        int mailtype;

        mailtype = Convert.ToInt32(gvsmtp.Rows[indx].Cells[1].Text);
        dtsmtp = smtp.GetDataByMailType(mailtype);

        ddlmailtypeid.Text = dtsmtp[0].MailTypeId.ToString();
        txtmailfrom.Text = dtsmtp[0].Name ;
        txtmailaddress.Text = dtsmtp[0].MailFrom;
        txtpassword.Text = dtsmtp[0].Password;
        txtsubject.Text = dtsmtp[0].Subject;
        txtsmtp.Text = dtsmtp[0].Smtp;
        txtbody.Text = dtsmtp[0].Body;
        ddlstatus.Text = dtsmtp[0].status.ToString();

    }
    protected void btnTestMail_Click(object sender, EventArgs e)
    {
        string user = "";
        try
        {         
           
            string msgbody = "Test Mail. Please ignore it";
            string sid ="n.islam@link3.net";
            string sname ="Nazrul ISlam";
            string rid = txtMailTo.Text.Trim();
            string rname = "Test";
            string msub = "Test";
            string mbody = msgbody;
            string ccid ="";
            if (Sendmail(sid, sname, rid, rname, ccid, msub, mbody))
                MessageBox1.ShowSuccess("Mail Send Successful");

            txtMailTo.Text = "";

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private bool Sendmail(string sid, string sname, string rid, string rname, string ccid, string msub, string mbody)
    {
        bool flg = false;
        try
        {
            string smtpadr = "";
            string passkey = "";
            DataTable dt = getSmtp();
            if (dt.Rows.Count > 0)
            {
                smtpadr = dt.Rows[0]["Smtp"].ToString();
                sid = dt.Rows[0]["MailFrom"].ToString();
                sname = dt.Rows[0]["Name"].ToString();
                passkey = dt.Rows[0]["Password"].ToString();

            }

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(smtpadr);
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.From = new System.Net.Mail.MailAddress(sid, sname);

            NetworkCredential Credentials = new NetworkCredential(sid, passkey);
            smtp.Credentials = Credentials;


            msg.To.Add(new System.Net.Mail.MailAddress(rid));
            string[] cary = ccid.Split(':');
            if (cary.Length >= 1)
            {
                for (int i = 0; i < cary.Length; i++)
                {
                    if (cary[i].Trim() != "")
                    {
                        msg.CC.Add(new System.Net.Mail.MailAddress(cary[i].Trim()));
                    }
                }
            }
            msg.Subject = msub;
            msg.Body = mbody;
            smtp.Send(msg);
            flg = true;
        }
        catch
        {
        }
        return flg;
    }




    private DataTable getSmtp()
    {
        DataTable dt = new DataTable();
        string sql = "select MailFrom,Name,[Password],Smtp from TblSmtpSetup where MailTypeId=1 and status=1";
        dt = DataProcess.GetData(ConnectionStr, sql);
        return dt;
    }

}
