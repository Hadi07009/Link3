<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_po_send_print.aspx.cs" Inherits="frm_po_send_print" Title=""   %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>PO PRINT</title>
    
    <style type="text/css">

 p.MsoNormal
	{margin-bottom:.0001pt;
	font-size:10.0pt;
	font-family:"Times New Roman","serif";
	        margin-left: 0in;
            margin-right: 0in;
            margin-top: 0in;
        }
        .style4
        {
            font-size: 20pt;
            font-family: "Times New Roman", Times, serif;
        }
        .style5
        {
            height: 23px;
        }
        .style6
        {
            font-weight: bold;
            color: white;
            background-color: #41519A;
            font-style: normal;
            font-variant: normal;
            font-size: 11pt;
            line-height: normal;
            font-family: verdana;
        }
        .style11
        {
            height: 18px;
            width: 677px;
        }
        .style12
        {
            width: 677px;
        }
        .style13
        {
            height: 0px;
            width: 677px;
        }
        .style14
        {
            height: 10px;
            width: 677px;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>

<table id="tblmaster" runat="server" class="tblmas" style="width: 100%">
        <tr>
            <td style="text-align: center">
                <table style="width:100%;">
                    <tr>
                        <td style="vertical-align: top; text-align: center;">
                                    City Cable Limited.</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                    Kemal Ataturk Avenue, <st1:street w:st="on"><st1:address w:st="on">
                    Banani</st1:address></st1:Street>-2, Dhaka-1212, Telephone:<o:p></o:p></td>
                    </tr>
                    <tr>
                        <td class="style5">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style6" style="text-align: center">
                PURCHASE ORDER</td>
        </tr>
        <tr>
            <td>
                    <table id="tbldet" runat="server"  
                        style="width: 765px; background-color:#F8E5A1; text-align: left">
                        <tr>
                            <td class="style11">
                                Date:
                                <asp:Label ID="lbldate" runat="server" Font-Bold="False" Width="138px"></asp:Label></td>
                            <td style="height: 18px">
                            </td>
                        </tr>
                        <tr>
                            <td class="style11">
                            </td>
                            <td style="height: 18px">
                            </td>
                        </tr>
                        <tr>
                            <td class="style11">
                                PO Ref:
                                <asp:Label ID="lblporef" runat="server" Font-Bold="False" Width="333px"></asp:Label>
                            </td>
                            <td style="height: 18px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style11">
                                &nbsp;</td>
                            <td style="height: 18px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; text-align: left" class="style12">
                                To: &nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblto" runat="server" Font-Bold="True" Width="508px"></asp:Label></td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="style11">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lbladd" runat="server" Font-Bold="False" Width="508px" 
                                    Height="41px"></asp:Label>&nbsp;</td>
                            <td style="height: 18px">
                            </td>
                        </tr>
                        <tr>
                            <td class="style11">
                                Sub:&nbsp;
                                <asp:Label ID="lblsub" runat="server" Font-Bold="True" Width="508px"></asp:Label></td>
                            <td style="height: 18px">
                            </td>
                        </tr>
                        <tr>
                            <td class="style11">
                            </td>
                            <td style="height: 18px">
                            </td>
                        </tr>
                        <tr>
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
                            <td style="height: 18px">
                            </td>
                        </tr>
                        <tr>
                            <td class="style13">
                            </td>
                            <td style="height: 0px">
                            </td>
                        </tr>
                        <tr>
                            <td class="style13">
                                <b>Total Amount TK:</b>
                                <asp:Label ID="lbltot" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td style="height: 0px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style13">
                                &nbsp;</td>
                            <td style="height: 0px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td  runat="server" class="style13">
                                <asp:Label ID="lblgen" runat="server" Font-Bold="True"></asp:Label></td>
                            <td style="height: 0px">
                            </td>
                        </tr>
                        <tr>
                            <td id="genterms" runat="server" class="style13">
                                &nbsp;</td>
                            <td style="height: 0px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style13">
                                <asp:Label ID="lblspe" runat="server" Font-Bold="True"></asp:Label></td>
                            <td style="height: 0px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td id="spterms" runat="server" class="style13">
                                &nbsp;</td>
                            <td style="height: 0px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style13">
                                <asp:Label ID="lblpay" runat="server" Font-Bold="True"></asp:Label></td>
                            <td style="height: 0px">
                            </td>
                        </tr>
                        <tr>
                            <td id="payterms" runat="server" class="style13">
                                &nbsp;</td>
                            <td style="height: 0px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td id="daycount" runat="server" class="style14">
                                &nbsp;</td>
                            <td style="height: 10px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style11">
                                Thanking you.</td>
                            <td style="height: 18px">
                            </td>
                        </tr>
                        <tr>
                            <td class="style11">
                                <br />
                                <br />
                                <br />
                            </td>
                            <td style="height: 18px">
                            </td>
                        </tr>
                        <tr>
                            <td class="style11">
                                <asp:Label ID="lblfrom" runat="server" Font-Bold="True" Height="38px" 
                                    Width="129px"></asp:Label><br />
                                <strong>Seven Circle (Bangladesh) Ltd.</strong></td>
                            <td style="height: 18px">
                            </td>
                        </tr>
                        </table>
            </td>
        </tr>
        </table>
    </form>
</body>
</html>

