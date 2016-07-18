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
using LibraryDAL.SCBL2DataSetTableAdapters;
using LibraryDAL.SCBL3DataSetTableAdapters;
using LibraryDAL.FpiDataSetTableAdapters;
using LibraryDAL.SCBLINTableAdapters;
using LibraryDAL.SCBLQryTableAdapters;

public partial class frm_inbox : System.Web.UI.Page
{   
    protected void Page_Load(object sender, EventArgs e)
    {
        //current.UserId = "MON";
        //current.UserName = "MONJU";
                 
        tblmaster.BgColor = "FFFFFFF";

        if (!Page.IsPostBack)
        {
            get_all_pending();
        }
        else
        {
        }        
    }

    private string[] get_plant(string apptype)
    {
        User_Role_DefinitionTableAdapter urole = new User_Role_DefinitionTableAdapter();
        SCBLDataSet.User_Role_DefinitionDataTable udt = new SCBLDataSet.User_Role_DefinitionDataTable();
        string[] plant_list;
        udt = urole.GetDataByUserCodeRole(current.UserId.ToString(), apptype);

        if (udt.Rows.Count > 0)
            plant_list = udt[0].plant_list.Split(',');
        else
            return null;

        return plant_list;
    }

    private int get_routing_count()
    {
        PuTr_IN_Det_ScblTableAdapter srdet = new PuTr_IN_Det_ScblTableAdapter();
        SCBLDataSet.PuTr_IN_Det_ScblDataTable dtbyreq = new SCBLDataSet.PuTr_IN_Det_ScblDataTable();
        int i, len, indx, cnt;    
        cnt = dtbyreq.Rows.Count;
        string[] plant_list = get_plant("ROU");

        if (plant_list == null)
        {
            return 0;
        }

        len = plant_list.Length;
        dtbyreq = srdet.GetPendingByRole("ROU");
        cnt = dtbyreq.Rows.Count;

        for (indx = cnt; indx > 0; indx--)
        {

            for (i = 0; i < len; i++)
            {
                if (dtbyreq[indx - 1].IN_Det_Code.Substring(0, 2) == plant_list[i])
                    goto nextcheck1;

            }
            dtbyreq.RemovePuTr_IN_Det_ScblRow(dtbyreq[indx - 1]);


        nextcheck1: ;
        }

        return dtbyreq.Rows.Count;

    }

    private int get_tender_count()
    {

        SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable itm = new SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable();
        PuTr_IN_Det_Scbl2TableAdapter det = new PuTr_IN_Det_Scbl2TableAdapter();

        int i, len, cnt, indx;
        string[] plant_list = get_plant("TEN");

        if (plant_list == null)
        {           
            return 0;
        }

        len = plant_list.Length;

        
        itm = det.GetDataByReqStatus("LPO", "TEN");

        cnt = itm.Rows.Count;
        for (indx = cnt; indx > 0; indx--)
        {

            for (i = 0; i < len; i++)
            {
                if (itm[indx - 1].IN_Det_Code.Substring(0, 2) == plant_list[i])
                    goto nextcheck1;
            }
            itm.RemovePuTr_IN_Det_Scbl2Row(itm[indx - 1]);

        nextcheck1: ;
        }
        
        return itm.Rows.Count;
       
    }

    private int get_quot_count()
    {
        PuTr_IN_Det_Scbl2TableAdapter srdet = new PuTr_IN_Det_Scbl2TableAdapter();
        SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable dtdet = new SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable();

      
        int i, len, cnt, indx;


        // For Quotetion entry permission as QEN but database as TEN

        string[] plant_list = get_plant("QEN");
        if (plant_list == null)
        {            
            return 0;
        }

        len = plant_list.Length;

        
        // For Quotetion entry permission as QEN but database as TEN
        dtdet = srdet.GetDataByReqStatus("LPO", "TEN");


        cnt = dtdet.Rows.Count;

        for (indx = cnt; indx > 0; indx--)
        {

            for (i = 0; i < len; i++)
            {
                if (dtdet[indx - 1].IN_Det_Code.Substring(0, 2) == plant_list[i])
                    goto nextcheck1;

            }
            dtdet.RemovePuTr_IN_Det_Scbl2Row(dtdet[indx - 1]);


        nextcheck1: ;
        }

        return dtdet.Rows.Count;
            
    }

