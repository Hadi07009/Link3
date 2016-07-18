<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" EnableEventValidation="false" ValidateRequest="false" CodeFile="frmDepreciationReport.aspx.cs" Inherits="modules_FixedAsset_TransactionDetails_frmDepreciationReport" %>

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
        <asp:Label ID="lblleave" Text="DEPRECIATION REPORT" runat="server" />
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
                             <asp:Panel ID="pnlSrchHdr" runat="server" CssClass="cpHeaderContent" Width="99%" Visible="False">
                                <asp:Label ID="lblSearchHdr" Text="Search By Name" runat="server" />
                             </asp:Panel>
                             <asp:Panel ID="pnlSrchDet" runat="server" CssClass="cpBody" Width="100%" 
                                     Height="34px" Visible="False">
                                <table>
                                    <tr>
                                        <td>
                                            Search Name&nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPoSearch" Width="300px" runat="server" autoComplete="off" 
                                                AutoPostBack="True"></asp:TextBox>                    
                                            <cc1:AutoCompleteExtender ID="txtPoSearch_AutoCompleteExtender" runat="server" 
                                                BehaviorID="AutoCompleteEx1" CompletionInterval="100" 
                                                CompletionListCssClass="autocomplete_completionListElement" 
                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" 
                                                DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="3" 
                                                ServiceMethod="GetInvPoListAll" 
                                                ServicePath="~/ClientSide/Inventory/services/InvAutoComplete.asmx" 
                                                ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtPoSearch">
                                            </cc1:AutoCompleteExtender>
                                        </td>   
                                        <td>
                                            <asp:Button ID="btnSearch" runat="server" onclick="btnSearch_Click" Text="Search" 
                                                Width="60px" Height="18px" />
                                        </td>    
                                        <td>
                                        <asp:Button ID="btnViewall" runat="server"  Text="View All" 
                                                Width="60px" Height="18px" onclick="btnViewall_Click" Visible="True" />
                                        </td>                
                                        <td>
                                            &nbsp;&nbsp;<asp:Label ID="lblEditFlag" runat="server" Text="N" Visible="False"></asp:Label>
                                            &nbsp;&nbsp;
                                        </td>
                                    </tr>
                                </table>  
                             </asp:Panel>
                             <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderSrch" runat="server" 
                                TargetControlID="pnlSrchDet" 
                                CollapseControlID="pnlSrchHdr" 
                                ExpandControlID="pnlSrchHdr"
                                Collapsed="true" 
                                TextLabelID="lblSearchHdr" 
                                CollapsedText="Search By Name" 
                                ExpandedText="Search By Name"
                                CollapsedSize="0"
                                ExpandedSize="34"
                                AutoCollapse="False"
                                AutoExpand="False"
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
                           <asp:Panel ID="pHeaderText" runat="server" CssClass="cpHeaderContent" Width="99%">
                                 <asp:Label ID="lblText" Text="Depreciation Report as on Date" runat="server" />
                           </asp:Panel>
                      
                          <asp:Panel ID="pHdrBody" runat="server" CssClass="cpBodyContent" Width="99%" Height="140px">
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
                                        As on:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFromDate" runat="server" Width="300px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" 
                                            Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" 
                                                ControlToValidate="txtFromDate" runat="server" 
                                                ErrorMessage="Enter Valid Date" ValidationGroup="HdrGrp"></asp:RequiredFieldValidator> 
                                    </td>                    
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:TextBox ID="txtToDate" runat="server" Width="300px" Visible="False"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" 
                                            Format="dd/MM/yyyy" TargetControlID="txtToDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" 
                                                ControlToValidate="txtToDate" runat="server" 
                                                ErrorMessage="Enter Valid Date" ValidationGroup="HdrGrp"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td align="center" style="text-align: right">
                                        <asp:Button ID="btnPrintduration" runat="server" 
                                            onclick="btnPrintduration_Click" Text="Print" Width="100px" />
                                    </td>                        
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>                    
                                    <td align="center" style="text-align: right">
                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                    </td>
                                    
                                </tr>  
                                
                                
                            </table>  
                           </asp:Panel>
                                     <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderHdr" runat="server" 
                                        TargetControlID="pHdrBody" 
                                        CollapseControlID="pHeaderText" 
                                        ExpandControlID="pHeaderText"
                                        Collapsed="false" 
                                        TextLabelID="lblText" 
                                        CollapsedText="Depreciation Report as on Date" 
                                        ExpandedText="Depreciation Report as on Date"
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
                                  <asp:Panel ID="pnlduration" runat="server" CssClass="cpHeaderContent" Width="99%">
                                         <asp:Label ID="Labeldurationtxt" Text="Depreciation Report date duration" runat="server" />
                                  </asp:Panel>
                      
                                  <asp:Panel ID="pnldurationdet" runat="server" CssClass="cpBodyContent" Width="99%" Height="140px">
                                  <table>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <asp:RadioButtonList ID="rdoSelection" runat="server" 
                                                    RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="WithoutAddress">Without Address</asp:ListItem>
                                                    <asp:ListItem Value="WithAddress">With Address</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>   
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
                                                        ControlToValidate="txtToDate" runat="server" 
                                                        ErrorMessage="Enter Valid Date" ValidationGroup="HdrGrp"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td align="center" style="text-align: right">
                                                <asp:Button ID="btnPrintFromTo" runat="server" 
                                                    onclick="btnPrintFromTo_Click" Text="Print" Width="100px" />
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
                                         <asp:Label ID="lblItemwise" Text="Item Wise Report With Depreciation" runat="server" />
                                  </asp:Panel>
                      
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
                                                As on:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtfromdate1" runat="server" Width="300px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" 
                                                    Format="dd/MM/yyyy" TargetControlID="txtfromdate1">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" 
                                                        ControlToValidate="txtfromdate1" runat="server" 
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
                            
                            <div align="center">
                                  <asp:Panel ID="pnlitmwisesummaryhdr" runat="server" CssClass="cpHeaderContent" Width="99%">
                                         <asp:Label ID="lblitmwisesummarytxt" Text="Item Wise Summary" runat="server" />
                                  </asp:Panel>
                      
                                  <asp:Panel ID="pnlitmwisesummarydet" runat="server" CssClass="cpBodyContent" Width="99%" Height="140px">
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
                                                As on:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtasondate1" runat="server" Width="300px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" 
                                                    Format="dd/MM/yyyy" TargetControlID="txtasondate1">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" 
                                                        ControlToValidate="txtasondate1" runat="server" 
                                                        ErrorMessage="Enter Valid Date" ValidationGroup="HdrGrp"></asp:RequiredFieldValidator> 
                                            </td>                    
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td align="center" style="text-align: right">
                                                <asp:Button ID="btnItmwiseSummary" runat="server" 
                                                    onclick="btnItmwiseSummary_Click" Text="Print" Width="100px" />
                                            </td>                        
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>                    
                                            <td align="center" style="text-align: right">
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                            </td>                                            
                                        </tr>                                                                               
                                     </table>                                     
                                   </asp:Panel>
                                     
                                     <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server" 
                                        TargetControlID="pnlitmwisesummarydet" 
                                        CollapseControlID="pnlitmwisesummaryhdr" 
                                        ExpandControlID="pnlitmwisesummaryhdr"
                                        Collapsed="false" 
                                        TextLabelID="lblitmwisesummarytxt" 
                                        CollapsedText="Item Wise Summary" 
                                        ExpandedText="Item Wise Summary"
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
                                  <asp:Panel ID="pnlPeriodicalhdr" runat="server" CssClass="cpHeaderContent" Width="99%">
                                         <asp:Label ID="lblperiodicalhdr" Text="Depreciation Report" runat="server" />
                                  </asp:Panel>
                      
                                  <asp:Panel ID="pnlPeriodicaldet" runat="server" CssClass="cpBodyContent" Width="99%" Height="140px">
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
                                                <asp:TextBox ID="txtfdt" runat="server" Width="300px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender5" runat="server" 
                                                    Format="dd/MM/yyyy" TargetControlID="txtfdt">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" 
                                                        ControlToValidate="txtfdt" runat="server" 
                                                        ErrorMessage="Enter Valid Date" ValidationGroup="HdrGrp"></asp:RequiredFieldValidator> 
                                            </td>                    
                                        </tr>
                                        <tr>
                                            <td>
                                                Date to:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txttdt" runat="server" Width="300px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender6" runat="server" 
                                                    Format="dd/MM/yyyy" TargetControlID="txttdt">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" 
                                                        ControlToValidate="txttdt" runat="server" 
                                                        ErrorMessage="Enter Valid Date" ValidationGroup="HdrGrp"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td align="center" style="text-align: right">
                                                &nbsp;</td>                        
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>                    
                                            <td align="center" style="text-align: right">
                                                <asp:Button ID="btnPreviewPeriodical" runat="server" 
                                                    onclick="btnPreviewPeriodical_Click" Text="Print" Width="100px" />
                                                <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                            </td>                                            
                                        </tr>                                                                               
                                     </table>                                     
                                   </asp:Panel>
                                     
                                     <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server" 
                                        TargetControlID="pnlPeriodicaldet" 
                                        CollapseControlID="pnlPeriodicalhdr" 
                                        ExpandControlID="pnlPeriodicalhdr"
                                        Collapsed="false" 
                                        TextLabelID="lblperiodicalhdr" 
                                        CollapsedText="Depreciation Report" 
                                        ExpandedText="Depreciation Report"
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
                                  <asp:Panel ID="Panel1" runat="server" CssClass="cpHeaderContent" Width="99%">
                                         <asp:Label ID="Label5" Text="Depreciation Report Summery Group Wise" runat="server" />
                                  </asp:Panel>
                      
                                  <asp:Panel ID="Panel2" runat="server" CssClass="cpBodyContent" Width="99%" Height="140px">
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
                                                <asp:TextBox ID="txtFrmDate" runat="server" Width="300px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender7" runat="server" 
                                                    Format="dd/MM/yyyy" TargetControlID="txtFrmDate">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" 
                                                        ControlToValidate="txtfdt" runat="server" 
                                                        ErrorMessage="Enter Valid Date" ValidationGroup="HdrGrp"></asp:RequiredFieldValidator> 
                                            </td>                    
                                        </tr>
                                        <tr>
                                            <td>
                                                Date to:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTdate" runat="server" Width="300px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender8" runat="server" 
                                                    Format="dd/MM/yyyy" TargetControlID="txtTdate">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" 
                                                        ControlToValidate="txttdt" runat="server" 
                                                        ErrorMessage="Enter Valid Date" ValidationGroup="HdrGrp"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td align="center" style="text-align: right">
                                                <asp:Button ID="btnPrintReport" runat="server" 
                                                     Text="Print" Width="100px" onclick="btnPrintReport_Click" />
                                            </td>                        
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>                    
                                            <td align="center" style="text-align: right">
                                                <asp:Label ID="Label7" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                            </td>                                            
                                        </tr>                                                                               
                                     </table>                                     
                                   </asp:Panel>
                                     
                                     <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender4" runat="server" 
                                        TargetControlID="pnlPeriodicaldet" 
                                        CollapseControlID="pnlPeriodicalhdr" 
                                        ExpandControlID="pnlPeriodicalhdr"
                                        Collapsed="false" 
                                        TextLabelID="lblperiodicalhdr" 
                                        CollapsedText="Depreciation Report" 
                                        ExpandedText="Depreciation Report"
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