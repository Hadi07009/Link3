<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_app_flow_det.aspx.cs" Inherits="frm_app_flow_det" Title=""   EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="update" runat="server">
<ContentTemplate>
<table id="tblmaster" runat="server" class="tblmas" style="width: 100%">
        <tr>
            <td style="height: 22px">
            </td>
            <td style="height: 22px">
            </td>
            <td style="height: 22px">
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="3" style="text-align: center">
                APPROVAL FLOW DETAIL SCREEN&nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 25px; text-align: center">
                <table style="width: 84%;">
                    <tr>
                        <td style="text-align: left; width: 102px">
                            FLOW ID</td>
                        <td style="width: 16px">
                            :</td>
                        <td style="width: 307px; text-align: left">
                            <asp:TextBox ID="txtid" runat="server" CssClass="txtbox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 102px">
                            FLOW DESC</td>
                        <td style="width: 16px">
                            :</td>
                        <td style="width: 307px; text-align: left">
                            <asp:TextBox ID="txtdesc" runat="server" CssClass="txtbox" Width="370px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 102px; height: 22px">
                        </td>
                        <td style="width: 16px; height: 22px">
                        </td>
                        <td style="width: 307px; text-align: left; height: 22px">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 102px">
                            &nbsp;</td>
                        <td style="width: 16px">
                            &nbsp;</td>
                        <td style="width: 307px; text-align: left">
                            <asp:Button ID="btnadd" runat="server" CssClass="btn2" Text="Add/Edit" 
                                Width="112px" onclick="btnadd_Click" />
                            <asp:Button ID="btnremove" runat="server" CssClass="btn2" Text="Remove" 
                                Width="112px" onclick="btnremove_Click" />
                        </td>
                    </tr>
                </table>
                </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 34px; text-align: left">
                
                
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 22px; text-align: left">
                
                
                <asp:GridView ID="gdapp" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"                    
                    OnRowDataBound="gdapp_RowDataBound" PageSize="100" SkinID="GridView"
                    Width="682px" BackColor="White" BorderColor="#41519A" BorderStyle="Solid" 
                    BorderWidth="1px" onselectedindexchanged="gdapp_SelectedIndexChanged">
                    <FooterStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#41519A" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="LightBlue" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="Lavender" />
                    <EditRowStyle BackColor="#2461BF" />
                </asp:GridView>
                
                
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 8px; text-align: center">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

