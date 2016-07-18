<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_po_status.aspx.cs" Inherits="frm_po_status" Title=""   EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


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
                PURCHASE ORDER LIST</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 25px; text-align: center">
                PO STATUS:
                <asp:DropDownList ID="ddlpolist" runat="server" Width="200px" 
                    AutoPostBack="True" onselectedindexchanged="ddlpolist_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 34px; text-align: left">
                
                
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 22px; text-align: left">
                <asp:UpdatePanel ID="updgrid" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gdItem" runat="server" 
                            BackColor="White" BorderColor="#41519A" BorderStyle="Solid" BorderWidth="1px" 
                            CellPadding="4" ForeColor="#333333" GridLines="None" PageSize="100" 
                            SkinID="GridView" 
                           
                            style="border-color: #e6e6fa; border-width: 1px; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='white', startcolorstr='#D6DCF9', gradienttype='0'); text-align: left;" 
                            Width="98%">
                            <FooterStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#41519A" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle Font-Bold="True" />
                            <HeaderStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="Lavender" Font-Size="8pt" 
                                Font-Underline="False" />
                            <RowStyle Font-Size="8pt" />
                           
                        </asp:GridView>
                    </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlpolist" EventName="SelectedIndexChanged" />
                </Triggers>
                </asp:UpdatePanel>
                
                        </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 8px; text-align: center">
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="text-align: center">
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

