using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using DataAccess;


public partial class usercontrols_ucLeaveDocument : System.Web.UI.UserControl
{
    string _connectionString = ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    string filepath = System.Configuration.ConfigurationSettings.AppSettings["upl"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void gdvFileLoad_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
        }
        e.Row.Cells[2].Visible = false;
    }
    public void LoadUploadFileByRef(string referenceNo)
    {
        string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
        DataTable dt = new DataTable();
        string sql = @"select 1 as SL,B.documentContent as [FileName], A.documentCode as [SavedFileName] from HRMS_Emp_LeaveDocument A
                     inner join [HRMS_Document] B on A.documentCode = B.documentName
                     WHERE A.referenceNo = '" + referenceNo + "'";
        dt = DataProcess.GetData(_connectionString, sql);
        gdvFileLoad.DataSource = null;
        gdvFileLoad.DataBind();
        if (dt.Rows.Count > 0)
        {
            gdvFileLoad.DataSource = dt;
            gdvFileLoad.DataBind();
        }
    }

    protected void gdvFileLoad_SelectedIndexChanged(object sender, EventArgs e)
    {
        int indx = gdvFileLoad.SelectedIndex;
        string gg = gdvFileLoad.Rows[indx].Cells[1].Text.Trim();
        String F1Path, F1Name;
        string abc = filepath + "\\" + gg.ToString().Replace("&amp;", "&");
        F1Path = abc.ToString();
        F1Name = Path.GetFileName(F1Path);
        GetFile(F1Path, F1Name);
    }
    private void GetFile(String strPath, String strSuggestedName)
    {

        String strServerPath;
        System.IO.FileInfo objSourceFileInfo;
        strServerPath = this.Server.MapPath(strSuggestedName);
        objSourceFileInfo = new System.IO.FileInfo(strPath);

        if (objSourceFileInfo.Exists)
        {
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + strSuggestedName);
            Response.AddHeader("Content-Length", objSourceFileInfo.Length.ToString());
            Response.WriteFile(objSourceFileInfo.FullName);
            Response.Flush();
            Response.End();
        }
        else
        {
            Response.Write("This file does not exist.");
        }
    }
    protected void gdvFileLoad_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indx = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "Download")
        {
            string gg = gdvFileLoad.Rows[indx].Cells[2].Text.Trim();
            String F1Path, F1Name;
            string abc = filepath + "\\" + gg.ToString().Replace("&amp;", "&");
            F1Path = abc.ToString();
            F1Name = Path.GetFileName(F1Path);
            GetFile(F1Path, F1Name);
        }
    }
    protected void gdvFileLoad_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    public void ClearGridFromUserControll()
    {
        gdvFileLoad.DataSource = null;
        gdvFileLoad.DataBind();
    }
}