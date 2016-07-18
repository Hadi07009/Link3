<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_cs_batch_approval.ascx.cs" Inherits="ClientSide_modules_commercial_usercontrols_ctl_cs_batch_approval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<style type="text/css">
	
table
{
	font-size: 1em;
   
}

.txtbox
{
    font-weight:300;
    color:black;
	font-style: normal;
	font-variant: normal;
	font-size: 9pt;
	line-height: normal;
	font-family: verdana;
	text-align: left;
}



A:link      { color: #41519A; text-decoration:none;}
    .style2
    {
        height: 34px;
    }
    


        .style2
        {
            width: 29px;
        }
            


.tbl
{
	font: 10pt verdana;
	font-weight: 300;
	color: #330099;
	
}

    .tbl
{
	font: 10pt verdana;
	font-weight: 300;
	color: #330099;	
}


        .btn2
{
	border: 1px Solid #41519A;
	background-color: White;
		color: #41519A;
		cursor:pointer;
	margin-left: 0px;
}

    </style>

<table>
 <tr>
   <td>
        <asp:Panel ID="HeaderPanel" runat="server" style="cursor: pointer;">
                                    <div >
                                        <asp:ImageButton ID="ToggleImage" runat="server" ImageUrl="~/images/expand.jpg" AlternateText="collapse" />                                       
                                        <asp:Label ID="lblheader" runat="server" Text="Label"></asp:Label>
                                    </div>
                                </asp:Panel>
   </td>
  </tr>
  <tr>
   <td>
   </td>
  </tr>
  <tr>
   <td >
       <asp:Panel id="ContentPanel" runat="server" Height="0px" Width="853px" style="overflow:hidden;" >   
                <table id="tbl_product" width="100%" runat="server"  style="border-top-width: 1px; border-left-width: 1px; border-left-color: #e6e6fa; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='white', startcolorstr='#D6DCF9', gradienttype='0'); border-bottom-width: 1px; border-bottom-color: #e6e6fa; border-top-color: #e6e6fa; border-right-width: 1px; border-right-color: #e6e6fa;">
                    <tr>
                        <td>
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
                                        <input id="Radio1" runat="server" type="radio" name="MaritalStatus" value="1" 
                                              > </td>
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
                                        <input id="Radio2" runat="server" type="radio" name="MaritalStatus" value="2" 
                                             > </td>
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
                                        <input id="Radio3"  runat="server" type="radio" name="MaritalStatus" 
                                            value="3"  > </td>
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
                                        <input id="Radio4" runat="server" type="radio" name="MaritalStatus" value="4" 
                                             >
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
                                        <input id="Radio5" runat="server" type="radio" name="MaritalStatus" value="5" 
                                             > </td>
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
                                        <input id="Radio6" runat="server" type="radio" name="MaritalStatus" value="6" 
                                             > </td>
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
                                        <input id="Radio7" runat="server" type="radio" name="MaritalStatus" value="7" 
                                             > </td>
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
                                        <input id="Radio8" runat="server" type="radio" name="MaritalStatus" value="8" 
                                             > </td>
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
                                        <input id="Radio9" runat="server" type="radio" name="MaritalStatus" value="9" 
                                             > </td>
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
                                        <input id="Radio10" runat="server" type="radio" name="MaritalStatus" 
                                            value="10"  > </td>
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
                                        <input id="Radio11" runat="server" type="radio" name="MaritalStatus" 
                                            value="11"  > </td>
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
                                        <input id="Radio12" runat="server" type="radio" name="MaritalStatus" 
                                            value="12"  > </td>
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
                                        <input id="Radio13" runat="server" type="radio" name="MaritalStatus" 
                                            value="13"  > </td>
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
                                        <input id="Radio14" runat="server" type="radio" name="MaritalStatus" 
                                            value="14"  > </td>
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
                                        <input id="Radio15" runat="server" type="radio" name="MaritalStatus" value="15" 
                                             > </td>
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
                        <td id="Td1" runat="server" style="text-align: center" class="tbl">
                            COMMENTS</td>
                    </tr>
                    <tr>
                        <td id="Td2" runat="server" style="text-align: left">
                            <asp:PlaceHolder ID="phcomments" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                </table>
             </asp:Panel>

      <ajaxToolkit:CollapsiblePanelExtender ID="cpe" runat="Server"
        TargetControlID="ContentPanel"
        ExpandControlID="HeaderPanel"
        CollapseControlID="HeaderPanel"
        Collapsed="True"
        SuppressPostBack="True"
        ExpandedImage="~/images/collapse.jpg"
        CollapsedImage="~/images/expand.jpg"
        ImageControlID="ToggleImage" /> 
   </td>
  </tr>
</table>
                                            