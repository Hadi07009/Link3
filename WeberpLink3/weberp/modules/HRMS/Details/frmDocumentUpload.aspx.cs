using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Label = System.Web.UI.WebControls.Label;

public partial class modules_HRMS_Details_frmDocumentUpload : System.Web.UI.Page
{
    readonly string _connectionString = ConfigurationManager.AppSettings["UbasysConnectionString"];
    private DocumentUploadController _objDocumentUploadController;
    private DocumentUpload _objDocumentUpload;

    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);

        if (!Page.IsPostBack)
        {
            LoadDocumentType();
            ActiveDocument(-1);
        }

    }

    private void LoadDocumentType()
    {
        try
        {
            ClsDropDownListController.LoadDropDownList(_connectionString, Sqlgenerate.SqlGetDocumentTypeIntoDDL(), ddlDocumentType, "documentTypeText", "documentTypeCode");
        }
        catch (Exception msgException)
        {

            MessageBox.Show(msgException.Message);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int indx = ddlDocumentType.SelectedIndex;
        if (indx == -1) return;
        
        AttachFileSave();
        ClearAllControl();
        ActiveDocument(indx);
    }

    private void Upload(string fileName, string referenceNo, string filenemeReference)
    {
        try
        {
            _objDocumentUploadController = new DocumentUploadController();
            _objDocumentUpload = new DocumentUpload();
            _objDocumentUpload.DocumentTypeCode = ddlDocumentType.SelectedValue == "-1" ? null : ddlDocumentType.SelectedValue;
            _objDocumentUpload.Description = txtDescription.Text == string.Empty ? null : txtDescription.Text;
            _objDocumentUpload.DocumentContent = null;
            _objDocumentUpload.EntryUser = current.UserId;
            _objDocumentUpload.DocumentContent = fileName;
            _objDocumentUpload.documentCode = referenceNo;
            _objDocumentUpload.documentName = filenemeReference;
            _objDocumentUploadController.DocumentUpload(_connectionString, _objDocumentUpload);
        }
        catch (Exception msgException)
        {
            MessageBox1.ShowError(msgException.Message);
        }
    }
    private void AttachFileSave()
    {
        try
        {
            string filepath = System.Configuration.ConfigurationSettings.AppSettings["upl"].ToString();


            HttpFileCollection hfc = Request.Files;

            _objDocumentUploadController = new DocumentUploadController();

            string referenceno = _objDocumentUploadController.GetReferenceNo(_connectionString);
            for (int i = 0; i < hfc.Count; i++)
            {
                HttpPostedFile hpf = hfc[i];

                if (hpf.ContentLength > 0)
                {
                    string fileName = System.IO.Path.GetFileName(hpf.FileName).Replace("'", "''");
                    string filenameReference = referenceno + "-" + fileName;
                    //hpf.SaveAs(Server.MapPath("~/AttachMentfile/") + "\\" + filenameReference);
                    hpf.SaveAs(filepath + "\\" + filenameReference);
                    Upload(fileName, referenceno, filenameReference);
                }

            }
        }
        catch(Exception ex) 
        {
            MessageBox1.ShowWarning(ex.Message);
        }
    }

    private void ClearAllControl()
    {
        txtDescription.Text = string.Empty;
    }
    protected void btnActiveDocument_Click(object sender, EventArgs e)
    {
        int indx = ddlDocumentType.SelectedIndex;

        if(indx==-1) return;

        ActiveDocument(indx);
    }

    private void ActiveDocument(int doctype)
    {
        try
        {
            _objDocumentUploadController = new DocumentUploadController();
            DataTable dtDocument = _objDocumentUploadController.GetActiveDocument(_connectionString, doctype);
            grdActiveDocument.DataSource = null;
            grdActiveDocument.DataBind();
            grdInactiveDocument.DataSource = null;
            grdInactiveDocument.DataBind();
            if (dtDocument.Rows.Count > 0)
            {
                grdActiveDocument.DataSource = dtDocument;
                grdActiveDocument.DataBind();
            }
        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }
    protected void btnInactiveDocument_Click(object sender, EventArgs e)
    {
        int index = ddlDocumentType.SelectedIndex;

        InactiveDocument(index);
    }

    private void InactiveDocument(int doctype)
    {
        try
        {
            _objDocumentUploadController = new DocumentUploadController();
            DataTable dtDocument = _objDocumentUploadController.GetInactiveDocument(_connectionString, doctype);
            grdActiveDocument.DataSource = null;
            grdActiveDocument.DataBind();
            grdInactiveDocument.DataSource = null;
            grdInactiveDocument.DataBind();
            if (dtDocument.Rows.Count > 0)
            {
                grdInactiveDocument.DataSource = dtDocument;
                grdInactiveDocument.DataBind();
            }
        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }
    protected void grdActiveDocument_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
    }
    protected void grdInactiveDocument_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
    }
    protected void grdActiveDocument_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            var selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName.Equals("Delete"))
            {
                int lblReferenceNo = Convert.ToInt32(((Label)grdActiveDocument.Rows[selectedIndex].FindControl("lblReferenceNo")).Text);
                _objDocumentUploadController = new DocumentUploadController();
                _objDocumentUpload = new DocumentUpload();
                _objDocumentUpload.referenceNo = lblReferenceNo;
                _objDocumentUpload.dstatus = 0;

                _objDocumentUploadController.UpdateDocumentSrtatus(_connectionString, _objDocumentUpload);
                ActiveDocument(-1);
            }
        }
        catch (Exception msgException)
        {

            MessageBox.Show(msgException.Message);
        }
    }
    protected void grdInactiveDocument_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            var selectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName.Equals("Delete"))
            {
                int lblReferenceNo = Convert.ToInt32(((Label)grdInactiveDocument.Rows[selectedIndex].FindControl("lblReferenceNo")).Text);
                _objDocumentUploadController = new DocumentUploadController();
                _objDocumentUpload = new DocumentUpload();
                _objDocumentUpload.referenceNo = lblReferenceNo;
                _objDocumentUpload.dstatus = 1;
                _objDocumentUploadController.UpdateDocumentSrtatus(_connectionString, _objDocumentUpload);
                InactiveDocument(-1);
            }
        }
        catch (Exception msgException)
        {

            MessageBox.Show(msgException.Message);
        }
    }
    protected void grdActiveDocument_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void grdInactiveDocument_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void ddlDocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int index = ddlDocumentType.SelectedIndex;
        ActiveDocument(index); 
    }
}