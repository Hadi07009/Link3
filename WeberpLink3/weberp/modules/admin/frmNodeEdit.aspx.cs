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
public partial class frmNodeEdit : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);
        if(!IsPostBack)
        {
            if (!Page.IsPostBack)
            {
                load_xml();
            }
        }
        
    }
    private void load_xml()
    {
        int cnt = 0;
        string path = Server.MapPath("~/xml/xml_treeview.xml");
        string varread, line;
        int row = 0;
        StreamReader reader = new StreamReader(path);

        varread = reader.ReadToEnd();
        reader.Close();
        reader.Dispose();

        reader = new StreamReader(path);

        while ((line = reader.ReadLine()) != null)
        {

            foreach (string word in line.Split())
            {
                if ((Convert.ToString(word) != " ") && (Convert.ToString(word) != ""))
                {

                    cnt++;
                }
            }
            row++;
        }


        reader.Close();
        reader.Dispose();

        txtXml.Text = varread;
        txtXml.Height = row * 16;
        btnUpdate.Enabled = true;

    }


    protected void btnReload_Click(object sender, EventArgs e)
    {
        load_xml();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string path = Server.MapPath("~/xml/xml_treeview.xml");
        StreamWriter writer = new StreamWriter(path);
        writer.Write(txtXml.Text);
        writer.Close();
        writer.Dispose();
    }

}
