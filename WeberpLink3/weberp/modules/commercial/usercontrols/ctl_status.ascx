<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_status.ascx.cs" Inherits="ClientSide_modules_commercial_usercontrols_ctl_status" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<table style="width: 100%" class="tblmas">
    <tr>
        <td style="height: 10px" colspan="3">
        </td>
    </tr>
    <tr>
        <td colspan="3" class="tblsmall">
            <asp:Panel ID="description_HeaderPanel" runat="server" Style="cursor: pointer" ForeColor="Navy" Width="646px">
                <div class="heading">
                    <asp:ImageButton ID="Description_ToggleImage" runat="server" AlternateText="collapse"
                        ImageUrl="~/images/collapse.jpg" /><asp:Label ID="lblhead" runat="server" Font-Bold="True"
                            Text="1. Product 121 121   123123   120 MTR" Width="353px"></asp:Label>&nbsp;<asp:Label
                                ID="lblstatus" runat="server" Font-Bold="True" ForeColor="Red" Text="Waitting for approval"
                                Width="210px"></asp:Label></div>
            </asp:Panel>
            <asp:Panel ID="description_ContentPanel" runat="server" Style="overflow: hidden" Width="650px" ScrollBars="Auto" Height="400px">
                <table id="tbl_product" runat="server" style="width: 681px; border-top-width: 1px; border-left-width: 1px; border-left-color: #e6e6fa; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='white', startcolorstr='#D6DCF9', gradienttype='0'); border-bottom-width: 1px; border-bottom-color: #e6e6fa; border-top-color: #e6e6fa; border-right-width: 1px; border-right-color: #e6e6fa;">
                    <tr>
                        <td style="width: 58px">
                        </td>
                        <td style="width: 9px">
                        </td>
                        <td style="width: 22px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <table style="width: 551px">
                                <tr>
                                    <td style="width: 65px">
                            Ref</td>
                                    <td style="width: 20px">
                                        :</td>
                                    <td id="celref" runat="server">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 65px">
                            Product</td>
                                    <td style="width: 20px">
                                        :</td>
                                    <td id="celproduct" runat="server">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 65px">
                            Quantity</td>
                                    <td style="width: 20px">
                                        :</td>
                                    <td id="celqty" runat="server">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center">
                            <span style="font-size: 10pt; color: #000099"><strong>APPROVAL</strong></span></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td id="celcomments" runat="server" colspan="3" style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td  colspan="3" style="text-align: center; height: 21px;">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <ajaxToolkit:CollapsiblePanelExtender ID="cpeDesc" runat="Server" CollapseControlID="description_HeaderPanel"
                Collapsed="True" ExpandControlID="description_HeaderPanel" ImageControlID="description_ToggleImage"
                TargetControlID="description_ContentPanel" CollapsedImage="../../../../images/expand.jpg" ExpandedImage="../../../../images/collapse.jpg" SuppressPostBack="true"  >
            </ajaxToolkit:CollapsiblePanelExtender>
        </td>
    </tr>
    <tr>
        <td colspan="3" style="height: 1px">
        </td>
    </tr>
</table>
