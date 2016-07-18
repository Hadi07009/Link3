using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Collections;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService {

    public WebService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    public List<string> GetEmpId(string prefixText, int count, String contextKey)
    {
        List<string> ItemList = new List<string>();
        DataTable dt = new DataTable();
        string str = "";
        string constr = contextKey;
        if (prefixText == "*")
        {
            str = "select Emp_Mas_Emp_Id,Emp_Mas_First_Name,Emp_Mas_Last_Name from HrMs_Emp_mas where Emp_Mas_Term_Flg='N' order by Emp_Mas_Emp_Id ";
            //str = "select Emp_Mas_Emp_Id,Emp_Mas_First_Name,Emp_Mas_Last_Name from HrMs_Emp_mas  order by Emp_Mas_Emp_Id ";
        }
        else
        {
            str = "select top 20 Emp_Mas_Emp_Id,Emp_Mas_First_Name,Emp_Mas_Last_Name from HrMs_Emp_mas where (Emp_Mas_Emp_Id like '%" + prefixText + "%'  or (Emp_Mas_First_Name+space(1)+Emp_Mas_Last_Name like '%" + prefixText + "%')) and Emp_Mas_Term_Flg='N' order by Emp_Mas_Emp_Id ";
            //str = "select top 20 Emp_Mas_Emp_Id,Emp_Mas_First_Name,Emp_Mas_Last_Name from HrMs_Emp_mas where (Emp_Mas_Emp_Id like '%" + prefixText + "%'  or (Emp_Mas_First_Name+space(1)+Emp_Mas_Last_Name like '%" + prefixText + "%')) order by Emp_Mas_Emp_Id ";
        }
        dt = DataProcess.GetData(constr, str);
        foreach (DataRow dr in dt.Rows)
        {
            ItemList.Add(dr["Emp_Mas_Emp_Id"].ToString() + ":" + dr["Emp_Mas_First_Name"].ToString() + "" + dr["Emp_Mas_Last_Name"].ToString());
        }
        return ItemList;
    }

    [WebMethod]
    public List<string> GetAllEmployeeId(string prefixText, int count, String contextKey)
    {
        List<string> ItemList = new List<string>();
        DataTable dt = new DataTable();
        string str = "";
        string constr = contextKey;
        if (prefixText == "*")
        {
            //str = "select Emp_Mas_Emp_Id,Emp_Mas_First_Name,Emp_Mas_Last_Name from HrMs_Emp_mas where Emp_Mas_Term_Flg='N' order by Emp_Mas_Emp_Id ";
            str = "select Emp_Mas_Emp_Id,Emp_Mas_First_Name,Emp_Mas_Last_Name from HrMs_Emp_mas  order by Emp_Mas_Emp_Id ";
        }
        else
        {
            //str = "select top 20 Emp_Mas_Emp_Id,Emp_Mas_First_Name,Emp_Mas_Last_Name from HrMs_Emp_mas where (Emp_Mas_Emp_Id like '%" + prefixText + "%'  or (Emp_Mas_First_Name+space(1)+Emp_Mas_Last_Name like '%" + prefixText + "%')) and Emp_Mas_Term_Flg='N' order by Emp_Mas_Emp_Id ";
            str = "select top 20 Emp_Mas_Emp_Id,Emp_Mas_First_Name,Emp_Mas_Last_Name from HrMs_Emp_mas where (Emp_Mas_Emp_Id like '%" + prefixText + "%'  or (Emp_Mas_First_Name+space(1)+Emp_Mas_Last_Name like '%" + prefixText + "%')) order by Emp_Mas_Emp_Id ";
        }
        dt = DataProcess.GetData(constr, str);
        foreach (DataRow dr in dt.Rows)
        {
            ItemList.Add(dr["Emp_Mas_Emp_Id"].ToString() + ":" + dr["Emp_Mas_First_Name"].ToString() + "" + dr["Emp_Mas_Last_Name"].ToString());
        }
        return ItemList;
    }

    [WebMethod]
    public List<string> GetHolidayDescription(string prefixText, int count, String contextKey)
    {
        List<string> ItemList = new List<string>();
        DataTable dt = new DataTable();
        string str = "";
        string configurationType = contextKey.Split(':')[1].ToString();
        string constr = contextKey.Split(':')[0].ToString();
        if (configurationType == "S")
        {
            if (prefixText == "")
            {
                str = "select HolidayDescription from Hrms_HolidaySetup where HolidayDescription != ''  order by HolidayDescription ";
            }
            else
            {
                str = "select HolidayDescription from Hrms_HolidaySetup where (HolidayDescription != '' AND HolidayDescription like '%" + prefixText + "%' )  order by HolidayDescription ";
            }
        }
        else
        {
            if (prefixText == "")
            {
                str = "select HolidayDescription from hrms_holidaysetupEmpWise where HolidayDescription != ''  order by HolidayDescription ";
            }
            else
            {
                str = "select HolidayDescription from hrms_holidaysetupEmpWise where (HolidayDescription != '' AND HolidayDescription like '%" + prefixText + "%' )  order by HolidayDescription ";
            }

        }
        dt = DataProcess.GetData(constr, str);
        foreach (DataRow dr in dt.Rows)
        {
            ItemList.Add(dr["HolidayDescription"].ToString());
        }
        return ItemList;
    }

    [WebMethod]
    public List<string> GetBase(string prefixText, int count, String contextKey)
    {
        List<string> ItemList = new List<string>();
        DataTable dt = new DataTable();
        string str = "";
        string constr = contextKey;
        if (prefixText == "*")
        {
            str = "select DISTINCT For_Mas_Cal_Code,For_Mas_Cal_Name from HrMs_For_Mas order by For_Mas_Cal_Code";
        }
        else
        {
            str = "select DISTINCT For_Mas_Cal_Code,For_Mas_Cal_Name from HrMs_For_Mas where (For_Mas_Cal_Code like '%" + prefixText + "%' or For_Mas_Cal_Name like '%" + prefixText + "%') order by For_Mas_Cal_Name";
        }
        dt = DataProcess.GetData(constr, str);
        foreach (DataRow dr in dt.Rows)
        {
            ItemList.Add(dr["For_Mas_Cal_Code"].ToString() + ':' + dr["For_Mas_Cal_Name"].ToString());
        }
        return ItemList;
    }

    [WebMethod]
    public List<string> GetMultiplier(string prefixText, int count, String contextKey)
    {
        List<string> ItemList = new List<string>();
        DataTable dt = new DataTable();
        string str = "";
        string constr = contextKey;
        if (prefixText == "*")
        {
            str = "select DISTINCT For_Mas_Mul from HrMs_For_Mas order by For_Mas_Mul";
        }
        else
        {
            str = "select DISTINCT For_Mas_Mul from HrMs_For_Mas where (For_Mas_Mul like '%" + prefixText + "%' ) order by For_Mas_Mul";
        }
        dt = DataProcess.GetData(constr, str);
        foreach (DataRow dr in dt.Rows)
        {
            ItemList.Add(dr["For_Mas_Mul"].ToString());
        }
        return ItemList;
    }

    [WebMethod]
    public List<string> GetAccumulationValue(string prefixText, int count, String contextKey)
    {
        List<string> ItemList = new List<string>();
        DataTable dt = new DataTable();
        string str = "";
        string constr = contextKey;
        if (prefixText == "*")
        {
            str = "select DISTINCT For_Mas_Acc_Code from HrMs_For_Mas order by For_Mas_Acc_Code";
        }
        else
        {
            str = "select DISTINCT For_Mas_Acc_Code from HrMs_For_Mas where (For_Mas_Acc_Code like '%" + prefixText + "%' ) order by For_Mas_Acc_Code";
        }
        dt = DataProcess.GetData(constr, str);
        foreach (DataRow dr in dt.Rows)
        {
            ItemList.Add(dr["For_Mas_Acc_Code"].ToString());
        }
        return ItemList;
    }

    #region Process Details

    [WebMethod]
    public ArrayList Emp_Mas_Emp_Id(string prefixText, int count) //, String contextKey
    {
        ArrayList ItemList = new ArrayList();
        DataTable dt = new DataTable();
        string str = "";
        //String sessionConnectionstring = ConfigurationManager.ConnectionStrings["LNKConnectionString"].ConnectionString;
        //string sessionConnectionstring = connectionString;//"data source=USER-PC;Initial Catalog=LNK;User Id=sa;password=123;Connection Timeout = 10;";
        ////contextKey;
        string constr = System.Configuration.ConfigurationManager.AppSettings["UbasysConnectionString"].ToString();

        if (prefixText == "*")
        {
            str = "select Emp_Mas_Emp_Id,Emp_Mas_First_Name,Emp_Mas_Last_Name from HrMs_Emp_mas where Emp_Mas_Term_Flg='N' order by Emp_Mas_Emp_Id ";
        }
        else
        {
            str = "select top 500 Emp_Mas_Emp_Id,Emp_Mas_First_Name,Emp_Mas_Last_Name from HrMs_Emp_mas where (Emp_Mas_Emp_Id like '%" + prefixText + "%'  or (Emp_Mas_First_Name+space(1)+Emp_Mas_First_Name like '%" + prefixText + "%')) and Emp_Mas_Term_Flg='N' order by Emp_Mas_Emp_Id ";
        }


        dt = DataProcess.GetData(constr, str);

        foreach (DataRow dr in dt.Rows)
        {
            ItemList.Add(dr["Emp_Mas_Emp_Id"].ToString() + ":" + dr["Emp_Mas_First_Name"].ToString() + "" + dr["Emp_Mas_Last_Name"].ToString());
        }

        return ItemList;
    }

    //[WebMethod]
    //public ArrayList SearchCRNumberUpd(string prefixText, int count)
    //{
    //    ArrayList ItemList = new ArrayList();
    //    Dataaccess.Connection Common = new Dataaccess.Connection();
    //    con = Common.init();
    //    string str = "select RefNo,(ClientCode +','+ClientName) as ClientName,ClientSL from CR_ExistingClientInformation where (RefNo like '%" + prefixText + "%'  or ClientName like '%" + prefixText + "%') Order by ClientName";

    //    cmd = new SqlCommand(str, con);
    //    dr = cmd.ExecuteReader();

    //    while (dr.Read())
    //    {
    //        ItemList.Add(dr["RefNo"].ToString() + "::" + dr["ClientName"].ToString() + "::" + dr["ClientSL"].ToString());
    //    }

    //    dr.Close();
    //    con.Close();
    //    return ItemList;
    //}
    #endregion Process Details

    [WebMethod]
    public List<string> GetDepartmentCode(string prefixText, int count, String contextKey)
    {
        List<string> ItemList = new List<string>();
        DataTable dt = new DataTable();
        string str = "";
        string constr = contextKey;
        if (prefixText == "*")
        {
            str = "select Distinct(Dept_Code) from Hrms_Dept_Master order by Dept_Code";
        }
        else
        {
            str = "select Distinct(Dept_Code) from Hrms_Dept_Master where ( Dept_Code like '%" + prefixText + "%') order by Dept_Code";
        }
        dt = DataProcess.GetData(constr, str);
        foreach (DataRow dr in dt.Rows)
        {
            ItemList.Add(dr["Dept_Code"].ToString());
        }
        return ItemList;
    }

    [WebMethod]
    public List<string> GetDepartmentName(string prefixText, int count, String contextKey)
    {
        List<string> ItemList = new List<string>();
        DataTable dt = new DataTable();
        string str = "";
        string constr = contextKey;
        if (prefixText == "*")
        {
            str = "select Distinct(Dept_Name) from Hrms_Dept_Master order by Dept_Name";
        }
        else
        {
            str = "select Distinct(Dept_Name) from Hrms_Dept_Master where ( Dept_Name like '%" + prefixText + "%') order by Dept_Name";
        }
        dt = DataProcess.GetData(constr, str);
        foreach (DataRow dr in dt.Rows)
        {
            ItemList.Add(dr["Dept_Name"].ToString());
        }
        return ItemList;
    }

    [WebMethod]
    public List<string> GetSectionCode(string prefixText, int count, String contextKey)
    {
        List<string> ItemList = new List<string>();
        DataTable dt = new DataTable();
        string str = "";
        string constr = contextKey;
        if (prefixText == "*")
        {
            str = "select Distinct(Sect_Code) from Hrms_Sect_Mas order by Sect_Code";
        }
        else
        {
            str = "select Distinct(Sect_Code) from Hrms_Sect_Mas where ( Sect_Code like '%" + prefixText + "%') order by Sect_Code";
        }
        dt = DataProcess.GetData(constr, str);
        foreach (DataRow dr in dt.Rows)
        {
            ItemList.Add(dr["Sect_Code"].ToString());
        }
        return ItemList;
    }

    [WebMethod]
    public List<string> GetSectionName(string prefixText, int count, String contextKey)
    {
        List<string> ItemList = new List<string>();
        DataTable dt = new DataTable();
        string str = "";
        string constr = contextKey;
        if (prefixText == "*")
        {
            str = "select Distinct(Sect_Name) from Hrms_Sect_Mas order by Sect_Name";
        }
        else
        {
            str = "select Distinct(Sect_Name) from Hrms_Sect_Mas where ( Sect_Name like '%" + prefixText + "%') order by Sect_Name";
        }
        dt = DataProcess.GetData(constr, str);
        foreach (DataRow dr in dt.Rows)
        {
            ItemList.Add(dr["Sect_Name"].ToString());
        }
        return ItemList;
    }
}
