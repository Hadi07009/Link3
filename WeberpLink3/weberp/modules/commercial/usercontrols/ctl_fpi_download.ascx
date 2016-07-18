<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_fpi_download.ascx.cs" Inherits="modules_commercial_usercontrols_ctl_fpi_download" %>
<table style="width:100%;">
    <tr>
        <td class="heading" colspan="3" style="text-align: center">
            FILE VIEW/DOWNLOAD</td>
    </tr>
    <tr>
        <td style="width: 130px">
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 130px">
            &nbsp;</td>
        <td style="text-align: center">
                <asp:GridView ID="gdItem" runat="server" BackColor="White" 
                BorderColor="#41519A" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #e6e6fa;  border-bottom-width: 1px; border-bottom-color: #e6e6fa; border-top-color: #e6e6fa; border-right-width: 1px; border-right-color: #e6e6fa;"
                    BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                ForeColor="#333333" GridLines="None"
                    
                    PageSize="100" SkinID="GridView" Width="80%" 
                     onrowdatabound="gdItem_RowDataBound"                                    
                              >
                    <FooterStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#41519A" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle Font-Bold="True" />
                    <HeaderStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button ID="btndownload" Font-Size="7pt" Text="View/Download" CssClass="btn2" runat="server"></asp:Button>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="Lavender" Font-Size="7pt" 
                        Font-Underline="False" />
                    <RowStyle Font-Size="7pt" />

                   

                </asp:GridView>                   
                            </td>
        <td>
            &nbsp;</td>
    </tr>
</table>

