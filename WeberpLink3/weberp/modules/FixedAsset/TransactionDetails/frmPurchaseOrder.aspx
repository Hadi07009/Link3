<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmPurchaseOrder.aspx.cs" Inherits="modules_FixedAsset_TransactionDetails_frmPurchaseOrder" %>
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
        </style> 
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="Purchase Order" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="900px">
              <table style="width:100%;">
                 <tr>
                     <td>
                         &nbsp;</td>
                     <td>
                         <asp:UpdatePanel ID="updtPnl" runat="server">
                         <ContentTemplate>
                         <div align="center">
                         <asp:Panel ID="pnlSrchHdr" runat="server" CssClass="cpHeader" Width="600px">
                            <asp:Label ID="lblSearchHdr" Text="Search PO" runat="server" />
                         </asp:Panel>
                         <asp:Panel ID="pnlSrchDet" runat="server" CssClass="cpBody" Width="600px" 
                                 Height="34px">
                            <table>
                <tr>
                    <td>
                        Search PO:
                    </td>
                    <td>
                        <asp:TextBox ID="txtPoSearch" Width="300px" runat="server" autoComplete="off" 
                            AutoPostBack="True" ontextchanged="txtItem_TextChanged"></asp:TextBox>                    
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
                        <asp:Button ID="btnSearch" runat="server" onclick="btnSearch_Click" Text="Search" />
                    </td>    
                    <td>
                    <asp:Button ID="btnClear" runat="server"  Text="Clear" 
                            Width="60px" onclick="btnClear_Click" Visible="False" />
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
                            CollapsedText="Search PO" 
                            ExpandedText="Search PO"
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
                         <asp:Panel ID="pHeaderText" runat="server" CssClass="cpHeader" Width="600px">
                            <asp:Label ID="lblText" Text="Header" runat="server" />
                         </asp:Panel>
                         <asp:Panel ID="pHdrBody" runat="server" CssClass="cpBody" Width="600px" 
                                 Height="175px">
                            <table>
                <tr>
                    <td>
                        Supplier:
                    </td>
                    <td>
                        <asp:TextBox ID="txtSupplier" Width="300px" runat="server"></asp:TextBox>     
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSupp" runat="server" 
                            BehaviorID="AutoCompleteExSupp" CompletionInterval="100" 
                            CompletionListCssClass="autocomplete_completionListElement" 
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                            CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" 
                            DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="3" 
                            ServiceMethod="GetInvSupplierList" 
                            ServicePath="~/ClientSide/Inventory/services/InvAutoComplete.asmx" 
                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSupplier">
                        </cc1:AutoCompleteExtender>               
                    </td>   
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                            ControlToValidate="txtSupplier" runat="server" ErrorMessage="Enter Supplier" 
                            ValidationGroup="HdrGrp" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            
                        <br />
                            
                        <asp:CustomValidator ID="CustomValidator1" runat="server" OnServerValidate="CustomValidator1_ServerValidate"
                             ControlToValidate="txtSupplier"
                             ErrorMessage="Supplier does not exists" ValidationGroup="HdrGrp"
                             ToolTip="Please select a different Supplier" SetFocusOnError="True"></asp:CustomValidator>
                    </td>                    
                </tr>
                <tr>
                    <td>
                        Order Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtOrderDate" runat="server" Width="300px"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtOrderDate" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                        </td>
                        <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" 
                                ControlToValidate="txtOrderDate" runat="server" 
                                ErrorMessage="Enter Valid Order Date" ValidationGroup="HdrGrp" 
                                SetFocusOnError="True"></asp:RequiredFieldValidator> 
                    </td>                    
                </tr>
                <tr>
                    <td>
                        Due Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtDueDate" runat="server" Width="300px"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDueDate" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                        </td>
                        <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" 
                                ControlToValidate="txtDueDate" runat="server" 
                                ErrorMessage="Enter Valid Due Date" ValidationGroup="HdrGrp" 
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
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
                                            ValidationGroup="HdrGrp" Width="60px" />
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
                         <div align="center">
                         <asp:Panel ID="pDetailsText" runat="server" CssClass="cpHeader" Width="600px">
                            <asp:Label ID="lblDetText" Text="Details" runat="server" />
                         </asp:Panel>
                         <asp:Panel ID="pDetBody" runat="server" CssClass="cpBody" Width="600px" 
                                 Height="200px">
                              <table style="width: 551px">
                <tr>
                    <td class="style1">
                        &nbsp;</td>
                    <td>
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
                                ValidationGroup="DetGrp" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <br />
                            <asp:CustomValidator ID="CustomValidator2" runat="server" 
                                ControlToValidate="txtItem" 
                                ErrorMessage="Item does not exists" 
                                OnServerValidate="CustomValidator2_ServerValidate" 
                                ToolTip="Please select a different Item" ValidationGroup="DetGrp" 
                                SetFocusOnError="True"></asp:CustomValidator>
                    </td>                    
                </tr>
                <tr>
                    <td class="style1">
                        &nbsp;</td>
                    <td>
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
                    <td>
                        Store:
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtStore" runat="server" autoComplete="off" 
                            AutoPostBack="True" Width="300px" ontextchanged="txtStore_TextChanged"></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="txtStore_AutoCompleteExtender" runat="server" 
                            BehaviorID="AutoCompleteEx3" CompletionInterval="100" 
                            CompletionListCssClass="autocomplete_completionListElement" 
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                            CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" 
                            DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="3" 
                            ServiceMethod="GetInvStoreList" 
                            ServicePath="~/ClientSide/Inventory/services/InvAutoComplete.asmx" 
                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtStore">
                        </cc1:AutoCompleteExtender>
                        </td>
                        <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" 
                                ControlToValidate="txtStore" runat="server" ErrorMessage="Enter Valid Store" 
                                ValidationGroup="DetGrp" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <br />
                            <asp:CustomValidator ID="CustomValidator3" runat="server" 
                                ControlToValidate="txtStore" 
                                ErrorMessage="Store does not exists" 
                                OnServerValidate="CustomValidator3_ServerValidate" 
                                ToolTip="Please select a different Store" ValidationGroup="DetGrp"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        &nbsp;</td>
                    <td>
                        Quantity:
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtQuantity" runat="server" Width="300px" AutoPostBack="True" 
                            ontextchanged="txtQuantity_TextChanged"></asp:TextBox>
                        </td>
                        <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" 
                                ControlToValidate="txtQuantity" runat="server" 
                                ErrorMessage="Enter Item Quantity" ValidationGroup="DetGrp" 
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <br />
                            <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                ControlToValidate="txtQuantity" ErrorMessage="Enter Valid Quantity" 
                                Operator="DataTypeCheck" Type="Currency" ValidationGroup="DetGrp" 
                                SetFocusOnError="True"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        &nbsp;</td>
                    <td>
                        Rate:
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtRate" runat="server" Width="300px" AutoPostBack="True" 
                           AutoCompleteType="Disabled" ontextchanged="txtRate_TextChanged"></asp:TextBox>
                        </td>
                        <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" 
                                ControlToValidate="txtRate" runat="server" ErrorMessage="Enter Item Rate" 
                                ValidationGroup="DetGrp"></asp:RequiredFieldValidator>
                            <br />
                            <asp:CompareValidator ID="CompareValidator2" runat="server" 
                                ControlToValidate="txtRate" ErrorMessage="Enter Valid Rate" 
                                Operator="DataTypeCheck" Type="Double" ValidationGroup="DetGrp"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        &nbsp;</td>
                    <td>
                        Amount:
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtAmount" runat="server" BackColor="#CCCCCC" ReadOnly="True" 
                            Width="300px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                                  <tr>
                                      <td class="style1">
                                          &nbsp;</td>
                                      <td>
                                          &nbsp;</td>
                                      <td align="center">
                                          <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" 
                                              Width="60px" ValidationGroup="DetGrp" />
                                      </td>
                                      <td align="center">
                                          <asp:Button ID="btnEdit" runat="server" Text="Edit" Width="60px" />
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
                         <br />
                         <div align="center">                         
                             <asp:GridView ID="GridView1" runat="server" BackColor="#003366"
                                 BorderStyle="None" CellPadding="4" 
                                 ForeColor="#333333" CellSpacing="1"
                                 onselectedindexchanged="GridView1_SelectedIndexChanged" Font-Names="Arial" 
                                 Font-Size="10pt" AutoGenerateColumns="False" >
                                 <Columns>
                                     <asp:CommandField SelectText="Remove" ShowSelectButton="True" 
                                         ButtonType="Image" SelectImageUrl="~/Images/remove.png">
                                         <ItemStyle ForeColor="Red" />
                                     </asp:CommandField>
                                     <asp:BoundField DataField="Item Code" HeaderText="Item Code" />
                                     <asp:BoundField DataField="Item Name" HeaderText="Item Name" />
                                     <asp:BoundField DataField="UOM" HeaderText="UOM" />
                                     <asp:BoundField DataField="Store" HeaderText="Store" />
                                     <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                         <ItemStyle HorizontalAlign="Right"/>
                                     </asp:BoundField>
                                     <asp:BoundField DataField="Rate" HeaderText="Rate" >
                                         <ItemStyle HorizontalAlign="Right" Width="60px" />
                                     </asp:BoundField>
                                     <asp:BoundField DataField="Amount" HeaderText="Amount" >
                                         <ItemStyle HorizontalAlign="Right" Width="60px" />
                                     </asp:BoundField>
                                 </Columns>
                             </asp:GridView>
                             <br />
                             
                             <asp:Button ID="btnHold" runat="server" Text="Hold" Width="60px" 
                    Visible="False" onclick="btnHold_Click" ValidationGroup="HdrGrp" />                        
                    
                             &nbsp;<asp:Button ID="btnPost" runat="server" Text="Post" Width="60px" 
                    onclick="btnPost_Click" Visible="False" ValidationGroup="HdrGrp" />                        
    
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
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
