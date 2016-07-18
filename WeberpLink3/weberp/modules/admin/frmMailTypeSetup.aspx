<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmMailTypeSetup.aspx.cs" Inherits="frmMailTypeSetup"   EnableEventValidation="false"   ValidateRequest="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
               MAIL TYPE SETUP
            </asp:Panel>

    <div style=" height:1000px; vertical-align:top; ">
       <table style="width: 100%" class="tblmas">
        <tr>
            <td style="height: 22px; width: 119px;">
            </td>
            <td style="height: 22px; width: 79%;">
            </td>
            <td style="height: 22px; width: 13%;">
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="3" style="text-align: center">
                MAIL TYPE&nbsp; SETUP</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                <asp:Label ID="lblmessage" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 8px; text-align: center">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 164px">&nbsp;</td>
                        <td style="font-size: small; width: 163px; text-align: right;">Mail Type ID</td>
                        <td style="width: 7px">:</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtMailTypeId" runat="server" ReadOnly="true"  Width="250px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 164px">&nbsp;</td>
                        <td style="font-size: small; width: 163px; text-align: right;">Mail Type Name</td>
                        <td style="width: 7px">:</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtMailTypeName" runat="server" Width="401px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 164px">&nbsp;</td>
                        <td style="width: 163px">&nbsp;</td>
                        <td style="width: 7px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 164px">&nbsp;</td>
                        <td style="width: 163px">&nbsp;</td>
                        <td style="width: 7px">&nbsp;</td>
                        <td style="text-align: left">
                            <asp:Button ID="btnsave" runat="server" Text="Save/Update" Width="86px" OnClick="btnsave_Click" />
&nbsp;<asp:Button ID="btnclear" runat="server" Text="Clear" Width="86px" OnClick="btnclear_Click" />
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 164px">&nbsp;</td>
                        <td style="width: 163px">&nbsp;</td>
                        <td style="width: 7px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="6">
                         <asp:GridView ID="gvmail" runat="server" CellPadding="4" Font-Names="Verdana" Font-Size="Small" ForeColor="#333333" GridLines="None"  Width="100%" OnSelectedIndexChanged="gvmail_SelectedIndexChanged">
                             <RowStyle BackColor="#EFF3FB" />
                             <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                             <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                             <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#99FF33" />
                             <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                             <EditRowStyle BackColor="#2461BF" />
                             <AlternatingRowStyle BackColor="White" />

                              <Columns>
                                 <asp:CommandField SelectText="Select" ShowSelectButton="True">
                                 <ItemStyle ForeColor="Red" />
                                 </asp:CommandField>
                             </Columns>
                            
                         </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 164px">&nbsp;</td>
                        <td style="width: 163px">&nbsp;</td>
                        <td style="width: 7px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    </table>
            </td>
        </tr>
                
        
    </table>

        
        </div>
    


</asp:Content>

