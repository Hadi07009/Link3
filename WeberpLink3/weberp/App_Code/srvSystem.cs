using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using LibraryDAL;
using LibraryDAL.dsTransportTableAdapters;
using LibraryDAL.dsLinkofficeTableAdapters;
using LibraryDAL.dsScfTableAdapters;
using LibraryDAL.dsMovementTableAdapters;
using System.Collections;
using LibraryDAL.SCBLINTableAdapters;
using LibraryDAL.SCBLDataSetTableAdapters;
using LibraryPF.dsTransactionTableAdapters;
using LibraryPF;



/// <summary>
/// Summary description for srvSystem
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]

public class srvSystem : System.Web.Services.WebService
{

    public srvSystem()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
  
    public string[] GetUserSearch(string prefixText, int count)
    {
        tblUserInfoTableAdapter usr = new tblUserInfoTableAdapter();
        dsLinkoffice.tblUserInfoDataTable dtuser = new dsLinkoffice.tblUserInfoDataTable();
         string companyCode =current.CompanyCode;
        int maxsize = 100;

        if (prefixText == "*")
        {
            dtuser = usr.GetAllUser(companyCode);
        }
        else
        {
            prefixText = "%" + prefixText + "%";
            dtuser = usr.GetUserSearch(prefixText, companyCode);
        }


        string[] str;

        if (dtuser.Rows.Count > maxsize)
            str = new string[maxsize];
        else
            str = new string[dtuser.Rows.Count];

        int indx = 0;


        foreach (dsLinkoffice.tblUserInfoRow dr in dtuser.Rows)
        {
            str[indx] = dr.UserEmpId.ToString() + ":" + dr.UserName.ToString();
            indx++;

            if (indx == maxsize) break;
        }

        return str;
    }


    [WebMethod]

    public string[] GetUserSearchByUseridEmpidCompanyid(string prefixText, int count)
    {
        tblUserInfoTableAdapter usr = new tblUserInfoTableAdapter();
        dsLinkoffice.tblUserInfoDataTable dtuser = new dsLinkoffice.tblUserInfoDataTable();
        string companyCode = current.CompanyCode;
        int maxsize = 100;

        if (prefixText == "*")
        {
            dtuser = usr.GetAllUser(companyCode);
        }
        else
        {
            prefixText = "%" + prefixText + "%";
            dtuser = usr.GetUserSearch(prefixText, companyCode);
        }


        string[] str;

        if (dtuser.Rows.Count > maxsize)
            str = new string[maxsize];
        else
            str = new string[dtuser.Rows.Count];

        int indx = 0;


        foreach (dsLinkoffice.tblUserInfoRow dr in dtuser.Rows)
        {
            str[indx] = dr.UserEmpId.ToString() + ":" + dr.UserName.ToString() + " AD:" + dr.UserId.ToString();
            indx++;

            if (indx == maxsize) break;
        }

        return str;
    }

        [WebMethod]
    public string[] GetVehicleSearch(string prefixText, int count)
    {
        PuMa_Tran_MasTableAdapter mas = new PuMa_Tran_MasTableAdapter();
        dsScf.PuMa_Tran_MasDataTable dtmas = new dsScf.PuMa_Tran_MasDataTable();
        
        
        dtmas = mas.GetDataBySearch("R",prefixText);

        string[] str;

        str = new string[dtmas.Rows.Count];

        int indx = 0;


        foreach (dsScf.PuMa_Tran_MasRow dr in dtmas.Rows)
        {
            str[indx] = dr.Tran_Mas_Code.ToString() + ":" + dr.Tran_Mas_Name.ToString();
            indx++;          
        }

        return str;
    }

        [WebMethod]
        public string[] GetVehicleBillSearchBill(string prefixText, int count)
        {
            DtTripforBillTableAdapter mas = new DtTripforBillTableAdapter();
            dsTransport.DtTripforBillDataTable dtmas = new dsTransport.DtTripforBillDataTable();


            dtmas = mas.GetDataBySearchBill(prefixText);

            string[] str;

            str = new string[dtmas.Rows.Count];

            int indx = 0;


            foreach (dsTransport.DtTripforBillRow dr in dtmas.Rows)
            {
                str[indx] = dr.mo_hdr_com10.ToString() + ":" + dr.tran_mas_name.ToString();
                indx++;
            }

            return str;
        }

