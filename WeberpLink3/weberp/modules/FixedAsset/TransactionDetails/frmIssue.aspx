<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" EnableEventValidation="false" ValidateRequest="false" CodeFile="frmIssue.aspx.cs" Inherits="modules_FixedAsset_TransactionDetails_frmIssue" %>

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
            .txtbox
            {
                padding: 0px;
                word-spacing:0px;                
                font-family:@Arial;
                
            }
        </style>  

    <div><asp:Panel ID="Panel2" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="Label3" Text="MATERIAL ISSUE" runat="server" />
             </asp:Panel></div>
    <table style="width:100%;">    
     <tr>
         <td>
             &nbsp;
         </td>
         
         <td>                                                                       
             <div align="center">
             <asp:Panel ID="pnlSrchIssueHdr" runat="server" CssClass="cpHeaderContent" Width="99%" Visible="False">
                <asp:Label ID="lblSearchIssue" Text="Search Issue Reference" runat="server" />
             </asp:Panel>
             <asp:Panel ID="pnlSrchIssueDet" runat="server" CssClass="cpBodyContent" Width="99%" 
                     Height="82px" Visible="False">
                <table>
                    <tr>
                        <td>
                            Search Issue:
                        </td>
                        <td>
                            <asp:TextBox ID="txtIssueSearch" runat="server" Width="300px"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="txtIssueSearch_AutoCompleteExtender" 
                                runat="server" BehaviorID="AutoCompleteExIssueSrch" CompletionInterval="100" 
                                CompletionListCssClass="autocomplete_completionListElement" 
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                                CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" 
                                DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="3" 
                                ServiceMethod="GetInvIssueListAll" 
                                ServicePath="~/ClientSide/Inventory/services/InvAutoComplete.asmx" 
                                ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtIssueSearch">
                            </cc1:AutoCompleteExtender>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" 
                                onclick="btnSearch_Click" Text="Search" />
                        </td>
                        <td>
                           <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Print" /></td>
                        <td>
                            <asp:Button ID="btnClearIssue" runat="server" 
                                onclick="btnClearIssue_Click" Text="Clear" Visible="False" Width="60px" />
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;<asp:Label ID="lblEditFlag" runat="server" Text="N"></asp:Label>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td align ="right" >
                            
                        </td>
                        <td align="right">
                            &nbsp;</td>
                        <td align="right">
                            &nbsp;</td>
                        <td>
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
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
            </table>  
            </asp:Panel>
             <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderSrchIssue" runat="server" 
                TargetControlID="pnlSrchIssueDet" 
                CollapseControlID="pnlSrchIssueHdr" 
                ExpandControlID="pnlSrchIssueHdr"
                Collapsed="true" 
                TextLabelID="lblSearchIssue" 
                CollapsedText="Search Issue" 
                ExpandedText="Search Issue"
                CollapsedSize="0"
                ExpandedSize="35"
                AutoCollapse="False"
                AutoExpand="False"
                ScrollContents="false"
                ImageControlID="Image1"
                ExpandedImage="~/images/collapse.jpg"
                CollapsedImage="~/images/expand.jpg"
                ExpandDirection="Vertical">
                </cc1:CollapsiblePanelExtender>                          
             </div>
             <br />                        
             <div align="center">
                         <asp:Panel ID="pnlSelectSrHdr" runat="server" CssClass="cpHeaderContent" Width="99%">
                            <asp:Label ID="lblSelectSR" Text="Select SR" runat="server" />
                         </asp:Panel>
                         <asp:Panel ID="pnlSelectSrBody" runat="server" CssClass="cpBodyContent" Width="99%" 
                                 Height="265px">
                                 <table>
                                 <tr>
                                 <td colspan="2">
                                     &nbsp;</td>
                                 </tr>
                                 <tr>
                                 <td>
                                     Select SR:
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txtSelectSR" runat="server" Width="300px"></asp:TextBox>
                                 <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSelectSr" runat="server" 
                                    BehaviorID="AutoCompleteExSelectSr" CompletionInterval="100" 
                                    CompletionListCssClass="autocomplete_completionListElement" 
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                                    CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" 
                                    DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="3" 
                                    ServiceMethod="GetInvRemSrList" 
                                    ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx" 
                                    ShowOnlyCurrentWordInCompletionListItem="true" 
                                    TargetControlID="txtSelectSR">
                                </cc1:AutoCompleteExtender>
                                 </td>
                                 <td>
                                     <asp:Button ID="btnSelectSR" runat="server" Text="Select" onclick="btnSelectSr_Click" />
                                 </td>
                                     <td>
                                         <asp:Button ID="btnClearSR" runat="server" 
                                             onclick="btnClearSR_Click" Text="Clear" Visible="False" Width="60px" />
                                     </td>
                                 </tr>
                                     
                                     <tr>
                                         <td>&nbsp;</td>
                                         <td>&nbsp;</td>
                                         <td>&nbsp;</td>
                                         <td>&nbsp;</td>
                                     </tr>
                                     
                                 </table>
                                 <div id="Div2" style="width: 100%; height: 232px; overflow:auto;" runat="server">
                                     
                                             <asp:GridView ID="gvSrDet" runat="server" AutoGenerateColumns="False" 
                                                 BackColor="#003366" BorderStyle="None" 
                                                 CellPadding="4" Font-Names="Arial" Font-Size="8pt" ForeColor="#333333" 
                                                 onselectedindexchanged="gvSrDet_SelectedIndexChanged" 
                                                 onrowcommand="gvSrDet_RowCommand" Width="100%" 
                                                 onrowdatabound="gvSrDet_RowDataBound" CellSpacing="1">
                                                 <Columns>
                                                     <asp:BoundField DataField="Sr_Det_Lno" HeaderText="Line No" 
                                                     HeaderStyle-CssClass = "hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                                                         <HeaderStyle HorizontalAlign="Left" />
                                                         <ItemStyle CssClass="hideGridColumn" />
                                                     </asp:BoundField>
                                                     <asp:BoundField DataField="Sr_Det_Icode" HeaderText="Item Code" >
                                                         <HeaderStyle HorizontalAlign="Left" />
                                                     </asp:BoundField>
                                                     <asp:BoundField DataField="Sr_Det_Itm_Desc" HeaderText="Item Name" >
                                                         <HeaderStyle HorizontalAlign="Left" />
                                                     </asp:BoundField>
                                                     <asp:BoundField DataField="Sr_Det_Itm_Uom" HeaderText="UOM" >
                                                         <HeaderStyle HorizontalAlign="Left" />
                                                     </asp:BoundField>
                                                     <asp:BoundField DataField="Sr_Det_Bal_Qty" HeaderText="SR Qty." >                                                     
                                                         <HeaderStyle HorizontalAlign="Left" />
                                                     </asp:BoundField>
                                                     <asp:BoundField DataField="Sr_Det_Str_Code" HeaderText="Store" >                                                                                                          
                                                         <HeaderStyle HorizontalAlign="Left" />
                                                     </asp:BoundField>
                                                     <asp:TemplateField HeaderText="Issue Qty.">
                                                         <EditItemTemplate>
                                                             <asp:TextBox ID="TextBox0" CssClass="txtbox" runat="server" Text='<%# Bind("Sr_Det_Iss_Qty")%>'>
                                                             </asp:TextBox>
                                                         </EditItemTemplate>
                                                         <ItemTemplate>                                                             
                                                             <asp:TextBox ID="TextBox1" CssClass="txtbox" runat="server" 
                                                             Text='<%# Bind("Sr_Det_Iss_Qty")%>' Width="50px" Font-Names="Arial" 
                                                             Font-Size="8pt" ForeColor="#333333"></asp:TextBox>
                                                         </ItemTemplate>                                                         
                                                     </asp:TemplateField>
                                                     <asp:TemplateField>
                                                         <EditItemTemplate>
                                                             <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                                                 ControlToValidate="TextBox0" 
                                                                 ErrorMessage="The Issue quantity must be less than SR quantity." 
                                                                 ToolTip="The Issue quantity must be less than SR quantity."
                                                                 Operator="LessThanEqual" Type="Currency" ValidationGroup="grpSrQty" 
                                                                 ValueToCompare='<%# Bind("Sr_Det_Bal_Qty")%>'>*</asp:CompareValidator>
                                                         </EditItemTemplate>
                                                         <ItemTemplate>
                                                             <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                                                 ControlToValidate="TextBox1" 
                                                                 ErrorMessage="The Issue quantity must be less than SR quantity."
                                                                 ToolTip="The Issue quantity must be less than SR quantity."
                                                                 Operator="LessThanEqual" Type="Currency" ValidationGroup="grpSrQty" 
                                                                 ValueToCompare='<%# Bind("Sr_Det_Bal_Qty")%>'>*</asp:CompareValidator>
                                                         </ItemTemplate>
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Select Serial">
                                                         <EditItemTemplate>
                                                             <asp:TextBox ID="txtSerialNo" CssClass="txtbox" runat="server" Width="120px"></asp:TextBox>
                                                         </EditItemTemplate>
                                                         <ItemTemplate>
                                                             <asp:TextBox ID="txtSerialNo" CssClass="txtbox" runat="server" Width="120px" 
                                                             Font-Names="Arial" Font-Size="8pt" ForeColor="#333333"></asp:TextBox>
                                                             <%--<cc1:AutoCompleteExtender ID="AutoCompleteExtenderAddSerial" runat="server" 
                                                                BehaviorID="AutoCompleteExAddSerial" CompletionInterval="100" 
                                                                CompletionListCssClass="autocomplete_completionListElement" 
                                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                                                                CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" 
                                                                DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="3" 
                                                                ServiceMethod="GetInvSerialList" UseContextKey="true" 
                                                                ContextKey='<%#Eval("Sr_Det_Icode") + ":"+ Eval("Sr_Det_Str_Code") + ":" + Eval("Issue Qty.") %>'
                                                                ServicePath="~/ClientSide/Inventory/services/InvAutoComplete.asmx" 
                                                                ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="TextBox3">
                                                            </cc1:AutoCompleteExtender>--%>
                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtenderAddSerial" runat="server" 
                                                                BehaviorID="AutoCompleteExAddSerial" CompletionInterval="100" 
                                                                CompletionListCssClass="autocomplete_completionListElement" 
                                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                                                                CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" 
                                                                DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="3" 
                                                                ServiceMethod="GetInvSerialList" UseContextKey="true" 
                                                                ContextKey="Item"
                                                                ServicePath="~/ClientSide/Inventory/services/InvAutoComplete.asmx" 
                                                                ShowOnlyCurrentWordInCompletionListItem="true" 
                                                                 TargetControlID="txtSerialNo">
                                                            </cc1:AutoCompleteExtender>
                                                             &nbsp;
                                                         </ItemTemplate>
                                                     </asp:TemplateField>
                                                     <asp:TemplateField>
                                                         <ItemTemplate>
                                                             <asp:Button ID="btnAddSerial" CommandName="btnAddSerial" runat="server" 
                                                             CommandArgument="<%#((GridViewRow)Container).RowIndex %>" Height="18px" 
                                                                 Text="Add" Width="35px" onclick="btnAddSerial_Click" />                                                             
                                                         </ItemTemplate>
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Serial No">
                                                         <EditItemTemplate>
                                                             <asp:TextBox ID="TextBox2" CssClass="txtbox" runat="server"></asp:TextBox>
                                                         </EditItemTemplate>
                                                         <ItemTemplate>                                                             
                                                             <asp:TextBox ID="txtSerial" CssClass="txtbox" runat="server" TextMode="MultiLine" Width="200px" 
                                                                 Text='<%# Bind("Sr_Det_Serial_No")%>' Enabled="False" ReadOnly="True"
                                                                 ontextchanged="txtSerial_TextChanged" 
                                                                 Font-Names="Arial" Font-Size="8pt" ForeColor="#333333"></asp:TextBox>                                                                 
                                                         </ItemTemplate>
                                                     </asp:TemplateField>
                                                     <asp:CommandField ButtonType="Image" SelectImageUrl="~/Images/accept-icon.png" 
                                                         ShowSelectButton="True" ValidationGroup="grpSrQty" HeaderText="Accept" >                                                         
                                                         <ItemStyle HorizontalAlign="Center" />
                                                     </asp:CommandField>
                                                     <asp:TemplateField HeaderText="Add" Visible="False">
                                                         <ItemTemplate>
                                                             <asp:Button ID="btnAddSerialNo" runat="server" CausesValidation="false" 
                                                                 CommandName="btnAdd" Text="Add"  
                                                                 CommandArgument="<%#((GridViewRow) Container).RowIndex %>"/>
                                                         </ItemTemplate>
                                                     </asp:TemplateField>
                                                 </Columns>

                                             </asp:GridView>


                                 </div>
                         </asp:Panel>
                         <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderSelectSr" runat="server" 
                            TargetControlID="pnlSelectSrBody" 
                            CollapseControlID="pnlSelectSrHdr" 
                            ExpandControlID="pnlSelectSrHdr"
                            Collapsed="false" 
                            TextLabelID="lblSelectSr" 
                            CollapsedText="Select SR" 
                            ExpandedText="Select SR"
                            CollapsedSize="0"
                            ExpandedSize="265"
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
             <asp:Panel ID="pHeaderText" runat="server" CssClass="cpHeaderContent" Width="99%">
                <asp:Label ID="lblText" Text="Header" runat="server" />
             </asp:Panel>
             <asp:Panel ID="pHdrBody" runat="server" CssClass="cpBodyContent" Width="99%" 
                     Height="230px">
                <table style="height: 162px">
    <tr>
        <td>
            Required For:</td>
        <td>
            <asp:TextBox ID="txtReqFor" Width="300px" runat="server" Enabled="False" 
                ReadOnly="True"></asp:TextBox>                    
        </td>   
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                ControlToValidate="txtReqFor" runat="server" ErrorMessage="Enter Required For" 
                ValidationGroup="HdrGrp"></asp:RequiredFieldValidator>
        </td>                    
    </tr>
    <tr>
        <td>
            Location ID:</td>
        <td>
            <asp:TextBox ID="txtLocationId" runat="server" Width="300px" Enabled="False"></asp:TextBox>
            </td>
            <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                    ControlToValidate="txtLocationId" runat="server" 
                    ErrorMessage="Enter Location Id" ValidationGroup="HdrGrp"></asp:RequiredFieldValidator>
        </td>                    
    </tr>
                    <tr>
                        <td>
                            Issue Type:</td>
                        <td>
                            <asp:TextBox ID="txtIssueType" runat="server" Width="300px" Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                ControlToValidate="txtIssueType" ErrorMessage="Enter Issue Type" 
                                ValidationGroup="HdrGrp"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
    <tr>
        <td>
            Issue Date:
        </td>
        <td>
            <asp:TextBox ID="txtIssueDate" runat="server" Width="300px"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txtIssueDate" 
                runat="server" Format="dd/MM/yyyy"/>
            </td>
            <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" 
                    ControlToValidate="txtIssueDate" runat="server" 
                    ErrorMessage="Enter Issue Date" ValidationGroup="HdrGrp"></asp:RequiredFieldValidator>
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
                        <td>&nbsp;</td>
                        <td>
                            <asp:TextBox ID="txtTkiNo" runat="server" Enabled="False" Visible="False" Width="300px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
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
                Collapsed="true" 
                TextLabelID="lblText" 
                CollapsedText="Show Header" 
                ExpandedText="Hide Header"
                CollapsedSize="0"
                ExpandedSize="230"
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
             <asp:Panel ID="pDetailsText" runat="server" CssClass="cpHeaderContent" Width="99%">
                <asp:Label ID="lblDetText" Text="Details" runat="server" />
             </asp:Panel>
             <asp:Panel ID="pDetBody" runat="server" CssClass="cpBodyContent" Width="99%" >       
                     <%--<div id="Div1" style="width: 100%; height: 200px; overflow:auto;" 
                         runat="server">--%>
                     <asp:GridView ID="gvIssue" runat="server" BackColor="#003366" 
                                  BorderStyle="None" CellPadding="4" Font-Names="Arial" 
                                  Font-Size="8pt" ForeColor="#333333" 
                                  onselectedindexchanged="gvIssue_SelectedIndexChanged" 
                             AutoGenerateColumns="False" CellSpacing="1" Width="100%">
                                  <Columns>
                                      <asp:CommandField ButtonType="Image" 
                                          SelectImageUrl="~/Images/remove.png" SelectText="Remove" 
                                          ShowSelectButton="True">
                                          <ItemStyle ForeColor="Red" HorizontalAlign="Center" />
                                      </asp:CommandField>
                                      <asp:BoundField DataField="SR Line#" HeaderText="SR Line#" 
                                      HeaderStyle-CssClass = "hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                                          <HeaderStyle CssClass="hideGridColumn" />
                                          <ItemStyle CssClass="hideGridColumn" />
                                      </asp:BoundField>
                                      <asp:BoundField DataField="Item Code" HeaderText="Item Code" />
                                      <asp:BoundField DataField="Item Name" HeaderText="Item Name" />
                                      <asp:BoundField DataField="UOM" HeaderText="UOM" />
                                      <asp:BoundField DataField="Store" HeaderText="Store" />
                                      <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                      <asp:BoundField DataField="Rate" HeaderText="Rate">
                                        <ItemStyle HorizontalAlign="Right" Width="60px" />
                                      </asp:BoundField>
                                      <asp:BoundField DataField="Amount" HeaderText="Amount">
                                        <ItemStyle HorizontalAlign="Right" Width="60px" />
                                      </asp:BoundField>
                                      <asp:TemplateField HeaderText="Serial">
                                          <EditItemTemplate>
                                              <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Serial") %>'></asp:TextBox>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                          <div style="width: 300px; word-wrap: break-word;">
                                              <asp:Label ID="Label1" runat="server" Text='<%# Bind("Serial") %>'></asp:Label>                                            
                                          </div>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:BoundField DataField="Rate ID" HeaderText="Rate ID" />
                                      <asp:BoundField DataField="Rate Line No" HeaderText="Rate Line No" />                                      
                                      <asp:BoundField DataField="MRR No" HeaderText="MRR No" />                                      
                                  </Columns>
                              </asp:GridView>
                    <%-- </div>   --%>                          
                  <br />
                  <span>
                  <asp:Button ID="btnHold" runat="server" onclick="btnHold_Click" Text="Hold" 
                     Visible="False" Width="60px" />                                  
                     &nbsp;&nbsp;&nbsp;
                     <asp:Button ID="btnPost" runat="server" onclick="btnPost_Click" Text="Post" 
                                  Visible="False" Width="100px" />
                     </span>
                  <br />
                     
                    
                  
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
                ExpandedSize="245"
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
                         Issue Ref No</td>
                     <td>
                         :</td>
                     <td>
                         <asp:TextBox ID="txtIssueRef" ReadOnly="true" Enabled="false" runat="server"  BorderStyle="None"></asp:TextBox></td>
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
                            
                         
                         </td>      
                                   
         <td>
            &nbsp;
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
</asp:Content>