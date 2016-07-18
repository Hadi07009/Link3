using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using LibraryPF;
using LibraryPF.dsMasterDataTableAdapters;
using LibraryPF.dsTransactionTableAdapters;
using System.Data.SqlClient;

public partial class frm_loan_reschedule : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.MsgConfirmBox(btnSave, "Are you sure to save ? ");
        clsStatic.CheckUserAuthentication(true);
        if (!Page.IsPostBack)
        {
            tbldet.Visible = false;
            buttonvisibility();
            cldinsdate.SelectedDate = DateTime.Now.AddDays(-1 * (DateTime.Now.Day - 1)).AddMonths(2).AddDays(-1);
           
            txtserviceBill_AutoCompleteemployee.ContextKey = current.UserId;
          
        }
    }

  
    protected void btnshow_Click(object sender, EventArgs e)
    {
        view_employee_infoTableAdapter emp = new view_employee_infoTableAdapter();
        LibraryPF.dsMasterData.view_employee_infoDataTable dtemp = new LibraryPF.dsMasterData.view_employee_infoDataTable();

        hrms_pf_loan_hdrTableAdapter loanhdr = new hrms_pf_loan_hdrTableAdapter();
        dsTransaction.hrms_pf_loan_hdrDataTable dthdr = new dsTransaction.hrms_pf_loan_hdrDataTable();

        hrms_pf_loan_detTableAdapter loandet = new hrms_pf_loan_detTableAdapter();
        dsTransaction.hrms_pf_loan_detDataTable dtdet = new dsTransaction.hrms_pf_loan_detDataTable();


        string emp_code, loan_ref;

        if (txtemployee.Text.Split(':').Length < 3)
        {
            tbldet.Visible = false;
            txtemployee.Text = "";

            return;
        }
        emp_code = txtemployee.Text.Split(':')[0];
        loan_ref = txtemployee.Text.Split(':')[1];

        dtemp = emp.GetDataByEmpCode(emp_code);
        if (dtemp.Rows.Count == 0)
        {
            tbldet.Visible = false ;
            return; //emp not found
        }

        dthdr = loanhdr.GetDataByRef(loan_ref);
        if (dthdr.Rows.Count == 0)
        {
            tbldet.Visible = false ;
            return; //loan hdr not found
        }

        dtdet = loandet.GetDataByRefStatus(loan_ref,"PENDING","DONE","");
        if (dtdet.Rows.Count == 0) return; //loan det not found


        tbldet.Visible = true;

        lblid.Text = dtemp[0].emp_code;
        lblname.Text = dtemp[0].emp_name;
        lbldesignation.Text = dtemp[0].emp_designation_name;
        lblorganization.Text = dtemp[0].emp_org_name;
        lblcompany.Text = dtemp[0].Isemp_company_nameNull() ? "" : dtemp[0].emp_company_name;

        lbljoindate.Text = dtemp[0].emp_join_date.ToShortDateString();

        DateTime dum_join_date = dtemp[0].emp_join_date;
        int nom = 0;

        while (dum_join_date <= DateTime.Now.AddDays(1))
        {
            dum_join_date = dum_join_date.AddMonths(1);
            if (dum_join_date <= DateTime.Now.AddDays(1)) nom++;
        }

        lblservicelen.Text = nom.ToString();

        //loan hdr
        lblloan_ref.Text = dthdr[0].loan_ref_no;
        lblloanamount.Text = dthdr[0].loan_amount.ToString("N2");
        lblinstrate.Text = dthdr[0].interest_rate.ToString("N2");
        lblnoofinst.Text = dthdr[0].no_of_inst.ToString();
        lblloandate.Text = dthdr[0].loan_app_date.ToShortDateString();
        lblloanstatus.Text = dthdr[0].loan_status;
        lblinsrate2.Text = dthdr[0].interest_rate.ToString("N2"); 
        //loan det

        DataTable dt = new DataTable();

        dt.Columns.Add("Ins No", typeof(string));
        dt.Columns.Add("Ins Date", typeof(string));
        dt.Columns.Add("Op Balance", typeof(string));
        dt.Columns.Add("Ins Amount", typeof(string));
        dt.Columns.Add("Principal Amount", typeof(string));
        dt.Columns.Add("Interest Amount", typeof(string));        
        dt.Columns.Add("Cl Balance", typeof(string));
        dt.Columns.Add("Ins Status", typeof(string));

        foreach (dsTransaction.hrms_pf_loan_detRow dr in dtdet.Rows)
        {
            dt.Rows.Add(dr.install_no.ToString(), dr.install_date.ToShortDateString(), dr.op_bal.ToString("N2"), dr.schedule_payment.ToString("N2"), dr.principal_amount.ToString("N2"), dr.interest_amount.ToString("N2"), dr.closing_bal.ToString("N2"), dr.install_status);
        }

        gdSchedule.DataSource = dt;
        gdSchedule.DataBind();
        

        lblclosingbal.Text = Convert.ToDecimal(loandet.GetBalanceByStatus(loan_ref, "PENDING")).ToString("N2");
        lblnoofinsgiven.Text = loandet.GetDataByRefStatus(loan_ref, "DONE","DONE","DONE").Rows.Count.ToString();

        txtemployee.Text = "";
        clear_all();
        
    }

    private void clear_all()
    {

        txtnoofinst.Text = "";
        gdnewSchedule.DataSource = null;
        gdnewSchedule.DataBind();
        buttonvisibility();
        lblMessage.Text = "";
    }

   
    protected double calcMonthly(double principalAmt, double noOfPayment, double iRate)
    {
        double monthly;
        double intRate = (iRate / 100) / 12;
        monthly = (principalAmt * (Math.Pow((1 + intRate), noOfPayment)) * intRate / (Math.Pow((1 + intRate), noOfPayment) - 1));
        return Convert.ToDouble(monthly);
    }


    private string  check_entry()
    {
        if (lblclosingbal.Text == "0.00") return "Closing balance 0";
        if (txtnoofinst.Text == "" || txtnoofinst.Text == (0).ToString()) return "Enter No of Intallment greater than 0";
        return "";
    }

   
    protected void btn_viewschedule_Click(object sender, EventArgs e)
    {
       if( check_entry()!="")
        {
            lblMessage.Text = check_entry();
            lblMessage.ForeColor = System.Drawing.Color.Red;
            return;
        }

       lblMessage.Text = "";
        
        DateTime insdate = cldinsdate.SelectedDate.AddDays(-1 * (cldinsdate.SelectedDate.Day - 1));
        double decDeductBalance;
        double interestPaid;
        double decNewBalance;
        double dblTotalPayments;
        double dblInterestToDecimal;
        double opbal;
        double totpay = 0, totintrest = 0, totprincipal = 0;
        //double dblPrincipal=;
        int intPmt = 1;
        double loanAmount = Convert.ToDouble(lblclosingbal.Text);             //Loan Amount
        double iRate = Convert.ToDouble(lblinsrate2.Text);                    //Interest Rate
        int noPayment = Convert.ToInt32(txtnoofinst.Text);                  //No. of payment
        opbal = loanAmount;
        double dblMonthlyPayment = calcMonthly(loanAmount, noPayment, iRate);

        //convert interest rate to decimal form
        dblInterestToDecimal = iRate / 100;
        //calculate interest
        double dblConvertInterest = dblInterestToDecimal / 12;

        //calculate the total number of payments (n * 12)
        int intYears = noPayment;     //years
        //int intNumOfPayments = intYears * 12;    //In Years (with Month wise)
        int intNumOfPayments = intYears;  // Only number of installments basis like (2 Installment or 3 Installment)

        dblTotalPayments = intNumOfPayments * dblMonthlyPayment; //total amount
        decNewBalance = loanAmount; //principle amount

        DataTable tblAmort = new DataTable();

        tblAmort.Columns.Add("Ins No", typeof(string));
        tblAmort.Columns.Add("Ins Date", typeof(string));
        tblAmort.Columns.Add("OP Bal", typeof(string));
        tblAmort.Columns.Add("Payment", typeof(string));
        tblAmort.Columns.Add("Principal", typeof(string));
        tblAmort.Columns.Add("Interest", typeof(string));
        tblAmort.Columns.Add("CL Bal", typeof(string));

        DataRow tRow;

        while (intPmt <= intNumOfPayments)
        {
            tRow = tblAmort.NewRow();
            interestPaid = decNewBalance * dblConvertInterest;
            decDeductBalance = dblMonthlyPayment - interestPaid;
            decNewBalance = decNewBalance - decDeductBalance;



            tblAmort.Rows.Add(tRow);
            tRow["Ins No"] = (intPmt+Convert.ToInt32(lblnoofinsgiven.Text)).ToString();
            tRow["Ins Date"] = insdate.AddMonths(intPmt).AddDays(-1).ToShortDateString();
            tRow["OP Bal"] = String.Format("{0:n2}", opbal);
            tRow["Payment"] = String.Format("{0:n2}", dblMonthlyPayment);           
            tRow["Principal"] = String.Format("{0:n2}", decDeductBalance);
            tRow["Interest"] = String.Format("{0:n2}", interestPaid);
            tRow["CL Bal"] = String.Format("{0:n2}", decNewBalance);

            totpay += dblMonthlyPayment;
            totintrest += interestPaid;
            totprincipal += decDeductBalance;

            opbal = opbal - decDeductBalance;
            intPmt += 1;
        }

        tblAmort.Rows.Add("", "", "Total", totpay.ToString("N2"), totprincipal.ToString("N2"), totintrest.ToString("N2"),"");

        gdnewSchedule.DataSource = tblAmort;
        gdnewSchedule.DataBind();
        buttonvisibility();

    }

    private void buttonvisibility()
    {

        if (gdnewSchedule.Rows.Count > 0)
        {
           
            btnSave.Visible = true;
        }
        else
        {
            btnSave.Visible = false;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        hrms_pf_loan_detTableAdapter loandet = new hrms_pf_loan_detTableAdapter();
        dsTransaction.hrms_pf_loan_detDataTable dtdet = new dsTransaction.hrms_pf_loan_detDataTable();

        if (gdnewSchedule.Rows.Count == 0) return;

        string ref_no =lblloan_ref.Text.Trim();

        SqlTransaction myTrans = HelperTA.OpenTransaction(loandet.Connection);

       try

       {
           loandet.AttachTransaction(myTrans);
           loandet.UpdateDetStatus("REJECTED", ref_no, "PENDING");

           for (int i = 0; i < gdnewSchedule.Rows.Count-1; i++)
           {
               loandet.InsertLoanDet(ref_no, Convert.ToInt32(gdnewSchedule.Rows[i].Cells[0].Text), Convert.ToDateTime(gdnewSchedule.Rows[i].Cells[1].Text), Convert.ToDecimal(gdnewSchedule.Rows[i].Cells[2].Text), Convert.ToDecimal(gdnewSchedule.Rows[i].Cells[3].Text), Convert.ToDecimal(gdnewSchedule.Rows[i].Cells[4].Text), Convert.ToDecimal(gdnewSchedule.Rows[i].Cells[5].Text), Convert.ToDecimal(gdnewSchedule.Rows[i].Cells[6].Text), "PENDING", "NO", DateTime.Now);
           }

           myTrans.Commit();
       }

        catch(Exception ex)
       {
          myTrans.Rollback();       
       }

       finally
       {
          HelperTA.CloseTransaction(loandet.Connection, myTrans);
       }
       Response.Redirect(Request.Url.AbsoluteUri);
      

    }
    protected void gdnewSchedule_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    
}