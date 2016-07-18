<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerAccount.aspx.cs"  MasterPageFile="~/masMain.master"  Inherits="modules_FixedAsset_Setup_CustomerAccount" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >
     <asp:UpdatePanel ID= "upd" runat="server">
    <ContentTemplate>
        <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
        <asp:Label ID="lblleave" Text="CUSTOMER ACCOUNT ENTRY" runat="server" />
    </asp:Panel>

    <div style="width: 1182px">
    
        <table class="style1" width="100%" style="text-align:left">
            <tr>
                <td colspan="10" style="height: 52px">
                    <asp:Label ID="lblNotification" runat="server"></asp:Label>
                </td>
            </tr>
            <%--<tr>
                <td style="width: 231px">
                    &nbsp;</td>
                <td class="tblbig" colspan="10">
                    <asp:Label ID="Label14" runat="server" Text="CUSTOMER ACCOUNT ENTRY"></asp:Label>
                </td>
            </tr>--%>
            <tr>
                <td class="style5" style="width: 105px">
                    <asp:Label ID="Label40" runat="server" Text="Search"></asp:Label>
                </td>
                <td class="style7">
                    :</td>
                <td class="style4">
                    <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="True" OnTextChanged="txtSearch_TextChanged" Width="400px"></asp:TextBox>
                    <cc2:AutoCompleteExtender ID="txtSearch_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCustomerAccountInformation" ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx" TargetControlID="txtSearch">
                    </cc2:AutoCompleteExtender>
                </td>
                <td class="style3">
                    &nbsp;</td>
                <td class="style8">
                    &nbsp;</td>
                <td class="style2">
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
            <tr>
                <td  class="style5" style="width: 105px">
                    <asp:Label ID="Label1" runat="server" Text="Code"></asp:Label>
                </td>
                <td class="style7">
                    :</td>
                <td >
                    <asp:TextBox ID="txtcode" runat="server" Width="400px" AutoPostBack="True" 
                        ontextchanged="txtcode_TextChanged1" ReadOnly="True"></asp:TextBox>
                      <cc1:AutoCompleteExtender ID="autoComplete1" runat="server" 
                            BehaviorID="AutoCompleteEx2" CompletionInterval="100" 
                            CompletionListCssClass="autocomplete_completionListElement" 
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                            CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" 
                            DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="2" 
                            ServiceMethod="GetCustomerAccountCode" 
                            ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx" 
                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtcode">
                        </cc1:AutoCompleteExtender>
                        
                </td>
                <td class="style3" >
                    <asp:Label ID="Label2" runat="server" Text="Releted To"></asp:Label>
                </td>
                <td class="style8">
                    :</td>
                <td class="style2">
                    <asp:TextBox ID="txtreletedto" runat="server" Width="400px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
                <td colspan="3" rowspan="16">
                    &nbsp;</td>
            </tr>
            <tr>
                <td  class="style5" style="width: 105px">
                    <asp:Label ID="Label3" runat="server" Text="Name"></asp:Label>
                </td>
                <td class="style7">
                    :</td>
                <td class="style4">
                    <asp:TextBox ID="txtname" runat="server" Width="400px"></asp:TextBox>
                </td>
                <td class="style3" >
                    <asp:Label ID="Label7" runat="server" Text="Permision(0-99)"></asp:Label>
                </td>
                <td class="style8">
                    :</td>
                <td class="style2">
                    <asp:TextBox ID="txtpermission" runat="server" Width="400px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td  class="style5" style="width: 105px">
                    <asp:Label ID="Label4" runat="server" Text="Secondary Code"></asp:Label>
                </td>
                <td class="style7">
                    :</td>
                <td class="style4">
                    <asp:TextBox ID="txtsecondarycode" runat="server" Width="400px"></asp:TextBox>
                </td>
                <td class="style3" >
                    <asp:Label ID="Label8" runat="server" Text="Account Balance"></asp:Label>
                </td>
                <td class="style8">
                    :</td>
                <td class="style2" colspan="2">
                    <asp:TextBox ID="txtaccountbalance" runat="server" Width="400px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td  class="style5" style="width: 105px">
                    <asp:Label ID="Label5" runat="server" Text="Status"></asp:Label>
                </td>
                <td class="style7">
                    :</td>
                <td class="style4">
                    <asp:CheckBox ID="chkStatus" runat="server" Text="Open Account" 
                        Enabled="False" />
                </td>
                <td class="style3" >
                    <asp:Label ID="Label9" runat="server" Text="Last Transaction" Width="110px"></asp:Label>
                </td>
                <td class="style8">
                    :</td>
                <td class="style2">
                    <asp:TextBox ID="txtlasttranaction" runat="server" Width="400px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td  class="style5" style="width: 105px">
                    <asp:Label ID="Label6" runat="server" Text="Comment"></asp:Label>
                </td>
                <td class="style7">
                    :</td>
                <td class="style4">
                    <asp:TextBox ID="txtcomment" runat="server" Width="400px"></asp:TextBox>
                </td>
                <td class="style3" >
                    <asp:Label ID="Label10" runat="server" Text="Currency Code"></asp:Label>
                </td>
                <td class="style8">
                    :</td>
                <td class="style2">
                    <asp:TextBox ID="txtcurrencycode" runat="server" Width="400px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td  class="style5" style="width: 105px">
                    <asp:Label ID="Label11" runat="server" style="margin-bottom: 0px" 
                        Text="Balance During TE Required" Width="156px"></asp:Label>
                </td>
                <td class="style7">
                    :</td>
                <td class="style4">
                    <asp:CheckBox ID="chkBalanceDuringRequired" runat="server" Text="YES" 
                        oncheckedchanged="chkBalanceDuringRequired_CheckedChanged" />
                </td>
                <td class="style3" >
                    <asp:Label ID="Label13" runat="server" Text="Check Clearing Account"></asp:Label>
                </td>
                <td class="style8">
                    :</td>
                <td class="style2">
                    <asp:TextBox ID="txtcheckClearingAccount" runat="server" Width="400px" 
                        AutoPostBack="True" ontextchanged="txtcheckClearingAccount_TextChanged"></asp:TextBox>
                    <cc1:AutoCompleteExtender
                    ID="AutoCompleteExtender2"
                    runat="server"
                    ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                    CompletionListItemCssClass="autocomplete_listItem"
                    ServiceMethod="GetCoaAccountCode"
                    TargetControlID="txtcheckClearingAccount"
                    MinimumPrefixLength="2"
                    CompletionSetCount="12">
                </cc1:AutoCompleteExtender>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style5" style="width: 105px">
                    <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Size="10pt" 
                        Text="Account Group"></asp:Label>
                </td>
                <td class="style7">
                    &nbsp;</td>
                <td class="style4">
                    <asp:CheckBox ID="chkaccountgroup" runat="server" AutoPostBack="True" 
                        Checked="True" oncheckedchanged="chkaccountgroup_CheckedChanged" Text="YES" />
                </td>
                <td class="style3">
                    <asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Size="10pt" 
                        Text="Analysis Required" Width="137px" Visible="False"></asp:Label>
                </td>
                <td class="style8">
                    &nbsp;</td>
                <td class="style2">
                    <asp:CheckBox ID="chkanalysis" runat="server" AutoPostBack="True" 
                        oncheckedchanged="chkanalysis_CheckedChanged" Text="YES" Visible="False" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style5" colspan="3" rowspan="6">
                    <asp:Panel ID="pnlAccountGroup" runat="server" Height="140px">
                        <table style="width: 99%">
                            <tr>
                                <td class="tblbig" colspan="2">
                                    <asp:Label ID="Label16" runat="server" Text="Group Name"></asp:Label>
                                </td>
                                <td class="tblbig">
                                    <asp:Label ID="Label17" runat="server" Font-Bold="True" Text="Group Code"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td  style="width: 156px">
                                    <asp:Label ID="Label18" runat="server" Text="First Group"></asp:Label>
                                </td>
                                <td style="width: 8px">
                                    :</td>
                                <td>
                                    <asp:TextBox ID="txtfirstgroup" runat="server" Width="400px">AR::Accounts Receivable</asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
                            BehaviorID="AutoCompleteEx11" CompletionInterval="100" 
                            CompletionListCssClass="autocomplete_completionListElement" 
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                            CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" 
                            DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="2" 
                            ServiceMethod="GetCoaAccountGroupCode1" 
                            ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx" 
                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtfirstgroup">
                        </cc1:AutoCompleteExtender>
                                </td>
                            </tr>
                            <tr>
                                <td  style="width: 156px">
                                    <asp:Label ID="Label19" runat="server" Text="Second Group"></asp:Label>
                                </td>
                                <td style="width: 8px">
                                    :</td>
                                <td>
                                    <asp:TextBox ID="txtsecondgroup" runat="server" Width="400px">AR::Accounts Receivable</asp:TextBox>
                                     <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" 
                            BehaviorID="AutoCompleteEx12" CompletionInterval="100" 
                            CompletionListCssClass="autocomplete_completionListElement" 
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                            CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" 
                            DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="2" 
                            ServiceMethod="GetCoaAccountGroupCode2" 
                            ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx" 
                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtsecondgroup">
                        </cc1:AutoCompleteExtender>
                                </td>
                            </tr>
                            <tr>
                                <td  style="width: 156px">
                                    <asp:Label ID="Label20" runat="server" Text="Third Group"></asp:Label>
                                </td>
                                <td style="width: 8px">
                                    :</td>
                                <td>
                                    <asp:TextBox ID="txtthirdgroup" runat="server" Width="400px">ISP::ISP</asp:TextBox>
                                     <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" 
                            BehaviorID="AutoCompleteEx13" CompletionInterval="100" 
                            CompletionListCssClass="autocomplete_completionListElement" 
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                            CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" 
                            DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="2" 
                            ServiceMethod="GetCoaAccountGroupCode3" 
                            ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx" 
                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtthirdgroup">
                        </cc1:AutoCompleteExtender>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td class="style3" colspan="3" rowspan="6">
                    <asp:Panel ID="pnlAnalysis" runat="server" Height="140px" Visible="False">
                        <table style="width: 100%">
                            <tr>
                                <td class="tblbig" colspan="3">
                                    <asp:Label ID="Label22" runat="server" Font-Bold="True" Text="ANALYSIS"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 63px" >
                                    <asp:Label ID="Label24" runat="server" Text="Level 1"></asp:Label>
                                </td>
                                <td>
                                    :</td>
                                <td>
                                    <asp:TextBox ID="txtlevel1" runat="server" Width="400px"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" 
                            BehaviorID="AutoCompleteEx14" CompletionInterval="100" 
                            CompletionListCssClass="autocomplete_completionListElement" 
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                            CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" 
                            DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="2" 
                            ServiceMethod="GetAnalysisAccountCode" 
                            ServicePath="~/ClientSide/modules/Accounts/services/AccountsService.asmx" 
                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtlevel1">
                        </cc1:AutoCompleteExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 63px" >
                                    <asp:Label ID="Label25" runat="server" Text="Level 2"></asp:Label>
                                </td>
                                <td>
                                    :</td>
                                <td>
                                    <asp:TextBox ID="txtlevel2" runat="server" Width="400px"></asp:TextBox>
                                     <cc1:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server" 
                            BehaviorID="AutoCompleteEx15" CompletionInterval="100" 
                            CompletionListCssClass="autocomplete_completionListElement" 
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                            CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" 
                            DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="2" 
                            ServiceMethod="GetAnalysisAccountCode" 
                            ServicePath="~/ClientSide/modules/Accounts/services/AccountsService.asmx" 
                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtlevel2">
                        </cc1:AutoCompleteExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 63px" >
                                    <asp:Label ID="Label26" runat="server" Text="Level 3"></asp:Label>
                                </td>
                                <td>
                                    :</td>
                                <td>
                                    <asp:TextBox ID="txtlevel3" runat="server" Width="400px"></asp:TextBox>
                                     <cc1:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server" 
                            BehaviorID="AutoCompleteEx16" CompletionInterval="100" 
                            CompletionListCssClass="autocomplete_completionListElement" 
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                            CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" 
                            DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="2" 
                            ServiceMethod="GetAnalysisAccountCode" 
                            ServicePath="~/ClientSide/modules/Accounts/services/AccountsService.asmx" 
                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtlevel3">
                        </cc1:AutoCompleteExtender>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td style="height: 16px">
                    </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="height: 13px">
                    </td>
            </tr>
            
            <tr>
                <td class="style5" colspan="7" align="left">
                    <table style="width: 100%; text-align:left">
                        <tr>
                            <td style="width: 146px">
                                <asp:Label ID="Label27" runat="server" Text="Address Line 1"></asp:Label>
                            </td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtAddressLine1" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 146px">
                                <asp:Label ID="Label28" runat="server" Text="Address Line 2"></asp:Label>
                            </td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtAddressLine2" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 146px">
                                <asp:Label ID="Label29" runat="server" Text="Address Line 3"></asp:Label>
                            </td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtAddressLine3" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 146px">
                                <asp:Label ID="Label30" runat="server" Text="City"></asp:Label>
                            </td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtCity" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 146px">
                                <asp:Label ID="Label31" runat="server" Text="Zone"></asp:Label>
                            </td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtZone" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                       <%-- <tr>
                            <td>
                                <asp:Label ID="Label32" runat="server" Text="Customer Status"></asp:Label>
                            </td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtCustomerStatus" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>--%>
                        <tr>
                            <td style="width: 146px">
                                <asp:Label ID="Label33" runat="server" Text="Fax"></asp:Label>
                            </td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtFax" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 146px">
                                <asp:Label ID="Label34" runat="server" Text="Email"></asp:Label>
                            </td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtEmail" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 146px">
                                <asp:Label ID="Label35" runat="server" Text="Telephone"></asp:Label>
                            </td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtTelephone" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 146px">
                                <asp:Label ID="Label36" runat="server" Text="Mobile Number"></asp:Label>
                            </td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtMobileNumber" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 146px">
                                <asp:Label ID="Label37" runat="server" Text="VAT Reg. No"></asp:Label>
                            </td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtVATRegNo" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 146px">
                                <asp:Label ID="Label38" runat="server" Text="TIN"></asp:Label>
                            </td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtTIN" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 146px">
                                <asp:Label ID="Label39" runat="server" Text="Contact Person"></asp:Label>
                            </td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtContactPerson" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 146px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 146px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:Button ID="btnAdd" runat="server" onclick="btnAdd_Click" Text="Add" Width="100px" />
                                <asp:Button ID="btnExit" runat="server" onclick="btnExit_Click" Text="Exit" Width="100px" />
                                <asp:Button ID="btnReport" runat="server" Text="Report" Visible="False" Width="100px" />
                                <asp:Button ID="btnDelete" runat="server" onclick="btnDelete_Click" Text="Delete" Visible="False" Width="60px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style5" style="width: 105px">
                    &nbsp;</td>
                <td class="style7">
                    &nbsp;</td>
                <td class="style4">
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td class="style8">
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>
    
    </div>
     </ContentTemplate> 
 
      <Triggers>  
     
       </Triggers>
       
      </asp:UpdatePanel> 
</asp:Content>