<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_mpr_status_hdr.aspx.cs" Inherits="frm_mpr_status_hdr" Title=""    %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="updpnl" runat="server">
                <ContentTemplate>
<table id="tblmaster" runat="server" class="tblmas" style="width: 100%">
        <tr>
            <td style="height: 22px">
            </td>
        </tr>
        <tr>
            <td class="heading" style="text-align: center">
                MPR STATUS DETAIL&nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" style="height: 24px; text-align: center">
                            <table style="width:100%;">
                                <tr>
                                    <td style="width: 54px; text-align: left">
                                        &nbsp;</td>
                                    <td style="width: 10px">
                                        &nbsp;</td>
                                    <td colspan="4" style="text-align: left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 54px; text-align: left">
                                        Plant</td>
                                    <td style="width: 10px">
                                        :</td>
                                    <td colspan="4" style="text-align: left">
                                        <asp:DropDownList ID="ddlplantlist" runat="server" CssClass="txtbox" 
                                            Width="300px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 54px">
                                        &nbsp;</td>
                                    <td style="width: 10px">
                                        &nbsp;</td>
                                    <td style="width: 151px">
                                        &nbsp;</td>
                                    <td style="width: 36px">
                                        &nbsp;</td>
                                    <td style="width: 11px">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 54px">
                                        From</td>
                                    <td style="width: 10px">
                                        :</td>
                                    <td style="text-align: left; width: 151px">
                                        <ew:CalendarPopup ID="cldfrom" runat="server" 
                                            Culture="English (United Kingdom)" Width="85px" 
                                            DisableTextBoxEntry="False">
                                            <ButtonStyle CssClass="btn2" />
                                        </ew:CalendarPopup>
                                    </td>
                                    <td style="text-align: left; width: 36px">
                                        To</td>
                                    <td style="text-align: left; width: 11px">
                                        :</td>
                                    <td style="text-align: left">
                                        <ew:CalendarPopup ID="cldto" runat="server" 
                                            Culture="English (United Kingdom)" Width="87px" 
                                            DisableTextBoxEntry="False">
                                            <ButtonStyle CssClass="btn2" />
                                        </ew:CalendarPopup>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 54px">
                                        &nbsp;</td>
                                    <td style="width: 10px">
                                        &nbsp;</td>
                                    <td style="width: 151px">
                                        &nbsp;</td>
                                    <td style="width: 36px">
                                        &nbsp;</td>
                                    <td style="width: 11px">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 54px; text-align: left;">
                                        Status</td>
                                    <td style="width: 10px">
                                        :</td>
                                    <td colspan="4" style="text-align: left;">
                                        <asp:CheckBoxList ID="chkstatus" runat="server" CssClass="txtbox" Width="700px" 
                                            Font-Size="7pt" RepeatColumns="4">
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 54px">
                                        &nbsp;</td>
                                    <td style="width: 10px">
                                        &nbsp;</td>
                                    <td style="width: 151px">
                                        &nbsp;</td>
                                    <td style="width: 36px">
                                        &nbsp;</td>
                                    <td style="width: 11px">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 54px">
                                        </td>
                                    <td style="width: 10px">
                                        &nbsp;</td>
                                    <td style="width: 151px; text-align: left">
                                        <asp:Button ID="btnview" runat="server" CssClass="btn2" onclick="btnview_Click" 
                                            Text="View" Width="128px" />
                                    </td>
                                    <td style="width: 36px">
                                        &nbsp;</td>
                                    <td style="width: 11px">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 54px">
                                        &nbsp;</td>
                                    <td style="width: 10px">
                                        &nbsp;</td>
                                    <td style="width: 151px; text-align: left">
                                        &nbsp;</td>
                                    <td style="width: 36px">
                                        &nbsp;</td>
                                    <td style="width: 11px">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
            </td>
        </tr>
    <tr>
        <td class="tbl" style="text-align: left">
             
            <asp:Button ID="btnexport" runat="server" CssClass="btn2" 
                onclick="btnexport_Click" Text="Export To Excel" Visible="False" 
                Width="157px" />
        </td>
    </tr>
        <tr>
            <td>
                
                 <asp:UpdateProgress ID="sp1" runat="server">
                        <ProgressTemplate>
                           <%-- <div class="TransparentGrayBackground"> </div>--%>
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
            <td>
            
                 <asp:GridView ID="GdHeader" runat="server"  Width="100%"
                    BackColor="White" BorderColor="#41519A" BorderStyle="Solid" BorderWidth="1px" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" 
                    PageSize="100" SkinID="GridView" 
                    style="border-color: #e6e6fa; border-width: 1px;  text-align: left;" 
                     onselectedindexchanged="GdHeader_SelectedIndexChanged" OnRowDataBound ="GdHeader_RowDataBound"
                     OnRowCommand="GdHeader_RowCommand"  OnSorting="GdHeader_Sorting" 
                      AllowSorting="True" >
                    <FooterStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#41519A" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle Font-Bold="True" />
                    <HeaderStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="Lavender" Font-Size="8pt" 
                        Font-Underline="False" />
                    <RowStyle Font-Size="8pt" Wrap="False" />
                  
                </asp:GridView>
                 
                
                
                
                </td>
        </tr>
          <tr>
            <td style="text-align: center">
                <asp:Label ID="lbldetail" runat="server" Text="DETAIL VIEW" Visible="False"></asp:Label>
              </td>
        </tr>
        <tr>
            <td>
                 <asp:GridView ID="gdItem" runat="server"  Width="100%"
                    BackColor="White" BorderColor="#41519A" BorderStyle="Solid" BorderWidth="1px" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" 
                    PageSize="100" SkinID="GridView" 
                    style="border-color: #e6e6fa; border-width: 1px; text-align: left;" 
                     onselectedindexchanged="gdItem_SelectedIndexChanged">
                    <FooterStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#41519A" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle Font-Bold="True" />
                    <HeaderStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="Lavender" Font-Size="8pt" 
                        Font-Underline="False" />
                    <RowStyle Font-Size="8pt" Wrap="False" />
                  
               </asp:GridView>
                </td>
        </tr>
        <tr>
            <td style="height: 92px">
            </td>
        </tr>
    </table>
     </ContentTemplate>
               <Triggers>
     <asp:PostBackTrigger ControlID="btnexport" />
     </Triggers>
     
                </asp:UpdatePanel>
                
</asp:Content>

