
using System.Data;
using System.Data.SqlClient;
using LibraryDAL;

namespace LibraryDAL.dsRtdTableAdapters
{


    public partial class TrTr_Mo_AdvTableAdapter
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
    
    
    public partial class dsRtd {
    }
}
