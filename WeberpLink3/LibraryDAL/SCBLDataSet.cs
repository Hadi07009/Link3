
using System.Data;
using System.Data.SqlClient;
using LibraryDAL;


namespace LibraryDAL.SCBLDataSetTableAdapters
{


    partial class App_Type_DetTableAdapter
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

    partial class quotation_detTableAdapter
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

    partial class quotation_logTableAdapter
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


    partial class User_Role_DefinitionTableAdapter
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



    partial class PuTr_PO_Det_ScblTableAdapter
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
    partial class PuTr_PO_Hdr_ScblTableAdapter
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

    partial class App_Flow_DefinitionTableAdapter
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

    partial class tbl_CommentsTableAdapter
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

    partial class tbl_app_ruleTableAdapter
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

    partial class tbl_tac_logTableAdapter
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

    partial class PuTr_PO_Hdr_ReviseTableAdapter
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

    partial class PuTr_PO_Det_ReviseTableAdapter
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

    partial class PuTr_IN_Hdr_ScblTableAdapter
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

    partial class PuTr_IN_Det_ScblTableAdapter
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

    partial class tbl_item_budgetTableAdapter
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


    partial class SCBLDataSet
    {
        partial class PuTr_IN_HdrDataTable
        {
        }
    
        partial class tbl_item_budgetDataTable
        {
        }

        partial class PuTr_IN_Det_ScblDataTable
        {
        }

        partial class tbl_CommentsDataTable
        {
        }

        partial class App_Flow_DefinitionDataTable
        {
        }

        partial class PuTr_PO_Det_ScblDataTable
        {
        }

        partial class PuTr_PO_Hdr_ScblDataTable
        {
        }

        partial class track_info_line1DataTable
        {
        }

        partial class App_flow_detDataTable
        {
        }

        partial class quotation_detDataTable
        {
        }

        partial class User_Role_DefinitionDataTable
        {
        }



        partial class track_info_det1DataTable
        {
        }


        partial class App_Type_DetDataTable
        {
        }

        partial class InTr_Sr_Hdr1DataTable
        {
        }

        partial class InTr_Sr_Det1DataTable
        {
        }

        partial class InMa_Str_LocDataTable
        {
        }

        partial class InMa_Itm_DetDataTable
        {
        }
    }
}



namespace LibraryDAL.SCBLDataSetTableAdapters
{


    public partial class quotation_logTableAdapter
    {
    }
}
