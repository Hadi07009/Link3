using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for clsDbCon
/// </summary>
/// 

public class clsDbCon
{
    string ConnectionString;
    SqlDataReader reader;
    SqlConnection conn = new SqlConnection();
    SqlCommand cmd = new SqlCommand();

    public void ExecuteSQLStmt(string sql)
    {

        // Open the connection
        if (conn.State == ConnectionState.Open)
        {
            conn.Close();
        }

        ConnectionString = ConfigurationSettings.AppSettings["UbasysConnectionString"].ToString();
        conn.ConnectionString = ConnectionString;
        conn.Open();
        cmd = new SqlCommand(sql, conn);

        try
        {
            cmd.ExecuteNonQuery();            
        }
        catch (SqlException ex)
        {
            Console.WriteLine(ex.StackTrace + "Source==" + ex.Source + "===End Message==" + ex.Message);
        }
    }//ExecuteSQLStmt

    public void ExecuteL3TSQLStmt(string sql)
    {

        // Open the connection
        if (conn.State == ConnectionState.Open)
        {
            conn.Close();
        }

        ConnectionString = ConfigurationSettings.AppSettings["UbasysConnectionString"].ToString();
        conn.ConnectionString = ConnectionString;
        conn.Open();
        cmd = new SqlCommand(sql, conn);

        try
        {
            cmd.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            Console.WriteLine(ex.StackTrace + "Source==" + ex.Source + "===End Message==" + ex.Message);
        }
    }//ExecuteSQLStmt

    //public void getItemData2(getItmRange itm)
    //{
    //    string fst = itm.FirstItem;
    //    string lst = itm.LastItem;
    //}

    public getItmRange getItemData()
    {
        getItmRange itmRange = new getItmRange();

        // Open the connection
        if (conn.State == ConnectionState.Open)
        {
            conn.Close();
        }

        ConnectionString = ConfigurationSettings.AppSettings["UbasysConnectionString"].ToString();
        conn.ConnectionString = ConnectionString;
        conn.Open();    

        // Create DataAdapter object for update and other operations
        SqlDataAdapter thisAdapter = new SqlDataAdapter("SELECT distinct top 10000 Itm_Det_ICode, Itm_Det_Desc FROM InMa_Itm_Det WHERE Itm_Det_Trn_Pst ='Y' ORDER BY Itm_Det_ICode", conn);

        // Create CommandBuilder object to build SQL commands
        SqlCommandBuilder thisBuilder = new SqlCommandBuilder(thisAdapter);

        // Create DataSet to contain related data tables, rows, and columns
        DataSet thisDataSet = new DataSet();

        // Fill DataSet using query defined previously for DataAdapter
        thisAdapter.Fill(thisDataSet, "InMa_Itm_Det");

        // Show data before change
        if (thisDataSet.Tables["InMa_Itm_Det"].Rows.Count > 0)
        {
            itmRange.FirstItem = thisDataSet.Tables["InMa_Itm_Det"].Rows[0]["Itm_Det_ICode"].ToString();
            itmRange.LastItem = thisDataSet.Tables["InMa_Itm_Det"].Rows[thisDataSet.Tables["InMa_Itm_Det"].Rows.Count-1]["Itm_Det_ICode"].ToString();
        }
        
        return itmRange;
    }//getItemData

    public getPartyRange getPartyData()
    {
        getPartyRange prtyRange = new getPartyRange();

        // Open the connection
        if (conn.State == ConnectionState.Open)
        {
            conn.Close();
        }

        ConnectionString = ConfigurationSettings.AppSettings["UbasysConnectionString"].ToString();
        conn.ConnectionString = ConnectionString;
        conn.Open();

        // Create DataAdapter object for update and other operations
        SqlDataAdapter thisAdapter = new SqlDataAdapter("select par_adr_code,par_adr_name,par_adr_line_1 from puma_par_adr_view order by par_adr_code", conn);

        // Create CommandBuilder object to build SQL commands
        SqlCommandBuilder thisBuilder = new SqlCommandBuilder(thisAdapter);

        // Create DataSet to contain related data tables, rows, and columns
        DataSet thisDataSet = new DataSet();

        // Fill DataSet using query defined previously for DataAdapter
        thisAdapter.Fill(thisDataSet, "puma_par_adr_view");

        // Show data before change
        if (thisDataSet.Tables["puma_par_adr_view"].Rows.Count > 0)
        {
            prtyRange.FirstParty = thisDataSet.Tables["puma_par_adr_view"].Rows[0]["par_adr_code"].ToString();
            prtyRange.LastParty = thisDataSet.Tables["puma_par_adr_view"].Rows[thisDataSet.Tables["puma_par_adr_view"].Rows.Count - 1]["par_adr_code"].ToString();
        }

        return prtyRange;
    }//getPartyData

    public string getStore(CheckBoxList chkList)
    {
        string StrSelect = "";
        string crptStrSelect = "";

        if (chkList.Items.Count > 0)
        {
            for (int i = 0; i < chkList.Items.Count; i++)
            {
                if ((chkList.Items[i].Selected == true) & (StrSelect.Length != 0))
                {
                    StrSelect = StrSelect + "," + "'" + chkList.Items[i].Text.ToString() + "'";
                    crptStrSelect = crptStrSelect + "," + chkList.Items[i].Text.ToString();
                }
                else
                {
                    if ((chkList.Items[i].Selected == true) & !(StrSelect.Length != 0))
                    {
                        StrSelect = StrSelect + "'" + chkList.Items[i].Text.ToString() + "'";
                        crptStrSelect = crptStrSelect + chkList.Items[i].Text.ToString();
                    }
                }
            }
        }
        return StrSelect;
    }
}

public class getItmRange
{
    public getItmRange() { }

    private string firstItem;
    private string lastItem;

    public String FirstItem
    {
        get { return firstItem; }
        set { firstItem = value; }
    }

    public String LastItem
    {
        get { return lastItem; }
        set { lastItem = value; }
    }
}

public class getPartyRange
{
    public getPartyRange() { }

    private string firstParty;
    private string lastParty;

    public String FirstParty
    {
        get { return firstParty; }
        set { firstParty = value; }
    }

    public String LastParty
    {
        get { return lastParty; }
        set { lastParty = value; }
    }
}