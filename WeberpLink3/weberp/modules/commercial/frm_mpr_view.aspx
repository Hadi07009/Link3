<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_mpr_view.aspx.cs" Inherits="frm_mpr_view" Title=""   EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register src="usercontrols/ctl_comments.ascx" tagname="ctl_comments" tagprefix="uc1" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="update" runat="server">
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
                PURCHASE REQUISITION DETAIL SCREEN</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 34px; text-align: left">
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
            <td class="tbl" colspan="3" style="height: 22px; text-align: center">
                
                
                <table id="tbl_po" width="99%" runat="server" style="text-align: left; border-top-width: 1px; border-left-width: 1px; border-left-color: #e6e6fa; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='white', startcolorstr='#D6DCF9', gradienttype='0'); border-bottom-width: 1px; border-bottom-color: #e6e6fa; border-top-color: #e6e6fa; border-right-width: 1px; border-right-color: #e6e6fa; ">
                    <tr>
                        <td style="text-align: left">Req Ref No:&nbsp;
                            <asp:Label ID="lblref" runat="server" Font-Bold="True" Text="Label" 
                                Width="150px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            Date Time:&nbsp;&nbsp;
                            <asp:Label ID="lbldate" runat="server" Font-Bold="True" Text="Label" 
                                Width="300px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            Dept Name:&nbsp;
                            <asp:Label ID="lbldept" runat="server" Font-Bold="True" Text="Label" 
                                Width="300px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            Comments:&nbsp;&nbsp;
                            <asp:Label ID="lblcomments" runat="server" Font-Bold="True" Text="Label" 
                                Width="300px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            Status:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Text="Label" 
                                Width="300px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                    <td  style="text-align: center">DETAIL
                        </td>
                        
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:GridView ID="gdItem" runat="server" BackColor="White" 
                                BorderColor="#41519A" BorderStyle="Solid" BorderWidth="10px" CellPadding="4" 
                                ForeColor="#333333" GridLines="None" 
                                PageSize="100" 
                                SkinID="GridView" 
                                style="border-top-width: 1px; border-left-width: 1px; border-left-color: #e6e6fa; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='white', startcolorstr='#D6DCF9', gradienttype='0'); border-bottom-width: 1px; border-bottom-color: #e6e6fa; border-top-color: #e6e6fa; border-right-width: 1px; border-right-color: #e6e6fa;" 
                                Width="98%" Font-Size="8pt">
                                <FooterStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#41519A" ForeColor="White" HorizontalAlign="Center" />                               
                                <HeaderStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                                
                                <EditRowStyle BackColor="Lavender" Font-Size="8pt" Font-Strikeout="False" />
                                <AlternatingRowStyle BackColor="Lavender" Font-Size="8pt" 
                                    Font-Underline="False" />
                               
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            COMMENTS</td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:PlaceHolder ID="phcomm" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; height: 22px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; height: 25px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 119px; text-align: center">
                &nbsp;</td>
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

