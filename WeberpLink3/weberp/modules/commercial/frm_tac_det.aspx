<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_tac_det.aspx.cs" Inherits="frm_tac_det" Title=""   EnableEventValidation="false" ValidateRequest="false"   %>
<%@ Register Assembly="AjaxControlToolkit"  Namespace="AjaxControlToolkit"   TagPrefix="ajaxToolkit" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="HTMLEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <asp:UpdatePanel ID ="updateall" runat="server">
   <ContentTemplate>
   
<table id="tblmaster" runat="server" class="tblmas" style="width: 100%">
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
                TERMS AND CONDITION MASTER SCREEN</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                <table style="width:61%;">
                    <tr>
                        <td style="width: 86px">
                            &nbsp;</td>
                        <td style="width: 15px">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 86px; text-align: left;">
                            TAC ID</td>
                        <td style="width: 15px">
                            :</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtid" runat="server" CssClass="txtbox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 86px; text-align: left;">
                            TYPE</td>
                        <td style="width: 15px">
                            :</td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddltype" runat="server" CssClass="txtbox">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="gen">Gen Terms</asp:ListItem>
                                <asp:ListItem Value="spe">Special Terms</asp:ListItem>
                                <asp:ListItem Value="pay">Pay Terms</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 86px; text-align: left;">
                            SEQ NO</td>
                        <td style="width: 15px">
                            :</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtseq" runat="server" CssClass="txtbox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 86px; text-align: left;">
                            CATEGORY</td>
                        <td style="width: 15px">
                            :</td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlcat" runat="server" style="text-align: left">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="com">Common</asp:ListItem>
                                <asp:ListItem Value="full">Full Advance</asp:ListItem>
                                <asp:ListItem Value="part">Part Advance</asp:ListItem>
                                <asp:ListItem Value="no">No Advance</asp:ListItem>
                                <asp:ListItem Value="LC">LC Payment</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    </table>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                DETAIL</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 204px; text-align: center">
            <HTMLEditor:Editor runat="server" Id="txteditor" Height="200px" AutoFocus="true" 
                    Width="100%" />
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                            <asp:Button ID="btnadd" runat="server" CssClass="btn2" onclick="btnadd_Click" 
                                Text="Add/Update" Width="104px" />
                            <asp:Button ID="btndel" runat="server" CssClass="btn2" onclick="btndel_Click" 
                                Text="Delete" Width="98px" />
                        </td>
        </tr>
        <tr>
            <td colspan="3" class="tdcell" style="height: 40px; text-align: left">
                <asp:GridView ID="gdtac" runat="server" BackColor="White" BorderColor="#41519A" 
                    BorderStyle="Solid" BorderWidth="1px" CellPadding="4" ForeColor="#333333" 
                    GridLines="None" OnRowDataBound="gdtac_RowDataBound" 
                    onselectedindexchanged="gdtac_SelectedIndexChanged" PageSize="100" 
                    SkinID="GridView" Width="682px">
                    <FooterStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#41519A" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="LightBlue" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="Lavender" />
                    <EditRowStyle BackColor="#2461BF" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 119px; text-align: center">
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
</asp:Content>

