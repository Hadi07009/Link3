<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmDesignationSetup.aspx.cs" Inherits="modules_HRMS_Setup_frmDesignationSetup" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:ConfirmBox ID="ConfirmBox1" runat="server" />
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>

            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="DESIGNATION SETUP" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">

                    <tr>
                        <td class="auto-style1" style="width: 107px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style1" style="width: 107px">
                            <asp:Label ID="Label2" runat="server" Text="Select Company"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlcompany" runat="server" AutoPostBack="True" CssClass="tbl"
                                OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" Width="380px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1" style="width: 107px">
                            <asp:Label ID="Label6" runat="server" Text="Designation Code"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtDesignationCode" runat="server" onkeypress="return IsMaxLength(this, 15);" Width="374px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1" style="width: 107px">
                            <asp:Label ID="Label3" runat="server" Text="Employee Status"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlEmployeeType" runat="server" Width="380px" AutoPostBack="True" OnSelectedIndexChanged="ddlEmployeeType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1" style="width: 107px">
                            <asp:Label ID="Label4" runat="server" Text="Mng. Level"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlMngLevel" runat="server" Width="380px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1" style="width: 107px">
                            <asp:Label ID="Label7" runat="server" Text="Designation"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtDesignation" runat="server" Width="374px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1" style="width: 107px">
                            <asp:Label ID="Label10" runat="server" Text="Status"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlStatus" runat="server" Width="380px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1" style="width: 107px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnSaveDesignation" runat="server" OnClick="btnSaveDesignation_Click" Text="Save" Width="100px" />
                            &nbsp;
                                    <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1" style="width: 107px">
                            <asp:Button ID="btnExporttoExcel" runat="server" Text="Export to Excel" Width="100px" OnClick="btnExporttoExcel_Click" />
                        </td>
                        <td class="auto-style7"></td>
                        <td class="auto-style7">
                            <asp:DropDownList ID="ddlJobType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlJobType_SelectedIndexChanged" Visible="False" Width="380px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style7" colspan="3">
                            <asp:GridView ID="grdShowDesignation" runat="server" AutoGenerateColumns="False" Width="100%" OnRowCommand="grdShowDesignation_RowCommand" OnRowDeleting="grdShowDesignation_RowDeleting" OnRowDataBound="grdShowDesignation_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <%# Container.DisplayIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Job Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJobType" runat="server" Text='<%# Bind("JobTypeTitle") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mng. Level">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMngLevel" runat="server" Text='<%# Bind("MngLevel") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmployeeType" runat="server" Text='<%# Bind("EmpType") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDesignationCode" runat="server" Text='<%# Bind("JobCode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDesignation" runat="server" Text='<%# Bind("JobTitle") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="JobType">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJobTypeCode" runat="server" Text='<%# Bind("JobType") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EmpType">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpTypeCode" runat="server" Text='<%# Bind("EmpTypeCode") %>'></asp:Label>
                                        </ItemTemplate>
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
                        <td class="auto-style1" style="width: 107px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExporttoExcel" />
            <asp:PostBackTrigger ControlID="btnSaveDesignation" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">

        function IsMaxLength(obj, MaxLen) {
            return (obj.value.length < MaxLen);
        }
    </script>

</asp:Content>

