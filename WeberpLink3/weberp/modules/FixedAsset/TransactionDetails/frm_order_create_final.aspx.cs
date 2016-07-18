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
using LibraryDAL.SCBL3DataSetTableAdapters;
using LibraryDAL.ErpDataSetTableAdapters;
using AjaxControlToolkit;

public partial class frm_order_create_final : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.MsgConfirmBox(btncreate, "Are you sure to make purchase order ?");
        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";
        
        if (!Page.IsPostBack)
        {
                      
        }
        generate_data();  
    }


    private void generate_data()
    {
        rdovaliddays.Items.Clear();
        ListItem itm;
        string ddlval = Session[clsStatic.sessionPartySelForPO].ToString();
       
        clsSpo[] seldet = (clsSpo[]) Session[clsStatic.sessionItemSelForPO];

        if (seldet == null) return;

        decimal qty, rate, tot, gtot;
        string tac_ref;
        string[] tmp = ddlval.Split(':');
        string cash_type = tmp[0].ToString();
        string pur_type = tmp[1].ToString();
        string app_party = tmp[2].ToString();
        int cnt,icnt,i,arraylen,vdays;

        Label lblseperator;

        RadioButtonList[] rdogen;
        rdogen = new RadioButtonList[20];

        RadioButtonList[] rdospe;
        rdospe = new RadioButtonList[20];

        RadioButtonList[] rdopay;
        rdopay = new RadioButtonList[20];

        for (i = 0; i < 20; i++)
        {
            rdogen[i] = new RadioButtonList();
            rdospe[i] = new RadioButtonList();
            rdopay[i] = new RadioButtonList();
        }


        ListItem lst;
        
        txtpurchasetype.Text = pur_type;
        txtreqtype.Text =cash_type;
        
        tbl_tac_logTableAdapter log = new tbl_tac_logTableAdapter();
        SCBLDataSet.tbl_tac_logDataTable dtlog = new SCBLDataSet.tbl_tac_logDataTable();
        PuTr_IN_Det_Scbl2TableAdapter dtdet = new PuTr_IN_Det_Scbl2TableAdapter();

        PuMa_Par_AdrTableAdapter adr = new PuMa_Par_AdrTableAdapter();
        quotation_detTableAdapter qdet = new quotation_detTableAdapter();
        SCBLDataSet.quotation_detRow qr;
        SCBL2DataSet.PuTr_IN_Det_Scbl2Row dr;
        DataTable dtgrd = new DataTable();

        dtgrd.Columns.Clear();
        dtgrd.Columns.Add("Sl", typeof(string));
        dtgrd.Columns.Add("Party", typeof(string));
        dtgrd.Columns.Add("Mpr", typeof(string));
        dtgrd.Columns.Add("Item", typeof(string));        
        dtgrd.Columns.Add("Mpr Qty", typeof(string));
        dtgrd.Columns.Add("Avl Qty", typeof(string));
        dtgrd.Columns.Add("Po Qty", typeof(string));
        dtgrd.Columns.Add("Rate", typeof(string));
        dtgrd.Columns.Add("Amount", typeof(string));

       

        arraylen = seldet.Length;
       
       
        gtot = 0;
        cnt = 0;
        for(i = 0 ; i < arraylen ; i++)
        {
            if (seldet[i] == null) goto skip_item;
            if (seldet[i].RefNo == null) goto skip_item;

            vdays = 0;
            dr = dtdet.GetDataByRefItem(seldet[i].RefNo, seldet[i].Icode)[0];
            if (dr.In_Det_Status != "APP") goto skip_item;
            if (Convert.ToDecimal(dr.IN_Det_Bal_Qty) < seldet[i].Qnty) goto skip_item;
            cnt++;                        
            qty = seldet[i].Qnty;
            rate = Convert.ToDecimal(dr.IN_Det_Lin_Rat);
            tot = qty * rate;                        
            gtot = gtot + tot;
            txtparty.Text = app_party + ":" + adr.GetDataByAdrCode(app_party)[0].par_adr_name.ToString();           
            
            qr = qdet.GetDataByRefParty(dr.In_Det_Quo_Ref, dr.In_Det_App_Party)[0];

            dtgrd.Rows.Add(cnt.ToString(), app_party + ":" + adr.GetDataByAdrCode(app_party)[0].par_adr_name.ToString(), dr.IN_Det_Ref.ToString(), dr.IN_Det_Icode.ToString() + ": " + dr.IN_Det_Itm_Desc.ToString(), dr.IN_Det_Lin_Qty.ToString("N2") + " " + dr.IN_Det_Itm_Uom.ToString(), dr.IN_Det_Bal_Qty.ToString("N2") + " " + dr.IN_Det_Itm_Uom.ToString(), qty.ToString("N2") + " " + dr.IN_Det_Itm_Uom.ToString(), rate.ToString("N2"), tot.ToString("N2"));

            tac_ref = qr.gen_terms;

            dtlog = log.GetDataByRef(tac_ref);

            foreach (SCBLDataSet.tbl_tac_logRow drlog in dtlog.Rows)
            {
                vdays = drlog.valid_days;
                switch (drlog.tac_type)
                {
                    case "gen":
                        {
                            lst = new ListItem();
                            lst.Value = drlog.tem_seq_no.ToString() + ":" + drlog.log_id.ToString();
                            lst.Text = drlog.tem_seq_no.ToString() + "." + cnt.ToString() + ". " + drlog.content_det.ToString() + "</br>";
                            rdogen[drlog.tem_seq_no].Items.Add(lst);                            
                            break;
                        }

                    case "spe":
                        {
                            lst = new ListItem();
                            lst.Value = drlog.tem_seq_no.ToString() + ":" + drlog.log_id.ToString();
                            lst.Text = drlog.tem_seq_no.ToString() + "." + cnt.ToString() + ". " + drlog.content_det.ToString() + "</br>";
                            rdospe[drlog.tem_seq_no].Items.Add(lst);
                            break;
                        }

                    case "pay":
                        {
                            lst = new ListItem();
                            lst.Value = drlog.tem_seq_no.ToString() + ":" + drlog.log_id.ToString();
                            lst.Text = drlog.tem_seq_no.ToString() + "." + cnt.ToString() + ". " + drlog.content_det.ToString() + "</br>";
                            rdopay[drlog.tem_seq_no].Items.Add(lst); 
                            break;
                        }

                }
            }


            //pay_type = "";

            //if (log.GetDataByPayType(tac_ref, "pay", "full").Rows.Count > 0)
            //    pay_type = "full";
            //else if (log.GetDataByPayType(tac_ref, "pay", "part").Rows.Count > 0)
            //    pay_type = "part";
            //else
            //    pay_type = "no";
                                    
           
            //add valid days items

            itm = new ListItem();
            itm.Value = vdays.ToString();
            itm.Text = vdays.ToString() + " Days";
            rdovaliddays.Items.Add(itm);

        skip_item: ;

        }

        icnt = rdogen.Length;       
        for (i = 0; i < icnt; i++)
        {
            if(rdogen[i]!=null)
                if (rdogen[i].Items.Count > 0)
                {
                    rdogen[i].SelectedIndex = 0;
                    rdogen[i].ID = "gen_"+i.ToString();
                    celgen.Controls.Add(rdogen[i]);
                    lblseperator = new Label();
                    lblseperator.Text = "----------------------------------------------------------------------------";
                    celgen.Controls.Add(lblseperator);
                }
        }

        icnt = rdospe.Length;
        for (i = 0; i < icnt; i++)
        {
            if (rdospe[i] != null)
                if (rdospe[i].Items.Count > 0)
                {
                    rdospe[i].SelectedIndex = 0;
                    rdospe[i].ID = "spe_" + i.ToString();
                    celspe.Controls.Add(rdospe[i]);
                    lblseperator = new Label();
                    lblseperator.Text = "----------------------------------------------------------------------------";
                    celspe.Controls.Add(lblseperator);
                }
        }

        icnt = rdopay.Length;
        for (i = 0; i < icnt; i++)
        {
            if (rdopay[i] != null)
                if (rdopay[i].Items.Count > 0)
                {
                    rdopay[i].SelectedIndex = 0;
                    rdopay[i].ID = "pay_" + i.ToString();
                    celpay.Controls.Add(rdopay[i]);
                    lblseperator = new Label();
                    lblseperator.Text = "----------------------------------------------------------------------------";
                    celpay.Controls.Add(lblseperator);
                }
        }

        if (rdovaliddays.Items.Count>0) rdovaliddays.SelectedIndex = 0;

        if (gtot == 0)
            btncreate.Visible = false;
        else
            btncreate.Visible = true;

        txtamount.Text = gtot.ToString("N2");
        lblinword.Text = NumerictowordClass.FNumber(gtot.ToString("N2"));

        gdItem.DataSource = dtgrd;
        gdItem.DataBind();
    }




    protected void btnok_Click(object sender, EventArgs e)
    {
        Response.Redirect("./frm_order_create.aspx");
    }



   
    protected void btncreate_Click(object sender, EventArgs e)
    {
        clsSpo[] seldet = (clsSpo[])Session[clsStatic.sessionItemSelForPO];                
        
        InSu_Trn_SetTableAdapter scfset = new InSu_Trn_SetTableAdapter();
        PuMa_Par_AdrTableAdapter adr = new PuMa_Par_AdrTableAdapter();

        PuTr_IN_Det_ScblTableAdapter det = new PuTr_IN_Det_ScblTableAdapter();
        PuTr_IN_Det_Scbl2TableAdapter det2 = new PuTr_IN_Det_Scbl2TableAdapter();
        PuTr_PO_Det_ScblTableAdapter podet = new PuTr_PO_Det_ScblTableAdapter();
        PuTr_PO_Hdr_ScblTableAdapter pohdr = new PuTr_PO_Hdr_ScblTableAdapter();
        tbl_tac_detTableAdapter tac = new tbl_tac_detTableAdapter();
        tbl_tac_logTableAdapter log = new tbl_tac_logTableAdapter();
        tbl_chq_voucherTableAdapter chqv = new tbl_chq_voucherTableAdapter();

        quotation_detTableAdapter qdet = new quotation_detTableAdapter();
        SCBLDataSet.quotation_detRow qr;

        SCBLDataSet.PuTr_IN_Det_ScblDataTable dtdet = new SCBLDataSet.PuTr_IN_Det_ScblDataTable();
        SCBL2DataSet.PuTr_IN_Det_Scbl2Row[] dr = new SCBL2DataSet.PuTr_IN_Det_Scbl2Row[seldet.Length];
        SCBLDataSet.tbl_tac_logRow drlog;




        bool flg = true;
        int lno, i, tcnt, tem_seq, entry_seq, arraylen;
        string[] tmp;

        string period = DateTime.Now.Year.ToString() + "/" + string.Format("{0:00}", DateTime.Now.Month);
        decimal itmp, atot, dum, rate, orgqty, balqty;
        tmp = txtparty.Text.Split(':');

        int daycnt = Convert.ToInt32(rdovaliddays.SelectedValue.ToString());
        string cash_type = txtreqtype.Text;
        string pur_type = txtpurchasetype.Text;
        string app_party = tmp[0].ToString();
        string app_party_name = tmp[1].ToString();
        string app_party_acc_code = adr.GetDataByAdrCode(app_party)[0].Par_Adr_Acc_Code.ToString();

        if (app_party_acc_code == "") return;

        string cid, selval, selqref, newstatus, remarks;
        string pendingfor;
        string template_id;
        string ocflg, prefx;
        string pay_term_flg = "";
        RadioButtonList rdogen;
        RadioButtonList rdospe;
        RadioButtonList rdopay;

        //if (txtreqtype.Text.Substring(0,2)=="HO") prefx = "LPOH"; else prefx = "LPOF";

        //prefx = "LPO" + txtreqtype.Text.Substring(0, 2);
        prefx = txtpurchasetype.Text.Trim().ToString() + txtreqtype.Text.Substring(0, 2);

        string last_num = scfset.GetDataByType_Code("PO", prefx)[0].Trn_Set_Ord_Next_No.ToString();
        string scf_ref = prefx + string.Format("{0:00}", Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2, 2))) + string.Format("{0:00}", DateTime.Now.Month) + "-" + last_num;

        string next_num = string.Format("{0:000000}", Convert.ToInt32(last_num) + 1);

        arraylen = seldet.Length;

        for (i = 0; i < arraylen; i++)
        {
            if (seldet[i] != null)
                if (seldet[i].RefNo != null)
                {                    
                    dr[i] = det2.GetDataByRefItem(seldet[i].RefNo, seldet[i].Icode)[0];
                }
        }
        
        
       

        atot = 0;
        SqlTransaction myTrn = HelperTA.OpenTransaction(det.Connection);
       
        try
        {
            det.AttachTransaction(myTrn);
            podet.AttachTransaction(myTrn);
            pohdr.AttachTransaction(myTrn);
            log.AttachTransaction(myTrn);

            scfset.AttachTransaction(myTrn);

            lno = 0;
           
            for (i = 0; i < arraylen; i++)
            {
                if (seldet[i] == null) goto skip_item;
                if (seldet[i].RefNo == null) goto skip_item;
                
                if (dr[i].In_Det_Status != "APP") goto skip_item;
                if (Convert.ToDecimal(dr[i].IN_Det_Bal_Qty) < seldet[i].Qnty) goto skip_item;

                lno = lno + 1;

                rate = Convert.ToDecimal(dr[i].IN_Det_Lin_Rat);
                itmp = seldet[i].Qnty;

                dum = rate * itmp;

                if (dum == 0) { flg = false; goto Error_Hndlr; }
                atot = atot + dum;

                if (Convert.ToDecimal(dr[i].IN_Det_Bal_Qty) == seldet[i].Qnty)
                    newstatus = "POC";
                else
                    newstatus = "APP";


                if (dr[i].T_C1 == "")
                    remarks = scf_ref + "," + seldet[i].Qnty.ToString();
                else
                    remarks = dr[i].T_C1 + ":" + scf_ref + "," + seldet[i].Qnty.ToString();

                orgqty = Convert.ToDecimal(dr[i].IN_Det_Org_QTY) + seldet[i].Qnty;
                balqty = Convert.ToDecimal(dr[i].IN_Det_Bal_Qty) - seldet[i].Qnty;

                //update scf qnty
                if (balqty == 0) ocflg = "C"; else ocflg = "O";               

                det.UpdateForPoCreate(newstatus, (double)orgqty, (double)balqty, remarks, seldet[i].RefNo, dr[i].IN_Det_Icode);
                qr = qdet.GetDataByRefParty(dr[i].In_Det_Quo_Ref, dr[i].In_Det_App_Party)[0];

                podet.InsertDetData("PO", cash_type, scf_ref, dr[i].In_Det_Quo_Ref, (short)lno, "", 0, dr[i].IN_Det_Icode, dr[i].IN_Det_Itm_Desc, dr[i].IN_Det_Itm_Uom,"", qr.specification, qr.product_brand, qr.origin, qr.packing, dr[i].IN_Det_Str_Code, dr[i].IN_Det_Bin_Code, dr[i].IN_Det_Ref, dr[i].IN_Det_Lno, "", DateTime.Now.Date, DateTime.Now, (double)itmp, 0, 0, (double)itmp, 0, "O", "N", rate, dum, dum, "", "", "", 0);

              
            skip_item: ;
            }

            if (atot == 0) { flg = false; goto Error_Hndlr; }

            template_id = "";
            pendingfor = "";

            pohdr.InsertHdrData("PO", cash_type, scf_ref, "APP", pendingfor, template_id, app_party_acc_code, app_party, app_party_acc_code, DateTime.Now.Date, app_party_name, "", "", "", "", "", "", "", "", "", atot, "P", period, current.UserId.ToString(), DateTime.Now, "", "", "", DateTime.Now, period, "", "", "L", 0, "", 0);

            scfset.UpdateNextNum_PO(next_num, "PO", prefx);

            // insert terms and conditions
            //gen           

            #region

            tcnt = celgen.Controls.Count;
            entry_seq = 0;
            for (i = 0; i < 20; i++)
            {
                cid = "gen_" + i.ToString();
                rdogen = new RadioButtonList();
                rdogen = (RadioButtonList)celgen.FindControl(cid);
               

                if (rdogen != null)
                    if (rdogen.Items.Count > 0)
                    {
                        entry_seq++;
                        selval = rdogen.SelectedValue.ToString();
                        tmp = selval.Split(':');
                        tem_seq = Convert.ToInt32(tmp[0].ToString());
                        selqref = tmp[1].ToString();
                        drlog = log.GetDataByIdTypeSeq(selqref, "gen", tem_seq)[0];
                        log.Inserttac(scf_ref, "PO", drlog.tac_type, drlog.pay_type, tem_seq, entry_seq, drlog.content_det, daycnt, drlog.remarks);
                        
                    }
            }

            //spe
            tcnt = celspe.Controls.Count;
            entry_seq = 0;
            for (i = 0; i < 20; i++)
            {
                cid = "spe_" + i.ToString();
                rdospe = new RadioButtonList();
                rdospe = (RadioButtonList)celspe.FindControl(cid);

                if (rdospe != null)
                    if (rdospe.Items.Count > 0)
                    {
                        entry_seq++;
                        selval = rdospe.SelectedValue.ToString();
                        tmp = selval.Split(':');
                        tem_seq = Convert.ToInt32(tmp[0].ToString());
                        selqref = tmp[1].ToString();
                        drlog = log.GetDataByIdTypeSeq(selqref, "spe", tem_seq)[0];
                        log.Inserttac(scf_ref, "PO", drlog.tac_type, drlog.pay_type, tem_seq, entry_seq, drlog.content_det, daycnt, drlog.remarks);
                    }
            }
            //pay
            tcnt = celpay.Controls.Count;
            entry_seq = 0;
            for (i = 0; i < 20; i++)
            {
                cid = "pay_" + i.ToString();
                rdopay = new RadioButtonList();
                rdopay = (RadioButtonList)celpay.FindControl(cid);

                if (rdopay != null)
                    if (rdopay.Items.Count > 0)
                    {
                        entry_seq++;
                        selval = rdopay.SelectedValue.ToString();
                        tmp = selval.Split(':');
                        tem_seq = Convert.ToInt32(tmp[0].ToString());
                        selqref = tmp[1].ToString();
                        drlog = log.GetDataByIdTypeSeq(selqref, "pay", tem_seq)[0];
                        log.Inserttac(scf_ref, "PO", drlog.tac_type, drlog.pay_type, tem_seq, entry_seq, drlog.content_det, daycnt, drlog.remarks);
                        pay_term_flg = drlog.pay_type.ToUpper();
                    }
            }

              #endregion
            // insert chq voucher 

            if ((pay_term_flg == "FULL")||(pay_term_flg == "PART"))
            {
                chqv.AttachTransaction(myTrn);
                chqv.InsertChq(scf_ref, app_party_acc_code,app_party, app_party_name, pay_term_flg, "INI", atot, 0, "", "", DateTime.Now, "");

            }



          

        Error_Hndlr:

            if (flg)
            {
                myTrn.Commit();               
               // myTrn.Rollback();    
            }
            else
            {
                myTrn.Rollback();               
            }
        }
        catch
        {
            flg = false;
            myTrn.Rollback();           
        }
        finally
        {
            HelperTA.CloseTransaction(det.Connection, myTrn);           
        }

        if (flg)
        {
            Session[clsStatic.sessionItemSelForPO] = null;
            lblporef.Text = scf_ref;
            ModalPopupExtender5.Show();
        }
        else
        {
            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }




    protected void btnprint_Click(object sender, EventArgs e)
    {
        Response.Redirect("./frm_po_send.aspx?po_ref_no="+ lblporef.Text);
    }
}
