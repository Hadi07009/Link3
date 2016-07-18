<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_pf_settlement.aspx.cs"  Inherits="frm_pf_settlement" EnableEventValidation="false" ValidateRequest="false"  %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<%@ Register assembly="TimePicker" namespace="MKB.TimePicker" tagprefix="MKB" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
   <cc1:MessageBox ID="MessageBox1" runat="server" />
   <cc1:ConfirmBox ID="ConfirmBox1" runat="server" />  
    
  
    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>

       
            <table style="text-align: center; width: 100%; border-left-width: 1px; border-bottom-width: 1px; border-right-width: 1px; vertical-align: middle">

                <tr>
                    <td class="cpHeaderContent">PF&nbsp; SETTLEMENT ENTRY</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblmessage" runat="server" Style="font-size: small"></asp:Label>
                    </td>
                </tr>


                <tr>
                    <td style="text-align: left;">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 144px">&nbsp;</td>
                                <td style="width: 156px; text-align: right; font-weight: 700;">Employee</td>
                                <td style="width: 7px">:</td>
                                <td>
                                    <asp:TextBox ID="txtemployee" runat="server" Width="407px"></asp:TextBox>
                                    <ajaxToolkit:AutoCompleteExtender ID="txtserviceBill_AutoCompleteemployee" runat="server" BehaviorID="AutoCompleteEmployeeInfo" CompletionInterval="1000" CompletionListCssClass="autocomplete_completionListElement" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="3" ServiceMethod="GetEmployee" ServicePath="~/services/srvSystem.asmx" ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtemployee">
                                    </ajaxToolkit:AutoCompleteExtender>
                                    &nbsp;<asp:Button ID="btshow" runat="server" Text="Show" OnClick="btshow_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 144px">&nbsp;</td>
                                <td style="width: 156px; text-align: right; font-weight: 700;">Settlement Date</td>
                                <td style="width: 7px">:</td>
                                <td>
                                    <ew:CalendarPopup ID="dtsettdate" runat="server" BackColor="#FFC20F" Culture="English (United Kingdom)" DisableTextBoxEntry="False"  Width="90px" AutoPostBack="True" OnDateChanged="dtsettdate_DateChanged">
                                        <ButtonStyle CssClass="btn2" />
                                    </ew:CalendarPopup>
                                </td>
                            </tr>
                        </table>


                            <tr>
                    <td style="text-align: left;">
                        <table id="tblempbasicinfo" runat="server" style="width: 75%; font-size: small;">
                            <tr>
                                <td style="color: #333333; text-align: center; background-color: #CCCCCC;" class="topcolor" colspan="8">Employee Basic Information</td>
                            </tr>
                            <tr>
                                <td style="width: 83px">&nbsp;</td>
                                <td style="width: 104px; text-align: right;">Employee ID</td>
                                <td style="width: 7px">:</td>
                                <td style="width: 149px">
                                    <asp:Label ID="lblempid" runat="server" Style="font-size: 14px" CssClass="strong"></asp:Label>
                                </td>
                                <td style="width: 165px; text-align: right;">Employee Name</td>
                                <td style="width: 7px">:</td>
                                <td style="width: 224px">
                                    <asp:Label ID="lblempname" runat="server" Style="font-size: 14px" CssClass="strong"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="btnstatement" runat="server" OnClick="btnstatement_Click" Text="View Statement" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 83px">&nbsp;</td>
                                <td style="width: 104px; text-align: right;">Designation</td>
                                <td style="width: 7px">:</td>
                                <td style="width: 149px">
                                    <asp:Label ID="lbldesignation" runat="server" Style="font-size: 14px" CssClass="strong"></asp:Label>
                                </td>
                                <td style="width: 165px; text-align: right;">Department</td>
                                <td style="width: 7px">:</td>
                                <td style="width: 224px">
                                    <asp:Label ID="lbldepartment" runat="server" Style="font-size: 14px" CssClass="strong"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 83px">&nbsp;</td>
                                <td style="width: 104px; text-align: right;">Joining Date</td>
                                <td style="width: 7px">:</td>
                                <td style="width: 149px">
                                    <asp:Label ID="lbljoindate" runat="server" Style="font-size: 14px" CssClass="strong"></asp:Label>
                                </td>
                                <td style="width: 165px; text-align: right;">Job Length(Month)</td>
                                <td style="width: 7px">:</td>
                                <td style="width: 224px">
                                    <asp:Label ID="lbljoblength" runat="server" Style="font-size: 14px" CssClass="strong"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 83px">&nbsp;</td>
                                <td style="width: 104px">&nbsp;</td>
                                <td style="width: 7px">&nbsp;</td>
                                <td style="width: 149px">&nbsp;</td>
                                <td style="width: 165px">&nbsp;</td>
                                <td style="width: 7px">&nbsp;</td>
                                <td style="width: 224px">
                                    &nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>

                         <table id="tblpfcontribution" runat="server" style="width: 95%; font-size: small;">
                             <tr>
                                 <td style="color: #333333; text-align: center; background-color: #CCCCCC;" colspan="9" class="topcolor">Contribution</td>
                                 <td rowspan="9" style="width: 20%" valign="top">
                                     <table style="width: 100%; border: 2px solid; border-color:orange ">
                                         <tr>
                                             <td style="font-size: small; text-align: right; width: 117px;">Total Due</td>
                                             <td>:</td>
                                             <td>
                                                 <asp:Label ID="lbltotdue" runat="server" Style="font-size: small" Text="due" CssClass="strong"></asp:Label>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td style="font-size: small; text-align: right; width: 117px;">Total Adjustment</td>
                                             <td>:</td>
                                             <td>
                                                 <asp:Label ID="lbltotadj" runat="server" Style="font-size: small" Text="adjust" CssClass="strong"></asp:Label>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td style="font-size: small; text-align: right; width: 117px;">Total Payment</td>
                                             <td>:</td>
                                             <td>
                                                 <asp:Label ID="lbltotpay" runat="server" Style="font-size: small" Text="payment" CssClass="strong"></asp:Label>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td style="font-size: small; text-align: right; width: 117px;">Balance</td>
                                             <td>:</td>
                                             <td>
                                                 <asp:Label ID="lbltotbal" runat="server" Style="font-size: small" Text="Balance" CssClass="strong"></asp:Label>
                                             </td>
                                         </tr>
                                     </table>
                                 </td>
                             </tr>
                             <tr>
                                 <td style="width: 6%; height: 25px;"></td>
                                 <td style="width: 9%; height: 25px; font-size:x-small; font-weight: 700;">Contribution</td>
                                 <td style="width: 1%; height: 25px; font-weight: 700; font-size: x-small;">Pay Ratio</td>
                                 <td style="width: 12%; height: 25px; font-weight: 700; font-size: x-small;">Payable</td>
                                 <td style="width: 8%; height: 25px; font-weight: 700; font-size: x-small;">Paid</td>
                                 <td style="width: 8%; height: 25px; font-weight: 700; font-size: x-small;">Due</td>
                                 <td style="width: 9%; height: 25px; font-weight: 700; font-size: x-small;">Adjustment</td>
                                 <td style="height: 25px; width: 9%; font-weight: 700; font-size: x-small;">Actual Pay</td>
                                 <td style="height: 25px; font-weight: 700; width: 100px; font-size: x-small;">Balance</td>
                             </tr>
                            <tr>
                                <td style="width: 6%; text-align: center; font-weight: 700; height: 27px;">Own</td>
                                <td style="width: 9%; height: 27px;">
                                    <asp:Label ID="lblowncont" runat="server" Style="font-size: 14px"  ></asp:Label>
                                </td>
                                <td style="width: 1%; height: 27px;">
                                    <asp:Label ID="lblownpayratio" runat="server" Style="font-size: 14px"></asp:Label>
                                </td>
                                <td style="width: 12%; height: 27px;">
                                    <asp:Label ID="lblownpayable" runat="server" Style="font-size: 14px"></asp:Label>
                                </td>
                                <td style="width: 8%; height: 27px;">
                                    <asp:Label ID="lblownpaid" runat="server" Style="font-size: 14px"></asp:Label>
                                </td>
                                <td style="width: 8%; height: 27px;">
                                    <asp:Label ID="lblowndue" runat="server" Style="font-size: 14px"></asp:Label>
                                </td>
                                <td style="width: 9%; height: 27px;">
                                    <asp:TextBox ID="txtownadjustment" runat="server" Width="100px" AutoPostBack="True" OnTextChanged="txtownadjustment_TextChanged">0</asp:TextBox>

                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" 
                                runat="server" Enabled="True" FilterMode="ValidChars" 
                                FilterType="Custom, Numbers" TargetControlID="txtownadjustment" ValidChars=".">
                            </ajaxToolkit:FilteredTextBoxExtender>


                                </td>
                                <td style="width: 9%; height: 27px;">
                                    <asp:TextBox ID="txtownactualpay" runat="server" Width="100px" AutoPostBack="True" OnTextChanged="txtownactualpay_TextChanged">0</asp:TextBox>

                                       <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" 
                                runat="server" Enabled="True" FilterMode="ValidChars" 
                                FilterType="Custom, Numbers" TargetControlID="txtownactualpay" ValidChars=".">
                            </ajaxToolkit:FilteredTextBoxExtender>


                                     <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
                                runat="server" Enabled="True" FilterMode="ValidChars" 
                                FilterType="Custom, Numbers" TargetControlID="txtownactualpay" ValidChars=".">
                            </ajaxToolkit:FilteredTextBoxExtender>

                                </td>
                                <td style="height: 27px; width: 100px;">
                                    <asp:Label ID="lblownbalance" runat="server" Style="font-size: 14px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 6%; text-align: center; font-weight: 700;">Employer</td>
                                <td style="width: 9%">
                                    <asp:Label ID="lblempcont" runat="server" Style="font-size: 14px"></asp:Label>
                                </td>
                                <td style="width: 1%">
                                    <asp:Label ID="lblcompayratio" runat="server" Style="font-size: 14px"></asp:Label>
                                </td>
                                <td style="width: 12%">
                                    <asp:Label ID="lblcompayable" runat="server" Style="font-size: 14px"></asp:Label>
                                </td>
                                <td style="width: 8%">
                                    <asp:Label ID="lblcompaid" runat="server" Style="font-size: 14px"></asp:Label>
                                </td>
                                <td style="width: 8%">
                                    <asp:Label ID="lblcomdue" runat="server" Style="font-size: 14px"></asp:Label>
                                </td>
                                <td style="width: 9%">
                                    <asp:TextBox ID="txtcomnadjustment" runat="server" Width="100px" AutoPostBack="True" OnTextChanged="txtcomnadjustment_TextChanged">0</asp:TextBox>

                                       <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
                                runat="server" Enabled="True" FilterMode="ValidChars" 
                                FilterType="Custom, Numbers" TargetControlID="txtcomnadjustment" ValidChars=".">
                            </ajaxToolkit:FilteredTextBoxExtender>


                                </td>
                                <td style="width: 9%">
                                    <asp:TextBox ID="txtcomactualpay" runat="server" Width="100px" AutoPostBack="True" OnTextChanged="txtcomactualpay_TextChanged">0</asp:TextBox>

                                      <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" 
                                runat="server" Enabled="True" FilterMode="ValidChars" 
                                FilterType="Custom, Numbers" TargetControlID="txtcomactualpay" ValidChars=".">
                            </ajaxToolkit:FilteredTextBoxExtender>

                                </td>
                                <td style="width: 100px">
                                    <asp:Label ID="lblcombalance" runat="server" Style="font-size: 14px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 6%">&nbsp;</td>
                                <td style="width: 9%">&nbsp;</td>
                                <td style="width: 1%">&nbsp;</td>
                                <td style="width: 12%">&nbsp;</td>
                                <td style="width: 8%">&nbsp;</td>
                                <td style="width: 8%">&nbsp;</td>
                                <td style="width: 9%">&nbsp;</td>
                                <td style="width: 9%">&nbsp;</td>
                                <td style="width: 100px">&nbsp;</td>
                            </tr>
                             <tr>
                                 <td colspan="9" style="text-align: center; color: #333333; background-color: #CCCCCC;" class="topcolor">Profit</td>
                             </tr>
                             <tr>
                                 <td style="width: 6%">&nbsp;</td>
                                 <td style="width: 9%; font-size: x-small;"><b>Profit</b></td>
                                 <td style="width: 9%"><b style="font-size: x-small">Pay Ratio</b></td>
                                 <td style="width: 12%"><b style="font-size: x-small">Payable</b></td>
                                 <td style="width: 8%"><b style="font-size: x-small">Paid</b></td>
                                 <td style="width: 8%"><b style="font-size: x-small">Due</b></td>
                                 <td style="width: 9%"><b style="font-size: x-small">Adjustment</b></td>
                                 <td style="width: 9%"><b style="font-size: x-small">Actual Pay</b></td>
                                 <td style="width: 100px"><b style="font-size: x-small">Balance</b></td>
                             </tr>
                             <tr>
                                 <td style="width: 6%; font-weight: 700;">Own</td>
                                 <td style="width: 9%">
                                     <asp:Label ID="lblownprofit" runat="server" Style="font-size: 14px"></asp:Label>
                                 </td>
                                 <td style="width: 1%">
                                     <asp:Label ID="lblownpayratiopro" runat="server" Style="font-size: 14px"></asp:Label>
                                 </td>
                                 <td style="width: 12%">
                                     <asp:Label ID="lblownpayablepro" runat="server" Style="font-size: 14px"></asp:Label>
                                 </td>
                                 <td style="width: 8%">
                                     <asp:Label ID="lblownpaidpro" runat="server" Style="font-size: 14px"></asp:Label>
                                 </td>
                                 <td style="width: 8%">
                                     <asp:Label ID="lblownduepro" runat="server" Style="font-size: 14px"></asp:Label>
                                 </td>
                                 <td style="width: 9%">
                                     <asp:TextBox ID="txtownadjustmentpro" runat="server" AutoPostBack="True" OnTextChanged="txtownadjustmentpro_TextChanged" Width="100px">0</asp:TextBox>
                                     <ajaxToolkit:FilteredTextBoxExtender ID="txtownadjustmentpro0_FilteredTextBoxExtender" runat="server" Enabled="True" FilterMode="ValidChars" FilterType="Custom, Numbers" TargetControlID="txtownadjustmentpro" ValidChars=".">
                                     </ajaxToolkit:FilteredTextBoxExtender>
                                 </td>
                                 <td style="width: 9%">
                                     <asp:TextBox ID="txtownactualpaypro" runat="server" AutoPostBack="True" OnTextChanged="txtownactualpaypro_TextChanged" Width="100px">0</asp:TextBox>
                                   
                                     <ajaxToolkit:FilteredTextBoxExtender ID="txtownactualpaypro0_FilteredTextBoxExtender0" runat="server" Enabled="True" FilterMode="ValidChars" FilterType="Custom, Numbers" TargetControlID="txtownactualpaypro" ValidChars=".">
                                     </ajaxToolkit:FilteredTextBoxExtender>
                                 </td>
                                 <td style="width: 100px">
                                     <asp:Label ID="lblownbalancepro" runat="server" Style="font-size: 14px"></asp:Label>
                                 </td>
                             </tr>
                             <tr>
                                 <td style="width: 6%; font-weight: 700;">Employer</td>
                                 <td style="width: 9%">
                                     <asp:Label ID="lblcomprofit" runat="server" Style="font-size: 14px"></asp:Label>
                                 </td>
                                 <td style="width: 1%">
                                     <asp:Label ID="lblcompayratiopro" runat="server" Style="font-size: 14px"></asp:Label>
                                 </td>
                                 <td style="width: 12%">
                                     <asp:Label ID="lblcompayablepro" runat="server" Style="font-size: 14px"></asp:Label>
                                 </td>
                                 <td style="width: 8%">
                                     <asp:Label ID="lblcompaidpro" runat="server" Style="font-size: 14px"></asp:Label>
                                 </td>
                                 <td style="width: 8%">
                                     <asp:Label ID="lblcomduepro" runat="server" Style="font-size: 14px"></asp:Label>
                                 </td>
                                 <td style="width: 9%">
                                     <asp:TextBox ID="txtcomadjustmentpro" runat="server" OnTextChanged="txtcomadjustmentpro_TextChanged" AutoPostBack="true" Width="100px">0</asp:TextBox>
                                     <ajaxToolkit:FilteredTextBoxExtender ID="txtcomadjustmentpro0_FilteredTextBoxExtender" runat="server" Enabled="True" FilterMode="ValidChars" FilterType="Custom, Numbers" TargetControlID="txtcomadjustmentpro" ValidChars=".">
                                     </ajaxToolkit:FilteredTextBoxExtender>
                                 </td>
                                 <td style="width: 9%">
                                     <asp:TextBox ID="txtcomactualpaypro" runat="server" AutoPostBack="True" OnTextChanged="txtcomactualpaypro_TextChanged" Width="100px">0</asp:TextBox>
                                     <ajaxToolkit:FilteredTextBoxExtender ID="txtcomactualpaypro0_FilteredTextBoxExtender" runat="server" Enabled="True" FilterMode="ValidChars" FilterType="Custom, Numbers" TargetControlID="txtcomactualpaypro" ValidChars=".">
                                     </ajaxToolkit:FilteredTextBoxExtender>
                                 </td>
                                 <td style="width: 100px">
                                     <asp:Label ID="lblcombalancepro" runat="server" Style="font-size: 14px"></asp:Label>
                                 </td>
                             </tr>
                             <tr>
                                 <td style="width: 6%">&nbsp;</td>
                                 <td style="width: 9%">&nbsp;</td>
                                 <td style="width: 1%">&nbsp;</td>
                                 <td style="width: 12%">&nbsp;</td>
                                 <td style="width: 8%">
                                     &nbsp;</td>
                                 <td style="width: 8%">&nbsp;</td>
                                 <td style="width: 9%">&nbsp;</td>
                                 <td style="width: 9%">&nbsp;</td>
                                 <td style="width: 100px">&nbsp;</td>
                             </tr>
                             <tr>
                                 <td colspan="9" style="text-align: center;">
                                     <asp:Button ID="btnsettlement" runat="server" OnClick="btnsettlement_Click" OnClientClick="ShowConfirmBox(this,'Are you sure settlement the employee?'); return false;" Text="Settlement" />
                                 </td>
                             </tr>
                             <tr>
                                 <td style="width: 6%">&nbsp;</td>
                                 <td style="width: 9%">&nbsp;</td>
                                 <td style="width: 1%">&nbsp;</td>
                                 <td style="width: 12%">&nbsp;</td>
                                 <td style="width: 8%">&nbsp;</td>
                                 <td style="width: 8%">&nbsp;</td>
                                 <td style="width: 9%">&nbsp;</td>
                                 <td style="width: 9%">&nbsp;</td>
                                 <td style="width: 100px">&nbsp;</td>


                             </tr>


                            


                        </table>

                       


                                </td>
                </tr>



                    </td>
                </tr>




               

            </table>
   
             </ContentTemplate>
                  <Triggers>
                      <asp:PostBackTrigger ControlID="btnstatement" />
                     <%-- <asp:AsyncPostBackTrigger ControlID="btadd" EventName="Click" />
                      <asp:AsyncPostBackTrigger ControlID="gdvpfsat" EventName="SelectedIndexChanged" />--%>
                  </Triggers>
    </asp:UpdatePanel>
</asp:Content>

