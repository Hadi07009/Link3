<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucEmployeeInformation.ascx.cs" Inherits="UserControls_ucEmployeeInformation" %>
<style type="text/css">
    .ucbgcolorfont {
        background-color: silver;
        font-family: 'Helvetica Neue LT';
        font-size: 12pt;
    }

    .ucColStyle {
        background-color:#149BF7;
        font-family: 'Helvetica Neue LT';
        font-size: 12pt;
        text-align-last: center;
    }
    .img-circle {
        border-radius: 50%;

    .ucBorderStyle {
        border-left-width: 0px;
        border-right-width: 0px;
        border-bottom: solid;
        border-top: solid;
        border-bottom-width: thin;
        border-top-width: thin;
        border-bottom-color: navy;
        border-top-color: navy;
        background-color: silver;
        font-family: 'Helvetica Neue LT';
    }

    span {
        font-family:'Helvetica Neue LT';
        font-size:small;
        font-weight: normal;
        color: #000000;
        text-align: left;
    }

    .auto-style1 {
        padding-left:5px;
    }

    .auto-style2 {
        width: 9px;
        
    }
</style>


    <table style="width: 99%; text-align: left; background-color:white; height:300px" class="ucBorderStyle">
        <tr style="height: 25px">
            <td class="ucColStyle" colspan="5" style="font-family:'Helvetica Neue LT'">
               EMPLOYEE BASIC INFORMATION</td>
        </tr>
        
        <tr style="height: 25px">
            <td class="auto-style1">
                &nbsp;</td>
            <td class="auto-style1">
                <asp:Label ID="Label8" runat="server" Text="ID"></asp:Label>
            </td>
            <td style="width:2px">:</td>
            <td>
                <asp:Label ID="lblId" runat="server"></asp:Label>
            </td>
            <td rowspan="4">
                <asp:Image ID="Image1" CssClass="img-circle" runat="server" Height="100px" Width="100px" />
            </td>
        </tr>
        <tr style="height: 25px">
            <td class="auto-style1">
                &nbsp;</td>
            <td class="auto-style1">
                <asp:Label ID="Label12" runat="server" Text="Joining Date"></asp:Label>
            </td>
            <td class="auto-style2">:</td>
            <td>
                <asp:Label ID="lblJoiningDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr style="height: 25px">
            <td class="auto-style1">
                &nbsp;</td>
            <td class="auto-style1">
                <asp:Label ID="Label28" runat="server" Text="Date Of Birth"></asp:Label>
            </td>
            <td class="auto-style2">:</td>
            <td>
                <asp:Label ID="lblDateOfBirth" runat="server"></asp:Label>
            </td>
        </tr>
        <tr style="height: 25px">
            <td class="auto-style1">
                &nbsp;</td>
            <td class="auto-style1">
                <asp:Label ID="Label14" runat="server" Text="Confirm Date"></asp:Label>
            </td>
            <td class="auto-style2">:</td>
            <td>
                <asp:Label ID="lblConfirmDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr style="height: 25px">
            <td class="auto-style1">
                &nbsp;</td>
            <td class="auto-style1">
                <asp:Label ID="Label9" runat="server" Text="Name"></asp:Label>
            </td>
            <td class="auto-style2">:</td>
            <td colspan="2">
                <asp:Label ID="lblName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr style="height: 25px">
            <td class="auto-style1">
                &nbsp;</td>
            <td class="auto-style1">
                <asp:Label ID="Label10" runat="server" Text="Department"></asp:Label>
            </td>
            <td class="auto-style2">:</td>
            <td colspan="2">
                <asp:Label ID="lbldept" runat="server"></asp:Label>
            </td>
        </tr>
        <tr style="height: 25px">
            <td class="auto-style1">
                &nbsp;</td>
            <td class="auto-style1">
                <asp:Label ID="Label11" runat="server" Text="Designation"></asp:Label>
            </td>
            <td class="auto-style2">:</td>
            <td colspan="2">
                <asp:Label ID="lblDesignation" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
