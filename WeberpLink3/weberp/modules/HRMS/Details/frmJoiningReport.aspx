<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmJoiningReport.aspx.cs" Inherits="modules_HRMS_Details_frmJoiningReport" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc2:MessageBox ID="MessageBox1" runat="server" />
    <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
        <asp:Label ID="lblleave" Text="Joining Report" runat="server" />
    </asp:Panel>
    <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
        <table style="width: 99%; text-align: left">
            <tr>
                <td style="text-align: right">
                    <asp:Label ID="Label3" runat="server" Text="Select Company"></asp:Label>
                </td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="ddlcompany" runat="server" AutoPostBack="True" CssClass="tbl" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" Width="310px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label1" runat="server" Text="From Date "></asp:Label>
                </td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtFromDate0" runat="server" Width="178px"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtFromDate0_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFromDate0">
                    </cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label2" runat="server" Text="To Date"></asp:Label>
                </td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtToDate" runat="server" Width="178px"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtToDate">
                    </cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td class="auto-style64"></td>
                <td class="auto-style64"></td>
                <td class="auto-style64">
                    <asp:Button ID="btnJoining" runat="server" OnClick="btnJoining_Click" Text="Joining Information" Width="183px" CssClass="btn2" />
                    <asp:Button ID="btnExportJoinResign" runat="server" CssClass="btn2" OnClick="btnExportJoinResign_Click" Text="Export" Width="183px" />
                    <asp:Button ID="btnJoiningReport" runat="server" OnClick="btnJoiningReport_Click" Text="Joining Report" Width="183px" CssClass="btn2" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:GridView ID="grdGetJoiningNdResignation" runat="server" AutoGenerateColumns="False" OnRowDataBound="grdGetJoiningNdResignation_RowDataBound" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Row">
                                <ItemTemplate>
                                    <%# Container.DisplayIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EID" HeaderText="Emp Id" />
                            <asp:BoundField DataField="Employee Name" HeaderText="Name" />
                            <asp:BoundField DataField="Department" HeaderText="Department" />
                            <asp:BoundField DataField="Designation" HeaderText="Designation" />
                            <asp:BoundField DataField="Joining Date" HeaderText="Joining Date" />
                            <asp:BoundField DataField="Confirm Date" HeaderText="Confirm Date" />
                            <asp:BoundField DataField="Accept Date" HeaderText="Accept Date" />
                            <asp:BoundField DataField="Release Date" HeaderText="Release Date" />
                            <asp:BoundField DataField="Settlement Type" HeaderText="Status" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </asp:Panel>

</asp:Content>
