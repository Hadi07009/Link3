<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmPayrollHold.aspx.cs" Inherits="modules_HRMS_Payroll_frmPayrollHold" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<%@ Register assembly="eWorld.UI.Compatibility" namespace="eWorld.UI.Compatibility" tagprefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="updpnl2" runat="server">
                    <ContentTemplate>
                        
                        <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                            <asp:Label ID="lblleave" Text="Payroll Calculation Hold" runat="server" />
                        </asp:Panel>
                        <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                            <table style="width: 99%; text-align: left">
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style4" colspan="3">
                                        <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
                                            <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="Payroll Calculation Hold"><ContentTemplate><table style="width:100%;"><tr><td style="width:152px"><asp:Label ID="Label1" runat="server" Text="Employee Code"></asp:Label></td><td>:</td><td><asp:TextBox ID="txtEmployeeCode" runat="server" AutoPostBack="True" OnTextChanged="txtEmployeeCode_TextChanged" Width="380px"></asp:TextBox><asp:AutoCompleteExtender ID="txtEmployeeCode_AutoCompleteExtender" runat="server" BehaviorID="txtEmployeeCode_AutoCompleteExtender" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetEmpId" ServicePath="~/modules/Payroll/WebService.asmx" TargetControlID="txtEmployeeCode"></asp:AutoCompleteExtender></td></tr><tr><td colspan="3"><asp:Panel ID="PanelForEmployeeDetails" runat="server" Height="170px" Width="100%"><div style="text-align: center"><asp:Panel ID="PanelForDetailsHeader" runat="server" CssClass="cpHeaderContent" Height="15px" Width="100%"><asp:Label ID="lblIntoEmployeeDetailsHD" runat="server" Text="EMPLOYEE DETAILS"></asp:Label></asp:Panel><asp:Panel ID="PanelForEmployeeDetailsBody" runat="server" Height="155px" Width="100%"><table style="width: 50%;"><tr><td  style="width:145px; text-align: left"><asp:Label ID="Label20" runat="server" Text="Name"></asp:Label></td><td >:</td><td style="text-align: left"><asp:Label ID="lblEmployeeName" runat="server"></asp:Label></td></tr><tr><td  style="text-align: left" ><asp:Label ID="Label5" runat="server" Text="Office Location"></asp:Label></td><td  >:</td><td style="text-align: left"><asp:Label ID="lblOfficeLocation" runat="server" Text="Label"></asp:Label><asp:Label ID="lblOfficeLocationCode" runat="server" Visible="False"></asp:Label></td></tr><tr><td  style="text-align: left" ><asp:Label ID="Label6" runat="server" Text="Department"></asp:Label></td><td  >:</td><td style="text-align: left"><asp:Label ID="lblEmployeeDepartment" runat="server" Text="Label"></asp:Label><asp:Label ID="lblDepartmentCode" runat="server" Visible="False"></asp:Label></td></tr><tr><td  style="text-align: left" ><asp:Label ID="Label7" runat="server" Text="Section"></asp:Label></td><td >:</td><td style="text-align: left"><asp:Label ID="lblSection" runat="server" Text="Label"></asp:Label><asp:Label ID="lblSectionCode" runat="server" Visible="False"></asp:Label></td></tr><tr><td style="text-align: left" ><asp:Label ID="Label8" runat="server" Text="Designation"></asp:Label></td><td >:</td><td style="text-align: left"><asp:Label ID="lblDesignation" runat="server" Text="Label"></asp:Label><asp:Label ID="lblDesignationCode" runat="server" Visible="False"></asp:Label></td></tr><tr><td style="text-align: left" ><asp:Label ID="Label9" runat="server" Text="Joining Date"></asp:Label></td><td >:</td><td style="text-align: left"><asp:Label ID="lblJoiningDate" runat="server"></asp:Label></td></tr></table></asp:Panel><asp:CollapsiblePanelExtender ID="PanelForEmployeeDetailsBody_CollapsiblePanelExtender" runat="server" CollapseControlID="PanelForDetailsHeader" CollapsedImage="~/images/expand.jpg" CollapsedSize="2" CollapsedText="EMPLOYEE DETAILS" Enabled="True" ExpandControlID="PanelForDetailsHeader" ExpandedImage="~/images/collapse.jpg" ExpandedSize="155" ExpandedText="EMPLOYEE DETAILS" ImageControlID="Image1" TargetControlID="PanelForEmployeeDetailsBody" TextLabelID="lblIntoEmployeeDetailsHD"></asp:CollapsiblePanelExtender></div></asp:Panel></td></tr><tr><td><asp:Label ID="Label2" runat="server" Text="Reason of Payroll Hold"></asp:Label></td><td>:</td><td><asp:TextBox ID="txtReasonOfPayrollHold" runat="server" Height="50px" TextMode="MultiLine" Width="380px"></asp:TextBox>
                                                <asp:Label ID="lblForUpdate" runat="server" Visible="False"></asp:Label>
                                                </td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td><asp:Button ID="btnHoldPayroll" runat="server" Text="Hold Payroll" Width="150px" OnClick="btnHoldPayroll_Click" /></td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td colspan="3"><asp:GridView ID="grdPayrollHold" runat="server" Width="100%" AutoGenerateColumns="False" OnRowCommand="grdPayrollHold_RowCommand" OnRowDeleting="grdPayrollHold_RowDeleting" OnRowDataBound="grdPayrollHold_RowDataBound">
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
                                                            <asp:Label ID="lblEmpcode" runat="server" Text='<%# Bind("Empcode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Hold Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHolddate" runat="server" Text='<%# Bind("Holddate", "{0:d}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Reason ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="AutoNumber">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAutono" runat="server" Text='<%# Bind("Autono") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField ShowSelectButton="True" SelectText="Edit" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    </asp:CommandField>
                                                    <asp:CommandField ShowDeleteButton="True" DeleteText="Release Hold" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    </asp:CommandField>
                                                </Columns>
                                                </asp:GridView></td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td colspan="3" align="center"><asp:Panel ID="PanelPayrollHoldAll" runat="server" CssClass="cpHeaderContent" Height="15px" Width="100%"><asp:Label ID="Label21" runat="server" Text="List Of Employee Those Salary Is Hold"></asp:Label></asp:Panel></td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td colspan="3"><asp:GridView ID="grdPayrollHoldAll" runat="server" Width="100%" AutoGenerateColumns="False" EmptyDataText="No Data Found !">
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
                                                            <asp:Label ID="lblEmpcode" runat="server" Text='<%# Bind("Empcode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblemployeeName" runat="server" Text='<%# Bind("employeeName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Office Location">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblofficeLocation" runat="server" Text='<%# Bind("officeLocation") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Department">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDept_Name" runat="server" Text='<%# Bind("Dept_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Section">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSect_Name" runat="server" Text='<%# Bind("Sect_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Designation">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJobTitle" runat="server" Text='<%# Bind("JobTitle") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Hold Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHolddate" runat="server" Text='<%# Bind("Holddate", "{0:d}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Reason ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                </asp:GridView></td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr></table></ContentTemplate></asp:TabPanel>
                                            <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Payroll End Date"><ContentTemplate><table style="width:100%;"><tr><td style="width:152px"><asp:Label ID="Label22" runat="server" Text="Employee Code"></asp:Label></td><td>:</td><td><asp:TextBox ID="txtEmployeeCodeTab2" runat="server" AutoPostBack="True" OnTextChanged="txtEmployeeCodeTab2_TextChanged" Width="380px"></asp:TextBox><asp:AutoCompleteExtender ID="txtEmployeeCodeTab2_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetEmpId" ServicePath="~/modules/Payroll/WebService.asmx" TargetControlID="txtEmployeeCodeTab2"></asp:AutoCompleteExtender></td></tr><tr>
                                                <td colspan="3">
                                                 <asp:Panel ID="PanelEmployeeDetailsTab2" runat="server" Height="170px" Width="100%">
                                <div style="text-align: center">
                                    <asp:Panel ID="PanelHDTab2" runat="server" CssClass="cpHeaderContent" Height="15px" Width="100%">
                                        <asp:Label ID="lblHDTab2" runat="server" Text="EMPLOYEE DETAILS"></asp:Label>
                                    </asp:Panel>
                                    <asp:Panel ID="PanelBDTab2" runat="server" Height="155px" Width="100%">
                                        <table style="width: 50%;">
                                            <tr>
                                                <td  style="width:140px; text-align: left">
                                                    <asp:Label ID="Label4" runat="server" Text="Name"></asp:Label>
                                                </td>
                                                <td >:</td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblNameTab2" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td  style="text-align: left" >
                                                    <asp:Label ID="Label11" runat="server" Text="Office Location"></asp:Label>
                                                </td>
                                                <td  >:</td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblOfficeLocationTab2" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td  style="text-align: left" >
                                                    <asp:Label ID="Label14" runat="server" Text="Department"></asp:Label>
                                                </td>
                                                <td  >:</td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblDepartmentTab2" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td  style="text-align: left" >
                                                    <asp:Label ID="Label17" runat="server" Text="Section"></asp:Label>
                                                </td>
                                                <td >:</td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblSectionTab2" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left" >
                                                    <asp:Label ID="Label25" runat="server" Text="Designation"></asp:Label>
                                                </td>
                                                <td >:</td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblDesignationTab2" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left" >
                                                    <asp:Label ID="Label28" runat="server" Text="Joining Date"></asp:Label>
                                                </td>
                                                <td >:</td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblJoiningDateTab2" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" CollapseControlID="PanelHDTab2" CollapsedImage="~/images/expand.jpg" CollapsedSize="2" CollapsedText="EMPLOYEE DETAILS" Enabled="True" ExpandControlID="PanelHDTab2" ExpandedImage="~/images/collapse.jpg" ExpandedSize="155" ExpandedText="EMPLOYEE DETAILS" ImageControlID="Image1" TargetControlID="PanelBDTab2" TextLabelID="lblHDTab2">
                                    </asp:CollapsiblePanelExtender>
                                    
                                </div>
                            </asp:Panel>    
                                                </td></tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label23" runat="server" Text="Payroll End Date"></asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td>
                                                        <ew:CalendarPopup ID="calenderTargetDate" runat="server" AutoPostBack="True" Culture="en-GB" PostedDate="23/03/2015" SelectedDate="03/23/2015 10:18:23" SelectedValue="03/23/2015 10:18:23" VisibleDate="03/23/2015 10:18:23" Width="122px">
                                                            <MonthHeaderStyle BackColor="#2A2965" />
                                                            <ButtonStyle CssClass="btn2" />
                                                        </ew:CalendarPopup>
                                                    </td>
                                                </tr>
                                                <tr><td><asp:Label ID="Label24" runat="server" Text="Remarks"></asp:Label></td><td>:</td><td><asp:TextBox ID="txtRemarks" runat="server" Height="50px" TextMode="MultiLine" Width="380px"></asp:TextBox></td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td><asp:Button ID="btnPayrollEndDate" runat="server" Text="Submit" Width="150px" /></td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr></table></ContentTemplate></asp:TabPanel>
                                        </asp:TabContainer>
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
                             <%--<asp:PostBackTrigger ControlID="btnHoldPayroll"/>--%>   
                        </Triggers>
         </asp:UpdatePanel>
</asp:Content>
