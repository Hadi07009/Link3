<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_change_routing.ascx.cs" Inherits="ClientSide_modules_commercial_usercontrols_ctl_change_routing" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>




<table id="tblmas" runat="server" style="width: 100%">
    <tr>
        <td style="height: 10px" colspan="3">
        </td>
    </tr>
    <tr>
        <td colspan="3" class="tblsmall">
            <table style="width: 99%; border-top-width: 1px; border-left-width: 1px; border-left-color: #e6e6fa; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='white', startcolorstr='#D6DCF9', gradienttype='0'); border-bottom-width: 1px; border-bottom-color: #e6e6fa; border-top-color: #e6e6fa; border-right-width: 1px; border-right-color: #e6e6fa;" class="tblsmall">
                <tr>
                    <td  style="text-align: left" >
                        Ref</td>
                    <td class="tblsmall" style="width: 7px; text-align: left">
                        :</td>
                    <td id="celref" runat="server" style="text-align: left">
                    </td>
                    <td rowspan="5" style="text-align: center" class="tblsmall">
                        <asp:DropDownList ID="ddlAction" runat="server" Width="83px" CssClass="tblsmall" Font-Size="XX-Small">
                            <asp:ListItem Selected="True">LPO</asp:ListItem>
                            <asp:ListItem>SPO</asp:ListItem>
                            <asp:ListItem>FPO</asp:ListItem>                            
                            <asp:ListItem>FORWARD</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;
                        <asp:Button ID="btnChange" runat="server" CssClass="btn2" 
                            onclick="btnChange_Click" Text="Change" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" >
                        Item</td>
                    <td class="tblsmall" style="width: 7px; text-align: left">
                        :</td>
                    <td  style="text-align: left" >
                        <asp:Label ID="celitem" runat="server" Font-Bold="True" Text="Label" Width="350px"></asp:Label></td>
                </tr>
                <tr>
                    <td  style="text-align: left" >
                        Quantity</td>
                    <td class="tblsmall" style="width: 7px; text-align: left">
                        :</td>
                    <td style="text-align: left" runat="server" id="celqty" >
                    </td>
                </tr>
                <tr>
                    <td  style="text-align: left;">
                        Purchase Type</td>
                    <td class="tblsmall" style="width: 7px; text-align: left">
                        :</td>
                    <td  style="text-align: left" runat="server" id="celpurtype" >
                        </td>
                </tr>
                <tr>
                    <td  style="text-align: left;" >
                        Current Status</td>
                    <td class="tblsmall" style="width: 7px; text-align: left">
                        :</td>
                    <td ID="celstatus" runat="server"  style="text-align: left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="tblsmall" colspan="4" style="height: 16px; text-align: left">
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
 
