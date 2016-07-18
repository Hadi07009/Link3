<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" EnableEventValidation="false" ValidateRequest="false" CodeFile="GetDataFromInv.aspx.cs" Inherits="modules_FixedAsset_TransactionDetails_GetDataFromInv" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>

            <div><asp:Panel ID="Panel2" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="Label3" Text="CONVERT TO FIXED ASSET" runat="server" />
             </asp:Panel></div>
            <div>

                <table width="100%">
                    <tr>
                        <td colspan="5" style="height: 32px">
                            <asp:Label ID="lblerrormessage" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" Text="Check All" Visible="False"/>
                        </td>
                        <td>
                            <asp:Label ID="lblamount" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" Font-Bold="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal" Visible="False">
                                <asp:ListItem>ONU</asp:ListItem>
                                <asp:ListItem>FIBER</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>&nbsp;</td>
                        <td align="right">
                            <asp:Button ID="btnconvertdata" runat="server" OnClick="btnconvertdata_Click" Text="Convert Data" Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:GridView ID="GridView2" runat="server" Font-Size="9pt" Width="100%"
                                AutoGenerateColumns="False">

                                <Columns>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkupdw" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="REF NO">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrefno" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Item Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblitemCode" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Item Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblitemName" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Party Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPartyCode" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Party Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPartyName" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="FA A/C Code">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtacCode" runat="server" />
                                             <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtenderAccCode" runat="server" 
                                                CompletionInterval="100" 
                                                CompletionListCssClass="autocomplete_completionListElement" 
                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" 
                                                DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="1" 
                                                ServiceMethod="GetAccountCode"
                                                ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx"
                                                ShowOnlyCurrentWordInCompletionListItem="true" 
                                                TargetControlID="txtacCode">
                                             </ajaxToolkit:AutoCompleteExtender>         
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="QTY">
                                        <ItemTemplate>
                                            <asp:Label ID="lblqty" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblamountw" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldate" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="FA ?">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkupdwtt" runat="server" Checked="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                </Columns>

                            </asp:GridView>
                        </td>
                    </tr>
                </table>

            </div>
        </ContentTemplate>

        <Triggers>
        </Triggers>

    </asp:UpdatePanel>

</asp:Content>
