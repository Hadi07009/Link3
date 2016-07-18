<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmManPowerReport.aspx.cs" Inherits="modules_HRMS_Details_frmManPowerReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
        MANPOWER REPORT
    </asp:Panel>
    <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
        <table style="width: 99%; text-align: left">
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Select Company"></asp:Label>
                </td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="ddlcompany" runat="server" AutoPostBack="True" CssClass="tbl" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" Width="380px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td align="center">&nbsp;</td>
                <td>
                    <asp:Button ID="btnBankAccountReport" runat="server" CssClass="btn2" OnClick="btnBankAccountReport_Click" Text="Preview Report" Width="198px" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
