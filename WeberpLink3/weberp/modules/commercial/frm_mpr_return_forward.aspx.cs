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
using LibraryDTO;
using LibraryDAL.SCBLDataSetTableAdapters;
using LibraryDAL.SCBL2DataSetTableAdapters;
using LibraryDAL.ErpDataSetTableAdapters;

public partial class frm_mpr_return_forward : System.Web.UI.Page
{
   

    protected void Page_Load(object sender, EventArgs e)
    {
        
        clsStatic.MsgConfirmBox(btnreject, "Are you sure to remove ? ");
        clsStatic.MsgConfirmBox(btnforward, "Are you sure to forward ? ");
                
        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";             

        if (!Page.IsPostBack)
        {
            load_pending_list();                       
        }
        else
        {
           // generate_comments(ddllist.SelectedValue.ToString());
        }
          
    }
    private string get_my_app()
    {
        User_Role_DefinitionTableAdapter urole = new User_Role_DefinitionTableAdapter();
        SCBLDataSet.User_Role_DefinitionDataTable udt = new SCBLDataSet.User_Role_DefinitionDataTable();
        string my_app = "";
        udt = urole.GetRoleByUser(current.UserId.ToString(),"MPR");

        if (udt.Rows.Count > 0) my_app = udt[0].role_as.ToString();

        return my_app;
    }

    private void load_pending_list()
    {
        PuTr_IN_Det_Scbl2TableAdapter det = new PuTr_IN_Det_Scbl2TableAdapter();
        SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable dtdet = new SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable();
        ListItem lst;
        string my_app = get_my_app();

        dtdet = det.GetDataByStatus("RET", my_app);

        if (dtdet.Rows.Count == 0)
        {
            Response.Redirect("./frm_com_inbox.aspx");
        }

        ddllist.Items.Clear();
        ddllist.Items.Add("");
        foreach (SCBL2DataSet.PuTr_IN_Det_Scbl2Row dr in dtdet.Rows)
        {
            lst = new ListItem();
            lst.Value = dr.IN_Det_Ref.ToString() + ":" + dr.IN_Det_Icode.ToString();
            lst.Text = dr.IN_Det_Icode.ToString() + ":" + dr.IN_Det_Itm_Desc.ToString();
            ddllist.Items.Add(lst);
        }

        lblcount.Text = "(" + dtdet.Rows.Count.ToString() + ")";
               
        tbl_po.Visible = false;
    }
   
    private void generate_detail_data(string ref_no, string item_code)
    {
        PuTr_IN_Det_Scbl2TableAdapter det = new PuTr_IN_Det_Scbl2TableAdapter();
        SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable dtdet = new SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable();

        dtdet = det.GetDataByRefItem(ref_no, item_code);
        if (dtdet.Rows.Count == 0) { tbl_po.Visible = false; return; }
        
        lblref.Text = dtdet[0].IN_Det_Ref;
        lblitemdet.Text = dtdet[0].IN_Det_Icode + ":" + dtdet[0].IN_Det_Itm_Desc;
        lbluom.Text = dtdet[0].IN_Det_Itm_Uom;
        txtqty.Text = dtdet[0].IN_Det_Lin_Qty.ToString("N2");
        txtspecification.Text = dtdet[0].In_Det_Specification;
        txtbrand.Text = dtdet[0].In_Det_Brand;
        txtorigin.Text = dtdet[0].In_Det_Origin;
        txtpacking.Text = dtdet[0].In_Det_Packing;
        txtremarks.Text = dtdet[0].In_Det_Remarks;
        cldetr.SelectedDate = dtdet[0].IN_Det_Exp_Dat;
    }

