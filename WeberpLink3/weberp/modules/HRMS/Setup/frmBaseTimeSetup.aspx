<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmBaseTimeSetup.aspx.cs" Inherits="modules_HRMS_Setup_frmBaseTimeSetup" %>


<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="BASE TIME SETUP" runat="server" Font-Bold="True" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
                    <tr>
                        <td style="width: 75px">
                            <asp:Label ID="Label1" runat="server" Text="Select Shift"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlShift" runat="server" Width="356px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 75px">
                            <asp:Label ID="Label2" runat="server" Text="Date From"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="popupFromDate" runat="server" placeholder="Date From" Width="350px"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="popupFromDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="popupFromDate">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 75px">
                            <asp:Label ID="Label3" runat="server" Text="Date To"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="popupToDate" runat="server" placeholder="Date To" Width="350px"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="popupToDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="popupToDate">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 75px">
                            <asp:Label ID="Label16" runat="server" Text="In Time"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <MKB:TimeSelector ID="inTime" runat="server" AmPm="AM" DisplaySeconds="False" Hour="9">
                            </MKB:TimeSelector>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 75px">
                            <asp:Label ID="Label17" runat="server" Text="Out Time"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <MKB:TimeSelector ID="outTime" runat="server" AmPm="PM" DisplaySeconds="False" Hour="9">
                            </MKB:TimeSelector>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 75px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="height: 29px; width: 75px;"></td>
                        <td style="height: 29px"></td>
                        <td style="height: 29px">
                            <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" OnClick="btnSave_Click" />
                            <asp:Button ID="btnClear" runat="server" Text="Clear" Width="100px" OnClick="btnClear_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 29px; width: 75px;">&nbsp;</td>
                        <td style="height: 29px">&nbsp;</td>
                        <td style="height: 29px">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3" style="height: 29px">
                            <asp:GridView ID="grdBaseTime" runat="server" AutoGenerateColumns="False" Width="100%" OnRowCommand="grdBaseTime_RowCommand" OnRowDataBound="grdBaseTime_RowDataBound" OnRowDeleting="grdBaseTime_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <%# Container.DisplayIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Shift">
                                        <ItemTemplate>
                                            <asp:Label ID="lblShift_Mas_Desc" runat="server" Text='<%# Bind("Shift_Mas_Desc") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date From">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDateFrom" runat="server" Text='<%# Bind("DateFrom", "{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date To">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDateTo" runat="server" Text='<%# Bind("DateTo", "{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="In Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInTime" runat="server" Text='<%# Bind("InTime") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Out Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOutTime" runat="server" Text='<%# Bind("OutTime") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="slNo">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSlno" runat="server" Text='<%# Bind("Slno") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="shiftCode">
                                        <ItemTemplate>
                                            <asp:Label ID="lblShiftID" runat="server" Text='<%# Bind("ShiftID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 75px">&nbsp;</td>
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
</asp:Content>

