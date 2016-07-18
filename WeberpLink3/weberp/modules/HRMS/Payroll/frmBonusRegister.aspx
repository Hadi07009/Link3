<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmBonusRegister.aspx.cs" Inherits="modules_HRMS_Payroll_frmBonusRegister" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>

            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="BONUS REGISTER" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 100%; text-align: left">
                    <tr>
                        <td style="width: 156px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td align="center">
                            <asp:Label ID="lblmsg" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 156px">
                            <asp:Label ID="Label3" runat="server" Text="Select Company"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlcompany" runat="server" AutoPostBack="True" CssClass="tbl"
                                OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" Width="310px">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 156px">
                            <asp:Label ID="Label4" runat="server" Text="Office Location"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlofficeLocation" runat="server" Width="310px">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 156px">
                            <asp:Label ID="Label2" runat="server" Text="Current Salary Month"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtSalaryDate" runat="server" AutoCompleteType="Disabled" Width="280px"></asp:TextBox>
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
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 156px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td colspan="2">
                            <table id="tbldet" runat="server" style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:RadioButtonList ID="rblForCondition" runat="server" RepeatDirection="Horizontal" Width="150px">
                                            <asp:ListItem Selected="True" Value="H">Hold</asp:ListItem>
                                            <asp:ListItem Value="P">Post</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSalreportPosted" runat="server" CssClass="btn2" OnClick="btnSalreportPosted_Click"
                                            Text="Bonus Register Preview" Width="300px" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btndeptsal" runat="server" CssClass="btn2" OnClick="btndeptsal_Click" Text="Department Wise Salary" Visible="False" Width="300px" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 156px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Label ID="lblmsgPf" runat="server" ForeColor="#CC0000"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 156px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Label ID="lblmsgSal" runat="server" ForeColor="#CC0000"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 156px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp; </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 156px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 156px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>

        </ContentTemplate>
        <Triggers>


            <asp:AsyncPostBackTrigger ControlID="ddlcompany" EventName="SelectedIndexChanged" />
            <asp:PostBackTrigger ControlID="btnSalreportPosted" />
            <asp:PostBackTrigger ControlID="btndeptsal" />

        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">

        function onCalendar1Shown(sender, args) {
            //set the default mode to month            
            sender._switchMode("months", true);
        }
    </script>
</asp:Content>
