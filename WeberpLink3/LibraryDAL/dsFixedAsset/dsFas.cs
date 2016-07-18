using System.Data.SqlClient;




namespace LibraryDAL.dsFixedAsset
{
    public partial class dsFas
    {
        partial class FAS_InTr_Trn_DetDataTable
        {
        }
    }
}



namespace LibraryDAL.dsFixedAsset.dsFasTableAdapters
{


    partial class FAS_InTr_Trn_HdrDataTable
    {
    }
    partial class FAS_InTr_Trn_HdrTableAdapter
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

    #region InTr_Trn_DetTableAdapter
    public partial class FAS_InTr_Trn_DetTableAdapter
    {
    }

    partial class FAS_InTr_Trn_DetTableAdapter
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
    #endregion

    #region InMa_Itm_SerialTableAdapter
    public partial class FAS_InMa_Itm_SerialTableAdapter
    {
    }

    partial class FAS_InMa_Itm_SerialTableAdapter
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
    #endregion


    #region AccTransactionHeaderHold
    public partial class AccTransactionHeaderHoldTableAdapter
    {
    }

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
    #endregion

    #region AccTransactionDetailsHold
    public partial class AccTransactionDetailsHoldTableAdapter
    {
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
    #endregion


}


