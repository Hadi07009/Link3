<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true"  CodeFile="frmStockTransfer.aspx.cs" Inherits="modules_FixedAsset_TransactionDetails_frmStockTransfer" %>
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
            .style1
            {
                width: 96px;
            }
            .style3
            {
                width: 30px;
            }
            .style4
            {
                width: 223px;
            }      
            .grid
            {
                font-family: lucida grande,arial,helvetica,sans-serif;
                font-size: 12px;
                width: 850px;
                word-break: break-all;
                word-wrap: break-word;              
            }              
        </style>   
    
        <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
            <asp:Label ID="lblleave" Text="STOCK TRANSFER OF ITEM" runat="server" />
         </asp:Panel>  
          
             <table style="width:100%;">
                 <tr>
                     <td>
                         &nbsp;</td>
                     <td>
                         <asp:UpdatePanel ID="updtPnl" runat="server">
                         <ContentTemplate>
                         <div align="center">
                         <asp:Panel ID="pnlSrchHdr" runat="server" CssClass="cpHeaderContent" Width="99%" Visible="False">
                            <asp:Label ID="lblSearchHdr" Text="Search Transfer" runat="server" />
                         </asp:Panel>
                         <asp:Panel ID="pnlSrchDet" runat="server" CssClass="cpBodyContent" Width="99%" 
                                 Height="34px" Visible="False">
                            <table>
                <tr>
                    <td>
                        Search Ref No:
                    </td>
                    <td>
                        <asp:TextBox ID="txtTransSearch" Width="300px" runat="server" autoComplete="off" 
                            AutoPostBack="True" ontextchanged="txtItem_TextChanged"></asp:TextBox>                    
                        <cc1:AutoCompleteExtender ID="txtTransSearch_AutoCompleteExtender" runat="server" 
                            BehaviorID="AutoCompleteEx1" CompletionInterval="100" 
                            CompletionListCssClass="autocomplete_completionListElement" 
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                            CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" 
                            DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="3" 
                            ServiceMethod="GetInvTransListAll" 
                            ServicePath="~/ClientSide/Inventory/services/InvAutoComplete.asmx" 
                            ShowOnlyCurrentWordInCompletionListItem="true" 
                            TargetControlID="txtTransSearch">
                        </cc1:AutoCompleteExtender>
                    </td>   
                    <td>
                        <asp:Button ID="btnSearch" runat="server" onclick="btnSearch_Click" Text="Search" />
                    </td>    
                    <td>
                    <asp:Button ID="btnClear" runat="server"  Text="Clear" onclick="btnClear_Click" Visible="False" />
                    </td>                
                    <td>
                        &nbsp;&nbsp;<asp:Label ID="lblEditFlag" runat="server" Text="N"></asp:Label>
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
                            CollapsedText="Search Transfer" 
                            ExpandedText="Search Transfer"
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
                            <asp:Label ID="lblText" Text="Header" runat="server" />
                         </asp:Panel>
                         <asp:Panel ID="pHdrBody" runat="server" CssClass="cpBodyContent" Width="99%" 
                                 Height="175px">
                            <table>
                <tr>
                    <td>
                        From Store</td>
                    <td>
                        <asp:TextBox ID="txtFromStore" Width="300px" runat="server"></asp:TextBox>     
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtenderFromeStore" runat="server" 
                            BehaviorID="AutoCompleteExFromStore" CompletionInterval="100" 
                            CompletionListCssClass="autocomplete_completionListElement" 
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                            CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" 
                            DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="1" 
                            ServiceMethod="GetInvStoreList" 
                            ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx"
                            ShowOnlyCurrentWordInCompletionListItem="true" 
                            TargetControlID="txtFromStore">
                        </cc1:AutoCompleteExtender>               
                    </td>   
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                            ControlToValidate="txtFromStore" runat="server" ErrorMessage="Enter From Store" 
                            ValidationGroup="HdrGrp"></asp:RequiredFieldValidator>
                            
                        <br />
                            
                        <asp:CustomValidator ID="CustomValidator1" runat="server" OnServerValidate="CustomValidator1_ServerValidate"
                             ControlToValidate="txtFromStore" Display="Dynamic"
                             ErrorMessage="Store does not exists" ValidationGroup="HdrGrp"
                             ToolTip="Please select a different Store"></asp:CustomValidator>
                    </td>                    
                </tr>
                <tr>
                    <td>
                        To Store</td>
                    <td>
                        <asp:TextBox ID="txtToStore" runat="server" Width="300px"></asp:TextBox>                        
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtenderToStore" runat="server" 
                            BehaviorID="AutoCompleteExToStore" CompletionInterval="100" 
                            CompletionListCssClass="autocomplete_completionListElement" 
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                            CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" 
                            DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="1" 
                            ServiceMethod="GetInvStoreList" 
                            ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx" 
                            ShowOnlyCurrentWordInCompletionListItem="true" 
                            TargetControlID="txtToStore">
                        </cc1:AutoCompleteExtender>   
                        </td>
                        <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" 
                                ControlToValidate="txtToStore" runat="server" 
                                ErrorMessage="Enter To Store" ValidationGroup="HdrGrp"></asp:RequiredFieldValidator> 
                            <br />
                            <asp:CustomValidator ID="CustomValidator4" runat="server" 
                                ControlToValidate="txtToStore" Display="Dynamic" 
                                ErrorMessage="Store does not exists" 
                                OnServerValidate="CustomValidator2_ServerValidate" 
                                ToolTip="Please select a different Store" ValidationGroup="HdrGrp"></asp:CustomValidator>
                    </td>                    
                </tr>
                <tr>
                    <td>
                        Transfer Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtTransferDate" runat="server" Width="300px"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" 
                            TargetControlID="txtTransferDate" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                        </td>
                        <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" 
                                ControlToValidate="txtTransferDate" runat="server" 
                                ErrorMessage="Enter Valid Transfer Date" ValidationGroup="HdrGrp"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Remarks:
                    </td>
                    <td>
                        <asp:TextBox ID="txtRemarks" runat="server" Width="300px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td align="center">
                                        <asp:Button ID="btnNext" runat="server" onclick="btnNext_Click" Text="Next" 
                                            ValidationGroup="HdrGrp" Width="60px" Visible="False" />
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
            </table>  
                         </asp:Panel>
                         <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderHdr" runat="server" 
                            TargetControlID="pHdrBody" 
                            CollapseControlID="pHeaderText" 
                            ExpandControlID="pHeaderText"
                            Collapsed="false" 
                            TextLabelID="lblText" 
                            CollapsedText="Show Header" 
                            ExpandedText="Hide Header"
                            CollapsedSize="0"
                            ExpandedSize="175"
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
                         <br />
                         <div align="center" >
                         <asp:Panel ID="pDetailsText" runat="server" CssClass="cpHeaderContent" Width="99%">
                            <asp:Label ID="lblDetText" Text="Details" runat="server" />
                         </asp:Panel>
                         <asp:Panel ID="pDetBody" runat="server" CssClass="cpBodyContent" Width="99%" 
                                 Height="350px">
                              <table id="tblTransDet" style="width: 592px" runat="server">
                <tr>
                    <td class="style1">
                        &nbsp;</td>
                    <td class="style4">
                        Item:
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtItem" runat="server" autoComplete="off" AutoPostBack="True" 
                            ontextchanged="txtItem_TextChanged" Width="300px"></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="autoComplete1" runat="server" 
                            BehaviorID="AutoCompleteEx2" CompletionInterval="100" 
                            CompletionListCssClass="autocomplete_completionListElement" 
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                            CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" 
                            DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="3" 
                            ServiceMethod="GetInvItemList" 
                            ServicePath="~/ClientSide/Inventory/services/InvAutoComplete.asmx" 
                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtItem">
                        </cc1:AutoCompleteExtender>
                        </td>
                        <td width="600">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" 
                                ControlToValidate="txtItem" runat="server" ErrorMessage="Enter Valid Item" 
                                ValidationGroup="DetGrp"></asp:RequiredFieldValidator>
                            <br />
                            <asp:CustomValidator ID="CustomValidator3" runat="server" 
                                ControlToValidate="txtItem" Display="Dynamic" 
                                ErrorMessage="Item does not exists" 
                                OnServerValidate="CustomValidator3_ServerValidate" 
                                ToolTip="Please select a different Item" ValidationGroup="DetGrp"></asp:CustomValidator>
                    </td>                    
                </tr>
                <tr>
                    <td class="style1">
                        &nbsp;</td>
                    <td class="style4">
                        UOM:
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtUom" runat="server" ReadOnly="True" BackColor="#CCCCCC" 
                            Width="300px"></asp:TextBox>
                    </td>                    
                </tr>
                                  <tr>
                                      <td class="style1">
                                          &nbsp;</td>
                                      <td class="style4">
                                          Current Stock</td>
                                      <td colspan="2">
                                          <asp:TextBox ID="txtCurrentStock" runat="server" BackColor="#CCCCCC" 
                                              ReadOnly="True" Width="300px"></asp:TextBox>
                                      </td>
                                  </tr>
                <tr>
                    <td class="style1">
                        &nbsp;</td>
                    <td class="style4">
                        Quantity:
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtQuantity" runat="server" Width="300px" AutoPostBack="True" ></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" 
                            ControlToCompare="txtCurrentStock" ControlToValidate="txtQuantity" 
                            ErrorMessage="Quantity should be less than or equel to current stock" 
                            Operator="LessThanEqual" Type="Currency" ValidationGroup="DetGrp"></asp:CompareValidator>
                        </td>
                        <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" 
                                ControlToValidate="txtQuantity" runat="server" 
                                ErrorMessage="Enter Item Quantity" ValidationGroup="DetGrp"></asp:RequiredFieldValidator>
                            <br />
                            <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                ControlToValidate="txtQuantity" ErrorMessage="Enter Valid Quantity" 
                                Operator="DataTypeCheck" Type="Currency" ValidationGroup="DetGrp"></asp:CompareValidator>
                                
                            <br />
                                
                    </td>
                </tr>
                                  <tr>
                                      <td class="style1">
                                          &nbsp;</td>
                                      <td class="style4">
                                          Serial:</td>
                                      <td>
                                          <asp:TextBox ID="txtSerialNo" runat="server" Width="300px" Visible="False"></asp:TextBox>
                                          <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSerial" runat="server" 
                                            BehaviorID="AutoCompleteExSerial" CompletionInterval="100" 
                                            CompletionListCssClass="autocomplete_completionListElement" 
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                                            CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" 
                                            DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="3" 
                                            ServiceMethod="GetInvSerialList" UseContextKey="true" ContextKey="Item"
                                            ServicePath="~/ClientSide/Inventory/services/InvAutoComplete.asmx" 
                                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSerialNo">
                                        </cc1:AutoCompleteExtender>
                                      </td>
                                      <td class="style3">
                                          <asp:Button ID="btnAddSerial" runat="server" Text="Add" 
                                              ValidationGroup="HdrGrp" Width="40px" onclick="btnAddSerial_Click" 
                                              Visible="False" />
                                      </td>
                                      <td>
                                          &nbsp;</td>
                                  </tr>
                <tr>
                    <td class="style1">
                        &nbsp;</td>
                    <td class="style4">
                        Serial:</td>
                    <td>
                        <asp:TextBox ID="txtSerial" runat="server" Height="40px" TextMode="MultiLine" 
                            Width="300px" Visible="False" Enabled="False" 
                            ontextchanged="txtSerial_TextChanged"></asp:TextBox>
                    </td>
                        <td>
                            <asp:Button ID="btnEditSerial" runat="server" onclick="btnEditSerial_Click" 
                                Text="Edit" ValidationGroup="HdrGrp" Visible="False" Width="40px" />
                    </td>
                        <td>
                            &nbsp;</td>
                </tr>
                                  <tr>
                                      <td class="style1">
                                          &nbsp;</td>
                                      <td class="style4">
                                          &nbsp;</td>
                                      <td align="center" colspan="2">
                                          <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" ValidationGroup="DetGrp" Width="100px" />
                                      </td>
                                      <td>
                                          &nbsp;</td>
                                  </tr>
            </table>   
                         </asp:Panel>
                         <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderDet" runat="server" 
                            TargetControlID="pDetBody" 
                            CollapseControlID="pDetailsText" 
                            ExpandControlID="pDetailsText"
                            Collapsed="true" 
                            TextLabelID="lblDetText" 
                            CollapsedText="Show Details" 
                            ExpandedText="Hide Details"
                            CollapsedSize="0"
                            ExpandedSize="350"
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
                         <br />
                         <div align="center">                         
                             <asp:GridView ID="gvStkTransfer" runat="server" BackColor="White" BorderColor="#32A545" 
                                 BorderStyle="Solid" BorderWidth="1px" CellPadding="4" Font-Names="Arial"
                                 Font-Size="8pt" ForeColor="#333333" 
                                 onselectedindexchanged="gvStkTransfer_SelectedIndexChanged" Width="99%">                                                                                                 
                                 <Columns>
                                     <asp:CommandField SelectText="Remove" ShowSelectButton="True" 
                                         ButtonType="Image" SelectImageUrl="~/Images/remove.png">
                                         <ItemStyle ForeColor="Red" />
                                     </asp:CommandField>
                                 </Columns>
                             </asp:GridView>
                         </div>                         
                         <br /> 
                         <div align="center">
                         <span>
                             <asp:Button ID="btnHold" runat="server" Text="Hold" 
                    Visible="False" onclick="btnHold_Click" ValidationGroup="HdrGrp" />                        
                    
                             &nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="btnPost" runat="server" Text="Post" Width="100px" 
                    onclick="btnPost_Click" Visible="False" ValidationGroup="HdrGrp" />                        
                         </span>     
                         </div>      
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
                         Transfer Ref No</td>
                     <td>
                         :</td>
                     <td>
                         <asp:TextBox ID="txtTransferRef" runat="server" ReadOnly="true" Enabled="false"  BorderStyle="None"></asp:TextBox></td>
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
             </asp:Content>