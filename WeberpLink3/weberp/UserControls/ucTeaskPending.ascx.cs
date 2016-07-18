using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


public partial class usercontrols_ucTeaskPending : System.Web.UI.UserControl
{
    string _connectionString = ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TaskPendingLoad();
        }
    }

    private void TaskPendingLoad()
    {
        
        Session["ApplicantID"] = Session[StaticData.sessionUserId].ToString();
        DataTable dtpending = new DataTable();
        ProcessCount procobj = new ProcessCount();
        dtpending = procobj.GetPendingApplicationCount(Session["ApplicantID"].ToString(), "", "", _connectionString);

        DataTable dt = new DataTable();
        string sql = "select * from [tblInbox] where TaskType='TaskPending' and Status=1 order by ID";
        dt = DataProcess.GetData(_connectionString, sql);
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["ID"].ToString() == "1")
            {
                LinkButton1.Text = ">>"+dr["TaskName"].ToString() + "("+GetTaskCount(dr["ProcessID"].ToString(),dtpending).ToString()+")";
                LinkButton1.PostBackUrl = dr["URL"].ToString();
            }
            else if (dr["ID"].ToString() == "2")
            {
                LinkButton2.Text = ">>" + dr["TaskName"].ToString() + "(" + GetTaskCount(dr["ProcessID"].ToString(), dtpending).ToString() + ")";
                LinkButton2.PostBackUrl = dr["URL"].ToString();
            }
            else if (dr["ID"].ToString() == "3")
            {
                LinkButton3.Text = ">>" + dr["TaskName"].ToString() + "(" + GetTaskCount(dr["ProcessID"].ToString(), dtpending).ToString() + ")";
                LinkButton3.PostBackUrl = dr["URL"].ToString();
            }
            else if (dr["ID"].ToString() == "4")
            {
                LinkButton3.Text = ">>" + dr["TaskName"].ToString() + "(" + GetTaskCount(dr["ProcessID"].ToString(), dtpending).ToString() + ")";
                LinkButton3.PostBackUrl = dr["URL"].ToString();
            }
            else if (dr["ID"].ToString() == "5")
            {
                LinkButton3.Text = ">>" + dr["TaskName"].ToString() + "(" + GetTaskCount(dr["ProcessID"].ToString(), dtpending).ToString() + ")";
                LinkButton3.PostBackUrl = dr["URL"].ToString();
            }
            
        }       
 
    }

    private int GetTaskCount(string processid, DataTable dtpending)
    {      
        int TP=0;
        foreach (DataRow drr in dtpending.Rows)
        {
            if (drr["ProcessId"].ToString() == processid)
            {
                TP = Convert.ToInt32(drr["nooftask"].ToString()); 
            }
        }

        return TP; 
    }    
}