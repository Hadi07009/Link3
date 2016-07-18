<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_po_mrr_pending.aspx.cs" Inherits="frm_po_mrr_pending" Title=""    %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="updpnl" runat="server">
                <ContentTemplate>
<table id="tblmaster" runat="server" class="tblmas" style="width: 100%">
        <tr>
            <td style="height: 22px">
            </td>
        </tr>
        <tr>
            <td class="heading" style="text-align: center">
                MRR STATUS DETAIL</td>
        </tr>
        <tr>
            <td class="tbl" style="height: 24px; text-align: center">
                            <table style="width:100%;">
                                <tr>
                                    <td style="width: 83px">
                                        &nbsp;</td>
                                    <td style="width: 38px; text-align: left">
                                        &nbsp;</td>
                                    <td style="width: 10px">
                                        &nbsp;</td>
                                    <td colspan="4" style="text-align: left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 83px">
                                        &nbsp;</td>
                                    <td style="width: 38px; text-align: left">
                                        Plant</td>
                                    <td style="width: 10px">
                                        :</td>
                                    <td colspan="4" style="text-align: left">
                                        <asp:DropDownList ID="ddlplantlist" runat="server" CssClass="txtbox" 
                                            Width="300px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 83px">
                                        &nbsp;</td>
                                    <td style="width: 38px">
                                        &nbsp;</td>
                                    <td style="width: 10px">
                                        &nbsp;</td>
                                    <td style="width: 151px">
                                        &nbsp;</td>
                                    <td style="width: 36px">
                                        &nbsp;</td>
                                    <td style="width: 11px">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 83px">
                                        &nbsp;</td>
                                    <td style="text-align: left; width: 38px">
                                        From</td>
                                    <td style="width: 10px">
                                        :</td>
                                    <td style="text-align: left; width: 151px">
                                        <ew:CalendarPopup ID="cldfrom" runat="server" 
                                            Culture="English (United Kingdom)" Width="85px" 
                                            DisableTextBoxEntry="False">
                                            <ButtonStyle CssClass="btn2" />
                                        </ew:CalendarPopup>
                                    </td>
                                    <td style="text-align: left; width: 36px">
                                        To</td>
                                    <td style="text-align: left; width: 11px">
                                        :</td>
                                    <td style="text-align: left">
                                        <ew:CalendarPopup ID="cldto" runat="server" 
                                            Culture="English (United Kingdom)" Width="87px" 
                                            DisableTextBoxEntry="False">
                                            <ButtonStyle CssClass="btn2" />
                                        </ew:CalendarPopup>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 83px">
                                        &nbsp;</td>
                                    <td style="width: 38px">
                                        &nbsp;</td>
                                    <td style="width: 10px">
                                        &nbsp;</td>
                                    <td style="width: 151px">
                                        &nbsp;</td>
                                    <td style="width: 36px">
                                        &nbsp;</td>
                                    <td style="width: 11px">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 83px">
                                        &nbsp;</td>
                                    <td style="width: 38px">
                                        &nbsp;</td>
                                    <td style="width: 10px">
                                        &nbsp;</td>
                                    <td colspan="4" style="text-align: left;">
                                        <asp:RadioButtonList ID="rdotype" runat="server" CellPadding="0" 
                                            RepeatColumns="3" Width="500px">
                                            <asp:ListItem Selected="True">Full Pending</asp:ListItem>
                                            <asp:ListItem>Partial Pending</asp:ListItem>
                                            <asp:ListItem>No Pending</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 83px">
                                        &nbsp;</td>
                                    <td style="width: 38px">
                                        &nbsp;</td>
                                    <td style="width: 10px">
                                        &nbsp;</td>
                                    <td style="width: 151px">
                                        &nbsp;</td>
                                    <td style="width: 36px">
                                        &nbsp;</td>
                                    <td style="width: 11px">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 83px">
                                        &nbsp;</td>
                                    <td style="width: 38px">
                                        &nbsp;</td>
                                    <td style="width: 10px">
                                        &nbsp;</td>
                                    <td style="width: 151px; text-align: left">
                                        <asp:Button ID="btnview" runat="server" CssClass="btn2" onclick="btnview_Click" 
                                            Text="View" Width="128px" />
                                    </td>
                                    <td style="width: 36px">
                                        &nbsp;</td>
                                    <td style="width: 11px">
                                        &nbsp;</td>
                                    <td>
                                        </td>
                                </tr>
                            </table>
            </td>
        </tr>
    <tr>
        <td class="tbl" style="text-align: left">
             
            <asp:Button ID="btnexport" runat="server" CssClass="btn2" 
                onclick="btnexport_Click" Text="Export To Excel" Visible="False" 
                Width="157px" />
        </td>
    </tr>
        <tr>
            <td>             
                              
               
               
                
                
            </td>
        </tr>
        <tr>
            <td>
            
                 <asp:GridView ID="GdHeader" runat="server"  Width="100%" AutoGenerateColumns="false"
                    BackColor="White" BorderColor="#41519A" BorderStyle="Solid" BorderWidth="1px" 
                    CellPadding="4" ForeColor="#333333" 
                    PageSize="100" SkinID="GridView" 
                    style="border-color: #e6e6fa; border-width: 1px;  text-align: left;" 
                     onselectedindexchanged="GdHeader_SelectedIndexChanged" OnRowDataBound ="GdHeader_RowDataBound"
                     OnRowCommand="GdHeader_RowCommand"  OnSorting="GdHeader_Sorting" 
                      AllowSorting="True" GridLines="Horizontal" >
                    <FooterStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#41519A" ForeColor="White" HorizontalAlign="Left" />
                    <SelectedRowStyle Font-Bold="True" />
                    <HeaderStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" Font-Size="8pt" 
                        Font-Underline="False" />
                    <RowStyle Font-Size="8pt" Wrap="False" BackColor="White" />
                  <Columns>                 
                  <asp:BoundField DataField="DETAIL" HeaderText="DETAIL" HeaderStyle-HorizontalAlign="Left"/>
                  <asp:BoundField DataField="POREF" HeaderText="PO REF" HeaderStyle-HorizontalAlign="Left"/>
                  <asp:BoundField DataField="DATE" HeaderText="DATE"   HeaderStyle-HorizontalAlign="Left"/>
                  <asp:BoundField DataField="PARTY" HeaderText="PARTY"  HeaderStyle-HorizontalAlign="Left"/>
                  <asp:BoundField DataField="EMPLOYEE" HeaderText="EMPLOYEE"  HeaderStyle-HorizontalAlign="Left"/>
                  <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT"   HeaderStyle-HorizontalAlign="Left"/>
                  <asp:BoundField DataField="STATUS" HeaderText="STATUS" HeaderStyle-HorizontalAlign="Left"  />
                  
                        <asp:TemplateField HeaderText="MRR Detail" HeaderStyle-HorizontalAlign="Left" >
                            <ItemTemplate>
                                <asp:GridView ID="GridView1" runat="server" BackColor="White" 
                                    BorderColor="#41519A" BorderStyle="Solid" BorderWidth="1px" 
                                    CellPadding="4" ForeColor="#333333" GridLines="None" 
                                    PageSize="100" SkinID="GridView" >
                                    <FooterStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#41519A" ForeColor="White" HorizontalAlign="Left" />
                                    <SelectedRowStyle Font-Bold="True" />
                                    <HeaderStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="White" Font-Size="8pt" 
                                        Font-Underline="False" />
                                    <RowStyle Font-Size="8pt" Wrap="False" BackColor="White" />
                                </asp:GridView>
                            </ItemTemplate>
                        </asp:TemplateField>
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
                 &nbsp;</td>
        </tr>
        <tr>
            <td>
                 &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="height: 92px">
            </td>
        </tr>
    </table>
     </ContentTemplate>
               <Triggers>
     <asp:PostBackTrigger ControlID="btnexport" />
     </Triggers>
     
                </asp:UpdatePanel>
                
</asp:Content>

