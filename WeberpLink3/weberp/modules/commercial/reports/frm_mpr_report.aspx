<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_mpr_report.aspx.cs" Inherits="frm_mpr_report" Title=""  EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>


<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>


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
                MPR REPORT&nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 25px; text-align: center">
                <table style="width:100%;">
                    <tr>
                        <td style="width: 46px">
                            &nbsp;</td>
                        <td style="width: 226px; text-align: left;">
                            MPR</td>
                        <td style="width: 78px">
                            &nbsp;</td>
                        <td style="text-align: left">
                            STATUS</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 46px">
                            &nbsp;</td>
                        <td style="width: 226px; text-align: left; vertical-align: top;">
                            <asp:RadioButtonList ID="radType" runat="server">
                                <asp:ListItem>Manual</asp:ListItem>
                                <asp:ListItem>Auto</asp:ListItem>
                                <asp:ListItem Selected="True">Both</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td style="width: 78px">
                            &nbsp;</td>
                        <td style="text-align: left; vertical-align: top;">
                            <asp:CheckBoxList ID="chkStatus" runat="server">
                                <asp:ListItem Selected="True" Value="APP">Approved</asp:ListItem>
                                <asp:ListItem Selected="True" Value="RUN">Running</asp:ListItem>
                                <asp:ListItem Selected="True" Value="REJ">Rejected</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 46px">
                            &nbsp;</td>
                        <td style="width: 226px; text-align: left; vertical-align: top;">
                            &nbsp;</td>
                        <td style="width: 78px">
                            &nbsp;</td>
                        <td style="text-align: left; vertical-align: top;">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="heading" colspan="5" style="height: 5px">
                            </td>
                    </tr>
                    <tr>
                        <td style="width: 46px">
                            &nbsp;</td>
                        <td style="width: 226px; text-align: left;">
                            &nbsp;</td>
                        <td style="width: 78px">
                            &nbsp;</td>
                        <td style="text-align: left">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 46px">
                            &nbsp;</td>
                        <td style="width: 226px; text-align: left;">
                            MRR</td>
                        <td style="width: 78px">
                            &nbsp;</td>
                        <td style="text-align: left">
                            DATE</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 46px">
                            &nbsp;</td>
                        <td style="width: 226px; text-align: left">
                            <asp:CheckBox ID="chkAllPlant" runat="server" AutoPostBack="True" 
                                oncheckedchanged="chkAllPlant_CheckedChanged" Text="ALL" />
                                <asp:UpdatePanel ID="upd" runat="server">
                                <ContentTemplate>
                                
                                
                            <asp:CheckBoxList ID="chkPlant" runat="server">
                                <asp:ListItem>CPMPR</asp:ListItem>
                                <asp:ListItem>WTMPR</asp:ListItem>
                                <asp:ListItem>BPMPR</asp:ListItem>
                                <asp:ListItem>CCMPR</asp:ListItem>
                                <asp:ListItem>RTMPR</asp:ListItem>
                                <asp:ListItem>FSMPR</asp:ListItem>
                                <asp:ListItem>CWMPR</asp:ListItem>
                            </asp:CheckBoxList>
                            </ContentTemplate>
                                <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="chkAllPlant" EventName="CheckedChanged"  />
                                </Triggers>
                                </asp:UpdatePanel>
                        </td>
                        <td style="width: 78px">
                            &nbsp;</td>
                        <td style="text-align: left; vertical-align: top;">
                            <table style="width:100%;">
                                <tr>
                                    <td>
                                        From</td>
                                    <td>
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
                                    <td>
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
                                    <td>
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
                        <td style="width: 46px">
                            &nbsp;</td>
                        <td style="width: 226px; text-align: left">
                            &nbsp;</td>
                        <td style="width: 78px">
                            &nbsp;</td>
                        <td style="text-align: left">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="height: 5px;" class="heading" colspan="5">
                            </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 16px; text-align: center">
                
                
                </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" 
                style="border: medium solid #F8E5A1; height: 34px; text-align: center">
                
                
                <asp:Button ID="btnShow" runat="server" CssClass="btn2" onclick="btnShow_Click" 
                   Text="Show" Width="79px" />
                
                
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 12px; text-align: left">
                
                
                </td>
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

