<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmApproval.aspx.cs" Inherits="modules_HRMS_Approval_frmApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function callForSearch() {
            var searchText = document.getElementById("<%=txtForSearchIntogrd.ClientID %>").value.toUpperCase();
        var gvDrv = document.getElementById("<%=GridViewLeavePending.ClientID %>");
        for (i = 1; i < gvDrv.rows.length; i++) {
            if (searchText == "") {
                gvDrv.rows[i].style.display = '';
            }
            else {
                if ((gvDrv.rows[i].cells[0].innerHTML).toString().match(searchText) ||
                (gvDrv.rows[i].cells[1].innerHTML).toString().toUpperCase().match(searchText) ||
                (gvDrv.rows[i].cells[2].innerHTML).toString().toUpperCase().match(searchText) ||
                (gvDrv.rows[i].cells[3].innerHTML).toString().toUpperCase().match(searchText) ||
                (gvDrv.rows[i].cells[4].innerHTML).toString().toUpperCase().match(searchText) ||
                (gvDrv.rows[i].cells[5].innerHTML).toString().toUpperCase().match(searchText) ||
                (gvDrv.rows[i].cells[6].innerHTML).toString().toUpperCase().match(searchText) ||
                (gvDrv.rows[i].cells[7].innerHTML).toString().toUpperCase().match(searchText)

                ) {
                    gvDrv.rows[i].style.display = '';
                }
                else {
                    gvDrv.rows[i].style.display = "none";
                }
            }
        }
    }
    </script>
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
                    <asp:Label ID="lblSearchHdr" Text="Pending Applications" runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnlSrchDet" runat="server" CssClass="cpBodyContent" >
                    <table style="width:99%;text-align:left">
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td >
                                    <div style="float: right;vertical-align:top">
                                        <asp:Label ID="Label27" runat="server" Text="Search :" Width="65px"></asp:Label>
                                        <asp:TextBox ID="txtForSearchIntogrd" runat="server" onkeyup="callForSearch();" Width="150px" ></asp:TextBox>
                                        <asp:Button ID="btnSearchIntogrd" runat="server" OnClick="btnSearchIntogrd_Click" Text="Search" Visible="False" Width="85px" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:GridView ID="GridViewLeavePending" runat="server" AutoGenerateColumns="False" DataKeyNames="LeaveType,ProcessLevelid,ProcessId,ProcessFlowId" OnPageIndexChanging="GridViewLeavePending_PageIndexChanging" OnRowCommand="GridViewLeavePending_RowCommand" OnRowDataBound="GridViewLeavePending_RowDataBound" OnSelectedIndexChanged="GridViewLeavePending_SelectedIndexChanged" Width="100%">
                                        <PagerStyle HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="Sl" HeaderStyle-HorizontalAlign="Center" HeaderText="SL #" ItemStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TransactionNo" HeaderStyle-HorizontalAlign="Left" HeaderText="Referance" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ApplicantId" HeaderStyle-HorizontalAlign="Left" HeaderText="Emp ID" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EmpName" HeaderStyle-HorizontalAlign="Left" HeaderText="Name" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Designation" HeaderStyle-HorizontalAlign="Left" HeaderText="Designation" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LeaveType" HeaderStyle-HorizontalAlign="Left" HeaderText="Type" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Overtime" HeaderStyle-HorizontalAlign="Left" HeaderText="Process Type" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="noofdays" HeaderStyle-HorizontalAlign="Center" HeaderText="Duration" ItemStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProcessLevelid" HeaderStyle-HorizontalAlign="Center" HeaderText="Level" ItemStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ownerofthistask" HeaderText="Responsible Person" />
                                            <asp:BoundField DataField="ProcessId" HeaderText="ProcessId" />
                                            <asp:BoundField DataField="ProcessFlowId" HeaderText="ProcessFlowId" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlActionType" runat="server" Width="150px">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnSubmitApproval" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Submit" Text="Submit" />
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
                        AutoCollapse="False" 
                        AutoExpand="false"
                        CollapseControlID="pnlSrchHdr"
                        Collapsed="false"                         
                        CollapsedImage="~/images/expand.jpg" 
                        CollapsedText="Pending Applications" 
                        ExpandControlID="pnlSrchHdr" 
                        ExpandDirection="Vertical" 
                        ExpandedImage="~/images/collapse.jpg" 
                        ExpandedText="Pending Applications" 
                        ImageControlID="Image1" 
                        ScrollContents="false" 
                        TargetControlID="pnlSrchDet" 
                        TextLabelID="lblSearchHdr">
                    </cc1:CollapsiblePanelExtender>
                <br />
            </div>
            <asp:Panel ID="Panel50" runat="server" Width="100%">
                <div align="center">
                    <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                        <asp:Label ID="lblleave" Text="OVERTIME APPLICATION DETAILS" runat="server" />
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
                                                 <asp:Label ID="Label32" runat="server" Text="Employee Information"></asp:Label>
                                             </td>
                                                        <td class="style20">&nbsp;</td>
                                                        <td class="style23">&nbsp;</td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="Label40" runat="server" Text="Previous Leave/ALL Information"></asp:Label>
                                             </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style9">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label33" runat="server" Text="ID"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblId" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label34" runat="server" Text="Name"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblName" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label35" runat="server" Text="Department"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lbldept" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label36" runat="server" Text="Designation"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label37" runat="server" Text="Joining Date"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblJoiningDate" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label38" runat="server" Text="Period"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblPeriod" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label39" runat="server" Text="Date"></asp:Label>
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
                                                                        <asp:Label ID="Label41" runat="server" Text="From"></asp:Label>
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
                                                                        <asp:Label ID="Label42" runat="server" Text="To"></asp:Label>
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
                                                                        <asp:Button ID="btnShow" runat="server" Text="Show Leave" Width="100px"
                                                                            OnClick="btnShow_Click" Visible="False" />
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
                                                                            Text="Show ALL" Width="100px" />
                                                                        <asp:Button ID="btnCurrentPeriod" runat="server" OnClick="btnCurrentPeriod_Click"
                                                                            Text="Refresh" Width="100px" Visible="False" />
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
                                                <asp:GridView ID="grdOvertime" runat="server" AutoGenerateColumns="False"
                                                    Width="100%" OnRowDataBound="grdOvertime_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="Sl" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderText="SL #" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Atnd_det_date" DataFormatString="{0:d}"
                                                            HeaderStyle-HorizontalAlign="Center" HeaderText="Date"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="weekdayname" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderText="Day Name" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Atnd_det_offlg" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderText="Description" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Sys_det_intime"
                                                            HeaderStyle-HorizontalAlign="Center" HeaderText="In Time"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Sys_det_outtime" DataFormatString="{0:d}"
                                                            HeaderStyle-HorizontalAlign="Center" HeaderText="Out Time"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Atnd_det_hrs" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderText="Hours" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center"
                                                            HeaderText=" Actual Intime" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <MKB:TimeSelector ID="timeoff1" runat="server" AmPm="AM" DisplaySeconds="False"
                                                                    Hour="9">
                                                                </MKB:TimeSelector>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Out Date">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtntDate1" runat="server"
                                                                    Text='<%# Eval("atnd_det_date", "{0:d}") %>'> &gt;</asp:TextBox>
                                                                <cc1:CalendarExtender ID="txtntDate1_CalendarExtender0" runat="server"
                                                                    Enabled="True" Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate"
                                                                    TargetControlID="txtntDate1">
                                                                </cc1:CalendarExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center"
                                                            HeaderText=" Actual Outtime" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <MKB:TimeSelector ID="timeoff2" runat="server" AmPm="PM" Date="2013-02-27"
                                                                    DisplaySeconds="False" Hour="6">
                                                                </MKB:TimeSelector>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Status"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblstatus" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="EmpName" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Action Taken By" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Select"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CheckRet" runat="server" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderText="LockedL"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderText="LockedP"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderText="Action"
                                                            ItemStyle-HorizontalAlign="Center" DataField="Action">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProcessLevelid" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderText="PLid" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderText="actin" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderText="actout" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TransactionNo" HeaderText="tno" />
                                                        <asp:BoundField DataField="TransactionNoLineNo" HeaderText="tlno" />
                                                        <asp:BoundField DataField="noofdays" HeaderText="Overtime" />
                                                        <asp:BoundField DataField="Atnd_det_intime" HeaderText="DActualIntime" />
                                                        <asp:BoundField DataField="Atnd_det_outtime" HeaderText="DActualOuttime" />
                                                    </Columns>
                                                </asp:GridView>
                                                <br />
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
                                                            <asp:Label ID="Label5" runat="server" Text="Responsible Person"></asp:Label>
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
                                                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:Button ID="btnPostLeave" runat="server" OnClick="btnPostLeave_Click"
                                                                Text="Post Leave" Visible="False" />
                                                            <asp:Button ID="btnApply" runat="server" OnClick="btnApply_Click"
                                                                Text="Apply Leave" Width="100px" Visible="False" />
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
                                                        <td style="text-align: left">&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
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
                                    CollapsedText="OVERTIME APPLICATION DETAILS"
                                    ExpandedText="OVERTIME APPLICATION DETAILS"                                    
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
            <asp:Panel ID="Panel1" runat="server" Width="100%">
                <div align="center">
                    <asp:Panel ID="PanelNightHeader" runat="server" CssClass="cpHeaderContent" Width="100%">
                        <asp:Label ID="lblNightForColops" Text="NIGHT APPLICATION DETAILS" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="Panel3" runat="server" CssClass="cpBodyContent" >
                        <table style="width:99%;text-align:left">
                            <tr>
                                <td colspan="3">
                                    <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#3333FF"></asp:Label>
                                    <br />
                                </td>
                                 <td>&nbsp;</td>
                                 </tr>
                            <tr>
                                <td colspan="3">
                                    <table style="width: 100%;">
                                                    <tr>
                                                        <td class="style9">
                                                            <asp:Label ID="Label43" runat="server" Text="Employee Information"></asp:Label>
                                                        </td>
                                                        <td class="style20">&nbsp;</td>
                                                        <td class="style23">&nbsp;</td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="Label51" runat="server" Text="Previous Leave/ALL Information"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style9">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label44" runat="server" Text="ID"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblIdN" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label45" runat="server" Text="Name"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblNameN" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label46" runat="server" Text="Department"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblDepartmentN" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label47" runat="server" Text="Designation"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblDesignationN" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label48" runat="server" Text="Joining Date"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblJoiningDateN" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label49" runat="server" Text="Period"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblPeriodN" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label50" runat="server" Text="Date"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblDateN" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="style20">
                                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
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
                                                            <asp:Image ID="Image2" runat="server" Height="100px" Visible="False"
                                                                Width="100px" />
                                                        </td>
                                                        <td style="text-align: right">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td class="style21">
                                                                        <asp:Label ID="Label52" runat="server" Text="From"></asp:Label>
                                                                    </td>
                                                                    <td class="style22">:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox1" runat="server" Width="178px"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                                            Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style21">
                                                                        <asp:Label ID="Label53" runat="server" Text="To"></asp:Label>
                                                                    </td>
                                                                    <td class="style22">:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox2" runat="server" Width="178px"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                                                                            Format="dd/MM/yyyy" TargetControlID="txtToDate">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style21">&nbsp;</td>
                                                                    <td class="style22">&nbsp;</td>
                                                                    <td>
                                                                        <asp:Button ID="Button1" runat="server" Text="Show Leave" Width="100px"
                                                                            OnClick="btnShow_Click" Visible="False" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style21">
                                                                        <asp:Button ID="Button2" runat="server" OnClick="btnprevious_Click"
                                                                            Text="&lt;&lt;" Visible="False" Width="50px" />
                                                                    </td>
                                                                    <td class="style22">&nbsp;</td>
                                                                    <td>
                                                                        <asp:Button ID="Button3" runat="server" OnClick="btnShowALL_Click"
                                                                            Text="Show ALL" Width="100px" />
                                                                        <asp:Button ID="Button4" runat="server" OnClick="btnCurrentPeriod_Click"
                                                                            Text="Refresh" Width="100px" Visible="False" />
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
                                                <asp:GridView ID="grdNight" runat="server" AutoGenerateColumns="False"
                                                    Width="100%" OnRowDataBound="grdNight_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="Sl" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderText="SL #" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Atnd_det_date" DataFormatString="{0:d}"
                                                            HeaderStyle-HorizontalAlign="Center" HeaderText="Date"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="weekdayname" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderText="Day Name" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Atnd_det_offlg" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderText="Description" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Sys_det_intime"
                                                            HeaderStyle-HorizontalAlign="Center" HeaderText="In Time"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Sys_det_outtime" DataFormatString="{0:d}"
                                                            HeaderStyle-HorizontalAlign="Center" HeaderText="Out Time"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Atnd_det_hrs" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderText="Hours" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center"
                                                            HeaderText=" Actual Intime" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <MKB:TimeSelector ID="timeoff1" runat="server" AmPm="AM" DisplaySeconds="False"
                                                                    Hour="9">
                                                                </MKB:TimeSelector>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Out Date">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtntDate1" runat="server"
                                                                    Text='<%# Eval("actTrnOutdate", "{0:d}") %>'> &gt;</asp:TextBox>
                                                                <cc1:CalendarExtender ID="txtntDate1_CalendarExtender0" runat="server"
                                                                    Enabled="True" Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate"
                                                                    TargetControlID="txtntDate1">
                                                                </cc1:CalendarExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center"
                                                            HeaderText=" Actual Outtime" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <MKB:TimeSelector ID="timeoff2" runat="server" AmPm="PM" Date="2013-02-27"
                                                                    DisplaySeconds="False" Hour="6">
                                                                </MKB:TimeSelector>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Status"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblstatus" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="EmpName" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Action Taken By" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Select"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CheckRet" runat="server" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderText="LockedL"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderText="LockedP"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderText="Action"
                                                            ItemStyle-HorizontalAlign="Center" DataField="Action">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProcessLevelid" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderText="PLid" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderText="actin" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderText="actout" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TransactionNo" HeaderText="tno" />
                                                        <asp:BoundField DataField="TransactionNoLineNo" HeaderText="tlno" />
                                                        <asp:BoundField DataField="noofdays" HeaderText="Overtime" />
                                                        <asp:BoundField DataField="Atnd_det_intime" HeaderText="DActualIntime" />
                                                        <asp:BoundField DataField="Atnd_det_outtime" HeaderText="DActualOuttime" />
                                                    </Columns>
                                                </asp:GridView>
                                                <br />
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
                                                            <asp:Label ID="Label13" runat="server" Text="Responsible Person"></asp:Label>
                                                        </td>
                                                        <td class="style26">:</td>
                                                        <td style="text-align: left" colspan="2">
                                                            <asp:Label ID="Label14" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label15" runat="server" Text="Remarks"></asp:Label>
                                                        </td>
                                                        <td>:</td>
                                                        <td class="style24" style="text-align: left">
                                                            <asp:TextBox ID="txtRemarksNight" runat="server" TextMode="MultiLine"
                                                                Width="500px"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:Button ID="Button5" runat="server" OnClick="btnPostLeave_Click"
                                                                Text="Post Leave" Visible="False" />
                                                            <asp:Button ID="btnApplyNight" runat="server" OnClick="btnApply_Click"
                                                                Text="Apply Leave" Width="100px" Visible="False" />
                                                            <asp:Button ID="btnForwardNight" runat="server" OnClick="btnForwardNight_Click"
                                                                Text="Forward" Width="100px" />
                                                            <asp:Button ID="btnReturnNight" runat="server" OnClick="btnReturnNight_Click"
                                                                Text="Return" Width="100px" />
                                                            <asp:Button ID="btnRejectNight" runat="server" OnClick="btnRejectNight_Click"
                                                                Text="Reject" Width="100px" />
                                                            <asp:Button ID="btnApproveNight" runat="server" OnClick="btnApproveNight_Click"
                                                                Text="Approve" Width="100px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td class="style24" style="text-align: left">
                                                            <asp:TextBox ID="txtRemarksAllNight" runat="server" TextMode="MultiLine"
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
                                                        <td style="text-align: left">&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
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
                    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server"
                                    TargetControlID="Panel3"
                                    CollapseControlID="PanelNightHeader"
                                    ExpandControlID="PanelNightHeader"
                                    Collapsed="false"
                                    TextLabelID="lblNightForColops"
                                    CollapsedText="NIGHT APPLICATION DETAILS"
                                    ExpandedText="NIGHT APPLICATION DETAILS"                                    
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
            <asp:Panel ID="Panel2" runat="server" Width="100%">
                <div align="center">
                    <asp:Panel ID="PanelLeaveDetailsHeader" runat="server" CssClass="cpHeaderContent" Width="100%">
                        <asp:Label ID="lblLeaveForColops" Text="LEAVE APPLICATION DETAILS" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="Panel5" runat="server" CssClass="cpBodyContent" >
                        <table style="width:99%;text-align:left">
                            <tr>
                                <td colspan="3">
                                    <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="#3333FF"></asp:Label>
                                    <br />
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                                        <tr>
                                            <td colspan="3">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td class="style9">
                                                            <asp:Label ID="Label54" runat="server" Text="Employee Information"></asp:Label>
                                                        </td>
                                                        <td class="style20">&nbsp;</td>
                                                        <td class="style23">&nbsp;</td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="Label62" runat="server" Text="Previous Leave/ALL Information"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style9">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label55" runat="server" Text="ID"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblIdL" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label56" runat="server" Text="Name"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblNameL" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label57" runat="server" Text="Department"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblDepartmentL" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label58" runat="server" Text="Designation"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblDesignationL" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label59" runat="server" Text="Joining Date"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblJoiningDateL" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label60" runat="server" Text="Period"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblPeriodL" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label61" runat="server" Text="Date"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblDateL" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="style20">
                                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
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
                                                            <asp:Image ID="Image3" runat="server" Height="100px" Visible="False"
                                                                Width="100px" />
                                                        </td>
                                                        <td style="text-align: right">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td class="style21">
                                                                        <asp:Label ID="Label63" runat="server" Text="From"></asp:Label>
                                                                    </td>
                                                                    <td class="style22">:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox3" runat="server" Width="178px"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server"
                                                                            Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style21">
                                                                        <asp:Label ID="Label64" runat="server" Text="To"></asp:Label>
                                                                    </td>
                                                                    <td class="style22">:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox4" runat="server" Width="178px"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server"
                                                                            Format="dd/MM/yyyy" TargetControlID="txtToDate">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style21">&nbsp;</td>
                                                                    <td class="style22">&nbsp;</td>
                                                                    <td>
                                                                        <asp:Button ID="Button7" runat="server" Text="Show Leave" Width="100px"
                                                                            OnClick="btnShow_Click" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style21">
                                                                        <asp:Button ID="Button8" runat="server" OnClick="btnprevious_Click"
                                                                            Text="&lt;&lt;" Visible="False" Width="50px" />
                                                                    </td>
                                                                    <td class="style22">&nbsp;</td>
                                                                    <td>
                                                                        <asp:Button ID="Button9" runat="server" OnClick="btnShowALL_Click"
                                                                            Text="Show ALL" Visible="False" Width="100px" />
                                                                        <asp:Button ID="Button10" runat="server" OnClick="btnCurrentPeriod_Click"
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
                                                        <asp:BoundField DataField="Atnd_det_intime" HeaderText="Intime" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="Atnd_det_outtime" HeaderText="Outtime" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="Atnd_det_hrs" HeaderText="Hours" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:TemplateField HeaderText="Comments" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtlvRemarks" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Leave" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="dpllday" runat="server"></asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Leave Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="lvtype" runat="server"></asp:DropDownList>
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
                                                            <asp:Label ID="Label17" runat="server" Text="Responsible Person"></asp:Label>
                                                        </td>
                                                        <td class="style26">:</td>
                                                        <td style="text-align: left" colspan="2">
                                                            <asp:Label ID="lblResponsiblepersonDuringLeave" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label19" runat="server" Text="Remarks"></asp:Label>
                                                        </td>
                                                        <td>:</td>
                                                        <td class="style24" style="text-align: left">
                                                            <asp:TextBox ID="txtRemarksLeave" runat="server" TextMode="MultiLine"
                                                                Width="500px"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:Button ID="Button11" runat="server" OnClick="btnPostLeave_Click"
                                                                Text="Post Leave" Visible="False" />
                                                            <asp:Button ID="Button12" runat="server" OnClick="btnApply_Click"
                                                                Text="Apply Leave" Width="100px" Visible="False" />
                                                            <asp:Button ID="btnForwardLeave" runat="server" OnClick="btnForwardLeave_Click"
                                                                Text="Forward" Width="100px" />
                                                            <asp:Button ID="btnReturnLeave" runat="server" OnClick="btnReturnLeave_Click"
                                                                Text="Return" Width="100px" />
                                                            <asp:Button ID="btnRejectLeave" runat="server" OnClick="btnRejectLeave_Click"
                                                                Text="Reject" Width="100px" />
                                                            <asp:Button ID="btnApproveLeave" runat="server" OnClick="btnApproveLeave_Click"
                                                                Text="Approve" Width="100px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td class="style24" style="text-align: left">
                                                            <asp:TextBox ID="txtRemarksAllLeave" runat="server" TextMode="MultiLine"
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
                                                        <td style="text-align: left">&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
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
                    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server"
                                    TargetControlID="Panel5"
                                    CollapseControlID="PanelLeaveDetailsHeader"
                                    ExpandControlID="PanelLeaveDetailsHeader"
                                    Collapsed="false"
                                    TextLabelID="lblLeaveForColops"
                                    CollapsedText="LEAVE APPLICATION DETAILS"
                                    ExpandedText="LEAVE APPLICATION DETAILS"                                    
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
            <asp:Panel ID="Panel4" runat="server" Width="100%">
                <div align="center">
                    <asp:Panel ID="PanelOffdayDetailsHeader" runat="server" CssClass="cpHeaderContent" Width="100%">
                        <asp:Label ID="lblOffdayForColops" Text="OFFDAY APPLICATION DETAILS" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="Panel7" runat="server" CssClass="cpBodyContent" >
                        <table style="width:99%;text-align:left">
                            <tr>
                                <td colspan="3">
                                    <asp:Label ID="Label7" runat="server" Font-Bold="True" ForeColor="#3333FF"></asp:Label>
                                     <br />
                                </td>
                                <td>&nbsp;</td>
                                 </tr>
                            <tr>
                                <td colspan="3">
                                    <table style="width: 100%;">
                                                    <tr>
                                                        <td class="style9">
                                                            <asp:Label ID="Label65" runat="server" Text="Employee Information"></asp:Label>
                                                        </td>
                                                        <td class="style20">&nbsp;</td>
                                                        <td class="style23">&nbsp;</td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="Label73" runat="server" Text="Previous off day inormation"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style9">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label66" runat="server" Text="ID"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblIdOD" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label67" runat="server" Text="Name"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblNameOD" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label68" runat="server" Text="Department"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblDepartmentOD" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label69" runat="server" Text="Designation"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblDesignationOD" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label70" runat="server" Text="Joining Date"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblJoiningDateOD" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label71" runat="server" Text="Period"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblPeriodOD" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label72" runat="server" Text="Date"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblDateOD" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="style20">
                                                            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False"
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
                                                            <asp:Image ID="Image4" runat="server" Height="100px" Visible="False"
                                                                Width="100px" />
                                                        </td>
                                                        <td style="text-align: right">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td class="style21">
                                                                        <asp:Label ID="Label74" runat="server" Text="From"></asp:Label>
                                                                    </td>
                                                                    <td class="style22">:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox5" runat="server" Width="178px"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender5" runat="server"
                                                                            Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style21">
                                                                        <asp:Label ID="Label75" runat="server" Text="To"></asp:Label>
                                                                    </td>
                                                                    <td class="style22">:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox6" runat="server" Width="178px"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender6" runat="server"
                                                                            Format="dd/MM/yyyy" TargetControlID="txtToDate">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style21">&nbsp;</td>
                                                                    <td class="style22">&nbsp;</td>
                                                                    <td>
                                                                        <asp:Button ID="Button13" runat="server" Text="Show Off day" Width="100px"
                                                                            OnClick="btnShow_Click" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style21">
                                                                        <asp:Button ID="Button14" runat="server" OnClick="btnprevious_Click"
                                                                            Text="&lt;&lt;" Visible="False" Width="50px" />
                                                                    </td>
                                                                    <td class="style22">&nbsp;</td>
                                                                    <td>
                                                                        <asp:Button ID="Button15" runat="server" OnClick="btnShowALL_Click"
                                                                            Text="Show ALL" Visible="False" Width="100px" />
                                                                        <asp:Button ID="Button16" runat="server" OnClick="btnCurrentPeriod_Click"
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
                                                <asp:GridView ID="GridViewOffday" runat="server" AutoGenerateColumns="False"
                                                    Width="100%" OnRowDataBound="GridViewOffday_RowDataBound">
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
                                                        <asp:BoundField DataField="weekdayname" HeaderText="Week Day"
                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Atnd_det_offlg" HeaderText="Status"
                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Atnd_det_intime" HeaderText="Sys Intime"
                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Atnd_det_outtime" HeaderText="Sys Outtime"
                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Atnd_det_hrs" HeaderText="Hours"
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
                                                        <asp:TemplateField HeaderText=" Actual Intime" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <MKB:TimeSelector ID="timeoff1" runat="server" Date="2013-02-27" Hour="9"
                                                                    DisplaySeconds="False" AmPm="AM">
                                                                </MKB:TimeSelector>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
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
                                                        <asp:BoundField DataField="TransactionNo" HeaderText="tno"
                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TransactionNoLineNo" HeaderText="tlno"
                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ActualIntime" HeaderText="ActualIntime"
                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ActualOuttime" HeaderText="ActualOuttime"
                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">

                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>

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
                                                            <asp:Label ID="Label20" runat="server" Text="Assign Person"></asp:Label>
                                                        </td>
                                                        <td class="style26">:</td>
                                                        <td style="text-align: left" colspan="2">
                                                            <asp:Label ID="lblResponsiblepersonDuringOffday" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label22" runat="server" Text="Remarks"></asp:Label>
                                                        </td>
                                                        <td>:</td>
                                                        <td class="style24" style="text-align: left">
                                                            <asp:TextBox ID="txtRemarksOffday" runat="server" TextMode="MultiLine" Width="500px"
                                                                Font-Size="Medium"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:Button ID="Button17" runat="server" OnClick="btnPostLeave_Click"
                                                                Text="Post Leave" Visible="False" />
                                                            <asp:Button ID="Button18" runat="server" OnClick="btnApply_Click"
                                                                Text="Apply Leave" Width="100px" Visible="False" />
                                                            <asp:Button ID="btnForwardOffday" runat="server" OnClick="btnForwardOffday_Click"
                                                                Text="Forward" Width="100px" />
                                                            <asp:Button ID="btnReturnOffday" runat="server" OnClick="btnReturnOffday_Click"
                                                                Text="Return" Width="100px" />
                                                            <asp:Button ID="btnRejectOffday" runat="server" OnClick="btnRejectOffday_Click"
                                                                Text="Reject" Width="100px" />
                                                            <asp:Button ID="btnApproveOffday" runat="server" OnClick="btnApproveOffday_Click"
                                                                Text="Approve" Width="100px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td class="style24" style="text-align: left">
                                                            <asp:TextBox ID="txtRemarksAllOffday" runat="server" TextMode="MultiLine"
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
                    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender4" runat="server"
                                    TargetControlID="Panel7"
                                    CollapseControlID="PanelOffdayDetailsHeader"
                                    ExpandControlID="PanelOffdayDetailsHeader"
                                    Collapsed="false"
                                    TextLabelID="lblOffdayForColops"
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
            <asp:Panel ID="Panel6" runat="server" Width="100%">
                <div align="center">
                    <asp:Panel ID="PanelAttendanceDetailsHeader" runat="server" CssClass="cpHeaderContent" Width="100%">
                        <asp:Label ID="Label1" Text="ATTENDANCE APPLICATION DETAILS" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="Panel9" runat="server" CssClass="cpBodyContent" >
                        <table style="width:99%;text-align:left">
                            <tr>
                                <td colspan="3">
                                    <asp:Label ID="Label8" runat="server" Font-Bold="True" ForeColor="#3333FF"></asp:Label>
                                     <br />
                                     </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                     <table style="width: 100%;">
                                                    <tr>
                                                        <td class="style9">
                                                            <asp:Label ID="Label76" runat="server" Text="Employee Information"></asp:Label>
                                                        </td>
                                                        <td class="style20">&nbsp;</td>
                                                        <td class="style23">&nbsp;</td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="Label84" runat="server" Text="Previous OT Information"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style9">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label77" runat="server" Text="ID"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblIdAttendance" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label78" runat="server" Text="Name"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblNameAttendance" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label79" runat="server" Text="Department"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblDepartmentAttendance" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label80" runat="server" Text="Designation"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblDesignationAttendance" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label81" runat="server" Text="Joining Date"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblJoiningDateAttendance" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label82" runat="server" Text="Period"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblPeriodAttendance" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label83" runat="server" Text="Date"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblDateAttendance" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="style20">
                                                            <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False"
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
                                                            <asp:Image ID="Image5" runat="server" Height="100px" Visible="False"
                                                                Width="100px" />
                                                        </td>
                                                        <td style="text-align: right">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td class="style21">
                                                                        <asp:Label ID="Label85" runat="server" Text="From"></asp:Label>
                                                                    </td>
                                                                    <td class="style22">:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox7" runat="server" Width="178px"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender7" runat="server"
                                                                            Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style21">
                                                                        <asp:Label ID="Label86" runat="server" Text="To"></asp:Label>
                                                                    </td>
                                                                    <td class="style22">:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox8" runat="server" Width="178px"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender8" runat="server"
                                                                            Format="dd/MM/yyyy" TargetControlID="txtToDate">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style21">&nbsp;</td>
                                                                    <td class="style22">&nbsp;</td>
                                                                    <td>
                                                                        <asp:Button ID="Button6" runat="server" Text="Show OT" Width="100px"
                                                                            OnClick="btnShow_Click" Visible="False" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style21">
                                                                        <asp:Button ID="Button19" runat="server" OnClick="btnprevious_Click"
                                                                            Text="&lt;&lt;" Visible="False" Width="50px" />
                                                                    </td>
                                                                    <td class="style22">&nbsp;</td>
                                                                    <td>
                                                                        <asp:Button ID="Button20" runat="server" OnClick="btnShowALL_Click"
                                                                            Text="Show ALL" Width="100px" Visible="False" />
                                                                        <asp:Button ID="Button21" runat="server" OnClick="btnCurrentPeriod_Click"
                                                                            Text="Refresh" Width="100px" Visible="False" />
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
                                                <asp:GridView ID="grdAttendance" runat="server" AutoGenerateColumns="False"
                                                    Width="100%" OnRowDataBound="grdAttendance_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="Sl" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderText="SL #" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Atnd_det_date" DataFormatString="{0:d}"
                                                            HeaderStyle-HorizontalAlign="Center" HeaderText="Date"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="weekdayname" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderText="Day Name" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Atnd_det_offlg" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderText="Description" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Sys_det_intime"
                                                            HeaderStyle-HorizontalAlign="Center" HeaderText="In Time"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Sys_det_outtime" DataFormatString="{0:d}"
                                                            HeaderStyle-HorizontalAlign="Center" HeaderText="Out Time"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Atnd_det_hrs" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderText="Hours" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center"
                                                            HeaderText=" Actual Intime" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <MKB:TimeSelector ID="timeoff1" runat="server" AmPm="AM" DisplaySeconds="False"
                                                                    Hour="9">
                                                                </MKB:TimeSelector>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Out Date">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtntDate1" runat="server"
                                                                    Text='<%# Eval("atnd_det_date", "{0:d}") %>'> &gt;</asp:TextBox>
                                                                <cc1:CalendarExtender ID="txtntDate1_CalendarExtender0" runat="server"
                                                                    Enabled="True" Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate"
                                                                    TargetControlID="txtntDate1">
                                                                </cc1:CalendarExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center"
                                                            HeaderText=" Actual Outtime" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <MKB:TimeSelector ID="timeoff2" runat="server" AmPm="PM" Date="2013-02-27"
                                                                    DisplaySeconds="False" Hour="6">
                                                                </MKB:TimeSelector>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Status"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblstatus" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="EmpName" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Action Taken By" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Select"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CheckRet" runat="server" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderText="LockedL"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderText="LockedP"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Action" HeaderStyle-HorizontalAlign="Center" HeaderText="Action"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProcessLevelid" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderText="PLid" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderText="actin" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderText="actout" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TransactionNo" HeaderText="tno" />
                                                        <asp:BoundField DataField="TransactionNoLineNo" HeaderText="tlno" />
                                                        <asp:BoundField DataField="noofdays" HeaderText="Overtime" />
                                                        <asp:BoundField DataField="Atnd_det_intime" HeaderText="DActualIntime" />
                                                        <asp:BoundField DataField="Atnd_det_outtime" HeaderText="DActualOuttime" />
                                                    </Columns>
                                                </asp:GridView>
                                                <br />
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
                                                            <asp:Label ID="Label23" runat="server" Text="Responsible Person"></asp:Label>
                                                        </td>
                                                        <td class="style26">:</td>
                                                        <td style="text-align: left" colspan="2">
                                                            <asp:Label ID="Label24" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label25" runat="server" Text="Remarks"></asp:Label>
                                                        </td>
                                                        <td>:</td>
                                                        <td class="style24" style="text-align: left">
                                                            <asp:TextBox ID="TextBox9" runat="server" TextMode="MultiLine" Width="500px"
                                                                Font-Size="Medium"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:Button ID="Button22" runat="server" OnClick="btnPostLeave_Click"
                                                                Text="Post Leave" Visible="False" />
                                                            <asp:Button ID="Button23" runat="server" OnClick="btnApply_Click"
                                                                Text="Apply Leave" Width="100px" Visible="False" />
                                                            <asp:Button ID="btnForwardAttendance" runat="server" OnClick="btnForwardAttendance_Click"
                                                                Text="Forward" Width="100px" />
                                                            <asp:Button ID="btnReturnAttendance" runat="server" OnClick="btnReturnAttendance_Click"
                                                                Text="Return" Width="100px" />
                                                            <asp:Button ID="btnRejectAttendance" runat="server" OnClick="btnRejectAttendance_Click"
                                                                Text="Reject" Width="100px" />
                                                            <asp:Button ID="btnApproveAttendance" runat="server" OnClick="btnApproveAttendance_Click"
                                                                Text="Approve" Width="100px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td class="style24" style="text-align: left">
                                                            <asp:TextBox ID="txtRemarksAllAttendance" runat="server" TextMode="MultiLine"
                                                                Width="500px" Font-Size="Medium"></asp:TextBox>
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
                                                        <td style="text-align: left">&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>


                                    </table>
                    </asp:Panel>
                    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender5" runat="server"
                                    TargetControlID="Panel9"
                                    CollapseControlID="PanelAttendanceDetailsHeader"
                                    ExpandControlID="PanelAttendanceDetailsHeader"
                                    Collapsed="false"
                                    TextLabelID="lblleave"
                                    CollapsedText="ATTENDANCE APPLICATION DETAILS"
                                    ExpandedText="ATTENDANCE APPLICATION DETAILS"                                    
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
            <asp:Panel ID="Panel8" runat="server" Width="100%">
                <div align="center">
                    <asp:Panel ID="PanelEarnedLeaveDetailsHeader" runat="server" CssClass="cpHeaderContent" Width="100%">
                        <asp:Label ID="lblEarnedLeaveForColops" Text="EARNED LEAVE ENCASH DETAILS" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="Panel11" runat="server" CssClass="cpBodyContent" >
                        <table style="width:99%;text-align:left">
                            <tr>
                                <td colspan="3">
                                    <asp:Label ID="Label10" runat="server" Font-Bold="True" ForeColor="#3333FF"></asp:Label>
                                    <br />
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                             <tr>
                                 <td colspan="3">
                                     <table style="width: 100%;">
                                                    <tr>
                                                        <td class="style9">
                                                            <asp:Label ID="Label87" runat="server" Text="Employee Information"></asp:Label>
                                                        </td>
                                                        <td class="style20">&nbsp;</td>
                                                        <td class="style23">&nbsp;</td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="Label95" runat="server" Text="Previous off day inormation"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style9">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label88" runat="server" Text="ID"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblIdEarnedLeave" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label89" runat="server" Text="Name"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblNameEarnedLeave" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label90" runat="server" Text="Department"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblDepartmentEarnedLeave" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label91" runat="server" Text="Designation"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblDesignationEarnedLeave" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label92" runat="server" Text="Joining Date"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblJoiningDateEarnedLeave" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label93" runat="server" Text="Period"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblPeriodEarnedLeave" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style10">
                                                                        <asp:Label ID="Label94" runat="server" Text="Date"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="lblDateEarnedLeave" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="style20">
                                                            <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False"
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
                                                            <asp:Image ID="Image6" runat="server" Height="100px" Visible="False"
                                                                Width="100px" />
                                                        </td>
                                                        <td style="text-align: right">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td class="style21">
                                                                        <asp:Label ID="Label96" runat="server" Text="From"></asp:Label>
                                                                    </td>
                                                                    <td class="style22">:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox10" runat="server" Width="178px"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender9" runat="server"
                                                                            Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style21">
                                                                        <asp:Label ID="Label97" runat="server" Text="To"></asp:Label>
                                                                    </td>
                                                                    <td class="style22">:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox11" runat="server" Width="178px"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender10" runat="server"
                                                                            Format="dd/MM/yyyy" TargetControlID="txtToDate">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style21">&nbsp;</td>
                                                                    <td class="style22">&nbsp;</td>
                                                                    <td>
                                                                        <asp:Button ID="Button24" runat="server" Text="Show Off day" Width="100px"
                                                                            OnClick="btnShow_Click" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style21">
                                                                        <asp:Button ID="Button25" runat="server" OnClick="btnprevious_Click"
                                                                            Text="&lt;&lt;" Visible="False" Width="50px" />
                                                                    </td>
                                                                    <td class="style22">&nbsp;</td>
                                                                    <td>
                                                                        <asp:Button ID="Button26" runat="server" OnClick="btnShowALL_Click"
                                                                            Text="Show ALL" Visible="False" Width="100px" />
                                                                        <asp:Button ID="Button27" runat="server" OnClick="btnCurrentPeriod_Click"
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
                                                <asp:GridView ID="GridViewEarnedLeave" runat="server" AutoGenerateColumns="False"
                                                    Width="100%" OnRowDataBound="GridViewEarnedLeave_RowDataBound">
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
                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">

                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TransactionNo" HeaderText="tno"
                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">

                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TransactionNoLineNo" HeaderText="tnlno"
                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">

                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>

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
                                                            <asp:Label ID="Label29" runat="server" Text="Assign Person"></asp:Label>
                                                        </td>
                                                        <td class="style26">:</td>
                                                        <td style="text-align: left" colspan="2">
                                                            <asp:Label ID="Label30" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label31" runat="server" Text="Remarks"></asp:Label>
                                                        </td>
                                                        <td>:</td>
                                                        <td class="style24" style="text-align: left">
                                                            <asp:TextBox ID="txtRemarksEarnedLeave" runat="server" TextMode="MultiLine" Width="500px"
                                                                Font-Size="Medium"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:Button ID="Button28" runat="server"
                                                                Text="Post Leave" Visible="False" />
                                                            <asp:Button ID="Button29" runat="server"
                                                                Text="Apply Leave" Width="100px" Visible="False" />
                                                            <asp:Button ID="btnForwardEarnedLeave" runat="server" OnClick="btnForwardEarnedLeave_Click"
                                                                Text="Forward" Width="100px" />
                                                            <asp:Button ID="btnReturnEarnedLeave" runat="server" OnClick="btnReturnEarnedLeave_Click"
                                                                Text="Return" Width="100px" />
                                                            <asp:Button ID="btnRejectEarnedLeave" runat="server" OnClick="btnRejectEarnedLeave_Click"
                                                                Text="Reject" Width="100px" />
                                                            <asp:Button ID="btnApproveEarnedLeave" runat="server" OnClick="btnApproveEarnedLeave_Click"
                                                                Text="Approve" Width="100px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td class="style24" style="text-align: left">
                                                            <asp:TextBox ID="txtRemarksAllEarnedLeave" runat="server" TextMode="MultiLine"
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
                    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender6" runat="server"
                                    TargetControlID="Panel11"
                                    CollapseControlID="PanelEarnedLeaveDetailsHeader"
                                    ExpandControlID="PanelEarnedLeaveDetailsHeader"
                                    Collapsed="false"
                                    TextLabelID="lblEarnedLeaveForColops"
                                    CollapsedText="EARNED LEAVE ENCASH DETAILS"
                                    ExpandedText="EARNED LEAVE ENCASH DETAILS"                                    
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
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
