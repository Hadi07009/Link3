
using System.Data;
using System.Data.SqlClient;
using LibraryDAL;



namespace LibraryDAL.SCBLINTableAdapters
{
    partial class tbl_str_val_reportTableAdapter
    {
    }

    partial class InMa_Itm_LocTableAdapter
    {
    }

    partial class tbl_store_ledgerTableAdapter
    {
    }

    partial class tbl_str_reportTableAdapter
    {
    }

    partial class tbl_prod_ctlTableAdapter
    {
    }


    partial class InTr_Sr_HdrTableAdapter
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
}

namespace LibraryDAL
{


    public partial class SCBLIN
    {
    }
}
