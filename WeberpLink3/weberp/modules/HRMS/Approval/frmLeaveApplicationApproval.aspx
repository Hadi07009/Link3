<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmLeaveApplicationApproval.aspx.cs" Inherits="modules_HRMS_Approval_frmLeaveApplicationApproval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="TimePicker" namespace="MKB.TimePicker" tagprefix="MKB" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>
    
<%@ Register src="../../../UserControls/ucLeaveDocument.ascx" tagname="ucLeaveDocument" tagprefix="uc1" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <cc1:ConfirmBox ID="ConfirmBox1" runat="server" />  

    <style type="text/css">  
        .ModalBackgroud
        {
            background-color:Gray;
            filter:alpha(opacity=50);
            opacity:0.5;
        }
        .cpHeaderWithOutWidth
            {
                color: white;
                background-color: #719DDB;
                font: bold 15px auto "Trebuchet MS", Verdana;
                font-size: 15px;
                cursor: pointer;
                height:15px;
            }
        .style2
        {
            height: 18px;
        }
        .style7
        {
            width: 109px;
            text-align: left;
        }
        .style8
        {
            width: 24px;
        }
        .style9
        {
            width: 348px;
            text-align: left;
        }
        .style10
        {
            width: 81px;
            text-align: left;
        }
        .style11
        {
            width: 13px;
        }
        .style20
        {
            width: 357px;
        }
        .style21
        {
            width: 83px;
            text-align: left;
        }
        .style22
        {
            width: 15px;
        }
        .style23
        {
            width: 156px;
        }
        .style24
        {
        }
        .style25
        {
            width: 109px;
            text-align: left;
            height: 18px;
        }
        .style26
        {
            width: 24px;
            height: 18px;
        }
        </style>
    <asp:UpdatePanel ID="updtPnl" runat="server">
        <ContentTemplate>
            <div align="center">
                <asp:Panel ID="pnlSrchHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                    PENDING LEAVE APPROVAL
                    </asp:Panel>
                <asp:Panel ID="pnlSrchDet" runat="server" CssClass="cpBodyContent" Height="100%" >
                    <table style="width:99%;text-align:left" >
                         <tr>
                             <td>
                                 &nbsp;</td>
                             <td>
                             </td>
                             <td> 
                             </td> 
                              </tr>
                         <tr>
                             <td colspan="3">
                                        <asp:GridView ID="GridViewLeavePending" runat="server" AutoGenerateColumns="False" 
                                            onrowdatabound="GridViewLeavePending_RowDataBound" Width="100%"                                            
                                            OnRowCommand="GridViewLeavePending_RowCommand" OnSelectedIndexChanged="GridViewLeavePending_SelectedIndexChanged">
                                            <Columns>
                                                <asp:BoundField DataField="Sl" HeaderStyle-HorizontalAlign="Center" 
                                                    HeaderText="SL #" ItemStyle-HorizontalAlign="Center" > 
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                 <asp:BoundField DataField="TransactionNo" HeaderStyle-HorizontalAlign="Left" 
                                                    HeaderText="Referance" ItemStyle-HorizontalAlign="Left" >                                                                                            
                                                     <HeaderStyle HorizontalAlign="Left" />
                                                     <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ApplicantId" HeaderStyle-HorizontalAlign="Left" 
                                                    HeaderText="Emp ID" ItemStyle-HorizontalAlign="Left" >
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EmpName" 
                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Name" 
                                                    ItemStyle-HorizontalAlign="Left" >
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Designation" 
                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Designation" 
                                                    ItemStyle-HorizontalAlign="Left" >
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LeaveType" HeaderStyle-HorizontalAlign="Left" 
                                                    HeaderText="Type" ItemStyle-HorizontalAlign="Left" >
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Leave_Mas_Name" HeaderStyle-HorizontalAlign="Left" 
                                                    HeaderText="Leave Type" ItemStyle-HorizontalAlign="Left" >    
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="noofdays" HeaderStyle-HorizontalAlign="Center" 
                                                    HeaderText="Days" ItemStyle-HorizontalAlign="Center" >                                                                                                                                                                                                  
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>                                                                                              
                                                <asp:BoundField DataField="ProcessLevelid" HeaderStyle-HorizontalAlign="Center" 
                                                    HeaderText="Level" ItemStyle-HorizontalAlign="Center" >                                                                                               
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ownerofthistask" HeaderStyle-HorizontalAlign="Left" 
                                                    HeaderText="Pending To" ItemStyle-HorizontalAlign="Left" >                                                                                               
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                             <asp:CommandField ShowSelectButton="True" />                                       
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                        </table>
                </asp:Panel>
                <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderSrch" runat="server" 
                            TargetControlID="pnlSrchDet" 
                            CollapseControlID="pnlSrchHdr" 
                            ExpandControlID="pnlSrchHdr"
                            Collapsed="false" 
                            TextLabelID="lblSearchHdr" 
                            CollapsedText="Pending Leave Application" 
                            ExpandedText="Pending Leave Application"                            
                            AutoCollapse="False"
                            AutoExpand="false"                            
                            ScrollContents="false"
                            ImageControlID="Image1"
                            ExpandedImage="~/images/collapse.jpg"
                            CollapsedImage="~/images/expand.jpg"
                            ExpandDirection="Vertical" 
                            >
                </cc1:CollapsiblePanelExtender>
                <br />
            </div>
            <asp:Panel ID="Panel50" runat="server" Width="100%">
                <div align="center">
                    <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                        <asp:Label ID="lblleave" Text="LEAVE APPLICATION DETAILS" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Height="100%" >
                        <table style="width:99%;text-align:left">
                             <tr>
                                 <td colspan="3">
                                     <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="#3333FF"></asp:Label>
                                     <br />
                                 </td>
                                 <td>
                                     &nbsp;</td>
                                  </tr>
                                 <tr>
                                     <td colspan="3">
                                         <table style="width:100%;">
                                             <tr>
                                                 <td class="style9">
                                                     <asp:Label ID="Label7" runat="server" Text="Employee Information"></asp:Label>
                                                 </td>
                                                 <td class="style20">
                                                     <asp:Label ID="Label15" runat="server" Text="Leave Summery"></asp:Label>
                                                 </td>
                                                 <td class="style23">
                                                     &nbsp;</td>
                                                 <td style="text-align: center">
                                                     <asp:Label ID="Label16" runat="server" Text="Previous Leave/ALL Information"></asp:Label>
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td class="style9">
                                                     <table style="width:100%;">
                                                         <tr>
                                                             <td class="style10">
                                                                 <asp:Label ID="Label8" runat="server" Text="ID"></asp:Label>
                                                             </td>
                                                             <td class="style11">
                                                                 :</td>
                                                             <td style="text-align: left">
                                                                 <asp:Label ID="lblId" runat="server"></asp:Label>
                                                             </td>
                                                         </tr>
                                                         <tr>
                                                             <td class="style10">
                                                                 <asp:Label ID="Label9" runat="server" Text="Name"></asp:Label>
                                                             </td>
                                                             <td class="style11">
                                                                 :</td>
                                                             <td style="text-align: left">
                                                                 <asp:Label ID="lblName" runat="server"></asp:Label>
                                                             </td>
                                                         </tr>
                                                         <tr>
                                                             <td class="style10">
                                                                 <asp:Label ID="Label10" runat="server" Text="Department"></asp:Label>
                                                             </td>
                                                             <td class="style11">
                                                                 :</td>
                                                             <td style="text-align: left">
                                                                 <asp:Label ID="lbldept" runat="server"></asp:Label>
                                                             </td>
                                                         </tr>
                                                         <tr>
                                                             <td class="style10">
                                                                 <asp:Label ID="Label11" runat="server" Text="Designation"></asp:Label>
                                                             </td>
                                                             <td class="style11">
                                                                 :</td>
                                                             <td style="text-align: left">
                                                                 <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                                                             </td>
                                                         </tr>
                                                         <tr>
                                                             <td class="style10">
                                                                 <asp:Label ID="Label12" runat="server" Text="Joining Date"></asp:Label>
                                                             </td>
                                                             <td class="style11">
                                                                 :</td>
                                                             <td style="text-align: left">
                                                                 <asp:Label ID="lblJoiningDate" runat="server"></asp:Label>
                                                             </td>
                                                         </tr>
                                                         <tr>
                                                             <td class="style10">
                                                                 <asp:Label ID="Label13" runat="server" Text="Period"></asp:Label>
                                                             </td>
                                                             <td class="style11">
                                                                 :</td>
                                                             <td style="text-align: left">
                                                                 <asp:Label ID="lblPeriod" runat="server"></asp:Label>
                                                             </td>
                                                         </tr>
                                                         <tr>
                                                             <td class="style10">
                                                                 <asp:Label ID="Label14" runat="server" Text="Date"></asp:Label>
                                                             </td>
                                                             <td class="style11">
                                                                 :</td>
                                                             <td style="text-align: left">
                                                                 <asp:Label ID="lblcurrentPeriod" runat="server"></asp:Label>
                                                             </td>
                                                         </tr>
                                                     </table>
                                                 </td>
                                                 <td class="style20">
                                                     <asp:GridView ID="gdvLeaveInfo" runat="server" AutoGenerateColumns="False" 
                                                         onrowdatabound="gdvLeaveInfo_RowDataBound">
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
                                                     <table style="width:100%;">
                                                         <tr>
                                                             <td class="style21">
                                                                 <asp:Label ID="Label17" runat="server" Text="From"></asp:Label>
                                                             </td>
                                                             <td class="style22">
                                                                 :</td>
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
                                                             <td class="style22">
                                                                 :</td>
                                                             <td>
                                                                 <asp:TextBox ID="txtToDate" runat="server" Width="178px"></asp:TextBox>
                                                                 <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" 
                                                                    Format="dd/MM/yyyy" TargetControlID="txtToDate">
                                                                 </cc1:CalendarExtender>
                                                                </td>
                                                         </tr>
                                                         <tr>
                                                             <td class="style21">
                                                                 &nbsp;</td>
                                                             <td class="style22">
                                                                 &nbsp;</td>
                                                             <td>
                                                                 <asp:Button ID="btnShow" runat="server" Text="Show Leave" Width="100px" 
                                                                     onclick="btnShow_Click" />
                                                             </td>
                                                         </tr>
                                                         <tr>
                                                             <td class="style21">
                                                                 <asp:Button ID="btnprevious" runat="server" onclick="btnprevious_Click" 
                                                                     Text="&lt;&lt;" Visible="False" Width="50px" />
                                                             </td>
                                                             <td class="style22">
                                                                 &nbsp;</td>
                                                             <td>
                                                                 <asp:Button ID="btnShowALL" runat="server" onclick="btnShowALL_Click" 
                                                                     Text="Show ALL" Visible="False" Width="100px" />
                                                                 <asp:Button ID="btnCurrentPeriod" runat="server" onclick="btnCurrentPeriod_Click" 
                                                                     Text="Refresh" Width="100px" />
                                                             </td>
                                                         </tr>
                                                     </table>
                                                 </td>
                                             </tr>
                                         </table>
                                     </td>
                                     <td>
                                         &nbsp;</td>
                                 </tr>
                   
                                 <tr>
                                     <td>
                                         &nbsp;</td>
                                     <td>
                                         &nbsp;</td>
                                     <td>
                                         &nbsp;</td>
                                     <td>
                                         &nbsp;</td>
                                 </tr>
                   
                                <tr>
                                    <td colspan="3">
                                        <asp:GridView ID="GridViewLeave" runat="server" AutoGenerateColumns="False" 
                                            Width="100%" onrowdatabound="GridViewLeave_RowDataBound" OnSelectedIndexChanged="GridViewLeave_SelectedIndexChanged">
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
                                                        <asp:Label ID="lblOutTime" runat="server" Text='<%# Bind("Atnd_det_outtime") %>'></asp:Label>
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
                                                        <asp:Dropdownlist ID="dpllday" runat="server"></asp:Dropdownlist>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Leave Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Dropdownlist ID="lvtype" runat="server"></asp:Dropdownlist>
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
                                                <asp:TemplateField HeaderText="">
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
                                                        <asp:Label ID="lblNoOfDays" runat="server" Text='<%# Bind("noofdays") %>'></asp:Label>
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
                                                <asp:TemplateField HeaderText="tno">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTransactionNo" runat="server" Text='<%# Bind("TransactionNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="tlno">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTransactionNoLineNo" runat="server" Text='<%# Bind("TransactionNoLineNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    
                                                </asp:TemplateField>
                                                  
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                    
                    
                                 <tr>
                                     <td colspan="3">
                                         <table style="width:100%;">
                                             <tr>
                                                 <td class="style25">
                                                     <asp:Label ID="Label5" runat="server" Text="Responsible Person"></asp:Label>
                                                 </td>
                                                 <td class="style26">
                                                     :</td>
                                                 <td style="text-align: left" colspan="2">
                                                     <asp:Label ID="lblResponsibleperson" runat="server"></asp:Label>
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>
                                                     <asp:Label ID="Label6" runat="server" Text="Remarks"></asp:Label>
                                                 </td>
                                                 <td>
                                                     :</td>
                                                 <td class="style24" style="text-align: left">
                                                     <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                                 </td>
                                                 <td style="text-align: left">
                                                     <asp:Button ID="btnPostLeave" runat="server" onclick="btnPostLeave_Click" 
                                                         Text="Post Leave" />
                                                     <asp:Button ID="btnApply" runat="server" onclick="btnApply_Click" 
                                                         Text="Apply Leave" Width="100px" />
                                                     <asp:Button ID="btnForward" runat="server" onclick="btnForward_Click" OnClientClick="ShowConfirmBox(this,'Are you sure to forward leave ?'); return false;"
                                                         Text="Forward" Width="100px" />
                                                     <asp:Button ID="btnReturn" runat="server" onclick="btnReturn_Click" OnClientClick="ShowConfirmBox(this,'Are you sure to return leave ?'); return false;"
                                                         Text="Return" Width="100px" />
                                                     <asp:Button ID="btnReject" runat="server" onclick="btnReject_Click" OnClientClick="ShowConfirmBox(this,'Are you sure to reject leave ?'); return false;"
                                                         Text="Reject" Width="100px" />
                                                     <asp:Button ID="btnApprove" runat="server" onclick="btnApprove_Click" OnClientClick="ShowConfirmBox(this,'Are you sure to approve leave ?'); return false;"
                                                         Text="Approve" Width="100px" />
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>
                                                     &nbsp;</td>
                                                 <td>
                                                     &nbsp;</td>
                                                 <td class="style24" style="text-align: left">
                                                     <asp:TextBox ID="txtRemarksAll" runat="server" TextMode="MultiLine" 
                                                         Width="500px"></asp:TextBox>
                                                 </td>
                                                 <td style="text-align: left">
                                                     &nbsp;</td>
                                             </tr>
                                             <tr>
                                                 <td>&nbsp;</td>
                                                 <td>&nbsp;</td>
                                                 <td class="style24" style="text-align: left">&nbsp;</td>
                                                 <td style="text-align: left">&nbsp;</td>
                                             </tr>
                                             <tr>
                                                 <td>
                                                     <asp:Label ID="Label19" runat="server" Text="Attachment(If Any)"></asp:Label>
                                                 </td>
                                                 <td>:</td>
                                                 <td class="style24" colspan="2" style="text-align: left">
                                                     <uc1:ucLeaveDocument ID="ucLeaveDocument1" runat="server" />
                                                 </td>
                                             </tr>
                                         </table>
                                     </td>
                                     <td>
                                         &nbsp;</td>
                                 </tr>
                            <tr>
                                    <td colspan="2" style="text-align:left">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                 <tr>
                                     <td colspan="3">
                                         <table style="width:100%;">
                                             <tr>
                                                 <td style="text-align: left">
                                                     &nbsp;</td>
                                                 <td>
                                                     &nbsp;</td>
                                                 <td>
                                                     &nbsp;</td>
                                             </tr>
                                             <tr>
                                                 <td>
                                                     &nbsp;</td>
                                                 <td>
                                                     &nbsp;</td>
                                                 <td>
                                                     &nbsp;</td>
                                             </tr>
                                         </table>
                                     </td>
                                     <td>
                                         &nbsp;</td>
                                 </tr>

                                  <tr>
                     <td>
                         &nbsp;</td>
                     <td>
                                                                
                         <asp:Panel ID="PanelForShowPreviousLeave" runat="server" BackColor="White" Height="585px" style="display:block" Width="700px" ScrollBars="Vertical">
                             <table style="width:100%; border-collapse:collapse" >
                                 <tr align="left" style="background-color:#719DDB; border:solid;border-color:#719DDB" >
                                     <td align="center" width="600px" style ="background-color:#719DDB; border:solid;border-color:#719DDB"  >
                                         <asp:Panel ID="Panel51" runat="server" CssClass="cpHeaderWithOutWidth"  BorderWidth="1px" BorderColor="#719DDB">
                                             <asp:Label ID="lblleave0" runat="server" Text="PREVIOUS LEAVE INFORMATION" />
                                             </asp:Panel>
                                     </td>
                                     <td align="right" width="125px" style ="background-color:#719DDB; border:solid;border-color:#719DDB" class="cpHeaderWithOutWidth" >
                                         <asp:Button ID="btnCancelPopUp" runat="server" Text="Close" Width="70px" />
                                     </td>
                                 </tr>
                                 <tr>
                                     <td colspan="2">&nbsp; </td>
                                 </tr>
                                 <tr>
                                     <td colspan="2">&nbsp;</td>
                                 </tr>
                                 <tr>
                                     <td colspan="2">
                                         <asp:GridView ID="GridViewLeaveForPreviousRecord" runat="server" EmptyDataText="No Data Found" AutoGenerateColumns="False" OnRowDataBound="GridViewLeaveForPreviousRecord_RowDataBound" Width="100%">
                                            <Columns>
                                                <asp:BoundField DataField="Sl" HeaderText="SL #" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Atnd_det_date" DataFormatString="{0:d}" HeaderText="Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Type" HeaderText="Leave Type" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                                                <asp:BoundField DataField="noofdays" HeaderText="No Of Days" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                                                <asp:BoundField DataField="EmpName" HeaderText="Approved By" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                  

                                            </Columns> 
                                         </asp:GridView>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td colspan="2">
                                         <asp:Button ID="btnShowPopup" runat="server" style="display:none" />
                                     </td>
                                 </tr>
                             </table>
                         </asp:Panel>
                         </td>                         
                     <td>
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td>
                         &nbsp;</td>
                     <td>
                         <cc1:ModalPopupExtender ID="PanelForShowPreviousLeave_ModalPopupExtender" runat="server"  Enabled="True" TargetControlID="btnShowPopup" PopupControlID="PanelForShowPreviousLeave"
                             CancelControlID="btnCancelPopUp" BackgroundCssClass="ModalBackgroud">
                         </cc1:ModalPopupExtender>
                     </td>
                     <td>
                         &nbsp;</td>
                 </tr>
                             </table>
                    </asp:Panel>
                    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server" 
                            TargetControlID="PanelLeavedet" 
                            CollapseControlID="PanelLeaveHdr" 
                            ExpandControlID="PanelLeaveHdr"
                            Collapsed="false" 
                            TextLabelID="lblleave" 
                            CollapsedText="LEAVE APPLICATION DETAILS" 
                            ExpandedText="LEAVE APPLICATION DETAILS"                            
                            AutoCollapse="False"
                            AutoExpand="false"
                            ScrollContents="false"
                            ImageControlID="Image1"
                            ExpandedImage="~/images/collapse.jpg"
                            CollapsedImage="~/images/expand.jpg"
                            ExpandDirection="Vertical" 
                            >
                            </cc1:CollapsiblePanelExtender>
                </div>
            </asp:Panel>
            <br />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnApprove" />
            <asp:PostBackTrigger ControlID="btnApply" />
            <asp:PostBackTrigger ControlID="ucLeaveDocument1" />

        </Triggers>
    </asp:UpdatePanel>
</asp:Content>