    private int get_pi_inq_count()
    {
        PuTr_IN_Det_Scbl2TableAdapter srdet = new PuTr_IN_Det_Scbl2TableAdapter();
        SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable dtdet = new SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable();

        dtdet = srdet.GetDataByReqStatus("FPO", "TEN");      

        return dtdet.Rows.Count;

    }
    private int get_pi_entry_count()
    {
        PuTr_IN_Det_Scbl2TableAdapter srdet = new PuTr_IN_Det_Scbl2TableAdapter();
        SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable dtdet = new SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable();
        
        dtdet = srdet.GetDataByReqStatus("FPO", "TEN");


        return dtdet.Rows.Count;

    }

    private int get_consignment_entry_count()
    {
        tbl_consignment_pendingTableAdapter cons = new tbl_consignment_pendingTableAdapter();       
        return cons.GetData().Rows.Count;
    }

    private int get_custom_entry_pending_count()
    {
        tbl_fpi_consignment_infoTableAdapter cons = new tbl_fpi_consignment_infoTableAdapter();
        return cons.GetDataByStatus("APP").Rows.Count;
    }

    private int get_custom_app_pending_count()
    {
        tbl_fpi_consignment_infoTableAdapter cons = new tbl_fpi_consignment_infoTableAdapter();
        return cons.GetDataByStatus("RUN").Rows.Count;
    }

    private int get_ctg_entry_pending_count()
    {
        getPendingCustomTableAdapter custom = new getPendingCustomTableAdapter();
        return custom.GetDataPendingCustom("RUN").Rows.Count;

    }

    private int get_survey_entry_pending_count()
    {
        getPendingCustomTableAdapter custom = new getPendingCustomTableAdapter();

        return custom.GetDataBySurLoanPending("RUN").Rows.Count;

    }

    private bool get_survey_status_count()
    {
        getPendingCustomTableAdapter custom = new getPendingCustomTableAdapter();

        return (custom.GetDataBySurStatus("RUN").Rows.Count > 0 ? true : false);

    }

   

    private int get_lpoc_count()
    {
        PuTr_IN_Det_Scbl2TableAdapter det = new PuTr_IN_Det_Scbl2TableAdapter();
        SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable dt = new SCBL2DataSet.PuTr_IN_Det_Scbl2DataTable();
       
       
        int i, len, cnt, indx;
        string[] plant_list = get_plant("LPOC");

        if (plant_list == null) return 0;
        
        len = plant_list.Length;

        dt = det.GetDataByReqStatus("LPO", "APP");
        cnt = dt.Rows.Count;
        for (indx = cnt; indx > 0; indx--)
        {

            for (i = 0; i < len; i++)
            {
                if (dt[indx - 1].IN_Det_Code.Substring(0, 2) == plant_list[i])
                    goto nextcheck1;
            }
            dt.RemovePuTr_IN_Det_Scbl2Row(dt[indx - 1]);

        nextcheck1: ;
        }

        return dt.Rows.Count;
        
    }

    private int get_adv_pay_count()
    {
        tbl_spo_advance_hdrTableAdapter hdr = new tbl_spo_advance_hdrTableAdapter();
        SCBL2DataSet.tbl_spo_advance_hdrDataTable dt = new SCBL2DataSet.tbl_spo_advance_hdrDataTable();

        int cnt, i, indx, len;


        dt = hdr.GetDataByStatus("APP");

        string[] plant_list = get_plant("ADVPAY");
        if (plant_list == null) return 0;
        len = plant_list.Length;

        cnt = dt.Rows.Count;

        for (indx = cnt; indx > 0; indx--)
        {
            for (i = 0; i < len; i++)
            {
                if (dt[indx - 1].plant == plant_list[i])
                    goto nextcheck1;

            }
            dt.Removetbl_spo_advance_hdrRow(dt[indx - 1]);


        nextcheck1: ;
        }

        return dt.Rows.Count;
    }

    private int get_adv_pay_lpo_count()
    {
        tbl_chq_voucherTableAdapter hdr = new tbl_chq_voucherTableAdapter();
        SCBL3DataSet.tbl_chq_voucherDataTable dt = new SCBL3DataSet.tbl_chq_voucherDataTable();
       
        int cnt, i, indx, len;


        dt = hdr.GetDataByStatus("INI");

        string[] plant_list = get_plant("ADVPAY");
        if (plant_list == null) return 0;
        len = plant_list.Length;

        cnt = dt.Rows.Count;

        for (indx = cnt; indx > 0; indx--)
        {
            for (i = 0; i < len; i++)
            {
                if (dt[indx - 1].ref_no.Substring(3, 2) == plant_list[i])
                    goto nextcheck1;

            }
            dt.Removetbl_chq_voucherRow(dt[indx - 1]);


        nextcheck1: ;
        }

        return dt.Rows.Count;
    }

