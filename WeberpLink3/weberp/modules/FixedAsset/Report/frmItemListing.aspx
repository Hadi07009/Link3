<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master"  AutoEventWireup="true" CodeFile="frmItemListing.aspx.cs" Inherits="modules_FixedAsset_Report_frmItemListing" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 1114px">
        <tr>
            <td style="width: 483px">
            </td>
            <td align="center" colspan="2">
                Item Listing</td>
            <td class="style1">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 483px">
                &nbsp;</td>
            <td align="center" colspan="2">
                &nbsp;</td>
            <td class="style1">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center" colspan="4">
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                    AutoDataBind="true" OnUnload="CrystalReportViewer1_Unload"  />
            </td>
        </tr>
        <tr>
            <td style="width: 483px">
                &nbsp;</td>
            <td style="width: 95px">
                &nbsp;</td>
            <td style="width: 40px">
                &nbsp;</td>
            <td class="style1">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

