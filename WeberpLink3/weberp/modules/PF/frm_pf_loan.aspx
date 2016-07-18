<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/masMain.master"  CodeFile="frm_pf_loan.aspx.cs" Inherits="frm_pf_loan" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<%@ Register assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2" namespace="eWorld.UI" tagprefix="ew" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

        <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>

    
        <table style="width: 100%;">
            <tr>
                <td style="width: 99px">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="cpHeaderContent" colspan="2">PF LOAN ENTRY</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 99px">&nbsp;</td>
                <td style="text-align: center">
                    <asp:Label ID="lblmessage" runat="server" style="font-size: 15px"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
                  <tr>
                <td style="width: 99px" valign="top" rowspan="2">
                    &nbsp;</td>
                <td valign="top">
                  <table>



                        <tr>
                            <td style="text-align: right; width: 214px;">
                                <asp:Label ID="Label1" runat="server" Text="EMPLOYEE :  "></asp:Label>
                            </td>
                            <td style="text-align: left; ">
                                </span>
                                <asp:TextBox ID="txtemployee" runat="server" Width="407px" AutoPostBack="True"  ></asp:TextBox>
                                <ajaxToolkit:AutoCompleteExtender ID="txtee_Auto"
                                     runat="server" BehaviorID="AutoCompEInfod" 
                                    CompletionInterval="1000" CompletionListCssClass="autocomplete_completionListElement"
                                     CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                                    CompletionListItemCssClass="autocomplete_listItem" 
                                    CompletionSetCount="20" DelimiterCharacters="," 
                                    EnableCaching="false" MinimumPrefixLength="3" 
                                    ServiceMethod="GetEmployee" ServicePath="~/services/srvSystem.asmx"
                                    ShowOnlyCurrentWordInCompletionListItem="true" 
                                    TargetControlID="txtemployee">
                                </ajaxToolkit:AutoCompleteExtender>
                                &nbsp;<asp:Button ID="btnshow" runat="server" OnClick="btnshow_Click" Text="SHOW" />
                            </td>
                            <td style="text-align: left; width: 225px;">&nbsp;</td>
                        </tr>
                    </table>


                </td>
                <td>&nbsp;</td>
            </tr>




            <tr>
                <td>





                    <table id="tbldet" runat="server" style="text-align: center; width: 100%; border-left-width: 1px; border-bottom-width: 1px; border-right-width: 1px; vertical-align: middle;">



                        <tr>
                            <td style="width: 213px; text-align: right;"><span style="font-size: small">Employee ID:</span></td>
                            <td style="text-align: left; background-color: #FFFFFF; width: 516px;">
                                <asp:Label ID="lblid" runat="server" style="font-size: 17px" Text=""></asp:Label>
                                </span>
                            </td>
                            <td style="width: 144px">

                                
                            </td>
                            <td rowspan="7" valign="top">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 213px; text-align: right;"><span style="font-size: small">Employee Name:</td>
                            <td style="text-align: left; background-color: #FFFFFF; width: 516px;">
                                <asp:Label ID="lblname" runat="server" Text="" style="font-size: 17px"></asp:Label>
                                </span>
                            </td>
                            <td style="width: 144px"></td>
                        </tr>
                        <tr>
                            <td style="width: 213px; text-align: right;"><span style="font-size: small">Employee Designation:</td>
                            <td style="text-align: left; background-color: #FFFFFF; width: 516px;">
                                <asp:Label ID="lbldesignation" runat="server" Text="" style="font-size: 17px"></asp:Label>
                                </span>
                            </td>
                            <td style="width: 144px"></td>
                        </tr>
                        <tr>
                            <td style="width: 213px; text-align: right;"><span style="font-size: small">Employee Organization:</td>
                            <td style="text-align: left; background-color: #FFFFFF; width: 516px;">
                                <asp:Label ID="lblorganization" runat="server" Text="" style="font-size: 17px"></asp:Label>
                                </span>
                            </td>
                            <td style="width: 144px"></td>
                        </tr>
                        <tr>
                            <td style="width: 213px; text-align: right;"><span style="font-size: small">Employee Company:</td>
                            <td style="text-align: left; background-color: #FFFFFF; width: 516px;">
                                <asp:Label ID="lblcompany" runat="server" Text="" style="font-size: 17px"></asp:Label>
                                </span>
                            </td>
                            <td style="width: 144px"></td>
                        </tr>
                        <tr>
                            <td style="width: 213px; text-align: right;"><span style="font-size: small">Employee Joinning Date:</td>
                            <td style="text-align: left; background-color: #FFFFFF; width: 516px;">
                                <asp:Label ID="lbljoindate" runat="server" Text="" style="font-size: 17px"></asp:Label>
                                </span>
                            </td>
                            <td style="width: 144px"></td>
                        </tr>
                        <tr>
                            <td style="width: 213px; text-align: right; height: 46px;"><span style="font-size: small">Service Length (Months):</td>
                            <td style="text-align: left; background-color: #FFFFFF; width: 516px;">
                                <asp:Label ID="lblservicelen" runat="server" Text="" style="font-size: 17px"></asp:Label>
                                </span>
                            </td>
                            <td style="width: 144px"></td>
                        </tr>
                        <tr>
                            <td style="width: 213px"><span style="font-size: small"></td>
                            <td style="width: 516px">
                                </span></td>
                            <td style="width: 144px">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 213px; text-align: right;"><span style="font-size: small">PF Contribution Details:</td>
                            <td style="text-align: right; width: 516px;">
                                <table style="border: .5px solid #0480B6; width:100%;">
                                    <tr>
                                        <td style="width: 140px; text-align: right;font-size:small">&nbsp;</td>
                                        <td style="width: 109px; font-size:small"><b style="text-align: right">Own</b></td>
                                        <td style ="font-size :small"><b>Employer</b></td>
                                        <td style ="font-size :small"><b>Own + Employer</b></td>
                                       
                                    </tr>
                                  
                                    <tr>
                                        <td style="text-align: left; width: 140px; font-size:small">Contribution</td>
                                        <td style="width: 109px; text-align: right">
                                            <asp:Label ID="lblowncont" runat="server" Text="Label" style="font-size: small"></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="lblempcont" runat="server" Text="Label" style="font-size: small"></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="lbltotcont" runat="server" Text="Label" style="font-size: small"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; width: 140px;font-size:small">Profit</td>
                                        <td style="width: 109px; text-align: right">
                                            <asp:Label ID="lblownprofit" runat="server" Text="Label" style="font-size: small"></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="lblempprofit" runat="server" Text="Label" style="font-size: small"></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="lbltotprofit" runat="server" Text="Label" style="font-size: small"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 140px; text-align: left"><strong style="text-align: left; font-size: small;">Total</strong></td>
                                        <td style="width: 109px; text-align: right">
                                            <asp:Label ID="lblowntot" runat="server" style="font-weight: bold;font-size:small" Text="Label" ></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="lblemptot" runat="server" style="font-weight: bold; font-size:small" Text="Label" ></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblgrandtot" runat="server" style="font-weight: bold;font-size:small" Text="Label"></asp:Label>
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 144px">
                    <asp:Button ID="btnpfstatement" runat="server"  Text="View PF Statement" Width="144px" OnClick="btnpfstatement_Click" />
                            </td>

                            
                            <td>&nbsp;</td>

                            
                        </tr>

                                                
                        <tr>
                            <td style="width: 213px">&nbsp;</td>
                            <td style="width: 516px">&nbsp;</td>
                            <td style="width: 144px">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>


                    <table id="tblloandet" runat="server" style="text-align: center; width: 100%; border-left-width: 1px; border-bottom-width: 1px; border-right-width: 1px; vertical-align: middle" >



                        <tr>
                            <td style="width: 213px; font-size: small; text-align: right;">PF Loan Amount:</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtloanamount" runat="server" Width ="341px"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 213px; font-size: small; text-align: right;">No of Installment:</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtnoofinst" runat="server"  Width ="341px"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 213px; font-size: small; text-align: right;">Interest Rate:</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtinstrate" runat="server"  Width ="341px"></asp:TextBox>
