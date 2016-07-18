

using System.Data;
using System.Data.SqlClient;
using LibraryDAL;


namespace LibraryDAL {
    
    
    public partial class dsMas {
       
    }
}

namespace LibraryDAL.dsMasTableAdapters {
    partial class tbl_papers_infoTableAdapter
    {
        public void AttachTransaction(SqlTransaction SqlTrn)
        {
            this._connection = SqlTrn.Connection;
            foreach (SqlCommand cmd in this.CommandCollection)
            {
                cmd.Transaction = SqlTrn;
            }
        }
    }

    

    partial class Puma_Tran_Mas_fuel_formuTableAdapter
    {
        public void AttachTransaction(SqlTransaction SqlTrn)
        {
            this._connection = SqlTrn.Connection;
            foreach (SqlCommand cmd in this.CommandCollection)
            {
                cmd.Transaction = SqlTrn;
            }
        }
    }
    
    
    public partial class PuMa_Tran_MasTableAdapter {
        public void AttachTransaction(SqlTransaction SqlTrn)
        {
            this._connection = SqlTrn.Connection;
            foreach (SqlCommand cmd in this.CommandCollection)
            {
                cmd.Transaction = SqlTrn;
            }
        }
    }
}
