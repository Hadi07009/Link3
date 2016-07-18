<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_quotation_add.aspx.cs" Inherits="frm_quotation_add" Title=""   EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>   
<%@ Register src="usercontrols/ctl_quotation_entry.ascx" tagname="ctl_quotation_entry" tagprefix="uc1" %>   
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script src="http://code.jquery.com/jquery-1.8.2.js" type="text/javascript"></script>
<script src="../../script/jquery.MultiFile.js" type="text/javascript"></script>

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
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: left">
                <table style="width:100%;">
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Supplier"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>                
                <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="txtbox"                 
                    Width="455px" AutoPostBack="True">
                </asp:DropDownList>
                
                
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>&nbsp;</td>
                        <td><asp:TextBox ID="txtparty" runat="server" CssClass="txtbox" autocomplete="off" 
                    Width="450px" Visible="False"></asp:TextBox>
                <ajaxToolkit:AutoCompleteExtender
                                runat="server"                                
                                ID="autoComplete1" 
                                BehaviorID="AutoCompleteEx"                                
                                TargetControlID="txtparty"
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
               
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 22px; text-align: center">
                
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 22px; text-align: left">
                <asp:PlaceHolder ID="celquo" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 22px; text-align: center">
                &nbsp;</td></tr><tr>
            <td class="tbl" colspan="3" style="height: 22px; text-align: left">
                <table style="width:100%;">
                    <tr>
                        <td colspan="2">
                            <b>GENERAL TERMS AND CONDITIONS:</b></td></tr><tr>
                        <td colspan="2" style="height: 25px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        
                            <asp:GridView ID="gdgen" runat="server" OnRowDataBound="gdgen_RowDataBound" Width="100%" 
                                AutoGenerateColumns="False" >
                            <Columns>
                                <asp:TemplateField>
                                    <AlternatingItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </AlternatingItemTemplate>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Text="All" />
                                    </HeaderTemplate>
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
                            <b>SPECIAL TERMS AND CONDITIONS:</b></td></tr><tr>
                        <td colspan="2" style="height: 23px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        
                            <asp:GridView ID="gdspe" runat="server" Width="100%" 
                                AutoGenerateColumns="False" OnRowDataBound="gdspe_RowDataBound" >
                            <Columns>
                                <asp:TemplateField>
                                    <AlternatingItemTemplate>
                                        <asp:CheckBox ID="CheckBox2" runat="server" />
                                    </AlternatingItemTemplate>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="CheckBox2" runat="server" Text="All" />
                                    </HeaderTemplate>
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
                                <asp:ListItem Value="LC">LC Payment</asp:ListItem>
                                </asp:DropDownList></td></tr><tr>
                        <td style="height: 24px">
                            &nbsp;</td><td style="height: 24px">
                            &nbsp;</td></tr><tr>
                        <td colspan="2">
                        <asp:UpdatePanel ID="upd" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gdpay" runat="server" AutoGenerateColumns="False" 
                                    OnRowDataBound="gdpay_RowDataBound" Width="100%">
                                    <Columns>
                                        <asp:TemplateField>
                                            <AlternatingItemTemplate>
                                                <asp:CheckBox ID="CheckBox3" runat="server" />
                                            </AlternatingItemTemplate>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="CheckBox3" runat="server" Text="All" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox3" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Terms and condition">
                                            <ItemTemplate>
                                                <FTB:FreeTextBox ID="TextBox3" runat="Server" AllowHtmlMode="False" 
                                                    BackColor="AliceBlue" BreakMode="LineBreak" DesignModeBodyTagCssClass="" 
                                                    DesignModeCss="" DownLevelCols="50" EditorBorderColorDark="AliceBlue" 
                                                    EditorBorderColorLight="AliceBlue" EnableHtmlMode="false" 
                                                    EnableToolbars="False" FormatHtmlTagsToXhtml="True" GutterBackColor="AliceBlue" 
                                                    GutterBorderColorDark="AliceBlue" GutterBorderColorLight="AliceBlue" 
                                                    Height="70px" Text="" Width="100%" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlpayterms" EventName="SelectedIndexChanged" />
                            </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;</td></tr>
                    <tr>
                        <td colspan="2" style="font-size: medium">
                            <b>Above quotetion valid upto
                            <asp:TextBox ID="txtvaliddays" runat="server" Width="50px"></asp:TextBox>
&nbsp;days after purchase order.</b></td>
<ajaxToolkit:FilteredTextBoxExtender ID="txtqty_FilteredTextBoxExtender" 
                                runat="server" FilterType="Numbers" TargetControlID="txtvaliddays">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            </tr></table></td></tr>
        <tr>
            <td class="tbl" colspan="3" style="text-align: left">
                <asp:Label ID="Label1" runat="server" Text="ATTACH FILE HERE"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:FileUpload ID="file_upload" runat="server" class="multi" />
            </td>
            <td>
                &nbsp;</td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Button ID="btnsave" runat="server" CssClass="btn2" Text="SAVE" 
                    Width="150px" onclick="btnsave_Click" />
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: left">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:GridView ID="gdvFileLoad" runat="server" AutoGenerateColumns="false" BorderColor="LightBlue" BorderWidth="0px" onrowdatabound="gdvFileLoad_RowDataBound" onselectedindexchanged="gdvFileLoad_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="ReferenceNo" HeaderText="Reference" ItemStyle-Width="150px">
                        <ItemStyle Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FileName" HeaderText="List of Attachment" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="450px"></ItemStyle>
                                        </asp:BoundField>
                        <asp:BoundField DataField="SavedFileName" HeaderText="SavedFileName" ItemStyle-Width="150px">
                        <ItemStyle Width="150px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Button ID="btndownload" runat="server" Enabled="true" Text="Download" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <script type="text/javascript">



         function SelectAll(id, indx) {
             //get reference of GridView control
             if (indx == '1')

                 var grid = document.getElementById("<%= gdgen.ClientID %>");
             else if (indx == '2')
                 var grid = document.getElementById("<%= gdspe.ClientID %>");
             else
                 var grid = document.getElementById("<%= gdpay.ClientID %>");

             // var grid = document.getElementById(itm);
             //variable to contain the cell of the grid
             var cell;

             if (grid.rows.length > 0) {
                 //loop starts from 1. rows[0] points to the header.
                 for (i = 1; i < grid.rows.length; i++) {
                     //get the reference of first column
                     cell = grid.rows[i].cells[0];

                     //loop according to the number of childNodes in the cell
                     for (j = 0; j < cell.childNodes.length; j++) {
                         //if childNode type is CheckBox


                         if (cell.childNodes[j].type == "checkbox") {

                             //assign the status of the Select All checkbox to the cell checkbox within the grid
                             cell.childNodes[j].checked = document.getElementById(id).checked;
                             ColorRow(cell.childNodes[j]);
                         }
                     }
                 }
             }
         }



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

