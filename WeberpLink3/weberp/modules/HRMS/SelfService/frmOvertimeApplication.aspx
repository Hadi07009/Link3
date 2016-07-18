<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmOvertimeApplication.aspx.cs" Inherits="ClientSide_modules_mis_naz_FORMS_HRMS_Forms_frmOvertimeApplication" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        function checkDate(sender, args) {
            if (sender._selectedDate < new Date()) {
                alert("You cannot select a day earlier than today!");
                //sender._selectedDate = new Date(); 
                // set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
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
            text-align: left;
        }
    </style>
    
    <asp:UpdatePanel ID="updtPnl" runat="server">
        <ContentTemplate>
            <div align="center">
                <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                    <asp:Label ID="lblleave" Text="OVERTIME APPLICATION" runat="server" />
                </asp:Panel>
                 <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                     <table width="100%">
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
                                                        <asp:Label ID="Label8" runat="server" Text="Employee Information"></asp:Label>
                                                    </td>
                                                    <td class="style20">&nbsp;</td>
                                                    <td class="style23">&nbsp;</td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="Label16" runat="server" Text="ALL Information"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style9">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td class="style10">
                                                                    <asp:Label ID="Label9" runat="server" Text="ID"></asp:Label>
                                                                </td>
                                                                <td class="style11">:</td>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="lblId" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style10">
                                                                    <asp:Label ID="Label10" runat="server" Text="Name"></asp:Label>
                                                                </td>
                                                                <td class="style11">:</td>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="lblName" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style10">
                                                                    <asp:Label ID="Label11" runat="server" Text="Department"></asp:Label>
                                                                </td>
                                                                <td class="style11">:</td>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="lbldept" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style10">
                                                                    <asp:Label ID="Label12" runat="server" Text="Designation"></asp:Label>
                                                                </td>
                                                                <td class="style11">:</td>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style10">
                                                                    <asp:Label ID="Label13" runat="server" Text="Joining Date"></asp:Label>
                                                                </td>
                                                                <td class="style11">:</td>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="lblJoiningDate" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style10">
                                                                    <asp:Label ID="Label14" runat="server" Text="Period"></asp:Label>
                                                                </td>
                                                                <td class="style11">:</td>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="lblPeriod" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style10">
                                                                    <asp:Label ID="Label15" runat="server" Text="Date"></asp:Label>
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
                                                                    <asp:Label ID="Label17" runat="server" Text="From"></asp:Label>
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
                                                                    <asp:Label ID="Label18" runat="server" Text="To"></asp:Label>
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
                                                                <td class="style21">
                                                                    <asp:Button ID="btnCurrentPeriod" runat="server"
                                                                        OnClick="btnCurrentPeriod_Click" Text="Refresh" Visible="False" Width="75px" />
                                                                </td>
                                                                <td class="style22">&nbsp;</td>
                                                                <td>&nbsp;</td>
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
                                            <asp:GridView ID="grdHoliday" runat="server" AutoGenerateColumns="False"
                                                Width="100%" OnRowDataBound="grdHoliday_RowDataBound" EnableModelValidation="True" ShowFooter="True">
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
                                                            <cc1:CalendarExtender ID="txtntDate1_CalendarExtender0" runat="server"
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
                                                    <asp:BoundField DataField="totalHours" HeaderText="Extra Hour" />
                                                    <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <FooterTemplate>
                                                            <asp:Button ID="btnTotal" runat="server" OnClick="btnTotal_Click" Text="Total" />
                                                        </FooterTemplate>
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
                                                        <asp:Label ID="Label5" runat="server" Text="Responsible Person" Visible="False"></asp:Label>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td class="style24" style="text-align: left">
                                                        <asp:DropDownList ID="dplResponsible" runat="server" Width="250px"
                                                            Visible="False">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="Label7" runat="server" Style="color: #FF3300; font-weight: 700" Text="Over Time at Least 0.5 hours otherwise no application will proceed"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;<asp:Label ID="Label19" runat="server" Text="Reason/ Remarks"></asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td class="style24" style="text-align: left" colspan="2">
                                                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="500px"
                                                            Font-Size="Medium"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td class="style24">
                                                        <asp:Button ID="btnApply" runat="server" OnClick="btnApply_Click"
                                                            Text="Apply" Width="100px" />
                                                    </td>
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
                                CollapsedText="OVERTIME APPLICATION"
                                ExpandedText="OVERTIME APPLICATION"
                                CollapsedSize="2"
                                ExpandedSize="1260"
                                AutoCollapse="False"
                                AutoExpand="False"
                                ScrollContents="true"
                                ImageControlID="Image1"
                                ExpandedImage="~/images/collapse.jpg"
                                CollapsedImage="~/images/expand.jpg"
                                ExpandDirection="Vertical">
                            </cc1:CollapsiblePanelExtender>--%>
                        </div>
             </ContentTemplate>
         <Triggers>
             <asp:PostBackTrigger ControlID="btnApply" />
              </Triggers>
    </asp:UpdatePanel>
</asp:Content>
