<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_Bonus_Cyclic.aspx.cs" Inherits="modules_HRMS_Payroll_frm_Bonus_Cyclic" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
     <cc1:ConfirmBox ID="ConfirmBox1" runat="server" />  

    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="BONUS CALCULATION" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 100%; text-align: left">
                    <tr>
                        <td style="width: 118px">&nbsp;</td>
                        <td style="width: 12px">&nbsp;</td>
                        <td align="center">
                            <asp:Label ID="lblmsg" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 118px">
                            <asp:Label ID="Label2" runat="server" Text="Select Company"></asp:Label>
                        </td>
                        <td style="width: 12px">:</td>
                        <td>
                            <asp:DropDownList ID="ddlcompany" runat="server" AutoPostBack="True" CssClass="tbl" 
                                OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" Width="365px">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 118px">&nbsp;</td>
                        <td style="width: 12px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 118px">&nbsp;</td>
                        <td style="width: 12px">&nbsp;</td>
                        <td colspan="2">
                            <table id="tbldet" runat="server" style="width: 100%;">
                                <tr>
                                    <td align="left" colspan="4">
                                        <table style="width:50%; text-align: left" >
                                            <tr>
                                                <td style="width: 152px" >
                                                    <asp:Label ID="Label3" runat="server" Text="Last Bonus Month"></asp:Label>
                                                </td>
                                                <td style="width: 8px">:</td>
                                                <td>
                                                    <asp:Label ID="lblsalmonth" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width:152px">
                                                    <asp:Label ID="Label4" runat="server" Text="Current Bonus Month"></asp:Label>
                                                </td>
                                                <td style="width: 8px">:</td>
                                                <td>
                                                    <asp:TextBox ID="TextBox1" runat="server" AutoCompleteType="Disabled" Width="150px"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                                                        BehaviorID="calendar11" 
                                                        Enabled="True" 
                                                        Format="dd/MM/yyyy" 
                                                        OnClientShown="onCalendar1Shown" 
                                                        PopupButtonID="ImageButton1" 
                                                        TargetControlID="TextBox1">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Calendar.ico" Width="32px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 22px"></td>
                                    <td style="height: 22px"></td>
                                    <td style="height: 22px"></td>
                                    <td style="height: 22px"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnCreatePayroll" runat="server" CssClass="btn2" OnClick="btnCreatePayroll_Click" OnClientClick="ShowConfirmBox(this,'Are you sure create bonus ?'); return false;" Text="Bonus Calculate" Width="360px" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnpost" runat="server" CssClass="btn2" ForeColor="Red" OnClick="btnpost_Click" OnClientClick="ShowConfirmBox(this,'Are you sure post payroll ?'); return false;" Text="Bonus Post" Width="360px" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnjv" runat="server" CssClass="btn2" ForeColor="Red" OnClick="btnjv_Click" Text="Create JV" Width="360px" Visible="False" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnsave" runat="server" CssClass="btn2" OnClick="btnsave_Click" Text="Create Payroll old" Visible="False" Width="360px" />
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 118px">&nbsp;</td>
                        <td style="width: 12px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 118px">&nbsp;</td>
                        <td style="width: 12px">&nbsp;</td>
                        <td>
                            <asp:Label ID="lblmsgSal" runat="server" ForeColor="#CC0000"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 118px">&nbsp;</td>
                        <td style="width: 12px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 118px">&nbsp;</td>
                        <td style="width: 12px">&nbsp;</td>
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
            <asp:PostBackTrigger ControlID="btnCreatePayroll"/>
            <asp:PostBackTrigger ControlID="btnpost"/>
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
