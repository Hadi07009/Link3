using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;

/// <summary>
/// Summary description for CommonMethods
/// </summary>
public class CommonMethods
{
	public CommonMethods()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static void LoadDepartmentCode(string connectionString,string officeLocationCode,DropDownList dropDownList)
    {
        ClsDropDownListController.LoadDropDownList(connectionString, Sqlgenerate.SqlGetDepartmentCodeByOfficeLocation(officeLocationCode), dropDownList, "Dept_Name", "Dept_Code");
    }
    public static DataTable GetActiveEmployeeCode(string connectionString)
    {
        return DataProcess.GetData(connectionString, Sqlgenerate.SqlGetEmployeeCode());
    }
    public static DataTable GetActiveEmployeeCode(string connectionString, string officeLocation)
    {
        return DataProcess.GetData(connectionString, Sqlgenerate.SqlGetEmployeeCode(officeLocation));
    }
    public DataTable GetDataFromExcel(string connectionString, string sheetName)
    {

        var connExcel = new OleDbConnection(connectionString);
        var cmdExcel = new OleDbCommand();
        var oda = new OleDbDataAdapter();
        var dt = new DataTable();
        cmdExcel.Connection = connExcel;
        connExcel.Open();
        cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();
        return dt;
    }
    public DataTable GetDataFromExcel(string connectionString, string sheetName, string employeeCode)
    {

        var connExcel = new OleDbConnection(connectionString);
        var cmdExcel = new OleDbCommand();
        var oda = new OleDbDataAdapter();
        var dt = new DataTable();
        cmdExcel.Connection = connExcel;
        connExcel.Open();
        cmdExcel.CommandText = "SELECT * From [" + sheetName + "] where ID = " + employeeCode + "";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();
        return dt;
    }
    public string TimeFormatGenerate(string atf)
    {
        try
        {
            string rtf = "";
            int h = Convert.ToInt32(atf.Split(':')[0].ToString());
            int m = Convert.ToInt32(atf.Split(':')[1].ToString());
            if (h > 12)
            {
                h = h - 12;
            }
            string hh = string.Format("{0:00}", h);
            string mm = string.Format("{0:00}", m);
            string ampm = atf.Split(':')[2].ToString();
            rtf = hh + ":" + mm + " " + ampm;
            return rtf;

        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }
    public DataTable LoadDepartmentIdByuserCode(string connectionString, string userCode, string companyCode, string nodeCode)
    {
        try
        {
            DataTable dt = new DataTable();
            string strSql = "";
            strSql = "  SELECT distinct DeptID, Dept FROM Emp_Details"
                          + " INNER JOIN Hrms_Emp_Mas on Emp_Details.EmpId = Hrms_Emp_Mas .Emp_Mas_Emp_Id and Emp_Mas_Term_Flg = 'N'"
                          + " Inner join tblUserCompanyDepartment on DepartmentID=Deptid"
                          + " where [UserID]='" + userCode + "' and [NodeID]='" + nodeCode + "' and [CompanyID]='" + companyCode + "'"
                          + " ORDER BY Dept  ASC";
            dt = DataProcess.GetData(connectionString, strSql);
            if (dt.Rows.Count == 0)
            {
                strSql = "SELECT distinct DeptID, Dept FROM Emp_Details INNER JOIN Hrms_Emp_Mas on Emp_Details.EmpId = Hrms_Emp_Mas .Emp_Mas_Emp_Id and Emp_Mas_Term_Flg = 'N' ORDER BY Dept  ASC";
                dt = DataProcess.GetData(connectionString, strSql);
            }
            return dt;

        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }

}