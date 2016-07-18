<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmJobTypeSetup.aspx.cs" Inherits="modules_HRMS_Setup_frmJobTypeSetup" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>

            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="JOB TYPE SETUP" runat="server" Font-Bold="True" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
                    <tr>
                        <td style="width: 105px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 105px">
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
                        <td style="width: 105px">
                            <asp:Label ID="Label3" runat="server" Text="Job Type Code"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtJobTypeCode" runat="server" Width="375px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 105px">
                            <asp:Label ID="Label4" runat="server" Text="Job Type Title"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtJobTypeTitle" runat="server" Width="375px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 105px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 105px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnSaveJobType" runat="server" OnClick="btnSaveJobType_Click" Text="Save" Width="100px" />
                            &nbsp;
                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 105px"></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="grdShowJobType" runat="server" AutoGenerateColumns="False" Width="100%"
                                OnRowCommand="grdShowJobType_RowCommand" OnRowDeleting="grdShowJobType_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <%# Container.DisplayIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Job Type Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJobTypeCode" runat="server" Text='<%# Bind("JobTypeCode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Job Type Title">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJobTypeTitle" runat="server" Text='<%# Bind("JobTypeTitle") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
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
                        <td style="width: 105px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveJobType" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

