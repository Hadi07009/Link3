<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmQueryReport.aspx.cs" Inherits="modules_HRMS_Details_frmQueryReport" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="QUERY REPORT" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
                    <tr>
                        <td>
                            <asp:Panel ID="PanelMaster" runat="server" Height="100%" Width="100%">

                                <table style="width: 99%; text-align: left">
                                    <tr>
                                        <td colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td style="width:130px">
                                            <asp:Label ID="Label54" runat="server" Text="Select"></asp:Label>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlQueryType" runat="server" Width="355px" AutoPostBack="True" OnSelectedIndexChanged="ddlQueryType_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:Panel ID="PanelForSelection" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                                                <table style="width: 100%;">


                                                    <tr valign="top">
                                                        <td>
                                                            <asp:Panel ID="Panel14" runat="server" Height="100%" Width="500px">
                                                                <table style="width: 100%; text-align: left">
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td align="left">
                                                                            <asp:Label ID="Label56" runat="server" Font-Bold="True" Text="Available Data Fields :"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td align="left">
                                                                            <asp:ListBox ID="lstBoxField" runat="server" AutoPostBack="True" Height="350px" OnSelectedIndexChanged="lstBoxField_SelectedIndexChanged" SelectionMode="Multiple" Width="350px"></asp:ListBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width:155px">
                                                                            <asp:Label ID="Label53" runat="server" Text="Report Name"></asp:Label>
                                                                        </td>
                                                                        <td style="width:15px">:</td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtReportName" runat="server" Width="350px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td>
                                                                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" Width="100px" />
                                                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="100px" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="Panel15" runat="server" Height="100%" Width="100px">
                                                                <table style="width: 99%; text-align: center">
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            &nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Button ID="btnForwardColumn" runat="server" OnClick="btnForwardColumn_Click" Text="&gt;" Width="50px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Button ID="btnForwardAllColumn" runat="server" OnClick="btnForwardAllColumn_Click" Text="&gt;&gt;" Width="50px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Button ID="btnBackColumn" runat="server" Text="&lt;" Width="50px" OnClick="btnBackColumn_Click" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Button ID="btnBackAllColumn" runat="server" OnClick="btnBackAllColumn_Click" Text="&lt;&lt;" Width="50px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="Panel16" runat="server" Height="100%" Width="500px">
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label57" runat="server" Font-Bold="True" Text="Selected Data Fields :"></asp:Label>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ListBox ID="lstBoxSelectedField" runat="server" AutoPostBack="True" Height="350px" OnSelectedIndexChanged="lstBoxSelectedField_SelectedIndexChanged" SelectionMode="Multiple" Width="350px"></asp:ListBox>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label55" runat="server" Text="Select Report"></asp:Label>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlReport" runat="server" Width="350px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" Width="100px" />
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnExporttoExcel" runat="server" OnClick="btnExporttoExcel_Click" Text="Export to Excel" />
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:GridView ID="grdGetQueryData" runat="server" Width="100%">
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>

                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ddlQueryType" />
            <asp:PostBackTrigger ControlID="lstBoxField" />
            <asp:PostBackTrigger ControlID="lstBoxSelectedField" />
            <asp:PostBackTrigger ControlID="btnForwardAllColumn" />
            <asp:PostBackTrigger ControlID="btnBackAllColumn" />
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnForwardColumn" />
            <asp:PostBackTrigger ControlID="btnBackColumn" />
            <asp:PostBackTrigger ControlID="btnExporttoExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
