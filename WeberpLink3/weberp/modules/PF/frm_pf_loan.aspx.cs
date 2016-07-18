using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using LibraryDAL;
using LibraryPF;
using LibraryPF.dsTransactionTableAdapters;
using System.IO;
using System.Data.SqlClient;

public partial class frm_pf_loan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsStatic.CheckUserAuthentication(true);
        clsStatic.MsgConfirmBox(btnpost, "Are you sure to post ? ");
        if (!Page.IsPostBack)
        {

            tbldet.Visible = false ;
            tblloandet.Visible =false ;
            cldloandate.SelectedDate = DateTime.Now.AddDays(-1 * (DateTime.Now.Day - 1)).AddMonths(1).AddDays(-1);
            cldinsdate.SelectedDate = DateTime.Now.AddDays(-1 * (DateTime.Now.Day - 1)).AddMonths(2).AddDays(-1);
            buttonvisibility();            
            txtee_Auto.ContextKey = current.UserId;
           
        }
 
    }

    private void buttonvisibility()
    {

        if (gdSchedule.Rows.Count > 0)
        {
            btnexport.Visible = true;
            btnpost.Visible = true;
        }
        else
        {
            btnexport.Visible = false;
            btnpost.Visible = false;
        }
    }


    private void parameterpass(ParameterFields myParams, string pname, string value)
    {
        ParameterField param = new ParameterField();
        ParameterDiscreteValue dis1 = new ParameterDiscreteValue();

        param.ParameterFieldName = pname;
        dis1.Value = value;
        param.CurrentValues.Add(dis1);
        myParams.Add(param);
    }

    protected void btnpayschedule_Click(object sender, EventArgs e)
    {
      if((txtloanamount.Text=="")||(txtnoofinst.Text=="")||(txtinstrate.Text=="")) return;
      loan_cal();
      buttonvisibility();
    }
   
    protected void btnshow_Click(object sender, EventArgs e)
    {

        if (txtemployee.Text == "")
        {
            tbldet.Visible =false;
            tblloandet.Visible = false;
            lblmessage.Text = "";
            return;                
        }

        if (txtemployee.Text.Split(':').Length < 2)
        {
            tbldet.Visible = false;
            tblloandet.Visible = false;
            lblmessage.Text = "";
            txtemployee.Text = "";

            return;
        }



        hrms_pf_loan_hdrTableAdapter loan_hdr = new hrms_pf_loan_hdrTableAdapter();
        LibraryPF.dsTransaction.hrms_pf_loan_hdrDataTable dtloan_hdr = new dsTransaction.hrms_pf_loan_hdrDataTable();

        dtloan_hdr = loan_hdr.GetDataByRefStatus(txtemployee.Text.Split(':')[0].ToString(), "RUN");
        if (dtloan_hdr.Rows.Count > 0)
        {
            load_header_data();
            tbldet.Visible = true;
            tblloandet.Visible = false;
            lblmessage.Text = "Already loan taken by the Employee " + lblid.Text +":"+ lblname.Text;
            lblmessage.ForeColor = System.Drawing.Color.Red;
            return;

        }

        else

        {
            load_header_data();
            tbldet.Visible = true;
            tblloandet.Visible = true;
            clear_all();
        
        }
          

    }

    private void load_header_data()
    {

        LibraryPF.dsMasterDataTableAdapters.view_employee_infoTableAdapter emp = new LibraryPF.dsMasterDataTableAdapters.view_employee_infoTableAdapter();
        LibraryPF.dsMasterData.view_employee_infoDataTable dtemp = new LibraryPF.dsMasterData.view_employee_infoDataTable();
        sp_pf_contribution_openingTableAdapter opn = new sp_pf_contribution_openingTableAdapter();
        dsTransaction.sp_pf_contribution_openingDataTable dtopn = new dsTransaction.sp_pf_contribution_openingDataTable();

        lblmessage.Text = "";
        //  tbldet.Visible = true;
        // tblloandet.Visible = true;
        dtemp = emp.GetDataByEmpCode(txtemployee.Text.Split(':')[0]);
        if (dtemp.Rows.Count == 0)
        {
            lblmessage.Text = "Employee Not Found";
            lblmessage.ForeColor = System.Drawing.Color.Red;

            tbldet.Visible = false ;
            tblloandet.Visible =false ;

            return; //emp not found
        }

        dtopn = opn.GetData(dtemp[0].emp_code, DateTime.Now);
        if (dtopn.Rows.Count == 0)
        {
            lblmessage.Text = "Profit Not Found";
            lblmessage.ForeColor = System.Drawing.Color.Red;
            return; 
          // tbldet.Visible = true;
        }

        lblid.Text = dtemp[0].emp_code;
        lblname.Text = dtemp[0].emp_name;
        lbldesignation.Text = dtemp[0].emp_designation_name;
        lblorganization.Text = dtemp[0].emp_org_name;
        lblcompany.Text = dtemp[0].Isemp_company_idNull() ? "" : dtemp[0].emp_company_name;

        lbljoindate.Text = dtemp[0].emp_join_date.ToShortDateString();

        DateTime dum_join_date = dtemp[0].emp_join_date;
        int nom = 0;

        while (dum_join_date <= DateTime.Now.AddDays(1))
        {
            dum_join_date = dum_join_date.AddMonths(1);
            if (dum_join_date <= DateTime.Now.AddDays(1)) nom++;
        }

        lblservicelen.Text = nom.ToString();

        lblowncont.Text = dtopn[0].own_cont.ToString("N2");
        lblempcont.Text = dtopn[0].emp_cont.ToString("N2");
        lbltotcont.Text = (dtopn[0].own_cont + dtopn[0].emp_cont).ToString("N2");

        lblownprofit.Text = dtopn[0].own_profit.ToString("N2");
        lblempprofit.Text = dtopn[0].emp_profit.ToString("N2");
        lbltotprofit.Text = (dtopn[0].own_profit + dtopn[0].emp_profit).ToString("N2");

        lblowntot.Text = (dtopn[0].own_cont + dtopn[0].own_profit).ToString("N2");
        lblemptot.Text = (dtopn[0].emp_cont + dtopn[0].emp_profit).ToString("N2");
        lblgrandtot.Text = (dtopn[0].own_cont + dtopn[0].own_profit + dtopn[0].emp_cont + dtopn[0].emp_profit).ToString("N2");
        txtemployee.Text = "";
    }

    private void clear_all()
    {

        txtloanamount.Text = "";
        txtnoofinst.Text = "";
        txtinstrate.Text = "";
        gdSchedule.DataSource =null;
        gdSchedule.DataBind();

        buttonvisibility();

    }

    private void loan_cal()
    {
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
        double loanAmount = Convert.ToDouble(txtloanamount.Text);             //Loan Amount
        double iRate = Convert.ToDouble(txtinstrate.Text);                    //Interest Rate
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
            tRow["Ins No"] = intPmt.ToString();
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

        tblAmort.Rows.Add("", "", "Total", totpay.ToString("N2"), totprincipal.ToString("N2"), totintrest.ToString("N2"), "");
        gdSchedule.DataSource = tblAmort;
        gdSchedule.DataBind();

    }

    protected double calcMonthly(double principalAmt, double noOfPayment, double iRate)
    {
        double monthly;
        double intRate = (iRate / 100) / 12;
        monthly = (principalAmt * (Math.Pow((1 + intRate), noOfPayment)) * intRate / (Math.Pow((1 + intRate), noOfPayment) - 1));
        return Convert.ToDouble(monthly);
    }

    protected void btnpfstatement_Click(object sender, EventArgs e)
    {
        LibraryDTO.clsReport rpt = new LibraryDTO.clsReport();
        ParameterFields myParams = new ParameterFields();
        parameterpass(myParams, "@st_date", Convert.ToDateTime(lbljoindate.Text).ToShortDateString());
        parameterpass(myParams, "@end_date", DateTime.Now.ToShortDateString());
        parameterpass(myParams, "@emp_code", lblid.Text);
        parameterpass(myParams, "period", "Period : " + Convert.ToDateTime(lbljoindate.Text).ToShortDateString() + " To " + DateTime.Now.ToShortDateString());
        parameterpass(myParams, "CompanyName", current.CompanyName);
        parameterpass(myParams, "CompanyAddress", current.CompanyAddress);
        rpt.ParametersFields = myParams;
        rpt.PageZoomFactor = 100;
        rpt.FileName = "reports/rpt_pf_statement.rpt";
        current.SessionReport = rpt;
        RegisterStartupScript("click", "<script>window.open('./frm_rpt_viewer.aspx');</script>");

    }
    protected void btnpost_Click(object sender, EventArgs e)
    {
        hrms_pf_loan_hdrTableAdapter loan_hdr = new hrms_pf_loan_hdrTableAdapter();
        LibraryPF.dsTransaction.hrms_pf_loan_hdrDataTable dtloan_hdr = new dsTransaction.hrms_pf_loan_hdrDataTable();
        hrms_pf_loan_detTableAdapter loan_det = new hrms_pf_loan_detTableAdapter();
        LibraryPF.dsTransaction.hrms_pf_loan_detDataTable dtloan_det = new dsTransaction.hrms_pf_loan_detDataTable();

        int max_sl = Convert.ToInt32(loan_hdr.GetMaxRefNo())+1;
        string ref_no = "LN"+ "-" + string.Format("{0:000000}", max_sl);

        SqlTransaction myTrans = HelperTA.OpenTransaction(loan_hdr.Connection);
        try
        {
            loan_hdr.AttachTransaction(myTrans);
            loan_det.AttachTransaction(myTrans);

            for (int i = 0; i < gdSchedule.Rows.Count - 1;i++ )
            {
                loan_det.InsertLoanDet(ref_no, Convert.ToInt32(gdSchedule.Rows[i].Cells[0].Text), Convert.ToDateTime(gdSchedule.Rows[i].Cells[1].Text), Convert.ToDecimal(gdSchedule.Rows[i].Cells[2].Text), Convert.ToDecimal(gdSchedule.Rows[i].Cells[3].Text), Convert.ToDecimal(gdSchedule.Rows[i].Cells[4].Text), Convert.ToDecimal(gdSchedule.Rows[i].Cells[5].Text), Convert.ToDecimal(gdSchedule.Rows[i].Cells[6].Text),"PENDING","NO", DateTime.Now);
            }
            loan_hdr.InsertLoanHdr(ref_no, lblid.Text, lblcompany.Text, "RUN", "NO", Convert.ToDecimal(txtloanamount.Text.Trim()), 0, 0, Convert.ToDecimal(txtinstrate.Text.Trim()), Convert.ToInt32(txtnoofinst.Text.Trim()), cldloandate.SelectedDate, cldinsdate.SelectedDate, System.DateTime.Now);

            myTrans.Commit();
        }

        catch (Exception ex)
        {
            myTrans.Rollback();           
        }

        finally
        {

            HelperTA.CloseTransaction(loan_hdr.Connection, myTrans);
        }

        Response.Redirect(Request.Url.AbsoluteUri);

    }
    protected void btnexport_Click(object sender, EventArgs e)
    {

        clsStatic.Export("export.xls", gdSchedule);
    }
   
}