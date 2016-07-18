<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="ConveyanceCalm.aspx.cs" Inherits="modules_HRMS_Details_ConveyanceCalm" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="Conveyance Claim" runat="server" />

            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
                    <tr>
                        <td style="width: 118px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 118px; height: 26px;">
                            <asp:Label ID="Label1" runat="server" Text="Import Excel File"></asp:Label>
                        </td>
                        <td style="height: 26px">:</td>
                        <td style="height: 26px">
                            <asp:FileUpload ID="FileUploadExcelFile" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 118px">
                            <asp:Label ID="Label2" runat="server" Text="Sheet Name"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlSheetName" runat="server" Width="250px" OnSelectedIndexChanged="ddlSheetName_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                            &nbsp;<asp:ImageButton ID="imgBtnExcelUpload" runat="server" Height="15px" ImageUrl="~/Images/imageup.jpg" OnClick="imgBtnExcelUpload_Click" Width="25px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 118px">
                            <asp:Label ID="Label3" runat="server" Text="Payment Period" Font-Italic="True" Font-Overline="False" Font-Strikeout="False" Font-Underline="True"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Import Data" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 118px">
                            <asp:Label ID="Label4" runat="server" Text="From Date"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="popupPaymentPeriodFrom" runat="server" placeholder="From Date" Width="250px"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="popupPaymentPeriodFrom_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="popupPaymentPeriodFrom">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 118px">
                            <asp:Label ID="Label5" runat="server" Text="To Date"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="popupPaymentPeriodTo" runat="server" placeholder="To Date" Width="250px"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="popupPaymentPeriodTo_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="popupPaymentPeriodTo">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 118px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="100px" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="grdGetConveyance" runat="server" Width="100%">
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 118px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 118px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 118px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 118px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgBtnExcelUpload" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="Button1" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
