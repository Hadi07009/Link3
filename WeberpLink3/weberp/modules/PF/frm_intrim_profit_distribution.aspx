<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_intrim_profit_distribution.aspx.cs"  Inherits="frm_intrim_profit_distribution" EnableEventValidation="false" ValidateRequest="false"  %>

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
       
      <table cellpadding="0" cellspacing="0" style="text-align: center; width: 100%; border-left-width: 1px; border-bottom-width: 1px; border-right-width: 1px; vertical-align: middle;" id="TABLE1">
        <tr>
            <td class="cpHeaderContent"  >
                INTERIM&nbsp; PROFIT DISTRIBUTION</td>
        </tr>
        <tr>
            <td style="text-align: center; height: 21px;">
                <asp:Label ID="lblmessage" runat="server" style="font-size: small"></asp:Label>
            </td>
        </tr>
          <tr>
              <td style="text-align: center">&nbsp;</td>
          </tr>
        <tr>
            <td style="text-align:left;" >
              
              <table cellpadding="0" cellspacing="0" style="text-align: center; width: 100%; border-left-width: 1px; border-bottom-width: 1px; border-right-width: 1px; vertical-align: middle;" id="TABLE2">
                 <tr>
                     <td style="height: 10px; width: 672px; font-size: small; text-align: right;">Last Yearly Distribution Date</td>
                     <td style="height: 10px; width: 44px;">:</td>
                     <td style="width: 256px; height: 10px; text-align: left;">
                         <ew:CalendarPopup ID="dtlastprofitdistribution" runat="server" BackColor="#FFC20F" Culture="English (United Kingdom)" DisableTextBoxEntry="False" SelectedDate="06/09/2015 11:22:33" Width="90px">
                             <ButtonStyle CssClass="btn2" />
                         </ew:CalendarPopup>
                     </td>
                     <td style="width: 10px; height: 10px; font-size: small;"></td>
                     <td style="width: 634px; height: 10px; font-size: small; text-align: left;">
                         <asp:Label ID="lbltotmonthinvestment" runat="server" style="font-size: 14px; text-align: left;" ForeColor="#006600"></asp:Label>
                     </td>
                     <td style="width: 172px; height: 10px; font-size: small; text-align: left;">&nbsp;</td>
                 </tr>
                 <tr>
                     <td style="height: 10px; width: 672px; font-size: small; text-align: right;">Distribution Upto</td>
                     <td style="width: 44px">:</td>
                     <td style="width: 256px;  text-align: left;">
                         <ew:CalendarPopup ID="dtdistributionupto" runat="server" BackColor="#FFC20F" Culture="English (United Kingdom)" DisableTextBoxEntry="False" SelectedDate="06/09/2015 11:22:33" Width="90px">
                             <ButtonStyle CssClass="btn2" />
                         </ew:CalendarPopup>
                     </td>
                     <td style="width: 10px;">&nbsp;</td>
                     <td style="width: 634px; text-align: left;">
                         <asp:Label ID="lbltotdistributionprofit" runat="server" style="font-size: 14px; text-align: left;" ForeColor="#006600"></asp:Label>
                     </td>
                     <td style="width: 172px; text-align: left;">&nbsp;</td>
                 </tr>
                 <tr>
                     <td style="width: 672px; height: 12px; font-size: small; text-align: right;">Profit</td>
                     <td style="height: 12px; width: 44px;">:</td>
                     <td style="width: 256px; height: 12px;">
                         <asp:TextBox ID="txtprofit" runat="server" AutoPostBack="True" OnTextChanged="txtprofit_TextChanged" Width="341px"></asp:TextBox>
                         <ajaxToolkit:FilteredTextBoxExtender ID="txtprofit_FilteredTextBoxExtender" runat="server" Enabled="True" FilterMode="ValidChars" FilterType="Custom, Numbers" TargetControlID="txtprofit" ValidChars=".">
                         </ajaxToolkit:FilteredTextBoxExtender>


                     </td>
                     <td style="width: 10px; "></td>
                     <td style="width: 634px; ">&nbsp;</td>
                     <td style="width: 172px; ">&nbsp;</td>
                 </tr>
                 <tr>
                     <td style="width: 672px; height: 12px; font-size: small; text-align: right;">Distribution Rate ( Maximum 1)</td>
                     <td style="width: 44px">:</td>
                     <td style="width: 256px; height: 12px;">
                         <asp:TextBox ID="txtdistributionrate" runat="server" AutoPostBack="True" OnTextChanged="txtdistributionrate_TextChanged" Width="341px"></asp:TextBox>
                         <ajaxToolkit:FilteredTextBoxExtender ID="txtstartingodometer_FilteredTextBoxExtender" runat="server" Enabled="True" FilterMode="ValidChars" FilterType="Custom, Numbers" TargetControlID="txtdistributionrate" ValidChars=".">
                         </ajaxToolkit:FilteredTextBoxExtender>
                     </td>
                     <td style="width: 10px; height: 12px; font-size: small;">&nbsp;</td>
                     <td style="width: 634px; height: 12px; font-size: small;">&nbsp;</td>
                     <td style="width: 172px; height: 12px; font-size: small;">&nbsp;</td>
                 </tr>
                 <tr>
                     <td style="width: 672px; font-size: small; height: 24px; text-align: right;">Distribution Amount</td>
                     <td style="width: 44px; height: 24px;">:</td>
                     <td style="width: 256px; text-align:left; height: 24px;">
                         <asp:TextBox ID="txtdistributionamt" runat="server" Width="341px" Enabled="False"></asp:TextBox>
                     </td>
                     <td style="width: 10px; text-align: right; height: 24px;"></td>
                     <td style="width: 634px; text-align: right; height: 24px;">&nbsp;</td>
                     <td style="width: 172px; text-align: right; height: 24px;">&nbsp;</td>
                 </tr>
                 <tr>
                     <td style="width: 672px; height: 21px;"></td>
                     <td style="width: 44px; height: 21px;"></td>
                     <td style="width: 256px; height: 21px;">
                         &nbsp;</td>
                     <td style="width: 10px; height: 21px;"></td>
                     <td style="width: 634px; height: 21px;">&nbsp;</td>
                     <td style="width: 172px; height: 21px;">&nbsp;</td>
                 </tr>
                 <tr>
                     <td style="width: 672px">&nbsp;</td>
                     <td style="width: 44px">&nbsp;</td>
                     <td style="width: 256px; text-align:left">
                         <asp:Button ID="btnsavedistribution" runat="server" OnClick="btnsavedistribution_Click" OnClientClick="ShowConfirmBox(this,'Are you sure save distribution?'); return false;" Text="Save Distribution" Width="120px" />
                         &nbsp;<asp:Button ID="btnpostdistribution" runat="server" OnClick="btnpostdistribution_Click" OnClientClick="ShowConfirmBox(this,'Are you sure post distribution?'); return false;" Text="Post Distribution" Visible="False" Width="125px" />
                     </td>
                     <td style="width: 10px">&nbsp;</td>
                     <td style="width: 634px">&nbsp;</td>
                     <td style="width: 172px">&nbsp;</td>
                 </tr>
    </table>
              </td>
        </tr> 
        <tr>
            <td style="height: 28px">
                 &nbsp; &nbsp; &nbsp;</td>
        </tr>
        <tr>
            <td style="height: 28px; text-align: left;">&nbsp

                EMPLOYEE&nbsp; : <asp:TextBox ID="txtemployee" runat="server" Width="407px"   ></asp:TextBox>

            <ajaxToolkit:AutoCompleteExtender ID="txtserviceBill_AutoCompleteemployee" 
                runat="server" BehaviorID="AutoCompleteEmployeeInfo" CompletionInterval="1000" 
                CompletionListCssClass="autocomplete_completionListElement" 
                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20"
                 DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="3" 
                ServiceMethod="GetEmployee" ServicePath="~/services/srvSystem.asmx"
                 ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtemployee">
                                            </ajaxToolkit:AutoCompleteExtender>

        &nbsp;
                    <asp:Button ID="btnadd" runat="server" Text="ADD" OnClick="btnadd_Click" />


            </td>
        </tr>
        <tr>
            <td style="height: 28px; text-align: left;">&nbsp;




                <asp:GridView ID="GridView_emp" runat="server" CellPadding="4" Font-Names="Verdana" Font-Size="Small" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView_emp_SelectedIndexChanged" Width="100%">
                    <RowStyle BackColor="#EFF3FB" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:CommandField SelectText="Remove" ShowSelectButton="True" />
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>




            </td>
        </tr>
        <tr>
            <td style="height: 28px; text-align: left;">
                <asp:Button ID="btnshowall" runat="server" Text="SHOW ALL" OnClick="btnshowall_Click" />
                <asp:Button ID="btnshowemp" runat="server"  Text="SHOW EMP" OnClick="btnshowemp_Click" />
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
            
                &nbsp;</td>
        </tr>
       
    </table>
   
            </ContentTemplate>
                  <Triggers>
                     
                       <asp:PostBackTrigger ControlID="btnsavedistribution"  />
                      <asp:AsyncPostBackTrigger ControlID="txtdistributionrate" EventName="TextChanged" />
                       <asp:AsyncPostBackTrigger ControlID="txtprofit" EventName="TextChanged" />
                       <asp:PostBackTrigger ControlID="btnpostdistribution"  />
                        <asp:PostBackTrigger ControlID="btnshowall" />
                      <asp:PostBackTrigger ControlID="btnshowemp" />
                   


                  </Triggers>
    </asp:UpdatePanel>
</asp:Content>

