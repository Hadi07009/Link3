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


public partial class usercontrols_ucFormsPolicy : System.Web.UI.UserControl
{
    string _connectionString = ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
    string filepath = System.Configuration.ConfigurationSettings.AppSettings["upl"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //SelfServiceLoad();
            LoadUploadFileByRef();
        }
    }



    protected void gdvFileLoad_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[0].Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
            //e.Row.Cells[0].Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";

            //e.Row.Cells[1].Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
            //e.Row.Cells[1].Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
            //e.Row.Cells[2].Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
            //e.Row.Cells[2].Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";

            //e.Row.Cells[0].Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gdvFileLoad, "Select$" + e.Row.RowIndex);
            //e.Row.Cells[1].Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gdvFileLoad, "Select$" + e.Row.RowIndex);
            //e.Row.Cells[2].Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gdvFileLoad, "Select$" + e.Row.RowIndex);
            e.Row.Cells[0].Text = (e.Row.RowIndex+1).ToString();

        }
        //e.Row.Cells[0].Visible = false;
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[3].Visible = false;
    }
    private void LoadUploadFileByRef()
    {
        string _connectionString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
        DataTable dt = new DataTable();
        dt = DataProcess.GetData(_connectionString, "select 1 as SL, documentCode as [ReferenceNo],documentContent as [FileName],documentName as [SavedFileName]  from HRMS_Document where documentTypeCode=1 and status=1 order by [ReferenceNo]");
        gdvFileLoad.DataSource = dt;
        gdvFileLoad.DataBind();
    }

    protected void gdvFileLoad_SelectedIndexChanged(object sender, EventArgs e)
    {
        int indx = gdvFileLoad.SelectedIndex;
        string gg = gdvFileLoad.Rows[indx].Cells[2].Text.Trim();
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
        if (e.CommandName == "Download")
        {
            int indx = Convert.ToInt32(e.CommandArgument);            
            string gg = gdvFileLoad.Rows[indx].Cells[3].Text.Trim();
            String F1Path, F1Name;
            string abc = filepath + "\\" + gg.ToString().Replace("&amp;", "&");
            F1Path = abc.ToString();
            F1Name = Path.GetFileName(F1Path);
            GetFile(F1Path, F1Name);
        }
    }
}