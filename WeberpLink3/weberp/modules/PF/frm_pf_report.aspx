<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_pf_report.aspx.cs"  Inherits="frm_pf_report" EnableEventValidation="false" ValidateRequest="false"  %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<%@ Register assembly="TimePicker" namespace="MKB.TimePicker" tagprefix="MKB" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
  
    <%-- <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>--%>
       
    <table style="text-align: center; width: 100%; border-left-width: 1px; border-bottom-width: 1px; border-right-width: 1px; vertical-align: middle;" >
       
        <tr>
            <td class="cpHeaderContent" >
                PF REPORT</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblmessage" runat="server" style="font-size: small"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align:left;" >
              
             <table style="width:100%; ">
   
    <tr >
        <td style="width: 638px; height: 10px;">&nbsp;</td>
        <td style="height: 10px; width: 554px; font-size: small; text-align: right;" >
            Report Type</td>
        <td style="width: 5px">
            :</td>
        <td  style="height: 10px;" colspan="4">
            <asp:RadioButtonList ID="rdolistreporttype" runat="server" style="font-size: small" Height="22px" Width="567px" AutoPostBack="True" OnSelectedIndexChanged="rdolistreporttype_SelectedIndexChanged" ForeColor="Black">
                <asp:ListItem Selected="True" Value="Employee Contribution Summary">Employee Contribution Summary</asp:ListItem>
                <asp:ListItem>Employee Contribution Detail</asp:ListItem>
                <asp:ListItem>Employee Statement</asp:ListItem>
                <asp:ListItem>Employees Settlement</asp:ListItem>
                <asp:ListItem>Yearly Distribution</asp:ListItem>
                <asp:ListItem>Employee Wise Distribution</asp:ListItem>
            </asp:RadioButtonList>

        </td>
        <td  style="width: 420px; height: 10px;" colspan="4">
            &nbsp;</td>
        <td style="width: 478px; height: 10px;">&nbsp;</td>
    </tr>
   
    <tr >
        <td style="width: 638px; height: 10px;"></td>
        
        <td style="width: 554px; font-size: small; text-align: right;" >
            </td>
        
        <td  style="height: 10px;" colspan="4">
            <table id="tblemployee" runat="server" style="width:137%;">
                                                                <tr>

                                                                    <td style="width: 5px">
            Employee:</td>

                                                                    <td>
            <asp:TextBox ID="txtemployee" runat="server" Width="472px" AutoPostBack="True"  ></asp:TextBox>

            <ajaxToolkit:AutoCompleteExtender ID="txtserviceBill_AutoCompleteemployee" 
                runat="server" BehaviorID="AutoCompleteEmployeeInfo" CompletionInterval="1000" 
                CompletionListCssClass="autocomplete_completionListElement" 
                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20"
                 DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="3" 
                ServiceMethod="GetEmployee" ServicePath="~/services/srvSystem.asmx"
                 ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtemployee">
                                            </ajaxToolkit:AutoCompleteExtender>

                                                                    </td>
                                                                </tr>
                                                                </table>


            </td>
    </tr>
                 <tr>
                     <td style="width: 638px; height: 12px;"></td>
                     <td style="width: 554px; height: 12px; font-size: small; text-align: right;">Period</td>
                     <td style="width: 5px; height: 12px;">:</td>
                     <td style="width: 173px; height: 12px;">
                         <ew:CalendarPopup ID="dtstdate" runat="server" BackColor="#FFC20F" Culture="English (United Kingdom)" DisableTextBoxEntry="False" Width="100px" SelectedDate="06/09/2015 11:22:33">
                             <ButtonStyle CssClass="btn2" />
                         </ew:CalendarPopup>
                     </td>
                     <td style="width: 42px; height: 12px; font-size: small;">
                         To</td>
                     <td style="width: 447px; height: 12px;">
                         <ew:CalendarPopup ID="dtenddate" runat="server" BackColor="#FFC20F" Culture="English (United Kingdom)" DisableTextBoxEntry="False" Width="100px" SelectedDate="06/09/2015 11:22:41">
                             <ButtonStyle CssClass="btn2" />
                         </ew:CalendarPopup>
                     </td>
                     <td style="height: 12px;">
                         &nbsp;</td>
                     <td style="width: 131px; height: 12px; font-size: small;">
                         &nbsp;</td>
                     <td style="width: 478px; height: 12px;">
                         &nbsp;</td>
                     <td style="width: 478px; height: 12px;">
                         </td>
                     <td style="width: 478px; height: 12px;"></td>
                 </tr>
                 <tr>
                     <td style="width: 638px">&nbsp;</td>
                     <td style="width: 554px">&nbsp;</td>
                     <td style="width: 5px">&nbsp;</td>
                     <td colspan="4">
                         &nbsp;</td>
                     <td style="width: 420px" colspan="4">
                         &nbsp;</td>
                     <td style="width: 478px">&nbsp;</td>
                 </tr>
    </table>
              </td>
        </tr> 
        <tr>
            <td style="height: 28px">
                 <asp:Button ID="btnshow" runat="server"   
                Text="Show" Width="106px" OnClick="btnshow_Click" />
                 &nbsp;
                 </td>
        </tr>
        <tr>
            <td style="text-align: left">
            
                &nbsp;</td>
        </tr>
       
    </table>
   
            <%-- </ContentTemplate>
                  <Triggers>
                      <asp:AsyncPostBackTrigger ControlID="btnupdate" EventName="Click" />
                      <asp:AsyncPostBackTrigger ControlID="GridView_employee" EventName="SelectedIndexChanged" />
                  </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>

