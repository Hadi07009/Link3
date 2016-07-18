<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="frm_tender_inquiry_print.aspx.cs" Inherits="frm_tender_inquiry_print" Title="" EnableEventValidation="false" ValidateRequest ="false"   %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>QUOTATION</title>
    <style type="text/css">
        .style1
        {
            height: 18px;
            width: 12px;
        }
        .style2
        {
            width: 12px;
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
                                    <asp:Image ID="Image1" runat="server" Height="36px" 
                        ImageUrl="~/images/scbl_logo.jpg" Width="48px" />
                
                                    City Cable Limited.</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            40 Kemal Ataturk Avenue,
                            <st1:street w:st="on">Banani, Dhaka-1213, Telephone: 8817690-4<o:p></o:p></td>
                    </tr>
                    <tr>
                        <td class="style5">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="heading" style="text-align: center">
                TENDER INQUIRY</td>
        </tr>
        <tr>
            <td>
                    <table id="tbldet" runat="server" style="width: 773px; text-align: left">
                        <tr>
                            <td class="style1">
                            </td>
                            <td style="width: 629px; height: 18px">
                                Date:
                                <asp:Label ID="lbldate" runat="server" Font-Bold="False" Width="138px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="vertical-align: top; width: 629px; text-align: left">
                                To: &nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblto" runat="server" Font-Bold="True" Width="508px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="style1">
                            </td>
                            <td style="width: 629px; height: 18px">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lbladd" runat="server" Font-Bold="False" Width="508px" 
                                    Height="41px"></asp:Label>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style1">
                                &nbsp;</td>
                            <td style="width: 629px; height: 18px">
                                Phone:&nbsp;
                                <asp:Label ID="lblphone" runat="server" Font-Bold="False" Width="194px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="style1">
                                &nbsp;</td>
                            <td style="width: 629px; height: 18px">
                                Fax:&nbsp;
                                <asp:Label ID="lblfax" runat="server" Font-Bold="False" Width="192px" 
                                    Height="16px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="style1">
                                &nbsp;</td>
                            <td style="width: 629px; height: 18px">
                                Email:&nbsp;
                                <asp:Label ID="lblemail" runat="server" Font-Bold="False" Width="182px" 
                                    Height="16px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="style1">
                                &nbsp;</td>
                            <td style="width: 629px; height: 18px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style1">
                            </td>
                            <td style="width: 629px; height: 18px">
                                Sub:&nbsp;
                                <asp:Label ID="lblsub" runat="server" Font-Bold="True" Width="508px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="style1">
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
                                            Qty</td>
                                        <td style="width: 77px">
                                            Brand</td>
                                        <td style="width: 77px">
                                            Origin</td>
                                        <td style="width: 77px">
                                            Specification</td>
                                        <td>
                                            Remarks</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                            </td>
                            <td id="celterms" runat="server" style="width: 629px; height: 18px">
                                <b>GENERAL TERMS</b></td>
                        </tr>
                        <tr>
                            <td class="style1">
                                &nbsp;</td>
                            <td id="genterms" runat="server" style="width: 629px; height: 18px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style1">
                                &nbsp;</td>
                            <td style="width: 629px; height: 18px">
                                <b>SPECIAL TERMS</b></td>
                        </tr>
                        <tr>
                            <td class="style1">
                                &nbsp;</td>
                            <td id="spterms" runat="server" style="width: 629px; height: 18px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style1">
                            </td>
                            <td style="width: 629px; height: 18px">
                                <b>PAYMENT TERMS (<asp:Label ID="lblpaytype" runat="server" Text="Label"></asp:Label>
                                </b>)</td>
                        </tr>
                        <tr>
                            <td class="style1">
                                &nbsp;</td>
                            <td id="payterms" runat="server" style="width: 629px; height: 18px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style1">
                            </td>
                            <td style="width: 629px; height: 18px">
                                Thanking you.</td>
                        </tr>
                        <tr>
                            <td class="style1">
                            </td>
                            <td style="width: 629px; height: 18px">
                                <asp:Label ID="lblfrom" runat="server" Font-Bold="True" Height="19px" 
                                    Width="495px"></asp:Label><br />
                                <strong>City Cable Limited.</strong></td>
                        </tr>
                        <tr>
                            <td class="style1">
                                &nbsp;</td>
                            <td style="width: 629px; height: 18px">
&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                        </tr>
                    </table>
            </td>
        </tr>
        </table>
 </div>
    </form>
</body>
</html>