        [WebMethod]
        public string[] GetVehicleBillSearchAdv(string prefixText, int count)
        {
            DtTripforBillTableAdapter mas = new DtTripforBillTableAdapter();
            dsTransport.DtTripforBillDataTable dtmas = new dsTransport.DtTripforBillDataTable();


            dtmas = mas.GetDataBySearchAdv(prefixText);

            string[] str;

            str = new string[dtmas.Rows.Count];

            int indx = 0;


            foreach (dsTransport.DtTripforBillRow dr in dtmas.Rows)
            {
                str[indx] = dr.mo_hdr_com10.ToString() + ":" + dr.tran_mas_name.ToString();
                indx++;
            }

            return str;
        } 
     [WebMethod]
        public string[] GetDriverSearch(string prefixText, int count)
        {
            Fa_Payee_Adr2TableAdapter mas = new Fa_Payee_Adr2TableAdapter();
            dsTransport.Fa_Payee_Adr2DataTable dtmas = new dsTransport.Fa_Payee_Adr2DataTable();


            dtmas = mas.GetDataBySearch(prefixText);

            string[] str;

            str = new string[dtmas.Rows.Count];

            int indx = 0;


            foreach (dsTransport.Fa_Payee_Adr2Row dr in dtmas.Rows)
            {
                str[indx] = dr.Payee_Adr_Code.ToString() + ":" + dr.Payee_Adr_Name.ToString();
                indx++;
            }

            return str;
        }


     [WebMethod]
     public string[] GetTc(string prefixText, int count)
     {
         dtTcTableAdapter tc = new dtTcTableAdapter();
         dsMovement.dtTcDataTable dttc = new dsMovement.dtTcDataTable();


         dttc = tc.SearchData("TRN", "P", "P", "P", prefixText);

         string[] str;

         str = new string[dttc.Rows.Count];

         int indx = 0;


         foreach (dsMovement.dtTcRow dr in dttc.Rows)
         {
             str[indx] = dr.Mo_Hdr_Ref.ToString() + ":" + dr.Mo_Hdr_DATE.ToShortDateString() + ":" + dr.Par_Acc_Name.ToString() + ":" + dr.Mo_Det_Org_QTY.ToString();
             indx++;
         }

         return str;
     }

     [WebMethod]
     public string[] GetMo(string prefixText, int count)
     {
         dtTcTableAdapter tc = new dtTcTableAdapter();
         dsMovement.dtTcDataTable dttc = new dsMovement.dtTcDataTable();


         dttc = tc.SearchData("MOV", "H", "P", "B", prefixText); 

         string[] str;

         str = new string[dttc.Rows.Count];

         int indx = 0;


         foreach (dsMovement.dtTcRow dr in dttc.Rows)
         {
             str[indx] = dr.Mo_Hdr_Ref.ToString() + ":" + dr.Mo_Hdr_DATE.ToShortDateString() + ":" + dr.Par_Acc_Name.ToString() + ":" + dr.Mo_Det_Org_QTY.ToString();
             indx++;
         }

         return str;
     }

     [WebMethod]
     public ArrayList GetAllEmp(string prefixText, int count)
     {
         ArrayList NameList = new ArrayList();
         LibraryDAL.dsMasTableAdapters.Fa_Payee_Adr2TableAdapter emp = new LibraryDAL.dsMasTableAdapters.Fa_Payee_Adr2TableAdapter();
         LibraryDAL.dsMas.Fa_Payee_Adr2DataTable dtemp = new LibraryDAL.dsMas.Fa_Payee_Adr2DataTable();

         dtemp = emp.GetDataBy_Search(prefixText, prefixText);
         foreach (LibraryDAL.dsMas.Fa_Payee_Adr2Row dr in dtemp.Rows)
         {

             NameList.Add(dr.Payee_Adr_Code + ":" + dr.Payee_Adr_Name);

         }
         return NameList;

     }

     [WebMethod]
     public string[] GetAll_Vehicle(string prefixText, int count)
     {
         LibraryDAL.dsMasTableAdapters.Puma_Tran_Mas_fuel_formuTableAdapter ta_formu = new LibraryDAL.dsMasTableAdapters.Puma_Tran_Mas_fuel_formuTableAdapter();
         LibraryDAL.dsMas.Puma_Tran_Mas_fuel_formuDataTable dt_formu = new dsMas.Puma_Tran_Mas_fuel_formuDataTable();
         dt_formu = ta_formu.GetDataBy_vehicle_name_search(Convert.ToInt32(prefixText), prefixText);


         string[] str;

         str = new string[dt_formu.Rows.Count];

         int indx = 0;


         foreach (LibraryDAL.dsMas.Puma_Tran_Mas_fuel_formuRow dr in dt_formu.Rows)
         {
             str[indx] = dr.Tran_mas_fuel_id + ":" + dr.Tran_mas_fuel_ty_vhei;
             indx++;
         }

         return str;
     }

