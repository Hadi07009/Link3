
using System.Data;
using System.Data.SqlClient;
using LibraryDAL;
        

namespace LibraryDAL.dsScfTableAdapters {
    partial class InSu_Trn_SetTableAdapter
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

    partial class InTr_Sr_DetTableAdapter
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
    
    
    public partial class InTr_Sr_HdrTableAdapter {
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


    public partial class dsScf
    {
        partial class InTr_Sr_DetDataTable
        {
        }
    }
}
