<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_po_detail_view.ascx.cs" Inherits="ClientSide_modules_commercial_usercontrols_ctl_po_detail_view" %>
<style type="text/css">
	
table
{
	font-size: 1em;
    text-align: left;
}

        .style7
        {
            height: 18px;
            width: 12px;
        }
        .style11
        {
            height: 18px;
            width: 685px;
        }
        .style8
        {
            width: 12px;
        }
        .style12
        {
            width: 685px;
        }
        .style9
        {
            height: 0px;
            width: 12px;
        }
        .style13
        {
            height: 0px;
            width: 685px;
        }
        .style10
        {
            height: 10px;
            width: 12px;
        }
        .style14
        {
            height: 10px;
            width: 685px;
        }
    .auto-style1 {
        height: 21px;
        width: 12px;
    }
    .auto-style2 {
        height: 21px;
        width: 685px;
    }
    </style>
                    <table id="tbldet" runat="server" class="tbl"  
    style="border: thin dotted #000000; width: 773px; background-color:#F8E5A1; text-align: left">
                        <tr>
                            <td>
                            </td>
                            <td class="style11">
                                Date:
                                <asp:Label ID="lbldate" runat="server" Font-Bold="False" Width="138px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td class="style11">
                                PO Ref:
                                <asp:Label ID="lblporef" runat="server" Font-Bold="False" Width="333px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="vertical-align: top; text-align: left" class="style12">
                                To: &nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblto" runat="server" Font-Bold="True" Width="508px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td class="style11">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lbladd" runat="server" Font-Bold="False" Width="508px" 
                                    Height="41px"></asp:Label>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td class="style11">
                                Sub:&nbsp;
                                <asp:Label ID="lblsub" runat="server" Font-Bold="True" Width="508px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td class="style11">
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td class="style11">
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
                        </tr>
                        <tr>
                            <td class="style9">
                            </td>
                            <td class="style13">
                            </td>
                        </tr>
                        <tr>
                            <td class="style9">
                                &nbsp;</td>
                            <td class="style13">
                                <b>Total Amount TK:</b>
                                <asp:Label ID="lbltot" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style9">
                                &nbsp;</td>
                            <td class="style13">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style1">
                            </td>
                            <td  runat="server" class="auto-style2">
                                <asp:Label ID="lblgen" runat="server" Font-Bold="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="style9">
                                &nbsp;</td>
                            <td id="genterms" runat="server" class="style13">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style9">
                                &nbsp;</td>
                            <td class="style13">
                                <asp:Label ID="lblspe" runat="server" Font-Bold="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="style9">
                                &nbsp;</td>
                            <td id="spterms" runat="server" class="style13">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style9">
                            </td>
                            <td class="style13">
                                <asp:Label ID="lblpay" runat="server" Font-Bold="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="style9">
                                &nbsp;</td>
                            <td id="payterms" runat="server" class="style13">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style10">
                                &nbsp;</td>
                            <td id="daycount" runat="server" class="style14">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td class="style11">
                                Thanking you.</td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td class="style11">
                                <asp:Label ID="lblfrom" runat="server" Font-Bold="True" Height="38px" 
                                    Width="129px"></asp:Label><br />
                                <strong>Seven Circle (Bangladesh) Ltd.</strong></td>
                        </tr>
                        </table>
            
