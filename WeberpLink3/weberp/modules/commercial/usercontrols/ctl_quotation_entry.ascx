<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_quotation_entry.ascx.cs" Inherits="ClientSide_modules_commercial_usercontrols_ctl_quotation_entry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<table style="width: 90%">
    <tr>
        <td colspan="3" style="height: 10px">
        </td>
    </tr>
    <tr>
        <td class="tblsmall" colspan="3">
            <table style="border-color: #e6e6fa; border-width: 1px; width: 89%; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='white', startcolorstr='#D6DCF9', gradienttype='0'); ">
                <tr>
                    <td style="width: 29px">
                        <asp:Label ID="lblsl" runat="server" Text="sl" Font-Bold="True" ForeColor="Navy"></asp:Label></td>
                    <td colspan="2" style="text-align: left">
                        &nbsp;<asp:Label ID="lblproduct" runat="server" Text="Product" Width="405px" Font-Bold="True" ForeColor="Navy"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 29px">
                    </td>
                    <td colspan="2" style="text-align: left">
                        <table id ="tblproduct" runat="server" style="width: 701px">
                            <tr>
                                <td style="width: 104px">
                                    Ref No</td>
                                <td style="width: 14px">
                                    :</td>
                                <td>
                                    <asp:Label ID="lblref" runat="server" Text="Label" Width="300px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 104px">
                                    Requisition</td>
                                <td style="width: 14px">
                                    :</td>
                                <td>
                                    <asp:Label ID="lblreqtype" runat="server" Text="Label" Width="300px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 104px">
                                    Item code</td>
                                <td style="width: 14px">
                                    :</td>
                                <td>
                        <asp:Label ID="lblitmcode" runat="server" Font-Bold="False" Text="code"
                            Width="155px"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="width: 104px">
                                    Qty</td>
                                <td style="width: 14px">
                                    :</td>
                                <td>
                                    <asp:Label ID="lblqty" runat="server" Font-Bold="False" Text="qty" Width="90px"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="width: 104px">
                                    Specification</td>
                                <td style="width: 14px">
                                    :</td>
                                <td>
                                    <asp:TextBox ID="txtspecification" runat="server" Width="500px" 
                                        CssClass="txtbox"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 104px">
                                    Brand</td>
                                <td style="width: 14px">
                                    :</td>
                                <td>
                                    <asp:TextBox ID="txtbrand" runat="server" Width="500px" CssClass="txtbox"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 104px">
                                    Origin</td>
                                <td style="width: 14px">
                                    :</td>
                                <td>
                                    <asp:TextBox ID="txtorigin" runat="server" Width="500px" CssClass="txtbox"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 104px">
                                    Packing</td>
                                <td style="width: 14px">
                                    :</td>
                                <td>
                                    <asp:TextBox ID="txtpacking" runat="server" Width="500px" CssClass="txtbox"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 104px">
                                    Unit Price</td>
                                <td style="width: 14px">
                                    :</td>
                                <td>
                                    <asp:TextBox ID="txtrate" runat="server" CssClass="txtbox" Width="137px"></asp:TextBox><span
                                        style="color: #ff0000">*</span>
                                    <asp:Label ID="lbltk" runat="server" Text="tk" Width="140px"></asp:Label><span style="color: #ff0000"></span>
                                    <ajaxtoolkit:filteredtextboxextender
                                        id="FilteredTextBoxExtender3" runat="server" filtertype="Custom, Numbers" targetcontrolid="txtrate"
                                        validchars="."> </ajaxtoolkit:filteredtextboxextender>
                                        </td>
                            </tr>
                            <tr>
                                <td style="width: 104px">
                                    <asp:Label ID="Label1" runat="server" Text="Recent Rate/Unit" Visible="False"></asp:Label>
                                </td>
                                <td style="width: 14px">
                                    &nbsp;</td>
                                <td>
                                    <asp:DropDownList ID="DropDownList1" runat="server" Visible="False" 
                                        Width="500px">
                                    </asp:DropDownList>
                                        </td>
                            </tr>
                            </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="heading" colspan="3" style="height: 1px">
        </td>
    </tr>
</table>
