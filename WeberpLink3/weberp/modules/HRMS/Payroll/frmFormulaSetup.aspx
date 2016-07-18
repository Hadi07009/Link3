<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmFormulaSetup.aspx.cs" Inherits="modules_HRMS_Payroll_frmFormulaSetup" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text=" FORMULA SETUP" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
                    <tr>
                        <td style="width: 125px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 125px">
                            <asp:Label ID="Label2" runat="server" Text="Code"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtCode" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 125px">
                            <asp:Label ID="Label3" runat="server" Text="Name"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 125px">
                            <asp:Label ID="Label4" runat="server" Text="Base"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtBase" runat="server" Width="250px" AutoPostBack="True" OnTextChanged="txtBase_TextChanged"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="txtBase_AutoCompleteExtender"
                                runat="server" DelimiterCharacters=""
                                BehaviorID="txtBase_AutoCompleteExtender"
                                Enabled="True"
                                MinimumPrefixLength="1"
                                ServiceMethod="GetBase"
                                ServicePath="~/modules/Payroll/WebService.asmx"
                                CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                CompletionListItemCssClass="autocomplete_listItem2"
                                TargetControlID="txtBase">
                            </ajaxToolkit:AutoCompleteExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 125px">
                            <asp:Label ID="Label5" runat="server" Text="Operator"></asp:Label>
                        </td>
                        <td class="auto-style9">:</td>
                        <td class="auto-style9">
                            <asp:DropDownList ID="ddlOperator" runat="server" Width="255px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 125px">
                            <asp:Label ID="Label6" runat="server" Text="Multiplier"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtMultiplier" runat="server" Width="250px" AutoPostBack="True"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="txtMultiplier_AutoCompleteExtender"
                                runat="server"
                                BehaviorID="txtMultiplier_AutoCompleteExtender"
                                DelimiterCharacters=""
                                Enabled="True"
                                MinimumPrefixLength="0"
                                ServicePath="~/modules/Payroll/WebService.asmx"
                                ServiceMethod="GetMultiplier"
                                TargetControlID="txtMultiplier">
                            </ajaxToolkit:AutoCompleteExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 125px">
                            <asp:Label ID="Label7" runat="server" Text="Accumulation"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:CheckBox ID="chbAccumulation" runat="server" Text="+" Width="200px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 125px">
                            <asp:Label ID="Label8" runat="server" Text="Accumulation Value"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtAccumulationValue" runat="server" Width="250px" AutoPostBack="True"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="txtAccumulationValue_AutoCompleteExtender"
                                runat="server"
                                BehaviorID="txtAccumulationValue_AutoCompleteExtender"
                                DelimiterCharacters=""
                                Enabled="True"
                                MinimumPrefixLength="0"
                                ServicePath="~/modules/Payroll/WebService.asmx"
                                ServiceMethod="GetAccumulationValue"
                                TargetControlID="txtAccumulationValue">
                            </ajaxToolkit:AutoCompleteExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 125px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:CheckBox ID="chbManualEntry" runat="server" Text="Manual Entry" Width="200px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 125px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 125px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" Width="100px" />
                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 125px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="grdGetSelectedFormula" runat="server" Width="100%" AutoGenerateColumns="False" OnRowCommand="grdGetSelectedFormula_RowCommand" OnRowDeleting="grdGetSelectedFormula_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <%# Container.DisplayIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfCode" runat="server" Text='<%# Bind("For_Mas_Cal_Code") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfName" runat="server" Text='<%# Bind("For_Mas_Cal_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Base">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfBase" runat="server" Text='<%# Bind("For_Mas_Base_Val") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Operator">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfOperator" runat="server" Text='<%# Bind("For_Mas_Opr") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Multiplier">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfMultiplier" runat="server" Text='<%# Bind("For_Mas_Mul") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Accumulation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfAccumulation" runat="server" Text='<%# Bind("For_Mas_Acc") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Accumulation Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfAccumulationValue" runat="server" Text='<%# Bind("For_Mas_Acc_Code") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Manual Entry">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfManualEntry" runat="server" Text='<%# Bind("T_In") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:CommandField ShowSelectButton="True">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:CommandField>
                                    <asp:CommandField ShowDeleteButton="True">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:CommandField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 125px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 125px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

