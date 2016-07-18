<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/masMain.master"  CodeFile="frm_loan_reschedule.aspx.cs" Inherits="frm_loan_reschedule" %><%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<%@ Register assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2" namespace="eWorld.UI" tagprefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

        <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
        <table style="width: 100%;">
            <tr>
                <td style="width: 101px">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="cpHeaderContent" colspan="2">PF LOAN RESCHEDULING</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 101px">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 101px">
                    &nbsp;</td>
                <td >

                    <table id="Table1" runat="server" style="width: 100%;">
                        <tr>
                            <td style="text-align: right; width: 283px">
                                <span style="font-size: small">EMPLOYEE&nbsp;/LOAN ID :</span></td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtemployee" runat="server"  Width="407px"></asp:TextBox>
                                <ajaxToolkit:AutoCompleteExtender ID="txtserviceBill_AutoCompleteemployee" runat="server"
                                    BehaviorID="AutoCompleteEmployeeInfo" CompletionInterval="1000"
                                    CompletionListCssClass="autocomplete_completionListElement"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                    CompletionListItemCssClass="autocomplete_listItem"
                                    CompletionSetCount="20" DelimiterCharacters=","
                                    EnableCaching="false" MinimumPrefixLength="3"
                                    ServiceMethod="GetEmployeeLoan"
                                    ServicePath="~/services/srvSystem.asmx"
                                    ShowOnlyCurrentWordInCompletionListItem="true"
                                    TargetControlID="txtemployee">
                                </ajaxToolkit:AutoCompleteExtender>
                                &nbsp;<asp:Button ID="btnshow" runat="server" OnClick="btnshow_Click" Style="font-size: small" Text="SHOW" />
                            </td>
                        </tr>
                    </table>
                    <table id="tbldet" runat="server" style="text-align: center; width: 100%; border-left-width: 1px; border-bottom-width: 1px; border-right-width: 1px; vertical-align: middle">
                        <tr>
                            <td style="width: 38%; text-align: right; font-size: small;">Employee ID:</td>
                            <td style="width: 135px; text-align: left;">
                                <asp:Label ID="lblid" runat="server" Text="" Style="font-size: 17px"></asp:Label>
                            </td>
                            <td style="width: 8%; text-align: right;">
                                <span style="font-size: small">Loan Ref No:</span></td>
                            <td style="width: 24%; text-align: left; font-size: 17px;">
                                <asp:Label ID="lblloan_ref" runat="server" Text="" Font-Size="17px"></asp:Label>
                                </span>
                            </td>
                            <td colspan="2" rowspan="7"></td>
                        </tr>
                        <tr>
                            <td style="width: 38%; text-align: right; font-size: small;">Employee Name:</td>
                            <td style="width: 135px; text-align: left;">
                                <asp:Label ID="lblname" runat="server" Text="" Style="font-size: 17px"></asp:Label>
                            </td>
                            <td style="width: 8%; text-align: right;">
                                <span style="font-size: small">Loan Amount:</span></td>
                            <td style="width: 24%; text-align: left; font-size: small;">
                                <asp:Label ID="lblloanamount" runat="server" Text=""  Font-Size="17px"></asp:Label>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 38%; text-align: right; font-size: small;">Employee Designation:</td>
                            <td style="width: 135px; text-align: left;">
                                <asp:Label ID="lbldesignation" runat="server" Text="" Style="font-size: 17px"></asp:Label>
                            </td>
                            <td style="width: 8%; text-align: right;">
                                <span style="font-size: small">Interest Rate:</span></td>
                            <td style="width: 24%; text-align: left; font-size: small;">
                                <asp:Label ID="lblinstrate" runat="server" Text=""  Font-Size="17px"></asp:Label>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 38%; text-align: right; font-size: small;">Employee Organization:</td>
                            <td style="width: 135px; text-align: left;">
                                <asp:Label ID="lblorganization" runat="server" Text="" Style="font-size: 17px"></asp:Label>
                            </td>
                            <td style="width: 8%; text-align: right;">
                                <span style="font-size: small">No Of Installment:</span></td>
                            <td style="width: 24%; text-align: left; font-size: small;">
                                <asp:Label ID="lblnoofinst" runat="server" Text=""  Font-Size="17px"></asp:Label>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 38%; text-align: right; font-size: small;">Employee Company:</td>
                            <td style="width: 135px; text-align: left;">
                                <asp:Label ID="lblcompany" runat="server" Text="" Style="font-size: 17px"></asp:Label>
                            </td>
                            <td style="width: 8%; text-align: right;">
                                <span style="font-size: small">Loan Given Date:</span></td>
                            <td style="width: 24%; text-align: left; font-size: small;">
                                <asp:Label ID="lblloandate" runat="server" Text=""  Font-Size="17px"></asp:Label>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 38%; text-align: right; font-size: small;">Employee Joinning Date:</td>
                            <td style="width: 135px; text-align: left;">
                                <asp:Label ID="lbljoindate" runat="server" Text="" Style="font-size: 17px"></asp:Label>
                            </td>
                            <td style="width: 8%; text-align: right;">
                                <span style="font-size: small">Loan Status:</span></td>
                            <td style="width: 24%; text-align: left; font-size: small;">
                                <asp:Label ID="lblloanstatus" runat="server" Text=""  Font-Size="17px"></asp:Label>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 38%; text-align: right; font-size: small;">Service Length (Months):</td>
                            <td style="width: 135px; text-align: left;">
                                <asp:Label ID="lblservicelen" runat="server" Text="" Style="font-size: 17px"></asp:Label>
                            </td>
                            <td style="width: 8%; text-align: right;">
                                <span style="font-size: small">No Installment Given</span></td>
                            <td style="width: 24%; text-align: left; font-size: small;">
                                <asp:Label ID="lblnoofinsgiven" runat="server" Text=""  Font-Size="17px"></asp:Label>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 38%; font-size: small; text-align: right;">Loan Installment Detail:</td>
                            <td colspan="4">&nbsp;
                                
                <asp:GridView ID="gdSchedule" runat="server" CellPadding="4"
                    ForeColor="#333333" GridLines="None" Width="100%" Font-Names="Verdana"
                    Font-Size="Small">
                    <RowStyle BackColor="#EFF3FB" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#A4FFD1" Font-Bold="True" ForeColor="#99FF33" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                            </td>
                            <td style="width: 11%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 38%">&nbsp;</td>
                            <td style="width: 135px">&nbsp;</td>
                            <td style="width: 8%">&nbsp;</td>
                            <td style="width: 24%">&nbsp;</td>
                            <td style="width: 11%">&nbsp;</td>
                            <td style="width: 11%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: center;" colspan="6">
                                <asp:Label ID="lblMessage" runat="server" Style="font-size: small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 38%; height: 20px; font-size: small; text-align: right;">Closing Balance:</td>
                            <td style="height: 20px; width: 135px; font-size: small; text-align: left;">

                                <asp:Label ID="lblclosingbal" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td style="width: 8%; height: 20px;"></td>
                            <td style="height: 20px; width: 24%;"></td>
                            <td style="height: 20px; width: 11%;"></td>
                            <td style="height: 20px; width: 11%;"></td>
                        </tr>
                        <tr>
                            <td style="width: 38%; font-size: small; text-align: right;">Interest Rate:</td>
                            <td style="width: 135px; text-align: left; font-size: small;">

                                <asp:Label ID="lblinsrate2" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td style="width: 8%">&nbsp;</td>
                            <td style="width: 24%">&nbsp;</td>
                            <td style="width: 11%">&nbsp;</td>
                            <td style="width: 11%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 38%; font-size: small; text-align: right;">No of Installment:</td>
                            <td style="width: 135px; text-align: left;">

                                <asp:TextBox ID="txtnoofinst" runat="server"></asp:TextBox>
                            </td>
                            <td style="width: 8%">

                                <asp:Button ID="btn_viewschedule" runat="server" Text="Show ReSchedule" Width="193px" OnClick="btn_viewschedule_Click" />
                            </td>
                            <td style="width: 24%">&nbsp;</td>
                            <td style="width: 11%">&nbsp;</td>
                            <td style="width: 11%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 38%; font-size: small; text-align: right;">Next Installment Date:</td>
                            <td style="width: 135px; text-align: left;">

                                <ew:CalendarPopup ID="cldinsdate" runat="server" BackColor="#FFC20F" Culture="English (United Kingdom)" DisableTextBoxEntry="False" Nullable="True" Width="85px">
                                    <ButtonStyle CssClass="btn2" />
                                </ew:CalendarPopup>
                            </td>
                            <td style="width: 8%">&nbsp;</td>
                            <td style="width: 24%">&nbsp;</td>
                            <td style="width: 11%">&nbsp;</td>
                            <td style="width: 11%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 38%">&nbsp;</td>
                            <td style="width: 135px">&nbsp;</td>
                            <td style="width: 8%">&nbsp;</td>
                            <td style="width: 24%">&nbsp;</td>
                            <td style="width: 11%">&nbsp;</td>
                            <td style="width: 11%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 38%">&nbsp;</td>
                            <td colspan="4">

                                <asp:GridView ID="gdnewSchedule" runat="server" CellPadding="4" Width="100%" Font-Names="Verdana"
                                    Font-Size="Small" OnSelectedIndexChanged="gdnewSchedule_SelectedIndexChanged" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px">
                                    <RowStyle BackColor="White" ForeColor="#330099" />
                                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                    <HeaderStyle BackColor="green" Font-Bold="True" ForeColor="#FFFFCC" />
                                    <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                    <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                    <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                    <SortedDescendingHeaderStyle BackColor="#7E0000" />
                                </asp:GridView>
                            </td>
                            <td style="width: 11%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 38%">&nbsp;</td>
                            <td style="width: 135px">&nbsp;</td>
                            <td style="width: 8%">&nbsp;</td>
                            <td style="width: 24%">&nbsp;</td>
                            <td style="width: 11%">&nbsp;</td>
                            <td style="width: 11%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: center;" colspan="6">
                                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" Width="83px" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 38%; height: 25px;"></td>
                            <td style="width: 135px; height: 25px;"></td>
                            <td style="width: 8%; height: 25px;"></td>
                            <td style="width: 24%; height: 25px;"></td>
                            <td style="width: 11%; height: 25px;"></td>
                            <td style="width: 11%; height: 25px;"></td>
                        </tr>
                    </table>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 101px">&nbsp;</td>
                <td>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    
   </ContentTemplate>

       <%--       <Triggers>

                  <asp:AsyncPostBackTrigger ControlID="btnshow" EventName="Click" />
                  <asp:AsyncPostBackTrigger ControlID="btnpayschedule" EventName="Click" />
                  <asp:PostBackTrigger ControlID="btnpfstatement" />
                  <asp:PostBackTrigger ControlID="btnexport" />   
                          
               </Triggers>--%>


          
            </asp:UpdatePanel>
    
</asp:Content>