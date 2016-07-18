<%@ Page Language="C#" MasterPageFile ="~/masMain.master" AutoEventWireup="true" CodeFile="OffDayConfiguration.aspx.cs" Inherits="modules_HRMS_Payments_OffDayConfiguration" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="Configuration Off Day Over Time Night" runat="server" Font-Bold="True"  />
            </asp:Panel>
         <asp:Panel ID="Panel1" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
            <table style="width: 100%; height: 206px;">
                <tr>
                    <td style="height: 16px; width: 2%;">
                        </td>
                    <td style="height: 16px; width: 28%;">
                        </td>
                    <td style="height: 16px; width: 206px;">
                        </td>
                    <td style="height: 16px; width: 62%;">
                        </td>
                    <td style="height: 16px; width: 2%;">
                        </td>
                </tr>
                <tr>
                    <td style="height: 16px; width: 2%;">
                        &nbsp;</td>
                    <td align="center" colspan="4">
                        <asp:Label ID="Label2" runat="server" ForeColor="#9900CC" Text="Label" 
                            Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height: 16px; width: 2%;">
                    </td>
                    <td align="center" style="height: 16px; width: 28%;">
                    
                        
                        &nbsp;</td>
                    <td align="center" style="height: 16px">
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                            RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                        </asp:RadioButtonList>
                    </td>
                    <td align="center" style="height: 16px">
                        <asp:Button ID="Button1" runat="server" onclick="Button1_Click1" 
                            Text="Save Configuration" CssClass="btn2"  />
                        &nbsp;<asp:Button ID="btnShowDesigbyDept" runat="server" 
                            onclick="btnShowDesigbyDept_Click" Text="Show Designation" Visible="False" />
                        <asp:Button ID="btnShowDesig" runat="server" 
                            Text="Show Data" onclick="btnShowDesig_Click" Visible="False" />
                    </td>
                    <td align="center" style="height: 16px; width: 2%;">
                    </td>
                </tr>
                <tr>
                    <td style="width: 2%">
                        &nbsp;</td>
                    <td rowspan="4" style="width: 28%">
                        
                    </td>
                    <td rowspan="4" valign="top">
                        <asp:Panel ID="Panel3" runat="server" BorderColor="Blue" BorderWidth="1px" 
                           ScrollBars="Vertical" Width="300px" CssClass="cpBodyContent">
                            <table>
                                <tr>
                                    <td style="width: 273px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 273px">
                                    
                                    <asp:GridView ID="gdvDept" runat="server" AutoGenerateColumns="False"  
                                PageSize="100" SkinID="GridView" 
                                style="border-color: #e6e6fa; border-width: 1px; text-align: left; color: #3366FF; margin-right: 27px;" 
                                Width="97%" CellPadding="4" ForeColor="#333333" CssClass="btn" 
                                            onrowcreated="gdvDept_RowCreated">
                                <EmptyDataRowStyle HorizontalAlign="Left" />
                                <FooterStyle 
                                    HorizontalAlign="Left" BackColor="#507CD1" Font-Bold="True" 
                                    ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle 
                                    HorizontalAlign="Left" BackColor="#507CD1" Font-Bold="True" 
                                    ForeColor="White" />
                                <EditRowStyle HorizontalAlign="Left" BackColor="#2461BF" />
                                <AlternatingRowStyle Font-Size="8pt" HorizontalAlign="Left" BackColor="White" />
                                <RowStyle Font-Size="8pt" BackColor="#EFF3FB" />
                                <Columns>
                                           
                                                              
                                    
                                    <asp:BoundField DataField="Dept_Name" HeaderText="Department name" 
                                        HeaderStyle-HorizontalAlign="Left">
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    
                                     <asp:BoundField DataField="Dept_code" HeaderText="Code" 
                                        HeaderStyle-HorizontalAlign="Left">
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    
                                   
                                                           
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                   
                                </Columns>
                            </asp:GridView>
                                    
                                    
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    
                    <td rowspan="4" valign="top" >
                        <asp:Panel ID="Panel4" runat="server" BorderColor="Blue" BorderWidth="1px" 
                            ScrollBars="Vertical" Width="446px" CssClass="cpBodyContent">
                            <table style="width: 70%">
                                <tr>
                                    <td>
                                    
                                    
                                        <asp:Label ID="LabelDepartment" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                    
                                    
                                       </td>
                                </tr>
                                <tr>
                                    <td>
                                    
                                    <asp:GridView ID="gdvDesignation" runat="server" AutoGenerateColumns="False" 
                                PageSize="100" SkinID="GridView" 
                                style="border-color: #e6e6fa; border-width: 1px; text-align: left;" 
                                Width="324px" CellPadding="4" ForeColor="#3366CC" CssClass="btn" BackColor="#3366CC" 
                                            onrowcreated="gdvDesignation_RowCreated">
                                <EmptyDataRowStyle HorizontalAlign="Left" />
                                <FooterStyle 
                                    HorizontalAlign="Left" BackColor="#507CD1" Font-Bold="True" 
                                    ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle 
                                    HorizontalAlign="Left" BackColor="#507CD1" Font-Bold="True" 
                                    ForeColor="White" />
                                <EditRowStyle HorizontalAlign="Left" BackColor="#2461BF" />
                                <AlternatingRowStyle Font-Size="8pt" HorizontalAlign="Left" BackColor="White" />
                                <RowStyle Font-Size="8pt" BackColor="#EFF3FB" />
                                <Columns>
                                           
                                                              
                                    
                                    <asp:BoundField DataField="JobTitle" HeaderText="Designation" 
                                        HeaderStyle-HorizontalAlign="Left">
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    
                                     <asp:BoundField DataField="JobCode" HeaderText="Code" 
                                        HeaderStyle-HorizontalAlign="Left">
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    
                                    
                                                            
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    
                                    
                                     <asp:TemplateField HeaderText="Rate(%)">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRate" Width="35px" runat="server"></asp:TextBox>
                                            
                                            <cc1:FilteredTextBoxExtender ID="txtRate_FilteredTextBoxExtender" 
                                                runat="server" Enabled="True" TargetControlID="txtRate"  FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars=".,1,2,3,4,5,6,7,8,9">
                                            </cc1:FilteredTextBoxExtender>
                                             </ItemTemplate>
                                    </asp:TemplateField> 
                                   
                                </Columns>
                            </asp:GridView>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                   
                    <td rowspan="4" style="width: 2%">
                        &nbsp;</td>
                </tr>
                 <tr>
                    <td style="width: 2%; height: 64px;">
                        </td>
                </tr>
                <tr>
                    <td style="width: 2%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 2%">
                        &nbsp;</td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    </asp:Content>
