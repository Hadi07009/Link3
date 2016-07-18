<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_spo_create_ini.aspx.cs" Inherits="frm_spo_create_ini" Title=""   ValidateRequest="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:UpdatePanel ID="updpannel" runat="server">
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
                PURCHASE ORDER INITIATE SCREEN&nbsp;(SPO)</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                <asp:Label ID="lblitem" runat="server" Font-Bold="True" ForeColor="#FF3300" 
                    Text="No Item Found." Visible="False" Width="200px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: left">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Plants:
                <asp:DropDownList ID="ddlplants" runat="server" AutoPostBack="True" 
                    CssClass="txtbox" onselectedindexchanged="ddlplants_SelectedIndexChanged" 
                    Width="200px">
                </asp:DropDownList>
            </td>
        </tr>
    <tr>
        <td class="tbl" colspan="3" style="height: 19px; text-align: left">
        
                &nbsp;</td>
    </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 19px; text-align: left">
                <asp:GridView ID="gdItem" runat="server" AutoGenerateColumns="False" 
                    BackColor="White" BorderColor="#41519A" BorderStyle="Solid" BorderWidth="1px" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" 
                    OnRowDataBound="gdItem_RowDataBound"
                    onselectedindexchanged="gdItem_SelectedIndexChanged" PageSize="100" 
                    SkinID="GridView" 
                    style="border-top-width: 1px; border-left-width: 1px; border-left-color: #e6e6fa; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='white', startcolorstr='#D6DCF9', gradienttype='0'); border-bottom-width: 1px; border-bottom-color: #e6e6fa; border-top-color: #e6e6fa; border-right-width: 1px; border-right-color: #e6e6fa;" 
                    Width="100%" AllowSorting="True">
                    <FooterStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#41519A" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle Font-Bold="True" Wrap="False" />
                    <HeaderStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="Lavender" Font-Size="8pt" 
                        Font-Underline="False" Wrap="False" />
                    <RowStyle Font-Size="8pt" Wrap="False" />
                    <Columns>
                        <asp:TemplateField HeaderText="Sel">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ref No">
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
                        <asp:TemplateField HeaderText="Qnty">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Balance">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO Qty">
                            <ItemTemplate>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
                                    runat="server" FilterType="Custom, Numbers" TargetControlID="TextBox1" 
                                    ValidChars=".">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="txtbox" Width="50px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rate">
                            <ItemTemplate>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
                                    runat="server" FilterType="Custom, Numbers" TargetControlID="TextBox2" 
                                    ValidChars=".">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="txtbox" Width="50px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Log">
                            <ItemTemplate>
                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="txtbox" 
                                    Font-Size="10px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Party">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" CssClass="txtbox" Width="300px" />
                                 <ajaxToolkit:AutoCompleteExtender
                                runat="server"                                
                                ID="autoComplete1"                                                           
                                TargetControlID="TextBox3"
                                ServicePath="services/autocomplete.asmx" 
                                ServiceMethod="GetPartyAdrList"
                                MinimumPrefixLength="1" 
                                CompletionInterval="100"
                                EnableCaching="false"
                                CompletionSetCount="20"                                 
                                CompletionListCssClass ="autocomplete_completionListElement"                            
                                CompletionListItemCssClass="autocomplete_listItem" 
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                DelimiterCharacters=","
                                ShowOnlyCurrentWordInCompletionListItem="true" > 
                            </ajaxToolkit:AutoCompleteExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Specifi">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" CssClass="txtbox" Width="50px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Brand">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBox5" runat="server" CssClass="txtbox" Width="50px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Origin">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBox6" runat="server" CssClass="txtbox" Width="50px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Packing">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBox7" runat="server" CssClass="txtbox" Width="50px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    <tr>
        <td class="tbl" colspan="3" style="height: 24px; text-align: center">
            &nbsp;</td>
    </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                <table id="tblsel" runat="server" style="width:100%;">
                    <tr>
                        <td colspan="5">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 56px">
                            &nbsp;</td>
                        <td style="text-align: left; width: 77px">
                            Employee</td>
                        <td style="width: 20px">
                            :</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtemployee" runat="server" CssClass="txtbox" Width="555px"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
                                BehaviorID="AutoCompleteEx" CompletionInterval="100" 
                                CompletionListCssClass="autocomplete_completionListElement" 
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                                CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" 
                                DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="1" 
                                ServiceMethod="GetEmployeeList" ServicePath="services/autocomplete.asmx" 
                                ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtemployee">
                            </ajaxToolkit:AutoCompleteExtender>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 56px">
                            &nbsp;</td>
                        <td style="text-align: left; width: 77px">
                            &nbsp;</td>
                        <td style="width: 20px">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: left">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 16px; text-align: center">
                &nbsp; &nbsp;&nbsp; 
            
                <asp:Button ID="btnproceed" runat="server" onclick="btnproceed_Click" 
                    Text="Proceed" CssClass="btn2" Width="109px" Visible="False" />
            
                &nbsp; &nbsp; &nbsp; &nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 25px; text-align: center">
            
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 22px; text-align: left">
                
                
            </td>
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
         
         function ShowHideField(DecisionControl, ToggleControl1, ToggleControl2, ToggleControl3, ToggleControl4, ToggleControl5, ToggleControl6, ToggleControl7, ToggleControl8) {

             if (DecisionControl.checked == true) {
                 ToggleControl1.style.visibility = 'visible';
                 ToggleControl2.style.visibility = 'visible';
                 ToggleControl3.style.visibility = 'visible';
                 ToggleControl4.style.visibility = 'visible';
                 ToggleControl5.style.visibility = 'visible';
                 ToggleControl6.style.visibility = 'visible';
                 ToggleControl7.style.visibility = 'visible';
                 ToggleControl8.style.visibility = 'visible';
             }
             else {
                 ToggleControl1.style.visibility = 'hidden';
                 ToggleControl2.style.visibility = 'hidden';
                 ToggleControl3.style.visibility = 'hidden';
                 ToggleControl4.style.visibility = 'hidden';
                 ToggleControl5.style.visibility = 'hidden';
                 ToggleControl6.style.visibility = 'hidden';
                 ToggleControl7.style.visibility = 'hidden';
                 ToggleControl8.style.visibility = 'hidden';
             }

         }

         
        
    </script>
</asp:Content>

