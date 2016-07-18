<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_stock_ledger_report.aspx.cs" Inherits="frm_stock_ledger_report" Title=""   %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>


<%@ Register assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2" namespace="eWorld.UI" tagprefix="ew" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <table id="tblmaster" runat="server" class="tblmas" style="width: 100%">
        <tr>
            <td style="height: 22px">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="heading" style="text-align: center">
                STORE LEDGER REPORT</td>
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
                            <asp:CheckBox ID="chkall" runat="server" Text="Select All Items" 
                                TextAlign="Left" Checked="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 54px; text-align: left">
                                        &nbsp;</td>
                        <td style="width: 111px; text-align: left">
                                        FROM ITEM</td>
                        <td style="width: 11px">
                                        :</td>
                        <td colspan="4" style="text-align: left">
                            <asp:TextBox ID="txtitemfrom" runat="server" 
                                autocomplete="off"  CssClass="txtbox" Width="500px" >
                                </asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender
                                runat="server"                                
                                ID="autoComplete1" 
                                BehaviorID="AutoCompleteEx2"                                
                                TargetControlID="txtitemfrom"
                                ServicePath="../../commercial/services/autocomplete.asmx" 
                                ServiceMethod="GetItemList2"
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
                    </tr>
                    <tr>
                        <td style="width: 54px; text-align: left">
                                        &nbsp;</td>
                        <td style="width: 111px; text-align: left">
                                        TO ITEM</td>
                        <td style="width: 11px">
                                        :</td>
                        <td colspan="4" style="text-align: left">
                            <asp:TextBox ID="txtitemto" runat="server" 
                                autocomplete="off"  CssClass="txtbox" Width="500px" >
                                </asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender
                                runat="server"                                
                                ID="txtitemto_AutoCompleteExtender" 
                                BehaviorID="AutoCompleteEx3"                                
                                TargetControlID="txtitemto"
                                ServicePath="../../commercial/services/autocomplete.asmx" 
                                ServiceMethod="GetItemList2"
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
                        <td style="width: 54px; text-align: left">
                                        &nbsp;</td>
                        <td style="width: 111px; text-align: left">
                                        DATE FROM</td>
                        <td style="width: 11px">
                                        :</td>
                        <td colspan="4" style="text-align: left">
                            <ew:CalendarPopup ID="cldfrom" runat="server" 
                                            Culture="English (United Kingdom)" Width="85px" 
                                            DisableTextBoxEntry="False">
                                <ButtonStyle CssClass="btn2" />
                            </ew:CalendarPopup>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 54px; text-align: left">
                                        &nbsp;</td>
                        <td style="width: 111px; text-align: left">
                                        DATE TO</td>
                        <td style="width: 11px">
                                        :</td>
                        <td colspan="4" style="text-align: left">
                            <ew:CalendarPopup ID="cldto" runat="server" 
                                            Culture="English (United Kingdom)" Width="87px" 
                                            DisableTextBoxEntry="False">
                                <ButtonStyle CssClass="btn2" />
                            </ew:CalendarPopup>
                        &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 54px; text-align: left">
                                        &nbsp;</td>
                        <td style="width: 111px; text-align: left">
                                        &nbsp;</td>
                        <td style="width: 11px">
                                        &nbsp;</td>
                        <td colspan="4" style="text-align: left">
                            <asp:RadioButtonList ID="RdoList" runat="server" RepeatDirection="Horizontal" 
                                Width="513px" Visible="true">
                                <asp:ListItem Value="0" Selected="True"> Legder</asp:ListItem>
                                <asp:ListItem Value="1">Value</asp:ListItem>
                                <asp:ListItem Value="2">Quantity</asp:ListItem>
                                <asp:ListItem Value="3">Value &amp; Quantity</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 54px; text-align: left">
                                        &nbsp;</td>
                        <td style="width: 111px; text-align: left">
                                        ITEM TYPE</td>
                        <td style="width: 11px">
                                        :</td>
                        <td colspan="4" style="text-align: left">
                            <asp:CheckBoxList ID="chktype" runat="server" RepeatColumns="3" Width="400px">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 54px; text-align: left;">
                                        &nbsp;</td>
                        <td style="width: 111px; text-align: left;">
                            STORE</td>
                        <td style="width: 11px">
                                        :</td>
                        <td style="text-align: left;" colspan="4">
                            <asp:UpdatePanel runat="server" ID="updrpt" >
                                <ContentTemplate>
                                    <asp:CheckBoxList ID="chkstore" runat="server" CssClass="txtbox" 
                                            Width="300px"  >
                                    </asp:CheckBoxList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
                        <td style="width: 11px" >
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
                        <td  style="width: 11px">
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
                                            Text="View" Width="128px" />
                        </td>
                        <td style="width: 36px">
                                        &nbsp;</td>
                        <td  style="width: 11px">
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
                        <td  style="width: 11px">
                                        &nbsp;</td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tbl" style="text-align: left">
            &nbsp;</td>
        </tr>
        <tr>
            <td>
                
                
            <div style="text-align: center">
            <asp:UpdatePanel id="updcr" runat="server">            
            <ContentTemplate>
            
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                    HasCrystalLogo="False" HasToggleGroupTreeButton="False" Height="50px" style="text-align: left" 
                    ToolbarStyle-BorderStyle="None" Width="350px" ToolPanelView="None" 
                    onunload="CrystalReportViewer1_Unload" PrintMode="ActiveX" />
            </ContentTemplate>

            <Triggers>
            <asp:PostBackTrigger ControlID="btnview"   />
            </Triggers>
                    </asp:UpdatePanel>
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
            <td style="height: 92px">
            </td>
        </tr>
    </table>
                    
</asp:Content>

