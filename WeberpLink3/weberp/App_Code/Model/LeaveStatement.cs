using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LeaveStatement
/// </summary>
public class LeaveStatement
{
    public LeaveStatement()
    {
        //
        // TODO: Add constructor logic here
        //
    }
   

    public string OfficeLocation { get; set; }  
    public DateTime FromDate { get; set; }
    public string EntryUser { get; set; }
    public string EmployeeCode { get; set; }  
  
}