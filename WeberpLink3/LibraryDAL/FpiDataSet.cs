using System.Data;
using System.Data.SqlClient;
using LibraryDAL.FpiDataSetTableAdapters;


namespace LibraryDAL.FpiDataSetTableAdapters
{



    partial class tbl_fpi_lc_infoTableAdapter
    {
        public void AttachTransaction(SqlTransaction SqlTrn)
        {
            this.Connection = SqlTrn.Connection;
            foreach (SqlCommand cmd in this.CommandCollection)
            {
                cmd.Transaction = SqlTrn;
            }
        }
    }

    partial class tbl_fpi_custom_clearingTableAdapter
    {
        public void AttachTransaction(SqlTransaction SqlTrn)
        {
            this.Connection = SqlTrn.Connection;
            foreach (SqlCommand cmd in this.CommandCollection)
            {
                cmd.Transaction = SqlTrn;
            }
        }
    }

    partial class tbl_fpi_cargo_landing_detTableAdapter
    {
        public void AttachTransaction(SqlTransaction SqlTrn)
        {
            this.Connection = SqlTrn.Connection;
            foreach (SqlCommand cmd in this.CommandCollection)
            {
                cmd.Transaction = SqlTrn;
            }
        }
    }

    partial class tbl_fpi_cargo_landing_hdrTableAdapter
    {

        public void AttachTransaction(SqlTransaction SqlTrn)
        {
            this.Connection = SqlTrn.Connection;
            foreach (SqlCommand cmd in this.CommandCollection)
            {
                cmd.Transaction = SqlTrn;
            }
        }
    }

    partial class tbl_fpi_approval_dataTableAdapter
    {
        public void AttachTransaction(SqlTransaction SqlTrn)
        {
            this.Connection = SqlTrn.Connection;
            foreach (SqlCommand cmd in this.CommandCollection)
            {
                cmd.Transaction = SqlTrn;
            }
        }

    }


    partial class tbl_fpi_loan_detailsTableAdapter
    {
        public void AttachTransaction(SqlTransaction SqlTrn)
        {
            this.Connection = SqlTrn.Connection;
            foreach (SqlCommand cmd in this.CommandCollection)
            {
                cmd.Transaction = SqlTrn;
            }
        }
    }





}

namespace LibraryDAL
{


    public partial class FpiDataSet
    {
        partial class tbl_fpi_anal_mapDataTable
        {
        }
    
        partial class tbl_fpi_approval_data_oldDataTable
        {
        }
    }





}