    private int get_spoini_count()
    {
        
        SCBLDataSet.PuTr_IN_Det_ScblDataTable itm = new SCBLDataSet.PuTr_IN_Det_ScblDataTable();
        PuTr_IN_Det_ScblTableAdapter det = new PuTr_IN_Det_ScblTableAdapter();        
        
        int cnt, i, indx, len;

        string[] plant_list = get_plant("SPOC");
        if (plant_list == null) return 0;
        len = plant_list.Length;

        itm = det.GetDataByReqStatusForIni("SPO", "TEN","");
        cnt = itm.Rows.Count;
        

        for (indx = cnt; indx > 0; indx--)
        {
            for (i = 0; i < len; i++)
            {
                if (itm[indx - 1].IN_Det_Code.Substring(0, 2) == plant_list[i])
                    goto nextcheck1;

            }
            itm.RemovePuTr_IN_Det_ScblRow(itm[indx - 1]);


        nextcheck1: ;
        }

        return itm.Rows.Count;
    }

    private int get_real_ini_count()
    {
        PuTr_PO_Hdr_ScblTableAdapter pohdr = new PuTr_PO_Hdr_ScblTableAdapter();
        SCBLDataSet.PuTr_PO_Hdr_ScblDataTable dthdr = new SCBLDataSet.PuTr_PO_Hdr_ScblDataTable();
       
        int i, len, indx, cnt;

        string[] plant_list = get_plant("SPOC");
        if (plant_list == null)           return 0;

        len = plant_list.Length;

        dthdr = pohdr.GetDataByStatus("ADV");

        cnt = dthdr.Rows.Count;
        for (indx = cnt; indx > 0; indx--)
        {

            for (i = 0; i < len; i++)
            {
                if (dthdr[indx - 1].PO_Hdr_Code.Substring(0, 2) == plant_list[i])
                    goto nextcheck1;
            }
            dthdr.RemovePuTr_PO_Hdr_ScblRow(dthdr[indx - 1]);

        nextcheck1: ;
        }

        return dthdr.Rows.Count;
    }

    private int get_adr_assign_count()
    {
        SCBLDataSet.PuTr_IN_Det_ScblDataTable itm = new SCBLDataSet.PuTr_IN_Det_ScblDataTable();
        PuTr_IN_Det_ScblTableAdapter det = new PuTr_IN_Det_ScblTableAdapter();
        
        itm = det.GetAdrAssign("APP");

        if (itm == null) 
            return 0;
        else
            return itm.Rows.Count;

    }
          
