<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true"  CodeFile="frmMRR1.aspx.cs" Inherits="modules_FixedAsset_TransactionDetails_frmMRR1" %>

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
            .hideGridColumn
            {
                display:none;
            }
    </style>           
             <table style="width:100%;">
                 <tr>
                     <td>
                         &nbsp;</td>
                     <td align="center">
                         Material Receive (MRR)</td>
                     <td>
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td>
                         &nbsp;</td>
                     <td>
                         <asp:UpdatePanel ID="updtPnl" runat="server">
                         <ContentTemplate>
                         <div align="center">
                         <asp:Panel ID="pnlSrchMrrHdr" runat="server" CssClass="cpHeader" Width="800px" >
                            <asp:Label ID="lblSearchMrr" Text="Search MRR" runat="server" />
                         </asp:Panel>
                         <asp:Panel ID="pnlSrchMrrDet" runat="server" CssClass="cpBody" Width="800px"  
                                 Height="50px">
                            <table>
                <tr>
                    <td>
                        Search MRR:
                    </td>
                    <td>
                        <asp:TextBox ID="txtMrrSearch" Width="300px" runat="server"></asp:TextBox>                    
                        <cc1:AutoCompleteExtender ID="txtMrrSearch_AutoCompleteExtender" runat="server" 
                            BehaviorID="AutoCompleteExMrrSrch" CompletionInterval="100" 
                            CompletionListCssClass="autocomplete_completionListElement" 
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                            CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" 
                            DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="3" 
                            ServiceMethod="GetInvMrrListAll"
                            ServicePath="~/ClientSide/Inventory/services/InvAutoComplete.asmx" 
                            ShowOnlyCurrentWordInCompletionListItem="true" 
                            TargetControlID="txtMrrSearch">
                        </cc1:AutoCompleteExtender>
                    </td>   
                    <td>
                        <asp:Button ID="btnSearchMrr" runat="server" onclick="btnSearch_Click" Text="Search" />
                    </td>    
                    <td>
                    <asp:Button ID="btnClearMrr" runat="server"  Text="Clear" 
                            Width="60px" onclick="btnClearMrr_Click" Visible="False" />
                    </td>                
                    <td>
                        &nbsp;<asp:Label ID="lblEditFlag" runat="server" Text="N"></asp:Label>
                        &nbsp;
                    </td>
                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td align="center">
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" 
                                            Width="60px" Visible="False" onclick="btnPrint_Click" />
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
            </table>  
                         </asp:Panel>
                         <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderSrchMrr" runat="server" 
                            TargetControlID="pnlSrchMrrDet" 
                            CollapseControlID="pnlSrchMrrHdr" 
                            ExpandControlID="pnlSrchMrrHdr"
                            Collapsed="true" 
                            TextLabelID="lblSearchMrr" 
                            CollapsedText="Search MRR" 
                            ExpandedText="Search MRR"
                            CollapsedSize="0"
                            ExpandedSize="50"
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
                         <asp:Panel ID="pnlSelectPoHdr" runat="server" CssClass="cpHeader" Width="800px">
                            <asp:Label ID="lblSelectPO" Text="Select PO" runat="server" />
                         </asp:Panel>
                         <asp:Panel ID="pnlSelectPoBody" runat="server" CssClass="cpBody" Width="800px" 
                                 Height="275px">
                                 
                                 <table>
                                 <tr>
                                 <td>
                                 Select PO:
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txtSelectPO" runat="server" Width="300px"></asp:TextBox>
                                 <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSelectPo" runat="server" 
                            BehaviorID="AutoCompleteExSelectPo" CompletionInterval="100" 
                            CompletionListCssClass="autocomplete_completionListElement" 
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                            CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" 
                            DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="3" 
                            ServiceMethod="GetInvRemPoList" 
                            ServicePath="~/ClientSide/Inventory/services/InvAutoComplete.asmx"
                            ShowOnlyCurrentWordInCompletionListItem="true" 
                            TargetControlID="txtSelectPO">
                        </cc1:AutoCompleteExtender>
                                 </td>
                                 <td>
                                     <asp:Button ID="btnSelectPo" runat="server" Text="Select" onclick="btnSelectPo_Click" />
                                 </td>
                                     <td>
                                         <asp:Button ID="btnClearPo" runat="server" 
                                             onclick="btnClearPo_Click" Text="Clear" Visible="False" Width="60px" />
                                     </td>
                                 </tr>
                                     
                                 </table>
                                 
                                 <div id="Div2" style="width: 750px; height: 232px; overflow:auto;" runat="server">                                                                  
                                             <asp:GridView ID="gvPoDet" runat="server" AutoGenerateColumns="False" 
                                                 BackColor="#003366" CellSpacing="1" BorderStyle="Solid" BorderWidth="1px" 
                                                 CellPadding="4" Font-Names="Arial" Font-Size="8pt" ForeColor="#333333" 
                                                 onselectedindexchanged="gvPoDet_SelectedIndexChanged" RowStyle-VerticalAlign="Bottom"
                                                 onrowcommand="gvPoDet_RowCommand" onrowdatabound="gvPoDet_RowDataBound">
                                                 <Columns>
                                                     <asp:BoundField DataField="PO_Det_Lno" HeaderText="L#" 
                                                     HeaderStyle-CssClass = "hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                                                         <HeaderStyle HorizontalAlign="Left" />
                                                     </asp:BoundField>
                                                     <asp:BoundField DataField="PO_Det_Icode" HeaderText="Item Code" >
                                                         <HeaderStyle HorizontalAlign="Left" />
                                                     </asp:BoundField>
                                                     <asp:BoundField DataField="PO_Det_Itm_Desc" HeaderText="Item Name" >
                                                         <HeaderStyle HorizontalAlign="Left" />
                                                     </asp:BoundField>
                                                     <asp:BoundField DataField="PO_Det_Itm_Uom" HeaderText="UOM" >
                                                         <HeaderStyle HorizontalAlign="Left" />
                                                     </asp:BoundField>
                                                     <asp:BoundField DataField="PO_Det_Str_Code" HeaderText="Store" >                                                                                                          
                                                         <HeaderStyle HorizontalAlign="Left" />
                                                     </asp:BoundField>
                                                     <asp:BoundField DataField="PO_Det_Bal_Qty" HeaderText="P.O Qty." >                                                     
                                                         <HeaderStyle HorizontalAlign="Left" />
                                                     </asp:BoundField>
                                                     <asp:BoundField DataField="PO_Det_Lin_Rat" HeaderText="Rate" 
                                                         DataFormatString="{0:F4}" >
                                                         <HeaderStyle HorizontalAlign="Left" />
                                                         <ItemStyle HorizontalAlign="Right" />
                                                     </asp:BoundField>
                                                     <asp:TemplateField HeaderText="MRR Qty.">
                                                         <EditItemTemplate>
                                                             <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Po_Det_Mrr_Qty") %>'></asp:TextBox>
                                                         </EditItemTemplate>
                                                         <ItemTemplate>                                                             
                                                             <asp:TextBox ID="txtMrrQty" Width="50px" runat="server" Text='<%# Bind("Po_Det_Mrr_Qty") %>'></asp:TextBox>
                                                             <asp:CompareValidator ID="CompareValidator1" runat="server"
                                                                ControlToValidate="txtMrrQty"
                                                                ErrorMessage="The MRR quantity must be less than PO quantity." 
                                                                Operator="LessThanEqual" Type="Currency" ValidationGroup="grpMrrQty"
                                                                ValueToCompare='<%# Bind("PO_Det_Bal_Qty") %>'>*</asp:CompareValidator>
                                                         </ItemTemplate>
                                                         <HeaderStyle HorizontalAlign="Left" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Serial">
                                                         <EditItemTemplate>
                                                             <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                         </EditItemTemplate>
                                                         <ItemTemplate>
                                                             <asp:TextBox ID="txtSerial" runat="server" TextMode="MultiLine" Width="200px" Text='<%# Bind("Po_Det_Serial_No") %>'></asp:TextBox>
                                                         </ItemTemplate>
                                                     </asp:TemplateField>
                                                     <asp:CommandField ButtonType="Image" SelectImageUrl="~/Images/accept-icon.png" 
                                                         ShowSelectButton="True" ValidationGroup="grpMrrQty" HeaderText="Accept" >                                                         
                                                         <ItemStyle HorizontalAlign="Center" />
                                                     </asp:CommandField>
                                                     <asp:TemplateField HeaderText="Emp Image" Visible="False">
                                                         <ItemTemplate>
                                                            <asp:LinkButton ID="lnkimage" runat ="server" ></asp:LinkButton>
                                                                
                                                         </ItemTemplate>
                                                     </asp:TemplateField>
                                                 </Columns>
                                             </asp:GridView>
                                             </div>
                                 
                         </asp:Panel>
                         <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderSelectPO" runat="server" 
                            TargetControlID="pnlSelectPoBody" 
                            CollapseControlID="pnlSelectPoHdr" 
                            ExpandControlID="pnlSelectPoHdr"
                            Collapsed="false" 
                            TextLabelID="lblSelectPO" 
                            CollapsedText="Select PO" 
                            ExpandedText="Select PO"
                            CollapsedSize="0"
                            ExpandedSize="275"
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
                         <asp:Panel ID="pHeaderText" runat="server" CssClass="cpHeader" Width="800px" >
                            <asp:Label ID="lblText" Text="Header" runat="server" />
                         </asp:Panel>
                         <asp:Panel ID="pHdrBody" runat="server" CssClass="cpBody" Width="800px" 
                                 Height="195px">
                            <table style="height: 162px">
                <tr>
                    <td>
                        Supplier:
                    </td>
                    <td>
                        <asp:TextBox ID="txtSupplier" Width="300px" runat="server"></asp:TextBox>                    
                    </td>   
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                            ControlToValidate="txtSupplier" runat="server" ErrorMessage="Enter Supplier" 
                            ValidationGroup="HdrGrp"></asp:RequiredFieldValidator>
                    </td>                    
                </tr>
                <tr>
                    <td>
                        Receipt Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtReceiptDate" runat="server" Width="300px"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtReceiptDate" runat="server" Format="dd/MM/yyyy"/>
                        </td>
                        <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" 
                                ControlToValidate="txtReceiptDate" runat="server" 
                                ErrorMessage="Enter Valid Receipt Date" ValidationGroup="HdrGrp"></asp:RequiredFieldValidator> 
                    </td>                    
                </tr>
                <tr>
                    <td>
                        Invoice Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtInvoiceDate" runat="server" Width="300px"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txtInvoiceDate" runat="server" Format="dd/MM/yyyy"/>
                        </td>
                        <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" 
                                ControlToValidate="txtInvoiceDate" runat="server" 
                                ErrorMessage="Enter Valid Invoice Date" ValidationGroup="HdrGrp"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        DC No:</td>
                    <td>
                        <asp:TextBox ID="txtDcNo" runat="server" Width="300px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                            ControlToValidate="txtDcNo" ErrorMessage="Enter Delivary Chalan No" 
                            ValidationGroup="HdrGrp"></asp:RequiredFieldValidator>
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
                            Collapsed="true" 
                            TextLabelID="lblText" 
                            CollapsedText="Show Header" 
                            ExpandedText="Hide Header"
                            CollapsedSize="0"
                            ExpandedSize="195"
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
                         <asp:Panel ID="pDetailsText" runat="server" CssClass="cpHeader" Width="800px" >
                            <asp:Label ID="lblDetText" Text="Details" runat="server" />
                         </asp:Panel>
                         <asp:Panel ID="pDetBody" runat="server" CssClass="cpBody" Width="800px"
                                 Height="270px">      
                                 <div style="width: 771px; height: 225px; overflow:auto;" runat="server">
                                 <asp:GridView ID="gvMRR" runat="server" 
                                              BorderStyle="Solid" BorderWidth="0px" CellPadding="4" Font-Names="Arial" 
                                              Font-Size="8pt" BackColor="#003366" CellSpacing="1"
                                              onselectedindexchanged="gvMRR_SelectedIndexChanged" 
                                         AutoGenerateColumns="False" Width="832px">
                                              <Columns>
                                                  <asp:CommandField ButtonType="Image" HeaderText="Remove" 
                                                      SelectImageUrl="~/Images/remove.png" SelectText="Remove" 
                                                      ShowSelectButton="True">
                                                      <ItemStyle ForeColor="Red" HorizontalAlign="Center" />
                                                  </asp:CommandField>
                                                  <asp:BoundField DataField="L#" HeaderText="L#" 
                                                  HeaderStyle-CssClass = "hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                                                      <HeaderStyle CssClass="hideGridColumn" />
                                                      <ItemStyle CssClass="hideGridColumn" />
                                                  </asp:BoundField>
                                                  <asp:BoundField DataField="Item Code" HeaderText="Item Code"/>
                                                  <asp:BoundField DataField="Item Name" HeaderText="Item Name"/>
                                                  <asp:BoundField DataField="UOM" HeaderText="UOM"/>
                                                  <asp:BoundField DataField="Store" HeaderText="Store"/>
                                                  <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                                      <ItemStyle HorizontalAlign="Right" />
                                                  </asp:BoundField>
                                                  <asp:BoundField DataField="Rate" HeaderText="Rate">
                                                      <ItemStyle HorizontalAlign="Right" Width="60px" />
                                                  </asp:BoundField>
                                                  <asp:BoundField  DataField="Amount" HeaderText="Amount">
                                                      <ItemStyle HorizontalAlign="Right" Width="60px"/>
                                                  </asp:BoundField>
                                                  <asp:TemplateField HeaderText="Serial">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Serial") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                        <div style="width: 280px; word-wrap: break-word;">
                                                          <asp:Label ID="Label1" runat="server" Text='<%# Bind("Serial") %>'></asp:Label>
                                                        </div>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                              </Columns>
                                          </asp:GridView>   
                                 </div>
                                 <br />
                              <span>
                              <asp:Button ID="btnHold" runat="server" onclick="btnHold_Click" Text="Hold" />
                                 &nbsp;&nbsp;&nbsp;
                              <asp:Button ID="btnPost" runat="server" onclick="btnPost_Click" Text="Post" 
                                  Visible="False" Width="60px" />                                                               
                              </span>                              
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
                            ExpandedSize="270"
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
                         MRR Ref No</td>
                     <td>
                         :</td>
                     <td>
                         <asp:TextBox ID="txtMrrRef" ReadOnly="true" Enabled="false" runat="server"  BorderStyle="None"></asp:TextBox></td>
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
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                 </tr>
             </table>                             
     </asp:Content>