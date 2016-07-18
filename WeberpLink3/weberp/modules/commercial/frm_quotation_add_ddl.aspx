<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_quotation_add_ddl.aspx.cs" Inherits="frm_quotation_add_ddl" Title=""   EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>   
<%@ Register src="usercontrols/ctl_quotation_entry.ascx" tagname="ctl_quotation_entry" tagprefix="uc1" %>   
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>


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
                QUOTATION ENTRY SCREEN&nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                <asp:Label ID="lblmsg" runat="server" Font-Bold="True" ForeColor="Red" 
                    Text="Please Entry General and Payment Terms" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: left">
                PARTY:
                <asp:TextBox ID="txtparty" runat="server" CssClass="txtbox" ReadOnly="True" 
                    Width="500px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 22px; text-align: center">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 22px; text-align: left">
                <asp:PlaceHolder ID="celquo" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 22px; text-align: center">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 22px; text-align: center">
            <asp:Button ID="btnshow" runat="server" CssClass="btn2" onclick="btnshow_Click" 
                Text=" Show T&amp;C Detail" Width="131px" />
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 22px; text-align: left">
            <asp:UpdatePanel ID="upd" runat="server">
                        <ContentTemplate>
                <table id="tbltac" runat="server" style="width:100%;">
                    <tr>
                        <td colspan="2">
                            <b>GENERAL TERMS AND CONDITIONS:</b></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 25px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gdgen" runat="server" AutoGenerateColumns="False" 
                                OnRowDataBound="gdgen_RowDataBound" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Terms and condition">
                                        <ItemTemplate>
                                            <FTB:FreeTextBox id="TextBox1"			
				runat="Server" Text="" Width="100%" EnableToolbars="False" BreakMode="LineBreak" 
                        DownLevelCols="50" EnableHtmlMode="false"
                        EditorBorderColorDark="AliceBlue" EditorBorderColorLight="AliceBlue" 
                        FormatHtmlTagsToXhtml="True" Height="70px" 
                        AllowHtmlMode="False" 
                        DesignModeBodyTagCssClass="" DesignModeCss="" BackColor="AliceBlue" 
                        GutterBackColor="AliceBlue" GutterBorderColorDark="AliceBlue" 
                        GutterBorderColorLight="AliceBlue" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 22px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <b>SPECIAL TERMS AND CONDITIONS:</b></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 23px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gdspe" runat="server" AutoGenerateColumns="False" 
                                OnRowDataBound="gdspe_RowDataBound" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox2" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Terms and condition">
                                        <ItemTemplate>
                                            <FTB:FreeTextBox id="TextBox2"			
				runat="Server" Text="" Width="100%" EnableToolbars="False" BreakMode="LineBreak" 
                        DownLevelCols="50" EnableHtmlMode="false"
                        EditorBorderColorDark="AliceBlue" EditorBorderColorLight="AliceBlue" 
                        FormatHtmlTagsToXhtml="True" Height="70px" 
                        AllowHtmlMode="False" 
                        DesignModeBodyTagCssClass="" DesignModeCss="" BackColor="AliceBlue" 
                        GutterBackColor="AliceBlue" GutterBorderColorDark="AliceBlue" 
                        GutterBorderColorLight="AliceBlue" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 25px">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>PAYMENT TERMS:</b>&nbsp;
                        </td>
                        <td>                        
                            <asp:DropDownList ID="ddlpayterms" runat="server" AutoPostBack="True" 
                            OnSelectedIndexChanged ="ddlpayterms_SelectedIndexChanged" Width="200px">  
                            <asp:ListItem Value="no">No Advance</asp:ListItem>                                                              
                                <asp:ListItem Value="part">Part Advance</asp:ListItem>
                                <asp:ListItem Value="full">Full Advance</asp:ListItem>
                                
                            </asp:DropDownList>                            
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 24px">
                            &nbsp;</td>
                        <td style="height: 24px">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        
                            <asp:GridView ID="gdpay" runat="server" AutoGenerateColumns="False" 
                                OnRowDataBound="gdpay_RowDataBound" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox3" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Terms and condition">
                                        <ItemTemplate>
                                            <FTB:FreeTextBox id="TextBox3"			
				runat="Server" Text="" Width="100%" EnableToolbars="False" BreakMode="LineBreak" 
                        DownLevelCols="50" EnableHtmlMode="false"
                        EditorBorderColorDark="AliceBlue" EditorBorderColorLight="AliceBlue" 
                        FormatHtmlTagsToXhtml="True" Height="70px" 
                        AllowHtmlMode="False" 
                        DesignModeBodyTagCssClass="" DesignModeCss="" BackColor="AliceBlue" 
                        GutterBackColor="AliceBlue" GutterBorderColorDark="AliceBlue" 
                        GutterBorderColorLight="AliceBlue" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                           
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 36px">
                            <b><span style="font-size: medium">Above quotetion valid upto </span>
                            <asp:TextBox ID="txtvaliddays" runat="server" style="font-size: medium" 
                                Width="50px"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="txtqty_FilteredTextBoxExtender" 
                                runat="server" FilterType="Numbers" TargetControlID="txtvaliddays">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <span style="font-size: medium">&nbsp;days after purchase order.</span></b></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 36px">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="btnupdate" runat="server" CssClass="btn2" 
                                onclick="btnupdate_Click" Text="Update" Width="109px" />
                        </td>
                    </tr>
                </table>
                 </ContentTemplate>
            <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlpayterms" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnshow" EventName="Click" />
            <asp:PostBackTrigger ControlID="btnupdate" />
            </Triggers>
            </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 22px; text-align: center">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="text-align: left">
                            &nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="text-align: center">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 85px; text-align: center">
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
       <script type="text/javascript">


         function ColorRow(CheckBoxObj) {
             if (CheckBoxObj.checked == true) {
                 CheckBoxObj.parentElement.parentElement.style.backgroundColor = '#88AAFF';
             }
             else {
                 CheckBoxObj.parentElement.parentElement.style.backgroundColor = '#F8E5A1';
             }

         }
              
        
        
    </script>
</asp:Content>

