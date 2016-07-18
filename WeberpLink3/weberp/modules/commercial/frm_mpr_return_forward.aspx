<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_mpr_return_forward.aspx.cs" Inherits="frm_mpr_return_forward" Title=""   EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register src="usercontrols/ctl_comments.ascx" tagname="ctl_comments" tagprefix="uc1" %> 
<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
    
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
                FORWARD MPR RETURNED ITEM</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 25px; text-align: left">
                Pending List:<asp:Label ID="lblcount" runat="server" Text="(0)"></asp:Label>
                <asp:DropDownList ID="ddllist" runat="server" Width="400px" 
                    onselectedindexchanged="ddllist_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
                <asp:Button ID="btnreload" runat="server" CssClass="btn2" 
                    onclick="btnreload_Click" Text="Reload" Width="77px" />
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
                        <td style="text-align: left">
                            &nbsp;<table style="width:100%;">
                                <tr>
                                    <td style="width: 75px">
                                        &nbsp;</td>
                                    <td style="width: 86px">
                                        Ref No</td>
                                    <td>
                                        :<asp:Label ID="lblref" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 75px">
                                        &nbsp;</td>
                                    <td style="width: 86px">
                                        Item Detail</td>
                                    <td>
                                        :<asp:Label ID="lblitemdet" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 75px">
                                        &nbsp;</td>
                                    <td style="width: 86px">
                                        Uom</td>
                                    <td>
                                        :<asp:Label ID="lbluom" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 75px">
                                        &nbsp;</td>
                                    <td style="width: 86px">
                                        Quantity</td>
                                    <td>
                                        :<asp:TextBox ID="txtqty" runat="server" CssClass="txtbox"></asp:TextBox>
                                         <ajaxToolkit:FilteredTextBoxExtender ID="txtqty_FilteredTextBoxExtender" 
                                runat="server" FilterType="Custom, Numbers" TargetControlID="txtqty" 
                                ValidChars=".">
                            </ajaxToolkit:FilteredTextBoxExtender>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 75px">
                                        &nbsp;</td>
                                    <td style="width: 86px">
                                        Specification</td>
                                    <td>
                                        :<asp:TextBox ID="txtspecification" runat="server" CssClass="txtbox" 
                                            Width="349px"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 75px">
                                        &nbsp;</td>
                                    <td style="width: 86px">
                                        Brand</td>
                                    <td>
                                        :<asp:TextBox ID="txtbrand" runat="server" CssClass="txtbox" Width="349px"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 75px">
                                        &nbsp;</td>
                                    <td style="width: 86px">
                                        Origin</td>
                                    <td>
                                        :<asp:TextBox ID="txtorigin" runat="server" CssClass="txtbox" Width="349px"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 75px">
                                        &nbsp;</td>
                                    <td style="width: 86px">
                                        Packing</td>
                                    <td>
                                        :<asp:TextBox ID="txtpacking" runat="server" CssClass="txtbox" Width="349px"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 75px">
                                        &nbsp;</td>
                                    <td style="width: 86px">
                                        Remarks</td>
                                    <td>
                                        :<asp:TextBox ID="txtremarks" runat="server" CssClass="txtbox" Width="349px"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 75px">
                                        &nbsp;</td>
                                    <td style="width: 86px">
                                        ETR</td>
                                    <td>
                                        :  <ew:CalendarPopup ID="cldetr" runat="server" 
                                Culture="English (United Kingdom)" DisableTextBoxEntry="False" Width="85px">
                                <ButtonStyle CssClass="btn2" />
                            </ew:CalendarPopup></td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
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
                        <td style="text-align: center">
                            <asp:Button ID="btnreject" runat="server" CssClass="btn2" 
                                Text="Cancel Item From MPR" onclick="btnreject_Click" Width="241px" />
                            &nbsp;
                            <asp:Button ID="btnforward" runat="server" CssClass="btn2" Text="Update and Forward Item" 
                                Width="241px" onclick="btnforward_Click" />
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
    <Triggers>
    <asp:AsyncPostBackTrigger ControlID="ddllist" EventName="SelectedIndexChanged" />    
    <asp:AsyncPostBackTrigger ControlID="btnforward" EventName="Click" />
    <asp:AsyncPostBackTrigger ControlID="btnreject" EventName="Click" />
    <asp:AsyncPostBackTrigger  ControlID="btnreload" EventName="Click" />
    
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>