    private void get_all_pending()
    {
        User_Role_DefinitionTableAdapter role = new User_Role_DefinitionTableAdapter();
        SCBLDataSet.User_Role_DefinitionDataTable roledt = new SCBLDataSet.User_Role_DefinitionDataTable();
        string usercode = current.UserId.ToString();
        roledt = role.GetAllrolebyUser(usercode);
        
        int cnt = 0;
        int tot = 0;


        foreach (SCBLDataSet.User_Role_DefinitionRow dr in roledt.Rows)
        {
            switch (dr.role_type)
            {

                case "GEN":


                    switch (dr.role_as)
                    {
                        case "ROU":

                            cnt = get_routing_count();
                            if (cnt == 0)
                            {
                                lnkforword.Visible = false;
                            }
                            else
                            {
                                lnkforword.Visible = true;
                                lnkforword.Text = lnkforword.Text + " (" + cnt.ToString() + ")";
                                tot = tot + cnt;
                            }

                            break;

                        case "TEN":

                            cnt = get_tender_count();
                            if (cnt == 0)
                            {
                                lnktender.Visible = false;
                            }
                            else
                            {
                                lnktender.Visible = true;
                                lnktender.Text = lnktender.Text + " (" + cnt.ToString() + ")";
                                tot = tot + cnt;
                            }

                            break;

                        case "QEN":
                            cnt = get_quot_count();
                            if (cnt == 0)
                            {
                                lnkquo.Visible = false;
                            }
                            else
                            {
                                lnkquo.Visible = true;
                                lnkquo.Text = lnkquo.Text + " (" + cnt.ToString() + ")";
                                tot = tot + cnt;
                            }

                            break;

                        case "PIINQ":
                            cnt = get_pi_inq_count();
                            if (cnt == 0)
                            {
                                lnkpiinq.Visible = false;
                            }
                            else
                            {
                                lnkpiinq.Visible = true;
                                lnkpiinq.Text = lnkpiinq.Text + " (" + cnt.ToString() + ")";
                                tot = tot + cnt;
                            }

                            break;

                        case "PIENT":
                            cnt = get_pi_entry_count();
                            if (cnt == 0)
                            {
                                lnkpientry.Visible = false;
                            }
                            else
                            {
                                lnkpientry.Visible = true;
                                lnkpientry.Text = lnkpientry.Text + " (" + cnt.ToString() + ")";
                                tot = tot + cnt;
                            }

                            break;

                        case "CONENT":
                            cnt = get_consignment_entry_count();
                            if (cnt == 0)
                            {
                                lnkconsignment.Visible = false;
                            }
                            else
                            {
                                lnkconsignment.Visible = true;
                                lnkconsignment.Text = lnkconsignment.Text + " (" + cnt.ToString() + ")";
                                tot = tot + cnt;
                            }

                            break;


                        case "CUSENT":
                            cnt = get_custom_entry_pending_count();
                            if (cnt == 0)
                            {
                                lnkcustentry.Visible = false;
                            }
                            else
                            {
                                lnkcustentry.Visible = true;
                                lnkcustentry.Text = lnkcustentry.Text + " (" + cnt.ToString() + ")";
                                tot = tot + cnt;
                            }

                            break;

                            
                        case "CUSAPP":
                            cnt = get_custom_app_pending_count();
                            if (cnt == 0)
                            {
                                lnkcustapp.Visible = false;
                            }
                            else
                            {
                                lnkcustapp.Visible = true;
                                lnkcustapp.Text = lnkcustapp.Text + " (" + cnt.ToString() + ")";
                                tot = tot + cnt;
                            }

                            break;

                       

                        case "CTG":
                            cnt = get_ctg_entry_pending_count();
                            if (cnt == 0)
                            {
                                lnkctgentry.Visible = false;
                            }
                            else
                            {
                                lnkctgentry.Visible = true;
                                lnkctgentry.Text = lnkctgentry.Text + " (" + cnt.ToString() + ")";
                                tot = tot + cnt;
                            }

                            break;

                        case "SER":
                            cnt = get_survey_entry_pending_count();
                            lnkserveyentry.Visible = get_survey_status_count();
                                                       
                            lnkserveyentry.Text = lnkserveyentry.Text + " (" + cnt.ToString() + ")";
                            tot = tot + cnt;
                           
                            break;

                       
                            
                            

                        case "LPOC":

                            cnt = get_lpoc_count();
                            if (cnt == 0)
                            {
                                lnklpocreate.Visible = false;
                            }
                            else
                            {
                                lnklpocreate.Visible = true;
                                lnklpocreate.Text = lnklpocreate.Text + " (" + cnt.ToString() + ")";
                                tot = tot + cnt;
                            }

                            break;

                       
                        case "SPOC":
                            cnt = get_spoini_count();
                            if (cnt == 0)
                            {
                                lnkspocreate.Visible = false;
                            }
                            else
                            {
                                lnkspocreate.Visible = true;
                                lnkspocreate.Text = lnkspocreate.Text + " (" + cnt.ToString() + ")";
                                tot = tot + cnt;
                            }

                            cnt = get_real_ini_count();
                            if (cnt == 0)
                            {
                                lnksporealini.Visible = false;
                            }
                            else
                            {
                                lnksporealini.Visible = true;
                                lnksporealini.Text = lnksporealini.Text + " (" + cnt.ToString() + ")";
                                tot = tot + cnt;
                            }
                           
                            break;

                        case "ADR":

                            cnt = get_adr_assign_count();
                            if (cnt == 0)
                            {
                                lnkadrassign.Visible = false;
                            }
                            else
                            {
                                lnkadrassign.Visible = true;
                                lnkadrassign.Text = lnkadrassign.Text + " (" + cnt.ToString() + ")";
                                tot = tot + cnt;
                            }
                            break;


                        default:
                            break;
                    }







                    break;

                case "SR":
                    cnt = get_sr_pending();
                    if (cnt == 0)
                    {
                        lnksr.Visible = false;
                    }
                    else
                    {
                        lnksr.Visible = true;
                        lnksr.Text = lnksr.Text + " (" + cnt.ToString() + ")";
                        tot = tot + cnt;
                    }

                    

                    break;

                    
                case "MPR":
                    cnt = get_mpr_pending();
                    if (cnt == 0)
                    {
                        lnkmpr.Visible = false;
                    }
                    else
                    {
                        lnkmpr.Visible = true;
                        lnkmpr.Text = lnkmpr.Text + " (" + cnt.ToString() + ")";
                        tot = tot + cnt;
                    }

                    cnt = get_mpr_return_pending();
                    if (cnt == 0)
                    {
                        lnkmprreturn.Visible = false;
                    }
                    else
                    {
                        lnkmprreturn.Visible = true;
                        lnkmprreturn.Text = lnkmprreturn.Text + " (" + cnt.ToString() + ")";
                        tot = tot + cnt;
                    }

                        break;
                case "CS":

                        cnt = get_cs_pending();
                        if (cnt == 0)
                        {
                            lnkcs.Visible = false;
                        }
                        else
                        {
                            lnkcs.Visible = true;
                            lnkcs.Text = "CS APPROVAL PENDING (" + cnt.ToString() + ")";
                            tot = tot + cnt;
                        }

                    break;
                case "SPOAPP":
                    cnt = get_spo_pending();
                    if (cnt == 0)
                    {
                        lnkspo.Visible = false;
                    }
                    else
                    {
                        lnkspo.Visible = true;
                        lnkspo.Text = lnkspo.Text + " (" + cnt.ToString() + ")";
                        tot = tot + cnt;
                    }
                    break;

                case "ADAPP":
                    cnt = get_adv_pending();
                    if (cnt == 0)
                    {
                        lnkreal.Visible = false;
                    }
                    else
                    {
                        lnkreal.Visible = true;
                        lnkreal.Text = lnkreal.Text + " (" + cnt.ToString() + ")";
                        tot = tot + cnt;
                    }
                    break;
                case "FPOAPP":
                    //get_fpo_pending();
                    break;
                case "REVISE":
                    cnt = get_po_rev_pending();
                    if (cnt == 0)
                    {
                        lnkporevise.Visible = false;
                    }
                    else
                    {
                        lnkporevise.Visible = true;
                        lnkporevise.Text = lnkporevise.Text + " (" + cnt.ToString() + ")";
                        tot = tot + cnt;
                    }
                    break;
                case "CANCEL":
                    cnt = get_po_cancel_pending();
                    if (cnt == 0)
                    {
                        lnkpocancel.Visible = false;
                    }
                    else
                    {
                        lnkpocancel.Visible = true;
                        lnkpocancel.Text = lnkpocancel.Text + " (" + cnt.ToString() + ")";
                        tot = tot + cnt;
                    }

                    break;
                case "CLOSE":

                    cnt = get_po_close_pending();
                    if (cnt == 0)
                    {
                        lnkpoclose.Visible = false;
                    }
                    else
                    {
                        lnkpoclose.Visible = true;
                        lnkpoclose.Text = lnkpoclose.Text + " (" + cnt.ToString() + ")";
                        tot = tot + cnt;
                    }
                    break;

               

                case "PIAPP":

                    cnt = get_pi_app_pending();
                    if (cnt == 0)
                    {
                        lnkpiapproval.Visible = false;
                    }
                    else
                    {
                        lnkpiapproval.Visible = true;
                        lnkpiapproval.Text = lnkpiapproval.Text + " (" + cnt.ToString() + ")";
                        tot = tot + cnt;
                    }


                    break;

                case "PAYREQ":

                    cnt = get_pay_req_app_pending();
                    if (cnt == 0)
                    {
                        lnkpayreq.Visible = false;
                    }
                    else
                    {
                        lnkpayreq.Visible = true;
                        lnkpayreq.Text = lnkpayreq.Text + " (" + cnt.ToString() + ")";
                        tot = tot + cnt;
                    }                    
                    break;
                

                case "PROD":

                    switch (dr.role_as)
                    {
                        case "prod1":
                            // prod app bulk
                            cnt = get_prod_pending(dr.role_as);
                            if (cnt == 0)
                            {
                                lnkProdappbulk.Visible = false;
                            }
                            else
                            {
                                lnkProdappbulk.Visible = true;
                                lnkProdappbulk.Text = lnkProdappbulk.Text + " (" + cnt.ToString() + ")";
                                tot = tot + cnt;
                            }

                            break;

                        case "prod2":
                            // prod app bag
                            cnt = get_prod_pending(dr.role_as);
                            if (cnt == 0)
                            {
                                lnkProdappbag.Visible = false;
                            }
                            else
                            {
                                lnkProdappbag.Visible = true;
                                lnkProdappbag.Text = lnkProdappbag.Text + " (" + cnt.ToString() + ")";
                                tot = tot + cnt;
                            }

                            break;
                        case "prod3":
                            // prod post bulk
                            cnt = get_prod_pending(dr.role_as);
                            if (cnt == 0)
                            {
                                lnkProdpostbulk.Visible = false;
                            }
                            else
                            {
                                lnkProdpostbulk.Visible = true;
                                lnkProdpostbulk.Text = lnkProdpostbulk.Text + " (" + cnt.ToString() + ")";
                                tot = tot + cnt;
                            }

                            break;

                        case "prod4":
                            // prod app bag
                            cnt = get_prod_pending(dr.role_as);
                            if (cnt == 0)
                            {
                                lnkProdpostbag.Visible = false;
                            }
                            else
                            {
                                lnkProdpostbag.Visible = true;
                                lnkProdpostbag.Text = lnkProdpostbag.Text + " (" + cnt.ToString() + ")";
                                tot = tot + cnt;
                            }

                            break;

                        default:

                            break;
                    }

                    break;


                default:
                    break;
            }
            
        }

        //if (tot == 0) lbltotal.Text = "YOU HAVE NO PENDING";
        //else lbltotal.Text = "TOTAL PENDING (" + tot.ToString() + ")";
        
    }

