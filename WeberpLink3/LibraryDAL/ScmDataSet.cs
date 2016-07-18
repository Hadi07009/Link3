

using System.Data;
using System.Data.SqlClient;
using LibraryDAL;
namespace LibraryDAL {
    
    
    public partial class ScmDataSet {
    }
}

namespace LibraryDAL.ScmDataSetTableAdapters {


    partial class tbl_loan_informationTableAdapter
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
