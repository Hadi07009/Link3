<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/masMain.master" CodeFile="StoreLocation.aspx.cs" Inherits="modules_FixedAsset_Setup_StoreLocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="Store Location Setup" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width:99%;text-align:left">
                    <tr>
                        <td style="width: 152px">
                            <asp:Label ID="Label2" runat="server" Text="Store Location ID"></asp:Label>
                        </td>
                        <td style="width: 4px">:</td>
                        <td>
                            <asp:TextBox ID="txtStoreLocationID" runat="server" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 152px">
                            <asp:Label ID="Label3" runat="server" Text="Store Location Name"></asp:Label>
                        </td>
                        <td style="width: 4px">:</td>
                        <td>
                            <asp:TextBox ID="txtStoreLocationName" runat="server" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 152px">&nbsp;</td>
                        <td style="width: 4px">&nbsp;</td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" OnClick="btnSave_Click" />
                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 152px">&nbsp;</td>
                        <td style="width: 4px">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="grdStoreLocation" runat="server" Width="100%" AutoGenerateColumns="False" OnRowCommand="grdStoreLocation_RowCommand" OnRowDeleting="grdStoreLocation_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                <ItemTemplate>
                                    <%# Container.DisplayIndex + 1 %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Store Location ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStoreLocationId" runat="server" Text='<%# Bind("Str_Loc_Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Store Location Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStoreLocationName" runat="server" Text='<%# Bind("Str_Loc_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowSelectButton="True" />
                                    <asp:CommandField ShowDeleteButton="True" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 152px">&nbsp;</td>
                        <td style="width: 4px">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                </asp:Panel>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>