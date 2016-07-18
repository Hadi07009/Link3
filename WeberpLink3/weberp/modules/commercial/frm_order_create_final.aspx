<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_order_create_final.aspx.cs" Inherits="frm_order_create_final" Title=""   ValidateRequest="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
 
<%@ Register src="usercontrols/ctl_order_create_final.ascx" tagname="ctl_order_create_final" tagprefix="uc2" %>   
   
    
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
                PURCHASE ORDER CREATE SCREEN&nbsp;(LPO)</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 17px; text-align: left">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: left">
                <table style="width:100%;">
                    <tr>
                        <td style="width: 46px">
                            &nbsp;</td>
                        <td style="width: 65px">
                            PARTY</td>
                        <td style="width: 12px">
                            :</td>
                        <td>
                            <asp:TextBox ID="txtparty" runat="server" CssClass="txtbox" ReadOnly="True" 
                                Width="473px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 46px">
                            &nbsp;</td>
                        <td style="width: 65px">
                            REQ</td>
                        <td style="width: 12px">
                            :</td>
                        <td>
                            <asp:TextBox ID="txtreqtype" runat="server" CssClass="txtbox" ReadOnly="True" 
                                Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 46px">
                            &nbsp;</td>
                        <td style="width: 65px">
                            TYPE</td>
                        <td style="width: 12px">
                            :</td>
                        <td>
                            <asp:TextBox ID="txtpurchasetype" runat="server" CssClass="txtbox" 
                                ReadOnly="True" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 46px">
                            &nbsp;</td>
                        <td style="width: 65px">
                            AMOUNT</td>
                        <td style="width: 12px">
                            :</td>
                        <td>
                            <asp:TextBox ID="txtamount" runat="server" CssClass="txtbox" ReadOnly="True" 
                                Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 46px">
                            &nbsp;</td>
                        <td style="width: 65px">
                            IN Word</td>
                        <td style="width: 12px">
                            :</td>
                        <td>
                            <asp:Label ID="lblinword" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 15px; text-align: left">
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 19px; text-align: left">
                
                <asp:GridView ID="gdItem" runat="server" BackColor="White" 
                BorderColor="#41519A" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #e6e6fa; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='white', startcolorstr='#D6DCF9', gradienttype='0'); border-bottom-width: 1px; border-bottom-color: #e6e6fa; border-top-color: #e6e6fa; border-right-width: 1px; border-right-color: #e6e6fa;"
                    BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                ForeColor="#333333" GridLines="None"
                    
                    PageSize="100" SkinID="GridView" Width="98%">
                    <FooterStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#41519A" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle Font-Bold="True" />
                    <HeaderStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="Lavender" Font-Size="8pt" 
                        Font-Underline="False" />
                    <RowStyle Font-Size="8pt" />
                
                </asp:GridView>                   
                                         
                   
                    </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="text-align: center; font-weight: bold;">
                TERMS &amp; CONDITION</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="text-align: left; font-weight: bold;">
                GENERAL TERMS</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="text-align: left">
                
                    <asp:PlaceHolder ID="celgen" runat="server"></asp:PlaceHolder>
                                         
                   
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="text-align: left">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="text-align: left; font-weight: bold;">
                SPECIAL TERMS</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="text-align: left">
                
                    <asp:PlaceHolder ID="celspe" runat="server"></asp:PlaceHolder>
                                         
                   
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="text-align: left">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="text-align: left; font-weight: bold;">
                PAYMENT TERMS</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="text-align: left">
                
                    <asp:PlaceHolder ID="celpay" runat="server"></asp:PlaceHolder>
                                         
                   
            </td>
        </tr>
        
        <tr>
            <td class="tbl" colspan="3" style="text-align: left; font-weight: bold;">
                Valid Days</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="text-align: left">
                
                    <asp:RadioButtonList ID="rdovaliddays" runat="server" Width="200px">
                    </asp:RadioButtonList>
                                         
                   
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
               <asp:Panel ID="Panel4" runat="server" BorderStyle="Solid" BorderWidth="2px" CssClass="tbl"
                    DefaultButton="btncancel" Height="200px" ScrollBars="Auto"  Style="border-right: black 2px solid;display: none;
                    padding-right: 20px; border-top: black 2px solid; padding-left: 20px; 
                    padding-bottom: 20px;  border-left: black 2px solid; padding-top: 20px; border-bottom: black 2px solid;
                    background-color: white" Width="329px">
                    <div style="border-color: #e6e6fa; border-width: 1px; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='white', startcolorstr='#D6DCF9', gradienttype='0');
                        width: 94%; height: 177px; text-align: center; ">
                        &nbsp;&nbsp;<table id="tblmsg" runat="server" style="width: 286px">
                        <tr>
                            <td colspan="1" style="width: 364px; height: 18px; text-align: center">
                                <span style="color: #ff0000"><strong>PO CREATED SUCCESSFULLY</strong></span></td>
                        </tr>
                        <tr>
                            <td style="width: 364px; height: 13px">
                            </td>
                        </tr>
                            <tr>
                                <td style="width: 364px; text-align: center">
                                PO REF NO:</td>
                            </tr>
                        <tr>
                            <td style="width: 364px; text-align: center">
                                &nbsp;
                                
                                <asp:Label ID="lblporef" runat="server" Font-Bold="True" Width="162px"></asp:Label>
                                                               
                                </td>
                        </tr>
                        <tr>
                            <td colspan="1" style="height: 19px; text-align: center; width: 364px;">
                            </td>
                        </tr>
                            <tr>
                                <td colspan="1" style="width: 364px; height: 29px; text-align: center">
                                    <asp:Button ID="btncancel" runat="server" CssClass="btn2" Width="0px" Height="0px" />
                                    <asp:Button ID="btnok" runat="server" CssClass="btn2" OnClick="btnok_Click" Text="OK"
                                        Width="102px" />
                                        <asp:Button ID="btnprint" runat="server" CssClass="btn2" 
                                        OnClick="btnprint_Click" Text="PRINT" Width="102px" />
                                        </td>
                            </tr>
                    </table>
                    </div>
                </asp:Panel>
                <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender5" runat="server" DisplayModalPopupID="ModalPopupExtender5"
                    TargetControlID="Button1">
                </ajaxToolkit:ConfirmButtonExtender>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender5" runat="server" BackgroundCssClass="modalBackground"
                    CancelControlID="btncancel" PopupControlID="Panel4" TargetControlID="Button1">
                </ajaxToolkit:ModalPopupExtender> 
                <asp:Button ID="Button1" runat="server" Text="Button" Visible="False" />
               
                
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
            
                <asp:Button ID="btncreate" runat="server" CssClass="btn2" OnClick="btncreate_Click"
                    Text="Create" Width="133px" />
                    
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

