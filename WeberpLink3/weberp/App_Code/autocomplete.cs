using System;
using System.Collections.Generic;
using System.Web.Services;
using LibraryDAL;
using LibraryDAL.SCBLDataSetTableAdapters;
using LibraryDAL.SCBL3DataSetTableAdapters;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using LibraryDAL.ErpDataSetTableAdapters;
using LibraryDAL.AccDataSetTableAdapters;
using LibraryDAL.SCBLINTableAdapters;
using LibraryDAL.AccDataSetTableAdapters;
using LibraryDAL.dsLinkofficeTableAdapters;
using System.Data;
using System.Linq;
using System.Web;
using System.Collections;
using Link3FrameWork;
using System.Configuration;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class autocomplete : System.Web.Services.WebService
{
    public autocomplete()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
   

    [WebMethod]
    public string[] GetItemList(string prefixText, int count)
    {
        InMa_Itm_DetTableAdapter itm = new InMa_Itm_DetTableAdapter();
        ErpDataSet.InMa_Itm_DetDataTable dt = new ErpDataSet.InMa_Itm_DetDataTable();

        int maxsize = 1000;

        if (prefixText == "*")
        {
            dt = itm.GetAllItems();
        }
        else
        {
            prefixText = "%" + prefixText + "%";
            dt = itm.SearchItem(prefixText);
        }


        string[] str;

        if (dt.Rows.Count > maxsize)
            str = new string[maxsize];
        else
            str = new string[dt.Rows.Count];
        
        int indx = 0;

        
        foreach (ErpDataSet.InMa_Itm_DetRow dr in dt.Rows)
        {
            str[indx] = dr.Itm_Det_Icode.ToString() + ":" + dr.Itm_Det_desc.ToString() + ":" + dr.Itm_Det_stk_unit.ToString();
            indx++;

            if (indx == maxsize) break;
        }

        return str;
    }

    [WebMethod]
    public string[] GetItemLoc(string prefixText, int count)
    {
        InMa_Itm_LocTableAdapter loc = new InMa_Itm_LocTableAdapter();
        SCBLIN.InMa_Itm_LocDataTable dtloc = new SCBLIN.InMa_Itm_LocDataTable();

        int maxsize = 1000;

        string[] tmp = prefixText.Split('.');

        switch (tmp.Length)
        {
            case 1:
                dtloc = loc.GetDataByType("B");
                break;

            case 2:
                dtloc = loc.GetDataByType("F");                
                break;
            case 3:
                dtloc = loc.GetDataByType("R");
                
                break;
            case 4:
                dtloc = loc.GetDataByType("L");
                
                break;

            default:

                break;

        }


        string[] str;

        if (dtloc.Rows.Count > maxsize)
            str = new string[maxsize];
        else
            str = new string[dtloc.Rows.Count];
        
        int indx = 0;


        foreach (SCBLIN.InMa_Itm_LocRow dr in dtloc.Rows)
        {
            str[indx] = dr.Itm_Loc_Id.ToString() + ":" + dr.Itm_Loc_Name.ToString();
            indx++;

            if (indx == maxsize) break;
        }

        return str;
    }

    


    [WebMethod]
    public string[] GetCoa(string prefixText, int count)
    {
        budgTableAdapter itm = new budgTableAdapter();
        AccDataSet.budgDataTable dt = new AccDataSet.budgDataTable();

        int maxsize = 1000;

        prefixText = "%" + prefixText + "%";
        dt = itm.SearchCoa(prefixText);

        string[] str;

        if (dt.Rows.Count > maxsize)
            str = new string[maxsize];
        else
            str = new string[dt.Rows.Count];

        int indx = 0;


        foreach (AccDataSet.budgRow dr in dt.Rows)
        {
            str[indx] = dr.Gl_Coa_Code.ToString() + ":" + dr.Gl_Coa_Name.ToString() + ":" + dr.Gl_Coa_Type.ToString();
            indx++;

            if (indx == maxsize) break;
        }

        return str;
    }

    [WebMethod]
    public string[] GetCoaForIssue(string prefixText, int count)
    {
        budgTableAdapter itm = new budgTableAdapter();
        AccDataSet.budgDataTable dt = new AccDataSet.budgDataTable();

        int maxsize = 1000;

        prefixText = "%" + prefixText + "%";
        dt = itm.SearchForIss(prefixText);

        string[] str;

        if (dt.Rows.Count > maxsize)
            str = new string[maxsize];
        else
            str = new string[dt.Rows.Count];

        int indx = 0;


        foreach (AccDataSet.budgRow dr in dt.Rows)
        {
            str[indx] = dr.Gl_Coa_Code.ToString() + ":" + dr.Gl_Coa_Name.ToString() + ":" + dr.Gl_Coa_Type.ToString();
            indx++;

            if (indx == maxsize) break;
        }

        return str;
    }

    [WebMethod]
    public string[] GetItemListforrec(string prefixText, int count)
    {
        InMa_Itm_DetTableAdapter itm = new InMa_Itm_DetTableAdapter();
        ErpDataSet.InMa_Itm_DetDataTable dt = new ErpDataSet.InMa_Itm_DetDataTable();

        string itype = "A,C,D,E,F,H,I,K,L,M,R,T,V,W";
        int maxsize = 1000;

        if (prefixText == "*")
        {
            dt = itm.GetAllForPr(itype);
        }
        else
        {
            prefixText = "%" + prefixText + "%";
            dt = itm.SearchForPr(itype, prefixText);
        }


        string[] str;

        if (dt.Rows.Count > maxsize)
            str = new string[maxsize];
        else
            str = new string[dt.Rows.Count];
        
        int indx = 0;

        
        foreach (ErpDataSet.InMa_Itm_DetRow dr in dt.Rows)
        {
            str[indx] = dr.Itm_Det_Icode.ToString() + ":" + dr.Itm_Det_desc.ToString() + ":" + dr.Itm_Det_stk_unit.ToString();
            indx++;

            if (indx == maxsize) break;
        }

        return str;
    }
    [WebMethod]
    public string[] GetItemList2(string prefixText, int count)
    {
        InMa_Itm_DetTableAdapter itm = new InMa_Itm_DetTableAdapter();
        ErpDataSet.InMa_Itm_DetDataTable dt = new ErpDataSet.InMa_Itm_DetDataTable();

        int maxsize = 1000;

        if (prefixText == "*")
        {
            dt = itm.GetData();
        }
        else
        {
            prefixText = "%" + prefixText + "%";
            dt = itm.SrcItemSort(prefixText);
        }


        string[] str;

        if (dt.Rows.Count > maxsize)
            str = new string[maxsize];
        else
            str = new string[dt.Rows.Count];

        int indx = 0;


        foreach (ErpDataSet.InMa_Itm_DetRow dr in dt.Rows)
        {
            str[indx] = dr.Itm_Det_Icode.ToString() + ":" + dr.Itm_Det_desc.ToString() + ":" + dr.Itm_Det_stk_unit.ToString();
            indx++;

            if (indx == maxsize) break;
        }

        return str;
    }

    [WebMethod]
    public string[] GetIStockDet(string prefixText, int count)
    {
        ErpDataSet.utbl_currentstoockDataTable dt = new ErpDataSet.utbl_currentstoockDataTable();
        utbl_currentstoockTableAdapter sto = new utbl_currentstoockTableAdapter();
        int maxsize = 50;

        dt = sto.GetSearchData(prefixText);



        string[] str;

        if (dt.Rows.Count > maxsize)
            str = new string[maxsize];
        else
            str = new string[dt.Rows.Count];

        int indx = 0;


        foreach (ErpDataSet.utbl_currentstoockRow dr in dt.Rows)
        {
            str[indx] = dr.Store + ":" + dr.Code + ":" + dr.Item + ": CurrentStock - " + dr.CurrentStock.ToString() + " " + dr.UOM;
            indx++;

            if (indx == maxsize) break;
        }

        return str;
    }


    //[WebMethod]
    //public string[] GetGrpList(string prefixText, int count)
    //{
    //    InMa_Grp_CodeTableAdapter itm = new InMa_Grp_CodeTableAdapter();
    //    ErpDataSet.InMa_Grp_CodeDataTable dt = new ErpDataSet.InMa_Grp_CodeDataTable();

    //    int maxsize = 1000;

    //    if (prefixText == "*")
    //    {
    //        dt = itm.GetData();
    //    }
    //    else
    //    {
    //        prefixText = "%" + prefixText + "%";
    //        dt = itm.SearchData(prefixText);
    //    }


    //    string[] str;

    //    if (dt.Rows.Count > maxsize)
    //        str = new string[maxsize];
    //    else
    //        str = new string[dt.Rows.Count];

    //    int indx = 0;


    //    foreach (ErpDataSet.InMa_Grp_CodeRow dr in dt.Rows)
    //    {
    //        str[indx] = dr.Grp_Code_Id.ToString() + ":" + dr.Grp_Code.ToString() + ":" + dr.Grp_Code_Name.ToString();
    //        indx++;

    //        if (indx == maxsize) break;
    //    }

    //    return str;
    //}

    


    [WebMethod]
    public string[] GetStoreList(string prefixText, int count)
    {
        InMa_Str_LocTableAdapter itm = new InMa_Str_LocTableAdapter();
        ErpDataSet.InMa_Str_LocDataTable dt = new ErpDataSet.InMa_Str_LocDataTable();

        int maxsize = 1000;

        if (prefixText == "*")
        {
            dt = itm.GetAllStore();
        }
        else
        {
            prefixText = "%" + prefixText + "%";
            dt = itm.SearchStore(prefixText);
        }

        string[] str;
        str = new string[dt.Rows.Count];
        int indx = 0;

        foreach (ErpDataSet.InMa_Str_LocRow dr in dt.Rows)
        {
            str[indx] = dr.Str_Loc_Id.ToString() + ":" + dr.Str_Loc_Name.ToString();
            indx++;
            if (indx == maxsize) break;
        }

        return str;
    }
    

    [WebMethod]
    public string[] GetPartyAccList(string prefixText, int count)
    {
        PuMa_Par_AccTableAdapter itm = new PuMa_Par_AccTableAdapter();
        ErpDataSet.PuMa_Par_AccDataTable dt = new ErpDataSet.PuMa_Par_AccDataTable();

        if (prefixText == "*")
        {
            dt = itm.GetAllData();
        }
        else
        {
            prefixText = "%" + prefixText + "%";
            dt = itm.SearchAcc(prefixText);
        }

        string[] str;
        str = new string[dt.Rows.Count];
        int indx = 0;

        foreach (ErpDataSet.PuMa_Par_AccRow dr in dt.Rows)
        {
            str[indx] = dr.Par_Acc_Code.ToString() + ":" + dr.Par_Acc_Name.ToString();
            indx++;
        }

        return str;
    }

    [WebMethod]
    public string[] GetPartyAdrListByParty(string prefixText, int count, string contextKey)
    {
        PuMa_Par_AdrTableAdapter itm = new PuMa_Par_AdrTableAdapter();
        ErpDataSet.PuMa_Par_AdrDataTable dt = new ErpDataSet.PuMa_Par_AdrDataTable();
        AccCoaAnalysisTableAdapter anal = new AccCoaAnalysisTableAdapter();
        AccCoaGroupCodeSetupTableAdapter grp = new LibraryDAL.AccDataSetTableAdapters.AccCoaGroupCodeSetupTableAdapter();
        AccDataSet.AccCoaGroupCodeSetupDataTable dtgrp = new AccDataSet.AccCoaGroupCodeSetupDataTable();

        string accgrp;

        string[] str;

        if (itm.GetAdrByAccCode(contextKey).Count ==0)
        {
            accgrp = anal.GetDataByCode(contextKey)[0].COST_ID;

            if (prefixText == "*")
            {
                dtgrp = grp.GetDataById(accgrp);
            }
            else
            {
                prefixText = "%" + prefixText + "%";
                dtgrp = grp.SearchMrr(accgrp, prefixText);
            }

            str = new string[dtgrp.Rows.Count];
            int indx = 0;

            foreach (AccDataSet.AccCoaGroupCodeSetupRow dr in dtgrp.Rows)
            {
                str[indx] = dr.Ccg_Code.ToString() + ":" + dr.Ccg_Name.ToString();
                indx++;
            }
        }
        else
        {

            if (prefixText == "*")
            {
                dt = itm.GetAdrByAccCode(contextKey);
            }
            else
            {
                prefixText = "%" + prefixText + "%";
                dt = itm.SearchByAcc(contextKey, prefixText);
            }

           
            str = new string[dt.Rows.Count];
            int indx = 0;

            foreach (ErpDataSet.PuMa_Par_AdrRow dr in dt.Rows)
            {
                str[indx] = dr.par_adr_code.ToString() + ":" + dr.par_adr_name.ToString();
                indx++;
            }
        }

        return str;
    }
    [WebMethod]
    public string[] GetPartyAdrList(string prefixText, int count)
    {
        PuMa_Par_AdrTableAdapter itm = new PuMa_Par_AdrTableAdapter();
        ErpDataSet.PuMa_Par_AdrDataTable dt = new ErpDataSet.PuMa_Par_AdrDataTable();

        if (prefixText == "*")
        {
            dt = itm.GetData();
        }
        else
        {
            prefixText = "%" + prefixText + "%";
            dt = itm.SearchAdr(prefixText);
        }

        string[] str;
        str = new string[dt.Rows.Count];
        int indx = 0;

        foreach (ErpDataSet.PuMa_Par_AdrRow dr in dt.Rows)
        {

            str[indx] = dr.par_adr_code.ToString() + ":" + dr.par_adr_name.ToString();
            indx++;

        }

        return str;
    }


    [WebMethod]
    public string[] GetEmployeeList(string prefixText, int count)
    {
        tblUserInfoTableAdapter user = new tblUserInfoTableAdapter();
        dsLinkoffice.tblUserInfoDataTable dtuser = new dsLinkoffice.tblUserInfoDataTable();

        if (prefixText == "*")
        {
            dtuser = user.GetAllEmployeeForAcc();
        }
        else
        {
            dtuser = user.SearchEmpForAcc(prefixText);
        }

        string[] str;
        str = new string[dtuser.Rows.Count];
        int indx = 0;

        foreach (dsLinkoffice.tblUserInfoRow dr in dtuser.Rows)
        {
            str[indx] = dr.UserId.ToString() + ":" + dr.UserName.ToString() + ":" + dr.UserDesignation.ToString();
            indx++;
        }

        return str;
    }

    [WebMethod]
    public ArrayList GetPartyForPoSend(String prefixText, int count)
    {
        ArrayList ItemList = new ArrayList();
        PuTr_PO_Hdr_ScblTableAdapter acc = new PuTr_PO_Hdr_ScblTableAdapter();
        SCBLDataSet.PuTr_PO_Hdr_ScblDataTable dt = new SCBLDataSet.PuTr_PO_Hdr_ScblDataTable();

        if (prefixText == "*")
        {
            //dt = acc.GetDataByStatusPtype("APP","L");
            dt = acc.GetDataByStatusPTypeAll("APP");
        }
        else
        {
            prefixText = "%" + prefixText + "%";
            //dt = acc.GetSearchData("APP", "L", prefixText);
            dt = acc.GetApprovedDataSearch("APP",prefixText);            
        }

        foreach (SCBLDataSet.PuTr_PO_Hdr_ScblRow dr in dt.Rows)
        {
            ItemList.Add(dr.PO_Hdr_Code + ":" + dr.PO_Hdr_Ref + ":" + dr.PO_Hdr_Com1);
        }


        return ItemList;

    }


    [WebMethod]
    public ArrayList GetPartyForPay(String prefixText, int count, string contextKey)
    {
        ArrayList ItemList = new ArrayList();
        PuTr_PO_Hdr_ScblTableAdapter acc = new PuTr_PO_Hdr_ScblTableAdapter();
        SCBLDataSet.PuTr_PO_Hdr_ScblDataTable dt = new SCBLDataSet.PuTr_PO_Hdr_ScblDataTable();

       
        bool addflg;

        string[] tmp = contextKey.Split(':');
        string[] filterdata = tmp[1].Split(',');
        prefixText = "%" + prefixText + "%";
        switch (tmp[0])
        {
            case "LPO":
            case "SPO":          
                {                                                         
                    dt = acc.Searchforpay("APP", tmp[0], prefixText);                  
                    foreach (SCBLDataSet.PuTr_PO_Hdr_ScblRow dr in dt.Rows)
                    {
                        addflg = false;
                        foreach (string pl in filterdata)
                        {
                            if (pl.Trim() == dr.PO_Hdr_Code.Substring(0, 2)) { addflg = true; goto addnow; }
                        }
                    addnow:
                        if (addflg)
                            ItemList.Add(dr.PO_Hdr_Ref + ":" + dr.PO_Hdr_Code + ":" + dr.PO_Hdr_Com1);
                    }
                }
                break;
                                  

        }
       


        return ItemList;

    }


    [WebMethod]
    public ArrayList GetPartyForPayView(String prefixText, int count, string contextKey)
    {
        ArrayList ItemList = new ArrayList();
        PuTr_PO_Hdr_ScblTableAdapter acc = new PuTr_PO_Hdr_ScblTableAdapter();
        SCBLDataSet.PuTr_PO_Hdr_ScblDataTable dt = new SCBLDataSet.PuTr_PO_Hdr_ScblDataTable();
                               
        
        prefixText = "%" + prefixText + "%";
        switch (contextKey)
        {
            case "LPO":
            case "SPO":           
                {                    
                    dt = acc.Searchforpay("APP", contextKey, prefixText);
                    
                    foreach (SCBLDataSet.PuTr_PO_Hdr_ScblRow dr in dt.Rows)
                    {
                        ItemList.Add(dr.PO_Hdr_Ref + ":" + dr.PO_Hdr_Code + ":" + dr.PO_Hdr_Com1);
                    }
                }
                break;

                


        }



        return ItemList;

    }


    [WebMethod]
    public ArrayList GetPartyForPoRevise(String prefixText, int count, string contextKey)
    {
        ArrayList ItemList = new ArrayList();
        PuTr_PO_Hdr_ScblTableAdapter acc = new PuTr_PO_Hdr_ScblTableAdapter();
        SCBLDataSet.PuTr_PO_Hdr_ScblDataTable dt = new SCBLDataSet.PuTr_PO_Hdr_ScblDataTable();

        bool addflg;

        string[] filterdata = contextKey.Split(',');

        if (prefixText == "*")
        {
            dt = acc.GetDataByStatus("APP");
        }
        else
        {
            prefixText = "%" + prefixText + "%";
            dt = acc.GetSearchRevData("APP", prefixText);
        }

        foreach (SCBLDataSet.PuTr_PO_Hdr_ScblRow dr in dt.Rows)
        {
            addflg = false;
            foreach (string pl in filterdata)
            {
                if (pl.Trim() == dr.PO_Hdr_Code.Substring(0, 2)) { addflg = true; goto addnow; }
            }
        addnow:
            if (addflg)
                ItemList.Add(dr.PO_Hdr_Ref + ":" + dr.PO_Hdr_Code + ":" + dr.PO_Hdr_Com1);
        }


        return ItemList;

    }


    [WebMethod]
    public ArrayList GetLC(String prefixText, int count)
    {
        ArrayList ItemList = new ArrayList();
        AccCoaGroupCodeSetupTableAdapter acc = new AccCoaGroupCodeSetupTableAdapter();
        AccDataSet.AccCoaGroupCodeSetupDataTable dt = new AccDataSet.AccCoaGroupCodeSetupDataTable();

        
        prefixText = "%" + prefixText + "%";
        dt = acc.SearchData(prefixText);


        foreach (AccDataSet.AccCoaGroupCodeSetupRow dr in dt.Rows)
        {
            ItemList.Add(dr.Ccg_Code + ":" + dr.Ccg_Name);
        }


        return ItemList;

    }


    [WebMethod]
    public ArrayList GetLoanNo(String prefixText, int count)
    {
        ArrayList ItemList = new ArrayList();
        AccCoaGroupCodeSetupTableAdapter acc = new AccCoaGroupCodeSetupTableAdapter();
        AccDataSet.AccCoaGroupCodeSetupDataTable dt = new AccDataSet.AccCoaGroupCodeSetupDataTable();


        prefixText = "%" + prefixText + "%";
        dt = acc.SearchMrr("T13", prefixText);


        foreach (AccDataSet.AccCoaGroupCodeSetupRow dr in dt.Rows)
        {
            ItemList.Add(dr.Ccg_Code);
        }


        return ItemList;

    }

    [WebMethod]
    public ArrayList GetCustomerAccountCode(String prefixText, int count)
    {
        ArrayList ItemList = new ArrayList();
        DataTable dt = DataProcess.GetData(ConfigurationSettings.AppSettings["SCFConnectionString"].ToString(), "select Par_Acc_Code,Par_Acc_Name from SaMa_Par_Acc where Par_Acc_Code like '%" + prefixText + "%' or par_acc_name like '%" + prefixText + "%'");

        foreach (DataRow dr in dt.Rows)
        {
            ItemList.Add(dr["Par_Acc_Code"].ToString() + "::" + dr["Par_Acc_Name"].ToString());
        }


        return ItemList;


    }

    [WebMethod]
    public ArrayList GetIssue(String prefixText, int count)
    {
        ArrayList ItemList = new ArrayList();

        InTr_Trn_HdrTableAdapter hdr = new InTr_Trn_HdrTableAdapter();
        ErpDataSet.InTr_Trn_HdrDataTable dt = new ErpDataSet.InTr_Trn_HdrDataTable();


        prefixText = "%" + prefixText + "%";
        dt = hdr.SearchForRet(prefixText);


        foreach (ErpDataSet.InTr_Trn_HdrRow dr in dt.Rows)
        {
            ItemList.Add(dr.Trn_Hdr_Ref + ":" + dr.Trn_Hdr_Pcode + ":" + dr.Trn_Hdr_Dcode);
        }


        return ItemList;

    }
    
    
    [WebMethod]
    public ArrayList GetPaymentRequest(String prefixText, int count)
    {
        ArrayList ItemList = new ArrayList();

        tbl_payment_request_detTableAdapter pay = new tbl_payment_request_detTableAdapter();
        SCBL3DataSet.tbl_payment_request_detDataTable dt = new SCBL3DataSet.tbl_payment_request_detDataTable();
               
        dt = pay.SearchPay(prefixText);


        foreach (SCBL3DataSet.tbl_payment_request_detRow dr in dt.Rows)
        {
            ItemList.Add(dr.pay_ref_no + ":" + dr.po_ref_no + ":" + dr.supplier_name +":"+dr.pay_status);
        }


        return ItemList;

    }

    [WebMethod]
    public ArrayList GetCustomerAccountAddressCode(String prefixText, int count)
    {
        ArrayList ItemList = new ArrayList();
        DataTable dt = DataProcess.GetData(ConfigurationSettings.AppSettings["SCFConnectionString"].ToString(), "SELECT [Par_Adr_Code],[par_adr_name],[Par_Adr_Sec_Code],[Par_Adr_Line1],[Par_Adr_Line2],[Par_Adr_Line3],[Par_Adr_Line4],[Par_Adr_Line5],[Par_Adr_Cst_No],[Par_Adr_Lst_No],[Par_Adr_Cnt_No],[Par_Adr_Tel_No],[Par_Adr_Fax_No],[Par_Adr_Email_Id],[Par_Adr_Acc_Code],[Par_Adr_Cmt],[Par_Adr_Upd_DATE],[Par_Adr_Trn_Flag],[Par_Adr_Lst_Trn_DATE],[Par_Adr_Ord_Bal],[Par_Adr_Inv_Val],[T_C1],[T_C2],[T_Fl],[T_In]  FROM [SaMa_Par_Adr] WHERE Par_Adr_Code like '%" + prefixText + "%' or par_adr_name like '%" + prefixText + "%'");

        foreach (DataRow dr in dt.Rows)
        {
            ItemList.Add(dr["Par_Adr_Code"].ToString() + "::" + dr["par_adr_name"].ToString());
        }


        return ItemList;

    }

    [WebMethod]
    public ArrayList GetCoaAccountCode(String prefixText, int count)
    {
        ArrayList ItemList = new ArrayList();
        DataTable dt = DataProcess.GetData(ConfigurationSettings.AppSettings["SCFConnectionString"].ToString(), "select Gl_coa_code,gl_coa_name from acccoa  where Gl_coa_code like '%" + prefixText + "%' or gl_coa_name like'%" + prefixText + "%'");

        foreach (DataRow dr in dt.Rows)
        {
            ItemList.Add(dr["Gl_coa_code"].ToString() + "::" + dr["gl_coa_name"].ToString());
        }


        return ItemList;

    }

    [WebMethod]
    public ArrayList GetAnalysisAccountCode(String prefixText, int count)
  {
        ArrayList ItemList = new ArrayList();
        DataTable dt = DataProcess.GetData(ConfigurationSettings.AppSettings["SCFConnectionString"].ToString(), "Select cost_id,cost_name from AccCoaGroupSetup where cost_id like 'T%' and (cost_id like '%" + prefixText + "%' or cost_name like'%" + prefixText + "%')");

        foreach (DataRow dr in dt.Rows)
        {
            ItemList.Add(dr["cost_id"].ToString() + "::" + dr["cost_name"].ToString());
        }


        return ItemList;

    }

    [WebMethod]
    public ArrayList GetCoaAccountGroupCode1(String prefixText, int count)
    {
        ArrayList ItemList = new ArrayList();
        DataTable dt = DataProcess.GetData(ConfigurationSettings.AppSettings["SCFConnectionString"].ToString(), "Select Ccg_Code,Ccg_Name from AccCoaGroupCodeSetup where CCG_Cost_Id='A1' and (Ccg_Code like '%" + prefixText + "%' or Ccg_Name like'%" + prefixText + "%')");

        foreach (DataRow dr in dt.Rows)
        {
            ItemList.Add(dr["Ccg_Code"].ToString() + "::" + dr["Ccg_Name"].ToString());
        }


        return ItemList;

    }
    [WebMethod]
    public ArrayList GetCoaAccountGroupCode2(String prefixText, int count)
    {
        ArrayList ItemList = new ArrayList();
        DataTable dt = DataProcess.GetData(ConfigurationSettings.AppSettings["SCFConnectionString"].ToString(), "Select Ccg_Code,Ccg_Name from AccCoaGroupCodeSetup where CCG_Cost_Id='A2' and (Ccg_Code like '%" + prefixText + "%' or Ccg_Name like'%" + prefixText + "%')");

        foreach (DataRow dr in dt.Rows)
        {
            ItemList.Add(dr["Ccg_Code"].ToString() + "::" + dr["Ccg_Name"].ToString());
        }


        return ItemList;

    }
    [WebMethod]
    public ArrayList GetCoaAccountGroupCode3(String prefixText, int count)
    {
        ArrayList ItemList = new ArrayList();
        DataTable dt = DataProcess.GetData(ConfigurationSettings.AppSettings["SCFConnectionString"].ToString(), "Select Ccg_Code,Ccg_Name from AccCoaGroupCodeSetup where CCG_Cost_Id='A3' and (Ccg_Code like '%" + prefixText + "%' or Ccg_Name like'%" + prefixText + "%')");

        foreach (DataRow dr in dt.Rows)
        {
            ItemList.Add(dr["Ccg_Code"].ToString() + "::" + dr["Ccg_Name"].ToString());
        }


        return ItemList;

    }

    [WebMethod]
    public ArrayList GetCoaAccountGroupAnalysis(String prefixText, int count, string contextKey)
    {
        ArrayList ItemList = new ArrayList();
        DataTable dt = DataProcess.GetData(ConfigurationSettings.AppSettings["SCFConnectionString"].ToString(), "Select Ccg_Code,Ccg_Name,Ccg_Cost_Id from AccCoaGroupCodeSetup where CCG_Cost_Id='" + contextKey + "' and (Ccg_Code like '%" + prefixText + "%' or Ccg_Name like'%" + prefixText + "%')");


        foreach (DataRow dr in dt.Rows)
        {
            ItemList.Add(dr["Ccg_Code"].ToString().Trim() + ":" + dr["Ccg_Name"].ToString().Trim() + ":" + dr["Ccg_Cost_Id"].ToString().Trim());
        }

        return ItemList;

    }


    [WebMethod]
    public string[] GetAllMprref(string prefixText, int count, string contextKey)
    {
        PuTr_IN_HdrTableAdapter mpr = new PuTr_IN_HdrTableAdapter();
        SCBLDataSet.PuTr_IN_HdrDataTable dtmpr = new SCBLDataSet.PuTr_IN_HdrDataTable();

        dtmpr = mpr.GetDataByDateRangeSearch(Convert.ToDateTime(contextKey.Split(':')[0]), Convert.ToDateTime(contextKey.Split(':')[1]), prefixText);

        string[] str;

        str = new string[dtmpr.Rows.Count];

        int indx = 0;

        foreach (SCBLDataSet.PuTr_IN_HdrRow dr in dtmpr.Rows)
        {
            str[indx] = dr.IN_Hdr_Ref.ToString().Trim();
            indx++;
        }

        return str;
    }


    [WebMethod]
    public string[] GetAllSrref(string prefixText, int count, string contextKey)
    {
        InTr_Sr_HdrTableAdapter sr = new InTr_Sr_HdrTableAdapter();
        LibraryDAL.SCBLIN.InTr_Sr_HdrDataTable dtsr = new SCBLIN.InTr_Sr_HdrDataTable();

        dtsr = sr.GetDataByDateRangeSearch(Convert.ToDateTime(contextKey.Split(':')[0]), Convert.ToDateTime(contextKey.Split(':')[1]), prefixText);

        string[] str;

        str = new string[dtsr.Rows.Count];

        int indx = 0;

        foreach (SCBLIN.InTr_Sr_HdrRow dr in dtsr.Rows)
        {
            str[indx] = dr.Sr_Hdr_Ref .ToString().Trim();
            indx++;
        }

        return str;
    }



   


}