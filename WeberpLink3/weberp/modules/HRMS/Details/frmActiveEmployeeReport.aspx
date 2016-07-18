<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmActiveEmployeeReport.aspx.cs" Inherits="modules_HRMS_Details_frmActiveEmployeeReport" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc2:MessageBox ID="MessageBox1" runat="server" />

    <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
        <asp:Label ID="lblleave" Text="Active Employee Report" runat="server" />
    </asp:Panel>
    <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
        <table style="width: 99%; text-align: left">
            <tr>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label1" runat="server" Text="To Date"></asp:Label>
                </td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtToDateForActiveEmp" runat="server" Width="212px"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtToDateForActiveEmp_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtToDateForActiveEmp">
                    </cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td>
                    <asp:Button ID="btnActiveEmp" runat="server" CssClass="btn2" OnClick="btnActiveEmp_Click" Text="Show Active Employee Information" Width="215px" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td>
                    <asp:Button ID="btnExportActiveEmployee" runat="server" CssClass="btn2" OnClick="btnExportActiveEmployee_Click" Text="Export" Width="215px" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td>
                    <asp:Button ID="btnReportForActiveEmployee" runat="server" CssClass="btn2" OnClick="btnReportForActiveEmployee_Click" Text="Report" Width="215px" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="3">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:GridView ID="grdGetActiveEmp" runat="server" AllowSorting="True" AutoGenerateColumns="False" OnRowDataBound="grdGetActiveEmp_RowDataBound" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Row">
                                            <ItemTemplate>
                                                <%# Container.DisplayIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EmpID" HeaderText="Emp ID" />
                                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                        <asp:BoundField DataField="Department" HeaderText="Department" />
                                        <asp:BoundField DataField="Section" HeaderText="Section" />
                                        <asp:BoundField DataField="Office Location" HeaderText="Office Location" />
                                        <asp:BoundField DataField="Joining Date" HeaderText="Joining Date" />
                                        <asp:BoundField DataField="Confirm Date" HeaderText="Confirm Date" />
                                        <asp:BoundField DataField="DOB" HeaderText="DOB" />
                                        <asp:BoundField DataField="Email" HeaderText="Email" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
            </tr>
        </table>
    </asp:Panel>

</asp:Content>
