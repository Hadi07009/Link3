<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_mat_qc.aspx.cs" Inherits="frm_mat_qc" Title=""   EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="updgrid" runat="server">
<ContentTemplate>
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
                MATERIAL QUALITY CHECK SCREEN</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: right">
                <asp:Label ID="lblplant" runat="server" Text="Label" Width="300px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 25px; text-align: center">
                PO LIST:
                <asp:DropDownList ID="ddlpolist" runat="server" Width="500px" 
                    AutoPostBack="True" 
                    onselectedindexchanged="ddlpolist_SelectedIndexChanged">
                </asp:DropDownList>
                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender2" runat="server"
                TargetControlID="ddlpolist" PromptCssClass="ListSearchExtenderPrompt"
                QueryPattern="Contains" QueryTimeout="2000">
            </ajaxToolkit:ListSearchExtender>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 34px; text-align: center">
                
                
                <table ID="tblspo" runat="server" style="width:100%;">
                    <tr>
                        <td style="width: 69px">
                            &nbsp;</td>
                        <td style="text-align: left; width: 139px">
                            Purchased By</td>
                        <td style="text-align: left; width: 19px">
                            :</td>
                        <td style="text-align: left">
                            <asp:Label ID="lblby" runat="server" Font-Bold="True" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 69px">
                            &nbsp;</td>
                        <td style="text-align: left; width: 139px">
                            Purchased From</td>
                        <td style="text-align: left; width: 19px">
                            :</td>
                        <td style="text-align: left">
                            <asp:Label ID="lblfrom" runat="server" Font-Bold="True" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 69px">
                            &nbsp;</td>
                        <td style="width: 139px">
                            &nbsp;</td>
                        <td style="width: 19px">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                
                
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 22px; text-align: left">
               
                        <asp:GridView ID="gdItem" runat="server" AutoGenerateColumns="False" 
                            BackColor="White" BorderColor="#41519A" BorderStyle="Solid" BorderWidth="1px" 
                            CellPadding="4" ForeColor="#333333" GridLines="None" PageSize="100" 
                            SkinID="GridView" 
                           OnRowDataBound ="gdItem_RowDataBound"   
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
                            <Columns>
                             <asp:TemplateField HeaderText="Sel">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                            </asp:TemplateField>
                            
                                <asp:TemplateField HeaderText="Ref">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Icode">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Idet">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Uom">
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Brand">
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Origin">
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Packing">
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ins Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="Label8" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="OK Qty">
                                    <ItemTemplate>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" 
                                            runat="server" FilterType="Custom, Numbers" TargetControlID="TextBox1" 
                                            ValidChars=".">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="txtbox" Width="50px" />
                                    </ItemTemplate>
                                </asp:TemplateField>     
                                 <asp:TemplateField HeaderText="Reject Qty">
                                    <ItemTemplate>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" 
                                            runat="server" FilterType="Custom, Numbers" TargetControlID="TextBox2" 
                                            ValidChars=".">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="txtbox" Width="50px" />
                                    </ItemTemplate>
                                </asp:TemplateField>  
                        <asp:TemplateField HeaderText="Serial Number">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" CssClass="txtbox" Width="250px" TextMode="MultiLine"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="lno" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="Label9" Visible="false" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>         
                            </Columns>
                        </asp:GridView>
                    
                
                        </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 8px; text-align: center">
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 8px; text-align: center">
                <asp:Button ID="btnProceed" runat="server" CssClass="btn2" 
                    onclick="btnProceed_Click" Text="Proceed" Visible="False" Width="117px" />
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 8px; text-align: center">
                &nbsp;</td>
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

</ContentTemplate>                
</asp:UpdatePanel>
 

</asp:Content>

