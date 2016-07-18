<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmOthersPayment.aspx.cs" Inherits="modules_HRMS_Payroll_frmOthersPayment" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text=" Others Payment" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
                    <tr>
                        <td style="width: 114px">
                            <asp:Label ID="Label10" runat="server" Text="Office Location"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <div style="OVERFLOW-Y: scroll; WIDTH: 350px; HEIGHT: 175px; border: 1px solid; border-color: #669999; border-style: Ridge">
                                <asp:CheckBoxList ID="chkofficelocation" runat="server" AutoPostBack="True">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                        </tr>
                    <tr>
                        <td style="width: 114px">
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
                        <td style="width: 114px">
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
                        <td style="width: 114px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 114px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 114px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 114px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 114px">&nbsp;</td>
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
