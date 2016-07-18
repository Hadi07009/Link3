<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmEarnedLeaveApplicationAdmin.aspx.cs" Inherits="ClientSide_modules_mis_naz_FORMS_HRMS_Forms_frmEarnedLeaveApplicationAdmin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        
        .style2 {
            height: 18px;
        }

        .style7 {
            width: 109px;
            text-align: left;
        }

        .style8 {
            width: 24px;
        }

        .style9 {
            width: 348px;
            text-align: left;
        }

        .style10 {
            width: 81px;
            text-align: left;
        }

        .style11 {
            width: 13px;
        }

        .style20 {
            width: 357px;
        }

        .style21 {
            width: 83px;
            text-align: left;
        }

        .style22 {
            width: 15px;
        }

        .style23 {
            width: 156px;
        }

        .style24 {
            text-align: left;
        }

        .auto-style1 {
            width: 348px;
            text-align: left;
            height: 18px;
        }

        .auto-style3 {
            width: 156px;
            height: 18px;
        }

        .auto-style4 {
            height: 18px;
        }
        .auto-style7 {
            width: 145px;
            text-align: left;
        }
        .auto-style8 {
            width: 194px;
            text-align: left;
        }
    </style>
    <asp:UpdatePanel ID="updtPnl" runat="server">
        <ContentTemplate>
             <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                                <asp:Label ID="lblleave" Text="EARNEDLEAVE APPLICATION" runat="server" />
                            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                                <table style="width:99%;text-align:left">
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="#3333FF"></asp:Label>
                                            <br />
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="auto-style1">
                                                        <asp:Label ID="Label5" runat="server" Text="Employee Information"></asp:Label>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td></td>
                                                    <td style="text-align: center" class="auto-style4">&nbsp;<asp:Label ID="Label13" runat="server" Text="Select Employee ID and Till Date"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style9">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td class="style10">
                                                                    <asp:Label ID="Label6" runat="server" Text="ID"></asp:Label>
                                                                </td>
                                                                <td class="style11">:</td>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="lblId" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style10">
                                                                    <asp:Label ID="Label7" runat="server" Text="Name"></asp:Label>
                                                                </td>
                                                                <td class="style11">:</td>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="lblName" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style10">
                                                                    <asp:Label ID="Label8" runat="server" Text="Department"></asp:Label>
                                                                </td>
                                                                <td class="style11">:</td>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="lbldept" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style10">
                                                                    <asp:Label ID="Label9" runat="server" Text="Designation"></asp:Label>
                                                                </td>
                                                                <td class="style11">:</td>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style10">
                                                                    <asp:Label ID="Label10" runat="server" Text="Joining Date"></asp:Label>
                                                                </td>
                                                                <td class="style11">:</td>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="lblJoiningDate" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style10">
                                                                    <asp:Label ID="Label11" runat="server" Text="Period"></asp:Label>
                                                                </td>
                                                                <td class="style11">:</td>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="lblPeriod" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style10">
                                                                    <asp:Label ID="Label12" runat="server" Text="Date"></asp:Label>
                                                                </td>
                                                                <td class="style11">:</td>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="lblcurrentPeriod" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td class="style20">
                                                        <asp:GridView ID="gdvLeaveInfo" runat="server" AutoGenerateColumns="False"
                                                            OnRowDataBound="gdvLeaveInfo_RowDataBound">
                                                            <Columns>
                                                                <asp:BoundField DataField="Code" HeaderStyle-HorizontalAlign="Left"
                                                                    HeaderText="SL #" ItemStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField DataField="Leave_Mas_Name" HeaderStyle-HorizontalAlign="Center"
                                                                    HeaderText="Leave Type" ItemStyle-HorizontalAlign="Center" />
                                                                <asp:BoundField DataField="AllocatedLeave" HeaderStyle-HorizontalAlign="Center"
                                                                    HeaderText="Allowed" ItemStyle-HorizontalAlign="Center" />
                                                                <asp:BoundField DataField="Enjoyed" HeaderStyle-HorizontalAlign="Center"
                                                                    HeaderText="Enjoyed" ItemStyle-HorizontalAlign="Center" />
                                                                <asp:BoundField DataField="LeaveBal" HeaderStyle-HorizontalAlign="Center"
                                                                    HeaderText="Balance" ItemStyle-HorizontalAlign="Center" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                    <td class="style23">
                                                        <asp:Image ID="Image1" runat="server" Height="100px" Visible="False"
                                                            Width="100px" />
                                                    </td>
                                                    <td style="text-align: right">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td class="auto-style8">
                                                                    <asp:Label ID="Label14" runat="server" Text="Employee ID" Width="90px"></asp:Label>
                                                                </td>
                                                                <td class="style22">:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtempid" runat="server" Width="178px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="auto-style8">
                                                                    <asp:Label ID="Label15" runat="server" Text="Apply Till Date" Width="90px"></asp:Label>
                                                                </td>
                                                                <td class="style22">:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtFromDate" runat="server" Width="178px"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                                                                    </cc1:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="auto-style8">
                                                                    <asp:Button ID="btnShowALL" runat="server" OnClick="btnShowALL_Click" Text="Show ALL" Visible="False" Width="100px" />
                                                                </td>
                                                                <td class="style22">&nbsp;</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtToDate" runat="server" Width="178px" Visible="False"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server"
                                                                        Format="dd/MM/yyyy" TargetControlID="txtToDate">
                                                                    </cc1:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="auto-style8">
                                                                    <asp:Button ID="btnCurrentPeriod" runat="server"
                                                                        OnClick="btnCurrentPeriod_Click" Text="Refresh" Visible="False" Width="75px" />
                                                                </td>
                                                                <td class="style22">&nbsp;</td>
                                                                <td>
                                                                    <asp:Button ID="btnShow" runat="server" Text="Show EL" Width="178px"
                                                                        OnClick="btnShow_Click" CssClass="btn2" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="auto-style8">
                                                                    <asp:Button ID="btnprevious" runat="server" OnClick="btnprevious_Click"
                                                                        Text="&lt;&lt;" Visible="False" Width="50px" />
                                                                </td>
                                                                <td class="style22">&nbsp;</td>
                                                                <td>
                                                                    <asp:Button ID="btnPreviewEL" runat="server" CssClass="btn2" OnClick="btnPreviewEL_Click" Text="Preview EL" Width="178px" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
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
                                        <td colspan="3">
                                            <asp:GridView ID="GridViewLeave" runat="server" AutoGenerateColumns="False"
                                                Width="100%" OnRowDataBound="GridViewLeave_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="Sl" HeaderText="SL #"
                                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="LastPaymentELDate" DataFormatString="{0:d}"
                                                        HeaderText="Last Encash Date" HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="LeaveFrom" DataFormatString="{0:d}"
                                                        HeaderText="Leave From" HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="LeaveTo" DataFormatString="{0:d}"
                                                        HeaderText="Leave To" HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TotalDays" HeaderText="Total Days"
                                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TotalHdays" HeaderText="Holidays"
                                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TotalLvenjoyed" HeaderText="Leave Enjoied"
                                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Actualworkingdays" HeaderText="Working Days"
                                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ELavailableThisPeriod" HeaderText="Total EL"
                                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ELenjoyedThisPeriod" HeaderText="EL Enjoied"
                                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ActualELpassed" HeaderText="Actual EL"
                                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="RateofGrossPay" HeaderText="Gross Salary"
                                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="RateELPerDay" HeaderText="Pay Rate"
                                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PaymentEncash" HeaderText="Amount"
                                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Comments" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtlvRemarks" runat="server"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Leave Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="lvtype" runat="server"></asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtlvstatus" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="EmpName" HeaderText="Action Taken By"
                                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="Checklv" runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Type" HeaderText="Type"
                                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="noofdays" HeaderText="noofd"
                                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks"
                                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IsLineLocked" HeaderText="LockedL"
                                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IsProcessLocked" HeaderText="LockedP"
                                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Action" HeaderText="Action"
                                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProcessLevelid" HeaderText="PLid"
                                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="HPflag" HeaderText="Elst"
                                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                                    <asp:BoundField DataField="felst" HeaderText="fElst"
                                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>

                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td class="style24" style="text-align: left">
                                                        <asp:DropDownList ID="dplResponsible" runat="server" Width="250px"
                                                            Visible="False">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align: left">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblRemarks" runat="server"></asp:Label>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td class="style24" style="text-align: left" colspan="2">
                                                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td class="style24">
                                                        <asp:Button ID="btnApplyAndApprove" runat="server" CssClass="btn2" OnClick="btnApplyAndApprove_Click" Text="Apply &amp; Approve" Width="178px" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                     <asp:Button ID="btnApply" runat="server" CssClass="btn2" OnClick="btnApply_Click" Text="Apply" Width="100px" Visible="False" />
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td class="style24">&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
            </asp:Panel>
             <%--<cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server"
                                TargetControlID="PanelLeavedet"
                                CollapseControlID="PanelLeaveHdr"
                                ExpandControlID="PanelLeaveHdr"
                                Collapsed="false"
                                TextLabelID="lblleave"
                                CollapsedText="EARNEDLEAVE APPLICATION"
                                ExpandedText="EARNEDLEAVE APPLICATION"
                                CollapsedSize="2"
                                ExpandedSize="1250"
                                AutoCollapse="False"
                                AutoExpand="False"
                                ScrollContents="False"
                                ImageControlID="Image1"
                                ExpandedImage="~/images/collapse.jpg"
                                CollapsedImage="~/images/expand.jpg"
                                ExpandDirection="Vertical">
                            </cc1:CollapsiblePanelExtender>--%>
        </ContentTemplate>
         <Triggers>
                        <asp:PostBackTrigger ControlID="btnPreviewEL" />
                        <asp:PostBackTrigger ControlID="btnApply" />
                    </Triggers>
    </asp:UpdatePanel>
</asp:Content>
