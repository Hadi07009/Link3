<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmDepartmentSetup.aspx.cs" Inherits="modules_HRMS_Setup_frmDepartmentSetup"  EnableEventValidation="false" MaintainScrollPositionOnPostback="true"%>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="DEPARTMENT SETUP" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table cellpadding="0" cellspacing="0" style="text-align: center; width: 100%; border-left-width: 1px; border-bottom-width: 1px; border-right-width: 1px; vertical-align: middle;" id="TABLE2">

                    <tr>
                        <td style="height: 29px; text-align: center">
                            <asp:Label ID="lblmsg" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 32px; text-align: left">
                            <table style="width: 99%;">
                                <tr>
                                    <td style="width:150px">
                                        <asp:Label ID="Label3" runat="server" Text="Select Company"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td valign="middle">
                                        <asp:DropDownList ID="ddlcompany" runat="server" AutoPostBack="True" CssClass="tbl" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" Width="375px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td >
                                        <asp:Label ID="Label4" runat="server" Text="Office Location"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlOfficeLocation" runat="server" Width="375px" AutoPostBack="True" 
                                            OnSelectedIndexChanged="ddlOfficeLocation_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td >
                                        <asp:Label ID="Label5" runat="server" Text="Department Code"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="txtDepartmentCode" runat="server" Width="370px"></asp:TextBox>
                                        <ajaxToolkit:AutoCompleteExtender ID="txtDepartmentCode_AutoCompleteExtender" runat="server" DelimiterCharacters="" 
                                            Enabled="True" ServicePath="~/modules/Payroll/WebService.asmx" 
                                            MinimumPrefixLength="1" ServiceMethod="GetDepartmentCode" 
                                             TargetControlID="txtDepartmentCode">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td >
                                        <asp:Label ID="Label6" runat="server" Text="Department Name"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="txtDepartmentName" runat="server" Width="370px" AutoPostBack="True" OnTextChanged="txtDepartmentName_TextChanged"></asp:TextBox>
                                        <ajaxToolkit:AutoCompleteExtender ID="txtDepartmentName_AutoCompleteExtender" runat="server" DelimiterCharacters="" 
                                            Enabled="True" ServicePath="~/modules/Payroll/WebService.asmx" 
                                            MinimumPrefixLength="1" ServiceMethod="GetDepartmentName" 
                                            TargetControlID="txtDepartmentName">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td >
                                        <asp:Label ID="Label7" runat="server" Text="Department Location"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="txtDepartmentLocation" runat="server" Width="370px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td >
                                        <asp:Label ID="Label8" runat="server" Text="Head of Department"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="txtHeadOfDepartment" runat="server" AutoPostBack="True" Width="370px" OnTextChanged="txtHeadOfDepartment_TextChanged"></asp:TextBox>
                                        <ajaxToolkit:AutoCompleteExtender ID="txtHeadOfDepartment_AutoCompleteExtender" runat="server" BehaviorID="txtHeadOfDepartment_AutoCompleteExtender" Enabled="true" MinimumPrefixLength="1" ServiceMethod="GetEmpId" ServicePath="~/modules/Payroll/WebService.asmx" TargetControlID="txtHeadOfDepartment">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td >
                                        <asp:Label ID="Label9" runat="server" Text="Substitute Head of Department"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="txtSubstituteHOD" runat="server" AutoPostBack="True" Width="370px" OnTextChanged="txtSubstituteHOD_TextChanged"></asp:TextBox>
                                        <ajaxToolkit:AutoCompleteExtender ID="txtSubstituteHOD_AutoCompleteExtender" runat="server" 
                                            BehaviorID="txtSubstituteHOD_AutoCompleteExtender" 
                                            Enabled="true" 
                                            MinimumPrefixLength="1" 
                                            ServiceMethod="GetEmpId" 
                                            ServicePath="~/modules/Payroll/WebService.asmx" 
                                            TargetControlID="txtSubstituteHOD">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td >
                                        <asp:Label ID="Label10" runat="server" Text="Status"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlStatus" runat="server" Width="375px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td >&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnSaveDepartment" runat="server" OnClick="btnSaveDepartment_Click" Text="Save" Width="100px" />
                                        &nbsp;
                                    <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="100px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td >
                                        <asp:Button ID="btnExporttoExcel" runat="server" OnClick="btnExporttoExcel_Click" Text="Export to Excel" Width="150px" />
                                    </td>
                                    <td class="auto-style7"></td>
                                    <td class="auto-style7"></td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:GridView ID="grdShowDepartment" runat="server" AutoGenerateColumns="False" Width="100%" OnRowCommand="grdShowDepartment_RowCommand" OnRowDataBound="grdShowDepartment_RowDataBound" OnRowDeleting="grdShowDepartment_RowDeleting" GridLines="None">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL">
                                                    <ItemTemplate>
                                                        <%# Container.DisplayIndex + 1 %>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CompanyCode">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCompanyCode" runat="server" Text='<%# Bind("Dept_Comp_Code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Company">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCompany" runat="server" Text='<%# Bind("CompanyName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Office Location">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOfficeLocation" runat="server" Text='<%# Bind("Division_Master_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="OfficeLocationCode">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOfficeLocationCode" runat="server" Text='<%# Bind("Dept_Division_Code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Department Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDepartmentCode" runat="server" Text='<%# Bind("Dept_Code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Department Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDepartmetName" runat="server" Text='<%# Bind("Dept_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Department Location">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDepartmentLocation" runat="server" Text='<%# Bind("Dept_Loc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HODCode">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHODCode" runat="server" Text='<%# Bind("Dept_HOD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HOD">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHOD" runat="server" Text='<%# Bind("Dept_HODName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Substitute HODCode">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubstituteHODCode" runat="server" Text='<%# Bind("Dept_HOD2") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Substitute  HOD">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubstituteHOD" runat="server" Text='<%# Bind("Dept_HOD2Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("TxtStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatusValue" runat="server" Text='<%# Bind("T_C1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                                <asp:CommandField ShowSelectButton="True" />
                                                <asp:CommandField ShowDeleteButton="True" ButtonType="Button">
                                                    <ControlStyle BorderStyle="None" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:CommandField>
                                            </Columns>
                                            <PagerStyle BorderStyle="None" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td >&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveDepartment" />
            <asp:PostBackTrigger ControlID="btnExporttoExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
