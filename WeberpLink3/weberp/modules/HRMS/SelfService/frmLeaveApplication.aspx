<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmLeaveApplication.aspx.cs" Inherits="ClientSide_modules_mis_naz_FORMS_HRMS_Forms_frmLeaveApplication" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <cc1:ConfirmBox ID="ConfirmBox1" runat="server" />


    <script src="http://code.jquery.com/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../../../script/jquery.MultiFile.js" type="text/javascript"></script>

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

        .auto-style4 {
            width: 61px;
        }

        .auto-style5 {
            width: 12px;
        }
    </style>
    <asp:UpdatePanel ID="updtPnl" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="LEAVE APPLICATION" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
                    <tr>
                        <td colspan="3">
                            <table style="width: 100%;">

                                <tr>
                                    <td class="style9" style="height: 22px">
                                        <asp:Label ID="Label7" runat="server" Text="Employee Information"></asp:Label>
                                    </td>
                                    <td class="style20" style="height: 22px">
                                        <asp:Label ID="Label18" runat="server" Text="Leave Summery"></asp:Label>
                                    </td>
                                    <td class="style23" style="height: 22px"></td>
                                    <td style="text-align: center; height: 22px;">
                                        <asp:Label ID="Label15" runat="server" Text="Previous Leave/ALL Information"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style9">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="style10">
                                                    <asp:Label ID="Label8" runat="server" Text="ID"></asp:Label>
                                                </td>
                                                <td class="style11">:</td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblId" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style10">
                                                    <asp:Label ID="Label9" runat="server" Text="Name"></asp:Label>
                                                </td>
                                                <td class="style11">:</td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblName" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style10">
                                                    <asp:Label ID="Label10" runat="server" Text="Department"></asp:Label>
                                                </td>
                                                <td class="style11">:</td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lbldept" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style10">
                                                    <asp:Label ID="Label11" runat="server" Text="Designation"></asp:Label>
                                                </td>
                                                <td class="style11">:</td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style10">
                                                    <asp:Label ID="Label12" runat="server" Text="Joining Date"></asp:Label>
                                                </td>
                                                <td class="style11">:</td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblJoiningDate" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style10">&nbsp;</td>
                                                <td class="style11">&nbsp;</td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblPeriod" runat="server" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style10">&nbsp;</td>
                                                <td class="style11">&nbsp;</td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblcurrentPeriod" runat="server" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="style20" style="vertical-align: top">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="gdvLeaveInfo" runat="server" AutoGenerateColumns="False" OnRowDataBound="gdvLeaveInfo_RowDataBound">
                                                        <Columns>
                                                            <asp:BoundField DataField="Code" HeaderStyle-HorizontalAlign="Left" HeaderText="SL #" ItemStyle-HorizontalAlign="Left" />
                                                            <asp:BoundField DataField="Leave_Mas_Name" HeaderStyle-HorizontalAlign="Center" HeaderText="Leave Type" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="AllocatedLeave" HeaderStyle-HorizontalAlign="Center" HeaderText="Allowed" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="Enjoyed" HeaderStyle-HorizontalAlign="Center" HeaderText="Enjoyed" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="LeaveBal" HeaderStyle-HorizontalAlign="Center" HeaderText="Balance" ItemStyle-HorizontalAlign="Center" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="style23">
                                        <asp:Image ID="Image1" runat="server" Height="100px" Visible="False"
                                            Width="100px" />
                                    </td>
                                    <td style="text-align: right">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="style21">
                                                    <asp:Label ID="Label16" runat="server" Text="From"></asp:Label>
                                                </td>
                                                <td class="style22">:</td>
                                                <td>
                                                    <asp:TextBox ID="txtFromDate" runat="server" Width="178px" autocomplete="off"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server"
                                                        Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style21">
                                                    <asp:Label ID="Label17" runat="server" Text="To"></asp:Label>
                                                </td>
                                                <td class="style22">:</td>
                                                <td>
                                                    <asp:TextBox ID="txtToDate" runat="server" Width="178px" autocomplete="off"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server"
                                                        Format="dd/MM/yyyy" TargetControlID="txtToDate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style21">
                                                    <asp:Button ID="btnCurrentPeriod" runat="server"
                                                        OnClick="btnCurrentPeriod_Click" Text="Refresh" Visible="False" Width="75px" />
                                                </td>
                                                <td class="style22">&nbsp;</td>
                                                <td>
                                                    <asp:Button ID="btnShowALL" runat="server" OnClick="btnShowALL_Click" Text="Show Attendance" Width="180px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style21">
                                                    <asp:Button ID="btnprevious" runat="server" OnClick="btnprevious_Click"
                                                        Text="&lt;&lt;" Visible="False" Width="50px" />
                                                </td>
                                                <td class="style22">&nbsp;</td>
                                                <td>
                                                    <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show All Leave Application" Width="180px" />
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
                        <td>
                            <asp:Label ID="Label19" runat="server" Text="Absent :"></asp:Label>
                            &nbsp;<asp:LinkButton ID="btnAbsentCount" runat="server" OnClick="btnAbsentCount_Click" ForeColor="Red"></asp:LinkButton>
                            &nbsp;,
                            <asp:Label ID="Label20" runat="server" Text="Late: "></asp:Label>
                            <asp:LinkButton ID="btnCountLate" runat="server" OnClick="btnCountLate_Click" ForeColor="Red"></asp:LinkButton>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="GridViewLeave" runat="server" AutoGenerateColumns="False"
                                Width="100%" OnRowDataBound="GridViewLeave_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL #">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSL" runat="server" Text='<%# Bind("Sl") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="Checklv" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Atnd_det_date", "{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Week Day">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWeekDay" runat="server" Text='<%# Bind("weekdayname") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Atnd_det_offlg") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Intime">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIntime" runat="server" Text='<%# Bind("Atnd_det_intime") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Outtime">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOuttime" runat="server" Text='<%# Bind("Atnd_det_outtime") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Hours">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHours" runat="server" Text='<%# Bind("Atnd_det_hrs") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comments" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtlvRemarks" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Leave" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="dpllday" runat="server"></asp:DropDownList>
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
                                    <asp:TemplateField HeaderText="AStatus">
                                        <ItemTemplate>
                                            <asp:Label ID="txtlvstatus" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Next Recipient">
                                        <ItemTemplate>
                                            <asp:Label ID="lblActionTakenBy" runat="server" Text='<%# Bind("EmpName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblType" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="noofd">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoOfDay" runat="server" Text='<%# Bind("noofdays") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="LockedL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIsLineLocked" runat="server" Text='<%# Bind("IsLineLocked") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="LockedP">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIsProcessLocked" runat="server" Text='<%# Bind("IsProcessLocked") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAction" runat="server" Text='<%# Bind("Action") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PLid">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProcessLevelid" runat="server" Text='<%# Bind("ProcessLevelid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ActionTypeID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblActionTypeID" runat="server" Text='<%# Bind("ActionTypeID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text="Responsible Person"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td class="style24" style="text-align: left">
                                        <asp:DropDownList ID="dplResponsible" runat="server" Width="500px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text="Reason/ Remarks"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td class="style24" style="text-align: left" colspan="2">
                                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td class="style24" colspan="2" style="text-align: left">
                                        <asp:FileUpload ID="file_upload" runat="server" class="multi" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnApply" runat="server" OnClick="btnApply_Click" OnClientClick="ShowConfirmBox(this,'Are you sure to apply leave ?'); return false;" Text="Apply Leave Application" Width="200px" />
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
            
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnApply" />
            <asp:PostBackTrigger ControlID="btnAbsentCount" />
            <asp:PostBackTrigger ControlID="btnCountLate" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
