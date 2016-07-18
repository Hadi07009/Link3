using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using LibraryDAL;
using LibraryDAL.SCBLDataSetTableAdapters;


public partial class ClientSide_modules_commercial_usercontrols_ctl_order_create : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        

    }



    protected void lblshowhide_Click(object sender, EventArgs e)
    {
        
        if (lblshowhide.Text == "Show Detail")
        {
            lblshowhide.Text = "Hide Detail";
            for (int i = 9; i <= 16; i++)
            {
                tbldet.Rows[i].Visible = true;
            }

        }
        else
        {
            lblshowhide.Text = "Show Detail";
            for (int i = 9; i <= 16; i++)
            {
                tbldet.Rows[i].Visible = false;
            }
        }

    }
}