&nbsp;
                    <asp:Button ID="btnpayschedule" runat="server" OnClick="btnpayschedule_Click" Text="Show Payment Schedule" Width="193px" />
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 213px; font-size: small; text-align: right;">Loan Date:</td>
                            <td style="text-align: left">
                         <ew:CalendarPopup ID="cldloandate" runat="server" BackColor="#FFC20F" Culture="English (United Kingdom)" DisableTextBoxEntry="False" Nullable="True" Width="85px">
                             <ButtonStyle CssClass="btn2" />
                         </ew:CalendarPopup>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 213px; font-size: small; text-align: right;">Installment Start Date</td>
                            <td style="text-align: left">
                         <ew:CalendarPopup ID="cldinsdate" runat="server" BackColor="#FFC20F" Culture="English (United Kingdom)" DisableTextBoxEntry="False" Nullable="True" Width="85px">
                             <ButtonStyle CssClass="btn2" />
                         </ew:CalendarPopup>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 213px">&nbsp;</td>
                            <td style="text-align: left">
                                <asp:Button ID="btnexport" runat="server" Text="Export to Excel" Width="155px" OnClick="btnexport_Click" />
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="gdSchedule" runat="server" CellPadding="4" Font-Names="Verdana" Font-Size="Small" ForeColor="#333333" GridLines="None" style="text-align: left" Width="100%">
                                    <RowStyle BackColor="#EFF3FB" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#A4FFD1" Font-Bold="True" ForeColor="#99FF33" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnpost" runat="server" OnClick="btnpost_Click" Text="POST" Width="83px" />
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 213px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
              </ContentTemplate>

              <Triggers>

                  <asp:AsyncPostBackTrigger ControlID="btnshow" EventName="Click" />
                  <asp:AsyncPostBackTrigger ControlID="btnpayschedule" EventName="Click" />
                  <asp:PostBackTrigger ControlID="btnpfstatement" />
                  <asp:PostBackTrigger ControlID="btnexport" />   
                          
               </Triggers>


          
            </asp:UpdatePanel>
    
</asp:Content>