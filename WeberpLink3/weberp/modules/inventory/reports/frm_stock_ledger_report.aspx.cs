using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.IO;
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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using LibraryDAL.SCBLINTableAdapters;
using LibraryDAL.ErpDataSetTableAdapters;
using LibraryDAL.AccDataSetTableAdapters;

public partial class frm_stock_ledger_report : System.Web.UI.Page
{
    ReportDocument rpt1 = new ReportDocument();
   
    protected void Page_Load(object sender, EventArgs e)
    {

        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";

        if (!Page.IsPostBack)
        {
            load_type();
            load_store();
            cldfrom.SelectedDate = DateTime.Now.AddDays((-1)*(DateTime.Now.Day-1));
            cldto.SelectedDate = DateTime.Now;

        }
        else
        {
            showreport();
        }
       
          
    }



    private void load_store()
    {
        InMa_Str_LocTableAdapter store = new InMa_Str_LocTableAdapter();
        ErpDataSet.InMa_Str_LocDataTable dtstore = new ErpDataSet.InMa_Str_LocDataTable();
        ListItem lst;
        dtstore = store.GetAllStore();

        chkstore.Items.Clear();
       

        foreach (ErpDataSet.InMa_Str_LocRow dr in dtstore.Rows)
        {
            lst = new ListItem();
            lst.Text = dr.Str_Loc_Id + ":" + dr.Str_Loc_Name;
            lst.Value = dr.Str_Loc_Id;
            lst.Selected = true;
            chkstore.Items.Add(lst);

        }

    }

    private void load_type()
    {
        
        chktype.Items.Clear();

        ListItem lst;

        lst = new ListItem();
        lst.Text = "Finish Goods";
        lst.Value = "F";
        chktype.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "Raw Material";
        lst.Value = "R";
        chktype.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "Process Material";
        lst.Value = "P";
        chktype.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "Packing Material";
        lst.Value = "K";
        chktype.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "Mechanical";
        lst.Value = "M";
        chktype.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "Electrical";
        lst.Value = "E";
        chktype.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "Tools";
        lst.Value = "T";
        chktype.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "General Hardware";
        lst.Value = "H";
        chktype.Items.Add(lst);
              

        lst = new ListItem();
        lst.Text = "Fuel & Lubricant";
        lst.Value = "L";
        chktype.Items.Add(lst);
        //
        lst = new ListItem();
        lst.Text = "Civil";
        lst.Value = "C";
        chktype.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "Domestic Equipments";
        lst.Value = "D";
        chktype.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "Wastage and Scrap";
        lst.Value = "W";
        chktype.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "Fixed Asset";
        lst.Value = "A";
        chktype.Items.Add(lst);

         lst = new ListItem();
        lst.Text = "IT Equipments";
        lst.Value = "I";
        chktype.Items.Add(lst);

         lst = new ListItem();
        lst.Text = "Auto Mobile";
        lst.Value = "V";
        chktype.Items.Add(lst);
        

    }


