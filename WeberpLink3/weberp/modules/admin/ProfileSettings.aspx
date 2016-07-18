<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/masMain.master" CodeFile="ProfileSettings.aspx.cs" Inherits="modules_admin_ProfileSettings" %>

<%@ Register src="../../UserControls/menuLogOut.ascx" tagname="menuLogOut" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="Profile Settings" runat="server" Font-Bold="True"  />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width:99%;text-align:left">
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <uc1:menuLogOut ID="menuLogOut1" runat="server" />
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                </asp:Panel>
        </ContentTemplate>
        <Triggers>
             </Triggers>
    </asp:UpdatePanel>
</asp:Content>