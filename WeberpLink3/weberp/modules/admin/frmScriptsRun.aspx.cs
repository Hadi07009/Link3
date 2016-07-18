using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;

public partial class modules_admin_frmScriptsRun : System.Web.UI.Page
{
    string connstr = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();    

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_reader_Click(object sender, EventArgs e)
    {
        gdata.DataSource = ExecuteReader(connstr, txtqry.Text);
        gdata.DataBind();

    }

    protected void btn_nonqry_Click(object sender, EventArgs e)
    {              
        string userid =current.UserId.ToString().ToLower();
        string hour = string.Format("{0:00}", System.DateTime.Now.Hour);
        string dd =string.Format("{0:00}",System.DateTime.Now.Day);
        string mm = string.Format("{0:00}", System.DateTime.Now.Month);
        string yyyy = string.Format("{0:0000}",System.DateTime.Now.Year);

        string passkey = userid + hour + dd + mm + yyyy;

        if (passkey != txtSecurity.Text.Trim())
        {
            MessageBox1.ShowSuccess("Security code has not matched");
            return; 
        }

        lbloutput.Text = ExecuteNonQuery(connstr, txtqry.Text);

        MessageBox1.ShowSuccess("Scripts executed successfull");

    }

    private string ExecuteNonQuery(string ConnectionString, string QueryStr)
    {
        SqlConnection connection = new SqlConnection(ConnectionString);
        int num = 0;
        try
        {
            connection.Open();
            num = new SqlCommand(QueryStr, connection).ExecuteNonQuery();
        }
        catch (Exception exception)
        {
            return exception.Message;
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        return num.ToString();
    }

    private DataTable ExecuteReader(string ConnectionString, string QueryStr)
    {
        SqlConnection selectConnection = new SqlConnection(ConnectionString);
        DataTable dataTable = new DataTable();
        try
        {
            string selectCommandText = QueryStr;
            selectConnection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(selectCommandText, selectConnection);
            dataTable = new DataTable();
            adapter.Fill(dataTable);
        }
        catch (SqlException exception)
        {
            return null;
        }
        catch (Exception exception2)
        {
            return null;
        }
        finally
        {
            if (selectConnection.State == ConnectionState.Open)
            {
                selectConnection.Close();
            }
        }
        return dataTable;
    }


    
}