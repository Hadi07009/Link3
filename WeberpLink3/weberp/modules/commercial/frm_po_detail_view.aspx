<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_po_detail_view.aspx.cs" Inherits="frm_po_detail_view" Title=""   EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table id="tblmaster" runat="server" class="tblmas" style="width: 100%">
        <tr>
            <td style="height: 22px">
            </td>
            <td style="height: 22px">
            </td>
            <td style="height: 22px">
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="3" style="text-align: center">
                PO DETAIL REPORT&nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                REPORT OPTION</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 25px; text-align: center">
                <table style="width:100%;">
                    <tr>
                        <td style="width: 11px">
                            &nbsp;</td>
                        <td style="width: 87px; text-align: left;">
                            PO TYPE</td>
                        <td style="width: 183px; text-align: left;">
                            PLANT</td>
                        <td style="text-align: left; width: 209px">
                            DATE</td>
                        <td style="text-align: left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 11px">
                            &nbsp;</td>
                        <td style="width: 87px; text-align: left;">
                            &nbsp;</td>
                        <td style="width: 183px; text-align: left; vertical-align: top;">
                            &nbsp;</td>
                        <td style="text-align: left; width: 209px">
                            &nbsp;</td>
                        <td style="text-align: left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 11px">
                            &nbsp;</td>
                        <td style="width: 87px; text-align: left; vertical-align: top;">
                            <asp:RadioButtonList ID="rdopoype" runat="server" Width="233px">                                
                                <asp:ListItem Selected="True">LPO</asp:ListItem>
                                <asp:ListItem Value="SPOU" >SPO (Unrealised)</asp:ListItem>
                                <asp:ListItem Value="SPOR">SPO (Realised)</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td style="width: 183px; text-align: left; vertical-align: top;">
                                                                
                                
                            <asp:RadioButtonList ID="rdocode" runat="server" Width="139px">
                                <asp:ListItem>CPMPR</asp:ListItem>
                                <asp:ListItem>CPSRQ</asp:ListItem>
                            </asp:RadioButtonList>
                            
                        </td>
                        <td style="width: 209px; text-align: left; vertical-align: top;">
                            <table style="width:100%;">
                                <tr>
                                    <td>
                                        From</td>
                                    <td style="width: 153px">
                                        <ew:CalendarPopup ID="cldfrom" runat="server" 
                                            Culture="English (United Kingdom)" Width="85px" 
                                            DisableTextBoxEntry="False">
                                            <ButtonStyle CssClass="btn2" />
                                        </ew:CalendarPopup>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        To</td>
                                    <td style="width: 153px">
                                        <ew:CalendarPopup ID="cldto" runat="server" 
                                            Culture="English (United Kingdom)" Width="87px" 
                                            DisableTextBoxEntry="False">
                                            <ButtonStyle CssClass="btn2" />
                                        </ew:CalendarPopup>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td style="width: 153px">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 11px">
                            &nbsp;</td>
                        <td style="width: 87px">
                            &nbsp;</td>
                        <td style="width: 183px">
                            &nbsp;</td>
                        <td style="width: 209px">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="3" style="height: 5px; text-align: center">
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 25px; text-align: center">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 25px; text-align: center">
                <asp:Button ID="btnShow" runat="server" CssClass="btn2" onclick="btnShow_Click" 
                    Text="Show" Width="111px" />
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 25px; text-align: center">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 22px; text-align: left">
                
                
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 8px; text-align: center">
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
    </table>


</asp:Content>

