using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using LibraryDAL;




namespace LibraryDAL
{
    public class ConfigureSheetDAL
    {
        public ConfigureSheetDAL() { }
                
        public DataTable ConfigSheetList()
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = null;
            SqlDataAdapter da = null;

            try
            {
                ConnectionManagerLNK.SetCommandConnection(ref cmd, ref da);
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.CommandText = "Config_List";
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.Fill(ds, "Config_List");
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                ConnectionManagerLNK.ReleaseCommandConnection(ref cmd, ref da);
            }

            return ds.Tables[0];
        }

        public DataTable Balancesheet_Assets_show()
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = null;
            SqlDataAdapter da = null;

            try
            {
                ConnectionManagerLNK.SetCommandConnection(ref cmd, ref da);
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.CommandText = "Balancesheet_Assets";
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.Fill(ds, "Balancesheet_Assets");
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                ConnectionManagerLNK.ReleaseCommandConnection(ref cmd, ref da);
            }

            return ds.Tables[0];
        }

        public DataTable LiabilitiesShow()
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = null;
            SqlDataAdapter da = null;

            try
            {
                ConnectionManagerLNK.SetCommandConnection(ref cmd, ref da);
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.CommandText = "Balancesheet_Liabilities";
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.Fill(ds, "Balancesheet_Liabilities");
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                ConnectionManagerLNK.ReleaseCommandConnection(ref cmd, ref da);
            }

            return ds.Tables[0];

        }

        public DataTable Complainlistshow(ConfigureSheetDTL datedtl)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = null;
            SqlDataAdapter da=null;

            try
            {
              ConnectionManager.SetCommandConnection(ref cmd, ref da);
              if(cmd.Connection.State==System.Data.ConnectionState.Open)
              {
                    cmd.CommandText = "complainlist";
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter param_datefrom = new SqlParameter("@datefrom", typeof(DateTime));
                    param_datefrom.Direction = ParameterDirection.Input;
                    param_datefrom.Value = datedtl.DateFrom;
                    cmd.Parameters.Add(param_datefrom);

                    SqlParameter param_dateto = new SqlParameter("@dateto", typeof(DateTime));
                    param_dateto.Direction = ParameterDirection.Input;
                    param_dateto.Value = datedtl.DateTo;
                    cmd.Parameters.Add(param_dateto);

                    da.Fill(ds, "complainlist");


              }
            }
            catch(Exception ex)
            {
                throw (ex);
            }
            finally
            {
                ConnectionManager.ReleaseCommandConnection(ref cmd,ref da);
            }

            return ds.Tables[0];
        
        }

        public DataTable PendingComplainlistshow(ConfigureSheetDTL datedtl)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = null;
            SqlDataAdapter da = null;

            try
            {
                ConnectionManager.SetCommandConnection(ref cmd, ref da);
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.CommandText = "Pendingcomplainlist";
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter param_datefrom = new SqlParameter("@datefrom", typeof(DateTime));
                    param_datefrom.Direction = ParameterDirection.Input;
                    param_datefrom.Value = datedtl.DateFrom;
                    cmd.Parameters.Add(param_datefrom);

                    SqlParameter param_dateto = new SqlParameter("@dateto", typeof(DateTime));
                    param_dateto.Direction = ParameterDirection.Input;
                    param_dateto.Value = datedtl.DateTo;
                    cmd.Parameters.Add(param_dateto);

                    da.Fill(ds, "complainlist");


                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                ConnectionManager.ReleaseCommandConnection(ref cmd, ref da);
            }

            return ds.Tables[0];

        }


        public DataTable SolvedComplainlistshow(ConfigureSheetDTL datedtl)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = null;
            SqlDataAdapter da = null;

            try
            {
                ConnectionManager.SetCommandConnection(ref cmd, ref da);
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.CommandText = "Solvedcomplainlist";
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter param_datefrom = new SqlParameter("@datefrom", typeof(DateTime));
                    param_datefrom.Direction = ParameterDirection.Input;
                    param_datefrom.Value = datedtl.DateFrom;
                    cmd.Parameters.Add(param_datefrom);

                    SqlParameter param_dateto = new SqlParameter("@dateto", typeof(DateTime));
                    param_dateto.Direction = ParameterDirection.Input;
                    param_dateto.Value = datedtl.DateTo;
                    cmd.Parameters.Add(param_dateto);

                    da.Fill(ds, "complainlist");


                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                ConnectionManager.ReleaseCommandConnection(ref cmd, ref da);
            }

            return ds.Tables[0];

        }



        public DataTable complaincategory()
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = null;
            SqlDataAdapter da = null;

            try
            {
                ConnectionManager.SetCommandConnection(ref cmd, ref da);
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.CommandText = "complaincategory";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //SqlParameter param_datefrom = new SqlParameter("@datefrom", typeof(DateTime));
                    //param_datefrom.Direction = ParameterDirection.Input;
                    //param_datefrom.Value = datedtl.DateFrom;
                    //cmd.Parameters.Add(param_datefrom);

                    //SqlParameter param_dateto = new SqlParameter("@dateto", typeof(DateTime));
                    //param_dateto.Direction = ParameterDirection.Input;
                    //param_dateto.Value = datedtl.DateTo;
                    //cmd.Parameters.Add(param_dateto);

                    da.Fill(ds, "complaincategory");


                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                ConnectionManager.ReleaseCommandConnection(ref cmd, ref da);
            }

            return ds.Tables[0];

        }

        public DataTable Complaincarriedoverlist(string qry)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = null;
            SqlDataAdapter da = null;

            try
            {
                ConnectionManager.SetCommandConnection(ref cmd, ref da);
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.CommandText = qry;
                    cmd.CommandType = CommandType.Text;                    
                   // da.Fill(ds, "mis_complain_Carriedover_list");                 
                    da.Fill(ds);                 

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                ConnectionManager.ReleaseCommandConnection(ref cmd, ref da);
            }

            return ds.Tables[0];

        }

        public DataTable CustomerOverdueList(string qry)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = null;
            SqlDataAdapter da = null;

            try
            {
                ConnectionManagerLNK.SetCommandConnection(ref cmd, ref da);
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.CommandText = qry;
                    cmd.CommandType = CommandType.Text;
                    // da.Fill(ds, "mis_complain_Carriedover_list");                 
                    da.Fill(ds);

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                ConnectionManagerLNK.ReleaseCommandConnection(ref cmd, ref da);
            }

            return ds.Tables[0];

        }

    }

}
