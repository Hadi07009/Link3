<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmInvoiceReceiptLocal.aspx.cs" Inherits="frmInvoiceReceiptLocal"  EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="http://code.jquery.com/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../../../script/jquery.MultiFile.js" type="text/javascript"></script>

    <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
        <asp:Label ID="lblleave" Text="Invoice Receipt" runat="server" />
    </asp:Panel>   
    <table style="width:100%; text-align:left">
                
                 <tr>
                     <td>
                         &nbsp;</td>
                     <td>
                         <%-- </asp:Panel>--%>
                         <div align="center">
                             <%--<cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderHdr" runat="server" 
                            TargetControlID="pHdrBody" 
                            CollapseControlID="pHeaderText" 
                            ExpandControlID="pHeaderText"
                            Collapsed="false" 
                            TextLabelID="lblText" 
                            CollapsedText="Show Header" 
                            ExpandedText="Hide Header"
                            CollapsedSize="0"
                            ExpandedSize="370"
                            AutoCollapse="False"
                            AutoExpand="False"
                            ScrollContents="false"
                            ImageControlID="Image1"
                            ExpandedImage="~/images/collapse.jpg"
                            CollapsedImage="~/images/expand.jpg"
                            ExpandDirection="Vertical" 
                            >
                            </cc1:CollapsiblePanelExtender>--%>
                                          <asp:Label ID="lblerrormsg" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                         </div>
             <div align="center">
                         <%-- <br />--%><%--  <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderDet" runat="server" 
                            TargetControlID="pDetBody" 
                            CollapseControlID="pDetailsText" 
                            ExpandControlID="pDetailsText"
                            Collapsed="true" 
                            TextLabelID="lblDetText" 
                            CollapsedText="Show Details" 
                            ExpandedText="Hide Details"
                            CollapsedSize="0"
                            ExpandedSize="300"
                           
                            AutoCollapse="False"
                            AutoExpand="False"
                            ScrollContents="false"
                            ImageControlID="Image1"
                            ExpandedImage="~/images/collapse.jpg"
                            CollapsedImage="~/images/expand.jpg"
                            ExpandDirection="Vertical" 
                            >
                            </cc1:CollapsiblePanelExtender>--%>
                <table width="100%">
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Supplier"></asp:Label>
                        &nbsp;:<asp:DropDownList ID="cboSupplier" runat="server" AutoPostBack="True" onselectedindexchanged="cboSupplier_SelectedIndexChanged" Width="405px">
                            <asp:ListItem Text="------Select-------" Value="------Select-------"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td >&nbsp;</td>
                    <td>
                        &nbsp;</td>   
                    <td>
                        &nbsp;</td>
                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:GridView ID="gvMrrData" runat="server" BackColor="White" BorderColor="#32A545" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" Font-Names="Arial" Font-Size="10pt" ForeColor="#333333" HeaderStyle-HorizontalAlign="Left" Width="100%" OnSelectedIndexChanged="gvMrrData_SelectedIndexChanged">
                                            <Columns>
                                                <asp:CommandField ButtonType="Image" SelectImageUrl="~/Images/accept-icon.png" SelectText="Remove" ShowSelectButton="True">
                                                <ItemStyle ForeColor="Red" HorizontalAlign="Left" />
                                                </asp:CommandField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style15"></td>
                                    <td class="auto-style15"></td>
                                    <td align="left" class="auto-style18"></td>
                                    <td class="auto-style15"></td>
                                </tr>
            </table>  
                         <%-- <br />--%><%--  <br />--%>
                         </div>  
                         <%-- </ContentTemplate>
                         <Triggers>
                        <%-- <asp:AsyncPostBackTrigger ControlID="txtQuantity" EventName="TextChanged"/>--%>
               <div align="center">
                         <asp:Panel ID="pDetailsHeader" runat="server" CssClass="cpHeaderContent" Width="100%">
                            <asp:Label ID="lblDetText" Text="View Receipt Details Information" runat="server" />
                         </asp:Panel>
                         <asp:Panel ID="pDetBody" runat="server" CssClass="cpBodyContent" Width="99%">
                              <table width="100%">
                <tr>
                    <td class="auto-style19" colspan="4">
                        <asp:GridView ID="gridMrr" runat="server" BackColor="White" BorderColor="#32A545" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" Font-Names="Arial" Font-Size="10pt" ForeColor="#333333" HeaderStyle-HorizontalAlign="Left"  Width="100%" OnRowDataBound="gridMrr_RowDataBound"  ShowFooter="true">
                                                       
                        </asp:GridView>
                        </td>
                </tr>
                                  <tr>
                                      <td class="auto-style19"></td>
                                      <td class="auto-style20"></td>
                                      <td class="auto-style20">
                                          &nbsp;</td>
                                      <td class="auto-style22">&nbsp;</td>
                                  </tr>
                                  <tr>
                                      <td class="auto-style19">
                                          <asp:Label ID="Label19" runat="server" Text="PO Number"></asp:Label>
                                      </td>
                                      <td class="auto-style20">:</td>
                                      <td class="auto-style20">
                                          <asp:Label ID="lblPO" runat="server"></asp:Label>
                                      </td>
                                      <td class="auto-style22">&nbsp;</td>
                                  </tr>
                                  <tr>
                                      <td class="auto-style19">
                                          <asp:Label ID="Label20" runat="server" Text="MRR Number"></asp:Label>
                                      </td>
                                      <td class="auto-style20">:</td>
                                      <td class="auto-style20">
                                          <asp:Label ID="lblMRR" runat="server"></asp:Label>
                                      </td>
                                      <td class="auto-style22">&nbsp;</td>
                                  </tr>
                                  <tr>
                                      <td class="auto-style21">
                                          <asp:Label ID="Label12" runat="server" Text="VAT Account Code"></asp:Label>
                                      </td>
                                      <td >
                                          :
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txtVatAccount" runat="server" AutoPostBack="false"  Width="400px"></asp:TextBox>
                                          <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
                                              CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                                              CompletionListItemCssClass="autocomplete_listItem" 
                                              CompletionSetCount="12" 
                                              MinimumPrefixLength="1" 
                                              ServiceMethod="GetCoaAccountCode" 
                                              ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx" 
                                              TargetControlID="txtVatAccount">
                                          </cc1:AutoCompleteExtender>
                                      </td>
                                      <td class="auto-style23">&nbsp;</td>
                                  </tr>
                                  <tr>
                                      <td class="auto-style21">
                                          <asp:Label ID="Label16" runat="server" Text="VAT Amount"></asp:Label>
                                      </td>
                                      <td >
                                          :
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txtVatAmount" runat="server" Width="400px" autocomplete="off"></asp:TextBox>
                                      </td>
                                      <td class="auto-style23">&nbsp;</td>
                                  </tr>
                                  <tr>
                                      <td class="auto-style21">
                                          <asp:Label ID="Label17" runat="server" Text="Tax Account Code"></asp:Label>
                                      </td>
                                      <td>:</td>
                                      <td>
                                          <asp:TextBox ID="txtTaxAccount" runat="server" AutoPostBack="false" Width="400px"></asp:TextBox>
                                          <cc1:AutoCompleteExtender ID="txtTaxAccount_AutoCompleteExtender" runat="server" 
                                              CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                                              CompletionListItemCssClass="autocomplete_listItem" 
                                              CompletionSetCount="12" 
                                              MinimumPrefixLength="1" 
                                              ServiceMethod="GetCoaAccountCode" 
                                              ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx" 
                                              TargetControlID="txtTaxAccount">
                                          </cc1:AutoCompleteExtender>
                                      </td>
                                      <td class="auto-style23">&nbsp;</td>
                                  </tr>
                                  <tr>
                                      <td class="auto-style21">
                                          <asp:Label ID="Label18" runat="server" Text="Tax Amount"></asp:Label>
                                      </td>
                                      <td>:</td>
                                      <td>
                                          <asp:TextBox ID="txtTaxAmount" runat="server"  Width="400px" autocomplete="off"></asp:TextBox>
                                      </td>
                                      <td class="auto-style23">&nbsp;</td>
                                  </tr>
                                  <tr>
                                      <td class="auto-style21">
                                          <asp:Label ID="Label21" runat="server" Text="Invoice Number"></asp:Label>
                                      </td>
                                      <td>:</td>
                                      <td>
                                          <asp:TextBox ID="txtInvoiceNumber" runat="server" Width="400px" autocomplete="off"></asp:TextBox>
                                      </td>
                                      <td class="auto-style23">&nbsp;</td>
                                  </tr>
                                  <tr>
                                      <td class="auto-style21">
                                          <asp:Label ID="Label23" runat="server" Text="Received Date"></asp:Label>
                                      </td>
                                      <td>:</td>
                                      <td>
                                          <asp:TextBox ID="txtReceiptDate" runat="server" Width="400px" autocomplete="off"></asp:TextBox>
                                          <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtReceiptDate" />
                                      </td>
                                      <td class="auto-style23">&nbsp;</td>
                                  </tr>
                                  <tr>
                                      <td class="auto-style21">
                                          <asp:Label ID="Label22" runat="server" Text="Attachment"></asp:Label>
                                      </td>
                                      <td>:</td>
                                      <td>
                                          <asp:FileUpload ID="file_upload" runat="server" class="multi" />
                                      </td>
                                      <td class="auto-style23">&nbsp;</td>
                                  </tr>
                                  <tr>
                                      <td class="auto-style21">&nbsp;</td>
                                      <td>&nbsp;</td>
                                      <td>&nbsp;</td>
                                      <td class="auto-style23">&nbsp;</td>
                                  </tr>
                                  <tr>
                                      <td class="auto-style21">&nbsp;</td>
                                      <td>&nbsp;</td>
                                      <td>
                                          <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" Width="100px" />
                                      </td>
                                      <td class="auto-style23">&nbsp;</td>
                                  </tr>
                                  <tr>
                                      <td class="auto-style21">&nbsp;</td>
                                      <td>&nbsp;</td>
                                      <td>&nbsp;</td>
                                      <td class="auto-style23">&nbsp;</td>
                                  </tr>
                                  <tr>
                                      <td class="auto-style21">&nbsp;</td>
                                      <td>&nbsp;</td>
                                      <td>
                                          <asp:GridView ID="gdvFileLoad" runat="server" AutoGenerateColumns="false" BorderColor="LightBlue" BorderWidth="0px" onrowdatabound="gdvFileLoad_RowDataBound" onselectedindexchanged="gdvFileLoad_SelectedIndexChanged">
                                              <Columns>
                                                  <asp:BoundField DataField="ReferenceNo" HeaderText="Reference" ItemStyle-Width="150px">
                                                  <ItemStyle Width="150px" />
                                                  </asp:BoundField>
                                                  <asp:BoundField DataField="FileName" HeaderStyle-HorizontalAlign="Center" HeaderText="List of Attachment">
                                                  <ItemStyle Width="450px" />
                                                  </asp:BoundField>
                                                  <asp:BoundField DataField="SavedFileName" HeaderText="SavedFileName" ItemStyle-Width="150px">
                                                  <ItemStyle Width="150px" />
                                                  </asp:BoundField>
                                                  <asp:TemplateField HeaderText="">
                                                      <ItemTemplate>
                                                          <asp:Button ID="btndownload" runat="server" Enabled="true" Text="Download" />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                              </Columns>
                                          </asp:GridView>
                                      </td>
                                      <td class="auto-style23">&nbsp;</td>
                                  </tr>
                                  <tr>
                                      <td class="auto-style24" >
                                          </td>
                                      <td class="auto-style25" >
                                          </td>
                                      <td align="left" class="auto-style25">
                                          &nbsp;</td>
                                      <td class="auto-style26"></td>
                                  </tr>
                                  <tr>
                                      <td class="auto-style24">&nbsp;</td>
                                      <td class="auto-style25">&nbsp;</td>
                                      <td align="left" class="auto-style25">
                                          <asp:Button ID="btnPOview" runat="server" OnClick="Button1_Click" Text="PO View" Width="100px" />
                                      </td>
                                      <td class="auto-style26">&nbsp;</td>
                                  </tr>
                                  <tr>
                                      <td class="auto-style24">&nbsp;</td>
                                      <td class="auto-style25">&nbsp;</td>
                                      <td align="left" class="auto-style25">
                                          <asp:Button ID="btnComments" runat="server" OnClick="btnComments_Click" Text="Comments" Width="100px" />
                                          &nbsp;<asp:Button ID="btnVatTaxUpdate" runat="server" OnClick="btnVatTaxUpdate_Click" Text="VAT &amp; TAX Modify" Width="120px" />
                                      </td>
                                      <td class="auto-style26">&nbsp;</td>
                                  </tr>
                                  <tr>
                                      <td class="auto-style24">
                                          <asp:Label ID="Label24" runat="server" Text="Remarks"></asp:Label>
                                      </td>
                                      <td class="auto-style25">:</td>
                                      <td align="left" class="auto-style25">
                                          <asp:TextBox ID="txtRemarks" runat="server" Font-Size="Medium" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                      </td>
                                      <td class="auto-style26">&nbsp;</td>
                                  </tr>
                                  <tr>
                                      <td class="auto-style24">&nbsp;</td>
                                      <td class="auto-style25">&nbsp;</td>
                                      <td align="left" class="auto-style25">
                                          <asp:TextBox ID="txtRemarksAll" runat="server" Font-Size="Medium" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                      </td>
                                      <td class="auto-style26">&nbsp;</td>
                                  </tr>
                                  <tr>
                                      <td class="auto-style24">&nbsp;</td>
                                      <td class="auto-style25">&nbsp;</td>
                                      <td align="left" class="auto-style25">&nbsp;</td>
                                      <td class="auto-style26">&nbsp;</td>
                                  </tr>
                                  <tr>
                                      <td class="auto-style24">&nbsp;</td>
                                      <td class="auto-style25">&nbsp;</td>
                                      <td align="left" class="auto-style25">&nbsp;</td>
                                      <td class="auto-style26">&nbsp;</td>
                                  </tr>
            </table>   
                         </asp:Panel>
                         <%-- </Triggers>                                      
                         </asp:UpdatePanel>--%>
                         </div>
                         <%-- </Triggers>                                      
                         </asp:UpdatePanel>--%><%-- </Triggers>                                      
                         </asp:UpdatePanel>--%>
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
                         Store Requisition No</td>
                     <td>
                         :</td>
                     <td>
                         <asp:TextBox ID="txtSrRef" runat="server" ReadOnly="true" Enabled="false"  BorderStyle="None"></asp:TextBox></td>
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
                         <%-- </Triggers>                                      
                         </asp:UpdatePanel>--%>
                         
                        <%-- </Triggers>                                      
                         </asp:UpdatePanel>--%>
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
                 <tr>
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
                 </tr>
             </table>
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
            padding-top: 2px;
            overflow : hidden;
            padding-left: 4px;
            padding-right: 4px;
            padding-bottom: 4px;
        }
        .style1
        {
            width: 71px;
        }
        .style2
        {
            width: 276px;
        }
        .style3
        {
            width: 125px;
        }
        .auto-style15 {            height: 25px;
        }
        .auto-style18 {
            width: 413px;
            text-align: left;
            height: 25px;
        }
        .auto-style19 {
            height: 23px;
        }
        .auto-style20 {
            height: 23px;
        }
        .auto-style21 {
            width: 328px;
        }
        .auto-style22 {
            height: 23px;
            width: 600px;
        }
        .auto-style23 {
            width: 600px;
        }
        .auto-style24 {
            width: 328px;
            height: 22px;
        }
        .auto-style25 {
            height: 22px;
        }
        .auto-style26 {
            width: 600px;
            height: 22px;
        }
    </style>                              
     </asp:Content>