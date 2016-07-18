<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmCashAdvice.aspx.cs" Inherits="modules_HRMS_Payroll_frmCashAdvice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>

            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="CASH ADVICE REPORT" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 100%; text-align: left">
                    <tr>
                        <td style="width: 114px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td align="center">
                            <asp:Label ID="lblmsg" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 114px">
                            <asp:Label ID="Label4" runat="server" Text="Select Company"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlcompany" runat="server" AutoPostBack="True" CssClass="tbl"
                                OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" Width="410px">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 114px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td colspan="2">
                            <table id="tbldet" runat="server" style="width: 100%;">
                                <tr>
                                    <td colspan="5">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 152px">
                                                    <asp:Label ID="Label5" runat="server" Text="Current Salary Month"></asp:Label>
                                                </td>
                                                <td style="width: 11px">:</td>
                                                <td>
                                                    <asp:TextBox ID="TextBox1" runat="server" AutoCompleteType="Disabled" Width="205px"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" BehaviorID="calendar11" Enabled="True" Format="MM/yyyy" OnClientShown="onCalendar1Shown" PopupButtonID="ImageButton1" TargetControlID="TextBox1">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Calendar.ico" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 152px">&nbsp;</td>
                                                <td style="width: 11px">&nbsp;</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlofficeLocation" runat="server" Width="230px" Visible="False">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 152px">&nbsp;</td>
                                                <td style="width: 11px">&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 152px">&nbsp;</td>
                                                <td style="width: 11px">&nbsp;</td>
                                                <td>
                                                    <asp:Button ID="btnCashAdvice" runat="server" CssClass="btn2" OnClick="btnCashAdvice_Click" Text="Cash Advice" Width="230px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 108px"></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="width: 108px">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 114px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 114px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlcompany" EventName="SelectedIndexChanged" />
            <asp:PostBackTrigger ControlID="btnCashAdvice" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">

        function onCalendar1Shown(sender, args) {
            //set the default mode to month            
            sender._switchMode("months", true);
        }
    </script>
</asp:Content>
