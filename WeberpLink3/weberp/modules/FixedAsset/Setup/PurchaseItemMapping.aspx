<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/masMain.master" CodeFile="PurchaseItemMapping.aspx.cs" Inherits="modules_FixedAsset_Setup_PurchaseItemMapping" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
        <asp:Label ID="lblleave" Text="PURCHASE ITEM MAPPING" runat="server" />
    </asp:Panel>
    <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="900px">
        <table style="width: 99%; text-align:left">
            <tr>
                <td style="width: 56px">&nbsp;</td>
                <td style="width: 8px">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 56px">
                    <asp:Label ID="Label1" runat="server" Text="Supplier"></asp:Label>
                </td>
                <td style="width: 8px">:</td>
                <td>
                    <asp:TextBox ID="txtSupplier" runat="server" AutoPostBack="True" OnTextChanged="txtSupplier_TextChanged" Width="400px"></asp:TextBox>
                    <cc2:AutoCompleteExtender ID="txtSupplier_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetSupplierAccountInformation" ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx" TargetControlID="txtSupplier">
                    </cc2:AutoCompleteExtender>
                </td>
            </tr>
            <tr>
                <td style="width: 56px">
                    <asp:Label ID="Label2" runat="server" Text="Item"></asp:Label>
                </td>
                <td style="width: 8px">:</td>
                <td>
                    <asp:TextBox ID="txtItem" runat="server" AutoPostBack="True" OnTextChanged="txtItem_TextChanged" Width="400px"></asp:TextBox>
                    <cc2:AutoCompleteExtender ID="txtItem_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetInvItem" ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx" TargetControlID="txtItem">
                    </cc2:AutoCompleteExtender>
                </td>
            </tr>
            <tr>
                <td style="width: 56px">&nbsp;</td>
                <td style="width: 8px">&nbsp;</td>
                <td>
                    <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal" Width="200px">
                        <asp:ListItem Selected="True" Value="Y">Active</asp:ListItem>
                        <asp:ListItem Value="N">Inactive</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td style="width: 56px">&nbsp;</td>
                <td style="width: 8px">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 56px">&nbsp;</td>
                <td style="width: 8px">&nbsp;</td>
                <td>
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" Width="100px" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" Width="100px" OnClick="btnClear_Click" />
                </td>
            </tr>
            <tr>
                <td style="width: 56px">&nbsp;</td>
                <td style="width: 8px">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:GridView ID="grdGetMappingInformation" runat="server" AutoGenerateColumns="False" Width="100%" OnRowCommand="grdGetMappingInformation_RowCommand" OnRowDataBound="grdGetMappingInformation_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="SL">
                                                    <ItemTemplate>
                                                        <%# Container.DisplayIndex + 1 %>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="RefNo">
                                <ItemTemplate>
                                    <asp:Label ID="lblRefNo" runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("ItemCode") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("Itm_Det_desc") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Status Value">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatusValue" runat="server" Text='<%# Bind("ItemStatus") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("ItemStatusTest") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="True" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="width: 56px">&nbsp;</td>
                <td style="width: 8px">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
       
    </asp:Panel>
</asp:Content>
