<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmOffdayApplicationApproval.aspx.cs" Inherits="modules_HRMS_Approval_frmOffdayApplicationApproval" %>

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
        }

        .style25 {
            width: 109px;
            text-align: left;
            height: 18px;
        }

        .style26 {
            width: 24px;
            height: 18px;
        }
    </style>
    <asp:UpdatePanel ID="updtPnl" runat="server">
        <ContentTemplate>
            <div align="center">
                <asp:Panel ID="pnlSrchHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                    <asp:Label ID="lblSearchHdr" Text="Search" runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnlSrchDet" runat="server" CssClass="cpBodyContent" >
                    <table style="width:99%;text-align:left">
                         <tr>
                             <td></td>
                             <td></td>
                             <td></td>
                         </tr>
                        <tr>
                            <td colspan="3">
                                <asp:GridView ID="GridViewLeavePending" runat="server" AutoGenerateColumns="False"
                                                OnRowDataBound="GridViewLeavePending_RowDataBound" Width="100%"
                                                OnSelectedIndexChanged="GridViewLeavePending_SelectedIndexChanged"
                                                OnPageIndexChanging="GridViewLeavePending_PageIndexChanging"
                                                OnRowCommand="GridViewLeavePending_RowCommand">
                                                <Columns>
                                                    <asp:BoundField DataField="Sl" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="SL #" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TransactionNo" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderText="Referance" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ApplicantId" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderText="Emp ID" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="EmpName"
                                                        HeaderStyle-HorizontalAlign="Left" HeaderText="Name"
                                                        ItemStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Designation"
                                                        HeaderStyle-HorizontalAlign="Left" HeaderText="Designation"
                                                        ItemStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="LeaveType" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderText="Type" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="offday" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderText="Description" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="noofdays" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="Days" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProcessLevelid" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="Level" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ownerofthistask" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderText="On behalf of" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlLoadPermission" runat="server">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnSubmit" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                CommandName="Submit" runat="server" Text="Submit" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                </asp:Panel>
                <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderSrch" runat="server"
                                TargetControlID="pnlSrchDet"
                                CollapseControlID="pnlSrchHdr"
                                ExpandControlID="pnlSrchHdr"
                                Collapsed="false"
                                TextLabelID="lblSearchHdr"
                                CollapsedText="Pending off day Application"
                                ExpandedText="Pending off day Application"                                
                                AutoCollapse="False"
                                AutoExpand="false"
                                ScrollContents="false"
                                ImageControlID="Image1"
                                ExpandedImage="~/images/collapse.jpg"
                                CollapsedImage="~/images/expand.jpg"
                                ExpandDirection="Vertical">
                </cc1:CollapsiblePanelExtender>
                <br />
            </div>
            <asp:Panel ID="Panel50" runat="server" Width="100%">
                <div align="center">
                    <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                        <asp:Label ID="lblleave" Text="LEAVE APPLICATION DETAILS" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" >
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
                                            <td class="style9">
                                                <asp:Label ID="Label7" runat="server" Text="Employee Information"></asp:Label>
                                            </td>
                                            <td class="style20">&nbsp;</td>
                                            <td class="style23">&nbsp;</td>
                                            <td style="text-align: center">
                                                <asp:Label ID="Label15" runat="server" Text="Previous off day inormation"></asp:Label>
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
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label13" runat="server" Text="Period"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblPeriod" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label14" runat="server" Text="Date"></asp:Label>
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
                                                                    <td class="style21">
                                                                        <asp:Label ID="Label16" runat="server" Text="From"></asp:Label>
                                                                    </td>
                                                                    <td class="style22">:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFromDate" runat="server" Width="178px"></asp:TextBox>
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
                                                                        <asp:TextBox ID="txtToDate" runat="server" Width="178px"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server"
                                                                            Format="dd/MM/yyyy" TargetControlID="txtToDate">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style21">&nbsp;</td>
                                                                    <td class="style22">&nbsp;</td>
                                                                    <td>
                                                                        <asp:Button ID="btnShow" runat="server" Text="Show Off day" Width="100px"
                                                                            OnClick="btnShow_Click" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style21">
                                                                        <asp:Button ID="btnprevious" runat="server" OnClick="btnprevious_Click"
                                                                            Text="&lt;&lt;" Visible="False" Width="50px" />
                                                                    </td>
                                                                    <td class="style22">&nbsp;</td>
                                                                    <td>
                                                                        <asp:Button ID="btnShowALL" runat="server" OnClick="btnShowALL_Click"
                                                                            Text="Show ALL" Visible="False" Width="100px" />
                                                                        <asp:Button ID="btnCurrentPeriod" runat="server" OnClick="btnCurrentPeriod_Click"
                                                                            Text="Refresh" Width="100px" />
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
                                                        <asp:BoundField DataField="Sl" HeaderText="SL #" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="Atnd_det_date" DataFormatString="{0:d}" HeaderText="Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="weekdayname" HeaderText="Week Day" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="Atnd_det_offlg" HeaderText="Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="Atnd_det_intime" HeaderText="Sys Intime" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="Atnd_det_outtime" HeaderText="Sys Outtime" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="Atnd_det_hrs" HeaderText="Hours" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:TemplateField HeaderText="Comments" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtlvRemarks" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText=" Actual Intime" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <MKB:TimeSelector ID="timeoff1" runat="server" Date="2013-02-27" Hour="9"
                                                                    DisplaySeconds="False" AmPm="AM">
                                                                </MKB:TimeSelector>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText=" Actual Outtime" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <MKB:TimeSelector ID="timeoff2" runat="server" Date="2013-02-27" Hour="6"
                                                                    DisplaySeconds="False" AmPm="PM">
                                                                </MKB:TimeSelector>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtlvstatus" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="EmpName" HeaderText="Action Taken By" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Checklv" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Type" HeaderText="Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="noofdays" HeaderText="noofd" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="IsLineLocked" HeaderText="LockedL" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="IsProcessLocked" HeaderText="LockedP" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="Action" HeaderText="Action" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="ProcessLevelid" HeaderText="PLid" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="TransactionNo" HeaderText="tno" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="TransactionNoLineNo" HeaderText="tlno" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="ActualIntime" HeaderText="ActualIntime" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="ActualOuttime" HeaderText="ActualOuttime" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />

                                                    </Columns>
                                                </asp:GridView>
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
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td class="style25">
                                                            <asp:Label ID="Label5" runat="server" Text="Assign Person"></asp:Label>
                                                        </td>
                                                        <td class="style26">:</td>
                                                        <td style="text-align: left" colspan="2">
                                                            <asp:Label ID="lblResponsibleperson" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label6" runat="server" Text="Remarks"></asp:Label>
                                                        </td>
                                                        <td>:</td>
                                                        <td class="style24" style="text-align: left">
                                                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="500px"
                                                                Font-Size="Medium"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:Button ID="btnPostLeave" runat="server" OnClick="btnPostLeave_Click"
                                                                Text="Post Leave" />
                                                            <asp:Button ID="btnApply" runat="server" OnClick="btnApply_Click"
                                                                Text="Apply Leave" Width="100px" />
                                                            <asp:Button ID="btnForward" runat="server" OnClick="btnForward_Click"
                                                                Text="Forward" Width="100px" />
                                                            <asp:Button ID="btnReturn" runat="server" OnClick="btnReturn_Click"
                                                                Text="Return" Width="100px" />
                                                            <asp:Button ID="btnReject" runat="server" OnClick="btnReject_Click"
                                                                Text="Reject" Width="100px" />
                                                            <asp:Button ID="btnApprove" runat="server" OnClick="btnApprove_Click"
                                                                Text="Approve" Width="100px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td class="style24" style="text-align: left">
                                                            <asp:TextBox ID="txtRemarksAll" runat="server" TextMode="MultiLine"
                                                                Width="500px"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: left">&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>


                                    </table>
                    </asp:Panel>
                    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server"
                                    TargetControlID="PanelLeavedet"
                                    CollapseControlID="PanelLeaveHdr"
                                    ExpandControlID="PanelLeaveHdr"
                                    Collapsed="false"
                                    TextLabelID="lblleave"
                                    CollapsedText="OFFDAY APPLICATION DETAILS"
                                    ExpandedText="OFFDAY APPLICATION DETAILS"                                    
                                    AutoCollapse="False"
                                    AutoExpand="false"
                                    ScrollContents="false"
                                    ImageControlID="Image1"
                                    ExpandedImage="~/images/collapse.jpg"
                                    CollapsedImage="~/images/expand.jpg"
                                    ExpandDirection="Vertical">
                    </cc1:CollapsiblePanelExtender>
                </div>
            </asp:Panel>
            <br />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnApprove" />
            <asp:PostBackTrigger ControlID="btnApply" />
            <asp:PostBackTrigger ControlID="btnForward" />
            <asp:PostBackTrigger ControlID="btnReject" />
            <asp:PostBackTrigger ControlID="btnApprove" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
