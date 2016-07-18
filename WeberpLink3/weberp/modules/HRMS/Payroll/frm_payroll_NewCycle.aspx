<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_payroll_NewCycle.aspx.cs" Inherits="modules_HRMS_Payroll_frm_payroll_NewCycle" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="PAYROLL CALCULATION" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td align="center">
                            <asp:Label ID="lblmsg" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Select Company"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlcompany" runat="server" AutoPostBack="True" CssClass="tbl" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" 
                                Width="400px">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td colspan="2">
                            <table id="tbldet" runat="server" style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="Last Salary Month"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="lblsalmonth" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 152px">
                                        <asp:Label ID="Label3" runat="server" Text="Current Salary Month"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="TextBox1" runat="server" AutoCompleteType="Disabled" Width="105px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" BehaviorID="calendar11" Enabled="True" Format="MM/yyyy" OnClientShown="onCalendar1Shown" PopupButtonID="ImageButton1" TargetControlID="TextBox1">
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Calendar.ico" />
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Button ID="btnsave" runat="server" CssClass="btn2" OnClick="btnsave_Click"
                                            Text="Save Mode Payroll" Width="300px" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnRefresh" runat="server" CssClass="btn2"
                                            OnClick="btnRefresh_Click" Text="Refresh" Visible="False" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4"></td>
                                    <td></td>
                                    <td ></td>
                                    <td ></td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Button ID="btnManpowerList" runat="server" CssClass="btn2"
                                            OnClick="btnManpowerList_Click" Text="Manpower List" Width="300px" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnpost" runat="server" CssClass="btn2" ForeColor="Red"
                                            OnClick="btnpost_Click" Text="Payroll Post" Width="300px" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style9" colspan="4"></td>
                                    <td class="style10"></td>
                                    <td class="style11"></td>
                                    <td class="style11"></td>
                                </tr>
                                <tr>
                                    <td colspan="4">&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnjv" runat="server" CssClass="btn2" ForeColor="Red"
                                            OnClick="btnjv_Click" Text="Create JV" Width="300px" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4"></td>
                                    <td></td>
                                    <td ></td>
                                    <td ></td>
                                </tr>
                                <tr>
                                    <td colspan="4">&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnremove" runat="server" CssClass="btn2" ForeColor="Red"
                                            OnClick="btnremove_Click" Text="Remove (ADVAM1,PAYDAY,MOB_DE,AREAR)"
                                            Width="300px" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:DropDownList ID="ddlofficeLocation" runat="server" Width="125px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="btndeptsal" runat="server" CssClass="btn2"
                                            Text="Department Wise Salary" Width="300px" OnClick="btndeptsal_Click" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4">&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnSalreport" runat="server" CssClass="btn2"
                                            OnClick="btnSalreport_Click" Text="Salary Report(Save Mode)" Width="300px" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4">&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnSalreportPosted" runat="server" CssClass="btn2"
                                            OnClick="btnSalreportPosted_Click" Text="Salary Report(Posted)" Width="300px" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4">&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnCashAdvice" runat="server" CssClass="btn2"
                                            Text="Cash Advice" Width="300px" OnClick="btnCashAdvice_Click" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="style27" style="width: 127px">
                                                    <asp:Label ID="Label4" runat="server" Text="Employee Code"></asp:Label>
                                                </td>
                                                <td class="style27">:</td>
                                                <td class="style28" style="width: 136px">
                                                    <asp:TextBox ID="txtid" runat="server" CssClass="btn2" Width="125px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnPaySlip" runat="server" CssClass="btn2"
                                                        OnClick="btnPaySlip_Click" Text="Employee Payslip" Width="300px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="style15">
                                                    &nbsp;</td>
                                                <td class="style16">
                                                    &nbsp;</td>
                                                <td class="style17">
                                                    &nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="style15">
                                                    <asp:Label ID="Label5" runat="server" Text="Bank Name"></asp:Label>
                                                </td>
                                                <td class="style16">
                                                    <asp:Label ID="Label6" runat="server" Text="Checq No"></asp:Label>
                                                </td>
                                                <td class="style17">
                                                    <asp:Label ID="Label7" runat="server" Text="Checq Date"></asp:Label>
                                                </td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="style15">
                                                    <asp:DropDownList ID="ddlBank" runat="server" Width="125px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="style16">
                                                    <asp:TextBox ID="txtCheqNo" runat="server" CssClass="btn2" Width="125px"></asp:TextBox>
                                                </td>
                                                <td style="text-align: left">
                                                    <ew:CalendarPopup ID="dtFrom" runat="server" CssClass="txtbox" DisableTextBoxEntry="False" DisplayPrevNextYearSelection="True"
                                                        Culture="English (United Kingdom)" Width="125px">
                                                        <MonthHeaderStyle BackColor="#2A2965" />
                                                        <ButtonStyle CssClass="btn2" />
                                                    </ew:CalendarPopup>
                                                    <%-- <asp:TextBox ID="dtFrom" runat="server" AutoCompleteType="Disabled" 
                                                            Width="80px"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" 
                                                                BehaviorID="calendar11" Enabled="True" Format="dd/MM/yyyy" 
                                                                OnClientShown="onCalendar1Shown" PopupButtonID="ImageButton2" 
                                                                TargetControlID="dtFrom">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <asp:ImageButton ID="ImageButton2" runat="server" 
                                                            ImageUrl="~/image/Calendar.ico" />--%>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnBankAdvice" runat="server" CssClass="btn2"
                                                        Text="Bank Advice" OnClick="btnBankAdvice_Click" Width="125px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="style23">
                                                    <asp:Label ID="Label8" runat="server" Text="Employee Code"></asp:Label>
                                                </td>
                                                <td class="style24">
                                                    <asp:Label ID="Label9" runat="server" Text="Date To"></asp:Label>
                                                </td>
                                                <td class="style25">&nbsp;</td>
                                                <td class="style26">&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="style23">
                                                    <asp:TextBox ID="txtempid" runat="server" Width="125px"></asp:TextBox>
                                                </td>
                                                <td class="style24">
                                                    <ew:CalendarPopup ID="dt2" runat="server" CssClass="txtbox" Enabled="true"
                                                        Culture="English (United Kingdom)" Width="125px" AutoPostBack="True">
                                                        <MonthHeaderStyle BackColor="#2A2965" />
                                                        <ButtonStyle CssClass="btn2" />
                                                    </ew:CalendarPopup>
                                                </td>
                                                <td class="style25">
                                                    <asp:Button ID="btnEL" runat="server" CssClass="btn2" OnClick="btnEL_Click"
                                                        Text="Calculate" Width="125px" />
                                                </td>
                                                <td class="style26">
                                                    <asp:Button ID="btnElpost" runat="server" CssClass="btn2" OnClick="btnElpost_Click"
                                                        Text="Post" Width="125px" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnElpreview" runat="server" CssClass="btn2"
                                                        OnClick="btnElpreview_Click" Text="Preview" Width="125px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style23">&nbsp;</td>
                                                <td class="style24">&nbsp;</td>
                                                <td class="style25">&nbsp;</td>
                                                <td class="style26">&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4">&nbsp;<asp:Label ID="Label10" runat="server" Text="Employee Code"></asp:Label>
                                        :<asp:TextBox ID="txtEmployeeID" runat="server"
                                        Width="125px"></asp:TextBox>

                                        <asp:Button ID="btnInclude" runat="server" CssClass="btn2" OnClick="btnInclude_Click" Text="Include In PF" Width="125px" />

                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Label ID="lblmsgPf" runat="server" ForeColor="#CC0000"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Label ID="lblmsgSal" runat="server" ForeColor="#CC0000"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp; </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
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
            <asp:AsyncPostBackTrigger ControlID="btnInclude" EventName="Click" />
            <asp:PostBackTrigger ControlID="btnSalreport" />
            <asp:PostBackTrigger ControlID="btnSalreportPosted" />
            <asp:PostBackTrigger ControlID="btnManpowerList" />
            <asp:PostBackTrigger ControlID="btnPaySlip" />
            <asp:PostBackTrigger ControlID="btndeptsal" />
            <asp:PostBackTrigger ControlID="btnCashAdvice" />
            <asp:PostBackTrigger ControlID="btnBankAdvice" />
            <asp:PostBackTrigger ControlID="btnPaySlip" />
            <asp:PostBackTrigger ControlID="btnEL" />
            <asp:PostBackTrigger ControlID="btnElpost" />
            <asp:PostBackTrigger ControlID="btnElpreview" />
            <asp:PostBackTrigger ControlID="btnRefresh" />
        </Triggers>
    </asp:UpdatePanel>
    
    <script type="text/javascript">

         function onCalendar1Shown(sender, args) {
             //set the default mode to month            
             sender._switchMode("months", true);
         }
    </script>
</asp:Content>
