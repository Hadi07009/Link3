using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_HRMS_SelfService_TestStringFormat : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnTestString_Click(object sender, EventArgs e)
    {
        try
        {
            StringGenerator objStringGenerator = new StringGenerator();
            string inputString1 = txtinputString1.Text == string.Empty ? null : txtinputString1.Text;
            string inputString2 = txtinputString2.Text == string.Empty ? null : txtinputString2.Text;
            string inputString3 = txtinputString3.Text == string.Empty ? null : txtinputString3.Text;
            string inputString4 = txtinputString4.Text == string.Empty ? null : txtinputString4.Text;
            string inputString5 = txtinputString5.Text == string.Empty ? null : txtinputString5.Text;
            string inputString6 = txtinputString6.Text == string.Empty ? null : txtinputString6.Text;
            lblFinalString.Text = string.Empty;
            lblFinalString.Text = objStringGenerator.CreatingCommaSeparatedlist(inputString1, inputString2, inputString3, inputString4, inputString5, inputString6);

        }
        catch (Exception msgException)
        {

            MessageBox1.ShowError(msgException.Message);
        }
    }
}