<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmDivisionSetup.aspx.cs"  Inherits="modules_HRMS_Setup_frmDivisionSetup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
           
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text=" OFFICE LOCATION SETUP" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left;">
                    
                    <tr>
                        <td style="width: 137px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 137px">
                            <asp:Label ID="Label2" runat="server" Text="Select Company"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlcompany" runat="server" AutoPostBack="True" CssClass="tbl" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" Width="380px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 137px">
                            <asp:Label ID="Label3" runat="server" Text="Office Location Code"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtDivisionCode" runat="server" Width="375px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style10" style="width: 137px; height: 27px;">
                            <asp:Label ID="Label4" runat="server" Text="Office Location Name"></asp:Label>
                        </td>
                        <td style="height: 27px">:</td>
                        <td style="height: 27px">
                            <asp:TextBox ID="txtDivisionName" runat="server" Width="375px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style10" style="width: 137px">
                            <asp:Label ID="Label5" runat="server" Text="Location"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtLocation" runat="server" Width="375px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style10" style="width: 137px">
                            <asp:Label ID="Label6" runat="server" Text="Address 1"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtAddress1" runat="server" Width="375px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style10" style="width: 137px">
                            <asp:Label ID="Label7" runat="server" Text="Address 2"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtAddress2" runat="server" Width="375px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style10" style="width: 137px">
                            <asp:Label ID="Label8" runat="server" Text="Address 3"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtAddress3" runat="server" Width="375px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style10" style="width: 137px">
                            <asp:Label ID="Label9" runat="server" Text="Status"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlStatus" runat="server" Width="380px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style10" style="width: 137px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnSaveDivision" runat="server" OnClick="btnSaveDivision_Click" Text="Save" Width="100px" />
                            &nbsp;
                                    <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style9" style="width: 137px">
                            <asp:Button ID="btnExporttoExcel" runat="server" OnClick="btnExporttoExcel_Click" Text="Export to Excel" Width="100px" />
                        </td>
                        <td class="auto-style7"></td>
                        <td class="auto-style7"></td>
                    </tr>
                    <tr>
                        <td class="auto-style7" colspan="3">
                            <asp:GridView ID="grdShowDivision" runat="server" Width="100%" 
                                AutoGenerateColumns="False" 
                                OnRowCommand="grdShowDivision_RowCommand" OnRowDeleting="grdShowDivision_RowDeleting" OnRowDataBound="grdShowDivision_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <%# Container.DisplayIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Office Location Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDivision_Master_Code" runat="server" Text='<%# Bind("Division_Master_Code") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Office Location Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDivision_Master_Name" runat="server" Text='<%# Bind("Division_Master_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Location">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDivision_Master_Loc" runat="server" Text='<%# Bind("Division_Master_Loc") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address 1">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDivision_Master_Address1" runat="server" Text='<%# Bind("Division_Master_Address1") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address 2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDivision_Master_Address2" runat="server" Text='<%# Bind("Division_Master_Address2") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address 3">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDivision_Master_Address3" runat="server" Text='<%# Bind("Division_Master_Address3") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
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
                        <td class="auto-style10" style="width: 137px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveDivision" />
            <asp:PostBackTrigger ControlID="btnExporttoExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

