<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmLunchBillApplication.aspx.cs" Inherits="modules_HRMS_SelfService_frmLunchBillApplication" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="LUNCH BILL APPLICATION" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
            <table style="width: 99%; text-align: left">
                <tr>
                    <td style="width: 161px">
                        <asp:Label ID="Label7" runat="server" Text="Employee Information"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 161px">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <table style="width: 50%;">
                            <tr>
                                <td class="style10" style="width: 92px">
                                    <asp:Label ID="Label8" runat="server" Text="ID"></asp:Label>
                                </td>
                                <td class="style11">:</td>
                                <td style="text-align: left">
                                    <asp:Label ID="lblId" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style10" style="width: 92px">
                                    <asp:Label ID="Label9" runat="server" Text="Name"></asp:Label>
                                </td>
                                <td class="style11">:</td>
                                <td style="text-align: left">
                                    <asp:Label ID="lblName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style10" style="width: 92px">
                                    <asp:Label ID="Label10" runat="server" Text="Department"></asp:Label>
                                </td>
                                <td class="style11">:</td>
                                <td style="text-align: left">
                                    <asp:Label ID="lbldept" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style10" style="width: 92px">
                                    <asp:Label ID="Label11" runat="server" Text="Designation"></asp:Label>
                                </td>
                                <td class="style11">:</td>
                                <td style="text-align: left">
                                    <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style10" style="width: 92px">
                                    <asp:Label ID="Label12" runat="server" Text="Joining Date"></asp:Label>
                                </td>
                                <td class="style11">:</td>
                                <td style="text-align: left">
                                    <asp:Label ID="lblJoiningDate" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 161px">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 161px">
                        <asp:Label ID="Label13" runat="server" Text="Date"></asp:Label>
                    </td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="txtDate" runat="server" placeholder="Select Date" Width="350px"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDate">
                        </ajaxToolkit:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width: 161px">
                        <asp:Label ID="Label14" runat="server" Text="Location During Lunch"></asp:Label>
                    </td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="txtLocationDuringLunch" runat="server" Width="350px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 161px">
                        <asp:Label ID="Label15" runat="server" Text="Purpose"></asp:Label>
                    </td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="txtPurpose" runat="server" Width="350px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 161px">
                        <asp:Label ID="Label18" runat="server" Text="Amount"></asp:Label>
                    </td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="txtAmount" runat="server" onkeypress="return isNumberKey(event)" Width="350px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 161px">
                        <asp:Label ID="Label19" runat="server" Text="Assigned By"></asp:Label>
                    </td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="txtEmployeeSearch" runat="server" AutoPostBack="True" OnTextChanged="txtEmployeeSearch_TextChanged" placeholder="Employee Code" Width="350px"></asp:TextBox>
                        <ajaxToolkit:AutoCompleteExtender ID="txtEmployeeSearch_AutoCompleteExtender" runat="server" CompletionListCssClass="autocomplete_completionListElement" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem2" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetEmpId" ServicePath="~/modules/Payroll/WebService.asmx" TargetControlID="txtEmployeeSearch">
                        </ajaxToolkit:AutoCompleteExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width: 161px">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 161px">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="btnAdd" runat="server" Text="Add" Width="100px" OnClick="btnAdd_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" Width="100px" OnClick="btnClear_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 161px">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:GridView ID="grdLunchBill" runat="server" EmptyDataText="No Data Found !" Width="100%" AutoGenerateColumns="False" OnRowCommand="grdLunchBill_RowCommand" OnRowDeleting="grdLunchBill_RowDeleting">
                            <Columns>
                                <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSl" runat="server" Text='<%# Container.DisplayIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbDate" runat="server" Text='<%# Bind("txtDate", "{0:d}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Location During Lunch">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLocationDuringLunch" runat="server" Text='<%# Bind("txtLocationDuringLunch") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Purpose">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPurpose" runat="server" Text='<%# Bind("txtPurpose") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("txtAmount","{0:0.00}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Assigned By">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAssignedBy" runat="server" Text='<%# Bind("txtEmployeeSearch") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowSelectButton="True" />
                                <asp:CommandField ShowDeleteButton="True" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="width: 161px">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="btnApplyLunchBill" runat="server" Text="Apply For Lunch Bill" Width="200px" OnClick="btnApplyLunchBill_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 161px">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
                </asp:Panel>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAdd" />
            <asp:PostBackTrigger ControlID="btnApplyLunchBill" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                alert("Please Enter Only Numeric Value:");
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