    protected void btnview_Click(object sender, EventArgs e)
    {

        if (chkstore.SelectedIndex == -1) return;
        
        InMa_Itm_DetTableAdapter itm = new InMa_Itm_DetTableAdapter();
        ErpDataSet.InMa_Itm_DetDataTable dtitm = new ErpDataSet.InMa_Itm_DetDataTable();
        //tbl_store_ledgerTableAdapter led = new tbl_store_ledgerTableAdapter();

        tbl_str_inv_reportTableAdapter rep = new tbl_str_inv_reportTableAdapter();
        InMa_Itm_GrpTableAdapter grp = new InMa_Itm_GrpTableAdapter();
        ErpDataSet.InMa_Itm_GrpDataTable dtgrp = new ErpDataSet.InMa_Itm_GrpDataTable();
        InMa_Grp_CodeTableAdapter gcode = new InMa_Grp_CodeTableAdapter();
        DataTable dtglo = new DataTable();
        string prm = "";
        string strname = "";
        string[] tmp;
        string fromitm, toitm,  grp1c, grp1n, grp2c, grp2n, grp3c, grp3n;
        decimal opbal, clbal, issue, addpur, opbalqty, clbalqty, issueqty, addpurqty ;

        foreach (ListItem lst in chkstore.Items)
            if (lst.Selected)
                if (strname == "") strname = lst.Value.ToString(); else strname += "," + lst.Value.ToString();

        if (chktype.SelectedIndex == -1) { return; }

        foreach (ListItem lst in chktype.Items)
        {
            if (lst.Selected)
            {
                if (prm == "")
                {
                    prm = lst.Value.ToString(); 
                }
                else
                {
                    prm = prm + "," + lst.Value.ToString();
                }
            }
        }

      


        if (chkall.Checked)
        {
            dtitm = itm.GetAllReport(prm);
        }
        else
        {
            tmp = txtitemfrom.Text.Split(':');
            if (tmp.Length < 3) return;
            if (itm.GetItemByCode(tmp[0]).Count > 0) { fromitm = tmp[0]; } else { return; }

            tmp = txtitemto.Text.Split(':');
            if (tmp.Length < 3) return;
            if (itm.GetItemByCode(tmp[0]).Count > 0) { toitm = tmp[0]; } else { return; }

            dtitm = itm.GetDataByItmRange(prm, fromitm, toitm);
        }

        //led.DeleteAll();

        dtglo.Columns.Clear();
        dtglo.Rows.Clear();
        dtglo.Columns.Add("itm_code", typeof(string));
        dtglo.Columns.Add("itm_name", typeof(string));
        dtglo.Columns.Add("uom", typeof(string));
        dtglo.Columns.Add("store_code", typeof(string));
        dtglo.Columns.Add("store_name", typeof(string));
        dtglo.Columns.Add("grp1_code", typeof(string));
        dtglo.Columns.Add("grp1_name", typeof(string));
        dtglo.Columns.Add("grp2_code", typeof(string));
        dtglo.Columns.Add("grp2_name", typeof(string));
        dtglo.Columns.Add("grp3_code", typeof(string));
        dtglo.Columns.Add("grp3_name", typeof(string));
        dtglo.Columns.Add("opening_bal", typeof(decimal));
        dtglo.Columns.Add("opening_bal_qty", typeof(decimal));
        dtglo.Columns.Add("add_purchase", typeof(decimal));
        dtglo.Columns.Add("add_purchase_qty", typeof(decimal));
        dtglo.Columns.Add("consumption", typeof(decimal));
        dtglo.Columns.Add("consumption_qty", typeof(decimal));
        dtglo.Columns.Add("closing_bal", typeof(decimal));
        dtglo.Columns.Add("closing_bal_qty", typeof(decimal));



        foreach (ErpDataSet.InMa_Itm_DetRow dr in dtitm.Rows)
        {
            opbal = Convert.ToDecimal(rep.GetOpBal(strname, dr.Itm_Det_Icode, "RC", "FI", "SR", "RT", "P", "P", "B", cldfrom.SelectedDate)) - Convert.ToDecimal(rep.GetOpBal(strname, dr.Itm_Det_Icode, "IS", "IT", "", "", "P", "P", "B", cldfrom.SelectedDate)) - Convert.ToDecimal(rep.GetOpBalDc(strname, dr.Itm_Det_Icode, "II", "", "", "", "P", "P", "B", cldfrom.SelectedDate)); ;
            issue = Convert.ToDecimal(rep.GetTrnval(strname, dr.Itm_Det_Icode, "IS", "IT", "", "", "P", "P", "B", cldfrom.SelectedDate, cldto.SelectedDate)) + Convert.ToDecimal(rep.GetTrnValDc(strname, dr.Itm_Det_Icode, "II", "", "", "", "P", "P", "B", cldfrom.SelectedDate, cldto.SelectedDate.AddDays(1)));
            addpur = Convert.ToDecimal(rep.GetTrnval(strname, dr.Itm_Det_Icode, "RC", "FI", "SR", "RT", "P", "P", "B", cldfrom.SelectedDate, cldto.SelectedDate));
            clbal = opbal + addpur - issue;

            opbalqty = Convert.ToDecimal(rep.GetOpBalQty(strname, dr.Itm_Det_Icode, "RC", "FI", "SR", "RT", "P", "P", "B", cldfrom.SelectedDate)) - Convert.ToDecimal(rep.GetOpBalQty(strname, dr.Itm_Det_Icode, "IS", "IT", "", "", "P", "P", "B", cldfrom.SelectedDate)) - Convert.ToDecimal(rep.GetOpBalQtyDc(strname, dr.Itm_Det_Icode, "II", "", "", "", "P", "P", "B", cldfrom.SelectedDate)); ; ;
            issueqty = Convert.ToDecimal(rep.GetTrnvalQty(strname, dr.Itm_Det_Icode, "IS", "IT", "", "", "P", "P", "B", cldfrom.SelectedDate, cldto.SelectedDate)) + Convert.ToDecimal(rep.GetTrnValQtyDc(strname, dr.Itm_Det_Icode, "II", "", "", "", "P", "P", "B", cldfrom.SelectedDate, cldto.SelectedDate.AddDays(1)));
            addpurqty = Convert.ToDecimal(rep.GetTrnvalQty(strname, dr.Itm_Det_Icode, "RC", "FI", "SR", "RT", "P", "P", "B", cldfrom.SelectedDate, cldto.SelectedDate));
            clbalqty = opbalqty + addpurqty - issueqty;

            //if (clbalqty != 0)
            //{

                dtgrp = grp.GetDataByIcode(dr.Itm_Det_Icode, "I01");
                if (dtgrp.Count == 0)
                {
                    grp1c = "";
                    grp1n = "";
                }
                else
                {
                    grp1c = dtgrp[0].Itm_Grp_Code;
                    grp1n = gcode.GetDataByAllGrp("I01", "", "", grp1c)[0].Grp_Code_Name;

                }


                dtgrp = new ErpDataSet.InMa_Itm_GrpDataTable();
                dtgrp = grp.GetDataByIcode(dr.Itm_Det_Icode, "I02");
                if (dtgrp.Count == 0)
                {
                    grp2c = "";
                    grp2n = "";
                }
                else
                {
                    grp2c = dtgrp[0].Itm_Grp_Code;
                    try
                    {
                        grp2n = gcode.GetDataByAllGrp("I02", grp1c, "", grp2c)[0].Grp_Code_Name;
                    }
                    catch
                    {
                        grp2n = "";
                    }
                }

                dtgrp = new ErpDataSet.InMa_Itm_GrpDataTable();
                dtgrp = grp.GetDataByIcode(dr.Itm_Det_Icode, "I03");
                if (dtgrp.Count == 0)
                {
                    grp3c = "";
                    grp3n = "";
                }
                else
                {
                    grp3c = dtgrp[0].Itm_Grp_Code;
                    try
                    {
                        grp3n = gcode.GetDataByAllGrp("I03", grp1c, grp2c, grp3c)[0].Grp_Code_Name;
                    }
                    catch
                    {
                        grp3n = "";
                    }
                }

                dtglo.Rows.Add(dr.Itm_Det_Icode, dr.Itm_Det_desc, dr.Itm_Det_PUSA_unit, dr.Itm_Det_add_des2, strname, grp1c, grp1n, grp2c, grp2n, grp3c, grp3n, opbal, opbalqty, addpur, addpurqty, issue, issueqty, clbal, clbalqty);
                //led.InsertStrLed(dr.Itm_Det_Icode, dr.Itm_Det_desc, dr.Itm_Det_PUSA_unit, strname, strname, grp1c, grp1n, grp2c, grp2n, grp3c, grp3n, opbal, opbalqty, addpur, addpurqty, issue, issueqty, clbal, clbalqty);
            //}
            
        }

        ViewState["dt"] = dtglo;

        showreport();

    }

