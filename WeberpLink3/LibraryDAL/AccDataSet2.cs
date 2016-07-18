

using System.Data;
using System.Data.SqlClient;
using LibraryDAL;


namespace LibraryDAL.AccDataSet2TableAdapters
{
    partial class AccTransactionHeaderHoldTableAdapter
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

    partial class AccTransactionDetailsHoldTableAdapter
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

    partial class AccTransactionAnalysisHoldTableAdapter
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

namespace LibraryDAL {
    
    
    public partial class AccDataSet2 {
    }
}
