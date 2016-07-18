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
using LibraryDTO;
using LibraryDAL;
using LibraryDAL.SCBLDataSetTableAdapters;
using LibraryDAL.SCBL2DataSetTableAdapters;
using LibraryDAL.ErpDataSetTableAdapters;
using LibraryDAL.SCBLQryTableAdapters;

public partial class frm_mpr_status_hdr : System.Web.UI.Page
{
   

    protected void Page_Load(object sender, EventArgs e)
    {

        //current.UserId = "MON";
        //current.UserName = "MONJU";

        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";
      
        if (!Page.IsPostBack)
        {
            cldfrom.SelectedDate = Convert.ToDateTime("01/12/2009");
            cldto.SelectedDate = DateTime.Now;
            load_plant();
            load_status();
        
        }
        else
        {

        }
          
    }
    

    private void load_plant()
    {
        string ucode = current.UserId.ToString();
        User_Role_DefinitionTableAdapter uda = new User_Role_DefinitionTableAdapter();
        SCBLDataSet.User_Role_DefinitionDataTable udt = new SCBLDataSet.User_Role_DefinitionDataTable();
        tbl_trn_detTableAdapter trn = new tbl_trn_detTableAdapter();
        SCBLDataSet.tbl_trn_detDataTable dttrn = new SCBLDataSet.tbl_trn_detDataTable();

        ListItem lst;
        int i;
        string[] items;

        udt = uda.GetDataByUserCodeRole(ucode, "MPRREP");


        ddlplantlist.Items.Clear();
        ddlplantlist.Items.Add("");
        if (udt.Rows.Count == 0) return;

        items = udt[0].plant_list.Split(',');

        dttrn = trn.GetAllCodeByType("IN");

        foreach (SCBLDataSet.tbl_trn_detRow dr in dttrn.Rows)
        {
            for (i = 0; i < items.Length; i++)
            {
                if (dr.trn_code.Substring(0, 2) == items[i].ToString())
                {
                    lst = new ListItem();
                    lst.Value = dr.trn_code.ToString();
                    lst.Text = dr.trn_code.ToString();
                    ddlplantlist.Items.Add(lst);
                    goto nextsrc;
                }
            }
        nextsrc: ;
        }

    }

    private void load_status()
    {

        ListItem lst;

        chkstatus.Items.Clear();

        lst = new ListItem();
        lst.Value = "APP:ROU:LPO,SPO,FPO,";
        lst.Text = "WAITTING FOR ASSAIGN P-TYPE";
        lst.Selected = true;
        chkstatus.Items.Add(lst);

        lst = new ListItem();
        lst.Value = "APP:RET:LPO,SPO,FPO,";
        lst.Text = "RETURNED FOR CORRECTION";
        lst.Selected = true;
        chkstatus.Items.Add(lst);

        lst = new ListItem();
        lst.Value = "APP:TEN:LPO";
        lst.Text = "WAITTING FOR QUOT ENTRY";
        lst.Selected = true;
        chkstatus.Items.Add(lst);

        lst = new ListItem();
        lst.Value = "APP:QUO:LPO";
        lst.Text = "CS PENDING";
        lst.Selected = true;
        chkstatus.Items.Add(lst);

        lst = new ListItem();
        lst.Value = "APP:APP:LPO";
        lst.Text = "CS APPROVED";
        lst.Selected = true;
        chkstatus.Items.Add(lst);

        lst = new ListItem();
        lst.Value = "APP:TEN:SPO";
        lst.Text = "WAITTING FOR SPO CREATE";
        lst.Selected = true;
        chkstatus.Items.Add(lst);


        lst = new ListItem();
        lst.Value = "APP:TEN:FPO";
        lst.Text = "WAITTING FOR FPO CREATE";
        lst.Selected = true;
        chkstatus.Items.Add(lst);

        
       

        lst = new ListItem();
        lst.Value = "APP:POC:LPO";
        lst.Text = "LPO CREATED";
        lst.Selected = true;
        chkstatus.Items.Add(lst);
        
        lst = new ListItem();
        lst.Value = "APP:POC:SPO";
        lst.Text = "SPO CREATED";
        lst.Selected = true;
        chkstatus.Items.Add(lst);
              

        lst = new ListItem();
        lst.Value = "RUN:RUN:LPO,SPO,FPO,";
        lst.Text = "MPR ON APPROVAL";
        lst.Selected = true;
        chkstatus.Items.Add(lst);

        
        lst = new ListItem();
        lst.Value = "REJ,APP:REJ,RUN:LPO,SPO,FPO,";
        lst.Text = "REJECTED MPR";
        lst.Selected = true;
        chkstatus.Items.Add(lst);

    }

