<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_po_item_mrr_view.ascx.cs" Inherits="ClientSide_modules_commercial_usercontrols_ctl_po_item_mrr_view" %>
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
                    <td colspan="5">
                        &nbsp;<asp:Label ID="lblproduct" runat="server" Text="Product" Width="476px" Font-Bold="True" ForeColor="Navy"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 29px">
                        </td>
                    <td>
                    </td>
                    <td style="text-align: left;">
                    </td>
                    <td colspan="1" style="text-align: left">
                    </td>
                    <td colspan="1" style="text-align: left">
                    </td>
                    <td colspan="1" style="text-align: left">
                    </td>
                </tr>
                <tr>
                    <td style="width: 29px" rowspan="5">
                        &nbsp;</td>
                    <td>
                        MPR Ref</td>
                    <td style="text-align: left">
                        &nbsp;</td>
                    <td colspan="3" style="text-align: left">
                        <asp:LinkButton ID="lnkmprref" runat="server"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        CS Ref</td>
                    <td style="text-align: left">
                        &nbsp;</td>
                    <td colspan="3" style="text-align: left">
                        <asp:LinkButton ID="lnkcsref" runat="server">LinkButton</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        Rate:</td>
                    <td style="text-align: left">
                        :</td>
                    <td colspan="3" style="text-align: left">
                        <asp:Label ID="lblrate" runat="server" Text="Label" Width="103px"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        Quantity:</td>
                    <td style="text-align: left">
                        :</td>
                    <td colspan="1" style="text-align: left">
                        <asp:Label ID="lblqty" runat="server" Text="Label" Width="103px"></asp:Label></td>
                    <td colspan="1" style="text-align: left">
                        &nbsp;</td>
                    <td colspan="1" style="text-align: left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        Amount:</td>
                    <td style="text-align: left">
                        :</td>
                    <td colspan="3" style="text-align: left">
                        <asp:Label ID="lbliamount" runat="server" Text="Label" Width="148px"></asp:Label></td>
                </tr>
                <tr>
                    <td rowspan="1" style="width: 29px">
                    </td>
                    <td>
                        Inward</td>
                    <td style="text-align: left">
                        :</td>
                    <td colspan="3" style="text-align: left">
                        <asp:Label ID="lbliinward" runat="server" Text="Label" Width="353px"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 29px">
                        &nbsp;</td>
                    <td>
                        Specification</td>
                    <td style="text-align: left">
                        :</td>
                    <td colspan="3" runat="server" id="celspe" style="text-align: left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 29px">
                        &nbsp;</td>
                    <td>
                        Brand</td>
                    <td style="text-align: left">
                        :</td>
                    <td colspan="3" runat="server" id="celbrand" style="text-align: left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 29px">
                        &nbsp;</td>
                    <td>
                        Origin</td>
                    <td style="text-align: left">
                        :</td>
                    <td colspan="3" runat="server" id="celorigin" style="text-align: left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 29px">
                        &nbsp;</td>
                    <td>
                        Packing</td>
                    <td style="text-align: left">
                        :</td>
                    <td colspan="3"  runat="server" id="celpacking" style="text-align: left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 29px">
                        &nbsp;</td>
                    <td colspan="5" style="text-align: center">
                        MRR DETAIL</td>
                </tr>
                <tr>
                    <td style="width: 29px">
                        &nbsp;</td>
                    <td colspan="5">
                <asp:GridView ID="gdItem" runat="server" BackColor="White" 
                BorderColor="#41519A" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #e6e6fa; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='white', startcolorstr='#D6DCF9', gradienttype='0'); border-bottom-width: 1px; border-bottom-color: #e6e6fa; border-top-color: #e6e6fa; border-right-width: 1px; border-right-color: #e6e6fa;"
                    BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                ForeColor="#333333" GridLines="None"
                    
                    PageSize="100" SkinID="GridView" Width="98%">
                    <FooterStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#41519A" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle Font-Bold="True" />
                    <HeaderStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="Lavender" Font-Size="8pt" 
                        Font-Underline="False" />
                    <RowStyle Font-Size="8pt" />
                
                </asp:GridView>                   
                        </td>
                </tr>
                </table>
        </td>
    </tr>
    <tr>
        <td colspan="3" style="height: 1px">
        </td>
    </tr>
</table>
