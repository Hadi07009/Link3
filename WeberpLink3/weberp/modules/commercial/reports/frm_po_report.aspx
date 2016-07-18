<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_po_report.aspx.cs" Inherits="frm_po_report" Title=""  EnableEventValidation="false" %>

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
                PO REPORT&nbsp;</td>
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
                        <td style="width: 105px; text-align: left;">
                            REQ</td>
                        <td style="width: 129px; text-align: left;">
                            STATUS</td>
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
                        <td style="width: 105px; text-align: left; vertical-align: top;">
                            &nbsp;</td>
                        <td style="width: 129px; text-align: left;">
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
                            <asp:CheckBoxList ID="ChkPOType" runat="server">
                                <asp:ListItem Selected="True">SPO</asp:ListItem>
                                <asp:ListItem Selected="True">LPO</asp:ListItem>
                                <asp:ListItem Selected="True">FPO</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <td style="width: 105px; text-align: left; vertical-align: top;">
                            <asp:CheckBox ID="ChkAllCode" runat="server" AutoPostBack="True" 
                                oncheckedchanged="ChkAllCode_CheckedChanged" Text="ALL" />
                                <asp:UpdatePanel ID="upd1" runat="server">
                                <ContentTemplate>
                                
                                
                            <asp:CheckBoxList ID="chkCode" runat="server">
                                <asp:ListItem>CPMPR</asp:ListItem>
                                <asp:ListItem>CPSRQ</asp:ListItem>
                            </asp:CheckBoxList>
                            </ContentTemplate>
                                <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ChkAllCode" EventName="CheckedChanged" />
                                </Triggers>
                                </asp:UpdatePanel>
                        </td>
                        <td style="width: 129px; text-align: left; vertical-align: top;">
                            <asp:CheckBox ID="ChkAllStatus" runat="server" AutoPostBack="True" 
                                oncheckedchanged="ChkAllStatus_CheckedChanged" Text="ALL" />
                                
                             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                            <asp:CheckBoxList ID="ChkStatus" runat="server">
                                <asp:ListItem>APP</asp:ListItem>
                                <asp:ListItem>CLOSE</asp:ListItem>
                                <asp:ListItem>REVISING</asp:ListItem>
                                <asp:ListItem>CANCELED</asp:ListItem>
                                <asp:ListItem>REJ</asp:ListItem>
                            </asp:CheckBoxList>
                            </ContentTemplate>
                                <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ChkAllStatus" EventName="CheckedChanged" />
                                </Triggers>
                                </asp:UpdatePanel>
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
                        <td style="width: 105px">
                            &nbsp;</td>
                        <td style="width: 129px">
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
                <asp:Button ID="btnShow" runat="server" CssClass="btn2" onclick="Button1_Click" 
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

