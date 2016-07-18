
using System.Data;
using System.Data.SqlClient;
using LibraryDAL;

namespace LibraryDAL.dsTransportTableAdapters
{


    public partial class DtTripBillTableAdapter
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
    
    
    public partial class dsTransport {
        partial class TrTr_Trip_Ext_pendingDataTable
        {
        }
    }
}

