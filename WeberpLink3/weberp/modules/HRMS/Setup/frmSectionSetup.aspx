<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmSectionSetup.aspx.cs" Inherits="modules_HRMS_Setup_frmSectionSetup" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="SECTION SETUP" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
                    <tr>
                        <td style="width:160px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td >
                            <asp:Label ID="Label2" runat="server" Text="Select Company"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlcompany" runat="server" AutoPostBack="True" CssClass="tbl" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" Width="380px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <asp:Label ID="Label3" runat="server" Text="Office Location"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlOfficeLocation" runat="server" Width="380px" AutoPostBack="True" 
                                OnSelectedIndexChanged="ddlOfficeLocation_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <asp:Label ID="Label4" runat="server" Text="Department Code"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlDepartmentCode" runat="server" Width="380px" AutoPostBack="True" 
                                OnSelectedIndexChanged="ddlDepartmentCode_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <asp:Label ID="Label5" runat="server" Text="Section Code"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtSectionCode" runat="server" Width="374px"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="txtSectionCode_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True"
                                 ServicePath="~/modules/Payroll/WebService.asmx"
                                MinimumPrefixLength="1" ServiceMethod="GetSectionCode"  
                                TargetControlID="txtSectionCode">
                            </ajaxToolkit:AutoCompleteExtender>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <asp:Label ID="Label6" runat="server" Text="Section Name"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtSectionName" runat="server" Width="374px" AutoPostBack="True" OnTextChanged="txtSectionName_TextChanged"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="txtSectionName_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True"
                                 ServicePath="~/modules/Payroll/WebService.asmx" 
                                MinimumPrefixLength="1" ServiceMethod="GetSectionName" 
                                TargetControlID="txtSectionName">
                            </ajaxToolkit:AutoCompleteExtender>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <asp:Label ID="Label7" runat="server" Text="Head of Section "></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtHeadOfSection" runat="server" AutoPostBack="True" OnTextChanged="txtHeadOfSection_TextChanged" Width="374px"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="txtHeadOfSection_AutoCompleteExtender" runat="server" 
                                BehaviorID="txtHeadOfSection_AutoCompleteExtender" 
                                Enabled="true" 
                                MinimumPrefixLength="1" 
                                ServiceMethod="GetEmpId" 
                                ServicePath="~/payroll/WebService.asmx" 
                                TargetControlID="txtHeadOfSection">
                            </ajaxToolkit:AutoCompleteExtender>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <asp:Label ID="Label8" runat="server" Text="Substitute Head of Section "></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtSubstituteHOS" runat="server" AutoPostBack="True" 
                                OnTextChanged="txtSubstituteHOS_TextChanged" Width="374px"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="txtSubstituteHOS_AutoCompleteExtender" runat="server" 
                                BehaviorID="txtSubstituteHOS_AutoCompleteExtender" 
                                Enabled="true" 
                                MinimumPrefixLength="1" 
                                ServiceMethod="GetEmpId" 
                                ServicePath="~/payroll/WebService.asmx" 
                                TargetControlID="txtSubstituteHOS">
                            </ajaxToolkit:AutoCompleteExtender>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <asp:Label ID="Label10" runat="server" Text="Status"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlStatus" runat="server" Width="380px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnSaveSection" runat="server" OnClick="btnSaveSection_Click" Text="Save" Width="100px" />
                            &nbsp;
                                    <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <asp:Button ID="btnExporttoExcel" runat="server" OnClick="btnExporttoExcel_Click" Text="Export to Excel" Width="100px" />
                        </td>
                        <td></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="grdShowSection" runat="server" AutoGenerateColumns="False" Width="100%" 
                                OnRowCommand="grdShowSection_RowCommand" OnRowDataBound="grdShowSection_RowDataBound" 
                                OnRowDeleting="grdShowSection_RowDeleting">

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
                                            <asp:Label ID="lblCompanyCode" runat="server" Text='<%# Bind("Sect_Comp_Code") %>'></asp:Label>
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
                                            <asp:Label ID="lblOfficeLocationCode" runat="server" Text='<%# Bind("Sect_Div_Code") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDepartmentCode" runat="server" Text='<%# Bind("Sect_Dept_Code") %>'></asp:Label>
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
                                    <asp:TemplateField HeaderText="Section Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSectionCode" runat="server" Text='<%# Bind("Sect_Code") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Section Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSectionName" runat="server" Text='<%# Bind("Sect_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sect_Head1Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSect_Head1Code" runat="server" Text='<%# Bind("Sect_Head1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HOS">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHOS" runat="server" Text='<%# Bind("Sect_Head1Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sect_Head2Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSect_Head2Code" runat="server" Text='<%# Bind("Sect_Head2") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Substitute HOS">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubstituteHOS" runat="server" Text='<%# Bind("Sect_Head2Name") %>'></asp:Label>
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
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>         
    
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveSection" />
            <asp:PostBackTrigger ControlID="btnExporttoExcel" />
        </Triggers>

    </asp:UpdatePanel>
</asp:Content>

