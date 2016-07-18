<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmEarnedLeaveCalculation.aspx.cs" Inherits="modules_HRMS_Payroll_frmEarnedLeaveCalculation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="EARNED LEAVE CALCULATION" runat="server" />
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
                            <asp:Label ID="Label2" runat="server" Text="Select Company"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlcompany" runat="server" AutoPostBack="True" CssClass="tbl"
                                OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" Width="400px">
                            </asp:DropDownList>
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
                        <td colspan="2">
                            <table id="tbldet" runat="server" style="width: 100%;">
                                <tr>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text="Employee Code"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" Text="Date To"></asp:Label>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtempid" runat="server" Width="150px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <ew:CalendarPopup ID="dt2" runat="server" AutoPostBack="True" CssClass="txtbox" Culture="English (United Kingdom)" Enabled="true" Width="150px">
                                                        <MonthHeaderStyle BackColor="#2A2965" />
                                                        <ButtonStyle CssClass="btn2" />
                                                    </ew:CalendarPopup>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnEL" runat="server" CssClass="btn2" OnClick="btnEL_Click" Text="Calculate" Width="100px" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnElpreview" runat="server" CssClass="btn2" OnClick="btnElpreview_Click" Text="Preview" Width="100px" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnElpost" runat="server" CssClass="btn2" OnClick="btnElpost_Click" Text="Post" Width="100px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
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
            <asp:PostBackTrigger ControlID="btnEL" />
            <asp:PostBackTrigger ControlID="btnElpost" />
            <asp:PostBackTrigger ControlID="btnElpreview" />

        </Triggers>
    </asp:UpdatePanel>
    
    <script type="text/javascript">

         function onCalendar1Shown(sender, args) {
             //set the default mode to month            
             sender._switchMode("months", true);
         }
    </script>
</asp:Content>
