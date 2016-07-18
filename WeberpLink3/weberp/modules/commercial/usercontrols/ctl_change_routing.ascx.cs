using System;
using System.Reflection;
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


public partial class ClientSide_modules_commercial_usercontrols_ctl_change_routing : System.Web.UI.UserControl
{

  
    public event EventHandler reset_pending;

    protected void Page_Load(object sender, EventArgs e)
    {
       // clsStatic.MsgConfirmBox(lnkProceed, "Are you sure to proceed ?");
        clsStatic.CheckUserAuthentication();
               
    }


    private bool chk_for_routing_change(string ref_no, int line_no)
    {
        bool flg=false;

        PuTr_IN_Det_ScblTableAdapter srdet = new PuTr_IN_Det_ScblTableAdapter();
        try
        {
            if (srdet.CheckPendingValidity1(ref_no, (short)line_no, "TEN").Rows.Count > 0) flg = true;
            if (srdet.CheckPendingValidity1(ref_no, (short)line_no, "QUO").Rows.Count > 0) flg = true;
            if (srdet.CheckPendingValidity1(ref_no, (short)line_no, "APP").Rows.Count > 0) flg = true;
        }
        catch
        {
            flg = false;
        }

        return flg;
    }


    protected void btnChange_Click(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication();
        PuTr_IN_Det_ScblTableAdapter In_Det = new PuTr_IN_Det_ScblTableAdapter();     
      
        string ref_no, act_det, tmp_str, userid, user_name;
        int line_no;
        string[] tmp;
        string[] tmp1;
        string[] tmp2;

        tmp_str = celref.InnerText.ToString();

        tmp = tmp_str.Split(',');
        tmp1 = tmp[0].Split('[');
        tmp2 = tmp[1].Split(']');

        ref_no = tmp1[1].ToString();
        line_no = Convert.ToInt32(tmp2[0]);
        act_det = ddlAction.SelectedValue.ToString();

       

        userid = current.UserId.ToString();
        user_name = current.UserName.ToString();
         
        if (chk_for_routing_change(ref_no, line_no) == false) return;

        SqlTransaction myTrn = HelperTA.OpenTransaction(In_Det.Connection);

        try
        {
            In_Det.AttachTransaction(myTrn);
            if (act_det == "FORWARD")
            {
                In_Det.UpdateFromRouting1("ROU", "", ref_no, (short)line_no);
            }
            else
            {
                In_Det.UpdateFromRouting1("TEN", act_det, ref_no, (short)line_no);
            }
            
           //myTrn.Rollback();
            myTrn.Commit();
        }
        catch
        {

            myTrn.Rollback();
        }
        finally
        {
            HelperTA.CloseTransaction(In_Det.Connection, myTrn);
        }
                
        
        
        //Page.GetType().InvokeMember("get_pending", BindingFlags.InvokeMethod, null, this.Page, null);
        //Page.GetType().InvokeMember("get_pending2", BindingFlags.InvokeMethod, null, this.Page, null);

        //Page.GetType().GetMethod("get_pending", BindingFlags.InvokeMethod, null,null,null);

        Response.Redirect(Request.Url.AbsoluteUri);
        
    }
}
