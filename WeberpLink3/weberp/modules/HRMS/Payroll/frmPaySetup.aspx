<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmPaySetup.aspx.cs" Inherits="modules_HRMS_Payroll_frmPaySetup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>

<%@ Register assembly="ProudMonkey.Common.Controls" namespace="ProudMonkey.Common.Controls" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text=" PAY SETUP" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
                    <tr>
                        <td valign="middle" style="width: 137px">&nbsp;</td>
                        <td valign="middle">&nbsp;</td>
                        <td valign="middle">&nbsp;</td>
                        <td valign="middle">&nbsp;</td>
                    </tr>
                    <tr>
                        <td valign="middle" style="width: 137px">
                            <asp:Label ID="Label10" runat="server" Text="Select Company"></asp:Label>
                        </td>
                        <td valign="middle">:</td>
                        <td valign="middle">
                            <asp:DropDownList ID="ddlcompany" runat="server" AutoPostBack="True" CssClass="tbl"
                                OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" Width="385px">
                            </asp:DropDownList>
                        </td>
                        <td valign="middle">&nbsp;</td>
                    </tr>
                    <tr>
                        <td valign="middle" style="width: 137px">
                            <asp:Label ID="Label11" runat="server" Text="Employee ID"></asp:Label>
                        </td>
                        <td valign="middle">:</td>
                        <td valign="middle">
                            <asp:TextBox ID="txtEmpId" runat="server" autocomplete="off" AutoPostBack="True" CssClass="btn2" OnTextChanged="txtEmpId_TextChanged" Width="380px"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="txtEmpId_AutoCompleteExtender" runat="server"
                                BehaviorID="txtEmpId_AutoCompleteExtender"
                                Enabled="true" MinimumPrefixLength="1"
                                ServiceMethod="GetAllEmployeeId"
                                ServicePath="~/modules/Payroll/WebService.asmx"
                                CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                CompletionListItemCssClass="autocomplete_listItem2"
                                TargetControlID="txtEmpId">
                            </ajaxToolkit:AutoCompleteExtender>
                        </td>
                        <td valign="middle">&nbsp;</td>
                    </tr>
                    <tr>
                        <td valign="middle" style="height: 22px; width: 137px">
                            <asp:Label ID="Label15" runat="server" Text="Name"></asp:Label>
                        </td>
                        <td valign="middle" style="height: 22px">:</td>
                        <td valign="middle" style="height: 22px">
                            <asp:Label ID="lblEmpName" runat="server" Width="100%"></asp:Label>
                        </td>
                        <td valign="middle" style="height: 22px"></td>
                    </tr>
                    <tr>
                        <td valign="middle" style="width: 137px">
                            <asp:Label ID="Label16" runat="server" Text="Grade"></asp:Label>
                        </td>
                        <td valign="middle">:</td>
                        <td valign="middle">
                            <asp:DropDownList ID="ddlGrade" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGrade_SelectedIndexChanged" Width="385px">
                            </asp:DropDownList>
                        </td>
                        <td valign="middle">&nbsp;</td>
                    </tr>
                    <tr>
                        <td valign="middle" style="width: 137px">
                            <asp:Label ID="Label12" runat="server" Text="Formula Code"></asp:Label>
                        </td>
                        <td valign="middle">:</td>
                        <td valign="middle">
                            <asp:DropDownList ID="ddlForFormula" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlForFormula_SelectedIndexChanged" Width="385px">
                            </asp:DropDownList>
                        </td>
                        <td valign="middle">&nbsp;</td>
                    </tr>
                    <tr>
                        <td valign="middle" style="width: 137px">
                            <asp:Label ID="Label17" runat="server" Text="Formula Name"></asp:Label>
                        </td>
                        <td valign="middle">:</td>
                        <td valign="middle">
                            <asp:Label ID="lblFormulaName" runat="server" Width="100%"></asp:Label>
                        </td>
                        <td valign="middle">&nbsp;</td>
                    </tr>
                    <tr>
                        <td valign="middle" style="width: 137px">
                            <asp:Label ID="Label13" runat="server" Text="Value For Code"></asp:Label>
                        </td>
                        <td valign="middle">:</td>
                        <td valign="middle">
                            <asp:CheckBox ID="chkboxForValue" runat="server" AutoPostBack="True" OnCheckedChanged="chkboxForValue_CheckedChanged" />
                        </td>
                        <td valign="middle">&nbsp;</td>
                    </tr>
                    <tr>
                        <td valign="middle" style="width: 137px">
                            <asp:Label ID="Label4" runat="server" Text="Enter Value" Width="90px"></asp:Label>
                        </td>
                        <td valign="middle">:</td>
                        <td valign="middle">
                            <asp:TextBox ID="txtValueForCode" runat="server" CssClass="btn2" onkeypress="return isNumberKey(event)" Width="100px"></asp:TextBox>
                        </td>
                        <td valign="middle">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 137px" valign="middle">
                            <asp:Label ID="Label18" runat="server" Text="Status"></asp:Label>
                        </td>
                        <td valign="middle">:</td>
                        <td valign="middle">
                            <asp:DropDownList ID="ddlStatus" runat="server" Width="385px">
                            </asp:DropDownList>
                        </td>
                        <td valign="middle">&nbsp;</td>
                    </tr>
                    <tr>
                        <td valign="middle" style="width: 137px">&nbsp;</td>
                        <td valign="middle">&nbsp;</td>
                        <td valign="middle">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn2" OnClick="btnSave_Click" Text="Save" Width="105px" />
                            <asp:Button ID="btnClear" runat="server" CssClass="btn2" OnClick="btnClear_Click" Text="Clear" Width="105px" />
                        </td>
                        <td valign="middle">&nbsp;</td>
                    </tr>
                    <tr>
                        <td valign="middle" style="width: 137px">&nbsp;</td>
                        <td valign="middle">&nbsp;</td>
                        <td valign="middle">&nbsp;</td>
                        <td valign="middle">&nbsp;</td>
                    </tr>
                    <tr>
                        <td valign="middle" style="width: 137px">&nbsp;</td>
                        <td valign="middle">&nbsp;</td>
                        <td valign="middle">
                            &nbsp;</td>
                        <td valign="middle">&nbsp;</td>
                    </tr>
                    <tr>
                        <td valign="middle" style="width: 137px">&nbsp;</td>
                        <td valign="middle">&nbsp;</td>
                        <td valign="middle">&nbsp;</td>
                        <td valign="middle">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4" valign="middle">
                            <asp:GridView ID="grdGetEmpForDet" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="grdGetEmpForDet_RowCancelingEdit"
                                OnRowCommand="grdGetEmpForDet_RowCommand" OnRowDataBound="grdGetEmpForDet_RowDataBound" OnRowDeleting="grdGetEmpForDet_RowDeleting"
                                OnRowEditing="grdGetEmpForDet_RowEditing" OnRowUpdating="grdGetEmpForDet_RowUpdating" ShowFooter="True" Width="100%">
                                <Columns>
                                    <%--<asp:BoundField DataField="For_Det_Empid" HeaderText="EmpId" />
                                            <asp:BoundField DataField="For_Det_ForCode" HeaderText="Formula Code" />
                                            <asp:BoundField DataField="For_Mas_Cal_Name" HeaderText="Formula Name" />
                                            <asp:BoundField DataField="For_Det_Value" HeaderText="Value" DataFormatString="{0:0.00}" />
                                            <asp:BoundField DataField="For_Det_ValFlg" HeaderText="Value Flag" />
                                            <asp:BoundField DataField="for_det_sno" HeaderText="Pay" />--%>
                                    <asp:TemplateField HeaderText="SL No">
                                        <ItemTemplate>
                                            <%# Container.DisplayIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EmpId">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpCode" runat="server" Text='<%# Bind("For_Det_Empid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Formula Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFormulaCode" runat="server" Text='<%# Bind("For_Det_ForCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Formula Name">
                                        <ItemTemplate>
                                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("For_Mas_Cal_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Value">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtValueForUpdate" runat="server" onkeypress="return isNumberKey(event)" Text='<%# Bind("For_Det_Value","{0:0.00}") %>' Width="150px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblValue" runat="server" Text='<%# Bind("For_Det_Value","{0:0.00}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Net">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNet" runat="server" Text='<%# Bind("For_Mas_Acc") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Value Flag">
                                        <ItemTemplate>
                                            <asp:Label ID="Label9" runat="server" Text='<%# Bind("For_Det_ValFlg") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pay">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPay" runat="server" Text='<%# Bind("for_det_sno") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Det_val2" HeaderText="Det_val2"  />
                                    <asp:TemplateField HeaderText="Status">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlStatusForUpdate" runat="server" Width="150px">
                                                <asp:ListItem Value="1">Active</asp:ListItem>
                                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("formulaStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowEditButton="True" />
                                    <asp:CommandField ShowDeleteButton="True" />
                                </Columns>
                                <FooterStyle BorderStyle="None" />
                            </asp:GridView>
                        </td>
                    </tr>

                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
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

