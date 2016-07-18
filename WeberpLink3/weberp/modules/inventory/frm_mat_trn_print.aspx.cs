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
using LibraryDAL.SCBL2DataSetTableAdapters;

public partial class frm_mat_trn_print : System.Web.UI.Page
{
   

    protected void Page_Load(object sender, EventArgs e)
    {
               
        clsStatic.CheckUserAuthentication();
        tblmaster.BgColor = "FFFFFFF";
        tbl_mat_rec_retTableAdapter mat = new tbl_mat_rec_retTableAdapter();
        SCBL2DataSet.tbl_mat_rec_retDataTable dt = new SCBL2DataSet.tbl_mat_rec_retDataTable();
        LibraryDAL.dsLinkofficeTableAdapters.tblUserInfoTableAdapter usr=new LibraryDAL.dsLinkofficeTableAdapters.tblUserInfoTableAdapter();
        HtmlTableRow hrow;

        int seq = 0;

        string qrystr = Session[clsStatic.sessionQueryString].ToString();
        dt = mat.GetDataByTrnRef(qrystr);

        if (dt == null) { tblmaster.Visible = false; return; }
        if (dt.Rows.Count == 0) { tblmaster.Visible = false; return; }

        tblmaster.Visible = true;

        lbldate.Text = dt[0].trn_datetime.ToString();
        lblparty.Text = dt[0].pcode + ": " + dt[0].pdet;
        lblporef.Text = dt[0].po_ref.ToString();
        lbltrnref.Text = dt[0].trn_ref_no;
        lbltrnby.Text = usr.GetUserByCode(dt[0].user_code.ToString())[0].UserName;
        switch (dt[0].trn_type.ToString())
        {
          
            case "CONFIRM":
                lbltype.Text = "MATERIAL RECEIVED (MRR)";
                break;

            case "OK":
                lbltype.Text = "MATERIAL RECEIVED(QUALITY CHECK OK)";
                break;

            case "INSPECTION":
                lbltype.Text = "MATERIAL RECEIVED FOR INSPECTION";
                break;
            case "RETURN":
                lbltype.Text = "MATERIAL RETURNED (QUALITY CHECK FAIL)";
                break;
            default:
                lbltype.Text = "ERROR";
                break;
        }

        foreach (SCBL2DataSet.tbl_mat_rec_retRow dr in dt.Rows)
        {
            hrow = new HtmlTableRow();

            hrow.Cells.Add(new HtmlTableCell());
            hrow.Cells.Add(new HtmlTableCell());
            hrow.Cells.Add(new HtmlTableCell());
            hrow.Cells.Add(new HtmlTableCell());
            hrow.Cells.Add(new HtmlTableCell());
            hrow.Cells.Add(new HtmlTableCell());
            hrow.Cells.Add(new HtmlTableCell());

            seq++;
            hrow.Cells[0].InnerText = seq.ToString();           
            hrow.Cells[1].InnerText = dr.icode.ToString()+": " + dr.idet.ToString();
            hrow.Cells[2].InnerText = dr.itm_rec_ret_qty.ToString("N2") + " " + dr.uom.ToString();
            hrow.Cells[3].InnerText = dr.brand;
            hrow.Cells[4].InnerText = dr.origin;
            hrow.Cells[5].InnerText = dr.packing;
            hrow.Cells[6].InnerText = dr.status_det;

            tblhtml.Rows.Add(hrow);
        }

          
    }
   
    
}

