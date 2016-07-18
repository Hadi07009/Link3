<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" EnableEventValidation="false" ValidateRequest="false" CodeFile="frmFixedAssetsAcquisitionReport.aspx.cs" Inherits="modules_FixedAsset_TransactionDetails_frmFixedAssetsAcquisitionReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">  
            .cpHeader
            {
            color: white;
            background-color: #719DDB;
            font: bold 11px auto "Trebuchet MS", Verdana;
            font-size: 12px;
            cursor: pointer;
            width:450px;
            height:18px;
            padding: 4px;           
            }
            .cpBody
            {
            background-color: #DCE4F9;
            font: normal 11px auto Verdana, Arial;
            border: 1px gray;               
            width:450px;           
            padding: 4px;
            padding-top: 2px;
            height:0px;
            overflow : hidden;
            }            
        </style> 
        <div>
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
            <asp:Label ID="lblleave" Text="ASSET ACQUISITION REPORT" runat="server" />
        </asp:Panel>
        </div>          
             <table style="width:100%;">
                 <tr>
                     <td>
                         &nbsp;</td>
                     <td>
                         <asp:UpdatePanel ID="updtPnl" runat="server">
                         <ContentTemplate>
                         
           
                       
                            
                           <div align="center">
                                  <asp:Panel ID="pnlduration" runat="server" CssClass="cpHeaderContent" Width="99%">
                                         Fixed Asset acquisition Report Details</asp:Panel>
                      
                                  <asp:Panel ID="pnldurationdet" runat="server" CssClass="cpBodyContent" Width="99%" Height="140px">
                                  <table>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>   
                                            <td>                            
                                                <br />                    
                                                
                                            </td>                    
                                        </tr>
                                        <tr>
                                            <td>
                                                Date from:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDate1" runat="server" Width="300px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                                                    Format="dd/MM/yyyy" TargetControlID="txtDate1">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                                                        ControlToValidate="txtDate1" runat="server" 
                                                        ErrorMessage="Enter Valid Date" ValidationGroup="HdrGrp"></asp:RequiredFieldValidator> 
                                            </td>                    
                                        </tr>
                                        <tr>
                                            <td>
                                                Date to:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDate2" runat="server" Width="300px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" 
                                                    Format="dd/MM/yyyy" TargetControlID="txtDate2">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                                                        ControlToValidate="txtDate2" runat="server" 
                                                        ErrorMessage="Enter Valid Date" ValidationGroup="HdrGrp"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td align="center" style="text-align: right">
                                                <asp:Button ID="btnPrintFromTo" runat="server" onclick="btnPrintFromTo_Click" Text="Print" Width="100px" />
                                            </td>                        
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>                    
                                            <td align="center" style="text-align: right">
                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                            </td>                                            
                                        </tr>                                                                               
                                     </table>                                     
                                   </asp:Panel>
                                     
                                     <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderDuration" runat="server" 
                                        TargetControlID="pnldurationdet" 
                                        CollapseControlID="pnlduration" 
                                        ExpandControlID="pnlduration"
                                        Collapsed="false" 
                                        TextLabelID="Labeldurationtxt" 
                                        CollapsedText="Depreciation Report Date duration" 
                                        ExpandedText="Depreciation Report Date duration"
                                        CollapsedSize="0"
                                        ExpandedSize="140"
                                        AutoCollapse="False"
                                        AutoExpand="False"
                                        ScrollContents="false"
                                        ImageControlID="Image1"
                                        ExpandedImage="~/images/collapse.jpg"
                                        CollapsedImage="~/images/expand.jpg"
                                        ExpandDirection="Vertical" 
                                        >
                                        </cc1:CollapsiblePanelExtender>
                            </div>  
                            
                            <div align="center">
                                  <asp:Panel ID="pnlItemwisehdr" runat="server" CssClass="cpHeaderContent" Width="99%">
                                        Fixed Asset Accquisition Report Summery Group wise</asp:Panel>
                      
                                  <asp:Panel ID="pnlItemwisedtl" runat="server" CssClass="cpBodyContent" Width="99%" Height="140px">
                                  <table>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>   
                                            <td>                            
                                                <br />                    
                                                
                                            </td>                    
                                        </tr>
                                        <tr>
                                            <td>
                                                As on:</td>
                                            <td>
                                                <asp:TextBox ID="txtfromdate1" runat="server" Width="300px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtfromdate1_CalendarExtender" runat="server" 
                                                    Format="dd/MM/yyyy" TargetControlID="txtfromdate1">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                    ControlToValidate="txtfromdate1" ErrorMessage="Enter Valid Date" 
                                                    ValidationGroup="HdrGrp"></asp:RequiredFieldValidator>
                                            </td>                    
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <asp:TextBox ID="txtToDate1" runat="server" Width="300px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" 
                                                    Format="dd/MM/yyyy" TargetControlID="txtToDate1">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" 
                                                        ControlToValidate="txtToDate1" runat="server" 
                                                        ErrorMessage="Enter Valid Date" ValidationGroup="HdrGrp"></asp:RequiredFieldValidator> 
                                            </td>                    
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td align="center" style="text-align: right">
                                                <asp:Button ID="btnItemwisedet" runat="server" 
                                                    onclick="btnItemwisedet_Click" Text="Print" Width="100px" />
                                            </td>                        
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>                    
                                            <td align="center" style="text-align: right">
                                                <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                            </td>                                            
                                        </tr>                                                                               
                                     </table>                                     
                                   </asp:Panel>
                                     
                                     <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" 
                                        TargetControlID="pnlItemwisedtl" 
                                        CollapseControlID="pnlItemwisehdr" 
                                        ExpandControlID="pnlItemwisehdr"
                                        Collapsed="false" 
                                        TextLabelID="lblItemwise" 
                                        CollapsedText="Item Wise Report With Depreciation" 
                                        ExpandedText="Item Wise Report With Depreciation"
                                        CollapsedSize="0"
                                        ExpandedSize="140"
                                        AutoCollapse="False"
                                        AutoExpand="False"
                                        ScrollContents="false"
                                        ImageControlID="Image1"
                                        ExpandedImage="~/images/collapse.jpg"
                                        CollapsedImage="~/images/expand.jpg"
                                        ExpandDirection="Vertical" 
                                        >
                                        </cc1:CollapsiblePanelExtender>
                            </div>                                
                            
                    
                         </ContentTemplate>
                         <Triggers>
                         <%--<asp:AsyncPostBackTrigger ControlID="txtQuantity" EventName="TextChanged"/>--%>
                         <%--<asp:AsyncPostBackTrigger ControlID="txtRate" EventName="TextChanged"/>--%>
                         <%--<asp:AsyncPostBackTrigger ControlID="btnHold" EventName="Click" />--%>
                         </Triggers>                                      
                         </asp:UpdatePanel>
                         </td>                         
                     
                 </tr>
                 
             </table>
             </asp:Content>