<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_mat_trn_det.aspx.cs" Inherits="frm_mat_trn_det" Title=""   ValidateRequest="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2" namespace="eWorld.UI" tagprefix="ew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >
&nbsp;&nbsp;&nbsp; `&nbsp;&nbsp;&nbsp;
<asp:UpdatePanel ID="updpnl" runat="server">
    <ContentTemplate>
        <table ID="tblmaster" runat="server" class="tblmas" style="width: 100%">
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
                    MATERIAL RECEIVE / RETURN DETAIL</td>
            </tr>
            <tr>
                <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                    <table style="width:100%;">
                        <tr>
                            <td style="width: 165px; text-align: right">
                                From Date: </td>
                            <td style="width: 229px">
                                <ew:CalendarPopup ID="cldfrom" runat="server" 
                                    Culture="English (United Kingdom)" DisableTextBoxEntry="False" Width="85px">
                                    <ButtonStyle CssClass="btn2" />
                                </ew:CalendarPopup>
                            </td>
                            <td style="width: 304px">
                                <asp:RadioButtonList ID="rdoOpt" runat="server" RepeatColumns="3" Width="550px" 
                                    RepeatDirection="Horizontal">
                                </asp:RadioButtonList>
                            </td>
                            <td style="text-align: left">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 165px; text-align: right">
                                To Date:</td>
                            <td style="width: 229px">
                                <ew:CalendarPopup ID="cldto" runat="server" Culture="English (United Kingdom)" 
                                    DisableTextBoxEntry="False" Width="87px">
                                    <ButtonStyle CssClass="btn2" />
                                </ew:CalendarPopup>
                            </td>
                            <td style="width: 304px">
                                &nbsp;</td>
                            <td style="text-align: left">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                    <asp:Button ID="btnshows" runat="server" CssClass="btn2" 
                        onclick="btnshows_Click" Text="Show" Width="91px" />
                </td>
            </tr>
            <tr>
                <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="tbl" colspan="3" style="height: 24px; text-align: left">
                    <asp:GridView ID="gdUser" runat="server" BackColor="White" 
                        BorderColor="#41519A" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                        ForeColor="#333333" GridLines="None" OnRowDataBound="gdUser_RowDataBound" 
                        OnSelectedIndexChanged="gdUser_SelectedIndexChanged" PageSize="100" 
                        SkinID="GridView" Width="100%">
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
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="height: 57px">
                </td>
                <td style="height: 57px">
                </td>
                <td style="height: 57px">
                </td>
            </tr>
        </table>
    </ContentTemplate>
      <Triggers>
      <asp:PostBackTrigger ControlID="gdUser" />
      </Triggers>
</asp:UpdatePanel>

</asp:Content>

