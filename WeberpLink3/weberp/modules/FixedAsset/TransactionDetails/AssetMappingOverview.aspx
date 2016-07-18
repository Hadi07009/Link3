<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="AssetMappingOverview.aspx.cs" Inherits="modules_FixedAsset_TransactionDetails_AssetMappingOverview" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>

    <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
        <asp:Label ID="lblleave" Text="Asset Mapping Overview" runat="server" />
    </asp:Panel>
            <table style="width: 100%;">
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:GridView ID="grdItemShow" runat="server" AutoGenerateColumns="False" Width="100%" OnRowCommand="grdItemShow_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <%# Container.DisplayIndex + 1 %>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                                <asp:BoundField DataField="Itm_Det_Desc" HeaderText="Item Name" />
                                <asp:BoundField DataField="ItemAcc" HeaderText="Account Code " />
                                <asp:BoundField DataField="FaAcc" HeaderText="FA A/C" />
                                <asp:BoundField DataField="DepAcc" HeaderText="Depreciation A/C" />
                                <asp:BoundField DataField="RevAcc" HeaderText="Revaluation A/C" />
                                <asp:BoundField DataField="DispAcc" HeaderText="Disposal A/C" />
                                <asp:BoundField DataField="WriteoffAcc" HeaderText="Write off A/C" />
                                <asp:BoundField DataField="CogsAcc" HeaderText="Cogs A/C" />
                                <asp:BoundField DataField="ExpenseAcc" HeaderText="Expense A/C" />
                                <asp:CommandField ShowSelectButton="True" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Panel ID="PanelForDetails" runat="server">
                            <table style="width:100%;text-align:left">
        <tr>
            <td style="width: 133px">
                <asp:Label ID="Label3" runat="server" Text="Item Code"></asp:Label>
            </td>
            <td style="width: 14px">:</td>
            <td style="width: 216px">
                <asp:TextBox ID="txtItemCode" runat="server" ReadOnly="True" width="400px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtItemCode" ErrorMessage="Enter Item Code" ValidationGroup="grpItm"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 133px" >
                <asp:Label ID="Label2" runat="server" Text="Item Name"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:TextBox ID="txtItemName" runat="server" width="400px" ReadOnly="True"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtItemName" ErrorMessage="Enter Item Name" 
                    ValidationGroup="grpItm"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 133px" >
                <asp:Label ID="Label8" runat="server" Text="Account Code"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:TextBox ID="txtAccCode" runat="server" Width="400px" AutoPostBack="True" OnTextChanged="txtAccCode_TextChanged"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="txtAccCode_AutoCompleteExtender" 
                    runat="server" 
                    DelimiterCharacters="" 
                    Enabled="True" 
                    ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx"
                    MinimumPrefixLength="1" 
                    ServiceMethod="GetAccountCode" 
                    TargetControlID="txtAccCode">
                </cc1:AutoCompleteExtender>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 133px" >
                <asp:Label ID="Label21" runat="server" Text="FA A/C"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:TextBox ID="txtFaAcc" runat="server" Width="400px" AutoPostBack="True" OnTextChanged="txtFaAcc_TextChanged"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="txtFaAcc_AutoCompleteExtender" runat="server" 
                    DelimiterCharacters="" 
                    Enabled="True" 
                    ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx" 
                    MinimumPrefixLength="1" 
                    ServiceMethod="GetAccountCode" 
                    TargetControlID="txtFaAcc">
                </cc1:AutoCompleteExtender>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 133px" >
                <asp:Label ID="Label22" runat="server" Text="Depreciation A/C"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:TextBox ID="txtDepAcc" runat="server" Width="400px" AutoPostBack="True" OnTextChanged="txtDepAcc_TextChanged"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="txtDepAcc_AutoCompleteExtender" 
                    runat="server" DelimiterCharacters="" 
                    Enabled="True" ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx" 
                    MinimumPrefixLength="1" 
                    ServiceMethod="GetAccountCode"
                    TargetControlID="txtDepAcc">
                </cc1:AutoCompleteExtender>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 133px" >
                <asp:Label ID="Label27" runat="server" Text="Disposal A/C"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:TextBox ID="txtDispAcc" runat="server" Width="400px" AutoPostBack="True" OnTextChanged="txtDispAcc_TextChanged"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="txtDispAcc_AutoCompleteExtender" 
                    runat="server" DelimiterCharacters="" 
                    Enabled="True" 
                    ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx"
                    MinimumPrefixLength="1" 
                    ServiceMethod="GetAccountCode"
                    TargetControlID="txtDispAcc">
                </cc1:AutoCompleteExtender>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 133px" >
                <asp:Label ID="Label28" runat="server" Text="Revaluation A/C"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:TextBox ID="txtRevAcc" runat="server" Width="400px" AutoPostBack="True" OnTextChanged="txtRevAcc_TextChanged"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="txtRevAcc_AutoCompleteExtender"
                     runat="server" 
                    DelimiterCharacters="" 
                    Enabled="True" 
                    ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx" 
                    MinimumPrefixLength="1" 
                    ServiceMethod="GetAccountCode"
                    TargetControlID="txtRevAcc">
                </cc1:AutoCompleteExtender>
            </td>
            <td>
                &nbsp;</td>
        </tr>
                                <tr>
                                    <td style="width: 133px">
                                        <asp:Label ID="Label26" runat="server" Text="Write off A/C"></asp:Label>
                                    </td>
                                    <td style="width: 14px">:</td>
                                    <td style="width: 216px">
                                        <asp:TextBox ID="txtWriteoffAcc" runat="server" AutoPostBack="True" OnTextChanged="txtWriteoffAcc_TextChanged" Width="400px"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="txtWriteoffAcc_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetAccountCode" ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx" TargetControlID="txtWriteoffAcc">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 133px">
                                        <asp:Label ID="Label30" runat="server" Text="Cogs A/C"></asp:Label>
                                    </td>
                                    <td style="width: 14px">:</td>
                                    <td style="width: 216px">
                                        <asp:TextBox ID="txtCogsAcc" runat="server" AutoPostBack="True" OnTextChanged="txtCogsAcc_TextChanged" Width="400px"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="txtCogsAcc_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetAccountCode" ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx" TargetControlID="txtCogsAcc">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 133px">
                                        <asp:Label ID="Label31" runat="server" Text="Expense A/C"></asp:Label>
                                    </td>
                                    <td style="width: 14px">:</td>
                                    <td style="width: 216px">
                                        <asp:TextBox ID="txtExpenseAcc" runat="server" AutoPostBack="True" OnTextChanged="txtExpenseAcc_TextChanged" Width="400px"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="txtExpenseAcc_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetAccountCode" ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx" TargetControlID="txtExpenseAcc">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
        <tr>
            <td style="width: 133px" >
                &nbsp;</td>
            <td style="width: 14px" >
                &nbsp;</td>
            <td style="width: 216px" >
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 133px" >
                &nbsp;</td>
            <td style="width: 14px" >
                &nbsp;</td>
            <td style="width: 216px" >
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" 
                    onclick="btnSave_Click" ValidationGroup="grpItm"  />
                 &nbsp;
                <asp:Button ID="btnClear" runat="server" CausesValidation="False" 
                    onclick="btnClear_Click" Text="Clear" Width="100px" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        </table>   
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
    
            </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers> 
        </asp:UpdatePanel>
    <script src="../../scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../../scripts/jquery.autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript">

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 46 || charCode > 57)) {
                alert("Please Enter Only Numeric Value:");
                return false;
            }
            return true;
        }
    </script>
</asp:Content>

