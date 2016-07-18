<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" EnableEventValidation="false" ValidateRequest="false" CodeFile="frmDepreciationCalculation.aspx.cs" Inherits="modules_FixedAsset_TransactionDetails_frmDepreciationCalculation" %>
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
        <asp:Label ID="lblleave" Text="DEPRECIATION CALCULATION" runat="server" />
    </asp:Panel>
    </div>
    <div>
             <table style="width:99%;">
                 <tr>
                     <td>
                         &nbsp;</td>
                     <td>
                         <asp:UpdatePanel ID="updtPnl" runat="server">
                         <ContentTemplate>
                         <div>                         
                         </div>                                                 
                         
                        <div align="center">
                                        <asp:Panel ID="pnlSrchHdr" runat="server" CssClass="cpHeaderContent" Width="100%" Visible="False">
                                        <asp:Label ID="lblSearchHdr" Text="Search By Name" runat="server" />
                                        </asp:Panel>
                                        <asp:Panel ID="pnlSrchDet" runat="server" CssClass="cpBodyContent" Width="100%" 
                                                Height="75px" Visible="False">
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
                                    &nbsp;&nbsp;</td>
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
                                    <asp:Label ID="lblText" Text="Depreciation Calculation" runat="server" />
                            </asp:Panel>
          
                            <asp:Panel ID="pHdrBody" runat="server" CssClass="cpBodyContent" Width="99%" Height="200px">
                            <table>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblCal" runat="server" Font-Bold="True" ForeColor="#3333FF"></asp:Label>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        From&nbsp; Date:
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
                                        To Date:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtToDate" runat="server" Width="300px"></asp:TextBox>
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
                                        Message:</td>
                                    <td>
                                        <asp:TextBox ID="txtRemarks" runat="server" Width="300px" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td align="center" style="text-align: right">
                                        <asp:Button ID="btnCalcCulate" runat="server" onclick="btnCalcCulate_Click" 
                                            Text="Calculate" Width="150px" />
                                    </td>                        
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButtonList ID="rdoOption" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True">History</asp:ListItem>
                                            <asp:ListItem>Summary</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>                    
                                    <td align="center" style="text-align: right">
                                        <asp:Button ID="BtnDepSumary" runat="server" onclick="BtnDepSumary_Click" 
                                            Text="View All DepreCiation" Width="150px" />
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
                                        CollapsedText="Depreciation Calculation" 
                                        ExpandedText="Depreciation Calculation"
                                        CollapsedSize="0"
                                        ExpandedSize="200"
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
                            <asp:Panel ID="pnljvHeader" runat="server" CssClass="cpHeaderContent" Width="99%">
                                    <asp:Label ID="lbljvheader" Text="Journal Post" runat="server" />
                            </asp:Panel>
          
                            <asp:Panel ID="pnljvbody" runat="server" CssClass="cpBodyContent" Width="99%" Height="150px">
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
                                        Post&nbsp; Date:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtJvdate" runat="server" Width="300px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                                            Format="dd/MM/yyyy" TargetControlID="txtJvdate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                                                ControlToValidate="txtJvdate" runat="server" 
                                                ErrorMessage="Enter Valid Date" ValidationGroup="HdrGrp"></asp:RequiredFieldValidator> 
                                    </td>                    
                                </tr>                    
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:TextBox ID="TextBox3" runat="server" Width="300px" Visible="False"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButtonList ID="rdoOption0" runat="server" RepeatDirection="Horizontal" Enabled="False">
                                            <asp:ListItem Selected="True">History</asp:ListItem>
                                            <asp:ListItem>Summary</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="center" style="text-align: right">
                                        <asp:Button ID="btnShowJV" runat="server" Text="Show JV" 
                                            onclick="btnShowJV_Click" Width="100px" />
                                        <asp:Button ID="btnJvpost" runat="server" onclick="btnJvpost_Click" 
                                            Text="Create JV" Width="100px" />
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>               
                    
                            </table>  
                            </asp:Panel>
                                        <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" 
                                        TargetControlID="pnljvbody" 
                                        CollapseControlID="pnljvHeader" 
                                        ExpandControlID="pnljvHeader"
                                        Collapsed="false" 
                                        TextLabelID="lbljvheader" 
                                        CollapsedText="Journal Post" 
                                        ExpandedText="Journal Post"
                                        CollapsedSize="0"
                                        ExpandedSize="150"
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
                   <asp:Panel ID="Panel2" runat="server" CssClass="cpHeaderContent" Width="99%">
                     <asp:Label ID="Label1" Text="View Depreciation " runat="server" />
               </asp:Panel>  
                   <table>
                       <tr>
                           <td>From&nbsp; Date: </td>
                           <td>
                               <asp:TextBox ID="txtDepFromDate" runat="server" Width="300px"></asp:TextBox>
                               <cc1:CalendarExtender ID="txtDepFromDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDepFromDate">
                               </cc1:CalendarExtender>
                           </td>
                           <td>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFromDate" ErrorMessage="Enter Valid Date" ValidationGroup="HdrGrp"></asp:RequiredFieldValidator>
                           </td>
                       </tr>
                       <tr>
                           <td>To Date: </td>
                           <td>
                               <asp:TextBox ID="txtDepToDate" runat="server" Width="300px"></asp:TextBox>
                               <cc1:CalendarExtender ID="txtDepToDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDepToDate">
                               </cc1:CalendarExtender>
                           </td>
                           <td>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtToDate" ErrorMessage="Enter Valid Date" ValidationGroup="HdrGrp"></asp:RequiredFieldValidator>
                           </td>
                       </tr>
                       <tr>
                           <td>
                               <asp:RadioButtonList ID="rdoOpt" runat="server" RepeatDirection="Horizontal">
                                   <asp:ListItem Selected="True">History</asp:ListItem>
                                   <asp:ListItem>Summary</asp:ListItem>
                               </asp:RadioButtonList>
                           </td>
                           <td align="center" style="text-align: right">
                               <asp:Button ID="BtnViewDepreciation" runat="server" onclick="BtnViewDepreciation_Click" Text="View Item DepreCiation" Width="150px" />
                           </td>
                       </tr>
                       <tr>
                           <td>
                               &nbsp;</td>
                           <td align="center" style="text-align: right">
                               <asp:Button ID="btnExportActiveEmployee" runat="server" CssClass="btn2" OnClick="btnExportActiveEmployee_Click" Text="Export" Width="150px" />
                           </td>
                       </tr>
                   </table>
                             </div>
                      
                             <br />               
                                                                   
                                      
                         <br />
                                                  
                         <div align="center">
                             <table style="width:100%;">
                                 <tr>
                                     <td align="center">
                                         <asp:GridView ID="GridView1"  Width="100%" runat="server" BackColor="White" 
                                             BorderColor="#32A545" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                                             Font-Names="Arial" Font-Size="Small" ForeColor="#333333" 
                                             onselectedindexchanged="GridView1_SelectedIndexChanged" HorizontalAlign="Left">                                             
                                         </asp:GridView>
                                     </td>
                                 </tr>
                             </table>
                         </div>
                         <br />
                         <div align="center" CssClass="cpBody">                         
                             <br />
                             
                             <asp:Button ID="btnHold" runat="server" Text="Hold" Width="60px" 
                    Visible="False" ValidationGroup="HdrGrp" />                        
                    
                             &nbsp;<asp:Button ID="btnPost" runat="server" Text="Post" Width="60px" 
                    Visible="False" ValidationGroup="HdrGrp" />                        
    
                         </div>                         
                         <br />            
                     <asp:Panel ID="Panel1" runat="server" Style="border-right: black 2px solid; padding-right: 20px;
                     border-top: black 2px solid; display: none; padding-left: 20px; padding-bottom: 20px;
                     border-left: black 2px solid; padding-top: 20px; border-bottom: black 2px solid;
                     background-color: white" Width="340px">
                    <table id="tblPopUp" runat="server" style="width: 328px">
                 <tr>
                     <td style="height: 21px" align="center" colspan="3">
                         <asp:Label ID="lblMsgHdr" runat="server"></asp:Label>
                     </td>
                 </tr>
                        <tr>
                            <td style="width: 151px; height: 21px">
                            </td>
                            <td style="height: 21px">
                            </td>
                            <td style="height: 21px">
                            </td>
                        </tr>
                 <tr>
                     <td style="width: 151px" align="right">
                         Purchase Order No</td>
                     <td>
                         :</td>
                     <td>
                         <asp:TextBox ID="txtPoRef" runat="server" ReadOnly="true" Enabled="false"  BorderStyle="None"></asp:TextBox></td>
                 </tr>
                 <tr>
                     <td style="width: 151px">
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td colspan="3" style="height: 18px; text-align: center">
                         </td>
                 </tr>
             </table>             
                    <div style="text-align: center">
                        <asp:Button ID="btnOk" runat="server"  Text="Ok"   Width="58px" />                        
                    </div>
                    </asp:Panel>
         
                    <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                        CancelControlID="btnOk"  PopupControlID="Panel1" TargetControlID="btnOk">
                    </cc1:ModalPopupExtender>  
                         </ContentTemplate>
                        <Triggers>
                         <asp:PostBackTrigger ControlID="btnExportActiveEmployee"/>
                         <%--<asp:AsyncPostBackTrigger ControlID="txtQuantity" EventName="TextChanged"/>--%>
                         <%--<asp:AsyncPostBackTrigger ControlID="txtRate" EventName="TextChanged"/>--%>
                         <%--<asp:AsyncPostBackTrigger ControlID="btnHold" EventName="Click" />--%>
                         </Triggers>                            
                         </asp:UpdatePanel>
                         </td>                         
                     <td>
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td>
                         &nbsp;</td>
                     <td>                                        
                     </td>
                     <td>
                         &nbsp;</td>
                 </tr>
             </table>
    </div>
             </asp:Content>