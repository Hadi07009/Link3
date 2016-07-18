<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmAdvanceTypeSetup.aspx.cs" Inherits="modules_HRMS_Setup_frmAdvanceTypeSetup" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
        <asp:Label ID="lblleave" Text="ADVANCE TYPE SETUP" runat="server" />
    </asp:Panel>
    <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
        <table style="width: 99%; text-align: left">
            
            <tr>
                <td style="width: 113px">&nbsp;</td>
                <td style="width: 2px">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 113px">
                    <asp:Label ID="Label3" runat="server"  Text="Select Company"></asp:Label>
                </td>
                <td style="width: 2px">:</td>
                <td>
                    <asp:DropDownList ID="ddlcompany" runat="server" AutoPostBack="True" CssClass="tbl" 
                        OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" Width="380px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 113px">
                    <asp:Label ID="Label4" runat="server"  Text="Advance Code"></asp:Label>
                </td>
                <td style="width: 2px">:</td>
                <td>
                    <asp:TextBox ID="txtAdvanceCode" runat="server" Width="375px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 113px">
                    <asp:Label ID="Label5" runat="server"  Text="Advance Name"></asp:Label>
                </td>
                <td style="width: 2px">:</td>
                <td>
                    <asp:TextBox ID="txtAdvanceName" runat="server" Width="375px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 113px">
                    <asp:Label ID="Label6" runat="server"  Text="Minimum Amount"></asp:Label>
                </td>
                <td style="width: 2px">:</td>
                <td>
                    <asp:TextBox ID="txtMinimumAmount" runat="server" Width="375px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 113px">
                    <asp:Label ID="Label7" runat="server"  Text="Maximum Amount"></asp:Label>
                </td>
                <td style="width: 2px">:</td>
                <td>
                    <asp:TextBox ID="txtMaximumAmount" runat="server" Width="375px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 113px">
                    <asp:Label ID="Label8" runat="server" Text="Mode Of Payment"></asp:Label>
                </td>
                <td style="width: 2px">:</td>
                <td>
                    <asp:DropDownList ID="ddlModeOfPayment" runat="server" Width="380px">
                        <asp:ListItem Selected="True" Value="-1">--- Please Select ---</asp:ListItem>
                        <asp:ListItem Value="1">Monthly</asp:ListItem>
                        <asp:ListItem Value="3">Quarterly</asp:ListItem>
                        <asp:ListItem Value="6">Half Yearly</asp:ListItem>
                        <asp:ListItem Value="12">Yearly</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 113px">
                    <asp:Label ID="Label9" runat="server"  Text="Frequency"></asp:Label>
                </td>
                <td style="width: 2px">:</td>
                <td>
                    <asp:TextBox ID="txtFrequency" runat="server" Width="375px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 113px">&nbsp;</td>
                <td style="width: 2px">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 113px">&nbsp;</td>
                <td style="width: 2px">&nbsp;</td>
                <td>
                    <asp:Button ID="btnSaveAdvanceType" runat="server" OnClick="btnSaveAdvanceType_Click" Text="Save" Width="100px" />
                </td>
            </tr>
            <tr>
                <td style="width: 113px"></td>
                <td style="width: 2px"></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:GridView ID="grdShowAdvanceType" runat="server" AutoGenerateColumns="False" Width="100%" 
                        OnRowCommand="grdShowAdvanceType_RowCommand" OnRowDeleting="grdShowAdvanceType_RowDeleting" 
                        OnRowDataBound="grdShowAdvanceType_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="SL">
                                <ItemTemplate>
                                    <%# Container.DisplayIndex + 1 %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CompanyCode">
                                <ItemTemplate>
                                    <asp:Label ID="lblCompanyCode" runat="server" Text='<%# Bind("companyCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company">
                                <ItemTemplate>
                                    <asp:Label ID="lblCompany" runat="server" Text='<%# Bind("CompanyName") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Advance Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblAdvanceCode" runat="server" Text='<%# Bind("advanceCode") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Advance Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblAdvanceName" runat="server" Text='<%# Bind("advanceName") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Minimum Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblMinimumAmount" runat="server" Text='<%# Eval("minimumAmount", "{0:0.00}") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Maximum Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblMaximumAmount" runat="server" Text='<%# Eval("maximumAmount", "{0:0.00}") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mode Of PaymentCode">
                                <ItemTemplate>
                                    <asp:Label ID="lblModeOfPaymentCode" runat="server" Text='<%# Bind("modeOfPaymentCode") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mode Of Payment">
                                <ItemTemplate>
                                    <asp:Label ID="lblModeOfPayment" runat="server" Text='<%# Bind("ModeofPayment") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Frequency">
                                <ItemTemplate>
                                    <asp:Label ID="lblFrequency" runat="server" Text='<%# Bind("frequency") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="True" />
                            <asp:CommandField ShowDeleteButton="True">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="width: 113px">&nbsp;</td>
                <td style="width: 2px">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </asp:Panel>

    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                alert("Please Enter Only Numeric Value:");
                return false;
            }
            return true;
        }
    </script>
</asp:Content>

