using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


public partial class usercontrols_ucSelfService : System.Web.UI.UserControl
{
    string _connectionString = ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SelfServiceLoad();
        }
    }

    private void SelfServiceLoad()
    {
        DataTable dt = new DataTable();
        string sql = "select * from [tblInbox] where TaskType='SelfService' and Status=1 order by ID";
        dt = DataProcess.GetData(_connectionString, sql);
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["ID"].ToString() == "1")
            {
                LinkButton1.Text = ">>"+dr["TaskName"].ToString();
                LinkButton1.PostBackUrl = dr["URL"].ToString();
            }
            else if (dr["ID"].ToString() == "2")
            {
                LinkButton2.Text = ">>" + dr["TaskName"].ToString();
                LinkButton2.PostBackUrl = dr["URL"].ToString();
            }
            else if (dr["ID"].ToString() == "3")
            {
                LinkButton3.Text = ">>" + dr["TaskName"].ToString();
                LinkButton3.PostBackUrl = dr["URL"].ToString();
            }
            else if (dr["ID"].ToString() == "4")
            {
                LinkButton4.Text = ">>" + dr["TaskName"].ToString();
                LinkButton4.PostBackUrl = dr["URL"].ToString();
            }
            else if (dr["ID"].ToString() == "5")
            {
                LinkButton5.Text = ">>" + dr["TaskName"].ToString();
                LinkButton5.PostBackUrl = dr["URL"].ToString();
            }
            else if (dr["ID"].ToString() == "6")
            {
                LinkButton6.Text = ">>" + dr["TaskName"].ToString();
                LinkButton6.PostBackUrl = dr["URL"].ToString();
            }
            else if (dr["ID"].ToString() == "7")
            {
                LinkButton7.Text = ">>" + dr["TaskName"].ToString();
                LinkButton7.PostBackUrl = dr["URL"].ToString();
            }
            
        }       
 
    }
    
}