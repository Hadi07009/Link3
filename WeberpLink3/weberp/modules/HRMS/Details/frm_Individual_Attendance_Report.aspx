<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_Individual_Attendance_Report.aspx.cs" Inherits="modules_HRMS_Details_frm_Individual_Attendance_Report" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <style type="text/css">
        .cpHeader {
            color: white;
            background-color: #719DDB;
            font: bold 11px auto "Trebuchet MS", Verdana;
            font-size: 12px;
            cursor: pointer;
            height: 18px;
            padding: 4px;
        }

        .cpBody {
            background-color: #DCE4F9;
            font: normal 12px auto "Trebuchet MS";
            border: 1px gray;
            padding: 4px;
            padding-top: 2px;
            height: 0px;
            overflow: hidden;
        }

        .auto-style4 {
            height: 35px;
        }
    </style>
    <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
        ATTENDANCE &amp; LATE REPORT
    </asp:Panel>
    <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
        <table style="width: 99%; text-align: left">
            <tr>
                <td style="width: 115px">
                    <asp:Label ID="Label1" runat="server" Text="Select Company"></asp:Label>
                </td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="ddlcompany" runat="server" CssClass="tbl" Width="385px"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Date From"></asp:Label>
                </td>
                <td>:</td>
                <td>
                    <ew:CalendarPopup ID="txtFromDate" runat="server" CssClass="txtbox" Culture="English (United Kingdom)" Enabled="true" Width="100px">
                        <MonthHeaderStyle BackColor="#2A2965" />
                        <ButtonStyle CssClass="btn2" />
                    </ew:CalendarPopup>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Date To"></asp:Label>
                </td>
                <td>:</td>
                <td>
                    <ew:CalendarPopup ID="txtToDate" runat="server" CssClass="txtbox" Culture="English (United Kingdom)" Enabled="true" Width="100px">
                        <MonthHeaderStyle BackColor="#2A2965" />
                        <ButtonStyle CssClass="btn2" />
                    </ew:CalendarPopup>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="Office Location"></asp:Label>
                </td>
                <td>:</td>
                <td>
                    <div style="OVERFLOW-Y: scroll; WIDTH: 380px; HEIGHT: 175px; border: 1px solid; border-color: #669999; border-style: Ridge">

                        <asp:CheckBoxList ID="chkofficelocation" runat="server">
                        </asp:CheckBoxList>
                    </div>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Department"></asp:Label>
                </td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="ddlDepartmentId" runat="server" AutoPostBack="True" Width="385px">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlShift" runat="server" AutoPostBack="True" Visible="False" Width="180px">
                        <asp:ListItem Value="1">Morning</asp:ListItem>
                        <asp:ListItem Value="2">Evening</asp:ListItem>
                        <asp:ListItem Value="3">Night</asp:ListItem>
                        <asp:ListItem Value="4">Weekend</asp:ListItem>
                        <asp:ListItem Value="5">Gen</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="Employee Category"></asp:Label>
                </td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="ddlEmpCategory" runat="server" AutoPostBack="True" Width="385px">
                        <asp:ListItem Value="-1">ALL</asp:ListItem>
                        <asp:ListItem>Officer</asp:ListItem>
                        <asp:ListItem>Staff</asp:ListItem>
                        <asp:ListItem>Worker</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Individual Attendance Report"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Employee ID"></asp:Label>
                </td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtEmpId" runat="server" CssClass="btn2" Width="380px" AutoCompleteType="None"></asp:TextBox>
                    <ajaxToolkit:AutoCompleteExtender ID="txtEmpId_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True"
                        CompletionListCssClass="autocomplete_completionListElement"
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        CompletionListItemCssClass="autocomplete_listItem2"
                        MinimumPrefixLength="1" ServiceMethod="GetEmpId"
                        ServicePath="~/modules/Payroll/WebService.asmx" TargetControlID="txtEmpId">
                    </ajaxToolkit:AutoCompleteExtender>

                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4"></td>
                <td class="auto-style4"></td>
                <td class="auto-style4">
                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem>Late</asp:ListItem>
                        <asp:ListItem Selected="True">Attendance</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="auto-style4"></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;<asp:Button ID="btnPreviewIndividual0" runat="server" CssClass="btn2" OnClick="btnPreviewIndividual0_Click" Text="Preview Report" Width="150px" />
                    &nbsp;<asp:Button ID="btnPreviewIndividual" runat="server" CssClass="btn2" OnClick="btnPreviewIndividual_Click" Text="Preview" Visible="False" Width="150px" />
                    &nbsp;<asp:Button ID="btnPreviewMonthlyReport" runat="server" CssClass="btn2" OnClick="btnPreviewMonthlyReport_Click" Text="Preview Monthly Attendance" Width="200px" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    <asp:GridView ID="gdvView" runat="server">
                    </asp:GridView>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr style="text-align: center">
                <td colspan="4">
                    <asp:Panel ID="Panel1" runat="server" CssClass="cpHeaderContent" Width="100%">
                        Monthly Summary Attendance
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnPreviewSummary" runat="server" CssClass="btn2" OnClick="btnPreviewSummary_Click" Text="View Attendance" Width="150px" />
                    &nbsp;
                    <asp:Button ID="btnReport" runat="server" CssClass="btn2" OnClick="btnReport_Click" Text="Preview Report" Width="150px" />
                    &nbsp;
                    <asp:Button ID="btnExport" runat="server" CssClass="btn2" OnClick="btnExport_Click" Text="Export" Width="150px" />
                    &nbsp;<asp:Button ID="btnExportAttendanceDetails" runat="server" CssClass="btn2" OnClick="btnExportAttendanceDetails_Click" Text="View Attendance Details" Width="150px" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="grdAttendanceRecord" runat="server" AutoGenerateColumns="False" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="SL">
                                <ItemTemplate>
                                    <%# Container.DisplayIndex + 1 %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Employee ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmployeeId" runat="server" Text='<%# Bind("EmpID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="From Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblFromDate" runat="server" Text='<%# Bind("FromDate", "{0:d}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="To Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblToDate" runat="server" Text='<%# Bind("ToDate", "{0:d}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Day">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalDay" runat="server" Text='<%# Bind("totalDay") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="WorkingDay" HeaderText="Working Day" />
                            <asp:BoundField DataField="PresentDay" HeaderText="Present Day" />
                            <asp:BoundField DataField="LeaveDay" HeaderText="Leave Day" />
                            <asp:BoundField DataField="Holiday" HeaderText="HoliDay" />
                            <asp:BoundField DataField="AbsentDay" HeaderText="Absent Day" />
                            <asp:BoundField DataField="PayableDay" HeaderText="Payable Day" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnExporttoExcel" runat="server" OnClick="btnExporttoExcel_Click" Text="Export to Excel" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td colspan="4">
                    <asp:GridView ID="grdAttendanceDetails" runat="server" Width="100%" OnPreRender="grdAttendanceDetails_PreRender" OnRowDataBound="grdAttendanceDetails_RowDataBound" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" ShowFooter="True">
                        
                        <AlternatingRowStyle BackColor="#CCCCCC" />
                        <FooterStyle BackColor="#CCCCCC" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#808080" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#383838" />
                        
                    </asp:GridView>
                </td>
            </tr>

        </table>
    </asp:Panel>


</asp:Content>
