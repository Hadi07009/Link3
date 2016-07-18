using System;
using System.Collections.Generic;
using System.Web.Services;
using LibraryDAL;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Data;
using System.Linq;
using System.Web;
using System.Collections;
using LibraryDAL;
using ADODB;
using System.Configuration;
using LibraryDAL.SCBLDataSetTableAdapters;
using LibraryDAL.SCBL3DataSetTableAdapters;
using LibraryDAL.ErpDataSetTableAdapters;
using LibraryDAL.AccDataSetTableAdapters;
using LibraryDAL.SCBLINTableAdapters;
using LibraryDAL.AccDataSetTableAdapters;
using LibraryDAL.dsLinkofficeTableAdapters;


[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class InvAutoComplete : System.Web.Services.WebService
{

    public InvAutoComplete()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    #region imran
    [WebMethod]
    public ArrayList GetInvItemList(String prefixText, int count)
    {
        ArrayList ItemList = new ArrayList();
        Recordset rs = new Recordset();
        Connection DC = new Connection();
        string constr, str;

        constr = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
        DC.Open(constr, null, null, 0);

        if(prefixText=="*")
            str = "SELECT distinct top 100 Itm_Det_ICode, Itm_Det_Desc FROM InMa_Itm_Det WHERE itm_det_bom_flag='N' ORDER BY Itm_Det_ICode";
        else
            str = "SELECT distinct top 100 Itm_Det_ICode, Itm_Det_Desc FROM InMa_Itm_Det WHERE itm_det_bom_flag='N' and (Itm_Det_ICode like '%" + prefixText + "%' or Itm_Det_Desc like '%" + prefixText + "%') ORDER BY Itm_Det_ICode";

        rs.Open(str, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);

        while (!rs.EOF)
        {
            ItemList.Add(rs.Fields[0].Value.ToString() + ":" + rs.Fields[1].Value.ToString());

            rs.MoveNext();
        }

        rs.Close();
        DC.Close();

        return ItemList;
    }

    [WebMethod]
    public List<string> GetInvItem(string prefixText, int count, String contextKey)
    {
        List<string> ItemList = new List<string>();

        string queryString = "";
        if (prefixText == "*")
            queryString = "SELECT distinct top 100 Itm_Det_ICode, Itm_Det_Desc FROM InMa_Itm_Det  ORDER BY Itm_Det_ICode";
        else
            queryString = "SELECT distinct top 100 Itm_Det_ICode, Itm_Det_Desc FROM InMa_Itm_Det WHERE (Itm_Det_ICode like '%" + prefixText + "%' or Itm_Det_Desc like '%" + prefixText + "%') ORDER BY Itm_Det_ICode";

        DataTable dtItem = DataProcess.GetData(contextKey, queryString);

        foreach (DataRow dr in dtItem.Rows)
        {
            ItemList.Add(dr["Itm_Det_ICode"].ToString() + ":" + dr["Itm_Det_Desc"].ToString());
        }
        return ItemList;
    }

    [WebMethod]
    public ArrayList GetInvSupplierList(String prefixText, int count)
    {
        ArrayList SupplierList = new ArrayList();
        Recordset rs = new Recordset();
        Connection DC = new Connection();
        string constr, str;

        constr = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

        DC.Open(constr, null, null, 0);

        str = "Select Par_Acc_Code,Par_Acc_Name from PuMa_Par_Acc where Par_Acc_Code like '%" + prefixText + "%' or Par_Acc_Name like '%" + prefixText + "%' order by Par_Acc_Code ";

        rs.Open(str, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);

        while (!rs.EOF)
        {
            SupplierList.Add(rs.Fields[0].Value.ToString() + ":" + rs.Fields[1].Value.ToString());

            rs.MoveNext();
        }

        rs.Close();
        DC.Close();

        return SupplierList;
    }

    [WebMethod]
    public ArrayList GetInvPartyList(String prefixText, int count)
    {
        ArrayList PartyList = new ArrayList();
        Recordset rs = new Recordset();
        Connection DC = new Connection();
        string constr, str;

        constr = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

        DC.Open(constr, null, null, 0);

        str = "select par_adr_code,par_adr_name,par_adr_line_1 from puma_par_adr_view where par_adr_code like '%" + prefixText + "%' or par_adr_name like '%" + prefixText + "%' order by par_adr_code ";

        rs.Open(str, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);

        while (!rs.EOF)
        {
            PartyList.Add(rs.Fields[0].Value.ToString() + ":" + rs.Fields[1].Value.ToString());

            rs.MoveNext();
        }

        rs.Close();
        DC.Close();

        return PartyList;
    }

    [WebMethod]
    public ArrayList GetInvStoreList(String prefixText, int count)
    {
        ArrayList StoreList = new ArrayList();
        Recordset rs = new Recordset();
        Connection DC = new Connection();
        string constr, str;

        constr = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

        DC.Open(constr, null, null, 0);

        if(prefixText=="*")
            str = "select Str_Loc_Id,Str_Loc_Name from InMa_Str_Loc order by Str_Loc_Name ";
        else
            str = "select Str_Loc_Id,Str_Loc_Name from InMa_Str_Loc where Str_Loc_Id like '%" + prefixText + "%' or Str_Loc_Name like '%" + prefixText + "%' order by Str_Loc_Name ";

        rs.Open(str, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);

        while (!rs.EOF)
        {
            StoreList.Add(rs.Fields[0].Value.ToString() + ":" + rs.Fields[1].Value.ToString());

            rs.MoveNext();
        }

        rs.Close();
        DC.Close();

        return StoreList;
    }

    [WebMethod]
    public List<string> GetInvStoreList(String prefixText, int count, String contextKey)
    {
        List<string> storeList = new List<string>();
        string queryString = "";

        if (prefixText == "*")
            queryString = "select Str_Loc_Id,Str_Loc_Name from InMa_Str_Loc order by Str_Loc_Name ";
        else
            queryString = "select Str_Loc_Id,Str_Loc_Name from InMa_Str_Loc where Str_Loc_Id like '%" + prefixText + "%' or Str_Loc_Name like '%" + prefixText + "%' order by Str_Loc_Name ";

        DataTable dtStore = DataProcess.GetData(contextKey, queryString);
        foreach (DataRow dr in dtStore.Rows)
        {
            storeList.Add(dr["Str_Loc_Id"].ToString() + ":" + dr["Str_Loc_Name"].ToString());
        }
        return storeList;
    }

    [WebMethod]
    public ArrayList GetInvPoListAll(String prefixText, int count)
    {
        ArrayList StoreList = new ArrayList();
        Recordset rs = new Recordset();
        Connection DC = new Connection();
        string constr, str;

        constr = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

        DC.Open(constr, null, null, 0);

        str = "Select PO_Hdr_Ref, Par_Acc_Name from PuTr_PO_Hdr left outer join PuMa_Par_Acc on PO_Hdr_Pcode=Par_Acc_Code where PO_Hdr_Ref like '%" + prefixText + "%' or Par_Acc_Name like '%" + prefixText + "%' order by PO_Hdr_Ref DESC";

        rs.Open(str, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);

        while (!rs.EOF)
        {
            StoreList.Add(rs.Fields[0].Value.ToString() + ":" + rs.Fields[1].Value.ToString());

            rs.MoveNext();
        }

        rs.Close();
        DC.Close();

        return StoreList;
    }

    [WebMethod]
    public ArrayList GetInvRemPoList(String prefixText, int count)
    {
        ArrayList StoreList = new ArrayList();
        Recordset rs = new Recordset();
        Connection DC = new Connection();
        string constr, str;

        constr = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

        DC.Open(constr, null, null, 0);

        str = "Select Distinct Po_Det_Ref,Par_Acc_Name from PuTr_PO_Hdr left outer join PuTr_PO_Det " +
              " on PO_Hdr_Type=PO_Det_Type and PO_Hdr_Code=PO_Det_Code and PO_Hdr_Ref=PO_Det_Ref " +
              " left  outer join PuMa_Par_Acc on PO_Hdr_Pcode=Par_Acc_Code where PO_Hdr_HPC_Flag='P' AND Po_Det_Bal_qty > 0 " +
              " and  Po_Det_Ref like '%" + prefixText + "%' or Par_Acc_Name like '%" + prefixText + "%' order by Po_Det_Ref desc";

        rs.Open(str, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);

        while (!rs.EOF)
        {
            StoreList.Add(rs.Fields[0].Value.ToString() + ":" + rs.Fields[1].Value.ToString());

            rs.MoveNext();
        }

        rs.Close();
        DC.Close();

        return StoreList;
    }

    [WebMethod]
    public ArrayList GetInvMrrListAll(String prefixText, int count)
    {
        ArrayList StoreList = new ArrayList();
        Recordset rs = new Recordset();
        Connection DC = new Connection();
        string constr, str;

        constr = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

        DC.Open(constr, null, null, 0);

        str = "Select Trn_Hdr_Ref,Par_Acc_Name from InTr_Trn_Hdr left outer join PuMa_Par_Acc on Trn_Hdr_Pcode=Par_Acc_Code where Trn_Hdr_Type='RC' and Trn_Hdr_Code='PO' and Trn_Hdr_Ref like '%" + prefixText + "%' or Par_Acc_Name like '%" + prefixText + "%' order by Trn_Hdr_Ref DESC";

        rs.Open(str, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);

        while (!rs.EOF)
        {
            StoreList.Add(rs.Fields[0].Value.ToString() + ":" + rs.Fields[1].Value.ToString());

            rs.MoveNext();
        }

        rs.Close();
        DC.Close();

        return StoreList;
    }

    [WebMethod]
    public ArrayList GetInvSerialList(String prefixText, int count, string contextKey)
    {
        clsDbCon dbCon = new clsDbCon();
        string qryStr;

        ArrayList StoreList = new ArrayList();
        Recordset rs = new Recordset();
        Connection DC = new Connection();
        string constr, str;
        string itemCode="", stroreCode="", serialNo="";

        var strContextKey = contextKey.Split(':');
        if (strContextKey.Length>=2)
        {
            itemCode = strContextKey[0];
            stroreCode = strContextKey[1];
            serialNo = strContextKey[2];
        }

        var serialQry = "";
        if (serialNo!="")
        {
            var allSerial = serialNo.Split(',');            
            foreach (var s in allSerial)
            {
                serialQry = serialQry + "'" + s + "',";
            }
            serialQry = serialQry.Substring(0, serialQry.Length - 1);
        }
        else
        {
            serialQry = "''";
        }

        constr = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

        qryStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[last_trn_status]')) " +
                 " DROP VIEW [dbo].[last_trn_status]";
        dbCon.ExecuteL3TSQLStmt(qryStr);

        qryStr =
            "CREATE VIEW last_trn_status AS SELECT itm_det_icode, itm_det_serial_no, MAX(sl_no) AS lasttrn  From Inma_itm_serial where itm_det_date <=convert(datetime,'" +
            DateTime.Now.ToString("dd/MM/yyyy") + "',103) and itm_det_icode ='" + itemCode +
            "' GROUP BY itm_det_icode, itm_det_serial_no ";
        dbCon.ExecuteL3TSQLStmt(qryStr);

        DC.Open(constr, null, null, 0);

        
        if (prefixText.ToUpper()=="ALL")
        {
            str = "select Distinct itm_det_serial_no from last_trn_serial_status Where itm_det_icode='" + itemCode +
                  "' and itm_det_str_code='" + stroreCode + "' and itm_det_serial_no not in (" + serialQry +
                  ") order by itm_det_serial_no";
        }
        else
        {
            str = "select Distinct itm_det_serial_no from last_trn_serial_status Where itm_det_icode='" + itemCode +
                  "' and itm_det_str_code='" + stroreCode + "' and itm_det_serial_no not in (" + serialQry +
                  ") and itm_det_serial_no like '%" + prefixText + "%' order by itm_det_serial_no";
        }        

        rs.Open(str, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);

        while (!rs.EOF)
        {
            StoreList.Add(rs.Fields[0].Value.ToString());

            rs.MoveNext();
        }

        rs.Close();
        DC.Close();

        return StoreList;
    }

    [WebMethod]
    public ArrayList GetInvTransListAll(String prefixText, int count)
    {
        ArrayList StoreList = new ArrayList();
        Recordset rs = new Recordset();
        Connection DC = new Connection();
        string constr, str;

        constr = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

        DC.Open(constr, null, null, 0);

        str = "Select Distinct Trn_Hdr_Ref,Str_Loc_Name from InTr_Trn_Hdr left outer join Inma_Str_Loc on Trn_Hdr_Pcode=Str_Loc_ID where Trn_Hdr_Type in ('IT','RT') and Trn_Hdr_Code='STR' and Trn_Hdr_Ref like '%" + prefixText + "%' or Str_Loc_Name like '%" + prefixText + "%' order by Trn_Hdr_Ref DESC";

        rs.Open(str, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);

        while (!rs.EOF)
        {
            StoreList.Add(rs.Fields[0].Value.ToString() + ":" + rs.Fields[1].Value.ToString());

            rs.MoveNext();
        }

        rs.Close();
        DC.Close();

        return StoreList;
    }

    [WebMethod]
    public ArrayList GetInvSrListAll(String prefixText, int count)
    {
        ArrayList StoreList = new ArrayList();
        Recordset rs = new Recordset();
        Connection DC = new Connection();
        string constr, str;

        constr = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

        DC.Open(constr, null, null, 0);
        str = "WITH ReqFor (ReqCode,ReqName) as (Select Par_Acc_Code,Par_Acc_Name from SaMa_Par_Acc " +
            "union select Ccg_Code,RTRIM(LTRIM(Ccg_Name)) from FA_COM_CCG where Ccg_Cost_Id='T02' ) " +
            "Select Sr_Hdr_Ref,RTRIM(LTRIM(ReqName)) from InTr_Sr_Hdr left outer join ReqFor on Sr_Hdr_Pcode=ReqCode " +
            "Where Sr_Hdr_Ref like '%" + prefixText + "%' or ReqName like '%" + prefixText + "%'order by Sr_Hdr_Ref DESC";

        rs.Open(str, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);

        while (!rs.EOF)
        {
            StoreList.Add(rs.Fields[0].Value.ToString() + ":" + rs.Fields[1].Value.ToString());

            rs.MoveNext();
        }

        rs.Close();
        DC.Close();

        return StoreList;
    }

    [WebMethod]
    public List<string> GetInvSrListAll(String prefixText, int count, String contextKey)
    {
        List<string> invSrListAll = new List<string>();
        string queryString = "WITH ReqFor (ReqCode,ReqName) as (Select Par_Acc_Code,Par_Acc_Name from SaMa_Par_Acc " +
            "union select Ccg_Code,RTRIM(LTRIM(Ccg_Name)) from FA_COM_CCG where Ccg_Cost_Id='T02' ) " +
            "Select Sr_Hdr_Ref,RTRIM(LTRIM(ReqName)) from InTr_Sr_Hdr left outer join ReqFor on Sr_Hdr_Pcode=ReqCode " +
            "Where Sr_Hdr_Ref like '%" + prefixText + "%' or ReqName like '%" + prefixText + "%'order by Sr_Hdr_Ref DESC";
        DataTable dtInvSrListAll = DataProcess.GetData(contextKey, queryString);
        foreach (DataRow dr in dtInvSrListAll.Rows)
        {
            invSrListAll.Add(dr["Sr_Hdr_Ref"].ToString() + ":" + dr[1].ToString());
        }
        return invSrListAll;
    }

    [WebMethod]
    public List<string> GetInvRemSrList(String prefixText, int count)
    {       
        string connString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
        List<string> invListAll = new List<string>();
        string queryString = null;
       
        queryString = "WITH ReqFor (ReqCode,ReqName) as (Select Par_Acc_Code,Par_Acc_Name from SaMa_Par_Acc " +
              "union select Ccg_Code,RTRIM(LTRIM(Ccg_Name)) from acccoagroupcodesetup where Ccg_Cost_Id='T01' ) " +
              "select distinct Sr_Hdr_Ref,ReqName from InTr_Sr_Hdr left outer join InTr_Sr_Det " +
              "on Sr_Hdr_Type=Sr_Det_Type and Sr_Hdr_Code=Sr_Det_Code and Sr_Hdr_Ref=Sr_Det_Ref " +
              "left outer join ReqFor on Sr_Hdr_Pcode=ReqCode where Sr_Hdr_HPC_Flag='P' " +
              "and Sr_Det_Bal_Qty>0 and (Sr_Hdr_Ref like '%" + prefixText + "%' or ReqName like '%" + prefixText + "%') " +
              "order by Sr_Hdr_Ref desc";        

        DataTable dtInvSrListAll = DataProcess.GetData(connString, queryString);

        foreach (DataRow dr in dtInvSrListAll.Rows)
        {
            invListAll.Add(dr[0].ToString() + ":" + dr[1].ToString());
        }

        return invListAll;
    }

    [WebMethod]
    public ArrayList GetInvLocListAll(String prefixText, int count,string contextKey)
    {
        ArrayList StoreList = new ArrayList();
        Recordset rs = new Recordset();
        Connection DC = new Connection();
        string constr, str;

        constr = System.Configuration.ConfigurationManager.AppSettings["ConStr"].ToString();

        DC.Open(constr, null, null, 0);

        if (contextKey.Trim() == "") return StoreList;

        var srType = "";
        var accCode = "";
        var srDate = DateTime.Now;
        var temp = contextKey.Split(':');
        if (temp.Length>=2)
        {
            srType = temp[0];
            accCode = temp[1];
            srDate = Convert.ToDateTime(temp[2]);
        }
        
        switch (srType)
        {
            case "SR":
                if (prefixText == "*") 
                    str = "Select top 50 Par_Adr_Code,par_adr_name from SaMa_Par_Adr where Par_Adr_Acc_Code='" + accCode + "' order by Par_Adr_Code";
                else
                    str = "Select Par_Adr_Code,par_adr_name from SaMa_Par_Adr where Par_Adr_Acc_Code='" + accCode + "' " +
                    "and (Par_Adr_Code like '%" + prefixText + "%' or par_adr_name like '%" + prefixText + "%') order by Par_Adr_Code";

                rs.Open(str, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);


                while (!rs.EOF)
                {
                    StoreList.Add(rs.Fields[0].Value.ToString() + ":" + rs.Fields[1].Value.ToString());

                    rs.MoveNext();
                }

                rs.Close();
                DC.Close();

                return StoreList;            

            case "SRB":
                string analGrp = "";
                str ="Select trn_doc_anal_flag From InSu_Trn_Doc_Anal Where  trn_doc_anal_tr_type = 'IS' And  trn_doc_anal_tr_code = 'SRB'";
                rs.Open(str, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);
                while (!rs.EOF)
                {
                    if (analGrp!="")
                    {
                        analGrp = analGrp + ",'" + rs.Fields[0].Value + "'";
                    }
                    else
                    {
                        analGrp = "'" + rs.Fields[0].Value + "'";
                    }
                    rs.MoveNext();
                }
                rs.Close();
                str = "select Grp_Code_Id,Grp_Code,Grp_Code_Name from InMa_Grp_Code where Grp_Code_Id in (" + analGrp +
                      ") and (Grp_Code like '%" + prefixText + "%' or Grp_Code_Name like '%" + prefixText + "%')";
                rs.Open(str, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);

                while (!rs.EOF)
                {
                    StoreList.Add(rs.Fields[0].Value.ToString() + ":" + rs.Fields[1].Value.ToString() + ":" +
                                  rs.Fields[2].Value.ToString());

                    rs.MoveNext();
                }

                rs.Close();
                DC.Close();

                return StoreList;

            case "SRO":
                DC.Close();
                var constrWfa2 = System.Configuration.ConfigurationManager.AppSettings["ConStr"].ToString();                
                DC.Open(constrWfa2, null, null, 0);

                if (prefixText=="*") 
                    str = "select distinct DeptID,Dept from Emp_Details where EmpID='" + accCode + "' order by DeptID";
                else
                    str = "select distinct DeptID,Dept from Emp_Details where EmpID='" + accCode + "' and ([DeptID] like '%" + prefixText + "%' or [Dept] like '%" + prefixText + "%') order by DeptID";

                rs.Open(str, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);

                while (!rs.EOF)
                {
                    StoreList.Add(rs.Fields[0].Value.ToString() + ":" + rs.Fields[1].Value.ToString());

                    rs.MoveNext();
                }

                rs.Close();
                DC.Close();                

                return StoreList;

            case "SRS":
                if (prefixText == "*")
                    str = "Select top 50 Par_Adr_Code,par_adr_name from SaMa_Par_Adr where Par_Adr_Acc_Code='" + accCode + "' order by Par_Adr_Code";
                else
                    str = "Select Par_Adr_Code,par_adr_name from SaMa_Par_Adr where Par_Adr_Acc_Code='" + accCode + "' " +
                    "and (Par_Adr_Code like '%" + prefixText + "%' or par_adr_name like '%" + prefixText + "%') order by Par_Adr_Code";

                rs.Open(str, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);

                while (!rs.EOF)
                {
                    StoreList.Add(rs.Fields[0].Value.ToString() + ":" + rs.Fields[1].Value.ToString());

                    rs.MoveNext();
                }

                rs.Close();
                DC.Close();

                return StoreList;
        }
        return StoreList;
    }


    [WebMethod]
    public ArrayList GetInvClientListAll(String prefixText, int count,string contextKey)
    {
        ArrayList StoreList = new ArrayList();
        Recordset rs = new Recordset();
        Connection DC = new Connection();
        string constr, str;

        constr = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

        DC.Open(constr, null, null, 0);

        if (contextKey=="SRO")
        {
            if (prefixText == "*")
                str = "select Distinct [Ccg_Code],[Ccg_Name] from  AccCoaGroupCodeSetup Where [Ccg_Cost_Id]='T01' order by Ccg_Code";
            else
                str = "select Distinct [Ccg_Code],[Ccg_Name] from  AccCoaGroupCodeSetup Where [Ccg_Cost_Id]='T01' and (Ccg_Code like '%" + prefixText + "%' or Ccg_Name like '%" + prefixText + "%') order by Ccg_Code";
        }
        else
        {
            if (prefixText=="*") 
                str = "select Distinct top 50 Par_Acc_Code,Par_Acc_Name from  SaMa_Par_Acc order by Par_Acc_Code";
            else
                str = "select Distinct top 50 Par_Acc_Code,Par_Acc_Name from  SaMa_Par_Acc Where Par_Acc_Code like '%" + prefixText + "%' or Par_Acc_Name like '%" + prefixText + "%' order by Par_Acc_Code";
        }

        rs.Open(str, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);

        while (!rs.EOF)
        {
            StoreList.Add((rs.Fields[0].Value.ToString().Trim() + ":" + rs.Fields[1].Value.ToString().Trim()));

            rs.MoveNext();
        }

        rs.Close();
        DC.Close();

        return StoreList;
    }

    [WebMethod]
    public List<string> GetInventoryClientListAll(String prefixText, int count, string contextKey)     
    {
        string connString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
        List<string> invListAll = new List<string>();
        string queryString = null;
        if (contextKey == "SRO")
        {
            if (prefixText == "*")
                queryString = "select Distinct rtrim([Ccg_Code]) as Ccg_Code,rtrim([Ccg_Name])  as Ccg_Name from  AccCoaGroupCodeSetup Where [Ccg_Cost_Id]='T01' order by Ccg_Code";
            else
                queryString = "select Distinct rtrim([Ccg_Code]) as Ccg_Code,rtrim([Ccg_Name])  as Ccg_Name from  AccCoaGroupCodeSetup Where [Ccg_Cost_Id]='T01' and (Ccg_Code like '%" + prefixText + "%' or Ccg_Name like '%" + prefixText + "%') order by Ccg_Code";
        }
        else
        {
            if (prefixText == "*")
                queryString = "select Distinct top 50 Par_Acc_Code,Par_Acc_Name from  SaMa_Par_Acc order by Par_Acc_Code";
            else
                queryString = "select Distinct top 50 Par_Acc_Code,Par_Acc_Name from  SaMa_Par_Acc Where Par_Acc_Code like '%" + prefixText + "%' or Par_Acc_Name like '%" + prefixText + "%' order by Par_Acc_Code";

        }
        DataTable dtInvSrListAll = DataProcess.GetData(connString, queryString);
        foreach (DataRow dr in dtInvSrListAll.Rows)
        {
            invListAll.Add(dr[0].ToString() + ":" + dr[1].ToString());
        }
        return invListAll;
    }

    [WebMethod]
    public ArrayList GetEmpListAll(String prefixText, int count, string contextKey)
    {
        ArrayList StoreList = new ArrayList();
        Recordset rs = new Recordset();
        Connection DC = new Connection();
        string constr, str;

        constr = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

        DC.Open(constr, null, null, 0);

        str = "select distinct [userid],[user_name] from tbl_user_info Where ([status]=1 or resign_date>=Convert(datetime,'" + Convert.ToDateTime(contextKey) + "',103)) and ([userid] like '%" + prefixText + "%' or [user_name] like '%" + prefixText + "%') order by [userid]";

        rs.Open(str, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);

        while (!rs.EOF)
        {
            StoreList.Add(rs.Fields[0].Value.ToString() + ":" + rs.Fields[1].Value.ToString());

            rs.MoveNext();
        }

        rs.Close();
        DC.Close();

        return StoreList;
    }

    [WebMethod]
    public List<string> GetEmployeeListAll(String prefixText, int count, string contextKey)   
    {
        string connString = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();
        List<string> invEmployeeList = new List<string>();
        string queryString = "";

        if (prefixText == "*")
            queryString = "select Distinct rtrim([Ccg_Code]) as Ccg_Code,rtrim([Ccg_Name])  as Ccg_Name from  AccCoaGroupCodeSetup Where [Ccg_Cost_Id]='T01' order by Ccg_Code";
        else
            queryString = "select Distinct rtrim([Ccg_Code]) as Ccg_Code,rtrim([Ccg_Name])  as Ccg_Name from  AccCoaGroupCodeSetup Where [Ccg_Cost_Id]='T01' and (Ccg_Code like '%" + prefixText + "%' or Ccg_Name like '%" + prefixText + "%') order by Ccg_Code";

       
        DataTable dtEmployeeList = DataProcess.GetData(connString, queryString);
        foreach (DataRow dr in dtEmployeeList.Rows)
        {
            invEmployeeList.Add(dr[0].ToString() + ":" + dr[1].ToString());
        }
        return invEmployeeList;
    }

    [WebMethod]
    public ArrayList GetInvIssueListAll(String prefixText, int count)
    {
        ArrayList StoreList = new ArrayList();
        Recordset rs = new Recordset();
        Connection DC = new Connection();
        string constr, str;

        constr = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

        DC.Open(constr, null, null, 0);
        str = "WITH ReqFor (ReqCode,ReqName) as (Select Par_Acc_Code,Par_Acc_Name from SaMa_Par_Acc " +
            "union select Ccg_Code,RTRIM(LTRIM(Ccg_Name)) from FA_COM_CCG where Ccg_Cost_Id='T02' ) " +
            "Select Trn_Hdr_Ref,RTRIM(LTRIM(ReqName)) from InTr_Trn_Hdr left outer join ReqFor on Trn_Hdr_Pcode=ReqCode " +
            "Where Trn_Hdr_Type='IS' and Trn_Hdr_Ref like '%" + prefixText + "%' or ReqName like '%" + prefixText + "%'order by Trn_Hdr_Ref DESC";

        rs.Open(str, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);

        while (!rs.EOF)
        {
            StoreList.Add(rs.Fields[0].Value.ToString() + ":" + rs.Fields[1].Value.ToString());

            rs.MoveNext();
        }

        rs.Close();
        DC.Close();

        return StoreList;
    }
    
    #endregion Imran

    [WebMethod]
    public ArrayList GetAccountCode(String prefixText, int count)
    {
        ArrayList ItemList = new ArrayList();
        Recordset rs = new Recordset();
        Connection DC = new Connection();
        string constr, str;

        constr = System.Configuration.ConfigurationManager.AppSettings["ConStr"].ToString();
        DC.Open(constr, null, null, 0);

        str = "SELECT Gl_Coa_Code,Gl_Coa_Name FROM budg WHERE (Gl_Coa_Code like '%" + prefixText + "%' or Gl_Coa_Name like '%" + prefixText + "%') ORDER BY Gl_Coa_Code";

        rs.Open(str, DC, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic, -1);

        while (!rs.EOF)
        {
            ItemList.Add(rs.Fields[0].Value.ToString() + ":" + rs.Fields[1].Value.ToString());

            rs.MoveNext();
        }

        rs.Close();
        DC.Close();

        return ItemList;
    }

    [WebMethod]
    public List<string> GetItemCodeForRevalueation(string prefixText, int count, String contextKey)
    {
        List<string> ItemList = new List<string>();
        string queryString = "SELECT distinct a.Itm_Det_ICode, a.Itm_Det_Desc FROM InMa_Itm_Det a INNER JOIN fas_item_depreciation b ON a.Itm_Det_ICode = b.ItemCode WHERE b.ItemCurrentLine = 'Y'  and (a.Itm_Det_ICode like '%" + prefixText + "%' or a.Itm_Det_Desc like '%" + prefixText + "%') ORDER BY a.Itm_Det_ICode";
        DataTable dtItem = DataProcess.GetData(contextKey, queryString);
        foreach (DataRow dr in dtItem.Rows)
        {
            ItemList.Add(dr["Itm_Det_ICode"].ToString() + ":" + dr["Itm_Det_Desc"].ToString());
        }
        return ItemList;
    }    

    [WebMethod]
    public ArrayList GetCustomerAccountCode(String prefixText, int count, String contextKey)
    {
        ArrayList ItemList = new ArrayList();
        DataTable dt = DataProcess.GetData(contextKey, "select top 100 Par_Acc_Code,Par_Acc_Name from SaMa_Par_Acc where Par_Acc_Code like '%" + prefixText + "%' or par_acc_name like '%" + prefixText + "%'");
        foreach (DataRow dr in dt.Rows)
        {
            ItemList.Add(dr["Par_Acc_Code"].ToString() + "::" + dr["Par_Acc_Name"].ToString());
        }
        return ItemList;
    }

    [WebMethod]
    public ArrayList GetCoaAccountGroupCode1(String prefixText, int count, String contextKey)
    {
        ArrayList ItemList = new ArrayList();
        DataTable dt = DataProcess.GetData(contextKey, "Select Ccg_Code,Ccg_Name from FA_COM_CCG where CCG_Cost_Id='A1' and (Ccg_Code like '%" + prefixText + "%' or Ccg_Name like'%" + prefixText + "%')");

        foreach (DataRow dr in dt.Rows)
        {
            ItemList.Add(dr["Ccg_Code"].ToString() + "::" + dr["Ccg_Name"].ToString());
        }
        return ItemList;
    }

    [WebMethod]
    public ArrayList GetCoaAccountGroupCode2(String prefixText, int count, String contextKey)
    {
        ArrayList ItemList = new ArrayList();
        DataTable dt = DataProcess.GetData(contextKey, "Select Ccg_Code,Ccg_Name from FA_COM_CCG where CCG_Cost_Id='A2' and (Ccg_Code like '%" + prefixText + "%' or Ccg_Name like'%" + prefixText + "%')");

        foreach (DataRow dr in dt.Rows)
        {
            ItemList.Add(dr["Ccg_Code"].ToString() + "::" + dr["Ccg_Name"].ToString());
        }
        return ItemList;
    }

    [WebMethod]
    public ArrayList GetCoaAccountGroupCode3(String prefixText, int count, String contextKey)
    {
        ArrayList ItemList = new ArrayList();
        DataTable dt = DataProcess.GetData(contextKey, "Select Ccg_Code,Ccg_Name from FA_COM_CCG where CCG_Cost_Id='A3' and (Ccg_Code like '%" + prefixText + "%' or Ccg_Name like'%" + prefixText + "%')");

        foreach (DataRow dr in dt.Rows)
        {
            ItemList.Add(dr["Ccg_Code"].ToString() + "::" + dr["Ccg_Name"].ToString());
        }
        return ItemList;
    }

    [WebMethod]
    public ArrayList GetCoaAccountCode(String prefixText, int count, String contextKey)
    {
        ArrayList ItemList = new ArrayList();
        DataTable dt = DataProcess.GetData(contextKey, "select Gl_coa_code,gl_coa_name from budg  where Gl_coa_code like '%" + prefixText + "%' or gl_coa_name like'%" + prefixText + "%'");

        foreach (DataRow dr in dt.Rows)
        {
            ItemList.Add(dr["Gl_coa_code"].ToString() + "::" + dr["gl_coa_name"].ToString());
        }
        return ItemList;
    }

   
    [WebMethod]
    public List<string> GetCustomerAccountInformation(string prefixText, int count, String contextKey)
    {
        List<string> customerList = new List<string>();
        string queryString = "select top 100 Par_Acc_Code,Par_Acc_Name from SaMa_Par_Acc where (Par_Acc_Code like '%" + prefixText + "%' or Par_Acc_Name like '%" + prefixText + "%') order by Par_Acc_Name";
        DataTable dtCustomer = DataProcess.GetData(contextKey, queryString);
        foreach (DataRow dr in dtCustomer.Rows)
        {
            customerList.Add(dr["Par_Acc_Code"].ToString() + ":" + dr["Par_Acc_Name"].ToString());
        }
        return customerList;
    }

    [WebMethod]
    public List<string> GetSupplierAccountInformation(string prefixText, int count, String contextKey)
    {
        List<string> customerList = new List<string>();
        string queryString = "select Par_Acc_Code,Par_Acc_Name from PuMa_Par_Acc where (Par_Acc_Code like '%" + prefixText + "%' or Par_Acc_Name like '%" + prefixText + "%') order by Par_Acc_Name";
        DataTable dtCustomer = DataProcess.GetData(contextKey, queryString);
        foreach (DataRow dr in dtCustomer.Rows)
        {
            customerList.Add(dr["Par_Acc_Code"].ToString() + ":" + dr["Par_Acc_Name"].ToString());
        }
        return customerList;
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
            dt = acc.GetApprovedDataSearch("APP", prefixText);
        }

        foreach (SCBLDataSet.PuTr_PO_Hdr_ScblRow dr in dt.Rows)
        {
            ItemList.Add(dr.PO_Hdr_Code + ":" + dr.PO_Hdr_Ref + ":" + dr.PO_Hdr_Com1);
        }


        return ItemList;

    }

}

