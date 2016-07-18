<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_mpr_routing.aspx.cs" Inherits="frm_mpr_routing" Title=""   ValidateRequest="false"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    
        
       
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
                PURCHASE REQUISITION FORWARD SCREEN&nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 8px; text-align: right">
                <asp:Label ID="lblplants" runat="server" Font-Bold="True" Text="Label" 
                    Width="300px" style="text-align: right"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 2px; text-align: left">
                <asp:Label ID="lblmsg" runat="server" Font-Bold="True" ForeColor="Red" 
                    Text="Label" Visible="False"></asp:Label>
            <asp:Button ID="btnexport" runat="server" CssClass="btn2" 
                onclick="btnexport_Click" Text="Export To Excel" 
                Width="157px" />
            </td>
        </tr>
        <tr>
        <td class="tbl" colspan="3" style="height: 24px; text-align:left">
            
       
           <asp:Label ID="lblcount" runat="server" Text="Total Pending Item: " 
                Width="200px"></asp:Label>
           <asp:UpdateProgress ID="sp1" runat="server">
                        <ProgressTemplate>
                            <%--<div class="TransparentGrayBackground"></div>--%>
                            <asp:Panel  ID="alwaysVisibleAP" runat="server" style="text-align: center" >
                            
                                <div style="text-align: center">
                                    <asp:Image  ID="ajaxLoadNotificationImage" 
                                                runat="server" 
                                                ImageUrl="~/images/Loading.gif" 
                                                AlternateText="[image]" />
                                    
                                </div>
                            
                            </asp:Panel>
                            <ajaxToolKit:AlwaysVisibleControlExtender 
                                ID="AlwaysVisibleControlExtender" 
                                runat="server"
                                TargetControlID="alwaysVisibleAP"
                                HorizontalSide="Center"
                                HorizontalOffset="0"
                                VerticalSide="Middle"
                                VerticalOffset="0">
                            </ajaxToolKit:AlwaysVisibleControlExtender>
                           
                        </ProgressTemplate>
                    </asp:UpdateProgress>
            
        
        </td>
        </tr>
        <tr>
        <td class="tbl" colspan="3" style="height: 24px; text-align:left">
            <asp:UpdatePanel ID="updpnl" runat="server">
            <ContentTemplate>
            
                    <asp:GridView ID="gdItem" runat="server" BackColor="White" 
                                BorderColor="#41519A" BorderStyle="Solid" 
                BorderWidth="1px" CellPadding="4" 
                                ForeColor="#333333" GridLines="None" PageSize="100" SkinID="GridView" 
                                OnRowCommand="gdItem_RowCommand"  
                OnSorting="gdItem_Sorting" OnRowDataBound = "gdItem_RowDataBound"
                                style="border-top-width: 1px; border-left-width: 1px; border-left-color: #e6e6fa;border-bottom-width: 1px; border-bottom-color: #e6e6fa; border-top-color: #e6e6fa; border-right-width: 1px; border-right-color: #e6e6fa;" 
                                Width="100%" AllowSorting="True">
                        <FooterStyle BackColor="#41519A" Font-Bold="True" ForeColor="White"  CssClass="wrp" />
                        <PagerStyle BackColor="#41519A" ForeColor="White" HorizontalAlign="Center"  CssClass="wrp"/>
                        <SelectedRowStyle Font-Bold="True" Wrap="true"  CssClass="wrp" />
                        <HeaderStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" CssClass="wrp" />
                        <EditRowStyle BackColor="#2461BF"  CssClass="wrp" />
                        <AlternatingRowStyle BackColor="Lavender" Font-Size="8pt" 
                                    Font-Underline="False"  CssClass="wrp" />
                        <RowStyle Font-Size="8pt" Wrap="true"  CssClass="wrp" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sel">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DropDownList1" CssClass="txtbox" Width="80px" Font-Size="8pt" runat="server">
                                    <asp:ListItem>LPO</asp:ListItem>
                                        <asp:ListItem>SPO</asp:ListItem>
                                        <asp:ListItem>FPO</asp:ListItem>
                                         <asp:ListItem>RETURN</asp:ListItem>
                                    </asp:DropDownList>                          
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Button ID="Button1" runat="server" Text="Proceed" OnClick="Button1_Click" 
                                        CssClass="btn2" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    </ContentTemplate>
            </asp:UpdatePanel>
        
        </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
            <asp:Panel ID="Panel4" runat="server" BorderStyle="Solid" BorderWidth="2px" CssClass="tbl"
                    DefaultButton="btncancel" Height="200px" ScrollBars="Auto" 
                    Style="border: 2px solid black; padding: 20px; display:none; background-color: white"  
                    Width="456px" HorizontalAlign="Center">
                    <div  style="border-color: #e6e6fa; border-width: 1px; text-align:center; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='white', startcolorstr='#D6DCF9', gradienttype='0');
                        width: 92%; height: 177px; text-align: center; ">
                        &nbsp;&nbsp;
                        <table id="tblmsg" runat="server" style="width: 85%" 
                            align="center">
                        <tr>
                            <td colspan="1" style="width: 364px; height: 18px; text-align: center">
                                <span style="color: #ff0000"><strong>&nbsp;REASON FOR MPR ITEM RETURN</strong></span></td>
                        </tr>
                        <tr>
                            <td style="width: 364px; height: 13px">
                            </td>
                        </tr>
                            <tr>
                                <td style="width: 364px; text-align: center;">
                                    <asp:TextBox ID="txtcomments" runat="server" CssClass="txtbox" Width="392px"></asp:TextBox>
                                </td>
                            </tr>
                        <tr>
                            <td style="width: 364px; text-align: center">
                           
                    </td>
                        </tr>
                        <tr>
                            <td colspan="1" style="height: 19px; width: 364px;">
                            </td>
                        </tr>
                            <tr>
                                <td colspan="1" style="width: 364px; height: 29px; text-align: center">
                                    <asp:Button ID="btncancel" CssClass="btn2"  runat="server" Width="120px"  Text="CANCEL"
                                         />
                                    &nbsp;<asp:Button ID="btnok" runat="server" CssClass="btn2" OnClick="btnok_Click" Text="RETURN"
                                        Width="102px" /></td>
                            </tr>
                    </table>
                    </div>
                </asp:Panel>
                <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender5" runat="server" DisplayModalPopupID="ModalPopupExtender5"
                    TargetControlID="Button2">
                </ajaxToolkit:ConfirmButtonExtender>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender5" runat="server" BackgroundCssClass="modalBackground"
                    CancelControlID="btncancel" PopupControlID="Panel4" TargetControlID="Button2">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Button ID="Button2" runat="server" Text="Button" Visible="False" />
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