    private void generate_comments(string ref_no, string itm_code)
    {
        tbl_CommentsTableAdapter com = new tbl_CommentsTableAdapter();
        SCBLDataSet.tbl_CommentsDataTable dt = new SCBLDataSet.tbl_CommentsDataTable();

        int cnt, i, j;
        dt = com.GetDataByRefRole(ref_no, "ROU");
        cnt = dt.Rows.Count;

        i = 0;
        j = 0;
        phcomm.Controls.Clear();

        foreach (SCBLDataSet.tbl_CommentsRow dr in dt.Rows)
        {
            j++;

            ClientSide_modules_commercial_usercontrols_ctl_comments ctl = (ClientSide_modules_commercial_usercontrols_ctl_comments)LoadControl("./usercontrols/ctl_comments.ascx");
            Label lblname = (Label)ctl.FindControl("lblname");
            Label lbldate = (Label)ctl.FindControl("lbldate");
            HtmlTableCell celcomm = (HtmlTableCell)ctl.FindControl("celcomm");
            Image imgimage = (Image)ctl.FindControl("imgimage");

            imgimage.ImageUrl = "~/handler/hndImage.ashx?id=" + dr.app_id;

            ctl.ID = "ctl_" + j.ToString() + phcomm.Controls.Count.ToString();

            lblname.Text = dr.app_name.ToString() + " (" + dr.app_designation.ToString() + ")";
            lbldate.Text = dr.app_date.ToString();
            celcomm.InnerText = dr.gen_comments.ToString();

            if (dr.sp_comments == itm_code) 
            {
                i++;
                phcomm.Controls.Add(ctl);
            }

            if ((j == cnt) && (i == 0)) { phcomm.Controls.Add(ctl); }

           
        }
    }

   

    protected void ddllist_SelectedIndexChanged(object sender, EventArgs e)
    {
        load_data();
        
    }


    private void load_data()
    {
        System.Threading.Thread.Sleep(1000);
        string selitem = ddllist.SelectedItem.Value.ToString();

        

        if (selitem == "")
        {
            tbl_po.Visible = false;
        }
        else
        {
            string ref_no = selitem.Split(':')[0];
            string item_code = selitem.Split(':')[1];

            tbl_po.Visible = true;
            generate_detail_data(ref_no, item_code);
            generate_comments(ref_no, item_code);
           
        }
    }


    private bool Check_Approval_Validity(string ref_no, string item_code)
    {
        PuTr_IN_Det_Scbl2TableAdapter det = new PuTr_IN_Det_Scbl2TableAdapter();
        bool flg = false;
        string myapp = get_my_app();
        if (det.GetDataByRefItem(ref_no, item_code)[0].In_Det_Status1.ToString() == myapp) { flg = true; }

        return flg;
    }

    protected void btnforward_Click(object sender, EventArgs e)
    {      

        if (Check_Approval_Validity(lblref.Text, lblitemdet.Text.Split(':')[0]) == false) { return; }
        PuTr_IN_Det_Scbl2TableAdapter det = new PuTr_IN_Det_Scbl2TableAdapter();
        
        string selitem = ddllist.SelectedItem.Value.ToString();

        string ref_no = selitem.Split(':')[0];
        string item_code = selitem.Split(':')[1];
        
        det.UpdateForReturnFor("ROU", "", cldetr.SelectedDate, Convert.ToDouble(txtqty.Text), Convert.ToDouble(txtqty.Text), txtremarks.Text, txtspecification.Text, txtbrand.Text, txtorigin.Text, txtpacking.Text, ref_no, item_code);
        Response.Redirect(Request.Url.AbsoluteUri);

        
    }
   
    protected void btnreject_Click(object sender, EventArgs e)
    {
        
        if (Check_Approval_Validity(lblref.Text, lblitemdet.Text.Split(':')[0]) == false) { return; }
        PuTr_IN_Det_Scbl2TableAdapter det = new PuTr_IN_Det_Scbl2TableAdapter();

        string selitem = ddllist.SelectedItem.Value.ToString();

        string ref_no = selitem.Split(':')[0];
        string item_code = selitem.Split(':')[1];


        det.UpdateFromReturn("REJ", "", ref_no, item_code);


        Response.Redirect(Request.Url.AbsoluteUri);

    }

 

    protected void btnreload_Click(object sender, EventArgs e)
    {
        load_data();
    }
    
}
