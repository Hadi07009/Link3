<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_fpi_import_consignment_view.ascx.cs" Inherits="ClientSide_modules_commercial_usercontrols_ctl_fpi_import_consignment_view" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>

                   <table   style="width:100%;">
                                <tr>
                                    <td style="text-align: center; " 
                                        class="heading" colspan="5">
                                        IMPORT CONSIGNMENT INFORMATION</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 450px;" >
                                        Consignment Number:</td>
                                    <td style="width: 135px">

                                        <strong>

                                        <asp:Label ID="lbl_consign_number" runat="server" 
                                            ></asp:Label>
                                      
                                        </strong>
                                      
                                    </td>
                                    <td style="text-align: right; width: 110px;">
                                        &nbsp;</td>
                                    <td style="width: 317px" >

                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 450px;" >
                                        &nbsp;Bill of Lading(B/L) Number: </td>
                                    <td style="width: 135px">

                                        <strong>

                                        <asp:Label ID="lbl_BL_number" runat="server" 
                                            ></asp:Label>
                                      
                                        </strong>
                                      
                                    </td>
                                    <td style="text-align: right; width: 110px;">
                                        Date: </td>
                                    <td style="width: 317px" >

                                        <strong>

                                        <asp:Label ID="lbl_date" runat="server" 
                                            ></asp:Label>
                                      
                                        </strong>
                                      
                                    </td>
                                    <td>
                                        </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 450px;" >
                                        Bill of Lading(B/L) Quantity: </td>
                                    <td style="width: 135px">
                                        <strong>
                                        <asp:Label ID="lbl_bill_of_leading_quantity" runat="server" ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="text-align: right; width: 110px;">
                                        Unit: </td>
                                    <td style="width: 317px" >
                                        <strong>
                                        <asp:Label ID="lbl_unit" runat="server" ></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 450px;" >
                                        Vessel Name: </td>
                                    <td style="width: 135px">
                                        <strong>
                                        <asp:Label ID="lbl_vessel_name" runat="server" ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="text-align: right; width: 110px;">
                                        Country: </td>
                                    <td style="width: 317px" >
                                        <strong>
                                        <asp:Label ID="lbl_country" runat="server" ></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 450px;" >
                                        Import Route name:</td>
                                    <td style="width: 135px">
                                        <strong>
                                        <asp:Label ID="lbl_import_rout" runat="server" ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="text-align: right; width: 110px;" >
                                        &nbsp;</td>
                                    <td style="width: 317px" >
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 450px;" >
                                        Import rotation No.:</td>
                                    <td style="width: 135px">
                                        <strong>
                                        <asp:Label ID="lbl_import_rotation_no" runat="server" ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="text-align: right; width: 110px;" >
                                        &nbsp;</td>
                                    <td style="width: 317px" >
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 450px;" >
                                        Shipping Agent Charge:</td>
                                    <td valign="middle" style="width: 135px">
                                        <strong>
                                        <asp:Label ID="lbl_shipping_agent_charge" runat="server" ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="text-align: right; width: 110px;" >
                                        &nbsp;</td>
                                    <td style="width: 317px" >
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 450px;" >
                                        Date of Arrival:</td>
                                    <td style="width: 135px">

                                        <strong>

                                        <asp:Label ID="dt_arrival" runat="server" 
                                            ></asp:Label>
                                      
                                        </strong>
                                      
                                    </td>
                                    <td style="text-align: right; width: 110px;" >
                                        &nbsp;</td>
                                    <td style="width: 317px" >
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 450px;" >
                                        Shipping Agent Name:</td>
                                    <td colspan="3">
                                        <strong>
                                        <asp:Label ID="lbl_shipping_agent_name" runat="server" Width="468px" ></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 450px;" >
                                        Stevedoring Service Provider Name:</td>
                                    <td colspan="3">
                                        <strong>
                                        <asp:Label ID="lbl_stevedoring" runat="server" Width="468px" ></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 450px;" >
                                        &nbsp;</td>
                                    <td style="width: 135px">
                                        &nbsp;</td>
                                    <td style="text-align: right; width: 110px;" >
                                        &nbsp;</td>
                                    <td style="width: 317px" >
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                </table>
                        
