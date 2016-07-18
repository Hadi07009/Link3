using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using LibraryDAL; 

public class OffdayProcess
    {
        public OffdayProcess() { }

        public string SaveOffdayData(List<OffdayProcessHeader> offphdrlst,SqlCommand myCommand)
        {

            SqlTransaction myTrans;
            SqlDataAdapter sqlDataAdapterObj = null;
            string retValue = "";

            try
            {                

                foreach (OffdayProcessHeader ofproc in offphdrlst)
                {
                    myCommand.CommandText = "exec [sp_offdaydatasave] '" + ofproc.ApplicantId + "','" + ofproc.ActIndate + "','" + ofproc.ActIntime + "','" + ofproc.ActOuttime + "','" + ofproc.Remarks + "'";
                    myCommand.CommandTimeout = 600;
                    myCommand.ExecuteNonQuery();
                }

                retValue = "Data Saved Successful";

                return retValue;

            }
            catch (Exception)
            {
                return retValue;
            }
        }

    }

