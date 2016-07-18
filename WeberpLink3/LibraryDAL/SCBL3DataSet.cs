
using System.Data;
using System.Data.SqlClient;
using LibraryDAL;



namespace LibraryDAL.SCBL3DataSetTableAdapters
{
    partial class FA_TE_WH_PAYTableAdapter
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
    partial class FA_TE_WD_PAYTableAdapter
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

    
    partial class tbl_payment_request_detTableAdapter
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
    partial class tbl_chq_voucherTableAdapter
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

namespace LibraryDAL.SCBL3DataSetTableAdapters
{

}
namespace LibraryDAL
{


    public partial class SCBL3DataSet
    {
    }
}

