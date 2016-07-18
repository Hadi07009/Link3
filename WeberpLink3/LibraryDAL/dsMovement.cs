
using System.Data;
using System.Data.SqlClient;
using LibraryDAL;

namespace LibraryDAL.dsMovementTableAdapters {
   

    partial class FA_TE_MOVTableAdapter
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

    partial class TrTr_MO_ExtTableAdapter
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

    partial class TrTr_MO_DetTableAdapter
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
    
    
    public partial class TrTr_MO_HdrTableAdapter {

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


    public partial class dsMovement
    {
        partial class TrTr_Tran_RecDataTable
        {
        }
    
        partial class TrTr_MO_ExtDataTable
        {
        }
    }
}