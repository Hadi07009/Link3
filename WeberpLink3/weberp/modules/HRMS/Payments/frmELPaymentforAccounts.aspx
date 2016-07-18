<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmELPaymentforAccounts.aspx.cs" Inherits="modules_HRMS_Payments_frmELPaymentforAccounts" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="TimePicker" namespace="MKB.TimePicker" tagprefix="MKB" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <script type="text/javascript">

        function calcSum() {
            var row = objRef.parentNode.parentNode;
            if (objRef.checked) {
            }
        }

    </script>
    <style type="text/css">  
        .auto-style1 {
            height: 22px;
        }
        .auto-style3 {
            width: 196px;
        }
        .auto-style4 {
            height: 22px;
            width: 196px;
        }
        .auto-style5 {
            height: 27px;
            width: 149px;
        }
        .auto-style6 {
            width: 8px;
        }
        .auto-style7 {
            height: 22px;
            width: 8px;
        }
        .auto-style8 {
            height: 27px;
            width: 8px;
        }
                    
        .auto-style9 {
            width: 1398px;
            text-align: right;
        }
        .auto-style10 {
            width: 582px;
            text-align: left;
        }
        .auto-style11 {
            width: 628px;
            text-align: right;
        }
        .auto-style12 {
            width: 149px;
        }
        .auto-style13 {
            width: 149px;
            height: 63px;
            background-color: white;
        }
        .auto-style14 {
            height: 22px;
            width: 149px;
        }
        </style> 
    <asp:UpdatePanel ID="updtPnl" runat="server">
        <ContentTemplate>
            <div align="center" >
                <asp:Panel ID="PanelPaymentHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                    <asp:Label ID="LabelPaymentSearchHdr" Text="Search" runat="server" />
                </asp:Panel>
                <asp:Panel ID="PanelPaymentdet" runat="server" CssClass="cpBodyContent" Width="99%" Height="100%" ScrollBars="None" >
                    <table width="99%">
                        <tr>
                            <td>
                                &nbsp;</td>
                             <td class="style28">
                                 &nbsp;</td>
                            <td class="auto-style9">
                                <asp:Label ID="Label1" runat="server" Text="From"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                        <asp:TextBox ID="txtDatefrom" runat="server" Width="178px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDatefrom_CalendarExtender" runat="server" 
                                            Format="dd/MM/yyyy" TargetControlID="txtDatefrom">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                        <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style28">
                                        &nbsp;</td>
                                    <td class="auto-style9">
                                        <asp:Label ID="Label2" runat="server" Text="To"></asp:Label>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:TextBox ID="txtDateTo" runat="server" Width="178px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateTo_CalendarExtender" runat="server" 
                                            Format="dd/MM/yyyy" TargetControlID="txtDateTo">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                        <tr>
                                    <td>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td class="auto-style9">                                        
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Button ID="BtnShowApprovedData" runat="server" onclick="BtnShowApprovedData_Click" 
                                            Text="Show Locked Data" Width="183px" />
                                    </td>
                                </tr>
                    </table>
                    <asp:Panel ID="PanelPayment" runat="server" Width="99%" Height="100%" ScrollBars="None">
                        <table width="99%" >
                            <tr>
                                <td>
                                     <asp:Button ID="btnExport" runat="server" CssClass="btn2" OnClick="btnExport_Click" Text="Export " Width="100px" />
                                </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            <tr>
                                    <td colspan="3">
                                        <asp:GridView ID="GdvOffdayPaymentData" runat="server" 
                                            AutoGenerateColumns="False" onrowdatabound="GdvOffdayPaymentData_RowDataBound" 
                                            onselectedindexchanged="GridViewLeavePending_SelectedIndexChanged" 
                                            style="text-align: left" Width="100%" ShowFooter="true" >
                                            <Columns>
                                                <asp:BoundField DataField="Sl" HeaderStyle-HorizontalAlign="Center" 
                                                    HeaderText="SL #" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EmpID" HeaderStyle-HorizontalAlign="Left" 
                                                    HeaderText="Emp ID" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EmpName" HeaderStyle-HorizontalAlign="Left" 
                                                    HeaderText="Name" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Designation" HeaderStyle-HorizontalAlign="Left" 
                                                    HeaderText="Designation" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Dept" HeaderStyle-HorizontalAlign="Left" 
                                                    HeaderText="Department" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Type" HeaderStyle-HorizontalAlign="Left" 
                                                    HeaderText="Type" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Description" HeaderStyle-HorizontalAlign="Left" 
                                                    HeaderText="Description" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Noofdays" HeaderStyle-HorizontalAlign="Center" 
                                                    HeaderText="Quantity" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Noofdays" HeaderStyle-HorizontalAlign="Center" 
                                                    HeaderText="Unit Qty" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Unit" HeaderStyle-HorizontalAlign="Center" 
                                                    HeaderText="Unit" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PayableRate" HeaderStyle-HorizontalAlign="Center" 
                                                    HeaderText="Pay Rate" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="UnitRate" HeaderStyle-HorizontalAlign="Center" 
                                                    HeaderText="Rate" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="maxlimit" HeaderStyle-HorizontalAlign="Center" 
                                                    HeaderText="Net Amount" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Payableamt" HeaderStyle-HorizontalAlign="Center" 
                                                    HeaderText="Payable Amount" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Datefrom" dataformatstring="{0:dd/MM/yyyy}" 
                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Date From" 
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DateTo" dataformatstring="{0:dd/MM/yyyy}" 
                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Date To" 
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ProcessID" HeaderStyle-HorizontalAlign="Center" 
                                                    HeaderText="ProcessID" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FlowID" HeaderStyle-HorizontalAlign="Center" 
                                                    HeaderText="FlowID" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TransactionNo" HeaderStyle-HorizontalAlign="Center" 
                                                    HeaderText="Transactionno" ItemStyle-HorizontalAlign="Center">                                                  
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Select" 
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckRet" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>

                                            <headerstyle backcolor="LightCyan" forecolor="MediumBlue"/>
                                            <footerstyle backcolor="Crimson" forecolor="MediumBlue"/>

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
                            <tr>
                                    <td colspan="3" class="style36">
                                        <table style="width:100%;">
                                            <tr>
                                                <td class="style31">
                                                    &nbsp;</td>
                                                <td class="style30">
                                                    <asp:TextBox ID="txtPaymentNarration" runat="server" TextMode="MultiLine" 
                                                        Width="300px" Visible="False"></asp:TextBox>
                                                </td>
                                                <td class="auto-style11">
                                                    <asp:Label ID="Label3" runat="server" Text="Process&nbsp; Date"></asp:Label>
                                                </td>
                                                <td class="auto-style10">
                                                    <asp:TextBox ID="txtPaymentDate" runat="server" Width="178px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtPaymentDate_CalendarExtender" runat="server" 
                                                        Format="dd/MM/yyyy" TargetControlID="txtPaymentDate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Button ID="btnPaymentDone" runat="server" onclick="btnPaymentDone_Click" 
                                                        Text="Process for Payment " Width="178px" CssClass="btn2" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style31">
                                                    &nbsp;</td>
                                                <td class="style30">
                                                    &nbsp;</td>
                                                <td class="auto-style11">
                                                    &nbsp;</td>
                                                <td class="auto-style10">
                                                    &nbsp;</td>
                                                <td style="text-align: right">
                                                    <asp:Button ID="btnCancelApplication" runat="server" CssClass="btn2" onclick="btnCancelApplication_Click" Text="Cancel Application" Width="178px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                        </table> 
                    </asp:Panel>
                </asp:Panel>
                <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderSrchforpayment" runat="server" 
                            TargetControlID="PanelPaymentdet" 
                            CollapseControlID="PanelPaymentHdr" 
                            ExpandControlID="PanelPaymentHdr"
                            Collapsed="false" 
                            TextLabelID="LabelPaymentSearchHdr" 
                            CollapsedText="Payment Pending" 
                            ExpandedText="Payment Pending"
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
                <asp:Panel ID="PanelPaymentInfohdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                    <asp:Label ID="LabelpaymentInfo" Text="Search" runat="server" />
                </asp:Panel>
                <asp:Panel ID="PanelPaymentInfodet" runat="server" CssClass="cpBodyContent"  Width="99%" Height="99%">
                     <table width="99%">
                                <tr>
                                    <td >
                                        &nbsp;</td>
                                    <td class="auto-style12" >
                                        &nbsp;</td>
                                    <td >
                                        <asp:CheckBoxList ID="ChkPmttype" runat="server" RepeatDirection="Horizontal" Visible="False">
                                            <asp:ListItem Selected="True" Value="EL">Earned Leave</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="ALL">ALL Summary</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </td>
                                    <td >
                                        &nbsp;</td>
                                    <td style="text-align: right">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style6">&nbsp;</td>
                                    <td class="auto-style13">&nbsp;</td>
                                    <td class="style34">
                                        <asp:RadioButtonList ID="rdoPaymentInfo" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Value="0">Pending</asp:ListItem>
                                            <asp:ListItem Value="1">Paid</asp:ListItem>
                                            <asp:ListItem Value="2">ALL</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="style27">&nbsp;</td>
                                    <td style="text-align: right">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style7"></td>
                                    <td class="auto-style14">
                                        <asp:Label ID="Label7" runat="server" Text="Payment Date From"></asp:Label>
                                    </td>
                                    <td class="auto-style1">
                                        <asp:TextBox ID="TxtPaymentDatefrom" runat="server" Width="200px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy" TargetControlID="TxtPaymentDatefrom">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="auto-style1"></td>
                                    <td style="text-align: right" class="auto-style1"></td>
                                </tr>
                                <tr>
                                    <td class="auto-style7">
                                        </td>
                                    <td class="auto-style14">
                                        <asp:Label ID="Label5" runat="server" Text="Payment Date To"></asp:Label>
                                    </td>
                                    <td class="auto-style1">
                                        <asp:TextBox ID="txtPaymentto" runat="server" Width="200px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender6" runat="server" 
                                            Format="dd/MM/yyyy" TargetControlID="txtPaymentto">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="auto-style1">
                                        </td>
                                    <td style="text-align: right" class="auto-style1">
                                        </td>
                                </tr>
                                <tr>
                                    <td class="auto-style6">
                                        </td>
                                    <td class="auto-style13">
                                        <asp:Label ID="Label6" runat="server" Text="Employee ID"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmpid" runat="server" Width="200px"></asp:TextBox>
                                         <cc1:TextBoxWatermarkExtender ID="txtEmpid_TextBoxWatermarkExtender" runat="server" Enabled="True" TargetControlID="txtEmpid" WatermarkText=" Emp ID for Individual" >
                                         </cc1:TextBoxWatermarkExtender>
                                    </td>
                                    
                                    <td></td>
                                    
                                    <td></td>
                                    
                                </tr>
                                <tr>
                                    <td class="auto-style8"></td>
                                    <td class="auto-style5"></td>
                                    <td>
                                        <asp:Button ID="btnShowPaymentInfo" runat="server" CssClass="btn2" onclick="btnShowPaymentInfo_Click" Text="Show payment Information" Width="200px" />
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                     </table>
                     <asp:GridView ID="GdvPaymentInfo" runat="server" AutoGenerateColumns="False" 
                                    onselectedindexchanged="GdvPaymentInfo_SelectedIndexChanged" 
                                    style="text-align: left" Width="100%" 
                                    onrowcommand="GdvPaymentInfo_RowCommand" 
                                    onrowdatabound="GdvPaymentInfo_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="Sl" HeaderStyle-HorizontalAlign="Center" 
                                            HeaderText="SL #" ItemStyle-HorizontalAlign="Center">                                           
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PaymentReference" HeaderStyle-HorizontalAlign="Left" 
                                            HeaderText="Reference" ItemStyle-HorizontalAlign="Left">                                           
                                        </asp:BoundField>
                                        <asp:BoundField DataField="empid" HeaderStyle-HorizontalAlign="Left" 
                                            HeaderText="Emp ID" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="80px">                                           
                                        </asp:BoundField>
                                        <asp:BoundField DataField="empname" HeaderStyle-HorizontalAlign="Left" 
                                            HeaderText="Emp Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px">                                           
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Designation" HeaderStyle-HorizontalAlign="Left" 
                                            HeaderText="Designation" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="120px">                                           
                                        </asp:BoundField>
                                        <asp:BoundField DataField="dept" HeaderStyle-HorizontalAlign="Left" 
                                            HeaderText="Department" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="120px">                                           
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PaymentDate" HeaderStyle-HorizontalAlign="Left" 
                                            HeaderText="Payment Date" ItemStyle-HorizontalAlign="Left" dataformatstring="{0:dd/MM/yyyy}">                                           
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PaymentNarration" HeaderStyle-HorizontalAlign="Left" 
                                            HeaderText="Payment Description" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px">                                           
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Type" HeaderStyle-HorizontalAlign="Left" 
                                            HeaderText="Payment Type" ItemStyle-HorizontalAlign="Left">                                            
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PaymentAmount" HeaderStyle-HorizontalAlign="Right" 
                                            HeaderText="Amount" ItemStyle-HorizontalAlign="right">                                           
                                        </asp:BoundField> 
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" 
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnPayment"  CommandName="DONE"  runat="server" Text="DONE" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                                    </ItemTemplate>                                                   
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" 
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnExport"  CommandName="REJECT"  runat="server" Text="REJECT" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                                    </ItemTemplate>                                                    
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" 
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnPrint" CommandName="PRINT" runat="server" Text="PRINT" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                                    </ItemTemplate>                                                   
                                        </asp:TemplateField>   
                                        
                                        <asp:BoundField DataField="PaymentFlag" HeaderStyle-HorizontalAlign="Left" 
                                            HeaderText="Status" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                                                                                                      
                                    </Columns>
                                </asp:GridView>
                 </asp:Panel>
                <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderPanelPaymentInfohdr" runat="server" 
                            TargetControlID="PanelPaymentInfodet" 
                            CollapseControlID="PanelPaymentInfohdr" 
                            ExpandControlID="PanelPaymentInfohdr"
                            Collapsed="false" 
                            TextLabelID="LabelpaymentInfo" 
                            CollapsedText="Payment Information" 
                            ExpandedText="Payment Information"
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
                <asp:Panel ID="Panel50" runat="server" Width="100%">  
                    <div align="center">
                         <asp:GridView ID="gdvExport" runat="server" Visible="False">
                         </asp:GridView>
                         </div> 
                </asp:Panel>
                <br />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
            <asp:PostBackTrigger ControlID="GdvPaymentInfo" />
            <asp:PostBackTrigger ControlID="BtnShowApprovedData"/>   
        </Triggers> 
    </asp:UpdatePanel>
</asp:Content>