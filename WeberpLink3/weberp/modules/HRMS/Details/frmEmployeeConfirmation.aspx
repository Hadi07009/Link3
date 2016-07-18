<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmEmployeeConfirmation.aspx.cs" Inherits="modules_HRMS_Details_frmEmployeeConfirmation" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc2:MessageBox ID="MessageBox1" runat="server" />
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                alert("Please Enter Only Numeric Value:");
                return false;
            }
            return true;
        }
        $(document).ready(function () {
            $("body").css("overflow", "hidden");
            $("body").css("overflow-x", "hidden");
            $("body").css("overflow-y", "hidden");
        });

        $(document).ready(function () {

            $("body").css("overflow", "");

        });
    </script>

    <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
        <asp:Label ID="lblleave" Text="Employee Confirmation" runat="server" />
    </asp:Panel>
    <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
        <table style="width: 99%; text-align: left">
            <tr>
                <td colspan="3">
                    <br />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">
                    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="1" Width="95%">
                        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Confirmation Reminder">
                            <HeaderTemplate>Confirmation Reminder</HeaderTemplate>
                            <ContentTemplate>
                                <table style="width: 99%; text-align: left">
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" Text="Date"></asp:Label></td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtFromDate" runat="server" Width="178px"></asp:TextBox><cc1:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFromDate"></cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnList" runat="server" OnClick="btnList_Click" Text="List" Width="130px" CssClass="btn2" /><asp:Button ID="btnReportForConfirmation" runat="server" OnClick="btnReportForConfirmation_Click" Text="Report" Width="130px" CssClass="btn2" /><asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export" Width="130px" CssClass="btn2" /></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:GridView ID="grdForConfirme" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="grdForConfirme_PageIndexChanging" OnRowCommand="grdForConfirme_RowCommand" OnRowDataBound="grdForConfirme_RowDataBound" PageSize="15" Width="100%">
                                                <Columns>
                                                    <asp:BoundField DataField="Employee Id" HeaderText="Emp Id" />
                                                    <asp:BoundField DataField="Name" HeaderText="Name" />
                                                    <asp:BoundField DataField="Department" HeaderText="Department" />
                                                    <asp:BoundField DataField="Section" HeaderText="Section" />
                                                    <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                    <asp:BoundField DataField="Joining Date" HeaderText="Joining Date" />
                                                    <asp:BoundField DataField="Present Salary" HeaderText="Present Salary" />
                                                    <asp:BoundField DataField="Confirm Salary" HeaderText="Confirm Salary" />
                                                    <asp:CommandField SelectText="View" ShowSelectButton="True" />
                                                </Columns>
                                                <PagerStyle HorizontalAlign="Center" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label8" runat="server" Text="Employee Code"></asp:Label></td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtGetEmpId" runat="server" Width="400px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label9" runat="server" Text="Employee Name"></asp:Label></td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtGetEmpName" runat="server" Width="400px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label12" runat="server" Text="Confirm/Extend Date"></asp:Label></td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtGetConfirmationDate" runat="server" Width="178px"></asp:TextBox><cc1:CalendarExtender ID="txtGetConfirmationDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtGetConfirmationDate"></cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;<asp:Label ID="Label13" runat="server" Text="Remarks"></asp:Label></td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtRemarks" runat="server" Height="80px" TextMode="MultiLine" Width="400px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnPostForConfirm" runat="server" OnClick="btnPostForConfirm_Click" Text="Post For Confirm" Width="130px" CssClass="btn2" /><asp:Button ID="btnForExtension" runat="server" OnClick="btnForExtension_Click" Text="Post For Extention" Width="130px" CssClass="btn2" /></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label10" runat="server" Text="Joining Salary" Visible="False"></asp:Label></td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtGetJoiningSalary" runat="server" onkeypress="return isNumberKey(event)" Width="400px" Visible="False"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label11" runat="server" Text="Confirm Salary" Visible="False"></asp:Label></td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtGetConfirmSalary" runat="server" onkeypress="return isNumberKey(event)" Width="400px" Visible="False"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px">
                                            <asp:Label ID="Label3" runat="server" Text="Employee Code" Visible="False"></asp:Label></td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtEmpId" runat="server" AutoPostBack="True" OnTextChanged="txtEmpId_TextChanged" Width="400px" Visible="False"></asp:TextBox><cc1:AutoCompleteExtender ID="txtEmpId_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="Emp_Mas_Emp_Id" ServicePath="~/modules/Payroll/WebService.asmx" TargetControlID="txtEmpId"></cc1:AutoCompleteExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Text="Employee Name" Visible="False"></asp:Label></td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtEmpName" runat="server" Width="400px" Visible="False"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" Text="Joining Salary" Visible="False"></asp:Label></td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtJoiningSal" runat="server" onkeypress="return isNumberKey(event)" Width="400px" Visible="False"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label6" runat="server" Text="Confirm Salary" Visible="False"></asp:Label></td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtConfirmSalary" runat="server" onkeypress="return isNumberKey(event)" Width="400px" Visible="False"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnApplySalary" runat="server" OnClick="btnApplySalary_Click" Text="Apply Salary" Width="120px" CssClass="btn2" Visible="False" /></td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Confirmed">
                            <ContentTemplate>
                                <table style="width: 99%; text-align: left">
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label99" runat="server" Text="Employee Search"></asp:Label></td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtEmployeeSearch" runat="server" AutoPostBack="True" OnTextChanged="txtEmployeeSearch_TextChanged" placeholder="Employee Code" Width="350px"></asp:TextBox><cc1:AutoCompleteExtender ID="txtEmployeeSearch_AutoCompleteExtender" runat="server" CompletionListCssClass="autocomplete_completionListElement" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem2" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetEmpId" ServicePath="~/modules/Payroll/WebService.asmx" TargetControlID="txtEmployeeSearch"></cc1:AutoCompleteExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label1" runat="server" Text="From Date "></asp:Label></td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtFromDate0" runat="server" Width="178px"></asp:TextBox><cc1:CalendarExtender ID="txtFromDate0_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFromDate0"></cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label2" runat="server" Text="To Date"></asp:Label></td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtToDate" runat="server" Width="178px"></asp:TextBox><cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtToDate"></cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnConfirmationInfo" runat="server" OnClick="btnConfirmationInfo_Click" Text="Confirmation Information" Width="183px" CssClass="btn2" /><asp:Button ID="btnExportJoinResign" runat="server" CssClass="btn2" OnClick="btnExportJoinResign_Click" Text="Export" Width="183px" /><asp:Button ID="btnConfirmationReport" runat="server" OnClick="btnConfirmationReport_Click" Text="Confirmation Report" Width="183px" CssClass="btn2" /></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:GridView ID="grdGetJoiningNdResignation" runat="server" AutoGenerateColumns="False" OnRowDataBound="grdGetJoiningNdResignation_RowDataBound" Width="100%" OnRowCommand="grdGetJoiningNdResignation_RowCommand" OnRowDeleting="grdGetJoiningNdResignation_RowDeleting">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Row">
                                                        <ItemTemplate></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="EID" HeaderText="Emp Id" />
                                                    <asp:BoundField DataField="Employee Name" HeaderText="Name" />
                                                    <asp:BoundField DataField="Department" HeaderText="Department" />
                                                    <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                    <asp:BoundField DataField="Joining Date" HeaderText="Joining Date" />
                                                    <asp:BoundField DataField="Confirm Date" HeaderText="Confirm Date" />
                                                    <asp:BoundField DataField="Accept Date" HeaderText="Accept Date" />
                                                    <asp:BoundField DataField="Release Date" HeaderText="Release Date" />
                                                    <asp:BoundField DataField="Settlement Type" HeaderText="Status" />
                                                    <asp:CommandField ShowDeleteButton="True">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:CommandField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="Yet to confirm by date range">
                            <HeaderTemplate>Yet to confirm by date range</HeaderTemplate>
                            <ContentTemplate>
                                <table style="width: 99%; text-align: left">
                                    <tr>
                                        <td style="width: 78px">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 78px">
                                            <asp:Label ID="Label14" runat="server" Text="From Date "></asp:Label></td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtFromDateConfirm" runat="server" Width="178px"></asp:TextBox><cc1:CalendarExtender ID="txtFromDateConfirm_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFromDateConfirm"></cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 78px">
                                            <asp:Label ID="Label15" runat="server" Text="To Date"></asp:Label></td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtToDateConfirm" runat="server" Width="178px"></asp:TextBox><cc1:CalendarExtender ID="txtToDateConfirm_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtToDateConfirm"></cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 78px">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 78px">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnListBydaterange" runat="server" CssClass="btn2" OnClick="btnListBydaterange_Click" Text="List" Width="120px" /><asp:Button ID="btnExportEmployeeForConfirm" runat="server" CssClass="btn2" OnClick="btnExportEmployeeForConfirm_Click" Text="Export" Width="120px" /></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 78px">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:GridView ID="grdConfirmByDaterange" runat="server" Width="100%" AutoGenerateColumns="False" EmptyDataText="No Data Found">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Employee Id" HeaderText="Emp Id" />
                                                    <asp:BoundField DataField="Name" HeaderText="Name" />
                                                    <asp:BoundField DataField="Department" HeaderText="Department" />
                                                    <asp:BoundField DataField="Section" HeaderText="Section" />
                                                    <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                    <asp:BoundField DataField="Joining Date" HeaderText="Joining Date" />
                                                    <asp:BoundField DataField="Present Salary" HeaderText="Present Salary" />
                                                    <asp:BoundField DataField="Confirm Salary" HeaderText="Confirm Salary" />
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 78px">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 78px">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                    </cc1:TabContainer>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </asp:Panel>

</asp:Content>

