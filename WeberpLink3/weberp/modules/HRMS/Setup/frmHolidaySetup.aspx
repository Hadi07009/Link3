<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmHolidaySetup.aspx.cs" Inherits="modules_HRMS_Setup_frmHolidaySetup" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>

            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="HOLIDAY SETUP" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
                    <tr>
                        <td style="width: 120px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td></td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 120px">
                            <asp:Label ID="Label8" runat="server" Text="Year"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" Width="380px">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 120px">
                            <asp:Label ID="Label9" runat="server" Text="Period"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlPeriod" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPeriod_SelectedIndexChanged" Width="380px">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 120px">
                            <asp:Label ID="Label2" runat="server" Text="Select Company"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlcompany" runat="server" AutoPostBack="True" CssClass="tbl" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" Width="380px">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 120px">
                            <asp:Label ID="Label10" runat="server" Text="Office Location"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                        <td>
                            <div style="OVERFLOW-Y: scroll; WIDTH: 380px; HEIGHT: 300px; border: 1px solid; border-color: #669999; border-style: Ridge">

                                <asp:CheckBoxList ID="chkofficelocation" runat="server" AutoPostBack="false" OnSelectedIndexChanged="chkofficelocation_SelectedIndexChanged">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 120px">
                            <asp:Label ID="Label11" runat="server" Text="Shift"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                        <td align="left" colspan="4">
                            <div style="OVERFLOW-Y: scroll; WIDTH: 380px; HEIGHT: 150px; border: 1px solid; border-color: #669999; border-style: Ridge;">
                                <asp:CheckBoxList ID="chkshift" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chkshift_SelectedIndexChanged">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px">
                            <asp:Label ID="Label6" runat="server" Text="Date"></asp:Label>
                        </td>
                        <td>:</td>
                        <td colspan="4">
                            <ew:CalendarPopup ID="popupHolidayDate" runat="server" AutoPostBack="True" Culture="English (United Kingdom)" Enabled="true" Width="348px">
                                <MonthHeaderStyle BackColor="#2A2965" />
                                <ButtonStyle CssClass="btn2" />
                            </ew:CalendarPopup>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px">
                            <asp:Label ID="Label7" runat="server" Text="Holiday&nbsp; Description"></asp:Label>
                        </td>
                        <td>:</td>
                        <td colspan="4">
                            <asp:TextBox ID="txtDescription" runat="server" Height="50px" TextMode="MultiLine" Width="380px"></asp:TextBox>
                            <%--<ajaxToolkit:AutoCompleteExtender ID="txtDescription_AutoCompleteExtender" runat="server" Enabled="true" MinimumPrefixLength="0" ServiceMethod="GetHolidayDescription" ServicePath="~/modules/Payroll/WebService.asmx" TargetControlID="txtDescription">
                            </ajaxToolkit:AutoCompleteExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td colspan="4">
                            <asp:Button ID="btnSaveHoliday" runat="server" OnClick="btnSaveHoliday_Click" Text="Save" Width="100px" />
                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="100px" />
                            <asp:Label ID="lblForRefNoForUpdate" runat="server" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style11" style="width: 120px"></td>
                        <td class="auto-style7"></td>
                        <td class="auto-style7" colspan="4">
                            <asp:RadioButtonList ID="rblForConfiguration" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblForConfiguration_SelectedIndexChanged" RepeatDirection="Horizontal" Visible="False">
                                <asp:ListItem Selected="True" Value="S">Shift Wise</asp:ListItem>
                                <asp:ListItem Value="E">Employee Wise</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style11" style="width: 120px">&nbsp;</td>
                        <td class="auto-style7">&nbsp;</td>
                        <td class="auto-style7" colspan="4">
                            <asp:TextBox ID="txtEmployeeCode" runat="server" AutoPostBack="True" OnTextChanged="txtEmployeeCode_TextChanged" Visible="False" Width="380px"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="txtEmployeeCode_AutoCompleteExtender" runat="server" BehaviorID="txtEmployeeCode_AutoCompleteExtender" Enabled="true" MinimumPrefixLength="1" ServiceMethod="GetEmpId" ServicePath="~/modules/Payroll/WebService.asmx" TargetControlID="txtEmployeeCode">
                            </ajaxToolkit:AutoCompleteExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style11" style="width: 120px">&nbsp;</td>
                        <td class="auto-style7">&nbsp;</td>
                        <td class="auto-style7" colspan="4">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style7" colspan="6">
                            <asp:GridView ID="grdShowHoliday" runat="server" AutoGenerateColumns="False" OnRowCommand="grdShowHoliday_RowCommand" OnRowDataBound="grdShowHoliday_RowDataBound" OnRowDeleting="grdShowHoliday_RowDeleting" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <%# Container.DisplayIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reference No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRefNo" runat="server" Text='<%# Bind("ReferenceNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCompanyCode" runat="server" Text='<%# Bind("CompanyCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCompanyName" runat="server" Text='<%# Bind("CompanyName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="OfficeLocation Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOfficeLocationCode" runat="server" Text='<%# Bind("OfficeLocationCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="OfficeLocation Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOfficeLocationName" runat="server" Text='<%# Bind("Division_Master_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Shift ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblShiftId" runat="server" Text='<%# Bind("ShiftID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Shift">
                                        <ItemTemplate>
                                            <asp:Label ID="lblShift" runat="server" Text='<%# Bind("Shift_Mas_Desc") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Holiday Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHolidayDate" runat="server" Text='<%# Bind("HolidayDate", "{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Holiday Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHolidayDescription" runat="server" Text='<%# Bind("HolidayDescription") %>'></asp:Label>
                                        </ItemTemplate>
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
                        <td class="auto-style7" colspan="6">
                            <asp:GridView ID="grdShowHolidayEmpWise" runat="server" AutoGenerateColumns="False" OnRowCommand="grdShowHolidayEmpWise_RowCommand" OnRowDataBound="grdShowHolidayEmpWise_RowDataBound" OnRowDeleting="grdShowHolidayEmpWise_RowDeleting" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <%# Container.DisplayIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reference No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRefNo" runat="server" Text='<%# Bind("ReferenceNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCompanyCode" runat="server" Text='<%# Bind("CompanyCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCompanyName" runat="server" Text='<%# Bind("CompanyName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="OfficeLocation Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOfficeLocationCode" runat="server" Text='<%# Bind("OfficeLocationCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="OfficeLocation Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOfficeLocationName" runat="server" Text='<%# Bind("Division_Master_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Shift ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblShiftId" runat="server" Text='<%# Bind("ShiftID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Shift">
                                        <ItemTemplate>
                                            <asp:Label ID="lblShift" runat="server" Text='<%# Bind("Shift_Mas_Desc") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Holiday Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHolidayDate" runat="server" Text='<%# Bind("HolidayDate", "{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Holiday Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHolidayDescription" runat="server" Text='<%# Bind("HolidayDescription") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmployeeID" runat="server" Text='<%# Bind("EmployeeID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Configure Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblConfigureType" runat="server" Text='<%# Bind("ConfigureType") %>'></asp:Label>
                                        </ItemTemplate>
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
                        <td style="width: 120px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td colspan="4">&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveHoliday" />
        </Triggers>

    </asp:UpdatePanel>

</asp:Content>

