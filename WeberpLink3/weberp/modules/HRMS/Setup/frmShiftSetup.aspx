<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmShiftSetup.aspx.cs" Inherits="modules_HRMS_Setup_frmShiftSetup" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>
<%@ Register assembly="TimePicker" namespace="MKB.TimePicker" tagprefix="MKB" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>

            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="SHIFT SETUP" runat="server" Font-Bold="True"  />
            </asp:Panel>

            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">

                <table style="width:99%;text-align:left">
                    <tr>
                        <td style="width: 176px">&nbsp;</td>
                        <td style="width: 15px">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 176px">
                            <asp:Label ID="Label1" runat="server" Text="Shift Code"></asp:Label>
                        </td>
                        <td style="width: 15px">:</td>
                        <td>
                            <asp:TextBox ID="txtShiftCode" runat="server" Width="375px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 176px">
                            <asp:Label ID="Label2" runat="server" Text="Shift Title"></asp:Label>
                        </td>
                        <td style="width: 15px">:</td>
                        <td>
                            <asp:TextBox ID="txtShiftTitle" runat="server" Width="375px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 176px">Grace Time(Minutes)</td>
                        <td style="width: 15px">:</td>
                        <td>
                            <asp:TextBox ID="txtGraceTime" runat="server" Width="95px"></asp:TextBox>

                             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" 
                                runat="server" Enabled="True" FilterMode="ValidChars" 
                                FilterType="Custom, Numbers" TargetControlID="txtGraceTime" ValidChars=".">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 176px; height: 22px">
                            <asp:Label ID="Label3" runat="server" Text="From (Time)"></asp:Label>
                        </td>
                        <td style="height: 22px; width: 15px">:</td>
                        <td style="height: 22px">
                            <MKB:TimeSelector ID="timeoffIntime" runat="server" AmPm="AM" DisplaySeconds="False" Hour="9">
                            </MKB:TimeSelector>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 176px">
                            <asp:Label ID="Label4" runat="server" Text="To (Time)"></asp:Label>
                        </td>
                        <td style="width: 15px">:</td>
                        <td>
                            <MKB:TimeSelector ID="timeoffOuttime" runat="server" AmPm="PM" DisplaySeconds="False" Hour="6">
                            </MKB:TimeSelector>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 176px">&nbsp;</td>
                        <td style="width: 15px">&nbsp;</td>
                        <td>
                            <asp:Label ID="lblHour" runat="server" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 176px">&nbsp;</td>
                        <td style="width: 15px">&nbsp;</td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" OnClick="btnSave_Click" />
                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 176px">&nbsp;</td>
                        <td style="width: 15px">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="grdShift" runat="server" Width="100%" AutoGenerateColumns="False" OnRowCommand="grdShift_RowCommand" OnRowDeleting="grdShift_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <%# Container.DisplayIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Shift Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblShiftCode" runat="server" Text='<%# Bind("Shift_Mas_Code") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Shift Title">
                                        <ItemTemplate>
                                            <asp:Label ID="lblShiftTitle" runat="server" Text='<%# Bind("Shift_Mas_Desc") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="From (Time)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFromTime" runat="server" Text='<%# Bind("Shift_Mas_From") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To (Time)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblToTime" runat="server" Text='<%# Bind("Shift_Mas_To") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Grace Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGraceTime" runat="server" Text='<%# Bind("Shift_Grace_Time") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Total Hour">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalHour" runat="server" Text='<%# Bind("Shift_Mas_Total") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    
                                   

                                    <asp:CommandField ShowSelectButton="True" >
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:CommandField>
                                    <asp:CommandField ShowDeleteButton="True" >
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:CommandField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 176px">&nbsp;</td>
                        <td style="width: 15px">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>

            </asp:Panel>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

