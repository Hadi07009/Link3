<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_po_send_mail.aspx.cs" Inherits="frm_po_send_mail" Title=""   MaintainScrollPositionOnPostback="true"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>PO PRINT</title>
    
    <style type="text/css">
        .style1
        {
            height: 42px;
        }
        .style2
        {
            height: 13px;
        }
        .style5
        {
            font-size: 16pt;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>

<table id="tblmaster" runat="server" class="tblmas" style="width: 100%">
        <tr>
            <td style="height: 22px">
            </td>
        </tr>
        <tr>
            <td class="tbl" style="height: 24px; text-align: center">
            </td>
        </tr>
    <tr>
        <td class="tbl" style="text-align: right">
            &nbsp;</td>
    </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                    <table id="tbldet" runat="server" style="width: 773px;  text-align: left">
                        <tr>
                            <td colspan="3"  style="text-align: center; font-size: 16pt;">
                                City Cable Limited.</td>
                        </tr>
                        <tr>
                            <td style="text-align: center;" colspan="3">
                                Kemal Ataturk Avenue, Banani<st1:street w:st="on">, Dhaka-1212, Telephone:</td>
                        </tr>
                        <tr>
                            <td style="text-align: center;" class="style1" colspan="3">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 18px; text-align: center;" colspan="3">
                                <b>PURCHASE ORDER</b></td>
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
                                Date:
                                <asp:Label ID="lbldate" runat="server" Font-Bold="False" Width="138px"></asp:Label></td>
                            <td style="height: 18px">
                            </td>
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
                                &nbsp;</td>
                            <td style="width: 629px; height: 18px">
                                PO Ref:
                                <asp:Label ID="lblporef" runat="server" Font-Bold="False" Width="374px"></asp:Label></td>
                            <td style="height: 18px">
                                &nbsp;</td>
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
                            <td  runat="server" style="width: 629px; height: 18px">
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
                                ** THIS IS SOFTWARE AUTO GENERATED EMAIL AND DONT REQUIRE A REPLY.
                                </td>
                            <td style="height: 39px">
                            </td>
                        </tr>
                        </table>
            </td>
        </tr>
        </table>
    </div>
                    <table style="width: 773px">
                        <tr>
                            <td>
                            </td>
                            <td style="width: 632px">
                                Mail To:&nbsp;
                                <asp:TextBox ID="txtto" runat="server" CssClass="txtbox" Width="520px"></asp:TextBox></td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="width: 632px">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="width: 632px">
                                CC: &nbsp; &nbsp; &nbsp;&nbsp;
                                <asp:TextBox ID="txtcc" runat="server" CssClass="txtbox" Width="520px"></asp:TextBox></td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="width: 632px">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td style="width: 632px">
                                <asp:RadioButtonList ID="rdoformat" runat="server" RepeatDirection="Horizontal" 
                                    Width="255px">
                                    <asp:ListItem Selected="True" Value="HTML">HTML Format</asp:ListItem>
                                    <asp:ListItem Value="PDF" Enabled="False">PDF Format</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="width: 632px">
                                <asp:Label ID="lblsent" runat="server" ForeColor="Red" Text="Email sent." Visible="False"
                                    Width="169px"></asp:Label></td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="width: 632px">
                                <asp:Button ID="btnsend" runat="server" CssClass="btn2" OnClick="Button1_Click" Text="SEND"
                                    Width="151px" /></td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 103px">
                            </td>
                            <td style="width: 632px; height: 103px">
                            </td>
                            <td style="height: 103px">
                            </td>
                        </tr>
                    </table>
    </form>
</body>
</html>

