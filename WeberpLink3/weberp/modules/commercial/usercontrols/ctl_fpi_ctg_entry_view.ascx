<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_fpi_ctg_entry_view.ascx.cs" Inherits="ClientSide_modules_commercial_usercontrols_ctl_fpi_ctg_entry_view" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>


                            <table   style="width:100%;">
                                <tr>
                                    <td style="text-align: center; width: 1%;" 
                                        >
                                        &nbsp;</td>
                                    <td style="text-align: center; " 
                                        class="heading" colspan="5">
                                        CARGO LANDING AND TRANSPORTATION TO FACTORY</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 1%;" >
                                        &nbsp;</td>
                                    <td style="text-align: right; width: 310px;" >
                                        Consign No:</td>
                                    <td colspan="3">
                                        <strong>
                                        <asp:Label ID="lbl_consign_number" runat="server" 
                                            ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 2%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 1%;" >
                                        &nbsp;</td>
                                    <td style="text-align: right; width: 310px;" >
                                        Ref No:</td>
                                    <td colspan="3">
                                        <strong>
                                        <asp:Label ID="lbl_ref_no" runat="server"  ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 2%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 1%;" >
                                        &nbsp;</td>
                                    <td style="text-align: right; width: 310px;" >
                                        Balance Quantity:</td>
                                    <td colspan="3">
                                        <strong>
                                        <asp:Label ID="lbl_balance_quantity" runat="server" 
                                            ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 2%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 1%;" >
                                        &nbsp;</td>
                                    <td style="text-align: right; width: 310px;">
                                        Mother vessel Name:</td>
                                    <td colspan="3">
                                        <strong>
                                        <asp:Label ID="lbl_mother_vessel_name" runat="server"  
                                            ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 2%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 1%;" >
                                        &nbsp;</td>
                                    <td style="text-align: right; width: 310px;">
                                        &nbsp;Mother vessel Origin:</td>
                                    <td colspan="3">
                                        <strong>
                                        <asp:Label ID="lbl_mother_vessel_origin" runat="server" 
                                            ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 2%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 1%;" >
                                        &nbsp;</td>
                                    <td style="text-align: right; width: 310px;">
                                        Import Rotaion No:</td>
                                    <td colspan="3">
                                        <strong>
                                        <asp:Label ID="lbl_import_rotation_no" runat="server"
                                            ></asp:Label>
                                        </strong>
                                    </td>
                                    <td class="style45" style="width: 2%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 1%;" >
                                        &nbsp;</td>
                                    <td style="text-align: right; width: 310px;">
                                        Arrival Date:</td>
                                    <td style="width: 207px">
                                        <strong>
                                        <asp:Label ID="lbl_arrival_date" runat="server" 
                                            ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="text-align: right; width: 138px;" >
                                        &nbsp;</td>
                                    <td >
                                        &nbsp;</td>
                                    <td style="width: 2%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 1%;" >
                                        &nbsp;</td>
                                    <td style="text-align: right; width: 310px;">
                                        Date of Landing:</td>
                                    <td style="width: 207px">
                                        <strong>
                                        <asp:Label ID="lbl_date_of_landing" runat="server" 
                                            ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="text-align: right; width: 138px" >
                                        Loading Completion Date:</td>
                                    <td>
                                        <strong>
                                        <asp:Label ID="lbl_completion_date" runat="server" 
                                            ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 2%">
                                        ,</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 1%;" >
                                        &nbsp;</td>
                                    <td style="text-align: right; width: 310px;">
                                        Unit of Quantity:</td>
                                    <td style="width: 207px">
                                        <strong>
                                        <asp:Label ID="lbl_unit_of_quantity" runat="server"
                                            ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="text-align: right; width: 138px;" >
                                        &nbsp;</td>
                                    <td >
                                    </td>
                                    <td style="width: 2%">
                                        </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 1%;" >
                                        &nbsp;</td>
                                    <td style="text-align: right; width: 310px;">
                                        Mode of Transport:</td>
                                    <td style="width: 207px">
                                        <strong>
                                        <asp:Label ID="lbl_mode_of_transport" runat="server" 
                                            ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="text-align: right; width: 138px;" >
                                        &nbsp;</td>
                                    <td >
                                        &nbsp;</td>
                                    <td style="width: 2%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 1%;" >
                                        &nbsp;</td>
                                    <td style="text-align: right; width: 310px;">
                                        Carrier Service:</td>
                                    <td colspan="3">
                                        <strong>
                                        <asp:Label ID="lbl_carrier_service" runat="server" 
                                            ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 2%" >
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 1%;" >
                                        &nbsp;</td>
                                    <td style="text-align: right; width: 310px;">
                                        &nbsp;MRR Quantity:</td>
                                    <td colspan="3">
                                        <strong>
                                        <asp:Label ID="lbl_mrr_qty" runat="server" 
                                            ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 2%" >
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 1%;" >
                                        &nbsp;</td>
                                    <td style="text-align: right; width: 310px;">
                                        Loan Given Quantity:</td>
                                    <td colspan="3">
                                        <strong>
                                        <asp:Label ID="lbl_loan_given" runat="server" 
                                            ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 2%" >
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 1%;" >
                                        &nbsp;</td>
                                    <td style="text-align: right; width: 310px;">
                                        Loan Refund&nbsp; Quantity:</td>
                                    <td colspan="3">
                                        <strong>
                                        <asp:Label ID="lbl_loan_refund" runat="server" 
                                            ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 2%" >
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 1%;" >
                                        &nbsp;</td>
                                    <td style="text-align: right; width: 310px;">
                                        Landed Quantity:</td>
                                    <td style="width: 207px">
                                        <strong>
                                        <asp:Label ID="lbl_landed_quantity" runat="server" 
                                            ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="text-align: right; width: 138px;" >
                                        Landed Quantity short:</td>
                                    <td>
                                        <strong>
                                        <asp:Label ID="lbl_landed_quantity_short" runat="server"  
                                            ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 2%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 1%;" >
                                        &nbsp;</td>
                                    <td style="text-align: right; width: 310px;" >
                                        &nbsp;</td>
                                    <td style="width: 207px" >
                                        &nbsp;</td>
                                    <td style="text-align: right; width: 138px;" >
                                        &nbsp;</td>
                                    <td >
                                        &nbsp;</td>
                                    <td style="width: 2%">
                                        &nbsp;</td>
                                </tr>
                                </table>
                        
