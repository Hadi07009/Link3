<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmManualLeaveEntry.aspx.cs" Inherits="modules_HRMS_Details_frmManualLeaveEntry" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
      <cc1:ConfirmBox ID="ConfirmBox1" runat="server" />
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>

            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                MANUAL LEAVE ENTRY
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="width: 125px">
                            <asp:Label ID="Label2" runat="server" Text="Select Company"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlcompany" runat="server" AutoPostBack="True" CssClass="tbl" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged"
                                Width="380px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Employee Code"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtEmployeeCode" runat="server" AutoPostBack="True" OnTextChanged="txtEmployeeCode_TextChanged" Width="375px"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="txtEmployeeCode_AutoCompleteExtender" runat="server"
                                BehaviorID="txtEmployeeCode_AutoCompleteExtender"
                                CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                CompletionListItemCssClass="autocomplete_listItem2"
                                Enabled="true"
                                MinimumPrefixLength="1"
                                ServiceMethod="GetEmpId"
                                ServicePath="~/modules/Payroll/WebService.asmx"
                                TargetControlID="txtEmployeeCode">
                            </ajaxToolkit:AutoCompleteExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Panel ID="PanelForEmployeeDetails" runat="server" Height="120px" ScrollBars="None" Width="100%">
                                <div style="text-align: center">
                                    <asp:Panel ID="PanelForDetailsHeader" runat="server" CssClass="cpHeaderContent" ScrollBars="None" Width="100%">
                                        EMPLOYEE DETAILS
                                    </asp:Panel>
                                    <asp:Panel ID="PanelForEmployeeDetailsBody" runat="server" Height="210px" Width="100%" ScrollBars="None">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 115px; text-align: left">
                                                    <asp:Label ID="Label20" runat="server" Text="Name"></asp:Label>
                                                </td>
                                                <td>:</td>
                                                <td style="text-align: left;width:250px">
                                                    <asp:Label ID="lblEmployeeName" runat="server"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width:100px">
                                                    <asp:Label ID="Label5" runat="server" Text="Office Location"></asp:Label>
                                                </td>
                                                <td style="text-align: left">:</td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblOfficeLocation" runat="server" Text="Label"></asp:Label>
                                                    <asp:Label ID="lblOfficeLocationCode" runat="server" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">
                                                    <asp:Label ID="Label6" runat="server" Text="Department"></asp:Label>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblEmployeeDepartment" runat="server" Text="Label"></asp:Label>
                                                    <asp:Label ID="lblDepartmentCode" runat="server" Visible="False"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="Label7" runat="server" Text="Section"></asp:Label>
                                                </td>
                                                <td style="text-align: left">:</td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblSection" runat="server" Text="Label"></asp:Label>
                                                    <asp:Label ID="lblSectionCode" runat="server" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">
                                                    <asp:Label ID="Label8" runat="server" Text="Designation"></asp:Label>
                                                </td>
                                                <td>:</td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblDesignation" runat="server" Text="Label"></asp:Label>
                                                    <asp:Label ID="lblDesignationCode" runat="server" Visible="False"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="Label9" runat="server" Text="Joining Date"></asp:Label>
                                                </td>
                                                <td style="text-align: left">:</td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblJoiningDate" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">
                                                    <asp:Label ID="Label24" runat="server" Text="Confirmation Date"></asp:Label>
                                                </td>
                                                <td>:</td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblConfirmationDate" runat="server"></asp:Label>
                                                </td>
                                                <td style="text-align: left">&nbsp;</td>
                                                <td style="text-align: left">&nbsp;</td>
                                                <td style="text-align: left">&nbsp;</td>
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
                                        ExpandedSize="200"
                                        AutoCollapse="False"
                                        AutoExpand="false"
                                        ScrollContents="false"
                                        ImageControlID="Image1"
                                        ExpandedImage="~/images/collapse.jpg"
                                        CollapsedImage="~/images/expand.jpg"
                                        ExpandDirection="Vertical">
                                    </ajaxToolkit:CollapsiblePanelExtender>
                                </div>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 125px">
                                        <asp:Label ID="Label19" runat="server" Text="Select Leave&nbsp; Type"></asp:Label>
                                    </td>
                                    <td style="width: 5px">:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlLeaveType" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlLeaveType_SelectedIndexChanged" Width="380px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Panel ID="panelForLeaveType" runat="server" Height="170px" Width="100%" ScrollBars="None">
                                <div style="text-align: center">
                                    <asp:Panel ID="panelAdTHD" runat="server" Width="100%" CssClass="cpHeaderContent" ScrollBars="None">
                                        DETAILS OF THE LEAVE TYPE
                                    </asp:Panel>
                                    <asp:Panel ID="panelAdTBody" runat="server" Height="150px" Width="100%" ScrollBars="None" Visible="False">
                                        <div style="text-align: center">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label12" runat="server" Text="Employee Type"></asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:Label ID="lblEmployeeType" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label10" runat="server" Text="Mode of Payment" Visible="False"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblModeofPayment" runat="server" Visible="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 115px; text-align: left">
                                                        &nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td style="text-align: left">
                                                        &nbsp;</td>
                                                    <td style="text-align: left">&nbsp;</td>
                                                    <td style="text-align: left">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label13" runat="server" Text="Carry Forword Next Year"></asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="lblCarryForwordNextYear" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">&nbsp;</td>
                                                    <td style="text-align: left">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label14" runat="server" Text="Maximum Leave Carry Forword to Next Year "></asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="lblMLCFTNYear" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">&nbsp;</td>
                                                    <td style="text-align: left">&nbsp;</td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:Panel>
                                    <ajaxToolkit:CollapsiblePanelExtender ID="panelAdTBody_CollapsiblePanelExtender" runat="server" Enabled="True"
                                        TargetControlID="panelAdTBody"
                                        CollapseControlID="panelAdTHD"
                                        ExpandControlID="panelAdTHD"
                                        Collapsed="false"
                                        TextLabelID="lblForAdvanceDetailsHD"
                                        CollapsedText="DETAILS OF THE LEAVE TYPE "
                                        ExpandedText="DETAILS OF THE LEAVE TYPE "
                                        CollapsedSize="2"
                                        ExpandedSize="150"
                                        AutoCollapse="False"
                                        AutoExpand="false"
                                        ScrollContents="false"
                                        ImageControlID="Image1"
                                        ExpandedImage="~/images/collapse.jpg"
                                        CollapsedImage="~/images/expand.jpg"
                                        ExpandDirection="Vertical">
                                    </ajaxToolkit:CollapsiblePanelExtender>
                                </div>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <%--<table style="width:100%;">--%>
                            <%--<tr>--%>
                            <%--<td>--%>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 125px">
                                        <asp:Label ID="Label11" runat="server" Text="Maximum Per Allow"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="lblMaximumPerAllow" runat="server" Font-Bold="True"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;Allocated Leave:&nbsp;
                                        <asp:Label ID="lblMaximumAllocated" runat="server" Font-Bold="True"></asp:Label>
                                        &nbsp;&nbsp;
                                        <asp:Label ID="Label15" runat="server" Text="Available Leave:"></asp:Label>
                                        &nbsp;<asp:Label ID="lblAvailableLeave" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label16" runat="server" Text="Leave Start Date"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <ew:CalendarPopup ID="calenderTargetDate" runat="server" AutoPostBack="True" Culture="English (United Kingdom)"
                                            Enabled="true" Width="100px" OnDateChanged="calenderTargetDate_DateChanged">
                                            <MonthHeaderStyle BackColor="#2A2965" />
                                            <ButtonStyle CssClass="btn2" />
                                        </ew:CalendarPopup>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label17" runat="server" Text="No of Days"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="txtNoOfDays" runat="server" Width="135px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label21" runat="server" Text="Address During Leave"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="txtAddressDuringLeave" runat="server" Width="550px"></asp:TextBox>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label22" runat="server" Text="Contact Number on Leave"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="txtContactNumber" runat="server" Width="200px"></asp:TextBox>
                                        &nbsp;
                                        <asp:Label ID="Label23" runat="server" Text="Responsible Person :"></asp:Label>
                                        &nbsp;<asp:TextBox ID="txtResponsiblePerson" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label18" runat="server" Text="Remarks"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="txtRemarks" runat="server" Height="35px" TextMode="MultiLine" Width="550px"></asp:TextBox>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnSaveLeaveRecord" runat="server" OnClick="btnSaveLeaveRecord_Click" OnClientClick="ShowConfirmBox(this,'Are you Sure Save Leave Record?'); return false;" Text="Save" Width="100px" />
                                        &nbsp;
                                        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="ShowConfirmBox(this,'Are you Sure Delete This Leave?'); return false;" Text="Delete" Width="100px" />
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div style="text-align: center">
                                            <asp:GridView ID="grdShowLeaveRecord" runat="server" Width="100%" AutoGenerateColumns="False" EmptyDataText="No Data Found "
                                                OnRowCommand="grdShowLeaveRecord_RowCommand" OnRowDeleting="grdShowLeaveRecord_RowDeleting" OnRowCreated="grdShowLeaveRecord_RowCreated">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <%# Container.DisplayIndex + 1 %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLeave_Det_Emp_Id" runat="server" Text='<%# Bind("Leave_Det_Emp_Id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmpName" runat="server" Text='<%# Bind("EmpName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Leave  Type Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLeave_Det_LCode" runat="server" Text='<%# Bind("Leave_Det_LCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Leave Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLeave_Mas_Name" runat="server" Text='<%# Bind("Leave_Mas_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Leave Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLeave_Det_Sta_Date" runat="server" Text='<%# Bind("Leave_Det_Sta_Date", "{0:d}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Days">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLeave_Det_Emp_Days" runat="server" Text='<%# Bind("Leave_Det_Emp_Days") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Address During Leave">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Address_During_Leave") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Contact Number">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblContactNumber" runat="server" Text='<%# Bind("Contact_Number_during_Leave") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Responsible Person">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblResponsiblePerson" runat="server" Text='<%# Bind("Responsible_Person_During_Leave") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("Atnd_det_rmks") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:CommandField ShowSelectButton="True">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:CommandField>
                                                    <asp:CommandField ShowDeleteButton="false">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:CommandField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>


        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveLeaveRecord" />
            <asp:PostBackTrigger ControlID="btnDelete" />

            
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 46 || charCode > 57)) {
                alert("Please Enter Only Numeric Value:");
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
