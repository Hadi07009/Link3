<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_quo_approval_batch.aspx.cs" Inherits="frm_quo_approval_batch" Title=""   ValidateRequest="false" EnableViewState="true"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register src="usercontrols/ctl_cs_batch_approval.ascx" tagname="ctl_cs_batch_approval" tagprefix="uc1" %>     
<%@ Register src="usercontrols/ctl_comments.ascx" tagname="ctl_comments" tagprefix="uc2" %> 
       
       
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
    .style1
    {
        height: 9px;
    }
    .style2
    {
        height: 34px;
    }
    


.tbl
{
	font: 10pt verdana;
	font-weight: 300;
	color: #330099;
	
}

    .style3
    {
        width: 646px;
        font-size: 1em;
        text-align: left;
        border: thin solid #000080;
    }
    .style4
    {
        text-align: center;
    }
    .style6
    {
        text-align: center;
        font-size: x-small;
    }
    .style5
    {
        width: 123px;
        font-weight: bold;
    }
    .heading
    {
        text-align: left;
    }
    .style34
    {
        height: 14px;
    }
        .style35
        {
            font-style: normal;
            font-variant: normal;
            font-weight: 300;
            font-size: 10pt;
            line-height: normal;
            font-family: verdana;
            color: #330099;
        }
    </style>
    <asp:UpdatePanel ID="upd1" runat="server">
    <ContentTemplate>
    
    <table class="tblmas" style="width: 100%" id="tblmaster" runat="server">
        <tr>
            <td style="height: 22px">
            </td>
            <td style="height: 22px">
            </td>
            <td style="height: 22px">
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="3" style="text-align: center">
                QUOTATION APPROVAL SCREEN&nbsp;</td>
        </tr>
        <tr>
            <td class="style35" colspan="3" style="text-align: left">
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: left">
                Total Items:&nbsp;<asp:Label ID="lblcount" runat="server" Text="(0)"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: left">
                    <asp:GridView ID="gdItem" runat="server" BackColor="White" 
                                BorderColor="#41519A" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                                ForeColor="#333333" GridLines="None" PageSize="100" SkinID="GridView" 
                                  OnRowDataBound = "gdItem_RowDataBound" 
                                style="border-top-width: 1px; border-left-width: 1px; border-left-color: #e6e6fa; border-bottom-width: 1px; border-bottom-color: #e6e6fa; border-top-color: #e6e6fa; border-right-width: 1px; border-right-color: #e6e6fa;" 
                                Width="100%" AutoGenerateColumns="False">
                        <FooterStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#41519A" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle Font-Bold="True" Wrap="False" />
                        <HeaderStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" 
                            HorizontalAlign="Left" Wrap="False" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="Lavender" Font-Size="8pt" 
                                    Font-Underline="False" HorizontalAlign="Left" 
                            VerticalAlign="Top" />
                        <RowStyle Font-Size="8pt" Wrap="True" VerticalAlign="Top" 
                            HorizontalAlign="Left" />
                        <Columns>
                        
                            <asp:TemplateField FooterStyle-HorizontalAlign="Left">
                             <AlternatingItemTemplate>
                                                <asp:CheckBox ID="CheckBox1"  runat="server" />
                              </AlternatingItemTemplate>
                            <HeaderTemplate>
                                        <asp:CheckBox ID="CheckBox1" Font-Bold="true"   runat="server" Text="All" />
                             </HeaderTemplate>
                            
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1"  runat="server"  />
                                </ItemTemplate>

                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Mpr Ref" Visible="false"  >
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" CssClass="tbl"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Quo Ref" Visible="false" >
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server"  CssClass="tbl"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Icode" Visible="false" >
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server"  CssClass="tbl"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" CssClass="tbl"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VIEW DETAIL" >
                                <ItemTemplate>
                                    <uc1:ctl_cs_batch_approval ID="ctl1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: left">
                       
                            <table ID="tbltooltip" runat="server" class="style3" 
                                style="background-color: #FFFFFF; background-position: center center" 
                               >
                                <tr >
                                    <td colspan="2" >
                                        &nbsp;</td>
                                </tr>
                                <tr >
                                    <td class="style6" colspan="2" >
                                        TERMS AND CONDITIONS</td>
                                </tr>
                                <tr >
                                    <td class="style5" valign="top" >
                                        General Terms:</td>
                                    <td bgcolor="AliceBlue" >
                                        &nbsp;</td>
                                </tr>
                                <tr >
                                    <td class="style5" valign="top" >
                                        Special Terms:</td>
                                    <td bgcolor="AliceBlue">
                                        &nbsp;</td>
                                </tr>
                                <tr >
                                    <td class="style5" valign="top">
                                        Pay Terms:</td>
                                    <td bgcolor="AliceBlue" >
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                <td class="style5" valign="top">
                                    Valid Days:</td>
                                <td bgcolor="AliceBlue">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style5">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                                </table>
                        </td>
        </tr>
        <tr>
            <td  class="tbl" colspan="3" style="height: 24px; text-align: left">
             <table  width="100%" >
                    <tr>
                        <td id="Td3" runat="server" style="text-align: center">
                            Comments: <asp:TextBox ID="txtcomments" runat="server" CssClass="txtbox" 
                                Width="500px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td id="Td4" runat="server" 
                            style="text-align: center; height: 21px;">
                            Forward to:<asp:DropDownList ID="ddlforto" runat="server" CssClass="txtbox" 
                                Width="500px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td id="Td5" runat="server" class="style34" 
                            style="text-align: center; ">
                            <asp:Label ID="lblComm" runat="server" ForeColor="Red" 
                                Text="Please type your comments." Visible="False" Width="477px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td  class="style1" style="text-align: left; ">
                        
                            
                      
                   
                            </td>
                    </tr>
                    <tr>
                   
                        <td style="text-align: center">
                            &nbsp;
                            <asp:Button ID="btnreject" runat="server" CssClass="btn2" OnClick="btnreject_Click"
                                Text="Reject" Width="80px" />
                            &nbsp;
                            <asp:Button ID="btnforward" runat="server" CssClass="btn2" OnClick="btnforward_Click"
                                Text="Forward" Width="80px" />&nbsp;
                            <asp:Button ID="btnapp" runat="server" CssClass="btn2" onclick="btnapp_Click" 
                                Text="Approve" Width="80px" />
                        </td>
                       
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            &nbsp;</td>
                    </tr>
                    </table>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        </table>
        </ContentTemplate>
    </asp:UpdatePanel>
  <script type="text/javascript">



         function SelectAll(id) {
             //get reference of GridView control

             var grid = document.getElementById("<%= gdItem.ClientID %>");

             // var grid = document.getElementById(itm);
             //variable to contain the cell of the grid
             var cell;

             if (grid.rows.length > 0) {
                 //loop starts from 1. rows[0] points to the header.
                 for (i = 1; i < grid.rows.length; i++) {
                     //get the reference of first column
                     cell = grid.rows[i].cells[0];

                     //loop according to the number of childNodes in the cell
                     for (j = 0; j < cell.childNodes.length; j++) {
                         //if childNode type is CheckBox


                         if (cell.childNodes[j].type == "checkbox") {

                             //assign the status of the Select All checkbox to the cell checkbox within the grid
                             cell.childNodes[j].checked = document.getElementById(id).checked;
                            
                         }
                     }
                 }
             }
         }



        
        
        
    </script>
</asp:Content>

