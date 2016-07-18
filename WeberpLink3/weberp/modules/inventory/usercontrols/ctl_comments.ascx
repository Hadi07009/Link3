<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_comments.ascx.cs" Inherits="ClientSide_modules_inventory_usercontrols_ctl_comments" %>
<table>
    <tr>
        <td rowspan="3" style="vertical-align: top; width: 34px; text-align: left">
            <asp:image runat="server" Height="30px" Width="30px" 
                ImageUrl="~/images/forum_old.gif" id="imgimage" /></td>
        <td style="width: 231px">
            <asp:Label ID="lblname" runat="server" Font-Bold="False" Font-Names="Arial Narrow"
                Font-Size="10pt" ForeColor="#2A2965" Text="MUHAMMAD MONJURUL ISLAM" Width="186px"></asp:Label></td>
        <td id="celcomm" runat="server" rowspan="3">
        </td>
    </tr>
    <tr>
        <td style="width: 231px">
        </td>
    </tr>
    <tr>
        <td style="width: 231px">
            <asp:Label ID="lbldate" runat="server" Font-Names="Verdana" Font-Size="9pt" ForeColor="#2A2965"
                Text="2/06/2008 10:28:18 AM " Width="179px"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 34px; height: 10px">
        </td>
        <td style="width: 231px; height: 10px">
        </td>
        <td style="height: 10px">
        </td>
    </tr>
</table>
