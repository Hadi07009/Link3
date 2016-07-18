<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmDocumentUpload.aspx.cs" Inherits="modules_HRMS_Details_frmDocumentUpload" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <script src="http://code.jquery.com/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../../../script/jquery.MultiFile.js" type="text/javascript"></script>
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <cc1:ConfirmBox ID="ConfirmBox1" runat="server" />    

    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="Document Upload" runat="server" />

            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
                    <tr>
                        <td style="width: 103px">
                            &nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 103px">
                            <asp:Label ID="Label1" runat="server" Text="Document Type"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlDocumentType" runat="server" Width="350px" AutoPostBack="True" OnSelectedIndexChanged="ddlDocumentType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 103px">
                            <asp:Label ID="Label2" runat="server" Text="Description"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="345px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 103px">
                            <asp:Label ID="Label3" runat="server" Text="Choose File"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:FileUpload ID="file_upload" runat="server" class="multi" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 103px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 103px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Width="100px" />
                            <asp:Button ID="btnActiveDocument" runat="server" OnClick="btnActiveDocument_Click" Text="Active Document" />
                            <asp:Button ID="btnInactiveDocument" runat="server" Text="Inactive Document" OnClick="btnInactiveDocument_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 103px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="grdActiveDocument" runat="server" Width="100%" AutoGenerateColumns="False" OnRowCommand="grdActiveDocument_RowCommand" OnRowDataBound="grdActiveDocument_RowDataBound" OnRowDeleting="grdActiveDocument_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <%# Container.DisplayIndex + 1 %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="lblReferenceNo" runat="server" Text='<%# Bind("ReferenceNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Document Name(Active)">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("documentContent") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Document Type">
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("documentTypeText") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField DeleteText="Inactive" ShowDeleteButton="True" HeaderText="Action" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="grdInactiveDocument" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDataBound="grdInactiveDocument_RowDataBound" OnRowCommand="grdInactiveDocument_RowCommand" OnRowDeleting="grdInactiveDocument_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <%# Container.DisplayIndex + 1 %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="lblReferenceNo" runat="server" Text='<%# Bind("ReferenceNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Document Name(Inactive)">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("documentContent") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Document Type">
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("documentTypeText") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField DeleteText="Active" ShowDeleteButton="True" HeaderText="Action" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 103px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 103px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 103px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnActiveDocument" />
            <asp:PostBackTrigger ControlID="btnInactiveDocument" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
