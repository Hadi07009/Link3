<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_issue_report.aspx.cs" Inherits="frm_issue_report" Title=""   %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>


<%@ Register assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2" namespace="eWorld.UI" tagprefix="ew" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
<table id="tblmaster" runat="server" class="tblmas" style="width: 100%">
        <tr>
            <td style="height: 22px">
            </td>
        </tr>
        <tr>
            <td class="heading" style="text-align: center">
                Fleet Wise Material Consumption Report</td>
        </tr>
        <tr>
            <td class="tbl" style="height: 24px; text-align: center">
                            <table style="width:100%;">
                                <tr>
                                    <td style="width: 54px; text-align: left">
                                        &nbsp;</td>
                                    <td style="width: 134px; text-align: left">
                                        &nbsp;</td>
                                    <td style="width: 11px">
                                        &nbsp;</td>
                                    <td colspan="4" style="text-align: left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 54px; text-align: left">
                                        &nbsp;</td>
                                    <td style="width: 134px; text-align: right">
                                        DATE FROM</td>
                                    <td style="width: 11px">
                                        :</td>
                                    <td colspan="4" style="text-align: left">
                                        <ew:CalendarPopup ID="dtstdate" runat="server" 
                                            Culture="English (United Kingdom)" Width="85px" 
                                            DisableTextBoxEntry="False">
                                            <ButtonStyle CssClass="btn2" />
                                        </ew:CalendarPopup>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 54px; text-align: left">
                                        &nbsp;</td>
                                    <td style="width: 134px; text-align: right">
                                        DATE TO</td>
                                    <td style="width: 11px">
                                        :</td>
                                    <td colspan="4" style="text-align: left">
                                        <ew:CalendarPopup ID="dtfinndate" runat="server" 
                                            Culture="English (United Kingdom)" Width="87px" 
                                            DisableTextBoxEntry="False">
                                            <ButtonStyle CssClass="btn2" />
                                        </ew:CalendarPopup>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 54px; text-align: left">
                                        &nbsp;</td>
                                    <td style="width: 134px; text-align: right">
                                        SELECT ALL VEHICLE</td>
                                    <td style="width: 11px">
                                        :</td>
                                    <td colspan="4" style="text-align: left">
                            <asp:CheckBox ID="chkall" runat="server" Text="" 
                                TextAlign="Left" Checked="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 54px; text-align: left">
                                        &nbsp;</td>
                                    <td style="width: 134px; text-align: right">
                                        FROM VEHICLE NO</td>
                                    <td style="width: 11px">
                                        :</td>
                                    <td colspan="4" style="text-align: left">
                                                <asp:TextBox ID="txtvihiclelist" runat="server" AutoCompleteType="None" 
                                                    
                                                    Width="350px"  ></asp:TextBox>
                                                <ajaxToolkit:AutoCompleteExtender ID="autoComplete1" runat="server" 
                                                    BehaviorID="AutoCompleteEx2" CompletionInterval="1000" 
                                                    CompletionListCssClass="autocomplete_completionListElement" 
                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" 
                                                    DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="3" 
                                                    ServiceMethod="GetVehicleSearch" ServicePath="~/services/srvSystem.asmx" 
                                                    ShowOnlyCurrentWordInCompletionListItem="true" 
                                                    TargetControlID="txtvihiclelist">
                                                </ajaxToolkit:AutoCompleteExtender>
                                            </td>
                                </tr>
                                <tr>
                                    <td style="width: 54px; text-align: left">
                                        &nbsp;</td>
                                    <td style="width: 134px; text-align: right">
                                        TO VEHICLE NO</td>
                                    <td style="width: 11px">
                                        :</td>
                                    <td colspan="4" style="text-align: left">
                                                <asp:TextBox ID="txtvihiclelistto" runat="server" AutoCompleteType="None" 
                                                    
                                                    Width="350px"  ></asp:TextBox>
                                                <ajaxToolkit:AutoCompleteExtender ID="txtvihiclelistto_AutoCompleteExtender" runat="server" 
                                                    BehaviorID="AutoCompleteEx3" CompletionInterval="1000" 
                                                    CompletionListCssClass="autocomplete_completionListElement" 
                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" 
                                                    DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="3" 
                                                    ServiceMethod="GetVehicleSearch" ServicePath="~/services/srvSystem.asmx" 
                                                    ShowOnlyCurrentWordInCompletionListItem="true" 
                                                    TargetControlID="txtvihiclelistto">
                                                </ajaxToolkit:AutoCompleteExtender>
                                            </td>
                                </tr>
                                <tr>
                                    <td style="width: 54px">
                                        &nbsp;</td>
                                    <td style="width: 134px">
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
                                    <td style="width: 134px">
                                        &nbsp;</td>
                                    <td style="width: 11px">
                                        &nbsp;</td>
                                    <td style="width: 151px; ">
                                        <asp:Button ID="btnview" runat="server" CssClass="btn2" onclick="btnview_Click" 
                                            Text="View" Width="128px" />
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
                                <iframe ID="iframe" runat="server" name="I1" 
                                    src="frm_rpt_viewer.aspx" 
                                    style="width: 900px; height: 1300px" __designer:mapid="1b7"></iframe>
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

