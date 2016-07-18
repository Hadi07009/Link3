<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_fpi_consign_sum.ascx.cs" Inherits="modules_commercial_usercontrols_ctl_fpi_consign_sum" %>
                        <table style="width: 49%; height: 244px;">
                            <tr>
                                <td style="color: #0000CC; font-size: small;" colspan="3" class="tblbig">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: center; background-color: #9999FF; height: 12px;">
                                <asp:Label ID="lbltitle" runat="server" style="font-size: 10pt"></asp:Label>
                                </td>
                            </tr>
                        <tr>
                                <td style="width: 174px; text-align: right;" class="auto-style9">
                                    Bill of Lading QTY (MT):</td>
                            <td style="text-align: left; width: 35px;">
                                &nbsp;</td>
                                <td style="text-align: left; ">
                                    <asp:Label ID="lblblqty" runat="server" Text=""></asp:Label>
                                </td>
                        </tr>
                        <tr>
                            <td style="width: 174px; text-align: right;" class="auto-style9">
                                MRR QTY (MT):</td>
                            <td style="width: 35px">
                                &nbsp;</td>
                            <td style="text-align: left">
                                <asp:Label ID="lblmrrqty" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 174px; text-align: right;" class="auto-style9">
                           
                                Loan Given QTY (MT):</td>
                            <td style="text-align: left; width: 35px;">
                                &nbsp;</td>
                            <td style="text-align: left">
                                <asp:Label ID="lblloangivenqty" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                            <tr>
                                <td style="width: 174px; text-align: right;" class="auto-style9">
                                    Loan Refund QTY (MT):</td>
                                <td style="text-align: left; width: 35px;">
                                    &nbsp;</td>
                                <td style="text-align: left">
                                    <asp:Label ID="lblloanrefundqty" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style9" style="width: 174px; text-align: right;">Landel Short/Excess QTY (MT):</td>
                                <td style="width: 35px">&nbsp;</td>
                                <td style="text-align: left">
                                    <asp:Label ID="lblshortaccessqty" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: right; height: 1px; background-color: #000066;"></td>
                            </tr>
                            <tr>
                                <td class="auto-style9" style="width: 174px; text-align: right;"><strong>Actual Landed Qty (MT):</strong></td>
                                <td style="width: 35px; text-align: left">&nbsp;</td>
                                <td style="text-align: left">
                                    <asp:Label ID="lblactqty" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style9" style="width: 174px; text-align: right;">&nbsp;</td>
                                <td style="width: 35px; text-align: left">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            </table>
                    
