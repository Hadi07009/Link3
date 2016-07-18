using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_Setup_TestRichTextBox2 : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection("Data Source=(local);Initial Catalog=SSP;User Id=sa; Password=;");
    string ConnectionStr = ConfigurationSettings.AppSettings["SCFConnectionString"].Replace("SCF", "SSP");
    protected void Page_Load(object sender, EventArgs e)
    {
        BindGridview();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //con.Open();
        //SqlCommand cmd = new SqlCommand("insert into RichTextBoxData(RichTextData) values(@Richtextbox)", con);
        //cmd.Parameters.AddWithValue("@Richtextbox", FreeTextBox1.Text);
        //cmd.ExecuteNonQuery();
        //con.Close();
        //FreeTextBox1.Text = "";
        //BindGridview();


        //SqlConnection con = null;
        //con = new SqlConnection(ConnectionStr.ToString());
        //SqlCommand cmd = new SqlCommand();
        //cmd.CommandText = "insert into RichTextBoxData(RichTextData) values(@Richtextbox)";
        //cmd.Connection = con;
        //cmd.Parameters.AddWithValue("@Richtextbox", FreeTextBox1.Text);
        ////cmd.Parameters.AddWithValue("@CompanyId", companyID);
        //con.Open();
        //cmd.ExecuteNonQuery();
        //con.Close();

        //cmd.Parameters.Add("@binaryValue", SqlDbType.VarBinary, 8000).Value = arraytoinsert;

        SqlConnection con = null;
        con = new SqlConnection(ConnectionStr.ToString());
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "insert into RichTextBoxData(RichTextData) values(@Richtextbox)";
        cmd.Connection = con;
        //cmd.Parameters.AddWithValue("@Richtextbox", Editor1.Content.ToString());
        cmd.Parameters.Add("@Richtextbox", SqlDbType.VarChar, 8000).Value = Editor1.Content.ToString();
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        BindGridview();
        Editor1.Content = string.Empty;


        //SqlConnection con = null;
        //con = new SqlConnection(ConnectionStr.ToString());
        //SqlCommand cmd = new SqlCommand();
        //cmd.CommandText = "insert into RichTextBoxData(RichTextData) values(@Richtextbox)";
        //cmd.Connection = con;
        //cmd.Parameters.AddWithValue("@Richtextbox", Editor1.Content.ToString());
        //con.Open();
        //cmd.ExecuteNonQuery();
        //con.Close();
    }

    protected void BindGridview()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("select RichTextData from RichTextBoxData", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();
        da.Fill(ds);
        GridView1.DataSource = ds;
        GridView1.DataBind();
        DataTable dt = DataProcess.GetData(cmd, "select RichTextData from RichTextBoxData");
        if(dt.Rows.Count>0)
            tdrich.InnerHtml = dt.Rows[0]["RichTextData"].ToString();


        con.Close();

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
        var lbltxt = ((Label)GridView1.Rows[selectedIndex].FindControl("lbltxt")).Text;
        if (e.CommandName.Equals("Select"))
        {
            Editor1.Content = lbltxt;
 
        }
    }
}