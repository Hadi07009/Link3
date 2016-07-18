<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_item_stock.aspx.cs" Inherits="frm_item_stock" Title=""   %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
<table id="tblmaster" runat="server" class="tblmas" style="width: 100%">
        <tr>
            <td style="height: 22px">
            </td>
        </tr>
        <tr>
            <td class="heading" style="text-align: center">
                ITEM STOCK DETAIL&nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" style="height: 24px; text-align: center">
                            <table style="width:100%;">
                                <tr>
                                    <td style="width: 54px; text-align: left">
                                        &nbsp;</td>
                                    <td style="width: 111px; text-align: left">
                                        &nbsp;</td>
                                    <td style="width: 11px">
                                        &nbsp;</td>
                                    <td colspan="4" style="text-align: left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 54px; text-align: left">
                                        &nbsp;</td>
                                    <td style="width: 111px; text-align: left">
                                        ITEM CODE/NAME</td>
                                    <td style="width: 11px">
                                        :</td>
                                    <td colspan="4" style="text-align: left">
                                        <asp:TextBox ID="txtitem" runat="server" autocomplete="off" CssClass="txtbox" 
                                            Width="600px">
                                </asp:TextBox>
                                        <ajaxToolkit:AutoCompleteExtender ID="autoComplete1" runat="server" 
                                            BehaviorID="AutoCompleteEx2" CompletionInterval="1000" 
                                            CompletionListCssClass="autocomplete_completionListElement" 
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                                            CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" 
                                            DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="2" 
                                            ServiceMethod="GetIStockDet" ServicePath="~/modules/commercial/services/autocomplete.asmx" 
                                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtitem">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 54px; text-align: left">
                                        &nbsp;</td>
                                    <td style="width: 111px; text-align: left">
                                        &nbsp;</td>
                                    <td style="width: 11px">
                                        &nbsp;</td>
                                    <td colspan="4" style="text-align: left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 54px; text-align: left;">
                                        &nbsp;</td>
                                    <td style="width: 111px; text-align: left;">
                                        STORE</td>
                                    <td style="width: 11px">
                                        :</td>
                                    <td style="text-align: left;" colspan="4">
                                        <asp:DropDownList ID="ddlplantlist" runat="server" CssClass="txtbox" 
                                            Width="300px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 54px">
                                        &nbsp;</td>
                                    <td style="width: 111px">
                                        &nbsp;</td>
                                    <td style="width: 11px">
                                        &nbsp;</td>
                                    <td style="width: 151px">
                                        &nbsp;</td>
                                    <td style="width: 36px">
                                        &nbsp;</td>
                                    <td style="width: 11px">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 54px">
                                        &nbsp;</td>
                                    <td style="width: 111px">
                                        &nbsp;</td>
                                    <td style="width: 11px">
                                        &nbsp;</td>
                                    <td style="width: 151px">
                                        &nbsp;</td>
                                    <td style="width: 36px">
                                        &nbsp;</td>
                                    <td style="width: 11px">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 54px">
                                        &nbsp;</td>
                                    <td style="width: 111px">
                                        &nbsp;</td>
                                    <td style="width: 11px">
                                        &nbsp;</td>
                                    <td style="width: 151px; ">
                                        <asp:Button ID="btnview" runat="server" CssClass="btn2" onclick="btnview_Click" 
                                            Text="View All" Width="168px" />
                                    </td>
                                    <td style="width: 36px">
                                        &nbsp;</td>
                                    <td style="width: 11px">
                                        &nbsp;</td>
                                    <td>
                                        
                                        &nbsp;</td>
                                    <td style="width: 54px">
                                        &nbsp;</td>
                                    <td style="width: 111px">
                                        &nbsp;</td>
                                    <td style="width: 11px">
                                        &nbsp;</td>
                                    <td style="width: 151px; text-align: left">
                                        &nbsp;</td>
                                    <td style="width: 36px">
                                        &nbsp;</td>
                                    <td style="width: 11px">
                                        &nbsp;</td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
            </td>
        </tr>
    <tr>
        <td class="tbl" style="text-align: left">
            <div style="text-align: center">
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                    HasCrystalLogo="False" HasToggleGroupTreeButton="False" Height="50px" 
                    ReuseParameterValuesOnRefresh="True" style="text-align: left" 
                    ToolbarStyle-BorderStyle="None" Width="350px" ToolPanelView="None" 
                    onunload="CrystalReportViewer1_Unload" />
            </div>
        </td>
    </tr>
        <tr>
            <td>
                
                
                &nbsp;</td>
        </tr>
        <tr>
            <td>
            
                 &nbsp;</td>
        </tr>
        <tr>
            <td>
                 &nbsp;</td>
        </tr>
        <tr>
            <td style="height: 92px">
            </td>
        </tr>
    </table>
                    
</asp:Content>

