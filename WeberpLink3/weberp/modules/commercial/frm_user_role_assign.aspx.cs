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
using LibraryDAL;

using LibraryDAL.SCBLDataSetTableAdapters;

public partial class frm_user_role_assign : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication();
        clsStatic.MsgConfirmBox(btnupdate, "Are you sure to update?");
        
        tblmaster.BgColor = "FFFFFFF";
        if (!Page.IsPostBack)
        {
            load_all_role_plant();
            load_all_user();
        }

    }

    private void load_all_role_plant()
    {
        App_Type_DetTableAdapter app = new App_Type_DetTableAdapter();
        SCBLDataSet.App_Type_DetDataTable dt = new SCBLDataSet.App_Type_DetDataTable();
        
        ListItem lst;
        ddlrole.Items.Clear();
        ddlrole.Items.Add("");

        lst = new ListItem();
        lst.Value = "SRQ";
        lst.Text = "SRQ: Store Requisition Initiation";
        ddlrole.Items.Add(lst);
                
        lst = new ListItem();
        lst.Value = "REQ";
        lst.Text = "REQ: MPR Initiation";
        ddlrole.Items.Add(lst);

        lst = new ListItem();
        lst.Value = "MPRREP";
        lst.Text = "MPRREP: MPR Report view";
        ddlrole.Items.Add(lst);

        lst = new ListItem();
        lst.Value = "ROU";
        lst.Text = "ROU: MPR Forwarding";
        ddlrole.Items.Add(lst);

        lst = new ListItem();
        lst.Value = "TEN";
        lst.Text = "TEN: Tender Inquiry";
        ddlrole.Items.Add(lst);

        lst = new ListItem();
        lst.Value = "QEN";
        lst.Text = "QEN: Quotation Entry";
        ddlrole.Items.Add(lst);

        lst = new ListItem();
        lst.Value = "PIINQ";
        lst.Text = "PIINQ: PI Inquiry";
        ddlrole.Items.Add(lst);


        lst = new ListItem();
        lst.Value = "PIENT";
        lst.Text = "PIENT: PI Entry";
        ddlrole.Items.Add(lst);

        lst = new ListItem();
        lst.Value = "CONENT";
        lst.Text = "CONENT: Consignment Entry";
        ddlrole.Items.Add(lst);


        lst = new ListItem();
        lst.Value = "CUSENT";
        lst.Text = "CUSENT: Custom Clearing Entry";
        ddlrole.Items.Add(lst);

        lst = new ListItem();
        lst.Value = "CUSAPP";
        lst.Text = "CUSAPP: Custom Clearing Approval";
        ddlrole.Items.Add(lst);

        lst = new ListItem();
        lst.Value = "CTG";
        lst.Text = "CTG: Port/ctg Entry";
        ddlrole.Items.Add(lst);

        lst = new ListItem();
        lst.Value = "SER";
        lst.Text = "SER: Factory Survey/Loan/MRR";
        ddlrole.Items.Add(lst);
        

        lst = new ListItem();
        lst.Value = "FPUPL";
        lst.Text = "FPUPL: FPI File Upload";
        ddlrole.Items.Add(lst);
        

        lst = new ListItem();
        lst.Value = "CPT";
        lst.Text = "CPT: Change Purchase Type";
        ddlrole.Items.Add(lst);
        
        lst = new ListItem();
        lst.Value = "LPOC";
        lst.Text = "LPOC: LP Order Creator";
        ddlrole.Items.Add(lst);
               

        lst = new ListItem();
        lst.Value = "SPOC";
        lst.Text = "SPOC: SP Order Create Initiate";
        ddlrole.Items.Add(lst);
                

        lst = new ListItem();
        lst.Value = "POREP";
        lst.Text = "POREP: Purchase Order Report view";
        ddlrole.Items.Add(lst);

        lst = new ListItem();
        lst.Value = "ADR";
        lst.Text = "ADR: Address Code Assign";
        ddlrole.Items.Add(lst);
               

        lst = new ListItem();
        lst.Value = "PORI";
        lst.Text = "PORI: PO Revise Initiation";
        ddlrole.Items.Add(lst);

        lst = new ListItem();
        lst.Value = "POCI";
        lst.Text = "POCI: PO Cancel Initiation";
        ddlrole.Items.Add(lst);

        lst = new ListItem();
        lst.Value = "POCLI";
        lst.Text = "POCLI: PO Close Initiation";
        ddlrole.Items.Add(lst);


        lst = new ListItem();
        lst.Value = "MATR";
        lst.Text = "MATR: Material Receive/Inspection/Return(LOCAL)/Issue";
        ddlrole.Items.Add(lst);
              

        lst = new ListItem();
        lst.Value = "PAYREQINI";
        lst.Text = "PAYREQINI: Payment Request Initiate";
        ddlrole.Items.Add(lst);

       

        dt = app.GetAllApp();

        foreach (SCBLDataSet.App_Type_DetRow dr in dt.Rows)
        {
            lst = new ListItem();
            lst.Value = dr.app_name.ToString();
            lst.Text = dr.app_name.ToString() + ": " + dr.app_desc.ToString();
            ddlrole.Items.Add(lst);
        }


        //plant
        

        chkPlantlist.Items.Clear();
        
        lst = new ListItem();
        lst.Value = "CT";
        lst.Text = "CT:CTMPR";
        chkPlantlist.Items.Add(lst);
       

        //role type

        ddlroletype.Items.Clear();
        ddlroletype.Items.Add("");

        lst = new ListItem();
        lst.Value = "GEN";
        lst.Text = "GEN: General Approval";
        ddlroletype.Items.Add(lst);

        lst = new ListItem();
        lst.Value = "SR";
        lst.Text = "SR: SR Approval";
        ddlroletype.Items.Add(lst);
        
        lst = new ListItem();
        lst.Value = "PROD";
        lst.Text = "PROD: Production";
        ddlroletype.Items.Add(lst);

        lst = new ListItem();
        lst.Value = "MPR";
        lst.Text = "MPR: MPR Approval";
        ddlroletype.Items.Add(lst);

        lst = new ListItem();
        lst.Value = "CS";
        lst.Text = "CS: CS Approval";
        ddlroletype.Items.Add(lst);

        lst = new ListItem();
        lst.Value = "SPOAPP";
        lst.Text = "SPOAPP: SPO Create Approval";
        ddlroletype.Items.Add(lst);

        lst = new ListItem();
        lst.Value = "ADAPP";
        lst.Text = "ADAPP: Adv Realization Approval";
        ddlroletype.Items.Add(lst);

        lst = new ListItem();
        lst.Value = "FPOAPP";
        lst.Text = "FPOAPP: FPO Create Approval";
        ddlroletype.Items.Add(lst);

        lst = new ListItem();
        lst.Value = "REVISE";
        lst.Text = "REVISE: PO Revise Approval";
        ddlroletype.Items.Add(lst);
               
        lst = new ListItem();
        lst.Value = "CANCEL";
        lst.Text = "CANCEL: PO Cancel Approval";
        ddlroletype.Items.Add(lst);

        lst = new ListItem();
        lst.Value = "CLOSE";
        lst.Text = "CLOSE: PO Close Approval";
        ddlroletype.Items.Add(lst);

        lst = new ListItem();
        lst.Value = "PIAPP";
        lst.Text = "PIAPP: PI Approval";
        ddlroletype.Items.Add(lst);
               

        lst = new ListItem();
        lst.Value = "PAYREQ";
        lst.Text = "PAYREQ: Payment Request Approval";
        ddlroletype.Items.Add(lst);

    }

    private void load_all_user()
    {
        LibraryDAL.dsLinkofficeTableAdapters.tblUserInfoTableAdapter  udal = new LibraryDAL.dsLinkofficeTableAdapters.tblUserInfoTableAdapter ();
        LibraryDAL.dsLinkoffice.tblUserInfoDataTable dt = new LibraryDAL.dsLinkoffice.tblUserInfoDataTable();
        ListItem itm;


        dt = udal.GetAllUser(current.CompanyCode);

        ddlusers.Items.Clear();
        ddlusers.Items.Add("");

        foreach (dsLinkoffice.tblUserInfoRow  dr in dt.Rows)
        {
            itm = new ListItem();
            itm.Value = dr.UserId;
            itm.Text = dr.UserId.ToString() + ": " + dr.UserName.ToString();
            ddlusers.Items.Add(itm);
        }

    }    

    protected void gdUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void gdUser_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["per_table"];
        dt.Rows[e.RowIndex].Delete();
        ViewState["per_table"] = dt;

        gdUser.DataSource = dt;
        gdUser.DataBind();
        
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        System.Threading.Thread.Sleep(4000);
        if (ddlprio.Text == "") return;
        if (ddlroletype.Text == "") return;
        if (ddlrole.Text == "") return;
        if (ddlusers.Text == "") return;
        
        string usercode,username, roletype, role_as;
        string plant_list="";
        int seq,priority;
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["per_table"];


        usercode=ddlusers.SelectedValue.ToString();
        username=ddlusers.SelectedItem.Text.Split(':')[1].Trim();
        roletype = ddlroletype.SelectedValue.ToString();
        role_as=ddlrole.SelectedValue.ToString();

        priority=Convert.ToInt32(ddlprio.Text);
        seq=dt.Rows.Count+1;
              
        foreach (ListItem lst in chkPlantlist.Items)
        {
            if (lst.Selected)
                plant_list = plant_list + lst.Value.ToString() + ",";
        }

        dt.Rows.Add(usercode, username, seq, roletype, role_as, plant_list, priority.ToString());

        gdUser.DataSource = dt;
        gdUser.DataBind();

        clear_all();

    }
   
    

    private void clear_all()
    {        
        ddlprio.Text = "";
        ddlroletype.Text = "";
        ddlrole.Text = "";
        foreach (ListItem lst in chkPlantlist.Items)
        {
            lst.Selected = false;
        }

    }


    private void generate_user_info()
    {
        if (ddlusers.Text == "")
        {
        }
        else
        {
            User_Role_DefinitionTableAdapter role = new User_Role_DefinitionTableAdapter();
            SCBLDataSet.User_Role_DefinitionDataTable roledt = new SCBLDataSet.User_Role_DefinitionDataTable();
            string usercode = ddlusers.SelectedValue.ToString();
            DataTable dtgrid = new DataTable();



            roledt = role.GetAllrolebyUser(usercode);

         
            dtgrid.Rows.Clear();
            dtgrid.Columns.Clear();

            dtgrid.Columns.Add("USER CODE", typeof(string));
            dtgrid.Columns.Add("NAME", typeof(string));
            dtgrid.Columns.Add("SEQ", typeof(int));
            dtgrid.Columns.Add("ROLE TYPE", typeof(string));
            dtgrid.Columns.Add("ROLE AS", typeof(string));
            dtgrid.Columns.Add("PLANTS", typeof(string));
            dtgrid.Columns.Add("PRIORITY", typeof(string));

            foreach (SCBLDataSet.User_Role_DefinitionRow dr in roledt.Rows)
            {
                dtgrid.Rows.Add(dr.user_id, dr.user_name, dr.per_seq_no,dr.role_type, dr.role_as, dr.plant_list, dr.priority.ToString());
            }
            
            ViewState["per_table"] = dtgrid;

            gdUser.DataSource = dtgrid;
            gdUser.DataBind();
        }
    }

    protected void ddlusers_SelectedIndexChanged(object sender, EventArgs e)
    {
        generate_user_info();
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        if (ddlusers.Text == "") return;
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["per_table"];
        if (dt.Rows.Count == 0) return;
        string usercode, username, roletype, role_as,plant_list;        
        int priority;
        int seq = 0;
        User_Role_DefinitionTableAdapter role = new User_Role_DefinitionTableAdapter();

        usercode = ddlusers.SelectedValue.ToString();
        username = ddlusers.SelectedItem.Text.Split(':')[1].Trim();
       

        role.DeletePermissionByUserCode(usercode);

        foreach (DataRow dr in dt.Rows)
        {
            seq++;
            roletype = dr[3].ToString();
            role_as = dr[4].ToString();
            plant_list = dr[5].ToString();
            priority = Convert.ToInt32( dr[6].ToString());

            role.InsertUserRole(usercode, username, seq, "ALL", roletype, role_as, plant_list, priority);

        }

        generate_user_info();

    }
}

