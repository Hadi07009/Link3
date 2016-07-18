<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_do_delivery_report.aspx.cs" Inherits="frm_do_delivery_report"  EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
<%@ Register assembly="TimePicker" namespace="MKB.TimePicker" tagprefix="MKB" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table id="tblmaster" runat="server" class="tblmas" style="width: 100%">
        <tr>
            <td style="height: 22px">
            </td>
            <td style="height: 22px">
            </td>
            <td style="height: 22px; text-align: right;">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="heading" colspan="3" style="text-align: center">
                DO DELIVERY REPORT</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: right">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 25px; text-align: center">
                <table style="width:100%;">
                                        <tr>
                                            <td style="width: 243px; text-align: right; ">PERIOD</td>
                                            <td style="width: 1px; text-align: right; "><b>:</b></td>
                                            <td>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="text-align: right; width: 76px">From:</td>
                                                        <td style="width: 125px">
                                                            <ew:CalendarPopup ID="dtFrom" runat="server" CssClass="txtbox" Culture="English (United Kingdom)" DisableTextBoxEntry="False" Width="76px">
                                                                <ButtonStyle CssClass="btn2" />
                                                            </ew:CalendarPopup>
                                                        </td>
                                                        <td style="text-align: right; width: 41px">To:</td>
                                                        <td style="text-align: left">
                                                            <ew:CalendarPopup ID="dtTo" runat="server" CssClass="txtbox" Culture="English (United Kingdom)" DisableTextBoxEntry="False" Width="76px">
                                                                <ButtonStyle CssClass="btn2" />
                                                            </ew:CalendarPopup>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 76px">
                                                            <asp:CheckBox ID="chktime" runat="server" Text="With Time" TextAlign="Left" 
                                                                Checked="True">
                                                                                                                          
                                                            </asp:CheckBox>
                                                        </td>
                                                        <td style="width: 125px">
                                                            <mkb:timeselector ID="TimeSelectorfrom" runat="server" AmPm="AM" Date="2013-02-27" DisplaySeconds="False" Hour="6">
                                                            </mkb:timeselector>
                                                        </td>
                                                        <td >&nbsp;</td>
                                                        <td>
                                                            <mkb:timeselector ID="TimeSelectorto" runat="server" AmPm="AM" Date="2013-02-27" DisplaySeconds="False" Hour="6">
                                                            </mkb:timeselector>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>

                             <tr>
                                            <td style="width: 243px; text-align: right; ">REPORT TYPE</td>

                                            <td style="width: 1px; text-align: right; "><b>:</b></td>

                                            <td>
                                                <table style="width:100%;">
                                                    <tr>
                                                        <td>
                                                            <table id="Table9" runat="server" style="width:100%;">
                                                                <tr>
                                                                    <td style="width: 53px; text-align: left;">
                                                                        <asp:RadioButtonList ID="rdolistreporttype" runat="server" RepeatDirection="Horizontal" Width="245px" Height="28px" >
                                                                            <asp:ListItem Selected="True" Value="1">Detail</asp:ListItem>
                                                                            <asp:ListItem Value="0">Summery</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                    <td style="width: 97px; text-align: left;">&nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                </table>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    </table>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>

                             <tr>
                                            <td style="width: 243px; text-align: right; ">CEMENT TYPE</td>

                                            <td style="width: 1px; text-align: right; "><b>:</b></td>

                                            <td>
                                                <table style="width:100%;">
                                                    <tr>
                                                        <td>
                                                            <table id="Table7" runat="server" style="width:100%;">
                                                                <tr>
                                                                    <td style="width: 53px; text-align: left;">
                                                                        <asp:RadioButtonList ID="rdolistcementtype" runat="server" RepeatDirection="Horizontal" Width="246px" >
                                                                            <asp:ListItem Selected="True" Value="BAG">Bag Cement</asp:ListItem>
                                                                            <asp:ListItem Value="MTON">Bulk Cement</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                    <td style="width: 97px; text-align: left;">&nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                </table>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    </table>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                            <tr>
                                            <td style="width: 243px; text-align: right; ">MOVEMENT STATUS</td>

                                            <td style="width: 1px; text-align: right; "><b>:</b></td>

                                            <td>
                                                <table style="width:100%;">
                                                    <tr>
                                                        <td>
                                                            <table id="Table8" runat="server" style="width:100%;">
                                                                <tr>
                                                                    <td style="width: 53px; text-align: left;">
                                                                        <asp:RadioButtonList ID="rdolistmovementstatus" runat="server" RepeatDirection="Horizontal" Width="306px" >
                                                                            <asp:ListItem Selected="True" Value="7">Outside Factory</asp:ListItem>
                                                                            <asp:ListItem Value="6">Inside Factory</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                    <td style="width: 97px; text-align: left;">&nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                </table>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    </table>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>

                            <tr>
                                            <td style="width: 243px; text-align: right; ">DO TYPE</td>

                                            <td style="width: 1px; text-align: right; ">:</td>

                                            <td>
                                                                        <asp:RadioButtonList ID="rdolistdotype" runat="server" RepeatDirection="Horizontal" Width="424px" >
                                                                            <asp:ListItem Selected="True" Value="All">All</asp:ListItem>
                                                                            <asp:ListItem Value="DON">Donation</asp:ListItem>
                                                                            <asp:ListItem Value="IUS">Internal Use</asp:ListItem>
                                                                            <asp:ListItem Value="DAM">Damage Replacement</asp:ListItem>
                                                                            <asp:ListItem Value="SAM">Sample</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>

                             <tr>
                                            <td style="width: 243px; text-align: right; ">SALES SECTION</td>

                                            <td style="width: 1px; text-align: right; "><b>:</b></td>

                                            <td>
                                                <table style="width:100%;">
                                                    <tr>
                                                        <td>
                                                            <table id="Table3" runat="server" style="width:100%;">
                                                                <tr>
                                                                    <td style="width: 53px; text-align: left;">
                                                                        <asp:CheckBox ID="chksection" runat="server" Checked="True" Text="ALL" />
                                                                    </td>
                                                                    <td style="text-align: left">
                                                                        <asp:DropDownList ID="ddlsection" runat="server" Width="500px" AutoPostBack="True" OnSelectedIndexChanged="ddlsection_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                </table>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    </table>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                             <tr>
                                            <td style="width: 243px; text-align: right; ">NAME OF PARTY</td>

                                            <td style="width: 1px; text-align: right; "><b>:</b></td>

                                            <td>
                                                <table style="width:100%;">
                                                    <tr>
                                                        <td>
                                                            <table id="Table4" runat="server" style="width:100%;">
                                                                <tr>
                                                                    <td style="width: 53px; text-align: left;">
                                                                        <asp:CheckBox ID="chkparty" runat="server" Checked="True" Text="ALL" />
                                                                    </td>
                                                                    <td style="text-align: left">
                                                                        <asp:DropDownList ID="ddlmainparty" runat="server"  Width="500px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                </table>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    </table>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>

                             <tr>
                                            <td style="width: 243px; text-align: right; ">&nbsp;</td>

                                            <td style="width: 1px; text-align: right; ">&nbsp;</td>

                                            <td>
                                                <table style="width:100%;">
                                                    <tr>
                                                        <td>
                                                            <table id="Table6" runat="server" style="width:100%;">
                                                                <tr>
                                                                    <td style="text-align: left">
                                                                        <asp:Button ID="btnView" runat="server" Text="View" Width="101px" OnClick="btnView_Click"  />
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                </table>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    </table>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 34px; text-align: left">
                &nbsp;</td>
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

