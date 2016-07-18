<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmSmtpSetup.aspx.cs" Inherits="frmSmtpSetup"   EnableEventValidation="false"   ValidateRequest="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <cc1:MessageBox ID="MessageBox1" runat="server" />

     <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                SMTP CONFIGURE
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
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                <asp:Label ID="lblmessage" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 8px; text-align: center">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 200px">&nbsp;</td>
                        <td style="font-size: small; width: 123px; text-align: right;">Mail Type ID</td>
                        <td style="width: 7px">:</td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlmailtypeid" runat="server"  Width="344px">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 200px">&nbsp;</td>
                        <td style="font-size: small; width: 123px; text-align: right;">Mail From</td>
                        <td style="width: 7px">:</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtmailfrom" runat="server" Width="401px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 200px">&nbsp;</td>
                        <td style="font-size: small; width: 123px; text-align: right;">Mail Address</td>
                        <td style="width: 7px">:</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtmailaddress" runat="server" Width="289px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 200px">&nbsp;</td>
                        <td style="font-size: small; width: 123px; text-align: right;">Password</td>
                        <td style="width: 7px">:</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtpassword" runat="server" Width="288px" TextMode="Password"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 200px">&nbsp;</td>
                        <td style="font-size: small; width: 123px; text-align: right;">SMTP</td>
                        <td style="width: 7px">:</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtsmtp" runat="server" Width="290px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 200px">&nbsp;</td>
                        <td style="font-size: small; width: 123px; text-align: right;">Subject</td>
                        <td style="width: 7px">:</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtsubject" runat="server" Width="401px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 200px; height: 103px"></td>
                        <td style="height: 103px; font-size: small; width: 123px; text-align: right;">Body</td>
                        <td style="width: 7px; height: 103px">:</td>
                        <td style="height: 103px; text-align: left">
                            <asp:TextBox ID="txtbody" runat="server" Height="100px" TextMode="MultiLine" Width="580px"></asp:TextBox>
                        </td>
                        <td style="height: 103px"></td>
                        <td style="height: 103px"></td>
                    </tr>
                    <tr>
                        <td style="width: 200px">&nbsp;</td>
                        <td style="font-size: small; width: 123px; text-align: right;">Status</td>
                        <td style="width: 7px">:</td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlstatus" runat="server" Width="114px">
                                <asp:ListItem Selected="True"></asp:ListItem>
                                <asp:ListItem Value="1">Active</asp:ListItem>
                                <asp:ListItem Value="0">In Active</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 200px">&nbsp;</td>
                        <td style="width: 123px">&nbsp;</td>
                        <td style="width: 7px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 200px">&nbsp;</td>
                        <td style="width: 123px">&nbsp;</td>
                        <td style="width: 7px">&nbsp;</td>
                        <td style="text-align: left">
                            <asp:Button ID="btnsave" runat="server" Text="Save/Update" Width="86px" OnClick="btnsave_Click" />
&nbsp;<asp:Button ID="btnclear" runat="server" Text="Clear" Width="86px" OnClick="btnclear_Click" />
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 200px">&nbsp;</td>
                        <td style="width: 123px">&nbsp;</td>
                        <td style="width: 7px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="6">
                         <asp:GridView ID="gvsmtp" runat="server" CellPadding="4" Font-Names="Verdana" Font-Size="Small" ForeColor="#333333" GridLines="None"  Width="100%" OnSelectedIndexChanged="gvsmtp_SelectedIndexChanged">
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
                        <td style="width: 200px">&nbsp;</td>
                        <td style="width: 123px">&nbsp;</td>
                        <td style="width: 7px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="6">

     <asp:Panel ID="PanelLeaveHdr0" runat="server" CssClass="cpHeaderContent" Width="100%">
                SMTP TEST
            </asp:Panel>

                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px">&nbsp;</td>
                        <td style="width: 123px">Mail To</td>
                        <td style="width: 7px">&nbsp;</td>
                        <td align="left">
                            <asp:TextBox ID="txtMailTo" runat="server" Width="401px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 200px">&nbsp;</td>
                        <td style="width: 123px">&nbsp;</td>
                        <td style="width: 7px">&nbsp;</td>
                        <td align="left">
                            <asp:Button ID="btnTestMail" runat="server" Text="Test Mail" Width="86px" OnClick="btnTestMail_Click" />
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    </table>
            </td>
        </tr>
                
        
    </table>

        
        </div>
    


</asp:Content>

