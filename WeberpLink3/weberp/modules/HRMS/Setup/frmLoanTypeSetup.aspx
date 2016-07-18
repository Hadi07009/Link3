<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmLoanTypeSetup.aspx.cs" Inherits="modules_HRMS_Setup_frmLoanTypeSetup" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="LOAN TYPE SETUP" runat="server" Font-Bold="True"  />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">

                <table style="width:99%;text-align:left">
                    <tr>
                        <td style="width: 79px">&nbsp;</td>
                        <td style="width: 2px">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 79px">
                            <asp:Label ID="Label1" runat="server" Text="Loan Code"></asp:Label>
                        </td>
                        <td style="width: 2px">:</td>
                        <td>
                            <asp:TextBox ID="txtLoanCode" runat="server" Width="375px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 79px">
                            <asp:Label ID="Label2" runat="server" Text="Loan Title"></asp:Label>
                        </td>
                        <td style="width: 2px">:</td>
                        <td>
                            <asp:TextBox ID="txtLoanTitle" runat="server" Width="375px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 79px">&nbsp;</td>
                        <td style="width: 2px">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 79px">&nbsp;</td>
                        <td style="width: 2px">&nbsp;</td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" OnClick="btnSave_Click" />
                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 79px">&nbsp;</td>
                        <td style="width: 2px">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="grdLoanType" runat="server" Width="100%" AutoGenerateColumns="False" OnRowCommand="grdLoanType_RowCommand" OnRowDeleting="grdLoanType_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <%# Container.DisplayIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Loan Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLoanCode" runat="server" Text='<%# Bind("loanCode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Loan Title">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLoanTitle" runat="server" Text='<%# Bind("loanTitle") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:CommandField ShowSelectButton="True" >
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:CommandField>
                                    <asp:CommandField ShowDeleteButton="True" >
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:CommandField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 79px">&nbsp;</td>
                        <td style="width: 2px">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>

                </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

