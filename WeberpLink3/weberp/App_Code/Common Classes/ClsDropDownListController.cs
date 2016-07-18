using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsDropDownListController
/// </summary>
public class ClsDropDownListController
{
	public ClsDropDownListController()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static void LoadDropDownList( string connectionString , string sqlQueryString ,DropDownList dropDownListName, string displayMember, string valueMember)
    {
        DataSet ds = new DataSet();
        SqlConnection connection = new SqlConnection(connectionString);
        try
        {
            SqlDataAdapter adapter = new SqlDataAdapter(sqlQueryString, connection);
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            dropDownListName.Items.Clear();
            if (dt.Rows.Count > 0)
            {
                dropDownListName.Items.Insert(0, new ListItem("--- Please Select ---", "-1"));
                foreach (DataRow dr in dt.Rows)
                {
                    ListItem lst = new ListItem();
                    lst.Value = dr[valueMember].ToString();
                    lst.Text = dr[displayMember].ToString();
                    dropDownListName.Items.Add(lst);
                }
            }
            else
            {
                dropDownListName.Items.Insert(0, new ListItem("--- No Data Found ---", "-1"));
            }
        }
        catch (SqlException ex)
        {
            throw new Exception("Error occurred during access the server !");
        }
    }
    public static void LoadDropDownListWithConcatenation(string connectionString, string sqlQueryString, DropDownList dropDownListName, string displayMember, string valueMember)
    {
        DataSet ds = new DataSet();
        SqlConnection connection = new SqlConnection(connectionString);
        try
        {
            SqlDataAdapter adapter = new SqlDataAdapter(sqlQueryString, connection);
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            dropDownListName.Items.Clear();
            dropDownListName.Items.Insert(0, new ListItem("--- Please Select ---", "-1"));
            foreach (DataRow dr in dt.Rows)
            {
                ListItem lst = new ListItem();
                lst.Value = dr[valueMember].ToString();
                lst.Text = dr[valueMember].ToString() + ":" + dr[displayMember].ToString();
                dropDownListName.Items.Add(lst);
            }
        }
        catch (SqlException ex)
        {
            throw new Exception("Error occurred during access the server !");
        }
    }
    public static void LoadCheckBoxList(string connectionString, string sqlQueryString, CheckBoxList checkBoxName, string displayMember, string valueMember)
    {
        DataSet ds = new DataSet();
        SqlConnection connection = new SqlConnection(connectionString);
        try
        {
            SqlDataAdapter adapter = new SqlDataAdapter(sqlQueryString, connection);
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            checkBoxName.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                ListItem lst = new ListItem();
                lst.Value = dr[valueMember].ToString();
                lst.Text = dr[valueMember].ToString() + ":" + dr[displayMember].ToString();
                checkBoxName.Items.Add(lst);
            }
        }
        catch (SqlException ex)
        {
            throw new Exception("Error occurred during access the server !");
        }
    }

    #region For Status
    public static List<GetStatus> ActiveOrInactive()
    {
        return new List<GetStatus> {
            new GetStatus{StatusValue="1",StatusName="Active"}
            ,new GetStatus{StatusValue="0",StatusName="Inactive"}
            };
    }

    public static void LoadddlStatus(DropDownList dropDownListName)
    {
        dropDownListName.DataSource = ActiveOrInactive().ToList();
        dropDownListName.DataTextField = "StatusName";
        dropDownListName.DataValueField = "StatusValue";
        dropDownListName.DataBind();
    }

    public class GetStatus
    {
        public string StatusValue { get; set; }
        public string StatusName { get; set; }
    }
    #endregion For Status

    public static void LoadDropDownListFromStoredProcedure(string connectionString, string storedProcedureCommandTest, DropDownList dropDownListName, string displayMember, string valueMember)
    {
        var myConnection = new SqlConnection(connectionString);
        myConnection.Open();
        var myCommand = myConnection.CreateCommand();
        var resultantDataTable = new DataTable();
        myCommand.CommandText = storedProcedureCommandTest;
        myCommand.ExecuteNonQuery();
        var resultantDataAdapter = new SqlDataAdapter(myCommand);
        resultantDataAdapter.Fill(resultantDataTable);
        myConnection.Close();

        dropDownListName.Items.Clear();
        if (resultantDataTable.Rows.Count > 0)
        {
            dropDownListName.Items.Insert(0, new ListItem("--- Please Select ---", "-1"));
            foreach (DataRow dr in resultantDataTable.Rows)
            {
                ListItem lst = new ListItem();
                lst.Value = dr[valueMember].ToString();
                lst.Text = dr[displayMember].ToString();
                dropDownListName.Items.Add(lst);
            }
        }
        else
        {
            dropDownListName.Items.Insert(0, new ListItem("--- No Data Found ---", "-1"));
        }
    }

    public static void EnableDisableDropDownList(DropDownList dropDownListName)
    {
        int itemCount = dropDownListName.Items.Count;
        if (itemCount == 2)
        {
            dropDownListName.SelectedIndex = 1;
            dropDownListName.Enabled = false;
        }
        else
        {
            dropDownListName.SelectedIndex = 0;
            dropDownListName.Enabled = true;
        }
    }

    public static void LoadCheckBoxListWithOutConcate(string connectionString, string sqlQueryString, CheckBoxList checkBoxName, string displayMember, string valueMember)
    {
        DataSet ds = new DataSet();
        SqlConnection connection = new SqlConnection(connectionString);
        try
        {
            SqlDataAdapter adapter = new SqlDataAdapter(sqlQueryString, connection);
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            checkBoxName.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                ListItem lst = new ListItem();
                lst.Value = dr[valueMember].ToString();
                lst.Text = dr[displayMember].ToString();
                checkBoxName.Items.Add(lst);
            }
        }
        catch (SqlException ex)
        {
            throw new Exception("Error occurred during access the server !");
        }
    }

    public static void LoadDropDownListUsingStoredProcedureWithConcatenation(string connectionString, string storedProcedureCommandTest, DropDownList dropDownListName, string displayMember, string valueMember)
    {
        var myConnection = new SqlConnection(connectionString);
        myConnection.Open();
        var myCommand = myConnection.CreateCommand();
        var resultantDataTable = new DataTable();
        myCommand.CommandText = storedProcedureCommandTest;
        myCommand.ExecuteNonQuery();
        var resultantDataAdapter = new SqlDataAdapter(myCommand);
        resultantDataAdapter.Fill(resultantDataTable);
        myConnection.Close();
        dropDownListName.Items.Clear();
        if (resultantDataTable.Rows.Count > 0)
        {
            dropDownListName.Items.Insert(0, new ListItem("--- Please Select ---", "-1"));
            foreach (DataRow dr in resultantDataTable.Rows)
            {
                ListItem lst = new ListItem();
                lst.Value = dr[valueMember].ToString();
                lst.Text = dr[valueMember].ToString() + ":" + dr[displayMember].ToString();
                dropDownListName.Items.Add(lst);
            }
        }
        else
        {
            dropDownListName.Items.Insert(0, new ListItem("--- No Data Found ---", "-1"));
        }
    }

}