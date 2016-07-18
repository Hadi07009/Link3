<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmSalaryCertificateReport.aspx.cs" Inherits="modules_HRMS_Details_frmSalaryCertificateReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
        SALARY CERTIFICATE REPORT
    </asp:Panel>


            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
                    <tr>
                        <td style="width: 182px; text-align: right">
                            <asp:Label ID="Label2" runat="server" Text="Select Company"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlcompany" runat="server" AutoPostBack="True" CssClass="tbl" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" Width="380px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 182px; text-align: right">
                            <asp:Label ID="Label6" runat="server" Text="Employee Name"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtemployeeSearch" CssClass="btn2" runat="server" Width="367px"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="txtemployeeSearch_AutoComplxtender" runat="server"
                                BehaviorID="txtemployeeSearch_Autopxtender"
                                Enabled="True" MinimumPrefixLength="1"
                                ServiceMethod="GetEmpId"
                                ServicePath="~/modules/Payroll/WebService.asmx"
                                CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocolete_highlightedListItem"
                                CompletionListItemCssClass="autocolete_listItem2"
                                TargetControlID="txtemployeeSearch">
                            </ajaxToolkit:AutoCompleteExtender>

                        </td>
                    </tr>
                    <tr>
                        <td style="width: 182px; text-align: right">
                            <asp:Label ID="Label3" runat="server" Text="From Date"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <ew:CalendarPopup ID="txtFromDate" runat="server" CssClass="txtbox" Culture="English (United Kingdom)" Enabled="true" Width="150px">
                                <MonthHeaderStyle BackColor="#2A2965" />
                                <ButtonStyle CssClass="btn2" />
                            </ew:CalendarPopup>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 182px; text-align: right">
                            <asp:Label ID="Label4" runat="server" Text="To Date"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <ew:CalendarPopup ID="txtToDate" runat="server" CssClass="txtbox" Culture="English (United Kingdom)" Enabled="true" Width="150px">
                                <MonthHeaderStyle BackColor="#2A2965" />
                                <ButtonStyle CssClass="btn2" />
                            </ew:CalendarPopup>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 182px; text-align: right">
                            <asp:Label ID="Label7" runat="server" Text="Regards"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtregards" runat="server" CssClass="btn2" TextMode="MultiLine" Width="367px" Text="Md. Nasir Reza		
Deputy General Manager, Finance & Accounts		
"  Height="73px"></asp:TextBox>
                           
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 182px">&nbsp;</td>
                        <td align="center">&nbsp;</td>
                        <td>
                            <asp:Button ID="btnTaxReportShow" runat="server" CssClass="btn2" OnClick="btnTaxReportShow_Click" Text="Preview Report" Width="179px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 182px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 182px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 182px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
      

</asp:Content>