    private void detailview(string ref_no, string item_code)
    {
        

        lbldetail.Visible = true;
        gdItem.Visible = true;

        string status_det = "";
        string hrd_sts = "";
        string mpr_pending = "";
        string mprqty, poqty, poref, recqty, mrrref, insqty;

        DateTime frdate = cldfrom.SelectedDate;
        DateTime todate = cldto.SelectedDate.AddDays(1);
        App_Type_DetTableAdapter appdet = new App_Type_DetTableAdapter();

        PuTr_IN_Hdr_ScblTableAdapter inhdr = new PuTr_IN_Hdr_ScblTableAdapter();
        SCBLDataSet.PuTr_IN_Hdr_ScblDataTable dthdr = new SCBLDataSet.PuTr_IN_Hdr_ScblDataTable();

        PuTr_IN_Det_Scbl2TableAdapter indet = new PuTr_IN_Det_Scbl2TableAdapter();
        SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable dtdet = new SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable();
        PuTr_PO_Det_ScblTableAdapter podet = new PuTr_PO_Det_ScblTableAdapter();
        SCBLDataSet.PuTr_PO_Det_ScblDataTable dtpodet = new SCBLDataSet.PuTr_PO_Det_ScblDataTable();

        InTr_Trn_HdrTableAdapter intrhdr = new InTr_Trn_HdrTableAdapter();
        ErpDataSet.InTr_Trn_HdrDataTable dtintrhdr = new ErpDataSet.InTr_Trn_HdrDataTable();

        dthdr = inhdr.GetDataByRef(ref_no);

        DataTable dt = new DataTable();

        dt.Columns.Clear();
        dt.Columns.Add("MPR", typeof(string));
        dt.Columns.Add("DATE", typeof(string));
        dt.Columns.Add("ICODE", typeof(string));
        dt.Columns.Add("ITEM", typeof(string));
        dt.Columns.Add("MPR QTY", typeof(string));
        dt.Columns.Add("TYPE", typeof(string));
        dt.Columns.Add("PO QTY", typeof(string));
        dt.Columns.Add("PO REF", typeof(string));
        dt.Columns.Add("REC QTY", typeof(string));
        dt.Columns.Add("MRR REF", typeof(string));
        dt.Columns.Add("INS QTY", typeof(string));
        dt.Columns.Add("STATUS", typeof(string));


        foreach (SCBLDataSet.PuTr_IN_Hdr_ScblRow drhdr in dthdr.Rows)
        {

            dtdet = new SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable();
            dtdet = indet.GetDataByRefItem(drhdr.IN_Hdr_Ref, item_code);

            hrd_sts = drhdr.IN_Hdr_Status;
            if (hrd_sts == "RUN")
            {
                mpr_pending = appdet.GetDataByAppName(drhdr.IN_Hdr_Pending.ToString())[0].app_desc;
            }


            foreach (SCBL2DataSet.PuTr_IN_Det_Scbl2Row drdet in dtdet.Rows)
            {
                poref = "";
                recqty = "";
                mrrref = "";
                insqty = "";

                switch (drdet.In_Det_Status)
                {
                    case "REJ":
                        status_det = "REJECTED";
                        break;

                    case "RUN":
                        if (hrd_sts == "REJ")
                        {
                            status_det = "MPR REJECTED";
                        }
                        else
                            if (hrd_sts == "RUN")
                            {
                                status_det = "MPR PENDING FOR " + mpr_pending;
                            }
                            else
                            {
                            }

                        break;

                    case "TEN":
                        if (drdet.In_Det_Pur_Type.ToString() == "LPO")
                        {
                            status_det = "WAITTING FOR QUOT ENTRY";
                        }
                        else
                            if (drdet.In_Det_Pur_Type.ToString() == "SPO")
                            {

                                if (drdet.IN_Det_Lin_Qty == drdet.IN_Det_Org_QTY)
                                {
                                    status_det = "WAITTING FOR SPO CREATE";
                                }
                                else
                                {
                                    status_det = "PARTIAL SPO CREATED";
                                    dtpodet = new SCBLDataSet.PuTr_PO_Det_ScblDataTable();
                                    dtpodet = podet.GetDataByMprRef(drdet.IN_Det_Ref, drdet.IN_Det_Icode);

                                    foreach (SCBLDataSet.PuTr_PO_Det_ScblRow drpodet in dtpodet.Rows)
                                    {
                                        poref = poref + drpodet.PO_Det_Ref + ":" + drpodet.PO_Det_Lin_Qty.ToString("N2") + ",";
                                        insqty = insqty + drpodet.PO_Det_Ins_QTY.ToString() + ",";
                                        recqty = recqty + drpodet.PO_Det_Org_QTY.ToString() + ",";
                                        dtintrhdr = new ErpDataSet.InTr_Trn_HdrDataTable();
                                        dtintrhdr = intrhdr.GetDataByDcNo(drpodet.PO_Det_Ref);

                                        foreach (ErpDataSet.InTr_Trn_HdrRow drintrhdr in dtintrhdr.Rows)
                                        {
                                            mrrref = mrrref + drintrhdr.Trn_Hdr_Ref + ",";
                                        }

                                    }

                                }
                            }
                            else
                            {
                                status_det = "PROCESSING FOR PI APPROVAL";   
                            }


                        break;

                    case "RET":
                        status_det = "RETURNED FOR CORRECTION";
                        break;


                    case "POC":

                        status_det = "PO CREATED";
                        dtpodet = new SCBLDataSet.PuTr_PO_Det_ScblDataTable();
                        dtpodet = podet.GetDataByMprRef(drdet.IN_Det_Ref, drdet.IN_Det_Icode);

                        foreach (SCBLDataSet.PuTr_PO_Det_ScblRow drpodet in dtpodet.Rows)
                        {
                            poref = poref + drpodet.PO_Det_Ref + ":" + drpodet.PO_Det_Lin_Qty.ToString("N2") + ",";
                            insqty = insqty + drpodet.PO_Det_Ins_QTY.ToString() + ",";
                            recqty = recqty + drpodet.PO_Det_Org_QTY.ToString() + ",";
                            dtintrhdr = new ErpDataSet.InTr_Trn_HdrDataTable();
                            dtintrhdr = intrhdr.GetDataByDcNo(drpodet.PO_Det_Ref);

                            foreach (ErpDataSet.InTr_Trn_HdrRow drintrhdr in dtintrhdr.Rows)
                            {
                                mrrref = mrrref + drintrhdr.Trn_Hdr_Ref + ",";
                            }

                        }

                        break;


                    case "QUO":
                        //
                        if (drdet.In_Det_Pur_Type == "FPO")
                        {
                            status_det = "WAITTING FOR PI APPROVAL";
                        }
                        else
                        {
                            status_det = "CS PENDING FOR " + appdet.GetDataByAppName(drdet.In_Det_Status1)[0].app_desc.ToString() + "";
                        }
                        break;

                    case "ROU":
                        status_det = "WAITTING FOR ASSAIGN P-TYPE";
                        break;

                    case "APP":
                        if (drdet.In_Det_Pur_Type.ToString() == "LPO")
                        {
                            status_det = "CS APPROVED";
                        }
                        else
                            if (drdet.In_Det_Pur_Type.ToString() == "SPO")
                            {
                                // status_det = "SPO REALIZATION APPROVED";
                            }
                            else
                            {

                            }

                        break;

                   
                    default:
                        status_det = "";
                        break;

                }

                mprqty = drdet.IN_Det_Lin_Qty.ToString("N2") + " " + drdet.IN_Det_Itm_Uom;
                poqty = drdet.IN_Det_Org_QTY.ToString("N2");
                
                if(drdet.In_Det_Pur_Type!="FPO")
                    dt.Rows.Add(drdet.IN_Det_Ref, drhdr.IN_Hdr_St_DATE.ToShortDateString(), drdet.IN_Det_Icode, drdet.IN_Det_Itm_Desc, mprqty, drdet.In_Det_Pur_Type, poqty, poref, recqty, mrrref, insqty, status_det);
            }

        }



        gdItem.DataSource = dt;
        gdItem.DataBind();

    }


