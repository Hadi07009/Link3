<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmOFOTNTPaymentforAccounts.aspx.cs" Inherits="modules_HRMS_Payments_frmOFOTNTPaymentforAccounts" %>
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
        .style27
        {
            width: 1113px;
            text-align: right;
        }
        .style28
        {
            width: 1103px;
            text-align: left;
        }
        .style29
        {
            width: 1150px;
            text-align: left;
        }
        .style30
        {
            width: 786px;
            text-align: left;
        }
        .style31
        {
            width: 213px;
            text-align: right;
        }
        .style32
        {
            width: 194px;
            text-align: right;
        }
        .style33
        {
            text-align: left;
        }
        .style34
        {
            width: 253px;
            text-align: left;
        }
        .style35
        {
            width: 171px;
            text-align: left;
        }
        .style36
        {
            height: 47px;
        }
        </style>  
    <asp:UpdatePanel ID="updtPnl" runat="server">
        <ContentTemplate>
            <div align="center">
                <asp:Panel ID="pnlSrchHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                            <asp:Label ID="lblSearchHdr" Text="Search" runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnlSrchDet" runat="server" CssClass="cpBodyContent" Width="99%" Height="99%">
                    <table width="99%">
                        <tr>
                            <td>
                                &nbsp;</td>
                                    <td class="style28">
                                        &nbsp;</td>
                                    <td class="style27">
                                        <asp:Label ID="Label7" runat="server" Text="From"></asp:Label>
                            </td>
                                    <td style="text-align: right">
                                        <asp:TextBox ID="txtoffdayFromDate" runat="server" Width="178px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtoffdayFromDate_CalendarExtender" runat="server" 
                                            Format="dd/MM/yyyy" TargetControlID="txtoffdayFromDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style28">
                                        &nbsp;</td>
                                    <td class="style27">
                                        <asp:Label ID="Label8" runat="server" Text="To"></asp:Label>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:TextBox ID="txtoffdayToDate" runat="server" Width="178px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtoffdayToDate_CalendarExtender" runat="server" 
                                            Format="dd/MM/yyyy" TargetControlID="txtoffdayToDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style28">
                                        <asp:Button ID="btmExport1" runat="server" OnClick="btmExport1_Click" Text="Export" Width="100px" />
                                    </td>
                                    <td class="style27">
                                        <asp:CheckBoxList ID="ChkPmttypeForSearch" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="OF">Off day</asp:ListItem>
                                            <asp:ListItem Value="OT">Over Time</asp:ListItem>
                                            <asp:ListItem Value="NT">NIght</asp:ListItem>
                                            <asp:ListItem Value="EL">EL Encash</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Button ID="btnShowOffday" runat="server" onclick="btnShowOffday_Click" 
                                            Text="Show" Width="183px" />
                                    </td>
                                </tr>
                            </table>
                    <asp:Panel ID="PanelLocked" runat="server" Width="99%">
                        <table width="99%" >
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:GridView ID="GridViewPendingLocked" runat="server" 
                                            AutoGenerateColumns="False" 
                                            onpageindexchanging="GridViewPendingLocked_PageIndexChanging" 
                                            onrowdatabound="GridViewPendingLocked_RowDataBound" 
                                            onselectedindexchanged="GridViewPendingLocked_SelectedIndexChanged" 
                                            Width="100%" style="text-align: left" ShowFooter="true">
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
                                                <asp:BoundField DataField="Paytype" HeaderStyle-HorizontalAlign="Left" 
                                                    HeaderText="Type" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Description" HeaderStyle-HorizontalAlign="Left" 
                                                    HeaderText="Description" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="quantity" HeaderStyle-HorizontalAlign="Center" 
                                                    HeaderText="Quantity" ItemStyle-HorizontalAlign="Center">
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
                                                <asp:BoundField DataField="Payableamt" HeaderStyle-HorizontalAlign="Center" 
                                                    HeaderText="Payable Amount" ItemStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>                                                                                                                                                                                             
                                                <asp:BoundField DataField="Datefrom" HeaderStyle-HorizontalAlign="Left" 
                                                    HeaderText="Date From" ItemStyle-HorizontalAlign="Left" dataformatstring="{0:dd/MM/yyyy}">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DateTo" HeaderStyle-HorizontalAlign="Left" 
                                                    HeaderText="Date To" ItemStyle-HorizontalAlign="Left" dataformatstring="{0:dd/MM/yyyy}">
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
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Select" 
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckRet" runat="server" onclick="calcSum(this)" />                                                                                                               
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
                                    <td style="text-align: left">
                                        &nbsp;</td>
                                    <td>
                                        <table style="width:100%;">
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td style="text-align: right">
                                                    <asp:Button ID="btnlockedforpayment" runat="server" Text="Locked for Payment" 
                                                        onclick="btnlockedforpayment_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                        </table> 
                    </asp:Panel>
                </asp:Panel>
                <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderSrch" runat="server" 
                            TargetControlID="pnlSrchDet" 
                            CollapseControlID="pnlSrchHdr" 
                            ExpandControlID="pnlSrchHdr"
                            Collapsed="false" 
                            TextLabelID="lblSearchHdr" 
                            CollapsedText="Approved data ready for payment locked" 
                            ExpandedText="Approved data ready for payment locked"
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
            <div align="center">
                <asp:Panel ID="PanelPaymentHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                    <asp:Label ID="LabelPaymentSearchHdr" Text="Search" runat="server" />
                </asp:Panel>
                <asp:Panel ID="PanelPaymentdet" runat="server" CssClass="cpBodyContent" Width="99%" Height="100%" ScrollBars="None">
                    <table width="99%">
                        <tr>
                            <td>
                                &nbsp;</td>
                                    <td class="style28">
                                        &nbsp;</td>
                                    <td class="style27">
                                        <asp:Label ID="Label9" runat="server" Text="From"></asp:Label>
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
                                    <td class="style27">
                                        <asp:Label ID="Label10" runat="server" Text="To"></asp:Label>
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
                                        &nbsp;</td>
                                    <td class="style28">
                                        <asp:Button ID="btmExport2" runat="server" OnClick="btmExport2_Click" Text="Export" Width="100px" />
                                    </td>
                                    <td class="style27">
                                        <asp:CheckBoxList ID="ChkPmttypeForPayment" runat="server" 
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Value="OF">Off day</asp:ListItem>
                                            <asp:ListItem Value="OT">Over Time</asp:ListItem>
                                            <asp:ListItem Value="NT">NIght</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Button ID="BtnShowApprovedData" runat="server" onclick="BtnShowApprovedData_Click" 
                                            Text="Show Locked Data" Width="183px" />
                                    </td>
                                </tr>
                            </table>
                    <asp:Panel ID="PanelPayment" runat="server" Width="100%">
                        <table width="100%" >
                                <tr>
                                    <td>
                                        &nbsp;</td>
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
                                                    <asp:Label ID="Label11" runat="server" Text="Process Narration"></asp:Label>
                                                </td>
                                                <td class="style30">
                                                    <asp:TextBox ID="txtPaymentNarration" runat="server" TextMode="MultiLine" 
                                                        Width="300px"></asp:TextBox>
                                                </td>
                                                <td class="style32">
                                                    <asp:Label ID="Label12" runat="server" Text="Process&nbsp; Date"></asp:Label>
                                                </td>
                                                <td class="style29">
                                                    <asp:TextBox ID="txtPaymentDate" runat="server" Width="178px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtPaymentDate_CalendarExtender" runat="server" 
                                                        Format="dd/MM/yyyy" TargetControlID="txtPaymentDate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Button ID="btnPaymentDone" runat="server" onclick="btnPaymentDone_Click" 
                                                        Text="Process for Payment " Width="178px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style31">
                                                    &nbsp;</td>
                                                <td class="style30">
                                                    &nbsp;</td>
                                                <td class="style32">
                                                    &nbsp;</td>
                                                <td class="style29">
                                                    &nbsp;</td>
                                                <td style="text-align: right">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    &nbsp;</td>
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
            </div>
            <div align="center">
                <asp:Panel ID="PanelPaymentInfohdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                    <asp:Label ID="LabelpaymentInfo" Text="Search" runat="server" />
                </asp:Panel>
                <asp:Panel ID="PanelPaymentInfodet" runat="server" CssClass="cpBodyContent" Width="99%" Height="100%" ScrollBars="None">
                    <table width="99%">
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td class="style33" colspan="3">
                                        <asp:CheckBoxList ID="ChkPmttype" runat="server" RepeatDirection="Horizontal" Visible="False">
                                            <asp:ListItem Value="OF" Selected="True">Off day</asp:ListItem>
                                            <asp:ListItem Value="OT" Selected="True">Over Time</asp:ListItem>
                                            <asp:ListItem Value="NT" Selected="True">NIght</asp:ListItem>
                                            <asp:ListItem Value="ALL" Selected="True">ALL Summary</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </td>
                                    <td style="text-align: right">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style35">
                                        <asp:Label ID="Label13" runat="server" Text="Payment Date From"></asp:Label>
                                    </td>
                                    <td class="style34">
                                        <asp:TextBox ID="TxtPaymentDatefrom" runat="server" Width="178px"></asp:TextBox>
                                         <cc1:CalendarExtender ID="CalendarExtender5" runat="server" 
                                            Format="dd/MM/yyyy" TargetControlID="TxtPaymentDatefrom">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style27">
                                        &nbsp;</td>
                                    <td style="text-align: right">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style35">
                                        <asp:Label ID="Label14" runat="server" Text="Payment Date To"></asp:Label>
                                    </td>
                                    <td class="style34">
                                        <asp:TextBox ID="txtPaymentto" runat="server" Width="178px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender6" runat="server" 
                                            Format="dd/MM/yyyy" TargetControlID="txtPaymentto">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style27">
                                        &nbsp;</td>
                                    <td style="text-align: right">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style35">
                                        <asp:RadioButtonList ID="rdoPaymentInfo" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Value="0">Pending</asp:ListItem>
                                            <asp:ListItem Value="1">Paid</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="style34">
                                        <asp:Button ID="btnShowPaymentInfo" runat="server" 
                                            onclick="btnShowPaymentInfo_Click" Text="Show payment Information" 
                                            Width="183px" />
                                    </td>
                                    <td class="style27">
                                        &nbsp;</td>
                                    <td style="text-align: right">
                                        &nbsp;</td>
                                </tr>
                            </table>
                    <asp:Panel ID="Panel3" runat="server" Width="100%">
                        <asp:GridView ID="GdvPaymentInfo" runat="server" AutoGenerateColumns="False" 
                                    onselectedindexchanged="GdvPaymentInfo_SelectedIndexChanged" 
                                    style="text-align: left" Width="100%" 
                                    onrowcommand="GdvPaymentInfo_RowCommand" 
                                    onrowdatabound="GdvPaymentInfo_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="Sl" HeaderStyle-HorizontalAlign="Center" 
                                            HeaderText="SL #" ItemStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PaymentReference" HeaderStyle-HorizontalAlign="Left" 
                                            HeaderText="Reference" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PaymentDate" HeaderStyle-HorizontalAlign="Left" 
                                            HeaderText="Payment Date" ItemStyle-HorizontalAlign="Left" dataformatstring="{0:dd/MM/yyyy}">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PaymentNarration" HeaderStyle-HorizontalAlign="Left" 
                                            HeaderText="Payment Description" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Type" HeaderStyle-HorizontalAlign="Left" 
                                            HeaderText="Payment Type" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PaymentAmount" HeaderStyle-HorizontalAlign="Left" 
                                            HeaderText="Amount" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField> 
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Payment" 
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnPayment"  CommandName="DONE"  runat="server" Text="DONE" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Export" 
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnExport"  CommandName="EXPORT"  runat="server" Text="EXPORT" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Print" 
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnPrint" CommandName="PRINT" runat="server" Text="PRINT" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>   
                                        
                                        <asp:BoundField DataField="PaymentFlag" HeaderStyle-HorizontalAlign="Left" 
                                            HeaderText="IsPaid" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField> 
                                                                                                                      
                                    </Columns>
                                </asp:GridView>
                    </asp:Panel>
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
            </div>
            <asp:Panel ID="Panel50" runat="server" Width="100%"> 
                <div align="center">
                    <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                        <asp:Label ID="lblleave" Text="LEAVE APPLICATION DETAILS" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="99%" Height="100%" ScrollBars="None">
                        <table width="99%">
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
                                                     <asp:Label ID="Label15" runat="server" Text="Employee Information"></asp:Label>
                                                 </td>
                                                 <td class="style20">
                                                     &nbsp;</td>
                                                 <td class="style23">
                                                     &nbsp;</td>
                                                 <td style="text-align: center">
                                                     <asp:Label ID="Label23" runat="server" Text="Previous off day inormation"></asp:Label>
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td class="style9">
                                                     <table style="width:100%;">
                                                         <tr>
                                                             <td class="style10">
                                                                 <asp:Label ID="Label16" runat="server" Text="ID"></asp:Label>
                                                             </td>
                                                             <td class="style11">
                                                                 :</td>
                                                             <td style="text-align: left">
                                                                 <asp:Label ID="lblId" runat="server"></asp:Label>
                                                             </td>
                                                         </tr>
                                                         <tr>
                                                             <td class="style10">
                                                                 <asp:Label ID="Label17" runat="server" Text="Name"></asp:Label>
                                                             </td>
                                                             <td class="style11">
                                                                 :</td>
                                                             <td style="text-align: left">
                                                                 <asp:Label ID="lblName" runat="server"></asp:Label>
                                                             </td>
                                                         </tr>
                                                         <tr>
                                                             <td class="style10">
                                                                 <asp:Label ID="Label18" runat="server" Text="Department"></asp:Label>
                                                             </td>
                                                             <td class="style11">
                                                                 :</td>
                                                             <td style="text-align: left">
                                                                 <asp:Label ID="lbldept" runat="server"></asp:Label>
                                                             </td>
                                                         </tr>
                                                         <tr>
                                                             <td class="style10">
                                                                 <asp:Label ID="Label19" runat="server" Text="Designation"></asp:Label>
                                                             </td>
                                                             <td class="style11">
                                                                 :</td>
                                                             <td style="text-align: left">
                                                                 <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                                                             </td>
                                                         </tr>
                                                         <tr>
                                                             <td class="style10">
                                                                 <asp:Label ID="Label20" runat="server" Text="Joining Date"></asp:Label>
                                                             </td>
                                                             <td class="style11">
                                                                 :</td>
                                                             <td style="text-align: left">
                                                                 <asp:Label ID="lblJoiningDate" runat="server"></asp:Label>
                                                             </td>
                                                         </tr>
                                                         <tr>
                                                             <td class="style10">
                                                                 <asp:Label ID="Label21" runat="server" Text="Period"></asp:Label>
                                                             </td>
                                                             <td class="style11">
                                                                 :</td>
                                                             <td style="text-align: left">
                                                                 <asp:Label ID="lblPeriod" runat="server"></asp:Label>
                                                             </td>
                                                         </tr>
                                                         <tr>
                                                             <td class="style10">
                                                                 <asp:Label ID="Label22" runat="server" Text="Date"></asp:Label>
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
                                                                 <asp:Label ID="Label24" runat="server" Text="From"></asp:Label>
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
                                                                 <asp:Label ID="Label25" runat="server" Text="To"></asp:Label>
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
                                            Width="100%" onrowdatabound="GridViewLeave_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="Sl" HeaderText="SL #" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>
                                                <asp:BoundField DataField="Atnd_det_date" DataFormatString="{0:d}" HeaderText="Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>
                                                <asp:BoundField DataField="weekdayname" HeaderText="Week Day" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                                                <asp:BoundField DataField="Atnd_det_offlg" HeaderText="Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                                                <asp:BoundField DataField="Atnd_det_intime" HeaderText="Intime" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>
                                                <asp:BoundField DataField="Atnd_det_outtime" HeaderText="Outtime" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>
                                                <asp:BoundField DataField="Atnd_det_hrs" HeaderText="Hours" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>                                                
                                                <asp:TemplateField HeaderText="Comments" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtlvRemarks" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Leave" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Dropdownlist ID="dpllday" runat="server"></asp:Dropdownlist>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Leave Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Dropdownlist ID="lvtype" runat="server"></asp:Dropdownlist>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtlvstatus" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="EmpName" HeaderText="Action Taken By"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>                                                 
                                                <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="Checklv" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Type" HeaderText="Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>                                                
                                                <asp:BoundField DataField="noofdays"   HeaderText="noofd"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>                                                
                                                <asp:BoundField DataField="Remarks"  HeaderText="Remarks" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>                                                
                                                <asp:BoundField DataField="IsLineLocked"  HeaderText="LockedL" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>   
                                                <asp:BoundField DataField="IsProcessLocked" HeaderText="LockedP" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>   
                                                <asp:BoundField DataField="Action" HeaderText="Action"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>   
                                                <asp:BoundField DataField="ProcessLevelid" HeaderText="PLid" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>
                                                <asp:BoundField DataField="TransactionNo" HeaderText="tno" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>   
                                                <asp:BoundField DataField="TransactionNoLineNo" HeaderText="tlno" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>      
                                                  
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
                                                     <asp:Label ID="Label5" runat="server" Text="Assign Person"></asp:Label>
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
                                                     <asp:Button ID="btnForward" runat="server" onclick="btnForward_Click" 
                                                         Text="Forward" Width="100px" />
                                                     <asp:Button ID="btnReturn" runat="server" onclick="btnReturn_Click" 
                                                         Text="Return" Width="100px" />
                                                     <asp:Button ID="btnReject" runat="server" onclick="btnReject_Click" 
                                                         Text="Reject" Width="100px" />
                                                     <asp:Button ID="btnApprove" runat="server" onclick="btnApprove_Click" 
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
                                         </table>
                                     </td>
                                     <td>
                                         &nbsp;</td>
                                 </tr>
                                 <tr>
                                     <td colspan="3">
                                         <table style="width:100%;">
                                             <tr>
                                                 <td style="text-align: left" colspan="3">
                                                     <asp:GridView ID="gdvExport" runat="server" Visible="False">
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
                            CollapsedText="OFFDAY APPLICATION DETAILS" 
                            ExpandedText="OFFDAY APPLICATION DETAILS"
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
            <asp:PostBackTrigger ControlID="btmExport1" />
            <asp:PostBackTrigger ControlID="btmExport2" />
            <asp:PostBackTrigger ControlID="GdvPaymentInfo" />
            <asp:PostBackTrigger ControlID="btnApprove" />
            <asp:PostBackTrigger ControlID="btnApply" />
            <asp:PostBackTrigger ControlID="BtnShowApprovedData"/>
        </Triggers> 
    </asp:UpdatePanel>
</asp:Content>