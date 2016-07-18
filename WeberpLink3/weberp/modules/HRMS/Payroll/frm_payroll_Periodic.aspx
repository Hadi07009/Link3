<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_payroll_Periodic.aspx.cs" Inherits="modules_HRMS_frm_payroll_Periodic" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="PAYROLL CALCULATION (PERIODIC)" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
                    <tr>
                        <td class="auto-style6" style="width: 122px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td align="center">
                            <asp:Label ID="lblmsg" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6" style="width: 122px">
                            <asp:Label ID="Label1" runat="server" Text="Select Company"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlcompany" runat="server" AutoPostBack="True" CssClass="tbl" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" Width="400px">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6" style="width: 122px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6" style="width: 122px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td colspan="2">
                            <table id="tbldet" runat="server" style="width: 50%;">
                                <tr>
                                    <td class="auto-style4" >
                                        <asp:Label ID="Label2" runat="server" Text="Last Salary Month&nbsp;"></asp:Label>
                                        </td>
                                    <td class="auto-style7">
                                        :</td>
                                    <td >
                                        <asp:Label ID="lblsalmonth" runat="server"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style4" ><asp:Label ID="Label3" runat="server" Text="Current Salary Month"></asp:Label>
                                    </td>
                                    <td class="auto-style7">
                                        :</td>
                                    <td >
                                        <asp:TextBox ID="TextBox1" runat="server" AutoCompleteType="Disabled" Width="195px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" BehaviorID="calendar11" Enabled="True" Format="MM/yyyy" OnClientShown="onCalendar1Shown" PopupButtonID="ImageButton1" TargetControlID="TextBox1">
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Calendar.ico" />
                                    </td>
                                    <td></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style4" >&nbsp;</td>
                                    <td class="auto-style7">&nbsp;</td>
                                    <td >&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style4">
                                        &nbsp;</td>
                                    <td class="auto-style7">&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnsave" runat="server" CssClass="btn2" OnClick="btnsave_Click" Text="Create Payroll" Width="215px" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style4" >&nbsp;</td>
                                    <td class="auto-style7">&nbsp;</td>
                                    <td >&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style4">
                                        &nbsp;</td>
                                    <td class="auto-style7">&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnpost" runat="server" CssClass="btn2" ForeColor="Red" OnClick="btnpost_Click" Text="Payroll Post" Width="215px" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style4" >&nbsp;</td>
                                    <td class="auto-style7">&nbsp;</td>
                                    <td >&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style4">
                                        &nbsp;</td>
                                    <td class="auto-style7">&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnjv" runat="server" CssClass="btn2" ForeColor="Red" OnClick="btnjv_Click" Text="Create JV" Width="215px" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6" style="width: 122px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Label ID="lblmsgPf" runat="server" ForeColor="#CC0000"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6" style="width: 122px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Label ID="lblmsgSal" runat="server" ForeColor="#CC0000"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6" style="width: 122px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:GridView ID="gdvView" runat="server">
                            </asp:GridView>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>


        </ContentTemplate>
        <Triggers>

            <asp:AsyncPostBackTrigger ControlID="btnpost" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnsave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlcompany" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">

        function onCalendar1Shown(sender, args) {
            //set the default mode to month            
            sender._switchMode("months", true);
        }
    </script>
</asp:Content>