    private int get_prod_pending(string role_as)
    {

        LibraryDAL.ProdDataSetTableAdapters.tbl_prod_ctl_detTableAdapter ctl = new LibraryDAL.ProdDataSetTableAdapters.tbl_prod_ctl_detTableAdapter();
        int cnt = 0;
        switch (role_as)
        {
            case "prod1":
                cnt = ctl.GetDataForPending("I", "B").Rows.Count;
                break;

            case "prod2":
                cnt = ctl.GetDataForPending("I", "P").Rows.Count;;
                break;

            case "prod3":
                cnt = ctl.GetDataForPending("F", "B").Rows.Count;
                break;

            case "prod4":
                cnt = ctl.GetDataForPending("F", "P").Rows.Count;
                break;

            default:
                break;
        }

        return cnt;
    }

    private string get_my_app_byrole(string roletype)
    {
        User_Role_DefinitionTableAdapter urole = new User_Role_DefinitionTableAdapter();
        SCBLDataSet.User_Role_DefinitionDataTable udt = new SCBLDataSet.User_Role_DefinitionDataTable();
        string my_app = "";
        udt = urole.GetRoleByUser(current.UserId.ToString(), roletype);

        if (udt.Rows.Count > 0) my_app = udt[0].role_as.ToString();
        return my_app;

    }

