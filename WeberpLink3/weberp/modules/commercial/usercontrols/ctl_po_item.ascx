<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_po_item.ascx.cs" Inherits="ClientSide_modules_commercial_usercontrols_ctl_po_item" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<table style="width: 61%">
    <tr>
        <td colspan="3" style="height: 10px">
        </td>
    </tr>
    <tr>
        <td class="tbl" colspan="3">
            <table style=" border-top-width: 1px; border-left-width: 1px; border-left-color: #e6e6fa; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='white', startcolorstr='#D6DCF9', gradienttype='0'); border-bottom-width: 1px; border-bottom-color: #e6e6fa; border-top-color: #e6e6fa; border-right-width: 1px; border-right-color: #e6e6fa; width: 720px;">
                <tr>
                    <td style="width: 29px">
                        <asp:Label ID="lblsl" runat="server" Text="sl" Font-Bold="True" ForeColor="Navy"></asp:Label></td>
                    <td colspan="6">
                        &nbsp;<asp:Label ID="lblproduct" runat="server" Text="Product" Width="476px" Font-Bold="True" ForeColor="Navy"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 29px">
                        </td>
                    <td>
                    </td>
                    <td colspan="2" style="text-align: left;">
                    </td>
                    <td colspan="1" style="text-align: left">
                    </td>
                    <td colspan="1" style="text-align: left">
                    </td>
                    <td colspan="1" style="text-align: left">
                    </td>
                </tr>
                <tr>
                    <td style="width: 29px" rowspan="3">
                        <asp:CheckBox ID="chksel" runat="server" Checked="True" Font-Bold="True" /></td>
                    <td>
                        Rate:</td>
                    <td colspan="2" style="text-align: left">
                        :</td>
                    <td colspan="3" style="text-align: left">
                        <asp:Label ID="lblrate" runat="server" Text="Label" Width="103px"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        Quantity:</td>
                    <td colspan="2" style="text-align: left">
                        :</td>
                    <td colspan="1" style="text-align: left">
                        <asp:Label ID="lblqty" runat="server" Text="Label" Width="103px"></asp:Label></td>
                    <td colspan="1" style="text-align: left">
                        <asp:Label ID="lblpoqty" runat="server" Text="PO Qty:" Width="56px"></asp:Label>
                    </td>
                    <td colspan="1" style="text-align: left">
                        <asp:TextBox ID="txtpo" runat="server" Font-Bold="False" Width="109px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        Amount:</td>
                    <td colspan="2" style="text-align: left">
                        :</td>
                    <td colspan="3" style="text-align: left">
                        <asp:Label ID="lbliamount" runat="server" Text="Label" Width="148px"></asp:Label></td>
                </tr>
                <tr>
                    <td rowspan="1" style="width: 29px">
                    </td>
                    <td>
                        Inward</td>
                    <td colspan="2" style="text-align: left">
                        :</td>
                    <td colspan="3" style="text-align: left">
                        <asp:Label ID="lbliinward" runat="server" Text="Label" Width="353px"></asp:Label></td>
                </tr>
            </table>
            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                FilterType="Custom, Numbers" TargetControlID="txtpo" ValidChars=".">
            </ajaxToolkit:FilteredTextBoxExtender>
        </td>
    </tr>
    <tr>
        <td colspan="3" style="height: 1px">
        </td>
    </tr>
</table>
