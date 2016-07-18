<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="COADepreciationSetup.aspx.cs" Inherits="modules_FixedAsset_Setup_COADepreciationSetup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div>
        <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
        <asp:Label ID="lblleave" Text="DEPRECIATION RATE SETUP" runat="server" />
    </asp:Panel>
    </div>
    <div align="center">          
        <asp:Panel ID ="pnlDepre" runat ="server" BorderColor="#999999" 
                        Width="100%">
                    <table width="100%">
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td class="style2">
                                &nbsp;</td>
                            <td class="style3">
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                            <td style="width: 6px">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Select "></asp:Label>
                            </td>
                            <td>
                                :</td>
                            <td>
                                <asp:DropDownList ID="ddlitem" runat="server" Width="305px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 6px">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Depreciation Rate"></asp:Label>
                            </td>
                            <td>
                                :</td>
                            <td>
                                <asp:TextBox ID="txtdpreciationrate" runat="server" Width="300px"></asp:TextBox>
                            </td>
                            <td style="width: 6px">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td >
                                <asp:Label ID="Label4" runat="server" Text="Depreciation Method"></asp:Label>
                            </td>
                            <td class="style3">
                                :</td>
                            <td>
                                <asp:DropDownList ID="ddlmethod" runat="server" Width="305px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 6px">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td class="style2">
                                &nbsp;</td>
                            <td class="style3">
                                &nbsp;</td>
                            <td>
                                <asp:Button ID="btnsave" runat="server" Text="Save" Width="100px" 
                                    onclick="btnsave_Click" />
                            </td>
                            <td style="width: 6px">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="style2">&nbsp;</td>
                            <td class="style3">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td style="width: 6px">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="style2" colspan="4">
                                <asp:GridView ID="GridView1" runat="server" Font-Names="Verdana" Font-Size="9pt" onrowdatabound="GridView1_RowDataBound" Width="100%" HeaderStyle-HorizontalAlign="Left">
                                </asp:GridView>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="style2">&nbsp;</td>
                            <td class="style3">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td style="width: 6px">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                
                </asp:Panel> 
                     
    </div>
 </asp:Content>