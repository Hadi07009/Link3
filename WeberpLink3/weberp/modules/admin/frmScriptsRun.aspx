<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmScriptsRun.aspx.cs" Inherits="modules_admin_frmScriptsRun" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <cc1:MessageBox ID="MessageBox1" runat="server" /> 
     <cc1:ConfirmBox ID="ConfirmBox1" runat="server" />  
    <asp:UpdatePanel ID="updpnl2" runat="server">
                    <ContentTemplate>

                        <table style="width:100%;">
                                        <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:TextBox ID="txtqry" runat="server" Height="500px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:TextBox ID="txtSecurity" runat="server" TextMode="Password"></asp:TextBox>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btn_reader" runat="server" OnClick="btn_reader_Click" Text="Reader" Width="134px" />
                                                &nbsp;&nbsp;
                                                <asp:Button ID="btn_nonqry" runat="server" OnClick="btn_nonqry_Click" OnClientClick="ShowConfirmBox(this,'Are you Sure Save Employee Information?'); return false;" Text="Execute Query" Width="124px" />
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbloutput" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gdata" runat="server">
                                        </asp:GridView>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>

                        </table>

                    </ContentTemplate>
                        <Triggers>
                             <asp:PostBackTrigger ControlID="btn_nonqry"/>   
                        </Triggers>
         </asp:UpdatePanel>
</asp:Content>
