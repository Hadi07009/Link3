
using System.Data;
using System.Data.SqlClient;
using LibraryDAL;
namespace LibraryDAL
{
}

namespace LibraryDAL
{
}

namespace LibraryDAL
{
}

namespace LibraryDAL
{
}

namespace LibraryDAL
{
}

namespace LibraryDAL.ProdDataSetTableAdapters
{
    partial class tbl_prod_itm_mapTableAdapter
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


    partial class tbl_prod_cost_sheetTableAdapter
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
    
    partial class tbl_prod_ctl_detTableAdapter
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

    partial class tbl_prod_entryTableAdapter
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


    public partial class ProdDataSet
    {
        partial class tbl_prod_entryDataTable
        {
        }
    }
}
