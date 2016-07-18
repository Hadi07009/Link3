<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_mat_rec_confirm.aspx.cs" Inherits="frm_mat_rec_confirm" Title=""   EnableEventValidation="false" %>
<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
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
                MATERIAL RECEIVE FROM INSPECTION (MRR)</td>
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
            <td class="tbl" colspan="3" style="height: 34px; text-align: left">
                
                
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
                </table>
                
                
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 22px; text-align: left">
                
                        MRR DATE:
                        <ew:CalendarPopup ID="cldmrrdate" runat="server" 
                            Culture="English (United Kingdom)" DisableTextBoxEntry="False" Width="87px">
                            <ButtonStyle CssClass="btn2" />
                        </ew:CalendarPopup>
                                    
                        </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 22px; text-align: left">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 22px; text-align: left">
                <asp:GridView ID="gdItem" runat="server" AutoGenerateColumns="False" 
                    BackColor="White" BorderColor="#41519A" BorderStyle="Solid" BorderWidth="1px" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" 
                    OnRowDataBound="gdItem_RowDataBound" PageSize="100" SkinID="GridView" 
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
                        <asp:TemplateField HeaderText="PO Qnty">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rec Qnty">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ins Qnty">
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Receive Qty">
                            <ItemTemplate>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" 
                                    runat="server" FilterType="Custom, Numbers" TargetControlID="TextBox1" 
                                    ValidChars=".">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="txtbox" Width="50px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Brand">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="txtbox" Width="80px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Origin">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" CssClass="txtbox" Width="80px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Packing">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" CssClass="txtbox" Width="80px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="lno" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="Label8" Visible="false" runat="server" />
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
            <td class="tbl" colspan="3" style="height: 8px; text-align: left">
                &nbsp;
                <asp:Label ID="lblmode" runat="server" Text="MODE OF DELIVERY:" Visible="False"></asp:Label>
                &nbsp;
                <asp:TextBox ID="txtmodeofdel" runat="server" CssClass="txtbox" Width="164px" 
                    Visible="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 8px; text-align: center">
                <asp:Button ID="btnProceed" runat="server" CssClass="btn2" Text="Proceed" 
                    Width="117px" onclick="btnProceed_Click" Visible="False" />
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
                
<script type="text/javascript">


         function ColorRow(CheckBoxObj) {
             if (CheckBoxObj.checked == true) {
                 CheckBoxObj.parentElement.parentElement.style.backgroundColor = '#88AAFF';
             }
             else {
                 CheckBoxObj.parentElement.parentElement.style.backgroundColor = '#F8E5A1';
             }

         }
         
         function ShowHideField(DecisionControl, ToggleControl1, ToggleControl2, ToggleControl3, ToggleControl4) {

             if (DecisionControl.checked == true) {
                 ToggleControl1.style.visibility = 'visible';
                 ToggleControl2.style.visibility = 'visible';
                 ToggleControl3.style.visibility = 'visible';
                 ToggleControl4.style.visibility = 'visible';               
             }
             else 
             {
                 ToggleControl1.style.visibility = 'hidden';
                 ToggleControl2.style.visibility = 'hidden';
                 ToggleControl3.style.visibility = 'hidden';
                 ToggleControl4.style.visibility = 'hidden';    
                               
             }

         }

         
        
    </script>

</asp:Content>

