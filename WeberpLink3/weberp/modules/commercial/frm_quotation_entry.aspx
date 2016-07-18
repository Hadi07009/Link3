<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_quotation_entry.aspx.cs" Inherits="frm_quotation_entry" Title=""   ValidateRequest="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    
   <%@ Register src="usercontrols/ctl_quotation_view.ascx" tagname="ctl_quotation_view" tagprefix="uc1" %>     
   <%@ Register src="usercontrols/ctl_comments.ascx" tagname="ctl_comments" tagprefix="uc2" %> 
        
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table class="tblmas" style="width: 100%" id="tblmaster" runat="server">
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
                QUOTETION ENTRY SCREEN&nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 13px; text-align: right">
                <asp:Label ID="lblplant" runat="server" style="text-align: right" Text="Label" 
                    Width="300px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: left">
           
                 Recent Tender Info                
                <asp:DropDownList ID="ddlquotlog" runat="server" CssClass="txtbox"                 
                    Width="425px" AutoPostBack="True" OnSelectedIndexChanged="ddlquotlog_SelectedIndexChanged">
                </asp:DropDownList>
                
                
                </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 8px; text-align: left">
                       
                    </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                <asp:Button ID="btnQuotation0" runat="server" CssClass="btn2" Text="QUOTATION ENTRY"
                    Width="159px" OnClick="btnQuotation_Click" />
            
               
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: left">
            <asp:UpdatePanel ID="updpnl" runat="server">
                <ContentTemplate>
                    
                     <asp:Panel ID="PNL" runat="server" CssClass="tbl" Style="border-right: black 2px solid;
                    padding-right: 20px; border-top: black 2px solid; padding-left: 20px; display: none;
                    padding-bottom: 20px; border-left: black 2px solid; width: 500px; padding-top: 20px;
                    border-bottom: black 2px solid; background-color: white" Height="224px" Width="500px">
                               &nbsp;<br />
                               <table style="width:100%;">
                                   <tr>
                                       <td style="width: 66px; text-align: left;">
                                           MPR</td>
                                       <td style="width: 6px">
                                           :</td>
                                       <td style="text-align: left">
                                           <asp:Label ID="lblmpr" runat="server"></asp:Label>
                                       </td>
                                   </tr>
                                   <tr>
                                       <td style="width: 66px; text-align: left">
                                           Item</td>
                                       <td style="width: 6px">
                                           :</td>
                                       <td style="text-align: left">
                                           <asp:Label ID="lblitem" runat="server"></asp:Label>
                                       </td>
                                   </tr>
                                   <tr>
                                       <td style="width: 66px; text-align: left">
                                           &nbsp;</td>
                                       <td style="width: 6px">
                                           &nbsp;</td>
                                       <td style="text-align: left">
                                           &nbsp;</td>
                                   </tr>
                                   <tr>
                                       <td style="width: 66px">
                                           &nbsp;</td>
                                       <td style="width: 6px">
                                           &nbsp;</td>
                                       <td style="text-align: left">
                                           <asp:CheckBox ID="chkurgent" runat="server" Font-Bold="True" 
                                               ForeColor="#FF3300" Text="URGENT" />
                                       </td>
                                   </tr>
                                   <tr>
                                       <td style="width: 66px">
                                           &nbsp;</td>
                                       <td style="width: 6px">
                                           &nbsp;</td>
                                       <td>
                                           &nbsp;</td>
                                   </tr>
                                   <tr>
                                       <td style="width: 66px; text-align: left;">
                                           Comments</td>
                                       <td style="width: 6px">
                                           :</td>
                                       <td style="text-align: left">
                                           <asp:TextBox ID="txtcomments" runat="server" CssClass="txtbox" Width="387px"></asp:TextBox>
                                       </td>
                                   </tr>
                                   <tr>
                                       <td style="width: 66px">
                                           &nbsp;</td>
                                       <td style="width: 6px">
                                           &nbsp;</td>
                                       <td>
                                           &nbsp;</td>
                                   </tr>
                               </table>
                    <br />
                    <div style="text-align: right">
                        <asp:Button ID="ButtonOk" runat="server" CssClass="btn2" Text="OK" 
                            Width="80px" onclick="ButtonOk_Click" />
                        &nbsp;<asp:Button ID="ButtonCancel" runat="server" CssClass="btn2" Text="Cancel" Width="82px" />
                    </div>
                </asp:Panel>
                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                    CancelControlID="ButtonCancel" PopupControlID="PNL" TargetControlID="Button2">
                </ajaxToolkit:ModalPopupExtender>
                <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" DisplayModalPopupID="ModalPopupExtender1"
                    TargetControlID="Button2">
                </ajaxToolkit:ConfirmButtonExtender>    
                 <asp:Button ID="Button2" runat="server" Visible="false"  />
                    <asp:GridView ID="gdItem" runat="server" BackColor="White" 
                                BorderColor="#6B7EBF" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                                ForeColor="#333333" GridLines="None" PageSize="100" SkinID="GridView" 
                                OnRowCommand="gdItem_RowCommand"  OnSorting="gdItem_Sorting" OnRowDataBound = "gdItem_RowDataBound"
                                style="border-top-width: 1px; border-left-width: 1px; border-left-color: #e6e6fa; border-bottom-width: 1px; border-bottom-color: #e6e6fa; border-top-color: #e6e6fa; border-right-width: 1px; border-right-color: #e6e6fa;" 
                                Width="100%" AllowSorting="True">
                        <FooterStyle BackColor="#6B7EBF" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#6B7EBF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle Font-Bold="True" Wrap="False" />
                        <HeaderStyle BackColor="#6B7EBF" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="Lavender" Font-Size="8pt" 
                                    Font-Underline="False" />
                        <RowStyle Font-Size="8pt" Wrap="true" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sel">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qtn">
                                <ItemTemplate>
                                    <uc1:ctl_quotation_view ID="ctl1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Button ID="Button3" runat="server" Text="Proceed" OnClick="Button1_Click" CssClass="btn2" />                                    
                                </ItemTemplate>
                                
                            </asp:TemplateField>
                             
                        </Columns>
                        
                    </asp:GridView>
                    
                </ContentTemplate>
                <Triggers>
               <asp:PostBackTrigger ControlID="ButtonOk" />
                </Triggers>
            </asp:UpdatePanel>
            
               
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                          
                &nbsp;</td>
        </tr>
       
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                <asp:Button ID="btnQuotation" runat="server" CssClass="btn2" Text="QUOTATION ENTRY"
                    Width="159px" OnClick="btnQuotation_Click" /></td>
        </tr>
       
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                       
                    <table id="tbltooltip" runat="server" style="border: thin solid #000000; width: 836px; font-size: 1em;">
                        <tr>
                            <td bgcolor="#ccccff" style="width: 21px">SL</td>
                            <td bgcolor="#ccccff">Party</td>
                            <td bgcolor="#ccccff">Rate</td>
                            <td bgcolor="#ccccff">Specification</td>
                            <td bgcolor="#ccccff">Brand</td>
                            <td bgcolor="#ccccff">Origin</td>
                            <td bgcolor="#ccccff">Packing</td>
                        </tr>
                        <tr>
                            <td style="width: 21px"></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 21px"></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 21px"></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 21px"></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 21px"></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 21px"></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 21px"></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 21px"></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 21px"></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 21px"></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 21px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 21px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 21px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 21px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table id="tbltooltip2" runat="server" style="background-color: #FFFFFF; background-position: center center; width: 772px; font-size: 1em;">
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style6" colspan="2">TERMS AND CONDITIONS</td>
                    </tr>
                    <tr>
                        <td class="style5" valign="top">General Terms:</td>
                        <td bgcolor="AliceBlue">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style5" valign="top">Special Terms:</td>
                        <td bgcolor="AliceBlue">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style5" valign="top">Pay Terms:</td>
                        <td bgcolor="AliceBlue">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style5" valign="top">Valid Days:</td>
                        <td bgcolor="AliceBlue">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style5">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="height: 57px">
            </td>
            <td style="height: 57px">
            </td>
            <td style="height: 57px">
            </td>
        </tr>
    </table>
        
</asp:Content>

