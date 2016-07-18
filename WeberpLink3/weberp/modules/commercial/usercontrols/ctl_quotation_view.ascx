<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_quotation_view.ascx.cs" Inherits="ClientSide_modules_commercial_usercontrols_ctl_quotation_view" %>
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

    .style4
    {
       
    }

    </style>

<table>
 <tr>
   <td>
        <asp:Panel ID="HeaderPanel" runat="server" style="cursor: pointer;">
                                    <div >
                                        <asp:ImageButton ID="ToggleImage" runat="server" ImageUrl="~/images/expand.jpg" AlternateText="collapse" />                                       
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
       <asp:Panel id="ContentPanel" runat="server" Height="0px"  style="overflow:hidden;" >   
                <table id="tbl_product"  runat="server"  style="border-color: #e6e6fa; border-width: 1px; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='#ffffff', startcolorstr='#e6e6fa', gradienttype='0'); ">
                    <tr>
                        <td id="celquotation" runat="server" style="text-align: left">
                            <table id="tbl_party" runat="server" >
                                <tr>
                                    <td bgcolor="#ccccff" style="width: 59px">
                                        Sl</td>
                                    <td bgcolor="#ccccff" style="width: 6px">
                                        Code</td>
                                    <td bgcolor="#ccccff">
                                        Party</td>
                                    <td bgcolor="#ccccff">
                                        Rate</td>
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
                                </tr>
                            </table>
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
                                            