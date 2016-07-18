<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_user_role_assign.aspx.cs" Inherits="frm_user_role_assign"  Title=""   ValidateRequest="false" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="updpnl" runat="server">
    <ContentTemplate>
    
    <table class="tblmas" id="tblmaster" runat="server" style="text-align: center" width="100%">
        <tr>
            <td style="width: 174px; height: 29px">
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="1" style="text-align: center">
                USER ROLE ASSIGN SCREEN</td>
        </tr>
        <tr>
            <td style="width: 174px; height: 22px">
            </td>
        </tr>
        <tr>
            <td style="width: 174px; text-align: center">
                <table style="width: 578px" >
                    <tr>
                        <td style="width: 136px; text-align: left">
                            USER</td>
                        <td style="width: 15px">
                            :</td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlusers" runat="server" AutoPostBack="True" 
                                CssClass="txtbox" onselectedindexchanged="ddlusers_SelectedIndexChanged" 
                                Width="400px">
                            </asp:DropDownList>
                            </td>
                    </tr>
                    <tr>
                        <td style="width: 136px; text-align: left">
                            ROLE TYPE</td>
                        <td style="width: 15px">
                            &nbsp;</td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlroletype" runat="server" CssClass="txtbox" 
                                Width="400px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 136px; text-align: left">
                            ROLE AS</td>
                        <td style="width: 15px">
                            :</td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlrole" runat="server" CssClass="txtbox" Width="400px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 136px; text-align: left">
                            PLANT</td>
                        <td style="width: 15px">
                            :</td>
                        <td style="text-align: left">
                            <asp:CheckBoxList ID="chkPlantlist" runat="server" Width="329px">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 136px; text-align: left">
                            PRIORITY</td>
                        <td style="width: 15px">
                            :</td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlprio" runat="server" CssClass="txtbox" Width="100px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 136px; text-align: right; height: 26px;">
                        </td>
                        <td style="width: 15px; height: 26px;">
                        </td>
                        <td style="height: 26px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 136px; text-align: right">
                        </td>
                        <td style="width: 15px">
                        </td>
                        <td style="text-align: left">
                            <asp:Button ID="btnAdd" runat="server" CssClass="btn2" Text="ADD" 
                                Width="104px" OnClick="btnAdd_Click" />
                            &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="btn2" Text="UPDATE" 
                                Width="103px" onclick="btnupdate_Click" />
                            </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 174px; height: 28px">
            </td>
        </tr>
        <tr>
            <td style="width: 174px; text-align: center">
                <asp:GridView ID="gdUser" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                    OnSelectedIndexChanged="gdUser_SelectedIndexChanged" OnRowDeleting ="gdUser_RowDeleting" PageSize="100" SkinID="GridView"
                    Width="682px" BackColor="White" BorderColor="#41519A" BorderStyle="Solid" BorderWidth="1px">
                    <FooterStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#41519A" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="LightBlue" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="Lavender" />
                    <EditRowStyle BackColor="#2461BF" />
                     <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="width: 174px; height: 59px; text-align: center;">
            </td>
        </tr>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

