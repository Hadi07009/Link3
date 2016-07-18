<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucActivityComments.ascx.cs" Inherits="usercontrols_ucActivityComments" %>
<style type="text/css">
    .style1
    {
        width: 287px;
    }
    .style2
    {
        height: 10px;
        width: 287px;
    }
    .style3
    {
        width: 287px;
        height: 3px;
    }
    .style4
    {
        width: 287px;
        height: 11px;
    }
</style>
<table style="width: 100%">
    <tr>
        <td rowspan="2" style="vertical-align: top; width: 34px; text-align: left">
            <asp:Image ID="imgimage" runat="server" Height="30px" 
                ImageUrl="~/images/forum_old.gif" Width="30px" />
        </td>
        <td>
            <asp:Label ID="lblname" runat="server" Font-Bold="True" 
                Font-Names="Arial Narrow" Font-Size="10pt" ForeColor="#2A2965" 
                Text="MUHAMMAD MONJURUL ISLAM" Width="265px"></asp:Label>
        </td>
        <td id="celcomm" runat="server" rowspan="2" 
            style="font-size: 10px; font-weight: bold">
        </td>
    </tr>
    <tr>
        <td class="style1">
            <asp:Label ID="lblaction" runat="server" Font-Names="Verdana" Font-Size="8pt" 
                ForeColor="#2A2965" Width="272px"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 34px; height: 10px">
        </td>
        <td>
        </td>
        <td style="height: 10px">
        </td>
    </tr>
</table>

