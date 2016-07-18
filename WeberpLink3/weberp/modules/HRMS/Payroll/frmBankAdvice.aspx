<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmBankAdvice.aspx.cs" Inherits="modules_HRMS_Payroll_frmBankAdvice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>

            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="BANK ADVICE REPORT" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 100%; text-align: left">
                    <tr>
                        <td style="width: 123px">&nbsp;</td>
                        <td style="width: 11px">&nbsp;</td>
                        <td align="center">
                            <asp:Label ID="lblmsg" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 123px">
                            <asp:Label ID="Label2" runat="server" Text="Select Company"></asp:Label>
                        </td>
                        <td style="width: 11px">:</td>
                        <td>
                            <asp:DropDownList ID="ddlcompany" runat="server" AutoPostBack="True" CssClass="tbl"
                                OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" Width="410px">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 123px">&nbsp;</td>
                        <td style="width: 11px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 123px">&nbsp;</td>
                        <td style="width: 11px">&nbsp;</td>
                        <td colspan="2">
                            <table id="tbldet" runat="server" style="width: 100%;">
                                <tr>
                                    <td>
                                        <table style="width: 55%;">
                                            <tr>
                                                <td class="auto-style5" style="width: 158px">
                                                    <asp:Label ID="Label3" runat="server" Text="Current Salary Month"></asp:Label>
                                                </td>
                                                <td style="width: 9px">:</td>
                                                <td>
                                                    <asp:TextBox ID="txtSalaryDate" runat="server" AutoCompleteType="Disabled" Width="200px"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                        BehaviorID="calendar11"
                                                        Enabled="True"
                                                        Format="MM/yyyy"
                                                        OnClientShown="onCalendar1Shown"
                                                        PopupButtonID="ImageButton1"
                                                        TargetControlID="txtSalaryDate">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Calendar.ico" Height="19px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style5" style="width: 158px">
                                                    <asp:Label ID="Label4" runat="server" Text="Bank Name"></asp:Label>
                                                </td>
                                                <td style="width: 9px">:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlBank" runat="server" Width="225px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style5" style="width: 158px">
                                                    <asp:Label ID="Label5" runat="server" Text="Checq No"></asp:Label>
                                                </td>
                                                <td style="width: 9px">:</td>
                                                <td>
                                                    <asp:TextBox ID="txtCheqNo" runat="server" CssClass="btn2" Width="220px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style5" style="width: 158px">
                                                    <asp:Label ID="Label6" runat="server" Text="Checq Date"></asp:Label>
                                                </td>
                                                <td style="width: 9px">:</td>
                                                <td>
                                                    <ew:CalendarPopup ID="dtFrom" runat="server" CssClass="txtbox" Culture="English (United Kingdom)" DisableTextBoxEntry="False" DisplayPrevNextYearSelection="True" Width="188px">
                                                        <MonthHeaderStyle BackColor="#2A2965" />
                                                        <ButtonStyle CssClass="btn2" />
                                                    </ew:CalendarPopup>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style5" style="width: 158px">&nbsp;</td>
                                                <td style="width: 9px">&nbsp;</td>
                                                <td>
                                                    <asp:Button ID="btnBankAdvice" runat="server" CssClass="btn2" OnClick="btnBankAdvice_Click" Text="Bank Advice" Width="100px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style5" style="width: 158px">&nbsp;</td>
                                                <td style="width: 9px">&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 123px">&nbsp;</td>
                        <td style="width: 11px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlcompany" EventName="SelectedIndexChanged" />
            <asp:PostBackTrigger ControlID="btnBankAdvice" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">

        function onCalendar1Shown(sender, args) {
            //set the default mode to month            
            sender._switchMode("months", true);
        }
    </script>
</asp:Content>
