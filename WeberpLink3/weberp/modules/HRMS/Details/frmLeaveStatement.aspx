<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmLeaveStatement.aspx.cs" Inherits="modules_HRMS_Details_frmLeaveStatement" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="Leave Statement" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
                    <tr>
                        <td style="width: 99px">
                            <asp:Label ID="Label2" runat="server" Text="Office Location"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <div style="OVERFLOW-Y: scroll; WIDTH: 380px; HEIGHT: 175px; border: 1px solid; border-color: #669999; border-style: Ridge">

                                <asp:CheckBoxList ID="chkofficelocation" runat="server">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 99px">
                            <asp:Label ID="Label4" runat="server" Text="Department"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:DropDownList ID="ddlDepartmentId" runat="server" AutoPostBack="True" Width="385px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 99px">
                            <asp:Label ID="Label11" runat="server" Text="Employee Category"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:DropDownList ID="ddlEmpCategory" runat="server" AutoPostBack="True" Width="385px">
                                <asp:ListItem Value="-1">ALL</asp:ListItem>
                                <asp:ListItem>Officer</asp:ListItem>
                                <asp:ListItem>Staff</asp:ListItem>
                                <asp:ListItem>Worker</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 99px">
                            <asp:Label ID="Label6" runat="server" Text="Employee ID"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:TextBox ID="txtEmpId" runat="server" AutoCompleteType="None" CssClass="btn2" Width="380px"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="txtEmpId_AutoCompleteExtender" runat="server" CompletionListCssClass="autocomplete_completionListElement" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem2" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetEmpId" ServicePath="~/modules/Payroll/WebService.asmx" TargetControlID="txtEmpId">
                            </ajaxToolkit:AutoCompleteExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 99px">
                            <asp:Label ID="Label1" runat="server" Text="As of Date"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="popupFromDate" runat="server" placeholder="Date To" Width="380px"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="popupFromDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="popupFromDate">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 99px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 99px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 99px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 99px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 99px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShow" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
