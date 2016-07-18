using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using LibraryDAL;
using LibraryDAL.SCBLDataSetTableAdapters;

public partial class frm_app_rule: System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";
        clsStatic.MsgConfirmBox(btnsavedet, "Are you sure to save ?");

        if (!Page.IsPostBack)
        {
            tblheader.Visible = false;
            tbldet.Visible = false;
            load_all_app_type();
            load_all_trn_type();
            load_all_flow();
        }
        else
        {

        }
        
    }

    private void load_all_app_type()
    {
        ddlapptype.Items.Clear();
        ddlapptype.Items.Add("");
        ddlapptype.Items.Add("SR");
        ddlapptype.Items.Add("MPR");
        ddlapptype.Items.Add("CS");
        ddlapptype.Items.Add("SPOAPP");
        ddlapptype.Items.Add("ADAPP");
        ddlapptype.Items.Add("FPOAPP");
        ddlapptype.Items.Add("REVISE");
        ddlapptype.Items.Add("CANCEL");
        ddlapptype.Items.Add("CLOSE");
        ddlapptype.Items.Add("PIAPP");        
        ddlapptype.Items.Add("PAYREQ");

    }
    private void load_all_flow()
    {
        App_Flow_HdrTableAdapter flow = new App_Flow_HdrTableAdapter();
        SCBLDataSet.App_Flow_HdrDataTable dt = new SCBLDataSet.App_Flow_HdrDataTable();
        ListItem lst;

        ddlflow1.Items.Clear();
        ddlflow2.Items.Clear();
        ddlflow3.Items.Clear();
        ddlflow4.Items.Clear();
        ddlflow5.Items.Clear();

        ddlflow1.Items.Add("");
        ddlflow2.Items.Add("");
        ddlflow3.Items.Add("");
        ddlflow4.Items.Add("");
        ddlflow5.Items.Add("");

        ddlappflow.Items.Clear();
        ddlappflow.Items.Add("");


        dt = flow.GetAllFlow();

        foreach (SCBLDataSet.App_Flow_HdrRow dr in dt.Rows)
        {
            lst = new ListItem();

            lst.Value = dr.flow_id.ToString();
            lst.Text = dr.flow_id.ToString() + ": " + dr.flow_det.ToString();
            ddlflow1.Items.Add(lst);
            ddlflow2.Items.Add(lst);
            ddlflow3.Items.Add(lst);
            ddlflow4.Items.Add(lst);
            ddlflow5.Items.Add(lst);
            ddlappflow.Items.Add(lst);
        }


    }
    private void load_all_trn_type()    
    {
        ListItem lst;
        App_Type_DetTableAdapter app = new App_Type_DetTableAdapter();
        SCBLDataSet.App_Type_DetDataTable appdt = new SCBLDataSet.App_Type_DetDataTable();
        tbl_trn_detTableAdapter trn = new tbl_trn_detTableAdapter();
        SCBLDataSet.tbl_trn_detDataTable dt = new SCBLDataSet.tbl_trn_detDataTable();



        dt = trn.GetAllCodeByType("IN");

        ddltype.Items.Clear();
        ddltype.Items.Add("");

        foreach (SCBLDataSet.tbl_trn_detRow dr in dt.Rows)
        {
            lst = new ListItem();
            lst.Value = dr.trn_code.ToString();
            lst.Text = dr.trn_code.ToString() +": " + dr.trn_det.ToString(); ;
            ddltype.Items.Add(lst);
        }


        appdt = app.GetAllApp();

        ddlapp1.Items.Clear();
        ddlapp1.Items.Add("");
                
        ddlapp2.Items.Clear();
        ddlapp2.Items.Add("");

        ddlapp3.Items.Clear();
        ddlapp3.Items.Add("");

        ddlapp4.Items.Clear();
        ddlapp4.Items.Add("");

        ddlapp5.Items.Clear();
        ddlapp5.Items.Add("");

        ddlapp6.Items.Clear();
        ddlapp6.Items.Add("");

        ddlapp7.Items.Clear();
        ddlapp7.Items.Add("");

        ddlapp8.Items.Clear();
        ddlapp8.Items.Add("");


        ddlfor11.Items.Clear();
        ddlfor11.Items.Add("");

        ddlfor12.Items.Clear();
        ddlfor12.Items.Add("");

        ddlfor13.Items.Clear();
        ddlfor13.Items.Add("");

        ddlfor14.Items.Clear();
        ddlfor14.Items.Add("");

        ddlfor15.Items.Clear();
        ddlfor15.Items.Add("");

        ddlfor16.Items.Clear();
        ddlfor16.Items.Add("");

        ddlfor17.Items.Clear();
        ddlfor17.Items.Add("");

        ddlfor18.Items.Clear();
        ddlfor18.Items.Add("");

        ddlfor21.Items.Clear();
        ddlfor21.Items.Add("");

        ddlfor22.Items.Clear();
        ddlfor22.Items.Add("");

        ddlfor23.Items.Clear();
        ddlfor23.Items.Add("");

        ddlfor24.Items.Clear();
        ddlfor24.Items.Add("");

        ddlfor25.Items.Clear();
        ddlfor25.Items.Add("");

        ddlfor26.Items.Clear();
        ddlfor26.Items.Add("");

        ddlfor27.Items.Clear();
        ddlfor27.Items.Add("");
        
        ddlfor28.Items.Clear();
        ddlfor28.Items.Add("");
        
        ddlfor31.Items.Clear();
        ddlfor31.Items.Add("");

        ddlfor32.Items.Clear();
        ddlfor32.Items.Add("");

        ddlfor33.Items.Clear();
        ddlfor33.Items.Add("");

        ddlfor34.Items.Clear();
        ddlfor34.Items.Add("");

        ddlfor35.Items.Clear();
        ddlfor35.Items.Add("");

        ddlfor36.Items.Clear();
        ddlfor36.Items.Add("");

        ddlfor37.Items.Clear();
        ddlfor37.Items.Add("");

        ddlfor38.Items.Clear();
        ddlfor38.Items.Add("");

        ddlfor41.Items.Clear();
        ddlfor41.Items.Add("");

        ddlfor42.Items.Clear();
        ddlfor42.Items.Add("");

        ddlfor43.Items.Clear();
        ddlfor43.Items.Add("");

        ddlfor44.Items.Clear();
        ddlfor44.Items.Add("");

        ddlfor45.Items.Clear();
        ddlfor45.Items.Add("");

        ddlfor46.Items.Clear();
        ddlfor46.Items.Add("");

        ddlfor47.Items.Clear();
        ddlfor47.Items.Add("");

        ddlfor48.Items.Clear();
        ddlfor48.Items.Add("");

        ddlfor51.Items.Clear();
        ddlfor51.Items.Add("");

        ddlfor52.Items.Clear();
        ddlfor52.Items.Add("");

        ddlfor53.Items.Clear();
        ddlfor53.Items.Add("");

        ddlfor54.Items.Clear();
        ddlfor54.Items.Add("");

        ddlfor55.Items.Clear();
        ddlfor55.Items.Add("");

        ddlfor56.Items.Clear();
        ddlfor56.Items.Add("");

        ddlfor57.Items.Clear();
        ddlfor57.Items.Add("");

        ddlfor58.Items.Clear();
        ddlfor58.Items.Add("");

        foreach (SCBLDataSet.App_Type_DetRow dr in appdt.Rows)
        {
            lst = new ListItem();
            lst.Value = dr.app_name.ToString();
            lst.Text = dr.app_name.ToString();

            ddlapp1.Items.Add(lst); ddlapp2.Items.Add(lst); ddlapp3.Items.Add(lst); ddlapp4.Items.Add(lst); ddlapp5.Items.Add(lst); ddlapp6.Items.Add(lst); ddlapp7.Items.Add(lst); ddlapp8.Items.Add(lst);

            ddlfor11.Items.Add(lst); ddlfor12.Items.Add(lst); ddlfor13.Items.Add(lst); ddlfor14.Items.Add(lst); ddlfor15.Items.Add(lst); ddlfor16.Items.Add(lst); ddlfor17.Items.Add(lst); ddlfor18.Items.Add(lst);
            ddlfor21.Items.Add(lst); ddlfor22.Items.Add(lst); ddlfor23.Items.Add(lst); ddlfor24.Items.Add(lst); ddlfor25.Items.Add(lst); ddlfor26.Items.Add(lst); ddlfor27.Items.Add(lst); ddlfor28.Items.Add(lst);
            ddlfor31.Items.Add(lst); ddlfor32.Items.Add(lst); ddlfor33.Items.Add(lst); ddlfor34.Items.Add(lst); ddlfor35.Items.Add(lst); ddlfor36.Items.Add(lst); ddlfor37.Items.Add(lst); ddlfor38.Items.Add(lst);
            ddlfor41.Items.Add(lst); ddlfor42.Items.Add(lst); ddlfor43.Items.Add(lst); ddlfor44.Items.Add(lst); ddlfor45.Items.Add(lst); ddlfor46.Items.Add(lst); ddlfor47.Items.Add(lst); ddlfor48.Items.Add(lst);
            ddlfor51.Items.Add(lst); ddlfor52.Items.Add(lst); ddlfor53.Items.Add(lst); ddlfor54.Items.Add(lst); ddlfor55.Items.Add(lst); ddlfor56.Items.Add(lst); ddlfor57.Items.Add(lst); ddlfor58.Items.Add(lst);

        }

    }

    
   
    private void generatedata(string flow_id)
    {
        tbl_app_ruleTableAdapter rul = new tbl_app_ruleTableAdapter();
        SCBLDataSet.tbl_app_ruleDataTable dt = new SCBLDataSet.tbl_app_ruleDataTable();

        int indx = 0;
        dt = rul.GetDataByType(flow_id);

        foreach (SCBLDataSet.tbl_app_ruleRow dr in dt.Rows)
        {
            indx++;

            switch (indx)
            {
                case 1:
                    ddlapp1.Text= dr.app_id.ToString();
                    ddlfor11.Text = dr.for_1.ToString();
                    ddlfor21.Text = dr.for_2.ToString();
                    ddlfor31.Text = dr.for_3.ToString();
                    ddlfor41.Text = dr.for_4.ToString();
                    ddlfor51.Text = dr.for_5.ToString();

                    if (dr.edit_per == 1) chk1.Items[0].Selected = true; else chk1.Items[0].Selected = false;
                    if (dr.app_per == 1) chk1.Items[1].Selected = true; else chk1.Items[1].Selected = false;
                    if (dr.rej_per == 1) chk1.Items[2].Selected = true; else chk1.Items[2].Selected = false;
                    
                    break;

                case 2:
                    ddlapp2.Text = dr.app_id.ToString();
                    ddlfor12.Text = dr.for_1.ToString();
                    ddlfor22.Text = dr.for_2.ToString();
                    ddlfor32.Text = dr.for_3.ToString();
                    ddlfor42.Text = dr.for_4.ToString();
                    ddlfor52.Text = dr.for_5.ToString();

                    if (dr.edit_per == 1) chk2.Items[0].Selected = true; else chk2.Items[0].Selected = false;
                    if (dr.app_per == 1) chk2.Items[1].Selected = true; else chk2.Items[1].Selected = false;
                    if (dr.rej_per == 1) chk2.Items[2].Selected = true; else chk2.Items[2].Selected = false;

                    break;

                case 3:
                    ddlapp3.Text = dr.app_id.ToString();
                    ddlfor13.Text = dr.for_1.ToString();
                    ddlfor23.Text = dr.for_2.ToString();
                    ddlfor33.Text = dr.for_3.ToString();
                    ddlfor43.Text = dr.for_4.ToString();
                    ddlfor53.Text = dr.for_5.ToString();

                    if (dr.edit_per == 1) chk3.Items[0].Selected = true; else chk3.Items[0].Selected = false;
                    if (dr.app_per == 1) chk3.Items[1].Selected = true; else chk3.Items[1].Selected = false;
                    if (dr.rej_per == 1) chk3.Items[2].Selected = true; else chk3.Items[2].Selected = false;

                    break;

                case 4:
                    ddlapp4.Text = dr.app_id.ToString();
                    ddlfor14.Text = dr.for_1.ToString();
                    ddlfor24.Text = dr.for_2.ToString();
                    ddlfor34.Text = dr.for_3.ToString();
                    ddlfor44.Text = dr.for_4.ToString();
                    ddlfor54.Text = dr.for_5.ToString();

                    if (dr.edit_per == 1) chk4.Items[0].Selected = true; else chk4.Items[0].Selected = false;
                    if (dr.app_per == 1) chk4.Items[1].Selected = true; else chk4.Items[1].Selected = false;
                    if (dr.rej_per == 1) chk4.Items[2].Selected = true; else chk4.Items[2].Selected = false;

                    break;

                case 5:
                    ddlapp5.Text = dr.app_id.ToString();
                    ddlfor15.Text = dr.for_1.ToString();
                    ddlfor25.Text = dr.for_2.ToString();
                    ddlfor35.Text = dr.for_3.ToString();
                    ddlfor45.Text = dr.for_4.ToString();
                    ddlfor55.Text = dr.for_5.ToString();

                    if (dr.edit_per == 1) chk5.Items[0].Selected = true; else chk5.Items[0].Selected = false;
                    if (dr.app_per == 1) chk5.Items[1].Selected = true; else chk5.Items[1].Selected = false;
                    if (dr.rej_per == 1) chk5.Items[2].Selected = true; else chk5.Items[2].Selected = false;

                    break;

                case 6:
                    ddlapp6.Text = dr.app_id.ToString();
                    ddlfor16.Text = dr.for_1.ToString();
                    ddlfor26.Text = dr.for_2.ToString();
                    ddlfor36.Text = dr.for_3.ToString();
                    ddlfor46.Text = dr.for_4.ToString();
                    ddlfor56.Text = dr.for_5.ToString();

                    if (dr.edit_per == 1) chk6.Items[0].Selected = true; else chk6.Items[0].Selected = false;
                    if (dr.app_per == 1) chk6.Items[1].Selected = true; else chk6.Items[1].Selected = false;
                    if (dr.rej_per == 1) chk6.Items[2].Selected = true; else chk6.Items[2].Selected = false;

                    break;

                case 7:
                    ddlapp7.Text = dr.app_id.ToString();
                    ddlfor17.Text = dr.for_1.ToString();
                    ddlfor27.Text = dr.for_2.ToString();
                    ddlfor37.Text = dr.for_3.ToString();
                    ddlfor47.Text = dr.for_4.ToString();
                    ddlfor57.Text = dr.for_5.ToString();

                    if (dr.edit_per == 1) chk77.Items[0].Selected = true; else chk77.Items[0].Selected = false;
                    if (dr.app_per == 1) chk77.Items[1].Selected = true; else chk77.Items[1].Selected = false;
                    if (dr.rej_per == 1) chk77.Items[2].Selected = true; else chk77.Items[2].Selected = false;

                    break;

                case 8:
                    ddlapp8.Text = dr.app_id.ToString();
                    ddlfor18.Text = dr.for_1.ToString();
                    ddlfor28.Text = dr.for_2.ToString();
                    ddlfor38.Text = dr.for_3.ToString();
                    ddlfor48.Text = dr.for_4.ToString();
                    ddlfor58.Text = dr.for_5.ToString();

                    if (dr.edit_per == 1) chk88.Items[0].Selected = true; else chk88.Items[0].Selected = false;
                    if (dr.app_per == 1) chk88.Items[1].Selected = true; else chk88.Items[1].Selected = false;
                    if (dr.rej_per == 1) chk88.Items[2].Selected = true; else chk88.Items[2].Selected = false;

                    break;
            }
        }
    }

    private void reset_tbl()
    {
        int i;

        for (i = 0; i < 3; i++)
        {
            chk1.Items[i].Selected = false;
            chk2.Items[i].Selected = false;
            chk3.Items[i].Selected = false;
            chk4.Items[i].Selected = false;
            chk5.Items[i].Selected = false;
            chk6.Items[i].Selected = false;
            chk77.Items[i].Selected = false;
            chk88.Items[i].Selected = false;
        }


        ddlapp1.Text = ""; ddlapp2.Text = ""; ddlapp3.Text = ""; ddlapp4.Text = ""; ddlapp5.Text = ""; ddlapp6.Text = ""; ddlapp7.Text = ""; ddlapp8.Text = "";

        ddlfor11.Text = ""; ddlfor12.Text = ""; ddlfor13.Text = ""; ddlfor14.Text = ""; ddlfor15.Text = ""; ddlfor16.Text = ""; ddlfor17.Text = ""; ddlfor18.Text = "";
        ddlfor21.Text = ""; ddlfor22.Text = ""; ddlfor23.Text = ""; ddlfor24.Text = ""; ddlfor25.Text = ""; ddlfor26.Text = ""; ddlfor27.Text = ""; ddlfor28.Text = "";
        ddlfor31.Text = ""; ddlfor32.Text = ""; ddlfor33.Text = ""; ddlfor34.Text = ""; ddlfor35.Text = ""; ddlfor36.Text = ""; ddlfor37.Text = ""; ddlfor38.Text = "";
        ddlfor41.Text = ""; ddlfor42.Text = ""; ddlfor43.Text = ""; ddlfor44.Text = ""; ddlfor45.Text = ""; ddlfor46.Text = ""; ddlfor47.Text = ""; ddlfor48.Text = "";
        ddlfor51.Text = ""; ddlfor52.Text = ""; ddlfor53.Text = ""; ddlfor54.Text = ""; ddlfor55.Text = ""; ddlfor56.Text = ""; ddlfor57.Text = ""; ddlfor58.Text = "";

    }


    private void generateheaderdata(string apptype, string reqtype, string ord_type)
    {
        App_Flow_DefinitionTableAdapter flow = new App_Flow_DefinitionTableAdapter();
        SCBLDataSet.App_Flow_DefinitionDataTable dt;
        string[] tmp;


        if (apptype == "MPR") ord_type = "";

        //link no 1
        dt = new SCBLDataSet.App_Flow_DefinitionDataTable();
        dt = flow.GetDataByTypeSeq(apptype, reqtype, ord_type, 1);
        if (dt.Rows.Count == 0)
        {
            chk7.Checked = false;
            txtfrom1.Text = "";
            txtto1.Text = "";
            ddlflow1.SelectedValue = "";
        }
        else
        {
            chk7.Checked = true;
            tmp = dt[0].lower_limit.ToString().Split('.');
            txtfrom1.Text = tmp[0];
            tmp = dt[0].upperlimit.ToString().Split('.');
            txtto1.Text = tmp[0];
            ddlflow1.SelectedValue = dt[0].flow_id.ToString();
        }

        //link no 2
        dt = new SCBLDataSet.App_Flow_DefinitionDataTable();
        dt = flow.GetDataByTypeSeq(apptype, reqtype, ord_type, 2);
        if (dt.Rows.Count == 0)
        {
            chk8.Checked = false;
            txtfrom2.Text = "";
            txtto2.Text = "";
            ddlflow2.SelectedValue = "";
        }
        else
        {
            chk8.Checked = true;
            tmp = dt[0].lower_limit.ToString().Split('.');
            txtfrom2.Text = tmp[0];
            tmp = dt[0].upperlimit.ToString().Split('.');
            txtto2.Text = tmp[0];
            ddlflow2.SelectedValue = dt[0].flow_id.ToString();
        }

        //link no 3
        dt = new SCBLDataSet.App_Flow_DefinitionDataTable();
        dt = flow.GetDataByTypeSeq(apptype, reqtype, ord_type, 3);
        if (dt.Rows.Count == 0)
        {
            chk9.Checked = false;
            txtfrom3.Text = "";
            txtto3.Text = "";
            ddlflow3.SelectedValue = "";
        }
        else
        {
            chk9.Checked = true;
            tmp = dt[0].lower_limit.ToString().Split('.');
            txtfrom3.Text = tmp[0];
            tmp = dt[0].upperlimit.ToString().Split('.');
            txtto3.Text = tmp[0];
            ddlflow3.SelectedValue = dt[0].flow_id.ToString();
        }


        //link no 4
        dt = new SCBLDataSet.App_Flow_DefinitionDataTable();
        dt = flow.GetDataByTypeSeq(apptype, reqtype, ord_type, 4);
        if (dt.Rows.Count == 0)
        {
            chk10.Checked = false;
            txtfrom4.Text = "";
            txtto4.Text = "";
            ddlflow4.SelectedValue = "";
        }
        else
        {
            chk10.Checked = true;
            tmp = dt[0].lower_limit.ToString().Split('.');
            txtfrom4.Text = tmp[0];
            tmp = dt[0].upperlimit.ToString().Split('.');
            txtto4.Text = tmp[0];
            ddlflow4.SelectedValue = dt[0].flow_id.ToString();
        }


        //link no 5
        dt = new SCBLDataSet.App_Flow_DefinitionDataTable();
        dt = flow.GetDataByTypeSeq(apptype, reqtype, ord_type, 5);
        if (dt.Rows.Count == 0)
        {
            chk11.Checked = false;
            txtfrom5.Text = "";
            txtto5.Text = "";
            ddlflow5.SelectedValue = "";
        }
        else
        {
            chk11.Checked = true;
            tmp = dt[0].lower_limit.ToString().Split('.');
            txtfrom5.Text = tmp[0];
            tmp = dt[0].upperlimit.ToString().Split('.');
            txtto5.Text = tmp[0];
            ddlflow5.SelectedValue = dt[0].flow_id.ToString();
        }
    }

    private void reset_data()
    {
        string apptype = ddlapptype.SelectedValue.ToString();
        string reqtype = ddltype.SelectedValue.ToString();
        string purtype = ddlpurtype.SelectedValue.ToString();

        
        
        if ((apptype == "") || (reqtype == "") ||((apptype != "MPR") && (purtype == "")))
        {
            tblheader.Visible = false;
        }
        else
        {
            tblheader.Visible = true;
            if (apptype == "MPR") purtype = "";

            generateheaderdata(apptype,reqtype, purtype);
        }
    }

    protected void ddlapptype_SelectedIndexChanged(object sender, EventArgs e)
    {
        reset_data();
    }
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        reset_data();

    }
    protected void ddlpurtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        reset_data();
    }


    private bool check_data()
    {
        bool flg = true;

        if (chk7.Checked)
        {
            if ((txtfrom1.Text == "") || (txtto1.Text == "") || (ddlflow1.SelectedValue == ""))
                flg = false;
        }
        else
            flg = false;

        if (chk8.Checked)
        {
            if ((txtfrom2.Text == "") || (txtto2.Text == "") || (ddlflow2.SelectedValue == ""))
                flg = false;
        }

        if (chk9.Checked)
        {
            if (chk8.Checked == false)
                flg = false;

            if ((txtfrom3.Text == "") || (txtto3.Text == "") || (ddlflow3.SelectedValue == ""))
                flg = false;
        }

        if (chk10.Checked)
        {
            if ((chk9.Checked == false) || (chk8.Checked == false))
                flg = false;

            if ((txtfrom4.Text == "") || (txtto4.Text == "") || (ddlflow4.SelectedValue == ""))
                flg = false;
        }

        if (chk11.Checked)
        {
            if ((chk10.Checked == false) || (chk9.Checked == false) || (chk8.Checked == false))
                flg = false;

            if ((txtfrom5.Text == "") || (txtto5.Text == "") || (ddlflow5.SelectedValue == ""))
                flg = false;
        }
        
        return flg;

    }
    protected void btnsaveheader_Click(object sender, EventArgs e)
    {
        bool flg = true;
        string apptype = ddlapptype.SelectedValue.ToString();
        string req_type = ddltype.SelectedValue.ToString();
        string ord_type = ddlpurtype.SelectedValue.ToString();
        
        if (apptype == "") return;
        if (req_type == "") return;


        if ((apptype!="MPR")&&(ord_type == "")) return;                    

        if (check_data() == false) return;

        if (apptype == "MPR") ord_type = "";

        App_Flow_DefinitionTableAdapter app = new App_Flow_DefinitionTableAdapter();
        SqlTransaction myTrans = HelperTA.OpenTransaction(app.Connection);

        try
        {
            app.AttachTransaction(myTrans);

            app.DeleteByPurchaseType(apptype, req_type, ord_type);

            if (chk7.Checked)
            {
                app.InsertFlowData(apptype, req_type, ord_type, 1, Convert.ToDecimal(txtfrom1.Text), Convert.ToDecimal(txtto1.Text), ddlflow1.SelectedValue.ToString());
            }

            if (chk8.Checked)
            {
                app.InsertFlowData(apptype, req_type, ord_type, 2, Convert.ToDecimal(txtfrom2.Text), Convert.ToDecimal(txtto2.Text), ddlflow2.SelectedValue.ToString());
            }


            if (chk9.Checked)
            {
                app.InsertFlowData(apptype, req_type, ord_type, 3, Convert.ToDecimal(txtfrom3.Text), Convert.ToDecimal(txtto3.Text), ddlflow3.SelectedValue.ToString());
            }


            if (chk10.Checked)
            {
                app.InsertFlowData(apptype, req_type, ord_type, 4, Convert.ToDecimal(txtfrom4.Text), Convert.ToDecimal(txtto4.Text), ddlflow4.SelectedValue.ToString());
            }


            if (chk11.Checked)
            {
                app.InsertFlowData(apptype, req_type, ord_type, 5, Convert.ToDecimal(txtfrom5.Text), Convert.ToDecimal(txtto5.Text), ddlflow5.SelectedValue.ToString());
            }


            if (flg)
                myTrans.Commit();
            else
                myTrans.Rollback();
        }
        catch
        {
            flg = false;
            myTrans.Rollback();
        }
        finally
        {
            HelperTA.CloseTransaction(app.Connection, myTrans);
        }

        if (flg)
            generateheaderdata(apptype,req_type, ord_type);

    }

    protected void btnsavedet_Click(object sender, EventArgs e)
    {
        string trn_type = ddlappflow.SelectedValue.ToString();
        if (trn_type == "") return;

        int aper, eper, rper;
        tbl_app_ruleTableAdapter rul = new tbl_app_ruleTableAdapter();

        SqlTransaction myTrans = HelperTA.OpenTransaction(rul.Connection);

        try
        {
            rul.AttachTransaction(myTrans);

            rul.DeleteRuleByTrnType(trn_type);

            if (ddlapp1.Text != "")
            {
                if (chk1.Items[0].Selected == true) eper = 1; else eper = 0;
                if (chk1.Items[1].Selected == true) aper = 1; else aper = 0;
                if (chk1.Items[2].Selected == true) rper = 1; else rper = 0;

                rul.InsertRule(trn_type, 1, ddlapp1.Text, eper, aper, rper, ddlfor11.Text, ddlfor21.Text, ddlfor31.Text, ddlfor41.Text, ddlfor51.Text, "");
            }

            if (ddlapp2.Text != "")
            {
                if (chk2.Items[0].Selected == true) eper = 1; else eper = 0;
                if (chk2.Items[1].Selected == true) aper = 1; else aper = 0;
                if (chk2.Items[2].Selected == true) rper = 1; else rper = 0;

                rul.InsertRule(trn_type, 2, ddlapp2.Text, eper, aper, rper, ddlfor12.Text, ddlfor22.Text, ddlfor32.Text, ddlfor42.Text, ddlfor52.Text, "");
            }

            if (ddlapp3.Text != "")
            {
                if (chk3.Items[0].Selected == true) eper = 1; else eper = 0;
                if (chk3.Items[1].Selected == true) aper = 1; else aper = 0;
                if (chk3.Items[2].Selected == true) rper = 1; else rper = 0;

                rul.InsertRule(trn_type, 3, ddlapp3.Text, eper, aper, rper, ddlfor13.Text, ddlfor23.Text, ddlfor33.Text, ddlfor43.Text, ddlfor53.Text, "");
            }

            if (ddlapp4.Text != "")
            {
                if (chk4.Items[0].Selected == true) eper = 1; else eper = 0;
                if (chk4.Items[1].Selected == true) aper = 1; else aper = 0;
                if (chk4.Items[2].Selected == true) rper = 1; else rper = 0;

                rul.InsertRule(trn_type, 4, ddlapp4.Text, eper, aper, rper, ddlfor14.Text, ddlfor24.Text, ddlfor34.Text, ddlfor44.Text, ddlfor54.Text, "");
            }

            if (ddlapp5.Text != "")
            {
                if (chk5.Items[0].Selected == true) eper = 1; else eper = 0;
                if (chk5.Items[1].Selected == true) aper = 1; else aper = 0;
                if (chk5.Items[2].Selected == true) rper = 1; else rper = 0;

                rul.InsertRule(trn_type, 5, ddlapp5.Text, eper, aper, rper, ddlfor15.Text, ddlfor25.Text, ddlfor35.Text, ddlfor45.Text, ddlfor55.Text, "");
            }

            if (ddlapp6.Text != "")
            {
                if (chk6.Items[0].Selected == true) eper = 1; else eper = 0;
                if (chk6.Items[1].Selected == true) aper = 1; else aper = 0;
                if (chk6.Items[2].Selected == true) rper = 1; else rper = 0;

                rul.InsertRule(trn_type, 6, ddlapp6.Text, eper, aper, rper, ddlfor16.Text, ddlfor26.Text, ddlfor36.Text, ddlfor46.Text, ddlfor56.Text, "");
            }

            if (ddlapp7.Text != "")
            {
                if (chk77.Items[0].Selected == true) eper = 1; else eper = 0;
                if (chk77.Items[1].Selected == true) aper = 1; else aper = 0;
                if (chk77.Items[2].Selected == true) rper = 1; else rper = 0;

                rul.InsertRule(trn_type, 7, ddlapp7.Text, eper, aper, rper, ddlfor17.Text, ddlfor27.Text, ddlfor37.Text, ddlfor47.Text, ddlfor57.Text, "");
            }

            if (ddlapp8.Text != "")
            {
                if (chk88.Items[0].Selected == true) eper = 1; else eper = 0;
                if (chk88.Items[1].Selected == true) aper = 1; else aper = 0;
                if (chk88.Items[2].Selected == true) rper = 1; else rper = 0;

                rul.InsertRule(trn_type, 8, ddlapp8.Text, eper, aper, rper, ddlfor18.Text, ddlfor28.Text, ddlfor38.Text, ddlfor48.Text, ddlfor58.Text, "");
            }

            myTrans.Commit();
            
        }
        catch
        {
            myTrans.Rollback();
        }
        finally
        {
            HelperTA.CloseTransaction(rul.Connection, myTrans);
        }


    }

    protected void ddlappflow_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selval = ddlappflow.SelectedValue.ToString();
       
        if (selval == "")
        {
            tbldet.Visible = false;
        }
        else
        {
            tbldet.Visible = true;
            reset_tbl();
            generatedata(selval);
        }
    }
   
}
