<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmDaywiseLeaveReport.aspx.cs" Inherits="modules_HRMS_Details_frmDaywiseLeaveReport" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="Day wise Leave Report" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
                    <tr>
                        <td style="width: 97px">
                            <asp:Label ID="Label7" runat="server" Text="Select Company"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlcompany" runat="server" AutoPostBack="True" CssClass="tbl" Width="353px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            <asp:Label ID="Label1" runat="server" Text="From Date"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="popupFromDate" runat="server" placeholder="From Date" Width="350px"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="popupFromDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="popupFromDate">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            <asp:Label ID="Label2" runat="server" Text="To Date"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="popupToDate" runat="server" placeholder="To Date" Width="350px"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="popupToDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="popupToDate">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            <asp:Label ID="Label12" runat="server" Text="Office Location"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <div style="OVERFLOW-Y: scroll; WIDTH: 350px; HEIGHT: 175px; border: 1px solid; border-color: #669999; border-style: Ridge">
                                             <asp:CheckBoxList ID="chkofficelocation" runat="server">
                                             </asp:CheckBoxList>
                                         </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            <asp:Label ID="Label10" runat="server" Text="Department"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlDepartmentId" runat="server" AutoPostBack="True" Width="353px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            <asp:Label ID="Label14" runat="server" Text="Category"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlEmpCategory" runat="server" AutoPostBack="True" Width="353px">
                                <asp:ListItem Value="-1">ALL</asp:ListItem>
                                <asp:ListItem>Officer</asp:ListItem>
                                <asp:ListItem>Staff</asp:ListItem>
                                <asp:ListItem>Worker</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            <asp:Label ID="Label3" runat="server" Text="Employee Code"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtEmployeeCode" runat="server" Width="350px" AutoPostBack="True" placeholder="Employee Code" OnTextChanged="txtEmployeeCode_TextChanged"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="txtEmployeeCode_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True" 
                                CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                CompletionListItemCssClass="autocomplete_listItem2"
                                MinimumPrefixLength="1" ServiceMethod="GetEmpId" ServicePath="~/modules/Payroll/WebService.asmx" TargetControlID="txtEmployeeCode">
                            </ajaxToolkit:AutoCompleteExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnShow" runat="server" Text="Show" Width="100px" OnClick="btnShow_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            <asp:Button ID="btnExporttoExcelDetails" runat="server" OnClick="btnExporttoExcelDetails_Click" Text="Export to Excel" Visible="False" Width="100px" />
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="grdLeaveDetails" runat="server" Width="100%" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <%# Container.DisplayIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLeave_Det_Emp_Id" runat="server" Text='<%# Bind("Leave_Det_Emp_Id") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpName" runat="server" Text='<%# Bind("EmpName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDesignation" runat="server" Text='<%# Bind("Designation") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDept" runat="server" Text='<%# Bind("Dept") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Leave Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLeaveType" runat="server" Text='<%# Bind("Leave_Mas_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLeave_Det_Sta_Date" runat="server" Text='<%# Bind("Leave_Det_Sta_Date", "{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount of Leave">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLeave_Det_Emp_Days" runat="server" Text='<%# Bind("Leave_Det_Emp_Days") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 97px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 97px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 97px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 97px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShow" />                        
            <asp:PostBackTrigger ControlID="btnExporttoExcelDetails" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
