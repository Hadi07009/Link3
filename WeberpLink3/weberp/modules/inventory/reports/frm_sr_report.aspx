<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_sr_report.aspx.cs" Inherits="frm_sr_report" Title=""   %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>


<%@ Register assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2" namespace="eWorld.UI" tagprefix="ew" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
<table id="tblmaster" runat="server" class="tblmas" style="width: 100%">
        <tr>
            <td style="height: 22px">
            </td>
        </tr>
        <tr>
            <td class="heading" style="text-align: center">
                STORE REQUISITION REPORT</td>
        </tr>
        <tr>
            <td class="tbl" style="height: 24px; text-align: center">
                            <table style="width:100%;">
                                <tr>
                                    <td style="width: 54px; text-align: left">
                                        &nbsp;</td>
                                    <td style="width: 111px; text-align: left">
                                        &nbsp;</td>
                                    <td style="width: 11px">
                                        &nbsp;</td>
                                    <td colspan="4" style="text-align: left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 54px; text-align: left">
                                        &nbsp;</td>
                                    <td style="width: 111px; text-align: left">
                                        DATE FROM</td>
                                    <td style="width: 11px">
                                        :</td>
                                    <td colspan="4" style="text-align: left">
                                        <ew:CalendarPopup ID="cldfrom" runat="server" 
                                            Culture="English (United Kingdom)" Width="85px" 
                                            DisableTextBoxEntry="False">
                                            <ButtonStyle CssClass="btn2" />
                                        </ew:CalendarPopup>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 54px; text-align: left">
                                        &nbsp;</td>
                                    <td style="width: 111px; text-align: left">
                                        DATE TO</td>
                                    <td style="width: 11px">
                                        :</td>
                                    <td colspan="4" style="text-align: left">
                                        <ew:CalendarPopup ID="cldto" runat="server" 
                                            Culture="English (United Kingdom)" Width="87px" 
                                            DisableTextBoxEntry="False">
                                            <ButtonStyle CssClass="btn2" />
                                        </ew:CalendarPopup>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 54px; text-align: left;">
                                        &nbsp;</td>
                                    <td style="width: 111px; text-align: left;">
                                        TYPE</td>
                                    <td style="width: 11px">
                                        :</td>
                                    <td style="text-align: left;" colspan="4">
                                    <asp:UpdatePanel runat="server" ID="updrpt" >
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlplantlist" runat="server" CssClass="txtbox" 
                                            Width="300px" AutoPostBack="True" 
                                            onselectedindexchanged="ddlplantlist_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td style="width: 54px">
                                        &nbsp;</td>
                                    <td style="width: 111px">
                                        &nbsp;</td>
                                    <td style="width: 11px">
                                        &nbsp;</td>
                                    <td style="width: 151px">
                                        &nbsp;</td>
                                    <td style="width: 36px">
                                        &nbsp;</td>
                                    <td style="width: 11px">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 54px">
                                        &nbsp;</td>
                                    <td style="width: 111px">
                                        &nbsp;</td>
                                    <td style="width: 11px">
                                        &nbsp;</td>
                                    <td style="width: 151px">
                                        &nbsp;</td>
                                    <td style="width: 36px">
                                        &nbsp;</td>
                                    <td style="width: 11px">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 54px">
                                        &nbsp;</td>
                                    <td style="width: 111px">
                                        &nbsp;</td>
                                    <td style="width: 11px">
                                        &nbsp;</td>
                                    <td style="width: 151px; ">
                                        <asp:Button ID="btnview" runat="server" CssClass="btn2" onclick="btnview_Click" 
                                            Text="View" Width="128px" />
                                    </td>
                                    <td style="width: 36px">
                                        &nbsp;</td>
                                    <td style="width: 11px">
                                        &nbsp;</td>
                                    <td>
                                        
                                        &nbsp;</td>
                                    <td style="width: 54px">
                                        &nbsp;</td>
                                    <td style="width: 111px">
                                        &nbsp;</td>
                                    <td style="width: 11px">
                                        &nbsp;</td>
                                    <td style="width: 151px; text-align: left">
                                        &nbsp;</td>
                                    <td style="width: 36px">
                                        &nbsp;</td>
                                    <td style="width: 11px">
                                        &nbsp;</td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
            </td>
        </tr>
    <tr>
        <td class="tbl" style="text-align: left">
            <div style="text-align: center">
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                    HasCrystalLogo="False" HasToggleGroupTreeButton="False" Height="50px" 
                    ReuseParameterValuesOnRefresh="True" style="text-align: left" 
                    ToolbarStyle-BorderStyle="None" Width="350px" ToolPanelView="None" 
                    onunload="CrystalReportViewer1_Unload" />
            </div>
        </td>
    </tr>
        <tr>
            <td>
                
                
                &nbsp;</td>
        </tr>
        <tr>
            <td>
            
                 &nbsp;</td>
        </tr>
        <tr>
            <td>
                 &nbsp;</td>
        </tr>
        <tr>
            <td style="height: 92px">
            </td>
        </tr>
    </table>
                    
</asp:Content>

