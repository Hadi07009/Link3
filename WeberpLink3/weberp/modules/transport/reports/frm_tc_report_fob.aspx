<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_tc_report_fob.aspx.cs" Inherits="frm_tc_report_fob" EnableEventValidation="false" ValidateRequest="false"  %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<%@ Register assembly="TimePicker" namespace="MKB.TimePicker" tagprefix="MKB" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="HTMLEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <table class="tblmas" style="width: 100%; vertical-align:text-top; ">
        <tr>
            <td style="height: 40px">
            </td>
            <td style="height: 25px; text-align: center">
            </td>
        </tr>
        <tr>
            <td class="heading" style="height: 10px; text-align: center">
                FOB DO REPORT</td>
        </tr>
        <tr>
            <td style="height: 25px; text-align: center">
                </td>
            <td style="height: 25px; text-align: center">
            </td>
        </tr>
        <tr>
            <td >
                <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional" >
                    <ContentTemplate>
                        <table style="width:100%;">
                            <tr>
                                <td colspan="3" style="text-align: left">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: left">
                                    <table style="width:100%;">
                                        <tr style="background-color:lightsteelblue">
                                            <td style="width: 155px; text-align: right;"><strong>Report Type</strong></td>
                                            <td style="width: 10px">
                                                :</td>
                                            <td colspan="2">
                                                <asp:RadioButtonList ID="rdotype" runat="server" RepeatDirection="Horizontal" Width="325px">
                                                    <asp:ListItem Selected="True" Value="1">Detail Report</asp:ListItem>
                                                    <asp:ListItem Value="2">Summary Report</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr >
                                            <td style="width: 155px; text-align: right;">&nbsp;</td>
                                            <td style="width: 10px">&nbsp;</td>
                                            <td colspan="2">&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr style="background-color:lightsteelblue ">
                                            <td style="width: 155px; text-align: right;"><b>DO Period</b></td>
                                            <td style="width: 10px">:</td>
                                            <td colspan="2">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="text-align: right; width: 29px">From:</td>
                                                        <td style="width: 138px">
                                                            <ew:CalendarPopup ID="dtstdate" runat="server" CssClass="txtbox" Culture="English (United Kingdom)" DisableTextBoxEntry="False" Width="76px">
                                                                <ButtonStyle CssClass="btn2" />
                                                            </ew:CalendarPopup>
                                                        </td>
                                                        <td style="text-align: right; width: 41px">To:</td>
                                                        <td>
                                                            <ew:CalendarPopup ID="dtfinndate" runat="server" CssClass="txtbox" Culture="English (United Kingdom)" DisableTextBoxEntry="False" Width="76px">
                                                                <ButtonStyle CssClass="btn2" />
                                                            </ew:CalendarPopup>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 155px; text-align: right;">&nbsp;</td>
                                            <td style="width: 10px">&nbsp;</td>
                                            <td colspan="2">&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr style="background-color:lightsteelblue">
                                            <td style="width: 155px; text-align: right;"><b>Report Selection </b></td>
                                            <td style="width: 10px">:</td>
                                            <td colspan="2">
                                                <asp:RadioButtonList ID="rdosel" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdosel_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True">DO Delivery Completed </asp:ListItem>
                                                    <asp:ListItem>DO Delivery All Pending (as on Date)  </asp:ListItem>
                                                    <asp:ListItem Value="DO Status">DO Status</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 155px; text-align: right;">&nbsp;</td>
                                            <td style="width: 10px">&nbsp;</td>
                                            <td colspan="2">
                                                <asp:Label ID="lblstatus" runat="server" Text="Select FOB DO :" style="font-weight: 700" Visible="False"></asp:Label>
                                                &nbsp;<asp:TextBox ID="txttcno" runat="server" Width="573px" Visible="False"></asp:TextBox>
                                                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender11" runat="server" BehaviorID="AutoCompleteEx21"
                                                     CompletionInterval="1000" CompletionListCssClass="autocomplete_completionListElement"
                                                     CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                     CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" DelimiterCharacters=","
                                                     EnableCaching="false" MinimumPrefixLength="3" ServiceMethod="GetFobDo"
                                                     ServicePath="~/services/srvSystem.asmx" ShowOnlyCurrentWordInCompletionListItem="true" 
                                                    TargetControlID="txttcno">
                                                </ajaxToolkit:AutoCompleteExtender>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 155px; text-align: right;"><b></b></td>
                                            <td style="width: 10px">&nbsp;</td>
                                            <td colspan="2">&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr style="background-color:lightsteelblue">
                                            <td rowspan="2" style="width: 155px; text-align: right;"><b>Party/Sales Wing Selection</b></td>
                                            <td rowspan="2" style="width: 10px">:</td>
                                            <td rowspan="2">
                                                <asp:RadioButtonList ID="rdoparty" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdoparty_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True">Party</asp:ListItem>
                                                    <asp:ListItem>Sales Wing</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td>
                                                <table id="tblparty" runat="server"  style="width:100%;">
                                                    <tr>
                                                        <td><b>Party</b></td>
                                                        <td>
                                                            <asp:CheckBox ID="chkpartyall" runat="server" Checked="True" Text="ALL" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtparty" runat="server" autocomplete="off" AutoPostBack="True" CssClass="txtbox" OnTextChanged="txtparty_TextChanged" Width="500px"></asp:TextBox>
                                                            <ajaxToolkit:AutoCompleteExtender ID="autoComplete1" runat="server" BehaviorID="AutoCompleteEx" CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="1" ServiceMethod="GetCustomerAccountCode" ServicePath="~/modules/commercial/services/autocomplete.asmx" ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtparty">
                                                            </ajaxToolkit:AutoCompleteExtender>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td><b>Sub Party</b></td>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlsubparty" runat="server" Height="20px" Width="503px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td rowspan="2">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table id="tblwing" runat="server" style="background-color:lightsteelblue; width:100%;">
                                                    <tr>
                                                        <td><strong>Sales Wing</strong></td>
                                                        <td>
                                                            <asp:CheckBox ID="chkwingall" runat="server" Checked="True" Text="ALL" />
                                                        </td>
                                                        <td>
                                                            <asp:CheckBoxList ID="chkwing" runat="server" RepeatColumns="4" Width="500px">
                                                            </asp:CheckBoxList>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 155px; text-align: right;"><b></b></td>
                                            <td style="width: 10px">&nbsp;</td>
                                            <td colspan="2">&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr style="background-color:lightsteelblue">
                                            <td style="width: 155px; text-align: right;"><b>Report Group By</b></td>
                                            <td style="width: 10px">:</td>
                                            <td colspan="2">
                                                <asp:RadioButtonList ID="rdogrp" runat="server" Width="229px">
                                                    <asp:ListItem Selected="True" Value="1">Party wise</asp:ListItem>                                                    
                                                    <asp:ListItem Value="2">Sales Wing wise</asp:ListItem>                                                    
                                                    <asp:ListItem Value="3">DO Date wise</asp:ListItem>
                                                    
                                                </asp:RadioButtonList>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 155px">&nbsp;</td>
                                            <td style="width: 10px">&nbsp;</td>
                                            <td colspan="2">&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: center">
                                    <asp:Button ID="btnShowPeport" runat="server" Height="24px" OnClick="btnShowPeport_Click" Text="Show Report" Visible="true" Width="166px" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: left">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 34px">
                                    &nbsp;</td>
                                <td style="text-align: left; width: 77px">
                        &nbsp;</td>
                                <td style="text-align: left">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 34px; height: 40px;">
                                </td>
                                <td style="text-align: left; width: 77px; height: 40px">
                                </td>
                                <td style="text-align: left; height: 40px">
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                     <asp:AsyncPostBackTrigger ControlID="rdoparty" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="rdosel" EventName="SelectedIndexChanged" />                        
                        <asp:PostBackTrigger ControlID ="btnShowPeport" />
                        <%-- <asp:PostBackTrigger ControlID="btnUpdate"  />--%>
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    
    </asp:Content>


