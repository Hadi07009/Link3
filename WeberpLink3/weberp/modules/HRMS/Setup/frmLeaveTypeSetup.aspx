<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmLeaveTypeSetup.aspx.cs" Inherits="modules_HRMS_Setup_frmLeaveTypeSetup" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>

            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="LEAVE TYPE SETUP" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
                    <tr>
                        <td style="width: 310px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Select Company"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlcompany" runat="server" AutoPostBack="True" CssClass="tbl" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" Width="380px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Leave Code"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtLeaveCode" runat="server" Width="380px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Leave Name"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtLeaveName" runat="server" Width="380px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Mode of Payment"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:RadioButtonList ID="rblModeOfPayment" runat="server" RepeatDirection="Horizontal" Width="380px">
                                <asp:ListItem Selected="True" Value="FP">Full Payment</asp:ListItem>
                                <asp:ListItem Value="HP">Half Payment</asp:ListItem>
                                <asp:ListItem Value="NP">No Payment</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Maximum Per Allow&nbsp;"></asp:Label>
                        </td>
                        <td style="height: 25px">:</td>
                        <td style="height: 25px">
                            <asp:TextBox ID="txtMaximumPerAllow" runat="server" Width="380px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="Employee Type"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlEmployeeType" runat="server" Width="380px">
                                <asp:ListItem Selected="True" Value="-1">--- Please Select ---</asp:ListItem>
                                <asp:ListItem>Officer</asp:ListItem>
                                <asp:ListItem>Staff</asp:ListItem>
                                <asp:ListItem>Worker</asp:ListItem>
                                <asp:ListItem>Internee</asp:ListItem>
                                <asp:ListItem>BOD</asp:ListItem>
                                <asp:ListItem>Manager</asp:ListItem>
                                <asp:ListItem>Trainee</asp:ListItem>
                                <asp:ListItem>Executive</asp:ListItem>
                                <asp:ListItem>Assistant</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label10" runat="server" Text="Gender"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:RadioButtonList ID="rblForGender" runat="server" RepeatDirection="Horizontal" Width="380px">
                                <asp:ListItem Selected="True" Value="B">Both</asp:ListItem>
                                <asp:ListItem Value="M">Male</asp:ListItem>
                                <asp:ListItem Value="F">Female</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="Carry Forword Next Year"></asp:Label>
                        </td>
                        <td style="height: 26px">:</td>
                        <td style="height: 26px">
                            <asp:RadioButtonList ID="rblCarryForword" runat="server" RepeatDirection="Horizontal" Width="130px">
                                <asp:ListItem Selected="True" Value="Y">Yes</asp:ListItem>
                                <asp:ListItem Value="N">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;<asp:Label ID="Label9" runat="server" Text="Maximum Leave Carry Forword to Next Year" Width="300px"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtMaximumLeaveCarryForword" runat="server" Width="380px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnSaveLeaveType" runat="server" OnClick="btnSaveLeaveType_Click" Text="Save" Width="100px" />
                            &nbsp;
                                    <asp:Button ID="btnClear" runat="server" Text="Clear" Width="100px" OnClick="btnClear_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <div style="text-align: center">
                                <asp:GridView ID="grdShowLeaveType" runat="server" AutoGenerateColumns="False" Width="100%" OnRowCommand="grdShowLeaveType_RowCommand" OnRowDeleting="grdShowLeaveType_RowDeleting" OnRowDataBound="grdShowLeaveType_RowDataBound">
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
                                                <asp:Label ID="lblCompanyCode" runat="server" Text='<%# Bind("CompanyCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompany" runat="server" Text='<%# Bind("CompanyName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Leave Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLeaveCode" runat="server" Text='<%# Bind("Leave_Mas_Code") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Leave Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLeaveName" runat="server" Text='<%# Bind("Leave_Mas_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mode of Payment">
                                            <ItemTemplate>
                                                <asp:Label ID="lblModeOfPayment" runat="server" Text='<%# Bind("ModeofPayment") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mode of PaymentCode">
                                            <ItemTemplate>
                                                <asp:Label ID="lblModeOfPaymentCode" runat="server" Text='<%# Bind("Leave_Mas_Mode_Of_Pay") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Maximum Per Allow">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmaxParAllow" runat="server" Text='<%# Bind("Leave_Mas_Max_Days_Allowed") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmployeeType" runat="server" Text='<%# Bind("T_C2") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Carry Forword Next Year">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCarryForword" runat="server" Text='<%# Bind("CRFlag") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Carry Forword Next YearCode">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCarryForwordCode" runat="server" Text='<%# Bind("Leave_Mas_CRFlag") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Maximum Leave Carry Forword to Next Year">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaxCarryForword" runat="server" Text='<%# Bind("Leave_Mas_CRDays") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Gender">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGender" runat="server" Text='<%# Bind("EmployeeGender") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GenderValue">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGenderValue" runat="server" Text='<%# Bind("T_C1") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowSelectButton="True" />
                                        <asp:CommandField ShowDeleteButton="True">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:CommandField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
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

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveLeaveType" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

