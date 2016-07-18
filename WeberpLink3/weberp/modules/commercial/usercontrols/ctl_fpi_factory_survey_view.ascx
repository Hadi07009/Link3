<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_fpi_factory_survey_view.ascx.cs" Inherits="ClientSide_modules_commercial_usercontrols_ctl_fpi_factory_survey_view" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>


                            <table   style="width:100%;">
                                <tr>
                                    <td 
                                         colspan="5" class="heading">
                                        Factory Survey Information</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 313px;" >
                                        Date of Landing:</td>
                                    <td style="width: 113px" >
                                        <asp:Label ID="dt_landing_date" runat="server" Width="175px" style="font-weight: 700"></asp:Label>
                                    </td>
                                    <td style="text-align: right" >
                                        Loading Completion Date:</td>
                                    <td>
                                        <strong>
                                        <asp:Label ID="lbl_completion_date" runat="server" Width="175px" ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 77px">
                                        ,</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 313px;" >
                                        Unit of Quantity:</td>
                                    <td style="width: 113px">
                                        <asp:Label ID="txt_unit_of_quantity" runat="server" Width="175px" style="font-weight: 700"></asp:Label>
                                    </td>
                                    <td style="text-align: right" >
                                        &nbsp;</td>
                                    <td >
                                    </td>
                                    <td style="width: 77px">
                                        </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 313px;" >
                                        Mode of Transport:</td>
                                    <td style="width: 113px" >
                                        <strong>
                                        <asp:Label ID="lbl_mode_of_transport" runat="server" Width="175px" ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="text-align: right" >
                                        &nbsp;</td>
                                    <td >
                                        &nbsp;</td>
                                    <td style="width: 77px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 313px;" >
                                        Carrier Service:</td>
                                    <td  colspan="3">
                                        <strong>
                                        <asp:Label ID="lbl_carrier_service" runat="server" Width="477px" ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 77px" >
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 313px;" >
                                        Landed Quantity:</td>
                                    <td style="width: 113px" >
                                        <strong>
                                        <asp:Label ID="lbl_landed_quantity" runat="server" Width="175px" ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="text-align: right" >
                                        Landed Quantity short:</td>
                                    <td >
                                        <strong>
                                        <asp:Label ID="lbl_landed_quantity_short" runat="server" Width="175px" ></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 77px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 313px;" >
                                        Transport Detail:</td>
                                    <td  colspan="3">
                                        <asp:GridView ID="dgv_details" runat="server">
                                        </asp:GridView>
                                    </td>
                                    <td style="width: 77px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 313px;" >
                                        &nbsp;</td>
                                    <td style="width: 113px" >
                                        &nbsp;</td>
                                    <td style="text-align: right" >
                                        &nbsp;</td>
                                    <td >
                                        &nbsp;</td>
                                    <td style="width: 77px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center"  colspan="4">
                                        <asp:Button ID="btn_save" runat="server" Text="SAVE" Width="85px" />
&nbsp;
                                        <asp:Button ID="btn_cancel" runat="server" Text="CANCEL" Width="85px" />
                                    </td>
                                    <td style="width: 77px" >
                                        </td>
                                </tr>
                                </table>
                        
