<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmProjectManagement.aspx.cs" Inherits="modules_HRMS_Details_frmProjectManagement" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <cc1:ConfirmBox ID="ConfirmBox1" runat="server" />
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td colspan="3" style="text-align: left">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left">
                        <asp:Panel ID="PanelTaksList" runat="server">
                            <table style="width:100%;">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="To Do List . . ."></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="grdTaskList" runat="server" AutoGenerateColumns="False" OnRowCommand="grdTaskList_RowCommand" OnRowDataBound="grdTaskList_RowDataBound" OnRowDeleting="grdTaskList_RowDeleting" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Activity ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblActivityID" runat="server" Text='<%# Bind("ActivityID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Activity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblActivityName" runat="server" Text='<%# Bind("ActivityName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Responsible">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblResponsibleID" runat="server" Text='<%# Bind("ResponsibleID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Accountable">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAccountableID" runat="server" Text='<%# Bind("AccountableID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Consulted">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblConsultedID" runat="server" Text='<%# Bind("ConsultedID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Informed">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInformedID" runat="server" Text='<%# Bind("InformedID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Due Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDueDate" runat="server" Text='<%# Bind("DueDate", "{0:d}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Priority">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPriority" runat="server" Text='<%# Bind("PriorityText") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowSelectButton="True" />                                                
                                                <asp:CommandField ShowDeleteButton="True" />
                                                <asp:ButtonField CommandName="Plan" Text="Plan" />
                                                <asp:ButtonField CommandName="Progress" Text="Progress"  />
                                                <asp:ButtonField CommandName="ActivityLog" Text="Activity Log" />
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left">
                        <asp:Panel ID="PanelNewEntry" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px">
                            <table style="width:90%;margin-left:100px">
                                <tr>
                                    <td colspan="7">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="7">
                                        <asp:Label ID="lblActivityText" runat="server"></asp:Label>
                                        <asp:Label ID="lblActivityID" runat="server" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPActivity" runat="server" Text="Activity"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="txtPActivity" runat="server"></asp:TextBox>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text="Priority"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlpPriority" runat="server" Width="220px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="Responsible"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="txtpResponsibleID" runat="server"></asp:TextBox>
                                        <ajaxToolkit:AutoCompleteExtender ID="txtpResponsibleID_AutoCompleteExtender" runat="server" BehaviorID="txtpResponsibleID_AutoCompleteExtender" DelimiterCharacters="" 
                                            CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                            CompletionListItemCssClass="autocomplete_listItem2"
                                            Enabled="True"
                                            MinimumPrefixLength="1" ServiceMethod="GetEmpId" ServicePath="~/modules/Payroll/WebService.asmx"
                                            TargetControlID="txtpResponsibleID">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Text="Description"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="txtpDescription" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="Accountable"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="txtpAccountableID" runat="server"></asp:TextBox>
                                        <ajaxToolkit:AutoCompleteExtender ID="txtpAccountableID_AutoCompleteExtender" runat="server" BehaviorID="txtpAccountableID_AutoCompleteExtender" DelimiterCharacters=""
                                            CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                            CompletionListItemCssClass="autocomplete_listItem2"
                                            Enabled="True"
                                            MinimumPrefixLength="1" ServiceMethod="GetEmpId" ServicePath="~/modules/Payroll/WebService.asmx"
                                            TargetControlID="txtpAccountableID">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" Text="Logistices Required"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="txtpLogisticesRequired" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="Consulted"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="txtpConsultedID" runat="server"></asp:TextBox>
                                        <ajaxToolkit:AutoCompleteExtender ID="txtpConsultedID_AutoCompleteExtender" runat="server" BehaviorID="txtpConsultedID_AutoCompleteExtender" DelimiterCharacters="" 
                                            CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                            CompletionListItemCssClass="autocomplete_listItem2"
                                            Enabled="True"
                                            MinimumPrefixLength="1" ServiceMethod="GetEmpId" ServicePath="~/modules/Payroll/WebService.asmx"
                                            TargetControlID="txtpConsultedID">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" Text="Related Costs"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="txtpRelatedCosts" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text="Informed"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="txtpInformedID" runat="server"></asp:TextBox>
                                        <ajaxToolkit:AutoCompleteExtender ID="txtpInformedID_AutoCompleteExtender" runat="server" BehaviorID="txtpInformedID_AutoCompleteExtender" DelimiterCharacters="" 
                                            CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                            CompletionListItemCssClass="autocomplete_listItem2"
                                            Enabled="True"
                                            MinimumPrefixLength="1" ServiceMethod="GetEmpId" ServicePath="~/modules/Payroll/WebService.asmx"
                                            TargetControlID="txtpInformedID">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:Label ID="Label11" runat="server" Text="Any Issue"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="txtpAnyIssue" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text="Due Date"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="txtpDueDate" runat="server"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txtpDueDate_CalendarExtender" runat="server" BehaviorID="txtpDueDate_CalendarExtender" Format="dd/MM/yyyy" TargetControlID="txtpDueDate">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" Text="Remarks"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="txtpRemarks" runat="server" TextMode="MultiLine" Width="215px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td colspan="5">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" Width="100px" OnClick="btnAdd_Click" />
                                        <asp:Button ID="btnDone" runat="server" OnClick="btnDone_Click" Text="Done" Width="100px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td colspan="5">&nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Panel ID="PanelProgress" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px">
                            <table style="width:90%;text-align:left;margin-left:100px">
                                <tr>
                                    <td colspan="3">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblActivityNameProgress" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label13" runat="server" Text="Remarks"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="txtRemarksProgress" runat="server" TextMode="MultiLine" Width="516px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label14" runat="server" Text="Status"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlActivityStatus" runat="server" Width="250px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" Width="100px" />
                                        <asp:Button ID="btnDoneProgress" runat="server" OnClick="btnDoneProgress_Click" Text="Done" Width="100px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Panel ID="PanelActivityLog" runat="server">
                            <table style="width:90%;text-align:left;margin-left:100px">
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblActivityNameLog" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:GridView ID="grdActivityLog" runat="server" AutoGenerateColumns="False" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatusText" runat="server" Text='<%# Bind("StatusText") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Entry User">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEntryUserId" runat="server" Text='<%# Bind("EntryUserId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEntryDate" runat="server" Text='<%# Bind("EntryDate", "{0:d}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnDoneLog" runat="server" OnClick="btnDoneLog_Click" Text="Done" Width="100px" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAdd" />
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 46 || charCode > 57)) {
                alert("Please Enter Only Numeric Value:");
                return false;
            }
            return true;
        }
        </script>
    
</asp:Content>
