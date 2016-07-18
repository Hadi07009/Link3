<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" EnableEventValidation="false" ValidateRequest="false" CodeFile="frmDepreciationReportViewer.aspx.cs" Inherits="modules_FixedAsset_Report_frmDepreciationReportViewer" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 1114px">
        <tr>
            <td style="width: 483px">
            </td>
            <td align="center" colspan="2">
                Item Depreciation Report</td>
            <td class="style1">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 483px">
                &nbsp;</td>
            <td align="center" colspan="2">
                &nbsp;</td>
            <td class="style1" style="text-align: right">
                <asp:Button ID="btnClose" runat="server" onclick="btnClose_Click" 
                    Text="Close Report" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4">
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                    AutoDataBind="true"  />
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

