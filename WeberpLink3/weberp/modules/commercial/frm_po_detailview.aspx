<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_po_detailview.aspx.cs" Inherits="frm_po_detailview" Title=""   %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
  
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="HTMLEditor" %>
<%@ Register src="usercontrols/ctl_po_item_mrr_view.ascx" tagname="ctl_po_item_mrr_view" tagprefix="uc1" %>    
  
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
        </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>

 <table class="tblmas" style="width: 100%" id="tblmaster" runat="server">
        <tr>
            <td style="height: 22px">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
            <td style="height: 22px">
            </td>
            <td style="height: 22px">
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="3" style="text-align: center">
                PURCHASE ORDER DETAIL SCREEN&nbsp;&nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: right">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: left">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: left">
            
                <table id="tbl_data_det" runat="server" 
                    
                    
                    style="border-color: #e6e6fa; border-width: 1px; width: 1198px; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='white', startcolorstr='#D6DCF9', gradienttype='0'); ">
                    <tr>
                        <td style="width: 58px">
                        </td>
                        <td style="width: 9px">
                        </td>
                        <td style="width: 22px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <table id="tbl_po_det" runat="server" style="width:100%;">
                                <tr>
                                    <td>
                            <table style="width: 620px">
                                <tr>
                                    <td style="width: 106px">
                                        PO Ref</td>
                                    <td style="width: 13px">
                                        :</td>
                                    <td>
                                        <asp:Label ID="lblref" runat="server" Font-Bold="True" Width="168px" 
                                            ForeColor="Red"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 106px">
                                        PO Date</td>
                                    <td style="width: 13px">
                                        :</td>
                                    <td>
                                        <asp:Label ID="lblpodate" runat="server" Text="Label" Width="217px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 106px">
                                        Req Type</td>
                                    <td style="width: 13px">
                                        :</td>
                                    <td>
                                        <asp:Label ID="lblreqtype" runat="server" Text="Label" Width="217px"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 106px">
                                        Purchase Type</td>
                                    <td style="width: 13px">
                                        :</td>
                                    <td>
                                        <asp:Label ID="lblpurtype" runat="server" Text="Label" Width="217px"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 106px">
                                        Party</td>
                                    <td style="width: 13px">
                                        :</td>
                                    <td>
                                        <asp:Label ID="lblparty" runat="server" Text="Label" Width="440px"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 106px">
                                        Amount</td>
                                    <td style="width: 13px">
                                        :</td>
                                    <td>
                                        <asp:Label ID="lblamount" runat="server" Text="Label" Width="139px"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 106px">
                                        Inward</td>
                                    <td style="width: 13px">
                                        :</td>
                                    <td>
                                        <asp:Label ID="lblinward" runat="server" Text="Label" Width="440px" 
                                            Font-Bold="True"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 106px">
                                        Status</td>
                                    <td style="width: 13px">
                                        :</td>
                                    <td>
                                        <asp:Label ID="lblstatus" runat="server" style="font-weight: 700" 
                                            Text="Label" Width="113px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; font-size: medium;">
                                        <b><strong style="font-size: small">PO DETAILS (LPO)</strong></b></td>
                                </tr>
                                <tr>
                                    <td>
                                        <table id="tblspounreal" runat="server" style="width:100%;">
                                            <tr>
                                                <td class="style1">
                                                    &nbsp;</td>
                                                <td>
                                                    Total Amount</td>
                                                <td class="style3">
                                                    :</td>
                                                <td>
                                                    <asp:Label ID="lbltotamount" runat="server" Font-Bold="False" Text="Label" 
                                                        Width="300px"></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="style1">
                                                    &nbsp;</td>
                                                <td>
                                                    In Word</td>
                                                <td class="style3">
                                                    :</td>
                                                <td>
                                                    <asp:Label ID="lblinword" runat="server" Font-Bold="False" Text="Label" 
                                                        Width="594px"></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="style1">
                                                    &nbsp;</td>
                                                <td>
                                                    ADV To</td>
                                                <td class="style3">
                                                    :</td>
                                                <td>
                                                    <asp:Label ID="lbladvto" runat="server" Font-Bold="False" Text="Label" 
                                                        Width="594px"></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="style1">
                                                    &nbsp;</td>
                                                <td>
                                                    Date</td>
                                                <td class="style3">
                                                    :</td>
                                                <td>
                                                    <asp:Label ID="lblpay_date" runat="server" Font-Bold="False" Text="Label" 
                                                        Width="594px"></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="style1">
                                                    &nbsp;</td>
                                                <td>
                                                    Status</td>
                                                <td class="style3">
                                                    &nbsp;</td>
                                                <td>
                                                    <asp:Label ID="lblpay_status" runat="server" Font-Bold="False" Text="Label" 
                                                        Width="594px"></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="style1">
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td class="style3">
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="style1" colspan="4">
                                                    <asp:GridView ID="gdItem" runat="server" BackColor="White" 
                                                        BorderColor="#41519A" BorderStyle="Solid" BorderWidth="10px" CellPadding="4" 
                                                        ForeColor="#333333" GridLines="None" PageSize="100" SkinID="GridView" 
                                                        style="border-top-width: 1px; border-left-width: 1px; border-left-color: #e6e6fa; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='white', startcolorstr='#D6DCF9', gradienttype='0'); border-bottom-width: 1px; border-bottom-color: #e6e6fa; border-top-color: #e6e6fa; border-right-width: 1px; border-right-color: #e6e6fa;" 
                                                        Width="98%">
                                                        <FooterStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#41519A" ForeColor="White" HorizontalAlign="Center" />
                                                        <SelectedRowStyle Font-Bold="True" />
                                                        <HeaderStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                                                        <EditRowStyle BackColor="Lavender" Font-Size="8pt" Font-Strikeout="False" />
                                                        <AlternatingRowStyle BackColor="Lavender" Font-Size="8pt" 
                                                            Font-Underline="False" />
                                                        <RowStyle Font-Size="8pt" />
                                                    </asp:GridView>
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center">
                            <span style="font-size: 10pt; color: #000099">
                                        <strong>ITEMS DETAIL</strong></span></td>
                                </tr>
                                <tr>
                                    <td>
                                <asp:PlaceHolder ID="celdetail" runat="server"></asp:PlaceHolder>                            
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center">
                            <span style="font-size: 10pt; color: #000099">
                                        <strong>TERMS &amp; CONDITIONS</strong></span> </td>
                                </tr>
                                <tr>
                                    <td>
                            <span style="font-size: 10pt; color: #000099">
                                        <strong>GENERAL TERMS </strong>
                            </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="celgent" runat="server">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                            <span style="font-size: 10pt; color: #000099">
                                        <strong>SPECIAL TERMS</strong></span></td>
                                </tr>
                                <tr>
                                    <td id="celspet" runat="server">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                            <span style="font-size: 10pt; color: #000099">
                                        <strong>PAYMENT TERMS</strong></span></td>
                                </tr>
                                <tr>
                                    <td  id="celpayt" runat="server">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td >
                                        &nbsp;</td>
                                </tr>
                                </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td  colspan="3" style="text-align: center">
                            &nbsp;</td>
                    </tr>
                    </table>
          
           
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: left">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="height: 57px">
            </td>
            <td style="height: 57px">
            </td>
            <td style="height: 57px">
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

