using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Xml;
using System.Web.UI.WebControls;
using LibraryDAL;
using LibraryDAL.dsLinkofficeTableAdapters;
public partial class frmNodePermission : System.Web.UI.Page
{
    private dsLinkoffice.tblNodePermDataTable Dtprm = new dsLinkoffice.tblNodePermDataTable();
    private string UseridTree = "";
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (current.UserId != "ADM")
        {
            clsStatic.CheckUserAuthentication(true);
        }

        if (!IsPostBack)
        {
            clsStatic.MsgConfirmBox(btnUpdate, "Are you sure to update ?");
            hdnMenu.Value = "";
            tblUser.Visible = false;
        }
    }

    private void load_tree()
    {
        XmlDataSource xml_workflow = new XmlDataSource();
        xml_workflow.DataFile = "~/xml/xml_treeview.xml";
        //xml_workflow.XPath = "//Root";
        xml_workflow.DataBind();
        treeMenu.DataSource = xml_workflow;
        treeMenu.DataBind();
        setTreeVisibility(treeMenu);
    }

    private void setTreeVisibility(TreeView tv)
    {
        foreach (TreeNode tn in tv.Nodes)
        {
            setNodeSelection(tn);

        }
    }

    private void setNodeSelection(TreeNode tn)
    {
        string[] prm, strper = { };
        string prmstr = "";
        tn.SelectAction = TreeNodeSelectAction.None;
        if (tn.ChildNodes.Count > 0)
        {

            foreach (TreeNode trchi in tn.ChildNodes)
            {
                setNodeSelection(trchi);
            }
        }
        else
        {
            foreach (dsLinkoffice.tblNodePermRow dr in Dtprm.Rows)
            {
                if (dr.NodeId == tn.Value)
                {
                    strper = dr.NodePerm.Split(',');
                    tn.Checked = true;
                    break;
                }
            }


            if (tn.ImageToolTip.Trim() != "")
            {
                prmstr = "<table><tr><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td><td><table class=\"tbl\"  style=\"width: 325px;\">   <tr>";
                prm = tn.ImageToolTip.ToString().Split(',');
                foreach (string itm in prm)
                {
                    if (itm.Trim() != "")
                    {
                        foreach (string subper in strper)
                        {
                            if (subper.Trim() == itm.Trim())
                            {
                                prmstr = prmstr + "<td>  <input id=\"" + tn.Value.Trim() + "#" + itm.Trim() +
                                 "\" checked=\"checked\" onclick =\"javascript:setdata(id+'#'+checked);\" type=\"checkbox\" />" +
                                 itm.Trim() + "</td>";
                                hdnMenu.Value += tn.Value.Trim() + "#" + itm.Trim() + "#" + "True$";
                                goto next_;

                            }
                        }

                        prmstr = prmstr + "<td>  <input id=\"" + tn.Value.Trim() + "#" + itm.Trim() +
                                 "\"  onclick =\"javascript:setdata(id+'#'+checked);\" type=\"checkbox\" />" +
                                 itm.Trim() + "</td>";
                    }

                next_:
                    ;

                }

                prmstr = prmstr + "</tr> </table></td></tr></table >";
                tn.Text += prmstr;
            }


        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string[] strarr = txtUserlist.Text.Split(':');
        clsSystem usr = new clsSystem();
        dsLinkoffice.tblUserInfoDataTable dtuser = new dsLinkoffice.tblUserInfoDataTable();

        if (strarr.Length == 2)
        {
            dtuser = usr.GetUserById(strarr[0], current.CompanyCode);
            if (dtuser.Count > 0)
            {
                saveTree(dtuser[0].UserId);
                textChange();
                updpnl.Update();
                //clsStatic.MsgConfirmBox("Saved Successfully");
                MessageBox1.ShowSuccess("Saved Successfully");
                


            }
        }
    }
    private void saveTree(string userid)
    {
        tblUserInfoTableAdapter usr = new tblUserInfoTableAdapter();
        clsSystem prm = new clsSystem();

        Dtprm = new dsLinkoffice.tblNodePermDataTable();
        UseridTree = userid;
        
        foreach (TreeNode tn in treeMenu.Nodes)
        {
            saveNodePermission(tn);
        }
        prm.UpdateNodePermission(UseridTree, Dtprm, current.CompanyCode);

        string HtmlTree, modtree, l1_tree,l2_tree;
        bool nodeget, modget, subget;
        string sitepref = "wwwwwwwwww";
        string[] key;
        HtmlTree = @"<ul id=""menu"" runat=""server"">";

        //HtmlTree = @"<ul id=""menu"" runat=""server""> <li><a href=""#"" class=""drop"">Home</a> <div class=""dropdown_2columns"">"
        //  + @"<div class=""col_2""> <h2>Welcome !</h2> </div>  <div class=""col_2"">"
        //  + @" <p>Hi and welcome here ! This is a showcase of the possibilities of this awesome Mega Drop Down Menu.</p>"
        //  + @"<p>This item comes with a large range of prepared typographic stylings such as headings, lists, etc.</p>"
        //  + @"</div><div class=""col_2""> <h2>Cross Browser Support</h2>  </div>"
        //  + @"<div class=""col_1"">  <img src=""img/browsers.png"" width=""125"" height=""48"" alt="""" /> </div>"
        //  + @"<div class=""col_1""> <p>This mega menu has been tested in all major browsers.</p> </div> </div> </li>";


       modtree= "";
        foreach ( TreeNode mod in treeMenu.Nodes[0].ChildNodes)
        {
            //module start
            modtree = @"<li><a href=""#"" class=""drop"">" + mod.Text.Split('<')[0] + "</a>";
            //modtree += @"<div class=""dropdown_4columns"">";
            int moduleNodeNo = 1, valueForIncrement = 1;
            foreach (TreeNode l_first in mod.ChildNodes)
            {
                foreach (TreeNode l_second in l_first.ChildNodes)
                {
                    if (l_second.Checked)
                    {
                        moduleNodeNo = valueForIncrement++;
                        break;
                    }
                }
            }
            switch (moduleNodeNo)
            {
                case 1: { modtree += @"<div class=""dropdown_11columns"">"; } break;
                case 2: { modtree += @"<div class=""dropdown_21columns"">"; } break;
                case 3: { modtree += @"<div class=""dropdown_31columns"">"; } break;
                case 4: { modtree += @"<div class=""dropdown_41columns"">"; } break;
                case 5: { modtree += @"<div class=""dropdown_51columns"">"; } break;
                case 6: { modtree += @"<div class=""dropdown_6columns"">"; } break;
                case 7: { modtree += @"<div class=""dropdown_7columns"">"; } break;
                default: { modtree += @"<div class=""dropdown_8columns"">"; } break;
            }
            modget = false;
            l1_tree = "";
            foreach (TreeNode l_first in mod.ChildNodes)
            {
                l1_tree = @"<div class=""col_1""><h3>" + l_first.Text.Split('<')[0] + "</h3> <ul>";
                nodeget = false;
                foreach (TreeNode l_second in l_first.ChildNodes)
                {
                    if (l_second.ChildNodes.Count == 0)
                    {
                        if (l_second.Checked)
                        {
                            key = GetNodeKey(l_second.Value).Split('+');
                            l1_tree += @" <li><a href=""" + sitepref + key[1].Replace("~", "") + @""">" + l_second.Text.Split('<')[0] + "</a></li> ";
                            nodeget = true;
                            modget = true;
                        }
                    }
                    else
                    {
                        subget = false;
                        l2_tree = @"<li><a href=""#"" style=""text-decoration : none; color :black; cursor:text; "">" + l_second.Text.Split('<')[0] + "</a></li> ";
                        foreach (TreeNode l_third in l_second.ChildNodes)
                        {
                            if (l_third.Checked)
                            {
                                key = GetNodeKey(l_third.Value).Split('+');                                
                                l2_tree += @"<li><a href=""" + sitepref + key[1].Replace("~", "") + @""" style="" font-size:smaller;"">&nbsp;&nbsp;&nbsp;" + l_third.Text.Split('<')[0] + "</a></li>";
                                nodeget = true;
                                modget = true;
                                subget = true;

                            }
                        }

                        if (subget)
                        {
                            l1_tree += l2_tree;
                        }
                    }
                }                             

                l1_tree += @"</ul>  </div>";
                if (nodeget) modtree += l1_tree;
               
            }

            modtree += @"</div> </li>";

            //module end
            if (modget) HtmlTree += modtree;
         
        }

       

        HtmlTree += @"</ul>";
       

        usr.UpdateUserMenu(HtmlTree, userid, current.CompanyCode);

    }

    private string GetNodeKey(string nodeid)
    {
        string kayval = "";
        bool find = false;
        XmlTextReader xmltree = new XmlTextReader(Server.MapPath("~/xml/xml_treeview.xml"));

        while (xmltree.Read())
        {
            switch (xmltree.NodeType)
            {

                case XmlNodeType.Element:
                    while (xmltree.MoveToNextAttribute())
                    {
                        if ((xmltree.Name == "Id") && (xmltree.Value == nodeid))
                        {
                            find = true;
                        }

                        if ((find) && ((xmltree.Name == "FormName") || (xmltree.Name == "Tergate") || (xmltree.Name == "Permission") || (xmltree.Name == "Parameter")))
                        {
                            kayval += xmltree.Value + "+";
                        }

                    }

                    if (find) return kayval;

                    break;
                case XmlNodeType.Text:

                    break;
                case XmlNodeType.EndElement:

                    break;
            }

        }
        xmltree.Close();

        return kayval;
    }

    private string getPermprm(string nodeid, string ormprm)
    {
        string prm = "";
        if (ormprm.Trim() == "") return prm;
        string[] itm, oritm, strhdn = hdnMenu.Value.ToString().Split('$');
        string sngper;
        oritm = ormprm.Split(',');

        foreach (string stror in oritm)
        {
            sngper = "";
            foreach (string str in strhdn)
            {
                itm = str.Split('#');
                if (itm.Length == 3)
                {
                    if ((itm[0] == nodeid) && (itm[1] == stror))
                    {
                        if (itm[2].ToUpper() == "TRUE")
                        {
                            sngper = stror;
                        }
                        else
                        {
                            sngper = "";
                        }

                    }
                }
            }

            if (sngper != "") prm += sngper + ",";
        }
        return prm;
    }

    private void saveNodePermission(TreeNode trn)
    {
        if (trn.ChildNodes.Count > 0)
        {
            foreach (TreeNode trchi in trn.ChildNodes)
            {
                saveNodePermission(trchi);
            }
        }
        else
        {
            if (trn.Checked)
            {
                string[] key = GetNodeKey(trn.Value).Split('+');
                dsLinkoffice.tblNodePermRow drper = Dtprm.NewtblNodePermRow();
                if (key.Length == 5)
                {
                    drper.UserId = UseridTree;
                    drper.NodeId = trn.Value;
                    drper.NodeName = trn.Text.Split('<')[0];
                    drper.NodeFormName = key[0];
                    drper.NodeUrl = key[1];
                    drper.NodePerm = getPermprm(trn.Value, key[2]);
                    drper.NodeParam = key[3];
                    drper.NodePermType = 1;
                    drper.CompanyCode = current.CompanyCode;
                    Dtprm.AddtblNodePermRow(drper);
                }
            }
        }
    }

    private void GenerateTree(dsLinkoffice.tblUserInfoRow druser)
    {

        clsSystem node = new clsSystem();
        Dtprm = new dsLinkoffice.tblNodePermDataTable();

        Dtprm = node.GetNodeByUserId(druser.UserId, current.CompanyCode);

        treeMenu.Visible = true;
        btnUpdate.Visible = true;
        tblUser.Visible = true;

        lblId.Text = druser.UserId;
        lblName.Text = druser.UserName;
        lblDesig.Text = druser.UserDesignation;
        lblDept.Text = druser.UserDepartment;


        load_tree();

    }

    protected void txtUserlist_TextChanged(object sender, EventArgs e)
    {
        textChange();
    }

    private void textChange()
    {
        hdnMenu.Value = "";
        string[] strarr = txtUserlist.Text.Split(':');
        clsSystem usr = new clsSystem();
        dsLinkoffice.tblUserInfoDataTable dtuser = new dsLinkoffice.tblUserInfoDataTable();
        treeMenu.Visible = false;
        btnUpdate.Visible = false;
        tblUser.Visible = false;
        if (strarr.Length == 2)
        {
            dtuser = usr.GetUserById(strarr[0], current.CompanyCode);
            if (dtuser.Count > 0)
            {
                GenerateTree(dtuser[0]);
            }
        }
    }

}
