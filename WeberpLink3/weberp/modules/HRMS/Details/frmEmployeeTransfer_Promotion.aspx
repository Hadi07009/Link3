<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmEmployeeTransfer_Promotion.aspx.cs" Inherits="modules_HRMS_Details_frmEmployeeTransfer_Promotion" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>

            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="Employee Transfer/Promotion" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <table style="width: 70%; text-align: left">
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:RadioButtonList ID="rblForActionType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblForActionType_SelectedIndexChanged" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Value="T">Employee Transfer</asp:ListItem>
                                            <asp:ListItem Value="P">Employee Promotion</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>

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
                                        <asp:DropDownList ID="ddlcompany" runat="server" AutoPostBack="True" CssClass="tbl" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" Width="380px">
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
                                        <ajaxToolkit:AutoCompleteExtender ID="txtEmployeeCode_AutoCompleteExtender" runat="server" BehaviorID="txtEmployeeCode_AutoCompleteExtender" Enabled="true" MinimumPrefixLength="1" ServiceMethod="GetEmpId" ServicePath="~/modules/Payroll/WebService.asmx" TargetControlID="txtEmployeeCode">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Panel ID="PanelForEmployeeDetails" runat="server" Height="170px" Width="100%" ScrollBars="None">
                                <div style="text-align: center">
                                    <asp:Panel ID="PanelForDetailsHeader" runat="server" CssClass="cpHeaderContent" Height="15px" Width="100%" ScrollBars="None">
                                        <asp:Label ID="lblIntoEmployeeDetailsHD" runat="server" Text="EMPLOYEE DETAILS"></asp:Label>
                                    </asp:Panel>
                                </div>
                                <asp:Panel ID="PanelForEmployeeDetailsBody" runat="server" Height="155px" Width="100%" ScrollBars="None">
                                    <table style="width: 40%; text-align: left">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text="Name&nbsp;"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblEmployeeName" runat="server" Text="Label"></asp:Label>
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
                                                <asp:Label ID="lblJoiningDate" runat="server" Text="Label"></asp:Label>
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
                        <td colspan="3">
                            <asp:Panel ID="PanelForTransfer" runat="server" Height="180px" Width="100%">
                                <div style="text-align: center">
                                    <asp:Panel ID="PanelTransferHD" runat="server" CssClass="cpHeaderContent">
                                        <asp:Label ID="lblTransferHD" runat="server" Text="TRANSFER TO"></asp:Label>
                                    </asp:Panel>
                                </div>
                                <asp:Panel ID="PanelTransferBody" runat="server" Height="160px" Width="100%">
                                    <table style="width: 50%; text-align: left">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblRowNoForUpdate" runat="server" Visible="False"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label10" runat="server" Text="Office Location"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td>
                                                <asp:DropDownList ID="ddlOfficeLocation" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOfficeLocation_SelectedIndexChanged" Width="380px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label11" runat="server" Text="Department Code"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td>
                                                <asp:DropDownList ID="ddlDepartmentCode" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlDepartmentCode_SelectedIndexChanged" Width="380px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label12" runat="server" Text="Section Code"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td>
                                                <asp:DropDownList ID="ddlSectionCode" runat="server" Width="380px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 29px">
                                                <asp:Label ID="Label13" runat="server" Text="Transferred Date"></asp:Label>
                                            </td>
                                            <td style="height: 29px">:</td>
                                            <td style="height: 29px">
                                                <asp:TextBox ID="popupTransferredDate" runat="server" placeholder="Transfer Date" Width="375px"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="popupTransferredDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="popupTransferredDate">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <ajaxToolkit:CollapsiblePanelExtender ID="PanelTransferBody_CollapsiblePanelExtender" runat="server" Enabled="True"
                                    TargetControlID="PanelTransferBody"
                                    CollapseControlID="PanelTransferHD"
                                    ExpandControlID="PanelTransferHD"
                                    Collapsed="false"
                                    TextLabelID="lblTransferHD"
                                    CollapsedText="TRANSFER TO"
                                    ExpandedText="TRANSFER TO"
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
                        <td colspan="3">
                            <asp:Panel ID="PanelForPromotion" runat="server" Height="190px" Width="100%">
                                <div style="text-align: center">
                                    <asp:Panel ID="PanelPromotionHD" runat="server" CssClass="cpHeaderContent">
                                        <asp:Label ID="lblPromotionHD" runat="server" Text="PROMOTION"></asp:Label>
                                    </asp:Panel>
                                </div>
                                <asp:Panel ID="PanelPromotionBody" runat="server" Height="175px" Width="100%">
                                    <table style="width: 55%; text-align: left">
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label14" runat="server" Text="Designation&nbsp;"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td>
                                                <asp:DropDownList ID="ddlDesignation" runat="server" Width="380px" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label15" runat="server" Text="Promotion Date"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td>
                                                <asp:TextBox ID="popupPromotionDate" runat="server" placeholder="Promotion Date" Width="380px"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="popupPromotionDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="popupPromotionDate">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label16" runat="server" Text="Remarks"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td>
                                                <asp:TextBox ID="txtRemarks" runat="server" Height="60px" TextMode="MultiLine" Width="90%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <ajaxToolkit:CollapsiblePanelExtender ID="PanelPromotionBody_CollapsiblePanelExtender" runat="server" Enabled="True"
                                    TargetControlID="PanelPromotionBody"
                                    CollapseControlID="PanelPromotionHD"
                                    ExpandControlID="PanelPromotionHD"
                                    Collapsed="false"
                                    TextLabelID="lblPromotionHD"
                                    CollapsedText="PROMOTION"
                                    ExpandedText="PROMOTION"
                                    CollapsedSize="2"
                                    ExpandedSize="175"
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
                        <td colspan="3">
                            <div align="left">
                                <asp:Button ID="btnSaveActionType" runat="server" OnClick="btnSaveActionType_Click" Text="Save" Width="100px" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style9"></td>
                        <td class="auto-style7"></td>
                        <td class="auto-style7"></td>
                    </tr>
                    <tr>
                        <td class="auto-style7" colspan="3">
                            <asp:GridView ID="grdEmployeeTransfer" runat="server" AutoGenerateColumns="False" Width="100%" OnRowCommand="grdEmployeeTransfer_RowCommand" OnRowDeleting="grdEmployeeTransfer_RowDeleting" OnRowDataBound="grdEmployeeTransfer_RowDataBound">
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
                                            <asp:Label ID="lblCompanyCode" runat="server" Text='<%# Bind("companyID") %>'></asp:Label>
                                        </ItemTemplate>
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
                                            <asp:Label ID="lblEmployeeID" runat="server" Text='<%# Bind("employeeID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FromDepartment">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFromDepartment" runat="server" Text='<%# Bind("fromDepartment") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="From Department">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFromDepartmentName" runat="server" Text='<%# Bind("fromDepartmentName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FromSection">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFromSection" runat="server" Text='<%# Bind("fromSection") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="From Section">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFromSectionName" runat="server" Text='<%# Bind("fromSectionName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FromOfficeLocation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFromOfficeLocation" runat="server" Text='<%# Bind("fromOfficeLocation") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="From Office Location">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFromOfficeLocationName" runat="server" Text='<%# Bind("fromOfficeLocationName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ToDepartment">
                                        <ItemTemplate>
                                            <asp:Label ID="lblToDepartment" runat="server" Text='<%# Bind("toDepartment") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To Department">
                                        <ItemTemplate>
                                            <asp:Label ID="lblToDepartmentName" runat="server" Text='<%# Bind("toDepartmentName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ToSection">
                                        <ItemTemplate>
                                            <asp:Label ID="lblToSection" runat="server" Text='<%# Bind("toSection") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To Section">
                                        <ItemTemplate>
                                            <asp:Label ID="lblToSectionName" runat="server" Text='<%# Bind("toSectionName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ToOfficeLocation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblToOfficeLocation" runat="server" Text='<%# Bind("toOfficeLocation") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To Office Location">
                                        <ItemTemplate>
                                            <asp:Label ID="lblToOfficeLocationName" runat="server" Text='<%# Bind("toOfficeLocationName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transferred Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransDetDate" runat="server" Text='<%# Bind("transDetDate", "{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AutoSerial">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAutoSerial" runat="server" Text='<%# Bind("autoSerial") %>'></asp:Label>
                                        </ItemTemplate>
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
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="grdEmployeePromotion" runat="server" Width="100%" AutoGenerateColumns="False" OnRowCommand="grdEmployeePromotion_RowCommand" OnRowDeleting="grdEmployeePromotion_RowDeleting" OnRowDataBound="grdEmployeePromotion_RowDataBound">
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
                                    <asp:TemplateField HeaderText="CompanyID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCompanyID" runat="server" Text='<%# Bind("companyID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCompanyName" runat="server" Text='<%# Bind("CompanyName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmployeeID" runat="server" Text='<%# Bind("employeeID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Promotion Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransDetDate" runat="server" Text='<%# Bind("transDetDate", "{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="From Designation Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFromDesignation" runat="server" Text='<%# Bind("fromDesignation") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="From Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFromDesignationName" runat="server" Text='<%# Bind("fromDesignationName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To DesignationCode">
                                        <ItemTemplate>
                                            <asp:Label ID="lblToDesignation" runat="server" Text='<%# Bind("toDesignation") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblToDesignationName" runat="server" Text='<%# Bind("toDesignationName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("remarks") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AutoSerial">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAutoSerial" runat="server" Text='<%# Bind("autoSerial") %>'></asp:Label>
                                        </ItemTemplate>
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
            </asp:Panel>


        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveActionType" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
