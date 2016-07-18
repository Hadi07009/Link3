<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmAttendanceIndividualView.aspx.cs" Inherits="Modules_HRMS_SelfService_frmAttendanceIndividualView" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script>
        function checkDate(sender, args) {
            if (sender._selectedDate < new Date()) {
                alert("You cannot select a day earlier than today!");
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
    </script>
    <cc2:MessageBox ID="MessageBox1" runat="server" />
    <style type="text/css">
        .cpHeader {
            color: white;
            background-color: #719DDB;
            font: bold 11px auto "Trebuchet MS", Verdana;
            font-size: 12px;
            cursor: pointer;
            width: 450px;
            height: 18px;
            padding: 4px;
        }

        .cpBody {
            background-color: #DCE4F9;
            font: normal 11px auto Verdana, Arial;
            border: 1px gray;
            width: 450px;
            padding: 4px;
            padding-top: 2px;
            height: 0px;
            overflow: hidden;
        }

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

        .auto-style5 {
            width: 170px;
            text-align: left;
        }
        </style>
    <asp:UpdatePanel ID="updtPnl" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="ATTENDANCE" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
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
                                    <td class="style9">
                                        <asp:Label ID="Label17" runat="server" Text="Employee Information"></asp:Label>
                                    </td>
                                    <td class="style20">&nbsp;</td>
                                    <td class="style23">&nbsp;</td>
                                    <td style="text-align: center">
                                        <asp:Label ID="Label6" runat="server" Font-Size="Larger" ForeColor="#FF6600" Text="You can visit all employee's attendance of your section/Department" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style9">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="style10">
                                                    <asp:Label ID="Label7" runat="server" Text="ID"></asp:Label>
                                                </td>
                                                <td class="style11">:</td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblId" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style10">
                                                    <asp:Label ID="Label8" runat="server" Text="Name"></asp:Label>
                                                </td>
                                                <td class="style11">:</td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblName" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style10">
                                                    <asp:Label ID="Label9" runat="server" Text="Department"></asp:Label>
                                                </td>
                                                <td class="style11">:</td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lbldept" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style10">
                                                    <asp:Label ID="Label10" runat="server" Text="Designation"></asp:Label>
                                                </td>
                                                <td class="style11">:</td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style10">
                                                    <asp:Label ID="Label11" runat="server" Text="Joining Date"></asp:Label>
                                                </td>
                                                <td class="style11">:</td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblJoiningDate" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style10">
                                                    <asp:Label ID="Label13" runat="server" Text="Date"></asp:Label>
                                                </td>
                                                <td class="style11">:</td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblcurrentPeriod" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style10">
                                                    &nbsp;</td>
                                                <td class="style11">&nbsp;</td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblPeriod" runat="server" Visible="False"></asp:Label>
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
                                                <td class="auto-style5">
                                                    <asp:Label ID="Label14" runat="server" Text="Select Employee"></asp:Label>
                                                </td>
                                                <td class="style22">:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlEmployeeId" runat="server" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style5">
                                                    <asp:Label ID="Label15" runat="server" Text="Date From"></asp:Label>
                                                </td>
                                                <td class="style22">:</td>
                                                <td>
                                                    <asp:TextBox ID="txtFromDate" runat="server" Width="178px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style5">
                                                    <asp:Label ID="Label16" runat="server" Text="Date To"></asp:Label>
                                                </td>
                                                <td class="style22">:</td>
                                                <td>
                                                    <asp:TextBox ID="txtToDate" runat="server" Width="178px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server"
                                                        Format="dd/MM/yyyy" TargetControlID="txtToDate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style5">
                                                    <asp:Button ID="btnCurrentPeriod" runat="server"
                                                        OnClick="btnCurrentPeriod_Click" Text="Refresh" Visible="False" Width="75px" />
                                                </td>
                                                <td class="style22">&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style5">
                                                    <asp:Button ID="btnprevious" runat="server" OnClick="btnprevious_Click"
                                                        Text="&lt;&lt;" Visible="False" Width="50px" />
                                                </td>
                                                <td class="style22">&nbsp;</td>
                                                <td>
                                                    <asp:Button ID="btnShowALL" runat="server" OnClick="btnShowALL_Click"
                                                        Text="View Attendance" Width="110px" />
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
                            <asp:Button ID="btnExport" runat="server" CssClass="btn2" OnClick="btnExport_Click" Text="Export To Excel" Width="150px" />
                            <asp:Button ID="btnPreview" runat="server" OnClick="btnPreview_Click" Text="Preview" Width="150px" />
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>

                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="grdHoliday" runat="server" AutoGenerateColumns="False"
                                Width="100%" OnRowDataBound="grdHoliday_RowDataBound" ShowFooter="True">
                                <Columns>

                                    <asp:BoundField DataField="Sl" HeaderText="SL #"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Atnd_det_date" DataFormatString="{0:d}"
                                        HeaderText="Date" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="weekdayname" HeaderText="Day Name"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Atnd_det_offlg" HeaderText="Description"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Atnd_det_intime" HeaderText="In Time"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Atnd_det_outtime" HeaderText="Out Time"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        DataFormatString="{0:d}">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Atnd_det_hrs" HeaderText="Hours"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText=" Actual Intime" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <MKB:TimeSelector ID="timeoff1" runat="server"
                                                DisplaySeconds="False" AmPm="AM" Hour="9">
                                            </MKB:TimeSelector>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Out Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtntDate1" runat="server" Text='<%# Eval("atnd_det_date", "{0:d}") %>'> &gt;</asp:TextBox>
                                            <cc1:CalendarExtender ID="txtntDate1_CalendarExtender0" runat="server" OnClientDateSelectionChanged="checkDate"
                                                Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtntDate1">
                                            </cc1:CalendarExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Actual Outtime" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <MKB:TimeSelector ID="timeoff2" runat="server" Date="2013-02-27" Hour="6"
                                                DisplaySeconds="False" AmPm="PM">
                                            </MKB:TimeSelector>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="EmpName" HeaderText="Action Taken By"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="totalHours" HeaderText="Extra Hour(>=30 minutes)" HeaderStyle-Width="120px" />
                                    <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckRet" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="LockedL"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="LockedP"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Action"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        DataField="Action">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="PLid"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        DataField="ProcessLevelid">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="actInTime" HeaderText="actin"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="actOutTime" HeaderText="actout"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">

                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="totalMinutes" HeaderText="Total Minutes" />
                                    <asp:BoundField DataField="totalLessHours" HeaderText="Less Hour" />
                                    <asp:BoundField DataField="LateMinute" HeaderText="Late By (Minutes)" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="EarlyMInute" HeaderText="Early By (Minutes)" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="Reason" HeaderText="Remarks" />
                                    <asp:BoundField DataField="totalLessMinutes" HeaderText="Less Total Minutes" />
                                    <asp:BoundField DataField="HolidayDesc" HeaderText="Holiday Desc " />

                                    <asp:BoundField DataField="totalWorkingMinutes" HeaderText="Total Working Minutes" />

                                </Columns>
                            </asp:GridView>
                        </td>
                        <td>&nbsp;</td>
                    </tr>


                    <tr>
                        <td colspan="3">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style7">
                                        <asp:Label ID="Label5" runat="server" Text="Responsible Person" Visible="False"></asp:Label>
                                    </td>
                                    <td class="style8">&nbsp;</td>
                                    <td class="style24" style="text-align: left">
                                        <asp:DropDownList ID="dplResponsible" runat="server" Width="250px"
                                            Visible="False">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style7">&nbsp;</td>
                                    <td class="style8">&nbsp;</td>
                                    <td class="style24" style="text-align: left" colspan="2">
                                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="500px"
                                            Font-Size="Medium" Visible="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style7">&nbsp;</td>
                                    <td class="style8">&nbsp;</td>
                                    <td class="style24">
                                        <asp:Button ID="btnApply" runat="server" OnClick="btnApply_Click"
                                            Text="Apply" Width="100px" CssClass="btn2" Visible="False" />
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
            <asp:PostBackTrigger ControlID="btnExport" />
            <asp:PostBackTrigger ControlID="btnPreview" />

        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