    private int get_adv_pending()
    {
        PuTr_PO_Hdr_ScblTableAdapter pohdr = new PuTr_PO_Hdr_ScblTableAdapter();
        SCBLDataSet.PuTr_PO_Hdr_ScblDataTable podt = new SCBLDataSet.PuTr_PO_Hdr_ScblDataTable();
        string my_app = get_my_app_byrole("ADAPP");
        if (my_app == "") return 0;

        return pohdr.GetPendingForPoApp("ADRUN", my_app).Rows.Count;

              
    }

   
    private int get_spo_pending()
    {
        PuTr_PO_Hdr_ScblTableAdapter hdr = new PuTr_PO_Hdr_ScblTableAdapter();       
        string my_app = get_my_app_byrole("SPOAPP");       

        if (my_app == "") return 0;
        return hdr.GetPendingForPoApp("RUN", my_app).Rows.Count;
        
                     
    }
    private void get_fpo_pending()
    {
        PuTr_PO_Hdr_ScblTableAdapter hdr = new PuTr_PO_Hdr_ScblTableAdapter();
        SCBLDataSet.PuTr_PO_Hdr_ScblDataTable dthdr = new SCBLDataSet.PuTr_PO_Hdr_ScblDataTable();
        string my_app = get_my_app_byrole("FPOAPP");
       

        if (my_app == "") return;
        dthdr = hdr.GetPendingForPoApp("RUN", my_app);
        

        if (dthdr.Rows.Count > 0)
        {
            Response.Redirect("./frm_fpo_approval.aspx");
        }       
    }
    private int get_po_cancel_pending()
    {
        PuTr_PO_Hdr_ScblTableAdapter hdr = new PuTr_PO_Hdr_ScblTableAdapter();        
        string my_app = get_my_app_byrole("CANCEL");
      

        if (my_app == "") return 0;
        return hdr.GetPendingForPoApp("CANCELING", my_app).Rows.Count;

        
        
    }

