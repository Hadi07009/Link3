<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="FrmEmployeeSettlement.aspx.cs" Inherits="modules_HRMS_Details_FrmEmployeeSettlement" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>

            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="Employee Settlement" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Select Company"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlcompany" runat="server" AutoPostBack="True" CssClass="tbl"
                                OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" Width="380px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Employee Code"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtEmployeeCode" runat="server" AutoPostBack="True" Width="375px" OnTextChanged="txtEmployeeCode_TextChanged"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="txtEmployeeCode_AutoCompleteExtender" runat="server"
                                BehaviorID="txtEmployeeCode_AutoCompleteExtender"
                                Enabled="true"
                                MinimumPrefixLength="1"
                                ServiceMethod="GetEmpId"
                                ServicePath="~/modules/Payroll/WebService.asmx"
                                TargetControlID="txtEmployeeCode">
                            </ajaxToolkit:AutoCompleteExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Panel ID="PanelForEmployeeDetails" runat="server" Height="215px" Width="100%">
                                <div style="text-align: center">
                                    <asp:Panel ID="PanelForDetailsHeader" runat="server" CssClass="cpHeaderContent" Height="15px" Width="100%">
                                        <asp:Label ID="lblIntoEmployeeDetailsHD" runat="server" Text="EMPLOYEE DETAILS"></asp:Label>
                                    </asp:Panel>
                                    <asp:Panel ID="PanelForEmployeeDetailsBody" runat="server" Height="200px" Width="100%">
                                        <table style="width: 100%; text-align: center">
                                            <tr>
                                                <td align="right">&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td align="left">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label6" runat="server" Text="Name&nbsp;"></asp:Label>
                                                </td>
                                                <td>:</td>
                                                <td align="left">
                                                    <asp:Label ID="lblEmployeeName" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label7" runat="server" Text="Office Location"></asp:Label>
                                                </td>
                                                <td>:</td>
                                                <td align="left">
                                                    <asp:Label ID="lblOfficeLocation" runat="server" Text="Label"></asp:Label>
                                                    <asp:Label ID="lblOfficeLocationCode" runat="server" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label8" runat="server" Text="Department"></asp:Label>
                                                </td>
                                                <td>:</td>
                                                <td align="left">
                                                    <asp:Label ID="lblEmployeeDepartment" runat="server" Text="Label"></asp:Label>
                                                    <asp:Label ID="lblDepartmentCode" runat="server" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label9" runat="server" Text="Section"></asp:Label>
                                                </td>
                                                <td>:</td>
                                                <td align="left">
                                                    <asp:Label ID="lblSection" runat="server" Text="Label"></asp:Label>
                                                    <asp:Label ID="lblSectionCode" runat="server" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label10" runat="server" Text="Designation"></asp:Label>
                                                </td>
                                                <td>:</td>
                                                <td align="left">
                                                    <asp:Label ID="lblDesignation" runat="server" Text="Label"></asp:Label>
                                                    <asp:Label ID="lblDesignationCode" runat="server" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label11" runat="server" Text="Joining Date"></asp:Label>
                                                </td>
                                                <td>:</td>
                                                <td align="left">
                                                    <asp:Label ID="lblJoiningDate" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
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
                                        ExpandedSize="200"
                                        AutoCollapse="False"
                                        AutoExpand="false"
                                        ScrollContents="true"
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
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label12" runat="server" Text="Settlement Type"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlSettlementType" runat="server" AutoPostBack="True" Width="380px">
                                <asp:ListItem Selected="True" Value="-1">--- Please Select ---</asp:ListItem>
                                <asp:ListItem>Resigned</asp:ListItem>
                                <asp:ListItem>Retired</asp:ListItem>
                                <asp:ListItem>Termination</asp:ListItem>
                                <asp:ListItem>Others</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label13" runat="server" Text="Notice Period [Days]"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtNoticePeriod" runat="server" AutoPostBack="True" OnDisposed="txtNoticePeriod_Disposed" onkeypress="return isNumberKey(event)" OnTextChanged="txtNoticePeriod_TextChanged" Style="text-align: center" Width="150px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label14" runat="server" Text="Acceptance Date"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <ew:CalendarPopup ID="calenderAcceptanceDate" runat="server" AutoPostBack="True" Culture="English (United Kingdom)"
                                Enabled="true" OnDateChanged="calenderAcceptanceDate_DateChanged" Width="117px">
                                <MonthHeaderStyle BackColor="#2A2965" />
                                <ButtonStyle CssClass="btn2" />
                            </ew:CalendarPopup>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label15" runat="server" Text="Relieving Date"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <ew:CalendarPopup ID="calenderRelievingDate" runat="server" AutoPostBack="True" Culture="English (United Kingdom)" Enabled="False" Width="117px">
                                <MonthHeaderStyle BackColor="#2A2965" />
                                <ButtonStyle CssClass="btn2" />
                            </ew:CalendarPopup>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label16" runat="server" Text="Compensation [Days]"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtCompensation" runat="server" onkeypress="return isNumberKey(event)" Style="text-align: center" Width="150px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label17" runat="server" Text="Comments or Reason"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtCommentsOrReason" runat="server" Width="630px" Height="75px" TextMode="MultiLine"></asp:TextBox>
                        </td>
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
                            <asp:Button ID="btnSaveEmployeeSettlement" runat="server" OnClick="btnSaveEmployeeSettlement_Click" Text="Save" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="grdShowEmpSettlementDetails" runat="server" AutoGenerateColumns="False"
                                OnRowCommand="grdShowEmpSettlementDetails_RowCommand" OnRowDataBound="grdShowEmpSettlementDetails_RowDataBound" OnRowDeleting="grdShowEmpSettlementDetails_RowDeleting" Width="100%" EmptyDataText="No Data Found">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <%# Container.DisplayIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmployeeID" runat="server" Text='<%# Bind("Emp_id") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpName" runat="server" Text='<%# Bind("Employeename") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Settlement Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSettlementType" runat="server" Text='<%# Bind("Settle_Typ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Acceptance Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAcceptanceDate" runat="server" Text='<%# Bind("Accp_Date", "{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Notice Period ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoticePeriod" runat="server" Text='<%# Bind("Notice_Prd") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Relieving Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRelievingDate" runat="server" Text='<%# Bind("Rel_Date", "{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Compensation [Days]">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCompensationDays" runat="server" Text='<%# Bind("COMPENSATION_DAY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comments ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCommentsReason" runat="server" Text='<%# Bind("Settle_Comm") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
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
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <table width="70%">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label18" runat="server" Text="Details Log From"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <ew:CalendarPopup ID="calenderDetailsLogFrom" runat="server" AutoPostBack="True" Culture="English (United Kingdom)" Enabled="true" Width="117px">
                                            <MonthHeaderStyle BackColor="#2A2965" />
                                            <ButtonStyle CssClass="btn2" />
                                        </ew:CalendarPopup>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label19" runat="server" Text="Details Log To"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <ew:CalendarPopup ID="calenderDetailsLogTo" runat="server" AutoPostBack="True" Culture="English (United Kingdom)" Enabled="true" Width="117px">
                                            <MonthHeaderStyle BackColor="#2A2965" />
                                            <ButtonStyle CssClass="btn2" />
                                        </ew:CalendarPopup>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnViewDetailsLog" runat="server" OnClick="btnViewDetailsLog_Click" Text="View Details Log" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="grdViewDetailsLog" runat="server" AutoGenerateColumns="False" EmptyDataText="No Data Found Between the given Date" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <%# Container.DisplayIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmployeeID" runat="server" Text='<%# Bind("Emp_id") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpName" runat="server" Text='<%# Bind("Employeename") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Settlement Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSettlementType" runat="server" Text='<%# Bind("Settle_Typ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Acceptance Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAcceptanceDate" runat="server" Text='<%# Bind("Accp_Date", "{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Notice Period ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoticePeriod" runat="server" Text='<%# Bind("Notice_Prd") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Relieving Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRelievingDate" runat="server" Text='<%# Bind("Rel_Date", "{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Compensation [Days]">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCompensationDays" runat="server" Text='<%# Bind("COMPENSATION_DAY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comments ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCommentsReason" runat="server" Text='<%# Bind("Settle_Comm") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserID" runat="server" Text='<%# Bind("EntryUserID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblActionType" runat="server" Text='<%# Bind("ActionType") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
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
            </asp:Panel>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveEmployeeSettlement" />
            <asp:PostBackTrigger ControlID="btnViewDetailsLog" />
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
