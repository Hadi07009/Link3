<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_movement_report_fob.aspx.cs" Inherits="frm_movement_report_fob" EnableEventValidation="false" ValidateRequest="false"  %>
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
                FOB
                MOVEMENT ORDER REPORT</td>
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
                                <td colspan="3" style="text-align: left;">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <table style="width:100%;">
                                        <tr>
                                            <td style="width: 149px; text-align: right; font-size: x-small;"><b>Movement Type:</b></td>
                                            <td>
                                                <asp:CheckBoxList ID="chkmotype" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="True" Value="Y">Posted</asp:ListItem>
                                                    <asp:ListItem Value="N">Canceled</asp:ListItem>
                                                </asp:CheckBoxList>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 149px; text-align: right; font-size: x-small;">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 149px; text-align: right; font-size: x-small;"><b>Report Type:</b></td>
                                            <td>
                                                <asp:RadioButtonList ID="rdotype" runat="server" Width="362px" RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="True" Value="1">Detail</asp:ListItem>
                                                    <asp:ListItem Value="2">Item wise Summary</asp:ListItem>
                                                    <asp:ListItem Value="3">Party wise Summery</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 149px; text-align: right; font-size: x-small;">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 149px; text-align: right; font-size: x-small;"><b>Period:</b></td>
                                            <td>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="text-align: right; width: 76px">From:</td>
                                                        <td style="width: 138px">
                                                            <ew:CalendarPopup ID="dtstdate" runat="server" CssClass="txtbox" Culture="English (United Kingdom)" DisableTextBoxEntry="False" Width="76px">
                                                                <ButtonStyle CssClass="btn2" />
                                                            </ew:CalendarPopup>
                                                        </td>
                                                        <td style="text-align: right; width: 41px">To:</td>
                                                        <td>&nbsp;<ew:CalendarPopup ID="dtfinndate" runat="server" CssClass="txtbox" Culture="English (United Kingdom)" DisableTextBoxEntry="False" Width="76px">
                                                                <ButtonStyle CssClass="btn2" />
                                                            </ew:CalendarPopup>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 76px; text-align: right;">
                                                            <asp:CheckBox ID="chktime" runat="server" Text="With Time" TextAlign="Left" Checked="True">
                                                                                                                          
                                                            </asp:CheckBox>
                                                        </td>
                                                        <td style="width: 138px">
                                                            <MKB:TimeSelector ID="tssttime" runat="server" AmPm="AM" Date="2013-02-27" DisplaySeconds="False" Hour="6">
                                                            </MKB:TimeSelector>
                                                        </td>
                                                        <td style="text-align: right; width: 41px">&nbsp;</td>
                                                        <td>
                                                            <MKB:TimeSelector ID="tsendtime" runat="server" AmPm="AM" Date="2013-02-27" DisplaySeconds="False" Hour="6">
                                                            </MKB:TimeSelector>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 149px; text-align: right; font-size: x-small;">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 149px; text-align: right; font-size: x-small;"><strong>Period On:</strong></td>
                                            <td>
                                                <asp:RadioButtonList ID="rdoperiodon" runat="server" RepeatDirection="Horizontal" Width="325px">
                                                    <asp:ListItem Selected="True" Value="1">Movement Date</asp:ListItem>
                                                    <asp:ListItem Value="2">Gate Out Date </asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 149px; text-align: right; font-size: x-small;">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 149px; text-align: right; font-size: x-small;"><b>Selection Criteria:</b></td>
                                            <td>
                                                <table style="width:100%;">
                                                    <tr>
                                                        <td rowspan="3">
                                                            <asp:RadioButtonList ID="rdocat" runat="server" Width="139px" AutoPostBack="True" OnSelectedIndexChanged="rdocat_SelectedIndexChanged">
                                                                <asp:ListItem Selected="True" Value="1">Party wise</asp:ListItem>
                                                                <asp:ListItem Value="3">Date wise</asp:ListItem>
                                                                <asp:ListItem Value="4">MOV Ref</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td>
                                                            <table id="tbl_party" runat="server" style="width:100%;">
                                                                <tr>
                                                                    <td style="width: 53px; text-align: right;" rowspan="2">
                                                                        <asp:CheckBox ID="chkpartyall" runat="server" Checked="True" Text="ALL" />
                                                                    </td>
                                                                    <td style="width: 89px; text-align: right;">Party From:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtpartyfrom" runat="server" autocomplete="off" CssClass="txtbox"  Width="500px"></asp:TextBox>
                                                                        <ajaxToolkit:AutoCompleteExtender ID="txtpartyfrom_AutoCompleteExtender" runat="server" BehaviorID="AutoCompleteEx" CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="1" ServiceMethod="GetCustomerAccountCode" ServicePath="~/modules/commercial/services/autocomplete.asmx" ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtpartyfrom">
                                                                        </ajaxToolkit:AutoCompleteExtender>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 89px; text-align: right;">Party To:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtpartyto" runat="server" autocomplete="off" CssClass="txtbox" Width="500px"></asp:TextBox>
                                                                        <ajaxToolkit:AutoCompleteExtender ID="txtpartyto_AutoCompleteExtender" runat="server" BehaviorID="AutoCompleteEx1" CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="1" ServiceMethod="GetCustomerAccountCode" ServicePath="~/modules/commercial/services/autocomplete.asmx" ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtpartyto">
                                                                        </ajaxToolkit:AutoCompleteExtender>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table id="tbl_movement" runat="server" style="width:100%;">
                                                                <tr>
                                                                    <td style="width: 54px; text-align: right;">
                                                                        <asp:CheckBox ID="chkmovall" runat="server" Checked="True" Text="ALL" />
                                                                    </td>
                                                                    <td style="width: 89px; text-align: right;">Mov Ref No:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtmofrom" runat="server" autocomplete="off" CssClass="txtbox" Width="500px"></asp:TextBox>
                                                                        <ajaxToolkit:AutoCompleteExtender ID="txtmofrom_AutoCompleteExtender" runat="server" BehaviorID="AutoCompleteEx4" CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="1" ServiceMethod="GetFobMovement" ServicePath="~/services/srvSystem.asmx" ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtmofrom">
                                                                        </ajaxToolkit:AutoCompleteExtender>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">&nbsp;</td>
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
                     <asp:AsyncPostBackTrigger ControlID="rdocat" EventName="SelectedIndexChanged" />
                        <asp:PostBackTrigger ControlID ="btnShowPeport" />
                        
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    
    </asp:Content>

