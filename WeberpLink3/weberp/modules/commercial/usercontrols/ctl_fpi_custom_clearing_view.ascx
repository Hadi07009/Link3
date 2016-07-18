<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_fpi_custom_clearing_view.ascx.cs" Inherits="ClientSide_modules_commercial_usercontrols_ctl_fpi_custom_clearing_view" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>




                            <table   style="width:100%;">
                                <tr>
                                    <td style="text-align: center; " 
                                        class="heading" colspan="5">
                                        CUSTOM CLEARING INFORMATION</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        Consignment No:</td>
                                    <td style="width: 184px">

                                        <asp:Label ID="lbl_consign_no" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                    </td>
                                    <td style="text-align: right; width: 101px;" >
                                        Consignment quantity:</td>
                                    <td >

                                        <asp:Label ID="lbl_consign_qty" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        &nbsp;LC/LCA No:</td>
                                    <td style="width: 184px">

                                        <asp:Label ID="lbl_LC_no" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                    </td>
                                    <td style="text-align: right; width: 101px;" >
                                        LC/LCA Date: </td>
                                    <td >

                                        <asp:Label ID="lbl_LC_date" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                        <%--<ew:CalendarPopup ID="dtfinndate" runat="server" CssClass="txtbox" 
                                                    Culture="English (United Kingdom)" DisableTextBoxEntry="False" Width="76px">
                                                    <ButtonStyle CssClass="btn2" />
                                                </ew:CalendarPopup>--%>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        &nbsp;LC/LCA Expire Date: </td>
                                    <td style="width: 184px">

                                        <asp:Label ID="lbl_expire_date" runat="server" style="font-weight: bold" 
                                            Width="175px"></asp:Label>
                                      
                                    </td>
                                    <td style="text-align: right; width: 101px;" >
                                        Shipment Date:</td>
                                    <td >

                                        <asp:Label ID="lbl_shipment_date" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                    </td>
                                    <td >
                                        </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        &nbsp;LC Value:</td>
                                    <td style="width: 184px">

                                        <asp:Label ID="lbl_LC_value" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                    </td>
                                    <td style="text-align: right; width: 101px;" >
                                        LC Amendment Value: </td>
                                    <td >

                                        <asp:Label ID="lbl_LC_amendment_value" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        Invoice Value: </td>
                                    <td style="width: 184px">

                                        <asp:Label ID="lbl_invoice_value" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                    </td>
                                    <td style="text-align: right; width: 101px;" >
                                        Curr:</td>
                                    <td >

                                        <asp:Label ID="lbl_currency_invoice" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        Exchange Rate: </td>
                                    <td style="width: 184px">

                                        <asp:Label ID="lbl_exchange_rate" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                    </td>
                                    <td style="width: 101px" >
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td >
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        Total Value in (Taka): </td>
                                    <td style="width: 184px">
                                        <asp:Label ID="lbl_total_value" runat="server" style="font-weight: bold" 
                                            Width="175px"></asp:Label>
                                    </td>
                                    <td style="text-align: right; width: 101px;" >
                                        &nbsp;</td>
                                    <td >
                                        &nbsp;</td>
                                    <td >
                                        </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        Payment Tems:</td>
                                    <td colspan="3">

                                        <asp:Label ID="lbl_payment_terms" runat="server" style="font-weight: bold" 
                                            Width="468px"></asp:Label>
                                      
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        Maturity Date: </td>
                                    <td style="width: 184px">

                                        <asp:Label ID="lbl_maturity_date" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                    </td>
                                    <td style="text-align: right; width: 101px;" >
                                        &nbsp;</td>
                                    <td >
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        </td>
                                    <td style="width: 184px" >
                                        </td>
                                    <td style="text-align: right; width: 101px;">
                                        </td>
                                    <td >
                                        </td>
                                    <td>
                                        </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        &nbsp;</td>
                                    <td style="width: 184px" >
                                        <strong>ACTUAL VALUE</strong></td>
                                    <td style="text-align: right; width: 101px;" >
                                        &nbsp;</td>
                                    <td >
                                        <strong>PROVITIONAL VALUE</strong></td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        &nbsp;</td>
                                    <td style="width: 184px" >
                                        &nbsp;</td>
                                    <td style="text-align: right; width: 101px;" >
                                        &nbsp;</td>
                                    <td >
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        Custom
                                        Duty:</td>
                                    <td style="width: 184px">
                                    &nbsp;<asp:Label ID="lbl_duty" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                        TK</td>
                                    <td style="text-align: right; width: 101px;" >
                                        &nbsp;</td>
                                    <td >
                                        <asp:Label ID="lbl_duty_pro" 
                                            runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                        TK</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        Regulatory Duty(R/D):</td>
                                    <td style="width: 184px" >
                                    &nbsp;<asp:Label 
                                            ID="lbl_regulatory_duty" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                        TK</td>
                                    <td style="text-align: right; width: 101px;" >
                                        </td>
                                    <td >

                                        <asp:Label ID="lbl_regulatory_duty_pro" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                    &nbsp;TK</td>
                                    <td >
                                        </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        Supplementary Duty(S/D):</td>
                                    <td style="width: 184px">
                                        <asp:Label ID="lbl_SuplimentoryDuty" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                        TK</td>
                                    <td style="text-align: right; width: 101px;" >
                                        &nbsp;</td>
                                    <td >
                                        <asp:Label 
                                            ID="lbl_suplimentery_duty_pro" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                        TK</td>
                                    <td >
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        Value Added Tax (VAT):</td>
                                    <td style="width: 184px">

                                        <asp:Label ID="lbl_vat" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                    &nbsp;TK</td>
                                    <td style="text-align: right; width: 101px;" >
                                        &nbsp;</td>
                                    <td >
                                        <asp:Label 
                                            ID="lbl_vat_pro" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                        TK</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        Advance Income Tax(AIT):</td>
                                    <td style="width: 184px">
                                        <asp:Label ID="lbl_ait" 
                                            runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                        TK</td>
                                    <td style="text-align: right; width: 101px;" >
                                        &nbsp;</td>
                                    <td >
                                        <asp:Label 
                                            ID="lbl_ait_pro" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                        TK</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        Advanced Trade VAT(ATV):</td>
                                    <td style="width: 184px">
                                        <asp:Label 
                                            ID="lbl_atv" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                        TK</td>
                                    <td style="text-align: right; width: 101px;" >
                                        &nbsp;</td>
                                    <td >
                                        <asp:Label 
                                            ID="lbl_atv_pro" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                        TK</td>
                                    <td>
                                        </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        Pre-Shipment Inspection(PSI):</td>
                                    <td style="width: 184px">
                                        <asp:Label ID="lbl_PSI" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                        TK</td>
                                    <td style="text-align: right; width: 101px;" >
                                        &nbsp;</td>
                                    <td >
                                        <asp:Label ID="lbl_PSI_pro" 
                                            runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                        TK</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        Document Processing Fee(DF) VAT : </td>
                                    <td style="width: 184px">
                                        <asp:Label ID="lbl_DF_Vat" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                        TK</td>
                                    <td style="text-align: right; width: 101px;" >
                                        &nbsp;</td>
                                    <td >
                                        <asp:Label ID="lbl_DF_vat_pro" 
                                            runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                        TK</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        Port Dues : </td>
                                    <td c&nbsp;</td style="width: 184px">
                                        <asp:Label ID="lbl_port_dues" 
                                            runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                        TK<td style="width: 101px" >
                                        &nbsp;</td>
                                    <td >
                                        <asp:Label ID="lbl_port_dues_pro" 
                                            runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                        TK</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        Stevedoring Service Provider Name: </td>
                                    <td style="width: 184px">
                                    &nbsp;<asp:Label ID="lbl_stevedoring" 
                                            runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                        </td>
                                    <td style="text-align: right; width: 101px;" >
                                        &nbsp;</td>
                                    <td >
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        Stevedoring Charge:</td>
                                    <td valign="middle" style="width: 184px">
                                    &nbsp;<asp:Label ID="lbl_stevedoring_change" 
                                            runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                            &nbsp;</td>
                                    <td style="width: 101px" >
                                        &nbsp;</td>
                                    <td >
                                        <asp:Label 
                                            ID="lbl_stevedoring_change_pro" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                        TK</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        Berth Operator Charge(BOC):</td>
                                    <td valign="middle" style="width: 184px">
                                    &nbsp;<asp:Label 
                                            ID="lbl_boc" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                        TK</td>
                                    <td style="text-align: right; width: 101px;" >
                                        &nbsp;</td>
                                    <td >

                                        <asp:Label ID="lbl_boc_pro" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                    &nbsp;TK</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        Shipping Agent Name:</td>
                                    <td colspan="3" valign="middle">

                                        <asp:Label ID="lbl_shipping_agent_name" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                    </td>
                                    <td >
                                        </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        Shipping Agent Charge:</td>
                                    <td valign="middle" style="width: 184px">
                                    &nbsp;<asp:Label ID="lbl_shipping_agent_charge" 
                                            runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                        TK</td>
                                    <td style="text-align: right; width: 101px;" >
                                        &nbsp;</td>
                                    <td >
                                        <asp:Label ID="lbl_shipping_agent_charge_pro" 
                                            runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                        TK</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        Freight Forwarder Name:</td>
                                    <td valign="middle" style="width: 184px">
                                    &nbsp;<asp:Label ID="lbl_freight_forward_name" 
                                            runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                        </td>
                                    <td style="text-align: right; width: 101px;" >
                                        &nbsp;</td>
                                    <td >
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        Freight Forwarder NOC Charge:</td>
                                    <td valign="middle" style="width: 184px">
                                    &nbsp;<asp:Label ID="lbl_NOC_charge" 
                                            runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                        TK</td>
                                    <td style="text-align: right; width: 101px;" >
                                        &nbsp;</td>
                                    <td >
                                        <asp:Label ID="lbl_NOC_charge_pro" 
                                            runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                        TK</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        Surveyor chage</td>
                                    <td valign="middle" style="width: 184px">
                                    &nbsp;<asp:Label ID="lbl_survey_charge" 
                                            runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                        TK</td>
                                    <td style="text-align: right; width: 101px;" >
                                        &nbsp;</td>
                                    <td >
                                        <asp:Label ID="lbl_survey_charge_pro" 
                                            runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                    &nbsp;TK</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        C&amp;F Agent Fee (Rate) : </td>
                                    <td valign="middle" style="width: 184px">
                                        <asp:Label ID="lbl_CF_rate" 
                                            runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                    &nbsp;TK</td>
                                    <td style="text-align: right; width: 101px;" >
                                        &nbsp;</td>
                                    <td >
                                        <asp:Label ID="lbl_CF_rate_pro" 
                                            runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                        TK</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        Total C&amp;F Fee : </td>
                                    <td valign="middle" style="width: 184px">
                                    &nbsp;<asp:Label ID="lbl_total_fee" 
                                            runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                        TK</td>
                                    <td style="text-align: right; width: 101px;" >
                                        &nbsp;</td>
                                    <td >
                                        <asp:Label ID="lbl_total_fee_Pro" 
                                            runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                        TK</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        Carrying Rate : </td>
                                    <td valign="middle" style="width: 184px">
                                    &nbsp;<asp:Label ID="lbl_carrying_rate" 
                                            runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                        TK</td>
                                    <td style="text-align: right; width: 101px;" >
                                        </td>
                                    <td >
                                        <asp:Label ID="lblCarryingRatepro" 
                                            runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                        TK</td>
                                    <td>
                                        </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        Total Carrying Cost : </td>
                                    <td valign="middle" style="width: 184px">
                                    &nbsp;<asp:Label ID="lblTotalCarryingCost" 
                                            runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                        TK</td>
                                    <td style="text-align: right; width: 101px;" >
                                        &nbsp;</td>
                                    <td >
                                        <asp:Label ID="lblTotalCarryingCostPro" 
                                            runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                        TK</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        Others Cost : </td>
                                    <td valign="middle" style="width: 184px">
                                    &nbsp;<asp:Label ID="lblOtherCost" 
                                            runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                        TK</td>
                                    <td style="text-align: right; width: 101px;" >
                                        &nbsp;</td>
                                    <td >
                                        <asp:Label ID="lbl_other_cost_pro" 
                                            runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                        TK</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        Reason for others cost:</td>
                                    <td colspan="3" valign="middle">
                                    &nbsp;
                                    
                                        <asp:Label ID="lbl_reason_for_othe_cost" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        Total Cost:</td>
                                    <td valign="middle" style="width: 184px">

                                        <asp:Label ID="lbl_total_cost" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                    </td>
                                    <td style="text-align: right; width: 101px;" >
                                        Total Cost:</td>
                                    <td >

                                        <asp:Label ID="lbl_total_cost_pro" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        (-)VAT:</td>
                                    <td valign="middle" style="width: 184px">

                                        <asp:Label ID="lbl_vat_2" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                    </td>
                                    <td style="text-align: right; width: 101px;" >
                                        (-)VAT:</td>
                                    <td >

                                        <asp:Label ID="lbl_vat_2_pro" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        (-)AIT:</td>
                                    <td valign="middle" style="width: 184px">

                                        <asp:Label ID="lbl_ait_2" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                    </td>
                                    <td style="text-align: right; width: 101px;" >
                                        (-)AIT:</td>
                                    <td >

                                        <asp:Label ID="lbl_ait_2_pro" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        (-)ATV:</td>
                                    <td valign="middle" style="width: 184px">

                                        <asp:Label ID="lbl_atv_2" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                    </td>
                                    <td style="text-align: right; width: 101px;" >
                                        (-)ATV:</td>
                                    <td >

                                        <asp:Label ID="lbl_atv_2_pro" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        NET Cost:</td>
                                    <td valign="middle" style="width: 184px">

                                        <asp:Label ID="lbl_net_cost" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                    </td>
                                    <td style="text-align: right; width: 101px;" >
                                        NET Cost:</td>
                                    <td >

                                        <asp:Label ID="lbl_net_cost_pro" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        Rate/Qty:</td>
                                    <td valign="middle" style="width: 184px">

                                        <asp:Label ID="lbl_rate" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                    </td>
                                    <td style="text-align: right; width: 101px;" >
                                        Rate/Qty:</td>
                                    <td >

                                        <asp:Label ID="lbl_rate_pro" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" >
                                        Comments:</td>
                                    <td valign="middle" colspan="3">

                                        <asp:Label ID="lbl_comments" runat="server" style="font-weight: bold" 
                                           ></asp:Label>
                                      
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                </table>
                        
