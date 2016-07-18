<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_approved_po_list.aspx.cs" Inherits="frm_approved_po_list" Title=""   ValidateRequest="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
 
 <%@ Register src="usercontrols/ctl_po_approval.ascx" tagname="ctl_po_approval" tagprefix="uc1" %>    
 <%@ Register src="usercontrols/ctl_po_item_det.ascx" tagname="ctl_po_item_det" tagprefix="uc1" %>    
    
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
                PURCHASE ORDER APPROVAL SCREEN&nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: left">
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: left">
                PURCHASE ORDER STATUS:
                <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="ddlstatus_SelectedIndexChanged" Width="150px">
                    <asp:ListItem Value="APP">APPROVED</asp:ListItem>
                    <asp:ListItem>CLOSED</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: left">
             <asp:UpdatePanel ID="updpannel" runat="server">
        <ContentTemplate>
           <asp:Label ID="lblcount" runat="server" Text="Total Item: " Width="200px"></asp:Label>
           <asp:PlaceHolder ID="celctl" runat="server"></asp:PlaceHolder>
           <asp:UpdateProgress ID="sp1" runat="server">
                        <ProgressTemplate>
                           <%-- <div class="TransparentGrayBackground"></div>--%>
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
            </ContentTemplate>
            
            <Triggers>            
            
            </Triggers>
         </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: left">
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
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

