<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_approval.ascx.cs" Inherits="ClientSide_modules_commercial_usercontrols_ctl_approval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<style type="text/css">
    .style1
    {
        height: 29px;
    }
    .style2
    {
        height: 34px;
    }
    


.tbl
{
	font: 10pt verdana;
	font-weight: 300;
	color: #330099;
	
}

    .style3
    {
        width: 500px;
        font-size: 1em;
        text-align: left;
        border: thin solid #000080;
    }
    .style4
    {
        text-align: center;
    }
    .style6
    {
        text-align: center;
        font-size: x-small;
    }
    .style5
    {
        width: 123px;
        font-weight: bold;
    }
    .heading
    {
        text-align: left;
    }
</style>



<table style="width: 100%" class="tblmas">
    <tr>
        <td style="height: 10px" colspan="3">
        
        </td>
    </tr>
    <tr>
        <td colspan="3" class="tbl">
            <asp:Panel ID="description_HeaderPanel" runat="server" Style="cursor: pointer" ForeColor="Navy">
                <div class="heading">
                    <asp:ImageButton ID="Description_ToggleImage" runat="server" AlternateText="collapse"
                        ImageUrl="~/images/collapse.jpg" /><asp:Label ID="lblhead" runat="server" Font-Bold="True"
                            Text="1. Product 121 121   123123   120 MTR"></asp:Label>&nbsp;</div>
            </asp:Panel>
            <asp:Panel ID="description_ContentPanel" runat="server" Width="670px" ScrollBars="Auto" Height="800px">
                <table id="tbl_product" runat="server"  style="border-top-width: 1px; border-left-width: 1px; border-left-color: #e6e6fa; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='white', startcolorstr='#D6DCF9', gradienttype='0'); border-bottom-width: 1px; border-bottom-color: #e6e6fa; border-top-color: #e6e6fa; border-right-width: 1px; border-right-color: #e6e6fa;">
                    <tr>
                        <td style="width: 58px">
                        </td>
                        <td style="width: 9px">
                        </td>
                        <td style="width: 22px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <table style="width: 551px">
                                <tr>
                                    <td style="width: 65px">
                                        Quo
                            Ref</td>
                                    <td style="width: 20px">
                                        :</td>
                                    <td id="celref" runat="server">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 65px">
                            Product</td>
                                    <td style="width: 20px">
                                        :</td>
                                    <td id="celproduct" runat="server">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 65px">
                            Quantity</td>
                                    <td style="width: 20px">
                                        :</td>
                                    <td id="celqty" runat="server">
                                    </td>                     

                                </tr>
                                <tr>
                                    <td style="width: 65px">
                                        Requisition</td>
                                    <td style="width: 20px">
                                        :</td>
                                    <td ID="celreq" runat="server">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center">
                            <span style="font-size: 10pt; color: #000099"><strong>QUOTATION</strong></span></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                       
                            <table ID="tbltooltip" runat="server" class="style3" 
                                style="background-color: #FFFFFF; background-position: center center">
                                <tr>
                                    <td colspan="2">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style6" colspan="2">
                                        TERMS AND CONDITIONS</td>
                                </tr>
                                <tr>
                                    <td class="style5">
                                        General Terms:</td>
                                    <td bgcolor="AliceBlue">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style5">
                                        Payments Terms:</td>
                                    <td bgcolor="AliceBlue">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style5">
                                        Delivery Terms:</td>
                                    <td bgcolor="AliceBlue">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style5">
                                        Other Terms:</td>
                                    <td bgcolor="AliceBlue">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                            
                    </tr>
                    <tr>
                        <td id="celquotation" runat="server" colspan="3" style="text-align: left">
                            <table id="tbl_party" runat="server" style="width: 660px">
                                <tr>
                                    <td bgcolor="#ccccff" style="width: 59px">
                                        Sl</td>
                                    <td bgcolor="#ccccff" style="width: 6px">
                                        Code</td>
                                    <td bgcolor="#ccccff">
                                        Party</td>
                                    <td bgcolor="#ccccff">
                                        Rate</td>
                                    <td bgcolor="#ccccff">
                                        Specification</td>
                                    <td bgcolor="#ccccff">
                                        Brand</td>
                                    <td bgcolor="#ccccff">
                                        Origin</td>
                                    <td bgcolor="#ccccff">
                                        Packing</td>
                                    <td bgcolor="#ccccff">
                                        T&amp;C</td>
                                </tr>
                                <tr>
                                    <td  style="width: 59px">
                                        <input runat="server" type="radio" name="MaritalStatus" value="1" Checked ="True">
                                        1.</td>
                                    <td style="width: 6px">
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:LinkButton ID="lnktc3" runat="server" onclick="lnktc1_Click">T&amp;C</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 59px">
                                        <input runat="server" type="radio" name="MaritalStatus" value="2">
                                        2.</td>
                                    <td style="width: 6px">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>  
 
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td  style="width: 59px">
                                        <input  runat="server" type="radio" name="MaritalStatus" value="3">
                                        3.</td>
                                    <td style="width: 6px">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 59px">
                                        <input runat="server" type="radio" name="MaritalStatus" value="4">
                                        4.</td>
                                    <td style="width: 6px">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 59px">
                                        <input runat="server" type="radio" name="MaritalStatus" value="5">
                                        5.</td>
                                    <td style="width: 6px">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 59px">
                                        <input runat="server" type="radio" name="MaritalStatus" value="6">
                                        6.</td>
                                    <td style="width: 6px">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 59px">
                                        <input runat="server" type="radio" name="MaritalStatus" value="7">
                                        7.</td>
                                    <td style="width: 6px">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 59px">
                                        <input runat="server" type="radio" name="MaritalStatus" value="8">
                                        8.</td>
                                    <td style="width: 6px">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 59px">
                                        <input runat="server" type="radio" name="MaritalStatus" value="9">
                                        9.</td>
                                    <td style="width: 6px">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 59px">
                                        <input runat="server" type="radio" name="MaritalStatus" value="10">
                                        10.</td>
                                    <td style="width: 6px">
                                    </td>
                                    <td>
                                        
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td  colspan="3" style="text-align: left">
                         <asp:Panel ID="Panel4" runat="server" BorderStyle="Solid" BorderWidth="2px" CssClass="tbl"
                DefaultButton="btncancel" Height="300px" ScrollBars="Auto" Style="border-right: black 2px solid;
                padding-right: 20px; border-top: black 2px solid; padding-left: 20px; 
                padding-bottom: 20px; border-left: black 2px solid; padding-top: 20px; border-bottom: black 2px solid;
                background-color: white" Width="600px">
                
                <table style="text-align: left; border-top-width: 1px; border-left-width: 1px; border-left-color: #e6e6fa; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='white', startcolorstr='#D6DCF9', gradienttype='0'); border-bottom-width: 1px; border-bottom-color: #e6e6fa; border-top-color: #e6e6fa; border-right-width: 1px; border-right-color: #e6e6fa; ">
                <tr>                            
                    <td>                   
                    <table id="Table1" runat="server" class="tbl"  style="width: 554px">
                        <tr>
                            <td style="width: 21px" bgcolor="#ccccff">
                                SL</td>
                            <td bgcolor="#ccccff">
                                Party</td>
                            <td bgcolor="#ccccff">
                                Rate</td>
                            <td bgcolor="#ccccff">
                                Specification</td>
                            <td bgcolor="#ccccff">
                                Brand</td>
                            <td bgcolor="#ccccff">
                                Origin</td>
                            <td bgcolor="#ccccff">
                                Packing</td>
                        </tr>
                        <tr>
                            <td style="width: 21px">
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 21px">
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="6" style="text-align: center; height: 26px;">
                                <asp:Button ID="btncancel" runat="server" CssClass="btn2" Text="Close" 
                                    Width="80px" />
                              <br /><br />
                            </td>
                            <td style="text-align: center; height: 26px;">
                                <br /><br />
                                </td>
                                
                        </tr>
                    </table>
                    </td>
                </tr>
                </table>
            </asp:Panel>
            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender5" runat="server" DisplayModalPopupID="ModalPopupExtender5"
                TargetControlID="Button1">
            </ajaxToolkit:ConfirmButtonExtender>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender5" runat="server" BackgroundCssClass="modalBackground"
                CancelControlID="btncancel"  PopupControlID="Panel4" TargetControlID="Button1">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Button ID="Button1" runat="server" Text="Button" Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td runat="server" colspan="3" style="text-align: center" class="tbl">
                            COMMENTS</td>
                    </tr>
                    <tr>
                        <td runat="server" colspan="3" style="text-align: left">
                            <asp:PlaceHolder ID="phcomments" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                    <tr>
                        <td runat="server" colspan="3" style="text-align: left">
                            Comments: <asp:TextBox ID="txtcomments" runat="server" CssClass="txtbox" Width="450px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td runat="server" colspan="3" style="text-align: left; height: 21px;">
                            Forward to:<asp:DropDownList ID="ddlforto" runat="server" CssClass="txtbox" 
                                Width="450px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td runat="server" class="style1" colspan="3" style="text-align: left; ">
                        </td>
                    </tr>
                    <tr>
                        <td runat="server" colspan="3" style="text-align: left">
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                            <asp:Button ID="btnreject" runat="server" CssClass="btn2" OnClick="btnreject_Click"
                                Text="Reject" Width="80px" />
                            &nbsp;
                            <asp:Button ID="btnforward" runat="server" CssClass="btn2" OnClick="btnforward_Click"
                                Text="Forward" Width="80px" />&nbsp;
                            <asp:Button ID="btnapp" runat="server" CssClass="btn2" onclick="btnapp_Click" 
                                Text="Approve" Width="80px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 58px; height: 35px;">
                        </td>
                        <td style="width: 9px; height: 35px;">
                        </td>
                        <td style="width: 22px; height: 35px;">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <ajaxToolkit:CollapsiblePanelExtender ID="cpeDesc" runat="Server" CollapseControlID="description_HeaderPanel"
                Collapsed="True" ExpandControlID="description_HeaderPanel" ImageControlID="description_ToggleImage"
                TargetControlID="description_ContentPanel" CollapsedImage="../../../../images/expand.jpg" ExpandedImage="../../../../images/collapse.jpg" SuppressPostBack="true"  >
            </ajaxToolkit:CollapsiblePanelExtender>
        </td>
    </tr>
    <tr>
        <td colspan="3" style="height: 1px">
        </td>
    </tr>
</table>
