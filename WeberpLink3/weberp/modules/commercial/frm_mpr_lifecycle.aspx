<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_mpr_lifecycle.aspx.cs" Inherits="frm_mpr_lifecycle" Title=""   %>
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
        .style7
        {
            width: 77px;
        }
        .style8
        {
            width: 88px;
        }
        .style9
        {
            width: 13px;
        }
        </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>

<table id="tblmaster" runat="server" class="tblmas" style="width: 100%">
        <tr>
            <td style="text-align: center">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style6" style="text-align: center">
                MPR STATUS DETAIL</td>
        </tr>
        <tr>
            <td>
                    &nbsp;</td>
        </tr>
        <tr>
            <td>
                    <table style="width:100%;">
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                MPR Date</td>
                            <td class="style9">
                                :</td>
                            <td>
                                <asp:Label ID="lblmprdate" runat="server"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                MPR Ref No</td>
                            <td class="style9">
                                :</td>
                            <td>
                                <asp:LinkButton ID="lnkref" runat="server"></asp:LinkButton>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                Item</td>
                            <td class="style9">
                                :</td>
                            <td>
                                <asp:Label ID="lblitm" runat="server"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                Quantity</td>
                            <td class="style9">
                                :</td>
                            <td>
                                <asp:Label ID="lblqty" runat="server"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                Specification</td>
                            <td class="style9">
                                :</td>
                            <td>
                                <asp:Label ID="lblspecification" runat="server"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                Brand</td>
                            <td class="style9">
                                :</td>
                            <td>
                                <asp:Label ID="lblbrand" runat="server"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                Origin</td>
                            <td class="style9">
                                :</td>
                            <td>
                                <asp:Label ID="lblorigin" runat="server"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                Packing</td>
                            <td class="style9">
                                :</td>
                            <td>
                                <asp:Label ID="lblpacking" runat="server"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                Nature of Exp</td>
                            <td class="style9">
                                :</td>
                            <td>
                                <asp:Label ID="lblnoe" runat="server"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                Loc of use </td>
                            <td class="style9">
                                :</td>
                            <td>
                                <asp:Label ID="lblloc" runat="server"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                ETR</td>
                            <td class="style9">
                                :</td>
                            <td>
                                <asp:Label ID="lbletr" runat="server"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                Remarks</td>
                            <td class="style9">
                                :</td>
                            <td>
                                <asp:Label ID="lblremarks" runat="server"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                Status</td>
                            <td class="style9">
                                :</td>
                            <td>
                                <asp:Label ID="lblstatus" runat="server"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                    &nbsp;</td>
        </tr>
        <tr>
            <td class="heading" style="text-align: center">
                    PO AND MRR DETAIL</td>
        </tr>
        <tr>
            <td>
                    &nbsp;</td>
        </tr>
        <tr>
            <td>
                 <asp:GridView ID="gdItem" runat="server"  Width="700px"
                    BackColor="White" BorderColor="#41519A" BorderStyle="Solid" BorderWidth="1px" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" 
                    PageSize="100" SkinID="GridView" 
                    style="border-color: #e6e6fa; border-width: 1px; text-align: left;" 
                        ShowHeader="False">
                    <FooterStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#41519A" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle Font-Bold="True" />
                    <HeaderStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="Lavender" Font-Size="8pt" 
                        Font-Underline="False" />
                    <RowStyle Font-Size="8pt" Wrap="True" />
                  
               </asp:GridView>
                            </td>
        </tr>
        <tr>
            <td>
                    &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: center">
                    &nbsp;</td>
        </tr>
        <tr>
            <td>
                 &nbsp;</td>
        </tr>
        <tr>
            <td>
                    &nbsp;</td>
        </tr>
        <tr>
            <td>
                    &nbsp;</td>
        </tr>
        </table>
    </form>
</body>
</html>

