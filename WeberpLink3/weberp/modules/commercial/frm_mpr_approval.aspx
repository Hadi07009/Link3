<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_mpr_approval.aspx.cs" Inherits="frm_mpr_approval" Title=""   EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>
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
                PURCHASE REQUISITION APPROVAL SCREEN</td>
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
                                onrowcancelingedit="gdItem_RowCancelingEdit" 
                                OnRowDataBound="gdItem_RowDataBound" OnRowDeleting="gdItem_RowDeleting" 
                                OnRowEditing="gdItem_RowEditing" OnRowUpdating="gdItem_RowUpdating" 
                                onselectedindexchanged="gdItem_SelectedIndexChanged" PageSize="100" 
                                SkinID="GridView" 
                                style="border-top-width: 1px; border-left-width: 1px; border-left-color: #e6e6fa; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='white', startcolorstr='#D6DCF9', gradienttype='0'); border-bottom-width: 1px; border-bottom-color: #e6e6fa; border-top-color: #e6e6fa; border-right-width: 1px; border-right-color: #e6e6fa;" 
                                Width="98%" Font-Size="8pt">
                                <FooterStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#41519A" ForeColor="White" HorizontalAlign="Center" />                               
                                <HeaderStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                                
                                <EditRowStyle BackColor="Lavender" Font-Size="8pt" Font-Strikeout="False" />
                                <AlternatingRowStyle BackColor="Lavender" Font-Size="8pt" 
                                    Font-Underline="False" />
                                <Columns>
                                    <asp:CommandField ShowEditButton="True" />
                                    <asp:CommandField ShowDeleteButton="True" />
                                </Columns>
                               
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
                        <td style="text-align: left">
                            Comments:&nbsp;
                            <asp:TextBox ID="txtcomments" runat="server" CssClass="txtbox" 
                                TextMode="MultiLine" Width="575px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; height: 23px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            Forward to:
                            <asp:DropDownList ID="ddlforto" runat="server" CssClass="txtbox" Width="575px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; height: 27px;">
                            </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Label ID="lblComm" runat="server" ForeColor="Red" 
                                Text="Please type your comments." Visible="False" Width="477px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="btnreject" runat="server" CssClass="btn2" Text="Reject" 
                                Width="96px" onclick="btnreject_Click" />
                            &nbsp;
                            <asp:Button ID="btnforward" runat="server" CssClass="btn2" Text="Forward" 
                                Width="96px" onclick="btnforward_Click" />
                            &nbsp;
                            <asp:Button ID="btnapprove" runat="server" CssClass="btn2" Text="Approve" 
                                Width="96px" onclick="btnapprove_Click" />
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
    <asp:AsyncPostBackTrigger ControlID="btnapprove" EventName="Click" />
    <asp:AsyncPostBackTrigger ControlID="btnforward" EventName="Click" />
    <asp:AsyncPostBackTrigger ControlID="btnreject" EventName="Click" />
    <asp:AsyncPostBackTrigger  ControlID="btnreload" EventName="Click" />
    
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>