     [WebMethod]
     public string[] GetFobMovement(string prefixText, int count)
     {

         LibraryDAL.dsRtdTableAdapters.tbl_tc_headerTableAdapter tc = new LibraryDAL.dsRtdTableAdapters.tbl_tc_headerTableAdapter();
         dsRtd.tbl_tc_headerDataTable dttc = new dsRtd.tbl_tc_headerDataTable();

         dttc = tc.SearchFobMo("F", prefixText);



         string[] str;

         str = new string[dttc.Rows.Count];

         int indx = 0;


         foreach (dsRtd.tbl_tc_headerRow dr in dttc.Rows)
         {
             str[indx] = dr.ref_no + ":" + dr.truck_no;
             indx++;
         }

         return str;
     }

     [WebMethod]
     public string[] GetFobDo(string prefixText, int count)
     {

         LibraryDAL.ErpDataSetTableAdapters.InTr_Trn_HdrTableAdapter trn = new LibraryDAL.ErpDataSetTableAdapters.InTr_Trn_HdrTableAdapter();
         ErpDataSet.InTr_Trn_HdrDataTable dttrn = new ErpDataSet.InTr_Trn_HdrDataTable();

         dttrn = trn.SearchDO("II", prefixText);



         string[] str;

         str = new string[dttrn.Rows.Count];

         int indx = 0;


         foreach (ErpDataSet.InTr_Trn_HdrRow dr in dttrn.Rows)
         {
             str[indx] = dr.Trn_Hdr_Ref;
             indx++;
         }

         return str;
     } 

    // Get Cost Center for Consumption report


     [WebMethod]
     public string[] GetCOACode(string prefixText, int count)
     {
         AccCoaGroupCodeSetupconsumptionTableAdapter cost = new AccCoaGroupCodeSetupconsumptionTableAdapter();
         SCBLIN.AccCoaGroupCodeSetupconsumptionDataTable dtcost = new SCBLIN.AccCoaGroupCodeSetupconsumptionDataTable();

         dtcost = cost.GetDataAll(prefixText);

        
         string[] str;

         str = new string[dtcost.Rows.Count];

         int indx = 0;

         foreach (SCBLIN.AccCoaGroupCodeSetupconsumptionRow dr in dtcost.Rows)
         {
             str[indx] = dr.Ccg_Code.ToString().Trim() + ":" + dr.Ccg_Name.ToString().Trim();
             indx++;
         }

         return str;
     }


     [WebMethod]

     public string[] GetEmployee(string prefixText, int count)
     {
         LibraryPF.dsMasterDataTableAdapters.view_employee_infoTableAdapter emp = new LibraryPF.dsMasterDataTableAdapters.view_employee_infoTableAdapter();
         LibraryPF.dsMasterData.view_employee_infoDataTable dtemp = new LibraryPF.dsMasterData.view_employee_infoDataTable();

         int maxsize = 30;

         if (prefixText == "*")
         {
             dtemp = emp.GetData();
         }
         else
         {
             prefixText = "%" + prefixText + "%";
             dtemp = emp.SearchEmployee(prefixText);
         }


         string[] str;

         if (dtemp.Rows.Count > maxsize)
             str = new string[maxsize];
         else
             str = new string[dtemp.Rows.Count];

         int indx = 0;


         foreach (LibraryPF.dsMasterData.view_employee_infoRow dr in dtemp.Rows)
         {
             str[indx] = dr.emp_code.ToString() + ":" + dr.emp_name.ToString();
             indx++;

             if (indx == maxsize) break;
         }

         return str;
     }


     [WebMethod]

     public string[] GetEmployeeLoan(string prefixText, int count)
     {
         hrms_pf_loan_hdrTableAdapter loan = new hrms_pf_loan_hdrTableAdapter();
         dsTransaction.hrms_pf_loan_hdrDataTable dtloan = new dsTransaction.hrms_pf_loan_hdrDataTable();

         int maxsize = 30;


         prefixText = "%" + prefixText + "%";
         dtloan = loan.SearchLoan(prefixText);



         string[] str;

         if (dtloan.Rows.Count > maxsize)
             str = new string[maxsize];
         else
             str = new string[dtloan.Rows.Count];

         int indx = 0;


         foreach (dsTransaction.hrms_pf_loan_hdrRow dr in dtloan.Rows)
         {
             str[indx] = dr.emp_code.ToString() + ":" + dr.loan_ref_no.ToString() + ":" + dr.loan_status;
             indx++;

             if (indx == maxsize) break;
         }

         return str;
     }
    
}

