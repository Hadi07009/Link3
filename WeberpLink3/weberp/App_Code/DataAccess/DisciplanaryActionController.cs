/// <summary>
/// Summary description for DisciplanaryActionController
/// </summary>
public class DisciplanaryActionController
{
	public DisciplanaryActionController()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void Delete(string connectionString, DisciplanaryAction objDisciplanaryAction)
    {
        DataProcess.DeleteQuery(connectionString, Sqlgenerate.SqlDeleteDisciplanaryAction(objDisciplanaryAction.CaseCode,objDisciplanaryAction.EmployeeCode));
    }
    public void Save(string connectionString, DisciplanaryAction objDisciplanaryAction)
    {
        var storedProcedureComandTest = "exec [DisciplanaryActionInitiate_HRMSDisciplanaryAction] '" +
                                        objDisciplanaryAction.CaseCode + "','" +
                                        objDisciplanaryAction.EmployeeCode + "','" +
                                        objDisciplanaryAction.CaseTopic + "','" +
                                        objDisciplanaryAction.CaseDate + "','" +
                                        objDisciplanaryAction.Inquary + "','" +
                                        objDisciplanaryAction.InquaryRecomondation + "','" +
                                        objDisciplanaryAction.CaseAction + "','" +
                                        objDisciplanaryAction.Remarks + "','" +
                                        objDisciplanaryAction.EntryUser + "'";

        StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);
    }

    public void SaveLogData(string connectionString, DisciplanaryAction objDisciplanaryAction)
    {
        var storedProcedureComandTest = "exec [DisciplanaryActionInitiateLog_HRMSDisciplanaryActionLog] '" +
                                        objDisciplanaryAction.CaseCode + "','" +
                                        objDisciplanaryAction.EmployeeCode + "','" +
                                        objDisciplanaryAction.CaseTopic + "','" +
                                        objDisciplanaryAction.CaseDate + "','" +
                                        objDisciplanaryAction.Inquary + "','" +
                                        objDisciplanaryAction.InquaryRecomondation + "','" +
                                        objDisciplanaryAction.CaseAction + "','" +
                                        objDisciplanaryAction.Remarks + "','" +
                                        objDisciplanaryAction.EntryUser + "','" +
                                        objDisciplanaryAction.Status + "'";

        StoredProcedureExecutor.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);
    }
}