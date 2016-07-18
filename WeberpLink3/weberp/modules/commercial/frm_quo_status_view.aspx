<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_quo_status_view.aspx.cs" Inherits="frm_quo_status_view" Title=""   ValidateRequest="false" EnableViewState="true"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register src="usercontrols/ctl_approval.ascx" tagname="ctl_approval" tagprefix="uc1" %>     
<%@ Register src="usercontrols/ctl_comments.ascx" tagname="ctl_comments" tagprefix="uc2" %> 
       
       
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
        width: 646px;
        font-size: 1em;
        text-align: left;
        border: thin solid #000080;
    }
    .style33
    {
        width: 676px;
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
        .style35
        {
            font-style: normal;
            font-variant: normal;
            font-weight: 300;
            font-size: 10pt;
            line-height: normal;
            font-family: verdana;
            color: #330099;
        }
    </style>
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
                QUOTATION INQUIRY DELAIL SCREEN&nbsp;</td>
        </tr>
        <tr>
            <td class="style35" colspan="3" style="text-align: left">
            </td>
        </tr>
        <tr>
            <td class="style35" colspan="3" style="text-align: left">
                PLANTS:
                <asp:DropDownList ID="ddlplants" runat="server" AutoPostBack="True" 
                    CssClass="txtbox" onselectedindexchanged="ddlplants_SelectedIndexChanged" 
                    Width="100px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                &nbsp;List:&nbsp;<asp:Label ID="lblcount" runat="server" Text="(0)" 
                    Visible="False"></asp:Label>
                &nbsp; 
                <asp:DropDownList ID="ddllist" runat="server" Width="700px" 
                    onselectedindexchanged="ddllist_SelectedIndexChanged" AutoPostBack="True" 
                    CssClass="txtbox">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: left">
                       
                            <table ID="tbltooltip" runat="server" class="style3" 
                                style="background-color: #FFFFFF; background-position: center center" 
                               >
                                <tr >
                                    <td colspan="2" >
                                        &nbsp;</td>
                                </tr>
                                <tr >
                                    <td class="style6" colspan="2" >
                                        TERMS AND CONDITIONS</td>
                                </tr>
                                <tr >
                                    <td class="style5" valign="top" >
                                        General Terms:</td>
                                    <td bgcolor="AliceBlue" >
                                        &nbsp;</td>
                                </tr>
                                <tr >
                                    <td class="style5" valign="top" >
                                        Special Terms:</td>
                                    <td bgcolor="AliceBlue">
                                        &nbsp;</td>
                                </tr>
                                <tr >
                                    <td class="style5" valign="top">
                                        Pay Terms:</td>
                                    <td bgcolor="AliceBlue" >
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                <td class="style5" valign="top">
                                    Valid Days:</td>
                                <td bgcolor="AliceBlue">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style5">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                                </table>
                        </td>
        </tr>
        <tr>
            <td  class="tbl" colspan="3" style="height: 24px; text-align: left">
            <table id="tblquotation" runat="server" style="width: 100%" >
    <tr>
        <td style="height: 10px">
         <asp:Panel ID="Panel4" runat="server" BorderStyle="Solid" BorderWidth="2px" CssClass="tbl"
                DefaultButton="btncancel" Height="500px" ScrollBars="Auto" Style="border-right: black 2px solid;
                padding-right: 20px; border-top: black 2px solid; padding-left: 20px; display:none;
                padding-bottom: 20px; border-left: black 2px solid; padding-top: 20px; border-bottom: black 2px solid;
                background-color: white" Width="700px">
                
                <table style="border-color: #e6e6fa; border-width: 1px; text-align: left; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='white', startcolorstr='#D6DCF9', gradienttype='0'); width: 688px;">
                <tr>                            
                    <td>                   
                        <table ID="tbltooltippnl" runat="server" class="style33" 
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
                                <td class="style5" valign="top">
                                    General Terms:</td>
                                <td bgcolor="AliceBlue">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style5" valign="top">
                                    Special Terms:</td>
                                <td bgcolor="AliceBlue">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style5" valign="top">
                                    Payment Terms:</td>
                                <td bgcolor="AliceBlue">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style5" valign="top">
                                    Valid Days:</td>
                                <td bgcolor="AliceBlue">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style5">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style5">
                                    &nbsp;</td>
                                <td>
                                    <asp:Button ID="btncancel" runat="server" CssClass="btn2" Text="Close" 
                                        Width="80px" />
                                    &nbsp;
                                    <asp:Button ID="btnedit" runat="server" CssClass="btn2" Text="Edit Terms &amp; Condition" 
                                        Width="207px" onclick="btnedit_Click" />
                                    &nbsp;
                                    <asp:Button ID="btneditval" runat="server" CssClass="btn2" 
                                        onclick="btneditval_Click" Text="Edit Rate &amp; Others" Width="155px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style5">
                                    </td>
                                <td>
                                    <br />
                                    <br />
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
        <td class="tbl">
                       
                <table id="tbl_product" width="100%" runat="server"  style="border-top-width: 1px; border-left-width: 1px; border-left-color: #e6e6fa; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='white', startcolorstr='#D6DCF9', gradienttype='0'); border-bottom-width: 1px; border-bottom-color: #e6e6fa; border-top-color: #e6e6fa; border-right-width: 1px; border-right-color: #e6e6fa;">
                    <tr>
                        <td style="text-align: center;">
                            <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Size="10pt" 
                                ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 551px">
                                <tr>
                                    <td style="width: 65px">
                                        Mpr Ref</td>
                                    <td style="width: 20px">
                                        :</td>
                                    <td >
                                        <asp:Label ID="lblmpr" runat="server" Width="200px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 65px">
                                        Quo
                            Ref</td>
                                    <td style="width: 20px">
                                        :</td>
                                    <td >
                                        <asp:Label ID="lblqref" runat="server" Width="200px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 65px">
                            Product</td>
                                    <td style="width: 20px">
                                        :</td>
                                    <td >
                                        <asp:Label ID="lblproduct" runat="server" Width="438px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 65px">
                            Quantity</td>
                                    <td style="width: 20px">
                                        :</td>
                                    <td >
                                        <asp:Label ID="lblqty" runat="server" Width="200px"></asp:Label>
                                    </td>                     

                                </tr>
                                <tr>
                                    <td style="width: 65px">
                                        Requisition</td>
                                    <td style="width: 20px">
                                        :</td>
                                    <td >
                                        <asp:Label ID="lblreq" runat="server" Width="200px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            Recent purchase Rate:
                            <asp:DropDownList ID="ddlrecentlist" runat="server" CssClass="txtbox" 
                                Width="300px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                       
                            <span style="font-size: 10pt; color: #000099"><strong>QUOTATION</strong></span></td>
                            
                    </tr>
                    <tr>
                        <td id="celquotation" runat="server" style="text-align: left">
                            <table id="tbl_party" runat="server" style="width: 98%">
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
                                        Amount</td>
                                    <td bgcolor="#ccccff">
                                        Specificaton</td>
                                    <td bgcolor="#ccccff">
                                        Brand</td>
                                    <td bgcolor="#ccccff">
                                        Origin</td>
                                    <td bgcolor="#ccccff">
                                        Packing</td>
                                </tr>
                                <tr>
                                    <td  style="width: 59px">
                                        <input id="Radio1" runat="server" type="radio" name="MaritalStatus" value="1" > </td>
                                    <td style="width: 6px">
                                    </td>
                                    <td>
                                        &nbsp;</td>
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
                                </tr>
                                <tr>
                                    <td style="width: 59px">
                                        <input id="Radio2" runat="server" type="radio" name="MaritalStatus" value="2"> </td>
                                    <td style="width: 6px">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
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
                                </tr>
                                <tr>
                                    <td  style="width: 59px">
                                        <input id="Radio3"  runat="server" type="radio" name="MaritalStatus" value="3"> </td>
                                    <td style="width: 6px">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
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
                                </tr>
                                <tr>
                                    <td style="width: 59px">
                                        <input id="Radio4" runat="server" type="radio" name="MaritalStatus" value="4">
                                        </td>
                                    <td style="width: 6px">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
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
                                </tr>
                                <tr>
                                    <td style="width: 59px">
                                        <input id="Radio5" runat="server" type="radio" name="MaritalStatus" value="5"> </td>
                                    <td style="width: 6px">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
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
                                </tr>
                                <tr>
                                    <td style="width: 59px">
                                        <input id="Radio6" runat="server" type="radio" name="MaritalStatus" value="6"> </td>
                                    <td style="width: 6px">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
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
                                </tr>
                                <tr>
                                    <td style="width: 59px">
                                        <input id="Radio7" runat="server" type="radio" name="MaritalStatus" value="7"> </td>
                                    <td style="width: 6px">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
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
                                </tr>
                                <tr>
                                    <td style="width: 59px">
                                        <input id="Radio8" runat="server" type="radio" name="MaritalStatus" value="8"> </td>
                                    <td style="width: 6px">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
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
                                </tr>
                                <tr>
                                    <td style="width: 59px">
                                        <input id="Radio9" runat="server" type="radio" name="MaritalStatus" value="9"> </td>
                                    <td style="width: 6px">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
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
                                </tr>
                                <tr>
                                    <td style="width: 59px">
                                        <input id="Radio10" runat="server" type="radio" name="MaritalStatus" value="10"> </td>
                                    <td style="width: 6px">
                                    </td>
                                    <td>
                                        
                                    </td>
                                    <td>
                                    </td>
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
                                </tr>
                                <tr>
                                    <td style="width: 59px">
                                        <input id="Radio11" runat="server" type="radio" name="MaritalStatus" value="11"> </td>
                                    <td style="width: 6px">
                                        &nbsp;</td>
                                    <td>
                                        
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 59px">
                                        <input id="Radio12" runat="server" type="radio" name="MaritalStatus" value="12"> </td>
                                    <td style="width: 6px">
                                        &nbsp;</td>
                                    <td>
                                        
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 59px">
                                        <input id="Radio13" runat="server" type="radio" name="MaritalStatus" value="13"> </td>
                                    <td style="width: 6px">
                                        &nbsp;</td>
                                    <td>
                                        
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 59px">
                                        <input id="Radio14" runat="server" type="radio" name="MaritalStatus" value="14"> </td>
                                    <td style="width: 6px">
                                        &nbsp;</td>
                                    <td>
                                        
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 59px">
                                        <input id="Radio15" runat="server" type="radio" name="MaritalStatus" value="15"> </td>
                                    <td style="width: 6px">
                                        &nbsp;</td>
                                    <td>
                                        
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td  class="style1" style="text-align: left; ">
                            &nbsp;</td>
                    </tr>
                    </table>
            </asp:Panel>
            
        </td>
    </tr>
    <tr>
        <td style="height: 1px; text-align: center;">
            COMMENTS</td>
    </tr>
    <tr>
        <td style="height: 1px">
                            <asp:PlaceHolder ID="phcomments" runat="server"></asp:PlaceHolder>
        </td>
    </tr>
</table>
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

