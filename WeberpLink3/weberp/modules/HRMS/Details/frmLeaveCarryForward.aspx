<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmLeaveCarryForward.aspx.cs" Inherits="modules_HRMS_Details_frmLeaveCarryForward" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="Leave Carry Forward" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
                    <tr>
                        <td style="width: 89px">
                            <asp:Label ID="Label12" runat="server" Text="Employee ID"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtEmployeeSearch" runat="server" AutoPostBack="True" OnTextChanged="txtEmployeeSearch_TextChanged" placeholder="Employee Code" Width="350px"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="txtEmployeeSearch_AutoCompleteExtender" runat="server" CompletionListCssClass="autocomplete_completionListElement" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem2" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetEmpId" ServicePath="~/modules/Payroll/WebService.asmx" TargetControlID="txtEmployeeSearch">
                            </ajaxToolkit:AutoCompleteExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 38px; width: 89px;">
                            <asp:Label ID="Label28" runat="server" Text="Date"></asp:Label>
                        </td>
                        <td style="height: 38px">:</td>
                        <td style="height: 38px">
                            <asp:TextBox ID="popupDate" runat="server" autocomplete="off" placeholder="Select First Date of Year" Width="350px"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="popupDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="popupDate">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 89px">
                            <asp:Label ID="Label19" runat="server" Text="Leave&nbsp; Type"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlLeaveType" runat="server" Width="355px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 89px">
                            <asp:Label ID="Label17" runat="server" Text="No of Leave"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtNoOfDays" runat="server" onkeypress="return isNumberKey(event)" Width="135px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 89px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 89px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" Width="100px" />
                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 89px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="grdLeaveReport" runat="server" AutoGenerateColumns="False" Width="100%" OnRowCommand="grdLeaveReport_RowCommand" OnRowDataBound="grdLeaveReport_RowDataBound" OnRowDeleting="grdLeaveReport_RowDeleting">
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
                                            <asp:Label ID="lblEmpid" runat="server" Text='<%# Bind("Empid") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLeaveCarryForwardDate" runat="server" Text='<%# Bind("LeaveCarryForwardDate", "{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Leave&nbsp; Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLeave_Mas_Name" runat="server" Text='<%# Bind("Leave_Mas_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Leave&nbsp; Type Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLeaveType" runat="server" Text='<%# Bind("LeaveType") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No of Leave">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoofleave" runat="server" Text='<%# Bind("Noofleave") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True">
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:CommandField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 89px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 46 || charCode > 57)) {
                alert("Please Enter Only Numeric Value:");
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