    private int get_po_close_pending()
    {
        PuTr_PO_Hdr_ScblTableAdapter hdr = new PuTr_PO_Hdr_ScblTableAdapter();
        string my_app = get_my_app_byrole("CLOSE");       

        if (my_app == "") return 0;
        return hdr.GetPendingForPoApp("CLOSING", my_app).Rows.Count;

    }

   
    private int get_pi_app_pending()
    {
        tbl_fpi_approval_dataTableAdapter app = new tbl_fpi_approval_dataTableAdapter();

        string my_app = get_my_app_byrole("PIAPP");
        if (my_app == "") return 0;
        return app.GetDataForPending("INI", my_app).Rows.Count;

    }

    private int get_pay_req_app_pending()
    {
        tbl_payment_request_detTableAdapter paydata = new tbl_payment_request_detTableAdapter();
        string my_app = get_my_app_byrole("PAYREQ");

        if (my_app == "") return 0;
        return paydata.GetDataForPending("RUN", my_app).Rows.Count;

    }
    

    private int get_po_rev_pending()
    {
        PuTr_PO_Hdr_ScblTableAdapter hdr = new PuTr_PO_Hdr_ScblTableAdapter();        
        string my_app = get_my_app_byrole("REVISE");       

        if (my_app == "") return 0;
        return hdr.GetPendingForPoApp("REVISING", my_app).Rows.Count;

        
    }


    private int get_cs_pending()
    {
        PuTr_IN_Det_ScblTableAdapter sr_det = new PuTr_IN_Det_ScblTableAdapter();
       

        string my_app = get_my_app_byrole("CS");      
       
        
        if (my_app == "") return 0;

        return sr_det.GetPendingForQuoApp("QUO", my_app).Rows.Count;
        
             

    }

    private int get_mpr_return_pending()
    {
        PuTr_IN_Det_Scbl2TableAdapter indet = new PuTr_IN_Det_Scbl2TableAdapter();
        
        string my_app = get_my_app_byrole("MPR");
       
        if (my_app == "") return 0;

        return indet.GetDataByStatus("RET", my_app).Rows.Count;
         

    }

    private int get_sr_pending()
    {
        InTr_Sr_HdrTableAdapter srhdr = new InTr_Sr_HdrTableAdapter();

        string my_app = get_my_app_byrole("SR");

        if (my_app == "") return 0;

        return srhdr.GetDataForPending("RUN", my_app).Rows.Count;

    }

    private int get_mpr_pending()
    {
        PuTr_IN_Hdr_ScblTableAdapter srhdr = new PuTr_IN_Hdr_ScblTableAdapter();
       
        string my_app = get_my_app_byrole("MPR");
       
        if (my_app == "") return 0;

        return srhdr.GetPending("RUN", my_app).Rows.Count;
       
    }

  



    protected void lnkchequeprint_Click(object sender, EventArgs e)
    {
        //string str = "frm_cheque_print.aspx?enc1=" + DateTime.Now.Day.ToString() + "&enc=" + DateTime.Now.Month.ToString() + "&enc3=" + DateTime.Now.Minute.ToString() + "&enc4=" + current.UserId + "&enc2=" + DateTime.Now.Hour.ToString() + "&enc5=" + current.UserName;
        string str = "http://192.168.7.16/cheque/ClientSide/modules/commercial/frm_cheque_print.aspx?enc1=" + DateTime.Now.Day.ToString() + "&enc=" + DateTime.Now.Month.ToString() + "&enc3=" + DateTime.Now.Minute.ToString() + "&enc4=" + current.UserId + "&enc2=" + DateTime.Now.Hour.ToString() + "&enc5=" + current.UserName;
        Response.Redirect(str);
    }
    
}
