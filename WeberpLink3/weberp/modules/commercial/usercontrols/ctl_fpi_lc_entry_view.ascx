<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_fpi_lc_entry_view.ascx.cs" Inherits="ClientSide_modules_commercial_usercontrols_ctl_fpi_lc_entry_view" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>


                            <table   style="width:100%;">


                                    <tr>

                                <td class="heading" colspan="5" style="text-align: center">
            LC RELATED INFORMATION </td>

                                  
                                </tr>

                               
                                <tr>
                                    <td style="text-align: right; width: 399px;" >
                                        LC/LCA Bank Name:</td>
                                    <td  colspan="3">
                                        <asp:Label ID="lbl_LC_bank_name" runat="server" style="font-weight: 700"></asp:Label>
                                    </td>
                                    <td >
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 399px;" >
                                        Branch Name:</td>
                                    <td class="style3" colspan="3">
                                        <asp:Label ID="lbl_branch_name" runat="server" style="font-weight: 700"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 399px;" >
                                        LC/LCA Opening Margin:</td>
                                    <td style="width: 176px" >
                                        <strong>
                                        <asp:Label ID="lbl_LC_opening_margin" runat="server"  ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="text-align: right; width: 18px;" >
                                        Currency:</td>
                                    <td class="style31">
                                        <strong>
                                        <asp:Label ID="lbl_currency_opening_margin" runat="server"  ></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 399px;" >
                                        LC/LCA Cash Margin:</td>
                                    <td style="width: 176px" >
                                        <strong>
                                        <asp:Label ID="lbl_LC_cash_margin" runat="server"  ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="text-align: right; width: 18px;" >
                                        Currency:</td>
                                    <td class="style31">
                                        <strong>
                                        <asp:Label ID="lbl_currency_cash_margin" runat="server"  ></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 399px;" >
                                        LC/LCA FDR Margin:</td>
                                    <td style="width: 176px" >
                                        <strong>
                                        <asp:Label ID="lbl_LC_fdr_margin" runat="server"  ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="text-align: right; width: 18px;">
                                        currency:</td>
                                    <td class="style24">
                                        <strong>
                                        <asp:Label ID="lbl_currency_fdr_margin" runat="server"></asp:Label>
                                        </strong>
                                    </td>
                                    <td >
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 399px;" >
                                        Insurance Company Name:</td>
                                    <td class="style3" colspan="3">
                                        <strong>
                                        <asp:Label ID="lbl_insurance_company" runat="server" ></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 399px;" >
                                        Insurance Branch Name:</td>
                                    <td class="style3" colspan="3">
                                        <strong>
                                        <asp:Label ID="lbl_insurance_branch" runat="server" ></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 399px;" >
                                        Insurance Total Amount:</td>
                                    <td style="width: 176px" >
                                        <strong>
                                        <asp:Label ID="lbl_insurance_total_amt" runat="server"  ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="text-align: right; width: 18px;" >
                                        Currency:</td>
                                    <td class="style31">
                                        <strong>
                                        <asp:Label ID="lbl_currency_total_amt" runat="server"  ></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 399px;" >
                                        Premium Amount:</td>
                                    <td style="width: 176px" >
                                        <strong>
                                        <asp:Label ID="lbl_premimum_amt" runat="server"  ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="text-align: right; width: 18px;" >
                                        Currency:</td>
                                    <td class="style31">
                                        <strong>
                                        <asp:Label ID="lbl_currency_premium_amt" runat="server"  ></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 399px;" >
                                        VAT Amount:</td>
                                    <td style="width: 176px" >
                                        <strong>
                                        <asp:Label ID="lbl_vat_amt" runat="server"  ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="text-align: right; width: 18px;" >
                                        Currency:</td>
                                    <td class="style31">
                                        <strong>
                                        <asp:Label ID="lbl_currency_vat_amt" runat="server"  ></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 399px;" >
                                        Stamp Duty Amount:</td>
                                    <td style="width: 176px" >
                                        <strong>
                                        <asp:Label ID="lbl_stamp_amt" runat="server"  ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="text-align: right; width: 18px;" >
                                        Currency:</td>
                                    <td class="style31">
                                        <strong>
                                        <asp:Label ID="lbl_currency_stamp_amt" runat="server"  ></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 399px;" >
                                        LC Close Status:</td>
                                    <td colspan="3" >
                                        <strong>
                                        <asp:Label ID="lbl_close_status" runat="server"  ></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 399px;" >
                                        &nbsp;</td>
                                    <td style="width: 176px" >
                                        &nbsp;</td>
                                    <td style="text-align: right; width: 18px;" >
                                        &nbsp;</td>
                                    <td class="style31">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                </table>
                        
