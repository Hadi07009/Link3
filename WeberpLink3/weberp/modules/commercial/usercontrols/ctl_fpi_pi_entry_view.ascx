<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_fpi_pi_entry_view.ascx.cs" Inherits="ClientSide_modules_commercial_usercontrols_ctl_fpi_pi_entry_view" %>

<table   style="width:100%;">
    <tr>
        <td  colspan="6" 
            style="color: #FFFFFF; text-align: center;" class="heading">
                                        Proforma Invoice Information Entry</td>
    </tr>
    <tr>
        <td style="width: 20%; text-align: right;" >
                                        &nbsp;</td>
        <td style="width: 14%; text-align: right;" >
                                        Reference&nbsp; No:</td>
        <td style="width: 172px" >
                                        <asp:Label ID="lbl_ref_no" runat="server" 
                style="font-weight: bold"></asp:Label>
                                    </td>
        <td style="text-align: right; width: 18%"  >
            Date</td>
        <td style="width: 26%">
                                        <asp:Label ID="lbl_date" runat="server" 
                style="font-weight: bold"></asp:Label>
                                    </td>
        <td style="width: 12%" >
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 20%; text-align: right;" >
                                        &nbsp;</td>
        <td style="width: 14%; text-align: right;" >
                                        User&#39;s Name:</td>
        <td style="width: 172px" >
                                        <asp:Label ID="lbl_user_name" runat="server" 
                style="font-weight: bold" ></asp:Label>
                                    </td>
        <td style="text-align: right; width: 18%"  >
            Expenses Nature:</td>
        <td style="width: 26%">
                                        <asp:Label ID="lbl_expense_nature" runat="server" 
                style="font-weight: bold"></asp:Label>
                                    </td>
        <td style="width: 12%" >
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 20%; text-align: right;" >
            &nbsp;</td>
        <td style="width: 14%; text-align: right;" >
            Location</td>
        <td colspan="3">
                                        <asp:Label ID="lbl_location" runat="server" 
                style="font-weight: bold" BorderStyle="None"  
                                            ></asp:Label>
                                    </td>
        <td style="width: 12%">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 20%; text-align: right;" >
                                        &nbsp;</td>
        <td style="width: 14%; text-align: right;" >
                                        PI/Indent No:</td>
        <td style="width: 172px" >
                                        <asp:Label ID="lb_lPI_no" runat="server" 
                style="font-weight: bold"></asp:Label>
                                    </td>
        <td style="text-align: right; width: 18%"  >
            Date:</td>
        <td style="width: 26%">
                                        <asp:Label ID="lbl_date_PI" runat="server" 
                style="font-weight: bold"></asp:Label>
                                    </td>
        <td style="width: 12%" >
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 20%; text-align: right; height: 16px;" >
                                        &nbsp;</td>
        <td style="width: 14%; text-align: right; height: 16px;" >
                                        Supplier Name:</td>
        <td  colspan="3" style="height: 16px">
                                        <asp:Label ID="lbl_supp" runat="server" 
                style="font-weight: bold" BorderStyle="None"
                                           ></asp:Label>
                                    </td>
        <td style="width: 12%; height: 16px;" >
            </td>
    </tr>
    <tr>
        <td style="width: 20%; text-align: right;" >
                                        &nbsp;</td>
        <td style="width: 14%; text-align: right;" >
                                        Item Details:</td>
        <td  colspan="3">
                                        <asp:Label ID="lbl_item_det" runat="server" 
                style="font-weight: bold" BorderStyle="None" 
                                            ></asp:Label>
                                    </td>
        <td style="width: 12%" >
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 20%; text-align: right;" >
            &nbsp;</td>
        <td style="width: 14%; text-align: right;" >
            Origin:</td>
        <td colspan="3" style="text-align: left">
                                        <asp:Label ID="lbl_origin" runat="server" 
                style="font-weight: bold" BorderStyle="None"  
                                           ></asp:Label>
                                    </td>
        <td style="text-align: right; width: 12%" >
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 20%; text-align: right;" >
                                        &nbsp;</td>
        <td style="width: 14%; text-align: right;" >
                                        Quantity:</td>
        <td style="width: 172px" >
                                        <asp:Label ID="lbl_quantity" runat="server" 
                style="font-weight: bold" ></asp:Label>
                                    </td>
        <td style="text-align: right; width: 18%"  >
                                        Unit:</td>
        <td style="width: 26%">
                                        <asp:Label ID="lbl_unit" runat="server" 
                style="font-weight: bold"></asp:Label>
                                    </td>
        <td style="width: 12%" >
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 20%; text-align: right;" >
                                        &nbsp;</td>
        <td style="width: 14%; text-align: right;" >
                                        Unit rate:</td>
        <td style="width: 172px" >
                                        <asp:Label ID="lbl_unit_rate" runat="server" 
                style="font-weight: bold" ></asp:Label>
                                    </td>
        <td style="text-align: right; width: 18%"  >
                                        currency:</td>
        <td style="width: 26%">
                                        <asp:Label ID="lbl_currency" runat="server" 
                style="font-weight: bold"></asp:Label>
                                    </td>
        <td style="width: 12%" >
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 20%; text-align: right;" >
                                        &nbsp;</td>
        <td style="width: 14%; text-align: right;" >
                                        Total value:</td>
        <td style="width: 172px" >
                                        <asp:Label ID="lbl_total_value" runat="server" 
                style="font-weight: bold"></asp:Label>
                                    </td>
        <td style="text-align: right; width: 18%"  >
                                        currency:</td>
        <td style="width: 26%">
                                        <asp:Label ID="lbl_currency_total_value" runat="server" 
                style="font-weight: bold"></asp:Label>
                                    </td>
        <td style="width: 12%" >
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 20%; text-align: right;" >
                                        &nbsp;</td>
        <td style="width: 14%; text-align: right;" >
                                        Payment Terms:</td>
        <td  colspan="3" style="text-align: left">
                                        <asp:Label ID="lbl_payment" runat="server" 
                style="font-weight: bold" BorderStyle="None"></asp:Label>
                                    </td>
        <td style="text-align: right; width: 12%" >
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 20%; text-align: right;" >
            &nbsp;</td>
        <td style="width: 14%; text-align: right;" >
            Last
                                        Date of Shipment:</td>
        <td style="width: 172px" >
                                        <asp:Label ID="lbl_shipment" runat="server" 
                style="font-weight: bold"></asp:Label>
                                    </td>
        <td style="text-align: right; width: 18%"  >
                                        LC Validity:</td>
        <td style="width: 26%">
                                        <asp:Label ID="lbl_LC_validity_date" runat="server" 
                style="font-weight: bold"></asp:Label>
                                    </td>
        <td style="width: 12%" >
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 20%; text-align: right;" >
                                        &nbsp;</td>
        <td style="width: 14%; text-align: right;" >
                                        Port of Loading:</td>
        <td class="style13" colspan="3">
                                        <asp:Label ID="lbl_port_of_loading" runat="server" 
                style="font-weight: bold" BorderStyle="None"></asp:Label>
                                    </td>
        <td style="width: 12%" >
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 20%; text-align: right;" >
                                        &nbsp;</td>
        <td style="width: 14%; text-align: right;" >
                                        Port of Discharge:</td>
        <td style="width: 172px" >
                                        <asp:Label ID="lbl_portof_discharge" runat="server" 
                style="font-weight: bold"></asp:Label>
                                    </td>
        <td style="text-align: right; width: 18%"  >
                                        Expect Arival Time:</td>
        <td style="width: 26%">
                                        <asp:Label ID="lbl_arrival" runat="server" 
                style="font-weight: bold"></asp:Label>
                                    </td>
        <td style="width: 12%" >
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 20%; text-align: right;" >
                                        &nbsp;</td>
        <td style="width: 14%; text-align: right;" >
                                        Name of C&amp;F Agent:</td>
        <td  colspan="3">
                                        <asp:Label ID="lbl_CF_agent" runat="server" 
                style="font-weight: bold" BorderStyle="None"></asp:Label>
                                    </td>
        <td style="width: 12%" >
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 20%; text-align: right;" >
                                        &nbsp;</td>
        <td style="width: 14%; text-align: right;" >
                                        Tranport Contractor:</td>
        <td  colspan="3">
                                        <asp:Label ID="lbl_transport_contact" runat="server" 
                style="font-weight: bold" BorderStyle="None"></asp:Label>
                                    </td>
        <td style="width: 12%" >
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 20%; text-align: right;" >
                                        &nbsp;</td>
        <td style="width: 14%; text-align: right;" >
                                        Mode of Transport:</td>
        <td style="width: 172px" >
                                        <asp:Label ID="lbl_mode_of_transport" runat="server" 
                style="font-weight: bold"></asp:Label>
                                    </td>
        <td style="text-align: right; width: 18%"  >
            Stock In Hand:</td>
        <td style="width: 26%">
                                        <asp:Label ID="lbl_stock_in_hand" runat="server" 
                style="font-weight: bold"></asp:Label>
                                    </td>
        <td style="width: 12%" >
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 20%; text-align: right;" >
                                        &nbsp;</td>
        <td style="width: 14%; text-align: right;" >
                                        L/C Details Pipeline:</td>
        <td  colspan="3">
                                        <asp:Label ID="lbl_LC_details" runat="server" 
                style="font-weight: bold" BorderStyle="None"></asp:Label>
                                    </td>
        <td style="width: 12%" >
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 20%; text-align: right;" >
                                        &nbsp;</td>
        <td style="width: 14%; text-align: right;" >
                                        Total Stock With Pipeline:</td>
        <td style="width: 172px" >
                                        <asp:Label ID="lbl_total_stock" runat="server" 
                style="font-weight: bold"></asp:Label>
                                    </td>
        <td style="text-align: right; width: 18%"  >
                                        Production CoveragePeriod:</td>
        <td style="width: 26%">
                                        <asp:Label ID="lbl_production_period" runat="server" 
                style="font-weight: bold"></asp:Label>
                                    </td>
        <td style="width: 12%" >
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 20%; text-align: right;" >
                                        &nbsp;</td>
        <td style="width: 14%; text-align: right;" >
                                        Previous Unit&nbsp; Rate:</td>
        <td style="width: 172px" >
                                        <asp:Label ID="lbl_unit_rate_previuos" runat="server" 
                style="font-weight: bold"></asp:Label>
                                    </td>
        <td style="width: 18%; text-align: right"  >
                                        Date</td>
        <td style="width: 26%">
                                        <asp:Label ID="lbl_date_previous_date" runat="server" 
                style="font-weight: bold"></asp:Label>
                                    </td>
        <td style="width: 12%" >
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 20%" >
            &nbsp;</td>
        <td style="width: 14%" >
            &nbsp;</td>
        <td style="width: 172px" >
            &nbsp;</td>
        <td style="text-align: left; width: 18%"  >
            &nbsp;</td>
        <td style="width: 26%">
            &nbsp;</td>
        <td style="width: 12%" >
            &nbsp;</td>
    </tr>
    </table>