    private void showreport()
    {

        if (ViewState["dt"] == null) { return; }

        //DataTable dtglo = new DataTable();
        //tbl_store_ledgerTableAdapter sto = new tbl_store_ledgerTableAdapter();
        string DateF = cldfrom.SelectedDate.ToShortDateString();
        string Datet = cldto.SelectedDate.ToShortDateString();
        string strname = "";
        foreach (ListItem lst in chkstore.Items)
            if (lst.Selected)
                if (strname == "") strname = lst.Value.ToString(); else strname += "," + lst.Value.ToString();
        strname = "STORE : " + strname;
        //dtglo = sto.GetData();
        DataTable dtglo = new DataTable();
        dtglo = (DataTable)ViewState["dt"];

        if (RdoList.SelectedIndex == 0)
        {
            rpt1.Load(Server.MapPath("files/rpt_store_ledger_avg.rpt"));
            rpt1.SetDataSource(dtglo);
        }
        else if (RdoList.SelectedIndex == 1)
        {
            rpt1.Load(Server.MapPath("files/rpt_store_ledger.rpt"));
            rpt1.SetDataSource(dtglo);
        }
        else if (RdoList.SelectedIndex == 2)
        {
            rpt1.Load(Server.MapPath("files/rpt_store_ledger_qty.rpt"));
            rpt1.SetDataSource(dtglo);
        }

         else if (RdoList.SelectedIndex == 3)
        {
            rpt1.Load(Server.MapPath("files/rpt_store_ledger_val_qty.rpt"));
            rpt1.SetDataSource(dtglo);
        }
        
        
       
        rpt1.DataDefinition.FormulaFields["DFrm"].Text = "'" + DateF + "'";
        rpt1.DataDefinition.FormulaFields["DTo"].Text = "'" + Datet + "'";
        rpt1.DataDefinition.FormulaFields["prm_store"].Text = "'" + strname + "'";
        CrystalReportViewer1.ReportSource = rpt1;
        CrystalReportViewer1.DataBind();


            //ViewState["dt"] = null;
            //rpt1.Close();
            //rpt1.Dispose();
        //RegisterStartupScript("click", "<script>window.open('./frm_report_viewer.aspx');</script>");
        
    }


    private string formulapass(string prm, string pname, string value)
    {
        if (prm == "")
            return pname + ":" + value;
        else
            return "," + pname + ":" + value;

        return prm;
    }

    private void parameterpass(ParameterFields myParams, string pname, string value)
    {
        ParameterField param = new ParameterField();
        ParameterDiscreteValue dis1 = new ParameterDiscreteValue();

        param.ParameterFieldName = pname;
        dis1.Value = value;
        param.CurrentValues.Add(dis1);
        myParams.Add(param);

    }


    protected void CrystalReportViewer1_Unload(object sender, EventArgs e)
    {
        rpt1.Close();
        rpt1.Dispose();
        GC.Collect();
    }
}


