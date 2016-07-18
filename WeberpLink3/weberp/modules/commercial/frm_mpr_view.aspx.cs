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
using LibraryDAL.ErpDataSetTableAdapters;

public partial class frm_mpr_view : System.Web.UI.Page
{
   

    protected void Page_Load(object sender, EventArgs e)
    {
        
        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";             

        if (!Page.IsPostBack)
        {
            string ref_no = Request.QueryString["ref_no"];            
            if (ref_no == null)  { return; }

            load_data(ref_no);
            

        }
        else
        {
            generate_comments();
        }
          
    }
    
    
    private void BindMyGridview()
    {
        DataTable dt = new DataTable();

        dt = (DataTable)Session[clsStatic.sessionTempDatatable];

        gdItem.DataSource = dt;
        gdItem.DataBind();
    }
        
    
    
    

    private void generate_detail_data(string ref_no)
    {
        LibraryDAL.ErpDataSetTableAdapters.InMa_Stk_CtlTableAdapter stk = new LibraryDAL.ErpDataSetTableAdapters.InMa_Stk_CtlTableAdapter();
        ErpDataSet.InMa_Stk_CtlDataTable dtstk = new ErpDataSet.InMa_Stk_CtlDataTable();

        PuTr_IN_Hdr_ScblTableAdapter hdr = new PuTr_IN_Hdr_ScblTableAdapter();
        SCBLDataSet.PuTr_IN_Hdr_ScblDataTable dthdr = new SCBLDataSet.PuTr_IN_Hdr_ScblDataTable();
        PuTr_IN_Det_ScblTableAdapter det = new PuTr_IN_Det_ScblTableAdapter();
        SCBLDataSet.PuTr_IN_Det_ScblDataTable dtdet = new SCBLDataSet.PuTr_IN_Det_ScblDataTable();
        DataTable dt = new DataTable();

        string freestk = "";

        decimal amnt;

        dthdr = hdr.GetDataByRef(ref_no);
        dtdet=det.GetDataByInRef(ref_no);
        lbldate.Text= dtdet[0].IN_Det_Exp_Dat.ToString();
        lblref.Text = ref_no;
        lblcomments.Text = dthdr[0].IN_Hdr_Com4;
        lbldept.Text = dthdr[0].IN_Hdr_Pcode + ":" + dthdr[0].IN_Hdr_Com5;
        lblstatus.Text = dthdr[0].IN_Hdr_Status;

        dt.Rows.Clear();
        dt.Columns.Clear();
        dt.Columns.Add("Sl", typeof(int));
        dt.Columns.Add("Item Code", typeof(string));
        dt.Columns.Add("Item Desc", typeof(string));
        dt.Columns.Add("UOM", typeof(string));
        dt.Columns.Add("Free Stk", typeof(string));
        dt.Columns.Add("Qty", typeof(string));
        dt.Columns.Add("Rate", typeof(string));
        dt.Columns.Add("Amnt", typeof(string));
        dt.Columns.Add("Specification", typeof(string));
        dt.Columns.Add("Brand", typeof(string));
        dt.Columns.Add("Origin", typeof(string));
        dt.Columns.Add("Packing", typeof(string));
        dt.Columns.Add("ETR", typeof(string));
        dt.Columns.Add("Remarks", typeof(string));

        foreach (SCBLDataSet.PuTr_IN_Det_ScblRow dr in dtdet.Rows)
        {
            dtstk = new ErpDataSet.InMa_Stk_CtlDataTable();
            dtstk = stk.GetDataByItemStore(dr.IN_Det_Icode, ref_no.Substring(0, 2) + "GEN");
            if (dtstk.Rows.Count == 0)
            {
                freestk = "";
            }
            else
            {
                freestk = dtstk[0].Stk_Ctl_Free_Stk.ToString("N2");
            }
            amnt = (decimal)dr.IN_Det_Lin_Qty * dr.IN_Det_Lin_Rat;
            dt.Rows.Add((int)dr.IN_Det_Lno, dr.IN_Det_Icode, dr.IN_Det_Itm_Desc, dr.IN_Det_Itm_Uom, freestk, dr.IN_Det_Lin_Qty.ToString(), dr.IN_Det_Lin_Rat.ToString("N2"), amnt, dr.In_Det_Specification, dr.In_Det_Brand, dr.In_Det_Origin, dr.In_Det_Packing, dr.IN_Det_Exp_Dat.ToShortDateString(),dr.In_Det_Remarks);
        }

        Session[clsStatic.sessionTempDatatable] = dt;
        BindMyGridview();
    }

    private void generate_comments()
    {
        tbl_CommentsTableAdapter com = new tbl_CommentsTableAdapter();
        SCBLDataSet.tbl_CommentsDataTable dt = new SCBLDataSet.tbl_CommentsDataTable();
        if (lblref.Text == "") return;
        string ref_no = lblref.Text;

        dt = com.GetCommentsByRef(ref_no);
        phcomm.Controls.Clear();
        foreach (SCBLDataSet.tbl_CommentsRow dr in dt.Rows)
        {
            ClientSide_modules_commercial_usercontrols_ctl_comments ctl = (ClientSide_modules_commercial_usercontrols_ctl_comments)LoadControl("./usercontrols/ctl_comments.ascx");
            Label lblname = (Label)ctl.FindControl("lblname");
            Label lbldate = (Label)ctl.FindControl("lbldate");
            HtmlTableCell celcomm = (HtmlTableCell)ctl.FindControl("celcomm");
            Image imgimage = (Image)ctl.FindControl("imgimage");

            imgimage.ImageUrl = "~/handler/hndImage.ashx?id=" + dr.app_id;

            ctl.ID = "ctl_" + phcomm.Controls.Count.ToString();

            lblname.Text = dr.app_name.ToString() + " (" + dr.app_designation.ToString() + ")";
            lbldate.Text = dr.app_date.ToString();
            celcomm.InnerText = dr.gen_comments.ToString();

            phcomm.Controls.Add(ctl);
        }
    }






    private void load_data(string selitem)
    {
       
        if (selitem == "")
        {
            tbl_po.Visible = false;
        }
        else
        {
            tbl_po.Visible = true;
            generate_detail_data(selitem);
            generate_comments();
           
        }
    }


    
    
   

 

   
}
