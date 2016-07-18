<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmDisciplanaryAction.aspx.cs" Inherits="modules.HRMS.Details.modules_HRMS_Details_frmDisciplanaryAction" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="Disciplinary Action" runat="server" />

            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">

                <table style="width: 99%; text-align: left">
                    <tr>
                        <td style="width: 162px">
                            <asp:Label ID="Label3" runat="server" Text="Employee Code"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtEmployeeCode" runat="server" AutoPostBack="True" Width="350px" OnTextChanged="txtEmployeeCode_TextChanged"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="txtEmployeeCode_AutoCompleteExtender"
                                runat="server" BehaviorID="txtEmployeeCode_AutoCompleteExtender"
                                CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                CompletionListItemCssClass="autocomplete_listItem2"
                                Enabled="true" MinimumPrefixLength="1"
                                ServiceMethod="GetEmpId"
                                ServicePath="~/modules/Payroll/WebService.asmx"
                                TargetControlID="txtEmployeeCode">
                            </ajaxToolkit:AutoCompleteExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Panel ID="PanelForEmployeeDetails" runat="server" Height="170px" ScrollBars="None" Width="100%">
                                <div style="text-align: center">
                                    <asp:Panel ID="PanelForDetailsHeader" runat="server" CssClass="cpHeaderContent" Height="15px" ScrollBars="None" Width="100%">
                                        <asp:Label ID="lblIntoEmployeeDetailsHD" runat="server" Text="EMPLOYEE DETAILS"></asp:Label>
                                    </asp:Panel>
                                    <asp:Panel ID="PanelForEmployeeDetailsBody" runat="server" Height="155px" Width="100%" ScrollBars="None">

                                        <div style="float: left; width: 70%">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 151px; text-align: left">
                                                        <asp:Label ID="Label20" runat="server" Text="Name"></asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="lblEmployeeName" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left; width: 151px;">
                                                        <asp:Label ID="Label5" runat="server" Text="Office Location"></asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="lblOfficeLocation" runat="server" Text="Label"></asp:Label>
                                                        <asp:Label ID="lblOfficeLocationCode" runat="server" Visible="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left; width: 151px;">
                                                        <asp:Label ID="Label6" runat="server" Text="Department"></asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="lblEmployeeDepartment" runat="server" Text="Label"></asp:Label>
                                                        <asp:Label ID="lblDepartmentCode" runat="server" Visible="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left; width: 151px;">
                                                        <asp:Label ID="Label7" runat="server" Text="Section"></asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="lblSection" runat="server" Text="Label"></asp:Label>
                                                        <asp:Label ID="lblSectionCode" runat="server" Visible="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left; width: 151px;">
                                                        <asp:Label ID="Label8" runat="server" Text="Designation"></asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="lblDesignation" runat="server" Text="Label"></asp:Label>
                                                        <asp:Label ID="lblDesignationCode" runat="server" Visible="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left; width: 151px;">
                                                        <asp:Label ID="Label9" runat="server" Text="Joining Date"></asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="lblJoiningDate" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="float: right; width: 25%; vertical-align: top">
                                            <table style="width: 40%">
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <asp:Label ID="lblImage" runat="server" BorderColor="Black" BorderWidth="1px" Font-Bold="True" Font-Italic="True" Font-Size="X-Large" ForeColor="Red" Height="100%" Style="text-align: center; vertical-align: middle" Width="100%"> <br /> Photo
                                             <br />  Not <br />  Available  
                                             
                                                        </asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                        </div>

                                    </asp:Panel>
                                    <ajaxToolkit:CollapsiblePanelExtender ID="PanelForEmployeeDetailsBody_CollapsiblePanelExtender" runat="server" Enabled="True"
                                        TargetControlID="PanelForEmployeeDetailsBody"
                                        CollapseControlID="PanelForDetailsHeader"
                                        ExpandControlID="PanelForDetailsHeader"
                                        Collapsed="false"
                                        TextLabelID="lblIntoEmployeeDetailsHD"
                                        CollapsedText="EMPLOYEE DETAILS"
                                        ExpandedText="EMPLOYEE DETAILS"
                                        CollapsedSize="2"
                                        ExpandedSize="155"
                                        AutoCollapse="False"
                                        AutoExpand="false"
                                        ScrollContents="false"
                                        ImageControlID="Image1"
                                        ExpandedImage="~/images/collapse.jpg"
                                        CollapsedImage="~/images/expand.jpg"
                                        ExpandDirection="Vertical">
                                    </ajaxToolkit:CollapsiblePanelExtender>
                                </div>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 162px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 162px">
                            <asp:Label ID="Label21" runat="server" Text="Topic"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtTopic" runat="server" Width="350px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 162px">
                            <asp:Label ID="Label22" runat="server" Text="Date"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="popupJoiningDate" runat="server" placeholder="Date" Width="350px"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="popupJoiningDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="popupJoiningDate">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 162px">
                            <asp:Label ID="Label23" runat="server" Text="Inquiry"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtInquary" runat="server" Width="350px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 162px">
                            <asp:Label ID="Label24" runat="server" Text="Inquiry-Recommendation"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtInquaryRecomondation" runat="server" Width="350px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 162px">
                            <asp:Label ID="Label25" runat="server" Text="Action"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtAction" runat="server" Width="350px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 162px">
                            <asp:Label ID="Label26" runat="server" Text="Remarks"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtRemarks" runat="server" Width="350px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 162px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" Width="100px" />
                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="100px" />
                            <asp:Button ID="btnPreviewAll" runat="server" OnClick="btnPreviewAll_Click" Text="Preview All" Width="100px" />
                            <asp:Label ID="lblUpdate" runat="server" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 162px">
                            <asp:Button ID="btnExporttoExcel" runat="server" OnClick="btnExporttoExcel_Click" Text="Export to Excel" Width="100px" />
                            <asp:Button ID="btnPreview" runat="server" OnClick="btnPreview_Click" Text="Preview" Width="100px" />
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="grdDisciplanary" runat="server" Width="100%" AutoGenerateColumns="False" OnRowCommand="grdDisciplanary_RowCommand" OnRowDataBound="grdDisciplanary_RowDataBound" OnRowDeleting="grdDisciplanary_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSL" runat="server" Text='<%# Bind("rowNmber") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Case Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCaseCode" runat="server" Text='<%# Bind("caseCode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Case Topic">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCaseTopic" runat="server" Text='<%# Bind("caseTopic") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Case Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCaseDate" runat="server" Text='<%# Bind("caseDate", "{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inquiry">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInquary" runat="server" Text='<%# Bind("inquary") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inquiry Recommendation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInquaryRecomondation" runat="server" Text='<%# Bind("inquaryRecomondation") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Case Action">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCaseAction" runat="server" Text='<%# Bind("caseAction") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("remarks") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmployeeCode" runat="server" Text='<%# Bind("employeeCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowSelectButton="True">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:CommandField>
                                    <asp:CommandField ShowDeleteButton="True">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:CommandField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 162px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 162px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>

            </asp:Panel>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnClear" />
            <asp:PostBackTrigger ControlID="btnExporttoExcel" />
            <asp:PostBackTrigger ControlID="btnPreviewAll" />
            <asp:PostBackTrigger ControlID="btnPreview" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
