<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_tender_inquiry.aspx.cs" Inherits="frm_tender_inquiry" Title=""   EnableEventValidation="false" ValidateRequest ="false"   %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>   

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
                TENDER INQUIRY SCREEN&nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: right">
                <asp:Label ID="lblplant" runat="server" Text="Label" Width="300px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="text-align: center">
                <asp:Label ID="lblnodata" runat="server" ForeColor="Red" Text="No items for tender inquiry."
                    Visible="False" Width="373px"></asp:Label></td>
        </tr>
    <tr>
        <td class="tbl" colspan="3" style="text-align: right">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="tbl" colspan="3" style="text-align: left">
            Date:
            <asp:TextBox ID="txtdate" runat="server" CssClass="txtbox" Width="105px"></asp:TextBox></td>
    </tr>
    <tr>
        <td class="tbl" colspan="3" style="text-align: left; height: 16px;">
        </td>
    </tr>
    <tr>
        <td class="tbl" colspan="3" style="vertical-align: top; text-align: left">
            To:&nbsp; &nbsp;<asp:TextBox ID="txtpartydet" runat="server" 
                CssClass="txtbox" autocomplete="off" 
                    Width="450px"></asp:TextBox>
                <ajaxToolkit:AutoCompleteExtender
                                runat="server"                                
                                ID="autoComplete1" 
                                BehaviorID="AutoCompleteEx"                                
                                TargetControlID="txtpartydet"
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
    <tr>
        <td class="tbl" colspan="3" style="vertical-align: middle; text-align: left; height: 17px;">
        </td>
    </tr>
    <tr>
        <td class="tbl" colspan="3" style="vertical-align: middle; text-align: left">
            Sub:&nbsp;
            <asp:TextBox ID="txtsub" runat="server" CssClass="txtbox" Width="515px"></asp:TextBox></td>
    </tr>
    <tr>
        <td class="tbl" colspan="3" style="vertical-align: middle; height: 17px; text-align: left">
        </td>
    </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 19px; text-align: left">
                &nbsp;<table id="tblhtml" runat="server" style="width: 99%" border="1" bordercolor="#41519A" cellspacing="0">
                    <tr>
                        <td style="width: 44px">
                            Select</td>
                        <td style="width: 27px">
                            Sl</td>
                        <td style="width: 27px">
                            Ref</td>
                        <td style="width: 27px">
                            Code</td>
                        <td style="width: 309px">
                            Product</td>
                        <td style="width: 77px">
                            Qty</td>
                        <td style="width: 77px">
                            Brand</td>
                        <td style="width: 77px">
                            Origin</td>
                        <td style="width: 77px">
                            Specification</td>
                        <td>
                            Remarks (If any)</td>
                    </tr>
                </table>
            </td>
        </tr>
    <tr>
        <td class="tbl" colspan="3" style="height: 19px; text-align: left">
            &nbsp;</td>
    </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 19px; text-align: left">
               
                <table style="width:100%;">
                    <tr>
                        <td colspan="2">
                            <b>GENERAL TERMS AND CONDITIONS:</b></td>
                    </tr>
                    <tr>
                        <td style="height: 25px" colspan="2">
                        
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
                        <td colspan="2">                                            
                        
                            &nbsp;</td>
                    </tr>
                    <tr>
                        
                        <td style="height: 22px" colspan="2">
                            </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <b>SPECIAL TERMS AND CONDITIONS:</b></td>
                    </tr>
                    <tr>
                        <td style="height: 23px" colspan="2">
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
                        <td style="height: 25px" colspan="2">
                            </td>
                    </tr>
                    <tr>
                        <td>
                            <b>PAYMENT TERMS:</b>
                          
                            </td>
                        <td>
                          
                            <asp:DropDownList ID="ddlpayterms" runat="server"  AutoPostBack="True" 
                                OnSelectedIndexChanged="ddlpayterms_SelectedIndexChanged" Width="200px">
                                <asp:ListItem Value="no">No Advance</asp:ListItem>                                
                                <asp:ListItem Value="part">Part Advance</asp:ListItem>
                                <asp:ListItem Value="full">Full Advance</asp:ListItem>
                                <asp:ListItem Value="LC">LC Payment</asp:ListItem>
                            </asp:DropDownList>
                            </td>
                    </tr>
                    <tr>
                        <td style="height: 24px">
                            </td>
                        <td style="height: 24px">
                            </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        <asp:UpdatePanel ID="upd" runat="server" >
                        <ContentTemplate>
                            <asp:GridView ID="gdpay" runat="server" Width="100%" 
                                AutoGenerateColumns="False" OnRowDataBound="gdpay_RowDataBound">
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
                            </ContentTemplate>
                            <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlpayterms" EventName="SelectedIndexChanged" />
                            </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;</td>
                    </tr>
                </table>
               
            </td>
        </tr>
    <tr>
        <td class="tbl" colspan="3" style="height: 19px; text-align: left">
        </td>
    </tr>
    <tr>
        <td class="tbl" colspan="3" style="height: 19px; text-align: left">
            Thanking You.<br />
            <asp:TextBox ID="txtfrom" runat="server" CssClass="txtbox" Height="65px" TextMode="MultiLine"
                Width="237px"></asp:TextBox></td>
    </tr>
    <tr>
        <td class="tbl" colspan="3" style="height: 24px; text-align: left">
        </td>
    </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 16px; text-align: center">
                &nbsp; &nbsp;&nbsp; 
            
                <asp:Button ID="btnproceed" runat="server" onclick="btnproceed_Click" 
                    Text="Proceed" CssClass="btn2" Width="109px" />
            
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

