<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_quotation_report.aspx.cs" Inherits="frm_quotation_report"  Title=""   ValidateRequest="false" EnableEventValidation="false"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  

    <table id="tblmaster" runat="server" class="tblmas" style="width: 100%">
        <tr>
            <td style="width: 99px">
                &nbsp;</td>
            <td style="width: 65px">
                &nbsp;</td>
            <td style="width: 9px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="5" class="heading" style="text-align: center">
                QUOTATION REPORT SCREEN</td>
        </tr>
        <tr>
            <td style="width: 99px">
                &nbsp;</td>
            <td style="width: 65px">
                &nbsp;</td>
            <td style="width: 9px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 99px; height: 22px; text-align: right;">
                &nbsp;</td>
            <td style="width: 65px; height: 22px; text-align: right;">
                SUPPLIER</td>
            <td style="width: 9px; height: 22px; text-align: right;">
                :</td>
            <td style="width: 84px; height: 22px">
            <asp:TextBox ID="txtpartydet" runat="server" 
                CssClass="txtbox" autocomplete="off" 
                    Width="499px"></asp:TextBox>
                     <ajaxToolkit:AutoCompleteExtender
                                runat="server"                                
                                ID="autoComplete1" 
                                BehaviorID="AutoCompleteEx"                                
                                TargetControlID="txtpartydet"
                                ServicePath="services/autocomplete.asmx" 
                                ServiceMethod="GetPartyAdrList"
                                MinimumPrefixLength="1" 
                                CompletionInterval="100"
                                EnableCaching="false"
                                CompletionSetCount="20"                                 
                                CompletionListCssClass ="autocomplete_completionListElement"                            
                                CompletionListItemCssClass="autocomplete_listItem" 
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                DelimiterCharacters=","
                                ShowOnlyCurrentWordInCompletionListItem="true" > 
                            </ajaxToolkit:AutoCompleteExtender>
                </td>
            <td style="width: 174px; height: 22px; text-align: left;">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 99px; text-align: left;">
                &nbsp;</td>
            <td style="width: 65px; text-align: right;">
                &nbsp;</td>
            <td style="width: 9px; text-align: right;">
                &nbsp;</td>
            <td style="text-align: center">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 99px; text-align: left;">
                &nbsp;</td>
            <td style="width: 65px; text-align: center;">
                ITEM</td>
            <td style="width: 9px; text-align: left;">
                :</td>
            <td>
                <asp:TextBox ID="txtitem" runat="server" 
                                autocomplete="off"  CssClass="txtbox" Width="500px" >
                                </asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender
                                runat="server"                                
                                ID="AutoCompleteExtender1" 
                                BehaviorID="AutoCompleteEx2"                                
                                TargetControlID="txtitem"
                                ServicePath="services/autocomplete.asmx" 
                                ServiceMethod="GetItemList"
                                MinimumPrefixLength="1" 
                                CompletionInterval="1000"
                                EnableCaching="false"
                                CompletionSetCount="20"                                 
                                CompletionListCssClass ="autocomplete_completionListElement"                            
                                CompletionListItemCssClass="autocomplete_listItem" 
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                DelimiterCharacters=","
                                ShowOnlyCurrentWordInCompletionListItem="true" > 
                            </ajaxToolkit:AutoCompleteExtender>
                </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 99px">
                &nbsp;</td>
            <td style="width: 65px">
                &nbsp;</td>
            <td style="width: 9px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 99px">
                &nbsp;</td>
            <td style="width: 65px">
                &nbsp;</td>
            <td style="width: 9px">
                &nbsp;</td>
            <td>
                <asp:Button ID="btnview" runat="server" CssClass="btn2" onclick="btnview_Click" 
                    Text="View" Width="92px" />
                </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 99px">
                &nbsp;</td>
            <td style="width: 65px">
                &nbsp;</td>
            <td style="width: 9px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:UpdatePanel ID="updpnl" runat="server">
<ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <asp:GridView ID="gdItem" runat="server" BackColor="White" 
                                BorderColor="#41519A" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                                ForeColor="#333333" GridLines="None" PageSize="100" SkinID="GridView" OnRowCommand="gdItem_RowCommand"  OnSorting="gdItem_Sorting" 
                                style="border-top-width: 1px; border-left-width: 1px; border-left-color: #e6e6fa; border-bottom-width: 1px; border-bottom-color: #e6e6fa; border-top-color: #e6e6fa; border-right-width: 1px; border-right-color: #e6e6fa;" 
                                Width="98%" AllowSorting="True">
                                <FooterStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#41519A" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle Font-Bold="True" />
                                <HeaderStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="Lavender" Font-Size="8pt" 
                                    Font-Underline="False" />
                                <RowStyle Font-Size="8pt" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                </ContentTemplate>
    <Triggers>
   <asp:AsyncPostBackTrigger ControlID="btnview" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>

                </td>
        </tr>
        <tr>
            <td style="width: 99px">
                &nbsp;</td>
            <td style="width: 65px">
                &nbsp;</td>
            <td style="width: 9px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 99px">
                &nbsp;</td>
            <td style="width: 65px">
                &nbsp;</td>
            <td style="width: 9px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    
</asp:Content>

