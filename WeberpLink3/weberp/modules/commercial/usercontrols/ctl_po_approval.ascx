<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_po_approval.ascx.cs" Inherits="ClientSide_modules_commercial_usercontrols_ctl_po_approval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<table style="width: 100%" class="tblmas">
    <tr>
        <td style="height: 10px">
        </td>
    </tr>
    <tr>
        <td class="tblsmall">
            <asp:Panel ID="description_HeaderPanel" runat="server" Style="cursor: pointer" ForeColor="Navy">
                <div class="heading">
                    <asp:ImageButton ID="Description_ToggleImage" runat="server" AlternateText="collapse"
                        ImageUrl="~/images/collapse.jpg" /><asp:Label ID="lblhead" runat="server" Font-Bold="True"
                            Text="1. Product 121 121   123123   120 MTR" Width="565px"></asp:Label>&nbsp;</div>
            </asp:Panel>
            <asp:Panel ID="description_ContentPanel"  runat="server" Style="overflow: hidden" Width="650px" ScrollBars="Auto" Height="400px">
                <table id="tbl_product" runat="server" style="width: 650px; border-top-width: 1px; border-left-width: 1px; border-left-color: #e6e6fa; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='white', startcolorstr='#D6DCF9', gradienttype='0'); border-bottom-width: 1px; border-bottom-color: #e6e6fa; border-top-color: #e6e6fa; border-right-width: 1px; border-right-color: #e6e6fa;">
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
                                        PO Ref</td>
                                    <td style="width: 20px">
                                        :</td>
                                    <td>
                                        <asp:Label ID="lblref" runat="server" Font-Bold="True" Text="Label" Width="168px" ForeColor="Red"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 65px">
                                        Party</td>
                                    <td style="width: 20px">
                                        :</td>
                                    <td>
                                        <asp:Label ID="lblparty" runat="server" Text="Label" Width="440px"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 65px">
                                        Amount</td>
                                    <td style="width: 20px">
                                        :</td>
                                    <td>
                                        <asp:Label ID="lblamount" runat="server" Text="Label" Width="139px"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 65px">
                                        Inward</td>
                                    <td style="width: 20px">
                                        :</td>
                                    <td>
                                        <asp:Label ID="lblinward" runat="server" Text="Label" Width="440px" Font-Bold="True"></asp:Label></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center">
                            <span style="font-size: 10pt; color: #000099"><strong>ITEMS DETAIL</strong></span></td>
                    </tr>
                    <tr>
                        <td  colspan="3" style="text-align: left">                           
                                <asp:PlaceHolder ID="celdetail" runat="server"></asp:PlaceHolder>                            
                        </td>
                    </tr>
                    <tr>
                        <td runat="server" colspan="3" style="text-align: center">
                            <span style="font-size: 10pt; color: #000099"><strong>TERMS &amp; CONDITIONS</strong></span> </td>
                    </tr>
                    <tr>
                        <td runat="server" colspan="3" style="text-align: left">
                            <span style="font-size: 10pt; color: #000099"><strong>GENERAL TERMS </strong>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td id="celgen" runat="server" colspan="3" style="text-align: left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td  runat="server" colspan="3" style="text-align: left">
                            <span style="font-size: 10pt; color: #000099"><strong>SPECIAL TERMS</strong></span></td>
                    </tr>
                    <tr>
                        <td id="celspe" runat="server" colspan="3" style="text-align: left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td  runat="server" colspan="3" style="text-align: left">
                            <span style="font-size: 10pt; color: #000099"><strong>PAYMENT TERMS</strong></span></td>
                    </tr>
                    <tr>
                        <td id="celpay" runat="server" colspan="3" style="text-align: left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="height: 1px;" class="heading" colspan="3">
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
    </table>
