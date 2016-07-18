<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_order_create_final.ascx.cs" Inherits="ClientSide_modules_commercial_usercontrols_ctl_order_create_final" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<style type="text/css">
    .style1
    {
        font-weight: bold;
    }
</style>
<table style="width: 100%">
    <tr>
        <td style="height: 10px; text-align: center;" colspan="3">
            &nbsp;&nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="3" class="tblsmall">
            <table style="width: 95%; border-top-width: 1px; border-left-width: 1px; border-left-color: #e6e6fa; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='white', startcolorstr='#D6DCF9', gradienttype='0'); border-bottom-width: 1px; border-bottom-color: #e6e6fa; border-top-color: #e6e6fa; border-right-width: 1px; border-right-color: #e6e6fa;" class="tblsmall">
                <tr>
                    <td class="style1" style="width: 67px; text-align: left">
                        <asp:Label ID="lblsl" runat="server" Text="1"></asp:Label>
                        Item</td>
                    <td class="tblsmall" style="width: 7px; text-align: left">
                        :</td>
                    <td runat="server" class="tblsmall" colspan="2" style="text-align: left">
                        <asp:Label ID="lblitem" runat="server" CssClass="tblsmall" Font-Bold="True" Text="Label"
                            Width="408px"></asp:Label></td>
                    <td rowspan="1" style="text-align: center">
                    </td>
                </tr>
                <tr>
                    <td class="tblsmall" style="width: 67px; text-align: left">
                        Ref No</td>
                    <td class="tblsmall" style="width: 7px; text-align: left">
                        :</td>
                    <td runat="server" class="tblsmall" colspan="2" style="text-align: left">
                        <asp:Label ID="lblmpr" runat="server" Text="Label" Width="155px"></asp:Label></td>
                    <td style="text-align: center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="tblsmall" style="width: 67px; text-align: left">
                        Party</td>
                    <td class="tblsmall" style="width: 7px; text-align: left">
                        :</td>
                    <td runat="server" class="tblsmall" colspan="2" style="text-align: left">
                        <asp:Label ID="lblparty" runat="server" Text="Label" Width="408px" CssClass="tblsmall"></asp:Label></td>
                    <td rowspan="1" style="text-align: center">
                    </td>
                </tr>
                <tr>
                    <td class="tblsmall" style="width: 67px; text-align: left">
                        MPR Qty</td>
                    <td class="tblsmall" style="width: 7px; text-align: left">
                        :</td>
                    <td runat="server" class="tblsmall" colspan="2" style="text-align: left">
                        <asp:Label ID="lblmprqty" runat="server" Text="Label" Width="155px"></asp:Label></td>
                    <td style="text-align: center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="tblsmall" style="width: 67px; text-align: left">
                        Avl Qty</td>
                    <td class="tblsmall" style="width: 7px; text-align: left">
                        :</td>
                    <td runat="server" class="tblsmall" colspan="2" style="text-align: left">
                        <asp:Label ID="lblavlqty" runat="server" Text="Label" Width="155px"></asp:Label></td>
                    <td style="text-align: center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="tblsmall" style="width: 67px; text-align: left">
                        Po
                        Qty</td>
                    <td class="tblsmall" style="width: 7px; text-align: left">
                        :</td>
                    <td id="Td5" runat="server" class="tblsmall" style="width: 257px; text-align: left">
                        <asp:Label ID="lblqty" runat="server" Text="Label" Width="155px" 
                            Font-Bold="True"></asp:Label></td>
                    <td runat="server" class="tblsmall" style="width: 179px; text-align: left">
                        </td>
                    <td rowspan="1" style="text-align: left">
                    </td>
                </tr>
                <tr>
                    <td class="tblsmall" style="width: 67px; text-align: left">
                        Rate</td>
                    <td class="tblsmall" style="width: 7px; text-align: left">
                        :</td>
                    <td runat="server" class="tblsmall" style="width: 257px; text-align: left" id="Td1">
                        <asp:Label ID="lblrate" runat="server" Text="Label" Width="155px" 
                            Font-Bold="True"></asp:Label></td>
                    <td runat="server" class="tblsmall" style="width: 179px; text-align: left">
                    </td>
                    <td rowspan="1" style="text-align: center">
                    </td>
                </tr>
                <tr>
                    <td class="tblsmall" style="width: 67px; text-align: left">
                        Amount</td>
                    <td class="tblsmall" style="width: 7px; text-align: left">
                        :</td>
                    <td runat="server" class="tblsmall" style="width: 257px; text-align: left" id="Td2">
                        <asp:Label ID="lblamount" runat="server" Text="Label" Width="155px" 
                            Font-Bold="True"></asp:Label></td>
                    <td runat="server" class="tblsmall" style="width: 179px; text-align: left">
                    </td>
                    <td rowspan="1" style="text-align: center">
                    </td>
                </tr>
                <tr>
                    <td class="tblsmall" style="width: 67px; text-align: left">
                        Specification</td>
                    <td class="tblsmall" style="width: 7px; text-align: left">
                        :</td>
                    <td runat="server" class="tblsmall" style="text-align: left" id="celspe" 
                        colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="tblsmall" style="width: 67px; text-align: left">
                        Brand</td>
                    <td class="tblsmall" style="width: 7px; text-align: left">
                        :</td>
                    <td runat="server" class="tblsmall" style="text-align: left" id="celbrand" 
                        colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="tblsmall" style="width: 67px; text-align: left">
                        Origin</td>
                    <td class="tblsmall" style="width: 7px; text-align: left">
                        :</td>
                    <td runat="server" class="tblsmall" style="text-align: left" id="celorigin" 
                        colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="tblsmall" style="width: 67px; text-align: left">
                        Packing</td>
                    <td class="tblsmall" style="width: 7px; text-align: left">
                        :</td>
                    <td runat="server" class="tblsmall" style="text-align: left" id="celpacking" 
                        colspan="3">
                        &nbsp;</td>
                </tr>
                </table>
        </td>
    </tr>
    <tr>
        <td class="heading" colspan="3" style="height: 1px">
        </td>
    </tr>
</table>
