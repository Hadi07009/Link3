using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using Link3FrameWork;

/// <summary>
/// Summary description for COACls
/// </summary>
public class COACls
{
    public COACls()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void GrpName(Label lbl1, Label lbl2, Label lbl3, Label lbl4, Label lbl5, Label lbl6, Label lbl7, Label lbl8, Label lbl9, Label lbl10)
    {
        DataTable dt = DataProcess.GetData(DBConnCls.ConnectionString, "Select * from AccCoaGroupSetup where Cost_Id like 'A%'");
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            i++;
            if (i == 1)
                lbl1.Text = dr["Cost_Name"].ToString();
            else if (i == 2)
                lbl2.Text = dr["Cost_Name"].ToString();
            else if (i == 3)
                lbl3.Text = dr["Cost_Name"].ToString();
            else if (i == 4)
                lbl4.Text = dr["Cost_Name"].ToString();
            else if (i == 5)
                lbl5.Text = dr["Cost_Name"].ToString();
            else if (i == 6)
                lbl6.Text = dr["Cost_Name"].ToString();
            else if (i == 7)
                lbl7.Text = dr["Cost_Name"].ToString();
            else if (i == 8)
                lbl8.Text = dr["Cost_Name"].ToString();
            else if (i == 9)
                lbl9.Text = dr["Cost_Name"].ToString();
            else if (i == 10)
                lbl10.Text = dr["Cost_Name"].ToString();
        }
    }

    public static void GroupTextName(TextBox txt, string searchStr)
    {
        
    }

    public static List<CoaAccGroupDAO> CoaGroupName(string coacode, TextBox txt1, TextBox txt2, TextBox txt3, bool li)
    {
        List<CoaAccGroupDAO> grp = new List<CoaAccGroupDAO>();


        if (li)
        {
            CoaAccGroupDAO cg = new CoaAccGroupDAO();
            cg.CoaCode = coacode;
            cg.CoaGroupID = "A0";
            cg.CoaGroupCode = Convert.ToString("1");
            grp.Add(cg);
        }

        if (Convert.ToString(txt1.Text) != "")
        {
            CoaAccGroupDAO cg = new CoaAccGroupDAO();
            cg.CoaCode = coacode;
            cg.CoaGroupID = "A1";
            string[] aa = txt1.Text.ToString().Split(':');
            cg.CoaGroupCode = Convert.ToString(aa[0]);
            cg.CoaGroupCodeName = Convert.ToString(aa[1]);
            grp.Add(cg);

        }
        if (Convert.ToString(txt2.Text) != "")
        {
            CoaAccGroupDAO cg = new CoaAccGroupDAO();
            cg.CoaCode = coacode;
            cg.CoaGroupID = "A2";
            string[] aa = txt2.Text.ToString().Split(':');
            cg.CoaGroupCode = Convert.ToString(aa[0]);
            cg.CoaGroupCodeName = Convert.ToString(aa[1]);
            grp.Add(cg);
        }
        if (Convert.ToString(txt3.Text) != "")
        {
            CoaAccGroupDAO cg = new CoaAccGroupDAO();
            cg.CoaCode = coacode;
            cg.CoaGroupID = "A3";
            string[] aa = txt3.Text.ToString().Split(':');
            cg.CoaGroupCode = Convert.ToString(aa[0]);
            cg.CoaGroupCodeName = Convert.ToString(aa[1]);
            grp.Add(cg);
        }
        
        return grp;
    }
    public static List<AccCoaAnalysis> CoaAnalysis(string coacode, TextBox txt1, TextBox txt2, TextBox txt3)
    {
        List<AccCoaAnalysis> grp = new List<AccCoaAnalysis>();


        if (Convert.ToString(txt1.Text) != "")
        {
            AccCoaAnalysis cg = new AccCoaAnalysis();
            cg.GlCoaCode = coacode;
            string[] aa = txt1.Text.ToString().Split(':');
            cg.COSTID = Convert.ToString(aa[0]);
            cg.COSTNAME = Convert.ToString(aa[2]);
            cg.LinNo = 1;
            grp.Add(cg);

        }
        if (Convert.ToString(txt2.Text) != "")
        {
            AccCoaAnalysis cg = new AccCoaAnalysis();
            cg.GlCoaCode = coacode;
            string[] aa = txt2.Text.ToString().Split(':');
            cg.COSTID = Convert.ToString(aa[0]);
            cg.COSTNAME = Convert.ToString(aa[2]);
            cg.LinNo = 2;
            grp.Add(cg);
        }
        if (Convert.ToString(txt3.Text) != "")
        {
            AccCoaAnalysis cg = new AccCoaAnalysis();
            cg.GlCoaCode = coacode;
            string[] aa = txt3.Text.ToString().Split(':');
            cg.COSTID = Convert.ToString(aa[0]);
            cg.COSTNAME = Convert.ToString(aa[2]);
            cg.LinNo = 3;
            grp.Add(cg);
        }

        return grp;
    }

    public static bool getGrpName(string grpID, string grpCode, TextBox txt)
    {
        try
        {
            DataTable dt = DataProcess.GetData(DBConnCls.ConnectionString, "select * from FA_COM_CCG where Ccg_Cost_Id='" + grpID + "' and Ccg_Code='" + grpCode + "'");
            if (dt.Rows.Count > 0)
            {
                txt.Text = grpCode + "::" + dt.Rows[0]["Ccg_Name"].ToString();
            }
            return true;
        }
        catch
        {
            System.Windows.Forms.MessageBox.Show("Please Try again", StringProcess.messageHead);
            return false;
        }
    }
}
