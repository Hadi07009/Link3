<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="frm_mat_trn_print.aspx.cs" Inherits="frm_mat_trn_print" Title=""   %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>QUOTATION</title>
    <style type="text/css">
        .style1
        {
            height: 18px;
            }
        .style2
        {
            width: 46px;
        }
        .style4
        {
            height: 18px;
            width: 712px;
            text-align: center;
        }
        .style5
        {
            width: 712px;
        }
        .style6
        {
            height: 18px;
            width: 80px;
        }
        .style7
        {
            width: 80px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>

<table id="tblmaster" runat="server" class="tblmas" style="width: 100%">
        <tr>
            <td class="heading" style="text-align: center">
                <asp:Label ID="lbltype" runat="server" Font-Bold="True" Text="Label" 
                    Width="400px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style=" text-align: left">
                    <table id="tbldet" runat="server" style="width: 913px; text-align: left">
                        <tr>
                            <td class="style1">
                            </td>
                            <td class="style6">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                Party:</td>
                            <td style="vertical-align: top; text-align: left" class="style5">
                                <asp:Label ID="lblparty" runat="server" Font-Bold="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                PO Ref:</td>
                            <td style="vertical-align: top; text-align: left" class="style5">
                                <asp:Label ID="lblporef" runat="server" Font-Bold="False"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                Trn Ref:</td>
                            <td style="vertical-align: top; text-align: left" class="style5">
                                <asp:Label ID="lbltrnref" runat="server" Font-Bold="False"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                Trn By:</td>
                            <td style="vertical-align: top; text-align: left" class="style5">
                                <asp:Label ID="lbltrnby" runat="server" Font-Bold="False"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                DateTime</td>
                            <td style="vertical-align: top; text-align: left" class="style5">
                                <asp:Label ID="lbldate" runat="server" Font-Bold="False"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="style1">
                            </td>
                            <td class="style6">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style1">
                            </td>
                            <td class="style1" colspan="2">
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
                                        <td>
                                            Origin</td>
                                        <td>
                                            Packing</td>
                                        <td style="width: 77px">
                                            Remarks.</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                &nbsp;</td>
                            <td class="style6">
                                &nbsp;</td>
                            <td>

                                </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                &nbsp;</td>
                            <td class="style6">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style1">
                                &nbsp;</td>
                            <td class="style6">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
            </td>
        </tr>
        </table>
 </div>
    </form>
</body>
</html>


