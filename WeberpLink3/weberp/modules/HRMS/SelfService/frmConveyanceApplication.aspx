<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmConveyanceApplication.aspx.cs" Inherits="modules_HRMS_SelfService_frmConveyanceApplication" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="CONVEYANCE APPLICATION" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
                    <tr>
                        <td style="width: 145px">
                            <asp:Label ID="Label7" runat="server" Text="Employee Information"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 145px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3"><table style="width: 50%;">
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
                                        </table></td>
                    </tr>
                    <tr>
                        <td style="width: 145px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
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
                        <td style="width: 145px">
                            <asp:Label ID="Label14" runat="server" Text="Purpose of Journey"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtPurposeofJourney" runat="server" Width="350px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
                            <asp:Label ID="Label15" runat="server" Text="From"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtFrom" runat="server" Width="350px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 145px; height: 38px;">
                            <asp:Label ID="Label16" runat="server" Text="To"></asp:Label>
                        </td>
                        <td style="height: 38px">:</td>
                        <td style="height: 38px">
                            <asp:TextBox ID="txtTo" runat="server" Width="350px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
                            <asp:Label ID="Label17" runat="server" Text="Mode of Journey"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtModeofJourney" runat="server" Width="350px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
                            <asp:Label ID="Label18" runat="server" Text="Amount"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtAmount" runat="server" onkeypress="return isNumberKey(event)" Width="350px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
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
                        <td style="width: 145px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 145px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" Width="100px" />
                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 145px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        
                        <td colspan="3">
                            <asp:GridView ID="grdConveyanceRecord" runat="server" AutoGenerateColumns="False" EmptyDataText="No Data Found !" ShowFooter="True" Width="100%" OnRowCommand="grdConveyanceRecord_RowCommand" OnRowDataBound="grdConveyanceRecord_RowDataBound" OnRowDeleting="grdConveyanceRecord_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSl" runat="server" Text='<%# Container.DisplayIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Date" DataField="txtDate"  DataFormatString="{0:d}"   />
                                    <asp:BoundField HeaderText="Purpose of Journey" DataField="txtPurposeofJourney" />
                                    <asp:BoundField HeaderText="From" DataField="txtFrom" />
                                    <asp:BoundField HeaderText="To" DataField="txtTo" />
                                    <asp:BoundField HeaderText="Mode of Journey" DataField="txtModeofJourney" />
                                    <asp:BoundField HeaderText="Amount" DataField="txtAmount" />
                                    <asp:BoundField HeaderText="Assigned By" DataField="txtEmployeeSearch" />
                                    <asp:CommandField ShowSelectButton="True" />
                                    <asp:CommandField ShowDeleteButton="True" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:Button ID="btnApplyConveyance" runat="server" Text="Apply Conveyance Application" Width="200px" OnClick="btnApplyConveyance_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 145px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 145px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 145px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 145px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 145px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 145px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 145px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAdd" />
            <asp:PostBackTrigger ControlID="btnApplyConveyance" />
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
