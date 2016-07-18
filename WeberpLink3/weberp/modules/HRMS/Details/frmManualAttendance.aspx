<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmManualAttendance.aspx.cs" Inherits="modules_HRMS_Details_frmManualAttendance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="TimePicker" namespace="MKB.TimePicker" tagprefix="MKB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="Manual Attendance Entry" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
                    <tr>
                        <td colspan="3">
                            <table style="width: 50%; text-align:left">
                                <tr>
                                    <td >
                                        <asp:Label ID="Label22" runat="server" Text="Select Company"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlcompany" runat="server" AutoPostBack="True" CssClass="tbl" 
                                            OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" Width="380px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <div style="width: 1000px">
                                <div style="width: 500px; float: left">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Text="Date"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td>
                                                <ew:CalendarPopup ID="calenderTargetDate" runat="server" AutoPostBack="True" Culture="English (United Kingdom)" Enabled="true" Width="203px">
                                                    <MonthHeaderStyle BackColor="#2A2965" />
                                                    <ButtonStyle CssClass="btn2" />
                                                </ew:CalendarPopup>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" Text="Employee Code"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td>
                                                <asp:TextBox ID="txtEmployeeCode" runat="server" AutoPostBack="True" OnTextChanged="txtEmployeeCode_TextChanged" Width="350px"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="txtEmployeeCode_AutoCompleteExtender" runat="server" BehaviorID="txtEmployeeCode_AutoCompleteExtender" Enabled="true" MinimumPrefixLength="1" ServiceMethod="GetEmpId" ServicePath="~/modules/Payroll/WebService.asmx" TargetControlID="txtEmployeeCode">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="width: 500px; float: right;">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" Text="Date From" Visible="False"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <ew:CalendarPopup ID="calenderFromDate" runat="server" AutoPostBack="True" Culture="English (United Kingdom)" Enabled="true" Width="100px" Visible="False">
                                                    <MonthHeaderStyle BackColor="#2A2965" />
                                                    <ButtonStyle CssClass="btn2" />
                                                </ew:CalendarPopup>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" Text="Date To" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <ew:CalendarPopup ID="calenderToDate" runat="server" AutoPostBack="True" Culture="English (United Kingdom)" Enabled="true" Width="100px" Visible="False">
                                                    <MonthHeaderStyle BackColor="#2A2965" />
                                                    <ButtonStyle CssClass="btn2" />
                                                </ew:CalendarPopup>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 31px">
                                                <asp:Label ID="Label9" runat="server" Text="Department" Visible="False"></asp:Label>
                                            </td>
                                            <td style="height: 31px">&nbsp;</td>
                                            <td style="height: 31px">
                                                <asp:DropDownList ID="ddlDepartment" runat="server" Width="100px" Visible="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 31px">
                                                <asp:Label ID="Label10" runat="server" Text="Employee Code" Visible="False"></asp:Label>
                                            </td>
                                            <td style="height: 31px">
                                                <asp:TextBox ID="txtEmployeeCodeForSearch" runat="server" AutoPostBack="True" OnTextChanged="txtEmployeeCodeForSearch_TextChanged" Width="50px" Visible="False"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="txtEmployeeCodeForSearch_AutoCompleteExtender" runat="server" BehaviorID="txtEmployeeCodeForSearch_AutoCompleteExtender" Enabled="true" MinimumPrefixLength="1" ServiceMethod="GetEmpId" ServicePath="~/payroll/WebService.asmx" TargetControlID="txtEmployeeCodeForSearch">
                                                </cc1:AutoCompleteExtender>
                                                &nbsp;<asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" Visible="False" Width="50px" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Panel ID="PanelForEmployeeDetails" runat="server" Height="90px" ScrollBars="None" Width="100%">
                                <div style="text-align: center">
                                    <asp:Panel ID="PanelForDetailsHeader" runat="server" CssClass="cpHeaderContent" Height="15px" ScrollBars="None" Width="100%">
                                        <asp:Label ID="lblIntoEmployeeDetailsHD" runat="server" Text="EMPLOYEE DETAILS"></asp:Label>
                                    </asp:Panel>
                                    <asp:Panel ID="PanelForEmployeeDetailsBody" runat="server" Height="80px" Width="100%" ScrollBars="None">
                                        <table style="width: 100%; text-align:left">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label11" runat="server" Text="Name&nbsp;"></asp:Label>
                                                </td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="lblEmployeeName" runat="server"></asp:Label>
                                                </td>
                                                <td style="width:100px; text-align:left">
                                                    <asp:Label ID="Label12" runat="server" Text="Office Location"></asp:Label>
                                                </td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="lblOfficeLocation" runat="server" Text="Label"></asp:Label>
                                                    <asp:Label ID="lblOfficeLocationCode" runat="server" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label13" runat="server" Text="Department"></asp:Label>
                                                </td>
                                                <td>:</td>
                                                <td><asp:Label ID="lblEmployeeDepartment" runat="server" Text="Label"></asp:Label>
                                                    <asp:Label ID="lblDepartmentCode" runat="server" Visible="False"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label14" runat="server" Text="Section"></asp:Label>
                                                </td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="lblSection" runat="server" Text="Label"></asp:Label>
                                                    <asp:Label ID="lblSectionCode" runat="server" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label15" runat="server" Text="Designation"></asp:Label>
                                                </td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="lblDesignation" runat="server" Text="Label"></asp:Label>
                                                    <asp:Label ID="lblDesignationCode" runat="server" Visible="False"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label16" runat="server" Text="Joining Date"></asp:Label>
                                                </td>
                                                <td>:</td>
                                                <td>
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
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <table style="width: 100%;">
                                <tr>
                                    <td colspan="3">
                                        <table style="width:50%;">
                                            <tr>
                                                <td style="width:100px; text-align:left">
                                                    <asp:Label ID="Label20" runat="server" Text="Intime"></asp:Label>
                                                </td>
                                                <td>:</td>
                                                <td style="width:130px;text-align:left">
                                                    <MKB:TimeSelector ID="timeoffIntime" runat="server" AmPm="AM" DisplaySeconds="False" Hour="9">
                                                    </MKB:TimeSelector>
                                                </td>
                                                <td style="width:110px;text-align:left">
                                                    &nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td style="width:110px;text-align:left">
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="width:100px; text-align:left">
                                                    <asp:Label ID="Label21" runat="server" Text="Outtime"></asp:Label>
                                                </td>
                                                <td>:</td>
                                                <td style="width:110px;text-align:left">
                                                    <MKB:TimeSelector ID="timeoffOuttime" runat="server" AmPm="PM" DisplaySeconds="False" Hour="6">
                                                    </MKB:TimeSelector>
                                                </td>
                                                <td style="width:110px;text-align:left">
                                                    <asp:Label ID="lblHour" runat="server" Visible="False"></asp:Label>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td style="width:110px;text-align:left">&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label19" runat="server" Text="Remarks"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="txtRemarks" runat="server" Height="35px" Style="margin-top: 0px" TextMode="MultiLine" Width="600px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" Width="100px" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnClearAll" runat="server" OnClick="btnClearAll_Click" Text="Clear All" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="grdGetAttendanceRecord" runat="server" AutoGenerateColumns="False" EmptyDataText="No Data Found " OnRowCommand="grdGetAttendanceRecord_RowCommand" OnRowDeleting="grdGetAttendanceRecord_RowDeleting" Width="100%">
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
                                            <asp:Label ID="lblAtnd_Det_Emp_Id" runat="server" Text='<%# Bind("Atnd_Det_Emp_Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmp_Name" runat="server" Text='<%# Bind("Emp_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAtnd_det_date" runat="server" Text='<%# Bind("Atnd_det_date", "{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Intime">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAtnd_det_intime" runat="server" Text='<%# Bind("Atnd_det_intime") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Outtime">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAtnd_det_outtime" runat="server" Text='<%# Bind("Atnd_det_outtime") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Hour">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAtnd_det_hrs" runat="server" Text='<%# Bind("Atnd_det_hrs") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAtnd_det_rmks" runat="server" Text='<%# Bind("Atnd_det_rmks") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowSelectButton="True" />
                                    <asp:CommandField ShowDeleteButton="True" />
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
            
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
