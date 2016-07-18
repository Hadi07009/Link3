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


public partial class ClientSide_modules_commercial_usercontrols_ctl_approval : System.Web.UI.UserControl
{
    public event EventHandler reset_pending;
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication();
        clsStatic.MsgConfirmBox(btnforward, "Are you sure to Forward this items ?");
        clsStatic.MsgConfirmBox(btnreject, "Are you sure to Reject this items ?");
        clsStatic.MsgConfirmBox(btnapp, "Are you sure to Approve this items ?");
       
    }



    private string get_my_app()
    {
        User_Role_DefinitionTableAdapter urole = new User_Role_DefinitionTableAdapter();
        SCBLDataSet.User_Role_DefinitionDataTable udt = new SCBLDataSet.User_Role_DefinitionDataTable();
        string my_app = "";
        udt = urole.GetRoleByUser(current.UserId.ToString(),"CS");

        if (udt.Rows.Count > 0) my_app = udt[0].role_as.ToString();

        return my_app;
    }

    private int GetSelectedIndex()
    {
        int indx = 1;
        int i;

        HtmlInputRadioButton rdolist;
        Control ctl;

        for (i = 1; i < 11; i++)
        {
            rdolist = new HtmlInputRadioButton();        
            ctl = new Control();           
          
            ctl = tbl_party.Rows[i].Cells[0].Controls[1];                 

            rdolist = (HtmlInputRadioButton)ctl;

            if (rdolist.Checked) { indx = i;  }
        }        

        return indx;

    }

   
 

    

    protected void btnforward_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
             

        if (reset_pending != null)
        {
            reset_pending(sender, e);
        }
        
    }
    protected void btnreject_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        
        if (reset_pending != null)
        {
            reset_pending(sender, e);
        }
    }

    protected void btnapp_Click(object sender, EventArgs e)
    {

    }
    protected void lnktc1_Click(object sender, EventArgs e)
    {
        ModalPopupExtender5.Show();
    }
}
