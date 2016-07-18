using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    /// <summary>
    /// Summary description for EmployeeInformationController
    /// </summary>
    public class EmployeeInformationController
    {
        private string _msg;

        public EmployeeInformationController()
        {

        }

        public string GetDepartmentCodeOfEmployee(string connectionString, string employeeCode)
        {
            string departmentCode = null;
            var storedProcedureComandRead = "exec [EmpBasicInformationGetFromHrms_Trans_Det] '" + employeeCode + "'";
            var dtBasicInfoFromTrans = StoredProcedureExecutor.StoredProcedureExecuteReader(connectionString, storedProcedureComandRead);
            if (dtBasicInfoFromTrans.Rows.Count > 0)
            {
                departmentCode = dtBasicInfoFromTrans.Rows[0].ItemArray[1].ToString();
            }
            return departmentCode;
        }

        public DataTable GetEmployeeDetails(string connectionString, string employeeCode)
        {
            var storedProcedureCommandText = "exec [Transfer_PromotionGetEmployeeDetails] '" + employeeCode + "'";
            var dtBasicInfoFrom = StoredProcedureExecutor.StoredProcedureExecuteReader(connectionString, storedProcedureCommandText);
            return dtBasicInfoFrom;
        }
        public void SaveOthersDocument(string connectionString, EmployeeInformation objEmployeeInformation)
        {
            var storedProcedureComandTest = "exec [EmpBasicInformationInitiate_HRMS_Emp_OthersDocument] '" +
                                        objEmployeeInformation.EmployeeCode + "','" +
                                        objEmployeeInformation.DocumentCode + "','" +
                                        objEmployeeInformation.ActivityCode + "'";
            StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);
        }
        public void DeleteOthersDocument(string connectionString, EmployeeInformation objemployeeEnformatin)
        {
            var storedProcedureComandTest = "exec [EmpBasicInformationDelete_HRMS_Emp_OthersDocument] '" + objemployeeEnformatin.DocumentCode + "'";
            StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);
        }
    }
}