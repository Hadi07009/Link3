
using System.Data;
using System.Data.SqlClient;
using LibraryDAL;



namespace LibraryDAL.SCBL2DataSetTableAdapters
{

    partial class tbl_foreign_insp_hdrTableAdapter
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

    partial class tbl_foreign_insp_detTableAdapter
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


    partial class tbl_spo_advance_hdrTableAdapter
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
    partial class tbl_spo_advance_detTableAdapter
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

    partial class PuTr_PO_Det_Scbl2TableAdapter
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

    partial class PuTr_IN_Det_Scbl2TableAdapter
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

    partial class tbl_mat_rec_retTableAdapter
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





    public partial class tbl_foreign_insp_hdrTableAdapter
    {
    }
}
namespace LibraryDAL
{


    public partial class SCBL2DataSet
    {
        partial class tbl_file_detailDataTable
        {
        }

        partial class tbl_mat_rec_ret2DataTable
        {
        }

        partial class tbl_fpi_preship_detDataTable
        {
        }
    }
}