    protected void btnview_Click(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(2000);
        gdItem.Visible = false;
        if (ddlplantlist.SelectedItem.ToString() == "")
        {
            GdHeader.Visible = false;
            return;
        }
        GdHeader.Visible = true;

        string status_det, hrd_sts, mpr_pending;

        DateTime frdate = cldfrom.SelectedDate;
        DateTime todate = cldto.SelectedDate.AddDays(1);


        DtMprStatusTableAdapter indet = new DtMprStatusTableAdapter();
        SCBLQry.DtMprStatusDataTable dtdet = new SCBLQry.DtMprStatusDataTable();

        App_Type_DetTableAdapter appdet = new App_Type_DetTableAdapter();

        DataTable dt = new DataTable();

        dt.Columns.Clear();
        dt.Columns.Add("DETAIL", typeof(string));
        dt.Columns.Add("MPR", typeof(string));
        dt.Columns.Add("DATE", typeof(DateTime));
        dt.Columns.Add("ICODE", typeof(string));
        dt.Columns.Add("ITEM", typeof(string));
        dt.Columns.Add("SPECIFICATION", typeof(string));
        dt.Columns.Add("BRAND", typeof(string));
        dt.Columns.Add("ORIGIN", typeof(string));
        dt.Columns.Add("PACKING", typeof(string));        
        dt.Columns.Add("NATURE OF EXP", typeof(string));
        dt.Columns.Add("LOC OF USE", typeof(string));
        dt.Columns.Add("ETR", typeof(DateTime));
        dt.Columns.Add("REMARKS", typeof(string));
        dt.Columns.Add("MPR QTY", typeof(double));
        dt.Columns.Add("UOM", typeof(string));
        dt.Columns.Add("STATUS", typeof(string));

        
        #region select criteria

        string[] tmp; 

        string In_Hdr_Status1 = "";
        string In_Hdr_Status2 = "";
        string In_Hdr_Status3 = "";

        string In_Det_Status1 = "";
        string In_Det_Status2 = "";
        string In_Det_Status3 = "";
        string In_Det_Status4 = "";
        string In_Det_Status5 = "";
        string In_Det_Status6 = "";
        string In_Det_Status7 = "";
        string In_Det_Status8 = "";

        string Pur_Type1 = "";
        string Pur_Type2 = "";
        string Pur_Type3 = "";
        string Pur_Type4 = "";

        string allhdrsel = "";
        string allstssel = "";
        string allpersel = "";

        foreach (ListItem lst in chkstatus.Items)
        {
            if (lst.Selected) 
            {
                allhdrsel = allhdrsel + lst.Value.ToString().Split(':')[0] + ",";
                allstssel = allstssel + lst.Value.ToString().Split(':')[1] + ",";
                allpersel = allpersel + lst.Value.ToString().Split(':')[2] + ","; 
            }
        }

        tmp = allhdrsel.Split(',');
        foreach (string str in tmp)
        {
            switch(str)
            {
                case "RUN":
                    In_Hdr_Status1 = "RUN";
                    break;
                case "APP":
                    In_Hdr_Status2 = "APP";
                    break;
                case "REJ":
                    In_Hdr_Status3 = "REJ";
                    break;
            }
        }

        tmp = allstssel.Split(',');
        foreach (string str in tmp)
        {
            
            switch (str)
            {
                case "APP":
                    In_Det_Status1 = "APP";
                    break;
                case "POC":
                    In_Det_Status2 = "POC";
                    break;
                case "QUO":
                    In_Det_Status3 = "QUO";
                    break;
                case "REJ":
                    In_Det_Status4 = "REJ";
                    break;
                case "RET":
                    In_Det_Status5 = "RET";
                    break;
                case "ROU":
                    In_Det_Status6 = "ROU";
                    break;
                case "RUN":
                    In_Det_Status7 = "RUN";
                    break;
                case "TEN":
                    In_Det_Status8 = "TEN";
                    break;
            }
        }

        tmp = allpersel.Split(',');
        foreach (string str in tmp)
        {
            switch (str)
            {
                case "":
                    Pur_Type1 = "";
                    break;
                case "LPO":
                    Pur_Type2 = "LPO";
                    break;
                case "SPO":
                    Pur_Type3 = "SPO";
                    break;
                case "FPO":
                    Pur_Type4 = "FPO";
                    break;
            }
        }



        #endregion
                
        dtdet = indet.GetDataMprStatus(ddlplantlist.SelectedItem.ToString(), In_Hdr_Status1, In_Hdr_Status2, In_Hdr_Status3, frdate, todate, In_Det_Status1, In_Det_Status2, In_Det_Status3, In_Det_Status4, In_Det_Status5, In_Det_Status6, In_Det_Status7, In_Det_Status8, Pur_Type1, Pur_Type2, Pur_Type3, Pur_Type4);

        foreach (SCBLQry.DtMprStatusRow drdet in dtdet.Rows)
        {
            hrd_sts = drdet.IN_Hdr_Status;
            if (hrd_sts == "RUN")
            {
                mpr_pending = appdet.GetDataByAppName(drdet.IN_Hdr_Pending.ToString())[0].app_desc;
            }
            else mpr_pending = "";


            status_det = "";
            switch (drdet.In_Det_Status)
            {
                case "REJ":
                    status_det = "REJECTED";
                    break;

                case "RUN":
                    if (hrd_sts == "REJ")
                    {
                        status_det = "MPR REJECTED";
                    }
                    else
                        if (hrd_sts == "RUN")
                        {
                            status_det = "MPR PENDING FOR " + mpr_pending;
                        }
                        else
                        {
                        }

                    break;

                case "TEN":
                    if (drdet.In_Det_Pur_Type.ToString() == "LPO")
                    {
                        status_det = "WAITTING FOR QUOT ENTRY";
                    }
                    else
                        if (drdet.In_Det_Pur_Type.ToString() == "SPO")
                        {

                            if (drdet.IN_Det_Lin_Qty == drdet.IN_Det_Bal_Qty)
                            {
                                status_det = "WAITTING FOR SPO CREATE";
                            }
                            else
                            {
                                status_det = "PARTIAL SPO CREATED";                                    

                            }
                        }
                        else
                        {
                            status_det = "PROCESSING FOR PI APPROVAL";            
                        }


                    break;

                case "POC":

                    status_det = "PO CREATED";
                    

                    break;


                case "QUO":
                    //
                   
                    if (drdet.In_Det_Pur_Type == "FPO")
                    {
                        status_det = "WAITTING FOR PI APPROVAL";
                    }
                    else
                    {
                        status_det = "CS PENDING FOR " + appdet.GetDataByAppName(drdet.In_Det_Status1)[0].app_desc.ToString() + "";
                    }
                    break;

                case "ROU":
                    status_det = "WAITTING FOR ASSAIGN P-TYPE";
                    break;

                case "APP":
                    if (drdet.In_Det_Pur_Type.ToString() == "LPO")
                    {
                        status_det = "CS APPROVED";
                    }
                    else
                        if (drdet.In_Det_Pur_Type.ToString() == "SPO")
                        {
                            // status_det = "SPO REALIZATION APPROVED";
                        }
                        else
                        {

                        }

                    break;

                //case "ADV":
                //    status_det = "SPO ADVANCE APPROVED";
                //    break;

                //case "ADRUN":
                //    status_det = "WAITTING FOR SPO ADVANCE APPROVAL";
                //    break;


                default:
                    status_det = "";
                    break;

            }

            dt.Rows.Add("", drdet.IN_Det_Ref, drdet.IN_Hdr_St_DATE, drdet.IN_Det_Icode, drdet.IN_Det_Itm_Desc, drdet.In_Det_Specification, drdet.In_Det_Brand, drdet.In_Det_Origin, drdet.In_Det_Packing, drdet.In_Det_Noe, drdet.In_Det_Loc, drdet.IN_Det_Exp_Dat, drdet.In_Det_Remarks, drdet.IN_Det_Lin_Qty, drdet.IN_Det_Itm_Uom, status_det);
      
        }

        if (dt.Rows.Count > 0) { btnexport.Visible = true; } else { btnexport.Visible = false; }       
        
        ViewState[clsStatic.ViewStateDataTable] = dt;
        GdHeader.DataSource = dt;
        GdHeader.DataBind();

    }
    protected void gdItem_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GdHeader_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName.Equals("Sort"))
        {

            DataTable dttemp = new DataTable();
            dttemp = (DataTable)ViewState[clsStatic.ViewStateDataTable];


            if (ViewState[clsStatic.ViewStateSortDirection] != null)
                if ((SortDirection)ViewState[clsStatic.ViewStateSortDirection] == SortDirection.Descending)
                {

                    dttemp.DefaultView.Sort = e.CommandArgument + "  ASC";
                    ViewState[clsStatic.ViewStateSortDirection] = SortDirection.Ascending;
                }
                else
                {
                    dttemp.DefaultView.Sort = e.CommandArgument + "  DESC";
                    ViewState[clsStatic.ViewStateSortDirection] = SortDirection.Descending;
                }
            else
            {
                dttemp.DefaultView.Sort = e.CommandArgument + "  ASC";
                ViewState[clsStatic.ViewStateSortDirection] = SortDirection.Ascending;
            }

            

            GdHeader.DataSource = dttemp;
            GdHeader.DataBind();



        }



    }

    protected void GdHeader_SelectedIndexChanged(object sender, EventArgs e)
    {
        AddSortImage(GdHeader.HeaderRow);

        int indx = GdHeader.SelectedIndex;
        //string ref_no, itm_code;
        lbldetail.Visible = false;
        gdItem.Visible = false;

        if (indx != -1)
        {
            //ref_no = GdHeader.Rows[indx].Cells[0].Text.Trim();
            //itm_code = GdHeader.Rows[indx].Cells[2].Text.Trim();
            //detailview(ref_no, itm_code);

            //RegisterStartupScript("click", "<script>window.open('./frm_mpr_lifecycle.aspx?ref_no=" + ref_no + "&itm_code=" + itm_code + "');</script>");
        }
       
    }

    protected void GdHeader_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState[clsStatic.ViewStateSortExpression] = e.SortExpression;
        AddSortImage(GdHeader.HeaderRow);
    }

    

    private void AddSortImage( GridViewRow headerRow)
    {

        if (ViewState[clsStatic.ViewStateSortExpression] == null) return;

        int columnIndex = 1;
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState[clsStatic.ViewStateDataTable];
        if (dt == null) return;
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            if (dt.Columns[i].Caption == ViewState[clsStatic.ViewStateSortExpression].ToString())
            {
                columnIndex = i;
            }
        }
        

        Image sortImage = new Image();

        if (ViewState[clsStatic.ViewStateSortDirection] == null) return;

        if ((SortDirection)ViewState[clsStatic.ViewStateSortDirection] == SortDirection.Ascending)
            sortImage.ImageUrl = "~/images/group_arrow_top.gif";
        else
             sortImage.ImageUrl = "~/images/group_arrow_bottom.gif";
      
        headerRow.Cells[columnIndex].Controls.Add(sortImage);

    }


    

    protected void GdHeader_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        HtmlInputButton btn;
        string ref_no;
        string itm_code;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            btn = new HtmlInputButton();

            ref_no = e.Row.Cells[1].Text;
            itm_code = e.Row.Cells[3].Text;

            //e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
            //e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
            //e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GdHeader, "Select$" + e.Row.RowIndex);

            btn.Attributes.Add("onclick ", "window.open('./frm_mpr_lifecycle.aspx?ref_no=" + ref_no + "&itm_code=" + itm_code + "')");
            btn.ID = "btnnewwindow" + e.Row.ClientID;            
            btn.Value = "View";
            btn.Attributes.Add("class", "btn2");
            e.Row.Cells[0].Controls.Add(btn);
        }

    }

  
    protected void btnexport_Click(object sender, EventArgs e)
    {
        clsStatic.Export("MPR_Status.xls", GdHeader);
    }
}


