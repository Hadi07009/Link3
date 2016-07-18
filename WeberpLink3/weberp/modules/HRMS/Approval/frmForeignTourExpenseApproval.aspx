<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmForeignTourExpenseApproval.aspx.cs" Inherits="modules_HRMS_Approval_frmForeignTourExpenseApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Src="../../../UserControls/ucLeaveDocument.ascx" TagName="ucLeaveDocument" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <cc1:ConfirmBox ID="ConfirmBox1" runat="server" />

    <style type="text/css">
        .style2 {
            height: 18px;
        }

        .style7 {
            width: 109px;
            text-align: left;
        }

        .style8 {
            width: 24px;
        }

        .style9 {
            width: 348px;
            text-align: left;
        }

        .style10 {
            width: 81px;
            text-align: left;
        }

        .style11 {
            width: 13px;
        }

        .style20 {
            width: 357px;
        }

        .style21 {
            width: 83px;
            text-align: left;
        }

        .style22 {
            width: 15px;
        }

        .style23 {
            width: 156px;
        }

        .style24 {
        }

        .style25 {
            width: 109px;
            text-align: left;
            height: 18px;
        }

        .style26 {
            width: 24px;
            height: 18px;
        }
        .auto-style6 {
            width: 176px;
        }
        .auto-style7 {
            width: 172px;
        }
        .auto-style8 {
            width: 176px;
            height: 20px;
        }
        .auto-style9 {
            height: 20px;
        }
        .auto-style12 {
            width: 146px;
        }
        .auto-style13 {
            width: 172px;
            height: 20px;
        }
        .auto-style14 {
            height: 20px;
            width: 37px;
        }
        .auto-style15 {
            width: 37px;
        }
        .auto-style16 {
            height: 20px;
            width: 14px;
        }
        .auto-style17 {
            width: 14px;
        }
    </style>
    <asp:UpdatePanel ID="updtPnl" runat="server">
        <ContentTemplate>
            <div align="center">
                <asp:Panel ID="pnlSrchHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                    PENDING FOREIGN TOUR EXPENSE APPROVAL
                </asp:Panel>
                <asp:Panel ID="pnlSrchDet" runat="server" CssClass="cpBodyContent" Height="100%">
                    <table style="width: 99%; text-align: left">
                        <tr>
                            <td>&nbsp;</td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:GridView ID="grdPendingExpensesBill" runat="server" Width="100%" AutoGenerateColumns="False" OnRowCommand="grdPendingExpensesBill_RowCommand" OnRowDataBound="grdPendingExpensesBill_RowDataBound" OnSelectedIndexChanged="grdPendingExpensesBill_SelectedIndexChanged">
                                    <Columns>
                                        <asp:BoundField DataField="SL" HeaderText="SL #" />
                                        <asp:BoundField DataField="TransactionNo" HeaderText="Referance" />
                                        <asp:BoundField DataField="ApplicantId" HeaderText="Emp ID" />
                                        <asp:BoundField DataField="EmpName" HeaderText="Name" />
                                        <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                        <asp:BoundField DataField="ProcessLevelid" HeaderText="Level" />
                                        <asp:BoundField DataField="ownerofthistask" HeaderText="Pending To" />
                                        <asp:CommandField ShowSelectButton="True" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderSrch" runat="server"
                    TargetControlID="pnlSrchDet"
                    CollapseControlID="pnlSrchHdr"
                    ExpandControlID="pnlSrchHdr"
                    Collapsed="false"
                    TextLabelID="lblSearchHdr"
                    CollapsedText="Pending Leave Application"
                    ExpandedText="Pending Leave Application"
                    AutoCollapse="False"
                    AutoExpand="false"
                    ScrollContents="false"
                    ImageControlID="Image1"
                    ExpandedImage="~/images/collapse.jpg"
                    CollapsedImage="~/images/expand.jpg"
                    ExpandDirection="Vertical">
                </cc1:CollapsiblePanelExtender>
                <br />
            </div>
            <asp:Panel ID="PanelForDetails" runat="server" Width="100%">
                <div align="center">
                    <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                        <asp:Label ID="lblleave" Text="FOREIGN TOUR EXPENSE DETAILS" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Height="100%">
                        <table style="width: 99%; text-align: left">
                            <tr>
                                <td colspan="3">
                                    <br />
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td class="style9">
                                                <asp:Label ID="Label7" runat="server" Text="Employee Information"></asp:Label>
                                            </td>
                                            <td class="style20">&nbsp;</td>
                                            <td class="style23">&nbsp;</td>
                                            <td style="text-align: center">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style9">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td class="style10">
                                                            <asp:Label ID="Label8" runat="server" Text="ID"></asp:Label>
                                                        </td>
                                                        <td class="style11">:</td>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="lblId" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style10">
                                                            <asp:Label ID="Label9" runat="server" Text="Name"></asp:Label>
                                                        </td>
                                                        <td class="style11">:</td>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="lblName" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style10">
                                                            <asp:Label ID="Label10" runat="server" Text="Department"></asp:Label>
                                                        </td>
                                                        <td class="style11">:</td>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="lbldept" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style10">
                                                            <asp:Label ID="Label11" runat="server" Text="Designation"></asp:Label>
                                                        </td>
                                                        <td class="style11">:</td>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style10">
                                                            <asp:Label ID="Label12" runat="server" Text="Joining Date"></asp:Label>
                                                        </td>
                                                        <td class="style11">:</td>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="lblJoiningDate" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="style20">&nbsp;</td>
                                            <td class="style23">&nbsp;</td>
                                            <td style="text-align: right">&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                                <td>&nbsp;</td>
                            </tr>

                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="Label21" runat="server" Font-Underline="True" Text="Details of Tour :"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr align="left">
                                <td colspan="4">
                                    <table style="width:70%; text-align:left">
                                <tr>
                                    <td style="width: 115px">
                                        <asp:Label ID="Label23" runat="server" Text="Place of tour"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td class="auto-style12">
                                        <asp:Label ID="lblPlaceoftour" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 39px">
                                        &nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                        <tr>
                                            <td style="width: 115px">
                                                <asp:Label ID="Label24" runat="server" Text="Country"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td class="auto-style12">
                                                <asp:Label ID="lblCountry" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 39px">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 115px">
                                                <asp:Label ID="Label32" runat="server" Text="Purpose"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td class="auto-style12">
                                                <asp:Label ID="lblPurpose" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 39px">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 115px">
                                                <asp:Label ID="Label26" runat="server" Text="Vendor"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td class="auto-style12">
                                                <asp:Label ID="lblVendor" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 39px">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 115px">
                                                <asp:Label ID="Label14" runat="server" Text="Date of Departure"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td class="auto-style12">
                                                <asp:Label ID="lblDateofDeparture" runat="server" Width="150px"></asp:Label>
                                            </td>
                                            <td style="width: 39px">
                                                <asp:Label ID="Label16" runat="server" Text="Time"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblDepartureTime" runat="server" Width="150px"></asp:Label>
                                            </td>
                                        </tr>
                                <tr>
                                    <td style="width: 115px">
                                        <asp:Label ID="Label31" runat="server" Text="Departure Flight"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td class="auto-style12">
                                        <asp:Label ID="lblDepartureFlight" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 39px">
                                        &nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                        <tr>
                                            <td style="width: 115px">
                                                <asp:Label ID="Label15" runat="server" Text="Date of Arrival"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td class="auto-style12">
                                                <asp:Label ID="lblDateofArrival" runat="server" Width="150px"></asp:Label>
                                            </td>
                                            <td style="width: 39px">
                                                <asp:Label ID="Label17" runat="server" Text="Time"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblArrivalTime" runat="server" Width="150px"></asp:Label>
                                            </td>
                                        </tr>
                                <tr>
                                    <td style="width: 115px">
                                        <asp:Label ID="Label1" runat="server" Text="Arrival Flight"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td class="auto-style12">
                                        <asp:Label ID="lblArrivalFlight" runat="server" Width="150px"></asp:Label>
                                    </td>
                                    <td style="width: 39px">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 115px">
                                        <asp:Label ID="Label2" runat="server" Text="Duration (Days)"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td class="auto-style12">
                                        <asp:Label ID="lblDuration" runat="server" Width="150px"></asp:Label>
                                    </td>
                                    <td style="width: 39px">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="Label22" runat="server" Font-Underline="True" Text="EXPENDITURE DETAILS AS FOLLOWS :"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>

                            <tr>
                                <td colspan="3">
                                    <asp:GridView ID="grdExpenseBillDetails" runat="server"
                                        Width="100%" AutoGenerateColumns="False" OnRowDataBound="grdExpenseBillDetails_RowDataBound" ShowFooter="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSl" runat="server" Text='<%# Container.DisplayIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <FooterTemplate>
                                                    <asp:Label ID="Label13" runat="server" Font-Bold="True" Text="Total Expenses - BDT"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblJobReference" runat="server" Text='<%# Bind("modeofJourney") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalAmount" runat="server" Font-Bold="True"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblamountCost" runat="server" Text='<%# Bind("amountCost","{0:0.00}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EmpName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpName" runat="server" Text='<%# Bind("EmpName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblType" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IsLineLocked">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIsLineLocked" runat="server" Text='<%# Bind("IsLineLocked") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IsProcessLocked">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIsProcessLocked" runat="server" Text='<%# Bind("IsProcessLocked") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAction" runat="server" Text='<%# Bind("Action") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ProcessLevelid">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProcessLevelid" runat="server" Text='<%# Bind("ProcessLevelid") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TransactionNo">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTransactionNo" runat="server" Text='<%# Bind("TransactionNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TransactionNoLineNo">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTransactionNoLineNo" runat="server" Text='<%# Bind("TransactionNoLineNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>


                            <tr>
                                <td colspan="4">
                                    <table style="width:99%;text-align:left">
                                        <tr>
                                            <td class="auto-style8">
                                                <asp:Label ID="Label19" runat="server" Text="Advanced received on BDT"></asp:Label>
                                            </td>
                                            <td class="auto-style9">:</td>
                                            <td class="auto-style13">
                                                <asp:Label ID="lblAdvanceReceived" runat="server" Width="170px"></asp:Label>
                                            </td>
                                            <td class="auto-style14">
                                                <asp:Label ID="Label29" runat="server" Text="Date"></asp:Label>
                                            </td>
                                            <td class="auto-style16">:</td>
                                            <td class="auto-style9">
                                                <asp:Label ID="lblAdvanceReceivedDate" runat="server" Width="170px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style8">
                                                <asp:Label ID="Label30" runat="server" Text="Actual Claim Amount BDT"></asp:Label>
                                            </td>
                                            <td class="auto-style9">:</td>
                                            <td class="auto-style13">
                                                <asp:Label ID="lblActualClaimAmountBDT" runat="server" Width="170px"></asp:Label>
                                            </td>
                                            <td class="auto-style14">&nbsp;</td>
                                            <td class="auto-style16">&nbsp;</td>
                                            <td class="auto-style9">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style6">
                                                <asp:Label ID="Label20" runat="server" Text="Net Claim / (Refund)"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td class="auto-style7">
                                                <asp:Label ID="lblNetClaim" runat="server" Width="170px"></asp:Label>
                                            </td>
                                            <td class="auto-style15">&nbsp;</td>
                                            <td class="auto-style17">&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>


                            <tr>
                                <td colspan="3">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td class="style24" style="text-align: left">&nbsp;</td>
                                            <td style="text-align: left">
                                                <asp:Button ID="btnForward" runat="server"
                                                    Text="Forward" Width="100px" OnClick="btnForward_Click" />
                                                <asp:Button ID="btnReturn" runat="server"
                                                    Text="Return" Width="100px" OnClick="btnReturn_Click" />
                                                <asp:Button ID="btnReject" runat="server"
                                                    Text="Reject" Width="100px" OnClick="btnReject_Click" />
                                                <asp:Button ID="btnApprove" runat="server"
                                                    Text="Approve" Width="100px" OnClick="btnApprove_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server"
                        TargetControlID="PanelLeavedet"
                        CollapseControlID="PanelLeaveHdr"
                        ExpandControlID="PanelLeaveHdr"
                        Collapsed="false"
                        TextLabelID="lblleave"
                        CollapsedText="FOREIGN TOUR EXPENSE DETAILS"
                        ExpandedText="FOREIGN TOUR EXPENSE DETAILS"
                        AutoCollapse="False"
                        AutoExpand="false"
                        ScrollContents="false"
                        ImageControlID="Image1"
                        ExpandedImage="~/images/collapse.jpg"
                        CollapsedImage="~/images/expand.jpg"
                        ExpandDirection="Vertical">
                    </cc1:CollapsiblePanelExtender>
                </div>
            </asp:Panel>
            <br />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnApprove" />
            <asp:PostBackTrigger ControlID="btnReject" />
            <asp:PostBackTrigger ControlID="btnReturn" />
            <asp:PostBackTrigger ControlID="btnForward" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
