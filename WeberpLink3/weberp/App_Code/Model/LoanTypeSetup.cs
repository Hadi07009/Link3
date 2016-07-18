using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LoanTypeSetup
/// </summary>
public class LoanTypeSetup
{
	public LoanTypeSetup()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string loanCode;

    public string LoanCode
    {
        get { return loanCode; }
        set { loanCode = value; }
    }
    private string loanTitle;

    public string LoanTitle
    {
        get { return loanTitle; }
        set { loanTitle = value; }
    }
}