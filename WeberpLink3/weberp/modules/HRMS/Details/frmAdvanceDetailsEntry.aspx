<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmAdvanceDetailsEntry.aspx.cs" Inherits="modules_HRMS_Details_frmAdvanceDetailsEntry" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>

            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="Advance Details Entry" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Select Company"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlcompany" runat="server" AutoPostBack="True" CssClass="tbl"
                                OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" Width="380px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Employee Code"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtEmployeeCode" runat="server" AutoPostBack="True" Width="375px" OnTextChanged="txtEmployeeCode_TextChanged"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="txtEmployeeCode_AutoCompleteExtender" runat="server"
                                BehaviorID="txtEmployeeCode_AutoCompleteExtender"
                                Enabled="true"
                                MinimumPrefixLength="1"
                                ServiceMethod="GetEmpId"
                                ServicePath="~/modules/Payroll/WebService.asmx"
                                TargetControlID="txtEmployeeCode">
                            </ajaxToolkit:AutoCompleteExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Panel ID="PanelForEmployeeDetails" runat="server" Height="170px" Width="100%" ScrollBars="None">
                                <div style="text-align: center">
                                    <asp:Panel ID="PanelForDetailsHeader" runat="server" CssClass="cpHeaderContent" Height="15px" Width="100%" ScrollBars="None">
                                        <asp:Label ID="lblIntoEmployeeDetailsHD" runat="server" Text="EMPLOYEE DETAILS"></asp:Label>
                                    </asp:Panel>
                                </div>
                                <asp:Panel ID="PanelForEmployeeDetailsBody" runat="server" Height="155px" Width="100%" HorizontalAlign="Left" ScrollBars="None">
                                    <table style="width: 50%; text-align: left">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text="Name&nbsp;"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblEmployeeName" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Text="Office Location"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblOfficeLocation" runat="server" Text="Label"></asp:Label>
                                                <asp:Label ID="lblOfficeLocationCode" runat="server" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" Text="Department"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblEmployeeDepartment" runat="server" Text="Label"></asp:Label>
                                                <asp:Label ID="lblDepartmentCode" runat="server" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" Text="Section"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblSection" runat="server" Text="Label"></asp:Label>
                                                <asp:Label ID="lblSectionCode" runat="server" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" Text="Designation"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblDesignation" runat="server" Text="Label"></asp:Label>
                                                <asp:Label ID="lblDesignationCode" runat="server" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label9" runat="server" Text="Joining Date"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblJoiningDate" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <ajaxToolkit:CollapsiblePanelExtender ID="PanelForEmployeeDetailsBody_CollapsiblePanelExtender" runat="server" Enabled="True"
                                    TargetControlID="PanelForEmployeeDetailsBody"
                                    CollapseControlID="PanelForDetailsHeader"
                                    ExpandControlID="PanelForDetailsHeader"
                                    Collapsed="false"
                                    TextLabelID="lblIntoEmployeeDetailsHD"
                                    CollapsedText="EMPLOYEE DETAILS"
                                    ExpandedText="EMPLOYEE DETAILS"
                                    CollapsedSize="2"
                                    ExpandedSize="155"
                                    AutoCollapse="False"
                                    AutoExpand="false"
                                    ScrollContents="false"
                                    ImageControlID="Image1"
                                    ExpandedImage="~/images/collapse.jpg"
                                    CollapsedImage="~/images/expand.jpg"
                                    ExpandDirection="Vertical">
                                </ajaxToolkit:CollapsiblePanelExtender>

                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label10" runat="server" Text="Advance Type"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlAdvanceType" runat="server" Width="380px" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlAdvanceType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Panel ID="panelForAdvanceType" runat="server" Height="175px" Width="100%" ScrollBars="None">
                                <div style="text-align: center">
                                    <asp:Panel ID="panelAdTHD" runat="server" Width="100%" CssClass="cpHeaderContent" Height="15px" ScrollBars="None">
                                        <asp:Label ID="lblForAdvanceDetailsHD" runat="server" Text="ADVANCE TYPE DETAILS"></asp:Label>
                                    </asp:Panel>
                                </div>
                                <asp:Panel ID="panelAdTBody" runat="server" Height="150px" Width="100%" ScrollBars="None">
                                    <table style="width: 40%; text-align: left">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label11" runat="server" Text="Advance Name"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblAdvanceTypeName" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label12" runat="server" Text="Minimum Amount"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblMinimumAmount" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label13" runat="server" Text="Maximum Amount"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblMaximumAmount" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label14" runat="server" Text="Frequency"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblFrequency" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label15" runat="server" Text="Mode Of Payment"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblPaymentMethod" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <ajaxToolkit:CollapsiblePanelExtender ID="panelAdTBody_CollapsiblePanelExtender" runat="server" Enabled="True"
                                    TargetControlID="panelAdTBody"
                                    CollapseControlID="panelAdTHD"
                                    ExpandControlID="panelAdTHD"
                                    Collapsed="false"
                                    TextLabelID="lblForAdvanceDetailsHD"
                                    CollapsedText="ADVANCE TYPE DETAILS"
                                    ExpandedText="ADVANCE TYPE DETAILS"
                                    CollapsedSize="2"
                                    ExpandedSize="160"
                                    AutoCollapse="False"
                                    AutoExpand="false"
                                    ScrollContents="false"
                                    ImageControlID="Image1"
                                    ExpandedImage="~/images/collapse.jpg"
                                    CollapsedImage="~/images/expand.jpg"
                                    ExpandDirection="Vertical">
                                </ajaxToolkit:CollapsiblePanelExtender>

                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="left">
                            <div style="text-align: left">
                                <table style="text-align: left" width="46%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label16" runat="server" Text="Advance Amount"></asp:Label>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtAdvanceAmount" runat="server" onkeypress="return isNumberKey(event)"
                                                onkeyup="UpdateFrequencySize();" Style="text-align: center" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label19" runat="server" Text="No of Installment"></asp:Label>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtFrequency" runat="server" onkeypress="return isNumberKey(event)" onkeyup="UpdateFrequencySize();"
                                                Style="text-align: center" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label20" runat="server" Text="Installment Amount"></asp:Label>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtFrequencySize" runat="server" Enabled="False" Style="text-align: center" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label17" runat="server" Text="Advance Taken Date"></asp:Label>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <ew:CalendarPopup ID="calenderAdvanceTakenDate" runat="server" AutoPostBack="True" Culture="English (United Kingdom)" Enabled="true" Width="115px">
                                                <MonthHeaderStyle BackColor="#2A2965" />
                                                <ButtonStyle CssClass="btn2" />
                                            </ew:CalendarPopup>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label18" runat="server" Text="Deduction Start Date"></asp:Label>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <ew:CalendarPopup ID="calenderDeductionStartDate" runat="server" AutoPostBack="True" Culture="English (United Kingdom)" Enabled="true" OnDateChanged="calenderDeductionStartDate_DateChanged" Width="115px">
                                                <MonthHeaderStyle BackColor="#2A2965" />
                                                <ButtonStyle CssClass="btn2" />
                                            </ew:CalendarPopup>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label21" runat="server" Text="End Date"></asp:Label>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <ew:CalendarPopup ID="calenderDeductionEndDate" runat="server" AutoPostBack="True"
                                                Culture="English (United Kingdom)" Enabled="False" Width="115px">
                                                <MonthHeaderStyle BackColor="#2A2965" />
                                                <ButtonStyle CssClass="btn2" />
                                            </ew:CalendarPopup>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="3">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td width="100%">
                                                                <div style="text-align: center">
                                                                    <asp:Button ID="btnSaveAdvanceDetails" runat="server" OnClick="btnSaveAdvanceDetails_Click" Text="Save" Width="100px" />
                                                                    <asp:Label ID="lblReferenceNoForUpdate" runat="server" Visible="False"></asp:Label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="100%">
                                                                <asp:GridView ID="grdAdvanceDetails" runat="server" AutoGenerateColumns="False" OnRowCommand="grdAdvanceDetails_RowCommand" OnRowDataBound="grdAdvanceDetails_RowDataBound" OnRowDeleting="grdAdvanceDetails_RowDeleting" Width="100%">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="SL">
                                                                            <ItemTemplate>
                                                                                <%# Container.DisplayIndex + 1 %>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="ReferenceNo">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblReferenceNo" runat="server" Text='<%# Bind("referenceNo") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="CompanyCode">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblCompanyCode" runat="server" Text='<%# Bind("companyCode") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Company">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblCompany" runat="server" Text='<%# Bind("CompanyName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Employee ID">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblEmployeeID" runat="server" Text='<%# Bind("Adv_Det_Emp_Id") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="AdvanceTypeCode">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAdvanceTypeCode" runat="server" Text='<%# Bind("Adv_Det_Adv_Code") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Advance Type">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAdvanceType" runat="server" Text='<%# Bind("advanceName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Advance Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAdvanceAmount" runat="server" Text='<%# Eval("Adv_Det_Value", "{0:0.00}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Frequency">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFrequency" runat="server" Text='<%# Eval("frequencyValue", "{0:0.00}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Frequency Size">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFrequencySize" runat="server" Text='<%# Eval("Adv_Det_Inst_Val", "{0:0.00}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Advance Taken Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAdvanceTakenDate" runat="server" Text='<%# Bind("Adv_Det_Taken_Date", "{0:d}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Deduction Start Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDeductionStartDate" runat="server" Text='<%# Bind("Adv_Det_Sta_Date", "{0:d}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Deduction End Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDeductionEndDate" runat="server" Text='<%# Bind("Adv_Det_End_Date", "{0:d}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:CommandField ShowSelectButton="True">
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:CommandField>
                                                                        <asp:CommandField ShowDeleteButton="True">
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:CommandField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveAdvanceDetails" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">

        function UpdateFrequencySize() {
            var frequencyValue = document.getElementById("<%= txtFrequency.ClientID %>").value;
            if (frequencyValue == "" || frequencyValue == 0) {
                document.getElementById("<%= txtFrequencySize.ClientID %>").innerHTML = 0;
                document.getElementById("<%=txtFrequencySize.ClientID%>").value = 0;
            }
            else {
                document.getElementById("<%= txtFrequencySize.ClientID %>").innerHTML = (document.getElementById("<%= txtAdvanceAmount.ClientID %>").value / document.getElementById("<%= txtFrequency.ClientID %>").value).toFixed(4);
                document.getElementById("<%=txtFrequencySize.ClientID%>").value = (document.getElementById("<%= txtAdvanceAmount.ClientID %>").value / document.getElementById("<%= txtFrequency.ClientID %>").value).toFixed(4);
            }
        }
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
