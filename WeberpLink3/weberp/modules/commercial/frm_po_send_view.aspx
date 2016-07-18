<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_po_send_view.aspx.cs" Inherits="frm_po_send_view" Title=""   %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table id="tblmaster" runat="server" class="tblmas" style="width: 100%">
        <tr>
            <td style="height: 22px">
            </td>
        </tr>
        <tr>
            <td class="heading" style="text-align: center">
                PURCHASE ORDER SCREEN&nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" style="height: 24px; text-align: center">
            </td>
        </tr>
    <tr>
        <td class="tbl" style="text-align: right">
        <table style="width: 547px">
                    <tr>
                        <td style="text-align: right">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/Print.gif" /><asp:LinkButton
                                ID="lnkView" runat="server"
                                Width="122px" OnClick="lnkView_Click">View Print Version</asp:LinkButton></td>
                        <td style="width: 11px">
                        </td>
                        <td style="text-align: right; width: 141px;">
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/images/mail.gif" /><asp:LinkButton
                                ID="lnkMail" runat="server" OnClick="lnkMail_Click" Width="86px" Height="16px">Send by mail </asp:LinkButton></td>
                    </tr>
                </table>
        </td>
    </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                    <table style="width: 773px; text-align: left">
                        <tr>
                            <td style="height: 18px">
                            </td>
                            <td style="width: 629px; height: 18px">
                                Date:
                                <asp:Label ID="lbldate" runat="server" Font-Bold="False" Width="138px"></asp:Label></td>
                            <td style="height: 18px">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 18px">
                            </td>
                            <td style="width: 629px; height: 18px">
                            </td>
                            <td style="height: 18px">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 18px">
                                &nbsp;</td>
                            <td style="width: 629px; height: 18px">
                                PO Ref:
                                <asp:Label ID="lblporef" runat="server" Font-Bold="False" Width="315px"></asp:Label>
                            </td>
                            <td style="height: 18px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="height: 18px">
                                &nbsp;</td>
                            <td style="width: 629px; height: 18px">
                                &nbsp;</td>
                            <td style="height: 18px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="vertical-align: top; width: 629px; text-align: left">
                                To: &nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblto" runat="server" Font-Bold="True" Width="508px"></asp:Label></td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 18px">
                            </td>
                            <td style="width: 629px; height: 18px">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lbladd" runat="server" Font-Bold="False" Width="508px" 
                                    Height="41px"></asp:Label>&nbsp;</td>
                            <td style="height: 18px">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 18px">
                            </td>
                            <td style="width: 629px; height: 18px">
                                Sub:&nbsp;
                                <asp:Label ID="lblsub" runat="server" Font-Bold="True" Width="508px"></asp:Label></td>
                            <td style="height: 18px">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 18px">
                            </td>
                            <td style="width: 629px; height: 18px">
                            </td>
                            <td style="height: 18px">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 18px">
                            </td>
                            <td style="width: 629px; height: 18px">
                                <table id="tblhtml" runat="server" border="1" bordercolor="#41519A" cellspacing="0"
                                    style="width: 99%">
                                    <tr>
                                        <td style="width: 27px">
                                            Sl</td>
                                        <td style="width: 309px">
                                            Description &amp; specification of Items.</td>
                                        <td style="width: 77px">
                                            Specification</td>
                        <td>
                            Brand</td>
                        <td>
                            Origin</td>
                        <td>
                            Packing</td>
                        <td>
                            Qty</td>
                        <td>
                            Rate</td>
                        <td>
                            Amount</td>
                                    </tr>
                                </table>
                            </td>
                            <td style="height: 18px">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 18px">
                            </td>
                            <td style="width: 629px; height: 18px">
                            </td>
                            <td style="height: 18px">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 18px">
                                &nbsp;</td>
                            <td style="width: 629px; height: 18px">
                                <b>Total Amount Tk:</b>
                                <asp:Label ID="lbltot" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td style="height: 18px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="height: 18px">
                                &nbsp;</td>
                            <td style="width: 629px; height: 18px">
                                &nbsp;</td>
                            <td style="height: 18px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="height: 18px">
                            </td>
                            <td runat="server" style="width: 629px; height: 18px">
                                <asp:Label ID="lblgen" runat="server" Font-Bold="True"></asp:Label></td>
                            <td style="height: 18px">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 18px">
                                &nbsp;</td>
                            <td id="genterms" runat="server" style="width: 629px; height: 18px">
                                &nbsp;</td>
                            <td style="height: 18px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="height: 18px">
                                &nbsp;</td>
                            <td style="width: 629px; height: 18px">
                                <asp:Label ID="lblspe" runat="server" Font-Bold="True"></asp:Label></td>
                            <td style="height: 18px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="height: 18px">
                                &nbsp;</td>
                            <td id="spterms" runat="server" style="width: 629px; height: 18px">
                                &nbsp;</td>
                            <td style="height: 18px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="height: 18px">
                            </td>
                            <td style="width: 629px; height: 18px">
                                <asp:Label ID="lblpay" runat="server" Font-Bold="True"></asp:Label></td>
                            <td style="height: 18px">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 18px">
                                &nbsp;</td>
                            <td id="payterms" runat="server" style="width: 629px; height: 18px">
                                &nbsp;</td>
                            <td style="height: 18px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="height: 18px">
                                &nbsp;</td>
                            <td style="width: 629px; height: 18px">
                                </td>
                            <td style="height: 18px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="height: 18px">
                                &nbsp;</td>
                            <td id="daycount" runat="server" style="width: 629px; height: 18px">
                                &nbsp;</td>
                            <td style="height: 18px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="height: 18px">
                                &nbsp;</td>
                            <td style="width: 629px; height: 18px">
                                &nbsp;</td>
                            <td style="height: 18px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="height: 18px">
                            </td>
                            <td style="width: 629px; height: 18px">
                                Thanking you.</td>
                            <td style="height: 18px">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 18px">
                            </td>
                            <td style="width: 629px; height: 18px">
                            </td>
                            <td style="height: 18px">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 18px">
                            </td>
                            <td style="width: 629px; height: 18px">
                                <asp:Label ID="lblfrom" runat="server" Font-Bold="True" Height="38px" 
                                    Width="129px"></asp:Label><br />
                                <strong>Seven Circle (Bangladesh) Ltd.</strong></td>
                            <td style="height: 18px">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 39px">
                            </td>
                            <td style="width: 629px; height: 39px">
                            </td>
                            <td style="height: 39px">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 18px">
                                &nbsp;</td>
                            <td style="width: 629px; height: 18px">
&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            <td style="height: 18px">
                                &nbsp;</td>
                        </tr>
                    </table>
            </td>
        </tr>
        <tr>
            <td style="height: 92px">
            </td>
        </tr>
    </table>
</asp:Content>

