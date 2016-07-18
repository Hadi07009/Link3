<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmExpensesBillApplicationApproval.aspx.cs" Inherits="modules_HRMS_Approval_frmExpensesBillApplicationApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>

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
        .auto-style4 {
            width: 174px;
        }
        .auto-style5 {
            width: 165px;
        }
    </style>
    <asp:UpdatePanel ID="updtPnl" runat="server">
        <ContentTemplate>
            <div align="center">
                <asp:Panel ID="pnlSrchHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                    PENDING EXPENSES BILL APPROVAL
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
                        <asp:Label ID="lblleave" Text="EXPENSES BILL DETAILS" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Height="100%">
                        <table style="width: 99%; text-align: left">
                            <tr>
                                <td colspan="3">
                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="#3333FF"></asp:Label>
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
                                <td colspan="3">
                                    <asp:GridView ID="grdLunchBillDetails" runat="server"
                                        Width="100%" AutoGenerateColumns="False" OnRowDataBound="grdLunchBillDetails_RowDataBound" ShowFooter="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSl" runat="server" Text='<%# Container.DisplayIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTrndate" runat="server" Text='<%# Bind("atnd_det_date", "{0:d}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Job Reference / Description">
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
                                            <td class="auto-style5">
                                                <asp:Label ID="Label19" runat="server" Text="Less : Advance Received"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblAdvanceReceived" runat="server" Width="350px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style5">
                                                <asp:Label ID="Label20" runat="server" Text="Net Claim / (Refund)"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblNetClaim" runat="server" Width="350px"></asp:Label>
                                            </td>
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
                        CollapsedText="EXPENSES BILL DETAILS"
                        ExpandedText="EXPENSES BILL DETAILS"
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
